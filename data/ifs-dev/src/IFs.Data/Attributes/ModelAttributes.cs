using System;

namespace IFs.Data
{
    [Flags]
    public enum ExecutionStage {
        // 0. unknown stage
        None = 0x00,
        // 1. load data series from legacy databases and perform various pre-processing
        Normalize = 0x01,
        // 2. convert country-based data series into region-based dataframes
        Regionalize = 0x02,
        // 3. build shift factors to match formulas to data
        Prepare = 0x04,
        // 4. iterate the time horizon and produce annual dataframes for all variables
        Iterate = 0x08,
        // 5. post-processing such as building base cases
        Finalize = 0x10,
        // stage 1 and 2
        Rebuild = Normalize | Regionalize,
        // stage 3, 4, 5
        Forecast = Prepare | Iterate | Finalize,
        // all stages
        All = Rebuild | Forecast,
    }

    [Flags]
    public enum ProducerScope {
        // 0. unknown scope
        None = 0x00,
        // 1. only invoke for the first year
        FirstYearOnly = 0x01,
        // 2. invoke for all years except the first year
        SecondYearOnward = 0x02,
        // 3. invoke for all years
        FullHorizon = FirstYearOnly | SecondYearOnward
    }

    // Declares dependency on a side-input, typically a country-based series generated during normalization 
    // (1st phase of rebuild base), or an annual dataframe generated during run model
    [System.AttributeUsage(
        System.AttributeTargets.Property | System.AttributeTargets.Field | System.AttributeTargets.Method, 
        Inherited = false, AllowMultiple = true)]
    public sealed class ImportsAssetAttribute : System.Attribute
    {
        readonly string assetName;

        public ImportsAssetAttribute(string assetName)
        {
            this.assetName = assetName;
        }

        public string AssetName => assetName;

        public string Description { get; set; }
    }

    // Declares a handler that normalizes a country-based series with coalescing and regression-based estimation.
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class ExportsAssetAttribute : System.Attribute
    {
        readonly string assetName;

        public ExportsAssetAttribute(string assetName)
        {
            this.assetName = assetName;
        }

        public string AssetName => assetName;

        public string Description { get; set; }
    }

    // Declare a handler that performs rebuild base for a specified variable.
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class RebuildsVariableAttribute : System.Attribute
    {
        readonly string variableName;

        public RebuildsVariableAttribute(string variableName)
        {
            this.variableName = variableName;
        }

        public string VariableName => variableName;
        public Type AggregatorType { get; set; }
        public string Description { get; set; }
    }

    // Declare a handler that performs run model for a specified variable.
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class ForecastsVariableAttribute : System.Attribute
    {
        readonly string variableName;

        public ForecastsVariableAttribute(string variableName)
        {
            this.variableName = variableName;
            this.Scope = ProducerScope.FullHorizon;
        }

        public string VariableName => variableName;

        public ProducerScope Scope { get; set; }

        public string Description { get; set; }
    }

    // Declare a dependency on a variable generated during forecast (2nd phase of run model).
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class ConsumesVariableAttribute : System.Attribute
    {
        readonly string variableName;

        public ConsumesVariableAttribute(string variableName)
        {
            this.variableName = variableName;
        }

        public string VariableName => variableName;
        public string Description { get; set; }
    }

    // Declare a business-logic module for a category of variables.
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class IFsModelAttribute : System.Attribute
    {
        readonly string name;

        public IFsModelAttribute(string name)
        {
            this.name = name;
        }

        public string Name => name;

        public string Description { get; set; }
    }

    // Declare the initializer of a business-logic module for the specified stage.
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class IFsModelInitAttribute : System.Attribute
    {
        readonly ExecutionStage stage;

        public IFsModelInitAttribute(ExecutionStage stage)
        {
            this.stage = stage;
        }

        public ExecutionStage Stage => stage;

        public string Description { get; set; }
    }
}
