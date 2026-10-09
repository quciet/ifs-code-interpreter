using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{

    // equality comparator for two arrays of integers
    public class ShortIntKeyComparer: IEqualityComparer<short[]>, IComparer<short[]>
    {
        public static readonly IEqualityComparer<short[]> DEFAULT_INSTANCE = new ShortIntKeyComparer();

        public int Compare(short[] x, short[] y) {
            for (int i = 0; i < Math.Min(x.Length, y.Length); i++) {
                if (x[i] != y[i]) {
                    return x[i] - y[i];
                }
            }
            if (x.Length < y.Length) {
                return -1;
            } else if (x.Length > y.Length) {
                return 1;
            } else {
                return 0;
            }
        }

        public bool Equals(short[] x, short[] y) {
            return Enumerable.SequenceEqual(x, y);
        }

        public int GetHashCode(short[] obj) {
            int hash = 17;
            foreach (int elem in obj) {
                hash = hash * 23 + elem;
            }
            return hash;
        }
    }

    public static class DataExtensions
    {
        public const int DIM_TIME = 0;

        public const string DIM_TIME_NAME = "Time";

        public const int DIM_REGION = 1;

        public const string DIM_REGION_NAME = "Region";

        internal struct ColumnarIterator<T>: IEnumerator<T>
        {
            private readonly int left, right;

            private readonly T[] data;

            private int i, j, k;

            public ColumnarIterator(T[] data, int left, int right) {
                this.data = data;
                this.left = left;
                this.right = right;
                i = j = 0;
                k = -1;
            }

            public void Dispose() {
                // NOP
            }

            public bool MoveNext() {
                if (++k >= right) {
                    k = 0;
                    if (++j >= data.Length) {
                        j = 0;
                        ++i;
                    }
                }
                return i < left;
            }

            public void Reset() {
                i = j = 0;
                k = -1;
            }

            public T Current {
                get {
                    return data[j];
                }
            }

            object IEnumerator.Current {
                get {
                    return data[j];
                }
            }

        }

        internal sealed class ColumnarEnumerator<T> : IEnumerable<T>
        {
            private readonly T[] data;
            private readonly int left, right;
            public ColumnarEnumerator(T[] data, int left, int right) {
                this.data = data;
                this.left = left;
                this.right = right;
            }

            public IEnumerator<T> GetEnumerator() {
                return new ColumnarIterator<T>(data, left, right);
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this.GetEnumerator();
            }

        }

        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
        { 
            var buckets = sequences.Select(s => s.ToArray()).ToArray();
            // corner case #1
            int n = buckets.Length;
            if (n == 0) {
                yield return Enumerable.Empty<T>();
                yield break;
            }
            // corner case #2
            int total = buckets.Select(b => b.Length).Aggregate(1, (a, b) => a * b);
            if (total == 0) {
                yield return Enumerable.Empty<T>();
                yield break;
            }
            // regular cases of one or more dimenson buckets
            int left = 1;
            int right = total;
            foreach (var b in buckets) {
                int size = b.Length;
                right /= size;
                yield return new ColumnarEnumerator<T>(b, left, right);
                left *= size;
            }
        }

        internal struct ZippedIterator<T>: IEnumerator<IEnumerable<T>>
        {

            private IEnumerator<T>[] columns;

            private IEnumerable<T> buffer;

            public ZippedIterator(IEnumerator<T>[] columns) {
                this.columns = columns;
                this.buffer = null;
            }

            public void Dispose() {
                foreach (var col in columns) {
                    col.Dispose();
                }
                columns = null;
                buffer = null;
            }

            public bool MoveNext() {
                foreach (var col in columns) {
                    if (!col.MoveNext()) {
                        return false;
                    }
                }
                buffer = columns.Select(c => c.Current);
                return true;
            }

            public void Reset() {
                foreach (var col in columns) {
                    col.Reset();
                }
                buffer = null;
            }

            public IEnumerable<T> Current {
                get {
                    return buffer;
                }
            }

            object IEnumerator.Current {
                get {
                    return buffer;
                }
            }
        }

        internal sealed class ZippedEnumerator<T> : IEnumerable<IEnumerable<T>>
        {
            private readonly IEnumerator<T>[] columns;
            public ZippedEnumerator(IEnumerator<T>[] columns) {
                this.columns = columns;
            }

            public IEnumerator<IEnumerable<T>> GetEnumerator() {
                return new ZippedIterator<T>(columns);
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }

        }

        public static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> sequences)
        {
            var enumerators = sequences.Select(s => s.GetEnumerator()).ToArray();
            // corner case #1
            if (enumerators.Length == 0) {
                return new IEnumerable<T>[] { Enumerable.Empty<T>() };
            }
            // corner case #2
            foreach (var e in enumerators) {
                if (!e.MoveNext()) {
                    return new IEnumerable<T>[] { Enumerable.Empty<T>() };
                } else {
                    e.Reset();
                }
            }
            // regular cases of one or more enumerators
            return new ZippedEnumerator<T>(enumerators);
        }

        // Generate a (row-first or column-first) dimension-only structure for the specified variable
        public static IEnumerable<IEnumerable<short>> MeshGrid(this IEnumerable<IDimension> dimensions, bool rowFirst = true) {
            if (rowFirst) {
                return dimensions.Select(d => d.Buckets.Keys).CartesianProduct().Transpose();
            } else {
                return dimensions.Select(d => d.Buckets.Keys).CartesianProduct();
            }
        }

        // 
        public static int? FirstOrNull(this IEnumerable<int> elements, Func<int, bool> predicate) {
            foreach (var item in elements) {
                if (predicate(item)) {
                    return item;
                }
            }
            return null;
        }

        // Find zero-based locations of values within elements, where filter evaluates to true.
        public static IEnumerable<int> FindIndices<T>(this IEnumerable<T> elements, Func<T, bool> predicate) {
            return elements.Select((value, index) => new {value, index}).Where(x => predicate(x.value)).Select(x => x.index);
        }

        public static IEnumerable<int> FindIndices<T>(this IEnumerable elements, Func<T, bool> predicate) {
            return elements.Cast<T>().FindIndices(predicate);
        }

        // Test if values within elements are consecutive (difference by one)
        public static bool IsConsecutive(this IEnumerable<short> elements) {
            return elements.Select((value, index) => value - index).Distinct().Skip(1).Any();
        }

        public static bool IsConsecutive(this IEnumerable<int> elements) {
            return elements.Select((value, index) => value - index).Distinct().Skip(1).Any();
        }

        // get the System.Type T of the entity (DbSet<T>) given its property name
        internal static Type GetModelType(this DbContext context, string propertyName) {
            PropertyInfo prop = context.GetType().GetProperty(
                propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (prop == null) {
                return null;
            }
            return prop.PropertyType.GetGenericArguments()[0];
        }

        internal static IEnumerable<string> FindFields(this DbContext context, Type modelType) {
        	//gvdb 2024/04/05
        	// update for new EF syntax
            var tableName = context.Model.FindEntityType(modelType).GetTableName();
            var conn = context.Database.GetDbConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            using (var cmd = conn.CreateCommand()) {
#if USE_JET
                // OLEDB / ACE does not have a LIMIT keyword, and TOP n does not support n = 0
                cmd.CommandText = $"SELECT * FROM [{tableName}] WHERE 1 = 2";
#else
                cmd.CommandText = $"SELECT * FROM [{tableName}] LIMIT 0";
#endif
                using (var reader = cmd.ExecuteReader()) {
                    for (int i = 0; i < reader.FieldCount; i++) {
                        yield return reader.GetName(i);
                    }
                }
            }
        }

        internal static IEnumerable<T> DistinctBy<T, TIdentity>(this IEnumerable<T> source, Func<T, TIdentity> identitySelector)
        {
            return source.Distinct(By(identitySelector));
        }

        private static IEqualityComparer<TSource> By<TSource, TIdentity>(Func<TSource, TIdentity> identitySelector)
        {
            return new DelegateComparer<TSource, TIdentity>(identitySelector);
        }

        private class DelegateComparer<T, TIdentity> : IEqualityComparer<T>
        {
            private readonly Func<T, TIdentity> identitySelector;

            public DelegateComparer(Func<T, TIdentity> identitySelector)
            {
                this.identitySelector = identitySelector;
            }

            public bool Equals(T x, T y)
            {
                return Equals(identitySelector(x), identitySelector(y));
            }

            public int GetHashCode(T obj)
            {
                return identitySelector(obj).GetHashCode();
            }
        }

    }

}
