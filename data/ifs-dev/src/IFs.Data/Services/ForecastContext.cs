
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IFs.Data
{

    internal partial class ForecastContext: IForecastContext
    {
        public ExecutionStage Stage => ExecutionStage.Forecast;

        private ContextMode _mode;

        private readonly short _SessionId;

        private readonly short _NCohorts;

        private readonly short _NSec;

        private readonly short _LastCohortFert;

        private readonly IContext _working;

        private readonly short _LastYear;

        private readonly bool _ShowMsg;

        private readonly short _NAges;

        private readonly short _NGender;

        private readonly short _NPriGr;

        private readonly short _NIOMatrices;

        private readonly short _NGovExp;

        private readonly short _NSkill;

        private readonly short _NHHTypes;

        private readonly short _NWBTar;

        private readonly string _StartDirec;

        private readonly string _FDRV;

        private readonly string _ResFileSubDir;

        private readonly string _PutFdrvRun;

        private readonly bool _DevelopmentMode;

        //jrs 2024/01/24
        private readonly bool _MultiThreading;

        private readonly short _FirstYearForecast;

        private readonly short _FirstYearHistory;

        private readonly short _LastYearForecast;

        private readonly short _LastYearHistory;
        //jrs 2023/01/30
        private readonly short _ConstantDollarYears;

        private readonly short _WVSCohorts;

        private readonly short _NInfraIndexICTComp;

        private readonly short _NInfraType;

        //ttt 2019/08/12
        private readonly short _NInfraIndexTradComp;

        private readonly short _NInfraIndTranComp;

        private readonly short _NInfraIndElecComp;

        private readonly short _NInfraIndWatSanComp;

        private readonly short _NInfraIndexComp;

        private readonly short _NLocation;

        private readonly short _NET;

        private readonly short _NEP;

        private readonly short _NER;

        private readonly string _ActiveBase;

        private readonly string _ScenarioFileDir;

        private readonly string _DataFileDir;

        private readonly IFsHistSeriesContext dbHistSeries;

        //jrs 2021/12/15
        private readonly IFsHistSeriesDyadicContext dbHistSeriesDyad;

        private readonly IDictionary<string, int> _regionalization;

        //jrs 2020/04/02 added SessionId
        //jrs 2019/06/27 added Horizon
        public ForecastContext(IContext working, short Horizon, short SessionId, bool ShowMs, ContextMode mode = ContextMode.Future) {
            Debug.WriteLine("initializing forecast context");
            try
            {
                Debug.Indent();
                this._working = working;
                this._mode = mode;
                this._LastYear = Horizon; //jrs 2019/06/27
                this._ShowMsg = ShowMs;
                this._SessionId = SessionId; //jrs 2020/04/02

                Debug.WriteLine("- fetching init parameters from IFsInit");
                using (var db = new IFsInitContext())
                {
                    this._NCohorts = db.NCohorts;
                    this._LastCohortFert = db.LastCohortFert;
                    this._NSec = db.NSec;
                    this._NAges = db.NAges;
                    this._NGender = db.NGender;
                    this._NPriGr = db.NPriGr;
                    this._NIOMatrices = db.NIOMatrices;
                    this._NGovExp = db.NGovExp;
                    this._NSkill = db.NSkill;
                    this._NHHTypes = db.NHHTypes;
                    this._NWBTar = db.NWBTar;
                    this._ResFileSubDir = db.ResFileSubDir;
                    this._StartDirec = AppDomain.CurrentDomain.BaseDirectory.Replace("Code.Ifs-Translation\\bin\\Debug\\net471\\", ""); //"D:/ifs-vanilla/ifs-vanilla/data/"; //
                                                                                                                                        //jrs 2019/12/28
                    this._StartDirec = _StartDirec.Replace("net471\\", ""); //"D:/ifs-vanilla/ifs-vanilla/data/"; //
                                                                            //jrs 2022/01/31
                    this._StartDirec = _StartDirec.Replace("Code.Ifs-Translation\\bin\\Debug\\net8\\", "");
                    this._StartDirec = _StartDirec.Replace("net8\\", "");

                    this._FDRV = this._StartDirec;

                    this._DevelopmentMode = db.DevelopmentMode;
                    this._FirstYearForecast = db.FirstYearForecast;
                    this._FirstYearHistory = db.FirstYearHistory;
                    this._LastYearForecast = db.LastYearForecast;
                    this._LastYearHistory = db.LastYearHistory;
                    //jrs 2024/01/24
                    this._MultiThreading = db.MultiThreading;
                    //jrs 2023/01/30
                    this._ConstantDollarYears = db.ConstantDollarsYear;
                    if (HistoricMode && _LastYear > _LastYearHistory) _LastYear = LastYearHistory;
                    if (!HistoricMode && _LastYear > _LastYearForecast) _LastYear = LastYearForecast;
                    this._WVSCohorts = db.WVSCohorts;
                    this._NInfraIndexICTComp = db.NInfraIndexICTComp;
                    this._NInfraType = db.NInfraType;
                    this._ActiveBase = db.ActiveBase;
                    this._ScenarioFileDir = this._FDRV + db.ScenarioFileSubDir;
                    this._DataFileDir = this._FDRV + db.DataFileSubDir;
                    //ttt
                    this._NInfraIndTranComp = db.NInfraIndTranComp;
                    this._NInfraIndElecComp = db.NInfraIndElecComp;
                    this._NInfraIndexTradComp = db.NInfraIndexTradComp;
                    this._NInfraIndWatSanComp = db.NInfraIndWatSanComp;
                    this._NInfraIndexComp = db.NInfraIndexComp;
                    this._NLocation = db.NLocation;
                    this._NET = db.NET;
                    this._NER = db.NER;
                    this._NEP = db.NEP;
                    //jrs 2020/04/02
                    if (SessionId > -1)
                    {
                        _ResFileSubDir = _ResFileSubDir + "Session";
                        _ScenarioFileDir = _ScenarioFileDir + "Session";
                        if (SessionId < 10)
                        {
                            _ResFileSubDir = _ResFileSubDir + "0";
                            _ScenarioFileDir = _ScenarioFileDir + "0";
                        }
                        _ResFileSubDir = _ResFileSubDir + SessionId.ToString().Trim() + "\\";
                        _ScenarioFileDir = _ScenarioFileDir + SessionId.ToString().Trim() + "\\";
                    }
                    this._PutFdrvRun = this._FDRV + this._ResFileSubDir;
                }
                //jrs 2019/07/26
                Debug.WriteLine("- fetching regionalization from IFs");
                IDimension d_region;
                using (var db = new IFsContext())
                {
                    this._regionalization = db.RegionalizationFull.ToDictionary(
                        e => e.CountryName, e => e.RegionId);
                    d_region = db.GetRegionDimension();
                }
                // context for querying historical data
                Debug.WriteLine("- instantiating IFsHistSeries");
                //jrs 2024/10/14
                if (SessionId > -1)
                { this.dbHistSeries = new IFsHistSeriesContext(_ResFileSubDir.Replace("RUNFILES\\", "") + "IfsHistSeries.db"); }
                else
                { this.dbHistSeries = new IFsHistSeriesContext(); }

                //jrs 2021/12/15
                // context for querying historical data
                Debug.WriteLine("- instantiating IFsHistSeriesDyad");
                this.dbHistSeriesDyad = new IFsHistSeriesDyadicContext();

                //jrs 2019/09/16
                //Debug.WriteLine("- instantiating Regression Coefficients");
                //this._regcoeffs = new Dictionary<string, IEnumerable<IRegressionCoefficients>>();
            }
            catch(Exception ex) {
                var m = ex.Message;
            }
            Debug.Unindent();
            Debug.WriteLine("initialized forecast context");
        }

        public short FirstYear => _working.FirstYear;

        //jrs 2019/06/27 public short LastYear => _working.LastYear;
        public short LastYear => _LastYear;

        //jrs 2019/07/26
        public short FirstYearForecast => _FirstYearForecast;

        public short FirstYearHistory => _FirstYearHistory;

        public short LastYearForecast => _LastYearForecast;

        public short LastYearHistory => _LastYearHistory;

        //jrs 2023/01/30
        public short ConstantDollarsYear => _ConstantDollarYears;

        public short BaseYear
            => this.HistoricMode ? _working.LastYear : _working.FirstYear;

        public bool HistoricMode
            => this.Mode == ContextMode.Historic;

        public short NCohorts
            => this._NCohorts;

        public short NSec
            => this._NSec;

        public short NGovExp
            => this._NGovExp;

        public short NSkill
            => this._NSkill;

        public short NWBTar
            => this._NWBTar;

        public short LastCohortFert
            => this._LastCohortFert;

        public bool ShowMsg
            => this._ShowMsg; //jrs 2019/07/17

        public short NAges
            => this._NAges;

        public short NGender
            => this._NGender;

        public short NPriGr
            => this._NPriGr;
        
        public short NIOMatrices
            => this._NIOMatrices;

        public short NHHTypes
            => this._NHHTypes;

        public short WVSCohorts
            => this._WVSCohorts;

        public short NInfraIndexICTComp
            => this._NInfraIndexICTComp;

        public short NInfraType
            => this._NInfraType;

        //ttt 2019/08/12 begin
        public short NInfraIndexTradComp => this._NInfraIndexTradComp; 

        public short NInfraIndTranComp => this._NInfraIndTranComp; 

        public short NInfraIndElecComp => this._NInfraIndElecComp; 

        public short NInfraIndWatSanComp => this._NInfraIndWatSanComp; 

        public short NInfraIndexComp => this._NInfraIndexComp;

        public short NLocation => this._NLocation;
        //ttt 2019/08/12 end

        //ttt 2019/08/13
        public short NET => this._NET;

        public short NEP => this._NEP;

        public short NER => this._NER;
        //ttt

        public string PutFdrvRun
            => this._PutFdrvRun;

        public bool DevelopmentMode
            => this._DevelopmentMode;

        //jrs 2024/01/24
        public bool MultiThreading => this._MultiThreading;

        public string ActiveBase => this._ActiveBase;

        public string ScenarioFileDir => this._ScenarioFileDir;

        public string DataFileDir => this._DataFileDir;
        public IEnumerable<IVariable> Variables => _working.Variables;

        public ICollection<string> VariableNames => _working.VariableNames;

        public IVariable GetVariable(string name) => _working.GetVariable(name);

        public IEnumerable<IDimension> Dimensions => _working.Dimensions;

        public ICollection<int> DimensionIds => _working.DimensionIds;

        public IDimension GetDimension(int index) => _working.GetDimension(index);

        public IEnumerable<IRegression> Regressions => _working.Regressions;

        public IEnumerable<IRegression> GetRegression(string name) => _working.GetRegression(name);

        public ICollection<string> RegressionNames => _working.RegressionNames;

        //jrs 2019/09/16
        //private IDictionary<string, IEnumerable<IRegressionCoefficients>> _regcoeffs;

        //jrs 2019/07/17
        public IEnumerable<IRegressionCoefficients> GetRegressionCoefficients(string name)
        {
            //jrs 2019/09/16
            //if (!_regcoeffs.ContainsKey(name))
            //{
            //    _regcoeffs.Add(name, _working.GetRegressionCoefficients(name));
            //}

            return _working.GetRegressionCoefficients(name);
            //return _regcoeffs[name];
        }

        //jrs 2019/07/19
        public IEnumerable<IPoint<double>> GetRegressionPoints(string name) => _working.GetRegressionPoints(name);

        //2019/07/26
        public IDictionary<string, int> Regionalization
            => this._regionalization;


        //jrs 2019/07/26
        public IDictionary<string, float?> GetSlice(string seriesName, string fieldName)
        {
            return this.dbHistSeries.SliceSeries(seriesName, fieldName).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public IDictionary<string, float?> GetSlice(string seriesName, short year, CoalescePolicy coalesce = CoalescePolicy.Null)
        {
            var slice = this.dbHistSeries.SliceSeries(seriesName, year.ToString());
            switch (coalesce)
            {
                case CoalescePolicy.Zero:
                    return (from s in slice
                            select new KeyValuePair<string, float?>(s.Key, s.Value ?? (float?)0.0f)
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

        //jrs 2020/07/14
        public IEnumerable<short> GetYears(string seriesName)
        {
            return this.dbHistSeries.GetYears(seriesName);
        }

        //jrs 2021/12/15
        public IEnumerable<short> GetYearsDyad(string seriesName)
        {
            return this.dbHistSeriesDyad.GetYearsDyad(seriesName);
        }

        //jrs 2020/07/14
        public IDictionary<string, float?[]> GetDice(string seriesName, params short[] years)
        {
            return this.dbHistSeries.DiceSeries(seriesName, years.Select(y => y.ToString()).ToArray()).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        //jrs 2021/12/15
        public IDictionary<string, float?[]> GetDiceDyad(string seriesName, params short[] years)
        {
            return this.dbHistSeriesDyad.DiceSeries(seriesName, years.Select(y => y.ToString()).ToArray()).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public IQueryBuilder Query(string variableName) {
            return _working.Query(variableName);
        }

        public void Store(string key, object value) {
            Debug.WriteLine($"caching dataframe {key}.");
            // TODO
        }

        public T Fetch<T>(string key) {
            Debug.WriteLine($"fetching dataframe {key}.");
            // TODO
            throw new KeyNotFoundException($"Asset {key} is not found.");
        }

        public void Evict(string key) {
            Debug.WriteLine($"evicting dataframe {key}.");
            // TODO
        }

        public void Dispose() {
            Debug.WriteLine("disposing forecast context");
            // NOP
            Debug.WriteLine("disposed forecast context");
        }

        public ContextMode Mode
        {
            get => this._mode;
            set
            {
                if (this._mode != value)
                {
                    Debug.WriteLine($"- setting context mode: {value}");
                    this._mode = value;
                    // update time dimension after mode change
                    //this._dimensions[0] = getTimeDimension();
                }
            }
        }

    }

}
