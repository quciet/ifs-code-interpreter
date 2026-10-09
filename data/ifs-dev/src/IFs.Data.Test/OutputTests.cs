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
    public class OutputTestSuite
    {
        [Fact]
        public void TestStorage_migration() {
            using (var db = new StorageContext()) {
                db.Database.EnsureDeleted();
                db.Database.Migrate();
            }
        }

        // [Fact]
        // public void TestBasecase_migration() {
        //     using (var db = BasecaseContext.Open()) {
        //         db.Database.EnsureDeleted();
        //         db.Database.Migrate();
        //     }
        // }

        private void testDefaultcontext(IContext context, short firstYear, short lastYear) {
            Assert.Equal(firstYear, context.FirstYear);
            Assert.Equal(lastYear, context.LastYear);
            // variables
            Assert.Equal(1489, context.VariableNames.Count);
            Assert.Equal(1489, context.Variables.Count());
            var v1 = context.GetVariable("POP");
            Assert.Equal("Population", v1.DisplayName);
            Assert.Equal(new int[] {0, 1}, v1.Dimensions);
            var v2 = context.GetVariable("EDBASENRG");
            Assert.Equal("Gross Basic Education Enrollment Rate", v2.DisplayName);
            Assert.Equal(new int[] {0, 1, 23}, v2.Dimensions);
            Assert.Equal(new int[] {0, 1, 23}, v2.Dimensions);
            // dimensions
            Assert.Equal(53, context.DimensionIds.Count); // time + 52 other dimensions
            Assert.Equal(53, context.Dimensions.Count()); // time + 52 other dimensions
            Assert.Equal(lastYear - firstYear + 1, context.GetDimension(0).Buckets.Count);
            Assert.Equal(189, context.GetDimension(1).Buckets.Count);
            Assert.Equal(6, context.GetDimension(2).Buckets.Count);
        }

        [Theory]
        [InlineData(ContextMode.Historic, 1960, 2015)]
        [InlineData(ContextMode.Future, 2015, 2100)]
        public void TestContextService_aggregator(ContextMode mode, short firstYear, short lastYear) {
            using (IContextService contextSvc = new ContextService()) {
                using (IRebuildContext context = contextSvc.ForRebuild(mode)) {
                    CoalescePolicy defaultPolicy = context.HistoricMode ? CoalescePolicy.Earliest : CoalescePolicy.MostRecent;
                    context.DefaultAggregator = ContextService.SUM[defaultPolicy];
                    context.PutAggregator("hivpeakyr", typeof(HIVPeakAggregator));
                    context.PutAggregator("hivpeakr", typeof(HIVPeakAggregator));
                    // slices of historical series
                    var POP = context.GetSlice("POP", context.FirstYear, defaultPolicy);
                    var GDP = context.GetSlice("GDP", context.FirstYear, defaultPolicy);
                    // historical series
                    var v1 = context.GetHistSeries("POP");
                    Assert.Equal(189, v1.Count);
                    var r1 = v1.First();
                    Assert.Equal(2, r1.Key.Length); // timeId, regionId
                    var v2 = context.GetHistSeries("EDBASENRG", "Male");
                    Assert.Equal(189, v2.Count);
                    // NOTE: LIFEXP has historical series for Male and Female but not Total,
                    // this is a special case describe by José in his email from 2018/09/19
                    var v3 = context.GetHistSeries("LIFEXP", "Male");
                    Assert.Equal(189, v3.Count);
                    var v4 = context.GetHistSeries("LIFEXP", "Total");
                    Assert.Equal(0, v4.Count);
                    var v5 = context.GetHistSeries("hivpeakyr");
                    var v6 = context.GetHistSeries("hivpeakr");
                    // context interfaces
                    testDefaultcontext(context as IContext, firstYear, lastYear);
                }
            }
        }

        [Theory]
        [InlineData(ContextMode.Historic, 1960, 2015)]
        [InlineData(ContextMode.Future, 2015, 2100)]
        public void TestStorageService_as_context(ContextMode mode, short firstYear, short lastYear) {
            using (IContextService contextSvc = new ContextService()) {
                using (IStorageService storageSvc = new StorageService(contextSvc)) {
                    using (IRebuildContext context = contextSvc.ForRebuild(mode)) {
                        using (var working = storageSvc.Open(true, context)) {
                            // context interfaces
                            testDefaultcontext(working, firstYear, lastYear);
                        }
                    }
                }
            }
        }

        [Theory]
        [InlineData("POP")]
        [InlineData("EDBASENRG")]
        public void TestStorageService_variable_non_null(string variableName) {
            using (IContextService contextSvc = new ContextService()) {
                using (IStorageService storageSvc = new StorageService(contextSvc)) {
                    using (IRebuildContext context = contextSvc.ForRebuild()) {
                        using (var working = storageSvc.Open(true, context)) {
                            // save dataframe to DB
                            using (var inStream = new MemoryStream()) {
                                BinaryWriter writer = new BinaryWriter(inStream);
                                writer.Write("Hello World!");
                                inStream.Position = 0;
                                working.PutVariableData(variableName, inStream.ToArray());
                            }
                            working.Flush();
                            // load dataframe from DB
                            using (var outStream = working.GetVariableStream(variableName)) {
                                outStream.Position = 0;
                                BinaryReader reader = new BinaryReader(outStream);
                                Assert.Equal("Hello World!", reader.ReadString());
                            }
                        }
                    }
                }
            }
        }

        [Theory]
        [InlineData("POP")]
        [InlineData("EDBASENRG")]
        public void TestStorageService_variable_null(string variableName) {
            using (IContextService contextSvc = new ContextService()) {
                using (IStorageService storageSvc = new StorageService(contextSvc)) {
                    using (IRebuildContext context = contextSvc.ForRebuild()) {
                        using (var working = storageSvc.Open(true, context)) {
                            // save dataframe to DB
                            working.PutVariableData(variableName, null);
                            working.Flush();
                            // load dataframe from DB
                            using (var outStream = working.GetVariableStream(variableName)) {
                                Assert.Null(outStream);
                            }
                        }
                    }
                }
            }
        }

        [Fact]
        public void TestBasecaseService() {
            using (IContextService contextSvc = new ContextService()) {
                using (IRebuildContext context = contextSvc.ForRebuild()) {
                    using (var db = BasecaseContext.Open(true, context, "")) {
                        // nop
                    }
                }
            }
        }

    }
}
