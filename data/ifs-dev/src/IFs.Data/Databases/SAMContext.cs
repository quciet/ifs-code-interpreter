using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Data.Sqlite;
using NLog;

namespace IFs.Data
{
    internal partial class SAMContext : DbContext
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        //jrs 2020/04/03
        private String _sWorkingFileName;

        public SAMContext(string WorkingFileName)
        {
            _sWorkingFileName = WorkingFileName;
        }

        public SAMContext()
        { }

        public SAMContext(DbContextOptions<IFsContext> options)
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
                    string connectionString = AppConfig.ConnectionStrings["SAM"].ConnectionString;
                    //jrs 2019/03/29
                    //if (Environment.CurrentDirectory.Contains("Code.Ifs-Translation"))
                    {
                        connectionString = connectionString.Replace("Source=", "Source=../");
                    }
                    //jrs 2020/04/03
                    if (_sWorkingFileName != null && _sWorkingFileName != "")
                    { connectionString = connectionString.Replace("SAMWorking.db", _sWorkingFileName); }

                    optionsBuilder.UseSqlite(connectionString);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnConfiguring of SAMContext");
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        	//gvdb 2024/04/05
        	//add logging
            try
            {
                modelBuilder.Entity<AX>().HasKey(r => new { r.Year, r.Region, r.Row, r.Column });

                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnModelCreating of SAMContext");
                throw;
            }
        }

        private SqliteConnection conn
        {
            get
            {
                return this.Database.GetDbConnection() as SqliteConnection;
            }
        }

        public void BulkInsertAX(Single[,,,] bulk, int NR, int NSec, int FirstYear, int RunYears)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                        stmt.CommandText = @"INSERT INTO [AX] (
                        [Year],
                        [Region],
                        [row],
                        [column],
                        [Value]
                    ) VALUES (
                        $Year,
                        $Region,
                        $row,
                        $column,
                        $Value
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Region", SqliteType.Integer);
                    stmt.AddParameter("row", SqliteType.Integer);
                    stmt.AddParameter("column", SqliteType.Integer);
                    stmt.AddParameter("Value", SqliteType.Real);
                    stmt.Prepare();
                    for (int iYr = 1; iYr <= RunYears; iYr++)
                    {
                        for (int R = 1; R <= NR; R++)
                        {
                            for (int row = 1; row <= NSec; row++)
                            {
                                for (int col = 1; col <= NSec; col++)
                                {
                                    stmt.Parameters[0].Value = iYr + FirstYear - 1;
                                    stmt.Parameters[1].Value = R;
                                    stmt.Parameters[2].Value = row;
                                    stmt.Parameters[3].Value = col;
                                    stmt.Parameters[4].Value = bulk[iYr, R, row, col];
                                    stmt.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

    }
}
