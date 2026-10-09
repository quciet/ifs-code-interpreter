using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IFs.Data
{
    public static class EstimateExtensions
    {
        // build a raw estimator from a regression
        public static Func<double, double> ToRawEstimator(this IRegression regression) {
            if (regression.Points.Count > 0) {
                return BuildPointEstimator(regression.Points);
            } else {
                return BuildFormulaEstimator(regression.Coefficients);
            }
        }

        // build an estimator from a regression and a corresponding series (dict of <CountryName, Value>) of independent variable
        public static SeriesEstimator ToEstimator(this IRegression regression, IDictionary<string, float?> series) {
            return BindSimpleEstimator(regression.ToRawEstimator(), series);
        }

        // reorder `regressions` such that the .InputName of the items are in the same order as they appear in `names`,
        // raise `ArgumentException` if there is no corresponding `IRegression` having the specified name.
        public static IEnumerable<IRegression> Normalize(this IEnumerable<IRegression> regressions, params string[] names) {
            var regDict = regressions.ToDictionary(r => r.InputName, r => r);
            if (regDict.Count != names.Length) {
                throw new ArgumentException($"Invalid number of independent variables: {regDict.Count} expected.");
            }
            foreach (string name in names) {
                if (regDict.TryGetValue(name, out var regression)) {
                    if (regression.InputName != name) {
                        throw new ArgumentException($"Unexpected regression {regression.Name}/{regression.InputName}: {name} expected.");
                    }
                    yield return regression;
                } else {
                    throw new ArgumentException($"No such regression taking {name} as independent variable.");
                }
            }
        }

        // build a composite estimator from multiple regressions and corresponding series of independent variables
        public static SeriesEstimator ToEstimator(this IEnumerable<IRegression> regressions, params IDictionary<string, float?>[] series) {
            var regArray = regressions.ToArray();
            if (regArray.Length != series.Length) {
                throw new ArgumentException($"Invalid number of independent variables: {regArray.Length} expected.");
            }
            // special case #1: when regressions is empty, reduct to null estimator
            if (regArray.Length < 1) {
                return ContextService.NULL_ESTIMATOR;
            }
            // special case #2: reduce to simple regression estimator
            if (regArray.Length == 1) {
                return regArray.Single().ToEstimator(series.Single());
            }
            // general case: compose a composite estimator that sums up the results of per-input estimators
            var initial = regArray.First().Coefficients;
            if (!initial.TryGetValue("b9", out double b9)) {
                b9 = 0.0;
            }
            if (!initial.TryGetValue("a", out double a)) {
                a = 0.0;
            }
            var g = regArray.Zip(series, (r, s) => BindPartialEstimator(BuildPartialEstimator(r.Coefficients), s)).ToArray();
            if (b9.IsNonZero()) {
                return (x) => {
                    double? partial = g.Select(f => f(x)).Sum();
                    if (partial.HasValue) {
                        //jrs 2025/05/09 changed signs so that interpretation of coefficients is standard for logistic, positive means increase in probability
                        //return (float) (b9 / (1.0 + Math.Exp(a + partial.Value)));
                        return (float)(b9 / (1.0 + Math.Exp(-1*a - partial.Value)));
                    }
                    return null;
                };
            } else {
                return (x) => (float) (a + g.Select(f => f(x)).Sum());
            }
        }

        // bind a series to a simple estimator as source of independent variable values
        private static SeriesEstimator BindSimpleEstimator(Func<double, double> estimator, IDictionary<string, float?> series) {
            return (string country) => {
                if (series.TryGetValue(country, out float? indVar)) {
                    if (indVar.HasValue) {
                        return (float) estimator(indVar.Value);
                    }
                }
                return null;
            };
        }

        // bind a series to a partial estimator as source of independent variable values,
        // returns double? instead of float?, otherwise is identical to BindSimpleEstimator
        private static Func<string, double?> BindPartialEstimator(Func<double, double> estimator, IDictionary<string, float?> series) {
            return (string country) => {
                if (series.TryGetValue(country, out float? indVar)) {
                    if (indVar.HasValue) {
                        return estimator(indVar.Value);
                    }
                }
                return null;
            };
        }

        // comparing a double value with zero with customizable rounding tolerance
        internal static bool IsNonZero(this double x, double eps = Double.Epsilon) {
            return (x < -eps) || (x > eps);
        }

        // migrated from the `bisect_left` method from Python's standard library
        internal static int BisectLeft(this double[] A, double x) {
            int lo = 0;
            int hi = A.Length;
            while (lo < hi) {
                int mid = (lo + hi) / 2; 
                if (A[mid] < x) {
                    lo = mid + 1;
                } else {
                    hi = mid;
                }
            }
            return lo; 
        }

        private static Func<double, double> BuildPointEstimator(IReadOnlyCollection<IPoint<double>> points) {
            Debug.Assert(points.Count > 0);
            var array = points.OrderBy(p => p.X).ToArray();
            var Xs = array.Select(p => p.X).ToArray();
            return (x) => {
                int pos = Xs.BisectLeft(x);
                if (pos < 1) {
                    // underflow
                    return array[0].Y;
                } else if ((1 <= pos) && (pos < Xs.Length)) {
                    var p0 = array[pos - 1];
                    var p1 = array[pos];
                    return (p0.Y * (p1.X - x) + p1.Y * (x - p0.X)) / (p1.X - p0.X);
                } else {
                    // overflow
                    return array[array.Length - 1].Y;
                }
            };
        }

        private static Func<double, double> BuildFormulaEstimator(IReadOnlyDictionary<string, double> coefficients) {
            if (!coefficients.TryGetValue("b1", out double b1)) {
                b1 = 0.0;
            }
            if (!coefficients.TryGetValue("b2", out double b2)) {
                b2 = 0.0;
            }
            if (!coefficients.TryGetValue("b3", out double b3)) {
                b3 = 0.0;
            }
            if (!coefficients.TryGetValue("b4", out double b4)) {
                b4 = 0.0;
            }
            if (!coefficients.TryGetValue("b5", out double b5)) {
                b5 = 0.0;
            }
            if (!coefficients.TryGetValue("b6", out double b6)) {
                b6 = 0.0;
            }
            if (!coefficients.TryGetValue("b7", out double b7)) {
                b7 = 0.0;
            }
            if (!coefficients.TryGetValue("b8", out double b8)) {
                b8 = 0.0;
            }
            if (!coefficients.TryGetValue("b9", out double b9)) {
                b9 = 0.0;
            }
            if (!coefficients.TryGetValue("a", out double a)) {
                a = 0.0;
            }
            if (b9.IsNonZero()) {
                return (x) => b9 / (1.0 + Math.Exp(a + b1 * x + b2 * Math.Pow(x, 2) + b3 * Math.Pow(x, 3) + b4 * Math.Log(x <= 0 ? 1 : x, Math.E) + b5 * Math.Pow(x, b6) + b7 * Math.Exp(b8 * x)));
            } else {
                return (x) => a + b1 * x + b2 * Math.Pow(x, 2) + b3 * Math.Pow(x, 3) + b4 * Math.Log(x <= 0 ? 1: x, Math.E) + b5 * Math.Pow(x, b6) + b7 * Math.Exp(b8 * x);
            }
        }

        private static Func<double, double> BuildPartialEstimator(IReadOnlyDictionary<string, double> coefficients) {
            if (!coefficients.TryGetValue("b1", out double b1)) {
                b1 = 0.0;
            }
            if (!coefficients.TryGetValue("b2", out double b2)) {
                b2 = 0.0;
            }
            if (!coefficients.TryGetValue("b3", out double b3)) {
                b3 = 0.0;
            }
            if (!coefficients.TryGetValue("b4", out double b4)) {
                b4 = 0.0;
            }
            if (!coefficients.TryGetValue("b5", out double b5)) {
                b5 = 0.0;
            }
            if (!coefficients.TryGetValue("b6", out double b6)) {
                b6 = 0.0;
            }
            if (!coefficients.TryGetValue("b7", out double b7)) {
                b7 = 0.0;
            }
            if (!coefficients.TryGetValue("b8", out double b8)) {
                b8 = 0.0;
            }
            return (x) => b1 * x + b2 * Math.Pow(x, 2) + b3 * Math.Pow(x, 3) + b4 * Math.Log(x <= 0 ? 1 : x, Math.E) + b5 * Math.Pow(x, b6) + b7 * Math.Exp(b8 * x);
        }
    }
}