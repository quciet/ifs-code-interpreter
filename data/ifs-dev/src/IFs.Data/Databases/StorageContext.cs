using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using NLog;

namespace IFs.Data
{
    internal partial class StorageContext : DbContext, IStorage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        // the storage context is not bound to a specific execution stage
        public ExecutionStage Stage => ExecutionStage.None;

        public StorageContext()
        {}

        public StorageContext(DbContextOptions<StorageContext> options)
            : base(options)
        {}

        //jrs 2019/12/13
        private String _sWorkingFileName;

        //jrs 2019/12/13
        public StorageContext(string WorkingFileName)
        {
            _sWorkingFileName = WorkingFileName;
        }

        internal IStorage Enter() {
            var conn = this.Database.GetDbConnection();
            if (conn.State == ConnectionState.Closed) {
                conn.Open();
            }
            using (var cmd = conn.CreateCommand()) {
                //jrs 2024/10/11 this was producing mal disk error on second run in big batch
                //cmd.CommandText = "PRAGMA journal_mode = WAL; PRAGMA wal_autocheckpoint = 0;";
                cmd.CommandText = "PRAGMA foreign_keys = OFF; PRAGMA ignore_check_constraints = ON; PRAGMA journal_mode = MEMORY; PRAGMA synchronous = OFF;";
                cmd.ExecuteNonQuery();
            }
            this.ChangeTracker.AutoDetectChangesEnabled = false;
            //this.Database.AutoTransactionsEnabled = false;
            //gvdb 2024/04/05
        	// update for new EF syntax
            this.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
            //jrs 2019/07/22 from LIU
            //this.RegressionData.Include(reg => reg.Coefficients).ToList();

            return this;
        }

        internal void Exit() {
            this.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        public void Flush() {
            this.SaveChanges(true);
            var conn = this.Database.GetDbConnection();
            if (conn.State == ConnectionState.Closed) {
                conn.Open();
            }
            using (var cmd = conn.CreateCommand()) {
                //jrs 2024/10/13 to avoid saving into wal backup files that hold big batch, they're still created (there are other conn.open), but not blocking big batch
                //cmd.CommandText = "PRAGMA wal_checkpoint(TRUNCATE);";
                cmd.CommandText = "PRAGMA foreign_keys = OFF; PRAGMA ignore_check_constraints = ON; PRAGMA journal_mode = MEMORY; PRAGMA synchronous = OFF;";
                cmd.ExecuteNonQuery();
            }
        }

        public short FirstYear
            => this.DimensionBucket.Where(db => db.DimensionId == DataExtensions.DIM_TIME).Min(db => db.Seq);

        public short LastYear
            => this.DimensionBucket.Where(db => db.DimensionId == DataExtensions.DIM_TIME).Max(db => db.Seq);

        //2019/09/27
        public string ActiveBase => "";

        //2019/09/27
        public bool HistoricMode => false;

        //2019/09/27
        public string DataFileDir => "";

        //jrs 2019/07/18
        public short BaseYear => 0;
        //jrs 2022/11/18
        public short ConstantDollarsYear => 2011;
        //jrs 2023/01/24
        public short FirstYearHistory => 0;
        //jrs 2023/10/03
        public short NGender => 3;

        //mti 2023/10/15
        public short NPriGr => 8;

        public ICollection<string> VariableNames
            => new HashSet<string>(this.VariableData.Select(v => v.Name));

        public IEnumerable<IVariable> Variables
            => this.VariableData.Include(v => v.Dimensions);

        public IVariable GetVariable(string name) {
            var variable = this.VariableData.Find(name);
            this.Entry(variable).Collection(v => v.Dimensions).Load();
            return variable;
        }

        public IEnumerable<IDimension> Dimensions
            => this.DimensionData.Include(d => d.Buckets);

        public ICollection<int> DimensionIds
            => new HashSet<int>(this.DimensionData.Select(d => d.Id));

        public IDimension GetDimension(int index) {
            var dimension = this.DimensionData.Find(index);
            this.Entry(dimension).Collection(d => d.Buckets).Load();
            return dimension;
        }

        public IEnumerable<IRegression> Regressions
            => this.RegressionData.OrderBy(r => r.Name).ThenBy(r => r.InputName);

        public ICollection<string> RegressionNames
            => new HashSet<string>(this.RegressionData.Select(r => r.Name).Distinct());

        public IEnumerable<IRegression> GetRegression(string name) {
            return this.RegressionData.Where(r => r.Name == name).OrderBy(r => r.InputName);
        }

        //jrs 2019/07/17
        public IEnumerable<IRegressionCoefficients> GetRegressionCoefficients(string name)
        {
            return this.RegressionCoefficients.Where(r => r.RegressionName == name);
        }

        //jrs 2019/07/19
        public IEnumerable<IPoint<double>> GetRegressionPoints(string name)
        {
            return this.RegressionPoints.Where(r => r.RegressionName == name);
        }

        //jrs 2025/05/23 added Bilat field
        internal void PutVariable(IVariable source) {
            this.VariableData.Add(new VariableRun() {
                Name = source.Name,
                DisplayName = source.DisplayName,
                Type = source.Type,
                Bilat = source.Bilat,
                Min = source.Min,
                Max = source.Max,     
                Aggregation = source.Aggregation, 
                CommonTarget = source.CommonTarget,
                Currency = source.Currency,
            });
            this.VariableBlob.Add(new VariableBlob() {
                VariableName = source.Name,
                Data = null
            });
            short dimensionSeq = 0;
            foreach (var dimensionId in source.Dimensions) {
                this.VariableDimension.Add(new VariableDimension() {
                    VariableName =  source.Name,
                    Seq = ++dimensionSeq,
                    DimensionId = dimensionId,
                });
            }
        }

        internal void PutDimension(IDimension source) {
            this.DimensionData.Add(new DimensionRun() {
                Id = source.Id,
                Name = source.Name,
            });
            foreach (var value in source.Buckets) {
                this.DimensionBucket.Add(new DimensionBucket() {
                    DimensionId = source.Id,
                    Seq = value.Key,
                    Name = value.Value,
                });
            }
        }

        internal void PutRegression(IRegression source) {
            // count number of existing regressions under the same name
            short seq = (short) (this.RegressionData.Local.Count(r => r.Name == source.Name) + 1);
            // save regression
            this.RegressionData.Add(new RegressionRun() {
                Name = source.Name,
                Seq = seq,
                InputName = source.InputName,
                OutputName = source.OutputName,
                StdError = source.StdError,
            });
            // attach coefficients to regression
            foreach (var c in source.Coefficients) {
                this.RegressionCoefficients.Add(new RegressionCoefficient() {
                    RegressionName = source.Name,
                    RegressionSeq = seq,
                    Name = c.Key,
                    Value = c.Value,
                });
            }
            // attach points to regression
            short pointSeq = 0;
            foreach (IPoint<double> p in source.Points) {
                this.RegressionPoints.Add(new RegressionPoint() {
                    RegressionName = source.Name,
                    RegressionSeq = seq,
                    Seq = ++pointSeq,
                    X = p.X,
                    Y = p.Y,
                });
            }
        }

        public IQueryBuilder Query(string variableName) {
            return StorageQueryBuilder.Create(variableName, this);
        }

        public Stream GetVariableStream(string variableName) {
            var conn = this.Database.GetDbConnection() as SqliteConnection;
            if (conn.State == ConnectionState.Closed) {
                conn.Open();
            }
            return VariableStream.ForRead(conn, variableName);
        }

        public void PutVariableStream(string variableName, Stream payload) {
            var conn = this.Database.GetDbConnection() as SqliteConnection;
            if (conn.State == ConnectionState.Closed) {
                conn.Open();
            }
            if (payload == null) {
                VariableStream.Nullify(conn, variableName);
            } else if (payload.Length == 0) {
                VariableStream.Nullify(conn, variableName);
            } else {
                using (var stream = VariableStream.ForWrite(conn, variableName, payload.Length)) {
                    payload.CopyTo(stream);
                }
            }
        }

        public byte[] GetVariableData(string variableName) {
            VariableBlob blob = this.VariableBlob.Find(variableName);
            if (blob == null) {
                return null;
            }
            return blob.Data;
        }

        public void PutVariableData(string variableName, byte[] payload) {
            if ((payload != null) && (payload.Length == 0)) {
                payload = null;
            }
            VariableBlob blob = this.VariableBlob.Find(variableName);
            if (blob == null) {
                blob = new VariableBlob() {
                    VariableName = variableName,
                    Data = payload
                };
                this.VariableBlob.Add(blob);
            } else {
                blob.Data = payload;
                this.VariableBlob.Update(blob);
            }
        }

        public IEnumerable<KeyValuePair<string, byte[]>> GetAllVariableData() {
            foreach (var vb in this.VariableBlob) {
                yield return new KeyValuePair<string, byte[]>(vb.VariableName, vb.Data);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //jrs 2019/12/13 
            //string sFileName;
            //if (_sWorkingFileName == null)
            //{ sFileName = "Storage"; }
            //else
            //{ sFileName = _sWorkingFileName; }
            //gvdb 2024/04/05
        	// add logging
            try
            {
                if (!optionsBuilder.IsConfigured)
                {
                    string connectionString = AppConfig.ConnectionStrings["Storage"].ConnectionString;

                    //jrs 2020/01/03
                    if (_sWorkingFileName != null && _sWorkingFileName != "")
                    { connectionString = connectionString.Replace("Working.run.db", _sWorkingFileName); }

                    //jrs 2019/12/30 added || net471
                    //jrs 2019/03/29
                    if (Environment.CurrentDirectory.Contains("Code.Ifs-Translation") || Environment.CurrentDirectory.Contains("net471") || Environment.CurrentDirectory.Contains("net8"))
                    {
                        connectionString = connectionString.Replace("Source=", "Source=../");
                    }
                    else
                    {
                        //jrs 2019/07/08 needed for web GUI
                        connectionString = connectionString.Replace("Source=", "Source=" + Environment.CurrentDirectory.ToString() + "/");
                    }

                    optionsBuilder.UseSqlite(connectionString);
                    optionsBuilder.EnableSensitiveDataLogging(true);
                    // optionsBuilder.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnConfiguring of StorageContext");
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        	//gvdb 2024/04/05
        	// add logging
            try
            {
                // note: composite primary keys should place columns in alphabetical order, otherwise EF Core will generate a
                // migration rule for creating an extra unique constraint, yet SQLite EF Core provider doesn't support this.
                modelBuilder.Entity<VariableRun>().HasIndex(v => v.Name);
                modelBuilder.Entity<VariableRun>().HasIndex(v => v.Type);
                modelBuilder.Entity<VariableRun>().HasIndex(v => v.Bilat); //jrs 2025/05/23
                modelBuilder.Entity<VariableRun>().HasMany(v => v.Dimensions).WithOne(d => d.Variable);
                modelBuilder.Entity<VariableBlob>().HasKey(b => b.VariableName);
                modelBuilder.Entity<VariableBlob>().HasOne(b => b.Variable).WithOne(v => v.Blob).HasForeignKey("VariableBlob");
                modelBuilder.Entity<VariableDimension>().HasKey(vd => new { vd.Seq, vd.VariableName });
                modelBuilder.Entity<DimensionRun>().HasIndex(d => d.Id);
                modelBuilder.Entity<DimensionRun>().HasMany(d => d.Variables);
                modelBuilder.Entity<DimensionRun>().HasMany(d => d.Buckets).WithOne(b => b.Dimension).HasForeignKey("DimensionId");
                modelBuilder.Entity<DimensionBucket>().HasKey(x => new { x.DimensionId, x.Seq });
                modelBuilder.Entity<DimensionBucket>().HasIndex(x => x.Name);
                modelBuilder.Entity<RegressionRun>().HasKey(r => new { r.Name, r.Seq });
                modelBuilder.Entity<RegressionRun>().HasIndex(r => r.Name);
                modelBuilder.Entity<RegressionCoefficient>().HasKey(rc => new { rc.Name, rc.RegressionName, rc.RegressionSeq });
                modelBuilder.Entity<RegressionPoint>().HasKey(rp => new { rp.RegressionName, rp.RegressionSeq, rp.Seq });

                //jrs 2019/07/16
                //modelBuilder.Entity<RegressionCoefficient>().HasOne(b => new { b.RegressionName, b.RegressionSeq }).WithOne(v => new {v.RegressionName, v.RegressionSeq }).HasForeignKey("RegressionCoefficient");
                //modelBuilder.Entity<RegressionRun>().HasMany(v => v.Coefficients).WithOne(rc => rc.myRegression).HasForeignKey(rc => new { rc.RegressionName, rc.RegressionSeq });

                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnModelCreating of StorageContext");
                throw;
            }
        }

        public override void Dispose() {
            Exit();
            base.Dispose();
        }

        //jrs 2019/11/21
        // Load all dataframes from storage service
        public void LoadAllDataframes(IEnumerable<IDataframe> all)
        {
            // load entire table
            var conn = this.Database.GetDbConnection() as SqliteConnection;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT * 
                    FROM ifs_var_blob AS vb";
                using (var reader = cmd.ExecuteReader())
                {
                    var _blob = reader.GetFieldValue<sqlite3_blob>(1);
                    // some how create the dataframe from the reader for each variable 
                    // save the dataframe in a Dictionary(of String, Dataframes)
                    // try doing it in parallel)
                }
            }

            // then create a Dataframe with the corresponding Stream for each
            // do it in parallel
        }

    }
}
