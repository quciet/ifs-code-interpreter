using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{
    [Table("SeriesExportGoodSer%")]
    internal partial class SeriesExportGoodSer : Series
    { }

    [Table("SeriesImportGoodSer%")]
    internal partial class SeriesImportGoodSer : Series
    { }

    [Table("SeriesAIDRec%GNI")]
    internal partial class SeriesAIDRecGNI : Series
    { }

    [Table("SeriesXCurActBal%GDP")]
    internal partial class SeriesXCurActBalGDP : Series
    { }

    [Table("SeriesVaddAg%")]
    internal partial class SeriesVaddAg : Series
    { }

    [Table("SeriesVaddMan%")]
    internal partial class SeriesVaddMan : Series
    { }

    [Table("SeriesVaddInd%")]
    internal partial class SeriesVaddInd : Series
    { }

    [Table("SeriesVaddSer%")]
    internal partial class SeriesVaddSer : Series
    { }

    [Table("SeriesVaddICT%GDP")]
    internal partial class SeriesVaddICTGDP : Series
    { }

    [Table("SeriesIncBelow1D25c%WDI2011")]
    internal partial class SeriesIncBelow1D25cWDI2011 : Series
    { }

    [Table("SeriesXWorkerRemitPaid")]
    internal partial class SeriesXWorkerRemitPaid : Series
    { }

    [Table("SeriesXWorkerRemitPaid%GDP")]
    internal partial class SeriesXWorkerRemitPaidGDP : Series
    { }

    [Table("SeriesXWorkerRemitReceived")]
    internal partial class SeriesXWorkerRemitReceived : Series
    { }

    [Table("SeriesXWorkerRemitReceived%GDP")]
    internal partial class SeriesXWorkerRemitReceivedGDP : Series
    { }

    [Table("SeriesExportsMerchandise")]
    internal partial class SeriesExportsMerchandise : Series
    { }

    [Table("SeriesExportsMerchandise%GDP")]
    internal partial class SeriesExportsMerchandiseGDP : Series
    { }

    [Table("SeriesImportsMerchandise")]
    internal partial class SeriesImportsMerchandise : Series
    { }

    [Table("SeriesImportsMerchandise%GDP")]
    internal partial class SeriesImportsMerchandiseGDP : Series
    { }

    [Table("SeriesExportServices")]
    internal partial class SeriesExportServices : Series
    { }

    [Table("SeriesExportServices%GDP")]
    internal partial class SeriesExportServicesGDP : Series
    { }

    [Table("SeriesImportServices")]
    internal partial class SeriesImportServices : Series
    { }

    [Table("SeriesImportServices%GDP")]
    internal partial class SeriesImportServicesGDP : Series
    { }

    [Table("SeriesArmsImp%TotImp")]
    internal partial class SeriesArmsImpTotImp : Series
    { }

    [Table("SeriesICTGoodsExport%ExpGoods")]
    internal partial class SeriesICTGoodsExportExpGoods : Series
    { }

    [Table("SeriesICTGoodsImport%ImpGoods")]
    internal partial class SeriesICTGoodsImportImpGoods : Series
    { }

    [Table("SeriesICTSerExport%ExpSer")]
    internal partial class SeriesICTSerExportExpSer : Series
    { }

    [Table("SeriesICTSerImport%ImpSer")]
    internal partial class SeriesICTSerImportImpSer : Series
    { }

    [Table("SeriesOresMetsEx%MerchEx")]
    internal partial class SeriesOresMetsExMerchEx : Series
    { }

    [Table("SeriesOresMetsIm%MerchIm")]
    internal partial class SeriesOresMetsImMerchIm : Series
    { }

    [Table("SeriesHouseCon%GDP")]
    internal partial class SeriesHouseConGDP : Series
    { }

    [Table("SeriesGovCon%GDP")]
    internal partial class SeriesGovConGDP : Series
    { }

    [Table("SeriesInvestGrCapForm%GDP")]
    internal partial class SeriesInvestGrCapFormGDP : Series
    { }

    [Table("SeriesGDPInformal%Elgin")]
    internal partial class SeriesGDPInformalElgin : Series
    { }

    [Table("SeriesGDPInformal%Blended")]
    internal partial class SeriesGDPInformalBlended : Series
    { }

    [Table("SeriesLaborInformalInfSec%TotalInformal")]
    internal partial class SeriesLaborInformalInfSecTotalInformal : Series
    { }

    [Table("SeriesLaborInformalFemale%InformalSec")]
    internal partial class SeriesLaborInformalFemaleInformalSec : Series
    { }

    [Table("SeriesLaborInformalMale%InformalSec")]
    internal partial class SeriesLaborInformalMaleInformalSec : Series
    { }

    [Table("SeriesLaborInformalHH%TotalInformal")]
    internal partial class SeriesLaborInformalHHTotalInformal : Series
    { }

    [Table("SeriesLaborInformalFemale%HHInformal")]
    internal partial class SeriesLaborInformalFemaleHHInformal : Series
    { }

    [Table("SeriesLaborInformalMale%HHInformal")]
    internal partial class SeriesLaborInformalMaleHHInformal : Series
    { }

    [Table("SeriesLaborInformalFormSec%TotalInformal")]
    internal partial class SeriesLaborInformalFormSecTotalInformal : Series
    { }

    [Table("SeriesLaborInformalFemale%FormalSec")]
    internal partial class SeriesLaborInformalFemaleFormalSec : Series
    { }

    [Table("SeriesLaborInformalMale%FormalSec")]
    internal partial class SeriesLaborInformalMaleFormalSec : Series
    { }

    [Table("SeriesGovSSWelBen%Exp")]
    internal partial class SeriesGovSSWelBenExp : Series
    { }

    [Table("SeriesGovtPension%GDP")]
    internal partial class SeriesGovtPensionGDP : Series
    { }

    [Table("SeriesTaxCorp%Tot")]
    internal partial class SeriesTaxCorpTot : Series
    { }

    [Table("SeriesTaxSocSec%CurRev")]
    internal partial class SeriesTaxSocSecCurRev : Series
    { }

    [Table("SeriesTaxGoodSer%CurRev")]
    internal partial class SeriesTaxGoodSerCurRev : Series
    { }

    //Revenue Series---------------------------------
    [Table("SeriesTotRev%GDPIMF")]
    internal partial class SeriesTotRevGDPIMF : Series
    { }

    [Table("SeriesGovRevOECD%GDP")]
    internal partial class SeriesGovRevOECDGDP : Series
    { }

    [Table("SeriesGovtCalcRevTot%GDP")]
    internal partial class SeriesGovtCalcRevTotGDP : Series
    { }

    [Table("SeriesGovtCurRev%GDP")]
    internal partial class SeriesGovtCurRevGDP : Series
    { }

    [Table("SeriesGovtCalcRevCen%GDP")]
    internal partial class SeriesGovtCalcRevCenGDP : Series
    { }

    [Table("SeriesGovtGenRev%GDPFM")]
    internal partial class SeriesGovtGenRevGDPFM : Series
    { }
    
    //'Expenditure Series---------------------------------
    [Table("SeriesGovTotalOutlays%GDP")]
    internal partial class SeriesGovTotalOutlaysGDP : Series
    { }

    [Table("SeriesGovtCalcExpendTot%GDP")]
    internal partial class SeriesGovtCalcExpendTotGDP : Series
    { }

    [Table("SeriesGovExpense%GDP")]
    internal partial class SeriesGovExpenseGDP : Series
    { }

    [Table("SeriesGovtCalcExpendCen%GDP")]
    internal partial class SeriesGovtCalcExpendCenGDP : Series
    { }

    [Table("SeriesGovtGenExp%GDPFM")]
    internal partial class SeriesGovtGenExpGDPFM : Series
    { }
    
    //SAM Stocks
    [Table("SeriesXWBLoans%GDP")]
    internal partial class SeriesXWBLoansGDP : Series
    { }

    [Table("SeriesXIMFCredit%GDP")]
    internal partial class SeriesXIMFCreditGDP : Series
    { }

    //private external debt
    [Table("SeriesXDEBTPPG%GDP")]
    internal partial class SeriesXDEBTPPGGDP : Series
    { }

    [Table("SeriesXDEBTPNG%GDP")]
    internal partial class SeriesXDEBTPNGGDP : Series
    { }

    [Table("SeriesXDEBT")]
    internal partial class SeriesXDEBT : Series
    { }

    [Table("SeriesXDEBT%GDP")]
    internal partial class SeriesXDEBTGDP : Series
    { }

    [Table("SeriesAidRecGrant%Total")]
    internal partial class SeriesAidRecGrantTotal : Series
    { }

    //bbh 2020/08/18
    [Table("SeriesAidRecGrant%TotRev")]
    internal partial class SeriesAidRecGrantTotRev : Series
    { }

    //SAM Additional components
    [Table("SeriesGovtDebt%GDP")]
    internal partial class SeriesGovtDebtGDP : Series
    { }

    //bbh 2020/08/19
    [Table("SeriesGovtDebt%GDPIMF")]
    internal partial class SeriesGovtDebtGDPIMF : Series
    { }

    [Table("SeriesGovtDebtGen%GDPIMF")]
    internal partial class SeriesGovtDebtGenGDPIMF : Series
    { }

    [Table("SeriesXFDIInflows%GDP")]
    internal partial class SeriesXFDIInflowsGDP : Series
    { }

    [Table("SeriesXFDIOutflows%GDP")]
    internal partial class SeriesXFDIOutflowsGDP : Series
    { }

    [Table("SeriesXPortBonds%GDP")]
    internal partial class SeriesXPortBondsGDP : Series
    { }

    [Table("SeriesXPortEquity%GDP")]
    internal partial class SeriesXPortEquityGDP : Series
    { }

    [Table("SeriesXReserves%GDP")]
    internal partial class SeriesXReservesGDP : Series
    { }

    [Table("SeriesXFlowsIBRD%GDP")]
    internal partial class SeriesXFlowsIBRDGDP : Series
    { }

    [Table("SeriesXFlowsIDA%GDP")]
    internal partial class SeriesXFlowsIDAGDP : Series
    { }

    [Table("SeriesXFlowsIMFCon%GDP")]
    internal partial class SeriesXFlowsIMFConGDP : Series
    { }

    [Table("SeriesXFlowsIMFNonCon%GDP")]
    internal partial class SeriesXFlowsIMFNonConGDP : Series
    { }

    [Table("SeriesPopForeign%")]
    internal partial class SeriesPopForeign : Series
    { }

    [Table("SeriesXIncPayments%GDP")]
    internal partial class SeriesXIncPaymentsGDP : Series
    { }

    [Table("SeriesXIncReceipts%GDP")]
    internal partial class SeriesXIncReceiptsGDP : Series
    { }

    [Table("SeriesIncShareL20%")]
    internal partial class SeriesIncShareL20 : Series
    { }

    [Table("SeriesGINIExtended")]
    internal partial class SeriesGINIExtended : Series
    { }

    [Table("SeriesIncBelow2Dollar%PovCal2015")]
    internal partial class SeriesIncBelow2DollarPovCal2015 : Series
    { }

    [Table("SeriesIncBelow1D90c%WDI")]
    internal partial class SeriesIncBelow1D90cWDI : Series
    { }

    [Table("SeriesIncBelow1D90c%Povcal")]
    internal partial class SeriesIncBelow1D90cPovcal : Series
    { }

    [Table("SeriesIncBelow2D15c%")]
    internal partial class SeriesIncBelow2D15c : Series
    { }

    [Table("SeriesIncBelow3D65c%")]
    internal partial class SeriesIncBelow3D65c : Series
    { }

    [Table("SeriesIncBelow6D85c%")]
    internal partial class SeriesIncBelow6D85c : Series
    { }

    //jrs 2024/02/26
    [Table("SeriesIncBelow2D15c%PIP")]
    internal partial class SeriesIncBelow2D15cPIP : Series
    { }

    [Table("SeriesIncBelow3D65c%PIP")]
    internal partial class SeriesIncBelow3D65cPIP : Series
    { }

    [Table("SeriesIncBelow6D85c%PIP")]
    internal partial class SeriesIncBelow6D85cPIP : Series
    { }

    [Table("SeriesIncBelow3D10c%WDI")]
    internal partial class SeriesIncBelow3D10cWDI : Series
    { }

    [Table("SeriesPovertyGap$1c90perDay")]
    internal partial class SeriesPovertyGap1c90perDay : Series
    { }

    [Table("SeriesIncBelow3D20c%Povcal")]
    internal partial class SeriesIncBelow3D20cPovcal : Series
    { }

    [Table("SeriesIncBelow5D50c%Povcal")]
    internal partial class SeriesIncBelow5D50cPovcal : Series
    { }

    [Table("SeriesIncBelowNPL%WDI")]
    internal partial class SeriesIncBelowNPLWDI : Series
    { }

    //jrs 2025/09/12
    [Table("SeriesIncBelow3D00c%PIP")]
    internal partial class SeriesIncBelow3D00cPIP : Series
    { }

    [Table("SeriesIncBelow4D20c%PIP")]
    internal partial class SeriesIncBelow4D20cPIP : Series
    { }

    [Table("SeriesIncBelow8D30c%PIP")]
    internal partial class SeriesIncBelow8D30cPIP : Series
    { }

    [Table("SeriesTaxImportDuties%Rev")]
    internal partial class SeriesTaxImportDutiesRev : Series
    { }

    [Table("SeriesPovGap1D90cperDayPovcal")]
    internal partial class SeriesPovGap1D90cperDayPovcal : Series
    { }

    [Table("SeriesPovGap3D20perDayPovcal")]
    internal partial class SeriesPovGap3D20perDayPovcal : Series
    { }

    [Table("SeriesPovGap5D50cperDayPovcal")]
    internal partial class SeriesPovGap5D50cperDayPovcal : Series
    { }

    [Table("SeriesPovGap2D15cperDay")]
    internal partial class SeriesPovGap2D15cperDay : Series
    { }

    [Table("SeriesPovGap3D65perDay")]
    internal partial class SeriesPovGap3D65cperDay : Series
    { }

    [Table("SeriesPovGap6D85cperDay")]
    internal partial class SeriesPovGap6D85cperDay : Series
    { }

    //jrs 2025/09/16
    [Table("SeriesPovGap3D00cperDayPIP")]
    internal partial class SeriesPovGap3D00cperDayPIP : Series
    { }

    [Table("SeriesPovGap4D20cperDayPIP")]
    internal partial class SeriesPovGap4D20cperDayPIP : Series
    { }

    [Table("SeriesPovGap8D30cperDayPIP")]
    internal partial class SeriesPovGap8D30cperDayPIP : Series
    { }

    [Table("SeriesLaborAgriSector")]
    internal partial class SeriesLaborAgriSector : Series
    { }

    [Table("SeriesPovLineNationalWB")]
    internal partial class SeriesPovLineNationalWB : Series
    { }

    [Table("SeriesPovLineNationalWB2017")]
    internal partial class SeriesPovLineNationalWB2017 : Series
    { }

    [Table("SeriesPovLineNationalWB2021")]
    internal partial class SeriesPovLineNationalWB2021 : Series
    { }

    [Table("SeriesArmsExportsMonadic%GDP")]
    internal partial class SeriesArmsExportsGDP : Series
    { }

    [Table("SeriesArmsImportsMonadic%GDP")]
    internal partial class SeriesArmsImportsGDP : Series
    { }

    //jrs 2022/06/08 
    [Table("SeriesForecastGiniRaoSSP1")]
    internal partial class SeriesForecastGiniRaoSSP1 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP1ConflictAdj")]
    internal partial class SeriesForecastGiniRaoSSP1ConflictAdj : Series
    { }

    [Table("SeriesForecastGiniRaoSSP1ConflictAdjHi")]
    internal partial class SeriesForecastGiniRaoSSP1ConflictAdjHi : Series
    { }

    [Table("SeriesForecastGiniRaoSSP1ConflictAdjLo")]
    internal partial class SeriesForecastGiniRaoSSP1ConflictAdjLo : Series
    { }

    [Table("SeriesForecastGiniRaoSSP2")]
    internal partial class SeriesForecastGiniRaoSSP2 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP2ConflictAdj")]
    internal partial class SeriesForecastGiniRaoSSP2ConflictAdj : Series
    { }

    [Table("SeriesForecastGiniRaoSSP2ConflictAdjHi")]
    internal partial class SeriesForecastGiniRaoSSP2ConflictAdjHi : Series
    { }

    [Table("SeriesForecastGiniRaoSSP2ConflictAdjLo")]
    internal partial class SeriesForecastGiniRaoSSP2ConflictAdjLo : Series
    { }

    [Table("SeriesForecastGiniRaoSSP3")]
    internal partial class SeriesForecastGiniRaoSSP3 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP3ConflictAdj")]
    internal partial class SeriesForecastGiniRaoSSP3ConflictAdj : Series
    { }

    [Table("SeriesForecastGiniRaoSSP3ConflictAdjHi")]
    internal partial class SeriesForecastGiniRaoSSP3ConflictAdjHi : Series
    { }

    [Table("SeriesForecastGiniRaoSSP3ConflictAdjLo")]
    internal partial class SeriesForecastGiniRaoSSP3ConflictAdjLo : Series
    { }

    [Table("SeriesForecastGiniRaoSSP4")]
    internal partial class SeriesForecastGiniRaoSSP4 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP4ConflictAdj")]
    internal partial class SeriesForecastGiniRaoSSP4ConflictAdj : Series
    { }

    [Table("SeriesForecastGiniRaoSSP4ConflictAdjHi")]
    internal partial class SeriesForecastGiniRaoSSP4ConflictAdjHi : Series
    { }

    [Table("SeriesForecastGiniRaoSSP4ConflictAdjLo")]
    internal partial class SeriesForecastGiniRaoSSP4ConflictAdjLo : Series
    { }

    [Table("SeriesForecastGiniRaoSSP5")]
    internal partial class SeriesForecastGiniRaoSSP5 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP5ConflictAdj")]
    internal partial class SeriesForecastGiniRaoSSP5ConflictAdj : Series
    { }

    [Table("SeriesForecastGiniRaoSSP5ConflictAdjHi")]
    internal partial class SeriesForecastGiniRaoSSP5ConflictAdjHi : Series
    { }

    [Table("SeriesForecastGiniRaoSSP5ConflictAdjLo")]
    internal partial class SeriesForecastGiniRaoSSP5ConflictAdjLo : Series
    { }
    
    // clp 2022/06/12 Adding military expenditures stock
    [Table("SeriesMilExpendStock")]
    internal partial class SeriesMilExpendStock : Series
    { }

    //bbh 2022/06/04
    [Table("SeriesAIDRec%GNIOECD")]
    internal partial class SeriesAIDRecGNIOECD : Series
    { }
   
    [Table("SeriesXIncSecPayments%GDP")]
    internal partial class SeriesXIncSecPaymentsGDP : Series
    { }

    [Table("SeriesXIncSecReceipts%GDP")]
    internal partial class SeriesXIncSecReceiptsGDP : Series
    { }

    [Table("SeriesForecastGiniRaoSSP1RCP26")]
    internal partial class SeriesForecastGiniRaoSSP1RCP26 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP1RCP26")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP1RCP26 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP1RCP45")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP1RCP45 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP1RCP45")]
    internal partial class SeriesForecastGiniRaoSSP1RCP45 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP1RCP60")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP1RCP60 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP1RCP60")]
    internal partial class SeriesForecastGiniRaoSSP1RCP60 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP1RCP85")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP1RCP85 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP1RCP85")]
    internal partial class SeriesForecastGiniRaoSSP1RCP85 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP2RCP26")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP2RCP26 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP2RCP26")]
    internal partial class SeriesForecastGiniRaoSSP2RCP26 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP2RCP45")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP2RCP45 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP2RCP45")]
    internal partial class SeriesForecastGiniRaoSSP2RCP45 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP2RCP60")]
    internal partial class SeriesForecastGiniRaoSSP2RCP60 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP2RCP60")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP2RCP60 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP2RCP85")]
    internal partial class SeriesForecastGiniRaoSSP2RCP85 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP2RCP85")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP2RCP85 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP3RCP26")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP3RCP26 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP3RCP26")]
    internal partial class SeriesForecastGiniRaoSSP3RCP26 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP3RCP45")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP3RCP45 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP3RCP45")]
    internal partial class SeriesForecastGiniRaoSSP3RCP45 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP3RCP60")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP3RCP60 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP3RCP60")]
    internal partial class SeriesForecastGiniRaoSSP3RCP60 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP3RCP85")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP3RCP85 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP3RCP85")]
    internal partial class SeriesForecastGiniRaoSSP3RCP85 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP4RCP26")]
    internal partial class SeriesForecastGiniRaoSSP4RCP26 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP4RCP26")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP4RCP26 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP4RCP45")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP4RCP45 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP4RCP45")]
    internal partial class SeriesForecastGiniRaoSSP4RCP45 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP4RCP60")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP4RCP60 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP4RCP60")]
    internal partial class SeriesForecastGiniRaoSSP4RCP60 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP4RCP85")]
    internal partial class SeriesForecastGiniRaoSSP4RCP85 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP4RCP85")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP4RCP85 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP5RCP26")]
    internal partial class SeriesForecastGiniRaoSSP5RCP26 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP5RCP26")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP5RCP26 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP5RCP45")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP5RCP45 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP5RCP45")]
    internal partial class SeriesForecastGiniRaoSSP5RCP45 : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP5RCP60")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP5RCP60 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP5RCP60")]
    internal partial class SeriesForecastGiniRaoSSP5RCP60 : Series
    { }

    [Table("SeriesForecastGiniRaoSSP5RCP85")]
    internal partial class SeriesForecastGiniRaoSSP5RCP85 : Series
    { }

    //jrs 2023/05/19 start
    [Table("SeriesForecastGiniRaoSSP1RCP26Pag")]
    internal partial class SeriesForecastGiniRaoSSP1RCP26Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP1RCP45Pag")]
    internal partial class SeriesForecastGiniRaoSSP1RCP45Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP1RCP60Pag")]
    internal partial class SeriesForecastGiniRaoSSP1RCP60Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP1RCP85Pag")]
    internal partial class SeriesForecastGiniRaoSSP1RCP85Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP2RCP26Pag")]
    internal partial class SeriesForecastGiniRaoSSP2RCP26Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP2RCP45Pag")]
    internal partial class SeriesForecastGiniRaoSSP2RCP45Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP2RCP60Pag")]
    internal partial class SeriesForecastGiniRaoSSP2RCP60Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP2RCP85Pag")]
    internal partial class SeriesForecastGiniRaoSSP2RCP85Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP3RCP26Pag")]
    internal partial class SeriesForecastGiniRaoSSP3RCP26Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP3RCP45Pag")]
    internal partial class SeriesForecastGiniRaoSSP3RCP45Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP3RCP60Pag")]
    internal partial class SeriesForecastGiniRaoSSP3RCP60Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP3RCP85Pag")]
    internal partial class SeriesForecastGiniRaoSSP3RCP85Pag : Series
    { }
    [Table("SeriesForecastGiniRaoSSP4RCP26Pag")]
    internal partial class SeriesForecastGiniRaoSSP4RCP26Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP4RCP45Pag")]
    internal partial class SeriesForecastGiniRaoSSP4RCP45Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP4RCP60Pag")]
    internal partial class SeriesForecastGiniRaoSSP4RCP60Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP4RCP85Pag")]
    internal partial class SeriesForecastGiniRaoSSP4RCP85Pag : Series
    { }
    [Table("SeriesForecastGiniRaoSSP5RCP26Pag")]
    internal partial class SeriesForecastGiniRaoSSP5RCP26Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP5RCP45Pag")]
    internal partial class SeriesForecastGiniRaoSSP5RCP45Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP5RCP60Pag")]
    internal partial class SeriesForecastGiniRaoSSP5RCP60Pag : Series
    { }

    [Table("SeriesForecastGiniRaoSSP5RCP85Pag")]
    internal partial class SeriesForecastGiniRaoSSP5RCP85Pag : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP1RCP26TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP1RCP26TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP1RCP45TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP1RCP45TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP1RCP60TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP1RCP60TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP1RCP85TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP1RCP85TP : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP2RCP26TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP2RCP26TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP2RCP45TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP2RCP45TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP2RCP60TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP2RCP60TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP2RCP85TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP2RCP85TP : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP3RCP26TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP3RCP26TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP3RCP45TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP3RCP45TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP3RCP60TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP3RCP60TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP3RCP85TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP3RCP85TP : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP4RCP26TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP4RCP26TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP4RCP45TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP4RCP45TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP4RCP60TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP4RCP60TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP4RCP85TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP4RCP85TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP5RCP26TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP5RCP26TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP5RCP45TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP5RCP45TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP5RCP60TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP5RCP60TP : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP5RCP85TP")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP5RCP85TP : Series
    { }
    [Table("SeriesForecastGiniRaoSSP1RCP26Das")]
    internal partial class SeriesForecastGiniRaoSSP1RCP26Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP1RCP45Das")]
    internal partial class SeriesForecastGiniRaoSSP1RCP45Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP1RCP60Das")]
    internal partial class SeriesForecastGiniRaoSSP1RCP60Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP1RCP85Das")]
    internal partial class SeriesForecastGiniRaoSSP1RCP85Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP2RCP26Das")]
    internal partial class SeriesForecastGiniRaoSSP2RCP26Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP2RCP45Das")]
    internal partial class SeriesForecastGiniRaoSSP2RCP45Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP2RCP60Das")]
    internal partial class SeriesForecastGiniRaoSSP2RCP60Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP2RCP85Das")]
    internal partial class SeriesForecastGiniRaoSSP2RCP85Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP3RCP26Das")]
    internal partial class SeriesForecastGiniRaoSSP3RCP26Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP3RCP45Das")]
    internal partial class SeriesForecastGiniRaoSSP3RCP45Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP3RCP60Das")]
    internal partial class SeriesForecastGiniRaoSSP3RCP60Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP3RCP85Das")]
    internal partial class SeriesForecastGiniRaoSSP3RCP85Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP4RCP26Das")]
    internal partial class SeriesForecastGiniRaoSSP4RCP26Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP4RCP45Das")]
    internal partial class SeriesForecastGiniRaoSSP4RCP45Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP4RCP60Das")]
    internal partial class SeriesForecastGiniRaoSSP4RCP60Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP4RCP85Das")]
    internal partial class SeriesForecastGiniRaoSSP4RCP85Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP5RCP26Das")]
    internal partial class SeriesForecastGiniRaoSSP5RCP26Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP5RCP45Das")]
    internal partial class SeriesForecastGiniRaoSSP5RCP45Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP5RCP60Das")]
    internal partial class SeriesForecastGiniRaoSSP5RCP60Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP5RCP85Das")]
    internal partial class SeriesForecastGiniRaoSSP5RCP85Das : Series
    { }
    [Table("SeriesForecastGiniRaoSSP1RCP26Hall")]
    internal partial class SeriesForecastGiniRaoSSP1RCP26Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP1RCP45Hall")]
    internal partial class SeriesForecastGiniRaoSSP1RCP45Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP1RCP60Hall")]
    internal partial class SeriesForecastGiniRaoSSP1RCP60Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP1RCP85Hall")]
    internal partial class SeriesForecastGiniRaoSSP1RCP85Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP2RCP26Hall")]
    internal partial class SeriesForecastGiniRaoSSP2RCP26Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP2RCP45Hall")]
    internal partial class SeriesForecastGiniRaoSSP2RCP45Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP2RCP60Hall")]
    internal partial class SeriesForecastGiniRaoSSP2RCP60Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP2RCP85Hall")]
    internal partial class SeriesForecastGiniRaoSSP2RCP85Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP3RCP26Hall")]
    internal partial class SeriesForecastGiniRaoSSP3RCP26Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP3RCP45Hall")]
    internal partial class SeriesForecastGiniRaoSSP3RCP45Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP3RCP60Hall")]
    internal partial class SeriesForecastGiniRaoSSP3RCP60Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP3RCP85Hall")]
    internal partial class SeriesForecastGiniRaoSSP3RCP85Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP4RCP26Hall")]
    internal partial class SeriesForecastGiniRaoSSP4RCP26Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP4RCP45Hall")]
    internal partial class SeriesForecastGiniRaoSSP4RCP45Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP4RCP60Hall")]
    internal partial class SeriesForecastGiniRaoSSP4RCP60Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP4RCP85Hall")]
    internal partial class SeriesForecastGiniRaoSSP4RCP85Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP5RCP26Hall")]
    internal partial class SeriesForecastGiniRaoSSP5RCP26Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP5RCP45Hall")]
    internal partial class SeriesForecastGiniRaoSSP5RCP45Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP5RCP60Hall")]
    internal partial class SeriesForecastGiniRaoSSP5RCP60Hall : Series
    { }
    [Table("SeriesForecastGiniRaoSSP5RCP85Hall")]
    internal partial class SeriesForecastGiniRaoSSP5RCP85Hall : Series
    { }
    //jrs 2023/05/19 end

    [Table("SeriesForecastGDP2011PCPPPOECDSSP5RCP85")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP5RCP85 : Series
    { }

    [Table("SeriesForecastEnvTempChgFLAT")]
    internal partial class SeriesForecastEnvTempChgFLAT : Series
    { }

    //jrs 2023/09/01
    [Table("SeriesForecastGDP2011PCPPPIIASASSP1")]
    internal partial class SeriesForecastGDP2011PCPPPIIASASSP1 : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPIIASASSP2")]
    internal partial class SeriesForecastGDP2011PCPPPIIASASSP2 : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPIIASASSP3")]
    internal partial class SeriesForecastGDP2011PCPPPIIASASSP3 : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPIIASASSP4")]
    internal partial class SeriesForecastGDP2011PCPPPIIASASSP4 : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPIIASASSP5")]
    internal partial class SeriesForecastGDP2011PCPPPIIASASSP5 : Series
    { }

    [Table("SeriesForecastGDP2017PCPPPIIASASSP1Rev23")]
    internal partial class SeriesForecastGDP2017PCPPPIIASASSP1Rev23: Series
    { }
    [Table("SeriesForecastGDP2017PCPPPIIASASSP2Rev23")]
    internal partial class SeriesForecastGDP2017PCPPPIIASASSP2Rev23 : Series
    { }
    [Table("SeriesForecastGDP2017PCPPPIIASASSP3Rev23")]
    internal partial class SeriesForecastGDP2017PCPPPIIASASSP3Rev23 : Series
    { }
    [Table("SeriesForecastGDP2017PCPPPIIASASSP4Rev23")]
    internal partial class SeriesForecastGDP2017PCPPPIIASASSP4Rev23: Series
    { }
    [Table("SeriesForecastGDP2017PCPPPIIASASSP5Rev23")]
    internal partial class SeriesForecastGDP2017PCPPPIIASASSP5Rev23 : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP1")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP1 : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP2")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP2 : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP3")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP3 : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP4")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP4 : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP5")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP5 : Series
    { }

    [Table("SeriesForecastGDP2017PCPPPOECDSSP1Rev23")]
    internal partial class SeriesForecastGDP2017PCPPPOECDSSP1Rev23 : Series
    { }
    [Table("SeriesForecastGDP2017PCPPPOECDSSP2Rev23")]
    internal partial class SeriesForecastGDP2017PCPPPOECDSSP2Rev23 : Series
    { }
    [Table("SeriesForecastGDP2017PCPPPOECDSSP3Rev23")]
    internal partial class SeriesForecastGDP2017PCPPPOECDSSP3Rev23 : Series
    { }
    [Table("SeriesForecastGDP2017PCPPPOECDSSP4Rev23")]
    internal partial class SeriesForecastGDP2017PCPPPOECDSSP4Rev23 : Series
    { }
    [Table("SeriesForecastGDP2017PCPPPOECDSSP5Rev23")]
    internal partial class SeriesForecastGDP2017PCPPPOECDSSP5Rev23 : Series
    { }

    [Table("SeriesForecastGDP2011IIASASSP1")]
    internal partial class SeriesForecastGDP2011IIASASSP1 : Series
    { }
    [Table("SeriesForecastGDP2011IIASASSP2")]
    internal partial class SeriesForecastGDP2011IIASASSP2 : Series
    { }
    [Table("SeriesForecastGDP2011IIASASSP3")]
    internal partial class SeriesForecastGDP2011IIASASSP3 : Series
    { }
    [Table("SeriesForecastGDP2011IIASASSP4")]
    internal partial class SeriesForecastGDP2011IIASASSP4 : Series
    { }
    [Table("SeriesForecastGDP2011IIASASSP5")]
    internal partial class SeriesForecastGDP2011IIASASSP5 : Series
    { }

    [Table("SeriesForecastGDP2017IIASASSP1Rev23")]
    internal partial class SeriesForecastGDP2017IIASASSP1Rev23 : Series
    { }
    [Table("SeriesForecastGDP2017IIASASSP2Rev23")]
    internal partial class SeriesForecastGDP2017IIASASSP2Rev23 : Series
    { }
    [Table("SeriesForecastGDP2017IIASASSP3Rev23")]
    internal partial class SeriesForecastGDP2017IIASASSP3Rev23 : Series
    { }
    [Table("SeriesForecastGDP2017IIASASSP4Rev23")]
    internal partial class SeriesForecastGDP2017IIASASSP4Rev23 : Series
    { }
    [Table("SeriesForecastGDP2017IIASASSP5Rev23")]
    internal partial class SeriesForecastGDP2017IIASASSP5Rev23 : Series
    { }
    [Table("SeriesForecastGDP2011OECDSSP1")]
    internal partial class SeriesForecastGDP2011OECDSSP1 : Series
    { }
    [Table("SeriesForecastGDP2011OECDSSP2")]
    internal partial class SeriesForecastGDP2011OECDSSP2 : Series
    { }
    [Table("SeriesForecastGDP2011OECDSSP3")]
    internal partial class SeriesForecastGDP2011OECDSSP3 : Series
    { }
    [Table("SeriesForecastGDP2011OECDSSP4")]
    internal partial class SeriesForecastGDP2011OECDSSP4 : Series
    { }
    [Table("SeriesForecastGDP2011OECDSSP5")]
    internal partial class SeriesForecastGDP2011OECDSSP5 : Series
    { }

    [Table("SeriesForecastGDP2017OECDSSP1Rev23")]
    internal partial class SeriesForecastGDP2017OECDSSP1Rev23 : Series
    { }
    [Table("SeriesForecastGDP2017OECDSSP2Rev23")]
    internal partial class SeriesForecastGDP2017OECDSSP2Rev23 : Series
    { }
    [Table("SeriesForecastGDP2017OECDSSP3Rev23")]
    internal partial class SeriesForecastGDP2017OECDSSP3Rev23 : Series
    { }
    [Table("SeriesForecastGDP2017OECDSSP4Rev23")]
    internal partial class SeriesForecastGDP2017OECDSSP4Rev23 : Series
    { }
    [Table("SeriesForecastGDP2017OECDSSP5Rev23")]
    internal partial class SeriesForecastGDP2017OECDSSP5Rev23 : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP1ConflictAdj")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP1ConflictAdj : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP1ConflictAdjHi")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP1ConflictAdjHi : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP1ConflictAdjLo")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP1ConflictAdjLo : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP2ConflictAdj")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP2ConflictAdj : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP2ConflictAdjHi")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP2ConflictAdjHi : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP2ConflictAdjLo")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP2ConflictAdjLo : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP3ConflictAdj")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP3ConflictAdj : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP3ConflictAdjHi")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP3ConflictAdjHi : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP3ConflictAdjLo")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP3ConflictAdjLo : Series
    { }
    [Table("SeriesForecastGDP2011PCPPPOECDSSP4ConflictAdj")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP4ConflictAdj : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP4ConflictAdjHi")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP4ConflictAdjHi : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP4ConflictAdjLo")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP4ConflictAdjLo : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP5ConflictAdj")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP5ConflictAdj : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP5ConflictAdjHi")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP5ConflictAdjHi : Series
    { }

    [Table("SeriesForecastGDP2011PCPPPOECDSSP5ConflictAdjLo")]
    internal partial class SeriesForecastGDP2011PCPPPOECDSSP5ConflictAdjLo : Series
    { }

    //jrs 2023/11/10
    [Table("SeriesGDPPCPIMAGELIFETP")]
    internal partial class SeriesGDPPCPIMAGELIFETP : Series
    { }
    [Table("SeriesInvestmentIMAGELIFETP")]
    internal partial class SeriesInvestmentIMAGELIFETP : Series
    { }

    [Table("SeriesGDPPCPIMAGETECHTP")]
    internal partial class SeriesGDPPCPIMAGETECHTP : Series
    { }
    [Table("SeriesInvestmentIMAGETECHTP")]
    internal partial class SeriesInvestmentIMAGETECHTP : Series
    { }
    //jrs 2023/11/30
    [Table("SeriesGDPPCPIMAGEREFTP")]
    internal partial class SeriesGDPPCPIMAGEREFTP : Series
    { }
    [Table("SeriesInvestmentIMAGEREFTP")]
    internal partial class SeriesInvestmentIMAGEREFTP : Series
    { }
    [Table("SeriesGDPPCPIMAGEREF1P5C")]
    internal partial class SeriesGDPPCPIMAGEREF1P5C : Series
    { }
    [Table("SeriesInvestmentIMAGEREF1P5C")]
    internal partial class SeriesInvestmentIMAGEREF1P5C : Series
    { }
    //jrs 2024/02/19 
    [Table("SeriesExoGDPPCP")]
    internal partial class SeriesExoGDPPCP : Series
    { }
    [Table("SeriesExoGINIDOM")]
    internal partial class SeriesExoGINIDOM : Series
    { }
    //jrs 2025/04/15
    [Table("SeriesExoGDP")]
    internal partial class SeriesExoGDP : Series
    { }
    //jrs 2025/07/18
    [Table("SeriesLaborEmploymentAgILO")]
    internal partial class SeriesLaborEmploymentAgILO : Series
    { }
    [Table("SeriesLaborEmploymentSerILO")]
    internal partial class SeriesLaborEmploymentSerILO : Series
    { }
    [Table("SeriesLaborEmploymentManuILO")]
    internal partial class SeriesLaborEmploymentManuILO : Series
    { }
    //jrs 2026/04/08
    [Table("SeriesGovRevGenTaxRev%GDP")]
    internal partial class SeriesGovRevGenTaxRevGDP : Series
    { }
    [Table("SeriesGovRevGenTotRev%GDP")]
    internal partial class SeriesGovRevGenTotRevGDP : Series
    { }

    [Table("SeriesGovRevGenSocialCon%GDP")]
    internal partial class SeriesGovRevGenSocialConGDP : Series
    { }

    [Table("SeriesGovRevGenTaxIncI%GDP")]
    internal partial class SeriesGovRevGenTaxIncIGDP : Series
    { }

    internal partial class IFsHistSeriesContext
    {
        //jrs 2026/04/08  
        protected virtual DbSet<SeriesGovRevGenTaxRevGDP> SeriesGovRevGenTaxRevGDP { get; set; }
        protected virtual DbSet<SeriesGovRevGenTotRevGDP> SeriesGovRevGenTotRevGDP { get; set; }
        protected virtual DbSet<SeriesGovRevGenSocialConGDP> SeriesGovRevGenSocialConGDP { get; set; }
        protected virtual DbSet<SeriesGovRevGenTaxIncIGDP> SeriesGovRevGenTaxIncIGDP { get; set; }

        //jrs 2025/07/18 
        protected virtual DbSet<SeriesLaborEmploymentAgILO> SeriesLaborEmploymentAgILO { get; set; }
        protected virtual DbSet<SeriesLaborEmploymentSerILO> SeriesLaborEmploymentSerILO { get; set; }
        protected virtual DbSet<SeriesLaborEmploymentManuILO> SeriesLaborEmploymentManuILO { get; set; }

        //jrs 2025/04/15
        protected virtual DbSet<SeriesExoGDP> SeriesExoGDP { get; set; }
        //jrs 2024/02/19
        protected virtual DbSet<SeriesExoGDPPCP> SeriesExoGDPPCP { get; set; }

        protected virtual DbSet<SeriesExoGINIDOM> SeriesExoGINIDOM { get; set; }
        //jrs 2023/11/10
        protected virtual DbSet<SeriesGDPPCPIMAGELIFETP> SeriesGDPPCPIMAGELIFETP { get; set; }
        protected virtual DbSet<SeriesInvestmentIMAGELIFETP> SeriesInvestmentIMAGELIFETP { get; set; }

        protected virtual DbSet<SeriesGDPPCPIMAGETECHTP> SeriesGDPPCPIMAGETECHTP { get; set; }
        protected virtual DbSet<SeriesInvestmentIMAGETECHTP> SeriesInvestmentIMAGETECHTP { get; set; }
        //jrs 2023/11/30
        protected virtual DbSet<SeriesGDPPCPIMAGEREFTP> SeriesGDPPCPIMAGEREFTP { get; set; }
        protected virtual DbSet<SeriesInvestmentIMAGEREFTP> SeriesInvestmentIMAGEREFTP { get; set; }
        protected virtual DbSet<SeriesGDPPCPIMAGEREF1P5C> SeriesGDPPCPIMAGEREF1P5C { get; set; }
        protected virtual DbSet<SeriesInvestmentIMAGEREF1P5C> SeriesInvestmentIMAGEREF1P5C { get; set; }



        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP5ConflictAdj> SeriesForecastGDP2011PCPPPOECDSSP5ConflictAdj { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP5ConflictAdjHi> SeriesForecastGDP2011PCPPPOECDSSP5ConflictAdjHi { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP5ConflictAdjLo> SeriesForecastGDP2011PCPPPOECDSSP5ConflictAdjLo { get; set; }

        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP4ConflictAdj> SeriesForecastGDP2011PCPPPOECDSSP4ConflictAdj { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP4ConflictAdjHi> SeriesForecastGDP2011PCPPPOECDSSP4ConflictAdjHi { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP4ConflictAdjLo> SeriesForecastGDP2011PCPPPOECDSSP4ConflictAdjLo { get; set; }

        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP3ConflictAdj> SeriesForecastGDP2011PCPPPOECDSSP3ConflictAdj { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP3ConflictAdjHi> SeriesForecastGDP2011PCPPPOECDSSP3ConflictAdjHi { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP3ConflictAdjLo> SeriesForecastGDP2011PCPPPOECDSSP3ConflictAdjLo { get; set; }

        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP2ConflictAdj> SeriesForecastGDP2011PCPPPOECDSSP2ConflictAdj { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP2ConflictAdjHi> SeriesForecastGDP2011PCPPPOECDSSP2ConflictAdjHi { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP2ConflictAdjLo> SeriesForecastGDP2011PCPPPOECDSSP2ConflictAdjLo { get; set; }

        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP1ConflictAdj> SeriesForecastGDP2011PCPPPOECDSSP1ConflictAdj { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP1ConflictAdjHi> SeriesForecastGDP2011PCPPPOECDSSP1ConflictAdjHi { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP1ConflictAdjLo> SeriesForecastGDP2011PCPPPOECDSSP1ConflictAdjLo { get; set; }


        protected virtual DbSet<SeriesForecastGDP2017OECDSSP1Rev23> SeriesForecastGDP2017OECDSSP1Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017OECDSSP2Rev23> SeriesForecastGDP2017OECDSSP2Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017OECDSSP3Rev23> SeriesForecastGDP2017OECDSSP3Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017OECDSSP4Rev23> SeriesForecastGDP2017OECDSSP4Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017OECDSSP5Rev23> SeriesForecastGDP2017OECDSSP5Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011OECDSSP1> SeriesForecastGDP2011OECDSSP1 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011OECDSSP2> SeriesForecastGDP2011OECDSSP2 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011OECDSSP3> SeriesForecastGDP2011OECDSSP3 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011OECDSSP4> SeriesForecastGDP2011OECDSSP4 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011OECDSSP5> SeriesForecastGDP2011OECDSSP5 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017IIASASSP1Rev23> SeriesForecastGDP2017IIASASSP1Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017IIASASSP2Rev23> SeriesForecastGDP2017IIASASSP2Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017IIASASSP3Rev23> SeriesForecastGDP2017IIASASSP3Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017IIASASSP4Rev23> SeriesForecastGDP2017IIASASSP4Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017IIASASSP5Rev23> SeriesForecastGDP2017IIASASSP5Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011IIASASSP1> SeriesForecastGDP2011IIASASSP1 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011IIASASSP2> SeriesForecastGDP2011IIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011IIASASSP3> SeriesForecastGDP2011IIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011IIASASSP4> SeriesForecastGDP2011IIASASSP4 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011IIASASSP5> SeriesForecastGDP2011IIASASSP5 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017PCPPPOECDSSP1Rev23> SeriesForecastGDP2017PCPPPOECDSSP1Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017PCPPPOECDSSP2Rev23> SeriesForecastGDP2017PCPPPOECDSSP2Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017PCPPPOECDSSP3Rev23> SeriesForecastGDP2017PCPPPOECDSSP3Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017PCPPPOECDSSP4Rev23> SeriesForecastGDP2017PCPPPOECDSSP4Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017PCPPPOECDSSP5Rev23> SeriesForecastGDP2017PCPPPOECDSSP5Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP1> SeriesForecastGDP2011PCPPPOECDSSP1 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP2> SeriesForecastGDP2011PCPPPOECDSSP2 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP3> SeriesForecastGDP2011PCPPPOECDSSP3 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP4> SeriesForecastGDP2011PCPPPOECDSSP4 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP5> SeriesForecastGDP2011PCPPPOECDSSP5 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017PCPPPIIASASSP1Rev23> SeriesForecastGDP2017PCPPPIIASASSP1Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017PCPPPIIASASSP2Rev23> SeriesForecastGDP2017PCPPPIIASASSP2Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017PCPPPIIASASSP3Rev23> SeriesForecastGDP2017PCPPPIIASASSP3Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017PCPPPIIASASSP4Rev23> SeriesForecastGDP2017PCPPPIIASASSP4Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2017PCPPPIIASASSP5Rev23> SeriesForecastGDP2017PCPPPIIASASSP5Rev23 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPIIASASSP1> SeriesForecastGDP2011PCPPPIIASASSP1 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPIIASASSP2> SeriesForecastGDP2011PCPPPIIASASSP2 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPIIASASSP3> SeriesForecastGDP2011PCPPPIIASASSP3 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPIIASASSP4> SeriesForecastGDP2011PCPPPIIASASSP4 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPIIASASSP5> SeriesForecastGDP2011PCPPPIIASASSP5 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP26Hall> SeriesForecastGiniRaoSSP1RCP26Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP45Hall> SeriesForecastGiniRaoSSP1RCP45Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP60Hall> SeriesForecastGiniRaoSSP1RCP60Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP85Hall> SeriesForecastGiniRaoSSP1RCP85Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP26Hall> SeriesForecastGiniRaoSSP2RCP26Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP45Hall> SeriesForecastGiniRaoSSP2RCP45Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP60Hall> SeriesForecastGiniRaoSSP2RCP60Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP85Hall> SeriesForecastGiniRaoSSP2RCP85Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP26Hall> SeriesForecastGiniRaoSSP3RCP26Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP45Hall> SeriesForecastGiniRaoSSP3RCP45Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP60Hall> SeriesForecastGiniRaoSSP3RCP60Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP85Hall> SeriesForecastGiniRaoSSP3RCP85Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP26Hall> SeriesForecastGiniRaoSSP4RCP26Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP45Hall> SeriesForecastGiniRaoSSP4RCP45Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP60Hall> SeriesForecastGiniRaoSSP4RCP60Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP85Hall> SeriesForecastGiniRaoSSP4RCP85Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP26Hall> SeriesForecastGiniRaoSSP5RCP26Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP45Hall> SeriesForecastGiniRaoSSP5RCP45Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP60Hall> SeriesForecastGiniRaoSSP5RCP60Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP85Hall> SeriesForecastGiniRaoSSP5RCP85Hall { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP26Das> SeriesForecastGiniRaoSSP1RCP26Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP45Das> SeriesForecastGiniRaoSSP1RCP45Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP60Das> SeriesForecastGiniRaoSSP1RCP60Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP85Das> SeriesForecastGiniRaoSSP1RCP85Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP26Das> SeriesForecastGiniRaoSSP2RCP26Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP45Das> SeriesForecastGiniRaoSSP2RCP45Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP60Das> SeriesForecastGiniRaoSSP2RCP60Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP85Das> SeriesForecastGiniRaoSSP2RCP85Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP26Das> SeriesForecastGiniRaoSSP3RCP26Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP45Das> SeriesForecastGiniRaoSSP3RCP45Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP60Das> SeriesForecastGiniRaoSSP3RCP60Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP85Das> SeriesForecastGiniRaoSSP3RCP85Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP26Das> SeriesForecastGiniRaoSSP4RCP26Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP45Das> SeriesForecastGiniRaoSSP4RCP45Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP60Das> SeriesForecastGiniRaoSSP4RCP60Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP85Das> SeriesForecastGiniRaoSSP4RCP85Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP26Das> SeriesForecastGiniRaoSSP5RCP26Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP45Das> SeriesForecastGiniRaoSSP5RCP45Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP60Das> SeriesForecastGiniRaoSSP5RCP60Das { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP85Das> SeriesForecastGiniRaoSSP5RCP85Das { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP1RCP26TP> SeriesForecastGDP2011PCPPPOECDSSP1RCP26TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP1RCP45TP> SeriesForecastGDP2011PCPPPOECDSSP1RCP45TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP1RCP60TP> SeriesForecastGDP2011PCPPPOECDSSP1RCP60TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP1RCP85TP> SeriesForecastGDP2011PCPPPOECDSSP1RCP85TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP2RCP26TP> SeriesForecastGDP2011PCPPPOECDSSP2RCP26TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP2RCP45TP> SeriesForecastGDP2011PCPPPOECDSSP2RCP45TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP2RCP60TP> SeriesForecastGDP2011PCPPPOECDSSP2RCP60TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP2RCP85TP> SeriesForecastGDP2011PCPPPOECDSSP2RCP85TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP3RCP26TP> SeriesForecastGDP2011PCPPPOECDSSP3RCP26TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP3RCP45TP> SeriesForecastGDP2011PCPPPOECDSSP3RCP45TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP3RCP60TP> SeriesForecastGDP2011PCPPPOECDSSP3RCP60TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP3RCP85TP> SeriesForecastGDP2011PCPPPOECDSSP3RCP85TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP4RCP26TP> SeriesForecastGDP2011PCPPPOECDSSP4RCP26TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP4RCP45TP> SeriesForecastGDP2011PCPPPOECDSSP4RCP45TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP4RCP60TP> SeriesForecastGDP2011PCPPPOECDSSP4RCP60TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP4RCP85TP> SeriesForecastGDP2011PCPPPOECDSSP4RCP85TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP5RCP26TP> SeriesForecastGDP2011PCPPPOECDSSP5RCP26TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP5RCP45TP> SeriesForecastGDP2011PCPPPOECDSSP5RCP45TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP5RCP60TP> SeriesForecastGDP2011PCPPPOECDSSP5RCP60TP { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP5RCP85TP> SeriesForecastGDP2011PCPPPOECDSSP5RCP85TP { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP26Pag> SeriesForecastGiniRaoSSP1RCP26Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP45Pag> SeriesForecastGiniRaoSSP1RCP45Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP60Pag> SeriesForecastGiniRaoSSP1RCP60Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP85Pag> SeriesForecastGiniRaoSSP1RCP85Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP26Pag> SeriesForecastGiniRaoSSP2RCP26Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP45Pag> SeriesForecastGiniRaoSSP2RCP45Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP60Pag> SeriesForecastGiniRaoSSP2RCP60Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP85Pag> SeriesForecastGiniRaoSSP2RCP85Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP26Pag> SeriesForecastGiniRaoSSP3RCP26Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP45Pag> SeriesForecastGiniRaoSSP3RCP45Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP60Pag> SeriesForecastGiniRaoSSP3RCP60Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP85Pag> SeriesForecastGiniRaoSSP3RCP85Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP26Pag> SeriesForecastGiniRaoSSP4RCP26Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP45Pag> SeriesForecastGiniRaoSSP4RCP45Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP60Pag> SeriesForecastGiniRaoSSP4RCP60Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP85Pag> SeriesForecastGiniRaoSSP4RCP85Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP26Pag> SeriesForecastGiniRaoSSP5RCP26Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP45Pag> SeriesForecastGiniRaoSSP5RCP45Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP60Pag> SeriesForecastGiniRaoSSP5RCP60Pag { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP85Pag> SeriesForecastGiniRaoSSP5RCP85Pag { get; set; }
        protected virtual DbSet<SeriesPovGap2D15cperDay> SeriesPovGap2D15cperDay { get; set; }
        protected virtual DbSet<SeriesPovGap3D65cperDay> SeriesPovGap3D65cperDay { get; set; }
        protected virtual DbSet<SeriesPovGap6D85cperDay> SeriesPovGap6D85cperDay { get; set; }
        //jrs 2025/09/16
        protected virtual DbSet<SeriesPovGap3D00cperDayPIP> SeriesPovGap3D00cperDayPIP { get; set; }
        protected virtual DbSet<SeriesPovGap4D20cperDayPIP> SeriesPovGap4D20cperDayPIP { get; set; }
        protected virtual DbSet<SeriesPovGap8D30cperDayPIP> SeriesPovGap8D30cperDayPIP { get; set; }

        protected virtual DbSet<SeriesIncBelow2D15c> SeriesIncBelow2D15c { get; set; }
        protected virtual DbSet<SeriesIncBelow3D65c> SeriesIncBelow3D65c { get; set; }
        protected virtual DbSet<SeriesIncBelow6D85c> SeriesIncBelow6D85c { get; set; }
//jrs 2024/02/26
        protected virtual DbSet<SeriesIncBelow2D15cPIP> SeriesIncBelow2D15cPIP { get; set; }
        protected virtual DbSet<SeriesIncBelow3D65cPIP> SeriesIncBelow3D65cPIP { get; set; }
        protected virtual DbSet<SeriesIncBelow6D85cPIP> SeriesIncBelow6D85cPIP { get; set; }

        //jrs 2025/09/12
        protected virtual DbSet<SeriesIncBelow3D00cPIP> SeriesIncBelow3D00cPIP { get; set; }
        protected virtual DbSet<SeriesIncBelow4D20cPIP> SeriesIncBelow4D20cPIP { get; set; }
        protected virtual DbSet<SeriesIncBelow8D30cPIP> SeriesIncBelow8D30cPIP { get; set; }

        protected virtual DbSet<SeriesForecastEnvTempChgFLAT> SeriesForecastEnvTempChgFLAT { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP5RCP85> SeriesForecastGDP2011PCPPPOECDSSP5RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP85> SeriesForecastGiniRaoSSP5RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP60> SeriesForecastGiniRaoSSP5RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP5RCP60> SeriesForecastGDP2011PCPPPOECDSSP5RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP45> SeriesForecastGiniRaoSSP5RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP5RCP45> SeriesForecastGDP2011PCPPPOECDSSP5RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP5RCP26> SeriesForecastGDP2011PCPPPOECDSSP5RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5RCP26> SeriesForecastGiniRaoSSP5RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP4RCP85> SeriesForecastGDP2011PCPPPOECDSSP4RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP85> SeriesForecastGiniRaoSSP4RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP60> SeriesForecastGiniRaoSSP4RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP4RCP60> SeriesForecastGDP2011PCPPPOECDSSP4RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP45> SeriesForecastGiniRaoSSP4RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP4RCP45> SeriesForecastGDP2011PCPPPOECDSSP4RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP4RCP26> SeriesForecastGDP2011PCPPPOECDSSP4RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4RCP26> SeriesForecastGiniRaoSSP4RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP85> SeriesForecastGiniRaoSSP3RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP3RCP85> SeriesForecastGDP2011PCPPPOECDSSP3RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP60> SeriesForecastGiniRaoSSP3RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP3RCP60> SeriesForecastGDP2011PCPPPOECDSSP3RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP45> SeriesForecastGiniRaoSSP3RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP3RCP45> SeriesForecastGDP2011PCPPPOECDSSP3RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3RCP26> SeriesForecastGiniRaoSSP3RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP3RCP26> SeriesForecastGDP2011PCPPPOECDSSP3RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP2RCP85> SeriesForecastGDP2011PCPPPOECDSSP2RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP85> SeriesForecastGiniRaoSSP2RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP2RCP60> SeriesForecastGDP2011PCPPPOECDSSP2RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP60> SeriesForecastGiniRaoSSP2RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP45> SeriesForecastGiniRaoSSP2RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP2RCP45> SeriesForecastGDP2011PCPPPOECDSSP2RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2RCP26> SeriesForecastGiniRaoSSP2RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP2RCP26> SeriesForecastGDP2011PCPPPOECDSSP2RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP85> SeriesForecastGiniRaoSSP1RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP1RCP85> SeriesForecastGDP2011PCPPPOECDSSP1RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP60> SeriesForecastGiniRaoSSP1RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP1RCP60> SeriesForecastGDP2011PCPPPOECDSSP1RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP45> SeriesForecastGiniRaoSSP1RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP1RCP45> SeriesForecastGDP2011PCPPPOECDSSP1RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastGDP2011PCPPPOECDSSP1RCP26> SeriesForecastGDP2011PCPPPOECDSSP1RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1RCP26> SeriesForecastGiniRaoSSP1RCP26 { get; set; }
        protected virtual DbSet<SeriesAIDRecGNIOECD> SeriesAIDRecGNIOECD { get; set; }
        protected virtual DbSet<SeriesXIncSecPaymentsGDP> SeriesXIncSecPaymentsGDP { get; set; }
        protected virtual DbSet<SeriesXIncSecReceiptsGDP> SeriesXIncSecReceiptsGDP { get; set; }
        protected virtual DbSet<SeriesMilExpendStock> SeriesMilExpendStock { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1> SeriesForecastGiniRaoSSP1 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1ConflictAdj> SeriesForecastGiniRaoSSP1ConflictAdj { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1ConflictAdjHi> SeriesForecastGiniRaoSSP1ConflictAdjHi { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP1ConflictAdjLo> SeriesForecastGiniRaoSSP1ConflictAdjLo { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2> SeriesForecastGiniRaoSSP2 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2ConflictAdj> SeriesForecastGiniRaoSSP2ConflictAdj { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2ConflictAdjHi> SeriesForecastGiniRaoSSP2ConflictAdjHi { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP2ConflictAdjLo> SeriesForecastGiniRaoSSP2ConflictAdjLo { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3> SeriesForecastGiniRaoSSP3 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3ConflictAdj> SeriesForecastGiniRaoSSP3ConflictAdj { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3ConflictAdjHi> SeriesForecastGiniRaoSSP3ConflictAdjHi { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP3ConflictAdjLo> SeriesForecastGiniRaoSSP3ConflictAdjLo { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4> SeriesForecastGiniRaoSSP4 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4ConflictAdj> SeriesForecastGiniRaoSSP4ConflictAdj { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4ConflictAdjHi> SeriesForecastGiniRaoSSP4ConflictAdjHi { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP4ConflictAdjLo> SeriesForecastGiniRaoSSP4ConflictAdjLo { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5> SeriesForecastGiniRaoSSP5 { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5ConflictAdj> SeriesForecastGiniRaoSSP5ConflictAdj { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5ConflictAdjHi> SeriesForecastGiniRaoSSP5ConflictAdjHi { get; set; }
        protected virtual DbSet<SeriesForecastGiniRaoSSP5ConflictAdjLo> SeriesForecastGiniRaoSSP5ConflictAdjLo { get; set; }
        protected virtual DbSet<SeriesArmsExportsGDP> SeriesArmsExportsGDP { get; set; }
        protected virtual DbSet<SeriesArmsImportsGDP> SeriesArmsImportsGDP { get; set; }
        protected virtual DbSet<SeriesPovLineNationalWB> SeriesPovLineNationalWB { get; set; }
        protected virtual DbSet<SeriesPovLineNationalWB2017> SeriesPovLineNationalWB2017 { get; set; }
        protected virtual DbSet<SeriesPovLineNationalWB2021> SeriesPovLineNationalWB2021 { get; set; }

        protected virtual DbSet<SeriesLaborAgriSector> SeriesLaborAgriSector { get; set; }

        protected virtual DbSet<SeriesPovGap1D90cperDayPovcal> SeriesPovGap1D90cperDayPovcal { get; set; }
        protected virtual DbSet<SeriesPovGap3D20perDayPovcal> SeriesPovGap3D20perDayPovcal { get; set; }
        protected virtual DbSet<SeriesPovGap5D50cperDayPovcal> SeriesPovGap5D50cperDayPovcal { get; set; }

        protected virtual DbSet<SeriesGovtDebtGDPIMF> SeriesGovtDebtGDPIMF { get; set; }
        protected virtual DbSet<SeriesGovtDebtGenGDPIMF> SeriesGovtDebtGenGDPIMF { get; set; }

        protected virtual DbSet<SeriesAidRecGrantTotRev> SeriesAidRecGrantTotRev { get; set; }
        
        protected virtual DbSet<SeriesExportGoodSer> ExportGoodSer { get; set; }
        protected virtual DbSet<SeriesImportGoodSer> ImportGoodSer { get; set; }
        protected virtual DbSet<SeriesAIDRecGNI> AIDRecGNI { get; set; }
        protected virtual DbSet<SeriesXCurActBalGDP> XCurActBalGDP { get; set; }
        protected virtual DbSet<SeriesVaddAg> VaddAg { get; set; }
        protected virtual DbSet<SeriesVaddMan> VaddMan { get; set; }
        protected virtual DbSet<SeriesVaddInd> VaddInd { get; set; }
        protected virtual DbSet<SeriesVaddSer> VaddSer { get; set; }
        protected virtual DbSet<SeriesVaddICTGDP> VaddICTGDP { get; set; }

        protected virtual DbSet<SeriesIncBelow1D25cWDI2011> SeriesIncBelow1D25cWDI2011 { get; set; }

        protected virtual DbSet<SeriesXWorkerRemitPaid> SeriesXWorkerRemitPaid { get; set; }
        protected virtual DbSet<SeriesXWorkerRemitPaidGDP> SeriesXWorkerRemitPaidGDP { get; set; }
        protected virtual DbSet<SeriesXWorkerRemitReceived> SeriesXWorkerRemitReceived { get; set; }
        protected virtual DbSet<SeriesXWorkerRemitReceivedGDP> SeriesXWorkerRemitReceivedGDP { get; set; }

        protected virtual DbSet<SeriesExportsMerchandise> SeriesExportsMerchandise { get; set; }
        protected virtual DbSet<SeriesExportsMerchandiseGDP> SeriesExportsMerchandiseGDP { get; set; }
        protected virtual DbSet<SeriesImportsMerchandise> SeriesImportsMerchandise { get; set; }
        protected virtual DbSet<SeriesImportsMerchandiseGDP> SeriesImportsMerchandiseGDP { get; set; }
        protected virtual DbSet<SeriesExportServices> SeriesExportServices { get; set; }
        protected virtual DbSet<SeriesExportServicesGDP> SeriesExportServicesGDP { get; set; }
        protected virtual DbSet<SeriesImportServices> SeriesImportServices { get; set; }
        protected virtual DbSet<SeriesImportServicesGDP> SeriesImportServicesGDP { get; set; }
        protected virtual DbSet<SeriesArmsImpTotImp> SeriesArmsImpTotImp { get; set; }

        
        protected virtual DbSet<SeriesICTGoodsExportExpGoods> SeriesICTGoodsExportExpGoods { get; set; }
        protected virtual DbSet<SeriesICTGoodsImportImpGoods> SeriesICTGoodsImportImpGoods { get; set; }

        protected virtual DbSet<SeriesICTSerExportExpSer> SeriesICTSerExportExpSer { get; set; }
        protected virtual DbSet<SeriesICTSerImportImpSer> SeriesICTSerImportImpSer { get; set; }

        protected virtual DbSet<SeriesOresMetsExMerchEx> SeriesOresMetsExMerchEx { get; set; }
        protected virtual DbSet<SeriesOresMetsImMerchIm> SeriesOresMetsImMerchIm { get; set; }

        protected virtual DbSet<SeriesHouseConGDP> SeriesHouseConGDP { get; set; }
        protected virtual DbSet<SeriesGovConGDP> SeriesGovConGDP { get; set; }
        protected virtual DbSet<SeriesInvestGrCapFormGDP> SeriesInvestGrCapFormGDP { get; set; }

        protected virtual DbSet<SeriesGDPInformalElgin> SeriesGDPInformalElgin { get; set; }
        protected virtual DbSet<SeriesGDPInformalBlended> SeriesGDPInformalBlended { get; set; }
        protected virtual DbSet<SeriesLaborInformalInfSecTotalInformal> SeriesLaborInformalInfSecTotalInformal { get; set; }
        protected virtual DbSet<SeriesLaborInformalFemaleInformalSec> SeriesLaborInformalFemaleInformalSec { get; set; }
        protected virtual DbSet<SeriesLaborInformalMaleInformalSec> SeriesLaborInformalMaleInformalSec { get; set; }
        protected virtual DbSet<SeriesLaborInformalHHTotalInformal> SeriesLaborInformalHHTotalInformal { get; set; }
        protected virtual DbSet<SeriesLaborInformalFemaleHHInformal> SeriesLaborInformalFemaleHHInformal { get; set; }
        protected virtual DbSet<SeriesLaborInformalMaleHHInformal> SeriesLaborInformalMaleHHInformal { get; set; }
        protected virtual DbSet<SeriesLaborInformalFormSecTotalInformal> SeriesLaborInformalFormSecTotalInformal { get; set; }
        protected virtual DbSet<SeriesLaborInformalFemaleFormalSec> SeriesLaborInformalFemaleFormalSec { get; set; }
        protected virtual DbSet<SeriesLaborInformalMaleFormalSec> SeriesLaborInformalMaleFormalSec { get; set; }

        protected virtual DbSet<SeriesGovSSWelBenExp> SeriesGovSSWelBenExp { get; set; }
        protected virtual DbSet<SeriesGovtPensionGDP> SeriesGovtPensionGDP { get; set; }
        protected virtual DbSet<SeriesTaxCorpTot> SeriesTaxCorpTot { get; set; }
        protected virtual DbSet<SeriesTaxSocSecCurRev> SeriesTaxSocSecCurRev { get; set; }
        protected virtual DbSet<SeriesTaxGoodSerCurRev> SeriesTaxGoodSerCurRev { get; set; }

        //'Revenue Series---------------------------------
        protected virtual DbSet<SeriesTotRevGDPIMF> SeriesTotRevGDPIMF { get; set; }
        protected virtual DbSet<SeriesGovRevOECDGDP> SeriesGovRevOECDGDP { get; set; }
        protected virtual DbSet<SeriesGovtCalcRevTotGDP> SeriesGovtCalcRevTotGDP { get; set; }
        protected virtual DbSet<SeriesGovtCurRevGDP> SeriesGovtCurRevGDP { get; set; }
        protected virtual DbSet<SeriesGovtCalcRevCenGDP> SeriesGovtCalcRevCenGDP { get; set; }

        protected virtual DbSet<SeriesGovtGenRevGDPFM> SeriesGovtGenRevGDPFM { get; set; }
        
        //'Expenditure Series---------------------------------
        protected virtual DbSet<SeriesGovTotalOutlaysGDP> SeriesGovTotalOutlaysGDP { get; set; }
        protected virtual DbSet<SeriesGovtCalcExpendTotGDP> SeriesGovtCalcExpendTotGDP { get; set; }
        protected virtual DbSet<SeriesGovExpenseGDP> SeriesGovExpenseGDP { get; set; }
        protected virtual DbSet<SeriesGovtCalcExpendCenGDP> SeriesGovtCalcExpendCenGDP { get; set; }

        protected virtual DbSet<SeriesGovtGenExpGDPFM> SeriesGovtGenExpGDPFM { get; set; }
        
        protected virtual DbSet<SeriesXWBLoansGDP> SeriesXWBLoansGDP { get; set; }
        protected virtual DbSet<SeriesXIMFCreditGDP> SeriesXIMFCreditGDP { get; set; }

        protected virtual DbSet<SeriesXDEBTPPGGDP> SeriesXDEBTPPGGDP { get; set; }
        protected virtual DbSet<SeriesXDEBTPNGGDP> SeriesXDEBTPNGGDP { get; set; }

        protected virtual DbSet<SeriesXDEBT> SeriesXDEBT { get; set; }
        protected virtual DbSet<SeriesXDEBTGDP> SeriesXDEBTGDP { get; set; }
        protected virtual DbSet<SeriesAidRecGrantTotal> SeriesAidRecGrantTotal { get; set; }

        protected virtual DbSet<SeriesGovtDebtGDP> SeriesGovtDebtGDP { get; set; }
        protected virtual DbSet<SeriesXFDIInflowsGDP> SeriesXFDIInflowsGDP { get; set; }
        protected virtual DbSet<SeriesXFDIOutflowsGDP> SeriesXFDIOutflowsGDP { get; set; }
        protected virtual DbSet<SeriesXPortBondsGDP> SeriesXPortBondsGDP { get; set; }
        protected virtual DbSet<SeriesXPortEquityGDP> SeriesXPortEquityGDP { get; set; }
        protected virtual DbSet<SeriesXReservesGDP> SeriesXReservesGDP { get; set; }
        protected virtual DbSet<SeriesXFlowsIBRDGDP> SeriesXFlowsIBRDGDP { get; set; }
        protected virtual DbSet<SeriesXFlowsIDAGDP> SeriesXFlowsIDAGDP { get; set; }
        protected virtual DbSet<SeriesXFlowsIMFConGDP> SeriesXFlowsIMFConGDP { get; set; }
        protected virtual DbSet<SeriesXFlowsIMFNonConGDP> SeriesXFlowsIMFNonConGDP { get; set; }
        protected virtual DbSet<SeriesPopForeign> SeriesPopForeign { get; set; }

        protected virtual DbSet<SeriesXIncPaymentsGDP> SeriesXIncPaymentsGDP { get; set; }
        protected virtual DbSet<SeriesXIncReceiptsGDP> SeriesXIncReceiptsGDP { get; set; }

        protected virtual DbSet<SeriesIncShareL20> SeriesIncShareL20 { get; set; }
        protected virtual DbSet<SeriesGINIExtended> SeriesGINIExtended { get; set; }
        protected virtual DbSet<SeriesIncBelow2DollarPovCal2015> SeriesIncBelow2DollarPovCal2015 { get; set; }
        protected virtual DbSet<SeriesIncBelow1D90cWDI> SeriesIncBelow1D90cWDI { get; set; }
        protected virtual DbSet<SeriesIncBelow1D90cPovcal> SeriesIncBelow1D90cPovcal { get; set; }        
        protected virtual DbSet<SeriesIncBelow3D10cWDI> SeriesIncBelow3D10cWDI { get; set; }
        protected virtual DbSet<SeriesPovertyGap1c90perDay> SeriesPovertyGap1c90perDay { get; set; }

        protected virtual DbSet<SeriesIncBelow3D20cPovcal> SeriesIncBelow3D20cPovcal { get; set; }
        protected virtual DbSet<SeriesIncBelow5D50cPovcal> SeriesIncBelow5D50cPovcal { get; set; }
        protected virtual DbSet<SeriesIncBelowNPLWDI> SeriesIncBelowNPLWDI { get; set; }

        protected virtual DbSet<SeriesTaxImportDutiesRev> SeriesTaxImportDutiesRev { get; set; }        
    }

}