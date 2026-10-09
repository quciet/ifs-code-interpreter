using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace IFs.Data
{

    internal partial class RebuildContext
    {
        public CoalescePolicy DefaultPolicy { get; set; }

        private SeriesEstimator _default_estimator = ContextService.NULL_ESTIMATOR;

        public SeriesEstimator DefaultEstimator {
            get => this._default_estimator;
            set {
                if (value == null) {
                    this._default_estimator = ContextService.NULL_ESTIMATOR;
                } else {
                    this._default_estimator = value;
                }
            }
        }

        private readonly IDictionary<string, SeriesEstimator> _estimators = new Dictionary<string, SeriesEstimator>();

        public SeriesEstimator GetEstimator(string seriesName) {
            // get estimator by series name
            SeriesEstimator estimator;
            if (this._estimators.TryGetValue(seriesName, out estimator)) {
                return estimator;
            }
            // get estimator by variable name
            int pos = seriesName.IndexOf("_");
            if ((pos > 0) && this._estimators.TryGetValue(seriesName.Substring(0, pos), out estimator)) {
                return estimator;
            }
            return DefaultEstimator;
        }

        public void PutEstimator(string seriesName, SeriesEstimator estimator) {
            int pos = seriesName.IndexOf("_");
            string variableName = (pos > 0) ? seriesName.Substring(0, pos) : seriesName;
            if (!VariableNames.Contains(variableName)) {
                throw new ArgumentException($"No such variable {variableName}.");
            }
            if (estimator == null) {
                this._estimators.Remove(seriesName);
            } else {
                this._estimators[seriesName] = estimator;
            }
        }

        private Type _default_aggregator = ContextService.NOP[CoalescePolicy.Null];

        public Type DefaultAggregator {
            get => this._default_aggregator;
            set {
                if (this._components.IsValueCreated) {
                    throw new NotSupportedException($"Cannot set default aggregator after initialization.");
                }
                if (value == null) {
                    this._default_aggregator = ContextService.NOP[CoalescePolicy.Null];
                } else {
                    this._default_aggregator = value;
                }
            }
        }

        private readonly IDictionary<string, Type> _aggregators = new Dictionary<string, Type>();

        public IAggregator GetAggregator(string variableName) {
            if (!this._aggregators.TryGetValue(variableName, out Type aggregatorType)) {
                aggregatorType = DefaultAggregator;
            }
            return this._components.Value.GetService(aggregatorType) as IAggregator;
        }

        public void PutAggregator(string variableName, Type aggregatorType) {
            if (this._components.IsValueCreated) {
                throw new NotSupportedException($"Cannot put aggregators after initialization.");
            }
            if (!VariableNames.Contains(variableName)) {
                throw new ArgumentException($"No such variable {variableName}.");
            }
            if (typeof(DummyAggregator).IsEquivalentTo(aggregatorType)) {
                // `DummyAggregator` indicates `DefaultAggregator` and is not tracked separately
                this._aggregators.Remove(variableName);
            } else if (aggregatorType.IsAbstract) {
                throw new ArgumentException($"{aggregatorType.FullName} is an abstract type.");
            } else if (!typeof(IAggregator).IsAssignableFrom(aggregatorType)) {
                throw new ArgumentException($"{aggregatorType.FullName} is not an IAggregator.");
            } else {
                this._aggregators[variableName] = aggregatorType;
            }
        }

        private IDictionary<string, object> _cache = new Dictionary<string, object>();

        public void Store(string key, object value) {
            Debug.WriteLine($"caching asset {key}.");
            this._cache[key] = value;
        }

        public T Fetch<T>(string key) {
            Debug.WriteLine($"fetching asset {key}.");
            if (this._cache.TryGetValue(key, out object value)) {
                return (T) value;
            }
            throw new KeyNotFoundException($"Asset {key} is not found.");
        }

        public void Evict(string key) {
            Debug.WriteLine($"evicting asset {key}.");
            this._cache.Remove(key);
        }

    }

    // abstract aggregator taking a null-handling policy as constructor option
    public abstract class CoalescingAggregator: IAggregator
    {
        public abstract CoalescePolicy Policy { get; }

        public virtual IDictionary<string, float?> Normalize(string seriesName, IRebuildFacade cntx) {
            var Estimate = cntx.GetEstimator(seriesName);
            return cntx.GetSlice(seriesName, cntx.FirstYear, this.Policy)
                .ToDictionary(s => s.Key, s => s.Value ?? Estimate(s.Key));
        }

        public abstract IEnumerable<KeyValuePair<int, float?>> Regionalize(string seriesName, IRebuildFacade cntx);
    }

    // strategy for regionalizing base year series without actual aggregation, throws
    // InvalidOperationException when the series contains more than one entries per region.
    public abstract class NopAggregator: CoalescingAggregator
    {
        public override IEnumerable<KeyValuePair<int, float?>> Regionalize(string seriesName, IRebuildFacade cntx) {
            return (from s in Normalize(seriesName, cntx)
                join r in cntx.Regionalization on s.Key equals r.Key
                group s.Value by r.Value
            ).ToDictionary(grp => grp.Key, grp => grp.Single());
        }
    }

    // strategy for regionalizing base year series using SUM aggregation
    public abstract class SumAggregator: CoalescingAggregator
    {
        public override IEnumerable<KeyValuePair<int, float?>> Regionalize(string seriesName, IRebuildFacade cntx) {
            return (from s in Normalize(seriesName, cntx)
                join r in cntx.Regionalization on s.Key equals r.Key
                group s.Value by r.Value
            ).ToDictionary(grp => grp.Key, grp => grp.Sum());
        }
    }

    // strategy for regionalizing base year series using non-weighted AVG aggregation
    public abstract class AvgAggregator: CoalescingAggregator
    {
        public override IEnumerable<KeyValuePair<int, float?>> Regionalize(string seriesName, IRebuildFacade cntx) {
            return (from s in Normalize(seriesName, cntx)
                join r in cntx.Regionalization on s.Key equals r.Key
                group s.Value by r.Value
            ).ToDictionary(grp => grp.Key, grp => grp.Average());
        }
    }

    // define concrete types for all built-in coalescing / aggregation combinations,
    // so that the dependency framework can differentiate them via type analysis.

    internal class NullNopAggregator: NopAggregator {
        public override CoalescePolicy Policy => CoalescePolicy.Null;
    }

    internal class ZeroNopAggregator: NopAggregator {
        public override CoalescePolicy Policy => CoalescePolicy.Zero;
    }

    internal class MostRecentNopAggregator: NopAggregator {
        public override CoalescePolicy Policy => CoalescePolicy.MostRecent;
    }

    internal class EarliestNopAggregator: NopAggregator {
        public override CoalescePolicy Policy => CoalescePolicy.Earliest;
    }

    internal class NullSumAggregator: SumAggregator {
        public override CoalescePolicy Policy => CoalescePolicy.Null;
    }

    internal class ZeroSumAggregator: SumAggregator {
        public override CoalescePolicy Policy => CoalescePolicy.Zero;
    }

    internal class MostRecentSumAggregator: SumAggregator {
        public override CoalescePolicy Policy => CoalescePolicy.MostRecent;
    }

    internal class EarliestSumAggregator: SumAggregator {
        public override CoalescePolicy Policy => CoalescePolicy.Earliest;
    }

    internal class NullAvgAggregator: AvgAggregator {
        public override CoalescePolicy Policy => CoalescePolicy.Null;
    }

    internal class ZeroAvgAggregator: AvgAggregator {
        public override CoalescePolicy Policy => CoalescePolicy.Zero;
    }

    internal class MostRecentAvgAggregator: AvgAggregator {
        public override CoalescePolicy Policy => CoalescePolicy.MostRecent;
    }

    internal class EarliestAvgAggregator: AvgAggregator {
        public override CoalescePolicy Policy => CoalescePolicy.Earliest;
    }

    // strategy for regionalizing base year series using weighted AVG aggregation
    public abstract class WeightedAverageAggregator: CoalescingAggregator
    {
        public abstract IDictionary<string, float?> Weights { get; }

        public override IEnumerable<KeyValuePair<int, float?>> Regionalize(string seriesName, IRebuildFacade cntx) {
            if ((this.Weights == null) || (this.Weights.Count < 1)) {
                throw new ArgumentNullException("Weights", $"Weight of {seriesName} is empty or not specified.");
            }
            return (from s in Normalize(seriesName, cntx)
                join r in cntx.Regionalization on s.Key equals r.Key
                join w in this.Weights on s.Key equals w.Key
                group new { Value = s.Value, Weight = w.Value } by r.Value into Group
                select new { RegionId = Group.Key, WeightedAverage = Group.Sum(v => v.Value * v.Weight) / Group.Sum(v => v.Weight) }
            ).ToDictionary(grp => grp.RegionId, grp => grp.WeightedAverage);
        }
    }

}
