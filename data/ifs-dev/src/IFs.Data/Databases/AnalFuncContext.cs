using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NLog;

#if USE_JET
using EntityFrameworkCore.Jet;
#endif

namespace IFs.Data
{
    internal partial class AnalFuncContext : DbContext
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public AnalFuncContext()
        {}

        public AnalFuncContext(DbContextOptions<AnalFuncContext> options)
            : base(options)
        {}

        public IEnumerable<IRegression> All
            => this.RegressionData;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        	//gvdb 2024/04/05
            // add logging
            try
            {
                if (!optionsBuilder.IsConfigured)
                {
#if USE_JET
                string connectionString = AppConfig.ConnectionStrings["AnalFunc.mdb"].ConnectionString;
                //jrs 2019/03/29
                //if (Environment.CurrentDirectory.Contains("Code.Ifs-Translation"))
                {
                    connectionString = connectionString.Replace("Source=", "Source=../");
                }
                optionsBuilder.UseJet(connectionString);
#else
                    string connectionString = AppConfig.ConnectionStrings["AnalFunc"].ConnectionString;
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
                _logger.Error(ex, "Error during OnConfiguring of AnalFuncContext");
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        	//gvdb 2024/04/05
            // add logging
            try { 
                modelBuilder.Entity<PartialRegression>().HasKey(v => new { v.Name, v.InputName });
                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnModelCreating of AnalFuncContext");
                throw;
            }
        }
    }
}
