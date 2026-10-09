using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NLog;

#if USE_JET
using EntityFrameworkCore.Jet;
#endif

namespace IFs.Data
{
    internal partial class IFsInitContext : DbContext
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public IFsInitContext()
        { }

        public IFsInitContext(DbContextOptions<IFsInitContext> options)
            : base(options)
        { }

        public bool DevelopmentMode
        {
            //jrs 2022/07/27 booleans now 0 or 1 in sqlite
            get => (this.IFsInit.Find("DevelopmentMode$").Value == "1" ? true : this.IFsInit.Find("DevelopmentMode$").Value == "0" ? false : Convert.ToBoolean(this.IFsInit.Find("DevelopmentMode$").Value));
        }

        //jrs 2024/01/24
        public bool MultiThreading
        {
            //jrs 2022/07/27 booleans now 0 or 1 in sqlite
            get => (this.IFsInit.Find("MultiThreading").Value == "1" ? true : this.IFsInit.Find("MultiThreading").Value == "0" ? false : Convert.ToBoolean(this.IFsInit.Find("MultiThreading").Value));
        }

        // shortcut to IFsInit.FirstYearForecast
        public short FirstYearForecast {
            get => Convert.ToInt16(this.IFsInit.Find("FirstYearForecast%").Value);
        }

        // shortcut to IFsInit.LastYearForecast
        public short LastYearForecast {
            get => Convert.ToInt16(this.IFsInit.Find("LastYearForecast%").Value);
        }

        // shortcut to IFsInit.LastYearHistory
        public short LastYearHistory {
            get => Convert.ToInt16(this.IFsInit.Find("LastYearHistory%").Value);
        }

        // shortcut to IFsInit.FirstYearHistory
        public short FirstYearHistory
        {
            get => Convert.ToInt16(this.IFsInit.Find("FirstYearHistory%").Value);
        }

        // shortcut to IFsInit.FirstYearForecast
        public short BaseYear
        {
            get => Convert.ToInt16(this.IFsInit.Find("FirstYearForecast%").Value);
        }

        // shortcut to IFsInit.MigrationRateTbl
        public string MigrationRateTbl
        {
            //jrs 2022/06/23 handling old files with no value
            //get => this.IFsInit.Find("MigrationRateTbl").Value;
            get => (this.IFsInit.Find("MigrationRateTbl") != null) ? this.IFsInit.Find("MigrationRateTbl").Value : "SeriesForecastNetMigrationRateMergedUNPDAbel";
        }

        // shortcut to IFsInit.MigrantsTbl
        public string MigrantsTbl
        {
            //jrs 2022/06/23 handling old files with no value
            //get => this.IFsInit.Find("MigrantsTbl").Value;
            get => (this.IFsInit.Find("MigrantsTbl") != null) ? this.IFsInit.Find("MigrantsTbl").Value : "SeriesForecastNetMigrationMergedUNPDAbel";
        }

        // shortcut to IFsInit.NCohorts
        public short NCohorts
        {
            get => Convert.ToInt16(this.IFsInit.Find("Ncohorts").Value);
        }

        // shortcut to IFsInit.LastCohortFert
        public short LastCohortFert
        {
            //jrs 2022/06/23 handling old files with no value
            //get => Convert.ToInt16(this.IFsInit.Find("LastCohortFert").Value);
            get => Convert.ToInt16((this.IFsInit.Find("LastCohortFert") != null) ? this.IFsInit.Find("LastCohortFert").Value : "11");
        }

        // shortcut to IFsInit.LastYearMigration
        public short LastYearMigration
        {
            //jrs 2022/06/23 handling old files with no value
            //get => Convert.ToInt16(this.IFsInit.Find("LastYearMigration").Value);
            get => Convert.ToInt16((this.IFsInit.Find("LastYearMigration") != null) ? this.IFsInit.Find("LastYearMigration").Value : "2100");
        }

        // shortcut to LoadFull.SupplementalDataFile
        public string SupplementalDataFile
        {
            get => this.LoadFull.Find("SupplementalDataFile$").Value;
        }

        // shortcut to LoadFull.SupplementalHistoricDataFile
        public string SupplementalHistoricDataFile
        {
            get => this.LoadFull.Find("SupplementalHistoricDataFile$").Value;
        }

        // shortcut to IFsInit.DataFileSubDir
        public string DataFileSubDir
        {
            get => this.IFsInit.Find("DataFileSubDir$").Value;
        }

        public string ResFileSubDir
        {
            get => this.IFsInit.Find("ResFileSubDir$").Value;
        }

        public string ActiveBase
        {
            get => this.IFsInit.Find("ActiveBase$").Value;
        }

        public string ScenarioFileSubDir
        {
            get => this.IFsInit.Find("ScenarioFileSubDir$").Value;
        }

        public short NInfraIndexTradComp
        {
            //jrs 2022/06/23 handling old files with no value
            //get => Convert.ToInt16(this.IFsInit.Find("NInfraIndexTradComp%").Value);
            get => Convert.ToInt16((this.IFsInit.Find("NInfraIndexTradComp%") != null) ? this.IFsInit.Find("NInfraIndexTradComp%").Value : "-3");
        }

        public short NInfraIndexICTComp
        {
            get => Convert.ToInt16(this.IFsInit.Find("NInfraIndexICTComp%").Value);
        }

        public short NInfraIndTranComp
        {
            get => Convert.ToInt16(this.IFsInit.Find("NInfraIndTranComp%").Value);
        }

        public short NInfraIndElecComp
        {
            get => Convert.ToInt16(this.IFsInit.Find("NInfraIndElecComp%").Value);
        }

        public short NInfraIndWatSanComp
        {
            get => Convert.ToInt16(this.IFsInit.Find("NInfraIndWatSanComp%").Value);
        }

        public short NInfraIndexComp
        {
            get => Convert.ToInt16(this.IFsInit.Find("NInfraIndexComp%").Value);
        }

        public short NLocation
        {
            get => Convert.ToInt16(this.IFsInit.Find("Nlocation").Value);
        }

        public short NInfraType
        {
            get => Convert.ToInt16(this.IFsInit.Find("NInfraType%").Value);
        }

        public short NSec
        {
            get => Convert.ToInt16(this.IFsInit.Find("NSec%").Value);
        }

        public short NAges
        {
            get => Convert.ToInt16(this.IFsInit.Find("Nages").Value);
        }

        public short NGender
        {
            get => Convert.ToInt16(this.IFsInit.Find("Ngender").Value);
        }

        public short NPriGr
        {
            //jrs 2025/10/31
            //get => Convert.ToInt16(this.IFsInit.Find("NPriGr").Value);
            get => Convert.ToInt16((this.IFsInit.Find("NPriGr") != null) ? this.IFsInit.Find("NPriGr").Value : "0");
        }

        public short NSkill
        {
            //jrs 2022/06/15 handling old files with no value
            get => Convert.ToInt16((this.IFsInit.Find("Nskill%") != null) ? this.IFsInit.Find("Nskill%").Value : "0");
        }

        public short NHHTypes
        {
            get => Convert.ToInt16(this.IFsInit.Find("NHHTypes%").Value);
        }

        public short NWBTar
        {
            get => Convert.ToInt16(this.IFsInit.Find("NWBTar%").Value);
        }
        
        public short NIOMatrices
        {
            get => Convert.ToInt16(this.IFsInit.Find("NIOMatrices").Value);
        }

        public short NGovExp
        {
            get => Convert.ToInt16(this.IFsInit.Find("NGovExp%").Value);
        }

        public short NET
        {
            get => Convert.ToInt16(this.IFsInit.Find("NET%").Value);
        }

        public short NEP
        {
            get => Convert.ToInt16(this.IFsInit.Find("NEP%").Value);
        }

        public short NER
        {
            get => Convert.ToInt16(this.IFsInit.Find("NER%").Value);
        }

        public short NumbCulReg
        {
            get => Convert.ToInt16(this.IFsInit.Find("NumbCulReg").Value);
        }

        public short WVSCohorts
        {
            get => Convert.ToInt16(this.IFsInit.Find("WVSCohorts").Value);
        }

        public short MaxReg
        {
            get => Convert.ToInt16(this.LoadFull.Find("MaxReg").Value);
        }

        //jrs 2022/11/01 shortcut to IFsInit.ConstantDollarsYear
        public short ConstantDollarsYear
        {
            //jrs 2023/01/29 handling old files with no value
            //get => Convert.ToInt16(this.IFsInit.Find("ConstantDollarsYear").Value);
            get => (this.IFsInit.Find("ConstantDollarsYear") != null) ? Convert.ToInt16(this.IFsInit.Find("ConstantDollarsYear").Value) : Convert.ToInt16(2011);
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
                string connectionString = AppConfig.ConnectionStrings["IFsInit.mdb"].ConnectionString;
                //jrs 2019/03/29
                //if (Environment.CurrentDirectory.Contains("Code.Ifs-Translation"))
                {
                    connectionString = connectionString.Replace("Source=", "Source=../");
                }
                optionsBuilder.UseJet(connectionString);
#else
                    string connectionString = AppConfig.ConnectionStrings["IFsInit"].ConnectionString;
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
                _logger.Error(ex, "Error during OnConfiguring of IFsInitContext");
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        	//gvdb 2024/04/05
            // add logging
            try
            {
                //jrs 2022/09/15
                modelBuilder.Entity<ProjectDataCountryLoad>().HasKey(r => new { r.Country });

                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during OnModelCreating of IFsInitContext");
                throw;
            }
        }
    }
}
