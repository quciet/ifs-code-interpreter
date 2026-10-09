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

    internal static class ParquetExtensions
    {

        // Build the Parquet.Data.Schema for the variable
        internal static IEnumerable<DataField> GetSchema(this IVariable variable)
        {
            //jrs 2019/06/11
            var b1Done = false;
            foreach (var d in variable.Dimensions) {
                //jrs 2019/06/11 *** begin ***
                //yield return new DataField(System.Convert.ToString(d), DataType.Short);
                if (d != 1 || b1Done == false)
                {
                    yield return new DataField(System.Convert.ToString(d), DataType.Short);
                    if (d == 1) b1Done = true;
                }
                else {
                    yield return new DataField("1_2", DataType.Short);
                }
                //jrs 2019/06/11 *** end ***
            }
            yield return new DataField("v", DataType.Float, true);
        }

        // Get a mutable representation of a Parquet.Data.DataSet's column data buffer
        internal static Dictionary<string, IList> GetColumns(this DataSet ds)
        {
            var field = ds.GetType().GetField("_columns", BindingFlags.NonPublic | BindingFlags.Instance);
            return (Dictionary<string, IList>)field.GetValue(ds);
        }

        // Update Parquet.Data.DataSet's internal row counter
        internal static void SetRowCount(this DataSet ds, int value)
        {
            var field = ds.GetType().GetField("_rowCount", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(ds, value);
        }

        // Get a handle for accessing an IDataframe's internal Parquet.Data.DataSet instance
        internal static DataSet GetHandle(this IDataframe df)
        {
            return (DataSet)df.Handle;
        }
    }

}
