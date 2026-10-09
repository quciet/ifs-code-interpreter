using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IFs.Data
{

    // Types of query filters
    public enum FilterType
    {
        NotEqual,
        Equal,
        ElementOf,
        Between
    }

    // Query filter
    public interface IQueryFilter
    {
        // Type of the filter
        FilterType Type { get; }

        // Dimension to apply the filter against
        IDimension Dimension { get; }

        // Operands of the filter
        IEnumerable<short> Operands { get; }

        // Matcher function for evaluating the filter
        Func<short, bool> Matcher { get; }

        // Logical AND with another filter
        IQueryFilter Conjunct(IQueryFilter other);

        // Logical OR with another filter
        IQueryFilter Disjunct(IQueryFilter other);
    }

    // Build queries against various data source
    public interface IQueryBuilder
    {
        // Target variable to query data against
        IVariable Variable { get; }

        // Dimensions of the variable
        IReadOnlyList<IDimension> Dimensions { get; }

        // Filters to apply on the variable
        IDictionary<int, IQueryFilter> Filters { get; }

        // Apply an additional filter to the query
        IQueryBuilder Filter(IQueryFilter pred);

        // Construct the query and execute
        IDataframe Execute();
    }

    // Row of a dataframe
    public interface IDataframeRecord : IEnumerable<object>
    {
        // dimension bucket values
        short[] Key { get; }

        // variable value
        float? Value { get; }
    }

    // Dataframe
    public interface IDataframe : IEnumerable<IDataframeRecord>, IDisposable
    {
        // Source variable of which the data belong
        IVariable Variable { get; }

        // Dimensions of the variable
        IReadOnlyList<IDimension> Dimensions { get; }

        // Handle to internal, implementation-specific object
        object Handle { get; }

        // Number of records
        int RowCount { get; }

        // For reading values of a dataframe as if it is an one-dimensional array.
        float? this[int index] { get; }

        // For reading values of a dataframe as if it is a multi-dimensional array.
        float? this[params short[] keys] { get; }

        // For translating categorical keys (i.e. combination of dimension bucket values) to linear offset.
        int Loc(short[] keys);

        // Return a new dataframe with the content of the current dataframe plus elements of source as new row(s).
        IDataframe Append(IDataframe source);

        // Return a new dataframe with the content of the current dataframe plus specified values as a new row.
        IDataframe Append(params object[] values);

        // Return a new dataframe with the content of the current dataframe plus elements of source as new row(s).
        IDataframe Append(params IEnumerable<object>[] source);

        // Initiate a query over the content of the current datafarme
        IQueryBuilder Filter(IQueryFilter pred = null);

        // Serialize the content of the current dataframe into a binary byte array
        byte[] ToBytes();
    }

}
