using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace IFs.Data
{

    public abstract partial class ForecastServiceBase: IForecastService
    {
        internal readonly IDictionary<string, ModelInfo> Models = new Dictionary<string, ModelInfo>();

        public IModelFactory Load(Type modelType) {
            var attr = FindAttributes<IFsModelAttribute>(modelType).SingleOrDefault();
            if (attr == default(IFsModelAttribute)) {
                throw new ArgumentException($"{modelType.FullName} is not an IFs model.");
            }
            Debug.WriteLine($"analyzing {modelType.FullName} ({attr.Name})");
            Debug.Indent();
            ModelInfo m = AnalyzeModel(modelType, attr);
            this.Models.Add(m.Name, m);
            Debug.Unindent();
            return this;
        }

        public virtual IDataframe Rebuild(string variableName, IRebuildContext context) {
            // TODO
            return Dataframe.Create(variableName, context, context.FirstYear, 0.0f);
        }

        public virtual IDataframe Forecast(string variableName, short year, IForecastContext context) {
            // TODO
            return Dataframe.Create(variableName, context, year, 0.0f);
        }

        public abstract IEnumerable<IDataframe> RebuildModule(string moduleName, IRebuildContext context);

        public abstract IEnumerable<IDataframe> ForecastModule(string moduleName, short year, IForecastContext context);

        public abstract IEnumerable<IDataframe> RebuildAll(IRebuildContext context);

        public abstract IEnumerable<IDataframe> ForecastAll(short year, IForecastContext context);

        //jrs 2019/06/13
        public abstract void ForecastAllStatic(IForecastContext context, Dictionary<String, Dataframe> IFsResults, Dictionary<String, IDataframe> IFsResults2);

        //jrs 2019/06/21
        public abstract void RebuildCommon(IRebuildContext context);

        public virtual void Dispose() {
            // NOP
        }

    }

}