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

    internal partial class IFsHistSeriesContext : DbContext
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();


        public IFsHistSeriesContext()
        {}

        public IFsHistSeriesContext(DbContextOptions<IFsHistSeriesContext> options)
            : base(options)
        {}

        //jrs 2024/10/14
        private String _sWorkingFileName;

        //jrs 2024/10/14
        public IFsHistSeriesContext(string WorkingFileName)
        {
            _sWorkingFileName = WorkingFileName;
        }

        public IEnumerable<short> GetYears(string seriesName) {
            var modelType = this.GetModelType(seriesName);
            // return empty list instead of throwing an error when the entity type does not exist
            if (modelType == null) {
                Debug.WriteLine($"Entity '{seriesName}' does not exist.");
                return Enumerable.Empty<short>();
            }
            return (from col in this.FindFields(modelType)
                where col.All(char.IsDigit)
                select short.Parse(col));
        }

        // enumerate <CountryName, Value> pairs for the specified field of the series entity type
        public IEnumerable<KeyValuePair<string, float?>> SliceSeries(string seriesName, string fieldName) {
            var modelType = this.GetModelType(seriesName);
            // return empty list instead of throwing an error when the entity type does not exist
            if (modelType == null) {
                Debug.WriteLine($"Entity '{seriesName}' does not exist.");
                yield break;
            }
            // get low-level database connection
            var conn = this.Database.GetDbConnection();
            if (conn.State == ConnectionState.Closed) {
                conn.Open();
            }
            using (var cmd = conn.CreateCommand()) {
                // if the table does not contain the specified field, query a list of null values so that the slice still has
                // the full list of countries this would simplify the implementation of high-level coalescing policies.
                //gvdb 2024/04/05
                // update for new EF syntax
                var tableName = this.Model.FindEntityType(modelType).GetTableName();
                if (this.FindFields(modelType).Contains(fieldName)) {
                    cmd.CommandText = $"SELECT [Country] AS [Country], [{fieldName}] AS [Value] FROM [{tableName}]";
                } else {
                    Debug.WriteLine($"Field '{tableName}.{fieldName}' does not exist.");
                    cmd.CommandText = $"SELECT [Country] AS [Country], NULL AS [Value] FROM [{tableName}]";
                }
                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        string key = reader.GetString(0);
                        float? value = null;
                        if (!reader.IsDBNull(1)) {
                            value = reader.GetFloat(1);
                        }
                        yield return new KeyValuePair<string, float?>(key, value);
                    }
                }
            }
        }

        //
        public IEnumerable<KeyValuePair<string, float?[]>> DiceSeries(string seriesName, params string[] fieldNames) {
            var modelType = this.GetModelType(seriesName);
            // return empty list instead of throwing an error when the entity type does not exist
            if (modelType == null) {
                Debug.WriteLine($"Entity '{seriesName}' does not exist.");
                yield break;
            }
            // get low-level database connection
            var conn = this.Database.GetDbConnection();
            if (conn.State == ConnectionState.Closed) {
                conn.Open();
            }
            using (var cmd = conn.CreateCommand()) {
            	 //gvdb 2024/04/05
                 // update for new EF syntax
                var tableName = this.Model.FindEntityType(modelType).GetTableName();
                var fieldList = this.FindFields(modelType);
                var projection = String.Join(", ", fieldNames.Select(f => {
                    if (fieldList.Contains(f)) {
                        return $"[{f}] AS [{f}]";
                    } else {
                        Debug.WriteLine($"Field '{tableName}.{f}' does not exist.");
                        return $"NULL AS [{f}]";
                    }
                }));
                cmd.CommandText = $"SELECT [Country] AS [Country], {projection} FROM [{tableName}]";
                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        string key = reader.GetString(0);
                        float?[] values = fieldNames.Select((f, i) => {
                            if (!reader.IsDBNull(i + 1)) {
                                return reader.GetFloat(i + 1);
                            }
                            return (float?) null;
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
            try {
                if (!optionsBuilder.IsConfigured) {
#if USE_JET
                string connectionString = AppConfig.ConnectionStrings["IFsHistSeries.mdb"].ConnectionString;
                //jrs 2019/03/29
                //if (Environment.CurrentDirectory.Contains("Code.Ifs-Translation"))
                {
                    connectionString = connectionString.Replace("Source=", "Source=../");
                }
                optionsBuilder.UseJet(connectionString);
#else
                    string connectionString = AppConfig.ConnectionStrings["IFsHistSeries"].ConnectionString;

                    //jrs 2024/10/14
                    if (_sWorkingFileName != null && _sWorkingFileName != "")
                    { connectionString = connectionString.Replace("IFsHistSeries.db", _sWorkingFileName); }

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
                _logger.Error(ex, "Error during OnConfiguring of IFsHistSeriesContext");
                throw;
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        	//gvdb 2024/04/05
            // add logging    
            try
            {
                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnModelCreating of IFsHistSeriesContext");
                throw;
            }
        }

    }
}
