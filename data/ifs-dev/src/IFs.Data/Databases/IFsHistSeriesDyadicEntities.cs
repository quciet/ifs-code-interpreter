using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{
    internal abstract partial class SeriesDyad
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Actor")]
        public string Actor { get; set; }
        [Column("FIPS_CODEA")]
        public string FipsCodeA { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Partner")]
        public string Partner { get; set; }
        [Column("FIPS_CODEB")]
        public string FipsCodeB { get; set; }
        [Column("Earliest")]
        public float? EarliestValue { get; set; }
        [Column("MostRecent")]
        public float? MostRecentValue { get; set; }
    }

    [Table("SeriesExportsDOTSDyadic")]
    internal partial class SeriesExportsDOTSDyadic : SeriesDyad
    { }

    [Table("SeriesExportsAg")]
    internal partial class SeriesExportsAg : SeriesDyad
    { }

    [Table("SeriesExportsEn")]
    internal partial class SeriesExportsEn : SeriesDyad
    { }

    [Table("SeriesExportsICT")]
    internal partial class SeriesExportsICT : SeriesDyad
    { }

    [Table("SeriesExportsMan")]
    internal partial class SeriesExportsMan : SeriesDyad
    { }
    
    [Table("SeriesExportsMat")]
    internal partial class SeriesExportsMat : SeriesDyad
    { }
    
    [Table("SeriesExportsServ")]
    internal partial class SeriesExportsServ : SeriesDyad
    { }

    [Table("SeriesWtTariffAgri")]
    internal partial class SeriesWtTariffAgri : SeriesDyad
    { }

    [Table("SeriesWtTariffEner")]
    internal partial class SeriesWtTariffEner : SeriesDyad
    { }

    [Table("SeriesWtTariffICT")]
    internal partial class SeriesWtTariffICT : SeriesDyad
    { }

    [Table("SeriesWtTariffManu")]
    internal partial class SeriesWtTariffManu : SeriesDyad
    { }

    [Table("SeriesWtTariffMate")]
    internal partial class SeriesWtTariffMate : SeriesDyad
    { }

    [Table("SeriesDistanceCEPII")]
    internal partial class SeriesDistanceCEPII : SeriesDyad
    { }
    
    [Table("SeriesMilitaryExportsTotal")]
    internal partial class SeriesMilitaryExportsTotal : SeriesDyad
    { }

    [Table("SeriesFDISent")]
    internal partial class SeriesFDISent : SeriesDyad
    { }

    [Table("SeriesAIDSent")]
    internal partial class SeriesAIDSent : SeriesDyad
    { }

    [Table("SeriesMigrationFlowOut")]
    internal partial class SeriesMigrationFlowOut : SeriesDyad
    { }

    //dsb 2025/10/20
    [Table("SeriesMigrationFlowOutDL")]
    internal partial class SeriesMigrationFlowOutDL : SeriesDyad
    { }

    [Table("SeriesMigrationForcedOut")]
    internal partial class SeriesMigrationForcedOut : SeriesDyad
    { }

    [Table("SeriesMigrationForcedReturn")]
    internal partial class SeriesMigrationForcedReturn : SeriesDyad
    { }

    [Table("SeriesMigrationForcedStock")]
    internal partial class SeriesMigrationForcedStock : SeriesDyad
    { }

    [Table("SeriesMigTotal")]
    internal partial class SeriesMigTotal : SeriesDyad
    { }

    [Table("SeriesDiploEmbassyOut")]
    internal partial class SeriesDiploEmbassyOutDyad : SeriesDyad
    { }

    [Table("SeriesDiploIGOSharedWeighted")]
    internal partial class SeriesDiploIGOSharedWeighted : SeriesDyad
    { }

    [Table("SeriesDiploTreatySharedWeighted")]
    internal partial class SeriesDiploTreatySharedWeighted : SeriesDyad
    { }

    [Table("SeriesDiploAllianceSharedWeighted_ATOP")]
    internal partial class SeriesDiploAllianceSharedWeighted_ATOP : SeriesDyad
    { }

    [Table("SeriesBandwidth")]
    internal partial class SeriesBandwidth : SeriesDyad
    { }

    [Table("SeriesDependence")]
    internal partial class SeriesDependence : SeriesDyad
    { }

    [Table("SeriesInfluence")]
    internal partial class SeriesInfluence : SeriesDyad
    { }

    [Table("SeriesMIDsEventBinaryMovAvg")]
    internal partial class SeriesMIDsEventBinaryMovAvg : SeriesDyad
    { }

    [Table("SeriesMIDsEventBinary")]
    internal partial class SeriesMIDsEventBinary : SeriesDyad
    { }

    [Table("SeriesArmsExportsAtoBStock")]
    internal partial class SeriesArmsExportsAtoBStock : SeriesDyad
    { }

    [Table("SeriesExportsGoods")]
    internal partial class SeriesExportsGoods : SeriesDyad
    { }

    [Table("SeriesUNCTADTariffRevAgri")]
    internal partial class SeriesUNCTADTariffRevAgri : SeriesDyad
    { }

    [Table("SeriesUNCTADTariffRevEner")]
    internal partial class SeriesUNCTADTariffRevEner : SeriesDyad
    { }

    [Table("SeriesUNCTADTariffRevICT")]
    internal partial class SeriesUNCTADTariffRevICT : SeriesDyad
    { }

    [Table("SeriesUNCTADTariffRevManu")]
    internal partial class SeriesUNCTADTariffRevManu : SeriesDyad
    { }

    [Table("SeriesUNCTADTariffRevMate")]
    internal partial class SeriesUNCTADTariffRevMate : SeriesDyad
    { }

    [Table("SeriesTCIAgri")]
    internal partial class SeriesTCIAgri : SeriesDyad
    { }

    [Table("SeriesTCIEner")]
    internal partial class SeriesTCIEner : SeriesDyad
    { }

    [Table("SeriesTCIMate")]
    internal partial class SeriesTCIMate : SeriesDyad
    { }

    [Table("SeriesTCIManu")]
    internal partial class SeriesTCIManu : SeriesDyad
    { }

    [Table("SeriesTCIICT")]
    internal partial class SeriesTCIICT : SeriesDyad
    { }

    [Table("SeriesTradeIndex")]
    internal partial class SeriesTradeIndex : SeriesDyad
    { }

    // clp 2024/07/10
    [Table("SeriesConflictYears")]
    internal partial class SeriesConflictYears : SeriesDyad
    { }

    // clp 2024/07/10
    [Table("SeriesConflictYears_UCDP")]
    internal partial class SeriesConflictYears_UCDP : SeriesDyad
    { }

    // clp 2024/07/10
    [Table("SeriesConflictYears_MIEs")]
    internal partial class SeriesConflictYears_MIEs : SeriesDyad
    { }

    // clp 2024/12/20
    [Table("SeriesRivalry")]
    internal partial class SeriesRivalry : SeriesDyad
    { }

    //jrs 2025/07/29
    [Table("SeriesWtTradeElasticitiesAgri")]
    internal partial class SeriesWtTradeElasticitiesAgri : SeriesDyad
    { }
    [Table("SeriesWtTradeElasticitiesEner")]
    internal partial class SeriesWtTradeElasticitiesEner : SeriesDyad
    { }
    [Table("SeriesWtTradeElasticitiesICT")]
    internal partial class SeriesWtTradeElasticitiesICT : SeriesDyad
    { }
    [Table("SeriesWtTradeElasticitiesManu")]
    internal partial class SeriesWtTradeElasticitiesManu : SeriesDyad
    { }
    [Table("SeriesWtTradeElasticitiesMate")]
    internal partial class SeriesWtTradeElasticitiesMate : SeriesDyad
    { }

    // clp 2025/12/09 SeriesDyadLeaderTripsIn
    [Table("SeriesDyadLeaderTripsIn")]
    internal partial class SeriesDyadLeaderTripsIn : SeriesDyad
    { }

    //dsb 2026/06/08
    [Table("SeriesMigTotal_UNPD2024")]
    internal partial class SeriesMigTotal_UNPD2024 : SeriesDyad
    { }

    internal partial class IFsHistSeriesDyadicContext
    {
        //jrs 2025/07/29
        protected virtual DbSet<SeriesWtTradeElasticitiesAgri> SeriesWtTradeElasticitiesAgri { get; set; }
        protected virtual DbSet<SeriesWtTradeElasticitiesEner> SeriesWtTradeElasticitiesEner { get; set; }
        protected virtual DbSet<SeriesWtTradeElasticitiesICT> SeriesWtTradeElasticitiesICT { get; set; }
        protected virtual DbSet<SeriesWtTradeElasticitiesManu> SeriesWtTradeElasticitiesManu { get; set; }
        protected virtual DbSet<SeriesWtTradeElasticitiesMate> SeriesWtTradeElasticitiesMate { get; set; }
        protected virtual DbSet<SeriesTradeIndex> SeriesTradeIndex { get; set; }
        protected virtual DbSet<SeriesTCIAgri> SeriesTCIAgri { get; set; }
        protected virtual DbSet<SeriesTCIEner> SeriesTCIEner { get; set; }
        protected virtual DbSet<SeriesTCIManu> SeriesTCIManu { get; set; }
        protected virtual DbSet<SeriesTCIMate> SeriesTCIMate { get; set; }
        protected virtual DbSet<SeriesTCIICT> SeriesTCIICT { get; set; }

        protected virtual DbSet<SeriesExportsDOTSDyadic> SeriesExportsDOTSDyadic { get; set; }
        protected virtual DbSet<SeriesExportsAg> SeriesExportsAg { get; set; }
        protected virtual DbSet<SeriesExportsEn> SeriesExportsEn { get; set; }
        protected virtual DbSet<SeriesExportsICT> SeriesExportsICT { get; set; }
        protected virtual DbSet<SeriesExportsMan> SeriesExportsMan { get; set; }        
        protected virtual DbSet<SeriesExportsMat> SeriesExportsMat { get; set; }
        protected virtual DbSet<SeriesExportsServ> SeriesExportsServ { get; set; }
        protected virtual DbSet<SeriesDistanceCEPII> SeriesDistanceCEPII { get; set; }

        protected virtual DbSet<SeriesWtTariffAgri> SeriesWtTariffAgri { get; set; }
        protected virtual DbSet<SeriesWtTariffEner> SeriesWtTariffEner { get; set; }
        protected virtual DbSet<SeriesWtTariffICT> SeriesWtTariffICT { get; set; }
        protected virtual DbSet<SeriesWtTariffManu> SeriesWtTariffManu { get; set; }
        protected virtual DbSet<SeriesWtTariffMate> SeriesWtTariffMate { get; set; }

        protected virtual DbSet<SeriesMilitaryExportsTotal> SeriesMilitaryExportsTotal { get; set; }
        protected virtual DbSet<SeriesFDISent> SeriesFDISent { get; set; }
        protected virtual DbSet<SeriesAIDSent> SeriesAIDSent { get; set; }

        protected virtual DbSet<SeriesMigrationFlowOut> SeriesMigrationFlowOut { get; set; }
        //dsb 2025/10/20
        protected virtual DbSet<SeriesMigrationFlowOutDL> SeriesMigrationFlowOutDL { get; set; }
        protected virtual DbSet<SeriesMigrationForcedOut> SeriesMigrationForcedOut { get; set; }
        protected virtual DbSet<SeriesMigrationForcedReturn> SeriesMigrationForcedReturn { get; set; }
        protected virtual DbSet<SeriesMigrationForcedStock> SeriesMigrationForcedStock { get; set; }
        protected virtual DbSet<SeriesMigTotal> SeriesMigTotal { get; set; }
        //dsb 2026/06/08
        protected virtual DbSet<SeriesMigTotal_UNPD2024> SeriesMigTotal_UNPD2024 { get; set; }

        protected virtual DbSet<SeriesDiploEmbassyOutDyad> SeriesDiploEmbassyOutDyad { get; set; }
        protected virtual DbSet<SeriesDiploIGOSharedWeighted> SeriesDiploIGOSharedWeighted { get; set; }
        protected virtual DbSet<SeriesDiploTreatySharedWeighted> SeriesDiploTreatySharedWeighted { get; set; }
        protected virtual DbSet<SeriesDiploAllianceSharedWeighted_ATOP> SeriesDiploAllianceSharedWeighted_ATOP { get; set; }
        protected virtual DbSet<SeriesBandwidth> SeriesBandwidth { get; set; }
        protected virtual DbSet<SeriesDependence> SeriesDependence { get; set; }
        protected virtual DbSet<SeriesInfluence> SeriesInfluence { get; set; }
        protected virtual DbSet<SeriesMIDsEventBinaryMovAvg> SeriesMIDsEventBinaryMovAvg { get; set; }
        protected virtual DbSet<SeriesMIDsEventBinary> SeriesMIDsEventBinary { get; set; }
        protected virtual DbSet<SeriesArmsExportsAtoBStock> SeriesArmsExportsAtoBStock { get; set; }

        protected virtual DbSet<SeriesExportsGoods> SeriesExportsGoods { get; set; }
        protected virtual DbSet<SeriesUNCTADTariffRevAgri> SeriesUNCTADTariffRevAgri { get; set; }
        protected virtual DbSet<SeriesUNCTADTariffRevEner> SeriesUNCTADTariffRevEner { get; set; }
        protected virtual DbSet<SeriesUNCTADTariffRevICT> SeriesUNCTADTariffRevICT { get; set; }
        protected virtual DbSet<SeriesUNCTADTariffRevManu> SeriesUNCTADTariffRevManu { get; set; }
        protected virtual DbSet<SeriesUNCTADTariffRevMate> SeriesUNCTADTariffRevMate { get; set; }
        protected virtual DbSet<SeriesConflictYears> SeriesConflictYears { get; set; } // clp 2024/07/10
        protected virtual DbSet<SeriesConflictYears_MIEs> SeriesConflictYears_MIEs { get; set; } // clp 2025/06/04
        protected virtual DbSet<SeriesConflictYears_UCDP> SeriesConflictYears_UCDP { get; set; } // clp 2025/06/04
        protected virtual DbSet<SeriesRivalry> SeriesRivalry { get; set; } // clp 2024/12/20

        protected virtual DbSet<SeriesDyadLeaderTripsIn> SeriesDyadLeaderTripsIn { get; set; } // clp 2025/12/09

    }
}
