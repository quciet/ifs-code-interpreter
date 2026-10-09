using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IFs.Data
{

    public static class QueryExtensions
    {
        // Query filter for `not equal to` (<>)
        public static IQueryFilter NEQ(this IDimension dimension, short operand)
        {
            return QueryFilter.Create(FilterType.NotEqual, dimension, operand);
        }

        // Query filter for `equal to` (=)
        public static IQueryFilter EQ(this IDimension dimension, short operand)
        {
            return QueryFilter.Create(FilterType.Equal, dimension, operand);
        }

        // Query filter for `is element of` (∈)
        public static IQueryFilter ANY(this IDimension dimension, params short[] operands)
        {
            return QueryFilter.Create(FilterType.ElementOf, dimension, operands);
        }

        // Query filter for `is between bounds` (inclusive [a, b])
        public static IQueryFilter BTWN(this IDimension dimension, short lbound, short ubound)
        {
            return QueryFilter.Create(FilterType.Between, dimension, lbound, ubound);
        }

        // Query filter for `always-empty` (`is element of empty set`)
        public static IQueryFilter EMPTY(this IDimension dimension)
        {
            return QueryFilter.Create(FilterType.ElementOf, dimension);
        }

        // Equality comparer for query filters
        public static bool Equal(this IQueryFilter target, IQueryFilter source)
        {
            return (target.Type == source.Type) && 
                (target.Dimension.Id == source.Dimension.Id) && 
                Enumerable.SequenceEqual(target.Operands, source.Operands);
        }

        // Load a dataframe from storage service
        public static IDataframe LoadDataframe(this IStorage run, string variableName)
        {
            // load empty stream as empty dataframe
            using (Stream s = run.GetVariableStream(variableName))
            {
                if (s == null) {
                    return Dataframe.Create(variableName, run);
                } else if (s.Length == 0) {
                    return Dataframe.Create(variableName, run);
                } else {
                    return Dataframe.FromStream(s, run);
                }
            }
        }

        // Save a dataframe to storage service
        public static void SaveDataframe(this IStorage run, IDataframe df)
        {
            // save empty dataframe as null
            if (df.RowCount > 0) {
                byte[] payload = df.ToBytes();
                if (payload.Length > 0) {
                    run.PutVariableData(df.Variable.Name, payload);
                } else {
                    run.PutVariableData(df.Variable.Name, null);
                }
            } else {
                run.PutVariableData(df.Variable.Name, null);
            }
        }

        // Enumerate all dataframes from storage service
        public static IEnumerable<IDataframe> LoadAllDataframes(this IStorage run) {
            var prefetch = PrefetchContext.Create(run);
            return run.GetAllVariableData().AsParallel().Select((kv) => {
                if ((kv.Value == null) || (kv.Value.Length == 0)) {
                    return Dataframe.Create(kv.Key, prefetch);
                }
                using (var s = new MemoryStream(kv.Value)) {
                    if ((s == null) || (s.Length == 0)) {
                        return Dataframe.Create(kv.Key, prefetch);
                    }
                    return Dataframe.FromStream(s, prefetch);
                }
            });
        }

    }

}
