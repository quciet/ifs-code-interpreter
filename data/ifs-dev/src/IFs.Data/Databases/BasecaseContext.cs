using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace IFs.Data
{
    internal partial class BasecaseContext : DbContext, IBasecase
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        //2020/04/03 added sFileName
        // create / update the skeleton of output with schema and metadata
        public static IBasecase Open(bool truncate, IContext source, string sFileName) {
            Debug.Indent();
            var db = new BasecaseContext(sFileName);
            if (truncate) {
                Truncate(db, source);
            } else {

                //jrs 2019/09/18 Migrate(db);
            }
            Debug.Unindent();
            return db.Enter();
        }

        private static void Truncate(BasecaseContext db, IContext source) {
            Debug.WriteLine("constructing database");
            db.Database.EnsureDeleted();
            db.Database.Migrate();
            // initialize with metadata
            db.ChangeTracker.AutoDetectChangesEnabled = false;
            Debug.Indent();
            using (var tx = db.Database.BeginTransaction()) {
                // propagate time dimension buckets to base case 
                foreach (var b in source.GetDimension(0).Buckets) {
                    db.TimeBuckets.Add(new TimeBucket() {
                        Seq = b.Key,
                        Name = b.Value,
                    });
                }
                // propagate region dimension buckets to base case
                foreach(var b in source.GetDimension(1).Buckets) {
                    db.RegionBuckets.Add(new RegionBucket() {
                        Seq = b.Key,
                        Name = b.Value,
                    });
                }
                db.Flush();
                tx.Commit();
            }
            Debug.Unindent();
            db.ChangeTracker.AutoDetectChangesEnabled = true;
            Debug.WriteLine("constructed database");
        }

        private static void Migrate(BasecaseContext db) {
            Debug.WriteLine("migrating database");
            db.Database.Migrate();
            // TODO: metadata verification logic
            Debug.WriteLine("migrated database");
        }

        //jrs 2020/04/03
        private String _sWorkingFileName;

        public BasecaseContext(string WorkingFileName)
        {
            _sWorkingFileName = WorkingFileName;
        }

        public BasecaseContext()
        {}

        public BasecaseContext(DbContextOptions<BasecaseContext> options)
            : base(options)
        {}

        private IBasecase Enter() {
            var conn = this.Database.GetDbConnection();
            if (conn.State == ConnectionState.Closed) {
                conn.Open();
            }
            using (var cmd = conn.CreateCommand()) {
                cmd.CommandText = "PRAGMA foreign_keys = OFF; PRAGMA ignore_check_constraints = ON; PRAGMA journal_mode = MEMORY; PRAGMA synchronous = OFF; ";
                cmd.ExecuteNonQuery();
            }
            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.Database.AutoTransactionsEnabled = false;
            this.Database.BeginTransaction();
            return this;
        }

        private void Exit() {
            if (this.Database.CurrentTransaction != null) {
                this.Database.CommitTransaction();
            }
            this.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        public void Flush() {
            this.SaveChanges(true);
        }

        IEnumerable<TimeBucket> IBasecase.TimeBuckets
            => this.TimeBuckets;

        IEnumerable<RegionBucket> IBasecase.RegionBuckets 
            => this.RegionBuckets;

        IEnumerable<CohortBucket> IBasecase.CohortBuckets 
            => this.CohortBuckets;

        IEnumerable<GenderBucket> IBasecase.GenderBuckets 
            => this.GenderBuckets;

        public void Add(PopBasecase record) {
            this.PopBasecase.Add(record);
        }

        //jrs 2019/07/26
        public void Add(FerBasecase record)
        {
            this.FerBasecase.Add(record);
        }

        public void Add(MorBasecase record)
        {
            this.MorBasecase.Add(record);
        }

        public void Add(MorDetBaseCase record)
        {
            this.MorDetBaseCase.Add(record);
        }

        public void Add(MorbDetBaseCase record)
        {
            this.MorbDetBaseCase.Add(record);
        }

        public void Add(MorDetFactors record)
        {
            this.MorDetFactors.Add(record);
        }

        public void Add(MorDetNormFactors record)
        {
            this.MorDetNormFactors.Add(record);
        }

        public void Add(StuntingByAge record)
        {
            this.StuntingByAge.Add(record);
        }
        
        public void Add(EdnGradsBaseCase record)
        {
            this.EdnGradsBaseCase.Add(record);
        }

        public void Add(EdnYears25BaseCase record)
        {
            this.EdnYears25BaseCase.Add(record);
        }

        public void Add(EdnYears15BaseCase record)
        {
            this.EdnYears15BaseCase.Add(record);
        }

        public void Add(HLPROXIMATEDRIVERS record)
        {
            this.HLPROXIMATEDRIVERS.Add(record);
        }

        public void Clear(Type modelType) {
        	//gvdb 2024/04/05
            // update for new EF syntax
            var tableName = this.Model.FindEntityType(modelType).GetTableName();
            var conn = this.Database.GetDbConnection();
            if (conn.State == ConnectionState.Closed) {
                conn.Open();
            }
            using (var cmd = conn.CreateCommand()) {
                cmd.CommandText = $"DELETE FROM [{tableName}]";
                cmd.ExecuteNonQuery();
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
                    string connectionString = AppConfig.ConnectionStrings["Basecase"].ConnectionString;
                    //jrs 2019/03/29
                    //if (Environment.CurrentDirectory.Contains("Code.Ifs-Translation"))
                    {
                        connectionString = connectionString.Replace("Source=", "Source=../");
                    }
                    //jrs 2020/04/03
                    if (_sWorkingFileName != null && _sWorkingFileName != "")
                    { connectionString = connectionString.Replace("ifsPopWork.db", _sWorkingFileName); }

                    optionsBuilder.UseSqlite(connectionString /*, x => x.SuppressForeignKeyEnforcement(true)*/);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnConfiguring of BasecaseContext");
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        	//gvdb 2024/04/05
            // add logging
            try
            {
                // time (to be filled by the storage service from context)
                modelBuilder.Entity<TimeBucket>();
                // region (to be filled by the storage service from context)
                modelBuilder.Entity<RegionBucket>();
                // gender (1 = Male; 2 = Female)
                modelBuilder.Entity<GenderBucket>().HasData(
                    new GenderBucket() { Seq = 1, Name = "Male" },
                    new GenderBucket() { Seq = 2, Name = "Female" }
                );
                // age groups
                modelBuilder.Entity<CohortBucket>().HasData(
                    new CohortBucket() { Seq = 0, Name = "infants" },
                    new CohortBucket() { Seq = 1, Name = "1-4" },
                    new CohortBucket() { Seq = 2, Name = "5-9" },
                    new CohortBucket() { Seq = 3, Name = "10-14" },
                    new CohortBucket() { Seq = 4, Name = "15-19" },
                    new CohortBucket() { Seq = 5, Name = "20-24" },
                    new CohortBucket() { Seq = 6, Name = "25-29" },
                    new CohortBucket() { Seq = 7, Name = "30-34" },
                    new CohortBucket() { Seq = 8, Name = "35-39" },
                    new CohortBucket() { Seq = 9, Name = "40-44" },
                    new CohortBucket() { Seq = 10, Name = "45-49" },
                    new CohortBucket() { Seq = 11, Name = "50-54" },
                    new CohortBucket() { Seq = 12, Name = "55-59" },
                    new CohortBucket() { Seq = 13, Name = "60-64" },
                    new CohortBucket() { Seq = 14, Name = "65-69" },
                    new CohortBucket() { Seq = 15, Name = "70-74" },
                    new CohortBucket() { Seq = 16, Name = "75-79" },
                    new CohortBucket() { Seq = 17, Name = "80-84" },
                    new CohortBucket() { Seq = 18, Name = "85-89" },
                    new CohortBucket() { Seq = 19, Name = "90-94" },
                    new CohortBucket() { Seq = 20, Name = "95-99" },
                    new CohortBucket() { Seq = 21, Name = "100+" }
                );
                modelBuilder.Entity<PopBasecase>()
                    .HasKey((v) => new { v.TimeId, v.RegionId, v.CohortId });
                // modelBuilder.Entity<PopBasecase>().HasIndex((v) => v.TimeId);
                // modelBuilder.Entity<PopBasecase>().HasIndex((v) => v.RegionId);
                // modelBuilder.Entity<PopBasecase>().HasIndex((v) => v.CohortId);
                // modelBuilder.Entity<PopBasecase>().HasIndex((v) => v.GenderId);

                //jrs 2019/07/26
                modelBuilder.Entity<FerBasecase>()
                    .HasKey((v) => new { v.TimeId, v.RegionId, v.CohortId });
                modelBuilder.Entity<MorBasecase>()
                    .HasKey((v) => new { v.TimeId, v.RegionId, v.CohortId });
                modelBuilder.Entity<MorDetBaseCase>()
                    .HasKey((v) => new { v.TimeId, v.RegionId, v.CohortId });
                modelBuilder.Entity<MorbDetBaseCase>()
                    .HasKey((v) => new { v.TimeId, v.RegionId, v.CohortId });
                modelBuilder.Entity<MorDetFactors>()
                    .HasKey((v) => new { v.RegionId, v.CohortId });
                modelBuilder.Entity<MorDetNormFactors>()
                    .HasKey((v) => new { v.RegionId, v.CohortId });
                modelBuilder.Entity<StuntingByAge>()
                    .HasKey((v) => new { v.Years, v.NCohorts, v.Region });
                modelBuilder.Entity<EdnGradsBaseCase>()
                    .HasKey((v) => new { v.TimeId, v.RegionId, v.CohortId });
                modelBuilder.Entity<EdnYears25BaseCase>()
                    .HasKey((v) => new { v.TimeId, v.RegionId, v.CohortId });
                modelBuilder.Entity<EdnYears15BaseCase>()
                    .HasKey((v) => new { v.TimeId, v.RegionId, v.CohortId });
                modelBuilder.Entity<HLPROXIMATEDRIVERS>()
                    .HasKey((v) => new { v.VarName, v.Year, v.Region, v.Dim2, v.Dim3 });

                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnModelCreating of BasecaseContext");
                throw;
            }
        }

        public override void Dispose() {
            Exit();
            base.Dispose();
        }

    }

}
