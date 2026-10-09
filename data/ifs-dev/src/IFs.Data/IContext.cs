using System;
using System.Collections.Generic;
using System.Dynamic;

namespace IFs.Data
{

    // initialization parameter abstraction
    public interface IParameter
    {
        string Name { get; }

        string Value { get; }
    }

    // dimension metadata abstraction
    public interface IDimension
    {
        // Unique identifier of the dimension
        int Id { get; }

        // Human-readable name of the dimension (optional)
        string Name { get; }

        // Maps categorical values of the dimension to their literal names
        IReadOnlyDictionary<short, string> Buckets { get; }
    }

    // variable metadata abstraction
    public interface IVariable
    {
        // Unique identifier of the variable
        string Name { get; }

        // Human-readable name of the variable (optional)
        string DisplayName { get; }

        // IFs-defined type of the variable
        int Type { get; }

        //jrs 2025/05/23
        // Identifier to mark bilateral variables not to be load/saved when bilateral mode is off
        int Bilat { get; } 

        //jrs 2019/08/18
        // IFs-defined type of the variable
        double? Min { get; }
        double? Max { get; }

        // Unique identifiers of the dimensions of the variable
        IReadOnlyList<int> Dimensions { get; }

        //2019/09/30
        bool? CommonTarget { get; }
        string Currency { get; }
        string Aggregation { get; }
    }

    // 2-dimensional point
    public interface IPoint<T>
    {
        T X { get; }
        T Y { get; }
    }

    // regression metadata abstraction
    public interface IRegression
    {
        // Unique identifier of the estimator
        string Name { get; }

        // Description of independent variable (input)
        string InputName { get; }

        // Description of dependent variable (output)
        string OutputName { get; }

        //jrs 2019/07/23
        short Seq { get; }

        //jrs 2019/08/07
        double? StdError { get; }

        // Coefficients for computed (linear or non-linear) regression
        IReadOnlyDictionary<string, double> Coefficients { get; }

        // Points for piecewise interpolation
        IReadOnlyCollection<IPoint<double>> Points { get; }
    }

    // jrs 2019/07/17 regression coefficients metadata abstraction
    public interface IRegressionCoefficients
    {
        // Regression Name
        string RegressionName { get; }

        // Id for Independent variable
        short RegressionSeq { get; }

        // Name of the coefficient
        string Name { get; }

        // Value of the coefficient
        double Value { get; set; }
    }

    public enum ContextMode
    {
        Historic,
        Future,
    }

    public enum CoalescePolicy
    {
        // preserve NULL as-is
        Null,
        // replace NULL with 0.0f
        Zero,
        // replace NULL with most recent value
        MostRecent,
        // replace NULL with earliest value
        Earliest,
    }

    public interface IExchange
    {
        T Fetch<T>(string key);

        void Store(string key, object value);

        void Evict(string key);
    }

    public interface IContext
    {
        //jrs 2019/09/27 moved here to make it available both for Forecast and Rebuild context
        // historic mode flag
        bool HistoricMode { get; }

        // execution stage of the context
        ExecutionStage Stage { get; }

        // Lower bound of the time horizon.
        short FirstYear { get; }

        //jrs 2023/01/24
        short FirstYearHistory { get; }
        //jrs 2023/01/30
        short ConstantDollarsYear { get; }
        //jrs 2023/10/03
        short NGender { get; }

        //mti 2023/10/15 - primary grades
        short NPriGr { get; }

        // Upper bound of the time horizon.
        short LastYear { get; }

        // Lower bound of the time horizon.
        short BaseYear { get; }

        // Name of the base case file
        string ActiveBase { get; }

        // Data File Directory
        string DataFileDir { get; }

        // Get the metadata of all variables.
        IEnumerable<IVariable> Variables { get; }

        // Get the full set of variable names.
        ICollection<string> VariableNames { get; }

        // Get variable metadata by variable name.
        IVariable GetVariable(string name);

        // Get the metadata of all dimensions.
        IEnumerable<IDimension> Dimensions { get; }

        // Get the full set of dimension IDs.
        ICollection<int> DimensionIds { get; }

        // Get dimension metadata by dimension ID.
        IDimension GetDimension(int index);

        // Get the metadata of all regressions.
        IEnumerable<IRegression> Regressions { get; }

        // Get regression by name.
        IEnumerable<IRegression> GetRegression(string name);

        // jrs 2019/07/17 Get regression coefficients by name.
        IEnumerable<IRegressionCoefficients> GetRegressionCoefficients(string name);

        // jrs 2019/07/17 Get regression Points by name.
        IEnumerable<IPoint<double>> GetRegressionPoints(string name);

        // Get the full set of regression names.
        ICollection<string> RegressionNames { get; }

        // Create a query builder for executing queries against the context.
        IQueryBuilder Query(string variableName);
    }

    public delegate float? SeriesEstimator(string arg);

    // facade for accessing raw context data during regionalization
    public interface IRebuildFacade: IContext, IExchange
    {
        // historic mode flag
        //jrs 2019/09/27 bool HistoricMode { get; }

        // development mode flag
        bool DevelopmentMode { get; }

        // get a dictionary of <CountryName, RegionId> pairs.
        IDictionary<string, int> Regionalization { get; }

        // get the list of available years of the series.
        IEnumerable<short> GetYears(string seriesName);

        // get the list of available years of the series.
        IEnumerable<short> GetYearsDyad(string seriesName);

        // get a slice of the series (dict of <CountryName, Value>) for the specified column.
        IDictionary<string, float?> GetSlice(string seriesName, string columnName);

        // get a slice of the series (dict of <CountryName, Value>) for the specified year.
        IDictionary<string, float?> GetSlice(string seriesName, short year, CoalescePolicy coalesce = CoalescePolicy.Null);

        // get a slice of the series (dict of <CountryName, Value>) for the specified column.
        IDictionary<string, float?> GetSliceDyad(string seriesName, string columnName);

        // get a slice of the series (dict of <CountryName, Value>) for the specified year.
        IDictionary<string, float?> GetSliceDyad(string seriesName, short year, CoalescePolicy coalesce = CoalescePolicy.Null);

        // get a dice of the series (dict of <Country, Value[]>) for all specified years.
        IDictionary<string, float?[]> GetDice(string seriesName, params short[] years);

        // get a dice of the series (dict of <Country, Value[]>) for all specified years.
        IDictionary<string, float?[]> GetDiceDyad(string seriesName, params short[] years);

        // get the estimator registered with the series or variable
        SeriesEstimator GetEstimator(string seriesOrVariableName);

        //Migration Rate table
        string MigrationRateTbl { get; }

        //Migrants table
        string MigrantsTbl { get; }

        //NCohorts
        short NCohorts { get; }

        //LastCohortFert
        short LastCohortFert { get; }

        //LastYearMigration
        short LastYearMigration { get; }

        //SupplementalDataFile
        string SupplementalDataFile { get; }

        //SupplementalHistoricDataFile
        string SupplementalHistoricDataFile { get; }

        //PutDataFileDir
        string PutDataFileDir { get; }

        //jrs 2019/09/27 string DataFileDir { get; }

        string PutFdrvRun { get; }

        short NInfraIndexTradComp { get; }

        short NInfraIndexICTComp { get; }

        short NInfraIndTranComp { get; }

        short NInfraIndElecComp { get; }

        short NInfraIndWatSanComp { get; }

        short NInfraType { get; }

        short NSec { get; }

        short NSkill { get; }

        short NIOMatrices { get; }

        short NumbCulReg { get;  }

        short MaxReg { get;  }

        //jrs 2022/11/01
        short ConstantDollarsYear { get; }
        //jrs 2023/10/03
        short NGender { get; }

        //mti 2023/10/15
        short NPriGr { get; }

    }

    public interface IRebuildContext: IRebuildFacade, IDisposable
    {
        // execution mode (historic / forecast)
        ContextMode Mode { get; set; }

        // default coalescing policy
        CoalescePolicy DefaultPolicy { get; set; }

        // for accessing the internals of the rebuild context
        IServiceProvider Components { get; }

        // default regression registered for all variables
        SeriesEstimator DefaultEstimator { get; set; }

        // associate a regression to be used as the estimator of the specified series or variable
        void PutEstimator(string seriesOrVariableName, SeriesEstimator estimator);

        // default regionalization strategy for all variables
        Type DefaultAggregator { get; set; }

        // get the regionalization strategy registered for the variable
        IAggregator GetAggregator(string variableName);

        // register a regionalization strategy for the variable
        void PutAggregator(string variableName, Type aggregatorType);

        // Slice the (regionId, value) sub-cube of the variable with timeId = `FirstYear`,
        // and the selected members (bucket seq's) of other dimensions as specified.
        IDictionary<short[], float?> GetHistSeries(string variableName, short[] selectedBuckets);

        // Slice the (regionId, value) sub-cube of the variable with timeId = `FirstYear`,
        // and the selected members (bucket names) of other dimensions as specified.
        IDictionary<short[], float?> GetHistSeries(string variableName, params string[] selectedBuckets);
    }

    public interface IForecastFacade: IContext, IExchange
    {
        // historic mode flag
        //jrs 2019/09/27 bool HistoricMode { get; }

        //NCohorts
        short NCohorts { get; }

        //LastCohortFert
        short LastCohortFert { get; }

        //NSec
        short NSec { get; }

        bool ShowMsg { get; }

        short NAges { get; }

        string PutFdrvRun { get; }

        //short NGender { get; }

        //short NPriGr { get; }

        short NIOMatrices { get; }

        short NGovExp { get; }

        short NSkill { get;  }

        short NHHTypes { get; }

        short NWBTar { get; }

        short WVSCohorts { get; }

        bool DevelopmentMode { get; }

        //jrs 2024/01/24
        bool MultiThreading { get; }

        short FirstYearForecast { get; }

        //short FirstYearHistory { get; }

        short LastYearForecast { get; }

        short LastYearHistory { get; }

        //jrs 2023/01/30
        //short ConstantDollarsYear { get; }

        short NInfraIndexICTComp { get; }

        short NInfraType { get; }

        short NInfraIndexComp { get; }

        short NInfraIndWatSanComp { get; }

        short NInfraIndElecComp { get; }

        short NInfraIndTranComp { get; }

        short NInfraIndexTradComp { get; }

        short NLocation { get; }

        short NET { get; }

        short NEP { get; }

        short NER { get; }

        //jrs 2019/07/26
        // get a slice of the series (dict of <CountryName, Value>) for the specified column.
        IDictionary<string, float?> GetSlice(string seriesName, string columnName);

        // get a slice of the series (dict of <CountryName, Value>) for the specified year.
        IDictionary<string, float?> GetSlice(string seriesName, short year, CoalescePolicy coalesce = CoalescePolicy.Null);

        // get a dictionary of <CountryName, RegionId> pairs.
        IDictionary<string, int> Regionalization { get; }

        //jrs 2020/07/14
        // get the list of available years of the series.
        IEnumerable<short> GetYears(string seriesName);

        // get a dice of the series (dict of <Country, Value[]>) for all specified years.
        IDictionary<string, float?[]> GetDice(string seriesName, params short[] years);

        //jrs 2021/12/15
        // get the list of available years of the series.
        IEnumerable<short> GetYearsDyad(string seriesName);

        // get a dice of the series (dict of <Country, Value[]>) for all specified years.
        IDictionary<string, float?[]> GetDiceDyad(string seriesName, params short[] years);

    }

    public interface IForecastContext: IForecastFacade, IDisposable
    {
        //jrs 2019/06/19
        // execution mode (historic / forecast)
        ContextMode Mode { get; set; }

        //jrs 2019/09/27
        string ScenarioFileDir { get; }
    }

    // aggregation (regionalization) strategy for historical series
    public interface IAggregator
    {
        // return an unregionalized dictionary of <CountryName, Value> pairs.
        IDictionary<string, float?> Normalize(string seriesName, IRebuildFacade cntx);

        // return a regionalized dictionary of <RegionId, Value> pairs.
        IEnumerable<KeyValuePair<int, float?>> Regionalize(string seriesName, IRebuildFacade cntx);
    }

    // preparation strategy for variables at the beginning of forecast
    public interface IPreparator
    {
    }

    public interface IContextService: IDisposable
    {
        // Initialize a context for the Rebuild (rebuild base) execution phase.
        IRebuildContext ForRebuild(ContextMode mode = ContextMode.Future);

        //jrs 2020/04/02 added SessionId
        // Initialize a context for the Forecast (run models) execution phase. A properly initialized `source`
        // context is required as the origin of metadata such as variables, dimensions, regressions, etc.
        IForecastContext ForForecast(IContext source, short Horizon, short SessionId, bool ShowMsg, ContextMode mode = ContextMode.Future); //jrs 2019/06/19 added ContextMode mode = ContextMode.Future //jrs 2019/06/27 added Horizon
    }

}
