// load dataframe from parquet file
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Parquet;
using Parquet.Data;

namespace IFs.Data
{

    // dataframe implementation (parquet I/O)
    public partial class Dataframe : AbstractDataframe
    {
        // Load dataframe content from the stream
        public static Dataframe FromStream(Stream stream, IContext context)
        {
            using (DataframeReader reader = new DataframeReader(stream, context)) {
                return reader.Read();
            }
        }

        public override byte[] ToBytes()
        {
            MemoryStream stream = new MemoryStream();
            using (DataframeWriter writer = new DataframeWriter(stream)) {
                writer.Write(this);
            }
            stream.Seek(0L, SeekOrigin.Begin);
            return stream.ToArray();
        }

    }

    internal class DataframeReader : ParquetReader
    {
        private IContext context { get; }

        public DataframeReader(Stream input, IContext context) : base(input, Dataframe.PARQUET_OPTIONS, null)
        {
            this.context = context;
        }

        // Load the entire dataframe to memory, the interface design leaves an opportunity
        // to push some or all of the predicates out-of-the core, i.e. filter on-disk data
        // without loading the entire dataframe. However, given that average dataframe sizes
        // are only a few KB, the performance benefit is not worth the added complicity.
        public IDataframe Apply(IQueryBuilder source)
        {
            // load the entire dataframe
            Dataframe df = Read();
            if (source.Filters.Count < 1) {
                return df;
            }
            // apply source query predicates to target query
            IQueryBuilder target = df.Filter();
            foreach (var pred in source.Filters.Values) {
                target = target.Filter(pred);
            }
            // execute the query against the dataframe
            return target.Execute();
        }

        public new Dataframe Read()
        {
            // load parquet dataset
            DataSet ds = base.Read();
            // verify variable name
            string variableName = null;
            if (!ds.Metadata.Custom.TryGetValue(Dataframe.IFS_VARIABLE_NAME, out variableName)) {
                throw new ParquetException("Invalid dataframe: missing variable name.");
            }
            // instantiate dataframe
            Dataframe df = Dataframe.Create(variableName, context, ds.RowCount);
            df.GetHandle().Merge(ds);
            return df;
        }
    }

    // save dataframe to parquet file
    internal class DataframeWriter : ParquetWriter
    {
        public DataframeWriter(Stream output) : base(output, Dataframe.PARQUET_OPTIONS, null)
        {}

        public void Write(Dataframe df)
        {
            base.Write(df.GetHandle(), CompressionMethod.Snappy, false);
        }
    }

}
