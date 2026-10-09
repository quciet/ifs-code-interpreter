using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace IFs.Data
{

    public partial class ContextService: IContextService, IDisposable {

        public static readonly IReadOnlyDictionary<CoalescePolicy, Type> NOP = new Dictionary<CoalescePolicy, Type>() {
            { CoalescePolicy.Null, typeof(NullNopAggregator) },
            { CoalescePolicy.Zero, typeof(ZeroNopAggregator) },
            { CoalescePolicy.Earliest, typeof(EarliestNopAggregator) },
            { CoalescePolicy.MostRecent, typeof(MostRecentNopAggregator) },
        };

        public static readonly IReadOnlyDictionary<CoalescePolicy, Type> SUM = new Dictionary<CoalescePolicy, Type>() {
            { CoalescePolicy.Null, typeof(NullSumAggregator) },
            { CoalescePolicy.Zero, typeof(ZeroSumAggregator) },
            { CoalescePolicy.Earliest, typeof(MostRecentSumAggregator) },
            { CoalescePolicy.MostRecent, typeof(EarliestSumAggregator) },
        };

        public static readonly IReadOnlyDictionary<CoalescePolicy, Type> AVG = new Dictionary<CoalescePolicy, Type>() {
            { CoalescePolicy.Null, typeof(NullAvgAggregator) },
            { CoalescePolicy.Zero, typeof(ZeroAvgAggregator) },
            { CoalescePolicy.Earliest, typeof(MostRecentAvgAggregator) },
            { CoalescePolicy.MostRecent, typeof(EarliestAvgAggregator) },
        };

        public static readonly SeriesEstimator NULL_ESTIMATOR = (s) => null;

        public IRebuildContext ForRebuild(ContextMode mode) {
            // TODO propagate global defaults to the context
            return new RebuildContext(mode);
        }

        //jrs 2020/04/02
        //jrs 2019/06/27 added Horizon
        //jrs 2019/06/19 added mode
        public IForecastContext ForForecast(IContext working, short Horizon, short Session, bool ShowMsg, ContextMode mode) {
            // TODO propagate global defaults to the context
            return new ForecastContext(working, Horizon, Session, ShowMsg, mode);
        }

        public virtual void Dispose() {
            // NOP
        }

    }

}
