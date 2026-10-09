
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IFs.Data
{

    internal partial class RebuildContext: IRebuildContext
    {
        public ExecutionStage Stage => ExecutionStage.Rebuild;

        private ContextMode _mode;

        private bool _DevelopmentMode;

        private readonly short _first_year_history;

        private readonly short _last_year_history;

        private readonly short _first_year_forecast;

        private readonly short _last_year_forecast;

        private readonly short _base_year;

        private readonly String _sMigrationRateTbl;

        private readonly String _sMigrationTbl;

        private readonly short _NCohorts;

        private readonly short _LastCohortFert;

        private readonly short _LastYearMigration;

        private readonly string _SupplementalDataFile;

        private readonly string _SupplementalHistoricDataFile;

        private readonly string _PutFdrv;

        private readonly string _FDRV;

        private readonly string _StartDirec;

        private readonly string _PutDataFileDir;

        private readonly string _DataFileDir;

        private readonly string _DataFileSubDir;

        private readonly string _PutFdrvRun;

        private readonly string _ResFileSubDir;

        private readonly short _NInfraIndexTradComp;

        private readonly short _NInfraIndexICTComp;

        private readonly short _NInfraIndTranComp;

        private readonly short _NInfraIndElecComp;

        private readonly short _NInfraIndWatSanComp;

        private readonly short _NInfraType;

        private readonly short _NSec;

        private readonly short _NSkill;

        private readonly short _NIOMatrices;

        private readonly short _NumbCulReg;

        private readonly short _MaxReg;

        private readonly string _ActiveBase;

        //jrs 2022/11/01
        private readonly short _ConstantDollarsYear;
        //jrs 2023/10/03
        private readonly short _NGender;

        //mti 2023/10/15
        private readonly short _NPriGr;

        private readonly IDictionary<string, int> _regionalization;

        private readonly IDictionary<string, IVariable> _variables;

        private readonly IDictionary<int, IDimension> _dimensions;

        private readonly IDictionary<string, IReadOnlyList<IRegression>> _regressions;

        private readonly IFsHistSeriesContext dbHistSeries;

        private readonly IFsHistSeriesDyadicContext dbHistSeriesDyad;

        private readonly Lazy<IServiceProvider> _components;

        public IServiceProvider Components {
            get => _components.Value;
        }

        public RebuildContext(ContextMode mode = ContextMode.Future) {
            Debug.WriteLine("initializing rebuild context");
            Debug.Indent();
            this._mode = mode;
            Debug.WriteLine("- fetching init parameters from IFsInit");
            using (var db = new IFsInitContext()) {
                //Console.WriteLine("JRS After open IFsInitContext");
                this._DevelopmentMode = db.DevelopmentMode;
                //Console.WriteLine("JRS After open IFsInitContext 2");
                this._first_year_history = db.FirstYearHistory;
                this._last_year_history = db.LastYearHistory;
                this._first_year_forecast = db.FirstYearForecast;
                this._last_year_forecast = db.LastYearForecast;
                this._base_year = this._first_year_forecast;
                this._sMigrationRateTbl = db.MigrationRateTbl;
                this._sMigrationTbl = db.MigrantsTbl;
                this._LastYearMigration = db.LastYearMigration;
                this._NCohorts = db.NCohorts;
                this._LastCohortFert = db.LastCohortFert;
                this._SupplementalDataFile = db.SupplementalDataFile;
                this._SupplementalHistoricDataFile = db.SupplementalHistoricDataFile;
                this._DataFileSubDir = db.DataFileSubDir;
                this._ResFileSubDir = db.ResFileSubDir;
                this._NInfraIndexTradComp = db.NInfraIndexTradComp;
                //jrs 2022/06/23
                if (this._NInfraIndexTradComp > -3)
                {
                    this._NInfraIndexICTComp = db.NInfraIndexICTComp;
                    this._NInfraIndTranComp = db.NInfraIndTranComp;
                    this._NInfraIndElecComp = db.NInfraIndElecComp;
                    this._NInfraIndWatSanComp = db.NInfraIndWatSanComp;
                    this._NInfraType = db.NInfraType;
                }
                this._NSec = db.NSec;
                this._NSkill = db.NSkill;
                this._NIOMatrices = db.NIOMatrices;
                this._NumbCulReg = db.NumbCulReg;
                this._MaxReg = db.MaxReg;
                this._StartDirec = AppDomain.CurrentDomain.BaseDirectory.Replace("Code.Ifs-Translation\\bin\\Debug\\net471\\", ""); //"D:/ifs-vanilla/ifs-vanilla/data/"; //
                //jrs 2019/12/28
                this._StartDirec = _StartDirec.Replace("net471\\", ""); //"D:/ifs-vanilla/ifs-vanilla/data/"; //
                //jrs 2022/01/28
                this._StartDirec = _StartDirec.Replace("Code.Ifs-Translation\\bin\\Debug\\net8\\", "");
                this._StartDirec = _StartDirec.Replace("net8\\", ""); 

                this._FDRV = this._StartDirec;
                this._PutFdrv = this._FDRV;
                this._PutDataFileDir = this._PutFdrv + this._DataFileSubDir;
                this._DataFileDir = this._FDRV + this._DataFileSubDir;
                this._PutFdrvRun = this._FDRV + this._ResFileSubDir;
                this._ActiveBase = db.ActiveBase;

                //jrs 2022/11/01
                this._ConstantDollarsYear = db.ConstantDollarsYear;
                //jrs 2023/10/03
                this._NGender = db.NGender;

                //mti 2023/10/15
                this._NPriGr = db.NPriGr;

            }
            Debug.WriteLine("- fetching regionalization from IFs");
            IDimension d_region;
            using (var db = new IFsContext()) {
                this._regionalization = db.RegionalizationFull.ToDictionary(
                    e => e.CountryName, e => e.RegionId);
                d_region = db.GetRegionDimension();
            }
            Debug.WriteLine("- fetching dimensions from IFs.grp");
            IDimension d_time = getTimeDimension();
            using (var grp = new IFsGrpContext()) {
                this._dimensions = new IDimension[] {
                    d_time,
                    d_region,
                }.Concat(grp.All).ToDictionary(d => d.Id, d => d);
            }
            Debug.WriteLine("- fetching variables from IFsVar");
            using (var db = new IFsVarContext()) {
                this._variables = db.All.ToDictionary(v => v.Name, v => v);
            }
            Debug.WriteLine("- fetching unary regressions from TablFunc");
            this._regressions = new Dictionary<string, IReadOnlyList<IRegression>>();
            using (var db = new TablFuncContext()) {

#if USE_JET
                //jrs 2025/10/31 Don't do anything for JET
#else
                //jrs 2025/07/22 Chat GPT suggested this as a workaround to avoid leaving the connection open
                db.Database.ExecuteSqlRaw("PRAGMA journal_mode=DELETE;");
#endif

                foreach (var grp in (from r in db.All group r by r.Name into g select g))
                {
                    this._regressions.Add(grp.Key, grp.OrderBy(reg => reg.InputName).ToArray());
                }
            }
            Debug.WriteLine("- fetching multivariate regressions from AnalFunc");
            using (var db = new AnalFuncContext()) {
                foreach (var grp in (from r in db.All group r by r.Name into g select g)) {
                    this._regressions.Add(grp.Key, grp.OrderBy(reg => reg.InputName).ToArray());
                }
            }
            // context for querying historical data
            Debug.WriteLine("- instantiating IFsHistSeries");
            this.dbHistSeries = new IFsHistSeriesContext();

            // context for querying historical data
            Debug.WriteLine("- instantiating IFsHistSeriesDyad");
            this.dbHistSeriesDyad = new IFsHistSeriesDyadicContext();

            // lazy-initialize processor repository
            this._components = new Lazy<IServiceProvider>(() => {
                Debug.WriteLine("- instantiating components");
                var comps = new ServiceCollection();
                comps.AddSingleton(DefaultAggregator);
                foreach (var aggType in this._aggregators.Values) {
                    if (!comps.Contains(ServiceDescriptor.Singleton(aggType))) {
                        comps.AddSingleton(aggType);
                    }
                }
                return comps.BuildServiceProvider();
            });
            Debug.Unindent();
            Debug.WriteLine("initialized rebuild context");
        }

        // IContext

        public bool DevelopmentMode
            => this._DevelopmentMode;

        public short FirstYear
            => this.HistoricMode ? this._first_year_history : this._first_year_forecast;

        //jrs 2023/01/24
        public short FirstYearHistory
            => this._first_year_history;

        public short LastYear
            => this.HistoricMode ? this._last_year_history : this._last_year_forecast;

        public short BaseYear
            => this._base_year;

        public string MigrationRateTbl
            => this._sMigrationRateTbl;

        public string MigrantsTbl
            => this._sMigrationTbl;

        public string ActiveBase => this._ActiveBase;

        public short NCohorts
            => this._NCohorts;

        public short LastCohortFert
            => this._LastCohortFert;

        public short LastYearMigration
            => this._LastYearMigration;

        public string SupplementalDataFile
            => this._SupplementalDataFile;

        public string SupplementalHistoricDataFile
            => this._SupplementalHistoricDataFile;

        public string PutDataFileDir
            => this._PutDataFileDir;

        public string DataFileDir
            => this._DataFileDir;

        public string ResFileSubDir
            => this._ResFileSubDir;

        public string PutFdrvRun
            => this._PutFdrvRun;

        public short NInfraIndexTradComp
            => this._NInfraIndexTradComp;

        public short NInfraIndexICTComp
            => this._NInfraIndexICTComp;

        public short NInfraIndTranComp
            => this._NInfraIndTranComp;

        public short NInfraIndElecComp
            => this._NInfraIndElecComp;

        public short NInfraIndWatSanComp
            => this._NInfraIndWatSanComp;

        public short NInfraType
            => this._NInfraType;

        public short NSec
            => this._NSec;

        public short NSkill
            => this._NSkill;

        public short NIOMatrices
            => this._NIOMatrices;

        public short NumbCulReg
            => this._NumbCulReg;

        public short MaxReg
            => this._MaxReg;

        //jrs 2022/11/01
        public short ConstantDollarsYear
            => this._ConstantDollarsYear;

        //jrs 2023/10/03
        public short NGender
            => this._NGender;

        //mti 2023/10/15
        public short NPriGr
            => this._NPriGr;

        public ICollection<string> VariableNames
            => this._variables.Keys;

        public IEnumerable<IVariable> Variables
            => this._variables.Values;

        public IVariable GetVariable(string name) {            
            return this._variables[name];
        }

        public ICollection<int> DimensionIds
            => this._dimensions.Keys;

        public IEnumerable<IDimension> Dimensions
            => this._dimensions.Values;

        public IDimension GetDimension(int index) {
            return this._dimensions[index];
        }

        public IEnumerable<IRegression> Regressions {
            get {
                foreach (var grp in this._regressions.Values) {
                    foreach (var reg in grp) {
                        yield return reg;
                    }
                }
            }
        }

        public ICollection<string> RegressionNames
            => this._regressions.Keys;

        public IEnumerable<IRegression> GetRegression(string name) {
            if (this._regressions.ContainsKey(name))
            {
                return this._regressions[name];
            }
            else //jrs 2018/11/12
            {
                return new IRegression[0];
            }
        }

        //jrs 2019/07/17
        public IEnumerable<IRegressionCoefficients> GetRegressionCoefficients(string name)
        {
            if (this._regressions.ContainsKey(name))
            {
                //Not implemented
                return new IRegressionCoefficients[0];
            }
            else 
            {
                return new IRegressionCoefficients[0];
            }
        }

        //jrs 2019/07/19
        public IEnumerable<IPoint<double>> GetRegressionPoints(string name)
        {
            if (this._regressions.ContainsKey(name))
            {
                //not implemented
                return new IPoint<double>[0];
            }
            else
            {
                return new IPoint<double>[0];
            }
        }

        public IQueryBuilder Query(string variableName) {
            // query variable dimensions
            IVariable variable = this.GetVariable(variableName);
            IReadOnlyList<IDimension> dimensions = (from d in variable.Dimensions select this.GetDimension(d)).ToList();
            // build a cache of historical series records
            IDictionary<short[], float?> cache = new Dictionary<short[], float?>(ShortIntKeyComparer.DEFAULT_INSTANCE);
            foreach (IEnumerable<short> key in dimensions.Skip(2).MeshGrid()) {
                foreach (var record in this.GetHistSeries(variable.Name, key.ToArray())) {
                    cache.Add(record.Key, record.Value);
                }
            }
            // create a new dataframe with single-year structure and NULL-filled values
            Dataframe df = Dataframe.Allocate(variable, dimensions, this.FirstYear, this.FirstYear);
            // update values with cached historical values
            int iRow = 0;
            foreach (var key in dimensions.Skip(1).MeshGrid()) {
                short[] recordKey = new short[] { this.FirstYear }.Concat(key).ToArray();
                float? recordValue = null;
                if (cache.TryGetValue(recordKey, out recordValue)) {
                    df[iRow] = recordValue;
                }
                iRow += 1;
            }
            return new DataframeQueryBuilder(df);
        }

        // IRebuildFacade

        public bool HistoricMode
            => this.Mode == ContextMode.Historic;

        public IDictionary<string, int> Regionalization
            => this._regionalization;

        public IEnumerable<short> GetYears(string seriesName) {
            return this.dbHistSeries.GetYears(seriesName);
        }

        public IEnumerable<short> GetYearsDyad(string seriesName)
        {
            return this.dbHistSeriesDyad.GetYearsDyad(seriesName);
        }

        public IDictionary<string, float?> GetSlice(string seriesName, string fieldName) {
            return this.dbHistSeries.SliceSeries(seriesName, fieldName).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public IDictionary<string, float?> GetSlice(string seriesName, short year, CoalescePolicy coalesce = CoalescePolicy.Null) {
            var slice = this.dbHistSeries.SliceSeries(seriesName, year.ToString());
            switch (coalesce) {
                case CoalescePolicy.Zero:
                    return (from s in slice
                        select new KeyValuePair<string, float?>(s.Key, s.Value ?? (float?) 0.0f)
                    ).ToDictionary(d => d.Key, d => d.Value);
                case CoalescePolicy.Earliest:
                    return (from s in slice
                        join c in this.dbHistSeries.SliceSeries(seriesName, "Earliest") on s.Key equals c.Key
                        select new KeyValuePair<string, float?>(s.Key, s.Value ?? c.Value)
                    ).ToDictionary(d => d.Key, d => d.Value);
                case CoalescePolicy.MostRecent:
                    return (from s in slice
                        join c in this.dbHistSeries.SliceSeries(seriesName, "MostRecent") on s.Key equals c.Key
                        select new KeyValuePair<string, float?>(s.Key, s.Value ?? c.Value)
                    ).ToDictionary(kv => kv.Key, kv => kv.Value);
                case CoalescePolicy.Null:
                default:
                    return slice.ToDictionary(kv => kv.Key, kv => kv.Value);
            }
        }

        public IDictionary<string, float?> GetSliceDyad(string seriesName, string fieldName)
        {
            return this.dbHistSeriesDyad.SliceSeries(seriesName, fieldName).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public IDictionary<string, float?> GetSliceDyad(string seriesName, short year, CoalescePolicy coalesce = CoalescePolicy.Null)
        {
            var slice = this.dbHistSeriesDyad.SliceSeries(seriesName, year.ToString());
            switch (coalesce)
            {
                case CoalescePolicy.Zero:
                    return (from s in slice
                            select new KeyValuePair<string, float?>(s.Key, s.Value ?? (float?)0.0f)
                    ).ToDictionary(d => d.Key, d => d.Value);
                case CoalescePolicy.Earliest:
                    return (from s in slice
                            join c in this.dbHistSeriesDyad.SliceSeries(seriesName, "Earliest") on s.Key equals c.Key
                            select new KeyValuePair<string, float?>(s.Key, s.Value ?? c.Value)
                    ).ToDictionary(d => d.Key, d => d.Value);
                case CoalescePolicy.MostRecent:
                    return (from s in slice
                            join c in this.dbHistSeriesDyad.SliceSeries(seriesName, "MostRecent") on s.Key equals c.Key
                            select new KeyValuePair<string, float?>(s.Key, s.Value ?? c.Value)
                    ).ToDictionary(kv => kv.Key, kv => kv.Value);
                case CoalescePolicy.Null:
                default:
                    return slice.ToDictionary(kv => kv.Key, kv => kv.Value);
            }
        }

        public IDictionary<string, float?[]> GetDice(string seriesName, params short[] years) {
            return this.dbHistSeries.DiceSeries(seriesName, years.Select(y => y.ToString()).ToArray()).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public IDictionary<string, float?[]> GetDiceDyad(string seriesName, params short[] years)
        {
            return this.dbHistSeriesDyad.DiceSeries(seriesName, years.Select(y => y.ToString()).ToArray()).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        // IRebuildContext

        public ContextMode Mode {
            get => this._mode;
            set {
                if (this._mode != value) {
                    Debug.WriteLine($"- setting context mode: {value}");
                    this._mode = value;
                    // update time dimension after mode change
                    this._dimensions[0] = getTimeDimension();
                }
            }
        }

        private IDimension getTimeDimension() {
            var dim = new DimensionBind();
            dim.Id = DataExtensions.DIM_TIME;
            dim.Name = DataExtensions.DIM_TIME_NAME;
            dim.Buckets = new SortedList<short, string>();
            for (short year = this.FirstYear; year <= this.LastYear; year++) {
                dim.Buckets.Add(year, Convert.ToString(year));
            }
            return dim;
        }

        public IDictionary<short[], float?> GetHistSeries(string variableName, params string[] selectedBuckets) {
            var vDims = this.GetVariable(variableName).Dimensions;
            if (vDims.Count != selectedBuckets.Length + 2) {
                throw new ArgumentOutOfRangeException("selectedBuckets", "Selected buckets are incompatible with variable structure.");
            }
            if ((vDims.Count < 1) || (vDims[0] != DataExtensions.DIM_TIME)) {
                throw new NotSupportedException("The first dimension is not time.");
            }
            if ((vDims.Count < 2) || (vDims[1] != DataExtensions.DIM_REGION)) {
                throw new NotSupportedException("The second dimension is not region.");
            }
            var selectedSeqs = new List<short>();
            string seriesName = variableName;
            if (selectedBuckets.Length > 0) {
                int dimSeq = 2;
                foreach (string bucketName in selectedBuckets) {
                    seriesName += $"_{bucketName}";
                    var dim = this.GetDimension(vDims[dimSeq++]);
                    var InvertedBuckets = dim.Buckets.ToDictionary(kv => kv.Value, kv => kv.Key);
                    if (!InvertedBuckets.TryGetValue(bucketName, out short bucketSeq)) {
                        throw new ArgumentException($"Invalid bucket name {bucketName}.");
                    }
                    selectedSeqs.Add(bucketSeq);
                }
            }
            return regionalizeSeries(variableName, seriesName, selectedSeqs.ToArray());
        }

        public IDictionary<short[], float?> GetHistSeries(string variableName, short[] selectedBuckets) {
            var vDims = this.GetVariable(variableName).Dimensions;
            if (vDims.Count != selectedBuckets.Length + 2) {
                throw new ArgumentOutOfRangeException("selectedBuckets", "Selected buckets are incompatible with variable structure.");
            }
            if ((vDims.Count < 1) || (vDims[0] != DataExtensions.DIM_TIME)) {
                throw new NotSupportedException("The first dimension is not time.");
            }
            if ((vDims.Count < 2) || (vDims[1] != DataExtensions.DIM_REGION)) {
                throw new NotSupportedException("The second dimension is not region.");
            }
            string seriesName = variableName;
            if (selectedBuckets.Length > 0) {
                int dimSeq = 2;
                foreach (short bucketSeq in selectedBuckets) {
                    var dim = this.GetDimension(vDims[dimSeq++]);
                    if (!dim.Buckets.TryGetValue(bucketSeq, out string bucketName)) {
                        throw new ArgumentException($"Invalid bucket sequence {bucketSeq}.");
                    }
                    seriesName += $"_{bucketName}";
                }
            }
            return regionalizeSeries(variableName, seriesName, selectedBuckets);
        }

        private IDictionary<short[], float?> regionalizeSeries(string variableName, string seriesName, short[] selectedSeqs) {
            return this.GetAggregator(variableName).Regionalize(seriesName, this).ToDictionary(
                e => new short[] { this.FirstYear, (short) e.Key }.Concat(selectedSeqs).ToArray(),
                e => e.Value, 
                ShortIntKeyComparer.DEFAULT_INSTANCE);
        }

        public void Dispose() {
            Debug.WriteLine("disposing rebuild context");
            // NOP
            Debug.WriteLine("disposed rebuild context");
        }

    }

}
