Imports IFs.Data
Imports System.Threading.Tasks
Imports System.Threading.Tasks.Dataflow
Imports System.Math

Namespace Models

    Partial Public Module Infrastructure
        'mti Testing Phone - end
#Region "Declare"

        Dim TbName$
        Dim MOBILshift(), BROADshift() As Single

        Dim ICTIndexShift() As Single


        'mti Infrastructure Computers September 2010 -start
        Dim SComputers(,) As Single
        'mti Infrastructure Computers September 2010 -end


        'mti 2015/04/29 - start
        Dim SICTIntNetHH(,) As Single

        'mti 2015/04/29 - end
        Dim SPENDshift() As Single

        'jrs 2009/06/05
        Dim SICTBroadExp(,) As Single
        Dim ICTBroadExpShift() As Single
        Dim ICTSpendExpShift() As Single
        'jrs 2011/01/17 begin Using Annual Growth Rate
        Dim ICTBroadGRExp() As Single?
        'jrs 2011/01/17 end Using Annual Growth Rate

        Public ICTBroadExp() As Single

        Public SINFRAELEC() As Single
        Public SINFRAROAD() As Single
        Public SINFRANET() As Single

        'mti April 26 2011 - start
        Public SINFRATELEOLD() As Single
        Public SINFRAINDTRAD() As Single
        Public SINFRAINDICT() As Single
        'mti April 26 2011 - end


        'mti May 2011 - start
        Public SSINFRAINDICT() As Single
        'mti May 2011 - end

        'mti October 20, 2011 -start
        Public SINFRAINDTRADDEM() As Single
        Public SINFRAINDICTDEM() As Single
        Public SSINFRAINDICTDEM() As Single
        'mti October 20, 2011 - end

        'mti Infrastructure August 2010 move watsan  - start
        Public SSANITATION(,) As Single
        Public SWATSAFE(,) As Single
        'mti Infrastructure August 2010 move watsan  - end

        'ttt-More-------------------------------
        Public SANITATIONShift(,) As Single
        Public WATSAFEShift(,) As Single

        Public SANITATION2(,) As Single
        Public SANITATIONShift2(,) As Single
        Public WATSAFE2(,) As Single
        Public WATSAFEShift2(,) As Single
        'jrs 2009/12/22
        Public sssanitation(,) As Single
        Public iWatSafeNone() As Single
        Public iSanitationHH() As Single

        Public DeppcntWS() As Double
        'ttt 2019/10/08 Double->Single
        Public sHHSIZEWS() As Single
        Public WATWASTEGR() As Single?, WATWASTETREATGR() As Single, SWATWASTETREAT() As Single, sWATWASTE() As Single, WatWasteTreatShift() As Single
        Public iWatWasteTreatCost() As Single
        Public WatSafeGR(,) As Single, SanitationGR(,) As Single
        Public PreMultWATWASTE() As Single ', sPreMultWATWASTE() As Single
        Public WatWasteColShift() As Single
        Public SANITATIONBF(,) As Single, WatSafeBF(,) As Single
        Public sSanitationBF(,) As Single, sWatSafeBF(,) As Single
        Public iSanitation(,) As Single
        Public WATWASTEBF() As Single, PreMultWATWASTEBF() As Single, WatWasteGRBF() As Single?
        Public WatWasteTreatBF() As Single
        Public sWATSAFEPreMult(,) As Single, sSanitationPreMult(,) As Single
        Public sPreBudSanitation(,) As Single, sPreBudWATSAFE(,) As Single

        'end ttt-More--------------------------------------
        'mti Dec 12, 2010 - start
        Dim sInfraBudImpact() As Double 'mti; change it to variant: so that you can check by 1; not by .99999
        'mti Dec 12, 2010 - end

        'mti 2022/09/30 start
        Dim sInfraBudImpact_WatSanNew() As Double   'parameter to save budget impact when the other one (sInfraBudImpact) is used as a flag (-9999); this is overall - all infra - budget impact though, not just WatSan
        'mti 2022/09/30 end

        'mti June 04, 2012 - start
        Dim ExtraFundTotal(), TotalDemandFromCandidates(), ExtraFundUsed() As Double
        Dim ExtraFundAllocated(,) As Double
        'mti June 04, 2012 - end

        'mti June 08, 2011 - start
        'jrs 2009/10/27
        Dim ENSOLFUELShift() As Single
        'jrs 2010/01/08

        'mti 2013/09/16 - Infra Fix -start
        Public ENSOLFUELI() As Single
        'mti 2013/09/16 - Infra Fix -end

        'mti June 08, 2011 - end

        Public INFRAINVESTNEWBF(,), INFRAINVESTMAINTBF(,) As Single

        'jrs 2013/09/16 *** begin ***
        'Infrastructure link to Poverty (GiniDom)
        'Public INFRAINDCOMBINED() As Single, SINFRAINDCOMBINED() As Single
        'jrs 2013/09/16 *** end ***

        'jrs 2013/12/06 *** begin ***
        Public sENSOLFUEL() As Single
        'jrs 2013/12/06 *** end ***

        'jrs 2015/03/19 *** begin ***
        Public WATERFILTEREDI(,) As Single
        'jrs 2015/03/19 *** end ***

        'sgh 2016/06/20
        Dim LandIrAreaEquipResid() As Single
        '======ttt MORE
        Dim PreMULTINFRAELECACC(,) As Single
        Dim PreMultLANDIRAREAEQUIP(), PreMultINFRAROAD(), PreMultINFRAROADPAVEDPCNT(), PreMultINFRATELE() As Single
        Dim PreMultEnElecShrEnDem() As Single
        Dim PreMultINFRAELECTRANLOSS() As Single

        Dim INFRAROADRUnConstrained() As Single?
        Dim INFRAELECGENCAPRUnConstrained() As Single?

        Dim EnDemDFRI() As Single 'Initial ratio of Energy demand data and forecast

        's-prefixed aariables usually save previous year values
        Dim sINFRAROADRAI() As Single, sINFRAROADDEM() As Single

        Dim sDesiredINFRAELECACCTotal() As Single
        Dim sINFRAELECGENCAP() As Single
        Dim sInfraElecGenCapTot() As Single
        Dim sinfraroadpavedpcnt() As Single
        Dim SICTMobil() As Single
        Dim SICTBroad() As Single
        Dim SICTBroadMobil() As Single

        'mti 2014/05/28 - start
        'Dim SMobil(NR, 2), SBroad(NR, 2), SBroadMobil(NR, 2)
        Dim SMobil(,), SBroadMobil(,) As Single
        Public SBroad(,) As Single

        Dim sPreBudINFRAROAD() As Single, sPreBudINFRATELE() As Single

        Dim sPreBudINFRAELECGENCAP() As Single
        'mti 2014/05/28 - end

        Dim slandirareaequip() As Single


        'Household size
        Dim sHHSIZE() As Single

        'Road access target bound
        Dim trgtINFRAROADRAI() As Single

        'i-prefixed aariables usually save initial year values
        Dim iINFRAROADRAI() As Single
        Dim iICTMOBIL() As Single
        Dim iICTBroad() As Single
        Dim iICTBROADMOBIL() As Single
        Dim iINFRAElecACC(,) As Single

        Dim iINFRATELE() As Single

        'save first year only (singl/flt cost on which multiplier is applied
        Dim iLandIrCost() As Single
        Dim iInfraelecadjfact() As Single?
        Dim iINFRAELECCONSPRODRATIO() As Single
        Dim iINFRAELECCON4RATIO() As Single
        Dim iINFRAELECPROD4RATIO() As Single

        Dim iFossilshare() As Single, iNonFossilshare() As Single
        'shift-suffixed variables hold initial year gap between data and forecast; RI-suffixed variables save the ratio of data and forecast in first year
        Dim INFRAROADRAIShift() As Single
        Dim INFRAELECGenCapShift() As Single
        Dim INFRAELECTRANLOSSShift() As Single
        Dim INFRARoadPavedPcntShift() As Single
        Dim INFRAELECACCShift(,) As Single
        Dim ICTComputerShift() As Single

        'mti 2015/04/29 - start
        Dim ICTIntNetHHShift() As Single

        'mti 2015/04/29 - end
        Dim BroadMOBILshift() As Single
        Dim TeleDensSaturation(), InfraTeleGRMax() As Single
        Public InfraTeleGR() As Single?
        Dim RoadDensShift(), RoadDensRI(), TeleDensRI(), InfraElecRI(), InfraNetRI() As Single

        'Depreciation rates for various types of infrastructure
        Dim Deppcnt() As Double
        'gr- or R-suffixed variables are usually growth rates
        Dim InfraElecTranLossGR() As Single?
        Public ICTMobilGR() As Single?, ICTBroadGR() As Single?
        Public ICTBroadMobilGR() As Single?
        Dim sInfraRoadPavedPcntR() As Single?
        Dim InfraRoadGR(), InfraRoadPavedPcntGR() As Single?, InfraElecGenCapGR() As Single?

        Dim LandIrAreaEquipGR() As Single?

        Dim SINFRATELE() As Double

        'SS-prefixed variables are usually values from two years back
        Dim SSINFRATELE() As Double

        'This is legacy code: Old telephone model (talk to BBH to see if this can be deleted)
        Dim INFRATELEMin() As Single

        'save independent variables in this module to avoid run to run from call sequence mix-up (revisit this)
        Dim sgdpp(), sLANDAREA(), sGDPPCP() As Single

        Dim InfraElecGR() As Single

        Dim EnElecShrEnDemGR() As Single?, sENELECSHRENDEM() As Single

        Dim ssINFRAELECACC(NR%, 3) As Single
        Public sINFRAELECACC(NR%, 3), SEnDem() As Single 'jrs 2021/02/04 made public

        Dim sGDSInfraInvest() As Single, GDSInfraBox() As Double

        'extrapolative growth calculation: parameters for positional and functional convergence
        Dim iextmaposnconvtimeenelecshr() As Single, iextmafuncenelecshr() As Single


        'BF-suffixed variables holds 'base path' forecast values of physical variables
        Dim InfraRoadBF() As Single, InfraRoadPavedPcntBF() As Single, INFRAROADRAIBF() As Single
        Dim sINFRAROADBF() As Single, sInfraRoadPavedPcntBF() As Single, sInfraRoadRAIBF() As Single
        Dim sInfraRoadPavedPcntRBF() As Single?

        Dim InfraElecGenCapBF() As Single
        Dim infraelecaccbf(,) As Single
        Dim sInfraElecGenCapBF() As Single
        Dim sINFRAELECACCBF(,) As Single
        Dim InfraElecGenCapGRBF() As Single?, sInfraElecGenCapTotBF() As Single



        Dim ICTMobilBF() As Single, ICTBroadBF() As Single, ICTBroadMobilBF() As Single
        Dim sICTMobilBF() As Single, sICTBroadBF() As Single, sICTBroadMobilBF() As Single

        Dim ICTMobilGRBF() As Single?, ICTBroadGRBF() As Single?, ICTBroadMobilGRBF() As Single?

        Dim sMobilBF(,), sBroadBF(,), SBroadMobilBF(,) As Single

        Dim sDesiredINFRAELECACCTotalBF() As Single


        'mti 2014/05/28 - start
        Dim EnElecShrEnDemGRBF() As Single?, EnElecShrEnDemBF() As Single, sENDEMSH() As Single


        Dim iInfraInvestNewPubShr(,) As Single

        Dim InfraRoadGRShift() As Single
        'mti 2014/05/28 - end
        Dim sINCOMELT1CS_Pcnt As Single 'ttt NewMade
        Dim cVal As Single 'ttt NewMade
        Dim Prediction As Single 'ttt NewMade
        Dim Prediction1 As Single 'ttt NewMade
        Dim watsanconv As Single = 75
        Dim IncomeDensityLand As Single?, TotalRoadsDensityPop As Single?, infraroadraicalc As Single 'ttt NewMade
        Dim TeleDens, mobilVal, BroadVal, BroadMobilVal As Single
        Public PrevsErrorSub As String = "" 'ttt 

        Dim ENVPREC() As Single
        'ttt 2019/10/08 
        Dim GDPPCPGR() As Single
        'jrs 2019/11/25
        'jrs 2019/11/26 Dim ilandirareasat() As Single

#End Region
        'mti 2022/06/21
        Dim SanitHHShift(,,) As Single, WATERHHShift(,,) As Single
        Dim iWaterHH(,,) As Single, iSANITHH(,,) As Single
        Public sWaterHH(,,) As Single, sSanitHH(,,) As Single
        Dim waterhhlifelowcost(), waterhhlifehighcost() As Single
        Dim sanithhlifelowcost(), sanithhlifehighcost() As Single
        Dim Loc_Str As String
        Dim Deppcnt2 As Single
        Dim sWatSafeThousands As Single
        Dim Poverty As Single
        Dim OddRat(5) As Single?
        'mti 2022/08/17
        Dim WaterHH2(,,) As Single, WaterHHShift2(,,) As Single
        Dim SanitHH2(,,) As Single, SanitHHShift2(,,) As Single
        'mti 2022/09/06 start
        Dim WaterHHBF(,,), SanitHHBF(,,) As Single
        Dim WaterHHInvestCapBF(,,), WaterHHInvestMaintBF(,,) As Single
        Dim SanitHHInvestCapBF(,,), SanitHHInvestMaintBF(,,) As Single
        'mti 2022/09/06 end

        'mti 2022/09/21 start
        Dim SanitHH_Funding_Gap(,,) As Single
        Dim WaterHH_Funding_Gap(,,) As Single
        'mti 2022/09/21 end

        'mti 2022/09/27
        Dim sSANITHHModel(,,) As Single
        Dim WaterHHGR(,,) As Single
        Dim SanitHHGR(,,) As Single

        'mti 2022/09/29 start
        Dim sSANITHHModelPlus(,,) As Single
        Dim sWATERHHModelPlus(,,) As Single
        Dim sWATERHHModel(,,) As Single
        'mti 2022/09/29 end

        'mti 2022/10/20 start
        Dim sInfraBudImpactByType(,) As Single
        'mti 2022/10/20 end

        'mti 2022/10/27 start
        Dim sWaterHHBF(,,), sSanitHHBF(,,) As Single
        'mti 2022/10/27 end

        'mti 2022/11/23 start
        Dim WaterHHSafeTrgtStartValue(,) As Single?
        Dim SANITHHSafeTrgtStartValue(,) As Single?
        Dim WaterHHSafeTrgtStartYr(,) As Integer
        Dim SANITHHSafeTrgtStartYr(,) As Integer

        Dim WaterHHBasicTrgtStartValue(,) As Single?
        Dim SanitHHBasicTrgtStartValue(,) As Single?
        Dim WaterHHBasicTrgtStartYr(,) As Integer
        Dim SanitHHBasicTrgtStartYr(,) As Integer
        'mti 2022/11/23 end

        Public Sub IFsInfraWATSANNewFirstYear(context As IForecastContext)
            'mti 2022/06/21 New Proc

            Dim sErrorSubPrIF = "IFsInfraWatSanNew1: "
            watsanconv = 75 '50

            'If IY% > 1 Then GoTo AllButFirst

            Dim Loc%, Srvc%
            ReDim SanitHHShift(NR%, 5, NLocation)
            ReDim WATERHHShift(NR%, 5, NLocation)

            ReDim iWaterHH(NR%, 5, NLocation)
            ReDim iSANITHH(NR%, 5, NLocation)

            'mti 2022/11/23 start
            ReDim WaterHHSafeTrgtStartValue(NR%, NLocation)
            ReDim SANITHHSafeTrgtStartValue(NR%, NLocation)
            ReDim WaterHHSafeTrgtStartYr(NR%, NLocation)
            ReDim SANITHHSafeTrgtStartYr(NR%, NLocation)


            ReDim WaterHHBasicTrgtStartValue(NR%, NLocation)
            ReDim SanitHHBasicTrgtStartValue(NR%, NLocation)
            ReDim WaterHHBasicTrgtStartYr(NR%, NLocation)
            ReDim SanitHHBasicTrgtStartYr(NR%, NLocation)
            'mti 2022/11/23 end

            ReDim sWaterHH(NR%, 5, NLocation)
            ReDim sSanitHH(NR%, 5, NLocation)

            ReDim waterhhlifelowcost(5), waterhhlifehighcost(5)
            ReDim sanithhlifelowcost(5), sanithhlifehighcost(5)

            'mti 2022/08/17 start
            ReDim WaterHH2(NR%, 5, NLocation), WaterHHShift2(NR%, 5, NLocation)
            ReDim SanitHH2(NR%, 5, NLocation), SanitHHShift2(NR%, 5, NLocation)
            'mti 2022/08/17 End

            'mti 2022/09/06 start
            ReDim WaterHHBF(NR%, 5, NLocation), SanitHHBF(NR%, 5, NLocation)
            ReDim WaterHHInvestCapBF(NR%, 5, NLocation), WaterHHInvestMaintBF(NR%, 5, NLocation)
            ReDim SanitHHInvestCapBF(NR%, 5, NLocation), SanitHHInvestMaintBF(NR%, 5, NLocation)
            'mti 2022/09/06 end

            'mti 2022/09/21 start
            ReDim SanitHH_Funding_Gap(NR%, 5, NLocation)
            ReDim WaterHH_Funding_Gap(NR%, 5, NLocation)
            'mti 2022/09/21 end

            'ReDim sPOPRURAL(NR%)

            'mti 2022/09/29 start
            ReDim sSANITHHModelPlus(NR%, 5, NLocation)
            ReDim sWATERHHModelPlus(NR%, 5, NLocation)
            ReDim sWATERHHModel(NR%, 5, NLocation)
            'mti 2022/09/29 end

            'mti 2022/10/27 Start
            ReDim sWaterHHBF(NR%, 5, NLocation)
            ReDim sSanitHHBF(NR%, 5, NLocation)
            'mti 2022/10/27 End

            'mti 2022/09/27 start
            'gvdb 2024/04/05
            'unused variable
            'Dim COVIDShk_WS As Integer
            ReDim sSANITHHModel(NR%, 5, NLocation)
            ReDim WaterHHGR(NR%, 5, NLocation)
            ReDim SanitHHGR(NR%, 5, NLocation)
            If context.HistoricMode = False Then
                Call LoadGrowthRatesThreeDim("WATERHHGR", WaterHHGR, context)
                Call LoadGrowthRatesThreeDim("SANITHHGR", SanitHHGR, context)
            End If
            'mti 2022/09/27 end

            '----- lifespan Water ----------------
            waterhhlifelowcost(SafelyManaged) = waterhhlifepiped
            waterhhlifehighcost(SafelyManaged) = waterhhlifepiped

            waterhhlifelowcost(Basic) = waterhhlifedugwell
            waterhhlifehighcost(Basic) = waterhhlifeboreholetubewell

            waterhhlifelowcost(Limited) = waterhhlifedugwell


            '----- Lifetime Sanitation ----------------
            sanithhlifelowcost(SafelyManaged) = sanithhlifesst
            sanithhlifehighcost(SafelyManaged) = sanithhlifesst

            sanithhlifelowcost(Basic) = sanithhlifepitlatrinebasic
            sanithhlifehighcost(Basic) = sanithhlifepitlatrinebasic

            sanithhlifelowcost(Limited) = sanithhlifepitlatrinebasic

            sanithhlifelowcost(Unimproved) = sanithhlifepitlatrinetrad  'rural; for ODF


            For R% = 1 To NR%

                sErrorSub = sErrorSubPrIF & ": Yr (" & CStr(IY) & ")" & ": Country: " & GRPS(1, R%)

                '
                '   First Year:  Water Connections: Shift Factor
                '   ---------------------------------------------
                'jrs 2022/11/21
                If ConstantDollarsYear = 2011 Then
                    Poverty = INCOMELT190LN(R%) / POP(R%) * 100
                ElseIf ConstantDollarsYear = 2017 Then 'jrs 2025/09/12
                    Poverty = INCOMELT215LN(R%) / POP(R%) * 100
                Else 'jrs 2025/09/12
                    Poverty = POVEXTREME(R%) / POP(R%) * 100
                End If
                Dim Income = GDPPCP(R%)
                Dim Education = EDYRSAG25(R%, Total)    'gdpvar, edvar, Poverty

                For Loc% = 1 To 2   'Rural and Urban

                    If Loc% = Urban Then Loc_Str = "Urban"
                    If Loc% = Rural Then Loc_Str = "Rural"

                    Call AnalFunc("Water Services, Fractional Logit, Surface Water, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Surface) = CSng(Exp(cVal))

                    Call AnalFunc("Water Services, Fractional Logit, Unimproved Source, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Unimproved) = CSng(Exp(cVal))

                    Call AnalFunc("Water Services, Fractional Logit, Limited Service, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Limited) = CSng(Exp(cVal))

                    Call AnalFunc("Water Services, Fractional Logit, Basic Service, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Basic) = CSng(Exp(cVal))

                    OddRat(SafelyManaged) = 1

                    Dim Denom As Single? = 1
                    For Srvc% = 1 To 4  '#5, safely managed is the base category of the logistic regresiion
                        Denom = Denom + OddRat(Srvc%)
                    Next
                    Denom = CSng(AMAX(0.001, Denom))

                    For Srvc% = 1 To 5
                        'If RegName(R%) = "Bangladesh" And Srvc% = SafelyManaged Then Stop

                        Dim WATERHHComp = AMAX(Amin(100, OddRat(Srvc) / Denom * 100), 0)
                        WATERHHShift(R%, Srvc%, Loc%) = CSng(WATERHH(R%, Srvc%, Loc%) - WATERHHComp)

                        iWaterHH(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%)
                        'mti 2022/08/17 start
                        'sWATERHH(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%)
                        'mti 2022/08/17 End
                    Next

                Next Loc%

                'mti 2022/08/17 start
                'National
                '        For Srvc% = 1 To 5
                '           WATERHH(R%, Srvc%, Total) = (WATERHH(R%, Srvc%, Rural) * (POP(R%) - POPURBAN(R%)) + WATERHH(R%, Srvc%, Urban) * POPURBAN(R%)) / AMAX(0.0001, POP(R%))
                '        Next
                'mti 2022/08/17 End

                For Loc% = 1 To 3
                    For Srvc% = 1 To 5
                        WATERHHDEM(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%)
                    Next Srvc%
                Next Loc%

                'mti 2022/09/12 start
                'mti 2022/09/21 start
                For Loc% = 1 To 3       '2       '3
                    For Srvc% = 1 To 5
                        WATERHHDEM(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%)
                        WaterHHBF(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%)
                        'mti 2022/10/04 start
                        sWATERHHModel(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%)
                        sWATERHHModelPlus(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%)
                        'mti 2022/10/04 end

                        'mti 2022/10/27 Start
                        sWaterHHBF(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%)
                        'mti 2022/10/27 End

                    Next Srvc%
                Next Loc%
                'mti 2022/09/21 End
                'mti 2022/09/12 end

                '******** End Water  ***************

                '
                '   First Year:  Sanitation Connections: Shift Factor
                '   ------------------------------------------------

                '
                '   First Year:  Water Connections: Shift Factor
                '   ---------------------------------------------



                For Loc% = 1 To 2   'Rural and Urban

                    If Loc% = Urban Then Loc_Str = "Urban"
                    If Loc% = Rural Then Loc_Str = "Rural"

                    Call AnalFunc("Sanitation Services, Fractional Logit, Open Defecation, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Surface) = CSng(Exp(cVal))

                    Call AnalFunc("Sanitation Services, Fractional Logit, Unimproved Services, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Unimproved) = CSng(Exp(cVal))

                    Call AnalFunc("Sanitation Services, Fractional Logit, Limited Service, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Limited) = CSng(Exp(cVal))

                    Call AnalFunc("Sanitation Services, Fractional Logit, Basic Service, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Basic) = CSng(Exp(cVal))

                    OddRat(SafelyManaged) = 1

                    Dim Denom As Single = 1
                    For Srvc% = 1 To 4  '#5, safely managed is the base category of the logistic regresiion
                        Denom = Denom + CSng(OddRat(Srvc%))
                    Next
                    Denom = AMAX(0.001, Denom)

                    For Srvc% = 1 To 5
                        Dim SanitHHComp = AMAX(Amin(100, OddRat(Srvc) / Denom * 100), 0)
                        SanitHHShift(R%, Srvc%, Loc%) = CSng(SANITHH(R%, Srvc%, Loc%) - SanitHHComp)
                        iSANITHH(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%)
                        'mti 2022/08/17 start
                        'sSANITHH(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%)
                        'mti 2022/08/17 End
                    Next

                Next Loc%

                'mti 2022/09/12 start
                'mti 2022/09/21 start
                For Loc% = 1 To 3           '2   '3
                    For Srvc% = 1 To 5
                        SANITHHDEM(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%)
                        SanitHHBF(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%)
                        'mti 2022/09/27 start
                        sSANITHHModel(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%)
                        'mti 2022/09/27 end
                        'mti 2022/09/29 start
                        sSANITHHModelPlus(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%)
                        'mti 2022/09/29 end

                        'mti 2022/10/27 Start
                        sSanitHHBF(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%)
                        'mti 2022/10/27 End

                    Next Srvc%
                Next Loc%
                'mti 2022/09/21 End
                'mti 2022/09/12 end



                '******** End Sanitation  ***************

                'mti 2022/11/23 start
                'Initializing variables for target start year and sttart value
                For Loc% = 1 To 3           '2   '3
                    WaterHHSafeTrgtStartValue(R%, Loc) = Nothing
                    SANITHHSafeTrgtStartValue(R%, Loc) = Nothing
                    WaterHHSafeTrgtStartYr(R%, Loc) = 0
                    SANITHHSafeTrgtStartYr(R%, Loc) = 0

                    WaterHHBasicTrgtStartValue(R%, Loc) = Nothing
                    SanitHHBasicTrgtStartValue(R%, Loc) = Nothing
                    WaterHHBasicTrgtStartYr(R%, Loc) = 0
                    SanitHHBasicTrgtStartYr(R%, Loc) = 0
                Next Loc%
                'mti 2022/11/23 end


            Next R%

            IFsInfraWATSANNewAllYears(context)

        End Sub

        Public Sub IFsInfraWATSANNewAllbutFirst(context As IForecastContext)
            '    On IFUN GoTo call1, call2
            'call1:
            Dim Loc%, Srvc%
            Dim sErrorSubPrIF = "IFsInfraWatSanNew1: "

            'mti 2022/09/27 start
            Dim COVIDShk_WS = 0
            If IY% - 1 + FirstYear = 2020 And gdprext(USAidx) < 0 Then COVIDShk_WS = 1
            If IY% - 1 + FirstYear = 2021 And gdprext(USAidx) < 0 Then COVIDShk_WS = 1
            '    If COVIDShk_WS = 1 Then
            '        For R% = 1 To NR%
            '            gdsbudgetprotec(R%, Infra) = 0.8
            '        Next
            '    End If
            'mti 2022/09/27 End

            For R% = 1 To NR%

                sErrorSub = sErrorSubPrIF & ": Yr (" & CStr(IY) & ")" & ": Country: " & GRPS(1, R%)
                '
                '   All But First:  Water Connections:
                '   ---------------------------------------------

                'RuralPop = (sPOPTotal(R%) - SPOPURBAN(R)) / sPOPTotal(R%) * 100
                'jrs 2022/11/21
                If ConstantDollarsYear = 2011 Then
                    Poverty = SINCOMELT190LN(R%) / sPOPTotal(R%) * 100
                ElseIf ConstantDollarsYear = 2017 Then 'jrs 2025/09/12
                    Poverty = SINCOMELT215LN(R%) / sPOPTotal(R%) * 100
                Else 'jrs 2025/09/12
                    Poverty = SINCOMELT300LN(R%) / sPOPTotal(R%) * 100
                End If
                Dim Income = sGDPPCP(R%)
                Dim Education = SEDYRSAG25(R%, Total)

                For Loc% = 1 To 2

                    If Loc% = Urban Then Loc_Str = "Urban"
                    If Loc% = Rural Then Loc_Str = "Rural"

                    Call AnalFunc("Water Services, Fractional Logit, Surface Water, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Surface) = CSng(Exp(cVal))

                    Call AnalFunc("Water Services, Fractional Logit, Unimproved Source, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Unimproved) = CSng(Exp(cVal))

                    Call AnalFunc("Water Services, Fractional Logit, Limited Service, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Limited) = CSng(Exp(cVal))

                    Call AnalFunc("Water Services, Fractional Logit, Basic Service, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Basic) = CSng(Exp(cVal))

                    OddRat(SafelyManaged) = 1

                    Dim Denom As Single = 1
                    For Srvc% = 1 To 4  '#5, safely managed is the base category of the logistic regresiion
                        Denom = Denom + CSng(OddRat(Srvc%))
                    Next
                    Denom = AMAX(0.001, Denom)
                    ' If RegName(R%) = "Maldives" And Loc = Rural And IY >= 4 Then Stop
                    Dim SumWaterHH As Single = 0
                    'mti 2022/08/13 start
                    For Srvc% = 1 To 5
                        'mti 2022/09/12 Start
                        'WaterHHComp = AMAX(Amin(100, OddRat(Srvc) / denom * 100), 0)
                        'WATERHH(R%, Srvc%, Loc%) = WaterHHComp + ConvergeOverTime(WaterHHShift(R%, Srvc%, Loc%), 0, watsanconv)

                        Dim WaterHHComp = AMAX(Amin(100, OddRat(Srvc) / Denom * 100), 0)
                        'mti 2022/09/29 start
                        Dim WaterHHconv% = 50   'watsanconv
                        'mti 2022/09/29 End

                        'mti 2022/09/21 start
                        'WATERHH(R%, Srvc%, Loc%) = AMAX(0, Amin(100, WATERHHcomp + ConvergeOverTime(WATERHHShift(R%, Srvc%, Loc%), 0, watsanconv)))
                        If WATERHHShift(R%, Srvc%, Loc%) > 0 Then
                            'data greater than estimate; keep data, slow down convergence for better services
                            If Srvc% = 5 Then
                                WATERHH(R%, Srvc%, Loc%) = CSng(AMAX(0, Amin(100, WaterHHComp + ConvergeOverTime(WATERHHShift(R%, Srvc%, Loc%), 0, 1000))))
                            Else
                                'mti 2022/09/29 start
                                WATERHH(R%, Srvc%, Loc%) = CSng(AMAX(0, Amin(100, WaterHHComp + ConvergeOverTime(WATERHHShift(R%, Srvc%, Loc%), 0, WaterHHconv%))))  'watsanconv
                                'mti 2022/09/29 End
                            End If
                        Else
                            If Srvc% = 5 Then
                                'mti 2022/09/29 start
                                WATERHH(R%, Srvc%, Loc%) = CSng(AMAX(0, Amin(100, WaterHHComp + ConvergeOverTime(WATERHHShift(R%, Srvc%, Loc%), 0, WaterHHconv%))))
                                'mti 2022/09/29 End
                            Else
                                WATERHH(R%, Srvc%, Loc%) = CSng(AMAX(0, Amin(100, WaterHHComp + ConvergeOverTime(WATERHHShift(R%, Srvc%, Loc%), 0, 1000))))
                            End If
                        End If
                        'mti 2022/09/21 end

                        'mti 2022/09/29 start
                        Dim WaterHH_Model = WATERHH(R%, Srvc%, Loc%) 'this now hold current year's model-based estimate
                        If WaterHHGR(R%, Srvc%, Loc%) <> -9999 And sWATERHHModel(R%, Srvc%, Loc%) <> 0 Then

                            Dim MA_Fct = 0.1
                            Dim CurGr As Single
                            If COVIDShk_WS = 0 Then
                                CurGr = CSng(AMAX(-0.1, Amin(0.1, MA_Fct * (WaterHH_Model / sWATERHHModel(R%, Srvc%, Loc%) - 1) + (1 - MA_Fct) * WaterHHGR(R%, Srvc%, Loc%))))
                                WaterHHGR(R%, Srvc%, Loc%) = CurGr
                            Else
                                CurGr = CSng(AMAX(-0.1, Amin(0.1, 0.4 * (WaterHH_Model / sWATERHHModel(R%, Srvc%, Loc%) - 1) + 0.6 * WaterHHGR(R%, Srvc%, Loc%))))
                            End If

                            'WaterHH(R%, Srvc%, Loc%) = Amin(100, AMAX(0, sWaterHHModel(R%, Srvc%, Loc%) * (1 + CurGr)))
                            WATERHH(R%, Srvc%, Loc%) = Amin(100, AMAX(0, sWATERHHModelPlus(R%, Srvc%, Loc%) * (1 + CurGr)))
                            'mti 2022/10/04 start
                        Else
                            If WaterHHGR(R%, Srvc%, Loc%) <> -9999 Then
                                WaterHHGR(R%, Srvc%, Loc%) = CSng(0.85 * WaterHHGR(R%, Srvc%, Loc%))   'smoother convergence of growth rate to zero
                                WATERHH(R%, Srvc%, Loc%) = Amin(100, AMAX(0, sWATERHHModelPlus(R%, Srvc%, Loc%) * (1 + WaterHHGR(R%, Srvc%, Loc%))))
                            End If
                            'mti 2022/10/04 End

                        End If
                        sWATERHHModel(R%, Srvc%, Loc%) = WaterHH_Model
                        sWATERHHModelPlus(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%)


                        SumWaterHH = SumWaterHH + WATERHH(R%, Srvc%, Loc%)
                    Next
                    For Srvc% = 1 To 5
                        'normalize to 100%
                        WATERHH(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%) / AMAX(0.0001, SumWaterHH) * 100
                    Next
                    'mti 2022/09/29 end

                    'mti 2022/09/21 Start
                    WATERHH(R%, SafelyManaged, Loc%) = AMAX(WaterHHBF(R%, SafelyManaged, Loc%) - 2, Amin(WaterHHBF(R%, SafelyManaged, Loc%) + 5, WATERHH(R%, SafelyManaged, Loc%)))
                    Dim SumWaterHH4Req = 100 - WATERHH(R%, SafelyManaged, Loc%)
                    Dim SumWaterHH4 As Single = 0
                    For Srvc% = 1 To 4
                        SumWaterHH4 = SumWaterHH4 + WATERHH(R%, Srvc%, Loc%)
                    Next
                    For Srvc% = 1 To 4
                        'mti 2022/09/26 start
                        If SumWaterHH4 > 0 Then
                            'normalize
                            WATERHH(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%) / SumWaterHH4 * SumWaterHH4Req
                        ElseIf SumWaterHH4Req > 0 Then
                            'all (lower) 4 cat are 0; but safelymnaged not 0
                            WATERHH(R%, Srvc%, Loc%) = SumWaterHH4Req / 4
                        Else
                            'all 4 0, safelymanaged 100
                            WATERHH(R%, Srvc%, Loc%) = 0
                        End If
                        'mti 2022/09/26 End
                    Next
                    'mti 2022/09/21 end

                    'mti 2022/09/06 start
                    'Save base projections before multipler and target parameters
                    For Srvc% = 1 To 5
                        WaterHHBF(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%)
                    Next

                    'Apply scenario paprameter - multiplier base 1   'Note - mutiplier on national will be implemented as same multiplier on rural and urban,
                    SumWaterHH = 0
                    For Srvc% = 1 To 5
                        'mti 2022/09/12 Start
                        WATERHH(R%, Srvc%, Loc%) = Amin(100, WATERHH(R%, Srvc%, Loc%) * If(waterhhm(R%, Srvc%, National) <> 1, waterhhm(R%, Srvc%, National), waterhhm(R%, Srvc%, Loc%)))
                        'mti 2022/09/12 End
                        SumWaterHH = SumWaterHH + WATERHH(R%, Srvc%, Loc%)
                    Next
                    For Srvc% = 1 To 5 'normalize to 100%
                        WATERHH(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%) / AMAX(0.0001, SumWaterHH) * 100
                    Next
                    'mti 2022/09/06 End

                    '               'Water, Safely Managed, Target value
                    Dim TargetYear = waterhhsafetrgtyr(R%, Loc)
                    Dim TargetVal = waterhhsafetrgtval(R%, Loc)
                    If waterhhsafetrgtyr(R%, National) <> 0 Then   'National overrides urban and rural
                        TargetYear = waterhhsafetrgtyr(R%, National)
                        TargetVal = waterhhsafetrgtval(R%, National)
                    End If
                    If TargetYear > 0 Then
                        'mti 2022/11/23 start
                        If Not WaterHHSafeTrgtStartValue(R%, Loc).HasValue Then
                            WaterHHSafeTrgtStartValue(R%, Loc) = sWaterHH(R%, SafelyManaged, Loc%)
                        End If
                        If WaterHHSafeTrgtStartYr(R%, Loc) = 0 Then
                            WaterHHSafeTrgtStartYr(R%, Loc) = IY '
                        End If
                        WATERHH(R%, SafelyManaged, Loc%) = AMAX(WATERHH(R%, SafelyManaged, Loc%), ConvergeOverTimeFromStartYear(CSng(WaterHHSafeTrgtStartValue(R%, Loc)), TargetVal, CInt(TargetYear), WaterHHSafeTrgtStartYr(R%, Loc)))
                        'WATERHH(R%, SafelyManaged, Loc%) = AMAX(WATERHH(R%, SafelyManaged, Loc%), ConvergeOverTime(iWaterHH(R%, SafelyManaged, Loc%), TargetVal, TargetYear))
                        'mti 2022/11/23 End

                        'Normalize the other 4 categories
                        SumWaterHH = 0
                        For Srvc% = 1 To 4
                            SumWaterHH = SumWaterHH + WATERHH(R%, Srvc%, Loc%)
                        Next Srvc%
                        If SumWaterHH > 0 Then
                            For Srvc% = 1 To 4
                                WATERHH(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%) * (100 - WATERHH(R%, SafelyManaged, Loc%)) / SumWaterHH
                            Next Srvc%
                        End If

                    End If

                    '               'Water, Safely Managed, Target value
                    TargetYear = waterhhbasictrgtyr(R%, Loc)
                    TargetVal = waterhhbasictrgtval(R%, Loc)
                    If waterhhbasictrgtyr(R%, National) <> 0 Then   'National overrides urban and rural
                        TargetYear = waterhhbasictrgtyr(R%, National)
                        TargetVal = waterhhbasictrgtval(R%, National)
                    End If
                    If TargetYear > 0 Then
                        'mti 2022/11/23 start
                        If Not WaterHHBasicTrgtStartValue(R%, Loc).HasValue Then
                            WaterHHBasicTrgtStartValue(R%, Loc) = sWaterHH(R%, SafelyManaged, Loc%) + sWaterHH(R%, Basic, Loc%)
                        End If
                        If WaterHHBasicTrgtStartYr(R%, Loc) = 0 Then
                            WaterHHBasicTrgtStartYr(R%, Loc) = IY '
                        End If
                        Dim TrgtPathVal = AMAX(WATERHH(R%, SafelyManaged, Loc%) + WATERHH(R%, Basic, Loc%),
                                    ConvergeOverTimeFromStartYear(CSng(WaterHHBasicTrgtStartValue(R%, Loc)), TargetVal, CInt(TargetYear), WaterHHBasicTrgtStartYr(R%, Loc)))

                        'iAtLeastBasic = iWaterHH(R%, SafelyManaged, Loc%) + iWaterHH(R%, Basic, Loc%)
                        'TrgtPathVal = ConvergeOverTime(iAtLeastBasic, TargetVal, TargetYear)
                        'mti 2022/11/23 End

                        'Normalize Safely Managed and Basic
                        SumWaterHH = 0
                        For Srvc% = 4 To 5
                            SumWaterHH = SumWaterHH + WATERHH(R%, Srvc%, Loc%)
                        Next Srvc%
                        If SumWaterHH > 0 Then
                            For Srvc% = 4 To 5
                                WATERHH(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%) * TrgtPathVal / SumWaterHH
                            Next Srvc%
                        End If

                        'Normalize the other 3 categories
                        SumWaterHH = 0
                        For Srvc% = 1 To 3
                            SumWaterHH = SumWaterHH + WATERHH(R%, Srvc%, Loc%)
                        Next Srvc%
                        If SumWaterHH > 0 Then
                            For Srvc% = 1 To 3
                                WATERHH(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%) * (100 - WATERHH(R%, SafelyManaged, Loc%) - WATERHH(R%, Basic, Loc%)) / SumWaterHH
                            Next Srvc%
                        End If

                    End If

                    'save desired values
                    For Srvc% = 1 To 5
                        WATERHHDEM(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%)
                    Next

                    '               'mti 2022/08/13 End

                Next Loc%

                'mti 2022/09/12 start
                'deleted code and comment here
                'mti 2022/09/12 end


                '   All But First:  Sanitation Connections: Shift Factor
                '   ---------------------------------------------

                '            'Poverty = 0
                '            gdpvar = sGDPPCP(R%)
                '            edvar = SEDYRSAG25(R%, Total)    'gdpvar, edvar, Poverty

                For Loc% = 1 To 2

                    If Loc% = Urban Then Loc_Str = "Urban"
                    If Loc% = Rural Then Loc_Str = "Rural"

                    'mti 2022/08/17 start
                    'mti 2022/08/13 start
                    'If sanithhmodel2sw = 0 Then  'The model that would work with the switch sanithhmodel2sw is distal driver formulation now

                    Call AnalFunc("Sanitation Services, Fractional Logit, Open Defecation, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(OpenDefecation) = CSng(Exp(cVal))

                    Call AnalFunc("Sanitation Services, Fractional Logit, Unimproved Services, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Unimproved) = CSng(Exp(cVal))

                    Call AnalFunc("Sanitation Services, Fractional Logit, Limited Service, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Limited) = CSng(Exp(cVal))

                    Call AnalFunc("Sanitation Services, Fractional Logit, Basic Service, " + Loc_Str,
                    New Single() {Income, Education, Poverty}, cVal,
                    New String() {"GDPPCP", "Educ25", "Poverty"}, context)
                    OddRat(Basic) = CSng(Exp(cVal))


                    'mti 2022/08/13 End
                    'mti 2022/08/17 End

                    OddRat(SafelyManaged) = 1

                    Dim Denom As Single = 1
                    For Srvc% = 1 To 4  '#5, safely managed is the base category of the logistic regresiion
                        Denom = Denom + CSng(OddRat(Srvc%))
                    Next
                    Denom = AMAX(0.001, Denom)

                    Dim SumSanitHH As Single = 0
                    'mti 2022/08/13 start
                    For Srvc% = 1 To 5
                        'mti 2022/09/12 start
                        'sanithhcomp = AMAX(Amin(100, OddRat(Srvc) / denom * 100), 0)
                        'SANITHH(R%, Srvc%, Loc%) = sanithhcomp + ConvergeOverTime(SanitHHShift(R%, Srvc%, Loc%), 0, watsanconv)
                        Dim sanithhcomp = OddRat(Srvc) / Denom * 100
                        'mti 2022/09/27 start
                        Dim Sanitconv% = 50   'watsanconv
                        'mti 2022/09/27 End
                        'mti 2022/09/21 start
                        'SANITHH(R%, Srvc%, Loc%) = AMAX(0, Amin(100, sanithhcomp + ConvergeOverTime(SanitHHShift(R%, Srvc%, Loc%), 0, watsanconv)))
                        If SanitHHShift(R%, Srvc%, Loc%) > 0 Then
                            'data greater than estimate; keep data, slow down convergence for better services
                            If Srvc% = 5 Then
                                'mti 2022/09/27 start
                                SANITHH(R%, Srvc%, Loc%) = CSng(AMAX(0, Amin(100, sanithhcomp + ConvergeOverTime(SanitHHShift(R%, Srvc%, Loc%), 0, Sanitconv))))
                                'mti 2022/09/27 start
                            Else
                                'mti 2022/09/27 start
                                SANITHH(R%, Srvc%, Loc%) = CSng(AMAX(0, Amin(100, sanithhcomp + ConvergeOverTime(SanitHHShift(R%, Srvc%, Loc%), 0, Sanitconv))))  'watsanconv
                                'mti 2022/09/27 end
                            End If
                        Else
                            If Srvc% = 5 Then
                                SANITHH(R%, Srvc%, Loc%) = CSng(AMAX(0, Amin(100, sanithhcomp + ConvergeOverTime(SanitHHShift(R%, Srvc%, Loc%), 0, watsanconv))))
                            Else
                                SANITHH(R%, Srvc%, Loc%) = CSng(AMAX(0, Amin(100, sanithhcomp + ConvergeOverTime(SanitHHShift(R%, Srvc%, Loc%), 0, 1000))))
                            End If
                        End If

                        'mti 2022/09/21 end
                        'Apply scenario paprameter - multiplier base 1   'Note - mutiplier on national will be implemented as same multiplier on rural and urban,
                        'mti 2022/09/06 start
                        'SANITHH(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%) * If(sanithhm(R%, Srvc%, National) <> 1, sanithhm(R%, Srvc%, National), sanithhm(R%, Srvc%, Loc%))
                        'mti 2022/09/06 End
                        'mti 2022/09/27 start
                        'If RegName(R%) = "Mexico" Then Stop
                        Dim SANITHH_Model = SANITHH(R%, Srvc%, Loc%)
                        Dim CurGr As Single
                        If (SanitHHGR(R%, Srvc%, Loc%) <> -9999) And sSANITHHModel(R%, Srvc%, Loc%) <> 0 Then
                            'China use sSANITHHModelPlus(R%, Srvc%, Loc%) instead
                            'mti 2022/10/04 start
                            'This is growth rate branch; MA_FCT is weight for smoothed convergence
                            'CurGrowth = ConvergeOverTime(CurGrowth, SanitHHGR(R%, Srvc%, Loc%), 10)
                            'mti 2022/10/04 end
                            Dim MA_Fct = 0.1
                            'mti 2022/10/04 start
                            'if SANITHH_Model = 0; it would give large negative growth rates;
                            'may be hold SanitHHGR(R%, Srvc%, Loc%) same at that point, especially if it is negative (it would be negtaive, as it is coming down)
                            'mti 2022/10/04 end

                            If COVIDShk_WS = 0 Then
                                CurGr = CSng(AMAX(-0.1, Amin(0.1, MA_Fct * (SANITHH_Model / sSANITHHModel(R%, Srvc%, Loc%) - 1) + (1 - MA_Fct) * SanitHHGR(R%, Srvc%, Loc%))))
                                SanitHHGR(R%, Srvc%, Loc%) = CurGr
                            Else
                                CurGr = CSng(AMAX(-0.1, Amin(0.1, 0.4 * (SANITHH_Model / sSANITHHModel(R%, Srvc%, Loc%) - 1) + 0.6 * SanitHHGR(R%, Srvc%, Loc%))))
                            End If
                            SANITHH(R%, Srvc%, Loc%) = Amin(100, AMAX(0, sSANITHHModelPlus(R%, Srvc%, Loc%) * (1 + CurGr)))
                            'mti 2022/09/29 End
                            'mti 2022/10/04 start
                        Else
                            If SanitHHGR(R%, Srvc%, Loc%) <> -9999 Then
                                SanitHHGR(R%, Srvc%, Loc%) = CSng(0.85 * SanitHHGR(R%, Srvc%, Loc%))  'smoother convergence of growth rate to zero
                                SANITHH(R%, Srvc%, Loc%) = Amin(100, AMAX(0, sSANITHHModelPlus(R%, Srvc%, Loc%) * (1 + SanitHHGR(R%, Srvc%, Loc%))))
                            End If
                            'mti 2022/10/04 End
                        End If
                        sSANITHHModel(R%, Srvc%, Loc%) = SANITHH_Model
                        'mti 2022/09/29 start
                        sSANITHHModelPlus(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%)    'save model and algorithmic
                        'mti 2022/09/29 end
                        'mti 2022/09/29 start
                        SumSanitHH = SumSanitHH + SANITHH(R%, Srvc%, Loc%)
                    Next
                    'mti 2022/09/27 start
                    For Srvc% = 1 To 5
                        'normalize to 100%
                        SANITHH(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%) / AMAX(0.0001, SumSanitHH) * 100
                    Next
                    'mti 2022/09/27 end
                    'mti 2022/09/29 End

                    'Base forecast growth not more than 5 ppt of base forecast of previous yesr; decline not more than 2% (based on growth rate data    'mti 2022/10/04 comment update
                    'mti 2022/09/21 Start
                    SANITHH(R%, SafelyManaged, Loc%) = AMAX(SanitHHBF(R%, SafelyManaged, Loc%) - 2, Amin(SanitHHBF(R%, SafelyManaged, Loc%) + 5, SANITHH(R%, SafelyManaged, Loc%)))
                    Dim SumSanitHH4Req = 100 - SANITHH(R%, SafelyManaged, Loc%)
                    Dim SumSANITHH4 As Single = 0
                    For Srvc% = 1 To 4
                        SumSANITHH4 = SumSANITHH4 + SANITHH(R%, Srvc%, Loc%)
                    Next
                    For Srvc% = 1 To 4
                        'mti 2022/09/26 start
                        'SANITHH(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%) / AMAX(0.0001, SumSanitHH4) * SumSanitHH4Req
                        If SumSANITHH4 > 0 Then
                            'normalize
                            SANITHH(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%) / SumSANITHH4 * SumSanitHH4Req
                        ElseIf SumSanitHH4Req > 0 Then
                            'all (lower) 4 cat are 0; but safelymnaged not 0
                            SANITHH(R%, Srvc%, Loc%) = SumSanitHH4Req / 4
                        Else
                            'all 4 0, safelymanaged 100
                            SANITHH(R%, Srvc%, Loc%) = 0
                        End If
                        'mti 2022/09/26 End
                    Next
                    'mti 2022/09/21 end

                    'mti 2022/09/06 start
                    'Save base projections before multipler and target parameters
                    For Srvc% = 1 To 5
                        SanitHHBF(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%)
                    Next

                    'Apply scenario paprameter - multiplier base 1   'Note - mutiplier on national will be implemented as same multiplier on rural and urban,
                    SumSanitHH = 0
                    For Srvc% = 1 To 5
                        'mti 2022/09/12 Start
                        SANITHH(R%, Srvc%, Loc%) = Amin(100, SANITHH(R%, Srvc%, Loc%) * If(sanithhm(R%, Srvc%, National) <> 1, sanithhm(R%, Srvc%, National), sanithhm(R%, Srvc%, Loc%)))
                        'mti 2022/09/12 End
                        SumSanitHH = SumSanitHH + SANITHH(R%, Srvc%, Loc%)
                    Next

                    For Srvc% = 1 To 5 'normalize to 100%
                        SANITHH(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%) / AMAX(0.0001, SumSanitHH) * 100
                    Next
                    'mti 2022/09/06 End

                    '               'Sanit, Safely Managed, Target value
                    Dim TargetYear = sanithhsafetrgtyr(R%, Loc)
                    Dim TargetVal = sanithhsafetrgtval(R%, Loc)
                    If sanithhsafetrgtyr(R%, National) <> 0 Then   'National overrides urban and rural
                        TargetYear = sanithhsafetrgtyr(R%, National)
                        TargetVal = sanithhsafetrgtval(R%, National)
                    End If
                    If TargetYear > 0 Then
                        'mti 2022/11/23 start
                        If Not SANITHHSafeTrgtStartValue(R%, Loc).HasValue Then
                            SANITHHSafeTrgtStartValue(R%, Loc) = sSanitHH(R%, SafelyManaged, Loc%)
                        End If
                        If SANITHHSafeTrgtStartYr(R%, Loc) = 0 Then
                            SANITHHSafeTrgtStartYr(R%, Loc) = IY '
                        End If
                        SANITHH(R%, SafelyManaged, Loc%) = AMAX(SANITHH(R%, SafelyManaged, Loc%), ConvergeOverTimeFromStartYear(CSng(SANITHHSafeTrgtStartValue(R%, Loc)), TargetVal, CInt(TargetYear), SANITHHSafeTrgtStartYr(R%, Loc)))
                        'SANITHH(R%, SafelyManaged, Loc%) = AMAX(SANITHH(R%, SafelyManaged, Loc%), ConvergeOverTime(iSANITHH(R%, SafelyManaged, Loc%), TargetVal, TargetYear))
                        'mti 2022/11/23 End

                        'Normalize the other 4 categories
                        SumSanitHH = 0
                        For Srvc% = 1 To 4
                            SumSanitHH = SumSanitHH + SANITHH(R%, Srvc%, Loc%)
                        Next Srvc%
                        If SumSanitHH > 0 Then
                            For Srvc% = 1 To 4
                                SANITHH(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%) * (100 - SANITHH(R%, SafelyManaged, Loc%)) / SumSanitHH
                            Next Srvc%
                        End If

                    End If

                    '               'Sanit, Safely Managed, Target value
                    TargetYear = sanithhbasictrgtyr(R%, Loc)
                    TargetVal = sanithhbasictrgtval(R%, Loc)
                    If sanithhbasictrgtyr(R%, National) <> 0 Then   'National overrides urban and rural
                        TargetYear = sanithhbasictrgtyr(R%, National)
                        TargetVal = sanithhbasictrgtval(R%, National)
                    End If
                    If TargetYear > 0 Then
                        'mti 2022/11/23 start
                        If Not SanitHHBasicTrgtStartValue(R%, Loc).HasValue Then
                            SanitHHBasicTrgtStartValue(R%, Loc) = sSanitHH(R%, SafelyManaged, Loc%) + sSanitHH(R%, Basic, Loc%)
                        End If
                        If SanitHHBasicTrgtStartYr(R%, Loc) = 0 Then
                            SanitHHBasicTrgtStartYr(R%, Loc) = IY '
                        End If
                        Dim TrgtPathVal = AMAX(SANITHH(R%, SafelyManaged, Loc%) + SANITHH(R%, Basic, Loc%),
                                    ConvergeOverTimeFromStartYear(CSng(SanitHHBasicTrgtStartValue(R%, Loc)), TargetVal, CInt(TargetYear), SanitHHBasicTrgtStartYr(R%, Loc)))

                        'iAtLeastBasic = iSanitHH(R%, SafelyManaged, Loc%) + iSanitHH(R%, Basic, Loc%)
                        'TrgtPathVal = ConvergeOverTime(iAtLeastBasic, TargetVal, TargetYear)
                        'mti 2022/11/23 End

                        'Normalize Safely Managed and Basic
                        SumSanitHH = 0
                        For Srvc% = 4 To 5
                            SumSanitHH = SumSanitHH + SANITHH(R%, Srvc%, Loc%)
                        Next Srvc%
                        If SumSanitHH > 0 Then
                            For Srvc% = 4 To 5
                                SANITHH(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%) * TrgtPathVal / SumSanitHH
                            Next Srvc%
                        End If

                        'Normalize the other 3 categories
                        SumSanitHH = 0
                        For Srvc% = 1 To 3
                            SumSanitHH = SumSanitHH + SANITHH(R%, Srvc%, Loc%)
                        Next Srvc%
                        If SumSanitHH > 0 Then
                            For Srvc% = 1 To 3
                                SANITHH(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%) * (100 - SANITHH(R%, SafelyManaged, Loc%) - SANITHH(R%, Basic, Loc%)) / SumSanitHH
                            Next Srvc%
                        End If

                    End If

                    'save desired values
                    For Srvc% = 1 To 5
                        SANITHHDEM(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%)
                    Next

                    '               'mti 2022/08/13 End
                Next Loc%

                'mti 2022/09/12 start
                'deleted code/comments here
                'mti 2022/09/12 End


                '------------- cost ----------------
                'gvdb 2024/04/05
            	'unused variable
                'Dim PopTot, sPopTot, lc_shr As Single
                'mti 2022/09/06 start
                'mti 2022/08/17 start
                If watsafenewsw = 1 Then 'new model
                    INFRAINVESTNEW(R%, SafeWaterHH) = 0
                    INFRAINVESTMAINT(R%, SafeWaterHH) = 0

                    INFRAINVESTNEW(R%, SafeWaterImproved) = 0
                    INFRAINVESTMAINT(R%, SafeWaterImproved) = 0

                    INFRAINVESTNEW(R%, SanitationHH) = 0
                    INFRAINVESTMAINT(R%, SanitationHH) = 0

                    INFRAINVESTNEW(R%, SanitationImproved) = 0
                    INFRAINVESTMAINT(R%, SanitationImproved) = 0

                    'Funds needed with base projections of acces rate (before multiplier and target)
                    INFRAINVESTNEWBF(R%, SafeWaterHH) = 0
                    INFRAINVESTMAINTBF(R%, SafeWaterHH) = 0

                    INFRAINVESTNEWBF(R%, SafeWaterImproved) = 0
                    INFRAINVESTMAINTBF(R%, SafeWaterImproved) = 0

                    INFRAINVESTNEWBF(R%, SanitationHH) = 0
                    INFRAINVESTMAINTBF(R%, SanitationHH) = 0

                    INFRAINVESTNEWBF(R%, SanitationImproved) = 0
                    INFRAINVESTMAINTBF(R%, SanitationImproved) = 0
                End If
                'mti 2022/08/17 End


                'mti 2022/10/04 start
                '            CalcInfraInvestWatSanNew R%, WATERHHCOSTLOWER(), WATERHHCOSTHIGHER(), WATERHHDEM(), sWATERHH(), _
                '                                            WATERHH(), waterhhlifelowcost(), waterhhlifehighcost(), waterhhcostlowershr(), _
                '                                            WATERHHINVESTCAP(), WATERHHINVESTMAINT()
                '
                '            'Fund required for base projection before multiplier and target
                '            CalcInfraInvestWatSanNew R%, WATERHHCOSTLOWER(), WATERHHCOSTHIGHER(), WATERHHBF(), sWATERHH(), _
                '                                            WATERHH(), waterhhlifelowcost(), waterhhlifehighcost(), waterhhcostlowershr(), _
                '                                            WaterHHInvestCapBF(), WaterHHInvestMaintBF()
                '
                '
                '
                '            '--- Sanitation ----
                '
                '            CalcInfraInvestWatSanNew R%, SANITHHCOSTLOWER(), SANITHHCOSTHIGHER(), SANITHHDEM(), sSANITHH(), _
                '                                    SANITHH(), Sanithhlifelowcost(), Sanithhlifehighcost(), sanithhcostlowershr(), _
                '                                    SANITHHINVESTCAP(), SANITHHINVESTMAINT()
                '
                '            'Fund required for base projection before multiplier and target
                '            CalcInfraInvestWatSanNew R%, SANITHHCOSTLOWER(), SANITHHCOSTHIGHER(), SANITHHBF(), sSANITHH(), _
                '                                    SANITHH(), Sanithhlifelowcost(), Sanithhlifehighcost(), sanithhcostlowershr(), _
                '                                    SanitHHInvestCapBF(), SanitHHInvestMaintBF()

                'mti 2022/10/27 start
                '            CalcInfraInvestWatSanNew R%, WATERHHCOSTLOWER(), WATERHHCOSTHIGHER(), WATERHHDEM(), WATERHHBF(), sWATERHH(), _
                '                                            WATERHH(), waterhhlifelowcost(), waterhhlifehighcost(), waterhhcostlowershr(), _
                '                                            WATERHHINVESTCAP(), WATERHHINVESTMAINT()
                '
                '            'Fund required for base projection before multiplier and target
                '            CalcInfraInvestWatSanNew R%, WATERHHCOSTLOWER(), WATERHHCOSTHIGHER(), WATERHHBF(), WATERHHBF(), sWATERHH(), _
                '                                            WATERHH(), waterhhlifelowcost(), waterhhlifehighcost(), waterhhcostlowershr(), _
                '                                            WaterHHInvestCapBF(), WaterHHInvestMaintBF()

                'mti 2022/11/18 Start
                'CalcInfraInvestWatSanNew R%, WATERHHCOSTLOWER(), WATERHHCOSTHIGHER(), WATERHHDEM(), WaterHHBF(), sWaterHHBF(), _
                'WATERHH(), waterhhlifelowcost(), waterhhlifehighcost(), waterhhcostlowershr(), _
                'WATERHHINVESTCAP(), WATERHHINVESTMAINT()
                CalcInfraInvestWatSanNew(R%, WATERHHCOSTLOWER, WATERHHCOSTHIGHER, WATERHHDEM, WaterHHBF, sWaterHH,
                                            WATERHH, waterhhlifelowcost, waterhhlifehighcost, waterhhcostlowershr,
                                            WATERHHINVESTCAP, WATERHHINVESTMAINT)
                'mti 2022/11/18 End

                'Fund required for base projection before multiplier and target
                CalcInfraInvestWatSanNew(R%, WATERHHCOSTLOWER, WATERHHCOSTHIGHER, WaterHHBF, WaterHHBF, sWaterHHBF,
                                            WATERHH, waterhhlifelowcost, waterhhlifehighcost, waterhhcostlowershr,
                                            WaterHHInvestCapBF, WaterHHInvestMaintBF)

                'mti 2022/10/27 End


                '--- Sanitation ----

                'mti 2022/10/27 Start
                '            CalcInfraInvestWatSanNew R%, SANITHHCOSTLOWER(), SANITHHCOSTHIGHER(), SANITHHDEM(), SANITHHBF(), sSANITHH(), _
                '                                    SANITHH(), Sanithhlifelowcost(), Sanithhlifehighcost(), sanithhcostlowershr(), _
                '                                    SANITHHINVESTCAP(), SANITHHINVESTMAINT()
                '
                '            'Fund required for base projection before multiplier and target
                '            CalcInfraInvestWatSanNew R%, SANITHHCOSTLOWER(), SANITHHCOSTHIGHER(), SANITHHBF(), SANITHHBF(), sSANITHH(), _
                '                                    SANITHH(), Sanithhlifelowcost(), Sanithhlifehighcost(), sanithhcostlowershr(), _
                '                                    SanitHHInvestCapBF(), SanitHHInvestMaintBF()

                'mti 2022/11/18 Start
                'CalcInfraInvestWatSanNew R%, SANITHHCOSTLOWER(), SANITHHCOSTHIGHER(), SANITHHDEM(), SanitHHBF(), sSanitHHBF(), _
                'SANITHH(), Sanithhlifelowcost(), Sanithhlifehighcost(), sanithhcostlowershr(), _
                'SANITHHINVESTCAP(), SANITHHINVESTMAINT()
                CalcInfraInvestWatSanNew(R%, SANITHHCOSTLOWER, SANITHHCOSTHIGHER, SANITHHDEM, SanitHHBF, sSanitHH,
                                    SANITHH, sanithhlifelowcost, sanithhlifehighcost, sanithhcostlowershr,
                                    SANITHHINVESTCAP, SANITHHINVESTMAINT)
                'mti 2022/11/18 End

                'Fund required for base projection before multiplier and target
                CalcInfraInvestWatSanNew(R%, SANITHHCOSTLOWER, SANITHHCOSTHIGHER, SanitHHBF, SanitHHBF, sSanitHHBF,
                                    SANITHH, sanithhlifelowcost, sanithhlifehighcost, sanithhcostlowershr,
                                    SanitHHInvestCapBF, SanitHHInvestMaintBF)


                For Loc% = 1 To 3
                    For Srvc% = 1 To 5
                        sWaterHHBF(R%, Srvc%, Loc%) = WaterHHBF(R%, Srvc%, Loc%)
                        sSanitHHBF(R%, Srvc%, Loc%) = SanitHHBF(R%, Srvc%, Loc%)
                    Next
                Next

                'mti 2022/10/27 End
                'mti 2022/10/04 End


                'National - Total Funds -new 5 category
                For Srvc% = 1 To 5
                    WATERHHINVESTCAP(R%, Srvc%, National) = WATERHHINVESTCAP(R%, Srvc%, Rural) + WATERHHINVESTCAP(R%, Srvc%, Urban)
                    WATERHHINVESTMAINT(R%, Srvc%, National) = WATERHHINVESTMAINT(R%, Srvc%, Rural) + WATERHHINVESTMAINT(R%, Srvc%, Urban)

                    SANITHHINVESTCAP(R%, Srvc%, National) = SANITHHINVESTCAP(R%, Srvc%, Rural) + SANITHHINVESTCAP(R%, Srvc%, Urban)
                    SANITHHINVESTMAINT(R%, Srvc%, National) = SANITHHINVESTMAINT(R%, Srvc%, Rural) + SANITHHINVESTMAINT(R%, Srvc%, Urban)

                    WaterHHInvestCapBF(R%, Srvc%, National) = WaterHHInvestCapBF(R%, Srvc%, Rural) + WaterHHInvestCapBF(R%, Srvc%, Urban)
                    WaterHHInvestMaintBF(R%, Srvc%, National) = WaterHHInvestMaintBF(R%, Srvc%, Rural) + WaterHHInvestMaintBF(R%, Srvc%, Urban)

                    SanitHHInvestCapBF(R%, Srvc%, National) = SanitHHInvestCapBF(R%, Srvc%, Rural) + SanitHHInvestCapBF(R%, Srvc%, Urban)
                    SanitHHInvestMaintBF(R%, Srvc%, National) = SanitHHInvestMaintBF(R%, Srvc%, Rural) + SanitHHInvestMaintBF(R%, Srvc%, Urban)

                    If watsafenewsw = 1 Then
                        If Srvc% = SafelyManaged Then
                            INFRAINVESTNEW(R%, SafeWaterHH) = WATERHHINVESTCAP(R%, Srvc%, National)
                            INFRAINVESTMAINT(R%, SafeWaterHH) = WATERHHINVESTMAINT(R%, Srvc%, National)

                            INFRAINVESTNEWBF(R%, SafeWaterHH) = WaterHHInvestCapBF(R%, Srvc%, National)
                            INFRAINVESTMAINTBF(R%, SafeWaterHH) = WaterHHInvestMaintBF(R%, Srvc%, National)

                            INFRAINVESTNEW(R%, SanitationHH) = SANITHHINVESTCAP(R%, Srvc%, National)
                            INFRAINVESTMAINT(R%, SanitationHH) = SANITHHINVESTMAINT(R%, Srvc%, National)

                            INFRAINVESTNEWBF(R%, SanitationHH) = SanitHHInvestCapBF(R%, Srvc%, National)
                            INFRAINVESTMAINTBF(R%, SanitationHH) = SanitHHInvestMaintBF(R%, Srvc%, National)
                            'mti 2022/09/21 start
                        ElseIf Srvc% = 1 Then   'Spending category - 1 to 4 is in SafeWaterImproved
                            INFRAINVESTNEW(R%, SafeWaterImproved) = WATERHHINVESTCAP(R%, Srvc%, National)
                            INFRAINVESTMAINT(R%, SafeWaterImproved) = WATERHHINVESTMAINT(R%, Srvc%, National)

                            INFRAINVESTNEWBF(R%, SafeWaterImproved) = WaterHHInvestCapBF(R%, Srvc%, National)
                            INFRAINVESTMAINTBF(R%, SafeWaterImproved) = WaterHHInvestMaintBF(R%, Srvc%, National)

                            INFRAINVESTNEW(R%, SanitationImproved) = SANITHHINVESTCAP(R%, Srvc%, National)
                            INFRAINVESTMAINT(R%, SanitationImproved) = SANITHHINVESTMAINT(R%, Srvc%, National)

                            INFRAINVESTNEWBF(R%, SanitationImproved) = SanitHHInvestCapBF(R%, Srvc%, National)
                            INFRAINVESTMAINTBF(R%, SanitationImproved) = SanitHHInvestMaintBF(R%, Srvc%, National)
                            'mti 2022/09/21 end
                        Else
                            INFRAINVESTNEW(R%, SafeWaterImproved) = INFRAINVESTNEW(R%, SafeWaterImproved) + WATERHHINVESTCAP(R%, Srvc%, National)
                            INFRAINVESTMAINT(R%, SafeWaterImproved) = INFRAINVESTMAINT(R%, SafeWaterImproved) + WATERHHINVESTMAINT(R%, Srvc%, National)

                            INFRAINVESTNEWBF(R%, SafeWaterImproved) = INFRAINVESTNEWBF(R%, SafeWaterImproved) + WaterHHInvestCapBF(R%, Srvc%, National)
                            INFRAINVESTMAINTBF(R%, SafeWaterImproved) = INFRAINVESTMAINTBF(R%, SafeWaterImproved) + WaterHHInvestMaintBF(R%, Srvc%, National)

                            INFRAINVESTNEW(R%, SanitationImproved) = INFRAINVESTNEW(R%, SanitationImproved) + SANITHHINVESTCAP(R%, Srvc%, National)
                            INFRAINVESTMAINT(R%, SanitationImproved) = INFRAINVESTMAINT(R%, SanitationImproved) + SANITHHINVESTMAINT(R%, Srvc%, National)

                            INFRAINVESTNEWBF(R%, SanitationImproved) = INFRAINVESTNEWBF(R%, SanitationImproved) + SanitHHInvestCapBF(R%, Srvc%, National)
                            INFRAINVESTMAINTBF(R%, SanitationImproved) = INFRAINVESTMAINTBF(R%, SanitationImproved) + SanitHHInvestMaintBF(R%, Srvc%, National)

                        End If
                    End If

                Next Srvc%

            Next R%

            'mti 2022/09/06 End

        End Sub

        'mti 2022/08/17
        Public Sub IFsInfraWATSANNew2AllbutFirstYear(context As IForecastContext)
            Dim Loc As Integer
            For R% = 1 To NR%
                'mti 2022/09/16 start
                For Loc% = 1 To 2       '3-> national; done in all years
                    For Srvc% = 1 To 5
                        WATERHH(R%, Srvc%, Loc%) = WATERHHDEM(R%, Srvc%, Loc%)   '..DEM is desired demand (=base forecast + scenario; before financial impact)
                        SANITHH(R%, Srvc%, Loc%) = SANITHHDEM(R%, Srvc%, Loc%)
                    Next
                Next Loc%
                'mti 2022/09/16 end

				'gvdb 2024/04/05
            	'unused variable
                'Dim PopTot As Single, sPopTot As Single
                'Dim WatSafeDemThousands As Single, ReqdAddition As Single
                'Dim RealAddition As Single, WatSafeRealThousands As Single
                'Dim lc_shr As Single
                'mti 2022/09/21 start
                '            If watsafenewsw = 0 Or infrafinon = 0 Or sInfraBudImpact(R%) >= 1 Then
                '            Else

                If watsafenewsw = 1 And infrafinon = 1 Then
                    'mti 2022/09/30 start - replacing sInfraBudImpact(R%) with sInfraBudImpact_WatSanNew(R%)
                    If sInfraBudImpact_WatSanNew(R%) < 1 Then
                        'mti 2022/09/30 end
                        'mti 2022/09/21 End

                        '                'All Years but First 2: Infrastructure Budget Impact
                        '                '----------------------------------------------------

                        'mti 2022/09/16 start
                        For Loc% = 1 To 2       '3-> national; done in all years
                        For Srvc% = 1 To 5
                            WATERHHINVESTCAP(R%, Srvc%, Loc%) = 0
                            WATERHHINVESTMAINT(R%, Srvc%, Loc%) = 0

                            SANITHHINVESTCAP(R%, Srvc%, Loc%) = 0
                            SANITHHINVESTMAINT(R%, Srvc%, Loc%) = 0
                        Next
                    Next Loc%
                        'mti 2022/09/16 end

                        'mti 2022/09/06 start
                        'mti 2022/09/30 start - replacing sInfraBudImpact(R%) with sInfraBudImpact_WatSanNew(R%)
                        'mti 2022/10/04 Start
                        '                        CalcInfraInvestWatSanNew R%, WATERHHCOSTLOWER(), WATERHHCOSTHIGHER(), WATERHHDEM(), sWATERHH(), _
                        '                                                        WATERHH(), waterhhlifelowcost(), waterhhlifehighcost(), waterhhcostlowershr(), _
                        '                                                        WATERHHINVESTCAP(), WATERHHINVESTMAINT(), sInfraBudImpact_WatSanNew(R%), True
                        '
                        '                        CalcInfraInvestWatSanNew R%, SANITHHCOSTLOWER(), SANITHHCOSTHIGHER(), SANITHHDEM(), sSANITHH(), _
                        '                                                        SANITHH(), Sanithhlifelowcost(), Sanithhlifehighcost(), sanithhcostlowershr(), _
                        '                                                        SANITHHINVESTCAP(), SANITHHINVESTMAINT(), sInfraBudImpact_WatSanNew(R%), True


                        'mti 2022/10/20 start
                        '                        CalcInfraInvestWatSanNew R%, WATERHHCOSTLOWER(), WATERHHCOSTHIGHER(), WATERHHDEM(), WATERHHBF(), sWATERHH(), _
                        '                                                        WATERHH(), waterhhlifelowcost(), waterhhlifehighcost(), waterhhcostlowershr(), _
                        '                                                        WATERHHINVESTCAP(), WATERHHINVESTMAINT(), sInfraBudImpact_WatSanNew(R%), True
                        '
                        '                        CalcInfraInvestWatSanNew R%, SANITHHCOSTLOWER(), SANITHHCOSTHIGHER(), SANITHHDEM(), SANITHHBF(), sSANITHH(), _
                        '                                                        SANITHH(), Sanithhlifelowcost(), Sanithhlifehighcost(), sanithhcostlowershr(), _
                        '                                                        SANITHHINVESTCAP(), SANITHHINVESTMAINT(), sInfraBudImpact_WatSanNew(R%), True

                        '        If RegName(R%) = "Brazil" Then Stop
                        CalcInfraInvestWatSanNew(R%, WATERHHCOSTLOWER, WATERHHCOSTHIGHER, WATERHHDEM, WaterHHBF, sWaterHH,
                                                        WATERHH, waterhhlifelowcost, waterhhlifehighcost, waterhhcostlowershr,
                                                        WATERHHINVESTCAP, WATERHHINVESTMAINT, sInfraBudImpactByType(R%, SafeWaterHH),
                                                        sInfraBudImpactByType(R%, SafeWaterImproved), True)

                        CalcInfraInvestWatSanNew(R%, SANITHHCOSTLOWER, SANITHHCOSTHIGHER, SANITHHDEM, SanitHHBF, sSanitHH,
                                                        SANITHH, sanithhlifelowcost, sanithhlifehighcost, sanithhcostlowershr,
                                                        SANITHHINVESTCAP, SANITHHINVESTMAINT, sInfraBudImpactByType(R%, SanitationHH),
                                                        sInfraBudImpactByType(R%, SanitationImproved), True)

                        'mti 2022/10/20 End
                        'mti 2022/10/04 End

                        'mti 2022/09/30 end

                        'mti 2022/09/12 - deleted commented block

                        'mti 2022/09/21 start
                        'mti 2022/09/30 start - replacing sInfraBudImpact(R%) with sInfraBudImpact_WatSanNew(R%)
                    ElseIf sInfraBudImpact_WatSanNew(R%) > 1 Then
                        'mti 2022/09/30 end

                        '   If IY >= 2 And R = 1 Then Stop
                        'Use up extra budget - For SafelyManaged (spending cat SafeWaterHH) and Basic (SafeWaterImproved)

                        CalcInfraUseExtraBudgetWatSanNew(R%, SafelyManaged, WATERHH, WaterHH_Funding_Gap, WATERHHCOSTLOWER, WATERHHCOSTHIGHER, waterhhcostlowershr, WATERHHINVESTCAP,
                                ExtraFundAllocated(R%, SafeWaterHH), INFRAINVESTNEWPUB(R%, SafeWaterHH), INFRAINVESTNEW(R%, SafeWaterHH),
                                ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))

                        CalcInfraUseExtraBudgetWatSanNew(R%, Basic, WATERHH, WaterHH_Funding_Gap, WATERHHCOSTLOWER, WATERHHCOSTHIGHER, waterhhcostlowershr, WATERHHINVESTCAP,
                                ExtraFundAllocated(R%, SafeWaterImproved), INFRAINVESTNEWPUB(R%, SafeWaterImproved), INFRAINVESTNEW(R%, SafeWaterImproved),
                                ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))


                        CalcInfraUseExtraBudgetWatSanNew(R%, SafelyManaged, SANITHH, SanitHH_Funding_Gap, SANITHHCOSTLOWER, SANITHHCOSTHIGHER, sanithhcostlowershr, SANITHHINVESTCAP,
                                ExtraFundAllocated(R%, SanitationHH), INFRAINVESTNEWPUB(R%, SanitationHH), INFRAINVESTNEW(R%, SanitationHH),
                                ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))

                        CalcInfraUseExtraBudgetWatSanNew(R%, Basic, SANITHH, SanitHH_Funding_Gap, SANITHHCOSTLOWER, SANITHHCOSTHIGHER, sanithhcostlowershr, SANITHHINVESTCAP,
                                ExtraFundAllocated(R%, SanitationImproved), INFRAINVESTNEWPUB(R%, SanitationImproved), INFRAINVESTNEW(R%, SanitationImproved),
                                ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))


                    End If     'sInfraBudImpact(R%) < 1 Then

                    'mti 2022/09/29 start
                    'One final normalization
                    For Loc% = 1 To 2
                        Dim SumSanitHH As Single = 0
                        Dim SumWaterHH As Single = 0
                        For Srvc% = 1 To 5
                            SumSanitHH = SumSanitHH + SANITHH(R%, Srvc%, Loc%)
                            SumWaterHH = SumWaterHH + WATERHH(R%, Srvc%, Loc%)
                        Next
                        For Srvc% = 1 To 5
                            SANITHH(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%) / AMAX(0.0001, SumSanitHH) * 100
                            WATERHH(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%) / AMAX(0.0001, SumWaterHH) * 100
                        Next
                    Next Loc%
                    'mti 2022/09/29 End

                    'Computation of national Investment by existing infra spending categories ;
                    '(not computing national spending by new categories here; to avoid double counting
                    For Srvc% = 1 To 5
                        If Srvc% = SafelyManaged Then   'SafeWaterHH
                            INFRAINVESTNEW(R%, SafeWaterHH) = WATERHHINVESTCAP(R%, Srvc%, Rural) + WATERHHINVESTCAP(R%, Srvc%, Urban)
                            INFRAINVESTMAINT(R%, SafeWaterHH) = WATERHHINVESTMAINT(R%, Srvc%, Rural) + WATERHHINVESTMAINT(R%, Srvc%, Urban)

                            INFRAINVESTNEW(R%, SanitationHH) = SANITHHINVESTCAP(R%, Srvc%, Rural) + SANITHHINVESTCAP(R%, Srvc%, Urban)
                            INFRAINVESTMAINT(R%, SanitationHH) = SANITHHINVESTMAINT(R%, Srvc%, Rural) + SANITHHINVESTMAINT(R%, Srvc%, Urban)
                        ElseIf Srvc% = 1 Then   'Spending category - 1 to 4 is in SafeWaterImproved
                            INFRAINVESTNEW(R%, SafeWaterImproved) = WATERHHINVESTCAP(R%, Srvc%, Rural) + WATERHHINVESTCAP(R%, Srvc%, Urban)
                            INFRAINVESTMAINT(R%, SafeWaterImproved) = WATERHHINVESTMAINT(R%, Srvc%, Rural) + WATERHHINVESTMAINT(R%, Srvc%, Urban)

                            INFRAINVESTNEW(R%, SanitationImproved) = SANITHHINVESTCAP(R%, Srvc%, Rural) + SANITHHINVESTCAP(R%, Srvc%, Urban)
                            INFRAINVESTMAINT(R%, SanitationImproved) = SANITHHINVESTMAINT(R%, Srvc%, Rural) + SANITHHINVESTMAINT(R%, Srvc%, Urban)
                        Else
                            INFRAINVESTNEW(R%, SafeWaterImproved) = INFRAINVESTNEW(R%, SafeWaterImproved) + WATERHHINVESTCAP(R%, Srvc%, Rural) + WATERHHINVESTCAP(R%, Srvc%, Urban)
                            INFRAINVESTMAINT(R%, SafeWaterImproved) = INFRAINVESTMAINT(R%, SafeWaterImproved) + WATERHHINVESTMAINT(R%, Srvc%, Rural) + WATERHHINVESTMAINT(R%, Srvc%, Urban)

                            INFRAINVESTNEW(R%, SanitationImproved) = INFRAINVESTNEW(R%, SanitationImproved) + SANITHHINVESTCAP(R%, Srvc%, Rural) + SANITHHINVESTCAP(R%, Srvc%, Urban)
                            INFRAINVESTMAINT(R%, SanitationImproved) = INFRAINVESTMAINT(R%, SanitationImproved) + SANITHHINVESTMAINT(R%, Srvc%, Rural) + SANITHHINVESTMAINT(R%, Srvc%, Urban)
                        End If
                    Next
                    'mti 2022/09/26 start
                    'watsafenewsw = 1 Then    'forward/funding linkage from watsan new (5 category)
                    INFRAINVESTNEW(R%, InfraTot) = 0
                    INFRAINVESTMAINT(R%, InfraTot) = 0

                    INFRAINVESTNEWPUB(R%, InfraTot) = 0
                    INFRAINVESTMAINTPUB(R%, InfraTot) = 0

                    For j = 1 To InfraTot - 1
                        INFRAINVESTNEW(R%, InfraTot) = INFRAINVESTNEW(R%, InfraTot) + INFRAINVESTNEW(R%, j)
                        INFRAINVESTMAINT(R%, InfraTot) = INFRAINVESTMAINT(R%, InfraTot) + INFRAINVESTMAINT(R%, j)

                        INFRAINVESTNEWPUB(R%, InfraTot) = INFRAINVESTNEWPUB(R%, InfraTot) + INFRAINVESTNEWPUB(R%, j)
                        INFRAINVESTMAINTPUB(R%, InfraTot) = INFRAINVESTMAINTPUB(R%, InfraTot) + INFRAINVESTMAINTPUB(R%, j)
                    Next
                    'mti 2022/09/26 End
                End If  'If watsafenewsw = 1 And infrafinon = 1 Then
                'mti 2022/09/21 End

            Next R%

            IFsInfraWATSANNewAllYears(context)
        End Sub

        Private Sub IFsInfraWATSANNewAllYears(context As IForecastContext)
            Dim Loc As Integer
            Dim Income As Single, Education As Single

            Dim NationalPop As Single, UrbanPop As Single, RuralPop As Single

            For R% = 1 To NR%

                For Srvc% = 1 To 5

                    'mti 2022/09/12 start

                    'mti 2022/09/16 start
                    'mti 2022/09/21 start
                    If IY = 1 Then
                        NationalPop = POP(R%)
                        UrbanPop = POPURBAN(R%)
                        RuralPop = NationalPop - UrbanPop

                    Else
                        NationalPop = sPOPTotal(R%)
                        UrbanPop = SPOPURBAN(R%)
                        RuralPop = NationalPop - UrbanPop

                        'Natonal Access Rates - Desired - here  'check if popurban is computed right
                        WATERHHDEM(R%, Srvc%, National) = (WATERHHDEM(R%, Srvc%, Rural) * RuralPop + WATERHHDEM(R%, Srvc%, Urban) * UrbanPop) / AMAX(0.0001, NationalPop)
                        SANITHHDEM(R%, Srvc%, National) = (SANITHHDEM(R%, Srvc%, Rural) * RuralPop + SANITHHDEM(R%, Srvc%, Urban) * UrbanPop) / AMAX(0.0001, NationalPop)
                        'mti 2022/09/12 End

                        'Natonal Access Rates - After Funding - here  'check if popurban is computed right
                        WATERHH(R%, Srvc%, National) = (WATERHH(R%, Srvc%, Rural) * RuralPop + WATERHH(R%, Srvc%, Urban) * UrbanPop) / AMAX(0.0001, NationalPop)
                        SANITHH(R%, Srvc%, National) = (SANITHH(R%, Srvc%, Rural) * RuralPop + SANITHH(R%, Srvc%, Urban) * UrbanPop) / AMAX(0.0001, NationalPop)


                    End If
                    'mti 2022/09/21 End

                    'Number of people
                    WATERHHPOP(R%, Srvc%, National) = WATERHH(R%, Srvc%, National) * NationalPop / 100
                    WATERHHPOP(R%, Srvc%, Urban) = WATERHH(R%, Srvc%, Urban) * UrbanPop / 100
                    WATERHHPOP(R%, Srvc%, Rural) = WATERHHPOP(R%, Srvc%, National) - WATERHHPOP(R%, Srvc%, Urban)

                    SANITHHPOP(R%, Srvc%, National) = SANITHH(R%, Srvc%, National) * NationalPop / 100
                    SANITHHPOP(R%, Srvc%, Urban) = SANITHH(R%, Srvc%, Urban) * UrbanPop / 100
                    SANITHHPOP(R%, Srvc%, Rural) = SANITHHPOP(R%, Srvc%, National) - SANITHHPOP(R%, Srvc%, Urban)
                    'mti 2022/09/16 End

                    'National - Total Funds
                    WATERHHINVESTCAP(R%, Srvc%, National) = WATERHHINVESTCAP(R%, Srvc%, Rural) + WATERHHINVESTCAP(R%, Srvc%, Urban)
                    WATERHHINVESTMAINT(R%, Srvc%, National) = WATERHHINVESTMAINT(R%, Srvc%, Rural) + WATERHHINVESTMAINT(R%, Srvc%, Urban)

                    SANITHHINVESTCAP(R%, Srvc%, National) = SANITHHINVESTCAP(R%, Srvc%, Rural) + SANITHHINVESTCAP(R%, Srvc%, Urban)
                    SANITHHINVESTMAINT(R%, Srvc%, National) = SANITHHINVESTMAINT(R%, Srvc%, Rural) + SANITHHINVESTMAINT(R%, Srvc%, Urban)

                    'mti 2022/09/21 start
                    WaterHHInvestCapBF(R%, Srvc%, National) = WaterHHInvestCapBF(R%, Srvc%, Rural) + WaterHHInvestCapBF(R%, Srvc%, Urban)
                    WaterHHInvestMaintBF(R%, Srvc%, National) = WaterHHInvestMaintBF(R%, Srvc%, Rural) + WaterHHInvestMaintBF(R%, Srvc%, Urban)

                    SanitHHInvestCapBF(R%, Srvc%, National) = SanitHHInvestCapBF(R%, Srvc%, Rural) + SanitHHInvestCapBF(R%, Srvc%, Urban)
                    SanitHHInvestMaintBF(R%, Srvc%, National) = SanitHHInvestMaintBF(R%, Srvc%, Rural) + SanitHHInvestMaintBF(R%, Srvc%, Urban)

                    'mti 2022/09/21 end

                Next

                'Save access rates
                For Loc% = 1 To 3
                    For Srvc% = 1 To 5
                        sWaterHH(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%)
                        sSanitHH(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%)
                    Next Srvc%
                Next Loc%

                'Distal Driver Formulation for Water and Sanitation - new 5 categories
                'mti 2022/10/20 start
                'If watsafenewsw = 1 And hlwatsansw > 0 Then
                If watsafenewsw = 1 And watsafefwdsw = 1 And hlwatsansw > 0 Then 'new 5 cat model
                    'mti 2022/10/20 end

                    If IY = 1 Then
                        Income = GDPPCP(R%)
                        Education = EDYRSAG25(R%, Total)    'gdpvar, edvar, Poverty
                    Else
                        Income = sGDPPCP(R%)
                        Education = SEDYRSAG25(R%, Total)
                    End If

                    For Loc% = 1 To 2   'Rural and Urban

                        If Loc% = Urban Then Loc_Str = "Urban"
                        If Loc% = Rural Then Loc_Str = "Rural"

                        'Distal Driver Formulation for Water  - new 5 categories
                        Call AnalFunc("Water Services Model 2, Fractional Logit, Surface Water, " + Loc_Str,
                        New Single() {Income, Education}, cVal,
                        New String() {"GDPPCP", "Educ25"}, context)
                        OddRat(Surface) = CSng(Exp(cVal))

                        Call AnalFunc("Water Services Model 2, Fractional Logit, Unimproved Services, " + Loc_Str,
                        New Single() {Income, Education}, cVal,
                        New String() {"GDPPCP", "Educ25"}, context)
                        OddRat(Unimproved) = CSng(Exp(cVal))

                        Call AnalFunc("Water Services Model 2, Fractional Logit, Limited Service, " + Loc_Str,
                        New Single() {Income, Education}, cVal,
                        New String() {"GDPPCP", "Educ25"}, context)
                        OddRat(Limited) = CSng(Exp(cVal))

                        Call AnalFunc("Water Services Model 2, Fractional Logit, Basic Service, " + Loc_Str,
                        New Single() {Income, Education}, cVal,
                        New String() {"GDPPCP", "Educ25"}, context)
                        OddRat(Basic) = CSng(Exp(cVal))

                        OddRat(SafelyManaged) = 1

                        Dim denom As Single = 1
                        For Srvc% = 1 To 4  '#5, safely managed is the base category of the logistic regresiion
                            denom = denom + CSng(OddRat(Srvc%))
                        Next
                        denom = AMAX(0.001, denom)

                        Dim SumWaterHH As Single = 0

                        For Srvc% = 1 To 5
                            Dim WaterHHComp = AMAX(Amin(100, OddRat(Srvc) / denom * 100), 0)
                            If IY = 1 Then
                                WaterHH2(R%, Srvc%, Loc%) = WATERHH(R%, Srvc%, Loc%)
                                WaterHHShift2(R%, Srvc%, Loc%) = CSng(WaterHH2(R%, Srvc%, Loc%) - WaterHHComp)
                            Else      'all but first
                                'mti 2022/09/16 start
                                'WaterHH2(R%, Srvc%, Loc%) = WaterHHComp + ConvergeOverTime(WaterHHShift2(R%, Srvc%, Loc%), 0, watsanconv)
                                WaterHH2(R%, Srvc%, Loc%) = CSng(AMAX(0, Amin(100, WaterHHComp + ConvergeOverTime(WaterHHShift2(R%, Srvc%, Loc%), 0, watsanconv))))
                                'mti 2022/09/16 end
                            End If
                            SumWaterHH = SumWaterHH + WaterHH2(R%, Srvc%, Loc%)
                        Next

                        For Srvc% = 1 To 5  'normalize to 100%
                            WaterHH2(R%, Srvc%, Loc%) = WaterHH2(R%, Srvc%, Loc%) / AMAX(0.0001, SumWaterHH) * 100
                        Next

                        'Distal Driver Formulation for Sanitation  - new 5 categories
                        Call AnalFunc("Sanitation Services Model 2, Fractional Logit, Open Defecation, " + Loc_Str,
                        New Single() {Income, Education}, cVal,
                        New String() {"GDPPCP", "Educ25"}, context)
                        OddRat(OpenDefecation) = CSng(Exp(cVal))

                        Call AnalFunc("Sanitation Services Model 2, Fractional Logit, Unimproved Services, " + Loc_Str,
                        New Single() {Income, Education}, cVal,
                        New String() {"GDPPCP", "Educ25"}, context)
                        OddRat(Unimproved) = CSng(Exp(cVal))

                        Call AnalFunc("Sanitation Services Model 2, Fractional Logit, Limited Service, " + Loc_Str,
                        New Single() {Income, Education}, cVal,
                        New String() {"GDPPCP", "Educ25"}, context)
                        OddRat(Limited) = CSng(Exp(cVal))

                        Call AnalFunc("Sanitation Services Model 2, Fractional Logit, Basic Service, " + Loc_Str,
                        New Single() {Income, Education}, cVal,
                        New String() {"GDPPCP", "Educ25"}, context)
                        OddRat(Basic) = CSng(Exp(cVal))

                        OddRat(SafelyManaged) = 1

                        denom = 1
                        For Srvc% = 1 To 4  '#5, safely managed is the base category of the logistic regresiion
                            denom = denom + CSng(OddRat(Srvc%))
                        Next
                        denom = AMAX(0.001, denom)

                        Dim SumSanitHH As Single = 0

                        For Srvc% = 1 To 5
                            Dim SanitHHComp = AMAX(Amin(100, OddRat(Srvc) / denom * 100), 0)
                            If IY = 1 Then
                                SanitHH2(R%, Srvc%, Loc%) = SANITHH(R%, Srvc%, Loc%)
                                SanitHHShift2(R%, Srvc%, Loc%) = CSng(SanitHH2(R%, Srvc%, Loc%) - SanitHHComp)
                            Else      'all but first
                                'mti 2022/09/16 start
                                'SanitHH2(R%, Srvc%, Loc%) = sanithhcomp + ConvergeOverTime(SanitHHShift2(R%, Srvc%, Loc%), 0, watsanconv)
                                SanitHH2(R%, Srvc%, Loc%) = CSng(AMAX(0, Amin(100, SanitHHComp + ConvergeOverTime(SanitHHShift2(R%, Srvc%, Loc%), 0, watsanconv))))
                                'mti 2022/09/16 End
                            End If
                            SumSanitHH = SumSanitHH + SanitHH2(R%, Srvc%, Loc%)
                        Next

                        For Srvc% = 1 To 5  'normalize to 100%
                            SanitHH2(R%, Srvc%, Loc%) = SanitHH2(R%, Srvc%, Loc%) / AMAX(0.0001, SumSanitHH) * 100
                        Next


                    Next Loc%

                    'Write Water Distal to IFsPop
                    'Natonal Access Rates - From Distal Driver Formulation - here  'check if popurban is computed right
                    For Srvc% = 1 To 5
                        'mti 2022/09/16 start
                        'WaterHH2(R%, Srvc%, National) = (WaterHH2(R%, Srvc%, Rural) * (POP(R%) - POPURBAN(R%)) + WaterHH2(R%, Srvc%, Urban) * POPURBAN(R%)) / AMAX(0.0001, POP(R%))
                        WaterHH2(R%, Srvc%, National) = (WaterHH2(R%, Srvc%, Rural) * RuralPop + WaterHH2(R%, Srvc%, Urban) * UrbanPop) / AMAX(0.0001, NationalPop)
                        'mti 2022/09/16 End
                    Next

                    'Write to IFsPop
                    WATSAFE2(R, 1) = WaterHH2(R%, Surface, National) + WaterHH2(R%, Unimproved, National)
                    WATSAFE2(R, 2) = WaterHH2(R%, Limited, National) + WaterHH2(R%, Basic, National)
                    WATSAFE2(R, 3) = WaterHH2(R%, SafelyManaged, National)
                    'For j = 1 To 3
                    '    TblHLPRDR.AddNew
                    '    TblHLPRDR.Fields("VarName") = "WATSAFE"
                    '    TblHLPRDR.Fields("Year") = IY% + frm_MainMenu.IFsGUIctrl1.cFirstYear - 1
                    '    TblHLPRDR.Fields("Region") = R%
                    '    TblHLPRDR.Fields("Dim2") = j
                    '    TblHLPRDR.Fields("DistalFormVal") = WATSAFE2(R, j)
                    '    TblHLPRDR.Update
                    'Next j


                    'Write Sanitation Distal to IFsPop
                    'Natonal Access Rates - From Distal Driver Formulation - here  'check if popurban is computed right
                    For Srvc% = 1 To 5
                        'mti 2022/09/16 start
                        'SanitHH2(R%, Srvc%, National) = (SanitHH2(R%, Srvc%, Rural) * (POP(R%) - POPURBAN(R%)) + SanitHH2(R%, Srvc%, Urban) * POPURBAN(R%)) / AMAX(0.0001, POP(R%))
                        SanitHH2(R%, Srvc%, National) = (SanitHH2(R%, Srvc%, Rural) * RuralPop + SanitHH2(R%, Srvc%, Urban) * UrbanPop) / AMAX(0.0001, NationalPop)
                        'mti 2022/09/16 End
                    Next

                    SANITATION2(R, 1) = SanitHH2(R%, OpenDefecation, National) + SanitHH2(R%, Unimproved, National)
                    SANITATION2(R, 2) = SanitHH2(R%, Limited, National) + SanitHH2(R%, Basic, National)
                    SANITATION2(R, 3) = SanitHH2(R%, SafelyManaged, National)
                    'For j = 1 To 3
                    '    TblHLPRDR.AddNew
                    '    TblHLPRDR.Fields("VarName") = "SANITATION"
                    '    TblHLPRDR.Fields("Year") = IY% + frm_MainMenu.IFsGUIctrl1.cFirstYear - 1
                    '    TblHLPRDR.Fields("Region") = R%
                    '    TblHLPRDR.Fields("Dim2") = j
                    '    TblHLPRDR.Fields("DistalFormVal") = SANITATION2(R, j)
                    '    TblHLPRDR.Update
                    'Next j

                End If  'watsafenewsw = 1 And hlwatsansw

            Next R%

            'mti 2022/09/16 start
            If watsafenewsw > 0 Then    'write only when forward linkage from new (5 category model) is off
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(SANITATION2, NR, IY + FirstYear - 1, "SANITATION", 3))
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(WATSAFE2, NR, IY + FirstYear - 1, "WATSAFE", 3))
            End If


            'mti 2022/08/17 start
            If IY > 1 Then
                CalcInfraindices(context)
            End If
            'mti August 07, 2012
            'mti 2022/08/17 end


            'mti 2022/08/17 End
        End Sub

        Public Sub IFsInfraWATSAN1FirstYear(context As IForecastContext)

            'ttt common FirstYear for all IFUNs
            'mti January 2011 - start
            'replaced all SanitHH wioth SanitationHH and WatSafeHH with SafeWaterHH
            'replaced dimensionality of DeppcntWS
            'mti January 2011 -end

            'mti Dec 12, 2010 - add parameter IFUN
            'mti Infrastructure August 2010 move watsan  - new proc
            'needs POPRural from ifsPop(2)
            'needs GDS(R, Health) from ifsIntlPol
            'needs GOVREGQUAL from ifsDomPol(1)
            'needs GINIDOM from ifsEcoSAM(2)

            sErrorSub = "IFsInfraWatSan1"
            'sErrorSub = "IFsInfraWatSan" & CStr(IFUN)

            'mti August 17, 2011 -start
            'Dim watsanconv As Single = 75 '50 'ttt moved out
            'mti August 17, 2011 -end

            'On Error GoTo errininfra
            'jrs 2009/09/30 changed POPURBAN to SPOPURBAN
            'jrs 2009/10/16
            Dim Poverty As Single
            'mti Dec 12, 2010 - start
            'jrs 2009/11/16
            'sErrorSub = "IFsInfraWATSAN"
            'If IY% = 1 Then
            'jrs 2009/04/07 moving from ifsPop
            ReDim SANITATIONShift(NR%, 3)
            ReDim WATSAFEShift(NR%, 3)

            ReDim SANITATION2(NR, 3)
            ReDim SANITATIONShift2(NR%, 3)
            ReDim WATSAFE2(NR, 3)
            ReDim WATSAFEShift2(NR%, 3)
            'jrs 2009/12/22
            ReDim SSANITATION(NR%, 3)
            ReDim sssanitation(NR%, 3)
            'jrs 2009/12/23
            ReDim SWATSAFE(NR%, 3)

            'mti October 20, 2011 - start
            ReDim iWatSafeNone(NR%)
            'mti October 20, 2011 - end

            'mti October 20, 2011 - start
            'mti May Day 2012 -start
            'ReDim iSanitationNone(NR%)
            ReDim iSanitationHH(NR%)
            'mti May Day 2012 -end
            'mti October 20, 2011 - end

            'mti January 2011 - start
            'ReDim DeppcntWS(7)
            ReDim DeppcntWS(NInfraType%)
            'mti January 2011 - end

            ReDim sHHSIZEWS(NR%)

            'mti February 2011 Infarstructure - start
            ReDim WATWASTEGR(NR%), WATWASTETREATGR(NR%), SWATWASTETREAT(NR%), sWATWASTE(NR%), WatWasteTreatShift(NR%)
            ReDim iWatWasteTreatCost(NR%)

            'mti May 04, 2012 - start
            ReDim WatSafeGR(NR%, 3), SanitationGR(NR%, 3)
            'mti May 04, 2012 - end

            ReDim sGDPPCP(NR%)

            'mti April 26 2011 - start
            ReDim SINFRAINDTRAD(NR%)
            ReDim SINFRAINDICT(NR%)
            'mti April 26 2011 - end

            'mti October 20, 2011 -start
            ReDim SINFRAINDTRADDEM(NR%)
            ReDim SINFRAINDICTDEM(NR%)
            ReDim SSINFRAINDICTDEM(NR%)
            'mti October 20, 2011 - end

            'mti May 2011 - start
            ReDim SSINFRAINDICT(NR%)
            'mti May 2011 - end


            'mti July 18, 2011 - start
            ReDim PreMultWATWASTE(NR%) ', sPreMultWATWASTE(NR%)

            'ReDim PreMultWATWASTEGR(NR%), sPreMultWATWASTEGR(NR%)
            '        ReDim PreMultWATWASTETREAT(NR%)
            'mti July 18, 2011 - end

            'mti February 2011 Infarstructure - end

            'mti August 17, 2011 -start
            ReDim WatWasteColShift(NR%)
            'mti August 17, 2011 - end


            'mti April 05, 2012 - start
            'Base forecast values of physical variables
            ReDim SANITATIONBF(NR%, 3), WatSafeBF(NR%, 3)
            ReDim sSanitationBF(NR%, 3), sWatSafeBF(NR%, 3)

            'mti Aug 21, 2012 -start
            ReDim iSanitation(NR%, 3)
            'mti Aug 21, 2012 - end

            ReDim WATWASTEBF(NR%), PreMultWATWASTEBF(NR%), WatWasteGRBF(NR%)
            ReDim WatWasteTreatBF(NR%)

            'mti April 05, 2012 - end

            ReDim sWATSAFEPreMult(NR%, 3), sSanitationPreMult(NR%, 3)

            'mti 2014/05/28 - start
            'mti 2021/08/10 start       sPreBudWATSAFE(NR%, 3)
            ReDim sPreBudSanitation(NR%, 3), sPreBudWATSAFE(NR%, 3)
            'mti 2021/08/10 end
            'mti 2014/05/28 - end

            'jrs 2015/03/19 *** begin ***
            ReDim WATERFILTEREDI(NR%, 3)
            'jrs 2015/03/19 *** end ***

            'Depreciation rates for various types of infrastructure
            'mti 2022/10/20 start
            'needs to go in IFsInfraWatsan as that is called earlier in Yr 1
            ReDim Deppcnt(NInfraType%)
            'mti 2022/10/20 End

            'jrs 2013/09/16 *** begin ***
            'Infrastructure link to Poverty (GiniDom)
            'ReDim INFRAINDCOMBINED(NR%), SINFRAINDCOMBINED(NR%)
            'jrs 2013/09/16 *** end ***

            '******** First Year: Water and Sanitation *********************
            '******************* begin *************************************
            'Last change on:
            'jrs 2009/03/23

            'If RegName$(R%) = "Afghanistan" Then Stop
            'mti February 2011 Infarstructure -start reading  growth rate

            'ttt 2019/10/08 
            ReDim GDPPCPGR(NR%)
            For R% = 1 To NR
                GDPPCPGR(R%) = (igdpr(R%) - POPR(R%) / 100) / (1 + POPR(R%) / 100) '0 to 1
                'Dim GDPPCPGR = (igdpr(R%) - POPR(R%) / 100) / (1 + POPR(R%) / 100) '0 to 1
                'Dim GDPPCPGR = (igdpr(R%) - POPR(R%) / 100) / (1 + POPR(R%) / 100) '0 to 1
            Next
            'ttt 2019/10/08 Taken out of loop

            sErrorSub = "IFsInfraWatSan1" & " - Loading Growth Rates"
            If Not context.HistoricMode Then
                'jrs 2012/06/15 Dim dbGR As Database
                'jrs 2012/06/15 Set dbGR = OpenDatabase(IFs_MDB2Active)

                'jrs 2012/06/15 Call LoadGrowthRatesOneDim("WATWASTEGR", WATWASTEGR, dbGR)
                Call LoadGrowthRatesOneDim("WATWASTEGR", WATWASTEGR, context)
                'Call LoadGrowthRatesOneDim("WATWASTETREATGR", WATWASTETREATGR, dbGR)

                'mti May 04, 2012 - start
                'jrs 2012/06/15 Call LoadGrowthRatesTwoDim("WATSAFEGR", WatSafeGR, dbGR)
                Call LoadGrowthRatesTwoDim("WATSAFEGR", WatSafeGR, context)
                'jrs 2012/06/15 Call LoadGrowthRatesTwoDim("SANITATIONGR", SanitationGR, dbGR)
                Call LoadGrowthRatesTwoDim("SANITATIONGR", SanitationGR, context)
                'mti May 04, 2012 - end

                'jrs 2012/06/15 dbGR.Close
            End If

            sErrorSub = "IFsInfraWatSan1"
            'mti February 2011 Infarstructure - end - growth rate
            '

            For R% = 1 To NR%
                '   First Year:  People with access to safe water and Sanitation: Shift
                '   -----------------------------------------------------------------------

                'mti Sep 12, 2011 - start
                'growth rate in GDPPCP; need to use GDPPCP from a year back, else there will be transients

                'POPR=Nothing
                'ttt 2019/10/08 Dim GDPPCPGR = (igdpr(R%) - POPR(R%) / 100) / (1 + POPR(R%) / 100) '0 to 1
                'mti Sep 12, 2011 - end

                'jrs 2009/02/26
                'Preparing Drivers for Full Model regression
                '-------------------------------------------
                'Rural Population
                '----------------

                'mti May 04, 2012 - start
                'RuralPop = (POP(R) - SPOPURBAN(R)) / POP(R) * 100

                'one year back
                Dim RuralPop = POP(R) - POPURBAN(R) '
                Dim Pop_Prev = POP(R%) / (1 + POPR(R%) / 100)
                Dim UrbanPop_Prev = POPURBAN(R) / (1 + POPURBGR(R%) / 100)
                Dim RuralPop_Prev = Pop_Prev - UrbanPop_Prev

                'percent rural last year
                RuralPop = RuralPop_Prev / Pop_Prev * 100
                'mti May 04, 2012 - end

                'Health Expenditures
                '-------------------
                'mti May 04, 2012 - start
                'HealthExp = GDS(R, Health) / GDP(R) * 100 
                Dim HealthExp = GDS(R, Health) / GDP(R) * 100 / (1 + GDPPCPGR(R)) 'ttt 2019/10/08 added R for GDPPCPGR
                'mti May 04, 2012 - end
                'Governance Regulatory Quality (using Efectiveness)
                'substracting -2.5 since forecast runs from 0 to 5
                'while historic variables run from -2.5 to 2.5 (used to find regression)
                '----------------------------------------------------------------------
                'jrs 2009/10/16 not used anymore Gov = GOVREGQUAL(R) - 2.5
                'GiniExtended
                '------------
                'mti February 2011 Infarstructure - start
                'jrs 2009/10/16 GiniExt = GINIDOM(R) * 100
                'Poverty = SINCOMELT1LN(R%) / POP(R%) * 100
                'Poverty = SINCOMELT1CS(R%) / POP(R%) * 100
                'mti May 04, 2012 -start
                'Poverty = INCOMELT125CS2005(R%) / POP(R%) * 100
                Poverty = EstIncomeLt1PcntPrevious(R%, context)
                'mti May 04, 2012 - end
                'mti February 2011 Infarstructure -end

                'jrs 2009/03/05 Full model regression

                'Call AnalFunc("Percent of People with No Access to Sanitation", _
                'Array(GDPPCP(R%), EDYRSAG25(R%, 3)), calSANITATION)
                'mti Sep 12, 2011 - start - last year GDPPCP

                'mti May 04, 2012 - start
                Dim EdYrsAG25Last = EDYRSAG25(R%, 3) * 0.995
                'mti April 05, 2012 - start

                'Call AnalFunc("Percent of People with No Access to Sanitation Full Model", _
                'Array(HealthExp, GDPPCP(R%) / (1 + GDPPCPGR), EDYRSAG25(R%, 3), Poverty), calSANITATION)
                Dim calsanitation As Single = 0
                Call AnalFunc("Percent of People with No Access to Sanitation Full Model",
        New Single() {HealthExp, GDPPCP(R%) / (1 + GDPPCPGR(R)), CSng(EdYrsAG25Last), Poverty}, calsanitation,
        New String() {"GovtHl%GDP", "GDPPCP", "Educ25", "INCOMELT1"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                'ttt 2019/10/08 added R to GDPPCPGR


                Dim s1 = Math.Exp(calsanitation)

                'mti May 04, 2012 - end

                'jrs 2009/03/05 Full model regression

                'Call AnalFunc("Percent of People with Access to Improved Sanitation but no HC", _
                'Array(GDPPCP(R%), EDYRSAG25(R%, 3)), calSANITATION)

                'mti May 04, 2012 - start

                'Call AnalFunc("Percent of People with Access to Improved Sanitation but no HC Full Model", _
                'Array(HealthExp, GDPPCP(R%) / (1 + GDPPCPGR), EDYRSAG25(R%, 3), Poverty), calSANITATION)

                Call AnalFunc("Percent of People with Access to Improved Sanitation but no HC Full Model",
        New Single() {HealthExp, GDPPCP(R%) / (1 + GDPPCPGR(R)), CSng(EdYrsAG25Last), Poverty}, calsanitation,
        New String() {"GovtHl%GDP", "GDPPCP", "Educ25", "INCOMELT1"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                'ttt 2019/10/08 added R to GDPPCPGR
                'mti May 04, 2012 - end

                Dim s2 = Math.Exp(calsanitation)
                'mti Sep 12, 2011 - start

                calsanitation = CSng(s1 / (1 + s1 + s2) * 100)
                calsanitation = CSng(AMAX(calsanitation, 0.0001))

                'jrs 2019/05/03 *** BEGIN ***
                If sanitationexovals(R%, 1) > -100 Then
                    SANITATION(R%, 1) = sanitationexovals(R%, 1)
                End If
                If sanitationexovals(R%, 2) > -100 Then
                    SANITATION(R%, 2) = sanitationexovals(R%, 2)
                End If
                If sanitationexovals(R%, 3) > -100 Then
                    SANITATION(R%, 3) = sanitationexovals(R%, 3)
                End If
                'jrs 2019/05/03 *** END ***

                'jrs 2009/04/24 change to Additive Shift Factor
                'SANITATIONShift(R%, 1) = SANITATION(R%, 1) / calSANITATION
                SANITATIONShift(R%, 1) = SANITATION(R%, 1) - calsanitation



                calsanitation = CSng(s2 / (1 + s1 + s2) * 100)
                calsanitation = CSng(AMAX(calsanitation, 0.0001))
                'jrs 2009/04/24 change to Additive Shift Factor
                'SANITATIONShift(R%, 2) = SANITATION(R%, 2) / calSANITATION
                SANITATIONShift(R%, 2) = SANITATION(R%, 2) - calsanitation

                ' If RegName(R%) = "Australia" Then Stop
                ' If RegName(R%) = "Libya" Then Stop
                ' If RegName(R%) = "Bahamas, The" Then Stop


                calsanitation = CSng(1 / (1 + s1 + s2) * 100)
                calsanitation = CSng(AMAX(calsanitation, 0.0001))
                'jrs 2009/04/24 change to Additive Shift Factor
                'SANITATIONShift(R%, 3) = SANITATION(R%, 3) / calSANITATION
                SANITATIONShift(R%, 3) = SANITATION(R%, 3) - calsanitation
                ' ttt temp for Dim3 primary key
                'jrs 2019/11/19
                'For j = 1 To 3
                '    ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                '        .VarName = "SANITATION",
                '        .Year = CShort(IY% + FirstYear - 1),
                '        .Region = CShort(R%),
                '        .Dim2 = CShort(j),
                '        .DistalFormVal = CSng(SANITATION(R%, j)),
                '        .Dim3 = CShort(j)
                '    })
                'Next j
                'ifsPopWorkDB.Flush()
                Call AnalFunc("Percent of People with No Access to Sanitation",
                    New Single() {GDPPCP(R%), EDYRSAG25(R%, 3)}, calsanitation,
                    New String() {"GDPPCP", "Educ25"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                s1 = Math.Exp(calsanitation)

                Call AnalFunc("Percent of People with Access to Improved Sanitation but no HC",
        New Single() {GDPPCP(R%), EDYRSAG25(R%, 3)}, calsanitation,
        New String() {"GDPPCP", "Educ25"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                s2 = Math.Exp(calsanitation)


                calsanitation = CSng(s1 / (1 + s1 + s2) * 100)
                calsanitation = CSng(AMAX(calsanitation, 0.0001))
                'jrs 2009/04/24 change to Additive Shift Factor
                'SANITATIONShift2(R%, 1) = SANITATION(R%, 1) / calSANITATION
                SANITATIONShift2(R%, 1) = SANITATION(R%, 1) - calsanitation

                calsanitation = CSng(s2 / (1 + s1 + s2) * 100)
                calsanitation = CSng(AMAX(calsanitation, 0.0001))
                'jrs 2009/04/24 change to Additive Shift Factor
                'SANITATIONShift2(R%, 2) = SANITATION(R%, 2) / calSANITATION
                SANITATIONShift2(R%, 2) = SANITATION(R%, 2) - calsanitation

                calsanitation = CSng(1 / (1 + s1 + s2) * 100)
                calsanitation = CSng(AMAX(calsanitation, 0.0001))
                'jrs 2009/04/24 change to Additive Shift Factor
                'SANITATIONShift2(R%, 3) = SANITATION(R%, 3) / calSANITATION
                SANITATIONShift2(R%, 3) = SANITATION(R%, 3) - calsanitation

                'SANITATIONShift(R%) = SANITATION(R%) - calSANITATION

                'mti Infrastructure Cost August 2010 start - commented multiplier on first year
                'jrs 2009/07/08
                '        SANITATION(R%, 1) = SANITATION(R%, 1) * sanitationm(R%, 1)
                '        SANITATION(R%, 2) = SANITATION(R%, 2) * sanitationm(R%, 2)
                '        SANITATION(R%, 3) = SANITATION(R%, 3) * sanitationm(R%, 3)

                'mti February 2011 Infarstructure -start
                'normalize to 100%
                '        calSANITATION = SANITATION(R%, 1) + SANITATION(R%, 2) + SANITATION(R%, 3)
                '        SANITATION(R%, 1) = SANITATION(R%, 1) / calSANITATION * 100
                '        SANITATION(R%, 2) = SANITATION(R%, 2) / calSANITATION * 100
                '        SANITATION(R%, 3) = SANITATION(R%, 3) / calSANITATION * 100
                'mti February 2011 Infarstructure -end
                'jrs 2009/12/22
                'If RegName(R%) = "India" Then Stop

                'jrs 2023/10/30
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("SANITATION", "Improved", "", CStr(FirstYear + IY - 1), R%, context)
                End If


                For j = 1 To 3
                    SSANITATION(R%, j) = SANITATION(R%, j)
                    sssanitation(R%, j) = SANITATION(R%, j)
                    'mti 2021/08/10 start
                    sPreBudSanitation(R%, j) = SANITATION(R%, j)
                Next j

                'mti 2014/05/28 - start
                'sPreBudSanitation(R%) = SANITATION(R%, 3)
                'mti 2021/08/10 End
                'mti 2014/05/28 - end

                'mti May Day 2012 - start
                'mti October 20, 2011 - start
                'iSanitationNone(R%) = SANITATION(R%, 1)
                'mti October 20, 2011 - end
                iSanitationHH(R%) = SANITATION(R%, 3)
                'mti May Day 2012 -end
                'jrs 2009/03/05 Full model regression

                'Call AnalFunc("Percent of People with No Access to Clean Water", _
                'Array(GDPPCP(R%), EDYRSAG25(R%, 3)), calWATSAFE)

                'mti Sep 12, 2011 - start
                'need to use GDPPCP from a year back, else there will be transients
                '        gdppcpgr = (igdpr(R%) - POPR(R%) / 100) / (1 + POPR(R%) / 100) '0 to 1
                'mti May 04, 2012 - start

                'Call AnalFunc("Percent of People with No Access to Clean Water Full Model", _
                'Array(RuralPop, HealthExp, GDPPCP(R%) / (1 + GDPPCPGR), EDYRSAG25(R%, 3), Poverty), calwatsafe)
                Dim calWATSAFE As Single = 0
                Call AnalFunc("Percent of People with No Access to Clean Water Full Model",
        New Single() {RuralPop, HealthExp, GDPPCP(R%) / (1 + GDPPCPGR(R)), CSng(EdYrsAG25Last), Poverty}, calWATSAFE,
        New String() {"PopRural%", "GovtHl%GDP", "GDPPCP", "Educ25", "INCOMELT1"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                'ttt 2019/10/08 added R to GDPPCPGR
                'mti May 04, 2012 - end
                s1 = Math.Exp(calWATSAFE)

                'jrs 2009/03/05 Full model regression

                'Call AnalFunc("Percent of People with Improved Access to Clean Water but No HC", _
                'Array(GDPPCP(R%), EDYRSAG25(R%, 3)), calWATSAFE)

                'If RegName(R%) = "India" Then Stop
                'mti May 04, 2012 - start

                'Call AnalFunc("Percent of People with Improved Access to Clean Water but No HC Full Model", _
                'Array(RuralPop, HealthExp, GDPPCP(R%) / (1 + GDPPCPGR), EDYRSAG25(R%, 3), Poverty), calwatsafe)

                Call AnalFunc("Percent of People with Improved Access to Clean Water but No HC Full Model",
        New Single() {RuralPop, HealthExp, GDPPCP(R%) / (1 + GDPPCPGR(R)), CSng(EdYrsAG25Last), Poverty}, calWATSAFE,
        New String() {"PopRural%", "GovtHl%GDP", "GDPPCP", "Educ25", "INCOMELT1"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                'ttt 2019/10/08 added R to GDPPCPGR
                s2 = Math.Exp(calWATSAFE)
                'mti May 04, 2012 - end

                'mti Sep 12, 2011 - end


                calWATSAFE = CSng(s1 / (1 + s1 + s2) * 100)
                calWATSAFE = CSng(AMAX(calWATSAFE, 0.0001))

                'jrs 2019/05/03 *** BEGIN ***
                If watsafeexovals(R%, 1) > -100 Then
                    WATSAFE(R%, 1) = watsafeexovals(R%, 1)
                End If
                If watsafeexovals(R%, 2) > -100 Then
                    WATSAFE(R%, 2) = watsafeexovals(R%, 2)
                End If
                If watsafeexovals(R%, 3) > -100 Then
                    WATSAFE(R%, 3) = watsafeexovals(R%, 3)
                End If
                'jrs 2019/05/03 *** END ***
                'jrs 2009/04/24 change to Additive Shift Factor
                'WATSAFEshift(R%, 1) = WATSAFE(R%, 1) / calWATSAFE
                WATSAFEShift(R%, 1) = WATSAFE(R%, 1) - calWATSAFE


                'If RegName(R%) = "Bhutan" Then Stop

                calWATSAFE = CSng(s2 / (1 + s1 + s2) * 100)
                calWATSAFE = CSng(AMAX(calWATSAFE, 0.0001))
                'jrs 2009/04/24 change to Additive Shift Factor
                'WATSAFEshift(R%, 2) = WATSAFE(R%, 2) / calWATSAFE
                WATSAFEShift(R%, 2) = WATSAFE(R%, 2) - calWATSAFE


                'If RegName(R%) = "India" Or RegName(R%) = "Bhutan" Or RegName(R%) = "Bangladesh" Then Stop 'Debug.Print WATSAFEShift(R%, 2); WATSAFE(R%, 2); calWATSAFE; s1; s2; RuralPop; HealthExp; GDPPCP(R%); EDYRSAG25(R%, 3); Poverty



                calWATSAFE = CSng(1 / (1 + s1 + s2) * 100)
                calWATSAFE = CSng(AMAX(calWATSAFE, 0.0001))
                'jrs 2009/04/24 change to Additive Shift Factor
                'WATSAFEshift(R%, 3) = WATSAFE(R%, 3) / calWATSAFE
                WATSAFEShift(R%, 3) = WATSAFE(R%, 3) - calWATSAFE

                'Debug.Print Replace(RegName(R%), ",", " "); ","; WATSAFE(R%, 3); ","; WATSAFEShift(R%, 3)

                'jrs 2009/03/23 Distal driver formulation
                'ttt temp Dim3
                'jrs 2019/11/19
                'For j = 1 To 3
                '    ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                '        .VarName = "WATSAFE",
                '        .Year = CShort(IY% + FirstYear - 1),
                '        .Region = CShort(R%),
                '        .Dim2 = CShort(j),
                '         .Dim3 = CShort(j),
                '        .DistalFormVal = CSng(WATSAFE(R%, j))
                '    })
                'Next j
                'ifsPopWorkDB.Flush()

                Call AnalFunc("Percent of People with No Access to Clean Water",
        New Single() {GDPPCP(R%), EDYRSAG25(R%, 3)}, calWATSAFE,
        New String() {"GDPPCP", "Educ25"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                s1 = Math.Exp(calWATSAFE)

                Call AnalFunc("Percent of People with Improved Access to Clean Water but No HC",
        New Single() {GDPPCP(R%), EDYRSAG25(R%, 3)}, calWATSAFE,
        New String() {"GDPPCP", "Educ25"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                s2 = Math.Exp(calWATSAFE)

                calWATSAFE = CSng(s1 / (1 + s1 + s2) * 100)
                calWATSAFE = CSng(AMAX(calWATSAFE, 0.0001))
                'jrs 2009/04/24 change to Additive Shift Factor
                'WATSAFEshift2(R%, 1) = WATSAFE(R%, 1) / calWATSAFE
                WATSAFEShift2(R%, 1) = WATSAFE(R%, 1) - calWATSAFE

                calWATSAFE = CSng(s2 / (1 + s1 + s2) * 100)
                calWATSAFE = CSng(AMAX(calWATSAFE, 0.0001))
                'jrs 2009/04/24 change to Additive Shift Factor
                'WATSAFEshift2(R%, 2) = WATSAFE(R%, 2) / calWATSAFE
                WATSAFEShift2(R%, 2) = WATSAFE(R%, 2) - calWATSAFE

                calWATSAFE = CSng(1 / (1 + s1 + s2) * 100)
                calWATSAFE = CSng(AMAX(calWATSAFE, 0.0001))
                'jrs 2009/04/24 change to Additive Shift Factor
                'WATSAFEshift2(R%, 3) = WATSAFE(R%, 3) / calWATSAFE
                WATSAFEShift2(R%, 3) = WATSAFE(R%, 3) - calWATSAFE

                'WATSAFEShift(R%) = WATSAFE(R%) - calWATSAFE
                'mti Infrastructure Cost August 2010 start - commented multiplier on first year
                'jrs 2009/07/08
                '        WATSAFE(R%, 1) = WATSAFE(R%, 1) * watsafem(R%, 1)
                '        WATSAFE(R%, 2) = WATSAFE(R%, 2) * watsafem(R%, 2)
                '        WATSAFE(R%, 3) = WATSAFE(R%, 3) * watsafem(R%, 3)
                'If RegName(R%) = "Albania" Then Stop
                'normalize to 100%
                'mti February 2011 Infarstructure -start
                '        calWATSAFE = WATSAFE(R%, 1) + WATSAFE(R%, 2) + WATSAFE(R%, 3)
                '        WATSAFE(R%, 1) = WATSAFE(R%, 1) / calWATSAFE * 100
                '        WATSAFE(R%, 2) = WATSAFE(R%, 2) / calWATSAFE * 100
                '        WATSAFE(R%, 3) = WATSAFE(R%, 3) / calWATSAFE * 100
                'mti February 2011 Infarstructure - end

                'mti sanitation June 2006 -end add watsafe historic, impose limit
                'mti Infrastructure Cost August 2010 start
                '---estimated per capita income at PPP for the second year ---------------

                'mti February 2011 Infarstructure - start
                '            If frm_MainMenu.IFsGUIctrl1.cHistoricBase = True Then
                '                TbName$ = "GDP/Capita Versus GDP/Capita at Purchasing Power Parity (1980)"
                '                Call XYTABL(TbName$, GDP(R%) / POP(R%), pppcomp1) 'GDPPCP derived from GDPPC using a function
                '                If GDP(R%) / POP(R%) > 14.29 Then pppcomp1 = GDP(R%) / POP(R%) 'richer countries: GDPPCP = GPPC
                '            Else
                '                TbName$ = "GDP/Capita Versus GDP/Capita at Purchasing Power Parity (2000) Power Function"
                '                Call XYTABL(TbName$, GDP(R%) / POP(R%), pppcomp1) 'GDPPCP derived from GDPPC using a function
                '                If GDP(R%) / POP(R%) > 17.2 Then pppcomp1 = GDP(R%) / POP(R%) 'richer countries: GDPPCP = GPPC
                '            End If
                '            epop = POP(R%) * (1 + POPR(R%) / 100) 'estimated population next year
                '            EGDP = GDP(R%) * (1 + igdpr(R%)) 'estimated GDP next year
                '            egdppc = EGDP / epop 'estimated GDP per capita
                '            Call XYTABL(TbName$, egdppc, pppcomp2)  'estimated GDP per capita at PPP
                '            If frm_MainMenu.IFsGUIctrl1.cHistoricBase <> True And egdppc > 17.2 Then
                '                pppcomp2 = egdppc
                '            End If
                '            egdppcp = GDPPCP(R%) * pppcomp2 / pppcomp1 're-estimated GDP PC at PPP next year
                'mti February 2011 Infarstructure - end
                '------------------------------------------------------------------------------

                'First Year: Infrastructure Sanitation Cost
                '------------------------------------------

                '            'estimate sanitation changes in first year using simple logarithmic function
                '            TbName$ = "GDP/Capita (PPP 2000) Vs Sanitation pcnt of people with HC (" & CStr(BaseYear) & ") Log"
                '            TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear)
                '
                '            Call XYTABL(TbName$, GDPPCP(R%), SanitCon)
                '            Call XYTABL(TbName$, egdppcp, eSanitCon)
                '
                '            SanitConadded = AMAX(0, (eSanitCon * epop - SanitCon * POP(R%)) * 10000 / HHSIZE(R%))
                '            CalcInfraUnitCost 10, 5, sanitationcostupper, sanitationcostlower, GDPPCP(R%), SANITATIONCOST(R%)
                '            INFRAINVESTNEW(R%, SanitHH) = SanitConadded * SANITATIONCOST(R%) / 1000000 'billion dollars
                'mti January 2011 - start
                DeppcntWS(SanitationHH) = Math.Exp(Math.Log(2) / sanitationlife) - 1

                DeppcntWS(SanitationImproved) = Math.Exp(Math.Log(2) / sanitationimplife) - 1
                'mti January 2011 - end
                '            INFRAINVESTMAINT(R%, SanitHH) = SANITATION(R%, 3) * POP(R%) * 10000 * DeppcntWS(SanitHH) * SANITATIONCOST(R%) / 1000000 / HHSIZE(R%) 'billion dollars

                'First Year: Infrastructure Water Connection Cost
                '------------------------------------------------

                '            'estimate sanitation changes in first year using simple logarithmic function
                '            TbName$ = "GDP/Capita (PPP 2000) Vs Water safe pcnt of people with HC (" & CStr(BaseYear) & ") Log"
                '            TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear)
                '
                '            Call XYTABL(TbName$, GDPPCP(R%), WatSafeCon)
                '            Call XYTABL(TbName$, egdppcp, eWatSafeCon)
                '
                '            WatSafeConAdded = AMAX(0, (eWatSafeCon * epop - WatSafeCon * POP(R%)) * 10000 / HHSIZE(R%))
                '            CalcInfraUnitCost 10, 5, watsafecostupper, watsafecostlower, GDPPCP(R%), WATSAFECOST(R%)
                '            INFRAINVESTNEW(R%, WatSafeHH) = WatSafeConAdded * WATSAFECOST(R%) / 1000000 'billion dollars
                'mti January 2011 - start
                DeppcntWS(SafeWaterHH) = Math.Exp(Math.Log(2) / watsafelife) - 1
                DeppcntWS(SafeWaterImproved) = Math.Exp(Math.Log(2) / watsafeimplife) - 1
                'mti January 2011 - end
                '            INFRAINVESTMAINT(R%, WatSafeHH) = WATSAFE(R%, 3) * POP(R%) * 10000 * DeppcntWS(WatSafeHH) * WATSAFECOST(R%) / 1000000 / HHSIZE(R%) 'billion dollars

                'mti February 2011 Infarstructure -start
                DeppcntWS(WasteWater) = Math.Exp(Math.Log(2) / watwastetreatlife) - 1
                'mti February 2011 Infarstructure -end

                'mti 2022/10/20 start
                Deppcnt(SanitationHH) = DeppcntWS(SanitationHH)
                Deppcnt(SanitationImproved) = DeppcntWS(SanitationImproved)
                Deppcnt(SafeWaterHH) = DeppcntWS(SafeWaterHH)
                Deppcnt(SafeWaterImproved) = DeppcntWS(SafeWaterImproved)
                Deppcnt(WasteWater) = DeppcntWS(WasteWater)
                'mti 2022/10/20 End

                'mti July 25, 2012 - start

                'mti August 17, 2011 - start
                Dim PopUrbanTerm = 100 * POPURBAN(R%) / POP(R%)
                Prediction = CSng(SANITATION(R%, 3) / (1 + Math.Exp(2.339779 - 0.042719 * GDPPCP(R%) - 0.041923 * PopUrbanTerm)))
                WatWasteColShift(R%) = WATWASTE(R%) - Prediction
                'mti August 17, 2011 - end
                'mti July 25, 2012 - end


                'mti February 2011 Infarstructure - start

                'First Year - WasteWater Treatment Shift Factor
                'mti - (moving average) Growth rate approach will be problematic when one variable converges to other;
                'With initial data/prediction gap on the first one not yet merged, the second one might converge and thus get a miniscule growth rate
                'with moving average this slower growth rate subsequently creates a divergence for some time


                'model gives % of collected that is treated
                'mti Sep 12, 2011 - start
                'Prediction = WATWASTE(R%) / (1 + Exp(1.036711 - 0.42925 * WATWASTE(R%)))
                Prediction = CSng(100 / (1 + Math.Exp(2.4821024086192 - 0.0382380937051429 * GDPPCP(R%) / (1 + GDPPCPGR(R)) - 0.0288342684215034 * WATWASTE(R%)))) 'ttt 2019/10/08 added R to GDPPCPGR


                'mti August 22, 2011 -start
                'new model is as % of pop
                'PcntColTreated = 100 * WATWASTETREAT(R%) / WATWASTE(R%)
                'mti Sep 12, 2011 - end
                WatWasteTreatShift(R%) = WATWASTETREAT(R%) - Prediction
                'mti August 22, 2011 -end

                sHHSIZEWS(R%) = HHSIZE(R%)
                'mti Infrastructure Cost August 2010 end
                'mti WatSafe->MalnchpSH May 2006 -end

                'If RegName(R%) = "Albania" Then Debug.Print HealthExp, GDPPCP(R%), EDYRSAG25(R%, 3), Poverty, RuralPop, WATSAFE(R%, 3)

                'jrs 2023/10/30
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("WATSAFE", "Piped", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                'jrs 2009/12/23
                For j = 1 To 3
                    SWATSAFE(R%, j) = WATSAFE(R%, j)
                    'mti 2021/08/10 start
                    sPreBudWATSAFE(R%, j) = WATSAFE(R%, j)
                Next j

                'mti 2014/05/28 - start
                'sPreBudWATSAFE(R%) = WATSAFE(R%, 3)
                'mti 2014/05/28 - end
                'mti 2021/08/10 end  sPreBudWATSAFE(R%, j)

                'mti October 20, 2011 - start
                iWatSafeNone(R%) = WATSAFE(R%, 1)
                'mti October 20, 2011 - end

                'mti February 2011 Infarstructure - start
                ' To avoid run to run
                iWatWasteTreatCost(R%) = WATWASTETREATCOST(R%)
                SWATWASTETREAT(R%) = WATWASTETREAT(R%)
                sWATWASTE(R%) = WATWASTE(R%)

                'mti July 18, 2011 - start
                PreMultWATWASTE(R%) = WATWASTE(R%)
                'sPreMultWATWASTE(R%) = WATWASTE(R%)

                '            PreMultWATWASTEGR(R%) = WATWASTEGR(R%)
                '            sPreMultWATWASTEGR(R%) = WATWASTEGR(R%)

                'mti July 18, 2011 - END

                sGDPPCP(R%) = GDPPCP(R%)
                'mti February 2011 Infarstructure - end

                'save variables to hold base forecast values (required for a target scenario)
                For j = 1 To 3
                    sSanitationPreMult(R%, j) = SANITATION(R%, j)
                    sWATSAFEPreMult(R%, j) = WATSAFE(R%, j)

                    SANITATIONBF(R%, j) = SANITATION(R%, j)
                    sSanitationBF(R%, j) = SANITATION(R%, j)

                    WatSafeBF(R%, j) = WATSAFE(R%, j)
                    sWatSafeBF(R%, j) = WATSAFE(R%, j)

                    'mti Aug 21, 2012 -start
                    iSanitation(R%, j) = SANITATION(R%, j)
                    'mti Aug 21, 2012 - end

                Next j

                WATWASTEBF(R%) = WATWASTE(R%)
                PreMultWATWASTEBF(R%) = WATWASTE(R%)
                WatWasteGRBF(R%) = WATWASTEGR(R%)

                WatWasteTreatBF(R%) = WATWASTETREAT(R%)

                '******** First Year: Water and Sanitation *********************
                '******************* end   *************************************
                'jrs 2015/03/19 *** begin ***
                For j = 1 To 3
                    WATERFILTEREDI(R%, j) = WATERFILTERED(R%, j)
                Next j
                'jrs 2015/03/19 *** end ***
            Next
            'mti Dec 12, 2010 - start
            '=>GoTo AllYears
            'ttt:  NOT APPLIED : AllYears: If IY > 1 Then CalcInfraindices
            'Else
            'AllButFirst
            'End If

            'mti 2022/09/16 start
            'mti 2022/10/20 start
            'If watsafenewsw < 1 Then    'write only when forward linkage from new (5 category model) is off
            If watsafenewsw < 1 And watsafefwdsw < 1 Then
                'mti 2022/10/20 end
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(SANITATION, NR, IY + FirstYear - 1, "SANITATION", 3))
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(WATSAFE, NR, IY + FirstYear - 1, "WATSAFE", 3))
                'ifsPopWorkDB.BulkInsertHLProxDrivers(SANITATION, NR, IY + FirstYear - 1, "SANITATION", 3)
                'ifsPopWorkDB.BulkInsertHLProxDrivers(WATSAFE, NR, IY + FirstYear - 1, "WATSAFE", 3)
            End If
        End Sub

        Public Sub IFsInfraWATSAN1AllbutFirstYear(context As IForecastContext)

            'call1:
            'mti Dec 12, 2010 - end
            'If IY >= 90 Then Stop
            Dim EffectSE As Single = 0

            For R% = 1 To NR%
                'If IY >= 26 And RegName(R%) = "DominicanRep" Then Stop

                '******** All Years but first: Water and Sanitation *********************
                '***************************** begin *************************************
                'Last change on:
                'jrs 2009/03/23

                'Debug.Print RegName(R%)
                'If IY >= 90 Then Stop


                ' If RegName(R%) = "China" Then Stop

                'jrs 2009/03/05
                'Preparing Drivers for Full Model regression
                '-------------------------------------------
                'Rural Population
                '----------------
                'mti February 2011 Infarstructure - start
                'RuralPop = (POP(R) - SPOPURBAN(R)) / POP(R) * 100
                Dim RuralPop = (sPOPTotal(R%) - SPOPURBAN(R)) / sPOPTotal(R%) * 100
                'Health Expenditures
                '-------------------
                'HealthExp = GDS(R, Health) / GDP(R) * 100
                Dim HealthExp = sHlthExpPcntGDP(R%)

                'Governance Regulatory Quality (using Efectiveness)
                'substracting -2.5 since forecast runs from 0 to 5
                'while historic variables run from -2.5 to 2.5 (used to find regression)
                '----------------------------------------------------------------------
                'jrs 2009/10/16 not used anymore Gov = GOVREGQUAL(R) - 2.5
                'GiniExtended
                '------------
                'mti February 2011 Infarstructure - start
                'jrs 2009/10/16 GiniExt = GINIDOM(R) * 100
                'Poverty = SINCOMELT1LN(R%) / POP(R%) * 100
                'Poverty = SINCOMELT1CS(R%) / POP(R%) * 100
                Dim Poverty = SINCOMELT1CS(R%) / sPOPTotal(R%) * 100

                'If RegName(R%) = "Chad" Then Debug.Print IY%; SINCOMELT1CS(R%); INCOMELT125CS2005(R%); SINCOMELT1LN(R%)

                'If RegName(R%) = "Burkina Faso" Then Stop

                'jrs 2010/01/07
                If sanithldsw = 0 Then
                    'jrs 2009/03/05 Full Model Regression
                    'Call AnalFunc("Percent of People with No Access to Sanitation", _
                    'Array(GDPPCP(R%), EDYRSAG25(R%, 3)), calSANITATION)
                    'Call AnalFunc("Percent of People with No Access to Sanitation Full Model", _
                    'Array(HealthExp, GDPPCP(R%), EDYRSAG25(R%, 3), Poverty), calSANITATION)
                    Dim calsanitation As Single = 0
                    Call AnalFunc("Percent of People with No Access to Sanitation Full Model",
                New Single() {HealthExp, sGDPPCP(R%), SEDYRSAG25(R%, 3), Poverty}, calsanitation,
                New String() {"GovtHl%GDP", "GDPPCP", "Educ25", "INCOMELT1"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                    Dim s1 = Math.Exp(calsanitation)

                    'jrs 2009/03/05 Full Model Regression
                    'Call AnalFunc("Percent of People with Access to Improved Sanitation but no HC", _
                    'Array(GDPPCP(R%), EDYRSAG25(R%, 3)), calSANITATION)
                    'Call AnalFunc("Percent of People with Access to Improved Sanitation but no HC Full Model", _
                    'Array(HealthExp, GDPPCP(R%), EDYRSAG25(R%, 3), Poverty), calSANITATION)
                    Call AnalFunc("Percent of People with Access to Improved Sanitation but no HC Full Model",
                New Single() {HealthExp, sGDPPCP(R%), SEDYRSAG25(R%, 3), Poverty}, calsanitation,
                New String() {"GovtHl%GDP", "GDPPCP", "Educ25", "INCOMELT1"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                    'mti February 2011 Infarstructure -end
                    Dim s2 = Math.Exp(calsanitation)

                    calsanitation = CSng(s1 / (1 + s1 + s2) * 100)
                    'jrs 2009/04/24 change to Additive Shift Factor
                    '            If SANITATIONShift(R%, 1) <> 0 Then 'Convergence of a 0 factor creates a transient
                    '                SANITATION(R%, 1) = calSANITATION * ConvergeOverTime(SANITATIONShift(R%, 1), 1, 100)
                    '            Else
                    '                SANITATION(R%, 1) = calSANITATION * SANITATIONShift(R%, 1)
                    '            End If
                    'If RegName(R%) = "Singapore" Then Stop

                    'mti July 18, 2011 -start
                    Dim SanNoAdd As Single = 0
                    'mti 2021/08/10 start
                    '                If SANITATIONShift(R%, 1) > 0 Then
                    '                    SanNoAdd = ConvergeOverTime(SANITATIONShift(R%, 1), 0, watsanconv)  '100
                    '                Else
                    '                    SanNoAdd = SANITATIONShift(R%, 1) '+ ConvergeOverTime(SANITATIONShift(R%, 1), 0, 1000)
                    '                End If
                    SanNoAdd = ConvergeOverTime(SANITATIONShift(R%, 1), 0, watsanconv)  '100
                    'mti 2021/08/10 End
                    'mti July 18, 2011 -end

                    'mti October 20, 2011 -start
                    'BBH/JRS SE approach
                    'dkb 2013/09/16 added dimension 1 to sanitnoconsetar and sanitnoconseyrtar

                    If sanitnoconsetar(R%) <> 0 Then
                        'mti April 05, 2012 - start
                        'SanNoSE = 12.8
                        Dim SanNoSE = 10
                        'mti April 05, 2012 -end
                        'mti July 05, 2012 - target bound
                        EffectSE = StdErrorAdditiveShift(CSng(SanNoSE), CSng(calsanitation), CSng(SanNoAdd), CSng(SANITATIONShift(R%, 1)),
                        CSng(sanitnoconsetar(R%)), CInt(FirstYear + sanitnoconseyrtar(R%)), FirstYear + IY - 1, FirstYear)
                        'SE, Prediction, DataAddShift, IDataAddShift, TargetSEs, TargetYr, CurrentYr
                    Else
                        EffectSE = 0
                    End If

                    'mti note: JRS original model did not have upper and lower bounds here; Check if that makes a difference
                    'If IY + FirstYear - 1 = 2044 And GRPS(1, R) = "Liberia" Then Stop
                    SANITATION(R%, 1) = calsanitation + SanNoAdd + EffectSE
                    'mti October 20, 2011 - end

                    SANITATION(R%, 1) = AMAX(Amin(SANITATION(R%, 1), 100), 0)

                    sSanitationBF(R%, 1) = SANITATIONBF(R%, 1)
                    SANITATIONBF(R%, 1) = AMAX(Amin(calsanitation + SanNoAdd, 100), 0)
                    'mti April 05, 2012 -start
                    'comment out getting category 2 as a residual
                    'mti July 18, 2011 -start
                    '                calSANITATION = s2 / (1 + s1 + s2) * 100
                    '
                    '                'jrs 2009/04/24 change to Additive Shift Factor
                    '    '            If SANITATIONShift(R%, 2) <> 0 Then
                    '    '                SANITATION(R%, 2) = calSANITATION * ConvergeOverTime(SANITATIONShift(R%, 2), 1, 100)
                    '    '            Else
                    '    '                SANITATION(R%, 2) = calSANITATION * SANITATIONShift(R%, 2)
                    '    '            End If
                    '
                    '                'mti February 28, 2012 - new start
                    '                If SANITATIONShift(R%, 2) < 0 Then
                    '                    SANITATION(R%, 2) = calSANITATION + ConvergeOverTime(SANITATIONShift(R%, 2), 0, watsanconv) '100
                    '                Else
                    '                    'very slow or negligible downward convergence
                    '                    SANITATION(R%, 2) = calSANITATION + SANITATIONShift(R%, 2) 'ConvergeOverTime(SANITATIONShift(R%, 3), 0, 1000)
                    '                End If
                    '
                    '
                    '                'mti: improved can go up by taking from none or go down by giving to household;
                    '                'SANITATION(R%, 2) = calSANITATION + ConvergeOverTime(SANITATIONShift(R%, 2), 0, 100)
                    '                'mti February 28, 2012 -new end
                    '
                    '
                    '                SANITATION(R%, 2) = AMAX(Amin(SANITATION(R%, 2), 100), 0)
                    'mti July 18, 2011 - end
                    'mti April 05, 2012 -end
                    '   If RegName(R%) = "Jordan" Then Stop
                    'mti April 05, 2012 -start
                    'category 2: go back to using the model result for category 2 (as opposed to getting as a residual
                    calsanitation = CSng(s2 / (1 + s1 + s2) * 100)

                    If SANITATIONShift(R%, 2) < 0 Then
                        'mti April 05, 2012 -change shift convergendce time (75 to 20) to make sure growing improved grows for some time
                        'mti Aug 21, 2012 -start - however for high income, very high hh this might result in growing improved (categ 2); so slow them down
                        If iSanitation(R%, 3) < 95 Then
                            SANITATION(R%, 2) = calsanitation + ConvergeOverTime(SANITATIONShift(R%, 2), 0, 30) '20 100
                        Else
                            'for high income, very high hh this might result in growing improved (categ 2); so slow them down
                            SANITATION(R%, 2) = calsanitation + ConvergeOverTime(SANITATIONShift(R%, 2), 0, 150) ' watsanconv '20 100
                        End If
                        'mti Aug 21, 2012 -start
                    Else
                        'needs downward convergence - or there will be remaining improved
                        'mti May 04, 2012 - 50 to watsanconv
                        SANITATION(R%, 2) = calsanitation + ConvergeOverTime(SANITATIONShift(R%, 2), 0, watsanconv) '50 ' SANITATIONShift(R%, 2) 'ConvergeOverTime(SANITATIONShift(R%, 3), 0, 1000)
                    End If



                    SANITATION(R%, 2) = AMAX(Amin(SANITATION(R%, 2), 100), 0)


                    sSanitationBF(R%, 2) = SANITATIONBF(R%, 2)
                    SANITATIONBF(R%, 2) = SANITATION(R%, 2)


                    'mti April 05, 2012 -end
                    'If RegName(R%) = "Afghanistan" Then Stop
                    'If RegName(R%) = "India" Then Stop
                    'If RegName(R%) = "Burkina Faso" Then Stop


                    calsanitation = CSng(1 / (1 + s1 + s2) * 100)
                    'jrs 2009/04/24 change to Additive Shift Factor
                    '            If SANITATIONShift(R%, 3) <> 0 Then
                    '                SANITATION(R%, 3) = calSANITATION * ConvergeOverTime(SANITATIONShift(R%, 3), 1, 100)
                    '            Else
                    '                SANITATION(R%, 3) = calSANITATION * SANITATIONShift(R%, 3)
                    '            End If


                    'mti July 18, 2011 - start
                    'mti 2021/08/10 start
                    '                If SANITATIONShift(R%, 3) < 0 Then
                    '                    SANITATION(R%, 3) = calSANITATION + ConvergeOverTime(SANITATIONShift(R%, 3), 0, watsanconv) '100
                    '                Else
                    '                    'very slow or negligible downward convergence
                    '                    SANITATION(R%, 3) = calSANITATION + SANITATIONShift(R%, 3) 'ConvergeOverTime(SANITATIONShift(R%, 3), 0, 1000)
                    '                End If
                    SANITATION(R%, 3) = calsanitation + ConvergeOverTime(SANITATIONShift(R%, 3), 0, watsanconv) '100
                    'mti 2021/08/10 End
                    'mti July 18, 2011 - end

                    'mti July 18, 2011 - start
                    'jrs 2009/07/07
                    'mti: take it down
                    '                SANITATION(R%, 3) = SANITATION(R%, 3) * sanitationm(R%, 3)
                    'mti July 18, 2011 - end

                    SANITATION(R%, 3) = AMAX(Amin(SANITATION(R%, 3), 100), 0)


                    'mti Aug 21, 2012 - start
                    '                If SSANITATION(R%, 3) > 99.5 Or SANITATION(R%, 3) > 99.5 Then
                    '                    SANITATION(R%, 2) = 0
                    '                    SANITATION(R%, 1) = 100 - SANITATION(R%, 3) - SANITATION(R%, 2)
                    '                End If
                    'mti Aug 21, 2012 - end

                    'mti May 04, 2012 -start
                    'simple extrapolative approach
                    '            If RegName(R%) = "Benin" Then Stop
                    'ttt vbb: If Not IsNull(SanitationGR(R%, 2)) And etc
                    If sSanitationPreMult(R%, 2) <> 0 Then
                        If SanitationGR(R%, 2) > -9999 Then
                            SanitationGR(R%, 2) = CSng(AMAX(-0.1, Amin(0.1, 0.15 * (SANITATION(R%, 2) / sSanitationPreMult(R%, 2) - 1) + 0.85 * SanitationGR(R%, 2))))
                        Else
                            SanitationGR(R%, 2) = CSng(AMAX(-0.1, Amin(0.1, 0.15 * (SANITATION(R%, 2) / sSanitationPreMult(R%, 2) - 1))))
                        End If
                        SANITATION(R%, 2) = CSng(Amin(100, AMAX(1, sSanitationPreMult(R%, 2) * (1 + SanitationGR(R%, 2)))))
                        sSanitationPreMult(R%, 2) = SANITATION(R%, 2)
                    End If

                    If sSanitationPreMult(R%, 3) <> 0 Then
                        If SanitationGR(R%, 3) > -9999 Then
                            SanitationGR(R%, 3) = CSng(AMAX(-0.1, Amin(0.1, 0.2 * (SANITATION(R%, 3) / sSanitationPreMult(R%, 3) - 1) + 0.8 * SanitationGR(R%, 3))))
                        Else
                            SanitationGR(R%, 3) = CSng(AMAX(-0.1, Amin(0.1, 0.2 * (SANITATION(R%, 3) / sSanitationPreMult(R%, 3) - 1))))
                        End If
                        SANITATION(R%, 3) = CSng(Amin(100, AMAX(1, sSanitationPreMult(R%, 3) * (1 + SanitationGR(R%, 3)))))
                        sSanitationPreMult(R%, 3) = SANITATION(R%, 3)
                    End If


                    'mti May 04, 2012 - end



                    sSanitationBF(R%, 3) = SANITATIONBF(R%, 3)
                    SANITATIONBF(R%, 3) = SANITATION(R%, 3)

                    'mti April 05, 2012 -start
                    '                'comment out getting category 2 as a residual
                    '
                    '                'mti July 18, 2011 - start
                    '                'mti: in case no connection plus household connection is greater than 100, normalize them
                    '                SomeofTwo = SANITATION(R%, 1) + SANITATION(R%, 3)
                    '                If SomeofTwo > 100 Then
                    '                    SANITATION(R%, 1) = SANITATION(R%, 1) * 100 / SomeofTwo
                    '                    SANITATION(R%, 3) = SANITATION(R%, 3) * 100 / SomeofTwo
                    '                End If
                    '
                    '
                    '                SANITATION(R%, 2) = 100 - (SANITATION(R%, 1) + SANITATION(R%, 3))
                    '                'mti July 18, 2011 - end
                    'mti April 05, 2012 - end


                    'now do all multipliers
                    SANITATION(R%, 1) = SANITATION(R%, 1) * sanitationm(R%, 1)
                    SANITATION(R%, 2) = SANITATION(R%, 2) * sanitationm(R%, 2)
                    SANITATION(R%, 3) = SANITATION(R%, 3) * sanitationm(R%, 3)


                    'mti: only a post-multiplier normalize needed now
                    'mti April 05, 2012 -start
                    'If sanitationm(R%, 1) <> 1 Or sanitationm(R%, 2) <> 1 Or sanitationm(R%, 3) <> 1 Then
                    'mti April 05, 2012 - end
                    'normalize to 100%
                    calsanitation = SANITATION(R%, 1) + SANITATION(R%, 2) + SANITATION(R%, 3)

                    'jrs 2009/11/16
                    'if multipliers are driving 2 variables to 0, and the other one is already 0
                    If calsanitation = 0 Then
                        Select Case True
                            Case sanitationm(R%, 1) > 0
                                SANITATION(R%, 1) = 1
                                calsanitation = 1
                            Case sanitationm(R%, 2) > 0
                                SANITATION(R%, 2) = 1
                                calsanitation = 1
                            Case sanitationm(R%, 3) > 0
                                SANITATION(R%, 3) = 1
                                calsanitation = 1
                        End Select
                        If calsanitation = 0 Then
                            Debug.WriteLine("At least one of the sanitation multipliers has to be non-zero for " & GRPS(1, R) & "-Water and Sanitation")
                            Exit Sub
                        End If
                    End If

                    'mti June 04, 2012 -start
                    'already established access rates cannot go down unless there is a hard multiplier sanitationm(R%, 3)
                    'hh conn
                    If sanitationm(R%, 3) >= 1 Then
                        'mti 2021/08/10 start sPreBudSanitation(R%)
                        'SANITATION(R%, 3) = AMAX(SSANITATION(R%, 3), SANITATION(R%, 3))
                        SANITATION(R%, 3) = AMAX(sPreBudSanitation(R%, 3), SANITATION(R%, 3))
                        'mti 2021/08/10 End
                        calsanitation = SANITATION(R%, 1) + SANITATION(R%, 2) + SANITATION(R%, 3)
                    End If

                    'improved can go up or down; thus we cannot protect the last year level;
                    'this gives rise to a tiny oscillation (e.g., Eritrea in the 1950s to 1960s)

                    '         If IY >= 69 And RegName(R%) = "DominicanRep" Then Stop


                    'none
                    If sanitationm(R%, 1) <= 1 Then
                        'mti 2021/08/10 start
                        'SANITATION(R%, 1) = Amin(SSANITATION(R%, 1), SANITATION(R%, 1))
                        SANITATION(R%, 1) = Amin(sPreBudSanitation(R%, 1), SANITATION(R%, 1))
                        'mti 2021/08/10 End
                        calsanitation = SANITATION(R%, 1) + SANITATION(R%, 2) + SANITATION(R%, 3)
                    End If
                    'mti June 04, 2012 - end

                    '         If IY >= 69 And RegName(R%) = "DominicanRep" Then Stop

                    SANITATION(R%, 1) = SANITATION(R%, 1) / calsanitation * 100
                    SANITATION(R%, 2) = SANITATION(R%, 2) / calsanitation * 100
                    SANITATION(R%, 3) = SANITATION(R%, 3) / calsanitation * 100

                    'normalize to 100% - for base forecast variables (separate calculation required for target cases - set condition here)
                    calsanitation = SANITATIONBF(R%, 1) + SANITATIONBF(R%, 2) + SANITATIONBF(R%, 3)

                    'if multipliers are driving 2 variables to 0, and the other one is already 0
                    If calsanitation = 0 Then
                        Select Case True
                            Case sanitationm(R%, 1) > 0
                                SANITATIONBF(R%, 1) = 1
                                calsanitation = 1
                            Case sanitationm(R%, 2) > 0
                                SANITATIONBF(R%, 2) = 1
                                calsanitation = 1
                            Case sanitationm(R%, 3) > 0
                                SANITATIONBF(R%, 3) = 1
                                calsanitation = 1
                        End Select
                        If calsanitation = 0 Then
                            Debug.WriteLine("At least one of the sanitation multipliers has to be non-zero for " & GRPS(1, R) & "-Water and Sanitation")
                            Exit Sub
                        End If
                    End If

                    SANITATIONBF(R%, 1) = SANITATIONBF(R%, 1) / calsanitation * 100
                    SANITATIONBF(R%, 2) = SANITATIONBF(R%, 2) / calsanitation * 100
                    SANITATIONBF(R%, 3) = SANITATIONBF(R%, 3) / calsanitation * 100



                    'mti April 05, 2012 -start
                    'End If
                    'mti April 05, 2012 -end
                    'mti July 18, 2011 - end

                    'mti May 05, 2011 - start
                    'parameter goal seek - start
                    If sanitationtrgtyr(R%) > 0 Then
                        'If sINFRAELECACC(R%, Urban) < 100 Then
                        'mti June 08, 2011 - start
                        '                        RatChange = Amin(1, 0.05 / AMAX(0.001, SSANITATION(R%, 1)))
                        '
                        '                        'ConvTime = AMAX(3, sanitationtrgtyr(R%)- 5 - IY) ' 30  'years
                        '                        ConvTime = AMAX(1, sanitationtrgtyr(R%)- IY)  ' 30  'years
                        '                        'mti June 08, 2011 - end
                        '                        cGrowthRateTarget = RatChange ^ (1 / ConvTime) - 1
                        '                        SANITATIONNoTrgt = SSANITATION(R%, 1) * (1 + cGrowthRateTarget)

                        'mti October 20, 2011 - start
                        '                        DistTrgt = AMAX(0, SSANITATION(R%, 1) - 0)
                        '                        TimeLeft = AMAX(1, sanitationtrgtyr(R%) + 2 - IY)

                        '                        SANITATIONNoTrgt = AMAX(0, SSANITATION(R%, 1) - DistTrgt / TimeLeft)

                        'mti May Day 2012 - start
                        '-----old code --- 0% None
                        'SANITATIONNoTrgt = iSanitationNone(R%) - Amin(sanitationtrgtyr(R%), IY) * iSanitationNone(R%) / sanitationtrgtyr(R%)

                        'mti October 20, 2011 - end

                        'mti June 08, 2011 -end

                        '                        SANITATIONNoInitCast = SANITATION(R%, 1)
                        '                        SANITATION(R%, 1) = Amin(SANITATIONNoInitCast, SANITATIONNoTrgt)
                        '
                        '                        tdef = SANITATIONNoInitCast - SANITATION(R%, 1)
                        '
                        '                        If tdef > 0 Then
                        '                            tother = SANITATION(R%, 2) + SANITATION(R%, 3)
                        '                            SANITATION(R%, 2) = SANITATION(R%, 2) + tdef * SANITATION(R%, 2) / tother
                        '                            SANITATION(R%, 3) = SANITATION(R%, 3) + tdef * SANITATION(R%, 3) / tother
                        '                        End If

                        '                   If RegName(R%) = "Burundi" Then Stop

                        '-----new code --- 100% to HH
                        'mti July 25, 2012 - start
                        'SanitationHHTrgt = Amin(100, iSanitationHH(R%) + Amin(sanitationtrgtyr(R%), IY) * (sanitationtrgtval(R%) - iSanitationHH(R%)) / sanitationtrgtyr(R%))
                        Dim SanitationHHTrgt = Amin(100, iSanitationHH(R%) + Amin(sanitationtrgtyr(R%), IY - 1) * (sanitationtrgtval(R%) - iSanitationHH(R%)) / sanitationtrgtyr(R%))
                        'mti July 25, 2012 - end
                        Dim SanitationHHInitCast = SANITATION(R%, 3)
                        SANITATION(R%, 3) = AMAX(SanitationHHInitCast, SanitationHHTrgt)

                        Dim tdef = SANITATION(R%, 3) - SanitationHHInitCast

                        If tdef > 0 Then
                            Dim tother = SANITATION(R%, 1) + SANITATION(R%, 2)
                            SANITATION(R%, 1) = SANITATION(R%, 1) - tdef * SANITATION(R%, 1) / tother
                            'Sanitation2ndCatInitCast = SanitationBF(R%, 2)
                            SANITATION(R%, 2) = SANITATION(R%, 2) - tdef * SANITATION(R%, 2) / tother
                        End If

                        'mti May Day 2012 - end
                        'End If
                    End If
                    'parameter goal seek - end
                    'mti May 05, 2011 - start

                    'mti 2014/05/28 - start
                    'See if curve has shifted up by extra money in the previous year (for improved cat only)
                    'mti 2021/08/10 start
                    'SanitImpShiftPrevYear = AMAX(0, (SSANITATION(R%, 3) - sPreBudSanitation(R%))) 'percent point
                    Dim SanitImpShiftPrevYear = AMAX(0, (SSANITATION(R%, 3) - sPreBudSanitation(R%, 3))) 'percent point
                    'If R = 95 And IY >= 3 Then Stop
                    'Save pre-finance value:
                    For j = 1 To 3
                        sPreBudSanitation(R%, j) = SANITATION(R%, j)
                    Next j
                    'mti 2021/08/10 end
                    'might be an issue in a multiplier scenario; as SICTBROAD will have multiplier effect; and SBroad(R, 3)  is saved pre-multiplier

                    '      If RegName(R%) = "Saudi Arabia" And IY >= 7 Then Stop

                    'In case curve has shifted up (by extra money in the previous year, demand signal needs to be sent so that it could continue to stay there
                    SANITATION(R%, 3) = Amin(100, SANITATION(R%, 3) + SanitImpShiftPrevYear)

                    Dim Calsanitation1 = 100 - SANITATION(R%, 3)
                    Dim calsanitation2 = SANITATION(R%, 1) + SANITATION(R%, 2)

                    If calsanitation2 > 0 Then
                        SANITATION(R%, 1) = Amin(100, AMAX(0, SANITATION(R%, 1) * Calsanitation1 / calsanitation2))
                        SANITATION(R%, 2) = Amin(100, AMAX(0, SANITATION(R%, 2) * Calsanitation1 / calsanitation2))
                    End If

                    'mti 2014/05/28 - end
                Else 'Sanitation Hold swith is On
                    For j = 1 To 3
                        SANITATION(R%, j) = SSANITATION(R%, j)
                    Next j

                End If
                'jrs 2019/05/03 *** BEGIN ***
                If sanitationexovals(R%, 1) > -100 Then
                    SANITATION(R%, 1) = sanitationexovals(R%, 1)
                End If
                If sanitationexovals(R%, 2) > -100 Then
                    SANITATION(R%, 2) = sanitationexovals(R%, 2)
                End If
                If sanitationexovals(R%, 3) > -100 Then
                    SANITATION(R%, 3) = sanitationexovals(R%, 3)
                End If
                'jrs 2019/05/03 *** END ***
                'jrs 2023/10/30
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("SANITATION", "Improved", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                'jrs 2010/01/04
                If hlwatsansw > 0 Then
                    'mti note: Apr 05, 2012: SE and hard target code not included in this branch

                    'jrs 2009/03/23 Distal driver formulation
                    'mti February 2011 Infarstructure - start
                    'Call AnalFunc("Percent of People with No Access to Sanitation", _
                    'Array(GDPPCP(R%), EDYRSAG25(R%, 3)), calSANITATION)
                    Dim calsanitation As Single = 0
                    Call AnalFunc("Percent of People with No Access to Sanitation",
                New Single() {sGDPPCP(R%), SEDYRSAG25(R%, 3)}, calsanitation,
                New String() {"GDPPCP", "Educ25"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                    'mti February 2011 Infarstructure - end
                    Dim s1 = Math.Exp(calsanitation)

                    'mti February 2011 Infarstructure - start
                    'Call AnalFunc("Percent of People with Access to Improved Sanitation but no HC", _
                    'Array(GDPPCP(R%), EDYRSAG25(R%, 3)), calSANITATION)
                    Call AnalFunc("Percent of People with Access to Improved Sanitation but no HC",
                New Single() {sGDPPCP(R%), SEDYRSAG25(R%, 3)}, calsanitation,
                New String() {"GDPPCP", "Educ25"}, context)  'jrs 2014/10/09 added DepVar Names to check order
                    'mti February 2011 Infarstructure - end
                    Dim s2 = Math.Exp(calsanitation)

                    calsanitation = CSng(s1 / (1 + s1 + s2) * 100)
                    'jrs 2009/04/24 change to Additive Shift Factor
                    '            If SANITATIONShift2(R%, 1) <> 0 Then 'Convergence of a 0 factor creates a transient
                    '                SANITATION2(R, 1) = calSANITATION * ConvergeOverTime(SANITATIONShift2(R%, 1), 1, 100)
                    '            Else
                    '                SANITATION2(R, 1) = calSANITATION * SANITATIONShift2(R%, 1)
                    '            End If

                    'mti July 18, 2011 -start
                    'SANITATION2(R, 1) = calSANITATION + ConvergeOverTime(SANITATIONShift2(R%, 1), 0, 100)
                    If SANITATIONShift2(R%, 1) > 0 Then
                        SANITATION2(R%, 1) = calsanitation + ConvergeOverTime(SANITATIONShift2(R%, 1), 0, watsanconv) '100
                    Else
                        SANITATION2(R%, 1) = calsanitation + SANITATIONShift2(R%, 1) '+ ConvergeOverTime(SANITATIONShift(R%, 1), 0, 1000)
                    End If
                    'mti July 18, 2011 -end


                    SANITATION2(R%, 1) = AMAX(Amin(SANITATION2(R%, 1), 100), 0)

                    'mti April 05, 2012 -start
                    'revert to using model result for category 2
                    'mti July 18, 2011 -start
                    calsanitation = CSng(s2 / (1 + s1 + s2) * 100)
                    'jrs 2009/04/24 change to Additive Shift Factor
                    '            If SANITATIONShift2(R%, 2) <> 0 Then
                    '                SANITATION2(R, 2) = calSANITATION * ConvergeOverTime(SANITATIONShift2(R%, 2), 1, 100)
                    '            Else
                    '                SANITATION2(R, 2) = calSANITATION * SANITATIONShift2(R%, 2)
                    '            End If
                    'SANITATION2(R, 2) = calSANITATION + ConvergeOverTime(SANITATIONShift2(R%, 2), 0, 100)


                    If SANITATIONShift2(R%, 2) > 0 Then
                        'mti April 05, 2012 -change shift convergendce time (75 to 20) to make sure growing improved grows for some time
                        'SANITATION2(R%, 2) = calSANITATION + ConvergeOverTime(SANITATIONShift2(R%, 2), 0, watsanconv) '100
                        SANITATION2(R%, 2) = calsanitation + ConvergeOverTime(SANITATIONShift2(R%, 2), 0, 20) '100
                    Else
                        SANITATION2(R%, 2) = calsanitation + ConvergeOverTime(SANITATIONShift2(R%, 2), 0, 50)  'SANITATIONShift2(R%, 2) '+ ConvergeOverTime(SANITATIONShift(R%, 1), 0, 1000)
                    End If
                    SANITATION2(R%, 2) = AMAX(Amin(SANITATION2(R%, 2), 100), 0)

                    'mti July 18, 2011 - end
                    'mti April 05, 2012 - end

                    calsanitation = CSng(1 / (1 + s1 + s2) * 100)
                    'jrs 2009/04/24 change to Additive Shift Factor
                    '            If SANITATIONShift2(R%, 3) <> 0 Then
                    '                SANITATION2(R, 3) = calSANITATION * ConvergeOverTime(SANITATIONShift2(R%, 3), 1, 100)
                    '            Else
                    '                SANITATION2(R, 3) = calSANITATION * SANITATIONShift2(R%, 3)
                    '            End If

                    'mti July 18, 2011 - start
                    '                SANITATION2(R, 3) = calSANITATION + ConvergeOverTime(SANITATIONShift2(R%, 3), 0, 100)

                    If SANITATIONShift2(R%, 3) < 0 Then
                        SANITATION2(R%, 3) = calsanitation + ConvergeOverTime(SANITATIONShift2(R%, 3), 0, watsanconv) '100
                    Else
                        'very slow or negligible downward convergence
                        SANITATION2(R%, 3) = calsanitation + SANITATIONShift2(R%, 3) 'ConvergeOverTime(SANITATIONShift(R%, 3), 0, 1000)
                    End If
                    'mti July 18, 2011 - end

                    SANITATION2(R%, 3) = AMAX(Amin(SANITATION2(R%, 3), 100), 0)

                    'mti April 05, 2012 - start
                    'comment out cat2 as residual
                    '                'mti July 18, 2011 - start
                    '                'mti: in case no connection plus household connection is greater than 100, normalize them
                    '                SomeofTwo = SANITATION2(R%, 1) + SANITATION2(R%, 3)
                    '                If SomeofTwo > 100 Then
                    '                    SANITATION2(R%, 1) = SANITATION2(R%, 1) * 100 / SomeofTwo
                    '                    SANITATION2(R%, 3) = SANITATION2(R%, 3) * 100 / SomeofTwo
                    '                End If
                    '
                    '                SANITATION2(R%, 2) = 100 - (SANITATION2(R%, 1) + SANITATION2(R%, 3))
                    'mti April 05, 2012 - end

                    'mti April 05, 2012 - start
                    'normalize to 100%
                    calsanitation = SANITATION2(R, 1) + SANITATION2(R, 2) + SANITATION2(R, 3)
                    SANITATION2(R, 1) = SANITATION2(R, 1) / calsanitation * 100
                    SANITATION2(R, 2) = SANITATION2(R, 2) / calsanitation * 100
                    SANITATION2(R, 3) = SANITATION2(R, 3) / calsanitation * 100
                    'mti July 18, 2011 - end
                    'mti April 05, 2012 - end
                    'jrs 2019/11/19
                    'For j = 1 To 3
                    '        ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                    '        .VarName = "SANITATION",
                    '        .Year = CShort(IY% + FirstYear - 1),
                    '        .Region = CShort(R%),
                    '        .Dim2 = CShort(j),
                    '        .Dim3 = CShort(j),
                    '        .DistalFormVal = CSng(SANITATION(R%, j))
                    '    })
                    '    Next j
                    '    ifsPopWorkDB.Flush()
                End If 'if WatSan switch is On, hlwatsansw > 0
                'mti February 2011 Infarstructure - start
                '----------sanitation connection cost---------
                '            SanitConadded = AMAX(0, (SANITATION(R%, 3) * POP(R%) / HHSIZE(R%) - SSANITATION(R%, 3) * CDbl(sPOPTOTAL(R%)) / sHHSIZEWS(R%)) * 10000)
                CalcInfraUnitCost(R%, 10, 5, sanitationcostupper, sanitationcostlower, sGDPPCP(R%), SANITATIONCOST(R%))

                'mti April 2011 -start
                SANITATIONCOST(R%) = SANITATIONCOST(R%) * sanitationcostm(R%)
                'mti April 2011 -end

                '            'mti January 2011 - start
                '            INFRAINVESTNEW(R%, SanitationHH) = SanitConadded * SANITATIONCOST(R%) / 1000000 'billion dollars
                '            INFRAINVESTMAINT(R%, SanitationHH) = SSANITATION(R%, 3) * CDbl(sPOPTOTAL(R%)) * 10000 * DeppcntWS(SanitationHH) * SANITATIONCOST(R%) / 1000000 / sHHSIZEWS(R%)  'billion dollars
                '    If IY = 50 Then Stop

                'If IY > 50 And RegName(R%) = "Cuba" Then Stop
                'If IY > 50 And RegName(R%) = "Greece" Then Stop
                'If RegName(R%) = "Tanzania" Then Stop

                Dim SanitDemThousands = SANITATION(R%, 3) * CDbl(POP(R%) * 10 / HHSIZE(R%)) 'Demanded Connections (stock) in Thousands This Year
                Dim sSanitThousands = SSANITATION(R%, 3) * CDbl(sPOPTotal(R%)) * 10 / sHHSIZEWS(R%) 'Connections (Stock) in Thousands Last Year
                CalcInfraInvest(CDbl(SanitDemThousands), CDbl(sSanitThousands),
                            DeppcntWS(SanitationHH), SANITATIONCOST(R%), INFRAINVESTNEW(R%, SanitationHH), INFRAINVESTMAINT(R%, SanitationHH))



                '----------sanitation Other Improved cost---------
                '            SanitConadded = AMAX(0, (SANITATION(R%, 3) * POP(R%) / HHSIZE(R%) - SSANITATION(R%, 3) * CDbl(sPOPTOTAL(R%)) / sHHSIZEWS(R%)) * 10000)
                CalcInfraUnitCost(R%, 10, 5, sanitationimpcostupper, sanitationimpcostlower, sGDPPCP(R%), SANITATIONIMPCOST(R%))

                SANITATIONIMPCOST(R%) = SANITATIONIMPCOST(R%) * sanitationimpcostm(R%)

                Dim SanitImpDemThousands = SANITATION(R%, 2) * CDbl(POP(R%) * 10) ' / HHSIZE(R%)) 'Demanded Connections (stock) in Thousands This Year
                Dim sSanitImpThousands = SSANITATION(R%, 2) * CDbl(sPOPTotal(R%)) * 10 '/ sHHSIZEWS(R%) 'Connections (Stock) in Thousands Last Year

                CalcInfraInvest(CDbl(SanitImpDemThousands), CDbl(sSanitImpThousands),
                            DeppcntWS(SanitationImproved), SANITATIONIMPCOST(R%), INFRAINVESTNEW(R%, SanitationImproved), INFRAINVESTMAINT(R%, SanitationImproved))

                '----- Spending for base forecast road target---------

                INFRAINVESTNEWBF(R%, SanitationHH) = INFRAINVESTNEW(R%, SanitationHH)
                INFRAINVESTMAINTBF(R%, SanitationHH) = INFRAINVESTMAINT(R%, SanitationHH)

                INFRAINVESTNEWBF(R%, SanitationImproved) = INFRAINVESTNEW(R%, SanitationImproved)
                INFRAINVESTMAINTBF(R%, SanitationImproved) = INFRAINVESTMAINT(R%, SanitationImproved)
                '       If RegName(R%) = "Tanzania" Then Stop
                If sanitationtrgtyr(R%) > 0 Or sanitnoconsetar(R%) > 0 Then 'there is an upward RAI target
                    'Base Forecast Cost - Required for target case
                    SanitDemThousands = SANITATIONBF(R%, 3) * CDbl(POP(R%) * 10 / HHSIZE(R%)) 'Demanded Connections (stock) in Thousands This Year

                    'mti June 14, 2012 - start
                    'sSanitThousands = sSanitationBF(R%, 3) * CDbl(sPOPTotal(R%)) * 10 / sHHSIZEWS(R%) 'Connections (Stock) in Thousands Last Year
                    sSanitThousands = SSANITATION(R%, 3) * CDbl(sPOPTotal(R%)) * 10 / sHHSIZEWS(R%) 'Connections (Stock) in Thousands Last Year
                    'mti June 14, 2012 - end

                    CalcInfraInvest(CDbl(SanitDemThousands), CDbl(sSanitThousands),
                                DeppcntWS(SanitationHH), SANITATIONCOST(R%), INFRAINVESTNEWBF(R%, SanitationHH), INFRAINVESTMAINTBF(R%, SanitationHH))

                    SanitImpDemThousands = SANITATIONBF(R%, 2) * CDbl(POP(R%) * 10) ' / HHSIZE(R%)) 'Demanded Connections (stock) in Thousands This Year

                    'mti June 14, 2012 - start
                    'sSanitImpThousands = sSanitationBF(R%, 2) * CDbl(sPOPTotal(R%)) * 10 '/ sHHSIZEWS(R%) 'Connections (Stock) in Thousands Last Year
                    sSanitImpThousands = SSANITATION(R%, 2) * CDbl(sPOPTotal(R%)) * 10 '/ sHHSIZEWS(R%) 'Connections (Stock) in Thousands Last Year
                    'mti June 14, 2012 - end

                    CalcInfraInvest(CDbl(SanitImpDemThousands), CDbl(sSanitImpThousands),
                                DeppcntWS(SanitationImproved), SANITATIONIMPCOST(R%), INFRAINVESTNEWBF(R%, SanitationImproved), INFRAINVESTMAINTBF(R%, SanitationImproved))

                End If
                'mti January 2011 - end
                'mti February 2011 Infarstructure - end


                'JRS 2009/02/17
                '            'percentage of people with access to safewater
                '            If frm_MainMenu.IFsGUIctrl1.cHistoricBase = True Then
                '                Call XYTABL("GDP/Capita (PPP) Versus People with Access to Safe Water (1970)", _
                '                    GDPPCP(R%), calWATSAFE)
                '
                '            Else
                '                Call XYTABL("GDP/Capita (PPP) Versus People with Access to Safe Water (2000)", _
                '                    GDPPCP(R%), calWATSAFE)
                '            End If
                '            calWATSAFE = Amin(100, calWATSAFE)
                '            'mti sanitation June 2006 -end add watsafe historic, impose limit
                '
                '            WATSAFE(R%) = AMAX(1, Amin(100, calWATSAFE + WATSAFEShift(R%)))

                'If RegName(R%) = "India" Then Stop
                'jrs 2010/01/07
                If watsafhldsw = 0 Then
                    'jrs 2009/03/05 Full Model Regression
                    'Call AnalFunc("Percent of People with No Access to Clean Water", _
                    'Array(GDPPCP(R%), EDYRSAG25(R%, 3)), calWATSAFE)
                    'mti February 2011 Infarstructure - start
                    'Call AnalFunc("Percent of People with No Access to Clean Water Full Model", _
                    'Array(RuralPop, HealthExp, GDPPCP(R%), EDYRSAG25(R%, 3), Poverty), calWATSAFE)
                    Dim calWATSAFE As Single = 0
                    Call AnalFunc("Percent of People with No Access to Clean Water Full Model",
                New Single() {RuralPop, HealthExp, sGDPPCP(R%), SEDYRSAG25(R%, 3), Poverty}, calWATSAFE,
                New String() {"PopRural%", "GovtHl%GDP", "GDPPCP", "Educ25", "INCOMELT1"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                    'mti February 2011 Infarstructure - end
                    Dim s1 = Math.Exp(calWATSAFE)

                    'jrs 2009/03/05 Full Model Regression
                    'Call AnalFunc("Percent of People with Improved Access to Clean Water but No HC", _
                    'Array(GDPPCP(R%), EDYRSAG25(R%, 3)), calWATSAFE)
                    'mti February 2011 Infarstructure - start
                    'Call AnalFunc("Percent of People with Improved Access to Clean Water but No HC Full Model", _
                    'Array(RuralPop, HealthExp, GDPPCP(R%), EDYRSAG25(R%, 3), Poverty), calWATSAFE)
                    '         If RegName(R%) = "India" Then Stop
                    '         If RegName(R%) = "Bhutan" Then Stop

                    Call AnalFunc("Percent of People with Improved Access to Clean Water but No HC Full Model",
                New Single() {RuralPop, HealthExp, sGDPPCP(R%), SEDYRSAG25(R%, 3), Poverty}, calWATSAFE,
                New String() {"PopRural%", "GovtHl%GDP", "GDPPCP", "Educ25", "INCOMELT1"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                    'mti February 2011 Infarstructure - end
                    Dim s2 = Math.Exp(calWATSAFE)


                    calWATSAFE = CSng(s1 / (1 + s1 + s2) * 100)
                    'jrs 2009/04/24 change to Additive Shift Factor
                    '            If WATSAFEshift(R%, 1) <> 0 Then
                    '                WATSAFE(R%, 1) = calWATSAFE * ConvergeOverTime(WATSAFEshift(R%, 1), 1, 100)
                    '            Else
                    '                WATSAFE(R%, 1) = calWATSAFE * WATSAFEshift(R%, 1)
                    '            End If
                    'mti July 18, 2011 -start
                    'WATSAFE(R%, 1) = calWATSAFE + ConvergeOverTime(WATSAFEShift(R%, 1), 0, 100)
                    Dim WatSafeNoAdd As Single = 0
                    'mti 2021/08/10 start
                    '                If WATSAFEShift(R%, 1) > 0 Then
                    '                    WatSafeNoAdd = ConvergeOverTime(WATSAFEShift(R%, 1), 0, watsanconv)  '100
                    '                Else
                    '                    WatSafeNoAdd = WATSAFEShift(R%, 1)  '+ ConvergeOverTime(WATSAFEShift(R%, 1), 0, 1000)
                    '                End If
                    WatSafeNoAdd = ConvergeOverTime(WATSAFEShift(R%, 1), 0, watsanconv)  '100
                    'mti 2021/08/10 End
                    'mti July 18, 2011 -end
                    'mti October 20, 2011 -start
                    'BBH/JRS SE approach
                    'dkb 2013/09/16 added dimension 1 to watsafenoconsetar and watsefenoconseyrtar
                    Dim WatSafeNoSE As Single = 0
                    If watsafenoconsetar(R%) <> 0 Then
                        'mti April 05, 2012 - start
                        'WatSafeNoSE = 8.2
                        WatSafeNoSE = 6
                        'mti April 05, 2012 - end
                        EffectSE = StdErrorAdditiveShift(CSng(WatSafeNoSE), CSng(calWATSAFE), CSng(WatSafeNoAdd), CSng(WATSAFEShift(R%, 1)),
                    CSng(watsafenoconsetar(R%)), CInt(FirstYear + watsafenoconseyrtar(R%)), FirstYear + IY - 1, FirstYear)
                        'SE, Prediction, DataAddShift, IDataAddShift, TargetSEs, TargetYr, CurrentYr
                    Else
                        EffectSE = 0
                    End If

                    'mti note: JRS original model did not have upper and lower bounds here; Check if that makes a difference
                    'If IY = 6 And GRPS(1, R) = "Iceland" Then Stop
                    WATSAFE(R%, 1) = calWATSAFE + WatSafeNoAdd + EffectSE
                    'mti October 20, 2011 - end


                    'jrs 2009/07/07
                    '                WATSAFE(R%, 1) = WATSAFE(R%, 1) * watsafem(R%, 1)

                    WATSAFE(R%, 1) = AMAX(Amin(WATSAFE(R%, 1), 100), 0)

                    sWatSafeBF(R%, 1) = WatSafeBF(R%, 1)
                    WatSafeBF(R%, 1) = AMAX(Amin(calWATSAFE + WatSafeNoAdd, 100), 0)

                    'mti April 05, 2012 - start
                    'revert cat2 from residual to cat2 from model
                    calWATSAFE = CSng(s2 / (1 + s1 + s2) * 100)

                    If WATSAFEShift(R%, 2) < 0 Then
                        'mti April 05, 2012 -change shift convergendce time (75 to 20) to make sure growing improved grows for some time
                        'WATSAFE(R%, 2) = calWATSAFE + ConvergeOverTime(WATSAFEShift(R%, 2), 0, watsanconv) '100
                        'mti May 04, 2012 - 20 to 30
                        WATSAFE(R%, 2) = calWATSAFE + ConvergeOverTime(WATSAFEShift(R%, 2), 0, 30)   ' 20 '100
                    Else
                        'needs down convergence
                        'WATSAFE(R%, 2) = calWATSAFE + ConvergeOverTime(WATSAFEShift(R%, 2), 0, 50) ' WATSAFEShift(R%, 2) 'ConvergeOverTime(WATSAFEShift(R%, 3), 0, 1000)
                        'mti May 04, 2012 - 50 to watsanconv
                        WATSAFE(R%, 2) = calWATSAFE + ConvergeOverTime(WATSAFEShift(R%, 2), 0, watsanconv) '50 ' WATSAFEShift(R%, 2) 'ConvergeOverTime(WATSAFEShift(R%, 3), 0, 1000)
                    End If

                    'WATSAFE(R%, 2) = calWATSAFE + ConvergeOverTime(WATSAFEShift(R%, 2), 0, 100)
                    'jrs 2009 / 7 / 7
                    '                WATSAFE(R%, 2) = WATSAFE(R%, 2) * watsafem(R%, 2)

                    WATSAFE(R%, 2) = AMAX(Amin(WATSAFE(R%, 2), 100), 0)
                    'mti April 05, 2012 - end

                    sWatSafeBF(R%, 2) = WatSafeBF(R%, 2)
                    WatSafeBF(R%, 2) = WATSAFE(R%, 2)


                    'If RegName(R%) = "India" Or RegName(R%) = "Bhutan" Or RegName(R%) = "Bangladesh" Then Stop

                    calWATSAFE = CSng(1 / (1 + s1 + s2) * 100)
                    'jrs 2009/04/24 change to Additive Shift Factor
                    '            If WATSAFEshift(R%, 3) <> 0 Then
                    '                WATSAFE(R%, 3) = calWATSAFE * ConvergeOverTime(WATSAFEshift(R%, 3), 1, 100)
                    '            Else
                    '                WATSAFE(R%, 3) = calWATSAFE * WATSAFEshift(R%, 3)
                    '            End If

                    'mti July 18, 2011 -start
                    'mti 2021/08/10 start
                    '                If WATSAFEShift(R%, 3) < 0 Then
                    '                    WATSAFE(R%, 3) = calWATSAFE + ConvergeOverTime(WATSAFEShift(R%, 3), 0, watsanconv) '100
                    '                Else
                    '                    'very slow or no down convergence
                    '                    WATSAFE(R%, 3) = calWATSAFE + WATSAFEShift(R%, 3) 'ConvergeOverTime(WATSAFEShift(R%, 3), 0, 1000)
                    '                End If
                    WATSAFE(R%, 3) = calWATSAFE + ConvergeOverTime(WATSAFEShift(R%, 3), 0, watsanconv) '100
                    'mti 2021/08/10 End
                    'mti July 18, 2011 - end

                    'jrs 2009/07/07
                    '                WATSAFE(R%, 3) = WATSAFE(R%, 3) * watsafem(R%, 3)



                    WATSAFE(R%, 3) = AMAX(Amin(WATSAFE(R%, 3), 100), 0)

                    '     If RegName(R%) = "Afghanistan" Then Stop
                    'mti May 04, 2012 -start
                    'simple extrapolative approach

                    'ttt vb6: If Not IsNull(WatSafeGR(R%, 2)) And sWATSAFEPreMult(R%, 2) <> 0 Then etc
                    If sWATSAFEPreMult(R%, 2) <> 0 Then

                        'mti 2016/04/05 - start commented code addition
                        'Saveval = WATSAFE(R%, 2)
                        If WatSafeGR(R%, 2) > -9999 Then
                            WatSafeGR(R%, 2) = CSng(AMAX(-0.1, Amin(0.1, 0.15 * (WATSAFE(R%, 2) / sWATSAFEPreMult(R%, 2) - 1) + 0.85 * WatSafeGR(R%, 2))))
                        Else
                            WatSafeGR(R%, 2) = CSng(AMAX(-0.1, Amin(0.1, 0.15 * (WATSAFE(R%, 2) / sWATSAFEPreMult(R%, 2) - 1))))
                        End If
                        WATSAFE(R%, 2) = Amin(100, AMAX(1, sWATSAFEPreMult(R%, 2) * (1 + WatSafeGR(R%, 2))))
                        sWATSAFEPreMult(R%, 2) = WATSAFE(R%, 2)

                    End If
                    'ttt vb6: If Not IsNull(WatSafeGR(R%, 3)) And sWATSAFEPreMult(R%, 3) <> 0 Then
                    If sWATSAFEPreMult(R%, 3) <> 0 Then

                        'mti 2016/04/05 - start commented code addition
                        'Saveval = WATSAFE(R%, 3)
                        'mti 2016/04/05 - end

                        If WatSafeGR(R%, 3) > -9999 Then
                            WatSafeGR(R%, 3) = CSng(AMAX(-0.1, Amin(0.1, 0.2 * (WATSAFE(R%, 3) / sWATSAFEPreMult(R%, 3) - 1) + 0.8 * WatSafeGR(R%, 3))))
                        Else
                            WatSafeGR(R%, 3) = CSng(AMAX(-0.1, Amin(0.1, 0.2 * (WATSAFE(R%, 3) / sWATSAFEPreMult(R%, 3) - 1))))
                        End If
                        WATSAFE(R%, 3) = Amin(100, AMAX(1, sWATSAFEPreMult(R%, 3) * (1 + WatSafeGR(R%, 3))))

                        'mti 2016/04/05 - start commented code addition
                        'WATSAFE(R%, 3) = AMAX(Saveval, WATSAFE(R%, 3))
                        'mti 2016/04/05 - end

                        sWATSAFEPreMult(R%, 3) = WATSAFE(R%, 3)
                    End If
                    'mti May 04, 2012 - end
                    sWatSafeBF(R%, 3) = WatSafeBF(R%, 3)
                    WatSafeBF(R%, 3) = WATSAFE(R%, 3)


                    '----------
                    'mti April 05, 2012 - start
                    '                'revert cat2 from residual to cat2 from model
                    '                'mti July 18, 2011 - start
                    '                'mti: in case no connection plus household connection is greater than 100, normalize them
                    '                SomeofTwo = WATSAFE(R%, 1) + WATSAFE(R%, 3)
                    '                If SomeofTwo > 100 Then
                    '                    WATSAFE(R%, 1) = WATSAFE(R%, 1) * 100 / SomeofTwo
                    '                    WATSAFE(R%, 3) = WATSAFE(R%, 3) * 100 / SomeofTwo
                    '                End If
                    '
                    '
                    '                WATSAFE(R%, 2) = 100 - (WATSAFE(R%, 1) + WATSAFE(R%, 3))
                    '                'mti July 18, 2011 - end
                    'mti April 05, 2012 - end

                    'mti July 18, 2011 - start
                    'now do all multipliers
                    WATSAFE(R%, 1) = WATSAFE(R%, 1) * watsafem(R%, 1)
                    WATSAFE(R%, 2) = WATSAFE(R%, 2) * watsafem(R%, 2)
                    WATSAFE(R%, 3) = WATSAFE(R%, 3) * watsafem(R%, 3)


                    'mti: only a post-multiplier normalize needed now
                    'mti April 05, 2012 - start
                    'If watsafem(R%, 1) <> 1 Or watsafem(R%, 2) <> 1 Or watsafem(R%, 3) <> 1 Then
                    'mti April 05, 2012 - end

                    'normalize to 100%
                    calWATSAFE = WATSAFE(R%, 1) + WATSAFE(R%, 2) + WATSAFE(R%, 3)

                    'jrs 2009/11/16
                    'if multipliers are driving 2 variables to 0, and the other one is already 0
                    If calWATSAFE = 0 Then
                        Select Case True
                            Case watsafem(R%, 1) > 0
                                WATSAFE(R%, 1) = 1
                                calWATSAFE = 1
                            Case watsafem(R%, 2) > 0
                                WATSAFE(R%, 2) = 1
                                calWATSAFE = 1
                            Case watsafem(R%, 3) > 0
                                WATSAFE(R%, 3) = 1
                                calWATSAFE = 1
                        End Select
                        If calWATSAFE = 0 Then
                            Debug.WriteLine("At least one of the safe water multipliers has to be non-zero for " & GRPS(1, R) & "-Water and Sanitation")
                            Exit Sub
                        End If
                    End If
                    'mti June 04, 2012 -start
                    'already established access rates cannot go down unless there is a hard multiplier watsafem(R%, 3)
                    'hh conn
                    'If (IY = 5 Or IY = 11) And GRPS(1, R) = "Zambia" Then Stop
                    If watsafem(R%, 3) >= 1 Then
                        'jrs 2020/06/ 08 WATSAFE(R%, 3) = CSng(Math.Round(AMAX(SWATSAFE(R%, 3), WATSAFE(R%, 3)), 4))
                        'mti 2021/08/10 start
                        'WATSAFE(R%, 3) = AMAX(SWATSAFE(R%, 3), WATSAFE(R%, 3))
                        WATSAFE(R%, 3) = AMAX(sPreBudWATSAFE(R%, 3), WATSAFE(R%, 3))
                        'mti 2021/08/10 end
                        calWATSAFE = WATSAFE(R%, 1) + WATSAFE(R%, 2) + WATSAFE(R%, 3)
                    End If

                    'none
                    'If (IY = 4 Or IY = 10) And GRPS(1, R) = "Zambia" Then Stop
                    If watsafem(R%, 1) <= 1 Then
                        'mti 2021/08/10 start
                        'WATSAFE(R%, 1) = Amin(SWATSAFE(R%, 1), WATSAFE(R%, 1))
                        WATSAFE(R%, 1) = Amin(sPreBudWATSAFE(R%, 1), WATSAFE(R%, 1))
                        'mti 2021/08/10 end
                        calWATSAFE = WATSAFE(R%, 1) + WATSAFE(R%, 2) + WATSAFE(R%, 3)
                    End If
                    'mti June 04, 2012 - end
                    'mti 2016/04/05 - start commented code addition
                    '                    If calWATSAFE < 110 And calWATSAFE > 100 Then
                    '                        'keep new connections (improved and piped) as is and cut from no connection
                    '                        WATSAFE(R%, 1) = 100 - WATSAFE(R%, 2) - WATSAFE(R%, 3)
                    '
                    '                    Else
                    'If IY = 7 And GRPS(1, R) = "Zambia" Then Stop
                    'mti 2016/04/05 - end commented code addition
                    'jrs 2020/06/08 WATSAFE(R%, 1) = CSng(Math.Round(WATSAFE(R%, 1) / calWATSAFE * 100, 4)) 
                    WATSAFE(R%, 1) = WATSAFE(R%, 1) / calWATSAFE * 100
                    'jrs 2020/06/08 WATSAFE(R%, 2) = CSng(Math.Round(WATSAFE(R%, 2) / calWATSAFE * 100, 4)) 
                    WATSAFE(R%, 2) = WATSAFE(R%, 2) / calWATSAFE * 100
                    'jrs 20200/06/08 WATSAFE(R%, 3) = CSng(Math.Round(WATSAFE(R%, 3) / calWATSAFE * 100, 4)) 
                    WATSAFE(R%, 3) = WATSAFE(R%, 3) / calWATSAFE * 100

                    'mti 2016/04/05 - start commented code addition
                    'End If

                    'mti 2016/04/05 end


                    'mti April 05, 2012 - start
                    'End If
                    'mti April 05, 2012 - end
                    'mti July 18, 2011 - end


                    'normalize to 100% - for base forecast values: required in target scenarios
                    calWATSAFE = WatSafeBF(R%, 1) + WatSafeBF(R%, 2) + WatSafeBF(R%, 3)

                    'jrs 2009/11/16
                    'if multipliers are driving 2 variables to 0, and the other one is already 0
                    If calWATSAFE = 0 Then
                        Select Case True
                            Case watsafem(R%, 1) > 0
                                WatSafeBF(R%, 1) = 1
                                calWATSAFE = 1
                            Case watsafem(R%, 2) > 0
                                WatSafeBF(R%, 2) = 1
                                calWATSAFE = 1
                            Case watsafem(R%, 3) > 0
                                WatSafeBF(R%, 3) = 1
                                calWATSAFE = 1
                        End Select
                        If calWATSAFE = 0 Then
                            Debug.WriteLine("At least one of the safe water multipliers has to be non-zero for " & GRPS(1, R) & "-Water and Sanitation")
                            Exit Sub
                        End If
                    End If

                    WatSafeBF(R%, 1) = WatSafeBF(R%, 1) / calWATSAFE * 100
                    WatSafeBF(R%, 2) = WatSafeBF(R%, 2) / calWATSAFE * 100
                    WatSafeBF(R%, 3) = WatSafeBF(R%, 3) / calWATSAFE * 100
                    'mti May 05, 2011 - start
                    'parameter goal seek - start
                    If watsafetrgtyr(R%) > 0 Then
                        'If sINFRAELECACC(R%, Urban) < 100 Then
                        'mti June 08, 2011 - start
                        '                        RatChange = Amin(1, 0.05 / AMAX(0.001, SWATSAFE(R%, 1)))
                        '
                        '
                        '                        'ConvTime = AMAX(3, watsafetrgtyr(R%)- 5 - IY) ' 30  'years
                        '                        ConvTime = AMAX(1, watsafetrgtyr(R%)- IY)  ' 30  'years
                        '                        'mti June 08, 2011 - end
                        '
                        '                        cGrowthRateTarget = RatChange ^ (1 / ConvTime) - 1
                        '                        watsafeNoTrgt = SWATSAFE(R%, 1) * (1 + cGrowthRateTarget)
                        '         If RegName(R%) = "Chad" And IY >= 2 Then Stop


                        'mti October 20, 2011 - start
                        '                        DistTrgt = AMAX(0, SWATSAFE(R%, 1) - 0)
                        '                        TimeLeft = AMAX(1, watsafetrgtyr(R%) + 2 - IY)

                        'WATSAFENoTrgt = AMAX(0, SWATSAFE(R%, 1) - DistTrgt / TimeLeft)
                        'mti May Day 2012 - start
                        'WATSAFENoTrgt = iWatSafeNone(R%) - Amin(watsafetrgtyr(R%), IY) * iWatSafeNone(R%) / watsafetrgtyr(R%)

                        'mti July 25, 2012 - start
                        'WATSAFENoTrgt = iWatSafeNone(R%) + watsafetrgtval(R%) - Amin(watsafetrgtyr(R%), IY) * iWatSafeNone(R%) / watsafetrgtyr(R%)
                        Dim WATSAFENoTrgt = iWatSafeNone(R%) + watsafetrgtval(R%) - Amin(watsafetrgtyr(R%), IY - 1) * iWatSafeNone(R%) / watsafetrgtyr(R%)
                        'mti July 25, 2012 - end

                        'mti May Day 2012 - end
                        'mti October 20, 2011 - end
                        'mti June 08, 2011 -end


                        Dim watsafeNoInitCast = WATSAFE(R%, 1)

                        'hard target cannot be worse than base case
                        WATSAFE(R%, 1) = Amin(watsafeNoInitCast, WATSAFENoTrgt)

                        'mti October 20, 2011 - start
                        Dim tdef = watsafeNoInitCast - WATSAFE(R%, 1)
                        If tdef > 0 Then
                            Dim tother = WATSAFE(R%, 2) + WATSAFE(R%, 3)
                            WATSAFE(R%, 2) = WATSAFE(R%, 2) + tdef * WATSAFE(R%, 2) / tother
                            WATSAFE(R%, 3) = WATSAFE(R%, 3) + tdef * WATSAFE(R%, 3) / tother
                        End If

                        'this will increase improved (not much of hh con) for countries historically high in improved - like Cameroon
                        '                        tdef = SWATSAFE(R%, 1) - WATSAFE(R%, 1)
                        '                        If tdef > 0 Then
                        '                            tother = SWATSAFE(R%, 2) + SWATSAFE(R%, 3)
                        '                            WATSAFE(R%, 2) = SWATSAFE(R%, 2) + tdef * SWATSAFE(R%, 2) / tother
                        '                            WATSAFE(R%, 3) = SWATSAFE(R%, 3) + tdef * SWATSAFE(R%, 3) / tother
                        '                        End If

                        'mti October 20, 2011 - end

                        'End If
                    End If
                    'parameter goal seek - end
                    'mti May 05, 2011 - start

                    'mti 2014/05/28 - start
                    'See if curve has shifted up by extra money in the previous year (for improved cat only)
                    'mti 2021/08/10 start
                    'WatSafeImpShiftPrevYear = AMAX(0, (SWATSAFE(R%, 3) - sPreBudWATSAFE(R%))) 'percent point
                    Dim WatSafeImpShiftPrevYear = AMAX(0, (SWATSAFE(R%, 3) - sPreBudWATSAFE(R%, 3))) 'percent point
                    'Save pre-finance value:
                    'sPreBudWATSAFE(R%) = WATSAFE(R%, 3)
                    For j = 1 To 3
                        sPreBudWATSAFE(R%, j) = WATSAFE(R%, j)
                    Next
                    'mti 2021/08/10 end
                    'might be an issue in a multiplier scenario; as SICTBROAD will have multiplier effect; and SBroad(R, 3)  is saved pre-multiplier

                    '      If RegName(R%) = "Saudi Arabia" And IY >= 7 Then Stop                    
                    'In case curve has shifted up (by extra money in the previous year, demand signal needs to be sent so that it could continue to stay there
                    'If IY = 7 And GRPS(1, R) = "Zambia" Then Stop
                    'jrs 2020/06/08 WATSAFE(R%, 3) = CSng(Math.Round(Amin(100, WATSAFE(R%, 3) + WatSafeImpShiftPrevYear), 4)) 
                    WATSAFE(R%, 3) = Amin(100, WATSAFE(R%, 3) + WatSafeImpShiftPrevYear)


                    Dim calWATSAFE1 = 100 - WATSAFE(R%, 3)
                    Dim calWATSAFE2 = WATSAFE(R%, 1) + WATSAFE(R%, 2)

                    If calWATSAFE2 > 0 Then
                        'jrs 2020/06/08 WATSAFE(R%, 1) = CSng(Math.Round(Amin(100, AMAX(0, WATSAFE(R%, 1) * calWATSAFE1 / calWATSAFE2)), 4)) 
                        WATSAFE(R%, 1) = Amin(100, AMAX(0, WATSAFE(R%, 1) * calWATSAFE1 / calWATSAFE2))
                        'jrs 2020/06/08 WATSAFE(R%, 2) = CSng(Math.Round(Amin(100, AMAX(0, WATSAFE(R%, 2) * calWATSAFE1 / calWATSAFE2)), 4)) 
                        WATSAFE(R%, 2) = Amin(100, AMAX(0, WATSAFE(R%, 2) * calWATSAFE1 / calWATSAFE2))
                    End If

                    'mti 2016/04/05 - start commented code addition
                    'WATSAFE(R%, 1) = 100 - WATSAFE(R%, 2) - WATSAFE(R%, 3)
                    'mti 2016/04/05 end

                    'mti 2014/05/28 - end

                Else 'Safe Water Hold Switch is On
                    For j = 1 To 3
                        WATSAFE(R%, j) = SWATSAFE(R%, j)
                    Next j
                End If
                'jrs 2019/05/03 *** BEGIN ***
                If watsafeexovals(R%, 1) > -100 Then
                    WATSAFE(R%, 1) = watsafeexovals(R%, 1)
                End If
                If watsafeexovals(R%, 2) > -100 Then
                    WATSAFE(R%, 2) = watsafeexovals(R%, 2)
                End If
                If watsafeexovals(R%, 3) > -100 Then
                    WATSAFE(R%, 3) = watsafeexovals(R%, 3)
                End If
                'jrs 2019/05/03 *** END ***
                'jrs 2023/10/30
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("WATSAFE", "Piped", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                '      If RegName(R%) = "India" Then Stop
                'jrs 2010/01/04
                If hlwatsansw > 0 Then
                    'mti Comment: April 2012: no SE and hard target in this branch
                    'jrs 2009/03/23 Distal Driver formulation
                    'mti February 2011 Infarstructure - start
                    'Call AnalFunc("Percent of People with No Access to Clean Water", _
                    'Array(GDPPCP(R%), EDYRSAG25(R%, 3)), calWATSAFE)
                    Dim calWATSAFE As Single = 0
                    Call AnalFunc("Percent of People with No Access to Clean Water",
                New Single() {sGDPPCP(R%), SEDYRSAG25(R%, 3)}, calWATSAFE,
                New String() {"GDPPCP", "Educ25"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                    Dim s1 = Math.Exp(calWATSAFE)


                    'Call AnalFunc("Percent of People with Improved Access to Clean Water but No HC", _
                    'Array(GDPPCP(R%), EDYRSAG25(R%, 3)), calWATSAFE)

                    Call AnalFunc("Percent of People with Improved Access to Clean Water but No HC",
                New Single() {sGDPPCP(R%), SEDYRSAG25(R%, 3)}, calWATSAFE,
                New String() {"GDPPCP", "Educ25"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                    'mti February 2011 Infarstructure - end
                    Dim s2 = Math.Exp(calWATSAFE)

                    calWATSAFE = CSng(s1 / (1 + s1 + s2) * 100)

                    'jrs 2009/04/24 change to Additive Shift Factor
                    '            If WATSAFEshift2(R%, 1) <> 0 Then
                    '                WATSAFE2(R, 1) = calWATSAFE * ConvergeOverTime(WATSAFEshift2(R%, 1), 1, 100)
                    '            Else
                    '                WATSAFE2(R, 1) = calWATSAFE * WATSAFEshift2(R%, 1)
                    '            End If

                    'mti July 18, 2011 -start
                    'WATSAFE2(R, 1) = calWATSAFE + ConvergeOverTime(WATSAFEshift2(R%, 1), 0, 100)

                    If WATSAFEShift2(R%, 1) > 0 Then
                        WATSAFE2(R%, 1) = calWATSAFE + ConvergeOverTime(WATSAFEShift2(R%, 1), 0, watsanconv) '100
                    Else
                        WATSAFE2(R%, 1) = calWATSAFE + WATSAFEShift2(R%, 1) '+ ConvergeOverTime(WATSAFEShift(R%, 1), 0, 1000)
                    End If
                    'mti July 18, 2011 -end


                    WATSAFE2(R%, 1) = AMAX(Amin(WATSAFE2(R%, 1), 100), 0)

                    'mti April 05, 2012 - start
                    'revert cat2 from residual to from model
                    'mti July 18, 2011 -start
                    calWATSAFE = CSng(s2 / (1 + s1 + s2) * 100)
                    'jrs 2009/04/24 change to Additive Shift Factor
                    '            If WATSAFEshift2(R%, 2) <> 0 Then
                    '                WATSAFE2(R, 2) = calWATSAFE * ConvergeOverTime(WATSAFEshift2(R%, 2), 1, 100)
                    '            Else
                    '                WATSAFE2(R, 2) = calWATSAFE * WATSAFEshift2(R%, 2)
                    '            End If
                    'WATSAFE2(R, 2) = calWATSAFE + ConvergeOverTime(WATSAFEShift2(R%, 2), 0, 100)
                    If WATSAFEShift2(R%, 2) > 0 Then
                        'mti April 05, 2012 -change shift convergendce time (75 to 20) to make sure growing improved grows for some time
                        'WATSAFE2(R%, 2) = calWATSAFE + ConvergeOverTime(WATSAFEShift2(R%, 2), 0, watsanconv) '100
                        WATSAFE2(R%, 2) = calWATSAFE + ConvergeOverTime(WATSAFEShift2(R%, 2), 0, 20) '100
                    Else
                        WATSAFE2(R%, 2) = calWATSAFE + ConvergeOverTime(WATSAFEShift2(R%, 2), 0, 50) '+ WATSAFEShift2(R%, 2) '+ ConvergeOverTime(WATSAFEShift(R%, 1), 0, 1000)
                    End If
                    WATSAFE2(R%, 2) = AMAX(Amin(WATSAFE2(R%, 2), 100), 0)
                    'mti July 18, 2011 -end
                    'mti July 18, 2011 -start

                    calWATSAFE = CSng(1 / (1 + s1 + s2) * 100)
                    'jrs 2009/04/24 change to Additive Shift Factor
                    '            If WATSAFEshift2(R%, 3) <> 0 Then
                    '                WATSAFE2(R, 3) = calWATSAFE * ConvergeOverTime(WATSAFEshift2(R%, 3), 1, 100)
                    '            Else
                    '                WATSAFE2(R, 3) = calWATSAFE * WATSAFEshift2(R%, 3)
                    '            End If

                    'mti July 18, 2011 -start
                    'WATSAFE2(R, 3) = calWATSAFE + ConvergeOverTime(WATSAFEShift2(R%, 3), 0, 100)

                    If WATSAFEShift2(R%, 3) < 0 Then
                        WATSAFE2(R%, 3) = calWATSAFE + ConvergeOverTime(WATSAFEShift2(R%, 3), 0, watsanconv) '100
                    Else
                        'very slow or no down convergence
                        WATSAFE2(R%, 3) = calWATSAFE + WATSAFEShift2(R%, 3) 'ConvergeOverTime(WATSAFEShift(R%, 3), 0, 1000)
                    End If

                    'mti July 18, 2011 - end

                    WATSAFE2(R%, 3) = AMAX(Amin(WATSAFE2(R%, 3), 100), 0)

                    'mti April 05, 2012 - start
                    'revert cat2 from residual to from model

                    '                'mti July 18, 2011 - start
                    '                'mti: in case no connection plus household connection is greater than 100, normalize them
                    '                SomeofTwo = WATSAFE2(R%, 1) + WATSAFE2(R%, 3)
                    '                If SomeofTwo > 100 Then
                    '                    WATSAFE2(R%, 1) = WATSAFE2(R%, 1) * 100 / SomeofTwo
                    '                    WATSAFE2(R%, 3) = WATSAFE2(R%, 3) * 100 / SomeofTwo
                    '                End If
                    '
                    '
                    '                WATSAFE2(R%, 2) = 100 - (WATSAFE2(R%, 1) + WATSAFE2(R%, 3))

                    '                'normalize to 100%
                    calWATSAFE = WATSAFE2(R, 1) + WATSAFE2(R, 2) + WATSAFE2(R, 3)
                    WATSAFE2(R, 1) = WATSAFE2(R, 1) / calWATSAFE * 100
                    WATSAFE2(R, 2) = WATSAFE2(R, 2) / calWATSAFE * 100
                    WATSAFE2(R, 3) = WATSAFE2(R, 3) / calWATSAFE * 100
                    'mti April 05, 2012 - end

                    'mti July 18, 2011 - end
                    'jrs 2019/11/19
                    'For j = 1 To 3
                    '    ifsPopWorkDB.Add(New HLPROXIMATEDRIVERS() With {
                    '        .VarName = "WATSAFE",
                    '        .Year = CShort(IY% + FirstYear - 1),
                    '        .Region = CShort(R%),
                    '        .Dim2 = CShort(j),
                    '        .Dim3 = CShort(j),
                    '        .DistalFormVal = CSng(WATSAFE(R%, j))
                    '    })
                    'Next j
                    'ifsPopWorkDB.Flush()
                End If 'if WatSan switch is On hlwatsansw > 0
                '  If RegName(R%) = "Angola" Then Stop
                'mti February 2011 Infarstructure - start
                'Water - Safe Water Connection Cost
                '            WatSafeConAdded = AMAX(0, (WATSAFE(R%, 3) * POP(R%) / HHSIZE(R%) - SWATSAFE(R%, 3) * CDbl(sPOPTOTAL(R%)) / sHHSIZEWS(R%)) * 10000)
                CalcInfraUnitCost(R%, 10, 5, watsafecostupper, watsafecostlower, sGDPPCP(R%), WATSAFECOST(R%))
                'mti April 2011 -start
                WATSAFECOST(R%) = WATSAFECOST(R%) * watsafecostm(R%)
                'mti April 2011 -end
                '            INFRAINVESTNEW(R%, SafeWaterHH) = WatSafeConAdded * WATSAFECOST(R%) / 1000000 'billion dollars
                '            INFRAINVESTMAINT(R%, SafeWaterHH) = SWATSAFE(R%, 3) * CDbl(sPOPTOTAL(R%)) * 10000 * DeppcntWS(SafeWaterHH) * WATSAFECOST(R%) / 1000000 / sHHSIZEWS(R%) 'billion dollars
                'mti February 2011 Infarstructure - end

                Dim WatSafeDemThousands = WATSAFE(R%, 3) * CDbl(POP(R%) * 10 / HHSIZE(R%)) 'Demanded Connections (stock) in Thousands This Year
                Dim sWatSafeThousands = SWATSAFE(R%, 3) * CDbl(sPOPTotal(R%)) * 10 / sHHSIZEWS(R%) 'Connections (Stock) in Thousands Last Year

                CalcInfraInvest(CDbl(WatSafeDemThousands), CDbl(sWatSafeThousands),
                            DeppcntWS(SafeWaterHH), WATSAFECOST(R%), INFRAINVESTNEW(R%, SafeWaterHH), INFRAINVESTMAINT(R%, SafeWaterHH))


                '----------Safe Water Other Improved cost---------
                '            SanitConadded = AMAX(0, (SANITATION(R%, 3) * POP(R%) / HHSIZE(R%) - SSANITATION(R%, 3) * CDbl(sPOPTOTAL(R%)) / sHHSIZEWS(R%)) * 10000)
                CalcInfraUnitCost(R%, 10, 5, watsafeimpcostupper, watsafeimpcostlower, sGDPPCP(R%), WATSAFEIMPCOST(R%))

                WATSAFEIMPCOST(R%) = WATSAFEIMPCOST(R%) * watsafeimpcostm(R%)

                Dim WatSafeImpDemThousands = WATSAFE(R%, 2) * CDbl(POP(R%) * 10)  '/ HHSIZE(R%)) 'Demanded people in Thousands This Year
                Dim sWatSafeImpThousands = SWATSAFE(R%, 2) * CDbl(sPOPTotal(R%)) * 10 ' / sHHSIZEWS(R%) 'people in Thousands Last Year

                CalcInfraInvest(CDbl(WatSafeImpDemThousands), CDbl(sWatSafeImpThousands),
                            DeppcntWS(SafeWaterImproved), WATSAFEIMPCOST(R%), INFRAINVESTNEW(R%, SafeWaterImproved), INFRAINVESTMAINT(R%, SafeWaterImproved))
                '----- Spending for base forecast water target---------

                'If RegName(R%) = "Tanzania" Then Stop

                INFRAINVESTNEWBF(R%, SafeWaterHH) = INFRAINVESTNEW(R%, SafeWaterHH)
                INFRAINVESTMAINTBF(R%, SafeWaterHH) = INFRAINVESTMAINT(R%, SafeWaterHH)

                INFRAINVESTNEWBF(R%, SafeWaterImproved) = INFRAINVESTNEW(R%, SafeWaterImproved)
                INFRAINVESTMAINTBF(R%, SafeWaterImproved) = INFRAINVESTMAINT(R%, SafeWaterImproved)

                'dkb 2013/09/16 added dimension 1 to watsafenoconsetar
                If watsafetrgtyr(R%) > 0 Or watsafenoconsetar(R%) > 0 Then 'there is an upward RAI target
                    WatSafeDemThousands = WatSafeBF(R%, 3) * CDbl(POP(R%) * 10 / HHSIZE(R%)) 'Demanded Connections (stock) in Thousands This Year

                    'mti June 14, 2012 - start
                    'sWatSafeThousands = sWatSafeBF(R%, 3) * CDbl(sPOPTotal(R%)) * 10 / sHHSIZEWS(R%) 'Connections (Stock) in Thousands Last Year
                    sWatSafeThousands = SWATSAFE(R%, 3) * CDbl(sPOPTotal(R%)) * 10 / sHHSIZEWS(R%) 'Connections (Stock) in Thousands Last Year
                    'mti June 14, 2012 - end



                    CalcInfraInvest(CDbl(WatSafeDemThousands), CDbl(sWatSafeThousands),
                                DeppcntWS(SafeWaterHH), WATSAFECOST(R%), INFRAINVESTNEWBF(R%, SafeWaterHH), INFRAINVESTMAINTBF(R%, SafeWaterHH))


                    WatSafeImpDemThousands = WatSafeBF(R%, 2) * CDbl(POP(R%) * 10)  '/ HHSIZE(R%)) 'Demanded people in Thousands This Year

                    'mti June 14, 2012 - start
                    'sWatSafeImpThousands = sWatSafeBF(R%, 2) * CDbl(sPOPTotal(R%)) * 10 ' / sHHSIZEWS(R%) 'people in Thousands Last Year
                    sWatSafeImpThousands = SWATSAFE(R%, 2) * CDbl(sPOPTotal(R%)) * 10 ' / sHHSIZEWS(R%) 'people in Thousands Last Year
                    'mti June 14, 2012 - end

                    CalcInfraInvest(CDbl(WatSafeImpDemThousands), CDbl(sWatSafeImpThousands),
                                DeppcntWS(SafeWaterImproved), WATSAFEIMPCOST(R%), INFRAINVESTNEWBF(R%, SafeWaterImproved), INFRAINVESTMAINTBF(R%, SafeWaterImproved))

                End If
                'mti Dec 12, 2010 - start
                ''******** All Years but first: Water and Sanitation *********************
                ''***************************** end *************************************

                'mti February 2011 Infarstructure -start
                'All Years but first: WasteWater
                '--------------------------------------

                '   If RegName(R%) = "Ethiopia" And IY >= 12 Then Stop
                '---% connected to wastewater collection system------------

                'mti February 28, 2012  -watwaste revert to older model

                'mti August 17, 2011 -start
                'PopUrbanTerm = 100 * POPURBAN(R%) / POP(R%)
                Dim PopUrbanTerm = 100 * SPOPURBAN(R%) / sPOPTotal(R%)
                Prediction = CSng(SSANITATION(R%, 3) / (1 + Math.Exp(2.339779 - 0.042719 * GDPPCP(R%) - 0.041923 * PopUrbanTerm)))


                'PopUrbanTerm = 100 * (sPOPTOTAL(R%) - sPOPRURAL(R%)) / sPOPTOTAL(R%)
                Dim PopUrbanTerm1 = 100 * sPopUrban1(R%) / sPopTotal1(R%)
                Prediction1 = CSng(sssanitation(R%, 3) / (1 + Math.Exp(2.339779 - 0.042719 * sGDPPCP(R%) - 0.041923 * PopUrbanTerm1)))

                'Movement with function
                Dim cWatWasteGrowthRateFunction As Single = 0
                If Prediction > Prediction1 Then
                    'percentage controlled by predicted values only
                    'mti 01/16/2013 - InfraWatsan Run2Run -start
                    'cWatWasteGrowthRateFunction = (Prediction - Prediction1) / AMAX(0.001, Prediction1)
                    cWatWasteGrowthRateFunction = CSng(Amin(0.3, (Prediction - Prediction1) / AMAX(0.001, Prediction1)))
                    'mti 01/16/2013 - InfraWatsan Run2Run - end
                Else
                    cWatWasteGrowthRateFunction = 0.000005 'negligible growth
                End If
                'mti July 18, 2011 - start
                'movement to function
                Dim cWatWasteGrowthRatePosition As Single = 0
                If Prediction > PreMultWATWASTE(R%) Then ' SWatWaste(R%)
                    Dim RatChange = Prediction / AMAX(PreMultWATWASTE(R%), 0.001) ' SWatWaste(R%)

                    'mti 01/16/2013 - InfraWatsan Run2Run -start
                    'needs faster convergence at lower rates
                    'ConvTime = Amin(40, (Int(PreMultWATWASTE(R%) / 5) + 1) * 5) '30  'years) ' SWatWaste(R%)
                    Dim ConvTime = AMAX(1, Amin(40, (Math.Floor(PreMultWATWASTE(R%) / 5) + 1) * 5)) '30  'years) ' SWatWaste(R%)

                    'cWatWasteGrowthRatePosition = RatChange ^ (1 / ConvTime) - 1 'can be negative
                    cWatWasteGrowthRatePosition = CSng(Amin(0.3, RatChange ^ (1 / ConvTime) - 1)) 'can be negative
                    'mti 01/16/2013 - InfraWatsan Run2Run - end
                Else
                    cWatWasteGrowthRatePosition = 0.000005 'negligible growth
                End If
                'mti August 17, 2011 - start
                'ttt vb6: If Not IsNull(WATWASTEGR(R%)) Then
                If WATWASTEGR(R%).HasValue Then
                    '--Growth Rate -----
                    WATWASTEGR(R%) = CSng(0.8 * WATWASTEGR(R%) + 0.05 * cWatWasteGrowthRateFunction + 0.15 * cWatWasteGrowthRatePosition)
                    WATWASTE(R%) = CSng(PreMultWATWASTE(R%) * (1 + WATWASTEGR(R%))) ' SWatWaste(R%)
                Else
                    If WatWasteColShift(R%) < 0 Then
                        WATWASTE(R%) = Prediction + ConvergeOverTime(WatWasteColShift(R%), 0, 25)
                    Else 'slower/no downward convergence
                        WATWASTE(R%) = Prediction + ConvergeOverTime(WatWasteColShift(R%), 0, 250)
                    End If
                    WATWASTEGR(R%) = (WATWASTE(R%) / sWATWASTE(R%)) - 1
                End If


                'mti note: use SSanitation here?
                PreMultWATWASTE(R%) = Amin(SANITATION(R%, 3), WATWASTE(R%))
                'mti July 18, 2011 - END

                'mti note: multiplier not applied in other infrastructures
                WATWASTE(R%) = WATWASTE(R%) * watwastem(R%)


                WATWASTE(R%) = Amin(SANITATION(R%, 3), WATWASTE(R%))

                'mti July 25, 2012 -start
                '            'base forecast (needed to protect finance)
                '            PredictionBF = SANITATIONBF(R%, 3) / (1 + Exp(2.339779 - 0.042719 * GDPPCP(R%) - 0.041923 * PopUrbanTerm))
                '
                '            If PredictionBF > Prediction1 Then
                '                'percentage controlled by predicted values only
                '                cWatWasteGrowthRateFunctionBF = (PredictionBF - Prediction1) / AMAX(0.001, Prediction1)
                '            Else
                '                cWatWasteGrowthRateFunctionBF = 0.000005 'negligible growth
                '            End If
                '
                '
                '
                '            If PredictionBF > PreMultWATWASTEBF(R%) Then ' SWatWaste(R%)
                '                RatChange = PredictionBF / AMAX(PreMultWATWASTEBF(R%), 0.001) ' SWatWaste(R%)
                '
                '                'needs faster convergence at lower rates
                '                ConvTime = Amin(40, (Int(PreMultWATWASTEBF(R%) / 5) + 1) * 5) '30  'years) ' SWatWaste(R%)
                '                cWatWasteGrowthRatePositionBF = RatChange ^ (1 / ConvTime) - 1 'can be negative
                '            Else
                '                cWatWasteGrowthRatePositionBF = 0.000005 'negligible growth
                '            End If
                '
                '
                '
                '            If Not IsNull(WatWasteGRBF(R%)) Then
                '                '--Growth Rate -----
                '                WatWasteGRBF(R%) = 0.8 * WatWasteGRBF(R%) + 0.05 * cWatWasteGrowthRateFunctionBF + 0.15 * cWatWasteGrowthRatePositionBF
                '                WATWASTEBF(R%) = PreMultWATWASTEBF(R%) * (1 + WatWasteGRBF(R%)) ' SWatWaste(R%)
                '            Else
                '                If WatWasteColShift(R%) < 0 Then
                '                    WATWASTEBF(R%) = PredictionBF + ConvergeOverTime(WatWasteColShift(R%), 0, 25)
                '                Else 'slower/no downward convergence
                '                    WATWASTEBF(R%) = PredictionBF + ConvergeOverTime(WatWasteColShift(R%), 0, 250)
                '                End If
                '                WatWasteGRBF(R%) = (WATWASTEBF(R%) / sWATWASTE(R%)) - 1
                '            End If
                '
                '            PreMultWATWASTEBF(R%) = Amin(SANITATIONBF(R%, 3), WATWASTEBF(R%))
                '            WATWASTEBF(R%) = Amin(SANITATIONBF(R%, 3), WATWASTEBF(R%))
                'mti July 25, 2012 - end



                'WATWASTE(R%) = SANITATION(R%, 3) ' model revision : watwaste = sanitation- HH 'WATWASTE(R%) * watwastem(R%)


                'mti July 25, 2012 -end
                'mti August 17, 2011 -end

                ' If RegName(R%) = "Ghana" And IY% = 2 Then Debug.Print IY; WATWASTE(R%); SWatWaste(R%); POPURBAN(R%); POP(R%); SANITATION(R%, 3); SSANITATION(R%, 3); GDPPCP(R%); sGDPPCP(R%); sPOPTOTAL(R%); sPOPRURAL(R%); WATWASTEGR(R%); cWatWasteGrowthRateFunction; cWatWasteGrowthRatePosition


                'If R% = 75 Then Stop
                ' If RegName(R%) = "India" Then Stop
                '---All Year But First: % connected to wastewater treatment system------------
                'mti - (moving average) Growth rate approach will be problematic when one variable converges to other;
                'With initial data/prediction gap on the first one not yet merged, the second one might converge and thus get a miniscule growth rate
                'with moving average this slower growth rate subsequently creates a divergence for some time
                'mti Sep 12, 2011 - start
                'Prediction = WATWASTE(R%) / (1 + Exp(1.036711 - 0.42925 * WATWASTE(R%)))
                Prediction = CSng(100 / (1 + Math.Exp(2.4821024086192 - 0.0382380937051429 * sGDPPCP(R%) - 0.0288342684215034 * WATWASTE(R%))))


                ' If RegName(R%) = "Venezuela" Then Stop
                'mti August 22, 2011 -start
                Dim WWTAdd As Single = 0
                If WatWasteTreatShift(R%) < 0 Then
                    WWTAdd = ConvergeOverTime(WatWasteTreatShift(R%), 0, 25)
                Else 'slower/no downward convergence
                    WWTAdd = ConvergeOverTime(WatWasteTreatShift(R%), 0, 250)
                End If
                'dkb 2013/09/18 added dimension 1 to watwastetreatsetar and watwastetreateseyrtar
                If watwastetreatsetar(R%) <> 0 Then
                    Dim WWTSE As Single = 18.8
                    'mti July 05, 2012 -parameter for 100% bound
                    EffectSE = StdErrorAdditiveShift(CSng(WWTSE), CSng(Prediction), CSng(WWTAdd), CSng(WatWasteTreatShift(R%)),
                CSng(watwastetreatsetar(R%)), CInt(FirstYear + watwastetreatseyrtar(R%)), FirstYear + IY - 1, FirstYear, 100)
                    'SE, Prediction, DataAddShift, IDataAddShift, TargetSEs, TargetYr, CurrentYr
                Else
                    EffectSE = 0
                End If

                'mti note: JRS original model did not have upper and lower bounds here; Check if that makes a difference
                WATWASTETREAT(R%) = Prediction + WWTAdd + EffectSE

                'new model as % of pop
                'WATWASTETREAT(R%) = PcntColTreated * WATWASTE(R%) / 100
                'WATWASTETREAT(R%) = PcntColTreated
                'mti Sep 12, 2011 - end
                'mti August 22, 2011 - end

                WATWASTETREAT(R%) = WATWASTETREAT(R%) * watwastetreatm(R%)


                'mti June 04, 2012 -start
                'already established access rates cannot go down unless there is a hard multiplier watsafem(R%, 3)
                'hh conn
                If watwastetreatm(R%) >= 1 Then
                    WATWASTETREAT(R%) = AMAX(SWATWASTETREAT(R%), WATWASTETREAT(R%))
                End If

                '    If RegName(R%) = "Ethiopia" Then Stop

                WATWASTETREAT(R%) = Amin(WATWASTE(R%), WATWASTETREAT(R%))
                'mti June 04, 2012 - end

                'base forecast
                '            PredictionBF = 100 / (1 + Exp(2.4821024086192 - 3.82380937051429E-02 * sGDPPCP(R%) - 2.88342684215034E-02 * WATWASTEBF(R%)))
                '            WatWasteTreatBF(R%) = PredictionBF + WWTAdd
                '            WatWasteTreatBF(R%) = Amin(WATWASTEBF(R%), WatWasteTreatBF(R%))

                '--cost of new construction of treatment plants ---------
                Dim WATWASTETREATAdded = WATWASTETREAT(R%) * POP(R%) / 100 - SWATWASTETREAT(R%) * CDbl(sPOPTotal(R%)) / 100 'million people added
                'single global cost and multiplier

                'mti October 20, 2011 - start
                'mti February 28, 2012 - start
                '            WATWASTETREATCOST(R%) = watwastetreatcostglobal * (ConvergeOverTime(INFRACOSTADJFAC(R%), 1, 100))
                '            'WATWASTETREATCOST(R%) = iWatWasteTreatCost(R%) * watwastetreatcostm(R%)  'multiplier on cost; cost is per person

                CalcInfraUnitCost(R%, 10, 5, watwastetreatcostupper, watwastetreatcostlower, sGDPPCP(R%), WATWASTETREATCOST(R%))
                'mti February 28, 2012 -end

                WATWASTETREATCOST(R%) = WATWASTETREATCOST(R%) * watwastetreatcostm(R%)  'multiplier on cost; cost is per person
                'mti October 20, 2011 - end

                INFRAINVESTNEW(R%, WasteWater) = AMAX(WATWASTETREATAdded, 0) * WATWASTETREATCOST(R%)  'million people * 1000 $ = billion $
                INFRAINVESTMAINT(R%, WasteWater) = CSng(SWATWASTETREAT(R%) * CDbl(sPOPTotal(R%)) * DeppcntWS(WasteWater) * WATWASTETREATCOST(R%) / 100) 'million people * 1000 $ = billion $)

                '----- Spending for base forecast road target---------

                INFRAINVESTNEWBF(R%, WasteWater) = INFRAINVESTNEW(R%, WasteWater)
                INFRAINVESTMAINTBF(R%, WasteWater) = INFRAINVESTMAINT(R%, WasteWater)

                '            If sanitationtrgtyr(R%) > 0 Or sanitnoconsetar > 0 Or watwastetreatsetar > 0 Then 'there is an upward RAI target
                '                WATWASTETREATAddedBF = WatWasteTreatBF(R%) * POP(R%) / 100 - SWATWASTETREAT(R%) * CDbl(sPOPTotal(R%)) / 100 'million people added
                '
                '                INFRAINVESTNEWBF(R%, WasteWater) = AMAX(WATWASTETREATAddedBF, 0) * WATWASTETREATCOST(R%)  'million people * 1000 $ = billion $
                '                INFRAINVESTMAINTBF(R%, WasteWater) = SWATWASTETREAT(R%) * CDbl(sPOPTotal(R%)) * DeppcntWS(WasteWater) * WATWASTETREATCOST(R%) / 100 'million people * 1000 $ = billion $
                '
                '            End If

                'mti July 25, 2012 -move this - moved back to avoid run to run
                '            'save GDP per capita for next year
                sGDPPCP(R%) = GDPPCP(R%)
            Next R%

            'mti 2022/09/16 start
            'mti 2022/10/20 start   watsafefwdsw
            'If watsafenewsw < 1 Then    'write only when forward linkage from new (5 category model) is off
            If watsafenewsw < 1 And watsafefwdsw < 1 Then   'write only when forward linkage from new (5 category model) is off
                'mti 2022/10/20 End
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(SANITATION, NR, IY + FirstYear - 1, "SANITATION", 3))
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertHLProxDrivers(WATSAFE, NR, IY + FirstYear - 1, "WATSAFE", 3))
                'ifsPopWorkDB.BulkInsertHLProxDrivers(SANITATION, NR, IY + FirstYear - 1, "SANITATION", 3)
                'ifsPopWorkDB.BulkInsertHLProxDrivers(WATSAFE, NR, IY + FirstYear - 1, "WATSAFE", 3)
            End If

        End Sub

        Public Sub IFsInfraWATSAN2AllbutFirstYear(context As IForecastContext)
            'call2:
            'ttt - No FirstYear for call2
            'IFsInfraWATSAN2AllYears() -ttt 
            'ttt--------
            ReDim INFRAINVESTNEWBF(NR%, NInfraType%)
            ReDim INFRAINVESTMAINTBF(NR%, NInfraType%)
            Dim EffectSE As Single = 0
            Dim PhysStckDmndThou As Double

            For R% = 1 To NR%
                'If IY >= 26 And RegName(R%) = "DominicanRep" Then Stop

                'All Years but First 2: Infrastructure Budget Impact
                '----------------------------------------------------
                'mti February 2011 Infarstructure -start
                'BudImpactOn = infrafinon(R%)  'switch for turning impact on
                If infrafinon = 1 Then
                    'mti February 2011 Infarstructure -end
                    'sInfraBudImpact(R%) already calculated in IfsInfra

                    '                  If R% = 75 Then Stop
                    'If RegName(R%) = "Ethiopia" And IY >= 12 Then Stop
                    If sInfraBudImpact(R%) < 1 Then 'deficit
                        'If IY = 23 And GRPS(1, R) = "Zambia" Then Stop

                        'mti 2022/09/25 start
                        'mti 2022/10/20 start
                        If watsafenewsw < 1 Then        'switch off; use old (3 category model for forward linkage and financing
                            'mti 2022/09/25 End

                            'Water Household Connections -----------------------------------------------------
                            PhysStckDmndThou = WATSAFE(R%, 3) * CDbl(POP(R%) * 10 / HHSIZE(R%)) 'Demanded Connections (stock) in ThoUSAnds This Year
                            sWatSafeThousands = CSng(SWATSAFE(R%, 3) * CDbl(sPOPTotal(R%)) * 10 / sHHSIZEWS(R%)) 'Connections (Stock) in ThoUSAnds Last Year

                            CalcInfraBudgetImpact(R%, PhysStckDmndThou, CDbl(sWatSafeThousands), WATSAFECOST(R%), SafeWaterHH)

                            'save household connections value before budget impact
                            Dim TWatSafeHC = WATSAFE(R%, 3)
                            WATSAFE(R%, 3) = CSng(PhysStckDmndThou * HHSIZE(R%) / POP(R%) / 10)
                            Dim WatSafeHC_Diff = TWatSafeHC - WATSAFE(R%, 3)


                            'Water Improved Connections -----------------------------------------------------
                            PhysStckDmndThou = WATSAFE(R%, 2) * CDbl(POP(R%) * 10)  '/ HHSIZE(R%)) 'Persons (stock) in ThoUSAnds This Year
                            Dim sPhysStckThoUSAnds = SWATSAFE(R%, 2) * CDbl(sPOPTotal(R%)) * 10 '/ sHHSIZEWS(R%) 'persons  (Stock) in ThoUSAnds Last Year

                            CalcInfraBudgetImpact(R%, PhysStckDmndThou, CDbl(sPhysStckThoUSAnds), WATSAFEIMPCOST(R%), SafeWaterImproved)

                            Dim TWatSafeImp = WATSAFE(R%, 2)
                            WATSAFE(R%, 2) = CSng(PhysStckDmndThou / POP(R%) / 10)
                            Dim WatSafeImp_Diff = TWatSafeImp - WATSAFE(R%, 2)

                            'give all cutback to none -DSR suggestion -does not work -see new code below
                            Dim tadd = WatSafeHC_Diff + WatSafeImp_Diff
                            If tadd > 0 Then
                                Dim tother = SWATSAFE(R%, 1) + SWATSAFE(R%, 2)
                                If tother > 0 Then
                                    Dim SaveWatSafeImp = WATSAFE(R%, 2)
                                    'If PhysStckDmndThoUSAnitImp < sPhysStckThoUSAnds Then
                                    If WATSAFE(R%, 2) < SWATSAFE(R%, 2) Then
                                        'if desired physical level of improved (2nd cat.) is less than actual of the previous year, add back to improved from HH cutback
                                        'proportionately upto previous year improved level
                                        WATSAFE(R%, 2) = Amin(SWATSAFE(R%, 2), WATSAFE(R%, 2) + tadd)  '* SWatSafe(R%, 2) / tother
                                    End If
                                    Dim CutbackGiven2Imp = WATSAFE(R%, 2) - SaveWatSafeImp

                                    If CutbackGiven2Imp < -0.01 Then Stop

                                    Dim CutbackRemaining = tadd - AMAX(0, CutbackGiven2Imp)

                                    WATSAFE(R%, 1) = WATSAFE(R%, 1) + CutbackRemaining
                                Else  ' SWATSAFE(R%, 1) + SWATSAFE(R%, 2)=0
                                    'already reached 100% HH in the previous year, coming down this year from financial shortage
                                    If tadd < 0.75 Then
                                        'small (hh) cutbacks  might be from poorly maintained household connections
                                        'in the most likely case these will return to other improved, not none;
                                        'there might be a (small) financial mismtach; however, no access graph will look better
                                        WATSAFE(R%, 2) = WATSAFE(R%, 2) + tadd
                                    Else
                                        'bigger (hh) cutbacks must be adjusted without any financial imbalance, i.e., by adding back to none
                                        WATSAFE(R%, 1) = WATSAFE(R%, 1) + tadd
                                    End If
                                End If
                                'jrs 2019/05/06 *** BEGIN ***
                                If watsafeexovals(R%, 1) > -100 Then
                                    WATSAFE(R%, 1) = watsafeexovals(R%, 1)
                                End If
                                If watsafeexovals(R%, 2) > -100 Then
                                    WATSAFE(R%, 2) = watsafeexovals(R%, 2)
                                End If
                                If watsafeexovals(R%, 3) > -100 Then
                                    WATSAFE(R%, 3) = watsafeexovals(R%, 3)
                                End If
                                'jrs 2019/05/06 *** END ***
                            End If

                            'mti 01/16/2013 - InfraWatsan Run2Run - start
                            For j = 1 To 3
                                'jrs 2020/06/08 WATSAFE(R%, j) = Round(AMAX(0, Amin(100, WATSAFE(R%, j))), 4)
                                WATSAFE(R%, j) = AMAX(0, Amin(100, WATSAFE(R%, j)))
                            Next j

                            'Sanitation -----------------------------------------------------

                            PhysStckDmndThou = SANITATION(R%, 3) * CDbl(POP(R%) * 10 / HHSIZE(R%)) 'Demanded Connections (stock) in ThoUSAnds This Year
                            Dim sSanitThousands = SSANITATION(R%, 3) * CDbl(sPOPTotal(R%)) * 10 / sHHSIZEWS(R%) 'Connections (Stock) in ThoUSAnds Last Year

                            CalcInfraBudgetImpact(R%, PhysStckDmndThou, CDbl(sSanitThousands), SANITATIONCOST(R%), SanitationHH)

                            'save household connections value before budget impact
                            Dim TSanitHC = SANITATION(R%, 3)
                            SANITATION(R%, 3) = CSng(PhysStckDmndThou * HHSIZE(R%) / POP(R%) / 10)


                            Dim SanitHC_Diff = TSanitHC - SANITATION(R%, 3)

                            'Water Improved Connections -----------------------------------------------------
                            PhysStckDmndThou = SANITATION(R%, 2) * CDbl(POP(R%) * 10)  '/ HHSIZE(R%)) 'persons (stock) in ThoUSAnds This Year
                            sPhysStckThoUSAnds = SSANITATION(R%, 2) * CDbl(sPOPTotal(R%)) * 10 ' / sHHSIZEWS(R%) 'persons in ThoUSAnds Last Year

                            Dim PhysStckDmndThoUSAnitImp = PhysStckDmndThou

                            CalcInfraBudgetImpact(R%, PhysStckDmndThou, CDbl(sPhysStckThoUSAnds), SANITATIONIMPCOST(R%), SanitationImproved)

                            'save value before budget impact
                            Dim TSanitImp = SANITATION(R%, 2)
                            SANITATION(R%, 2) = CSng(PhysStckDmndThou / POP(R%) / 10)
                            Dim SanitImp_Diff = TSanitImp - SANITATION(R%, 2)

                            'mti May 04, 2012 - start
                            'give all cutback to none -DSR suggestion
                            'SANITATION(R%, 1) = AMAX(0, (Amin(100, SANITATION(R%, 1) + SanitHC_Diff + SanitImp_Diff)))
                            'the above algorith creates problem; makes none in target case less than the same in base case

                            'give construction cutback to both, like before, but checking to make sure imp do not go above last year with addition
                            'since this branch is visited for budimpact <0 , _diff is always positive

                            tadd = SanitHC_Diff + SanitImp_Diff
                            If tadd > 0 Then
                                Dim tother = SSANITATION(R%, 1) + SSANITATION(R%, 2)
                                If tother > 0 Then
                                    Dim SaveSANITATIONImp = SANITATION(R%, 2)
                                    'If PhysStckDmndThoUSAnitImp < sPhysStckThoUSAnds Then
                                    If SANITATION(R%, 2) < SSANITATION(R%, 2) Then
                                        'if desired physical level of improved (2nd cat.) is less than actual of the previous year, add back to improved from HH cutback
                                        'proportionately upto previous year improved level
                                        SANITATION(R%, 2) = Amin(SSANITATION(R%, 2), SANITATION(R%, 2) + tadd)  '* SSANITATION(R%, 2) / tother
                                    End If
                                    Dim CutbackGiven2Imp = SANITATION(R%, 2) - SaveSANITATIONImp


                                    Dim CutbackRemaining = tadd - AMAX(0, CutbackGiven2Imp)

                                    SANITATION(R%, 1) = SANITATION(R%, 1) + CutbackRemaining

                                Else  ' SSANITATION(R%, 1) + SSANITATION(R%, 2)
                                    'already reached 100% HH in the previous year, coming down this year from financial shortage
                                    If tadd < 0.75 Then
                                        'small (hh) cutbacks  might be from poorly maintained household connections
                                        'in the most likely case these will return to other improved, not none;
                                        'there might be a (small) financial mismtach; however, no access graph will look better
                                        SANITATION(R%, 2) = SANITATION(R%, 2) + tadd
                                    Else
                                        'bigger (hh) cutbacks must be adjusted without any financial imbalance, i.e., by adding back to none
                                        SANITATION(R%, 1) = SANITATION(R%, 1) + tadd
                                    End If
                                End If
                                'jrs 2019/05/06 *** BEGIN ***
                                If sanitationexovals(R%, 1) > -100 Then
                                    SANITATION(R%, 1) = sanitationexovals(R%, 1)
                                End If
                                If sanitationexovals(R%, 2) > -100 Then
                                    SANITATION(R%, 2) = sanitationexovals(R%, 2)
                                End If
                                If sanitationexovals(R%, 3) > -100 Then
                                    SANITATION(R%, 3) = sanitationexovals(R%, 3)
                                End If
                                'jrs 2019/05/06 *** END ***
                            End If

                            'mti 01/16/2013 - InfraWatsan Run2Run - start
                            For j = 1 To 3
                                SANITATION(R%, j) = AMAX(0, Amin(100, SANITATION(R%, j)))
                            Next j

                        End If 'watsafenewsw < 1 Then

                        'Waste Water Treatment
                        PhysStckDmndThou = WATWASTETREAT(R%) * POP(R%) * 10 'thoUSAnd connections
                        Dim sPhysStckDmndThou = SWATWASTETREAT(R%) * CDbl(sPOPTotal(R%)) * 10 'thoUSAnd connections

                        CalcInfraBudgetImpact(R%, PhysStckDmndThou, CDbl(sPhysStckDmndThou), WATWASTETREATCOST(R%), WasteWater)
                        'mti 2022/10/20 End

                        WATWASTETREAT(R%) = CSng(PhysStckDmndThou / POP(R%) / 10)
                        WATWASTETREAT(R%) = Amin(WATWASTE(R%), WATWASTETREAT(R%))

                        'readjust watwaste? (at the end)
                        'mti June 04, 2012 -start
                    ElseIf sInfraBudImpact(R%) > 1 Then

                        'mti 2022/09/25 start
                        If watsafenewsw < 1 Then    'switch off; use old (3 category model for forward linkage and financing
                            'mti 2022/09/25 End

                            'the extrafund and demand calculation will already be done at IFsInfra

                            'how much is extra
                            'ExtraFundTotal(R%) carried from IFsInfra ' = (INFRABUDDEMNEW(R%, InfraTot) + INFRABUDDEMMNT(R%, InfraTot)) * (sInfraBudImpact(R%) - 1)

                            'demand by candidate sectors
                            'TotalDemandFromCandidates(R%) carried from IFsInfra '= TotalDemandFromCandidates(R%) + INFRAINVESTNEWPUB(R%, RoadPaved) + INFRAINVESTNEWPUB(R%, RoadUnpaved) + INFRAINVESTNEWPUB(R%, ElectricityGen)



                            'invest (only for public; no change in private)
                            'ExtraFundUsed(R%) carried from IfsInfra ' = 0

                            '         If RegName(R%) = "Cuba" And IY >= 28 Then Stop

                            'Sanitation: HHC
                            PhysStckDmndThou = SANITATION(R%, 3) * CDbl(POP(R%) * 10 / HHSIZE(R%)) 'Demanded Connections (stock) in Thousands This Year)
                            'If IY >= 3 And R = 95 Then Stop
                            CalcInfraUseExtraBudget(PhysStckDmndThou, SANITATIONCOST(R%), ExtraFundAllocated(R%, SanitationHH), INFRAINVESTNEWPUB(R%, SanitationHH), INFRAINVESTNEW(R%, SanitationHH), ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))
                            Dim SaveSanHH = SANITATION(R%, 3)
                            SANITATION(R%, 3) = CSng(Amin(100, PhysStckDmndThou / CDbl(POP(R%) * 10 / HHSIZE(R%))))

                            Dim SanitHHAdded = AMAX(0, (SANITATION(R%, 3) - SaveSanHH))

                            Dim SaveSanImp = SANITATION(R%, 2)
                            'mti: might lead to fin. mismatch

                            SANITATION(R%, 2) = CSng(AMAX(0, SANITATION(R%, 2) - 0.5 * SanitHHAdded))   '/ SanitNonHHSum
                            Dim SanitImpTaken = AMAX(0, (SaveSanImp - SANITATION(R%, 2)))

                            Dim SanitHHAddedRem = AMAX(0, (SanitHHAdded - SanitImpTaken))

                            Dim SaveSanNone = SANITATION(R%, 1)
                            SANITATION(R%, 1) = AMAX(0, SANITATION(R%, 1) - SanitHHAddedRem)   '/ SanitNonHHSum
                            Dim SanitNoneTaken = AMAX(0, (SaveSanNone - SANITATION(R%, 1)))

                            Dim SanitHHAddedRemFurther = AMAX(0, (SanitHHAddedRem - SanitNoneTaken))
                            SANITATION(R%, 2) = AMAX(0, SANITATION(R%, 2) - SanitHHAddedRemFurther)   '/ SanitNonHHSum

                            SANITATION(R%, 1) = AMAX(0, 100 - SANITATION(R%, 3) - SANITATION(R%, 2))

                            'note from MTI - this might give slight financial mismatch for Improved category (see if you can do anything later)

                            'WatSafe: HHC
                            'If (IY = 7 Or IY = 18) And GRPS(1, R) = "Zambia" Then Stop
                            PhysStckDmndThou = WATSAFE(R%, 3) * CDbl(POP(R%) * 10 / HHSIZE(R%)) 'Demanded Connections (stock) in Thousands This Year
                            CalcInfraUseExtraBudget(PhysStckDmndThou, WATSAFECOST(R%), ExtraFundAllocated(R%, SafeWaterHH), INFRAINVESTNEWPUB(R%, SafeWaterHH), INFRAINVESTNEW(R%, SafeWaterHH), ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))
                            Dim SaveWatHH = WATSAFE(R%, 3)
                            'jrs 2020/06/08 WATSAFE(R%, 3) = CSng(Math.Round(Amin(100, PhysStckDmndThou / CDbl(POP(R%) * 10 / HHSIZE(R%))), 4)) 
                            WATSAFE(R%, 3) = CSng(Amin(100, PhysStckDmndThou / CDbl(POP(R%) * 10 / HHSIZE(R%))))

                            Dim WatSafHHAdded = AMAX(0, (WATSAFE(R%, 3) - SaveWatHH))

                            Dim SaveWatSafImp = WATSAFE(R%, 2)
                            'mti: might lead to fin. mismatch
                            WATSAFE(R%, 2) = CSng(AMAX(0, WATSAFE(R%, 2) - 0.5 * WatSafHHAdded))   '/ SanitNonHHSum
                            Dim WatSafImpTaken = AMAX(0, (SaveWatSafImp - WATSAFE(R%, 2)))

                            Dim WatSafHHAddedRem = AMAX(0, (WatSafHHAdded - WatSafImpTaken))

                            Dim SaveWatSafNone = WATSAFE(R%, 1)
                            WATSAFE(R%, 1) = AMAX(0, WATSAFE(R%, 1) - WatSafHHAddedRem)   '/ SanitNonHHSum
                            Dim WatSafNoneTaken = AMAX(0, (SaveWatSafNone - WATSAFE(R%, 1)))

                            Dim WatSafHHAddedRemFurther = AMAX(0, (WatSafHHAddedRem - WatSafNoneTaken))
                            'jrs 2020/06/08 WATSAFE(R%, 2) = CSng(Math.Round(AMAX(0, WATSAFE(R%, 2) - WatSafHHAddedRemFurther), 4))    '/ SanitNonHHSum
                            WATSAFE(R%, 2) = AMAX(0, WATSAFE(R%, 2) - WatSafHHAddedRemFurther)    '/ SanitNonHHSum

                            'jrs 2020/06/08 WATSAFE(R%, 1) = CSng(Math.Round(AMAX(0, 100 - WATSAFE(R%, 3) - WATSAFE(R%, 2)), 4)) 
                            WATSAFE(R%, 1) = AMAX(0, 100 - WATSAFE(R%, 3) - WATSAFE(R%, 2))

                            'mti 2022/09/25 start
                        End If 'watsafenewsw < 1 Then
                        'mti 2022/09/25 End

                        'If RegName(R%) = "Ethiopia" Then Stop

                        'WWTreat
                        PhysStckDmndThou = WATWASTETREAT(R%) * POP(R%) * 10 'thousand connections
                        CalcInfraUseExtraBudget(PhysStckDmndThou, WATWASTETREATCOST(R%), ExtraFundAllocated(R%, WasteWater), INFRAINVESTNEWPUB(R%, WasteWater), INFRAINVESTNEW(R%, WasteWater), ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))
                        WATWASTETREAT(R%) = CSng(Amin(100, PhysStckDmndThou / AMAX(0.000001, POP(R%)) / 10))

                        'adjust watawste if needed
                        WATWASTE(R%) = AMAX(WATWASTETREAT(R%), WATWASTE(R%))

                        'if yes, put the extra in the box cart


                        'mti June 04, 2012 -end
                    End If 'sInfraBudImpact(R%) < 1
                End If 'BudImpactOn = 1
                'mti Dec 12, 2010 - start
                If watsafhldsw = 0 Then
                    'jrs 2009/12/23
                    For j = 1 To 3
                        'jrs 2020/06/08 SWATSAFE(R%, j) = CSng(Math.Round(WATSAFE(R%, j), 4)) 
                        SWATSAFE(R%, j) = WATSAFE(R%, j)
                    Next j
                End If
                'mti Dec 12, 2010 - end
                '             If IY >= 69 And RegName(R%) = "DominicanRep" Then Stop
                '             If SANITATION(R%, 1) < 0 And RegName(R%) = "DominicanRep" Then Stop
                'mti Dec 12, 2010 - start
                If sanithldsw = 0 Then
                    'jrs 2009/12/22
                    For j = 1 To 3
                        sssanitation(R%, j) = SSANITATION(R%, j)
                        SSANITATION(R%, j) = SANITATION(R%, j)
                    Next j
                End If
                'mti Dec 12, 2010 - end

                'mti Infrastructure Cost August 2010 start
                sHHSIZEWS(R%) = HHSIZE(R%)

                'mti February 2011 Infarstructure - start
                ' To avoid run to run
                'If RegName(R%) = "Indonesia" Then Stop
                SWATWASTETREAT(R%) = WATWASTETREAT(R%)

                'mti July 25, 2012 - start
                'revise watwaste
                'WATWASTE(R%) = (2.4821024086192 - 3.82380937051429E-02 * sGDPPCP(R%) - Log(100 / WATWASTETREAT(R%) - 1)) / 2.88342684215034E-02

                'adjust WATWASTE per sanitation cap
                '-----------------------------------
                '                PopUrbanTerm = 100 * SPOPURBAN(R%) / sPOPTotal(R%)
                '                Prediction = SANITATION(R%, 3) / (1 + Exp(2.339779 - 0.042719 * GDPPCP(R%) - 0.041923 * PopUrbanTerm))
                '
                '
                '                'PopUrbanTerm = 100 * (sPOPTOTAL(R%) - sPOPRURAL(R%)) / sPOPTOTAL(R%)
                '                PopUrbanTerm1 = 100 * sPopUrban1(R%) / sPopTotal1(R%)
                '                Prediction1 = SSANITATION(R%, 3) / (1 + Exp(2.339779 - 0.042719 * sGDPPCP(R%) - 0.041923 * PopUrbanTerm1))
                '
                '                'Movement with function
                '                If Prediction > Prediction1 Then
                '                    'percentage controlled by predicted values only
                '                    cWatWasteGrowthRateFunction = (Prediction - Prediction1) / AMAX(0.001, Prediction1)
                '                Else
                '                    cWatWasteGrowthRateFunction = 0.000005 'negligible growth
                '                End If
                '
                '
                '                'mti July 18, 2011 - start
                '                'movement to function
                '                If Prediction > sPreMultWATWASTE(R%) Then ' SWatWaste(R%)
                '                    RatChange = Prediction / AMAX(sPreMultWATWASTE(R%), 0.001) ' SWatWaste(R%)
                '
                '                    'needs faster convergence at lower rates
                '                    ConvTime = Amin(40, (Int(sPreMultWATWASTE(R%) / 5) + 1) * 5) '30  'years) ' SWatWaste(R%)
                '                    cWatWasteGrowthRatePosition = RatChange ^ (1 / ConvTime) - 1 'can be negative
                '                Else
                '                    cWatWasteGrowthRatePosition = 0.000005 'negligible growth
                '                End If
                '
                '
                '
                '
                '                'mti August 17, 2011 - start
                '                If Not IsNull(sPreMultWATWASTEGR(R%)) Then
                '                    '--Growth Rate -----
                '                    WATWASTEGR(R%) = 0.8 * sPreMultWATWASTEGR(R%) + 0.05 * cWatWasteGrowthRateFunction + 0.15 * cWatWasteGrowthRatePosition
                '                    WATWASTE(R%) = sPreMultWATWASTE(R%) * (1 + WATWASTEGR(R%)) ' SWatWaste(R%)
                '                Else
                '                    If WatWasteColShift(R%) < 0 Then
                '                        WATWASTE(R%) = Prediction + ConvergeOverTime(WatWasteColShift(R%), 0, 25)
                '                    Else 'slower/no downward convergence
                '                        WATWASTE(R%) = Prediction + ConvergeOverTime(WatWasteColShift(R%), 0, 250)
                '                    End If
                '                    WATWASTEGR(R%) = (WATWASTE(R%) / sWATWASTE(R%)) - 1
                '                End If
                '
                '                'PreMultWATWASTE(R%) = Amin(SANITATION(R%, 3), WATWASTE(R%))
                '                'mti July 18, 2011 - END
                '
                '                'mti note: multiplier not applied in other infrastructures
                '                'WATWASTE(R%) = WATWASTE(R%) * watwastem(R%)
                '
                '
                '                WATWASTE(R%) = Amin(SANITATION(R%, 3), WATWASTE(R%))
                '
                '                WATWASTE(R%) = AMAX(WATWASTETREAT(R%), WATWASTE(R%))
                '-----------------------------

                sWATWASTE(R%) = WATWASTE(R%)

                'mti July 25, 2012 -moved this - moved back to where it was to avoid run to run
                'sGDPPCP(R%) = GDPPCP(R%)

                'mti February 2011 Infarstructure - end


                '******** All Years but first: Water and Sanitation *********************
                '***************************** end *************************************
                '********'sum infrastructure investment across all infrastructure types (all years but 1): sum for first year is at IFsInfra (since that is called later
                'mti 2022/09/26 start
                If watsafenewsw < 1 Then    'forward/funding linkage from watsan old (3 category)
                    'mti 2022/09/26 End
                    INFRAINVESTNEW(R%, InfraTot) = 0
                    INFRAINVESTMAINT(R%, InfraTot) = 0

                    INFRAINVESTNEWPUB(R%, InfraTot) = 0
                    INFRAINVESTMAINTPUB(R%, InfraTot) = 0

                    For j = 1 To InfraTot - 1
                        INFRAINVESTNEW(R%, InfraTot) = INFRAINVESTNEW(R%, InfraTot) + INFRAINVESTNEW(R%, j)
                        INFRAINVESTMAINT(R%, InfraTot) = INFRAINVESTMAINT(R%, InfraTot) + INFRAINVESTMAINT(R%, j)

                        INFRAINVESTNEWPUB(R%, InfraTot) = INFRAINVESTNEWPUB(R%, InfraTot) + INFRAINVESTNEWPUB(R%, j)
                        INFRAINVESTMAINTPUB(R%, InfraTot) = INFRAINVESTMAINTPUB(R%, InfraTot) + INFRAINVESTMAINTPUB(R%, j)
                    Next
                    'mti 2022/09/26 start
                End If 'watsafenewsw < 1 Then
                'mti 2022/09/26 End

                'If RegName(R%) = "Albania" And IY = 2 Then Debug.Print HealthExp, sGDPPCP(R%), SEDYRSAG25(R%), Poverty, RuralPop, WATSAFE(R%, 3)
                'mti 2015/03/04 - start
                For j = 1 To 3
                    WATERFILTERED(R%, j) = AMAX(0, Amin(100, WATERFILTERED(R%, j) * waterfilteredm(R%, j)))
                Next j
                'mti 2015/03/04 -end

            Next 'R%

            'mti 2022/08/17 start
            '    If IY > 1 Then
            '     CalcInfraindices
            '    End If
            'mti August 07, 2012
            'mti 2022/08/17 end
        End Sub

        'jrs 2024/06/27 
        Dim TFNameComputerOwningHH As String, TFNameICTExpendPctGDP As String
        'jrs 2024/06/28
        Dim TFNameInternetConnHH As String
        'jrs 2024/07/04
        Dim TFNameTelephonesPer100 As String
        Public Sub IFsInfra1FirstYear(context As IForecastContext)
            'Error location identification string
            'Dim sErrorSub = "IFsInfra" & CStr(IFUN) ' IFUN

            'Declare Variables
            Dim rVal As Single
            'Call1
            ' If IY% > 1 Then GoTo AllButFirst
            'First Year 1: Variable Declaration (Static variables For which values are saved across calls)
            '--------------------------------------------------------------------------------------------
            'Variables to hold pre-multiplier values (for use in the subsequent year)
            ReDim PreMULTINFRAELECACC(NR%, 3)
            ReDim PreMultLANDIRAREAEQUIP(NR%), PreMultINFRAROAD(NR%), PreMultINFRAROADPAVEDPCNT(NR%), PreMultINFRATELE(NR%)
            ReDim PreMultEnElecShrEnDem(NR%)
            ReDim PreMultINFRAELECTRANLOSS(NR%)

            ReDim INFRAROADRUnConstrained(NR%)
            ReDim INFRAELECGENCAPRUnConstrained(NR%)

            ReDim EnDemDFRI(NR%) 'Initial ratio of Energy demand data and forecast

            's-prefixed aariables usually save previous year values
            ReDim sINFRAROADRAI(NR%), sINFRAROADDEM(NR%)

            ReDim sDesiredINFRAELECACCTotal(NR%)
            ReDim sINFRAELECGENCAP(NR%)
            ReDim sInfraElecGenCapTot(NR%)
            ReDim sinfraroadpavedpcnt(NR%)
            ReDim SICTMobil(NR%)
            ReDim SICTBroad(NR%)
            ReDim SICTBroadMobil(NR%)

            'mti 2014/05/28 - start
            'ReDim SMobil(NR, 2), SBroad(NR, 2), SBroadMobil(NR, 2)
            ReDim SMobil(NR, 3), SBroad(NR, 3), SBroadMobil(NR, 3)

            ReDim sPreBudINFRAROAD(NR%), sPreBudINFRATELE(NR%)

            ReDim sPreBudINFRAELECGENCAP(NR%)
            'mti 2014/05/28 - end

            ReDim slandirareaequip(NR%)
            ReDim SINFRAELEC(NR%), SINFRANET(NR%), SINFRATELEOLD(NR%)
            ReDim SINFRAROAD(NR%)

            ReDim sENSOLFUEL(NR%)

            ReDim SComputers(NR, 2)
            ReDim SGOVREGQUAL(NR)

            'Household size
            ReDim sHHSIZE(NR%)

            'Road access target bound
            ReDim trgtINFRAROADRAI(NR%)

            'i-prefixed aariables usually save initial year values
            ReDim iINFRAROADRAI(NR%)
            ReDim iICTMOBIL(NR%)
            ReDim iICTBroad(NR%)
            ReDim iICTBROADMOBIL(NR%)
            ReDim iINFRAElecACC(NR%, NLocation)

            ReDim iINFRATELE(NR%)

            'save first year only (singl/flt cost on which multiplier is applied
            ReDim iLandIrCost(NR%)
            ReDim iInfraelecadjfact(NR%)
            ReDim iINFRAELECCONSPRODRATIO(NR%)
            ReDim iINFRAELECCON4RATIO(NR%)
            ReDim iINFRAELECPROD4RATIO(NR%)

            ReDim iFossilshare(NR%), iNonFossilshare(NR%)

            ReDim ENSOLFUELI(NR%)

            'shift-suffixed variables hold initial year gap between data and forecast; RI-suffixed variables save the ratio of data and forecast in first year
            ReDim INFRAROADRAIShift(NR%)
            ReDim INFRAELECGenCapShift(NR%)
            ReDim INFRAELECTRANLOSSShift(NR%)
            ReDim INFRARoadPavedPcntShift(NR%)
            ReDim INFRAELECACCShift(NR%, NLocation%)
            ReDim ICTComputerShift(NR)

            'mti 2015/04/29 - start
            ReDim ICTIntNetHHShift(NR%)
            ReDim SICTIntNetHH(NR, 2)
            'mti 2015/04/29 - end

            ReDim MOBILshift(NR), BROADshift(NR)

            ReDim BroadMOBILshift(NR%)


            ReDim ICTIndexShift(NR)
            ReDim SPENDshift(NR)

            ReDim ENSOLFUELShift(NR%)


            ReDim TeleDensSaturation(NR%), InfraTeleGR(NR%), InfraTeleGRMax(NR%)
            ReDim RoadDensShift(NR%), RoadDensRI(NR%), TeleDensRI(NR%), InfraElecRI(NR%), InfraNetRI(NR%)

            ReDim InfraElecRI(NR%)


            'Depreciation rates for various types of infrastructure
            'mti 2022/10/20 start
            'needs to go in IFsInfraWatsan as that is called earlier in Yr 1
            'ReDim Deppcnt(NInfraType%) As Double
            'mti 2022/10/20 End


            'gr- or R-suffixed variables are usually growth rates
            ReDim InfraElecTranLossGR(NR%)
            ReDim ICTMobilGR(NR%), ICTBroadGR(NR%)
            ReDim ICTBroadMobilGR(NR%)
            ReDim sInfraRoadPavedPcntR(NR%)
            ReDim InfraRoadGR(NR%), InfraRoadPavedPcntGR(NR%), InfraElecGenCapGR(NR%)

            ReDim LandIrAreaEquipGR(NR%)

            ReDim SINFRATELE(NR%)

            'SS-prefixed variables are usually values from two years back
            ReDim SSINFRATELE(NR%)

            'This is legacy code: Old telephone model (talk to BBH to see if this can be deleted)
            ReDim INFRATELEMin(NR%)

            'save independent variables in this module to avoid run to run from call sequence mix-up (revisit this)
            ReDim sgdpp(NR%), sLANDAREA(NR%), sGDPPCP(NR%)

            ReDim sInfraBudImpact(NR%)

            'mti 2022/09/30 start
            'parameter to save budget impact when the other one (sInfraBudImpact) is used as a flag (-9999); this is overall - all infra - budget impact though, not just WatSan
            ReDim sInfraBudImpact_WatSanNew(NR%)
            'mti 2022/09/30 end


            ReDim ExtraFundTotal(NR%), TotalDemandFromCandidates(NR%), ExtraFundUsed(NR%)
            ReDim ExtraFundAllocated(NR%, NInfraType%)


            ReDim InfraElecGR(NR%)

            ReDim EnElecShrEnDemGR(NR%), sENELECSHRENDEM(NR%)

            ReDim sINFRAELECACC(NR%, 3), ssINFRAELECACC(NR%, 3), SEnDem(NR%)

            ReDim sGDSInfraInvest(NR%), GDSInfraBox(NR%)

            'extrapolative growth calculation: parameters for positional and functional convergence
            ReDim iextmaposnconvtimeenelecshr(NR%), iextmafuncenelecshr(NR%)


            'BF-suffixed variables holds 'base path' forecast values of physical variables
            ReDim InfraRoadBF(NR%), InfraRoadPavedPcntBF(NR%), INFRAROADRAIBF(NR%)
            ReDim sINFRAROADBF(NR%), sInfraRoadPavedPcntBF(NR%), sInfraRoadRAIBF(NR%)
            ReDim sInfraRoadPavedPcntRBF(NR%)

            ReDim InfraElecGenCapBF(NR%), infraelecaccbf(NR%, NLocation%)
            ReDim sInfraElecGenCapBF(NR%), sINFRAELECACCBF(NR%, NLocation%)
            ReDim InfraElecGenCapGRBF(NR%), sInfraElecGenCapTotBF(NR%)



            ReDim ICTMobilBF(NR%), ICTBroadBF(NR%), ICTBroadMobilBF(NR%)
            ReDim sICTMobilBF(NR%), sICTBroadBF(NR%), sICTBroadMobilBF(NR%)

            ReDim ICTMobilGRBF(NR%), ICTBroadGRBF(NR%), ICTBroadMobilGRBF(NR%)

            ReDim sMobilBF(NR, 2), sBroadBF(NR, 2), SBroadMobilBF(NR, 2)



            ReDim sINFRAROADBF(NR%), sInfraRoadPavedPcntBF(NR%), sInfraRoadRAIBF(NR%)

            ReDim sDesiredINFRAELECACCTotalBF(NR%)


            'mti 2014/05/28 - start
            ReDim EnElecShrEnDemGRBF(NR%), EnElecShrEnDemBF(NR%), sENDEMSH(NR%)

            ReDim INFRAINVESTNEWBF(NR%, NInfraType%), INFRAINVESTMAINTBF(NR%, NInfraType%), iInfraInvestNewPubShr(NR%, 3)

            ReDim InfraRoadGRShift(NR%)
            'mti 2014/05/28 - end

            'sgh 2016/06/20
            ReDim LandIrAreaEquipResid(NR%)
            'ReDim CENVPREC(NR%)
            'sgh 2016/06/20
            'jrs 2019/08/12
            ReDim ENVPREC(NR%)

            'mti 2022/10/20 start
            ReDim sInfraBudImpactByType(NR%, NInfraType%)
            'mti 2022/10/20 end

            'jrs 2024/06/19
            TFNameInternetPctUse = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Internet Percent Use (" & BaseYear & ")"
            TFNameInternetPctUse = getFunctionNameYrOrMstRct(TFNameInternetPctUse, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            'jrs 2024/06/27
            TFNameComputerOwningHH = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Computer Owning Households(" & BaseYear & ") Log"
            TFNameComputerOwningHH = getFunctionNameYrOrMstRct(TFNameComputerOwningHH, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameICTExpendPctGDP = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus ICT Expenditures % GDP (" & CStr(BaseYear) & ") Linear"
            TFNameICTExpendPctGDP = getFunctionNameYrOrMstRct(TFNameICTExpendPctGDP, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            'jrs 2024/06/28
            TFNameInternetConnHH = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Internet Connected Households(" & BaseYear & ") Log"
            TFNameInternetConnHH = getFunctionNameYrOrMstRct(TFNameInternetConnHH, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            'jrs 2024/07/04
            TFNameTelephonesPer100 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Telephones per 100 (" & BaseYear & ") Parabolic"
            TFNameTelephonesPer100 = getFunctionNameYrOrMstRct(TFNameTelephonesPer100, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")

            'First Year Call 1: Reading Historical Growth Rates
            '---------------------------------------------------

            '        sErrorSub = "IFsInfra" & CStr(IFUN) & " - Loading Growth Rates"

            If context.HistoricMode Then
                Call LoadGrowthRatesOneDim("INFRAROADGR", InfraRoadGR, context)
                Call LoadGrowthRatesOneDim("INFRAROADPAVEDPCNTGR", InfraRoadPavedPcntGR, context)
            Else
                Call LoadGrowthRatesOneDim("ICTMOBILGR", ICTMobilGR, context)
                Call LoadGrowthRatesOneDim("ICTBroadGR", ICTBroadGR, context)

                Call LoadGrowthRatesOneDim("ICTBroadmobilGR", ICTBroadMobilGR, context)
                Call LoadGrowthRatesOneDim("INFRAROADGR", InfraRoadGR, context)
                Call LoadGrowthRatesOneDim("INFRAROADPAVEDPCNTGR", InfraRoadPavedPcntGR, context)
                Call LoadGrowthRatesOneDim("INFRAELECGENCAPGR", InfraElecGenCapGR, context)
                Call LoadGrowthRatesOneDim("INFRAELECGR", InfraElecGR, context)

                Call LoadGrowthRatesOneDim("INFRATELEGR", InfraTeleGR, context)
                Call LoadGrowthRatesOneDim("LANDIRAREAEQUIPGR", LandIrAreaEquipGR, context)
                Call LoadGrowthRatesOneDim("INFRAELECTRANLOSSGR", InfraElecTranLossGR, context)
                Call LoadGrowthRatesOneDim("ENELECSHRENDEMGR", EnElecShrEnDemGR, context)
            End If
            'First Year Call 1: ICT Expenditure Shift Factor (done by JRS)
            '------------------------------------------------------------
            'ttt 2019-08-12 This has been moved out from inside of loop
            'SeriesEnvSolidFuelsImproved is used tempararily
            '=> SHOULD change to SeriesEnvPrecipitation (not declared yet)
            'jrs 2019/08/12 
            'Dim valtbl As SeriesEnvSolidFuelsImproved = New SeriesEnvSolidFuelsImproved '->"SeriesEnvPrecipitation"
            'Dim CENVPREC(context.Regionalization.Keys.Count) As Single ' As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?) 'ccount
            'Using db As IFs2Context = New IFs2Context()
            '    'jrs 2018/09/20 to Icount? changed to CCount
            '    Dim CC As Integer = 1
            '    For Each key In context.Regionalization.Keys
            '        'CENVPREC(key) = valtbl.Fields("1999")
            '        CENVPREC(CC) = If(valtbl.EarliestValue, 0) '->1999
            '        CC += 1
            '    Next key
            'End Using
            Dim CENVPREC As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
            For Each item In (From s In context.GetSlice("SeriesEnvPrecipitation", 1999)
                              Select Key = s.Key, Value = s.Value)
                CENVPREC.Add(item.Key, item.Value)
            Next
            Dim CLANDAREA As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
            For Each item In (From s In context.GetSlice("LANDAREA", "MostRecent")
                              Select Key = s.Key, Value = s.Value)
                CLANDAREA.Add(item.Key, If(item.Value, 1))
            Next

            For Each item In (From s In CENVPREC
                              Join r In context.Regionalization On s.Key Equals r.Key
                              Join w In CLANDAREA On s.Key Equals w.Key
                              Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
               ).ToDictionary(
                     Function(grp) grp.RegionId,
                     Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
               )
                ENVPREC(item.Key) = CSng(item.Value)
            Next

            'ttt 2019/10/08 Taken Out
            Array.Copy(ENSOLFUEL, ENSOLFUELI, NR + 1)
            Array.Copy(ENSOLFUEL, sENSOLFUEL, NR + 1)
            Array.Copy(INFRAROADRAI, iINFRAROADRAI, NR + 1)
            Array.Copy(ICTMobilGR, ICTMobilGRBF, NR + 1)
            Array.Copy(ICTBroadGR, ICTBroadGRBF, NR + 1)
            Array.Copy(ICTBroadMobilGR, ICTBroadMobilGRBF, NR + 1)
            Array.Copy(ICTBROADMOBIL, SICTBroadMobil, NR + 1)
            Array.Copy(ICTBROADMOBIL, ICTBROADMOBILDEM, NR + 1)
            Array.Copy(INFRAROAD, SINFRAROAD, NR + 1)
            Array.Copy(INFRAROAD, sPreBudINFRAROAD, NR + 1)
            Array.Copy(INFRAROAD, PreMultINFRAROAD, NR + 1)
            Array.Copy(INFRAROADPAVEDPCNT, PreMultINFRAROADPAVEDPCNT, NR + 1)
            Array.Copy(INFRATELE, PreMultINFRATELE, NR + 1)
            '-
            Array.Copy(ENELECSHRENDEM, PreMultEnElecShrEnDem, NR + 1)
            Array.Copy(LANDIRAREAEQUIP, PreMultLANDIRAREAEQUIP, NR + 1)
            Array.Copy(INFRAELECTRANLOSS, PreMultINFRAELECTRANLOSS, NR + 1)
            Array.Copy(INFRANET, SINFRANET, NR + 1)
            Array.Copy(INFRATELEOLD, SINFRATELEOLD, NR + 1)
            Array.Copy(INFRATELE, SINFRATELE, NR + 1)
            Array.Copy(SINFRATELE, SSINFRATELE, NR + 1)

            Array.Copy(INFRATELE, sPreBudINFRATELE, NR + 1)
            Array.Copy(INFRATELE, iINFRATELE, NR + 1)
            Array.Copy(INFRATELEOLD, INFRATELEMin, NR + 1)
            Array.Copy(INFRAROADPAVEDPCNT, sinfraroadpavedpcnt, NR + 1)
            Array.Copy(InfraRoadPavedPcntGR, sInfraRoadPavedPcntR, NR + 1)
            Array.Copy(sInfraRoadPavedPcntR, sInfraRoadPavedPcntRBF, NR + 1)
            Array.Copy(INFRAELECGENCAP, sINFRAELECGENCAP, NR + 1)
            Array.Copy(INFRAELECGENCAP, sPreBudINFRAELECGENCAP, NR + 1)
            Array.Copy(GDPP, sgdpp, NR + 1)
            Array.Copy(LANDAREA, sLANDAREA, NR + 1)
            Array.Copy(GDPPCP, sGDPPCP, NR + 1)
            Array.Copy(ICTMOBIL, SICTMobil, NR + 1)
            Array.Copy(ICTBROAD, SICTBroad, NR + 1)
            Array.Copy(InfraRoadGR, INFRAROADRUnConstrained, NR + 1)

            Array.Copy(INFRAROAD, INFRAROADDEM, NR + 1)
            Array.Copy(INFRAROADDEM, sINFRAROADDEM, NR + 1)
            Array.Copy(INFRAROADPAVEDPCNT, INFRAROADPAVEDPCNTDEM, NR + 1)
            Array.Copy(INFRAELECGENCAP, INFRAELECGENCAPDEM, NR + 1)
            'good for 2dims etc too
            Array.Copy(SANITATION, SANITATIONDEM, SANITATION.Length)
            Array.Copy(WATSAFE, WATSAFEDEM, WATSAFE.Length)
            '---
            Array.Copy(WATWASTETREAT, WATWASTETREATDEM, NR + 1)
            Array.Copy(LANDIRAREAEQUIP, LANDIRAREAEQUIPDEM, NR + 1)
            Array.Copy(INFRATELE, INFRATELEDEM, NR + 1)
            Array.Copy(ICTMOBIL, ICTMOBILDEM, NR + 1)
            Array.Copy(ICTBROAD, ICTBROADDEM, NR + 1)

            Array.Copy(INFRAROADRAI, sINFRAROADRAI, NR + 1)
            Array.Copy(INFRAROADRAI, INFRAROADRAIDEM, NR + 1)

            Array.Copy(ICTMOBIL, iICTMOBIL, NR + 1)
            Array.Copy(ICTBROAD, iICTBroad, NR + 1)
            Array.Copy(ICTBROADMOBIL, iICTBROADMOBIL, NR + 1)
            Array.Copy(INFRAELECACC, iINFRAElecACC, INFRAELECACC.Length)

            Array.Copy(INFRAROAD, InfraRoadBF, NR + 1)
            Array.Copy(INFRAROADPAVEDPCNT, InfraRoadPavedPcntBF, NR + 1)
            Array.Copy(INFRAROADRAI, INFRAROADRAIBF, NR + 1)
            Array.Copy(INFRAROAD, sINFRAROADBF, NR + 1)
            Array.Copy(INFRAROADPAVEDPCNT, sInfraRoadPavedPcntBF, NR + 1)
            Array.Copy(INFRAROADRAI, sInfraRoadRAIBF, NR + 1)
            Array.Copy(INFRAELECGENCAP, InfraElecGenCapBF, NR + 1)
            Array.Copy(INFRAELECGENCAP, sInfraElecGenCapBF, NR + 1)
            Array.Copy(sInfraElecGenCapTot, sInfraElecGenCapTotBF, NR + 1)
            Array.Copy(INFRAELECACC, infraelecaccbf, INFRAELECACC.Length)
            Array.Copy(INFRAELECACC, sINFRAELECACCBF, INFRAELECACC.Length)
            Array.Copy(EnElecShrEnDemGR, EnElecShrEnDemGRBF, NR + 1)
            Array.Copy(ENELECSHRENDEM, EnElecShrEnDemBF, NR + 1)
            Array.Copy(ICTMOBIL, ICTMobilBF, NR + 1)
            Array.Copy(ICTBROAD, ICTBroadBF, NR + 1)
            Array.Copy(ICTBROADMOBIL, ICTBroadMobilBF, NR + 1)
            Array.Copy(ICTMobilBF, sICTMobilBF, NR + 1)
            Array.Copy(ICTBroadBF, sICTBroadBF, NR + 1)
            Array.Copy(ICTBroadMobilBF, sICTBroadMobilBF, NR + 1)

            Array.Copy(ENDEMSH, sENDEMSH, NR + 1)

            For R% = 1 To NR%
                Dim RegionName = GRPS(1, R%)
                If govregqualm(R) = 0 Then
                    SGOVREGQUAL(R) = GOVREGQUAL(R)
                Else
                    SGOVREGQUAL(R) = GOVREGQUAL(R) / govregqualm(R)
                End If
                'First Year 1: ICT Spending as Pct. of GDP
                '---------------------------------------
                If context.HistoricMode Then
                    TbName$ = "GDP/Capita (PPP 2000) Versus ICT Expenditures % GDP (2005) Linear"
                    Call XYTABL(TbName$, GDPPCP(R), rVal, context)
                Else
                    'jrs 2024/06/27 TbName$ = "GDP/Capita (PPP 2000) Versus ICT Expenditures % GDP (" & CStr(BaseYear) & ") Linear"
                    'TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
                    Call XYTABL(TFNameICTExpendPctGDP, GDPPCP(R), rVal, context)
                End If
                SPENDshift(R%) = ICTSPEND(R%) - rVal
                'applying multiplier (MTI note: Multiplier applied in first year: code by JURS)
                'jrs 2016/05/24 removing multipliers in year 1 ICTSPEND(R%) = AMAX(Amin(ICTSPEND(R%) * ictspendm(R%), 100), 0)


                'First Year Call 1: Access to Electricity Grid: Shift Factors
                '------------------------------------------------------

                'compute percentage of people livig on less than dollar a day for the year before the base year
                sINCOMELT1CS_Pcnt = EstIncomeLt1PcntPrevious(R%, context)

                'Compute shift factor for urban electrifcation
                'predicted value from the statistical model (developed by DSR)
                rVal = CSng(AMAX(1.0F, 100 / (1 + Math.Exp(-(1.14416457907482 - 4.85840701059291 * 0.01 * sINCOMELT1CS_Pcnt + 0.837209311546776 * GOVEFFECT(R%))))))

                'shift factor, i.e., data and forecast difference for the initial year
                INFRAELECACCShift(R%, Urban) = INFRAELECACC(R%, Urban) - rVal

                'Compute shift factor for rural electrifcation
                'predicted value from the statistical model (developed by DSR)
                rVal = CSng(AMAX(1.0F, 100 / (1 + Math.Exp(-(-0.499710126002115 - 6.92532112045222 * 0.01 * sINCOMELT1CS_Pcnt + 0.958215164433785 * GOVEFFECT(R%))))))

                'shift factor, i.e., data and forecast difference for the initial year
                INFRAELECACCShift(R%, Rural) = CSng(INFRAELECACC(R%, Rural) - rVal)

                'Compute total electrification from urban and rural elctrification and population
                INFRAELECACC(R%, Total) = (POPURBAN(R%) * INFRAELECACC(R%, Urban) + (POP(R%) - POPURBAN(R%)) * INFRAELECACC(R%, Rural)) / POP(R%)
                'First Year Call 1: Electricity Transmission Loss - Shift
                '-------------------------------------------------------
                'predicted value from the statistical model (developed by DSR)
                rVal = CSng(Amin(90, AMAX(3, Math.Exp(3.1253418 - 0.124777 * GOVREGQUAL(R%) - 0.025779 * GDPPCP(R%)))))  'value was bound between 3 to 90 in pre-proc

                INFRAELECTRANLOSSShift(R%) = CSng(INFRAELECTRANLOSS(R%) - rVal)

                'First Year Call 1: Solid fuel use: Shift Fcator
                '---------------------------------------------------

                'This part of code was originally done by JRS; modifications were done byt MTI

                'jrs 2013/12/18 *** begin ***
                'predicted value from the statistical model (developed by DSR)
                'jrs 2013/12/18 Call AnalFunc("Percent of People using Primarily Solid Fuels Revised", Array(GDPPCP(R%), INFRAELECACC(R%, Total)), cVal)
                'revised formulation by DSR added POPURBAN
                'DSR 2014/02/12 corrected to use urban percentage not urban population
                Call AnalFunc("Percent of People using Primarily Solid Fuels (Full Model 2010)",
                             New Single() {GDPPCP(R%), INFRAELECACC(R%, Total), 100 * POPURBAN(R%) / POP(R%)},
                              cVal,
              New String() {"GDPPCP", "INFRAELECACC", "PopUrban%"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                'jrs 2015/05/22 *** begin ***
                'To avoid overflow, maximum single number is: 3.40E+38 = e^(88)
                If cVal < -88 Then
                    cVal = -88
                End If
                'jrs 2015/05/22 *** end ***

                'jrs 2013/12/18 ENSOLFUELShift(R%) = ENSOLFUEL(R%) / (100 / (Exp(cVal) + 1))
                ENSOLFUELShift(R%) = ENSOLFUEL(R%) / (100 / (CSng(Math.Exp(-1 * cVal)) + 1))
                'jrs 2013/12/18 *** end ***

                'ttt 2019/10/08 Taken Out
                'Save initial value
                'ENSOLFUELI(R%) = ENSOLFUEL(R%)

                'save value to use in the subsequent year
                'sENSOLFUEL(R%) = ENSOLFUEL(R%)
                'EndzzTaken Out

                'First Year Call 1: Internet Use - Shift
                '-------------------------------------------------------

                'code by JRS (?)/ Function by BBH/JDM (?)

                'jrs 2024/06/19 Call XYTABL("GDP/Capita (PPP) Versus Internet Percent Use (2000)",
                'Amin(40, GDPPCP(R%)), Prediction, context)
                Call XYTABL(TFNameInternetPctUse, Amin(40, GDPPCP(R%)), Prediction, context)
                'Multiplicative shift

                InfraNetRI(R%) = INFRANET(R%) / AMAX(0.001, Prediction)



                'First Year Call 1: Road Network: Shift Factor
                '--------------------------------------------

                'Shift factor needs to be used when growth rate needed for extrapolative approach cannot be estimated from data
                Prediction = ComputeRoadDensity(GDPP(R%), POP(R%), LANDAREA(R%))
                RoadDensShift(R%) = INFRAROAD(R%) - Prediction

                'First Year Call 1: Percentage of Roads that are Paved - Shift Factor
                '---------------------------------------------------------------------

                INFRARoadPavedPcntShift(R%) = INFRAROADPAVEDPCNT(R%) - ComputePavePcnt(GDPPCP(R%), POP(R%), LANDAREA(R%), INFRAROAD(R%)) ' PavedPcnt


                'First Year Call 1: Rural Road Access Index - Shift Factor
                '--------------------------------------------------------

                'ttt 2019/10/08  taken out
                'Save initial value (needed for hard target)
                'iINFRAROADRAI(R%) = INFRAROADRAI(R%)

                IncomeDensityLand = (GDPP(R%) * 1000 / AMAX(LANDAREA(R%), 0.0001F)) '$Intl/ha
                TotalRoadsDensityPop = INFRAROAD(R%) * LANDAREA(R%) * 1000 / AMAX(POP(R%), 0.000001F) ' INFRAROAD(R%) [km/1000 ha] ; LANDAREA(R%) [1000000 ha] ; POP [million ppeople]

                infraroadraicalc = CSng(100 * Math.Exp(-3.55820835391394 + 0.132781679654008 * Math.Log(If(IncomeDensityLand, 0)) + 0.239067182377656 * Math.Log(If(TotalRoadsDensityPop, 0))))

                INFRAROADRAIShift(R%) = INFRAROADRAI(R%) - infraroadraicalc

                'target/bound for RAI is the minimum of halfway towards universal access and target parameter infraroadraitrgtval; unless initial data is higher (which is then st as bound)
                trgtINFRAROADRAI(R%) = Amin(AMAX(infraroadraitrgtval(R%), iINFRAROADRAI(R%)), iINFRAROADRAI(R%) + (100 - iINFRAROADRAI(R%)) / 2)

                'First Year Call 1: Fixed Telephone Lines - Shift Factor
                '--------------------------------------------------------

                '------------- Using old model for MFP impacts (old way): get rid of this? --------
                Call XYTABL("GDP/Capita (PPP) Versus Telephones per 1000 (2000)",
                  Amin(40, GDPPCP(R%)), TeleDens, context)

                'function is per 1000; new variable is per 100
                TeleDensRI(R%) = INFRATELEOLD(R%) * 10 / AMAX(1, TeleDens)

                '-----  new model--------

                'jrs 2024/07/04 Call XYTABL("GDP/Capita (PPP) Versus Telephones per 100 (MOSTRECENT) Parabolic",
                'Amin(70, GDPPCP(R%)), TeleDens, context)
                Call XYTABL(TFNameTelephonesPer100, Amin(70, GDPPCP(R%)), TeleDens, context)

                'First Year Call 1: Mobile phone  - Shift Factor
                '--------------------------------------------------
                Call AnalFunc("ICT Mobile Access % of Population Rev 2010", New Single() {GDPPCP(R), GOVREGQUAL(R)}, mobilVal,
                New String() {"GDP2005PCPPP(MOSTRECENT)/ 1000 (Log)", "GovernanceRegQual(MOSTRECENT)+ 2.5 (Linear)"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                MOBILshift(R) = ICTMOBIL(R) - mobilVal

                '--These are needed for saving levels for tech shift implementation (coded by JRS)
                SMobil(R, 1) = ICTMOBIL(R)
                SMobil(R, 2) = ICTMOBIL(R)

                '---- needed for saving base forecast -------------
                sMobilBF(R, 1) = ICTMOBIL(R)
                sMobilBF(R, 2) = ICTMOBIL(R)
                'ttt 2019/10/08  Taken out
                'ICTMobilGRBF(R%) = ICTMobilGR(R%)

                'First Year Call 1: Fixed Broadband
                '----------------------------------
                Call AnalFunc("ICT Broadband Access % of Population Rev 2010",
                New Single() {GDPPCP(R), GOVREGQUAL(R)}, BroadVal,
               New String() {"GDP2005PCPPP(MOSTRECENT)/ 1000 (Log)", "GovernanceRegQual(MOSTRECENT)+ 2.5 (Linear)"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                BROADshift(R) = ICTBROAD(R) - BroadVal

                'Note MTI: this came from JRS: multiplier should not be at first year: JRS used it for expected BB (needed for renewable energy impacts of ICT)
                'applying multiplier
                'jrs 2016/05/24 removing multipliers in year 1 ICTBROAD(R%) = AMAX(Amin(ICTBROAD(R%) * ictbroadm(R%), 100), 0) '??

                'jrs 2009/05/12
                SBroad(R, 1) = ICTBROAD(R)
                SBroad(R, 2) = ICTBROAD(R)

                'mti 2014/05/28 - start
                SBroad(R, 3) = ICTBROAD(R)
                'mti 2014/05/28 - end

                '---- needed for base forecast -------------
                sBroadBF(R, 1) = ICTBROAD(R)
                sBroadBF(R, 2) = ICTBROAD(R)
                'ttt 2019/10/08  Taken out
                'ICTBroadGRBF(R%) = ICTBroadGR(R%)

                'First Year Call 1: Mobile Broadband
                '----------------------------------
                Call AnalFunc("ICT Broadband Mobile Access % of Population Rev 2010",
                           New Single() {GDPPCP(R%), GOVREGQUAL(R%)}, BroadMobilVal,
                          New String() {"GDP2005PCPPP(MOSTRECENT)/ 1000 (Log)", "GovernanceRegQual(MOSTRECENT)+ 2.5 (Linear)"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                BroadMOBILshift(R) = ICTBROADMOBIL(R) - BroadMobilVal

                SBroadMobil(R, 1) = ICTBROADMOBIL(R)

                SBroadMobil(R, 2) = ICTBROADMOBIL(R)
                'ttt 2019/10/08 Taken out
                'ICTBroadMobilGRBF(R%) = ICTBroadMobilGR(R%)

                '---- needed for base forecast -------------
                SBroadMobilBF(R, 1) = ICTBROADMOBIL(R)
                SBroadMobilBF(R, 2) = ICTBROADMOBIL(R)

                'ttt 2019/10/08 Taken out
                'SICTBroadMobil(R) = ICTBROADMOBIL(R)
                'ICTBROADMOBILDEM(R) = ICTBROADMOBIL(R)

                'First Year Call 1: Infrastructure Lifetime and Depreciation Rates
                '------------------------------------------------------------------
                Deppcnt(RoadPaved) = CSng(Math.Exp(Math.Log(2) / infraroadpavedlife) - 1)
                Deppcnt(RoadUnpaved) = CSng(Math.Exp(Math.Log(2) / infraroadunpavedlife) - 1)

                Deppcnt(Irrigation) = CSng(Math.Exp(Math.Log(2) / landirlife) - 1)
                Deppcnt(Telephone) = CSng(Math.Exp(Math.Log(2) / infratelelife) - 1)

                Deppcnt(Mobile) = CSng(Math.Exp(Math.Log(2) / ictmobillife) - 1)

                Deppcnt(Broadband) = CSng(Math.Exp(Math.Log(2) / ictbroadlife) - 1)

                Deppcnt(BroadbandMobile) = CSng(Math.Exp(Math.Log(2) / ictbroadmobillife) - 1)

                Deppcnt(ElectricityGen) = CSng(Math.Exp(Math.Log(2) / infraelecgenlife) - 1)

                Deppcnt(ElectricityAccRural) = CSng(Math.Exp(Math.Log(2) / infraelecaccrurallife) - 1)
                Deppcnt(ElectricityAccUrban) = CSng(Math.Exp(Math.Log(2) / infraelecaccurbanlife) - 1)

                'First Year Call 1: Save values for use in the subsequent year
                '----------------------------------------------------------------
                'ttt 2019/10/08 Taken Out
                'SINFRAROAD(R%) = INFRAROAD(R%)

                'mti 2014/05/28 - start
                'ttt 2019/10/08 Taken Out
                'sPreBudINFRAROAD(R%) = INFRAROAD(R%)
                'mti 2014/05/28 - end

                INFRAROADTOTAL(R%) = LANDAREA(R%) * INFRAROAD(R%) * 1000  'km
                'ttt 2019/10/08 Taken Out
                'PreMultINFRAROAD(R%) = INFRAROAD(R%)
                'PreMultINFRAROADPAVEDPCNT(R%) = INFRAROADPAVEDPCNT(R%)
                'PreMultINFRATELE(R%) = INFRATELE(R%)
                'PreMultEnElecShrEnDem(R%) = ENELECSHRENDEM(R%)
                'PreMultLANDIRAREAEQUIP(R%) = LANDIRAREAEQUIP(R%)
                'PreMultINFRAELECTRANLOSS(R%) = INFRAELECTRANLOSS(R%)

                'SINFRANET(R%) = INFRANET(R%)

                'SINFRATELEOLD(R%) = INFRATELEOLD(R%)
                'SINFRATELE(R%) = INFRATELE(R%)
                'SSINFRATELE(R%) = SINFRATELE(R%)

                ''mti 2014/05/28 - start
                'sPreBudINFRATELE(R%) = INFRATELE(R%)
                ''mti 2014/05/28 - end

                If Not InfraTeleGR(R%).HasValue Then
                    InfraTeleGR(R%) = Amin(0.05F, igdpr(R%) * 0.5F)
                End If

                'ttt 2019/10/08 taken out
                'iINFRATELE(R%) = INFRATELE(R%)

                'INFRATELEMin(R%) = INFRATELEOLD(R%)

                'sinfraroadpavedpcnt(R%) = INFRAROADPAVEDPCNT(R%)
                'sInfraRoadPavedPcntR(R%) = InfraRoadPavedPcntGR(R%)
                'sInfraRoadPavedPcntRBF(R%) = sInfraRoadPavedPcntR(R%)

                'sINFRAELECGENCAP(R%) = INFRAELECGENCAP(R%)

                ''mti 2014/05/28 - start
                'sPreBudINFRAELECGENCAP(R%) = INFRAELECGENCAP(R%)
                'mti 2014/05/28 - end

                sInfraElecGenCapTot(R%) = INFRAELECGENCAP(R%) * POP(R%)
                'jrs 2018/12/21 can't do changes to LANDIRAREAEQUIP in First Year, causes Run To Run, send an alert instead *** begin ***
                'sgh 2018/02/15 - start
                'Can't initialize land equipped for irrigation higher than saturation
                If LANDIRAREAEQUIP(R%) > landirareasat(R%) Then
                    'LANDIRAREAEQUIP(R%) = LANDIRAREASAT(R%)
                    'slandirareaequip(R%) = LANDIRAREAEQUIP(R%)
                    Debug.WriteLine("Need to fix LANDIRAREAEQUIP > LANDIRAREASAT in Preprocessor for " & GRPS(1, R%) & "Infrastructure Module") ', vbCritical, "Infrastructure Module")
                    'MsgBox()
                End If
                'jrs 2018/12/21 can't do changes to LANDIRAREAEQUIP in First Year, causes Run To Run, send an alert instead *** end ***
                slandirareaequip(R%) = LANDIRAREAEQUIP(R%)
                'sgh 2018/02/15 - end

                iLandIrCost(R%) = LANDIRCOST(R%)
                TeleDensSaturation(R%) = -1
                InfraTeleGRMax(R%) = -100
                'ttt 2019/10/08 Taken Out
                'sgdpp(R%) = GDPP(R%)
                'sLANDAREA(R%) = LANDAREA(R%)
                'sGDPPCP(R%) = GDPPCP(R%)

                'SICTMobil(R%) = ICTMOBIL(R%)
                'SICTBroad(R%) = ICTBROAD(R%)

                'INFRAROADRUnConstrained(R%) = InfraRoadGR(R%)

                'sum investment across all infrastructure types (year 1): sum for latter year is at IFsInfraWatsan (since that is called later)
                INFRAINVESTNEW(R%, InfraTot) = 0
                INFRAINVESTMAINT(R%, InfraTot) = 0

                INFRAINVESTNEWPUB(R%, InfraTot) = 0
                INFRAINVESTMAINTPUB(R%, InfraTot) = 0


                For j = 1 To NInfraType%
                    INFRAINVESTNEW(R%, InfraTot) = INFRAINVESTNEW(R%, InfraTot) + INFRAINVESTNEW(R%, j)
                    INFRAINVESTMAINT(R%, InfraTot) = INFRAINVESTMAINT(R%, InfraTot) + INFRAINVESTMAINT(R%, j)

                    INFRAINVESTNEWPUB(R%, j) = INFRAINVESTNEW(R%, j) * infrainvnewpubshrm(R%, j)
                    INFRAINVESTMAINTPUB(R%, j) = INFRAINVESTMAINT(R%, j) * infrainvmaintpubshrm(R%, j)

                    INFRAINVESTNEWPUB(R%, InfraTot) = INFRAINVESTNEWPUB(R%, InfraTot) + INFRAINVESTNEWPUB(R%, j)
                    INFRAINVESTMAINTPUB(R%, InfraTot) = INFRAINVESTMAINTPUB(R%, InfraTot) + INFRAINVESTMAINTPUB(R%, j)
                Next

                sInfraBudImpact(R%) = 1

                'mti 2022/09/30 start
                sInfraBudImpact_WatSanNew(R%) = 1
                'mti 2022/09/30 end

                sHHSIZE(R%) = HHSIZE(R%)

                'First Year Call 1: Save values for use in the subsequent year
                '----------------------------------------------------------------

                'mti March 2011 -start
                'For the first year - initialize desired demand to same as supply for all infrastructure including WatSan
                'ttt 2019/10/08 Taken Out
                'INFRAROADDEM(R%) = INFRAROAD(R%)
                'sINFRAROADDEM(R%) = INFRAROADDEM(R%)
                'INFRAROADPAVEDPCNTDEM(R%) = INFRAROADPAVEDPCNT(R%)
                'INFRAELECGENCAPDEM(R%) = INFRAELECGENCAP(R%)
                'For j = 1 To 3
                '    SANITATIONDEM(R%, j) = SANITATION(R%, j)
                '    WATSAFEDEM(R%, j) = WATSAFE(R%, j)
                'Next
                'WATWASTETREATDEM(R%) = WATWASTETREAT(R%)
                'LANDIRAREAEQUIPDEM(R%) = LANDIRAREAEQUIP(R%)
                'INFRATELEDEM(R%) = INFRATELE(R%)
                'ICTMOBILDEM(R%) = ICTMOBIL(R%)
                'ICTBROADDEM(R%) = ICTBROAD(R%)

                'For j = 1 To NLocation
                '    INFRAELECACCDEM(R%, j) = INFRAELECACC(R%, j)
                'Next

                'sINFRAROADRAI(R%) = INFRAROADRAI(R%)
                'INFRAROADRAIDEM(R%) = INFRAROADRAI(R%)

                'mti - need to go back one year here - calc growth rate in pre-proc
                sDesiredINFRAELECACCTotal(R%) = INFRAELECACC(R%, Total)

                'ttt 2019/10/08 taken out
                'iICTMOBIL(R%) = ICTMOBIL(R%)
                'iICTBroad(R%) = ICTBROAD(R%)
                'iICTBROADMOBIL(R%) = ICTBROADMOBIL(R%)

                'For j = 1 To NLocation
                '    iINFRAElecACC(R%, j) = INFRAELECACC(R%, j)
                'Next

                '    If RegName(R%) = "China" Then Stop

                sGDSInfraInvest(R%) = GDS(R%, Infra) / GDP(R%)
                GDSInfraBox(R%) = 0

                'Base Forecast values of physical variables (may not be necessary for year 1)
                'ttt 2019/10/08 Taken Out
                'InfraRoadBF(R%) = INFRAROAD(R%)
                'InfraRoadPavedPcntBF(R%) = INFRAROADPAVEDPCNT(R%)
                'INFRAROADRAIBF(R%) = INFRAROADRAI(R%)

                'sINFRAROADBF(R%) = INFRAROAD(R%)
                'sInfraRoadPavedPcntBF(R%) = INFRAROADPAVEDPCNT(R%)
                'sInfraRoadRAIBF(R%) = INFRAROADRAI(R%)


                'InfraElecGenCapBF(R%) = INFRAELECGENCAP(R%)
                'sInfraElecGenCapBF(R%) = INFRAELECGENCAP(R%)


                'sInfraElecGenCapTotBF(R%) = sInfraElecGenCapTot(R%)

                'For j = 1 To NLocation
                '    infraelecaccbf(R%, j) = INFRAELECACC(R%, j)
                '    sINFRAELECACCBF(R%, j) = INFRAELECACC(R%, j)
                'Next

                sDesiredINFRAELECACCTotalBF(R%) = INFRAELECACC(R%, Total)

                'ttt 2019/10/08 Taken Out
                'EnElecShrEnDemGRBF(R%) = EnElecShrEnDemGR(R%)
                'EnElecShrEnDemBF(R%) = ENELECSHRENDEM(R%)

                'ICTMobilBF(R%) = ICTMOBIL(R%)
                ''jrs 2016/05/24 removing multipliers in year 1 *** begin ***
                ''ICTBroadBF(R%) = ICTBROAD(R%) / ictbroadm(R%) 'take back multiplier, if any
                ''ICTBroadMobilBF(R%) = ICTBROADMOBIL(R%) / ictbroadmobilm(R%) 'take back multiplier, if any
                'ICTBroadBF(R%) = ICTBROAD(R%)
                'ICTBroadMobilBF(R%) = ICTBROADMOBIL(R%)
                ''jrs 2016/05/24 removing multipliers in year 1 *** end ***

                'sICTMobilBF(R%) = ICTMobilBF(R%)
                'sICTBroadBF(R%) = ICTBroadBF(R%)
                'sICTBroadMobilBF(R%) = ICTBroadMobilBF(R%)

                'extra infra funds are none at the first year
                ExtraFundTotal(R%) = 0
                TotalDemandFromCandidates(R%) = 0
                ExtraFundUsed(R%) = 0

                'ttt 2019/10/08 Taken Out
                'sENDEMSH(R%) = ENDEMSH(NR%)
                'mti June 24, 2012 - end
                'sgh 2016/09/25 -START
                'Need to calculate a residual in the estimation of land equipped for irrigation (LandIrAreaEquip)
                'Need precipitation for estimation, which is read into Environment.Bas in the first year. But Environment.Bas
                'is after Infra.bas so we read it in here too.

                'LoadCountries dbIFs NO NEED?

                'Load Change in Avg Ann. Temp. by Region
                '---------------------------------------
                '    Dim db As Database
                'Set db = OpenDatabase(IFsHistSeries_MDB, False, True, DataPW$)

                'Set valtbl = db.OpenRecordset("SeriesEnvPrecipitation")
                ' Dim IOMatTbl As Recordset, LaborTbl As Recordset, matrix As Integer, SerRow As Integer, Target As String
                '=>
                'Dim IOMatTbl As IOMatCalc, LaborTbl() As IOLabC, matrix As Integer, SerRow As Integer, labIdx As Integer
                Call XYTABL("LandIrEquipPortionPotential (Linear) versus EnvPrecipitation(MOSTRECENT) (Log)" _
              , ENVPREC(R%), Prediction, context)
                Prediction = Prediction * landirareasat(R%)
                LandIrAreaEquipResid(R%) = slandirareaequip(R%) / Prediction
                LANDIRAREAEQUIP(R%) = slandirareaequip(R%)
            Next

            Array.Copy(INFRAELECACC, INFRAELECACCDEM, INFRAELECACC.Length)

            ' =GoTo AllYears
            SubAllYearsForIFsInfra(context)

            'jrs 2019/11/25
            'jrs 2019/11/26 ReDim ilandirareasat(NR)
            'jrs 2019/11/26 Array.Copy(landirareasat, ilandirareasat, NR + 1)
        End Sub

        Public Sub IFsInfra2FirstYear(context As IForecastContext)
            'call2:  'call sequence 2: has both first year (electricity and infrastructure index) and subseqent years
            'ElecCons = (6.12 / 3.6) * 1000000 * Exp(-4.052943)
            'Use Only IY = 1

            'ttt Add 
            'mti April 26 2011 - start
            ReDim SINFRAINDTRAD(NR%)
            ReDim SINFRAINDICT(NR%)
            'mti April 26 2011 - end

            'mti October 20, 2011 -start
            ReDim SINFRAINDTRADDEM(NR%)
            ReDim SINFRAINDICTDEM(NR%)
            ReDim SSINFRAINDICTDEM(NR%)
            'mti October 20, 2011 - end

            'mti May 2011 - start
            ReDim SSINFRAINDICT(NR%)
            'mti May 2011 - end
            'End ttt Add 
            For R% = 1 To NR%

                Dim Baseextmaposnconvtimeenelecshr As Single = 10
                Dim Baseextmafuncenelecshr As Single = 0.1
                iFossilshare(R%) = (ENP(R%, Oil) + ENP(R%, Gas) + ENP(R%, Coal)) / ENDEMSH(R%)
                iNonFossilshare(R%) = (ENP(R%, Hydro) + ENP(R%, Renew)) / ENDEMSH(R%)
                Prediction = CSng(Math.Round(Math.Exp(-0.021399 + 0.2753483 * Math.Log(GDPPCP(R%)) + 0.4923844 * Math.Log(INFRAELECACC(R%, Total)) - 0.077159 * Math.Log(iFossilshare(R%)) + 0.1227198 * Math.Log(iNonFossilshare(R%))), 6))
                'jrs 2020/08/31 this is false in vb6 and true in .NET, looks like rounding problem
                'If GRPS(1, R) = "Maldives" Then Stop
                If Prediction >= ENELECSHRENDEM(R%) Then
                    'iextmaposnconvtimeenelecshr(R%) = AMAX(8, Int(Baseextmaposnconvtimeenelecshr / (Prediction / ENELECSHRENDEM(R%))))
                    iextmaposnconvtimeenelecshr(R%) = CSng(AMAX(30, Baseextmaposnconvtimeenelecshr + 10 * Math.Floor(CSng(Prediction / ENELECSHRENDEM(R%)))))
                    iextmafuncenelecshr(R%) = Baseextmafuncenelecshr
                Else
                    iextmaposnconvtimeenelecshr(R%) = Baseextmaposnconvtimeenelecshr
                    'iextmafuncenelecshr(R%) = Amin(0.1, AMAX(0.02, Baseextmafuncenelecshr / (ENELECSHRENDEM(R%) / Prediction)))
                    iextmafuncenelecshr(R%) = Amin(0.1F, AMAX(0.09F, Baseextmafuncenelecshr / (ENELECSHRENDEM(R%) / Prediction)))
                End If
                'Initial ratio of ENDEM - data and forecast
                EnDemDFRI(R%) = ENDEMSH(R%) / ENDEM(R%)

                Dim CalcINFRAELEC As Single = ENELECSHRENDEM(R%) * ENDEM(R%) * EnDemDFRI(R%) * 17000 / POP(R%) 'in kwhr ; ENELECSHRENDEM(R%) in 0-100; POP in millions; EnDem in BBOE; 1 BOE = 1700 kwhr

                'If R% = 81 Then Stop

                'jrs 2016/05/11 run to run for Jamaica where there's no data
                '                If INFRAELEC(R%) <= 0 Then 'those with no data
                '                    INFRAELEC(R%) = CalcINFRAELEC
                '                End If
                InfraElecRI(R%) = INFRAELEC(R%) / AMAX(1, CalcINFRAELEC)
                'First Year Call 2: Electricity Generation Capacity Growth Rate
                '----------------------------------------------------------------

                If InfraElecGenCapGR(R%).HasValue Then
                    INFRAELECGENCAPRUnConstrained(R%) = InfraElecGenCapGR(R%)
                Else
                    'four countries have null values for growth rate; replaced by GDP growth rate; Timor-Leste has a 6% GDP growth rate
                    INFRAELECGENCAPRUnConstrained(R%) = Amin(0.03F, GDPR(R%) / 100)
                End If

                InfraElecGenCapGRBF(R%) = INFRAELECGENCAPRUnConstrained(R%)
                'First Year Call 2: Compute Initial Electricity (Production) Adjustment Factor
                '----------------------------------------------------------------------------------
                INFRAELECADJFACT(R%) = INFRAELECPROD(R%) / If((AMAX(INFRAELECGENCAP(R%) * POP(R%) / (1 + INFRAELECGENCAPRUnConstrained(R%)), 0.0001F) * 8766), 0)


                iInfraelecadjfact(R%) = INFRAELECADJFACT(R%)

                'First Year Call 2: Compute Electricity Consumption Production Ratio
                '-------------------------------------------------------------------

                INFRAELECCONSPRODRATIO(R%) = (INFRAELEC(R%) * POP(R%)) / (AMAX(0.0001, INFRAELECPROD(R%)) * AMAX(0.001, (1 - INFRAELECTRANLOSS(R%) / 100)))
                iINFRAELECCONSPRODRATIO(R%) = INFRAELECCONSPRODRATIO(R%)

                'save initial electricity consumption and production values used for calculation initial year ratio
                iINFRAELECCON4RATIO(R%) = INFRAELEC(R%) * POP(R%)
                iINFRAELECPROD4RATIO(R%) = INFRAELECPROD(R%) * AMAX(0.001, (1 - INFRAELECTRANLOSS(R%) / 100))


                'First Year Call 2: Growth Rate for electricity share of energy
                '----------------------------------------------------------------

                If Not EnElecShrEnDemGR(R%).HasValue Then
                    If GDPPCP(R%) > 12000 Then
                        EnElecShrEnDemGR(R%) = 0.01
                    Else
                        EnElecShrEnDemGR(R%) = 0.02
                    End If
                    EnElecShrEnDemGRBF(R%) = EnElecShrEnDemGR(R%)

                End If
                'save energy demand etc for next year
                SEnDem(R%) = ENDEM(R%)

                sENELECSHRENDEM(R%) = ENELECSHRENDEM(R%)

                'jrs 2023/10/25
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("INFRAELEC", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                SINFRAELEC(R%) = INFRAELEC(R%)

                'First Year Call 2: Save electricity access rate values
                '----------------------------------------------------------------

                For loccnt% = 1 To NLocation%
                    sINFRAELECACC(R%, loccnt%) = INFRAELECACC(R%, loccnt%)
                    ssINFRAELECACC(R%, loccnt%) = INFRAELECACC(R%, loccnt%)
                    PreMULTINFRAELECACC(R%, loccnt%) = INFRAELECACC(R%, loccnt%)
                Next

                'First Year Call 2: Households with  Computers Shift Factor - Needs Electricity Use Per Capita
                '-----------------------------------------------------------------------------------

                'mti 2015/04/29 - start
                Dim rVal As Single = 0
                'The following multi-variate function was done with Computer per 100 (NOT with % of hh with computers)
                'ttt Commented Out?
                'Call AnalFunc("ICT Computers Per 100 Person with Electricity",
                'New Single() {INFRAELEC(R), GDPPCP(R), GOVREGQUAL(R)}, rVal,
                '   New String() {"EnElecConsPerCap(MOSTRECENT) (Linear)", "GDP2000PCPPP(MOSTRECENT)/ 1000 (Log)", "GovernanceRegQual(MOSTRECENT) (Linear)"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                'mti 2015/05/06 - start
                'Temporary replcament with a simpe rregression of % of households with computers on GDP PCP (Logistic function)
                'jrs 2024/06/27 TbName$ = "GDP/Capita (PPP 2011) Versus Computer Owning Households(MOSTRECENT) Log"  'function for earlier years yet to be estimated
                'mti 2015/05/06 - end
                Call XYTABL(TFNameComputerOwningHH, GDPPCP(R%), rVal, context)
                'mti 2015/04/29 - end
                ICTComputerShift(R) = ICTCOMPUTERS(R) - rVal
                SComputers(R, 1) = ICTCOMPUTERS(R)
                SComputers(R, 2) = ICTCOMPUTERS(R)

                'mti 2015/04/29 - start
                'First Year Call 2: Households with Internet Shift Factor
                '---------------------------------------------------------
                'mti 2015/05/06 - start
                'jrs 2024/06/28 TbName$ = "GDP/Capita (PPP 2011) Versus Internet Connected Households(MOSTRECENT) Log"
                'mti 2015/05/06 - end
                Call XYTABL(TFNameInternetConnHH, GDPPCP(R%), rVal, context)
                ICTIntNetHHShift(R) = ICTINTNETHH(R) - rVal
                SICTIntNetHH(R, 1) = ICTINTNETHH(R)
                SICTIntNetHH(R, 2) = ICTINTNETHH(R)
                'mti 2015/04/29 - end
            Next
            'sErrorSub = "IFsInfra" & CStr(IFUN)
            'mti August 07, 2012 - start
            'First Year Call 2: Computers per 100 Shift Factor - Needs Electricity Use Per Capita
            '-----------------------------------------------------------------------------------

            'all infra variables for year 1 (including WatSan(call 1) are calculated now - infrastructure indices will be computed now
            CalcInfraindices(context)
            'mti August 07, 2012 -end
            '  If IY > 1 Then => ttt-Moved to IFsInfra2AllYears
        End Sub

        Public Sub IFsInfra2AllbutFirstYear(context As IForecastContext)
            'ttt Extract from Call2 for IY > 1
            Dim INFRAELECBF As Single = 0 'ttt New Made
            PrevsErrorSub = sErrorSub
            Dim InfraRoadPavedDemThousands As Single = 0
            Dim sInfraRoadPavedThousands As Single = 0
            Dim InfraRoadUnPavedDemThousands As Single = 0
            Dim sInfraRoadUnPavedThousands As Single = 0
            For R% = 1 To NR%
                '   All but First Call 2:  Infrastructure Calculation
                ' ---------------------------------------------------


                '   All but First Call 2: Road Density
                '---------------------------------------
                Dim RegionName = context.GetDimension(1).Buckets.Item(CShort(R))
                sErrorSub = PrevsErrorSub + " " + RegionName + " Road Density"
                'current year drivers and forecast

                Prediction = ComputeRoadDensity(GDPP(R%), POP(R%), LANDAREA(R%))

                'If RegName(R%) = "Bangladesh" Then Stop
                'If IY = 2 Then Debug.Print RegName(R%); InfraRoadGR(R%)
                'If IY = 11 And GRPS(1, R) = "Congo, Republic of" Then Stop
                If InfraRoadGR(R%).HasValue Then

                    'Previous year's driver values and prediction obatained from those drivers
                    Dim Prediction1 = ComputeRoadDensity(sgdpp(R%), sPOPTotal(R%), sLANDAREA(R%))

                    'Compute growth rate using extrapolative approach
                    'Attempts to converge current value to expected value using:
                    'a.growth due to the annual change in driver variables, b. growth to cover up positional gap, b. moving average with previous year's growth rate
                    InfraRoadGR(R%) = CSng(CalcExtrapolativeGR(INFRAROADRUnConstrained(R%), CDbl(extmafuncroad), CDbl(extmaposnroad), CDbl(PreMultINFRAROAD(R%)), CDbl(Prediction), CDbl(Prediction1), CDbl(extmaposnconvtimeroad)))

                    'apply growth rate on previous year's (base or pre-target or pre-multiplier) forecast
                    INFRAROAD(R%) = CSng((1 + If(InfraRoadGR(R%), 0)) * PreMultINFRAROAD(R%))

                Else  'Growth rate could not be computed for lack of data: use cross-sectional model
                    If RoadDensShift(R%) > 0 Then 'data value higher than prediction - slower convergence
                        INFRAROAD(R%) = Prediction + ConvergeOverTime(RoadDensShift(R%), 0, mfpconv(R%) * 6) 'mfpconv = 50 years
                    Else
                        INFRAROAD(R%) = Prediction + ConvergeOverTime(RoadDensShift(R%), 0, 40) 'mfpconv = 50 years
                    End If
                End If
                'mti July 18, 2011 - start
                'cap annual increments to prevent transient(in the second year in low density high shift factor countries) like UAE, Sudan, Mongolia etc
                INFRAROAD(R%) = Amin(PreMultINFRAROAD(R%) * 1.25F, INFRAROAD(R%)) 'downward fall is already prevented

                'cap road density to a value of 100 to prevent countries like Malta or Bahrain going up very high
                'MTI note: this cap is not yet imposed after the handling of extra finance
                INFRAROAD(R%) = Amin(100, INFRAROAD(R%))


                '(Base) or pre-target pre-miltiplier forecast values of physical variables
                'needed for estimating base fund demands that are protected in an upward target scenario
                'so that no single physical infrastructure is hurt from an ambitious target
                sINFRAROADBF(R%) = InfraRoadBF(R%)
                InfraRoadBF(R%) = INFRAROAD(R%)


                'recalculate growth rate for shift approach (or in case road density was capped above)
                InfraRoadGR(R%) = (INFRAROAD(R%) - PreMultINFRAROAD(R%)) / PreMultINFRAROAD(R%)



                INFRAROADRUnConstrained(R%) = InfraRoadGR(R%)  'save growth rate (base or pre-multiplier pre-target)

                'save base or pre-multiplier pre-target value for next year (to avoid cumulative effects from multipliers or targets)
                PreMultINFRAROAD(R%) = INFRAROAD(R%)


                'apply base 1 mutliplier on total road if any: PPHP4 did not use this:
                'Note MTI: DSR wanted no cap here: make sure this is followed up in later calculations
                INFRAROAD(R%) = INFRAROAD(R%) * infraroadm(R%)
                'mti 2014/05/28 - start

                'In case curve has shifted up, it should stay there
                Dim InfraRoadShiftPrevYear = CSng(AMAX(0, (SINFRAROAD(R%) - sPreBudINFRAROAD(R%))))

                '        If RegName(R%) = "Saudi Arabia" And IY >= 6 Then Stop

                sPreBudINFRAROAD(R%) = INFRAROAD(R%)

                INFRAROAD(R%) = INFRAROAD(R%) + InfraRoadShiftPrevYear

                'mti 2014/05/28 - end


                'mti June 04, 2012 -start
                'already built roads should not go down unless there is a multipliertarget to take them down
                'dkb 2013/09/16 added dimension 1 to infraroadraisetar
                If infraroadm(R%) >= 1 And infraroadraisetar(R%) >= 0 Then
                    INFRAROAD(R%) = AMAX(SINFRAROAD(R%), INFRAROAD(R%))
                End If
                'Note MTI: make sure this is not messing up RAI in scenarios
                '   All but First Call 2: Percentage of Roads That are Paved
                '------------------------------------------------------------
                sErrorSub = PrevsErrorSub + " " + RegionName + " Road Percentage Paved"
                Dim IncTerm As Double = 0
                Dim PopTerm As Double = 0
                Dim LandTerm As Double = 0
                'Current year forecast
                Prediction = CSng(ComputePavePcnt(GDPPCP(R%), POP(R%), LANDAREA(R%), INFRAROAD(R%)))
                'Current year forecast: base or pre-target, pre-multiplier (suffixed with BF)
                Dim PredictionBF = ComputePavePcnt(GDPPCP(R%), POP(R%), LANDAREA(R%), InfraRoadBF(R%))

                sInfraRoadPavedPcntBF(R%) = InfraRoadPavedPcntBF(R%)
                Dim InfraRoadPavedPcntR As Single = 0
                Dim InfraRoadPavedPcntRBF As Single = 0
                If InfraRoadPavedPcntGR(R%).HasValue Then

                    Dim Prediction1 = CSng(ComputePavePcnt(sGDPPCP(R%), sPOPTotal(R%), LANDAREA(R%), SINFRAROAD(R%)))
                    Dim Prediction1BF = ComputePavePcnt(sGDPPCP(R%), sPOPTotal(R%), LANDAREA(R%), sINFRAROADBF(R%))

                    'extrapolative approach
                    InfraRoadPavedPcntR = CSng(CalcExtrapolativeGR(sInfraRoadPavedPcntR(R%), extmafuncroadpaved, extmaposnroadpaved, CDbl(PreMultINFRAROADPAVEDPCNT(R%)), CDbl(Prediction), CDbl(Prediction1), extmaposnconvtimeroadpaved))
                    InfraRoadPavedPcntRBF = CSng(CalcExtrapolativeGR(sInfraRoadPavedPcntRBF(R%), extmafuncroadpaved, extmaposnroadpaved, CDbl(sInfraRoadPavedPcntBF(R%)), CDbl(PredictionBF), CDbl(Prediction1BF), extmaposnconvtimeroadpaved))
                    INFRAROADPAVEDPCNT(R%) = CSng((1 + InfraRoadPavedPcntR) * PreMultINFRAROADPAVEDPCNT(R%))
                    InfraRoadPavedPcntBF(R%) = (1 + InfraRoadPavedPcntRBF) * InfraRoadPavedPcntBF(R%)

                Else
                    'growth rate could not be obtained from data: use cross-sectional approach
                    If INFRARoadPavedPcntShift(R%) > 0 Then
                        'historical value higher than forecast at the initial year - keep higher by imposing a very slow shift convergence
                        INFRAROADPAVEDPCNT(R%) = Prediction + ConvergeOverTime(INFRARoadPavedPcntShift(R%), 0, 500)
                        InfraRoadPavedPcntBF(R%) = PredictionBF + ConvergeOverTime(INFRARoadPavedPcntShift(R%), 0, 500)
                    Else
                        'historical value lower than forecast at the initial year - bring up
                        INFRAROADPAVEDPCNT(R%) = Prediction + ConvergeOverTime(INFRARoadPavedPcntShift(R%), 0, 50)
                        InfraRoadPavedPcntBF(R%) = PredictionBF + ConvergeOverTime(INFRARoadPavedPcntShift(R%), 0, 50)
                    End If

                End If
                INFRAROADPAVEDPCNT(R%) = Amin(100, AMAX(1, INFRAROADPAVEDPCNT(R%)))
                InfraRoadPavedPcntBF(R%) = Amin(100, AMAX(1, InfraRoadPavedPcntBF(R%)))


                're/calculate growth rate for shift approach or  in case it was capped - check if it creates a problem in moving average
                InfraRoadPavedPcntR = CSng((INFRAROADPAVEDPCNT(R%) - PreMultINFRAROADPAVEDPCNT(R%)) / PreMultINFRAROADPAVEDPCNT(R%))
                InfraRoadPavedPcntRBF = (InfraRoadPavedPcntBF(R%) - sInfraRoadPavedPcntBF(R%)) / sInfraRoadPavedPcntBF(R%)

                'Unconstrained growth rate in paved pcnt
                sInfraRoadPavedPcntR(R%) = InfraRoadPavedPcntR
                sInfraRoadPavedPcntRBF(R%) = InfraRoadPavedPcntRBF

                PreMultINFRAROADPAVEDPCNT(R%) = INFRAROADPAVEDPCNT(R%)

                'Apply multiplier (base 1): this type of multiplier was not used for PPHP4
                INFRAROADPAVEDPCNT(R%) = Amin(100, INFRAROADPAVEDPCNT(R%) * infraroadpavedpcntm(R%))

                'already built roads should not go down unless there is a hard multiplier infraroadm
                If infraroadpavedpcntm(R%) >= 1 Then
                    InfraRoadPavedDemThousands = LANDAREA(R%) * INFRAROAD(R%) * INFRAROADPAVEDPCNT(R%) / 100 'Demanded Connections (stock) in Thousands This Year =(Lines/100)x(Pop x 10000 x 100)/(1000)
                    sInfraRoadPavedThousands = LANDAREA(R%) * SINFRAROAD(R%) * sinfraroadpavedpcnt(R%) / 100 'Connections (Stock) in Thousands Last Year

                    InfraRoadPavedDemThousands = AMAX(InfraRoadPavedDemThousands, sInfraRoadPavedThousands)

                    INFRAROADPAVEDPCNT(R%) = CSng(Amin(100, Math.Round(100 * InfraRoadPavedDemThousands / LANDAREA(R%) / AMAX(0.00001, INFRAROAD(R%)), 6)))

                End If
                'note from MTI: make sure this is not messing up RAI in scenarios
                'mti June 04, 2012 - end
                '   All but First Call 2: Rural Road Access Index- Desired
                '---------------------------------------------------------

                '                sErrorSub = saveErrorSub + " " + RegName(R%) + " Rural Road Index"

                'Income per land area
                IncomeDensityLand = (sgdpp(R%) * 1000 / AMAX(LANDAREA(R%), 0.0001F)) 'unit: $/ha
                'Road per population
                TotalRoadsDensityPop = INFRAROAD(R%) * LANDAREA(R%) * 1000 / AMAX(sPOPTotal(R%), 0.000001F) ' INFRAROAD(R%) [km/1000 ha] ; LANDAREA(R%) [1000000 ha] ; POP [million ppeople]

                'forecast from function
                infraroadraicalc = CSng(100 * Math.Exp(-3.55820835391394 + 0.132781679654008 * Math.Log(If(IncomeDensityLand, 0)) + 0.239067182377656 * Math.Log(If(TotalRoadsDensityPop, 0))))

                'Cross-sectional model: add initial shift converged to 0 over time
                Dim shiftadded As Single = 0
                If INFRAROADRAIShift(R%) > 0 Then
                    'slow convergence of shift factors for countries historically above the forecast value
                    shiftadded = ConvergeOverTime(INFRAROADRAIShift(R%), 0, 500)
                    INFRAROADRAI(R%) = infraroadraicalc + shiftadded
                Else
                    shiftadded = ConvergeOverTime(INFRAROADRAIShift(R%), 0, 100)
                    INFRAROADRAI(R%) = infraroadraicalc + shiftadded
                End If
                'Impose upper bound: Note MTI: this should be just 100 - will change later
                Dim RBound = AMAX(100, sINFRAROADRAI(R%))
                INFRAROADRAI(R%) = AMAX(0.001, Amin(RBound, INFRAROADRAI(R%)))

                'save base (or pre-target, pre-multiplier) forecast
                sInfraRoadRAIBF(R%) = INFRAROADRAIBF(R%)
                INFRAROADRAIBF(R%) = INFRAROADRAI(R%)

                'save desired RAI (not affected by fund)
                INFRAROADRAIDEM(R%) = INFRAROADRAI(R%)
                '   All but First Call 2: Rural Road Access Index- SE target
                '-----------------------------------------------------------

                'Standard Error (SE) target on road access index: (reverse) compute roads and paved% needed to reach RAI target
                'dkb 2013/09/16 added dimension 1 to infraroadraisetar
                If infraroadraisetar(R%) <> 0 And infraroadraitrgtyr(R%) = 0 Then 'hard target and SE target cannot go together
                    'Note MTI: 0 SE scenarios need to be done with a small SE: like .001 SE
                    'Standard error of the RAI estimation by DSR
                    Dim roadRAISE = 17
                    Dim infraroadraicalcPreTarget = infraroadraicalc

                    'Note MTI: In universal target we recalculate shift at this point to adjust for various bounds on RAI: shouldn't we do it here?

                    'compute growth needed to reach SE target using JRS/BBH function
                    'dkb 2013/09/16 added dimension 1 to infraroadraisetar and infraroadraiseyrtar
                    Dim EffectSE = StdErrorAdditiveShift(CSng(roadRAISE), CSng(infraroadraicalc), CSng(shiftadded), CSng(INFRAROADRAIShift(R%)),
                        CSng(infraroadraisetar(R%)), CInt(FirstYear + infraroadraiseyrtar(R%)), FirstYear + IY - CShort(1), FirstYear, trgtINFRAROADRAI(R%))

                    'subtract the shift factor added to (base) RAI forecast estimated from prediction function
                    'This approach means no road building is needed for shift convergence
                    infraroadraicalc = INFRAROADRAI(R%) - shiftadded

                    'bound RAI at a value little above 100 so that road requirement continues to grow especially for negative shift countries
                    'DSR's log-log RAI model can accomodate RAI values above 100
                    Dim RUBound = AMAX(120, iINFRAROADRAI(R%)) 'Note MTI: drop iINFRAROADRAI(R%): it is always below 120

                    'add RAI growth required by SE targeting with appropriate bounding (bounds are different for negative shift)
                    infraroadraicalc = AMAX(0.001, Amin(RUBound - shiftadded, Amin(RUBound, infraroadraicalc + EffectSE))) ' could be high for countries with negative initial shift; e.g., Brunei

                    'save desired RAI (not affected by fund) 'kept the bound at more than 100 for debugging help
                    INFRAROADRAI(R%) = AMAX(0.001, Amin(120, infraroadraicalc + shiftadded))

                    'Reverse calculation
                    'dkb 2013/09/16 added dimension 1 to infraroadraisetar
                    If infraroadraisetar(R%) > 0 Then
                        'for a postive SE target RAI cannot go below base forecast; and cannot go below last year other than through lack of road maintenance
                        If infraroadraicalcPreTarget < infraroadraicalc Then
                            'reverse estimation of roads per population for the targeted RAI value
                            Dim TotalRoadsDensityPopTrgt = CSng(Math.Exp((Math.Log(infraroadraicalc) - 1.04696183207415 - 0.132781679654008 * Math.Log(If(IncomeDensityLand, 0))) / 0.239067182377656))
                            'revised total road/road density per target
                            INFRAROAD(R%) = TotalRoadsDensityPopTrgt * sPOPTotal(R%) / AMAX(LANDAREA(R%), 0.0001F) / 1000

                        End If

                        'dkb 2013/09/16 added dimension 1 to infraroadraisetar
                    ElseIf infraroadraisetar(R%) < 0 Then
                        'negative SE; targeted RAI can go below base forecast; cannot go below last year (other than through lack of maintenance)
                        Dim TotalRoadsDensityPopTrgt = CSng(Math.Exp((Math.Log(infraroadraicalc) - 1.04696183207415 - 0.132781679654008 * Math.Log(If(IncomeDensityLand, 0))) / 0.239067182377656))
                        INFRAROAD(R%) = TotalRoadsDensityPopTrgt * sPOPTotal(R%) / AMAX(LANDAREA(R%), 0.0001F) / 1000
                    End If

                    'roads cannot go below last year (other than through lack of maintenance)
                    INFRAROAD(R%) = AMAX(INFRAROAD(R%), SINFRAROAD(R%))


                    '                Else 'Note MTI: this branch will not be needed any more (commented on 09 August 2012)
                    '
                    '                                If infraroadraisetar > 0 Then 'roads should not go down for positive SE
                    '                                    INFRAROAD(R%) = AMAX(INFRAROAD(R%), SINFRAROAD(R%))
                    '                                End If

                End If
                '   All but First Call 2: Rural Road Access Index- Universal target
                '------------------------------------------------------------------

                'dkb 2013/09/16 added dimension 1 to infraroadraisetar
                If infraroadraitrgtyr(R%) > 0 And infraroadraisetar(R%) = 0 Then 'cannot do universal target and SE target together
                    'RAI  required to reach universal target
                    'target value is capped (above) at trgtINFRAROADRAI(R%), which is the maximum between a. initial value and b. the minimum of a target value parameter and the value halfway to universal from the initial value
                    Dim INFRAROADRAITrgt = Amin(trgtINFRAROADRAI(R%), iINFRAROADRAI(R%) + Amin(infraroadraitrgtyr(R%), IY - 1) * (trgtINFRAROADRAI(R%) - iINFRAROADRAI(R%)) / infraroadraitrgtyr(R%))

                    'If target RAI is higher than base RAI estimate more roads will be needed
                    If INFRAROADRAI(R%) < INFRAROADRAITrgt Then

                        'Shift factor that was added to RAI base estimate: need recalculation here in case final RAI estimate was adjusted by bounds
                        shiftadded = INFRAROADRAI(R%) - infraroadraicalc

                        'new bound : per new hard target algorithm
                        Dim UpBound = 130 '100 '500 '120 '100 'mti: there might be an issue here;
                        'Note MTI: if initial (pre-target) INFRAROADRAICalc was >100 (it was not bound), back calculation might result in a decrease in roads density
                        'this can be prevented by bounding back calculated INFRAROAD with initial forecast of INFRAROAD

                        'estimated RAI to be used for reverse calculation of roads
                        infraroadraicalc = AMAX(0.001, Amin(UpBound, INFRAROADRAITrgt - shiftadded))  'mti negative shift countries will be bound at UpBound


                        'save desired RAI (not affected by fund) 'kept the bound at more than 100 for debugging help
                        INFRAROADRAI(R%) = AMAX(0.001, Amin(UpBound, infraroadraicalc + shiftadded))

                        'Reverse calculation of road requirement
                        Dim TotalRoadsDensityPopTrgt = CSng(Math.Exp((Math.Log(infraroadraicalc) - 1.04696183207415 - 0.132781679654008 * Math.Log(If(IncomeDensityLand, 0))) / 0.239067182377656))
                        INFRAROAD(R%) = TotalRoadsDensityPopTrgt * sPOPTotal(R%) / AMAX(LANDAREA(R%), 0.0001F) / 1000

                        'roads should not go down while chasing universal RAI target
                        INFRAROAD(R%) = AMAX(INFRAROAD(R%), sINFRAROADDEM(R%))

                    Else

                        'Note MTI: if traget RAI is less than or equal to base estimate: nothing needs to be done
                        'MTI commented the following line on 09 August 2012
                        INFRAROAD(R%) = AMAX(INFRAROAD(R%), sINFRAROADDEM(R%))

                    End If

                End If
                'capping road density even with target
                'mti July 25, 2012 -end - Roadcap
                INFRAROAD(R%) = Amin(100, INFRAROAD(R%))
                'mti July 25, 2012 -end - Roadcap
                '   All but First Call 2: Rural Road Access Index targeting: Revise paved percentage as a result of road change
                '--------------------------------------------------------------------------------------------------------------

                'dkb 2013/09/16 added dimension 1 to infraroadraisetar
                If infraroadraitrgtyr(R%) > 0 Or infraroadraisetar(R%) <> 0 Then

                    'Note MTI: August 09, 2012: the following (4 lines of code) code does not work as driver values remain empty (no net effect on paved pcnt change from targets)
                    'we however kept it as it is because the paved % behaves badly with the (seemingly) right code commented below
                    'post PPHP4 we need to come back on this and fix this


                    'reusing incterm; popterm and landterm from above etc
                    'formula: % of RoadsPaved = 100 * 1/(1 + e^1.0219486 * GDPperCapita^-0.832949 * Population^-0.756058 * LandArea^0.7260397 * TotalRoadDensity^0.2669518)
                    Dim RoadDensTerm = InfraRoadBF(R%) ^ 0.2669518
                    PredictionBF = CSng(100 / (1 + 2.77860387967124 * IncTerm * PopTerm * LandTerm * RoadDensTerm))
                    RoadDensTerm = INFRAROAD(R%) ^ 0.2669518
                    Dim PredictionTF = CSng(100 / (1 + 2.77860387967124 * IncTerm * PopTerm * LandTerm * RoadDensTerm))

                    'PredictionBF = ComputePavePcnt(GDPPCP(R%), POP(R%), LANDAREA(R%), InfraRoadBF(R%))
                    'PredictionTF = ComputePavePcnt(GDPPCP(R%), POP(R%), LANDAREA(R%), INFRAROAD(R%)) 'should we use last years accomplished instead

                    INFRAROADPAVEDPCNT(R%) = Amin(100, AMAX(1, INFRAROADPAVEDPCNT(R%) + PredictionTF - PredictionBF))

                End If 'INFRAROADRAI(R%) <> INFRAROADRAIBF(R%) Then
                '   All but First Call 2: Estimate (Desired) Road Costs for Construction and Maintenance
                '---------------------------------------------------------------------------------------

                '                sErrorSub = saveErrorSub + " " + RegName(R%) + " Road Costs"

                '-- Paved Roads------------
                CalcInfraUnitCost(R%, 13, 3, infraroadpavedcostupper, infraroadpavedcostlower, sGDPPCP(R%), INFRAROADPAVEDCOST(R%))
                INFRAROADPAVEDCOST(R%) = INFRAROADPAVEDCOST(R%) * infraroadpavedcostm(R%) 'multiplier on cost
                InfraRoadPavedDemThousands = LANDAREA(R%) * INFRAROAD(R%) * INFRAROADPAVEDPCNT(R%) / 100 'Demanded Connections (stock) in Thousands This Year =(Lines/100)x(Pop x 10000 x 100)/(1000)
                sInfraRoadPavedThousands = LANDAREA(R%) * SINFRAROAD(R%) * sinfraroadpavedpcnt(R%) / 100 'Connections (Stock) in Thousands Last Year
                CalcInfraInvest(CDbl(InfraRoadPavedDemThousands), CDbl(sInfraRoadPavedThousands),
                            Deppcnt(RoadPaved), INFRAROADPAVEDCOST(R%) * 1000, INFRAINVESTNEW(R%, RoadPaved), INFRAINVESTMAINT(R%, RoadPaved))

                '--Unpaved Roads------------
                CalcInfraUnitCost(R%, 13, 3, infraroadunpavedcostupper, infraroadunpavedcostlower, sGDPPCP(R%), INFRAROADUNPAVEDCOST(R%))
                INFRAROADUNPAVEDCOST(R%) = INFRAROADUNPAVEDCOST(R%) * infraroadunpavedcostm(R%) 'multiplier on cost
                InfraRoadUnPavedDemThousands = LANDAREA(R%) * INFRAROAD(R%) * (100 - INFRAROADPAVEDPCNT(R%)) / 100 'Demanded Connections (stock) in Thousands This Year =(Lines/100)x(Pop x 10000 x 100)/(1000)
                sInfraRoadUnPavedThousands = LANDAREA(R%) * SINFRAROAD(R%) * (100 - sinfraroadpavedpcnt(R%)) / 100 'Connections (Stock) in Thousands Last Year
                CalcInfraInvest(CDbl(InfraRoadUnPavedDemThousands), CDbl(sInfraRoadUnPavedThousands),
                            Deppcnt(RoadUnpaved), INFRAROADUNPAVEDCOST(R%) * 1000, INFRAINVESTNEW(R%, RoadUnpaved), INFRAINVESTMAINT(R%, RoadUnpaved))

                '----- Spending for base (or pre-target) forecast: RAI target (required for allocating enough funds to protect base growth of the physical variable)---------
                INFRAINVESTNEWBF(R%, RoadPaved) = INFRAINVESTNEW(R%, RoadPaved)
                INFRAINVESTMAINTBF(R%, RoadPaved) = INFRAINVESTMAINT(R%, RoadPaved)

                INFRAINVESTNEWBF(R%, RoadUnpaved) = INFRAINVESTNEW(R%, RoadUnpaved)
                INFRAINVESTMAINTBF(R%, RoadUnpaved) = INFRAINVESTMAINT(R%, RoadUnpaved)
                'dkb 2013/09/16 added dimension 1 to infraroadraisetar
                If infraroadraitrgtyr(R%) > 0 Or infraroadraisetar(R%) > 0 Then 'there is an upward RAI target - universal or SE
                    'Paved Roads
                    'physical level this year with base (pre-target)forecast
                    InfraRoadPavedDemThousands = LANDAREA(R%) * InfraRoadBF(R%) * InfraRoadPavedPcntBF(R%) / 100 'Demanded Connections (stock) in Thousands This Year =(Lines/100)x(Pop x 10000 x 100)/(1000)
                    'physical level last year - final
                    sInfraRoadPavedThousands = LANDAREA(R%) * SINFRAROAD(R%) * sinfraroadpavedpcnt(R%) / 100 'Connections (Stock) in Thousands Last Year

                    CalcInfraInvest(CDbl(InfraRoadPavedDemThousands), CDbl(sInfraRoadPavedThousands),
                                Deppcnt(RoadPaved), INFRAROADPAVEDCOST(R%) * 1000, INFRAINVESTNEWBF(R%, RoadPaved), INFRAINVESTMAINTBF(R%, RoadPaved))

                    'UnPaved Roads
                    InfraRoadUnPavedDemThousands = LANDAREA(R%) * InfraRoadBF(R%) * (100 - InfraRoadPavedPcntBF(R%)) / 100 'Demanded Connections (stock) in Thousands This Year =(Lines/100)x(Pop x 10000 x 100)/(1000)
                    sInfraRoadUnPavedThousands = LANDAREA(R%) * SINFRAROAD(R%) * (100 - sinfraroadpavedpcnt(R%)) / 100 'Connections (Stock) in Thousands Last Year
                    CalcInfraInvest(CDbl(InfraRoadUnPavedDemThousands), CDbl(sInfraRoadUnPavedThousands),
                                Deppcnt(RoadUnpaved), INFRAROADUNPAVEDCOST(R%) * 1000, INFRAINVESTNEWBF(R%, RoadUnpaved), INFRAINVESTMAINTBF(R%, RoadUnpaved))

                End If
                '   All but First Call 2: Estimate (Desired) Irrigation Infrastructure
                '---------------------------------------------------------------------------------------
                'sgh 2016/06/20 - START
                ' Remove this formulation that converges to the potential and replace with equation using AG demand
                'and precipiation as drivers.
                '               sErrorSub = saveErrorSub + " " + RegName(R%) + " Irrigation"

                'movement to Saturation (potential) initialized at pre-processor
                '                If LANDIRAREASAT(R%) > slandirareaequip(R%) Then
                '                    RatChange = LANDIRAREASAT(R%) / AMAX(0.001, PreMultLANDIRAREAEQUIP(R%))
                '                    ConvTime = 50 'slow and moving convergence
                '                    cGrowthRatePosition = RatChange ^ (1 / ConvTime) - 1
                '                Else
                '                    cGrowthRatePosition = 0
                '                End If
                '
                '                'smoothing of the growth rate
                '                If Not IsNull(LandIrAreaEquipGR(R%)) Then
                '                    LandIrAreaEquipGR(R%) = LandIrAreaEquipGR(R%) * 0.85 + cGrowthRatePosition * 0.15
                '                    LANDIRAREAEQUIP(R%) = PreMultLANDIRAREAEQUIP(R%) * (1 + LandIrAreaEquipGR(R%))
                '                Else 'no historical data to smooth (first year only)
                '                    LandIrAreaEquipGR(R%) = AMAX(0.000005, Amin(0.08, cGrowthRatePosition))
                '                    LANDIRAREAEQUIP(R%) = PreMultLANDIRAREAEQUIP(R%) * (1 + LandIrAreaEquipGR(R%))
                '                End If
                '
                '                'save the pre-multiplier (or base forecast) value on which growth rate will be applied next year
                '                PreMultLANDIRAREAEQUIP(R%) = LANDIRAREAEQUIP(R%)

                'sgh 2016/06/20
                'If RegName(R%) = "Saudi Arabia" Then Stop
                'If IY% = 2 Then
                'Call AnalFunc("LandEquippedPercentTotal (Log) vs CropDemandOverPotential (Log) & Precip (Log)", _
                'Array((AGDEM(R%, 1) / LANDIRAREASAT(R%)), (SENVPREC(R%) + SENVPRCHG(R%))), Prediction)
                'Prediction = Exp(Prediction) * 100 * LANDIRAREASAT(R%)
                'LandIrAreaEquipResid(R%) = slandirareaequip(R%) / Prediction
                'LANDIRAREAEQUIP(R%) = slandirareaequip(R%)
                'End If
                If IY% > 1 Then
                    'sgh 2018/01/04 - START
                    'Use just precipitation to drive LANDIRAREAEQUIP
                    '                Call AnalFunc("LandEquippedPercentTotal (Log) vs CropDemandOverPotential (Log) & Precip (Log)", _
                    '                Array((SENVPREC(R%) + SENVPRCHG(R%)), (AGDEM(R%, 1) / LANDIRAREASAT(R%))), Prediction)
                    '                Prediction = Exp(Prediction) * 100 * LANDIRAREASAT(R%)
                    'LANDIRAREAEQUIP(R%) = Prediction * LandIrAreaEquipResid(R%)
                    Call XYTABL("LandIrEquipPortionPotential (Linear) versus EnvPrecipitation(MOSTRECENT) (Log)" _
                        , ENVPREC(R%) + SENVPRCHG(R%), Prediction, context)
                    Prediction = Prediction * landirareasat(R%)

                    '                    If IY% < 21 And Not IsNull(LandIrAreaEquipGR(R%)) Then
                    '                        LANDIRAREAEQUIP(R%) = ((21 - IY%) * (slandirareaequip(R%) * (1 + LandIrAreaEquipGR(R%))) + _
                    '                            (IY% - 1) * (Prediction * LandIrAreaEquipResid(R%))) / 20
                    '                    Else
                    '                        LANDIRAREAEQUIP(R%) = Prediction * LandIrAreaEquipResid(R%)
                    '                    End If
                    If IY% < 21 And LandIrAreaEquipGR(R%).HasValue Then
                        LANDIRAREAEQUIP(R%) = If(((21 - IY%) * (slandirareaequip(R%) * (1 + LandIrAreaEquipGR(R%))) +
                                (IY% - 1) * (Prediction * LandIrAreaEquipResid(R%))) / 20, 0)
                        'sgh 2018/02/15 -start
                        '                        LANDIRAREAEQUIP(R%) = LANDIRAREAEQUIP(R%) * (1 + ((swatprice(R%) - 100) / 100) * -0.01) * (1 + ((FPRI(R%, 1) - 100) / 100) * 0.1)
                        LANDIRAREAEQUIP(R%) = CSng(LANDIRAREAEQUIP(R%) * (1 + ((swatprice(R%) - 100) / 100) * landirareaequipelas(R%)) * (1 + ((FPRI(R%, 1) - 100) / 100) * 0.1))
                        'sgh 2018/02/15 - end
                    Else
                        LANDIRAREAEQUIP(R%) = Prediction * LandIrAreaEquipResid(R%)
                        'sgh 2018/02/15 - start
                        '                        LANDIRAREAEQUIP(R%) = LANDIRAREAEQUIP(R%) * (1 + ((swatprice(R%) - 100) / 100) * -0.01) * (1 + ((FPRI(R%, 1) - 100) / 100) * 0.1)
                        LANDIRAREAEQUIP(R%) = CSng(LANDIRAREAEQUIP(R%) * (1 + ((swatprice(R%) - 100) / 100) * landirareaequipelas(R%)) * (1 + ((FPRI(R%, 1) - 100) / 100) * 0.1))
                        'sgh 2018/02/15 - end
                    End If
                End If
                'sgh 2018/04/26 - START
                LANDIRAREAEQUIP(R%) = CSng(AMAX(LANDIRAREAEQUIP(R%), 0.9 * slandirareaequip(R%)))
                'If RegName(R%) = "Saudi Arabia" Then Stop
                'sgh 2018/04/26 - END
                'If RegName(R%) = "South Africa" Then Stop
                'sgh 2018/01/04 - END

                LANDIRAREAEQUIP(R%) = LANDIRAREAEQUIP(R%) * landirareaequipm(R%)

                'If RegName(R%) = "Pakistan" And IY% > 70 Then Stop
                'sgh 2016/07/05

                If slandirareaequip(R%) < landirareasat(R%) Then
                    LANDIRAREAEQUIP(R%) = Amin(LANDIRAREAEQUIP(R%), Amin(slandirareaequip(R%) + ((landirareasat(R%) - slandirareaequip(R%)) / 10),
                        landirareasat(R%)))
                End If

                'sgh 2018/01/04 - START
                'Constrain LANDIRAREAEQUIP by crop land as well
                If LANDIRAREAEQUIP(R%) > (LD(R%, 1) * 1000) Then
                    LANDIRAREAEQUIP(R%) = (LD(R%, 1) * 1000)
                End If
                'sgh 2018/01/04 - END

                LANDIRAREAEQUIP(R%) = Amin(landirareasat(R%), LANDIRAREAEQUIP(R%))

                'sgh 2018/01/04 - START
                'already built irrigation should not go down unless there is a hard multiplier infraroadm(R%, Urban)
                '                If landirareaequipm(R%) >= 1 Then 'scenario handle (multiplier base 1)
                '                    LANDIRAREAEQUIP(R%) = Amin(LANDIRAREASAT(R%), AMAX(slandirareaequip(R%), LANDIRAREAEQUIP(R%)))
                '                End If
                'sgh 2018/01/04 - END

                'mti June 04, 2012 - end
                '   All but First Call 2: Estimate (Desired) Irrigation Infrastructure
                '---------------------------------------------------------------------------------------
                sErrorSub = PrevsErrorSub + " " + RegionName + " Irrigation Costs"
                CalcInfraUnitCost(R%, 10, 5, landircostupper, landircostlower, sGDPPCP(R%), LANDIRCOST(R%))

                LANDIRCOST(R%) = LANDIRCOST(R%) * landircostm(R%)   'base 1 multiplier on irrigation cost
                CalcInfraInvest(CDbl(LANDIRAREAEQUIP(R%)), CDbl(slandirareaequip(R%)),
                            Deppcnt(Irrigation), LANDIRCOST(R%), INFRAINVESTNEW(R%, Irrigation), INFRAINVESTMAINT(R%, Irrigation))
                '----- Fund demand for base (pre-target) infrastructure need: same as there is no irrigation target at this point---------
                INFRAINVESTNEWBF(R%, Irrigation) = INFRAINVESTNEW(R%, Irrigation)
                INFRAINVESTMAINTBF(R%, Irrigation) = INFRAINVESTMAINT(R%, Irrigation)
                '   All but First Call 2: Estimate (Desired) Fixed Telephone Access Rate
                '-----------------------------------------------------------------------

                'sErrorSub = saveErrorSub + " " + RegName(R%) + " Fixed Phone"

                '--------- Old way of doing fixed telephone model --------------------
                'Note MTI: Infrateleold is used for determining MFPPC old way (when inframfpsw <3): for Base inframfpsw=3 now
                'We can now take away INFRATELEOLD (ask BBH first)

                Call XYTABL("GDP/Capita (PPP) Versus Telephones per 1000 (2000)",
                Amin(40, GDPPCP(R%)), Prediction, context)

                INFRATELEOLD(R%) = ConvergeOverTime(Prediction * TeleDensRI(R%), Prediction, mfpconv(R%))

                'Estimation function is per 1000: Variable is per 100: converting here
                INFRATELEOLD(R%) = INFRATELEOLD(R%) / 10

                INFRATELEOLD(R%) = AMAX(INFRATELEMin(R%), INFRATELEOLD(R%))
                '--------- New way of doing fixed telephone  --------------------
                'jrs 2024/07/04 Call XYTABL("GDP/Capita (PPP) Versus Telephones per 100 (MOSTRECENT) Parabolic",
                'Amin(70, GDPPCP(R%)), Prediction, context)
                Call XYTABL(TFNameTelephonesPer100, Amin(70, GDPPCP(R%)), Prediction, context)

                'jrs 2024/07/04 Call XYTABL("GDP/Capita (PPP) Versus Telephones per 100 (MOSTRECENT) Parabolic",
                'Amin(70, sGDPPCP(R%)), Prediction1, context)
                Call XYTABL(TFNameTelephonesPer100, Amin(70, sGDPPCP(R%)), Prediction1, context)

                Prediction = AMAX(1, Prediction)
                Prediction1 = AMAX(1, Prediction1)
                '--- annual growth rate along prediction function (functional growth) ---
                Dim cGrowthRateFunction = (Prediction - Prediction1) / Prediction1

                cGrowthRateFunction = AMAX(0, cGrowthRateFunction)
                'Note MTI: This cap means only upward movement (growth) is picked up from function
                'Downward movement (decline) results only from downward saturation algorithm later in the sequence

                Dim SatVal As Single
                Dim ConvTime As Single
                Dim ShareGrowthRatePosition As Single
                '---Rate of change taking fixed telephone down to a floor value as mobile phone grows (up to the function otherwise): positional growth rate
                If TeleDensSaturation(R%) < 0 Then 'Note MTI: check why this condition is necessary
                    If SICTMobil(R%) > 30 Then 'peak at Mobil = 30
                        'Cell phone reached 30%; allow fixed phone to start to decline towards a floor of 2.5%
                        TeleDensSaturation(R%) = 2.5
                        SatVal = TeleDensSaturation(R%)
                        ConvTime = infrateledtfp ' 15 '30
                        ShareGrowthRatePosition = 0.12
                    Else
                        'Mobile is still low; Fixed phone can grow towards whatever its estimation function predicts
                        SatVal = AMAX(0, Prediction)
                        ConvTime = 20
                        ShareGrowthRatePosition = 0.1
                    End If
                Else
                    'This must be coming from a legacy bug fix; does not seem necessary now; will keep as is for now
                    SatVal = TeleDensSaturation(R%)
                    ConvTime = infrateledtfp '15 '30
                    ShareGrowthRatePosition = 0.12
                End If

                Dim RatChange = SatVal / PreMultINFRATELE(R%)
                Dim cGrowthRatePosition = RatChange ^ (1 / ConvTime) - 1 'can be negative

                '---Moving average with previous year's rate of change----------
                Dim ShareGrowthRateLastYear = 0.8
                Dim ShareGrowthRateFunction = (1 - ShareGrowthRateLastYear) - ShareGrowthRatePosition

                InfraTeleGR(R%) = CType(ShareGrowthRateLastYear * InfraTeleGR(R%) + ShareGrowthRateFunction * cGrowthRateFunction + ShareGrowthRatePosition * cGrowthRatePosition, Single?)
                INFRATELE(R%) = If((1 + InfraTeleGR(R%)) * PreMultINFRATELE(R%), 0)

                '--save pre-multiplier (or base) forecast
                PreMultINFRATELE(R%) = Amin(100, AMAX(0.5, INFRATELE(R%)))

                INFRATELE(R%) = Amin(100, AMAX(0.5, INFRATELE(R%) * infratelem(R%)))
                'mti 2014/05/28 - start
                'See if curve has shifted up by extra money in the previous year
                Dim INFRATELEShiftPrevYear = AMAX(0, (SINFRATELE(R%) - sPreBudINFRATELE(R%))) 'percent point
                'Save pre-finance value:
                sPreBudINFRATELE(R%) = INFRATELE(R%)
                'might be an issue in a multiplier scenario; as SICTBROAD will have multiplier effect; and SBroad(R, 3)  is saved pre-multiplier

                '      If RegName(R%) = "Saudi Arabia" And IY >= 7 Then Stop

                'In case curve has shifted up (by extra money in the previous year, demand signal needs to be sent so that it could continue to stay there
                INFRATELE(R%) = CSng(Amin(100, INFRATELE(R%) + INFRATELEShiftPrevYear))
                'mti 2014/05/28 - end
                '   All but First Call 2: Estimate (Desired) Fixed Telephone Fund Demand
                '-----------------------------------------------------------------------
                CalcInfraUnitCost(R%, 10, 5, infratelecostupper, infratelecostlower, sGDPPCP(R%), INFRATELECOST(R%), False)

                INFRATELECOST(R%) = INFRATELECOST(R%) * infratelecostm(R%) 'multiplier on phone cost

                Dim InfraTeleDemThousands = INFRATELE(R%) * CDbl(POP(R%)) * 10 'Demanded Connections (stock) in Thousands This Year =(Lines/100)x(Pop x 10000 x 100)/(1000)
                Dim sInfraTeleThousands = SINFRATELE(R%) * CDbl(sPOPTotal(R%)) * 10 'Connections (Stock) in Thousands Last Year

                CalcInfraInvest(CDbl(InfraTeleDemThousands), CDbl(sInfraTeleThousands),
                            Deppcnt(Telephone), INFRATELECOST(R%), INFRAINVESTNEW(R%, Telephone), INFRAINVESTMAINT(R%, Telephone))
                '----- Spending for base (pre-multiplier/pre-target) level of fixed phone (same as there is no option for target) --
                INFRAINVESTNEWBF(R%, Telephone) = INFRAINVESTNEW(R%, Telephone)
                INFRAINVESTMAINTBF(R%, Telephone) = INFRAINVESTMAINT(R%, Telephone)

                '   All but First Call 2: Estimate (Desired) ICT Network (old variable)
                '---------------------------------------------------------------------
                'Note MTI: Legacy Variable, ask BBH to move it: BBH said we should do it in September 2012

                'sErrorSub = saveErrorSub + " " + RegName(R%) + " InfraNet"

                INFRANET(R%) = NUMNWP(R%) / AMAX(0.001, POP(R%)) * 100 * infranetm(R%)

                '   All but First Call 2: Estimate (desired) Electricity Access rates
                '----------------------------------------------------------------------

                'sErrorSub = saveErrorSub + " " + RegName(R%) + " Access to Electricity"

                'Note MTI: There is not enough data for calculating a historical growth rate needed for extrapolative approach
                'So we use cross-sectional approach for electricity access instead

                '--- Electricity Access Rate: Urban
                'Note MTI: Goveffect coming from current year: make sure the call sequence is right

                Dim rVal = AMAX(1, 100 / (1 +
                                Math.Exp(-(1.14416457907482 - 4.85840701059291 * SINCOMELT1CS(R%) / sPOPTotal(R%) + 0.837209311546776 * GOVEFFECT(R%)))))
                Dim ShiftAdd As Single
                If INFRAELECACCShift(R%, Urban) > 0 Then
                    ShiftAdd = ConvergeOverTime(INFRAELECACCShift(R%, Urban), 0, 500)
                Else
                    ShiftAdd = ConvergeOverTime(INFRAELECACCShift(R%, Urban), 0, 50)
                End If


                Dim SER = 15.8 'Standard error of the estimation equation supplied by DSR
                'dkb 2013/09/16 added dimension 1 to infraelecaccsetar and infraelecaccseyrtar
                Dim EffectSER As Single
                If infraelecaccsetar(R%) <> 0 And infraelecacctrgtyr(R%) = 0 Then 'SE target: 'Note MTI: 0 SE should be done with a 0.01 SE
                    EffectSER = StdErrorAdditiveShift(CSng(SER), CSng(rVal), CSng(ShiftAdd), CSng(INFRAELECACCShift(R%, Urban)),
                        CSng(infraelecaccsetar(R%)), CInt(FirstYear + infraelecaccseyrtar(R%)), FirstYear + IY - CShort(1), FirstYear, 100)
                Else
                    EffectSER = 0
                End If
                INFRAELECACC(R%, Urban) = CSng(AMAX(0.0001F, rVal + ShiftAdd + EffectSER))

                '-- Base (pre-target/pre-multiplier) forecast values -----------
                sINFRAELECACCBF(R%, Urban) = infraelecaccbf(R%, Urban)
                infraelecaccbf(R%, Urban) = CSng(AMAX(1, Amin(100.0F, rVal + ShiftAdd)))

                '--- cap base growth or decline to 3 percentage points -------------
                If PreMULTINFRAELECACC(R%, Urban) > 1 And infraelecaccsetar(R%) = 0 Then 'for normal base only, bound growt
                    INFRAELECACC(R%, Urban) = Amin(PreMULTINFRAELECACC(R%, Urban) + 3, AMAX(PreMULTINFRAELECACC(R%, Urban) - 3, INFRAELECACC(R%, Urban)))
                End If

                'save base forecast for next year
                PreMULTINFRAELECACC(R%, Urban) = INFRAELECACC(R%, Urban)

                '-- Universal target: reach a value of infraelecacctrgtval(R%) by infraelecacctrgtyr(R%) ---
                If infraelecacctrgtyr(R%) > 0 And infraelecaccsetar(R%) = 0 Then
                    Dim INFRAELECACCUrbanTrgt = Amin(100, iINFRAElecACC(R%, Urban) + Amin(infraelecacctrgtyr(R%), IY - 1) * (infraelecacctrgtval(R%) - iINFRAElecACC(R%, Urban)) / infraelecacctrgtyr(R%))
                    INFRAELECACC(R%, Urban) = AMAX(INFRAELECACC(R%, Urban), INFRAELECACCUrbanTrgt)
                End If

                '-- Multiplier (base 1)
                INFRAELECACC(R%, Urban) = INFRAELECACC(R%, Urban) * If(infraelecaccm(R%, Total) <> 1, infraelecaccm(R%, Total), infraelecaccm(R%, Urban))

                INFRAELECACC(R%, Urban) = AMAX(1, Amin(100, INFRAELECACC(R%, Urban)))

                'already established access rates cannot go down unless there is a hard multiplier infraelecaccm <1 or SE < 0
                If infraelecaccm(R%, Total) >= 1 And infraelecaccm(R%, Urban) >= 1 And infraelecaccsetar(R%) >= 0 Then
                    INFRAELECACC(R%, Urban) = AMAX(sINFRAELECACC(R%, Urban), INFRAELECACC(R%, Urban))
                End If

                '--- Electricity Access Rate: Rural
                rVal = AMAX(1, 100 / (1 +
                                 Math.Exp(-(-0.499710126002115 - 6.92532112045222 * SINCOMELT1CS(R%) / sPOPTotal(R%) + 0.958215164433785 * GOVEFFECT(R%)))))

                If INFRAELECACCShift(R%, Rural) > 0 Then
                    ShiftAdd = ConvergeOverTime(INFRAELECACCShift(R%, Rural), 0, 500)
                Else
                    ShiftAdd = ConvergeOverTime(INFRAELECACCShift(R%, Rural), 0, 50)
                End If

                SER = 17.8 'Standard error of estimation obtained from DSR
                If infraelecaccsetar(R%) <> 0 And infraelecacctrgtyr(R%) = 0 Then 'Standard Error (SE) target
                    EffectSER = StdErrorAdditiveShift(CSng(SER), CSng(rVal), CSng(ShiftAdd), CSng(INFRAELECACCShift(R%, Rural)),
                        CSng(infraelecaccsetar(R%)), CInt(FirstYear + infraelecaccseyrtar(R%)), FirstYear + IY - CShort(1), FirstYear, 100)
                Else
                    EffectSER = 0
                End If

                'Save base (pre-target)/(pre-multiplier) values --
                sINFRAELECACCBF(R%, Rural) = infraelecaccbf(R%, Rural)
                infraelecaccbf(R%, Rural) = CSng(AMAX(1.0F, Amin(100, rVal + ShiftAdd)))
                INFRAELECACC(R%, Rural) = CSng(AMAX(0.0001F, rVal + ShiftAdd + EffectSER))

                'cap maximum percentage growth/decline
                If PreMULTINFRAELECACC(R%, Rural) > 1 And infraelecaccsetar(R%) = 0 Then 'only when no SE targeting
                    INFRAELECACC(R%, Rural) = Amin(PreMULTINFRAELECACC(R%, Rural) + 3, AMAX(PreMULTINFRAELECACC(R%, Rural) - 3, INFRAELECACC(R%, Rural)))
                End If

                PreMULTINFRAELECACC(R%, Rural) = INFRAELECACC(R%, Rural)
                '-- Universal target: reach a value of infraelecacctrgtval(R%) by infraelecacctrgtyr(R%) ---
                If infraelecacctrgtyr(R%) > 0 And infraelecaccsetar(R%) = 0 Then
                    Dim INFRAELECACCRuralTrgt = Amin(100, iINFRAElecACC(R%, Rural) + Amin(infraelecacctrgtyr(R%), IY - 1) * (infraelecacctrgtval(R%) - iINFRAElecACC(R%, Rural)) / infraelecacctrgtyr(R%))
                    INFRAELECACC(R%, Rural) = AMAX(INFRAELECACCRuralTrgt, INFRAELECACC(R%, Rural))
                End If

                'Multiplier base 1
                INFRAELECACC(R%, Rural) = INFRAELECACC(R%, Rural) * If(infraelecaccm(R%, Total) <> 1, infraelecaccm(R%, Total), infraelecaccm(R%, Rural))

                INFRAELECACC(R%, Rural) = AMAX(1, Amin(100, INFRAELECACC(R%, Rural)))

                'Desired/expected demanded access level
                INFRAELECACCDEM(R%, Rural) = INFRAELECACC(R%, Rural)

                'already established access rates cannot go down unless there is a hard multiplier infraelecaccm <1 or negative SE target
                If infraelecaccm(R%, Total) >= 1 And infraelecaccm(R%, Rural) >= 1 And infraelecaccsetar(R%) >= 0 Then
                    INFRAELECACC(R%, Rural) = AMAX(sINFRAELECACC(R%, Rural), INFRAELECACC(R%, Rural))
                End If

                '--- Electricity Access Rate: Total or National --------
                INFRAELECACC(R%, Total) = (SPOPURBAN(R%) * INFRAELECACC(R%, Urban) + (sPOPTotal(R%) - SPOPURBAN(R%)) * INFRAELECACC(R%, Rural)) / sPOPTotal(R%)
                INFRAELECACCDEM(R%, Total) = (SPOPURBAN(R%) * INFRAELECACCDEM(R%, Urban) + (sPOPTotal(R%) - SPOPURBAN(R%)) * INFRAELECACCDEM(R%, Rural)) / sPOPTotal(R%)

                sINFRAELECACCBF(R%, Total) = infraelecaccbf(R%, Total)
                infraelecaccbf(R%, Total) = (SPOPURBAN(R%) * infraelecaccbf(R%, Urban) + (sPOPTotal(R%) - SPOPURBAN(R%)) * infraelecaccbf(R%, Rural)) / sPOPTotal(R%)

                '   All but First Call 2: Estimate (Desired) Electricity Access Fund Demand
                '--------------------------------------------------------------------------
                CalcInfraUnitCost(R%, 10, 5, infraelecaccruralcostupper, infraelecaccruralcostlower, sGDPPCP(R%), INFRAELECACCRURALCOST(R%))
                INFRAELECACCRURALCOST(R%) = INFRAELECACCRURALCOST(R%) * infraelecaccruralcostm(R%)
                Dim InfraElecAccRuralDemThousands = INFRAELECACC(R%, Rural) * CDbl((sPOPTotal(R%) - SPOPURBAN(R%)) * 10 / HHSIZE(R%)) 'Demanded Connections (stock) in Thousands This Year
                Dim sInfraElecAccRuralThousands = sINFRAELECACC(R%, Rural) * CDbl((sPopTotal1(R%) - sPopUrban1(R%))) * 10 / sHHSIZE(R%) 'Connections (Stock) in Thousands Last Year
                CalcInfraInvest(CDbl(InfraElecAccRuralDemThousands), CDbl(sInfraElecAccRuralThousands),
                                Deppcnt(ElectricityAccRural), INFRAELECACCRURALCOST(R%), INFRAINVESTNEW(R%, ElectricityAccRural), INFRAINVESTMAINT(R%, ElectricityAccRural))


                CalcInfraUnitCost(R%, 10, 5, infraelecaccurbancostupper, infraelecaccurbancostlower, sGDPPCP(R%), INFRAELECACCURBANCOST(R%))
                INFRAELECACCURBANCOST(R%) = INFRAELECACCURBANCOST(R%) * infraelecaccurbancostm(R%)
                Dim InfraElecAccUrbanDemThousands = INFRAELECACC(R%, Urban) * CDbl(SPOPURBAN(R%) * 10 / HHSIZE(R%)) 'Demanded Connections (stock) in Thousands This Year
                Dim sInfraElecAccUrbanThousands = sINFRAELECACC(R%, Urban) * CDbl(sPopUrban1(R%)) * 10 / sHHSIZE(R%) 'Connections (Stock) in Thousands Last Year
                CalcInfraInvest(CDbl(InfraElecAccUrbanDemThousands), CDbl(sInfraElecAccUrbanThousands),
                                Deppcnt(ElectricityAccUrban), INFRAELECACCURBANCOST(R%), INFRAINVESTNEW(R%, ElectricityAccUrban), INFRAINVESTMAINT(R%, ElectricityAccUrban))

                '----- Spending needed to achieve base (pre-target) level of electricity access --

                INFRAINVESTNEWBF(R%, ElectricityAccUrban) = INFRAINVESTNEW(R%, ElectricityAccUrban)
                INFRAINVESTMAINTBF(R%, ElectricityAccUrban) = INFRAINVESTMAINT(R%, ElectricityAccUrban)

                INFRAINVESTNEWBF(R%, ElectricityAccRural) = INFRAINVESTNEW(R%, ElectricityAccRural)
                INFRAINVESTMAINTBF(R%, ElectricityAccRural) = INFRAINVESTMAINT(R%, ElectricityAccRural)

                If infraelecacctrgtyr(R%) > 0 Or infraelecaccsetar(R%) > 0 Then 'there is an upward RAI target
                    'Note MT: 0 SE must be tested with a very small SE (like .001)
                    'Urban
                    InfraElecAccUrbanDemThousands = infraelecaccbf(R%, Urban) * CDbl(SPOPURBAN(R%) * 10 / HHSIZE(R%)) 'Demanded Connections (stock) in Thousands This Year
                    sInfraElecAccUrbanThousands = sINFRAELECACC(R%, Urban) * CDbl(sPopUrban1(R%)) * 10 / sHHSIZE(R%) 'Connections (Stock) in Thousands Last Year
                    CalcInfraInvest(CDbl(InfraElecAccUrbanDemThousands), CDbl(sInfraElecAccUrbanThousands),
                                    Deppcnt(ElectricityAccUrban), INFRAELECACCURBANCOST(R%), INFRAINVESTNEWBF(R%, ElectricityAccUrban), INFRAINVESTMAINTBF(R%, ElectricityAccUrban))
                    'Rural
                    InfraElecAccRuralDemThousands = infraelecaccbf(R%, Rural) * CDbl((sPOPTotal(R%) - SPOPURBAN(R%)) * 10 / HHSIZE(R%)) 'Demanded Connections (stock) in Thousands This Year
                    sInfraElecAccRuralThousands = sINFRAELECACC(R%, Rural) * CDbl((sPopTotal1(R%) - sPopUrban1(R%))) * 10 / sHHSIZE(R%) 'Connections (Stock) in Thousands Last Year
                    CalcInfraInvest(CDbl(InfraElecAccRuralDemThousands), CDbl(sInfraElecAccRuralThousands),
                                    Deppcnt(ElectricityAccRural), INFRAELECACCRURALCOST(R%), INFRAINVESTNEWBF(R%, ElectricityAccRural), INFRAINVESTMAINTBF(R%, ElectricityAccRural))
                End If
                '   All but First Call 2: (Desired) Electricity Generation Capacity Model
                '---------------------------------------------------------------------------

                '   All but First Call 2:  Electricity Share of Energy Consumption
                '------------------------------------------------------------------
                'sErrorSub = saveErrorSub + " " + RegName(R%) + " Electricity Consumption"

                'Statistical model from DSR
                Prediction = CSng(AMAX(0.0001F, Math.Exp(-0.021399 + 0.2753483 * Math.Log(GDPPCP(R%)) + 0.4923844 * Math.Log(INFRAELECACC(R%, Total)) - 0.077159 * Math.Log(iFossilshare(R%)) + 0.1227198 * Math.Log(iNonFossilshare(R%)))))
                Prediction1 = CSng(AMAX(0.0001F, Math.Exp(-0.021399 + 0.2753483 * Math.Log(sGDPPCP(R%)) + 0.4923844 * Math.Log(sDesiredINFRAELECACCTotal(R%)) - 0.077159 * Math.Log(iFossilshare(R%)) + 0.1227198 * Math.Log(iNonFossilshare(R%)))))

                'save base electricity access total for use as a driver next year
                'Note MTI 1: can use ..BF variable now)
                'Note MTI 2: discuss this with DSR/BBH (should we use base desired or base after funding?)
                sDesiredINFRAELECACCTotal(R%) = INFRAELECACC(R%, Total)

                'extrapolative approach: growth rate
                'If GRPS(1, R) = "Maldives" Then Stop
                EnElecShrEnDemGR(R%) = CSng(CalcExtrapolativeGR(EnElecShrEnDemGR(R%), iextmafuncenelecshr(R%), extmaposnenelecshr, CDbl(PreMultEnElecShrEnDem(R%)), CDbl(Prediction), CDbl(Prediction1), iextmaposnconvtimeenelecshr(R%)))
                ENELECSHRENDEM(R%) = If(PreMultEnElecShrEnDem(R%) * (1 + EnElecShrEnDemGR(R%)), 0)

                PreMultEnElecShrEnDem(R%) = AMAX(0.001, Amin(100, ENELECSHRENDEM(R%)))

                ENELECSHRENDEM(R%) = ENELECSHRENDEM(R%) * enelecshrendemm(R%)

                'Note MTI: no cap here? Electricity cannot be all of energy by 2100: Discuss
                ENELECSHRENDEM(R%) = AMAX(0.001, Amin(100, ENELECSHRENDEM(R%)))

                'Energy demand data and forecast do not match in first year (see energy/economic modules)
                'Infrastructure module maintains a data and forecast ratio that converges to 1
                Dim EnDemDFRIVal = ConvergeOverTime(EnDemDFRI(R%), 1, enconv)
                'Shadow energy demand come from ENDEM data (pre-processed in datainfra: it converges to ENDEM using above ratio
                ENDEMSH(R%) = ENDEM(R%) * EnDemDFRIVal

                '   All but First Call 2:  Electricity Consumption
                '------------------------------------------------------------------

                'Electricity consumption computed using the energy share of electricity; we use ENDEM consitent with historical data
                Dim CompINFRAELEC = ENELECSHRENDEM(R%) * ENDEM(R%) * EnDemDFRIVal * 17000 / AMAX(0.000001, POP(R%)) 'in kwhr ; ENELECSHRENDEM(R%) in 0-100; POP in millions; EnDem in BBOE; 1 BOE = 1700 kwhr
                'Apply initial year ratio (or multiplicative shift)
                'If GRPS(1, R) = "Maldives" Then Stop
                INFRAELEC(R%) = ConvergeOverTime(CompINFRAELEC * InfraElecRI(R%), CompINFRAELEC, 40) '25
                'jrs 2023/10/25
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("INFRAELEC", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                '   All but First Call 2:  Electricity Transmission Loss
                '-------------------------------------------------------
                'sErrorSub = saveErrorSub + " " + RegName(R%) + " Electricity Transmission"

                If Not InfraElecTranLossGR(R%).HasValue Then
                    'cross-sectional model (2nd year only) for countries with not enough data for growth rate
                    'Note MTI: check for transient between second and third year
                    rVal = CSng(Math.Exp(3.1253418 - 0.124777 * GOVREGQUAL(R%) - 0.025779 * GDPPCP(R%)))

                    'value was bound between 3 to 90 in data pre-processor
                    rVal = Amin(90, AMAX(3, rVal))

                    If INFRAELECTRANLOSSShift(R%) > 0 Then
                        'higher than function - let it come down (lower is better in his case)
                        INFRAELECTRANLOSS(R%) = CSng(rVal + ConvergeOverTime(INFRAELECTRANLOSSShift(R%), 0, 50))
                    Else
                        'lower than function - keep there (lower is better in his case)
                        INFRAELECTRANLOSS(R%) = CSng(rVal + ConvergeOverTime(INFRAELECTRANLOSSShift(R%), 0, 500))
                    End If
                    InfraElecTranLossGR(R%) = INFRAELECTRANLOSS(R%) / PreMultINFRAELECTRANLOSS(R%) - 1
                Else

                    'extrapolative approach
                    Prediction = CSng(Amin(90, AMAX(3, Math.Exp(3.1253418 - 0.124777 * GOVREGQUAL(R%) - 0.025779 * GDPPCP(R%)))))
                    Prediction1 = CSng(Amin(90, AMAX(3, Math.Exp(3.1253418 - 0.124777 * SGOVREGQUAL(R%) - 0.025779 * sGDPPCP(R%)))))

                    'extrapolative approach - can be negative
                    InfraElecTranLossGR(R%) = CSng(CalcExtrapolativeGR(InfraElecTranLossGR(R%), extmafuncelectran, extmaposnelectran, CDbl(PreMultINFRAELECTRANLOSS(R%)), CDbl(Prediction), CDbl(Prediction1), extmaposnconvtimeelectran, , True, False))
                End If

                INFRAELECTRANLOSS(R%) = If(PreMultINFRAELECTRANLOSS(R%) * (1 + InfraElecTranLossGR(R%)), 0)


                PreMultINFRAELECTRANLOSS(R%) = AMAX(3, Amin(90, INFRAELECTRANLOSS(R%))) ''mti Sep 12, 2011 bound change - was .001; 100

                INFRAELECTRANLOSS(R%) = INFRAELECTRANLOSS(R%) * infraelectranlossm(R%)
                INFRAELECTRANLOSS(R%) = AMAX(3, Amin(90, INFRAELECTRANLOSS(R%))) 'lower bound taken from 3 to 2

                '   All but First Call 2:  Electricity Adjustment Factor, Production, Consumption/productionRatio
                '------------------------------------------------------------------------------------------------

                'sErrorSub = saveErrorSub + " " + RegName(R%) + " Electricity Adjustment"

                'convergence of initial value to .55: Note MTI: convergeovertime routine skips 1; so using ConvergeOverTime1 which starts from year 1

                INFRAELECADJFACT(R%) = ConvergeOverTime1(If(iInfraelecadjfact(R%), 0), 0.55, 50)

                INFRAELECPROD(R%) = sINFRAELECGENCAP(R%) * sPOPTotal(R%) * 8766 * INFRAELECADJFACT(R%)

                'INFRAELEC is in Kwh/person;  POP in million peopl; infraelec*pop in million kwh or Gw-Hours
                INFRAELECCONSPRODRATIO(R%) = (INFRAELEC(R%) * POP(R%)) / (AMAX(0.0001, INFRAELECPROD(R%)) * AMAX(0.001, (1 - INFRAELECTRANLOSS(R%) / 100)))


                '   All but First Call 2:  Compute the need for adding electricity generation capacity given the consumption and production
                '--------------------------------------------------------------------------------------------------------------------------
                'Note MTI: At this point trade is not explicitly represented; trade is however proxied by maintaining a fixed consumption-production ratio from year 1

                'sErrorSub = saveErrorSub + " " + RegName(R%) + " Electricity Generation Cap"
                Dim GenCapToAddPerCap As Single = 0
                If INFRAELECCONSPRODRATIO(R%) > iINFRAELECCONSPRODRATIO(R%) Then
                    'this years' consumption-production ratio greater than initial/fixed ratio: need to add capacity

                    Dim DeltaRat = INFRAELECCONSPRODRATIO(R%) - iINFRAELECCONSPRODRATIO(R%)
                    Dim CurrYear_ElecCons_GwHr = INFRAELEC(R%) * POP(R%)
                    Dim AdjustedProductionPostLoss = CurrYear_ElecCons_GwHr * iINFRAELECPROD4RATIO(R%) / AMAX(0.000001, iINFRAELECCON4RATIO(R%))
                    Dim AdjustedProduction = AdjustedProductionPostLoss / AMAX(0.001, (1 - INFRAELECTRANLOSS(R%) / 100)) 'CurrYear_ElecCons_GwHr * iINFRAELECPROD4RATIO(R%) / AMAX(0.000001, iINFRAELECCON4RATIO(R%))
                    Dim Adjusted_GenCapPC = AdjustedProduction / 8766 / AMAX(0.00001, POP(R%)) / AMAX(0.001, INFRAELECADJFACT(R%))

                    'the following is in kw/person
                    GenCapToAddPerCap = Adjusted_GenCapPC - sINFRAELECGENCAP(R%)


                Else
                    GenCapToAddPerCap = 0
                End If
                Dim cElecGrowthRatePosition As Single = 0
                Prediction = sINFRAELECGENCAP(R%) + GenCapToAddPerCap  'GenCapNeeded
                If Prediction > sINFRAELECGENCAP(R%) Then
                    RatChange = Prediction / sINFRAELECGENCAP(R%)
                    ConvTime = 1  '5 '1  '30  'years
                    'mti 2023/09/21
                    cElecGrowthRatePosition = CSng(Amin(0.1, CSng(RatChange ^ (1 / ConvTime) - 1))) 'can be negative
                Else
                    cElecGrowthRatePosition = 0 '0.000005 'negligible growth
                End If


                InfraElecGenCapGR(R%) = CType(0.8 * INFRAELECGENCAPRUnConstrained(R%) + 0.2 * cElecGrowthRatePosition, Single?)
                Dim INFRAELECGENCAPTot = sInfraElecGenCapTot(R%) * (1 + InfraElecGenCapGR(R%))
                INFRAELECGENCAP(R%) = If(INFRAELECGENCAPTot / POP(R%), 0)
                INFRAELECGENCAP(R%) = AMAX(0.0001, Amin(50, INFRAELECGENCAP(R%)))

                INFRAELECGENCAPRUnConstrained(R%) = (INFRAELECGENCAP(R%) * POP(R%) - sInfraElecGenCapTot(R%)) / sInfraElecGenCapTot(R%)
                INFRAELECGENCAP(R%) = INFRAELECGENCAP(R%) * infraelecgencapm(R%)
                'mti 2014/08/20 - start
                '                'mti 2014/05/28 - start
                '
                '                'In case curve has shifted up, it should stay there
                '                InfraElecGenShiftPrevYear = AMAX(0, (sINFRAELECGENCAP(R%) - sPreBudINFRAELECGENCAP(R%)))
                '
                ''        If RegName(R%) = "Saudi Arabia" And IY >= 6 Then Stop
                '
                '                sPreBudINFRAELECGENCAP(R%) = INFRAELECGENCAP(R%)
                '
                '                INFRAELECGENCAP(R%) = INFRAELECGENCAP(R%) + InfraElecGenShiftPrevYear
                '                'mti 2014/05/28 - end
                'mti 2014/08/20 - end


                'already built generation plants should not go down unless there is a hard multiplier infraroadm(R%, Urban)
                If infraelecgencapm(R%) >= 1 Then
                    INFRAELECGENCAPTot = AMAX(INFRAELECGENCAP(R%) * POP(R%), sInfraElecGenCapTot(R%))
                    INFRAELECGENCAP(R%) = CSng(INFRAELECGENCAPTot / AMAX(0.00001, POP(R%)))
                End If
                'note to MTI: asked DSR to think about the consequences on things like : negative initial growth rate; export - import balance


                '---- Electricity Demand and Supply with base forecast (as opposed to target forecast) on elctricity access - Start --

                PredictionBF = CSng(Math.Exp(-0.021399 + 0.2753483 * Math.Log(GDPPCP(R%)) + 0.4923844 * Math.Log(infraelecaccbf(R%, Total)) - 0.077159 * Math.Log(iFossilshare(R%)) + 0.1227198 * Math.Log(iNonFossilshare(R%))))
                Dim PredictionBF1 = Math.Exp(-0.021399 + 0.2753483 * Math.Log(sGDPPCP(R%)) + 0.4923844 * Math.Log(sDesiredINFRAELECACCTotalBF(R%)) - 0.077159 * Math.Log(iFossilshare(R%)) + 0.1227198 * Math.Log(iNonFossilshare(R%)))

                'save for next year
                sDesiredINFRAELECACCTotalBF(R%) = infraelecaccbf(R%, Total)

                EnElecShrEnDemGRBF(R%) = CSng(CalcExtrapolativeGR(EnElecShrEnDemGRBF(R%), iextmafuncenelecshr(R%), extmaposnenelecshr, CDbl(PreMultEnElecShrEnDem(R%)), CDbl(PredictionBF), CDbl(PredictionBF1), iextmaposnconvtimeenelecshr(R%)))
                EnElecShrEnDemBF(R%) = CSng(AMAX(0.001, Amin(100, EnElecShrEnDemBF(R%) * (1 + EnElecShrEnDemGRBF(R%)))))

                Dim CompINFRAELECBF = EnElecShrEnDemBF(R%) * ENDEM(R%) * EnDemDFRIVal * 17000 / AMAX(0.000001, POP(R%)) 'in kwhr ; ENELECSHRENDEM(R%) in 0-100; POP in millions; EnDem in BBOE; 1 BOE = 1700 kwhr
                INFRAELECBF = ConvergeOverTime(CompINFRAELECBF * InfraElecRI(R%), CompINFRAELECBF, 40)
                'mti July 05, 2012 -start
                'Here we should use real installation; not base forecast?
                Dim INFRAELECPRODBF = sINFRAELECGENCAP(R%) * sPOPTotal(R%) * 8766 * INFRAELECADJFACT(R%)

                'INFRAELEC is in Kwh/person;  POP in million peopl; infraelec*pop in million kwh or Gw-Hours
                Dim INFRAELECCONSPRODRATIOBF = (INFRAELECBF * POP(R%)) / (AMAX(0.0001, INFRAELECPRODBF) * AMAX(0.001, (1 - INFRAELECTRANLOSS(R%) / 100)))


                If INFRAELECCONSPRODRATIOBF > iINFRAELECCONSPRODRATIO(R%) Then
                    'this years' consumption-production ratio greater than historic cons

                    'adjust production?
                    Dim DeltaRat = INFRAELECCONSPRODRATIOBF - iINFRAELECCONSPRODRATIO(R%)
                    Dim CurrYear_ElecCons_GwHr = INFRAELECBF * POP(R%)

                    Dim AdjustedProductionPostLoss = CurrYear_ElecCons_GwHr * iINFRAELECPROD4RATIO(R%) / AMAX(0.000001, iINFRAELECCON4RATIO(R%))
                    Dim AdjustedProduction = AdjustedProductionPostLoss / AMAX(0.001, (1 - INFRAELECTRANLOSS(R%) / 100)) 'CurrYear_ElecCons_GwHr * iINFRAELECPROD4RATIO(R%) / AMAX(0.000001, iINFRAELECCON4RATIO(R%))

                    Dim Adjusted_GenCapPC = AdjustedProduction / 8766 / AMAX(0.00001, POP(R%)) / AMAX(0.001, INFRAELECADJFACT(R%))

                    'the following is in kw/person
                    GenCapToAddPerCap = Adjusted_GenCapPC - sINFRAELECGENCAP(R%)

                Else

                    GenCapToAddPerCap = 0
                End If

                Prediction = sINFRAELECGENCAP(R%) + GenCapToAddPerCap  'GenCapNeeded
                If Prediction > sINFRAELECGENCAP(R%) Then
                    RatChange = Prediction / sINFRAELECGENCAP(R%)
                    ConvTime = 1  '5 '1  '30  'years
                    'mti 2023/09/21
                    cElecGrowthRatePosition = CSng(Amin(0.1, CSng(RatChange ^ (1 / ConvTime) - 1))) 'can be negative
                Else
                    cElecGrowthRatePosition = 0 '0.000005 'negligible growth
                End If

                InfraElecGenCapGRBF(R%) = CType(0.8 * InfraElecGenCapGRBF(R%) + 0.2 * cElecGrowthRatePosition, Single?)

                Dim InfraElecGenCapTotBF = sInfraElecGenCapTot(R%) * (1 + InfraElecGenCapGRBF(R%))

                sInfraElecGenCapBF(R%) = InfraElecGenCapBF(R%)

                InfraElecGenCapBF(R%) = If(AMAX(0.0001F, Amin(50.0F, InfraElecGenCapTotBF / POP(R%))), 0)

                InfraElecGenCapGRBF(R%) = (InfraElecGenCapBF(R%) * POP(R%) - sInfraElecGenCapTot(R%)) / sInfraElecGenCapTot(R%)
                sInfraElecGenCapTotBF(R%) = InfraElecGenCapBF(R%) * POP(R%)

                '---- Electricity Demand and Supply with base forecast (as opposed to target forecast) on elctricity access - End --

                '   All but First Call 2:  Electricity Generation Capacity Finance
                '-----------------------------------------------------------------

                CalcInfraUnitCost(R%, 13, 3, infraelecgencostupper, infraelecgencostlower, sGDPPCP(R%), INFRAELECGENCOST(R%)) 'thousand $)
                INFRAELECGENCOST(R%) = INFRAELECGENCOST(R%) * infraelecgencostm(R%)
                Dim InfraElectricityDemThousands = INFRAELECGENCAP(R%) * CDbl(POP(R%)) * 1000 'Demanded Connections (stock) in Thousands This Year =(Lines/100)x(Pop x 10000 x 100)/(1000)
                Dim sInfraElectricityThousands = sINFRAELECGENCAP(R%) * CDbl(sPOPTotal(R%)) * 1000 'Connections (Stock) in Thousands Last Year

                CalcInfraInvest(CDbl(InfraElectricityDemThousands), CDbl(sInfraElectricityThousands),
                            Deppcnt(ElectricityGen), INFRAELECGENCOST(R%), INFRAINVESTNEW(R%, ElectricityGen), INFRAINVESTMAINT(R%, ElectricityGen))


                '----- Spending for base forecast electricity generation (pre-target) ---------

                INFRAINVESTNEWBF(R%, ElectricityGen) = INFRAINVESTNEW(R%, ElectricityGen)
                INFRAINVESTMAINTBF(R%, ElectricityGen) = INFRAINVESTMAINT(R%, ElectricityGen)

                If infraelecacctrgtyr(R%) > 0 Or infraelecaccsetar(R%) > 0 Then 'there is an upward hard/Se target
                    'base forecast cost - should be same for base case
                    InfraElectricityDemThousands = InfraElecGenCapBF(R%) * CDbl(POP(R%)) * 1000 'Demanded Connections (stock) in Thousands This Year =(Lines/100)x(Pop x 10000 x 100)/(1000)
                    sInfraElectricityThousands = sINFRAELECGENCAP(R%) * CDbl(sPOPTotal(R%)) * 1000 'Connections (Stock) in Thousands Last Year
                    CalcInfraInvest(CDbl(InfraElectricityDemThousands), CDbl(sInfraElectricityThousands),
                                Deppcnt(ElectricityGen), INFRAELECGENCOST(R%), INFRAINVESTNEWBF(R%, ElectricityGen), INFRAINVESTMAINTBF(R%, ElectricityGen))
                End If
                '--------------------------------------------------------------------------------
                'Note MTI: save this code for when we add trade eexplicitly - start
                '-----------------------------------------------------------------------------------
                '            'mti April 2011 - electricity trade comment for now - start
                '            Next R%
                '
                '            GlobalElecDeficit = 0
                '            GlobalElecSurplus = 0
                '
                '            For R% = 1 To NR%
                '                ElecDeficit(R%) = 0
                '                ElecSurplus(R%) = 0
                '                AvailElec(R%) = AMAX(0.0001, INFRAELECPROD(R%)) * AMAX(0.001, (1 - INFRAELECTRANLOSS(R%) / 100))
                '                If INFRAELECCONSPRODRATIO(R%) > 1 Then
                '                    ElecDeficit(R%) = INFRAELEC(R%) * POP(R%) - AvailElec(R%)
                '                    GlobalElecDeficit = GlobalElecDeficit + ElecDeficit(R%)
                '                End If
                '                If INFRAELECCONSPRODRATIO(R%) < 1 Then
                '                    ElecSurplus(R%) = AvailElec(R%) - INFRAELEC(R%) * POP(R%)
                '                    GlobalElecSurplus = GlobalElecSurplus + ElecSurplus(R%)
                '                End If
                '
                '            Next R%
                '
                '            'adjust with initial year
                '            GlobalSDRatio = GlobalElecSurplus / GlobalElecDeficit / iGlobalSDRatio
                '
                '            If GlobalElecSurplus > 0 And GlobalElecDeficit > 0 Then
                '                For R% = 1 To NR%
                '                    ElecImport(R%) = 0
                '                    ElecExport(R%) = 0
                '
                '                    If GlobalElecSurplus > GlobalElecDeficit Then
                '                        If ElecDeficit(R%) > 0 Then
                '                            ElecImport(R%) = ElecDeficit(R%)
                '                        End If
                '                        If ElecSurplus(R%) > 0 Then
                '                            ElecExport(R%) = ElecSurplus(R%) / GlobalSDRatio
                '                            AvailElec(R%) = AvailElec(R%) - (ElecSurplus(R%) - ElecExport(R%))
                '                            INFRAELECPROD(R%) = AvailElec(R%) / AMAX(0.001, (1 - INFRAELECTRANLOSS(R%) / 100))
                '                            INFRAELECADJFACT(R%) = INFRAELECPROD(R%) / sInfraelecGencap(R%) / POP(R%) / 8766
                '                        End If
                '                    End If
                '
                '
                '                    If GlobalElecSurplus < GlobalElecDeficit Then
                '                        If ElecDeficit(R%) > 0 Then
                '                            ElecImport(R%) = ElecDeficit(R%) * GlobalSDRatio
                '                            INFRAELEC(R%) = INFRAELEC(R%) - (ElecDeficit(R%) - ElecImport(R%)) / POP(R%)
                '                        End If
                '                        If ElecSurplus(R%) > 0 Then
                '                            ElecExport(R%) = ElecSurplus(R%)
                '                        End If
                '                    End If
                '                Next R%
                '            End If
                '
                '            For R% = 1 To NR%
                '--------------------------------------------------------------------------------
                'Note MTI: save this code for when we add trade eexplicitly - end
                '-----------------------------------------------------------------------------------


                '   All but First Call 2:  Computers per 100 persons
                '---------------------------------------------------

                sErrorSub = PrevsErrorSub + " " + RegionName + " Computers"
                'mti 2015/04/29 - start
                'The following multi-variate function was done with Computer per 100 (NOT with % of hh with computers)

                'Call AnalFunc("ICT Computers Per 100 Person with Electricity", _
                'Array(INFRAELEC(R), GDPPCP(R), GOVREGQUAL(R)), rVal, _
                '    Array("EnElecConsPerCap(MOSTRECENT) (Linear)", "GDP2000PCPPP(MOSTRECENT)/ 1000 (Log)", "GovernanceRegQual(MOSTRECENT) (Linear)")) 'jrs 2014/10/09 added DepVar Names to check order
                'should use sGDPPCP (if GDPPCP not calculated yet)

                'Temporary replcament with a simpe rregression of % of households with computers on GDP PCP (Logistic function)
                'mti 2015/05/06 - start
                'jrs 2024/06/27 TbName$ = "GDP/Capita (PPP 2011) Versus Computer Owning Households(MOSTRECENT) Log"  'function for earlier years yet to be estimated
                'mti 2015/05/06 - end
                Dim rVal2 As Single
                Call XYTABL(TFNameComputerOwningHH, GDPPCP(R%), rVal2, context)
                rVal = rVal2
                'mti 2015/04/29 - end
                If ICTComputerShift(R) < 0 Then 'upward convergence only
                    ICTCOMPUTERS(R) = CSng(rVal + ConvergeOverTime(ICTComputerShift(R), 0, 50))
                Else
                    ICTCOMPUTERS(R) = CSng(rVal + ConvergeOverTime(ICTComputerShift(R), 0, 200))
                End If

                '------ Tech Shift
                Dim incCS = ICTCOMPUTERS(R) - SComputers(R, 1)
                Dim incTec As Single
                If SComputers(R, 2) < ictcomputerstecinflection Then
                    incTec = (ictcomputerstechighrt - ictcomputersteclowrt) * SComputers(R, 2) / ictcomputerstecinflection + ictcomputersteclowrt
                Else
                    incTec = (ictcomputersteclowrt - ictcomputerstechighrt) * (SComputers(R, 2) - ictcomputerstecinflection) / (100 - ictcomputerstecinflection) + ictcomputerstechighrt
                End If

                SComputers(R, 1) = ICTCOMPUTERS(R)

                'Add Increase/Decrease to final value, this is the tec factor
                'Total increment can't be more than 25% of the remaining gap to reach target of 120
                ICTCOMPUTERS(R) = SComputers(R, 2) + Amin(incTec + incCS, (100 - SComputers(R, 2)) / 4)
                'mti 2015/06/01 - start
                'ICTCOMPUTERS(R) = Amin(AMAX(ICTCOMPUTERS(R), SComputers(R, 2)), 100)
                ICTCOMPUTERS(R) = Amin(AMAX(ICTCOMPUTERS(R), SComputers(R, 2)) * ictcomputersm(R%), 100)
                SComputers(R, 2) = ICTCOMPUTERS(R)

                '------adding logistic growth   -------------- end  ---

                'mti 2015/04/29 - start
                ''ICTCOMPUTERS(R) = Amin(ICTCOMPUTERS(R), 150)
                'ICTCOMPUTERS(R) = Amin(ICTCOMPUTERS(R), 100)
                'mti 2015/06/01 - end
                'mti 2015/04/29 - end


                'mti 2015/04/29 - start
                'First Year Call 2: Households with Internet Shift Factor
                '---------------------------------------------------------
                sErrorSub = PrevsErrorSub + " " + RegionName + " Internet HH"
                'mti 2015/05/06 - start
                'jrs 2024/06/28 TbName$ = "GDP/Capita (PPP 2011) Versus Internet Connected Households(MOSTRECENT) Log"
                'mti 2015/05/06 - end
                Call XYTABL(TFNameInternetConnHH, GDPPCP(R%), rVal2, context)
                rVal = rVal2

                If ICTIntNetHHShift(R) < 0 Then
                    ICTINTNETHH(R) = CSng(rVal + ConvergeOverTime(ICTIntNetHHShift(R), 0, 50))
                Else
                    ICTINTNETHH(R) = CSng(rVal + ConvergeOverTime(ICTIntNetHHShift(R), 0, 200))
                End If

                '------ Tech Shift (temporarily) using same inflection rate etc as Computers
                incCS = ICTINTNETHH(R) - SICTIntNetHH(R, 1)
                If SICTIntNetHH(R, 2) < ictcomputerstecinflection Then
                    incTec = (ictcomputerstechighrt - ictcomputersteclowrt) * SICTIntNetHH(R, 2) / ictcomputerstecinflection + ictcomputersteclowrt
                Else
                    incTec = (ictcomputersteclowrt - ictcomputerstechighrt) * (SICTIntNetHH(R, 2) - ictcomputerstecinflection) / (100 - ictcomputerstecinflection) + ictcomputerstechighrt
                End If

                SICTIntNetHH(R, 1) = ICTINTNETHH(R)

                'Add Increase/Decrease to final value, this is the tec factor
                'Total increment can't be more than 25% of the remaining gap to reach target of 120
                ICTINTNETHH(R) = SICTIntNetHH(R, 2) + Amin(incTec + incCS, (100 - SICTIntNetHH(R, 2)) / 4)

                'mti 2015/06/01 - start
                'ICTINTNETHH(R) = Amin(AMAX(ICTINTNETHH(R), SICTIntNetHH(R, 2)), 100)
                ICTINTNETHH(R) = Amin(AMAX(ICTINTNETHH(R), SICTIntNetHH(R, 2)) * ictintnethhm(R), 100)

                SICTIntNetHH(R, 2) = ICTINTNETHH(R)

                '------adding logistic growth   -------------- end  ---

                'ICTINTNETHH(R) = Amin(ICTINTNETHH(R), 100)
                'mti 2015/06/01 - end

                '========================================
                'mti 2015/04/29 - end


            Next 'R%
            sErrorSub = PrevsErrorSub
        End Sub

        Public Sub IFsInfra3AllbutFirstYear(context As IForecastContext)
            'call3:
            'ttt 2019/10/08 TakeOut
            Array.Copy(INFRAROAD, INFRAROADDEM, NR + 1)
            Array.Copy(INFRAROADDEM, sINFRAROADDEM, NR + 1)
            Array.Copy(INFRAROADPAVEDPCNT, INFRAROADPAVEDPCNTDEM, NR + 1)
            Array.Copy(INFRAROADRAI, INFRAROADRAIDEM, NR + 1)

            Array.Copy(INFRAELECACC, INFRAELECACCDEM, INFRAELECACC.Length)
            Array.Copy(INFRAELECGENCAP, INFRAELECGENCAPDEM, NR + 1)
            Array.Copy(SANITATION, SANITATIONDEM, SANITATION.Length)
            Array.Copy(WATSAFE, WATSAFEDEM, WATSAFE.Length)
            Array.Copy(WATWASTETREAT, WATWASTETREATDEM, NR + 1)
            Array.Copy(LANDIRAREAEQUIP, LANDIRAREAEQUIPDEM, NR + 1)
            Array.Copy(INFRATELE, INFRATELEDEM, NR + 1)
            Array.Copy(ICTMOBIL, ICTMOBILDEM, NR + 1)
            Array.Copy(ICTBROAD, ICTBROADDEM, NR + 1)
            Array.Copy(ICTBROADMOBIL, ICTBROADMOBILDEM, NR + 1)

            For R% = 1 To NR%
                '   All but First Call 3:  Determine Final Infrastructure Levels Given The Public Budget Allocation
                '--------------------------------------------------------------------------------------------------
                'Note MTI: In most cases private sector is equivalently affected by public sector fund shortage

                '---------- Save pre-budget or desired infrastructure forecasts ---------------------

                '   If RegName(R%) = "Bangladesh" Then Stop
                '    If RegName(R%) = "Saudi Arabia" And IY >= 7 Then Stop
                'ttt 2019/10/08 TakenOut
                'INFRAROADDEM(R%) = INFRAROAD(R%)
                'sINFRAROADDEM(R%) = INFRAROADDEM(R%)
                'INFRAROADPAVEDPCNTDEM(R%) = INFRAROADPAVEDPCNT(R%)
                'INFRAROADRAIDEM(R%) = INFRAROADRAI(R%)

                'For j = 1 To NLocation
                '    INFRAELECACCDEM(R%, j) = INFRAELECACC(R%, j)
                'Next
                'INFRAELECGENCAPDEM(R%) = INFRAELECGENCAP(R%)

                ''--watsan desired levels are also calculated by no: (IfsInfrWatsan call 1) w : OK to save ..DEM variables
                'For j = 1 To 3
                '    SANITATIONDEM(R%, j) = SANITATION(R%, j)
                '    WATSAFEDEM(R%, j) = WATSAFE(R%, j)
                'Next

                'WATWASTETREATDEM(R%) = WATWASTETREAT(R%)

                'LANDIRAREAEQUIPDEM(R%) = LANDIRAREAEQUIP(R%)
                'INFRATELEDEM(R%) = INFRATELE(R%)
                'ICTMOBILDEM(R%) = ICTMOBIL(R%)
                'ICTBROADDEM(R%) = ICTBROAD(R%)

                'ICTBROADMOBILDEM(R%) = ICTBROADMOBIL(R%)
                'End zzTakenOut----------------
                Dim RegionName = GRPS(1, R)
                sErrorSub = PrevsErrorSub + " " + RegionName + " Finance Post Govt Budget"
                '            GDPPCP(R%) = Amin(GDPPCP(R%), 500)

                INFRAINVESTNEWPUB(R%, InfraTot) = 0
                INFRAINVESTMAINTPUB(R%, InfraTot) = 0
                '   All but First Call 3:  Distribute Budget Allocation Across various Infrastructure
                '-------------------------------------------------------------------------------------
                'Note MTI: In most cases private sector is equivalently affected by public sector fund shortage

                'mti 2022/09/29 start
                'If R = 79 And IY > 70 Then Debug.Print IY; RegName(R%); "IFsInfra Call 3 a "; INFRAINVESTNEW(R%, SafeWaterHH); INFRAINVESTNEWBF(R%, SafeWaterHH); INFRAINVESTNEWPUB(R%, SafeWaterHH); INFRAINVESTNEWBF(R%, SafeWaterHH) * infrainvnewpubshrm(R%, SafeWaterHH); infrainvnewpubshrm(R%, SafeWaterHH)
                'mti 2022/09/29 End

                For j = 1 To NInfraType%
                    INFRAINVESTNEWPUB(R%, j) = INFRAINVESTNEW(R%, j) * infrainvnewpubshrm(R%, j)
                    INFRAINVESTMAINTPUB(R%, j) = INFRAINVESTMAINT(R%, j) * infrainvmaintpubshrm(R%, j)

                    INFRAINVESTNEWPUB(R%, InfraTot) = INFRAINVESTNEWPUB(R%, InfraTot) + INFRAINVESTNEWPUB(R%, j)
                    INFRAINVESTMAINTPUB(R%, InfraTot) = INFRAINVESTMAINTPUB(R%, InfraTot) + INFRAINVESTMAINTPUB(R%, j)
                Next

                'If RegName(R%) = "Vietnam" Then Stop

                If infrafinon = 1 Then
                    'Note MTI: this switch was usued in the early coding stage to be able to disable infrastructure finance model:
                    'It is not necessary now: should take off
                    'Also to make it work properly base need to be rebuilt

                    'total budget demand already calculated at Sociopol (IFsIntlPol10s
                    Dim InfraPublicFundDemand = INFRABUDDEMNEW(R%, InfraTot) + INFRABUDDEMMNT(R%, InfraTot)
                    '       If RegName(R%) = "China" Then Stop
                    'adjust with (moving) adjustment ratio
                    InfraPublicFundDemand = InfraPublicFundDemand * InfraCostGDSInfraRI(R%)  'initial ratio

                    Dim InfraBudImpact As Single?
                    '------- Boxcar used to prevent upsurge in infrastructure spending: boxcar saves part of surge and releases gradually
                    If gdsnormalize <> 0 Then 'Decativate boxcar for freemoney scenario
                        'gdsnormalize=1 in the base case; making it 0 would mean none of the public expenditure sectors are constrained
                        'Base need to be rebuilt to see the impact of changes in gdsnormalize

                        'Impose a limit on spending growth
                        Dim InfraSpndIncreaseLim As Single
                        If gdsm(R%, Infra) <= 1 Then
                            InfraSpndIncreaseLim = 0.005  '% of GDP
                        Else
                            InfraSpndIncreaseLim = 1 'no upper limit for a gdsm(infra) growth scenario
                        End If

                        'portion that can be unloaded from boxcar
                        Dim InfraSpndBoxUnloadFactor = 0.2 'factor

                        Dim GDSInfraAllocated = GDS(R%, Infra) / GDP(R%) '% of GDP
                        '               If RegName(R%) = "China" Then Stop
                        Dim GDSInfraWithLimit = Amin(sGDSInfraInvest(R%) + InfraSpndIncreaseLim, GDSInfraAllocated) '% of GDP
                        'if there were extra funds last year and if someof that extra funds could not be used in building infrastructure add that to box

                        'mti 2014/05/28 - start
                        '                         'cap surplus fund from exceeding demand by more than three quarters of a percent of GDP
                        '                         GDSInfraWithLimit = Amin(InfraPublicFundDemand / GDP(R%) + 0.0033, GDSInfraWithLimit)
                        '                         'mti 2014/05/28 - end

                        'mti 2022/10/20 - start
                        GDSInfraWithLimit = CSng(Amin(1.05 * InfraPublicFundDemand / GDP(R%), GDSInfraWithLimit))
                        'mti 2022/10/20 - end

                        Dim AddCarryOverFromLastYeartoBox = AMAX(0, ExtraFundTotal(R%) - ExtraFundUsed(R%))
                        GDSInfraBox(R%) = GDSInfraBox(R%) + AddCarryOverFromLastYeartoBox

                        Dim GetFromBox = AMAX(0, GDSInfraBox(R%) * InfraSpndBoxUnloadFactor)

                        Dim GDSInfraInvest = GDSInfraWithLimit * GDP(R%) + GetFromBox 'billion dollars

                        'put the unused portion of current year allocated to box ; unused to keep spending pattern smooth
                        Dim AddtoBox = AMAX(0, GDSInfraAllocated - GDSInfraWithLimit) * GDP(R%) 'billion dollars

                        'initialize extra fund containers for use this year
                        ExtraFundTotal(R%) = 0
                        TotalDemandFromCandidates(R%) = 0
                        ExtraFundUsed(R%) = 0

                        GDSInfraBox(R%) = CSng(AMAX(0F, GDSInfraBox(R%) + AddtoBox - GetFromBox))


                        sGDSInfraInvest(R%) = CSng(0.8 * sGDSInfraInvest(R%) + 0.2 * (GDSInfraInvest / GDP(R%)))  'used next year for determining the spending limit,0)
                        'there is a moving average on Budget Impact further down; that works only on downside though (while this works only on updside)


                        'budget impact: ratio of fund obtained and fund demanded: public only
                        InfraBudImpact = CSng(GDSInfraInvest / AMAX(0.0001F, InfraPublicFundDemand)) 'supply  by demand
                        'If IY = 29 And R = 95 Then Stop

                        'mti 2022/09/30 start
                        sInfraBudImpact_WatSanNew(R%) = CSng(InfraBudImpact)
                        'mti 2022/09/30 end

                        'mti 2022/10/20 start Redim sInfraBudImpactByType(NR%, NInfraType%)
                        For j = 1 To NInfraType%
                            sInfraBudImpactByType(R%, j) = CSng(InfraBudImpact)
                        Next j
                        'mti 2022/10/20 end

                        '   All but First Call 3:  Protection of base fund demand
                        '--------------------------------------------------------

                        '--- Protection of base fund demand to  make sure base infrastructure is not distorted by excessive demand for any one target

                        If InfraBudImpact < 0.99999999 Then 'do this only when there is a budget shortage
                            'Note MTI: possibility of transient? check 2012

                            'mti 2022/10/20 start
                            InfraBudgetAllocationWithShortage(R%, GDSInfraInvest, sInfraBudImpactByType, InfraBudImpact)
                            'mti 2022/10/20 End

                        End If


                    Else
                        '  no boxcar for freemoney scenario; and GDS(R%, Infra) is as much as was demanded
                        InfraBudImpact = GDS(R%, Infra) / AMAX(0.0001, InfraPublicFundDemand) 'supply  by demand

                        'mti 2022/10/20 start Redim sInfraBudImpactByType(NR%, NInfraType%)
                        For j = 1 To NInfraType%
                            sInfraBudImpactByType(R%, j) = CSng(InfraBudImpact)
                        Next j
                        'mti 2022/10/20 end

                    End If

                    'slow down the impact
                    '                 sInfraBudImpact(R%) = 0.8 * sInfraBudImpact(R%) + 0.2 * InfraBudImpact
                    sInfraBudImpact(R%) = If(InfraBudImpact, 0)   'mti October 03, 2011 -temporarily turning off budget impact moving average

                    'mti 2022/09/30 start
                    'Not updating sInfraBudImpact_WatSanNew(R%) here, to avoid picking -9999; Code above already computes sInfraBudImpact_WatSanNew
                    'mti 2022/09/30 end

                    'mti 2013/12/01 - comment out this
                    'Note MTI: using a unused Education Variable for saving values for debug purpose only:
                    'EDTERSUR(R%, 1) = sInfraBudImpact(R%)

                    '   All but First Call 3:  Adjustment of physical infrstructure construction and maintenance to reflect fund shortage
                    '--------------------------------------------------------------------------------------------------------------------
                    'Note MTI: Private sector is also affected equally in all but ICT sectors
                    '       If RegName(R%) = "Bangladesh" Then Stop
                    '--- There is budget shortage so infrastructure levels need to be adjusted
                    If sInfraBudImpact(R%) < 0.99999999 Then 'the condition below does not work for 1; so I replace 1 with a very close value .999999999999999
                        'mti April 05, 2012 -end

                        '----Roads Paved-----------

                        Dim PhysStckDmndThou As Double = LANDAREA(R%) * INFRAROAD(R%) * INFRAROADPAVEDPCNT(R%) / 100 'Demanded Connections (stock) in Thousands This Year =(Lines/100)x(Pop x 10000 x 100)/(1000)
                        Dim sPhysStckThou As Double = LANDAREA(R%) * SINFRAROAD(R%) * sinfraroadpavedpcnt(R%) / 100 'Connections (Stock) in Thousands Last Year

                        Dim InfraRoadPavedDemThousands = PhysStckDmndThou 'taotal paved roads demanded

                        'mti 2022/10/20 start  sInfraBudImpactByType
                        CalcInfraBudgetImpact(R%, PhysStckDmndThou, sPhysStckThou, INFRAROADPAVEDCOST(R%) * 1000, RoadPaved)
                        'mti 2022/10/20 End

                        Dim InfraRoadPavedFinThousands = PhysStckDmndThou 'final paved roads reflecting fund shortage


                        '----------- UnPaved Roads -----------
                        PhysStckDmndThou = LANDAREA(R%) * INFRAROAD(R%) * (100 - INFRAROADPAVEDPCNT(R%)) / 100  'Demanded Connections (stock) in Thousands This Year =(Lines/100)x(Pop x 10000 x 100)/(1000)
                        sPhysStckThou = LANDAREA(R%) * SINFRAROAD(R%) * (100 - sinfraroadpavedpcnt(R%)) / 100 'Connections (Stock) in Thousands Last Year

                        Dim UnPavedStayPut As Double = 0 'variable for holding a rough estimate of unpaved roads that could not be paved due to fund shortage

                        'dkb 2013/09/16 added dimension 1 to infraroadraisetar
                        If infraroadraitrgtyr(R%) > 0 Or infraroadraisetar(R%) > 0 Then 'make sure total roads won't go down for universal target or positive SE target
                            'this should be done for base csae as well? but only for increasing total roads

                            'this part is needed for a situation where target imposes fast paving of unpaved;
                            'and then there is a fund shortage
                            'without this total roads will go down in such a situation

                            Dim UnPaved2PavedConThou = AMAX(0, sPhysStckThou - PhysStckDmndThou) 'this is a crude measure of unpaved roads (attempted) to be converted to paved
                            Dim PavedAdditionCutBack = AMAX(0, InfraRoadPavedDemThousands - InfraRoadPavedFinThousands) 'total financial impact on paved
                            UnPavedStayPut = Amin(UnPaved2PavedConThou, PavedAdditionCutBack)
                        End If

                        PhysStckDmndThou = PhysStckDmndThou '+ UnPavedStayPut  '
                        sPhysStckThou = sPhysStckThou - UnPavedStayPut  'Note MTI: revisit this

                        'mti 2022/10/20 Start
                        CalcInfraBudgetImpact(R%, PhysStckDmndThou, sPhysStckThou, INFRAROADUNPAVEDCOST(R%) * 1000, RoadUnpaved)
                        'mti 2022/10/20 End

                        Dim InfraRoadUnPavedFinThousands = PhysStckDmndThou + UnPavedStayPut

                        '----------- Total Roads and road density post budget -----------
                        Dim InfraRoadTotalFinThousands = InfraRoadPavedFinThousands + InfraRoadUnPavedFinThousands
                        INFRAROAD(R%) = CSng(InfraRoadTotalFinThousands / LANDAREA(R%)) 'thousand km/mill ha = km/1000 ha

                        INFRAROADPAVEDPCNT(R%) = CSng(100 * InfraRoadPavedFinThousands / InfraRoadTotalFinThousands)


                        '   All but First Call 3:  Adjustment of electricity variables to reflect fund shortage
                        '----------------------------------------------------------------------------------------

                        'Electricity Generation Capacity -----------------------------------------------------

                        PhysStckDmndThou = CSng(INFRAELECGENCAP(R%) * CDbl(POP(R%)) * 1000) 'Demanded Connections (stock) in Thousands This Year
                        sPhysStckThou = CSng(sINFRAELECGENCAP(R%) * CDbl(sPOPTotal(R%)) * 1000) 'Connections (Stock) in Thousands Last Year

                        Dim ToAddThou = PhysStckDmndThou - sPhysStckThou

                        'mti 2022/10/20 start  sInfraBudImpactByType
                        CalcInfraBudgetImpact(R%, PhysStckDmndThou, sPhysStckThou, INFRAELECGENCOST(R%), ElectricityGen)
                        'mti 2022/10/20 End


                        INFRAELECGENCAP(R%) = CSng(AMAX(0.0001, PhysStckDmndThou / POP(R%) / 1000))


                        'Electricity Access  -----------------------------------------------------
                        If sPOPTotal(R%) - SPOPURBAN(R%) <> 0 Then
                            PhysStckDmndThou = CSng(INFRAELECACC(R%, Rural) * CDbl((sPOPTotal(R%) - SPOPURBAN(R%)) * 10 / HHSIZE(R%))) 'Demanded Connections (stock) in Thousands This Year
                            sPhysStckThou = CSng(sINFRAELECACC(R%, Rural) * CDbl(sPopTotal1(R%) - sPopUrban1(R%)) * 10 / sHHSIZE(R%)) 'Connections (Stock) in Thousands Last Year

                            'mti 2022/10/20 start  sInfraBudImpactByType
                            CalcInfraBudgetImpact(R%, PhysStckDmndThou, sPhysStckThou, INFRAELECACCRURALCOST(R%), ElectricityAccRural)
                            'mti 2022/10/20 End


                            INFRAELECACC(R%, Rural) = CSng(AMAX(0.0001, PhysStckDmndThou * HHSIZE(R%) / (sPOPTotal(R%) - SPOPURBAN(R%)) / 10))

                        End If

                        'If RegName(R%) = "Bangladesh" Then Stop

                        PhysStckDmndThou = CSng(INFRAELECACC(R%, Urban) * CDbl(SPOPURBAN(R%) * 10 / HHSIZE(R%))) 'Demanded Connections (stock) in Thousands This Year
                        sPhysStckThou = CSng(sINFRAELECACC(R%, Urban) * CDbl(sPopUrban1(R%)) * 10 / sHHSIZE(R%)) 'Connections (Stock) in Thousands Last Year

                        'mti 2022/10/20 start  sInfraBudImpactByType
                        CalcInfraBudgetImpact(R%, PhysStckDmndThou, sPhysStckThou, INFRAELECACCURBANCOST(R%), ElectricityAccUrban)
                        'mti 2022/10/20 End


                        INFRAELECACC(R%, Urban) = CSng(AMAX(0.0001, PhysStckDmndThou * HHSIZE(R%) / SPOPURBAN(R%) / 10))

                        'mti: using current year POPURBAN gives a run to run here (using previous year): needs to revisit the sequence of calls
                        INFRAELECACC(R%, Total) = (SPOPURBAN(R%) * INFRAELECACC(R%, Urban) + (sPOPTotal(R%) - SPOPURBAN(R%)) * INFRAELECACC(R%, Rural)) / sPOPTotal(R%)


                        '   All but First Call 3:  Adjustment of ICT variables to reflect fund shortage
                        '----------------------------------------------------------------------------------------

                        'ICT Telephone -----------------------------------------------------

                        'mti February 2011 Infarstructure -start
                        PhysStckDmndThou = INFRATELE(R%) * CDbl(POP(R%)) * 10 'Demanded Connections (stock) in Thousands This Year =(Lines/100)x(Pop x 10000 x 100)/(1000)
                        sPhysStckThou = SINFRATELE(R%) * CDbl(sPOPTotal(R%)) * 10 'Connections (Stock) in Thousands Last Year

                        'mti 2022/10/20 start  sInfraBudImpactByType
                        CalcInfraBudgetImpact(R%, PhysStckDmndThou, sPhysStckThou, INFRATELECOST(R%), Telephone)
                        'mti 2022/10/20 End

                        INFRATELE(R%) = CSng(PhysStckDmndThou / POP(R%) / 10)


                        'ICT Mobile Phones -----------------------------------------------------
                        PhysStckDmndThou = ICTMOBIL(R%) * CDbl(POP(R%)) * 10 'Demanded Connections (stock) in Thousands This Year
                        Dim sICTMobilThousands = SICTMobil(R%) * CDbl(sPOPTotal(R%)) * 10 'Connections (Stock) in Thousands Last Year

                        'mti 2022/10/20 start  sInfraBudImpactByType
                        CalcInfraBudgetImpact(R%, PhysStckDmndThou, CDbl(sICTMobilThousands), ICTMOBILCOST(R%), Mobile, True)
                        'mti 2022/10/20 End

                        ICTMOBIL(R%) = CSng(PhysStckDmndThou / POP(R%) / 10)


                        'ICT BroadBand Fixed -----------------------------------------------------

                        PhysStckDmndThou = CSng(ICTBROAD(R%) * CDbl(POP(R%)) * 10) 'Demanded Connections (stock) in Thousands This Year
                        Dim sICTBroadThousands = CSng(SICTBroad(R%) * CDbl(sPOPTotal(R%)) * 10) 'Connections (Stock) in Thousands Last Year

                        'mti 2022/10/20 start
                        CalcInfraBudgetImpact(R%, PhysStckDmndThou, CDbl(sICTBroadThousands), ICTBROADCOST(R%), Broadband, True)
                        'mti 2022/10/20 End

                        ICTBROAD(R%) = CSng(PhysStckDmndThou / POP(R%) / 10)

                        'ICT Broadband Mobile -----------------------------------------------------

                        PhysStckDmndThou = CSng(ICTBROADMOBIL(R%) * CDbl(POP(R%)) * 10) 'Demanded Connections (stock) in Thousands This Year
                        Dim sICTBroadMobilThousands = SICTBroadMobil(R%) * CDbl(sPOPTotal(R%)) * 10 'Connections (Stock) in Thousands Last Year

                        'mti 2022/10/20 start  sInfraBudImpactByType
                        CalcInfraBudgetImpact(R%, PhysStckDmndThou, CDbl(sICTBroadMobilThousands), ICTBROADMOBILCOST(R%), BroadbandMobile, True)
                        'mti 2022/10/20 End

                        ICTBROADMOBIL(R%) = CSng(PhysStckDmndThou / POP(R%) / 10)
                        'mti April 2011 - end

                        'Irrigation -----------------------------------------------------

                        PhysStckDmndThou = LANDIRAREAEQUIP(R%)

                        'mti 2022/10/20 start  sInfraBudImpactByType
                        CalcInfraBudgetImpact(R%, PhysStckDmndThou, CDbl(slandirareaequip(R%)), LANDIRCOST(R%), Irrigation)
                        'mti 2022/10/20 End

                        LANDIRAREAEQUIP(R%) = CSng(PhysStckDmndThou)
                        'mti March 2011 - end

                        'mti June 04, 2012 -start
                    ElseIf sInfraBudImpact(R%) > 1 Then

                        '   All but First Call 3:  Boost infrastructure variables to reflect fund surplus
                        '----------------------------------------------------------------------------------------
                        Call HandleExtraFundNonWatSan(R%, iInfraInvestNewPubShr(R%, RoadPaved), iInfraInvestNewPubShr(R%, RoadUnpaved), iInfraInvestNewPubShr(R%, ElectricityGen))

                    End If 'budimpact <1
                End If  'budimpacton
                '   All but First Call 3:  Road Accesss Index: Recalculation to reflect road changes from fund shortage/surplus
                '--------------------------------------------------------------------------------------------------------------

                IncomeDensityLand = (sgdpp(R%) * 1000 / AMAX(LANDAREA(R%), 0.0001F)) '$Intl/ha
                'jrs 2013/09/09 TotalRoadsDensityPop = INFRAROAD(R%) * LANDAREA(R%) * 1000 / AMAX(sPOPTotal(R%), 0.000001) ' INFRAROAD(R%) [km/1000 ha] ; LANDAREA(R%) [1000000 ha] ; POP [million ppeople]
                TotalRoadsDensityPop = AMAX(0.000001F, INFRAROAD(R%)) * LANDAREA(R%) * 1000 / AMAX(sPOPTotal(R%), 0.000001F)
                infraroadraicalc = CSng(100 * Math.Exp(-3.55820835391394 + 0.132781679654008 * Math.Log(CDbl(IncomeDensityLand)) + 0.239067182377656 * Math.Log(CDbl(TotalRoadsDensityPop))))
                Dim shiftadded As Single
                If INFRAROADRAIShift(R%) > 0 Then
                    shiftadded = ConvergeOverTime(INFRAROADRAIShift(R%), 0, 500)
                Else
                    shiftadded = ConvergeOverTime(INFRAROADRAIShift(R%), 0, 100)
                End If

                INFRAROADRAI(R%) = infraroadraicalc + shiftadded '+ EffectSE

                'RBound = AMAX(trgtINFRAROADRAI(R%), sINFRAROADRAI(R%))
                Dim RBound = AMAX(100, sINFRAROADRAI(R%))
                INFRAROADRAI(R%) = AMAX(0.001, Amin(RBound, INFRAROADRAI(R%)))

                '------ Total roads -------------------
                INFRAROADTOTAL(R%) = LANDAREA(R%) * INFRAROAD(R%) * 1000
                '   All but First Call 3:  Solid Fuel Use
                '----------------------------------------

                'jrs 2010/01/08 :Note MTI: original code by JRS: DSR changed education driver to electricity access
                If ensolflhldsw = 0 Then
                    'jrs 2013/12/20 *** begin ***
                    'revised formulation by DSR added POPURBAN
                    'Call AnalFunc("Percent of People using Primarily Solid Fuels Revised", _
                    'Array(GDPPCP(R%), INFRAELECACC(R%, Total)), cVal)
                    'DSR 2014/02/12 corrected to use urban percentage not urban population
                    'bbh 2016/08/28 start; constraint on GDPPCP to keep AnalFunc from  blowing up at about $700,000
                    Dim IndVarConstrained = Amin(GDPPCP(R%), 500)
                    Call AnalFunc("Percent of People using Primarily Solid Fuels (Full Model 2010)",
                New Single() {IndVarConstrained, INFRAELECACC(R%, Total), 100 * SPOPURBAN(R%) / POP(R%)}, cVal,
                New String() {"GDPPCP", "INFRAELECACC", "PopUrban%"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                    'Call AnalFunc("Percent of People using Primarily Solid Fuels (Full Model 2010)", _
                    '    Array(GDPPCP(R%), INFRAELECACC(R%, Total), 100 * SPOPURBAN(R%) / POP(R%)), cVal, _
                    'Array("GDPPCP", "INFRAELECACC", "PopUrban%")) 'jrs 2014/10/09 added DepVar Names to check order
                    'bbh 2016/08/28 end
                    cVal = cVal * -1
                    'jrs 2013/12/20 *** end ***


                    'jrs 2011/12/02 added check for Exp(709) max value
                    If cVal > 709 Then
                        Prediction = 0
                    Else
                        Prediction = CSng(100 / (Math.Exp(cVal) + 1))
                    End If

                    'SE approach - multiplicative : SE value from DSR
                    'dkb 2013/09/16 added R dimention to ensolfuelsetar and ensolfuelseyrtar
                    'jrs 2013/12/19 *** begin ***
                    'EnSolFuelSE = 16.4
                    Dim EnSolFuelSE As Single = 14.5
                    Dim EffectSE As Single
                    'jrs 2013/12/19 *** end ***
                    If ensolfuelsetar(R%) <> 0 And ensolfueltrgtyr(R%) = 0 Then
                        EffectSE = StdErrorMultShift(CSng(EnSolFuelSE), CSng(Prediction), CSng(ENSOLFUELShift(R%)), CSng(ENSOLFUELShift(R%)),
                        CSng(ensolfuelsetar(R%)), CInt(FirstYear + ensolfuelseyrtar(R%)), FirstYear + IY - CShort(1), CSng(ENSOLFUELI(R%)), FirstYear)
                        'SE , Prediction, DataMultShift, IDataMultShift, TargetSEs, TargetYr, currentYr, IData
                    Else
                        EffectSE = 1
                    End If
                    ENSOLFUEL(R%) = Prediction * ENSOLFUELShift(R%) * EffectSE

                    'SE approach - multiplicative

                    '                'Universal Target - taking solid fuel use to 0
                    '                If ensolfueltrgtyr(R%) > 0 And ensolfuelsetar(R%) = 0 Then
                    '
                    '                    ENSOLFUELTrgt = ENSOLFUELI(R%) - Amin(ensolfueltrgtyr(R%), IY - 1) * ENSOLFUELI(R%) / ensolfueltrgtyr(R%)
                    '                    ENSOLFUEL(R%) = Amin(ENSOLFUELTrgt, ENSOLFUEL(R%))
                    '                End If

                    'DSR 20 Sep 2013 - Start
                    'Universal Target - taking solid fuel use to 0
                    '                If ensolfueltrgtyr(R%) > 0 And ensolfuelsetar = 0 Then
                    '
                    '                    ENSOLFUELTrgt = ENSOLFUELI(R%) - Amin(ensolfueltrgtyr(R%), IY - 1) * ENSOLFUELI(R%) / ensolfueltrgtyr(R%)
                    '                    ENSOLFUEL(R%) = Amin(ENSOLFUELTrgt, ENSOLFUEL(R%))
                    '                End If
                    If ensolfueltrgtyr(R%) > 0 And ensolfuelsetar(R%) = 0 Then
                        Dim ensolfueltrgtvalTmp = Amin(ensolfueltrgtval(R%), ENSOLFUELI(R%))
                        Dim ENSOLFUELTrgt = ENSOLFUELI(R%) - Amin(ensolfueltrgtyr(R%), IY - 1) * (ENSOLFUELI(R%) - ensolfueltrgtvalTmp) / ensolfueltrgtyr(R%)
                        ENSOLFUEL(R%) = Amin(ENSOLFUELTrgt, ENSOLFUEL(R%))
                    End If
                    'DSR 20 Sep 2013 - End


                    ENSOLFUEL(R%) = ENSOLFUEL(R%) * ensolfuelm(R%)
                    ENSOLFUEL(R%) = Amin(AMAX(ENSOLFUEL(R%), 0), 100)
                Else 'if Solid Fueld Hold switch is On
                    ENSOLFUEL(R%) = ENSOLFUELI(R%)
                End If 'ensolflhldsw = 0
                sENSOLFUEL(R%) = ENSOLFUEL(R%)


                '   All but First Call 3:  Save values for later use
                '--------------------------------------------------

                SINFRAELEC(R%) = INFRAELEC(R%)

                SINFRAROAD(R%) = INFRAROAD(R%)


                sinfraroadpavedpcnt(R%) = INFRAROADPAVEDPCNT(R%)

                SINFRANET(R%) = INFRANET(R%)

                SSINFRATELE(R%) = SINFRATELE(R%)
                SINFRATELE(R%) = INFRATELE(R%)
                SINFRATELEOLD(R%) = INFRATELEOLD(R%)

                INFRATELEMin(R%) = AMAX(INFRATELEMin(R%), INFRATELEOLD(R%))

                sINFRAELECGENCAP(R%) = INFRAELECGENCAP(R%)

                sInfraElecGenCapTot(R%) = INFRAELECGENCAP(R%) * POP(R%)


                SICTMobil(R%) = ICTMOBIL(R%)
                SICTBroad(R%) = ICTBROAD(R%)

                SICTBroadMobil(R%) = ICTBROADMOBIL(R%)

                'sgh 2016/07/05
                'sgh 2018/01/04 - START
                'If slandirareaequip(R%) > LANDIRAREAEQUIP(R%) Then
                '    LANDIRAREAEQUIP(R%) = slandirareaequip(R%)
                'End If
                'sgh 2018/01/04 - END
                'sgh 2016/07/05

                slandirareaequip(R%) = LANDIRAREAEQUIP(R%)

                sHHSIZE(R%) = HHSIZE(R%)

                'save energy demand etc for next year
                SEnDem(R%) = ENDEM(R%)
                sENDEMSH(R%) = ENDEMSH(NR%)

                For loccnt% = 1 To NLocation%
                    ssINFRAELECACC(R%, loccnt%) = sINFRAELECACC(R%, loccnt%)
                    sINFRAELECACC(R%, loccnt%) = INFRAELECACC(R%, loccnt%)
                Next

                sENELECSHRENDEM(R%) = ENELECSHRENDEM(R%)
                sINFRAROADRAI(R%) = INFRAROADRAI(R%)

                'Save GDP per capita for next year -
                sGDPPCP(R%) = GDPPCP(R%)

                'save gdp and landarea for use next year
                sgdpp(R%) = GDPP(R%)
                sLANDAREA(R%) = LANDAREA(R%)
            Next 'R%

            SubAllYearsForIFsInfra(context)
        End Sub

        Public Sub IFsInfra1AllbutFirstYear(context As IForecastContext)
            'AllButFirst:  'block for all years but first year (exception: call2 handles exlectricity first year and infrastructure index calculation first year)
            'ttt- ReDim
            ReDim INFRAINVESTNEWBF(NR%, NInfraType%)
            ReDim INFRAINVESTMAINTBF(NR%, NInfraType%)
            'jrs 2019/10/22
            Array.Copy(sLANDAREA, LANDAREA, NR + 1)
            'jrs 2019/11/25
            'jrs 2019/11/26 Array.Copy(ilandirareasat, landirareasat, NR + 1)

            For R% = 1 To NR%

                'sErrorSub = "IFsInfra" & CStr(IFUN) & "-" & RegName(R%)

                '   All but First Call 1:  ICT Spending as Pct. of GDP
                ' ---------------------------------------------------
                'Note MTI: came from JRS old ICT model: need to conform with new ICT spending

                'jrs 2009/05/18
                Dim rVal As Single = 0
                If context.HistoricMode Then
                    TbName$ = "GDP/Capita (PPP 2000) Versus ICT Expenditures % GDP (2005) Linear"
                    Call XYTABL(TbName$, GDPPCP(R), rVal, context)
                Else
                    'jrs 2024/06/27 TbName$ = "GDP/Capita (PPP 2000) Versus ICT Expenditures % GDP (" & CStr(BaseYear) & ") Linear"
                    'TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
                    Call XYTABL(TFNameICTExpendPctGDP, GDPPCP(R), rVal, context)
                End If
                ICTSPEND(R%) = rVal + SPENDshift(R%)
                'applying multiplier
                ICTSPEND(R%) = ICTSPEND(R%) * ictspendm(R%)
                ICTSPEND(R%) = AMAX(Amin(ICTSPEND(R%), 100), 0)

            Next R%
            '   All but First Call 1:  Access to Mobile phones
            ' ------------------------------------------------
            ICTMobilForecast(SGOVREGQUAL, sGDPPCP, sPOPTotal, SMobil, ICTMobilGR, ICTMobilBF,
                             sICTMobilBF, sMobilBF, ICTMobilGRBF, INFRAINVESTNEWBF, INFRAINVESTMAINTBF,
                             Deppcnt(Mobile), SICTMobil, context)
            '   All but First Call 1:  Access to Fixed Broadband
            ' --------------------------------------------------

            ICTFixedBBForecast(SGOVREGQUAL, sGDPPCP, sPOPTotal, SBroad,
                               ICTBroadGR, ICTBroadBF, sICTBroadBF, sBroadBF, ICTBroadGRBF,
                               INFRAINVESTNEWBF, INFRAINVESTMAINTBF, Deppcnt(Broadband),
                               SICTBroad, SSINFRATELE, SINFRATELE, iICTBroad, ictbroadtrgtyr, ictbroadtrgtval, context)


            'mti 2013/12/20 start
            If context.HistoricMode AndAlso context.FirstYearHistory < 1990 Then

                For R% = 1 To NR%
                    ICTBROADMOBIL(R) = 0
                    ICTBroadMobilGR(R) = 0

                    ICTBROADMOBILCOST(R%) = 0
                    INFRAINVESTNEW(R%, BroadbandMobile) = 0
                    INFRAINVESTMAINT(R%, BroadbandMobile) = 0

                    ICTBroadMobilBF(R%) = 0
                    ICTBroadMobilGRBF(R%) = 0
                    INFRAINVESTNEWBF(R%, BroadbandMobile) = 0
                    INFRAINVESTMAINTBF(R%, BroadbandMobile) = 0
                Next

            Else
                'mti 2013/12/20 end
                Dim rVal As Single = 0
                Dim SE As Single = 0
                Dim EffectSE As Single = 0
                Dim incCS As Single = 0
                Dim incTec As Single = 0
                Dim incTot As Single = 0
                Dim gratem As Single = 0
                Dim gratec As Single = 0
                Dim incCSBF As Single = 0
                Dim incTecBF As Single = 0
                Dim incTotBF As Single = 0
                Dim gratemBF As Single = 0
                Dim gratecBF As Single = 0
                Dim BroadMobilAdd As Single = 0
                'BroadMobilAdd
                For R% = 1 To NR%

                    '   All but First Call 1:  Access to Mobile Broadband
                    ' --------------------------------------------------

                    Call AnalFuncSE("ICT Broadband Mobile Access % of Population Rev 2010",
                                New Single() {GDPPCP(R%), SGOVREGQUAL(R%)}, rVal, SE,
                               New String() {"GDP2005PCPPP(MOSTRECENT)/ 1000 (Log)", "GovernanceRegQual(MOSTRECENT)+ 2.5 (Linear)"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                    'Convergence of Cross-sectional
                    '-----------------------------------

                    If BroadMOBILshift(R) > 0 Then
                        BroadMobilAdd = BroadMOBILshift(R%)
                    Else
                        BroadMobilAdd = ConvergeOverTime(BroadMOBILshift(R%), 0, 100)
                    End If

                    '-- SE target --
                    'dkb 2013/09/16 added dimension 1 to ictbroadmobilsetar and ictbroadmobiltrgtyr
                    If ictbroadmobilsetar(R%) <> 0 And ictbroadmobiltrgtyr(R%) = 0 Then
                        EffectSE = StdErrorAdditiveShift(CSng(SE), CSng(rVal), CSng(BroadMobilAdd), CSng(BroadMOBILshift(R)),
                        CSng(ictbroadmobilsetar(R%)), CInt(FirstYear + ictbroadmobilseyrtar(R%)), FirstYear + IY - 1, FirstYear)
                        'SE, Prediction, DataAddShift, IDataAddShift, TargetSEs, TargetYr, CurrentYr
                    Else
                        EffectSE = 0
                    End If

                    'mti note: JRS original model did not have upper and lower bounds here; Check if that makes a difference
                    ICTBROADMOBIL(R%) = rVal + BroadMobilAdd + EffectSE

                    'mti 2013/09/16 - Infra Fix - start
                    ICTBROADMOBIL(R%) = CSng(AMAX(ICTBROADMOBIL(R%), 0.0001))
                    'mti 2013/09/16 - Infra Fix - end

                    'Growth from cross-sectional function---------
                    '-----------------------------------------------------
                    'jrs 2010/12/06 *** begin: Using Annual Growth Rate ***
                    '******************************************************
                    'Scaling CS change depending Data/Function rate
                    If rVal > SBroadMobil(R, 2) Then
                        incCS = (ICTBROADMOBIL(R) - SBroadMobil(R, 1)) * SBroadMobil(R, 2) / Math.Abs(rVal)
                    Else
                        incCS = ICTBROADMOBIL(R) - SBroadMobil(R, 1)
                    End If


                    ' Tech Shift Growth
                    '-----------------------------------------------------

                    If SBroadMobil(R, 2) < ictbroadmobiltecinflection Then
                        incTec = (ictbroadmobiltechighrt(R%) - ictbroadmobilteclowrt) * SBroadMobil(R, 2) / ictbroadmobiltecinflection + ictbroadmobilteclowrt
                    Else
                        'jrs 2010/12/06 incTec = (ictmobilteclowrt - ictmobiltechighrt) * (SMobil(R, 2) - ictmobiltecinflection) / (120 - ictmobiltecinflection) + ictmobiltechighrt

                        'mti June 08, 2011 - start
                        'incTec = (ictbroadmobilteclowrt - ictbroadmobiltechighrt) * (SBroadMobil(R, 2) - ictbroadmobiltecinflection) / (ictbroadmobilsaturation - ictbroadmobiltecinflection) + ictbroadmobiltechighrt
                        incTec = (ictbroadmobilteclowrt - ictbroadmobiltechighrt(R%)) * (SBroadMobil(R, 2) - ictbroadmobiltecinflection) / (ictmobilsaturation + 10 - ictbroadmobiltecinflection) + ictbroadmobiltechighrt(R%)
                        'mti June 08, 2011 - end
                    End If
                    SBroadMobil(R, 1) = ICTBROADMOBIL(R)

                    'jrs 2010/12/06
                    If Not (ICTBroadMobilGR(R).HasValue) Then
                        incTot = incTec + incCS
                    Else
                        If IY > 2 Then
                            'mti June 08, 2011 - start
                            '                    HistWg = 0.5
                            '                    'incTot = HistWg * ICTMobilGR(R) * SMobil(R, 2) + (1 - HistWg) * (incTec + incCS)
                            '                    'Instead of using the compoundig Growth Rate, use just the flat differece between the 2 last years
                            '                    'F(y-1) - F(y-2) = F(y-1) * (1 - 1/(1+GR))
                            '                    'incTot = HistWg * SMobil(R, 2) * (1 - 1 / (1 + ICTMobilGR(R))) + (1 - HistWg) * (incTec + incCS)
                            '
                            '                    incTot = ConvergeOverTime(SBroadMobil(R, 2) * (1 - 1 / (1 + ICTBroadMobilGR(R))), incTec + incCS, 10)
                            'new formulation (growth rate convergence, slower)
                            gratem = (incTec + incCS) / AMAX(0.01, SBroadMobil(R, 2)) 'growth rate from model
                            gratec = ConvergeOverTime(CSng(ICTBroadMobilGR(R)), gratem, 25) 'growth rate from previous year converge to that from model
                            incTot = gratec * SBroadMobil(R, 2)
                            'this branch needed for year 2 when pre-processor calculates % point or absolute growth; for years >2 ICTBroadMobilGR(R) converts to growth rate (see below)
                        Else
                            '                    'revert to growth rate as opposed to increment
                            '                    incTot = ICTBroadMobilGR(R) * SBroadMobil(R, 2)
                            gratem = incTec + incCS
                            gratec = ConvergeOverTime(CSng(ICTBroadMobilGR(R)), gratem, 5)
                            incTot = gratec
                            '
                        End If
                    End If

                    'Add Increase/Decrease to final value, this is the tec factor
                    'Total increment can't be more than 25% of the remaining gap to reach target of 120
                    ICTBROADMOBIL(R) = SBroadMobil(R, 2) + Amin(incTot, (ictmobilsaturation + 10 - SBroadMobil(R, 2)) / 4)

                    'same saturation for mobile and broadband
                    If ICTBROADMOBIL(R) > ictmobilsaturation And IY > 5 Then
                        If IY > 15 Then
                            ICTBROADMOBIL(R) = ConvergeOverTime(ICTBROADMOBIL(R), ictmobilsaturation, 96)
                        ElseIf IY > 10 Then
                            ICTBROADMOBIL(R) = ConvergeOverTime(ICTBROADMOBIL(R), ictmobilsaturation, 150)
                        Else
                            ICTBROADMOBIL(R) = ConvergeOverTime(ICTBROADMOBIL(R), ictmobilsaturation, 200)
                        End If
                    End If

                    'jrs 2011/01/17 begin Using Annual Growth Rate
                    If ICTBroadMobilGR(R).HasValue Then
                        If SBroadMobil(R, 2) <> 0 Then
                            ICTBroadMobilGR(R) = Amin(1.3F, ICTBROADMOBIL(R) / SBroadMobil(R, 2) - 1)
                        End If
                    End If

                    'jrs 2011/01/17 end Using Annual Growth Rate
                    SBroadMobil(R, 2) = ICTBROADMOBIL(R)


                    'Universal target
                    'dkb 2013/09/16 added dimension 1 to ictbroadmobilsetar
                    If ictbroadmobiltrgtyr(R%) > 0 And ictbroadmobilsetar(R%) = 0 Then
                        Dim ICTBROADMOBILTrgt = Amin(ictmobilsaturation, iICTBROADMOBIL(R%) + Amin(ictbroadmobiltrgtyr(R%), IY - 1) * (ictbroadmobiltrgtval(R%) - iICTBROADMOBIL(R%)) / ictbroadmobiltrgtyr(R%))
                        ICTBROADMOBIL(R%) = AMAX(ICTBROADMOBIL(R%), ICTBROADMOBILTrgt)

                        'adjust n=mobile to be consistent with mobile BB
                        If ICTMOBIL(R%) < ICTBROADMOBIL(R%) Then
                            ICTMOBIL(R%) = ICTBROADMOBIL(R%)
                            SMobil(R, 2) = ICTMOBIL(R)
                            Dim ICTMobilDemThousands = ICTMOBIL(R%) * CDbl(POP(R%)) * 10 'Demanded Connections (stock) in Thousands This Year
                            Dim sICTMobilThousands = SICTMobil(R%) * CDbl(sPOPTotal(R%)) * 10 'Connections (Stock) in Thousands Last Year

                            CalcInfraInvest(CDbl(ICTMobilDemThousands), CDbl(sICTMobilThousands),
                                            Deppcnt(Mobile), ICTMOBILCOST(R%), INFRAINVESTNEW(R%, Mobile), INFRAINVESTMAINT(R%, Mobile))


                        End If

                    End If


                    ICTBROADMOBIL(R%) = ICTBROADMOBIL(R%) * ictbroadmobilm(R%)


                    'mti 2014/08/20 - start
                    '                'mti 2014/05/28 - start
                    '                'See if curve has shifted up by extra money in the previous year
                    '                ICTBROADMobilShiftPrevYear = AMAX(0, (SICTBroadMobil(R) - SBroadMobil(R, 3))) 'percent point
                    '                'Save pre-finance value:
                    '                SBroadMobil(R, 3) = ICTBROADMOBIL(R)
                    '                'might be an issue in a multiplier scenario; as SICTBROAD will have multiplier effect; and SBroad(R, 3)  is saved pre-multiplier
                    '
                    '    '      If RegName(R%) = "Saudi Arabia" And IY >= 7 Then Stop
                    '
                    '                'In case curve has shifted up (by extra money in the previous year, demand signal needs to be sent so that it could continue to stay there
                    '                ICTBROADMOBIL(R%) = ICTBROADMOBIL(R%) + ICTBROADMobilShiftPrevYear
                    '                'mti 2014/05/28 - end
                    'mti 2014/08/20 - end


                    'already built infrastructure should not go down unless there is a hard multiplier infraroadm(R%, Urban)
                    If ictbroadmobilm(R%) >= 1 And ictbroadmobiltrgtyr(R%) > 0 Then
                        ICTBROADMOBIL(R) = AMAX(ICTBROADMOBIL(R%), SICTBroadMobil(R))
                    End If
                    'Note MTI: unlike other infrastructure there is no hard parameter to check against here

                    ICTBROADMOBIL(R%) = Amin(ICTBROADMOBIL(R%), ICTMOBIL(R%))

                    'mti 2013/09/16 - Infra Fix - start
                    ICTBROADMOBIL(R%) = CSng(AMAX(ICTBROADMOBIL(R%), 0.0001))
                    'mti 2013/09/16 - Infra Fix - end

                    '   All but First Call 1:  Mobile Broadband Finance
                    ' --------------------------------------------------


                    CalcInfraUnitCost(R%, 10, 5, ictbroadmobilcostupper, ictbroadmobilcostlower, sGDPPCP(R%), ICTBROADMOBILCOST(R%))
                    ICTBROADMOBILCOST(R%) = ICTBROADMOBILCOST(R%) * ictbroadmobilcostm(R%)

                    Dim ICTBroadMobilDemThousands = ICTBROADMOBIL(R%) * CDbl(POP(R%)) * 10 'Demanded Connections (stock) in Thousands This Year
                    Dim sICTBroadMobilThousands = SICTBroadMobil(R%) * CDbl(sPOPTotal(R%)) * 10 'Connections (Stock) in Thousands Last Year

                    CalcInfraInvest(CDbl(ICTBroadMobilDemThousands), CDbl(sICTBroadMobilThousands),
                                    Deppcnt(BroadbandMobile), ICTBROADMOBILCOST(R%), INFRAINVESTNEW(R%, BroadbandMobile), INFRAINVESTMAINT(R%, BroadbandMobile))


                    '----------base forecast needed for base spending (pre-targdet) in  target scenario case----------
                    'dkb 2013/09/16 added dimension 1 to ictbroadmobilsetar
                    If ictbroadmobiltrgtyr(R%) > 0 Or ictbroadmobilsetar(R%) > 0 Then             'for postive setar only
                        '--------- base forecast physical ----------
                        sICTBroadMobilBF(R) = ICTBroadMobilBF(R)
                        ICTBroadMobilBF(R) = rVal + BroadMobilAdd

                        If rVal > SBroadMobilBF(R, 2) Then
                            incCSBF = (ICTBroadMobilBF(R) - SBroadMobilBF(R, 1)) * SBroadMobilBF(R, 2) / Math.Abs(rVal)
                        Else
                            incCSBF = ICTBroadMobilBF(R) - SBroadMobilBF(R, 1)
                        End If


                        If SBroadMobilBF(R, 2) < ictbroadmobiltecinflection Then
                            incTecBF = (ictbroadmobiltechighrt(R%) - ictbroadmobilteclowrt) * SBroadMobilBF(R, 2) / ictbroadmobiltecinflection + ictbroadmobilteclowrt
                        Else
                            incTecBF = (ictbroadmobilteclowrt - ictbroadmobiltechighrt(R%)) * (SBroadMobilBF(R, 2) - ictbroadmobiltecinflection) / (ictmobilsaturation + 10 - ictbroadmobiltecinflection) + ictbroadmobiltechighrt(R%)
                        End If
                        SBroadMobilBF(R, 1) = ICTBroadMobilBF(R)

                        If Not (ICTBroadMobilGRBF(R).HasValue) Then
                            incTotBF = incTecBF + incCSBF
                        Else
                            If IY > 2 Then
                                gratemBF = (incTecBF + incCSBF) / AMAX(0.01, SBroadMobilBF(R, 2)) 'growth rate from model
                                gratecBF = ConvergeOverTime(CSng(ICTBroadMobilGRBF(R)), gratemBF, 25) 'growth rate from previous year converge to that from model
                                incTotBF = gratecBF * SBroadMobilBF(R, 2)
                            Else
                                gratemBF = incTecBF + incCSBF
                                gratecBF = ConvergeOverTime(CSng(ICTBroadMobilGRBF(R)), gratemBF, 5)
                                incTotBF = gratecBF
                            End If
                        End If

                        ICTBroadMobilBF(R) = SBroadMobilBF(R, 2) + Amin(incTotBF, (ictmobilsaturation + 10 - SBroadMobilBF(R, 2)) / 4)

                        If ICTBroadMobilBF(R) > ictmobilsaturation And IY > 5 Then
                            If IY > 15 Then
                                ICTBroadMobilBF(R) = ConvergeOverTime(ICTBroadMobilBF(R), ictmobilsaturation, 96)
                            ElseIf IY > 10 Then
                                ICTBroadMobilBF(R) = ConvergeOverTime(ICTBroadMobilBF(R), ictmobilsaturation, 150)
                            Else
                                ICTBroadMobilBF(R) = ConvergeOverTime(ICTBroadMobilBF(R), ictmobilsaturation, 200)
                            End If
                        End If

                        If ICTBroadMobilGRBF(R).HasValue Then
                            If SBroadMobilBF(R, 2) <> 0 Then
                                ICTBroadMobilGRBF(R) = Amin(1.3F, ICTBroadMobilBF(R) / SBroadMobilBF(R, 2) - 1)
                            End If
                        End If

                        SBroadMobilBF(R, 2) = ICTBroadMobilBF(R)
                        '-----------------------------------------


                        '--------- base forecast financial need (to be protected in a target scenario) ------------
                        ICTBroadMobilDemThousands = ICTBroadMobilBF(R%) * CDbl(POP(R%)) * 10 'Demanded Connections (stock) in Thousands This Year
                        sICTBroadMobilThousands = SICTBroadMobil(R%) * CDbl(sPOPTotal(R%)) * 10 'Connections (Stock) in Thousands Last Year

                        CalcInfraInvest(CDbl(ICTBroadMobilDemThousands), CDbl(sICTBroadMobilThousands),
                                        Deppcnt(BroadbandMobile), ICTBROADMOBILCOST(R%), INFRAINVESTNEWBF(R%, BroadbandMobile), INFRAINVESTMAINTBF(R%, BroadbandMobile))

                    Else

                        INFRAINVESTNEWBF(R%, BroadbandMobile) = INFRAINVESTNEW(R%, BroadbandMobile)
                        INFRAINVESTMAINTBF(R%, BroadbandMobile) = INFRAINVESTMAINT(R%, BroadbandMobile)

                    End If



                Next R%
                'mti 2013/12/20 start
            End If
            'mti 2013/12/20 end

            sErrorSub = "IFsInfra1"

            Exit Sub
        End Sub
        Public Sub SubAllYearsForIFsInfra(context As IForecastContext)
            'AllYears:

            '   All Years: Infrastructure
            '----------------------------------------

            'Note MTI: This is leagcy: take off in September (2012)
            'jrs 2009/02/26
            'Compute Digital Access Index
            '----------------------------
            '(Mobil + Broadband + Education + Network) / 4
            'Dim Ed As Single, Nt As Single, DAI As Single
            For R = 1 To NR
                'mti 2015/04/29 - start

                '        'Compute Education Term
                '        '----------------------
                '        Ed = (EDPRIENRN(R, 3) + EDSECENRN(R, 3) + EDTERENRG(R, 3)) / 3
                '        Ed = AMAX(Ed, 100)
                '        'Compute Network Term
                '        Nt = NUMNWP(R) / POP(R) * 100
                '        'Compute DAI (0 to 1)
                '        '--------------------
                '        'jrs 2009/04/02 DAI = (ICTMOBIL(R) + ICTBROAD(R) + Ed + Nt) / 400
                '        'dividing by 2 because ICTMOBIL now can go to 200
                '        DAI = (ICTMOBIL(R) / 2 + ICTBROAD(R) + Ed + Nt) / 400
                '        'First Year
                '        '----------
                '        If IY = 1 Then
                '            'Compute Shift
                '            '-------------
                '            ICTIndexShift(R) = ICTINDEX(R) - DAI
                '        Else 'All years but first
                '        '------------------------
                '            ICTINDEX(R) = DAI + ConvergeOverTime(ICTIndexShift(R), 0, 100)
                '        End If
                '        'it saturates at a value of 1
                '        ICTINDEX(R) = Amin(ICTINDEX(R), 1)

                'mti 2015/04/29 -end


                '        'All Years: Budget Shares
                '        '------------------------
                If IY = 1 Then
                    If INFRAINVESTNEWPUB(R%, InfraTot) > 0 Then
                        iInfraInvestNewPubShr(R%, RoadPaved) = INFRAINVESTNEWPUB(R%, RoadPaved) / INFRAINVESTNEWPUB(R%, InfraTot) 'check for INFRAINVESTNEWPUB(R%, InfraTot)
                        iInfraInvestNewPubShr(R%, RoadUnpaved) = INFRAINVESTNEWPUB(R%, RoadUnpaved) / INFRAINVESTNEWPUB(R%, InfraTot)
                        iInfraInvestNewPubShr(R%, ElectricityGen) = INFRAINVESTNEWPUB(R%, ElectricityGen) / INFRAINVESTNEWPUB(R%, InfraTot)

                    Else
                        iInfraInvestNewPubShr(R%, RoadPaved) = 0.25
                        iInfraInvestNewPubShr(R%, RoadUnpaved) = 0.05
                        iInfraInvestNewPubShr(R%, ElectricityGen) = 0.2

                    End If

                Else
                    If INFRAINVESTNEWPUB(R%, InfraTot) > 0 Then

                        iInfraInvestNewPubShr(R%, RoadPaved) = CSng(0.8 * iInfraInvestNewPubShr(R%, RoadPaved) + 0.2 * INFRAINVESTNEWPUB(R%, RoadPaved) / INFRAINVESTNEWPUB(R%, InfraTot)) 'check for INFRAINVESTNEWPUB(R%, InfraTot)
                        iInfraInvestNewPubShr(R%, RoadUnpaved) = CSng(0.8 * iInfraInvestNewPubShr(R%, RoadUnpaved) + 0.2 * INFRAINVESTNEWPUB(R%, RoadUnpaved) / INFRAINVESTNEWPUB(R%, InfraTot))
                        iInfraInvestNewPubShr(R%, ElectricityGen) = CSng(0.8 * iInfraInvestNewPubShr(R%, ElectricityGen) + 0.2 * INFRAINVESTNEWPUB(R%, ElectricityGen) / INFRAINVESTNEWPUB(R%, InfraTot))

                    End If
                End If

                Dim ShareTot = iInfraInvestNewPubShr(R%, RoadPaved) + iInfraInvestNewPubShr(R%, RoadUnpaved) + iInfraInvestNewPubShr(R%, ElectricityGen)

                If ShareTot > 0.999 Then
                    iInfraInvestNewPubShr(R%, RoadPaved) = CSng(0.999 * iInfraInvestNewPubShr(R%, RoadPaved) / ShareTot)
                    iInfraInvestNewPubShr(R%, RoadUnpaved) = CSng(0.999 * iInfraInvestNewPubShr(R%, RoadUnpaved) / ShareTot)
                    iInfraInvestNewPubShr(R%, ElectricityGen) = CSng(0.999 * iInfraInvestNewPubShr(R%, ElectricityGen) / ShareTot)
                End If

                'mti 2022/08/17 Start
                INFRAELECACCPOP(R%, National) = INFRAELECACC(R%, National) * POP(R%) / 100
                'jrs 2023/03/16 INFRAELECACCPOP(R%, Urban) = INFRAELECACC(R%, Urban) * POPURBAN(R%) / 100
                INFRAELECACCPOP(R%, Urban) = INFRAELECACC(R%, Urban) * SPOPURBAN(R%) / 100
                INFRAELECACCPOP(R%, Rural) = INFRAELECACCPOP(R%, National) - INFRAELECACCPOP(R%, Urban)
                'mti 2022/08/17 End

            Next
            '        'All Years: Expected Fixed BB needed for ICT - renewable link
            '        '-------------------------------------------------------------

            'Note MTI: Revisit thois in September: Original code done by JRS: not working most likely because of sequence mismtach with Governance
            'jrs 2009/06/05
            computeICTBroadExp(context)
        End Sub
        Private Function EstIncomeLt1PcntPrevious(R%, context As IForecastContext) As Single
            'estimates previous years income less than $1 %, using GDPPC growth rate, population growth rate, CS function,  and current year value

            'mti May 04, 2012 - new proc
            Dim rEstIncomeLt1PcntPrevious As Single = 0
            'mti February 28, 2012 - start
            'growth rate in GDPPCP; need to use GDPPCP from a year back, else there will be transients
            'ttt 2019/10/08 Dim GDPPCPGR = (igdpr(R%) - POPR(R%) / 100) / (1 + POPR(R%) / 100) '0 to 1
            'mti February 28, 2012 - start
            Dim Pred_sINCOMELT1CS_Pcnt As Single = 0
            'ttt 2019/10/08 added R to GDPPCPGR
            Call AnalFunc("Percent with Income Below $1.25 per day",
                New Single() {GDPPCP(R%) / (1 + GDPPCPGR(R)), GINIDOM(R%)}, Pred_sINCOMELT1CS_Pcnt,
              New String() {"GDP2005PCPPP(MOSTRECENT)/ 1000 (Log)", "GiniExtended(MOSTRECENT)/ 100 (Linear)"}, context) 'jrs 2014/10/09 added DepVar Names to check order

            Pred_sINCOMELT1CS_Pcnt = AMAX(0.001, Amin(100, Pred_sINCOMELT1CS_Pcnt))

            Dim Pred_INCOMELT125CS2005_Pcnt As Single = 0
            Call AnalFunc("Percent with Income Below $1.25 per day",
               New Single() {GDPPCP(R%), GINIDOM(R%)}, Pred_INCOMELT125CS2005_Pcnt,
                New String() {"GDP2005PCPPP(MOSTRECENT)/ 1000 (Log)", "GiniExtended(MOSTRECENT)/ 100 (Linear)"}, context) 'jrs 2014/10/09 added DepVar Names to check order

            Pred_INCOMELT125CS2005_Pcnt = AMAX(0.001, Amin(100, Pred_INCOMELT125CS2005_Pcnt))

            rEstIncomeLt1PcntPrevious = AMAX(0.001, Amin(100, 100 * INCOMELT125CS2005(R%) / AMAX(0.00001, POP(R%)) - (Pred_INCOMELT125CS2005_Pcnt - Pred_sINCOMELT1CS_Pcnt)))
            Return rEstIncomeLt1PcntPrevious
        End Function
        Private Function ComputeRoadDensity(Income As Single, Population As Single, LndArea As Single) As Single
            '---------------------------------------------------------------------
            'computes the forecast for road density using equation estimated by DSR (using a full panel of data)

            Dim rComputeRoadDensity As Single = 0
            Dim IncDensity = Amin(100000, Income * 1000 / (AMAX(0.00001, LndArea))) '$/hectare; GDPP in billion $;  Landrea in 10000 sq km = 1 mil hectare, converted to 1000 hecatare
            Dim PopDensity = Population / (AMAX(0.00001, LndArea))  'person/ha

            rComputeRoadDensity = CSng(0.0789105773431179 * (IncDensity ^ 0.4834545) * (PopDensity ^ 0.1827084) * (LndArea ^ -0.102367))
            Return rComputeRoadDensity
        End Function
        Private Function ComputePavePcnt(IncPCVar As Single, PopVar As Single, LandVar As Single, RoadDensityVar As Single,
            Optional ByRef IncTerm As Double = 0, Optional ByRef PopTerm As Double = 0, Optional ByRef LandTerm As Double = 0) As Single
            'formula: % of RoadsPaved = 100 * 1/(1 + e^1.0219486 * GDPperCapita^-0.832949 * Population^-0.756058 * LandArea^0.7260397 * TotalRoadDensity^0.2669518)
            '7.89105773431179E-02 = Exp(1.0219486)

            Dim rComputePavePcnt As Single = 0
            IncTerm = IncPCVar ^ -0.832949
            PopTerm = PopVar ^ -0.756058
            LandTerm = LandVar ^ 0.7260397
            'jrs 2013/09/09 RoadDensTerm = RoadDensityVar ^ 0.2669518
            Dim RoadDensTerm = AMAX(RoadDensityVar, 0.0001) ^ 0.2669518
            rComputePavePcnt = CSng(100 / (1 + 2.77860387967124 * IncTerm * PopTerm * LandTerm * RoadDensTerm))
            Return rComputePavePcnt
        End Function

        Private Sub CalcInfraindices(context As IForecastContext)
            'All Years: Infrastructure Index
            '----------------------------------------------------
            Dim LNCompPerPop(NInfraIndexComp%) As Single
            Dim InfraICTIndComp(NInfraIndexICTComp%) As Single

            'mti Sep 12, 2011 - start
            Dim InfraIndTranComp(NInfraIndTranComp%) As Single
            Dim InfraIndElecComp(NInfraIndElecComp%) As Single
            Dim InfraIndWatSanComp(NInfraIndWatSanComp%) As Single
            'mti Sep 12, 2011 - end

            'mti February 28, 2012 - Start
            Dim InfraIndICTzComp(NInfraIndexICTComp%) As Single
            'mti February 28, 2012 - end

            'mti October 20, 2011 - start
            Dim InfraICTIndCompDem(NInfraIndexICTComp%) As Single
            Dim InfraIndTranCompDem(NInfraIndTranComp%) As Single
            Dim InfraIndElecCompDem(NInfraIndElecComp%) As Single
            Dim InfraIndWatSanCompDem(NInfraIndWatSanComp%) As Single


            'mti June 08, 2011 - start
            'ictmobilsaturation = AMAX(ictmobilsaturation, ictbroadmobilsaturation)
            'Dim To100Fact As Single = 100 / ictmobilsaturation
            'ttt
            Dim To100Fact As Single = If(ictmobilsaturation = 0, 0, 100 / ictmobilsaturation)
            'mti June 08, 2011  - end

            Dim TotComp As Single = 0
            For IICCnt% = 1 To NInfraIndWatSanComp%
                TotComp = TotComp + infraindwatsancompwt(IICCnt%)
            Next
            'Dim WtPerCompWatSan = 1 / TotComp
            'ttt
            Dim WtPerCompWatSan = If(TotComp = 0, 0, 1 / TotComp)

            TotComp = 0
            For IICCnt% = 1 To NInfraIndElecComp%
                TotComp = TotComp + infraindeleccompwt(IICCnt%)
            Next
            'Dim WtPerCompElec = 1 / TotComp
            'ttt
            Dim WtPerCompElec = If(TotComp = 0, 0, 1 / TotComp)
            TotComp = 0
            For IICCnt% = 1 To NInfraIndTranComp%
                TotComp = TotComp + infraindtrancompwt(IICCnt%)
            Next
            'Dim WtPerCompTran = 1 / TotComp
            'ttt
            Dim WtPerCompTran = If(TotComp = 0, 0, 1 / TotComp)
            TotComp = 0
            For IICCnt% = 1 To NInfraIndexICTComp%
                TotComp = TotComp + infraindictcompwt(IICCnt%)
            Next
            'Dim WtPerCompICT = 1 / TotComp
            'ttt
            Dim WtPerCompICT = If(TotComp = 0, 0, 1 / TotComp)
            'mti February 28, 2012 - Start
            TotComp = 0
            For IICCnt% = 1 To NInfraIndexICTComp%
                TotComp = TotComp + infraindictzcompwt(IICCnt%)
            Next
            'Dim WtPerCompICTz = 1 / TotComp
            'ttt
            Dim WtPerCompICTz = If(TotComp = 0, 0, 1 / TotComp)
            'mti February 28, 2012 - end

            For R% = 1 To NR%
                ' If RegName(R%) = "Vietnam" Then Stop
                'mti April 2011 -start


                'mti July 18, 2011 -start
                'mti October 20, 2011 - start
                '        LNCompPerPop(TotalRoad) = Math.Log(INFRAROAD(R%) * LANDAREA(R%) / POP(R%) / 1000)
                '        LNCompPerPop(ElecGen) = Math.Log(INFRAELECGENCAP(R%))
                '        LNCompPerPop(ElecAcc) = Math.Log(AMAX(0.5, INFRAELECACC(R%, Total)) / 100)
                '        LNCompPerPop(WaterCon) = Math.Log(AMAX(0.5, WATSAFE(R%, 3)) / 100)
                '        LNCompPerPop(SanitCon) = Math.Log(AMAX(0.5, SANITATION(R%, 3)) / 100)
                '        LNCompPerPop(FixedPhone) = Math.Log(AMAX(0.5, Amin(INFRATELE(R%), 100)) / 100)
                '        LNCompPerPop(FixedBroadband) = Math.Log(AMAX(0.5, Amin(ICTBROAD(R%), 100)) / 100)
                '        LNCompPerPop(MobilePhone) = Math.Log(AMAX(0.5, Amin(ICTMOBIL(R%), 100)) / 100)
                '        LNCompPerPop(MobileBroadband) = Math.Log(AMAX(0.5, Amin(ICTBROADMOBIL(R%), 100)) / 100)
                'mti October 20, 2011 - end


                '        LNCompPerPop(TotalRoad) = Math.Log(INFRAROAD(R%) / POP(R%)) 'km/(land-person)
                '        LNCompPerPop(ElecGen) = Math.Log(INFRAELECGENCAP(R%) / GDP(R%)) 'kwh/(gdp-person)
                '
                '        LNCompPerPop(ElecAcc) = Math.Log(AMAX(1, INFRAELECACC(R%, Total)))
                '        LNCompPerPop(WaterCon) = Math.Log(AMAX(1, WATSAFE(R%, 3)))
                '        LNCompPerPop(SanitCon) = Math.Log(AMAX(1, SANITATION(R%, 3)))
                '        LNCompPerPop(FixedPhone) = Math.Log(AMAX(1, Amin(INFRATELE(R%), 100)))
                '        LNCompPerPop(FixedBroadband) = Math.Log(AMAX(1, Amin(ICTBROAD(R%), 100)))
                '        LNCompPerPop(MobilePhone) = Math.Log(AMAX(1, Amin(ICTMOBIL(R%), 100)))
                '        LNCompPerPop(MobileBroadband) = Math.Log(AMAX(1, Amin(ICTBROADMOBIL(R%), 100)))



                'mti July 18, 2011 -end


                'mti - This will work for equal weight only
                'mti October 20, 2011 -start
                '        TotComp = 0
                '        For IICCnt% = 1 To NInfraIndexComp%
                '            TotComp = TotComp + infraimodcompwt(IICCnt%)
                '        Next
                '        WtPerComp = 1 / TotComp
                '
                '        INFRAIMOD(R%) = 0
                '        For IICCnt% = 1 To NInfraIndexComp%
                '            ZComp = (LNCompPerPop(IICCnt%) - INFRAIMODCOMPMEANI(IICCnt%)) / INFRAIMODCOMPSDI(IICCnt%)
                ''If RegName(R%) = "Canada" And IY = 2 Then Debug.Print IICCnt%; ZComp;
                ''If RegName(R%) = "Canada" And IY = 1 Then Debug.Print IICCnt%; ZComp;
                '            INFRAIMOD(R%) = INFRAIMOD(R%) + infraimodcompwt(IICCnt%) * WtPerComp * ZComp
                '        Next
                'mti October 20, 2011 - end
                'If RegName(R%) = "Canada" And IY = 2 Then Debug.Print IY
                'If RegName(R%) = "Canada" And IY = 1 Then Debug.Print IY
                'mti April 2011 -start
                'mti Sep 12, 2011 - start
                '        'mti April 26 2011 -start
                '        TotComp = 0
                '        For IICCnt% = 1 To NInfraIndexTradComp%
                '            TotComp = TotComp + infraindtradcompwt(IICCnt%)
                '        Next
                '        WtPerComp = 1 / TotComp
                '
                '        INFRAINDTRAD(R%) = 0
                '
                ''If RegName(R%) = "Guinea" Then
                ''        Debug.Print IY;
                ''End If
                '
                '        For IICCnt% = 1 To NInfraIndexTradComp%
                '            ZComp = (LNCompPerPop(IICCnt%) - INFRAINDTRADCOMPMEANI(IICCnt%)) / INFRAINDTRADCOMPSDI(IICCnt%)
                '            INFRAINDTRAD(R%) = INFRAINDTRAD(R%) + infraindtradcompwt(IICCnt%) * WtPerComp * ZComp
                ''
                ''If RegName(R%) = "Guinea" Then
                ''        Debug.Print IICCnt; ZComp; infraindtradcompwt(IICCnt%) * WtPerComp * ZComp;
                ''End If
                '
                '        Next
                'mti Sep 12, 2011 - end

                'If RegName(R%) = "Guinea" Then
                '        Debug.Print
                'End If
                'If RegName(R%) = "Sweden" Then Stop

                InfraICTIndComp(Fixed_Phone) = Amin(100, AMAX(0, INFRATELE(R%)))
                InfraICTIndComp(Fixed_Broadband) = Amin(100, AMAX(0, ICTBROAD(R%)))



                'mti June 08, 2011 - start
                '        InfraICTIndComp(Mobile_Phone) = Amin(120, AMAX(0, ICTMOBIL(R%))) * 5 / 6
                '        InfraICTIndComp(Mobile_Broadband) = Amin(120, AMAX(0, ICTBROADMOBIL(R%))) * 5 / 6

                InfraICTIndComp(Mobile_Phone) = Amin(ictmobilsaturation, ICTMOBIL(R%)) * To100Fact
                InfraICTIndComp(Mobile_Broadband) = Amin(ictmobilsaturation, ICTBROADMOBIL(R%)) * To100Fact


                'mti June 08, 2011 - end

                '        TotComp = 0
                '        For IICCnt% = 1 To NInfraIndexICTComp%
                '            TotComp = TotComp + infraindictcompwt(IICCnt%)
                '        Next
                '        WtPerComp = 1 / TotComp

                INFRAINDICT(R%) = 0

                For IICCnt% = 1 To NInfraIndexICTComp%

                    'Total components 9; 1 to 5 traditional; 6 to 9 ICT ; their are 3 separate sub-dimensions though
                    'ZComp = (LNCompPerPop(NInfraIndexTradComp% + IICCnt%) - INFRAINDICTCOMPMEANI(IICCnt%)) / INFRAINDICTCOMPSDI(IICCnt%)
                    'INFRAINDICT(R%) = INFRAINDICT(R%) + infraindictcompwt(IICCnt%) * WtPerComp * ZComp
                    INFRAINDICT(R%) = CSng(INFRAINDICT(R%) + infraindictcompwt(IICCnt%) * WtPerCompICT * InfraICTIndComp(IICCnt%))

                Next


                'mti Sep 12, 2011 - start

                'Infrstructure Sub-Index: Transportation
                'Infrstructure Sub-Index: Transportation
                'jrs 2013/09/09 InfraIndTranComp(Road_PerCapita) = Math.Log(INFRAROAD(R%) * LANDAREA(R%) / POP(R%)) 'km per 1000 person
                InfraIndTranComp(Road_PerCapita) = CSng(Math.Log(AMAX(0.0001, INFRAROAD(R%)) * LANDAREA(R%) / POP(R%)))
                'jrs 2013/09/09  InfraIndTranComp(Road_PerLand) = Math.Log(INFRAROAD(R%)) 'km per 1000 hectare
                InfraIndTranComp(Road_PerLand) = CSng(Math.Log(INFRAROAD(R%)))   'km per 1000 hectare
                InfraIndTranComp(Road_Access) = CSng(Math.Log(AMAX(0.0001, INFRAROADRAI(R%))))  'Road access index, lower bound set at one to avoid negative
                InfraIndTranComp(Road_Paved) = CSng(Math.Log(AMAX(0.0001, INFRAROADPAVEDPCNT(R%))))  'Road paved percent, lower bound set at one to avoid negative


                '        TotComp = 0
                '        For IICCnt% = 1 To NInfraIndTranComp%
                '            TotComp = TotComp + infraindtrancompwt(IICCnt%)
                '        Next
                '        WtPerComp = 1 / TotComp

                INFRAINDTRAN(R%) = 0
                Dim zcomp As Single = 0
                For IICCnt% = 1 To NInfraIndTranComp%
                    'zcomp = (InfraIndTranComp(IICCnt%) - INFRAINDTRANCOMPMEANI(IICCnt%)) / INFRAINDTRANCOMPSDI(IICCnt%)
                    'ttt
                    zcomp = If(INFRAINDTRANCOMPSDI(IICCnt%) = 0, 0, (InfraIndTranComp(IICCnt%) - INFRAINDTRANCOMPMEANI(IICCnt%)) / INFRAINDTRANCOMPSDI(IICCnt%))
                    INFRAINDTRAN(R%) = INFRAINDTRAN(R%) + infraindtrancompwt(IICCnt%) * WtPerCompTran * zcomp
                Next

                'Infrstructure Sub-Index: Electricity
                'jrs 2013/09/09  InfraIndElecComp(Elec_Gen) = Math.Log(INFRAELECGENCAP(R%)) 'electricity generation capacity per capita
                InfraIndElecComp(Elec_Gen) = CSng(Math.Log(AMAX(0.0001, INFRAELECGENCAP(R%))))
                InfraIndElecComp(Elec_Acc) = CSng(Math.Log(AMAX(0.0001, INFRAELECACC(R%, Total))))   'electricity access, percentage, lower bound set at one to avoid negative

                'for transmission loss; lower is better: value bound between 3 to 90; 91-90 = 1 (trying to avoid values < 1, as it is logged) ; 91-3=88;
                'InfraIndElecComp(Elec_Tran) = Math.Log(AMAX(1, INFRAELECTRANLOSS(R%)))    'electricity transmission loss, percentage, lower bound set at one to avoid negative
                InfraIndElecComp(Elec_Tran) = CSng(Math.Log(Amin(88, AMAX(1, 91 - INFRAELECTRANLOSS(R%))))) 'electricity transmission loss, percentage, lower bound set at one to avoid negative

                InfraIndElecComp(Elec_Share) = CSng(Math.Log(ENELECSHRENDEM(R%)))    'electricity share of total energy

                '    If RegName$(R%) = "Argentina" Then Stop
                '   If RegName$(R%) = "Palestine" Then Stop

                '        TotComp = 0
                '        For IICCnt% = 1 To NInfraIndElecComp%
                '            TotComp = TotComp + infraindeleccompwt(IICCnt%)
                '        Next
                '        WtPerComp = 1 / TotComp

                INFRAINDELEC(R%) = 0
                For IICCnt% = 1 To NInfraIndElecComp%
                    'zcomp = (InfraIndElecComp(IICCnt%) - INFRAINDELECCOMPMEANI(IICCnt%)) / INFRAINDELECCOMPSDI(IICCnt%)
                    'ttt
                    zcomp = If(INFRAINDELECCOMPSDI(IICCnt%) = 0, 0, (InfraIndElecComp(IICCnt%) - INFRAINDELECCOMPMEANI(IICCnt%)) / INFRAINDELECCOMPSDI(IICCnt%))
                    INFRAINDELEC(R%) = INFRAINDELEC(R%) + infraindeleccompwt(IICCnt%) * WtPerCompElec * zcomp
                Next

                'mti February 28, 2012 - Start

                'Infrstructure Sub-Index: ICT Z
                InfraIndICTzComp(Fixed_Phone) = CSng(Math.Log(AMAX(0.0001, INFRATELE(R%)) / 100))
                InfraIndICTzComp(Fixed_Broadband) = CSng(Math.Log(AMAX(0.0001, ICTBROAD(R%)) / 100))

                InfraIndICTzComp(Mobile_Phone) = CSng(Math.Log(AMAX(0.0001, Amin(ICTMOBIL(R%), 100)) / 100))

                InfraIndICTzComp(Mobile_Broadband) = CSng(Math.Log(AMAX(0.0001, ICTBROADMOBIL(R%)) / 100))

                INFRAINDICTZ(R%) = 0

                For IICCnt% = 1 To NInfraIndexICTComp%
                    'mti June 14, 2012 -start
                    'zcomp = (InfraIndICTzComp(IICCnt%) - INFRAINDICTZCOMPMEANI(IICCnt%)) / INFRAINDICTZCOMPSDI(IICCnt%)

                    If INFRAINDICTZCOMPSDI(IICCnt%) <> 0 Then
                        zcomp = (InfraIndICTzComp(IICCnt%) - INFRAINDICTZCOMPMEANI(IICCnt%)) / INFRAINDICTZCOMPSDI(IICCnt%)
                    Else
                        zcomp = 0
                    End If
                    'mti June 14, 2012 -end

                    INFRAINDICTZ(R%) = INFRAINDICTZ(R%) + infraindictzcompwt(IICCnt%) * WtPerCompICTz * zcomp
                Next

                'mti February 28, 2012 - End


                'Infrstructure Sub-Index: Water and Sanitation
                'mti 2022/08/17 start
                Dim Water_Safe_Rate As Single, Sanit_Safe_Rate As Single
                'mti 2022/10/20 start
                'If watsafenewsw = 1 Then
                If watsafenewsw = 1 And watsafefwdsw = 1 Then
                    'mti 2022/10/20 end
                    'newer 5 category model
                    Water_Safe_Rate = WATERHH(R%, 5, National)
                    Sanit_Safe_Rate = SANITHH(R%, 5, National)
                Else
                    'older 3 category model
                    Water_Safe_Rate = WATSAFE(R%, 3)
                    Sanit_Safe_Rate = SANITATION(R%, 3)

                End If

                InfraIndWatSanComp(Watsan_Wat) = CSng(Log(CSng(AMAX(0.0001, Water_Safe_Rate))))  'safe water access, household connection percentage, lower bound set at one to avoid negative
                InfraIndWatSanComp(Watsan_San) = CSng(Log(CSng(AMAX(0.0001, Sanit_Safe_Rate))))  'sanitation access, household connection percentage, lower bound set at one to avoid negative
                InfraIndWatSanComp(Watsan_WasteTreat) = CSng(Log(CSng(AMAX(0.0001, WATWASTETREAT(R%)))))  'waste water treatment access, household connection percentage, lower bound set at one to avoid negative
                'mti 2022/08/17 End

                INFRAINDWATSAN(R%) = 0


                For IICCnt% = 1 To NInfraIndWatSanComp%
                    zcomp = (InfraIndWatSanComp(IICCnt%) - INFRAINDWATSANCOMPMEANI(IICCnt%)) / INFRAINDWATSANCOMPSDI(IICCnt%)
                    INFRAINDWATSAN(R%) = INFRAINDWATSAN(R%) + infraindwatsancompwt(IICCnt%) * WtPerCompWatSan * zcomp
                Next


                'mti July 18, 2011 - start
                '        If RegName(R%) = "Sweden" Or RegName(R%) = "United States of America" Or RegName(R%) = "Zambia" Or RegName(R%) = "China" Or RegName(R%) = "Chad" Or RegName(R%) = "India" Or RegName(R%) = "Brazil" Then
                '            Stop
                '            INFRAINDICT(R%) = INFRAINDICT(R%) + Abs(INFRAINDICT(R%) * (Amin(0.5, (IY - 1) * (1.5 - 1) / 10)))
                '            INFRAINDICT(R%) = Amin(100, INFRAINDICT(R%))
                '        End If

                'mti 2014/05/28 - start
                If Not context.HistoricMode Then
                    INFRAINDTRAD(R%) = (INFRAINDTRAN(R%) + INFRAINDELEC(R%) + INFRAINDWATSAN(R%)) / 3
                Else
                    'too much rise in sanitation and watsafe - they have been excluded from infratrad index to check MFPPC growth
                    INFRAINDTRAD(R%) = (INFRAINDTRAN(R%) + INFRAINDELEC(R%)) / 2
                End If
                'mti 2014/05/28 - end

                '   If RegName(R%) = "Afghanistan" And IY < 5 Then Debug.Print IY; INFRAINDTRAD(R%); INFRAINDTRAN(R%); INFRAINDELEC(R%); INFRAINDWATSAN(R%)
                'mti October 20, 2011 - start
                If IY = 1 Then
                    INFRAINDTRADDEM(R%) = INFRAINDTRAD(R%)
                Else


                    'Infrstructure Sub-Index: Transportation
                    'jrs 2013/09/09 InfraIndTranCompDem(Road_PerCapita) = Math.Log(INFRAROADDEM(R%) * LANDAREA(R%) / POP(R%)) 'km per 1000 person
                    InfraIndTranCompDem(Road_PerCapita) = CSng(Math.Log(CDbl(AMAX(INFRAROADDEM(R%) * LANDAREA(R%) / POP(R%), 0.0001)))) 'km per 1000 person
                    'jrs 2013/09/09  InfraIndTranCompDem(Road_PerLand) = Math.Log(INFRAROADDEM(R%)) 'km per 1000 hectare
                    InfraIndTranCompDem(Road_PerLand) = CSng(Math.Log(INFRAROADDEM(R%)))   'km per 1000 hectare
                    InfraIndTranCompDem(Road_Access) = InfraIndTranComp(Road_Access) 'Road access index, does not have dem
                    InfraIndTranCompDem(Road_Paved) = CSng(Math.Log(AMAX(0.0001, INFRAROADPAVEDPCNTDEM(R%))))  'Road paved percent, lower bound set at one to avoid negative


                    Dim INFRAINDTRANDEM As Single = 0
                    Dim ZCompDem As Single = 0
                    For IICCnt% = 1 To NInfraIndTranComp%
                        ZCompDem = (InfraIndTranCompDem(IICCnt%) - INFRAINDTRANCOMPMEANI(IICCnt%)) / INFRAINDTRANCOMPSDI(IICCnt%)
                        INFRAINDTRANDEM = INFRAINDTRANDEM + infraindtrancompwt(IICCnt%) * WtPerCompTran * ZCompDem
                    Next

                    InfraIndElecCompDem(Elec_Gen) = CSng(Math.Log(INFRAELECGENCAPDEM(R%)))   'electricity generation capacity per capita
                    InfraIndElecCompDem(Elec_Acc) = InfraIndElecComp(Elec_Acc) 'Math.Log(AMAX(1, INFRAELECACC(R%, Total)))   'electricity access, percentage, lower bound set at one to avoid negative
                    InfraIndElecCompDem(Elec_Tran) = InfraIndElecComp(Elec_Tran) 'Math.Log(Amin(88, AMAX(1, 91 - INFRAELECTRANLOSS(R%)))) 'electricity transmission loss, percentage, lower bound set at one to avoid negative
                    InfraIndElecCompDem(Elec_Share) = InfraIndElecComp(Elec_Share) 'Math.Log(ENELECSHRENDEM(R%))    'electricity share of total energy

                    Dim INFRAINDELECDEM As Single = 0
                    For IICCnt% = 1 To NInfraIndElecComp%
                        ZCompDem = (InfraIndElecCompDem(IICCnt%) - INFRAINDELECCOMPMEANI(IICCnt%)) / INFRAINDELECCOMPSDI(IICCnt%)
                        INFRAINDELECDEM = INFRAINDELECDEM + infraindeleccompwt(IICCnt%) * WtPerCompElec * ZCompDem
                    Next

                    'Infrstructure Sub-Index: Water and Sanitation
                    'mti 2022/08/17 start
                    'mti 2022/10/20 start
                    'If watsafenewsw = 1 Then
                    If watsafenewsw = 1 And watsafefwdsw = 1 Then
                        'mti 2022/10/20 end
                        'newer 5 category model
                        Water_Safe_Rate = WATERHHDEM(R%, 5, National)
                        Sanit_Safe_Rate = SANITHHDEM(R%, 5, National)
                    Else
                        'older 3 category model
                        Water_Safe_Rate = WATSAFEDEM(R%, 3)
                        Sanit_Safe_Rate = SANITATIONDEM(R%, 3)
                    End If

                    InfraIndWatSanCompDem(Watsan_Wat) = CSng(Log(CSng(AMAX(0.0001, Water_Safe_Rate))))  'safe water access, household connection percentage, lower bound set at one to avoid negative
                    InfraIndWatSanCompDem(Watsan_San) = CSng(Log(CSng(AMAX(0.0001, Sanit_Safe_Rate))))  'sanitation access, household connection percentage, lower bound set at one to avoid negative
                    InfraIndWatSanCompDem(Watsan_WasteTreat) = CSng(Log(CSng(AMAX(0.0001, WATWASTETREATDEM(R%)))))  'waste water treatment access, household connection percentage, lower bound set at one to avoid negative
                    'mti 2022/08/17 End

                    Dim INFRAINDWATSANDEM As Single = 0
                    For IICCnt% = 1 To NInfraIndWatSanComp%
                        ZCompDem = (InfraIndWatSanCompDem(IICCnt%) - INFRAINDWATSANCOMPMEANI(IICCnt%)) / INFRAINDWATSANCOMPSDI(IICCnt%)
                        INFRAINDWATSANDEM = INFRAINDWATSANDEM + infraindwatsancompwt(IICCnt%) * WtPerCompWatSan * ZCompDem
                    Next


                    INFRAINDTRADDEM(R%) = (INFRAINDTRANDEM + INFRAINDELECDEM + INFRAINDWATSANDEM) / 3

                End If
                'mti October 20, 2011 - end




                'mti Sep 12, 2011 - end

                SINFRAINDTRAD(R%) = INFRAINDTRAD(R%)


                SINFRAINDTRADDEM(R%) = INFRAINDTRADDEM(R%)


                'mti May 2011 -start
                'If RegName(R%) = "Sweden" Then Stop
                If IY = 1 Then
                    SSINFRAINDICT(R%) = INFRAINDICT(R%)
                    SSINFRAINDICTDEM(R%) = INFRAINDICTDEM(R%)

                    INFRAINDICTDEM(R%) = INFRAINDICT(R%)
                Else
                    InfraICTIndCompDem(Fixed_Phone) = Amin(100, AMAX(0, INFRATELEDEM(R%)))
                    InfraICTIndCompDem(Fixed_Broadband) = Amin(100, AMAX(0, ICTBROADDEM(R%)))
                    InfraICTIndCompDem(Mobile_Phone) = Amin(ictmobilsaturation, ICTMOBILDEM(R%)) * To100Fact
                    InfraICTIndCompDem(Mobile_Broadband) = Amin(ictmobilsaturation, ICTBROADMOBILDEM(R%)) * To100Fact

                    INFRAINDICTDEM(R%) = 0
                    For IICCnt% = 1 To NInfraIndexICTComp%
                        INFRAINDICTDEM(R%) = INFRAINDICTDEM(R%) + infraindictcompwt(IICCnt%) * WtPerCompICT * InfraICTIndCompDem(IICCnt%)
                    Next

                    SSINFRAINDICT(R%) = SINFRAINDICT(R%)
                    SSINFRAINDICTDEM(R%) = SINFRAINDICTDEM(R%)
                End If
                'mti May 2011 - end

                SINFRAINDICT(R%) = INFRAINDICT(R%)
                SINFRAINDICTDEM(R%) = INFRAINDICTDEM(R%)
                'mti October 20, 2011 -end
                'mti April 26 2011 - end

                'mti April 26 2011 - end

                'If RegName(R%) = "Liberia" Then Debug.Print INFRAIMOD(R%)

                'jrs 2013/09/16 *** begin ***
                'Infrastructure link to Poverty (GiniDom)
                'SINFRAINDCOMBINED(R%) = INFRAINDCOMBINED(R%)
                'INFRAINDCOMBINED(R%) = 0.25 * INFRAINDICTZ(R%) + 0.75 * INFRAINDTRAD(R%)
                'jrs 2013/09/16 *** end ***

                'mti 2015/04/29 - start
                'If RegName(R%) = "Denmark" Then Stop

                'mti 04/17/2015 - start

                'Including this gives the index a downward trend because traditional telephone forecast goes down in IFs
                'TPhone = Amin(1, INFRATELE(R%) / 60)

                'including fixed broadband instead
                'wired telephone infrastructure are assumed to be taken over by fixed broadband in our model

                'mti 2015/05/06 - start
                'TBroad = Amin(1, ICTBROAD(R%) / 60) 'broadband already used in another component

                'mti 2015/06/01 - start
                Dim TMobil = Amin(1, ICTMOBIL(R%) / 120)
                Dim TComputerHH = Amin(1, ICTCOMPUTERS(R%) / 100)
                Dim TIntNetHH = Amin(1, ICTINTNETHH(R%) / 100)
                'Note: International Bandwidth Per Internet User is not included yet
                Dim ICTAcc = (TMobil + TComputerHH + TIntNetHH) / 3


                Dim TBroad = Amin(1, ICTBROAD(R%) / 50)
                Dim TBroadMobil = Amin(1, ICTBROADMOBIL(R%) / 100)

                'TIntNetIndiv = Amin(1, ICTINTNET(R%) / 100)
                Dim ICTUse = (TBroad + TBroadMobil) / 2
                'mti 2015/05/06 - end


                Dim TLit = Amin(1, SLit(R%) / 100)

                Dim TEdSecEnrg = Amin(1, EDSECENRG(R%, Total) / 100)
                Dim TEdTerEnrg = Amin(1, EDTERENRG(R%, Total) / 100)
                Dim ICTSkill = (TLit + TEdSecEnrg + TEdTerEnrg) / 3


                Dim calcICTIndex = CSng(10 * (0.4 * ICTAcc + 0.4 * ICTUse + 0.2 * ICTSkill))
                'mti 2015/06/01 - end

                'If RegName(R%) = "Armenia" Then Stop
                ''If RegName(R%) = "Iraq" Then Stop
                'If RegName(R%) = "France" Then Stop
                If IY = 1 Then
                    ICTIndexShift(R%) = ICTINDEX(R%) - calcICTIndex
                Else
                    If ICTIndexShift(R) < 0 Then 'upward convergence only
                        ICTINDEX(R%) = calcICTIndex + ConvergeOverTime(ICTIndexShift(R%), 0, 50) '50
                    Else
                        'mti 2015/05/06 - start
                        ICTINDEX(R%) = calcICTIndex + ConvergeOverTime(ICTIndexShift(R%), 0, 70) '300 '200
                        'mti 2015/05/06 - end
                    End If
                    'mti 2015/06/01 - start
                    ICTINDEX(R%) = ICTINDEX(R%) * ictindexm(R%)
                    'mti 2015/06/01 - end
                End If

                'If IY = 1 Then Debug.Print RegName(R%); ","; TICTTot; ","; TICTAcc; ","; TICTUse; ","; TICTSkill
                'mti 2015/04/29 - end

            Next R

        End Sub
        Private Function CalcExtrapolativeGR(Grate As Single?, Portion_Function As Double, Portion_Position As Double,
            Position_Previous As Double, Prediction_Current As Double, Prediction_Previous As Double,
            PosConvTime As Double, Optional PosConvFloat As Boolean = True, Optional PosConvDownOnly As Boolean = False, Optional PosConvUpOnly As Boolean = True) As Single
            'On Error GoTo errext
            'mti October 03, 2011 - take whole proc

            'mti Sep 12, 2011 - new procedure
            'calculates growth rate for extrapolative approach
            'uses current year and previous year predictions (from function), previous year's growth rate and current position
            Dim Portion_Last = AMAX(0, 1 - (Portion_Function + Portion_Position))

            'Functional growth rate, i.e., growth rate required to reach current year predicted value from previous year's predicted value
            'could be a growth or decline depending on values of drivers
            Dim cGrowthRateFunction = (Prediction_Current - Prediction_Previous) / AMAX(0.000001, Prediction_Previous)

            'Growth rate needed to converge to function from current position

            'mti October 03, 2011 -start
            'floating positional convergence
            'PosConvTime = 10
            'PosConvTime has to be a low value to avoid divergence; however very low values might give transient
            Dim RatChange = Prediction_Current / AMAX(0.00001, Position_Previous)

            'mti May 15, 2012 - start
            Dim cGrowthRatePosition = RatChange ^ (1 / PosConvTime) - 1 'can be negative
            '    PosConvTime1 = AMAX(1, PosConvTime - AMAX(0, (IY - 50)))
            '    cGrowthRatePosition = RatChange ^ (1 / PosConvTime1) - 1 'can be negative
            'mti May 15, 2012 - end



            '    'fixed positional convergence
            '    'there is a delay through Portion_Position (.1); since convergence time is usually >10 years values this should accumulate
            '    Position_Current = ConvergeOverTime1(Position_Previous, Prediction_Current, PosConvTime)
            '    cGrowthRatePosition = (Positon_Current - Position_Previous) / AMAX(0.000001, Position_Previous)

            'mti October 03, 2011 -end

            'mti February 08, 2012 -start
            Dim Portion_FunctionAdj = Portion_Function

            If PosConvUpOnly Then
                'positional convergence to function works upward only
                cGrowthRatePosition = AMAX(0, cGrowthRatePosition)
                '
                'mti - put a condition for this (only for elec)
                '        If RatChange > 1.01 Then Portion_FunctionAdj = Portion_Function + Portion_Position

                '

            End If

            If PosConvDownOnly Then
                'positional convergence to function works downward only
                cGrowthRatePosition = Amin(0, cGrowthRatePosition)
            End If





            'CalcExtrapolativeGR = Portion_Last * Grate + Portion_Function * cGrowthRateFunction + Portion_Position * cGrowthRatePosition
            CalcExtrapolativeGR = CSng(Portion_Last * Grate + Portion_FunctionAdj * cGrowthRateFunction + Portion_Position * cGrowthRatePosition)
            'mti February 08, 2012 -end

            'DSR February 15, 2012 - start
            'mti note -checked China, USA, Norway, - did not make any difference- so commentd it for now
            '
            ''When predicted value is within 1% of expected, set Portion_Position=0 and give amount to Portion_Function
            '    Portion_Function2 = Portion_FunctionAdj
            '    Portion_Last2 = Portion_Last
            '    Portion_Position2 = Portion_Position
            '    If ((1 / 1.01) * Prediction_Current < Position_Previous) And ((1 / 0.99) * Prediction_Current > Position_Previous) Then
            '        Portion_Function2 = Portion_FunctionAdj / (1 - Portion_Position)
            '        Portion_Last2 = Portion_Last / (1 - Portion_Position)
            '        Portion_Position2 = 0
            '    End If
            '
            '    CalcExtrapolativeGR = Portion_Last2 * Grate + Portion_Function2 * cGrowthRateFunction + Portion_Position2 * cGrowthRatePosition
            'DSR February 15, 2012 - end
            'Exit Function
            'errext:
            '    Stop
        End Function

        Private Sub CalcInfraUnitCost(RegNo%, Drvr_UpVal As Single, Drvr_LowVal As Single, UCost_UpVal As Single, UCost_LowVal As Single, Drvr_CurVal As Single, ByRef UCost_CurVal As Single, Optional Increasing As Boolean = True)
            'mti October 20, 2011 - whole proc
            'mti Infrastructure Cost August 2010 - new procedure
            'generic procedure for calculating unit cost - starts with a lower value
            'and merges linearly to a upper value as the driver (e.g., GDPPCP) merges to a upper value

            If Increasing Then
                If Drvr_CurVal <= Drvr_LowVal Then 'lower value of driver/GDPPPCP for example
                    UCost_CurVal = UCost_LowVal
                Else  '5000 to 10000 - linear path
                    UCost_CurVal = UCost_LowVal + (UCost_UpVal - UCost_LowVal) * (Amin(Drvr_CurVal, Drvr_UpVal) - Drvr_LowVal) / (Drvr_UpVal - Drvr_LowVal)
                End If
            Else 'decreasing cost
                If Drvr_CurVal <= Drvr_LowVal Then 'lower value of driver/GDPPPCP for example
                    UCost_CurVal = UCost_UpVal
                Else  '5000 to 10000 - linear path
                    UCost_CurVal = UCost_UpVal - (UCost_UpVal - UCost_LowVal) * (Amin(Drvr_CurVal, Drvr_UpVal) - Drvr_LowVal) / (Drvr_UpVal - Drvr_LowVal)
                End If
            End If

            'mti October 20, 2011 - start
            UCost_CurVal = UCost_CurVal * ConvergeOverTime(INFRACOSTADJFAC(RegNo%), 1, 100) '60
            'mti October 20, 2011 - end

        End Sub
        Private Sub CalcInfraInvest(PhysStckDmndThou As Double, sPhysStckThou As Double, DepPercent As Double, UnitCostInThousand As Double, ByRef InvestNewCon As Single, ByRef InvestMaint As Single)
            'mti May 05, 2011 - new

            'New Construction -----------------------------------------------------
            InvestNewCon = CSng(AMAX(0, (PhysStckDmndThou - sPhysStckThou)) * UnitCostInThousand / 1000)  'in billion $

            'Maintenance Funds
            InvestMaint = CSng(sPhysStckThou * DepPercent * UnitCostInThousand / 1000)  'in billion $

        End Sub

        Private Sub CalcInfraBudgetImpact(R%, PhysStckDmndThou As Double, sPhysStckThou As Double, UnitCostInThoUSAnd As Single, InfraType%, Optional IsICT As Boolean = False)
            'mti 2022/10/20 - changed proc (take entire proc)
            'Private Sub CalcInfraBudgetImpact(PhysStckDmndThou As Double, sPhysStckThou As Double, DepPercent As Double, UnitCostInThoUSAnd, InvestNewCon, InvestMaint, BudgDemndNewCon, sInfraBudImpactByType(R%, InfraType) As Double, publicsharenewcon, publicsharemaint)
            'mti October 20, 2011 - whole proc
            'mti February 2011 Infarstructure - new proc


            'New Construction -----------------------------------------------------
            'mti April 05, 2012 ' -9999 is dummy for avoiding impact as it is nalready done for certain types of target financing
            If sInfraBudImpactByType(R%, InfraType) <> -9999 Then
                INFRAINVESTNEWPUB(R%, InfraType) = INFRABUDDEMNEW(R%, InfraType) * sInfraBudImpactByType(R%, InfraType)
                INFRAINVESTMAINTPUB(R%, InfraType) = INFRABUDDEMMNT(R%, InfraType) * sInfraBudImpactByType(R%, InfraType)
            End If

            Dim TotalFundsNewCon, PhysStckAdded, InvestNewConPri As Double
            If Not IsICT Then
                'same deficit in public and private
                TotalFundsNewCon = INFRAINVESTNEWPUB(R%, InfraType) / infrainvnewpubshrm(R%, InfraType)
                PhysStckAdded = 1000 * TotalFundsNewCon / AMAX(0.01, UnitCostInThoUSAnd)  ' 1000* (billion $) / (thoUSAnd $/ha) =  1000 ha
            Else
                'no deficit in private for ICT
                'same logic maintained for ICT private (private sector) investment in a target scenario
                InvestNewConPri = (INFRABUDDEMNEW(R%, InfraType) / infrainvnewpubshrm(R%, InfraType)) * (1 - infrainvnewpubshrm(R%, InfraType))

                TotalFundsNewCon = INFRAINVESTNEWPUB(R%, InfraType) + InvestNewConPri
                PhysStckAdded = 1000 * TotalFundsNewCon / AMAX(0.01, UnitCostInThoUSAnd)  ' 1000* (billion $) / (thoUSAnd $/ha) =  1000 ha
            End If

            'mti January 2011 - start
            Dim PhysStckAddedUnConstrained = AMAX(0, (PhysStckDmndThou - sPhysStckThou)) 'thoUSAnd units
            Dim NewConCut = AMAX(0, PhysStckAddedUnConstrained - PhysStckAdded)


            'Maintenance Funds
            'mti May 05, 2011 - start
            'UnMaintPhysStck = 0
            'MaintReqPhysStck = SPhysStckThou * DepPercent '1000 ha

            'Maintenance cut at this point are from public only; if private is added ICT might take big hits

            'MaintDeficitPhysStckPcnt = DepPercent * publicsharemaint * (1 - sInfraBudImpactByType(R%, InfraType))
            'MaintDeficitPhysStckPcnt = DepPercent * (1 - sInfraBudImpactByType(R%, InfraType))

            'mti October 20, 2011 - start
            'UnMaintPhysStck = SPhysStckThou * MaintDeficitPhysStckPcnt

            'mti April 05, 2012
            'this can be different with new (target) allocation logic
            Dim UnMaintPhysStck As Double
            If INFRABUDDEMMNT(R%, InfraType) > 0 Then
                Dim BudgetImpactRatioMnt = INFRAINVESTMAINTPUB(R%, InfraType) / INFRABUDDEMMNT(R%, InfraType)

                If Not IsICT Then
                    UnMaintPhysStck = AMAX(0, sPhysStckThou * Deppcnt(InfraType) * (1 - BudgetImpactRatioMnt))
                    'readjust total investment - maintenance
                    'InvestMaint = (MaintReqPhysStck - UnMaintPhysStck) * UnitCostInThoUSAnd / 1000 'in billion $
                    INFRAINVESTMAINT(R%, InfraType) = CSng(sPhysStckThou * Deppcnt(InfraType) * BudgetImpactRatioMnt * UnitCostInThoUSAnd / 1000) 'in billion $
                    'mti May 05, 2011 - end
                Else
                    UnMaintPhysStck = AMAX(0, sPhysStckThou * infrainvmaintpubshrm(R%, InfraType) * Deppcnt(InfraType) * (1 - BudgetImpactRatioMnt))

                    'readjust total investment - maintenance
                    'INFRAINVESTMaint(R%, InfraType) = (MaintReqPhysStck - UnMaintPhysStck) * UnitCostInThoUSAnd / 1000 'in billion $
                    INFRAINVESTMAINT(R%, InfraType) = CSng(sPhysStckThou * Deppcnt(InfraType) * infrainvmaintpubshrm(R%, InfraType) * BudgetImpactRatioMnt * UnitCostInThoUSAnd / 1000) 'in billion $
                    INFRAINVESTMAINT(R%, InfraType) = CSng(INFRAINVESTMAINT(R%, InfraType) + sPhysStckThou * Deppcnt(InfraType) * (1 - infrainvmaintpubshrm(R%, InfraType)) * UnitCostInThoUSAnd / 1000) 'in billion $
                    'mti May 05, 2011 - end
                End If
                'mti October 20, 2011 - end

            End If



            'adjust physical stock with reduced new constructions and lack of maintenance
            PhysStckDmndThou = AMAX(0.001, PhysStckDmndThou - UnMaintPhysStck - NewConCut) '1000


            'readjust total investment - new construction
            INFRAINVESTNEW(R%, InfraType) = CSng(PhysStckAdded * UnitCostInThoUSAnd / 1000)  'in billion $

        End Sub

        Private Sub HandleExtraFundNonWatSan(R%, iINFRAINVESTNEWPUBShareRoadPaved As Single, iINFRAINVESTNEWPUBShareRoadUnPaved As Single, iINFRAINVESTNEWPUBShareElectricityGen As Single)
            'mti June 04, 2012 - new proc
            'watsan? (initial budgeting done only)
            '       If RegName(R%) = "Saudi Arabia" Then Stop

            'Calculate how much is extra
            '------------------------
            ExtraFundTotal(R%) = (INFRABUDDEMNEW(R%, InfraTot) + INFRABUDDEMMNT(R%, InfraTot)) * (sInfraBudImpact(R%) - 1)

            'demand by candidate sectors
            '------------------------

            'RoadPaved, RoadUnpaved and ElecGen are always candidates
            '---------------------

            'invest (only for public; no change in private)
            ExtraFundUsed(R%) = 0
            TotalDemandFromCandidates(R%) = 0

            'give road (paved and unpaved) per their share in total in the initial years

            'Paved Roads
            ExtraFundAllocated(R%, RoadPaved) = ExtraFundTotal(R%) * iINFRAINVESTNEWPUBShareRoadPaved 'billions of dollar
            TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, RoadPaved)

            ExtraFundAllocated(R%, RoadUnpaved) = ExtraFundTotal(R%) * iINFRAINVESTNEWPUBShareRoadUnPaved  'billions of dollar
            TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, RoadUnpaved)

            ExtraFundAllocated(R%, ElectricityGen) = ExtraFundTotal(R%) * iINFRAINVESTNEWPUBShareElectricityGen 'billions of dollar
            TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, ElectricityGen)


            '            'revise extra fund total by subtracting those given to road and elc. gen
            '            ExtraFundTotal(R%) = ExtraFundTotal(R%) - ExtraFundUsed(R%)

            'Sectors other than road and elecgen (access gap sectors)
            '------------------------------------------------------

            'maximum % access gap attempted to be met by extra fund
            Dim MaxGapToBeMet As Single? = 10

            'mti July 05, 2012 -start
            MaxGapToBeMet = Amin(5, (100 - INFRAELECACC(R%, Rural)) / 10) 'meet the gap gradually but faster for those who are far off from universal access

            Dim PotPhysStckThou = AMAX(0, Amin(MaxGapToBeMet, 100 - INFRAELECACC(R%, Rural))) * CDbl((sPOPTotal(R%) - SPOPURBAN(R%)) * 10 / HHSIZE(R%)) 'thousand connections
            ExtraFundAllocated(R%, ElectricityAccRural) = CSng(If(PotPhysStckThou * INFRAELECACCRURALCOST(R%) / 1000, 0)) 'in billion $
            TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, ElectricityAccRural)

            'mti July 05, 2012 -start
            MaxGapToBeMet = Amin(5, (100 - INFRAELECACC(R%, Urban)) / 10) 'meet the gap gradually but faster for those who are far off from universal access

            PotPhysStckThou = AMAX(0, Amin(MaxGapToBeMet, 100 - INFRAELECACC(R%, Urban))) * CDbl(SPOPURBAN(R%) * 10 / HHSIZE(R%)) 'thousand connections
            ExtraFundAllocated(R%, ElectricityAccUrban) = CSng(If(PotPhysStckThou * INFRAELECACCURBANCOST(R%) / 1000, 0)) 'in billion $
            TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, ElectricityAccUrban)

            'mti July 05, 2012 -start
            MaxGapToBeMet = Amin(5, 10 * AMAX(0, (1 - LANDIRAREAEQUIP(R%) / landirareasat(R%))))  'meet the gap gradually but faster for those who are far off from universal access

            PotPhysStckThou = AMAX(0, Amin(LANDIRAREAEQUIP(R%) * MaxGapToBeMet / 100, landirareasat(R%) - LANDIRAREAEQUIP(R%))) 'thousands
            ExtraFundAllocated(R%, Irrigation) = CSng(PotPhysStckThou * LANDIRCOST(R%) / 1000) 'in billion $
            TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, Irrigation)

            'sanitation - improved can go either direction; ignore

            'mti July 05, 2012 -start
            'mti 2022/09/21 start
            If watsafenewsw < 1 Then
                MaxGapToBeMet = Amin(5, (100 - SANITATION(R%, 3)) / 10) 'meet the gap gradually but faster for those who are far off from universal access

                PotPhysStckThou = AMAX(0, Amin(MaxGapToBeMet, 100 - SANITATION(R%, 3))) * CDbl(POP(R%) * 10 / HHSIZE(R%)) 'thoUSAnd connections
                ExtraFundAllocated(R%, SanitationHH) = CSng(PotPhysStckThou * SANITATIONCOST(R%) / 1000) 'in billion $
                TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, SanitationHH)
            Else
                CalcInfraFundingGapWatsanNew(R%, SanitationImproved, SanitationHH, SanitHH_Funding_Gap, sSanitHH, SANITHHCOSTLOWER, SANITHHCOSTHIGHER, sanithhcostlowershr)
                TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, SanitationHH) + ExtraFundAllocated(R%, SanitationImproved)
            End If
            'mti 2022/09/21 end

            'water - improved can go either direction; ignore

            'mti July 05, 2012 -start
            'mti 2022/09/21 start
            If watsafenewsw < 1 Then
                MaxGapToBeMet = Amin(5, (100 - WATSAFE(R%, 3)) / 10) 'meet the gap gradually but faster for those who are far off from universal access

                PotPhysStckThou = AMAX(0, Amin(MaxGapToBeMet, 100 - WATSAFE(R%, 3))) * CDbl(POP(R%) * 10 / HHSIZE(R%)) 'thoUSAnd connections
                ExtraFundAllocated(R%, SafeWaterHH) = CSng(PotPhysStckThou * WATSAFECOST(R%) / 1000) 'in billion $
                TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, SafeWaterHH)
            Else
                CalcInfraFundingGapWatsanNew(R%, SafeWaterImproved, SafeWaterHH, WaterHH_Funding_Gap, sWaterHH, WATERHHCOSTLOWER, WATERHHCOSTHIGHER, waterhhcostlowershr)
                'mti 2022/09/25 start
                'TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, SafeWaterHH) + ExtraFundAllocated(R%, SafeWateImproved)
                TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, SafeWaterHH) + ExtraFundAllocated(R%, SafeWaterImproved)
                'mti 2022/09/25 start
            End If
            'mti 2022/09/21 End

            'should not bound watwastegrowth by sanitation growth; the bound has already been done; now they should move equally

            'mti July 05, 2012 -start
            MaxGapToBeMet = Amin(5, (100 - WATWASTETREAT(R%)) / 10) 'meet the gap gradually but faster for those who are far off from universal access


            PotPhysStckThou = AMAX(0, Amin(MaxGapToBeMet, 100 - WATWASTETREAT(R%))) * POP(R%) * 10 'thousand connections
            ExtraFundAllocated(R%, WasteWater) = CSng(PotPhysStckThou * WATWASTETREATCOST(R%) / 1000) 'in billion $
            TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, WasteWater)

            'ignore telephone

            'mti July 05, 2012 -start
            MaxGapToBeMet = Amin(10, (ictmobilsaturation - ICTMOBIL(R%)) / 5) '(ICT can be built faster than traditional) meet the gap gradually but faster for those who are far off from universal access

            PotPhysStckThou = AMAX(0, Amin(MaxGapToBeMet, ictmobilsaturation - ICTMOBIL(R%))) * POP(R%) * 10 'thousand connections
            ExtraFundAllocated(R%, Mobile) = CSng(PotPhysStckThou * ICTMOBILCOST(R%) / 1000) 'in billion $
            TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, Mobile)

            'mti June 14, 2012 - BBCap - start
            Dim ictbroadcap As Single = 50

            'mti July 05, 2012 -start
            MaxGapToBeMet = Amin(10, (ictbroadcap - ICTBROAD(R%)) / 5) '(ICT can be built faster than traditional) meet the gap gradually but faster for those who are far off from universal access

            'PotPhysStckThou = Amin(MaxGapToBeMet, 100 - ICTBROAD(R%)) * POP(R%) * 10 'thousand connections
            PotPhysStckThou = AMAX(0, Amin(MaxGapToBeMet, ictbroadcap - ICTBROAD(R%))) * POP(R%) * 10 'thousand connections
            'mti June 14, 2012 - BBCap - end

            ExtraFundAllocated(R%, Broadband) = CSng(PotPhysStckThou * ICTBROADCOST(R%) / 1000) 'in billion $
            TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, Broadband)

            'should not bound mobile BB by mobile; the bound has already been done; now they should move equally
            'mti July 05, 2012 -start
            MaxGapToBeMet = Amin(10, (ictmobilsaturation - ICTBROADMOBIL(R%)) / 5) '(ICT can be built faster than traditional) meet the gap gradually but faster for those who are far off from universal access

            PotPhysStckThou = AMAX(0, Amin(MaxGapToBeMet, ictmobilsaturation - ICTBROADMOBIL(R%))) * POP(R%) * 10 'thousand connections
            ExtraFundAllocated(R%, BroadbandMobile) = CSng(PotPhysStckThou * ICTBROADMOBILCOST(R%) / 1000) 'in billion $
            TotalDemandFromCandidates(R%) = TotalDemandFromCandidates(R%) + ExtraFundAllocated(R%, BroadbandMobile)


            'Use extra fund; normalized by totaldemand
            'Roads            
            'If IY = 3 And R = 38 Then Stop

            Dim PhysStckDmndThouPaved As Double = LANDAREA(R%) * INFRAROAD(R%) * INFRAROADPAVEDPCNT(R%) / 100  'stock in Thousands
            CalcInfraUseExtraBudget(PhysStckDmndThouPaved, INFRAROADPAVEDCOST(R%) * 1000, ExtraFundAllocated(R%, RoadPaved), INFRAINVESTNEWPUB(R%, RoadPaved), INFRAINVESTNEW(R%, RoadPaved), ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))
            Dim PhysStckDmndThouUnPaved As Double = LANDAREA(R%) * INFRAROAD(R%) * (100 - INFRAROADPAVEDPCNT(R%)) / 100 'stock in Thousands
            CalcInfraUseExtraBudget(PhysStckDmndThouUnPaved, INFRAROADUNPAVEDCOST(R%) * 1000, ExtraFundAllocated(R%, RoadUnpaved), INFRAINVESTNEWPUB(R%, RoadUnpaved), INFRAINVESTNEW(R%, RoadUnpaved), ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))
            INFRAROAD(R%) = CSng((PhysStckDmndThouPaved + PhysStckDmndThouUnPaved) / LANDAREA(R%))  'km/1000 hectare

            'note from MTI: road density are not bound here by 100: will come back later (needs recalc)

            INFRAROADPAVEDPCNT(R%) = CSng(100 * PhysStckDmndThouPaved / (PhysStckDmndThouPaved + PhysStckDmndThouUnPaved))


            'revise road access- will be done at the end of IfsInfra

            'ElecGen
            Dim PhysStckDmndThou As Double = INFRAELECGENCAP(R%) * CDbl(POP(R%)) * 1000
            CalcInfraUseExtraBudget(PhysStckDmndThou, INFRAELECGENCOST(R%), ExtraFundAllocated(R%, ElectricityGen), INFRAINVESTNEWPUB(R%, ElectricityGen), INFRAINVESTNEW(R%, ElectricityGen), ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))

            INFRAELECGENCAP(R%) = CSng(PhysStckDmndThou / (AMAX(0.000001, CDbl(POP(R%))) * 1000))



            'proportionate investment in (in non-road/elec gen) candidate sectors - new con only


            'ElecAccRural
            '                    ExtraFundSector = 1000000 * ExtraFundTotal(R%) * INFRAINVESTNEWPUB(R%, ElectricityAccRural) / TotalDemandFromCandidates(R%) 'converted to thousands of dollar
            '
            '                    PhysStckRemaining = (100 - INFRAELECACC(R%, Rural)) * CDbl((sPOPTotal(R%) - SPOPURBAN(R%)) * 10 / HHSIZE(R%)) 'in thousand connections
            '                    ExtrafundNeededforRemaining = 1000 * PhysStckRemaining * INFRAELECACCRURALCOST(R%) 'thousands of dollar
            '                    ExtraFundSector = Amin(ExtraFundSector, ExtrafundNeededforRemaining)
            '
            '                    PhysStckAdd = 0.001 * ExtraFundSector / INFRAELECACCRURALCOST(R%)  ' physical var converted to thousands
            '                    PhysStckNewElecAccRural = PhysStckAdd + INFRAELECACC(R%, Rural) * CDbl((sPOPTotal(R%) - SPOPURBAN(R%)) * 10 / HHSIZE(R%))

            If sPOPTotal(R%) > SPOPURBAN(R%) Then
                PhysStckDmndThou = CSng(INFRAELECACC(R%, Rural) * CDbl((sPOPTotal(R%) - SPOPURBAN(R%)) * 10 / HHSIZE(R%)))
                CalcInfraUseExtraBudget(PhysStckDmndThou, INFRAELECACCRURALCOST(R%), ExtraFundAllocated(R%, ElectricityAccRural), INFRAINVESTNEWPUB(R%, ElectricityAccRural), INFRAINVESTNEW(R%, ElectricityAccRural), ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))
                INFRAELECACC(R%, Rural) = CSng(PhysStckDmndThou / CDbl((sPOPTotal(R%) - SPOPURBAN(R%)) * 10 / HHSIZE(R%)))
            End If

            'ElecAccUrban
            '                    ExtraFundSector = 1000000 * ExtraFundTotal(R%) * INFRAINVESTNEWPUB(R%, ElectricityAccUrban) / TotalDemandFromCandidates(R%) 'converted to thousands of dollar
            '
            '                    PhysStckRemaining = (100 - INFRAELECACC(R%, Urban)) * CDbl(SPOPURBAN(R%) * 10 / HHSIZE(R%)) 'in thousand connections
            '                    ExtrafundNeededforRemaining = 1000 * PhysStckRemaining * INFRAELECACCURBANCOST(R%) 'thousands of dollar
            '                    ExtraFundSector = Amin(ExtraFundSector, ExtrafundNeededforRemaining)
            '
            '                    PhysStckAdd = 0.001 * ExtraFundSector / INFRAELECACCURBANCOST(R%)  ' physical var converted to thousands
            '                    PhysStckNewElecAccUrban = PhysStckAdd + INFRAELECACC(R%, Urban) * CDbl(SPOPURBAN(R%) * 10 / HHSIZE(R%))
            '
            '                    INFRAINVESTNEWPUB(R%, ElectricityAccUrban) = INFRAINVESTNEWPUB(R%, ElectricityAccUrban) + ExtraFundSector
            '                    INFRAELECACC(R%, Urban) = PhysStckNewElecAccUrban / CDbl(SPOPURBAN(R%) * 10 / HHSIZE(R%))
            '                    ExtraFundUsed(R%) = ExtraFundUsed(R%) + ExtraFundSector



            PhysStckDmndThou = CSng(INFRAELECACC(R%, Urban) * CDbl(SPOPURBAN(R%) * 10 / HHSIZE(R%)))
            CalcInfraUseExtraBudget(PhysStckDmndThou, INFRAELECACCURBANCOST(R%), ExtraFundAllocated(R%, ElectricityAccUrban), INFRAINVESTNEWPUB(R%, ElectricityAccUrban), INFRAINVESTNEW(R%, ElectricityAccUrban), ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))
            INFRAELECACC(R%, Urban) = CSng(PhysStckDmndThou / CDbl(SPOPURBAN(R%) * 10 / HHSIZE(R%)))

            'recalc infraelecacc total
            INFRAELECACC(R%, Total) = (SPOPURBAN(R%) * INFRAELECACC(R%, Urban) + (sPOPTotal(R%) - SPOPURBAN(R%)) * INFRAELECACC(R%, Rural)) / sPOPTotal(R%)

            'If RegName$(R%) = "Palestine" Then Stop '4

            'Irrigation
            'jrs 2020/08/05
            Dim tmpLANDIRAREAEQUIP As Double
            tmpLANDIRAREAEQUIP = LANDIRAREAEQUIP(R%)
            CalcInfraUseExtraBudget(tmpLANDIRAREAEQUIP, LANDIRCOST(R%), ExtraFundAllocated(R%, Irrigation), INFRAINVESTNEWPUB(R%, Irrigation), INFRAINVESTNEW(R%, Irrigation), ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))
            LANDIRAREAEQUIP(R%) = CSng(tmpLANDIRAREAEQUIP)


            'WatsafeHH; Sanitation HH; WatWaste HH goes to IfsInfraWatsan

            'ICTMOBIL
            PhysStckDmndThou = CSng(ICTMOBIL(R%) * CDbl(POP(R%)) * 10) 'Demanded Connections (stock) in Thousands This Year
            CalcInfraUseExtraBudget(PhysStckDmndThou, ICTMOBILCOST(R%), ExtraFundAllocated(R%, Mobile), INFRAINVESTNEWPUB(R%, Mobile), INFRAINVESTNEW(R%, Mobile), ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))
            ICTMOBIL(R%) = CSng(PhysStckDmndThou / AMAX(0.000001, POP(R%)) / 10)

            'ICTBROAD
            PhysStckDmndThou = CSng(ICTBROAD(R%) * CDbl(POP(R%)) * 10) 'Demanded Connections (stock) in Thousands This Year
            CalcInfraUseExtraBudget(PhysStckDmndThou, ICTBROADCOST(R%), ExtraFundAllocated(R%, Broadband), INFRAINVESTNEWPUB(R%, Broadband), INFRAINVESTNEW(R%, Broadband), ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))
            ICTBROAD(R%) = CSng(PhysStckDmndThou / AMAX(0.000001, POP(R%)) / 10)


            'ICTBROADMobil
            PhysStckDmndThou = CSng(ICTBROADMOBIL(R%) * CDbl(POP(R%)) * 10) 'Demanded Connections (stock) in Thousands This Year
            CalcInfraUseExtraBudget(PhysStckDmndThou, ICTBROADMOBILCOST(R%), ExtraFundAllocated(R%, BroadbandMobile), INFRAINVESTNEWPUB(R%, BroadbandMobile), INFRAINVESTNEW(R%, BroadbandMobile), ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%))
            ICTBROADMOBIL(R%) = CSng(PhysStckDmndThou / AMAX(0.000001, POP(R%)) / 10)
            'any candidate goes up the limit with extra?

            'if yes, put the extra in the box cart

            'jrs 2024/03/25
            INFRAINVESTNEWPUB(R%, InfraTot) = 0
            For j = 1 To NInfraType%
                INFRAINVESTNEWPUB(R%, InfraTot) = INFRAINVESTNEWPUB(R%, InfraTot) + INFRAINVESTNEWPUB(R%, j)
            Next

        End Sub

        'jrs 2020/08/05 changed PhysStckDmndThou from Single to Double, INFRAINVESTExtraPUBSector from s to d, ExtraFundTotalAvailable from S to D, TotalDemandFromCandidateSectors from S to D, ExtraFundUsedUpSoFar from S to D
        Private Sub CalcInfraUseExtraBudget(ByRef PhysStckDmndThou As Double, UnitCostInThousand As Single, INFRAINVESTExtraPUBSector As Double, ByRef INFRAINVESTNEWPUBSector As Single, ByRef INFRAINVESTNEWTOTSector As Single, ExtraFundTotalAvailable As Double, TotalDemandFromCandidateSectors As Double, ByRef ExtraFundUsedUpSoFar As Double)
            'ttt What ByRef?
            'mti June 04, 2012 - new proc

            Dim INFRAINVESTNEWPriSector = INFRAINVESTNEWTOTSector - INFRAINVESTNEWPUBSector

            Dim ExtraFundSector = 1000000 * ExtraFundTotalAvailable * INFRAINVESTExtraPUBSector / TotalDemandFromCandidateSectors 'converted to thousands of dollar
            Dim PhysStckAdd As Double
            'mti 2013/12/20 start
            If UnitCostInThousand <> 0 Then
                PhysStckAdd = 0.001 * ExtraFundSector / UnitCostInThousand  ' physical var converted to thousands
            Else
                PhysStckAdd = 0
            End If
            'mti 2013/12/20 end

            PhysStckDmndThou = PhysStckAdd + PhysStckDmndThou

            INFRAINVESTNEWPUBSector = CSng(INFRAINVESTNEWPUBSector + ExtraFundSector / 1000000)

            INFRAINVESTNEWTOTSector = INFRAINVESTNEWPUBSector + INFRAINVESTNEWPriSector
            ExtraFundUsedUpSoFar = ExtraFundUsedUpSoFar + ExtraFundSector / 1000000

        End Sub

        'jrs 2023/07/25 added byref to AccessRate, INFRAINVESTNEWTOTSector, ExtraFundUsedUpSoFar
        Private Sub CalcInfraUseExtraBudgetWatSanNew(R%, Add_Categ%, ByRef AccessRate(,,) As Single, Funding_Gap(,,) As Single, UnitCost_Lower(,,) As Single, UnitCost_Higher(,,) As Single, shrlowrcost(,,) As Single, InvestByLocationCap(,,) As Single,
                INFRAINVESTExtraPUBSector As Double, INFRAINVESTNEWPUBSector As Single, ByRef INFRAINVESTNEWTOTSector As Single, ExtraFundTotalAvailable As Double, TotalDemandFromCandidateSectors As Double, ByRef ExtraFundUsedUpSoFar As Double)
            'mti 2022/09/21 - new proc
            'R%, UnitCost_Lower(), UnitCost_Higher(), Reqd_AccessRate(), Prev_AccessRate(), Real_AccessRate()
            'PhysStckDmndThou, SANITATIONCOST(R%), ExtraFundAllocated(R%, SanitationHH), INFRAINVESTNEWPUB(R%, SanitationHH), INFRAINVESTNEW(R%, SanitationHH), ExtraFundTotal(R%), TotalDemandFromCandidates(R%), ExtraFundUsed(R%)


            'If (Funding_Gap(R%, Add_Categ, Rural) + Funding_Gap(R%, Add_Categ, Urban)) >0 and ExtraFundSector>0

            Dim Pop_Loc_Tot(2), Pop_Connected(2), Pop_Added(2), Pop_Final(2), ExtraFund_Loc(2), AvgCost_Loc(2) As Single
            Dim Loc%

            'mti 2022/09/25 start
            'If ExtraFundSector = 0 Or (Funding_Gap(R%, Add_Categ, Rural) + Funding_Gap(R%, Add_Categ, Urban)) = 0 Then Exit Sub
            'mti 2022/09/25 end

            Dim INFRAINVESTNEWPriSector = INFRAINVESTNEWTOTSector - INFRAINVESTNEWPUBSector

            'keep in billion dollars
            Dim ExtraFundSector = ExtraFundTotalAvailable * INFRAINVESTExtraPUBSector / TotalDemandFromCandidateSectors

            'mti 2022/09/25 start
            If ExtraFundSector = 0 Or (Funding_Gap(R%, Add_Categ, Rural) + Funding_Gap(R%, Add_Categ, Urban)) = 0 Then Exit Sub
            'mti 2022/09/25 end

            ExtraFund_Loc(Rural) = CSng(ExtraFundSector * Funding_Gap(R%, Add_Categ, Rural) / (Funding_Gap(R%, Add_Categ, Rural) + Funding_Gap(R%, Add_Categ, Urban)))
            ExtraFund_Loc(Urban) = CSng(ExtraFundSector - ExtraFund_Loc(Rural))

            Pop_Loc_Tot(Rural) = sPOPTotal(R%) - SPOPURBAN(R%)
            Pop_Loc_Tot(Urban) = SPOPURBAN(R%)

            Dim shr_lowr_cost As Single
            'mti 2022/09/25 start
            '        For Loc% = 1 To 2
            '            shr_lowr_cost = shrlowrcost(R%, Add_Categ%, Loc%)
            '            If UnitCost_Higher(R%, Add_Categ%, Loc%) = 0 Then shr_lowr_cost = 1
            '            If UnitCost_Lower(R%, Add_Categ%, Loc%) = 0 Then shr_lowr_cost = 0
            '        Next

            For Loc% = 1 To 2
                shr_lowr_cost = shrlowrcost(R%, Add_Categ%, Loc%)
                If UnitCost_Higher(R%, Add_Categ%, Loc%) = 0 Then shr_lowr_cost = 1
                If UnitCost_Lower(R%, Add_Categ%, Loc%) = 0 Then shr_lowr_cost = 0
                'mti 2022/09/25 End
                AvgCost_Loc(Loc%) = UnitCost_Lower(R%, Add_Categ%, Loc%) * GDPPC(R%) / 100 * shr_lowr_cost   'thousand dollars
                AvgCost_Loc(Loc%) = AvgCost_Loc(Loc%) + UnitCost_Higher(R%, Add_Categ%, Loc%) * GDPPC(R%) / 100 * (1 - shr_lowr_cost)
            Next

            For Loc% = 1 To 2
                'mti 2022/09/25 Start
                'Pop_Connected(Loc%) = AccessRate(R%, Add_Categ%, Rural) / 100 * Pop_Loc_Tot(Loc%)   'million pop
                Pop_Connected(Loc%) = AccessRate(R%, Add_Categ%, Loc%) / 100 * Pop_Loc_Tot(Loc%)   'million pop
                'mti 2022/09/25 End
                Pop_Added(Loc%) = 0
                If AvgCost_Loc(Loc%) <> 0 Then
                    Pop_Added(Loc%) = ExtraFund_Loc(Loc%) / AvgCost_Loc(Loc%) ' billion dollar by thousand dollar
                End If
                Pop_Final(Loc%) = Amin(Pop_Loc_Tot(Loc%), Pop_Connected(Loc%) + Pop_Added(Loc%))
                Pop_Added(Loc%) = Pop_Final(Loc%) - Pop_Connected(Loc%)
                If Pop_Loc_Tot(Loc%) > 0 Then
                    AccessRate(R%, Add_Categ%, Loc%) = Amin(100, 100 * Pop_Final(Loc%) / Pop_Loc_Tot(Loc%))
                Else
                    Pop_Added(Loc%) = 0
                End If
            Next


            If Add_Categ = Basic Then
                CalcInfraUseExtraBudgetWatSanNewTakeFromLower(R%, Basic, Limited, AccessRate, Pop_Connected, Pop_Added, Pop_Final, Pop_Loc_Tot)
                '            For Loc% = 1 To 2
                '                Pop_Con_Below_AddCateg = 0
                '                For Srvc% = 1 To Add_Categ - 1
                '                    Pop_Con_Below_AddCateg = Pop_Con_Below_AddCateg + AccessRate(R%, Srvc%, Loc%) / 100 * Pop_Loc_Tot(Loc%)
                '                Next
                '                If Pop_Added(Loc%) > Pop_Con_Below_AddCateg Then
                '                    Pop_Added(Loc%) = Pop_Con_Below_AddCateg
                '                    Pop_Final(Loc%) = Pop_Connected(Loc%) + Pop_Added(Loc%)
                '                    If Pop_Loc_Tot(Loc%) > 0 Then AccessRate(R%, Add_Categ%, Loc%) = Amin(100, 100 * Pop_Final(Loc%) / Pop_Loc_Tot(Loc%))
                '                End If
                '                If Pop_Con_Below_AddCateg > 0 And Pop_Loc_Tot(Loc%) > 0 Then
                '                    For Srvc% = 1 To AddCateg - 1  'normalize total addition to basic across the three categories below
                '                        Pop_Con = AccessRate(R%, Srvc%, Loc%) / 100 * Pop_Loc_Tot(Loc%)
                '                        Pop_Sub = Pop_Added(Loc%) * Pop_Con / Pop_Con_Below_AddCateg
                '                        AccessRate(R%, Srvc%, Loc%) = AMAX(0, Amin(100, 100 * (Pop_Con - Pop_Sub) / Pop_Loc_Tot(Loc%)))
                '                    Next
                '                End If
                '            Next
            ElseIf Add_Categ = SafelyManaged Then
                Dim UseLower3 = False
                For Loc% = 1 To 2
                    'mti 2022/09/29 start
                    Dim Categ% = Add_Categ - 1
                    Dim Pop_Con_Below_AddCateg = AccessRate(R%, Categ, Loc%) / 100 * Pop_Loc_Tot(Loc%)
                    If Pop_Con_Below_AddCateg = 0 Then
                        Categ% = Categ% - 1
                        Pop_Con_Below_AddCateg = AccessRate(R%, Categ, Loc%) / 100 * Pop_Loc_Tot(Loc%)
                    End If
                    If Pop_Con_Below_AddCateg = 0 Then
                        Categ% = Categ% - 1
                        Pop_Con_Below_AddCateg = AccessRate(R%, Categ, Loc%) / 100 * Pop_Loc_Tot(Loc%)
                    End If
                    If Pop_Con_Below_AddCateg = 0 Then
                        Categ% = Categ% - 1 'surface/ODF
                        Pop_Con_Below_AddCateg = AccessRate(R%, Categ, Loc%) / 100 * Pop_Loc_Tot(Loc%)
                    End If
                    If Pop_Added(Loc%) <= Pop_Con_Below_AddCateg Then
                        Dim Pop_Con = AccessRate(R%, Categ, Loc%) / 100 * Pop_Loc_Tot(Loc%)
                        Dim Pop_Sub = Pop_Added(Loc%)

                        If Pop_Loc_Tot(Loc%) > 0 Then
                            AccessRate(R%, Categ, Loc%) = AMAX(0, Amin(100, 100 * (Pop_Con - Pop_Sub) / Pop_Loc_Tot(Loc%)))
                            Pop_Added(Loc%) = 0 'all additions done
                        End If
                    ElseIf Pop_Con_Below_AddCateg > 0 Then
                        Pop_Connected(Loc%) = Pop_Connected(Loc%) + Pop_Con_Below_AddCateg
                        Pop_Added(Loc%) = Pop_Added(Loc%) - Pop_Con_Below_AddCateg  'some subtraction could not be accommodated in next lower category; go further down
                        AccessRate(R%, Categ, Loc%) = 0 'nothing left; all taken - check this added 09/29 11:52 PM
                        If Pop_Added(Loc%) > 0.000001 Then UseLower3 = True         'will 'take from lower categories
                    End If
                    'mti 2022/09/29 End
                Next

                If UseLower3 Then
                    CalcInfraUseExtraBudgetWatSanNewTakeFromLower(R%, SafelyManaged, Limited, AccessRate, Pop_Connected, Pop_Added, Pop_Final, Pop_Loc_Tot)
                End If
            End If

            'some of the additional funds might be unused
            Dim ExtraFundUsedUp As Single = 0
            For Loc% = 1 To 2
                ExtraFund_Loc(Loc%) = Pop_Added(Loc%) * AvgCost_Loc(Loc%)
                ExtraFundUsedUp = ExtraFundUsedUp + ExtraFund_Loc(Loc%)

                'Recompute capital investment
                InvestByLocationCap(R%, Add_Categ, Loc%) = InvestByLocationCap(R%, Add_Categ, Loc%) + ExtraFund_Loc(Loc%)
            Next

            'Recompute funding variables
            INFRAINVESTNEWPUBSector = INFRAINVESTNEWPUBSector + ExtraFundUsedUp '/ 1000000  'Check units here  'ExtraFundSector / 1000000

            INFRAINVESTNEWTOTSector = INFRAINVESTNEWPUBSector + INFRAINVESTNEWPriSector
            ExtraFundUsedUpSoFar = ExtraFundUsedUpSoFar + ExtraFundUsedUp '/ 1000000     ' + ExtraFundSector / 1000000

        End Sub
        '=============== Infrastr.bas =================
        Private Sub CalcInfraUseExtraBudgetWatSanNewTakeFromLower(R%, AddCateg%, SubCategTop%, ByRef AccessRate(,,) As Single, Pop_Connected() As Single, Pop_Added() As Single, Pop_Final() As Single, Pop_Loc_Tot() As Single)
            '----------------------------------------------
            'mti 2022/09/21 New procedure
            'Pop_Added() -> Pop_Added()
            Dim Loc%

            For Loc% = 1 To 2
                Dim Pop_Con_Below_AddCateg As Single = 0
                For Srvc% = 1 To SubCategTop
                    Pop_Con_Below_AddCateg = Pop_Con_Below_AddCateg + AccessRate(R%, Srvc%, Loc%) / 100 * Pop_Loc_Tot(Loc%)
                Next
                If Pop_Added(Loc%) > Pop_Con_Below_AddCateg Then
                    Pop_Added(Loc%) = Pop_Con_Below_AddCateg
                    Pop_Final(Loc%) = Pop_Connected(Loc%) + Pop_Added(Loc%)
                    If Pop_Loc_Tot(Loc%) > 0 Then AccessRate(R%, AddCateg%, Loc%) = Amin(100, 100 * Pop_Final(Loc%) / Pop_Loc_Tot(Loc%))
                End If
                If Pop_Con_Below_AddCateg > 0 And Pop_Loc_Tot(Loc%) > 0 Then
                    For Srvc% = 1 To SubCategTop  'normalize total addition to basic across the three categories below
                        Dim Pop_Con = AccessRate(R%, Srvc%, Loc%) / 100 * Pop_Loc_Tot(Loc%)
                        Dim Pop_Sub = Pop_Added(Loc%) * Pop_Con / Pop_Con_Below_AddCateg
                        AccessRate(R%, Srvc%, Loc%) = AMAX(0, Amin(100, 100 * (Pop_Con - Pop_Sub) / Pop_Loc_Tot(Loc%)))
                    Next
                End If
            Next
        End Sub

        '=============== Infrastr.bas =================
        Private Sub CalcInfraFundingGapWatsanNew(R%, Fund_Cat_Basic%, Fund_Cat_SafelyManaged%, Funding_Gap(,,) As Single, Prev_AccessRate(,,) As Single, UnitCost_Lower(,,) As Single, UnitCost_Higher(,,) As Single, shrlowrcost(,,) As Single)
            '----------------------------------------------
            'mti 2022/09/21 New procedure
            Dim Loc%
            Dim Fund_Needed_Basic As Single = 0               'Fund required to convert all of the less than basic (i.e., surface, unimproved and limited) connections to basic
            Dim Fund_Needed_SafelyManaged As Single = 0    'Fund required to convert all of the basic connections to safely managed

            For Loc% = 1 To 2

                For Srvc% = 4 To 5  'Basic (4) and Sfely Managed (5)

                    '--- Financing required to bring everyone to this level  ---------
                    Dim Access_Gap As Single = 0
                    If Srvc% = 4 Then   '--- Add up the share of population who are still below this level of ladder ---------
                        For SrvL% = 1 To Srvc% - 1
                            Access_Gap = Access_Gap + Prev_AccessRate(R%, SrvL%, Loc%)
                        Next
                    ElseIf Srvc% = 5 Then
                        Access_Gap = Access_Gap + Prev_AccessRate(R%, Srvc% - 1, Loc%)
                    End If

                    Dim shr_lowr_cost = shrlowrcost(R%, Srvc%, Loc%)
                    If UnitCost_Higher(R%, Srvc%, Loc%) = 0 Then shr_lowr_cost = 1
                    If UnitCost_Lower(R%, Srvc%, Loc%) = 0 Then shr_lowr_cost = 0

                    Funding_Gap(R%, Srvc, Loc%) = UnitCost_Lower(R%, Srvc%, Loc%) * GDPPC(R%) / 100 * shr_lowr_cost
                    Funding_Gap(R%, Srvc, Loc%) = Funding_Gap(R%, Srvc, Loc%) + UnitCost_Higher(R%, Srvc%, Loc%) * GDPPC(R%) / 100 * (1 - shr_lowr_cost)
                    Funding_Gap(R%, Srvc, Loc%) = Funding_Gap(R%, Srvc, Loc%) * Access_Gap / 100
                    If Loc% = Rural Then Funding_Gap(R%, Srvc, Loc%) = Funding_Gap(R%, Srvc, Loc%) * (sPOPTotal(R%) - SPOPURBAN(R%))
                    If Loc% = Urban Then Funding_Gap(R%, Srvc, Loc%) = Funding_Gap(R%, Srvc, Loc%) * SPOPURBAN(R%)

                    If Srvc% = Basic Then Fund_Needed_Basic = Fund_Needed_Basic + Funding_Gap(R%, Srvc, Loc%)
                    If Srvc% = SafelyManaged Then Fund_Needed_SafelyManaged = Fund_Needed_SafelyManaged + Funding_Gap(R%, Srvc, Loc%)

                Next

            Next

            ExtraFundAllocated(R%, Fund_Cat_Basic) = Fund_Needed_Basic
            ExtraFundAllocated(R%, Fund_Cat_SafelyManaged) = Fund_Needed_SafelyManaged


        End Sub

        'jrs 2009/06/05
        Private Sub computeICTBroadExp(context As IForecastContext)
            'Adjusting Energy Demand with ICT drivers

            Dim rVal As Single, incCS As Single, incTec As Single
            Dim ICTSpendExp As Single
            'gvdb 2024/04/05
            'unused variable
            'Dim EffectAdd As Single
            Dim rValCS As Double

            Dim R As Integer

            If IY = 1 Then
                '        Stop

                ReDim SICTBroadExp(NR, 2)
                ReDim ICTBroadExpShift(NR)
                ReDim ICTSpendExpShift(NR)

                ReDim ICTBroadExp(NR)
                'jrs 2011/01/17 begin Using Annual Growth Rate
                ReDim ICTBroadGRExp(NR)
                'jrs 2011/01/17 end Using Annual Growth Rate
            End If

            For R = 1 To NR
                'If RegName(R%) = "Estonia" Then Stop


                'Expected ICT Spending
                '---------------------
                'jrs 2024/06/27 TbName$ = "GDP/Capita (PPP 2000) Versus ICT Expenditures % GDP (2005) Linear"
                Call XYTABL(TFNameICTExpendPctGDP, GDPPCP(R), rVal, context)

                If IY = 1 Then
                    'jrs 2016/05/24 removing multipliers in year 1 *** begin ***
                    '            If ictspendm(R) > 0 Then
                    '                ICTSpendExpShift(R%) = ICTSPEND(R) / ictspendm(R) - rVal
                    '                ICTSpendExp = ICTSPEND(R) / ictspendm(R)
                    '            Else
                    '                ICTSpendExpShift(R%) = -1 * rVal
                    '                ICTSpendExp = ICTSPEND(R)
                    '            End If
                    ICTSpendExpShift(R%) = ICTSPEND(R) - rVal
                    ICTSpendExp = ICTSPEND(R)
                    'jrs 2016/05/24 removing multipliers in year 1 *** end ***

                    'jrs 2011/01/17 begin Using Annual Growth Rate
                    ICTBroadGRExp(R) = ICTBroadGR(R)
                    'jrs 2011/01/17 end Using Annual Growth Rate
                Else
                    ICTSpendExp = rVal + ICTSpendExpShift(R)
                    ICTSpendExp = AMAX(Amin(ICTSpendExp, 100), 0)
                End If
                'If RegName(R%) = "Angola" Then Stop
                Call AnalFunc("ICT Broadband Access % of Population",
        New Single() {GDPPCP(R), GOVREGQUALExp(R), ICTSpendExp}, rVal,
        New String() {"GDPPCP", "GOVREGQUAL", "ICTSPEND"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                ICTBroadExp(R) = rVal
                If IY > 1 Then
                    rValCS = rVal + ConvergeOverTime(ICTBroadExpShift(R), 0, 100)
                    incCS = CSng(rValCS - SICTBroadExp(R, 1))

                    'mti April 2011 - start

                    'mti June 08, 2011 - start
                    'ictbroadtecinflection = ictmobilsaturation / 2
                    'mti June 08, 2011 - end

                    Dim ltfac As Single = 0.5
                    If SICTBroadExp(R, 2) < ictbroadtecinflection Then
                        incTec = (ictbroadtechighrt(R%) * ltfac - ictbroadteclowrt * ltfac) * SICTBroadExp(R, 2) / ictbroadtecinflection + ictbroadteclowrt * ltfac
                    Else
                        incTec = (ictbroadteclowrt * ltfac - ictbroadtechighrt(R%) * ltfac) * (SICTBroadExp(R, 2) - ictbroadtecinflection) / (100 - ictbroadtecinflection) + ictbroadtechighrt(R%) * ltfac
                    End If

                    '            'mti March 2011 - start - disable tech shift
                    '            'mti going back to old formulation
                    '            If SICTBroadExp(R, 2) < ictbroadtecinflection Then
                    '                incTec = (ictbroadtechighrt - ictbroadteclowrt) * SICTBroadExp(R, 2) / ictbroadtecinflection + ictbroadteclowrt
                    '            Else
                    '                incTec = (ictbroadteclowrt - ictbroadtechighrt) * (SICTBroadExp(R, 2) - ictbroadtecinflection) / (100 - ictbroadtecinflection) + ictbroadtechighrt
                    '            End If
                    '
                    '            'mti April 2011 - redo new formulation - start
                    '            incTec = 0
                    '            'mti April 2011 - end


                    'jrs 2011/01/17 *** begin: Using Annual Growth Rate ***
                    '******************************************************
                    Dim incTot As Single
                    If Not ICTBroadGRExp(R).HasValue Then
                        incTot = incTec + incCS
                    Else
                        'mti July 18, 2011 - Rebase 2010 ICT -start
                        'If IY > 5 Then
                        'mti July 18, 2011 - Rebase 2010 ICT - end
                        'mti June 08, 2011 -start
                        'incTot = HistWg * ICTBroadGR(R) * SBroad(R, 2) + (1 - HistWg) * (incTec + incCS)
                        'Instead of using the compoundig Growth Rate, use just the flat differece between the 2 last years
                        'F(y-1) - F(y-2) = F(y-1) * (1 - 1/(1+GR))
                        'incTot = HistWg * SBroad(R, 2) * (1 - 1 / (1 + ICTBroadGR(R))) + (1 - HistWg) * (incTec + incCS)
                        incTot = ConvergeOverTime(CSng(SICTBroadExp(R, 2) * (1 - 1 / (1 + ICTBroadGRExp(R)))), incTec + incCS, 10)

                        'new formulation (growth rate convergence, slower)
                        Dim gratem = (incTec + incCS) / AMAX(0.01, SICTBroadExp(R, 2)) 'growth rate from model
                        Dim gratec = ConvergeOverTime(CSng(ICTBroadGRExp(R)), gratem, 25) 'growth rate from previous year converge to that from model
                        incTot = gratec * SICTBroadExp(R, 2)

                        'mti July 18, 2011 - Rebase 2010 ICT -start
                        '                Else
                        '                    'mti July 18, 2011 - Rebase 2010 ICT -Start
                        '                    'revert to growth rate (as opposed to absolute growth)
                        '                    incTot = ICTBroadGRExp(R) * SICTBroadExp(R, 2)
                        '                    'incTot = ICTBroadGRExp(R)
                        '                    'mti July 18, 2011 - Rebase 2010 ICT -end
                        '                    'mti June 08, 2011 - end
                        '                End If
                        'mti July 18, 2011 - Rebase 2010 ICT -start
                    End If
                    'jrs 2011/01/17 *** end: Using Annual Growth Rate ****
                    '******************************************************

                    'Add Increase/Decrease to final value, this is the tec factor
                    'Total increment can't be more than 25% of the remaining gap to reach target of 100

                    'mti March 2011 -start
                    'mti July 18, 2011 - Rebase 2010 ICT -start
                    'If IY > 5 Then
                    'mti July 18, 2011 - Rebase 2010 ICT - end
                    ICTBroadExp(R) = SICTBroadExp(R, 2) + Amin(incTot, (100 - SICTBroadExp(R, 2)) / 4)
                    'mti July 18, 2011 - Rebase 2010 ICT -start
                    '            Else
                    '                ICTBroadExp(R) = SICTBroadExp(R, 2) + incTot
                    '            End If
                    'mti July 18, 2011 - Rebase 2010 ICT - end
                    'mti March 2011 -end

                    ICTBroadExp(R) = Amin(AMAX(ICTBroadExp(R), SICTBroadExp(R, 2)), 100)

                    'jrs 2011/01/17 begin Using Annual Growth Rate
                    'mti June 08, 2011 - start
                    'for first 5 years we use same absolute growth now
                    'mti July 18, 2011 - Rebase 2010 ICT - start
                    'If IY >= 5 Then
                    'mti July 18, 2011 - Rebase 2010 ICT -end

                    'mti March 2011 -start
                    If ICTBroadGRExp(R).HasValue Then
                        If SICTBroadExp(R, 2) <> 0 Then
                            'mti April 2011 - start
                            'this has to be bound for low initial value countries
                            'ICTBroadGRExp(R) = ICTBroadExp(R) / SICTBroadExp(R, 2) - 1
                            ICTBroadGRExp(R) = Amin(1.5F, ICTBroadExp(R) / SICTBroadExp(R, 2) - 1)
                            'mti April 2011 - end
                        End If
                    End If

                    'mti July 18, 2011 - Rebase 2010 ICT - start
                    'End If
                    'mti July 18, 2011 - Rebase 2010 ICT -end
                    'mti June 08, 2011 - end
                    'mti March 2011 - end
                    'jrs 2011/01/17 end Using Annual Growth Rate
                Else
                    ICTBroadExpShift(R) = ICTBROAD(R) - ICTBroadExp(R)
                    rValCS = ICTBROAD(R)
                    ICTBroadExp(R) = CSng(rValCS)
                End If

                SICTBroadExp(R, 1) = CSng(rValCS)
                SICTBroadExp(R, 2) = ICTBroadExp(R)

            Next R

        End Sub

        Private Sub ICTMobilForecast(SGOVREGQUAL() As Single, sGDPPCP() As Single,
                                     sPOPTotal() As Single, ByRef SMobil(,) As Single,
                                     ByRef ICTMobilGR() As Single?, ByRef ICTMobilBF() As Single,
                                     ByRef sICTMobilBF() As Single, ByRef sMobilBF(,) As Single,
                                     ByRef ICTMobilGRBF() As Single?, ByRef INFRAINVESTNEWBF(,) As Single,
                                     ByRef INFRAINVESTMAINTBF(,) As Single, DeppcntMobile As Double,
                                     SICTMobil() As Single, context As IForecastContext)
            '----------------------------------------------------------------------
            'All Years but First: Access to Mobile phones

            'mti 2013/12/20 start
            If context.HistoricMode AndAlso context.FirstYearHistory < 1990 Then
                For R% = 1 To NR%
                    ICTMOBIL(R) = 0
                    ICTMobilGR(R) = 0

                    ICTMOBILCOST(R%) = 0
                    INFRAINVESTNEW(R%, Mobile) = 0
                    INFRAINVESTMAINT(R%, Mobile) = 0

                    ICTMobilBF(R%) = 0
                    ICTMobilGRBF(R%) = 0

                    INFRAINVESTNEWBF(R%, Mobile) = 0
                    INFRAINVESTMAINTBF(R%, Mobile) = 0
                Next
                Exit Sub
            End If
            'mti 2013/12/20 end
            Dim rVal As Single = 0
            Dim SE As Single = 0
            Dim EffectSE As Single = 0
            Dim incCS As Single = 0
            Dim incTec As Single = 0
            Dim incTot As Single = 0
            Dim gratem As Single = 0
            Dim gratec As Single = 0
            Dim incCSBF As Single = 0
            Dim incTecBF As Single = 0
            Dim incTotBF As Single = 0
            Dim gratemBF As Single = 0
            Dim gratecBF As Single = 0
            For R% = 1 To NR%
                'Cross-sectional model (with SE Effect Calculation)
                '--------------------------------------------------------
                Call AnalFuncSE("ICT Mobile Access % of Population Rev 2010",
            New Single() {GDPPCP(R), SGOVREGQUAL(R)}, rVal, SE,
            New String() {"GDP2005PCPPP(MOSTRECENT)/ 1000 (Log)", "GovernanceRegQual(MOSTRECENT)+ 2.5 (Linear)"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                Dim MobilAdd As Single = 0
                If MOBILshift(R) > 0 Then
                    MobilAdd = MOBILshift(R)
                Else
                    MobilAdd = ConvergeOverTime(MOBILshift(R), 0, 100)
                End If


                If ictmobilsetar <> 0 Then
                    EffectSE = StdErrorAdditiveShift(CSng(SE), CSng(rVal), CSng(MobilAdd), CSng(MOBILshift(R)),
                    CSng(ictmobilsetar), CInt(FirstYear + ictmobilseyrtar), FirstYear + IY - CShort(1), FirstYear)
                Else
                    EffectSE = 0
                End If

                'mti note: JRS original model did not have upper and lower bounds here; Check if that makes a difference
                ICTMOBIL(R%) = rVal + MobilAdd + EffectSE


                'jrs 2009/05/06
                'Tec Factor: S-Curve shaped
                '---------------------------
                'jrs 2010/12/06 *** begin: Using Annual Growth Rate ***
                '******************************************************
                'jrs 2009/05/12
                'jrs 2010/12/06 incCS = ICTMOBIL(R) - SMobil(R, 1)

                'Scaling CS change depending Data/Function rate
                If rVal > SMobil(R, 2) Then
                    incCS = (ICTMOBIL(R) - SMobil(R, 1)) * SMobil(R, 2) / Math.Abs(rVal)
                Else
                    incCS = ICTMOBIL(R) - SMobil(R, 1)
                End If

                '---- Scaling CS change depending Data/Function rate ----

                If SMobil(R, 2) < ictmobiltecinflection Then
                    incTec = (ictmobiltechighrt(R%) - ictmobilteclowrt) * SMobil(R, 2) / ictmobiltecinflection + ictmobilteclowrt
                Else
                    'Added 10 to saturation to let it go over before coming back
                    incTec = (ictmobilteclowrt - ictmobiltechighrt(R%)) * (SMobil(R, 2) - ictmobiltecinflection) / (ictmobilsaturation + 10 - ictmobiltecinflection) + ictmobiltechighrt(R%)
                End If
                SMobil(R, 1) = ICTMOBIL(R)

                'jrs 2010/12/06
                If Not (ICTMobilGR(R).HasValue) Then
                    incTot = incTec + incCS
                Else
                    'transition between extrapolative to cross-sectional plus tech shift

                    'old formulation (first difference convergence, faster)
                    '                    HistWg = 0.5
                    '                    'incTot = HistWg * ICTMobilGR(R) * SMobil(R, 2) + (1 - HistWg) * (incTec + incCS)
                    '                    'Instead of using the compoundig Growth Rate, use just the flat differece between the 2 last years
                    '                    'F(y-1) - F(y-2) = F(y-1) * (1 - 1/(1+GR))
                    '                    'incTot = HistWg * SMobil(R, 2) * (1 - 1 / (1 + ICTMobilGR(R))) + (1 - HistWg) * (incTec + incCS)
                    '                    incTot = ConvergeOverTime(SMobil(R, 2) * (1 - 1 / (1 + ICTMobilGR(R))), incTec + incCS, 10)


                    'new formulation (growth rate convergence, slower)
                    gratem = (incTec + incCS) / AMAX(0.01, SMobil(R, 2)) 'growth rate from model


                    gratec = ConvergeOverTime(CSng(ICTMobilGR(R)), gratem, 25) 'growth rate from previous year converge to that from model

                    incTot = gratec * SMobil(R, 2)

                    '                    'absolute growth rate calc
                    '                    'incTot = ICTMobilGR(R)
                    '                    'revert to growth rate
                    '                    incTot = ICTMobilGR(R) * SMobil(R, 2)
                    '                    'mti July 18, 2011 - Rebase 2010 ICT - end
                    '
                End If

                'with higher saturation value we need a larger number in the denominator
                ICTMOBIL(R) = SMobil(R, 2) + Amin(incTot, (ictmobilsaturation + 10 - SMobil(R, 2)) / 5)


                If ICTMOBIL(R) > ictmobilsaturation And IY > 5 Then
                    If IY > 15 Then
                        ICTMOBIL(R) = ConvergeOverTime(ICTMOBIL(R), ictmobilsaturation, 96)
                    ElseIf IY > 10 Then
                        ICTMOBIL(R) = ConvergeOverTime(ICTMOBIL(R), ictmobilsaturation, 150)
                    Else
                        ICTMOBIL(R) = ConvergeOverTime(ICTMOBIL(R), ictmobilsaturation, 200)
                    End If
                End If


                'for first 5 years we use same absolute growth now
                If SMobil(R, 2) <> 0 Then
                    'this has to be bound for low initial value countries like Korea North
                    ICTMobilGR(R) = CSng(Amin(1.5, ICTMOBIL(R) / SMobil(R, 2) - 1))
                End If





                'jrs 2010/12/06 *** end: Using Annual Growth Rate ***
                '******************************************************

                SMobil(R, 2) = ICTMOBIL(R)


                'mti 2014/08/20 - start
                '            'mti 2014/05/28 - start
                '            'See if curve has shifted up by extra money in the previous year
                '            ICTMOBILShiftPrevYear = AMAX(0, (SICTMobil(R) - SMobil(R, 3))) 'percent point
                '            'Save pre-finance value:
                '            SMobil(R, 3) = ICTMOBIL(R)
                '            'might be an issue in a multiplier scenario; as SICTBROAD will have multiplier effect; and SBroad(R, 3)  is saved pre-multiplier
                '
                ''      If RegName(R%) = "Saudi Arabia" And IY >= 7 Then Stop
                '
                '            'In case curve has shifted up (by extra money in the previous year, demand signal needs to be sent so that it could continue to stay there
                '            ICTMOBIL(R) = ICTMOBIL(R%) + ICTMOBILShiftPrevYear
                '            'mti 2014/05/28 - end
                'mti 2014/08/20 - end


                'mti June 04, 2012 -start
                'already built infrastructure should not go down unless there is a hard multiplier infraroadm(R%, Urban)
                ICTMOBIL(R) = AMAX(ICTMOBIL(R%), SICTMobil(R%))
                'note to MTI: unlike other infrastructure there is no hard parameter to check against here
                'mti June 04, 2012 - end


                'spending need
                'MobilConAdded = AMAX(0, (CDbl(ICTMOBIL(R)) * CDbl(POP(R%)) - SICTMobil(R) * CDbl(sPOPTOTAL(R%))) / 100) 'millions of subscribers

                CalcInfraUnitCost(R%, 10, 5, ictmobilcostupper, ictmobilcostlower, sGDPPCP(R%), ICTMOBILCOST(R%), False)
                ICTMOBILCOST(R%) = ICTMOBILCOST(R%) * ictmobilcostm(R%)

                'INFRAINVESTNEW(R%, Mobile) = MobilConAdded * ICTMOBILCOST(R%) 'billion dollars
                'INFRAINVESTMAINT(R%, Mobile) = (SICTMobil(R%) * CDbl(sPOPTOTAL(R%)) / 100) * DeppcntMobile * ICTMOBILCOST(R%)  'percent con * million pop/100 = million com * 1000 $ =  billion dollars

                Dim ICTMobilDemThousands = ICTMOBIL(R%) * CDbl(POP(R%)) * 10 'Demanded Connections (stock) in Thousands This Year
                Dim sICTMobilThousands = SICTMobil(R%) * CDbl(sPOPTotal(R%)) * 10 'Connections (Stock) in Thousands Last Year
                CalcInfraInvest(CDbl(ICTMobilDemThousands), CDbl(sICTMobilThousands),
                            DeppcntMobile, ICTMOBILCOST(R%), INFRAINVESTNEW(R%, Mobile), INFRAINVESTMAINT(R%, Mobile))



                '----------base forecast needed for base spending in  target scenario case----------
                If ictmobilsetar > 0 Then            'for postive setar only
                    sICTMobilBF(R%) = ICTMobilBF(R%)
                    ICTMobilBF(R%) = rVal + MobilAdd
                    If rVal > sMobilBF(R, 2) Then
                        incCSBF = (ICTMobilBF(R) - sMobilBF(R, 1)) * sMobilBF(R, 2) / Math.Abs(rVal)
                    Else
                        incCSBF = ICTMobilBF(R) - sMobilBF(R, 1)
                    End If
                    If sMobilBF(R, 2) < ictmobiltecinflection Then
                        incTecBF = (ictmobiltechighrt(R%) - ictmobilteclowrt) * sMobilBF(R, 2) / ictmobiltecinflection + ictmobilteclowrt
                    Else
                        'Added 10 to saturation to let it go over before coming back
                        incTecBF = (ictmobilteclowrt - ictmobiltechighrt(R%)) * (sMobilBF(R, 2) - ictmobiltecinflection) / (ictmobilsaturation + 10 - ictmobiltecinflection) + ictmobiltechighrt(R%)
                    End If
                    sMobilBF(R, 1) = ICTMobilBF(R%)

                    If Not (ICTMobilGRBF(R).HasValue) Then
                        incTotBF = incTecBF + incCSBF
                    Else
                        'new formulation (growth rate convergence, slower)
                        gratemBF = (incTecBF + incCSBF) / AMAX(0.01, sMobilBF(R, 2)) 'growth rate from model
                        gratecBF = ConvergeOverTime(CSng(ICTMobilGRBF(R)), gratemBF, 25) 'growth rate from previous year converge to that from model
                        incTotBF = gratecBF * sMobilBF(R, 2)
                    End If

                    ICTMobilBF(R) = sMobilBF(R, 2) + Amin(incTotBF, (ictmobilsaturation + 10 - sMobilBF(R, 2)) / 5)

                    If ICTMobilBF(R) > ictmobilsaturation And IY > 5 Then
                        If IY > 15 Then
                            ICTMobilBF(R) = ConvergeOverTime(ICTMobilBF(R), ictmobilsaturation, 96)
                        ElseIf IY > 10 Then
                            ICTMobilBF(R) = ConvergeOverTime(ICTMobilBF(R), ictmobilsaturation, 150)
                        Else
                            ICTMobilBF(R) = ConvergeOverTime(ICTMobilBF(R), ictmobilsaturation, 200)
                        End If
                    End If

                    If sMobilBF(R, 2) <> 0 Then
                        ICTMobilGRBF(R) = CSng(Amin(1.5, ICTMobilBF(R) / sMobilBF(R, 2) - 1))
                    End If

                    sMobilBF(R, 2) = ICTMobilBF(R) 'declare smobil etc; gr etc

                    ICTMobilDemThousands = ICTMobilBF(R%) * CDbl(POP(R%)) * 10 'Demanded Connections (stock) in Thousands This Year

                    'mti June 14, 2012 - start
                    'sICTMobilThousands = sICTMobilBF(R%) * CDbl(sPOPTotal(R%)) * 10 'Connections (Stock) in Thousands Last Year
                    sICTMobilThousands = SICTMobil(R%) * CDbl(sPOPTotal(R%)) * 10 'Connections (Stock) in Thousands Last Year
                    'mti June 14, 2012 - end

                    CalcInfraInvest(CDbl(ICTMobilDemThousands), CDbl(sICTMobilThousands),
                                DeppcntMobile, ICTMOBILCOST(R%), INFRAINVESTNEWBF(R%, Mobile), INFRAINVESTMAINTBF(R%, Mobile))


                Else

                    INFRAINVESTNEWBF(R%, Mobile) = INFRAINVESTNEW(R%, Mobile)
                    INFRAINVESTMAINTBF(R%, Mobile) = INFRAINVESTMAINT(R%, Mobile)
                End If
                '----End base forecast------------
            Next R%
        End Sub

        Private Sub ICTFixedBBForecast(ByRef SGOVREGQUAL() As Single, ByRef sGDPPCP() As Single, ByRef sPOPTotal() As Single,
                                       ByRef SBroad(,) As Single, ByRef ICTBroadGR() As Single?, ByRef ICTBroadBF() As Single,
                                       ByRef sICTBroadBF() As Single, ByRef sBroadBF(,) As Single, ByRef ICTBroadGRBF() As Single?,
                                       ByRef INFRAINVESTNEWBF(,) As Single, ByRef INFRAINVESTMAINTBF(,) As Single, DeppcntBroad As Double,
                                       ByRef SICTBroad() As Single, ByRef SSINFRATELE() As Double, ByRef SINFRATELE() As Double,
                                       ByRef iICTBroad() As Single, ByRef ictbroadtrgtyr() As Single, ByRef ictbroadtrgtval() As Single, context As IForecastContext)
            '----------------------------------------------------------------------------------------------------------------------------------
            'mti June 14, 2012 - start

            'mti 2013/12/20 start
            If context.HistoricMode = True AndAlso context.FirstYearHistory < 1990 Then
                'no Fixed BB for pre-1990 hist base
                For R% = 1 To NR%
                    ICTBROAD(R) = 0
                    ICTBroadGR(R) = 0

                    ICTBROADCOST(R%) = 0
                    INFRAINVESTNEW(R%, Broadband) = 0
                    INFRAINVESTMAINT(R%, Broadband) = 0

                    ICTBroadBF(R%) = 0
                    ICTBroadGRBF(R%) = 0
                    INFRAINVESTNEWBF(R%, Broadband) = 0
                    INFRAINVESTMAINTBF(R%, Broadband) = 0
                Next
                Exit Sub
            End If
            'mti 2013/12/20 end
            Dim rVal As Single = 0
            Dim SE As Single = 0
            Dim BroadAdd As Single = 0
            Dim EffectSE As Single = 0
            Dim incCS As Single = 0
            Dim UrbImp As Single = 0
            Dim ictbroadcap As Single = 0
            Dim ltfac As Single = 0
            Dim incTec As Single = 0
            Dim incTot As Single = 0
            Dim gratem As Single = 0
            Dim gratec As Single = 0
            Dim incTotBF As Single = 0
            Dim gratemBF As Single = 0
            Dim gratecBF As Single = 0
            Dim ictbroadTrgt As Single = 0
            Dim incTecBF As Single = 0
            For R% = 1 To NR%
                Dim RegionName = context.GetDimension(1).Buckets.Item(CShort(R))
                sErrorSub = "ICTFixedBBForecast" & "1" & "-" & RegionName

                'sErrorSub = "ICTFixedBBForecast" & CStr(IFUN) & "-" & RegName(R%)

                'All years but first: Access to Fixed Broadband
                '------------------------------------------------------
                'CS (Cross-sectional formulation)
                Call AnalFuncSE("ICT Broadband Access % of Population Rev 2010",
            New Single() {GDPPCP(R), SGOVREGQUAL(R)}, rVal, SE,
            New String() {"GDP2005PCPPP(MOSTRECENT)/ 1000 (Log)", "GovernanceRegQual(MOSTRECENT)+ 2.5 (Linear)"}, context) 'jrs 2014/10/09 added DepVar Names to check order


                'Tec Factor: S-Curve shaped
                '---------------------------
                'Drawn from World Data from 1980 to 2007       'Saturation of 120

                'Shift convergence
                BroadAdd = ConvergeOverTime(BROADshift(R), 0, 100)

                'dkb 2013/09/16 added dimension 1 to ictbroadsetar and ictbroadseyrtar
                If ictbroadsetar(R%) <> 0 Then
                    EffectSE = StdErrorAdditiveShift(CSng(SE), CSng(rVal), CSng(BroadAdd), CSng(BROADshift(R)),
                CSng(ictbroadsetar(R%)), CInt(FirstYear + ictbroadseyrtar(R%)), FirstYear + IY - 1, FirstYear)
                Else
                    EffectSE = 0
                End If

                'mti note: JRS original model did not have upper and lower bounds here; Check if that makes a difference
                ICTBROAD(R) = rVal + BroadAdd + EffectSE

                incCS = ICTBROAD(R) - SBroad(R, 1)


                UrbImp = ictbroadurimpmin + AMAX(0, (ictbroadurimpmax - ictbroadurimpmin)) * SPOPURBAN(R%) / sPOPTotal(R%)
                incCS = incCS * UrbImp

                'mti June 14, 2012 - BBCap - start
                ictbroadcap = 50
                ictbroadtecinflection = ictbroadcap / 2
                'mti June 14, 2012 - BBCap -end

                ltfac = 0.5
                If SBroad(R, 2) < ictbroadtecinflection Then
                    incTec = (ictbroadtechighrt(R%) * ltfac - ictbroadteclowrt * ltfac) * SBroad(R, 2) / ictbroadtecinflection + ictbroadteclowrt * ltfac
                Else
                    'incTec = (ictbroadteclowrt * ltfac - ictbroadtechighrt(R%) * ltfac) * (SBroad(R, 2) - ictbroadtecinflection) / (100 - ictbroadtecinflection) + ictbroadtechighrt(R%) * ltfac
                    incTec = (ictbroadteclowrt * ltfac - ictbroadtechighrt(R%) * ltfac) * (SBroad(R, 2) - ictbroadtecinflection) / (ictbroadcap - ictbroadtecinflection) + ictbroadtechighrt(R%) * ltfac
                End If



                SBroad(R, 1) = ICTBROAD(R)

                'jrs 2010/12/06 *** begin: Using Annual Growth Rate ***
                '******************************************************
                'jrs 2010/12/06
                If Not (ICTBroadGR(R).HasValue) Then
                    incTot = incTec + incCS
                Else


                    '                    HistWg = 0
                    '                    'incTot = HistWg * ICTBroadGR(R) * SBroad(R, 2) + (1 - HistWg) * (incTec + incCS)
                    '                    'Instead of using the compoundig Growth Rate, use just the flat differece between the 2 last years
                    '                    'F(y-1) - F(y-2) = F(y-1) * (1 - 1/(1+GR))
                    '                    'incTot = HistWg * SBroad(R, 2) * (1 - 1 / (1 + ICTBroadGR(R))) + (1 - HistWg) * (incTec + incCS)
                    '                    incTot = ConvergeOverTime(SBroad(R, 2) * (1 - 1 / (1 + ICTBroadGR(R))), incTec + incCS, 10)

                    'new formulation (growth rate convergence, slower)
                    '                    gratem = (incTec + incCS) / AMAX(0.01, SBroad(R, 2)) 'growth rate from model
                    '                    gratec = ConvergeOverTime(ICTBroadGR(R), gratem, 25) 'growth rate from previous year converge to that from model
                    '                    incTot = gratec * SBroad(R, 2)
                    'mti January 17, 2012 -end

                    'percent point change
                    gratem = incTec + incCS '/ AMAX(0.01, SBroad(R, 2)) 'growth rate from model
                    gratec = ConvergeOverTime(CSng(ICTBroadGR(R)), gratem, 15) 'growth rate from previous year converge to that from model
                    incTot = gratec '* SBroad(R, 2)


                End If

                'Add Increase/Decrease to final value, this is the tec factor
                'Total increment can't be more than 25% of the remaining gap to reach target of 120
                ICTBROAD(R) = SBroad(R, 2) + Amin(incTot, (ictbroadcap - SBroad(R, 2)) / 4)

                'Apply Absolute Cap; prevent decline of base forecast
                ICTBROAD(R) = Amin(AMAX(ICTBROAD(R), SBroad(R, 2)), ictbroadcap)




                'Save (base forecast) for next year
                SBroad(R, 2) = ICTBROAD(R)


                'Add telephone divestment impact
                ICTBROAD(R%) = CSng(Amin(ictbroadcap, AMAX(0.0001, ICTBROAD(R%) + ictbroadfromtelem * AMAX(0, SSINFRATELE(R%) - SINFRATELE(R%)))))





                '            'mti 2014/05/28 - start
                '            'See if curve has shifted up by extra money in the previous year
                '            ICTBROADShiftPrevYear = AMAX(0, (SICTBroad(R) - SBroad(R, 3))) 'percent point
                '
                '
                '            'mti 2014/05/28 - start
                '            'Save pre-finance value:
                '            SBroad(R, 3) = ICTBROAD(R)
                '            'might be an issue in a multiplier scenario; as SICTBROAD will have multiplier effect; and SBroad(R, 3)  is saved pre-multiplier
                '            'mti 2014/05/28 - end
                '
                ''      If RegName(R%) = "Saudi Arabia" And IY >= 7 Then Stop
                '
                '            'In case curve has shifted up (by extra money in the previous year, demand signal needs to be sent so that it could continue to stay there
                '            ICTBROAD(R%) = Amin(ictbroadcap, ICTBROAD(R%) + ICTBROADShiftPrevYear)
                '            'mti 2014/05/28 - end


                'parameter goal seek - start
                If ictbroadtrgtyr(R%) > 0 Then

                    ictbroadTrgt = Amin(ictbroadcap, iICTBroad(R%) + Amin(ictbroadtrgtyr(R%), IY - 1) * (ictbroadtrgtval(R%) - iICTBroad(R%)) / ictbroadtrgtyr(R%))

                    ICTBROAD(R%) = AMAX(ICTBROAD(R%), ictbroadTrgt)
                End If



                'Multiplier applied on base forecast (CS + tecshift + tele divest)
                ICTBROAD(R%) = ICTBROAD(R%) * ictbroadm(R%)



                'mti 2014/05/28 - start
                'See if curve has shifted up by extra money in the previous year
                Dim ICTBROADShiftPrevYear = AMAX(0, (SICTBroad(R) - SBroad(R, 3))) 'percent point
                'Save pre-finance value:
                SBroad(R, 3) = ICTBROAD(R)
                'might be an issue in a multiplier scenario; as SICTBROAD will have multiplier effect; and SBroad(R, 3)  is saved pre-multiplier

                '      If RegName(R%) = "Saudi Arabia" And IY >= 7 Then Stop

                'In case curve has shifted up (by extra money in the previous year, demand signal needs to be sent so that it could continue to stay there
                ICTBROAD(R%) = Amin(ictbroadcap, ICTBROAD(R%) + ICTBROADShiftPrevYear)
                'mti 2014/05/28 - end

                'already built infrastructure should not go down unless there is a hard multiplier infraroadm(R%, Urban)
                If ictbroadm(R%) >= 1 Then
                    ICTBROAD(R) = AMAX(ICTBROAD(R%), SICTBroad(R))
                End If
                'note to MTI: unlike other infrastructure there is no hard parameter to check against here

                '            'mti 2014/05/28 - start
                '            'Save pre-finance value:
                '            SBroad(R, 3) = ICTBROAD(R)
                '            'mti 2014/05/28 - end

                'Finance
                CalcInfraUnitCost(R%, 10, 5, ictbroadcostupper, ictbroadcostlower, sGDPPCP(R%), ICTBROADCOST(R%)) ', False

                'mti April 2011 -start
                ICTBROADCOST(R%) = ICTBROADCOST(R%) * ictbroadcostm(R%)
                'mti April 2011 -end

                Dim ICTBroadDemThousands = ICTBROAD(R%) * CDbl(POP(R%)) * 10 'Demanded Connections (stock) in Thousands This Year
                Dim sICTBroadThousands = SICTBroad(R%) * CDbl(sPOPTotal(R%)) * 10 'Connections (Stock) in Thousands Last Year
                CalcInfraInvest(CDbl(ICTBroadDemThousands), CDbl(sICTBroadThousands),
                            DeppcntBroad, ICTBROADCOST(R%), INFRAINVESTNEW(R%, Broadband), INFRAINVESTMAINT(R%, Broadband))


                '----------base forecast needed for base spending in  target scenario case----------
                If ictbroadtrgtyr(R%) > 0 OrElse ictbroadsetar(R%) > 0 Then             'for postive setar only

                    '--------- base forecast physical ----------
                    sICTBroadBF(R) = ICTBroadBF(R)
                    ICTBroadBF(R) = rVal + BroadAdd
                    Dim incCSBF = ICTBroadBF(R) - sBroadBF(R, 1)
                    incCSBF = incCSBF * UrbImp

                    ltfac = 0.5
                    If sBroadBF(R, 2) < ictbroadtecinflection Then
                        incTecBF = (ictbroadtechighrt(R%) * ltfac - ictbroadteclowrt * ltfac) * sBroadBF(R, 2) / ictbroadtecinflection + ictbroadteclowrt * ltfac
                    Else
                        'mti June 14, 2012 - BBCap - start - ictbroadcap
                        'incTecBF = (ictbroadteclowrt * ltfac - ictbroadtechighrt(R%) * ltfac) * (SBroadBF(R, 2) - ictbroadtecinflection) / (100 - ictbroadtecinflection) + ictbroadtechighrt(R%) * ltfac
                        incTecBF = (ictbroadteclowrt * ltfac - ictbroadtechighrt(R%) * ltfac) * (sBroadBF(R, 2) - ictbroadtecinflection) / (ictbroadcap - ictbroadtecinflection) + ictbroadtechighrt(R%) * ltfac
                        'mti June 14, 2012 - BBCap - end
                    End If

                    sBroadBF(R, 1) = ICTBroadBF(R)

                    If Not (ICTBroadGRBF(R).HasValue) Then
                        incTotBF = incTecBF + incCSBF
                    Else
                        gratemBF = incTecBF + incCSBF '/ AMAX(0.01, SBroad(R, 2)) 'growth rate from model
                        gratecBF = ConvergeOverTime(CSng(ICTBroadGRBF(R)), gratemBF, 15) 'growth rate from previous year converge to that from model
                        incTotBF = gratecBF '* SBroad(R, 2)
                    End If


                    'mti June 14, 2012 - BBCap - start -
                    ICTBroadBF(R) = sBroadBF(R, 2) + Amin(incTotBF, (ictbroadcap - sBroadBF(R, 2)) / 4)
                    ICTBroadBF(R) = Amin(AMAX(ICTBroadBF(R), sBroadBF(R, 2)), ictbroadcap)
                    'mti June 14, 2012 - BBCap - end
                    sBroadBF(R, 2) = ICTBroadBF(R)

                    'mti June 14, 2012 - BBCap - start - ictbroadcap
                    'ICTBroadBF(R%) = Amin(100, AMAX(0.0001, ICTBroadBF(R%) + ictbroadfromtelem * AMAX(0, SSINFRATELE(R%) - SINFRATELE(R%))))
                    ICTBroadBF(R%) = CSng(Amin(ictbroadcap, AMAX(0.0001, ICTBroadBF(R%) + ictbroadfromtelem * AMAX(0, SSINFRATELE(R%) - SINFRATELE(R%)))))
                    'mti June 14, 2012 - BBCap - end
                    '---------------------------

                    '--------- base forecast physical ------------
                    ICTBroadDemThousands = ICTBroadBF(R%) * CDbl(POP(R%)) * 10 'Demanded Connections (stock) in Thousands This Year

                    'mti June 14, 2012 - start
                    'sICTBroadThousands = sICTBroadBF(R%) * CDbl(sPOPTotal(R%)) * 10 'Connections (Stock) in Thousands Last Year
                    sICTBroadThousands = SICTBroad(R%) * CDbl(sPOPTotal(R%)) * 10 'Connections (Stock) in Thousands Last Year
                    'mti June 14, 2012 - end

                    CalcInfraInvest(CDbl(ICTBroadDemThousands), CDbl(sICTBroadThousands),
                                DeppcntBroad, ICTBROADCOST(R%), INFRAINVESTNEWBF(R%, Broadband), INFRAINVESTMAINTBF(R%, Broadband))

                Else

                    INFRAINVESTNEWBF(R%, Broadband) = INFRAINVESTNEW(R%, Broadband)
                    INFRAINVESTMAINTBF(R%, Broadband) = INFRAINVESTMAINT(R%, Broadband)

                End If
                '----End base forecast------------
            Next R%

        End Sub

        'jrs 2023/07/25 added byRef to Real_AccessRate, InvestCapital_WS, InvestMaint_WS
        Private Sub CalcInfraInvestWatSanNew(R%, UnitCost_Lower(,,) As Single, UnitCost_Higher(,,) As Single, Reqd_AccessRate(,,) As Single, Base_AccessRate(,,) As Single, Prev_AccessRate(,,) As Single,
                                            ByRef Real_AccessRate(,,) As Single, lifetime_lower_cost() As Single, lifetime_higher_cost() As Single, shrlowrcost(,,) As Single,
                                            ByRef InvestCapital_WS(,,) As Single, ByRef InvestMaint_WS(,,) As Single, Optional BudgetImpactFactor_Safe As Single = 1,
                                                Optional BudgetImpactFactor_OtherImproved As Single = 1, Optional ApplyBudgetImpact As Boolean = False)
            '------------------------------------------------------------------------------------------------------------------------------------
            'mti 2022/10/20 Take procedure - including definition

            'mti 2022/10/04 change procedure
            'mti 2022/09/06 new procedure
            'mti 2022/09/15 deleted comment

            Dim Loc%
            Dim PopTot, sPopTot As Single

            For Loc% = 1 To 2

                If Loc% = Rural Then
                    PopTot = AMAX(0.0001, sPOPTotal(R%) - SPOPURBAN(R%))
                    sPopTot = AMAX(0.0001, sPopTotal1(R%) - sPopUrban1(R%))
                Else
                    PopTot = AMAX(0.0001, SPOPURBAN(R%))
                    sPopTot = AMAX(0.0001, sPopUrban1(R%))
                End If


                '----------  Water -----------
                For Srvc% = 1 To 5

                    'mti 2022/10/20 Start
                    Dim BudgetImpactFactor As Single
                    If Srvc% < 5 Then
                        BudgetImpactFactor = BudgetImpactFactor_Safe
                    Else
                        BudgetImpactFactor = BudgetImpactFactor_OtherImproved
                    End If
                    'mti 2022/10/20 End

                    Dim WatSafeDemThousands, ReqdAddition, RealAddition As Single
                    Dim WatSafeRealThousands, pWatSafeThousands As Double
                    If UnitCost_Lower(R%, Srvc%, Loc%) > 0 Or UnitCost_Higher(R%, Srvc%, Loc%) > 0 Then



                        WatSafeDemThousands = Reqd_AccessRate(R%, Srvc%, Loc%) * PopTot / 100  'Demanded Connections (stock) this Year
                        sWatSafeThousands = Prev_AccessRate(R%, Srvc%, Loc%) * sPopTot / 100  'Connections (Stock) Last Year
                        ReqdAddition = AMAX(0, (WatSafeDemThousands - sWatSafeThousands)) 'Addition ----------
                        'note - might want to save it in my earlier computation




                        'taking whole now - 'same deficit in public and private
                        If ApplyBudgetImpact Then
                            'mti 2022/09/15 start
                            RealAddition = ReqdAddition * BudgetImpactFactor
                            'mti 2022/09/15 end
                            'mti 2022/09/27 start
                            'WatSafeRealThousands = sWatSafeThousands + RealAddition
                            'If current year's desired access rate is lower than that of previous year, for example, for a decline in drivers, like in Venezuela,
                            'fund shortage should keep desired access rate at that lower rate, as opposed to taking it to last year's level
                            WatSafeRealThousands = Amin(WatSafeDemThousands, sWatSafeThousands + RealAddition)
                            'mti 2022/09/27 end

                            'mti 2022/10/04 start
                            If Prev_AccessRate(R%, Srvc%, Loc%) > 95 And
                                                Abs(Reqd_AccessRate(R%, Srvc%, Loc%) - Base_AccessRate(R%, Srvc%, Loc%)) < 0.001 Then
                                'And BudgetImpactFactor > 0.85 Then
                                'high access levels; no multiplier; (and, budget shortage not very large)
                                pWatSafeThousands = Prev_AccessRate(R%, Srvc%, Loc%) * PopTot / 100  'previus year's  rate with current year's population
                                WatSafeRealThousands = AMAX(pWatSafeThousands, WatSafeRealThousands)
                            End If
                            'mti 2022/10/04 End

                            'Final value
                            Real_AccessRate(R%, Srvc%, Loc%) = CSng(AMAX(0, Amin(100, WatSafeRealThousands / PopTot * 100)))
                            ReqdAddition = RealAddition
                        Else

                        End If

                        '--- Share using lower/higher cost options ---
                        'mti 2022/09/21 start
                        '                                 If UnitCost_Lower(R%, Srvc%, Loc%) > 0 And UnitCost_Higher(R%, Srvc%, Loc%) > 0 Then
                        '                                     lc_shr = shrlowrcost(R%, Srvc%, Loc%) '0.5
                        '                                 Else
                        '                                     If UnitCost_Lower(R%, Srvc%, Loc%) > 0 Then
                        '                                         lc_shr = 1
                        '                                     Else
                        '                                         lc_shr = 0
                        '                                     End If
                        '                                 End If

                        Dim lc_shr = shrlowrcost(R%, Srvc%, Loc%)
                        If UnitCost_Higher(R%, Srvc%, Loc%) = 0 Then lc_shr = 1
                        If UnitCost_Lower(R%, Srvc%, Loc%) = 0 Then lc_shr = 0
                        'mti 2022/09/21 End


                        '--- Capital cost ---
                        InvestCapital_WS(R%, Srvc%, Loc%) = ReqdAddition * lc_shr * UnitCost_Lower(R%, Srvc%, Loc%) * GDPPC(R%) / 100
                        InvestCapital_WS(R%, Srvc%, Loc%) = InvestCapital_WS(R%, Srvc%, Loc%) + ReqdAddition * (1 - lc_shr) * UnitCost_Higher(R%, Srvc%, Loc%) * GDPPC(R%) / 100


                        'Maintenance Cut
                        Dim WatSafeRealThousands_lc As Double = 0
                        Dim WatSafeRealThousands_hc As Double = 0
                        Dim MaintCut As Double
                        If UnitCost_Lower(R%, Srvc%, Loc%) > 0 Then

                            'mti 2022/10/20 start
                            Dim Deppcnt_WS_New = Exp(Log(2) / lifetime_lower_cost(Srvc%)) - 1

                            If ApplyBudgetImpact Then

                                'mti 2022/09/15 start
                                MaintCut = sWatSafeThousands * lc_shr * Deppcnt_WS_New * (1 - BudgetImpactFactor)
                                WatSafeRealThousands_lc = WatSafeRealThousands * lc_shr - MaintCut  'mti - check for negatives?
                                InvestMaint_WS(R%, Srvc%, Loc%) = CSng(sWatSafeThousands * lc_shr * Deppcnt_WS_New * BudgetImpactFactor * UnitCost_Lower(R%, Srvc%, Loc%) * GDPPC(R%) / 100) 'in billion $
                                'mti 2022/09/15 end

                            Else
                                InvestMaint_WS(R%, Srvc%, Loc%) = CSng(sWatSafeThousands * lc_shr * Deppcnt_WS_New * UnitCost_Lower(R%, Srvc%, Loc%) * GDPPC(R%) / 100) 'in billion $
                            End If
                        End If

                        If UnitCost_Higher(R%, Srvc%, Loc%) > 0 Then
                            Dim Deppcnt_WS_New = Exp(Log(2) / lifetime_higher_cost(Srvc%)) - 1

                            If ApplyBudgetImpact Then
                                'mti 2022/09/15 start
                                MaintCut = sWatSafeThousands * (1 - lc_shr) * Deppcnt_WS_New * (1 - BudgetImpactFactor)
                                WatSafeRealThousands_hc = WatSafeRealThousands * (1 - lc_shr) - MaintCut
                                InvestMaint_WS(R%, Srvc%, Loc%) = CSng(InvestMaint_WS(R%, Srvc%, Loc%) +
                                                            sWatSafeThousands * (1 - lc_shr) * Deppcnt_WS_New * BudgetImpactFactor * UnitCost_Higher(R%, Srvc%, Loc%) * GDPPC(R%) / 100) 'in billion $
                                'mti 2022/09/15 end
                            Else
                                InvestMaint_WS(R%, Srvc%, Loc%) = CSng(InvestMaint_WS(R%, Srvc%, Loc%) +
                                                            sWatSafeThousands * (1 - lc_shr) * Deppcnt_WS_New * UnitCost_Higher(R%, Srvc%, Loc%) * GDPPC(R%) / 100) 'in billion $
                            End If
                            'mti 2022/10/20 End
                        End If

                        'Recompute physical infrastructure with maintenance cut  (now do sanitation)
                        If ApplyBudgetImpact Then
                            If UnitCost_Lower(R%, Srvc%, Loc%) > 0 Or UnitCost_Higher(R%, Srvc%, Loc%) > 0 Then
                                WatSafeRealThousands = WatSafeRealThousands_lc + WatSafeRealThousands_hc

                                'mti 2022/10/04 start
                                If Prev_AccessRate(R%, Srvc%, Loc%) > 95 And
                                                    Abs(Reqd_AccessRate(R%, Srvc%, Loc%) - Base_AccessRate(R%, Srvc%, Loc%)) < 0.001 Then
                                    'And BudgetImpactFactor > 0.7 Then
                                    'high access levels; no multiplier; and, budget shortage not very large
                                    pWatSafeThousands = Prev_AccessRate(R%, Srvc%, Loc%) * PopTot / 100
                                    WatSafeRealThousands = AMAX(pWatSafeThousands, WatSafeRealThousands)
                                End If
                                'mti 2022/10/04 End

                                Real_AccessRate(R%, Srvc%, Loc%) = CSng(AMAX(0, Amin(100, WatSafeRealThousands / PopTot * 100)))
                            End If
                        End If

                    End If


                Next Srvc%

            Next Loc%

            'mti 2022/09/27 start
            For Loc% = 1 To 2
                Dim SumReduction As Single = 0
                For Srvc% = 3 To 5
                    SumReduction = SumReduction + AMAX(0, (Reqd_AccessRate(R%, Srvc%, Loc%) - Real_AccessRate(R%, Srvc%, Loc%)) * PopTot / 100)
                Next

                'give everything to Cat2, unimproved (limited has a cost and will also cut back)
                Dim UnImp_HC = Real_AccessRate(R%, Unimproved, Loc%) * PopTot / 100
                If PopTot > 0 Then
                    Real_AccessRate(R%, Unimproved, Loc%) = AMAX(0, Amin(100, (UnImp_HC + SumReduction) / PopTot * 100))
                End If


            Next
            'mti 2022/09/27 end

        End Sub

        '============================= Infrastr.BAS  =======================================================================================
        Private Sub InfraBudgetAllocationWithShortage(R%, GDSInfraAllocation As Double, InfraBudImpactByType(,) As Single, InfraBudImpactOverall As Single?)
            '----------------------------------------------------------------------------------------------------------------
            'mti 2022/10/20 New Proc

            Dim InfraSetAsideNew(InfraTot), InfraSetAsideMaint(InfraTot), InfraRemainingNew(InfraTot), InfraRemainingMaint(InfraTot) As Double

            'Water and sanitation have their own budget protection parameters; if set they override the budget protection ;parameters by infrastructure type
            If waterbudgetprotec(R%) <> 0 Then
                infrabudgetprotec(R%, SafeWaterHH) = waterbudgetprotec(R%)
                infrabudgetprotec(R%, SafeWaterImproved) = waterbudgetprotec(R%)
            End If

            If sanitbudgetprotec(R%) <> 0 Then
                infrabudgetprotec(R%, SanitationHH) = sanitbudgetprotec(R%)
                infrabudgetprotec(R%, SanitationImproved) = sanitbudgetprotec(R%)
            End If

            'reallocate to each task as base demand for the task
            'plus distribution of "total surplus to base demand" (if any) proportional to additional demand for each target (aka target base differential)

            'sum of additinal demand for target (i.e., target base differential) across all tasks
            Dim Sum_TargetBaseDiff As Double = 0
            For j = 1 To NInfraType%
                'target (public)  - base(all) * public share
                Sum_TargetBaseDiff = Sum_TargetBaseDiff + AMAX(0, INFRAINVESTNEWPUB(R%, j) - INFRAINVESTNEWBF(R%, j) * infrainvnewpubshrm(R%, j))
                Sum_TargetBaseDiff = Sum_TargetBaseDiff + AMAX(0, INFRAINVESTMAINTPUB(R%, j) - INFRAINVESTMAINTBF(R%, j) * infrainvmaintpubshrm(R%, j))

            Next j

            'reallocation as base demand plus proportional distribution of surplus
            If Sum_TargetBaseDiff > 0.00000001 Then 'if less do nothing? so that base case does not go in here

                Dim InfraPublicFundDemandBF As Double = 0
                For j = 1 To NInfraType%
                    InfraPublicFundDemandBF = InfraPublicFundDemandBF + INFRAINVESTNEWBF(R%, j) * infrainvnewpubshrm(R%, j)
                    InfraPublicFundDemandBF = InfraPublicFundDemandBF + INFRAINVESTMAINTBF(R%, j) * infrainvmaintpubshrm(R%, j)
                Next
                InfraPublicFundDemandBF = InfraPublicFundDemandBF * InfraCostGDSInfraRI(R%)  ' multiply by initial ratio, as it was done for the base demaand
                'surplus to base demand for all tasks
                Dim SurplustoBaseDemand As Double = AMAX(0, (GDSInfraAllocation - InfraPublicFundDemandBF))

                For j = 1 To NInfraType%
                    Dim BaseDemandNew = INFRAINVESTNEWBF(R%, j) * infrainvnewpubshrm(R%, j)
                    Dim TargetBaseDiffNew = AMAX(0, INFRAINVESTNEWPUB(R%, j) - BaseDemandNew)
                    Dim AddtoBaseNew = TargetBaseDiffNew * SurplustoBaseDemand / Sum_TargetBaseDiff
                    'unless fund protection is sought, make sure base fund demand is met for all infra-types, and distribute the remainder proportionately,
                    If infrabudgetprotec(R%, j) = 0 Then INFRAINVESTNEWPUB(R%, j) = CSng(Amin(INFRAINVESTNEWPUB(R%, j), BaseDemandNew + AddtoBaseNew))

                    Dim BaseDemandMnt = INFRAINVESTMAINTBF(R%, j) * infrainvmaintpubshrm(R%, j)
                    Dim TargetBaseDiffMnt = AMAX(0, INFRAINVESTMAINTPUB(R%, j) - BaseDemandMnt)
                    Dim AddtoBaseMaint = TargetBaseDiffMnt * SurplustoBaseDemand / Sum_TargetBaseDiff
                    If infrabudgetprotec(R%, j) = 0 Then INFRAINVESTMAINTPUB(R%, j) = CSng(Amin(INFRAINVESTMAINTPUB(R%, j), BaseDemandMnt + AddtoBaseMaint))
                Next j

                InfraBudImpactOverall = -9999 'use a coded value to indicate base protection needed to be activated (required for budget impact routine)

            End If  'Sum_TargetBaseDiff > 0.00000001


            '--normalize reallocated public investment---------

            '--- set aside ---
            InfraSetAsideNew(InfraTot) = 0
            InfraSetAsideMaint(InfraTot) = 0
            For j = 1 To NInfraType% - 1
                InfraSetAsideNew(j) = INFRAINVESTNEWPUB(R%, j) * Amin(infrabudgetprotec(R%, j), 1)
                InfraSetAsideMaint(j) = INFRAINVESTMAINTPUB(R%, j) * Amin(infrabudgetprotec(R%, j), 1)

                InfraSetAsideNew(InfraTot) = InfraSetAsideNew(InfraTot) + InfraSetAsideNew(j)
                InfraSetAsideMaint(InfraTot) = InfraSetAsideMaint(InfraTot) + InfraSetAsideMaint(j)
            Next

            'set aside should not exceed 90% of total allocation
            Dim adj_fac As Double
            If InfraSetAsideNew(InfraTot) + InfraSetAsideMaint(InfraTot) > 0.9 * GDSInfraAllocation Then
                Dim InfraSetAsideSought = InfraSetAsideNew(InfraTot) + InfraSetAsideMaint(InfraTot)
                Dim InfraSetAsideAvailable = 0.9 * GDSInfraAllocation
                adj_fac = InfraSetAsideAvailable / InfraSetAsideSought
                InfraSetAsideNew(InfraTot) = 0
                InfraSetAsideMaint(InfraTot) = 0
                For j = 1 To NInfraType%
                    InfraSetAsideNew(j) = InfraSetAsideNew(j) * adj_fac
                    InfraSetAsideMaint(j) = InfraSetAsideMaint(j) * adj_fac

                    InfraSetAsideNew(InfraTot) = InfraSetAsideNew(InfraTot) + InfraSetAsideNew(j)
                    InfraSetAsideMaint(InfraTot) = InfraSetAsideMaint(InfraTot) + InfraSetAsideMaint(j)
                Next j
            End If

            'distribute remaining amount------------
            InfraRemainingNew(InfraTot) = 0
            InfraRemainingMaint(InfraTot) = 0
            For j = 1 To NInfraType%
                InfraRemainingNew(j) = INFRAINVESTNEWPUB(R%, j) - InfraSetAsideNew(j)
                InfraRemainingMaint(j) = INFRAINVESTMAINTPUB(R%, j) - InfraSetAsideMaint(j)

                InfraRemainingNew(InfraTot) = InfraRemainingNew(InfraTot) + InfraRemainingNew(j)
                InfraRemainingMaint(InfraTot) = InfraRemainingMaint(InfraTot) + InfraRemainingMaint(j)
            Next j

            Dim InfraRemainingSought = InfraRemainingNew(InfraTot) + InfraRemainingMaint(InfraTot)
            Dim InfraRemainingAvailable = AMAX(0, GDSInfraAllocation - InfraSetAsideNew(InfraTot) - InfraSetAsideMaint(InfraTot))
            adj_fac = InfraRemainingAvailable / InfraRemainingSought

            InfraRemainingNew(InfraTot) = 0
            InfraRemainingMaint(InfraTot) = 0
            For j = 1 To NInfraType%
                InfraRemainingNew(j) = InfraRemainingNew(j) * adj_fac
                InfraRemainingMaint(j) = InfraRemainingMaint(j) * adj_fac

                InfraRemainingNew(InfraTot) = InfraRemainingNew(InfraTot) + InfraRemainingNew(j)
                InfraRemainingMaint(InfraTot) = InfraRemainingMaint(InfraTot) + InfraRemainingMaint(j)
            Next j

            'final amounts ------------
            For j = 1 To NInfraType%
                If INFRAINVESTMAINTPUB(R%, j) > 0 Then      'using maintenance expenditure first, as it is less likely to be 0
                    InfraBudImpactByType(R%, j) = CSng(Amin(1, (InfraSetAsideMaint(j) + InfraRemainingMaint(j)) / INFRAINVESTMAINTPUB(R%, j)))
                Else
                    If INFRAINVESTNEWPUB(R%, j) > 0 Then      'using maintenance expenditure, as it is not likely to be 0
                        InfraBudImpactByType(R%, j) = CSng(Amin(1, (InfraSetAsideNew(j) + InfraRemainingNew(j)) / INFRAINVESTNEWPUB(R%, j)))
                    Else
                        InfraBudImpactByType(R%, j) = 1
                    End If
                End If
            Next j



        End Sub

        '
        '------------------------------------------InfraStr.Bas-------------------------------------
        '-------------------------------------------------------------------------------------------------------------------
        'International Futures System (IFs) - General Public License (GPL) Declaration
        '-------------------------------------------------------------------------------------------------------------------
        '
        '
        '    Copyright 1988-2009 Barry B. Hughes
        '
        '
        '    This file is part of International Futures (IFs) developed by Barry B. Hughes, bhughes@du.edu
        '
        '    IFs is free software: you can redistribute it and/or modify
        '    it under the terms of the GNU General Public License as published by
        '    the Free Software Foundation, either version 3 of the License, or
        '    (at your option) any later version.
        '
        '    IFs is distributed in the hope that it will be useful,
        '    but WITHOUT ANY WARRANTY; without even the implied warranty of
        '    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
        '    GNU General Public License for more details.
        '
        '    You should have received a copy of the GNU General Public License
        '    along with International Futures System Development Version. If not, please see http://www.gnu.org/licenses/
        '
        '    Developers of International Futures System are required to send the modified code to the IFs core team
        '    Developers should send such code updates, if any, twice every year, on April 1st and on October 1st
        '    Code updates should be sent to Frederick S. Pardee Center for International Futures (contact: pardee@du.edu)
        '-------------------------------------------------------------------------------------------------------------------
        '------------------------------------------------------------------------------------------
        '
        '


        'mti January 2011 - start
        'form search on Road and replace with Roadpaved; SanitHH; WatsafeHH
        'mti January 2011 - end



        '
        '   Copyright by Barry B. Hughes 1988-2009
        '   Model code available for review and education only
        '   Reproduction or incorporation in other software by permission only
        '

        ' What we are today comes from out thoughts of yesterday, and our present
        ' thoughts build our life of tomorrow: our life is the creation of our mind. -Buddha

        'mti Testing Phone -start
        'Public INFRATELEMin()
        'mti Testing Phone - end
    End Module

End Namespace

