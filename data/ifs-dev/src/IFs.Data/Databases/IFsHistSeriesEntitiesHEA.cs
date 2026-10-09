using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{
    [Table("SeriesHealthSmokingFemales%SI")]
    internal partial class SeriesHealthSmokingFemalesSI : Series
    { }
    [Table("SeriesHealthSmokingMales%SI")]
    internal partial class SeriesHealthSmokingMalesSI : Series
    { }
    [Table("SeriesHealthSmokingTCS")]
    internal partial class SeriesHealthSmokingTCS : Series
    { }
    [Table("SeriesHealthObesityFemales%")]
    internal partial class SeriesHealthObesityFemales : Series
    { }
    [Table("SeriesHealthObesityFemales30+Est%")]
    internal partial class SeriesHealthObesityFemales30Est : Series
    { }
    [Table("SeriesHealthObesityMales%")]
    internal partial class SeriesHealthObesityMales : Series
    { }
    [Table("SeriesHealthObesityMales30+Est%")]
    internal partial class SeriesHealthObesityMales30Est : Series
    { }
    [Table("SeriesHealthBMIFemales30+EstMean")]
    internal partial class SeriesHealthBMIFemales30EstMean : Series
    { }
    [Table("SeriesHealthBMIMales30+EstMean")]
    internal partial class SeriesHealthBMIMales30EstMean : Series
    { }
    [Table("SeriesHealthIGTPrev%")]
    internal partial class SeriesHealthIGTPrev : Series
    { }
    [Table("SeriesHealthDiabetesPrev%")]
    internal partial class SeriesHealthDiabetesPrev : Series
    { }
    [Table("SeriesHealthSmokingImpactMales30to44")]
    internal partial class SeriesHealthSmokingImpactMales30to44 : Series
    { }
    [Table("SeriesHealthSmokingImpactMales45to59")]
    internal partial class SeriesHealthSmokingImpactMales45to59 : Series
    { }
    [Table("SeriesHealthSmokingImpactMales60to69")]
    internal partial class SeriesHealthSmokingImpactMales60to69 : Series
    { }
    [Table("SeriesHealthSmokingImpactMales70to100")]
    internal partial class SeriesHealthSmokingImpactMales70to100 : Series
    { }
    [Table("SeriesHealthSmokingImpactFemales30to44")]
    internal partial class SeriesHealthSmokingImpactFemales30to44 : Series
    { }
    [Table("SeriesHealthSmokingImpactFemales45to59")]
    internal partial class SeriesHealthSmokingImpactFemales45to59 : Series
    { }
    [Table("SeriesHealthSmokingImpactFemales60to69")]
    internal partial class SeriesHealthSmokingImpactFemales60to69 : Series
    { }
    [Table("SeriesHealthSmokingImpactFemales70to100")]
    internal partial class SeriesHealthSmokingImpactFemales70to100 : Series
    { }
    [Table("SeriesHealthDiabetesRRMales15to29")]
    internal partial class SeriesHealthDiabetesRRMales15to29 : Series
    { }
    [Table("SeriesHealthDiabetesRRMales30to44")]
    internal partial class SeriesHealthDiabetesRRMales30to44 : Series
    { }
    [Table("SeriesHealthDiabetesRRMales45to59")]
    internal partial class SeriesHealthDiabetesRRMales45to59 : Series
    { }
    [Table("SeriesHealthDiabetesRRMales60to69")]
    internal partial class SeriesHealthDiabetesRRMales60to69 : Series
    { }
    [Table("SeriesHealthDiabetesRRMales70to79")]
    internal partial class SeriesHealthDiabetesRRMales70to79 : Series
    { }
    [Table("SeriesHealthDiabetesRRMales80to100")]
    internal partial class SeriesHealthDiabetesRRMales80to100 : Series
    { }
    [Table("SeriesHealthDiabetesRRFemales15to29")]
    internal partial class SeriesHealthDiabetesRRFemales15to29 : Series
    { }
    [Table("SeriesHealthDiabetesRRFemales30to44")]
    internal partial class SeriesHealthDiabetesRRFemales30to44 : Series
    { }
    [Table("SeriesHealthDiabetesRRFemales45to59")]
    internal partial class SeriesHealthDiabetesRRFemales45to59 : Series
    { }
    [Table("SeriesHealthDiabetesRRFemales60to69")]
    internal partial class SeriesHealthDiabetesRRFemales60to69 : Series
    { }
    [Table("SeriesHealthDiabetesRRFemales70to79")]
    internal partial class SeriesHealthDiabetesRRFemales70to79 : Series
    { }
    [Table("SeriesHealthDiabetesRRFemales80to100")]
    internal partial class SeriesHealthDiabetesRRFemales80to100 : Series
    { }
    //[Table("SeriesGDP2005PCPPP")]
    //internal partial class SeriesGDP2005PCPPP : Series
    //{ }
    //[Table("SeriesGDP2011PCPPP")]
    //internal partial class SeriesGDP2011PCPPP : Series
    //{ }
    [Table("SeriesGDP2005")]
    internal partial class SeriesGDP2005 : Series
    { }
    //[Table("SeriesGDP2011")]
    //internal partial class SeriesGDP2011 : Series
    //{ }
    [Table("SeriesPopulationUNFilled")]
    internal partial class SeriesPopulationUNFilled : Series
    { }
    //[Table("SeriesPopulation")]
    //internal partial class SeriesPopulation : Series
    //{ }
    [Table("SeriesMalnChil%WeightSevere")]
    internal partial class SeriesMalnChilWeightSevere : Series
    { }
    [Table("SeriesSevereWasting")]
    internal partial class SeriesSevereWasting : Series
    { }
    //jrs 2026/05/18 [Table("SeriesPopulationUnder5Mill")]
    [Table("SeriesPopUnder5UNPD")]
    internal partial class SeriesPopulationUnder5Mill : Series
    { }
    [Table("SeriesMalnChilSAMAdmission%")]
    internal partial class SeriesMalnChilSAMAdmissionPct : Series
    { }
    [Table("SeriesMalnChilSAMAdmission")]
    internal partial class SeriesMalnChilSAMAdmission : Series
    { }
    [Table("SeriesMalnChilSAMCured%Admission")]
    internal partial class SeriesMalnChilSAMCuredAdmission : Series
    { }
    [Table("SeriesHealthSmokingMales%")]
    internal partial class SeriesHealthSmokingMales : Series
    { }
    [Table("SeriesHealthSmokingTobaccoMalesBlend%")]
    internal partial class SeriesHealthSmokingTobaccoMalesBlend : Series
    { }
    [Table("SeriesHealthSmokingPrevalenceWHOMales%")]
    internal partial class SeriesHealthSmokingPrevalenceWHOMales : Series
    { }
    [Table("SeriesHealthSmokingFemales%")]
    internal partial class SeriesHealthSmokingFemales : Series
    { }
    [Table("SeriesHealthSmokingTobaccoFemalesBlend%")]
    internal partial class SeriesHealthSmokingTobaccoFemalesBlend : Series
    { }
    [Table("SeriesHealthSmokingPrevalenceWHOFemales%")]
    internal partial class SeriesHealthSmokingPrevalenceWHOFemales : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearF0to4")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearF0to4 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearF5to14")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearF5to14 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearF15to29")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearF15to29 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearF30to44")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearF30to44 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearF45to59")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearF45to59 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearF60to69")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearF60to69 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearF70to79")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearF70to79 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearF80+")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearF80 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearM0to4")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearM0to4 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearM5to14")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearM5to14 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearM15to29")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearM15to29 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearM30to44")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearM30to44 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearM45to59")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearM45to59 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearM60to69")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearM60to69 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearM70to79")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearM70to79 : Series
    { }
    [Table("SeriesHealthDiarrhoeaDthsPerYearM80+")]
    internal partial class SeriesHealthDiarrhoeaDthsPerYearM80 : Series
    { }
    [Table("SeriesHealthMalarDths")]
    internal partial class SeriesHealthMalarDths : Series
    { }
    [Table("SeriesHealthMalarDthsper100000")]
    internal partial class SeriesHealthMalarDthsper100000 : Series
    { }
    [Table("SeriesMortChildUnder5")]
    internal partial class SeriesMortChildUnder5 : Series
    { }

    [Table("SeriesIHMEInciMalariaNumber")]
    internal partial class SeriesIHMEInciMalariaNumber : Series
    { }
    [Table("SeriesIHMEPrevMalariaNumber")]
    internal partial class SeriesIHMEPrevMalariaNumber : Series
    { }
    [Table("SeriesIHMEDthsMalar")]
    internal partial class SeriesIHMEDthsMalar : Series
    { }
    [Table("SeriesIHMEInciHIVNumber")]
    internal partial class SeriesIHMEInciHIVNumber : Series
    { }
    [Table("SeriesIHMEPrevHIVNumber")]
    internal partial class SeriesIHMEPrevHIVNumber : Series
    { }
    [Table("SeriesIHMEDthsHIV")]
    internal partial class SeriesIHMEDthsHIV : Series
    { }
    [Table("SeriesHealthMalarBedNetsWDI")]
    internal partial class SeriesHealthMalarBedNetsWDI : Series
    { }
    [Table("SeriesHealthUNAIDSARTCoverMidEst")]
    internal partial class SeriesHealthUNAIDSARTCoverMidEst : Series
    { }
    [Table("SeriesHealthHIVMenCondUse")]
    internal partial class SeriesHealthHIVMenCondUse : Series
    { }

    [Table("SeriesUnder5MortRateIHME")]
    internal partial class SeriesUnder5MortRateIHME : Series
    { }

    [Table("SeriesChildStuntingPercentWDI")]
    internal partial class SeriesChildStuntingPercentWDI : Series
    { }    

    internal partial class IFsHistSeriesContext
    {
        protected virtual DbSet<SeriesHealthSmokingFemalesSI> SeriesHealthSmokingFemalesSI { get; set; }
        protected virtual DbSet<SeriesHealthSmokingMalesSI> SeriesHealthSmokingMalesSI { get; set; }
        protected virtual DbSet<SeriesHealthSmokingTCS> SeriesHealthSmokingTCS { get; set; }
        protected virtual DbSet<SeriesHealthObesityFemales> SeriesHealthObesityFemales { get; set; }
        protected virtual DbSet<SeriesHealthObesityFemales30Est> SeriesHealthObesityFemales30Est { get; set; }
        protected virtual DbSet<SeriesHealthObesityMales> SeriesHealthObesityMales { get; set; }
        protected virtual DbSet<SeriesHealthObesityMales30Est> SeriesHealthObesityMales30Est { get; set; }
        protected virtual DbSet<SeriesHealthBMIFemales30EstMean> SeriesHealthBMIFemales30EstMean { get; set; }
        protected virtual DbSet<SeriesHealthBMIMales30EstMean> SeriesHealthBMIMales30EstMean { get; set; }
        protected virtual DbSet<SeriesHealthIGTPrev> SeriesHealthIGTPrev { get; set; }
        protected virtual DbSet<SeriesHealthDiabetesPrev> SeriesHealthDiabetesPrev { get; set; }
        protected virtual DbSet<SeriesHealthSmokingImpactMales30to44> SeriesHealthSmokingImpactMales30to44 { get; set; }
        protected virtual DbSet<SeriesHealthSmokingImpactMales45to59> SeriesHealthSmokingImpactMales45to59 { get; set; }
        protected virtual DbSet<SeriesHealthSmokingImpactMales60to69> SeriesHealthSmokingImpactMales60to69 { get; set; }
        protected virtual DbSet<SeriesHealthSmokingImpactMales70to100> SeriesHealthSmokingImpactMales70to100 { get; set; }
        protected virtual DbSet<SeriesHealthSmokingImpactFemales30to44> SeriesHealthSmokingImpactFemales30to44 { get; set; }
        protected virtual DbSet<SeriesHealthSmokingImpactFemales45to59> SeriesHealthSmokingImpactFemales45to59 { get; set; }
        protected virtual DbSet<SeriesHealthSmokingImpactFemales60to69> SeriesHealthSmokingImpactFemales60to69 { get; set; }
        protected virtual DbSet<SeriesHealthSmokingImpactFemales70to100> SeriesHealthSmokingImpactFemales70to100 { get; set; }
        protected virtual DbSet<SeriesHealthDiabetesRRMales15to29> SeriesHealthDiabetesRRMales15to29 { get; set; }
        protected virtual DbSet<SeriesHealthDiabetesRRMales30to44> SeriesHealthDiabetesRRMales30to44 { get; set; }
        protected virtual DbSet<SeriesHealthDiabetesRRMales45to59> SeriesHealthDiabetesRRMales45to59 { get; set; }
        protected virtual DbSet<SeriesHealthDiabetesRRMales60to69> SeriesHealthDiabetesRRMales60to69 { get; set; }
        protected virtual DbSet<SeriesHealthDiabetesRRMales70to79> SeriesHealthDiabetesRRMales70to79 { get; set; }
        protected virtual DbSet<SeriesHealthDiabetesRRMales80to100> SeriesHealthDiabetesRRMales80to100 { get; set; }
        protected virtual DbSet<SeriesHealthDiabetesRRFemales15to29> SeriesHealthDiabetesRRFemales15to29 { get; set; }
        protected virtual DbSet<SeriesHealthDiabetesRRFemales30to44> SeriesHealthDiabetesRRFemales30to44 { get; set; }
        protected virtual DbSet<SeriesHealthDiabetesRRFemales45to59> SeriesHealthDiabetesRRFemales45to59 { get; set; }
        protected virtual DbSet<SeriesHealthDiabetesRRFemales60to69> SeriesHealthDiabetesRRFemales60to69 { get; set; }
        protected virtual DbSet<SeriesHealthDiabetesRRFemales70to79> SeriesHealthDiabetesRRFemales70to79 { get; set; }
        protected virtual DbSet<SeriesHealthDiabetesRRFemales80to100> SeriesHealthDiabetesRRFemales80to100 { get; set; }
        //protected virtual DbSet<SeriesGDP2005PCPPP> SeriesGDP2005PCPPP { get; set; }
        protected virtual DbSet<SeriesGDP2011PCPPP> SeriesGDP2011PCPPP { get; set; }
        protected virtual DbSet<SeriesGDP2017PCPPP> SeriesGDP2017PCPPP { get; set; }
        protected virtual DbSet<SeriesGDP2005> SeriesGDP2005 { get; set; }
        protected virtual DbSet<SeriesGDP2011> SeriesGDP2011 { get; set; }
        protected virtual DbSet<SeriesGDP2017> SeriesGDP2017 { get; set; }
        protected virtual DbSet<SeriesPopulationUNFilled> SeriesPopulationUNFilled { get; set; }
        protected virtual DbSet<SeriesPopulation> SeriesPopulation { get; set; }
        protected virtual DbSet<SeriesMalnChilWeightSevere> SeriesMalnChilWeightSevere { get; set; }
        protected virtual DbSet<SeriesSevereWasting> SeriesSevereWasting { get; set; }
        protected virtual DbSet<SeriesPopulationUnder5Mill> SeriesPopulationUnder5Mill { get; set; }
        protected virtual DbSet<SeriesMalnChilSAMAdmissionPct> SeriesMalnChilSAMAdmissionPct { get; set; }
        protected virtual DbSet<SeriesMalnChilSAMAdmission> SeriesMalnChilSAMAdmission { get; set; }
        protected virtual DbSet<SeriesMalnChilSAMCuredAdmission> SeriesMalnChilSAMCuredAdmission { get; set; }
        protected virtual DbSet<SeriesHealthSmokingMales> SeriesHealthSmokingMales { get; set; }
        protected virtual DbSet<SeriesHealthSmokingTobaccoMalesBlend> SeriesHealthSmokingTobaccoMalesBlend { get; set; }
        protected virtual DbSet<SeriesHealthSmokingPrevalenceWHOMales> SeriesHealthSmokingPrevalenceWHOMales { get; set; }
        protected virtual DbSet<SeriesHealthSmokingFemales> SeriesHealthSmokingFemales { get; set; }
        protected virtual DbSet<SeriesHealthSmokingTobaccoFemalesBlend> SeriesHealthSmokingTobaccoFemalesBlend { get; set; }
        protected virtual DbSet<SeriesHealthSmokingPrevalenceWHOFemales> SeriesHealthSmokingPrevalenceWHOFemales { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearF0to4> SeriesHealthDiarrhoeaDthsPerYearF0to4 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearF5to14> SeriesHealthDiarrhoeaDthsPerYearF5to14 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearF15to29> SeriesHealthDiarrhoeaDthsPerYearF15to29 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearF30to44> SeriesHealthDiarrhoeaDthsPerYearF30to44 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearF45to59> SeriesHealthDiarrhoeaDthsPerYearF45to59 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearF60to69> SeriesHealthDiarrhoeaDthsPerYearF60to69 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearF70to79> SeriesHealthDiarrhoeaDthsPerYearF70to79 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearF80> SeriesHealthDiarrhoeaDthsPerYearF80 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearM0to4> SeriesHealthDiarrhoeaDthsPerYearM0to4 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearM5to14> SeriesHealthDiarrhoeaDthsPerYearM5to14 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearM15to29> SeriesHealthDiarrhoeaDthsPerYearM15to29 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearM30to44> SeriesHealthDiarrhoeaDthsPerYearM30to44 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearM45to59> SeriesHealthDiarrhoeaDthsPerYearM45to59 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearM60to69> SeriesHealthDiarrhoeaDthsPerYearM60to69 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearM70to79> SeriesHealthDiarrhoeaDthsPerYearM70to79 { get; set; }
        protected virtual DbSet<SeriesHealthDiarrhoeaDthsPerYearM80> SeriesHealthDiarrhoeaDthsPerYearM80 { get; set; }
        protected virtual DbSet<SeriesHealthMalarDths> SeriesHealthMalarDths { get; set; }
        protected virtual DbSet<SeriesHealthMalarDthsper100000> SeriesHealthMalarDthsper100000 { get; set; }
        protected virtual DbSet<SeriesMortChildUnder5> SeriesMortChildUnder5 { get; set; }

        protected virtual DbSet<SeriesIHMEInciMalariaNumber> SeriesIHMEInciMalariaNumber { get; set; }        
        protected virtual DbSet<SeriesIHMEPrevMalariaNumber> SeriesIHMEPrevMalariaNumber { get; set; }
        protected virtual DbSet<SeriesIHMEDthsMalar> SeriesIHMEDthsMalar { get; set; }
        protected virtual DbSet<SeriesIHMEInciHIVNumber> SeriesIHMEInciHIVNumber { get; set; }
        protected virtual DbSet<SeriesIHMEPrevHIVNumber> SeriesIHMEPrevHIVNumber { get; set; }
        protected virtual DbSet<SeriesIHMEDthsHIV> SeriesIHMEDthsHIV { get; set; }
        protected virtual DbSet<SeriesHealthMalarBedNetsWDI> SeriesHealthMalarBedNetsWDI { get; set; }
        protected virtual DbSet<SeriesHealthUNAIDSARTCoverMidEst> SeriesHealthUNAIDSARTCoverMidEst { get; set; }
        protected virtual DbSet<SeriesHealthHIVMenCondUse> SeriesHealthHIVMenCondUse { get; set; }

        protected virtual DbSet<SeriesUnder5MortRateIHME> SeriesUnder5MortRateIHME { get; set; }
        protected virtual DbSet<SeriesChildStuntingPercentWDI> SeriesChildStuntingPercentWDI { get; set; }        

    }
}
