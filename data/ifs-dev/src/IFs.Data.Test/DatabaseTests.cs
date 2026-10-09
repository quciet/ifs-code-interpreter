using System;
using System.Linq;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Xunit;
using IFs.Data;

namespace IFs.Test
{
    [Collection("IFs.Test.Data")]
    public class DatabaseTestSuite
    {
        [Fact]
        public void TestIFsInit_years() {
            using (var db = new IFsInitContext()) {
                Assert.Equal(1960, db.FirstYearHistory);
                Assert.Equal(2015, db.LastYearHistory);
                Assert.Equal(2015, db.FirstYearForecast);
                Assert.Equal(2100, db.LastYearForecast);
            }
        }

        [Fact]
        public void TestIFs_total() {
            using (var db = new IFsContext()) {
                Assert.Equal(216, db.RegionalizationFull.Count());
                Assert.Equal(189, db.RegionIdNewFull.Count());
            }
        }

        [Theory]
        [InlineData("Australia", "Australia")]
        [InlineData("Taiwan, China", "Taiwan")]
        [InlineData("United States", "USA")]
        public void TestIFs_name_mapping(string countryName, string regionName) {
            using (var db = new IFsContext()) {
                // translation from country name to region name
                int RegionId = db.RegionalizationFull.Find(countryName).RegionId;
                string Name = db.RegionIdNewFull.Find(RegionId).RegionName;
                Assert.Equal(regionName, Name);
            }
        }

        [Fact]
        public void TestIFsVar_total() {
            using (var db = new IFsVarContext()) {
                Assert.Equal(1489, db.All.Count());
            }
        }

        [Theory]
        [InlineData("POP", "Population", 2, new int[] {0, 1})]
        [InlineData("EDBASENRG", "Gross Basic Education Enrollment Rate", 2, new int[] {0, 1, 23})]
        public void TestIFsVar_properties(string name, string displayName, int type, int[] dimSeq) {
            using (var db = new IFsVarContext()) {
                var v = db.All.Where(e => e.Name == name).First();
                Assert.Equal(name, v.Name);
                Assert.Equal(displayName, v.DisplayName);
                Assert.Equal(type, v.Type);
                Assert.Equal(dimSeq, v.Dimensions);
            }
        }

        [Fact]
        public void TestIFsGrp_total() {
            using (var db = new IFsGrpContext()) {
                Assert.Equal(51, db.All.Count());
            }
        }

        [Theory]
        [InlineData("POP", 2015, true)]
        [InlineData("EDBASENRG_Male", 2005, true)]
        [InlineData("LIFEXP_Male", 2015, true)]
        [InlineData("LIFEXP_Female", 2015, true)]
        [InlineData("LIFEXP_Total", 2015, false)]
        public void TestIFsHistSeries_nonexistent_series(string seriesName, int year, bool exists) {
            using (var db = new IFsHistSeriesContext()) {
                var s = db.SliceSeries(seriesName, year.ToString());
                if (exists) {
                    Assert.NotEmpty(s);
                } else {
                    Assert.Empty(s);
                }
            }
        }

        [Theory]
        [InlineData("POP", "Australia", 2015, 23, 24)]
        [InlineData("EDBASENRG_Male", "United States", 2005, 99, 100)]
        public void TestIFsHistSeries_nonexistent_slices(string seriesName, string countryName, int year, int lbound, int ubound) {
            // non-existing year
            using (var db = new IFsHistSeriesContext()) {
                var s = db.SliceSeries(seriesName, "1860").ToDictionary(kv => kv.Key, kv => kv.Value);
                var v = s[countryName];
                Assert.False(v.HasValue);
            }
            // existing year
            using (var db = new IFsHistSeriesContext()) {
                var s = db.SliceSeries(seriesName, year.ToString()).ToDictionary(kv => kv.Key, kv => kv.Value);
                var v = s[countryName];
                Assert.True(v.HasValue);
                Assert.InRange(v.Value, lbound, ubound);
            }
            // non-existing year
            using (var db = new IFsHistSeriesContext()) {
                var s = db.SliceSeries(seriesName, "2200").ToDictionary(kv => kv.Key, kv => kv.Value);
                var v = s[countryName];
                Assert.False(v.HasValue);
            }
        }

        [Theory]
        [InlineData("A Function to Play With", 9, 5)]
        [InlineData("GDP/Capita (PPP) Versus Total Fertility Rate (1995)", 9, 0)]
        public void TestTablFunc_retrieve(string regressionName, int coefficients, int points) {
            using (var db = new TablFuncContext()) {
                var reg = db.All.Where(r => r.Name == regressionName).First();
                Assert.NotNull(reg);
                Assert.Equal(coefficients, reg.Coefficients.Count);
                Assert.Equal(points, reg.Points.Count());
            }
        }

    }

}
