Imports IFs.Data
Imports System.Math
Imports System.Threading.Tasks
Imports System.Threading.Tasks.Dataflow

Namespace Models

    Partial Public Module HealthMod

        'jrs 2007/06/26
        Dim Ntypes As Integer

        'jrs 2008/10/03
        Dim AvgAgePerCohort() As Single
        'jrs 2010/03/15
        ' Dim MaxLIFEXP As Single
        ' Dim MaxLifeExpCohort As Integer
        ' Dim MaxLifeExpCohortPortion As Single
        Dim StdLifeExp(,) As Single

        'jrs 2008/10/09
        'Transition factors (low to high income)
        Dim modmordstdet2(,,) As Double
        Dim Factors2(,,) As Double
        Dim NormFactors2(,,) As Double
        Dim TransitionLevel As Single
        Dim mortShiftFactorsDiarr2(,,) As Single
        'jrs 2009/07/02
        Dim TransitionInc(,,) As Single

        'jrs 2008/11/20
        Dim SHLSMOKINGIMP(,,) As Single
        Dim SHLSMOKING(,,) As Single
        'jrs 2009/02/16 Health Expenditure Factor
        'this variable is to save Health Expenditures (% of GDP) in year 1
        Dim GDSHI() As Single
        'jrs 2009/04/03
        'Regression factors
        Dim mortFactorsInf(,,) As Single
        'jrs 2009/09/15
        Dim mortFactorsRespInf(,,) As Single

        'jrs 2009/09/11
        ' 'jrs 2009/04/07
        ' Dim mortDiarrhea()
        ' Dim mortShiftFactorsDiarr()
        'jrs 2009/07/08
        Dim SHLSTUNT() As Single
        'jrs 2009/07/14
        Public HLSTUNTExp() As Single
        Dim HLSTUNTExpShift() As Single

        'jrs 2007/04/12
        Dim Factors(,,) As Double
        Dim mortFactors(,,,) As Double
        Dim modmordstdet(,,) As Double
        'jrs 2007/07/09
        Dim MNfactors() As Double
        'jrs 2015/12/04
        Dim SAMfactors() As Double

        'jrs 2008/08/15
        Dim BMIfactorsM() As Double
        Dim BMIfactorsF() As Double
        'jrs 2009/09/01
        Dim BMIfullfactorsM() As Double
        Dim BMIfullfactorsF() As Double

        'jrs 2007/06/26
        Dim NormFactors(,,) As Double

        'jrs 2019/05/01
        Dim smodmordstdet(,,) As Double
        'kbn 2019/05/12- Start
        'Add shift factor for child stunting
        Dim HlChildstuntingshift() As Double
        'kbn 2019/05/12-End
        Dim SHLSTUNTHeight(), MortShiftI() As Double

        'jrs 2013/12/18 *** begin ***
        'added second dimension to handle lag
        Dim COOKSTRDDiff(,,) As Single
        Dim COOKSTLCDiff(,,) As Single
        Dim COOKSTIHDDiff(,,,) As Single
        Dim COOKSTSTDiff(,,,) As Single
        'jrs 2013/12/18 *** end ***

        'jrs 2014/03/13 *** begin ***
        Dim BaseCostMorb() As Single
        Dim BaseCostMort() As Single
        Dim CostAdjFactorMorb(,) As Single
        Dim CostAdjFactorMort(,) As Single
        'jrs 2014/06/02 Dim LifeExtensionCostPerCap As Single
        'jrs 2014/04/29 Dim ResidualCostPerCap() As Single
        'jrs 2014/05/26 Dim PrivHLExpPctGDPFxName As String
        'jrs 2014/05/26 Dim HLEXPENDPRIVShift() As Single
        Dim BaseCostMorb2() As Single
        Dim BaseCostMort2() As Single
        Dim CostAdjFactorMorb2(,) As Single
        Dim CostAdjFactorMort2(,) As Single
        Dim bType2Countrys() As Boolean
        'jrs 2014/03/13 *** end ***
        'jrs 2014/05/06
        Dim IHLCOSTOTHER() As Single

        Dim modmorbdstdet(,,) As Double

        'jrs 2007/10/12 Traffic Mortality Male to Female ratios
        Dim TMMFratios(,) As Double
        'jrs 2008/08/15
        'BMI and obesity modification
        Dim z As Single, BMI As Single
        Dim BMIOb As Single, BMISTDPrp As Single

        'jrs 2013/12/04
        Dim rrCOOKSTRI As Dictionary(Of String, Single)
        Dim rrCOOKSTRD As Dictionary(Of String, Single)
        Dim rrCOOKSTLC As Dictionary(Of String, Single) 'Lung Cancer
        Dim rrCOOKSTIHD As Dictionary(Of String, Single) 'Ischemic Heart Disease
        Dim rrCOOKSTST As Dictionary(Of String, Single) 'Stroke
        'jrs 2014/01/24 *** begin ***
        'Need to keep constant for Distal Driver formulation (only allow changes to RRs for Full Model)
        Dim rrCOOKSTRIDD As Dictionary(Of String, Single)
        Dim rrCOOKSTRDDD As Dictionary(Of String, Single)
        Dim rrCOOKSTLCDD As Dictionary(Of String, Single) 'Lung Cancer
        Dim rrCOOKSTIHDDD As Dictionary(Of String, Single) 'Ischemic Heart Disease
        Dim rrCOOKSTSTDD As Dictionary(Of String, Single) 'Stroke
        'jrs 2014/01/24 *** end ***

        Dim LungCancerDist(,,), IHDDist(,,), StrokeDist(,,) As Single
        'jrs 2014/01/30
        Dim COPDDist(,,) As Single
        'Dim IHDmults(), StrokeMults()
        Dim IHDmults(,) As Single
        Dim StrokeMults(,) As Single

        Dim mortShiftFactorsDiarr(,,) As Single

        'jrs 2007/05/05
        Dim SmokingFactors(,) As Single
        Dim ObesityFactors(,) As Single

        'jrs 2013/09/20
        Dim VentFctI() As Single, VentFctChI() As Single

        'jrs 2013/12/06 *** begin ***
        'Moved from ifsHealth, because the procedure was too large

        'jrs 2009/10/16
        Dim rrCV(5) As Single
        'jrs 2009/08/20
        'Relative Risks for other communicable diseases,
        'other than Diarrhoeal diseases, Malaria, Measles, Lower respiratory infections
        Dim rrOCD(4) As Single
        '    rrOCD(1) = 1#
        '    rrOCD(2) = 2.06
        '    rrOCD(3) = 4.24
        '    rrOCD(4) = 8.72

        'Relative Risks for Diarrheal diseases
        Dim rrD(4) As Single
        '    rrD(1) = 1#
        '    rrD(2) = 2.32
        '    rrD(3) = 5.39
        '    rrD(4) = 12.5

        'jrs 2009/09/15
        'Relative Risks for Malaria
        Dim rrM(4) As Single
        '    rrM(1) = 1#
        '    rrM(2) = 2.12
        '    rrM(3) = 4.48
        '    rrM(4) = 9.49

        'Relative Risks for Resp. Infections
        Dim rrRI(4) As Single
        '    rrRI(1) = 1#
        '    rrRI(2) = 2.01
        '    rrRI(3) = 4.03
        '    rrRI(4) = 8.09

        'jrs 2021/08/05 start
        Dim HLSTUNTCHLShift(), SHLSTUNTCHLDDF() As Single
        Dim rrD_Stunt(2) As Single
        Dim rrOCD_Stunt(2) As Single
        Dim rrRI_Stunt(2) As Single
        'jrs 2021/08/05 end

        'jrs 2009/10/28
        'Relative Risk for Resp. Diseases and Infections from Solid Fuel use
        Dim rrSFRI(1) As Single, rrSFRD(2) As Single
        'jrs 2009/10/29
        'Relative Risk for Resp. Disease, Infections and CV from Outdoor air polution (PM2.5)
        Dim rrPM2RI(1) As Single 'same for all 3 types

        'jrs 2015/03/18 Dim rrWATSANDia(5) As Single
        Dim rrWATDia(3) As Single
        Dim rrSANDia(1) As Single

        Dim popgdr(,) As Double
        'jrs 2013/12/06 *** end ***

        'jrs 2007/06/08 Smoking Impact
        Dim SmokingImpactFactors(,,) As Single
        Dim HLSMOKINGIMPFEMALEI(,), HLSMOKINGIMPMALEI(,) As Single

        Dim NeoNatMorshift() As Single

        'kbn 2018/03/30
        Dim MatMortshift() As Single
        Dim MatMortTimeTr() As Single 'bbh 2019/01/19

        'bbh 2019/04/06 start
        Dim HIVIncShift() As Single
        Dim HIVMortShift() As Single
        Dim MalIncShift() As Single
        Dim MalMortShift() As Single
        Dim SHlHIVPrev() As Single
        Dim SHlBedNets() As Single
        Dim SHlHIVInc() As Single
        Dim SHlHIVMort() As Single
        Dim SHlMalShift() As Single
        Dim CondomUseShift() As Single
        Dim ARTShift() As Single
        Dim SHLMalIncR() As Single
        Dim SHlMalPrevR() As Single
        Dim SHlMalMortR() As Single
        'bbh 2019/04/06 end

        'jrs 2007/07/18
        Dim WHOLOWGDPMODEL() As Integer
        'jrs 2009/12/03
        Dim WHOLOWGDPMODEL4() As Integer
        'jrs 2009/10/30
        Dim LowMortLimit(,,) As Double

        'jrs 2011/05/17 *** Smoking begin ***
        Dim SMOKINGGRP(,) As Single
        Dim SMOKINGPEAKYR(,) As Single
        Dim SMOKINGEXPREG(,,) As Single
        Dim SMOKINGTIME(,) As Integer
        Dim SMOKINGTRANCOEFF(,,) As Single?
        'jrs 2011/05/17 *** Smoking end ***
        'jrs 2011/08/18
        Dim SmokingCumEff(,) As Single
        'jrs 2011/09/09
        Dim SHLSMOKINGTCS() As Single

        'jrs 2012/01/13
        Dim RTCS() As Single?
        'jrs 2012/10/18
        'jrs 2013/01/10 Dim hllongvtybetaI() As Variant, hllongvtyalphaI() As Variant
        Dim modmordstdetT(,,) As Single
        'jrs 2014/06/20
        Dim modmordstdetTI(,,) As Single
        'jrs 2014/08/04
        '    'jrs 2013/02/05
        '    Dim ageStCD As Integer, ageStNCD As Integer, ageStInj As Integer
        Dim ageSt() As Integer
        'jrs 2011/11/23
        Dim TCSMax, TCSMin As Single
        Dim SmokingMax, SmokingMin As Single
        Dim ObesityMax, ObesityMin As Single
        'jrs 2009/12/11
        Dim HLDRRShift(,,) As Single
        Dim ipopgdr(,) As Single
        Dim iagedst(,,) As Single
        'jrs 2018/04/20
        Dim IntInjTimeFactors(,) As Double
        Dim mordstDet(,,) As Double
        'jrs 2009/05/11
        Dim sird(,,) As Single
        'jrs 2014/08/20
        Dim BetasT(,,), alphasT(,,) As Single
        'jrs 2014/09/24
        Dim alphaFxs(,) As String, alphaShifts(,) As Single
        Dim StdAgeDst() As Single
        Dim smodmorbdstdet(,,) As Single
        Dim YLDtoYLLratio(,,) As Single
        Dim imodmorbdstdet(,,) As Single
        Dim YLDtoPOPratio(,) As Single

        'jrs 2014/05/07
        Dim USidx As Integer
        Dim HLCOSTMORTShift(,) As Single
        Dim HLCOSTMORBShift(,) As Single
        'jrs 2014/05/26
        Public SHLCOST() As Single, SHLCOST2() As Single, SHealthExpToCostGap() As Single
        'jrs 2014/06/05
        Dim hlCostageSt As Integer
        'jrs 2014/06/09
        Dim hlcostleyrperFirstYear As Integer

        Dim dtmp(,), lifexpiTMP() As Double
        Dim slifexpTMP(,), LIFEXPshiftTMP(,) As Single
        Dim mfactorTMP, mortdstmgtmp, mortinfants As Double
        Dim CalNeoNatMor As Single
        Dim ConvTime As Integer
        Dim NEONATMORBase, adjfactor, deathstmp, CumMortTMP, lagcoef As Double
        Dim MatmortTemp, MatMortTimePar, MatMortTimeParAdj As Single
        Dim yearsM, yearsF, Years, modmorbdstdetRt As Single

        'jrs 2020/04/02
        Public sMATMORTRATIO() As Single

        'jrs 2020/08/19 moved out of ifsHealth
        Dim IY2030 As Integer

        'jrs 2021/04/08
        Dim StuntingByAgeShift(,) As Single
        'mti 2021/05/10
        Dim iStuntVal() As Single
        'jrs 2024/06/20
        Dim TFNamePctARTCov As String
        'jrs 2024/12/03
        Public MaleAIDSPortionI() As Single


        'jrs 2007/04/12
        '   Implemented by José R. Solórzano
        '   Analysis and Development by Cecilia Petterson and José Solórzano
        '   Supervised by Barry B. Hughes
        Public Sub ifsHealth1FirstYear(context As IForecastContext)
            '    Dim hlScaleConvDown As Integer
            '    hlScaleConvDown = 0
            'jrs 2007/06/14
            sErrorSub = "IFsHealth"
            'jrs 2009/08/10 Ntypes = 22 'Adding Diabetes and AIDS
            Ntypes = 30 'adding Diarrhea, Malaria, Resp. Infect. and Mental Health
            'jrs 2008/10/09
            TransitionLevel = 15

			'gvdb 2024/04/05
            'unused variable
            'Dim ultmult As Single
            'Dim limit As Single

            '   FIRST YEAR AND CALL ONLY
            '   -------------------------------------------------------------
            ReDim mordstDet(NCohorts, NR%, Ntypes) 'jrs 2007/06/26 now we have data for all types
            'jrs 2007/04/10 Mortality Detail
            ReDim modmordstdet(NCohorts, NR%, Ntypes)
            'Morbidity detail
            'jrs 2009/08/25 ReDim modmorbdstdet(NCohorts, NR%, 6)
            ReDim modmorbdstdet(NCohorts, NR%, Ntypes)
            'ReDim mortdstmg(6)
            ReDim Factors(NCohorts, NR%, Ntypes) 'jrs 2007/06/26 now we have data for all types
            ReDim NormFactors(NCohorts, NR%, Ntypes)
            ReDim mortFactors(NCohorts, 18, 6, 2)
            'jrs 2009/03/30
            ReDim mortFactorsInf(NCohorts, 2, 2) 'Cohorts, gender, Constant or Beta
            'jrs 2009/09/14
            ReDim mortFactorsRespInf(NCohorts, 2, 2)
            'jrs 2007/10/12
            ReDim TMMFratios(NCohorts, NR)
            'jrs 2008/10/03
            ReDim AvgAgePerCohort(NCohorts)
            'jrs 2010/03/15
            ReDim StdLifeExp(NCohorts, 2)
            'jrs 2014/01/30 *** begin ***
            'added 17 age categories and gender
            'jrs 2013/12/11
            ReDim LungCancerDist(NR, 17, 2)
            ReDim IHDDist(NR, 17, 2)
            ReDim StrokeDist(NR, 17, 2)
            ReDim COPDDist(NR, 17, 2)
            ReDim IHDmults(12, 2)
            ReDim StrokeMults(12, 2)
            'jrs 2014/01/30 *** end ***

            'jrs 2009/02/16
            ReDim GDSHI(NR)
            'jrs 2009/04/07
            'jrs 2009/08/11 ReDim mortDiarrhea(NCohorts, NR, 2) 'Cohorts, region, cohort
            ReDim mortShiftFactorsDiarr(NCohorts, NR, 2)
            'gvdb 2024/04/05
            'unused variable
            'Dim RegVal As Single
            ReDim sird(3, 4, 2) '3 Regions, 4 cohorts and 2 genders
            'jrs 2009/06/04
            ReDim YLDtoYLLratio(NR, CInt(Ntypes / 2), 2)
            'jrs 2009/08/10
            ReDim YLDtoPOPratio(NR, 2)
            'jrs 2009/10/02 no longer needed since saved in common
            '    'jrs 2009/08/24
            '    Dim YLDtoYLLratioGrowthPortion() As Single
            '    ReDim YLDtoYLLratioGrowthPortion(Ntypes / 2) As Single

            '    Dim SHLYLL(), SHLYLD() As Single
            '    ReDim SHLYLL(NR, Ntypes / 2, 2), SHLYLD(NR, Ntypes / 2, 2)
            'jrs 2019/05/01 Dim smodmordstdet() As Single
            ReDim smodmordstdet(NCohorts, NR%, Ntypes)
            ReDim smodmorbdstdet(NCohorts, NR%, Ntypes)
            'gvdb 2024/04/05
            'unused variable
            'Dim modmorbdstdetRt As Single
            'jrs 2013/01/17
            ReDim imodmorbdstdet(NCohorts, NR%, Ntypes)

            'jrs 2008/10/09
            'Mortality for transition from low income to high income
            ReDim modmordstdet2(NCohorts, NR%, Ntypes)
            ReDim Factors2(NCohorts, NR%, Ntypes) 'jrs 2007/06/26 now we have data for all types
            ReDim NormFactors2(NCohorts, NR%, Ntypes)
            'gvdb 2024/04/05
            'unused variable
            'Dim chg As Single, Port As Single, chg2 As Single
            ReDim mortShiftFactorsDiarr2(NCohorts, NR, 2)
            'jrs 2009/07/02
            ReDim TransitionInc(NCohorts, NR, Ntypes)
            'gvdb 2024/04/05
            'unused variable
            'Dim Inc As Single
            'jrs 2009/08/03
            'Dim DiffMorts As Single
            'jrs 2009/07/14
            ReDim HLSTUNTExp(NR)
            ReDim HLSTUNTExpShift(NR)
            'bbh 2009/12/21
            ReDim SHLSTUNTHeight(NR), MortShiftI(NR)
            'jrs 2013/12/18 *** begin ***
            ReDim COOKSTRDDiff(NR, 2, 5), COOKSTLCDiff(NR, 2, 5)
            'jrs 2014/01/30 *** begin ***
            'added Age dimension, now dimensions are: Country, Gender, Age, Lag
            ReDim COOKSTIHDDiff(NR, 2, 12, 3), COOKSTSTDiff(NR, 2, 12, 3) 'jrs 2014/02/17 added an extra year for lag
            'jrs 2014/01/30 *** end ***
            'jrs 2013/12/18 *** end ***

            'jrs 2010/03/09 Standard Age Distribution of Population
            'Adds up to 1
            'jrs 2013/12/06
            LoadStAgeDst(StdAgeDst)

            '3rd dim
            '1. Constant
            '2. lnY
            '3. lnHC
            '4. (lnY)^2
            '5. Year
            '6. lnSI

            'jrs 2007/07/09
            ReDim MNfactors(NR%)
            'jrs 2015/12/04
            ReDim SAMfactors(NR%)
            'jrs 2008/08/15
            ReDim BMIfactorsM(NR%)
            ReDim BMIfactorsF(NR%)
            BMIOb = 30
            BMISTDPrp = 0.2
            'jrs 2009/09/01
            ReDim BMIfullfactorsM(NR%)
            ReDim BMIfullfactorsF(NR%)

            'jrs 2007/05/05
            ReDim SmokingFactors(NR%, 2)
            ReDim ObesityFactors(NR%, 2)
            ReDim popgdr(NR%, 2)
            'gvdb 2024/04/05
            'unused variable
            'Dim FormValue As Single
            'Dim EffectAdd As Single
            'jrs 2013/01/18
            ReDim ipopgdr(NR%, 2)
            ReDim iagedst(NCohorts, NR%, 2)

            'jrs 2007/06/08 SMOKINGIMPACT
            ReDim SmokingImpactFactors(NCohorts, NR, 2)
            ReDim HLSMOKINGIMPMALEI(NR, NCohorts), HLSMOKINGIMPFEMALEI(NR, NCohorts)

            'jrs 2008/11/20
            ReDim SHLSMOKINGIMP(NR, 4, 2)
            'gvdb 2024/04/05
            'unused variable
            'Dim gg As Integer

            'jrs 2009/03/24
            'jrs 2009/09/21 Dim HLOBESITYDISTM As Single, HLOBESITYDISTF As Single
            'jrs 2009/09/21
            'gvdb 2024/04/05
            'unused variable
            'Dim HLBMIDISTM As Single, HLBMIDISTF As Single
            'jrs 2009/07/10
            '    Dim HLOBESITYI() As Single
            '    ReDim HLOBESITYI(NR, 2) As Single 'initial values with no multipliers

            'jrs 2009/07/08
            ReDim SHLSTUNT(NR)
            'jrs 2009/10/28
            ReDim ENSOLFUELShift(NR)
            'gvdb 2024/04/05
            'unused variable
            'Dim ENSOLFUELDIST As Single
            'Dim VentFct As Single, PopCh As Single
            'jrs 2021/08/05
            ReDim HLSTUNTCHLShift(NR)
            ReDim SHLSTUNTCHLDDF(NR)

            'jrs 2010/01/28
            'gvdb 2024/04/05
            'unused variable
            'Dim VentFctCh As Single
            'jrs 2013/09/20
            ReDim VentFctI(NR), VentFctChI(NR)

            'kbn 2018/05/17
            ReDim SDEATHMntlHealth(NR)
            'kbn 2018/05/31
            ReDim SDEATHIntIj(NR)

            'mti 2019/02/22 Start
            ReDim NeoNatMorshift(NR%)
            'mti 2019/02/22 End

            'jrs 2013/01/10
            'jrs 2012/10/18
            '    If NCohorts > 21 Then
            '        'variables for scenarios of extreme longevity
            '        ReDim hllongvtybetaI(NR, 3, 3) As Variant, hllongvtyalphaI(NR, 3, 3) As Variant
            '        'Load them from ifs.mdb
            '        LoadInitAlphaBetaForLongevity dbIFs, hllongvtyalphaI, hllongvtybetaI
            '    End If
            'kbn 2018/03/30
            ReDim MatMortshift(NR%)
            ReDim MatMortTimeTr(NR%) 'bbh 2019/01/19

            'bbh 2019/04/06 start
            ReDim HIVIncShift(NR%)
            ReDim HIVMortShift(NR%)
            ReDim MalIncShift(NR%)
            ReDim MalMortShift(NR%)
            ReDim SHlHIVPrev(NR%)
            ReDim SHlBedNets(NR%)
            ReDim SHlHIVInc(NR%)
            ReDim SHlHIVMort(NR%)
            ReDim SHlMalShift(NR%)
            ReDim CondomUseShift(NR%)
            ReDim ARTShift(NR%)
            ReDim SHLMalIncR(NR%)
            ReDim SHlMalPrevR(NR%)
            ReDim SHlMalMortR(NR%)
            'bbh 2019/04/06 end

            'kbn 2019/05/12- Start-
            ReDim HlChildstuntingshift(NR%)
            'kbn 2019/05/12- End-


            'jrs 2010/01/12
            'variables for Discounting and Age Weighting
            'gvdb 2024/04/05
            'unused variable
            'Dim Years As Single
            'jrs 2010/03/15
            'Dim yearsM As Single, yearsF As Single
            Dim AWC As Single, AWBeta As Single
            Dim DiscountRt As Single
            AWBeta = 0.04
            AWC = 0.1658
            DiscountRt = 0.03 '3%

            'dkb 2020/03/31 - Adding save varaibles for GII
            ReDim sMATMORTRATIO(NR%) 'should be Dim?

            'jrs 2007/04/16 changed tables to read Regions instead of countries
            'also added indices
            'jrs 2007/03/08
            'First Year: Mortality Detail
            '-----------------------------
            'jrs 2012/06/15 Set db = OpenDatabase(IFs_MDB2Active)

            'jrs 2014/01/10
            'RR table name for CookStoves
            Dim sRRTblName As String
            sRRTblName = getRRTableName()

            'jrs 2016/03/29 *** added parameter sRRTblName **** BEGIN
            'jrs 2016/01/13
            If samrrsw = 0 Then
                'jrs 2009/10/15
                'Load Relative Risks
                LoadRRs("MALNCHP", "OthCommumDis", rrOCD, sRRTblName)
                LoadRRs("MALNCHP", "Diarrhea", rrD, sRRTblName)
                LoadRRs("MALNCHP", "Malaria", rrM, sRRTblName)
                LoadRRs("MALNCHP", "RespInfec", rrRI, sRRTblName)
            Else
                'Load Relative Risks
                LoadRRs("MALNCHPSAM", "OthCommumDis", rrOCD, sRRTblName)
                LoadRRs("MALNCHPSAM", "Diarrhea", rrD, sRRTblName)
                LoadRRs("MALNCHPSAM", "Malaria", rrM, sRRTblName)
                LoadRRs("MALNCHPSAM", "RespInfec", rrRI, sRRTblName)
            End If

            'jrs 2021/08/05 start
            LoadRRs("HLCHILDSTUNT", "OthCommumDis", rrOCD_Stunt, sRRTblName)
            LoadRRs("HLCHILDSTUNT", "Diarrhea", rrD_Stunt, sRRTblName)
            LoadRRs("HLCHILDSTUNT", "RespInfec", rrRI_Stunt, sRRTblName)
            'jrs 2021/08/05 end

            LoadRRs("HLBMI", "CardioVasc", rrCV, sRRTblName)
            'jrs 2015/03/18 LoadRRs dbIFs, "WATSAN", "Diarrhea", rrWATSANDia
            LoadRRs("SAN", "Diarrhea", rrSANDia, sRRTblName)
            LoadRRs("WATER", "Diarrhea", rrWATDia, sRRTblName)
            'jrs 2009/10/28

            LoadRRs("ENSOLFUEL", "RespInfec", rrSFRI, sRRTblName)
            LoadRRs("ENSOLFUEL", "Respiratory", rrSFRD, sRRTblName)
            'jrs 2009/10/29
            LoadRRs("ENVPM2PT5", "RespInfec", rrPM2RI, sRRTblName)
            'jrs 2016/03/29 *** added parameter sRRTblName **** END
            'jrs 2013/12/09
            rrCOOKSTRI = New Dictionary(Of String, Single)
            rrCOOKSTRD = New Dictionary(Of String, Single)
            rrCOOKSTLC = New Dictionary(Of String, Single)
            rrCOOKSTIHD = New Dictionary(Of String, Single)
            rrCOOKSTST = New Dictionary(Of String, Single)
            LoadMultiDimRRs("COOKSTOVES", "RespInfec", rrCOOKSTRI, sRRTblName)
            LoadMultiDimRRs("COOKSTOVES", "Respiratory", rrCOOKSTRD, sRRTblName)
            LoadMultiDimRRs("COOKSTOVES", "Lung Cancer", rrCOOKSTLC, sRRTblName)
            LoadMultiDimRRs("COOKSTOVES", "IHD", rrCOOKSTIHD, sRRTblName)
            LoadMultiDimRRs("COOKSTOVES", "Stroke", rrCOOKSTST, sRRTblName)

            'jrs 2014/01/24 *** begin ***
            rrCOOKSTRIDD = New Dictionary(Of String, Single)
            rrCOOKSTRDDD = New Dictionary(Of String, Single)
            rrCOOKSTLCDD = New Dictionary(Of String, Single)
            rrCOOKSTIHDDD = New Dictionary(Of String, Single)
            rrCOOKSTSTDD = New Dictionary(Of String, Single)
            LoadMultiDimRRs("COOKSTOVES", "RespInfec", rrCOOKSTRIDD, "HealthProxDrivRRs")
            LoadMultiDimRRs("COOKSTOVES", "Respiratory", rrCOOKSTRDDD, "HealthProxDrivRRs")
            LoadMultiDimRRs("COOKSTOVES", "Lung Cancer", rrCOOKSTLCDD, "HealthProxDrivRRs")
            LoadMultiDimRRs("COOKSTOVES", "IHD", rrCOOKSTIHDDD, "HealthProxDrivRRs")
            LoadMultiDimRRs("COOKSTOVES", "Stroke", rrCOOKSTSTDD, "HealthProxDrivRRs")
            'jrs 2014/01/24 *** end ***

            'jrs 2007/07/18
            'Load WHO Low GDP Model codes
            LoadWHOLIMcodes()

            'Initialize Historic Data (set values to -1)
            '-------------------------------------------
            InitHistData(Ntypes)

            '1: Communicable disease Males
            '2: Communicable disease Females
            '3: Malignant Neoplasms Males
            '4: Malignant Neoplasms Females
            '5: Cardiovascular Diseases Males
            '6: Cardiovascular Diseases Females
            '7. Digestive Diseases Males
            '8. Digestive Diseases Females
            '9. Respiratory Diseases Males
            '10. Respiratory Diseases Females
            '11. Other Non-communicable Diseases Males
            '12. Other Non-communicable Diseases Females
            '13. Road Traffic Accidents Males
            '14. Road Traffic Accidents Females
            '15. Other unintentional injuries Males
            '16. Other unintentional injuries Females
            '17. Intentional Injuries Males
            '18. Intentional Injuries Females
            '19. Diabetes Males
            '20. Diabetes Females
            '21. AIDS Males
            '22. AIDS Females

            '23. Diarrhea Males
            '24. Diarrhea Females
            '25. Malaria Males
            '26. Malaria Females
            '27. Resp. Infect. Males
            '28. Resp. Infect. Females
            '29. Mental Health Males
            '30. Mental Health Females

            'First Year: Units will be in Deaths per 1000 people
            '----------------------------------------------------
            LoadInitData(modmordstdet)

            'jrs 2024/12/03
            ReDim MaleAIDSPortionI(NR)
            ComputeMaleAIDSPortion(MaleAIDSPortionI, context)

            'First Year: Load Data for Aids
            '----------------------------------------------------
            HL_LoadAIDSData(context)

            'jrs 2009/08/10
            'Load Other initialization data
            '--------------------------------
            'jrs 2010/02/17 LoadOthrInitData db, modmordsdet

            'First Year: Morbidity Detail
            '-----------------------------
            LoadMorbidityDetail(modmorbdstdet)

            'jrs 2009/06/04
            'First Year: Read Ratio YLD/YLL
            '-------------------------------
            LoadYLDtoYLLratio(YLDtoYLLratio)

            'jrs 2009/08/10
            'First Year: Read Ratio YLD/POP
            '------------------------------
            LoadYLDtoPOPratio(YLDtoPOPratio)

            'First Year: Load Factors Table
            '------------------------------
            LoadMortFactors(mortFactors)

            'jrs 2009/09/15
            'First Year: Load Other Mortality Factors
            '------------------------------
            LoadOthrMortFactors(sird)

            'jrs 2009/10/26
            'First Year: Load 5yr distribution for proxy
            '-------------------------------------------
            Load5yrDistributions(LowMortLimit, context)

            'jrs 2010/02/01
            'First Year: Adjust GBD big group distribution into 5 year distribution
            '----------------------------------------------------------------------
            'jrs 2013/04/01 This logic was not being applied because of an error (T < 7 and T > 9 is never true)
            'When fixed the error the distribution for Mali got worse
            'Where all the CDs for both genders started contracting at 90-94, and kept contracting afterwards
            'Without it, the contratction only happens in the 85-89 for females in Other CDs
            'We're going to change the data to fix Mali's problems, but other countries have similar issues like Congo DR
            'AdjustInitMort LowMortLimit, modmordstdet
            AdjustJcurveShape(modmordstdet, LowMortLimit, Ntypes, 1, agedst) 'males
            AdjustJcurveShape(modmordstdet, LowMortLimit, Ntypes, 2, agedst) 'females

            'First Year:  Save Original Data before computing first year
            '-----------------------------------------------------------
            'ttt 2019/10/08 Change
            Array.Copy(modmordstdet, mordstDet, modmordstdet.Length)
            'For R% = 1 To NR%
            '    For j = 0 To NCohorts
            '        For k = 1 To Ntypes 'jrs 2007/06/26 now we have data per type
            '            mordstDet(j, R%, k) = modmordstdet(j, R%, k)
            '        Next k
            '    Next j
            'Next R%

            'jrs 2007/05/05
            'First Year: Smoking Additive Shift Factors
            '----------------------------------------------------------
            LoadSmkingShiftFctrs(SmokingFactors, context)
            'jrs 2008/12/03
            'First Year: Load 10 years of Smoking History
            '--------------------------------------------
            ReDim SHLSMOKING(NR, 2, 12) 'Region, Sex, Year
            '11th year is to store regression value
            LoadSmokingHist(context)
            'jrs 2011/05/17 *** Smoking begin ***
            ReDim SMOKINGGRP(NR, 2)
            ReDim SMOKINGPEAKYR(NR, 2)
            ReDim SMOKINGEXPREG(NR, 4, 2) '4 coeficients, 2 for exponential, 2 for logarithmic, for male and female
            ReDim SMOKINGTIME(NR, 2)
            ReDim SMOKINGTRANCOEFF(NR, 4, 2) 'coefficients for transitions
            '10 is the position where current value is
            FindSmokingGroup(10, context)
            'jrs 2011/05/17 Smoking end ***

            'jrs 2009/09/01
            'First Year only: Multiplicative Shift Factor for full model BMI
            '----------------------------------------------------------------
            LoadBMIShiftFctrs(BMIfullfactorsM, BMIfullfactorsF, context)

            'First Year: Obesity Additive Shift Factors
            '----------------------------------------------------------
            LoadObShiftFctrs(ObesityFactors, context)

            'Call FindVarNumb(SmokingIdx, "HLSMOKING")
            'Call FindVarNumb(ObesityIdx, "HLOBESITY")
            'jrs 2011/11/23
            'Call FindVarNumb(TCSIdx, "hlsmokingtcs")
            TCSMax = CSng(context.GetVariable("hlsmokingtcs").Max)
            TCSMin = CSng(context.GetVariable("hlsmokingtcs").Min)
            SmokingMax = CSng(context.GetVariable("HLSMOKING").Max)
            SmokingMin = CSng(context.GetVariable("HLSMOKING").Min)
            ObesityMax = CSng(context.GetVariable("HLOBESITY").Max)
            ObesityMin = CSng(context.GetVariable("HLOBESITY").Min)

            'JRS 2007/06/29
            'First Year: Set UP Smoking Impact Initial Values
            '-------------------------------------------------
            SmokingImpactInit()

            'kbn 2018/03/30 START- FirstYear: Compute Shift factors for Maternal Mortality
            '-----------------------------------------------------------------------
            MaternalMortalityshift(context)
            'kbn 2018/03/30 End

            'bbh 2019/04/06 HIV and Malaria Incidence and Mortality Shift Factors
            'First Year:  Call HIV and Malaria Incidence and Mortality Shift Factor routines
            '-------------------------------------------------------------------------------
            HIVPrevIncShift(context)
            HIVMortRPrevShift(context)
            HIVCondomUseShift(context)
            HIVARTShift(context)
            'ttt 2019/10/08 TakeOut
            Array.Copy(HLHIVPREV, SHlHIVPrev, NR + 1)
            Array.Copy(HLHIVINC, SHlHIVInc, NR + 1)
            Array.Copy(HLHIVMORT, SHlHIVMort, NR + 1)
            Array.Copy(HLBEDNETS, SHlBedNets, NR + 1)

            For R% = 1 To NR
                'ttt 2019/10/08 taken out
                'SHlHIVPrev(R%) = HLHIVPREV(R%)
                'SHlHIVInc(R%) = HLHIVINC(R%)
                'SHlHIVMort(R%) = HLHIVMORT(R%)
                'SHlBedNets(R%) = HLBEDNETS(R%)
                SHLMalIncR(R%) = AMAX(0.000001, HLMALARIAINC(R%) / POP(R%))
                SHlMalPrevR(R%) = HLMALARIAPREV(R%) / POP(R%)
                SHlMalMortR(R%) = HLMALARIAMORT(R%) / POP(R%)

            Next R%
            'bbh 2010/04/06 end

            'jrs 2013/09/20
            LoadAvgAgeAndStdLifeExp()

            'jrs 2013/12/11
            LoadMortSubTypeDistributions(context)

            'mti 2019/02/22 Start
            'FirstYear: Compute Shift factors for Neonatal Mortality
            '-----------------------------------------------------------------------
            Dim CalNeoNatMor As Single
            For R% = 1 To NR
                'mti 2019/03/06 start
                If context.HistoricMode = True Then
                    Call XYTABL("Infant Mortality Rate Versus Neonatal Mortality Rate (1960) Linear",
                    INFMOR(R%), CalNeoNatMor, context)  'Mortality rates are in deaths per 1000 live births; The function has no intercept
                Else
                    Call XYTABL("Infant Mortality Rate Versus Neonatal Mortality Rate (MOSTRECENT) Linear",
                    INFMOR(R%), CalNeoNatMor, context)  'Mortality rates are in deaths per 1000 live births; The function has no an intercept
                End If
                'mti 2019/03/06 End
                NeoNatMorshift(R%) = NEONATMOR(R%) - CalNeoNatMor
            Next R%
            'mti 2019/02/22 End

            ifsHealthAllYears(context)
        End Sub

        Public Sub ifsHealthAllYears(context As IForecastContext)
            Dim z As Integer
            'jrs 2007/05/05
            If IY% > 1 Then
                'jrs 2008/12/04
                'Smoking Compound Growth rate
                'gvdb 2024/04/05
                'unused variable
                'Dim SmkCmpGr As Single
                'Smoking Growth
                'Dim SmkGr As Single

                'All Years: Not in First Year, Apply Smoking Factors
                '---------------------------------------------------
                If hlsmokingstsw = 0 Then
                    Health_Forecast_Smoking(context)
                Else
                    Health_Forecast_Smoking_InStages(context)
                End If

                'jrs 2009/09/01
                'All Years but first, Apply BMI factors
                '--------------------------------------
                Health_Forecast_BMI(context)
                'All Years: Not in First Year, Apply Obesity Factors
                '---------------------------------------------------
                Health_Forecast_Obesity(context)
            End If

            'All Years: Apply multipliers to Obesity and Smoking
            '---------------------------------------------------
            Health_Forecast_Multipliers() 'jrs 2012/01/10 added TCSIdx

            'All Years: Check Mins and Maxs for Obesity and Smoking
            '------------------------------------------------------
            CheckSmokObMinMax()

            'All Years: Forecast Diabetes RRs
            '-----------------------------------
            Health_Forecast_DRR()

            'All Years: Compute Weighted Average for Totals
            '----------------------------------------------
            Dim G As Integer

            ComputeSmokObBMIWeightAvgTotals(popgdr, agedst)
            'jrs 2013/01/18
            If IY = 1 Then
                'jrs 2020/04/17 had to change datatypes so can't do array.copy
                'ttt 2019/10/08 TakeOut
                'Array.Copy(popgdr, ipopgdr, popgdr.Length)
                'Array.Copy(agedst, iagedst, agedst.Length)
                For R = 1 To NR
                    For G = 1 To 2
                        ipopgdr(R, G) = CSng(popgdr(R, G))
                        For j = 0 To NCohorts
                            iagedst(j, R, G) = CSng(agedst(j, R, G))
                        Next j
                    Next G
                Next R
            End If

            'jrs 2008/11/20
            'All Years but First - Estimate Smoking Impact
            '---------------------------------------------
            If Not context.HistoricMode And IY% > 1 Then
                EstimateSmokingImpact(IY% + BaseYear - 1, context)
            End If

            'jrs 2007/06/27
            'All Years: Smoking Impact - Compute Averages for All Ages and Accross Gender
            '----------------------------------------------------------------------------
            ComputeSmokAvgAllAgesSex()

            'All Years: Diabetes Relative Risk - Compute Averages for All Ages and Accross Gender
            '------------------------------------------------------------------------------------
            ComputeAvgDiabRRAgeSex(popgdr)

            'jrs 2018/04/19
            'All Years: Converge Intentional Injury time coeficient to avoid going too high over long horizons
            '-------------------------------------------------------------------------------------------------
            'jrs 2018/05/25
            'jrs 2020/08/19 Dim IY2030 As Integer move out, I don't know why it wasn't resetting to 0 every year
            Dim k As Integer

            If IY% = 1 Then
                ReDim IntInjTimeFactors(12, 2)
                'for both genders
                For k = 17 To 18
                    'ages 5 to 59
                    For j = 2 To 12
                        IntInjTimeFactors(j, k Mod 2) = mortFactors(j, k, 5, 2)
                    Next j
                Next k
                'jrs 2018/05/25
                IY2030 = (2030 - BaseYear + 1)
            End If
            If BaseYear + IY% - 1 > 2030 And context.FirstYearForecast < 2031 Then
                'for both genders
                For k = 17 To 18
                    'ages 5 to 59
                    For j = 2 To 12
                        If mortFactors(j, k, 5, 2) > 0 Then
                            'mortFactors(j, k, 5, 2) = ConvergeOverTime(IntInjTimeFactors(j, k Mod 2), IntInjTimeFactors(j, k Mod 2) * 0.65, 2050 - FirstYearForecast)
                            'jrs 2018/05/25 Convergence was producing a W in deaths for Int Inj, increasing from start to 2030, then droping to 2050, and then increasing again until the end
                            'mortFactors(j, k, 5, 2) = ConvergeOverTimeStartX(IntInjTimeFactors(j, k Mod 2), IntInjTimeFactors(j, k Mod 2) * 0.65, 2050 - FirstYearForecast, 2031 - FirstYearForecast + 1)
                            'Lets just try to reduce the speed of this linear factor
                            mortFactors(j, k, 5, 2) = ((IY2030 + context.FirstYearForecast - 1900 - 1) * IntInjTimeFactors(j, k Mod 2) + IntInjTimeFactors(j, k Mod 2) / 10 * (IY - IY2030)) / (IY + context.FirstYearForecast - 1900 - 1)
                        End If
                    Next j
                Next k
            End If

            Dim zy As Integer
            zy = IY% - 1

            '*****************************************************************************************
            'All Years: Compute formula
            '--------------------------
            'ln M = C + B1 * ln Y + B2 * ln HC + B3 * (ln Y)^2 + B4 * T + B5 * ln SI
            'M is mortality level, in deaths per 100,000 people
            'The Health Module uses a regression equation similar to the one used in
            'the original Global Burden of Disease (GBD) study, but considering
            'the work by Colin Mathers and Dejan Loncar in creating updated regressions
            'using substantially more data.
            '*****************************************************************************************

            'jrs 2007/06/07
            Dim vGDPPCP, vEDYRSAG25, vSI As Single
            Dim vIY As Single 'jrs 2007/07/20 change from integer to single, for the slow down effect
            Dim bUnintInj As Boolean, bIntInj As Boolean
            Dim DiabCoh As Integer, SICoh As Integer
            'jrs 2007/07/20
            Dim hcbeta As Single
            Dim MaleAIDSPortion, maleaidsdths, femaleaidsdths, hltechbaseadj, limit As Single
            Dim aidsdthscoh As Double
            Dim kk As Integer

            For R% = 1 To NR%
                'If GRPS(1, R%) = "United States of America" Then Stop
                If IY > 1 Then
                    Call XYTABL("GDP/Capita (PPP) Versus HIV Infections, Male Portion",
                        GDPPCP(R%), MaleAIDSPortion, context)
                    'jrs 2024/12/03 added * MaleAIDSPortionI(R%)
                    maleaidsdths = AIDSDTHS(R%) * MaleAIDSPortion * MaleAIDSPortionI(R%)
                    femaleaidsdths = AIDSDTHS(R%) * (1 - MaleAIDSPortion * MaleAIDSPortionI(R%))
                End If

                'bbh 2009/12/20
                ' Adjust hltechbase with scenario factor
                hltechbaseadj = hltechbase + Amin(40, GDPPCIP(R%) * hltechshift) / 40

                For j = 0 To NCohorts
                    'bbh 2009/12/21 start
                    'jrs 2012/09/13
                    If j > 21 Then Exit For
                    'jrs 2009/12/09
                    If j = 0 Then
                        limit = 400
                    ElseIf j = 1 Then
                        limit = 200
                    Else
                        limit = 750
                    End If

                    'SI Cohort
                    If j > 6 Then
                        Select Case j
                    '30 to 44
                            Case 7, 8, 9
                                SICoh = 1
                    '45 to 59
                            Case 10, 11, 12
                                SICoh = 2
                    '60 to 69
                            Case 13, 14
                                SICoh = 3
                                '70 +
                            Case Else
                                SICoh = 4
                        End Select
                    Else
                        SICoh = 0
                    End If
                    For k = 1 To Ntypes ' types of mortality (and gender)
                        'jrs 2007/06/07
                        'All Years: Exceptions that make the mortality rate constant
                        '-----------------------------------------------------------
                        'Unintentional Injuries for Males 70+, Females 60+
                        bUnintInj = (k = 15 And j > 14) Or (k = 16 And j > 12)
                        'Intentional Injuries for Males 0-4, 60+, Females 0-4, 45+
                        bIntInj = (k = 17 And (j < 2 Or j > 12)) Or (k = 18 And (j < 2 Or j > 9))
                        'Set Drivers
                        If bUnintInj Or bIntInj Then
                            vGDPPCP = GDPPCIP(R)
                            vEDYRSAG25 = EDYRSAG25I(R, 3) 'jrs 2008/05/15 remove gender specific educ. EDYRSAG25I(R, ((k + 1) Mod 2) + 1)
                            vIY = 1
                            If k Mod 2 = 0 Then
                                vSI = HLSMOKINGIMPFEMALEI(R, j)
                            Else
                                vSI = HLSMOKINGIMPMALEI(R, j)
                            End If
                        Else
                            vGDPPCP = GDPPCP(R)
                            vEDYRSAG25 = EDYRSAG25(R, 3) 'jrs 2008/05/15 remove gender specific educ. EDYRSAG25(R, ((k + 1) Mod 2) + 1)
                            'jrs 2014/06/03
                            vEDYRSAG25 = CSng(AMAX(vEDYRSAG25, 0.01))
                            vIY = IY
                            If SICoh > 0 Then
                                If k Mod 2 = 0 Then
                                    vSI = HLSMOKINGIMP(R, SICoh, 2)
                                Else
                                    vSI = HLSMOKINGIMP(R, SICoh, 1)
                                End If
                            Else
                                vSI = 1
                            End If
                        End If
                        'jrs 2007/10/11 added Traffic Accidents logic k=13,14
                        'jrs 2007/07/20
                        'jrs 2009/01/13 If k < 19 And k <> 13 And k <> 14 Then
                        'Adding Traffic Accidents Switch
                        If k < 19 And ((k <> 13 And k <> 14) Or hlvehsw = 0) Then
                            If GDPPCIP(R%) < 3 Then
                                hcbeta = CSng(mortFactors(j, k, 3, 1))
                            Else
                                hcbeta = CSng(mortFactors(j, k, 3, 2))
                            End If
                            'All Years:
                            'jrs 2007/08/21 Mortality Modification Switch
                            If hlmortmodsw > 1 Then 'jrs 2009/04/20
                                'Low-Income Model - Mortality Modification after 2002 revision from the GBD
                                '--------------------------------------------------------------------------
                                'jrs 2007/10/04
                                '                        If WHOLOWGDPMODEL(R%) = 1 And mortFactors(j, k, 3, 2) < 0 Then
                                '                            hcbeta = 0.5 * mortFactors(j, k, 3, 2)
                                '                        End If
                                If WHOLOWGDPMODEL(R%) = 1 And hcbeta < 0 Then
                                    hcbeta = CSng(0.5 * hcbeta)
                                End If
                                'jrs 2008/05/28
                                If (WHOLOWGDPMODEL(R%) = 2 Or (WHOLOWGDPMODEL(R%) = -1 And GDPPCIP(R%) < 3)) And hcbeta < 0 Then
                                    hcbeta = CSng(0.75 * hcbeta)
                                End If


                                If WHOLOWGDPMODEL(R%) = 1 Then
                                    'jrs 2009/01/13 vIY = 1
                                    vIY = (vIY - 1) * hltechssa + 1
                                    '-1 is for countries with no code
                                ElseIf WHOLOWGDPMODEL(R%) = 2 Or (WHOLOWGDPMODEL(R%) = -1 And GDPPCIP(R%) < 3) Then
                                    'jrs 2009/01/13 vIY = (vIY - 1) * 0.25 + 1
                                    vIY = (vIY - 1) * hltechlinc + 1
                                Else 'jrs 2009/01/13
                                    vIY = (vIY - 1) * hltechbaseadj + 1 'bbh 1009/12/20
                                End If
                            ElseIf hlmortmodsw > 0 Then 'jrs 2009/04/20
                                'jrs 2009/11/30
                                'Intentional Injuries (17 and 18) are going crazy for high tech scenarios
                                'put a top to the multiplier
                                If k = 17 Or k = 18 Then
                                    'jrs 2009/12/03
                                    'Adding modification for children < 5 in Low Income countries
                                    '--------------------------------------------------------
                                    If j < 2 And WHOLOWGDPMODEL4(R%) > -1 Then
                                        If WHOLOWGDPMODEL4(R%) = 0 Then
                                            'High Income countries
                                            vIY = CSng((vIY - 1) * Amin(hltechbaseadj, 1.5) + 1) 'bbh 1009/12/20
                                        ElseIf WHOLOWGDPMODEL4(R%) = 5 Then
                                            'Middle East
                                            vIY = CSng((vIY - 1) * Amin(hltechlinc, 1.5) + 1)
                                        Else
                                            'if Africa, Europe, SE Asia or West Pac, then use 0
                                            vIY = CSng((vIY - 1) * Amin(hltechssa, 1.5) + 1)
                                        End If
                                    Else
                                        'Low-Income Model - Mortality Modification after 2004 revision from the GBD
                                        '--------------------------------------------------------------------------
                                        'low income
                                        If GDPPCIP(R%) < 3 Then
                                            'hltechlinc = 0.25
                                            vIY = CSng((vIY - 1) * Amin(hltechlinc, 1.5) + 1)
                                        Else
                                            'hltechbase = 1
                                            vIY = CSng((vIY - 1) * Amin(hltechbaseadj, 1.5) + 1) 'bbh 1009/12/20
                                        End If
                                    End If
                                Else
                                    'Low-Income Model - Mortality Modification after 2004 revision from the GBD
                                    '--------------------------------------------------------------------------
                                    'jrs 2009/12/03
                                    'Adding modification for children < 5 in Low Income countries
                                    '--------------------------------------------------------
                                    'jrs 2010/02/03 not for CV (5 and 6)
                                    If j < 2 And WHOLOWGDPMODEL4(R%) > -1 And (k <> 5 And k <> 6) Then
                                        If WHOLOWGDPMODEL4(R%) = 0 Then
                                            'High Income countries
                                            vIY = (vIY - 1) * hltechbaseadj + 1  'bbh 1009/12/20
                                        ElseIf WHOLOWGDPMODEL4(R%) = 5 Then
                                            'Middle East
                                            vIY = (vIY - 1) * hltechlinc + 1
                                        Else
                                            'if Africa, Europe, SE Asia or West Pac, then use 0
                                            vIY = (vIY - 1) * hltechssa + 1
                                        End If
                                    Else
                                        'jrs 2010/02/03 CV does not need low inc modification
                                        'low income
                                        If GDPPCIP(R%) < 3 And (k <> 5 And k <> 6) Then
                                            'hltechlinc = 0.25
                                            vIY = (vIY - 1) * hltechlinc + 1
                                        Else
                                            'hltechbase = 1
                                            vIY = (vIY - 1) * hltechbaseadj + 1 'bbh 1009/12/20
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        If vSI = 0 Then vSI = 1 '-> cancel the log

                        'jrs 2009/12/04 adding income growth coefficient
                        '(1 + hlincoefgr / 100) ^ (IY - 1)

                        'Adding Diabetes
                        If k < 19 Then
                            'jrs 2007/10/11 added Traffic Accidents logic k=13,14
                            'jrs 2009/01/13 If k <> 13 And k <> 14 Then
                            'Traffic Accidents Switch
                            If (k <> 13 And k <> 14) Or hlvehsw = 0 Then
                                If GDPPCIP(R%) < 3 Then 'low income countries
                                    'jrs 2009/12/04
                                    If mortFactors(j, k, 1, 1) +
                                    (1 + hlincoefgr / 100) ^ (IY - 1) * mortFactors(j, k, 2, 1) * Log(vGDPPCP * 1000) +
                                    hcbeta * Log(vEDYRSAG25) +
                                    (1 + hlincoefgr / 100) ^ (IY - 1) * mortFactors(j, k, 4, 1) * (Log(vGDPPCP * 1000) ^ 2) +
                                    mortFactors(j, k, 5, 1) * (FirstYear + vIY - 1 - 1900) +
                                    mortFactors(j, k, 6, 1) * Log(vSI) > 709 Then
                                        modmordstdet(j, R, k) = limit
                                    Else
                                        modmordstdet(j, R, k) = CSng(Exp(mortFactors(j, k, 1, 1) +
                                        (1 + hlincoefgr / 100) ^ (IY - 1) * mortFactors(j, k, 2, 1) * Log(vGDPPCP * 1000) +
                                        hcbeta * Log(vEDYRSAG25) +
                                        (1 + hlincoefgr / 100) ^ (IY - 1) * mortFactors(j, k, 4, 1) * (Log(vGDPPCP * 1000) ^ 2) +
                                        mortFactors(j, k, 5, 1) * (FirstYear + vIY - 1 - 1900) +
                                        mortFactors(j, k, 6, 1) * Log(vSI)) / 100)
                                    End If

                                    'jrs 2008/10/09
                                    'transition from low income to high income
                                    hcbeta = CSng(mortFactors(j, k, 3, 2))
                                    'jrs 2009/07/13
                                    If bUnintInj Or bIntInj Then
                                        vIY = 1
                                    Else
                                        vIY = IY
                                    End If
                                    If hlmortmodsw > 1 Then
                                        If WHOLOWGDPMODEL(R%) = 1 And hcbeta < 0 Then
                                            hcbeta = CSng(0.5 * hcbeta)
                                        End If
                                        If (WHOLOWGDPMODEL(R%) = 2 Or (WHOLOWGDPMODEL(R%) = -1 And GDPPCIP(R%) < 3)) And hcbeta < 0 Then
                                            hcbeta = CSng(0.75 * hcbeta)
                                        End If
                                        vIY = (vIY - 1) * hltechbaseadj + 1 'bbh 1009/12/20
                                    ElseIf hlmortmodsw > 0 Then
                                        'jrs 2009/11/30
                                        If k = 17 Or k = 18 Then
                                            vIY = CSng((vIY - 1) * Amin(hltechbaseadj, 1.5) + 1) 'bbh 1009/12/20
                                        Else
                                            vIY = (vIY - 1) * hltechbaseadj + 1 'bbh 1009/12/20
                                        End If
                                    End If

                                    'jrs 2009/12/04
                                    If mortFactors(j, k, 1, 2) +
                                    (1 + hlincoefgr / 100) ^ (IY - 1) * mortFactors(j, k, 2, 2) * Log(vGDPPCP * 1000) +
                                    hcbeta * Log(vEDYRSAG25) +
                                    (1 + hlincoefgr / 100) ^ (IY - 1) * mortFactors(j, k, 4, 2) * (Log(vGDPPCP * 1000) ^ 2) +
                                    mortFactors(j, k, 5, 2) * (FirstYear + vIY - 1 - 1900) +
                                    mortFactors(j, k, 6, 2) * Log(vSI) > 709 Then
                                        modmordstdet2(j, R, k) = limit
                                    Else
                                        modmordstdet2(j, R, k) = CSng(Exp(mortFactors(j, k, 1, 2) +
                                        (1 + hlincoefgr / 100) ^ (IY - 1) * mortFactors(j, k, 2, 2) * Log(vGDPPCP * 1000) +
                                        hcbeta * Log(vEDYRSAG25) +
                                        (1 + hlincoefgr / 100) ^ (IY - 1) * mortFactors(j, k, 4, 2) * (Log(vGDPPCP * 1000) ^ 2) +
                                        mortFactors(j, k, 5, 2) * (FirstYear + vIY - 1 - 1900) +
                                        mortFactors(j, k, 6, 2) * Log(vSI)) / 100)
                                    End If
                                Else 'high income countries
                                    'jrs 2009/12/04
                                    If mortFactors(j, k, 1, 2) +
                                    (1 + hlincoefgr / 100) ^ (IY - 1) * mortFactors(j, k, 2, 2) * Log(vGDPPCP * 1000) +
                                    hcbeta * Log(vEDYRSAG25) +
                                    (1 + hlincoefgr / 100) ^ (IY - 1) * mortFactors(j, k, 4, 2) * (Log(vGDPPCP * 1000) ^ 2) +
                                    mortFactors(j, k, 5, 2) * (FirstYear + vIY - 1 - 1900) +
                                    mortFactors(j, k, 6, 2) * Log(vSI) > 709 Then
                                        modmordstdet(j, R, k) = limit
                                    Else
                                        modmordstdet(j, R, k) = CSng(Exp(mortFactors(j, k, 1, 2) +
                                        (1 + hlincoefgr / 100) ^ (IY - 1) * mortFactors(j, k, 2, 2) * Log(vGDPPCP * 1000) +
                                        hcbeta * Log(vEDYRSAG25) +
                                        (1 + hlincoefgr / 100) ^ (IY - 1) * mortFactors(j, k, 4, 2) * (Log(vGDPPCP * 1000) ^ 2) +
                                        mortFactors(j, k, 5, 2) * (FirstYear + vIY - 1 - 1900) +
                                        mortFactors(j, k, 6, 2) * Log(vSI)) / 100)
                                    End If
                                End If
                            Else 'Traffic Accidents
                                'deaths per 1000 vehi x veh p/ 1000 people = deaths / million peop
                                modmordstdet(j, R, k) = DEATHTRPV(R) * VEHICFLPC(R) / 1000
                            End If
                        ElseIf k < 21 Then 'For Diabetes, 19 Diabetes Males, 11 Other Group 2 Males
                            If j > 3 Then
                                Select Case j
                            '15 to 29
                                    Case 4, 5, 6
                                        DiabCoh = 1
                            '30 to 44
                                    Case 7, 8, 9
                                        DiabCoh = 2
                            '45 to 59
                                    Case 10, 11, 12
                                        DiabCoh = 3
                            '60 to 69
                                    Case 13, 14
                                        DiabCoh = 4
                            '70 to 79
                                    Case 15, 16
                                        DiabCoh = 5
                                        '80+
                                    Case Else
                                        DiabCoh = 6
                                End Select
                                'jrs 2007/08/21 modmordstdet(j, R, k) = HLDIABETESRR(R, DiabCoh, ((k + 1) Mod 2) + 1) * modmordstdet(j, R, k - 8) ^ 0.75
                                'jrs 2007/09/24 error when doing Log(0)
                                'jrs 2009/12/04
                                If modmordstdet(j, R, k - 8) > 709 Then
                                    modmordstdet(j, R, k) = CSng(Log(HLDIABETESRR(R, DiabCoh, ((k + 1) Mod 2) + 1) * Exp(709) ^ 0.75))
                                ElseIf HLDIABETESRR(R, DiabCoh, ((k + 1) Mod 2) + 1) * Exp(modmordstdet(j, R, k - 8)) ^ 0.75 = 0 Then
                                    modmordstdet(j, R, k) = 0
                                Else
                                    modmordstdet(j, R, k) = CSng(Log(HLDIABETESRR(R, DiabCoh, ((k + 1) Mod 2) + 1) * Exp(modmordstdet(j, R, k - 8)) ^ 0.75))
                                End If

                                'jrs 2008/10/09
                                'transition from low income to high income
                                If GDPPCIP(R%) < 3 Then
                                    'jrs 2009/12/04
                                    If modmordstdet2(j, R, k - 8) > 709 Then
                                        modmordstdet2(j, R, k) = CSng(Log(HLDIABETESRR(R, DiabCoh, ((k + 1) Mod 2) + 1) * Exp(709) ^ 0.75))
                                    ElseIf HLDIABETESRR(R, DiabCoh, ((k + 1) Mod 2) + 1) * Exp(modmordstdet2(j, R, k - 8)) ^ 0.75 = 0 Then
                                        modmordstdet2(j, R, k) = 0
                                    Else
                                        modmordstdet2(j, R, k) = CSng(Log(HLDIABETESRR(R, DiabCoh, ((k + 1) Mod 2) + 1) * Exp(modmordstdet2(j, R, k - 8)) ^ 0.75))
                                    End If
                                End If
                            Else
                                modmordstdet(j, R, k) = 0
                                'jrs 2008/10/10
                                'transition from low income to high income
                                If GDPPCIP(R%) < 3 Then
                                    modmordstdet2(j, R, k) = 0
                                End If
                            End If
                        ElseIf k < 23 Then 'For Aids, 21 AIDS Males
                            If IY > 1 Then
                                If k = 21 Then
                                    If agedst(j, R%, 1) <> 0 Then
                                        'jrs 2007/08/14 using Heavy Aids Vector
                                        aidsdthscoh = maleaidsdths * aidsdeathsbyageHeavy(j, R, 1) / agedst(j, R%, 1)
                                    Else
                                        aidsdthscoh = 0
                                    End If
                                Else
                                    If agedst(j, R%, 2) <> 0 Then
                                        'jrs 2007/08/14 using Heavy Aids Vector
                                        aidsdthscoh = femaleaidsdths * aidsdeathsbyageHeavy(j, R, 2) / agedst(j, R%, 2)
                                    Else
                                        aidsdthscoh = 0
                                    End If
                                End If
                                modmordstdet(j, R, k) = aidsdthscoh * 1000
                            End If
                            'jrs 2009/08/10
                        ElseIf k < 27 Then 'for Diarrhea and Malaria
                            'jrs 2009/09/15
                            '((k + 1) Mod 2) + 1 -> is 1 for odds, 2 for evens
                            'which is 1 for males and 2 for females
                            'k takes values from 23 to 26
                            kk = ((k + 1) Mod 2) + 1
                            'ln(MortDiarr) = C + B * ln(MortCommDis)
                            'units in 100,000
                            'if othr. comm. is non zero
                            If modmordstdet(j, R, kk) > 0 Then
                                modmordstdet(j, R, k) = CSng(Exp(mortFactorsInf(j, kk, 1) + mortFactorsInf(j, kk, 2) * Log(modmordstdet(j, R%, kk) * 100)) / 100)
                            Else
                                modmordstdet(j, R, k) = 0
                            End If
                            'if othr. comm. is non zero
                            If modmordstdet2(j, R, kk) > 0 Then
                                'Transition Mortality
                                modmordstdet2(j, R, k) = CSng(Exp(mortFactorsInf(j, kk, 1) + mortFactorsInf(j, kk, 2) * Log(modmordstdet2(j, R%, kk) * 100)) / 100)
                            Else
                                modmordstdet2(j, R, k) = 0
                            End If
                        ElseIf k < 29 Then 'Resp. Infections
                            'jrs 2009/09/15
                            '((k + 1) Mod 2) + 1 -> is 1 for odds, 2 for evens
                            'which is 1 for males and 2 for females
                            'k takes values from 23 to 26
                            kk = ((k + 1) Mod 2) + 1

                            'ln(MortDiarr) = C + B * ln(MortCommDis)
                            'units in 100,000
                            'if othr. comm. is non zero
                            If modmordstdet(j, R, kk) > 0 Then
                                modmordstdet(j, R, k) = CSng(Exp(mortFactorsRespInf(j, kk, 1) + mortFactorsRespInf(j, kk, 2) * Log(modmordstdet(j, R%, kk) * 100)) / 100)
                            Else
                                modmordstdet(j, R, k) = 0
                            End If
                            'if othr. comm. is non zero
                            If modmordstdet2(j, R, kk) > 0 Then
                                'Transition Mortality
                                modmordstdet2(j, R, k) = CSng(Exp(mortFactorsRespInf(j, kk, 1) + mortFactorsRespInf(j, kk, 2) * Log(modmordstdet2(j, R%, kk) * 100)) / 100)
                            Else
                                modmordstdet2(j, R, k) = 0
                            End If
                        Else 'Mental Health 29 and 30
                            'jrs 2018/03/03
                            'If R% = 118 And j = 18 And k = 29 Then Stop
                            'Compute Mental Health as a portion of Other NCDs
                            'modmordstdet(j, R%, k) = modmordstdet(j, R%, k - 18) * mordstDet(j, R%, k) / (mordstDet(j, R%, k) + mordstDet(j, R%, k - 18))
                            'modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k - 18) * mordstDet(j, R%, k) / (mordstDet(j, R%, k) + mordstDet(j, R%, k - 18))
                            'jrs 2010/02/18
                            'Restore original data to keep constant
                            modmordstdet(j, R, k) = mordstDet(j, R%, k)
                            'Save data for transition too
                            modmordstdet2(j, R, k) = modmordstdet(j, R, k)
                        End If
                    Next k

                Next j
            Next R%

            'jrs 2009/05/12
            UpdateRespDisease(modmordstdet, sird, modmordstdet2)

            'All Years: First Year Only, compute normalization factors
            '---------------------------------------------------------
            If IY% = 1 Then
                'jrs 2010/01/25
                ComputeFactorsNormScalShift(mordstDet, popgdr, Ntypes, context)
            End If

            'All Years: Apply Factors
            '------------------------
            'jrs 2013/12/06
            ApplyFactors(context)

            'jrs 2007/10/12
            'All Years: apply 1.5 Male to Female ratio factor to Traffic Mortality
            '---------------------------------------------------------------------
            If IY > 1 Then
                Dim TMratio As Single
                Dim CohTMort As Single
                For R = 1 To NR
                    For j = 0 To NCohorts
                        'jrs 2009/01/13 If TMMFratios(j, R) > 0 Then
                        'Traffic Mortality Switch
                        If TMMFratios(j, R) > 0 And hlvehsw > 0 Then
                            'jrs 2024/03/26 TMratio = CSng(1.5 * (IY - 1) / 100 + TMMFratios(j, R) * (101 - IY) / 100)
                            TMratio = CSng(1.5 * (IY - 1) / (LastYear - FirstYear) + TMMFratios(j, R) * (LastYear - FirstYear + 1 - IY) / (LastYear - FirstYear))
                            CohTMort = CSng(modmordstdet(j, R, 13) + modmordstdet(j, R, 14))
                            'Males
                            modmordstdet(j, R, 13) = CohTMort * TMratio / (TMratio + 1)
                            'Females
                            modmordstdet(j, R, 14) = CohTMort / (TMratio + 1)
                        End If
                    Next j
                Next R
            Else
                For R = 1 To NR
                    For j = 0 To NCohorts
                        If modmordstdet(j, R, 14) <> 0 Then
                            TMMFratios(j, R) = modmordstdet(j, R, 13) / modmordstdet(j, R, 14)
                        Else
                            TMMFratios(j, R) = 0
                        End If
                    Next j
                Next R
            End If

            'jrs 2009/10/30
            'All years: Check that J-curve is monotonically increasing (using Proxy J-curve, Sweden)
            '-----------------------------------------------------------------------------------------
            AdjustJcurveShape(modmordstdet, LowMortLimit, Ntypes, 1, agedst) 'males
            AdjustJcurveShape(modmordstdet, LowMortLimit, Ntypes, 2, agedst) 'females

            'jrs 2012/09/13
            If NCohorts > 21 Then
                'Create Extended Longevity Mortality
                '-----------------------------------
                ifsExtendedLongevityMortality(modmordstdet, Ntypes, context)
                'Reinitialize Extended Ages Population
                If IY = 1 Then
                    PopRedistributionofCentenarians(modmordstdet, agedst, Ntypes, context)
                End If
            End If

            'jrs 2009/09/29
            'All years: apply exogenous multiplier
            '-------------------------------------
            If IY > 1 Then
                For R = 1 To NR
                    For j = 0 To NCohorts
                        For k = 1 To Ntypes
                            modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * hlmortm(R%, k \ 2 + k Mod 2) * mortm(R%) 'jrs 2014/10/06
                            'jrs 2013/02/14
                            'Children under 5 CDs
                            If j < 2 And (k < 3 Or k > 20) And k < 29 Then
                                'Males
                                If k Mod 2 = 1 Then
                                    modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * hlmortcdchldm(R%, 1) * hlmortcdchldm(R%, 3)
                                Else 'Females
                                    modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * hlmortcdchldm(R%, 2) * hlmortcdchldm(R%, 3)
                                End If
                            End If
                            'Adult 15-49 CDs
                            If j > 3 And j < 11 And (k < 3 Or k > 20) And k < 29 Then
                                'Males
                                If k Mod 2 = 1 Then
                                    modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * hlmortcdadltm(R%, 1) * hlmortcdadltm(R%, 3)
                                Else 'Females
                                    modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * hlmortcdadltm(R%, 2) * hlmortcdadltm(R%, 3)
                                End If
                            End If
                        Next k
                    Next j
                Next R
            End If

            'jrs 2020/03/19
            'Apply Additional Comm Disease Mortality
            '---------------------------------------
            Dim jEqIdx As Integer, MortFactor As Single, PrevFactor As Single
            For R = 1 To NR
                For j = 1 To 10
                    HLCOMADDDTHS(R, j) = 0
                Next j
                For j = 0 To NCohorts
                    If j = 0 Then
                        jEqIdx = 1
                    ElseIf j < 17 Then
                        jEqIdx = CInt(Floor(CDbl((j + 1) / 2)))
                    Else
                        jEqIdx = 9
                    End If
                    MortFactor = AMAX(hlcomaddmort(R%, jEqIdx), hlcomaddmort(R%, 10))
                    PrevFactor = AMAX(hlcomaddprev(R%, jEqIdx), hlcomaddprev(R%, 10))
                    modmordstdet(j, R%, 1) = modmordstdet(j, R%, 1) + PrevFactor * MortFactor * 1000
                    modmordstdet(j, R%, 2) = modmordstdet(j, R%, 2) + PrevFactor * MortFactor * 1000
                    HLCOMADDDTHS(R, jEqIdx) = CSng(HLCOMADDDTHS(R, jEqIdx) + PrevFactor * MortFactor * (agedst(j, R%, 1) + agedst(j, R%, 2)))
                Next j
            Next R

            'jrs 2018/06/04
            'All Years: Update Intentional Injuries
            '--------------------------------------
            If IY > 1 And svtohlsw = 1 Then
                UpdateIntInj()
            End If

            'jrs 2019/05/01
            'Keep mortality constant if necessary
            '------------------------------------
            If IY > 1 Then
                KeepMortalityConstant()
            End If

            'All Years: TEMP Life Expectancy
            '-------------------------------
            ReDim dtmp(NCohorts, 2), lifexpiTMP(NR%)
            If IY = 1 Then
                ReDim slifexpTMP(NR%, NGender%), LIFEXPshiftTMP(NR%, NGender%)
            End If
            birthfshr = 1 - birthmshr

            For R% = 1 To NR%
                'If R = 179 Then Stop
                '   All Years :  Compute Calorie-based Starvation Factor
                '   -----------------------------------------------------

                'jrs 2009/09/30 removing starvation deaths
                '        clav = 1E+08!
                '        If (agon > 0.5) Then
                '            clav = AMAX(0.1, CLAVAL(R%))
                '        End If
                '        clsexp = AMAX(0, Amin(clsexp, 5))
                '        CLNEED(R%) = AMAX(0.001, CLNEED(R%))
                '        clsf = AMAX(1 - (clav / CLNEED(R%) * CalIAdj(R%)), 0) ^ clsexp
                '        clsf = Amin(0.5, clsf)

                '        mortinfants = mordst(0, R%, 1) * birthmshr + mordst(0, R%, 2) * birthfshr
                '        CumMortTMP = mortinfants
                '        CumLifTMP = 0
                '        For j = 1 To NCohorts
                '            mordstTMP1 = (modmordstdet(j, R%, 1) + modmordstdet(j, R%, 3) + modmordstdet(j, R%, 5)) / 1000
                '            mordstTMP2 = (modmordstdet(j, R%, 2) + modmordstdet(j, R%, 4) + modmordstdet(j, R%, 6)) / 1000
                '            MortCohortTMP = (mordstTMP1 * agedst(j, R%, 1) + _
                '                mordstTMP2 * agedst(j, R%, 2)) / _
                '                AMAX(0.0001, (agedst(j, R%, 1) + agedst(j, R%, 2)))
                '            CohMortTMP = MortCohortTMP
                '            'If CumLifTMP + 5 < LIFEXPTMP(R%, Total) Then
                '            If CumMortTMP + CohMortTMP * 5 < 0.5 Then
                '                CumMortTMP = CumMortTMP + CohMortTMP * 5 'Five year cohorts
                '                CumLifTMP = CumLifTMP + 5
                '            Else
                '                'Dif = LIFEXPTMP(R%, 3) - CumLifTMP
                '                Dif = 0.5 - CumMortTMP
                '                'CumMortTMP = CumMortTMP + CohMortTMP * Dif
                '                CumMortTMP = CumMortTMP + Dif
                '                'CumLifTMP = CumLifTMP + Dif
                '                CumLifTMP = CumLifTMP + Dif / CohMortTMP
                '            End If
                '        Next j
                '        mfactorTMP = 0.5 / CumMortTMP
                mfactorTMP = 1

                'All Years: Compute deaths
                '-------------------------
                Dim PopInCat As Single 'jrs 2020/03/19
                For j = 0 To NCohorts
                    For gend% = 1 To 2
                        mortdstmgtmp = 0
                        For z = 1 To CInt(Ntypes / 2)
                            If gend% = 1 Then
                                mortdstmgtmp = mortdstmgtmp + modmordstdet(j, R%, (z * 2) - 1)
                            Else
                                mortdstmgtmp = mortdstmgtmp + modmordstdet(j, R%, z * 2)
                            End If
                        Next z
                        mortdstmgtmp = mortdstmgtmp / 1000
                        'jrs 2012/09/18
                        If mortdstmgtmp > 1 Then
                            'jrs 2020/03/19 *** begin ***
                            If j = 0 Then
                                jEqIdx = 1
                                PopInCat = CSng(agedst(0, R, 1) + agedst(1, R, 1) + agedst(2, R, 1))
                                PopInCat = CSng(PopInCat + agedst(0, R, 2) + agedst(1, R, 2) + agedst(2, R, 2))
                            ElseIf j < 17 Then
                                If j < 3 Then
                                    PopInCat = CSng(agedst(0, R, 1) + agedst(1, R, 1) + agedst(2, R, 1))
                                    PopInCat = CSng(PopInCat + agedst(0, R, 2) + agedst(1, R, 2) + agedst(2, R, 2))
                                ElseIf j Mod 2 = 0 Then
                                    PopInCat = CSng(agedst(j - 1, R, 1) + agedst(j, R, 1))
                                    PopInCat = CSng(PopInCat + agedst(j - 1, R, 2) + agedst(j, R, 2))
                                Else
                                    PopInCat = CSng(agedst(j, R, 1) + agedst(j + 1, R, 1))
                                    PopInCat = CSng(PopInCat + agedst(j, R, 2) + agedst(j + 1, R, 2))
                                End If
                                jEqIdx = CInt(Floor(CDbl((j + 1) / 2)))
                            Else
                                PopInCat = 0
                                For z = 1 To NCohorts
                                    PopInCat = CSng(PopInCat + agedst(z, R, 1) + agedst(z, R, 2))
                                Next z
                                jEqIdx = 9
                            End If
                            If HLCOMADDDTHS(R, jEqIdx) > 0 Then
                                HLCOMADDDTHS(R, jEqIdx) = CSng(HLCOMADDDTHS(R, jEqIdx) / mortdstmgtmp * agedst(j, R, gend) / PopInCat + HLCOMADDDTHS(R, jEqIdx) * (PopInCat - agedst(j, R, gend)) / PopInCat)
                            End If
                            'jrs 2020/03/19 *** end ***

                            'Adjust mortality to be at most 1
                            For z = 1 To CInt(Ntypes / 2)
                                If gend% = 1 Then
                                    modmordstdet(j, R%, (z * 2) - 1) = modmordstdet(j, R%, (z * 2) - 1) / mortdstmgtmp
                                Else
                                    modmordstdet(j, R%, z * 2) = modmordstdet(j, R%, z * 2) / mortdstmgtmp
                                End If
                            Next z
                        End If

                        If j = 0 Then
                            'jrs 2009/09/30 removing starvation deaths
                            'ChilSdeath = Amin(0.1 * agedst(j, R%, gend%), _
                            'agedst(j, R%, gend%) * (clsf * clmage(0)))
                            'jrs 2012/09/05
                            'jrs 2007/12/17 adding aMax, to put a lower limit on deaths since they were going negative
                            'the value 0.0000336 is the minimum found for Japan in deaths per 1000 and divided by 100
                            dtmp(j, gend%) = CSng(AMAX(0.000000336 * agedst(j, R%, gend%), Amin(0.25 * agedst(j, R%, gend%),
                                agedst(j, R%, gend%) * mortdstmgtmp * mfactorTMP)))  'jrs 2009/09/30 + _
                            'ChilSdeath) ' + aidsdthscoh)
                            'jrs 2009/09/30 saving infant mortality
                            If gend% = 1 Then
                                mortinfants = dtmp(0, 1)
                            Else
                                mortinfants = mortinfants + dtmp(0, 2)
                                mortinfants = mortinfants / (agedst(0, R%, 1) + agedst(0, R%, 2))
                            End If

                        ElseIf j = 1 Then 'As with child malnutrition, SDEATH applies to cohorts 0,1
                            'jrs 2009/09/30 removing starvation deaths
                            'ChilSdeath = Amin(0.01 * agedst(j, R%, gend%), _
                            'agedst(j, R%, gend%) * (clsf * clmage(1)))
                            'jrs 2012/09/05
                            'jrs 2007/12/17 adding aMax, to put a lower limit on deaths since they were going negative
                            'the value 0.0000336 is the minimum found for Japan in deaths per 1000 and divided by 100
                            dtmp(j, gend%) = CSng(AMAX(0.000000336 * agedst(j, R%, gend%), Amin(0.1 * agedst(j, R%, gend%),
                                agedst(j, R%, gend%) * mortdstmgtmp * mfactorTMP))) 'jrs 2009/09/30 + _
                            'ChilSdeath) ' + aidsdthscoh)
                        Else
                            'jrs 2012/09/05
                            'jrs 2007/12/17 adding aMax, to put a lower limit on deaths since they were going negative
                            'the value 0.0000336 is the minimum found for Japan in deaths per 1000 and divided by 100
                            'jrs 2012/09/18 dtmp(j, gend%) = AMAX(0.000000336 * agedst(j, R%, gend%), Amin(0.75 * agedst(j, R%, gend%), _
                            'agedst(j, R%, gend%) * mortdstmgTMP * mfactorTMP)) ' + _
                            'aidsdthscoh)
                            'jrs 2024/03/22 leave at least 100 people per age category
                            If j < NCohorts Then
                                dtmp(j, gend%) = AMAX(0, Amin(agedst(j, R%, gend%) - 0.0001,
                                agedst(j, R%, gend%) * mortdstmgtmp * mfactorTMP))
                            Else
                                'Allow all centenarians to die
                                dtmp(j, gend%) = AMAX(0, Amin(agedst(j, R%, gend%),
                                agedst(j, R%, gend%) * mortdstmgtmp * mfactorTMP))
                            End If
                        End If
                    Next gend%
                Next j

                'mti 2019/03/06 start
                '----------- Neonatal Mortality Model --------------------

                If IY > 1 Then

                    INFMOR(R%) = CSng(1000 * mortinfants)
                    'mortinfants, done by JRS above, is deaths by pop for age-group 0(newborns); INFMOR is in deaths per 1000 newborns
                    'the same computation is later; having it here will help build the driver (diagram) info for NeoNat

                    'mti 2019/02/22 Start
                    'Neonatal Mortality Rate
                    '------------------------
                    If context.HistoricMode Then
                        Call XYTABL("Infant Mortality Rate Versus Neonatal Mortality Rate (1960) Linear",
                                  INFMOR(R%), CalNeoNatMor, context)  'Mortality rates are in deaths per 1000 live births; The function has no intercept
                    Else
                        Call XYTABL("Infant Mortality Rate Versus Neonatal Mortality Rate (MOSTRECENT) Linear",
                                    INFMOR(R%), CalNeoNatMor, context)  'Mortality rates are in deaths per 1000 live births; The function has no an intercept
                    End If

                    'Yr 1: NeoNatMorshift(R%) = NEONATMOR(R%) - CalNeoNatMor
                    If NeoNatMorshift(R%) < 0 Then
                        ConvTime = 85 'death rate lower than function, country is already better than function
                    Else
                        ConvTime = 60
                    End If

                    '    If RegName(R%) = "Kosovo" Then Stop

                    NEONATMORBase = CSng(Amin(INFMOR(R%) * 0.9, AMAX(INFMOR(R%) * 0.2, CalNeoNatMor + ConvergeOverTime(NeoNatMorshift(R%), 0, ConvTime))))
                    NEONATMOR(R%) = CSng(Amin(INFMOR(R%) * 0.9, AMAX(INFMOR(R%) * 0.2, NEONATMORBase * neonatmorm(R%))))

                    If neonatmorm(R%) <> 1 Then
                        'INFMOR(R%) = Amin(400, AMAX(1, INFMOR(R%) + NEONATMOR(R%) - NEONATMORBase))
                        'basis of both neonatal and infant mortality is the same - 1000 live births
                        'adjustment -> change/base; per JRS - the value 0.0000336 is the minimum found for Japan in deaths per 1000 and divided by 100
                        adjfactor = 1 + ((NEONATMOR(R%) - NEONATMORBase) * (agedst(0, R%, 1) + agedst(0, R%, 2)) / 1000 / AMAX(0.0000000336, (dtmp(0, 1) + dtmp(0, 2))))

                        'adjusting infant deaths - total and by category using JRS health model code
                        For gend% = 1 To 2

                            mortdstmgtmp = 0
                            'Adjust all values of modmordstdet proportionately, but keep below 1000
                            'Repeate aggregation of modmordstdet over disease types within cohort to get mortdstmgtmp
                            For z = 1 To CInt(Ntypes / 2)
                                If gend% = 1 Then
                                    modmordstdet(0, R%, (z * 2) - 1) = Amin(1000, adjfactor * modmordstdet(0, R%, (z * 2) - 1))
                                    mortdstmgtmp = mortdstmgtmp + modmordstdet(0, R%, (z * 2) - 1)
                                Else
                                    modmordstdet(0, R%, z * 2) = Amin(1000, adjfactor * modmordstdet(0, R%, z * 2))
                                    mortdstmgtmp = mortdstmgtmp + modmordstdet(0, R%, z * 2)
                                End If
                            Next z

                            'Convert units or mortdstmgtmp to deaths per person
                            mortdstmgtmp = mortdstmgtmp / 1000
                            'Repeat check to make sure that total mortality rate by geneder and cohort is at most 1
                            If mortdstmgtmp > 1 Then
                                For z = 1 To CInt(Ntypes / 2)
                                    If gend% = 1 Then
                                        modmordstdet(0, R%, (z * 2) - 1) = modmordstdet(0, R%, (z * 2) - 1) / mortdstmgtmp
                                    Else
                                        modmordstdet(0, R%, z * 2) = modmordstdet(0, R%, z * 2) / mortdstmgtmp
                                    End If
                                Next z
                            End If

                            dtmp(0, gend%) = CSng(AMAX(0.000000336 * agedst(0, R%, gend%), Amin(0.25 * agedst(0, R%, gend%),
                                        agedst(0, R%, gend%) * mortdstmgtmp * mfactorTMP)))

                            If gend% = 1 Then
                                mortinfants = dtmp(0, 1)
                            Else
                                mortinfants = mortinfants + dtmp(0, 2)
                                mortinfants = mortinfants / AMAX(0.00001, (agedst(0, R%, 1) + agedst(0, R%, 2)))
                            End If
                        Next gend%

                    End If  'neonatmorm(R%) <> 1
                    'mti 2019/02/22 End


                End If

                'mti 2019/03/06 End

                'jrs 2019/05/17 moved up to affect Life Expectancy
                If IY > 1 Then
                    ifsHealthHIVMalaria(R%, context)
                    'All Years: Update HIV and Malaria
                    '-----------------------------------
                    'This parameter could go to common later
                    If hlmorthivmalsw = 1 Then
                        UpdateHIV(R%)
                        UpdateMalaria(R%)
                    End If
                End If

                'dsr 2016/03/24 Code to deal with matching deaths in SSPs **** start ****
                'General approach is to simply scale mortality rate by cohort and gender proportionately in each country
                'There may be some remaining differences, but almost certainly too small to worry about
                If ifsexomodelid > 0 Then
                    'MatchSSPDeaths dtmp, R%
                    'Calculate Initial Estimate of Total Deaths
                    deathstmp = 0
                    adjfactor = 0
                    'dsr 2016/05/04 Next line not needed
                    'DEATHS(R%) = 0
                    For gend% = 1 To 2
                        For j = 0 To NCohorts
                            deathstmp = deathstmp + dtmp(j, gend%)
                        Next j
                    Next gend%

                    'Read exogenous deaths
                    ReplaceVarWithExogenousForecastxReg("DEATHS", "", "", CStr(FirstYear + IY - 1), R%, context)
                    'Calculate adjustment factor
                    adjfactor = DEATHS(R%) / deathstmp

                    For j = 0 To NCohorts
                        For gend% = 1 To 2
                            mortdstmgtmp = 0
                            'Adjust all values of modmordstdet proportionately, but keep below 1000
                            'Repeate aggregation of modmordstdet over disease types within cohort to get mortdstmgtmp
                            For z = 1 To CInt(Ntypes / 2)
                                If gend% = 1 Then
                                    modmordstdet(j, R%, (z * 2) - 1) = Amin(1000, adjfactor * modmordstdet(j, R%, (z * 2) - 1))
                                    mortdstmgtmp = mortdstmgtmp + modmordstdet(j, R%, (z * 2) - 1)
                                Else
                                    modmordstdet(j, R%, z * 2) = Amin(1000, adjfactor * modmordstdet(j, R%, z * 2))
                                    mortdstmgtmp = mortdstmgtmp + modmordstdet(j, R%, z * 2)
                                End If
                            Next z
                            'Convert units or mortdstmgtmp to deaths per person
                            mortdstmgtmp = mortdstmgtmp / 1000
                            'Repeat check to make sure that total mortality rate by geneder and cohort is at most 1
                            If mortdstmgtmp > 1 Then
                                For z = 1 To CInt(Ntypes / 2)
                                    If gend% = 1 Then
                                        modmordstdet(j, R%, (z * 2) - 1) = modmordstdet(j, R%, (z * 2) - 1) / mortdstmgtmp
                                    Else
                                        modmordstdet(j, R%, z * 2) = modmordstdet(j, R%, z * 2) / mortdstmgtmp
                                    End If
                                Next z
                            End If

                            'Recalculate dtmp, same as before
                            If j = 0 Then
                                dtmp(j, gend%) = CSng(AMAX(0.000000336 * agedst(j, R%, gend%), Amin(0.25 * agedst(j, R%, gend%),
                                        agedst(j, R%, gend%) * mortdstmgtmp * mfactorTMP)))
                                If gend% = 1 Then
                                    mortinfants = dtmp(0, 1)
                                Else
                                    mortinfants = mortinfants + dtmp(0, 2)
                                    mortinfants = mortinfants / (agedst(0, R%, 1) + agedst(0, R%, 2))
                                End If
                            ElseIf j = 1 Then
                                dtmp(j, gend%) = CSng(AMAX(0.000000336 * agedst(j, R%, gend%), Amin(0.1 * agedst(j, R%, gend%),
                                        agedst(j, R%, gend%) * mortdstmgtmp * mfactorTMP)))
                            Else
                                'jrs 2024/03/22 leave at least 100 people, added - 0.0001
                                If j < NCohorts Then
                                    dtmp(j, gend%) = AMAX(0, Amin(agedst(j, R%, gend%) - 0.0001,
                                        agedst(j, R%, gend%) * mortdstmgtmp * mfactorTMP))
                                Else
                                    'allow all centenarians to die
                                    dtmp(j, gend%) = AMAX(0, Amin(agedst(j, R%, gend%),
                                        agedst(j, R%, gend%) * mortdstmgtmp * mfactorTMP))
                                End If
                            End If
                        Next gend%
                    Next j
                End If
                'dsr 2016/03/24 Code to deal with matching deaths in SSPs **** end ****


                'All Years: Compute Life Expectancy
                '----------------------------------

                'Explained by Randall Kuhn
                'Life Expectancy was computed as the median age at death,
                'and it should be computed as a mean
                'average number of years of life given the survival rates in each age group

                Dim infMort, cohMort As Single
                Dim lx, N, nax, nqx As Single

                For gend% = 1 To 2
                    'jrs 2009/05/22
                    '           CumMortTMP = dtmp(0, gend%) / _
                    '                AMAX(0.00001, agedst(0, R%, gend%))    'Infants
                    '           LIFEXPHLM(R%, gend%) = 0!
                    '           For j = 1 To NCohorts
                    '               CohMortTMP = dtmp(j, gend%) / _
                    '                   AMAX(0.00001, agedst(j, R%, gend%)) * 5 'Five year cohorts
                    '               If (CumMortTMP + CohMortTMP >= 0.5) Then
                    '                   LIFEXPHLM(R%, gend%) = LIFEXPHLM(R%, gend%) + _
                    '                       5! * (0.5 - CumMortTMP) / AMAX(0.0001, CohMortTMP)
                    '                   CumMortTMP = CumMortTMP + CohMortTMP
                    '                   Exit For
                    '               Else
                    '                   CumMortTMP = CumMortTMP + CohMortTMP
                    '                   LIFEXPHLM(R%, gend%) = LIFEXPHLM(R%, gend%) + 5!
                    '               End If
                    '           Next j
                    LIFEXPHLM(R%, gend%) = 0
                    infMort = CSng(dtmp(0, gend%) / AMAX(0.00001, agedst(0, R%, gend%)))
                    lx = 1
                    For j = 0 To NCohorts
                        'Years in cohort
                        If j = 0 Then
                            N = 1
                        ElseIf j = 1 Then
                            N = 4
                        Else
                            N = 5
                        End If

                        'jrs 2012/10/31
                        'cohMort = dtmp(j, gend%) / AMAX(0.00001, agedst(j, R%, gend%))
                        If agedst(j, R%, gend%) > 0 Then
                            cohMort = CSng(dtmp(j, gend%) / agedst(j, R%, gend%))
                        Else
                            cohMort = 0
                            For z = 1 To CInt(Ntypes / 2)
                                If gend% = 1 Then
                                    cohMort = CSng(cohMort + modmordstdet(j, R%, (z * 2) - 1))
                                    'Adjust mortality to be 0
                                    modmordstdet(j, R%, (z * 2) - 1) = 0
                                Else
                                    cohMort = CSng(cohMort + modmordstdet(j, R%, z * 2))
                                    'Adjust mortality to be 0
                                    modmordstdet(j, R%, z * 2) = 0
                                End If
                            Next z
                            cohMort = cohMort / 1000
                            'jrs 2013/08/27 no Life Expectancy beyond people alive
                            'cohMort = 1
                        End If

                        'Average years lived by those who die (per Keyfitz)
                        If j = 0 Then
                            If cohMort >= 0.107 Then
                                nax = 0.34
                            Else
                                nax = CSng(0.049 + 2.742 * cohMort)
                            End If
                        ElseIf j = 1 Then
                            If infMort >= 0.107 Then
                                nax = 1.356
                            Else
                                nax = CSng(1.587 - 2.167 * infMort)
                            End If
                        Else
                            nax = 2.5
                        End If

                        'Probability of death using period data
                        If j = NCohorts Then
                            nqx = 1
                        Else
                            nqx = (N * cohMort) / (1 + ((N - nax) * cohMort))
                            'jrs 2012/09/04
                            If nqx > 1 Then
                                nqx = 1
                            End If
                        End If

                        'Adding N years for survivors, and nax for deaths
                        If j < NCohorts Then
                            LIFEXPHLM(R%, gend%) = LIFEXPHLM(R%, gend%) + (lx * (1 - nqx) * N) + (lx * nqx * nax)
                        Else
                            'jrs 2012/09/03
                            If cohMort > 0 Then
                                LIFEXPHLM(R%, gend%) = LIFEXPHLM(R%, gend%) + lx / cohMort
                            End If
                        End If
                        'Probability of surviving until age x
                        lx = lx * (1 - nqx)
                        'Computing cummulative mortality for Barry's logic when LE going above 100
                        CumMortTMP = CumMortTMP + N * cohMort
                    Next j

                    If CumMortTMP < 0.5 Then
                        LIFEXPHLM(R%, gend%) = CSng(LIFEXPHLM(R%, gend%) + 15 * (0.5 - CumMortTMP) / AMAX(CumMortTMP, 0.001))
                    End If
                    LIFEXPHLM(R%, gend%) = AMAX(1, Amin(LIFEXPHLM(R%, gend%), 200))
                    'jrs 2007/06/06
                    '           If IY% = 1 Then
                    '               LIFEXPshiftTMP(R%, gend%) = slifexpTMP(R%, gend%) - LIFEXPHLM(R%, gend%)
                    '           End If
                    '           LIFEXPHLM(R%, gend%) = LIFEXPHLM(R%, gend%) + ConvergeOverTime(LIFEXPshiftTMP(R%, gend%), 0, 50)
                    '           LIFEXPHLM(R%, gend%) = AMAX(1, Amin(LIFEXPHLM(R%, gend%), 200))

                    'jrs 2008/10/03
                    'All Years: Save Life Expectancy to Population Module
                    '----------------------------------------------------
                    If hlmodelsw = 1 And (IY > 1 Or NCohorts > 21) Then 'jrs 2012/09/19 Or NCohorts > 21 'jrs 2010/01/08 added and IY>1
                        'jrs 2012/11/08 this was causing run to run
                        If IY > 1 Then
                            LIFEXP(R%, gend%) = LIFEXPHLM(R%, gend%)
                        End If
                        'jrs 2009/09/30 saving deaths
                        If gend% = 1 Then
                            DEATHS(R%) = 0
                        End If
                        For j = 0 To NCohorts
                            d(j, R%, gend%) = dtmp(j, gend%)
                            DEATHS(R%) = CSng(DEATHS(R%) + d(j, R%, gend%))
                        Next j
                        'jrs 2017/12/05
                        SDEATHS(R%) = DEATHS(R%)

                        'jrs 2009/09/30
                        'Life expectancy with no starvation deaths
                        LIFEXPNS(R%, gend%) = LIFEXPHLM(R%, gend%)
                        'jrs 2012/10/30
                        'jrs 2019/11/19
                        'If gend% = 2 Then
                        '    'jrs 2012/09/03
                        '    'Save total mortality vector
                        '    For j = 0 To NCohorts
                        '        ifsPopWorkDB.Add(New MorBasecase() With {
                        '                    .TimeId = CShort(IY% + FirstYear - 1),
                        '                    .RegionId = CShort(R%),
                        '                    .CohortId = CShort(j),
                        '                    .MortalityMales = If(agedst(j, R%, 1) > 0, d(j, R%, 1) / agedst(j, R%, 1) * 1000, Nothing),
                        '                    .MortalityFemales = If(agedst(j, R%, 2) > 0, d(j, R%, 2) / agedst(j, R%, 2) * 1000, Nothing)
                        '                })

                        '    Next j
                        '    ifsPopWorkDB.Flush()
                        'End If
                    End If
                Next gend%

                'jrs 2017/10/25 Save Child under 5 mortality
                If agedst(0, R%, 1) + agedst(0, R%, 2) + agedst(1, R%, 1) + agedst(1, R%, 2) > 0 Then
                    CHLMOR(R%) = CSng((dtmp(0, 1) + dtmp(0, 2) + dtmp(1, 1) + dtmp(1, 2)) / (agedst(0, R%, 1) + agedst(0, R%, 2) + agedst(1, R%, 1) + agedst(1, R%, 2)) * 1000)
                    'jrs 2017/11/13 Also save probability of dying as a child under 5
                    CHILDDTHR(R%) = 0
                    For G = 1 To 2
                        lx = 1
                        For j = 0 To 1
                            'Years in cohort
                            If j = 0 Then
                                N = 1
                            ElseIf j = 1 Then
                                N = 4
                            End If

                            'Mortality by age
                            If agedst(j, R%, G) > 0 Then
                                cohMort = CSng(dtmp(j, G) / agedst(j, R%, G))
                            Else
                                cohMort = 1
                            End If

                            'Average years lived by those who die (per Keyfitz)
                            If j = 0 Then
                                infMort = cohMort
                                If cohMort >= 0.107 Then
                                    nax = 0.34
                                Else
                                    nax = CSng(0.049 + 2.742 * cohMort)
                                End If
                            ElseIf j = 1 Then
                                If infMort >= 0.107 Then
                                    nax = 1.356
                                Else
                                    nax = CSng(1.587 - 2.167 * infMort)
                                End If
                            End If

                            'Probability of death using period data
                            nqx = (N * cohMort) / (1 + ((N - nax) * cohMort))

                            lx = lx * (1 - nqx)
                        Next j
                        CHILDDTHR(R%) = CSng(CHILDDTHR(R%) + (1 - lx) * (agedst(0, R%, G) + agedst(1, R%, G)))
                    Next G
                    CHILDDTHR(R%) = CSng(CHILDDTHR(R%) / (agedst(0, R%, 1) + agedst(1, R%, 1) + agedst(0, R%, 2) + agedst(1, R%, 2)) * 1000)
                End If

                'SMATMORTRAIO
                'jrs 2009/09/30
                If hlmodelsw = 1 And IY > 1 Then 'jrs 2010/01/08 added and IY>1
                    'saving infant mortality
                    INFMOR(R%) = CSng(mortinfants * 1000)
                    'jrs 2009/10/05
                    'bbh 2009/12/07
                    If IY% = 1 Then
                        sinfmor(R%) = INFMOR(R%)
                    Else
                        sinfmor(R%) = CSng(0.2 * INFMOR(R%) + 0.8 * sinfmor(R%))
                        'bbh 2014/01/26
                        lagcoef = ConvergeOverTime(1, infmorttfrlag, 10) 'phasing in the lag much reduces initial transient rise
                        laginfmor(R%) = CSng((1 - lagcoef) * INFMOR(R%) + lagcoef * laginfmor(R%))
                    End If
                    'bbh 2009/12/07 end
                    'saving crude death rate
                    CDR(R%) = DEATHS(R%) / POP(R%) * 1000
                    'dsr 2016/05/04 Save CDR for next year
                    SCDR(R%) = CDR(R%)
                    'jrs 2016/05/04
                    SCBR(R%) = CBR(R%)
                    'saving Population growth rate
                    POPR(R%) = (CBR(R%) - CDR(R%)) / 1000.0! * 100.0! + MIGRATE(R%)
                    'starvation deaths

                    'mti 2019/02/04 start
                    'mti 2019/01/22 start
                    'sPOPR(R%) = POPR(R%)
                    'mti 2019/01/22 End
                    'mti 2019/02/04 End

                    'SDEATH(R%) = 0
                    'kbn 2018/03/30-START- Add Maternal mortality here
                    'bbh 2021/04/30 Start: eliminate impact of TFR below 2 so that increases from below 1.9 don't raise MatMortRatio
                    'Same change in shift factor computation, year 1
                    'Call AnalFunc("MaternalMortalityRatio versus TFR(Log) & GDPPCP(Log)", New Single() {GDPPCP(R%), TFR(R%)}, 
                    'MatmortTemp, New String() {"GDP per capita in 1000 USD (Log)", "TFRMedUNPD(MOSTRECENT) (Log)"}, context)
                    '            MatMortTimePar = GDPPCPMax(R%) 'bbh 2021/04/30 possible code if want to limit impact of GDPPCP decline as in Oman
                    '            If GDPPCP(R%) < GDPPCPMax(R%) Then MatMortTimePar = GDPPCPMax(R%) - 0.2 * (GDPPCPMax(R%) - GDPPCP(R%))
                    Call AnalFunc("MaternalMortalityRatio versus TFR(Log) & GDPPCP(Log)", New Single() {GDPPCP(R%), AMAX(2, TFR(R%))},
                    MatmortTemp, New String() {"GDP per capita in 1000 USD (Log)", "TFRMedUNPD(MOSTRECENT) (Log)"}, context)
                    If MatMortshift(R%) < 0 Then 'Greatly reduce shift factor reduction when is negative and reduction raises MatMortRatio
                        MATMORTRATIO(R%) = AMAX(Amin(MatmortTemp, 2000), 3) + ConvergeOverTime(MatMortshift(R%), 0, 1000)
                    Else
                        MATMORTRATIO(R%) = AMAX(Amin(MatmortTemp, 2000), 3) + ConvergeOverTime(MatMortshift(R%), 0, 100)
                    End If
                    'bbh 2021/04/30 End

                    'bbh 2019/01/19 start
                    MatMortTimePar = 0.015
                    MatMortTimeParAdj = MatMortTimePar * AMAX(0, (30 - GDPPCP(R%)) / 30)
                    MatMortTimeTr(R%) = MatMortTimeTr(R%) + MATMORTRATIO(R%) * MatMortTimeParAdj
                    MATMORTRATIO(R%) = MATMORTRATIO(R%) - MatMortTimeTr(R%)
                    'jrs 2019/06/04
                    'jrs 2025/06/09 MATMORTRATIO(R%) = MATMORTRATIO(R%) * matmortratiom(R%)
                    'bbh 2019/01/19 end
                    'bbh 2021/04/30 start; historically, 5% reduction is a big drop each year but even bigger percentage drops were possible when MatMort was very high
                    MatMortTimePar = 0.95
                    If MATMORTRATIO(R%) >= 80 Then MatMortTimePar = 0.93
                    If MATMORTRATIO(R%) <= 40 Then MatMortTimePar = 0.97
                    'jrs 2025/06/05 I think the idea was to have something for the range between 40 and 80, the OR is wrong
                    'If MATMORTRATIO(R%) > 40 Or MATMORTRATIO(R%) > 80 Then MatMortTimePar = CSng(0.97 - 0.04 * (MATMORTRATIO(R%) - 40) / 40)
                    If MATMORTRATIO(R%) > 40 And MATMORTRATIO(R%) < 80 Then MatMortTimePar = CSng(0.97 - 0.04 * (MATMORTRATIO(R%) - 40) / 40)
                    If IY% > 1 Then
                        MATMORTRATIO(R%) = AMAX(MATMORTRATIO(R%), sMATMORTRATIO(R%) * MatMortTimePar)
                    End If
                    'bbh 2021/06/01 replace check against $30,000 with check against last year
                    'bbh 2021/06/01 If GDPPCP(R%) > 30 Then MATMORTRATIO(R%) = CSng(Amin(MATMORTRATIO(R%), sMATMORTRATIO(R%) * 1.01))
                    If GDPPCP(R%) < sGDPPCP(R%) Then MATMORTRATIO(R%) = CSng(Amin(MATMORTRATIO(R%), sMATMORTRATIO(R%) * 1.01))
                    'jrs 2025/06/09 moved after the limits to allow multiplier have an impact
                    MATMORTRATIO(R%) = MATMORTRATIO(R%) * matmortratiom(R%)

                    'E.g. Oman and Brunei GDPPCP drops after oil; but want to prevent huge rise in MatMortRatio
                    'bbh 2021/04/30 end
                    MATMORTRATIO(R%) = AMAX(Amin(MATMORTRATIO(R%), 2000), 3)
                    'kbn 2018/03/30 End

                    'bbh 2019/04/06 start
                    'jrs 2019/05/17 ifsHealthHIVMalaria (R%)
                    'bbh 2019/04/06 end

                    '            'mti 2019/02/22 Start
                    '            'Neonatal Mortality Rate
                    '            '------------------------
                    '            Call XYTABL("Infant Mortality Rate Versus Neonatal Mortality Rate (MOSTRECENT) Linear", _
                    '                            INFMOR(R%), CalNeoNatMor) 'Mortality rates are in deaths per 1000 live births; The function is without an intercept
                    '            'Yr 1: NeoNatMorshift(R%) = NEONATMOR(R%) - CalNeoNatMor
                    '            If NeoNatMorshift(R%) < 0 Then
                    '                ConvTime = 40 'death rate lower than function, country is already better than function
                    '            Else
                    '                ConvTime = 20
                    '            End If
                    '
                    ''    If RegName(R%) = "Kosovo" Then Stop
                    '
                    '            NEONATMORBase = Amin(INFMOR(R%) * 0.9, AMAX(INFMOR(R%) * 0.2, CalNeoNatMor + ConvergeOverTime(NeoNatMorshift(R%), 0, ConvTime)))
                    '            NEONATMOR(R%) = Amin(INFMOR(R%) * 0.9, AMAX(INFMOR(R%) * 0.2, NEONATMORBase * neonatmorm(R%)))
                    '
                    '            If neonatmorm(R%) <> 1 Then
                    '                INFMOR(R%) = Amin(400, AMAX(1, INFMOR(R%) + NEONATMOR(R%) - NEONATMORBase))
                    '            End If
                    '            'mti 2019/02/22 End

                End If

                'mti 2019/02/04 start
                sPOPR(R%) = POPR(R%)
                'mti 2019/02/04 End

                'dkb 2020/03/31 - Adding save variables for GII
                sMATMORTRATIO(R%) = MATMORTRATIO(R%)

                LIFEXPHLM(R%, Total) = CSng((LIFEXPHLM(R%, 1) * popgdr(R%, 1) +
                        LIFEXPHLM(R%, 2) * popgdr(R%, 2)) / AMAX(0.00001, popgdr(R%, 1) + popgdr(R%, 2)))
                'jrs 2008/10/03
                'All Years: Save Life Expectancy to Population Module
                '----------------------------------------------------
                If hlmodelsw = 1 And IY > 1 Then 'jrs 2010/01/08 added and IY>1
                    LIFEXP(R%, Total) = LIFEXPHLM(R%, Total)
                    'jrs 2009/09/30
                    'Life expectancy with no starvation deaths
                    LIFEXPNS(R%, Total) = LIFEXPHLM(R%, Total)
                End If
                'jrs 2013/05/07
                SLIFEXP(R%, Total) = LIFEXP(R%, Total)

                If IY% = 1 Then
                    For gend% = 1 To NGender%
                        slifexpTMP(R%, gend%) = LIFEXPHLM(R%, gend%)
                    Next gend%
                End If
            Next R%

            'jrs 2019/11/19
            If hlmodelsw = 1 And (IY > 1 Or NCohorts > 21) Then
                'jrs 2024/01/24
                If MultiThreading Then
                    ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertMor(d, agedst, NCohorts, NR, IY + FirstYear - 1))
                Else
                    ifsPopWorkDB.BulkInsertMor(d, agedst, NCohorts, NR, IY + FirstYear - 1)
                End If
            End If

            'jrs 2007/05/23
            Dim facLimM As Single, facLimF As Single
            'jrs 2009/08/25
            Dim facLim As Single
            'jrs 2007/06/28
            Dim modmordstdetCons(,,) As Double
            ReDim modmordstdetCons(NCohorts, NR%, 6)

            'All Years: Save Mortality Detail Distribution
            '---------------------------------------------
            For tempi% = 0 To NCohorts
                For tempj = 1 To NR%
                    modmordstdetCons(tempi%, tempj, 1) = modmordstdet(tempi%, tempj, 1) + modmordstdet(tempi%, tempj, 21) +
                    modmordstdet(tempi%, tempj, 23) + modmordstdet(tempi%, tempj, 25) + modmordstdet(tempi%, tempj, 27)
                    modmordstdetCons(tempi%, tempj, 2) = modmordstdet(tempi%, tempj, 2) + modmordstdet(tempi%, tempj, 22) +
                    modmordstdet(tempi%, tempj, 24) + modmordstdet(tempi%, tempj, 26) + modmordstdet(tempi%, tempj, 28)

                    modmordstdetCons(tempi%, tempj, 3) = 0
                    For z = 3 To 11 Step 2
                        modmordstdetCons(tempi%, tempj, 3) = modmordstdetCons(tempi%, tempj, 3) + modmordstdet(tempi%, tempj, z)
                    Next z
                    'Diabetes
                    modmordstdetCons(tempi%, tempj, 3) = modmordstdetCons(tempi%, tempj, 3) + modmordstdet(tempi%, tempj, 19)
                    'Mental Health
                    modmordstdetCons(tempi%, tempj, 3) = modmordstdetCons(tempi%, tempj, 3) + modmordstdet(tempi%, tempj, 29)
                    modmordstdetCons(tempi%, tempj, 4) = 0
                    For z = 4 To 12 Step 2
                        modmordstdetCons(tempi%, tempj, 4) = modmordstdetCons(tempi%, tempj, 4) + modmordstdet(tempi%, tempj, z)
                    Next z
                    'Diabetes
                    modmordstdetCons(tempi%, tempj, 4) = modmordstdetCons(tempi%, tempj, 4) + modmordstdet(tempi%, tempj, 20)
                    'Mental Health
                    modmordstdetCons(tempi%, tempj, 4) = modmordstdetCons(tempi%, tempj, 4) + modmordstdet(tempi%, tempj, 30)

                    modmordstdetCons(tempi%, tempj, 5) = 0
                    For z = 13 To 17 Step 2
                        modmordstdetCons(tempi%, tempj, 5) = modmordstdetCons(tempi%, tempj, 5) + modmordstdet(tempi%, tempj, z)
                    Next z

                    modmordstdetCons(tempi%, tempj, 6) = 0
                    For z = 14 To 18 Step 2
                        modmordstdetCons(tempi%, tempj, 6) = modmordstdetCons(tempi%, tempj, 6) + modmordstdet(tempi%, tempj, z)
                    Next z

                    'jrs 2007/07/05
                    'All Years: Add all sub types (11)
                    '-----------------------------------

                    'All Years: Communicable Diseases
                    '---------------------------------
                    'jrs 2009/08/11 added Diarrhea, Malaria and Resp. Infections
                    'Added Aids
                    'jrs 2019/11/19
                    'ifsPopWorkDB.Add(New MorDetBaseCase() With {
                    '                    .TimeId = CShort(IY% + FirstYear - 1),
                    '                    .RegionId = CShort(tempj),
                    '                    .CohortId = CShort(tempi),
                    '                    .MortPerMalesComDis = modmordstdet(tempi%, tempj, 1) + modmordstdet(tempi%, tempj, 21) +
                    '                        modmordstdet(tempi%, tempj, 23) + modmordstdet(tempi%, tempj, 25) + modmordstdet(tempi%, tempj, 27),
                    '                    .MortPerFeMalesComDis = modmordstdet(tempi%, tempj, 2) + modmordstdet(tempi%, tempj, 22) +
                    '                        modmordstdet(tempi%, tempj, 24) + modmordstdet(tempi%, tempj, 26) + modmordstdet(tempi%, tempj, 28),
                    '                    .MortPerMalesNonComDis = modmordstdetCons(tempi%, tempj, 3),
                    '                    .MortPerFeMalesNonComDis = modmordstdetCons(tempi%, tempj, 4),
                    '                    .MortPerMalesInjuries = modmordstdetCons(tempi%, tempj, 5),
                    '                    .MortPerFeMalesInjuries = modmordstdetCons(tempi%, tempj, 6),
                    '                    .ComDisNoAIDSMales = modmordstdet(tempi%, tempj, 1),
                    '                    .ComDisNoAIDSFemales = modmordstdet(tempi%, tempj, 2),
                    '                    .MaligNeoPlasMales = modmordstdet(tempi%, tempj, 3),
                    '                    .MaligNeoPlasFemales = modmordstdet(tempi%, tempj, 4),
                    '                    .CardioVascMales = modmordstdet(tempi%, tempj, 5),
                    '                    .CardioVascFemales = modmordstdet(tempi%, tempj, 6),
                    '                    .DigestiveMales = modmordstdet(tempi%, tempj, 7),
                    '                    .DigestiveFemales = modmordstdet(tempi%, tempj, 8),
                    '                    .RespiratoryMales = modmordstdet(tempi%, tempj, 9),
                    '                    .RespiratoryFemales = modmordstdet(tempi%, tempj, 10),
                    '                    .OtherG2Males = modmordstdet(tempi%, tempj, 11),
                    '                    .OtherG2Females = modmordstdet(tempi%, tempj, 12),
                    '                    .RoadTrafficAccMales = modmordstdet(tempi%, tempj, 13),
                    '                    .RoadTrafficAccFemales = modmordstdet(tempi%, tempj, 14),
                    '                    .OtherunintInjMales = modmordstdet(tempi%, tempj, 15),
                    '                    .OtherunintInjFemales = modmordstdet(tempi%, tempj, 16),
                    '                    .IntenInjMales = modmordstdet(tempi%, tempj, 17),
                    '                    .IntenInjFemales = modmordstdet(tempi%, tempj, 18),
                    '                    .DiabetesMales = modmordstdet(tempi%, tempj, 19),
                    '                    .DiabetesFemales = modmordstdet(tempi%, tempj, 20),
                    '                    .AIDSMales = modmordstdet(tempi%, tempj, 21),
                    '                    .AIDSFemales = modmordstdet(tempi%, tempj, 22),
                    '                    .DiarrheaMales = modmordstdet(tempi%, tempj, 23),
                    '                    .DiarrheaFemales = modmordstdet(tempi%, tempj, 24),
                    '                    .MalariaMales = modmordstdet(tempi%, tempj, 25),
                    '                    .MalariaFemales = modmordstdet(tempi%, tempj, 26),
                    '                    .RespInfMales = modmordstdet(tempi%, tempj, 27),
                    '                    .RespInfFemales = modmordstdet(tempi%, tempj, 28),
                    '                    .MentalHealthMales = modmordstdet(tempi%, tempj, 29),
                    '                    .MentalHealthFemales = modmordstdet(tempi%, tempj, 30)
                    '    })

                    'All Years: DEATHCAT
                    '-------------------
                    'jrs 2007/04/30 Save DEATHCAT '*** BEGIN ***'
                    If tempi% = 0 Then
                        'jrs 2007/07/09
                        '                DEATHCAT(tempj, 1, 1) = 0
                        '                DEATHCAT(tempj, 1, 2) = 0
                        '                DEATHCAT(tempj, 2, 1) = 0
                        '                DEATHCAT(tempj, 2, 2) = 0
                        '                DEATHCAT(tempj, 3, 1) = 0
                        '                DEATHCAT(tempj, 3, 2) = 0
                        For k = 1 To CInt(Ntypes / 2)
                            DEATHCAT(tempj, k, 1) = 0
                            DEATHCAT(tempj, k, 2) = 0
                            'jrs 2010/04/12
                            DEATHCAT(tempj, k, 3) = 0
                            'JRS 2009/06/11
                            HLYLL(tempj, k, 1) = 0
                            HLYLL(tempj, k, 2) = 0
                            'jrs 2009/08/31
                            HLYLD(tempj, k, 1) = 0
                            HLYLD(tempj, k, 2) = 0
                            HLYLDWORK(tempj, k, 1) = 0
                            HLYLDWORK(tempj, k, 2) = 0
                            HLYLLWORK(tempj, k, 1) = 0
                            HLYLLWORK(tempj, k, 2) = 0
                        Next k
                    End If
                    'All Years: Limits for DEATHCAT
                    '------------------------------
                    'jrs 2007/05/23 apply limits, 25% for infants, 10% for children 1-4, 75% for others
                    If tempi% = 0 Then
                        limit = 400
                    ElseIf tempi% = 1 Then
                        limit = 200
                    ElseIf tempi% = NCohorts Then 'jrs 2014/04/08 allowing all people in last age to die
                        limit = 1000
                    Else
                        limit = 750
                    End If
                    'jrs 2007/06/26
                    facLimM = 0
                    For z = 1 To CInt(Ntypes / 2)
                        facLimM = CSng(facLimM + modmordstdet(tempi%, tempj, (z * 2) - 1))
                    Next z

                    If facLimM > limit Then
                        facLimM = limit / facLimM
                    Else
                        facLimM = 1
                    End If

                    facLimF = 0
                    For z = 1 To CInt(Ntypes / 2)
                        facLimF = CSng(facLimF + modmordstdet(tempi%, tempj, (z * 2)))
                    Next z
                    If facLimF > limit Then
                        facLimF = limit / facLimF
                    Else
                        facLimF = 1
                    End If

                    'jrs 2007/07/09
                    '            DEATHCAT(tempj, 1, 1) = DEATHCAT(tempj, 1, 1) + facLimM * _
                    '            modmordstdetCons(tempi%, tempj, 1) * agedst(tempi%, tempj, 1) / 1000
                    '            DEATHCAT(tempj, 1, 2) = DEATHCAT(tempj, 1, 2) + facLimF * _
                    '            modmordstdetCons(tempi%, tempj, 2) * agedst(tempi%, tempj, 2) / 1000
                    '            DEATHCAT(tempj, 2, 1) = DEATHCAT(tempj, 2, 1) + facLimM * _
                    '            modmordstdetCons(tempi%, tempj, 3) * agedst(tempi%, tempj, 1) / 1000
                    '            DEATHCAT(tempj, 2, 2) = DEATHCAT(tempj, 2, 2) + facLimF * _
                    '            modmordstdetCons(tempi%, tempj, 4) * agedst(tempi%, tempj, 2) / 1000
                    '            DEATHCAT(tempj, 3, 1) = DEATHCAT(tempj, 3, 1) + facLimM * _
                    '            modmordstdetCons(tempi%, tempj, 5) * agedst(tempi%, tempj, 1) / 1000
                    '            DEATHCAT(tempj, 3, 2) = DEATHCAT(tempj, 3, 2) + facLimF * _
                    '            modmordstdetCons(tempi%, tempj, 6) * agedst(tempi%, tempj, 2) / 1000


                    For k = 1 To CInt(Ntypes / 2)
                        'jrs 2010/03/10 if standarization is off
                        If hlstddthsw = 0 Then
                            'Males
                            DEATHCAT(tempj, k, 1) = CSng(DEATHCAT(tempj, k, 1) + facLimM *
                            modmordstdet(tempi%, tempj, (2 * k) - 1) * agedst(tempi%, tempj, 1) / 1000)
                            'Females
                            DEATHCAT(tempj, k, 2) = CSng(DEATHCAT(tempj, k, 2) + facLimF *
                            modmordstdet(tempi%, tempj, 2 * k) * agedst(tempi%, tempj, 2) / 1000)
                        Else
                            'Males
                            DEATHCAT(tempj, k, 1) = CSng(DEATHCAT(tempj, k, 1) + facLimM *
                            modmordstdet(tempi%, tempj, (2 * k) - 1) * StdAgeDst(tempi%) * 100)
                            'Females
                            DEATHCAT(tempj, k, 2) = CSng(DEATHCAT(tempj, k, 2) + facLimF *
                            modmordstdet(tempi%, tempj, 2 * k) * StdAgeDst(tempi%) * 100)
                            'jrs 2010/04/07
                            'Total
                            If agedst(tempi%, tempj, 1) + agedst(tempi%, tempj, 2) > 0 Then
                                DEATHCAT(tempj, k, 3) = CSng(DEATHCAT(tempj, k, 3) +
                                (facLimM * modmordstdet(tempi%, tempj, (2 * k) - 1) * agedst(tempi%, tempj, 1) +
                                facLimF * modmordstdet(tempi%, tempj, 2 * k) * agedst(tempi%, tempj, 2)) /
                                (agedst(tempi%, tempj, 1) + agedst(tempi%, tempj, 2)) * StdAgeDst(tempi%) * 100)
                            Else 'if no population, then do simple average
                                DEATHCAT(tempj, k, 3) = CSng(DEATHCAT(tempj, k, 3) +
                                (facLimM * modmordstdet(tempi%, tempj, (2 * k) - 1) +
                                facLimF * modmordstdet(tempi%, tempj, 2 * k)) / 2 * StdAgeDst(tempi%) * 100)
                            End If
                            'jrs 2012/11/20 *** begin ***
                            If hlmodelsw = 1 And k = 11 Then 'for AIDS
                                'This is only necessary when there's a multiplier on the health module mortality, otherwise should be already the same
                                If tempi = 0 Then
                                    AIDSDTHS(tempj) = 0
                                End If
                                AIDSDTHS(tempj) = CSng(AIDSDTHS(tempj) + facLimM *
                                modmordstdet(tempi%, tempj, (2 * k) - 1) * agedst(tempi%, tempj, 1) / 1000)
                                AIDSDTHS(tempj) = CSng(AIDSDTHS(tempj) + facLimF *
                                modmordstdet(tempi%, tempj, 2 * k) * agedst(tempi%, tempj, 2) / 1000)
                                AIDSDRATE(tempj) = AIDSDTHS(tempj) / POP(tempj) * 100
                            End If
                            'jrs 2012/11/20 *** end ***

                        End If
                    Next k

                    'jrs 2007/04/30 Save DEATHCAT '*** END ***'


                    'jrs 2008/10/03 Save HLYLL *** BEGIN ***
                    'jrs 2010/03/15 If AvgAgePerCohort(tempi%) > 0 Then
                    'jrs 2010/01/12 adding discounting and age weighting
                    'jrs 2010/03/15 years = -AWC * Exp(-1 * AWBeta * AvgAgePerCohort(tempi%)) / (AWBeta + DiscountRt) ^ 2 * (Exp(-(AWBeta + DiscountRt) * (MaxLIFEXP - AvgAgePerCohort(tempi%))) * (1 + (AWBeta + DiscountRt) * MaxLIFEXP) - (1 + (AWBeta + DiscountRt) * AvgAgePerCohort(tempi%)))
                    'jrs 2014/03/13 *** begin ***
                    'Turning Off Discount Rate and Weights
                    'yearsM = -AWC * Exp(-1 * AWBeta * AvgAgePerCohort(tempi%)) / (AWBeta + DiscountRt) ^ 2 * (Exp(-(AWBeta + DiscountRt) * StdLifeExp(tempi%, 1)) * (1 + (AWBeta + DiscountRt) * (AvgAgePerCohort(tempi%) + StdLifeExp(tempi%, 1))) - (1 + (AWBeta + DiscountRt) * AvgAgePerCohort(tempi%)))
                    'yearsF = -AWC * Exp(-1 * AWBeta * AvgAgePerCohort(tempi%)) / (AWBeta + DiscountRt) ^ 2 * (Exp(-(AWBeta + DiscountRt) * StdLifeExp(tempi%, 2)) * (1 + (AWBeta + DiscountRt) * (AvgAgePerCohort(tempi%) + StdLifeExp(tempi%, 2))) - (1 + (AWBeta + DiscountRt) * AvgAgePerCohort(tempi%)))
                    yearsM = StdLifeExp(tempi%, 1)
                    yearsF = StdLifeExp(tempi%, 2)
                    'jrs 2014/03/13 *** end ***

                    'jrs 2010/03/15
                    '            'In last cohort
                    '            If tempi% = MaxLifeExpCohort Then
                    '                For k = 1 To cint(Ntypes / 2)
                    '                    'jrs 2009/08/25
                    '                    For G = 1 To 2
                    '                        If G = 1 Then
                    '                            facLim = facLimM
                    '                        Else
                    '                            facLim = facLimF
                    '                        End If
                    '
                    '                        HLYLL(tempj, k, G) = HLYLL(tempj, k, G) + _
                    '                        years * MaxLifeExpCohortPortion * facLim * _
                    '                        modmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) * agedst(tempi%, tempj, G) / 1000
                    '
                    '                        'All Years but first
                    '                        '--------------------
                    '                        If IY > 1 And smodmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) > 0.000000001 Then
                    '                            modmorbdstdetRt = (modmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) - _
                    '                            smodmordstdet(tempi%, tempj, (2 * k) - (G Mod 2))) / _
                    '                            smodmordstdet(tempi%, tempj, (2 * k) - (G Mod 2))
                    '
                    '                            modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) = _
                    '                            smodmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) * _
                    '                            (modmorbdstdetRt * hlmorbtomortgthport(k) + 1) 'jrs 2009/10/02 replaced YLDtoYLLratioGrowthPortion with hlmorbtomortgthport
                    '                        Else
                    '                            modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) = _
                    '                            modmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) * YLDtoYLLratio(tempj, k, G)
                    '                        End If
                    '                        'Save current year values
                    '                        '------------------------
                    '                        'Mortality
                    '                        smodmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) = modmordstdet(tempi%, tempj, (2 * k) - (G Mod 2))
                    '                        'Morbidity
                    '                        smodmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) = modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2))
                    '                        'Compute YLD
                    '                        '-----------
                    '                        HLYLD(tempj, k, G) = HLYLD(tempj, k, G) + _
                    '                        years * MaxLifeExpCohortPortion * facLim * _
                    '                        modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) * agedst(tempi%, tempj, G) / 1000
                    '
                    '                    Next G
                    '                Next k
                    '            Else 'all but last cohort
                    For k = 1 To CInt(Ntypes / 2)
                        'jrs 2009/08/25 computing Morbidity
                        'jrs 2009/08/25
                        For G = 1 To 2
                            If G = 1 Then
                                facLim = facLimM
                                Years = yearsM 'jrs 2010/03/15
                            Else
                                facLim = facLimF
                                Years = yearsF 'jrs 2010/03/15
                            End If

                            'YLL
                            '---
                            HLYLL(tempj, k, G) = CSng(HLYLL(tempj, k, G) +
                            Years * facLim *
                            modmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) * agedst(tempi%, tempj, G) / 1000)

                            'jrs 2009/08/31
                            'YLLWORK
                            '-------
                            'between 20 and 64
                            If tempi% > 4 And tempi% < 14 Then
                                HLYLLWORK(tempj, k, G) = CSng(HLYLLWORK(tempj, k, G) +
                                Years * facLim *
                                modmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) * agedst(tempi%, tempj, G) / 1000)
                            End If

                            'If GRPS(1, tempj) = "Bangladesh" And IY + FirstYear - 1 > 2030 And k = 3 And G = 1 Then
                            '    If smodmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) <= 0.000000001 And modmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) > 0 Then Stop
                            '    If modmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) - smodmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) > 0 Then Stop
                            'End If

                            'All Years but first
                            '--------------------

                            If IY > 1 And smodmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) > 0.000000001 Then
                                modmorbdstdetRt = CSng((modmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) -
                                smodmordstdet(tempi%, tempj, (2 * k) - (G Mod 2))) /
                                smodmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)))

                                'jrs 2014/07/21
                                modmorbdstdetRt = Amin(modmorbdstdetRt, 3)

                                'jrs 2014/07/23
                                'when hlmorbtomortgthport is no 1, like for Injuries
                                'then multiplying it directly by the rate is not simetrical for up and down movements
                                'ex. (1+P*0.75)
                                'you need 1/(1+P*0.75)
                                'so for mortality you have
                                '1 + P
                                'and to revert it you'll have 1/(1 + P)
                                'so 1 + P2 = 1 /(1+P)
                                'P = 1/(1+P2)-1
                                'replacing in what you need:
                                '1/(1+(1/(1+P2)-1)*0.75)
                                If modmorbdstdetRt > 0 Then
                                    modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) =
                                    smodmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) *
                                    1 / (1 + (1 / (1 + modmorbdstdetRt) - 1) * hlmorbtomortgthport(k))
                                Else
                                    modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) =
                                    smodmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) *
                                    (modmorbdstdetRt * hlmorbtomortgthport(k) + 1) 'jrs 2009/10/02 replaced YLDtoYLLratioGrowthPortion with hlmorbtomortgthport
                                End If
                            Else
                                'jrs 2014/07/21
                                If IY > 1 Then
                                    'keep it constant
                                    modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) =
                                    smodmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2))
                                Else
                                    'If tempi = 1 And GRPS(1, tempj) = "Russian Federation" And G = 1 And ((k > 1 And k < 7) Or k = 10 Or k = 15) Then Stop
                                    modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) =
                                    modmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) * YLDtoYLLratio(tempj, k, G)
                                End If
                                'jrs 2013/01/24 added iy=1
                                'jrs 2013/01/17
                                If IY = 1 Then
                                    imodmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) = CSng(modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)))
                                End If
                            End If
                            'Save current year values
                            '------------------------
                            'Mortality
                            smodmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) = modmordstdet(tempi%, tempj, (2 * k) - (G Mod 2))
                            'Morbidity
                            'jrs 2013/01/17
                            If IY > 1 And hlmorbm > -1 Then
                                If hlmorbconv + 1 > IY Then
                                    modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) =
                                    ConvergeOverTime(CSng(modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2))),
                                    imodmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) * hlmorbm, hlmorbconv + 1)
                                Else
                                    modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) =
                                    imodmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) * hlmorbm
                                End If
                            End If
                            'jrs 2014/08/05 added limit to 1000
                            'jrs 2013/01/16
                            modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) = Amin(AMAX(modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)), 0), 1000)

                            smodmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) = CSng(modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)))
                            'Compute YLD
                            '-----------
                            HLYLD(tempj, k, G) = CSng(HLYLD(tempj, k, G) +
                            Years * facLim *
                            modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) * agedst(tempi%, tempj, G) / 1000)
                            'jrs 2009/08/31
                            'YLDWORK
                            '-------
                            'between 20 and 64
                            If tempi% > 4 And tempi% < 14 And k <> 15 Then 'jrs 2019/05/01 not valid for mental health
                                HLYLDWORK(tempj, k, G) = CSng(HLYLDWORK(tempj, k, G) +
                                Years * facLim *
                                modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) * agedst(tempi%, tempj, G) / 1000)
                            End If
                        Next G
                    Next k
                    'jrs 2009/09/01 for mental health save YLD work
                    'between 20 and 64
                    If tempi% > 4 And tempi% < 14 Then
                        For G = 1 To 2
                            HLYLDWORK(tempj, 15, G) = CSng(HLYLDWORK(tempj, 15, G) + agedst(tempi%, tempj, G) * YLDtoPOPratio(tempj, G))
                            'jrs 2013/01/18
                            If IY > 1 And hlmorbm > -1 Then
                                If hlmorbconv + 1 > IY Then
                                    HLYLDWORK(tempj, 15, G) = ConvergeOverTime(HLYLDWORK(tempj, 15, G), CSng(agedst(tempi%, tempj, G) * YLDtoPOPratio(tempj, G) * hlmorbm), hlmorbconv + 1)
                                Else
                                    HLYLDWORK(tempj, 15, G) = CSng(agedst(tempi%, tempj, G) * YLDtoPOPratio(tempj, G) * hlmorbm)
                                End If
                            End If
                        Next G
                    End If
                    'jrs 2010/03/15            End If
                    'jrs 2010/03/15
                    '            Else
                    '                'jrs 2009/08/25
                    '                'Use same logic to compute morbidity for the rest of the cohorts
                    '                'This cohorts won't be used for YLL or YLDs but will be saved for display
                    '                For k = 1 To cint(Ntypes / 2)
                    '                    For G = 1 To 2
                    '                        modmorbdstdet(tempi%, tempj, (2 * k) - (G Mod 2)) = _
                    '                        modmordstdet(tempi%, tempj, (2 * k) - (G Mod 2)) * YLDtoYLLratio(tempj, k, G)
                    '                    Next G
                    '                Next k
                    '            End If
                    'jrs 2008/10/03 Save HLYLL ***  END  ***
                Next tempj
            Next tempi%
            'jrs 2019/11/19 ifsPopWorkDB.Flush()
            'jrs 2024/01/24
            If MultiThreading Then
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertMorDet(modmordstdet, modmordstdetCons, NCohorts, NR, IY% + FirstYear - 1))
            Else
                ifsPopWorkDB.BulkInsertMorDet(modmordstdet, modmordstdetCons, NCohorts, NR, IY% + FirstYear - 1)
            End If

            'jrs 2009/08/18 move outside the cohort and region loop, since it's no cohort specific
            For R = 1 To NR
                For k = 1 To CInt(Ntypes / 2)
                    For G = 1 To 2
                        '                'no mental health
                        '                If k < 15 Then
                        'jrs 2009/08/24
                        'All years but first
                        '                    If IY > 1 Then
                        '                        If SHLYLL(R, k, G) <> 0 Then
                        '                            YLLGR = (HLYLL(R, k, G) - SHLYLL(R, k, G)) / SHLYLL(R, k, G)
                        '                            HLYLD(R, k, G) = SHLYLD(R, k, G) * (YLLGR * YLDtoYLLratioGrowthPortion(k) + 1)
                        '                        Else
                        '                            HLYLD(R, k, G) = HLYLL(R, k, G) * YLDtoYLLratio(R, k, G)
                        '                        End If
                        '                    Else 'First Year only
                        'jrs 2009/06/04 HLYLD(tempj, k, 1) = HLYLL(tempj, k, 1) * hldisfac(k)
                        '                        HLYLD(R, k, G) = HLYLL(R, k, G) * YLDtoYLLratio(R, k, G)
                        '                    End If
                        'jrs 2009/08/24
                        'Save computed values
                        '                    SHLYLL(R, k, G) = HLYLL(R, k, G)
                        '                    SHLYLD(R, k, G) = HLYLD(R, k, G)
                        '                Else
                        If k = 15 Then
                            'Mental Health
                            'Compute YLD based on Population
                            HLYLD(R, k, G) = CSng(popgdr(R%, G) * YLDtoPOPratio(R, G))
                            'jrs 2013/01/18
                            If IY > 1 And hlmorbm > -1 Then
                                If hlmorbconv + 1 > IY Then
                                    HLYLD(R, k, G) = ConvergeOverTime(HLYLD(R, k, G), ipopgdr(R%, G) * YLDtoPOPratio(R, G) * hlmorbm, hlmorbconv + 1)
                                Else
                                    HLYLD(R, k, G) = ipopgdr(R%, G) * YLDtoPOPratio(R, G) * hlmorbm
                                End If
                            End If
                            'jrs 2014/04/08
                            'Need to compute morbidty for Heatlh Cost display
                            'If GRPS(1, R) = "United States of America" Then Stop
                            If HLYLL(R, k, G) > 0 Then
                                For tempi = 0 To NCohorts
                                    'If tempi = 1 And GRPS(1, R) = "Russian Federation" And G = 1 Then  Stop
                                    modmorbdstdet(tempi, R, (2 * k) - (G Mod 2)) = HLYLD(R, k, G) * modmordstdet(tempi, R, (2 * k) - (G Mod 2)) / HLYLL(R, k, G)
                                    'jrs 2014/08/05
                                    modmorbdstdet(tempi%, R, (2 * k) - (G Mod 2)) = Amin(AMAX(modmorbdstdet(tempi%, R, (2 * k) - (G Mod 2)), 0), 1000)
                                Next tempi
                            End If
                        End If

                        'jrs 2009/04/23
                        'Daly
                        '-----
                        HLDALY(R, k, G) = HLYLL(R, k, G) + HLYLD(R, k, G)
                    Next G
                Next k
            Next R

            'All Years: Sum for Total DEATHCAT
            '-------------------------------------
            'jrs 2007/04/30 Save DEATHCAT '*** BEGIN ***'
            Dim rr As Integer
            'jrs 2010/04/07
            If hlstddthsw = 0 Then
                For rr = 1 To NR%
                    'jrs 2007/07/09
                    '        DEATHCAT(rr, 1, 3) = DEATHCAT(rr, 1, 1) + DEATHCAT(rr, 1, 2)
                    '        DEATHCAT(rr, 2, 3) = DEATHCAT(rr, 2, 1) + DEATHCAT(rr, 2, 2)
                    '        DEATHCAT(rr, 3, 3) = DEATHCAT(rr, 3, 1) + DEATHCAT(rr, 3, 2)
                    For k = 1 To CInt(Ntypes / 2)
                        DEATHCAT(rr, k, 3) = DEATHCAT(rr, k, 1) + DEATHCAT(rr, k, 2)
                    Next k
                    'jrs 2012/11/20 *** begin ***
                    If hlmodelsw = 1 And IY > 1 Then 'jrs 2013/02/18 added and iy>1
                        'This is only necessary when there's a multiplier on the health module mortality, otherwise should be already the same
                        AIDSDTHS(rr) = DEATHCAT(rr, 11, 3)
                        AIDSDRATE(rr) = AIDSDTHS(rr) / POP(rr) * 100
                    End If
                    'jrs 2012/11/20 *** end ***
                Next rr
            End If
            'jrs 2007/04/30 Save DEATHCAT '*** END ***'

            'kbn 2018/05/17
            'kbn 2018/05/31 Insert new saved variable for intentional injuries
            For rr = 1 To NR%
                SDEATHMntlHealth(rr) = DEATHCAT(rr, 15, 3)
                SDEATHIntIj(rr) = DEATHCAT(rr, 9, 3)
            Next rr

            'jrs 2008/10/03 Save HLYLL *** BEGIN ***
            'All Years: SUM for Total HLYLL
            '------------------------------
            For rr = 1 To NR
                For k = 1 To CInt(Ntypes / 2)
                    HLYLL(rr, k, 3) = HLYLL(rr, k, 1) + HLYLL(rr, k, 2)
                    'jrs 2009/01/12
                    HLYLD(rr, k, 3) = HLYLD(rr, k, 1) + HLYLD(rr, k, 2)
                    'jrs 2009/04/23
                    HLDALY(rr, k, 3) = HLYLL(rr, k, 3) + HLYLD(rr, k, 3)
                    'jrs 2009/08/31
                    HLYLLWORK(rr, k, 3) = HLYLLWORK(rr, k, 1) + HLYLLWORK(rr, k, 2)
                    HLYLDWORK(rr, k, 3) = HLYLDWORK(rr, k, 1) + HLYLDWORK(rr, k, 2)
                Next k
            Next rr
            'jrs 2008/10/03 Save HLYLL ***  END  ***

            'All Years: Save Morbidity Detail Distribution
            '---------------------------------------------
            'jrs 2019/11/19
            ''jrs 2009/08/26
            'For tempi% = 0 To NCohorts
            '    For tempj = 1 To NR%
            '        ifsPopWorkDB.Add(New MorbDetBaseCase() With {
            '                                .TimeId = CShort(IY% + FirstYear - 1),
            '                                .RegionId = CShort(tempj),
            '                                .CohortId = CShort(tempi%),
            '                .MorbPerMalesComDis = modmorbdstdet(tempi%, tempj, 1) +
            '                modmorbdstdet(tempi%, tempj, 21) + modmorbdstdet(tempi%, tempj, 23) +
            '                modmorbdstdet(tempi%, tempj, 25) + modmorbdstdet(tempi%, tempj, 27),
            '                .MorbPerFeMalesComDis = modmorbdstdet(tempi%, tempj, 2) +
            '                modmorbdstdet(tempi%, tempj, 22) + modmorbdstdet(tempi%, tempj, 24) +
            '                modmorbdstdet(tempi%, tempj, 26) + modmorbdstdet(tempi%, tempj, 28),
            '                .MorbPerMalesNonComDis = modmorbdstdet(tempi%, tempj, 3) +
            '                modmorbdstdet(tempi%, tempj, 5) + modmorbdstdet(tempi%, tempj, 7) +
            '                modmorbdstdet(tempi%, tempj, 9) + modmorbdstdet(tempi%, tempj, 11) +
            '                modmorbdstdet(tempi%, tempj, 19) + modmorbdstdet(tempi%, tempj, 29),
            '                .MorbPerFeMalesNonComDis = modmorbdstdet(tempi%, tempj, 4) +
            '                modmorbdstdet(tempi%, tempj, 6) + modmorbdstdet(tempi%, tempj, 8) +
            '                modmorbdstdet(tempi%, tempj, 10) + modmorbdstdet(tempi%, tempj, 12) +
            '                modmorbdstdet(tempi%, tempj, 20) + modmorbdstdet(tempi%, tempj, 30),
            '                .MorbPerMalesInjuries = modmorbdstdet(tempi%, tempj, 13) +
            '                modmorbdstdet(tempi%, tempj, 15) + modmorbdstdet(tempi%, tempj, 17),
            '                .MorbPerFeMalesInjuries = modmorbdstdet(tempi%, tempj, 14) +
            '                modmorbdstdet(tempi%, tempj, 16) + modmorbdstdet(tempi%, tempj, 18),
            '                .ComDisNoAIDSMales = modmorbdstdet(tempi%, tempj, 1),
            '                .ComDisNoAIDSFemales = modmorbdstdet(tempi%, tempj, 2),
            '                .MaligNeoPlasMales = modmorbdstdet(tempi%, tempj, 3),
            '                .MaligNeoPlasFemales = modmorbdstdet(tempi%, tempj, 4),
            '                .CardioVascMales = modmorbdstdet(tempi%, tempj, 5),
            '                .CardioVascFemales = modmorbdstdet(tempi%, tempj, 6),
            '                .DigestiveMales = modmorbdstdet(tempi%, tempj, 7),
            '                .DigestiveFemales = modmorbdstdet(tempi%, tempj, 8),
            '                .RespiratoryMales = modmorbdstdet(tempi%, tempj, 9),
            '                .RespiratoryFemales = modmorbdstdet(tempi%, tempj, 10),
            '                .OtherG2Males = modmorbdstdet(tempi%, tempj, 11),
            '                .OtherG2Females = modmorbdstdet(tempi%, tempj, 12),
            '                .RoadTrafficAccMales = modmorbdstdet(tempi%, tempj, 13),
            '                .RoadTrafficAccFemales = modmorbdstdet(tempi%, tempj, 14),
            '                .OtherunintInjMales = modmorbdstdet(tempi%, tempj, 15),
            '                .OtherunintInjFemales = modmorbdstdet(tempi%, tempj, 16),
            '                .IntenInjMales = modmorbdstdet(tempi%, tempj, 17),
            '                .IntenInjFemales = modmorbdstdet(tempi%, tempj, 18),
            '                .DiabetesMales = modmorbdstdet(tempi%, tempj, 19),
            '                .DiabetesFemales = modmorbdstdet(tempi%, tempj, 20),
            '                .AIDSMales = modmorbdstdet(tempi%, tempj, 21),
            '                .AIDSFemales = modmorbdstdet(tempi%, tempj, 22),
            '                .DiarrheaMales = modmorbdstdet(tempi%, tempj, 23),
            '                .DiarrheaFemales = modmorbdstdet(tempi%, tempj, 24),
            '                .MalariaMales = modmorbdstdet(tempi%, tempj, 25),
            '                .MalariaFemales = modmorbdstdet(tempi%, tempj, 26),
            '                .RespInfMales = modmorbdstdet(tempi%, tempj, 27),
            '                .RespInfFemales = modmorbdstdet(tempi%, tempj, 28),
            '                .MentalHealthMales = modmorbdstdet(tempi%, tempj, 29),
            '                .MentalHealthFemales = modmorbdstdet(tempi%, tempj, 30)
            '                })
            '    Next tempj
            'Next tempi%

            'ifsPopWorkDB.Flush()
            'jrs 2024/01/24
            If MultiThreading Then
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertMorbDet(modmorbdstdet, NCohorts, NR, IY + FirstYear - 1))
            Else
                ifsPopWorkDB.BulkInsertMorbDet(modmorbdstdet, NCohorts, NR, IY + FirstYear - 1)
            End If

            'jrs 2008/11/20
            'All Years but first: Save Smoking Rate Forecast
            '-----------------------------------------------
            If Not context.HistoricMode And IY% > 1 Then
                Using dbS As IFsSmokingContext = New IFsSmokingContext(sSmokingFile)
                    For tempi% = 1 To NR
                        For gg = 1 To 2
                            dbS.Add(New SmokingRateEstimatesReg() With {
                                    .RegionName = GRPS(1, tempi%),
                                    .RegionId = tempi%,
                                    .Year = IY% + BaseYear - 1,
                                    .Sex = gg,
                                    .SmokingRate = HLSMOKING(tempi%, gg)
                                })
                        Next gg
                    Next tempi%
                    dbS.Flush()
                End Using
            End If

            'jrs 2009/07/08
            'Stunting
            '--------
            IFsHealthStunting(context)

            'jrs 2014/03/13
            'Compute Health Cost
            '-------------------
            IFsHealthCost(modmorbdstdet, context)

        End Sub

        Private Sub IFsHealthStunting(context As IForecastContext)
            'jrs 2016/12/01
            'Initialize Stunting Distribution
            '--------------------------------
            Dim ChlStuntingPopTerm, mm, ff As Double
            Dim aa As Integer
            Dim k As Integer

            If IY = 1 Then

                StuntingInit()

                'First Year: Save Stunting Detail Distribution
                '---------------------------------------------
                For R = 1 To NR
                    HLSTUNTWORK(R) = 0
                    'kbn 2019/05/12 -Start-
                    'Initialize shift factors for child stunting in the first year

                    ''If stunting is initialized as 0, fill holes with formulation in IFs
                    If HLSTUNTCHL(R) = 0 Then
                        'jrs 2020/11/17 HLSTUNTCHL(R) = CSng((((StuntingByAge(0, R, 1) + StuntingByAge(0, R, 2) + StuntingByAge(1, R, 1) + StuntingByAge(1, R, 2))) / POP(R)))
                        'jrs 2021/06/17 Stunting0 uses agedst0, Stunting1 uses agedst1, but POP0TO5 is agedst0 + fagedst1-4, so is very close to agedst1
                        'HLSTUNTCHL(R) = CSng((((StuntingByAge(0, R, 1) + StuntingByAge(0, R, 2) + StuntingByAge(1, R, 1) + StuntingByAge(1, R, 2))) / 10 / POP0TO5(R)))
                        HLSTUNTCHL(R) = CSng((((StuntingByAge(0, R, 1) + StuntingByAge(0, R, 2) + StuntingByAge(1, R, 1) + StuntingByAge(1, R, 2))) / 10 / (agedst(0, R, 1) + agedst(0, R, 2) + agedst(1, R, 1) + agedst(1, R, 2))))
                        'jrs 2021/06/23 Bahamas
                        HLSTUNT(R) = CSng(HLSTUNTCHL(R) * 1.5)
                    End If

                    'jrs 2020/11/17 ChlStuntingPopTerm = (((StuntingByAge(0, R, 1) + StuntingByAge(0, R, 2) + StuntingByAge(1, R, 1) + StuntingByAge(1, R, 2))) / POP(R))
                    'jrs 2021/06/17 Stunting0 uses agedst0, Stunting1 uses agedst1, but POP0TO5 is agedst0 + fagedst1-4, so is very close to agedst1
                    'ChlStuntingPopTerm = (((StuntingByAge(0, R, 1) + StuntingByAge(0, R, 2) + StuntingByAge(1, R, 1) + StuntingByAge(1, R, 2))) / 10 / POP0TO5(R))
                    ChlStuntingPopTerm = (((StuntingByAge(0, R, 1) + StuntingByAge(0, R, 2) + StuntingByAge(1, R, 1) + StuntingByAge(1, R, 2))) / 10 / (agedst(0, R, 1) + agedst(0, R, 2) + agedst(1, R, 1) + agedst(1, R, 2)))
                    HlChildstuntingshift(R) = HLSTUNTCHL(R) / ChlStuntingPopTerm
                    'kbn 2019/05/12 -End-

                    For j = 0 To NCohorts
                        'jrs 2019/11/19
                        'ifsPopWorkDB.Add(New StuntingByAge() With {
                        '        .Years = CShort(IY% + FirstYear - 1),
                        '        .NCohorts = CShort(j),
                        '        .Region = CShort(R),
                        '        .StuntingMales = StuntingByAge(j, R, 1),
                        '        .StuntingFemales = StuntingByAge(j, R, 2)
                        '           })

                        'First Year: Initialize Stunting for the working population
                        '-----------------------------------------------------------
                        If j < 2 Then
                            k = j
                        Else
                            k = (j - 1) * 5
                        End If
                        If k > workageentry(R%) - 5 And k < workageretire(R%) Then
                            If k < workageentry(R%) Then
                                'only part of the category belongs to working age
                                mm = 0
                                ff = 0
                                For aa = CInt(workageentry(R%)) To k + 4
                                    mm = mm + fagedst(aa, R%, 1)
                                    ff = ff + fagedst(aa, R%, 2)
                                Next aa
                                If agedst(j, R%, 1) > 0 Then
                                    mm = mm / agedst(j, R%, 1)
                                Else
                                    mm = 0
                                End If
                                If agedst(j, R%, 2) > 0 Then
                                    ff = ff / agedst(j, R%, 2)
                                Else
                                    ff = 0
                                End If
                                HLSTUNTWORK(R) = CSng(HLSTUNTWORK(R) + StuntingByAge(j, R, 1) * mm + StuntingByAge(j, R, 2) * ff)
                            ElseIf k + 4 >= workageretire(R%) Then
                                'only part of the category belongs to working age
                                mm = 0
                                ff = 0
                                For aa = k To CInt(workageretire(R%) - 1)
                                    mm = mm + fagedst(aa, R%, 1)
                                    ff = ff + fagedst(aa, R%, 2)
                                Next aa
                                If agedst(j, R%, 1) > 0 Then
                                    mm = mm / agedst(j, R%, 1)
                                Else
                                    mm = 0
                                End If
                                If agedst(j, R%, 2) > 0 Then
                                    ff = ff / agedst(j, R%, 2)
                                Else
                                    ff = 0
                                End If
                                HLSTUNTWORK(R) = CSng(HLSTUNTWORK(R) + StuntingByAge(j, R, 1) * mm + StuntingByAge(j, R, 2) * ff)
                            Else
                                HLSTUNTWORK(R) = CSng(HLSTUNTWORK(R) + StuntingByAge(j, R, 1) + StuntingByAge(j, R, 2))
                            End If
                        End If

                    Next j
                    If POPWORKING(R) > 0 Then
                        HLSTUNTWORK(R) = HLSTUNTWORK(R) / 10 / POPWORKING(R)
                    End If
                Next R
                'jrs 2019/11/19 ifsPopWorkDB.Flush()
                'jrs 2024/01/24
                If MultiThreading Then
                    ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertStunt(StuntingByAge, NCohorts, NR, IY% + FirstYear - 1))
                Else
                    ifsPopWorkDB.BulkInsertStunt(StuntingByAge, NCohorts, NR, IY% + FirstYear - 1)
                End If
            End If

            'use a slow moving average of the last 25 years
            'All years 
            '-------------------
            'gvdb 2024/04/05
            'unused variable
            'Dim Mortsum, TypeSum, MortRate, HeightDif As Double
            Dim HLSTUNTHeight As Double
            'Dim MortExp As Single
            'Dim HeightDifExp, HLSTUNTHeightDif As Double
            Dim FormValue As Single
            Dim TbName As String

            'jrs 2021/04/08
            TbName$ = "Child Stunting vs Child Malnutrition and Adult Female Sec. Completion"
            Dim StuntVal As Single
            If IY = 1 Then
                ReDim StuntingByAgeShift(NR, 2)
                'mti 2021/05/10 start
                ReDim iStuntVal(NR%)
                'mti 2021/05/10 End
            End If
            'jrs 2026/05/20
            'Dim TbName2 As String
            'TbName2$ = "GDP/Capita (PPP 2005) Versus Stunting (" & CStr(BaseYear) & ") Log"
            'TbName2$ = getFunctionNameYrOrMstRct(TbName2$, BaseYear, context)


            For R = 1 To NR
                'jrs 2021/04/08 Changing Stunting Forecast *** start ***
                'Mortsum = 0
                'For j = 0 To 3
                '    TypeSum = 0
                '    For k = 1 To Ntypes
                '        TypeSum = TypeSum + modmordstdet(j, R, k)
                '    Next k
                '    Mortsum = Mortsum + TypeSum
                'Next j
                'MortRate = Mortsum / 2 'gender division; redo above with Jose's approach
                'TbName$ = "GDP/Capita (PPP 2000) Versus Child (0-14) Mortality 2005 Log"
                'Call XYTABL(TbName$, GDPPCP(R%), MortExp, context)
                'jrs 2021/04/08 Analitical Function
                Call AnalFunc(TbName$, New Single() {MALNCHP(R%), EDSECPER(R%, 2)}, StuntVal,
                New String() {"MALNCHP", "EDSECPER(Female)"}, context)

                'mti 2021/05/10 start
                'StuntVal = AMAX(StuntVal, 0.000001)
                StuntVal = CSng(AMAX(StuntVal, 0.1)) 'jrs 2021/06/21 changed limit from 0 to 0.1 for Ukraine
                'mti 2021/05/10 End

                If IY = 1 Then
                    'MortShiftI(R) = MortRate - MortExp
                    'SHLSTUNTHeight(R) = 0
                    'HLSTUNTHeight = 0
                    'jrs 2021/04/08 Shift Factor
                    'mti 2021/05/10 start
                    iStuntVal(R%) = StuntVal
                    If StuntVal > 0.1 Then 'jrs 2021/06/21 changed limit from 0 to 0.1 for Ukraine
                        StuntingByAgeShift(R%, 1) = CSng(StuntingByAge(0, R%, 1) / agedst(0, R, 1) / StuntVal)
                        StuntingByAgeShift(R%, 2) = CSng(StuntingByAge(0, R%, 2) / agedst(0, R, 2) / StuntVal)
                    Else
                        StuntingByAgeShift(R%, 1) = CSng(StuntingByAge(0, R%, 1) / agedst(0, R, 1) - StuntVal)
                        StuntingByAgeShift(R%, 2) = CSng(StuntingByAge(0, R%, 2) / agedst(0, R, 2) - StuntVal)
                    End If
                    'mti 2021/05/10 End

                Else
                    'HeightDif = CSng(-0.06746 * MortRate + 0.00014 * MortRate ^ 2)
                    ''Function from Bozzoli, Deaton, Quintana-Domeque; yields centimeters
                    'HeightDifExp = CSng(-0.06746 * (MortExp + MortShiftI(R)) + 0.00014 * (MortExp + MortShiftI(R)) ^ 2)
                    'HLSTUNTHeightDif = -(HeightDif - HeightDifExp) * 2 'Minus sign translates negative height into positive stunting %
                    ''Mortality portionl; 1 CM Height Dif yields 2% Population stunting
                    ''jrs 2016/03/08 HLSTUNTHeight = (SHLSTUNTHeight(R) * 24 + HLSTUNTHeightDif) / 25
                    'HLSTUNTHeight = (SHLSTUNTHeight(R) * (hlstuntyrs - 1) + HLSTUNTHeightDif) / hlstuntyrs
                    ''jrs 2015/12/07 HLSTUNT(R) = (SHLSTUNT(R) * 24 + MALNCHP(R)) / 25 + HLSTUNTHeight
                    ''jrs 2016/03/08 HLSTUNT(R) = (SHLSTUNT(R) * 24 + MALNCHP(R) + MALNCHPSAM(R) * malnchpsamtostunt) / 25 + HLSTUNTHeight
                    ''jrs 2016/12/01
                    ''HLSTUNT(R) = (SHLSTUNT(R) * (hlstuntyrs - 1) + MALNCHP(R) + MALNCHPSAM(R) * malnchpsamtostunt) / hlstuntyrs + HLSTUNTHeight
                    ''Add Height term to malnutrition term
                    ''jrs 2016/12/01
                    ''HLSTUNT(R) = AMAX(Amin(HLSTUNT(R), 80), 0)
                    ''jrs 2016/12/06
                    ''jrs 2016/12/01
                    ''StuntingByAge(0, R, 1) = (MALNCHP(R) + MALNCHPSAM(R) * malnchpsamtostunt + HLSTUNTHeightDif) * POP(R%) / hlstuntyrs * 10 * agedst(0, R, 1) / (agedst(0, R, 1) + agedst(0, R, 2))
                    ''StuntingByAge(0, R, 2) = (MALNCHP(R) + MALNCHPSAM(R) * malnchpsamtostunt + HLSTUNTHeightDif) * POP(R%) / hlstuntyrs * 10 * agedst(0, R, 2) / (agedst(0, R, 1) + agedst(0, R, 2))
                    ''bbh 2021/01/18 replaced two lines including HLSTUNTHeightDif with two not having it; was raising HLSTUNTCHL in high-income countries w/o child undernutrtion
                    ''StuntingByAge(0, R, 1) = (MALNCHP(R) + MALNCHPSAM(R) * malnchpsamtostunt + HLSTUNTHeightDif) * 10 * agedst(0, R, 1)
                    ''StuntingByAge(0, R, 2) = (MALNCHP(R) + MALNCHPSAM(R) * malnchpsamtostunt + HLSTUNTHeightDif) * 10 * agedst(0, R, 2)
                    'StuntingByAge(0, R, 1) = (MALNCHP(R) + MALNCHPSAM(R) * malnchpsamtostunt) * 10 * agedst(0, R, 1)
                    'StuntingByAge(0, R, 2) = (MALNCHP(R) + MALNCHPSAM(R) * malnchpsamtostunt) * 10 * agedst(0, R, 2)
                    'jrs 2021/04/08 New Stunting Forecast
                    '------------------------------------
                    'jrs 2021/05/13
                    StuntVal = StuntVal + malnchpsamtostunt * (MALNCHPSAM(R) - SMALNCHPSAM(R))
                    'jrs 2021/05/13
                    SMALNCHPSAM(R%) = MALNCHPSAM(R%)

                    'mti 2021/05/10 start
                    If iStuntVal(R%) > 0.1 Then 'jrs 2021/06/21 changed limit from 0 to 0.1 for Ukraine
                        StuntingByAge(0, R%, 1) = StuntingByAgeShift(R%, 1) * StuntVal * agedst(0, R, 1)
                        StuntingByAge(0, R%, 2) = StuntingByAgeShift(R%, 2) * StuntVal * agedst(0, R, 2)
                    Else
                        StuntingByAge(0, R%, 1) = (StuntingByAgeShift(R%, 1) + StuntVal) * agedst(0, R, 1)
                        StuntingByAge(0, R%, 2) = (StuntingByAgeShift(R%, 2) + StuntVal) * agedst(0, R, 2)
                    End If
                    'mti 2021/05/10 End

                    'jrs 2021/04/08 Changing Stunting Forecast *** end ***

                    'jrs 2016/12/01
                    HLSTUNT(R) = 0
                    HLSTUNTWORK(R) = 0
                    'All Years but first: Save Stunting Detail Distribution
                    '------------------------------------------------------
                    For j = 0 To NCohorts
                        'jrs 2019/11/19
                        'ifsPopWorkDB.Add(New StuntingByAge() With {
                        '        .Years = CShort(IY% + FirstYear - 1),
                        '        .NCohorts = CShort(j),
                        '        .Region = CShort(R),
                        '        .StuntingMales = StuntingByAge(j, R, 1),
                        '        .StuntingFemales = StuntingByAge(j, R, 2)
                        '           })

                        'jrs 2016/12/06
                        If j > 3 Then
                            'jrs 2016/12/01
                            HLSTUNT(R) = CSng(HLSTUNT(R) + StuntingByAge(j, R, 1) + StuntingByAge(j, R, 2))
                        End If

                        'kbn 2019/05/12 -Start- Add child stunting computation to below
                        'jrs 2020/11/17 HLSTUNTCHL(R%) = CSng((((StuntingByAge(0, R, 1) + StuntingByAge(0, R, 2) + StuntingByAge(1, R, 1) + StuntingByAge(1, R, 2))) / POP(R)))
                        'jrs 2021/06/17 move HLSTUNTCHL out of the j (age) loop
                        'HLSTUNTCHL(R%) = CSng((((StuntingByAge(0, R, 1) + StuntingByAge(0, R, 2) + StuntingByAge(1, R, 1) + StuntingByAge(1, R, 2))) / 10 / POP0TO5(R)))
                        'If HlChildstuntingshift(R%) > 1 Then 'bbh 2021/01/18 added if test on convergence; allow only to decrease stunting (was raising for high-income countries)
                        '    HLSTUNTCHL(R%) = HLSTUNTCHL(R%) * ConvergeOverTime(CSng(HlChildstuntingshift(R%)), 1, 100)
                        'Else
                        '    HLSTUNTCHL(R%) = CSng(HLSTUNTCHL(R%) * HlChildstuntingshift(R%))
                        'End If
                        'HLSTUNTCHL(R%) = Amin(80, AMAX(0, HLSTUNTCHL(R%)))
                        'kbn 2019/05/12 -End

                        If j < 2 Then
                            k = j
                        Else
                            k = (j - 1) * 5
                        End If
                        If k > workageentry(R%) - 5 And k < workageretire(R%) Then
                            If k < workageentry(R%) Then
                                'only part of the category belongs to working age
                                mm = 0
                                ff = 0
                                For aa = CInt(workageentry(R%)) To k + 4
                                    mm = mm + fagedst(aa, R%, 1)
                                    ff = ff + fagedst(aa, R%, 2)
                                Next aa
                                If agedst(j, R%, 1) > 0 Then
                                    mm = mm / agedst(j, R%, 1)
                                Else
                                    mm = 0
                                End If
                                If agedst(j, R%, 2) > 0 Then
                                    ff = ff / agedst(j, R%, 2)
                                Else
                                    ff = 0
                                End If
                                HLSTUNTWORK(R) = CSng(HLSTUNTWORK(R) + StuntingByAge(j, R, 1) * mm + StuntingByAge(j, R, 2) * ff)
                            ElseIf k + 4 >= workageretire(R%) Then
                                'only part of the category belongs to working age
                                mm = 0
                                ff = 0
                                For aa = k To CInt(workageretire(R%) - 1)
                                    mm = mm + fagedst(aa, R%, 1)
                                    ff = ff + fagedst(aa, R%, 2)
                                Next aa
                                If agedst(j, R%, 1) > 0 Then
                                    mm = mm / agedst(j, R%, 1)
                                Else
                                    mm = 0
                                End If
                                If agedst(j, R%, 2) > 0 Then
                                    ff = ff / agedst(j, R%, 2)
                                Else
                                    ff = 0
                                End If
                                HLSTUNTWORK(R) = CSng(HLSTUNTWORK(R) + StuntingByAge(j, R, 1) * mm + StuntingByAge(j, R, 2) * ff)
                            Else
                                HLSTUNTWORK(R) = CSng(HLSTUNTWORK(R) + StuntingByAge(j, R, 1) + StuntingByAge(j, R, 2))
                            End If
                        End If
                    Next j

                    'jrs 2021/06/17 move HLSTUNTCHL out of the j (age) loop, start using only cohort 1, which includes infants from previous year
                    'jrs 2021/06/24 HLSTUNTCHL(R%) = CSng((((StuntingByAge(0, R, 1) + StuntingByAge(0, R, 2) + StuntingByAge(1, R, 1) + StuntingByAge(1, R, 2))) / 10 / (agedst(0, R, 1) + agedst(0, R, 2) + agedst(1, R, 1) + agedst(1, R, 2))))
                    HLSTUNTCHL(R%) = CSng((((StuntingByAge(0, R, 1) + StuntingByAge(0, R, 2) + fStuntingByAge(0, R, 1) + fStuntingByAge(0, R, 2) + StuntingByAge(1, R, 1) + StuntingByAge(1, R, 2))) / 10 / (agedst(0, R, 1) + agedst(0, R, 2) + fagedst(0, R, 1) + fagedst(0, R, 2) + agedst(1, R, 1) + agedst(1, R, 2))))
                    If HlChildstuntingshift(R%) > 1 Then 'bbh 2021/01/18 added if test on convergence; allow only to decrease stunting (was raising for high-income countries)
                        HLSTUNTCHL(R%) = HLSTUNTCHL(R%) * ConvergeOverTime(CSng(HlChildstuntingshift(R%)), 1, 100)
                    Else
                        HLSTUNTCHL(R%) = CSng(HLSTUNTCHL(R%) * HlChildstuntingshift(R%))
                    End If
                    'jrs 2021/06/29
                    HLSTUNTCHL(R%) = HLSTUNTCHL(R%) * hlstuntchlm(R%)
                    HLSTUNTCHL(R%) = Amin(99, AMAX(0, HLSTUNTCHL(R%))) 'jrs 2021/06/22 changed limit from 80 to 99

                    'Convert to % of total population
                    'jrs 2016/12/06
                    'jrs 2016/12/01
                    'HLSTUNT(R) = HLSTUNT(R) / 10 / POP(R)
                    HLSTUNT(R) = HLSTUNT(R) / 10 / (POP(R) - POPLE15(R))
                    HLSTUNT(R) = AMAX(Amin(HLSTUNT(R), 99), 0) 'jrs 2021/06/22 changed limit from 80 to 99
                    If POPWORKING(R) > 0 Then
                        HLSTUNTWORK(R) = HLSTUNTWORK(R) / 10 / POPWORKING(R)
                    End If
                End If
                SHLSTUNTHeight(R) = HLSTUNTHeight
                SHLSTUNT(R) = CSng(HLSTUNT(R) - HLSTUNTHeight) 'back out height term from maln moving average
                'bbh 2009/12/21 end

                'jrs 2009/07/14
                'Expected Stunting
                '-----------------
                'jrs 2021/04/08
                'TbName$ = "GDP/Capita (PPP 2005) Versus Stunting (" & CStr(BaseYear) & ") Log"
                'TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
                'jrs 2026/05/20 TblNameStunting
                'Call XYTABL(TbName2$, GDPPCP(R%), FormValue, context)
                Call XYTABL(TblNameStunting, GDPPCP(R%), FormValue, context)
                If IY = 1 Then
                    HLSTUNTExpShift(R) = HLSTUNT(R) - FormValue
                End If
                HLSTUNTExp(R) = FormValue + HLSTUNTExpShift(R)
                HLSTUNTExp(R) = AMAX(Amin(HLSTUNTExp(R), 80), 0)
            Next R
            'jrs 2019/11/19 ifsPopWorkDB.Flush()
            If IY > 1 Then
                'jrs 2024/01/24
                If MultiThreading Then
                    ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertStunt(StuntingByAge, NCohorts, NR, IY + FirstYear - 1))
                Else
                    ifsPopWorkDB.BulkInsertStunt(StuntingByAge, NCohorts, NR, IY + FirstYear - 1)
                End If
            End If
        End Sub

        'jrs 2013/12/06
        Private Sub LoadStAgeDst(ByRef StdAgeDst() As Single)
            ReDim StdAgeDst(NCohorts)
            StdAgeDst(0) = 0.0186
            StdAgeDst(1) = 0.07
            StdAgeDst(2) = 0.0869
            StdAgeDst(3) = 0.086
            StdAgeDst(4) = 0.0847
            StdAgeDst(5) = 0.0822
            StdAgeDst(6) = 0.0793
            StdAgeDst(7) = 0.0761
            StdAgeDst(8) = 0.0715
            StdAgeDst(9) = 0.0659
            StdAgeDst(10) = 0.0604
            StdAgeDst(11) = 0.0537
            StdAgeDst(12) = 0.0455
            StdAgeDst(13) = 0.0372
            StdAgeDst(14) = 0.0296
            StdAgeDst(15) = 0.0221
            StdAgeDst(16) = 0.0152
            StdAgeDst(17) = 0.0091
            StdAgeDst(18) = 0.0044
            StdAgeDst(19) = 0.0015
            StdAgeDst(20) = 0.0004
            StdAgeDst(21) = 0.00005
        End Sub

        'jrs 2014/01/10
        Private Function getRRTableName() As String
            'ReLoads the RRs
            If hlproxdrvrrsw = 0 Then
                getRRTableName = "HealthProxDrivRRs"
            Else
                Using db As IFsContext = New IFsContext()
                    Dim hpdrr As HealthProxDrivRRsList = db.HealthProxDrivRRsList.Where(Function(g) g.ID = hlproxdrvrrsw).Single()
                    If hpdrr IsNot Nothing Then
                        getRRTableName = hpdrr.TableName
                    Else
                        Debug.Print("Table for RRs Not found, the Default will be used - Health Module")
                        getRRTableName = "HealthProxDrivRRs"
                    End If
                End Using
            End If
        End Function

        'jrs 2016/03/29 added sTblName
        'jrs 2009/10/15
        Private Sub LoadRRs(sProxDriv As String, sDisType As String, ByRef rr() As Single, ByVal sTblName As String)
            'Loads Relative Risks

            Using db As IFsContext = New IFsContext()
                Select Case sTblName
                    Case "HealthProxDrivRRs"
                        Dim rs = (From PD In db.HealthProxDrivRRs
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx, Value = PD.RRValue)

                        For Each item In rs
                            rr(item.Key) = CSng(item.Value)
                        Next
                    Case "HealthProxDrivRRs-Black"
                        Dim rs = (From PD In db.HealthProxDrivRRsBlack
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx, Value = PD.RRValue)

                        For Each item In rs
                            rr(item.Key) = CSng(item.Value)
                        Next
                    Case "HealthProxDrivRRs-HH"
                        Dim rs = (From PD In db.HealthProxDrivRRsHH
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx, Value = PD.RRValue)

                        For Each item In rs
                            rr(item.Key) = CSng(item.Value)
                        Next
                    Case "HealthProxDrivRRs-HL"
                        Dim rs = (From PD In db.HealthProxDrivRRsHL
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx, Value = PD.RRValue)

                        For Each item In rs
                            rr(item.Key) = CSng(item.Value)
                        Next
                    Case "HealthProxDrivRRs-HM"
                        Dim rs = (From PD In db.HealthProxDrivRRsHM
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx, Value = PD.RRValue)

                        For Each item In rs
                            rr(item.Key) = CSng(item.Value)
                        Next
                    Case "HealthProxDrivRRs-LH"
                        Dim rs = (From PD In db.HealthProxDrivRRsLH
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx, Value = PD.RRValue)

                        For Each item In rs
                            rr(item.Key) = CSng(item.Value)
                        Next
                    Case "HealthProxDrivRRs-LL"
                        Dim rs = (From PD In db.HealthProxDrivRRsLL
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx, Value = PD.RRValue)

                        For Each item In rs
                            rr(item.Key) = CSng(item.Value)
                        Next
                    Case "HealthProxDrivRRs-ML"
                        Dim rs = (From PD In db.HealthProxDrivRRsML
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx, Value = PD.RRValue)

                        For Each item In rs
                            rr(item.Key) = CSng(item.Value)
                        Next
                    Case "HealthProxDrivRRs-Olofin"
                        Dim rs = (From PD In db.HealthProxDrivRRsOlofin
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx, Value = PD.RRValue)

                        For Each item In rs
                            rr(item.Key) = CSng(item.Value)
                        Next
                    Case "HealthProxDrivRRs-Olofin-High"
                        Dim rs = (From PD In db.HealthProxDrivRRsOlofinHigh
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx, Value = PD.RRValue)

                        For Each item In rs
                            rr(item.Key) = CSng(item.Value)
                        Next
                    Case "HealthProxDrivRRs-Olofin-Low"
                        Dim rs = (From PD In db.HealthProxDrivRRsOlofinLow
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx, Value = PD.RRValue)

                        For Each item In rs
                            rr(item.Key) = CSng(item.Value)
                        Next
                    Case "HealthProxDrivRRs-Olofin-W4A"
                        Dim rs = (From PD In db.HealthProxDrivRRsOlofinW4A
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx, Value = PD.RRValue)

                        For Each item In rs
                            rr(item.Key) = CSng(item.Value)
                        Next
                End Select
            End Using

        End Sub

        Private Sub LoadMultiDimRRs(sProxDriv As String, sDisType As String, ByRef rr As Dictionary(Of String, Single), ByVal sTblName As String)
            'Loads Relative Risks
            'Dim sType As String
            'jrs 2014/01/10 added parameter sTblName
            Using db As IFsContext = New IFsContext()
                Select Case sTblName
                    Case "HealthProxDrivRRs"
                        Dim rs = (From PD In db.HealthProxDrivRRs
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx & If(PD.Gender, "") & PD.Age, Value = PD.RRValue)

                        For Each item In rs
                            rr.Add(item.Key, CSng(item.Value))
                        Next
                    Case "HealthProxDrivRRs-Black"
                        Dim rs = (From PD In db.HealthProxDrivRRsBlack
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx & If(PD.Gender, "") & PD.Age, Value = PD.RRValue)

                        For Each item In rs
                            rr.Add(item.Key, CSng(item.Value))
                        Next
                    Case "HealthProxDrivRRs-HH"
                        Dim rs = (From PD In db.HealthProxDrivRRsHH
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx & If(PD.Gender, "") & PD.Age, Value = PD.RRValue)

                        For Each item In rs
                            rr.Add(item.Key, CSng(item.Value))
                        Next
                    Case "HealthProxDrivRRs-HL"
                        Dim rs = (From PD In db.HealthProxDrivRRsHL
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx & If(PD.Gender, "") & PD.Age, Value = PD.RRValue)

                        For Each item In rs
                            rr.Add(item.Key, CSng(item.Value))
                        Next
                    Case "HealthProxDrivRRs-HM"
                        Dim rs = (From PD In db.HealthProxDrivRRsHM
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx & If(PD.Gender, "") & PD.Age, Value = PD.RRValue)

                        For Each item In rs
                            rr.Add(item.Key, CSng(item.Value))
                        Next
                    Case "HealthProxDrivRRs-LH"
                        Dim rs = (From PD In db.HealthProxDrivRRsLH
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx & If(PD.Gender, "") & PD.Age, Value = PD.RRValue)

                        For Each item In rs
                            rr.Add(item.Key, CSng(item.Value))
                        Next
                    Case "HealthProxDrivRRs-LL"
                        Dim rs = (From PD In db.HealthProxDrivRRsLL
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx & If(PD.Gender, "") & PD.Age, Value = PD.RRValue)

                        For Each item In rs
                            rr.Add(item.Key, CSng(item.Value))
                        Next
                    Case "HealthProxDrivRRs-ML"
                        Dim rs = (From PD In db.HealthProxDrivRRsML
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx & If(PD.Gender, "") & PD.Age, Value = PD.RRValue)

                        For Each item In rs
                            rr.Add(item.Key, CSng(item.Value))
                        Next
                    Case "HealthProxDrivRRs-Olofin"
                        Dim rs = (From PD In db.HealthProxDrivRRsOlofin
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx & If(PD.Gender, "") & PD.Age, Value = PD.RRValue)

                        For Each item In rs
                            rr.Add(item.Key, CSng(item.Value))
                        Next
                    Case "HealthProxDrivRRs-Olofin-High"
                        Dim rs = (From PD In db.HealthProxDrivRRsOlofinHigh
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx & If(PD.Gender, "") & PD.Age, Value = PD.RRValue)

                        For Each item In rs
                            rr.Add(item.Key, CSng(item.Value))
                        Next
                    Case "HealthProxDrivRRs-Olofin-Low"
                        Dim rs = (From PD In db.HealthProxDrivRRsOlofinLow
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx & If(PD.Gender, "") & PD.Age, Value = PD.RRValue)

                        For Each item In rs
                            rr.Add(item.Key, CSng(item.Value))
                        Next
                    Case "HealthProxDrivRRs-Olofin-W4A"
                        Dim rs = (From PD In db.HealthProxDrivRRsOlofinW4A
                                  Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                                  Select Key = PD.DistributionIdx & If(PD.Gender, "") & PD.Age, Value = PD.RRValue)

                        For Each item In rs
                            rr.Add(item.Key, CSng(item.Value))
                        Next
                End Select
            End Using
        End Sub

        'jrs 2009/07/09
        'Loads WHO's Low-Income Model codes,
        'roughly: 0-high income, 1-SSA, 2-other low-income
        Private Sub LoadWHOLIMcodes()
            Using db As IFsContext = New IFsContext()
                If hlmortmodsw > 1 Then 'jrs 2009/04/20
                    ReDim WHOLOWGDPMODEL(NR%)
                    Dim valtbl = (From LG In db.HealthWHOLowGDPModel Join R In db.RegionalizationFull On LG.Country Equals R.CountryName
                                  Join RI In db.RegionIdNewFull On R.RegionId Equals RI.Id
                                  Select Key = RI.Id, Value = LG.Code)

                    For Each item In valtbl
                        'JRS 2008/08/19
                        If item.Value.HasValue Then
                            WHOLOWGDPMODEL(item.Key) = CInt(item.Value)
                        Else
                            WHOLOWGDPMODEL(item.Key) = -1
                        End If
                    Next

                    'JRS 2009/12/04
                ElseIf hlmortmodsw = 1 Then
                    ReDim WHOLOWGDPMODEL4(NR%)

                    Dim valtbl = (From LG In db.HealthWHOLowGDPModel Join R In db.RegionalizationFull On LG.Country Equals R.CountryName
                                  Join RI In db.RegionIdNewFull On R.RegionId Equals RI.Id
                                  Select Key = RI.Id, Value = LG.Code4)

                    For Each item In valtbl
                        'JRS 2008/08/19
                        If item.Value.HasValue Then
                            WHOLOWGDPMODEL4(item.Key) = CInt(item.Value)
                        Else
                            WHOLOWGDPMODEL4(item.Key) = -1
                        End If

                    Next

                End If
            End Using
        End Sub

        'jrs 2009/07/09
        Private Sub InitHistData(Ntypes As Integer)
            Dim R As Integer, j As Integer, k As Integer
            For R = 1 To NR
                For j = 0 To NCohorts
                    For k = 1 To Ntypes - 2 'jrs 2010/02/17 leave mental health as 0
                        modmordstdet(j, R, k) = -1
                    Next k
                    'jrs 2009/08/11
                    '            'jrs 2009/04/07 Diarrhea
                    '            For k = 1 To 2
                    '                mortDiarrhea(j, R%, k) = -1
                    '            Next k
                Next j
            Next R%
        End Sub

        'jrs 2010/02/17
        'First Year: Units will be in Deaths per 1000 people
        '----------------------------------------------------
        Public Sub LoadInitData(ByRef modmordstdet(,,) As Double)

            Dim iGender As Integer '1 = Male, 2 = Female
            Dim iType As Integer 'from 1 to 30, odds for males, evens for females
            Dim R As Integer

            Using db As IFs2Context = New IFs2Context()
                'jrs 2011/09/19 changed from 2k4 to 2k8
                'jrs 2013/08/31 HealthDetailedDeathsRegs2k8
                'jrs 2024/12/03 For Each item In db.HealthDetailedDeathsRegs.Where(Function(g) g.CauseID <> 11)
                For Each item In db.HealthDetailedDeathsRegs
                    iGender = CInt(item.sex)
                    iType = CInt(2 * (item.CauseID - 1) + iGender)
                    R = CInt(item.RegNum)
                    'Load infants and children 1-4
                    modmordstdet(0, R, iType) = CSng(item.R0) 'jrs 2011/09/19 now it's deaths per 1000 * 1000
                    'jrs 2017/03/02 modmordstdet(1, R, iType) = valtbl.Fields("R0") 'jrs 2011/09/19 now it's deaths per 1000 * 1000
                    modmordstdet(1, R, iType) = CSng(item.R1) 'jrs 2011/09/19 now it's deaths per 1000 * 1000
                    'For ages 5 to 84
                    'now For ages 5 to 100+
                    'For j = 2 To 20 'jrs 2019/06/05 used to be to 17
                    '    modmordstdet(j, R, iType) = valtbl.Fields("R" & CStr(j + 1)) 'jrs 2011/09/19 now it's deaths per 1000 * 1000
                    'Next j
                    modmordstdet(2, R, iType) = CSng(item.R3)
                    modmordstdet(3, R, iType) = CSng(item.R4)
                    modmordstdet(4, R, iType) = CSng(item.R5)
                    modmordstdet(5, R, iType) = CSng(item.R6)
                    modmordstdet(6, R, iType) = CSng(item.R7)
                    modmordstdet(7, R, iType) = CSng(item.R8)
                    modmordstdet(8, R, iType) = CSng(item.R9)
                    modmordstdet(9, R, iType) = CSng(item.R10)
                    modmordstdet(10, R, iType) = CSng(item.R11)
                    modmordstdet(11, R, iType) = CSng(item.R12)
                    modmordstdet(12, R, iType) = CSng(item.R13)
                    modmordstdet(13, R, iType) = CSng(item.R14)
                    modmordstdet(14, R, iType) = CSng(item.R15)
                    modmordstdet(15, R, iType) = CSng(item.R16)
                    modmordstdet(16, R, iType) = CSng(item.R17)
                    modmordstdet(17, R, iType) = CSng(item.R18)
                    modmordstdet(18, R, iType) = CSng(item.R19)
                    modmordstdet(19, R, iType) = CSng(item.R20)
                    modmordstdet(20, R, iType) = CSng(item.R21)
                    'jrs 2019/06/05 not necessary anymore
                    'Load 85+
                    'Load 100+
                    For j = 21 To NCohorts 'jrs 2019/06/05 used to start at 18
                        'jrs 2019/06/05 modmordstdet(j, R, iType) = valtbl.Fields("R19") 'jrs 2011/09/19 now it's deaths per 1000 * 1000
                        modmordstdet(j, R, iType) = CSng(item.R21)
                    Next j
                Next
            End Using



            'Add up MH to ONCDs and set MH to 0
            For R = 1 To NR
                For j = 0 To NCohorts
                    For iGender = 1 To 2
                        modmordstdet(j, R, 10 + iGender) = modmordstdet(j, R, 10 + iGender) + modmordstdet(j, R, 28 + iGender)
                        'modmordstdet(j, R, 28 + iGender) = 0
                    Next iGender
                Next j
            Next R

            'jrs 2018/03/07
            'Add up Diarrhea (23-24), Malaria (25-26) and Resp Infections (27, 28) to OCDs (1-2)
            Dim SumVal As Double?
            For R = 1 To NR
                For j = 0 To NCohorts
                    For iGender = 1 To 2
                        SumVal = Nothing
                        If modmordstdet(j, R, iGender) >= 0 Then
                            SumVal = modmordstdet(j, R, iGender)
                            If modmordstdet(j, R, 22 + iGender) >= 0 Then
                                SumVal = SumVal + modmordstdet(j, R, 22 + iGender)
                            End If
                            If modmordstdet(j, R, 24 + iGender) >= 0 Then
                                SumVal = SumVal + modmordstdet(j, R, 24 + iGender)
                            End If
                            If modmordstdet(j, R, 26 + iGender) >= 0 Then
                                SumVal = SumVal + modmordstdet(j, R, 26 + iGender)
                            End If
                        Else
                            If modmordstdet(j, R, 22 + iGender) >= 0 Then
                                SumVal = modmordstdet(j, R, 22 + iGender)
                                If modmordstdet(j, R, 24 + iGender) >= 0 Then
                                    SumVal = SumVal + modmordstdet(j, R, 24 + iGender)
                                End If
                                If modmordstdet(j, R, 26 + iGender) >= 0 Then
                                    SumVal = SumVal + modmordstdet(j, R, 26 + iGender)
                                End If
                            Else
                                If modmordstdet(j, R, 24 + iGender) >= 0 Then
                                    SumVal = modmordstdet(j, R, 24 + iGender)
                                    If modmordstdet(j, R, 26 + iGender) >= 0 Then
                                        SumVal = SumVal + modmordstdet(j, R, 26 + iGender)
                                    End If
                                Else
                                    If modmordstdet(j, R, 26 + iGender) >= 0 Then
                                        SumVal = modmordstdet(j, R, 26 + iGender)
                                    End If
                                End If
                            End If
                        End If
                        If SumVal.HasValue Then
                            modmordstdet(j, R, iGender) = CSng(SumVal)
                        End If
                    Next iGender
                Next j
            Next R

        End Sub

        'jrs 2024/12/03
        Private Sub ComputeMaleAIDSPortion(ByRef MaleAIDSPortion() As Single, context As IForecastContext)
            Dim R As Integer, DEATHSM As Single, DEATHS As Single
            Dim g As Integer, j As Integer
            For R = 1 To NR
                DEATHS = 0
                DEATHSM = 0
                For g = 1 To 2
                    For j = 0 To NCohorts
                        DEATHS = DEATHS + CSng(agedst(j, R, g) * modmordstdet(j, R, 20 + g) / 1000)
                        If g = 1 Then
                            DEATHSM = DEATHSM + CSng(agedst(j, R, g) * modmordstdet(j, R, 20 + g) / 1000)
                        End If
                    Next
                Next
                Dim Portion As Single
                Call XYTABL("GDP/Capita (PPP) Versus HIV Infections, Male Portion",
                    GDPPCP(R), Portion, context)
                If DEATHS > 0 Then
                    MaleAIDSPortion(R) = (DEATHSM / DEATHS) / Portion
                Else
                    MaleAIDSPortion(R) = 1
                End If
            Next
        End Sub

        'jrs 2009/07/09
        Private Sub HL_LoadAIDSData(context As IForecastContext)
            Dim R As Integer, j As Integer, k As Integer
            Dim maleaidsdths, femaleaidsdths, aidsdthscoh As Double
            Dim MaleAIDSPortion As Single
            For R = 1 To NR
                Call XYTABL("GDP/Capita (PPP) Versus HIV Infections, Male Portion",
                GDPPCP(R), MaleAIDSPortion, context)
                maleaidsdths = AIDSDTHS(R) * MaleAIDSPortion * MaleAIDSPortionI(R)
                femaleaidsdths = AIDSDTHS(R) * (1 - MaleAIDSPortion * MaleAIDSPortionI(R))
                For j = 0 To NCohorts
                    For k = 21 To 22
                        If k = 21 Then
                            If agedst(j, R, 1) <> 0 Then
                                'jrs 2007/08/14 using Heavy Aids Vector
                                aidsdthscoh = maleaidsdths * aidsdeathsbyageHeavy(j, R, 1) / agedst(j, R, 1)
                            Else
                                aidsdthscoh = 0
                            End If
                        Else
                            If agedst(j, R, 2) <> 0 Then
                                'jrs 2007/08/14 using Heavy Aids Vector
                                aidsdthscoh = femaleaidsdths * aidsdeathsbyageHeavy(j, R, 2) / agedst(j, R, 2)
                            Else
                                aidsdthscoh = 0
                            End If
                        End If
                        modmordstdet(j, R, k) = aidsdthscoh * 1000
                    Next k
                Next j
            Next R
        End Sub

        'jrs 2009/08/18
        Private Sub LoadMorbidityDetail(modmorbdstdet(,,) As Double)

            Dim R, j As Integer

            Using db As IFs2Context = New IFs2Context()

                For Each valtbl In db.HealthYLDComMatPerCohortRegionMale
                    R = valtbl.RegionId
                    For Each coh In valtbl.Cohorts
                        j = CInt(coh.Key.Replace("Cohort", ""))
                        modmorbdstdet(j, R, 1) = coh.Value
                        If j = NCohorts Then Exit For
                    Next
                Next

                For Each valtbl In db.HealthYLDComMatPerCohortRegionFemale
                    R = valtbl.RegionId
                    For Each coh In valtbl.Cohorts
                        j = CInt(coh.Key.Replace("Cohort", ""))
                        modmorbdstdet(j, R, 2) = coh.Value
                        If j = NCohorts Then Exit For
                    Next
                Next

                For Each valtbl In db.HealthYLDNonComDisCohortRegionMale
                    R = valtbl.RegionId
                    For Each coh In valtbl.Cohorts
                        j = CInt(coh.Key.Replace("Cohort", ""))
                        modmorbdstdet(j, R, 3) = coh.Value
                        If j = NCohorts Then Exit For
                    Next
                Next

                For Each valtbl In db.HealthYLDNonComDisCohortRegionFemale
                    R = valtbl.RegionId
                    For Each coh In valtbl.Cohorts
                        j = CInt(coh.Key.Replace("Cohort", ""))
                        modmorbdstdet(j, R, 4) = coh.Value
                        If j = NCohorts Then Exit For
                    Next
                Next

                For Each valtbl In db.HealthYLDInjuriesCohortRegionMale
                    R = valtbl.RegionId
                    For Each coh In valtbl.Cohorts
                        j = CInt(coh.Key.Replace("Cohort", ""))
                        modmorbdstdet(j, R, 5) = coh.Value
                        If j = NCohorts Then Exit For
                    Next
                Next

                For Each valtbl In db.HealthYLDInjuriesCohortRegionFemale
                    R = valtbl.RegionId
                    For Each coh In valtbl.Cohorts
                        j = CInt(coh.Key.Replace("Cohort", ""))
                        modmorbdstdet(j, R, 6) = coh.Value
                        If j = NCohorts Then Exit For
                    Next
                Next

            End Using

        End Sub

        'jrs 2009/06/04
        Private Sub LoadYLDtoYLLratio(ByRef YLDtoYLLratio(,,) As Single)
            Using db As IFs2Context = New IFs2Context()
                For Each valtbl In db.HealthRatioYLDYLLRegion
                    YLDtoYLLratio(valtbl.RegionId, valtbl.Code, valtbl.Gender) =
                    CSng(valtbl.RatioYLDYLL)
                Next
            End Using
        End Sub

        'jrs 2009/08/10
        Private Sub LoadYLDtoPOPratio(ByRef YLDtoPOPratio(,) As Single)
            Using db As IFs2Context = New IFs2Context()
                'Cohort -1 holds the ratio for all ages
                For Each valtbl In db.HealthMentalHealthReg.Where(Function(g) g.Cohort = -1)
                    YLDtoPOPratio(valtbl.RegionId, valtbl.Gender) =
                    CSng(valtbl.YLDtoPOP)
                Next
            End Using

        End Sub

        'jrs 2009/08/25
        Private Sub LoadMortFactors(ByRef mortFactors(,,,) As Double)
            Using db As IFsContext = New IFsContext()
                For Each valtbl In db.HealthMortFactorCommDisMale
                    mortFactors(valtbl.Cohort, 1, 1, valtbl.IncomeType) = valtbl.Constant
                    mortFactors(valtbl.Cohort, 1, 2, valtbl.IncomeType) = valtbl.lnY
                    mortFactors(valtbl.Cohort, 1, 3, valtbl.IncomeType) = valtbl.lnHC
                    mortFactors(valtbl.Cohort, 1, 4, valtbl.IncomeType) = valtbl.lnY2
                    mortFactors(valtbl.Cohort, 1, 5, valtbl.IncomeType) = valtbl.Year
                    mortFactors(valtbl.Cohort, 1, 6, valtbl.IncomeType) = valtbl.lnSI
                Next

                For Each valtbl In db.HealthMortFactorCommDisFemale
                    mortFactors(valtbl.Cohort, 2, 1, valtbl.IncomeType) = valtbl.Constant
                    mortFactors(valtbl.Cohort, 2, 2, valtbl.IncomeType) = valtbl.lnY
                    mortFactors(valtbl.Cohort, 2, 3, valtbl.IncomeType) = valtbl.lnHC
                    mortFactors(valtbl.Cohort, 2, 4, valtbl.IncomeType) = valtbl.lnY2
                    mortFactors(valtbl.Cohort, 2, 5, valtbl.IncomeType) = valtbl.Year
                    mortFactors(valtbl.Cohort, 2, 6, valtbl.IncomeType) = valtbl.lnSI
                Next

                For Each valtbl In db.HealthMortFactorNonCommDisMale
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2 - 1), 1, valtbl.IncomeType) = valtbl.Constant
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2 - 1), 2, valtbl.IncomeType) = valtbl.lnY
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2 - 1), 3, valtbl.IncomeType) = valtbl.lnHC
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2 - 1), 4, valtbl.IncomeType) = valtbl.lnY2
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2 - 1), 5, valtbl.IncomeType) = valtbl.Year
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2 - 1), 6, valtbl.IncomeType) = valtbl.lnSI
                Next

                For Each valtbl In db.HealthMortFactorNonCommDisFemale
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2), 1, valtbl.IncomeType) = CSng(valtbl.Constant)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2), 2, valtbl.IncomeType) = CSng(valtbl.lnY)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2), 3, valtbl.IncomeType) = CSng(valtbl.lnHC)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2), 4, valtbl.IncomeType) = CSng(valtbl.lnY2)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2), 5, valtbl.IncomeType) = CSng(valtbl.Year)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2), 6, valtbl.IncomeType) = CSng(valtbl.lnSI)
                Next

                For Each valtbl In db.HealthMortFactorInjuryMale
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2 - 1), 1, valtbl.IncomeType) = CSng(valtbl.Constant)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2 - 1), 2, valtbl.IncomeType) = CSng(valtbl.lnY)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2 - 1), 3, valtbl.IncomeType) = CSng(valtbl.lnHC)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2 - 1), 4, valtbl.IncomeType) = CSng(valtbl.lnY2)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2 - 1), 5, valtbl.IncomeType) = CSng(valtbl.Year)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2 - 1), 6, valtbl.IncomeType) = CSng(valtbl.lnSI)
                Next

                For Each valtbl In db.HealthMortFactorInjuryFemale
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2), 1, valtbl.IncomeType) = CSng(valtbl.Constant)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2), 2, valtbl.IncomeType) = CSng(valtbl.lnY)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2), 3, valtbl.IncomeType) = CSng(valtbl.lnHC)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2), 4, valtbl.IncomeType) = CSng(valtbl.lnY2)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2), 5, valtbl.IncomeType) = CSng(valtbl.Year)
                    mortFactors(valtbl.Cohort, (valtbl.Group * 2), 6, valtbl.IncomeType) = CSng(valtbl.lnSI)
                Next
            End Using
        End Sub

        'jrs 2009/09/15
        Private Sub LoadOthrMortFactors(ByRef sird(,,) As Single)
            'jrs 2009/03/30
            'Load Infection factors: for Diarrhea and Malaria
            '------------------------------------------------
            'mortFactorsInf(Cohort, gender, constant) = Beta
            '1 is constant, 2 is Beta
            'Males: 0 to 4
            For cc = 0 To 1
                mortFactorsInf(cc, 1, 1) = -4.787
                mortFactorsInf(cc, 1, 2) = 1.493
            Next cc
            'Males: 5 to 14
            For cc = 2 To 3
                mortFactorsInf(cc, 1, 1) = -0.728
                mortFactorsInf(cc, 1, 2) = 0.997
            Next cc
            'Males: 15 to 29
            For cc = 4 To 6
                mortFactorsInf(cc, 1, 1) = -0.872
                mortFactorsInf(cc, 1, 2) = 1.126
            Next cc
            'Males: 30 to 44
            For cc = 7 To 9
                mortFactorsInf(cc, 1, 1) = -0.846
                mortFactorsInf(cc, 1, 2) = 1.117
            Next cc
            'Males: 45 to 59
            For cc = 10 To 12
                mortFactorsInf(cc, 1, 1) = -0.991
                mortFactorsInf(cc, 1, 2) = 1.098
            Next cc
            'Males: 60 to 69
            For cc = 13 To 14
                mortFactorsInf(cc, 1, 1) = -1.242
                mortFactorsInf(cc, 1, 2) = 1.088
            Next cc
            'Males: 70+
            For cc = 15 To NCohorts
                mortFactorsInf(cc, 1, 1) = -0.752
                mortFactorsInf(cc, 1, 2) = 0.887
            Next cc
            'Females: 0 to 4
            For cc = 0 To 1
                mortFactorsInf(cc, 2, 1) = -4.5
                mortFactorsInf(cc, 2, 2) = 1.473
            Next cc
            'Females: 5 to 14
            For cc = 2 To 3
                mortFactorsInf(cc, 2, 1) = -0.787
                mortFactorsInf(cc, 2, 2) = 1.001
            Next cc
            'Females: 15 to 29
            For cc = 4 To 6
                mortFactorsInf(cc, 2, 1) = -0.894
                mortFactorsInf(cc, 2, 2) = 0.996
            Next cc
            'Females: 30 to 44
            For cc = 7 To 9
                mortFactorsInf(cc, 2, 1) = -0.853
                mortFactorsInf(cc, 2, 2) = 1.005
            Next cc
            'Females: 45 to 59
            For cc = 10 To 12
                mortFactorsInf(cc, 2, 1) = -0.874
                mortFactorsInf(cc, 2, 2) = 1.063
            Next cc
            'Females: 60 to 69
            For cc = 13 To 14
                mortFactorsInf(cc, 2, 1) = -0.968
                mortFactorsInf(cc, 2, 2) = 1.02
            Next cc
            'Females: 70+
            For cc = 15 To NCohorts
                mortFactorsInf(cc, 2, 1) = -1.028
                mortFactorsInf(cc, 2, 2) = 0.899
            Next cc

            'Load Resp. Infection factors
            '---------------------------------
            'mortFactorsInf(Cohort, gender, constant) = Beta
            '1 is constant, 2 is Beta
            'Males: 0 to 4
            For cc = 0 To 1
                mortFactorsRespInf(cc, 1, 1) = -4.625
                mortFactorsRespInf(cc, 1, 2) = 1.476
            Next cc
            'Males: 5 to 14
            For cc = 2 To 3
                mortFactorsRespInf(cc, 1, 1) = -0.808
                mortFactorsRespInf(cc, 1, 2) = 0.936
            Next cc
            'Males: 15 to 29
            For cc = 4 To 6
                mortFactorsRespInf(cc, 1, 1) = -0.644
                mortFactorsRespInf(cc, 1, 2) = 0.763
            Next cc
            'Males: 30 to 44
            For cc = 7 To 9
                mortFactorsRespInf(cc, 1, 1) = -0.485
                mortFactorsRespInf(cc, 1, 2) = 0.741
            Next cc
            'Males: 45 to 59
            For cc = 10 To 12
                mortFactorsRespInf(cc, 1, 1) = -0.354
                mortFactorsRespInf(cc, 1, 2) = 0.826
            Next cc
            'Males: 60 to 69
            For cc = 13 To 14
                mortFactorsRespInf(cc, 1, 1) = -0.018
                mortFactorsRespInf(cc, 1, 2) = 0.834
            Next cc
            'Males: 70+
            For cc = 15 To NCohorts
                mortFactorsRespInf(cc, 1, 1) = -0.292
                mortFactorsRespInf(cc, 1, 2) = 0.972
            Next cc
            'Females: 0 to 4
            For cc = 0 To 1
                mortFactorsRespInf(cc, 2, 1) = -4.489
                mortFactorsRespInf(cc, 2, 2) = 1.484
            Next cc
            'Females: 5 to 14
            For cc = 2 To 3
                mortFactorsRespInf(cc, 2, 1) = -0.744
                mortFactorsRespInf(cc, 2, 2) = 0.927
            Next cc
            'Females: 15 to 29
            For cc = 4 To 6
                mortFactorsRespInf(cc, 2, 1) = -0.909
                mortFactorsRespInf(cc, 2, 2) = 0.741
            Next cc
            'Females: 30 to 44
            For cc = 7 To 9
                mortFactorsRespInf(cc, 2, 1) = -0.635
                mortFactorsRespInf(cc, 2, 2) = 0.699
            Next cc
            'Females: 45 to 59
            For cc = 10 To 12
                mortFactorsRespInf(cc, 2, 1) = -0.455
                mortFactorsRespInf(cc, 2, 2) = 0.82
            Next cc
            'Females: 60 to 69
            For cc = 13 To 14
                mortFactorsRespInf(cc, 2, 1) = -0.351
                mortFactorsRespInf(cc, 2, 2) = 0.893
            Next cc
            'Females: 70+
            For cc = 15 To NCohorts
                mortFactorsRespInf(cc, 2, 1) = -0.237
                mortFactorsRespInf(cc, 2, 2) = 0.97
            Next cc

            'jrs 2009/05/12
            'First Year: Respiratory Factors
            'sird: smoking impact ratio denominator
            'China males
            sird(1, 1, 1) = 13.56400681
            sird(1, 2, 1) = 453.2135925
            sird(1, 3, 1) = 2479.453125
            sird(1, 4, 1) = 4474.043945
            'China females
            sird(1, 1, 2) = 16.22751045
            sird(1, 2, 2) = 240.6329498
            sird(1, 3, 2) = 834.9159546
            sird(1, 4, 2) = 984.8682251
            'World Males
            sird(2, 1, 1) = 7.179250717
            sird(2, 2, 1) = 116.9517517
            sird(2, 3, 1) = 479.2998047
            sird(2, 4, 1) = 996.305603
            'World Females
            sird(2, 1, 2) = 7.795292377
            sird(2, 2, 2) = 74.83332825
            sird(2, 3, 2) = 230.01297
            sird(2, 4, 2) = 352.4757385
            'Seard Males
            sird(3, 1, 1) = 7.306945801
            sird(3, 2, 1) = 123.6769867
            sird(3, 3, 1) = 519.3028564
            sird(3, 4, 1) = 1062.222778
            'Seard Females
            sird(3, 1, 2) = 7.963936806
            sird(3, 2, 2) = 78.14931488
            sird(3, 3, 2) = 242.1110382
            sird(3, 4, 2) = 363.5887756

        End Sub

        'jrs 2009/10/26
        Private Sub Load5yrDistributions(ByRef LowMortLimit(,,) As Double, context As IForecastContext)
            'Data is stored in ifs.mdb, HealthDetailed5YearsDeathsRegs
            'Not all countries have data
            'Not all countries with data have all types of disease
            'Some distributions go to 70+ -> format 5
            '75+ -> format 4 and 3
            '85+ -> format 2 and 1
            '95+ -> format 0

            'Load proxy for minimum growth within j-curve
            ReDim LowMortLimit(NCohorts, 15, 2)
            Dim T As Integer, G As Integer, j As Integer
            Dim DeathPop As KeyValuePair(Of Integer?, Integer?)
            'jrs 2010/02/08
            Dim SwedenIdx As Integer
            SwedenIdx = (From R In context.GetDimension(1).Buckets.Where(Function(m) m.Value = "Sweden")).Single().Key

            Using db As IFsContext = New IFsContext()
                'Sweden as a limit on low mortality
                For Each rs5Yr In db.HealthDetailed5YearsDeathsRegs.Where(Function(m) m.RegionName = "Sweden")
                    G = rs5Yr.Sex
                    T = rs5Yr.TypeId

                    LowMortLimit(0, T, G) = CSng(rs5Yr.Deaths2 / rs5Yr.Pop2)
                    LowMortLimit(1, T, G) = CSng(rs5Yr.Deaths3 / rs5Yr.Pop3)

                    For j = 2 To NCohorts - 1
                        DeathPop = rs5Yr.DeathPop(j + 5)
                        'jrs 2012/09/01
                        If DeathPop.Value > 0 Then
                            'jrs 2012/09/03
                            LowMortLimit(j, T, G) = CSng(DeathPop.Key / DeathPop.Value * 1000)
                        End If
                    Next j

                    'jrs 2010/02/08
                    'Estimate 100+ category
                    If modmordst(NCohorts - 1, SwedenIdx, G) > 0 Then
                        LowMortLimit(NCohorts, T, G) = LowMortLimit(NCohorts - 1, T, G) * modmordst(NCohorts, SwedenIdx, G) / modmordst(NCohorts - 1, SwedenIdx, G)
                    End If

                    'use type 15 for totals, since mental health is 0
                    For j = 0 To NCohorts - 1
                        LowMortLimit(j, 15, G) = LowMortLimit(j, 15, G) + LowMortLimit(j, T, G)
                    Next j
                Next
            End Using
        End Sub

        'jrs 2009/10/30
        'Procedure to adjust J-curve in case is not monotonically increasing
        'like cancer for females in Russia, or males in US
        'the intention is to use a proxy, Sweeden, to limit the minimum growth to be allowed in J-curve
        'use only non communicable diseases (cronic)
        Private Sub AdjustJcurveShape(modmordstdet(,,) As Double, LowMortLimit(,,) As Double, Ntypes As Integer, gender As Integer, agedst(,,) As Double)
            'for each country
            Dim R As Integer, H1 As Integer, k As Integer, j As Integer
            Dim H1val As Single
            Dim Growth As Single, ProxyGrowth(,) As Single, TotGrowth() As Single ', TotProxyGrowth As Single
            Dim Adj As Single
            Dim kk As Integer, kkH1 As Integer
            Dim newMort As Single
            Dim MIN As Single, Max As Single
            Dim bReAdjust As Boolean, iFstAge As Integer
            iFstAge = 10
            Dim GrowthH1 As Single, GrowthH1b As Single, GrowthH1c As Single
            Dim bPop2Check As Boolean
            Dim bMort2Check As Boolean
            'jrs 2010/01/22
            'First Saturated Point
            Dim FstSatPt() As Integer
            Dim TotMort() As Single
            Dim jr As Integer

            For R = 1 To NR
                bReAdjust = False
                'jrs 2010/01/22
                ReDim FstSatPt(14)
                ReDim TotMort(NCohorts)
                ReDim TotGrowth(NCohorts)
                ReDim ProxyGrowth(NCohorts, 14)
                'look for type H1 with highest mortality (to be used to reduce increases in other types)
                H1val = CSng(modmordstdet(NCohorts, R, gender + 18)) 'diabetes
                H1 = gender + 18
                For k = gender + 2 To 12 Step 2 '-2 because not including mental health
                    If H1val < modmordstdet(NCohorts, R, k) Then
                        H1val = CSng(modmordstdet(NCohorts, R, k))
                        H1 = k
                    End If
                Next k

                'jrs 2010/01/21 replace kk with kkH1
                If gender = 2 Then
                    kkH1 = CInt(H1 / 2)
                Else
                    kkH1 = CInt((H1 + 1) / 2)
                End If

                'jrs 2010/01/22
                'for each age, 45+
                'FstAge = 10
                For j = iFstAge - 1 To NCohorts
                    'compute Total Mortality
                    For k = gender To Ntypes - 2 Step 2
                        TotMort(j) = TotMort(j) + CSng(modmordstdet(j, R, k))
                        'compute Proxy growth
                        If gender = 2 Then
                            kk = CInt(k / 2)
                        Else
                            kk = CInt((k + 1) / 2)
                        End If
                        If LowMortLimit(j - 1, kk, gender) > 0 Then
                            ProxyGrowth(j - 1, kk) = CSng((LowMortLimit(j, kk, gender) - LowMortLimit(j - 1, kk, gender)) / LowMortLimit(j - 1, kk, gender))
                        Else
                            ProxyGrowth(j - 1, kk) = 1000000.0#
                        End If
                    Next k

                    'compute Total growth
                    If j > iFstAge - 1 Then
                        If TotMort(j - 1) = 0 And TotMort(j) = 0 Then
                            TotGrowth(j - 1) = 0
                        ElseIf TotMort(j - 1) = 0 Then
                            TotGrowth(j - 1) = 1000000.0#
                        Else
                            TotGrowth(j - 1) = (TotMort(j) - TotMort(j - 1)) / TotMort(j - 1)
                        End If

                        'check that Proxy Growth is not too big (max is 25% of TotMort)
                        For kk = 1 To 14
                            If ProxyGrowth(j - 1, kk) > 0.25 * TotGrowth(j - 1) Then
                                ProxyGrowth(j - 1, kk) = CSng(0.25 * TotGrowth(j - 1))
                            End If
                        Next kk
                    End If
                Next j

                'for each age, 45+
                'FstAge = 10
                For j = iFstAge To NCohorts - 1
                    'for each type (except H1) -> Non Comm. Dis
                    For k = gender + 2 To 20 Step 2
                        If k <> H1 And Not (k > 12 And k < 19) Then
                            If gender = 2 Then
                                kk = CInt(k / 2)
                            Else
                                kk = CInt((k + 1) / 2)
                            End If
                            'when there's mortality
                            If modmordstdet(j, R, k) > 0.000000001 And LowMortLimit(j, kk, gender) > 0.000000001 And modmordstdet(j, R, H1) > 0.000000001 Then
                                'find growth from one age to the next
                                Growth = CSng((modmordstdet(j + 1, R, k) - modmordstdet(j, R, k)) / modmordstdet(j, R, k))
                                'if growth is smaller then adjust type and remove adjustment from H1
                                'jrs 2009/12/04 added a check for population in age j+1
                                If Growth < ProxyGrowth(j, kk) And agedst(j + 1, R, gender) > 0 Then
                                    'find min current (j) mort
                                    MIN = CSng(modmordstdet(j - 1, R, k) * (1 + ProxyGrowth(j - 1, kk)))
                                    'find max current (j) mort
                                    'max = modmordstdet(j + 1, R, k) / (1 + ProxyGrowth)
                                    'considering that modmordstdet(j + 1, R, k) will change by Adj *agedst(j)/agedst(j+1)
                                    'Adj = modmordstdet(j , R, k) - max, we find
                                    'max = (modmordstdet(j + 1, R, k) + (modmordstdet(j , R, k) - max)*agedst(j)/agedst(j+1)) / (1 + ProxyGrowth)
                                    'max (1 + ProxyGrowth) + max (agedst(j)/agedst(j+1)) = modmordstdet(j + 1, R, k) + (modmordstdet(j , R, k) * agedst(j)/agedst(j+1)
                                    Max = CSng((modmordstdet(j + 1, R, k) + modmordstdet(j, R, k) * agedst(j, R, gender) / agedst(j + 1, R, gender)) / (1 + ProxyGrowth(j, kk) + agedst(j, R, gender) / agedst(j + 1, R, gender)))

                                    If MIN <= Max Then
                                        newMort = Max
                                        If FstSatPt(kk) = 0 Then
                                            FstSatPt(kk) = j
                                        End If
                                    Else
                                        'jrs 2010/01/22
                                        'if there's room, not all points are saturated then try going backwards
                                        If (FstSatPt(kk) > 11 Or FstSatPt(kk) = 0) And j > 11 Then
                                            newMort = Max
                                        Else
                                            newMort = MIN
                                        End If
                                    End If

                                    Adj = CSng(modmordstdet(j, R, k) - newMort)
                                    'check if adjustment fits in max category (H1)
                                    GrowthH1 = CSng((modmordstdet(j + 1, R, H1) - Adj * agedst(j, R, gender) / agedst(j + 1, R, gender) - (modmordstdet(j, R, H1) + Adj)) / (modmordstdet(j, R, H1) + Adj))

                                    'jrs 2010/02/05
                                    bMort2Check = (j > 19)
                                    If Not bMort2Check Then
                                        'checking not to introduce a biggger drop than we already have
                                        bMort2Check = CBool((modmordstdet(j + 1, R, k) + Adj * agedst(j, R, gender) / agedst(j + 1, R, gender) <= modmordstdet(j + 2, R, k) / AMAX((1 + Amin(Growth, ProxyGrowth(j + 1, kk))), 0.000001)))
                                    End If

                                    'jrs 2009/12/04 added newMort <= 1000
                                    'if adjustment fits in H1 then save it
                                    If GrowthH1 >= ProxyGrowth(j, kkH1) And newMort <= 750 And
                            modmordstdet(j + 1, R, k) + Adj * agedst(j, R, gender) / agedst(j + 1, R, gender) <= 750 And bMort2Check And
                            modmordstdet(j + 1, R, k) + Adj * agedst(j, R, gender) / agedst(j + 1, R, gender) >= 0 And
                            modmordstdet(j + 1, R, k) + Adj * agedst(j, R, gender) / agedst(j + 1, R, gender) >= newMort * (1 + Growth) And
                            newMort >= 0 And
                            newMort >= modmordstdet(j - 1, R, k) * (1 + Amin(Growth, ProxyGrowth(j - 1, kk))) And
                            modmordstdet(j, R, H1) + Adj <= 750 Then
                                        modmordstdet(j, R, k) = newMort
                                        modmordstdet(j + 1, R, k) = modmordstdet(j + 1, R, k) + Adj * agedst(j, R, gender) / agedst(j + 1, R, gender)

                                        modmordstdet(j, R, H1) = modmordstdet(j, R, H1) + Adj
                                        modmordstdet(j + 1, R, H1) = modmordstdet(j + 1, R, H1) - Adj * agedst(j, R, gender) / agedst(j + 1, R, gender)

                                        If MIN > Max Then 'adjustment might not be enough, need to check again
                                            'jrs 2010/01/22
                                            'if there's no room, keep going forward
                                            If (FstSatPt(kk) <= 11 And FstSatPt(kk) > 0) Or j <= 11 Then
                                                Growth = CSng((modmordstdet(j + 1, R, k) - modmordstdet(j, R, k)) / modmordstdet(j, R, k))
                                                If Growth < ProxyGrowth(j, kk) Then
                                                    newMort = CSng(modmordstdet(j, R, k) * (1 + ProxyGrowth(j, kk)))
                                                    Adj = CSng(newMort - modmordstdet(j + 1, R, k))
                                                    'check if adjustment fits in max category (H1)
                                                    GrowthH1 = CSng((modmordstdet(j + 1, R, H1) - Adj - modmordstdet(j, R, H1)) / modmordstdet(j, R, H1))
                                                    'jrs 2009/12/04 check Pop in age j+2 if necessary
                                                    If j < 20 Then
                                                        bPop2Check = (agedst(j + 2, R, gender) > 0)
                                                        'jrs 2009/12/22
                                                        If bPop2Check Then
                                                            bMort2Check = (modmordstdet(j + 2, R, H1) + Adj * agedst(j + 1, R, gender) / agedst(j + 2, R, gender) <= 750)
                                                            'jrs 2010/01/22
                                                            If bMort2Check Then
                                                                'jrs 2010/02/05
                                                                If j < 19 Then
                                                                    bMort2Check = CBool((modmordstdet(j + 2, R, H1) + Adj * agedst(j + 1, R, gender) / agedst(j + 2, R, gender) <= modmordstdet(j + 3, R, H1) / AMAX((1 + ProxyGrowth(j + 2, kkH1)), 0.000001)))
                                                                End If
                                                                If bMort2Check Then
                                                                    'check also type k to stay above 0
                                                                    bMort2Check = (modmordstdet(j + 2, R, k) - Adj * agedst(j + 1, R, gender) / agedst(j + 2, R, gender)) >= 0
                                                                    'jrs 2010/02/05 also not to make the drop worse
                                                                    If bMort2Check Then
                                                                        bMort2Check = (modmordstdet(j + 2, R, k) - Adj * agedst(j + 1, R, gender) / agedst(j + 2, R, gender)) >= modmordstdet(j + 1, R, k) * (1 + Amin(Growth, ProxyGrowth(j + 1, kk)))
                                                                    End If
                                                                End If
                                                            End If
                                                        Else
                                                            bMort2Check = False
                                                        End If
                                                    Else
                                                        bPop2Check = True
                                                        bMort2Check = (modmordstdet(iFstAge, R, H1) + Adj * agedst(j + 1, R, gender) / agedst(iFstAge, R, gender) <= 750)
                                                        'jrs 2010/01/22
                                                        If bMort2Check Then
                                                            'jrs 2010/02/05
                                                            bMort2Check = CBool((modmordstdet(iFstAge, R, H1) + Adj * agedst(j + 1, R, gender) / agedst(iFstAge, R, gender) <= modmordstdet(iFstAge + 1, R, H1) / AMAX((1 + ProxyGrowth(iFstAge, kkH1)), 0.000001)))
                                                            If bMort2Check Then
                                                                bMort2Check = (modmordstdet(iFstAge, R, k) - Adj * agedst(j + 1, R, gender) / agedst(iFstAge, R, gender) >= 0)
                                                                'jrs 2010/02/05 also not to make the drop worse
                                                                If bMort2Check Then
                                                                    bMort2Check = (modmordstdet(iFstAge, R, k) - Adj * agedst(j + 1, R, gender) / agedst(iFstAge, R, gender) >= modmordstdet(iFstAge - 1, R, k) * (1 + Amin(Growth, 0)))
                                                                End If
                                                            End If
                                                        End If
                                                    End If

                                                    'jrs 2009/12/04 added newMort <= 1000
                                                    'if adjustment fits in H1 then continue
                                                    If GrowthH1 >= ProxyGrowth(j, kkH1) And bPop2Check And newMort <= 750 And bMort2Check Then
                                                        modmordstdet(j + 1, R, k) = newMort
                                                        'Tilt next segment to keep constant # of deaths
                                                        'if working in last cohort remove excess from infants
                                                        If j < 20 Then
                                                            modmordstdet(j + 2, R, k) = modmordstdet(j + 2, R, k) - Adj * agedst(j + 1, R, gender) / agedst(j + 2, R, gender)
                                                        Else
                                                            modmordstdet(iFstAge, R, k) = modmordstdet(iFstAge, R, k) - Adj * agedst(j + 1, R, gender) / agedst(iFstAge, R, gender)
                                                            bReAdjust = True
                                                        End If
                                                        'adjust H1
                                                        modmordstdet(j + 1, R, H1) = modmordstdet(j + 1, R, H1) - Adj
                                                        'tilt keeping # of deaths constant
                                                        'if last cohort use infants to keep deaths constant
                                                        If j < 20 Then
                                                            modmordstdet(j + 2, R, H1) = modmordstdet(j + 2, R, H1) + Adj * agedst(j + 1, R, gender) / agedst(j + 2, R, gender)
                                                        Else
                                                            modmordstdet(iFstAge, R, H1) = modmordstdet(iFstAge, R, H1) + Adj * agedst(j + 1, R, gender) / agedst(iFstAge, R, gender)
                                                        End If
                                                    Else
                                                        bReAdjust = True
                                                    End If 'if there's room in H1 (GrowthH1 >= ProxyGrowthH1)
                                                End If 'if Growth < ProxyGrowth
                                            Else 'if there's room, start moving backwards (If FstSatPt(kk) = 11 Then else)
                                                For jr = j - 1 To 11 Step -1
                                                    'find growth from one age to the next
                                                    Growth = CSng((modmordstdet(jr + 1, R, k) - modmordstdet(jr, R, k)) / modmordstdet(jr, R, k))
                                                    'if growth is smaller then adjust type and remove adjustment from H1
                                                    If Growth < ProxyGrowth(jr, kk) And agedst(jr - 1, R, gender) > 0 Then
                                                        MIN = CSng((modmordstdet(jr - 1, R, k) + modmordstdet(jr, R, k) * agedst(jr, R, gender) / agedst(jr - 1, R, gender)) / (1 / (1 + ProxyGrowth(jr - 1, kk)) + agedst(jr, R, gender) / agedst(jr - 1, R, gender)))
                                                        Max = CSng(modmordstdet(jr + 1, R, k) / (1 + ProxyGrowth(jr, kk)))

                                                        newMort = Max
                                                        If MIN <= Max Then
                                                            If FstSatPt(kk) = 0 Or FstSatPt(kk) > jr Then
                                                                FstSatPt(kk) = jr
                                                            End If
                                                        End If

                                                        Adj = CSng(modmordstdet(jr, R, k) - newMort)
                                                        'check if adjustment fits in max category (H1)
                                                        'jrs 2010/04/12 check divisions by 0
                                                        If modmordstdet(jr, R, H1) + Adj <> 0 Then
                                                            GrowthH1 = CSng((modmordstdet(jr + 1, R, H1) - (modmordstdet(jr, R, H1) + Adj)) / (modmordstdet(jr, R, H1) + Adj))
                                                        Else
                                                            If modmordstdet(jr + 1, R, H1) > (modmordstdet(jr, R, H1) + Adj) Then
                                                                GrowthH1 = 100
                                                            Else
                                                                GrowthH1 = -100
                                                            End If
                                                        End If
                                                        If (modmordstdet(jr - 1, R, H1) - Adj * agedst(jr, R, gender) / agedst(jr - 1, R, gender)) <> 0 Then
                                                            'jrs 2019/08/27 added + Adj to first term
                                                            GrowthH1b = CSng((modmordstdet(jr, R, H1) + Adj - (modmordstdet(jr - 1, R, H1) - Adj * agedst(jr, R, gender) / agedst(jr - 1, R, gender))) /
                                                            (modmordstdet(jr - 1, R, H1) - Adj * agedst(jr, R, gender) / agedst(jr - 1, R, gender)))
                                                        Else
                                                            'jrs 2019/08/27  added + Adj to first term
                                                            If (modmordstdet(jr, R, H1) + Adj - (modmordstdet(jr - 1, R, H1) - Adj * agedst(jr, R, gender) / agedst(jr - 1, R, gender))) > 0 Then
                                                                GrowthH1b = 100
                                                            Else
                                                                GrowthH1b = -100
                                                            End If
                                                        End If
                                                        If (modmordstdet(jr - 2, R, H1)) <> 0 Then
                                                            GrowthH1c = CSng(((modmordstdet(jr - 1, R, H1) - Adj * agedst(jr, R, gender) / agedst(jr - 1, R, gender)) - modmordstdet(jr - 2, R, H1)) /
                                                    (modmordstdet(jr - 2, R, H1)))
                                                        Else
                                                            If ((modmordstdet(jr - 1, R, H1) - Adj * agedst(jr, R, gender) / agedst(jr - 1, R, gender)) - modmordstdet(jr - 2, R, H1)) > 0 Then
                                                                GrowthH1c = 100
                                                            Else
                                                                GrowthH1c = -100
                                                            End If
                                                        End If

                                                        'jrs 2019/08/27 added and (modmordstdet(jr, R, k) - Adj - (modmordstdet(jr - 1, R, k) + Adj * agedst(jr, R, gender) / agedst(jr - 1, R, gender))) / (modmordstdet(jr - 1, R, k) + Adj * agedst(jr, R, gender) / agedst(jr - 1, R, gender)) > ProxyGrowth(jr, kk)
                                                        If GrowthH1 >= ProxyGrowth(jr, kkH1) And GrowthH1b >= ProxyGrowth(jr - 1, kkH1) And GrowthH1c >= ProxyGrowth(jr - 2, kkH1) And newMort <= 750 And
                                                        modmordstdet(jr - 1, R, H1) - Adj * agedst(jr, R, gender) / agedst(jr - 1, R, gender) >= 0 And
                                                        modmordstdet(jr, R, H1) + Adj <= 750 And
                                                        modmordstdet(jr - 1, R, k) + Adj * agedst(jr, R, gender) / agedst(jr - 1, R, gender) <= 750 And
                                                        (modmordstdet(jr, R, k) - Adj - (modmordstdet(jr - 1, R, k) + Adj * agedst(jr, R, gender) / agedst(jr - 1, R, gender))) / (modmordstdet(jr - 1, R, k) + Adj * agedst(jr, R, gender) / agedst(jr - 1, R, gender)) > ProxyGrowth(jr, kk) Then
                                                            modmordstdet(jr, R, k) = newMort
                                                            modmordstdet(jr - 1, R, k) = modmordstdet(jr - 1, R, k) + Adj * agedst(jr, R, gender) / agedst(jr - 1, R, gender)

                                                            modmordstdet(jr, R, H1) = modmordstdet(jr, R, H1) + Adj
                                                            modmordstdet(jr - 1, R, H1) = modmordstdet(jr - 1, R, H1) - Adj * agedst(jr, R, gender) / agedst(jr - 1, R, gender)

                                                            If MIN <= Max Then
                                                                Exit For
                                                            End If
                                                        Else
                                                            bReAdjust = True
                                                            Exit For
                                                        End If
                                                    Else
                                                        Exit For
                                                    End If
                                                Next jr
                                            End If 'end of moving backwards (If FstSatPt(kk) = 10 Then else end if)
                                        End If 'if min > max
                                    Else
                                        bReAdjust = True
                                    End If 'H1 has room (GrowthH1 >= ProxyGrowthH1)
                                Else
                                    If Growth < ProxyGrowth(j, kk) Then
                                        bReAdjust = True
                                    End If
                                End If
                            End If
                        End If
                    Next k
                Next j
                'if we ran out of room try it a 2nd time
                If bReAdjust Then
                    ReAdjustRegJcurveShape(modmordstdet, LowMortLimit, Ntypes, gender, agedst, R, H1, ProxyGrowth)
                End If
            Next R

        End Sub

        'jrs 2009/11/04
        'Readjust Regional Jcurve
        Private Sub ReAdjustRegJcurveShape(modmordstdet(,,) As Double, LowMortLimit(,,) As Double, Ntypes As Integer,
        gender As Integer, agedst(,,) As Double, R As Integer, H1 As Integer, ProxyGrowth(,) As Single)
            'for each country
            Dim k As Integer, j As Integer
            'gvdb 2024/04/05
            'unused variable
            'Dim H1val As Single
            Dim Growth As Single, TotGrowth As Single ', TotProxyGrowth As Single
            Dim Adj As Single
            Dim kk As Integer, kkH1 As Integer
            Dim newMort As Single
            Dim TotMort1 As Single, TotMort2 As Single
            'gvdb 2024/04/05
            'unused variable
            'Dim Growth2 As Single
            Dim MIN As Single, Max As Single
            Dim TotGrowth2 As Single, TotMort3 As Single
            'jrs 2010/02/05
            Dim bMort2Check As Boolean
            Dim GrowthH1 As Single

            'jrs 2010/01/22 replacing kk with kkH1
            If gender = 2 Then
                kkH1 = CInt(H1 / 2)
            Else
                kkH1 = CInt((H1 + 1) / 2)
            End If

            'for each age, 45+
            For j = 10 To NCohorts - 1
                'compute Total Mortality
                TotMort1 = 0
                TotMort2 = 0
                TotMort3 = 0
                For k = gender To Ntypes - 2 Step 2
                    TotMort1 = TotMort1 + CSng(modmordstdet(j, R, k))
                    TotMort2 = TotMort2 + CSng(modmordstdet(j + 1, R, k))
                    TotMort3 = TotMort3 + CSng(modmordstdet(j - 1, R, k))
                Next k

                'jrs 2012/09/03
                If TotMort1 = 0 Then Exit For

                'compute Total growth
                TotGrowth = (TotMort2 - TotMort1) / TotMort1
                TotGrowth2 = (TotMort1 - TotMort3) / TotMort3
                'compare to Total for Proxy
                'TotProxyGrowth = (LowMortLimit(j + 1, 15, gender) - LowMortLimit(j, 15, gender)) / LowMortLimit(j, 15, gender)

                'for each type (except H1) -> Non Comm. Dis
                For k = gender + 2 To 20 Step 2
                    If k <> H1 And Not (k > 12 And k < 19) Then
                        If gender = 2 Then
                            kk = CInt(k / 2)
                        Else
                            kk = CInt((k + 1) / 2)
                        End If

                        'when there's mortality
                        If modmordstdet(j, R, k) > 0.000000001 And LowMortLimit(j, kk, gender) > 0.000000001 And modmordstdet(j, R, H1) > 0.000000001 Then
                            'find growth from one age to the next
                            Growth = CSng((modmordstdet(j + 1, R, k) - modmordstdet(j, R, k)) / modmordstdet(j, R, k))
                            'jrs 2009/12/04 added check for population in age j+1
                            'if growth is smaller then adjust type and remove adjustment from H1
                            If Growth < ProxyGrowth(j, kk) And agedst(j + 1, R, gender) > 0 Then
                                'find min current (j) mort
                                MIN = CSng(modmordstdet(j - 1, R, k) * (1 + ProxyGrowth(j - 1, kk)))
                                'find max current (j) mort
                                'max = modmordstdet(j + 1, R, k) / (1 + ProxyGrowth)
                                'considering that modmordstdet(j + 1, R, k) will change by Adj *agedst(j)/agedst(j+1)
                                'Adj = modmordstdet(j , R, k) - max, we find
                                Max = CSng((modmordstdet(j + 1, R, k) + modmordstdet(j, R, k) * agedst(j, R, gender) / agedst(j + 1, R, gender)) / (1 + ProxyGrowth(j, kk) + agedst(j, R, gender) / agedst(j + 1, R, gender)))

                                If MIN < Max Then
                                    newMort = Max
                                Else
                                    newMort = MIN
                                End If

                                Adj = CSng(modmordstdet(j, R, k) - newMort)
                                'check if adjustment fits in max category (H1)
                                GrowthH1 = CSng((modmordstdet(j + 1, R, H1) - Adj * agedst(j, R, gender) / agedst(j + 1, R, gender) - (modmordstdet(j, R, H1) + Adj)) / (modmordstdet(j, R, H1) + Adj))

                                'jrs 2010/02/05
                                bMort2Check = (j > 19)
                                If Not bMort2Check Then
                                    'checking not to introduce a biggger drop than we already have
                                    bMort2Check = CBool((modmordstdet(j + 1, R, k) + Adj * agedst(j, R, gender) / agedst(j + 1, R, gender) <= modmordstdet(j + 2, R, k) / AMAX((1 + Amin(Growth, ProxyGrowth(j + 1, kk))), 0.000001)))
                                End If
                                'jrs 2010/02/08 negative Adj, make j-curve worst
                                'don't allow
                                bMort2Check = bMort2Check And (Adj > 0)

                                'jrs 20009/12/04
                                'if adjustment fits in H1 then save it
                                If GrowthH1 >= ProxyGrowth(j, kkH1) And newMort <= 750 And
                        modmordstdet(j + 1, R, k) + Adj * agedst(j, R, gender) / agedst(j + 1, R, gender) <= 750 And bMort2Check And
                        modmordstdet(j + 1, R, k) + Adj * agedst(j, R, gender) / agedst(j + 1, R, gender) >= 0 And
                        modmordstdet(j, R, H1) + Adj <= 750 Then
                                    modmordstdet(j, R, k) = newMort
                                    modmordstdet(j + 1, R, k) = modmordstdet(j + 1, R, k) + Adj * agedst(j, R, gender) / agedst(j + 1, R, gender)

                                    modmordstdet(j, R, H1) = modmordstdet(j, R, H1) + Adj
                                    modmordstdet(j + 1, R, H1) = modmordstdet(j + 1, R, H1) - Adj * agedst(j, R, gender) / agedst(j + 1, R, gender)

                                    If MIN > Max Then 'adjustment might not be enough, need to check again
                                        Growth = CSng((modmordstdet(j + 1, R, k) - modmordstdet(j, R, k)) / modmordstdet(j, R, k))

                                        If Growth < ProxyGrowth(j, kk) Then
                                            If j < 20 Then
                                                newMort = CSng(modmordstdet(j, R, k) * (1 + ProxyGrowth(j, kk)))
                                                Adj = CSng(newMort - modmordstdet(j + 1, R, k))
                                                'check if adjustment fits in max category (H1)
                                                GrowthH1 = CSng((modmordstdet(j + 1, R, H1) - Adj - modmordstdet(j, R, H1)) / modmordstdet(j, R, H1))
                                                'jrs 2009/12/04 check population for age j+2 and newMort <= 1000
                                                'if adjustment fits in H1 then continue
                                                'jrs 2010/02/06
                                                bMort2Check = agedst(j + 2, R, gender) > 0
                                                If bMort2Check Then
                                                    bMort2Check = modmordstdet(j + 2, R, k) - Adj * agedst(j + 1, R, gender) / agedst(j + 2, R, gender) >= 0
                                                    If bMort2Check Then
                                                        bMort2Check = modmordstdet(j + 2, R, k) - Adj * agedst(j + 1, R, gender) / agedst(j + 2, R, gender) >= modmordstdet(j + 1, R, k) * (1 + Amin(Growth, ProxyGrowth(j + 1, kk)))
                                                    End If
                                                End If
                                                'jrs 2010/01/22 added to check that we don't go negative in type k (j+2)
                                                If GrowthH1 >= ProxyGrowth(j, kkH1) And bMort2Check And newMort <= 750 Then
                                                    'jrs 2010/02/05
                                                    bMort2Check = (j > 18)
                                                    If Not bMort2Check Then
                                                        'checking not to introduce a biggger drop than we already have
                                                        bMort2Check = CBool((modmordstdet(j + 2, R, H1) + Adj * agedst(j + 1, R, gender) / agedst(j + 2, R, gender) <= modmordstdet(j + 3, R, H1) / AMAX((1 + ProxyGrowth(j + 2, kkH1)), 0.000001)))
                                                    End If

                                                    If modmordstdet(j + 2, R, H1) + Adj * agedst(j + 1, R, gender) / agedst(j + 2, R, gender) <= 750 And bMort2Check Then
                                                        modmordstdet(j + 1, R, k) = newMort
                                                        'Tilt next segment to keep constant # of deaths
                                                        'if working in last cohort remove excess from infants
                                                        modmordstdet(j + 2, R, k) = modmordstdet(j + 2, R, k) - Adj * agedst(j + 1, R, gender) / agedst(j + 2, R, gender)
                                                        'adjust H1
                                                        modmordstdet(j + 1, R, H1) = modmordstdet(j + 1, R, H1) - Adj
                                                        'tilt keeping # of deaths constant
                                                        'if last cohort use infants to keep deaths constant
                                                        modmordstdet(j + 2, R, H1) = modmordstdet(j + 2, R, H1) + Adj * agedst(j + 1, R, gender) / agedst(j + 2, R, gender)
                                                    Else
                                                        'MsgBox "There's no room in max category to switch mortality for " & GRPS(1, R), vbCritical, "Adjust J-curve"
                                                    End If
                                                Else
                                                    'MsgBox "There's no room in max category to switch mortality for " & GRPS(1, R), vbCritical, "Adjust J-curve"
                                                End If
                                            Else
                                                'MsgBox "There's no room to make J-curve monotonically increasing for " & GRPS(1, R), vbCritical, "Readjust J-curve"
                                            End If
                                        End If 'if growth < ProxyGrowth
                                    End If 'if min > max
                                Else
                                    'MsgBox "There's no room in max category to switch mortality for " & GRPS(1, R), vbCritical, "Adjust J-curve"
                                End If 'if adjustment fits in H1 (GrowthH1 >= ProxyGrowthH1)
                            End If 'if not monotonically increasing (Growht < ProxyGrowth)
                        End If 'when there's mortality
                    End If 'for all types except H1
                Next k
            Next j

        End Sub

        'jrs 2009/10/28
        'Loads Smoking Additive Shift Factors
        Private Sub LoadSmkingShiftFctrs(SmokingFactors(,) As Single, context As IForecastContext)
            Dim FormValue As Single
            Dim TbName As String
            'jrs 2008/07/18 changed from Linear to Quadratic
            'jrs 2024/06/17 TbName$ = "GDP/Capita (PPP 2000) Versus Male Smoking Rate (" & CStr(BaseYear) & ") Quadratic"
            TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Male Smoking Rate (" & CStr(BaseYear) & ") Quadratic"
            TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            For R% = 1 To NR%
                'jrs 2008/07/18 limit to $50k
                Call XYTABL(TbName$, Amin(GDPPCP(R%), 50), FormValue, context)
                SmokingFactors(R%, 1) = HLSMOKING(R%, 1) - FormValue
            Next R%

            'jrs 2008/07/15 changed from Linear to Quadratic
            'jrs 2024/06/17 TbName$ = "GDP/Capita (PPP 2000) Versus Female Smoking Rate (" & CStr(BaseYear) & ") Quadratic"
            TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Female Smoking Rate (" & CStr(BaseYear) & ") Quadratic"
            TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            For R% = 1 To NR%
                'jrs 2008/07/18 limit to $30k
                Call XYTABL(TbName$, Amin(GDPPCP(R%), 30), FormValue, context)
                SmokingFactors(R%, 2) = HLSMOKING(R%, 2) - FormValue
            Next R%
        End Sub

        'jrs 2008/12/03
        'jrs 2011/07/26 the estimates are now the last 25 years before base year
        'Smoking Impact from WHO is from 2005 to 2030
        Private Sub LoadSmokingHist(context As IForecastContext)
            If Not context.HistoricMode Then
                Dim Range As Integer
                Range = 10
                Using dbS As IFsSmokingContext = New IFsSmokingContext
                    For Each rsS In (dbS.SmokingRateEstimatesReg.Where(Function(s) s.Year >= BaseYear - Range And s.Year <= BaseYear))
                        SHLSMOKING(rsS.RegionId, rsS.Sex, rsS.Year - (BaseYear - Range)) = CSng(rsS.SmokingRate)
                    Next
                End Using
            End If
        End Sub

        'jrs 2011/05/17 *** new procedure for Smoking in Stages ***
        Private Sub FindSmokingGroup(ByVal Range As Integer, context As IForecastContext)
            'Males
            '1. Early to Rising:
            '1.1 Male prevalence rises during historic estimate
            '1.2 Current prevalence <=15%
            '2. Rising to Peak:
            '2.1 Male prevalence rises during historic estimate
            '2.2 Current prevalence > 15%
            '3. Peak to Falling:
            '3.1 Male prevalence peaks during historic estimate (1980-2005) but less than 10 years of historic decline (ie peak year> 1995 when current year=2005) OR
            'During Forecast also consider:
            '3.2 Male prevalence peaks during forecast due to 65% male prevalence ceiling in stage 2 OR
            '3.3 Male prevalence peaks due to scenario (peak year switch activated)
            '4. Late Stage:
            '4.1 Male prevalence declines throughout at least the last ten years of historic estimation (ie peak year < 1995 when current year = 2005)
            If Not context.HistoricMode Then
                Dim SmokingRateEstimatesRegList As List(Of SmokingRateEstimatesReg)
                Using dbS As IFsSmokingContext = New IFsSmokingContext
                    SmokingRateEstimatesRegList = dbS.SmokingRateEstimatesReg.ToList
                End Using

                'Dim tbl As IQueryable(Of SmokingRateEstimatesReg)
                'Dim tbl As Dictionary(Of Integer, Double?)
                Dim tbl As IEnumerable(Of SmokingRateEstimatesReg)

                Dim SR2 = (From E2 In SmokingRateEstimatesRegList
                           Where E2.Year < BaseYear + 1
                           Group Value = E2.SmokingRate By E2.RegionId, E2.Sex Into Data = Group, MaxVal = Max(Value))

                Dim SR3 = (From E1 In SmokingRateEstimatesRegList
                           Join E2 In SR2 On E1.RegionId Equals E2.RegionId And E1.Sex Equals E2.Sex
                           Where E1.SmokingRate = E2.MaxVal And E1.Year < BaseYear + 1
                           Group Value = E2.MaxVal, ValYear = E1.Year By E1.RegionId, E1.Sex Into Data = Group, Y = Max(ValYear))

                'It's just 372 rows (NR x 2)
                For Each rsS In SR3
                    'tbl = New Dictionary(Of Integer, Double?)
                    'For Males
                    If rsS.Sex = 1 Then
                        Select Case True
                        'Early to Rising
                            Case rsS.Y = BaseYear And SHLSMOKING(rsS.RegionId, rsS.Sex, Range) <= 15
                                SMOKINGGRP(rsS.RegionId, rsS.Sex) = 1
                        'Rising to Peak
                            Case rsS.Y = BaseYear And SHLSMOKING(rsS.RegionId, rsS.Sex, Range) > 15
                                SMOKINGGRP(rsS.RegionId, rsS.Sex) = 2
                        'Peak to Falling
                            Case rsS.Y > BaseYear - 20
                                SMOKINGGRP(rsS.RegionId, rsS.Sex) = 3

                                tbl = (From s In SmokingRateEstimatesRegList
                                       Where s.Sex = 1 And s.RegionId = rsS.RegionId And s.Year >= rsS.Y And s.Year <= BaseYear)

                                ExpReg(tbl, SMOKINGEXPREG(rsS.RegionId, 1, rsS.Sex), SMOKINGEXPREG(rsS.RegionId, 2, rsS.Sex))
                        'Late Stage
                            Case rsS.Y <= BaseYear - 20
                                SMOKINGGRP(rsS.RegionId, rsS.Sex) = 4
                                tbl = (From s In SmokingRateEstimatesRegList
                                       Where s.Sex = 1 And s.RegionId = rsS.RegionId And s.Year >= rsS.Y And s.Year <= BaseYear)
                                ExpReg(tbl, SMOKINGEXPREG(rsS.RegionId, 1, rsS.Sex), SMOKINGEXPREG(rsS.RegionId, 2, rsS.Sex))
                                LogReg(tbl, SMOKINGEXPREG(rsS.RegionId, 3, rsS.Sex), SMOKINGEXPREG(rsS.RegionId, 4, rsS.Sex))
                        End Select
                        'For Females
                    Else 'jrs 2011/06/16
                        Select Case True
                        'Early to Rising
                            Case rsS.Y > BaseYear - 5 And SHLSMOKING(rsS.RegionId, rsS.Sex, Range) <= 1
                                SMOKINGGRP(rsS.RegionId, rsS.Sex) = 1
                                tbl = (From s In SmokingRateEstimatesRegList
                                       Where s.Sex = 2 And s.RegionId = rsS.RegionId And s.Year <= BaseYear)

                                ExpReg(tbl, SMOKINGEXPREG(rsS.RegionId, 1, rsS.Sex), SMOKINGEXPREG(rsS.RegionId, 2, rsS.Sex))
                        'Rising to Peak
                            Case rsS.Y > BaseYear - 5 And SHLSMOKING(rsS.RegionId, rsS.Sex, Range) > 1 And SHLSMOKING(rsS.RegionId, rsS.Sex, Range) < 45
                                SMOKINGGRP(rsS.RegionId, rsS.Sex) = 2
                                tbl = (From s In SmokingRateEstimatesRegList
                                       Where s.Sex = 2 And s.RegionId = rsS.RegionId And s.Year <= BaseYear)
                                ExpReg(tbl, SMOKINGEXPREG(rsS.RegionId, 1, rsS.Sex), SMOKINGEXPREG(rsS.RegionId, 2, rsS.Sex))
                        'Peak to Falling
                            Case rsS.Y > BaseYear - 5 And SHLSMOKING(rsS.RegionId, rsS.Sex, Range) >= 45
                                SMOKINGGRP(rsS.RegionId, rsS.Sex) = 3
                        'Late Stage
                            Case rsS.Y <= BaseYear - 5
                                SMOKINGGRP(rsS.RegionId, rsS.Sex) = 4
                                tbl = (From s In SmokingRateEstimatesRegList
                                       Where s.Sex = 2 And s.RegionId = rsS.RegionId And s.Year >= rsS.Y And s.Year <= BaseYear)

                                LogReg(tbl, SMOKINGEXPREG(rsS.RegionId, 3, rsS.Sex), SMOKINGEXPREG(rsS.RegionId, 4, rsS.Sex))
                        End Select
                    End If
                    SMOKINGPEAKYR(rsS.RegionId, rsS.Sex) = rsS.Y
                Next
            End If
        End Sub

        'jrs 2011/05/24 *** new procedure for Smoking in Stages ***
        'Exponential Regression based in LReg code created by Mohammod
        'Private Sub ExpReg(valtbl As IQueryable(Of SmokingRateEstimatesReg), ByRef A As Single, ByRef B As Single)
        Private Sub ExpReg(valtbl As IEnumerable(Of SmokingRateEstimatesReg), ByRef A As Single, ByRef B As Single)
            '1. valtbl is the table that contains the historic data - Regionalized
            '2. StartYear is the first year to consider for the regression
            '3. a in y = a*b^x
            '4. b in y = a*b^x

			'gvdb 2024/04/05
            'unused variable
            'Dim j As Integer, 
            Dim YearVal As Integer
            Dim VarVal As Double?
            Dim X As Double, Y As Double
            Dim reg As New RegressionObject
            Dim pt As Integer

            With reg
                .Init()
                .Degree = 1 'Degree
                pt = 0
                For Each item In valtbl
                    YearVal = item.Year
                    VarVal = item.SmokingRate
                    If VarVal.HasValue Then
                        If VarVal > 0 Then
                            X# = YearVal
                            Y# = Log(CDbl(VarVal))
                            .XYAdd(X#, Y#)
                            pt = pt + 1
                        End If
                    End If
                Next

                A = -9999
                B = -9999
                If pt > 1 Then
                    If Abs(reg.Coeff(0)) < 700 And Abs(reg.Coeff(1)) < 700 Then
                        A = CSng(Exp(reg.Coeff(0)))
                        B = CSng(Exp(reg.Coeff(1)))
                    End If
                End If
            End With
        End Sub

        'jrs 2011/05/24 *** new procedure for Smoking in Stages ***
        'Logarithmic Regression based in LReg code created by Mohammod
        'Private Sub LogReg(ByRef valtbl As IQueryable(Of SmokingRateEstimatesReg), A As Single, B As Single)
        'Private Sub LogReg(ByRef valtbl As Dictionary(Of Integer, Double?), A As Single, B As Single)
        Private Sub LogReg(ByRef valtbl As IEnumerable(Of SmokingRateEstimatesReg), A As Single, B As Single)
            '1. valtbl is the table that contains the historic data - Regionalized
            '2. StartYear is the first year to consider for the regression
            '3. a in y = a+b*ln(x)
            '4. b in y = a+b*ln(x)

			'gvdb 2024/04/05
            'unused variable
            'Dim j As Integer,
            Dim YearVal As Integer
            Dim VarVal As Double?
            Dim X As Double, Y As Double
            Dim reg As New RegressionObject
            Dim pt As Integer

            With reg
                .Init()
                .Degree = 1 'Degree
                pt = 0
                For Each item In valtbl
                    YearVal = item.Year 'item.Key '
                    VarVal = item.SmokingRate 'item.Value 
                    If VarVal.HasValue Then
                        X# = Log(CDbl(YearVal))
                        Y# = CSng(VarVal)
                        .XYAdd(X#, Y#) 'Ln(rr%), Format(PlotLong(rr%, 1), "0.000")
                        pt = pt + 1
                    End If
                Next

                A = -9999
                B = -9999
                If pt > 1 Then
                    A = CSng(reg.Coeff(0))
                    B = CSng(reg.Coeff(1))
                End If
            End With
        End Sub

        'jrs 2009/10/28
        'Loads BMI multiplicative shift factors
        Private Sub LoadBMIShiftFctrs(BMIfullfactorsM() As Double, BMIfullfactorsF() As Double, context As IForecastContext)
            Dim TbName As String
            Dim FormValue As Single
            TbName$ = "Calories per Capita Versus Female BMI (" & CStr(BaseYear) & ") Linear"
            TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
            For R% = 1 To NR%
                Call XYTABL(TbName$, CLPC(R%, 4), FormValue, context)
                BMIfullfactorsF(R%) = HLBMI(R%, 2) / FormValue
            Next R%

            TbName$ = "Calories per Capita Versus Male BMI (" & CStr(BaseYear) & ") Linear"
            TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
            For R% = 1 To NR%
                Call XYTABL(TbName$, CLPC(R%, 4), FormValue, context)
                BMIfullfactorsM(R%) = HLBMI(R%, 1) / FormValue
            Next R%
        End Sub

        'jrs 2009/10/28
        'Loads Obesity additive shift factors
        Private Sub LoadObShiftFctrs(ObesityFactors(,) As Single, context As IForecastContext)
            'jrs 2008/08/21 TbName$ = "Calories per Capita Versus Male Obesity Rate (" & CStr(BaseYear) & ") Linear"
            'jrs 2009/09/01 TbName$ = "Calories per Capita Versus Obesity Rate Adult Males (" & CStr(BaseYear) & ") Linear"
            'jrs 2009/09/01 TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear)
            Dim TbName As String
            Dim FormValue As Single
            TbName$ = "BMI Versus Male Obesity % (CRA) Quad"
            For R% = 1 To NR%
                Call XYTABL(TbName$, HLBMI(R%, 1), FormValue, context)
                ObesityFactors(R%, 1) = HLOBESITY(R%, 1) - FormValue
            Next R%

            'jrs 2008/08/21 TbName$ = "Calories per Capita Versus Female Obesity Rate (" & CStr(BaseYear) & ") Linear"
            'jrs 2009/09/01 TbName$ = "Calories per Capita Versus Obesity Rate Adult Females (" & CStr(BaseYear) & ") Linear"
            'jrs 2009/09/01 TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear)
            TbName$ = "BMI Versus Female Obesity % (CRA) Quad"
            For R% = 1 To NR%
                Call XYTABL(TbName$, HLBMI(R%, 2), FormValue, context)
                ObesityFactors(R%, 2) = HLOBESITY(R%, 2) - FormValue
            Next R%
        End Sub

        'jrs 2009/10/28
        'Smoking Impact Initialization
        Private Sub SmokingImpactInit()
            For R% = 1 To NR%
                For j = 7 To NCohorts
                    Select Case j
                '30 to 44
                        Case 7, 8, 9
                            HLSMOKINGIMPMALEI(R%, j) = HLSMOKINGIMP(R%, 1, 1)
                            HLSMOKINGIMPFEMALEI(R%, j) = HLSMOKINGIMP(R%, 1, 2)
                '45 to 59
                        Case 10, 11, 12
                            HLSMOKINGIMPMALEI(R%, j) = HLSMOKINGIMP(R%, 2, 1)
                            HLSMOKINGIMPFEMALEI(R%, j) = HLSMOKINGIMP(R%, 2, 2)
                '60 to 69
                        Case 13, 14
                            HLSMOKINGIMPMALEI(R%, j) = HLSMOKINGIMP(R%, 3, 1)
                            HLSMOKINGIMPFEMALEI(R%, j) = HLSMOKINGIMP(R%, 3, 2)
                            '70+
                        Case Else
                            HLSMOKINGIMPMALEI(R%, j) = HLSMOKINGIMP(R%, 4, 1)
                            HLSMOKINGIMPFEMALEI(R%, j) = HLSMOKINGIMP(R%, 4, 2)
                    End Select
                Next j

                'jrs 2008/11/20
                For j = 1 To 4
                    For gg = 1 To 2
                        SHLSMOKINGIMP(R%, j, gg) = HLSMOKINGIMP(R%, j, gg)
                    Next gg
                Next j
            Next R%

        End Sub

        'kbn 2018/03/30 START- FirstYear: Compute Shift factors for Maternal Mortality
        '----------------------------------------------------------------------

        Private Sub MaternalMortalityshift(context As IForecastContext)
            Dim matmor, MatMortTimePar, MatMortTimeParAdj As Single
            If IY% = 1 Then
                For R% = 1 To NR%

                    'Call AnalFunc("MaternalMortalityRatio versus TFR(Log) & GDPPCP(Log)", New Single() {GDPPCP(R%), TFR(R%)},
                    'matmor, New String() {"GDP per capita in 1000 USD (Log)", "TFRMedUNPD(MOSTRECENT) (Log)"}, context)
                    Call AnalFunc("MaternalMortalityRatio versus TFR(Log) & GDPPCP(Log)", New Single() {GDPPCP(R%), AMAX(2, TFR(R%))},
                    matmor, New String() {"GDP per capita in 1000 USD (Log)", "TFRMedUNPD(MOSTRECENT) (Log)"}, context) 'bbh 2021/04/30

                    MatMortshift(R%) = MATMORTRATIO(R%) - AMAX(Amin(matmor, 2000), 3)
                    'bbh 2019/01/19 start 1st year only
                    MatMortTimePar = 0.015
                    MatMortTimeParAdj = MatMortTimePar * AMAX(0, (30 - GDPPCP(R%)) / 30)
                    MatMortTimeTr(R%) = MATMORTRATIO(R%) * MatMortTimeParAdj
                    'bbh 2019/01/19 end
                Next R%
            End If
        End Sub

        'bbh 2019/04/06 start

        Private Sub HIVPrevIncShift(context As IForecastContext)
            Dim TbName As String
            Dim CurrentRatio, CalcIncRPrev As Single
            TbName$ = "Condom Use at Risk Sex Versus HIV Incidence/Prevalence (2015) Lin"
            For R% = 1 To NR%
                If HLHIVPREV(R%) = 0 Then
                    CurrentRatio = 0.08 'value of ratio at 50% Condom use
                Else
                    CurrentRatio = HLHIVINC(R%) / HLHIVPREV(R%)
                End If
                Call XYTABL(TbName$, HLCONDOMUSE(R%), CalcIncRPrev, context)

                'If HLHIVPREV(R%) = 0 Then CurrentRatio = 0.08 'value of ratio at 50% ART coverage
                HIVIncShift(R%) = CurrentRatio / AMAX(0.0001, CalcIncRPrev)
                HLHIVINCPREVR(R%) = CurrentRatio
            Next R%
        End Sub

        Private Sub HIVMortRPrevShift(context As IForecastContext)
            Dim TbName As String
            Dim CurrentRatio, CalcMortRPrev As Single
            TbName$ = "HIV Mortality/Prevalence vs ART Percent (2015) Lin"
            For R% = 1 To NR%
                If HLHIVPREV(R%) = 0 Then
                    CurrentRatio = 0.04 'value of ratio at 50% ART coverage
                Else
                    CurrentRatio = HLHIVMORT(R%) / HLHIVPREV(R%)
                End If
                Call AnalFunc(TbName$, New Single() {HLHIVART(R%)}, CalcMortRPrev,
            New String() {"HLHIVART"}, context)
                'If HLHIVPREV(R%) = 0 Then CurrentRatio = 0.04 'value of ratio at 50% ART coverage
                HIVMortShift(R%) = CurrentRatio / AMAX(0.0001, CalcMortRPrev)
                HLHIVMORTPREVR(R%) = CurrentRatio
                'If RegName$(R%) = "Argentina" Then Stop
            Next R%
        End Sub

        Private Sub HIVCondomUseShift(context As IForecastContext)
            Dim TbName As String
            Dim CalcCondomUse As Single
            TbName$ = "Education Years (15+) Versus Condom Use (" & CStr(BaseYear) & ") Log"
            TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
            For R% = 1 To NR%
                Call XYTABL(TbName$, EDYRSAG15(R%, 3), CalcCondomUse, context) 'check teo make sure edyrsag15 is calculated also after new rebase
                If CalcCondomUse > 0 Then
                    CondomUseShift(R%) = HLCONDOMUSE(R%) / CalcCondomUse
                Else
                    CondomUseShift(R%) = 1
                End If
            Next R%
        End Sub

        Private Sub HIVARTShift(context As IForecastContext)
            'jrs 2024/06/20 Dim TbName As String
            Dim CalcART As Single
            TFNamePctARTCov = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Percentage ART Coverage (" & CStr(BaseYear) & ") Log"
            TFNamePctARTCov = getFunctionNameYrOrMstRct(TFNamePctARTCov, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            For R% = 1 To NR%
                Call XYTABL(TFNamePctARTCov, GDPPCP(R%), CalcART, context) 'check to make sure edyrsag15 is calculated also after new rebase
                If CalcART > 0 Then
                    ARTShift(R%) = HLHIVART(R%) / CalcART
                Else
                    ARTShift(R%) = 1
                End If
                'If RegName$(R%) = "Argentina" Then Stop
            Next R%
        End Sub

        'Private Sub BedNetsShift()
        'HLBEDNETS
        'Could add a function on BedNets and a shift factor.  But strongest relationships
        '  are with incidence, prevalence, mortalility rates and such a function could set up
        '  a decrease in useage of them with success, while historical patterns are
        '  steady increase even with/contributing to success.  Hence use of algorithm in
        '  years past 1st that builds in growth that slows with approach to 100% and
        '  is not as fast if rates are very low.
        'End Sub
        'bbh 2019/04/06 end

        'jrs 2013/09/20
        Private Sub LoadAvgAgeAndStdLifeExp()
            'FirstYear: Compute Average Age per Cohort
            '-----------------------------------------
            AvgAgePerCohort(0) = 0.1 'jrs 2010/03/15 used to be 0.5
            AvgAgePerCohort(1) = 2.6 'jrs 2010/03/15 used to be 2.5
            AvgAgePerCohort(2) = 7.5 'jrs 2010/03/15 used to be 7
            AvgAgePerCohort(NCohorts - 2) = NAges - 8 'jrs 2013/12/19 92
            AvgAgePerCohort(NCohorts - 1) = CSng(NAges - 3.5) 'jrs 2013/12/19 96.5
            AvgAgePerCohort(NCohorts) = CSng(NAges + 2.5) 'jrs 2013/12/19 102.5
            For j = 3 To NCohorts - 3
                'jrs 2010/03/05
                '        If MaxLIFEXP >= (j - 1) * 5 And MaxLIFEXP < j * 5 Then
                '            AvgAgePerCohort(j) = (MaxLIFEXP + (j - 1) * 5) / 2
                '            MaxLifeExpCohort = j
                '            MaxLifeExpCohortPortion = (MaxLIFEXP - (j - 1) * 5) / 5
                '            Exit For
                '        Else
                AvgAgePerCohort(j) = AvgAgePerCohort(j - 1) + 5
                '        End If
            Next j

            'jrs 2010/03/15
            'Loading Standard Life Expectancy for Males
            '------------------------------------------
            'jrs 2014/01/07 *** begin ***
            '    StdLifeExp(0, 1) = 79.94
            '    StdLifeExp(1, 1) = 77.77
            '    StdLifeExp(2, 1) = 72.89
            '    StdLifeExp(3, 1) = 67.91
            '    StdLifeExp(4, 1) = 62.93
            '    StdLifeExp(5, 1) = 57.95
            '    StdLifeExp(6, 1) = 52.99
            '    StdLifeExp(7, 1) = 48.04
            '    StdLifeExp(8, 1) = 43.1
            '    StdLifeExp(9, 1) = 38.2
            '    StdLifeExp(10, 1) = 33.38
            '    StdLifeExp(11, 1) = 28.66
            '    StdLifeExp(12, 1) = 24.07
            '    StdLifeExp(13, 1) = 19.65
            '    StdLifeExp(14, 1) = 15.54
            '    StdLifeExp(15, 1) = 11.87
            '    StdLifeExp(16, 1) = 8.81
            '    StdLifeExp(17, 1) = 6.34
            '    StdLifeExp(18, 1) = 4.39
            '    StdLifeExp(19, 1) = 3.05
            '    StdLifeExp(20, 1) = 2.06
            '    StdLifeExp(21, 1) = 1.06
            StdLifeExp(0, 1) = 86.02
            StdLifeExp(1, 1) = 83.73
            StdLifeExp(2, 1) = 79.23
            StdLifeExp(3, 1) = 74.28
            StdLifeExp(4, 1) = 69.32
            StdLifeExp(5, 1) = 64.37
            StdLifeExp(6, 1) = 59.43
            StdLifeExp(7, 1) = 54.49
            StdLifeExp(8, 1) = 49.58
            StdLifeExp(9, 1) = 44.71
            StdLifeExp(10, 1) = 39.9
            StdLifeExp(11, 1) = 35.19
            StdLifeExp(12, 1) = 30.56
            StdLifeExp(13, 1) = 26
            StdLifeExp(14, 1) = 21.55
            StdLifeExp(15, 1) = 17.28
            StdLifeExp(16, 1) = 13.27
            StdLifeExp(17, 1) = 9.65
            StdLifeExp(18, 1) = 6.61
            StdLifeExp(19, 1) = 4.35
            StdLifeExp(20, 1) = 2.88
            StdLifeExp(21, 1) = 1.93
            'jrs 2014/01/07 *** end ***

            'Loading Standard Life Expectancy for Females
            '--------------------------------------------
            'jrs 2014/01/07 *** begin ***
            '    StdLifeExp(0, 2) = 82.43
            '    StdLifeExp(1, 2) = 80.28
            '    StdLifeExp(2, 2) = 75.47
            '    StdLifeExp(3, 2) = 70.51
            '    StdLifeExp(4, 2) = 65.55
            '    StdLifeExp(5, 2) = 60.63
            '    StdLifeExp(6, 2) = 55.72
            '    StdLifeExp(7, 2) = 50.83
            '    StdLifeExp(8, 2) = 45.96
            '    StdLifeExp(9, 2) = 41.13
            '    StdLifeExp(10, 2) = 36.36
            '    StdLifeExp(11, 2) = 31.68
            '    StdLifeExp(12, 2) = 27.1
            '    StdLifeExp(13, 2) = 22.64
            '    StdLifeExp(14, 2) = 18.32
            '    StdLifeExp(15, 2) = 14.24
            '    StdLifeExp(16, 2) = 10.59
            '    StdLifeExp(17, 2) = 7.56
            '    StdLifeExp(18, 2) = 5.24
            '    StdLifeExp(19, 2) = 3.71
            '    StdLifeExp(20, 2) = 2.62
            '    StdLifeExp(21, 2) = 1.55
            StdLifeExp(0, 2) = 86.02
            StdLifeExp(1, 2) = 83.73
            StdLifeExp(2, 2) = 79.23
            StdLifeExp(3, 2) = 74.28
            StdLifeExp(4, 2) = 69.32
            StdLifeExp(5, 2) = 64.37
            StdLifeExp(6, 2) = 59.43
            StdLifeExp(7, 2) = 54.49
            StdLifeExp(8, 2) = 49.58
            StdLifeExp(9, 2) = 44.71
            StdLifeExp(10, 2) = 39.9
            StdLifeExp(11, 2) = 35.19
            StdLifeExp(12, 2) = 30.56
            StdLifeExp(13, 2) = 26
            StdLifeExp(14, 2) = 21.55
            StdLifeExp(15, 2) = 17.28
            StdLifeExp(16, 2) = 13.27
            StdLifeExp(17, 2) = 9.65
            StdLifeExp(18, 2) = 6.61
            StdLifeExp(19, 2) = 4.35
            StdLifeExp(20, 2) = 2.88
            StdLifeExp(21, 2) = 1.93
            'jrs 2014/01/07 *** end ***
        End Sub

        'jrs 2013/12/11
        'Load Ratios of Mortality Subtypes to Mortality types
        Private Sub LoadMortSubTypeDistributions(context As IForecastContext)

            Using db As IFsContext = New IFsContext

                Dim ageCol As Dictionary(Of String, Integer)
                ageCol = New Dictionary(Of String, Integer)
                ageCol.Add("Infants", 0)
                ageCol.Add("1-4 years", 1)
                Dim i As Integer
                For i = 2 To 16
                    ageCol.Add(CStr((i - 1) * 5) & "-" & CStr(i * 5 - 1) & " years", i)
                Next i
                ageCol.Add("80+ years", 17)
                Dim genCol As Dictionary(Of String, Integer)
                genCol = New Dictionary(Of String, Integer)
                genCol.Add("Male", 1)
                genCol.Add("Female", 2)

                'Lung Cancer
                'gvdb 2024/04/05
                'update for new EF syntax
                Dim rs = (From f In context.Regionalization
                          Join r In db.HealthMortRatios On f.Key Equals r.Country
                          Where r.Disease = "Lung Cancer"
                          Select RegionId = f.Value, r.Age, r.Gender, r.Ratio)
                For Each item In rs
                    LungCancerDist(item.RegionId, ageCol(item.Age), genCol(item.Gender)) = CSng(item.Ratio)
                Next
                'IHD
                'gvdb 2024/04/05
                'update for new EF syntax
                rs = (From f In context.Regionalization
                      Join r In db.HealthMortRatios On f.Key Equals r.Country
                      Where r.Disease = "IHD"
                      Select RegionId = f.Value, r.Age, r.Gender, r.Ratio)
                For Each item In rs
                    IHDDist(item.RegionId, ageCol(item.Age), genCol(item.Gender)) = CSng(item.Ratio)
                Next
                'Stroke
                'gvdb 2024/04/05
                'update for new EF syntax
                rs = (From f In context.Regionalization
                      Join r In db.HealthMortRatios On f.Key Equals r.Country
                      Where r.Disease = "Stroke"
                      Select RegionId = f.Value, r.Age, r.Gender, r.Ratio)
                For Each item In rs
                    StrokeDist(item.RegionId, ageCol(item.Age), genCol(item.Gender)) = CSng(item.Ratio)
                Next
                'COPD
                'gvdb 2024/04/05
                'update for new EF syntax
                rs = (From f In context.Regionalization
                      Join r In db.HealthMortRatios On f.Key Equals r.Country
                      Where r.Disease = "COPD"
                      Select RegionId = f.Value, r.Age, r.Gender, r.Ratio)
                For Each item In rs
                    COPDDist(item.RegionId, ageCol(item.Age), genCol(item.Gender)) = CSng(item.Ratio)
                Next
            End Using
        End Sub

        'jrs 2009/10/28
        Private Sub Health_Forecast_Smoking(context As IForecastContext)
            Dim TbName As String
            Dim EffectAdd, FormValue, SmkCmpGr, SmkGr As Single

            'jrs 2008/07/18 changed from Linear to Quadratic
            'jrs 2024/06/17 TbName$ = "GDP/Capita (PPP 2000) Versus Male Smoking Rate (" & CStr(BaseYear) & ") Quadratic"
            TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Male Smoking Rate (" & CStr(BaseYear) & ") Quadratic"
            TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            For R% = 1 To NR%
                'jrs 2008/07/24
                '        If SmokingFactors(R%, 1) < 0 And GDPPCIP(R%) > 25 Then
                EffectAdd = SmokingFactors(R%, 1)
                '        Else
                '            'Converge Factors
                '            EffectAdd = ConvergeOverTime(SmokingFactors(R%, 1), 0, 100) 'Base, Target, Years
                '        End If
                Call XYTABL(TbName$, Amin(GDPPCP(R%), 50), FormValue, context)
                HLSMOKING(R%, 1) = FormValue + EffectAdd
                'jrs 2009/02/09
                HLSMOKING(R%, 1) = CSng(AMAX(HLSMOKING(R%, 1), 0.0001))
                'jrs 2008/12/04
                If Not context.HistoricMode Then
                    'Moving Avg 90% Comp. 10 yr and 10% Regression growth
                    SmkCmpGr = CSng(((SHLSMOKING(R%, 1, 10) / AMAX(SHLSMOKING(R%, 1, 0), 0.0001)) ^ (1 / 10)) - 1)
                    'save first year
                    If IY% = 2 Then
                        SHLSMOKING(R%, 1, 11) = SHLSMOKING(R%, 1, 10)
                    End If
                    SmkGr = CSng((HLSMOKING(R%, 1) - SHLSMOKING(R%, 1, 11)) / AMAX(SHLSMOKING(R%, 1, 11), 0.0001)) 'jrs 2011/10/26 added Amax
                    'save regression number
                    SHLSMOKING(R%, 1, 11) = HLSMOKING(R%, 1)
                    HLSMOKING(R%, 1) = CSng(SHLSMOKING(R%, 1, 10) * (1 + (0.9 * SmkCmpGr + 0.1 * SmkGr)))
                    'converge to regression
                    HLSMOKING(R%, 1) = ConvergeOverTime(HLSMOKING(R%, 1), SHLSMOKING(R%, 1, 11), 100 - IY) 'Base, Target, Years
                    'Avoid growth for rich countries if already in downward trend
                    If GDPPCIP(R%) > 25 And HLSMOKING(R%, 1) > SHLSMOKING(R%, 1, 10) And SHLSMOKING(R%, 1, 10) <= SHLSMOKING(R%, 1, 9) Then
                        HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 10)
                    End If
                    'move historic numbers
                    For j = 1 To 10
                        SHLSMOKING(R%, 1, j - 1) = SHLSMOKING(R%, 1, j)
                    Next j
                    SHLSMOKING(R%, 1, 10) = HLSMOKING(R%, 1)
                End If
            Next R%

            'jrs 2008/07/15 changed from Linear to Quadratic
            'jrs 2024/06/17 TbName$ = "GDP/Capita (PPP 2000) Versus Female Smoking Rate (" & CStr(BaseYear) & ") Quadratic"
            TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Female Smoking Rate (" & CStr(BaseYear) & ") Quadratic"
            TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            For R% = 1 To NR%
                'jrs 2008/07/24
                If SmokingFactors(R%, 2) < 0 And GDPPCIP(R%) > 25 Then
                    EffectAdd = SmokingFactors(R%, 2)
                Else
                    'Converge Factors
                    EffectAdd = ConvergeOverTime(SmokingFactors(R%, 2), 0, 100) 'Base, Target, Years
                End If
                'jrs 2008/07/18 limit to $30k
                Call XYTABL(TbName$, Amin(GDPPCP(R%), 30), FormValue, context)
                HLSMOKING(R%, 2) = FormValue + EffectAdd
                'jrs 2009/02/09
                HLSMOKING(R%, 2) = CSng(AMAX(HLSMOKING(R%, 2), 0.0001))
                'jrs 2008/12/04
                If Not context.HistoricMode Then
                    'Moving Avg 90% Comp. 10 yr and 10% Regression growth
                    SmkCmpGr = CSng(((SHLSMOKING(R%, 2, 10) / AMAX(SHLSMOKING(R%, 2, 0), 0.0001)) ^ (1 / 10)) - 1)
                    'save first year
                    If IY% = 2 Then
                        SHLSMOKING(R%, 2, 11) = SHLSMOKING(R%, 2, 10)
                    End If
                    SmkGr = CSng((HLSMOKING(R%, 2) - SHLSMOKING(R%, 2, 11)) / AMAX(SHLSMOKING(R%, 2, 11), 0.0001)) 'jrs 2011/10/26 added AMAX
                    'jrs 2014/01/20
                    'This growth limit was taken from the regression equation
                    'The steepest slope of the quadratic formulation is 0.7 when x=0
                    If SHLSMOKING(R%, 2, 11) <= 0.0001 Then
                        SmkGr = CSng(Amin(SmkGr, 0.7))
                    End If
                    'save regression number
                    SHLSMOKING(R%, 2, 11) = HLSMOKING(R%, 2)
                    HLSMOKING(R%, 2) = CSng(SHLSMOKING(R%, 2, 10) * (1 + (0.9 * SmkCmpGr + 0.1 * SmkGr)))
                    'converge to regression
                    HLSMOKING(R%, 2) = ConvergeOverTime(HLSMOKING(R%, 2), SHLSMOKING(R%, 2, 11), 100 - IY) 'Base, Target, Years
                    'Avoid growth for reach countries if already in downward trend
                    If GDPPCIP(R%) > 25 And HLSMOKING(R%, 2) > SHLSMOKING(R%, 2, 10) And SHLSMOKING(R%, 2, 10) <= SHLSMOKING(R%, 2, 9) Then
                        HLSMOKING(R%, 2) = SHLSMOKING(R%, 2, 10)
                    End If
                    'move historic numbers
                    For j = 1 To 10
                        SHLSMOKING(R%, 2, j - 1) = SHLSMOKING(R%, 2, j)
                    Next j
                    SHLSMOKING(R%, 2, 10) = HLSMOKING(R%, 2)
                End If
            Next R%
        End Sub

        'jrs 2011/05/17 *** new procedure for Smoking in Stages ***
        Private Sub Health_Forecast_Smoking_InStages(context As IForecastContext)

            If IY = 2 Then
                'If context.HistoricMode = True Then
                'Loads # of countries and region codes
                '--------------------------------------
                'LoadCountries dbIFs
                'End If

                'Tobacco Control Score
                '---------------------
                Dim CPop As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                If context.HistoricMode Then
                    For Each item In (From s In context.GetSlice("POP", "Earliest")
                                      Select Key = s.Key, Value = If(s.Value, 1))
                        CPop.Add(item.Key, item.Value)
                    Next
                Else
                    For Each item In (From s In context.GetSlice("POP", BaseYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = If(s.Value, 1))
                        CPop.Add(item.Key, item.Value)
                    Next
                End If

                Dim CTCS As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                ReDim RTCS(NR)

                If context.HistoricMode Then
                    For Each item In (From s In context.GetSlice("SeriesHealthSmokingTCS", "Earliest")
                                      Select Key = s.Key, Value = If(s.Value, 0))
                        CTCS.Add(item.Key, item.Value)
                    Next
                Else
                    For Each item In (From s In context.GetSlice("SeriesHealthSmokingTCS", BaseYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = If(s.Value, 0))
                        CTCS.Add(item.Key, item.Value)
                    Next
                End If

                'Call RegAvgOneDimInMem(CTCS, CPop, RTCS, "TCS") 'jrs 2012/02/28
                For Each item In (From s In CTCS
                                  Join r In context.Regionalization On s.Key Equals r.Key
                                  Join w In CPop On s.Key Equals w.Key
                                  Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
               ).ToDictionary(
                     Function(grp) grp.RegionId,
                     Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
               )
                    RTCS(item.Key) = CSng(item.Value)
                Next

            End If


            Dim SmkCmpGr As Single
            Dim A As Single, B As Single, C As Single, yp As Single, Y As Integer
            'jrs 2011/12/09
            'gvdb 2024/04/05
            'unused variable
            'Dim PrevYrSmkRt As Single
            'jrs 2012/01/11
            Dim GrRt As Single
            For R% = 1 To NR%
                If IY = 2 Then
                    SMOKINGTIME(R, 1) = 1
                    SMOKINGTIME(R, 2) = 1
                    SHLSMOKING(R%, 1, 11) = SHLSMOKING(R%, 1, 10)
                    SHLSMOKING(R%, 2, 11) = SHLSMOKING(R%, 2, 10)
                    SHLSMOKING(R%, 1, 12) = SHLSMOKING(R%, 1, 10)
                    SHLSMOKING(R%, 2, 12) = SHLSMOKING(R%, 2, 10)
                End If

                'jrs 2011/10/28
                If hlsmokingtcsm(R%) <> 1 Then
                    'jrs 2011/11/23 added min and max checks
                    hlsmokingtcs(R%) = AMAX(Amin(hlsmokingtcs(R%) * hlsmokingtcsm(R%), TCSMax), TCSMin)
                End If
                'jrs 2011/09/09
                'Tobacco Control Score
                If Abs(CSng(hlsmokingtcs(R%) - RTCS(R%))) > 0.0001 Then 'jrs 2012/10/19 don't know why but the parameter has lots of un wanted decimals
                    hlsmokingincm = CSng((1 + (RTCS(R%) - hlsmokingtcs(R%)) / 100 * hlsmokingtcsel))
                    hlsmokingdecm = CSng((1 - (RTCS(R%) - hlsmokingtcs(R%)) / 100 * hlsmokingtcsel))
                End If

                'Males
                Select Case SMOKINGGRP(R%, 1)
                    Case 1 'Early to Rising
                        SmkCmpGr = CSng(((SHLSMOKING(R%, 1, 10) / AMAX(SHLSMOKING(R%, 1, 0), 0.0001)) ^ (1 / 10)) - 1)
                        If SmkCmpGr < 0.05 Then
                            SmkCmpGr = ConvergeOverTime(SmkCmpGr, 0.05, 10)
                        End If
                        'Smoothing transition
                        If SHLSMOKING(R%, 1, 10) > 12 Then
                            If SHLSMOKING(R%, 1, 9) < 12 Or IY = 2 Then
                                yp = SHLSMOKING(R%, 1, 10) * (1 + SmkCmpGr)
                                Y = 0
                                Do While yp < 15
                                    yp = CSng(yp * (1 + (SmkCmpGr + 0.01) / 2))
                                    Y = Y + 1
                                    If Y > 20 Then Exit Do
                                Loop
                                SMOKINGEXPREG(R, 3, 1) = IY
                                SMOKINGEXPREG(R, 4, 1) = Y
                            End If
                            SmkCmpGr = ConvergeOverTimeStartX(SmkCmpGr, 0.01, CInt(SMOKINGEXPREG(R, 3, 1) + SMOKINGEXPREG(R, 4, 1)), CInt(SMOKINGEXPREG(R, 3, 1)))
                        End If

                        HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 10) * (1 + SmkCmpGr)
                        'Transition to next group
                        If HLSMOKING(R%, 1) > 15 Then
                            SMOKINGGRP(R%, 1) = 2
                            SMOKINGTIME(R, 1) = 0
                            SHLSMOKING(R%, 1, 11) = HLSMOKING(R%, 1)
                        End If
                    Case 2 'Rising to Peak
                        If SMOKINGTIME(R, 1) = 1 Then
                            SMOKINGEXPREG(R, 0, 1) = SHLSMOKING(R%, 1, 10) / SHLSMOKING(R%, 1, 9)
                        End If
                        If SHLSMOKING(R%, 1, 11) * SMOKINGEXPREG(R, 0, 1) ^ SMOKINGTIME(R, 1) < SHLSMOKING(R%, 1, 11) * Exp(0.01 * SMOKINGTIME(R, 1)) Or SMOKINGTIME(R, 1) < IY - 1 Then
                            HLSMOKING(R%, 1) = CSng(SHLSMOKING(R%, 1, 11) * Exp(0.01 * SMOKINGTIME(R, 1)))
                        Else
                            'jrs 2012/01/11 HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 10) * (SHLSMOKING(R%, 1, 10) / SHLSMOKING(R%, 1, 9))
                            'Constant growth
                            GrRt = SMOKINGEXPREG(R, 0, 1)
                            HLSMOKING(R%, 1) = CSng(SHLSMOKING(R%, 1, 11) * GrRt ^ SMOKINGTIME(R, 1))
                            'Avoid compounding of multiplier
                            '                    If SMOKINGTIME(R, 1) > 1 And SMOKINGEXPREG(R, 0, 1) <> 0 Then
                            '                        HLSMOKING(R%, 1) = HLSMOKING(R%, 1) / SMOKINGEXPREG(R, 0, 1)
                            '                    End If
                        End If
                        'jrs 2012/01/11 HLSMOKING(R%, 1) = HLSMOKING(R%, 1) * hlsmokingincm
                        If hlsmokingincm <> 1 Then
                            GrRt = (HLSMOKING(R%, 1) - SHLSMOKING(R%, 1, 11)) * hlsmokingincm
                            HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 11) + GrRt
                        End If

                        'SMOKINGEXPREG(R, 0, 1) = hlsmokingincm
                        'Slow down transition to next group
                        If HLSMOKING(R%, 1) >= hlsmokingceiling(1) - 3 Or (hlsmokingpeakyr - FirstYear - IY + 1 < 5 And hlsmokingpeakyr >= FirstYear) Then
                            If SHLSMOKING(R%, 1, 10) < hlsmokingceiling(1) - 3 Or (hlsmokingpeakyr - FirstYear - IY + 1 = 4 And hlsmokingpeakyr >= FirstYear) Then
                                'JRS 2011/12/09
                                HLSMOKING(R%, 1) = Amin(HLSMOKING(R%, 1), AMAX(hlsmokingceiling(1), SHLSMOKING(R%, 1, 10)))
                                'y'=s1=2ax1+b -> slope in current year
                                'b=s1-2ax1

                                'y1=ax1^2+(s1-2ax1)x1+c -> current point assuming quadratic equation
                                'y1=s1x1-ax1^2+c
                                'c=y1+ax1^2-s1x1

                                'y'=0=2axp+b -> slope is 0 at p(eak)
                                'xp=-b/(2a)=(2ax1-s1)/(2a)

                                'yp=a[(2ax1-s1)/(2a)]^2+(s1-2ax1)[(2ax1-s1)/(2a)]+y1+ax1^2-s1x1
                                'yp=-(2ax1-s1)^2/(4a)+y1+ax1^2-s1x1
                                '4ayp=-(2ax1-s1)^2+[y1-s1x1]4a+4a^2x1^2
                                '0=-(4a^2x1^2-4ax1s1+s1^2)+4ay1-4as1x1+4a^2x1^2-4ayp
                                '0=-(s1^2)+4ay1-4ayp
                                'a=(s1^2)/(4*[y1-yp])

                                'yp is the peak value assuming constant growth
                                yp = HLSMOKING(R, 1)
                                Y = 0
                                Do While yp < hlsmokingceiling(1) Or (hlsmokingpeakyr > FirstYear And Y < 5)
                                    yp = yp * HLSMOKING(R, 1) / SHLSMOKING(R%, 1, 10)
                                    'jrs 2011/10/24
                                    If Abs(HLSMOKING(R, 1) - SHLSMOKING(R%, 1, 10)) < 0.0001 Then
                                        Exit Do
                                    End If
                                    Y = Y + 1
                                Loop

                                'jrs 2011/10/24
                                If Abs(HLSMOKING(R, 1) - SHLSMOKING(R%, 1, 10)) >= 0.0001 And Abs(HLSMOKING(R%, 1) - yp) > 0 Then
                                    A = CSng((HLSMOKING(R%, 1) - SHLSMOKING(R%, 1, 10)) ^ 2 / (4 * (HLSMOKING(R%, 1) - yp)))
                                    C = CSng(HLSMOKING(R%, 1) + A * (FirstYear + IY - 1) ^ 2 - (FirstYear + IY - 1) * (HLSMOKING(R%, 1) - SHLSMOKING(R%, 1, 10)))
                                    B = HLSMOKING(R%, 1) - SHLSMOKING(R%, 1, 10) - 2 * A * (FirstYear + IY - 1)

                                    SMOKINGTRANCOEFF(R, 1, 1) = A
                                    SMOKINGTRANCOEFF(R, 2, 1) = B
                                    SMOKINGTRANCOEFF(R, 3, 1) = C
                                    SMOKINGTRANCOEFF(R, 4, 1) = yp
                                Else
                                    SMOKINGTRANCOEFF(R, 1, 1) = Nothing
                                    SMOKINGTRANCOEFF(R, 2, 1) = Nothing
                                    SMOKINGTRANCOEFF(R, 3, 1) = Nothing
                                    SMOKINGTRANCOEFF(R, 4, 1) = Nothing
                                End If

                            Else
                                If Not SMOKINGTRANCOEFF(R, 1, 1).HasValue Then
                                    SMOKINGTRANCOEFF(R, 4, 1) = Amin(HLSMOKING(R%, 1), hlsmokingceiling(1))
                                    'jrs 2011/12/12
                                    HLSMOKING(R%, 1) = Amin(HLSMOKING(R%, 1), hlsmokingceiling(1))
                                Else
                                    A = CSng(SMOKINGTRANCOEFF(R, 1, 1))
                                    B = CSng(SMOKINGTRANCOEFF(R, 2, 1))
                                    C = CSng(SMOKINGTRANCOEFF(R, 3, 1))

                                    HLSMOKING(R%, 1) = CSng(A * (FirstYear + IY - 1) ^ 2 + B * (FirstYear + IY - 1) + C)
                                End If
                            End If
                        End If
                        'Transition to next group
                        If Abs(CSng(HLSMOKING(R%, 1) - SMOKINGTRANCOEFF(R, 4, 1))) < 0.06 Or HLSMOKING(R%, 1) < SHLSMOKING(R%, 1, 10) Or hlsmokingpeakyr = FirstYear + IY - 1 Then
                            SMOKINGGRP(R%, 1) = 3
                            SMOKINGTIME(R, 1) = 0
                            SMOKINGPEAKYR(R, 1) = FirstYear + IY - 1
                            'jrs 2012/11/02
                            If SHLSMOKING(R%, 1, 11) <= hlsmokingceiling(1) Then
                                If HLSMOKING(R%, 1) > hlsmokingceiling(1) Then
                                    HLSMOKING(R%, 1) = hlsmokingceiling(1)
                                End If
                            End If
                            SHLSMOKING(R%, 1, 11) = HLSMOKING(R%, 1)
                            SHLSMOKING(R%, 1, 12) = HLSMOKING(R%, 1)
                        End If
                    Case 3 'Peak to Falling
                        'if peaks during historic estimate
                        If SMOKINGPEAKYR(R, 1) < BaseYear Then
                            If BaseYear + IY - 1 - SMOKINGPEAKYR(R, 1) <= 10 Then
                                'derivative to find growth
                                'y = a*b^x
                                'y'= a*ln(b)b^x
                                HLSMOKING(R%, 1) = CSng(SHLSMOKING(R%, 1, 12) + (SMOKINGEXPREG(R, 1, 1) * Log(SMOKINGEXPREG(R, 2, 1)) * SMOKINGEXPREG(R, 2, 1) ^ (BaseYear + IY - 1)))
                                'jrs 2011/12/09 avoid compounding
                                SHLSMOKING(R%, 1, 12) = HLSMOKING(R%, 1)
                                'jrs 2012/01/11 HLSMOKING(R%, 1) = HLSMOKING(R%, 1) * hlsmokingdecm
                                If hlsmokingdecm <> 1 Then
                                    GrRt = (HLSMOKING(R%, 1) - SHLSMOKING(R%, 1, 11)) * hlsmokingdecm
                                    HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 11) + GrRt
                                End If
                            Else 'after 10 years faster exponential decrease
                                If BaseYear + IY - 1 - SMOKINGPEAKYR(R, 1) = 11 Then
                                    SMOKINGTIME(R, 1) = 1
                                    SHLSMOKING(R%, 1, 11) = SHLSMOKING(R%, 1, 10)
                                    SHLSMOKING(R%, 1, 12) = SHLSMOKING(R%, 1, 10)
                                End If
                                'Smooth out transition
                                HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 12) + ConvergeOverTimeStartX(CSng((SMOKINGEXPREG(R, 1, 1) * Log(SMOKINGEXPREG(R, 2, 1)) * SMOKINGEXPREG(R, 2, 1) ^ (BaseYear + IY - 1))),
                                CSng(SHLSMOKING(R%, 1, 11) * (Exp(-0.01 * (SMOKINGTIME(R, 1) + 1)) - Exp(-0.01 * SMOKINGTIME(R, 1)))), CInt(SMOKINGPEAKYR(R, 1) + 15 - BaseYear + 1), CInt(SMOKINGPEAKYR(R, 1) + 11 - BaseYear + 1))
                                'jrs 2011/12/09 avoid compounding
                                SHLSMOKING(R%, 1, 12) = HLSMOKING(R%, 1)
                                'jrs 2012/01/11 HLSMOKING(R%, 1) = HLSMOKING(R%, 1) * hlsmokingdecm
                                If hlsmokingdecm <> 1 Then
                                    GrRt = (HLSMOKING(R%, 1) - SHLSMOKING(R%, 1, 11)) * hlsmokingdecm
                                    HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 11) + GrRt
                                End If

                                'Faster Exponential decrease
                                'HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 11) * Exp(-0.01 * SMOKINGTIME(R))
                                If HLSMOKING(R%, 1) <= hlsmokingfloor(1) And SHLSMOKING(R%, 1, 10) <= hlsmokingfloor(1) Then
                                    'keep constant
                                    HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 10)
                                    SMOKINGTIME(R, 1) = SMOKINGTIME(R, 1) - 1
                                End If
                            End If
                        Else 'peaks during forecast
                            If SMOKINGTIME(R, 1) <= 10 Then
                                'slow Exponential decrease
                                HLSMOKING(R%, 1) = CSng(SHLSMOKING(R%, 1, 11) * Exp(-0.001 * SMOKINGTIME(R, 1)))
                                If hlsmokingdecm <> 1 Then
                                    'jrs 2012/01/11
                                    'HLSMOKING(R%, 1) = ConvergeOverTimeStartX(HLSMOKING(R%, 1), SHLSMOKING(R%, 1, 11) + _
                                    '(HLSMOKING(R%, 1) - SHLSMOKING(R%, 1, 11)) * hlsmokingdecm, _
                                    'SMOKINGPEAKYR(R, 1) + 5 - BaseYear + 1, SMOKINGPEAKYR(R, 1) + 1 - BaseYear + 1)
                                    GrRt = (HLSMOKING(R%, 1) - SHLSMOKING(R%, 1, 11)) * hlsmokingdecm
                                    HLSMOKING(R%, 1) = ConvergeOverTimeStartX(HLSMOKING(R%, 1),
                                    SHLSMOKING(R%, 1, 11) + GrRt,
                                    CInt(SMOKINGPEAKYR(R, 1) + 5 - BaseYear + 1), CInt(SMOKINGPEAKYR(R, 1) + 1 - BaseYear + 1))
                                End If
                            Else
                                'Smooth out transition
                                HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 12) +
                                ConvergeOverTimeStartX(CSng(SHLSMOKING(R%, 1, 11) * (Exp(-0.001 * SMOKINGTIME(R, 1)) - Exp(-0.001 * (SMOKINGTIME(R, 1) - 1)))),
                                CSng(SHLSMOKING(R%, 1, 11) * (Exp(-0.01 * (SMOKINGTIME(R, 1) - 10)) - Exp(-0.01 *
                                (SMOKINGTIME(R, 1) - 11)))), CInt(SMOKINGPEAKYR(R, 1) + 15 - BaseYear + 1), CInt(SMOKINGPEAKYR(R, 1) + 11 - BaseYear + 1))
                                'jrs 2011/12/09 avoid compounding
                                SHLSMOKING(R%, 1, 12) = HLSMOKING(R%, 1)
                                'jrs 2012/01/11 HLSMOKING(R%, 1) = HLSMOKING(R%, 1) * hlsmokingdecm
                                If hlsmokingdecm <> 1 Then
                                    GrRt = (HLSMOKING(R%, 1) - SHLSMOKING(R%, 1, 11)) * hlsmokingdecm
                                    HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 11) + GrRt
                                End If

                                'fast Exponential decrease
                                'HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 11) * Exp(-0.01 * (SMOKINGTIME(R) - 10))
                            End If
                            If HLSMOKING(R%, 1) <= hlsmokingfloor(1) And SHLSMOKING(R%, 1, 10) <= hlsmokingfloor(1) Then
                                'keep constant
                                HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 10)
                                SMOKINGTIME(R, 1) = SMOKINGTIME(R, 1) - 1
                            End If
                        End If
                    Case 4 'Last Stage
                        If SHLSMOKING(R%, 1, 11) >= 30 Then
                            'derivative to find growth
                            'y = a*b^x
                            'y'= a*ln(b)b^x
                            'jrs 2011/10/24
                            If SHLSMOKING(R%, 1, 10) > hlsmokingfloor(1) Then
                                HLSMOKING(R%, 1) = CSng(SHLSMOKING(R%, 1, 12) + (SMOKINGEXPREG(R, 1, 1) * Log(SMOKINGEXPREG(R, 2, 1)) * SMOKINGEXPREG(R, 2, 1) ^ (BaseYear + IY - 1)))
                                'jrs 2011/12/09 avoid compounding
                                SHLSMOKING(R%, 1, 12) = HLSMOKING(R%, 1)
                                'jrs 2012/01/11 HLSMOKING(R%, 1) = HLSMOKING(R%, 1) * hlsmokingdecm
                                If hlsmokingdecm <> 1 Then
                                    GrRt = (HLSMOKING(R%, 1) - SHLSMOKING(R%, 1, 11)) * hlsmokingdecm
                                    HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 11) + GrRt
                                End If
                            Else
                                HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 10)
                            End If
                        Else
                            'derivative to find growth
                            'y = a+bln(x)
                            'y'= b/x
                            'jrs 2011/10/24
                            If SHLSMOKING(R%, 1, 10) > hlsmokingfloor(1) Then
                                HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 10) + (SMOKINGEXPREG(R, 4, 1) / (BaseYear + IY - 1))
                                'jrs 2011/12/09 avoid compounding
                                SHLSMOKING(R%, 1, 12) = HLSMOKING(R%, 1)
                                'jrs 2012/01/11 HLSMOKING(R%, 1) = HLSMOKING(R%, 1) * hlsmokingdecm
                                If hlsmokingdecm <> 1 Then
                                    GrRt = (HLSMOKING(R%, 1) - SHLSMOKING(R%, 1, 11)) * hlsmokingdecm
                                    HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 11) + GrRt
                                End If

                            Else
                                HLSMOKING(R%, 1) = SHLSMOKING(R%, 1, 10)
                            End If
                        End If
                        'jrs 2011/10/24
                        '                If HLSMOKING(R%, 1) <= hlsmokingfloor(1) Then
                        '                    SMOKINGEXPREG(R, 1, 1) = 0
                        '                    SMOKINGEXPREG(R, 4, 1) = 0
                        '                End If
                End Select
                'jrs 2011/06/16
                'Females *****************************************************************************************************************************************
                Select Case SMOKINGGRP(R%, 2)
                    Case 1 'Early to Rising
                        SmkCmpGr = CSng(((SHLSMOKING(R%, 2, 10) / AMAX(SHLSMOKING(R%, 2, 9), 0.0001))) - 1)
                        HLSMOKING(R%, 2) = SHLSMOKING(R%, 2, 10) * (1 + SmkCmpGr)
                        'Transition to next group
                        If HLSMOKING(R%, 2) > 1 Then
                            SMOKINGGRP(R%, 2) = 2
                            SMOKINGTIME(R, 2) = 0
                            SHLSMOKING(R%, 2, 11) = HLSMOKING(R%, 2)
                        End If
                    Case 2 'Rising to Peak
                        If SMOKINGTIME(R, 2) < IY - 1 Then
                            HLSMOKING(R%, 2) = CSng(SHLSMOKING(R%, 2, 11) * Exp(0.01 * SMOKINGTIME(R, 2)))
                        Else
                            'jrs 2011/12/09
                            If SMOKINGTIME(R, 2) = 1 Then
                                SHLSMOKING(R%, 2, 12) = SHLSMOKING(R%, 2, 10)
                            End If
                            'derivative to find growth
                            'y = a*b^x
                            'y'= a*ln(b)b^x
                            HLSMOKING(R%, 2) = CSng(SHLSMOKING(R%, 2, 12) + (SMOKINGEXPREG(R, 1, 2) * Log(SMOKINGEXPREG(R, 2, 2)) * SMOKINGEXPREG(R, 2, 2) ^ (BaseYear + IY - 1)))
                            'jrs 2011/12/09 avoid compounding of multiplier
                            SHLSMOKING(R%, 2, 12) = HLSMOKING(R%, 2)
                        End If
                        'jrs 2011/07/26 maximum growth of 2.5%
                        HLSMOKING(R%, 2) = CSng(Amin(HLSMOKING(R%, 2), SHLSMOKING(R%, 2, 10) + 0.5)) 'jrs 2011/09/09 used to be 2.5, but Cecilia didn't like it
                        'jrs 2012/01/11 HLSMOKING(R%, 2) = HLSMOKING(R%, 2) * hlsmokingincm
                        If hlsmokingincm <> 1 Then
                            GrRt = (HLSMOKING(R%, 2) - SHLSMOKING(R%, 2, 11)) * hlsmokingincm
                            HLSMOKING(R%, 2) = SHLSMOKING(R%, 2, 11) + GrRt
                        End If
                        'Slow down transition to next group
                        If HLSMOKING(R%, 2) >= hlsmokingceiling(2) - 5 Or (hlsmokingpeakyr - FirstYear - IY + 1 < 5 And hlsmokingpeakyr >= FirstYear) Then
                            If SHLSMOKING(R%, 2, 10) < hlsmokingceiling(2) - 5 Or (hlsmokingpeakyr - FirstYear - IY + 1 = 4 And hlsmokingpeakyr >= FirstYear) Then
                                'JRS 2011/12/09
                                HLSMOKING(R%, 2) = Amin(HLSMOKING(R%, 2), AMAX(hlsmokingceiling(2), SHLSMOKING(R%, 2, 10)))
                                'y'=s1=2ax1+b -> slope in current year
                                'b=s1-2ax1

                                'y1=ax1^2+(s1-2ax1)x1+c -> current point assuming quadratic equation
                                'y1=s1x1-ax1^2+c
                                'c=y1+ax1^2-s1x1

                                'y'=0=2axp+b -> slope is 0 at p(eak)
                                'xp=-b/(2a)=(2ax1-s1)/(2a)

                                'yp=a[(2ax1-s1)/(2a)]^2+(s1-2ax1)[(2ax1-s1)/(2a)]+y1+ax1^2-s1x1
                                'yp=-(2ax1-s1)^2/(4a)+y1+ax1^2-s1x1
                                '4ayp=-(2ax1-s1)^2+[y1-s1x1]4a+4a^2x1^2
                                '0=-(4a^2x1^2-4ax1s1+s1^2)+4ay1-4as1x1+4a^2x1^2-4ayp
                                '0=-(s1^2)+4ay1-4ayp
                                'a=(s1^2)/(4*[y1-yp])

                                'yp is the peak value assuming constant growth
                                yp = HLSMOKING(R, 2)
                                Y = 0
                                Do While yp < hlsmokingceiling(2) Or (hlsmokingpeakyr > FirstYear And Y < 5)
                                    yp = yp * HLSMOKING(R, 2) / SHLSMOKING(R%, 2, 10)
                                    Y = Y + 1
                                    'jrs 2011/10/24
                                    If Abs(HLSMOKING(R, 2) - SHLSMOKING(R%, 2, 10)) < 0.0001 Then
                                        Exit Do
                                    End If
                                Loop

                                'jrs 2011/10/24
                                If Abs(HLSMOKING(R, 2) - SHLSMOKING(R%, 2, 10)) >= 0.0001 And Abs(HLSMOKING(R%, 2) - yp) > 0 Then
                                    A = CSng((HLSMOKING(R%, 2) - SHLSMOKING(R%, 2, 10)) ^ 2 / (4 * (HLSMOKING(R%, 2) - yp)))
                                    C = CSng(HLSMOKING(R%, 2) + A * (FirstYear + IY - 1) ^ 2 - (FirstYear + IY - 1) * (HLSMOKING(R%, 2) - SHLSMOKING(R%, 2, 10)))
                                    B = HLSMOKING(R%, 2) - SHLSMOKING(R%, 2, 10) - 2 * A * (FirstYear + IY - 1)

                                    SMOKINGTRANCOEFF(R, 1, 2) = A
                                    SMOKINGTRANCOEFF(R, 2, 2) = B
                                    SMOKINGTRANCOEFF(R, 3, 2) = C
                                    SMOKINGTRANCOEFF(R, 4, 2) = yp
                                Else
                                    SMOKINGTRANCOEFF(R, 1, 2) = Nothing
                                    SMOKINGTRANCOEFF(R, 2, 2) = Nothing
                                    SMOKINGTRANCOEFF(R, 3, 2) = Nothing
                                    SMOKINGTRANCOEFF(R, 4, 2) = Nothing
                                End If

                            Else
                                If Not SMOKINGTRANCOEFF(R, 1, 2).HasValue Then
                                    SMOKINGTRANCOEFF(R, 4, 2) = Amin(HLSMOKING(R%, 2), hlsmokingceiling(2))
                                    'jrs 2011/12/12
                                    HLSMOKING(R%, 2) = Amin(HLSMOKING(R%, 2), hlsmokingceiling(2))
                                Else
                                    A = CSng(SMOKINGTRANCOEFF(R, 1, 2))
                                    B = CSng(SMOKINGTRANCOEFF(R, 2, 2))
                                    C = CSng(SMOKINGTRANCOEFF(R, 3, 2))

                                    HLSMOKING(R%, 2) = CSng(A * (FirstYear + IY - 1) ^ 2 + B * (FirstYear + IY - 1) + C)
                                End If
                            End If
                        End If
                        'Transition to next group
                        If Abs(CSng(HLSMOKING(R%, 2) - SMOKINGTRANCOEFF(R, 4, 2))) < 0.06 Or HLSMOKING(R%, 2) < SHLSMOKING(R%, 2, 10) Or hlsmokingpeakyr = FirstYear + IY - 1 Then
                            SMOKINGGRP(R%, 2) = 3
                            'SMOKINGTIME(R) = 0
                            SMOKINGPEAKYR(R, 2) = FirstYear + IY - 1
                            'jrs 2012/11/02
                            If SHLSMOKING(R%, 2, 11) <= hlsmokingceiling(2) Then
                                If HLSMOKING(R%, 2) > hlsmokingceiling(2) Then
                                    HLSMOKING(R%, 2) = hlsmokingceiling(2)
                                End If
                            End If

                            SHLSMOKING(R%, 2, 11) = HLSMOKING(R%, 2)
                            SHLSMOKING(R%, 2, 12) = HLSMOKING(R%, 2)
                        End If
                    Case 3 'Peak to Falling
                        'slow logarithmic decline
                        HLSMOKING(R%, 2) = CSng(SHLSMOKING(R%, 2, 10) - 0.05 * Log(FirstYear + IY - 1 - SMOKINGPEAKYR(R, 2)))
                        'jrs 2011/12/09 avoid compounding
                        SHLSMOKING(R%, 2, 12) = HLSMOKING(R%, 2)
                        'jrs 2012/01/11 HLSMOKING(R%, 2) = HLSMOKING(R%, 2) * hlsmokingdecm
                        If hlsmokingdecm <> 1 Then
                            GrRt = (HLSMOKING(R%, 2) - SHLSMOKING(R%, 2, 11)) * hlsmokingdecm
                            HLSMOKING(R%, 2) = SHLSMOKING(R%, 2, 11) + GrRt
                        End If

                        If HLSMOKING(R%, 2) <= hlsmokingfloor(2) And SHLSMOKING(R%, 2, 10) <= hlsmokingfloor(2) Then
                            'keep constant
                            HLSMOKING(R%, 2) = SHLSMOKING(R%, 2, 10)
                            'SMOKINGTIME(R) = SMOKINGTIME(R) - 1
                        End If
                    Case 4 'Last Stage
                        'derivative to find growth
                        'y = a+bln(x)
                        'y'= b/x
                        HLSMOKING(R%, 2) = SHLSMOKING(R%, 2, 10) + (SMOKINGEXPREG(R, 4, 2) / (BaseYear + IY - 1))
                        'jrs 2011/12/09 avoid compounding
                        SHLSMOKING(R%, 2, 12) = HLSMOKING(R%, 2)
                        'jrs 2012/01/11 HLSMOKING(R%, 2) = HLSMOKING(R%, 2) * hlsmokingdecm
                        If hlsmokingdecm <> 1 Then
                            GrRt = (HLSMOKING(R%, 2) - SHLSMOKING(R%, 2, 11)) * hlsmokingdecm
                            HLSMOKING(R%, 2) = SHLSMOKING(R%, 2, 11) + GrRt
                        End If


                        If HLSMOKING(R%, 2) <= hlsmokingfloor(2) And SHLSMOKING(R%, 2, 10) <= hlsmokingfloor(2) Then
                            'keep constant
                            HLSMOKING(R%, 2) = SHLSMOKING(R%, 2, 10)
                            'SMOKINGTIME(R) = SMOKINGTIME(R) - 1
                        End If
                End Select

                'move historic numbers
                For j = 1 To 10
                    SHLSMOKING(R%, 1, j - 1) = SHLSMOKING(R%, 1, j)
                    SHLSMOKING(R%, 2, j - 1) = SHLSMOKING(R%, 2, j)
                Next j
                SHLSMOKING(R%, 1, 10) = HLSMOKING(R%, 1)
                SHLSMOKING(R%, 2, 10) = HLSMOKING(R%, 2)
                SMOKINGTIME(R, 1) = SMOKINGTIME(R, 1) + 1
                SMOKINGTIME(R, 2) = SMOKINGTIME(R, 2) + 1
            Next R%

        End Sub

        'jrs 2009/10/30
        Private Sub Health_Forecast_BMI(context As IForecastContext)
            Dim TbName As String
            Dim FormValue As Single
            TbName$ = "Calories per Capita Versus Female BMI (" & CStr(BaseYear) & ") Linear"
            TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
            For R% = 1 To NR%
                Call XYTABL(TbName$, CLPC(R%, 4), FormValue, context)
                HLBMI(R%, 2) = CSng(BMIfullfactorsF(R%) * FormValue)
            Next R%

            TbName$ = "Calories per Capita Versus Male BMI (" & CStr(BaseYear) & ") Linear"
            TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
            For R% = 1 To NR%
                Call XYTABL(TbName$, CLPC(R%, 4), FormValue, context)
                HLBMI(R%, 1) = CSng(BMIfullfactorsM(R%) * FormValue)
            Next R%

            'jrs 2010/02/23
            'Moved from Health_Forecast_Multipliers
            If IY > 1 Then
                For R = 1 To NR
                    'jrs 2009/11/18
                    '            'jrs 2009/09/01
                    '            HLBMI(R%, 1) = HLBMI(R%, 1) * hlbmim(R%, 1) * hlbmim(R%, 3)
                    '            HLBMI(R%, 2) = HLBMI(R%, 2) * hlbmim(R%, 2) * hlbmim(R%, 3)
                    'using a limit of 21 for downward multipliers
                    If hlbmim(R%, 1) * hlbmim(R%, 3) > 1 Then
                        HLBMI(R%, 1) = Amin(HLBMI(R%, 1) * hlbmim(R%, 1) * hlbmim(R%, 3), 40)
                    ElseIf hlbmim(R%, 1) * hlbmim(R%, 3) < 1 Then
                        If HLBMI(R%, 1) > 21 Then
                            HLBMI(R%, 1) = HLBMI(R%, 1) - (HLBMI(R%, 1) - 21) * (1 - hlbmim(R%, 1) * hlbmim(R%, 3))
                        End If
                    End If
                    If hlbmim(R%, 2) * hlbmim(R%, 3) > 1 Then
                        HLBMI(R%, 2) = Amin(HLBMI(R%, 2) * hlbmim(R%, 2) * hlbmim(R%, 3), 40)
                    ElseIf hlbmim(R%, 2) * hlbmim(R%, 3) < 1 Then
                        If HLBMI(R%, 2) > 21 Then
                            HLBMI(R%, 2) = HLBMI(R%, 2) - (HLBMI(R%, 2) - 21) * (1 - hlbmim(R%, 2) * hlbmim(R%, 3))
                        End If
                    End If
                Next R
            End If
        End Sub

        'jrs 2009/10/30
        Private Sub Health_Forecast_Obesity(context As IForecastContext)
            Dim TbName As String
            Dim EffectAdd, FormValue As Single
            'jrs 2008/08/21 TbName$ = "Calories per Capita Versus Male Obesity Rate (" & CStr(BaseYear) & ") Linear"
            'jrs 2009/09/01 TbName$ = "Calories per Capita Versus Obesity Rate Adult Males (" & CStr(BaseYear) & ") Linear"
            'jrs 2009/09/01 TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear)
            TbName$ = "BMI Versus Male Obesity % (CRA) Quad"
            For R% = 1 To NR%
                'Converge Factors
                'jrs 2007/06/27 EffectAdd = ConvergeOverTime(ObesityFactors(R%, 1), 0, 50) 'Base, Target, Years
                EffectAdd = ObesityFactors(R%, 1)
                Call XYTABL(TbName$, HLBMI(R%, 1), FormValue, context)
                HLOBESITY(R%, 1) = FormValue + EffectAdd
            Next R%

            'jrs 2008/08/21 TbName$ = "Calories per Capita Versus Female Obesity Rate (" & CStr(BaseYear) & ") Linear"
            'jrs 2009/09/01 TbName$ = "Calories per Capita Versus Obesity Rate Adult Females (" & CStr(BaseYear) & ") Linear"
            'jrs 2009/09/01 TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear)
            TbName$ = "BMI Versus Female Obesity % (CRA) Quad"
            For R% = 1 To NR%
                'Converge Factors
                'jrs 2007/06/27 EffectAdd = ConvergeOverTime(ObesityFactors(R%, 2), 0, 50) 'Base, Target, Years
                EffectAdd = ObesityFactors(R%, 2)
                Call XYTABL(TbName$, HLBMI(R%, 2), FormValue, context)
                HLOBESITY(R%, 2) = FormValue + EffectAdd
            Next R%
        End Sub

        'jrs 2012/01/10 added parameter TCSIdx
        'jrs 2009/11/18
        Private Sub Health_Forecast_Multipliers()
            'JRS 2011/09/09
            '    If IY = 1 Then
            '        ReDim SHLSMOKINGTCS(NR)
            '    End If

            For R% = 1 To NR%
                'jrs 2009/07/10
                'Save obesity numbers
                '        If IY = 1 Then
                '            HLOBESITYI(R%, 1) = HLOBESITY(R%, 1)
                '            HLOBESITYI(R%, 2) = HLOBESITY(R%, 2)
                '        End If

                'jrs 2009/09/01
                If IY > 1 Then
                    HLOBESITY(R%, 1) = HLOBESITY(R%, 1) * hlobesitym(R%, 1) * hlobesitym(R%, 3)
                    HLOBESITY(R%, 2) = HLOBESITY(R%, 2) * hlobesitym(R%, 2) * hlobesitym(R%, 3)

                    HLSMOKING(R%, 1) = HLSMOKING(R%, 1) * hlsmokingm(R%, 1) * hlsmokingm(R%, 3)
                    HLSMOKING(R%, 2) = HLSMOKING(R%, 2) * hlsmokingm(R%, 2) * hlsmokingm(R%, 3)

                    '            'jrs 2011/10/28
                    '            If hlsmokingtcsm(R%) <> 1 Then
                    '                'jrs 2011/11/23 added min and max checks
                    '                hlsmokingtcs(R%) = AMAX(Amin(hlsmokingtcs(R%) * hlsmokingtcsm(R%), VMax(TCSIdx)), VMin(TCSIdx))
                    '            End If
                    '
                    '            'jrs 2011/09/09
                    '            'Tobacco Control Score
                    '            If hlsmokingtcs(R%) - SHLSMOKINGTCS(R%) <> 0 Then
                    '                HLSMOKING(R%, 1) = HLSMOKING(R%, 1) * (1 + (SHLSMOKINGTCS(R%) - hlsmokingtcs(R%)) / 100 * hlsmokingtcsel)
                    '                HLSMOKING(R%, 2) = HLSMOKING(R%, 2) * (1 + (SHLSMOKINGTCS(R%) - hlsmokingtcs(R%)) / 100 * hlsmokingtcsel)
                    '            End If
                Else
                    'jrs 2012/01/11 SHLSMOKINGTCS(R%) = hlsmokingtcs(R%)
                    '            SHLSMOKINGTCS(R%) = RTCS(R%)
                    '            If RTCS(R%) <> hlsmokingtcs(R%) Then
                    '                HLSMOKING(R%, 1) = HLSMOKING(R%, 1) * (1 + (SHLSMOKINGTCS(R%) - hlsmokingtcs(R%)) / 100 * hlsmokingtcsel)
                    '                HLSMOKING(R%, 2) = HLSMOKING(R%, 2) * (1 + (SHLSMOKINGTCS(R%) - hlsmokingtcs(R%)) / 100 * hlsmokingtcsel)
                    '            End If
                End If
            Next R%
        End Sub

        'jrs 2009/12/04
        Private Sub CheckSmokObMinMax()
            For R% = 1 To NR%
                'jrs 2011/10/25
                If hlsmokingstsw = 0 Or IY = 1 Then
                    If HLSMOKING(R%, 1) < SmokingMin Then
                        HLSMOKING(R%, 1) = SmokingMin
                    End If
                    If HLSMOKING(R%, 2) < SmokingMin Then
                        HLSMOKING(R%, 2) = SmokingMin
                    End If
                    If HLSMOKING(R%, 1) > SmokingMax Then
                        HLSMOKING(R%, 1) = SmokingMax
                    End If
                    If HLSMOKING(R%, 2) > SmokingMax Then
                        HLSMOKING(R%, 2) = SmokingMax
                    End If
                Else
                    'jrs 2011/10/31 adding checks for initial conditions
                    If SHLSMOKING(R%, 1, 11) >= hlsmokingfloor(1) Then
                        If HLSMOKING(R%, 1) < hlsmokingfloor(1) Then
                            HLSMOKING(R%, 1) = hlsmokingfloor(1)
                        End If
                    Else
                        If HLSMOKING(R%, 1) < SmokingMin Then
                            HLSMOKING(R%, 1) = SmokingMin
                        End If
                    End If

                    If SHLSMOKING(R%, 2, 11) >= hlsmokingfloor(2) Then
                        If HLSMOKING(R%, 2) < hlsmokingfloor(2) Then
                            HLSMOKING(R%, 2) = hlsmokingfloor(2)
                        End If
                    Else
                        If HLSMOKING(R%, 2) < SmokingMin Then
                            HLSMOKING(R%, 2) = SmokingMin
                        End If
                    End If

                    If SHLSMOKING(R%, 1, 11) <= hlsmokingceiling(1) Then
                        If HLSMOKING(R%, 1) > hlsmokingceiling(1) Then
                            HLSMOKING(R%, 1) = hlsmokingceiling(1)
                        End If
                    Else
                        If HLSMOKING(R%, 1) > SmokingMax Then
                            HLSMOKING(R%, 1) = SmokingMax
                        End If
                    End If

                    If SHLSMOKING(R%, 2, 11) <= hlsmokingceiling(2) Then
                        If HLSMOKING(R%, 2) > hlsmokingceiling(2) Then
                            HLSMOKING(R%, 2) = hlsmokingceiling(2)
                        End If
                    Else
                        If HLSMOKING(R%, 2) > SmokingMax Then
                            HLSMOKING(R%, 2) = SmokingMax
                        End If
                    End If
                End If

                If HLOBESITY(R%, 1) < ObesityMin Then
                    HLOBESITY(R%, 1) = ObesityMin
                End If
                If HLOBESITY(R%, 2) < ObesityMin Then
                    HLOBESITY(R%, 2) = ObesityMin
                End If
                If HLOBESITY(R%, 1) > ObesityMax Then
                    HLOBESITY(R%, 1) = ObesityMax
                End If
                If HLOBESITY(R%, 2) > ObesityMax Then
                    HLOBESITY(R%, 2) = ObesityMax
                End If
            Next R%
        End Sub

        'jrs 2009/12/11
        'Forecasting Diabetes RRs
        Private Sub Health_Forecast_DRR()
            Dim R As Integer
            Dim FormVal As Single
            'in the first year compute shift factors
            If IY = 1 Then
                ReDim HLDRRShift(NR, 6, 2) '6 age categories, 2 genders

                For R = 1 To NR
                    '1. Males:
                    '=========
                    '1.1) 15 to 29:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 15 to 29 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 1), FormVal)
                    'FormVal = Exp((HLBMI(R, 1) - 21) * 0.233)
                    FormVal = Expected_DRR(HLBMI(R, 1), 0.233)
                    FormVal = AMAX(FormVal, 1)
                    HLDRRShift(R, 1, 1) = HLDIABETESRR(R, 1, 1) / FormVal
                    '1.2) 30 to 44:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 30 to 44 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 1), FormVal)
                    'FormVal = Exp((HLBMI(R, 1) - 21) * 0.307)
                    FormVal = Expected_DRR(HLBMI(R, 1), 0.307)
                    FormVal = AMAX(FormVal, 1)
                    HLDRRShift(R, 2, 1) = HLDIABETESRR(R, 2, 1) / FormVal
                    '1.3) 45 to 59:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 45 to 59 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 1), FormVal)
                    'FormVal = Exp((HLBMI(R, 1) - 21) * 0.215)
                    FormVal = Expected_DRR(HLBMI(R, 1), 0.215)
                    FormVal = AMAX(FormVal, 1)
                    HLDRRShift(R, 3, 1) = HLDIABETESRR(R, 3, 1) / FormVal
                    '1.4) 60 to 69:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 60 to 69 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 1), FormVal)
                    'FormVal = Exp((HLBMI(R, 1) - 21) * 0.166)
                    FormVal = Expected_DRR(HLBMI(R, 1), 0.166)
                    FormVal = AMAX(FormVal, 1)
                    HLDRRShift(R, 4, 1) = HLDIABETESRR(R, 4, 1) / FormVal
                    '1.5) 70 to 79:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 70 to 79 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 1), FormVal)
                    'FormVal = Exp((HLBMI(R, 1) - 21) * 0.239)
                    FormVal = Expected_DRR(HLBMI(R, 1), 0.239)
                    FormVal = AMAX(FormVal, 1)
                    HLDRRShift(R, 5, 1) = HLDIABETESRR(R, 5, 1) / FormVal
                    '1.6) 80+:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 80+ Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 1), FormVal)
                    'FormVal = Exp((HLBMI(R, 1) - 21) * 0.239)
                    FormVal = Expected_DRR(HLBMI(R, 1), 0.239)
                    FormVal = AMAX(FormVal, 1)
                    HLDRRShift(R, 6, 1) = HLDIABETESRR(R, 6, 1) / FormVal
                    '2. Females:
                    '=========
                    '2.1) 15 to 29:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 15 to 29 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 2), FormVal)
                    'FormVal = Exp((HLBMI(R, 2) - 21) * 0.247)
                    FormVal = Expected_DRR(HLBMI(R, 2), 0.247)
                    FormVal = AMAX(FormVal, 1)
                    HLDRRShift(R, 1, 2) = HLDIABETESRR(R, 1, 2) / FormVal
                    '2.2) 30 to 44:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 30 to 44 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 2), FormVal)
                    'FormVal = Exp((HLBMI(R, 2) - 21) * 0.385)
                    FormVal = Expected_DRR(HLBMI(R, 2), 0.385)
                    FormVal = AMAX(FormVal, 1)
                    HLDRRShift(R, 2, 2) = HLDIABETESRR(R, 2, 2) / FormVal
                    '2.3) 45 to 59:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 45 to 59 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 2), FormVal)
                    'FormVal = Exp((HLBMI(R, 2) - 21) * 0.293)
                    FormVal = Expected_DRR(HLBMI(R, 2), 0.293)
                    FormVal = AMAX(FormVal, 1)
                    HLDRRShift(R, 3, 2) = HLDIABETESRR(R, 3, 2) / FormVal
                    '2.4) 60 to 69:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 60 to 69 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 2), FormVal)
                    'FormVal = Exp((HLBMI(R, 2) - 21) * 0.191)
                    FormVal = Expected_DRR(HLBMI(R, 2), 0.191)
                    FormVal = AMAX(FormVal, 1)
                    HLDRRShift(R, 4, 2) = HLDIABETESRR(R, 4, 2) / FormVal
                    '2.5) 70 to 79:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 70 to 79 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 2), FormVal)
                    'FormVal = Exp((HLBMI(R, 2) - 21) * 0.182)
                    FormVal = Expected_DRR(HLBMI(R, 2), 0.182)
                    FormVal = AMAX(FormVal, 1)
                    HLDRRShift(R, 5, 2) = HLDIABETESRR(R, 5, 2) / FormVal
                    '2.6) 80+:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 80+ Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 2), FormVal)
                    'FormVal = Exp((HLBMI(R, 2) - 21) * 0.182)
                    FormVal = Expected_DRR(HLBMI(R, 2), 0.182)
                    FormVal = AMAX(FormVal, 1)
                    HLDRRShift(R, 6, 2) = HLDIABETESRR(R, 6, 2) / FormVal
                Next R
            Else 'all years but first compute forecast
                For R = 1 To NR
                    '1. Males:
                    '=========
                    '1.1) 15 to 29:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 15 to 29 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 1), FormVal)
                    'FormVal = Exp((HLBMI(R, 1) - 21) * 0.233)
                    FormVal = Expected_DRR(HLBMI(R, 1), 0.233)
                    FormVal = AMAX(FormVal, 1)
                    HLDIABETESRR(R, 1, 1) = HLDRRShift(R, 1, 1) * FormVal
                    HLDIABETESRR(R, 1, 1) = AMAX(HLDIABETESRR(R, 1, 1), 1)
                    '1.2) 30 to 44:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 30 to 44 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 1), FormVal)
                    'FormVal = Exp((HLBMI(R, 1) - 21) * 0.307)
                    FormVal = Expected_DRR(HLBMI(R, 1), 0.307)
                    FormVal = AMAX(FormVal, 1)
                    HLDIABETESRR(R, 2, 1) = HLDRRShift(R, 2, 1) * FormVal
                    HLDIABETESRR(R, 2, 1) = AMAX(HLDIABETESRR(R, 2, 1), 1)
                    '1.3) 45 to 59:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 45 to 59 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 1), FormVal)
                    'FormVal = Exp((HLBMI(R, 1) - 21) * 0.215)
                    FormVal = Expected_DRR(HLBMI(R, 1), 0.215)
                    FormVal = AMAX(FormVal, 1)
                    HLDIABETESRR(R, 3, 1) = HLDRRShift(R, 3, 1) * FormVal
                    HLDIABETESRR(R, 3, 1) = AMAX(HLDIABETESRR(R, 3, 1), 1)
                    '1.4) 60 to 69:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 60 to 69 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 1), FormVal)
                    'FormVal = Exp((HLBMI(R, 1) - 21) * 0.166)
                    FormVal = Expected_DRR(HLBMI(R, 1), 0.166)
                    FormVal = AMAX(FormVal, 1)
                    HLDIABETESRR(R, 4, 1) = HLDRRShift(R, 4, 1) * FormVal
                    HLDIABETESRR(R, 4, 1) = AMAX(HLDIABETESRR(R, 4, 1), 1)
                    '1.5) 70 to 79:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 70 to 79 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 1), FormVal)
                    'FormVal = Exp((HLBMI(R, 1) - 21) * 0.239)
                    FormVal = Expected_DRR(HLBMI(R, 1), 0.239)
                    FormVal = AMAX(FormVal, 1)
                    HLDIABETESRR(R, 5, 1) = HLDRRShift(R, 5, 1) * FormVal
                    HLDIABETESRR(R, 5, 1) = AMAX(HLDIABETESRR(R, 5, 1), 1)
                    '1.6) 80+:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 80+ Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 1), FormVal)
                    'FormVal = Exp((HLBMI(R, 1) - 21) * 0.239)
                    FormVal = Expected_DRR(HLBMI(R, 1), 0.239)
                    FormVal = AMAX(FormVal, 1)
                    HLDIABETESRR(R, 6, 1) = HLDRRShift(R, 6, 1) * FormVal
                    HLDIABETESRR(R, 6, 1) = AMAX(HLDIABETESRR(R, 6, 1), 1)
                    '2. Females:
                    '=========
                    '2.1) 15 to 29:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 15 to 29 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 2), FormVal)
                    'FormVal = Exp((HLBMI(R, 2) - 21) * 0.247)
                    FormVal = Expected_DRR(HLBMI(R, 2), 0.247)
                    FormVal = AMAX(FormVal, 1)
                    HLDIABETESRR(R, 1, 2) = HLDRRShift(R, 1, 2) * FormVal
                    HLDIABETESRR(R, 1, 2) = AMAX(HLDIABETESRR(R, 1, 2), 1)
                    '2.2) 30 to 44:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 30 to 44 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 2), FormVal)
                    'FormVal = Exp((HLBMI(R, 2) - 21) * 0.385)
                    FormVal = Expected_DRR(HLBMI(R, 2), 0.385)
                    FormVal = AMAX(FormVal, 1)
                    HLDIABETESRR(R, 2, 2) = HLDRRShift(R, 2, 2) * FormVal
                    HLDIABETESRR(R, 2, 2) = AMAX(HLDIABETESRR(R, 2, 2), 1)
                    '2.3) 45 to 59:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 45 to 59 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 2), FormVal)
                    'FormVal = Exp((HLBMI(R, 2) - 21) * 0.293)
                    FormVal = Expected_DRR(HLBMI(R, 2), 0.293)
                    FormVal = AMAX(FormVal, 1)
                    HLDIABETESRR(R, 3, 2) = HLDRRShift(R, 3, 2) * FormVal
                    HLDIABETESRR(R, 3, 2) = AMAX(HLDIABETESRR(R, 3, 2), 1)
                    '2.4) 60 to 69:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 60 to 69 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 2), FormVal)
                    'FormVal = Exp((HLBMI(R, 2) - 21) * 0.191)
                    FormVal = Expected_DRR(HLBMI(R, 2), 0.191)
                    FormVal = AMAX(FormVal, 1)
                    HLDIABETESRR(R, 4, 2) = HLDRRShift(R, 4, 2) * FormVal
                    HLDIABETESRR(R, 4, 2) = AMAX(HLDIABETESRR(R, 4, 2), 1)
                    '2.5) 70 to 79:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 70 to 79 Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 2), FormVal)
                    'FormVal = Exp((HLBMI(R, 2) - 21) * 0.182)
                    FormVal = Expected_DRR(HLBMI(R, 2), 0.182)
                    FormVal = AMAX(FormVal, 1)
                    HLDIABETESRR(R, 5, 2) = HLDRRShift(R, 5, 2) * FormVal
                    HLDIABETESRR(R, 5, 2) = AMAX(HLDIABETESRR(R, 5, 2), 1)
                    '2.6) 80+:
                    '--------------
                    'TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 80+ Cubic"
                    'Call XYTABL(TbName$, HLBMI(R, 2), FormVal)
                    'FormVal = Exp((HLBMI(R, 2) - 21) * 0.182)
                    FormVal = Expected_DRR(HLBMI(R, 2), 0.182)
                    FormVal = AMAX(FormVal, 1)
                    HLDIABETESRR(R, 6, 2) = HLDRRShift(R, 6, 2) * FormVal
                    HLDIABETESRR(R, 6, 2) = AMAX(HLDIABETESRR(R, 6, 2), 1)
                Next R
            End If
        End Sub

        'jrs 2010/01/08
        'Function to compute the Expected value for Diabetes RR
        Private Function Expected_DRR(avgBMI As Single, LogRR As Single) As Single
            Dim DX As Single
            Dim StdDev As Single
            StdDev = CSng(0.1 * avgBMI)
            DX = 6 * StdDev / 50
            Dim Pi As Double
            Pi = 3.14159265358979

			'gvdb 2024/04/05
            ' unused variable
            Dim DRR As Single, i As Integer, BMI As Single, P As Single ' z As Single,
            DRR = 0
            For i = 1 To 50
                BMI = avgBMI + DX * (i - 25)
                'z = (BMI - avgBMI) / StdDev
                'Population portion
                '        P = CalcNormalCDF(z)
                '        z = (BMI - DX - avgBMI) / StdDev
                '        P = (P - CalcNormalCDF(z)) / DX
                'jrs 2010/02/15 using PDF (Probability density function)
                'http://en.wikipedia.org/wiki/Normal_distribution
                P = CSng(1 / (2 * Pi * (StdDev ^ 2)) ^ 0.5 * Exp(-((BMI - avgBMI) ^ 2) / (2 * (StdDev ^ 2))))

                'BMI of 21 is the International reference average
                If BMI > 21 Then
                    'Accumulate Regional Value
                    DRR = CSng(DRR + Exp((BMI - ((BMI - avgBMI) / StdDev + 21)) * LogRR) * P * DX)
                Else
                    'Accumulate Regional Value
                    DRR = DRR + P * DX
                End If
            Next i

            Expected_DRR = DRR
        End Function

        'jrs 2009/12/04
        Private Sub ComputeSmokObBMIWeightAvgTotals(popgdr(,) As Double, agedst(,,) As Double)
            For R% = 1 To NR%
                popgdr(R%, 1) = 0
                popgdr(R%, 2) = 0
                For j = 1 To NCohorts
                    popgdr(R%, 1) = popgdr(R%, 1) + agedst(j, R%, 1)
                    popgdr(R%, 2) = popgdr(R%, 2) + agedst(j, R%, 2)
                Next j
                HLSMOKING(R%, 3) = CSng((HLSMOKING(R%, 1) * popgdr(R%, 1) + HLSMOKING(R%, 2) *
        popgdr(R%, 2)) / (popgdr(R%, 1) + popgdr(R%, 2)))
                HLOBESITY(R%, 3) = CSng((HLOBESITY(R%, 1) * popgdr(R%, 1) + HLOBESITY(R%, 2) *
        popgdr(R%, 2)) / (popgdr(R%, 1) + popgdr(R%, 2)))
                'jrs 2009/09/01
                HLBMI(R%, 3) = CSng((HLBMI(R%, 1) * popgdr(R%, 1) + HLBMI(R%, 2) *
        popgdr(R%, 2)) / (popgdr(R%, 1) + popgdr(R%, 2)))
            Next R%
        End Sub

        'jrs 2008/11/20
        Private Sub EstimateSmokingImpact(ByVal iYr As Integer, context As IForecastContext)
            'iYr is the Year to estimate Smoking Impact, should be YYYY format, ex. 2006

            'Load Smoking Rate with Lag of 25 years
            Dim iLagYears As Integer
            iLagYears = 25

            Using dbS As IFsSmokingContext = New IFsSmokingContext
                Dim rsSYr1 = (From s In dbS.SmokingRateEstimatesReg
                              Where s.Year = iYr - iLagYears - 1)

                Dim rsSYr2 = (From s In dbS.SmokingRateEstimatesReg
                              Where s.Year = iYr - iLagYears)

                'Growth Rate
                Dim Rt As Single
                Dim j As Integer
                'jrs 2011/05/27 *** Smoking begin ***
                Dim RegVal As Single
                Dim sTableName As String
                'jrs 2011/05/27 *** Smoking end ***
                'jrs 2011/10/27
                Dim SRegVal As Single
                'jrs 2011/08/18
                Dim SmkMult As Single, Delta As Single, CurrDelta As Single
                If iYr = BaseYear + 1 Then
                    ReDim SmokingCumEff(NR, 2)
                End If

                For Each item In (From s1 In rsSYr1
                                  Join s2 In rsSYr2 On s1.RegionId Equals s2.RegionId And s1.Sex Equals s2.Sex
                                  Select SmokingRate1 = s1.SmokingRate, SmokingRate2 = s2.SmokingRate, RegionId = s1.RegionId, Sex = s1.Sex)
                    If item.SmokingRate1 > 0 Then
                        Rt = CSng((item.SmokingRate2 - item.SmokingRate1) / item.SmokingRate1)
                        'jrs 2011/06/17
                        If Rt > 0.25 And item.SmokingRate1 < 1 Then
                            Rt = 0.25
                        End If
                    ElseIf item.SmokingRate2 = 0 Then 'jrs 2016/11/01
                        Rt = 0
                    Else
                        Rt = 0.5 'jrs 2016/11/01 Rt = 10000
                    End If            'Use same Rate for all cohorts
                    For j = 1 To 4
                        'jrs 2011/05/27 *** Smoking begin ***
                        If hlsmokingstsw = 1 Then
                            If item.Sex = 1 Then
                                Select Case j
                                    Case 1
                                        'jrs 2011/10/26
                                        sTableName = "Male Smoking Prevalence (2005) Versus Male 30 to 44 Smoking Impact (2030) Linear"
                                'sTableName = "Male Smoking Prevalence (2005) Versus Male 30 to 44 Smoking Impact (2030) Exp"
                                    Case 2
                                        'jrs 2011/10/26
                                        sTableName = "Male Smoking Prevalence (2005) Versus Male 45 to 59 Smoking Impact (2030) Linear"
                                'sTableName = "Male Smoking Prevalence (2005) Versus Male 45 to 59 Smoking Impact (2030) Exp"
                                    Case 3
                                        'jrs 2011/10/26
                                        sTableName = "Male Smoking Prevalence (2005) Versus Male 60 to 69 Smoking Impact (2030) Linear"
                                'sTableName = "Male Smoking Prevalence (2005) Versus Male 60 to 69 Smoking Impact (2030) Exp"
                                    Case 4
                                        'jrs 2011/10/26
                                        sTableName = "Male Smoking Prevalence (2005) Versus Male 70+ Smoking Impact (2030) Linear"
                                        'sTableName = "Male Smoking Prevalence (2005) Versus Male 70+ Smoking Impact (2030) Exp"
                                End Select
                                Call XYTABL(sTableName, CSng(item.SmokingRate2), RegVal, context)
                            Else
                                Select Case j
                                    Case 1
                                        sTableName = "Female Smoking Prev (2005) Versus Female 30 to 44 Smok Impact (2030) Linear"
                                    Case 2
                                        sTableName = "Female Smoking Prev (2005) Versus Female 45 to 59 Smok Impact (2030) Linear"
                                    Case 3
                                        sTableName = "Female Smoking Prev (2005) Versus Female 60 to 69 Smok Impact (2030) Linear"
                                    Case 4
                                        sTableName = "Female Smoking Prev (2005) Versus Female 70+ Smok Impact (2030) Linear"
                                End Select
                                Call XYTABL(sTableName, CSng(item.SmokingRate2), RegVal, context)
                            End If
                            'jrs 2011/10/27
                            'jrs 2011/12/12 If iYr = BaseYear + 1 And hlsmokingstsw = 1 Then
                            Call XYTABL(sTableName, CSng(item.SmokingRate1), SRegVal, context)
                            'jrs 2011/12/12 End If
                        End If
                        'jrs 2011/05/27 *** Smoking end ***

                        'jrs 2009/01/13 Smoking Impact switch
                        If iYr > 2030 And hlsmimpsw = 0 Then
                            'Switch is off, keeps SI constant
                            HLSMOKINGIMP(item.RegionId, j, item.Sex) = SHLSMOKINGIMP(item.RegionId, j, item.Sex)
                        Else
                            'jrs 2011/05/27 *** Smoking begin ***
                            If hlsmokingstsw = 0 Then
                                HLSMOKINGIMP(item.RegionId, j, item.Sex) = SHLSMOKINGIMP(item.RegionId, j, item.Sex) * (1 + Rt)
                            Else
                                'jrs 2011/12/12
                                '                        If iYr = BaseYear + 1 Then
                                '                            'jrs 2011/12/08 SmokingImpactFactors(j, item.RegionId, item.Sex) = SHLSMOKINGIMP(item.RegionId, j, item.Sex) - SRegVal
                                '                            If SRegVal <> 0 Then
                                '                                SmokingImpactFactors(j, item.RegionId, item.Sex) = SHLSMOKINGIMP(item.RegionId, j, item.Sex) / SRegVal
                                '                            Else
                                '                                SmokingImpactFactors(j, item.RegionId, item.Sex) = 1
                                '                            End If
                                '                        End If
                                '                        'jrs 2011/12/08 HLSMOKINGIMP(item.RegionId, j, item.Sex) = SmokingImpactFactors(j, item.RegionId, item.Sex) + RegVal
                                '                        HLSMOKINGIMP(item.RegionId, j, item.Sex) = SmokingImpactFactors(j, item.RegionId, item.Sex) * RegVal
                                If SRegVal <> 0 Then
                                    Rt = (RegVal - SRegVal) / Abs(SRegVal)
                                    If Abs(SRegVal) < 1 Then
                                        If Rt > 1 Then
                                            Rt = 1
                                        ElseIf Rt < -0.5 Then
                                            Rt = -0.5
                                        End If
                                    End If
                                ElseIf RegVal > 0 Then
                                    Rt = 1
                                Else
                                    Rt = -0.5
                                End If
                                HLSMOKINGIMP(item.RegionId, j, item.Sex) = SHLSMOKINGIMP(item.RegionId, j, item.Sex) * (1 + Rt)
                            End If
                            HLSMOKINGIMP(item.RegionId, j, item.Sex) = CSng(AMAX(HLSMOKINGIMP(item.RegionId, j, item.Sex), 0.0001))
                            HLSMOKINGIMP(item.RegionId, j, item.Sex) = Amin(HLSMOKINGIMP(item.RegionId, j, item.Sex), 100000)
                            'jrs 2011/05/27 *** Smoking end ***
                        End If
                        'Save last estimation
                        SHLSMOKINGIMP(item.RegionId, j, item.Sex) = HLSMOKINGIMP(item.RegionId, j, item.Sex)
                        'jrs 2011/08/18
                        'Adding elasticity to partial Delta for Immediate effect
                        SmkMult = hlsmokingm(item.RegionId, item.Sex) * hlsmokingm(item.RegionId, 3)
                        'jrs 2011/10/25
                        'Considering TCS as a multiplier too
                        'jrs 2012/01/13 SmkMult = SmkMult * (1 + (SHLSMOKINGTCS(item.RegionId) - hlsmokingtcs(item.RegionId)) / 100 * hlsmokingtcsel)
                        'TCS has changed and it's not a simple multiplier anymore
                        If SmkMult <> 0 And SmkMult <> 1 Then
                            CurrDelta = HLSMOKING(item.RegionId, item.Sex) * (SmkMult - 1) / SmkMult
                            Delta = (CurrDelta + SmokingCumEff(item.RegionId, item.Sex)) * hlsmkimeff
                            If j = 4 Then
                                SmokingCumEff(item.RegionId, item.Sex) = (SmokingCumEff(item.RegionId, item.Sex) + CurrDelta) * (1 - hlsmkimeff)
                            End If
                            HLSMOKINGIMP(item.RegionId, j, item.Sex) = HLSMOKINGIMP(item.RegionId, j, item.Sex) * (1 + Delta * hlsmkel)
                            HLSMOKINGIMP(item.RegionId, j, item.Sex) = CSng(AMAX(HLSMOKINGIMP(item.RegionId, j, item.Sex), 0.0001))
                            HLSMOKINGIMP(item.RegionId, j, item.Sex) = Amin(HLSMOKINGIMP(item.RegionId, j, item.Sex), 100000)
                        End If
                    Next j

                Next

            End Using

        End Sub

        'jrs 2009/12/04
        Private Sub ComputeSmokAvgAllAgesSex()
            'jrs 2008/11/21
            'average should be on Adult population and not all population
            Dim PopAd As Single
            Dim CohPop As Single
            Dim CohPopT As Single
            Dim z As Integer

            'jrs 2007/06/27
            'All Years: Smoking Impact - Compute Averages for All Ages and Accross Gender
            '----------------------------------------------------------------------------
            For R% = 1 To NR%
                'Filling Up All Ages dimension
                For G = 1 To NGender - 1
                    HLSMOKINGIMP(R%, 5, G) = 0
                    'jrs 2008/11/21
                    PopAd = 0

                    For j = 1 To 4
                        Select Case j
                            Case 1 '30 to 44
                                CohPop = CSng(agedst(7, R%, G) + agedst(8, R%, G) + agedst(9, R%, G))
                            Case 2 '45 to 59
                                CohPop = CSng(agedst(10, R%, G) + agedst(11, R%, G) + agedst(12, R%, G))
                            Case 3 '60 to 69
                                CohPop = CSng(agedst(13, R%, G) + agedst(14, R%, G))
                            Case 4 '70+
                                CohPop = 0
                                For z = 15 To NCohorts
                                    CohPop = CSng(CohPop + agedst(z, R%, G))
                                Next z
                        End Select
                        HLSMOKINGIMP(R%, 5, G) = HLSMOKINGIMP(R%, 5, G) + HLSMOKINGIMP(R%, j, G) * CohPop
                        'jrs 2008/11/21
                        PopAd = PopAd + CohPop
                    Next j
                    'jrs 2013/01/18
                    If PopAd > 0 Then
                        HLSMOKINGIMP(R%, 5, G) = HLSMOKINGIMP(R%, 5, G) / PopAd 'jrs 2008/11/21 popgdr(R%, G)
                    Else
                        HLSMOKINGIMP(R%, 5, G) = 0
                    End If
                Next G
                'Filling Up Total (Gender) dimension
                For j = 1 To 5
                    CohPopT = 0
                    HLSMOKINGIMP(R%, j, 3) = 0
                    For G = 1 To NGender - 1
                        Select Case j
                            Case 1 '30 to 44
                                CohPop = CSng(agedst(7, R%, G) + agedst(8, R%, G) + agedst(9, R%, G))
                            Case 2 '45 to 59
                                CohPop = CSng(agedst(10, R%, G) + agedst(11, R%, G) + agedst(12, R%, G))
                            Case 3 '60 to 69
                                CohPop = CSng(agedst(13, R%, G) + agedst(14, R%, G))
                            Case 4 '70+
                                CohPop = 0
                                For z = 15 To NCohorts
                                    CohPop = CSng(CohPop + agedst(z, R%, G))
                                Next z
                            Case 5 'All years
                                'jrs 2009/02/06 CohPop = popgdr(R%, G)
                                CohPop = 0
                                For z = 7 To NCohorts
                                    CohPop = CSng(CohPop + agedst(z, R%, G))
                                Next z
                        End Select
                        HLSMOKINGIMP(R%, j, 3) = HLSMOKINGIMP(R%, j, 3) + HLSMOKINGIMP(R%, j, G) * CohPop
                        CohPopT = CohPopT + CohPop
                    Next G
                    'jrs 2013/01/18
                    If CohPopT > 0 Then
                        HLSMOKINGIMP(R%, j, 3) = HLSMOKINGIMP(R%, j, 3) / CohPopT
                    Else
                        HLSMOKINGIMP(R%, j, 3) = 0
                    End If
                Next j
            Next R%

        End Sub

        'jrs 2009/12/09
        Private Sub ComputeAvgDiabRRAgeSex(popgdr(,) As Double)
            Dim R As Integer, G As Integer, j, z As Integer
            Dim CohPop As Single, CohPopT As Single

            For R% = 1 To NR%
                'Filling Up All Ages dimension
                For G = 1 To NGender - 1
                    HLDIABETESRR(R%, 7, G) = 0
                    For j = 1 To 6
                        Select Case j
                            Case 1 '15 to 29
                                CohPop = CSng(agedst(4, R%, G) + agedst(5, R%, G) + agedst(6, R%, G))
                            Case 2 '30 to 44
                                CohPop = CSng(agedst(7, R%, G) + agedst(8, R%, G) + agedst(9, R%, G))
                            Case 3 '45 to 59
                                CohPop = CSng(agedst(10, R%, G) + agedst(11, R%, G) + agedst(12, R%, G))
                            Case 4 '60 to 69
                                CohPop = CSng(agedst(13, R%, G) + agedst(14, R%, G))
                            Case 5 '70 to 79
                                CohPop = CSng(agedst(15, R%, G) + agedst(16, R%, G))
                            Case 6 '80+
                                CohPop = 0
                                For z = 17 To NCohorts
                                    CohPop = CSng(CohPop + agedst(z, R%, G))
                                Next z
                        End Select
                        HLDIABETESRR(R%, 7, G) = HLDIABETESRR(R%, 7, G) + HLDIABETESRR(R%, j, G) * CohPop
                    Next j
                    'jrs 2013/01/18
                    If popgdr(R%, G) > 0 Then
                        HLDIABETESRR(R%, 7, G) = CSng(HLDIABETESRR(R%, 7, G) / popgdr(R%, G))
                    Else
                        HLDIABETESRR(R%, 7, G) = 0
                    End If
                Next G
                'Filling Up Total (Gender) dimension
                For j = 1 To 7
                    CohPopT = 0
                    HLDIABETESRR(R%, j, 3) = 0
                    For G = 1 To NGender - 1
                        Select Case j
                            Case 1 '15 to 29
                                CohPop = CSng(agedst(4, R%, G) + agedst(5, R%, G) + agedst(6, R%, G))
                            Case 2 '30 to 44
                                CohPop = CSng(agedst(7, R%, G) + agedst(8, R%, G) + agedst(9, R%, G))
                            Case 3 '45 to 59
                                CohPop = CSng(agedst(10, R%, G) + agedst(11, R%, G) + agedst(12, R%, G))
                            Case 4 '60 to 69
                                CohPop = CSng(agedst(13, R%, G) + agedst(14, R%, G))
                            Case 5 '70 to 79
                                CohPop = CSng(agedst(15, R%, G) + agedst(16, R%, G))
                            Case 6 '80+
                                CohPop = 0
                                For z = 17 To NCohorts
                                    CohPop = CSng(CohPop + agedst(z, R%, G))
                                Next z
                            Case 7 'All years
                                CohPop = CSng(popgdr(R%, G))
                        End Select
                        HLDIABETESRR(R%, j, 3) = HLDIABETESRR(R%, j, 3) + HLDIABETESRR(R%, j, G) * CohPop
                        CohPopT = CohPopT + CohPop
                    Next G
                    'jrs 2013/01/18
                    If CohPopT > 0 Then
                        HLDIABETESRR(R%, j, 3) = HLDIABETESRR(R%, j, 3) / CohPopT
                    Else
                        HLDIABETESRR(R%, j, 3) = 0
                    End If
                Next j
            Next R%
        End Sub

        'jrs 2009/05/12
        Private Sub UpdateRespDisease(ByRef modmordstdet(,,) As Double, ByRef sird(,,) As Single, ByRef modmordstdet2(,,) As Double)
            Dim sirRR As Integer '1 for China, 2 for World, 3 for SEAR D
            Dim SICoh As Integer
            Dim rtCOPD As Double, rtOthResp As Double, rrCOPD As Single, rrOthResp As Single
            Dim SIR As Single


            'All Years: Changing respiratory disease
            '---------------------------------------
            For R% = 1 To NR%
                'If R = 179 Then Stop
                sirRR = 2
                If GRPS(1, R) = "China" Then
                    sirRR = 1
                ElseIf GRPS(1, R) = "Bangladesh" Or GRPS(1, R) = "Bhutan" Or GRPS(1, R) = "India" _
        Or GRPS(1, R) = "Korea North" Or GRPS(1, R) = "Maldives" Or GRPS(1, R) = "Myanmar" _
        Or GRPS(1, R) = "Nepal" Or GRPS(1, R) = "Afghanistan" Or GRPS(1, R) = "Pakistan" Then
                    'SEAR D group plus Afghanistan and Pakistan, and no Timo L'este
                    sirRR = 3
                End If
                For j = 7 To NCohorts
                    'SI Cohort
                    Select Case j
                '30 to 44
                        Case 7, 8, 9
                            SICoh = 1
                '45 to 59
                        Case 10, 11, 12
                            SICoh = 2
                '60 to 69
                        Case 13, 14
                            SICoh = 3
                            '70 +
                        Case Else
                            SICoh = 4
                    End Select
                    For k = 9 To 10 ' Resp. Dis. for Males and Females
                        'Resp. Disease constants
                        '-----------------------
                        If k = 9 Then 'Male
                            rrCOPD = 9.974
                            'age 60-69
                            If j = 13 Or j = 14 Then
                                rrOthResp = 1.84
                                '70+
                            ElseIf j > 14 Then
                                rrOthResp = 1.55
                            Else
                                rrOthResp = 2.428
                            End If
                        Else
                            rrCOPD = 10.247
                            'age 60-69
                            If j = 13 Or j = 14 Then
                                rrOthResp = 2.12
                                '70+
                            ElseIf j > 14 Then
                                rrOthResp = 1.55 'this one could be only for males.
                            Else
                                rrOthResp = 2.176
                            End If
                        End If

                        If k = 10 Then
                            SIR = HLSMOKINGIMP(R, SICoh, 2) / sird(sirRR, SICoh, 2)
                        Else
                            SIR = HLSMOKINGIMP(R, SICoh, 1) / sird(sirRR, SICoh, 1)
                        End If

                        'k + 2 is Other Non-communicable Diseases
                        'jrs 2009/12/04
                        If modmordstdet(j, R, k + 2) > 709 Then
                            'non-smoker chronic resp assumed to be changing
                            rtCOPD = (SIR * rrCOPD + 1 - SIR) * (Exp(709) ^ 0.75)
                            'at half the rate of other group II death rates
                            rtOthResp = (SIR * rrOthResp + 1 - SIR) * (Exp(709) ^ 0.75)
                        Else
                            'non-smoker chronic resp assumed to be changing
                            rtCOPD = (SIR * rrCOPD + 1 - SIR) * (Exp(modmordstdet(j, R, k + 2)) ^ 0.75)
                            'at half the rate of other group II death rates
                            rtOthResp = (SIR * rrOthResp + 1 - SIR) * (Exp(modmordstdet(j, R, k + 2)) ^ 0.75)
                        End If

                        'Mort for COPD + Othr. Resp. Dis.
                        If rtCOPD > 0 Then
                            modmordstdet(j, R, k) = CSng(Log(rtCOPD))
                        Else
                            modmordstdet(j, R, k) = 0
                        End If
                        If rtOthResp > 0 Then
                            modmordstdet(j, R, k) = CSng(modmordstdet(j, R, k) + Log(rtOthResp))
                            modmordstdet(j, R, k) = Amin(modmordstdet(j, R, k), 1000)
                        End If

                        'jrs 2008/10/09
                        'Transition factors (low to high income)
                        If GDPPCIP(R) < 3 Then
                            'k + 2 is Other Non-communicable Diseases
                            'jrs 2009/12/04
                            If modmordstdet2(j, R, k + 2) > 709 Then
                                'non-smoker chronic resp assumed to be changing
                                rtCOPD = (SIR * rrCOPD + 1 - SIR) * (Exp(709) ^ 0.75)
                                'at half the rate of other group II death rates
                                rtOthResp = (SIR * rrOthResp + 1 - SIR) * (Exp(709) ^ 0.75)
                            Else
                                'non-smoker chronic resp assumed to be changing
                                rtCOPD = (SIR * rrCOPD + 1 - SIR) * (Exp(modmordstdet2(j, R, k + 2)) ^ 0.75)
                                'at half the rate of other group II death rates
                                rtOthResp = (SIR * rrOthResp + 1 - SIR) * (Exp(modmordstdet2(j, R, k + 2)) ^ 0.75)
                            End If

                            'Mort for COPD + Othr. Resp. Dis.
                            If rtCOPD > 0 Then
                                modmordstdet2(j, R, k) = CSng(Log(rtCOPD))
                            Else
                                modmordstdet2(j, R, k) = 0
                            End If
                            If rtOthResp > 0 Then
                                modmordstdet2(j, R, k) = CSng(modmordstdet2(j, R, k) + Log(rtOthResp))
                                modmordstdet2(j, R, k) = Amin(modmordstdet2(j, R, k), 1000)
                            End If
                        End If
                    Next k
                Next j
            Next R%

        End Sub

        'jrs 2010/01/25
        Private Sub ComputeFactorsNormScalShift(mordstDet(,,) As Double, popgdr(,) As Double, Ntypes As Integer, context As IForecastContext)

            'jrs 2007/07/06
            Dim NormFacM, NormFacF As Double
            'jrs 2014/11/07
            Dim AIDSMort As Single, bMort As Boolean
            Dim z As Integer
            Dim FormValue, FormValue2 As Single

            For R% = 1 To NR%
                For j = 0 To NCohorts
                    'All Years: First Year Only, Normalization factor
                    ' from Initialization Data to Mortality in the Population Module
                    '---------------------------------------------------------------
                    'jrs 2007/05/04 added normalization factor
                    'mod 2 will give only 2 values 0 or 1
                    '(k + 1) mod 2 will give 0 for odds, 1 for evens
                    ' odds are males (1), evens are females (2)
                    NormFacM = 0
                    NormFacF = 0
                    'Excluding AIDS in the normalization
                    'Aids are types 21 and 22
                    'Males
                    '------
                    For z = 1 To 19 Step 2 'jrs 2009/09/15 don't use new comm. dis types (included in old other comm.)
                        'if there's no historic data, use Mathers' formula
                        If mordstDet(j, R, z) = -1 Then
                            NormFacM = NormFacM + modmordstdet(j, R, z)
                        Else 'Using historic data
                            NormFacM = NormFacM + mordstDet(j, R, z)
                        End If
                    Next z
                    If NormFacM <> 0 Then
                        'Remove Aids from the value in Population Mortality for the normalization
                        NormFacM = (modmordst(j, R, 1) - modmordstdet(j, R, 21)) / NormFacM
                        'jrs 2009/10/19
                        If NormFacM < 0 Then
                            'jrs 2014/11/07
                            '                        Select Case j
                            '                            Case 0
                            '                                sAge = "Infants"
                            '                            Case 1
                            '                                sAge = "ages 1 to 4"
                            '                            Case Else
                            '                                sAge = "ages " & (j - 1) * 5 & " to " & j * 5 - 1
                            '                        End Select
                            '                        MsgBox "Aids Mortality seems to be greater than total mortality for Males in Region " & _
                            '                        GRPS(1, R) & " for " & sAge, vbCritical, "Health Module"
                            '                        NormFacM = 1
                            AIDSMort = CSng(modmordstdet(j, R, 21))
                            NormFacM = NormFacM / (modmordst(j, R, 1) - modmordstdet(j, R, 21))
                            modmordstdet(j, R, 21) = CSng(modmordst(j, R, 1) * 0.95)
                            NormFacM = (modmordst(j, R, 1) - modmordstdet(j, R, 21)) / NormFacM
                            'keep constant the number of deaths
                            If agedst(j, R, 2) > 0 Then
                                modmordstdet(j, R, 22) = modmordstdet(j, R, 22) + (AIDSMort - modmordstdet(j, R, 21)) * agedst(j, R, 1) / agedst(j, R, 2)
                            Else
                                modmordstdet(j, R, 22) = modmordstdet(j, R, 22) + (AIDSMort - modmordstdet(j, R, 21))
                            End If
                        End If
                    Else
                        NormFacM = 0
                    End If
                    'Females
                    '------
                    For z = 2 To 20 Step 2 'jrs 2009/09/15 don't use new comm. dis types (included in old other comm.)
                        'jrs 2009/08/11
                        'if there's no historic data, use Mathers' formula
                        If mordstDet(j, R, z) = -1 Then
                            NormFacF = NormFacF + modmordstdet(j, R, z)
                        Else 'Using historic data
                            NormFacF = NormFacF + mordstDet(j, R, z)
                        End If
                    Next z
                    If NormFacF <> 0 Then
                        'Remove Aids from the value in Population Mortality for the normalization
                        NormFacF = (modmordst(j, R, 2) - modmordstdet(j, R, 22)) / NormFacF
                        'jrs 2009/10/19
                        If NormFacF < 0 Then
                            'jrs 2014/11/07
                            '                        Select Case j
                            '                            Case 0
                            '                                sAge = "Infants"
                            '                            Case 1
                            '                                sAge = "ages 1 to 4"
                            '                            Case Else
                            '                                sAge = "ages " & (j - 1) * 5 & " to " & j * 5 - 1
                            '                        End Select
                            '                        MsgBox "Aids Mortality seems to be greater than total mortality for Females in Region " & _
                            '                        GRPS(1, R) & " for " & sAge, vbCritical, "Health Module"
                            '                        NormFacF = 1
                            AIDSMort = CSng(modmordstdet(j, R, 22))
                            NormFacF = NormFacF / (modmordst(j, R, 2) - modmordstdet(j, R, 22))
                            modmordstdet(j, R, 22) = CSng(modmordst(j, R, 2) * 0.95)
                            NormFacF = (modmordst(j, R, 2) - modmordstdet(j, R, 22)) / NormFacF
                            'keep constant the number of deaths
                            bMort = ((modmordst(j, R, 1) - modmordstdet(j, R, 21)) <> 0)
                            If bMort Then
                                NormFacM = NormFacM / (modmordst(j, R, 1) - modmordstdet(j, R, 21))
                            End If
                            If agedst(j, R, 1) > 0 Then
                                modmordstdet(j, R, 21) = modmordstdet(j, R, 21) + (AIDSMort - modmordstdet(j, R, 22)) * agedst(j, R, 2) / agedst(j, R, 1)
                            Else
                                modmordstdet(j, R, 21) = modmordstdet(j, R, 21) + (AIDSMort - modmordstdet(j, R, 22))
                            End If
                            If bMort Then
                                NormFacM = (modmordst(j, R, 1) - modmordstdet(j, R, 21)) / NormFacM
                                If NormFacM < 0 Then
                                    NormFacM = 1
                                End If
                            End If
                        End If
                    Else
                        NormFacF = 0
                    End If
                    'Save
                    'jrs 2019/11/19
                    'ifsPopWorkDB.Add(New MorDetNormFactors() With {
                    '        .CohortId = CShort(j),
                    '        .RegionId = CShort(R),
                    '        .Males = NormFacM,
                    '        .Females = NormFacF
                    '            })

                    For k = 1 To Ntypes 'cuases of mortality (odd for male, even for female) 'jrs 2007/06/26 9 causes 2 genders
                        'All Years: First Year Only, Add Scaling factor
                        'from Mathers' equation to Initialization Data
                        '----------------------------------------------------
                        'jrs 2007/06/26  change to additive factor Factors(j, R%, k) = NormFac * mordstDet(j, R, k) / modmordstdet(j, R, k)
                        If k Mod 2 = 0 Then
                            NormFactors(j, R%, k) = NormFacF
                        Else
                            NormFactors(j, R%, k) = NormFacM
                        End If
                        If mordstDet(j, R, k) = -1 Then
                            Factors(j, R%, k) = 1
                        ElseIf modmordstdet(j, R, k) <> 0 Then
                            Factors(j, R%, k) = mordstDet(j, R, k) / modmordstdet(j, R, k)
                        Else
                            If mordstDet(j, R, k) = 0 Then
                                Factors(j, R%, k) = 1
                            Else
                                Factors(j, R%, k) = CSng(mordstDet(j, R, k) / 0.0000000001)
                                modmordstdet(j, R, k) = 0.0000000001
                            End If
                        End If

                        'All Years: First Year Only, Save Factors
                        '----------------------------------------
                        'jrs 2019/11/19
                        'If k = Ntypes Then
                        '    ifsPopWorkDB.Add(New MorDetFactors() With {
                        '        .CohortId = CShort(j),
                        '        .RegionId = CShort(R),
                        '        .MortPerMalesComDis = Factors(j, R, 1),
                        '        .MortPerFeMalesComDis = Factors(j, R, 2),
                        '        .MaligNeoPlasMales = Factors(j, R, 3),
                        '        .MaligNeoPlasFemales = Factors(j, R, 4),
                        '        .CardioVascMales = Factors(j, R, 5),
                        '        .CardioVascFemales = Factors(j, R, 6),
                        '        .DigestiveMales = Factors(j, R, 7),
                        '        .DigestiveFemales = Factors(j, R, 8),
                        '        .RespiratoryMales = Factors(j, R, 9),
                        '        .RespiratoryFemales = Factors(j, R, 10),
                        '        .OtherG2Males = Factors(j, R, 11),
                        '        .OtherG2Females = Factors(j, R, 12),
                        '        .RoadTrafficAccMales = Factors(j, R, 13),
                        '        .RoadTrafficAccFemales = Factors(j, R, 14),
                        '        .OtherunintInjMales = Factors(j, R, 15),
                        '        .OtherunintInjFemales = Factors(j, R, 16),
                        '        .IntenInjMales = Factors(j, R, 17),
                        '        .IntenInjFemales = Factors(j, R, 18),
                        '        .DiabetesMales = Factors(j, R, 19),
                        '        .DiabetesFemales = Factors(j, R, 20),
                        '        .DiarrheaMales = Factors(j, R, 23),
                        '        .DiarrheaFemales = Factors(j, R, 24),
                        '        .MalariaMales = Factors(j, R, 25),
                        '        .MalariaFemales = Factors(j, R, 26),
                        '        .RespInfMales = Factors(j, R, 27),
                        '        .RespInfFemales = Factors(j, R, 28),
                        '        .MentalHealthMales = Factors(j, R, 29),
                        '        .MentalHealthFemales = Factors(j, R, 30)
                        '        })
                        'End If
                    Next k
                Next j
                'All Years: First Year only - Compute Shift Factor for MalNutrition
                '------------------------------------------------------------------
                'jrs 2008/01/09 Call XYTABL(TbName$, GDPPCP(R%), FormValue)
                'jrs 2009/12/16 Call AnalFunc("Percent of Children Malnourished (2005)", _
                'Array(GDPPCP(R%), EDYRSAG25(R%, 3)), FormValue)
                'jrs 2009/12/23 FormValue = Exp(3.54 - 0.524 * Log(GDPPCP(R%)) - 0.478 * Log(EDYRSAG25(R%, 3)))
                FormValue = CSng(-0.714297 - 0.58979 * Log(GDPPCP(R%)) - 0.544938 * Log(EDYRSAG25(R%, 3)))
                FormValue = CSng(100 * Exp(FormValue) / (1 + Exp(FormValue)))
                'jrs 2009/04/02 change to additive MNfactors(R%) = MALNCHP(R%) / FormValue
                MNfactors(R%) = MALNCHP(R%) - FormValue

                'jrs 2016/01/13 *********** begin *******************
                FormValue2 = CSng(-2.821 - 0.118 * Log(GDPPCP(R%)) - 0.45 * Log(EDYRSAG25(R%, 3)))
                FormValue2 = CSng(100 * Exp(FormValue2) / (1 + Exp(FormValue2)))
                SAMfactors(R%) = MALNCHPSAM(R%) - FormValue2
                'jrs 2016/01/13 *********** end *********************


                'jrs 2009/03/24
                'jrs 2019/11/19
                'ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                '        .VarName = "MALNCHP",
                '        .Year = CShort(IY% + FirstYear - 1),
                '        .Region = CShort(R%),
                '        .Dim2 = 0,
                '        .Dim3 = 0,
                '        .DistalFormVal = CSng(MALNCHP(R%))
                '            })

                'jrs 2008/08/15
                'All Years: First Year only - Compute Shift Factor for BMI
                '---------------------------------------------------------
                'BMIOb = 30, STD = 20% of Mean
                Call AnalFunc("BMI Male (2005)",
                    New Single() {GDPPCP(R%), EDYRSAG25(R%, 3)}, FormValue,
                    New String() {"GDPPCP", "Educ25"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                'jrs 2009/07/10 changed to using initial value with no multiplier
                'jrs 2009/09/01 z = NormalInverse(1 - HLOBESITYI(R%, 1) / 100)
                'z = (BMIOb - BMI)/ STD
                'z = (BMIOb - BMI)/ (BMISTDPrp * BMI)
                'z * BMISTDPrp * BMI = BMIOb - BMI
                'z * BMISTDPrp * BMI + BMI = BMIOb
                'jrs 2009/09/01 BMI = BMIOb / (z * BMISTDPrp + 1)
                BMIfactorsM(R%) = HLBMI(R%, 1) / FormValue

                Call AnalFunc("BMI Female (2005)",
                    New Single() {GDPPCP(R%), EDYRSAG25(R%, 3)}, FormValue,
                    New String() {"GDPPCP", "Educ25"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                'jrs 2009/07/10 changed to using initial value with no multiplier
                'jrs 2009/09/01 z = NormalInverse(1 - HLOBESITYI(R%, 2) / 100)
                'jrs 2009/09/01 BMI = BMIOb / (z * BMISTDPrp + 1)
                BMIfactorsF(R%) = HLBMI(R%, 2) / FormValue


                'jrs 2009/03/24
                'jrs 2011/11/19
                'ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                '        .VarName = "HLBMI",
                '        .Year = CShort(IY% + FirstYear - 1),
                '        .Region = CShort(R%),
                '        .Dim2 = 1,
                '        .Dim3 = 0,
                '        .DistalFormVal = CSng(HLBMI(R%, 1))
                '            })

                'ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                '        .VarName = "HLBMI",
                '        .Year = CShort(IY% + FirstYear - 1),
                '        .Region = CShort(R%),
                '        .Dim2 = 2,
                '        .Dim3 = 0,
                '        .DistalFormVal = CSng(HLBMI(R%, 2))
                '            })

                'ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                '        .VarName = "HLBMI",
                '        .Year = CShort(IY% + FirstYear - 1),
                '        .Region = CShort(R%),
                '        .Dim2 = 3,
                '        .Dim3 = 0,
                '        .DistalFormVal = CSng((HLBMI(R%, 1) * popgdr(R%, 1) + HLBMI(R%, 2) *
                '        popgdr(R%, 2)) / (popgdr(R%, 1) + popgdr(R%, 2)))
                '            })

                'jrs 2009/09/21 TblHLPRDR.Fields("DistalFormVal") = (HLOBESITY(R%, 1) * popgdr(R%, 1) + HLOBESITY(R%, 2) * _
                'popgdr(R%, 2)) / (popgdr(R%, 1) + popgdr(R%, 2))

                'jrs 2009/10/28
                'shift factor for solid fuels (distal driver)
                'jrs 2013/12/19
                'Call AnalFunc("Percent of People using Primarily Solid Fuels", _
                'Array(GDPPCP(R%), EDYRSAG25(R%, 3)), FormValue)
                Call AnalFunc("Percent of People using Primarily Solid Fuels (Distal 2010)",
                    New Single() {GDPPCP(R%), EDYRSAG25(R%, 3)}, FormValue,
                    New String() {"GDPPCP", "Educ25"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                'jrs 2013/12/19 ENSOLFUELShift(R%) = ENSOLFUEL(R%) / (100 * Exp(FormValue) / (Exp(FormValue) + 1))
                ENSOLFUELShift(R%) = CSng(ENSOLFUEL(R%) / (100 / (Exp(Amin(-1 * FormValue, 709)) + 1)))

                'jrs 2019/11/19
                'ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                '        .VarName = "ENSOLFUEL",
                '        .Year = CShort(IY% + FirstYear - 1),
                '        .Region = CShort(R%),
                '        .Dim2 = 0,
                '        .Dim3 = 0,
                '        .DistalFormVal = CSng(ENSOLFUEL(R%))
                '            })

                'Save it
                'ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                '        .VarName = "ensfvent",
                '        .Year = CShort(IY% + FirstYear - 1),
                '        .Region = CShort(R%),
                '        .Dim2 = 0,
                '        .Dim3 = 0,
                '        .DistalFormVal = CSng(1)
                '            })

                'ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                '        .VarName = "ENVPM2PT5",
                '        .Year = CShort(IY% + FirstYear - 1),
                '        .Region = CShort(R%),
                '        .Dim2 = 0,
                '        .Dim3 = 0,
                '        .DistalFormVal = CSng(ENVPM2PT5(R))
                '            })

                'jrs 2021/08/05 start
                Call AnalFunc("Child Stunting (2017) versus GDPPCP and EDYRSAG25",
                New Single() {GDPPCP(R%), EDYRSAG25(R%, 3)}, FormValue,
                New String() {"GDPPCP[0](2017) (Log)", "EDYRSAG25[0],Total(2017) (Log)"}, context)
                FormValue = CSng(AMAX(FormValue, 0.01))
                HLSTUNTCHLShift(R%) = HLSTUNTCHL(R%) / FormValue
                'jrs 2021/08/05 end
            Next R%
            'jrs 2019/11/19 ifsPopWorkDB.Flush()
            'jrs 2024/01/24
            If MultiThreading Then
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertMorDetNormFactors(NormFactors, NCohorts, NR))
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertMorDetFactors(Factors, NCohorts, NR))
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(MALNCHP, NR, IY + FirstYear - 1, "MALNCHP"))
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(HLBMI, NR, IY + FirstYear - 1, "HLBMI", 2))
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDriversT(HLBMI, popgdr, NR, IY + FirstYear - 1, "HLBMI"))
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(ENSOLFUEL, NR, IY + FirstYear - 1, "ENSOLFUEL"))
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(1, NR, IY + FirstYear - 1, "ensfvent"))
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(ENVPM2PT5, NR, IY + FirstYear - 1, "ENVPM2PT5"))
                'jrs 2021/08/05
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(HLSTUNTCHL, NR, IY + FirstYear - 1, "HLSTUNTCHL"))
            Else
                ifsPopWorkDB.BulkInsertMorDetNormFactors(NormFactors, NCohorts, NR)
                ifsPopWorkDB.BulkInsertMorDetFactors(Factors, NCohorts, NR)
                ifsPopWorkDB.BulkInsertHLProxDrivers(MALNCHP, NR, IY + FirstYear - 1, "MALNCHP")
                ifsPopWorkDB.BulkInsertHLProxDrivers(HLBMI, NR, IY + FirstYear - 1, "HLBMI", 2)
                ifsPopWorkDB.BulkInsertHLProxDriversT(HLBMI, popgdr, NR, IY + FirstYear - 1, "HLBMI")
                ifsPopWorkDB.BulkInsertHLProxDrivers(ENSOLFUEL, NR, IY + FirstYear - 1, "ENSOLFUEL")
                ifsPopWorkDB.BulkInsertHLProxDrivers(1, NR, IY + FirstYear - 1, "ensfvent")
                ifsPopWorkDB.BulkInsertHLProxDrivers(ENVPM2PT5, NR, IY + FirstYear - 1, "ENVPM2PT5")
                ifsPopWorkDB.BulkInsertHLProxDrivers(HLSTUNTCHL, NR, IY + FirstYear - 1, "HLSTUNTCHL")
            End If
            'jrs 2008/10/09
            'Transition Factors
            SaveTransitionFactors(mordstDet, Ntypes)
        End Sub

        'jrs 2008/10/09
        Private Sub SaveTransitionFactors(ByRef mordstDet(,,) As Double, ByVal Ntypes As Integer)
            Dim R As Integer, j, z As Integer
            'gvdb 2024/04/05
            ' unused variable
            'Dim RegVal As Single
            Dim NormFacM As Single, NormFacF As Single
            For R% = 1 To NR%
                'only for low income countries
                If GDPPCIP(R) < 3 Then
                    For j = 0 To NCohorts
                        'All Years: First Year Only, Normalization factor
                        ' from Initialization Data to Mortality in the Population Module
                        '---------------------------------------------------------------
                        'mod 2 will give only 2 values 0 or 1
                        '(k + 1) mod 2 will give 0 for odds, 1 for evens
                        ' odds are males (1), evens are females (2)
                        NormFacM = 0
                        NormFacF = 0
                        'Excluding AIDS in the normalization
                        'Aids are types 21 and 22
                        'Males
                        '------
                        For z = 1 To 19 Step 2 'jrs 2009/09/15 don't use new comm. dis types (included in old other comm.)
                            'if there's no historic data, use Mathers' formula
                            If mordstDet(j, R, z) = -1 Then
                                NormFacM = CSng(NormFacM + modmordstdet2(j, R, z))
                            Else 'Using historic data
                                NormFacM = CSng(NormFacM + mordstDet(j, R, z))
                            End If
                        Next z
                        If NormFacM <> 0 Then
                            'Remove Aids from the value in Population Mortality for the normalization
                            NormFacM = CSng((modmordst(j, R, 1) - modmordstdet(j, R, 21)) / NormFacM)
                        Else
                            NormFacM = 0
                        End If
                        'Females
                        '------
                        For z = 2 To 20 Step 2 'jrs 2009/09/15 don't use new comm. dis types (included in old other comm.)
                            'if there's no historic data, use Mathers' formula
                            If mordstDet(j, R, z) = -1 Then
                                NormFacF = CSng(NormFacF + modmordstdet2(j, R, z))
                            Else 'Using historic data
                                NormFacF = CSng(NormFacF + mordstDet(j, R, z))
                            End If
                        Next z
                        If NormFacF <> 0 Then
                            'Remove Aids from the value in Population Mortality for the normalization
                            NormFacF = CSng((modmordst(j, R, 2) - modmordstdet(j, R, 22)) / NormFacF)
                        Else
                            NormFacF = 0
                        End If

                        For k = 1 To Ntypes 'cuases of mortality (odd for male, even for female) 'jrs 2007/06/26 9 causes 2 genders
                            'All Years: First Year Only, Add Scaling factor
                            'from Mathers' equation to Initialization Data
                            '----------------------------------------------------

                            'jrs 2007/06/26  change to additive factor Factors(j, R%, k) = NormFac * mordstDet(j, R, k) / modmordstdet(j, R, k)
                            If k Mod 2 = 0 Then
                                NormFactors2(j, R%, k) = NormFacF
                            Else
                                NormFactors2(j, R%, k) = NormFacM
                            End If
                            If mordstDet(j, R, k) = -1 Then
                                Factors2(j, R%, k) = 1
                            ElseIf modmordstdet2(j, R, k) <> 0 Then
                                Factors2(j, R%, k) = mordstDet(j, R, k) / modmordstdet2(j, R, k)
                            Else
                                If mordstDet(j, R, k) = 0 Then
                                    Factors2(j, R%, k) = 1
                                Else
                                    Factors2(j, R%, k) = CSng(mordstDet(j, R, k) / 0.0000000001)
                                    modmordstdet2(j, R, k) = 0.0000000001
                                End If
                            End If

                        Next k

                        'First Year only: - Compute Shift Factor for Diarrhea
                        '---------------------------------------------------------
                        'ln(MortDiarr) = C + B * ln(MortCommDis)
                        'units in 100,000
                        '                For g = 1 To 2
                        '                    'jrs 2009/05/01 adding factors
                        '                    If modmordstdet2(j, R%, g) > 0 And Factors2(j, R%, g) > 0 And NormFactors2(j, R%, g) > 0 Then
                        '                        RegVal = Exp(mortFactorsInf(j, g, 1) + mortFactorsInf(j, g, 2) * Log(modmordstdet2(j, R%, g) * Factors2(j, R%, g) * NormFactors2(j, R%, g) * 100)) / 100
                        '                        mortShiftFactorsDiarr2(j, R%, g) = mortDiarrhea(j, R%, g) / RegVal
                        '                    Else
                        '                        'if Comm. Dis. is 0, then diarrhea should be 0 too
                        '                        mortShiftFactorsDiarr2(j, R%, g) = 0
                        '                    End If
                        '                Next g

                    Next j
                End If 'only for low income countries

            Next R%

        End Sub

        'jrs 2013/12/06
        Private Sub ApplyFactors(context As IForecastContext)
            Dim FormValue As Single
            Dim G As Integer, T, k As Integer
            'jrs 2015/12/04
            Dim FormValue2 As Single

            Dim MNDiff As Single
            Dim PAF1 As Single, PAF2 As Single
            'jrs 2009/08/20
            Dim MNDiarrDiff As Single
            'jrs 2009/09/15
            Dim MNMalDiff As Single, MNRespInfDiff As Single
            'jrs 2014/01/31
            Dim sAgeRange As String

            'jrs 2008/08/20
            'gvdb 2024/04/05
            'unused variable
            'Dim ObDiffM As Single, ObDiffF As Single
            'jrs 2009/09/18
            'Age by Gender
            Dim BMIDiff(5, 2) As Single
            'gvdb 2024/04/05
            'unused variable
            'Dim ObPct, OwPct As Single
            'jrs 2013/12/05
            'Age by Gender
            Dim COOKSTRIDiff As Single
            'jrs 2021/08/05
            Dim STUNTCHLDiff(3) As Single

            'jrs 2009/02/16
            'Health Expenditures Factor
            Dim HlExpFct As Single
            'jrs 2014/05/27
            Dim HlExpToCostGapFact() As Single
            ReDim HlExpToCostGapFact(CInt(Ntypes / 2))
            'jrs 2009/03/27
            'Water and Sanitation Factor
            Dim WATSANFct As Single
            'jrs 2009/10/28
            'Solid Fuel
            Dim SFRIDiff As Single, SFRDMDiff As Single, SFRDFDiff As Single
            'jrs 2009/10/29
            Dim PM2pt5Diff, ENSOLFUELDIST, HLBMIDISTM, HLBMIDISTF As Single
            Dim VentFct, VentFctCh, limit, portion, DiffMorts, Inc As Double

            'jrs 2019/11/19 need to save PROXIMATE drives for BulkInsert
            Dim MALNCHPPD(), HLBMIPD(,), ENSOLFUELPD() As Single
            ReDim MALNCHPPD(NR), HLBMIPD(NR, 2), ENSOLFUELPD(NR)

            For R% = 1 To NR
                'If IY% = 2070 - FirstYear + 1 And R% = 159 Then Stop
                'modmordstdet(5, 49, 1)

                'All Years:
                'xxxx Malnutrition proportion difference
                'Malnutrition shift plus PAF
                '---------------------------------------------
                If IY > 1 And hlmlnsw > 0 Then 'jrs 2008/01/09 added and hlmlnsw(R%)>0
                    'If GRPS(1, R) = "Nepal" And (IY = 2020 - FirstYear + 1) And j = 1 And k = 2 Then Stop
                    'If GRPS(1, R) = "Japan" Then Stop
                    'jrs 2008/01/09 Call XYTABL(TbName$, GDPPCP(R%), FormValue)
                    'jrs 2009/12/16 Call AnalFunc("Percent of Children Malnourished (2005)", _
                    'new Single() {GDPPCP(R%), EDYRSAG25(R%, 3)), FormValue)
                    'jrs 2009/12/23 FormValue = Exp(3.54 - 0.524 * Log(GDPPCP(R%)) - 0.478 * Log(EDYRSAG25(R%, 3)))
                    FormValue = CSng(-0.714297 - 0.58979 * Log(GDPPCP(R%)) - 0.544938 * Log(EDYRSAG25(R%, 3)))
                    FormValue = CSng(100 * Exp(FormValue) / (1 + Exp(FormValue)))

                    'jrs 2009/04/02 changing to additive factor
                    '            'jrs 2009/03/10 using convergence
                    '            If MNfactors(R%) = 0 Then
                    '                FormValue = FormValue * MNfactors(R%)
                    '            Else
                    '                FormValue = FormValue * ConvergeOverTime(MNfactors(R%), 1, 100)
                    '            End If
                    FormValue = FormValue + ConvergeOverTime(CSng(MNfactors(R%)), 0, 100)

                    '            If FormValue < 0 Then Stop
                    'jrs 2007/07/16
                    FormValue = Amin(FormValue, 100)
                    FormValue = AMAX(FormValue, 0)
                    'jrs 2015/12/04 SAM ******** begin *******************
                    FormValue2 = CSng(-1.453 - 0.432 * Log(GDPPCP(R%)) - 0.553 * Log(EDYRSAG25(R%, 3)))
                    FormValue2 = CSng(100 * Exp(FormValue2) / (1 + Exp(FormValue2)))
                    FormValue2 = FormValue2 + ConvergeOverTime(CSng(SAMfactors(R%)), 0, 100)
                    FormValue2 = CSng(Amin(FormValue2, 0.91 * FormValue))
                    FormValue2 = AMAX(FormValue2, 0)
                    'jrs 2015/12/04 SAM ******** end *********************

                    '            If FormValue = 0 And MALNPOPP(R%) <> 0 Then
                    '                MNDiff = 1
                    '            ElseIf FormValue = 0 And MALNPOPP(R%) = 0 Then
                    'jrs 2008/01/09 removing condition for developed countries
                    '            If FormValue < 1 Or MALNCHP(R%) < 1 Then
                    '                MNDiff = 0
                    '            Else
                    'jrs 2008/01/09 MNDiff = (MALNCHP(R%) - FormValue) / FormValue
                    'Compute PAF
                    PAF1 = PAF(MALNCHP(R%) / 100, MALNCHPSAM(R%) / 100, rrOCD)
                    PAF2 = PAF(FormValue / 100, FormValue2 / 100, rrOCD)
                    'jrs 2009/03/05 changing to ratio formulation
                    'MNDiff = PAF1 - PAF2
                    MNDiff = ((1 - PAF2) / (1 - PAF1)) - 1
                    '            End If
                    'MNDiff = MNDiff * hlmlnsw(R%)

                    'jrs 2009/08/20
                    PAF1 = PAF(MALNCHP(R%) / 100, MALNCHPSAM(R%) / 100, rrD)
                    PAF2 = PAF(FormValue / 100, FormValue2 / 100, rrD)
                    MNDiarrDiff = ((1 - PAF2) / (1 - PAF1)) - 1

                    'jrs 2009/09/15
                    PAF1 = PAF(MALNCHP(R%) / 100, MALNCHPSAM(R%) / 100, rrM)
                    PAF2 = PAF(FormValue / 100, FormValue2 / 100, rrM)
                    MNMalDiff = ((1 - PAF2) / (1 - PAF1)) - 1

                    PAF1 = PAF(MALNCHP(R%) / 100, MALNCHPSAM(R%) / 100, rrRI)
                    PAF2 = PAF(FormValue / 100, FormValue2 / 100, rrRI)
                    MNRespInfDiff = ((1 - PAF2) / (1 - PAF1)) - 1


                    'jrs 2009/03/24
                    'jrs 2019/11/19
                    'ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                    '.VarName = "MALNCHP",
                    '.Year = CShort(IY% + FirstYear - 1),
                    '.Region = CShort(R%),
                    '.Dim2 = 0,
                    '.Dim3 = 0,
                    '.DistalFormVal = CSng(FormValue)
                    '    })
                    MALNCHPPD(R) = FormValue
                Else
                    MNDiff = 0
                    'jrs 2009/08/20
                    MNDiarrDiff = 0
                    'jrs 2009/09/15
                    MNMalDiff = 0
                    MNRespInfDiff = 0
                End If

                'jrs 2008/08/20
                'All Years:
                'xxxx Obesity proportion difference
                'Obesity shift plus PAF
                '------------------------------------

                If IY > 1 And hlobsw > 0 Then
                    'Males
                    Call AnalFunc("BMI Male (2005)",
                    New Single() {GDPPCP(R%), EDYRSAG25(R%, 3)}, FormValue,
                    New String() {"GDPPCP", "Educ25"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                    FormValue = CSng(FormValue * BMIfactorsM(R%))
                    'from BMI to %Obesity, BMIOb = 30
                    'jrs 2009/09/01 FormValue = 1 - NormalCDF(BMIOb, FormValue, BMISTDPrp * FormValue)
                    'JRS 2009/09/18
                    'TbName$ = "BMI Versus Male Obesity % (CRA) Quad"
                    'Call XYTABL(TbName$, FormValue, FormValue)
                    'FormValue = FormValue / 100

                    'FormValue = Amin(FormValue, 1)
                    'FormValue = AMAX(FormValue, 0)

                    'Compute PAF
                    'PAF1 = PAFOb(HLOBESITY(R%, 1) / 100, "M")
                    PAF1 = PAFBMI(HLBMI(R, 1), "30-44", "M", rrCV)
                    'PAF2 = PAFOb(FormValue, "M")
                    PAF2 = PAFBMI(FormValue, "30-44", "M", rrCV)

                    'jrs 2009/03/27 ObDiffM = PAF1 - PAF2
                    'ObDiffM = ((1 - PAF2) / (1 - PAF1)) - 1
                    BMIDiff(1, 1) = ((1 - PAF2) / (1 - PAF1)) - 1

                    'JRS 2009/09/21
                    PAF1 = PAFBMI(HLBMI(R, 1), "45-59", "M", rrCV)
                    PAF2 = PAFBMI(FormValue, "45-59", "M", rrCV)
                    BMIDiff(2, 1) = ((1 - PAF2) / (1 - PAF1)) - 1

                    PAF1 = PAFBMI(HLBMI(R, 1), "60-69", "M", rrCV)
                    PAF2 = PAFBMI(FormValue, "60-69", "M", rrCV)
                    BMIDiff(3, 1) = ((1 - PAF2) / (1 - PAF1)) - 1

                    PAF1 = PAFBMI(HLBMI(R, 1), "70-79", "M", rrCV)
                    PAF2 = PAFBMI(FormValue, "70-79", "M", rrCV)
                    BMIDiff(4, 1) = ((1 - PAF2) / (1 - PAF1)) - 1

                    PAF1 = PAFBMI(HLBMI(R, 1), "80+", "M", rrCV)
                    PAF2 = PAFBMI(FormValue, "80+", "M", rrCV)
                    BMIDiff(5, 1) = ((1 - PAF2) / (1 - PAF1)) - 1

                    'jrs 2009/09/21 saving bmi instead of obesity
                    'jrs 2009/03/24
                    '            HLOBESITYDISTM = FormValue * 100
                    '            TblHLPRDR.AddNew
                    '            TblHLPRDR.Fields("VarName") = "HLOBESITY"
                    '            TblHLPRDR.Fields("Year") = IY% + frm_MainMenu.IFsGUIctrl1.cFirstYear - 1
                    '            TblHLPRDR.Fields("Region") = R%
                    '            TblHLPRDR.Fields("Dim2") = 1
                    '            TblHLPRDR.Fields("DistalFormVal") = HLOBESITYDISTM
                    '            TblHLPRDR.Update

                    HLBMIDISTM = FormValue
                    'jrs 2019/11/19
                    'ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                    '.VarName = "HLBMI",
                    '.Year = CShort(IY% + FirstYear - 1),
                    '.Region = CShort(R%),
                    '.Dim2 = 1,
                    '.Dim3 = 0,
                    '.DistalFormVal = CSng(HLBMIDISTM)
                    '    })
                    HLBMIPD(R, 1) = HLBMIDISTM

                    'Females
                    Call AnalFunc("BMI Female (2005)",
                    New Single() {GDPPCP(R%), EDYRSAG25(R%, 3)}, FormValue,
                    New String() {"GDPPCP", "Educ25"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                    FormValue = CSng(FormValue * BMIfactorsF(R%))
                    'from BMI to %Obesity, BMIOb = 30
                    'jrs 2009/09/01 FormValue = 1 - NormalCDF(BMIOb, FormValue, BMISTDPrp * FormValue)
                    'JRS 2009/09/21
                    '            TbName$ = "BMI Versus Female Obesity % (CRA) Quad"
                    '            Call XYTABL(TbName$, FormValue, FormValue)
                    '            FormValue = FormValue / 100
                    '
                    '            FormValue = Amin(FormValue, 1)
                    '            FormValue = AMAX(FormValue, 0)
                    'Compute PAF
                    '            PAF1 = PAFOb(HLOBESITY(R%, 2) / 100, "F")
                    '            PAF2 = PAFOb(FormValue, "F")
                    '            'jrs 2009/03/27 ObDiffM = PAF1 - PAF2
                    '            ObDiffF = ((1 - PAF2) / (1 - PAF1)) - 1 'jrs 2009/07/02 I was using ObDiffM
                    PAF1 = PAFBMI(HLBMI(R, 2), "30-44", "F", rrCV)
                    PAF2 = PAFBMI(FormValue, "30-44", "F", rrCV)
                    BMIDiff(1, 2) = ((1 - PAF2) / (1 - PAF1)) - 1

                    PAF1 = PAFBMI(HLBMI(R, 2), "45-59", "F", rrCV)
                    PAF2 = PAFBMI(FormValue, "45-59", "F", rrCV)
                    BMIDiff(2, 2) = ((1 - PAF2) / (1 - PAF1)) - 1

                    PAF1 = PAFBMI(HLBMI(R, 2), "60-69", "F", rrCV)
                    PAF2 = PAFBMI(FormValue, "60-69", "F", rrCV)
                    BMIDiff(3, 2) = ((1 - PAF2) / (1 - PAF1)) - 1

                    PAF1 = PAFBMI(HLBMI(R, 2), "70-79", "F", rrCV)
                    PAF2 = PAFBMI(FormValue, "70-79", "F", rrCV)
                    BMIDiff(4, 2) = ((1 - PAF2) / (1 - PAF1)) - 1

                    PAF1 = PAFBMI(HLBMI(R, 2), "80+", "F", rrCV)
                    PAF2 = PAFBMI(FormValue, "80+", "F", rrCV)
                    BMIDiff(5, 2) = ((1 - PAF2) / (1 - PAF1)) - 1

                    'jrs 2009/03/24
                    '            HLOBESITYDISTF = FormValue * 100
                    '            TblHLPRDR.AddNew
                    '            TblHLPRDR.Fields("VarName") = "HLOBESITY"
                    '            TblHLPRDR.Fields("Year") = IY% + frm_MainMenu.IFsGUIctrl1.cFirstYear - 1
                    '            TblHLPRDR.Fields("Region") = R%
                    '            TblHLPRDR.Fields("Dim2") = 2
                    '            TblHLPRDR.Fields("DistalFormVal") = HLOBESITYDISTF
                    '            TblHLPRDR.Update

                    HLBMIDISTF = FormValue

                    'jrs 2019/11/19
                    'ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                    '.VarName = "HLBMI",
                    '.Year = CShort(IY% + FirstYear - 1),
                    '.Region = CShort(R%),
                    '.Dim2 = 2,
                    '.Dim3 = 0,
                    '.DistalFormVal = CSng(HLBMIDISTF)
                    '    })
                    HLBMIPD(R, 2) = HLBMIDISTF

                    'jrs 2019/11/19
                    '            ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                    '            .VarName = "HLBMI",
                    '            .Year = CShort(IY% + FirstYear - 1),
                    '            .Region = CShort(R%),
                    '            .Dim2 = 3,
                    '            .Dim3 = 0,
                    '            .DistalFormVal = CSng((HLBMIDISTM * popgdr(R%, 1) + HLBMIDISTF *
                    'popgdr(R%, 2)) / (popgdr(R%, 1) + popgdr(R%, 2)))
                    '                })

                Else 'IF NOT YEAR > 1 OR OBSW IS OFF
                    'JRS 2009/09/21
                    'ObDiffM = 0
                    'ObDiffF = 0
                    For II = 1 To 5
                        For G = 1 To 2
                            BMIDiff(II, G) = 0
                        Next G
                    Next II
                End If 'IF YEAR>1 AND OBSW IS ON ELSE

                'jrs 2009/02/16
                'Health Expenditures Factor
                If IY = 1 Then 'save
                    GDSHI(R) = GDS(R, Health) / GDP(R) * 100
                End If

                If IY > 1 And GDPPCIP(R) < 15 And GDSHI(R%) > 0 Then
                    'jrs 2010/01/25 divided by GDSHI and added limit of -0.8
                    HlExpFct = elhlmortspn * ((100 * GDS(R%, Health) / GDP(R%)) - GDSHI(R%)) / GDSHI(R%)
                    HlExpFct = CSng(AMAX(HlExpFct, -0.8))
                Else
                    HlExpFct = 0
                End If

                'jrs 2014/05/27
                'jrs 2014/06/20 If IY > 2 Then
                'jrs 2014/06/20 using only NCDs alphas, for Males and all, assuming if using SENS, we're using it for all 3 big types
                'jrs 2014/09/24 added and not using ratio, also added check for all types
                If IY > 2 Then
                    For k = 1 To CInt(Ntypes / 2)
                        If hllongvtyalpha(R, k, 1) = -1 And hllongvtyalpha(R, k, 2) = -1 And hllongvtyalpha(R, k, 3) = -1 _
            And hllongvtyalpharatio(k, 1) = 0 And hllongvtyalpharatio(k, 2) = 0 And hllongvtyalpharatio(k, 3) = 0 Then
                            HlExpToCostGapFact(k) = hlexpcostgaptohealthel(k) * SHealthExpToCostGap(R)
                            HlExpToCostGapFact(k) = CSng(AMAX(HlExpToCostGapFact(k), -0.8))
                        Else
                            HlExpToCostGapFact(k) = 0
                        End If
                    Next k
                Else
                    For k = 1 To CInt(Ntypes / 2)
                        HlExpToCostGapFact(k) = 0
                    Next k
                End If

                'jrs 2009/03/27
                'Water and Sanitation effect on Mortality
                If IY > 1 And hlwatsansw > 0 Then
                    'If RegName(R) = "Cameroon" And FirstYear + IY - 1 >= 2028 Then Stop
                    'jrs 2015/03/18 *** begin ***
                    'Full Model
                    'PAF1 = PAFWATSAN(WATSAFE(R, 1), WATSAFE(R, 2), WATSAFE(R, 3), _
                    'SANITATION(R, 1), SANITATION(R, 2), SANITATION(R, 3), rrWATSANDia)
                    'mti 2022/11/15 start
                    If watsafenewsw = 1 And watsafefwdsw = 1 Then 'new 5 category model

                        Dim WATSAFE3C_1 = WATERHH(R%, Surface, National) + WATERHH(R%, Unimproved, National)
                        Dim WATSAFE3C_2 = WATERHH(R%, Limited, National) + WATERHH(R%, Basic, National)
                        Dim WATSAFE3C_3 = WATERHH(R%, SafelyManaged, National)

                        Dim SANITATION3C_3 = SANITHH(R%, SafelyManaged, National)

                        PAF1 = PAFWATSAN(WATSAFE3C_1, WATSAFE3C_2, WATSAFE3C_3,
                                SANITATION3C_3, WATERFILTERED, rrSANDia(1), rrWATDia, R)

                    Else

                        PAF1 = PAFWATSAN(WATSAFE(R, 1), WATSAFE(R, 2), WATSAFE(R, 3),
                                SANITATION(R, 3), WATERFILTERED, rrSANDia(1), rrWATDia, R)
                    End If


                    'Distal Driver Formulation
                    'Note WATSAFE2 (used in PAF 2 is already saved in database with new 5 catgeory model when switch is on
                    'mti 2022/11/15 end
                    'Distal Driver Formulation
                    'PAF2 = PAFWATSAN(WATSAFE2(R, 1), WATSAFE2(R, 2), WATSAFE2(R, 3), _
                    'SANITATION2(R, 1), SANITATION2(R, 2), SANITATION2(R, 3), rrWATSANDia)
                    PAF2 = PAFWATSAN(WATSAFE2(R, 1), WATSAFE2(R, 2), WATSAFE2(R, 3),
                    SANITATION2(R, 3), WATERFILTEREDI, rrSANDia(1), rrWATDia, R)
                    'jrs 2015/03/18 *** end ***
                    'Effect on Mortality
                    WATSANFct = ((1 - PAF2) / (1 - PAF1)) - 1
                Else
                    WATSANFct = 0
                End If

                'jrs 2013/09/20
                If IY = 1 And (GDPPCI(R%) < 5 Or ENSOLFUELI(R%) > 10) Then
                    VentFctI(R%) = ensfvent(R%)
                    If GRPS(1, R%) = "China" Then
                        'Ventilation factor for children is half of the one for adults
                        'originally 0.5 and 0.25
                        VentFctChI(R%) = CSng(VentFctI(R%) * 0.5)
                    Else
                        VentFctChI(R%) = ensfvent(R%)
                    End If
                End If

                'jrs 2009/10/28 Solid Fuels
                If IY > 1 And hlsolfuelsw = 1 Then
                    'jrs 2009/10/28
                    'shift factor for solid fuels (distal driver)
                    'jrs 2013/12/19
                    'Call AnalFunc("Percent of People using Primarily Solid Fuels", _
                    'new Single() {GDPPCP(R%), EDYRSAG25(R%, 3)), FormValue)
                    Call AnalFunc("Percent of People using Primarily Solid Fuels (Distal 2010)",
                    New Single() {GDPPCP(R%), EDYRSAG25(R%, 3)}, FormValue,
                    New String() {"GDPPCP", "Educ25"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                    'jrs 2013/12/19 ENSOLFUELDIST = ENSOLFUELShift(R%) * (100 * Exp(FormValue) / (Exp(FormValue) + 1))
                    ENSOLFUELDIST = CSng(ENSOLFUELShift(R%) * (100 / (Exp(Amin(-1 * FormValue, 709)) + 1)))

                    'jrs 2015/06/02
                    ENSOLFUELDIST = Amin(AMAX(ENSOLFUELDIST, 0), 100)

                    'jrs 2019/11/19
                    'ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                    '.VarName = "ENSOLFUEL",
                    '.Year = CShort(IY% + FirstYear - 1),
                    '.Region = CShort(R%),
                    '.Dim2 = 0,
                    '.Dim3 = 0,
                    '.DistalFormVal = CSng(ENSOLFUELDIST)
                    '    })
                    ENSOLFUELPD(R) = ENSOLFUELDIST

                    'jrs 2010/01/28
                    VentFct = ensfvent(R%)
                    If GRPS(1, R%) = "China" Then
                        'Ventilation factor for children is half of the one for adults
                        'originally 0.5 and 0.25
                        VentFctCh = CSng(VentFct * 0.5)
                    Else
                        VentFctCh = VentFct
                    End If

                    'jrs 2013/09/20 - making ventilation effect the extreme positive
                    'minimizing effect of indoor air polution
                    If GDPPCI(R%) > 5 And ENSOLFUELI(R%) < 10 Then
                        VentFct = 0
                        VentFctCh = 0
                    End If

                    '            If GRPS(1, R%) = "China" Then
                    '                PopCh = 0
                    '                For j = 0 To 3
                    '                    PopCh = PopCh + agedst(j, R%, 1) + agedst(j, R%, 2)
                    '                Next j
                    '                PopAd = POP(R%) - PopCh
                    '                'Ventilation factor for children is half of the one for adults
                    '                'originally 0.5 and 0.25
                    '                VentFct = (PopAd * ensfvent(R%) + PopCh * ensfvent(R%) * 0.5) / POP(R%)
                    '
                    '                'Save it
                    '                TblHLPRDR.AddNew
                    '                TblHLPRDR.Fields("VarName") = "ensfvent"
                    '                TblHLPRDR.Fields("Year") = IY% + frm_MainMenu.IFsGUIctrl1.cFirstYear - 1
                    '                TblHLPRDR.Fields("Region") = R%
                    '                TblHLPRDR.Fields("DistalFormVal") = VentFct
                    '                TblHLPRDR.Update
                    '            Else
                    '                VentFct = ensfvent(R%)
                    '            End If

                    'jrs 2013/09/20
                    '            'Effect on Respiratory Infections
                    '            SFRIDiff = ((rrSFRI(1) - 1) * ENSOLFUEL(R%) / 100 * VentFctCh + 1) / ((rrSFRI(1) - 1) * ENSOLFUELDIST / 100 * VentFctCh + 1) - 1
                    '            'Effect on Respiratory Disease for Males
                    '            SFRDMDiff = ((rrSFRD(1) - 1) * ENSOLFUEL(R%) / 100 * VentFct + 1) / ((rrSFRD(1) - 1) * ENSOLFUELDIST / 100 * VentFct + 1) - 1
                    '            'Effect on Respiratory Disease for Females
                    '            SFRDFDiff = ((rrSFRD(2) - 1) * ENSOLFUEL(R%) / 100 * VentFct + 1) / ((rrSFRD(2) - 1) * ENSOLFUELDIST / 100 * VentFct + 1) - 1
                    'Effect on Respiratory Infections
                    SFRIDiff = CSng(((rrSFRI(1) - 1) * ENSOLFUEL(R%) / 100 * VentFctCh + 1) / ((rrSFRI(1) - 1) * ENSOLFUELDIST / 100 * VentFctChI(R%) + 1) - 1)
                    'Effect on Respiratory Disease for Males
                    SFRDMDiff = CSng(((rrSFRD(1) - 1) * ENSOLFUEL(R%) / 100 * VentFct + 1) / ((rrSFRD(1) - 1) * ENSOLFUELDIST / 100 * VentFctI(R%) + 1) - 1)
                    'Effect on Respiratory Disease for Females
                    SFRDFDiff = CSng(((rrSFRD(2) - 1) * ENSOLFUEL(R%) / 100 * VentFct + 1) / ((rrSFRD(2) - 1) * ENSOLFUELDIST / 100 * VentFctI(R%) + 1) - 1)

                Else
                    SFRIDiff = 0
                    SFRDMDiff = 0
                    SFRDFDiff = 0
                End If

                'jrs 2013/12/05 *** begin ***
                'Cook Stoves
                '-----------
                If IY > 1 And hlsolfuelsw = 2 Then
                    'Move Lagged effects one year
                    For G = 1 To 2
                        'jrs 2014/01/31 adding age dimension from 25 to 80+
                        For j = 1 To 12
                            For T = 3 To 2 Step -1 'jrs 2014/02/17 added an extra year for lag
                                COOKSTIHDDiff(R, G, j, T) = COOKSTIHDDiff(R, G, j, T - 1)
                                COOKSTSTDiff(R, G, j, T) = COOKSTSTDiff(R, G, j, T - 1)
                            Next T
                        Next j
                    Next G
                    'Move Lagged effects five year
                    For G = 1 To 2
                        For T = 5 To 2 Step -1
                            COOKSTRDDiff(R, G, T) = COOKSTRDDiff(R, G, T - 1)
                            COOKSTLCDiff(R, G, T) = COOKSTLCDiff(R, G, T - 1)
                        Next T
                    Next G

                    'jrs 2014/01/24 *** begin ***
                    'Respiratory Infections
                    '======================
                    'Full Model
                    PAF1 = PAFCOOKST(rrCOOKSTRI, "", "0-4", R%) 'jrs 2014/02/21
                    'Distal Driver Formulation
                    PAF2 = PAFCOOKSTDD(rrCOOKSTRIDD, "", "0-4", R%, COOKSTOVESDD) 'jrs 2014/02/21
                    'Effect on Mortality
                    COOKSTRIDiff = ((1 - PAF2) / (1 - PAF1)) - 1
                    'Respiratory Disease
                    '======================
                    'Males
                    '-----
                    'Full Model
                    PAF1 = PAFCOOKST(rrCOOKSTRD, "M", "25+", R%)
                    'Distal Driver Formulation
                    PAF2 = PAFCOOKSTDD(rrCOOKSTRDDD, "M", "25+", R%, COOKSTOVESDD)
                    'Effect on Mortality
                    COOKSTRDDiff(R, 1, 1) = ((1 - PAF2) / (1 - PAF1)) - 1
                    'Females
                    '-----
                    'Full Model
                    PAF1 = PAFCOOKST(rrCOOKSTRD, "F", "25+", R%)
                    'Distal Driver Formulation
                    PAF2 = PAFCOOKSTDD(rrCOOKSTRDDD, "F", "25+", R%, COOKSTOVESDD)
                    'Effect on Mortality
                    COOKSTRDDiff(R, 2, 1) = ((1 - PAF2) / (1 - PAF1)) - 1
                    'Lung Cancer
                    '======================
                    'Males
                    '-----
                    'Full Model
                    PAF1 = PAFCOOKST(rrCOOKSTLC, "M", "25+", R%)
                    'Distal Driver Formulation
                    PAF2 = PAFCOOKSTDD(rrCOOKSTLCDD, "M", "25+", R%, COOKSTOVESDD)
                    'Effect on Mortality
                    COOKSTLCDiff(R, 1, 1) = ((1 - PAF2) / (1 - PAF1)) - 1
                    'Females
                    '-----
                    'Full Model
                    PAF1 = PAFCOOKST(rrCOOKSTLC, "F", "25+", R%)
                    'Distal Driver Formulation
                    PAF2 = PAFCOOKSTDD(rrCOOKSTLCDD, "F", "25+", R%, COOKSTOVESDD)
                    'Effect on Mortality
                    COOKSTLCDiff(R, 2, 1) = ((1 - PAF2) / (1 - PAF1)) - 1

                    'jrs 2014/01/31 *** begin ***
                    'Change to include RRs by age, instead of simply 25+

                    'IHD
                    '======================
                    'Males
                    '-----
                    'By age, 25 to 80+
                    For j = 1 To 12
                        If j < 12 Then
                            sAgeRange = CStr((j + 4) * 5) & "-" & CStr((j + 5) * 5 - 1)
                        Else
                            sAgeRange = "80+"
                        End If
                        'Full Model
                        PAF1 = PAFCOOKST(rrCOOKSTIHD, "M", sAgeRange, R%)
                        'Distal Driver Formulation
                        PAF2 = PAFCOOKSTDD(rrCOOKSTIHDDD, "M", sAgeRange, R%, COOKSTOVESDD)
                        'Effect on Mortality
                        COOKSTIHDDiff(R, 1, j, 1) = ((1 - PAF2) / (1 - PAF1)) - 1
                    Next j
                    'Females
                    '-----
                    'By age, 25 to 80+
                    For j = 1 To 12
                        If j < 12 Then
                            sAgeRange = CStr((j + 4) * 5) & "-" & CStr((j + 5) * 5 - 1)
                        Else
                            sAgeRange = "80+"
                        End If
                        'Full Model
                        PAF1 = PAFCOOKST(rrCOOKSTIHD, "F", sAgeRange, R%)
                        'Distal Driver Formulation
                        PAF2 = PAFCOOKSTDD(rrCOOKSTIHDDD, "F", sAgeRange, R%, COOKSTOVESDD)
                        'Effect on Mortality
                        COOKSTIHDDiff(R, 2, j, 1) = ((1 - PAF2) / (1 - PAF1)) - 1
                    Next j

                    'Stroke
                    '======================
                    'Males
                    '-----
                    'By age, 25 to 80+
                    For j = 1 To 12
                        If j < 12 Then
                            sAgeRange = CStr((j + 4) * 5) & "-" & CStr((j + 5) * 5 - 1)
                        Else
                            sAgeRange = "80+"
                        End If
                        'Full Model
                        PAF1 = PAFCOOKST(rrCOOKSTST, "M", sAgeRange, R%)
                        'Distal Driver Formulation
                        PAF2 = PAFCOOKSTDD(rrCOOKSTSTDD, "M", sAgeRange, R%, COOKSTOVESDD)
                        'Effect on Mortality
                        COOKSTSTDiff(R, 1, j, 1) = ((1 - PAF2) / (1 - PAF1)) - 1
                    Next j
                    'Females
                    '-----
                    'By age, 25 to 80+
                    For j = 1 To 12
                        If j < 12 Then
                            sAgeRange = CStr((j + 4) * 5) & "-" & CStr((j + 5) * 5 - 1)
                        Else
                            sAgeRange = "80+"
                        End If
                        'Full Model
                        PAF1 = PAFCOOKST(rrCOOKSTST, "F", sAgeRange, R%)
                        'Distal Driver Formulation
                        PAF2 = PAFCOOKSTDD(rrCOOKSTSTDD, "F", sAgeRange, R%, COOKSTOVESDD)
                        'Effect on Mortality
                        COOKSTSTDiff(R, 2, j, 1) = ((1 - PAF2) / (1 - PAF1)) - 1
                    Next j
                    'jrs 2014/01/31 *** end ***
                    'jrs 2014/01/24 *** end ***
                Else
                    COOKSTRIDiff = 0

                    COOKSTRDDiff(R, 1, 1) = 0
                    COOKSTRDDiff(R, 2, 1) = 0

                    COOKSTLCDiff(R, 1, 1) = 0
                    COOKSTLCDiff(R, 2, 1) = 0

                    'jrs 2014/01/31 from 25 to 80+
                    For j = 1 To 12
                        COOKSTIHDDiff(R, 1, j, 1) = 0
                        COOKSTIHDDiff(R, 2, j, 1) = 0

                        COOKSTSTDiff(R, 1, j, 1) = 0
                        COOKSTSTDiff(R, 2, j, 1) = 0
                    Next j
                End If
                'jrs 2013/12/05 *** end ***

                'jrs 2009/10/29
                'Outdoor Air polution adjustment factor
                '--------------------------------------
                If IY > 1 And hlpm2pt5sw > 0 Then
                    'jrs 2019/11/19
                    'ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                    '.VarName = "ENVPM2PT5",
                    '.Year = CShort(IY% + FirstYear - 1),
                    '.Region = CShort(R%),
                    '.Dim2 = 0,
                    '.Dim3 = 0,
                    '.DistalFormVal = CSng(SENVPM2PT5DDF(R))
                    '    })

                    PM2pt5Diff = CSng((((SENVPM2PT5(R%) + 1) / (SENVPM2PT5DDF(R%) + 1)) ^ rrPM2RI(1)) - 1)
                Else
                    PM2pt5Diff = 0
                End If

                'jrs 2021/08/05 start
                'Child Stunting effect on Mortality
                '----------------------------------
                'need to add hlstuntchlsw
                If IY > 1 And hlstuntchlsw > 0 Then
                    Call AnalFunc("Child Stunting (2017) versus GDPPCP and EDYRSAG25",
                    New Single() {GDPPCP(R%), EDYRSAG25(R%, 3)}, FormValue,
                    New String() {"GDPPCP[0](2017) (Log)", "EDYRSAG25[0],Total(2017) (Log)"}, context)
                    FormValue = FormValue * HLSTUNTCHLShift(R%)
                    FormValue = CSng(AMAX(FormValue, 0.01))

                    'Other CDs
                    PAF1 = PAFSTUNTCHL(HLSTUNTCHL(R%), rrOCD_Stunt)
                    PAF2 = PAFSTUNTCHL(FormValue, rrOCD_Stunt)

                    STUNTCHLDiff(1) = ((1 - PAF2) / (1 - PAF1)) - 1

                    'Diarrhea
                    PAF1 = PAFSTUNTCHL(HLSTUNTCHL(R%), rrD_Stunt)
                    PAF2 = PAFSTUNTCHL(FormValue, rrD_Stunt)

                    STUNTCHLDiff(2) = ((1 - PAF2) / (1 - PAF1)) - 1

                    'Resp Infections
                    PAF1 = PAFSTUNTCHL(HLSTUNTCHL(R%), rrRI_Stunt)
                    PAF2 = PAFSTUNTCHL(FormValue, rrRI_Stunt)

                    STUNTCHLDiff(3) = ((1 - PAF2) / (1 - PAF1)) - 1

                    SHLSTUNTCHLDDF(R%) = FormValue

                Else
                    STUNTCHLDiff(1) = 0
                    STUNTCHLDiff(2) = 0
                    STUNTCHLDiff(3) = 0
                End If
                'jrs 2021/08/05 end

                For j = 0 To NCohorts
                    'jrs 2012/09/13
                    If j > 21 Then Exit For

                    'jrs 2009/12/09
                    If j = 0 Then
                        limit = 400
                    ElseIf j = 1 Then
                        limit = 200
                    Else
                        limit = 750
                    End If

                    For k = 1 To Ntypes 'Dont apply Factors to AIDS types 21, 22 'jrs 2007/06/26 all death types
                        If k <> 21 And k <> 22 Then
                            'jrs 2010/04/12 convergence only for subsequent years
                            'jrs 2009/12/04 adding convergence for normalization factor
                            'modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * Factors(j, R, k) * NormFactors(j, R%, k)
                            If hlgbdconvDown > 0 And NormFactors(j, R%, k) > 1 And IY > 1 Then
                                modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * ConvergeOverTime(CSng(NormFactors(j, R%, k)), 1, hlgbdconvDown)
                            ElseIf hlgbdconvUp > 0 And NormFactors(j, R%, k) < 1 And IY > 1 Then 'jrs 2010/03/10
                                modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * ConvergeOverTime(CSng(NormFactors(j, R%, k)), 1, hlgbdconvUp)
                            Else
                                modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * NormFactors(j, R%, k)
                            End If
                            'jrs 2010/04/09 adding convergence for scaling factors
                            If hlscaleconvdown > 0 And Factors(j, R%, k) > 1 And IY > 1 Then
                                modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * ConvergeOverTime(CSng(Factors(j, R%, k)), 1, hlscaleconvdown)
                            Else
                                modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * Factors(j, R%, k)
                            End If

                            'All Years: apply Malnutrition Effect to Communicable Diseases
                            '-------------------------------------------------------------
                            'jrs 2008/01/09 added and j<2, just for children under 5
                            'jrs 2009/08/11 Added Diarrhea, Malaria and Resp. Infect.
                            If IY > 1 And (k < 3 Or (k > 22 And k < 29)) And j < 2 Then
                                'jrs 2009/08/20 Diarrhea has its own RRs
                                If k = 23 Or k = 24 Then
                                    modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + MNDiarrDiff)
                                ElseIf k = 25 Or k = 26 Then 'jrs 2009/09/15
                                    modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + MNMalDiff)
                                ElseIf k = 27 Or k = 28 Then 'jrs 2009/09/15
                                    modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + MNRespInfDiff)
                                Else
                                    modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + MNDiff)
                                End If
                            End If

                            'jrs 2021/08/05 start
                            'All Years: apply Stunting effect to Communicable diseases
                            '---------------------------------------------------------
                            If IY > 1 And (k < 3 Or (k > 22 And k < 29)) And j < 2 Then
                                If k = 23 Or k = 24 Then
                                    modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + STUNTCHLDiff(2))
                                ElseIf k = 25 Or k = 26 Then 'using Other CDs for Malaria
                                    modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + STUNTCHLDiff(1))
                                ElseIf k = 27 Or k = 28 Then
                                    modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + STUNTCHLDiff(3))
                                Else
                                    modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + STUNTCHLDiff(1))
                                End If
                            End If
                            'jrs 2021/08/05 end

                            'jrs 2008/08/20
                            'All Years: apply Obesity Effect to Cardiovascular Diseases
                            '-------------------------------------------------------------
                            If IY > 1 And j > 6 Then
                                'jrs 2009/09/21
                                '                        If k = 5 Then
                                '                            modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + ObDiffM)
                                '                        ElseIf k = 6 Then
                                '                            modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + ObDiffF)
                                '                        End If
                                G = k - 4 '1 for males, 2 for females
                                If k = 5 Or k = 6 Then
                                    Select Case j
                                        Case 7, 8, 9 '30-44
                                            modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + BMIDiff(1, G))
                                        Case 10, 11, 12 '45-59
                                            modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + BMIDiff(2, G))
                                        Case 13, 14 '60-69
                                            modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + BMIDiff(3, G))
                                        Case 15, 16 '70-79
                                            modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + BMIDiff(4, G))
                                        Case Else '80+
                                            modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + BMIDiff(5, G))
                                    End Select
                                End If
                            End If

                            'jrs 2009/02/16
                            'All years (but first): apply Health Expenditures Factor
                            '-------------------------------------------------------
                            If IY > 1 And j < 2 Then
                                modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + HlExpFct)
                            End If
                            'jrs 2014/05/27
                            'All years (but first): apply Health Expenditrues to Health Cost Gap Factor
                            '--------------------------------------------------------------------------
                            If IY > 2 Then
                                modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + HlExpToCostGapFact((k + 1) \ 2))
                            End If

                            'jrs 2009/04/09
                            'All years (but first): apply WATSAN effect on Diarrhea
                            '-------------------------------------------------------
                            'jrs 2009/04/30
                            If hlwatsansw > 0 Then
                                'If RegName(R) = "Iceland" Then Stop
                                'jrs 2009/08/11
                                '                        If IY > 1 And k < 3 And modmordstdet(j, R%, k) > 0 Then 'k=1 and k=2 communicable diseases for males and females
                                '                            'Find Diarrhea Mortality
                                '                            RegVal = Exp(mortFactorsInf(j, k, 1) + mortFactorsInf(j, k, 2) * Log(modmordstdet(j, R%, k) * 100)) / 100
                                '                            RegVal = RegVal * mortShiftFactorsDiarr(j, R%, k)
                                '                            'WATSANFct holds a portion of Diarrhea to increase or reduce
                                '                            'change total communicable disease mortality
                                '                            modmordstdet(j, R%, k) = modmordstdet(j, R%, k) + RegVal * WATSANFct
                                '                        End If
                                If IY > 1 And (k = 23 Or k = 24) Then
                                    'jrs 2010/01/12
                                    'avoid shifting this effect to OCDs
                                    modmordstdet(j, R%, k - 22) = modmordstdet(j, R%, k - 22) + modmordstdet(j, R%, k) * WATSANFct

                                    modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + WATSANFct)
                                End If
                            End If

                            'jrs 2009/10/28
                            'All Years but first: apply Solid Fuel factor
                            '--------------------------------------------
                            If IY > 1 And hlsolfuelsw > 0 Then
                                If hlsolfuelsw = 1 Then
                                    'Respiratory Infections for children under 5
                                    '--------------------------------------------
                                    If j < 2 And (k = 27 Or k = 28) Then
                                        'jrs 2010/01/12
                                        'avoid shifting this effect to OCDs (1 and 2)
                                        modmordstdet(j, R%, k - 26) = modmordstdet(j, R%, k - 26) + modmordstdet(j, R%, k) * SFRIDiff

                                        modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + SFRIDiff)
                                    End If

                                    'Respiratry Disease for adults over 30
                                    '-------------------------------------
                                    If j > 6 Then
                                        If k = 9 Then
                                            modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + SFRDMDiff)
                                        ElseIf k = 10 Then
                                            modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + SFRDFDiff)
                                        End If
                                    End If

                                Else

                                    'jrs 2013/12/05 *** begin ***
                                    'Respiratory Infections
                                    '----------------------
                                    'bbh 2013/12/28
                                    If IY > 1 And (j = 0 Or j = 1) And (k = 27 Or k = 28) Then
                                        'If IY > 1 And j = 1 And (k = 27 Or k = 28) Then
                                        modmordstdet(j, R%, k - 26) = modmordstdet(j, R%, k - 26) + modmordstdet(j, R%, k) * COOKSTRIDiff

                                        modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + COOKSTRIDiff)
                                    End If

                                    'Respiratory Disease for adults over 25
                                    '--------------------------------------
                                    If IY > 1 And j > 5 And (k = 9 Or k = 10) Then
                                        If k = 9 Then
                                            G = 1
                                        Else
                                            G = 2
                                        End If
                                        'jrs 2014/01/30 added age and gender to Disease Distribution
                                        modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * COPDDist(R%, Amin(j, 17), (k - 8)) * (1 + COOKSTRDDiff(R, G, 5)) +
                                        modmordstdet(j, R%, k) * (1 - COPDDist(R%, Amin(j, 17), (k - 8)))
                                    End If

                                    'Lung Cancer for adults over 25
                                    '--------------------------------------
                                    If k = 3 Then
                                        G = 1
                                    Else
                                        G = 2
                                    End If
                                    If IY > 1 And j > 5 And (k = 3 Or k = 4) And COOKSTLCDiff(R, G, 5) <> 0 Then
                                        'jrs 2014/01/30 added age and gender to Disease Distribution
                                        modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * LungCancerDist(R%, Amin(j, 17), (k - 2)) * (1 + COOKSTLCDiff(R, G, 5)) +
                                        modmordstdet(j, R%, k) * (1 - LungCancerDist(R%, Amin(j, 17), (k - 2)))
                                    End If

                                    'IHD and Stroke for adults over 25
                                    '--------------------------------------
                                    If k = 5 Then
                                        G = 1
                                    Else
                                        G = 2
                                    End If
                                    'jrs 2014/02/17 added an extra year for lag
                                    'jrs 2014/01/31 added age dimension to Diff Adj. factor
                                    If IY > 1 And j > 5 And (k = 5 Or k = 6) And (COOKSTSTDiff(R, G, Amin(AMAX(j - 5, 1), 12), 3) <> 0 Or COOKSTIHDDiff(R, G, Amin(AMAX(j - 5, 1), 12), 3) <> 0) Then
                                        'jrs 2014/01/30 added age and gender to Disease Distribution
                                        modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * IHDDist(R%, Amin(j, 17), (k - 4)) * (1 + COOKSTIHDDiff(R, G, Amin(j - 5, 12), 3)) +
                                        modmordstdet(j, R%, k) * StrokeDist(R%, Amin(j, 17), (k - 4)) * (1 + COOKSTSTDiff(R, G, Amin(j - 5, 12), 3)) +
                                        modmordstdet(j, R%, k) * (1 - IHDDist(R%, Amin(j, 17), (k - 4)) - StrokeDist(R%, Amin(j, 17), (k - 4)))
                                    End If
                                    'jrs 2013/12/05 *** end ***

                                End If
                            End If

                            'jrs 2009/10/29
                            'All Years but first: apply Outdoor air polution factor
                            '-------------------------------------------------------
                            If IY > 1 And hlpm2pt5sw > 0 And j > 6 Then 'adults 30+
                                'Respiratory Infections, or Resp. Disease or Card. Vasc.
                                '-------------------------------------------------------
                                If (k = 27 Or k = 28) Or (k = 9 Or k = 10) Or (k = 5 Or k = 6) Then
                                    'jrs 2010/01/12
                                    If k = 27 Or k = 28 Then
                                        'avoid shifting this effect to OCDs
                                        modmordstdet(j, R%, k - 26) = modmordstdet(j, R%, k - 26) + modmordstdet(j, R%, k) * PM2pt5Diff
                                    End If

                                    modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (1 + PM2pt5Diff)
                                End If
                            End If

                            'jrs 2010/01/12 this needs to be done at the end to use total OCDs as input for D, RI and Mal.
                            'jrs 2009/09/14
                            'if doing Resp. Inf. (last of Comm.Dis.)
                            If (k = 27 Or k = 28) Then
                                '-4 is Diarrhea
                                '-2 is Malaria
                                If modmordstdet(j, R%, k - 26) > 0 Then
                                    portion = (modmordstdet(j, R%, k - 4) + modmordstdet(j, R%, k - 2) + modmordstdet(j, R%, k)) / modmordstdet(j, R%, k - 26)
                                    If portion > 0.95 Then
                                        modmordstdet(j, R%, k - 4) = CSng(modmordstdet(j, R%, k - 4) * 0.95 / portion)
                                        modmordstdet(j, R%, k - 2) = CSng(modmordstdet(j, R%, k - 2) * 0.95 / portion)
                                        modmordstdet(j, R%, k) = CSng(modmordstdet(j, R%, k) * 0.95 / portion)
                                    End If

                                    modmordstdet(j, R%, k - 26) = modmordstdet(j, R%, k - 26) - modmordstdet(j, R%, k - 4) -
                                    modmordstdet(j, R%, k - 2) - modmordstdet(j, R%, k)
                                Else
                                    modmordstdet(j, R%, k - 26) = 0
                                    modmordstdet(j, R%, k - 4) = 0
                                    modmordstdet(j, R%, k - 2) = 0
                                    modmordstdet(j, R%, k) = 0
                                End If
                            End If

                            'jrs 2010/02/18
                            'if doing mental health
                            If (k = 29 Or k = 30) And modmordstdet(j, R%, k) > 0 Then
                                'Remove it from Other NCDs
                                If modmordstdet(j, R%, k - 18) >= modmordstdet(j, R%, k) Then
                                    modmordstdet(j, R%, k - 18) = modmordstdet(j, R%, k - 18) - modmordstdet(j, R%, k)
                                Else
                                    modmordstdet(j, R%, k - 18) = 0
                                End If
                            End If

                            'jrs 2009/07/07
                            'Transition switch
                            If hlmorttranssw > 0 Then
                                'jrs 2008/10/09
                                'Transition factors for low income countries
                                If GDPPCIP(R) < 3 And IY > 1 And k <> 21 And k <> 22 And ((k <> 13 And k <> 14) Or hlvehsw = 0) Then
                                    'Face out Normalization and Scaling Factors between $3k and $15k per cap
                                    If GDPPCP(R) > 3 Then
                                        'jrs 2009/07/13
                                        '                            If GDPPCP(R) < TransitionLevel Then
                                        '                                Port = (GDPPCP(R) - 3) / (TransitionLevel - 3)
                                        '                            Else
                                        '                                Port = 1
                                        '                            End If
                                        '                            If NormFactors2(j, R, k) > 1 Then
                                        '                                chg = 1 - NormFactors2(j, R, k)
                                        '                            Else
                                        '                                chg = 0
                                        '                            End If
                                        '                            If Factors2(j, R, k) > 1 Then
                                        '                                chg2 = 1 - Factors2(j, R, k)
                                        '                            Else
                                        '                                chg2 = 0
                                        '                            End If
                                        '                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (NormFactors2(j, R%, k) + chg * Port) * _
                                        '                            (Factors2(j, R, k) + chg2 * Port)

                                        'jrs 2010/04/12 convergence only for subsequent years
                                        'jrs 2009/12/03 upward convergence for normalization factors
                                        If hlgbdconvDown > 0 And NormFactors2(j, R%, k) > 1 And IY > 1 Then
                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * ConvergeOverTime(CSng(NormFactors2(j, R%, k)), 1, hlgbdconvDown)
                                        ElseIf hlgbdconvUp > 0 And NormFactors2(j, R%, k) < 1 And IY > 1 Then 'jrs 2010/03/10
                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * ConvergeOverTime(CSng(NormFactors2(j, R%, k)), 1, hlgbdconvUp)
                                        Else
                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * NormFactors2(j, R%, k)
                                        End If
                                        'jrs 2010/04/09 adding convergence for scaling factors
                                        If hlscaleconvdown > 0 And Factors2(j, R%, k) > 1 And IY > 1 Then
                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * ConvergeOverTime(CSng(Factors2(j, R%, k)), 1, hlscaleconvdown)
                                        Else
                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * Factors2(j, R%, k)
                                        End If

                                    End If

                                    'All Years but first: apply Malnutrition Effect to Communicable Diseases
                                    '-------------------------------------------------------------
                                    If (k < 3 Or (k > 22 And k < 29)) And j < 2 Then 'jrs 2008/01/09 added and j<2, just for children under 5
                                        'jrs 2009/08/20 diarrhea has its own RRs
                                        If k = 23 Or k = 24 Then
                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + MNDiarrDiff)
                                        ElseIf k = 25 Or k = 26 Then 'jrs 2009/09/15
                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + MNMalDiff)
                                        ElseIf k = 27 Or k = 28 Then 'jrs 2009/09/15
                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + MNRespInfDiff)
                                        Else
                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + MNDiff)
                                        End If
                                    End If

                                    'jrs 2021/08/05 start
                                    'All Years: apply Stunting effect to Communicable diseases
                                    '---------------------------------------------------------
                                    If IY > 1 And (k < 3 Or (k > 22 And k < 29)) And j < 2 Then
                                        If k = 23 Or k = 24 Then
                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + STUNTCHLDiff(2))
                                        ElseIf k = 25 Or k = 26 Then 'using Other CDs for Malaria
                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + STUNTCHLDiff(1))
                                        ElseIf k = 27 Or k = 28 Then
                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + STUNTCHLDiff(3))
                                        Else
                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + STUNTCHLDiff(1))
                                        End If
                                    End If
                                    'jrs 2021/08/05 end

                                    'All Years: apply Obesity Effect to Cardiovascular Diseases
                                    '-------------------------------------------------------------
                                    If j > 6 Then
                                        'jrs 2009/09/21
                                        '                                If k = 5 Then
                                        '                                    modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + ObDiffM)
                                        '                                ElseIf k = 6 Then
                                        '                                    modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + ObDiffF)
                                        '                                End If
                                        G = k - 4 '1 for males, 2 for females
                                        If k = 5 Or k = 6 Then
                                            Select Case j
                                                Case 7, 8, 9 '30-44
                                                    modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + BMIDiff(1, G))
                                                Case 10, 11, 12 '45-59
                                                    modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + BMIDiff(2, G))
                                                Case 13, 14 '60-69
                                                    modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + BMIDiff(3, G))
                                                Case 15, 16 '70-79
                                                    modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + BMIDiff(4, G))
                                                Case Else '80+
                                                    modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + BMIDiff(5, G))
                                            End Select
                                        End If
                                    End If

                                    'jrs 2009/02/16
                                    'All years (but first): apply Health Expenditures Factor
                                    '-------------------------------------------------------
                                    If j < 2 Then
                                        modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + HlExpFct)
                                    End If
                                    'jrs 2014/05/27
                                    'All years (but first): apply Health Expenditrues to Health Cost Gap Factor
                                    '--------------------------------------------------------------------------
                                    If IY > 2 Then
                                        modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + HlExpToCostGapFact((k + 1) \ 2))
                                    End If

                                    'jrs 2009/04/09
                                    'All years (but first): apply WATSAN effect on Diarrhea
                                    '-------------------------------------------------------
                                    'jrs 2009/04/30
                                    If hlwatsansw > 0 Then
                                        'jrs 2009/08/11
                                        '                                If k < 3 And modmordstdet2(j, R%, k) > 0 Then 'k=1 and k=2 communicable diseases for males and females
                                        '                                    'Find Diarrhea Mortality
                                        '                                    RegVal = Exp(mortFactorsInf(j, k, 1) + mortFactorsInf(j, k, 2) * Log(modmordstdet2(j, R%, k) * 100)) / 100
                                        '                                    RegVal = RegVal * mortShiftFactorsDiarr2(j, R%, k)
                                        '                                    'WATSANFct holds a portion of Diarrhea to increase or reduce
                                        '                                    'change total communicable disease mortality
                                        '                                    modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) + RegVal * WATSANFct
                                        '                                End If
                                        If (k = 23 Or k = 24) Then
                                            'jrs 2010/01/12
                                            'avoid shifting this effect to OCDs
                                            modmordstdet2(j, R%, k - 22) = modmordstdet2(j, R%, k - 22) + modmordstdet2(j, R%, k) * WATSANFct

                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + WATSANFct)
                                        End If
                                    End If

                                    'jrs 2009/10/28
                                    'All Years but first: apply Solid Fuel factor
                                    '--------------------------------------------
                                    If IY > 1 And hlsolfuelsw > 0 Then
                                        'jrs 2014/01/23
                                        If hlsolfuelsw = 1 Then
                                            'Respiratory Infections for children under 5
                                            '--------------------------------------------
                                            If j < 2 And (k = 27 Or k = 28) Then
                                                'jrs 2010/01/12
                                                'avoid shifting this effect to OCDs
                                                modmordstdet2(j, R%, k - 26) = modmordstdet2(j, R%, k - 26) + modmordstdet2(j, R%, k) * SFRIDiff

                                                modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + SFRIDiff)
                                            End If
                                            'Respiratry Disease for adults over 30
                                            '-------------------------------------
                                            If j > 6 Then
                                                If k = 9 Then
                                                    modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + SFRDMDiff)
                                                ElseIf k = 10 Then
                                                    modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + SFRDFDiff)
                                                End If
                                            End If
                                        Else 'jrs 2014/01/23 Cook Stoves
                                            'jrs 2014/01/23 *** begin ***
                                            'Respiratory Infections
                                            '----------------------
                                            'bbh 2013/12/28
                                            If IY > 1 And (j = 0 Or j = 1) And (k = 27 Or k = 28) Then
                                                'If IY > 1 And j = 1 And (k = 27 Or k = 28) Then
                                                modmordstdet2(j, R%, k - 26) = modmordstdet2(j, R%, k - 26) + modmordstdet2(j, R%, k) * COOKSTRIDiff

                                                modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + COOKSTRIDiff)
                                            End If

                                            'Respiratory Disease for adults over 25
                                            '--------------------------------------
                                            If IY > 1 And j > 5 And (k = 9 Or k = 10) Then
                                                If k = 9 Then
                                                    G = 1
                                                Else
                                                    G = 2
                                                End If
                                                'jrs 2014/01/30 added age and gender to Disease Distribution
                                                modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * COPDDist(R%, Amin(j, 17), (k - 8)) * (1 + COOKSTRDDiff(R, G, 5)) +
                                                modmordstdet2(j, R%, k) * (1 - COPDDist(R%, Amin(j, 17), (k - 8)))
                                            End If

                                            'Lung Cancer for adults over 25
                                            '--------------------------------------
                                            If k = 3 Then
                                                G = 1
                                            Else
                                                G = 2
                                            End If
                                            If IY > 1 And j > 5 And (k = 3 Or k = 4) And COOKSTLCDiff(R, G, 5) <> 0 Then
                                                'jrs 2014/01/30 added age and gender to Disease Distribution
                                                modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * LungCancerDist(R%, Amin(j, 17), (k - 2)) * (1 + COOKSTLCDiff(R, G, 5)) +
                                                modmordstdet2(j, R%, k) * (1 - LungCancerDist(R%, Amin(j, 17), (k - 2)))
                                            End If

                                            'IHD and Stroke for adults over 25
                                            '--------------------------------------
                                            If k = 5 Then
                                                G = 1
                                            Else
                                                G = 2
                                            End If
                                            'jrs 2014/02/17 added an extra year for lag
                                            'jrs 2014/01/31 added age dimension to Diff Adj. factor
                                            If IY > 1 And j > 5 And (k = 5 Or k = 6) And (COOKSTSTDiff(R, G, Amin(AMAX(j - 5, 1), 12), 3) <> 0 Or COOKSTIHDDiff(R, G, Amin(AMAX(j - 5, 1), 12), 3) <> 0) Then
                                                'jrs 2014/01/30 added age and gender to Disease Distribution
                                                modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * IHDDist(R%, Amin(j, 17), (k - 4)) * (1 + COOKSTIHDDiff(R, G, Amin(j - 5, 12), 3)) +
                                                modmordstdet2(j, R%, k) * StrokeDist(R%, Amin(j, 17), (k - 4)) * (1 + COOKSTSTDiff(R, G, Amin(j - 5, 12), 3)) +
                                                modmordstdet2(j, R%, k) * (1 - IHDDist(R%, Amin(j, 17), (k - 4)) - StrokeDist(R%, Amin(j, 17), (k - 4)))
                                            End If
                                            'jrs 2014/01/23 *** end ***
                                        End If
                                    End If

                                    'jrs 2009/10/29
                                    'All Years but first: apply Outdoor air polution factor
                                    '-------------------------------------------------------
                                    If IY > 1 And hlpm2pt5sw > 0 And j > 6 Then 'adults 30+
                                        'Respiratory Infections, or Resp. Disease or Card. Vasc.
                                        '-------------------------------------------------------
                                        If (k = 27 Or k = 28) Or (k = 9 Or k = 10) Or (k = 5 Or k = 6) Then
                                            If k = 27 Or k = 28 Then
                                                'jrs 2010/01/12
                                                'avoid shifting this effect to OCDs
                                                modmordstdet2(j, R%, k - 26) = modmordstdet2(j, R%, k - 26) + modmordstdet2(j, R%, k) * PM2pt5Diff
                                            End If

                                            modmordstdet2(j, R%, k) = modmordstdet2(j, R%, k) * (1 + PM2pt5Diff)
                                        End If
                                    End If

                                    'jrs 2009/09/14
                                    'if doing Resp. Inf. (last of Comm.Dis.)
                                    If (k = 27 Or k = 28) Then
                                        '-4 is Diarrhea
                                        '-2 is Malaria
                                        If modmordstdet2(j, R%, k - 26) > 0 Then
                                            portion = (modmordstdet2(j, R%, k - 4) + modmordstdet2(j, R%, k - 2) + modmordstdet2(j, R%, k)) / modmordstdet2(j, R%, k - 26)
                                            If portion > 0.95 Then
                                                modmordstdet2(j, R%, k - 4) = CSng(modmordstdet2(j, R%, k - 4) * 0.95 / portion)
                                                modmordstdet2(j, R%, k - 2) = CSng(modmordstdet2(j, R%, k - 2) * 0.95 / portion)
                                                modmordstdet2(j, R%, k) = CSng(modmordstdet2(j, R%, k) * 0.95 / portion)
                                            End If

                                            modmordstdet2(j, R%, k - 26) = modmordstdet2(j, R%, k - 26) - modmordstdet2(j, R%, k - 4) -
                                            modmordstdet2(j, R%, k - 2) - modmordstdet2(j, R%, k)
                                        Else
                                            modmordstdet2(j, R%, k - 26) = 0
                                            modmordstdet2(j, R%, k - 4) = 0
                                            modmordstdet2(j, R%, k - 2) = 0
                                            modmordstdet2(j, R%, k) = 0
                                        End If
                                    End If

                                    'jrs 2010/02/18
                                    'if doing mental health
                                    If (k = 29 Or k = 30) And modmordstdet2(j, R%, k) > 0 Then
                                        'Remove it from Other NCDs
                                        If modmordstdet2(j, R%, k - 18) >= modmordstdet2(j, R%, k) Then
                                            modmordstdet2(j, R%, k - 18) = modmordstdet2(j, R%, k - 18) - modmordstdet2(j, R%, k)
                                        Else
                                            modmordstdet2(j, R%, k - 18) = 0
                                        End If
                                    End If

                                    '********************************************************************
                                    'Transition to high income model
                                    '*******************************************************************
                                    'jrs 2009/07/02 turn off
                                    'If False Then
                                    If GDPPCP(R) > 3 Then

                                        'jrs 2009/12/04
                                        modmordstdet(j, R%, k) = Amin(modmordstdet(j, R%, k), limit)
                                        modmordstdet2(j, R%, k) = Amin(modmordstdet2(j, R%, k), limit)

                                        'If modmordstdet(j, R%, k) < modmordstdet2(j, R%, k) And GRPS(1, R) = "Bangladesh" Then
                                        ''And GRPS(27, (k + 1) / 2) = "MaligNeoPl" And k Mod 2 <> 0 And j = 1
                                        '    Open FdrvRun$ & "ifslog2.txt" For Append As #400
                                        '    Print #400, GRPS(1, R); j; FirstYear + IY - 1;
                                        '    If k Mod 2 = 0 Then
                                        '        Print #400, GRPS(27, k / 2); " Female";
                                        '    Else
                                        '        Print #400, GRPS(27, (k + 1) / 2); " Male";
                                        '    End If
                                        '    Print #400, modmordstdet(j, R%, k); modmordstdet2(j, R%, k); NormFactors2(j, R, k); Factors2(j, R, k); chg; chg2; Port; MNDiff; ObDiffM; ObDiffF
                                        '    Close #400
                                        'End If

                                        'jrs 2009/07/02 use moving average for transition
                                        '                        If GDPPCP(R) > TransitionLevel Then
                                        '                            modmordstdet(j, R%, k) = modmordstdet2(j, R%, k)
                                        '                        Else
                                        '                            modmordstdet(j, R%, k) = modmordstdet(j, R%, k) * (TransitionLevel - GDPPCP(R)) / (TransitionLevel - 3) + _
                                        '                            modmordstdet2(j, R%, k) * (GDPPCP(R) - 3) / (TransitionLevel - 3)
                                        '                        End If
                                        'jrs 2009/08/04
                                        If GDPPCP(R) > TransitionLevel Then
                                            DiffMorts = modmordstdet2(j, R%, k) - modmordstdet(j, R%, k)
                                        Else
                                            'interpolation of target mortality
                                            DiffMorts = modmordstdet(j, R%, k) * (TransitionLevel - GDPPCP(R)) / (TransitionLevel - 3) +
                                            modmordstdet2(j, R%, k) * (GDPPCP(R) - 3) / (TransitionLevel - 3)
                                            'removing higher mortality (for low income countries)
                                            DiffMorts = DiffMorts - modmordstdet(j, R%, k)
                                        End If

                                        'DiffMorts = modmordstdet2(j, R%, k) - modmordstdet(j, R%, k)
                                        Inc = CSng(0.2 * DiffMorts + 0.8 * TransitionInc(j, R%, k))
                                        modmordstdet(j, R%, k) = modmordstdet(j, R%, k) + AMAX(Inc, DiffMorts)
                                        TransitionInc(j, R%, k) = CSng(AMAX(Inc, DiffMorts))
                                    End If
                                End If 'Transition factors for low income countries
                            End If 'jrs 2009/07/07 transition switch

                            'jrs 2009/12/04
                            modmordstdet(j, R%, k) = Amin(modmordstdet(j, R%, k), limit)
                        End If 'don't apply when k=21 and k=22, AIDS
                    Next k
                Next j
            Next R%

            'jrs 2019/11/19 ifsPopWorkDB.Flush()
            If IY > 1 Then
                If hlmlnsw > 0 Then
                    'jrs 2024/01/24
                    If MultiThreading Then
                        ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(MALNCHPPD, NR, IY + FirstYear - 1, "MALNCHP"))
                    Else
                        ifsPopWorkDB.BulkInsertHLProxDrivers(MALNCHPPD, NR, IY + FirstYear - 1, "MALNCHP")
                    End If
                End If
                If hlobsw > 0 Then
                    'jrs 2024/01/24
                    If MultiThreading Then
                        ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(HLBMIPD, NR, IY + FirstYear - 1, "HLBMI", 2))
                        ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDriversT(HLBMIPD, popgdr, NR, IY + FirstYear - 1, "HLBMI"))
                    Else
                        ifsPopWorkDB.BulkInsertHLProxDrivers(HLBMIPD, NR, IY + FirstYear - 1, "HLBMI", 2)
                        ifsPopWorkDB.BulkInsertHLProxDriversT(HLBMIPD, popgdr, NR, IY + FirstYear - 1, "HLBMI")
                    End If
                End If
                If hlsolfuelsw = 1 Then
                    'jrs 2024/01/24
                    If MultiThreading Then
                        ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(ENSOLFUELPD, NR, IY + FirstYear - 1, "ENSOLFUEL"))
                    Else
                        ifsPopWorkDB.BulkInsertHLProxDrivers(ENSOLFUELPD, NR, IY + FirstYear - 1, "ENSOLFUEL")
                    End If
                End If
                'ifsPopWorkDB.BulkInsertHLProxDrivers(1, NR, IY + FirstYear - 1, "ensfvent")
                If hlpm2pt5sw > 0 Then
                    'jrs 2024/01/24
                    If MultiThreading Then
                        ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(SENVPM2PT5DDF, NR, IY + FirstYear - 1, "ENVPM2PT5"))
                    Else
                        ifsPopWorkDB.BulkInsertHLProxDrivers(SENVPM2PT5DDF, NR, IY + FirstYear - 1, "ENVPM2PT5")
                    End If
                End If
                'jrs 2021/08/05
                If hlstuntchlsw > 0 Then
                    'jrs 2024/01/24
                    If MultiThreading Then
                        ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(SHLSTUNTCHLDDF, NR, IY + FirstYear - 1, "HLSTUNTCHL"))
                    Else
                        ifsPopWorkDB.BulkInsertHLProxDrivers(SHLSTUNTCHLDDF, NR, IY + FirstYear - 1, "HLSTUNTCHL")
                    End If
                End If
            End If

        End Sub

        'jrs 2015/12/04 added SAM parameter
        'jrs 2008/01/04
        'PAF - Population Attributable Fraction, used for Malnutrition
        Public Function PAF(Pm2 As Single, SAM As Single, rr() As Single) As Single
            'Pm2 is the proportion of the population that is underweight (<2STD)
            'SAM is the portion of the population that suffers from sever malnutrition

            PAF = 0
            'check validity of Pm2
            If Pm2 > 1 Or Pm2 < 0 Then
                Debug.Print("Invalid Malnutrition proportion, value has to be between 0 and 1, PAF")
                Exit Function
            End If

            'z is the normalized value corresponding to <2STD
            Dim z As Single
            'mti Oct 01, 2009
            'z = NormalInverse(Pm2)
            z = CalcNormalInverse(Pm2)
            'mti Oct 01, 2009


            'Proportion array for 4 different levels of malnutrition
            Dim P(4) As Single
            '1) > -1 STD - base line
            '2) -1 to -2 STD - mild
            '3) -2 to -3 STD - moderate
            '4) < -3 STD - severe

            'mti Oct 01, 2009 - start
            'P(4) = NormalCDF(z - 1)
            'jrs 2015/12/04
            If samrrsw = 0 Then
                P(4) = CSng(CalcNormalCDF(z - 1))
            Else
                P(4) = SAM
            End If

            P(3) = Pm2 - P(4)

            P(2) = CSng(CalcNormalCDF(z + 1) - Pm2)
            'P(2) = NormalCDF(z + 1) - Pm2
            P(1) = 1 - P(2) - P(3) - P(4)

            'mti Oct 01, 2009 -end

            'International Reference (Theoretical Minimum)
            Dim IntRef(4) As Single
            IntRef(1) = 0.8414
            IntRef(2) = 0.1359
            IntRef(3) = 0.0214
            IntRef(4) = 0.0013

            Dim i As Integer
            'Regional exposure
            Dim RegVal As Single
            RegVal = 0
            'International exposure
            Dim IntRefVal As Single

            For i = 1 To 4
                RegVal = RegVal + P(i) * rr(i)
                'no need to compute because it's constant
                IntRefVal = IntRefVal + IntRef(i) * rr(1)
            Next i
            'constant value
            'only for OCD IntRefVal = 1.223226

            'PAF calculation
            PAF = (RegVal - IntRefVal) / RegVal
        End Function

        'jrs 2010/01/05
        Public Function PAFBMI(avgBMI As Single, sAge As String, sGender As String, rrCV() As Single) As Single
            'jrs 2009/12/02
            If avgBMI < 21 Then
                PAFBMI = 0
                Exit Function
            End If

            'Assume a Normal distribution with this Avg BMI
            '--------------------------------------------------
            'gvdb 2024/04/05
            'unused variable
            Dim i, P, BMI As Single ', BMI1 As Single, BMI2 As Single
            Dim StdDev As Single
            StdDev = CSng(0.1 * avgBMI)
            'Differential to compute area under the curve (p(x)*dx = area of a single rectangle)
            Dim DX As Single
            DX = 6 * StdDev / 100
            Dim Pi As Double
            Pi = 3.14159265358979
            'Relative Risk
            Dim rr As Single
            'International Reference
            'computed using this same procedure with avg bmi of 21
            'thus a bmi of 21 should return a PAF of 0
            Dim IntRef As Single
            Select Case sAge
                Case "30-44"
                    'rr = 1.14
                    rr = rrCV(1)
                    'jrs 2009/11/23 IntRef = 1.1965
                    IntRef = 1.328882
                Case "45-59"
                    'rr = 1.09
                    rr = rrCV(2)
                    'jrs 2009/11/23 IntRef = 1.114
                    IntRef = 1.186402
                Case "60-69"
                    'rr = 1.08
                    rr = rrCV(3)
                    'jrs 2009/11/23 IntRef = 1.0995
                    IntRef = 1.161536
                Case "70-79"
                    'rr = 1.05
                    rr = rrCV(4)
                    'jrs 2009/11/23 IntRef = 1.0594
                    IntRef = 1.093189
                Case "80+"
                    'rr = 1.02
                    rr = rrCV(5)
                    'jrs 2009/11/23 IntRef = 1.0239
                    IntRef = 1.033211
            End Select
            'Regional Value
            Dim RegVal As Single
            RegVal = 0
            'First BMI considered in the distribution
            'BMI1 = avgBMI + DX * (1 - 50)
            'Last BMI considered in the distribution
            'BMI2 = avgBMI + DX * (100 - 50)

            'Loop through full distribution
            For i = 1 To 100
                'BMI level
                BMI = avgBMI + DX * (i - 50)
                'z = (BMI - avgBMI) / StdDev
                'Population portion
                '        P = CalcNormalCDF(z)
                '        z = (BMI - DX - avgBMI) / StdDev
                '        P = (P - CalcNormalCDF(z)) / DX
                'jrs 2010/02/15 using PDF (Probability density function)
                'http://en.wikipedia.org/wiki/Normal_distribution
                P = CSng(1 / (2 * Pi * (StdDev ^ 2)) ^ 0.5 * Exp(-((BMI - avgBMI) ^ 2) / (2 * (StdDev ^ 2))))

                'BMI of 21 is the International reference average
                If BMI > 21 Then
                    'Accumulate Regional Value
                    RegVal = CSng(RegVal + (rr ^ (BMI - 21)) * P * DX)
                Else
                    'Accumulate Regional Value
                    RegVal = RegVal + P * DX
                End If
            Next i

            'PAF calculation
            PAFBMI = (RegVal - IntRef) / RegVal
        End Function

        'jrs 2015/03/18
        ''jrs 2009/03/27
        ''PAF - Population Attributable Fraction, used for Water and Sanitation
        'Public Function PAFWATSAN(ByVal WatNone As Single, ByVal WatImpr As Single, ByVal WatHC As Single, _
        'ByVal SanitNone As Single, ByVal SanitImpr As Single, ByVal SanitHC As Single, ByRef rrD As Variant) As Single
        '    Dim CatII As Single, CatVI As Single, CatVb As Single, CatVa As Single, CatIV As Single
        '    Dim WAR As Single
        '    'Category II: minimum of (share of population with HC for water supply, share of population with HC for sanitation)
        '    CatII = Amin(WatHC, SanitHC)
        '    'Category VI: minimum of (share without improved or HC for water supply, share without improved or HC for sanitation)
        '    CatVI = Amin(WatNone, SanitNone)
        '    'Category Va: minimum of (share with basic or HC sanitation, remainder of those without improved or HC for water supply that are not already in category VI)
        '    CatVa = Amin(SanitImpr + SanitHC, WatNone - CatVI)
        '    'Category Vb: minimum of (share with improved or HC water supply, remainder of those without improved or HC for sanitation that are not already in category VI)
        '    CatVb = Amin(WatImpr + WatHC, SanitNone - CatVI)
        '    'Category IV: minimum of (share with improved or HC water supply not in category Vb or II, share with basic or HC sanitation not in category Va or II)
        '    CatIV = Amin(WatImpr + WatHC - CatVb - CatII, SanitImpr + SanitHC - CatVa - CatII)
        '    'Weighted Average Risk
        '    'WAR = (2.5 * CatII + 11 * CatVI + 6.9 * CatVa + 8.7 * CatVb + 6.9 * CatIV) / 100
        '    WAR = (rrD(1) * CatII + rrD(5) * CatVI + rrD(3) * CatVa + rrD(4) * CatVb + rrD(2) * CatIV) / 100
        '    'PAF
        '    PAFWATSAN = 1 - (1 / WAR)
        'End Function
        Public Function PAFWATSAN(ByVal WATSAFE1 As Single, ByVal WATSAFE2 As Single, ByVal WATSAFE3 As Single, ByVal SANITATION3 As Single,
        ByRef WATFILT(,) As Single, ByVal SanitationRR As Single, ByRef WatSafeRR() As Single, ByVal R As Integer) As Single
            Dim WATSAFEFORPAF(4) As Single
            WATSAFEFORPAF(1) = WATSAFE1 * (1 - WATFILT(R, 1) / 100)
            WATSAFEFORPAF(2) = WATSAFE2 * (1 - WATFILT(R, 2) / 100)
            WATSAFEFORPAF(3) = WATSAFE3 * (1 - WATFILT(R, 3) / 100)
            WATSAFEFORPAF(4) = 100 - (WATSAFEFORPAF(1) + WATSAFEFORPAF(2) + WATSAFEFORPAF(3))

            Dim SANPAF As Single
            SANPAF = 1 - 1 / (SANITATION3 / 100 + SanitationRR * (1 - SANITATION3 / 100))

            Dim WATPAF As Single
            WATPAF = 1 - 1 / (WATSAFEFORPAF(4) / 100 + WatSafeRR(1) * WATSAFEFORPAF(1) / 100 + WatSafeRR(2) * WATSAFEFORPAF(2) / 100 + WatSafeRR(3) * WATSAFEFORPAF(3) / 100)

            PAFWATSAN = 1 - (1 - SANPAF) * (1 - WATPAF)
        End Function

        'jrs 2013/12/05
        Public Function PAFCOOKST(rr As Dictionary(Of String, Single), sGender As String, sAge As String, RegIdx As Integer) As Single
            'Weigthed Average Risk
            Dim WAR As Single, i As Integer, sKey As String
            WAR = 0
            'For each level of cook stove
            For i = 1 To 3
                sKey = CStr(i)
                If sGender > "" Then
                    sKey = sKey & sGender
                End If
                sKey = sKey & sAge
                'SCOOKSTOVES is saved in portions of a unit, not in millions, to avoid having to save HHTOT, which is computed very early
                'jrs 2023/12/05 WAR = WAR + rr.Item(sKey) * SCOOKSTOVES(RegIdx, i)
                WAR = WAR + rr.Item(sKey) * SCOOKSTOVESPHH(RegIdx, i)
            Next
            PAFCOOKST = 1 - (1 / WAR)
        End Function

        'jrs 2013/12/05
        Public Function PAFCOOKSTDD(rr As Dictionary(Of String, Single), sGender As String, sAge As String, RegIdx As Integer, COOKSTOVESDD(,) As Single) As Single
            'Weigthed Average Risk
            Dim WAR As Single, i As Integer, sKey As String
            WAR = 0
            'For each level of cook stove
            For i = 1 To 3
                sKey = CStr(i)
                If sGender > "" Then
                    sKey = sKey & sGender
                End If
                sKey = sKey & sAge
                WAR = WAR + rr.Item(sKey) * COOKSTOVESDD(RegIdx, i)
            Next
            PAFCOOKSTDD = 1 - (1 / WAR)
        End Function

        'jrs 2021/08/05
        Public Function PAFSTUNTCHL(STUNTCHL As Single, rrCV() As Single) As Single

            'International Reference
            'computed using this same procedure with STUNTCHL of 0.0025 (Iceland in base year, 2017, version 7.71
            'thus a STUNTCHL of 0.0025 should return a PAF of 0
            Dim IntRef As Single
            IntRef = CSng(0.0025 * rrCV(2) + (1 - 0.0025) * rrCV(1))
            'Regional Value
            Dim RegVal As Single
            RegVal = STUNTCHL * rrCV(2) + (1 - STUNTCHL) * rrCV(1)

            'PAF calculation
            PAFSTUNTCHL = (RegVal - IntRef) / RegVal
        End Function

        'jrs 2012/09/13
        'Mortality rate extrapolation using logistic function and assuming b = 0.11
        'a moves the J-curve to the right
        'b changes how fast the J-curve rises
        'p(mort) = a * exp(b*age) /(1 + a * exp(b*age) )
        'Designed by Randall Khun, implemented by José Solórzano
        Private Sub ifsExtendedLongevityMortality(ByRef modmordstdet(,,) As Double, ByVal Ntypes As Integer, context As IForecastContext)
            'modmordstdet is the detailed mortality distribution with 3 indices: age, country, type
            'jrs 2014/08/04
            '    Dim BNCD As Single, BCD As Single, BInj As Single
            '    Dim aNCD As Single, aCD As Single, aInj As Single
            '    Dim mortNCD As Single, mortCD As Single, mortInj As Single

            '    'jrs 2012/10/18
            '    'Target Alphas and Betas (loaded from parameters)
            '    Dim BNCDT As Single, BCDT As Single, BInjT As Single
            '    Dim aNCDT As Single, aCDT As Single, aInjT As Single
            '    'Converged Alphas and Betas (estimated by Randall)
            '    Dim BNCDC As Single, BCDC As Single, BInjC As Single
            '    Dim aNCDC As Single, aCDC As Single, aInjC As Single
            'jrs 2014/06/12
            Dim HlExpToCostGapFact() As Single
            ReDim HlExpToCostGapFact(CInt(Ntypes / 2))
            Dim k As Integer

            'jrs 2013/01/08
            If IY = 1 Then
                ReDim modmordstdetT(NCohorts, NR%, Ntypes)
                'jrs 2014/06/20
                ReDim modmordstdetTI(NCohorts, NR%, Ntypes)
                'jrs 2014/08/04
                ReDim ageSt(Ntypes)
                For k = 1 To CInt(Ntypes / 2)
                    If hllongvtystartage(k) > 0 Then
                        ageSt(k) = CInt(hllongvtystartage(k) / 5 + 1)
                    Else
                        ageSt(k) = 0
                    End If
                Next k

                '        'jrs 2013/02/05
                '        If hllongvtystartage(1) > 0 Then
                '            ageStCD = hllongvtystartage(1) / 5 + 1
                '        Else
                '            ageStCD = 0
                '        End If
                '        If hllongvtystartage(2) > 0 Then
                '            ageStNCD = hllongvtystartage(2) / 5 + 1
                '        Else
                '            ageStNCD = 0
                '        End If
                '        If hllongvtystartage(3) > 0 Then
                '            ageStInj = hllongvtystartage(3) / 5 + 1
                '        Else
                '            ageStInj = 0
                '        End If
            End If

            'jrs 2012/10/09 B = 0.11

            'For each country/gender
            'gvdb 2024/04/05
            'unused variable
            Dim R As Integer, G As Integer, j As Integer ' z As Integer,
            'jrs 2014/08/04
            Dim Betas(,,), alphas(,,) As Single
            ReDim Betas(NR, Ntypes, 2), alphas(NR, Ntypes, 2)
            'jrs 2014/08/20
            If IY = 1 Then
                ReDim BetasT(NR, Ntypes, 2), alphasT(NR, Ntypes, 2), alphaFxs(Ntypes, 2)
                LoadAlphaFxs(alphaFxs)
            End If
            ReDim alphaShifts(Ntypes, 2)

            Dim JapanIdx As Integer, JapanVal As Single
            JapanIdx = context.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("Japan")).Key
            If JapanIdx = -1 Then
                JapanIdx = 0
            End If

            For R = 1 To NR
                For G = 1 To 2
                    'jrs 2014/08/04
                    For k = 1 To CInt(Ntypes / 2)
                        'jrs 2014/08/20
                        '                BetasT(R, k, G) = hllongvtybeta(R, k, G)
                        '                If BetasT(R, k, G) = 0.11 Then 'default value
                        '                    BetasT(R, k, G) = hllongvtybeta(R, k, 3)
                        '                End If
                        '                alphasT(R, k, G) = hllongvtyalpha(R, k, G)
                        '                If alphasT(R, k, G) = -1 Then
                        '                    alphasT(R, k, G) = hllongvtyalpha(R, k, 3)
                        '                End If
                        '                If alphasT(R, k, G) > -1 Then
                        '                    alphasT(R, k, G) = alphasT(R, k, G) / 1000000
                        '                End If
                        'If GRPS(1, R) = "Uzbekistan" And k = 3 And G = 2 Then Stop
                        Betas(R, k, G) = 0.11
                        alphas(R, k, G) = CSng(modmordstdet(20, R, (k - 1) * 2 + G) / 1000 / (1 - modmordstdet(20, R, (k - 1) * 2 + G) / 1000) / Exp(Betas(R, k, G) * 95))
                        'jrs 2014/09/24
                        'ratio will be used to find target alpha for Japan, all other other countries will use a function on GDPPCP to find their alphas
                        'jrs 2014/08/20 *** begin ***
                        If hllongvtyalpharatio(k, G) > 0 Then
                            If R <> JapanIdx Then
                                'Call respective function
                                XYTABL(CStr(alphaFxs(k, G)), GDPPCP(R), alphasT(R, k, G), context)
                                'Find shift factor
                                If R = 1 Then
                                    If IY = 1 Then
                                        Betas(JapanIdx, k, G) = 0.11
                                        alphas(JapanIdx, k, G) = CSng(modmordstdet(20, JapanIdx, (k - 1) * 2 + G) / 1000 / (1 - modmordstdet(20, JapanIdx, (k - 1) * 2 + G) / 1000) / Exp(Betas(JapanIdx, k, G) * 95))
                                        alphasT(JapanIdx, k, G) = alphas(JapanIdx, k, G) * hllongvtyalpharatio(k, G)
                                    End If
                                    XYTABL(CStr(alphaFxs(k, G)), GDPPCP(JapanIdx), JapanVal, context)
                                    alphaShifts(k, G) = alphasT(JapanIdx, k, G) - JapanVal
                                End If
                                'Add shift factor
                                alphasT(R, k, G) = alphasT(R, k, G) + alphaShifts(k, G)
                            End If
                        ElseIf hllongvtyalpharatio(k, 3) > 0 Then
                            If R <> JapanIdx Then
                                'Call respective function
                                XYTABL(CStr(alphaFxs(k, G)), GDPPCP(R), alphasT(R, k, G), context)
                                'Find shift factor
                                If R = 1 Then
                                    If IY = 1 Then
                                        Betas(JapanIdx, k, G) = 0.11
                                        alphas(JapanIdx, k, G) = CSng(modmordstdet(20, JapanIdx, (k - 1) * 2 + G) / 1000 / (1 - modmordstdet(20, JapanIdx, (k - 1) * 2 + G) / 1000) / Exp(Betas(JapanIdx, k, G) * 95))
                                        alphasT(JapanIdx, k, G) = alphas(JapanIdx, k, G) * hllongvtyalpharatio(k, 3)
                                    End If
                                    XYTABL(CStr(alphaFxs(k, G)), GDPPCP(JapanIdx), JapanVal, context)
                                    alphaShifts(k, G) = alphasT(JapanIdx, k, G) - JapanVal
                                End If
                                'Add shift factor
                                alphasT(R, k, G) = alphasT(R, k, G) + alphaShifts(k, G)
                            End If
                        Else
                            alphasT(R, k, G) = hllongvtyalpha(R, k, G)
                            If alphasT(R, k, G) = -1 Then
                                alphasT(R, k, G) = hllongvtyalpha(R, k, 3)
                            End If
                            If alphasT(R, k, G) > -1 Then
                                alphasT(R, k, G) = alphasT(R, k, G) / 1000000
                            End If
                        End If
                        If hllongvtybetaratio(k, G) > 0 Then
                            If IY = 1 Then
                                BetasT(R, k, G) = Betas(R, k, G) * hllongvtybetaratio(k, G)
                            End If
                        ElseIf hllongvtybetaratio(k, 3) > 0 Then
                            If IY = 1 Then
                                BetasT(R, k, G) = Betas(R, k, G) * hllongvtybetaratio(k, 3)
                            End If
                        Else
                            BetasT(R, k, G) = hllongvtybeta(R, k, G)
                            If BetasT(R, k, G) = 0.11 Then 'default value
                                BetasT(R, k, G) = hllongvtybeta(R, k, 3)
                            End If
                        End If
                        'jrs 2014/08/20 *** begin ***
                        'If GRPS(1, R) = "Japan" And IY = 1 And G = 2 And k = 15 Then Stop
                        'for each older age
                        For j = 21 To NCohorts
                            If modmordstdet(20, R, (k - 1) * 2 + G) > 0 Then
                                'jrs 2014/08/08
                                If k = 15 Then
                                    modmordstdet(j, R, (k - 1) * 2 + G) = modmordstdet(20, R, (k - 1) * 2 + G)
                                Else
                                    modmordstdet(j, R, (k - 1) * 2 + G) = CSng(1000 *
                                    alphas(R, k, G) * Exp(Betas(R, k, G) * (j - 1) * 5) / (1 + alphas(R, k, G) * Exp(Betas(R, k, G) * (j - 1) * 5)))
                                End If
                            Else 'jrs 2014/08/08
                                modmordstdet(j, R, (k - 1) * 2 + G) = 0
                            End If
                        Next j

                        If IY = 1 And alphasT(R, k, G) > -1 And BetasT(R, k, G) > -1 Then
                            For j = ageSt(k) To NCohorts
                                'If GRPS(1, R) = "Japan" And IY = 1 And G = 2 And k = 14 And j = 20 Then Stop
                                'Compute Target mortality
                                If modmordstdet(20, R, (k - 1) * 2 + G) > 0 Then
                                    modmordstdetTI(j, R, (k - 1) * 2 + G) = CSng(1000 *
                                    alphasT(R, k, G) * Exp(BetasT(R, k, G) * (j - 1) * 5) / (1 + alphasT(R, k, G) * Exp(BetasT(R, k, G) * (j - 1) * 5)))
                                End If
                            Next j
                        End If
                    Next k

                    'jrs 2014/08/04
                    'jrs 2012/10/09
                    '            BCDT = hllongvtybeta(R, 1, G)
                    '            If BCDT = 0.11 Then 'default value
                    '                BCDT = hllongvtybeta(R, 1, 3)
                    '            End If
                    '            BNCDT = hllongvtybeta(R, 2, G)
                    '            If BNCDT = 0.11 Then 'default value
                    '                BNCDT = hllongvtybeta(R, 2, 3)
                    '            End If
                    '            BInjT = hllongvtybeta(R, 3, G)
                    '            If BInjT = 0.11 Then 'default value
                    '                BInjT = hllongvtybeta(R, 3, 3)
                    '            End If

                    '            aCDT = hllongvtyalpha(R, 1, G)
                    '            If aCDT = -1 Then
                    '                aCDT = hllongvtyalpha(R, 1, 3)
                    '            End If
                    '            aNCDT = hllongvtyalpha(R, 2, G)
                    '            If aNCDT = -1 Then
                    '                aNCDT = hllongvtyalpha(R, 2, 3)
                    '            End If
                    '            aInjT = hllongvtyalpha(R, 3, G)
                    '            If aInjT = -1 Then
                    '                aInjT = hllongvtyalpha(R, 3, 3)
                    '            End If

                    '            If aCDT > -1 Then
                    '                aCDT = aCDT / 1000000
                    '            End If
                    '            If aNCDT > -1 Then
                    '                aNCDT = aNCDT / 1000000
                    '            End If
                    '            If aInjT > -1 Then
                    '                aInjT = aInjT / 1000000
                    '            End If

                    '            'Non Comm Dis
                    '            '============
                    '            mortNCD = 0
                    '            For z = 2 To 10 Step 2
                    '                mortNCD = mortNCD + modmordstdet(20, R, z + G)
                    '            Next z
                    '            'Diabetes
                    '            mortNCD = mortNCD + modmordstdet(20, R, 18 + G)
                    '            'Mental Health
                    '            mortNCD = mortNCD + modmordstdet(20, R, 28 + G)
                    '            mortNCD = mortNCD / 1000
                    '
                    '            BNCD = 0.11
                    '            aNCD = mortNCD / (1 - mortNCD) / Exp(BNCD * 95)

                    '            'jrs 2013/01/08
                    ''            If aNCDT > -1 Then
                    ''                'Do convergence of Initial Alphas and Betas to Target
                    ''                If hllongvtyalphaconv(R, 2) + 1 > IY Then
                    ''                    aNCDC = ConvergeOverTime(hllongvtyalphaI(R, 2, G), aNCDT, hllongvtyalphaconv(R, 2) + 1)
                    ''                Else
                    ''                    aNCDC = aNCDT
                    ''                End If
                    ''                If hllongvtybetaconv(R, 2) + 1 > IY Then
                    ''                    BNCDC = ConvergeOverTime(hllongvtybetaI(R, 2, G), BNCDT, hllongvtybetaconv(R, 2) + 1)
                    ''                Else
                    ''                    BNCDC = BNCDT
                    ''                End If
                    ''            End If

                    'Other Comm Dis
                    '            mortCD = modmordstdet(20, R, G) + modmordstdet(20, R, 20 + G) + _
                    '            modmordstdet(20, R, 22 + G) + modmordstdet(20, R, 24 + G) + modmordstdet(20, R, 26 + G)
                    '            mortCD = mortCD / 1000
                    '
                    '            BCD = 0.11
                    '            aCD = mortCD / (1 - mortCD) / Exp(BCD * 95)

                    '            'jrs 2013/01/08
                    ''            If aCDT > -1 Then
                    ''                'Do convergence of Initial Alphas and Betas to Target
                    ''                If hllongvtyalphaconv(R, 1) + 1 > IY Then
                    ''                    aCDC = ConvergeOverTime(hllongvtyalphaI(R, 1, G), aCDT, hllongvtyalphaconv(R, 1) + 1)
                    ''                Else
                    ''                    aCDC = aCDT
                    ''                End If
                    ''                If hllongvtybetaconv(R, 1) + 1 > IY Then
                    ''                    BCDC = ConvergeOverTime(hllongvtybetaI(R, 1, G), BCDT, hllongvtybetaconv(R, 1) + 1)
                    ''                Else
                    ''                    BCDC = BCDT
                    ''                End If
                    ''            End If

                    'Injuries
                    '            mortInj = 0
                    '            For z = 12 To 17 Step 2
                    '                mortInj = mortInj + modmordstdet(20, R, z + G)
                    '            Next z
                    '            mortInj = mortInj / 1000
                    '
                    '            BInj = 0.11
                    '            aInj = mortInj / (1 - mortInj) / Exp(BInj * 95)

                    '            'jrs 2013/01/08
                    ''            If aInjT > -1 Then
                    ''                'Do convergence of Initial Alphas and Betas to Target
                    ''                If hllongvtyalphaconv(R, 3) + 1 > IY Then
                    ''                    aInjC = ConvergeOverTime(hllongvtyalphaI(R, 3, G), aInjT, hllongvtyalphaconv(R, 3) + 1)
                    ''                Else
                    ''                    aInjC = aInjT
                    ''                End If
                    ''                If hllongvtybetaconv(R, 3) + 1 > IY Then
                    ''                    BInjC = ConvergeOverTime(hllongvtybetaI(R, 3, G), BInjT, hllongvtybetaconv(R, 3) + 1)
                    ''                Else
                    ''                    BInjC = BInjT
                    ''                End If
                    ''            End If

                    '            'for each older age
                    '            For j = 21 To NCohorts
                    '                'Non Comm Dis
                    '                If mortNCD > 0 Then
                    '                    For z = 2 To 10 Step 2
                    '                        modmordstdet(j, R, z + G) = modmordstdet(20, R, z + G) / mortNCD * aNCD * Exp(BNCD * (j - 1) * 5) / (1 + aNCD * Exp(BNCD * (j - 1) * 5))
                    '                    Next z
                    '                    'Diabetes
                    '                    modmordstdet(j, R, 18 + G) = modmordstdet(20, R, 18 + G) / mortNCD * aNCD * Exp(BNCD * (j - 1) * 5) / (1 + aNCD * Exp(BNCD * (j - 1) * 5))
                    '                    'Mental Health
                    '                    modmordstdet(j, R, 28 + G) = modmordstdet(20, R, 28 + G) / mortNCD * aNCD * Exp(BNCD * (j - 1) * 5) / (1 + aNCD * Exp(BNCD * (j - 1) * 5))
                    '                End If
                    '
                    '                'Other Comm Dis
                    '                If mortCD > 0 Then
                    '                    modmordstdet(j, R, G) = modmordstdet(20, R, G) / mortCD * aCD * Exp(BCD * (j - 1) * 5) / (1 + aCD * Exp(BCD * (j - 1) * 5))
                    '                    For z = 20 To 26 Step 2
                    '                        modmordstdet(j, R, z + G) = modmordstdet(20, R, z + G) / mortCD * aCD * Exp(BCD * (j - 1) * 5) / (1 + aCD * Exp(BCD * (j - 1) * 5))
                    '                    Next z
                    '                End If
                    '
                    '                'Injuries
                    '                If mortInj > 0 Then
                    '                    For z = 12 To 17 Step 2
                    '                        modmordstdet(j, R, z + G) = modmordstdet(20, R, z + G) / mortInj * aInj * Exp(BInj * (j - 1) * 5) / (1 + aInj * Exp(BInj * (j - 1) * 5))
                    '                    Next z
                    '                End If
                    '            Next j

                    '            'NCDs
                    '            If IY = 1 And aNCDT > -1 And BNCDT > -1 Then
                    '                'Compute MortT
                    '                'for adults 25+ (6)
                    '                'jrs 2013/02/05 replaced to use parameter
                    '                For j = ageStNCD To NCohorts
                    '                    mortNCD = 0
                    '                    For z = 2 To 10 Step 2
                    '                        mortNCD = mortNCD + modmordstdet(j, R, z + G)
                    '                    Next z
                    '                    'Diabetes
                    '                    mortNCD = mortNCD + modmordstdet(j, R, 18 + G)
                    '                    'Mental Health
                    '                    mortNCD = mortNCD + modmordstdet(j, R, 28 + G)
                    '                    mortNCD = mortNCD / 1000
                    '
                    '                    'Compute Target mortality
                    '                    If mortNCD > 0 Then
                    '                        For z = 2 To 10 Step 2
                    '                            modmordstdetTI(j, R, z + G) = modmordstdet(j, R, z + G) / mortNCD * aNCDT * Exp(BNCDT * (j - 1) * 5) / (1 + aNCDT * Exp(BNCDT * (j - 1) * 5))
                    '                        Next z
                    '                        'Diabetes
                    '                        modmordstdetTI(j, R, 18 + G) = modmordstdet(j, R, 18 + G) / mortNCD * aNCDT * Exp(BNCDT * (j - 1) * 5) / (1 + aNCDT * Exp(BNCDT * (j - 1) * 5))
                    '                        'Mental Health
                    '                        modmordstdetTI(j, R, 28 + G) = modmordstdet(j, R, 28 + G) / mortNCD * aNCDT * Exp(BNCDT * (j - 1) * 5) / (1 + aNCDT * Exp(BNCDT * (j - 1) * 5))
                    '                    End If
                    '                Next j
                    '            End If
                    '
                    '            'CDs
                    '            If IY = 1 And aCDT > -1 And BCDT > -1 Then
                    '                'Compute MortT
                    '                'for adults 25+ (6)
                    '                'jrs 2013/02/05 replaced to use parameter
                    '                For j = ageStCD To NCohorts
                    '                    mortCD = modmordstdet(j, R, G) + modmordstdet(j, R, 20 + G) + _
                    '                    modmordstdet(j, R, 22 + G) + modmordstdet(j, R, 24 + G) + modmordstdet(j, R, 26 + G)
                    '                    mortCD = mortCD / 1000
                    '
                    '                    'Compute Target mortality
                    '                    If mortCD > 0 Then
                    '                        modmordstdetTI(j, R, G) = modmordstdet(j, R, G) / mortCD * aCDT * Exp(BCDT * (j - 1) * 5) / (1 + aCDT * Exp(BCDT * (j - 1) * 5))
                    '                        For z = 20 To 26 Step 2
                    '                            modmordstdetTI(j, R, z + G) = modmordstdet(j, R, z + G) / mortCD * aCDT * Exp(BCDT * (j - 1) * 5) / (1 + aCDT * Exp(BCDT * (j - 1) * 5))
                    '                        Next z
                    '                    End If
                    '                Next j
                    '            End If
                    '
                    '            'Injuries
                    '            If IY = 1 And aInjT > -1 And BInjT > -1 Then
                    '                'Compute MortT
                    '                'for adults 25+ (6)
                    '                'jrs 2013/02/05 replaced to use parameter
                    '                For j = ageStInj To NCohorts
                    '                    mortInj = 0
                    '                    For z = 12 To 17 Step 2
                    '                        mortInj = mortInj + modmordstdet(j, R, z + G)
                    '                    Next z
                    '                    mortInj = mortInj / 1000
                    '
                    '                    If mortInj > 0 Then
                    '                        For z = 12 To 17 Step 2
                    '                            modmordstdetTI(j, R, z + G) = modmordstdet(j, R, z + G) / mortInj * aInjT * Exp(BInjT * (j - 1) * 5) / (1 + aInjT * Exp(BInjT * (j - 1) * 5))
                    '                        Next z
                    '                    End If
                    '                Next j
                    '            End If

                    'jrs 2014/06/12
                    If IY > 2 Then
                        If SHealthExpToCostGap(R) < 0 Then 'jrs 2014/06/20 added and SHealthExpToCostGap(R) < 0
                            For k = 1 To CInt(Ntypes / 2)
                                'jrs 2014/06/20
                                'HlExpToCostGapFact(k) = hlexpcostgaptohealthel(k) * SHealthExpToCostGap(R)
                                'HlExpToCostGapFact(k) = AMAX(HlExpToCostGapFact(k), -0.8)
                                'jrs 2014/09/25
                                HlExpToCostGapFact(k) = -1 * SHealthExpToCostGap(R) * SGDPinPOP(R) / SHLCOST(R) 'jrs 2014/07/22 adding 0.5 to reduce the effect of gap
                                'HlExpToCostGapFact(k) = 0
                            Next k
                        Else
                            For k = 1 To CInt(Ntypes / 2)
                                HlExpToCostGapFact(k) = 0
                            Next k
                        End If
                    Else
                        For k = 1 To CInt(Ntypes / 2)
                            HlExpToCostGapFact(k) = 0
                        Next k
                    End If

                    'jrs 2014/08/04
                    For k = 1 To CInt(Ntypes / 2)
                        If alphasT(R, k, G) > -1 And IY > 1 Then
                            'for all ages
                            For j = ageSt(k) To NCohorts
                                'If GRPS(1, R) = "Japan" And IY = 91 And G = 1 And k = 2 And j = 20 Then Stop
                                'Adjustment for Health Gap
                                '-------------------------
                                If HlExpToCostGapFact(k) > 0 Then
                                    modmordstdetT(j, R, (k - 1) * 2 + G) = Amin(AMAX(modmordstdetTI(j, R, (k - 1) * 2 + G) * (1 - HlExpToCostGapFact(k)) + modmordstdet(j, R, (k - 1) * 2 + G) * HlExpToCostGapFact(k), 0), 1000)
                                Else
                                    modmordstdetT(j, R, (k - 1) * 2 + G) = modmordstdetTI(j, R, (k - 1) * 2 + G)
                                End If

                                If hllongvtyalphaconv(R, k) + 1 > IY Then
                                    'Do convergence from WHO mortality to Target mortality
                                    modmordstdet(j, R, (k - 1) * 2 + G) = ConvergeOverTime(CSng(modmordstdet(j, R, (k - 1) * 2 + G)), modmordstdetT(j, R, (k - 1) * 2 + G), hllongvtyalphaconv(R, k) + 1)
                                Else
                                    modmordstdet(j, R, (k - 1) * 2 + G) = modmordstdetT(j, R, (k - 1) * 2 + G)
                                End If
                            Next j
                        End If
                    Next k

                    'jrs 2014/04/08
                    '            'Convergence of mortality
                    '            'Non Comm Dis
                    '            If aNCDT > -1 And IY > 1 Then
                    '                'for all ages
                    '                'jrs 2013/01/10 now only for adults 25+ (6)
                    '                'jrs 2013/02/05 replaced to use parameter
                    '                For j = ageStNCD To NCohorts
                    '                    'jrs 2013/01/10 this is done only in year 1
                    ''                    mortNCD = 0
                    ''                    For z = 2 To 10 Step 2
                    ''                        mortNCD = mortNCD + modmordstdet(j, R, z + G)
                    ''                    Next z
                    ''                    'Diabetes
                    ''                    mortNCD = mortNCD + modmordstdet(j, R, 18 + G)
                    ''                    'Mental Health
                    ''                    mortNCD = mortNCD + modmordstdet(j, R, 28 + G)
                    ''                    mortNCD = mortNCD / 1000
                    ''
                    ''                    aNCD = aNCDC
                    ''                    BNCD = BNCDC
                    '                    'Compute Target mortality
                    '                    'If mortNCD > 0 Then
                    ''If GRPS(1, R) = "United States of America" And j = 14 And FirstYear + IY - 1 >= 2030 And G = 1 Then Stop
                    '                    For z = 2 To 10 Step 2
                    '                        'jrs 2014/06/12
                    '                        'Adjustment for Health Gap
                    '                        '-------------------------
                    '                        'jrs 2014/06/20
                    ''                        If HlExpToCostGapFact(CInt((z + G) / 2)) <> -1 Then
                    ''                            modmordstdetT(j, R, z + G) = Amin(AMAX(modmordstdetT(j, R, z + G) + modmordstdet(j, R, z + G) * HlExpToCostGapFact(CInt((z + G) / 2)) / (1 + HlExpToCostGapFact(CInt((z + G) / 2))), 0), 1000)
                    ''                        End If
                    '                        If HlExpToCostGapFact(CInt((z + G) / 2)) > 0 Then
                    '                            modmordstdetT(j, R, z + G) = Amin(AMAX(modmordstdetTI(j, R, z + G) * (1 - HlExpToCostGapFact(CInt((z + G) / 2))) + modmordstdet(j, R, z + G) * HlExpToCostGapFact(CInt((z + G) / 2)), 0), 1000)
                    '                        Else
                    '                            modmordstdetT(j, R, z + G) = modmordstdetTI(j, R, z + G)
                    '                        End If
                    '
                    '                        'modmordstdetT(j, R, z + G) = modmordstdet(j, R, z + G) / mortNCD * aNCD * Exp(BNCD * (j - 1) * 5) / (1 + aNCD * Exp(BNCD * (j - 1) * 5))
                    '                        If hllongvtyalphaconv(R, 2) + 1 > IY Then
                    '                            'Do convergence from WHO mortality to Target mortality
                    '                            modmordstdet(j, R, z + G) = ConvergeOverTime(modmordstdet(j, R, z + G), modmordstdetT(j, R, z + G), hllongvtyalphaconv(R, 2) + 1)
                    '                        Else
                    '                            modmordstdet(j, R, z + G) = modmordstdetT(j, R, z + G)
                    '                        End If
                    '                    Next z
                    '                    'Diabetes
                    '                    'jrs 2014/06/12
                    '                    'Adjustment for Health Gap
                    '                    '-------------------------
                    '                    'jrs 2014/06/20
                    ''                    If HlExpToCostGapFact(10) <> -1 Then
                    ''                        modmordstdetT(j, R, 18 + G) = Amin(AMAX(modmordstdetT(j, R, 18 + G) + modmordstdet(j, R, 18 + G) * HlExpToCostGapFact(10) / (1 + HlExpToCostGapFact(10)), 0), 1000)
                    ''                    End If
                    '                    If HlExpToCostGapFact(10) > 0 Then
                    '                        modmordstdetT(j, R, 18 + G) = Amin(AMAX(modmordstdetTI(j, R, 18 + G) * (1 - HlExpToCostGapFact(10)) + modmordstdet(j, R, 18 + G) * HlExpToCostGapFact(10), 0), 1000)
                    '                    Else
                    '                        modmordstdetT(j, R, 18 + G) = modmordstdetTI(j, R, 18 + G)
                    '                    End If
                    '
                    '                    'modmordstdetT(j, R, 18 + G) = modmordstdet(j, R, 18 + G) / mortNCD * aNCD * Exp(BNCD * (j - 1) * 5) / (1 + aNCD * Exp(BNCD * (j - 1) * 5))
                    '                    If hllongvtyalphaconv(R, 2) + 1 > IY Then
                    '                        'Do convergence from WHO mortality to Target mortality
                    '                        modmordstdet(j, R, 18 + G) = ConvergeOverTime(modmordstdet(j, R, 18 + G), modmordstdetT(j, R, 18 + G), hllongvtyalphaconv(R, 2) + 1)
                    '                    Else
                    '                        modmordstdet(j, R, 18 + G) = modmordstdetT(j, R, 18 + G)
                    '                    End If
                    '                    'Mental Health
                    '                    'jrs 2014/06/12
                    '                    'Adjustment for Health Gap
                    '                    '-------------------------
                    '                    'jrs 2014/06/20
                    ''                    If HlExpToCostGapFact(15) <> -1 Then
                    ''                        modmordstdetT(j, R, 28 + G) = Amin(AMAX(modmordstdetT(j, R, 28 + G) + modmordstdet(j, R, 28 + G) * HlExpToCostGapFact(15) / (1 + HlExpToCostGapFact(15)), 0), 1000)
                    ''                    End If
                    '                    If HlExpToCostGapFact(15) > 0 Then
                    '                        modmordstdetT(j, R, 28 + G) = Amin(AMAX(modmordstdetTI(j, R, 28 + G) * (1 - HlExpToCostGapFact(15)) + modmordstdet(j, R, 28 + G) * HlExpToCostGapFact(15), 0), 1000)
                    '                    Else
                    '                        modmordstdetT(j, R, 28 + G) = modmordstdetTI(j, R, 28 + G)
                    '                    End If
                    '
                    '                    'modmordstdetT(j, R, 28 + G) = modmordstdet(j, R, 28 + G) / mortNCD * aNCD * Exp(BNCD * (j - 1) * 5) / (1 + aNCD * Exp(BNCD * (j - 1) * 5))
                    '                    If hllongvtyalphaconv(R, 2) + 1 > IY Then
                    '                        'Do convergence from WHO mortality to Target mortality
                    '                        modmordstdet(j, R, 28 + G) = ConvergeOverTime(modmordstdet(j, R, 28 + G), modmordstdetT(j, R, 28 + G), hllongvtyalphaconv(R, 2) + 1)
                    '                    Else
                    '                        modmordstdet(j, R, 28 + G) = modmordstdetT(j, R, 28 + G)
                    '                    End If
                    '                    'End If
                    '                Next j
                    '            End If

                    '            'Other Comm Dis
                    '            If aCDT > -1 And IY > 1 Then
                    '                'for all ages
                    '                'jrs 2013/01/10 now only for adults 25+ (6)
                    '                'jrs 2013/02/05 replaced to use parameter
                    '                For j = ageStCD To NCohorts
                    '                    'jrs 2013/01/10 this is done only in year 1
                    ''                    mortCD = modmordstdet(j, R, G) + modmordstdet(j, R, 20 + G) + _
                    ''                    modmordstdet(j, R, 22 + G) + modmordstdet(j, R, 24 + G) + modmordstdet(j, R, 26 + G)
                    ''                    mortCD = mortCD / 1000
                    ''
                    ''                    aCD = aCDC
                    ''                    BCD = BCDC
                    '                    'Compute Target mortality
                    '                    'If mortCD > 0 Then
                    '                    'modmordstdetT(j, R, G) = modmordstdet(j, R, G) / mortCD * aCD * Exp(BCD * (j - 1) * 5) / (1 + aCD * Exp(BCD * (j - 1) * 5))
                    '
                    '                    'jrs 2014/06/12
                    '                    'Adjustment for Health Gap
                    '                    '-------------------------
                    '                    'jrs 2014/06/20
                    ''                    If HlExpToCostGapFact(1) <> -1 Then
                    ''                        modmordstdetT(j, R, G) = Amin(AMAX(modmordstdetT(j, R, G) + modmordstdet(j, R, G) * HlExpToCostGapFact(1) / (1 + HlExpToCostGapFact(1)), 0), 1000)
                    ''                    End If
                    '                    If HlExpToCostGapFact(1) > 0 Then
                    '                        modmordstdetT(j, R, G) = Amin(AMAX(modmordstdetTI(j, R, G) * (1 - HlExpToCostGapFact(1)) + modmordstdet(j, R, G) * HlExpToCostGapFact(1), 0), 1000)
                    '                    Else
                    '                        modmordstdetT(j, R, G) = modmordstdetTI(j, R, G)
                    '                    End If
                    '
                    '                    If hllongvtyalphaconv(R, 1) + 1 > IY Then
                    '                        'Do convergence from WHO mortality to Target mortality
                    '                        modmordstdet(j, R, G) = ConvergeOverTime(modmordstdet(j, R, G), modmordstdetT(j, R, G), hllongvtyalphaconv(R, 1) + 1)
                    '                    Else
                    '                        modmordstdet(j, R, G) = modmordstdetT(j, R, G)
                    '                    End If
                    '                    For z = 20 To 26 Step 2
                    '                        'jrs 2014/06/12
                    '                        'Adjustment for Health Gap
                    '                        '-------------------------
                    '                        'jrs 2014/06/20
                    ''                        If HlExpToCostGapFact(CInt((z + G) / 2)) <> -1 Then
                    ''                            modmordstdetT(j, R, z + G) = Amin(AMAX(modmordstdetT(j, R, z + G) + modmordstdet(j, R, z + G) * HlExpToCostGapFact(CInt((z + G) / 2)) / (1 + HlExpToCostGapFact(CInt((z + G) / 2))), 0), 1000)
                    ''                        End If
                    '                        If HlExpToCostGapFact(CInt((z + G) / 2)) > 0 Then
                    '                            modmordstdetT(j, R, z + G) = Amin(AMAX(modmordstdetTI(j, R, z + G) * (1 - HlExpToCostGapFact(CInt((z + G) / 2))) + modmordstdet(j, R, z + G) * HlExpToCostGapFact(CInt((z + G) / 2)), 0), 1000)
                    '                        Else
                    '                            modmordstdetT(j, R, z + G) = modmordstdetTI(j, R, z + G)
                    '                        End If
                    '                        'modmordstdetT(j, R, z + G) = modmordstdet(j, R, z + G) / mortCD * aCD * Exp(BCD * (j - 1) * 5) / (1 + aCD * Exp(BCD * (j - 1) * 5))
                    '                        If hllongvtyalphaconv(R, 1) + 1 > IY Then
                    '                            'Do convergence from WHO mortality to Target mortality
                    '                            modmordstdet(j, R, z + G) = ConvergeOverTime(modmordstdet(j, R, z + G), modmordstdetT(j, R, z + G), hllongvtyalphaconv(R, 1) + 1)
                    '                        Else
                    '                            modmordstdet(j, R, z + G) = modmordstdetT(j, R, z + G)
                    '                        End If
                    '                    Next z
                    '                    'End If
                    '                Next j
                    '            End If
                    '
                    '            'Injuries
                    '            If aInjT > -1 And IY > 1 Then
                    '                'for all ages
                    '                'jrs 2013/01/10 now only for adults 25+ (6)
                    '                'jrs 2013/02/05 replaced to use parameter
                    '                For j = ageStInj To NCohorts
                    '                    'jrs 2013/01/10 this is done only in year 1
                    ''                    mortInj = 0
                    ''                    For z = 12 To 17 Step 2
                    ''                        mortInj = mortInj + modmordstdet(j, R, z + G)
                    ''                    Next z
                    ''                    mortInj = mortInj / 1000
                    ''
                    ''                    aInj = aInjC
                    ''                    BInj = BInjC
                    '
                    '                    'If mortInj > 0 Then
                    '                    For z = 12 To 17 Step 2
                    '                        'jrs 2014/06/12
                    '                        'Adjustment for Health Gap
                    '                        '-------------------------
                    '                        'jrs 2014/06/20
                    ''                        If HlExpToCostGapFact(CInt((z + G) / 2)) <> -1 Then
                    ''                            modmordstdetT(j, R, z + G) = Amin(AMAX(modmordstdetT(j, R, z + G) + modmordstdet(j, R, z + G) * HlExpToCostGapFact(CInt((z + G) / 2)) / (1 + HlExpToCostGapFact(CInt((z + G) / 2))), 0), 1000)
                    ''                        End If
                    '                        If HlExpToCostGapFact(CInt((z + G) / 2)) > 0 Then
                    '                            modmordstdetT(j, R, z + G) = Amin(AMAX(modmordstdetTI(j, R, z + G) * (1 - HlExpToCostGapFact(CInt((z + G) / 2))) + modmordstdet(j, R, z + G) * HlExpToCostGapFact(CInt((z + G) / 2)), 0), 1000)
                    '                        Else
                    '                            modmordstdetT(j, R, z + G) = modmordstdetTI(j, R, z + G)
                    '                        End If
                    '                        'modmordstdetT(j, R, z + G) = modmordstdet(j, R, z + G) / mortInj * aInj * Exp(BInj * (j - 1) * 5) / (1 + aInj * Exp(BInj * (j - 1) * 5))
                    '                        If hllongvtyalphaconv(R, 3) + 1 > IY Then
                    '                            'Do convergence from WHO mortality to Target mortality
                    '                            modmordstdet(j, R, z + G) = ConvergeOverTime(modmordstdet(j, R, z + G), modmordstdetT(j, R, z + G), hllongvtyalphaconv(R, 3) + 1)
                    '                        Else
                    '                            modmordstdet(j, R, z + G) = modmordstdetT(j, R, z + G)
                    '                        End If
                    '                    Next z
                    '                    'End If
                    '                Next j
                    '            End If
                Next G
            Next R
        End Sub

        'jrs 2014/09/24
        Private Sub LoadAlphaFxs(alphaFxs(,) As String)
            alphaFxs(1, 1) = "GDP/Capita (PPP 2011) Versus Alpha for Other Communicable Diseases Males (Logit)"
            alphaFxs(1, 2) = "GDP/Capita (PPP 2011) Versus Alpha for Other Communicable Diseases Females (Logit)"
            alphaFxs(2, 1) = "GDP/Capita (PPP 2011) Versus Alpha for Malignant Neoplasms Males (Log)"
            alphaFxs(2, 2) = "GDP/Capita (PPP 2011) Versus Alpha for Malignant Neoplasms Females (Log)"
            alphaFxs(3, 1) = "GDP/Capita (PPP 2011) Versus Alpha for Cardiovascular Disease Males (Logit)"
            alphaFxs(3, 2) = "GDP/Capita (PPP 2011) Versus Alpha for Cardiovascular Disease Females (Logit)"
            alphaFxs(4, 1) = "GDP/Capita (PPP 2011) Versus Alpha for Digestive Diseases Males (Log)"
            alphaFxs(4, 2) = "GDP/Capita (PPP 2011) Versus Alpha for Digestive Diseases Females (Log)"
            alphaFxs(5, 1) = "GDP/Capita (PPP 2011) Versus Alpha for Respiratory Disease Males (Logit)"
            alphaFxs(5, 2) = "GDP/Capita (PPP 2011) Versus Alpha for Respiratory Disease Females (Logit)"
            alphaFxs(6, 1) = "GDP/Capita (PPP 2011) Versus Alpha for Other non-Communicable Diseases Males (Logit)"
            alphaFxs(6, 2) = "GDP/Capita (PPP 2011) Versus Alpha for Other non-Communicable Diseases Females (Logit)"
            alphaFxs(7, 1) = "GDP/Capita (PPP 2011) Versus Alpha for Traffic Males (Logit)"
            alphaFxs(7, 2) = "GDP/Capita (PPP 2011) Versus Alpha for Traffic Females (Logit)"
            alphaFxs(8, 1) = "GDP/Capita (PPP 2011) Versus Alpha for Unintentional Injuries Males (Logit)"
            alphaFxs(8, 2) = "GDP/Capita (PPP 2011) Versus Alpha for Unintentional Injuries Females (Logit)"
            alphaFxs(9, 1) = "GDP/Capita (PPP 2011) Versus Alpha for Intentional Injuries Males (Logit)"
            alphaFxs(9, 2) = "GDP/Capita (PPP 2011) Versus Alpha for Intentional Injuries Females (Logit)"
            alphaFxs(10, 1) = "GDP/Capita (PPP 2011) Versus Alpha for Diabetes Males"
            alphaFxs(10, 2) = "GDP/Capita (PPP 2011) Versus Alpha for Diabetes Females"
            alphaFxs(11, 1) = "GDP/Capita (PPP 2011) Versus Alpha for AIDS Males (Logit)"
            alphaFxs(11, 2) = "GDP/Capita (PPP 2011) Versus Alpha for AIDS Females (Logit)"
            alphaFxs(12, 1) = "GDP/Capita (PPP 2011) Versus Alpha for Diarrhea Males (Logit)"
            alphaFxs(12, 2) = "GDP/Capita (PPP 2011) Versus Alpha for Diarrhea Females (Logit)"
            alphaFxs(13, 1) = "GDP/Capita (PPP 2011) Versus Alpha for Malaria Males (Logit)"
            alphaFxs(13, 2) = "GDP/Capita (PPP 2011) Versus Alpha for Malaria Females (Logit)"
            alphaFxs(14, 1) = "GDP/Capita (PPP 2011) Versus Alpha for Resp. Inf. Males (Logit)"
            alphaFxs(14, 2) = "GDP/Capita (PPP 2011) Versus Alpha for Resp. Inf. Females (Logit)"
            alphaFxs(15, 1) = "GDP/Capita (PPP 2011) Versus Alpha for Mental Health Males"
            alphaFxs(15, 2) = "GDP/Capita (PPP 2011) Versus Alpha for Mental Health Females"
        End Sub

        'jrs 2012/09/13
        'This procedure takes the newly created mortality vector and distributes the existing population of age 100+
        'In the base year
        Private Sub PopRedistributionofCentenarians(ByRef modmordstdet(,,) As Double, ByRef agedst(,,) As Double, ByVal Ntypes As Integer, context As IForecastContext)
            Dim R As Integer, G As Integer, j As Integer, z As Integer
            Dim mort As Single, PopOrg As Single, PopOrg1 As Single
            For R = 1 To NR
                For G = 1 To 2
                    PopOrg = 0
                    For j = 21 To NCohorts
                        PopOrg = PopOrg + CSng(agedst(j, R, G))
                    Next j
                    PopOrg1 = PopOrg

                    For j = 21 To NCohorts
                        mort = 0
                        For z = 1 To Ntypes
                            If (z Mod 2 = 1 And G = 1) Or (z Mod 2 = 0 And G = 2) Then
                                mort = mort + CSng(modmordstdet(j, R, z))
                            End If
                        Next z
                        If mort > 1000 Then
                            mort = 1000
                        End If
                        'Save the 1 year population
                        If j < NCohorts Then
                            For z = 5 To 1 Step -1
                                fagedst(j * 5 - z, R, G) = AMAX(Round(PopOrg1 * mort / 1000, 6), 0) 'jrs 2013/02/19
                                PopOrg1 = AMAX(PopOrg1 - fagedst(j * 5 - z, R, G), 0)
                            Next z
                        End If

                        'Convert to 5 year mortality
                        mort = CSng(1 - ((1 - mort / 1000) ^ 5))
                        'this is the population that will die in this cohort,
                        'others will survive above it
                        agedst(j, R, G) = AMAX(Round(PopOrg * mort, 6), 0) 'jrs 2013/02/19

                        PopOrg = AMAX(PopOrg - agedst(j, R, G), 0)
                    Next j
                Next G
            Next R

            'Delete previously saved centenarians
            Dim sql As String
            Using cmd = conn3.CreateCommand()
                'jrs 2024/12/20 changed NCohorts to CohortId
                sql = "DELETE FROM pop_base_case WHERE CohortId > 20 AND TimeId = " & CStr(FirstYear)
                cmd.CommandText = sql
                cmd.ExecuteNonQuery()
            End Using

            'Save new population of Centenarians
            Using db As IBasecase = BasecaseContext.Open(False, context, sIFsPopFile)
                '   Age Distribution
                For tempi% = 21 To NCohorts 'jrs 2008/04/15 save infants too, it used to be from 1
                    For tempj% = 1 To NR%
                        db.Add(New PopBasecase() With {
                            .TimeId = CShort(IY% + FirstYear - 1),
                            .RegionId = CShort(tempj%),
                            .CohortId = CShort(tempi%),
                            .PopulationMales = CSng(Round(agedst(tempi%, tempj%, 1), 6)), 'jrs 2012/09/18 added round 6
                            .PopulationFemales = CSng(Round(agedst(tempi%, tempj%, 2), 6)) 'jrs 2012/09/18 added round 6
                        })
                    Next
                Next
                db.Flush()
            End Using

            'Recompute Deaths
            '----------------
            For R = 1 To NR
                For G = 1 To 2
                    For j = 21 To NCohorts
                        mort = 0
                        For z = 1 To Ntypes
                            If (z Mod 2 = 1 And G = 1) Or (z Mod 2 = 0 And G = 2) Then
                                mort = CSng(mort + modmordstdet(j, R, z))
                            End If
                        Next z
                        If mort > 1000 Then
                            mort = 1000
                        End If
                        'jrs 2012/11/08 min = 0, max = agedst
                        d(j, R, G) = AMAX(0, Amin(agedst(j, R, G),
                        Round(agedst(j, R, G) * mort / 1000, 6)))
                    Next j
                Next G
            Next R

            '   Spread Age Distribution Across Years - Full Distribution
            '   ---------------------------------------------------------------------
            Dim Sum, Sum2, MulFage As Double
            Dim msg As String
            For R = 1 To NR
                For j = 21 To NCohorts
                    If j < NCohorts Then
                        For G = 1 To 2 'spline function spread of population across years

                            Sum = fagedst(j * 5 - 5, R, G) + fagedst(j * 5 - 4, R, G) +
                        fagedst(j * 5 - 3, R, G) + fagedst(j * 5 - 2, R, G) +
                        fagedst(j * 5 - 1, R, G)
                            If Sum <> agedst(j, R, G) Then
                                If agedst(j, R, G) > 0 And Sum > 0 Then
                                    MulFage = agedst(j, R, G) / Sum
                                    For z = 5 To 1 Step -1
                                        fagedst(j * 5 - z, R, G) = Round(fagedst(j * 5 - z, R, G) * MulFage, 6)
                                    Next z
                                    Sum2 = fagedst(j * 5 - 5, R, G) + fagedst(j * 5 - 4, R, G) +
                                fagedst(j * 5 - 3, R, G) + fagedst(j * 5 - 2, R, G) +
                                fagedst(j * 5 - 1, R, G)
                                    If Abs(Sum2 - agedst(j, R, G)) > 0.000005 And bShowMsg Then 'jrs 2008/04/02
                                        msg$ = "There is an error in spreading populuation to 1-year cohorts"
                                        'jrs 2013/02/18
                                        msg$ = msg$ & " for " & GRPS(1, R) & " " & GRPS(23, G) & " at age " & CStr((j - 1) * 5)
                                        Debug.Print(msg$)
                                    End If
                                End If
                            End If
                        Next G
                    Else
                        For G = 1 To 2
                            fagedst(NAges, R, G) = agedst(j, R, G)
                        Next G
                    End If
                Next j
            Next R
        End Sub

        'jrs 2018/06/04
        Private Sub UpdateIntInj()
            'Replace Intentional Injury Mortality with values computed in the Societal Violence Module
            '-----------------------------------------------------------------------------------------
            Dim R As Integer, G As Integer, j As Integer
            Dim Weights(,) As Double
            ReDim Weights(NCohorts + 3, 2)
            For R = 1 To NR
                For G = 1 To 2
                    'Load Weights with Health module forecast
                    '-----------------------------------------
                    Weights(NCohorts + 1, G) = 0
                    Weights(NCohorts + 2, G) = 0
                    Weights(NCohorts + 3, G) = 0
                    For j = 0 To NCohorts
                        Weights(j, G) = modmordstdet(j, R, 16 + G) * agedst(j, R, G)
                        Weights(NCohorts + 1, G) = Weights(NCohorts + 1, G) + modmordstdet(j, R, 16 + G) * agedst(j, R, G)
                        If j > 3 Then
                            Weights(NCohorts + 2, G) = Weights(NCohorts + 2, G) + modmordstdet(j, R, 16 + G) * agedst(j, R, G)
                        Else
                            Weights(NCohorts + 3, G) = Weights(NCohorts + 3, G) + modmordstdet(j, R, 16 + G) * agedst(j, R, G)
                        End If
                    Next j
                Next G
                'Distribute deaths from conflict, police and self harm among entire population
                '----------------------------------------------------------------------------
                If (Weights(NCohorts + 1, 1) + Weights(NCohorts + 1, 2)) > 0 Then
                    'If GRPS(1, R) = "United States of America" Then Stop
                    For G = 1 To 2
                        For j = 0 To NCohorts
                            If agedst(j, R, G) > 0 Then
                                modmordstdet(j, R, 16 + G) = (SVDTHSCONFLICTTOT(R) + SVDTHSPOLICSTOT(R) + SVDTHSSELFHARMTOT(R)) / (1000 * agedst(j, R, G)) * Weights(j, G) / (Weights(NCohorts + 1, 1) + Weights(NCohorts + 1, 2))
                            End If
                        Next j
                    Next G
                End If
                'Distribute deaths from homicides among adult males
                '--------------------------------------------------
                If Weights(NCohorts + 2, 1) > 0 Then
                    For j = 4 To NCohorts
                        If agedst(j, R, 1) > 0 Then
                            modmordstdet(j, R, 17) = modmordstdet(j, R, 17) + SVDTHSOTHERINTERTOT(R) / (1000 * agedst(j, R, 1)) * Weights(j, 1) / Weights(NCohorts + 2, 1)
                        End If
                    Next j
                End If
                'Distribute deaths from feminicides among adult females
                '------------------------------------------------------
                If Weights(NCohorts + 2, 2) > 0 Then
                    G = 2
                    For j = 4 To NCohorts
                        If agedst(j, R, 2) > 0 Then
                            modmordstdet(j, R, 18) = modmordstdet(j, R, 18) + SVDTHSWOMENTOT(R) / (1000 * agedst(j, R, 2)) * Weights(j, 2) / Weights(NCohorts + 2, 2)
                        End If
                    Next j
                End If
                'Distribute deaths from violence against children
                '------------------------------------------------
                If (Weights(NCohorts + 3, 1) + Weights(NCohorts + 3, 2)) > 0 Then
                    For G = 1 To 2
                        For j = 0 To 3
                            If agedst(j, R, G) > 0 Then
                                modmordstdet(j, R, 16 + G) = modmordstdet(j, R, 16 + G) + SVDTHSCHILTOT(R) / (1000 * agedst(j, R, G)) * Weights(j, G) / (Weights(NCohorts + 3, 1) + Weights(NCohorts + 3, 2))
                            End If
                        Next j
                    Next G
                End If
            Next R
        End Sub

        'jrs 2019/05/01
        Private Sub KeepMortalityConstant()
            Dim R As Integer, T As Integer, A As Integer, G As Integer
            For R = 1 To NR
                For T = 1 To CInt(Ntypes / 2)
                    If hlmortconstsw(R, T) > 0 Then
                        For G = 1 To 2
                            For A = 0 To NCohorts
                                modmordstdet(A, R, (2 * T) - (G Mod 2)) = smodmordstdet(A, R, (2 * T) - (G Mod 2))
                            Next A
                        Next G
                    End If
                Next T
            Next R
        End Sub

        'jrs 2019/05/17 moving logic by Barry
        'bbh 2019/04/06
        Private Sub ifsHealthHIVMalaria(R As Integer, context As IForecastContext)
            Dim TbName As String
            Dim CalcCondomUse, CalcART, BNetIncentive, MalIncR, MalPrevR, MalMortR As Single
            Dim CalcIncRPrev, CalcMortRPrev, NormalDeaths, TotDeathsPrev As Single
            'For R% = 1 To NR
            'All Years: HIV, Malaria, and Driving Variables
            '----------------------------------------------
            If IY% > 1 Then  'Large All Years but First Block
                'Condom use Driving Variable - All Years but First
                '-------------------------------------------------
                'jrs 2019/09/17 can't have search inside loop
                'TbName$ = "Education Years (15+) Versus Condom Use (" & CStr(BaseYear) & ") Log"
                'TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
                TbName$ = "Education Years (15+) Versus Condom Use (2014) Log"
                Call XYTABL(TbName$, EDYRSAG15(R%, 3), CalcCondomUse, context) 'check to make sure edyrsag15 is calculated already after new rebase
                CalcCondomUse = Amin(CalcCondomUse, 100)
                ' If RegName$(R%) = "Afghanistan" Then Stop
                If CondomUseShift(R%) > 1 Then
                    HLCONDOMUSE(R%) = CalcCondomUse * CondomUseShift(R%)
                Else
                    HLCONDOMUSE(R%) = CalcCondomUse * ConvergeOverTime(CondomUseShift(R%), 1, 100)
                End If
                If SHlHIVPrev(R%) / AMAX(0.00001, POP(R%)) > 0.02 Then
                    HLCONDOMUSE(R%) = CSng(HLCONDOMUSE(R%) * (1 + IY% / 50)) 'assume 2% annual growth regardless
                End If
                HLCONDOMUSE(R%) = Amin(100, HLCONDOMUSE(R%) * hlcondomusem(R%))

                'ART Driving Variable - All Years but First
                '------------------------------------------
                'jrs 2019/09/17 can't have search inside loop
                'TbName$ = "GDP/Capita (PPP 2011) Versus Percentage ART Coverage (" & CStr(BaseYear) & ") Log"
                'TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
                'jrs 2024/06/20 TbName$ = "GDP/Capita (PPP 2011) Versus Percentage ART Coverage (2017) Log"
                Call XYTABL(TFNamePctARTCov, GDPPCP(R%), CalcART, context) 'check to make sure GDPPCP is calculated already after new rebase
                If ARTShift(R%) > 1 Then
                    HLHIVART(R%) = CalcART * ARTShift(R%)
                Else
                    HLHIVART(R%) = CalcART * ConvergeOverTime(ARTShift(R%), 1, 200)
                End If
                If SHlHIVPrev(R%) / AMAX(0.00001, POP(R%)) > 0.02 Then
                    HLHIVART(R%) = CSng(HLHIVART(R%) * (1 + IY% / 50)) 'assume 2% annual growth regardless
                End If
                HLHIVART(R%) = Amin(100, HLHIVART(R%) * hlhivartm(R%))

                'BedNets Driving Variable - All Years but First
                '-----------------------------------------------
                BNetIncentive = CSng(SHlMalPrevR(R%) / 0.15)
                'in 2017 median rate among those fighting it was about .15 or 15% prevalence
                HLBEDNETS(R%) = SHlBedNets(R%) + 3 * (100 - SHlBedNets(R%)) / 100 * BNetIncentive
                '3 here probably should become a parameter
                HLBEDNETS(R%) = Amin(100, HLBEDNETS(R%) * hlbednetsm(R%))

                'Malaria Incidence - All Years but First
                '---------------------------------------
                MalIncR = CSng(SHLMalIncR(R%) * (1 - (HLBEDNETS(R%) - SHlBedNets(R%)) / 100 - 0.04))
                ' same three here related to bednets; plus about 4% downard trend in rate
                '    (not in numbers becuase pop rising)
                'If RegName$(R%) = "Nigeria" Then Stop
                MalIncR = CSng(AMAX(0.000001, Amin(0.5, MalIncR * hlmalariaincm(R%))))
                SHlBedNets(R%) = HLBEDNETS(R%)
                HLMALARIAINC(R%) = MalIncR * POP(R%)

                'Malaria Prevalence - All Years but First
                '----------------------------------------
                MalPrevR = CSng(SHlMalPrevR(R%) * MalIncR / AMAX(0.000001, SHLMalIncR(R%)) - 0.01 * SHlMalPrevR(R%))
                'Prevalence and Mortality dropped about 1% faster than incidence 2005-2017
                MalPrevR = CSng(AMAX(0, Amin(0.5, MalPrevR * hlmalariaprevm(R%))))
                HLMALARIAPREV(R%) = MalPrevR * POP(R%)

                'Malaria Mortality - All Years but First
                '--------------------------------------
                MalMortR = CSng(SHlMalMortR(R%) * MalIncR / AMAX(0.000001, SHLMalIncR(R%)) - 0.01 * SHlMalMortR(R%))
                MalMortR = CSng(AMAX(0, Amin(0.5, MalMortR * hlmalariamortm(R%))))
                HLMALARIAMORT(R%) = MalMortR * POP(R%)


                'Malaria Variable Wrapping - All Years but First
                '----------------------------------------------
                SHLMalIncR(R%) = MalIncR
                SHlMalPrevR(R%) = MalPrevR
                SHlMalMortR(R%) = MalMortR
                If HLMALARIAPREV(R%) <= 0 Then
                    HLMALMORTPREVR(R%) = 0.04
                    HLMALINCPREVR(R%) = 0.08
                Else
                    HLMALINCPREVR(R%) = HLMALARIAINC(R%) / AMAX(0.000001, HLMALARIAPREV(R%))
                    HLMALMORTPREVR(R%) = HLMALARIAMORT(R%) / AMAX(0.000001, HLMALARIAPREV(R%))
                End If
                HLMALPREVPOPR(R%) = HLMALARIAPREV(R%) / AMAX(0.0001, POP(R%))

                'HIV Incidence, Driven by Prevalence and Other Variables
                '--------------------------------------------------------
                TbName$ = "Condom Use at Risk Sex Versus HIV Incidence/Prevalence (2015) Lin"
                Call XYTABL(TbName$, HLCONDOMUSE(R%), CalcIncRPrev, context)
                If SHlHIVPrev(R%) = 0 Then CalcIncRPrev = 0.08 'value of ratio at 50% Condom use
                HLHIVINC(R%) = CSng(SHlHIVPrev(R%) * CalcIncRPrev * HIVIncShift(R%) * (AMAX(0, 1 - 0.04 * IY% ^ 0.75)))
                'Condoms not nearly enough to put future on historical track; multiplier tuned to global pattern
                '0.75 roughly eliminates mortality by 2100 with 2015 base
                'Need additional decline from behavior change, etc.
                SHlHIVInc(R%) = HLHIVINC(R%) 'Save before multiplier; is flow
                HLHIVINC(R%) = CSng(AMAX(HLHIVINC(R%) * hlhivincm(R%), 0.0001 * SHlHIVPrev(R%)))
                HLHIVINC(R%) = CSng(Amin(0.2 * POP(R%), HLHIVINC(R%)))

                'HIV Mortality, Driven by Prevalence and Other Variables - All Years but First
                '-----------------------------------------------------------------------------
                TbName$ = "HIV Mortality/Prevalence vs ART Percent (2015) Lin"
                Call AnalFunc(TbName$, New Single() {HLHIVART(R%)}, CalcMortRPrev,
                New String() {"HLHIVART"}, context)
                If SHlHIVPrev(R%) = 0 Then CalcMortRPrev = 0.04 'value of ratio at 50% ART coverage
                'HIVMortShift(R%) = CurrentRatio / AMAX(0.0001, CalcMortRPrev)
                HLHIVMORT(R%) = CSng(SHlHIVPrev(R%) * CalcMortRPrev * HIVMortShift(R%) * (AMAX(0, 1 - 0.08 * IY% ^ 0.75)))
                'ART not nearly enough to put future on historical track; multiplier tuned to global pattern
                SHlHIVMort(R%) = HLHIVMORT(R%) 'Save before multiplier; is flow
                HLHIVMORT(R%) = CSng(AMAX(HLHIVMORT(R%) * hlhivmortm(R%), 0.0001 * SHlHIVPrev(R%)))
                HLHIVMORT(R%) = HLHIVMORT(R%) * hlhivmortm(R%)
                HLHIVMORT(R%) = CSng(Amin(0.2 * POP(R%), HLHIVMORT(R%)))

                'HIV Prevalence, as stock - All Years but First
                '----------------------------------------------
                ' If RegName$(R%) = "Albania" Then Stop
                NormalDeaths = CDR(R%) / 1000 * SHlHIVPrev(R%)
                TotDeathsPrev = NormalDeaths + HLHIVMORT(R%)
                HLHIVPREV(R%) = AMAX(0, SHlHIVPrev(R%) + HLHIVINC(R%) - TotDeathsPrev)
                HLHIVPREV(R%) = CSng(Amin(HLHIVPREV(R%) * hlhivprevm(R%), 0.8 * POP(R%)))
                'no good ukntil have prevalence

                'HIV Variable Wrapping - All Years but First
                '-------------------------------------------
                SHlHIVPrev(R%) = HLHIVPREV(R%)

                'HIV Ratios - All Years but First
                '--------------------------------
                If HLHIVPREV(R%) <= 0 Then
                    HLHIVMORTPREVR(R%) = 0.04
                    HLHIVINCPREVR(R%) = 0.08
                Else
                    HLHIVMORTPREVR(R%) = HLHIVMORT(R%) / AMAX(0.000001, HLHIVPREV(R%))
                    HLHIVINCPREVR(R%) = HLHIVINC(R%) / AMAX(0.0000001, HLHIVPREV(R%))
                End If
            End If
            'Next R%
        End Sub

        'jrs 2019/05/17
        Private Sub UpdateHIV(R As Integer)
            'Replace Intentional Injury Mortality with values computed in the Societal Violence Module
            '-----------------------------------------------------------------------------------------
            Dim G As Integer, j As Integer
            Dim Weights(,) As Double
            ReDim Weights(NCohorts + 3, 2)
            For G = 1 To 2
                'Load Weights with Health module forecast
                '-----------------------------------------
                Weights(NCohorts + 1, G) = 0
                Weights(NCohorts + 2, G) = 0
                Weights(NCohorts + 3, G) = 0
                For j = 0 To NCohorts
                    Weights(j, G) = modmordstdet(j, R, 20 + G) * agedst(j, R, G)
                    Weights(NCohorts + 1, G) = Weights(NCohorts + 1, G) + modmordstdet(j, R, 20 + G) * agedst(j, R, G)
                    If j > 3 Then
                        Weights(NCohorts + 2, G) = Weights(NCohorts + 2, G) + modmordstdet(j, R, 20 + G) * agedst(j, R, G)
                    Else
                        Weights(NCohorts + 3, G) = Weights(NCohorts + 3, G) + modmordstdet(j, R, 20 + G) * agedst(j, R, G)
                    End If
                Next j
            Next G
            'Distribute deaths from HIV among entire population
            '----------------------------------------------------------------------------
            If (Weights(NCohorts + 1, 1) + Weights(NCohorts + 1, 2)) > 0 Then
                For G = 1 To 2
                    For j = 0 To NCohorts
                        If agedst(j, R, G) > 0 Then
                            If hlmortconstsw(R, 11) > 0 Then
                                modmordstdet(j, R, 20 + G) = smodmordstdet(j, R, 20 + G)
                            Else
                                modmordstdet(j, R, 20 + G) = HLHIVMORT(R) * 1000 / (agedst(j, R, G)) * Weights(j, G) / (Weights(NCohorts + 1, 1) + Weights(NCohorts + 1, 2))
                            End If
                        End If
                    Next j
                Next G
            End If
        End Sub

        'jrs 2019/05/17
        Private Sub UpdateMalaria(R As Integer)
            'Replace Intentional Injury Mortality with values computed in the Societal Violence Module
            '-----------------------------------------------------------------------------------------
            Dim G As Integer, j As Integer
            Dim Weights(,) As Double
            ReDim Weights(NCohorts + 3, 2)
            For G = 1 To 2
                'Load Weights with Health module forecast
                '-----------------------------------------
                Weights(NCohorts + 1, G) = 0
                Weights(NCohorts + 2, G) = 0
                Weights(NCohorts + 3, G) = 0
                For j = 0 To NCohorts
                    Weights(j, G) = modmordstdet(j, R, 24 + G) * agedst(j, R, G)
                    Weights(NCohorts + 1, G) = Weights(NCohorts + 1, G) + modmordstdet(j, R, 24 + G) * agedst(j, R, G)
                    If j > 3 Then
                        Weights(NCohorts + 2, G) = Weights(NCohorts + 2, G) + modmordstdet(j, R, 24 + G) * agedst(j, R, G)
                    Else
                        Weights(NCohorts + 3, G) = Weights(NCohorts + 3, G) + modmordstdet(j, R, 24 + G) * agedst(j, R, G)
                    End If
                Next j
            Next G
            'Distribute deaths from HIV among entire population
            '----------------------------------------------------------------------------
            If (Weights(NCohorts + 1, 1) + Weights(NCohorts + 1, 2)) > 0 Then
                For G = 1 To 2
                    For j = 0 To NCohorts
                        If agedst(j, R, G) > 0 Then
                            If hlmortconstsw(R, 13) > 0 Then
                                modmordstdet(j, R, 24 + G) = smodmordstdet(j, R, 24 + G)
                            Else
                                modmordstdet(j, R, 24 + G) = HLMALARIAMORT(R) * 1000 / (agedst(j, R, G)) * Weights(j, G) / (Weights(NCohorts + 1, 1) + Weights(NCohorts + 1, 2))
                            End If
                        End If
                    Next j
                Next G
            End If
        End Sub

        'jrs 2016/12/01
        Private Sub StuntingInit()
            'Use Initial Stunting pct of population
            'and distribute it proportionally in entire population distribution
            Dim i As Integer, j As Integer, G As Integer, k As Integer
            'For all countries
            For i = 1 To NR
                'jrs 2016/12/06
                'Stunting among infants
                '        If (agedst(0, i, 1) + agedst(0, i, 2)) > 0 Then
                '            StuntingByAge(0, i, 1) = (MALNCHP(i) + MALNCHPSAM(i) * malnchpsamtostunt) * POP(i) / hlstuntyrs * 10 * agedst(0, i, 1) / (agedst(0, i, 1) + agedst(0, i, 2))
                '            StuntingByAge(0, i, 2) = (MALNCHP(i) + MALNCHPSAM(i) * malnchpsamtostunt) * POP(i) / hlstuntyrs * 10 * agedst(0, i, 2) / (agedst(0, i, 1) + agedst(0, i, 2))
                '        End If
                'Stunting for Children
                For j = 0 To 3
                    'jrs 2021/05/13 removing SAM from initial year
                    'StuntingByAge(j, i, 1) = (MALNCHP(i) + MALNCHPSAM(i) * malnchpsamtostunt) * 10 * agedst(j, i, 1)
                    'StuntingByAge(j, i, 2) = (MALNCHP(i) + MALNCHPSAM(i) * malnchpsamtostunt) * 10 * agedst(j, i, 2)
                    'jrs 2021/06/17
                    If HLSTUNTCHL(i) = 0 Then
                        StuntingByAge(j, i, 1) = MALNCHP(i) * 10 * agedst(j, i, 1)
                        StuntingByAge(j, i, 2) = MALNCHP(i) * 10 * agedst(j, i, 2)
                    Else
                        StuntingByAge(j, i, 1) = HLSTUNTCHL(i) * 10 * agedst(j, i, 1)
                        StuntingByAge(j, i, 2) = HLSTUNTCHL(i) * 10 * agedst(j, i, 2)
                    End If

                    'jrs 2021/05/12
                    'One year stunting
                    If j = 0 Then
                        'jrs 2021/06/21 using fagedst to initialize, instead of agedst
                        If HLSTUNTCHL(i) = 0 Then
                            fStuntingByAge(0, i, 1) = MALNCHP(i) * 10 * fagedst(0, i, 1)
                            fStuntingByAge(0, i, 2) = MALNCHP(i) * 10 * fagedst(0, i, 2)
                        Else
                            fStuntingByAge(0, i, 1) = HLSTUNTCHL(i) * 10 * fagedst(0, i, 1)
                            fStuntingByAge(0, i, 2) = HLSTUNTCHL(i) * 10 * fagedst(0, i, 2)
                        End If
                    ElseIf j = 1 Then
                        'jrs 2021/06/17
                        If HLSTUNTCHL(i) = 0 Then
                            For k = 1 To 4
                                fStuntingByAge(k, i, 1) = MALNCHP(i) * 10 * fagedst(k, i, 1)
                                fStuntingByAge(k, i, 2) = MALNCHP(i) * 10 * fagedst(k, i, 2)
                            Next k
                        Else
                            For k = 1 To 4
                                'jrs 2021/06/17
                                fStuntingByAge(k, i, 1) = HLSTUNTCHL(i) * 10 * fagedst(k, i, 1)
                                fStuntingByAge(k, i, 2) = HLSTUNTCHL(i) * 10 * fagedst(k, i, 2)
                            Next k
                        End If
                    Else
                        'jrs 2021/06/17
                        If HLSTUNTCHL(i) = 0 Then
                            For k = 5 * (j - 1) To 5 * j - 1
                                fStuntingByAge(k, i, 1) = MALNCHP(i) * 10 * fagedst(k, i, 1)
                                fStuntingByAge(k, i, 2) = MALNCHP(i) * 10 * fagedst(k, i, 2)
                            Next k
                        Else
                            For k = 5 * (j - 1) To 5 * j - 1
                                'jrs 2021/06/17
                                fStuntingByAge(k, i, 1) = HLSTUNTCHL(i) * 10 * fagedst(k, i, 1)
                                fStuntingByAge(k, i, 2) = HLSTUNTCHL(i) * 10 * fagedst(k, i, 2)
                            Next k
                        End If
                    End If
                Next j

                'jrs 2016/12/06
                'Distribute Stunting across all ages and gender
                '        For j = 1 To NCohorts
                '            If (POP(i) - agedst(0, i, 1) - agedst(0, i, 2)) > 0 Then
                '                'For Males and Females
                '                For G = 1 To 2
                '                    'In Thousands: Stunt%/100 * Pop(in Millions)*1000 = Total Stunt in Thousands
                '                    'Total Stunts in Thousands * portion in age = Total Stunt in age
                '                    'POP cancels out in numerator  and denominator
                '                    StuntingByAge(j, i, G) = AMAX((HLSTUNT(i) * POP(i) * 10 - StuntingByAge(0, i, 1) - StuntingByAge(0, i, 2)), 0) * agedst(j, i, G) / (POP(i) - agedst(0, i, 1) - agedst(0, i, 2))
                '                Next G
                '            End If
                '        Next j

                If HLSTUNT(i) = 0 Then
                    'Stunting for Adults 15+
                    For j = 4 To NCohorts
                        'For Males and Females
                        For G = 1 To 2
                            StuntingByAge(j, i, G) = MALNCHP(i) * 10 * agedst(j, i, G) * 1.5
                            'jrs 2021/05/12
                            If j < NCohorts Then
                                For k = 5 * (j - 1) To 5 * j - 1
                                    fStuntingByAge(k, i, G) = MALNCHP(i) * 10 * fagedst(k, i, G) * 1.5
                                Next k
                            Else
                                k = 5 * (j - 1)
                                fStuntingByAge(k, i, G) = MALNCHP(i) * 10 * fagedst(k, i, G) * 1.5
                            End If
                        Next G
                    Next j
                Else
                    'Stunting for Adults 15+
                    For j = 4 To NCohorts
                        'For Males and Females
                        For G = 1 To 2
                            StuntingByAge(j, i, G) = HLSTUNT(i) * 10 * agedst(j, i, G)
                            'jrs 2021/05/12
                            If j < NCohorts Then
                                For k = 5 * (j - 1) To 5 * j - 1
                                    fStuntingByAge(k, i, G) = HLSTUNT(i) * 10 * fagedst(k, i, G)
                                Next k
                            Else
                                k = 5 * (j - 1)
                                fStuntingByAge(k, i, G) = HLSTUNT(i) * 10 * fagedst(k, i, G)
                            End If
                        Next G
                    Next j
                End If
            Next i
        End Sub

        'jrs 2014/03/13
        Public Sub IFsHealthCost(ByRef modmorbdstdet(,,) As Double, context As IForecastContext)
            If IY% = 1 Then

                ReDim BaseCostMorb(CInt(Ntypes / 2))
                ReDim BaseCostMort(CInt(Ntypes / 2))
                ReDim CostAdjFactorMorb(NR, CInt(Ntypes / 2))  'jrs 2014/05/08 added 2nd dimension
                ReDim CostAdjFactorMort(NR, CInt(Ntypes / 2))
                'jrs 2014/04/29 ReDim ResidualCostPerCap(NR) As Single
                ReDim BaseCostMorb2(CInt(Ntypes / 2))
                ReDim BaseCostMort2(CInt(Ntypes / 2))
                ReDim CostAdjFactorMorb2(NR, CInt(Ntypes / 2))
                ReDim CostAdjFactorMort2(NR, CInt(Ntypes / 2))
                'jrs 2014/05/26 ReDim HLEXPENDPRIVShift(NR) As Single
                ReDim bType2Countrys(NR)
                'jrs 2014/05/26 Dim PrivHLExp As Single
                'jrs 2014/05/06
                ReDim IHLCOSTOTHER(NR)
                'jrs 2014/05/07
                ReDim HLCOSTMORTShift(NR, CInt(Ntypes / 2))
                ReDim HLCOSTMORBShift(NR, CInt(Ntypes / 2))
                '        PrivHLExpPctGDPFxName = "GDP/Capita (PPP 2005) Versus Private Health Expenditures % of GDP (2012) Log"
                '        PrivHLExpPctGDPFxName = getFunctionNameYrOrMstRct(PrivHLExpPctGDPFxName, BaseYear)

                LoadBaseCosts()
                'jrs 2026/03/04
                If gRegionIdx("United States of America") > 0 Then
                    'jrs 2014/08/05 LoadType2Countries
                    USidx = context.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("United States of America")).Key
                Else
                    USidx = -1
                End If
                'jrs 2014/11/06
                If USidx < 0 Then
                    USidx = context.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("US-California")).Key
                End If

                'jrs 2014/05/26
                ReDim PrivExpToCost(NR%)
                ReDim SHLCOST(NR%), SHLCOST2(NR%), SHealthExpToCostGap(NR%)

                'jrs 2014/06/05
                hlCostageSt = 0
                If hllongvtystartage(1) > 0 Then
                    hlCostageSt = CInt(hllongvtystartage(1) / 5 + 1)
                End If
                If hllongvtystartage(2) > hllongvtystartage(1) Then
                    hlCostageSt = CInt(hllongvtystartage(2) / 5 + 1)
                End If
                If hllongvtystartage(3) > hllongvtystartage(2) Then
                    hlCostageSt = CInt(hllongvtystartage(3) / 5 + 1)
                End If
                'jrs 2014/06/09
                hlcostleyrperFirstYear = 0

            End If

            '
            '   ALL YEARS
            '   -------------------------------------------------------------------------

            'If IY + FirstYear - 1 = 2100 Then Stop
            LoadCostAdjFactor(USidx, context)
            Dim phaseInFact As Single
            'jrs 2014/06/09
            If hlcostleyrper > 0 Then
                If hlcostleyrperFirstYear = 0 Then
                    hlcostleyrperFirstYear = IY
                End If
                If hlcostletime > 0 Then
                    If IY >= 20 + hlcostleyrperFirstYear Then
                        phaseInFact = 1 / hlcostletime
                    Else
                        phaseInFact = 1 / hlcostletime * (IY - hlcostleyrperFirstYear + 1) / 21
                    End If
                Else
                    phaseInFact = 1
                End If
            End If

            Dim R As Integer, T As Integer, G As Integer, A As Integer
            Dim CostMorbRT As Single, CostMortRT As Single
            'jrs 2014/06/03
            Dim CostMorbT As Single, CostMortT As Single
            'gvdb 2024/04/05
            'unused variable
            Dim CostFactorM As Single ' , CostFactorF As Single
            'For each country
            For R = 1 To NR
                HLCOSTLE(R) = 0
                'jrs 2014/06/09
                HLCOSTLESUB(R) = 0
                'jrs 2014/06/03
                CostMorbT = 0
                CostMortT = 0

                'For each Disease Type
                For T = 1 To CInt(Ntypes / 2)
                    'If R = 12 And T = 3 And IY + FirstYear - 1 = 2100 Then Stop
                    'similar to the Netherlands (European for now)
                    If bType2Countrys(R) Then
                        CostMorbRT = BaseCostMorb2(T) * CostAdjFactorMorb2(R, T)
                        CostMortRT = BaseCostMort2(T) * CostAdjFactorMort2(R, T)
                    Else 'similar to the US (rest of the World)
                        CostMorbRT = BaseCostMorb(T) * CostAdjFactorMorb(R, T)
                        CostMortRT = BaseCostMort(T) * CostAdjFactorMort(R, T)
                    End If

                    HLCOSTMORB(R, T) = 0
                    HLCOSTMORT(R, T) = 0
                    'for Males and Females
                    For G = 1 To 2
                        'Morbidity Cost
                        '--------------
                        'jrs 2014/05/30
                        '                If HLYLL(R, T, G) <> 0 Then
                        '                    HLCOSTMORB(R, T) = HLCOSTMORB(R, T) + DEATHCAT(R, T, G) * HLYLD(R, T, G) / HLYLL(R, T, G) * CostMorbRT
                        '                Else
                        '                    For A = 0 To NCohorts
                        '                        HLCOSTMORB(R, T) = HLCOSTMORB(R, T) + modmorbdstdet(A, R, 2 * (T - 1) + G) * agedst(A, R, G) * CostMorbRT / 1000
                        '                    Next A
                        '                End If
                        For A = 0 To NCohorts
                            HLCOSTMORB(R, T) = CSng(HLCOSTMORB(R, T) + modmorbdstdet(A, R, 2 * (T - 1) + G) * agedst(A, R, G) * CostMorbRT / 1000)
                        Next A

                        'Mortality Cost
                        '--------------
                        HLCOSTMORT(R, T) = HLCOSTMORT(R, T) + DEATHCAT(R, T, G) * CostMortRT
                        'Life Extension Cost for Adults 30 or older
                        '------------------------------------------
                        'jrs 2014/06/03 no need to add up for each type
                        If T = 1 Then
                            For A = hlCostageSt To NCohorts 'jrs 2014/06/05 we were using 7 for Adults 30 or older, but acutally the treatment starts at 15 for CDs and 25 for NCDs and Injuries, so I'm using the max, in the base case 25
                                'jrs 2014/06/05 added phase in factor
                                'using Cost Adjust Factor to adapt cost of life extension treatment
                                'bbh 2014/06/07 start
                                CostFactorM = AMAX(0.05, Amin(1, CostAdjFactorMorb(R, T)))
                                '                       HLCOSTLE(R) = HLCOSTLE(R) + hlcostleyrper * CostAdjFactorMorb(R, T) * agedst(A, R, G) * phaseInFact 'jrs 2014/06/02 LifeExtensionCostPerCap * agedst(A, R, G)
                                HLCOSTLE(R) = CSng(HLCOSTLE(R) + hlcostleyrper * CostFactorM * agedst(A, R, G) * phaseInFact) 'jrs 2014/06/02 LifeExtensionCostPerCap * agedst(A, R, G)
                                'jrs 2014/06/09
                                HLCOSTLESUB(R) = CSng(HLCOSTLESUB(R) + hlcostleyrper * (1 - CostFactorM) * agedst(A, R, G) * phaseInFact)
                                'HLCOSTLE(R) = HLCOSTLE(R) + hlcostleyrper * agedst(A, R, G)
                            Next A
                        End If
                    Next G
                    'jrs 2014/05/07
                    'Shift factor to adjust countries where other cost was initially below the minimum
                    If IY > 1 Then
                        HLCOSTMORT(R, T) = HLCOSTMORT(R, T) * HLCOSTMORTShift(R, T)
                        HLCOSTMORB(R, T) = HLCOSTMORB(R, T) * HLCOSTMORBShift(R, T)
                    Else
                        HLCOSTMORTShift(R, T) = 1
                        HLCOSTMORBShift(R, T) = 1
                    End If
                    'jts 2014/06/03
                    CostMorbT = CostMorbT + HLCOSTMORB(R, T)
                    CostMortT = CostMortT + HLCOSTMORT(R, T)
                Next T
                'jrs 2014/04/29
                'Initialize Residual Cost
                '        If IY = 1 Then
                '            'Match Health Expenditures
                '            ResidualCostPerCap(R) = HLCOST(R) 'jrs 2014/04/29 we were doing GDS which is public only
                '            'for each disease type
                '            For T = 1 To Ntypes / 2
                '                ResidualCostPerCap(R) = ResidualCostPerCap(R) - HLCOSTMORB(R, T) - HLCOSTMORT(R, T)
                '            Next T
                '            ResidualCostPerCap(R) = ResidualCostPerCap(R) - HLCOSTLE(R)
                '            ResidualCostPerCap(R) = ResidualCostPerCap(R) / POP(R)
                '        End If

                'Other Costs
                '-----------
                'jrs 2014/04/29 HLCOSTOTHER(R) = ResidualCostPerCap(R) * POP(R)
                If IY = 1 Then
                    'In year 1, cost = expenditures, HLCOST = GDS + EXPPriv
                    HLCOSTOTHER(R) = GDS(R, Health) + HLEXPENDPRIV(R) 'HLCOST(R)
                    'For each Disease Type
                    For T = 1 To CInt(Ntypes / 2)
                        'Remove the cost of mortality and morbidity
                        HLCOSTOTHER(R) = HLCOSTOTHER(R) - HLCOSTMORT(R, T) - HLCOSTMORB(R, T)
                    Next T
                    'jrs 2014/06/03 IHLCOSTOTHER(R) = HLCOSTOTHER(R)
                    IHLCOSTOTHER(R) = HLCOSTOTHER(R) / (CostMortT + CostMorbT + HLCOSTLE(R))
                Else
                    'jrs 2014/06/03 HLCOSTOTHER(R) = IHLCOSTOTHER(R) * GDPPC(R) / GDPPCI(R)
                    HLCOSTOTHER(R) = IHLCOSTOTHER(R) * (CostMortT + CostMorbT + HLCOSTLE(R))
                    'jrs 2014/06/10
                    HLCOSTOTHER(R) = ConvergeOverTime(HLCOSTOTHER(R), hlcostotherrattarget * (CostMortT + CostMorbT + HLCOSTLE(R)), hlcostotherrattargetconv)
                End If

                'Total Cost
                '-----------
                HLCOST(R) = HLCOSTLE(R) + HLCOSTOTHER(R)
                'for each disease type
                For T = 1 To CInt(Ntypes / 2)
                    HLCOST(R) = HLCOST(R) + HLCOSTMORB(R, T) + HLCOSTMORT(R, T)
                Next T


                '        'Expenditures Side
                '        '=================
                '
                '        'Private Expenditures
                '        '--------------------
                '        'jrs 2014/05/07 Call AnalFunc("Private Health Expenditures % of GDP (2012)", Array(GDPPCP(R), EDYRSAG25(R, 3)), PrivHLExp)
                '        Call XYTABL(PrivHLExpPctGDPFxName, GDPPCP(R), PrivHLExp)
                '        If IY = 1 Then
                '            HLEXPENDPRIVShift(R) = HLEXPENDPRIV(R) / GDP(R) * 100 - PrivHLExp
                '        Else
                '            PrivHLExp = PrivHLExp + HLEXPENDPRIVShift(R)
                '            'Convert to absolute terms
                '            HLEXPENDPRIV(R) = PrivHLExp * GDP(R) / 100
                '        End If
                '
                '
                '        'Total Health Expenditures
                '        '-------------------------
                '        HLEXPEND(R) = GDS(R, Health) + HLEXPENDPRIV(R)

                'jrs 2014/04/29 HLCOST(R) = HLCOST(R) + HLCOSTLE(R) + HLCOSTOTHER(R)
                'jrs 2014/05/05
                '        Call XYTABL(PrivHLExpPctGDPFxName, GDS(R, Health) / GDP(R) * 100, PrivHLExp)
                '        PrivHLExp = AMAX(Amin(PrivHLExp, 35), 1)
                '        PrivHLExp = PrivHLExp / 100 * GDP(R)
                '        If IY = 1 Then
                '            HLEXPENDPRIVShift(R) = HLCOST(R) - PrivHLExp
                '        End If
                '        PrivHLExp = PrivHLExp + HLEXPENDPRIVShift(R)
                '        HLCOSTOTHER(R) = PrivHLExp - HLCOST(R)
                '        HLCOST(R) = PrivHLExp + HLCOSTLE(R)

                'jrs 2014/05/26 Save Total Cost for use in IFsIntlPol(1) which is run before Health
                'Balancing of expenditures vs  cost (demand)
                If IY > 1 Then
                    SHLCOST2(R) = SHLCOST(R)
                End If
                SHLCOST(R) = HLCOST(R)

                'jrs 2014/05/26
                'First Year: Find Private Expenditures to Total Cost ratio
                '==========================================================
                If IY = 1 Then
                    PrivExpToCost(R%) = HLEXPENDPRIV(R%) / HLCOST(R%)
                End If
                'jrs 2014/05/27
                SHealthExpToCostGap(R) = (HLEXPEND(R) - HLCOST(R)) / GDP(R)
                'If GRPS(1, R) = "India" Then Stop
            Next R

            'jrs 2014/05/07
            'Set a Minimum for Other Cost, using the value for the US
            If IY = 1 Then
                Dim diff As Single, MortCost As Single, MorbCost As Single
                For R = 1 To NR
                    'If R = 38 Then Stop 'congo
                    'If HLCOSTOTHER(R) < (HLCOSTOTHER(USidx) / GDP(USidx)) * GDP(R) / 4 Then
                    If HLCOSTOTHER(R) < (HLCOSTOTHER(USidx) / HLCOST(USidx)) * HLCOST(R) / 4 Then
                        'Diff = (HLCOSTOTHER(USidx) / GDP(USidx)) * GDP(R) / 4 - HLCOSTOTHER(R)
                        diff = HLCOST(R) - (HLCOSTOTHER(USidx) / HLCOST(USidx)) * HLCOST(R) / 4
                        'Need to adjust mortality and morbidity cost to match expenses in year 1
                        MortCost = 0
                        MorbCost = 0
                        'for each disease type
                        For T = 1 To CInt(Ntypes / 2)
                            MortCost = MortCost + HLCOSTMORT(R, T)
                            MorbCost = MorbCost + HLCOSTMORB(R, T)
                        Next T
                        'HLCOSTOTHER(R) = (HLCOSTOTHER(USidx) / GDP(USidx)) * GDP(R) / 4
                        HLCOSTOTHER(R) = (HLCOSTOTHER(USidx) / HLCOST(USidx)) * HLCOST(R) / 4
                        'jrs 2014/06/03 IHLCOSTOTHER(R) = HLCOSTOTHER(R)
                        CostMortT = 0
                        CostMorbT = 0
                        'for each disease type
                        For T = 1 To CInt(Ntypes / 2)
                            HLCOSTMORTShift(R, T) = diff / (MortCost + MorbCost)
                            HLCOSTMORT(R, T) = HLCOSTMORT(R, T) * HLCOSTMORTShift(R, T)
                            HLCOSTMORBShift(R, T) = diff / (MortCost + MorbCost)
                            HLCOSTMORB(R, T) = HLCOSTMORB(R, T) * HLCOSTMORBShift(R, T)
                            'jts 2014/06/03
                            CostMorbT = CostMorbT + HLCOSTMORB(R, T)
                            CostMortT = CostMortT + HLCOSTMORT(R, T)
                        Next T
                        'jrs 2014/06/03
                        IHLCOSTOTHER(R) = HLCOSTOTHER(R) / (CostMortT + CostMorbT + HLCOSTLE(R))
                        HLCOST(R) = CostMortT + CostMorbT + HLCOSTOTHER(R) + HLCOSTLE(R)
                        SHLCOST(R) = HLCOST(R)
                    End If
                Next R
            End If
        End Sub

        'jrs 2014/03/13
        Private Sub LoadBaseCosts()
            Using dbIFs As IFsContext = New IFsContext
                'The Units need to be in Thousand $
                For Each rs In dbIFs.HealthCostByType
                    If rs.CountryIdx = 1 Then 'US
                        BaseCostMorb(rs.TypeId) = rs.MorbidityCost
                        BaseCostMort(rs.TypeId) = rs.MortalityCost
                    Else 'Netherlands
                        BaseCostMorb2(rs.TypeId) = rs.MorbidityCost
                        BaseCostMort2(rs.TypeId) = rs.MortalityCost
                    End If
                Next
            End Using
        End Sub

        'jrs 2014/03/13
        Private Sub LoadCostAdjFactor(USidx As Integer, context As IForecastContext)
            'jrs 2014/05/07 description
            'this factor actually includes 2 factors
            '1. the first one is the difference between a given country and the US in year 1: GDPPCI(R)/GDPPCI(US)
            '2. the second one is the change in cost with time relative to year 1, which is: GDPPC(R)/GDPPCI(R)
            'when you multiply both terms, the GDPPCI cancels out, and you end up with GDPPC(R)/GDPPCI(US)
            'when then add and subtract GDPPCI to make the factor easier to manage with elasticiy:
            'GDPPC(R)/GDPPCI(US)= (GDPPC(R)-GDPPCI(US)+GDPPCI(US))/GDPPCI(US) = (GDPPC(R)-GDPPCI(US))/GDPPCI(US)+ 1
            'We will call this the delta term:
            'Delta = (GDPPC(R)-GDPPCI(US))/GDPPCI(US)
            'The elasticity will affect directly the delta term:
            'factor = 1 + Delta * elasticity
            Dim i As Integer, NEDidx As Integer, j As Integer
            NEDidx = context.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("Netherlands")).Key
            For i = 1 To NR
                For j = 1 To CInt(Ntypes / 2)
                    'jrs 2014/09/25
                    CostAdjFactorMorb(i, j) = 1 + (GDPPC(i) - GDPPCI(USidx)) / GDPPCI(USidx) * hlcostgrowthel(j)
                    CostAdjFactorMort(i, j) = 1 + (GDPPC(i) - GDPPCI(USidx)) / GDPPCI(USidx) * hlcostgrowthel(j)
                    CostAdjFactorMorb2(i, j) = 1 + (GDPPC(i) - GDPPCI(NEDidx)) / GDPPCI(NEDidx) * hlcostgrowthel(j)
                    CostAdjFactorMort2(i, j) = 1 + (GDPPC(i) - GDPPCI(NEDidx)) / GDPPCI(NEDidx) * hlcostgrowthel(j)
                    '            CostAdjFactorMorb(i, j) = 1 + (PPPCONV(i) - 1) * hlcostgrowthel(j)
                    '            CostAdjFactorMort(i, j) = 1 + (PPPCONV(i) - 1) * hlcostgrowthel(j)
                    '            CostAdjFactorMorb2(i, j) = 1 + (PPPCONV(i) - 1) * hlcostgrowthel(j)
                    '            CostAdjFactorMort2(i, j) = 1 + (PPPCONV(i) - 1) * hlcostgrowthel(j)
                Next j
            Next i
        End Sub

    End Module

End Namespace
