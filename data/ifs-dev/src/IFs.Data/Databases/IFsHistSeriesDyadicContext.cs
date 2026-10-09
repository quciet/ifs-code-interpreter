using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NLog;

#if USE_JET
using EntityFrameworkCore.Jet;
#endif

namespace IFs.Data
{

    internal partial class IFsHistSeriesDyadicContext : DbContext
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public IFsHistSeriesDyadicContext()
        { }

        public IFsHistSeriesDyadicContext(DbContextOptions<IFsHistSeriesDyadicContext> options)
            : base(options)
        { }

        public IEnumerable<short> GetYearsDyad(string seriesName)
        {
            var modelType = this.GetModelType(seriesName);
            // return empty list instead of throwing an error when the entity type does not exist
            if (modelType == null)
            {
                Debug.WriteLine($"Entity '{seriesName}' does not exist.");
                return Enumerable.Empty<short>();
            }
            return (from col in this.FindFields(modelType)
                    where col.All(char.IsDigit)
                    select short.Parse(col));
        }

        // enumerate <CountryName, Value> pairs for the specified field of the series entity type
        public IEnumerable<KeyValuePair<string, float?>> SliceSeries(string seriesName, string fieldName)
        {
            var modelType = this.GetModelType(seriesName);
            // return empty list instead of throwing an error when the entity type does not exist
            if (modelType == null)
            {
                Debug.WriteLine($"Entity '{seriesName}' does not exist.");
                yield break;
            }
            // get low-level database connection
            var conn = this.Database.GetDbConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            using (var cmd = conn.CreateCommand())
            {
                // if the table does not contain the specified field, query a list of null values so that the slice still has
                // the full list of countries this would simplify the implementation of high-level coalescing policies.
                //gvdb 2024/04/05
                // update for new EF syntax
                var tableName = this.Model.FindEntityType(modelType).GetTableName();
                if (this.FindFields(modelType).Contains(fieldName))
                {
                    cmd.CommandText = $"SELECT [Actor] AS [Actor], [Partner] AS [Partner], [{fieldName}] AS [Value] FROM [{tableName}]";
                }
                else
                {
                    Debug.WriteLine($"Field '{tableName}.{fieldName}' does not exist.");
                    cmd.CommandText = $"SELECT [Actor] AS [Actor], [Partner] AS [Partner], NULL AS [Value] FROM [{tableName}]";
                }
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string key = reader.GetString(0).Trim() + ';' + reader.GetString(1).Trim();
                        float? value = null;
                        if (!reader.IsDBNull(2))
                        {
                            value = reader.GetFloat(2);
                        }
                        yield return new KeyValuePair<string, float?>(key, value);
                    }
                }
            }
        }

        //
        public IEnumerable<KeyValuePair<string, float?[]>> DiceSeries(string seriesName, params string[] fieldNames)
        {
            var modelType = this.GetModelType(seriesName);
            // return empty list instead of throwing an error when the entity type does not exist
            if (modelType == null)
            {
                Debug.WriteLine($"Entity '{seriesName}' does not exist.");
                yield break;
            }
            // get low-level database connection
            var conn = this.Database.GetDbConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            using (var cmd = conn.CreateCommand())
            {
            	//gvdb 2024/04/05
                // update for new EF syntax
                var tableName = this.Model.FindEntityType(modelType).GetTableName();
                var fieldList = this.FindFields(modelType);
                var projection = String.Join(", ", fieldNames.Select(f => {
                    if (fieldList.Contains(f))
                    {
                        return $"[{f}] AS [{f}]";
                    }
                    else
                    {
                        Debug.WriteLine($"Field '{tableName}.{f}' does not exist.");
                        return $"NULL AS [{f}]";
                    }
                }));
                cmd.CommandText = $"SELECT [Actor] AS [Actor], [Partner] AS [Partner], {projection} FROM [{tableName}]";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string key = reader.GetString(0).Trim() + ';' + reader.GetString(1).Trim();
                        float?[] values = fieldNames.Select((f, i) => {
                            if (!reader.IsDBNull(i + 2))
                            {
                                return reader.GetFloat(i + 2);
                            }
                            return (float?)null;
                        }).ToArray();
                        yield return new KeyValuePair<string, float?[]>(key, values);
                    }
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        	//gvdb 2024/04/05
            // add logging
            try
            {
                if (!optionsBuilder.IsConfigured)
                {
#if USE_JET
                string connectionString = AppConfig.ConnectionStrings["IFsHistSeriesDyadic.mdb"].ConnectionString;
                //jrs 2019/03/29
                //if (Environment.CurrentDirectory.Contains("Code.Ifs-Translation"))
                {
                    connectionString = connectionString.Replace("Source=", "Source=../");
                }
                optionsBuilder.UseJet(connectionString);
#else
                    string connectionString = AppConfig.ConnectionStrings["IFsHistSeriesDyadic"].ConnectionString;
                    //jrs 2019/03/29
                    //if (Environment.CurrentDirectory.Contains("Code.Ifs-Translation"))
                    {
                        connectionString = connectionString.Replace("Source=", "Source=../");
                    }
                    optionsBuilder.UseSqlite(connectionString);
#endif
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnConfiguring of IFsHistSeriesDyadicContext");
                throw;
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        	//gvdb 2024/04/05
            // add logging
            try
            {
                modelBuilder.Entity<SeriesExportsDOTSDyadic>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesExportsAg>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesExportsEn>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesExportsICT>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesExportsMan>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesExportsMat>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesExportsServ>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesDistanceCEPII>().HasKey(r => new { r.Actor, r.Partner });

                modelBuilder.Entity<SeriesWtTariffAgri>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesWtTariffEner>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesWtTariffICT>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesWtTariffManu>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesWtTariffMate>().HasKey(r => new { r.Actor, r.Partner });

                //jrs 2025/07/29
                modelBuilder.Entity<SeriesWtTradeElasticitiesAgri>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesWtTradeElasticitiesEner>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesWtTradeElasticitiesICT>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesWtTradeElasticitiesManu>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesWtTradeElasticitiesMate>().HasKey(r => new { r.Actor, r.Partner });

                modelBuilder.Entity<SeriesMilitaryExportsTotal>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesFDISent>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesAIDSent>().HasKey(r => new { r.Actor, r.Partner });

                modelBuilder.Entity<SeriesMigrationFlowOut>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesMigrationFlowOutDL>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesMigrationForcedOut>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesMigrationForcedReturn>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesMigrationForcedStock>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesMigTotal>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesMigTotal_UNPD2024>().HasKey(r => new { r.Actor, r.Partner });

                modelBuilder.Entity<SeriesDiploEmbassyOutDyad>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesDiploIGOSharedWeighted>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesDiploTreatySharedWeighted>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesDiploAllianceSharedWeighted_ATOP>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesBandwidth>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesDependence>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesInfluence>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesMIDsEventBinaryMovAvg>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesMIDsEventBinary>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesArmsExportsAtoBStock>().HasKey(r => new { r.Actor, r.Partner });

                modelBuilder.Entity<SeriesExportsGoods>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesUNCTADTariffRevAgri>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesUNCTADTariffRevEner>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesUNCTADTariffRevICT>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesUNCTADTariffRevManu>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesUNCTADTariffRevMate>().HasKey(r => new { r.Actor, r.Partner });

                modelBuilder.Entity<SeriesTCIAgri>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesTCIEner>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesTCIManu>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesTCIMate>().HasKey(r => new { r.Actor, r.Partner });
                modelBuilder.Entity<SeriesTCIICT>().HasKey(r => new { r.Actor, r.Partner });

                modelBuilder.Entity<SeriesTradeIndex>().HasKey(r => new { r.Actor, r.Partner });

                modelBuilder.Entity<SeriesConflictYears>().HasKey(r => new { r.Actor, r.Partner }); // clp 2024/07/10

                modelBuilder.Entity<SeriesConflictYears_UCDP>().HasKey(r => new { r.Actor, r.Partner }); // clp 2024/06/04

                modelBuilder.Entity<SeriesConflictYears_MIEs>().HasKey(r => new { r.Actor, r.Partner }); // clp 2025/06/04

                modelBuilder.Entity<SeriesRivalry>().HasKey(r => new { r.Actor, r.Partner }); // clp 2024/12/20 //SeriesDyadLeaderTripsIn

                modelBuilder.Entity<SeriesDyadLeaderTripsIn>().HasKey(r => new { r.Actor, r.Partner }); // clp 2024/12/20 //SeriesDyadLeaderTripsIn

                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnModelCreating of IFsHistSeriesDyadicContext");
                throw;
            }

        }

    }
}
