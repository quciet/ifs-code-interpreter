using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using IFs.Data;

namespace IFs.Test
{
    internal class HIVPeakAggregator: WeightedAverageAggregator
    {

        public override CoalescePolicy Policy
            => CoalescePolicy.MostRecent;

        public override IDictionary<string, float?> Weights
            => CPOP;

        [ImportsAsset("CPOP")]
        public IDictionary<string, float?> CPOP { get; set; }

        public override IDictionary<string, float?> Normalize(string seriesName, IRebuildFacade cntx) {
            IDictionary<string, float?> series;
            switch (seriesName) {
                case "hivpeakyr":
                    series = cntx.GetSlice(seriesName, "PeakYear");
                    break;
                case "hivpeakr":
                    series = cntx.GetSlice(seriesName, "PeakPrevalence");
                    break;
                default:
                    throw new ArgumentException($"Aggregator cannot be applied on series {seriesName}.");
            }
            var Estimate = cntx.GetEstimator(seriesName);
            return series.ToDictionary(s => s.Key, s => s.Value ?? Estimate(s.Key));
        }
    }

}
