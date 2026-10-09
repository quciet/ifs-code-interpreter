using System;
using System.Collections.Generic;
using System.Reflection;
using IFs.Data;

namespace IFs.Data
{

    public delegate IEnumerable<IDataframe> VariableProducer();

    // generator of execution plans for producing variables of a given year
    public interface IModelPlanner<C> where C: IContext {
        IEnumerable<VariableProducer> Plan(short year);
    }

    // generator of model planners for the specified stages
    public interface IModelFactory {
        IModelFactory Load(Type modelType);
        IModelPlanner<IRebuildContext> GetPlanner(IRebuildContext context);
        IModelPlanner<IForecastContext> GetPlanner(IForecastContext context);
    }

    public interface IForecastService: IModelFactory, IDisposable
    {
        // rebuild base for the specified variable
        IDataframe Rebuild(string variableName, IRebuildContext context);

        // run model for the specified variable
        IDataframe Forecast(string variableName, short year, IForecastContext context);

        // rebild base for all variables of the specified module
        IEnumerable<IDataframe> RebuildModule(String moduleName, IRebuildContext context);

        // run model(s) for all variables of the specified model
        IEnumerable<IDataframe> ForecastModule(String moduleName, short year, IForecastContext context);

        // rebuild base for all variables
        IEnumerable<IDataframe> RebuildAll(IRebuildContext context);

        // run model(s) for all variables for the specified year
        IEnumerable<IDataframe> ForecastAll(short year, IForecastContext context);

        //jrs 2019/06/13
        void ForecastAllStatic(IForecastContext context, Dictionary<String, Dataframe> IFsResults, Dictionary<String, IDataframe> IFsResults2);

        // rebuild common for all variables
        void RebuildCommon(IRebuildContext context);

    }

}
