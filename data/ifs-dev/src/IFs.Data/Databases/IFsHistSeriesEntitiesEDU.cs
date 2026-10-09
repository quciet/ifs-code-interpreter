using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{
    [Table("SeriesEdPriDuration")]
    internal partial class SeriesEdPriDuration : Series
    { }

    [Table("SeriesEdPriEntranceAge")]
    internal partial class SeriesEdPriEntranceAge : Series
    { }

    [Table("SeriesEdPriPTR")]
    internal partial class SeriesEdPriPTR : Series
    { }

    [Table("SeriesEdPriEnrollGrossMalePcnt")]
    internal partial class SeriesEdPriEnrollGrossMalePcnt : Series
    { }

    [Table("SeriesEdPriEnrollNetMalePcnt")]
    internal partial class SeriesEdPriEnrollNetMalePcnt : Series
    { }

    [Table("SeriesEdPriNIRMale%")]
    internal partial class SeriesEdPriNIRMale : Series
    { }

    [Table("SeriesEdPriAIRMale%")]
    internal partial class SeriesEdPriAIRMale : Series
    { }

    [Table("SeriesEdPriSurvivalMale%")]
    internal partial class SeriesEdPriSurvivalMale : Series
    { }

    //primary completion
    [Table("SeriesEdPriCompletionMale%")]
    internal partial class SeriesEdPriCompletionMale : Series
    { }

    //transition from primary to secondary; general only; new secondary 1st graders (general) as % of pri last grade enrollment
    [Table("SeriesEdPriTransition2Sec%Male")]
    internal partial class SeriesEdPriTransition2SecMale : Series
    { }

    [Table("SeriesEdPriAdultGrads15Male%")]
    internal partial class SeriesEdPriAdultGrads15Male : Series
    { }

    [Table("SeriesEdPriAdultGrads15to24Male%")]
    internal partial class SeriesEdPriAdultGrads15to24Male : Series
    { }

    //female tables
    [Table("SeriesEdPriEnrollGrossFemalePcnt")]
    internal partial class SeriesEdPriEnrollGrossFemalePcnt : Series
    { }

    [Table("SeriesEdPriEnrollNetFemalePcnt")]
    internal partial class SeriesEdPriEnrollNetFemalePcnt : Series
    { }

    [Table("SeriesEdPriNIRFemale%")]
    internal partial class SeriesEdPriNIRFemale : Series
    { }

    [Table("SeriesEdPriAIRFemale%")]
    internal partial class SeriesEdPriAIRFemale : Series
    { }

    [Table("SeriesEdPriSurvivalFemale%")]
    internal partial class SeriesEdPriSurvivalFemale : Series
    { }

    //primary completion
    [Table("SeriesEdPriCompletionFemale%")]
    internal partial class SeriesEdPriCompletionFemale : Series
    { }

    //primary transition to secondary
    [Table("SeriesEdPriTransition2Sec%Female")]
    internal partial class SeriesEdPriTransition2SecFemale : Series
    { }

    [Table("SeriesEdPriAdultGrads15Female%")]
    internal partial class SeriesEdPriAdultGrads15Female : Series
    { }

    [Table("SeriesEdPriAdultGrads15to24Female%")]
    internal partial class SeriesEdPriAdultGrads15to24Female : Series
    { }

    [Table("SeriesEdPriEnrollGrossTotalPcnt")]
    internal partial class SeriesEdPriEnrollGrossTotalPcnt : Series
    { }

    [Table("SeriesEdPriEnrollNetTotalPcnt")]
    internal partial class SeriesEdPriEnrollNetTotalPcnt : Series
    { }

    [Table("SeriesEdPriNIRTotal%")]
    internal partial class SeriesEdPriNIRTotal : Series
    { }

    [Table("SeriesEdPriAIRTotal%")]
    internal partial class SeriesEdPriAIRTotal : Series
    { }

    [Table("SeriesEdPriSurvivalTotal%")]
    internal partial class SeriesEdPriSurvivalTotal : Series
    { }

    [Table("SeriesEdPriCompletionTotal%")]
    internal partial class SeriesEdPriCompletionTotal : Series
    { }

    [Table("SeriesEdPriAdultGrads15Total%")]
    internal partial class SeriesEdPriAdultGrads15Total : Series
    { }

    [Table("SeriesEDPRINIRENTRNM1Total")]
    internal partial class SeriesEDPRINIRENTRNM1Total : Series
    { }
    
    //Lower secondary
    [Table("SeriesEdPriTransition2Sec%Total")]
    internal partial class SeriesEdPriTransition2SecTotal : Series
    { }

    [Table("SeriesEdPriAdultGrads15to24Total%")]
    internal partial class SeriesEdPriAdultGrads15to24Total : Series
    { }

    [Table("SeriesEdSecLowerDuration")]
    internal partial class SeriesEdSecLowerDuration : Series
    { }

    //Upper Secondary
    [Table("SeriesEdSecUpperDuration")]
    internal partial class SeriesEdSecUpperDuration : Series
    { }

    //Secondary Lower Male
    [Table("SeriesEdSecLowerVoc%AllMale")]
    internal partial class SeriesEdSecLowerVocAllMale : Series
    { }

    [Table("SeriesEdSecLowerEnrollGross%Male")]
    internal partial class SeriesEdSecLowerEnrollGrossMale : Series
    { }

    //Transition from lower to upper secondary; general only; new upper secondary 1st graders (general) as % of lower secondary last grade enrollment
    [Table("SeriesEdSecLower2UpperMale%")]
    internal partial class SeriesEdSecLower2UpperMale : Series
    { }

    [Table("SeriesEdSecLowerGradRateAllMal")]
    internal partial class SeriesEdSecLowerGradRateAllMal : Series
    { }

    //Secondary Upper Male
    [Table("SeriesEdSecUpperVoc%AllMale")]
    internal partial class SeriesEdSecUpperVocAllMale : Series
    { }

    [Table("SeriesEdSecUpperGradRateAllMal")]
    internal partial class SeriesEdSecUpperGradRateAllMal : Series
    { }

    [Table("SeriesEdSecUpperEnrollGross%Male")]
    internal partial class SeriesEdSecUpperEnrollGrossMale : Series
    { }

    //Secondary male
    [Table("SeriesEdSecAdultGrads15Male%")]
    internal partial class SeriesEdSecAdultGrads15Male : Series
    { }

    [Table("SeriesEdSecAdultGrads15to24Male%")]
    internal partial class SeriesEdSecAdultGrads15to24Male : Series
    { }

    [Table("SeriesEdSecEnrollNetMale")]
    internal partial class SeriesEdSecEnrollNetMale : Series
    { }

    [Table("SeriesEdSecEnrollNetMaleOlder")]
    internal partial class SeriesEdSecEnrollNetMaleOlder : Series
    { }

    [Table("SeriesEdSecEnrollGross%Male")]
    internal partial class SeriesEdSecEnrollGrossMale : Series
    { }

    //Secondary Female
    //Secondary Lower Female
    [Table("SeriesEdSecLowerVoc%AllFemale")]
    internal partial class SeriesEdSecLowerVocAllFemale : Series
    { }

    [Table("SeriesEdSecLowerEnrollGross%Female")]
    internal partial class SeriesEdSecLowerEnrollGrossFemale : Series
    { }

    [Table("SeriesEdSecLower2UpperFemale%")]
    internal partial class SeriesEdSecLower2UpperFemale : Series
    { }

    [Table("SeriesEdSecLowerGradRateAllFem")]
    internal partial class SeriesEdSecLowerGradRateAllFem : Series
    { }

    //Secondary Upper Female
    [Table("SeriesEdSecUpperVoc%AllFemale")]
    internal partial class SeriesEdSecUpperVocAllFemale : Series
    { }

    [Table("SeriesEdSecUpperGradRateAllFem")]
    internal partial class SeriesEdSecUpperGradRateAllFem : Series
    { }

    [Table("SeriesEdSecUpperEnrollGross%Female")]
    internal partial class SeriesEdSecUpperEnrollGrossFemale : Series
    { }

    //secondary female
    [Table("SeriesEdSecAdultGrads15Female%")]
    internal partial class SeriesEdSecAdultGrads15Female : Series
    { }

    [Table("SeriesEdSecAdultGrads15to24Female%")]
    internal partial class SeriesEdSecAdultGrads15to24Female : Series
    { }

    [Table("SeriesEdSecEnrollNetFemale")]
    internal partial class SeriesEdSecEnrollNetFemale : Series
    { }

    [Table("SeriesEdSecEnrollNetFemaleOlder")]
    internal partial class SeriesEdSecEnrollNetFemaleOlder : Series
    { }

    [Table("SeriesEdSecEnrollGross%Female")]
    internal partial class SeriesEdSecEnrollGrossFemale : Series
    { }

    //secondary Total
    //Secondary Lower Total
    [Table("SeriesEdSecLowerVoc%AllTotal")]
    internal partial class SeriesEdSecLowerVocAllTotal : Series
    { }

    [Table("SeriesEdSecLowerEnrollGross%Total")]
    internal partial class SeriesEdSecLowerEnrollGrossTotal : Series
    { }

    //Transition from lower to upper secondary
    [Table("SeriesEdSecLower2UpperTotal%")]
    internal partial class SeriesEdSecLower2UpperTotal : Series
    { }

    [Table("SeriesEdSecLowerGradRateAllTot")]
    internal partial class SeriesEdSecLowerGradRateAllTot : Series
    { }

    //Secondary Upper Total
    [Table("SeriesEdSecUpperVoc%AllTotal")]
    internal partial class SeriesEdSecUpperVocAllTotal : Series
    { }

    [Table("SeriesEdSecUpperGradRateAllTot")]
    internal partial class SeriesEdSecUpperGradRateAllTot : Series
    { }

    [Table("SeriesEdSecUpperEnrollGross%Total")]
    internal partial class SeriesEdSecUpperEnrollGrossTotal : Series
    { }

    [Table("SeriesEdSecAdultGrads15Total%")]
    internal partial class SeriesEdSecAdultGrads15Total : Series
    { }

    [Table("SeriesEdSecAdultGrads15to24Total%")]
    internal partial class SeriesEdSecAdultGrads15to24Total : Series
    { }

    [Table("SeriesEdSecEnrollNet")]
    internal partial class SeriesEdSecEnrollNet : Series
    { }

    [Table("SeriesEdSecEnrollNetOlder")]
    internal partial class SeriesEdSecEnrollNetOlder : Series
    { }

    [Table("SeriesEdSecEnrollGross%Total")]
    internal partial class SeriesEdSecEnrollGrossTotal : Series
    { }

    //Tertiary education data tables
    //Male
    [Table("SeriesEdTerIntakeGrossMale%")]
    internal partial class SeriesEdTerIntakeGrossMale : Series
    { }

    [Table("SeriesEdTerEnrollGross%Male")]
    internal partial class SeriesEdTerEnrollGrossMale : Series
    { }

    [Table("SeriesEdTerGradRate1stDegreeMale%")]
    internal partial class SeriesEdTerGradRate1stDegreeMale : Series
    { }

    [Table("SeriesEdTerAdultGrads15Male%")]
    internal partial class SeriesEdTerAdultGrads15Male : Series
    { }

    //Female
    [Table("SeriesEdTerIntakeGrossFemale%")]
    internal partial class SeriesEdTerIntakeGrossFemale : Series
    { }

    [Table("SeriesEdTerEnrollGross%Female")]
    internal partial class SeriesEdTerEnrollGrossFemale : Series
    { }

    [Table("SeriesEdTerGradRate1stDegreeFemale%")]
    internal partial class SeriesEdTerGradRate1stDegreeFemale : Series
    { }

    [Table("SeriesEdTerAdultGrads15Female%")]
    internal partial class SeriesEdTerAdultGrads15Female : Series
    { }

    //tertiary total
    [Table("SeriesEdTerIntakeGrossTotal%")]
    internal partial class SeriesEdTerIntakeGrossTotal : Series
    { }

    [Table("SeriesEdTerEnrollGross%Total")]
    internal partial class SeriesEdTerEnrollGrossTotal : Series
    { }

    [Table("SeriesEdTerGradRate1stDegreeTotal%")]
    internal partial class SeriesEdTerGradRate1stDegreeTotal : Series
    { }

    [Table("SeriesEdTerGrads%SciEngg")]
    internal partial class SeriesEdTerGradsSciEngg : Series
    { }

    [Table("SeriesEdTerAdultGrads15Total%")]
    internal partial class SeriesEdTerAdultGrads15Total : Series
    { }

    //expenditure
    [Table("SeriesEdExpPri%GDPPC")]
    internal partial class SeriesEdExpPriGDPPC : Series
    { }

    [Table("SeriesEdExpSec%GDPPC")]
    internal partial class SeriesEdExpSecGDPPC : Series
    { }

    [Table("SeriesEdExpSecLowr%GDPPC")]
    internal partial class SeriesEdExpSecLowrGDPPC : Series
    { }

    [Table("SeriesEdExpSecUppr%GDPPC")]
    internal partial class SeriesEdExpSecUpprGDPPC : Series
    { }

    [Table("SeriesEdExpTer%GDPPC")]
    internal partial class SeriesEdExpTerGDPPC : Series
    { }

    //Years of Education - 15 to 25
    //Male
    [Table("SeriesEdYearsAge20-29Male")]
    internal partial class SeriesEdYearsAge2029Male : Series
    { }
    //Female
    [Table("SeriesEdYearsAge20-29Female")]
    internal partial class SeriesEdYearsAge2029Female : Series
    { }
    //Total
    [Table("SeriesEdYearsAge20-29Total")]
    internal partial class SeriesEdYearsAge2029Total : Series
    { }

    //Secondary completed 20 to 24
    //Male
    [Table("SeriesEdSecAdultGrads20to24Male%")]
    internal partial class SeriesEdSecAdultGrads20to24Male : Series
    { }
    //Female
    [Table("SeriesEdSecAdultGrads20to24Female%")]
    internal partial class SeriesEdSecAdultGrads20to24Female : Series
    { }
    //total
    [Table("SeriesEdSecAdultGrads20to24Total%")]
    internal partial class SeriesEdSecAdultGrads20to24Total : Series
    { }

    //Years of Education - 15 to 25
    //male
    [Table("SeriesEdYearsAge15-24Male")]
    internal partial class SeriesEdYearsAge1524Male : Series
    { }
    //female
    [Table("SeriesEdYearsAge15-24Female")]
    internal partial class SeriesEdYearsAge1524Female : Series
    { }
    //total
    [Table("SeriesEdYearsAge15-24Total")]
    internal partial class SeriesEdYearsAge1524Total : Series
    { }

    //Years of Education - 15+
    //Male
    [Table("SeriesEdYearsAge15Male")]
    internal partial class SeriesEdYearsAge15Male : Series
    { }
    //Female
    [Table("SeriesEdYearsAge15Female")]
    internal partial class SeriesEdYearsAge15Female : Series
    { }
    //Total
    [Table("SeriesEdYearsAge15Total")]
    internal partial class SeriesEdYearsAge15Total : Series
    { }

    //backup series for educational attainment, 15+
    [Table("SeriesForecastMYSMaleAbove15YRsIIASASSP1")]
    internal partial class SeriesForecastMYSMaleAbove15YRsIIASASSP1 : Series
    { }

    [Table("SeriesForecastMYSFemaleAbove15YRsIIASASSP1")]
    internal partial class SeriesForecastMYSFemaleAbove15YRsIIASASSP1 : Series
    { }

    [Table("SeriesForecastMYSBothSexesAbove15YRsIIASASSP1")]
    internal partial class SeriesForecastMYSBothSexesAbove15YRsIIASASSP1 : Series
    { }

    //Years of Education - 25+
    //male
    [Table("SeriesEdYearsAge25Male")]
    internal partial class SeriesEdYearsAge25Male : Series
    { }
    //female
    [Table("SeriesEdYearsAge25Female")]
    internal partial class SeriesEdYearsAge25Female : Series
    { }
    //total
    [Table("SeriesEdYearsAge25")]
    internal partial class SeriesEdYearsAge25 : Series
    { }

    //backup series for educational attainment, 25+
    //male
    [Table("SeriesForecastMYSMaleAbove25YRsIIASASSP1")]
    internal partial class SeriesForecastMYSMaleAbove25YRsIIASASSP1 : Series
    { }
    //female
    [Table("SeriesForecastMYSFemaleAbove25YRsIIASASSP1")]
    internal partial class SeriesForecastMYSFemaleAbove25YRsIIASASSP1 : Series
    { }
    //total
    [Table("SeriesForecastMYSBothSexesAbove25YRsIIASASSP1")]
    internal partial class SeriesForecastMYSBothSexesAbove25YRsIIASASSP1 : Series
    { }

    //Quality 
    //Pri Sci both sexes (no gendered table now)
    [Table("SeriesEdQualPriSciAvgScrWBGA")]
    internal partial class SeriesEdQualPriSciAvgScrWBGA : Series
    { }

    //Pri Read both sexes (no gendered table now)
    [Table("SeriesEdQualPriReadAvgScrWBGA")]
    internal partial class SeriesEdQualPriReadAvgScrWBGA : Series
    { }
    //Pri Math both sexes (no gendered table now)
    [Table("SeriesEdQualPriMathAvgScrWBGA")]
    internal partial class SeriesEdQualPriMathAvgScrWBGA : Series
    { }
    //Pri All both sexes (no gendered table now)
    [Table("SeriesEdQualPriAllAvgScrWBGA")]
    internal partial class SeriesEdQualPriAllAvgScrWBGA : Series
    { }
    //Sec Sci both sexes (no gendered table now)
    [Table("SeriesEdQualSecSciAvgScrWBGA")]
    internal partial class SeriesEdQualSecSciAvgScrWBGA : Series
    { }
    //Sec Read both sexes (no gendered table now)
    [Table("SeriesEdQualSecReadAvgScrWBGA")]
    internal partial class SeriesEdQualSecReadAvgScrWBGA : Series
    { }
    //Sec Math both sexes (no gendered table now)
    [Table("SeriesEdQualSecMathAvgScrWBGA")]
    internal partial class SeriesEdQualSecMathAvgScrWBGA : Series
    { }
    //Sec All both sexes (no gendered table now)
    [Table("SeriesEdQualSecAllAvgScrWBGA")]
    internal partial class SeriesEdQualSecAllAvgScrWBGA : Series
    { }
    //Pri Math Gender Ratio Table
    [Table("SeriesEdQualPriMathRatioNAEK")]
    internal partial class SeriesEdQualPriMathRatioNAEK : Series
    { }
    //Pri Sci Gender Ratio Table
    [Table("SeriesEdQualPriSciRatioNAEK")]
    internal partial class SeriesEdQualPriSciRatioNAEK : Series
    { }
    //Pri Read Gender Ratio Table
    [Table("SeriesEdQualPriReadRatioNAEK")]
    internal partial class SeriesEdQualPriReadRatioNAEK : Series
    { }
    //Pri All Subject Gender Ratio Table
    [Table("SeriesEdQualPriTotalRatioNAEK")]
    internal partial class SeriesEdQualPriTotalRatioNAEK : Series
    { }
    //--- Secondary Gender Ratio Tables ---
    //Sec Math Gender Ratio Table
    [Table("SeriesEdQualSecMathRatioNAEK")]
    internal partial class SeriesEdQualSecMathRatioNAEK : Series
    { }
    //Sec Sci Gender Ratio Table
    [Table("SeriesEdQualSecSciRatioNAEK")]
    internal partial class SeriesEdQualSecSciRatioNAEK : Series
    { }
    //Sec Read Gender Ratio Table
    [Table("SeriesEdQualSecReadRatioNAEK")]
    internal partial class SeriesEdQualSecReadRatioNAEK : Series
    { }
    //Sec All Subject Gender Ratio Table
    [Table("SeriesEdQualSecTotalRatioNAEK")]
    internal partial class SeriesEdQualSecTotalRatioNAEK : Series
    { }

    [Table("SeriesEdAttnShrMalePostSec15+IIASASSP2")]
    internal partial class SeriesEdAttnShrMalePostSec15IIASASSP2 : Series
    { }

    [Table("SeriesEdAttnShrMaleSecUppr15+IIASASSP2")]
    internal partial class SeriesEdAttnShrMaleSecUppr15IIASASSP2 : Series
    { }

    [Table("SeriesEdAttnShrMaleSecLowr15+IIASASSP2")]
    internal partial class SeriesEdAttnShrMaleSecLowr15IIASASSP2 : Series
    { }

    [Table("SeriesEdAttnShrMalePri15+IIASASSP2")]
    internal partial class SeriesEdAttnShrMalePri15IIASASSP2 : Series
    { }

    [Table("SeriesEdAttnShrFemalePostSec15+IIASASSP2")]
    internal partial class SeriesEdAttnShrFemalePostSec15IIASASSP2 : Series
    { }

    [Table("SeriesEdAttnShrFemaleSecUppr15+IIASASSP2")]
    internal partial class SeriesEdAttnShrFemaleSecUppr15IIASASSP2 : Series
    { }

    [Table("SeriesEdAttnShrFemaleSecLowr15+IIASASSP2")]
    internal partial class SeriesEdAttnShrFemaleSecLowr15IIASASSP2 : Series
    { }

    [Table("SeriesEdAttnShrFemalePri15+IIASASSP2")]
    internal partial class SeriesEdAttnShrFemalePri15IIASASSP2 : Series
    { }

    [Table("SeriesEdAttnShrBothSexesPostSec15+IIASASSP2")]
    internal partial class SeriesEdAttnShrBothSexesPostSec15IIASASSP2 : Series
    { }

    [Table("SeriesEdAttnShrBothSexesSecUppr15+IIASASSP2")]
    internal partial class SeriesEdAttnShrBothSexesSecUppr15IIASASSP2 : Series
    { }

    [Table("SeriesEdAttnShrBothSexesSecLowr15+IIASASSP2")]
    internal partial class SeriesEdAttnShrBothSexesSecLowr15IIASASSP2 : Series
    { }

    [Table("SeriesEdAttnShrBothSexesPri15+IIASASSP2")]
    internal partial class SeriesEdAttnShrBothSexesPri15IIASASSP2 : Series
    { }

    [Table("SeriesEDPRINIRENTRNP1Total")]
    internal partial class SeriesEDPRINIRENTRNP1Total : Series
    { }

    [Table("SeriesEDPRINIRENTRNMale")]
    internal partial class SeriesEDPRINIRENTRNMale : Series
    { }

    [Table("SeriesEDPRINIRENTRNM1Male")]
    internal partial class SeriesEDPRINIRENTRNM1Male : Series
    { }

    //mti 2024/05/07 start
    [Table("SeriesEDPRINIRENTRNP1Male")]
    internal partial class SeriesEDPRINIRENTRNP1Male : Series
    { }
    //mti 2024/05/07 end

    [Table("SeriesEDPRINIRENTRNFemale")]
    internal partial class SeriesEDPRINIRENTRNFemale : Series
    { }

    [Table("SeriesEDPRINIRENTRNM1Female")]
    internal partial class SeriesEDPRINIRENTRNM1Female : Series
    { }

    [Table("SeriesEDPRINIRENTRNP1Female")]
    internal partial class SeriesEDPRINIRENTRNP1Female : Series
    { }

    //mti 2022/08/16
    [Table("SeriesEdPriEnrGr1MaleNo")]
    internal partial class SeriesEdPriEnrGr1MaleNo : Series
    { }
    [Table("SeriesEdPriEnrGr2MaleNo")]
    internal partial class SeriesEdPriEnrGr2MaleNo : Series
    { }
    [Table("SeriesEdPriEnrGr3MaleNo")]
    internal partial class SeriesEdPriEnrGr3MaleNo : Series
    { }
    [Table("SeriesEdPriEnrGr4MaleNo")]
    internal partial class SeriesEdPriEnrGr4MaleNo : Series
    { }
    [Table("SeriesEdPriEnrGr5MaleNo")]
    internal partial class SeriesEdPriEnrGr5MaleNo : Series
    { }
    [Table("SeriesEdPriEnrGr6MaleNo")]
    internal partial class SeriesEdPriEnrGr6MaleNo : Series
    { }
    [Table("SeriesEdPriEnrGr7MaleNo")]
    internal partial class SeriesEdPriEnrGr7MaleNo : Series
    { }
    [Table("SeriesEdPriEnrGr1FemaleNo")]
    internal partial class SeriesEdPriEnrGr1FemaleNo : Series
    { }
    [Table("SeriesEdPriEnrGr2FemaleNo")]
    internal partial class SeriesEdPriEnrGr2FemaleNo : Series
    { }
    [Table("SeriesEdPriEnrGr3FemaleNo")]
    internal partial class SeriesEdPriEnrGr3FemaleNo : Series
    { }
    [Table("SeriesEdPriEnrGr4FemaleNo")]
    internal partial class SeriesEdPriEnrGr4FemaleNo : Series
    { }
    [Table("SeriesEdPriEnrGr5FemaleNo")]
    internal partial class SeriesEdPriEnrGr5FemaleNo : Series
    { }
    [Table("SeriesEdPriEnrGr6FemaleNo")]
    internal partial class SeriesEdPriEnrGr6FemaleNo : Series
    { }
    [Table("SeriesEdPriEnrGr7FemaleNo")]
    internal partial class SeriesEdPriEnrGr7FemaleNo : Series
    { }
    [Table("SeriesEdPriDrpGr1Male")]
    internal partial class SeriesEdPriDrpGr1Male : Series
    { }
    [Table("SeriesEdPriDrpGr2Male")]
    internal partial class SeriesEdPriDrpGr2Male : Series
    { }
    [Table("SeriesEdPriDrpGr3Male")]
    internal partial class SeriesEdPriDrpGr3Male : Series
    { }
    [Table("SeriesEdPriDrpGr4Male")]
    internal partial class SeriesEdPriDrpGr4Male : Series
    { }
    [Table("SeriesEdPriDrpGr5Male")]
    internal partial class SeriesEdPriDrpGr5Male : Series
    { }
    [Table("SeriesEdPriDrpGr6Male")]
    internal partial class SeriesEdPriDrpGr6Male : Series
    { }
    [Table("SeriesEdPriDrpGr1Female")]
    internal partial class SeriesEdPriDrpGr1Female : Series
    { }
    [Table("SeriesEdPriDrpGr2Female")]
    internal partial class SeriesEdPriDrpGr2Female : Series
    { }
    [Table("SeriesEdPriDrpGr3Female")]
    internal partial class SeriesEdPriDrpGr3Female : Series
    { }
    [Table("SeriesEdPriDrpGr4Female")]
    internal partial class SeriesEdPriDrpGr4Female : Series
    { }
    [Table("SeriesEdPriDrpGr5Female")]
    internal partial class SeriesEdPriDrpGr5Female : Series
    { }
    [Table("SeriesEdPriDrpGr6Female")]
    internal partial class SeriesEdPriDrpGr6Female : Series
    { }
    [Table("SeriesEdPriDrpGr1BothSexes")]
    internal partial class SeriesEdPriDrpGr1BothSexes : Series
    { }
    [Table("SeriesEdPriDrpGr2BothSexes")]
    internal partial class SeriesEdPriDrpGr2BothSexes : Series
    { }
    [Table("SeriesEdPriDrpGr3BothSexes")]
    internal partial class SeriesEdPriDrpGr3BothSexes : Series
    { }
    [Table("SeriesEdPriDrpGr4BothSexes")]
    internal partial class SeriesEdPriDrpGr4BothSexes : Series
    { }
    [Table("SeriesEdPriDrpGr5BothSexes")]
    internal partial class SeriesEdPriDrpGr5BothSexes : Series
    { }
    [Table("SeriesEdPriDrpGr6BothSexes")]
    internal partial class SeriesEdPriDrpGr6BothSexes : Series
    { }
    [Table("SeriesEdSecLowrEnrGr1MaleNo")]
    internal partial class SeriesEdSecLowrEnrGr1MaleNo : Series
    { }
    [Table("SeriesEdSecLowrEnrGr2MaleNo")]
    internal partial class SeriesEdSecLowrEnrGr2MaleNo : Series
    { }
    [Table("SeriesEdSecLowrEnrGr3MaleNo")]
    internal partial class SeriesEdSecLowrEnrGr3MaleNo : Series
    { }
    [Table("SeriesEdSecLowrEnrGr4MaleNo")]
    internal partial class SeriesEdSecLowrEnrGr4MaleNo : Series
    { }
    [Table("SeriesEdSecLowrEnrGr5MaleNo")]
    internal partial class SeriesEdSecLowrEnrGr5MaleNo : Series
    { }
    [Table("SeriesEdSecLowrEnrGr6MaleNo")]
    internal partial class SeriesEdSecLowrEnrGr6MaleNo : Series
    { }
    [Table("SeriesEdSecLowrEnrGr1FemaleNo")]
    internal partial class SeriesEdSecLowrEnrGr1FemaleNo : Series
    { }
    [Table("SeriesEdSecLowrEnrGr2FemaleNo")]
    internal partial class SeriesEdSecLowrEnrGr2FemaleNo : Series
    { }
    [Table("SeriesEdSecLowrEnrGr3FemaleNo")]
    internal partial class SeriesEdSecLowrEnrGr3FemaleNo : Series
    { }
    [Table("SeriesEdSecLowrEnrGr4FemaleNo")]
    internal partial class SeriesEdSecLowrEnrGr4FemaleNo : Series
    { }
    [Table("SeriesEdSecLowrEnrGr5FemaleNo")]
    internal partial class SeriesEdSecLowrEnrGr5FemaleNo : Series
    { }
    [Table("SeriesEdSecLowrEnrGr6FemaleNo")]
    internal partial class SeriesEdSecLowrEnrGr6FemaleNo : Series
    { }
    [Table("SeriesEdSecLowrDrpGr1Male")]
    internal partial class SeriesEdSecLowrDrpGr1Male : Series
    { }
    [Table("SeriesEdSecLowrDrpGr2Male")]
    internal partial class SeriesEdSecLowrDrpGr2Male : Series
    { }
    [Table("SeriesEdSecLowrDrpGr3Male")]
    internal partial class SeriesEdSecLowrDrpGr3Male : Series
    { }
    [Table("SeriesEdSecLowrDrpGr4Male")]
    internal partial class SeriesEdSecLowrDrpGr4Male : Series
    { }
    [Table("SeriesEdSecLowrDrpGr5Male")]
    internal partial class SeriesEdSecLowrDrpGr5Male : Series
    { }
    [Table("SeriesEdSecLowrDrpGr1Female")]
    internal partial class SeriesEdSecLowrDrpGr1Female : Series
    { }
    [Table("SeriesEdSecLowrDrpGr2Female")]
    internal partial class SeriesEdSecLowrDrpGr2Female : Series
    { }
    [Table("SeriesEdSecLowrDrpGr3Female")]
    internal partial class SeriesEdSecLowrDrpGr3Female : Series
    { }
    [Table("SeriesEdSecLowrDrpGr4Female")]
    internal partial class SeriesEdSecLowrDrpGr4Female : Series
    { }
    [Table("SeriesEdSecLowrDrpGr5Female")]
    internal partial class SeriesEdSecLowrDrpGr5Female : Series
    { }
    [Table("SeriesEdSecLowrDrpGr1BothSexes")]
    internal partial class SeriesEdSecLowrDrpGr1BothSexes : Series
    { }
    [Table("SeriesEdSecLowrDrpGr2BothSexes")]
    internal partial class SeriesEdSecLowrDrpGr2BothSexes : Series
    { }
    [Table("SeriesEdSecLowrDrpGr3BothSexes")]
    internal partial class SeriesEdSecLowrDrpGr3BothSexes : Series
    { }
    [Table("SeriesEdSecLowrDrpGr4BothSexes")]
    internal partial class SeriesEdSecLowrDrpGr4BothSexes : Series
    { }
    [Table("SeriesEdSecLowrDrpGr5BothSexes")]
    internal partial class SeriesEdSecLowrDrpGr5BothSexes : Series
    { }
    //jrs 2023/09/01
    [Table("SeriesForecastMYSBothSexesAbove15YRsIIASASSP1Rev23")]
    internal partial class SeriesForecastMYSBothSexesAbove15YRsIIASASSP1Rev23 : Series
    { }
    [Table("SeriesForecastMYSBothSexesAbove15YRsIIASASSP2Rev23")]
    internal partial class SeriesForecastMYSBothSexesAbove15YRsIIASASSP2Rev23 : Series
    { }
    [Table("SeriesForecastMYSBothSexesAbove15YRsIIASASSP3Rev23")]
    internal partial class SeriesForecastMYSBothSexesAbove15YRsIIASASSP3Rev23 : Series
    { }
    [Table("SeriesForecastMYSBothSexesAbove15YRsIIASASSP4Rev23")]
    internal partial class SeriesForecastMYSBothSexesAbove15YRsIIASASSP4Rev23 : Series
    { }
    [Table("SeriesForecastMYSBothSexesAbove15YRsIIASASSP5Rev23")]
    internal partial class SeriesForecastMYSBothSexesAbove15YRsIIASASSP5Rev23 : Series
    { }
    [Table("SeriesForecastMYSBothSexesAbove25YRsIIASASSP1Rev23")]
    internal partial class SeriesForecastMYSBothSexesAbove25YRsIIASASSP1Rev23 : Series
    { }
    [Table("SeriesForecastMYSBothSexesAbove25YRsIIASASSP2Rev23")]
    internal partial class SeriesForecastMYSBothSexesAbove25YRsIIASASSP2Rev23 : Series
    { }
    [Table("SeriesForecastMYSBothSexesAbove25YRsIIASASSP3Rev23")]
    internal partial class SeriesForecastMYSBothSexesAbove25YRsIIASASSP3Rev23 : Series
    { }
    [Table("SeriesForecastMYSBothSexesAbove25YRsIIASASSP4Rev23")]
    internal partial class SeriesForecastMYSBothSexesAbove25YRsIIASASSP4Rev23 : Series
    { }
    [Table("SeriesForecastMYSBothSexesAbove25YRsIIASASSP5Rev23")]
    internal partial class SeriesForecastMYSBothSexesAbove25YRsIIASASSP5Rev23 : Series
    { }
    [Table("SeriesForecastMYSFemaleAbove15YRsIIASASSP1Rev23")]
    internal partial class SeriesForecastMYSFemaleAbove15YRsIIASASSP1Rev23 : Series
    { }
    [Table("SeriesForecastMYSFemaleAbove15YRsIIASASSP2Rev23")]
    internal partial class SeriesForecastMYSFemaleAbove15YRsIIASASSP2Rev23 : Series
    { }
    [Table("SeriesForecastMYSFemaleAbove15YRsIIASASSP3Rev23")]
    internal partial class SeriesForecastMYSFemaleAbove15YRsIIASASSP3Rev23 : Series
    { }
    [Table("SeriesForecastMYSFemaleAbove15YRsIIASASSP4Rev23")]
    internal partial class SeriesForecastMYSFemaleAbove15YRsIIASASSP4Rev23 : Series
    { }
    [Table("SeriesForecastMYSFemaleAbove15YRsIIASASSP5Rev23")]
    internal partial class SeriesForecastMYSFemaleAbove15YRsIIASASSP5Rev23 : Series
    { }
    [Table("SeriesForecastMYSFemaleAbove25YRsIIASASSP1Rev23")]
    internal partial class SeriesForecastMYSFemaleAbove25YRsIIASASSP1Rev23 : Series
    { }
    [Table("SeriesForecastMYSFemaleAbove25YRsIIASASSP2Rev23")]
    internal partial class SeriesForecastMYSFemaleAbove25YRsIIASASSP2Rev23 : Series
    { }
    [Table("SeriesForecastMYSFemaleAbove25YRsIIASASSP3Rev23")]
    internal partial class SeriesForecastMYSFemaleAbove25YRsIIASASSP3Rev23 : Series
    { }
    [Table("SeriesForecastMYSFemaleAbove25YRsIIASASSP4Rev23")]
    internal partial class SeriesForecastMYSFemaleAbove25YRsIIASASSP4Rev23 : Series
    { }
    [Table("SeriesForecastMYSFemaleAbove25YRsIIASASSP5Rev23")]
    internal partial class SeriesForecastMYSFemaleAbove25YRsIIASASSP5Rev23 : Series
    { }
    [Table("SeriesForecastMYSMaleAbove15YRsIIASASSP1Rev23")]
    internal partial class SeriesForecastMYSMaleAbove15YRsIIASASSP1Rev23 : Series
    { }
    [Table("SeriesForecastMYSMaleAbove15YRsIIASASSP2Rev23")]
    internal partial class SeriesForecastMYSMaleAbove15YRsIIASASSP2Rev23 : Series
    { }
    [Table("SeriesForecastMYSMaleAbove15YRsIIASASSP3Rev23")]
    internal partial class SeriesForecastMYSMaleAbove15YRsIIASASSP3Rev23 : Series
    { }
    [Table("SeriesForecastMYSMaleAbove15YRsIIASASSP4Rev23")]
    internal partial class SeriesForecastMYSMaleAbove15YRsIIASASSP4Rev23 : Series
    { }
    [Table("SeriesForecastMYSMaleAbove15YRsIIASASSP5Rev23")]
    internal partial class SeriesForecastMYSMaleAbove15YRsIIASASSP5Rev23 : Series
    { }
    [Table("SeriesForecastMYSMaleAbove25YRsIIASASSP1Rev23")]
    internal partial class SeriesForecastMYSMaleAbove25YRsIIASASSP1Rev23 : Series
    { }
    [Table("SeriesForecastMYSMaleAbove25YRsIIASASSP2Rev23")]
    internal partial class SeriesForecastMYSMaleAbove25YRsIIASASSP2Rev23 : Series
    { }
    [Table("SeriesForecastMYSMaleAbove25YRsIIASASSP3Rev23")]
    internal partial class SeriesForecastMYSMaleAbove25YRsIIASASSP3Rev23 : Series
    { }
    [Table("SeriesForecastMYSMaleAbove25YRsIIASASSP4Rev23")]
    internal partial class SeriesForecastMYSMaleAbove25YRsIIASASSP4Rev23 : Series
    { }
    [Table("SeriesForecastMYSMaleAbove25YRsIIASASSP5Rev23")]
    internal partial class SeriesForecastMYSMaleAbove25YRsIIASASSP5Rev23 : Series
    { }
    //jrs 2024/02/19
    [Table("SeriesExoEDYRSAG25TOTAL")]
    internal partial class SeriesExoEDYRSAG25TOTAL : Series
    { }
    [Table("SeriesExoEDYRSAG25FEMALE")]
    internal partial class SeriesExoEDYRSAG25FEMALE : Series
    { }
    [Table("SeriesExoEDYRSAG25MALE")]
    internal partial class SeriesExoEDYRSAG25MALE : Series
    { }
    [Table("SeriesExoEDYRSAG15TOTAL")]
    internal partial class SeriesExoEDYRSAG15TOTAL : Series
    { }
    [Table("SeriesExoEDYRSAG15FEMALE")]
    internal partial class SeriesExoEDYRSAG15FEMALE : Series
    { }
    [Table("SeriesExoEDYRSAG15MALE")]
    internal partial class SeriesExoEDYRSAG15MALE : Series
    { }

    internal partial class IFsHistSeriesContext
    {
        //jrs 2024/02/19
        protected virtual DbSet<SeriesExoEDYRSAG25TOTAL> SeriesExoEDYRSAG25TOTAL { get; set; }
        protected virtual DbSet<SeriesExoEDYRSAG25FEMALE> SeriesExoEDYRSAG25FEMALE { get; set; }
        protected virtual DbSet<SeriesExoEDYRSAG25MALE> SeriesExoEDYRSAG25MALE { get; set; }
        protected virtual DbSet<SeriesExoEDYRSAG15TOTAL> SeriesExoEDYRSAG15TOTAL { get; set; }
        protected virtual DbSet<SeriesExoEDYRSAG15FEMALE> SeriesExoEDYRSAG15FEMALE { get; set; }
        protected virtual DbSet<SeriesExoEDYRSAG15MALE> SeriesExoEDYRSAG15MALE { get; set; }

        protected virtual DbSet<SeriesForecastMYSMaleAbove25YRsIIASASSP1Rev23> SeriesForecastMYSMaleAbove25YRsIIASASSP1Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove25YRsIIASASSP2Rev23> SeriesForecastMYSMaleAbove25YRsIIASASSP2Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove25YRsIIASASSP3Rev23> SeriesForecastMYSMaleAbove25YRsIIASASSP3Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove25YRsIIASASSP4Rev23> SeriesForecastMYSMaleAbove25YRsIIASASSP4Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove25YRsIIASASSP5Rev23> SeriesForecastMYSMaleAbove25YRsIIASASSP5Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove15YRsIIASASSP1Rev23> SeriesForecastMYSMaleAbove15YRsIIASASSP1Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove15YRsIIASASSP2Rev23> SeriesForecastMYSMaleAbove15YRsIIASASSP2Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove15YRsIIASASSP3Rev23> SeriesForecastMYSMaleAbove15YRsIIASASSP3Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove15YRsIIASASSP4Rev23> SeriesForecastMYSMaleAbove15YRsIIASASSP4Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove15YRsIIASASSP5Rev23> SeriesForecastMYSMaleAbove15YRsIIASASSP5Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove25YRsIIASASSP1Rev23> SeriesForecastMYSFemaleAbove25YRsIIASASSP1Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove25YRsIIASASSP2Rev23> SeriesForecastMYSFemaleAbove25YRsIIASASSP2Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove25YRsIIASASSP3Rev23> SeriesForecastMYSFemaleAbove25YRsIIASASSP3Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove25YRsIIASASSP4Rev23> SeriesForecastMYSFemaleAbove25YRsIIASASSP4Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove25YRsIIASASSP5Rev23> SeriesForecastMYSFemaleAbove25YRsIIASASSP5Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove15YRsIIASASSP1Rev23> SeriesForecastMYSFemaleAbove15YRsIIASASSP1Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove15YRsIIASASSP2Rev23> SeriesForecastMYSFemaleAbove15YRsIIASASSP2Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove15YRsIIASASSP3Rev23> SeriesForecastMYSFemaleAbove15YRsIIASASSP3Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove15YRsIIASASSP4Rev23> SeriesForecastMYSFemaleAbove15YRsIIASASSP4Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove15YRsIIASASSP5Rev23> SeriesForecastMYSFemaleAbove15YRsIIASASSP5Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove25YRsIIASASSP1Rev23> SeriesForecastMYSBothSexesAbove25YRsIIASASSP1Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove25YRsIIASASSP2Rev23> SeriesForecastMYSBothSexesAbove25YRsIIASASSP2Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove25YRsIIASASSP3Rev23> SeriesForecastMYSBothSexesAbove25YRsIIASASSP3Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove25YRsIIASASSP4Rev23> SeriesForecastMYSBothSexesAbove25YRsIIASASSP4Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove25YRsIIASASSP5Rev23> SeriesForecastMYSBothSexesAbove25YRsIIASASSP5Rev23 { get; set; }

        protected virtual DbSet<SeriesForecastMYSBothSexesAbove15YRsIIASASSP1Rev23> SeriesForecastMYSBothSexesAbove15YRsIIASASSP1Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove15YRsIIASASSP2Rev23> SeriesForecastMYSBothSexesAbove15YRsIIASASSP2Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove15YRsIIASASSP3Rev23> SeriesForecastMYSBothSexesAbove15YRsIIASASSP3Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove15YRsIIASASSP4Rev23> SeriesForecastMYSBothSexesAbove15YRsIIASASSP4Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove15YRsIIASASSP5Rev23> SeriesForecastMYSBothSexesAbove15YRsIIASASSP5Rev23 { get; set; }

        protected virtual DbSet<SeriesEdPriDrpGr1Male> SeriesEdPriDrpGr1Male { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr2Male> SeriesEdPriDrpGr2Male { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr3Male> SeriesEdPriDrpGr3Male { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr4Male> SeriesEdPriDrpGr4Male { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr5Male> SeriesEdPriDrpGr5Male { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr6Male> SeriesEdPriDrpGr6Male { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr1Female> SeriesEdPriDrpGr1Female { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr2Female> SeriesEdPriDrpGr2Female { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr3Female> SeriesEdPriDrpGr3Female { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr4Female> SeriesEdPriDrpGr4Female { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr5Female> SeriesEdPriDrpGr5Female { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr6Female> SeriesEdPriDrpGr6Female { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr1BothSexes> SeriesEdPriDrpGr1BothSexes { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr2BothSexes> SeriesEdPriDrpGr2BothSexes { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr3BothSexes> SeriesEdPriDrpGr3BothSexes { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr4BothSexes> SeriesEdPriDrpGr4BothSexes { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr5BothSexes> SeriesEdPriDrpGr5BothSexes { get; set; }
        protected virtual DbSet<SeriesEdPriDrpGr6BothSexes> SeriesEdPriDrpGr6BothSexes { get; set; }
        protected virtual DbSet<SeriesEdPriEnrGr1MaleNo> SeriesEdPriEnrGr1MaleNo { get; set; }
        protected virtual DbSet<SeriesEdPriEnrGr2MaleNo> SeriesEdPriEnrGr2MaleNo { get; set; }
        protected virtual DbSet<SeriesEdPriEnrGr3MaleNo> SeriesEdPriEnrGr3MaleNo { get; set; }
        protected virtual DbSet<SeriesEdPriEnrGr4MaleNo> SeriesEdPriEnrGr4MaleNo { get; set; }
        protected virtual DbSet<SeriesEdPriEnrGr5MaleNo> SeriesEdPriEnrGr5MaleNo { get; set; }
        protected virtual DbSet<SeriesEdPriEnrGr6MaleNo> SeriesEdPriEnrGr6MaleNo { get; set; }
        protected virtual DbSet<SeriesEdPriEnrGr7MaleNo> SeriesEdPriEnrGr7MaleNo { get; set; }
        protected virtual DbSet<SeriesEdPriEnrGr1FemaleNo> SeriesEdPriEnrGr1FemaleNo { get; set; }
        protected virtual DbSet<SeriesEdPriEnrGr2FemaleNo> SeriesEdPriEnrGr2FemaleNo { get; set; }
        protected virtual DbSet<SeriesEdPriEnrGr3FemaleNo> SeriesEdPriEnrGr3FemaleNo { get; set; }
        protected virtual DbSet<SeriesEdPriEnrGr4FemaleNo> SeriesEdPriEnrGr4FemaleNo { get; set; }
        protected virtual DbSet<SeriesEdPriEnrGr5FemaleNo> SeriesEdPriEnrGr5FemaleNo { get; set; }
        protected virtual DbSet<SeriesEdPriEnrGr6FemaleNo> SeriesEdPriEnrGr6FemaleNo { get; set; }
        protected virtual DbSet<SeriesEdPriEnrGr7FemaleNo> SeriesEdPriEnrGr7FemaleNo { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr1Male> SeriesEdSecLowrDrpGr1Male { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr2Male> SeriesEdSecLowrDrpGr2Male { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr3Male> SeriesEdSecLowrDrpGr3Male { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr4Male> SeriesEdSecLowrDrpGr4Male { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr5Male> SeriesEdSecLowrDrpGr5Male { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr1Female> SeriesEdSecLowrDrpGr1Female { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr2Female> SeriesEdSecLowrDrpGr2Female { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr3Female> SeriesEdSecLowrDrpGr3Female { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr4Female> SeriesEdSecLowrDrpGr4Female { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr5Female> SeriesEdSecLowrDrpGr5Female { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr1BothSexes> SeriesEdSecLowrDrpGr1BothSexes { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr2BothSexes> SeriesEdSecLowrDrpGr2BothSexes { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr3BothSexes> SeriesEdSecLowrDrpGr3BothSexes { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr4BothSexes> SeriesEdSecLowrDrpGr4BothSexes { get; set; }
        protected virtual DbSet<SeriesEdSecLowrDrpGr5BothSexes> SeriesEdSecLowrDrpGr5BothSexes { get; set; }
        protected virtual DbSet<SeriesEdSecLowrEnrGr1MaleNo> SeriesEdSecLowrEnrGr1MaleNo { get; set; }
        protected virtual DbSet<SeriesEdSecLowrEnrGr2MaleNo> SeriesEdSecLowrEnrGr2MaleNo { get; set; }
        protected virtual DbSet<SeriesEdSecLowrEnrGr3MaleNo> SeriesEdSecLowrEnrGr3MaleNo { get; set; }
        protected virtual DbSet<SeriesEdSecLowrEnrGr4MaleNo> SeriesEdSecLowrEnrGr4MaleNo { get; set; }
        protected virtual DbSet<SeriesEdSecLowrEnrGr5MaleNo> SeriesEdSecLowrEnrGr5MaleNo { get; set; }
        protected virtual DbSet<SeriesEdSecLowrEnrGr6MaleNo> SeriesEdSecLowrEnrGr6MaleNo { get; set; }
        protected virtual DbSet<SeriesEdSecLowrEnrGr1FemaleNo> SeriesEdSecLowrEnrGr1FemaleNo { get; set; }
        protected virtual DbSet<SeriesEdSecLowrEnrGr2FemaleNo> SeriesEdSecLowrEnrGr2FemaleNo { get; set; }
        protected virtual DbSet<SeriesEdSecLowrEnrGr3FemaleNo> SeriesEdSecLowrEnrGr3FemaleNo { get; set; }
        protected virtual DbSet<SeriesEdSecLowrEnrGr4FemaleNo> SeriesEdSecLowrEnrGr4FemaleNo { get; set; }
        protected virtual DbSet<SeriesEdSecLowrEnrGr5FemaleNo> SeriesEdSecLowrEnrGr5FemaleNo { get; set; }
        protected virtual DbSet<SeriesEdSecLowrEnrGr6FemaleNo> SeriesEdSecLowrEnrGr6FemaleNo { get; set; }
        protected virtual DbSet<SeriesEDPRINIRENTRNP1Female> SeriesEDPRINIRENTRNP1Female { get; set; }
        protected virtual DbSet<SeriesEDPRINIRENTRNM1Female> SeriesEDPRINIRENTRNM1Female { get; set; }
        protected virtual DbSet<SeriesEDPRINIRENTRNFemale> SeriesEDPRINIRENTRNFemale { get; set; }
        protected virtual DbSet<SeriesEDPRINIRENTRNM1Male> SeriesEDPRINIRENTRNM1Male { get; set; }
        protected virtual DbSet<SeriesEDPRINIRENTRNP1Male> SeriesEDPRINIRENTRNP1Male { get; set; } //mti 2024/05/07
        protected virtual DbSet<SeriesEDPRINIRENTRNMale> SeriesEDPRINIRENTRNMale { get; set; }
        protected virtual DbSet<SeriesEDPRINIRENTRNP1Total> SeriesEDPRINIRENTRNP1Total { get; set; }
        protected virtual DbSet<SeriesEDPRINIRENTRNM1Total> SeriesEDPRINIRENTRNM1Total { get; set; }
        protected virtual DbSet<SeriesEdAttnShrBothSexesPri15IIASASSP2> SeriesEdAttnShrBothSexesPri15IIASASSP2 { get; set; }
        protected virtual DbSet<SeriesEdAttnShrBothSexesSecLowr15IIASASSP2> SeriesEdAttnShrBothSexesSecLowr15IIASASSP2 { get; set; }
        protected virtual DbSet<SeriesEdAttnShrBothSexesSecUppr15IIASASSP2> SeriesEdAttnShrBothSexesSecUppr15IIASASSP2 { get; set; }
        protected virtual DbSet<SeriesEdAttnShrBothSexesPostSec15IIASASSP2> SeriesEdAttnShrBothSexesPostSec15IIASASSP2 { get; set; }
        protected virtual DbSet<SeriesEdAttnShrFemalePri15IIASASSP2> SeriesEdAttnShrFemalePri15IIASASSP2 { get; set; }
        protected virtual DbSet<SeriesEdAttnShrFemaleSecLowr15IIASASSP2> SeriesEdAttnShrFemaleSecLowr15IIASASSP2 { get; set; }
        protected virtual DbSet<SeriesEdAttnShrFemaleSecUppr15IIASASSP2> SeriesEdAttnShrFemaleSecUppr15IIASASSP2 { get; set; }
        protected virtual DbSet<SeriesEdAttnShrFemalePostSec15IIASASSP2> SeriesEdAttnShrFemalePostSec15IIASASSP2 { get; set; }
        protected virtual DbSet<SeriesEdAttnShrMalePri15IIASASSP2> SeriesEdAttnShrMalePri15IIASASSP2 { get; set; }
        protected virtual DbSet<SeriesEdAttnShrMaleSecLowr15IIASASSP2> SeriesEdAttnShrMaleSecLowr15IIASASSP2 { get; set; }
        protected virtual DbSet<SeriesEdAttnShrMaleSecUppr15IIASASSP2> SeriesEdAttnShrMaleSecUppr15IIASASSP2 { get; set; }
        protected virtual DbSet<SeriesEdPriDuration> SeriesEdPriDuration { get; set; }
        protected virtual DbSet<SeriesEdPriEntranceAge> SeriesEdPriEntranceAge { get; set; }
        protected virtual DbSet<SeriesEdPriPTR> SeriesEdPriPTR { get; set; }
        protected virtual DbSet<SeriesEdPriEnrollGrossMalePcnt> SeriesEdPriEnrollGrossMalePcnt { get; set; }
        protected virtual DbSet<SeriesEdPriEnrollNetMalePcnt> SeriesEdPriEnrollNetMalePcnt { get; set; }
        protected virtual DbSet<SeriesEdPriNIRMale> SeriesEdPriNIRMale { get; set; }
        protected virtual DbSet<SeriesEdPriAIRMale> SeriesEdPriAIRMale { get; set; }
        protected virtual DbSet<SeriesEdPriSurvivalMale> SeriesEdPriSurvivalMale { get; set; }
        protected virtual DbSet<SeriesEdPriCompletionMale> SeriesEdPriCompletionMale { get; set; }
        protected virtual DbSet<SeriesEdPriTransition2SecMale> SeriesEdPriTransition2SecMale { get; set; }
        protected virtual DbSet<SeriesEdPriAdultGrads15Male> SeriesEdPriAdultGrads15Male { get; set; }
        protected virtual DbSet<SeriesEdPriAdultGrads15to24Male> SeriesEdPriAdultGrads15to24Male { get; set; }
        protected virtual DbSet<SeriesEdPriEnrollGrossFemalePcnt> SeriesEdPriEnrollGrossFemalePcnt { get; set; }
        protected virtual DbSet<SeriesEdPriEnrollNetFemalePcnt> SeriesEdPriEnrollNetFemalePcnt { get; set; }
        protected virtual DbSet<SeriesEdPriNIRFemale> SeriesEdPriNIRFemale { get; set; }
        protected virtual DbSet<SeriesEdPriAIRFemale> SeriesEdPriAIRFemale { get; set; }
        protected virtual DbSet<SeriesEdPriSurvivalFemale> SeriesEdPriSurvivalFemale { get; set; }
        protected virtual DbSet<SeriesEdPriCompletionFemale> SeriesEdPriCompletionFemale { get; set; }
        protected virtual DbSet<SeriesEdPriTransition2SecFemale> SeriesEdPriTransition2SecFemale { get; set; }
        protected virtual DbSet<SeriesEdPriAdultGrads15Female> SeriesEdPriAdultGrads15Female { get; set; }
        protected virtual DbSet<SeriesEdPriAdultGrads15to24Female> SeriesEdPriAdultGrads15to24Female { get; set; }
        protected virtual DbSet<SeriesEdPriEnrollGrossTotalPcnt> SeriesEdPriEnrollGrossTotalPcnt { get; set; }
        protected virtual DbSet<SeriesEdPriEnrollNetTotalPcnt> SeriesEdPriEnrollNetTotalPcnt { get; set; }
        protected virtual DbSet<SeriesEdPriNIRTotal> SeriesEdPriNIRTotal { get; set; }
        protected virtual DbSet<SeriesEdPriAIRTotal> SeriesEdPriAIRTotal { get; set; }
        protected virtual DbSet<SeriesEdPriSurvivalTotal> SeriesEdPriSurvivalTotal { get; set; }
        protected virtual DbSet<SeriesEdPriCompletionTotal> SeriesEdPriCompletionTotal { get; set; }
        protected virtual DbSet<SeriesEdPriAdultGrads15Total> SeriesEdPriAdultGrads15Total { get; set; }
        protected virtual DbSet<SeriesEdPriTransition2SecTotal> SeriesEdPriTransition2SecTotal { get; set; }
        protected virtual DbSet<SeriesEdPriAdultGrads15to24Total> SeriesEdPriAdultGrads15to24Total { get; set; }
        protected virtual DbSet<SeriesEdSecLowerDuration> SeriesEdSecLowerDuration { get; set; }
        protected virtual DbSet<SeriesEdSecUpperDuration> SeriesEdSecUpperDuration { get; set; }
        protected virtual DbSet<SeriesEdSecLowerVocAllMale> SeriesEdSecLowerVocAllMale { get; set; }
        protected virtual DbSet<SeriesEdSecLowerEnrollGrossMale> SeriesEdSecLowerEnrollGrossMale { get; set; }
        protected virtual DbSet<SeriesEdSecLower2UpperMale> SeriesEdSecLower2UpperMale { get; set; }
        protected virtual DbSet<SeriesEdSecLowerGradRateAllMal> SeriesEdSecLowerGradRateAllMal { get; set; }
        protected virtual DbSet<SeriesEdSecUpperVocAllMale> SeriesEdSecUpperVocAllMale { get; set; }
        protected virtual DbSet<SeriesEdSecUpperGradRateAllMal> SeriesEdSecUpperGradRateAllMal { get; set; }
        protected virtual DbSet<SeriesEdSecUpperEnrollGrossMale> SeriesEdSecUpperEnrollGrossMale { get; set; }
        protected virtual DbSet<SeriesEdSecAdultGrads15Male> SeriesEdSecAdultGrads15Male { get; set; }
        protected virtual DbSet<SeriesEdSecAdultGrads15to24Male> SeriesEdSecAdultGrads15to24Male { get; set; }
        protected virtual DbSet<SeriesEdSecEnrollNetMale> SeriesEdSecEnrollNetMale { get; set; }
        protected virtual DbSet<SeriesEdSecEnrollNetMaleOlder> SeriesEdSecEnrollNetMaleOlder { get; set; }
        protected virtual DbSet<SeriesEdSecEnrollGrossMale> SeriesEdSecEnrollGrossMale { get; set; }
        protected virtual DbSet<SeriesEdSecLowerVocAllFemale> SeriesEdSecLowerVocAllFemale { get; set; }
        protected virtual DbSet<SeriesEdSecLowerEnrollGrossFemale> SeriesEdSecLowerEnrollGrossFemale { get; set; }
        protected virtual DbSet<SeriesEdSecLower2UpperFemale> SeriesEdSecLower2UpperFemale { get; set; }
        protected virtual DbSet<SeriesEdSecLowerGradRateAllFem> SeriesEdSecLowerGradRateAllFem { get; set; }
        protected virtual DbSet<SeriesEdSecUpperVocAllFemale> SeriesEdSecUpperVocAllFemale { get; set; }
        protected virtual DbSet<SeriesEdSecUpperGradRateAllFem> SeriesEdSecUpperGradRateAllFem { get; set; }
        protected virtual DbSet<SeriesEdSecUpperEnrollGrossFemale> SeriesEdSecUpperEnrollGrossFemale { get; set; }
        protected virtual DbSet<SeriesEdSecAdultGrads15Female> SeriesEdSecAdultGrads15Female { get; set; }
        protected virtual DbSet<SeriesEdSecAdultGrads15to24Female> SeriesEdSecAdultGrads15to24Female { get; set; }
        protected virtual DbSet<SeriesEdSecEnrollNetFemale> SeriesEdSecEnrollNetFemale { get; set; }
        protected virtual DbSet<SeriesEdSecEnrollNetFemaleOlder> SeriesEdSecEnrollNetFemaleOlder { get; set; }
        protected virtual DbSet<SeriesEdSecEnrollGrossFemale> SeriesEdSecEnrollGrossFemale { get; set; }
        protected virtual DbSet<SeriesEdSecLowerVocAllTotal> SeriesEdSecLowerVocAllTotal { get; set; }
        protected virtual DbSet<SeriesEdSecLowerEnrollGrossTotal> SeriesEdSecLowerEnrollGrossTotal { get; set; }
        protected virtual DbSet<SeriesEdSecLower2UpperTotal> SeriesEdSecLower2UpperTotal { get; set; }
        protected virtual DbSet<SeriesEdSecLowerGradRateAllTot> SeriesEdSecLowerGradRateAllTot { get; set; }
        protected virtual DbSet<SeriesEdSecUpperVocAllTotal> SeriesEdSecUpperVocAllTotal { get; set; }
        protected virtual DbSet<SeriesEdSecUpperGradRateAllTot> SeriesEdSecUpperGradRateAllTot { get; set; }
        protected virtual DbSet<SeriesEdSecUpperEnrollGrossTotal> SeriesEdSecUpperEnrollGrossTotal { get; set; }
        protected virtual DbSet<SeriesEdSecAdultGrads15Total> SeriesEdSecAdultGrads15Total { get; set; }
        protected virtual DbSet<SeriesEdSecAdultGrads15to24Total> SeriesEdSecAdultGrads15to24Total { get; set; }
        protected virtual DbSet<SeriesEdSecEnrollNet> SeriesEdSecEnrollNet { get; set; }
        protected virtual DbSet<SeriesEdSecEnrollNetOlder> SeriesEdSecEnrollNetOlder { get; set; }
        protected virtual DbSet<SeriesEdSecEnrollGrossTotal> SeriesEdSecEnrollGrossTotal { get; set; }
        protected virtual DbSet<SeriesEdTerIntakeGrossMale> SeriesEdTerIntakeGrossMale { get; set; }
        protected virtual DbSet<SeriesEdTerEnrollGrossMale> SeriesEdTerEnrollGrossMale { get; set; }
        protected virtual DbSet<SeriesEdTerGradRate1stDegreeMale> SeriesEdTerGradRate1stDegreeMale { get; set; }
        protected virtual DbSet<SeriesEdTerAdultGrads15Male> SeriesEdTerAdultGrads15Male { get; set; }
        protected virtual DbSet<SeriesEdTerIntakeGrossFemale> SeriesEdTerIntakeGrossFemale { get; set; }
        protected virtual DbSet<SeriesEdTerEnrollGrossFemale> SeriesEdTerEnrollGrossFemale { get; set; }
        protected virtual DbSet<SeriesEdTerGradRate1stDegreeFemale> SeriesEdTerGradRate1stDegreeFemale { get; set; }
        protected virtual DbSet<SeriesEdTerAdultGrads15Female> SeriesEdTerAdultGrads15Female { get; set; }
        protected virtual DbSet<SeriesEdTerIntakeGrossTotal> SeriesEdTerIntakeGrossTotal { get; set; }
        protected virtual DbSet<SeriesEdTerEnrollGrossTotal> SeriesEdTerEnrollGrossTotal { get; set; }
        protected virtual DbSet<SeriesEdTerGradRate1stDegreeTotal> SeriesEdTerGradRate1stDegreeTotal { get; set; }
        protected virtual DbSet<SeriesEdTerGradsSciEngg> SeriesEdTerGradsSciEngg { get; set; }
        protected virtual DbSet<SeriesEdTerAdultGrads15Total> SeriesEdTerAdultGrads15Total { get; set; }
        protected virtual DbSet<SeriesEdExpPriGDPPC> SeriesEdExpPriGDPPC { get; set; }
        protected virtual DbSet<SeriesEdExpSecGDPPC> SeriesEdExpSecGDPPC { get; set; }
        protected virtual DbSet<SeriesEdExpSecLowrGDPPC> SeriesEdExpSecLowrGDPPC { get; set; }
        protected virtual DbSet<SeriesEdExpSecUpprGDPPC> SeriesEdExpSecUpprGDPPC { get; set; }
        protected virtual DbSet<SeriesEdExpTerGDPPC> SeriesEdExpTerGDPPC { get; set; }
        protected virtual DbSet<SeriesEdYearsAge2029Male> SeriesEdYearsAge2029Male { get; set; }
        protected virtual DbSet<SeriesEdYearsAge2029Female> SeriesEdYearsAge2029Female { get; set; }
        protected virtual DbSet<SeriesEdYearsAge2029Total> SeriesEdYearsAge2029Total { get; set; }
        protected virtual DbSet<SeriesEdSecAdultGrads20to24Male> SeriesEdSecAdultGrads20to24Male { get; set; }
        protected virtual DbSet<SeriesEdSecAdultGrads20to24Female> SeriesEdSecAdultGrads20to24Female { get; set; }
        protected virtual DbSet<SeriesEdSecAdultGrads20to24Total> SeriesEdSecAdultGrads20to24Total { get; set; }
        protected virtual DbSet<SeriesEdYearsAge1524Male> SeriesEdYearsAge1524Male { get; set; }
        protected virtual DbSet<SeriesEdYearsAge1524Female> SeriesEdYearsAge1524Female { get; set; }
        protected virtual DbSet<SeriesEdYearsAge1524Total> SeriesEdYearsAge1524Total { get; set; }
        protected virtual DbSet<SeriesEdYearsAge15Male> SeriesEdYearsAge15Male { get; set; }
        protected virtual DbSet<SeriesEdYearsAge15Female> SeriesEdYearsAge15Female { get; set; }
        protected virtual DbSet<SeriesEdYearsAge15Total> SeriesEdYearsAge15Total { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove15YRsIIASASSP1> SeriesForecastMYSMaleAbove15YRsIIASASSP1 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove15YRsIIASASSP1> SeriesForecastMYSFemaleAbove15YRsIIASASSP1 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove15YRsIIASASSP1> SeriesForecastMYSBothSexesAbove15YRsIIASASSP1 { get; set; }
        protected virtual DbSet<SeriesEdYearsAge25Male> SeriesEdYearsAge25Male { get; set; }
        protected virtual DbSet<SeriesEdYearsAge25Female> SeriesEdYearsAge25Female { get; set; }
        protected virtual DbSet<SeriesEdYearsAge25> SeriesEdYearsAge25 { get; set; }
        protected virtual DbSet<SeriesForecastMYSMaleAbove25YRsIIASASSP1> SeriesForecastMYSMaleAbove25YRsIIASASSP1 { get; set; }
        protected virtual DbSet<SeriesForecastMYSFemaleAbove25YRsIIASASSP1> SeriesForecastMYSFemaleAbove25YRsIIASASSP1 { get; set; }
        protected virtual DbSet<SeriesForecastMYSBothSexesAbove25YRsIIASASSP1> SeriesForecastMYSBothSexesAbove25YRsIIASASSP1 { get; set; }

        protected virtual DbSet<SeriesEdQualPriSciAvgScrWBGA> SeriesEdQualPriSciAvgScrWBGA { get; set; }
        protected virtual DbSet<SeriesEdQualPriReadAvgScrWBGA> SeriesEdQualPriReadAvgScrWBGA { get; set; }
        protected virtual DbSet<SeriesEdQualPriMathAvgScrWBGA> SeriesEdQualPriMathAvgScrWBGA { get; set; }
        protected virtual DbSet<SeriesEdQualPriAllAvgScrWBGA> SeriesEdQualPriAllAvgScrWBGA { get; set; }
        protected virtual DbSet<SeriesEdQualSecSciAvgScrWBGA> SeriesEdQualSecSciAvgScrWBGA { get; set; }
        protected virtual DbSet<SeriesEdQualSecReadAvgScrWBGA> SeriesEdQualSecReadAvgScrWBGA { get; set; }
        protected virtual DbSet<SeriesEdQualSecMathAvgScrWBGA> SeriesEdQualSecMathAvgScrWBGA { get; set; }
        protected virtual DbSet<SeriesEdQualSecAllAvgScrWBGA> SeriesEdQualSecAllAvgScrWBGA { get; set; }
        protected virtual DbSet<SeriesEdQualPriMathRatioNAEK> SeriesEdQualPriMathRatioNAEK { get; set; }
        protected virtual DbSet<SeriesEdQualPriSciRatioNAEK> SeriesEdQualPriSciRatioNAEK { get; set; }
        protected virtual DbSet<SeriesEdQualPriReadRatioNAEK> SeriesEdQualPriReadRatioNAEK { get; set; }
        protected virtual DbSet<SeriesEdQualPriTotalRatioNAEK> SeriesEdQualPriTotalRatioNAEK { get; set; }
        protected virtual DbSet<SeriesEdQualSecMathRatioNAEK> SeriesEdQualSecMathRatioNAEK { get; set; }
        protected virtual DbSet<SeriesEdQualSecSciRatioNAEK> SeriesEdQualSecSciRatioNAEK { get; set; }
        protected virtual DbSet<SeriesEdQualSecReadRatioNAEK> SeriesEdQualSecReadRatioNAEK { get; set; }
        protected virtual DbSet<SeriesEdQualSecTotalRatioNAEK> SeriesEdQualSecTotalRatioNAEK { get; set; }
        protected virtual DbSet<SeriesEdAttnShrMalePostSec15IIASASSP2> SeriesEdAttnShrMalePostSec15IIASASSP2 { get; set; }
        
    }
}
