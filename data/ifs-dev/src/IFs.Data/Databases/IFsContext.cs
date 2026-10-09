using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using NLog;

#if USE_JET
using EntityFrameworkCore.Jet;
#endif

namespace IFs.Data
{
    internal partial class IFsContext : DbContext
    {

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public IFsContext()
        {}

        public IFsContext(DbContextOptions<IFsContext> options)
            : base(options)
        {}

        public IDimension GetRegionDimension() {
            var dim = new DimensionBind();
            dim.Id = DataExtensions.DIM_REGION;
            dim.Name = DataExtensions.DIM_REGION_NAME;
            dim.Buckets = new SortedList<short, string>();
            using (var db = new IFsContext()) {
                RegionIdNewFull.ToList().ForEach(e => {
                    dim.Buckets.Add((short) e.Id, e.RegionName);
                });
            }
            return dim;
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
                string connectionString = AppConfig.ConnectionStrings["IFs.mdb"].ConnectionString;
                //jrs 2019/03/29
                //if (Environment.CurrentDirectory.Contains("Code.Ifs-Translation"))
                {
                    connectionString = connectionString.Replace("Source=", "Source=../");
                }
                optionsBuilder.UseJet(connectionString);
#else
                    string connectionString = AppConfig.ConnectionStrings["IFs"].ConnectionString;
                    //jrs 2019/03/29
                    //if (Environment.CurrentDirectory.Contains("Code.Ifs-Translation"))
                    {
                        connectionString = connectionString.Replace("Source=", "Source=../");
                    }
                    optionsBuilder.UseSqlite(connectionString); 
#endif
                }
            } catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnConfiguring of IFsContext");
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        	//gvdb 2024/04/05
            // add logging
            try
            {
                modelBuilder.Entity<GroupingFull>().HasKey(r => new { r.GroupId, r.RegionId });
                modelBuilder.Entity<GList>().HasKey(r => new { r.GListCode, r.RegionName, r.MemberOrder });
                modelBuilder.Entity<GlobalParameters>().HasKey(r => new { r.ParameterName, r.Dimension, r.Dimension2 });
                modelBuilder.Entity<GlobalParametersHist>().HasKey(r => new { r.ParameterName, r.Dimension, r.Dimension2 });
                modelBuilder.Entity<GlobalParametersMult>().HasKey(r => new { r.ParameterName, r.Dimension, r.Dimension2 });
                modelBuilder.Entity<GlobalParametersMultHist>().HasKey(r => new { r.ParameterName, r.Dimension, r.Dimension2 });
                modelBuilder.Entity<GlobalParametersMultiple>().HasKey(r => new { r.ELASTRPR1, r.ELASTRPR2, r.ELASTRPR3, r.ELASTRPR4, r.ELASTRPR5, r.ELASTRPR6, r.ETECHADV1, r.ETECHADV2, r.ETECHADV3, r.ETECHADV4, r.ETECHADV5, r.ETECHADV6, r.ETECHADV7, r.LKE1, r.LKE2, r.LKE3, r.LKE4, r.LKE5, r.LKE6, r.LKS1, r.LKS2, r.LKS3, r.LKS4, r.LKS5, r.LKS6, r.MFPLEADR1, r.MFPLEADR2, r.MFPLEADR3, r.MFPLEADR4, r.MFPLEADR5, r.MFPLEADR6, r.RDI1, r.RDI2, r.RDI3, r.RDI4, r.RDINR1, r.RDINR2, r.RDINR3, r.RDINR4, r.WAP1, r.WAP2, r.WAP3, r.WORLDAP1, r.WORLDAP2 });
                modelBuilder.Entity<GlobalParametersMultipleHist>().HasKey(r => new { r.ELASTRPR1, r.ELASTRPR2, r.ELASTRPR3, r.ELASTRPR4, r.ELASTRPR5, r.ELASTRPR6, r.ETECHADV1, r.ETECHADV2, r.ETECHADV3, r.ETECHADV4, r.ETECHADV5, r.ETECHADV6, r.ETECHADV7, r.LKE1, r.LKE2, r.LKE3, r.LKE4, r.LKE5, r.LKE6, r.LKS1, r.LKS2, r.LKS3, r.LKS4, r.LKS5, r.LKS6, r.MFPLEADR1, r.MFPLEADR2, r.MFPLEADR3, r.MFPLEADR4, r.MFPLEADR5, r.MFPLEADR6, r.RDI1, r.RDI2, r.RDI3, r.RDI4, r.RDINR1, r.RDINR2, r.RDINR3, r.RDINR4, r.WAP1, r.WAP2, r.WORLDAP1, r.WORLDAP2 });
                modelBuilder.Entity<Contiguity>().HasKey(r => new { r.Region, r.Region_Adjacent });
                modelBuilder.Entity<TerritorialDisputes>().HasKey(r => new { r.Region, r.Region_Dispute });
                modelBuilder.Entity<HealthDetailedDeathsCtry>().HasKey(r => new { r.Country, r.sex, r.CauseID });

                modelBuilder.Entity<HealthMortFactorCommDisMale>().HasKey(r => new { r.Cohort, r.IncomeType });
                modelBuilder.Entity<HealthMortFactorCommDisFemale>().HasKey(r => new { r.Cohort, r.IncomeType });
                modelBuilder.Entity<HealthMortFactorNonCommDisMale>().HasKey(r => new { r.Cohort, r.IncomeType, r.Group });
                modelBuilder.Entity<HealthMortFactorNonCommDisFemale>().HasKey(r => new { r.Cohort, r.IncomeType, r.Group });
                modelBuilder.Entity<HealthMortFactorInjuryMale>().HasKey(r => new { r.Cohort, r.IncomeType, r.Group });
                modelBuilder.Entity<HealthMortFactorInjuryFemale>().HasKey(r => new { r.Cohort, r.IncomeType, r.Group });

                modelBuilder.Entity<HealthDetailed5YearsDeathsRegs>().HasKey(r => new { r.RegionId, r.TypeId, r.Sex });

                modelBuilder.Entity<HealthMortRatios>().HasKey(r => new { r.Country, r.Disease, r.Gender, r.Age });
                modelBuilder.Entity<HealthCostByType>().HasKey(r => new { r.TypeId, r.CountryIdx });

                modelBuilder.Entity<HealthGBDYLD>().HasKey(r => new { r.Code, r.Gender, r.GroupName });

                modelBuilder.Entity<SharedRiversCountry>().HasKey(r => new { r.CountryACowNumber, r.CountryBCowNumber });
                modelBuilder.Entity<GovConflict>().HasKey(r => new { r.Country1, r.Country2, r.Year });

                modelBuilder.Entity<IFsExogenousVars>().HasKey(r => new { r.ExogenousModelId, r.TableName });

                //jrs 2024/02/20
                modelBuilder.Entity<IFsExogenousVarsGral>().HasKey(r => new { r.VarName, r.TableName });

                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnModelCreating of IFsContext");
                throw;
            }
        }

    }

    internal partial class IFs2Context : DbContext
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public IFs2Context()
        { }

        public IFs2Context(DbContextOptions<IFsContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try { 
                if (!optionsBuilder.IsConfigured)
                {
                    string connectionString = AppConfig.ConnectionStrings["IFs"].ConnectionString;
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
                _logger.Error(ex, "Error during OnConfiguring of IFs2Context");
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        	//gvdb 2024/04/05
            // add logging
            try { 
                modelBuilder.Entity<AnnualGrowthRates>().HasKey(r => new { r.VarName, r.RegName, r.Dim2, r.Dim3 });
                modelBuilder.Entity<AnnualGrowthRatesExt>().HasKey(r => new { r.VarName, r.RegName, r.Dim2, r.Dim3 });
                modelBuilder.Entity<AnnualGrowthRatesHist>().HasKey(r => new { r.VarName, r.RegName, r.Dim2, r.Dim3 });
                modelBuilder.Entity<IOMatricesCalc>().HasKey(r => new { r.Matrix, r.RowSector });
                modelBuilder.Entity<IOMatricesCalcHist>().HasKey(r => new { r.Matrix, r.RowSector });
                modelBuilder.Entity<HealthDetailedDeathsRegs>().HasKey(r => new { r.RegNum, r.sex, r.CauseID });
                modelBuilder.Entity<PopCohortAIDSRegion>().HasKey(r => new { r.RegionId, r.Sex});
                modelBuilder.Entity<HealthDiarrheaReg>().HasKey(r => new { r.RegionId, r.Cohort, r.Gender });
                modelBuilder.Entity<HealthMalariaReg>().HasKey(r => new { r.RegionId, r.Cohort, r.Gender });
                modelBuilder.Entity<HealthMentalHealthReg>().HasKey(r => new { r.RegionId, r.Cohort, r.Gender });
                modelBuilder.Entity<HealthRespInfReg>().HasKey(r => new { r.RegionId, r.Cohort, r.Gender });
                modelBuilder.Entity<HealthRatioYLDYLLRegion>().HasKey(r => new { r.RegionId, r.Code, r.Gender }); 

                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnModelCreating of IFs2Context");
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
