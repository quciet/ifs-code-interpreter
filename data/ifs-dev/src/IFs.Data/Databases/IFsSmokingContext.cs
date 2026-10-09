using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using NLog;

namespace IFs.Data
{
    internal partial class IFsSmokingContext : DbContext
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        //jrs 2020/04/03
        private String _sWorkingFileName;

        public IFsSmokingContext(string WorkingFileName)
        {
            _sWorkingFileName = WorkingFileName;
        }

        public IFsSmokingContext()
        { }

        public IFsSmokingContext(DbContextOptions<IFsSmokingContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        	//gvdb 2024/04/05
            // add logging
            try
            {
                if (!optionsBuilder.IsConfigured)
                {
                    string connectionString = AppConfig.ConnectionStrings["IFsSmoking"].ConnectionString;
                    //jrs 2019/03/29
                    //if (Environment.CurrentDirectory.Contains("Code.Ifs-Translation"))
                    {
                        connectionString = connectionString.Replace("Source=", "Source=../");
                    }
                    //jrs 2020/04/03
                    if (_sWorkingFileName != null && _sWorkingFileName != "")
                    { connectionString = connectionString.Replace("IFsSmoking.db", _sWorkingFileName); }

                    optionsBuilder.UseSqlite(connectionString);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnConfiguring of IFsSmokingContext");
                throw;
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        	//gvdb 2024/04/05
            // add logging
            try
            {
                modelBuilder.Entity<SmokingRateEstimatesReg>().HasKey(r => new { r.RegionId, r.Year, r.Sex });

                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnModelCreating of IFsSmokingContext");
                throw;
            }
        }

        public void Clear(Type modelType)
        {
        	//gvdb 2024/04/05
        	// update for new EF syntax
            var tableName = this.Model.FindEntityType(modelType).GetTableName();
            var conn = this.Database.GetDbConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM [{tableName}]";
                cmd.ExecuteNonQuery();
            }
        }

        public void Flush()
        {
            this.SaveChanges(true);
        }
    }
}
