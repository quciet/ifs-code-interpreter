using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{
    [Table("SeriesWHO2012WaterUnImpFiltered")]
    internal partial class SeriesWHO2012WaterUnImpFiltered : Series
    { }

    [Table("SeriesWHO2012WaterUnImpNoFilter")]
    internal partial class SeriesWHO2012WaterUnImpNoFilter : Series
    { }

    [Table("SeriesWHO2012WaterOthImpFiltered")]
    internal partial class SeriesWHO2012WaterOthImpFiltered : Series
    { }

    [Table("SeriesWHO2012WaterOthImpNoFilter")]
    internal partial class SeriesWHO2012WaterOthImpNoFilter : Series
    { }

    [Table("SeriesWHO2012WaterPipedFiltered")]
    internal partial class SeriesWHO2012WaterPipedFiltered : Series
    { }

    [Table("SeriesWHO2012WaterPipedNoFilter")]
    internal partial class SeriesWHO2012WaterPipedNoFilter : Series
    { }

    [Table("SeriesEnElecTotalCapacityEIA")]
    internal partial class SeriesEnElecTotalCapacityEIA : Series
    { }

    [Table("SeriesEnElecAccess%Urban")]
    internal partial class SeriesEnElecAccessUrban : Series
    { }

    [Table("SeriesEnElecAccess%Rural")]
    internal partial class SeriesEnElecAccessRural : Series
    { }

    [Table("SeriesEnElecAccess%National")]
    internal partial class SeriesEnElecAccessNational : Series
    { }

    [Table("SeriesEnElecTransLoss%")]
    internal partial class SeriesEnElecTransLoss : Series
    { }

    [Table("SeriesEnProdElec")]
    internal partial class SeriesEnProdElec : Series
    { }

    [Table("SeriesEnElecShrEnDem")]
    internal partial class SeriesEnElecShrEnDem : Series
    { }

    [Table("SeriesEnElecConsPerCap")]
    internal partial class SeriesEnElecConsPerCap : Series
    { }

    [Table("SeriesRoadsTotalNetwork")]
    internal partial class SeriesRoadsTotalNetwork : Series
    { }

    [Table("SeriesRoadsPaved%")]
    internal partial class SeriesRoadsPaved : Series
    { }

    [Table("SeriesRoadPavedKm")]
    internal partial class SeriesRoadPavedKm : Series
    { }

    [Table("SeriesWSSJMPSanitationTotal%Improved")]
    internal partial class SeriesWSSJMPSanitationTotalImproved : Series
    { }

    [Table("SeriesWSSJMPSanitationTotal%Shared")]
    internal partial class SeriesWSSJMPSanitationTotalShared : Series
    { }

    [Table("SeriesWSSJMPSanitationTotal%OpenDefecation")]
    internal partial class SeriesWSSJMPSanitationTotalOpenDefecation : Series
    { }

    [Table("SeriesWSSJMPSanitationTotal%OtherUnimproved")]
    internal partial class SeriesWSSJMPSanitationTotalOtherUnimproved : Series
    { }

    [Table("SeriesWSSJMPWaterTotal%Piped")]
    internal partial class SeriesWSSJMPWaterTotalPiped : Series
    { }

    [Table("SeriesWSSJMPWaterTotal%OtherImproved")]
    internal partial class SeriesWSSJMPWaterTotalOtherImproved : Series
    { }

    [Table("SeriesWSSJMPWaterTotal%OtherUnImproved")]
    internal partial class SeriesWSSJMPWaterTotalOtherUnImproved : Series
    { }

    [Table("SeriesWSSJMPWaterTotal%Surface")]
    internal partial class SeriesWSSJMPWaterTotalSurface : Series
    { }

    [Table("SeriesWasteWaterColConnect%")]
    internal partial class SeriesWasteWaterColConnect : Series
    { }

    [Table("SeriesWasteWaterTreatConnect%")]
    internal partial class SeriesWasteWaterTreatConnect : Series
    { }

    [Table("SeriesICTTelephoneLinesPer100")]
    internal partial class SeriesICTTelephoneLinesPer100 : Series
    { }

    [Table("SeriesICTComputerHousehold%")]
    internal partial class SeriesICTComputerHousehold : Series
    { }

    [Table("SeriesICTInternetHousehold%")]
    internal partial class SeriesICTInternetHousehold : Series
    { }

    [Table("SeriesRoadRuralAccessIndex")]
    internal partial class SeriesRoadRuralAccessIndex : Series
    { }

    [Table("SeriesLandAgri")]
    internal partial class SeriesLandAgri : Series
    { }

    [Table("SeriesLandIrPotential")]
    internal partial class SeriesLandIrPotential : Series
    { }

    [Table("SeriesLandIrPotentialReached")]
    internal partial class SeriesLandIrPotentialReached : Series
    { }

    [Table("SeriesLandIrAreaEquip")]
    internal partial class SeriesLandIrAreaEquip : Series
    { }

    //'ICT formulations for Broadband and Mobile Technology, ICT Index
    //'****************************************************************
    [Table("SeriesICTTelephoneCellSubscribersPer100")]
    internal partial class SeriesICTTelephoneCellSubscribersPer100 : Series
    { }

    [Table("SeriesICTBroadbandSubscribersPer100ITU")]
    internal partial class SeriesICTBroadbandSubscribersPer100ITU : Series
    { }

    [Table("SeriesICTBroadbandMobileSubsPer100")]
    internal partial class SeriesICTBroadbandMobileSubsPer100 : Series
    { }

    [Table("SeriesICTCYBICTDevelopmentIndexITU")]
    internal partial class SeriesICTCYBICTDevelopmentIndexITU : Series
    { }

    //'ICT Spending
    //'------------
    [Table("SeriesICTExpend%GDP")]
    internal partial class SeriesICTExpendGDP : Series
    { }

    [Table("SeriesGovernanceEffect")]
    internal partial class SeriesGovernanceEffect : Series
    { }

    [Table("SeriesGovernanceRegQual")]
    internal partial class SeriesGovernanceRegQual : Series
    { }

    [Table("SeriesEnConTotalWDI")]
    internal partial class SeriesEnConTotalWDI : Series
    { }

    [Table("SeriesHouseholds")]
    internal partial class SeriesHouseholds : Series
    { }

    // mti 2022/11/14 start

    //Water and Sanitation - Access rate data series declared in the partial class
    //Water (new 5 category) access rate data series declared in the partial class
    //rural water - access rate data
    [Table("SeriesWSSJMPWaterRural%Surface")]
    internal partial class SeriesWSSJMPWaterRuralSurface : Series
    { }

    [Table("SeriesWSSJMPWaterRural%Unimproved")]
    internal partial class SeriesWSSJMPWaterRuralUnimproved : Series
    { }

    [Table("SeriesWSSJMPWaterRural%Limited")]
    internal partial class SeriesWSSJMPWaterRuralLimited : Series
    { }

    [Table("SeriesWSSJMPWaterRural%Basic")]
    internal partial class SeriesWSSJMPWaterRuralBasic : Series
    { }

    [Table("SeriesWSSJMPWaterRural%SafelyManaged")]
    internal partial class SeriesWSSJMPWaterRuralSafelyManaged : Series
    { }

    //Urban water - access rate data
    [Table("SeriesWSSJMPWaterUrban%Surface")]
    internal partial class SeriesWSSJMPWaterUrbanSurface : Series
    { }

    [Table("SeriesWSSJMPWaterUrban%Unimproved")]
    internal partial class SeriesWSSJMPWaterUrbanUnimproved : Series
    { }

    [Table("SeriesWSSJMPWaterUrban%Limited")]
    internal partial class SeriesWSSJMPWaterUrbanLimited : Series
    { }

    [Table("SeriesWSSJMPWaterUrban%Basic")]
    internal partial class SeriesWSSJMPWaterUrbanBasic : Series
    { }

    [Table("SeriesWSSJMPWaterUrban%SafelyManaged")]
    internal partial class SeriesWSSJMPWaterUrbanSafelyManaged : Series
    { }

    //National water - access rate data
    /* Surface - Already opened in three category Watsan code
     [Table("SeriesWSSJMPWaterTotal%Surface")]
    internal partial class SeriesWSSJMPWaterTotalSurface : Series
    { } */

    [Table("SeriesWSSJMPWaterTotal%Unimproved")]
    internal partial class SeriesWSSJMPWaterTotalUnimproved : Series
    { }

    [Table("SeriesWSSJMPWaterTotal%Limited")]
    internal partial class SeriesWSSJMPWaterTotalLimited : Series
    { }

    [Table("SeriesWSSJMPWaterTotal%Basic")]
    internal partial class SeriesWSSJMPWaterTotalBasic : Series
    { }

    [Table("SeriesWSSJMPWaterTotal%SafelyManaged")]
    internal partial class SeriesWSSJMPWaterTotalSafelyManaged : Series
    { }

    //Sanitation (new 5 category) access rate data series declared in the partial class
    //rural
    [Table("SeriesWSSJMPSanitationRural%OpenDefecation")]
    internal partial class SeriesWSSJMPSanitationRuralOpenDefecation : Series
    { }

    [Table("SeriesWSSJMPSanitationRural%Unimproved")]
    internal partial class SeriesWSSJMPSanitationRuralUnimproved : Series
    { }

    [Table("SeriesWSSJMPSanitationRural%Limited")]
    internal partial class SeriesWSSJMPSanitationRuralLimited : Series
    { }

    [Table("SeriesWSSJMPSanitationRural%Basic")]
    internal partial class SeriesWSSJMPSanitationRuralBasic : Series
    { }

    [Table("SeriesWSSJMPSanitationRural%SafelyManaged")]
    internal partial class SeriesWSSJMPSanitationRuralSafelyManaged : Series
    { }

    //Urban
    [Table("SeriesWSSJMPSanitationUrban%OpenDefecation")]
    internal partial class SeriesWSSJMPSanitationUrbanOpenDefecation : Series
    { }

    [Table("SeriesWSSJMPSanitationUrban%Unimproved")]
    internal partial class SeriesWSSJMPSanitationUrbanUnimproved : Series
    { }

    [Table("SeriesWSSJMPSanitationUrban%Limited")]
    internal partial class SeriesWSSJMPSanitationUrbanLimited : Series
    { }

    [Table("SeriesWSSJMPSanitationUrban%Basic")]
    internal partial class SeriesWSSJMPSanitationUrbanBasic : Series
    { }

    [Table("SeriesWSSJMPSanitationUrban%SafelyManaged")]
    internal partial class SeriesWSSJMPSanitationUrbanSafelyManaged : Series
    { }

    //Total
    /* Open Defecation Already opened in three category Watsan code
     * [Table("SeriesWSSJMPSanitationTotal%OpenDefecation")]
    internal partial class SeriesWSSJMPSanitationTotalOpenDefecation : Series
    { }*/

    [Table("SeriesWSSJMPSanitationTotal%Unimproved")]
    internal partial class SeriesWSSJMPSanitationTotalUnimproved : Series
    { }

    [Table("SeriesWSSJMPSanitationTotal%Limited")]
    internal partial class SeriesWSSJMPSanitationTotalLimited : Series
    { }

    [Table("SeriesWSSJMPSanitationTotal%Basic")]
    internal partial class SeriesWSSJMPSanitationTotalBasic : Series
    { }

    [Table("SeriesWSSJMPSanitationTotal%SafelyManaged")]
    internal partial class SeriesWSSJMPSanitationTotalSafelyManaged : Series
    { }

    // Water and Sanitation - Cost for (applicable categories) - rural and urban only

    //Water and Sanitation - Cost data series declared in the partial class
    //Water (new 5 category) cost data series declared in the partial class
    //rural water - cost data

    // no cost for water surface, unimproved, or limited

    //water cost - rural basic - lower cost 
    [Table("SeriesWSSJMPWaterCostRuralDugWell")]
    internal partial class SeriesWSSJMPWaterCostRuralDugwell : Series
    { }

    //water cost - rural basic - higher cost 
    [Table("SeriesWSSJMPWaterCostRuralTubewellBorehole")]
    internal partial class SeriesWSSJMPWaterCostRuralTubewellBorehole : Series
    { }

    //water cost - rural safely managed - lower cost 
    [Table("SeriesWSSJMPWaterCostRuralPipedOnPlot")]
    internal partial class SeriesWSSJMPWaterCostRuralPipedOnPlot : Series
    { }

    //Urban WaterCost - access rate data
    //water cost - Urban basic - lower cost 
    [Table("SeriesWSSJMPWaterCostUrbanDugWell")]
    internal partial class SeriesWSSJMPWaterCostUrbanDugwell : Series
    { }

    //water cost - Urban basic - higher cost 
    [Table("SeriesWSSJMPWaterCostUrbanTubewellBorehole")]
    internal partial class SeriesWSSJMPWaterCostUrbanTubewellBorehole : Series
    { }

    //water cost - Urban safely managed - lower cost 
    [Table("SeriesWSSJMPWaterCostUrbanPipedOnPlot")]
    internal partial class SeriesWSSJMPWaterCostUrbanPipedOnPlot : Series
    { }

    //SanitationCost (new 5 category) access rate data series declared in the partial class
    //rural
    // no cost for ODF


    // rural low cost - unimproved
    [Table("SeriesWSSJMPSanitationCostRuralUnimprovedPitLatrine")]
    internal partial class SeriesWSSJMPSanitationCostRuralUnimprovedPitLatrine : Series
    { }

    // no cost for limited

    // rural low cost - basic  
    [Table("SeriesWSSJMPSanitationCostRuralDryPitLatrine")]
    internal partial class SeriesWSSJMPSanitationCostRuralDryPitLatrine : Series
    { }

    // rural high cost - basic  
    [Table("SeriesWSSJMPSanitationCostRuralWetPitLatrine")]
    internal partial class SeriesWSSJMPSanitationCostRuralWetPitLatrine : Series
    { }

    // rural low cost - safely managed  
    [Table("SeriesWSSJMPSanitationCostRuralPitLatrineWithFSM")]
    internal partial class SeriesWSSJMPSanitationCostRuralPitLatrineWithFSM : Series
    { }

    // rural high cost - safely managed  
    [Table("SeriesWSSJMPSanitationCostRuralPitLatrineWST")]
    internal partial class SeriesWSSJMPSanitationCostRuralPitLatrineWST : Series
    { }

    //Urban
    // no cost for ODF, Unimproved

    // no cost for limited

    // Urban low cost - basic  
    [Table("SeriesWSSJMPSanitationCostUrbanWetPitLatrine")]
    internal partial class SeriesWSSJMPSanitationCostUrbanWetPitLatrine : Series
    { }

    // Urban high cost - basic  
    [Table("SeriesWSSJMPSanitationCostUrbanLatrineWithSepticTank")]
    internal partial class SeriesWSSJMPSanitationCostUrbanLatrineWithSepticTank : Series
    { }

    // Urban low cost - safely managed  
    [Table("SeriesWSSJMPSanitationCostUrbanSepticTankWithFSM")]
    internal partial class SeriesWSSJMPSanitationCostUrbanSepticTankWithFSM : Series
    { }

    // Urban high cost - safely managed  
    [Table("SeriesWSSJMPSanitationCostUrbanSewerageWithTreatment")]
    internal partial class SeriesWSSJMPSanitationCostUrbanSewerageWithTreatment : Series
    { }

    //Total - no cost data for Total/national 

    //Water and Sanitation - Access rate data series for the Safely Managed Sub-Categories

    //Water (new 5 category) access rate data series for the Safely Managed Sub-Categories
    
    //rural water - access rate data for the Safely Managed Sub-Categories
    [Table("SeriesWSSJMPWaterRural%SMOnPremise")]
    internal partial class SeriesWSSJMPWaterRuralSMOnPremise : Series
    { }

    [Table("SeriesWSSJMPWaterRural%SMAsNeeded")]
    internal partial class SeriesWSSJMPWaterRuralSMAsNeeded : Series
    { }

    [Table("SeriesWSSJMPWaterRural%SMNotContam")]
    internal partial class SeriesWSSJMPWaterRuralSMNotContam : Series
    { }

    //Urban water - access rate data for the Safely Managed Sub-Categories
    [Table("SeriesWSSJMPWaterUrban%SMOnPremise")]
    internal partial class SeriesWSSJMPWaterUrbanSMOnPremise : Series
    { }

    [Table("SeriesWSSJMPWaterUrban%SMAsNeeded")]
    internal partial class SeriesWSSJMPWaterUrbanSMAsNeeded : Series
    { }

    [Table("SeriesWSSJMPWaterUrban%SMNotContam")]
    internal partial class SeriesWSSJMPWaterUrbanSMNotContam : Series
    { }

    //Total water - access rate data for the Safely Managed Sub-Categories
    [Table("SeriesWSSJMPWaterTotal%SMOnPremise")]
    internal partial class SeriesWSSJMPWaterTotalSMOnPremise : Series
    { }

    [Table("SeriesWSSJMPWaterTotal%SMAsNeeded")]
    internal partial class SeriesWSSJMPWaterTotalSMAsNeeded : Series
    { }

    [Table("SeriesWSSJMPWaterTotal%SMNotContam")]
    internal partial class SeriesWSSJMPWaterTotalSMNotContam : Series
    { }

    //Water - Access rate data series for the Piped
    //rural
    [Table("SeriesWSSJMPWaterRural%Piped")]
    internal partial class SeriesWSSJMPWaterRuralPiped : Series
    { }

    //urban
    [Table("SeriesWSSJMPWaterUrban%Piped")]
    internal partial class SeriesWSSJMPWaterUrbanPiped : Series
    { }

    //Total

    /* already declared with 3 category
    [Table("SeriesWSSJMPWaterTotal%Piped")]
    internal partial class SeriesWSSJMPWaterTotalPiped : Series
    { } */


    //Water - Access rate data series for the AtLeastBasic
    //rural
    [Table("SeriesWSSJMPWaterRural%AtLeastBasic")]
    internal partial class SeriesWSSJMPWaterRuralAtLeastBasic : Series
    { }

    //urban
    [Table("SeriesWSSJMPWaterUrban%AtLeastBasic")]
    internal partial class SeriesWSSJMPWaterUrbanAtLeastBasic : Series
    { }

    //Total
     
    [Table("SeriesWSSJMPWaterTotal%AtLeastBasic")]
    internal partial class SeriesWSSJMPWaterTotalAtLeastBasic : Series
    { } 

    //Sanitation (new 5 category) access rate data series for the Safely Managed Sub-Categories

    //rural Sanitation - access rate data for the Safely Managed Sub-Categories
    [Table("SeriesWSSJMPSanitationRural%SMDisposedInSitu")]
    internal partial class SeriesWSSJMPSanitationRuralSMDisposedInSitu : Series
    { }

    [Table("SeriesWSSJMPSanitationRural%SMEmpTreated")]
    internal partial class SeriesWSSJMPSanitationRuralSMEmpTreated : Series
    { }

    [Table("SeriesWSSJMPSanitationRural%SMWastewaterTreated")]
    internal partial class SeriesWSSJMPSanitationRuralSMWastewaterTreated : Series
    { }

    //Urban Sanitation - access rate data for the Safely Managed Sub-Categories
    [Table("SeriesWSSJMPSanitationUrban%SMDisposedInSitu")]
    internal partial class SeriesWSSJMPSanitationUrbanSMDisposedInSitu : Series
    { }

    [Table("SeriesWSSJMPSanitationUrban%SMEmpTreated")]
    internal partial class SeriesWSSJMPSanitationUrbanSMEmpTreated : Series
    { }

    [Table("SeriesWSSJMPSanitationUrban%SMWastewaterTreated")]
    internal partial class SeriesWSSJMPSanitationUrbanSMWastewaterTreated : Series
    { }

    //Total Sanitation - access rate data for the Safely Managed Sub-Categories
    [Table("SeriesWSSJMPSanitationTotal%SMDisposedInSitu")]
    internal partial class SeriesWSSJMPSanitationTotalSMDisposedInSitu : Series
    { }

    [Table("SeriesWSSJMPSanitationTotal%SMEmpTreated")]
    internal partial class SeriesWSSJMPSanitationTotalSMEmpTreated : Series
    { }

    [Table("SeriesWSSJMPSanitationTotal%SMWastewaterTreated")]
    internal partial class SeriesWSSJMPSanitationTotalSMWastewaterTreated : Series
    { }

    //Sanitation - Access rate data series for the AtLeastBasic
    //rural
    [Table("SeriesWSSJMPSanitationRural%AtLeastBasic")]
    internal partial class SeriesWSSJMPSanitationRuralAtLeastBasic : Series
    { }

    //urban
    [Table("SeriesWSSJMPSanitationUrban%AtLeastBasic")]
    internal partial class SeriesWSSJMPSanitationUrbanAtLeastBasic : Series
    { }

    //Total

    [Table("SeriesWSSJMPSanitationTotal%AtLeastBasic")]
    internal partial class SeriesWSSJMPSanitationTotalAtLeastBasic : Series
    { }

    //jrs 2023/11/10
    [Table("SeriesElectricityAccessPerIMAGELIFETP")]
    internal partial class SeriesElectricityAccessPerIMAGELIFETP : Series
    { }
    [Table("SeriesSanitationAccessImprovedPerIMAGELIFETP")]
    internal partial class SeriesSanitationAccessImprovedPerIMAGELIFETP : Series
    { }
    [Table("SeriesWaterImprovedAccessPerIMAGELIFETP")]
    internal partial class SeriesWaterImprovedAccessPerIMAGELIFETP : Series
    { }

    [Table("SeriesElectricityAccessPerIMAGETECHTP")]
    internal partial class SeriesElectricityAccessPerIMAGETECHTP : Series
    { }
    [Table("SeriesSanitationAccessImprovedPerIMAGETECHTP")]
    internal partial class SeriesSanitationAccessImprovedPerIMAGETECHTP : Series
    { }
    [Table("SeriesWaterImprovedAccessPerIMAGETECHTP")]
    internal partial class SeriesWaterImprovedAccessPerIMAGETECHTP : Series
    { }

    //jrs 2023/11/30
    [Table("SeriesElectricityAccessPerIMAGEREFTP")]
    internal partial class SeriesElectricityAccessPerIMAGEREFTP : Series
    { }
    [Table("SeriesSanitationAccessImprovedPerIMAGEREFTP")]
    internal partial class SeriesSanitationAccessImprovedPerIMAGEREFTP : Series
    { }
    [Table("SeriesWaterImprovedAccessMillionsIMAGEREFTP")]
    internal partial class SeriesWaterImprovedAccessMillionsIMAGEREFTP : Series
    { }

    [Table("SeriesElectricityAccessPerIMAGEREF1P5C")]
    internal partial class SeriesElectricityAccessPerIMAGEREF1P5C : Series
    { }
    [Table("SeriesSanitationAccessImprovedPerIMAGEREF1P5C")]
    internal partial class SeriesSanitationAccessImprovedPerIMAGEREF1P5C : Series
    { }
    [Table("SeriesWaterImprovedAccessPerIMAGEREF1P5C")]
    internal partial class SeriesWaterImprovedAccessPerIMAGEREF1P5C : Series
    { }
    //jrs 2024/02/19
    [Table("SeriesExoSANITATIONIMPROVED")]
    internal partial class SeriesExoSANITATIONIMPROVED : Series
    { }
    [Table("SeriesExoWATSAFEPIPED")]
    internal partial class SeriesExoWATSAFEPIPED : Series
    { }


    // mti 2022/11/14 end

    internal partial class IFsHistSeriesContext
    {
        //jrs 2024/02/19
        protected virtual DbSet<SeriesExoSANITATIONIMPROVED> SeriesExoSANITATIONIMPROVED { get; set; }
        protected virtual DbSet<SeriesExoWATSAFEPIPED> SeriesExoWATSAFEPIPED     { get; set; }

        //jrs 2023/11/10
        protected virtual DbSet<SeriesElectricityAccessPerIMAGELIFETP> SeriesElectricityAccessPerIMAGELIFETP { get; set; }
        protected virtual DbSet<SeriesSanitationAccessImprovedPerIMAGELIFETP> SeriesSanitationAccessImprovedPerIMAGELIFETP { get; set; }
        protected virtual DbSet<SeriesWaterImprovedAccessPerIMAGELIFETP> SeriesWaterImprovedAccessPerIMAGELIFETP { get; set; }
        protected virtual DbSet<SeriesElectricityAccessPerIMAGETECHTP> SeriesElectricityAccessPerIMAGETECHTP { get; set; }
        protected virtual DbSet<SeriesSanitationAccessImprovedPerIMAGETECHTP> SeriesSanitationAccessImprovedPerIMAGETECHTP { get; set; }
        protected virtual DbSet<SeriesWaterImprovedAccessPerIMAGETECHTP> SeriesWaterImprovedAccessPerIMAGETECHTP { get; set; }
        //jrs 2023/11/30
        protected virtual DbSet<SeriesElectricityAccessPerIMAGEREFTP> SeriesElectricityAccessPerIMAGEREFTP { get; set; }
        protected virtual DbSet<SeriesSanitationAccessImprovedPerIMAGEREFTP> SeriesSanitationAccessImprovedPerIMAGEREFTP { get; set; }
        protected virtual DbSet<SeriesWaterImprovedAccessMillionsIMAGEREFTP> SeriesWaterImprovedAccessMillionsIMAGEREFTP { get; set; }

        protected virtual DbSet<SeriesElectricityAccessPerIMAGEREF1P5C> SeriesElectricityAccessPerIMAGEREF1P5C { get; set; }
        protected virtual DbSet<SeriesSanitationAccessImprovedPerIMAGEREF1P5C> SeriesSanitationAccessImprovedPerIMAGEREF1P5C { get; set; }
        protected virtual DbSet<SeriesWaterImprovedAccessPerIMAGEREF1P5C> SeriesWaterImprovedAccessPerIMAGEREF1P5C { get; set; }



        protected virtual DbSet<SeriesWHO2012WaterUnImpFiltered> SeriesWHO2012WaterUnImpFiltered { get; set; }
        protected virtual DbSet<SeriesWHO2012WaterUnImpNoFilter> SeriesWHO2012WaterUnImpNoFilter { get; set; }
        protected virtual DbSet<SeriesWHO2012WaterOthImpFiltered> SeriesWHO2012WaterOthImpFiltered { get; set; }
        protected virtual DbSet<SeriesWHO2012WaterOthImpNoFilter> SeriesWHO2012WaterOthImpNoFilter { get; set; }
        protected virtual DbSet<SeriesWHO2012WaterPipedFiltered> SeriesWHO2012WaterPipedFiltered { get; set; }
        protected virtual DbSet<SeriesWHO2012WaterPipedNoFilter> SeriesWHO2012WaterPipedNoFilter { get; set; }

        protected virtual DbSet<SeriesEnElecTotalCapacityEIA> SeriesEnElecTotalCapacityEIA { get; set; }
        protected virtual DbSet<SeriesEnElecAccessUrban> SeriesEnElecAccessUrban { get; set; }
        protected virtual DbSet<SeriesEnElecAccessRural> SeriesEnElecAccessRural { get; set; }
        protected virtual DbSet<SeriesEnElecAccessNational> SeriesEnElecAccessNational { get; set; }
        protected virtual DbSet<SeriesEnElecTransLoss> SeriesEnElecTransLoss { get; set; }
        protected virtual DbSet<SeriesEnProdElec> SeriesEnProdElec { get; set; }

        protected virtual DbSet<SeriesEnElecShrEnDem> SeriesEnElecShrEnDem { get; set; }
        protected virtual DbSet<SeriesEnElecConsPerCap> SeriesEnElecConsPerCap { get; set; }
        protected virtual DbSet<SeriesRoadsTotalNetwork> SeriesRoadsTotalNetwork { get; set; }
        protected virtual DbSet<SeriesRoadsPaved> SeriesRoadsPaved { get; set; }
        protected virtual DbSet<SeriesRoadPavedKm> SeriesRoadPavedKm { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationTotalImproved> SeriesWSSJMPSanitationTotalImproved { get; set; }

        protected virtual DbSet<SeriesWSSJMPSanitationTotalShared> SeriesWSSJMPSanitationTotalShared { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationTotalOpenDefecation> SeriesWSSJMPSanitationTotalOpenDefecation { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationTotalOtherUnimproved> SeriesWSSJMPSanitationTotalOtherUnimproved { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterTotalPiped> SeriesWSSJMPWaterTotalPiped { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterTotalOtherImproved> SeriesWSSJMPWaterTotalOtherImproved { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterTotalOtherUnImproved> SeriesWSSJMPWaterTotalOtherUnImproved { get; set; }

        protected virtual DbSet<SeriesWSSJMPWaterTotalSurface> SeriesWSSJMPWaterTotalSurface { get; set; }
        protected virtual DbSet<SeriesWasteWaterColConnect> SeriesWasteWaterColConnect { get; set; }
        protected virtual DbSet<SeriesWasteWaterTreatConnect> SeriesWasteWaterTreatConnect { get; set; }
        protected virtual DbSet<SeriesICTTelephoneLinesPer100> SeriesICTTelephoneLinesPer100 { get; set; }
        protected virtual DbSet<SeriesICTComputerHousehold> SeriesICTComputerHousehold { get; set; }
        protected virtual DbSet<SeriesICTInternetHousehold> SeriesICTInternetHousehold { get; set; }

        protected virtual DbSet<SeriesRoadRuralAccessIndex> SeriesRoadRuralAccessIndex { get; set; }
        protected virtual DbSet<SeriesLandAgri> SeriesLandAgri { get; set; }
        protected virtual DbSet<SeriesLandIrPotential> SeriesLandIrPotential { get; set; }
        protected virtual DbSet<SeriesLandIrPotentialReached> SeriesLandIrPotentialReached { get; set; }
        protected virtual DbSet<SeriesLandIrAreaEquip> SeriesLandIrAreaEquip { get; set; }
        protected virtual DbSet<SeriesICTTelephoneCellSubscribersPer100> SeriesICTTelephoneCellSubscribersPer100 { get; set; }

        protected virtual DbSet<SeriesICTBroadbandSubscribersPer100ITU> SeriesICTBroadbandSubscribersPer100ITU { get; set; }
        protected virtual DbSet<SeriesICTBroadbandMobileSubsPer100> SeriesICTBroadbandMobileSubsPer100 { get; set; }
        protected virtual DbSet<SeriesICTCYBICTDevelopmentIndexITU> SeriesICTCYBICTDevelopmentIndexITU { get; set; }
        protected virtual DbSet<SeriesICTExpendGDP> SeriesICTExpendGDP { get; set; }
        protected virtual DbSet<SeriesGovernanceEffect> SeriesGovernanceEffect { get; set; }
        protected virtual DbSet<SeriesGovernanceRegQual> SeriesGovernanceRegQual { get; set; }

        protected virtual DbSet<SeriesEnConTotalWDI> SeriesEnConTotalWDI { get; set; }
        protected virtual DbSet<SeriesHouseholds> SeriesHouseholds { get; set; }

        //mti 2022/11/14 
        //Water and Sanitation - Access rate data series declared in the partial class

        protected virtual DbSet<SeriesWSSJMPWaterRuralSurface> SeriesWSSJMPWaterRuralSurface { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterRuralUnimproved> SeriesWSSJMPWaterRuralUnimproved { get; set; } 
        protected virtual DbSet<SeriesWSSJMPWaterRuralLimited> SeriesWSSJMPWaterRuralLimited { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterRuralBasic> SeriesWSSJMPWaterRuralBasic { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterRuralSafelyManaged> SeriesWSSJMPWaterRuralSafelyManaged { get; set; }

        //Urban water - access rate data
        protected virtual DbSet<SeriesWSSJMPWaterUrbanSurface> SeriesWSSJMPWaterUrbanSurface { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterUrbanUnimproved> SeriesWSSJMPWaterUrbanUnimproved { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterUrbanLimited> SeriesWSSJMPWaterUrbanLimited { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterUrbanBasic> SeriesWSSJMPWaterUrbanBasic { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterUrbanSafelyManaged> SeriesWSSJMPWaterUrbanSafelyManaged { get; set; }

        //National water - access rate data
        /* Surface - Already opened in three category Watsan code
         [Table("SeriesWSSJMPWaterTotal%Surface")]
        protected virtual DbSet<SeriesWSSJMPWaterTotalSurface  { get; set; }
        { } */

        protected virtual DbSet<SeriesWSSJMPWaterTotalUnimproved> SeriesWSSJMPWaterTotalUnimproved { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterTotalLimited> SeriesWSSJMPWaterTotalLimited { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterTotalBasic>SeriesWSSJMPWaterTotalBasic  { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterTotalSafelyManaged>SeriesWSSJMPWaterTotalSafelyManaged  { get; set; }

        //Sanitation (new 5 category) access rate data series declared in the partial class
        //rural
        protected virtual DbSet<SeriesWSSJMPSanitationRuralOpenDefecation> SeriesWSSJMPSanitationRuralOpenDefecation { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationRuralUnimproved> SeriesWSSJMPSanitationRuralUnimproved { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationRuralLimited> SeriesWSSJMPSanitationRuralLimited { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationRuralBasic> SeriesWSSJMPSanitationRuralBasic { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationRuralSafelyManaged> SeriesWSSJMPSanitationRuralSafelyManaged { get; set; }
 
        //Urban
        protected virtual DbSet<SeriesWSSJMPSanitationUrbanOpenDefecation> SeriesWSSJMPSanitationUrbanOpenDefecation { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationUrbanUnimproved> SeriesWSSJMPSanitationUrbanUnimproved { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationUrbanLimited> SeriesWSSJMPSanitationUrbanLimited { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationUrbanBasic> SeriesWSSJMPSanitationUrbanBasic { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationUrbanSafelyManaged> SeriesWSSJMPSanitationUrbanSafelyManaged { get; set; }

        //Total
        /* Open Defecation Already opened in three category Watsan code
         * [Table("SeriesWSSJMPSanitationTotal%OpenDefecation")]
        protected virtual DbSet<SeriesWSSJMPSanitationTotalOpenDefecation  { get; set; }
        { }*/

        protected virtual DbSet<SeriesWSSJMPSanitationTotalUnimproved> SeriesWSSJMPSanitationTotalUnimproved { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationTotalLimited> SeriesWSSJMPSanitationTotalLimited { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationTotalBasic> SeriesWSSJMPSanitationTotalBasic { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationTotalSafelyManaged> SeriesWSSJMPSanitationTotalSafelyManaged { get; set; }

        // Water and Sanitation - Cost for (applicable categories) - rural and urban only

        //Water and Sanitation - Cost data series declared in the partial class
        //Water (new 5 category) cost data series declared in the partial class
        //rural water - cost data

        // no cost for water surface, unimproved, or limited

        //water cost - rural basic - lower cost 
        protected virtual DbSet<SeriesWSSJMPWaterCostRuralDugwell> SeriesWSSJMPWaterCostRuralDugwell { get; set; }

        //water cost - rural basic - higher cost 
        protected virtual DbSet<SeriesWSSJMPWaterCostRuralTubewellBorehole> SeriesWSSJMPWaterCostRuralTubewellBorehole { get; set; }

        //water cost - rural safely managed - lower cost 
        protected virtual DbSet<SeriesWSSJMPWaterCostRuralPipedOnPlot> SeriesWSSJMPWaterCostRuralPipedOnPlot { get; set; }

        //Urban WaterCost - access rate data
        //water cost - Urban basic - lower cost 
        protected virtual DbSet<SeriesWSSJMPWaterCostUrbanDugwell> SeriesWSSJMPWaterCostUrbanDugwell { get; set; }

        //water cost - Urban basic - higher cost 
        protected virtual DbSet<SeriesWSSJMPWaterCostUrbanTubewellBorehole> SeriesWSSJMPWaterCostUrbanTubewellBorehole { get; set; }

        //water cost - Urban safely managed - lower cost 
        protected virtual DbSet<SeriesWSSJMPWaterCostUrbanPipedOnPlot> SeriesWSSJMPWaterCostUrbanPipedOnPlot { get; set; }

        //SanitationCost (new 5 category) access rate data series declared in the partial class
        //rural
        // no cost for ODF


        // rural low cost - unimproved
        protected virtual DbSet<SeriesWSSJMPSanitationCostRuralUnimprovedPitLatrine> SeriesWSSJMPSanitationCostRuralUnimprovedPitLatrine { get; set; }

        // no cost for limited

        // rural low cost - basic  
        protected virtual DbSet<SeriesWSSJMPSanitationCostRuralDryPitLatrine> SeriesWSSJMPSanitationCostRuralDryPitLatrine { get; set; }

        // rural high cost - basic  
        protected virtual DbSet<SeriesWSSJMPSanitationCostRuralWetPitLatrine> SeriesWSSJMPSanitationCostRuralWetPitLatrine { get; set; }

        // rural low cost - safely managed  
        protected virtual DbSet<SeriesWSSJMPSanitationCostRuralPitLatrineWithFSM> SeriesWSSJMPSanitationCostRuralPitLatrineWithFSM { get; set; }

        // rural high cost - safely managed  
        protected virtual DbSet<SeriesWSSJMPSanitationCostRuralPitLatrineWST> SeriesWSSJMPSanitationCostRuralPitLatrineWST { get; set; }

        //Urban
        // no cost for ODF, Unimproved

        // no cost for limited

        // Urban low cost - basic  
        protected virtual DbSet<SeriesWSSJMPSanitationCostUrbanWetPitLatrine> SeriesWSSJMPSanitationCostUrbanWetPitLatrine { get; set; }

        // Urban high cost - basic  
        protected virtual DbSet<SeriesWSSJMPSanitationCostUrbanLatrineWithSepticTank> SeriesWSSJMPSanitationCostUrbanLatrineWithSepticTank { get; set; }

        // Urban low cost - safely managed  
        protected virtual DbSet<SeriesWSSJMPSanitationCostUrbanSepticTankWithFSM> SeriesWSSJMPSanitationCostUrbanSepticTankWithFSM { get; set; }

        // Urban high cost - safely managed  
        protected virtual DbSet<SeriesWSSJMPSanitationCostUrbanSewerageWithTreatment> SeriesWSSJMPSanitationCostUrbanSewerageWithTreatment { get; set; }

        //Total - no cost data for Total/national 

        //Water and Sanitation - Access rate data series for the Safely Managed Sub-Categories

        //Water (new 5 category) access rate data series for the Safely Managed Sub-Categories

        //rural water - access rate data for the Safely Managed Sub-Categories
        protected virtual DbSet<SeriesWSSJMPWaterRuralSMOnPremise> SeriesWSSJMPWaterRuralSMOnPremise { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterRuralSMAsNeeded> SeriesWSSJMPWaterRuralSMAsNeeded { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterRuralSMNotContam> SeriesWSSJMPWaterRuralSMNotContam { get; set; }


        //Urban water - access rate data for the Safely Managed Sub-Categories
        protected virtual DbSet<SeriesWSSJMPWaterUrbanSMOnPremise> SeriesWSSJMPWaterUrbanSMOnPremise { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterUrbanSMAsNeeded> SeriesWSSJMPWaterUrbanSMAsNeeded { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterUrbanSMNotContam> SeriesWSSJMPWaterUrbanSMNotContam { get; set; }

        //Total water - access rate data for the Safely Managed Sub-Categories
        protected virtual DbSet<SeriesWSSJMPWaterTotalSMOnPremise> SeriesWSSJMPWaterTotalSMOnPremise { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterTotalSMAsNeeded> SeriesWSSJMPWaterTotalSMAsNeeded { get; set; }
        protected virtual DbSet<SeriesWSSJMPWaterTotalSMNotContam> SeriesWSSJMPWaterTotalSMNotContam { get; set; }

        //Water - Access rate data series for the Piped
        //rural
        protected virtual DbSet<SeriesWSSJMPWaterRuralPiped> SeriesWSSJMPWaterRuralPiped { get; set; }

        //urban
        protected virtual DbSet<SeriesWSSJMPWaterUrbanPiped> SeriesWSSJMPWaterUrbanPiped { get; set; }

        //Total

        /* already declared with 3 category
        [Table("SeriesWSSJMPWaterTotal%Piped")]
        protected virtual DbSet<SeriesWSSJMPWaterTotalPiped  { get; set; }
        { } */


        //Water - Access rate data series for the AtLeastBasic
        //rural
        protected virtual DbSet<SeriesWSSJMPWaterRuralAtLeastBasic> SeriesWSSJMPWaterRuralAtLeastBasic { get; set; }

        //urban
        protected virtual DbSet<SeriesWSSJMPWaterUrbanAtLeastBasic> SeriesWSSJMPWaterUrbanAtLeastBasic { get; set; }

        //Total
        protected virtual DbSet<SeriesWSSJMPWaterTotalAtLeastBasic> SeriesWSSJMPWaterTotalAtLeastBasic { get; set; }

        //Sanitation (new 5 category) access rate data series for the Safely Managed Sub-Categories

        //rural Sanitation - access rate data for the Safely Managed Sub-Categories
        protected virtual DbSet<SeriesWSSJMPSanitationRuralSMDisposedInSitu> SeriesWSSJMPSanitationRuralSMDisposedInSitu { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationRuralSMEmpTreated> SeriesWSSJMPSanitationRuralSMEmpTreated { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationRuralSMWastewaterTreated> SeriesWSSJMPSanitationRuralSMWastewaterTreated { get; set; }

        //Urban Sanitation - access rate data for the Safely Managed Sub-Categories
        protected virtual DbSet<SeriesWSSJMPSanitationUrbanSMDisposedInSitu> SeriesWSSJMPSanitationUrbanSMDisposedInSitu { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationUrbanSMEmpTreated> SeriesWSSJMPSanitationUrbanSMEmpTreated { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationUrbanSMWastewaterTreated> SeriesWSSJMPSanitationUrbanSMWastewaterTreated { get; set; }

        //Total Sanitation - access rate data for the Safely Managed Sub-Categories
        protected virtual DbSet<SeriesWSSJMPSanitationTotalSMDisposedInSitu> SeriesWSSJMPSanitationTotalSMDisposedInSitu { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationTotalSMEmpTreated> SeriesWSSJMPSanitationTotalSMEmpTreated { get; set; }
        protected virtual DbSet<SeriesWSSJMPSanitationTotalSMWastewaterTreated> SeriesWSSJMPSanitationTotalSMWastewaterTreated { get; set; }

        //Sanitation - Access rate data series for the AtLeastBasic
        //rural
        protected virtual DbSet<SeriesWSSJMPSanitationRuralAtLeastBasic> SeriesWSSJMPSanitationRuralAtLeastBasic { get; set; }

        //urban
        protected virtual DbSet<SeriesWSSJMPSanitationUrbanAtLeastBasic> SeriesWSSJMPSanitationUrbanAtLeastBasic { get; set; }

        //Total
        protected virtual DbSet<SeriesWSSJMPSanitationTotalAtLeastBasic> SeriesWSSJMPSanitationTotalAtLeastBasic { get; set; }

    }
}
