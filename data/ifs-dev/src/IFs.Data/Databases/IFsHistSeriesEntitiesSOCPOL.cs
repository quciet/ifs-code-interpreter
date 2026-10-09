using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{
    //[Table("SeriesWHO2012WaterUnImpFiltered")]
    //internal partial class SeriesWHO2012WaterUnImpFiltered : Series
    //{ }
    [Table("SeriesFreedom")]
    internal partial class SeriesFreedom : Series
    { }

    [Table("SeriesFreedomEcon")]
    internal partial class SeriesFreedomEcon : Series
    { }
   
    [Table("SeriesCorruption")]
    internal partial class SeriesCorruption : Series
    { }

    [Table("SeriesCorruptionCPINew")]
    internal partial class SeriesCorruptionCPINew : Series
    { }

    //Duplicate Table -> Infrastructure???
    //[Table("SeriesGovernanceEffect")]
    //internal partial class SeriesGovernanceEffect : Series
    //{ }
    //[Table("SeriesGovernanceRegQual")]
    //internal partial class SeriesGovernanceRegQual : Series
    //{ }
    [Table("SeriesGovWBDoingBusinessCostofStarting")]
    internal partial class SeriesGovWBDoingBusinessCostofStarting : Series
    { }

    [Table("SeriesGovBusinessRegulationIndex")]
    internal partial class SeriesGovBusinessRegulationIndex : Series
    { }
    [Table("SeriesPolityDemoc")]
    internal partial class SeriesPolityDemoc : Series
    { }
    [Table("SeriesPolityAutoc")]
    internal partial class SeriesPolityAutoc : Series
    { }
    //jrs 2023/10/13
    [Table("SeriesPolyarchy")]
    internal partial class SeriesPolyarchy : Series
    { }
    //clp 2026/02/26
    [Table("SeriesWomenPropRights")]
    internal partial class SeriesWomenPropRights : Series
    { }
    //clp 2026/02/26
    [Table("SeriesFemaleJurShr")]
    internal partial class SeriesFemaleJurShr : Series
    { }
    //clp 2026/02/26
    [Table("SeriesFemaleLegShr")]
    internal partial class SeriesFemaleLegShr : Series
    { }

    //clp 2026/02/26
    [Table("SeriesTheta1")]
    internal partial class SeriesTheta1 : Series 
    { }

    //clp 2026/02/26
    [Table("SeriesTheta2")]
    internal partial class SeriesTheta2 : Series
    { }

    //clp 2026/02/26
    [Table("SeriesTheta3")]
    internal partial class SeriesTheta3 : Series
    { }

    [Table("SeriesGEM")]
    internal partial class SeriesGEM : Series
    { }
    [Table("SeriesEthnic1")]
    internal partial class SeriesEthnic1 : Series
    { }
    [Table("SeriesEthnic2")]
    internal partial class SeriesEthnic2 : Series
    { }
    [Table("SeriesICTInternet%Pop")]
    internal partial class SeriesICTInternetPop : Series
    { }
    [Table("SeriesPoliticalEmbeddCOWJDM")]
    internal partial class SeriesPoliticalEmbeddCOWJDM : Series
    { }
    [Table("SeriesDrugPrevalenceRateAmphetamines")]
    internal partial class SeriesDrugPrevalenceRateAmphetamines : Series
    { }
    //jrs 2026/05/18 [Table("SeriesPopYouthBulgeBy15")]
    [Table("SeriesYouthBulgeBy15UNPD")]
    internal partial class SeriesPopYouthBulgeBy15 : Series
    { }
    [Table("SeriesDrugPrevalenceRateCocaine")]
    internal partial class SeriesDrugPrevalenceRateCocaine : Series
    { }
    [Table("SeriesHealthExpend%ofGDPForDM")]
    internal partial class SeriesHealthExpendofGDPForDM : Series
    { }
    [Table("SeriesDrugPrevalenceRateOpiates")]
    internal partial class SeriesDrugPrevalenceRateOpiates : Series
    { }
    [Table("SeriesConsumptionperCapitaDM")]
    internal partial class SeriesConsumptionperCapitaDM : Series
    { }
    [Table("SeriesDrugPrevalenceRatePrescriptionOpioids")]
    internal partial class SeriesDrugPrevalenceRatePrescriptionOpioids : Series
    { }
    [Table("SeriesDrugPrevalenceAmphetaminesIHME")]
    internal partial class SeriesDrugPrevalenceAmphetaminesIHME : Series
    { }
    [Table("SeriesDrugPrevalenceCocaineIHME")]
    internal partial class SeriesDrugPrevalenceCocaineIHME : Series
    { }
    [Table("SeriesDrOpiodPrevIHME")]
    internal partial class SeriesDrOpiodPrevIHME : Series
    { }
    [Table("SeriesDrugPrevAmphetamineRegAvg")]
    internal partial class SeriesDrugPrevAmphetamineRegAvg : Series
    { }
    [Table("SeriesDrugPrevCocaineRegAvg")]
    internal partial class SeriesDrugPrevCocaineRegAvg : Series
    { }
    [Table("SeriesDrugPrevOpiateRegAvg")]
    internal partial class SeriesDrugPrevOpiateRegAvg : Series
    { }
    [Table("SeriesDrugPrevPresOpRegAvg")]
    internal partial class SeriesDrugPrevPresOpRegAvg : Series
    { }
    //jrs 2026/05/18 [Table("SeriesPopulation65PlusMill")]
    [Table("SeriesPop65PlusUNPD")]
    internal partial class SeriesPopulation65PlusMill : Series
    { }
    [Table("SeriesViolenceSelfHarm")]
    internal partial class SeriesViolenceSelfHarm : Series
    { }
    [Table("SeriesViolencePoliticalPoliceExecution")]
    internal partial class SeriesViolencePoliticalPoliceExecution : Series
    { }
    [Table("SeriesViolenceInterpersonWomenChild")]
    internal partial class SeriesViolenceInterpersonWomenChild : Series
    { }
    [Table("SeriesViolenceInterpersonOther")]
    internal partial class SeriesViolenceInterpersonOther : Series
    { }
    [Table("SeriesViolenceInterpersonWomenAdult")]
    internal partial class SeriesViolenceInterpersonWomenAdult : Series
    { }
    [Table("SeriesViolenceInterpersonChild")]
    internal partial class SeriesViolenceInterpersonChild : Series
    { }
    [Table("SeriesSocietalViolenceDeathsTotal")]
    internal partial class SeriesSocietalViolenceDeathsTotal : Series
    { }
    [Table("SeriesViolencePoliticalConflictTerror")]
    internal partial class SeriesViolencePoliticalConflictTerror : Series
    { }
    [Table("SeriesNPOW")]
    internal partial class SeriesNPOW : Series
    { }
    [Table("SeriesEUMembership")]
    internal partial class SeriesEUMembership : Series
    { }
    [Table("SeriesGenIneqInd")]
    internal partial class SeriesGenIneqInd : Series
    { }

    //jrs 2022/06/08
    [Table("SeriesConflictProbHegreSSP1")]
    internal partial class SeriesConflictProbHegreSSP1 : Series
    { }
    [Table("SeriesConflictProbHegreSSP2")]
    internal partial class SeriesConflictProbHegreSSP2 : Series
    { }
    [Table("SeriesConflictProbHegreSSP3")]
    internal partial class SeriesConflictProbHegreSSP3 : Series
    { }
    [Table("SeriesConflictProbHegreSSP4")]
    internal partial class SeriesConflictProbHegreSSP4 : Series
    { }
    [Table("SeriesConflictProbHegreSSP5")]
    internal partial class SeriesConflictProbHegreSSP5 : Series
    { }
    //jrs 2025/01/16
    [Table("SeriesExoGOVCORRUPT")]
    internal partial class SeriesExoGOVCORRUPT : Series
    { }
    [Table("SeriesExoGOVEFFECT")]
    internal partial class SeriesExoGOVEFFECT : Series
    { }
    [Table("SeriesExoSFCIVILWARUCDP")]
    internal partial class SeriesExoSFCIVILWARUCDP : Series
    { }
    //jrs 2025/08/28
    [Table("SeriesDiploCBIISPRDSecCount")]
    internal partial class SeriesDiploCBIISPRDSecCount : Series
    { }
    [Table("SeriesDiploGeoDistAvgCEPII")]
    internal partial class SeriesDiploGeoDistAvgCEPII : Series
    { }
    [Table("SeriesLeaderTravelBoth")]
    internal partial class SeriesLeaderTravelBoth : Series
    { }
    internal partial class IFsHistSeriesContext
    {
        //jrs 2025/08/28
        protected virtual DbSet<SeriesDiploCBIISPRDSecCount> SeriesDiploCBIISPRDSecCount { get; set; }
        protected virtual DbSet<SeriesDiploGeoDistAvgCEPII> SeriesDiploGeoDistAvgCEPII { get; set; }

        //jrs 2025/01/16
        protected virtual DbSet<SeriesExoGOVCORRUPT> SeriesExoGOVCORRUPT { get; set; }
        protected virtual DbSet<SeriesExoGOVEFFECT> SeriesExoGOVEFFECT { get; set; }
        protected virtual DbSet<SeriesExoSFCIVILWARUCDP> SeriesExoSFCIVILWARUCDP { get; set; }

        //jrs 2022/06/08
        protected virtual DbSet<SeriesConflictProbHegreSSP1> SeriesConflictProbHegreSSP1 { get; set; }
        protected virtual DbSet<SeriesConflictProbHegreSSP2> SeriesConflictProbHegreSSP2 { get; set; }
        protected virtual DbSet<SeriesConflictProbHegreSSP3> SeriesConflictProbHegreSSP3 { get; set; }
        protected virtual DbSet<SeriesConflictProbHegreSSP4> SeriesConflictProbHegreSSP4 { get; set; }
        protected virtual DbSet<SeriesConflictProbHegreSSP5> SeriesConflictProbHegreSSP5 { get; set; }
        //protected virtual DbSet<SeriesWHO2012WaterUnImpFiltered> SeriesWHO2012WaterUnImpFiltered { get; set; }

        protected virtual DbSet<SeriesCorruptionCPINew> SeriesCorruptionCPINew { get; set; }

        protected virtual DbSet<SeriesGenIneqInd> SeriesGenIneqInd { get; set; }

        protected virtual DbSet<SeriesFreedom> SeriesFreedom { get; set; }
        protected virtual DbSet<SeriesFreedomEcon> SeriesFreedomEcon { get; set; }
        protected virtual DbSet<SeriesCorruption> SeriesCorruption { get; set; }
        protected virtual DbSet<SeriesGovWBDoingBusinessCostofStarting> SeriesGovWBDoingBusinessCostofStarting { get; set; }
        protected virtual DbSet<SeriesGovBusinessRegulationIndex> SeriesGovBusinessRegulationIndex { get; set; }
       
        protected virtual DbSet<SeriesPolityDemoc> SeriesPolityDemoc { get; set; }
       
        protected virtual DbSet<SeriesPolityAutoc> SeriesPolityAutoc { get; set; }
        //jrs 2023/10/13
        protected virtual DbSet<SeriesPolyarchy> SeriesPolyarchy { get; set; }

        protected virtual DbSet<SeriesFemaleJurShr> SeriesFemaleJurShr { get; set; } // clp 2026/02/26
        protected virtual DbSet<SeriesFemaleLegShr> SeriesFemaleLegShr { get; set; } // clp 2026/02/26
        protected virtual DbSet<SeriesWomenPropRights> SeriesWomenPropRights { get; set; } // clp 2026/02/26

        protected virtual DbSet<SeriesTheta1> SeriesTheta1 { get; set; } // clp 2026/02/26
        protected virtual DbSet<SeriesTheta2> SeriesTheta2 { get; set; } // clp 2026/02/26
        protected virtual DbSet<SeriesTheta3> SeriesTheta3 { get; set; } // clp 2026/02/26

        protected virtual DbSet<SeriesGEM> SeriesGEM { get; set; }

        protected virtual DbSet<SeriesLeaderTravelBoth> SeriesLeaderTravelBoth { get; set; }

        protected virtual DbSet<SeriesEthnic1> SeriesEthnic1 { get; set; }
       
        protected virtual DbSet<SeriesEthnic2> SeriesEthnic2 { get; set; }
       
        protected virtual DbSet<SeriesICTInternetPop> SeriesICTInternetPop { get; set; }
        
        protected virtual DbSet<SeriesPoliticalEmbeddCOWJDM> SeriesPoliticalEmbeddCOWJDM { get; set; }
      
        protected virtual DbSet<SeriesDrugPrevalenceRateAmphetamines> SeriesDrugPrevalenceRateAmphetamines { get; set; }
       
        protected virtual DbSet<SeriesPopYouthBulgeBy15> SeriesPopYouthBulgeBy15 { get; set; }
        
        protected virtual DbSet<SeriesDrugPrevalenceRateCocaine> SeriesDrugPrevalenceRateCocaine { get; set; }
       
        protected virtual DbSet<SeriesHealthExpendofGDPForDM> SeriesHealthExpendofGDPForDM { get; set; }
        
        protected virtual DbSet<SeriesDrugPrevalenceRateOpiates> SeriesDrugPrevalenceRateOpiates { get; set; }
        
        protected virtual DbSet<SeriesConsumptionperCapitaDM> SeriesConsumptionperCapitaDM { get; set; }
       
        protected virtual DbSet<SeriesDrugPrevalenceRatePrescriptionOpioids> SeriesDrugPrevalenceRatePrescriptionOpioids { get; set; }
      
        protected virtual DbSet<SeriesDrugPrevalenceAmphetaminesIHME> SeriesDrugPrevalenceAmphetaminesIHME { get; set; }
       
        protected virtual DbSet<SeriesDrugPrevalenceCocaineIHME> SeriesDrugPrevalenceCocaineIHME { get; set; }
       
        protected virtual DbSet<SeriesDrOpiodPrevIHME> SeriesDrOpiodPrevIHME { get; set; }
       
        protected virtual DbSet<SeriesDrugPrevAmphetamineRegAvg> SeriesDrugPrevAmphetamineRegAvg { get; set; }
        
        protected virtual DbSet<SeriesDrugPrevCocaineRegAvg> SeriesDrugPrevCocaineRegAvg { get; set; }
       
        protected virtual DbSet<SeriesDrugPrevOpiateRegAvg> SeriesDrugPrevOpiateRegAvg { get; set; }
       
        protected virtual DbSet<SeriesDrugPrevPresOpRegAvg> SeriesDrugPrevPresOpRegAvg { get; set; }
       
        protected virtual DbSet<SeriesPopulation65PlusMill> SeriesPopulation65PlusMill { get; set; }
       
        protected virtual DbSet<SeriesViolenceSelfHarm> SeriesViolenceSelfHarm { get; set; }
      
        protected virtual DbSet<SeriesViolencePoliticalPoliceExecution> SeriesViolencePoliticalPoliceExecution { get; set; }
       
        protected virtual DbSet<SeriesViolenceInterpersonWomenChild> SeriesViolenceInterpersonWomenChild { get; set; }
       
        protected virtual DbSet<SeriesViolenceInterpersonOther> SeriesViolenceInterpersonOther { get; set; }
        
        protected virtual DbSet<SeriesViolenceInterpersonWomenAdult> SeriesViolenceInterpersonWomenAdult { get; set; }
      
        protected virtual DbSet<SeriesViolenceInterpersonChild> SeriesViolenceInterpersonChild { get; set; }
       
        protected virtual DbSet<SeriesSocietalViolenceDeathsTotal> SeriesSocietalViolenceDeathsTotal { get; set; }
        
        protected virtual DbSet<SeriesViolencePoliticalConflictTerror> SeriesViolencePoliticalConflictTerror { get; set; }
        
        protected virtual DbSet<SeriesNPOW> SeriesNPOW { get; set; }
        
        protected virtual DbSet<SeriesEUMembership> SeriesEUMembership { get; set; }
        
    }
}
