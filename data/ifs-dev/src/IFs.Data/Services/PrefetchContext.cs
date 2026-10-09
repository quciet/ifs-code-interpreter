using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IFs.Data {

    // Proactively caches all variables, dimensions and regressions from a 
    // source IContext, and serves cached data from in-memory data structures.
    // The aim is to facilitate multi-threaded access, and to reduce the 
    // overhead of repetitively accessing the source, improves performance
    // significantly when the source is based on external storage.
    internal class PrefetchContext: IContext {

        public static IContext Create(IContext src) {
            return new PrefetchContext(src);
        }

        private readonly IContext _source;

        private readonly short _first_year;

        //jrs 2023/01/24
        private readonly short _first_year_history;
        //jrs 2023/01/30
        private readonly short _constant_dollars_year;
        //jrs 2023/10/03
        private readonly short _NGender;

        //mti 2023/10/15
        private readonly short _NPriGr;

        private readonly short _last_year;

        private readonly short _base_year;

        private readonly IDictionary<string, IVariable> _variables;

        private readonly IDictionary<int, IDimension> _dimensions;

        private readonly IDictionary<string, IReadOnlyList<IRegression>> _regressions;

        private PrefetchContext(IContext src) {
            Debug.WriteLine("initializing prefetch context");
            Debug.Indent();
            this._source = src;
            this._first_year = src.FirstYear;
            this._last_year = src.LastYear;
            this._base_year = src.BaseYear;
            //jrs 2023/01/24
            this._first_year_history = src.FirstYearHistory;
            //jrs 2023/01/30
            this._constant_dollars_year = src.ConstantDollarsYear;
            //jrs 2023/10/03
            this._NGender = src.NGender;

            //mti 2023/10/15
            this._NPriGr = src.NPriGr;

            Debug.WriteLine("- fetching variables from source context");
            this._variables = src.Variables.ToDictionary(v => v.Name);
            Debug.WriteLine("- fetching dimensions from source context");
            this._dimensions = src.Dimensions.ToDictionary(d => d.Id);
            Debug.WriteLine("- fetching regressions from source context");
            this._regressions = src.Regressions.GroupBy(r => r.Name).ToDictionary(
                grp => grp.Key, 
                grp => grp.OrderBy(r => r.InputName).ToList() as IReadOnlyList<IRegression>
            );
            Debug.Unindent();
            Debug.WriteLine("initialized prefetch context");
        }

        public ExecutionStage Stage
            => this._source.Stage;

        public bool HistoricMode
            => this._source.HistoricMode;

        public short FirstYear
            => this._first_year;

        //jrs 2023/01/24
        public short FirstYearHistory
            => this._first_year_history;

        public short LastYear
            => this._last_year;

        public short BaseYear
            => this._base_year;

        //jrs 2023/01/30
        public short ConstantDollarsYear
            => this._constant_dollars_year;

        //jrs 2023/10/03
        public short NGender
            => this._NGender;

        //mti 2023/10/15
       public short NPriGr
    => this._NPriGr;

        public string ActiveBase 
            => this._source.ActiveBase;

        public string DataFileDir
            => this._source.DataFileDir;


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

        public ICollection<string> RegressionNames
            => this._regressions.Keys;

        public IEnumerable<IRegression> Regressions {
            get {
                foreach (var grp in this._regressions.Values) {
                    foreach (var reg in grp) {
                        yield return reg;
                    }
                }
            }
        }

        public IEnumerable<IRegression> GetRegression(string name) {
            if (!this._regressions.ContainsKey(name)) {
                return Enumerable.Empty<IRegression>();
            }
            return this._regressions[name];
        }

        public IEnumerable<IRegressionCoefficients> GetRegressionCoefficients(string name)
            => this._source.GetRegressionCoefficients(name);

        public IEnumerable<IPoint<double>> GetRegressionPoints(string name) 
            => this._source.GetRegressionPoints(name);

        public IQueryBuilder Query(string variableName)
            => this._source.Query(variableName);

    }

}
