using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{

    internal abstract partial class Series
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string CountryName { get; set; }
        [Column("FIPS_CODE")]
        public string FipsCode { get; set; }
        [Column("Earliest")]
        public float? EarliestValue { get; set; }
        [Column("MostRecent")]
        public float? MostRecentValue { get; set; }
    }

    [Table("SeriesAidDon%GNI")]
    internal partial class SeriesAidDon: Series
    {}

    [Table("SeriesAidRec%GNI")]
    internal partial class SeriesAidRec: Series
    {}

    [Table("SeriesEdBasicEnrollGross%Total")]
    internal partial class SeriesEdBasicEnrollGrossTotal: Series
    {}

    [Table("SeriesEdBasicEnrollGross%Male")]
    internal partial class SeriesEdBasicEnrollGrossMale: Series
    {}

    [Table("SeriesEdBasicEnrollGross%Female")]
    internal partial class SeriesEdBasicEnrollGrossFemale: Series
    {}

    [Table("SeriesGDP2011")]
    internal partial class SeriesGDP2011: Series
    {}

    [Table("SeriesGDP2017")]
    internal partial class SeriesGDP2017 : Series
    { }

    [Table("SeriesGDP2021")]
    internal partial class SeriesGDP2021 : Series
    { }

    [Table("SeriesGDP2000PCPPP")]
    internal partial class SeriesGDP2000PCPPP : Series
    { }

    [Table("SeriesGDP2005PCPPP")]
    internal partial class SeriesGDP2005PCPPP : Series
    { }

    [Table("SeriesGDP2011PCPPP")]
    internal partial class SeriesGDP2011PCPPP : Series
    { }

    [Table("SeriesGDP2017PCPPP")]
    internal partial class SeriesGDP2017PCPPP : Series
    { }

    [Table("SeriesGDP2021PCPPP")]
    internal partial class SeriesGDP2021PCPPP : Series
    { }

    [Table("SeriesTFRMedUNPD")]
    internal partial class SeriesTFRMedUNPD : Series
    { }

    [Table("SeriesTFR")]
    internal partial class SeriesTFR : Series
    { }

    [Table("SeriesInfMortMedUNPD")]
    internal partial class SeriesINFMOR : Series
    { }

    [Table("SeriesInfMortRateIHME")]
    internal partial class SeriesInfMortRateIHME : Series
    { }

    [Table("SeriesLandArea")]
    internal partial class SeriesLandArea : Series
    { }

    [Table("SeriesPopulation")]
    internal partial class SeriesPopulation : Series
    { }

    [Table("SeriesMaternalMortalityRatio")]
    internal partial class SeriesMATMORTRATIO : Series
    { }

    [Table("SeriesLifExpectMale")]
    internal partial class SeriesLifExpectMale : Series
    { }

    [Table("SeriesLifExpectFemale")]
    internal partial class SeriesLifExpectFemale : Series
    { }
    //cdv 2022/02/28 ****START****
    [Table("SeriesAvgAgeAtFirstMarriageMen-WB")]
    internal partial class SeriesAvgAgeAtFirstMarriageMenWB : Series
    { }

    [Table("SeriesAvgAgeAtFirstMarriageMen-UN")]
    internal partial class SeriesAvgAgeAtFirstMarriageMenUN : Series
    { }
    [Table("SeriesAvgAgeAtFirstMarriageWomen-WB")]
    internal partial class SeriesAvgAgeAtFirstMarriageWomenWB : Series
    { }

    [Table("SeriesAvgAgeAtFirstMarriageWomen-UN")]
    internal partial class SeriesAvgAgeAtFirstMarriageWomenUN : Series
    { }
    //cdv 2022/02/28 ****END****

    //cdv 2022/10/28 ****START****
    [Table("SeriesGenderUnpaidDomesticWorkTimeFemale")]
internal partial class SeriesGenderUnpaidDomesticWorkTimeFemale : Series
    { }
    [Table("SeriesGenderUnpaidDomesticWorkTimeMale")]
    internal partial class SeriesGenderUnpaidDomesticWorkTimeMale : Series
    { }

    //cdv 2022/10/28 ****END****

    //cdv 2022/12/12 ****START****
    [Table("SeriesFoodInsecPopMod%FemaleFIES")]
    internal partial class SeriesFoodInsecPopModFemaleFIES : Series
    { }

    [Table("SeriesFoodInsecPopMod%MaleFIES")]
    internal partial class SeriesFoodInsecPopModMaleFIES : Series
    { }
    //cdv 2022/12/12 ****END****
    [Table("SeriesLifeExpectIHMEMaleHistOnly")]
    internal partial class SeriesLifeExpectIHMEMaleHistOnly : Series
    { }

    [Table("SeriesLifeExpectIHMEFemaleHistOnly")]
    internal partial class SeriesLifeExpectIHMEFemaleHistOnly : Series
    { }

    [Table("SeriesPopContrUse%WDI")]
    internal partial class SeriesCONTRUSE : Series
    { }

    [Table("SeriesIlliteracy%")]
    internal partial class SeriesILIT : Series
    { }

    [Table("SeriesLiteracyAdultTotal%")]
    internal partial class SeriesLIT : Series
    { }

    [Table("SeriesLabor")]
    internal partial class SeriesLAPOPR : Series
    { }

    [Table("SeriesLaborFemale%")]
    internal partial class SeriesFEMSHRLAB : Series
    { }

    [Table("SeriesLaborParRate15PlusFemale%")]
    internal partial class SeriesLABPARR_Female : Series
    { }

    [Table("SeriesLaborParRate15PlusMale%")]
    internal partial class SeriesLABPARR_Male : Series
    { }

    [Table("SeriesLaborParRate15PlusTotal%")]
    internal partial class SeriesLABPARR_Total : Series
    { }

    [Table("SeriesLaborUnemploy%")]
    internal partial class SeriesLABUNEMPR : Series
    { }
    //cdv 2022/10/28 ****END****
    [Table("SeriesLaborWageSexRatio")]
    internal partial class SeriesLABWAGESEXRAT : Series
    { }

    //[Table("SeriesLaborInformal%TotalAllBlended")] 
    [Table("SeriesLaborInformal%NonAgILO")] 
    internal partial class SeriesLABINFORMSHR : Series
    { }

    [Table("SeriesLaborInformal%TotalAllWorldBank")]
    internal partial class SeriesLaborInformalTotalAllWorldBank : Series
    { }
    
    [Table("SeriesLaborInformal%TotalFemaleWB")]
    internal partial class SeriesLabInfPcntNAgFem : Series
    { }

    [Table("SeriesLaborInformal%TotalMaleWB")]
    internal partial class SeriesLabInfPcntNAgMale : Series
    { }

    [Table("SeriesLaborAgr%TotFemale")]
    internal partial class SeriesLabAgrPcntTotFem : Series
    { }

    [Table("SeriesLaborAgr%TotMale")]
    internal partial class SeriesLabAgrPcntTotMale : Series
    { }

    [Table("SeriesCBR")]
    internal partial class SeriesCBR : Series
    { }

    [Table("SeriesCDR")]
    internal partial class SeriesCDR : Series
    { }

    [Table("SeriesHealthUNAIDSTotalHIVMidEst")]
    internal partial class SeriesHIVRATE : Series
    { }

    [Table("SeriesHealthUNAIDSTotalHIVHighEst")]
    internal partial class SeriesHIVRATEHigh : Series
    { }

    [Table("SeriesHealthUNAIDSTotalHIVLowEst")]
    internal partial class SeriesHIVRATELow : Series
    { }

    [Table("SeriesHIVPeaks")]
    internal partial class SeriesHIVPeaks
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string CountryName { get; set; }
        [Column("FIPS_CODE")]
        public string FipsCode { get; set; }
        [Column("PeakYear")]
        public float? PeakYear { get; set; }
        [Column("PeakPrevalence")]
        public float? PeakPrevalence { get; set; }
    }

    [Table("SeriesHealthUNAIDSDeathsMidEst")]
    internal partial class SeriesAIDSDTHS : Series
    { }

    [Table("SeriesHealthUNAIDSDeathsHighEst")]
    internal partial class SeriesAIDSDthsHigh : Series
    { }

    [Table("SeriesHealthUNAIDSDeathsLowEst")]
    internal partial class SeriesAIDSDthsLow : Series
    { }

    [Table("SeriesPopulationUrban")]
    internal partial class SeriesPOPURBAN : Series
    { }

    //jrs 2026/05/18  [Table("SeriesPopulationYouthDep%")]
    [Table("SeriesYouthDep%UNPD")]
    internal partial class SeriesAgeRatio : Series
    { }

    [Table("SeriesForecastNetMigrationRateUNPD")]
    internal partial class SeriesForecastNetMigrationRateUNPD : Series
    { }

    [Table("SeriesForecastNetMigrationUNPD")]
    internal partial class SeriesForecastNetMigrationUNPD : Series
    { }

    [Table("SeriesForecastNetMigrationRateMergedUNPDAbel")]
    internal partial class SeriesForecastNetMigrationRateMergedUNPDAbel : Series
    { }

    [Table("SeriesForecastNetMigrationMergedUNPDAbel")]
    internal partial class SeriesForecastNetMigrationMergedUNPDAbel : Series
    { }

    [Table("SeriesHealthMortNeoNatRate")]
    internal partial class SeriesHealthMortNeoNatRate : Series
    { }

    [Table("SeriesForecastMigrantStockIn")]
    internal partial class SeriesForecastMigrantStockIn : Series
    { }

    [Table("SeriesForecastMigrantStockOut")]
    internal partial class SeriesForecastMigrantStockOut : Series
    { }

    [Table("SeriesForecastMigrantFlowIn")]
    internal partial class SeriesForecastMigrantFlowIn : Series
    { }

    [Table("SeriesForecastMigrantFlowOut")]
    internal partial class SeriesForecastMigrantFlowOut : Series
    { }

    [Table("SeriesForecastMigrantRateIn")]
    internal partial class SeriesForecastMigrantRateIn : Series
    { }

    [Table("SeriesForecastMigrantRateOut")]
    internal partial class SeriesForecastMigrantRateOut : Series
    { }

    //jrs 2020/07/15
    [Table("SeriesForecastTotFertilityIIASASSP1")]
    internal partial class SeriesForecastTotFertilityIIASASSP1 : Series
    { }

    [Table("SeriesForecastPopUrbanShrNCARSSP1")]
    internal partial class SeriesForecastPopUrbanShrNCARSSP1 : Series
    { }

    [Table("SeriesForecastBirthsIIASASSP1")]
    internal partial class SeriesForecastBirthsIIASASSP1 : Series
    { }

    [Table("SeriesForecastDeathsIIASASSP1")]
    internal partial class SeriesForecastDeathsIIASASSP1 : Series
    { }

    [Table("SeriesForecastPopIIASASSP1")]
    internal partial class SeriesForecastPopIIASASSP1 : Series
    { }

    [Table("SeriesForecastTotFertilityIIASASSP2")]
    internal partial class SeriesForecastTotFertilityIIASASSP2 : Series
    { }

    [Table("SeriesForecastPopUrbanShrNCARSSP2")]
    internal partial class SeriesForecastPopUrbanShrNCARSSP2 : Series
    { }

    [Table("SeriesForecastMYSMaleAbove15YRsIIASASSP2")]
    internal partial class SeriesForecastMYSMaleAbove15YRsIIASASSP2 : Series
    { }

    [Table("SeriesForecastMYSFemaleAbove15YRsIIASASSP2")]
    internal partial class SeriesForecastMYSFemaleAbove15YRsIIASASSP2 : Series
    { }

    [Table("SeriesForecastMYSBothSexesAbove15YRsIIASASSP2")]
    internal partial class SeriesForecastMYSBothSexesAbove15YRsIIASASSP2 : Series
    { }

    [Table("SeriesForecastMYSMaleAbove25YRsIIASASSP2")]
    internal partial class SeriesForecastMYSMaleAbove25YRsIIASASSP2 : Series
    { }

    [Table("SeriesForecastMYSFemaleAbove25YRsIIASASSP2")]
    internal partial class SeriesForecastMYSFemaleAbove25YRsIIASASSP2 : Series
    { }

    [Table("SeriesForecastMYSBothSexesAbove25YRsIIASASSP2")]
    internal partial class SeriesForecastMYSBothSexesAbove25YRsIIASASSP2 : Series
    { }

    [Table("SeriesForecastBirthsIIASASSP2")]
    internal partial class SeriesForecastBirthsIIASASSP2 : Series
    { }

    [Table("SeriesForecastDeathsIIASASSP2")]
    internal partial class SeriesForecastDeathsIIASASSP2 : Series
    { }

    [Table("SeriesForecastPopIIASASSP2")]
    internal partial class SeriesForecastPopIIASASSP2 : Series
    { }

    [Table("SeriesForecastTotFertilityIIASASSP3")]
    internal partial class SeriesForecastTotFertilityIIASASSP3 : Series
    { }

    [Table("SeriesForecastPopUrbanShrNCARSSP3")]
    internal partial class SeriesForecastPopUrbanShrNCARSSP3 : Series
    { }

    [Table("SeriesForecastBirthsIIASASSP3")]
    internal partial class SeriesForecastBirthsIIASASSP3 : Series
    { }

    [Table("SeriesForecastDeathsIIASASSP3")]
    internal partial class SeriesForecastDeathsIIASASSP3 : Series
    { }

    [Table("SeriesForecastPopIIASASSP3")]
    internal partial class SeriesForecastPopIIASASSP3 : Series
    { }

    [Table("SeriesForecastTotFertilityIIASASSP4")]
    internal partial class SeriesForecastTotFertilityIIASASSP4 : Series
    { }

    [Table("SeriesForecastPopUrbanShrNCARSSP4")]
    internal partial class SeriesForecastPopUrbanShrNCARSSP4 : Series
    { }

    [Table("SeriesForecastMYSMaleAbove15YRsIIASASSP4")]
    internal partial class SeriesForecastMYSMaleAbove15YRsIIASASSP4 : Series
    { }

    [Table("SeriesForecastMYSFemaleAbove15YRsIIASASSP4")]
    internal partial class SeriesForecastMYSFemaleAbove15YRsIIASASSP4 : Series
    { }

    [Table("SeriesForecastMYSBothSexesAbove15YRsIIASASSP4")]
    internal partial class SeriesForecastMYSBothSexesAbove15YRsIIASASSP4 : Series
    { }

    [Table("SeriesForecastMYSMaleAbove25YRsIIASASSP4")]
    internal partial class SeriesForecastMYSMaleAbove25YRsIIASASSP4 : Series
    { }

    [Table("SeriesForecastMYSFemaleAbove25YRsIIASASSP4")]
    internal partial class SeriesForecastMYSFemaleAbove25YRsIIASASSP4 : Series
    { }

    [Table("SeriesForecastMYSBothSexesAbove25YRsIIASASSP4")]
    internal partial class SeriesForecastMYSBothSexesAbove25YRsIIASASSP4 : Series
    { }

    [Table("SeriesGenderManagersFemaleShare")]
    internal partial class SeriesGenderManagersFemaleShare : Series
    { }

    [Table("SeriesMarriageRatesWomen15to19")]
    internal partial class SeriesMarriageRatesWomen15to19 : Series
    { }

    [Table("SeriesMarriageRatesWomen20to24")]
    internal partial class SeriesMarriageRatesWomen20to24 : Series
    { }

    [Table("SeriesForecastBirthsIIASASSP4")]
    internal partial class SeriesForecastBirthsIIASASSP4 : Series
    { }

    [Table("SeriesForecastDeathsIIASASSP4")]
    internal partial class SeriesForecastDeathsIIASASSP4 : Series
    { }

    [Table("SeriesForecastPopIIASASSP4")]
    internal partial class SeriesForecastPopIIASASSP4 : Series
    { }

    [Table("SeriesForecastTotFertilityIIASASSP5")]
    internal partial class SeriesForecastTotFertilityIIASASSP5 : Series
    { }

    [Table("SeriesForecastPopUrbanShrNCARSSP5")]
    internal partial class SeriesForecastPopUrbanShrNCARSSP5 : Series
    { }

    [Table("SeriesForecastMYSMaleAbove15YRsIIASASSP5")]
    internal partial class SeriesForecastMYSMaleAbove15YRsIIASASSP5 : Series
    { }

    [Table("SeriesForecastMYSFemaleAbove15YRsIIASASSP5")]
    internal partial class SeriesForecastMYSFemaleAbove15YRsIIASASSP5 : Series
    { }

    [Table("SeriesForecastMYSBothSexesAbove15YRsIIASASSP5")]
    internal partial class SeriesForecastMYSBothSexesAbove15YRsIIASASSP5 : Series
    { }

    [Table("SeriesForecastMYSMaleAbove25YRsIIASASSP5")]
    internal partial class SeriesForecastMYSMaleAbove25YRsIIASASSP5 : Series
    { }

    [Table("SeriesForecastMYSFemaleAbove25YRsIIASASSP5")]
    internal partial class SeriesForecastMYSFemaleAbove25YRsIIASASSP5 : Series
    { }

    [Table("SeriesForecastMYSBothSexesAbove25YRsIIASASSP5")]
    internal partial class SeriesForecastMYSBothSexesAbove25YRsIIASASSP5 : Series
    { }

    [Table("SeriesForecastBirthsIIASASSP5")]
    internal partial class SeriesForecastBirthsIIASASSP5 : Series
    { }

    [Table("SeriesForecastDeathsIIASASSP5")]
    internal partial class SeriesForecastDeathsIIASASSP5 : Series
    { }

    [Table("SeriesForecastPopIIASASSP5")]
    internal partial class SeriesForecastPopIIASASSP5 : Series
    { }

    [Table("SeriesForecastNetMigrationRateIIASASSP1")]
    internal partial class SeriesForecastNetMigrationRateIIASASSP1 : Series
    { }

    [Table("SeriesForecastNetMigrationRateIIASASSP2")]
    internal partial class SeriesForecastNetMigrationRateIIASASSP2 : Series
    { }

    [Table("SeriesForecastNetMigrationRateIIASASSP3")]
    internal partial class SeriesForecastNetMigrationRateIIASASSP3 : Series
    { }

    [Table("SeriesForecastNetMigrationRateIIASASSP4")]
    internal partial class SeriesForecastNetMigrationRateIIASASSP4 : Series
    { }

    [Table("SeriesForecastNetMigrationRateIIASASSP5")]
    internal partial class SeriesForecastNetMigrationRateIIASASSP5 : Series
    { }

    [Table("SeriesForecastMYSMaleAbove25YRsIIASASSP3")]
    internal partial class SeriesForecastMYSMaleAbove25YRsIIASASSP3 : Series
    { }

    [Table("SeriesForecastMYSFemaleAbove25YRsIIASASSP3")]
    internal partial class SeriesForecastMYSFemaleAbove25YRsIIASASSP3 : Series
    { }

    [Table("SeriesForecastMYSBothSexesAbove25YRsIIASASSP3")]
    internal partial class SeriesForecastMYSBothSexesAbove25YRsIIASASSP3 : Series
    { }

    [Table("SeriesForecastMYSFemaleAbove15YRsIIASASSP3")]
    internal partial class SeriesForecastMYSFemaleAbove15YRsIIASASSP3 : Series
    { }

    [Table("SeriesForecastMYSMaleAbove15YRsIIASASSP3")]
    internal partial class SeriesForecastMYSMaleAbove15YRsIIASASSP3 : Series
    { }

    [Table("SeriesForecastMYSBothSexesAbove15YRsIIASASSP3")]
    internal partial class SeriesForecastMYSBothSexesAbove15YRsIIASASSP3 : Series
    { }

    [Table("SeriesForecastMigrationRateINIIASASSP1")]
    internal partial class SeriesForecastMigrationRateINIIASASSP1 : Series
    { }

    [Table("SeriesForecastMigrationRateINIIASASSP2")]
    internal partial class SeriesForecastMigrationRateINIIASASSP2 : Series
    { }

    [Table("SeriesForecastMigrationRateINIIASASSP3")]
    internal partial class SeriesForecastMigrationRateINIIASASSP3 : Series
    { }

    [Table("SeriesForecastMigrationRateINIIASASSP4")]
    internal partial class SeriesForecastMigrationRateINIIASASSP4 : Series
    { }

    [Table("SeriesForecastMigrationRateINIIASASSP5")]
    internal partial class SeriesForecastMigrationRateINIIASASSP5 : Series
    { }

    [Table("SeriesForecastMigrationRateOUTIIASASSP1")]
    internal partial class SeriesForecastMigrationRateOUTIIASASSP1 : Series
    { }

    [Table("SeriesForecastMigrationRateOUTIIASASSP2")]
    internal partial class SeriesForecastMigrationRateOUTIIASASSP2 : Series
    { }

    [Table("SeriesForecastMigrationRateOUTIIASASSP3")]
    internal partial class SeriesForecastMigrationRateOUTIIASASSP3 : Series
    { }

    [Table("SeriesForecastMigrationRateOUTIIASASSP4")]
    internal partial class SeriesForecastMigrationRateOUTIIASASSP4 : Series
    { }

    [Table("SeriesForecastMigrationRateOUTIIASASSP5")]
    internal partial class SeriesForecastMigrationRateOUTIIASASSP5 : Series
    { }

    //jrs 2023/09/01
    [Table("SeriesForecastPopIIASASSP1Rev23")]
    internal partial class SeriesForecastPopIIASASSP1Rev23 : Series
    { }
    [Table("SeriesForecastPopIIASASSP2Rev23")]
    internal partial class SeriesForecastPopIIASASSP2Rev23 : Series
    { }
    [Table("SeriesForecastPopIIASASSP3Rev23")]
    internal partial class SeriesForecastPopIIASASSP3Rev23 : Series
    { }
    [Table("SeriesForecastPopIIASASSP4Rev23")]
    internal partial class SeriesForecastPopIIASASSP4Rev23 : Series
    { }
    [Table("SeriesForecastPopIIASASSP5Rev23")]
    internal partial class SeriesForecastPopIIASASSP5Rev23 : Series
    { }
    //jrs 2023/11/10
    [Table("SeriesPopulationmillionsIMAGELIFETP")]
    internal partial class SeriesPopulationmillionsIMAGELIFETP : Series
    { }

    [Table("SeriesPopulationmillionsIMAGETECHTP")]
    internal partial class SeriesPopulationmillionsIMAGETECHTP : Series
    { }
    //jrs 2023/11/30
    [Table("SeriesPopulationmillionsIMAGEREFTP")]
    internal partial class SeriesPopulationmillionsIMAGEREFTP : Series
    { }
    [Table("SeriesPopulationmillionsIMAGEREF1P5C")]
    internal partial class SeriesPopulationmillionsIMAGEREF1P5C : Series
    { }
    //jrs 2024/02/06 
    [Table("SeriesForecastPopNMacedonia")]
    internal partial class SeriesForecastPopNMacedonia : Series
    { }
    //jrs 2024/02/19 
    [Table("SeriesExoBIRTHS")]
    internal partial class SeriesExoBIRTHS : Series
    { }
    [Table("SeriesExoMIGRATER")]
    internal partial class SeriesExoMIGRATER : Series
    { }
    [Table("SeriesExoMIGRATEOUT")]
    internal partial class SeriesExoMIGRATEOUT : Series
    { }
    [Table("SeriesExoMIGRATEIN")]
    internal partial class SeriesExoMIGRATEIN : Series
    { }
    [Table("SeriesExoDEATHS")]
    internal partial class SeriesExoDEATHS : Series
    { }
    [Table("SeriesExoPOPURBAN")]
    internal partial class SeriesExoPOPURBAN : Series
    { }
    [Table("SeriesExoTFR")]
    internal partial class SeriesExoTFR : Series
    { }
    [Table("SeriesExoPOP")]
    internal partial class SeriesExoPOP : Series
    { }
    //dsb 2026/1/30
    [Table("SeriesOutwardMigrationAbel")]
    internal partial class SeriesOutwardMigrationAbel : Series
    { }


    // TODO: declare more series models HERE AFTER,
    // NOTE: series model names should follow the pattern of Series<XxxYyyZzz>.

    internal partial class IFsHistSeriesContext
    {
        //dsb 2026/1/30
        protected virtual DbSet<SeriesOutwardMigrationAbel> SeriesOutwardMigrationAbel { get; set; }
        //jrs 2024/02/19
        protected virtual DbSet<SeriesExoBIRTHS> SeriesExoBIRTHS { get; set; }
        protected virtual DbSet<SeriesExoMIGRATER> SeriesExoMIGRATER { get; set; }
        protected virtual DbSet<SeriesExoMIGRATEOUT> SeriesExoMIGRATEOUT { get; set; }
        protected virtual DbSet<SeriesExoMIGRATEIN> SeriesExoMIGRATEIN { get; set; }
        protected virtual DbSet<SeriesExoDEATHS> SeriesExoDEATHS { get; set; }
        protected virtual DbSet<SeriesExoPOPURBAN> SeriesExoPOPURBAN { get; set; }
        protected virtual DbSet<SeriesExoTFR> SeriesExoTFR { get; set; }
        protected virtual DbSet<SeriesExoPOP> SeriesExoPOP { get; set; }
        //jrs 2024/02/06
        protected virtual DbSet<SeriesForecastPopNMacedonia> SeriesForecastPopNMacedonia { get; set; }
        //jrs 2023/11/10 
        protected virtual DbSet<SeriesPopulationmillionsIMAGELIFETP> SeriesPopulationmillionsIMAGELIFETP { get; set; }

        protected virtual DbSet<SeriesPopulationmillionsIMAGETECHTP> SeriesPopulationmillionsIMAGETECHTP { get; set; }

        //jrs 2023/11/30
        protected virtual DbSet<SeriesPopulationmillionsIMAGEREFTP> SeriesPopulationmillionsIMAGEREFTP { get; set; }
        protected virtual DbSet<SeriesPopulationmillionsIMAGEREF1P5C> SeriesPopulationmillionsIMAGEREF1P5C { get; set; }

        protected virtual DbSet<SeriesForecastPopIIASASSP1Rev23> SeriesForecastPopIIASASSP1Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastPopIIASASSP2Rev23> SeriesForecastPopIIASASSP2Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastPopIIASASSP3Rev23> SeriesForecastPopIIASASSP3Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastPopIIASASSP4Rev23> SeriesForecastPopIIASASSP4Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastPopIIASASSP5Rev23> SeriesForecastPopIIASASSP5Rev23 { get; set; }
        //jrs 2023/05/04
        protected virtual DbSet<SeriesMarriageRatesWomen15to19> MARRIEDFEMSHR_Age15_19 { get; set; }
        protected virtual DbSet<SeriesMarriageRatesWomen20to24> MARRIEDFEMSHR_Age20_24 { get; set; }

        //jrs 2020/07/15
        protected virtual DbSet<SeriesForecastTotFertilityIIASASSP1> SeriesForecastTotFertilityIIASASSP1 { get; set; }
        protected virtual DbSet<SeriesForecastPopUrbanShrNCARSSP1> SeriesForecastPopUrbanShrNCARSSP1 { get; set; }
        protected virtual DbSet<SeriesForecastBirthsIIASASSP1> SeriesForecastBirthsIIASASSP1 { get; set; }
        protected virtual DbSet<SeriesForecastDeathsIIASASSP1> SeriesForecastDeathsIIASASSP1 { get; set; }
        protected virtual DbSet<SeriesForecastPopIIASASSP1> SeriesForecastPopIIASASSP1 { get; set; }
        protected virtual DbSet<SeriesForecastTotFertilityIIASASSP2> SeriesForecastTotFertilityIIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastPopUrbanShrNCARSSP2> SeriesForecastPopUrbanShrNCARSSP2 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove15YRsIIASASSP2> SeriesForecastMYSMaleAbove15YRsIIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove15YRsIIASASSP2> SeriesForecastMYSFemaleAbove15YRsIIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove15YRsIIASASSP2> SeriesForecastMYSBothSexesAbove15YRsIIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove25YRsIIASASSP2> SeriesForecastMYSMaleAbove25YRsIIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove25YRsIIASASSP2> SeriesForecastMYSFemaleAbove25YRsIIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove25YRsIIASASSP2> SeriesForecastMYSBothSexesAbove25YRsIIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastBirthsIIASASSP2> SeriesForecastBirthsIIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastDeathsIIASASSP2> SeriesForecastDeathsIIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastPopIIASASSP2> SeriesForecastPopIIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastTotFertilityIIASASSP3> SeriesForecastTotFertilityIIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastPopUrbanShrNCARSSP3> SeriesForecastPopUrbanShrNCARSSP3 { get; set; }
        protected virtual DbSet<SeriesForecastBirthsIIASASSP3> SeriesForecastBirthsIIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastDeathsIIASASSP3> SeriesForecastDeathsIIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastPopIIASASSP3> SeriesForecastPopIIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastTotFertilityIIASASSP4> SeriesForecastTotFertilityIIASASSP4 { get; set; }
        protected virtual DbSet<SeriesForecastPopUrbanShrNCARSSP4> SeriesForecastPopUrbanShrNCARSSP4 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove15YRsIIASASSP4> SeriesForecastMYSMaleAbove15YRsIIASASSP4 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove15YRsIIASASSP4> SeriesForecastMYSFemaleAbove15YRsIIASASSP4 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove15YRsIIASASSP4> SeriesForecastMYSBothSexesAbove15YRsIIASASSP4 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove25YRsIIASASSP4> SeriesForecastMYSMaleAbove25YRsIIASASSP4 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove25YRsIIASASSP4> SeriesForecastMYSFemaleAbove25YRsIIASASSP4 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove25YRsIIASASSP4> SeriesForecastMYSBothSexesAbove25YRsIIASASSP4 { get; set; }
        protected virtual DbSet<SeriesGenderManagersFemaleShare> WOMENINLEADERSHIP { get; set; }
        protected virtual DbSet<SeriesForecastBirthsIIASASSP4> SeriesForecastBirthsIIASASSP4 { get; set; }
        protected virtual DbSet<SeriesForecastDeathsIIASASSP4> SeriesForecastDeathsIIASASSP4 { get; set; }
        protected virtual DbSet<SeriesForecastPopIIASASSP4> SeriesForecastPopIIASASSP4 { get; set; }
        protected virtual DbSet<SeriesForecastTotFertilityIIASASSP5> SeriesForecastTotFertilityIIASASSP5 { get; set; }
        protected virtual DbSet<SeriesForecastPopUrbanShrNCARSSP5> SeriesForecastPopUrbanShrNCARSSP5 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove15YRsIIASASSP5> SeriesForecastMYSMaleAbove15YRsIIASASSP5 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove15YRsIIASASSP5> SeriesForecastMYSFemaleAbove15YRsIIASASSP5 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove15YRsIIASASSP5> SeriesForecastMYSBothSexesAbove15YRsIIASASSP5 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove25YRsIIASASSP5> SeriesForecastMYSMaleAbove25YRsIIASASSP5 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove25YRsIIASASSP5> SeriesForecastMYSFemaleAbove25YRsIIASASSP5 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove25YRsIIASASSP5> SeriesForecastMYSBothSexesAbove25YRsIIASASSP5 { get; set; }
        protected virtual DbSet<SeriesForecastBirthsIIASASSP5> SeriesForecastBirthsIIASASSP5 { get; set; }
        protected virtual DbSet<SeriesForecastDeathsIIASASSP5> SeriesForecastDeathsIIASASSP5 { get; set; }
        protected virtual DbSet<SeriesForecastPopIIASASSP5> SeriesForecastPopIIASASSP5 { get; set; }
        protected virtual DbSet<SeriesForecastNetMigrationRateIIASASSP1> SeriesForecastNetMigrationRateIIASASSP1 { get; set; }
        protected virtual DbSet<SeriesForecastNetMigrationRateIIASASSP2> SeriesForecastNetMigrationRateIIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastNetMigrationRateIIASASSP3> SeriesForecastNetMigrationRateIIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastNetMigrationRateIIASASSP4> SeriesForecastNetMigrationRateIIASASSP4 { get; set; }
        protected virtual DbSet<SeriesForecastNetMigrationRateIIASASSP5> SeriesForecastNetMigrationRateIIASASSP5 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove25YRsIIASASSP3> SeriesForecastMYSMaleAbove25YRsIIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove25YRsIIASASSP3> SeriesForecastMYSFemaleAbove25YRsIIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove25YRsIIASASSP3> SeriesForecastMYSBothSexesAbove25YRsIIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove15YRsIIASASSP3> SeriesForecastMYSFemaleAbove15YRsIIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove15YRsIIASASSP3> SeriesForecastMYSMaleAbove15YRsIIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove15YRsIIASASSP3> SeriesForecastMYSBothSexesAbove15YRsIIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastMigrationRateINIIASASSP1> SeriesForecastMigrationRateINIIASASSP1 { get; set; }
        protected virtual DbSet<SeriesForecastMigrationRateINIIASASSP2> SeriesForecastMigrationRateINIIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastMigrationRateINIIASASSP3> SeriesForecastMigrationRateINIIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastMigrationRateINIIASASSP4> SeriesForecastMigrationRateINIIASASSP4 { get; set; }
        protected virtual DbSet<SeriesForecastMigrationRateINIIASASSP5> SeriesForecastMigrationRateINIIASASSP5 { get; set; }
        protected virtual DbSet<SeriesForecastMigrationRateOUTIIASASSP1> SeriesForecastMigrationRateOUTIIASASSP1 { get; set; }
        protected virtual DbSet<SeriesForecastMigrationRateOUTIIASASSP2> SeriesForecastMigrationRateOUTIIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastMigrationRateOUTIIASASSP3> SeriesForecastMigrationRateOUTIIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastMigrationRateOUTIIASASSP4> SeriesForecastMigrationRateOUTIIASASSP4 { get; set; }
        protected virtual DbSet<SeriesForecastMigrationRateOUTIIASASSP5> SeriesForecastMigrationRateOUTIIASASSP5 { get; set; }

        protected virtual DbSet<SeriesForecastMigrantRateIn> SeriesForecastMigrantRateIn { get; set; }
        protected virtual DbSet<SeriesForecastMigrantRateOut> SeriesForecastMigrantRateOut { get; set; }

        protected virtual DbSet<SeriesForecastMigrantStockIn> SeriesForecastMigrantStockIn { get; set; }
        protected virtual DbSet<SeriesForecastMigrantStockOut> SeriesForecastMigrantStockOut { get; set; }
        protected virtual DbSet<SeriesForecastMigrantFlowIn> SeriesForecastMigrantFlowIn { get; set; }
        protected virtual DbSet<SeriesForecastMigrantFlowOut> SeriesForecastMigrantFlowOut { get; set; }

        protected virtual DbSet<SeriesLaborInformalTotalAllWorldBank> SeriesLaborInformalTotalAllWorldBank { get; set; }        
        protected virtual DbSet<SeriesAidDon> aiddon { get; set; }
        protected virtual DbSet<SeriesAidRec> AIDREC { get; set; }
        protected virtual DbSet<SeriesEdBasicEnrollGrossTotal> EDBASENRG_Total { get; set; }
        protected virtual DbSet<SeriesEdBasicEnrollGrossMale> EDBASENRG_Male { get; set; }
        protected virtual DbSet<SeriesEdBasicEnrollGrossFemale> EDBASENRG_Female { get; set; }
        protected virtual DbSet<SeriesGDP2011> GDP2011 { get; set; }
        protected virtual DbSet<SeriesGDP2017> GDP2017 { get; set; }
        protected virtual DbSet<SeriesGDP2021> GDP2021 { get; set; }
        protected virtual DbSet<SeriesGDP2000PCPPP> SeriesGDP2000PCPPP { get; set; }
        protected virtual DbSet<SeriesGDP2005PCPPP> SeriesGDP2005PCPPP { get; set; }
        protected virtual DbSet<SeriesGDP2011PCPPP> GDPPCP2011 { get; set; }
        protected virtual DbSet<SeriesGDP2017PCPPP> GDPPCP2017 { get; set; }
        protected virtual DbSet<SeriesGDP2021PCPPP> GDPPCP2021 { get; set; }
        protected virtual DbSet<SeriesLandArea> LANDAREA { get; set; }
        protected virtual DbSet<SeriesPopulation> POP { get; set; }
        protected virtual DbSet<SeriesTFR> TFR { get; set; }
        protected virtual DbSet<SeriesTFRMedUNPD> TFRMedUNPD { get; set; }
        protected virtual DbSet<SeriesInfMortRateIHME> INFMOR { get; set; }
        protected virtual DbSet<SeriesMATMORTRATIO> MATMORTRATIO { get; set; }
        protected virtual DbSet<SeriesAvgAgeAtFirstMarriageMenWB> AGEOFMARRIAGE_Male { get; set; }
        protected virtual DbSet<SeriesAvgAgeAtFirstMarriageMenUN> SeriesAvgAgeAtFirstMarriageMenUN { get; set;}
        protected virtual DbSet<SeriesAvgAgeAtFirstMarriageWomenWB> AGEOFMARRIAGE_Female { get; set; }
        protected virtual DbSet<SeriesAvgAgeAtFirstMarriageWomenUN> SeriesAvgAgeAtFirstMarriageWomenUN { get; set; }
        protected virtual DbSet<SeriesGenderUnpaidDomesticWorkTimeFemale> UNPAIDLABOR_Female { get; set; }
        protected virtual DbSet<SeriesGenderUnpaidDomesticWorkTimeMale> UNPAIDLABOR_Male { get; set; }
        protected virtual DbSet<SeriesFoodInsecPopModFemaleFIES> FOODINSECURITY_Female { get; set; }
        protected virtual DbSet<SeriesFoodInsecPopModMaleFIES> FOODINSECURITY_Male { get; set; }
        protected virtual DbSet<SeriesLifeExpectIHMEMaleHistOnly> LIFEXP_Male { get; set; }
        protected virtual DbSet<SeriesLifeExpectIHMEFemaleHistOnly> LIFEXP_Female { get; set; }
        protected virtual DbSet<SeriesCONTRUSE> CONTRUSE { get; set; }
        protected virtual DbSet<SeriesILIT> ILIT { get; set; }
        protected virtual DbSet<SeriesLIT> LIT { get; set; }
        protected virtual DbSet<SeriesLAPOPR> LAPOPR { get; set; }
        protected virtual DbSet<SeriesFEMSHRLAB> FEMSHRLAB { get; set; }
        protected virtual DbSet<SeriesLABPARR_Female> LABPARR_Female { get; set; }
        protected virtual DbSet<SeriesLABPARR_Male> LABPARR_Male { get; set; }
        protected virtual DbSet<SeriesLABPARR_Total> LABPARR_Total { get; set; }
        protected virtual DbSet<SeriesLABUNEMPR> LABUNEMPR { get; set; }
        protected virtual DbSet<SeriesLABWAGESEXRAT> LABWAGESEXRAT { get; set; }
        protected virtual DbSet<SeriesLABINFORMSHR> LABINFORMSHR { get; set; }
        protected virtual DbSet<SeriesLabInfPcntNAgFem> LabInfPcntNAgFem { get; set; }
        protected virtual DbSet<SeriesLabInfPcntNAgMale> LabInfPcntNAgMale { get; set; }
        protected virtual DbSet<SeriesLabAgrPcntTotFem> LabAgrPcntTotFem { get; set; }
        protected virtual DbSet<SeriesLabAgrPcntTotMale> LabAgrPcntTotMale { get; set; }
        protected virtual DbSet<SeriesCBR> CBR { get; set; }
        protected virtual DbSet<SeriesCDR> CDR { get; set; }
        protected virtual DbSet<SeriesHIVRATE> HIVRATE { get; set; }
        protected virtual DbSet<SeriesHIVRATEHigh> HIVRATEHigh { get; set; }
        protected virtual DbSet<SeriesHIVRATELow> HIVRATELow { get; set; }
        protected virtual DbSet<SeriesHIVPeaks> hivpeakyr { get; set; }
        protected virtual DbSet<SeriesHIVPeaks> hivpeakr { get; set; }
        protected virtual DbSet<SeriesIHMEDthsHIV> AIDSDTHS { get; set; }
        protected virtual DbSet<SeriesAIDSDthsHigh> AIDSDthsHigh { get; set; }
        protected virtual DbSet<SeriesAIDSDthsLow> AIDSDthsLow { get; set; }
        protected virtual DbSet<SeriesPOPURBAN> POPURBAN { get; set; }
        protected virtual DbSet<SeriesAgeRatio> AgeRatio { get; set; }
        protected virtual DbSet<SeriesForecastNetMigrationRateUNPD> SeriesForecastNetMigrationRateUNPD { get; set; }
        protected virtual DbSet<SeriesForecastNetMigrationUNPD> SeriesForecastNetMigrationUNPD { get; set; }
        protected virtual DbSet<SeriesForecastNetMigrationRateMergedUNPDAbel> SeriesForecastNetMigrationRateMergedUNPDAbel { get; set; }
        protected virtual DbSet<SeriesForecastNetMigrationMergedUNPDAbel> SeriesForecastNetMigrationMergedUNPDAbel { get; set; }
        protected virtual DbSet<SeriesHealthMortNeoNatRate> NEONATMOR { get; set; }

        // TODO: declare more series properties HERE AFTER,
        // NOTE: property names should follow the pattern of <VAR>_<Dim2> or <VAR>_<Dim2>_<Dim3>,
        // where <VAR> is the case-sensitive variable name as found in IFsVar.mdb/IFSVAR; <Dim?> is the 
        // case-sensitive bucket name of either the 2nd or the 3rd dimensions of the variable as found 
        // in IFS.grp. the 0th (time) and the 1st (country) dimensions are excluded from the name.
    }

}
