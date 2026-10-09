using System;
using System.Linq;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Xunit;
using IFs.Data;

namespace IFs.Test
{

    [Collection("IFs.Test.Data")]
    public class ContextTestSuite
    {
        [Fact]
        public void TestContextFacade_all_years() {
            using (IContextService contextSvc = new ContextService()) {
                using (IRebuildContext context = contextSvc.ForRebuild()) {
                    var cntx = context as IRebuildFacade;
                    var seriesName = "EDBASENRG_Male";
                    short[] years = cntx.GetYears(seriesName).OrderByDescending(y => y).ToArray();
                    var dice = cntx.GetDice(seriesName, years);
                    Assert.Equal(216, dice.Count);
                    foreach (var country in cntx.Regionalization.Keys) {
                        var series = dice[country];
                        // most recent year with non-null data
                        int? i = Enumerable.Range(0, years.Length).FirstOrNull(k => series[k].HasValue);
                        if (!i.HasValue) {
                            Debug.WriteLine($"{country}: null (?), null (?)");
                            continue;
                        }
                        short mostRctYr = years[i.Value];
                        float? mostRct = series[i.Value];
                        // 5 years or more back with non-null data
                        int? j = Enumerable.Range(0, years.Length).FirstOrNull(k => (years[k] <= mostRctYr - 5) && series[k].HasValue);
                        if (!j.HasValue) {
                            Debug.WriteLine($"{country}: null (?), {mostRct.Value} ({mostRctYr})");
                            continue;
                        }
                        short histDatYr = years[j.Value];
                        float? histDat = series[j.Value];
                        Debug.WriteLine($"{country}: {histDat.Value} ({histDatYr}), {mostRct.Value} ({mostRctYr})");
                    }
                }
            }
        }
    }

}