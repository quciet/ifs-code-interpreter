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
    public class ExtensionTestSuite
    {
        [Theory]
        [InlineData(0, 1, new int[] {})]
        [InlineData(0, 1, new int[] {}, new int[] {})]
        [InlineData(5, 1, new int[] {1, 2, 3, 4, 5}, new int[] {1, 2, 3, 4, 5})]
        [InlineData(2, 3, new int[] {1, 1}, new int[] {1}, new int[] {2, 3}, new int[] {4})]
        [InlineData(6, 3, new int[] {1, 1, 2, 2, 3, 3}, new int[] {1, 2, 3}, new int[] {4, 5}, new int[] {6})]
        [InlineData(0, 1, new int[] {}, new int[] {}, new int[] {1, 2}, new int[] {})]
        public void TestDataExtensions_CartesianProduct(int nRows, int nColumns, int[] firstCol, params int[][] sets) {
            var table = sets.CartesianProduct();
            int count = 0;
            foreach (var col in table) {
                ++count;
                Assert.Equal(nRows, col.Count());
                if (count == 1) {
                    Assert.Equal(col, firstCol);
                }
            }
            Assert.Equal(nColumns, count);
        }

        [Theory]
        [InlineData(1, 0, new int[] {})]
        [InlineData(1, 0, new int[] {}, new int[] {})]
        [InlineData(3, 2, new int[] {1, 4}, new int[] {1, 2, 3}, new int[] {4, 5, 6})]
        [InlineData(3, 3, new int[] {1, 4, 7}, new int[] {1, 2, 3, 10, 11}, new int[] {4, 5, 6}, new int[] {7, 8, 9})]
        public void TestDataExtensions_Transpose(int nRows, int nColumns, int[] firstRow, params int[][] columns) {
            var table = columns.Transpose();
            int count = 0;
            foreach (var row in table) {
                ++count;
                Assert.Equal(nColumns, row.Count());
                if (count == 1) {
                    Assert.Equal(row, firstRow);
                }
            }
            Assert.Equal(nRows, count);
        }

        [Theory]
        [InlineData("POP", 0, 16254, 2)]
        [InlineData("POP", 1, 189, 1)]
        [InlineData("POP", 2, 1, 0)]
        [InlineData("POP", 3, 1, 0)]
        [InlineData("EDBASENRG", 0, 48762, 3)]
        [InlineData("EDBASENRG", 1, 567, 2)]
        [InlineData("EDBASENRG", 2, 3, 1)]
        [InlineData("EDBASENRG", 3, 1, 0)]
        [InlineData("EDBASENRG", 4, 1, 0)]
        public void TestDataExtensions_MeshGrid(string variableName, int skipNum, int nRows, int nColumns) {
            using (IContextService contextSvc = new ContextService()) {
                using (IRebuildContext context = contextSvc.ForRebuild()) {
                    var v = context.GetVariable(variableName);
                    var dimensions = v.Dimensions.Skip(skipNum).Select(d => context.GetDimension(d));
                    var cells = Math.Max(0, v.Dimensions.Count - skipNum) * dimensions.Select(d => d.Buckets.Keys.Count()).Aggregate(1, (a, b) => a * b);
                    var table = dimensions.MeshGrid();
                    Assert.Equal(nRows, table.Count());
                    foreach (var row in table) {
                        Assert.Equal(nColumns, row.Count());
                    }
                    Assert.Equal(nRows * nColumns, cells);
                }
            }
        }


        [Theory]
        [InlineData(ContextMode.Future, "A Function to Play With", 0.9, 6.0)]
        [InlineData(ContextMode.Future, "A Function to Play With", 1.0, 6.0)]
        [InlineData(ContextMode.Future, "A Function to Play With", 1.5, 6.5)]
        [InlineData(ContextMode.Future, "A Function to Play With", 2.0, 7.0)]
        [InlineData(ContextMode.Future, "A Function to Play With", 3.0, 8.0)]
        [InlineData(ContextMode.Future, "A Function to Play With", 4.0, 9.0)]
        [InlineData(ContextMode.Future, "A Function to Play With", 4.5, 9.5)]
        [InlineData(ContextMode.Future, "A Function to Play With", 5.0, 10.0)]
        [InlineData(ContextMode.Future, "A Function to Play With", 5.1, 10.0)]
        [InlineData(ContextMode.Future, "GDP/Capita (PPP) Versus Total Fertility Rate (1995)", 1.0, 5.021)]
        public void TestEstimateExtensions_raw_estimator(ContextMode mode, string regressionName, double indVar, double depVarStd) {
            using (IContextService contextSvc = new ContextService()) {
                using (IRebuildContext context = contextSvc.ForRebuild(mode)) {
                    var rawEstimator = context.GetRegression(regressionName).Single().ToRawEstimator();
                    Assert.NotNull(rawEstimator);
                    var depVar = rawEstimator(indVar);
                    Assert.Equal(depVarStd, depVar, 3);
                }
            }
        }

        [Theory]
        [InlineData(ContextMode.Future, "GDP/Capita (PPP) Versus Total Fertility Rate (1995)", new string[] { "GDPPCP" }, "United States", -6.440f)]
        [InlineData(ContextMode.Future, "BMI Female (2005)", new string[] { "GDPPCP", "Educ25" }, "United States", 27.739f)]
        public void TestEstimateExtensions_estimator(ContextMode mode, string regressionName, string[] indVarNames, string country, float? depVarStd) {
            using (IContextService contextSvc = new ContextService()) {
                using (IRebuildContext context = contextSvc.ForRebuild(mode)) {
                    var indVarSeries = (from n in indVarNames select context.GetSlice(n, context.FirstYear, CoalescePolicy.Zero)).ToArray();
                    var Estimate = context.GetRegression(regressionName).Normalize(indVarNames).ToEstimator(indVarSeries);
                    Assert.NotNull(Estimate);
                    var depVar = Estimate(country);
                    Assert.True(depVarStd.HasValue);
                    Assert.True(depVar.HasValue);
                    Assert.Equal(depVarStd.Value, depVar.Value, 3);
                }
            }
        }

    }
}
