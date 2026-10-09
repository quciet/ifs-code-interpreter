using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Parquet;
using Parquet.Data;

namespace IFs.Data
{
    // dataframe implementation (internals)
    public partial class Dataframe : AbstractDataframe
    {
        internal static ParquetOptions PARQUET_OPTIONS { get; } = new ParquetOptions();

        internal static string IFS_VARIABLE_NAME { get; } = "ifs.variable.name";

        internal static Dataframe Allocate(IVariable variable, IReadOnlyList<IDimension> dimensions, short startYear, short endYear, float? value = null)
        {
            int blockSize = (from d in dimensions.Skip(1) select d.Buckets.Count).Aggregate(1, (a, b) => a * b);
            int totalRowCount = blockSize * (endYear - startYear + 1);
            Dataframe df = new Dataframe(variable, dimensions, totalRowCount);
            // directly operate on dataframe's internal structures for high-performance insertion
            IReadOnlyDictionary<string, IList> _target = df._handle.GetColumns();
            int iCol = 0;
            // column[0]: time dimension
            IList _column = _target[df.Fields[iCol]];
            for (short year = startYear; year <= endYear; year++)
            {
                for (int iRow = 0; iRow <= blockSize - 1; iRow++) {
                    _column.Add(year);
                }
            }
            // column[1 .. block.Length]: non-time dimensions (when the variable does not
            // have non-time dimensions, the shape of block is [[]], i.e. an 2D-array of
            // shape 1 (rows) x 0 (_columns), in this case, we just skip the entire block.
            if (df.Dimensions.Count > 1)
            {
                short[][] block = (from col in df.Dimensions.Skip(1).MeshGrid(false)
                                   select col.ToArray()).ToArray();
                while (iCol < block.Length)
                {
                    iCol += 1;
                    _column = _target[df.Fields[iCol]];
                    for (short year = startYear; year <= endYear; year++)
                    {
                        foreach (var cell in block[iCol - 1]) {
                            _column.Add(cell);
                        }
                    }
                }
            }
            // column[block.Lengh + 1]: value
            _column = _target[df.Fields[iCol + 1]];
            for (short year = startYear; year <= endYear; year++)
            {
                for (int iRow = 0; iRow <= blockSize - 1; iRow++) {
                    _column.Add(value);
                }
            }
            // update row count
            df._handle.SetRowCount(totalRowCount);
            return df;
        }

        internal static Dataframe Allocate(IVariable variable, IReadOnlyList<IDimension> dimensions, short startYear, short endYear, IEnumerable<float?> values)
        {
            Dataframe df = Dataframe.Allocate(variable, dimensions, startYear, endYear);
            IEnumerator<float?> enumerator = values.GetEnumerator();
            for (int iRow = 0; iRow <= df.RowCount - 1; iRow++)
            {
                if (enumerator.MoveNext()) {
                    df[iRow] = enumerator.Current;
                } else {
                    break;
                }
            }
            return df;
        }

        internal static Dataframe Clone(Dataframe source)
        {
            Dataframe target = new Dataframe(source.Variable, source.Dimensions, source.RowCount);
            if (source._handle.RowCount > 0) {
                target._handle.Merge(source._handle);
            }
            return target;
        }

        internal static Dataframe CloneWhere(Dataframe source, BitArray cond)
        {
            int[] keepIndex = cond.FindIndices<bool>(x => x).ToArray();
            Dataframe target = new Dataframe(source.Variable, source.Dimensions, keepIndex.Length);
            // directly operate on dataframes' internal structures for high-performance insertion
            IReadOnlyDictionary<string, IList> _source = source._handle.GetColumns();
            IReadOnlyDictionary<string, IList> _target = target._handle.GetColumns();
            for (int iCol = 0; iCol <= source.Fields.Count - 1; iCol++)
            {
                IList _from = _source[source.Fields[iCol]];
                IList _to = _target[source.Fields[iCol]];
                foreach (var iRow in keepIndex) {
                    _to.Add(_from[iRow]);
                }
            }
            target._handle.SetRowCount(keepIndex.Count());
            return target;
        }

        private readonly DataSet _handle;

        public override object Handle
        {
            get
            {
                return this._handle;
            }
        }

        public override int RowCount
        {
            get
            {
                return this._handle.RowCount;
            }
        }

        private readonly IList _values;

        // names of columns (for internal use)
        internal IReadOnlyList<string> Fields { get; }

        public override float? this[int index]
        {
            get
            {
                return (float?)this._values[index];
            }
            set
            {
                this._values[index] = value;
            }
        }

        // map from categorial dimension buckets to value offsets (for internal use)
        private readonly Lazy<IDictionary<short[], int>> _key2loc;

        public override int Loc(short[] keys)
        {
            return this._key2loc.Value[keys];
        }

        private Dataframe(IVariable variable, int? capacity = null)
        {
            this._handle = new DataSet(variable.GetSchema());
            this._handle.Metadata.Custom.Add(IFS_VARIABLE_NAME, variable.Name);
            this.Fields = (from f in this._handle.Schema.Fields
                           select f.Name).ToList();
            if (capacity.HasValue) {                
                this.patchHandle(Math.Max(capacity.Value, 768));
            } else {
                this.patchHandle(768);
            }
            this._values = _handle.GetColumns()["v"];
            this._key2loc = new Lazy<IDictionary<short[], int>>(() => this.buildIndex());
        }

        private IDictionary<short[], int> buildIndex()
        {
            IDictionary<short[], int> idx = new Dictionary<short[], int>(ShortIntKeyComparer.DEFAULT_INSTANCE);
            int row = 0;
            foreach (var rec in this)
            {
                idx.Add(rec.Key, row);
                row += 1;
            }
            return idx;
        }

        // As of Parquet.NET v2.1.4, calling ds.Merge() on an empty target ds would throw
        // `KeyNotFoundException` because the column dict was not initialized, until there
        // is data added to the dataset. following is a hack to overcome this issue before
        // there is an official fix from the upstream. This hack assumes a particular schema
        // being used internal to dataframes, don't use it unless absolutely necessary.
        private void patchHandle(int capacity)
        {
            IDictionary<string, IList> _columns = _handle.GetColumns();
            if (_handle.RowCount < 1 && _columns.Count < Fields.Count)
            {
                string name;
                // dimension _columns
                for (var iCol = 0; iCol <= Fields.Count - 2; iCol++)
                {
                    name = Fields[iCol];
                    if (!_columns.ContainsKey(name))
                    {
                        _columns.Add(name, new List<short>(capacity));
                    }
                }
                // value column
                name = Fields[Fields.Count - 1];
                if (!_columns.ContainsKey(name)) {
                    _columns.Add(name, new List<float?>(capacity));
                }
            }
        }

        public override IDataframe Append(params IEnumerable<object>[] source)
        {
            // if source is empty, return current datafram as-is.
            if (!source.Any()) {
                return this;
            }
            // otherwise, clone current datafarme and merge the content of source into it
            Dataframe df = Dataframe.Clone(this);
            foreach (var row in source) {
                df._handle.Add(row.ToArray());
            }
            return df;
        }

        public override IDataframe Append(IDataframe source)
        {
            return Append(source.GetHandle());
        }

        private Dataframe Append(DataSet source)
        {
            string variableName = null;
            if (!source.Metadata.Custom.TryGetValue(IFS_VARIABLE_NAME, out variableName))
            {
                throw new ArgumentException("Invalid dataframe: missing variable name.");
            } else if (variableName != this.Variable.Name) {
                throw new ArgumentException("Invalid dataframe: variable mismatch.");
            }
            // if source is empty, return current datafram as-is.
            if (source.RowCount < 1) {
                return this;
            }
            // otherwise, clone current datafarme and merge the content of source into it
            Dataframe df = Dataframe.Clone(this);
            df._handle.Merge(source);
            return df;
        }

        public override IEnumerator<IDataframeRecord> GetEnumerator()
        {
            IReadOnlyDictionary<string, IList> _target = this._handle.GetColumns();
            List<IList<short>> keyCols = new List<IList<short>>(this.Fields.Count - 1);
            int iCol = 0;
            while (iCol < this.Fields.Count - 1) {
                keyCols.Add((IList<short>)_target[this.Fields[iCol]]);
                iCol += 1;
            }
            IList<float?> valueCol = (IList<float?>)_target[this.Fields[iCol]];
            return new DataframeRecordEnumerator(keyCols, valueCol);
        }

        public override void Dispose()
        {}
    }

    // Row enumerator of dataframe
    internal sealed class DataframeRecordEnumerator : IEnumerator<IDataframeRecord>
    {
        private readonly IEnumerator<IEnumerable<short>> keyCursor;

        private readonly IEnumerator<float?> valueCursor;

        public DataframeRecordEnumerator(
            IEnumerable<IList<short>> keyCols,
            IList<float?> valueCol
        )
        {
            this.keyCursor = keyCols.Transpose().GetEnumerator();
            this.valueCursor = valueCol.GetEnumerator();
        }

        object IEnumerator.Current
            => new DataframeRecord(keyCursor.Current, valueCursor.Current);

        IDataframeRecord IEnumerator<IDataframeRecord>.Current
            => new DataframeRecord(keyCursor.Current, valueCursor.Current);

        bool IEnumerator.MoveNext()
        {
            return this.keyCursor.MoveNext() && this.valueCursor.MoveNext();
        }

        void IEnumerator.Reset()
        {
            this.keyCursor.Reset();
            this.valueCursor.Reset();
        }

        void IDisposable.Dispose()
        {
            this.keyCursor.Dispose();
            this.valueCursor.Dispose();
        }
    }

    // Cell enumerator of dataframe row
    internal struct DataframeRecord : IDataframeRecord
    {
        private readonly short[] _key;

        private readonly float? _value;

        public DataframeRecord(IEnumerable<short> key, float? value)
        {
            this._key = key.ToArray();
            this._value = value;
        }

        public short[] Key
            => this._key;

        public float? Value
            => this._value;

        public int Length
            => this._key.Length + 1;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._key.Cast<object>().Concat(new object[] { this._value }).GetEnumerator();
        }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            return this._key.Cast<object>().Concat(new object[] { this._value }).GetEnumerator();
        }

    }

}
