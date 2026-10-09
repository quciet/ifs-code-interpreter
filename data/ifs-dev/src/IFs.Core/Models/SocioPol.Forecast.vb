Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data
Imports IFs.Data.ProducerScope
Imports System.Math
Imports Microsoft.EntityFrameworkCore

Namespace Models

    Partial Public Module SocioPol
        '-------------------------------------------------------------------------------------------------------------------
        'International Futures System (IFs) - General Public License (GPL) Declaration
        '-------------------------------------------------------------------------------------------------------------------
        '
        '
        '    Copyright 1988-2012 Barry B. Hughes
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

        '"Live as if you were to die tommorrow."  Popular
        '"Learn as if you were to live forever."  Gandhi
        '
        '"The future is alwlays present, as a promise, a lure, and a temptation."
        '   Karl Popper
        '
        '   Copyright by Barry B. Hughes 1988-2012
        '   Model code available for review and education only
        '   Reproduction or incorporation in other software by permission only
        '

        '   Government expenditure categories:
        '       1.  Military
        '       2.  Health
        '       3.  Education
        '       4.  Research and development
        '       5.  Other (including administrative)
        '       6.  Foreign Aid

        'Public SSFINTLWARMAG() As Single

        Dim SCPOW() As Single, SNPOW() As Single, SGDSMil() As Single

        Dim swatres() As Single 'jrs 2019/10/22
        'clp 2016/08/26 - Adding GDS R&D
        Dim SGDSRandD() As Single
        Dim SWCPOW As Single, SWNPOW As Single, WGDSMIL As Single, WGDSRandD As Single, WGOV As Single, WGDPPPP As Single, WTECHP As Single, WTECH As Single
        'jrs 2020/11/19
        Dim WTECHmix As Single

        'dkb 2013/09/09 creating components for world DPI
        Dim WDPIBN As Single, WDPIMN As Single, WDPIILN As Single, WDPI As Single

        'dkb 2014/07/07 adding world Effective Human Capital component for POWER
        Dim WEHC As Single

        'jrs 2011/10/11 GPI
        Dim WGOVNUCWP As Single, WConvMilSpend As Single, WENX As Single, WENM As Single, WTradeTotal As Single, WXFDIFIN As Single
        Dim WAID As Single, WHumCapWkPopHDI As Single, WGOVREV As Single, WInnovationRD As Single, WConnectivityICTProd As Single

        'dkb 2014/10/23 - new EnergyNetExport specification - START
        Dim WENXM As Single
        'clp 2016/08/25 - Adding world energy production variable
        Dim WENProd As Single
        'dkb 2014/10/23 - new EnergyNetExport specification - END

        'dkb 2014/08/06 - adding save variables for Diplometrics POWER component calculation - START
        Public SDIPLOEMBASSY() As Single, SDIPLOIGO() As Single, SDIPLOTREATY() As Single 'jrs 2017/05/22 changed to public
        'Dim SDIPLOEMBASSYMAX, SDIPLOIGOMAX, SDIPLOTREATYMAX
        'dkb 2014/08/06 - END

        'jrs 2022/04/20
        Public SDIPLOEMBASSY2() As Single


        'mti April 2010 - Infrastructure Consolidation -start
        'jrs 2009/02/25
        'Dim MOBILshift(), BROADshift() As Double
        'jrs 2009/02/26
        'Dim ICTIndexShift()
        'jrs 2009/02/27
        'Dim SMobil()
        'jrs 2009/06/08
        'Public SBroad() As Double
        'jrs 2009/05/18
        'Dim SPENDshift()

        'jrs 2009/05/28
        'Dim SGOVREGQUAL()
        Public SGOVREGQUAL() As Single



        'mti February 2011 Infarstructure - start
        'The following adjustment factor is needed in Infrastr Module
        Public InfraCostGDSInfraRI() As Single
        Public sHlthExpPcntGDP() As Single
        'mti February 2011 Infarstructure - end

        'dkb 2018/05/09 'Declarations for drug model
        Dim drugtotalest() As Single
        Dim sdrugtotalest() As Single
        Dim DrugAmphetamineEst() As Single, DrugCocaineEst() As Single, DrugOpiateEst() As Single, DrugPrescriptionOpioidEst() As Single, DrugEstTot() As Single
        Dim sDrugAmphetamineEst() As Single, sDrugCocaineEst() As Single, sDrugOpiateEst() As Single, sDrugPrescriptionOpioidEst() As Single, sDrugEstTot() As Single
        Dim SDRUGUSEAMPHETAMINE() As Single, SDRUGUSECOCAINE() As Single, SDRUGUSEOPIATES() As Single, SDRUGUSEPRESCRIPTOPIOID() As Single
        Dim iDrugUseAmphetamine() As Single, iDrugUseCocaine() As Single, iDrugUseOpiates() As Single, iDrugUsePrescriptOpioid() As Single, iDrugUseTot() As Single
        Dim iDrugAmphetamineEst() As Single, iDrugCocaineEst() As Single, iDrugOpiateEst(), iDrugPrescriptionOpioidEst() As Single, iDrugEstTot() As Single
        Dim hhconpc() As Single, shhconpc() As Single
        'kbn 2018/09/25- Start-
        Dim MaxHomicides As Single
        'kbn 2018/09/25- END-


        'jrs 2009/06/05
        'Dim SICTBroadExp() As Double
        'Dim ICTBroadExpShift() As Double
        'Dim ICTSpendExpShift() As Double
        'Dim GovRegQualShift() As Double
        Public GovRegQualShift() As Single

        'Public ICTBroadExp() As Double

        'Dim GOVREGQUALExp() As Double
        Public GOVREGQUALExp() As Single
        'mti April 2010 - Infrastructure Consolidation -end

        'jrs 2011/01/11
        Dim GOVRISKMAX() As Single, GOVRISKMIN() As Single, GOVRISKNAME() As String, GOVRISKSIGN() As Single
        'jrs 2011/02/14 *** begin Embeddedness ***
        Dim GOVEMBEDShift() As Single, SGOVEMBED() As Single
        'jrs 2011/02/14 *** end Embeddedness ***



        'JAM 2019-08-09
        ' Dim dbD As Database, tblK As Recordset, tblR As Recordset, tblC As Recordset

        'jrs 2011/10/11
        Dim SRANDDEXP() As Single, SGOVNUCWP() As Single


        'JAM 2019-08-09
        'Dim IFsCtrys As New Collection


        'jrs 2012/11/05
        Dim DIPLOEMBASSYShift() As Single, DIPLOEMBASSYMAXShift As Single
        Dim DIPLOIGOShift() As Single, DIPLOIGOMAXShift As Single
        Dim DIPLOTREATYShift() As Single, DIPLOTREATYMAXShift As Single
        'jrs 2014/05/26
        Public PrivExpToCost() As Single
        'jrs 2014/05/30 Dim HLEXPENDPRIVShift() As Single
        Dim PrivHLExpPctGDPFxName As String
        Dim HlGrTerm() As Single
        'jrs 2016/01/13
        Public SSFINTLWARMAG() As Single
        'JRS 2016/08/02
        Dim HTCGRIShift() As Single, HTCTPIShift() As Single
        'jrs 2017/07/07
        Public SPOWERGPI() As Single
        'jrs 2022/04/20 
        Public SPOWER() As Single
        'jrs 2017/08/08
        Public SDEMOCPOLITY() As Single
        'jrs 2018/02/26
        Public SGOVRISK() As Single
        'dkb 2018/06/01
        Public SGOVINDSECUR() As Single
        Dim svindexmax As Double
        'JAM 2019-08-09 New DEclarations

        Dim DEMOCPOLITYI() As Single, smdemocpolity() As Single
        'jrs 2023/10/13
        Dim DEMOCPOLYARCHYShift() As Single
        Public SDEMOCPOLYARCHY() As Single
        'Dim SDEMOCPOLYARCHYFX() As Single
        'Dim PolyarchyGR() As Single?

        Dim swingeffects() As Single, demglobaleffects() As Single
        Dim FREEDOMI() As Single, econfreei() As Single, GovEffectAdd() As Single, GovCorruptAdd() As Single
        Dim GEMI() As Single, GEMPI() As Single, democcomp() As Single
        Dim YTHBULGEI() As Single, gdprmovavg() As Single, ssfintlwarallLoc() As Single, econfreeri() As Single
        'BBH 2011/06/18
        Dim energydepend() As Single, ContiguityIntlWarAddI() As Single
        Dim democpolityri() As Single
        Dim snumnwp() As Single, INUMNWP() As Single, inumnwpgr() As Single, numnwpmin() As Single
        Dim SFINSTABALLRI() As Single, SFINSTABMAGRI() As Single, SFINSTABY1RI() As Single
        'dsb 2026/08/04
        Dim ContiguityCivilWarUCDPAddI() As Single
        'BBH 2011/06/18
        Dim SFINTLWARALLADD() As Single, SFINTLWARMAGRI() As Single, SFINTLWARY1RI() As Single
        'jrs 2024/05/17
        Dim ssfcivilwarUCDPLoc() As Single, SFCIVILWARUCDPADD() As Single, SFCIVILWARUCDPI() As Single
        'dsb 2026/06/03
        Dim SSFCIVILWARUCDP() As Single, SSFINTLWARALL() As Single
        'dsb 2026/1/8
        Dim ssfcivilwarPardeeLoc() As Single, SFCIVILWARPARDEEADD() As Single, SFCIVILWARPARDEEI() As Single
        Dim ContiguityCivilWarPardeeAddI() As Single
        'dsb 2026/1/13
        Dim SFCIVILWARPOPIMPSHIFTBASE() As Single
        Public SSFCIVILWARPOPIMP() As Single
        'dsb 2026/1/15 
        Public SSFCIVILWARPARDEE() As Single, GOVEFFECT_Lag1() As Single, GOVEFFECT_Lag() As Single, SVDTHSWOMENCHILDREN_Lag1() As Single, SVDTHSWOMENCHILDREN_Lag() As Single, SSVDTHSOTHERINTERPERSON_Pop() As Single
        'jrs 2025/08/27
        Dim SPRDSEC() As Single, SPRDSECFX() As Single
        Public AvgDistanceAtoB() As Single
        'bbh 2011/09/21
        Dim SFINTLWARALLI() As Single, SFINTLWARMAGI() As Single
        Dim SFPredictionI() As Single
        Dim SSURVSEI(,) As Single
        Dim SwingStsDemoc(9) As Single, SwingStsNumb(9) As Single, sedyrsag15() As Single, EDYRSAG15I() As Single
        'bbh 2011/08/19
        Dim sedyrsag15fem() As Single, EDYRSAG15FemI() As Single
        Dim RandDExpScale() As Single, RAndDExpI() As Single, EdTerGRateScale() As Single, EdTerGRateI() As Single
        'jrs 2009/04/22
        Dim GovRegQualAdd() As Single

        'mti 2014/11/14 -start
        Dim GovBusCostShift() As Single
        'mti 2014/11/14 - end

        'mti 2015/03/04 - start
        Dim GovBusRegIndShift() As Single
        Dim PoliceShift() As Single, ConflictShift() As Single, SelfHarmShift() As Single, HomicideShift() As Single, WomenandChilshift() As Single, SSVDTHSSELFHARM() As Single, SSVDTHSPOLICS() As Single, Womenshift() As Single, ChilShift() As Single, SelfHarmEstimate() As Single, sSelfHarmEstimate() As Single
        Dim SSVDTHSCONFLICT() As Single, SSVDTHSOTHERINTERPERSON() As Single, SSVDTHSWOMENCHILDREN() As Single, SSOCIETALVIOLENCEDEATHS() As Single, SHOMICIDEINDEX() As Single, SSVDTHSWOMEN() As Single, SSVDTHSCHIL() As Single
        Dim PoliceEstimate() As Single, ConflictEstimate() As Single, HomicideEstimate() As Single, WomenandChilEstimate() As Single
        Dim sPoliceEstimate() As Single, sConflictEstimate() As Single, sWomenandChilEstimate() As Single, sHomicideEstimate() As Single
        Dim GIIShift() As Single, GIII() As Single 'jrs 2020/03/23
        Dim DemocWaveI As Single
        Dim SDEMOCWAVE As Single
        Dim SDEMOCWVCOUNT As Single
        Dim SDEMOCWVDIR As Single
        Dim educ15term As Single
        Dim AdultMaleTerm As Double
        Dim WomenTerm As Double
        Dim ChildrenTerm As Double
        Dim poptotalterm As Double
        Dim MatPostRAdj() As Double, TradSRatAdj() As Double, SurvSEAdj() As Double
        Dim SMATPOSTR(,) As Single, MatPostRI(,) As Single
        Dim SSURVSE(,) As Single, SURVSEI(,) As Single
        Dim STradSRat(,) As Single, TRADSRATI(,) As Single
        Dim POWERI() As Single
        Dim gkmul() As Single, milspendrati() As Single, LenWar(,) As Single, PowerTranTermI(,) As Single
        Dim AllyI(,) As Single
        Dim nowarper(,) As Single, gpowertermI(,) As Single, TerDisputeI(,) As Single
        Public ThreatIData(,) As Single, ThreatIPred(,) As Single 'clp 2024/07/10
        Dim WarGDPOwn() As Single, WarGDPOpp() As Single, WarCPOWOwn() As Single, WarCPOWOpp() As Single
        Dim WarNPOWOwn() As Single, WarNPOWOpp() As Single, CPOWDM() As Single, NPOWDM() As Single
        Dim gk(,) As Single, gkri(,) As Single, gkcomp() As Single, healthgdpri() As Single, RandDI() As Single
        'jrs 2021/09/09
        Dim educgdpri() As Single
        Public BlThreatInital_PwrRatio(,) As Single, BlThreatInital_TrdShr(,) As Single, BlThreatInital_AffGap(,) As Single, IDyTrdShrGdp(,) As Single, BlThreatInital_DemMin(,) As Single 'clp 2025/04/29 adding ability to keep values constant for THREAT components
        Public sumthreatI() As Single ', SGDSMil() As Single 'clp 2024/07/10 ***
        Dim govconwb() As Single, gdswb(,) As Single, RandDExpSh() As Single
        'BBH 4/16/04  V5.0
        Dim SMORTMG() As Single
        'mti January 06, 2009
        Dim EdCostGDSEdRI() As Single, SEdTotCost() As Single, EdCostGDSEdRIShift() As Single
        'mti January 06, 2009
        'Dim InfraCostGDSInfraRI() As Single, , 
        Dim sInfraTotCost() As Single, InfraCostGDSInfraRIShift() As Single
        Dim DIPLOINGOPROGRAMShift() As Single, DIPLOINGOADVOCACYShift() As Single
        Dim UKcol() As Single, FRAcol() As Single, GERcol() As Single, PORcol() As Single
        Dim SPAcol() As Single, NETcol() As Single, RUScol() As Single
        'mti 2016/07/16 Target value in GDS%GDP start
        'Declare array variable for saving initial values and target GDS values (when targeting is set as a parameter)
        Dim igk(,) As Single, GDSTrgt(,) As Single
        Dim gdssetaside() As Single
        Dim GOVEMBEDGR() As Single?
        Dim SWGDP As Single
        Dim GLOCOMI As Single
        Dim PConGreatI As Single
        ' Dim CalcInfraBudgetDemand() As Single
        Dim InfrabudallocNew() As Single
        Dim InfrabudallocMnt() As Single
        'Public sedyrsag15(,) As Single 'JAM 2019-08-21
        Public SGEM() As Single
        Public womenjurshift(), womenlegshift(), womenproprightshift(), theta1shift(), theta2shift(), theta3shift() As Single 'clp 2026/02/25 Add V-Dem indicators for Essex collaboration

        Private Function CalcInfraBudgetDemand(R As Int32) As Single
            'mti April 2011 - change whole proc

            'Estimate public portion of the infrastructure investment and sum to a total budget demand
            Dim InfCt%

            INFRABUDDEMMNT(R%, InfraTot) = 0
            INFRABUDDEMNEW(R%, InfraTot) = 0

            CalcInfraBudgetDemand = 0

            For InfCt% = 1 To NInfraType%
                INFRABUDDEMNEW(R%, InfCt%) = infrainvnewpubshrm(R%, InfCt%) * INFRAINVESTNEW(R%, InfCt%)
                INFRABUDDEMMNT(R%, InfCt%) = infrainvmaintpubshrm(R%, InfCt%) * INFRAINVESTMAINT(R%, InfCt%)
                CalcInfraBudgetDemand = CalcInfraBudgetDemand + INFRABUDDEMNEW(R%, InfCt%) + INFRABUDDEMMNT(R%, InfCt%)  'billion dollars
                INFRABUDDEMNEW(R%, InfraTot) = INFRABUDDEMNEW(R%, InfraTot) + INFRABUDDEMNEW(R%, InfCt%)
                INFRABUDDEMMNT(R%, InfraTot) = INFRABUDDEMMNT(R%, InfraTot) + INFRABUDDEMMNT(R%, InfCt%)
            Next InfCt%
            'mti April 2011 - end
        End Function


        'jrs 2009/06/09 added sedyrsag15 as parameter
        Private Sub ComputeDrugs(context As IForecastContext)
            'kbn 2018/05/09- Take Entire Procedure
            'sErrorSub = "ComputeDrugs" & CStr(IFUN)
            '   'dkb 2018/05/09
            Dim RemainingPotentialHHConPC As Single
            Dim Zamph As Single
            Dim sdrugtotal As Single
            Dim drugtotal As Single
            Dim zcoke As Single
            Dim Zopiate As Single
            Dim Zpresopioid As Single
            Dim drugest As Single
            Dim Ztotdrug As Single
            Dim drugadjust As Single
            Dim RemainingPotentialAmph As Single
            Dim RemainingPotentialCocaine As Single
            Dim RemainingPotentialOpiates As Single
            Dim RemainingPotentialOpioids As Single

            If IY% > 1 Then
                For R% = 1 To NR%

                    If C(R%) / POP(R%) >= 30 Then
                        hhconpc(R%) = shhconpc(R%)
                    ElseIf (C(R%) / POP(R%)) > 25 And (C(R%) / POP(R%)) < 30 Then
                        RemainingPotentialHHConPC = 30 - (C(R%) / POP(R%))
                        hhconpc(R%) = AMAX(0.002, Amin((C(R%) / POP(R%)), hhconpc(R%) + (RemainingPotentialHHConPC / 20)))
                    Else
                        hhconpc(R%) = C(R%) / POP(R%)
                    End If

                    shhconpc(R%) = hhconpc(R%)

                    '--------------------Amphetamines----------------------------
                    'kbn 2018/01/20 Call logistic function and apply shift factor below. Apply upper and lower bounds
                    'dkb 2018/05/09 - First order forecasts are now just the growth in the forecast equation (recall that the shift factors are really just initial values)

                    'Call AnalFunc("Amphetmanine Drug prevalence versus youth bulge and GEM", Array(YTHBULGE(R%), GEM(R%)), Zamph, Array("Youthbulge", "GEM"))

                    '--------------------------------------------------
                    Call AnalFunc("Amphetmanine Drug prevalence versus youth bulge and GEM",
                        New Single() {YTHBULGE(R%), GEM(R%)}, Zamph,
                        New String() {"Youthbulge", "GEM"}, context)


                    DrugAmphetamineEst(R%) = CSng(AMAX(Amin(((100 * (Exp(Zamph) / (1 + Exp(Zamph))))), 3), 0.002))




                    DRUGUSEAMPHETAMINE(R%) = CSng(SDRUGUSEAMPHETAMINE(R%) * AMAX(Amin(DrugAmphetamineEst(R%) / sDrugAmphetamineEst(R%), 1.05), 0.95))

                    '--------------------Cocaine--------------------
                    'kbn 2018/01/20 Call logistic function and apply shift factor below. Apply upper and lower bounds
                    '          Call AnalFunc("Cocaine Drug prevalence versus consumption per person, GEM and GINI",
                    'Array(hhconpc(R%), GEM(R%), GINIDOM(R%)), zcoke, Array("C/POP", "GEM", "GINIDOM"))            'dkb: Limited HHConPC (asuming theres a limit to how much happiness money can buy)

                    Call AnalFunc("Cocaine Drug prevalence versus consumption per person, GEM and GINI",
                        New Single() {hhconpc(R%), GEM(R%), GINIDOM(R%)}, zcoke,
                        New String() {"C/POP", "GEM", "GINIDOM"}, context)

                    DrugCocaineEst(R%) = CSng(AMAX(Amin((100 * (Exp(zcoke) / (1 + Exp(zcoke)))), 2.5), 0.002))
                    DRUGUSECOCAINE(R%) = CSng(SDRUGUSECOCAINE(R%) * AMAX(Amin(DrugCocaineEst(R%) / sDrugCocaineEst(R%), 1.05), 0.95))

                    '-------------------Opiates---------------------
                    'kbn 2018/01/20 Call logistic function and apply shift factor below. Apply upper and lower bounds
                    '            Call AnalFunc("Opiate Drug prevalence",
                    'Array((100 * SINCOMELT310LN(R%) / POP(R%)), (100 * POPGT65(R%) / POP(R%)), (100 * SPOPURBAN(R%) / POP(R%))), Zopiate, Array("Poverty", "POP over the age of 65", "Urbanpopulation")) 'dkb: Swaped in POPGT65% for YouthBulge

                    'jrs 2021/09/02 changed from 3.10 to 3.20/day
                    'Call AnalFunc("Opiate Drug prevalence",
                    '    New Single() {(100 * SINCOMELT310LN(R%) / POP(R%)), (100 * POPGT65(R%) / POP(R%)), (100 * SPOPURBAN(R%) / POP(R%))}, Zopiate,
                    '    New String() {"Poverty", "POP over the age of 65", "Urbanpopulation"}, context)
                    'jrs 2022/11/21
                    If ConstantDollarsYear = 2011 Then
                        Call AnalFunc("Opiate Drug prevalence",
                        New Single() {(100 * SINCOMELT320LN(R%) / POP(R%)), (100 * POPGT65(R%) / POP(R%)), (100 * SPOPURBAN(R%) / POP(R%))}, Zopiate,
                        New String() {"Poverty", "POP over the age of 65", "Urbanpopulation"}, context)
                    ElseIf ConstantDollarsYear = 2017 Then 'jrs 2025/09/12
                        Call AnalFunc("Opiate Drug prevalence",
                        New Single() {(100 * SINCOMELT365LN(R%) / POP(R%)), (100 * POPGT65(R%) / POP(R%)), (100 * SPOPURBAN(R%) / POP(R%))}, Zopiate,
                        New String() {"Poverty", "POP over the age of 65", "Urbanpopulation"}, context)
                    Else 'jrs 2025/09/12
                        Call AnalFunc("Opiate Drug prevalence",
                        New Single() {(100 * SINCOMELT420LN(R%) / POP(R%)), (100 * POPGT65(R%) / POP(R%)), (100 * SPOPURBAN(R%) / POP(R%))}, Zopiate,
                        New String() {"Poverty", "POP over the age of 65", "Urbanpopulation"}, context)
                    End If

                    DrugOpiateEst(R%) = CSng(AMAX(Amin((100 * (Exp(Zopiate) / (1 + Exp(Zopiate)))), 4), 0.002))
                    DRUGUSEOPIATES(R%) = CSng(SDRUGUSEOPIATES(R%) * AMAX(Amin((DrugOpiateEst(R%) / sDrugOpiateEst(R%)), 1.05), 0.95))

                    ''------------------PrecriptionOpioids---------------------------
                    'kbn 2018/01/20 Call logistic function and apply shift factor below. Apply upper and lower bounds
                    'jrs 2019/08/02 added max of 100% for Health Expenditures
                    'Call AnalFunc("Prescription Drug Use prevalence versus health expenditure", _
                    'Array((100 * (GDS(R%, Health) + HLEXPENDPRIV(R%)) / GDP(R%))), Zpresopioid, Array("Healthexpenditure")) 'prescription opioids
                    '            Call AnalFunc("Prescription Drug Use prevalence versus health expenditure",
                    'Array((100 * Amin((GDS(R%, Health) + HLEXPENDPRIV(R%)) / GDP(R%), 1))), Zpresopioid, Array("Healthexpenditure")) 'prescription opioids

                    Call AnalFunc("Prescription Drug Use prevalence versus health expenditure",
                        New Single() {(100 * Amin((GDS(R%, Health) + HLEXPENDPRIV(R%)) / GDP(R%), 1))}, Zpresopioid,
                        New String() {"Healthexpenditure"}, context)

                    DrugPrescriptionOpioidEst(R%) = CSng(AMAX(Amin((100 * (Exp(Zpresopioid) / (1 + Exp(Zpresopioid)))), 5.5), 0.002))
                    DRUGUSEPRESCRIPTOPIOID(R%) = CSng(SDRUGUSEPRESCRIPTOPIOID(R%) * AMAX(Amin(DrugPrescriptionOpioidEst(R%) / sDrugPrescriptionOpioidEst(R%), 1.05), 0.95))

                    'dkb 2018/05/09

                    '----------- TOTAL DRUG USE AND RENORMALIZATION-----------------------
                    'If RegName(R%) = "Nigeria" And IY > 25 Then Stop

                    If drugglobalconvsw(R%) = 1 Then  'kbn: The user has the option to turn off this switch
                        sdrugtotal = (SDRUGUSEAMPHETAMINE(R%) + SDRUGUSECOCAINE(R%) + SDRUGUSEOPIATES(R%) + SDRUGUSEPRESCRIPTOPIOID(R%)) / drugusepolyindex(R%)
                        drugtotal = (DRUGUSEAMPHETAMINE(R%) + DRUGUSECOCAINE(R%) + DRUGUSEOPIATES(R%) + DRUGUSEPRESCRIPTOPIOID(R%)) / drugusepolyindex(R%)

                        'Call AnalFunc("Total illicit drug use (Percent)", Array(hhconpc(R%), YTHBULGE(R%)), Ztotdrug, Array("Household consumption", "Youth bulge"))

                        Call AnalFunc("Total illicit drug use (Percent)",
                        New Single() {hhconpc(R%), YTHBULGE(R%)}, Ztotdrug,
                        New String() {"Household consumption", "Youth bulge"}, context)



                        drugest = CSng(100 * (Exp(Ztotdrug) / (1 + Exp(Ztotdrug))))
                        drugest = CSng(Amin(AMAX(1.3 * drugest, 0.2), drugglobalconv(R%)))

                        If drugest > drugtotal Then
                            drugtotalest(R%) = ConvergeOverTime(drugtotal, CSng(Amin(drugest, 1.02 * drugtotal)), 100)
                            drugadjust = CSng(Amin(drugtotalest(R%) - drugtotal, 0.1 * drugtotal))               'dkb: Bounding target at 25% above or below current value
                        Else
                            drugtotalest(R%) = ConvergeOverTime(drugtotal, CSng(AMAX(drugest, 0.98 * drugtotal)), 100)
                            drugadjust = CSng(AMAX(drugtotalest(R%) - drugtotal, -0.1 * drugtotal))
                        End If

                        DRUGUSEAMPHETAMINE(R%) = DRUGUSEAMPHETAMINE(R%) + (drugadjust * DRUGUSEAMPHETAMINE(R%) / (drugtotal * drugusepolyindex(R%)))
                        DRUGUSECOCAINE(R%) = DRUGUSECOCAINE(R%) + (drugadjust * DRUGUSECOCAINE(R%) / (drugtotal * drugusepolyindex(R%)))
                        DRUGUSEOPIATES(R%) = DRUGUSEOPIATES(R%) + (drugadjust * DRUGUSEOPIATES(R%) / (drugtotal * drugusepolyindex(R%)))
                        DRUGUSEPRESCRIPTOPIOID(R%) = DRUGUSEPRESCRIPTOPIOID(R%) + (drugadjust * DRUGUSEPRESCRIPTOPIOID(R%) / (drugtotal * drugusepolyindex(R%)))

                    End If

                    'dkb 2018/05/09 - Moving growth constraints down here
                    '-------CONSTRAINTS ON THE GROWTH RATES FOR INDIVIDUAL DRUG TYPES------------------
                    '---------- Amphetamine
                    'kbn 2018/01/20 Now start code below that helps countries gradually reach their peak values
                    If DRUGUSEAMPHETAMINE(R%) > 2 Then                                                                                                'dkb: bumped max down to 3
                        RemainingPotentialAmph = 3 - SDRUGUSEAMPHETAMINE(R%)
                        DRUGUSEAMPHETAMINE(R%) = AMAX(0.002, Amin(DRUGUSEAMPHETAMINE(R%), SDRUGUSEAMPHETAMINE(R%) + (RemainingPotentialAmph / 20)))
                    End If

                    'kbn 2018/01/20 Apply multipliers below
                    DRUGUSEAMPHETAMINE(R%) = DRUGUSEAMPHETAMINE(R%) * druguseamphetaminem(R%)

                    'kbn 2018/01/20 Restrict growth rates (upward and downward) to 2 percent per year. Ensure that this happens only when parameters are not activated.
                    If druguseamphetaminem(R%) = 1 And Abs(((DRUGUSEAMPHETAMINE(R%) - SDRUGUSEAMPHETAMINE(R%)) / AMAX(0.002, SDRUGUSEAMPHETAMINE(R%)))) > 0.02 _
                    And DRUGUSEAMPHETAMINE(R%) > SDRUGUSEAMPHETAMINE(R%) Then   'This line restricts the growth rate to 2 percent in upward direction (only when parameter is not activated)
                        DRUGUSEAMPHETAMINE(R%) = CSng(AMAX(0.002, Amin(SDRUGUSEAMPHETAMINE(R%) * 1.02, 3)))
                    End If

                    If druguseamphetaminem(R%) = 1 And Abs(((DRUGUSEAMPHETAMINE(R%) - SDRUGUSEAMPHETAMINE(R%)) / AMAX(0.002, SDRUGUSEAMPHETAMINE(R%)))) > 0.02 _
                        And DRUGUSEAMPHETAMINE(R%) < SDRUGUSEAMPHETAMINE(R%) Then 'This line restricts the growth rate to 2 percent in downward direction (only when parameter is not activated)
                        DRUGUSEAMPHETAMINE(R%) = CSng(AMAX(0.002, Amin(SDRUGUSEAMPHETAMINE(R%) * 0.98, 3)))
                    End If

                    '---------- Cocaine
                    'kbn 2018/01/20 Now start code below that helps countries gradually reach their peak values
                    If DRUGUSECOCAINE(R%) > 1.9 Then
                        RemainingPotentialCocaine = CSng(2.5 - SDRUGUSECOCAINE(R%))
                        DRUGUSECOCAINE(R%) = AMAX(0.002, Amin(DRUGUSECOCAINE(R%), SDRUGUSECOCAINE(R%) + RemainingPotentialCocaine / 20))
                    End If

                    'kbn 2018/01/20 Apply multipliers below
                    DRUGUSECOCAINE(R%) = DRUGUSECOCAINE(R%) * drugusecocainem(R%)

                    'kbn 2018/01/20 Restrict growth rates (upward and downward) to 2 percent per year. Ensure that this happens only when parameters are not activated.
                    If drugusecocainem(R%) = 1 And Abs(((DRUGUSECOCAINE(R%) - SDRUGUSECOCAINE(R%)) / AMAX(0.002, SDRUGUSECOCAINE(R%)))) > 0.02 _
        And DRUGUSECOCAINE(R%) > SDRUGUSECOCAINE(R%) Then ' kbn 2018/01/24- This line restricts the growth rate to 2 percent in upward direction (only when parameter is not activated)
                        DRUGUSECOCAINE(R%) = CSng(AMAX(0.002, Amin(SDRUGUSECOCAINE(R%) * 1.02, 2.5)))
                    End If

                    If drugusecocainem(R%) = 1 And Abs(((DRUGUSECOCAINE(R%) - SDRUGUSECOCAINE(R%)) / AMAX(0.002, SDRUGUSECOCAINE(R%)))) > 0.02 _
        And DRUGUSECOCAINE(R%) < SDRUGUSECOCAINE(R%) Then 'kbn 2018/01/24- This line restricts the growth rate to 2 percent in downward direction (only when parameter is not activated)
                        DRUGUSECOCAINE(R%) = CSng(AMAX(0.002, Amin(SDRUGUSECOCAINE(R%) * 0.98, 2.5)))
                    End If

                    '---------- Opiates
                    'kbn 2018/01/20 Now start code below that helps countries gradually reach their peak values
                    If DRUGUSEOPIATES(R%) > 3 Then
                        RemainingPotentialOpiates = 4 - SDRUGUSEOPIATES(R%)
                        DRUGUSEOPIATES(R%) = AMAX(0.002, Amin(DRUGUSEOPIATES(R%), SDRUGUSEOPIATES(R%) + RemainingPotentialOpiates / 20))
                    End If

                    'kbn 2018/01/20 Apply multipliers below
                    DRUGUSEOPIATES(R%) = DRUGUSEOPIATES(R%) * druguseopiatesm(R%)

                    'kbn 2018/01/20 Restrict growth rates (upward and downward) to 1 percent per year. Ensure that this happens only when parameters are not activated.
                    If druguseopiatesm(R%) = 1 And Abs(((DRUGUSEOPIATES(R%) - SDRUGUSEOPIATES(R%)) / AMAX(0.002, SDRUGUSEOPIATES(R%)))) > 0.02 _
        And DRUGUSEOPIATES(R%) > SDRUGUSEOPIATES(R%) Then ' kbn 2018/01/24- This line restricts the growth rate to 1 percent in upward direction (only when parameter is not activated)
                        DRUGUSEOPIATES(R%) = CSng(AMAX(0.002, Amin(SDRUGUSEOPIATES(R%) * 1.02, 4)))
                    End If

                    If druguseopiatesm(R%) = 1 And Abs(((DRUGUSEOPIATES(R%) - SDRUGUSEOPIATES(R%)) / AMAX(0.002, SDRUGUSEOPIATES(R%)))) > 0.01 _
        And DRUGUSEOPIATES(R%) < SDRUGUSEOPIATES(R%) Then 'kbn 2018/01/24- This line restricts the growth rate to 2 percent in downward direction (only when parameter is not activated)
                        DRUGUSEOPIATES(R%) = CSng(AMAX(0.002, Amin(SDRUGUSEOPIATES(R%) * 0.99, 4)))
                    End If

                    '---------- Prescription Opioids
                    'kbn 2018/01/20 Now start code below that helps countries gradually reach their peak values
                    If DRUGUSEPRESCRIPTOPIOID(R%) > 4 And DRUGUSEPRESCRIPTOPIOID(R%) < 5.5 Then
                        RemainingPotentialOpioids = CSng(5.5 - SDRUGUSEPRESCRIPTOPIOID(R%))
                        DRUGUSEPRESCRIPTOPIOID(R%) = AMAX(0.002, Amin(DRUGUSEPRESCRIPTOPIOID(R%), SDRUGUSEPRESCRIPTOPIOID(R%) + RemainingPotentialOpioids / 20))
                    End If

                    'kbn 2018/01/20 Apply multipliers below
                    DRUGUSEPRESCRIPTOPIOID(R%) = DRUGUSEPRESCRIPTOPIOID(R%) * druguseprescriptopioidm(R%)

                    'kbn 2018/01/20 Restrict growth rates (upward and downward) to 1 percent per year. Ensure that this happens only when parameters are not activated.
                    If druguseprescriptopioidm(R%) = 1 And Abs(((DRUGUSEPRESCRIPTOPIOID(R%) - SDRUGUSEPRESCRIPTOPIOID(R%)) / AMAX(0.002, SDRUGUSEPRESCRIPTOPIOID(R%)))) > 0.01 _
         And DRUGUSEPRESCRIPTOPIOID(R%) > SDRUGUSEPRESCRIPTOPIOID(R%) Then ' kbn 2018/01/24- This line restricts the growth rate to 2 percent in upward direction (only when parameter is not activated)
                        DRUGUSEPRESCRIPTOPIOID(R%) = CSng(AMAX(0.002, Amin(SDRUGUSEPRESCRIPTOPIOID(R%) * 1.01, 5.5)))
                    End If

                    If druguseprescriptopioidm(R%) = 1 And Abs(((DRUGUSEPRESCRIPTOPIOID(R%) - SDRUGUSEPRESCRIPTOPIOID(R%)) / AMAX(0.002, SDRUGUSEPRESCRIPTOPIOID(R%)))) > 0.01 _
         And DRUGUSEPRESCRIPTOPIOID(R%) < SDRUGUSEPRESCRIPTOPIOID(R%) Then 'kbn 2018/01/24- This line restricts the growth rate to 2 percent in downward direction (only when parameter is not activated)
                        DRUGUSEPRESCRIPTOPIOID(R%) = CSng(AMAX(0.002, Amin(SDRUGUSEPRESCRIPTOPIOID(R%) * 0.99, 5.5)))
                    End If


                    sDrugAmphetamineEst(R%) = CSng(AMAX(DrugAmphetamineEst(R%), 0.002))
                    sDrugCocaineEst(R%) = CSng(AMAX(DrugCocaineEst(R%), 0.002))
                    sDrugOpiateEst(R%) = CSng(AMAX(DrugOpiateEst(R%), 0.002))
                    sDrugPrescriptionOpioidEst(R%) = CSng(AMAX(DrugPrescriptionOpioidEst(R%), 0.002))

                    SDRUGUSEAMPHETAMINE(R%) = DRUGUSEAMPHETAMINE(R%)
                    SDRUGUSECOCAINE(R%) = DRUGUSECOCAINE(R%)
                    SDRUGUSEOPIATES(R%) = DRUGUSEOPIATES(R%)
                    SDRUGUSEPRESCRIPTOPIOID(R%) = DRUGUSEPRESCRIPTOPIOID(R%)

                Next R%
                'kbn 2018/02/16 end
            End If

        End Sub



        'Private Sub computeRegQualExp(ByRef sedyrsag15 As Variant, ByVal b2Dims As Boolean)
        Private Sub computeRegQualExp(ByRef sedyrsag15(,) As Single, context As IForecastContext)
            If IY = 1 Then
                ReDim GOVREGQUALExp(NR)
                ReDim GovRegQualShift(NR)
            End If

            Dim R As Integer
            Dim rVal As Single
            Dim EffectAdd As Single
            For R = 1 To NR
                'Expected Government Reg. Quality
                '--------------------------------
                'If b2Dims Then

                '    Call AnalFunc("Governance Regulatory Quality (2006)",
                'Array(GDPPCP(R%), sedyrsag15(R%, 3)), rVal,
                'Array("GDP2000PCPPP(MOSTRECENT)/ 1000 (Linear)", "EdYearsAge15Total(MOSTRECENT) (Log)")) 'jrs 2014/10/09 added DepVar Names to check order

                Call AnalFunc("Governance Regulatory Quality (2006)",
                        New Single() {GDPPCP(R%), sedyrsag15(R%, 3)}, rVal,
                        New String() {"GDP2000PCPPP(MOSTRECENT)/ 1000 (Linear)", "EdYearsAge15Total(MOSTRECENT) (Log)"}, context) 'jrs 2014/10/09 added DepVar Names to check order



                'Else
                '    Call AnalFunc("Governance Regulatory Quality (2006)",
                'Array(GDPPCP(R%), sedyrsag15(R%)), rVal,
                'Array("GDP2000PCPPP(MOSTRECENT)/ 1000 (Linear)", "EdYearsAge15Total(MOSTRECENT) (Log)")) 'jrs 2014/10/09 added DepVar Names to check order

                'Call AnalFunc("Governance Regulatory Quality (2006)",
                '   New Single() {GDPPCP(R%), sedyrsag15(R%)}, rVal,
                '    New String() {"GDP2000PCPPP(MOSTRECENT)/ 1000 (Linear)", "EdYearsAge15Total(MOSTRECENT) (Log)"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                'End If
                rVal = rVal + CSng(2.5)
                If IY = 1 Then
                    rVal = Amin(AMAX(rVal, 0), 5)
                    If govregqualm(R) > 0 Then
                        GovRegQualShift(R%) = GOVREGQUAL(R%) / govregqualm(R) - rVal
                        GOVREGQUALExp(R) = GOVREGQUAL(R) / govregqualm(R)
                    Else
                        GovRegQualShift(R%) = -1 * rVal
                        GOVREGQUALExp(R) = GOVREGQUAL(R)
                    End If
                Else
                    EffectAdd = ConvergeOverTime(GovRegQualShift(R%), 0, goveffconv) 'Base, Target, Years
                    GOVREGQUALExp(R) = AMAX(0, Amin(5, (rVal + EffectAdd)))
                End If
            Next R
        End Sub

        Private Sub computeRegQualExp(ByRef sedyrsag15() As Single, context As IForecastContext)

            Dim R As Integer
            Dim rVal As Single
            Dim EffectAdd As Single
            For R = 1 To NR
                'Expected Government Reg. Quality
                '--------------------------------
                'If b2Dims Then

                '    Call AnalFunc("Governance Regulatory Quality (2006)",
                'Array(GDPPCP(R%), sedyrsag15(R%, 3)), rVal,
                'Array("GDP2000PCPPP(MOSTRECENT)/ 1000 (Linear)", "EdYearsAge15Total(MOSTRECENT) (Log)")) 'jrs 2014/10/09 added DepVar Names to check order

                'Call AnalFunc("Governance Regulatory Quality (2006)",
                '        New Single() {GDPPCP(R%), sedyrsag15(R%, 3)}, rVal,
                '        New String() {"GDP2000PCPPP(MOSTRECENT)/ 1000 (Linear)", "EdYearsAge15Total(MOSTRECENT) (Log)"}, context) 'jrs 2014/10/09 added DepVar Names to check order



                'Else
                '    Call AnalFunc("Governance Regulatory Quality (2006)",
                'Array(GDPPCP(R%), sedyrsag15(R%)), rVal,
                'Array("GDP2000PCPPP(MOSTRECENT)/ 1000 (Linear)", "EdYearsAge15Total(MOSTRECENT) (Log)")) 'jrs 2014/10/09 added DepVar Names to check order

                Call AnalFunc("Governance Regulatory Quality (2006)",
                       New Single() {GDPPCP(R%), sedyrsag15(R%)}, rVal,
                        New String() {"GDP2000PCPPP(MOSTRECENT)/ 1000 (Linear)", "EdYearsAge15Total(MOSTRECENT) (Log)"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                'End If
                rVal = rVal + CSng(2.5)
                If IY = 1 Then
                    rVal = Amin(AMAX(rVal, 0), 5)
                    If govregqualm(R) > 0 Then
                        GovRegQualShift(R%) = GOVREGQUAL(R%) / govregqualm(R) - rVal
                        GOVREGQUALExp(R) = GOVREGQUAL(R) / govregqualm(R)
                    Else
                        GovRegQualShift(R%) = -1 * rVal
                        GOVREGQUALExp(R) = GOVREGQUAL(R)
                    End If
                Else
                    EffectAdd = ConvergeOverTime(GovRegQualShift(R%), 0, goveffconv) 'Base, Target, Years
                    GOVREGQUALExp(R) = AMAX(0, Amin(5, (rVal + EffectAdd)))
                End If
            Next R
        End Sub

        Private Sub ValueChange(context As IForecastContext)


            Dim MatPostRComp As Double
            Dim SurvSEComp As Double
            Dim TradSRatComp As Double


            'If IY% > 1 Then  GoTo AllButFirst
            If IY% = 1 Then

                ReDim MatPostRAdj(NR%), TradSRatAdj(NR%), SurvSEAdj(NR%)
                ReDim SMATPOSTR(NR%, WVSCohorts), MatPostRI(NR%, WVSCohorts)
                ReDim SSURVSE(NR%, WVSCohorts), SURVSEI(NR%, WVSCohorts)
                ReDim STradSRat(NR%, WVSCohorts), TRADSRATI(NR%, WVSCohorts)

                '
                '   First Year 1: Compute Adjustments for Total Cultural Indices
                '       Necessary because WVS values assume different population cohort sizes
                '   -------------------------------------------------------------------------

                For R% = 1 To NR%
                    For cc% = 1 To WVSCohorts 'Last, 7th cohort is total adult population
                        MatPostRI(R%, cc%) = MATPOSTR(R%, cc%)
                        SMATPOSTR(R%, cc%) = MATPOSTR(R%, cc%)
                        SURVSEI(R%, cc%) = SURVSE(R%, cc%)
                        SSURVSE(R%, cc%) = SURVSE(R%, cc%)
                        TRADSRATI(R%, cc%) = TRADSRAT(R%, cc%)
                        STradSRat(R%, cc%) = TRADSRAT(R%, cc%)
                    Next cc%

                    MatPostRComp = 0
                    For cc% = 1 To WVSCohorts - 1
                        MatPostRComp = MatPostRComp +
                    MATPOSTR(R%, cc%) * PopCohorts(R%, cc%)
                    Next cc%
                    MatPostRComp = MatPostRComp /
                AMAX(0.01, PopCohorts(R%, WVSCohorts))
                    MatPostRAdj(R%) = MatPostRI(R%, WVSCohorts) - MatPostRComp

                    SurvSEComp = 0
                    For cc% = 1 To WVSCohorts - 1
                        SurvSEComp = SurvSEComp +
                    SURVSE(R%, cc%) * PopCohorts(R%, cc%)
                    Next cc%
                    SurvSEComp = SurvSEComp /
                AMAX(0.01, PopCohorts(R%, WVSCohorts))
                    SurvSEAdj(R%) = SURVSEI(R%, WVSCohorts) - SurvSEComp

                    TradSRatComp = 0
                    For cc% = 1 To WVSCohorts - 1
                        TradSRatComp = TradSRatComp +
                    TRADSRAT(R%, cc%) * PopCohorts(R%, cc%)
                    Next cc%
                    TradSRatComp = TradSRatComp /
                AMAX(0.01, PopCohorts(R%, WVSCohorts))
                    TradSRatAdj(R%) = TRADSRATI(R%, WVSCohorts) - TradSRatComp
                Next R%


            Else

                '   All but First 1:  Materialism/Postmaterialism Calculations
                '   --------------------------------------------------------
                Dim Title As String
                Dim MPRComp As Single
                Dim MPRComp2 As Single
                Dim MPRMul As Single
                Dim MPRTarget As Single
                Dim TimeFAc As Single
                Dim MPRBase As Single
                Dim NewCohortPop As Double
                Dim wvscomp As Single
                Dim wvscomp2 As Single
                Dim wvstarget As Single
                Dim wvsadd As Single
                Dim wvsbase As Single

                If wvsagesw <> 2 Then 'Use GDP/Cap to predict all cohorts
                    For R% = 1 To NR%
                        For cc% = 1 To WVSCohorts - 1
                            Title$ = "GDP/Capita (PPP) Versus Materialism/Postmaterialism Cohort" +
                                Convert.ToString(cc%)
                            If cc% = WVSCohorts Then
                                Title$ = "GDP/Capita (PPP) Versus Materialism/Postmaterialism Total"
                            End If
                            Call XYTABL(Title$, GDPPCP(R%), MPRComp, context)
                            Call XYTABL(Title$, GDPPCIP(R%), MPRComp2, context)
                            MPRMul = MPRComp / AMAX(0.01, MPRComp2)
                            MPRTarget = MPRComp2 + cultshmp(CInt(CULTREG(R%)))
                            TimeFAc = Amin(IY%, mprconv)
                            MPRBase = (MatPostRI(R%, cc%) * (mprconv - TimeFAc) +
                                MPRTarget * TimeFAc) / mprconv
                            MATPOSTR(R%, cc%) = AMAX(1, Amin(MPRBase * MPRMul + matpostradd(R%), 5))
                            '                MATPOSTR(r%, cc%) = AMAX(1, Amin(MPRBase * MPRMul, 5))
                        Next cc%
                    Next R%
                ElseIf wvsagesw = 2 Then 'Use GDP/Cap to predict first cohort; advance others
                    For R% = 1 To NR%
                        Title$ = "GDP/Capita (PPP) Versus Materialism/Postmaterialism Cohort 1"
                        Call XYTABL(Title$, GDPPCP(R%), MPRComp, context)
                        Call XYTABL(Title$, GDPPCIP(R%), MPRComp2, context)
                        MPRMul = MPRComp / AMAX(0.01, MPRComp2)
                        MPRTarget = MPRComp2 + cultshmp(CInt(CULTREG(R%)))
                        TimeFAc = Amin(IY%, mprconv)
                        MPRBase = (MatPostRI(R%, 1) * (mprconv - TimeFAc) + MPRTarget * TimeFAc) /
                            mprconv
                        MATPOSTR(R%, 1) = AMAX(1, Amin(MPRBase * MPRMul + matpostradd(R%), 5))
                        '            MATPOSTR(r%, 1) = AMAX(1, Amin(MPRBase * MPRMul, 5))
                        SMATPOSTR(R%, 1) = MATPOSTR(R%, 1)
                        For cc% = 2 To WVSCohorts - 1
                            NewCohortPop = PopCohorts(R%, cc%) - AdvCohorts(R%, cc%) +
                                AdvCohorts(R%, cc% - 1)
                            MATPOSTR(R%, cc%) = CSng(((PopCohorts(R%, cc%) -
                                AdvCohorts(R%, cc%)) * SMATPOSTR(R%, cc%) +
                                AdvCohorts(R%, cc% - 1) * SMATPOSTR(R%, cc% - 1)) / NewCohortPop)
                            SMATPOSTR(R%, cc%) = MATPOSTR(R%, cc%)
                        Next cc%
                    Next R%
                End If

                '   All but First 1:  Compute Total Population Value Using Cohort Sizes
                '   -----------------------------------------------------------------

                For R% = 1 To NR%
                    MATPOSTR(R%, WVSCohorts) = 0
                    For cc% = 1 To WVSCohorts - 1
                        MATPOSTR(R%, WVSCohorts) = CSng(MATPOSTR(R%, WVSCohorts) +
                            MATPOSTR(R%, cc%) * PopCohorts(R%, cc%))
                    Next cc%
                    MATPOSTR(R%, WVSCohorts) = CSng(MATPOSTR(R%, WVSCohorts) /
                        AMAX(0.01, PopCohorts(R%, WVSCohorts)) + MatPostRAdj(R%))
                Next R%


                '   All but First 1:  Survival/Self-Expression Calculations
                '   ------------------------------------------------

                If wvsagesw <> 2 Then 'Use GDP/Cap to predict all cohorts
                    For R% = 1 To NR%
                        For cc% = 1 To WVSCohorts - 1
                            'jrs 2025/05/08 Title$ = "GDP/Capita (PPP) Versus Survival/Self-Expression Cohort" +
                            'Convert.ToString(cc%)
                            Title$ = TFNameSurvSelfExpCohs(cc%)
                            If cc% = WVSCohorts Then
                                Title$ = "GDP/Capita (PPP) Versus Survival/Self-Expression Total"
                            End If
                            Call XYTABL(Title$, GDPPCP(R%), wvscomp, context)
                            Call XYTABL(Title$, GDPPCIP(R%), wvscomp2, context)
                            wvstarget = wvscomp2 + cultshse(CInt(CULTREG(R%)))
                            wvsadd = wvscomp - wvscomp2
                            TimeFAc = Amin(IY%, mprconv)
                            wvsbase = (SURVSEI(R%, cc%) * (mprconv - TimeFAc) +
                                wvstarget * TimeFAc) / mprconv
                            SURVSE(R%, cc%) = AMAX(-2, Amin((wvsbase + wvsadd + survseadd(R%)), 2))
                            '                SURVSE(r%, cc%) = AMAX(-2, Amin((wvsbase + wvsadd), 2))
                        Next cc%
                    Next R%
                ElseIf wvsagesw = 2 Then 'Use GDP/Cap to predict first cohort; advance others
                    For R% = 1 To NR%
                        'jrs 2025/05/08 added Log to name 
                        'Title$ = "GDP/Capita (PPP) Versus Survival/Self-Expression Cohort 1 Log"
                        'Call XYTABL(Title$, GDPPCP(R%), wvscomp, context)
                        'Call XYTABL(Title$, GDPPCIP(R%), wvscomp2, context)
                        Call XYTABL(TFNameSurvSelfExpCoh1, GDPPCP(R%), wvscomp, context)
                        Call XYTABL(TFNameSurvSelfExpCoh1, GDPPCIP(R%), wvscomp2, context)

                        wvstarget = wvscomp2 + cultshse(CInt(CULTREG(R%)))
                        wvsadd = wvscomp - wvscomp2
                        TimeFAc = Amin(IY%, mprconv)
                        wvsbase = (SURVSEI(R%, 1) * (mprconv - TimeFAc) + wvstarget * TimeFAc) /
                            mprconv
                        SURVSE(R%, 1) = AMAX(-2, Amin((wvsbase + wvsadd + survseadd(R%)), 2))
                        '            SURVSE(r%, 1) = AMAX(-2, Amin((wvsbase + wvsadd), 2))
                        SSURVSE(R%, 1) = SURVSE(R%, 1)
                        For cc% = 2 To WVSCohorts - 1
                            NewCohortPop = PopCohorts(R%, cc%) - AdvCohorts(R%, cc%) +
                                AdvCohorts(R%, cc% - 1)
                            SURVSE(R%, cc%) = CSng(((PopCohorts(R%, cc%) -
                                AdvCohorts(R%, cc%)) * SSURVSE(R%, cc%) +
                                AdvCohorts(R%, cc% - 1) * SSURVSE(R%, cc% - 1)) / NewCohortPop)
                            SSURVSE(R%, cc%) = SURVSE(R%, cc%)
                        Next cc%
                    Next R%
                End If

                '   All but First 1:  Compute Total Population Value for SurvSE Using Cohort Sizes
                '   ----------------------------------------------------------------------------

                For R% = 1 To NR%
                    SURVSE(R%, WVSCohorts) = 0
                    For cc% = 1 To WVSCohorts - 1
                        SURVSE(R%, WVSCohorts) = CSng(SURVSE(R%, WVSCohorts) +
                            SURVSE(R%, cc%) * PopCohorts(R%, cc%))
                    Next cc%
                    SURVSE(R%, WVSCohorts) = CSng(SURVSE(R%, WVSCohorts) /
                        AMAX(0.01, PopCohorts(R%, WVSCohorts)) + SurvSEAdj(R%))
                Next R%

                '   All but First 1:  Compute SurvSE Load for Region
                '   -----------------------------------------------------------------

                '    For r% = 1 To NR%
                '             predicted = -2.20983402782123 + 0.032043009 * CGDPPC(ICount%) + _
                '                0.03536954 * laborserper + 0.13332074 * higdpr - 0.401962222 * culexcomdum

                '    Next r%


                '   All but First 1:  Traditional/Secular-Rational Calculations
                '   ---------------------------------------------------------

                If wvsagesw <> 2 Then 'Use GDP/Cap to predict all cohorts
                    For R% = 1 To NR%
                        For cc% = 1 To WVSCohorts - 1
                            'jrs 2025/05/15 
                            'Title$ = "GDP/Capita (PPP) Versus Traditional/Secular-Rational Cohort" +
                            'Convert.ToString(cc%) 
                            Title$ = TFNameTradSecRatCohs(cc%)
                            If cc% = WVSCohorts Then
                                'jrs 2025/05/15 Title$ = "GDP/Capita (PPP) Versus Traditional/Secular-Rational Total"
                                Title$ = TFNameTradSecRatTotal
                            End If
                            Call XYTABL(Title$, GDPPCP(R%), wvscomp, context)
                            Call XYTABL(Title$, GDPPCIP(R%), wvscomp2, context)
                            wvstarget = wvscomp2 + cultshts(CInt(CULTREG(R%)))
                            wvsadd = wvscomp - wvscomp2
                            TimeFAc = Amin(IY%, mprconv)
                            wvsbase = (TRADSRATI(R%, cc%) * (mprconv - TimeFAc) +
                                wvstarget * TimeFAc) / mprconv
                            TRADSRAT(R%, cc%) = AMAX(-2, Amin((wvsbase + wvsadd + tradsratadd(R%)), 2))
                            '                TRADSRAT(r%, cc%) = AMAX(-2, Amin((wvsbase + wvsadd), 2))
                        Next cc%
                    Next R%
                ElseIf wvsagesw = 2 Then 'Use GDP/Cap to predict first cohort; advance others
                    For R% = 1 To NR%
                        'jrs 2025/05/15 
                        'Title$ = "GDP/Capita (PPP) Versus Traditional/Secular-Rational Cohort 1 Log"
                        Title$ = TFNameTradSecRatCoh1
                        Call XYTABL(Title$, GDPPCP(R%), wvscomp, context)
                        Call XYTABL(Title$, GDPPCIP(R%), wvscomp2, context)
                        wvstarget = wvscomp2 + cultshts(CInt(CULTREG(R%)))
                        wvsadd = wvscomp - wvscomp2
                        TimeFAc = Amin(IY%, mprconv)
                        wvsbase = (TRADSRATI(R%, 1) * (mprconv - TimeFAc) + wvstarget * TimeFAc) /
                            mprconv
                        TRADSRAT(R%, 1) = AMAX(-2, Amin((wvsbase + wvsadd + tradsratadd(R%)), 2))
                        '            TRADSRAT(r%, 1) = AMAX(-2, Amin((wvsbase + wvsadd), 2))
                        STradSRat(R%, 1) = TRADSRAT(R%, 1)
                        For cc% = 2 To WVSCohorts - 1
                            NewCohortPop = PopCohorts(R%, cc%) - AdvCohorts(R%, cc%) +
                                AdvCohorts(R%, cc% - 1)
                            TRADSRAT(R%, cc%) = CSng(((PopCohorts(R%, cc%) - AdvCohorts(R%, cc%)) _
                                * STradSRat(R%, cc%) +
                                AdvCohorts(R%, cc% - 1) * STradSRat(R%, cc% - 1)) / NewCohortPop)
                            STradSRat(R%, cc%) = TRADSRAT(R%, cc%)
                        Next cc%
                    Next R%
                End If

                '   All but First 1:  Compute Total Population Value Using Cohort Sizes
                '   -----------------------------------------------------------------

                For R% = 1 To NR%
                    TRADSRAT(R%, WVSCohorts) = 0
                    For cc% = 1 To WVSCohorts - 1
                        TRADSRAT(R%, WVSCohorts) = CSng(TRADSRAT(R%, WVSCohorts) +
                            TRADSRAT(R%, cc%) * PopCohorts(R%, cc%))
                    Next cc%
                    TRADSRAT(R%, WVSCohorts) = CSng(TRADSRAT(R%, WVSCohorts) /
                        AMAX(0.01, PopCohorts(R%, WVSCohorts)) + TradSRatAdj(R%))
                Next R%

            End If
        End Sub
        ''' <summary>
        ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' 
        '''    JAM 2019-08-15 Function used in Dompol Y1Call2
        ''' 
        ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' </summary>
        ''' <param name="R%"></param>
        ''' <returns></returns>


        'bbh 2011/06/18 begin
        '=============================SocioPolitical.BAS  =======================
        Function ContiguityIntlWarAdd(R%) As Single
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            '   This routine calculates the additive factor for intl war from contiguity
            '   and the conflicts of the neighbors
            ContiguityIntlWarAdd = 0
            For ICount% = 1 To NR%
                'jrs 2025/07/15 avoid including same country: and ICount% <> R%
                If contiguity(R%, ICount%) <= 3 And ICount% <> R% Then  '1= land contiguity; 2 = 0-12 miles; 3 = 12-24 miles
                    'jrs 2024/05/17
                    'If sfcivilwarucdpsw = 1 Then
                    'jrs 2024/01/18 If SFINTLWARALL(ICount%) >= 0.2 Then  'Enough risk next door to add to yours
                    'If IY > 1 Then 'dsb 2026/06/03 Fix for run to run issue
                    '    If SSFCIVILWARUCDP(ICount%) >= 0.1 Then  'Enough risk next door to add to yours
                    '        'jrs 2024/01/18 ContiguityIntlWarAdd = ContiguityIntlWarAdd + CSng(0.1)
                    '        ContiguityIntlWarAdd = ContiguityIntlWarAdd + AMAX(Amin(SSFCIVILWARUCDP(ICount%) - 0.1, 0.1), 0)
                    '    End If
                    'Else
                    '    If SFCIVILWARUCDP(ICount%) >= 0.1 Then  'Enough risk next door to add to yours
                    '        'jrs 2024/01/18 ContiguityIntlWarAdd = ContiguityIntlWarAdd + CSng(0.1)
                    '        ContiguityIntlWarAdd = ContiguityIntlWarAdd + AMAX(Amin(SFCIVILWARUCDP(ICount%) - 0.1, 0.1), 0)
                    '    End If
                    'End If
                    'Else
                    If IY > 1 Then 'dsb 2026/06/03 Fix for run to run issue
                            'jrs 2024/01/18 If SFINTLWARALL(ICount%) >= 0.2 Then  'Enough risk next door to add to yours
                            If SSFINTLWARALL(ICount%) >= 0.1 Then  'Enough risk next door to add to yours
                                'jrs 2024/01/18 ContiguityIntlWarAdd = ContiguityIntlWarAdd + CSng(0.1)
                                ContiguityIntlWarAdd = ContiguityIntlWarAdd + AMAX(Amin(SSFINTLWARALL(ICount%) - 0.1, 0.1), 0)
                            End If
                        Else
                            'jrs 2024/01/18 If SFINTLWARALL(ICount%) >= 0.2 Then  'Enough risk next door to add to yours
                            If SFINTLWARALL(ICount%) >= 0.1 Then  'Enough risk next door to add to yours
                                'jrs 2024/01/18 ContiguityIntlWarAdd = ContiguityIntlWarAdd + CSng(0.1)
                                ContiguityIntlWarAdd = ContiguityIntlWarAdd + AMAX(Amin(SFINTLWARALL(ICount%) - 0.1, 0.1), 0)
                            End If
                        End If
                        'End If
                    End If
            Next ICount%
            ContiguityIntlWarAdd = Amin(CSng(0.4), ContiguityIntlWarAdd)
            If ContiguityIntlWarAdd > 0 And GDPPCP(R%) > 10 Then 'phase down Contiguity impact as approach GDPPCP of $15K
                ContiguityIntlWarAdd = ContiguityIntlWarAdd * AMAX(0, (15 - GDPPCP(R%)) / 5)
            End If

            '+ + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + +
        End Function
        'bbh 2011/06/18 end

        'dsb 2026/08/03 *** Start ***This routine calculates the additive factor for intl war from contiguity and the conflicts of the neighbors based on the UCDP Civil War variable
        Function ContiguityCivilWarUCDPAdd(R%) As Single
            ContiguityCivilWarUCDPAdd = 0
            For ICount = 1 To NR%
                If contiguity(R%, ICount%) <= 3 And ICount% <> R% Then
                    If IY > 1 Then
                        If SSFCIVILWARUCDP(ICount%) >= 0.1 Then
                            ContiguityCivilWarUCDPAdd = ContiguityCivilWarUCDPAdd + AMAX(Amin(SSFCIVILWARUCDP(ICount%) - 0.1, 0.1), 0)
                        End If
                    Else
                        If SFCIVILWARUCDP(ICount%) >= 0.1 Then
                            ContiguityCivilWarUCDPAdd = ContiguityCivilWarUCDPAdd + AMAX(Amin(SFCIVILWARUCDP(ICount%) - 0.1, 0.1), 0)
                        End If
                    End If
                End If
            Next ICount%
            ContiguityCivilWarUCDPAdd = Amin(CSng(0.4), ContiguityCivilWarUCDPAdd)
            If ContiguityCivilWarUCDPAdd > 0 And GDPPCP(R%) > 10 Then
                ContiguityCivilWarUCDPAdd = ContiguityCivilWarUCDPAdd * AMAX(0, (15 - GDPPCP(R%)) / 5)
            End If
        End Function
        'dsb 2026/08/03 *** End ***
        'dsb 2026/1/8 *** Start ***
        Function ContiguityCivilWarPardeeAdd(R%) As Single
            '   This routine calculates the additive factor for intl war from contiguity and the conflicts of the neighbors based on the Pardee Civil War variable
            ContiguityCivilWarPardeeAdd = 0
            For ICount = 1 To NR%
                If contiguity(R%, ICount%) <= 3 And ICount% <> R% Then
                    If IY > 1 Then 'dsb 2026/06/03 Fix for run to run issue
                        If SSFCIVILWARPARDEE(ICount%) >= 0.1 Then
                            ContiguityCivilWarPardeeAdd = ContiguityCivilWarPardeeAdd + AMAX(Amin(SSFCIVILWARPARDEE(ICount%) - 0.1, 0.1), 0)
                        End If
                    Else
                        If SFCIVILWARPARDEE(ICount%) >= 0.1 Then
                            ContiguityCivilWarPardeeAdd = ContiguityCivilWarPardeeAdd + AMAX(Amin(SFCIVILWARPARDEE(ICount%) - 0.1, 0.1), 0)
                        End If
                    End If
                End If
            Next ICount%
            ContiguityCivilWarPardeeAdd = Amin(CSng(0.4), ContiguityCivilWarPardeeAdd)
            If ContiguityCivilWarPardeeAdd > 0 And GDPPCP(R%) > 10 Then
                ContiguityCivilWarPardeeAdd = ContiguityCivilWarPardeeAdd * AMAX(0, (15 - GDPPCP(R%)) / 5)
            End If
        End Function
        'dsb 2026/1/8 *** End ***


        'jrs 2016/01/18
        Private Sub LoadColonyData(ByRef UKcol() As Single, ByRef FRAcol() As Single, ByRef GERcol() As Single, ByRef PORcol() As Single,
                    ByRef SPAcol() As Single, ByRef NETcol() As Single, ByRef RUScol() As Single)

            Using db As IFs2Context = New IFs2Context()

                For Each item In db.RegionWasColonyOf

                    UKcol(item.RegionId) = CSng(item.ukcol)
                    FRAcol(item.RegionId) = CSng(item.fracol)
                    GERcol(item.RegionId) = CSng(item.gercol)
                    PORcol(item.RegionId) = CSng(item.porcol)
                    SPAcol(item.RegionId) = CSng(item.spacol)
                    NETcol(item.RegionId) = CSng(item.netcol)
                    RUScol(item.RegionId) = CSng(item.ruscol)
                Next

            End Using


            'Dim rsRegCol As recordset
            'Set rsRegCol = dbIFs.OpenRecordset("RegionWasColonyOf")
            'rsRegCol.MoveFirst
            'Do While Not rsRegCol.EOF
            '    UKcol(rsRegCol.Fields("RegionId")) = rsRegCol.Fields("UKCol").Value
            '    FRAcol(rsRegCol.Fields("RegionId")) = rsRegCol.Fields("FRACol").Value
            '    GERcol(rsRegCol.Fields("RegionId")) = rsRegCol.Fields("GERCol").Value
            '    PORcol(rsRegCol.Fields("RegionId")) = rsRegCol.Fields("PORCol").Value
            '    SPAcol(rsRegCol.Fields("RegionId")) = rsRegCol.Fields("SPACol").Value
            '    NETcol(rsRegCol.Fields("RegionId")) = rsRegCol.Fields("NETCol").Value
            '    RUScol(rsRegCol.Fields("RegionId")) = rsRegCol.Fields("RUSCol").Value
            '    rsRegCol.MoveNext
            'Loop
            'rsRegCol.Close



        End Sub


        '=============================SocioPolitical.BAS  =======================
        Private Sub PowerCalculation(context As IForecastContext)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'jrs 2011/10/11
            'clp 2016/08/24 - Adding power_EnergyProd
            'gvdb 2024/04/05
            'unused variable
            Dim power_GovNucWP As Single, power_EnergyNetExport As Single, power_TradeTotal, power_FDIInflows As Single ' power_ConvMilSpend As Single,power_EnergyProd As Single,
            Dim power_AID As Single, power_HumCapWkPopHDI As Single, power_GovRev As Single, power_InnovationRD As Single, power_ConnectivityICTProd As Single

            'dkb 2014/07/07 - START
            'gvdb 2024/04/05
            'unused variable
            Dim power_embassy As Single, power_treaty As Single, power_igo As Single, power_dpi As Single ', power_efectivehumancap As Single
            Dim powerHHMI_GovNucWP As Single, powerHHMI_pop As Single, powerHHMI_GDPPPP As Single, powerHHMI_techp As Single ', powerHHMI_milPPP As Single
            Dim powerHHMI_embassy As Single, powerHHMI_igo As Single, powerHHMI_treaty As Single
            'dkb 2014/07/07 - END
            'dkb 2014/09/24 - Adding POWERGPI components - START
            'clp 2016/08/24 - adding powerGPI_EnergyProd
            'gvdb 2024/04/05
            'unused variable
            Dim powerGPI_GDPPPP As Single, powerGPI_mil As Single, powerGPI_GovNucWP As Single, powerGPI_EnergyNetExport As Single, powerGPI_TradeTotal As Single ' powerGPI_EnergyProd As Single,
            Dim powerGPI_FDIInflows As Single, powerGPI_AID As Single, powerGPI_InnovationRD As Single, powerGPI_ConnectivityICTProd As Single, powerGPI_dpi As Single
            Dim powerGPI_effectivehumancap As Single
            Dim PBASE As Single

            Dim power_pop As Single
            Dim power_GDPPPP As Single
            Dim power_GDP As Single
            Dim power_techp As Single
            Dim POWER_TECH As Single
            Dim POWER_G As Single
            'gvdb 2024/04/05
            'unused variable
            'Dim power_milPPP As Single
            Dim power_mil As Single
            'jrs 2020/11/19
            Dim power_gdp_gdppcp As Single

            'clp 2016/08/26 - Adding power_RandD
            Dim power_RandD As Single
            Dim POWER_Conv As Single
            Dim POWER_Nuc As Single
            Dim EnTot As Single
            Dim E As Integer
            Dim power_EnProd As Single
            Dim power_effectivehumancap As Single
            Dim powerHHMI_mil As Single
            'dkb 2014/09/24 - Adding POWERGPI components - END

            'dkb 2014/07/07 Looped to 24 instead of 18 to account for added dimensions
            'jrs 2021/08/18 For ICount% = 1 To 24 'jrs 2011/10/11 GPI
            For ICount% = 1 To context.GetDimension(15).Buckets.Count 'jrs 2011/10/11 GPI                
                PBASE = PBASE + wpwghtpow(ICount%)
            Next ICount%
            'jrs 2011/10/13
            PBASE = PBASE - wpwghtpow(11)
            'jrs 2017/07/07
            If IY% = 1 Then
                ReDim SPOWERGPI(NR)
                'jrs 2022/04/20
                ReDim SPOWER(NR)
            End If

            'jrs 2021/08/18
            Dim power_GDPPC, power_GDPPCP As Single
            Dim GDPPCMAX, GDPPCPMAX As Single
            GDPPCMAX = 0
            GDPPCPMAX = 0
            For R% = 1 To NR%
                If GDPPC(R%) > GDPPCMAX Then
                    GDPPCMAX = GDPPC(R%)
                End If
                If GDPPCP(R%) > GDPPCPMAX Then
                    GDPPCPMAX = GDPPCP(R%)
                End If
            Next R%

            ' clp 2025/12/12 ***START*** Add Better Beckley
            Dim beckleytot As Single
            beckleytot = 0
            For R% = 1 To NR%
                beckleytot = beckleytot + (GDP(R%) * GDPPCP(R%))
            Next R%
            ' clp 2025/12/12 ***END*** Add Better Beckley

            'bbh 2011/11/19 Changes throughout R% Loop
            For R% = 1 To NR%
                'dkb 2014/09/24 - Added new POWERGPI variable and corresponding (hardcoded) components. _
                'Restructured And cleaned the following block to allow for reuse of base POWER components _
                'accross 3 POWER measures - START
                power_pop = POP(R%) / AMAX(0.001, WPOP)
                power_GDPPPP = GDPP(R%) / AMAX(0.001, WGDPPPP)
                power_GDP = GDP(R%) / AMAX(0.001, WGDP)
                power_techp = GDPP(R%) * GDPPCP(R%) / AMAX(0.001, WTECHP)
                POWER_TECH = GDP(R%) * GDPPC(R%) / AMAX(0.001, WTECH)
                POWER_G = GOVCON(R%) / AMAX(0.001, WGOV)
                'jrs 2020/11/19 power_milPPP = SGDSMil(R%) * GDPP(R%) / GDP(R%) / AMAX(0.001, WGDSMIL)
                power_gdp_gdppcp = GDP(R%) * GDPPCP(R%) / AMAX(0.001, WTECHmix)
                power_mil = SGDSMil(R%) / AMAX(0.001, WGDSMIL)
                'clp 2016/08/26 - Adding power_RandD
                power_RandD = (SGDSRandD(R%) / AMAX(0.001, WGDSRandD))
                POWER_Conv = SCPOW(R%) / AMAX(0.001, SWCPOW)
                POWER_Nuc = SNPOW(R%) / AMAX(0.001, SWNPOW)
                If SGOVNUCWP(R%) > 0 Then
                    power_GovNucWP = CSng(Log(SGOVNUCWP(R%)) / AMAX(0.001, WGOVNUCWP))
                Else
                    power_GovNucWP = 0
                End If
                'jrs 2021/08/18
                power_GDPPC = GDPPC(R%) / AMAX(0.001, GDPPCMAX)
                power_GDPPCP = GDPPCP(R%) / AMAX(0.001, GDPPCPMAX)

                '       If IY% = 1 Then
                '            power_EnergyNetExport = ((XS(R%, Ener) / WEPBYEAR) / AMAX(0.001, WENX)) - ((MS(R%, Ener) / WEPBYEAR) / _
                '        AMAX(0.001, WENM))
                '       Else
                '            power_EnergyNetExport = (SENX(R%) / AMAX(0.001, WENX) - SENM(R%) / AMAX(0.001, WENM))
                '       End If

                'dkb 2014/10/06 - As per Tim Smith's request, power_EnergyNetExport is not allowed to go below 0 - START
                'dkb 2014/10/23 - new EnergyNetExport specification - START
                'clp 2016/08/25 - adding energy production variable
                If IY% = 1 Then
                    '    power_EnergyNetExport = AMAX(0, ((XS(R%, Ener) / WEPBYEAR) / AMAX(0.001, WENX)) - ((MS(R%, Ener) / WEPBYEAR) / _
                    'AMAX(0.001, WENM)))
                    power_EnergyNetExport = AMAX(0, (XS(R%, Ener) / WEPBYEAR) - ((MS(R%, Ener) / WEPBYEAR))) / AMAX(0.001, WENXM)

                    EnTot = 0
                    For E = 1 To Renew 'Oil, gas, coal, hydro, nuclear, renewable
                        If E <= Hydro Then
                            EnTot = EnTot + ENP(R%, E)
                            'bbh 2011/10/16
                            'Else
                            '   EnTot = EnTot + ENP(R%, e%) '* 0.5
                        End If
                    Next E

                    power_EnProd = EnTot / WENProd

                Else
                    '   power_EnergyNetExport = AMAX(0, (SENX(R%) / AMAX(0.001, WENX) - SENM(R%) / AMAX(0.001, WENM)))
                    power_EnergyNetExport = AMAX(0, SENX(R%) - SENM(R%)) / AMAX(0.001, WENXM)
                    power_EnProd = SEnTotal(R%) / WENProd

                End If
                'dkb 2014/10/23 - new EnergyNetExport specification - END
                'dkb 2014/10/06 - As per Tim Smith's request, power_EnergyNetExport is not allowed to go below 0 - END

                '              If IY% = 1 Then
                '            power_EnergyNetExport = (XS(R%, Ener) / WEPBYEAR) / AMAX(0.001, WENX)
                '       Else
                '            power_EnergyNetExport = SENX(R%) / AMAX(0.001, WENX)
                '       End If

                power_TradeTotal = (X(R%) + M(R%)) / AMAX(0.001, WTradeTotal)
                power_FDIInflows = XFDIFIN(R%) / AMAX(0.001, WXFDIFIN)

                If AID(R%) < 0 Then 'AID power of that of donors (net aid receipts<0) not recipients (aid>0)
                    power_AID = 0 - AID(R%) / AMAX(0.001, WAID)
                Else
                    power_AID = 0
                End If

                power_HumCapWkPopHDI = POPWORKING(R%) * SHDINEW(R%) / AMAX(0.001, WHumCapWkPopHDI)
                power_GovRev = GOVREV(R%) / AMAX(0.001, WGOVREV)
                power_InnovationRD = SRANDDEXP(R%) * GDP(R%) / AMAX(0.001, WInnovationRD)
                power_ConnectivityICTProd = KS(R%, 6) / AMAX(0.001, WConnectivityICTProd)

                'dkb 2014/10/27 - Not using save variables for first year - START
                'clp 2016/08/23 - removing unstandardizing; now using absolute values
                If IY% = 1 Then
                    'power_embassy = ((DIPLOEMBASSY(R%) * 32.48639) + 39.42253) / AMAX(0.001, WDPIBN)
                    'power_igo = ((DIPLOIGO(R%) * 129.6466) + 244.7328) / AMAX(0.001, WDPIMN)
                    'power_treaty = ((DIPLOTREATY(R%) * 747.6514) + 906.7255) / AMAX(0.001, WDPIILN)
                    'power_dpi = ((((0.5 * ((DIPLOEMBASSY(R%) * 32.48639) + 39.42253) / ((DIPLOEMBASSYMAX * 32.48639) + 39.42253))) + _
                    '(0.33 * ((DIPLOIGO(R%) * 129.6466) + 244.7328) / ((DIPLOIGOMAX * 129.6466) + 244.7328)) + _
                    '(0.17 * (((DIPLOTREATY(R%) * 747.6514) + 906.7255) / ((DIPLOTREATYMAX * 747.6514) + 906.7255)))) * GOVREV(R%)) / _
                    'AMAX(0.001, WDPI)

                    power_embassy = DIPLOEMBASSY(R%) / AMAX(0.001, WDPIBN)
                    power_igo = DIPLOIGO(R%) / AMAX(0.001, WDPIMN)
                    power_treaty = DIPLOTREATY(R%) / AMAX(0.001, WDPIILN)
                    'power_dpi = ((((0.5 * DIPLOEMBASSY(R%) / DIPLOEMBASSYMAX)) + _
                    '(0.33 * DIPLOIGO(R%) / DIPLOIGOMAX) + _
                    '(0.17 * (DIPLOTREATY(R%) / DIPLOTREATYMAX))) * GOVREV(R%)) / _
                    'AMAX(0.001, WDPI)
                    'clp 2016/08/24 - updating dpi calculation to match Tim's new formulation
                    power_dpi = CSng(((((99.83 * (DIPLOEMBASSY(R%) / DIPLOEMBASSYMAX) ^ 2)) +
                    (35.49 * (DIPLOIGO(R%) / DIPLOIGOMAX) ^ 2) +
                    (7.34 * ((DIPLOTREATY(R%) / DIPLOTREATYMAX) ^ 1.5))) * GOVREV(R%)) /
                    AMAX(0.001, WDPI))
                    'power_dpi = (((0.5 * (DIPLOEMBASSY(R%) / DIPLOEMBASSYMAX)) + (0.33 * (DIPLOIGO(R%) / DIPLOIGOMAX)) + _
                    ' (0.17 * (DIPLOTREATY(R%) / DIPLOTREATYMAX))) * GOVREV(R%)) / AMAX(0.001, WDPI)

                Else
                    'clp 2016/08/23 - removing unstandardizing; now using absolute values
                    'power_embassy = ((SDIPLOEMBASSY(R%) * 32.48639) + 39.42253) / AMAX(0.001, WDPIBN)
                    'power_igo = ((SDIPLOIGO(R%) * 129.6466) + 244.7328) / AMAX(0.001, WDPIMN)
                    'power_treaty = ((SDIPLOTREATY(R%) * 747.6514) + 906.7255) / AMAX(0.001, WDPIILN)
                    'power_dpi = ((((0.5 * ((SDIPLOEMBASSY(R%) * 32.48639) + 39.42253) / ((DIPLOEMBASSYMAX * 32.48639) + 39.42253))) + _
                    '   (0.33 * ((SDIPLOIGO(R%) * 129.6466) + 244.7328) / ((DIPLOIGOMAX * 129.6466) + 244.7328)) + _
                    '  (0.17 * (((SDIPLOTREATY(R%) * 747.6514) + 906.7255) / ((DIPLOTREATYMAX * 747.6514) + 906.7255)))) * GOVREV(R%)) / _
                    ' AMAX(0.001, WDPI)
                    power_embassy = SDIPLOEMBASSY(R%) / AMAX(0.001, WDPIBN)
                    power_igo = SDIPLOIGO(R%) / AMAX(0.001, WDPIMN)
                    power_treaty = SDIPLOTREATY(R%) / AMAX(0.001, WDPIILN)
                    'clp 2016/08/24 - updating dpi calculation to match Tim's new formulation
                    'power_dpi = ((((0.5 * SDIPLOEMBASSY(R%) / DIPLOEMBASSYMAX)) + _
                    '(0.33 * SDIPLOIGO(R%) / DIPLOIGOMAX) + _
                    '(0.17 * (SDIPLOTREATY(R%) / DIPLOTREATYMAX))) * GOVREV(R%)) / _
                    'AMAX(0.001, WDPI)
                    power_dpi = CSng(((((99.83 * (SDIPLOEMBASSY(R%) / DIPLOEMBASSYMAX) ^ 2)) +
                                (35.49 * (SDIPLOIGO(R%) / DIPLOIGOMAX) ^ 2) +
                                (7.34 * ((SDIPLOTREATY(R%) / DIPLOTREATYMAX) ^ 1.5))) * GOVREV(R%)) /
                                AMAX(0.001, WDPI))

                    ''power_dpi = (((0.5 * (SDIPLOEMBASSY(R%) / DIPLOEMBASSYMAX)) + (0.33 * (SDIPLOIGO(R%) / DIPLOIGOMAX)) + _
                    '(0.17 * (SDIPLOTREATY(R%) / DIPLOTREATYMAX))) * GOVREV(R%)) / AMAX(0.001, WDPI)
                End If
                'dkb 2014/10/27 - Not using save variables for first year - START
                'clp 2016/08/23 - Update to match new calculations from Tim/Alanna
                'power_effectivehumancap = ((POP15TO65(R%) - (SINCOMELT1LN(R%) * (POP15TO65(R%) / POP(R%))) - _
                ' (((SINCOMELT2LN(R%) - SINCOMELT1LN(R%)) / 2) * (POP15TO65(R%) / POP(R%)))) * SHDINEW(R%)) / AMAX(0.001, WEHC)

                'power_effectivehumancap = ((POP15TO65(R%) - (SINCOMELT190LN(R%) * (POP15TO65(R%) / POP(R%))) - _
                '(((SINCOMELT310LN(R%) - SINCOMELT190LN(R%)) / 2) * (POP15TO65(R%) / POP(R%)))) * SHDINEW(R%)) / AMAX(0.001, WEHC)
                'jrs 2021/09/02 changed from 3.10 to 3.20/day
                'power_effectivehumancap = (((POP(R%) - SINCOMELT310LN(R%)) * (POP15TO65(R%) / POP(R%))) * SHDINEW(R%) * SHDINEW(R%)) / AMAX(0.001, WEHC)
                'jrs 2021/11/21
                If ConstantDollarsYear = 2011 Then
                    power_effectivehumancap = (((POP(R%) - SINCOMELT320LN(R%)) * (POP15TO65(R%) / POP(R%))) * SHDINEW(R%) * SHDINEW(R%)) / AMAX(0.001, WEHC)
                ElseIf ConstantDollarsYear = 2017 Then 'jrs 2025/09/12
                    power_effectivehumancap = (((POP(R%) - SINCOMELT365LN(R%)) * (POP15TO65(R%) / POP(R%))) * SHDINEW(R%) * SHDINEW(R%)) / AMAX(0.001, WEHC)
                Else 'jrs 2025/09/12
                    power_effectivehumancap = (((POP(R%) - SINCOMELT420LN(R%)) * (POP15TO65(R%) / POP(R%))) * SHDINEW(R%) * SHDINEW(R%)) / AMAX(0.001, WEHC)
                End If

                'If RegName(R%) = "Seychelles" Then Stop

                'dkb 2014/09/24 - Hardcoding POWERGPI components - START
                'clp 2016/08/23 - Updated GPI weights
                'powerGPI_GDPPPP = power_GDPPPP * 0.2
                powerGPI_GDPPPP = CSng(power_GDP * 0.2)
                powerGPI_mil = CSng(power_mil * 0.2)
                powerGPI_GovNucWP = CSng(power_GovNucWP * 0.05)
                'clp 2016/08/25 - changed Energy Exports to production
                'powerGPI_EnergyNetExport = power_EnergyNetExport * 0.05
                powerGPI_EnergyNetExport = CSng(power_EnProd * 0.05)
                powerGPI_TradeTotal = CSng(power_TradeTotal * 0.05)
                powerGPI_FDIInflows = CSng(power_FDIInflows * 0.05)
                powerGPI_AID = CSng(power_AID * 0.05)
                'clp 2016/08/26 - New R&D calculation for GPI
                'powerGPI_InnovationRD = power_InnovationRD * 0.05
                powerGPI_InnovationRD = CSng(power_RandD * 0.05)
                powerGPI_ConnectivityICTProd = CSng(power_ConnectivityICTProd * 0.05)
                powerGPI_dpi = CSng(power_dpi * 0.2)
                powerGPI_effectivehumancap = CSng(power_effectivehumancap * 0.05)
                'dkb 2014/09/24 - Hardcoding POWERGPI components - END

                'If RegName(R%) = "United States of America" Then Stop

                'dkb 2014/09/24 - Hardcoding POWERHHMI components - START
                powerHHMI_pop = CSng(power_pop * 0.8)
                powerHHMI_GDPPPP = CSng(power_GDPPPP * 1.1)
                powerHHMI_techp = CSng(power_techp * 0.3)
                powerHHMI_mil = CSng(power_mil * 0.9)
                powerHHMI_GovNucWP = CSng(power_GovNucWP * 0.25)
                powerHHMI_embassy = CSng(power_embassy * 0.3)
                powerHHMI_igo = CSng(power_igo * 0.2)
                powerHHMI_treaty = CSng(power_treaty * 0.1)
                'dkb 2014/09/24 - Hardcoding POWERHHMI components - END

                'jrs 2021/04/09 (item 19 + 7 + 5 + 12 + 10)
                POWER(R%) = CSng(0.2 * power_embassy + 0.2 * power_mil + 0.2 * POWER_TECH + 0.2 * power_TradeTotal + 0.2 * power_GovNucWP)
                'dkb 2014/09/24 - Adding wpwghtpow parameters for POWER components - START
                power_pop = power_pop * wpwghtpow(1)
                power_GDPPPP = power_GDPPPP * wpwghtpow(2)
                power_GDP = power_GDP * wpwghtpow(3)
                power_techp = power_techp * wpwghtpow(4)
                POWER_TECH = POWER_TECH * wpwghtpow(5)
                POWER_G = POWER_G * wpwghtpow(6)
                'jrs 2020/11/19 power_milPPP = power_milPPP * wpwghtpow(23)
                power_gdp_gdppcp = power_gdp_gdppcp * wpwghtpow(23)
                power_mil = power_mil * wpwghtpow(7)
                POWER_Conv = POWER_Conv * wpwghtpow(8)
                POWER_Nuc = POWER_Nuc * wpwghtpow(9)

                power_GovNucWP = power_GovNucWP * wpwghtpow(10)
                power_EnergyNetExport = power_EnergyNetExport * wpwghtpow(11)
                power_TradeTotal = power_TradeTotal * wpwghtpow(12)
                power_FDIInflows = power_FDIInflows * wpwghtpow(13)
                power_AID = power_AID * wpwghtpow(14)
                power_HumCapWkPopHDI = power_HumCapWkPopHDI * wpwghtpow(15)
                power_GovRev = power_GovRev * wpwghtpow(16)
                power_InnovationRD = power_InnovationRD * wpwghtpow(17)
                power_ConnectivityICTProd = power_ConnectivityICTProd * wpwghtpow(18)

                power_embassy = power_embassy * wpwghtpow(19)
                power_igo = power_igo * wpwghtpow(20)
                power_treaty = power_treaty * wpwghtpow(21)
                power_dpi = power_dpi * wpwghtpow(22)

                power_effectivehumancap = power_effectivehumancap * wpwghtpow(24)
                'dkb 2014/09/24 - Adding wpwghtpow parameters for POWER components - END

                'jrs 2021/08/18
                power_GDPPC = power_GDPPC * wpwghtpow(25)
                power_GDPPCP = power_GDPPCP * wpwghtpow(26)

                POWERHHMI(R%) = powerHHMI_pop + powerHHMI_GDPPPP + powerHHMI_techp + powerHHMI_mil +
                powerHHMI_embassy + powerHHMI_GovNucWP + powerHHMI_igo + powerHHMI_treaty

                POWERGPI(R%) = powerGPI_GDPPPP + powerGPI_mil + powerGPI_GovNucWP + powerGPI_EnergyNetExport +
                powerGPI_TradeTotal + powerGPI_FDIInflows + powerGPI_AID + powerGPI_InnovationRD +
                powerGPI_ConnectivityICTProd + powerGPI_dpi + powerGPI_effectivehumancap

                'If RegName(R%) = "United States of America" Then Stop

                'jrs 2021/04/09 renamed from POWER TO POWERHH
                POWERHH(R%) = power_pop + power_GDPPPP + power_techp + power_mil
                POWERHH(R%) = POWERHH(R%) + power_GDP + POWER_TECH + POWER_G + POWER_Conv + POWER_Nuc +
                power_GovRev + power_GovNucWP + power_EnergyNetExport + power_TradeTotal +
                power_FDIInflows + power_AID + power_HumCapWkPopHDI + power_ConnectivityICTProd +
                power_effectivehumancap + power_InnovationRD + power_gdp_gdppcp 'jrs 2020/11/19 removed power_milPPP and added power_gdp_gdppcp
                POWERHH(R%) = POWERHH(R%) + power_dpi + power_embassy + power_igo + power_treaty
                'jrs 2021/08/18
                POWERHH(R%) = POWERHH(R%) + power_GDPPC + power_GDPPCP

                'If RegName(R%) = "United States of America" Then Stop

                POWERHHMI(R%) = CSng(AMAX(-0.1, POWERHHMI(R%) / AMAX(0.001, 3.95) * 100))
                'POWERGPI(R%) = AMAX(-0.1, POWERGPI(R%) / AMAX(0.001, 1 - 0.05) * 100)
                POWERGPI(R%) = AMAX(-0.1, POWERGPI(R%) / AMAX(0.001, 1) * 100)
                'JRS 2021/04/09
                POWERHH(R%) = AMAX(-0.1, POWERHH(R%) / AMAX(0.001, PBASE) * 100)
                'jrs 2021/04/26
                POWER(R%) = POWER(R%) * 100

                POWERHHMI(R%) = AMAX(0.000001, Amin(90, POWERHHMI(R%)))
                POWERGPI(R%) = AMAX(0.000001, Amin(90, POWERGPI(R%)))
                'JRS 2021/04/09
                POWERHH(R%) = AMAX(0.000001, Amin(90, POWERHH(R%)))

                ' clp 2025/12/12 ***START*** Add Better Beckley
                POWERBECKLEY(R%) = (GDP(R%) * GDPPCP(R%)) / beckleytot * 100
                POWERBECKLEY(R%) = AMAX(0.000001, Amin(90, POWERBECKLEY(R%)))
                ' clp 2025/12/12 ***END*** Add Better Beckley

                'jrs 2017/07/07
                SPOWERGPI(R%) = POWERGPI(R%)
                'jrs 2022/04/20
                SPOWER(R%) = POWER(R%)



                'If RegName(R%) = "United States of America" Then Stop

                'dkb 2014/09/24 - END


                'dkb 2014/09/24 - The following commented-out block can be found in above code - START
                ''========================================='
                ''=====  BEGIN POWER COMPONENT BLOCK  ====='
                ''========================================='
                '       power_pop = POP(R%) / AMAX(0.001, WPOP) * wpwghtpow(1)
                '       power_GDPPPP = GDPP(R%) / AMAX(0.001, WGDPPPP) * wpwghtpow(2)
                '       POWER_GDP = GDP(R%) / AMAX(0.001, WGDP) * wpwghtpow(3)
                '       power_techp = GDPP(R%) * GDPPCP(R%) / AMAX(0.001, WTECHP) * wpwghtpow(4)
                '       POWER_TECH = GDP(R%) * GDPPC(R%) / AMAX(0.001, WTECH) * wpwghtpow(5)
                '       POWER_G = GOVCON(R%) / AMAX(0.001, WGOV) * wpwghtpow(6)
                '       'power_mil = SGDSMil(R%) * GDPP(R%) / GDP(R%) / AMAX(0.001, WGDSMIL) * wpwghtpow(7)
                '       'dkb 2014/07/07 - Adding military spending at PPP - START
                '       power_milPPP = SGDSMil(R%) * GDPP(R%) / GDP(R%) / AMAX(0.001, WGDSMIL) * wpwghtpow(23)
                '       'dkb 2014/07/07 - END
                '       power_mil = SGDSMil(R%) / AMAX(0.001, WGDSMIL) * wpwghtpow(7)
                ''If RegName(R%) = "United States of America" Then Stop
                '       POWER_Conv = SCPOW(R%) / AMAX(0.001, SWCPOW) * wpwghtpow(8)
                '       POWER_Nuc = SNPOW(R%) / AMAX(0.001, SWNPOW) * wpwghtpow(9)
                '       'jrs 2011/10/11
                '       'power_GovNucWP = SGOVNUCWP(R%) / AMAX(0.001, WGOVNUCWP) * wpwghtpow(10)
                '       If SGOVNUCWP(R%) > 0 Then
                '            power_GovNucWP = Log(SGOVNUCWP(R%)) / AMAX(0.001, WGOVNUCWP) * wpwghtpow(10)
                '       Else
                '            power_GovNucWP = 0
                '       End If
                '
                '       'dkb 2014/08/20 - adding save variables to power component, using XS and MS because ENX and ENM are computed after this point - START
                '       If IY% = 1 Then
                '            power_EnergyNetExport = (((XS(R%, Ener) / WEPBYEAR) / AMAX(0.001, WENX)) - ((MS(R%, Ener) / WEPBYEAR) / AMAX(0.001, WENM))) * wpwghtpow(11)
                '       Else
                '            power_EnergyNetExport = (SENX(R%) / AMAX(0.001, WENX) - SENM(R%) / AMAX(0.001, WENM)) * wpwghtpow(11)
                '       End If
                '       'power_EnergyNetExport = (SENX(R%) / AMAX(0.001, WENX) - SENM(R%) / AMAX(0.001, WENM)) * wpwghtpow(11)
                '       'dkb 2014/08/20 - END
                '
                '       power_TradeTotal = (X(R%) + M(R%)) / AMAX(0.001, WTradeTotal) * wpwghtpow(12)
                '       power_FDIInflows = XFDIFIN(R%) / AMAX(0.001, WXFDIFIN) * wpwghtpow(13)
                '
                ''        If AID(R%) > 0 Then
                ''            power_AID = AID(R%) / AMAX(0.001, WAID) * wpwghtpow(14)
                ''        Else
                ''            power_AID = 0
                ''        End If
                '        If AID(R%) < 0 Then 'AID power of that of donors (net aid receipts<0) not recipients (aid>0)
                '            power_AID = 0 - AID(R%) / AMAX(0.001, WAID) * wpwghtpow(14)
                '        Else
                '            power_AID = 0
                '        End If
                '       'jrs 2013/04/12 power_HumCapWkPopHDI = POP15TO65(R%) * SHDINEW(R%) / AMAX(0.001, WHumCapWkPopHDI) * wpwghtpow(15)
                '       power_HumCapWkPopHDI = POPWORKING(R%) * SHDINEW(R%) / AMAX(0.001, WHumCapWkPopHDI) * wpwghtpow(15)
                '       power_GovRev = GOVREV(R%) / AMAX(0.001, WGOVREV) * wpwghtpow(16)
                '       'power_InnovationRD = SRANDDEXP(R%) * GDPP(R%) / AMAX(0.001, WInnovationRD) * wpwghtpow(17)
                '       power_InnovationRD = SRANDDEXP(R%) * GDP(R%) / AMAX(0.001, WInnovationRD) * wpwghtpow(17)
                '       power_ConnectivityICTProd = KS(R%, 6) / AMAX(0.001, WConnectivityICTProd) * wpwghtpow(18)
                '
                '       'dkb 2014/07/07 unstandardizing diplo components for POWER - START
                '       'dkb 2013/09/09 creating components for world DPI - START
                '       'power_embassy = DIPLOEMBASSY(R%) / AMAX(0.001, WDPIBN) * wpwghtpow(19)
                '       'power_igo = DIPLOIGO(R%) / AMAX(0.001, WDPIMN) * wpwghtpow(20)
                '       'power_treaty = DIPLOTREATY(R%) / AMAX(0.001, WDPIILN) * wpwghtpow(21)
                '       'power_dpi = ((0.5 * (DIPLOEMBASSY(R%) / DIPLOEMBASSYMAX)) + (0.33 * (DIPLOIGO(R%) / DIPLOIGOMAX)) + _
                '            (0.17 * (DIPLOTREATY(R%) / DIPLOTREATYMAX))) / AMAX(0.001, WDPI) * wpwghtpow(22)
                '        'dkb 2013/09/09 - END
                '       power_embassy = ((SDIPLOEMBASSY(R%) * 32.48639) + 39.42253) / AMAX(0.001, WDPIBN) * wpwghtpow(19)
                '       power_igo = ((SDIPLOIGO(R%) * 129.6466) + 244.7328) / AMAX(0.001, WDPIMN) * wpwghtpow(20)
                '       power_treaty = ((SDIPLOTREATY(R%) * 747.6514) + 906.7255) / AMAX(0.001, WDPIILN) * wpwghtpow(21)
                '       power_dpi = ((((0.5 * ((SDIPLOEMBASSY(R%) * 32.48639) + 39.42253) / ((DIPLOEMBASSYMAX * 32.48639) + 39.42253))) + _
                '            (0.33 * ((SDIPLOIGO(R%) * 129.6466) + 244.7328) / ((DIPLOIGOMAX * 129.6466) + 244.7328)) + _
                '            (0.17 * (((SDIPLOTREATY(R%) * 747.6514) + 906.7255) / ((DIPLOTREATYMAX * 747.6514) + 906.7255)))) * GOVREV(R%)) / _
                '            AMAX(0.001, WDPI) * wpwghtpow(22)
                '        'dkb 2014/07/07 - END
                '
                '        'dkb 2014/07/07 adding Effective Human Capital POWER component - START
                '        power_effectivehumancap = ((POP15TO65(R%) - (SINCOMELT1LN(R%) * (POP15TO65(R%) / POP(R%))) - _
                '            (((SINCOMELT2LN(R%) - SINCOMELT1LN(R%)) / 2) * (POP15TO65(R%) / POP(R%)))) * SHDINEW(R%)) / AMAX(0.001, WEHC) * wpwghtpow(24)
                '        'dkb 2014/07/07 adding Effective Human Capital POWER component - END
                '
                '        'dkb 2014/09/24 Swapping out Mil Spend at PPP for MER and fixing hardcoded weights - START
                '        'dkb 2014/07/07 Adding components to hardcode POWERHHMI - START
                '        powerHHMI_pop = POP(R%) / AMAX(0.001, WPOP) * 0.8
                '        powerHHMI_GDPPPP = GDPP(R%) / AMAX(0.001, WGDPPPP) * 1.1
                '        powerHHMI_techp = GDPP(R%) * GDPPCP(R%) / AMAX(0.001, WTECHP) * 0.3
                '        'powerHHMI_milPPP = SGDSMil(R%) * GDPP(R%) / GDP(R%) / AMAX(0.001, WGDSMIL) * 0.9
                '        powerHHMI_mil = SGDSMil(R%) / AMAX(0.001, WGDSMIL) * 0.9
                '        powerHHMI_embassy = ((SDIPLOEMBASSY(R%) * 32.48639) + 39.42253) / AMAX(0.001, WDPIBN) * 0.3
                '        powerHHMI_igo = ((SDIPLOIGO(R%) * 129.6466) + 244.7328) / AMAX(0.001, WDPIMN) * 0.2
                '        powerHHMI_treaty = ((SDIPLOTREATY(R%) * 747.6514) + 906.7255) / AMAX(0.001, WDPIILN) * 0.1
                '        'dkb 2014/09/24 - END
                '
                '        If SGOVNUCWP(R%) > 0 Then
                '          powerHHMI_GovNucWP = Log(SGOVNUCWP(R%)) / AMAX(0.001, WGOVNUCWP) * 0.25
                '        Else
                '          powerHHMI_GovNucWP = 0
                '        End If
                '        'dkb 2014/07/07 Adding components to hardcode POWERHHMI - END
                '
                '       'dkb 2013/09/30 Added power_embassy, power_igo, power_treaty, and power)dpi to POWER(R%) calculation
                '       'Created POWERHH(R%) to incoporate into EmbassyEstimate calculation to avoid multicolinearity
                '       'POWERHH(R%) = power_pop + power_GDPPPP + POWER_GDP + power_mil + power_techp + POWER_TECH
                '       'POWER(R%) = power_pop + power_GDPPPP + POWER_GDP + power_techp + POWER_TECH + _
                '               POWER_G power_mil + POWER_Conv + POWER_Nuc + power_embassy + power_igo + power_treaty + power_dpi
                '        'dkb 2013/09/30 - END
                '        'dkb 2014/09/24 Swapping out Mil Spend at PPP for MER - START
                '        'dkb 2014/07/07 Replacing POWERHH with hardcoded POWERHHMI - START
                '
                '
                '        POWERHHMI(R%) = powerHHMI_pop + powerHHMI_GDPPPP + powerHHMI_techp + powerHHMI_embassy + _
                '                powerHHMI_igo + powerHHMI_treaty + powerHHMI_GovNucWP + powerHHMI_mil
                '        'dkb 2014/07/07 Replacing POWERHH with hardcoded POWERHHMI - END
                '        'dkb 2014/09/24 - END
                '
                ''If RegName$(R%) = "Saudi Arabia" Then Stop
                '        'dkb 2014/07/07 Updating POWER with GPI components and Effective Human Capital - START
                '        POWER(R%) = power_pop + power_GDPPPP + power_techp + power_GovNucWP + power_EnergyNetExport + power_TradeTotal + _
                '            power_FDIInflows + power_AID + power_HumCapWkPopHDI + power_ConnectivityICTProd + power_dpi + power_milPPP + _
                '            power_effectivehumancap
                '        POWER(R%) = POWER(R%) + POWER_GDP + POWER_TECH + POWER_G + power_mil + POWER_Conv + POWER_Nuc + power_GovRev + _
                '            power_InnovationRD + power_embassy + power_igo + power_treaty
                '        'dkb 2014/07/07 - END
                '
                '       'POWER(R%) = power_pop + power_GDPPPP + POWER_GDP + power_techp + POWER_TECH + _
                '               POWER_G + power_mil + POWER_Conv + POWER_Nuc
                '        'jrs 2011/10/11
                '        'POWER(R%) = POWER(R%) + power_GovNucWP + power_EnergyNetExport + power_TradeTotal + power_FDIInflows + _
                '        power_AID + power_HumCapWkPopHDI + power_GovRev + power_InnovationRD + power_ConnectivityICTProd
                ''If RegName$(R%) = "United States of America" Then Stop
                '
                '        'dkb 2013/10/30 - Added scale for POWERHH
                '        'dkb 2014/07/07 - Changed POWERHH to POWERHHMI and hardcoded POWERHHMI's PBASE - START
                '        'POWERHH(R%) = AMAX(-0.1, POWERHH(R%) / AMAX(0.001, PBASE) * 100)
                '        POWERHHMI(R%) = AMAX(-0.1, POWERHHMI(R%) / AMAX(0.001, 3.95) * 100)
                '        'dkb 2014/07/07 - END
                '        POWER(R%) = AMAX(-0.1, POWER(R%) / AMAX(0.001, PBASE) * 100)
                ''If RegName$(R%) = "United States of America" Then Stop
                '
                '        'dkb 2013/10/30 Bound min POWER value to positive because it will later be logged. Micronesia, Tonga,
                '        'and Sao Tome have very small POWER values (1E^-4). - START
                '       'POWER(R%) = Amin(90, POWER(R%))
                '        'dkb 2014/07/07 - Changed POWERHH to POWERHHMI - START
                '       POWER(R%) = AMAX(0.000001, Amin(90, POWER(R%)))
                '       POWERHHMI(R%) = AMAX(0.000001, Amin(90, POWERHHMI(R%)))
                '       'dkb 2013/10/30 - END
                '
                ''       POWER(R%) = AMAX(-0.1, POWER(R%) / AMAX(0.001, PBASE) * 100)
                ''       POWER(R%) = Amin(90, POWER(R%))
                ''========================================='
                ''=====   END POWER COMPONENT BLOCK   ====='
                ''========================================='
                'dkb 2014/09/24 - The previous commented-out block can be found in above code - START
            Next R%
        End Sub
        '=============================SocioPolitical.BAS  =======================
        Private Sub PowerConcentration()
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            '   Power Concentration with EU counted as Single Power
            '   ---------------------------------------------------
            Dim POWEREU As Single
            Dim SumGreat As Single
            Dim SumGreatPower As Single
            Dim SumSharesSq As Single
            Dim PortionP As Single
            Dim R As Integer
            Dim SumSharesSqAll As Single
            Dim SumAll As Single

            POWEREU = 0
            For R% = 1 To NR%
                If eumembsw(R%) = 1 Then
                    POWEREU = POWEREU + POWER(R%)
                End If
            Next R%

            If POWEREU > wpgreatlev Then
                SumGreat = 1
                SumGreatPower = POWEREU
            Else
                SumGreat = 0
                SumGreatPower = 0
            End If
            For R% = 1 To NR%
                If POWER(R%) >= wpgreatlev And eumembsw(R%) <> 1 Then
                    SumGreat = SumGreat + 1
                    SumGreatPower = SumGreatPower + POWER(R%)
                End If
            Next R%
            SumGreat = AMAX(1, SumGreat)
            SumGreatPower = AMAX(0.01, SumGreatPower)
            If POWEREU > wpgreatlev Then
                SumSharesSq = CSng((POWEREU / SumGreatPower) ^ 2)
            Else
                SumSharesSq = 0
            End If
            For R% = 1 To NR%
                If POWER(R%) >= wpgreatlev And eumembsw(R%) <> 1 Then
                    SumSharesSq = CSng(SumSharesSq + (POWER(R%) / SumGreatPower) ^ 2)
                End If
            Next R%
            If SumGreat <= 1.1 Then
                PCONGREATEU = 1
            Else
                PCONGREATEU = CSng(((AMAX(0.001, SumSharesSq - 1 / SumGreat)) / (1 - 1 / SumGreat)) ^ 0.5)
            End If
            '   Power Concentration with EU counted as Single Power - Partial Great Powers
            '   --------------------------------------------------------------------------
            POWEREU = 0
            For R% = 1 To NR%
                If eumembsw(R%) = 1 Then
                    POWEREU = POWEREU + POWER(R%)
                End If
            Next R%
            If POWEREU > wpgreatlev Then
                SumGreat = 1
                SumGreatPower = POWEREU
            ElseIf POWEREU >= wpgreatthresh And POWEREU < wpgreatlev Then
                'jrs 2020/02/12 R = 187, in vb6 doesn't blow up but the content is 0
                'PortionP = (POWER(R%) - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                PortionP = (0 - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                SumGreat = PortionP
                SumGreatPower = POWEREU * PortionP
            Else
                SumGreat = 0
                SumGreatPower = 0
            End If
            For R% = 1 To NR%
                If POWER(R%) >= wpgreatlev And eumembsw(R%) <> 1 Then
                    SumGreat = SumGreat + 1
                    SumGreatPower = SumGreatPower + POWER(R%)
                ElseIf POWER(R%) >= wpgreatthresh And POWER(R%) < wpgreatlev And eumembsw(R%) <> 1 Then
                    PortionP = (POWER(R%) - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                    SumGreat = SumGreat + PortionP
                    SumGreatPower = SumGreatPower + POWER(R%) * PortionP
                End If
            Next R%
            SumGreat = AMAX(1, SumGreat)
            SumGreatPower = AMAX(0.01, SumGreatPower)
            If POWEREU > wpgreatlev Then
                SumSharesSq = CSng((POWEREU / SumGreatPower) ^ 2)
            ElseIf POWEREU >= wpgreatthresh And POWEREU < wpgreatlev Then
                'jrs 2020/02/12 R = 187, in vb6 doesn't blow up but the content is 0
                'PortionP = (POWER(R%) - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                PortionP = (0 - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                SumSharesSq = CSng((POWEREU * PortionP / SumGreatPower) ^ 2)
            Else
                SumSharesSq = 0
            End If
            For R% = 1 To NR%
                If POWER(R%) >= wpgreatlev And eumembsw(R%) <> 1 Then
                    SumSharesSq = CSng(SumSharesSq + (POWER(R%) / SumGreatPower) ^ 2)
                ElseIf POWER(R%) >= wpgreatthresh And POWER(R%) < wpgreatlev And eumembsw(R%) <> 1 Then
                    PortionP = (POWER(R%) - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                    SumSharesSq = CSng(SumSharesSq + (POWER(R%) * PortionP / SumGreatPower) ^ 2)
                End If
            Next R%
            If SumGreat <= 1.1 Then
                PCONGREATEUF = 1
            Else
                PCONGREATEUF = CSng(((AMAX(0.001, SumSharesSq - 1 / SumGreat)) / (1 - 1 / SumGreat)) ^ 0.5)
            End If

            '   Power Concentration with EU not counted as Single Power
            '   -------------------------------------------------------

            SumGreat = 0
            SumGreatPower = 0
            SumAll = 0
            For R% = 1 To NR%
                If POWER(R%) >= wpgreatlev Then
                    SumGreat = SumGreat + 1
                    SumGreatPower = SumGreatPower + POWER(R%)
                End If
                SumAll = SumAll + POWER(R%)
            Next R%
            SumGreat = AMAX(1, SumGreat)
            SumGreatPower = AMAX(0.01, SumGreatPower)
            SumAll = AMAX(0.01, SumAll)
            SumSharesSq = 0
            SumSharesSqAll = 0
            For R% = 1 To NR%
                If POWER(R%) >= wpgreatlev Then
                    SumSharesSq = CSng(SumSharesSq + (POWER(R%) / SumGreatPower) ^ 2)
                End If
                SumSharesSqAll = CSng(SumSharesSqAll + (POWER(R%) / SumAll) ^ 2)
            Next R%
            If SumGreat <= 1.1 Then
                PCONGREAT = 1
            Else
                PCONGREAT = CSng(((AMAX(0.001, SumSharesSq - 1 / SumGreat)) / (1 - 1 / SumGreat)) ^ 0.5)
            End If
            PCONSYS = CSng(((AMAX(0.001, SumSharesSqAll - 1 / NR%)) / (1 - 1 / NR%)) ^ 0.5)
            '   Power Concentration with EU not counted as Single Power - Partial Great Powers
            '   ------------------------------------------------------------------------------

            SumGreat = 0
            SumGreatPower = 0
            SumAll = 0
            For R% = 1 To NR%
                If POWER(R%) >= wpgreatlev Then
                    SumGreat = SumGreat + 1
                    SumGreatPower = SumGreatPower + POWER(R%)
                ElseIf POWER(R%) >= wpgreatthresh And POWER(R%) < wpgreatlev Then
                    PortionP = (POWER(R%) - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                    SumGreat = SumGreat + PortionP
                    SumGreatPower = SumGreatPower + POWER(R%) * PortionP
                End If
                SumAll = SumAll + POWER(R%)
            Next R%
            SumGreat = AMAX(1, SumGreat)
            SumGreatPower = AMAX(0.01, SumGreatPower)
            SumAll = AMAX(0.01, SumAll)
            SumSharesSq = 0
            SumSharesSqAll = 0
            For R% = 1 To NR%
                If POWER(R%) >= wpgreatlev Then
                    SumSharesSq = CSng(SumSharesSq + (POWER(R%) / SumGreatPower) ^ 2)
                ElseIf POWER(R%) >= wpgreatthresh And POWER(R%) < wpgreatlev Then
                    PortionP = (POWER(R%) - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                    SumSharesSq = CSng(SumSharesSq + (POWER(R%) * PortionP / SumGreatPower) ^ 2)
                End If
                SumSharesSqAll = CSng(SumSharesSqAll + (POWER(R%) / SumAll) ^ 2)
            Next R%
            If SumGreat <= 1.1 Then
                PCONGREATF = 1
            Else
                PCONGREATF = CSng(((AMAX(0.001, SumSharesSq - 1 / SumGreat)) / (1 - 1 / SumGreat)) ^ 0.5)
            End If
            PCONSYS = CSng(((AMAX(0.001, SumSharesSqAll - 1 / NR%)) / (1 - 1 / NR%)) ^ 0.5)

        End Sub

        'jrs 2021/12/05
        Private Sub PowerHHConcentration()
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            '   Power Concentration with EU counted as Single Power
            '   ---------------------------------------------------
            Dim POWEREU As Single
            Dim SumGreat As Single
            Dim SumGreatPower As Single
            Dim SumSharesSq As Single
            Dim PortionP As Single
            Dim R As Integer
            Dim SumSharesSqAll As Single
            Dim SumAll As Single

            POWEREU = 0
            For R% = 1 To NR%
                If eumembsw(R%) = 1 Then
                    POWEREU = POWEREU + POWERHH(R%)
                End If
            Next R%

            If POWEREU > wpgreatlev Then
                SumGreat = 1
                SumGreatPower = POWEREU
            Else
                SumGreat = 0
                SumGreatPower = 0
            End If
            For R% = 1 To NR%
                If POWERHH(R%) >= wpgreatlev And eumembsw(R%) <> 1 Then
                    SumGreat = SumGreat + 1
                    SumGreatPower = SumGreatPower + POWERHH(R%)
                End If
            Next R%
            SumGreat = AMAX(1, SumGreat)
            SumGreatPower = AMAX(0.01, SumGreatPower)
            If POWEREU > wpgreatlev Then
                SumSharesSq = CSng((POWEREU / SumGreatPower) ^ 2)
            Else
                SumSharesSq = 0
            End If
            For R% = 1 To NR%
                If POWERHH(R%) >= wpgreatlev And eumembsw(R%) <> 1 Then
                    SumSharesSq = CSng(SumSharesSq + (POWERHH(R%) / SumGreatPower) ^ 2)
                End If
            Next R%
            If SumGreat <= 1.1 Then
                PCONGREATEU = 1
            Else
                PCONGREATEU = CSng(((AMAX(0.001, SumSharesSq - 1 / SumGreat)) / (1 - 1 / SumGreat)) ^ 0.5)
            End If
            '   Power Concentration with EU counted as Single Power - Partial Great Powers
            '   --------------------------------------------------------------------------
            POWEREU = 0
            For R% = 1 To NR%
                If eumembsw(R%) = 1 Then
                    POWEREU = POWEREU + POWERHH(R%)
                End If
            Next R%
            If POWEREU > wpgreatlev Then
                SumGreat = 1
                SumGreatPower = POWEREU
            ElseIf POWEREU >= wpgreatthresh And POWEREU < wpgreatlev Then
                'jrs 2020/02/12 R = 187, in vb6 doesn't blow up but the content is 0
                'PortionP = (POWERHH(R%) - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                PortionP = (0 - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                SumGreat = PortionP
                SumGreatPower = POWEREU * PortionP
            Else
                SumGreat = 0
                SumGreatPower = 0
            End If
            For R% = 1 To NR%
                If POWERHH(R%) >= wpgreatlev And eumembsw(R%) <> 1 Then
                    SumGreat = SumGreat + 1
                    SumGreatPower = SumGreatPower + POWERHH(R%)
                ElseIf POWERHH(R%) >= wpgreatthresh And POWERHH(R%) < wpgreatlev And eumembsw(R%) <> 1 Then
                    PortionP = (POWERHH(R%) - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                    SumGreat = SumGreat + PortionP
                    SumGreatPower = SumGreatPower + POWERHH(R%) * PortionP
                End If
            Next R%
            SumGreat = AMAX(1, SumGreat)
            SumGreatPower = AMAX(0.01, SumGreatPower)
            If POWEREU > wpgreatlev Then
                SumSharesSq = CSng((POWEREU / SumGreatPower) ^ 2)
            ElseIf POWEREU >= wpgreatthresh And POWEREU < wpgreatlev Then
                'jrs 2020/02/12 R = 187, in vb6 doesn't blow up but the content is 0
                'PortionP = (POWERHH(R%) - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                PortionP = (0 - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                SumSharesSq = CSng((POWEREU * PortionP / SumGreatPower) ^ 2)
            Else
                SumSharesSq = 0
            End If
            For R% = 1 To NR%
                If POWERHH(R%) >= wpgreatlev And eumembsw(R%) <> 1 Then
                    SumSharesSq = CSng(SumSharesSq + (POWERHH(R%) / SumGreatPower) ^ 2)
                ElseIf POWERHH(R%) >= wpgreatthresh And POWERHH(R%) < wpgreatlev And eumembsw(R%) <> 1 Then
                    PortionP = (POWERHH(R%) - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                    SumSharesSq = CSng(SumSharesSq + (POWERHH(R%) * PortionP / SumGreatPower) ^ 2)
                End If
            Next R%
            If SumGreat <= 1.1 Then
                PCONGREATEUF = 1
            Else
                PCONGREATEUF = CSng(((AMAX(0.001, SumSharesSq - 1 / SumGreat)) / (1 - 1 / SumGreat)) ^ 0.5)
            End If

            '   Power Concentration with EU not counted as Single Power
            '   -------------------------------------------------------

            SumGreat = 0
            SumGreatPower = 0
            SumAll = 0
            For R% = 1 To NR%
                If POWERHH(R%) >= wpgreatlev Then
                    SumGreat = SumGreat + 1
                    SumGreatPower = SumGreatPower + POWERHH(R%)
                End If
                SumAll = SumAll + POWERHH(R%)
            Next R%
            SumGreat = AMAX(1, SumGreat)
            SumGreatPower = AMAX(0.01, SumGreatPower)
            SumAll = AMAX(0.01, SumAll)
            SumSharesSq = 0
            SumSharesSqAll = 0
            For R% = 1 To NR%
                If POWERHH(R%) >= wpgreatlev Then
                    SumSharesSq = CSng(SumSharesSq + (POWERHH(R%) / SumGreatPower) ^ 2)
                End If
                SumSharesSqAll = CSng(SumSharesSqAll + (POWERHH(R%) / SumAll) ^ 2)
            Next R%
            If SumGreat <= 1.1 Then
                PCONGREAT = 1
            Else
                PCONGREAT = CSng(((AMAX(0.001, SumSharesSq - 1 / SumGreat)) / (1 - 1 / SumGreat)) ^ 0.5)
            End If
            PCONSYS = CSng(((AMAX(0.001, SumSharesSqAll - 1 / NR%)) / (1 - 1 / NR%)) ^ 0.5)
            '   Power Concentration with EU not counted as Single Power - Partial Great Powers
            '   ------------------------------------------------------------------------------

            SumGreat = 0
            SumGreatPower = 0
            SumAll = 0
            For R% = 1 To NR%
                If POWERHH(R%) >= wpgreatlev Then
                    SumGreat = SumGreat + 1
                    SumGreatPower = SumGreatPower + POWERHH(R%)
                ElseIf POWERHH(R%) >= wpgreatthresh And POWERHH(R%) < wpgreatlev Then
                    PortionP = (POWERHH(R%) - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                    SumGreat = SumGreat + PortionP
                    SumGreatPower = SumGreatPower + POWERHH(R%) * PortionP
                End If
                SumAll = SumAll + POWERHH(R%)
            Next R%
            SumGreat = AMAX(1, SumGreat)
            SumGreatPower = AMAX(0.01, SumGreatPower)
            SumAll = AMAX(0.01, SumAll)
            SumSharesSq = 0
            SumSharesSqAll = 0
            For R% = 1 To NR%
                If POWERHH(R%) >= wpgreatlev Then
                    SumSharesSq = CSng(SumSharesSq + (POWERHH(R%) / SumGreatPower) ^ 2)
                ElseIf POWERHH(R%) >= wpgreatthresh And POWERHH(R%) < wpgreatlev Then
                    PortionP = (POWERHH(R%) - wpgreatthresh) / (wpgreatlev - wpgreatthresh)
                    SumSharesSq = CSng(SumSharesSq + (POWERHH(R%) * PortionP / SumGreatPower) ^ 2)
                End If
                SumSharesSqAll = CSng(SumSharesSqAll + (POWERHH(R%) / SumAll) ^ 2)
            Next R%
            If SumGreat <= 1.1 Then
                PCONGREATF = 1
            Else
                PCONGREATF = CSng(((AMAX(0.001, SumSharesSq - 1 / SumGreat)) / (1 - 1 / SumGreat)) ^ 0.5)
            End If
            PCONSYS = CSng(((AMAX(0.001, SumSharesSqAll - 1 / NR%)) / (1 - 1 / NR%)) ^ 0.5)

        End Sub

        'jrs 2011/02/14 *** Embeddedness new Procedure  ***
        Private Sub Embeddedness(context As IForecastContext)
            If IY = 1 Then
                ReDim GOVEMBEDShift(NR), GOVEMBEDGR(NR), SGOVEMBED(NR)
                If context.HistoricMode = False Then
                    'jrs 2012/06/15 Dim dbGR As Database
                    'jrs 2012/06/15 Set dbGR = OpenDatabase(IFs_MDB2Active)
                    Call LoadGrowthRatesOneDim("GOVEMBEDGR", GOVEMBEDGR, context)
                    'jrs 2012/06/15 dbGR.Close
                End If

            End If

            Dim R As Integer, FormValue As Single
            For R = 1 To NR
                'jrs 2021/12/05 added switch
                If wppowhhsw = 0 Then
                    Call AnalFunc("Government Embeddedness (2000)",
                    New Single() {GDPPCP(R), GOVEXP(R%) / GDP(R) * 100, POWER(R)}, FormValue,
                    New String() {"GDP2000PCPPP(2000)/ 1000 (Linear)", "GovExpense%GDP(2000) (Linear)", "GovHistMatPowerJDM(2000) (Linear)"}, context)
                Else
                    Call AnalFunc("Government Embeddedness (2000)",
                    New Single() {GDPPCP(R), GOVEXP(R%) / GDP(R) * 100, POWERHH(R)}, FormValue,
                    New String() {"GDP2000PCPPP(2000)/ 1000 (Linear)", "GovExpense%GDP(2000) (Linear)", "GovHistMatPowerJDM(2000) (Linear)"}, context)
                End If

                'First Year
                If IY = 1 Then
                    GOVEMBEDShift(R) = GOVEMBED(R) - FormValue
                Else 'Other Years
                    'jrs 2019/09/09
                    If Not GOVEMBEDGR(R).HasValue Then
                        GOVEMBED(R) = FormValue + GOVEMBEDShift(R)
                    Else
                        GOVEMBED(R) = ConvergeOverTime(SGOVEMBED(R) * (1 + CSng(GOVEMBEDGR(R))), FormValue + ConvergeOverTime(GOVEMBEDShift(R), 0, 100), 20)
                    End If
                    GOVEMBED(R) = AMAX(GOVEMBED(R), 0)
                End If
                If SGOVEMBED(R) <> 0 And GOVEMBEDGR(R).HasValue Then
                    GOVEMBEDGR(R) = GOVEMBED(R) / SGOVEMBED(R) - 1
                End If
                SGOVEMBED(R) = GOVEMBED(R)
            Next R
        End Sub

        'jrs 2024/06/27 
        Dim TFNameBusinessRegIdx As String, TFNameCostStartBuss As String
        'jrs 2025/01/07
        Dim sOilinc250pcthreshAll() As Single
        'jrs 2025/05/08
        Dim TFNameSurvSelfExpCoh1, TFNameSurvSelfExpCoh2, TFNameSurvSelfExpCoh3, TFNameSurvSelfExpCoh4, TFNameSurvSelfExpCoh5, TFNameSurvSelfExpCoh6 As String
        Dim TFNameSurvSelfExpCohs() As String
        'jrs 2025/05/15
        Dim TFNameTradSecRatCoh1, TFNameTradSecRatCoh2, TFNameTradSecRatCoh3, TFNameTradSecRatCoh4, TFNameTradSecRatCoh5, TFNameTradSecRatCoh6, TFNameTradSecRatTotal As String
        Dim TFNameTradSecRatCohs() As String

        Public Sub IFsDomPol1FirstYear(context As IForecastContext)
            '
            '   FIRST TIME STEP, FIRST CALL ONLY
            '   -----------------------------------------------------------------------

            ReDim DEMOCPOLITYI(NR%), SDEMOCPOLITY(NR%), smdemocpolity(NR%)
            'jrs 2023/10/12
            ReDim DEMOCPOLYARCHYShift(NR%)
            ReDim SDEMOCPOLYARCHY(NR%)
            'ReDim SDEMOCPOLYARCHYFX(NR%)
            'ReDim PolyarchyGR(NR%)

            ReDim swingeffects(NR%), demglobaleffects(NR%)
            ReDim FREEDOMI(NR%), econfreei(NR%), GovEffectAdd(NR%), GovCorruptAdd(NR%)
            ReDim GEMI(NR%), GEMPI(NR%), democcomp(NR%)
            'BBH 2011/04/14
            ReDim YTHBULGEI(NR%), gdprmovavg(NR%), ssfintlwarallLoc(NR%), econfreeri(NR%)
            'dsb 2026/1/8 
            ReDim ContiguityCivilWarPardeeAddI(NR%)
            'BBH 2011/06/18
            ReDim energydepend(NR%), ContiguityIntlWarAddI(NR%)
            ReDim democpolityri(NR%)
            ReDim snumnwp(NR%), INUMNWP(NR%), inumnwpgr(NR%), numnwpmin(NR%)
            ReDim SFINSTABALLRI(NR%), SFINSTABMAGRI(NR%), SFINSTABY1RI(NR%)
            'dsb 2026/08/04
            ReDim ContiguityCivilWarUCDPAddI(NR%)
            'BBH 2011/06/18
            ReDim SFINTLWARALLADD(NR%), SFINTLWARMAGRI(NR%), SFINTLWARY1RI(NR%)
            'bbh 2011/09/21
            ReDim SFINTLWARALLI(NR%), SFINTLWARMAGI(NR%)
            ReDim SFPredictionI(NR%)
            ReDim SSURVSEI(NR%, WVSCohorts)
            ReDim SwingStsDemoc(9), SwingStsNumb(9), sedyrsag15(NR%), EDYRSAG15I(NR%)
            'bbh 2011/08/19
            ReDim sedyrsag15fem(NR%), EDYRSAG15FemI(NR%), SGEM(NR%)
            ReDim RandDExpScale(NR%), RAndDExpI(NR%), EdTerGRateScale(NR%), EdTerGRateI(NR%)
            'jrs 2009/04/22
            ReDim GovRegQualAdd(NR%)
            'dsb 2026/1/8
            ReDim ssfcivilwarPardeeLoc(NR%), SFCIVILWARPARDEEADD(NR%), SFCIVILWARPARDEEI(NR%)
            'dsb 2026/1/13
            ReDim SFCIVILWARPOPIMPSHIFTBASE(NR%)
            ReDim SSFCIVILWARPOPIMP(NR%)
            'dsb 2026/1/15
            ReDim SSFCIVILWARPARDEE(NR%), GOVEFFECT_Lag1(NR%), GOVEFFECT_Lag(NR%), SVDTHSWOMENCHILDREN_Lag1(NR%), SVDTHSWOMENCHILDREN_Lag(NR%), SSVDTHSOTHERINTERPERSON_Pop(NR%)
            'dsb 2026/06/04
            ReDim SSFCIVILWARUCDP(NR%), SSFINTLWARALL(NR%)
            'jrs 2024/05/17
            ReDim ssfcivilwarUCDPLoc(NR%), SFCIVILWARUCDPADD(NR%), SFCIVILWARUCDPI(NR%)
            'jrs 2025/08/27
            ReDim SPRDSEC(NR), SPRDSECFX(NR%), AvgDistanceAtoB(NR%)
            LoadAvgDistance(context)

            'mti 2014/11/14 -start
            ReDim GovBusCostShift(NR%)
            'mti 2014/11/14 - end

            'mti 2015/03/04 - start
            ReDim GovBusRegIndShift(NR%)
            'mti 2015/03/04 - end

            'jrs 2016/01/13
            ReDim SSFINTLWARMAG(NR%)
            'kbn 2018/01/11 - Define shift factors for all drugs, terms required
            'ReDim DrugAmphetamineshift(NR%), DrugCocaineshift(NR%), DrugOpiateShift(NR%)
            'ReDim SDRUGUSEAMPHETAMINE(NR%), SDRUGUSECOCAINE(NR%), SDRUGUSEOPIATES(NR%)
            'kbn 2018/01/11 End
            'kbn 2018/04/26- Start- Define shift factors for violence model here
            'dkb 2018/05/15 Added  new estimate variables for self harm
            ReDim PoliceShift(NR%), ConflictShift(NR%), SelfHarmShift(NR%), HomicideShift(NR%), WomenandChilshift(NR%), SSVDTHSSELFHARM(NR%), SSVDTHSPOLICS(NR%), Womenshift(NR%), ChilShift(NR%), SelfHarmEstimate(NR%), sSelfHarmEstimate(NR%)
            ReDim SSVDTHSCONFLICT(NR%), SSVDTHSOTHERINTERPERSON(NR%), SSVDTHSWOMENCHILDREN(NR%), SSVDTHSSELFHARM(NR%), SSVDTHSPOLICS(NR%), SSOCIETALVIOLENCEDEATHS(NR%), SHOMICIDEINDEX(NR%), SSVDTHSWOMEN(NR%), SSVDTHSCHIL(NR%)
            ReDim PoliceEstimate(NR%), ConflictEstimate(NR%), SelfHarmEstimate(NR%), HomicideEstimate(NR%), WomenandChilEstimate(NR%)
            ReDim sPoliceEstimate(NR%), sConflictEstimate(NR%), sSelfHarmEstimate(NR%), sHomicideEstimate(NR%), sWomenandChilEstimate(NR%)
            ReDim GIIShift(NR%), GIII(NR%) 'dkb 2020/02/24
            'dkb 2018/05/15- End-
            'kbn 2018/04/26- End-

            'jrs 2025/01/07
            ReDim sOilinc250pcthreshAll(NR%)

            'jrs 2023/10/18
            'Call LoadGrowthRatesOneDim("PolyarchyGR", PolyarchyGR, context)



            '   BBH 6/9/04 V5_0
            '   First Year 1: Initial Freedom, Democracy Levels, Governance Effectiveness
            '   --------------------------------------------------------------------------

            DemocWaveI = DEMOCWAVE
            SDEMOCWAVE = DEMOCWAVE
            SDEMOCWVCOUNT = DEMOCWVCOUNT
            SDEMOCWVDIR = DEMOCWVDIR

            'jrs 2023/10/13
            Dim PolyarchyVal As Single

            'jrs 2023/06/27  
            TFNameBusinessRegIdx = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Business Regulation Index (" & BaseYear & ") Log"
            TFNameBusinessRegIdx = getFunctionNameYrOrMstRct(TFNameBusinessRegIdx, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameCostStartBuss = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Cost of Starting Business (" & CStr(BaseYear) & ") Log"
            TFNameCostStartBuss = getFunctionNameYrOrMstRct(TFNameCostStartBuss, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            'jrs 2025/05/08
            ReDim TFNameSurvSelfExpCohs(6)
            TFNameSurvSelfExpCoh1 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Survival/Self-Expression Cohort 1 Log"
            TFNameSurvSelfExpCoh1 = getFunctionNameYrOrMstRct(TFNameSurvSelfExpCoh1, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameSurvSelfExpCohs(1) = TFNameSurvSelfExpCoh1
            TFNameSurvSelfExpCoh2 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Survival/Self-Expression Cohort 2 Log"
            TFNameSurvSelfExpCoh2 = getFunctionNameYrOrMstRct(TFNameSurvSelfExpCoh2, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameSurvSelfExpCohs(2) = TFNameSurvSelfExpCoh2
            TFNameSurvSelfExpCoh3 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Survival/Self-Expression Cohort 3 Log"
            TFNameSurvSelfExpCoh3 = getFunctionNameYrOrMstRct(TFNameSurvSelfExpCoh3, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameSurvSelfExpCohs(3) = TFNameSurvSelfExpCoh3
            TFNameSurvSelfExpCoh4 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Survival/Self-Expression Cohort 4 Log"
            TFNameSurvSelfExpCoh4 = getFunctionNameYrOrMstRct(TFNameSurvSelfExpCoh4, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameSurvSelfExpCohs(4) = TFNameSurvSelfExpCoh4
            TFNameSurvSelfExpCoh5 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Survival/Self-Expression Cohort 5 Log"
            TFNameSurvSelfExpCoh5 = getFunctionNameYrOrMstRct(TFNameSurvSelfExpCoh5, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameSurvSelfExpCohs(5) = TFNameSurvSelfExpCoh5
            TFNameSurvSelfExpCoh6 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Survival/Self-Expression Cohort 6 Log"
            TFNameSurvSelfExpCoh6 = getFunctionNameYrOrMstRct(TFNameSurvSelfExpCoh6, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameSurvSelfExpCohs(6) = TFNameSurvSelfExpCoh6
            'jrs 2025/05/15
            ReDim TFNameTradSecRatCohs(6)
            TFNameTradSecRatCoh1 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Traditional/Secular-Rational Cohort 1 Log"
            TFNameTradSecRatCoh1 = getFunctionNameYrOrMstRct(TFNameTradSecRatCoh1, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameTradSecRatCohs(1) = TFNameTradSecRatCoh1
            TFNameTradSecRatCoh2 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Traditional/Secular-Rational Cohort 2 Log"
            TFNameTradSecRatCoh2 = getFunctionNameYrOrMstRct(TFNameTradSecRatCoh2, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameTradSecRatCohs(2) = TFNameTradSecRatCoh2
            TFNameTradSecRatCoh3 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Traditional/Secular-Rational Cohort 3 Log"
            TFNameTradSecRatCoh3 = getFunctionNameYrOrMstRct(TFNameTradSecRatCoh3, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameTradSecRatCohs(3) = TFNameTradSecRatCoh3
            TFNameTradSecRatCoh4 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Traditional/Secular-Rational Cohort 4 Log"
            TFNameTradSecRatCoh4 = getFunctionNameYrOrMstRct(TFNameTradSecRatCoh4, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameTradSecRatCohs(4) = TFNameTradSecRatCoh4
            TFNameTradSecRatCoh5 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Traditional/Secular-Rational Cohort 5 Log"
            TFNameTradSecRatCoh5 = getFunctionNameYrOrMstRct(TFNameTradSecRatCoh5, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameTradSecRatCohs(5) = TFNameTradSecRatCoh5
            TFNameTradSecRatCoh6 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Traditional/Secular-Rational Cohort 6 Log"
            TFNameTradSecRatCoh6 = getFunctionNameYrOrMstRct(TFNameTradSecRatCoh6, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameTradSecRatCohs(6) = TFNameTradSecRatCoh6
            TFNameTradSecRatTotal = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Traditional/Secular-Rational Total Log"
            TFNameTradSecRatTotal = getFunctionNameYrOrMstRct(TFNameTradSecRatTotal, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")

            For R% = 1 To NR%
                FREEDOMI(R%) = FREEDOM(R%)
                econfreei(R%) = ECONFREE(R%)
                DEMOCPOLITYI(R%) = DEMOCPOLITY(R%)
                SDEMOCPOLITY(R%) = DEMOCPOLITY(R%)
                smdemocpolity(R%) = DEMOCPOLITY(R%)
                GEMI(R%) = GEM(R%)
                'BBH 2011/04/02
                YTHBULGEI(R%) = YTHBULGE(R%)
                gdprmovavg(R%) = igdpr(R%)

                'jrs 2023/10/13 *** start ***
                'clp 2024/03/28 *** start ***
                'Call AnalFunc("Polyarchy",
                '    New Single() {GINIDOM(R%), INFMOR(R%), GDPR(R%), GDPR(R%) * INFMOR(R%)}, PolyarchyVal,
                '    New String() {"GINIDOM", "INFMOR", "GDPR", "GDPRxINFMOR"}, context)
                'If GRPS(1, R%) = "United States of America" Then Stop
                Dim Oilinc250pcthresh As Single
                Dim SOilinc250pcthresh As Single

                If CSng(((ENPRI(R%) * ENP(R%, 1)) * 1000000000) / (POP(R%) * 1000000)) < 250 Or oilpcthresw = 0 Then ' clp 2024/12/20 add Or oilpcthresw = 0
                    Oilinc250pcthresh = 0 ' does not meet oil income pc threshold
                    'jrs 2025/01/07
                    sOilinc250pcthreshAll(R%) = 0
                Else
                    'jrs 2025/01/07
                    Oilinc250pcthresh = 1 'meets oil income pc threshold
                    sOilinc250pcthreshAll(R%) = 1
                End If

                'SOilinc250pcthresh(R%) = CSng(Oilinc250pcthresh) ' Why was this here?

                PolyarchyVal = CSng(0.1269731 + (EDYRSAG15(R%, 3) * 0.0558807) + (INFMOR(R%) * -0.0000843) + (Oilinc250pcthresh * -0.0312973))

                'If R% = 111 Then Stop
                If (DEMOCPOLYARCHY(R%) <> -9999 And PolyarchyVal <> 0) And DEMOCPOLYARCHY(R%) <> 0 Then ' clp 2024/07/24 Added or zero to fill holes as the empty data values are showing as zero and no data value hits true zero
                    'DEMOCPOLYARCHYShift(R%) = DEMOCPOLYARCHY(R%) / PolyarchyVal switched to additive
                    DEMOCPOLYARCHYShift(R%) = DEMOCPOLYARCHY(R%) - PolyarchyVal
                Else
                    DEMOCPOLYARCHY(R%) = PolyarchyVal
                    DEMOCPOLYARCHYShift(R%) = 0 'switched to additive
                End If
                DEMOCPOLYARCHY(R%) = AMAX(Amin(DEMOCPOLYARCHY(R%), 1), 0)
                'jrs 2023/10/23
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("DEMOCPOLYARCHY", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If
                SDEMOCPOLYARCHY(R%) = DEMOCPOLYARCHY(R%)
                'SDEMOCPOLYARCHYFX(R%) = PolyarchyVal
                'clp 2024/03/28 *** end ***
                'jrs 2023/10/13 *** end ***

                '        If frm_MainMenu.IFsGUIctrl1.cHistoricBase = True Then
                '            Call AnalFunc("Polity Democracy with GDPPCP and Surv-SE (1960)", _
                '                Array(GDPPCP(R%), SURVSE(R%, WVSCohorts)), Prediction)
                '                'Last, WVSCohorts cohort is total adult population
                '            democcomp(R%) = AMAX(0, Amin(20, Prediction))
                '        Else
                ''            Call AnalFunc("Polity Democracy with GDPPCP and Surv-SE (2000)", _
                '                Array(GDPPCP(R%), SURVSE(R%, WVSCohorts)), Prediction)
                '
                '            educ15term = EDYRSAG15(R%, 3)
                '            Call AnalFunc("Polity Democ with GDPPCP and EdYears 15 (2005)", _
                '                Array(GDPPCP(R%), educ15term), Prediction)
                '            EnergyTerm = ENX(R%) * ENPRI(R%) / GDP(R%)
                ''            Call AnalFunc("Polity Democ with EdYearsAg15, YthBulge, EnExDep (2005)", _
                ''                Array(educ15term,YthBulge(r%),EnergyTerm), Prediction)
                '            democcomp(R%) = AMAX(0, Amin(20, Prediction))
                '        End If
                '        DEMOCEXP(R%) = democcomp(R%)

                '        'bbh 2011/04/13 start
                educ15term = EDYRSAG15(R%, 3)
                sedyrsag15(R%) = EDYRSAG15(R%, 3)
                EDYRSAG15I(R%) = EDYRSAG15(R%, 3)
                'bbh 2011/08/19
                sedyrsag15fem(R%) = EDYRSAG15(R%, 2)
                EDYRSAG15FemI(R%) = EDYRSAG15(R%, 2)
                SGEM(R%) = GEM(R%)

                '        Call AnalFunc("Governance Effectiveness (2006)", _
                '            Array(GDPPCP(R%), educ15term), Prediction) 'Using bivariate in preprocessor
                '        Prediction = AMAX(0, Amin(5, Prediction + 2.5))
                '        GovEffectAdd(R%) = GOVEFFECT(R%) - Prediction
                'jrs 2009/04/22 begin
                '        Call AnalFunc("Governance Regulatory Quality (2006)", _
                '            Array(GDPPCP(R%), educ15term), Prediction) 'Using bivariate in preprocessor
                '        Prediction = AMAX(0, Amin(5, Prediction + 2.5))
                '        GovRegQualAdd(R%) = GOVREGQUAL(R%) - Prediction
                '        'jrs 2009/04/22 end
                '        'jrs 2009/06/05
                '        GOVREGQUAL(R) = GOVREGQUAL(R) * govregqualm(R)



                ''        TbName$ = "GDP/Capita (PPP 2000) Versus TI Corruption Perception (" & CStr(BaseYear) & ") Linear"
                ''        TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear)
                ''        Call XYTABL(TbName$, GDPPCP(R%), Prediction)
                '            'To be compatible with DataEcon and Economy
                '
                '        energyterm = ENX(R%) * ENPRI(R%) / GDP(R%)
                '        Call AnalFunc("Governance Corruption (2005)", _
                '            Array(GDPPCP(R%), DEMOCPOLITY(R%), energyterm), prediction)
                '            'To be compatible Economy; have used simpler function for DataEcon hole filling
                '
                '        govcorruptadd(R%) = GOVCORRUPT(R%) - prediction
                '
                '        'bbh 2011/04/13 end

                '
                '   First Year 1: Save information about networked people numbers
                '   -------------------------------------------------------------

                snumnwp(R%) = NUMNWP(R%)
                INUMNWP(R%) = NUMNWP(R%)
                inumnwpgr(R%) = NUMNWPGR(R%)
                numnwpmin(R%) = NUMNWP(R%)
                '
                '   First Year 1: Save survival/self-expression for democracy calculation
                '   ---------------------------------------------------------------------

                SSURVSEI(R%, WVSCohorts) = SURVSE(R%, WVSCohorts)
                '    First Year 1: Save Societal deaths value
                'kbn 2018/06/01- Start- Save first year values for violence variables
                SSOCIETALVIOLENCEDEATHS(R%) = SOCIETALVIOLENCEDEATHS(R%)
                SSVDTHSCONFLICT(R%) = SVDTHSCONFLICT(R%)
                SSVDTHSOTHERINTERPERSON(R%) = SVDTHSOTHERINTERPERSON(R%)
                SSVDTHSWOMENCHILDREN(R%) = SVDTHSWOMENCHILDREN(R%)
                SSVDTHSSELFHARM(R%) = SVDTHSSELFHARM(R%)
                SSVDTHSPOLICS(R%) = SVDTHSPOLICS(R%)
                SSVDTHSWOMEN(R%) = SVDTHSWOMEN(R%)
                SSVDTHSCHIL(R%) = SVDTHSCHIL(R%)
                'kbn 2018/06/01- End-
                'kbn 2018/09/25-Start- Calculate temporary values for saved values of homicide index
                AdultMaleTerm = ((agedst(4, R%, 1) + agedst(5, R%, 1) + agedst(6, R%, 1) + agedst(7, R%, 1) + agedst(8, R%, 1) + agedst(9, R%, 1) + agedst(10, R%, 1) +
     agedst(11, R%, 1) + agedst(12, R%, 1) + agedst(13, R%, 1) + agedst(14, R%, 1) + agedst(15, R%, 1) + agedst(16, R%, 1) + agedst(17, R%, 1) + agedst(18, R%, 1) + agedst(19, R%, 1) + agedst(20, R%, 1) + agedst(21, R%, 1)) * 1000000)
                WomenTerm = ((agedst(4, R%, 2) + agedst(5, R%, 2) + agedst(6, R%, 2) + agedst(7, R%, 2) + agedst(8, R%, 2) + agedst(9, R%, 2) + agedst(10, R%, 2) +
     agedst(11, R%, 2) + agedst(12, R%, 2) + agedst(13, R%, 2) + agedst(14, R%, 2) + agedst(15, R%, 2) + agedst(16, R%, 2) + agedst(17, R%, 2) + agedst(18, R%, 2) + agedst(19, R%, 2) + agedst(20, R%, 2) + agedst(21, R%, 2)) * 1000000)
                ChildrenTerm = (agedst(0, R%, 1) + agedst(0, R%, 2) + agedst(1, R%, 1) + agedst(1, R%, 2) + agedst(2, R%, 1) + agedst(2, R%, 2) + agedst(3, R%, 1) + agedst(3, R%, 2)) * 1000000
                poptotalterm = (AdultMaleTerm + WomenTerm + ChildrenTerm)

                'kbn 2018/11/26-Start- Added minimum for deaths at 0

                SVDTHSOTHERINTERTOT(R%) = AMAX(((SVDTHSOTHERINTERPERSON(R%) / 100000) * AdultMaleTerm) + svdthsadd(R%, 5), 0)
                SVDTHSPOLICSTOT(R%) = AMAX(((SVDTHSPOLICS(R%) / 100000) * poptotalterm) + svdthsadd(R%, 3), 0)
                SVDTHSCHILTOT(R%) = AMAX(((SVDTHSCHIL(R%) / 100000) * ChildrenTerm) + svdthsadd(R%, 4), 0)
                SVDTHSWOMENTOT(R%) = AMAX(((SVDTHSWOMEN(R%) / 100000) * WomenTerm) + svdthsadd(R%, 4), 0)
                SVDTHSSELFHARMTOT(R%) = AMAX(((SVDTHSSELFHARM(R%) / 100000) * poptotalterm) + svdthsadd(R%, 1), 0)
                SVDTHSCONFLICTTOT(R%) = AMAX(((SVDTHSCONFLICT(R%) / 100000) * poptotalterm) + svdthsadd(R%, 2), 0)
                SVDTHSSOCIETALVIOLENCETOT(R%) = AMAX(SVDTHSOTHERINTERTOT(R%) + SVDTHSPOLICSTOT(R%) + SVDTHSCHILTOT(R%) + SVDTHSWOMENTOT(R%) + SVDTHSSELFHARMTOT(R%) + SVDTHSCONFLICTTOT(R%) + svdthsadd(R%, 6), 0)
                'kbn 2018/11/26- Stop


                SHOMICIDEINDEX(R%) = ((SVDTHSCONFLICTTOT(R%) * svindexm(R%, 2) + (SVDTHSOTHERINTERTOT(R%) * svindexm(R%, 5)) + (SVDTHSWOMENTOT(R%) * svindexm(R%, 4)) + (SVDTHSCHILTOT(R%) * svindexm(R%, 4)) + (SVDTHSPOLICSTOT(R%) * svindexm(R%, 3)) /
     (svindexm(R%, 5) + svindexm(R%, 2) + svindexm(R%, 3) + svindexm(R%, 4)))) / POP(R%)

                'kbn 2018/09/25-End
            Next R%
            'jrs 2009/06/05
            computeRegQualExp(EDYRSAG15, context)

            'Call ValueChange
            ValueChange(context)
            'kbn 2018/09/25-Start- Compute Max homicides (Highest number of deaths)



            For R% = 1 To NR%
                MaxHomicides = 1
                MaxHomicides = AMAX(MaxHomicides, SHOMICIDEINDEX(R%))
                MaxHomicides = MaxHomicides * CSng(1.1)
            Next R%

            'JAM 2019-08-26
            Call IfsDomPolAllSteps(context)

            'kbn 2018/09/25-End

        End Sub
        ''' <summary>
        ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' 
        '''    2019-08-14 Y1Call2 DomPol
        ''' 
        ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' </summary>
        ''' <param name="context"></param>
        ''' 

        'jrs 2024/05/17
        Dim LNINFMORWORLD As Double

        Public Sub IFsDomPol2FirstYear(context As IForecastContext)
            Dim winfmor As Single
            Dim infmorterm As Single
            Dim TradeTerm As Single
            Dim democterm As Single
            Dim GDPterm As Single
            Dim Prediction As Single
            'Dim XYTBL As SeriesEstimator
            Dim TbName$
            Dim ExpPrediction As Single
            Dim AdjustedViolenceTerm As Single
            Dim SVTerm As Double
            Dim ConflictEst As Single
            Dim HomicideEst As Single
            Dim WomenandChilEst As Single
            Dim WomenEst As Single
            Dim ChilEst As Single
            Dim PoliceVioEst As Single

            Dim dment As Single
            Dim cterm As Single
            Dim yterm As Single
            Dim SelfHarmEst As Single
            Dim sterm As Single
            'dsb 2026/1/8
            Dim PredictionPardee As Single
            'Dim yearCivilWarPardee As Single = FirstYear + IY - 1
            Dim yearCivilWarPardee As Single = FirstYear
            Dim p As Single
            Dim observedPercentage As Single
            'dsb 2026/1/15
            Dim PredictionPopImp As Single





            '
            '   First Year 2: Read Historical State Failure Levels
            '   --------------------------------------------------

            '
            '   First Year 2:  State Failure Calculations
            '   ------------------------------------------------------------

            winfmor = 0
            For R% = 1 To NR%
                'jrs 2021/08/04 Exogenous Models
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("SFINTLWARALL", "", "", CStr(FirstYear + IY - 1), R%, context)
                    'jrs 2024/05/17
                    ReplaceVarWithExogenousForecastxReg("SFCIVILWARUCDP", "", "", CStr(FirstYear + IY - 1), R%, context)
                    'dsb 2026/1/9
                    'ReplaceVarWithExogenousForecastxReg("SFCIVILWARPARDEE", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If
                winfmor = winfmor + INFMOR(R%) * POP(R%)
            Next R%
            winfmor = AMAX(0.0001, winfmor / WPOP)

            '   First Year 2:  Driver terms for function
            '   ----------------------------------------
            'ttt 2019/10/08  TakeOut
            Array.Copy(SFINTLWARALL, ssfintlwarallLoc, NR + 1)
            'jrs 2024/05/17
            Array.Copy(SFCIVILWARUCDP, ssfcivilwarUCDPLoc, NR + 1)
            'dsb 2026/1/8
            Array.Copy(SFCIVILWARPARDEE, ssfcivilwarPardeeLoc, NR + 1)

            'jrs 2024/05/17
            LNINFMORWORLD = 0
            For R% = 1 To NR%
                LNINFMORWORLD = LNINFMORWORLD + Log(INFMOR(R%))
            Next
            LNINFMORWORLD = LNINFMORWORLD / NR

            For R% = 1 To NR%
                infmorterm = INFMOR(R%) / winfmor 'Normalized
                '        tradeterm = Log((X(R%) + M(R%)) / GDP(R%) * 100)  'Data are percent terms
                TradeTerm = (X(R%) + M(R%)) / GDP(R%) * 100  'Data are percent terms
                democterm = DEMOCPOLITY(R%)  'Data and model are 0-20
                '        GDPterm = Log(GDPPCP(R%))
                GDPterm = GDPPCP(R%)
                educ15term = EDYRSAG15(R%, 3)
                '       bbh 2011/08/19
                'ttt 2019/10/08  TakenOut ssfintlwarallLoc(R%) = SFINTLWARALL(R%)
                '        SFPredictionI(R%) = AMAX(0.001, 1.4757 + 1.0011 * Log(InfMorTerm) - _
                '            0.7414 * Log(tradeterm) + 0.0316 * DemocTerm)  'Original SF Project Eqn
                '        Prediction = _
                '            AMAX(0.001, sfpinstaby1(1) + sfpinstaby1(2) * GDPterm + _
                '                sfpinstaby1(3) * DEMOCterm + sfpinstaby1(4) * INFMORterm + _
                '                sfpinstaby1(5) * tradeterm)

                '                '    Call AnalFunc("State Failure from Regime Instability, year 1",
                '                'Array(GDPPCP(R%), democterm, infmorterm, TradeTerm, educ15term), Prediction,
                '                'Array("GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25")) 'jrs 2014/10/09 added DepVar Names to check order


                Call AnalFunc("State Failure from Regime Instability, year 1",
                New Single() {GDPPCP(R%), democterm, infmorterm, TradeTerm, educ15term}, Prediction,
                New String() {"GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25"}, context)

                Prediction = AMAX(0.001, Prediction)
                'jrs 2010/01/18 SFINSTABY1RI(R%) = SFINSTABY1(R%) / prediction
                ''If sfusehist < 0.5 Then
                ''    'jrs 2010/01/18 SFINSTABY1(R%) = AMAX(0.0001, Amin(1, prediction))
                ''End If

                '        Prediction = _
                'AMAX(0.001, sfpinstaball(1) + sfpinstaball(2) * GDPterm +
                '    sfpinstaball(3) * DEMOCterm + sfpinstaball(4) * INFMORterm +
                '    sfpinstaball(5) * TradeTerm)
                'Call AnalFunc("State Failure from Regime Instability, all years",
                '    Array(GDPPCP(R%), democterm, infmorterm, TradeTerm, educ15term), Prediction,
                '    Array("GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25")) 'jrs 2014/10/09 added DepVar Names to check order


                Call AnalFunc("State Failure from Regime Instability, all years",
                New Single() {GDPPCP(R%), democterm, infmorterm, TradeTerm, educ15term}, Prediction,
                New String() {"GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25"}, context)

                Prediction = AMAX(0.001, Prediction)
                SFINSTABALLRI(R%) = SFINSTABALL(R%) / Prediction
                If sfusehist < 0.5 Then
                    ' SFINSTABALL(R%) = AMAX(0.0001, Amin(1, Prediction))
                End If
                'bbh 2011/06/18 This looks very dangerous for run-to-run; if sfusehist <.05 changing 1st year variable used earlier than change

                '        Prediction = _
                'AMAX(0.001, sfpinstabmag(1) + sfpinstabmag(2) * GDPterm +
                '    sfpinstabmag(3) * DEMOCterm + sfpinstabmag(4) * INFMORterm +
                '    sfpinstabmag(5) * TradeTerm)
                'Call AnalFunc("State Failure from Regime Instability, magnitude",
                '    Array(GDPPCP(R%), democterm, infmorterm, TradeTerm, educ15term), Prediction,
                '    Array("GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25")) 'jrs 2014/10/09 added DepVar Names to check order


                Call AnalFunc("State Failure from Regime Instability, magnitude",
                New Single() {GDPPCP(R%), democterm, infmorterm, TradeTerm, educ15term}, Prediction,
                New String() {"GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25"}, context)



                Prediction = AMAX(0.001, Prediction)
                SFINSTABMAGRI(R%) = SFINSTABMAG(R%) / Prediction
                If sfusehist < 0.5 Then
                    'bbh 2011/06/18 better to have transient if use analytic only than to have run-to-run
                    ' SFINSTABMAG(R%) = AMAX(0.0001, Amin(1, Prediction))
                End If

                '        Prediction = _
                'AMAX(0.001, sfpintlwary1(1) + sfpintlwary1(2) * GDPterm +
                '    sfpintlwary1(3) * DEMOCterm + sfpintlwary1(4) * INFMORterm +
                '    sfpintlwary1(5) * TradeTerm)
                'Call AnalFunc("State Failure from Internal War, year 1",
                '    Array(GDPPCP(R%), democterm, infmorterm, TradeTerm, educ15term), Prediction,
                '    Array("GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25")) 'jrs 2014/10/09 added DepVar Names to check order

                Call AnalFunc("State Failure from Internal War, year 1",
                New Single() {GDPPCP(R%), democterm, infmorterm, TradeTerm, educ15term}, Prediction,
                New String() {"GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25"}, context)


                Prediction = AMAX(0.001, Prediction)
                'jrs 2010/01/18 SFINTLWARY1RI(R%) = SFINTLWARY1(R%) / prediction
                If sfusehist < 0.5 Then
                    'jrs 2010/01/18 SFINTLWARY1(R%) = AMAX(0.0001, Amin(1, prediction))
                End If

                '        Prediction = _
                'AMAX(0.001, sfpintlwarall(1) + sfpintlwarall(2) * GDPterm +
                '    sfpintlwarall(3) * DEMOCterm + sfpintlwarall(4) * INFMORterm +
                '    sfpintlwarall(5) * TradeTerm)
                'bbh 07/29/2011
                '        Call AnalFunc("State Failure from Internal War, all years", _
                '            Array(GDPPCP(R%), DEMOCterm, infmorterm, TradeTerm, educ15term), Prediction)
                'Call AnalFunc("State Failure from Internal War, all years",
                '    Array(GDPPCP(R%), democterm, INFMOR(R%), TradeTerm, educ15term), Prediction,
                '    Array("GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25")) 'jrs 2014/10/09 added DepVar Names to check order

                Call AnalFunc("State Failure from Internal War, all years",
                New Single() {GDPPCP(R%), democterm, INFMOR(R%), TradeTerm, educ15term}, Prediction,
                New String() {"GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25"}, context)


                'bbh 07/29/2011 end
                Prediction = AMAX(0.001, Prediction)
                'bbh 2011/06/18 begin
                ContiguityIntlWarAddI(R%) = ContiguityIntlWarAdd(R%)
                'If RegName$(R%) = "Azerbaijan" Then Stop
                SFINTLWARALLADD(R%) = SFINTLWARALL(R%) - Prediction
                SSFINTLWARALL(R%) = AMAX(0.001, SFINTLWARALL(R%)) 'dsb 2026/06/02 Fix for run to run issue
                'If R% = 75 Then Stop
                'dsb 2026/08/04
                ContiguityCivilWarUCDPAddI(R%) = ContiguityCivilWarUCDPAdd(R%)
                'jrs 2024/08/20 removed Ythbulge and GDPGRowth
                'jrs 2024/05/17 *** start ***
                'Call AnalFunc("State Failure from Internal War UCDP",
                'New Single() {INFMOR(R%), TradeTerm, YTHBULGE(R%) * 100, GDPR(R%), ContiguityIntlWarAddI(R%), DEMOCPOLYARCHY(R%) * 10, CSng((10 * DEMOCPOLYARCHY(R%)) ^ 2)}, Prediction,
                'New String() {"IMR", "Tradeopen", "Ythbulge", "Gdpgrowth_ma60", "Neighboringconflict", "Polyarchy", "PolyarchySqr"}, context)
                Call AnalFunc("State Failure from Internal War UCDP",
                New Single() {INFMOR(R%), TradeTerm, ContiguityCivilWarUCDPAddI(R%), DEMOCPOLYARCHY(R%) * 10, CSng((10 * DEMOCPOLYARCHY(R%)) ^ 2)}, Prediction,
                New String() {"IMR", "Tradeopen", "Neighboringconflict", "Polyarchy", "PolyarchySqr"}, context)

                Prediction = CSng(Exp(Prediction) / (1 + Exp(Prediction)))
                SFCIVILWARUCDPADD(R%) = CSng(SFCIVILWARUCDP(R%) / AMAX(Prediction, 0.000001))
                SFCIVILWARUCDPI(R%) = AMAX(0.001, SFCIVILWARUCDP(R%))
                SSFCIVILWARUCDP(R%) = AMAX(0.001, SFCIVILWARUCDP(R%))
                'jrs 2024/05/23
                'If GRPS(1, R%) = "Russian Federation" Then
                '    Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "SFCIVILWARUCDP.txt", append:=False)
                '    objWriter.WriteLine(ContiguityIntlWarAdd(R%).ToString)
                '    objWriter.Close()
                'End If
                'jrs 2024/05/17 *** end ***

                ''DSB 2026/1/15 *** start ****
                ''---------------------------------------------------------------
                ''Percent of Population Impacted calculated using OLS
                ''All drivers are lagged by one year
                ''---------------------------------------------------------------
                'If R% = 1 Or R% = 180 Then Stop
                'Call AnalFunc("State Failure Percent of Population Impacted (2026)",
                '              New Single() {POP(R%), SFCIVILWARPARDEE(R%), SVDTHSWOMENCHILDREN(R%), GOVEFFECT(R%)}, PredictionPopImp,
                '              New String() {"POP_Lag1", "SFCIVILWARPARDEE_Lag1", "SVDTHS_Lag1", "GOVEFFECT_Lag1"}, context)
                'SFCIVILWARPOPIMP(R%) = PredictionPOPIMP
                'SFCIVILWARPOPIMP(R%) = AMAX(0, Amin(100, SFCIVILWARPOPIMP(R%)))
                'SFCIVILWARPOPIMP(R%) = SFCIVILWARPOPIMP(R%)
                'SSFCIVILWARPOPIMP(R%) = SFCIVILWARPOPIMP(R%)
                ''DSB 2026/1/15 *** end ****

                'dsb 2026/1/8 *** start ***
                ContiguityCivilWarPardeeAddI(R%) = ContiguityCivilWarPardeeAdd(R%)

                Call AnalFunc("State Failure from Internal War Pardee Onset (2026)",
                              New Single() {INFMOR(R%), TradeTerm, ContiguityCivilWarPardeeAddI(R%), DEMOCPOLYARCHY(R%) * 10, CSng((10 * DEMOCPOLYARCHY(R%)) ^ 2), yearCivilWarPardee, CSng(yearCivilWarPardee ^ 2)}, PredictionPardee,
                              New String() {"IMR", "Tradeopen", "NeighboringconflictPardee", "Polyarchy", "PolyarchySqr", "Year", "YearSquared"}, context)

                PredictionPardee = CSng(Exp(PredictionPardee) / (1 + Exp(PredictionPardee)))

                SFCIVILWARPARDEEADD(R%) = CSng(SFCIVILWARPARDEE(R%) / AMAX(PredictionPardee, 0.000001))
                SFCIVILWARPARDEEI(R%) = AMAX(0.001, SFCIVILWARPARDEE(R%))
                SSFCIVILWARPARDEE(R%) = AMAX(0.001, SFCIVILWARPARDEE(R%)) 'dsb 2026/1/15
                'dsb 2026/1/8 *** end ***

                'dsb 2026/1/13*** start ***

                'p = SFCIVILWARPARDEE(R%) 'conflict probability (0 to 1)
                'observedPercentage = SFCIVILWARPOPIMP(R%) * 100 'observed percentage of population impacted (0 to 100) in base year
                'SFCIVILWARPOPIMPSHIFTBASE(R%) = observedPercentage - (100 * p)
                ''SFCIVILWARPOPIMP(R%) = observedPercentage / 100
                'SSFCIVILWARPOPIMP(R%) = SFCIVILWARPOPIMP(R%)
                'dsb 2026/1/13*** end ***

                'jrs 2025/08/27 *** start ***
                '   All but First 2: Politically Relevant Dyad Security Count
                '   ------------------------------------------------------------
                Dim conflictIndex As Single 'dsb 2026/08/03 After asking in the modeling pod meeting, Though it was estimated using the UCDP data, the switch is added to use the probability measure based on the switch as the coefficients are going to be very similar
                If sfcivilwarucdpsw = 2 Then
                    conflictIndex = SFINTLWARALL(R%)
                ElseIf sfcivilwarucdpsw = 3 Then
                    conflictIndex = SFCIVILWARPARDEE(R%)
                Else
                    conflictIndex = SFCIVILWARUCDP(R%)
                End If
                Call AnalFunc("Politically Relevant Dyad Security Count",
                New Single() {AvgDistanceAtoB(R%), wpsovcount, POWER(R%), POP(R%), LIFEXP(R%, 3), conflictIndex}, Prediction,
                New String() {"AvgDistanceAtoB", "Sovereign_Count_Series", "POWER", "Population", "LifeExpBothSexes", "SFCIVILWARUCDP"}, context)
                Prediction = CSng(AMAX(Prediction, 1))
                SPRDSECFX(R%) = Prediction
                SPRDSEC(R%) = PRDSEC(R%)
                'jrs 2025/08/27 *** end ***


                'bbh 2011/09/21
                SFINTLWARALLI(R%) = AMAX(0.001, SFINTLWARALL(R%))
                If sfusehist < 0.5 Then ' then use analytic function only, override historical data from preprocessor
                    '            SFINTLWARALL(R%) = AMAX(0.0001, Amin(1, Prediction))
                End If
                'bbh 2011/06/18 Above looks very dangerous for run-to-run;
                'if sfusehist <.05 changing 1st year variable used earlier than change
                'Also, have used history for contiguity; in next run will be different
                'bbh 2011/06/18 end
                '        Prediction = _
                'AMAX(0.001, sfpintlwarmag(1) + sfpintlwarmag(2) * GDPterm +
                '    sfpintlwarmag(3) * DEMOCterm + sfpintlwarmag(4) * INFMORterm +
                '    sfpintlwarmag(5) * TradeTerm)
                'bbh 2011/09/21
                '        Call AnalFunc("State Failure from Internal War, magnitude", _
                '            Array(GDPPCP(R%), democterm, infmorterm, TradeTerm, educ15term), prediction)
                '        prediction = AMAX(0.001, prediction)
                '        SFINTLWARMAGRI(R%) = SFINTLWARMAG(R%) / prediction
                SFINTLWARMAGI(R%) = AMAX(0.001, SFINTLWARMAG(R%))
                SSFINTLWARMAG(R%) = SFINTLWARMAG(R%)
                'If RegName$(R%) = "Albania" Then Stop
                '        If sfusehist < 0.5 Then
                'bbh 2011/06/18 better to have transient if use analytic only than to have run-to-run
                'SFINTLWARMAG(R%) = AMAX(0.0001, Amin(1, Prediction))
                '        End If

                'bbh 2011/04/13 start
                '   First Year 2:  Goverment and Adjustment factors
                '   -----------------------------------------------------------

                '        confforsw = 1

                'bbh 2011/04/30 start
                energydepend(R%) = ENX(R%) * ENPRI(R%) / GDP(R%)


                'bbh 2011/08/19
                '        Call AnalFunc("Governance Corruption (2005)", _
                'Array(GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)), Prediction)
                '    Call AnalFunc("GOVCORRUPTversus GDPPCP, DEMOCPOLITY, GEM, Ex Export Depend (2010)",
                'Array(GDPPCP(R%), DEMOCPOLITY(R%), GEM(R%), energydepend(R%)), Prediction,
                'Array("GDPPCP[0](0) (Linear)", "DEMOCPOLITY[0](0) (Linear)", "GEM[0](0) (Linear)", "EnExValPerGDP(0) (Linear)")) 'jrs 2014/10/09 added DepVar Names to check order

                Call AnalFunc("GOVCORRUPTversus GDPPCP, DEMOCPOLITY, GEM, Ex Export Depend (2010)",
                New Single() {GDPPCP(R%), DEMOCPOLITY(R%), GEM(R%), energydepend(R%)}, Prediction,
                New String() {"GDPPCP[0](0) (Linear)", "DEMOCPOLITY[0](0) (Linear)", "GEM[0](0) (Linear)", "EnExValPerGDP(0) (Linear)"}, context)


                'bbh 2011/04/30 end
                If confforsw = 1 Then
                    If sfcivilwarucdpsw = 1 Then 'dsb 2026/07/28
                        Prediction = Prediction - CSng(0.5) * ssfcivilwarUCDPLoc(R%)
                    ElseIf sfcivilwarucdpsw = 3 Then
                        Prediction = Prediction - CSng(0.5) * ssfcivilwarPardeeLoc(R%)
                    Else
                        Prediction = Prediction - CSng(0.5) * ssfintlwarallLoc(R%)
                    End If
                End If
                'jrs 2024/02/29
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("GOVCORRUPT", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                'To be compatible Economy; have used simpler function for DataEcon hole filling
                GovCorruptAdd(R%) = GOVCORRUPT(R%) - Prediction

                '        energyterm = ENX(R%) * WEPBYEAR / GDP(R%)
                'bbh 2011/04/30
                'Call AnalFunc("Governance Effectiveness (2005)",
                '    Array(GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)), Prediction,
                '    Array("GDPPCP", "DEMOCPOLITY", "EnExDep")) 'jrs 2014/10/09 added DepVar Names to check order 'Using bivariate in preprocessor


                Call AnalFunc("Governance Effectiveness (2005)",
                New Single() {GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)}, Prediction,
                New String() {"GDPPCP", "DEMOCPOLITY", "EnExDep"}, context)


                If confforsw = 1 Then
                    If sfcivilwarucdpsw = 1 Then 'dsb 2026/07/28
                        Prediction = Prediction - CSng(0.08) * ssfcivilwarUCDPLoc(R%)
                    ElseIf sfcivilwarucdpsw = 3 Then
                        Prediction = Prediction - CSng(0.08) * ssfcivilwarPardeeLoc(R%)
                    Else
                        Prediction = Prediction - CSng(0.08) * ssfintlwarallLoc(R%)
                    End If
                End If
                'bbh 2011/09/21 Function now estimated with forecasts (0-5)/not data (-2.5 to 2/5)
                'Prediction = AMAX(0, Amin(5, Prediction + 2.5))
                Prediction = AMAX(0, Amin(5, Prediction))
                'jrs 2024/02/29
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("GOVEFFECT", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If
                'If R% = 1 Or R% = 180 Then Stop
                GovEffectAdd(R%) = GOVEFFECT(R%) - Prediction
                'dsb 2026/1/15
                GOVEFFECT_Lag(R%) = GOVEFFECT(R%)
                'If RegName(R%) = "Austria" Then Stop
                'bbh 2011/04/30
                'Call AnalFunc("Governance Regulatory Quality (2005)",
                '    Array(GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)), Prediction,
                '    Array("GDPPCP", "DEMOCPOLITY", "EnExDep")) 'jrs 2014/10/09 added DepVar Names to check order 'Using bivariate in preprocessor

                Call AnalFunc("Governance Regulatory Quality (2005)",
                New Single() {GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)}, Prediction,
                New String() {"GDPPCP", "DEMOCPOLITY", "EnExDep"}, context)


                If confforsw = 1 Then
                    If sfcivilwarucdpsw = 1 Then 'dsb 2026/07/28
                        Prediction = Prediction - CSng(0.2) * ssfcivilwarUCDPLoc(R%)
                    ElseIf sfcivilwarucdpsw = 3 Then
                        Prediction = Prediction - CSng(0.2) * ssfcivilwarPardeeLoc(R%)
                    Else
                        Prediction = Prediction - CSng(0.2) * ssfintlwarallLoc(R%)
                    End If
                End If
                'bbh 2011/09/21 Function now estimated with forecasts (0-5)/not data (-2.5 to 2/5)
                'Prediction = AMAX(0, Amin(5, Prediction + 2.5))
                Prediction = AMAX(0, Amin(5, Prediction))
                GovRegQualAdd(R%) = GOVREGQUAL(R%) - Prediction
                'bbh 2011/04/30

                'mti 2014/11/14 -start
                'jrs 2024/06/27 TbName$ = "GDP/Capita (PPP 2011) Versus Cost of Starting Business (" & CStr(BaseYear) & ") Log"
                'TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
                'Call XYTABL(TbName$, GDPPCP(R%), Prediction)

                Call XYTABL(TFNameCostStartBuss, GDPPCP(R), Prediction, context)

                GovBusCostShift(R%) = GOVBUSCOST(R%) - Prediction



                'mti 2014/11/14 - end

                'If RegName(R%) = "Singapore" Then Stop

                'mti 2014/11/27 -start

                'mti 2015/01/20 - start
                'TbName$ = "GDP/Capita (PPP 2011) Versus Business Index (" & CStr(BaseYear) & ") Log"
                'jrs 2024/06/27 TbName$ = "GDP/Capita (PPP 2011) Versus Business Regulation Index (" & CStr(BaseYear) & ") Log"
                'mti 2015/01/20 - end

                'TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
                Call XYTABL(TFNameBusinessRegIdx, GDPPCP(R%), Prediction, context)

                'mti 2015/03/04 - start
                GovBusRegIndShift(R%) = GOVBUSREGIND(R%) - Prediction
                'mti 2015/03/04 - end

                'mti 2014/11/27 - end

                'jrs 2013/11/28
                '        Call AnalFunc("Economic Freedom (2005)", _
                '            Array(GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)), Prediction) 'Using bivariate in preprocessor



                'Call AnalFunc("Economic Freedom (2010)",
                '    Array(GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)), Prediction,
                '    Array("GDPPCP (Log)", "DEMOCPOLITY (Linear)", "ENX*ENPRI/GDP(Linear)")) 'jrs 2014/10/09 added DepVar Names to check order 'Using bivariate in preprocessor


                Call AnalFunc("Economic Freedom (2010)",
                New Single() {GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)}, Prediction,
                New String() {"GDPPCP (Log)", "DEMOCPOLITY (Linear)", "ENX*ENPRI/GDP(Linear)"}, context)


                '        prediction = AMAX(0.01, Amin(10, prediction))
                If confforsw = 1 Then
                    If sfcivilwarucdpsw = 1 Then 'dsb 2026/07/28
                        Prediction = Prediction - CSng(0.3) * ssfcivilwarUCDPLoc(R%)
                    ElseIf sfcivilwarucdpsw = 3 Then
                        Prediction = Prediction - CSng(0.3) * ssfcivilwarPardeeLoc(R%)
                    Else
                        Prediction = Prediction - CSng(0.3) * ssfintlwarallLoc(R%)
                    End If
                End If
                econfreeri(R%) = ECONFREE(R%) / AMAX(0.01, Prediction)

                educ15term = EDYRSAG15(R%, 3)
                If context.HistoricMode Then
                    '            Call AnalFunc("Polity Democracy with GDPPCP and Surv-SE (1960)", _
                    '                Array(GDPPCP(R%), SURVSE(R%, WVSCohorts)), Prediction)
                    'bbh 2011/05/02
                    'bbh 2011/08/19
                    ' Call AnalFunc("Polity Democ versus Edyears, YthBulge, ln(EnExDep) 2005", _
                    'Array(educ15term, YTHBULGE(R%), energydepend(R%)), Prediction)
                    '    Call AnalFunc("Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)",
                    'Array(GEM(R%), YTHBULGE(R%), energydepend(R%)), Prediction,
                    'Array("GEM[0](0) (Linear)", "YTHBULGE[0](0) (Linear)", "EnExValPerGDP(0) (Linear)")) 'jrs 2014/10/09 added DepVar Names to check order

                    Call AnalFunc("Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)",
                    New Single() {GEM(R%), YTHBULGE(R%), energydepend(R%)}, Prediction,
                    New String() {"GEM[0](0) (Linear)", "YTHBULGE[0](0) (Linear)", "EnExValPerGDP(0) (Linear)"}, context)



                    '            Call AnalFunc("Polity Democ with EdYearsAg15, YthBulge, EnExDep (2005)", _
                    'Array(educ15term, YTHBULGE(R%), energydepend(R%)), Prediction)
                    'Last, WVSCohorts cohort is total adult population
                    democcomp(R%) = AMAX(1, Amin(20, Prediction))
                    ExpPrediction = Prediction
                Else
                    'bbh 2011/06/23
                    'Call AnalFunc("Polity Democ with GDPPCP and EdYears 15 (2005)",
                    '    Array(GDPPCP(R%), educ15term), ExpPrediction,
                    '    Array("GDPPCP", "Educ15")) 'jrs 2014/10/09 added DepVar Names to check order

                    Call AnalFunc("Polity Democ with GDPPCP and EdYears 15 (2005)",
                    New Single() {GDPPCP(R%), educ15term}, ExpPrediction,
                    New String() {"GDPPCP", "Educ15"}, context)


                    'bbh 2011/05/02
                    'bbh 2011/08/19
                    '            Call AnalFunc("Polity Democ versus Edyears, YthBulge, ln(EnExDep) 2005", _
                    'Array(educ15term, YTHBULGE(R%), energydepend(R%)), Prediction)
                    '    Call AnalFunc("Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)",
                    'Array(GEM(R%), YTHBULGE(R%), energydepend(R%)), Prediction,
                    'Array("GEM[0](0) (Linear)", "YTHBULGE[0](0) (Linear)", "EnExValPerGDP(0) (Linear)")) 'jrs 2014/10/09 added DepVar Names to check order

                    Call AnalFunc("Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)",
                    New Single() {GEM(R%), YTHBULGE(R%), energydepend(R%)}, Prediction,
                    New String() {"GEM[0](0) (Linear)", "YTHBULGE[0](0) (Linear)", "EnExValPerGDP(0) (Linear)"}, context)


                    '            Call AnalFunc("Polity Democ with EdYearsAg15, YthBulge, EnExDep (2005)", _
                    'Array(educ15term, YTHBULGE(R%), energydepend(R%)), Prediction)
                    democcomp(R%) = AMAX(1, Amin(20, Prediction))
                End If
                'bbh 2011/06/23
                DEMOCEXP(R%) = AMAX(1, Amin(ExpPrediction, 20))
                democpolityri(R%) = DEMOCPOLITY(R%) / AMAX(0.01, democcomp(R%))
                'If RegName(R%) = "Chad" Then Stop

                'bbh 2011/04/13 end

                'dkb 2020/02/24 - START. Adding logic for GII. Note: The useless coefficients and exponents (set to 1) are placeholders for missing components (i.e. political participation)
                'jrs 2020/04/02 changed to SCBR
                Dim AdolescentBirthRate = AMAX(1, 3.28 * SCBR(R%) - 19.9) 'dkb 2021/01/12 changed from 0.1 to 1
                Dim MatMortTerm = AMAX(0.001, 1 - AMAX(((Amin(2000, MATMORTRATIO(R%)) - 3) / (2000 - 3)), 0.001)) 'dkb 2020/04/29 'dkb 2021/01/12 changed from 1000 to 2000

                'dkb 2020/04/29 Dim GeoMeanFem = ((((10 / AMAX(10, Amin(1000, sMATMORTRATIO(R%)))) * (1 / AdolescentBirthRate)) ^ (1 / 2)) * ((1 * sEDSECPER(R%, Female) / 100) ^ 1) * (LABPARR(R%, Female) / 100)) ^ (1 / 3)
                Dim GeoMeanFem = ((((MatMortTerm) * (1 / AdolescentBirthRate)) ^ (1 / 2)) * ((1 * EDSECPER(R%, Female) / 100) ^ 1) * (LABPARR(R%, Female) / 100)) ^ (1 / 3)
                Dim GeoMeanMale = (((1 * (1 * EDSECPER(R%, Male) / 100)) ^ 1) * (LABPARR(R%, Male) / 100)) ^ (1 / 3)

                Dim HarmonicMean = (((GeoMeanFem ^ (-1)) + (GeoMeanMale ^ (-1))) / 2) ^ (-1)

                'dkb 2020/04/29 Dim ArithMeanHealth = ((((10 / AMAX(10, Amin(1000, MATMORTRATIO(R%)))) * (1 / AdolescentBirthRate)) ^ (1 / 2)) + 1) / 2
                Dim ArithMeanHealth = ((((MatMortTerm) * (1 / AdolescentBirthRate)) ^ (1 / 2)) + 1) / 2
                Dim ArithMeanEmpower = (((1 * EDSECPER(R%, Female) / 100) ^ 1) + ((1 * (1 * EDSECPER(R%, Male) / 100)) ^ 1)) / 2
                Dim ArithMeanLaborPar = ((LABPARR(R%, Female) / 100) + (LABPARR(R%, Male) / 100)) / 2

                Dim GeoArithMean = (ArithMeanHealth * ArithMeanEmpower * ArithMeanLaborPar) ^ (1 / 3)

                GIII(R%) = CSng(1 - (HarmonicMean / GeoArithMean))

                If GII(R%) = 0 Or GII(R%) = 1 Then
                    GII(R%) = GIII(R%)
                End If

                GIIShift(R%) = GII(R%) - GIII(R%)
                'dkb 2020/02/24 *** end ***

                'kbn 2018/04/27 Start-Violence Model
                'kbn 2018/06/26- Start- Enter descriptions in code for violence model below
                '-----------Violence model (First Year)-----------------------
                'Violence model in the first year proceeds in following sequence
                '1. Create Population terms for total deaths using the age structure variables in the population model
                '2. Normalize deaths in the violence model to those in the population model if normalization switch is on or if violence forward linkage is on
                '3. Define shift factors for the violence model
                '4. Compute total number of deaths using the death rates and age structure calculated in 1. above
                '5. Recalculate total death rate using total number of deaths
                '6. Compute the HOMICIDE index for the first year
                '7. Save variable values for the first year
                'kbn 2018/06/26 End

                '1. kbn 2018/05/16 create population terms for total deaths computation using 'agedst' from POPULAT
                AdultMaleTerm = ((agedst(4, R%, 1) + agedst(5, R%, 1) + agedst(6, R%, 1) + agedst(7, R%, 1) + agedst(8, R%, 1) + agedst(9, R%, 1) + agedst(10, R%, 1) +
                agedst(11, R%, 1) + agedst(12, R%, 1) + agedst(13, R%, 1) + agedst(14, R%, 1) + agedst(15, R%, 1) + agedst(16, R%, 1) + agedst(17, R%, 1) + agedst(18, R%, 1) + agedst(19, R%, 1) + agedst(20, R%, 1) + agedst(21, R%, 1)) * 1000000)
                WomenTerm = ((agedst(4, R%, 2) + agedst(5, R%, 2) + agedst(6, R%, 2) + agedst(7, R%, 2) + agedst(8, R%, 2) + agedst(9, R%, 2) + agedst(10, R%, 2) +
                agedst(11, R%, 2) + agedst(12, R%, 2) + agedst(13, R%, 2) + agedst(14, R%, 2) + agedst(15, R%, 2) + agedst(16, R%, 2) + agedst(17, R%, 2) + agedst(18, R%, 2) + agedst(19, R%, 2) + agedst(20, R%, 2) + agedst(21, R%, 2)) * 1000000)
                ChildrenTerm = (agedst(0, R%, 1) + agedst(0, R%, 2) + agedst(1, R%, 1) + agedst(1, R%, 2) + agedst(2, R%, 1) + agedst(2, R%, 2) + agedst(3, R%, 1) + agedst(3, R%, 2)) * 1000000
                poptotalterm = (AdultMaleTerm + WomenTerm + ChildrenTerm)
                'kbn 2018/05/16-End


                '2. kbn 2018/05/31-Start- Normalize violence deaths to Intentional Injuries in the Health Model
                ' AdjustedViolenceTerm  is the total number of deaths from intentional injuries
                ' SVTerm is the calculated value of societal violence deaths in the violence model
                'kbn 2018/06/26-Start Introduce normalization switch below


                If svvionormsw > 0 Or svtohlsw > 0 Then


                    AdjustedViolenceTerm = (SDEATHIntIj(R%) * 1000000)
                    SVTerm = (((SSVDTHSOTHERINTERPERSON(R%) / 100000) * AdultMaleTerm + (SSVDTHSWOMEN(R%) / 100000) * WomenTerm + (SSVDTHSCHIL(R%) / 100000) * ChildrenTerm _
                                                      + (SSVDTHSCONFLICT(R%) / 100000) * poptotalterm + (SSVDTHSPOLICS(R%) / 100000) * poptotalterm + (SSVDTHSSELFHARM(R%) / 100000) * poptotalterm) / poptotalterm) * 100000

                    SVDTHSCONFLICT(R%) = CSng(((AdjustedViolenceTerm * ((SVDTHSCONFLICT(R%) * poptotalterm) / (SVTerm * poptotalterm))) / poptotalterm) * 100000)
                    SVDTHSOTHERINTERPERSON(R%) = CSng(((AdjustedViolenceTerm * ((SVDTHSOTHERINTERPERSON(R%) * AdultMaleTerm) / (SVTerm * poptotalterm))) / AdultMaleTerm) * 100000)
                    SVDTHSWOMENCHILDREN(R%) = CSng(((AdjustedViolenceTerm * ((SVDTHSWOMENCHILDREN(R%) * (WomenTerm + ChildrenTerm)) / (SVTerm * poptotalterm))) / (WomenTerm + ChildrenTerm)) * 100000)
                    SVDTHSWOMEN(R%) = CSng(((AdjustedViolenceTerm * ((SVDTHSWOMEN(R%) * WomenTerm) / (SVTerm * poptotalterm))) / WomenTerm) * 100000)
                    SVDTHSCHIL(R%) = CSng(((AdjustedViolenceTerm * ((SVDTHSCHIL(R%) * ChildrenTerm) / (SVTerm * poptotalterm))) / ChildrenTerm) * 100000)
                    SVDTHSPOLICS(R%) = CSng(((AdjustedViolenceTerm * ((SVDTHSPOLICS(R%) * poptotalterm) / (SVTerm * poptotalterm))) / poptotalterm) * 100000)
                    SVDTHSSELFHARM(R%) = CSng(((AdjustedViolenceTerm * ((SVDTHSSELFHARM(R%) * poptotalterm) / (SVTerm * poptotalterm))) / poptotalterm) * 100000)

                End If
                'kbn 2018/05/31 End
                'kbn 2018/06/26 End
                'SOCIETALVIOLENCEDEATHS(R%) = (((SVDTHSOTHERINTERPERSON(R%) / 100000) * AdultMaleterm + (SVDTHSWOMEN(R%) / 100000) * WomenTerm + (SVDTHSCHIL(R%) / 100000) * ChildrenTerm _
                ' + (SVDTHSCONFLICT(R%) / 100000) * poptotalterm + (SVDTHSPOLICS(R%) / 100000) * poptotalterm + (SVDTHSSELFHARM(R%) / 100000) * poptotalterm) / poptotalterm) * 100000


                '3. kbn 2018/04/26-Start- Define shift factors for violence model below

                '1. Conflict
                'Upper limit based on Libya's value for 2011
                'kbn 2018/01/06- Start- Change this formulation to one that uses internal war all
                'Call AnalFunc("Conflict deaths (Deaths per 100000)", Array(SSFINTLWARMAG(R%) ^ 2, GOVEFFECT(R%)), ConflictEst, Array("SFINTLWARMAG", "GOVEFFECT"))
                'Call AnalFunc("Conflict Deaths per 100000", Array(ssfintlwarallLoc(R%)), ConflictEst, Array("Probability of internal war"))
                'ConflictEst = AMAX(-0.3137 + 8.2297 * SSFINTLWARMAG(R%), 0.0000001) 'dkb 2019/01/22 - updating conflict deaths to be driven by interal war magnitude
                'ConflictEst = AMAX(0.0991 + 0.5501 * SSFINTLWARMAG(R%), 0.0000001) 'dkb 2019/01/22 - updating conflict deaths to be driven by interal war magnitude and to log (ln) DV
                'ConflictEst = Amin(AMAX(Exp(ConflictEst), 0.000001), 636) 'dkb 2019/01/22 - Unlogging conflict estimate

                'Call XYTABL("Conflict Deaths per 100000 (natural log)", SSFINTLWARMAG(R%), ConflictEst)

                Call XYTABL("Conflict Deaths per 100000 (natural log)", SSFINTLWARMAG(R%), ConflictEst, context)

                ConflictEst = CSng(Exp(CDbl(ConflictEst)) - CSng(1)) 'dkb 2019/01/22 - Unlogging conflict estimate and removing the +1 applied during regression to bypass logs of zero
                ConflictShift(R%) = CSng(SVDTHSCONFLICT(R%) - Amin(AMAX(ConflictEst, 0.000001), 636))
                'kbn 2018/01/06-End

                '2. Homicides
                'Upper limit based on Colombia's 1992 value
                'Call XYTABL("HomicideDeaths vs GINI", GINIDOM(R%), HomicideEst)
                'Call AnalFunc("Interpersonal deaths per 100000 people", Array(GINIDOM(R%) * 100, YTHBULGE(R%) * 100), HomicideEst, Array("GINIDOM", "Youthbulge"))

                Call AnalFunc("Interpersonal deaths per 100000 people",
                    New Single() {GINIDOM(R%) * 100, YTHBULGE(R%) * 100}, HomicideEst,
                    New String() {"GINIDOM", "Youthbulge"}, context)

                HomicideShift(R%) = CSng(SVDTHSOTHERINTERPERSON(R%) / (Amin(AMAX(Exp(HomicideEst), 0.000001), 208)))

                '3. Vulnerable populations (Women and Children)
                'Upper limit based on South Africa's 2003 value
                'Call AnalFunc("Interpersonal deaths per 100000 people", Array(GINIDOM(R%) * 100, YTHBULGE(R%) * 100), WomenandChilEst, Array("GINIDOM", "Youthbulge"))

                Call AnalFunc("Interpersonal deaths per 100000 people",
                    New Single() {GINIDOM(R%) * 100, YTHBULGE(R%) * 100}, WomenandChilEst,
                    New String() {"GINIDOM", "Youthbulge"}, context)

                WomenandChilshift(R%) = CSng(SVDTHSWOMENCHILDREN(R%) / (Amin(AMAX(Exp(WomenandChilEst), 0.000001), 21)))

                '3a Violence against women
                ' Call AnalFunc("Interpersonal deaths per 100000 people", Array(GINIDOM(R%) * 100, YTHBULGE(R%) * 100), WomenEst, Array("GINIDOM", "Youthbulge"))

                Call AnalFunc("Interpersonal deaths per 100000 people",
                    New Single() {GINIDOM(R%) * 100, YTHBULGE(R%) * 100}, WomenEst,
                    New String() {"GINIDOM", "Youthbulge"}, context)

                Womenshift(R%) = CSng(SVDTHSWOMEN(R%) / (Amin(AMAX(Exp(WomenEst), 0.000001), 21)))

                '3a Violence against children
                'Call AnalFunc("Interpersonal deaths per 100000 people", Array(GINIDOM(R%) * 100, YTHBULGE(R%) * 100), ChilEst, Array("GINIDOM", "Youthbulge"))


                Call AnalFunc("Interpersonal deaths per 100000 people",
                    New Single() {GINIDOM(R%) * 100, YTHBULGE(R%) * 100}, ChilEst,
                    New String() {"GINIDOM", "Youthbulge"}, context)

                ChilShift(R%) = CSng(SVDTHSCHIL(R%) / (Amin(AMAX(Exp(ChilEst), 0.000001), 21)))

                '4. Police violence
                'Upper limit based on Trinidad and Tobago's 2010 value
                'Call AnalFunc("PoliceViolenceDeaths", Log(GDPPC(R%)), CorruptEST)

                'Call AnalFunc("PoliceViolenceDeaths (Deaths per 100000)", Array(SVDTHSOTHERINTERPERSON(R%), GOVCORRUPT(R%) / 10), PoliceVioEst, Array("Log(Homicides)", "Corruption"))


                Call AnalFunc("PoliceViolenceDeaths (Deaths per 100000)",
                    New Single() {SVDTHSOTHERINTERPERSON(R%), GOVCORRUPT(R%) / 10}, PoliceVioEst,
                    New String() {"Log(Homicides)", "Corruption"}, context)

                'Somalia was creating a problem for this calculation. Hence limiting the estimated value to 3.
                PoliceShift(R%) = CSng(SVDTHSPOLICS(R%) / (Amin(AMAX(Exp(Amin(PoliceVioEst, 3)), 0.000001), 2)))

                '5. Self harm
                'Upper limit based on Russia's 1994 value
                'kbn 2018/05/17 Start Brought in new code from DKB for self harm
                'Call AnalFunc("SelfHarmDeaths vs Edyrsage15 vs interpersonalwomenandChildren", Array(SVDTHSWOMENCHILDREN(R%), EDYRSAG15(R%, 3)), SelfHarmEst, Array("InterpersonalWomenandChil", "EdYrsAge15"))
                'kbn 2018/05/17- End
                'kbn 2018/06/26-Start- Replace Mental health with new function
                dment = CSng(AMAX(SDEATHMntlHealth(R%) / POP(R%) * 1000, 0.1))
                'Call XYTABL("WomenandChilDeaths(per 100000) vs MentalHealth deaths (Log)", dment, sterm)
                Call XYTABL("WomenandChilDeaths(per 100000) vs MentalHealth deaths (Log)", dment, sterm, context)

                cterm = CSng(AMAX(Amin(sterm / 4.36, 4.2), 0))
                yterm = 9.5
                'Call AnalFunc("Self Harm Deaths per 100000 people", Array(POPMEDAGE(R%), SVDTHSWOMENCHILDREN(R%)), SelfHarmEst, Array("POPMEDAGE", "SVDTHSWOMENCHILDREN"))
                SelfHarmEst = yterm + cterm * SVDTHSWOMENCHILDREN(R%)
                SelfHarmShift(R%) = CSng(SVDTHSSELFHARM(R%) / (Amin(AMAX(SelfHarmEst, 0.0000001), 53)))
                'kbn 2018/04/26- End

                'kbn 2018/05/17-Start- Compute estimates below for each
                ConflictEstimate(R%) = CSng(Amin(AMAX(Exp(ConflictEst), 0.000001), 636))
                HomicideEstimate(R%) = CSng(Amin(AMAX(Exp(HomicideEst), 0.000001), 208))
                WomenandChilEstimate(R%) = CSng(Amin(AMAX(Exp(WomenandChilEst), 0.000001), 21))
                PoliceEstimate(R%) = CSng(Amin(AMAX(Exp(PoliceVioEst), 0.000001), 2))
                SelfHarmEstimate(R%) = SelfHarmEst
                'kbn 2018/05/17-End

                'kbn 2018/05/17-Start- save value for estimates estimates below for each violence category
                sConflictEstimate(R%) = ConflictEstimate(R%)
                sHomicideEstimate(R%) = HomicideEstimate(R%)
                sWomenandChilEstimate(R%) = HomicideEstimate(R%)
                sPoliceEstimate(R%) = PoliceEstimate(R%)
                sSelfHarmEstimate(R%) = SelfHarmEstimate(R%)
                'kbn 2018/05/17-End

                '4. kbn 2018/05/17-Start- Compute total number of deaths for all violence variable
                'kbn 2018/11/26-Start- Added minimum of 0 to variables below
                SVDTHSOTHERINTERTOT(R%) = AMAX(((SVDTHSOTHERINTERPERSON(R%) / 100000) * AdultMaleTerm) + svdthsadd(R%, 5), 0)
                SVDTHSPOLICSTOT(R%) = AMAX(((SVDTHSPOLICS(R%) / 100000) * poptotalterm) + svdthsadd(R%, 3), 0)
                SVDTHSCHILTOT(R%) = AMAX(((SVDTHSCHIL(R%) / 100000) * ChildrenTerm) + svdthsadd(R%, 4), 0)
                SVDTHSWOMENTOT(R%) = AMAX(((SVDTHSWOMEN(R%) / 100000) * WomenTerm) + svdthsadd(R%, 4), 0)
                SVDTHSSELFHARMTOT(R%) = AMAX(((SVDTHSSELFHARM(R%) / 100000) * poptotalterm) + svdthsadd(R%, 1), 0)
                SVDTHSCONFLICTTOT(R%) = AMAX(((SVDTHSCONFLICT(R%) / 100000) * poptotalterm) + svdthsadd(R%, 2), 0)
                SVDTHSSOCIETALVIOLENCETOT(R%) = AMAX(SVDTHSOTHERINTERTOT(R%) + SVDTHSPOLICSTOT(R%) + SVDTHSCHILTOT(R%) + SVDTHSWOMENTOT(R%) + SVDTHSSELFHARMTOT(R%) + SVDTHSCONFLICTTOT(R%) + svdthsadd(R%, 6), 0)
                'kbn 2018/05/17-End-
                '

                '5. kbn 2018/06/01-Start- Recalculate total death rate
                'kbn 2018/11/26-Add minimum values of 0 to the below
                SVDTHSCONFLICT(R%) = AMAX((SVDTHSCONFLICTTOT(R%) / poptotalterm) * 100000, 0)
                SVDTHSOTHERINTERPERSON(R%) = AMAX((SVDTHSOTHERINTERTOT(R%) / AdultMaleTerm) * 100000, 0)
                SVDTHSPOLICS(R%) = AMAX((SVDTHSPOLICSTOT(R%) / poptotalterm) * 100000, 0)
                SVDTHSCHIL(R%) = AMAX((SVDTHSCHILTOT(R%) / ChildrenTerm) * 100000, 0)
                SVDTHSWOMEN(R%) = AMAX((SVDTHSWOMENTOT(R%) / WomenTerm) * 100000, 0)
                SVDTHSSELFHARM(R%) = AMAX((SVDTHSSELFHARMTOT(R%) / poptotalterm) * 100000, 0)
                SOCIETALVIOLENCEDEATHS(R%) = AMAX((SVDTHSSOCIETALVIOLENCETOT(R%) / poptotalterm) * 100000, 0)
                'kbn 2018/06/01-End-
                'kbn 2018/11/26-End-


                '6. kbn 2018/05/17-Start- Societal violence index for the first year
                'kbn 2018/09/25 Start- Revise HOMICIDE INDEX below
                'HOMICIDEINDEX(R%) = (SVDTHSCONFLICT(R%) * svindexm(R%, 2) + (SVDTHSOTHERINTERPERSON(R%) * svindexm(R%, 5)) + (SVDTHSWOMEN(R%) * svindexm(R%, 4)) + (SVDTHSCHIL(R%) * svindexm(R%, 4)) + (SVDTHSPOLICS(R%) * svindexm(R%, 3)) / _
                '(svindexm(R%, 5) + svindexm(R%, 2) + svindexm(R%, 3) + svindexm(R%, 4)))
                HOMICIDEINDEX(R%) = ((SVDTHSCONFLICTTOT(R%) * svindexm(R%, 2) + (SVDTHSOTHERINTERTOT(R%) * svindexm(R%, 5)) + (SVDTHSWOMENTOT(R%) * svindexm(R%, 4)) + (SVDTHSCHILTOT(R%) * svindexm(R%, 4)) + (SVDTHSPOLICSTOT(R%) * svindexm(R%, 3)) /
     (svindexm(R%, 5) + svindexm(R%, 2) + svindexm(R%, 3) + svindexm(R%, 4)))) / POP(R%)


                'kbn 2018/09/25 Now divide homicide index by max homicides

                HOMICIDEINDEX(R%) = HOMICIDEINDEX(R%) / MaxHomicides

                'kbn 2018/05/17-End
                'kbn 2018/09/25-End

                '7. kbn 2018/05/17-Start- Saved values for each violence category
                SSVDTHSCONFLICT(R%) = SVDTHSCONFLICT(R%)
                SSVDTHSOTHERINTERPERSON(R%) = SVDTHSOTHERINTERPERSON(R%)
                SSVDTHSOTHERINTERPERSON_Pop(R%) = SVDTHSOTHERINTERPERSON(R%)
                SSVDTHSWOMENCHILDREN(R%) = SVDTHSWOMENCHILDREN(R%)
                'dsb 2026/1/15 SVDTHSWOMENCHILDREN(R%) lagged value by one year
                'If R% = 1 Or R% = 180 Then Stop
                SVDTHSWOMENCHILDREN_Lag(R%) = SVDTHSWOMENCHILDREN(R%)
                SSVDTHSSELFHARM(R%) = SVDTHSSELFHARM(R%)
                SSVDTHSPOLICS(R%) = SVDTHSPOLICS(R%)
                SSOCIETALVIOLENCEDEATHS(R%) = SOCIETALVIOLENCEDEATHS(R%)
                SHOMICIDEINDEX(R%) = HOMICIDEINDEX(R%)
                SSVDTHSWOMEN(R%) = SVDTHSWOMEN(R%)
                SSVDTHSCHIL(R%) = SVDTHSCHIL(R%)
                'kbn 2018/05/17-End-
            Next R%

            'JAM 2019-08-26
            Call IfsDomPolAllSteps(context)

        End Sub

        Public Sub IFsDomPol1AllButFirstYear(context As IForecastContext)

            '
            '   ALL TIME STEPS BUT FIRST
            '
            '   **************

            '   All but First 1:  Value Change
            '   ------------------------------
            Dim SwingStateNames(9) As String
            Dim WDemoc As Single
            Dim SwingState As Boolean
            Dim RegName(9) As String
            Dim Swgsts As Short
            Dim DemocImpMax As Single
            Dim DemocWaveShift As Single
            Dim DEMOCWVDIR As Single
            Dim DemocImpLead As Single
            Dim MaxDemocShift As Single
            Dim EffectMul As Single
            Dim Prediction As Single
            Dim SE As Single
            Dim ExpPrediction As Single
            Dim EffectSE As Single
            Dim FreeComp As Single
            Dim FreeComp2 As Single
            Dim educ15termfem As Single
            Dim educ15termi As Single
            Dim educ15termfemi As Single
            Dim FreeMul As Single
            Dim CorruptAdd As Single
            Dim EffectAdd As Single
            Dim TbName As String
            Dim govbushiftyear As Single
            Dim TableName As String
            Dim GEMComp As Single
            Dim GEMComp2 As Single
            Dim Multerm As Int16
            Dim GEMMul As Single
            Dim ConflictEst As Single
            Dim HomicideEst As Single
            Dim WomenandChilEst As Single
            Dim WomenEst As Single
            Dim ChilEst As Single
            Dim PoliceVioEst As Single
            Dim dment As Single
            Dim cterm As Single
            Dim sterm As Single
            Dim yterm As Single
            Dim SelfHarmEst As Single
            Dim nwplimfunc As Single
            Dim nwplim As Single
            Dim netcompnum As Single
            Dim ConvTar As Single





            Call ValueChange(context)

            'jrs 2009/06/05
            'computeRegQualExp sedyrsag15, False

            computeRegQualExp(sedyrsag15, context)

            '
            '   All but First 1:  Democracy:  Swing State Contribution
            '   ------------------------------------------------------

            SwingStateNames$(1) = "Brazil"
            SwingStateNames$(2) = "Indonesia"
            SwingStateNames$(3) = "Mexico"
            SwingStateNames$(4) = "Nigeria"
            SwingStateNames$(5) = "Pakistan"
            SwingStateNames$(6) = "Russian Federation"
            SwingStateNames$(7) = "South Africa"
            SwingStateNames$(8) = "Turkey"
            SwingStateNames$(9) = "Ukraine"

            WDemoc = 0
            For R% = 1 To NR%
                WDemoc = WDemoc + SDEMOCPOLITY(R%)
                swingeffects(R%) = 0
            Next R%
            WDemoc = WDemoc / NR%

            For R% = 1 To NR%
                If swseffects = 1 Then 'Swing state effects turned on
                    SwingState = False
                    For Swgsts = 1 To 9 'Is state r% a swing state?
                        'jrs 2023/11/14 If RegName$(R%) = SwingStateNames$(Swgsts) Then
                        If GRPS(1, R%) = SwingStateNames$(Swgsts) Then
                            SwingState = True
                            Exit For 'will save number of swing state
                        End If
                    Next Swgsts
                    If SwingState = True Then 'Have found swing state and have number
                        SwingStsDemoc(Swgsts) = SDEMOCPOLITY(R%) 'Swing state itself in region
                        SwingStsNumb(Swgsts) = 1
                        For swingee% = 1 To NR% 'Find swingees and recalculate democracy
                            If SWINGSTS(Swgsts, swingee%) = 1 Then 'have found swingee
                                SwingStsDemoc(Swgsts) = SwingStsDemoc(Swgsts) +
                                    SDEMOCPOLITY(swingee%)
                                SwingStsNumb(Swgsts) = SwingStsNumb(Swgsts) + 1
                            End If
                        Next swingee%
                        SwingStsDemoc(Swgsts) = SwingStsDemoc(Swgsts) /
                            AMAX(1, SwingStsNumb(Swgsts))
                        For swingee% = 1 To NR%
                            If SWINGSTS(Swgsts, swingee%) = 1 Or swingee% = R% Then
                                'have found swingee or swing state itself
                                swingeffects(swingee%) =
                                    CSng(0.2 * swingstsdem(Swgsts, 1) *
                                        (WDemoc - SDEMOCPOLITY(swingee%)) +
                                    0.2 * swingstsdem(Swgsts, 2) *
                                        (SDEMOCPOLITY(R%) - SDEMOCPOLITY(swingee%)) +
                                    0.2 * swingstsdem(Swgsts, 3) *
                                        (SwingStsDemoc(Swgsts) - SDEMOCPOLITY(swingee%)))
                                'Global average, swing state, and regional average effects
                            End If
                        Next swingee%
                    End If
                End If
            Next R%

            '
            '   All but First 1:  Democracy:  Global Contributions
            '   --------------------------------------------------

            DemocImpMax = 3
            DEMOCWVDIR = SDEMOCWVDIR
            DemocWaveShift = democwvmax / AMAX(10, democwvlen) * DEMOCWVDIR
            If DEMOCWVDIR < -0.5 Then DemocWaveShift = DemocWaveShift / 3 ' downward shifts less dramatic
            DemocImpLead = (democimpus - democimpusn) * eldemocimp / AMAX(10, democwvlen)
            DEMOCWAVE = SDEMOCWAVE + DemocImpLead + democimpoth + DemocWaveShift
            MaxDemocShift = (DemocImpMax + democwvmax) / 2
            DEMOCWAVE = AMAX(-MaxDemocShift, Amin(DEMOCWAVE, MaxDemocShift))
            SDEMOCWAVE = DEMOCWAVE

            DEMOCWVCOUNT = SDEMOCWVCOUNT + 1
            If DEMOCWVCOUNT >= democwvlen Then
                DEMOCWVCOUNT = 0
                DEMOCWVDIR = DEMOCWVDIR * (-1)
            End If
            SDEMOCWVDIR = DEMOCWVDIR
            SDEMOCWVCOUNT = DEMOCWVCOUNT

            For R% = 1 To NR%
                '        If smdemocpolity(R%) <= 10 Then 'Effects maximimized for states in middle range
                '            EffectMul = smdemocpolity(R%) / 10 * 3
                '        Else
                '            EffectMul = AMAX(0, (20 - smdemocpolity(R%)) / 10 * 3)
                '        End If

                If DEMOCPOLITYI(R%) <= 10 Then 'Effects maximimized for states in middle range
                    EffectMul = DEMOCPOLITYI(R%) / 10 * 3
                Else
                    EffectMul = AMAX(0, (20 - DEMOCPOLITYI(R%)) / 10 * 3)
                End If

                If GDPPCIP(R%) > 10 Then EffectMul = 0
                'Turns off effect for richer countries (like OECD)
                demglobaleffects(R%) = (DEMOCWAVE - DemocWaveI) * EffectMul
            Next R%

            '
            '   All but First 1:  Democracy:  Domestic plus Total Calculations
            '   --------------------------------------------------------------
            'jrs 2023/10/13
            Dim PolyarchyVal As Single

            For R% = 1 To NR%
                educ15term = sedyrsag15(R%)
                'bbh 2011/04/30
                'If RegName(R%) = "Chad" Then Stop

                energydepend(R%) = CSng(energydepend(R%) * 0.8 + (ENX(R%) * ENPRI(R%) / GDP(R%)) * 0.2)
                If context.HistoricMode = True Then
                    '            Call AnalFunc("Polity Democracy with GDPPCP and Surv-SE (1960)", _
                    '                Array(GDPPCP(R%), SURVSE(R%, WVSCohorts)), Prediction)
                    '            democcomp(R%) = AMAX(1, Amin(20, Prediction))
                    '            Call AnalFunc("Polity Democracy with GDPPCP and Surv-SE (1960)", _
                    '                Array(GDPPCIP(R%), SSURVSEI(R%, WVSCohorts)), Prediction)
                    '            democcomp2 = AMAX(1, Amin(20, Prediction))
                    '            'bbh 2011/05/02
                    'bbh 2011/08/19
                    'Call AnalFunc("Polity Democ versus Edyears, YthBulge, ln(EnExDep) 2005", _
                    'Array(educ15term, YTHBULGE(R%), energydepend(R%)), Prediction)
                    'jrs 2011/10/05 Call AnalFunc("Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)", _
                    'Array(SGEM(R%), YTHBULGE(R%), energydepend(R%)), Prediction)
                    '    Call AnalFuncSE("Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)",
                    'Array(SGEM(R%), YTHBULGE(R%), energydepend(R%)), Prediction, SE,
                    'Array("GEM[0](0) (Linear)", "YTHBULGE[0](0) (Linear)", "EnExValPerGDP(0) (Linear)")) 'jrs 2014/10/09 added DepVar Names to check order


                    Call AnalFuncSE("Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)",
                                New Single() {SGEM(R%), YTHBULGE(R%), energydepend(R%)}, Prediction, SE,
                               New String() {"GEM[0](0) (Linear)", "YTHBULGE[0](0) (Linear)", "EnExValPerGDP(0) (Linear)"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                    '            Call AnalFunc("Polity Democ with EdYearsAg15, YthBulge, EnExDep (2005)", _
                    'Array(educ15term, YTHBULGE(R%), energydepend(R%)), Prediction)
                    democcomp(R%) = AMAX(1, Amin(20, Prediction))
                    'Note: if 0 used as minimum creates great instability at low GDPPCP
                    'bbh 2011/06/23
                    ExpPrediction = Prediction
                    'bbh 2011/06/23
                    'Call AnalFunc("Polity Democ with GDPPCP and EdYears 15 (2005)",
                    '    Array(GDPPCP(R%), educ15term), ExpPrediction,
                    '    Array("GDPPCP", "Educ15")) 'jrs 2014/10/09 added DepVar Names to check order

                    Call AnalFunc("Polity Democ with GDPPCP and EdYears 15 (2005)",
                    New Single() {GDPPCP(R%), educ15term}, ExpPrediction,
                    New String() {"GDPPCP", "Educ15"}, context)

                Else
                    'bbh 2011/06/23
                    'Call AnalFunc("Polity Democ with GDPPCP and EdYears 15 (2005)",
                    '    Array(GDPPCP(R%), educ15term), ExpPrediction,
                    '    Array("GDPPCP", "Educ15")) 'jrs 2014/10/09 added DepVar Names to check order


                    Call AnalFunc("Polity Democ with GDPPCP and EdYears 15 (2005)",
                    New Single() {GDPPCP(R%), educ15term}, ExpPrediction,
                    New String() {"GDPPCP", "Educ15"}, context)


                    '            Call AnalFunc("Polity Democracy with GDPPCP and Surv-SE (2000)", _
                    '                Array(GDPPCP(R%), SURVSE(R%, WVSCohorts)), Prediction)
                    '            Call AnalFunc("Polity Democ with GDPPCP and EdYears 15 (2005)", _
                    '                Array(GDPPCP(R%), educ15term), Prediction)
                    'bbh 2011/05/02
                    'bbh 2011/08/19
                    'Call AnalFunc("Polity Democ versus Edyears, YthBulge, ln(EnExDep) 2005", _
                    'Array(educ15term, YTHBULGE(R%), energydepend(R%)), Prediction)
                    'jrs 2011/10/05 Call AnalFunc("Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)", _
                    'Array(SGEM(R%), YTHBULGE(R%), energydepend(R%)), Prediction)
                    '    Call AnalFuncSE("Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)",
                    'Array(SGEM(R%), YTHBULGE(R%), energydepend(R%)), Prediction, SE,
                    'Array("GEM[0](0) (Linear)", "YTHBULGE[0](0) (Linear)", "EnExValPerGDP(0) (Linear)")) 'jrs 2014/10/09 added DepVar Names to check order

                    Call AnalFuncSE("Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)",
                     New Single() {SGEM(R%), YTHBULGE(R%), energydepend(R%)}, Prediction, SE,
                     New String() {"GEM[0](0) (Linear)", "YTHBULGE[0](0) (Linear)", "EnExValPerGDP(0) (Linear)"}, context) 'jrs 2014/10/09 added DepVar Names to check order



                    '            Call AnalFunc("Polity Democ with EdYearsAg15, YthBulge, EnExDep (2005)", _
                    'Array(educ15term, YTHBULGE(R%), energydepend(R%)), Prediction)
                    democcomp(R%) = AMAX(1, Amin(20, Prediction))
                    '            democcomp(R%) = AMAX(1, Amin(20, Prediction))
                    '            educ15term = EDYRSAG15I(R%)
                    '            Call AnalFunc("Polity Democ with GDPPCP and EdYears 15 (2005)", _
                    '                Array(GDPPCIP(R%), educ15term), Prediction)
                    '            democcomp2 = AMAX(1, Amin(20, Prediction))
                End If
                'jrs 2011/10/05
                'dkb 2013/09/18 added dimension 1 to democpolitysetar and democpolityseyrtar
                If democpolitysetar(R%) <> 0 Then
                    EffectSE = StdErrorMultShift(SE, CSng(democcomp(R%)), CSng(democpolityri(R%)), CSng(democpolityri(R%)),
                    CSng(democpolitysetar(R%)), CInt(FirstYear + democpolityseyrtar(R%)), FirstYear + IY - 1, CSng(DEMOCPOLITYI(R%)), FirstYear)
                    'SE , Prediction, DataMultShift, IDataMultShift, TargetSEs, TargetYr, currentYr, IData
                Else
                    EffectSE = 1
                End If
                'jrs 2011/10/05 DEMOCPOLITY(R%) = democpolityri(R%) * democcomp(R%)
                DEMOCPOLITY(R%) = democpolityri(R%) * democcomp(R%) * EffectSE
                DEMOCPOLITY(R%) = AMAX(1, Amin(20, DEMOCPOLITY(R%)))
                'If RegName(R%) = "Chad" Then Stop

                ' Basic domestic calculation - equal to expected value
                ' If RegName(R%) = "Armenia" Then Stop

                '        democmul = democcomp(R%) / AMAX(0.01, democcomp2)
                '        DEMOCPOLITY(R%) = DEMOCPOLITYI(R%) * democmul

                '        If democcomp(R%) > DemocPolityI(R%) And DEMOCPOLITY(R%) > democcomp(R%) Then
                '            DEMOCPOLITY(R%) = democcomp(R%) 'Hold down growth realted to small initial
                '        End If
                'The above logic created a discontinuity (e.g. Tanzania in 2030) when democcomp
                'became just larger than democpolityi; big downward shift because democmul was about
                '1.6, so lost that multiplier on deomcpolityi=democcomp
                DEMOCPOLITY(R%) = DEMOCPOLITY(R%) * democm(R%)
                'If RegName(R%) = "Tanzania" And IY% >= 29 Then Stop
                democcomp(R%) = AMAX(1, Amin(20, Prediction))
                DEMOCPOLITY(R%) = ConvergeOverTime(DEMOCPOLITY(R%), democcomp(R%), CSng(democconv * 1.5))
                'Base, Target, Years
                'bbh 2011/06/23
                DEMOCEXP(R%) = AMAX(1, Amin(ExpPrediction, 20))


                'Swing state addition, if turned on
                'DEMOCPOLITY(R%) = SDemocPolity(R%) 'to maximize swing effects
                If swseffects = 1 Then
                    DEMOCPOLITY(R%) = DEMOCPOLITY(R%) +
                        AMAX(-swseffmax * (IY% - 1), Amin(swseffmax * (IY% - 1), swingeffects(R%)))
                End If

                'Systemic wave effects

                DEMOCPOLITY(R%) = DEMOCPOLITY(R%) + demglobaleffects(R%)

                DEMOCPOLITY(R%) = AMAX(1, Amin(DEMOCPOLITY(R%), 20))
                SDEMOCPOLITY(R%) = DEMOCPOLITY(R%)
                smdemocpolity(R%) = CSng(0.6 * smdemocpolity(R%) + 0.4 * DEMOCPOLITY(R%))

                'jrs 2023/10/12 *** start ***
                'clp 2024/03/28 *** start ***
                'Call AnalFunc("Polyarchy",
                '   New Single() {GINIDOM(R%), sinfmor(R%), GDPR(R%), GDPR(R%) * sinfmor(R%)}, PolyarchyVal,
                '    New String() {"GINIDOM", "INFMOR", "GDPR", "GDPRxINFMOR"}, context)
                'If GRPS(1, R%) = "United States of America" And IY + FirstYear - 1 = 2100 Then Stop

                Dim Oilinc250pcthresh As Single

                If CSng(((ENPRI(R%) * ENP(R%, 1)) * 1000000000) / (POP(R%) * 1000000)) < 250 Or oilpcthresw = 0 Then ' clp 2024/12/20 add Or oilpcthresw = 0
                    'jrs 2025/01/07 smothing the pass from not meting to meeting the oil per capita threshold
                    'Oilinc250pcthresh = 0 'not met
                    If sOilinc250pcthreshAll(R%) <= 0 Then
                        Oilinc250pcthresh = 0 'not met
                    Else
                        Oilinc250pcthresh = CSng(sOilinc250pcthreshAll(R%) - 1 / 5)
                        Oilinc250pcthresh = AMAX(Oilinc250pcthresh, 0)
                        sOilinc250pcthreshAll(R%) = Oilinc250pcthresh
                        Oilinc250pcthresh = CSng(1 / (1 + Exp(-1.5 * Oilinc250pcthresh * 5 + 4)))
                    End If
                Else
                    'jrs 2025/01/07 smothing the pass from not meting to meeting the oil per capita threshold
                    'Oilinc250pcthresh = 1 'meets oil per capita threhold
                    If sOilinc250pcthreshAll(R%) >= 1 Then
                        Oilinc250pcthresh = 1 'meets oil per capita threhold
                    Else
                        Oilinc250pcthresh = CSng(sOilinc250pcthreshAll(R%) + 1 / 5)
                        Oilinc250pcthresh = Amin(Oilinc250pcthresh, 1)
                        sOilinc250pcthreshAll(R%) = Oilinc250pcthresh
                        Oilinc250pcthresh = CSng(1 / (1 + Exp(-1.5 * Oilinc250pcthresh * 5 + 4)))
                    End If
                End If

                PolyarchyVal = CSng(0.1269731 + (sedyrsag15(R%) * 0.0558807) + (sinfmor(R%) * -0.0000843) + (Oilinc250pcthresh * -0.0312973))

                DEMOCPOLYARCHY(R%) = PolyarchyVal + DEMOCPOLYARCHYShift(R%)

                'If GRPS(1, R%) = "Zambia" And IY = 4 Then Stop
                'If PolyarchyGR(R%) = 0 Then
                '    PolyarchyGR(R%) = PolyarchyVal / SDEMOCPOLYARCHYFX(R%) - 1
                'End If
                'DEMOCPOLYARCHY(R%) = CSng(SDEMOCPOLYARCHY(R%) * (1 + PolyarchyGR(R%)))
                'If IY > 3 Then
                '    If SDEMOCPOLYARCHYFX(R%) <> 0 Then
                '        PolyarchyGR(R%) = CSng(0.8 * PolyarchyGR(R%) + 0.2 * (PolyarchyVal / SDEMOCPOLYARCHYFX(R%) - 1))
                '    End If
                'End If
                'End If
                DEMOCPOLYARCHY(R%) = DEMOCPOLYARCHY(R%) * democpolyarchym(R%)
                DEMOCPOLYARCHY(R%) = DEMOCPOLYARCHY(R%) + democpolyarchyadd(R%)
                DEMOCPOLYARCHY(R%) = AMAX(Amin(DEMOCPOLYARCHY(R%), 1), 0)
                'jrs 2023/10/23
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("DEMOCPOLYARCHY", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If
                SDEMOCPOLYARCHY(R%) = DEMOCPOLYARCHY(R%)
                'SDEMOCPOLYARCHYFX(R%) = PolyarchyVal
                'clp 2024/03/28 *** end ***
                'jrs 2023/10/12 *** end ***
            Next R%


            '
            '   BBH 2011/04/12
            '   All but First 1:  Freedom, Government Effectiveness, Empowerment Calculations
            '   -----------------------------------------------------------------------------

            For R% = 1 To NR%
                If context.HistoricMode = True Then
                    'Call XYTABL("GDP/Capita (PPP) Versus Freedom -Logged and Inverted (Earliest)",
                    '    GDPPCP(R%), FreeComp)

                    Call XYTABL("GDP/Capita (PPP) Versus Freedom -Logged and Inverted (Earliest)",
                        GDPPCP(R%), FreeComp, context)
                    'Call XYTABL("GDP/Capita (PPP) Versus Freedom -Logged and Inverted (Earliest)",
                    '    GDPPCIP(R%), FreeComp2)

                    Call XYTABL("GDP/Capita (PPP) Versus Freedom -Logged and Inverted (Earliest)",
                        GDPPCIP(R%), FreeComp2, context)

                    '            Call XYTABL("GDP/Capita (PPP) Versus Economic Freedom (1970)", _
                    '                GDPPCP(R%), econfreecomp)
                    '            Call XYTABL("GDP/Capita (PPP) Versus Economic Freedom (1970)", _
                    '                GDPPCIP(R%), econfreecomp2)
                Else
                    '            Call XYTABL("GDP/Capita (PPP) Versus Freedom -Logged and Inverted (2005)", _
                    '                GDPPCP(R%), FreeComp)
                    '            Call XYTABL("GDP/Capita (PPP) Versus Freedom -Logged and Inverted (2005)", _
                    '                GDPPCIP(R%), FreeComp2)
                    educ15term = sedyrsag15(R%)
                    'bbh 2011/08/19
                    educ15termfem = sedyrsag15fem(R%)
                    educ15termi = EDYRSAG15I(R%)
                    educ15termfemi = EDYRSAG15FemI(R%)
                    'jrs 2013/11/28
                    '            Call AnalFunc("Feeedom (Inverted) with GDPPCP and EdYears 15 (2005)", _
                    '                Array(GDPPCP(R%), educ15term), FreeComp)
                    'Call AnalFunc("Freedom (Inverted) with GDPPCP and EdYears 15 (2012)",
                    '    Array(GDPPCP(R%), educ15term), FreeComp,
                    '    Array("GDP2005PCPPP(MOSTRECENT)/ 1000 (Log)", "EdYearsAge15Total(MOSTRECENT) (Linear)")) 'jrs 2014/10/09 added DepVar Names to check order

                    Call AnalFunc("Freedom (Inverted) with GDPPCP and EdYears 15 (2012)",
                    New Single() {GDPPCP(R%), educ15term}, FreeComp,
                    New String() {"GDP2005PCPPP(MOSTRECENT)/ 1000 (Log)", "EdYearsAge15Total(MOSTRECENT) (Linear)"}, context)

                    'Note Preprocessor uses GDPPCP only
                    '            educ15termi = EDYRSAG15I(R%)
                    'jrs 2013/11/28
                    '            Call AnalFunc("Feeedom (Inverted) with GDPPCP and EdYears 15 (2005)", _
                    '                Array(GDPPCIP(R%), educ15termi), FreeComp2)
                    'Call AnalFunc("Freedom (Inverted) with GDPPCP and EdYears 15 (2012)",
                    '    Array(GDPPCIP(R%), educ15termi), FreeComp2,
                    '    Array("GDP2005PCPPP(MOSTRECENT)/ 1000 (Log)", "EdYearsAge15Total(MOSTRECENT) (Linear)")) 'jrs 2014/10/09 added DepVar Names to check order


                    Call AnalFunc("Freedom (Inverted) with GDPPCP and EdYears 15 (2012)",
                    New Single() {GDPPCIP(R%), educ15termi}, FreeComp2,
                    New String() {"GDP2005PCPPP(MOSTRECENT)/ 1000 (Log)", "EdYearsAge15Total(MOSTRECENT) (Linear)"}, context)


                    '            Call XYTABL("GDP/Capita (PPP) Versus Economic Freedom (2000) Log", _
                    '                GDPPCP(R%), econfreecomp)
                    '            Call XYTABL("GDP/Capita (PPP) Versus Economic Freedom (2000) Log", _
                    '                GDPPCIP(R%), econfreecomp2)

                    'mti June 2010 - bug found by mti and fix confirmed by BBH -start
                    '            TbName$ = "GDP/Capita (PPP) Versus Economic Freedom (" & CStr(BaseYear) & ") Log"
                    '            TbName$ = "GDP/Capita (PPP) Versus Economic Freedom (" & CStr(BaseYear) & ") Log"
                    'mti June 2010 - bug found by mti and fix confirmed by BBH - end

                    '            TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear)
                    '            Call XYTABL(TbName$, GDPPCP(R%), econfreecomp)
                    '            Call XYTABL(TbName$, GDPPCIP(R%), econfreecomp2)
                    'To be compatible with DataEcon and SocioPol

                    '            educ15term = sedyrsag15(R%)
                End If
                FreeMul = FreeComp / AMAX(0.01, FreeComp2)
                FREEDOM(R%) = FREEDOMI(R%) * FreeMul * freedomm(R%)
                FREEDOM(R%) = AMAX(2, Amin(FREEDOM(R%), 14))
                '        EconFreeMul = econfreecomp / AMAX(0.01, econfreecomp2)
                '        ECONFREE(R%) = econfreei(R%) * EconFreeMul * econfreem(R%)
                '        ECONFREE(R%) = AMAX(1, Amin(ECONFREE(R%), 10))


                '        educ15term = sedyrsag15(R%)
                '        Call AnalFunc("Governance Effectiveness (2006)", _
                '            Array(GDPPCP(R%), educ15term), Prediction)
                '        EffectAdd = ConvergeOverTime(GovEffectAdd(R%), 0, goveffconv) 'Base, Target, Years
                '        GOVEFFECT(R%) = AMAX(0, Amin(5, (Prediction + EffectAdd + 2.5) * goveffectm(R%)))

                'Regulatory Quality
                '------------------
                '        Call AnalFunc("Governance Regulatory Quality (2006)", _
                '            Array(GDPPCP(R%), educ15term), Prediction)
                '        EffectAdd = ConvergeOverTime(GovRegQualAdd(R%), 0, goveffconv) 'Base, Target, Years
                '        GOVREGQUAL(R%) = AMAX(0, Amin(5, (Prediction + EffectAdd + 2.5) * govregqualm(R%)))
                '        SGOVREGQUAL(R) = GOVREGQUAL(R)

                'bbh 2011/04/13 start
                '        TbName$ = "GDP/Capita (PPP 2000) Versus TI Corruption Perception (" & CStr(BaseYear) & ") Linear"
                '        TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear)
                '        Call XYTABL(TbName$, GDPPCP(R%), Prediction)

                '        confforsw = 1

                'bbh 2011/04/30 start
                'energyterm = ENX(R%) * ENPRI(R%) / GDP(R%)
                'bbh 2011/08/19
                ' Call AnalFunc("Governance Corruption (2005)", _
                'Array(GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)), Prediction)
                '    Call AnalFuncSE("GOVCORRUPTversus GDPPCP, DEMOCPOLITY, GEM, Ex Export Depend (2010)",
                'Array(GDPPCP(R%), DEMOCPOLITY(R%), SGEM(R%), energydepend(R%)), Prediction, SE,
                'Array("GDPPCP[0](0) (Linear)", "DEMOCPOLITY[0](0) (Linear)", "GEM[0](0) (Linear)", "EnExValPerGDP(0) (Linear)")) 'jrs 2014/10/09 added DepVar Names to check order

                Call AnalFuncSE("GOVCORRUPTversus GDPPCP, DEMOCPOLITY, GEM, Ex Export Depend (2010)",
                     New Single() {GDPPCP(R%), DEMOCPOLITY(R%), SGEM(R%), energydepend(R%)}, Prediction, SE,
                     New String() {"GDPPCP[0](0) (Linear)", "DEMOCPOLITY[0](0) (Linear)", "GEM[0](0) (Linear)", "EnExValPerGDP(0) (Linear)"}, context) 'jrs 2014/10/09 added DepVar Names to check order


                'bbh 2011/04/30 end
                If confforsw = 1 Then
                    If sfcivilwarucdpsw = 1 Then
                        Prediction = CSng(Prediction - 0.5 * SSFCIVILWARUCDP(R%))
                    ElseIf sfcivilwarucdpsw = 3 Then
                        Prediction = CSng(Prediction - 0.5 * SSFCIVILWARPARDEE(R%))
                    Else
                        Prediction = CSng(Prediction - 0.5 * ssfintlwarallLoc(R%))
                    End If
                End If
                'To be compatible Economy; have used simpler function for DataEcon hole filling
                CorruptAdd = ConvergeOverTime(GovCorruptAdd(R%), 0, goveffconv)
                'bbh 2011/10/08 start
                'dkb 2013/09/18 added dimension 1 to goveffectsetar and goveffectseyrtar
                If govcorruptsetar(R%) <> 0 Then
                    EffectSE = StdErrorAdditiveShift(SE, CSng(Prediction), CSng(CorruptAdd), CSng(GovCorruptAdd(R%)),
                    CSng(govcorruptsetar(R%)), CInt(FirstYear + govcorruptseyrtar(R%)), FirstYear + IY - 1, FirstYear)
                    'SE, Prediction, DataAddShift, IDataAddShift, TargetSEs, TargetYr, CurrentYr
                Else
                    EffectSE = 0
                End If
                GOVCORRUPT(R%) = AMAX(1, Amin(10, (Prediction + CorruptAdd + EffectSE) * govcorruptm(R%)))
                'bbh 2011/10/08 end

                'jrs 2024/02/29
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("GOVCORRUPT", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If


                'If RegName(R%) = "Afghanistan" Then Stop
                'bbh 2011/04/30 start
                '        energyterm = ENX(R%) * WEPBYEAR / GDP(R%)
                'Call AnalFuncSE("Governance Effectiveness (2005)",
                '    Array(GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)), Prediction, SE,
                '    Array("GDPPCP", "DEMOCPOLITY", "EnExDep")) 'jrs 2014/10/09 added DepVar Names to check order 'Using bivariate in preprocessor 'Using bivariate in preprocessor

                Call AnalFuncSE("Governance Effectiveness (2005)",
                     New Single() {GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)}, Prediction, SE,
                     New String() {"GDPPCP", "DEMOCPOLITY", "EnExDep"}, context) 'jrs 2014/10/09 added DepVar Names to check order 'Using bivariate in preprocessor 'Using bivariate in preprocessor



                'bbh 2011/04/30 end
                'bbh 2011/09/21 Function now estimated with forecasts (0-5)/not data (-2.5 to 2/5)
                'Prediction = AMAX(0, Amin(5, Prediction + 2.5))
                Prediction = AMAX(0, Amin(5, Prediction))
                If confforsw = 1 Then
                    If sfcivilwarucdpsw = 1 Then
                        Prediction = CSng(Prediction - 0.08 * SSFCIVILWARUCDP(R%))
                    ElseIf sfcivilwarucdpsw = 3 Then
                        Prediction = CSng(Prediction - 0.08 * SSFCIVILWARPARDEE(R%))
                    Else
                        Prediction = CSng(Prediction - 0.08 * ssfintlwarallLoc(R%))
                    End If
                End If
                EffectAdd = ConvergeOverTime(GovEffectAdd(R%), 0, goveffconv) 'Base, Target, Years
                'bbh 2011/10/08 start
                If goveffectsetar(R%) <> 0 Then
                    EffectSE = StdErrorAdditiveShift(SE, CSng(Prediction), CSng(EffectAdd), CSng(GovEffectAdd(R%)),
                    CSng(goveffectsetar(R%)), CInt(FirstYear + goveffectseyrtar(R%)), FirstYear + IY - 1, FirstYear)
                    'SE, Prediction, DataAddShift, IDataAddShift, TargetSEs, TargetYr, CurrentYr
                Else
                    EffectSE = 0
                End If
                'If R% = 1 Or R% = 180 Then Stop
                'GOVEFFECT(R%) = AMAX(0, Amin(5, (Prediction + EffectAdd) * goveffectm(R%)))
                GOVEFFECT(R%) = AMAX(0, Amin(5, (Prediction + EffectAdd + EffectSE) * goveffectm(R%)))
                GOVEFFECT_Lag1(R%) = GOVEFFECT_Lag(R%)
                GOVEFFECT_Lag(R%) = GOVEFFECT(R%)
                'jrs 2024/02/29
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("GOVEFFECT", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                'bbh 2011/10/08 end
                'If RegName(R%) = "Austria" Then Stop
                'bbh 2011/04/30
                'jrs 2011/10/04 Call AnalFunc("Governance Regulatory Quality (2005)", _
                'Array(GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)), Prediction) 'Using bivariate in preprocessor
                '    Call AnalFuncSE("Governance Regulatory Quality (2005)",
                'Array(GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)), Prediction, SE,
                'Array("GDPPCP", "DEMOCPOLITY", "EnExDep")) 'jrs 2014/10/09 added DepVar Names to check order 'Using bivariate in preprocessor

                Call AnalFuncSE("Governance Regulatory Quality (2005)",
                     New Single() {GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)}, Prediction, SE,
                     New String() {"GDPPCP", "DEMOCPOLITY", "EnExDep"}, context)

                If confforsw = 1 Then
                    If sfcivilwarucdpsw = 1 Then
                        Prediction = CSng(Prediction - 0.2 * SSFCIVILWARUCDP(R%))
                    ElseIf sfcivilwarucdpsw = 3 Then
                        Prediction = CSng(Prediction - 0.2 * SSFCIVILWARPARDEE(R%))
                    Else
                        Prediction = CSng(Prediction - 0.2 * ssfintlwarallLoc(R%))
                    End If
                End If
                'bbh 2011/09/21 Function now estimated with forecasts (0-5)/not data (-2.5 to 2/5)
                'Prediction = AMAX(0, Amin(5, Prediction + 2.5))
                Prediction = AMAX(0, Amin(5, Prediction))
                EffectAdd = ConvergeOverTime(GovRegQualAdd(R%), 0, goveffconv) 'Base, Target, Years
                'jrs 2011/10/04
                'dkb 2013/09/18 added dimension 1 to govregqualsetar and govregqualseyrtar
                If govregqualsetar(R%) <> 0 Then
                    EffectSE = StdErrorAdditiveShift(SE, CSng(Prediction), CSng(EffectAdd), CSng(GovRegQualAdd(R%)),
                    CSng(govregqualsetar(R%)), CInt(FirstYear + govregqualseyrtar(R%)), FirstYear + IY - 1, FirstYear)
                    'SE, Prediction, DataAddShift, IDataAddShift, TargetSEs, TargetYr, CurrentYr
                Else
                    EffectSE = 0
                End If
                'jrs 2011/10/04 GOVREGQUAL(R%) = AMAX(0, Amin(5, (Prediction + EffectAdd) * govregqualm(R%)))
                GOVREGQUAL(R%) = AMAX(0, Amin(5, (Prediction + EffectAdd + EffectSE) * govregqualm(R%)))
                SGOVREGQUAL(R) = GOVREGQUAL(R)
                'bbh 2011/04/30

                'jrs 2019/09/17
                ''mti 2014/11/14 -start
                'TbName$ = "GDP/Capita (PPP 2011) Versus Cost of Starting Business (" & CStr(BaseYear) & ") Log"
                'TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
                'jrs 2024/06/27 TbName$ = "GDP/Capita (PPP 2011) Versus Cost of Starting Business (MOSTRECENT) Log"
                Call XYTABL(TFNameCostStartBuss, GDPPCP(R%), Prediction, context)

                If GovBusCostShift(R%) > 0 Then
                    govbushiftyear = 50
                Else
                    govbushiftyear = 120
                End If

                GOVBUSCOST(R%) = AMAX(1, Amin(300, Prediction + ConvergeOverTime(GovBusCostShift(R%), 0, govbushiftyear)))
                GOVBUSCOST(R%) = AMAX(1, Amin(300, GOVBUSCOST(R%) * govbuscostm(R%)))
                'mti 2014/11/14 - end


                'jrs 2019/09/17
                ''mti 2014/11/27 -start
                ''mti 2015/01/20 - start
                ''TbName$ = "GDP/Capita (PPP 2011) Versus Business Index (" & CStr(BaseYear) & ") Log"
                'TbName$ = "GDP/Capita (PPP 2011) Versus Business Regulation Index (" & CStr(BaseYear) & ") Log"
                ''mti 2015/01/20 - end

                'TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
                TbName$ = "GDP/Capita (PPP 2011) Versus Business Regulation Index (MOSTRECENT) Log"
                Call XYTABL(TbName$, GDPPCP(R%), Prediction, context)

                'mti 2015/03/04 - start
                If GovBusRegIndShift(R%) < 0 Then
                    govbushiftyear = 50
                Else
                    govbushiftyear = 200 ' 120
                End If


                GOVBUSREGIND(R%) = AMAX(0.01, Amin(10, Prediction + ConvergeOverTime(GovBusRegIndShift(R%), 0, govbushiftyear)))
                GOVBUSREGIND(R%) = AMAX(0.01, Amin(10, GOVBUSREGIND(R%) * govbusregindm(R%)))
                'mti 2015/03/04 - end
                'mti 2014/11/27 - end

                '   If RegName(R%) = "Singapore" And IY > 16 Then Stop
                'jrs 2013/11/28
                '        Call AnalFunc("Economic Freedom (2005)", _
                '            Array(GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)), Prediction) 'Using bivariate in preprocessor
                'Call AnalFunc("Economic Freedom (2010)",
                '    Array(GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)), Prediction,
                '    Array("GDPPCP (Log)", "DEMOCPOLITY (Linear)", "ENX*ENPRI/GDP(Linear)")) 'jrs 2014/10/09 added DepVar Names to check order 'Using bivariate in preprocessor

                Call AnalFunc("Economic Freedom (2010)",
                    New Single() {GDPPCP(R%), DEMOCPOLITY(R%), energydepend(R%)}, Prediction,
                    New String() {"GDPPCP (Log)", "DEMOCPOLITY (Linear)", "ENX*ENPRI/GDP(Linear)"}, context)


                If confforsw = 1 Then
                    If sfcivilwarucdpsw = 1 Then
                        Prediction = CSng(Prediction - 0.3 * SSFCIVILWARUCDP(R%))
                    ElseIf sfcivilwarucdpsw = 3 Then
                        Prediction = CSng(Prediction - 0.3 * SSFCIVILWARPARDEE(R%))
                    Else
                        Prediction = CSng(Prediction - 0.3 * ssfintlwarallLoc(R%))
                    End If
                End If
                Prediction = AMAX(0.01, Amin(10, Prediction))
                ECONFREE(R%) = (AMAX(0.01, Amin(10, econfreeri(R%) * Prediction * econfreem(R%))))
                'If RegName(R%) = "Afghanistan" Then Stop

                'bbh 2011/04/13 end

                '        Call XYTABL("GDP/Capita Versus Global Empowerment Measure", GDPPC(R%), GEMComp)
                '        Call XYTABL("GDP/Capita Versus Global Empowerment Measure", GDPPCI(R%), GEMComp2)

                'bbh 2011/08/19 'Tried funciton with log on GDPPCP but stops growth for high income countries
                TableName$ = "GEM versus GDPPCP,EDYRSAG15 Female, YTHBULGE(2010)"
                'Call AnalFuncSE(TableName$, Array(GDPPCIP(R%), educ15termfemi, YTHBULGEI(R%)), GEMComp2, SE,
                'Array("GDPPCP[0](0) (Linear)", "EDYRSAG15[0],Female(0) (Linear)", "YTHBULGE[0](0) (Linear)")) 'jrs 2014/10/09 added DepVar Names to check order


                Call AnalFuncSE(TableName$,
                     New Single() {GDPPCIP(R%), educ15termfemi, YTHBULGEI(R%)}, GEMComp2, SE,
                     New String() {"GDPPCP[0](0) (Linear)", "EDYRSAG15[0],Female(0) (Linear)", "YTHBULGE[0](0) (Linear)"}, context)

                'Call AnalFuncSE(TableName$, Array(GDPPCP(R%), educ15termfemi, YTHBULGE(R%)), GEMComp, SE,
                'Array("GDPPCP[0](0) (Linear)", "EDYRSAG15[0],Female(0) (Linear)", "YTHBULGE[0](0) (Linear)")) 'jrs 2014/10/09 added DepVar Names to check order

                'jrs 2022/06/07 we were using educ15termfemi which is first year value
                Call AnalFuncSE(TableName$,
                     New Single() {GDPPCP(R%), educ15termfem, YTHBULGE(R%)}, GEMComp, SE,
                     New String() {"GDPPCP[0](0) (Linear)", "EDYRSAG15[0],Female(0) (Linear)", "YTHBULGE[0](0) (Linear)"}, context)


                'bbh 2011/10/08 start
                'dkb 2013/09/18 added dimension 1 to gemsetar and gemseyrtar
                If gemsetar(R%) <> 0 Then
                    Multerm = 1
                    EffectSE = StdErrorMultShift(SE, CSng(GEMComp), CSng(Multerm), CSng(Multerm),
                    CSng(gemsetar(R%)), CInt(FirstYear + gemseyrtar(R%)), FirstYear + IY - 1, CSng(GEMComp2), FirstYear)
                    'SE, Prediction, DataMultShift, IDataMultShift, TargetSEs, TargetYr, CurrentYr, IData
                Else
                    EffectSE = 1
                End If
                GEMMul = GEMComp / AMAX(0.01, GEMComp2)
                'bbh 2011/08/18
                GEM(R%) = GEMI(R%) * GEMMul * EffectSE * gemm(R%)
                'GEM(R%) = GEMI(R%) * GEMMul * gemm(R%)
                'bbh 2011/10/08 end
                'GEMBase = ConvergeOverTime(GEM(R%), GEMComp, gemconv) 'Base, Target, Years
                'bbh 2011/08/18 end
                GEM(R%) = CSng(AMAX(0, Amin(GEM(R%), 1.0#)))
                SGEM(R%) = GEM(R%) 'bbh 2011/08/20

                'dkb 2020/02/24 - START. Adding logic for GII. Note: The useless coefficients and exponents (set to 1) are placeholders for missing components (i.e. political participation)
                'If RegName$(R%) = "Rwanda" And IY% > 2 Then Stop
                Dim AdolescentBirthRate = AMAX(1, 3.28 * SCBR(R%) - 19.9)  'dkb 2021/01/12 changed from 0.1 to 1
                Dim MatMortTerm = AMAX(0.001, 1 - AMAX(((Amin(2000, sMATMORTRATIO(R%)) - 3) / (2000 - 3)), 0.001)) 'dkb 2020/04/29 'dkb 2021/01/12 changed from 1000 to 2000

                'jrs 2020/04/02 changed to use sEDSECPER in next 2 lines
                'dkb 2020/04/29 Dim GeoMeanFem = ((((10 / AMAX(10, Amin(1000, sMATMORTRATIO(R%)))) * (1 / AdolescentBirthRate)) ^ (1 / 2)) * ((1 * sEDSECPER(R%, Female) / 100) ^ 1) * (LABPARR(R%, Female) / 100)) ^ (1 / 3)
                Dim GeoMeanFem = ((((MatMortTerm) * (1 / AdolescentBirthRate)) ^ (1 / 2)) * ((1 * sEDSECPER(R%, Female) / 100) ^ 1) * (LABPARR(R%, Female) / 100)) ^ (1 / 3)
                Dim GeoMeanMale = (((1 * (1 * sEDSECPER(R%, Male) / 100)) ^ 1) * (LABPARR(R%, Male) / 100)) ^ (1 / 3)

                Dim HarmonicMean = (((GeoMeanFem ^ (-1)) + (GeoMeanMale ^ (-1))) / 2) ^ (-1)

                'jrs 2020/04/02 changed to sMATMORTRATIO
                'dkb 2020/04/29 Dim ArithMeanHealth = ((((10 / AMAX(10, Amin(1000, sMATMORTRATIO(R%)))) * (1 / AdolescentBirthRate)) ^ (1 / 2)) + 1) / 2
                Dim ArithMeanHealth = ((((MatMortTerm) * (1 / AdolescentBirthRate)) ^ (1 / 2)) + 1) / 2
                'jrs 2020/04/02 changed to use sEDSECPER
                Dim ArithMeanEmpower = (((1 * sEDSECPER(R%, Female) / 100) ^ 1) + ((1 * (1 * sEDSECPER(R%, Male) / 100)) ^ 1)) / 2
                Dim ArithMeanLaborPar = ((LABPARR(R%, Female) / 100) + (LABPARR(R%, Male) / 100)) / 2

                Dim GeoArithMean = (ArithMeanHealth * ArithMeanEmpower * ArithMeanLaborPar) ^ (1 / 3)
                GII(R%) = CSng(1 - (HarmonicMean / GeoArithMean) + ConvergeOverTime(GIIShift(R%), 0, 100))
                'dkb 2020/02/24 *** end ***

            Next R%


            'kbn 2018/06/26-Start- Add notes for violence model
            '--------All but first 1: Violence model--------
            'Violence model in the in following sequence
            '1. TO 5. Compute death rates for different types of violence
            '6. Compute total societal violence deaths using the death rates and age structure calculated using agedst
            '7. Calculate total number of deaths
            '8. Recalculate total death rate using total number of deaths
            '9. Compute the HOMICIDE index
            'kbn 2018/06/26- End-

            For R% = 1 To NR%
                'If RegName$(R%) = "Yemen" And IY% > 6 Then Stop
                '1. Conflict
                'kbn 2018/06/01- Start- Change formulation below to drive conflict with function that uses probability of internal war
                'Call AnalFunc("Conflict Deaths per 100000", Array(ssfintlwarallLoc(R%)), ConflictEst, Array("Probability of internal war"))
                'ConflictEst = AMAX(-0.3137 + 8.2297 * SSFINTLWARMAG(R%), 0.0000001) 'dkb 2019/01/22 - updating conflict deaths to be driven by interal war magnitude
                'ConflictEst = AMAX(0.0991 + 0.5501 * SSFINTLWARMAG(R%), 0.0000001) 'dkb 2019/01/22 - updating conflict deaths to be driven by interal war magnitude and to log (ln) DV
                'ConflictEst = Amin(AMAX(Exp(ConflictEst), 0.000001), 636) 'dkb 2019/01/22 - Unlogging conflict estimate
                Call XYTABL("Conflict Deaths per 100000 (natural log)", SSFINTLWARMAG(R%), ConflictEst, context)
                ConflictEst = CSng(Exp(ConflictEst) - 1) 'dkb 2019/01/22 - Unlogging conflict estimate and removing the +1 applied during regression to bypass logs of zero
                'SVDTHSCONFLICT(R%) = Amin(636, AMAX(Exp(ConflictEst) - 1 + ConflictShift(R%)), 0.000001) * svmulm(R%, 2)
                'SVDTHSCONFLICT(R%) = Amin(AMAX(Exp(ConflictEst) - 1 + ConflictShift(R%), 0.00001), 636) * svmulm(R%, 2)
                'ConflictEstimate(R%) = Amin(AMAX(Exp(ConflictEst), 0.000001), 636)

                'kbn 2018/06/26-Start- If Countries are above the function, then converge downward over 5 years, if not converge upward over 200 years
                If ConflictShift(R%) > ConflictEst Then

                    SVDTHSCONFLICT(R%) = CSng((Amin(AMAX(ConflictEst, 0.000001), 636) + ConvergeOverTime(ConflictShift(R%), 0, 5)) * svmulm(R%, 2))
                Else
                    SVDTHSCONFLICT(R%) = CSng((Amin(AMAX(ConflictEst, 0.000001), 636) + ConvergeOverTime(ConflictShift(R%), 0, 200)) * svmulm(R%, 2))
                End If
                'kbn 2018/06/01-End
                'kbn 2018/06/26-End
                '2. Homicides
                'Call XYTABL("HomicideDeaths vs GINI", GINIDOM(R%), HomicideEst)
                'Call AnalFunc("Interpersonal deaths per 100000 people", Array(GINIDOM(R%) * 100, YTHBULGE(R%) * 100), HomicideEst, Array("GINIDOM", "Youthbulge"))

                Call AnalFunc("Interpersonal deaths per 100000 people",
                    New Single() {GINIDOM(R%) * 100, YTHBULGE(R%) * 100}, HomicideEst,
                    New String() {"GINIDOM", "Youthbulge"}, context)

                'SVDTHSOTHERINTERPERSON(R%) = Amin(AMAX(Exp(HomicideEst) + (HomicideShift(R%), 0, 100), 0.000001), 208) * svmulm(R%, 5)
                'SVDTHSOTHERINTERPERSON(R%) = Amin(AMAX(Exp(HomicideEst) * HomicideShift(R%), 0.00001), 208) * svmulm(R%, 5)
                HomicideEstimate(R%) = CSng(Amin(AMAX(Exp(HomicideEst), 0.000001), 208))
                SVDTHSOTHERINTERPERSON(R%) = SSVDTHSOTHERINTERPERSON(R%) * HomicideEstimate(R%) / sHomicideEstimate(R%) * svmulm(R%, 5)

                '3. Vulnerable populations (Women and Children)
                'Call AnalFunc("Interpersonal deaths per 100000 people", Array(GINIDOM(R%) * 100, YTHBULGE(R%) * 100), WomenandChilEst, Array("GINIDOM", "Youthbulge"))

                Call AnalFunc("Interpersonal deaths per 100000 people",
                    New Single() {GINIDOM(R%) * 100, YTHBULGE(R%) * 100}, WomenandChilEst,
                    New String() {"GINIDOM", "Youthbulge"}, context)


                'SVDTHSWOMENCHILDREN(R%) = Amin(AMAX(Exp(WomenandChilEst) + ConvergeOverTime(WomenandChilshift(R%), 0, 100), 0.000001), 21) * svmulm(R%, 4)
                'SVDTHSWOMENCHILDREN(R%) = Amin(AMAX(Exp(WomenandChilEst) * WomenandChilshift(R%), 0.00001), 21) * svmulm(R%, 4)
                SVDTHSWOMENCHILDREN(R%) = SSVDTHSWOMENCHILDREN(R%) * HomicideEstimate(R%) / sHomicideEstimate(R%) * svmulm(R%, 4)
                'dsb 2026/1/15 Saving lagged values
                'If R% = 1 Or R% = 180 Then Stop
                SVDTHSWOMENCHILDREN_Lag1(R%) = SVDTHSWOMENCHILDREN_Lag(R%)
                SVDTHSWOMENCHILDREN_Lag(R%) = SVDTHSWOMENCHILDREN(R%)
                '3a. Violence against women
                'Call AnalFunc("Interpersonal deaths per 100000 people", Array(GINIDOM(R%) * 100, YTHBULGE(R%) * 100), WomenEst, Array("GINIDOM", "Youthbulge"))

                Call AnalFunc("Interpersonal deaths per 100000 people",
                    New Single() {GINIDOM(R%) * 100, YTHBULGE(R%) * 100}, WomenEst,
                    New String() {"GINIDOM", "Youthbulge"}, context)

                SVDTHSWOMEN(R%) = CSng(Amin(AMAX(Exp(WomenEst) * Womenshift(R%), 0.00001), 21) * svmulm(R%, 4))
                '3b. Violence against children
                'Call AnalFunc("Interpersonal deaths per 100000 people", Array(GINIDOM(R%) * 100, YTHBULGE(R%) * 100), ChilEst, Array("GINIDOM", "Youthbulge"))

                Call AnalFunc("Interpersonal deaths per 100000 people",
                    New Single() {GINIDOM(R%) * 100, YTHBULGE(R%) * 100}, ChilEst,
                    New String() {"GINIDOM", "Youthbulge"}, context)


                SVDTHSCHIL(R%) = CSng(Amin(AMAX(Exp(ChilEst) * ChilShift(R%), 0.000001), 21) * svmulm(R%, 4))
                '4. Police violence
                'Call XYTABL("gdppc Versus CorruptionCPINew(MOSTRECENT) Quadratic", Log(GDPPC(R%)), CorruptEST)
                ' Call AnalFunc("PoliceViolenceDeaths (Deaths per 100000)", Array(SVDTHSOTHERINTERPERSON(R%), GOVCORRUPT(R%) / 10), PoliceVioEst, Array("Log(Homicides)", "Corruption"))

                Call AnalFunc("PoliceViolenceDeaths (Deaths per 100000)",
                    New Single() {SVDTHSOTHERINTERPERSON(R%), GOVCORRUPT(R%) / 10}, PoliceVioEst,
                    New String() {"Log(Homicides)", "Corruption"}, context)


                PoliceEstimate(R%) = CSng(Amin(AMAX(Exp(Amin(PoliceVioEst, 3)), 0.000001), 2))
                SVDTHSPOLICS(R%) = SSVDTHSPOLICS(R%) * PoliceEstimate(R%) / sPoliceEstimate(R%) * svmulm(R%, 3)
                'SVDTHSPOLICS(R%) = Amin(AMAX(Exp(Amin(PoliceVioEst, 3)) + ConvergeOverTime(PoliceShift(R%), 0, 100), 0.000001), 2) * svmulm(R%, 3)
                'SVDTHSPOLICS(R%) = Amin(AMAX(Exp(Amin(PoliceVioEst, 3) * PoliceShift(R%)), 0.000001), 2) * svmulm(R%, 3)

                '5. Self harm
                'kbn 2018/05/17-Start- Bring in DKB's new code here for self harm
                'Call AnalFunc("SelfHarmDeaths vs Edyrsage15 vs interpersonalwomenandChildren", Array(SVDTHSWOMENCHILDREN(R%), EDYRSAG15(R%, 3)), SelfHarmEst, Array("InterpersonalWomenandChil", "EdYrsAge15"))
                'SVDTHSSELFHARM(R%) = Amin(AMAX(Exp(SelfHarmEst) + ConvergeOverTime(SelfHarmShift(R%), 0, 100), 0.000001), 53) * svmulm(R%, 1)
                'kbn 2018/06/26-Start- Replace function for self harm with one that uses POPMEDAGE
                'Call AnalFunc("Self Harm Deaths per 100000 people", Array(POPMEDAGE(R%), SVDTHSWOMENCHILDREN(R%)), SelfHarmEst, Array("POPMEDAGE", "SVDTHSWOMENCHILDREN"))
                dment = CSng(AMAX(SDEATHMntlHealth(R%) / POP(R%) * 1000, 0.1))
                Call XYTABL("WomenandChilDeaths(per 100000) vs MentalHealth deaths (Log)", dment, sterm, context)
                cterm = CSng(AMAX(Amin(sterm / 4.36, 4.2), 0))
                yterm = 9.5
                SelfHarmEst = yterm + cterm * SVDTHSWOMENCHILDREN(R%)
                SelfHarmEstimate(R%) = SelfHarmEst
                'kbn 2018/05/17-End
                'kbn 2018/06/26 End
                SVDTHSSELFHARM(R%) = SSVDTHSSELFHARM(R%) * SelfHarmEstimate(R%) / sSelfHarmEstimate(R%) * svmulm(R%, 1)
                'SVDTHSSELFHARM(R%) = Amin(AMAX(SelfHarmEst * SelfHarmShift(R%), 0.000001), 53) * svmulm(R%, 1)

                'Since we are driving self harm,Police violence with deaths of women and children and homicides, we are retricting upward year on year growth to 2%
                'jrs 2023/03/13
                If SVDTHSSELFHARM(R%) > 0 Then
                    If SVDTHSSELFHARM(R%) > SSVDTHSSELFHARM(R%) And ((SVDTHSSELFHARM(R%) - SSVDTHSSELFHARM(R%)) / SVDTHSSELFHARM(R%)) > 0.02 Then
                        SVDTHSSELFHARM(R%) = CSng(SSVDTHSSELFHARM(R%) * 1.02)
                    End If
                End If

                'jrs 2023/03/13
                If SSVDTHSPOLICS(R%) > 0 Then
                    If SVDTHSPOLICS(R%) > SSVDTHSPOLICS(R%) And ((SVDTHSPOLICS(R%) - SSVDTHSPOLICS(R%)) / SSVDTHSPOLICS(R%)) > 0.02 Then
                        SVDTHSPOLICS(R%) = CSng(SSVDTHSPOLICS(R%) * 1.02)
                    End If
                End If


                'sConflictEstimate(R%) = ConflictEstimate(R%)
                'sHomicideEstimate(R%) = HomicideEstimate(R%)
                'sWomenandChilEstimate(R%) = WomenandChilEstimate(R%)
                'sPoliceEstimate(R%) = PoliceEstimate(R%)
                'sSelfHarmEstimate(R%) = SelfHarmEstimate(R%)

                '6. Societal Violence Deaths total
                'kbn 2018/05/16-Start- First create population terms for total deaths computation using 'agedst' from POPULAT
                AdultMaleTerm = ((agedst(4, R%, 1) + agedst(5, R%, 1) + agedst(6, R%, 1) + agedst(7, R%, 1) + agedst(8, R%, 1) + agedst(9, R%, 1) + agedst(10, R%, 1) +
                agedst(11, R%, 1) + agedst(12, R%, 1) + agedst(13, R%, 1) + agedst(14, R%, 1) + agedst(15, R%, 1) + agedst(16, R%, 1) + agedst(17, R%, 1) + agedst(18, R%, 1) + agedst(19, R%, 1) + agedst(20, R%, 1) + agedst(21, R%, 1)) * 1000000)
                WomenTerm = ((agedst(4, R%, 2) + agedst(5, R%, 2) + agedst(6, R%, 2) + agedst(7, R%, 2) + agedst(8, R%, 2) + agedst(9, R%, 2) + agedst(10, R%, 2) +
                agedst(11, R%, 2) + agedst(12, R%, 2) + agedst(13, R%, 2) + agedst(14, R%, 2) + agedst(15, R%, 2) + agedst(16, R%, 2) + agedst(17, R%, 2) + agedst(18, R%, 2) + agedst(19, R%, 2) + agedst(20, R%, 2) + agedst(21, R%, 2)) * 1000000)
                ChildrenTerm = (agedst(0, R%, 1) + agedst(0, R%, 2) + agedst(1, R%, 1) + agedst(1, R%, 2) + agedst(2, R%, 1) + agedst(2, R%, 2) + agedst(3, R%, 1) + agedst(3, R%, 2)) * 1000000
                poptotalterm = (AdultMaleTerm + WomenTerm + ChildrenTerm)
                'kbn 2018/05/16-End
                'SOCIETALVIOLENCEDEATHS(R%) = (((SVDTHSOTHERINTERPERSON(R%) / 100000) * AdultMaleterm + (SVDTHSWOMEN(R%) / 100000) * WomenTerm + (SVDTHSCHIL(R%) / 100000) * ChildrenTerm _
                '  + (SVDTHSCONFLICT(R%) / 100000) * poptotalterm + (SVDTHSPOLICS(R%) / 100000) * poptotalterm + (SVDTHSSELFHARM(R%) / 100000) * poptotalterm) / poptotalterm) * 100000

                'SOCIETALVIOLENCEDEATHS(R%) = SOCIETALVIOLENCEDEATHS(R%) * svmulm(R%, 6)

                '7. kbn 2018/05/17-Start- Compute total number of deaths for all violence variables
                'kbn 2018/11/26-Start- Added minimum of 0 to variables below
                SVDTHSOTHERINTERTOT(R%) = AMAX(((SVDTHSOTHERINTERPERSON(R%) / 100000) * AdultMaleTerm) + svdthsadd(R%, 5), 0)
                SVDTHSPOLICSTOT(R%) = AMAX(((SVDTHSPOLICS(R%) / 100000) * poptotalterm) + svdthsadd(R%, 3), 0)
                SVDTHSCHILTOT(R%) = AMAX(((SVDTHSCHIL(R%) / 100000) * ChildrenTerm) + svdthsadd(R%, 4), 0)
                SVDTHSWOMENTOT(R%) = AMAX(((SVDTHSWOMEN(R%) / 100000) * WomenTerm) + svdthsadd(R%, 4), 0)
                SVDTHSSELFHARMTOT(R%) = AMAX(((SVDTHSSELFHARM(R%) / 100000) * poptotalterm) + svdthsadd(R%, 1), 0)
                SVDTHSCONFLICTTOT(R%) = AMAX(((SVDTHSCONFLICT(R%) / 100000) * poptotalterm) + svdthsadd(R%, 2), 0)
                SVDTHSSOCIETALVIOLENCETOT(R%) = AMAX(SVDTHSOTHERINTERTOT(R%) + SVDTHSPOLICSTOT(R%) + SVDTHSCHILTOT(R%) + SVDTHSWOMENTOT(R%) + SVDTHSSELFHARMTOT(R%) + SVDTHSCONFLICTTOT(R%) + svdthsadd(R%, 6), 0)
                'kbn 2018/05/17- End

                '8. kbn 2018/06/01-Start- Recalculate total death rate
                SVDTHSCONFLICT(R%) = AMAX((SVDTHSCONFLICTTOT(R%) / poptotalterm) * 100000, 0)
                SVDTHSOTHERINTERPERSON(R%) = AMAX((SVDTHSOTHERINTERTOT(R%) / AdultMaleTerm) * 100000, 0)
                SVDTHSPOLICS(R%) = AMAX((SVDTHSPOLICSTOT(R%) / poptotalterm) * 100000, 0)
                SVDTHSCHIL(R%) = AMAX((SVDTHSCHILTOT(R%) / ChildrenTerm) * 100000, 0)
                SVDTHSWOMEN(R%) = AMAX((SVDTHSWOMENTOT(R%) / WomenTerm) * 100000, 0)
                SVDTHSSELFHARM(R%) = AMAX((SVDTHSSELFHARMTOT(R%) / poptotalterm) * 100000, 0)
                SOCIETALVIOLENCEDEATHS(R%) = AMAX((SVDTHSSOCIETALVIOLENCETOT(R%) / poptotalterm) * 100000, 0)
                'kbn 2018/06/01-End-
                'kbn 2018/11/26-End-

                '9. Societal violence index computation
                'kbn 2018/09/25 Start- Revise HOMICIDE INDEX below
                'HOMICIDEINDEX(R%) = (SVDTHSCONFLICT(R%) * svindexm(R%, 2) + (SVDTHSOTHERINTERPERSON(R%) * svindexm(R%, 5)) + (SVDTHSWOMEN(R%) * svindexm(R%, 4)) + (SVDTHSCHIL(R%) * svindexm(R%, 4)) + (SVDTHSPOLICS(R%) * svindexm(R%, 3)) / _
                '(svindexm(R%, 5) + svindexm(R%, 2) + svindexm(R%, 3) + svindexm(R%, 4)))
                HOMICIDEINDEX(R%) = ((SVDTHSCONFLICTTOT(R%) * svindexm(R%, 2) + (SVDTHSOTHERINTERTOT(R%) * svindexm(R%, 5)) + (SVDTHSWOMENTOT(R%) * svindexm(R%, 4)) + (SVDTHSCHILTOT(R%) * svindexm(R%, 4)) + (SVDTHSPOLICSTOT(R%) * svindexm(R%, 3)) /
     (svindexm(R%, 5) + svindexm(R%, 2) + svindexm(R%, 3) + svindexm(R%, 4)))) / POP(R%)


                'kbn 2018/09/25 Now divide homicide index by max homicides

                HOMICIDEINDEX(R%) = HOMICIDEINDEX(R%) / MaxHomicides

                'kbn 2018/05/17-End
                'kbn 2018/09/25-End

                '10. Saved value for variables
                'kbn 2018/06/26- Start- Comment out below saved variables
                'SSVDTHSCONFLICT(R%) = SVDTHSCONFLICT(R%)
                'SSVDTHSOTHERINTERPERSON(R%) = SVDTHSOTHERINTERPERSON(R%)
                'SSVDTHSWOMENCHILDREN(R%) = SVDTHSWOMENCHILDREN(R%)
                'SSVDTHSSELFHARM(R%) = SVDTHSSELFHARM(R%)
                'SSVDTHSPOLICS(R%) = SVDTHSPOLICS(R%)
                'SSOCIETALVIOLENCEDEATHS(R%) = SOCIETALVIOLENCEDEATHS(R%)
                'SHOMICIDEINDEX(R%) = HOMICIDEINDEX(R%)
                'SSVDTHSWOMEN(R%) = SVDTHSWOMEN(R%)
                'SSVDTHSCHIL(R%) = SVDTHSCHIL(R%)
                'kbn 2018/06/26 end
                SSVDTHSOTHERINTERPERSON_Pop(R%) = SVDTHSOTHERINTERPERSON(R%)
            Next R%

            'kbn 2018/04/27 END
            'All but First 1:  Networked Person Calculations
            '   ---------------------------------------------

            For R% = 1 To NR%

                'If RegName$(R%) = "Somalia" Then Stop

                Call XYTABL("GDP/Capita (PPP) Versus Internet Percent Use Target", Amin(40, GDPPCP(R%)), nwplimfunc, context)
                nwplimfunc = nwplimfunc / 100
                nwplim = nwplimfunc * POP(R%)
                'nwplim = numnwplim * POP(R%)
                NUMNWPGR(R%) = CSng(inumnwpgr(R%) * numnwpgrm(R%) *
                    (AMAX(0.01, (nwplim - snumnwp(R%)) / (nwplim - INUMNWP(R%)))) ^ 3)  ' 2
                NUMNWPGR(R%) = Amin(NUMNWPGR(R%), 200) 'bbh 2016/03/11
                If snumnwp(R%) * (1 + NUMNWPGR(R%) / 100) ^ 10 > nwplim Then
                    NUMNWPGR(R%) = CSng(((nwplim / snumnwp(R%)) ^ (1 / 10) - 1) * 100)
                End If
                NUMNWPGR(R%) = CSng(AMAX(NUMNWPGR(R%), 0.1))
                NUMNWP(R%) = snumnwp(R%) * (1 + NUMNWPGR(R%) / 100)
                'Call XYTABL("GDP/Capita (PPP) Versus Internet Percent Use (2000)", _
                'Amin(40, GDPPCP(R%)), NetComp)
                netcompnum = CSng(AMAX(0, nwplimfunc * POP(R%)))

                ConvTar = ConvergeOverTime(NUMNWP(R%), netcompnum, mfpconv(R%))
                'Base, Target, Years
                NUMNWP(R%) = CSng(Amin(NUMNWP(R%) * 1.5, ConvTar))
                numnwpmin(R%) = AMAX(numnwpmin(R%), NUMNWP(R%)) 'Don't let decline
                'mti 2023/05/04 start
                'NUMNWP(R%) = Amin(numnwpmin(R%), nwplimfunc * POP(R%))
                NUMNWP(R%) = AMAX(numnwpmin(R%), NUMNWP(R%))
                'mti 2023/05/04 end
                '        snumnwpgr(r%) = numnwpgr(r%)
                snumnwp(R%) = NUMNWP(R%)
            Next R%

            'JAM 2019-08-26
            Call IfsDomPolAllSteps(context)

        End Sub
        Public Sub IfsDomPol2AllButFirstYear(context As IForecastContext)
            '
            '   All but First 2:  Infant Mortality for State Failure Calculations
            '   ---------------------------------------------------------------

            Dim winfmor As Single
            Dim infmorterm As Single
            Dim Tradeterm As Single
            Dim democterm As Single
            Dim GDPterm As Single
            Dim Prediction As Single
            Dim historybased As Single
            Dim DemocAddCur As Single
            Dim DemocAdd As Single
            Dim DemocAddI As Single
            Dim YthBulgeAddCur As Single
            Dim YthBulgeAddI As Single
            Dim ythbulgeadd As Single
            Dim growthratediff As Single
            Dim econgrowthadd As Single
            Dim extintervadd As Single
            Dim increment As Single
            'dsb 2026/1/8
            Dim PredictionPardee As Single
            'Dim yearCivilWarPardee As Single = FirstYear + IY - 1
            Dim yearCivilWarPardee As Single = FirstYear
            Dim p As Single
            Dim shiftCivWarPardee As Single
            'dsb 2026/1/15
            Dim PredictionPOPIMP As Single

            winfmor = 0
            For R% = 1 To NR%
                winfmor = winfmor + INFMOR(R%) * POP(R%)
            Next R%
            winfmor = winfmor / WPOP

            'jrs 2024/05/17
            LNINFMORWORLD = 0
            For R% = 1 To NR%
                LNINFMORWORLD = LNINFMORWORLD + Log(INFMOR(R%))
            Next
            LNINFMORWORLD = LNINFMORWORLD / NR

            '
            '   All but First 2:  State Failure Calculations
            '   --------------------------------------------

            For R% = 1 To NR%
                infmorterm = INFMOR(R%) / winfmor
                '        tradeterm = Log((X(R%) + M(R%)) / GDP(R%) * 100)
                Tradeterm = (X(R%) + M(R%)) / GDP(R%) * 100
                democterm = DEMOCPOLITY(R%)
                '        GDPterm = Log(GDPPCP(R%))
                GDPterm = GDPPCP(R%)
                educ15term = EDYRSAG15(R%, 3)

                'This formulation was put in from SF project vol 2; needs to be reviewed, estimated
                '   the democracy term sign seems wrong
                '        SFPrediction = 1.4757 + 1.0011 * Log(InfMorTerm) - _
                ' 0.7414 * Log(TradeTerm) + 0.0316 * DemocTerm ' Original SF Project Eqn

                '    Call AnalFunc("State Failure from Regime Instability, year 1",
                'Array(GDPPCP(R%), democterm, infmorterm, Tradeterm, educ15term), Prediction,
                'Array("GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25")) 'jrs 2014/10/09 added DepVar Names to check order

                Call AnalFunc("State Failure from Regime Instability, year 1",
                    New Single() {GDPPCP(R%), democterm, infmorterm, Tradeterm, educ15term}, Prediction,
                    New String() {"GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25"}, context)


                Prediction = AMAX(0.001, Prediction)
                historybased = AMAX(0.0001, Amin(1, SFINSTABY1RI(R%) * Prediction))
                If sfusehist < 0.5 Then
                    'jrs 2010/01/18 SFINSTABY1(R%) = AMAX(0.0001, Amin(1, prediction))
                ElseIf sfusehist > 1.5 Then
                    'jrs 2010/01/18 SFINSTABY1(R%) = ConvergeOverTime(HistoryBased, prediction, sfconv)
                    'Base, Target, Years
                Else
                    'jrs 2010/01/18 SFINSTABY1(R%) = HistoryBased
                End If

                'Call AnalFunc("State Failure from Regime Instability, all years",
                '    Array(GDPPCP(R%), democterm, infmorterm, Tradeterm, educ15term), Prediction,
                '    Array("GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25")) 'jrs 2014/10/09 added DepVar Names to check order

                Call AnalFunc("State Failure from Regime Instability, all years",
                    New Single() {GDPPCP(R%), democterm, infmorterm, Tradeterm, educ15term}, Prediction,
                    New String() {"GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25"}, context)


                Prediction = AMAX(0.001, Prediction)
                historybased = AMAX(0.0001, Amin(1, SFINSTABALLRI(R%) * Prediction))
                If sfusehist < 0.5 Then
                    SFINSTABALL(R%) = AMAX(0.0001, Amin(1, Prediction))
                ElseIf sfusehist > 1.5 Then
                    SFINSTABALL(R%) = ConvergeOverTime(historybased, Prediction, sfconv)
                    'Base, Target, Years
                Else
                    SFINSTABALL(R%) = historybased
                End If

                'Call AnalFunc("State Failure from Regime Instability, magnitude",
                '    Array(GDPPCP(R%), democterm, infmorterm, Tradeterm, educ15term), Prediction,
                '    Array("GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25")) 'jrs 2014/10/09 added DepVar Names to check order

                Call AnalFunc("State Failure from Regime Instability, magnitude",
                    New Single() {GDPPCP(R%), democterm, infmorterm, Tradeterm, educ15term}, Prediction,
                    New String() {"GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25"}, context)

                Prediction = AMAX(0.001, Prediction)
                historybased = AMAX(0.0001, Amin(5, SFINSTABMAGRI(R%) * Prediction))
                If sfusehist < 0.5 Then
                    SFINSTABMAG(R%) = AMAX(0.0001, Amin(1, Prediction))
                ElseIf sfusehist > 1.5 Then
                    SFINSTABMAG(R%) = ConvergeOverTime(historybased, Prediction, sfconv)
                    'Base, Target, Years
                Else
                    SFINSTABMAG(R%) = historybased
                End If

                'Call AnalFunc("State Failure from Internal War, year 1",
                '    Array(GDPPCP(R%), democterm, infmorterm, Tradeterm, educ15term), Prediction,
                '    Array("GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25")) 'jrs 2014/10/09 added DepVar Names to check order

                Call AnalFunc("State Failure from Internal War, year 1",
                    New Single() {GDPPCP(R%), democterm, infmorterm, Tradeterm, educ15term}, Prediction,
                    New String() {"GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25"}, context)


                Prediction = AMAX(0.001, Prediction)
                historybased = AMAX(0.0001, Amin(1, SFINTLWARY1RI(R%) * Prediction))
                If sfusehist < 0.5 Then
                    'jrs 2010/01/18 SFINTLWARY1(R%) = AMAX(0.0001, Amin(1, prediction))
                ElseIf sfusehist > 1.5 Then
                    'jrs 2010/01/18 SFINTLWARY1(R%) = ConvergeOverTime(HistoryBased, prediction, sfconv)
                    'Base, Target, Years
                Else
                    'jrs 2010/01/18 SFINTLWARY1(R%) = HistoryBased
                End If

                'bbh 07/29/2011
                '       Call AnalFunc("State Failure from Internal War, all years", _
                '            Array(GDPPCP(R%), DEMOCterm, infmorterm, TradeTerm, educ15term), Prediction)
                'Call AnalFunc("State Failure from Internal War, all years",
                '    Array(GDPPCP(R%), democterm, INFMOR(R%), TradeTerm, educ15term), Prediction,
                '    Array("GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25")) 'jrs 2014/10/09 added DepVar Names to check order

                Call AnalFunc("State Failure from Internal War, all years",
                    New Single() {GDPPCP(R%), democterm, INFMOR(R%), Tradeterm, educ15term}, Prediction,
                    New String() {"GDPPCP", "POLITY", "INFMOR", "tradeopen", "Educ25"}, context)


                'bbh 07/29/2011 end

                Prediction = AMAX(0.001, Prediction)
                'If RegName$(R%) = "Azerbaijan" Then Stop
                'bbh 2011/06/18 begin
                historybased = AMAX(0.0001, Amin(1, SFINTLWARALLADD(R%) + Prediction))
                'If GRPS(1, R%) = "Russian Federation" Then Stop
                If sfusehist < 0.5 Then  'use analytical formulation only; no adjustment to history
                    SFINTLWARALL(R%) = AMAX(0.0001, Amin(1, Prediction))
                ElseIf sfusehist > 1.5 Then 'use analytical formulation adjusted to history, converging to analystic only
                    SFINTLWARALL(R%) = ConvergeOverTime(historybased, Prediction, sfconv)
                    'Base, Target, Years
                Else 'use analytical formulation adjusted to history
                    SFINTLWARALL(R%) = historybased
                End If
                'bbh 2011/06/18 end

                'jrs 2024/05/17 *** start ***
                'jrs 2024/08/20 removed Ythbulge and GDPGRowth
                'Call AnalFunc("State Failure from Internal War UCDP",
                'New Single() {INFMOR(R%), Tradeterm, YTHBULGE(R%) * 100, CSng((GDPR(R%) + 0.6 * SGDPR(R%)) / 2), ContiguityIntlWarAdd(R%), DEMOCPOLYARCHY(R%) * 10, CSng((10 * DEMOCPOLYARCHY(R%)) ^ 2)}, Prediction,
                'New String() {"IMR", "Tradeopen", "Ythbulge", "Gdpgrowth_ma60", "Neighboringconflict", "Polyarchy", "PolyarchySqr"}, context)
                Call AnalFunc("State Failure from Internal War UCDP",
                New Single() {INFMOR(R%), Tradeterm, ContiguityCivilWarUCDPAdd(R%), DEMOCPOLYARCHY(R%) * 10, CSng((10 * DEMOCPOLYARCHY(R%)) ^ 2)}, Prediction,
                New String() {"IMR", "Tradeopen", "Neighboringconflict", "Polyarchy", "PolyarchySqr"}, context)

                Prediction = CSng(Exp(Prediction) / (1 + Exp(Prediction)))
                SFCIVILWARUCDP(R%) = SFCIVILWARUCDPADD(R%) * Prediction
                SFCIVILWARUCDP(R%) = AMAX(0.001, SFCIVILWARUCDP(R%))
                SFCIVILWARUCDP(R%) = Amin(1, SFCIVILWARUCDP(R%))
                'jrs 2024/05/23
                'If GRPS(1, R%) = "Russian Federation" Then
                '    Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "SFCIVILWARUCDP.txt", append:=True)
                '    objWriter.WriteLine(ContiguityIntlWarAdd(R%).ToString)
                '    objWriter.Close()
                'End If
                'jrs 2024/05/17 *** end ***

                ''DSB 2026/1/15 *** start ****
                ''---------------------------------------------------------------
                ''Percent of Population Impacted calculated using OLS
                ''All the drivers are lagged by one year
                ''---------------------------------------------------------------
                'If R% = 1 Or R% = 180 Then Stop
                'Call AnalFunc("State Failure Percent of Population Impacted (2026)",
                '              New Single() {POP_Lag(R%), SSFCIVILWARPARDEE(R%), SVDTHSWOMENCHILDREN_Lag1(R%), GOVEFFECT_Lag1(R%)}, PredictionPOPIMP,
                '              New String() {"POP_Lag1", "SFCIVILWARPARDEE_Lag1", "SVDTHS_Lag1", "GOVEFFECT_Lag1"}, context)
                'SFCIVILWARPOPIMP(R%) = PredictionPOPIMP
                'SFCIVILWARPOPIMP(R%) = AMAX(0, Amin(100, SFCIVILWARPOPIMP(R%)))
                'SFCIVILWARPOPIMP(R%) = SFCIVILWARPOPIMP(R%)
                'SSFCIVILWARPOPIMP(R%) = SFCIVILWARPOPIMP(R%)
                ''DSB 2026/1/15 *** end ****

                'DSB 2026/1/8 *** start ****

                Call AnalFunc("State Failure from Internal War Pardee Onset (2026)",
                              New Single() {INFMOR(R%), Tradeterm, ContiguityCivilWarPardeeAdd(R%), DEMOCPOLYARCHY(R%) * 10, CSng((10 * DEMOCPOLYARCHY(R%)) ^ 2), yearCivilWarPardee, CSng(yearCivilWarPardee ^ 2)}, PredictionPardee,
                              New String() {"IMR", "Tradeopen", "NeighboringconflictPardee", "Polyarchy", "PolyarchySqr", "Year", "YearSquared"}, context)
                PredictionPardee = CSng(Exp(PredictionPardee) / (1 + Exp(PredictionPardee)))
                SFCIVILWARPARDEE(R%) = SFCIVILWARPARDEEADD(R%) * PredictionPardee
                SFCIVILWARPARDEE(R%) = AMAX(0.001, SFCIVILWARPARDEE(R%))
                SFCIVILWARPARDEE(R%) = Amin(1, SFCIVILWARPARDEE(R%))
                'SSFCIVILWARPARDEE(R%) = SFCIVILWARPARDEE(R%)
                'DSB 2026/1/8 *** End ****

                'DSB 2026/1/13 *** Start ****
                '---------------------------------------------------------------
                'Forecast years: Converge shift to 0 and generate new percent of population affected

                'p = SFCIVILWARPARDEE(R%) 'conflict probability (0 - 1)
                'shiftCivWarPardee = ConvergeOverTime(SFCIVILWARPOPIMPSHIFTBASE(R%), 0, 2100 - FirstYear + 10) 'Base, Target, Years
                'SFCIVILWARPOPIMP(R%) = (100 * p) + shiftCivWarPardee

                ''Bound between 0 and 100%
                'SFCIVILWARPOPIMP(R%) = AMAX(0, Amin(100, SFCIVILWARPOPIMP(R%)))
                'SFCIVILWARPOPIMP(R%) = SFCIVILWARPOPIMP(R%) / 100
                'SSFCIVILWARPOPIMP(R%) = SFCIVILWARPOPIMP(R%) 'save for next year's convergence
                'DSB 2026/1/13 *** End ****

                'jrs 2025/08/27 *** start ***
                '   All but First 2: Politically Relevant Dyad Security Count
                '   ------------------------------------------------------------
                Dim conflictIndex As Single 'dsb 2026/08/03 After asking in the modeling pod meeting, Though it was estimated using the UCDP data, the switch is added to use the probability measure based on the switch as the coefficients are going to be very similar
                If sfcivilwarucdpsw = 2 Then
                    conflictIndex = SFINTLWARALL(R%)
                ElseIf sfcivilwarucdpsw = 3 Then
                    conflictIndex = SFCIVILWARPARDEE(R%)
                Else
                    conflictIndex = SFCIVILWARUCDP(R%)
                End If
                Call AnalFunc("Politically Relevant Dyad Security Count",
                New Single() {AvgDistanceAtoB(R%), wpsovcount, POWER(R%), POP(R%), LIFEXP(R%, 3), conflictIndex}, Prediction,
                New String() {"AvgDistanceAtoB", "Sovereign_Count_Series", "POWER", "Population", "LifeExpBothSexes", "SFCIVILWARUCDP"}, context)
                Prediction = CSng(AMAX(Prediction, 1))
                If SPRDSECFX(R%) <> 0 Then
                    PRDSEC(R%) = SPRDSEC(R%) * Prediction / SPRDSECFX(R%)
                    PRDSEC(R%) = Amin(PRDSEC(R%), wpsovcount)
                Else
                    PRDSEC(R%) = 0
                End If
                SPRDSECFX(R%) = Prediction
                SPRDSEC(R%) = PRDSEC(R%)
                'jrs 2025/08/27 *** end ***


                'BBH 2011/04/02 Begin

                '   All but First 2: Add Dynamic adjustment terms for calculation of Internal War
                '   ------------------------------------------------------------

                'BBH 2011/06/18
                If IY% = 2 Then ssfintlwarallLoc(R%) = SFINTLWARALL(R%) 'SFINSTABALL(R%)

                If DEMOCPOLITY(R%) < 9 Then
                    DemocAddCur = CSng(0.1 + DEMOCPOLITY(R%) / 9 * 0.2)
                Else
                    DemocAddCur = CSng((20 - DEMOCPOLITY(R%)) / 11 * 0.3)
                End If
                If DEMOCPOLITYI(R%) < 9 Then
                    DemocAddI = CSng(0.1 + DEMOCPOLITYI(R%) / 9 * 0.2)
                Else
                    DemocAddI = CSng((20 - DEMOCPOLITYI(R%)) / 11 * 0.3)
                End If
                DemocAdd = DemocAddCur - DemocAddI

                YthBulgeAddCur = CSng(-0.141 + 0.7623 * YTHBULGE(R%))
                YthBulgeAddI = CSng(-0.141 + 0.7623 * YTHBULGEI(R%))
                ythbulgeadd = YthBulgeAddCur - YthBulgeAddI

                growthratediff = gdprmovavg(R%) - GDPR(R%)
                gdprmovavg(R%) = (gdprmovavg(R%) * 4 + GDPR(R%)) / 5
                If growthratediff > 0 Then
                    econgrowthadd = CSng(growthratediff * 0.04)
                Else
                    econgrowthadd = 0
                End If
                If econgrowthadd > 0 Then
                    econgrowthadd = econgrowthadd * AMAX(0, (18 - GDPPCP(R%)) / 18)
                End If
                If ythbulgeadd > 0 And GDPPCP(R%) > 10 Then 'phase down Youth Bulge impact as approach GDPPCP of $20K
                    ythbulgeadd = ythbulgeadd * AMAX(0, (20 - GDPPCP(R%)) / 10)
                End If

                extintervadd = wpextinterv(R%)
                '        If frm_MainMenu.IFsGUIctrl1.cHistoricBase = True Then
                '            If (RegName$(R%) = "Vietnam" Or RegName$(R%) = "Cambodia" Or RegName$(R%) = "Laos") And IY% >= 1 And IY% <= 12 Then
                '                ExtIntervAdd = 1
                '            End If
                '            If (RegName$(R%) = "Thailand") And IY% >= 6 And IY% <= 12 Then
                '                ExtIntervAdd = 1
                '            End If
                '            If (RegName$(R%) = "Afghanistan" Or RegName$(R%) = "Pakistan") And IY% >= 30 Then
                '                ExtIntervAdd = 1
                '            End If
                '            If (RegName$(R%) = "Iraq") And IY% >= 43 Then
                '                ExtIntervAdd = 1
                '            End If
                '        End If


                'If RegName$(R%) = "Azerbaijan" Then Stop
                SFINTLWARALL(R%) = AMAX(0, Amin(1, SFINTLWARALL(R%) + DemocAdd + ythbulgeadd + econgrowthadd + extintervadd))
                If SFINTLWARALL(R%) < ssfintlwarallLoc(R%) Then  'Slow down decay when dropping, except for rich
                    increment = (ssfintlwarallLoc(R%) - SFINTLWARALL(R%)) * AMAX(0, (10 - GDPPCP(R%)) / 10)
                    SFINTLWARALL(R%) = CSng(SFINTLWARALL(R%) + 0.6 * increment)
                End If
                'If IY% > 40 And RegName$(R%) = "Iran" Then Stop
                'ssfintlwarallLoc(R%) = SFINTLWARALL(R%) 'Do before Contiguity addition so does not spread around the world
                ssfintlwarallLoc(R%) = AMAX(0, Amin(1, SFINTLWARALL(R%) + sfintlwaradd(R%))) 'dkb 2019/01/03 - conflict deaths not picking up exogenous shift in internal war

            Next R% 'break loop so that SFINTLWARALL is calculated for all regions before doing contiguity analysis
            'Otherwise calculation below will introduce run to run

            For R% = 1 To NR%
                'bbh 2011/06/18 begin
                '        contiguityadd = 0
                '        For ICount% = 1 To NR%
                '            If contiguity(R%, ICount%) <= 3 Then  '1= land contiguity; 2 = 0-12 miles; 3 = 12-24 miles
                '                If SFINTLWARALL(ICount%) >= 0.2 Then  'Enough risk next door to add to yours
                '                    contiguityadd = contiguityadd + 0.1
                '                End If
                '            End If
                '        Next ICount%
                '        contiguityadd = Amin(0.4, contiguityadd)
                '        If contiguityadd > 0 And GDPPCP(R%) > 10 Then 'phase down Contiguity impact as approach GDPPCP of $15K
                '            contiguityadd = contiguityadd * AMAX(0, (15 - GDPPCP(R%)) / 5)

                '        SFINTLWARALL(R%) = Amin(1, SFINTLWARALL(R%) + contiguityadd)
                '        End If
                'If RegName$(R%) = "Azerbaijan" Then Stop
                'If GRPS(1, R%) = "Nigeria" And FirstYear + IY - 1 >= 2056 Then Stop
                SFINTLWARALL(R%) = AMAX(0, Amin(1, SFINTLWARALL(R%) + ContiguityIntlWarAdd(R%) - ContiguityIntlWarAddI(R%)))
                'bbh 2011/06/18 begin
                'If RegName(R%) = "Kazakhstan" Then Stop
                'jrs 2024/05/20
                If sfcivilwarucdpsw = 0 Or sfcivilwarucdpsw = 2 Then
                    'bbh 2011/10/5
                    If sfmassrep(R%) >= 0.01 Then SFINTLWARALL(R%) = SFINTLWARALL(R%) * sfmassrep(R%)
                    'If sfmassrep(R%) > 0.5 Then SFINTLWARALL(R%) = SFINTLWARALL(R%) * 0.1
                    SFINTLWARALL(R%) = AMAX(0, Amin(1, SFINTLWARALL(R%) + sfintlwaradd(R%)))
                ElseIf sfcivilwarucdpsw = 1 Then
                    If sfmassrep(R%) >= 0.01 Then SFCIVILWARUCDP(R%) = SFCIVILWARUCDP(R%) * sfmassrep(R%)

                    SFCIVILWARUCDP(R%) = AMAX(0, Amin(1, SFCIVILWARUCDP(R%) + sfintlwaradd(R%)))
                ElseIf sfcivilwarucdpsw = 3 Then 'dsb 2026/07/21
                    'dsb 2026/07/29 Moved the additive adjustment to Pardee variable to here and replaced sfcivilwaradd with sfintlwaradd to keep them consistent with the other internal war variables
                    SFCIVILWARPARDEE(R%) = AMAX(0, Amin(1, SFCIVILWARPARDEE(R%) + sfintlwaradd(R%)))
                    If sfmassrep(R%) >= 0.01 Then SFCIVILWARPARDEE(R%) = SFCIVILWARPARDEE(R%) * sfmassrep(R%)
                End If
                'BBH 2011/04/02 end
                'If IY% > 40 And RegName$(R%) = "Iran" Then Stop

                'jrs 2021/08/04 Exogenous Models
                If ifsexomodelid > 0 Then
                    Dim old As Single
                    old = SFINTLWARALL(R%)
                    ReplaceVarWithExogenousForecastxReg("SFINTLWARALL", "", "", CStr(FirstYear + IY - 1), R%, context)
                    'if there's a change update saved variable
                    If old <> SFINTLWARALL(R%) Then
                        ssfintlwarallLoc(R%) = AMAX(0, Amin(1, SFINTLWARALL(R%) + sfintlwaradd(R%)))
                    End If
                    'jrs 2024/05/20
                    ReplaceVarWithExogenousForecastxReg("SFCIVILWARUCDP", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                '       bbh 2011/09/21 Tie change in magnitude to change in probability instead of simple, separate function
                '        Call AnalFunc("State Failure from Internal War, magnitude", _
                '            Array(GDPPCP(R%), democterm, infmorterm, TradeTerm, educ15term), prediction)
                '        prediction = AMAX(0.001, prediction)
                '        historybased = AMAX(0.0001, Amin(5, SFINTLWARMAGRI(R%) * prediction))
                '        If sfusehist < 0.5 Then
                '            SFINTLWARMAG(R%) = AMAX(0.0001, Amin(1, prediction))
                '        ElseIf sfusehist > 1.5 Then
                '            SFINTLWARMAG(R%) = ConvergeOverTime(historybased, prediction, sfconv)
                '                'Base, Target, Years
                '        Else
                '            SFINTLWARMAG(R%) = historybased
                '        End If
                'SFINTLWARMAG(R%) = SFINTLWARMAGI(R%) * Amin(3, SFINTLWARALL(R%) / SFINTLWARALLI(R%))
                SFINTLWARMAG(R%) = SFINTLWARMAGI(R%) * Amin(3, SFINTLWARALL(R%) / SFINTLWARALLI(R%)) * sfintlwarmagm(R%) 'dkb 2019/01/22 - adding multiplier on internal war magnitude
                'jrs 2016/01/13
                SSFINTLWARMAG(R%) = SFINTLWARMAG(R%)
                'dsb 2026/06/04
                SSFCIVILWARUCDP(R%) = SFCIVILWARUCDP(R%)
                SSFCIVILWARPARDEE(R%) = SFCIVILWARPARDEE(R%)
                SSFINTLWARALL(R%) = SFINTLWARALL(R%)

                '   All but First 2:  Save Education Years for Govt Effectiveness
                '   -------------------------------------------------------------

                sedyrsag15(R%) = EDYRSAG15(R%, 3)
                'bbh 2011/08/19
                sedyrsag15fem(R%) = EDYRSAG15(R%, 2)



            Next R%

            'JAM 2019-08-26
            Call IfsDomPolAllSteps(context)

        End Sub

        'jrs 2024/06/11
        Dim TFDemocracy As String
        'jrs 2024/06/20
        Dim TFLifExp As String

        Public Sub IfsDomPolAllSteps(context As IForecastContext)
            '     All Steps:  Knowledge Society
            '     ------------------------------
            Dim Est As Single
            Dim Auto As Single
            Dim PartAuto As Single
            Dim PartAuto2 As Single
            Dim PartDem As Single
            Dim PartDem2 As Single
            Dim EstDem As Single
            Dim EstLif As Single
            Dim EstYth As Single


            If IY% = 1 Then
                'ttt 2019/10/08   Take Out
                Array.Copy(RANDDEXP, RAndDExpI, RAndDExpI.Length)

                For R% = 1 To NR%
                    'ttt 2019/10/08 RAndDExpI(R%) = RANDDEXP(R%)
                    EdTerGRateI(R%) = EDTERGRATE(R%, 3)
                Next R%
                '        Call ScaleMe(RAndDExpI)
                '        Call ScaleMe(EdTerGRateI)
            End If
            'ttt 2019/10/08   Take Out
            Array.Copy(RANDDEXP, RandDExpScale, RandDExpScale.Length)
            For R% = 1 To NR%
                'ttt 2019/10/08 RandDExpScale(R%) = RANDDEXP(R%)
                EdTerGRateScale(R%) = EDTERGRATE(R%, 3)
            Next R%
            Call ScaleMeOrig(RandDExpScale, RAndDExpI)
            Call ScaleMeOrig(EdTerGRateScale, EdTerGRateI)
            For R% = 1 To NR%
                KNOWSOC(R%) = (RandDExpScale(R%) + EdTerGRateScale(R%)) / 2
            Next R%

            'jrs 2017/04/13
            'All Steps: State Failure Probability from Demographic, Economic, Governance and Structural pressure
            '-----------------------------------------------------------------------------------------------------
            'jrs 2024/06/11
            If IY = 1 Then
                TFDemocracy = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Democracy (all years) Log"
                TFDemocracy = getFunctionNameYrOrMstRct(TFDemocracy, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
                'jrs 2024/06/20
                TFLifExp = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Life Expectancy (all years) Log"
                TFLifExp = getFunctionNameYrOrMstRct(TFLifExp, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            End If

            For R = 1 To NR
                'Call AnalFunc("State Failure Probability from Demographic pressure (2015)", Array(INFMOR(R), POP(R), POPR(R), YTHBULGE(R) * 100, MIGRANTS(R)), Est, Array("INFMOR", "POP", "POPR", "YTHBULGE", "MIGRANTS"))

                Call AnalFunc("State Failure Probability from Demographic pressure (2015)",
                    New Single() {INFMOR(R), POP(R), POPR(R), YTHBULGE(R) * 100, MIGRANTS(R)}, Est,
                    New String() {"INFMOR", "POP", "POPR", "YTHBULGE", "MIGRANTS"}, context)

                SFDEM(R) = Est
                'the second driver is GDPPCR, GDP per Capita Growth
                '(GDPPC2 - GDPPC1)/GDPPC1 * 100
                'GDPPC1 = GDP1/POP1
                'GDPPC2 = GDP1*(1+GDPGR/100)/ (POP1*(1+POPGR/100))
                'Call AnalFunc("State Failure Probability from Economic pressure (2015)", Array(GDPPCP(R), ((1 + GDPR(R) / 100) * (1 - 1 / (1 + POPR(R) / 100)) / (POPR(R) / 100) - 1) * 100, LIFEXP(R, 3)), Est, Array("GDPPCP", "LOGGDPPCR", "LIFEXP"))

                Call AnalFunc("State Failure Probability from Economic pressure (2015)",
                    New Single() {GDPPCP(R), ((1 + GDPR(R) / 100) * (1 - 1 / (1 + POPR(R) / 100)) / (POPR(R) / 100) - 1) * 100, LIFEXP(R, 3)}, Est,
                    New String() {"GDPPCP", "LOGGDPPCR", "LIFEXP"}, context)

                SFECONDEV(R) = Est
                'jrs 2017/04/27
                Auto = 0
                PartAuto = 0
                PartAuto2 = 0
                PartDem = 0
                PartDem2 = 0
                'If GRPS(1, R) = "Singapore" Then Stop
                Select Case True
                    Case DEMOCPOLITY(R) - 10 <= -7
                        Auto = 1
                    Case DEMOCPOLITY(R) - 10 <= -4
                        PartAuto = 1
                    Case DEMOCPOLITY(R) - 10 <= 0
                        PartAuto2 = 1
                    Case DEMOCPOLITY(R) - 10 < 4
                        PartDem = 1
                    Case DEMOCPOLITY(R) - 10 < 7
                        PartDem2 = 1
                End Select

                'Call AnalFunc("State Failure Probability from Governance pressure (2015)", Array(Auto, PartAuto, PartAuto2, PartDem, PartDem2), Est, Array("AUTOCRACY", "PARTIALAUTOCRACY", "PARTIALAUTOCRACY2", "PARTIALDEMOCRACY", "PARTIALDEMOCRACY2"))


                Call AnalFunc("State Failure Probability from Governance pressure (2015)",
                    New Single() {Auto, PartAuto, PartAuto2, PartDem, PartDem2}, Est,
                    New String() {"AUTOCRACY", "PARTIALAUTOCRACY", "PARTIALAUTOCRACY2", "PARTIALDEMOCRACY", "PARTIALDEMOCRACY2"}, context)


                SFGOV(R) = Est

                'jrs 2024/06/11 
                'Call XYTABL("GDP/Capita (PPP 2011) Versus Democracy (all years) Log", GDPPCP(R%), EstDem, context)
                Call XYTABL(TFDemocracy, GDPPCP(R%), EstDem, context)

                'jrs 2024/06/20 Call XYTABL("GDP/Capita (PPP 2011) Versus Life Expectancy (all years) Log", GDPPCP(R%), EstLif, context)
                Call XYTABL(TFLifExp, GDPPCP(R%), EstLif, context)
                Call XYTABL("Democracy Versus  Youth bulge (Most Recent) Log", DEMOCPOLITY(R%) - 10, EstYth, context)

                'Call AnalFunc("State Failure Probability from Structural pressure (2015)", Array(DEMOCPOLITY(R) - 10 - EstDem, LIFEXP(R, 3) - EstLif, YTHBULGE(R) * 100 - EstYth), Est, Array("DEMOCPOLITY", "LIFEXP", "YTHBULGE"))


                Call AnalFunc("State Failure Probability from Structural pressure (2015)",
                    New Single() {DEMOCPOLITY(R) - 10 - EstDem, LIFEXP(R, 3) - EstLif, YTHBULGE(R) * 100 - EstYth}, Est,
                    New String() {"DEMOCPOLITY", "LIFEXP", "YTHBULGE"}, context)


                SFIMBAL(R) = Est
            Next R


        End Sub

        Dim XWBFLOWSIEd As Single
        Dim Reduc As Single
        Dim xwbflowsihl As Single
        Dim predrandd As Single

        Dim EmbassyMaxEstimate As Single
        Dim IGOMaxEstimate As Single
        Dim TreatyMaxEstimate As Single
        Dim EnTot As Single
        Dim E As Integer
        Dim wtradei As Single
        Dim GPowerNumb As Single
        Dim gpowerterm As Single
        Dim ContiguityTerm As Single
        Dim terdisputeterm As Single
        Dim PowerTerm As Single
        Dim PowerRatio As Single
        Dim MinDemoc As Single
        Dim DemocTermMin As Single
        Dim DemocDist As Single
        Dim democtermdist As Single
        Dim allyterm As Single
        Public ThreatTerm As Single 'clp 2024/07/10
        Dim Zamph As Single
        Dim SConcenTerm As Single
        Dim STradeTerm As Single

        Dim zCoke As Single
        Dim Zopiate As Single
        Dim Zpresopioid As Single

        Dim PrivHLExp As Single
        Dim Dflt_GDS_Trgt_Yr As Integer, Trgt_Yr As Integer

        'jrs 2024/06/06
        Dim TblNameMilExp As String, TblNameRandDExp As String
        'jrs 2024/06/24
        Public TblNameRandDPctGDP As String
        'jrs 2024/06/24
        Public TblNameYrsEduc15 As String

        Public Sub IFsIntlPol1FirstYear(context As IForecastContext)
            ReDim InfraCostGDSInfraRI(NR%), sHlthExpPcntGDP(NR)

            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            '   Copyright by Barry B. Hughes 1988-2012
            '   Model code available for review and education only
            '   Reproduction or incorporation in other software by permission only

            '
            '   INTERNATIONAL POLITICAL MODEL
            '
            '   Last changed by BBH: 2005

            'jrs 2007/03/23
            ' sErrorSub = "IFsIntlPol" & CStr(IFUN)


            'If IY% > 1 Then GoTo AllButFirst

            '
            '   FIRST TIME STEP ONLY
            '   -----------------------------------------------------------------------

            ReDim POWERI(NR%), FREEDOMI(NR%), DEMOCPOLITYI(NR%)
            ReDim gkmul(NR%), milspendrati(NR%), LenWar(NR%, NR%), PowerTranTermI(NR%, NR%)
            ReDim AllyI(NR%, NR%)
            ReDim nowarper(NR%, NR%), gpowertermI(NR%, NR%), TerDisputeI(NR%, NR%)
            ReDim ThreatIData(NR%, NR%), ThreatIPred(NR%, NR%)
            ReDim WarGDPOwn(NR%), WarGDPOpp(NR%), WarCPOWOwn(NR%), WarCPOWOpp(NR%)
            ReDim WarNPOWOwn(NR%), WarNPOWOpp(NR%), CPOWDM(NR%), NPOWDM(NR%)
            ReDim gk(NR%, NGovExp%), gkri(NR%, NGovExp%), gkcomp(NGovExp%), healthgdpri(NR%), RandDI(NR%)
            ReDim SCPOW(NR%), SNPOW(NR%), sumthreatI(NR%), SGDSMil(NR%)
            'jrs 2021/09/09
            ReDim educgdpri(NR%)
            ReDim BlThreatInital_PwrRatio(NR%, NR%), BlThreatInital_TrdShr(NR%, NR%), BlThreatInital_AffGap(NR%, NR%), IDyTrdShrGdp(NR%, NR%), BlThreatInital_DemMin(NR%, NR%) 'clp 2025/04/29 adding ability to keep values constant for THREAT components

            'clp 2016/08/26 - adding SGDSRandD
            ReDim SGDSRandD(NR%)
            ReDim govconwb(NR%), gdswb(NR%, NGovExp%), RandDExpSh(NR%)
            'BBH 4/16/04  V5.0
            ReDim SMORTMG(NR%)

            'mti January 06, 2009
            ReDim EdCostGDSEdRI(NR%), SEdTotCost(NR%), EdCostGDSEdRIShift(NR%)
            'mti January 06, 2009

            ReDim InfraCostGDSInfraRI(NR%), sInfraTotCost(NR%), InfraCostGDSInfraRIShift(NR%)



            'mti February 2011 Infarstructure - start
            ReDim sHlthExpPcntGDP(NR%)
            'mti February 2011 Infarstructure - end

            'jrs 2011/10/11
            ReDim SRANDDEXP(NR%)
            ReDim SGOVNUCWP(NR%)
            'jrs 2012/11/05 begin
            ReDim DIPLOEMBASSYShift(NR%)
            ReDim DIPLOIGOShift(NR%)
            ReDim DIPLOTREATYShift(NR%)
            'jrs 2012/11/05 end

            'jrs 2016/08/02 *** begin ***
            ReDim HTCTPIShift(NR%)
            ReDim HTCGRIShift(NR%)
            'jrs 2016/08/02 *** end ***

            'dkb 2014/08/06 - adding save variables for Diplometrics POWER component calculation - START
            ReDim SDIPLOEMBASSY(NR%), SDIPLOIGO(NR%), SDIPLOTREATY(NR%)
            'ReDim SDIPLOEMBASSYMAX, SDIPLOIGOMAX, SDIPLOTREATYMAX
            'dkb 2014/08/06 - END

            'jrs 2022/04/20
            ReDim SDIPLOEMBASSY2(NR%)

            'jrs 2014/05/26
            ReDim HlGrTerm(NR) 'jrs 2014/05/30 HLEXPENDPRIVShift(NR) As Single
            'jrs 2014/05/30 PrivHLExpPctGDPFxName = "GDP/Capita (PPP 2005) Versus Private Health Expenditures % of GDP (2012) Log"
            'jrs 2014/05/30 PrivHLExpPctGDPFxName = getFunctionNameYrOrMstRct(PrivHLExpPctGDPFxName, BaseYear)

            'jrs 2016/01/18 *** begin ***
            ReDim DIPLOINGOPROGRAMShift(NR%), DIPLOINGOADVOCACYShift(NR%)
            ReDim UKcol(NR%), FRAcol(NR%), GERcol(NR%), PORcol(NR%), SPAcol(NR%), NETcol(NR%), RUScol(NR%)

            'mti 2016/07/16 Target value in GDS%GDP start
            'Declare array variable for saving initial values and target GDS values (when targeting is set as a parameter)
            ReDim igk(NR%, NGovExp%), GDSTrgt(NR%, NGovExp%)
            ReDim gdssetaside(NGovExp%)
            'mti 2016/07/16 End

            'ReDim DrugPrescriptionOpioidShift(NR%), SDRUGUSEPRESCRIPTOPIOID(NR%) 'kbn 2018/02/16
            'kbn 2018/05/15-Start-
            'Redimension for drug model variables
            ReDim drugtotalest(NR%)
            ReDim sdrugtotalest(NR%)
            ReDim DrugAmphetamineEst(NR%), DrugCocaineEst(NR%), DrugOpiateEst(NR%), DrugPrescriptionOpioidEst(NR%), DrugEstTot(NR%)
            ReDim sDrugAmphetamineEst(NR%), sDrugCocaineEst(NR%), sDrugOpiateEst(NR%), sDrugPrescriptionOpioidEst(NR%), sDrugEstTot(NR%)
            ReDim iDrugAmphetamineEst(NR%), iDrugCocaineEst(NR%), iDrugOpiateEst(NR%), iDrugPrescriptionOpioidEst(NR%), iDrugEstTot(NR%)
            ReDim SDRUGUSEAMPHETAMINE(NR%), SDRUGUSECOCAINE(NR%), SDRUGUSEOPIATES(NR%), SDRUGUSEPRESCRIPTOPIOID(NR%)
            ReDim iDrugUseAmphetamine(NR%), iDrugUseCocaine(NR%), iDrugUseOpiates(NR%), iDrugUsePrescriptOpioid(NR%), iDrugUseTot(NR%)
            ReDim hhconpc(NR%), shhconpc(NR%)
            'kbn 2018/05/15-End

            'Load Colony Data
            LoadColonyData(UKcol, FRAcol, GERcol, PORcol, SPAcol, NETcol, RUScol)
            'jrs 2016/01/18 *** end ***


            '
            '   First Year: Initial Global Community, Freedom, Empowerment, WVS Levels
            '   ----------------------------------------------------------------------

            'jrs 2024/06/06
            TblNameMilExp = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Govt Exp Mil as % of GDP (" & BaseYear & ") Linear"
            TblNameMilExp = getFunctionNameYrOrMstRct(TblNameMilExp, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TblNameRandDExp = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Govt Exp R&D as % of GNP (" & BaseYear & ") Linear"
            TblNameRandDExp = getFunctionNameYrOrMstRct(TblNameRandDExp, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            'jrs 2024/06/28
            TblNameRandDPctGDP = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus R&D Total  as % of GDP (" & BaseYear & ") Linear"
            TblNameRandDPctGDP = getFunctionNameYrOrMstRct(TblNameRandDPctGDP, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TblNameYrsEduc15 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Years of Educ at 15+ (" & BaseYear & ") Log"
            TblNameYrsEduc15 = getFunctionNameYrOrMstRct(TblNameYrsEduc15, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")

            GLOCOMI = glocom
            For R% = 1 To NR%
                FREEDOMI(R%) = FREEDOM(R%)
                DEMOCPOLITYI(R%) = DEMOCPOLITY(R%)

                '   First Year: Initial Conventional Power, Other Government Expenditures
                '   ---------------------------------------------------------------------
                CPOW(R%) = GDS(R%, Mil) *
                (1 - nmilf(R%)) * cpowf(R%) / AMAX(0.001, igdpr(R%) + drcpow)
                '
                '   First Year: Save Levels of WB Spending in Government Categories
                '   --------------------------------------------------------------

                'Following adjustment logic must exactly parallel that in IFsEco

                XWBFLOWSIEd = XWBFLOWS(R%, 1)
                If XWBFLOWS(R%, 1) > 0.9 * GDS(R%, Educ) Then
                    'compare flows to educ with expendutures on it
                    Reduc = CSng(XWBFLOWS(R%, 1) - 0.9 * GDS(R%, Educ))
                    XWBFLOWSIEd = XWBFLOWS(R%, 1) - Reduc
                End If
                xwbflowsihl = XWBFLOWS(R%, 2)
                If XWBFLOWS(R%, 2) > 0.9 * GDS(R%, Health) Then
                    'compare flows to health with expendutures on it
                    Reduc = CSng(XWBFLOWS(R%, 2) - 0.9 * GDS(R%, Health))
                    xwbflowsihl = XWBFLOWS(R%, 2) - Reduc
                End If

                For S% = 1 To NGovExp% '(up through other)
                    gdswb(R%, S%) = 0
                Next S%
                govconwb(R%) = XWBFLOWSIEd + xwbflowsihl
                'gdswb(R%, Educ) = XXWBFLOWSIEd 'Education
                '   BBH 8/18/04 Education Expenditures
                gdswb(R%, Educ) = XWBFLOWSIEd 'Education
                gdswb(R%, Health) = xwbflowsihl  'Health
                '
                '   First Year: Compute Rates of Spending in Government Categories
                '   --------------------------------------------------------------

                For S% = 1 To NGovExp% '(up through other)
                    'gk(R%, S%) = (GDS(R%, S%) - gdswb(R%, S%)) / AMAX(0.001, GOVCON(R%) - govconwb(R%))
                    gk(R%, S%) = (GDS(R%, S%) - gdswb(R%, S%)) / AMAX(0.001, GDP(R%)) * 100
                    'Other expenditure (cat 5) includes aid for donors
                    'GOVCON includes aid for recipients
                Next S%
                If context.HistoricMode = True Then
                    Call XYTABL("GDP/Capita (PPP) Versus Govt Exp R&D as % of GNP (2000) Linear",
                    Amin(40, GDPPCP(R%)), gkcomp(RandD), context)
                    Call XYTABL("GDP/Capita (PPP) Versus R&D Total  as % of GNI (1980) - linear",
                    Amin(40, GDPPCP(R%)), predrandd, context)
                    Call XYTABL("GDP/Capita (PPP) Versus Govt Exp Mil as % of GDP (1965) - linear",
                Amin(40, GDPPCP(R%)), gkcomp(Mil), context)
                    Call XYTABL("GDP/Capita (PPP) Versus Govt Exp Hlth as % of GDP (1990) - log",
                Amin(40, GDPPCP(R%)), gkcomp(Health), context)
                    Call XYTABL("GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (1960) - log",
                Amin(40, GDPPCP(R%)), gkcomp(Educ), context)
                Else
                    'jrs 2024/06/06 
                    'Call XYTABL("GDP/Capita (PPP) Versus Govt Exp R&D as % of GNP (2000) - linear",
                    'Amin(40, GDPPCP(R%)), gkcomp(RandD), context)
                    Call XYTABL(TblNameRandDExp, Amin(40, GDPPCP(R%)), gkcomp(RandD), context)
                    'jrs 2013/11/28
                    '            Call XYTABL("GDP/Capita (PPP) Versus R&D Total  as % of GNI (2000) - linear", _
                    '                    Amin(40, GDPPCP(R%)), PredRandD)
                    'jrs 2024/06/28 Call XYTABL("GDP/Capita (PPP 2005) Versus R&D Total  as % of GDP (2009) Linear",
                    'Amin(40, GDPPCP(R%)), predrandd, context)
                    Call XYTABL(TblNameRandDPctGDP, Amin(40, GDPPCP(R%)), predrandd, context)
                    'jrs 2024/06/06 
                    '    Call XYTABL("GDP/Capita (PPP) Versus Govt Exp Mil as % of GDP (2000) - linear",
                    'Amin(40, GDPPCP(R%)), gkcomp(Mil), context)
                    Call XYTABL(TblNameMilExp, Amin(40, GDPPCP(R%)), gkcomp(Mil), context)
                    'jrs 2024/06/06 
                    '    Call XYTABL("GDP/Capita (PPP) Versus Govt Exp Hlth as % of GDP (2000) - log",
                    'Amin(40, GDPPCP(R%)), gkcomp(Health), context)
                    Call XYTABL(TblNameHealthExp, Amin(40, GDPPCP(R%)), gkcomp(Health), context)
                    'jrs 2024/06/06 
                    '    Call XYTABL("GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (2002) - log",
                    'Amin(40, GDPPCP(R%)), gkcomp(Educ), context)
                    Call XYTABL(TblNameEducExp, Amin(40, GDPPCP(R%)), gkcomp(Educ), context)

                End If
                gkcomp(Other) = gk(R%, Other)

                'mti Sep 12, 2011 - start
                gkcomp(InfraOther) = CSng(1.8162 + 0.061 * Log(GDPPCP(R%)))
                'mti Sep 12, 2011 - end

                RandDExpSh(R%) = RANDDEXP(R%) - predrandd
                For S% = 1 To NGovExp% '(up through other)
                    gkri(R%, S%) = gk(R%, S%) / AMAX(0.001, gkcomp(S%))
                    'gkri(R%, S%) = gk(R%, S%) - gkcomp(S%)

                    'mti 2016/07/16 Target value in GDS%GDP start
                    'saving initial values of GDS as a % of GDP
                    igk(R%, S%) = gk(R%, S%)
                    'mti 2016/07/16 End

                Next S%


                SGDSMil(R%) = GDS(R%, Mil)

                'clp 2016/08/26 - adding save variable for R&D GDS
                SGDSRandD(R%) = GDS(R%, RandD)

                healthgdpri(R%) = GDS(R%, Health) / GDP(R%)
                RandDI(R%) = GDS(R%, RandD) / GDP(R%) * 100
                EdCostGDSEdRI(R%) = GDS(R%, Educ) / AMAX(0.001, EDTOTCOST(R%))
                'jrs 2021/09/09
                educgdpri(R%) = GDS(R%, Educ) / GDP(R%)

                'mti January 06, 2009
                EdCostGDSEdRIShift(R%) = CSng(EdCostGDSEdRI(R%) - 1.1)
                'mti January 06, 2009 - end

                SEdTotCost(R%) = EDTOTCOST(R%) 'no demand unconstrained calculation first year; take total cost from data


                'mti February 2011 Infarstructure -start

                '   First Year: Compute Public Spending in Infrastructure Categories
                '   ------------------------------------------------------------------

                'mti 2014/03/27 - start

                'The following branch will be activated once we have better historical data on infrastructure spending
                'We use a initial ratio of 1 now (see below)
                'sInfraTotCost(R%) = CalcInfraBudgetDemand(R%)
                'InfraCostGDSInfraRI(R%) = GDS(R%, Infra) / AMAX(0.001, sInfraTotCost(R%))
                'InfraCostGDSInfraRIShift(R%) = InfraCostGDSInfraRI(R%) - 1

                'Just force it to 1 for now; will remove useless codes later
                InfraCostGDSInfraRI(R%) = 1
                InfraCostGDSInfraRIShift(R%) = 0
                'mti 2014/03/27 - end


                sHlthExpPcntGDP(R%) = GDS(R%, Health) / GDP(R%) * 100


                'jrs 2011/10/11
                SGOVNUCWP(R%) = GOVNUCWP(R%)
                SHDINEW(R%) = HDI(R%)
                SRANDDEXP(R%) = RANDDEXP(R%)
            Next R%
            '
            '   First Year: Global Govt Size, Nuclear and Conventional Power
            '   -------------------------------------------------

            WGOV = 0
            WGDSMIL = 0
            'clp 2016/08/26 - intialize WGDSRandD
            WGDSRandD = 0
            WNPOW = 0
            WCPOW = 0
            WGDPPPP = 0
            WTECHP = 0
            WTECH = 0
            'jrs 2020/11/19
            WTECHmix = 0

            'jrs 2011/10/11
            WGOVNUCWP = 0
            WENX = 0
            'clp 2016/08/25 - new energy production variable
            WENProd = 0
            WENM = 0
            WTradeTotal = 0
            WXFDIFIN = 0

            'dkb 2014/10/23 - new EnergyNetExport specification - START
            WENXM = 0
            'dkb 2014/10/23 - new EnergyNetExport specification - END

            WAID = 0
            WHumCapWkPopHDI = 0
            WGOVREV = 0
            WInnovationRD = 0
            WConnectivityICTProd = 0

            'dkb 2013/09/09 creating components for world DPI - START
            WDPIBN = 0
            WDPIMN = 0
            WDPIILN = 0
            WDPI = 0
            'dkb 2013/09/09 - END

            'dkb 2014/07/03 creating components for world Effective Human Capital - START
            WEHC = 0
            'dkb 2014/07/03 - END

            'dkb 2014/08/01 - moved up from below, because DIPLOEMBASSYMAX, DIPLOIGOMAX, DIPLOTREATYMAX are _
            'used in WDPI - START
            'clp 2016/08/23 - Updating equations for estimation with Mickey's new equations
            'EmbassyMaxEstimate = 0.012 * (IY - 1 + FirstYear) - 20.663
            'IGOMaxEstimate = 0.1019 * (IY - 1 + FirstYear) - 398.46
            'TreatyMaxEstimate = 0.0505 * (IY - 1 + FirstYear) + 3.1859
            'mtr 2017/01/05 - updating logic for temporal inclusion of IVs in IGOMax and EmbassyMax
            'EmbassyMaxEstimate = 1.546716 * (IY - 1 + FirstYear) - 2907.35951
            'IGOMaxEstimate = 11.98167 * (IY - 1 + FirstYear) - 23036.023
            EmbassyMaxEstimate = CSng(0.3358688 * (IY - 1 + FirstYear) - 481.8743414)
            IGOMaxEstimate = CSng(7.380403 * (IY - 1 + FirstYear) - 13800.158803)
            'mtr 2017/01/05 *** end ***
            TreatyMaxEstimate = CSng(48.67768 * (IY - 1 + FirstYear) - 94913.4686)

            DIPLOEMBASSYMAXShift = DIPLOEMBASSYMAX - EmbassyMaxEstimate
            DIPLOIGOMAXShift = DIPLOIGOMAX - IGOMaxEstimate
            DIPLOTREATYMAXShift = DIPLOTREATYMAX - TreatyMaxEstimate

            'Apply multipliers
            DIPLOEMBASSYMAX = DIPLOEMBASSYMAX * diploembassymaxm
            DIPLOIGOMAX = DIPLOIGOMAX * diploigomaxm
            DIPLOTREATYMAX = DIPLOTREATYMAX * diplotreatymaxm
            'dkb 2014/08/01 - END

            'bbh 2011/11/19 Changes throughout R% Loop
            For R% = 1 To NR%
                SNPOW(R%) = NPOW(R%)
                SCPOW(R%) = CPOW(R%)
                WNPOW = WNPOW + NPOW(R%)
                WCPOW = WCPOW + CPOW(R%)
                WGOV = WGOV + GOVCON(R%)
                'WGDSMIL = WGDSMIL + SGDSMil(R%) * GDPP(R%) / GDP(R%)
                WGDSMIL = WGDSMIL + SGDSMil(R%)
                'clp 2016/08/26 - adding GDS R&D world sum
                WGDSRandD = WGDSRandD + SGDSRandD(R%)
                WGDPPPP = WGDPPPP + GDPP(R%)
                WTECHP = WTECHP + GDPP(R%) * GDPPCP(R%)
                WTECH = WTECH + GDP(R%) * GDPPC(R%)
                'jrs 2020/11/19
                WTECHmix = WTECHmix + GDP(R%) * GDPPCP(R%)

                'dkb 2014/07/07 unstandardizing components for world DPI - START
                'dkb 2013/09/09 creating components for world DPI - START
                'dkb 2013/09/09 - END
                '        WDPIBN = WDPIBN + ((SDIPLOEMBASSY(R%) * 32.48639) + 39.42253)
                '        WDPIMN = WDPIMN + ((SDIPLOIGO(R%) * 129.6466) + 244.7328)
                '        WDPIILN = WDPIILN + ((SDIPLOTREATY(R%) * 747.6514) + 906.7255)
                '        WDPI = WDPI + (((0.5 * (((SDIPLOEMBASSY(R%) * 32.48639) + 39.42253) / ((DIPLOEMBASSYMAX * 32.48639) + 39.42253))) + _
                '            (0.33 * ((SDIPLOIGO(R%) * 129.6466) + 244.7328) / ((DIPLOIGOMAX * 129.6466) + 244.7328)) + _
                '            (0.17 * ((SDIPLOTREATY(R%) * 747.6514) + 906.7255) / ((DIPLOTREATYMAX * 747.6514) + 906.7255))) * GOVREV(R%))
                'dkb 2014/07/07 - END

                'dkb 2014/10/27 - END
                'clp 2016/08/23 - removing unstandaradiztions; decision to move to absolute values
                'WDPIBN = WDPIBN + ((DIPLOEMBASSY(R%) * 32.48639) + 39.42253)
                'WDPIMN = WDPIMN + ((DIPLOIGO(R%) * 129.6466) + 244.7328)
                'WDPIILN = WDPIILN + ((DIPLOTREATY(R%) * 747.6514) + 906.7255)
                'WDPI = WDPI + (((0.5 * (((DIPLOEMBASSY(R%) * 32.48639) + 39.42253) / ((DIPLOEMBASSYMAX * 32.48639) + 39.42253))) + _
                '(0.33 * ((DIPLOIGO(R%) * 129.6466) + 244.7328) / ((DIPLOIGOMAX * 129.6466) + 244.7328)) + _
                '(0.17 * ((DIPLOTREATY(R%) * 747.6514) + 906.7255) / ((DIPLOTREATYMAX * 747.6514) + 906.7255))) * GOVREV(R%))

                WDPIBN = WDPIBN + DIPLOEMBASSY(R%)
                WDPIMN = WDPIMN + DIPLOIGO(R%)
                WDPIILN = WDPIILN + DIPLOTREATY(R%)

                'clp 2016/08/24 - Changing to match Tim's new formulation
                'WDPI = WDPI + ((0.5 * (DIPLOEMBASSY(R%) / DIPLOEMBASSYMAX) + _
                '(0.33 * DIPLOIGO(R%) / DIPLOIGOMAX) + _
                '(0.17 * DIPLOTREATY(R%) / DIPLOTREATYMAX)) * GOVREV(R%))
                WDPI = CSng(WDPI + ((((99.83 * (DIPLOEMBASSY(R%) / DIPLOEMBASSYMAX) ^ 2)) +
            (35.49 * (DIPLOIGO(R%) / DIPLOIGOMAX) ^ 2) +
            (7.34 * ((DIPLOTREATY(R%) / DIPLOTREATYMAX) ^ 1.5))) * GOVREV(R%)))
                'WDPI = WDPI + (((0.5 * (DIPLOEMBASSY(R%) / DIPLOEMBASSYMAX)) + (0.33 * (DIPLOIGO(R%) / DIPLOIGOMAX)) + _
                ' (0.17 * (DIPLOTREATY(R%) / DIPLOTREATYMAX))) * GOVREV(R%))
                'dkb 2014/10/27 - END

                'If RegName(R%) = "China" Then Stop
                'dkb 2014/07/07 creating components for world Effective Human Capital - START
                'WEHC = WEHC + ((POP15TO65(R%) - (SINCOMELT1LN(R%) * (POP15TO65(R%) / POP(R%))) - _
                ' (((SINCOMELT2LN(R%) - SINCOMELT1LN(R%)) / 2) * (POP15TO65(R%) / POP(R%)))) * SHDINEW(R%))
                'clp 2016/08/24 - Updating WEHC formula
                'jrs 2021/09/02 moving from 3.10 to 3.20/day
                'WEHC = WEHC + (((POP(R%) - SINCOMELT310LN(R%)) * (POP15TO65(R%) / POP(R%))) * SHDINEW(R%) * SHDINEW(R%))
                'jrs 2022/11/21
                If ConstantDollarsYear = 2011 Then
                    WEHC = WEHC + (((POP(R%) - SINCOMELT320LN(R%)) * (POP15TO65(R%) / POP(R%))) * SHDINEW(R%) * SHDINEW(R%))
                ElseIf ConstantDollarsYear = 2017 Then 'jrs 2025/09/12
                    WEHC = WEHC + (((POP(R%) - SINCOMELT365LN(R%)) * (POP15TO65(R%) / POP(R%))) * SHDINEW(R%) * SHDINEW(R%))
                Else 'jrs 2025/09/12
                    WEHC = WEHC + (((POP(R%) - SINCOMELT420LN(R%)) * (POP15TO65(R%) / POP(R%))) * SHDINEW(R%) * SHDINEW(R%))
                End If
                'dkb 2014/07/07 - END

                'jrs 2011/10/11
                'WGOVNUCWP = WGOVNUCWP + SGOVNUCWP(R%)
                If SGOVNUCWP(R%) > 0 Then
                    WGOVNUCWP = CSng(WGOVNUCWP + Log(SGOVNUCWP(R%)))
                End If
                'WConvMilSpend = WConvMilSpend

                'dkb 2014/08/20 - creating components for world energy trade, first year - START
                '        WENX = WENX + ENX(R%)
                '        WENM = WENM + ENM(R%)
                WENX = WENX + (XS(R%, Ener) / WEPBYEAR)
                WENM = WENM + (MS(R%, Ener) / WEPBYEAR)
                'dkb 2014/10/23 - new EnergyNetExport specification - START
                WENXM = WENXM + AMAX(0, (XS(R%, Ener) / WEPBYEAR) - (MS(R%, Ener) / WEPBYEAR))
                'dkb 2014/10/23 - new EnergyNetExport specification - END
                'dkb 2014/08/20 - END

                'clp 2016/08/25 - calculating world energy production
                EnTot = 0
                For E = 1 To Renew 'Oil, gas, coal, hydro, nuclear, renewable
                    If E <= Hydro Then
                        EnTot = EnTot + ENP(R%, E)
                        'bbh 2011/10/16
                        'Else
                        '   EnTot = EnTot + ENP(R%, e%) '* 0.5
                    End If
                Next E

                WENProd = WENProd + EnTot


                WTradeTotal = WTradeTotal + (X(R%) + M(R%))
                WXFDIFIN = WXFDIFIN + XFDIFIN(R%)

                If AID(R%) < 0 Then 'Sum aid of donors
                    WAID = WAID - AID(R%)
                End If
                'jrs 2013/04/12 WHumCapWkPopHDI = WHumCapWkPopHDI + POP15TO65(R%) * SHDINEW(R%)
                WHumCapWkPopHDI = WHumCapWkPopHDI + POPWORKING(R%) * SHDINEW(R%)
                WGOVREV = WGOVREV + GOVREV(R%)
                'WInnovationRD = WInnovationRD + SRANDDEXP(R%) * GDPP(R%)
                WInnovationRD = WInnovationRD + SRANDDEXP(R%) * GDP(R%)
                'WConnectivityICTProd = WConnectivityICTProd + ZS(R%, 6)
                WConnectivityICTProd = WConnectivityICTProd + KS(R%, 6)
            Next R%
            SWNPOW = WNPOW
            SWCPOW = WCPOW
            '
            '   First Year:  Power Index, Other Initializations
            '   -----------------------------------------------

            Call PowerCalculation(context)
            'jrs 2021/12/05 added switch
            If wppowhhsw = 0 Then
                Call PowerConcentration()
            Else
                Call PowerHHConcentration()
            End If

            PConGreatI = PCONGREAT
            wtradei = WTRADE
            'jrs 2011/02/14 *** begin Embeddedness ***
            Call Embeddedness(context)
            'jrs 2011/02/14 *** end Embeddedness ***


            For R% = 1 To NR%
                'jrs 2021/12/05 added switcH
                If wppowhhsw = 0 Then
                    POWERI(R%) = AMAX(0.01, POWER(R%))
                Else
                    POWERI(R%) = AMAX(0.01, POWERHH(R%))
                End If
                milspendrati(R%) = GDS(R%, Mil) / AMAX(0.01, GDP(R%))
                'If RegName(R%) = "Belgium" Then Stop

                For R2% = 1 To NR%
                    CWARPB(R2%, R%) = cwarbase(R2%, R%)
                    nowarper(R2%, R%) = 0
                Next R2%
            Next R%

            For tar% = 1 To NR%
                sumthreatI(tar%) = 0
                For act% = 1 To NR%

                    '           First Year: Great power term,  none, one or both
                    '           ------------------------------------------------

                    wpgreatthresh = Amin(wpgreatthresh, 25)
                    wpgreatlev = AMAX(wpgreatlev, wpgreatthresh + 1)

                    'jrs 2021/12/05 using POWERHH when switch is on
                    If wppowhhsw = 0 Then
                        If POWER(tar%) <= wpgreatthresh Then
                            GPowerNumb = 0
                        ElseIf POWER(tar%) >= wpgreatlev Then
                            GPowerNumb = 1
                        Else
                            GPowerNumb = (POWER(tar%) - wpgreatthresh) _
                        / AMAX(1, (wpgreatlev - wpgreatthresh))
                        End If

                        If POWER(act%) <= wpgreatthresh Then
                            GPowerNumb = GPowerNumb
                        ElseIf POWER(act%) >= wpgreatlev Then
                            GPowerNumb = GPowerNumb + 1
                        Else
                            GPowerNumb = GPowerNumb + (POWER(act%) - wpgreatthresh) _
                        / (wpgreatlev - wpgreatthresh)
                        End If
                    Else
                        If POWERHH(tar%) <= wpgreatthresh Then
                            GPowerNumb = 0
                        ElseIf POWERHH(tar%) >= wpgreatlev Then
                            GPowerNumb = 1
                        Else
                            GPowerNumb = (POWERHH(tar%) - wpgreatthresh) _
                        / AMAX(1, (wpgreatlev - wpgreatthresh))
                        End If

                        If POWERHH(act%) <= wpgreatthresh Then
                            GPowerNumb = GPowerNumb
                        ElseIf POWERHH(act%) >= wpgreatlev Then
                            GPowerNumb = GPowerNumb + 1
                        Else
                            GPowerNumb = GPowerNumb + (POWERHH(act%) - wpgreatthresh) _
                        / (wpgreatlev - wpgreatthresh)
                        End If
                    End If

                    If GPowerNumb <= 1 Then
                        gpowerterm = GPowerNumb * wpgreat1
                    Else
                        gpowerterm = wpgreat1 + (GPowerNumb - 1) * wpgreat2
                        'if 2 great powers, full effect
                    End If
                    gpowertermI(act%, tar%) = gpowerterm
                    gpowertermI(tar%, act%) = gpowerterm

                    '           First Year:  Contiguity term
                    '           ----------------------------

                    If contiguity(act%, tar%) = 1 Then
                        ContiguityTerm = wpcontiguity
                    Else
                        ContiguityTerm = 0
                    End If

                    '           First Year:  Territorial Dispute Term
                    '           -------------------------------------

                    terdisputeterm = terdispute(act%, tar%) * wpterdisp
                    TerDisputeI(act%, tar%) = terdisputeterm

                    '           First Year: Power transition/ratio term
                    '           ---------------------------------------

                    'jrs 2021/12/05
                    If wppowhhsw = 0 Then
                        If POWER(act%) >= wpgreatlev And POWER(tar%) >= wpgreatlev Then
                            PowerRatio = Amin(POWER(act%) / POWER(tar%), POWER(tar%) / POWER(act%))
                            If (PowerRatio >= wppowtran1) Then
                                PowerTerm = CSng(wppowtran2 * (PowerRatio - wppowtran1) / 0.1)
                                'term works on 10% intervals
                            Else
                                PowerTerm = 0
                            End If
                        Else
                            PowerTerm = 0
                        End If
                    Else
                        If POWERHH(act%) >= wpgreatlev And POWERHH(tar%) >= wpgreatlev Then
                            PowerRatio = Amin(POWERHH(act%) / POWERHH(tar%), POWERHH(tar%) / POWERHH(act%))
                            If (PowerRatio >= wppowtran1) Then
                                PowerTerm = CSng(wppowtran2 * (PowerRatio - wppowtran1) / 0.1)
                                'term works on 10% intervals
                            Else
                                PowerTerm = 0
                            End If
                        Else
                            PowerTerm = 0
                        End If
                    End If

                    PowerTranTermI(act%, tar%) = PowerTerm
                    PowerTranTermI(tar%, act%) = PowerTerm

                    '           First Year:  Dyadic democracy terms,  joint and political distance
                    '           --------------------------------------------------------------------

                    MinDemoc = Amin(DEMOCPOLITY(act%), DEMOCPOLITY(tar%))
                    DemocTermMin = MinDemoc * wpdemmin

                    DemocDist = Abs(DEMOCPOLITY(act%) - DEMOCPOLITY(tar%))
                    democtermdist = DemocDist * wpdemdist

                    '           First Year: alliance
                    '           --------------------

                    allyterm = AMAX(ally(act%, tar%), ally(tar%, act%)) * wpally
                    AllyI(act%, tar%) = allyterm
                    AllyI(tar%, act%) = allyterm

                    '           First Year: Data-based and Predicted Initial Threat
                    '           ---------------------------------------------------

                    ThreatIData(act%, tar%) = THREAT(act%, tar%)
                    'Threat from initializtion data
                    ThreatIPred(act%, tar%) = gpowertermI(act%, tar%) + ContiguityTerm _
                + terdisputeterm
                    If ThreatIPred(act%, tar%) > 0 Then  'RelevantDyad
                        ThreatIPred(act%, tar%) = ThreatIPred(act%, tar%) +
                    PowerTranTermI(act%, tar%) + AllyI(tar%, act%) +
                    DemocTermMin + democtermdist
                    End If


                    If tar% = act% Then ThreatIPred(act%, tar%) = 0
                    If wpthrconv = 1 Then
                        THREAT(act%, tar%) = ThreatIPred(act%, tar%)
                    End If
                    sumthreatI(tar%) = sumthreatI(tar%) + THREAT(act%, tar%) * reac(act%, tar%)
                    'If tar% = 180 And act% = 34 Then Stop

                    '           First Year:  Probability of Conventional War
                    '           --------------------------------------------

                    ThreatTerm = AMAX(THREAT(act%, tar%), THREAT(tar%, act%)) * wpthrwar
                    CWARPB(act%, tar%) = cwarbase(act%, tar%) + ThreatTerm
                    CWARPB(act%, tar%) = Amin(100, CWARPB(act%, tar%))

                Next act%
                sumthreatI(tar%) = AMAX(0.01, sumthreatI(tar%))
            Next tar%

            For act% = 1 To NR%
                THREATSENT(act%) = 0
                For tar% = 1 To NR%
                    THREATSENT(act%) = THREATSENT(act%) + THREAT(act%, tar%) / 100 * (100 - THREATSENT(act%))
                    'If act% = 34 Then Stop
                Next tar%
            Next act%

            For tar% = 1 To NR%
                THREATREC(tar%) = 0
                For act% = 1 To NR%
                    THREATREC(tar%) = THREATREC(tar%) + THREAT(act%, tar%) / 100 * (100 - THREATREC(tar%))
                Next act%
            Next tar%

            'clp 2024/07/10 ***START*** Adding dyadic threat
            If wpindsw = 5 Then
                For act% = 1 To NR%
                    THREATSENT(act%) = 0
                    For tar% = 1 To NR%
                        THREATSENT(act%) = THREATSENT(act%) + THREAT(act%, tar%)
                        'If act% = 34 Then Stop
                        ' If tar% = 146 Then Stop
                    Next tar%
                Next act%

                For tar% = 1 To NR%
                    THREATREC(tar%) = 0
                    For act% = 1 To NR%
                        THREATREC(tar%) = THREATREC(tar%) + THREAT(act%, tar%)
                    Next act%
                Next tar%
            End If
            'clp 2024/07/10 ***END*** Adding dyadic threat

            SConcenTerm = 0
            STradeTerm = 0
            SWGDP = WGDP

            'First Year: Private Expenditures
            '----------------------------------
            For R = 1 To NR
                'jrs 2014/05/07 Call AnalFunc("Private Health Expenditures % of GDP (2012)", Array(GDPPCP(R), EDYRSAG25(R, 3)), PrivHLExp)
                'jrs 2014/05/30 Call XYTABL(PrivHLExpPctGDPFxName, GDPPCP(R), PrivHLExp)
                'jrs 2014/05/30 HLEXPENDPRIVShift(R) = HLEXPENDPRIV(R) / GDP(R) * 100 - PrivHLExp

                'Total Health Expenditures
                '-------------------------
                HLEXPEND(R) = GDS(R, Health) + HLEXPENDPRIV(R)
            Next R

            'kbn 2018/02/16-Start Drug model computation
            'First Year: Drug model
            '---------------------------------
            For R% = 1 To NR%
                'kbn 2018/01/11-Start
                'First year: Drug model (Calculate shift factors)
                'dkb 2018/05/09 - Repurposing the shift factors for save values of the estimates (to be used later)
                'dkb: Renamed shift factors as Estimates
                '    Call AnalFunc("Amphetmanine Drug prevalence versus youth bulge and GEM",
                'Array(YTHBULGE(R%), GEM(R%)), Zamph, Array("Youthbulge", "GEM")) 'Logistic model (Amphetamines)

                Call AnalFunc("Amphetmanine Drug prevalence versus youth bulge and GEM",
                New Single() {YTHBULGE(R%), GEM(R%)}, Zamph,
                New String() {"Youthbulge", "GEM"}, context)

                'DrugAmphetamineshift(R%) = DRUGUSEAMPHETAMINE(R%) - AMAX(0.0001, (Amin(100 * (Exp(Zamph) / (1 + Exp(Zamph))), 4)))
                DrugAmphetamineEst(R%) = CSng(AMAX(0.002, (Amin(100 * (Exp(Zamph) / (1 + Exp(Zamph))), 3))))

                '    Call AnalFunc("Cocaine Drug prevalence versus consumption per person, GEM and GINI",
                'Array((C(R%) / POP(R%)), GEM(R%), GINIDOM(R%)), zcoke, Array("C/POP", "GEM", "GINIDOM")) 'Logistic model (Cocaine)

                Call AnalFunc("Cocaine Drug prevalence versus consumption per person, GEM and GINI",
                New Single() {(C(R%) / POP(R%)), GEM(R%), GINIDOM(R%)}, zCoke,
                New String() {"C/POP", "GEM", "GINIDOM"}, context)


                'DrugCocaineshift(R%) = DRUGUSECOCAINE(R%) - AMAX(0.00001, (Amin(100 * (Exp(zcoke) / (1 + Exp(zcoke))), 2.5)))
                DrugCocaineEst(R%) = CSng(AMAX(0.002, (Amin(100 * (Exp(zCoke) / (1 + Exp(zCoke))), 2.5))))

                '    Call AnalFunc("Opiate Drug prevalence",
                'Array((100 * SINCOMELT310LN(R%) / POP(R%)), (100 * POPGT65(R%) / POP(R%)), (100 * SPOPURBAN(R%) / POP(R%))), Zopiate, Array("Poverty", "POP over the age of 65", "Urbanpopulation")) 'dkb: Swaped in POPGT65% for YouthBulge
                '    'Array((100 * SINCOMELT310LN(R%) / POP(R%)), YTHBULGE(R%), (100 * SPOPURBAN(R%) / POP(R%))), Zopiate, Array("Poverty", "Youthbulge", "Urbanpopulation")) 'Logistic model (opiates)

                'jrs 2021/09/02 changed from 3.10 to 3.20/day
                'Call AnalFunc("Opiate Drug prevalence",
                'New Single() {(100 * SINCOMELT310LN(R%) / POP(R%)), (100 * POPGT65(R%) / POP(R%)), (100 * SPOPURBAN(R%) / POP(R%))}, Zopiate,
                'New String() {"Poverty", "POP over the age of 65", "Urbanpopulation"}, context)
                'jrs 2022/11/21
                If ConstantDollarsYear = 2011 Then
                    Call AnalFunc("Opiate Drug prevalence",
                    New Single() {(100 * SINCOMELT320LN(R%) / POP(R%)), (100 * POPGT65(R%) / POP(R%)), (100 * SPOPURBAN(R%) / POP(R%))}, Zopiate,
                    New String() {"Poverty", "POP over the age of 65", "Urbanpopulation"}, context)
                ElseIf ConstantDollarsYear = 2017 Then 'jrs 2025/09/12
                    Call AnalFunc("Opiate Drug prevalence",
                    New Single() {(100 * SINCOMELT365LN(R%) / POP(R%)), (100 * POPGT65(R%) / POP(R%)), (100 * SPOPURBAN(R%) / POP(R%))}, Zopiate,
                    New String() {"Poverty", "POP over the age of 65", "Urbanpopulation"}, context)
                Else 'jrs 2025/09/12
                    Call AnalFunc("Opiate Drug prevalence",
                    New Single() {(100 * SINCOMELT420LN(R%) / POP(R%)), (100 * POPGT65(R%) / POP(R%)), (100 * SPOPURBAN(R%) / POP(R%))}, Zopiate,
                    New String() {"Poverty", "POP over the age of 65", "Urbanpopulation"}, context)
                End If


                'DrugOpiateShift(R%) = DRUGUSEOPIATES(R%) - AMAX(0.0001, (Amin(100 * (Exp(Zopiate) / (1 + Exp(Zopiate))), 4)))
                DrugOpiateEst(R%) = CSng(AMAX(0.002, (Amin(100 * (Exp(Zopiate) / (1 + Exp(Zopiate))), 4))))

                '    Call AnalFunc("Prescription Drug Use prevalence versus health expenditure",
                'Array((100 * (GDS(R%, Health) + HLEXPENDPRIV(R%)) / GDP(R%))), Zpresopioid, Array("Healthexpenditure")) 'Logistic model (Opioids)

                Call AnalFunc("Prescription Drug Use prevalence versus health expenditure",
                New Single() {(100 * (GDS(R%, Health) + HLEXPENDPRIV(R%)) / GDP(R%))}, Zpresopioid,
                New String() {"Healthexpenditure"}, context)

                'DrugPrescriptionOpioidShift(R%) = DRUGUSEPRESCRIPTOPIOID(R%) - AMAX(0.0001, (Amin(100 * (Exp(Zpresopioid) / (1 + Exp(Zpresopioid))), 5.5)))
                DrugPrescriptionOpioidEst(R%) = CSng(AMAX(0.002, (Amin(100 * (Exp(Zpresopioid) / (1 + Exp(Zpresopioid))), 5.5))))

                DrugEstTot(R%) = CSng(100 * (Exp(-3.498 + (0.508 * Log(sc(R%) / POP(R%))) + (1.245 * Log(YTHBULGE(R%))))) / (1 + Exp(-3.498 + (0.508 * Log(C(R%) / POP(R%))) + (1.245 * Log(YTHBULGE(R%))))))

                'kbn 2018/01/23-Start- First year values for Drug model
                SDRUGUSEAMPHETAMINE(R%) = DRUGUSEAMPHETAMINE(R%)
                SDRUGUSECOCAINE(R%) = DRUGUSECOCAINE(R%)
                SDRUGUSEOPIATES(R%) = DRUGUSEOPIATES(R%)
                SDRUGUSEPRESCRIPTOPIOID(R%) = DRUGUSEPRESCRIPTOPIOID(R%)

                iDrugUseAmphetamine(R%) = DRUGUSEAMPHETAMINE(R%)
                iDrugUseCocaine(R%) = DRUGUSECOCAINE(R%)
                iDrugUseOpiates(R%) = DRUGUSEOPIATES(R%)
                iDrugUsePrescriptOpioid(R%) = DRUGUSEPRESCRIPTOPIOID(R%)

                iDrugUseTot(R%) = CSng((iDrugUseAmphetamine(R%) + iDrugUseCocaine(R%) + iDrugUseOpiates(R%) + iDrugUsePrescriptOpioid(R%)) / 1.12)
                'kbn 2018/01/23- End-
                'dkb 2018/05/09-Start - Saving estimates for later
                sDrugAmphetamineEst(R%) = CSng(AMAX(DrugAmphetamineEst(R%), 0.002))
                sDrugCocaineEst(R%) = CSng(AMAX(DrugCocaineEst(R%), 0.002))
                sDrugOpiateEst(R%) = CSng(AMAX(DrugOpiateEst(R%), 0.002))
                sDrugPrescriptionOpioidEst(R%) = CSng(AMAX(DrugPrescriptionOpioidEst(R%), 0.002))
                sDrugEstTot(R%) = CSng(AMAX(Amin(DrugEstTot(R%), 5), 0.02))

                iDrugAmphetamineEst(R%) = CSng(AMAX(DrugAmphetamineEst(R%), 0.002))
                iDrugCocaineEst(R%) = CSng(AMAX(DrugCocaineEst(R%), 0.002))
                iDrugOpiateEst(R%) = CSng(AMAX(DrugOpiateEst(R%), 0.002))
                iDrugPrescriptionOpioidEst(R%) = CSng(AMAX(DrugPrescriptionOpioidEst(R%), 0.002))
                iDrugEstTot(R%) = CSng(AMAX(Amin(DrugEstTot(R%), 5), 0.02))


                If C(R%) / POP(R%) >= 30 Then
                    hhconpc(R%) = 30

                Else
                    hhconpc(R%) = (C(R%) / POP(R%))
                End If

                shhconpc(R%) = hhconpc(R%)

                'dkb 2018/05/09-End
            Next R%

            'jrs 2016/08/02
            'First Year: Human Traffic
            '----------------------------------
            Dim Est As Single
            For R = 1 To NR
                'Human Traffic Prevalence
                'Call AnalFunc("Human Traffic Prevalence (2015)", Array(POP(R), GOVEFFECT(R)), Est, Array("POP", "GOVEFFECT"))

                Call AnalFunc("Human Traffic Prevalence (2015)",
                New Single() {POP(R), GOVEFFECT(R)}, Est,
                New String() {"POP", "GOVEFFECT"}, context)


                If HTCTPI(R) <> -1 Then
                    HTCTPIShift(R) = HTCTPI(R) - Est
                Else
                    HTCTPI(R) = Est
                    HTCTPIShift(R) = 0
                End If
                'Government Response to Trafficking
                'Call AnalFunc("Government response to trafficking (2015)", Array(POP(R), DEMOCPOLITY(R), EDSECENRG(R, 2)), Est, Array("POP", "POLITYCOMBINED", "EDSECENROLLGROSSFEMALE"))

                Call AnalFunc("Government response to trafficking (2015)",
                New Single() {POP(R), DEMOCPOLITY(R), EDSECENRG(R, 2)}, Est,
                New String() {"POP", "POLITYCOMBINED", "EDSECENROLLGROSSFEMALE"}, context)

                If HTCGRI(R) <> -1 Then
                    HTCGRIShift(R) = HTCGRI(R) - Est
                Else
                    HTCGRI(R) = Est
                    HTCGRIShift(R) = 0
                End If
            Next R

            ' GoTo AllSteps
            Call IfsIntlPolAllSteps(context)

        End Sub

        Dim ConcenChange As Single
        Dim concenterm As Single
        Dim TradeTerm As Single
        Dim comterm As Single

        Dim WGDPR As Single
        Dim LongTermGDPR As Single
        Dim wgdpgrowthterm As Single
        Dim MinDemocI As Single
        Dim DemocDistI As Single
        Dim powertranterm As Single
        Dim DeltaPowerTerms As Single
        Dim THREATPred As Single
        Dim sumthreat As Single
        Dim DeltaNonPowerTerms As Single
        Dim GKShift As Single
        Dim GDSTrgtPcnt As Single
        Dim RandDMul As Single
        Dim RandDComp As Single
        Dim RandDComp2 As Single
        Dim RandDGDP As Single
        Dim cInfraCostGDSInfraRI As Single
        Dim InfraTotDemand As Single
        Dim DemandCalcInfra As Single
        Dim DemandCalcInfraOther As Single
        Dim cEdCostGDSEdRI As Single
        Dim EcGrTerm As Single
        Dim DemandCalc As Single
        Dim diff As Single
        Dim NoSurplus As Boolean
        Dim GTOT As Single
        Dim Tot_SetAside As Single
        Dim GOVCONRed As Single
        Dim Est As Single
        Dim Cond2 As Boolean
        Dim Rnd As Single


        Public Sub IFsIntlPol1AllButFirstYear(context As IForecastContext)
            'If IFUN = 2 Then GoTo IPCall2

            '
            '   All but First 1:  Power Index - Based on Last Year's Conventional and Nuclear Power
            '   ---------------------------------------------------------------------------------





            WGOV = 0
            WGDSMIL = 0
            'clp 2016/08/26 - initalizing WGDSRandD
            WGDSRandD = 0
            WGDPPPP = 0
            WTECHP = 0
            WTECH = 0
            'jrs 2020/11/19
            WTECHmix = 0

            'jrs 2011/10/11
            WGOVNUCWP = 0
            WENX = 0
            WENM = 0

            'dkb 2014/10/23 - new EnergyNetExport specification - START
            WENXM = 0
            'clp 2016/08/25 - new world energy production variable
            WENProd = 0
            'dkb 2014/10/23 - new EnergyNetExport specification - END

            WTradeTotal = 0
            WXFDIFIN = 0

            WAID = 0
            WHumCapWkPopHDI = 0
            WGOVREV = 0
            WInnovationRD = 0
            WConnectivityICTProd = 0

            'dkb 2013/09/09 creating components for world DPI - START
            WDPIBN = 0
            WDPIMN = 0
            WDPIILN = 0
            WDPI = 0
            'dkb 2013/09/09 - END

            'dkb 2014/07/07 creating components for world Effective Human Capital - START
            WEHC = 0
            'dkb 2014/07/07 - END

            'dkb 2014/08/01 - moved up from below, because DIPLOEMBASSYMAX, DIPLOIGOMAX, DIPLOTREATYMAX are _
            'used in WDPI - START
            'clp 2016/08/23 - Updated equations per Mickey
            'EmbassyMaxEstimate = 0.012 * (IY - 1 + FirstYear) - 20.663
            'IGOMaxEstimate = 0.1019 * (IY - 1 + FirstYear) - 398.46
            'TreatyMaxEstimate = 0.0505 * (IY - 1 + FirstYear) + 3.1859
            'mtr 2017/01/05 - updating logic for temporal inclusion of IVs in IGOMax and EmbassyMax
            'EmbassyMaxEstimate = 1.546716 * (IY - 1 + FirstYear) - 2907.35951
            'IGOMaxEstimate = 11.98167 * (IY - 1 + FirstYear) - 23036.023
            EmbassyMaxEstimate = CSng(0.3358688 * (IY - 1 + FirstYear) - 481.8743414)
            IGOMaxEstimate = CSng(7.380403 * (IY - 1 + FirstYear) - 13800.158803)
            'mtr 2017/01/05 *** end ***
            TreatyMaxEstimate = CSng(48.67768 * (IY - 1 + FirstYear) - 94913.4686)

            DIPLOEMBASSYMAX = EmbassyMaxEstimate + DIPLOEMBASSYMAXShift
            DIPLOIGOMAX = IGOMaxEstimate + DIPLOIGOMAXShift
            DIPLOTREATYMAX = TreatyMaxEstimate + DIPLOTREATYMAXShift
            'Apply multipliers
            DIPLOEMBASSYMAX = DIPLOEMBASSYMAX * diploembassymaxm
            DIPLOIGOMAX = DIPLOIGOMAX * diploigomaxm
            DIPLOTREATYMAX = DIPLOTREATYMAX * diplotreatymaxm
            'dkb 2014/08/01 - END

            'bbh 2011/11/19 Changes throughout R% loop
            For R% = 1 To NR%
                WGOV = WGOV + GOVCON(R%)
                'WGDSMIL = WGDSMIL + SGDSMil(R%) * GDPP(R%) / GDP(R%)
                WGDSMIL = WGDSMIL + SGDSMil(R%)
                'clp 2016/08/26 - creating new sum of world public R&D exp. variable for GPI
                WGDSRandD = WGDSRandD + SGDSRandD(R%)
                'If RegName(R%) = "Belgium" And IY% = 2 Then Stop

                WGDPPPP = WGDPPPP + GDPP(R%)
                WTECHP = WTECHP + GDPP(R%) * GDPPCP(R%)
                WTECH = WTECH + GDP(R%) * GDPPC(R%)
                'jrs 2020/11/19
                WTECHmix = WTECHmix + GDP(R%) * GDPPCP(R%)

                'dkb 2014/07/07 unstandardizing components for world DPI - START
                'dkb 2013/09/09 creating components for world DPI - START
                'WDPIBN = WDPIBN + DIPLOEMBASSY(R%)
                'WDPIMN = WDPIMN + DIPLOIGO(R%)
                'WDPIILN = WDPIILN + DIPLOTREATY(R%)
                'WDPI = WDPI + (0.5 * (DIPLOEMBASSY(R%) / DIPLOEMBASSYMAX)) + (0.33 * (DIPLOIGO(R%) / DIPLOIGOMAX)) + _
                '(0.17 * (DIPLOTREATY(R%) / DIPLOTREATYMAX))
                'dkb 2013/09/09 - END
                'clp 2016/08/23 - removed unstandardizing
                'WDPIBN = WDPIBN + ((SDIPLOEMBASSY(R%) * 32.48639) + 39.42)
                'WDPIMN = WDPIMN + ((SDIPLOIGO(R%) * 129.6466) + 244.7328)
                'WDPIILN = WDPIILN + ((SDIPLOTREATY(R%) * 747.6514) + 906.7255)
                'WDPI = WDPI + (((0.5 * (((SDIPLOEMBASSY(R%) * 32.48639) + 39.42253) / ((DIPLOEMBASSYMAX * 32.48639) + 39.42253))) + _
                '(0.33 * ((SDIPLOIGO(R%) * 129.6466) + 244.7328) / ((DIPLOIGOMAX * 129.6466) + 244.7328)) + _
                '(0.17 * ((SDIPLOTREATY(R%) * 747.6514) + 906.7255) / ((DIPLOTREATYMAX * 747.6514) + 906.7255))) * GOVREV(R%))

                WDPIBN = WDPIBN + SDIPLOEMBASSY(R%)
                WDPIMN = WDPIMN + SDIPLOIGO(R%)
                WDPIILN = WDPIILN + SDIPLOTREATY(R%)

                'clp 2016/08/24 - Changing to match Tim's new formulation
                'WDPI = WDPI + ((0.5 * (DIPLOEMBASSY(R%) / DIPLOEMBASSYMAX) + _
                ' (0.33 * DIPLOIGO(R%) / DIPLOIGOMAX) + _
                '(0.17 * DIPLOTREATY(R%) / DIPLOTREATYMAX)) * GOVREV(R%))
                WDPI = CSng(WDPI + ((((99.83 * (SDIPLOEMBASSY(R%) / DIPLOEMBASSYMAX) ^ 2)) +
                (35.49 * (SDIPLOIGO(R%) / DIPLOIGOMAX) ^ 2) +
                (7.34 * ((SDIPLOTREATY(R%) / DIPLOTREATYMAX) ^ 1.5))) * GOVREV(R%)))
                'dkb 2014/07/07 - END
                'If RegName(R%) = "China" Then Stop
                'dkb 2014/07/07 - creating components for world Effective Human Capital - START
                'clp 2016/08/23 - updated equation to match new GPI equations provided by Tim/Alanna
                ' WEHC = WEHC + ((POP15TO65(R%) - (SINCOMELT1LN(R%) * (POP15TO65(R%) / POP(R%))) - _
                ' (((SINCOMELT2LN(R%) - SINCOMELT1LN(R%)) / 2) * (POP15TO65(R%) / POP(R%)))) * SHDINEW(R%))
                'WEHC = WEHC + ((POP15TO65(R%) - (SINCOMELT190LN(R%) * (POP15TO65(R%) / POP(R%))) - _
                '(((SINCOMELT310LN(R%) - SINCOMELT190LN(R%)) / 2) * (POP15TO65(R%) / POP(R%)))) * SHDINEW(R%))
                'jrs 2021/09/02 changed from 3.10 to 3.20/day
                'WEHC = WEHC + (((POP(R%) - SINCOMELT310LN(R%)) * (POP15TO65(R%) / POP(R%))) * SHDINEW(R%) * SHDINEW(R%))
                'jrs 2022/11/21
                If ConstantDollarsYear = 2011 Then
                    WEHC = WEHC + (((POP(R%) - SINCOMELT320LN(R%)) * (POP15TO65(R%) / POP(R%))) * SHDINEW(R%) * SHDINEW(R%))
                ElseIf ConstantDollarsYear = 2017 Then 'jrs 2025/09/12
                    WEHC = WEHC + (((POP(R%) - SINCOMELT365LN(R%)) * (POP15TO65(R%) / POP(R%))) * SHDINEW(R%) * SHDINEW(R%))
                Else
                    WEHC = WEHC + (((POP(R%) - SINCOMELT420LN(R%)) * (POP15TO65(R%) / POP(R%))) * SHDINEW(R%) * SHDINEW(R%))
                End If
                'dkb 2014/07/03 - END


                'jrs 2011/10/11
                'WGOVNUCWP = WGOVNUCWP + SGOVNUCWP(R%)
                If SGOVNUCWP(R%) > 0 Then
                    WGOVNUCWP = CSng(WGOVNUCWP + Log(SGOVNUCWP(R%)))
                End If
                'WConvMilSpend = WConvMilSpend
                'dkb 2014/08/20 - creating components for world energy trade, All but first - START
                'WENX = WENX + ENX(R%)
                'WENM = WENM + ENM(R%)
                WENX = WENX + SENX(R%)
                WENM = WENM + SENM(R%)
                'dkb 2014/10/23 - new EnergyNetExport specification - START
                WENXM = WENXM + AMAX(0, SENX(R%) - SENM(R%))
                'dkb 2014/10/23 - new EnergyNetExport specification - END
                'dkb 2014/08/20 - END

                'clp 2016/08/25 - calculating world energy production
                WENProd = WENProd + SEnTotal(R%)

                WTradeTotal = WTradeTotal + (X(R%) + M(R%))
                WXFDIFIN = WXFDIFIN + XFDIFIN(R%)

                If AID(R%) > 0 Then
                    WAID = WAID + AID(R%)
                End If
                'jrs 2013/04/12 WHumCapWkPopHDI = WHumCapWkPopHDI + POP15TO65(R%) * SHDINEW(R%)
                WHumCapWkPopHDI = WHumCapWkPopHDI + POPWORKING(R%) * SHDINEW(R%)
                WGOVREV = WGOVREV + GOVREV(R%)
                'WInnovationRD = WInnovationRD + SRANDDEXP(R%) * GDPP(R%)
                WInnovationRD = WInnovationRD + SRANDDEXP(R%) * GDP(R%)
                'WConnectivityICTProd = WConnectivityICTProd + ZS(R%, 6)
                WConnectivityICTProd = WConnectivityICTProd + KS(R%, 6)
            Next R%


            Call PowerCalculation(context)

            'jrs 2021/12/05 added switch
            If wppowhhsw = 0 Then
                Call PowerConcentration()
            Else
                Call PowerHHConcentration()
            End If

            'jrs 2011/02/14 *** begin Embeddedness ***
            Call Embeddedness(context)
            'jrs 2011/02/14 *** end Embeddedness ***


            '   All but First 1:  Threat Calculation, Global community term
            '   ---------------------------------------------------------

            comterm = CSng((glocom / GLOCOMI) ^ wpcommun - 1)

            '   All but First 1:  Threat Calculation, Power concentration term
            '   ------------------------------------------------------------

            ConcenChange = AMAX(wpconmin, Amin(wpconmax, PCONGREAT)) -
        AMAX(wpconmin, Amin(wpconmax, PConGreatI))
            'Mansfield says to bound concentration between .202 and .417
            concenterm = CSng(wpcon * ConcenChange + wpconsq * (ConcenChange ^ 2))
            concenterm = CSng(0.8 * SConcenTerm + 0.2 * concenterm)
            concenterm = CSng(AMAX(-0.075, Amin(0.075, concenterm)))
            SConcenTerm = concenterm

            '   All but First 1:  Threat Calculation, Trade term
            '   ----------------------------------------------

            TradeTerm = wptrade * (WTRADE - wtradei) '/ 100
            TradeTerm = CSng(0.8 * STradeTerm + 0.2 * TradeTerm)
            TradeTerm = AMAX(-2, Amin(2, TradeTerm))
            STradeTerm = TradeTerm

            '   All but First 1:  Systemic Economic Growth Term
            '   ---------------------------------------------

            WGDPR = CSng((WGDP - SWGDP) / AMAX(0.001, SWGDP) * 100)
            SWGDP = WGDP
            If IY% = 2 Then LongTermGDPR = WGDPR
            wgdpgrowthterm = (WGDPR - LongTermGDPR) * wpsysgr / AMAX(10, NR%) 'systemic effect; spread across system size
            LongTermGDPR = CSng(LongTermGDPR * 0.8 + WGDPR * 0.2)


            '   All but First 1:  Threat Calculation, Dyadic terms
            '   ------------------------------------------------

            For tar% = 1 To NR%  'Reacting region
                For act% = 1 To NR%  'Actors

                    '           All but First 1:  Dyadic great power term:  none, one or both
                    '           -----------------------------------------------------------

                    wpgreatthresh = Amin(wpgreatthresh, 25)
                    wpgreatlev = AMAX(wpgreatlev, wpgreatthresh + 1)
                    'jrs 2021/12/05 added switch
                    If wppowhhsw = 0 Then
                        If POWER(tar%) <= wpgreatthresh Then
                            GPowerNumb = 0
                        ElseIf POWER(tar%) >= wpgreatlev Then
                            GPowerNumb = 1
                        Else
                            GPowerNumb = (POWER(tar%) - wpgreatthresh) _
                        / AMAX(1, (wpgreatlev - wpgreatthresh))
                        End If

                        If POWER(act%) <= wpgreatthresh Then
                            GPowerNumb = GPowerNumb
                        ElseIf POWER(act%) >= wpgreatlev Then
                            GPowerNumb = GPowerNumb + 1
                        Else
                            GPowerNumb = GPowerNumb + (POWER(act%) - wpgreatthresh) _
                        / (wpgreatlev - wpgreatthresh)
                        End If
                    Else
                        If POWERHH(tar%) <= wpgreatthresh Then
                            GPowerNumb = 0
                        ElseIf POWERHH(tar%) >= wpgreatlev Then
                            GPowerNumb = 1
                        Else
                            GPowerNumb = (POWERHH(tar%) - wpgreatthresh) _
                        / AMAX(1, (wpgreatlev - wpgreatthresh))
                        End If

                        If POWERHH(act%) <= wpgreatthresh Then
                            GPowerNumb = GPowerNumb
                        ElseIf POWERHH(act%) >= wpgreatlev Then
                            GPowerNumb = GPowerNumb + 1
                        Else
                            GPowerNumb = GPowerNumb + (POWERHH(act%) - wpgreatthresh) _
                        / (wpgreatlev - wpgreatthresh)
                        End If
                    End If
                    If GPowerNumb <= 1 Then
                        gpowerterm = GPowerNumb * wpgreat1
                    Else
                        gpowerterm = wpgreat1 + (GPowerNumb - 1) * wpgreat2
                        'if 2 great powers, full effect
                    End If
                    gpowerterm = gpowerterm - gpowertermI(act%, tar%)

                    '           All but First 1:  Territorial Dispute Term
                    '           ----------------------------------------

                    terdisputeterm = terdispute(act%, tar%) * wpterdisp -
                TerDisputeI(act%, tar%)

                    '           All but First 1:  Dyadic democracy terms,  joint and political distance
                    '           --------------------------------------------------------------------

                    MinDemoc = Amin(DEMOCPOLITY(act%), DEMOCPOLITY(tar%))
                    MinDemocI = Amin(DEMOCPOLITYI(act%), DEMOCPOLITYI(tar%))
                    DemocTermMin = (MinDemoc - MinDemocI) * wpdemmin

                    DemocDist = Abs(DEMOCPOLITY(act%) - DEMOCPOLITY(tar%))
                    DemocDistI = Abs(DEMOCPOLITYI(act%) - DEMOCPOLITYI(tar%))
                    democtermdist = (DemocDist - DemocDistI) * wpdemdist

                    '           All but First 1:  Dyadic power transition term; only for great powers
                    '           -------------------------------------------------------------------

                    'jrs 2021/12/05 added switch
                    If wppowhhsw = 0 Then
                        If POWER(act%) >= wpgreatlev And POWER(tar%) >= wpgreatlev Then
                            PowerRatio = Amin(POWER(act%) / POWER(tar%), POWER(tar%) / POWER(act%))
                            If (PowerRatio >= wppowtran1) Then
                                PowerTerm = CSng(wppowtran2 * (PowerRatio - wppowtran1) / 0.1)
                                'term works on 10% intervals
                            Else
                                PowerTerm = 0
                            End If
                        Else
                            PowerTerm = 0
                        End If
                    Else
                        If POWERHH(act%) >= wpgreatlev And POWERHH(tar%) >= wpgreatlev Then
                            PowerRatio = Amin(POWERHH(act%) / POWERHH(tar%), POWERHH(tar%) / POWERHH(act%))
                            If (PowerRatio >= wppowtran1) Then
                                PowerTerm = CSng(wppowtran2 * (PowerRatio - wppowtran1) / 0.1)
                                'term works on 10% intervals
                            Else
                                PowerTerm = 0
                            End If
                        Else
                            PowerTerm = 0
                        End If
                    End If

                    powertranterm = PowerTerm - PowerTranTermI(act%, tar%)


                    '           All but First 1:  Alliance term
                    '           ------------------------------

                    If ally(act%, tar%) <> ally(tar%, act%) Then
                        ally(act%, tar%) = AMAX(ally(act%, tar%), ally(tar%, act%))
                        ally(tar%, act%) = ally(act%, tar%)
                    End If
                    allyterm = ally(act%, tar%) * wpally - AllyI(act%, tar%)

                    '           All but First 1:  Contiguity term - no change, so not used to compute threat
                    '           --------------------------------------------------------------------------

                    If contiguity(act%, tar%) = 1 Then
                        ContiguityTerm = wpcontiguity
                    Else
                        ContiguityTerm = 0
                    End If

                    '           All but First 1:  Threat computation
                    '           ----------------------------------
                    If wpindsw < 5 Then
                        If act% <> tar% Then
                            'If act% = 139 And tar% = 57 Then Stop
                            'Cond1 = RegName$(act%) = "United States of America" Or RegName$(act%) = "Chad"
                            'Cond2 = RegName$(tar%) = "United States of America" Or RegName$(tar%) = "Chad"
                            'If Cond1 And Cond2 Then Stop
                            DeltaPowerTerms = gpowerterm + powertranterm +
                    concenterm + terdisputeterm + comterm
                            DeltaNonPowerTerms = DemocTermMin + democtermdist + TradeTerm +
                    allyterm + wgdpgrowthterm
                            THREAT(act%, tar%) = ThreatIData(act%, tar%) + DeltaPowerTerms _
                    + DeltaNonPowerTerms
                            THREAT(act%, tar%) = AMAX(0, Amin(100, THREAT(act%, tar%)))

                            THREATPred = ThreatIPred(act%, tar%) + DeltaPowerTerms _
                    + DeltaNonPowerTerms
                            THREATPred = AMAX(0, Amin(100, THREATPred))
                            'If IY% > 10 And RegName$(act%) = "United States of America" And RegName$(tar%) = "Albania" Then Stop
                            THREAT(act%, tar%) =
                    ConvergeOverTime(THREAT(act%, tar%), THREATPred, wpthrconv)
                            'Base, Target, Years
                        Else
                            THREAT(act%, tar%) = 0
                        End If
                    End If ' clp 2025/04/02 add condition to not overwrite THREAT values for relevancy checks 
                    'If RegName$(act%) = "Brazil" And RegName$(tar%) = "Kenya" Then Stop
                    'Don't forget community term, democterm, tradeterm, etc.

                Next act%
            Next tar%

            'clp 2024/07/10 ***START*** Adding dyadic threat
            'clp 2024/12/20 ***START*** changing things to work with rivalry version of threat
            'wpindsw = 8
            If wpindsw > 4 Then
                Dim z As Single
                Dim pwrrat As Single
                Dim contig As Single
                Dim tradeshr As Single
                Dim ally As Single
                Dim losact As Single
                Dim totpwr As Single
                Dim i As Integer
                Dim alt As Single
                Dim pyrsdecay As Single
                Dim lwrdemoc As Single

                'If IY > 1 Then
                'For i = 1 To NR%
                'clp 2025/03/18 ***START*** Adding reverse los, GDS-Mil option for power, and assign polrelevance default
                ' If wpindsw <> 7 And wpindsw <> 9 Then totpwr = totpwr + (GDP(i) * GDPPCP(i))
                'If wpindsw = 7 Or wpindsw = 9 Then totpwr = totpwr + (sgds(i, Mil) * GDPPCP(i))
                'clp 2025/03/18 ***END*** Adding reverse los, GDS-Mil option for power, and assign polrelevance default
                ' Next i
                'End If

                For tar% = 1 To NR%
                    For act% = 1 To NR%
                        'CONFLICTYEARS(act%, tar%) = CONFLICTYEARS(act%, tar%) + Amin(AMAX(1 - ((THREAT(act%, tar%) / 100 / 0.00175) * 0.5), 0), 1)
                        SCONFLICTYEARS(act%, tar%) = CONFLICTYEARS(act%, tar%)
                        'CONFLICTYEARS(act%, tar%) = CONFLICTYEARS(act%, tar%) + Amin(AMAX(1 - (THREAT(act%, tar%) / 100 / 0.000619) * CSng(peaceyearcountadj(act%, tar%)), 0), 1)
                        'CONFLICTYEARS(act%, tar%) = CONFLICTYEARS(act%, tar%) + 1

                        If wpindsw = 5 Then
                            If STHREAT(act%, tar%) > 0.175 Then
                                CONFLICTYEARS(act%, tar%) = CONFLICTYEARS(act%, tar%) + 1
                            End If
                        ElseIf wpindsw = 10 Then
                            If STHREAT(act%, tar%) > 0.25 Then
                                CONFLICTYEARS(act%, tar%) = CONFLICTYEARS(act%, tar%) + 1
                            End If
                        Else
                            If STHREAT(act%, tar%) > 20 Then
                                CONFLICTYEARS(act%, tar%) = CONFLICTYEARS(act%, tar%) + 1
                            End If
                        End If

                        If peaceyearset(act%, tar%) <> -1 Then
                            If peaceyearset(act%, tar%) = -2 Then
                                CONFLICTYEARS(act%, tar%) = ICONFLICTYEARS(act%, tar%)
                                SCONFLICTYEARS(act%, tar%) = ICONFLICTYEARS(act%, tar%)
                            Else
                                CONFLICTYEARS(act%, tar%) = peaceyearset(act%, tar%)
                                SCONFLICTYEARS(act%, tar%) = peaceyearset(act%, tar%)
                            End If
                        End If
                    Next act%
                Next tar%

                'clp 2024/08/21
                If wpindsw > 4 Then
                    Using db As IFsForDyadicContext = New IFsForDyadicContext()
                        Dim conn = db.Database.GetDbConnection
                        If conn.State = 0 Then 'closed
                            conn.Open()
                        End If

                        Using cmd2 = conn.CreateCommand()
                            cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                   PRAGMA ignore_check_constraints = ON;
                  PRAGMA journal_mode = MEMORY;
                 PRAGMA synchronous = OFF;"
                            cmd2.ExecuteNonQuery()
                        End Using
                        db.Database.AutoTransactionsEnabled = False 'gets suggestions BC40000 obsolete, suggests alternative; ignored to mirror extend code in RedetermineEcDyadicBase in Economy Rebuild 
                        db.ChangeTracker.AutoDetectChangesEnabled = False
                        db.ChangeTracker.LazyLoadingEnabled = False

                        db.BulkInsertCONFLICTYEARS(CONFLICTYEARS, CShort(FirstYear + IY - 1), NR) 'clp 2024/08/30 relocating to not lose last year of run and keep standard process
                        'removed conditional 'If wpindsw = 6 Or wpindsw = 8 Or wpindsw = 7 Or wpindsw = 9 Then
                        db.BulkInsertRIVALRY(RIVALRY, CShort(FirstYear + IY - 1), NR) 'clp 2024/12/20
                    End Using
                End If

                'If wpindsw = 6 Or wpindsw = 8 Or wpindsw = 7 Or wpindsw = 9 Then 'clp 2025/03/18 add the 7 condition
                For tar% = 1 To NR%
                    sumthreatI(tar%) = 0
                    For act% = 1 To NR%
                        'If act% = 114 Then Stop
                        'CONFLICTYEARS(act%, tar%) = CONFLICTYEARS(act%, tar%) + 1
                        'If act% = 174 And tar% = 13 Then Stop
                        If act% <> tar% Then

                            'If act% = 75 And tar% = 127 Then Stop 'And FirstYear + IY - 1 >= 2046 Then Stop
                            'clp 2025/03/18 ***START*** Adding reverse los, GDS-Mil option for power, and assign polrelevance default
                            If wpindsw = 7 Or wpindsw = 9 Then
                                losact = CSng((sgds(act%, Mil) * GDPPC(act%)) ^ (1 / Log((DistanceBilat(act%, tar%) / 804.672) + 10 - 2.71828))) '0.621371 is the unit conversion from km to miles; 500 comes from the academic works and relates to miles able to be covered; 10 and e are more for calculation and behavior purposes; See BDM or Lemke for detials. 
                            Else
                                losact = CSng((GDP(act%) * GDPPC(act%)) ^ (1 / Log((DistanceBilat(act%, tar%) / 804.672) + 10 - 2.71828))) '0.621371 is the unit conversion from km to miles; 500 comes from the academic works and relates to miles able to be covered; 10 and e are more for calculation and behavior purposes; See BDM or Lemke for detials. 
                            End If

                            If losact < aprdval Then ' if the dyad fails the check, try the other direction 
                                If wpindsw = 7 Or wpindsw = 9 Then
                                    losact = CSng((sgds(tar%, Mil) * GDPPC(tar%)) ^ (1 / Log((DistanceBilat(act%, tar%) / 804.672) + 10 - 2.71828))) '0.621371 is the unit conversion from km to miles; 500 comes from the academic works and relates to miles able to be covered; 10 and e are more for calculation and behavior purposes; See BDM or Lemke for detials. 
                                Else
                                    losact = CSng((GDP(tar%) * GDPPC(tar%)) ^ (1 / Log((DistanceBilat(act%, tar%) / 804.672) + 10 - 2.71828))) '0.621371 is the unit conversion from km to miles; 500 comes from the academic works and relates to miles able to be covered; 10 and e are more for calculation and behavior purposes; See BDM or Lemke for detials. 
                                End If
                            End If

                            'clp 2025/03/18 ***START*** Adding reverse los, GDS-Mil option for power, and assign polrelevance default

                            If polrelevance(act%, tar%) <> 0 Then
                                losact = polrelevance(act%, tar%) + losact
                            End If

                            'If RIVALRY(act%, tar%) = -99 And losact > aprdval And peaceyearset(act%, tar%) <> -2 Then
                            'CONFLICTYEARS(act%, tar%) = 0
                            'SCONFLICTYEARS(act%, tar%) = 0
                            'End If

                            pyrsdecay = CSng(Exp(-0.0693147 * SCONFLICTYEARS(act%, tar%)))

                            'If act% = 131 And tar% = 113 Then Stop
                            If contiguity(act%, tar%) = 1 Then contig = 1 Else contig = 0
                            'If act% = 75 And tar% = 126 And (2038 < IY + 2019) Then Stop
                            'If act% = 75 Then Stop
                            'If RIVALRY(act%, tar%) <> CSng(-99) And (losact > aprdval Or contig = 1) Then 'checking if LOS-PRD ' added third condition to fix jumps in second year
                            '           First Year: Dyadic Data-based and Predicted Initial Threat
                            '           ---------------------------------------------------
                            'pwrrat = AMAX((GDP(act%) * GDPPCP(act%)), (GDP(tar%) * GDPPCP(tar%))) / ((GDP(act%) * GDPPCP(act%)) + (GDP(tar%) * GDPPCP(tar%)))
                            pwrrat = Amin((GDP(act%) * GDPPC(act%)), (GDP(tar%) * GDPPC(tar%))) / ((GDP(act%) * GDPPC(act%)) + (GDP(tar%) * GDPPC(tar%)))
                            'tradeshr = (((XDYAD(act%, tar%, 1) + XDYAD(tar%, act%, 1)) / 1000000000) + ((XDYAD(act%, tar%, 2) + XDYAD(tar%, act%, 2)) / 1000000000) + ((XDYAD(act%, tar%, 3) + XDYAD(tar%, act%, 3)) / 1000000000) + ((XDYAD(act%, tar%, 4) + XDYAD(tar%, act%, 4)) / 1000000000) + ((XDYAD(act%, tar%, 6) + XDYAD(tar%, act%, 6)) / 1000000000)) / GDP(act%) * 100

                            'clp 2025/04/29 ***START*** adding ability to keep values constant for THREAT components
                            ' All But First Year
                            'If act% = 62 And tar% = 126 Then Stop

                            If threatconstsw = 2 Or threatconstsw = 0 Or threatconstsw = 4 Or threatconstsw = 6 Or threatconstsw > 7 Then
                                lwrdemoc = Amin(DEMOCPOLYARCHY(act), DEMOCPOLYARCHY(tar%))
                            Else
                                lwrdemoc = BlThreatInital_DemMin(act%, tar%)
                            End If

                            tradeshr = 0
                            alt = 0

                            ' START BRANCHES HERE
                            If wpindsw = 6 Or wpindsw = 7 Or wpindsw = 8 Or wpindsw = 9 Or wpindsw = 10 Then
                                'All undirected; 6 = Rivalry, GDPmer*GDPPCppp, no entrances at 66; 7 Rivalry, GDS*GDPPCppp, no entrances at 66;
                                '8 Rivalry, GDPmerXGDPPCppp, entrances at 66; 9 Rivalry, GDS*GDPPCppp, no entrances at 66
                                '10 Undirected UCDP-PRIO
                                If threattradesw = 0 Then 'calculate using current year values 
                                    For S% = 1 To NSec '6
                                        tradeshr = tradeshr + (XDYAD(act%, tar%, S%) + XDYAD(tar%, act%, S%)) / 1000000000
                                    Next
                                    tradeshr = AMAX(tradeshr / GDP(act%) * 100, tradeshr / GDP(tar%) * 100)

                                ElseIf threattradesw = 1 Then ' use inital year values for all but energy
                                    alt = (IDyTrdShrGdp(act%, tar%) / 100 * GDP(tar%) + (XDYAD(act%, tar%, 2) + XDYAD(tar%, act%, 2)) / 1000000000) / GDP(tar%) * 100
                                    tradeshr = AMAX((IDyTrdShrGdp(act%, tar%) / 100 * GDP(act%) + (XDYAD(act%, tar%, 2) + XDYAD(tar%, act%, 2)) / 1000000000) / GDP(act%) * 100, alt)

                                Else 'calculate with all held constant
                                    tradeshr = IDyTrdShrGdp(act%, tar%)
                                End If
                                'clp 2025/04/29 ***END*** adding ability to keep values constant for THREAT components

                                If ALLIANCE(act%, tar%) > 75 Then ally = 1 Else ally = 0



                                If wpindsw <> 10 Then
                                    If threatconstsw = 0 Then ' All Active
                                        Call AnalFunc("Dyadic THREAT (Rivalry)",
                                            New Single() {pwrrat, Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay, lwrdemoc}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years", "Lower Democracy"}, context)
                                    End If
                                    If threatconstsw = 1 Then ' Just Trade
                                        Call AnalFunc("Dyadic THREAT (Rivalry)",
                                            New Single() {BlThreatInital_PwrRatio(act%, tar%), BlThreatInital_AffGap(act%, tar%), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay, lwrdemoc}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years", "Lower Democracy"}, context)
                                    End If
                                    If threatconstsw = 2 Then ' Just Democracy
                                        Call AnalFunc("Dyadic THREAT (Rivalry)",
                                            New Single() {BlThreatInital_PwrRatio(act%, tar%), Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, BlThreatInital_TrdShr(act%, tar%), contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay, lwrdemoc}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years", "Lower Democracy"}, context)
                                    End If
                                    If threatconstsw = 3 Then ' Just Power
                                        Call AnalFunc("Dyadic THREAT (Rivalry)",
                                            New Single() {pwrrat, BlThreatInital_AffGap(act%, tar%), ally, BlThreatInital_TrdShr(act%, tar%), contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay, lwrdemoc}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years", "Lower Democracy"}, context)
                                    End If
                                    If threatconstsw = 4 Then ' No Power
                                        Call AnalFunc("Dyadic THREAT (Rivalry)",
                                            New Single() {BlThreatInital_PwrRatio(act%, tar%), Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay, lwrdemoc}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years", "Lower Democracy"}, context)
                                    End If
                                    If threatconstsw = 5 Then ' No Democracy
                                        Call AnalFunc("Dyadic THREAT (Rivalry)",
                                            New Single() {pwrrat, BlThreatInital_AffGap(act%, tar%), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay, lwrdemoc}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years", "Lower Democracy"}, context)
                                    End If
                                    If threatconstsw = 6 Then ' No Trade
                                        Call AnalFunc("Dyadic THREAT (Rivalry)",
                                            New Single() {pwrrat, Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, BlThreatInital_TrdShr(act%, tar%), contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay, lwrdemoc}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years", "Lower Democracy"}, context)
                                    End If
                                    If threatconstsw = 7 Then ' All Fixed
                                        Call AnalFunc("Dyadic THREAT (Rivalry)",
                                            New Single() {BlThreatInital_PwrRatio(act%, tar%), BlThreatInital_AffGap(act%, tar%), ally, BlThreatInital_TrdShr(act%, tar%), contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay, lwrdemoc}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years", "Lower Democracy"}, context)
                                    End If
                                    If threatconstsw > 7 Then ' Overflow capture; Uses Full Model
                                        Call AnalFunc("Dyadic THREAT (Rivalry)",
                                            New Single() {pwrrat, Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay, lwrdemoc}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years", "Lower Democracy"}, context)
                                    End If
                                Else ' Undirected UCDP-PRIO 
                                    If threatconstsw = 0 Then ' All Active
                                        Call AnalFunc("Dyadic THREAT (Undirected UCDP-PRIO)",
                                            New Single() {pwrrat, Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                    End If
                                    If threatconstsw = 1 Then ' Just Trade 
                                        Call AnalFunc("Dyadic THREAT (Undirected UCDP-PRIO)",
                                            New Single() {BlThreatInital_PwrRatio(act%, tar%), BlThreatInital_AffGap(act%, tar%), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                    End If
                                    If threatconstsw = 2 Then ' Just Democracy
                                        Call AnalFunc("Dyadic THREAT (Undirected UCDP-PRIO)",
                                            New Single() {BlThreatInital_PwrRatio(act%, tar%), Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, BlThreatInital_TrdShr(act%, tar%), contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                    End If
                                    If threatconstsw = 3 Then ' Just Power 
                                        Call AnalFunc("Dyadic THREAT (Undirected UCDP-PRIO)",
                                            New Single() {pwrrat, BlThreatInital_AffGap(act%, tar%), ally, BlThreatInital_TrdShr(act%, tar%), contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                    End If
                                    If threatconstsw = 4 Then ' No Power
                                        Call AnalFunc("Dyadic THREAT (Undirected UCDP-PRIO)",
                                            New Single() {BlThreatInital_PwrRatio(act%, tar%), Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                    End If
                                    If threatconstsw = 5 Then ' No Democracy 
                                        Call AnalFunc("Dyadic THREAT (Undirected UCDP-PRIO)",
                                            New Single() {pwrrat, BlThreatInital_AffGap(act%, tar%), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                    End If
                                    If threatconstsw = 6 Then ' No Trade
                                        Call AnalFunc("Dyadic THREAT (Undirected UCDP-PRIO)",
                                            New Single() {pwrrat, Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, BlThreatInital_TrdShr(act%, tar%), contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                    End If
                                    If threatconstsw = 7 Then ' All Fixed 
                                        Call AnalFunc("Dyadic THREAT (Undirected UCDP-PRIO)",
                                            New Single() {BlThreatInital_PwrRatio(act%, tar%), BlThreatInital_AffGap(act%, tar%), ally, BlThreatInital_TrdShr(act%, tar%), contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                    End If
                                    If threatconstsw > 7 Then ' Default to full
                                        Call AnalFunc("Dyadic THREAT (Undirected UCDP-PRIO)",
                                            New Single() {pwrrat, Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                            New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                    End If
                                End If
                            End If
                            'If act% = 174 And tar% = 134 Then Stop
                            If wpindsw = 5 Then ' directed MIDs
                                If threattradesw = 0 Then 'calculate using current year values 
                                    For S% = 1 To NSec '6
                                        tradeshr = tradeshr + (XDYAD(act%, tar%, S%) + XDYAD(tar%, act%, S%)) / 1000000000
                                    Next
                                    tradeshr = tradeshr / GDP(act%) * 100

                                ElseIf threattradesw = 1 Then ' use inital year values for all but energy
                                    tradeshr = (IDyTrdShrGdp(act%, tar%) / 100 * GDP(act%) + (XDYAD(act%, tar%, 2) + XDYAD(tar%, act%, 2)) / 1000000000) / GDP(act%) * 100

                                Else 'calculate with all held constant
                                    tradeshr = IDyTrdShrGdp(act%, tar%)
                                End If
                                'clp 2025/04/29 ***END*** adding ability to keep values constant for THREAT components
                                'tradeshr = (((XDYAD(act%, tar%, 1) + XDYAD(tar%, act%, 1)) / 1000000000) + ((XDYAD(act%, tar%, 2) + XDYAD(tar%, act%, 2)) / 1000000000) + ((XDYAD(act%, tar%, 3) + XDYAD(tar%, act%, 3)) / 1000000000) + ((XDYAD(act%, tar%, 4) + XDYAD(tar%, act%, 4)) / 1000000000) + ((XDYAD(act%, tar%, 6) + XDYAD(tar%, act%, 6)) / 1000000000)) / GDP(act%) * 100
                                If ALLIANCE(act%, tar%) > 75 Then ally = 1 Else ally = 0

                                If threatconstsw = 0 Then ' All Active 
                                    Call AnalFunc("Dyadic THREAT (Directed MIDs)",
                                        New Single() {pwrrat, Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                        New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                End If
                                If threatconstsw = 1 Then ' Just Trade
                                    Call AnalFunc("Dyadic THREAT (Directed MIDs)",
                                        New Single() {BlThreatInital_PwrRatio(act%, tar%), BlThreatInital_AffGap(act%, tar%), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                        New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                End If
                                If threatconstsw = 2 Then ' Just Democracy 
                                    Call AnalFunc("Dyadic THREAT (Directed MIDs)",
                                        New Single() {BlThreatInital_PwrRatio(act%, tar%), Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, BlThreatInital_TrdShr(act%, tar%), contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                        New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                End If
                                If threatconstsw = 3 Then ' Just Power
                                    Call AnalFunc("Dyadic THREAT (Directed MIDs)",
                                        New Single() {pwrrat, BlThreatInital_AffGap(act%, tar%), ally, BlThreatInital_TrdShr(act%, tar%), contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                        New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                End If
                                If threatconstsw = 4 Then ' No Power 
                                    Call AnalFunc("Dyadic THREAT (Directed MIDs)",
                                        New Single() {BlThreatInital_PwrRatio(act%, tar%), Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                        New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                End If
                                If threatconstsw = 5 Then ' No Democracy
                                    Call AnalFunc("Dyadic THREAT (Directed MIDs)",
                                        New Single() {pwrrat, BlThreatInital_AffGap(act%, tar%), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                        New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                End If
                                If threatconstsw = 6 Then ' No Trade 
                                    Call AnalFunc("Dyadic THREAT (Directed MIDs)",
                                        New Single() {pwrrat, Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, BlThreatInital_TrdShr(act%, tar%), contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                        New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                End If
                                If threatconstsw = 7 Then ' All Fixed
                                    Call AnalFunc("Dyadic THREAT (Directed MIDs)",
                                        New Single() {BlThreatInital_PwrRatio(act%, tar%), Abs(DEMOCPOLYARCHY(act) - DEMOCPOLYARCHY(tar%)), ally, BlThreatInital_TrdShr(act%, tar%), contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                        New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                End If
                                If threatconstsw > 7 Then ' Overflow capture; Uses Full Model
                                    Call AnalFunc("Dyadic THREAT (Directed MIDs)",
                                        New Single() {pwrrat, BlThreatInital_AffGap(act%, tar%), ally, tradeshr, contig, CSng(DistanceBilat(act%, tar%)), pyrsdecay}, z,
                                        New String() {"Power Transition Ratio", "Democracy Gap, Polyarchy", "Alliance", "Trade Dependence", "Contiguity", "Distance", "Peace Years"}, context)
                                End If
                            End If
                            ' END BRANCHES HERE
                            'If act% = 174 And tar% = 134 Then Stop
                            'If act% = 164 And tar% = 179 Then Stop
                            THREATPred = CSng(100 * (Exp(z) / (1 + Exp(z))))

                            'clp 2024/07/10 ***START*** Adding dyadic threat
                            'clp 2024/07/10 ***END*** Adding dyadic threat
                            'THREAT(act%, tar%) = THREATPred + RIVALRY(act%, tar%) 'clp 2024/12/20 adding rivalary initalization
                            'THREAT(act%, tar%) = AMAX(Amin(THREAT(act%, tar%), 100), 0)
                            'sumthreatI(tar%) = sumthreatI(tar%) + THREAT(act%, tar%) * reac(act%, tar%)

                            '           First Year:  Probability of Conventional War
                            '           --------------------------------------------
                            'If act% = 125 And tar% = 138 Then Stop
                            'ThreatTerm = AMAX(THREAT(act%, tar%), THREAT(tar%, act%)) * wpthrwar
                            'CWARPB(act%, tar%) = cwarbase(act%, tar%) + ThreatTerm
                            'CWARPB(act%, tar%) = Amin(100, CWARPB(act%, tar%))
                            'clp 2025/05/23 ***START*** Removing ordinal initalization 
                            If RIVALRY(act%, tar%) <> CSng(-99) And (losact > aprdval Or contig = 1) Then
                                THREAT(act%, tar%) = THREATPred

                            ElseIf RIVALRY(act%, tar%) = CSng(-99) And losact > aprdval Then
                                RIVALRY(act%, tar%) = 1
                                THREAT(act%, tar%) = THREATPred

                            Else 'checking if LOS-PRD
                                THREAT(act%, tar%) = 0.0000000001
                                RIVALRY(act%, tar%) = -99
                            End If 'checking if LOS-PRD
                        Else
                            THREAT(act%, tar%) = 0 ' self dyads
                        End If
                        'clp 2025/05/23 ***END*** Removing ordinal initalization 

                        STHREAT(act%, tar%) = THREAT(act%, tar%)
                        ThreatTerm = AMAX(THREAT(act%, tar%), THREAT(tar%, act%)) * wpthrwar
                        CWARPB(act%, tar%) = cwarbase(act%, tar%) + ThreatTerm
                        CWARPB(act%, tar%) = Amin(100, CWARPB(act%, tar%))
                    Next act%
                    'sumthreatI(tar%) = AMAX(0.01, sumthreatI(tar%))
                Next tar%
            End If
            'clp 2024/07/10 ***END*** Adding dyadic threat

            '    Contiguity scale is:  1=land contiguity; 2=0=12 miles; 3=12-24 miles;
            '    4=24-150 miles; 5=150-400 miles; 6=more than 400 miles

            For act% = 1 To NR%
                THREATSENT(act%) = 0
                For tar% = 1 To NR%
                    THREATSENT(act%) = THREATSENT(act%) + THREAT(act%, tar%) / 100 * (100 - THREATSENT(act%))
                Next tar%
            Next act%

            For tar% = 1 To NR%
                THREATREC(tar%) = 0
                For act% = 1 To NR%
                    THREATREC(tar%) = THREATREC(tar%) + THREAT(act%, tar%) / 100 * (100 - THREATREC(tar%))
                Next act%
            Next tar%

            'clp 2024/07/10 ***START*** Adding dyadic threat
            If wpindsw > 4 Then
                For act% = 1 To NR%
                    THREATSENT(act%) = 0
                    For tar% = 1 To NR%
                        THREATSENT(act%) = THREATSENT(act%) + THREAT(act%, tar%)
                        'If act% = 34 Then Stop
                        'If tar% = 146 Then Stop
                    Next tar%
                Next act%

                For tar% = 1 To NR%
                    THREATREC(tar%) = 0
                    For act% = 1 To NR%
                        THREATREC(tar%) = THREATREC(tar%) + THREAT(act%, tar%)
                    Next act%
                Next tar%
            End If
            'clp 2024/07/10 ***END*** Adding dyadic threat

            'clp 2025/10/24 ***START*** Add Monadic Leader Travel
            Dim ldrtvlest As Single
            Dim conflictind As Single
            Dim allycount As Single
            Dim ldrdatalog As Single
            Dim beckleyshr As Single
            Dim beckleytot As Single
            Dim allyind As Single

            conflictind = 0
            allycount = 0
            beckleytot = 0

            For R% = 1 To NR%
                beckleytot = beckleytot + (GDPP(R%) * GDPPCP(R%))
            Next R%


            For R% = 1 To NR%
                beckleyshr = 0
                beckleyshr = GDPP(R%) * GDPPCP(R%) / beckleytot * 100
                For j = 1 To NR%
                    allycount = allycount + ally(R%, j%)
                    conflictind = AMAX(THREAT(R%, j%) / 100, conflictind)
                Next j

                If sfcivilwarucdpsw = 1 Then
                    conflictind = AMAX(conflictind, SSFCIVILWARUCDP(R%))
                ElseIf sfcivilwarucdpsw = 3 Then 'dsb 2026/07/21
                    conflictind = AMAX(conflictind, SSFCIVILWARPARDEE(R%))
                Else
                    conflictind = AMAX(conflictind, SSFINTLWARALL(R%))
                End If

                'ldrtvlest = CSng(1.660993 + (-0.0000000000000544 * GDP(R%) * 1000000000) + (-0.00000293 * GDPPCP(R%) * 1000) + (0.5199978 * DEMOCPOLYARCHY(R%)) +
                '       (0.0009503 * (X(R%) / GDP(R%) * 100)) + (beckleyshr * 0.0177485) + (conflictind * 0.1292389) + (0.001464 * DIPLOIGO(R%) / 7) +
                '      (0.00629 * XS(R%, 2) / X(R%) * 100) + (-0.0018747 * allycount) + (0.0000329 * XARMS(R%) * 1000))
                'If R% = 58 And IY > 60 Then Stop
                ldrtvlest = CSng((0.0639355 * beckleyshr) + (-1.320654 * DEMOCPOLYARCHY(R%)) + (-0.0011699 * (X(R%) / GDP(R%) * 100)) + (0.0475679 * SDIPLOIGO(R%) / 7) + (AvgDistanceAtoB(R%) * -0.000000727) - 0.3788661)
                ldrtvlest = CSng((0.2692479 * beckleyshr) + (-0.1775876 * DEMOCPOLYARCHY(R%)) + (-0.0027898 * (X(R%) / GDP(R%) * 100)) + (0.1010465 * SDIPLOIGO(R%) / 7) + (AvgDistanceAtoB(R%) * -0.0004044) + 3.896546)
                ldrtvlest = CSng((0.1746358 * DEMOCPOLYARCHY(R%)) + (-0.0042733 * (X(R%) / GDP(R%) * 100)) + (0.1133453 * SDIPLOIGO(R%) / 7) + (AvgDistanceAtoB(R%) * -0.0003711) + 3.048309)

                ldrtvlest = CSng((1.676865 * beckleyshr) + (-6.738254 * DEMOCPOLYARCHY(R%)) + (-0.0065327 * (X(R%) / GDP(R%) * 100)) + (0.2573842 * SDIPLOIGO(R%) / 7) + (AvgDistanceAtoB(R%) * -0.0001699) - 3.74905)
                'If R% = 180 Or R% = 24 Then Stop
                'ldrtvlest = CSng(Exp(ldrtrvlshift(R%) + ldrtvlest))

                ldrtvlest = CSng(ldrtrvlshift(R%) + ldrtvlest)

                LDRTRIP(R%) = ldrtvlest * ldrtripm(R%)
                'If R% = 58 And IY > 60 Then Stop
            Next R%

            'clp 2025/10/20 ***START*** Add dyadic leader travel
            If bilateralsw = 1 Then
                Dim FBICSUM As Single

                conflictind = 0
                allycount = 0
                beckleytot = 0

                For R% = 1 To NR%
                    beckleytot = beckleytot + (GDPP(R%) * GDPPCP(R%))
                Next R%


                For R% = 1 To NR%
                    beckleyshr = 0
                    beckleyshr = GDPP(R%) * GDPPCP(R%) / beckleytot * 100
                    FBICSUM = 0
                    For j = 1 To NR%
                        allycount = allycount + ally(R%, j%)
                        'jrs 2026/07/30 conflictind = AMAX(THREAT(R%, j%) / 100, conflictind)
                        FBICSUM = FBICSUM + INFLUENCE(R%, j%)
                    Next j
                    'If R% = 180 And IY > 2 Then Stop

                    If sfcivilwarucdpsw = 1 Then
                        conflictind = AMAX(conflictind, SSFCIVILWARUCDP(R%))
                    ElseIf sfcivilwarucdpsw = 3 Then 'dsb 2026/07/21
                        conflictind = AMAX(conflictind, SSFCIVILWARPARDEE(R%))
                    Else
                        conflictind = AMAX(conflictind, SSFINTLWARALL(R%))
                    End If
                    'jrs 2026/05/21 ldrtvlest = CSng(DIPLOEMBASSY(R%) * 0.088 + FBICSUM * 0.814 + POWERGPI(R%) / 100 * 130.673 + 0.059)
                    ldrtvlest = CSng(SDIPLOEMBASSY(R%) * 0.088 + FBICSUM * 0.814 + POWERGPI(R%) / 100 * 130.673 + 0.059)

                    ldrtrvlshift(R%) = ConvergeOverTime(ldrtrvlshifti(R%), 0, 150)
                    'ldrtrvlshift(R%) = 0
                    'If GRPS(1, R%) = "Japan" Then Stop

                    ldrtvlest = CSng(ldrtrvlshift(R%) + ldrtvlest)

                    LDRTRIP(R%) = ldrtvlest * ldrtripm(R%)
                    LDRTRIP(R%) = AMAX(0, LDRTRIP(R%))
                Next R%

                'clp 2025/10/20 ***START*** Add dyadic leader travel

                Dim ldrtripdydtot As Single

                Dim MSTOCK_MIL() As Single
                ReDim MSTOCK_MIL(NR)
                Dim DYTRADESUM() As Single
                ReDim DYTRADESUM(NR)
                For R = 1 To NR
                    For j = 1 To NR
                        MSTOCK_MIL(j) = MSTOCK_MIL(j) + DYADMILMSTOCK(R, j)
                        DYTRADESUM(j) = DYTRADESUM(j) + (XDYAD(R, j, 1) + XDYAD(j, R, 1) + XDYAD(R, j, 2) + XDYAD(j, R, 2) + XDYAD(R, j, 3) + XDYAD(j, R, 3) + XDYAD(R, j, 4) + XDYAD(j, R, 4) + XDYAD(R, j, 5) + XDYAD(j, R, 5) + XDYAD(R, j, 6) + XDYAD(j, R, 6))
                    Next j
                Next R

                For j = 1 To NR%
                    'jrs 2026/07/30
                    conflictind = 0
                    For ci = 1 To NR%
                        conflictind = AMAX(THREAT(j, ci) / 100, conflictind)
                    Next ci

                    If sfcivilwarucdpsw = 1 Then
                        conflictind = AMAX(conflictind, SSFCIVILWARUCDP(j))
                    ElseIf sfcivilwarucdpsw = 3 Then
                        conflictind = AMAX(conflictind, SSFCIVILWARPARDEE(j))
                    Else
                        conflictind = AMAX(conflictind, SSFINTLWARALL(j))
                    End If

                    For R% = 1 To NR% 'inital estimate and sum of estimates
                        'jrs 2026/07/30
                        'For ci = 1 To NR%
                        '    conflictind = AMAX(THREAT(j, ci) / 100, conflictind)
                        'Next ci

                        'If sfcivilwarucdpsw = 1 Then
                        '    conflictind = AMAX(conflictind, SFCIVILWARUCDP(j))
                        'Else
                        '    conflictind = AMAX(conflictind, SFINTLWARALL(j))
                        'End If
                        beckleyshr = (GDPP(j) * GDPPCP(j)) / (GDPP(R%) * GDPPCP(R%) + GDPP(j) * GDPPCP(j))

                        allyind = ALLIANCE(R%, j)

                        If allyind > 0.9 Then
                            allyind = 1
                        Else
                            allyind = 0
                        End If

                        ldrtvlest = CSng(0.0001299 + (0.000000541 * GDPPCP(j)) + (0.000000506 * GDPPCP(R%)) + (-0.0005269 * conflictind) +
                           (0.0140274 * beckleyshr) + (0 * DistanceBilat(R%, j)) + (0.0135756 * (1 - Abs(DEMOCPOLYARCHY(R%) - DEMOCPOLYARCHY(j)))) + (0.0180946 * allyind) +
                           (0.0012572 * ((XDYAD(R%, j, 1) + XDYAD(R, j, 2) + XDYAD(R%, j, 3) + XDYAD(R%, j, 4) + XDYAD(R%, j, 5) + XDYAD(R%, j, 6) + XDYAD(j, R%, 1) + XDYAD(j, R%, 2) + XDYAD(j, R%, 3) + XDYAD(j, R%, 4) + XDYAD(j, R%, 5) + XDYAD(j, R%, 6)) / DYTRADESUM(R%) * 100)) +
                           (0 * XDYAD2(R%, j, 5)) + (0 * XDYAD2(j, R%, 5)) + (0.0004842 * DYADMILMSTOCK(j, R%) / AMAX(MSTOCK_MIL(R%), CSng(1)) * 100)) ' foreign aid at start of line was (1.121694 * XDYAD2(j, R%, 3) / 1000 / 1000)
                        'If GRPS(1, R) = "United States of America" And GRPS(1, j) = "Japan" Then Stop

                        'If R = 180 And j = 88 And IY > 40 Then Stop
                        dyldrtripshift(R%, j) = ConvergeOverTime(dyldrtripshifti(R%, j), 0, 150)
                        DYDLDRTRIP(R%, j) = AMAX(ldrtvlest + dyldrtripshift(R%, j), 0)

                        DYDLDRTRIP(R%, j) = DYDLDRTRIP(R%, j) * dyadldrtripm(R%, j)

                        'If GRPS(1, R%) = "United States of America" And GRPS(1, j) = "Japan" And IY = 14 Then Stop
                    Next
                Next
                For R% = 1 To NR%
                    ldrtripdydtot = 0
                    For j = 1 To NR
                        ldrtripdydtot = ldrtripdydtot + DYDLDRTRIP(j, R%)
                    Next
                    'If GRPS(1, R%) = "Japan" Then Stop
                    'If GRPS(1, R%) = "New Zealand" And IY = 11 Then Stop
                    If LDRTRIP(R%) <> 0 And ldrtripdydtot <> 0 Then
                        ldrtripdydtot = LDRTRIP(R%) / ldrtripdydtot ' calculate conversion for dyadic sum
                    Else
                        ldrtripdydtot = 0
                    End If
                    For j = 1 To NR% 'calculate cont. shares using conversion
                        'If GRPS(1, j) = "United States of America" And GRPS(1, R%) = "Japan" Then Stop
                        DYDLDRTRIP(j, R%) = DYDLDRTRIP(j, R%) * ldrtripdydtot
                        SDYDLDRTRIP(j, R%) = DYDLDRTRIP(j, R%)
                    Next
                Next

                For R% = 1 To NR%
                    LDRTRIPOUT(R%) = 0
                    For j = 1 To NR%
                        LDRTRIPOUT(R%) = LDRTRIPOUT(R%) + DYDLDRTRIP(R%, j)
                    Next
                Next

                Using db As IFsForDyadicContext = New IFsForDyadicContext()
                    Dim conn = db.Database.GetDbConnection
                    If conn.State = 0 Then 'closed
                        conn.Open()
                    End If

                    'jrs 2019/11/18 *** begin ***
                    Using cmd2 = conn.CreateCommand()
                        cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
                        cmd2.ExecuteNonQuery()
                    End Using
                    db.Database.AutoTransactionsEnabled = False
                    db.ChangeTracker.AutoDetectChangesEnabled = False
                    db.ChangeTracker.LazyLoadingEnabled = False
                    db.BulkInsertDYDLDRTRIP(DYDLDRTRIP, CShort(FirstYear + IY - 1), NR)
                End Using
            End If
            'clp 2025/10/20 ***END*** Add dyadic leader travel
            'clp 2025/10/24 ***END*** Add Monadic Leader Travel

            '   GOVT EXPENDITURES IN TRADITIONAL DESTINATION CATEGORIES
            '   1=DEFENSE; 2=HEALTH; 3=EDUCATION; 4=R&D; 5=OTHER; 6=FOREIGN AID
            '
            '   All but First 1:  Compute Defense Multiplier Via Action-Reaction
            '   --------------------------------------------------------------
            If actreaon > 0.5 Then
                For tar% = 1 To NR%

                    sumthreat = 0
                    '            SumReact = 0
                    For act% = 1 To NR%
                        sumthreat = sumthreat + THREAT(act%, tar%) * reac(act%, tar%)
                        '                If tar% <> act% Then SumReact = SumReact + reac(act%, tar%)
                    Next act%
                    If sumthreatI(tar%) > 0.01 Then
                        gkmul(tar%) = CSng(AMAX(0.5, Amin(2, ((sumthreat + 10) / (sumthreatI(tar%) + 10)) ^ wpmilthr)))
                        'Use of additive factor prevents threat that goes from 0 to small number causing big military shift
                        'E.g., Belgium has essentially no history of threat in base year
                    Else
                        gkmul(tar%) = 1
                    End If
                Next tar%
            Else
                For tar% = 1 To NR%
                    gkmul(tar%) = 1
                Next tar%
            End If
            '
            '   All but First 1:  Expenditures With/Without Action-Reaction (Initialized in Economy)
            '   ----------------------------------------------------------------------------------

            For R% = 1 To NR%

                'If RegName$(R%) = "Congo, Democratic Republic of" Then Stop

                '       Recompute gk with change in GDPPC
                '           1=DEFENSE; 2=HEALTH; 3=EDUCATION; 4=R&D; 5=OTHER; 6=FOREIGN AID
                If context.HistoricMode = True Then
                    Call XYTABL("GDP/Capita (PPP) Versus Govt Exp R&D as % of GNP (2000) Linear",
                    Amin(40, GDPPCP(R%)), gkcomp(RandD), context)
                    Call XYTABL("GDP/Capita (PPP) Versus R&D Total  as % of GNI (1980) - linear",
                    Amin(40, GDPPCP(R%)), predrandd, context)
                    Call XYTABL("GDP/Capita (PPP) Versus Govt Exp Mil as % of GDP (1965) - linear",
                Amin(40, GDPPCP(R%)), gkcomp(Mil), context)
                    Call XYTABL("GDP/Capita (PPP) Versus Govt Exp Hlth as % of GDP (1990) - log",
                Amin(40, GDPPCP(R%)), gkcomp(Health), context)
                    Call XYTABL("GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (1960) - log",
                Amin(40, GDPPCP(R%)), gkcomp(Educ), context)
                Else
                    'jrs 2024/06/06 
                    'Call XYTABL("GDP/Capita (PPP) Versus Govt Exp R&D as % of GNP (2000) - linear",
                    'Amin(40, GDPPCP(R%)), gkcomp(RandD), context)
                    Call XYTABL(TblNameRandDExp, Amin(40, GDPPCP(R%)), gkcomp(RandD), context)

                    'jrs 2013/11/28
                    '            Call XYTABL("GDP/Capita (PPP) Versus R&D Total  as % of GNI (2000) - linear", _
                    '                    Amin(40, GDPPCP(R%)), PredRandD)
                    'jrs 2024/06/28 Call XYTABL("GDP/Capita (PPP 2005) Versus R&D Total  as % of GDP (2009) Linear",
                    'Amin(40, GDPPCP(R%)), predrandd, context)
                    Call XYTABL(TblNameRandDPctGDP, Amin(40, GDPPCP(R%)), predrandd, context)
                    'jrs 2024/06/06 
                    '    Call XYTABL("GDP/Capita (PPP) Versus Govt Exp Mil as % of GDP (2000) - linear",
                    'Amin(40, GDPPCP(R%)), gkcomp(Mil), context)
                    Call XYTABL(TblNameMilExp, Amin(40, GDPPCP(R%)), gkcomp(Mil), context)

                    'jrs 2024/06/06 
                    '    Call XYTABL("GDP/Capita (PPP) Versus Govt Exp Hlth as % of GDP (2000) - log",
                    'Amin(40, GDPPCP(R%)), gkcomp(Health), context)
                    Call XYTABL(TblNameHealthExp, Amin(40, GDPPCP(R%)), gkcomp(Health), context)

                    'jrs 2024/06/10 
                    'Call XYTABL("GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (2002) - log",
                    'Amin(40, GDPPCP(R%)), gkcomp(Educ), context)
                    Call XYTABL(TblNameEducExp, Amin(40, GDPPCP(R%)), gkcomp(Educ), context)
                End If
                predrandd = predrandd * randdexpm(R%) 'bbh 2015/03/25
                gkcomp(Other) = gk(R%, Other)
                'If RegName(R%) = "Bangladesh" Then Stop 'Debug.Print IY; gkri(R%, Other); gk(R%, Other)

                For S% = 1 To NGovExp% '(up through other)
                    'mti 2017/05/21 start
                    'GKShift = ConvergeOverTime(gkri(R%, S%), 1, 200)
                    GKShift = ConvergeOverTime(gkri(R%, S%), 1, 75)
                    'mti 2017/05/21 End
                    'GKShift = ConvergeOverTime(gkri(R%, S%), 1, mfpconv)
                    gk(R%, S%) = gkcomp(S%) * GKShift
                    'GKShift = ConvergeOverTime(gkri(R%, S%), 0, mfpconv)
                    'gk(R%, S%) = AMAX(0.001, gkcomp(S%) + GKShift)
                Next S%
                'If RegName$(R%) = "Congo, Republic of" Then Stop

                govconwb(R%) = XWBFLOWS(R%, 1) + XWBFLOWS(R%, 2)
                For S% = 1 To NGovExp% '(up through other)
                    gdswb(R%, S%) = 0
                Next S%
                gdswb(R%, Educ) = XWBFLOWS(R%, 1) 'Education
                gdswb(R%, Health) = XWBFLOWS(R%, 2)  'Health



                'mti 2016/07/16 Target value in GDS%GDP start
                Dflt_GDS_Trgt_Yr = 10
                ' If R% = 1 Then Stop
                'Spending - Target - as a % of GDP
                For S% = 1 To NGovExp%
                    If gdstrgtval(R%, S%) > 0 Then 'has a target (as a % of GDP) in Military expenditure, greater or less than initial value, but not 0
                        Trgt_Yr = CInt(gdstrgtyr(R%, S%))
                        If Trgt_Yr = 0 Then Trgt_Yr = Dflt_GDS_Trgt_Yr
                        If Trgt_Yr >= IY Then
                            'mti 2018/08/24 Start
                            '                    GDSTrgtPcnt = AMAX(0.1, igk(R%, S%) + (IY - 1) * (gdstrgtval(R%, S%) - igk(R%, S%)) / (Trgt_Yr - 1))
                            GDSTrgtPcnt = AMAX(0.01, igk(R%, S%) + (IY - 1) * (gdstrgtval(R%, S%) - igk(R%, S%)) / (Trgt_Yr - 1))
                            'mti 2018/08/24 End
                        Else
                            GDSTrgtPcnt = gdstrgtval(R%, S%)
                        End If
                        'Convert to dollars; ignore the spneding multiplier, gdsm
                        GDSTrgt(R%, S%) = GDP(R%) * GDSTrgtPcnt / 100
                    Else
                        GDSTrgt(R%, S%) = 0
                    End If
                Next S%
                'jrs 2021/09/09
                If gdscnstsw(R%, Mil) = 0 Then
                    'mti 2016/07/16 end
                    GDS(R%, Mil) = GDP(R%) * gk(R%, Mil) / 100 * gdsm(R%, Mil) * gkmul(R%)
                Else
                    GDS(R%, Mil) = GDP(R%) * igk(R%, Mil) / 100
                End If

                For S% = 2 To NGovExp% 'for all sectors other than military
                    'jrs 2021/09/09
                    If gdscnstsw(R%, S%) = 0 Then
                        'mti June 08, 2011 - start
                        'mti Sep 12, 2011 - start
                        'If S% <> Educ And S% <> Infra Then 'Education multiplier will come in only after all education
                        If S% <> Educ And S% <> Infra And S% <> InfraOther And S% <> Health Then 'jrs 2014/05/26 added Health
                            GDS(R%, S%) = GDP(R%) * gk(R%, S%) / 100 * gdsm(R%, S%) + gdswb(R%, S%)
                        Else
                            'Education multiplier will come in only after other spending logic (e.g., set aside) is applied on base spending
                            'This will give 0 GDS on infra, at this point; that's OK as supply side does not exist for infra spending (check infrafinon = 0 case though)
                            GDS(R%, S%) = GDP(R%) * gk(R%, S%) / 100 + gdswb(R%, S%)
                        End If
                    Else
                        If S% = Health Then
                            GDS(R%, S%) = GDP(R%) * healthgdpri(R%)
                        ElseIf S% = Educ Then
                            GDS(R%, S%) = GDP(R%) * educgdpri(R%)
                        Else
                            GDS(R%, S%) = GDP(R%) * igk(R%, S%) / 100
                        End If
                    End If
                Next S%

                'Recompute Govt RandD expenditures using function
                'jrs 2024/06/06 
                'Call XYTABL("GDP/Capita (PPP) Versus Govt Exp R&D as % of GNP (2000) - linear",
                'Amin(40, GDPPCP(R%)), RandDComp, context)
                Call XYTABL(TblNameRandDExp, Amin(40, GDPPCP(R%)), RandDComp, context)
                'jrs 2024/06/06
                'Call XYTABL("GDP/Capita (PPP) Versus Govt Exp R&D as % of GNP (2000) - linear",
                'Amin(40, GDPPCIP(R%)), RandDComp2, context)
                Call XYTABL(TblNameRandDExp, Amin(40, GDPPCIP(R%)), RandDComp2, context)

                RandDMul = RandDComp / AMAX(0.0001, RandDComp2)
                RandDGDP = RandDI(R%) * RandDMul
                RandDGDP = ConvergeOverTime(RandDGDP, RandDComp, 1000) 'Base, Target, Years
                'jrs 2021/09/09
                If gdscnstsw(R%, RandD) = 0 Then
                    '1000 convergence or high value necessary, or will swing eocnomic growth
                    GDS(R%, RandD) = RandDGDP * GDP(R%) / 100 * gdsm(R%, RandD)
                Else
                    GDS(R%, RandD) = GDP(R%) * igk(R%, RandD) / 100
                End If

                'If RegName(R%) = "Gambia" Then Stop

                'bbh 2015/03/25 start  Big gdsm and negative RandDexpShr could drive RANDDEXP negaive
                'Compute total R&D from function derived value (PredRandD) and shift from first year
                If RandDExpSh(R%) > 0 Then
                    RANDDEXP(R%) = predrandd + ConvergeOverTime(RandDExpSh(R%), 0, 600 * 2)
                    'RANDDEXP(R%) = PredRandD + ConvergeOverTime(RandDExpSh(R%), 0, 600 * 2) * gdsm(R%, RandD)
                Else
                    RANDDEXP(R%) = predrandd + ConvergeOverTime(RandDExpSh(R%), 0, 200 * 2)
                    'RANDDEXP(R%) = PredRandD + ConvergeOverTime(RandDExpSh(R%), 0, 200 * 2) * gdsm(R%, RandD)
                End If
                'bbh 2015/03/25 end

                '   All but First 1:  Infrastructure Expenditures (Public)
                '------------------------------------------------------------------------------------------

                'adjustment ratio from initial value (first year budget demand and supply ratio) converging to 1 (muted for now: see first year code)
                cInfraCostGDSInfraRI = 1 + ConvergeOverTime1(InfraCostGDSInfraRIShift(R%), 0, 70)
                InfraCostGDSInfraRI(R%) = cInfraCostGDSInfraRI
                InfraTotDemand = CalcInfraBudgetDemand(R%)

                DemandCalcInfra = InfraTotDemand * InfraCostGDSInfraRI(R%)
                'If IY = 29 And R = 95 Then Stop
                'infrabudsdrat is the set aside/protect parameter
                'mti 2016/01/07 - start
                'BudBalInfra = AMAX(0, Amin(infrabudsdrat, 1))

                'mti 2016/07/16 start
                'BudBalInfra = AMAX(0, Amin(infrabudsdrat(R%), 1))
                'mti 2016/07/16 end

                'mti 2016/01/07 - end

                'switch to turn infrastructure finance on and off
                If infrafinon = 1 Then
                    'jrs 2021/09/09
                    If gdscnstsw(R%, Infra) = 0 Then
                        GDS(R%, Infra) = DemandCalcInfra  'billion dollars
                    Else
                        GDS(R%, Infra) = GDP(R%) * igk(R%, Infra) / 100
                    End If

                    'InfraOther
                    'jrs 2021/09/09
                    If gdscnstsw(R%, InfraOther) = 0 Then
                        'As % of GDP: using function from DSR (function should go to Tablfunc)
                        GDS(R%, InfraOther) = CSng(1.8162 + 0.061 * Log(GDPPCP(R%)))
                        'initial shift
                        GKShift = ConvergeOverTime(gkri(R%, InfraOther), 1, 200)
                        GDS(R%, InfraOther) = GDS(R%, InfraOther) * GKShift
                        'Convert to billion dollars
                        GDS(R%, InfraOther) = (GDS(R%, InfraOther) / 100) * GDP(R%)
                    Else
                        GDS(R%, InfraOther) = GDP(R%) * igk(R%, InfraOther) / 100
                    End If
                    DemandCalcInfraOther = GDS(R%, InfraOther)
                Else
                    GDS(R%, Infra) = 0
                    DemandCalcInfra = 0
                    GDS(R%, InfraOther) = 0
                End If



                ' Education: Forcing GDS for education in budget model to education model spending demand * 1.1 over time

                'mti 2016/07/16 start  ' gdsbudgetconv(R%,Educ)
                'mti 2016/01/07 - start
                'budbal = AMAX(0, Amin(edbudgon, 1))
                'budbal = AMAX(0, Amin(edbudgon(R%), 1))
                'mti 2016/01/07 - end
                'mti 2016/07/16 end

                'mti January 06, 2009
                'converge towards 1.1; 10% for pre-primary or non-levelwise
                cEdCostGDSEdRI = CSng(1.1 + ConvergeOverTime1(EdCostGDSEdRIShift(R%), 0, 70))
                'cap change to 1%
                EdCostGDSEdRI(R%) = CSng(AMAX(EdCostGDSEdRI(R%) * 0.99, Amin(EdCostGDSEdRI(R%) * 1.01, cEdCostGDSEdRI)))
                'mti January 06, 2009 - end

                EcGrTerm = ConvergeOverTime(igdpr(R%), 0, 50)
                DemandCalc = SEdTotCost(R%) * (1 + EcGrTerm) * EdCostGDSEdRI(R%)

                'If RegName(R%) = "Afghanistan" Then Stop
                'jrs 2021/09/09
                If gdscnstsw(R%, Educ) = 0 Then
                    'mti 2015/11/15 -start
                    If GDS(R%, Educ) > DemandCalc Then
                        'supply is historically higher (e.g., Brazil) - supply should gradually converge wth demand
                        diff = GDS(R%, Educ) - DemandCalc
                        diff = ConvergeOverTime1(diff, 0, 70) '50
                        GDS(R%, Educ) = DemandCalc + diff
                        NoSurplus = False
                    Else

                        'if demand is higher; budget demanded needs to be prioritized (according to edbudgon value)
                        'check for transients - shouldn't any as demand and supply should be close when there is a switch
                        'mti 2016/07/16 start  ' gdsbudgetconv(R%,Educ)

                        'mti 2016/12/09 start
                        'GDS(R%, Educ) = DemandCalc * gdsbudgetconv(R%, Educ) + _
                        'GDS(R%, Educ) * (1 - gdsbudgetconv(R%, Educ))

                        'The above (commented) convergence code was distorting the protection mechanism by reducing demand through convergence
                        GDS(R%, Educ) = DemandCalc * gdsbudgetprotec(R%, Educ) +
                        GDS(R%, Educ) * (1 - gdsbudgetprotec(R%, Educ))
                        'mti 2016/12/09 End

                        'mti 2016/07/16 end

                    End If

                End If



                'mti 2014/03/27 -start
                ''GDS(R%, Educ) = DemandCalc * (1 - budbal) + _
                'GDS(R%, Educ) * budbal

                'GDS(R%, Educ) = DemandCalc * budbal + _
                'GDS(R%, Educ) * (1 - budbal)

                'mti 2014/03/27 -end

                'mti 2015/11/15 -end

                'jrs 2014/05/26 *** begin ***
                'Expenditures Side
                '=================

                'Private Expenditures
                '--------------------
                'jrs 2014/05/07 Call AnalFunc("Private Health Expenditures % of GDP (2012)", Array(GDPPCP(R), EDYRSAG25(R, 3)), PrivHLExp)
                'jrs 2014/05/30 Call XYTABL(PrivHLExpPctGDPFxName, GDPPCP(R), PrivHLExp)
                'jrs 2014/05/30 PrivHLExp = PrivHLExp + HLEXPENDPRIVShift(R)
                'Convert to absolute terms
                'jrs 2014/05/30 HLEXPENDPRIV(R) = PrivHLExp * GDP(R) / 100
                HLEXPENDPRIV(R) = SHLCOST(R) * PrivExpToCost(R%)

                'All but First: Budget gap for Health expenditures vs cost (demand)
                '-------------------------------------------------------------------
                If IY = 2 Then
                    HlGrTerm(R) = igdpr(R)
                Else
                    If SHLCOST2(R) > 0 Then
                        HlGrTerm(R) = CSng(0.8 * HlGrTerm(R) + 0.2 * (SHLCOST(R) - SHLCOST2(R)) / SHLCOST2(R))
                    End If
                End If

                DemandCalc = SHLCOST(R%) * (1 + HlGrTerm(R)) * (1 - PrivExpToCost(R%))

                'If RegName(R%) = "Chad" Then Stop
                '        If GDS(R%, Health) > DemandCalc Then
                '            diff = GDS(R%, Health) - DemandCalc
                '            diff = ConvergeOverTime1(diff, 0, 50)
                '            GDS(R%, Health) = DemandCalc + diff
                '        End If
                'mti 2016/01/07 - start
                'GDS(R%, Health) = DemandCalc * hlbudsdrat + _
                'GDS(R%, Health) * (1 - hlbudsdrat)


                'mti 2016/07/16 start
                'GDS(R%, Health) = DemandCalc * hlbudsdrat(R%) + _
                'GDS(R%, Health) * (1 - hlbudsdrat(R%))

                'mti 2016/12/09 start
                'GDS(R%, Health) = DemandCalc * gdsbudgetconv(R%, Health) + _
                'GDS(R%, Health) * (1 - gdsbudgetconv(R%, Health))

                'The above (commented) convergence code was distorting the protection mechanism by reducing demand through convergence
                'jrs 2021/09/09
                If gdscnstsw(R%, Health) = 0 Then
                    GDS(R%, Health) = DemandCalc * gdsbudgetprotec(R%, Health) +
                    GDS(R%, Health) * (1 - gdsbudgetprotec(R%, Health))
                End If
                'mti 2016/12/09 End


                'mti 2016/07/16 end

                'mti 2016/01/07 - end

                'If GRPS(1, R) = "United States of America" And IY + FirstYear - 1 = 2100 Then Stop
                'jrs 2014/05/26 *** end ***
                'If RegName(R%) = "Gambia" Then Stop

                If gdsnormalize <> 1 Then
                    'bbh 2015/03/25 start Back out gdsm in rump RANDEXP to avoid negative rump and total after normalizaton
                    'RANDDEXP(R%) = RANDDEXP(R%) - GDS(R%, RandD) / AMAX(0.001, GDP(R%))
                    RANDDEXP(R%) = RANDDEXP(R%) - GDS(R%, RandD) / AMAX(0.001, GDP(R%)) * 100 / gdsm(R%, RandD)
                End If

                If gdsnormalize = 1 Then 'base (budget constrained) case

                    'RANDDEXP(R%) = RANDDEXP(R%) - GDS(R%, RandD) / AMAX(0.001, GDP(R%))
                    RANDDEXP(R%) = RANDDEXP(R%) - GDS(R%, RandD) / AMAX(0.001, GDP(R%)) * 100 / gdsm(R%, RandD)
                    'If RegName(R%) = "Gambia" Then Stop
                    'bbh 2015/03/25 end

                    'Education/Infra/Infraother set aside/ protection (calculated from base amounts)

                    'mti 2016/07/16 start  'gdsbudgetprotec(R%,Educ)
                    '        'GDSEdSetAside = GDS(R%, Educ) * Budbal
                    '        GDSEdSetAside = GDS(R%, Educ) * gdsbudgetprotec(R%, Educ)
                    '
                    '        'GDSInfraSetAside = GDS(R%, Infra) * BudBalInfra
                    '        GDSInfraSetAside = GDS(R%, Infra) * gdsbudgetprotec(R%, Infra)
                    '
                    '

                    '        'GDSInfraOtherSetAside = GDS(R%, InfraOther) * BudBalInfra 'no set aside for infraother; used to be in the same proportion as infra
                    '        'GDSRandDSetAside = (GDS(R%, RandD) / gdsm(R%, RandD)) * rdbudprotec(R%) 'set aside/protection applied on base amount only
                    '        GDSRandDSetAside = (GDS(R%, RandD) / gdsm(R%, RandD)) * gdsbudgetprotec(R%, RandD) 'set aside/protection applied on base amount only
                    '
                    '        'jrs 2014/05/26
                    '        'GDSHLsetaside = GDS(R%, Health) * hlbudsdrat
                    '        GDSHLsetaside = GDS(R%, Health) * gdsbudgetprotec(R%, Health)
                    '

                    '

                    'mti 2016/09/09 Start
                    '
                    'Now apply gdsm multipliers on infra, education and infraother (multipliers for other sectors are applied already)
                    GDS(R%, Infra) = GDS(R%, Infra) * gdsm(R%, Infra)
                    GDS(R%, InfraOther) = GDS(R%, InfraOther) * gdsm(R%, InfraOther)
                    GDS(R%, Educ) = GDS(R%, Educ) * gdsm(R%, Educ)
                    'jrs 2014/05/26
                    GDS(R%, Health) = GDS(R%, Health) * gdsm(R%, Health)

                    'mti 2016/09/09 End


                    'Spending - Target - as a % of GDP
                    For S% = 1 To NGovExp%
                        If gdstrgtval(R%, S%) > 0 Then 'has a target (as a % of GDP) in Military expenditure, greater or less than initial value, but not 0
                            GDS(R%, S%) = GDSTrgt(R%, S%)
                        End If
                    Next S%
                    'mti 2016/07/16 end

                    'Normalization of all GDS to GOVCON

                    'Total Fund Needs
                    GTOT = 0
                    For S% = 1 To NGovExp%
                        'jrs 2021/09/09
                        If gdscnstsw(R%, S%) = 0 Then
                            GDS(R%, S%) = AMAX(0, Amin(GOVCON(R%), GDS(R%, S%)))
                        End If
                        GTOT = GTOT + GDS(R%, S%)
                    Next S%
                    'If IY = 29 And R = 95 Then Stop
                    'If RegName(R%) = "Brazil" Then Stop

                    'mti 2016/07/16 start
                    'Adjust govcon only when there is shortage
                    If GTOT > GOVCON(R%) Then GOVCON(R%) = GOVCON(R%) * (1 - govconswing) + GTOT * govconswing

                    'Determine set asides
                    If GTOT > GOVCON(R%) Then
                        'fund shortage

                        'Determine set asides
                        For S% = 1 To NGovExp%
                            gdssetaside(S%) = 0
                            'jrs 2021/09/09 added or gdscnstsw(R%,S%)>0
                            If gdstrgtval(R%, S%) > 0 Or gdscnstsw(R%, S%) > 0 Then 'GDS%GDP target - full set aside
                                gdssetaside(S%) = GDS(R%, S%)
                            ElseIf gdsbudgetprotec(R%, S%) > 0 Then 'set aside a portion up to 100%
                                gdssetaside(S%) = GDS(R%, S%) * Amin(1, gdsbudgetprotec(R%, S%))
                            End If
                        Next S%


                        '            'Subtract protected fund from total demand and supply of funds
                        '            GDS(R%, Educ) = GDS(R%, Educ) - GDSEdSetAside
                        '            GDS(R%, Infra) = GDS(R%, Infra) - GDSInfraSetAside
                        '
                        '            'mti 2016/01/07 - start
                        '            'GDS(R%, InfraOther) = GDS(R%, InfraOther) - GDSInfraOtherSetAside
                        '            'mti 2016/01/07 - end
                        '
                        '            'jrs 2014/05/26
                        '            GDS(R%, Health) = GDS(R%, Health) - GDSHLsetaside
                        '
                        '            'mti 2016/01/07 - start
                        '            GDS(R%, RandD) = GDS(R%, RandD) - GDSRandDSetAside
                        '            'jrs 2014/05/26
                        '            'GTOT = AMAX(0.01 * GTOT, GTOT - GDSEdSetAside - GDSInfraSetAside - GDSInfraOtherSetAside)
                        '            'GTOT = AMAX(0.01 * GTOT, GTOT - GDSEdSetAside - GDSInfraSetAside - GDSInfraOtherSetAside - GDSHLsetaside)
                        '            GTOT = AMAX(0.01 * GTOT, GTOT - GDSEdSetAside - GDSInfraSetAside - GDSHLsetaside - GDSRandDSetAside)
                        '
                        '
                        '
                        '            'jrs 2014/05/26
                        '            'GOVCONRed = AMAX(0.01 * GOVCON(R%), GOVCON(R%) - GDSEdSetAside - GDSInfraSetAside - GDSInfraOtherSetAside)
                        '            'GOVCONRed = AMAX(0.01 * GOVCON(R%), GOVCON(R%) - GDSEdSetAside - GDSInfraSetAside - GDSInfraOtherSetAside - GDSHLsetaside)
                        '            GOVCONRed = AMAX(0.0)



                        '            'Distribute the remaining funds
                        '            For S% = 1 To NGovExp%
                        '                 GDS(R%, S%) = GDS(R%, S%) / AMAX(0.001, GTOT) * GOVCONRed
                        '            Next S%
                        '
                        '            'Add back the protected portions
                        '
                        '
                        '
                        '
                        '            GDS(R%, Educ) = GDS(R%, Educ) + GDSEdSetAside
                        '            GDS(R%, Infra) = GDS(R%, Infra) + GDSInfraSetAside

                        '            'GDS(R%, InfraOther) = GDS(R%, InfraOther) + GDSInfraOtherSetAside

                        '
                        '            'jrs 2014/05/26
                        '            GDS(R%, Health) = GDS(R%, Health) + GDSHLsetaside
                        '
                        '            'mti 2016/01/07 - start
                        '            GDS(R%, RandD) = GDS(R%, RandD) + GDSRandDSetAside
                        '            'mti 2016/01/07 - end


                    Else 'no fund shortage

                        For S% = 1 To NGovExp%
                            gdssetaside(S%) = 0
                            'jrs 2021/09/09 added or gdscnstsw(R%,S%)>0
                            If gdstrgtval(R%, S%) > 0 Or gdscnstsw(R%, S%) > 0 Then gdssetaside(S%) = GDS(R%, S%) 'GDS%GDP target - full set aside
                        Next S%


                        '            For S% = 1 To NGovExp%
                        '              GDS(R%, S%) = GDS(R%, S%) / AMAX(0.001, GTOT) * GOVCON(R%)
                        '            Next S%

                    End If

                    'Recompute GDS and total accounting for set aside
                    Tot_SetAside = 0
                    For S% = 1 To NGovExp%
                        GDS(R%, S%) = GDS(R%, S%) - gdssetaside(S%)
                        Tot_SetAside = Tot_SetAside + gdssetaside(S%)
                    Next S%

                    'Prepare for normalization with set-aside; with at least 1% of GOVCON saved for those with no-set-aside
                    GOVCONRed = AMAX(0.01 * GOVCON(R%), GOVCON(R%) - Tot_SetAside)
                    GTOT = GTOT - Tot_SetAside ' AMAX(0.01 * GTOT, GTOT - Tot_SetAside)

                    'Distribute the remaining funds proportionate to sectoral GDS (minus set aside); and then add back set aside
                    If GTOT >= 0 Then
                        For S% = 1 To NGovExp%
                            'Calculate normalized share of sector from total minus all set-aside
                            If GTOT > 0 Then GDS(R%, S%) = GDS(R%, S%) / AMAX(0.001, GTOT) * GOVCONRed
                            'reattach set aside
                            GDS(R%, S%) = GDS(R%, S%) + gdssetaside(S%)
                        Next S%
                    Else
                        'GTOT went negative after set aside
                        'reattach the set aside and do a plain normalization
                        GTOT = 0
                        For S% = 1 To NGovExp%
                            GDS(R%, S%) = GDS(R%, S%) + gdssetaside(S%)
                            GTOT = GTOT + GDS(R%, S%)
                        Next S%
                        For S% = 1 To NGovExp%
                            GDS(R%, S%) = GDS(R%, S%) / AMAX(0.001, GTOT) * GOVCON(R%)
                        Next S%
                    End If

                    'mti 2016/07/16 end gdstarget

                    'mti 2014/03/27 - end

                    'jrs 2014/05/26
                    'Total Health Expenditures
                    '-------------------------
                    HLEXPEND(R) = GDS(R, Health) + HLEXPENDPRIV(R)

                    'mti Sep 04, 2012 -start
                    'Note from MTI: The following block disburse education surplus to other sectors (check countries like Congo, Rep, Iran, Uzbekistan, Turkmenistan, Sudan for such surplus))
                    'MTI commented this code so that any education surplus can go back to education model changing enrollment and unit cost therein
                    'This resulted in a GDP hike, most notably in Sudan and other countries
                    'So, MTI uncommented and kept it as is
                    'mti 2014/03/27 - start
                    '        If GDS(R%, Educ) > DemandCalc Then 'There is a surplus -- proceed to eliminate
                    '
                    '            'If GDS(R%, Educ) / DemandCalc > 1.05 Then Stop
                    '
                    '            Surplus = GDS(R%, Educ) - DemandCalc
                    '            GTOT = 0
                    '            For S% = 1 To NGovExp%
                    '                If S% <> Educ Then GTOT = GTOT + GDS(R%, S%)
                    '            Next S%
                    '            For S% = 1 To NGovExp%
                    '                If S% <> Educ Then
                    '                    GDS(R%, S%) = GDS(R%, S%) + Surplus * GDS(R%, S%) / AMAX(0.001, GTOT)
                    '                End If
                    '            Next S%
                    '            GDS(R%, Educ) = DemandCalc
                    '
                    '        End If
                    'mti 2014/03/27 - end
                    'mti Sep 04, 2012 -end

                End If 'gdsnormalize = 1 Then 'base case

                RANDDEXP(R%) = RANDDEXP(R%) + GDS(R%, RandD) / AMAX(0.001, GDP(R%)) * 100
                'mti R&D Septembe 2010 -end
                'If RegName(R%) = "Gambia" Then Stop
                SRANDDEXP(R%) = RANDDEXP(R%)

                '   Preparation of government spending for other modules
                MORTMG(R%) = 1.0! - (GDS(R%, Health) / GDP(R%) - healthgdpri(R%)) * elashc 'prepare for Population
                If IY% = 2 Then
                    SMORTMG(R%) = MORTMG(R%)
                Else
                    MORTMG(R%) = CSng(MORTMG(R%) * 0.2 + SMORTMG(R%) * 0.8)
                    SMORTMG(R%) = MORTMG(R%)
                End If
                If MORTMG(R%) < 0.5 Then MORTMG(R%) = 0.5
                SGDSMil(R%) = GDS(R%, Mil)
                'clp 2016/08/26 - adding government spending on R&D
                SGDSRandD(R%) = GDS(R%, RandD)

                'mti February 2011 Infarstructure - start
                If gdsm(R%, Health) >= 1 Then
                    'for use in next year in WatSan; don't allow sliding down
                    sHlthExpPcntGDP(R%) = AMAX(GDS(R%, Health) / GDP(R%) * 100, sHlthExpPcntGDP(R%))
                Else
                    'if sought through scenario handles, allow going down
                    sHlthExpPcntGDP(R%) = GDS(R%, Health) / GDP(R%) * 100
                End If
                'mti February 2011 Infarstructure - end

            Next R%

            'kbn 2018/02/16 Start Drug model below
            Call ComputeDrugs(context)
            'kbn 2018/02/16 end


            '   All but First 1:  Power:  Conventional and Nuclear
            '   ------------------------------------------------

            For R% = 1 To NR%
                NPOW(R%) = SNPOW(R%) * (1 - drnpow) + GDS(R%, Mil) * nmilf(R%) * npowf
                'Note:  This is inconsistent with specification in preprocessor
                ' (.002 billion/warhead)
                'and results in sharp decline in arsenals; result right, but wrong reasons
                cpowf(R%) = cpowldcf - (cpowldcf - 1) * Amin(10, GDPPC(R%)) / 10 'as in preprocessor
                CPOW(R%) = SCPOW(R%) * (1 - drcpow) + GDS(R%, Mil) * (1 - nmilf(R%)) * cpowf(R%)
                NPOW(R%) = AMAX(0, Amin(NPOW(R%), 100000))
                CPOW(R%) = AMAX(0, Amin(CPOW(R%), 100000))
                'bbh 2011/08/18
                GOVNUCWP(R%) = GOVNUCWP(R%) + govnucwpadd(R%)
                GOVNUCWP(R%) = AMAX(0, Amin(GOVNUCWP(R%), 900000))
                'bbh 2011/08/18 end
                'jrs 2011/10/11
                SGOVNUCWP(R%) = GOVNUCWP(R%)
            Next R%
            '
            '   All but First 1:  Power:  Save for Next Cycle and Compute Global Totals
            '   ---------------------------------------------------------------------

            WNPOW = 0
            WCPOW = 0
            For R% = 1 To NR%
                SNPOW(R%) = NPOW(R%)
                SCPOW(R%) = CPOW(R%)
                WNPOW = WNPOW + NPOW(R%)
                WCPOW = WCPOW + CPOW(R%)
            Next R%
            SWNPOW = WNPOW
            SWCPOW = WCPOW
            '
            '   All but First 1:  Probability of Conventional War
            '   -----------------------------------------------

            For act% = 1 To NR%
                For tar% = 1 To NR%
                    ThreatTerm = AMAX(THREAT(act%, tar%), THREAT(tar%, act%)) * wpthrwar
                    CWARPB(act%, tar%) = cwarbase(act%, tar%) + ThreatTerm
                    CWARPB(act%, tar%) = Amin(100, CWARPB(act%, tar%))

                Next tar%
            Next act%
            '
            '   All but First 1:  Determine Whether Conventional and/or Nuclear War and Length
            '   ----------------------------------------------------------------------------

            For act% = 1 To NR%
                For tar% = 1 To NR%
                    CWAR(act%, tar%) = 0
                    NWAR(act%, tar%) = 0
                Next tar%
            Next act%

            For act% = 1 To NR%
                For tar% = 1 To NR%
                    '            If CWARPB(act%, tar%) > 0 Then Stop
                    'If RegName$(act%) = "United States of America" And RegName$(tar%) = "Mexico" Then Stop

                    Cond2 = ((cwarf >= 100) Or
                (CWARPB(act%, tar%) > Rnd * 100 And nowarper(act%, tar%) < 1)) _
                 And (act% <> tar%)
                    If waron < 0.1 Then Cond2 = False
                    If LenWar(act%, tar%) > 0 Then   'Ongoing War
                        CWAR(act%, tar%) = 1
                        CWAR(tar%, act%) = 1
                    ElseIf Cond2 Then
                        CWAR(act%, tar%) = 1     'New War
                        CWAR(tar%, act%) = 1
                        LenWar(act%, tar%) = Convert.ToInt32(Rnd * 6) + 1
                        LenWar(tar%, act%) = LenWar(act%, tar%)


                    End If
                    If LenWar(act%, tar%) > 0 Then
                        If LenWar(act%, tar%) = 1 Then
                            nowarper(act%, tar%) = Convert.ToInt32(Rnd * 12) + 1   'Set Period Without War
                            nowarper(tar%, act%) = nowarper(act%, tar%)
                        End If
                        LenWar(act%, tar%) = LenWar(act%, tar%) - 1  'Gradually End War
                        LenWar(tar%, act%) = LenWar(act%, tar%)
                    End If
                    If nowarper(act%, tar%) > 0 Then     'Phase Out NoWar Period
                        nowarper(act%, tar%) = nowarper(act%, tar%) - 1
                        nowarper(tar%, act%) = nowarper(act%, tar%)
                    End If
                    NWARPB(act%, tar%) = CWAR(act%, tar%) * cwarsv * nwarf * 100  'Nuclear War Prob
                    If NPOW(act%) < 1 And NPOW(tar%) < 1 Then NWARPB(act%, tar%) = 0
                    If (nwarf >= 100) Or (NWARPB(act%, tar%) > Rnd * 100) Then
                        NWAR(act%, tar%) = 1  'Nuclear War
                        NWAR(tar%, act%) = 1
                        nowarper(act%, tar%) = 10  'No Other War for 10 Years
                        nowarper(tar%, act%) = 10
                        LenWar(act%, tar%) = 0  'War Length Set to One Year
                        LenWar(tar%, act%) = 0
                    End If
                Next tar%
            Next act%
            '
            '   All but First 1:  Spread War to Allies of Combatants
            '   --------------------------------------------------

            For act% = 1 To NR%
                For tar% = 1 To NR%
                    If CWAR(act%, tar%) = 1 Then
                        For Part% = 1 To NR% 'spread war with target to actor's allies
                            If act% <> Part% And ally(act%, Part%) = 1 Then
                                CWAR(Part%, tar%) = 1
                                CWAR(tar%, Part%) = 1
                                LenWar(Part%, tar%) = LenWar(act%, tar%)
                                LenWar(tar%, Part%) = LenWar(act%, tar%)
                            End If
                        Next Part%
                        For Part% = 1 To NR% 'spread war with actor to target's allies
                            If tar% <> Part% And ally(Part%, tar%) = 1 Then
                                CWAR(Part%, act%) = 1
                                ' CWAR(act%, par%) = 1
                                CWAR(act%, Part%) = 1

                                LenWar(Part%, act%) = LenWar(act%, tar%)
                                LenWar(act%, Part%) = LenWar(act%, tar%)
                            End If
                        Next Part%
                    End If
                Next tar%
            Next act%


            'jrs 2016/08/02
            'All but First 1: Human Traffic
            '----------------------------------
            For R = 1 To NR
                'Human Traffic Prevalence
                'Call AnalFunc("Human Traffic Prevalence (2015)", Array(POP(R), GOVEFFECT(R)), Est, Array("POP", "GOVEFFECT"))

                Call AnalFunc("Human Traffic Prevalence (2015)",
                  New Single() {POP(R), GOVEFFECT(R)}, Est,
                  New String() {"POP", "GOVEFFECT"}, context)

                HTCTPI(R) = Est + ConvergeOverTime(HTCTPIShift(R), 0, 100)
                HTCTPI(R) = Amin(AMAX(HTCTPI(R), 0), 100)
                'Government Response to Trafficking
                'Call AnalFunc("Government response to trafficking (2015)", Array(POP(R), DEMOCPOLITY(R), SEdSecEnrGFem(R)), Est, Array("POP", "POLITYCOMBINED", "EDSECENROLLGROSSFEMALE"))

                Call AnalFunc("Government response to trafficking (2015)",
                  New Single() {POP(R), DEMOCPOLITY(R), SEdSecEnrGFem(R)}, Est,
                  New String() {"POP", "POLITYCOMBINED", "EDSECENROLLGROSSFEMALE"}, context)

                HTCGRI(R) = Est + ConvergeOverTime(HTCGRIShift(R), 0, 100)
                HTCGRI(R) = Amin(AMAX(HTCGRI(R), 0), 100)
            Next R

            ' GoTo AllSteps
            IfsIntlPolAllSteps(context)


        End Sub

        Public Sub IfsIntlPol2AllButFirstYear(context As IForecastContext)
            ReDim InfrabudallocNew(NInfraType%), InfrabudallocMnt(NInfraType%)
            'ttt 2019/10/08  TakeOut
            Array.Copy(EDBUDDEM, SEdTotCost, NR + 1)
            'For R% = 1 To NR%
            '    'mti December 15, 2008 'EDBUDDEM(R%) calculates unconstrained budget demand in IFsEduc
            '    SEdTotCost(R%) = EDBUDDEM(R%) 'EDTOTCOST(R%)
            'Next R%


        End Sub
        Public Sub IfsIntlPolAllSteps(context As IForecastContext)
            '
            '   All Years:  War Damage
            '   ----------------------
            Dim ConvDAM As Single
            Dim NuclDam As Single


            For act% = 1 To NR% 'Note:  this is not spreading war to allies
                CIVDM(act%) = 0
                CPOWDM(act%) = 0
                NPOWDM(act%) = 0
                For tar% = 1 To NR%
                    If CWAR(act%, tar%) > 0.5 Then 'Conventional war with target, add damage
                        ConvDAM = cwarsv * CPOW(tar%)
                        CIVDM(act%) = CIVDM(act%) + ConvDAM / AMAX(0.01, GDP(act%)) * cdmf
                        CPOWDM(act%) = CPOWDM(act%) + ConvDAM * cpowdf
                    End If
                    If NWAR(act%, tar%) > 0.5 Then 'Nuclear war with target, add damage
                        NuclDam = CSng(1.0! - 0.5 ^ ((NPOW(tar%) * nwarsv / 1000) ^ elndpow))
                        CIVDM(act%) = CIVDM(act%) + NuclDam
                        CPOWDM(act%) = CPOWDM(act%) + NuclDam
                        NPOWDM(act%) = nwarsv * NPOW(act%)
                    End If
                Next tar%
                CIVDM(act%) = CSng(Amin(0.8, CIVDM(act%)))
                SCPOW(act%) = CSng(SCPOW(act%) * (1 - Amin(0.8, CPOWDM(act%))))
                SNPOW(act%) = SNPOW(act%) - NPOWDM(act%)
            Next act%

            '   All Years:  Global Dispute Threat
            '   ---------------------------------
            WTHREAT = 0
            For act% = 1 To NR%
                For tar% = 1 To NR%
                    WTHREAT = WTHREAT + THREAT(act%, tar%)
                Next tar%
            Next act%
            'TempNr = NR%
            'Tempdiv = (TempNr * (TempNr - 1))
            'TempCalc = WTHREAT / Tempdiv
            'WTHREAT = TempCalc
            'WTHREAT = WTHREAT / (NR% * (NR% - 1))
            WTHREAT = CSng(WTHREAT / (CDbl(NR%) * CDbl(NR% - 1)))

            'jrs 2013/02/19 *** begin ***
            'jrs 2012/11/05

            'All Years: DIPLOMETRICS FORECAST
            '==================================

            'dkb 2014/08/01 - the following commented code has been moved above because DIPLOEMBASSYMAX, DIPLOIGOMAX, _
            'DIPLOTREATYMAX are used in WDPI - START
            '    Dim EmbassyEstimate As Single, EmbassyMaxEstimate As Single
            '    Dim IGOEstimate As Single, IGOMaxEstimate As Single
            '    Dim TreatyEstimate As Single, TreatyMaxEstimate As Single

            Dim EmbassyEstimate As Single
            Dim IGOEstimate As Single
            Dim TreatyEstimate As Single

            'jrs 2016/01/18
            Dim INGOProgramEstimate As Single
            Dim INGOAdvocacyEstimate As Single

            'Estimation
            'jrs 2013/01/21
            '    EmbassyMaxEstimate = 1.1125 * Log(IY - 1 + FirstYear) - 0.8026
            '    IGOMaxEstimate = 0.001 * (IY - 1 + FirstYear) ^ 2 + 0.047 * (IY - 1 + FirstYear) - 0.3923
            '    TreatyMaxEstimate = 0.001 * (IY - 1 + FirstYear) ^ 2 + 0.0073 * (IY - 1 + FirstYear) - 0.5559
            'jrs 2013/02/19
            '    EmbassyMaxEstimate = 0.012 * (IY - 1 + FirstYear) - 20.286
            '    IGOMaxEstimate = 0.045 * (IY - 1 + FirstYear) - 341.91
            '    TreatyMaxEstimate = 0.0505 * (IY - 1 + FirstYear) + 3.1859
            '    EmbassyMaxEstimate = 0.012 * (IY - 1 + FirstYear) - 20.663
            '    IGOMaxEstimate = 0.1019 * (IY - 1 + FirstYear) - 398.46
            '    TreatyMaxEstimate = 0.0505 * (IY - 1 + FirstYear) + 3.1859

            'dkb 2013/08/14 Updated forecast equations with those found with JDM
            'dkb 2013/08/29 New forecast equations look worse than earlier - TreatyMax is obviously off. - START
            '    EmbassyMaxEstimate = 1.1125 * Log(IY - 1 + FirstYear) - 0.8026
            '    IGOMaxEstimate = 0.1864 * (IY - 1 + FirstYear) - 367.42
            '    TreatyMaxEstimate = 0.0005 * (IY - 1 + FirstYear) ^ 2 + -1.7645 * (IY - 1 + FirstYear) + 1646.3
            'dkb 2013/08/29 - END

            '    If IY = 1 Then
            '        'Find shift factor
            '        DIPLOEMBASSYMAXShift = DIPLOEMBASSYMAX - EmbassyMaxEstimate
            '        DIPLOIGOMAXShift = DIPLOIGOMAX - IGOMaxEstimate
            '        DIPLOTREATYMAXShift = DIPLOTREATYMAX - TreatyMaxEstimate
            '    End If
            '    DIPLOEMBASSYMAX = EmbassyMaxEstimate + DIPLOEMBASSYMAXShift
            '    DIPLOIGOMAX = IGOMaxEstimate + DIPLOIGOMAXShift
            '    DIPLOTREATYMAX = TreatyMaxEstimate + DIPLOTREATYMAXShift
            '    'Apply multipliers
            '    DIPLOEMBASSYMAX = DIPLOEMBASSYMAX * diploembassymaxm
            '    DIPLOIGOMAX = DIPLOIGOMAX * diploigomaxm
            '    DIPLOTREATYMAX = DIPLOTREATYMAX * diplotreatymaxm
            'dkb 2014/08/01 - END

            'jrs 2013/01/02 moved down this loop to use the max calculated above
            For R = 1 To NR
                'Estimation
                'jrs 2013/02/19
                '        EmbassyEstimate = -11.36 + (0.1306 * Log(M(R) / GDP(R) + X(R) / GDP(R))) + (0.479 * Log(POWER(R))) + (0.00636 * (IY - 1 + FirstYear))
                '        IGOEstimate = -96.31 + (0.0191 * DEMOCPOLITY(R)) + (0.1715 * Log(GDPPCP(R))) + (0.04835 * (IY - 1 + FirstYear))
                '        TreatyEstimate = -97.06 - 0.000045 * diplodistancetoeu(R) + 0.038 * DEMOCPOLITY(R) + 0.0488 * (IY - 1 + FirstYear)
                'dkb 2013/09/30 Changed POWER(R) to POWERHH(R) to avoid multicolinearity in EmbassyEstimate - START
                'dkb 2014/07/07 Changed POWERHH(R) to POWERHHMI(R) - START
                'dkb 2014/10/23 Changed POWERHHMI(R) to POWER(R) so that small logged POWERHHMI did not make DIPLOEMBASSY negative - START
                'clp 2016/08/23 - Updated equations per Mickey
                'EmbassyEstimate = -26.2711 + (0.12909 * Log(M(R) / GDP(R) + X(R) / GDP(R))) + (0.468811 * Log(POWER(R))) + (0.0138 * (IY - 1 + FirstYear))
                'IGOEstimate = -96.31 + (0.0191 * DEMOCPOLITY(R)) + (0.1715 * Log(GDPPCP(R))) + (0.04835 * (IY - 1 + FirstYear))
                'TreatyEstimate = -97.06 - 0.000045 * diplodistancetoeu(R) + 0.038 * DEMOCPOLITY(R) + 0.0488 * (IY - 1 + FirstYear)

                'mtr 2016/10 update to EmbassyEstimate using the new unstandardized DV for embassies
                'EmbassyEstimate = -0.388141 + (0.0079443 * Log(M(R) / GDP(R) + X(R) / GDP(R))) + (0.0784585 * Log(POWER(R))) + (0.00041 * (IY - 1 + FirstYear))
                'jrs 2021/12/05 added switch
                If wppowhhsw = 0 Then
                    EmbassyEstimate = CSng(-784.1326443 + (3.0130231 * Log(M(R) / GDP(R) + X(R) / GDP(R))) + (14.0734501 * Log(POWER(R))) + (0.4320105 * (IY - 1 + FirstYear)))
                Else
                    EmbassyEstimate = CSng(-784.1326443 + (3.0130231 * Log(M(R) / GDP(R) + X(R) / GDP(R))) + (14.0734501 * Log(POWERHH(R))) + (0.4320105 * (IY - 1 + FirstYear)))
                End If
                'mtr 2016/10 updating model of IGOEstimate to include only data from 2000 to capture prevailing trend in membership
                'IGOEstimate = -8472.629416 + (2.84705 * DEMOCPOLITY(R)) + (23.168069 * Log(GDPPCP(R))) + (4.290947 * (IY - 1 + FirstYear))
                IGOEstimate = CSng(-2522.218911 + (4.51878 * DEMOCPOLITY(R)) + (32.892498 * Log(GDPPCP(R))) + (1.273021 * (IY - 1 + FirstYear)))

                TreatyEstimate = CSng(-48033.84 - 0.0208028 * diplodistancetoeu(R) + 22.61359 * DEMOCPOLITY(R) + 24.50642 * (IY - 1 + FirstYear))
                'If R = 128 Then Stop
                'dkb 2014/07/07 - END
                'dkb 2013/09/30 - END
                'dkb 2014/10/23 - END
                If IY = 1 Then
                    'Find shift factors
                    'jrs 2013/01/25 added Hong Kong, Puerto Rico and Plaestine to stay constant
                    'clp 2016/08/23 Changed -4's to match new initalization of 0 - START
                    'jrs 2026/08/03 run to run If DIPLOEMBASSY(R) > 0 (Gambia) And Not GRPS(1, R) = "Hong Kong" And Not GRPS(1, R) = "Puerto Rico" And Not GRPS(1, R) = "Palestine" Then
                    If DIPLOEMBASSY(R) > 0.0001 And Not GRPS(1, R) = "Hong Kong" And Not GRPS(1, R) = "Puerto Rico" And Not GRPS(1, R) = "Palestine" Then
                        DIPLOEMBASSYShift(R) = DIPLOEMBASSY(R) - EmbassyEstimate
                    Else
                        DIPLOEMBASSYShift(R) = -99999
                    End If
                    DIPLOIGOShift(R) = DIPLOIGO(R) - IGOEstimate
                    If DIPLOTREATY(R) > 0 Then
                        DIPLOTREATYShift(R) = DIPLOTREATY(R) - TreatyEstimate
                    Else
                        DIPLOTREATYShift(R) = -99999
                    End If
                End If
                'clp 2016/08/23 - END
                'If R = 128 Then Stop
                If DIPLOEMBASSYShift(R) > -99999 Then
                    DIPLOEMBASSY(R) = EmbassyEstimate + DIPLOEMBASSYShift(R)
                End If

                'jrs 2021/01/07
                If IY > 1 Then
                    DIPLOEMBASSY(R) = DIPLOEMBASSY(R) * diploembassym(R)
                End If

                'jrs 2013/01/02
                DIPLOEMBASSY(R) = Amin(DIPLOEMBASSY(R), DIPLOEMBASSYMAX)
                'jrs 2023/12/05
                DIPLOEMBASSY(R) = CSng(AMAX(DIPLOEMBASSY(R), 0.0001))

                'jrs 2023/03/16
                If IY > 1 Then
                    DIPLOIGO(R) = IGOEstimate + DIPLOIGOShift(R)
                End If

                'jrs 2013/01/02
                DIPLOIGO(R) = Amin(DIPLOIGO(R), DIPLOIGOMAX)
                If DIPLOTREATYShift(R) > -99999 Then
                    DIPLOTREATY(R) = TreatyEstimate + DIPLOTREATYShift(R)
                    'jrs 2013/01/02
                    DIPLOTREATY(R) = Amin(DIPLOTREATY(R), DIPLOTREATYMAX)
                End If

                'jrs 2022/04/20
                SDIPLOEMBASSY2(R%) = SDIPLOEMBASSY(R%)

                'dkb 2014/08/06 - adding save variables for Diplometrics POWER component - START
                SDIPLOEMBASSY(R%) = DIPLOEMBASSY(R%)
                SDIPLOIGO(R%) = DIPLOIGO(R%)
                SDIPLOTREATY(R%) = DIPLOTREATY(R%)
                'dkb 2014/08/06 - END

                'jrs 2016/01/18 *** begin ***
                If IY = 1 Then
                    'Call AnalFunc("INGO Programmatic (2012)", _
                    '   Array(INFRAROADPAVEDPCNT(R%), GOVCORRUPT(R%), DIPLOEMBASSY(R%), DEMOCPOLITY(R%), SFINTLWARMAG(R%), Log(POP(R%)), _
                    '  UKcol(R%), FRAcol(R%), GERcol(R%), PORcol(R%), SPAcol(R%), NETcol(R%), RUScol(R%)), INGOProgramEstimate, _
                    ' Array("PAVEDROADSPCT", "GOVCORRUPT", "DIPLOEMBASSY", "DEMOCPOLITY", "SFINTLWARMAG", "LNPOP", _
                    '"UKCOL", "FRACOL", "GERCOL", "PORCOL", "SPACOL", "NETCOL", "RUSCOL"))

                    '    Call AnalFunc("INGO Programmatic (2017)",
                    'Array(INFRAROADPAVEDPCNT(R%), GOVCORRUPT(R%), DIPLOEMBASSY(R%), DEMOCPOLITY(R%), POP(R%),
                    'UKcol(R%), FRAcol(R%), GERcol(R%), PORcol(R%), SPAcol(R%), NETcol(R%), RUScol(R%)), INGOProgramEstimate,
                    'Array("PAVEDROADSPCT", "GOVCORRUPT", "DIPLOEMBASSY", "DEMOCPOLITY", "LNPOP",
                    '"UKCOL", "FRACOL", "GERCOL", "PORCOL", "SPACOL", "NETCOL", "RUSCOL"))

                    Call AnalFunc("INGO Programmatic (2017)",
                       New Single() {INFRAROADPAVEDPCNT(R%), GOVCORRUPT(R%), DIPLOEMBASSY(R%), DEMOCPOLITY(R%), POP(R%),
                                    UKcol(R%), FRAcol(R%), GERcol(R%), PORcol(R%), SPAcol(R%), NETcol(R%), RUScol(R%)}, INGOProgramEstimate,
                       New String() {"PAVEDROADSPCT", "GOVCORRUPT", "DIPLOEMBASSY", "DEMOCPOLITY", "LNPOP",
                                    "UKCOL", "FRACOL", "GERCOL", "PORCOL", "SPACOL", "NETCOL", "RUSCOL"}, context)

                    DIPLOINGOPROGRAMShift(R%) = DIPLOINGOPROGRAM(R%) - INGOProgramEstimate

                    'Call AnalFunc("INGO Advocacy (2012)", _
                    '   Array(DIPLOEMBASSY(R%), (X(R%) + M(R%)) / GDP(R%), DEMOCPOLITY(R%), GOVREGQUAL(R%), Log(POP(R%)), _
                    '  UKcol(R%), FRAcol(R%), GERcol(R%), PORcol(R%), SPAcol(R%), NETcol(R%), RUScol(R%)), INGOAdvocacyEstimate, _
                    ' Array("DIPLOEMBASSY", "XMPERGDP", "DEMOCPOLITY", "GOVREGQUAL", "LNPOP", _
                    '"UKCOL", "FRACOL", "GERCOL", "PORCOL", "SPACOL", "NETCOL", "RUSCOL"))

                    '    Call AnalFunc("INGO Advocacy (2017)",
                    'Array(DIPLOEMBASSY(R%), (X(R%) + M(R%)) / GDP(R%), DEMOCPOLITY(R%), GOVREGQUAL(R%), POP(R%),
                    'UKcol(R%), GERcol(R%), SPAcol(R%), NETcol(R%), RUScol(R%)), INGOAdvocacyEstimate,
                    'Array("DIPLOEMBASSY", "XMPERGDP", "DEMOCPOLITY", "GOVREGQUAL", "LNPOP",
                    '"UKCOL", "GERCOL", "SPACOL", "NETCOL", "RUSCOL"))


                    Call AnalFunc("INGO Advocacy (2017)",
                         New Single() {DIPLOEMBASSY(R%), (X(R%) + M(R%)) / GDP(R%), DEMOCPOLITY(R%), GOVREGQUAL(R%), POP(R%),
                                        UKcol(R%), GERcol(R%), SPAcol(R%), NETcol(R%), RUScol(R%)}, INGOAdvocacyEstimate,
                         New String() {"DIPLOEMBASSY", "XMPERGDP", "DEMOCPOLITY", "GOVREGQUAL", "LNPOP",
                                        "UKCOL", "GERCOL", "SPACOL", "NETCOL", "RUSCOL"}, context)

                    DIPLOINGOADVOCACYShift(R%) = DIPLOINGOADVOCACY(R%) - INGOAdvocacyEstimate
                Else
                    'Call AnalFunc("INGO Programmatic (2012)", _
                    '   Array(INFRAROADPAVEDPCNT(R%), GOVCORRUPT(R%), DIPLOEMBASSY(R%), DEMOCPOLITY(R%), SSFINTLWARMAG(R%), Log(POP(R%)), _
                    '  UKcol(R%), FRAcol(R%), GERcol(R%), PORcol(R%), SPAcol(R%), NETcol(R%), RUScol(R%)), INGOProgramEstimate, _
                    ' Array("PAVEDROADSPCT", "GOVCORRUPT", "DIPLOEMBASSY", "DEMOCPOLITY", "SFINTLWARMAG", "LNPOP", _
                    '"UKCOL", "FRACOL", "GERCOL", "PORCOL", "SPACOL", "NETCOL", "RUSCOL"))

                    '    Call AnalFunc("INGO Programmatic (2017)",
                    'Array(INFRAROADPAVEDPCNT(R%), GOVCORRUPT(R%), DIPLOEMBASSY(R%), DEMOCPOLITY(R%), POP(R%),
                    'UKcol(R%), FRAcol(R%), GERcol(R%), PORcol(R%), SPAcol(R%), NETcol(R%), RUScol(R%)), INGOProgramEstimate,
                    'Array("PAVEDROADSPCT", "GOVCORRUPT", "DIPLOEMBASSY", "DEMOCPOLITY", "LNPOP",
                    '"UKCOL", "FRACOL", "GERCOL", "PORCOL", "SPACOL", "NETCOL", "RUSCOL"))

                    Call AnalFunc("INGO Programmatic (2017)",
                        New Single() {INFRAROADPAVEDPCNT(R%), GOVCORRUPT(R%), DIPLOEMBASSY(R%), DEMOCPOLITY(R%), POP(R%),
                                UKcol(R%), FRAcol(R%), GERcol(R%), PORcol(R%), SPAcol(R%), NETcol(R%), RUScol(R%)}, INGOProgramEstimate,
                        New String() {"PAVEDROADSPCT", "GOVCORRUPT", "DIPLOEMBASSY", "DEMOCPOLITY", "LNPOP",
                                "UKCOL", "FRACOL", "GERCOL", "PORCOL", "SPACOL", "NETCOL", "RUSCOL"}, context)

                    DIPLOINGOPROGRAM(R%) = INGOProgramEstimate + ConvergeOverTime(DIPLOINGOPROGRAMShift(R%), 0, diploingoprogramconv)
                    DIPLOINGOPROGRAM(R%) = DIPLOINGOPROGRAM(R%) * diploingoprogramm(R%)
                    DIPLOINGOPROGRAM(R%) = AMAX(Amin(DIPLOINGOPROGRAM(R%), 200), 0)

                    'Call AnalFunc("INGO Advocacy (2012)", _
                    '   Array(DIPLOEMBASSY(R%), (X(R%) + M(R%)) / GDP(R%), DEMOCPOLITY(R%), GOVREGQUAL(R%), Log(POP(R%)), _
                    '  UKcol(R%), FRAcol(R%), GERcol(R%), PORcol(R%), SPAcol(R%), NETcol(R%), RUScol(R%)), INGOAdvocacyEstimate, _
                    ' Array("DIPLOEMBASSY", "XMPERGDP", "DEMOCPOLITY", "GOVREGQUAL", "LNPOP", _
                    '"UKCOL", "FRACOL", "GERCOL", "PORCOL", "SPACOL", "NETCOL", "RUSCOL"))

                    '    Call AnalFunc("INGO Advocacy (2017)",
                    'Array(DIPLOEMBASSY(R%), (X(R%) + M(R%)) / GDP(R%), DEMOCPOLITY(R%), GOVREGQUAL(R%), POP(R%),
                    'UKcol(R%), GERcol(R%), SPAcol(R%), NETcol(R%), RUScol(R%)), INGOAdvocacyEstimate,
                    'Array("DIPLOEMBASSY", "XMPERGDP", "DEMOCPOLITY", "GOVREGQUAL", "LNPOP",
                    '"UKCOL", "GERCOL", "SPACOL", "NETCOL", "RUSCOL"))

                    Call AnalFunc("INGO Advocacy (2017)",
                        New Single() {DIPLOEMBASSY(R%), (X(R%) + M(R%)) / GDP(R%), DEMOCPOLITY(R%), GOVREGQUAL(R%), POP(R%),
                                UKcol(R%), GERcol(R%), SPAcol(R%), NETcol(R%), RUScol(R%)}, INGOAdvocacyEstimate,
                        New String() {"DIPLOEMBASSY", "XMPERGDP", "DEMOCPOLITY", "GOVREGQUAL", "LNPOP",
                                "UKCOL", "GERCOL", "SPACOL", "NETCOL", "RUSCOL"}, context)

                    DIPLOINGOADVOCACY(R%) = INGOAdvocacyEstimate + ConvergeOverTime(DIPLOINGOADVOCACYShift(R%), 0, diploingoadvocacyconv)
                    DIPLOINGOADVOCACY(R%) = DIPLOINGOADVOCACY(R%) * diploingoadvocacym(R%)
                    DIPLOINGOADVOCACY(R%) = AMAX(Amin(DIPLOINGOADVOCACY(R%), 200), 0)
                End If
                'jrs 2016/01/18 *** end ***
                'mtr 2017/01/17 *** end ***
            Next R
            'jrs 2013/02/19 *** end ***
        End Sub

        Public Sub IFsRiskIndexAllYears(context As IForecastContext)
            'Number of Variables inlcuded in Index
            Dim iVarNum As Integer
            iVarNum = 22 'jrs 2012/01/09
            'jrs 2011/04/13
            Dim WeightTot As Single
            WeightTot = 0
            'Settings for each variable
            If IY = 1 Then
                ReDim GOVRISKMAX(iVarNum), GOVRISKMIN(iVarNum), GOVRISKNAME(iVarNum), GOVRISKSIGN(iVarNum)
                ReDim swatres(NR%) 'jrs 2019/10/22
                Array.Copy(watres, swatres, NR% + 1) 'jrs 2019/10/22

                'JRS 2018/02/26
                ReDim SGOVRISK(NR)
                'Setting Min and Max
                '-------------------
                Dim currRow As Integer
                '1. Corruption
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 10
                GOVRISKNAME(currRow) = "GOVCORRUPT"
                GOVRISKSIGN(currRow) = -1
                '2. Effectiveness
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 5
                GOVRISKNAME(currRow) = "GOVEFFECT"
                GOVRISKSIGN(currRow) = -1
                '3. Democracy
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 20
                GOVRISKNAME(currRow) = "DEMOCPOLITY"
                GOVRISKSIGN(currRow) = -1
                '4. Freedom
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 2
                GOVRISKMAX(currRow) = 14
                GOVRISKNAME(currRow) = "FREEDOM"
                GOVRISKSIGN(currRow) = -1
                'Security
                '--------
                '5. Instability
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 1
                GOVRISKNAME(currRow) = "SFINSTABALL"
                GOVRISKSIGN(currRow) = 1
                '6. Internal War
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 1
                GOVRISKNAME(currRow) = "SFINTLWARALL"
                GOVRISKSIGN(currRow) = 1
                'Economy
                '--------
                '7. Poverty
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 1
                'jrs 2016/04/29 GOVRISKNAME(currRow) = "INCOMELT125LN2005/POP"
                'jrs 2022/11/21
                If ConstantDollarsYear = 2011 Then
                    GOVRISKNAME(currRow) = "INCOMELT190LN/POP"
                ElseIf ConstantDollarsYear = 2017 Then 'jrs 2025/09/12
                    GOVRISKNAME(currRow) = "INCOMELT215LN/POP"
                Else 'jrs 2025/09/12
                    GOVRISKNAME(currRow) = "POVEXTREME/POP"
                End If
                GOVRISKSIGN(currRow) = 1
                '8. Inequality
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 1
                GOVRISKNAME(currRow) = "GINIDOM"
                GOVRISKSIGN(currRow) = 1
                '9. Export Dependance
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 1.5
                GOVRISKNAME(currRow) = "XSres/GDP"
                GOVRISKSIGN(currRow) = 1
                '10. Per Capita Growth
                currRow = currRow + 1
                GOVRISKMIN(currRow) = -100
                GOVRISKMAX(currRow) = 100
                GOVRISKNAME(currRow) = "GDPR-POPR"
                GOVRISKSIGN(currRow) = -1
                'Health
                '-------
                '11. Infant Mortality
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 500
                GOVRISKNAME(currRow) = "INFMOR"
                GOVRISKSIGN(currRow) = 1
                '12. Life Expectancy
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 125
                GOVRISKNAME(currRow) = "LIFEXP"
                GOVRISKSIGN(currRow) = -1
                '13. Malnutrition
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 100
                GOVRISKNAME(currRow) = "MALNCHP"
                GOVRISKSIGN(currRow) = 1
                '14. HIV Prevalence Rate
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 100
                GOVRISKNAME(currRow) = "HIVRATE"
                GOVRISKSIGN(currRow) = 1
                'Education
                '---------
                '15. Primary Net Enrollment
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 100
                GOVRISKNAME(currRow) = "EDPRIENRN"
                GOVRISKSIGN(currRow) = -1
                '16. Adult Education years
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 20
                GOVRISKNAME(currRow) = "EDYRSAG15"
                GOVRISKSIGN(currRow) = -1
                'Population
                '----------
                '17. Youth bulge
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 1
                GOVRISKNAME(currRow) = "YTHBULGE"
                GOVRISKSIGN(currRow) = 1
                '18. Elderly bulge
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 1
                'jrs 2013/04/15 GOVRISKNAME(currRow) = "POPGT65"
                GOVRISKNAME(currRow) = "POPRETIRED"
                GOVRISKSIGN(currRow) = 1
                '19. Urbanization Rage
                currRow = currRow + 1
                GOVRISKMIN(currRow) = -10
                GOVRISKMAX(currRow) = 10
                GOVRISKNAME(currRow) = "POPURBGR"
                GOVRISKSIGN(currRow) = 1
                'Environment
                '-----------
                '20. Water Use
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 90
                GOVRISKNAME(currRow) = "WATUSE/WATRES"
                GOVRISKSIGN(currRow) = 1
                '21. Climate Change
                currRow = currRow + 1
                GOVRISKMIN(currRow) = -30
                GOVRISKMAX(currRow) = 30
                GOVRISKNAME(currRow) = "ENVYLCHG"
                GOVRISKSIGN(currRow) = -1
                'jrs 2012/01/09
                '22. Gender Empowerment
                currRow = currRow + 1
                GOVRISKMIN(currRow) = 0
                GOVRISKMAX(currRow) = 1
                GOVRISKNAME(currRow) = "GEM"
                GOVRISKSIGN(currRow) = -1
            Else 'jrs 2019/10/22
                Array.Copy(swatres, watres, NR% + 1) 'jrs 2019/10/22
            End If

            Dim R As Integer, i As Integer, VarVal As Single
            For R = 1 To NR
                GOVRISK(R) = 0
                For i = 1 To iVarNum
                    Select Case GOVRISKNAME(i)
                        Case "GOVCORRUPT"
                            VarVal = GOVCORRUPT(R)
                        Case "GOVEFFECT"
                            VarVal = GOVEFFECT(R)
                        Case "DEMOCPOLITY"
                            VarVal = DEMOCPOLITY(R)
                        Case "FREEDOM"
                            VarVal = FREEDOM(R)
                        Case "SFINSTABALL"
                            VarVal = SFINSTABALL(R)
                        Case "SFINTLWARALL"
                            'jrs 2024/05/20
                            If sfcivilwarucdpsw = 0 Or sfcivilwarucdpsw = 2 Then
                                VarVal = SFINTLWARALL(R)
                            ElseIf sfcivilwarucdpsw = 1 Then
                                VarVal = SFCIVILWARUCDP(R)
                            ElseIf sfcivilwarucdpsw = 3 Then 'dsb 2026/07/21
                                VarVal = SFCIVILWARPARDEE(R)
                            End If
                'jrs 2016/04/29
                'Case "INCOMELT125LN2005/POP"
                '    VarVal = INCOMELT125LN2005(R) / POP(R)
                        Case "INCOMELT190LN/POP"
                            VarVal = INCOMELT190LN(R) / POP(R)
                        Case "INCOMELT215LN/POP"
                            VarVal = INCOMELT215LN(R) / POP(R)
                        Case "GINIDOM"
                            VarVal = GINIDOM(R)
                        Case "XSres/GDP"
                            VarVal = (XS(R, 1) + XS(R, 2) + XS(R, 3)) / GDP(R)
                        Case "GDPR-POPR"
                            VarVal = GDPR(R) - POPR(R)
                        Case "INFMOR"
                            VarVal = INFMOR(R)
                        Case "LIFEXP"
                            VarVal = LIFEXP(R, 3)
                        Case "MALNCHP"
                            VarVal = MALNCHP(R)
                        Case "HIVRATE"
                            VarVal = HIVRATE(R)
                        Case "EDPRIENRN"
                            VarVal = EDPRIENRN(R, 3)
                        Case "EDYRSAG15"
                            VarVal = EDYRSAG15(R, 3)
                        Case "YTHBULGE"
                            VarVal = YTHBULGE(R)
                        Case "POPRETIRED" 'JRS 2013/04/15 "POPGT65"
                            'VarVal = POPGT65(R) / POP(R)
                            VarVal = POPRETIRED(R) / POP(R)
                        Case "POPURBGR"
                            VarVal = POPURBGR(R)
                        Case "WATUSE/WATRES"
                            VarVal = WATUSE(R) / watres(R)
                        Case "ENVYLCHG"
                            VarVal = ENVYLCHG(R)
                        Case "GEM" 'jrs 2012/01/09
                            VarVal = GEM(R)
                    End Select
                    If GOVRISKSIGN(i) > 0 Then
                        VarVal = (VarVal - GOVRISKMIN(i)) / (GOVRISKMAX(i) - GOVRISKMIN(i))
                    Else
                        VarVal = (VarVal - GOVRISKMAX(i)) / (GOVRISKMIN(i) - GOVRISKMAX(i))
                    End If
                    GOVRISK(R) = GOVRISK(R) + VarVal * govriskweight(i) 'jrs 2011/04/13 added weight
                    'jrs 2011/04/13
                    If R = 1 Then
                        WeightTot = WeightTot + govriskweight(i)
                    End If
                Next i
                If WeightTot <> 0 Then
                    GOVRISK(R) = GOVRISK(R) / WeightTot 'jrs 2011/04/13 iVarNum
                    'JRS 2018/02/26
                    SGOVRISK(R) = GOVRISK(R)
                End If
                If IY% > 1 Then GOVRISK(R) = GOVRISK(R) * govriskm(R) 'amb 2014/01/03 added multiplier on govrisk
            Next R
        End Sub

        Public Sub IFsGovIndexAllYears(context As IForecastContext)
            'dkb 2018/06/01 - START - adding SOCIETALVIOLENCEDEATHS to GOVINDSECUR
            'kbn 2018/06/01-Start- Insert svindexmax and parameters for scaling terms
            Dim R As Integer

            ReDim SHOMICIDEINDEX(NR%)
            ReDim SGOVINDSECUR(NR)

            Dim conflictterm As Single
            Dim govriskterm As Single
            Dim violenceterm As Single
            Dim scaleterm As Single
            Dim govrevterm As Single


            If IY% = 1 Then



                svindexmax = 1
                For R = 1 To NR
                    svindexmax = AMAX(svindexmax, HOMICIDEINDEX(R%))
                Next R

            End If

            'ttt 2019/10/08  TakeOut
            Array.Copy(HOMICIDEINDEX, SHOMICIDEINDEX, NR + 1)

            For R = 1 To NR
                'kbn 2018/06/12-Start Save value for societal violence index here
                'ttt 2019/10/08 SHOMICIDEINDEX(R%) = HOMICIDEINDEX(R%)
                'jrs 2020/01/27
                'govsecrskexp = 1.0#
                'govsecrskdiv = 0.5

                'kbn 2018/06/12 Change terms below
                'jrs 2024/05/20
                If sfcivilwarucdpsw = 0 Or sfcivilwarucdpsw = 2 Then
                    conflictterm = govindsecurconflict * Amin(1, AMAX(0, SFINTLWARALL(R)))
                ElseIf sfcivilwarucdpsw = 1 Then
                    conflictterm = govindsecurconflict * Amin(1, AMAX(0, SFCIVILWARUCDP(R)))
                ElseIf sfcivilwarucdpsw = 3 Then 'dsb 2026/07/21
                    conflictterm = govindsecurconflict * Amin(1, AMAX(0, SFCIVILWARPARDEE(R)))
                End If
                govriskterm = CSng(govindsecurgovrisk * Amin(1, AMAX(0, (GOVRISK(R) - 0.15) * 2)))
                'violenceterm = 0.5 * SOCIETALVIOLENCEINDEX(R) / Amin(svindexmax, 1)
                violenceterm = CSng(govindsecurviolence * Amin(1, AMAX(0, SHOMICIDEINDEX(R) / svindexmax)))             'dkb: replace 200 with svindexmax
                scaleterm = govindsecurconflict + govindsecurgovrisk + govindsecurviolence
                GOVINDSECUR(R) = AMAX(0, Amin(1, 1 - (conflictterm + govriskterm + violenceterm) / scaleterm))
                'kbn 2018/06/12-End-
                'kbn 2018/06/01-End-
                'mti 2018/06/18 Start
                GOVINDSECUR(R%) = AMAX(0, Amin(1, GOVINDSECUR(R%) * govindsecurm(R%)))
                'mti 2018/06/18 End

                SGOVINDSECUR(R) = GOVINDSECUR(R)
                'dkb 2018/06/01 - END
                '        GOVINDSECUR(R) = AMAX(0, Amin(1, 1 - (SFINTLWARALL(R) + 0.1) ^ 0.5 * (GOVRISK(R) / AMAX(0.1, govsecrskdiv)) ^ govsecrskexp))
                '        GOVINDCAPAC(R) = Amin((GOVREV(R) - AID(R)) / GDP(R), 0.4) / 0.4 * GOVCORRUPT(R) / 10
                '        GOVINDINCLUS(R) = DEMOCPOLITY(R) / 20 * Amin(1#, GEM(R))


                'GOVINDSECUR(R) = AMAX(0, Amin(1, 1 - (SFINTLWARALL(R) + (GOVRISK(R) - 0.15) * 2) / 2))
                govrevterm = CSng(Amin((GOVREV(R) - AID(R)) / GDP(R), 0.45) / 0.45)
                GOVINDCAPAC(R) = AMAX(0, Amin(1, (govrevterm + GOVCORRUPT(R) / 10) / 2))
                'If RegName(R%) = "Qatar" And IY% >= 19 Then Stop
                GOVINDINCLUS(R) = CSng(AMAX(0, Amin(1, (DEMOCPOLITY(R) / 20 + Amin(1.0#, GEM(R))) / 2)))

                GOVINDTOTAL(R) = (GOVINDSECUR(R) + GOVINDCAPAC(R) + GOVINDINCLUS(R)) / 3
                'kbn 2018/06/01 END
                'dkb 2018/06/02-Start - Calling this procedure in order to obtain SMIGFORCEDEST after calculation of GOVINDSECUR
                If IY% = 1 Then
                    Call ForcedMigEstimate()
                    'dkb 2018/06/02-End
                End If

            Next R

        End Sub

        'jrs 2025/08/28
        Private Sub LoadAvgDistance(context As IForecastContext)

            Dim CAvgDis As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)

            If context.HistoricMode Then
                For Each item In (From s In context.GetSlice("SeriesDiploGeoDistAvgCEPII", "Earliest")
                                  Select Key = s.Key, Value = If(s.Value, 0))
                    CAvgDis.Add(item.Key, item.Value)
                Next
            Else
                For Each item In (From s In context.GetSlice("SeriesDiploGeoDistAvgCEPII", BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = If(s.Value, 0))
                    CAvgDis.Add(item.Key, item.Value)
                Next
            End If


            For Each item In (From s In CAvgDis
                              Join r In context.Regionalization On s.Key Equals r.Key
                              Group Value = s.Value By RegionId = r.Value Into Data = Group, AvgVal = Average(Value)
      ).ToDictionary(
       Function(grp) grp.RegionId,
       Function(grp) grp.AvgVal
      )
                AvgDistanceAtoB(item.Key) = CSng(item.Value)
            Next

        End Sub

        'Public Sub IFsIntlConflictFirstYear(context As IForecastContext)
        '    If context.HistoricMode = True Or wpindsw = 0 Then
        '        Exit Sub
        '    End If

        '    'jrs 2011/12/08
        '    'sErrorSub = "IFsIntlConflict"

        '    Dim R1 As Integer, R2 As Integer, Democracy As Single, Trade As Single, IO As Single
        '    Using db As IFsContext = New IFsContext()

        '        If IY = 1 Then
        '            'Load Region codes for countries
        '            '        If IFsCtrys.Count = 0 Then 'jrs 2011/12/08 added condition in case running the scenario several times
        '            '            For R1 = 1 To CCount
        '            '                IFsCtrys.Add CounReg(R1), CountryName$(R1)
        '            'Next R1
        '            '        End If

        '            ReDim WPINDKANT(NR, NR), WPINDREAL(NR, NR), COI(NR, NR, 2)
        '            ReDim SharedRiv(NR, NR)
        '            'Read Shared River data
        '            '        Dim db As Database, sql As String, rs As Recordset
        '            '        sql = "SELECT CT1.Country, CT2.Country" &
        '            '" FROM [Country Translation] AS CT1, [Country Translation] AS CT2, SharedRiversCountry AS SR" &
        '            '" WHERE CT1.[COW Numbers] = SR.CountryACowNumber" &
        '            '" AND CT2.[COW Numbers] = SR.CountryBCowNumber"

        '            For Each item In (From SR In db.SharedRiversCountry
        '                              Join CT1 In db.CountryTranslation On CT1.COWNumbers Equals SR.CountryACowNumber
        '                              Join CT2 In db.CountryTranslation On CT2.COWNumbers Equals SR.CountryBCowNumber
        '                              Select Country1 = CT1.Country, Country2 = CT2.Country)

        '                'R1 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(0)), False)
        '                'R2 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(1)), False)
        '                'R1 = IFsCtrys(rs.Fields(0))
        '                'R2 = IFsCtrys(rs.Fields(1))
        '                R1 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country1)).Value
        '                R2 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country2)).Value

        '                If SharedRiv(R1, R2) = False Then
        '                    SharedRiv(R1, R2) = True
        '                    SharedRiv(R2, R1) = True
        '                End If
        '            Next

        '            'Initialize COI
        '            'jrs 2011/11/09
        '            '        sql = "SELECT CT1.Country, CT2.Country, CI.StandCOI " & _
        '            '        " FROM GovConflict as CI, [Country Translation] AS CT1, [Country Translation] AS CT2" & _
        '            '        " WHERE int(CT1.[COW Numbers]) = CI.COW1" & _
        '            '        " AND int(CT2.[COW Numbers]) = CI.COW2" & _
        '            '        " AND CI.Year = " & CStr(BaseYear - 9) 'jrs 2011/10/12 Jonathan wants to use 2001

        '            Dim MaxCOIYear = (From CI In db.GovConflict
        '                              Where CI.StandCOI.HasValue
        '                              Group Value = CI.Year By CI.COW1, CI.COW2 Into Data = Group, MaxVal = Max(Value))


        '            For Each item In (From CI In db.GovConflict
        '                              Join CT1 In db.CountryTranslation On CT1.COWNumbers Equals CI.COW1.ToString
        '                              Join CT2 In db.CountryTranslation On CT2.COWNumbers Equals CI.COW2.ToString
        '                              Join MaxCOI In MaxCOIYear On MaxCOI.COW1 Equals CI.COW1 And MaxCOI.COW2 Equals CI.COW2 And MaxCOI.MaxVal Equals CI.Year
        '                              Select Country1 = CT1.Country, Country2 = CT2.Country, StandCOI = CI.StandCOI, Year = CI.Year)

        '                'R1 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(0)), False)
        '                'R2 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(1)), False)
        '                'R1 = IFsCtrys(rs.Fields(0))
        '                'R2 = IFsCtrys(rs.Fields(1))
        '                R1 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country1)).Value
        '                R2 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country2)).Value

        '                'Value
        '                COI(R1, R2, 1) = CSng(item.StandCOI)
        '                COI(R2, R1, 1) = CSng(item.StandCOI)
        '                'Year
        '                COI(R1, R2, 2) = item.Year
        '                COI(R2, R1, 2) = item.Year

        '            Next

        '            'jrs 2011/11/10
        '            ReDim COI2(NR, NR, 2)





        '            '        Sql = "SELECT CT1.Country, CT2.Country, CI.StandCOI, CI.Year" &
        '            '" FROM [Country Translation] AS CT1, [Country Translation] AS CT2, GovConflict as CI, " &
        '            '" (SELECT CI2.COW1, CI2.COW2, MAX(CI2.Year) as MaxYrMinus10 " &
        '            '" FROM (SELECT CI.COW1, CI.COW2, CI.Year " &
        '            '" FROM GovConflict as CI, " &
        '            '" (SELECT GovConflict.COW1, GovConflict.COW2, Max(GovConflict.Year) AS MaxOfYear " &
        '            '" From GovConflict " &
        '            '" Where ((Not (GovConflict.[StandCOI]) Is Null)) " &
        '            '" GROUP BY GovConflict.COW1, GovConflict.COW2) as MaxCOIYear " &
        '            '" Where MaxCOIYear.MaxOfYear > [CI].[Year] + 9 " &
        '            '" AND MaxCOIYear.COW2=[CI].[COW2] " &
        '            '" AND MaxCOIYear.COW1=[CI].[COW1]) AS CI2 " &
        '            '" GROUP BY CI2.COW1, CI2.COW2) as MaxCOIYearM10 " &
        '            '" Where Int([CT1].[COW Numbers]) = [CI].[COW1] " &
        '            '" AND Int([CT2].[COW Numbers])=[CI].[COW2] " &
        '            '" AND MaxCOIYearM10.MaxYrMinus10=[CI].[Year] " &
        '            '" AND MaxCOIYearM10.COW2=[CI].[COW2] " &
        '            '" AND MaxCOIYearM10.COW1=[CI].[COW1]"


        '            'Set rs = dbIFs.OpenRecordset(sql)
        '            'Do While Not rs.EOF
        '            '                'R1 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(0)), False)
        '            '                'R2 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(1)), False)
        '            '                R1 = IFsCtrys(rs.Fields(0))
        '            '                R2 = IFsCtrys(rs.Fields(1))

        '            '                'Value
        '            '                COI2(R1, R2, 1) = rs.Fields(2)
        '            '                COI2(R2, R1, 1) = rs.Fields(2)
        '            '                'Year
        '            '                COI2(R1, R2, 2) = rs.Fields(3)
        '            '                COI2(R2, R1, 2) = rs.Fields(3)
        '            '                rs.MoveNext
        '            '            Loop
        '            '            rs.Close



        '            Dim MaxCOIYearSQL = (From CI In db.GovConflict
        '                                 Where CI.StandCOI.HasValue
        '                                 Group Value = CI.Year By CI.COW1, CI.COW2 Into Data = Group, MaxVal2 = Max(Value))

        '            Dim MaxCOIYearM10SQL = (From CI2 In db.GovConflict
        '                                    Join MaxCOI In MaxCOIYearSQL On MaxCOI.COW1 Equals CI2.COW1 And MaxCOI.COW2 Equals CI2.COW2 And MaxCOI.MaxVal2 Equals CI2.Year + 9 'Review
        '                                    Group Value = CI2.Year By CI2.COW1, CI2.COW2 Into Data = Group, MaxYrMinus10 = Max(Value))


        '            For Each item In (From CI In db.GovConflict
        '                              Join CT1 In db.CountryTranslation On CT1.COWNumbers Equals CI.COW1.ToString
        '                              Join CT2 In db.CountryTranslation On CT2.COWNumbers Equals CI.COW2.ToString
        '                              Join MaxCOIYearM10 In MaxCOIYearM10SQL On MaxCOIYearM10.COW1 Equals CI.COW1 And MaxCOIYearM10.COW2 Equals CI.COW2 And MaxCOIYearM10.MaxYrMinus10 Equals CI.Year
        '                              Select Country1 = CT1.Country, Country2 = CT2.Country, StandCOI = CI.StandCOI, Year = CI.Year)

        '                R1 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country1)).Value
        '                R2 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country2)).Value

        '                'Value
        '                COI2(R1, R2, 1) = CSng(item.StandCOI)
        '                COI2(R2, R1, 1) = CSng(item.StandCOI)
        '                'Year
        '                COI2(R1, R2, 2) = item.Year
        '                COI2(R2, R1, 2) = item.Year



        '            Next

        '            Dim MinCOIYearSQL = (From CI2 In db.GovConflict
        '                                 Join MaxCOI In MaxCOIYearSQL On MaxCOI.COW1 Equals CI2.COW1 And MaxCOI.COW2 Equals CI2.COW2 And MaxCOI.MaxVal2 Equals CI2.Year 'Review
        '                                 Group Value = CI2.Year By CI2.COW1, CI2.COW2 Into Data = Group, MinYr = Max(Value))


        '            For Each item In (From CI In db.GovConflict
        '                              Join CT1 In db.CountryTranslation On CT1.COWNumbers Equals CI.COW1.ToString
        '                              Join CT2 In db.CountryTranslation On CT2.COWNumbers Equals CI.COW2.ToString
        '                              Join MinCOIYear In MinCOIYearSQL On MinCOIYear.COW1 Equals CI.COW1 And MinCOIYear.COW2 Equals CI.COW2 And MinCOIYear.MinYr Equals CI.Year
        '                              Select Country1 = CT1.Country, Country2 = CT2.Country, StandCOI = CI.StandCOI, Year = CI.Year)


        '                R1 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country1)).Value
        '                R2 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country2)).Value

        '                If COI2(R1, R2, 1) = 0 Then
        '                    'Value
        '                    COI2(R1, R2, 1) = CSng(item.StandCOI)
        '                    COI2(R2, R1, 1) = CSng(item.StandCOI)
        '                    'Year
        '                    COI2(R1, R2, 2) = item.Year
        '                    COI2(R2, R1, 2) = item.Year
        '                End If


        '            Next


        '            ''        Sql = "SELECT CT1.Country, CT2.Country, CI.StandCOI, CI.Year" &
        '            ''" FROM [Country Translation] AS CT1, [Country Translation] AS CT2, GovConflict as CI, " &
        '            ''" (SELECT CI2.COW1, CI2.COW2, MIN(CI2.Year) as MinYr " &
        '            ''" FROM (SELECT CI.COW1, CI.COW2, CI.Year " &
        '            ''" FROM GovConflict as CI, " &
        '            ''" (SELECT GovConflict.COW1, GovConflict.COW2, Max(GovConflict.Year) AS MaxOfYear " &
        '            ''" From GovConflict " &
        '            ''" Where ((Not (GovConflict.[StandCOI]) Is Null)) " &
        '            ''" GROUP BY GovConflict.COW1, GovConflict.COW2) as MaxCOIYear " &
        '            ''" Where MaxCOIYear.MaxOfYear > [CI].[Year] " &
        '            ''" AND MaxCOIYear.COW2=[CI].[COW2] " &
        '            ''" AND MaxCOIYear.COW1=[CI].[COW1]) AS CI2 " &
        '            ''" GROUP BY CI2.COW1, CI2.COW2) as MinCOIYear " &
        '            ''" Where Int([CT1].[COW Numbers]) = [CI].[COW1] " &
        '            ''" AND Int([CT2].[COW Numbers])=[CI].[COW2] " &
        '            ''" AND MinCOIYear.MinYr=[CI].[Year] " &
        '            ''" AND MinCOIYear.COW2=[CI].[COW2] " &
        '            ''" AND MinCOIYear.COW1=[CI].[COW1]"

        '            'Set rs = dbIFs.OpenRecordset(sql)
        '            'Do While Not rs.EOF
        '            '                'R1 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(0)), False)
        '            '                'R2 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(1)), False)
        '            '                R1 = IFsCtrys(rs.Fields(0))
        '            '                R2 = IFsCtrys(rs.Fields(1))

        '            '                If COI2(R1, R2, 1) = 0 Then
        '            '                    'Value
        '            '                    COI2(R1, R2, 1) = rs.Fields(2)
        '            '                    COI2(R2, R1, 1) = rs.Fields(2)
        '            '                    'Year
        '            '                    COI2(R1, R2, 2) = rs.Fields(3)
        '            '                    COI2(R2, R1, 2) = rs.Fields(3)
        '            '                End If
        '            '                rs.MoveNext
        '            '            Loop
        '            '            rs.Close

        '            'jrs 2012/06/15 db.Close

        '            'jrs 2011/11/21
        '            '        Set dbD = OpenDatabase(PutFdrvRun$ & "ifsDyadic.mdb")
        '            '        dbD.Execute ("DELETE * FROM WPINDKANTREG")
        '            '        dbD.Execute ("DELETE * FROM WPINDREALREG")
        '            '        dbD.Execute ("DELETE * FROM COIREG")
        '            '        Set tblK = dbD.OpenRecordset("WPINDKANTREG")
        '            '        Set tblR = dbD.OpenRecordset("WPINDREALREG")
        '            '        Set tblC = dbD.OpenRecordset("COIREG")

        '            'jrs 2011/12/22
        '            'Finding Maximum Government Embededness
        '            MaxGovEmb1 = 0
        '            MaxGovEmb2 = 0
        '            For R1 = 1 To NR
        '                If GOVEMBED(R1) > MaxGovEmb1 Then
        '                    MaxGovEmb2 = MaxGovEmb1
        '                    MaxGovEmb1 = GOVEMBED(R1)
        '                ElseIf GOVEMBED(R1) > MaxGovEmb2 Then
        '                    MaxGovEmb2 = GOVEMBED(R1)
        '                End If
        '            Next R1

        '            'Finding Maximum Risk Index
        '            For R1 = 1 To NR
        '                If GOVRISK(R1) > MaxRiskIdx Then
        '                    MaxRiskIdx = GOVRISK(R1)
        '                End If
        '            Next R1

        '        End If

        '        'Total Energy Production
        '        Dim WENPT As Single
        '        Dim i As Integer
        '        WENPT = 0
        '        For i = 1 To 6
        '            WENPT = WENPT + WENP(i)
        '        Next i

        '        'Liberal Index (Kant)
        '        '--------------------
        '        For R1 = 1 To NR - 1
        '            For R2 = R1 + 1 To NR
        '                '1. Democracy
        '                Democracy = (DEMOCPOLITY(R1) + DEMOCPOLITY(R2)) / 40
        '                '2. International Organization Membership
        '                IO = (GOVEMBED(R1) + GOVEMBED(R2)) / (MaxGovEmb1 + MaxGovEmb2)
        '                IO = Amin(IO, 1)
        '                '3. Trade
        '                Trade = ((M(R1) + X(R1)) / GDP(R1) + (M(R2) + X(R2)) / GDP(R2)) / 2
        '                'jrs 2011/11/10 Trade = AMAX(Amin(Trade, 1), 0)
        '                'Liberal Index
        '                WPINDKANT(R1, R2) = (Democracy + IO + Trade) / 3
        '            Next R2
        '        Next R1

        '        'jrs 2011/11/10
        '        If IY = 1 Then
        '            'Find the mean of Liberal Index
        '            For R1 = 1 To NR - 1
        '                For R2 = R1 + 1 To NR
        '                    MeanLiberal = MeanLiberal + WPINDKANT(R1, R2)
        '                Next R2
        '            Next R1

        '            MeanLiberal = CSng(MeanLiberal / (NR * CLng(NR - 1) / 2))
        '            'Find the standard deviation
        '            For R1 = 1 To NR - 1
        '                For R2 = R1 + 1 To NR
        '                    StdDevLiberal = CSng(StdDevLiberal + (WPINDKANT(R1, R2) - MeanLiberal) ^ 2)
        '                Next R2
        '            Next R1
        '            StdDevLiberal = CSng((StdDevLiberal / (NR * CLng(NR - 1) / 2)) ^ (1 / 2))
        '        End If

        '        'Standarize the score
        '        For R1 = 1 To NR - 1
        '            For R2 = R1 + 1 To NR
        '                WPINDKANT(R1, R2) = (WPINDKANT(R1, R2) - MeanLiberal) / StdDevLiberal
        '            Next R2
        '        Next R1

        '        'Realist Index
        '        '-------------
        '        Dim MatPow As Single, Fragility As Single, WATER As Single, ENERGY As Single
        '        For R1 = 1 To NR - 1
        '            For R2 = R1 + 1 To NR
        '                'if not contiguous and both non powers
        '                If contiguity(R1, R2) > 5 And POWER(R1) < wpgreatthresh And POWER(R2) < wpgreatthresh Then
        '                    MatPow = 0
        '                    Fragility = 0
        '                    WATER = 0
        '                    ENERGY = 0
        '                Else
        '                    '1. Material Power
        '                    If POWER(R1) > POWER(R2) Then
        '                        MatPow = (POWER(R1) + POWER(R2)) / (POWER(R1) * 2)
        '                    Else
        '                        MatPow = (POWER(R1) + POWER(R2)) / (POWER(R2) * 2)
        '                    End If
        '                    '2. Fragility
        '                    If GOVRISK(R1) > GOVRISK(R2) Then
        '                        Fragility = GOVRISK(R1) / MaxRiskIdx
        '                    Else
        '                        Fragility = GOVRISK(R2) / MaxRiskIdx
        '                    End If
        '                    '3. Water
        '                    'If contiguity(r1, r2) < 6 Then
        '                    If SharedRiv(R1, R2) Then
        '                        WATER = (WATUSE(R1) / watres(R1) + WATUSE(R2) / watres(R2)) / 2
        '                        If WATER > 1 Then
        '                            WATER = 1
        '                        ElseIf WATER < 0.4 Then
        '                            WATER = 0
        '                        Else
        '                            WATER = CSng((WATER - 0.4) / 0.6)
        '                        End If
        '                    Else
        '                        WATER = 0
        '                    End If
        '                    '4. Energy
        '                    ENERGY = (WENP(Oil) / WENPT) * 80 * (RESER(R1, Oil) + RESER(R2, Oil)) / WRESER(Oil)
        '                    ENERGY = ENERGY + (WENP(Gas) / WENPT) * 80 * (RESER(R1, Gas) + RESER(R2, Gas)) / WRESER(Gas)
        '                    ENERGY = ENERGY / 2
        '                    ENERGY = Amin(AMAX(ENERGY, 0), 1)
        '                End If
        '                'Realist Index
        '                WPINDREAL(R1, R2) = -1 * (MatPow + Fragility + WATER + ENERGY) / 4
        '            Next R2
        '        Next R1

        '        'jrs 2011/11/10
        '        If IY = 1 Then
        '            'Find the mean of Realist Index
        '            For R1 = 1 To NR - 1
        '                For R2 = R1 + 1 To NR
        '                    MeanReal = MeanReal + WPINDREAL(R1, R2)
        '                Next R2
        '            Next R1

        '            MeanReal = CSng(MeanReal / (NR * CLng(NR - 1) / 2))
        '            'Find the standard deviation
        '            For R1 = 1 To NR - 1
        '                For R2 = R1 + 1 To NR
        '                    StdDevReal = CSng(StdDevReal + (WPINDREAL(R1, R2) - MeanReal) ^ 2)
        '                Next R2
        '            Next R1
        '            StdDevReal = CSng((StdDevReal / (NR * CLng(NR - 1) / 2)) ^ (1 / 2))
        '        End If

        '        'Standarize the score
        '        For R1 = 1 To NR - 1
        '            For R2 = R1 + 1 To NR
        '                WPINDREAL(R1, R2) = (WPINDREAL(R1, R2) - MeanReal) / StdDevReal
        '            Next R2
        '        Next R1

        '        Dim COIPrediction As Single
        '        'Character if Bilateral State Interaction (proxy for War)
        '        For R1 = 1 To NR - 1
        '            For R2 = R1 + 1 To NR
        '                'COI Forecast uses constant growth until 2050 then stays flat
        '                If COI2(R1, R2, 1) = 0 Or COI(R1, R2, 2) = COI2(R1, R2, 2) Or IY = 1 Or Abs(COI(R1, R2, 1)) > 3 Then
        '                    COIPrediction = COI(R1, R2, 1)
        '                ElseIf COI(R1, R2, 1) > 0 Then
        '                    COIPrediction = CSng((COI(R1, R2, 1) + 3) * ((COI(R1, R2, 1) + 3) / (COI2(R1, R2, 1) + 3)) ^ ((Amin(IY, 41) - 1) / (COI(R1, R2, 2) - COI2(R1, R2, 2))))
        '                    COIPrediction = COIPrediction - 3
        '                    'jrs 2011/12/16
        '                    If Abs(COIPrediction) > 3 Then
        '                        COIPrediction = Sign(COIPrediction) * 3
        '                    End If
        '                End If
        '                THREAT(R1, R2) = (WPINDKANT(R1, R2) + WPINDREAL(R1, R2)) + COIPrediction
        '                THREAT(R2, R1) = THREAT(R1, R2)
        '            Next R2
        '        Next R1

        '        'Save results to table
        '        If wpindsw > 1 Then
        '            For R1 = 1 To NR - 1
        '                For R2 = R1 + 1 To NR
        '                    'jrs 2011/11/21 saving to Threat variable
        '                    '                tblK.AddNew
        '                    '                tblK.Fields("RegionIdA") = R1
        '                    '                tblK.Fields("RegionIdB") = R2
        '                    '                tblK.Fields("Year") = FirstYear + IY - 1
        '                    '                tblK.Fields("Forecast") = WPINDKANT(R1, R2)
        '                    '                tblK.Update
        '                    '
        '                    '                tblR.AddNew
        '                    '                tblR.Fields("RegionIdA") = R1
        '                    '                tblR.Fields("RegionIdB") = R2
        '                    '                tblR.Fields("Year") = FirstYear + IY - 1
        '                    '                tblR.Fields("Forecast") = WPINDREAL(R1, R2)
        '                    '                tblR.Update
        '                    '
        '                    Select Case wpindsw
        '                        Case 2
        '                            THREAT(R1, R2) = WPINDKANT(R1, R2)
        '                        Case 3
        '                            THREAT(R1, R2) = WPINDREAL(R1, R2)
        '                        Case 4
        '                            'COI Forecast uses constant growth until 2050 then stays flat
        '                            If COI2(R1, R2, 1) = 0 Or COI(R1, R2, 2) = COI2(R1, R2, 2) Or IY = 1 Or Abs(COI(R1, R2, 1)) > 3 Then
        '                                COIPrediction = COI(R1, R2, 1)
        '                            Else
        '                                COIPrediction = CSng((COI(R1, R2, 1) + 3) * ((COI(R1, R2, 1) + 3) / (COI2(R1, R2, 1) + 3)) ^ ((Amin(IY, 41) - 1) / (COI(R1, R2, 2) - COI2(R1, R2, 2))))
        '                                COIPrediction = COIPrediction - 3
        '                                'jrs 2011/12/16
        '                                If Abs(COIPrediction) > 3 Then
        '                                    COIPrediction = Sign(COIPrediction) * 3
        '                                End If
        '                            End If
        '                            THREAT(R1, R2) = COIPrediction
        '                    End Select
        '                    THREAT(R2, R1) = THREAT(R1, R2)
        '                    '
        '                    '                tblC.AddNew
        '                    '                tblC.Fields("RegionIdA") = R1
        '                    '                tblC.Fields("RegionIdB") = R2
        '                    '                tblC.Fields("Year") = FirstYear + IY - 1
        '                    '                tblC.Fields("Forecast") = COIPrediction
        '                    '                tblC.Update

        '                Next R2
        '            Next R1
        '        End If


        '    End Using
        '    'jrs 2011/11/21
        '    '    If FirstYear + IY - 1 = LastYearForecast Then
        '    '        tblK.Close
        '    '        tblR.Close
        '    '        tblC.Close
        '    '        dbD.Close
        '    '    End If


        'End Sub


    End Module

End Namespace
