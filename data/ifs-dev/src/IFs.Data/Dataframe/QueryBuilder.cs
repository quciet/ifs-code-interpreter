using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Parquet;
using Parquet.Data;

namespace IFs.Data
{
    public abstract class QueryBuilder : IQueryBuilder
    {
        public IVariable Variable { get; }

        public IReadOnlyList<IDimension> Dimensions { get; }

        public IDictionary<int, IQueryFilter> Filters { get; }

        protected QueryBuilder(IVariable variable, IReadOnlyList<IDimension> dimensions)
        {
            this.Variable = variable;
            this.Dimensions = dimensions;
            this.Filters = new SortedList<int, IQueryFilter>();
        }

        public virtual IQueryBuilder Filter(IQueryFilter pred)
        {
            if (pred == null) {
                return this;
            }
            int dimensionSeq = this.Dimensions.Select(d => d.Id).ToList().IndexOf(pred.Dimension.Id);
            if (dimensionSeq < 0) {
                throw new ArgumentException($"Variable does not have a dimension with ID {pred.Dimension}");
            }
            IQueryFilter oldPred = null;
            if (this.Filters.TryGetValue(dimensionSeq, out oldPred)) {
                IQueryFilter newPred = oldPred.Conjunct(pred);
                if (newPred != null) {
                    this.Filters[dimensionSeq] = newPred;
                } else {
                    this.Filters.Remove(dimensionSeq);
                }
            } else {
                this.Filters[dimensionSeq] = pred;
            }
            return this;
        }

        public abstract IDataframe Execute();
    }

    // Query data from storage service
    internal class StorageQueryBuilder : QueryBuilder
    {
        public static StorageQueryBuilder Create(string variableName, IStorage storage)
        {
            IVariable variable;
            IReadOnlyList<IDimension> dimensions;
            variable = storage.GetVariable(variableName);
            dimensions = (from d in variable.Dimensions select storage.GetDimension(d)).ToList();
            return new StorageQueryBuilder(variable, dimensions, storage);
        }

        private readonly IStorage _storage;

        private StorageQueryBuilder(IVariable variable, IReadOnlyList<IDimension> dimensions, IStorage storage) : base(variable, dimensions)
        {
            this._storage = storage;
        }

        private IDataframe applyToStream(Stream stream)
        {
            using (DataframeReader reader = new DataframeReader(stream, this._storage)) {
                return reader.Apply(this);
            }
        }

        public override IDataframe Execute()
        {
            using (Stream s = this._storage.GetVariableStream(this.Variable.Name)) {
                if (s == null) {
                    return new Dataframe(this.Variable, this.Dimensions);
                } else if (s.Length == 0) {
                    return new Dataframe(this.Variable, this.Dimensions);
                } else {
                    return this.applyToStream(s);
                }
            }
        }
    }

    // Query data from dataframe
    internal class DataframeQueryBuilder : QueryBuilder
    {
        private readonly Dataframe df;

        public DataframeQueryBuilder(Dataframe df) : base(df.Variable, df.Dimensions)
        {
            this.df = df;
        }

        public override IDataframe Execute()
        {
            if (this.Filters.Count < 1 || this.df.RowCount < 1) {
                return this.df;
            }
            DataSet _handle = this.df.GetHandle();
            IReadOnlyDictionary<string, IList> _target = _handle.GetColumns();
            int rowCount = System.Convert.ToInt32(this.df.RowCount);
            BitArray keep = new BitArray(rowCount, true);
            foreach (var pred in this.Filters) {
                IList column = _target[df.Fields[pred.Key]];
                BitArray mask = new BitArray(rowCount, false);
                foreach (var iRow in column.FindIndices<short>(pred.Value.Matcher)) {
                    mask.Set(iRow, true);
                }
                keep.And(mask);
            }
            return Dataframe.CloneWhere(this.df, keep);
        }
    }

}
