using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using Microsoft.Data.Sqlite;
using NLog;

namespace IFs.Data
{
    internal partial class IFsForDyadicContext : DbContext
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public IFsForDyadicContext()
        { }

        public IFsForDyadicContext(DbContextOptions<IFsContext> options)
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
                    string connectionString = AppConfig.ConnectionStrings["IFsForDyadic"].ConnectionString;
                    //jrs 2019/03/29
                    //if (Environment.CurrentDirectory.Contains("Code.Ifs-Translation"))
                    {
                        connectionString = connectionString.Replace("Source=", "Source=../");
                    }
                    optionsBuilder.UseSqlite(connectionString);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnConfiguring of IFsForDyadicContext");
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<XDYAD>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<XDYADAGR>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<XDYADENE>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<XDYADMAT>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<XDYADMAN>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<XDYADICT>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<XDYADSER>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                modelBuilder.Entity<XDYADMIL>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<XDYADFDI>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<XDYADAID>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<XDYADMIGRANT>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<XDYADLOR>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                modelBuilder.Entity<DYADMIGRANTFORCED>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADMIGRANTFORCEDRETURN>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADPOPFOREIGNFORCED>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADMIGFORCEDTARGETPCTPOP>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADMIGVOLTARGETPCTPOP>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                modelBuilder.Entity<MDYAD>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<MDYADAGR>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<MDYADENE>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<MDYADMAT>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<MDYADMAN>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<MDYADICT>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<MDYADSER>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                modelBuilder.Entity<MDYADMIL>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<MDYADFDI>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<MDYADAID>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<MDYADMIGRANT>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<MDYADLOR>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                modelBuilder.Entity<Distance>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADALLIANCE>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADIGO>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTREATY>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADBANDWIDTH>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADDEPENDENCE>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADINFLUENCE>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADPOPFOREIGN>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADMILMSTOCK>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                modelBuilder.Entity<DYADTARIFFRATEAGR>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTARIFFRATEENE>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTARIFFRATEICT>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTARIFFRATEMAN>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTARIFFRATEMAT>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTARIFFRATESER>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                //jrs 2025/07/29
                modelBuilder.Entity<DYADTARIFFRATEAGRWGEL>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTARIFFRATEENEWGEL>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTARIFFRATEICTWGEL>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTARIFFRATEMANWGEL>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTARIFFRATEMATWGEL>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                modelBuilder.Entity<DYADTARIFFREVAGR>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTARIFFREVENE>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTARIFFREVICT>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTARIFFREVMAN>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTARIFFREVMAT>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTARIFFREVSER>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                modelBuilder.Entity<DYADTRADECOMPLEMENTAGR>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTRADECOMPLEMENTENE>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTRADECOMPLEMENTICT>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTRADECOMPLEMENTMAN>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTRADECOMPLEMENTMAT>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADTRADECOMPLEMENTSER>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                //jrs 2022/01/04
                modelBuilder.Entity<DYADTRADEAGREEMENTINDEX>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                //ttt
                modelBuilder.Entity<DYADMIGRANTVOL>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADPOPFOREIGNVOL>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                modelBuilder.Entity<ParametersDefault>().HasKey(r => new { r.ParameterName });
                modelBuilder.Entity<ParametersDetail>().HasKey(r => new { r.ParameterName, r.Actor, r.Partner, r.Sector, r.Year });

                //jrs 2020/07/09
                modelBuilder.Entity<DYADINF_AID_PCTGDP>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADINF_AID_PCTTOTALAID>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADINF_ARMS_PCTGDSMIL>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADINF_ARMS_PCTMONADICARMS>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADINF_ARMSBANDWIDTH>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADINF_LOR_AVG>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADINF_TOTALTRADE_PCTGDP>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADINF_TOTALTRADE_PCTMONADICTRADE>().HasKey(r => new { r.Actor, r.Partner, r.Year });
                modelBuilder.Entity<DYADINF_TRADEBANDWIDTH>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                // clp 2024/07/10
                modelBuilder.Entity<CONFLICTYEARS>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                // clp 2024/07/10
                modelBuilder.Entity<CONFLICTYEARS_MIES>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                // clp 2024/07/10
                modelBuilder.Entity<CONFLICTYEARS_UCDP>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                // clp 2024/07/10
                modelBuilder.Entity<RIVALRY>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                // clp 2025/10/30
                modelBuilder.Entity<DYDLDRTRIP>().HasKey(r => new { r.Actor, r.Partner, r.Year });

                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnModelCreating of IFsForDyadicContext");
                throw;
            }

        }

        public void Clear(Type modelType)
        {
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

        public IFsForDyadicContext Enter()
        {
            var conn = this.Database.GetDbConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "PRAGMA foreign_keys = OFF; PRAGMA ignore_check_constraints = ON; PRAGMA journal_mode = MEMORY; PRAGMA synchronous = OFF;";
                cmd.ExecuteNonQuery();
            }
            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.Database.AutoTransactionsEnabled = false;
            this.Database.BeginTransaction();
            return this;
        }

        public void Exit()
        {
            if (this.Database.CurrentTransaction != null)
            {
                this.Database.CommitTransaction();
            }
            this.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        private SqliteConnection conn
        {
            get
            {
                return this.Database.GetDbConnection() as SqliteConnection;
            }
        }

        public void BulkInsertParametersDetail(Single[,] bulk, int[] RegIdx, int[]RegIdx2, int ActorCount, int PartnerCount, int TimeId, string VarName, int Dim3Idx)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [ParametersDetail] (
                        [ParameterName],
                        [Year],
                        [Actor],
                        [Partner],
                        [Sector],
                        [ParameterValue]
                    ) VALUES (
                        $ParameterName,
                        $Year,
                        $Actor,
                        $Partner,
                        $Sector,
                        $ParameterValue
                    );";
                    stmt.AddParameter("ParameterName", SqliteType.Integer);
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Sector", SqliteType.Integer);
                    stmt.AddParameter("ParameterValue", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= ActorCount; R++)
                    {
                        for (int i = 1; i <= PartnerCount; i++)
                        {
                            stmt.Parameters[0].Value = VarName;
                            stmt.Parameters[1].Value = TimeId;
                            stmt.Parameters[2].Value = RegIdx[R];
                            stmt.Parameters[3].Value = RegIdx2[i];
                            stmt.Parameters[4].Value = Dim3Idx;
                            stmt.Parameters[5].Value = bulk[R,i];
                            stmt.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void SingleInsertParametersDetail(Single val, int RegIdx, int RegIdx2, int TimeId, string VarName, int Dim3Idx)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [ParametersDetail] (
                        [ParameterName],
                        [Year],
                        [Actor],
                        [Partner],
                        [Sector],
                        [ParameterValue]
                    ) VALUES (
                        $ParameterName,
                        $Year,
                        $Actor,
                        $Partner,
                        $Sector,
                        $ParameterValue
                    );";
                    stmt.AddParameter("ParameterName", SqliteType.Integer);
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Sector", SqliteType.Integer);
                    stmt.AddParameter("ParameterValue", SqliteType.Real);
                    stmt.Prepare();

                    stmt.Parameters[0].Value = VarName;
                    stmt.Parameters[1].Value = TimeId;
                    stmt.Parameters[2].Value = RegIdx;
                    stmt.Parameters[3].Value = RegIdx2;
                    stmt.Parameters[4].Value = Dim3Idx;
                    stmt.Parameters[5].Value = val;
                    stmt.ExecuteNonQuery();

                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYAD(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYAD] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 0] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 0];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYAD(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYAD] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYAD(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYAD] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 0] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i, 0];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYAD(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYAD] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADAGR(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADAGR] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 1] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 1];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADAGR(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADAGR] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADAGR(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADAGR] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 1] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i, 1];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADAGR(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADAGR] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADENE(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADENE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 2] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 2];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADENE(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADENE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADENE(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADENE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 2] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i, 2];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADENE(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADENE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADMAT(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADMAT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 3] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 3];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADMAT(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADMAT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADMAT(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADMAT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 3] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i, 3];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADMAT(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADMAT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADMAN(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADMAN] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 4] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 4];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADMAN(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADMAN] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADMAN(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADMAN] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 4] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i, 4];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADMAN(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADMAN] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADSER(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADSER] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 5] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 5];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADSER(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADSER] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADSER(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADSER] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 5] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i, 5];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADSER(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADSER] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADICT(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADICT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 6] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 6];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADICT(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADICT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADICT(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADICT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 6] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i, 6];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADICT(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADICT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFRATEAGR(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEAGR] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 1] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 1];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFRATEAGR(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEAGR] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        //jrs 2025/07/29
        public void BulkInsertDYADTARIFFRATEAGRWGEL(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEAGRWGEL] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFREVAGR(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFREVAGR] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 1] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 1];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFRATEENE(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEENE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 2] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 2];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFRATEENE(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEENE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        //jrs 2025/07/29
        public void BulkInsertDYADTARIFFRATEENEWGEL(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEENEWGEL] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFREVENE(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFREVENE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 2] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 2];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFRATEMAT(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEMAT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 3] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 3];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFRATEMAT(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEMAT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        //jrs 2025/07/29
        public void BulkInsertDYADTARIFFRATEMATWGEL(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEMATWGEL] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFREVMAT(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFREVMAT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 3] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 3];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFRATEMAN(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEMAN] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 4] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 4];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFRATEMAN(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEMAN] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        //jrs 2025/07/29
        public void BulkInsertDYADTARIFFRATEMANWGEL(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEMANWGEL] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFREVMAN(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFREVMAN] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 4] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 4];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFRATESER(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATESER] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 5] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 5];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFRATESER(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATESER] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFREVSER(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFREVSER] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 5] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 5];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFRATEICT(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEICT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 6] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 6];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFRATEICT(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEICT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        //jrs 2025/07/29
        public void BulkInsertDYADTARIFFRATEICTWGEL(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFRATEICTWGEL] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTARIFFREVICT(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTARIFFREVICT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 6] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 6];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTRADECOMPLEMENTAGR(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTRADECOMPLEMENTAGR] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 1] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 1];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTRADECOMPLEMENTAGR(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTRADECOMPLEMENTAGR] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTRADECOMPLEMENTENE(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTRADECOMPLEMENTENE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 2] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 2];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTRADECOMPLEMENTENE(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTRADECOMPLEMENTENE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTRADECOMPLEMENTMAT(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTRADECOMPLEMENTMAT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 3] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 3];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTRADECOMPLEMENTMAT(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTRADECOMPLEMENTMAT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTRADECOMPLEMENTMAN(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTRADECOMPLEMENTMAN] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 4] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 4];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTRADECOMPLEMENTMAN(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTRADECOMPLEMENTMAN] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTRADECOMPLEMENTSER(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTRADECOMPLEMENTSER] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 5] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 5];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTRADECOMPLEMENTSER(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTRADECOMPLEMENTSER] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        //jrs 2022/01/03
        public void BulkInsertDYADTRADEAGREEMENTINDEX(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTRADEAGREEMENTINDEX] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        //jrs 2022/01/03
        public void BulkInsertDYADTRADEAGREEMENTINDEX(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTRADEAGREEMENTINDEX] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            stmt.Parameters[0].Value = TimeId;
                            stmt.Parameters[1].Value = R;
                            stmt.Parameters[2].Value = i;
                            stmt.Parameters[3].Value = bulk[R, i];
                            stmt.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTRADECOMPLEMENTICT(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTRADECOMPLEMENTICT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 6] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 6];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTRADECOMPLEMENTICT(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTRADECOMPLEMENTICT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADMIL(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADMIL] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 1] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 1];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADMIL(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADMIL] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADMIL(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADMIL] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 1] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i, 1];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADMIL(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADMIL] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADFDI(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADFDI] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 2] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 2];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADFDI(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADFDI] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADFDI(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADFDI] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 2] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i, 2];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADFDI(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADFDI] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADAID(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADAID] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 3] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 3];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADAID(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADAID] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADAID(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADAID] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 3] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i, 3];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADAID(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADAID] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADLOR(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADLOR] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 5] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 5];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADLOR(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADLOR] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADLOR(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADLOR] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 5] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i, 5];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADLOR(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADLOR] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADALLIANCE(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADALLIANCE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADALLIANCE(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADALLIANCE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADIGO(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADIGO] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADIGO(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADIGO] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTREATY(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTREATY] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADTREATY(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADTREATY] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADMILMSTOCK(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADMILMSTOCK] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADMILMSTOCK(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADMILMSTOCK] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADDEPENDENCE(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADDEPENDENCE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADDEPENDENCE(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADDEPENDENCE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADBANDWIDTH(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADBANDWIDTH] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADBANDWIDTH(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADBANDWIDTH] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADINFLUENCE(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADINFLUENCE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADINFLUENCE(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADINFLUENCE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        //jrs 2020/07/09 *** begin ***
        public void BulkInsertDYADINF_AID_PCTGDP(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADINF_AID_PCTGDP] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 1] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 1];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADINF_AID_PCTTOTALAID(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADINF_AID_PCTTOTALAID] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i,1] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i,1];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADINF_ARMS_PCTGDSMIL(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADINF_ARMS_PCTGDSMIL] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i,1] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i,1];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADINF_ARMS_PCTMONADICARMS(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADINF_ARMS_PCTMONADICARMS] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i,1] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i,1];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADINF_ARMSBANDWIDTH(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADINF_ARMSBANDWIDTH] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (Math.Exp(bulk[R, i,1]) > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = Math.Exp(bulk[R, i,1]);
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADINF_LOR_AVG(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADINF_LOR_AVG] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i, 1] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i, 1];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADINF_TOTALTRADE_PCTGDP(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADINF_TOTALTRADE_PCTGDP] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i,1] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i,1];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADINF_TOTALTRADE_PCTMONADICTRADE(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADINF_TOTALTRADE_PCTMONADICTRADE] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i,1] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i,1];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADINF_TRADEBANDWIDTH(Single[,,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADINF_TRADEBANDWIDTH] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (Math.Exp(bulk[R, i,1]) > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = Math.Exp(bulk[R, i,1]);
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }
        //jrs 2020/07/09 *** end ****

        public void BulkInsertXDYADMIGRANT(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADMIGRANT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertXDYADMIGRANT(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [XDYADMIGRANT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADMIGRANT(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADMIGRANT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMDYADMIGRANT(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [MDYADMIGRANT] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = i;
                                stmt.Parameters[2].Value = R;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADMIGRANTFORCED(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADMIGRANTFORCED] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADMIGRANTFORCED(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADMIGRANTFORCED] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADMIGRANTFORCEDRETURN(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADMIGRANTFORCEDRETURN] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADMIGRANTFORCEDRETURN(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADMIGRANTFORCEDRETURN] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADMIGRANTVOL(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADMIGRANTVOL] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADPOPFOREIGN(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADPOPFOREIGN] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADPOPFOREIGN(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADPOPFOREIGN] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkUpdateDYADPOPFOREIGN(Single[,] bulk, int TimeId, int NR, Boolean[,] bulk2)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"UPDATE [DYADPOPFOREIGN] SET
                        [Val] = $Val
                        WHERE 
                        [Year] = $Year
                        AND [Actor] = $Actor
                        AND [Partner] = $Partner;";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    int cnt = 0;
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk2[R, i])
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                                cnt++;
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }


        public void BulkInsertDYADPOPFOREIGNVOL(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADPOPFOREIGNVOL] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADPOPFOREIGNFORCED(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADPOPFOREIGNFORCED] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADPOPFOREIGNFORCED(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADPOPFOREIGNFORCED] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADMIGFORCEDTARGETPCTPOP(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADMIGFORCEDTARGETPCTPOP] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDYADMIGVOLTARGETPCTPOP(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADMIGVOLTARGETPCTPOP] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertDistance(Single?[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [Distance] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i].HasValue)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        //jrs 2022/04/20
        public void BulkInsertPOLITYAFFINITY(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYADPOLITYAFFINITY] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        // clp 2024/07/10
        public void BulkInsertCONFLICTYEARS(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [CONFLICTYEARS] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertCONFLICTYEARS_UCDP(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [CONFLICTYEARS_UCDP] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertCONFLICTYEARS_MIES(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [CONFLICTYEARS_MIES] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }

        // 'clp 2024/12/20
        public void BulkInsertRIVALRY(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [RIVALRY] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }
        // 'clp 2024/12/20
        // 'clp 2025/10/30
        public void BulkInsertDYDLDRTRIP(Single[,] bulk, int TimeId, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [DYDLDRTRIP] (
                        [Year],
                        [Actor],
                        [Partner],
                        [Val]
                    ) VALUES (
                        $Year,
                        $Actor,
                        $Partner,
                        $Val
                    );";
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Actor", SqliteType.Integer);
                    stmt.AddParameter("Partner", SqliteType.Integer);
                    stmt.AddParameter("Val", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= NR; i++)
                        {
                            if (bulk[R, i] > 0)
                            {
                                stmt.Parameters[0].Value = TimeId;
                                stmt.Parameters[1].Value = R;
                                stmt.Parameters[2].Value = i;
                                stmt.Parameters[3].Value = bulk[R, i];
                                stmt.ExecuteNonQuery();
                            }
                        }
                    }
                    tx.Commit();
                }
            }
        }
        // 'clp 2025/10/30
    }
}


