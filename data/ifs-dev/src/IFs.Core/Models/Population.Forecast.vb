Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.IO
Imports System.Linq
Imports IFs.Data
Imports IFs.Data.ProducerScope
Imports System.Math
Imports Microsoft.EntityFrameworkCore
Imports System.Threading.Tasks
Imports System.Threading.Tasks.Dataflow

Namespace Models

    Partial Public Module Population

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

        '   Seek simplicity, but distrust it.  Alfred North Whitehead

        '
        '
        '   Developed by Barry B. Hughes 1988-2012
        '   Model code available for review and education only
        '   Reproduction or incorporation in other software by permission only
        '
        '
        '   Last changed:  Januasry 27, 2007
        'mti Jan 24, 2007

        'mti 2015/06/01 - start
        Public SLit() As Single
        'mti 2015/06/01 - end


        'jrs 2013/06/11 changed to public
        Public fagedst(,,) As Double
        'jrs 2009/10/28 changed to public
        Public agedst(,,) As Double



        'bbh August 29, 2009
        'bbh 2009/09/09
        'dsr 2016/05/04 Need SCBR and SCDR to be Public for using Exogenous series with annual changes
        'Dim SCBR(), SCDR(), SGDPinPOP(), SLIFEXPFin()
        Public SGDPinPOP(), SLIFEXPFin(,) As Single
        Public SCBR(), SCDR() As Single

        'jrs 2013/05/07
        Public SLIFEXP(,) As Single


        'jrs 2007/05/04
        'Used to normalize Factors for mortality detail
        Public modmordst(,,) As Double
        'jrs 2007/05/10
        Dim clmage() As Double
        Dim aidsdeathsbyage(,) As Single?
        Dim mordst(,,) As Single
        Dim CalIAdj() As Single
        Dim incaidsdths() As Single
        'jrs 2007/05/17
        Dim coname$()
        'jrs 2007/06/07 added second dimension to EDYRSAG25I
        Public EDYRSAG25I(,), EDYRSAG15I(,) As Single
        'jrs 2007/06/19
        Dim lifexptari(,) As Single
        Dim lifexpmaxi() As Single
        'jrs 2007/08/14
        Dim HIVRATEI() As Single
        Public aidsdeathsbyageHeavy(,,) As Single


        'jrs 2009/09/30 used to save deaths
        Public d(,,) As Double

        'mti Infrastructure August 2010 move watsan  - start
        'jrs 2009/09/30
        'Dim SPOPURBAN()
        'jrs 2009/10/05
        Public SPOPURBAN(), PopUrbanCalc(), POPURBANRMAX() As Single 'bbh 2012/08/13
        'mti Infrastructure August 2010 move watsan  - end

        'mti 2022/06/21 start
        Public sPOPRURAL() As Single
        'mti 2022/06/21 end

        'mti February 2011 Infarstructure -start
        'mti Holds values for two years back - required for extrapolative approach where module orders don't allow using same year value of these
        Public sPopUrban1() As Single
        Public sPopTotal1() As Single
        'mti February 2011 Infarstructure end

        'mti 2019/01/22 start
        Public sPOPR() As Single
        'mti 2019/01/22 End

        Public sinfmor() As Single
        Public laginfmor() As Single 'bbh 2014/01/11
        'jrs 2009/10/28
        Public ENSOLFUELShift() As Single 'jrs 2013/12/19 changed to public

        'mti Infrastructure August 2010 move watsan  - start
        ' 'jrs 2009/12/22
        ' Dim SSANITATION()
        ' 'jrs 2009/12/23
        ' Dim SWATSAFE()
        'mti Infrastructure August 2010 move watsan  - end
        Public SEDYRSAG25(,) As Single
        'dsr 2016/05/04 Add these for using annual changes with exogenous series
        Public stfr(), SEDYRSAG15v2(,) As Single
        'mti June 02, 2010 - added to avaoid run to run in averaging infraelecacc (rural and urban)
        'mti February 2011 Infarstructure -start
        'Public sPOPRURAL()
        'mti February 2011 Infarstructure -end
        Public sPOPTotal() As Single
        'jrs 2011/10/11
        Public SHDINEW() As Single
        'jrs 2013/05/28
        Public SavingsNeeded() As Single, SavingsNew() As Single, SavingsOld() As Single, WorkYearsNew As Single



        'jrs 2016/03/07
        Dim ISAMADMISSION() As Single

        'jrs 2016/12/01
        Public StuntingByAge(,,) As Double
        'jrs 2021/05/12
        Public fStuntingByAge(,,) As Double

        'jrs 2017/11/06
        Public smigrater() As Single
        Public smigrants() As Single
        Public SMigrateIn(), SMigrateOut(), SMigrantsIn(), SMigrantsOut() As Single 'bbh 2020/03/09
        'jrs 2017/11/24
        Dim XDYADMIG(,), SXDYADMIG(,) As Single
        'dsb 2025/12/17 
        'Dim SSFCIVILWARPOPIMP() As Single
        'dsb 2026/01/20
        Dim MIGRANTSOUTFORECAST(), SMIGRANTSOUTFORECAST(), SMIGRANTSOUTN() As Single
        'dsb 2026/01/30
        Dim MigrantsOutAverage() As Single
        'dsb 2026/02/16
        Dim MigrantsBilatAverage(,) As Single
        'dsb 2026/04/13
        Dim Migrantsnetabove1percent() As Single
        'jrs 2017/12/04
        Dim GRAVITYDYADMIG(,), SGRAVITYDYADMIG(,), GRAVITYMIG() As Single
        Dim DYADPOPFOREIGN(,), SDYADPOPFOREIGN(,) As Single
        'jrs 2020/06/19
        Dim bPOPFOREIGNUPDATE(,) As Boolean
        Public SDEATHS() As Single
        'jrs 2017/12/13
        Public migsharenativem(,), migdeathratesw(,), limit4returningmigs(,) As Single
        Public MIGIN(), MIGOUT() As Single
        'jrs 2017/12/22
        'jrs 2018/08/13 Dim XDYADMIGUNDOC(), DYADPOPFOREIGNUNDOC(), SDYADPOPFOREIGNUNDOC()
        'Public migundocsharenativem(), migundocdeathratesw(), limit4returningundocmigs()
        'jrs 2018/01/04
        'Public ratiomigdoctoundoc()
        'jrs 2018/01/16
        Public migrantsmissingm(,) As Single
        'jrs 2018/01/19
        Dim DYADMIGFORCED(,), SDYADMIGFORCED(,), DYADMIGFORCEDRETURN(,), SDYADMIGFORCEDRETURN(,), DYADPOPFOREIGNFORCED(,), SDYADPOPFOREIGNFORCED(,) As Single
        Dim SMIGFORCEDEST(), SMIGFORCEDRETURNEST() As Single
        Dim SharesForced(,), SSharesForced(,), GravityDyadForced(,), SGravityDyadForced(,), GravityForced() As Single
        Dim SharesFlowForced(,), SharesStockForced(,), SSharesFlowForced(,), SSharesStockForced(,) As Single
        'jrs 2018/01/22
        Dim DYADMIGVOL(,), SDYADMIGVOL(,), DYADPOPFOREIGNVOL(,), SDYADPOPFOREIGNVOL(,) As Single
        'jrs 2018/01/26
        Public dyadmigforcedreturnadd(,) As Single
        'jrs 2018/01/29
        Dim GRAVITYDYADMIGVAL(,) As Single
        'jrs 2018/01/30
        Dim SHIFTGRAVITYDYADMIG(,) As Single
        'jrs 2018/02/01
        'kbn 2018/03/28 Need people 15 to 65 in drug model
        'Dim sPOP15TO65()
        Public sPOP15TO65() As Single
        Dim AdjustFstYr(), AdjustBaseVal() As Single
        Dim SGRAVITYDYADMIGVAL(,) As Single
        'jrs 2018/02/23
        Dim DYADMIGFORRETLST0(,) As Single
        'jrs 2018/03/01
        Dim SPOPLE15() As Single
        'jrs 2018/03/21
        'Dim DYADMIGFORCEDTARGETPCTPOP(), DYADMIGVOLTARGETPCTPOP()
        Dim IDYADMIGFORCEDTARGETPCTPOP(,), IDYADMIGVOLTARGETPCTPOP(,) As Single
        Dim IDYADMIGFORCEDPCTPOP(,), IDYADMIGVOLPCTPOP(,) As Single
        'jrs 2018/04/03
        Dim SPopForeignIn(), SPopForeignOut() As Single 'bbh 2020/03/09
        Dim PopForInRatioI(), MigInflowMul(), MigOutFlowMul() As Single 'bbh 2022/06/23
        'jrs 2018/04/16
        Public dyadforcedreturnlimitm(,), VolTarget(,) As Single
        'jrs 2018/05/04
        Public dyadmigvoltargetsw(,) As Single
        'jrs 2018/06/26
        Public dyadforcedreturnsignal(,) As Single

        'kbn 2018/05/17
        Public SDEATHMntlHealth() As Single
        'kbn 2018/05/31
        Public SDEATHIntIj() As Single

        'jrs 2018/07/09
        Public dyadmigrantforcedm(,) As Single
        'jrs 2018/12/04
        Public Migratebyage(,) As Single?, birthchk(), deathchk(), sworkageretire() As Double

        Dim GDPPCPHDINewMinLog(), GDPPCPHDINewDenom() As Single
        Dim modFERDST(,) As Double, ferdst(,) As Double, basferdst(,) As Double
        Dim BasMordst(,,) As Single
        Dim clage() As Double
        Dim LITI() As Single
        Dim ultimatefertility() As Double
        Dim ultimatemort(,) As Single?
        Dim AdjMorDst(,,) As Single
        Dim fb() As Double
        Dim infmori() As Single
        Dim infmoradj() As Single
        Dim TFRI() As Single
        Dim tfrdst() As Double
        Dim TFRAdj() As Double
        Dim ConAdj() As Single
        Dim PopUrbGRI() As Single
        Dim sHHSIZE(), HHSizeShift(), HHSizeGR() As Single?
        Dim conusei(), IncAdj(), tfrminr() As Single
        Dim ContrExcessMin(), malnchpi(), CLPCi() As Single
        Dim malnchpsami(), samadmshift(), sSAMAVERTED() As Single
        Public PopCohorts(,) As Double, AdvCohorts(,) As Double
        Dim mortdstmg(), infmorg() As Single
        Dim popchk(), popchk2(), popchk3() As Double
        Dim SHIVRate(), maxhivrate(), AIDSDthRateI() As Single
        Dim GDPPCPI() As Single
        Dim SPopForeign() As Single
        Dim EDPRIPERI(,), sedyrsag15() As Single
        Dim CumSDeath(), HIVTECCNTLReg(), SHIVTECCNTLReg(), SHIVTECCNTLRegI(), SAIDSDTHSCM() As Single
        Dim POPRI() As Single, malnpoppi() As Single
        'jrs 2024/10/28
        Dim POPI() As Single
        Dim HIVRateL(,), SAIDSDRATE(), HIVAIDSRI(), HIVRateMAvg() As Single
        Dim HlthSpendRGDPI(), SmLifExpTarBase(,), POPURBShift() As Single
        Dim LifeCompI(), LifExpTarBas(), LifExpTarBas2(), LifExpTar(), LifTechAdv() As Single
        Dim LifNormMax(), LifMax(), LifExpMax(), LifExpTarMax() As Single
        Dim SLIFEXPRat(), WATSAFEI(), sCLPC() As Single
        Dim LifExpI(,), LIFEXPshift(,) As Single
        Dim deathsNS(,), MalnChilCompI() As Single
        Public LIFEXPNS(,), LIFEXPshiftNS(,) As Single
        Dim MalnChilShift(), SMalnChP(), smalnsamcomp(), WorkAgeRetireI() As Single
        Dim migrateRI(), EdIndHDINew(,), LifeExpIndHDINew(,) As Single
        'jrs 2026/08/12
        Dim migrateinRI(), migrateoutRI(), spopurbanm() As Single
        Dim smalnpopmul(), smalnmul() As Single
        Dim TFRGR() As Single?
        Public birthfshr As Single

        Dim SDYADMIGFORECEDRETURNEST() As Single
        Dim savingsnetcoh() As Single, SavingsNetCohMDC() As Single, SavingsNetCohLDC() As Single
        Dim savingsagei() As Double
        'ttt Made Public
        Public SavingsAgeAdj() As Single
        'ttt 2019/10/08
        Dim POPMaleG, POPFemaleG As Double()
        'jrs 2021/02/04
        Dim CLPCCVShift(), CLPCMDERShift() As Single
        'jrs 2021/05/13
        Public SMALNCHPSAM() As Single
        'jrs 2021/08/04
        Dim SCLPCCV() As Single
        'cdv 2022/03/07
        Dim AGEOFMARRIAGEShift(,) As Single
        Dim ageofmarriagecomp(2) As Single
        'mti 2022/10/24
        Dim sAGEOFMARRIAGE(,) As Single
        Dim AGEOFMARRIAGEGR(,) As Single

        Dim UnchainedLastYearUnpaidLabor(,) As Single
        Dim ChainedLastYearUnpaidLabor(,) As Single
        'jrs 2022/11/28
        Dim MARRIEDFEMSHRShift(,), sMARRIEDFEMSHR(,), MARRIEDFEMSHRGR(,) As Single

        'dsb 2026/1/15 Population lagged variable for SFCivilwarPopIMP calculation
        Public POP_Lag() As Single, POP_Lag1() As Single

        'cdv 2022/11/18
        Dim UnchainedLastYearWomenLeadership() As Single
        Dim ChainedLastYearWomenLeadership() As Single

        'cdv 2022/12/12
        Dim UnchainedLastYearFoodInsecurity(,) As Single
        Dim ChainedLastYearFoodInsecurity(,) As Single

        Dim Groupings() As ArrayList


        ' mockup forcast logic for POP
        <ForecastsVariable("POP", Scope:=FirstYearOnly)>
        Public Function ForecastPOPFirstYear(year As Short, context As IForecastContext) As IDataframe
            Dim df As IDataframe = context.Query("POP").Filter(BaseYearOnly).Execute()
            Return PostprocPOP(df, context)
        End Function

        ' mockup forcast logic for POP
        <ForecastsVariable("POP", Scope:=SecondYearOnward)>
        Public Function ForecastPOPSubsequentYears(year As Short, context As IForecastContext) As IDataframe
            Dim forPrevYear As IQueryFilter = context.GetDimension(0).EQ(year - 1S)
            Dim prev As IDataframe = context.Query("POP").Filter(forPrevYear).Execute()
            Dim df As Dataframe = Dataframe.Create("POP", context, year, Nothing)
            Dim growthRate As Single = 1.03F
            For Each iRegion As Short In prev.Dimensions(1).Buckets.Keys
                Dim base As Single? = prev(year - 1S, iRegion)
                If base.HasValue Then
                    df(year, iRegion) = base.Value * growthRate
                End If
            Next
            Return PostprocPOP(df, context)
        End Function

        ' post-processing (disaggregate to base case) for POP
        Public Function PostprocPOP(df As IDataframe, context As IForecastContext) As IDataframe
            Using db As IBasecase = BasecaseContext.Open(True, context, sIFsPopFile)
                db.Clear(GetType(PopBasecase))
                Dim cohortBuckets As IList(Of Short) = db.CohortBuckets.Select(Function(e) e.Seq).ToList()
                Dim genderBuckets As IList(Of Short) = db.GenderBuckets.Select(Function(e) e.Seq).ToList()
                Dim factor As Single = cohortBuckets.Count * genderBuckets.Count
                For Each row As IDataframeRecord In df
                    For Each iCohort As Short In cohortBuckets
                        'For Each iGender As Short In genderBuckets
                        db.Add(New PopBasecase() With {
                            .TimeId = row.Key(0),
                            .RegionId = row.Key(1),
                            .CohortId = iCohort,
                            .PopulationMales = row.Value / factor,
                            .PopulationFemales = row.Value / factor
                                   })
                        '.GenderId = iGender,
                        '.Value = row.Value / factor                            
                        '})
                        'Next
                    Next
                Next
                db.Flush()
            End Using
            Return df
        End Function

        ' mockup forcast logic for EDBASENRG
        <ForecastsVariable("EDBASENRG")>
        Public Function ForecastEDBASENRG(year As Short, context As IForecastContext) As IDataframe
            If year = context.FirstYear Then
                Return context.Query("POP").Filter(BaseYearOnly).Execute()
            Else
                Dim forPrevYear As IQueryFilter = context.GetDimension(0).EQ(year - 1S)
                Dim prev As IDataframe = context.Query("EDBASENRG").Filter(forPrevYear).Execute()
                Dim df As Dataframe = Dataframe.Create("EDBASENRG", context, year, Nothing)
                ' predict values for [baseYear + 1 thru endYear]
                Dim growthStep As Single = 0.5F
                For Each iRegion As Short In prev.Dimensions(1).Buckets.Keys
                    For Each iGender As Short In prev.Dimensions(2).Buckets.Keys
                        Dim base As Single? = prev(year - 1S, iRegion, iGender)
                        If base.HasValue Then
                            df(year, iRegion, iGender) = Math.Min(base.Value + growthStep, 100.0F)
                        Else
                            df(year, iRegion, iGender) = growthStep
                        End If
                    Next
                Next
                Return df
            End If
        End Function

        'jrs 2019/06/13
        Public Sub ifsPOP1FirstYear(context As IForecastContext)
            ReDim modmordst(NCohorts, NR%, 2), modFERDST(NCohorts, NR%)
            ReDim agedst(NCohorts, NR%, 2), mordst(NCohorts, NR%, 2), BasMordst(NCohorts, NR%, 2)
            ReDim ferdst(NCohorts, NR%), basferdst(NCohorts, NR%)
            ReDim fagedst(NAges, NR%, 2), d(NCohorts, NR%, 2)
            ReDim clmage(NCohorts), clage(NCohorts), LITI(NR%)
            ReDim Migratebyage(NCohorts, 2)
            ReDim ultimatefertility(NCohorts), ultimatemort(NCohorts, 2), AdjMorDst(NCohorts, NR%, 2)
            ReDim coname$(NCohorts), fb(NR%), infmori(NR%), infmoradj(NR%), sinfmor(NR%) 'bbh 2009/09/12
            ReDim laginfmor(NR%) 'bbh 2014/01/11
            ReDim TFRI(NR%), tfrdst(NR%)
            ReDim TFRAdj(NR%), ConAdj(NR%)
            ReDim PopUrbGRI(NR%), SPOPURBAN(NR%), POPURBANRMAX(NR%) 'bbh 2012/03/26
            ReDim PopUrbanCalc(NR%) 'bbh 2012/08/13
            'mti 2022/06/21 start
            ReDim sPOPRURAL(NR%)
            'mti 2022/06/21 end

            ReDim conusei(NR%), IncAdj(NR%), tfrminr(NR%)
            ReDim ContrExcessMin(NR%), malnchpi(NR%), CLPCi(NR%)
            'jrs 2015/11/27 SAM
            ReDim malnchpsami(NR%), samadmshift(NR%), sSAMAVERTED(NR%)

            ReDim PopCohorts(NR%, WVSCohorts), AdvCohorts(NR%, WVSCohorts - 1)
            ReDim mortdstmg(2), infmorg(2), sPOP15TO65(NR%) ', SPOP15to50(NR%)
            ReDim deathchk(NR%), birthchk(NR%), popchk(NR%), popchk2(NR%), popchk3(NR%)
            ReDim SHIVRate(NR%), maxhivrate(NR%), aidsdeathsbyage(NCohorts, 2)
            ReDim incaidsdths(NR%), AIDSDthRateI(NR%), sGDPPCP(NR%), GDPPCPI(NR%), SGDPinPOP(NR%)

            ReDim SCBR(NR%), SCDR(NR%), SPopForeign(NR%), SLit(NR%)
            ReDim SPopForeignIn(NR%) 'bbh 2020/03/09
            ReDim SPopForeignOut(NR%) 'bbh 2020/03/09
            ReDim PopForInRatioI(NR%), MigInflowMul(NR%), MigOutFlowMul(NR%) 'bbh 2022/06/23 'bbh 2022/06/23
            ReDim EDYRSAG25I(NR%, 3)
            ReDim EDYRSAG15I(NR%, 3), EDPRIPERI(NR%, 3), sedyrsag15(NR%)
            ReDim CumSDeath(NR%), HIVTECCNTLReg(NR%), SHIVTECCNTLReg(NR%), SHIVTECCNTLRegI(NR%), SAIDSDTHSCM(NR%)
            ReDim POPRI(NR%), CalIAdj(NR%), malnpoppi(NR%)
            'jrs 2024/10/28
            ReDim POPI(NR%)
            ReDim HIVRateL(NR%, 10), SAIDSDRATE(NR%), HIVAIDSRI(NR%), HIVRateMAvg(NR%)
            ReDim HlthSpendRGDPI(NR%), SmLifExpTarBase(NR%, 2), POPURBShift(NR%)
            ReDim LifeCompI(2), LifExpTarBas(2), LifExpTarBas2(2), LifExpTar(2), lifexpmaxi(2), LifTechAdv(2)
            ReDim LifNormMax(2), LifMax(2), LifExpMax(2), LifExpTarMax(2)
            ReDim SLIFEXPRat(NR%), SEdSecEnrGFem(NR%), WATSAFEI(NR%), sCLPC(NR%)
            ReDim LifExpI(NR%, NGender%), LIFEXPshift(NR%, NGender%)
            ReDim deathsNS(2, NGender%) '2 cohorts, 2 genders
            ReDim LIFEXPNS(NR%, NGender%), LIFEXPshiftNS(NR%, NGender%), SLIFEXP(NR%, NGender%), SLIFEXPFin(NR%, NGender%)
            'jrs 2009/12/16
            ReDim MalnChilCompI(NR%) 'bbh 2019/06/02 changed from smalncomp
            'jrs 2015/12/07
            ReDim MalnChilShift(NR%), SMalnChP(NR%) 'bbh 2019/06/02
            ReDim smalnsamcomp(NR%)
            'jrs 2021/05/13
            ReDim SMALNCHPSAM(NR%)

            'jrs 2010/04/10
            ReDim SEDYRSAG25(NR%, 3) 'jrs 2016/01/13 added 2nd dimension
            ReDim SEDYRSAG15v2(NR%, 3) 'dsr 2016/05/04 for using annual changes from exogenous series
            'jrs 2011/10/11
            ReDim SHDINEW(NR%)
            'jrs 2013/05/28
            ReDim SavingsNeeded(NR%), SavingsNew(NR%), SavingsOld(NR%)
            'bbh 2014/06/18
            ReDim sworkageretire(NR%), WorkAgeRetireI(NR%)

            'mti June 02, 2010 - added to avaoid run to run in averaging infraelecacc (rural and urban)
            'mti February 2011 Infarstructure -start
            'ReDim sPOPRURAL(NR%)
            'mti February 2011 Infarstructure -end
            ReDim sPOPTotal(NR%)

            'dsb 2026/1/15
            ReDim POP_Lag(NR%), POP_Lag1(NR%)
            'mti April 05, 2012 - start
            ReDim HHSizeShift(NR%)
            ReDim HHSizeGR(NR%)
            ReDim sHHSIZE(NR%)
            'mti April 05, 2012 - end


            'mti February 2011 Infarstructure -start
            ReDim sPopUrban1(NR%), migrateRI(NR%)  'mti 2018/11/27
            'jrs 2026/08/12
            ReDim migrateinRI(NR%), migrateoutRI(NR%), spopurbanm(NR%)
            ReDim sPopTotal1(NR%)
            'mti February 2011 Infarstructure end

            'mti 2019/01/22 start
            ReDim sPOPR(NR%)
            'mti 2019/01/22 End

            'jrs 2007/06/19
            ReDim lifexptari(NR%, 2)
            lifexpmaxi(1) = 80
            lifexpmaxi(2) = 85
            'JRS 2007/08/14
            ReDim HIVRATEI(NR%)
            'jrs 2007/08/28 added regions as dimension
            ReDim aidsdeathsbyageHeavy(NCohorts, NR, 2)
            'jrs 2009/02/17 share of access to safe water and sanitation

            'mti 03/10/2011 -start
            ReDim smalnpopmul(NR%), smalnmul(NR%)
            'mti 03/10/2011 - end

            'mti August 2017 Start
            ReDim EdIndHDINew(NR%, NGender%)
            ReDim LifeExpIndHDINew(NR%, NGender%)
            'mti August 2017 End


            ReDim TFRGR(NR%), stfr(NR%) 'bbh 2013/13/19
            ReDim smigrater(NR%) 'jrs 2017/11/06
            ReDim smigrants(NR%) 'jrs 2017/11/14
            ReDim SMigrateIn(NR%), SMigrateOut(NR%), SMigrantsIn(NR%), SMigrantsOut(NR%) 'bbh 2020/03/09
            'jrs 2016/12/01
            ReDim StuntingByAge(NCohorts, NR, 2)
            'jrs 2021/05/12
            ReDim fStuntingByAge(NAges, NR, 2)

            'mti September 2017 start
            ReDim sGDPPC(NR%)
            'mti September 2017 End

            'jrs 2017/12/05
            ReDim SDEATHS(NR%)

            'mti 2017/12/27 start
            ReDim GDPPCPHDINewMinLog(NGender%), GDPPCPHDINewDenom(NGender%)
            'mti 2017/12/27 End

            'jrs 2018/03/01
            ReDim SPOPLE15(NR%)

            'mti 2022/11/25 start - moved to new procedure
            '    'cdv 2022/03/07
            '    ReDim AGEOFMARRIAGEShift(NR%, 3)
            '
            '    'mti 2022/10/24 start
            '    ReDim sAGEOFMARRIAGE(NR%, NGender%)
            '    'mti 2022/10/24 End
            'mti 2022/11/25 end
            'cdv 2022/10/28
            ReDim UnchainedLastYearUnpaidLabor(NR%, 3)
            ReDim ChainedLastYearUnpaidLabor(NR%, 3)

            'mti 2022/10/24 start
            ReDim sAGEOFMARRIAGE(NR%, NGender%)
            'mti 2022/10/24 End


            ReDim UnchainedLastYearWomenLeadership(NR%)
            ReDim ChainedLastYearWomenLeadership(NR%)

            'cdv 2022/12/12
            ReDim UnchainedLastYearFoodInsecurity(NR%, 3)
            ReDim ChainedLastYearFoodInsecurity(NR%, 3)

            ReDim Preserve Groupings(NR%)
            's = exp^(Regression)
            'gvdb 2024/04/05
            ' unused variable
            'Dim s1, s2 As Single

            'mti April 05, 2012 - start
            '
            '   First Year:  Read House Hold Size Growth Rate (MTI: talk to BBH/JRS about placement of this code)
            '   ---------------------------------------------------------------
            If context.HistoricMode = False Then
                'jrs 2012/06/15 Dim dbGR As Database
                'jrs 2012/06/15 Set dbGR = OpenDatabase(IFs_MDB2Active)
                Call LoadGrowthRatesOneDim("HHSIZEGR", HHSizeGR, context)
                'jrs 2012/06/15 dbGR.Close
            End If
            For R% = 1 To NR%
                sHHSIZE(R%) = HHSIZE(R%)
                If Not HHSizeGR(R%).HasValue Then
                    HHSizeGR(R%) = -0.005 '.5% reduction in HHSize
                End If
            Next R%
            'mti April 05, 2012 - end

            'jrs 2011/03/01
            bCancelMsgMatchMortLE = False
            'jrs 2020/05/08 Need to read birthmshr from working file
            birthfshr = 1 - birthmshr

            '
            '   First Year:  Read Global Population Parameters
            '   ----------------------------------------------

            For j% = 0 To NCohorts
                coname$(j%) = "Cohort" + j%.ToString.Trim
            Next j%

            Dim MaleMigrants As Single, FemaleMigrants As Single
            Dim MaleDeaths As Single, FemaleDeaths As Single

            Using db As IFs2Context = New IFs2Context()
                Dim pc As PopCohortGlobal = db.PopCohortGlobal.Where(Function(g) g.Variable = "Clage").Single()
                LoadPopCohortGlobalVar(clage, pc, context)

                pc = db.PopCohortGlobal.Where(Function(g) g.Variable = "Clmage").Single()
                LoadPopCohortGlobalVar(clmage, pc, context)

                pc = db.PopCohortGlobal.Where(Function(g) g.Variable = "MigrateByAgeM").Single()
                LoadPopCohortGlobalVar(Migratebyage, pc, context, 1)
                For j% = 0 To NCohorts  'Read differential migration by age, males
                    'jrs 2012/08/31
                    If Migratebyage(j%, 1).HasValue Then
                        MaleMigrants = MaleMigrants + CSng(Migratebyage(j%, 1))
                    End If
                Next

                pc = db.PopCohortGlobal.Where(Function(g) g.Variable = "MigrateByAgeF").Single()
                LoadPopCohortGlobalVar(Migratebyage, pc, context, 2)
                For j% = 0 To NCohorts  'Read differential migration by age, females
                    'jrs 2012/08/31
                    If Migratebyage(j%, 2).HasValue Then
                        FemaleMigrants = FemaleMigrants + CSng(Migratebyage(j%, 2))
                    End If
                Next

                For j% = 0 To NCohorts 'Normalize migrants by age to portion of total
                    Migratebyage(j%, 1) = Migratebyage(j%, 1) / MaleMigrants
                    Migratebyage(j%, 2) = Migratebyage(j%, 2) / FemaleMigrants
                Next j%

                pc = db.PopCohortGlobal.Where(Function(g) g.Variable = "AIDSDeathsByAgeM").Single()
                LoadPopCohortGlobalVar(aidsdeathsbyage, pc, context, 1)
                For j% = 0 To NCohorts  'Read differential AIDS deaths by age, males
                    'jrs 2012/08/31
                    If aidsdeathsbyage(j%, 1).HasValue Then
                        MaleDeaths = MaleDeaths + CSng(aidsdeathsbyage(j%, 1))
                    End If
                Next

                pc = db.PopCohortGlobal.Where(Function(g) g.Variable = "AIDSDeathsByAgeF").Single()
                LoadPopCohortGlobalVar(aidsdeathsbyage, pc, context, 2)
                For j% = 0 To NCohorts  'Read differential AIDS deaths by age, females
                    'jrs 2012/08/31
                    If aidsdeathsbyage(j%, 2).HasValue Then
                        FemaleDeaths = FemaleDeaths + CSng(aidsdeathsbyage(j%, 2))
                    End If
                Next

                For j% = 0 To NCohorts 'Normalize deaths by age to portion of total
                    aidsdeathsbyage(j%, 1) = aidsdeathsbyage(j%, 1) / MaleDeaths
                    aidsdeathsbyage(j%, 2) = aidsdeathsbyage(j%, 2) / FemaleDeaths
                Next j%

                ' Read ultimate fertility
                pc = db.PopCohortGlobal.Where(Function(g) g.Variable = "UltimateFertility").Single()
                LoadPopCohortGlobalVar(ultimatefertility, pc, context)
                For j% = 0 To NCohorts
                    ultimatefertility(j%) = ultimatefertility(j%) / 1000
                Next

                pc = db.PopCohortGlobal.Where(Function(g) g.Variable = "UltimateSurvByAgeF").Single()
                LoadPopCohortGlobalVar(ultimatemort, pc, context, 2)

                pc = db.PopCohortGlobal.Where(Function(g) g.Variable = "UltimateSurvByAgeM").Single()
                LoadPopCohortGlobalVar(ultimatemort, pc, context, 1)

                Dim pcAIDS As PopCohortAIDSRegion
                For Each pcAIDS In db.PopCohortAIDSRegion
                    LoadPopCohortAIDSRegion(aidsdeathsbyageHeavy, pcAIDS, context)
                Next
            End Using

            'jrs 2009/05/22
            '--------------
            'explanation by Randall Kuhn
            'When we do (Survivor(t)-Survivor(t-1))/Survivor(t-1) we're computing nqx, n = t, and x = t-1
            'we were using nmx = nqx/n
            'nqx is probability of ever dying between age x and x+n
            'we should be using mqx
            'mqx is the probability that someone age x to x+n dies in the current year
            'nmx = ( 1 - ( ( 1 - nqx ) ^ 1/n ) )
            'we multiply times 1000 to convert to deaths per 1,000 people
            Dim nqx As Single

            For j% = 1 To 2 'Gender specific; computes portion of population within a one-year
                'cohort that will die
                'in a year from information about those who survive across 5-year intervals
                'When finished, the values in UltimateMort will be in same form as BasMorDst
                For cocount% = NCohorts - 1 To 0 Step -1
                    If cocount% = 0 Then
                        ultimatemort(cocount%, j%) =
                            (100000 - ultimatemort(cocount%, j%)) / 100000
                    ElseIf cocount% = 1 Then
                        'jrs 2009/05/22 ultimatemort(cocount%, j%) = _
                        '    (ultimatemort(cocount% - 1, j%) - ultimatemort(cocount%, j%)) / _
                        'ultimatemort(cocount% - 1, j%) / 4  'Actually only years 1-4
                        nqx = CSng((ultimatemort(cocount% - 1, j%) - ultimatemort(cocount%, j%)) /
                        ultimatemort(cocount% - 1, j%))
                        ultimatemort(cocount%, j%) = CSng((1 - ((1 - nqx) ^ (1 / 4)))) 'Actually only years 1-4
                    Else
                        'jrs 2009/05/22 ultimatemort(cocount%, j%) = _
                        '(ultimatemort(cocount% - 1, j%) - ultimatemort(cocount%, j%)) / _
                        'ultimatemort(cocount% - 1, j%) / 5
                        'jrs 2012/08/31
                        If ultimatemort(cocount% - 1, j%).HasValue AndAlso ultimatemort(cocount%, j%).HasValue Then
                            nqx = CSng((ultimatemort(cocount% - 1, j%) - ultimatemort(cocount%, j%)) /
                            ultimatemort(cocount% - 1, j%))
                        Else
                            nqx = 0
                        End If
                        ultimatemort(cocount%, j%) = CSng((1 - ((1 - nqx) ^ (1 / 5))))
                    End If
                Next cocount%
                'jrs 2012/09/04
                If ultimatemort(NCohorts - 1, j%) = 0 Then
                    ultimatemort(NCohorts, j%) = 0
                Else
                    ultimatemort(NCohorts, j%) = Amin(ultimatemort(NCohorts - 1, j%) +
                        AMAX(0.045F, (ultimatemort(NCohorts - 1, j%) - ultimatemort(NCohorts - 2, j%))) * 10.0F, 1000.0F) 'jrs 2009/05/26 adding limit of 1000
                End If
                ' UN data does not include mortality above 100; developed this to
                ' produce reasonable forecast for US in 2050; ie like UN scenario
                ' Probability of death at 100+ must be very high
            Next j%

            '   First Year:  Read Cohort Distribution Data
            '   ------------------------------------------
            'ttt 2019/10/08 
            ReDim POPMaleG(NR), POPFemaleG(NR)

            Using db As IFs2Context = New IFs2Context()
                'ttt 2019/10/08 ReadPopCohortDist(agedst, context, db)
                ReadPopCohortDist(agedst, context, db, POPMaleG, POPFemaleG)
                'jrs 2025/01/22
                ReadPopSingleAgeDist(fagedst, context, db)
            End Using
            ReadMortCohortDist(BasMordst, context)

            Using db As IFs2Context = New IFs2Context()
                If context.HistoricMode Then
                    For Each Reg In db.PopFertilityCohortRegionFemaleHist
                        LoadFertCohReg(basferdst, Reg)
                    Next
                Else
                    For Each Reg In db.PopFertilityCohortRegionFemale
                        LoadFertCohReg(basferdst, Reg)
                    Next
                End If
            End Using

            Dim HHSizeCalc As Single, HHIntensity As Single
            Dim tfrcomp As Double, B As Double
            Dim POPMale As Double, POPFemale As Double
            Dim dth As Double, PopAge25Male As Double, PopAge25FeMale As Double, PopAge15Male As Double, PopAge15FeMale As Double

            For R% = 1 To NR%
                '
                '   First Year:  Adjust Age Distribution (National Data) to Regional Population
                '   ---------------------------------------------------------------------------

                'dsr 2016/03/24 Code to Read in Exogenous value for Initial POP **** start ****
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("POP", "", "", CStr(context.FirstYear), R%, context)
                End If
                'dsr 2016/03/24 Code to Read in Exogenous value for Initial POP **** end ****
                AdjustCohPop(agedst, POP, R)
                'jrs 2024/10/28
                POPI(R%) = POP(R%)

                '   First Year:  Adults (>15 and <50) - Needed for AIDS Calculations Later
                '   ----------------------------------------------------------------------
                POPLE15(R%) = 0
                'jrs 2018/04/18
                POPGT65(R%) = 0
                '        POPGT50 = 0
                For j% = 1 To 3
                    POPLE15(R%) = CSng(POPLE15(R%) + agedst(j%, R%, 1) + agedst(j%, R%, 2))
                Next j%
                'jrs 2018/03/01 Need it for Migration Dyadic logic, repeated in All years
                For j% = 14 To NCohorts
                    POPGT65(R%) = CSng(POPGT65(R%) + agedst(j%, R%, 1) + agedst(j%, R%, 2))
                Next j%
                POP15TO65(R%) = POP(R%) - POPLE15(R%) - POPGT65(R%)
                SPOPLE15(R%) = POPLE15(R%)
                sPOP15TO65(R%) = POP15TO65(R%)

                'jrs 2013/04/12
                POPPREWORK(R%) = 0
                For j% = 1 To 3 'jrs 2013/04/30 don't allow changing parameters in first year workageentry(R%) \ 5
                    POPPREWORK(R%) = CSng(POPPREWORK(R%) + agedst(j%, R%, 1) + agedst(j%, R%, 2))
                Next j%

                'mti HH Size for Infrastructure August 2010 start
                '   First Year:  Household Size: Needs POP and POPLE15
                '   ----------------------------------------------------------------------
                If context.HistoricMode Then
                    Call XYTABL("Population Age Ratio Versus Households Intensity (1960)",
                            100 * POPPREWORK(R%) / AMAX(0.000001, POP(R%)), HHIntensity, context)
                Else
                    Call XYTABL("Population Age Ratio Versus Households Intensity (MOSTRECENT)",
                            100 * POPPREWORK(R%) / AMAX(0.000001, POP(R%)), HHIntensity, context)
                End If

                HHSizeCalc = 1 / AMAX(0.000001, HHIntensity)
                HHSizeShift(R%) = HHSIZE(R%) - HHSizeCalc

                'mti HH Size for Infrastructure August 2010 end

                'jrs 2022/01/25
                '   First Year:  Spread Age Distribution Across Years - Full Distribution
                '   ---------------------------------------------------------------------
                IFsPOPSpreadAgeDst(R%, context)

                '   BBH 6/23/04 V5_0 Begin
                '
                '   First Year:  Notes on Design Issues
                '   -----------------------------------
                '
                '   There are three numbers that capture fertility:  TFR, CBR, the fertility distribution
                '   There are three numbers that caputure mortality:  LIFEXP, CDR, mortality distribution
                '   They will not be completely compatible.  The distributions must be used to handle cohort
                '   dynamics.  But they may not be available for all countries (e.g. Taiwan, Palestine),
                '   whereas the others are.  For many years we used CBR and CDR to adjust the distributions
                '   and then calculated TFR and LIFEXP from them.  Over time, TFR and LIFEXP have become the
                '   key variables that dynamically drive the population model, and good data are now available.
                '   We have therefore switched over to them as the bases for initial conditions of the model,
                '   calculating CBR and CDR from them, giving us also POPR.

                'jrs 2019/05/03 *** BEGIN ***
                If tfrexovals(R%) > -100 Then
                    TFR(R%) = tfrexovals(R%)
                End If
                'jrs 2019/05/03 *** END ***

                'jrs 2014/08/27 Exogenous Models
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("TFR", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                '
                '   First Year:  Compute Distribution Multiplier from Total Fertility Rate
                '   ---------------------------------------------------------------------
                tfrcomp = 0
                For j% = 0 To NCohorts
                    tfrcomp = tfrcomp + basferdst(j%, R%) * 5
                    '*5 because of 5 year cohorts
                Next j%
                fb(R%) = TFR(R%) / AMAX(0.001, tfrcomp)
                TFRI(R%) = TFR(R%)
                stfr(R%) = TFR(R%) 'bbh 2013/11/19


                '
                '   First Year:  Compute Crude Birth Rate from TFR-adjusted Fertility Distribution
                '   ------------------------------------------------------------------------------

                B = 0!
                For j% = 1 To NCohorts
                    B = B + agedst(j%, R%, 2) * basferdst(j%, R%) * fb(R%)
                Next j%
                CBR(R%) = CSng(B / POP(R%) * 1000)


                '
                '   First Year:  Compute Mortality Distribution Multiplier Compatible with Life Expectancy
                '   ---------------------------------------------------------------------------------------
                'mti LifeExp/ gender May 2006 - average LifeXP (Total) from male and female
                'ttt 2019/10/08 
                POPMale = POPMaleG(R)
                POPFemale = POPFemaleG(R)
                'jrs 2026/07/21
                POPBYSEX(R%, Male) = CSng(POPMale)
                POPBYSEX(R%, Female) = CSng(POPFemale)
                POPBYSEX(R%, 3) = CSng(POPMale + POPFemale)

                'POPMale = 0
                'POPFemale = 0
                'For j% = 1 To NCohorts
                '    POPMale = POPMale + agedst(j%, R%, 1)
                '    POPFemale = POPFemale + agedst(j%, R%, 2)
                'Next j%
                'End ttt 2019/10/08

                LIFEXP(R%, Total) = CSng((LIFEXP(R%, Male) * POPMale + LIFEXP(R%, Female) * POPFemale) / AMAX(0.00001, POPMale + POPFemale))
                'mti LifeExp/ gender May 2006 -end

                'jrs 2007/09/04
                'jrs 2009/05/22 Dim mfactorMCohort As Integer, mfactorFCohort As Integer
                Dim mFactorM As Single, mFactorF As Single

                'jrs 2009/05/22
                'Explained by Randall Kuhn
                'Life Expectancy was computed as the median age at death,
                'and it should be computed as a mean
                'average number of years of life given the survival rates in each age group

                'jrs 2007/09/04
                'mfactor for Males
                '        mortinfants = BasMordst(0, R%, 1)
                '        cumMort = mortinfants  'Infant Mortality
                '        CumLif = 0
                '
                '        For j% = 1 To NCohorts
                '            MortCohort = BasMordst(j%, R%, 1)
                '            cohMort = MortCohort
                '
                '            If CumLif + 5 < LIFEXP(R%, 1) Then
                '                cumMort = cumMort + cohMort * 5 'Five year cohorts
                '                CumLif = CumLif + 5
                '            Else
                '                Dif = LIFEXP(R%, 1) - CumLif
                '                cumMort = cumMort + cohMort * Dif
                '                CumLif = CumLif + Dif
                '                mfactorMCohort = j
                '                Exit For
                '            End If
                '        Next j%
                '        mFactorM = 0.5 / cumMort  '.5 represents 50% chance of dying younger,
                'jrs 2019/05/06 maxfactor from 6 to 14 for Iran historical
                mFactorM = matchMortWLifeExp(BasMordst, R, 1, LIFEXP(R, 1), 0, 14, bCancelMsgMatchMortLE) 'jrs 2016/11/01 change the max factor from 6 to 7

                'mfactor for FeMales
                '        mortinfants = BasMordst(0, R%, 2)
                '        cumMort = mortinfants  'Infant Mortality
                '        CumLif = 0
                '
                '        For j% = 1 To NCohorts
                '            MortCohort = BasMordst(j%, R%, 2)
                '            cohMort = MortCohort
                '
                '            If CumLif + 5 < LIFEXP(R%, 2) Then
                '                cumMort = cumMort + cohMort * 5 'Five year cohorts
                '                CumLif = CumLif + 5
                '            Else
                '                Dif = LIFEXP(R%, 2) - CumLif
                '                cumMort = cumMort + cohMort * Dif
                '                CumLif = CumLif + Dif
                '                mfactorFCohort = j
                '                Exit For
                '            End If
                '        Next j%
                '        mFactorF = 0.5 / cumMort  '.5 represents 50% chance of dying younger,
                'jrs 2019/05/06 maxfactor from 6 to 16 for Iran historical
                mFactorF = matchMortWLifeExp(BasMordst, R, 2, LIFEXP(R, 2), 0, 16, bCancelMsgMatchMortLE) 'jrs 2016/11/01 change the max factor from 6 to 7

                ' and 50% change of dying older; hence life expectancy at birth
                'mti LifeExp/ gender May 2006
                'lifexpi(R%) = LIFEXP(R%, 3)

                'mti Lifexp Smooth June 2006 -START
                For gend% = 1 To NGender%
                    LifExpI(R%, gend%) = LIFEXP(R%, gend%)
                    'bbh 2009/09/09
                    SLIFEXPFin(R%, gend%) = LifExpI(R%, gend%)
                Next gend%
                'mti Lifexp Smooth June 2006 - end


                '        Lifexpi(R%) = LIFEXP(R%)
                '
                '   First Year:  Adjust Crude Death Rate
                '   ------------------------------------------------------------------------
                'temp1 = BasMorDst(0, R%, 1) + BasMorDst(0, R%, 2)
                'temp2 = temp1 * mfactor
                'temp3 = CDbl(CBR(R%)) * CDbl(POP(R%))
                'dth = temp2 * temp3
                ''dth = (CBR(R%) * POP(R%) / 1000) * (BasMorDst(0, R%, 1) + BasMorDst(0, R%, 2)) * mfactor
                'jrs 2007/09/04 dth = mfactor * (CBR(R%) * POP(R%)) * (BasMordst(0, R%, 1) + BasMordst(0, R%, 2))
                dth = (CBR(R%) * POP(R%)) * (mFactorM * BasMordst(0, R%, 1) + mFactorF * BasMordst(0, R%, 2))
                'infant deaths approximation; could reproduce all of fertility code, but clumsy
                'very small error introduced to CDR, POPR when deaths are recalculated later
                'If RegName$(R%) = "Taiwan" Then Print #6, j%, dth, temp1, temp2, temp3, CBR(R%), _
                'POP(R%), BasMorDst(0, R%, 1), BasMorDst(0, R%, 2), mfactor
                For j% = 1 To NCohorts
                    'dth = dth + (agedst(j%, R%, 1) * BasMorDst(j%, R%, 1) + _
                    'agedst(j%, R%, 2) * BasMorDst(j%, R%, 2)) * mfactor
                    'jrs 2007/09/04 dth = dth + (agedst(j%, R%, 1) * BasMordst(j%, R%, 1) + _
                    'agedst(j%, R%, 2) * BasMordst(j%, R%, 2)) * mfactor * 1000

                    'jrs 2009/05/22
                    '            If j <= mfactorMCohort Then
                    '                dth = dth + agedst(j%, R%, 1) * BasMordst(j%, R%, 1) * mFactorM * 1000
                    '            Else
                    '                dth = dth + agedst(j%, R%, 1) * BasMordst(j%, R%, 1) * (mFactorM + (1 - mFactorM) / _
                    '                (NCohorts - mfactorMCohort) * (j - mfactorMCohort)) * 1000
                    '            End If
                    '            If j <= mfactorFCohort Then
                    '                dth = dth + agedst(j%, R%, 2) * BasMordst(j%, R%, 2) * mFactorF * 1000
                    '            Else
                    '                dth = dth + agedst(j%, R%, 2) * BasMordst(j%, R%, 2) * (mFactorF + (1 - mFactorF) / _
                    '                (NCohorts - mfactorFCohort) * (j - mfactorFCohort)) * 1000
                    '            End If
                    'jrs 2012/08/31
                    dth = dth + agedst(j%, R%, 1) * BasMordst(j%, R%, 1) * mFactorM * 1000
                    'jrs 2012/08/31
                    dth = dth + agedst(j%, R%, 2) * BasMordst(j%, R%, 2) * mFactorF * 1000

                    'If RegName$(R%) = "Taiwan" Then Print #6, j%, dth, agedst(j%, R%, 1), _
                    'BasMordst(j%, R%, 1), agedst(j%, R%, 2), BasMorDst(j%, R%, 2)

                Next j%
                'CDR(R%) = (dth * 1000) / POP(R%)
                CDR(R%) = CSng(dth / POP(R%))
                'This can be quite different from the CDR computed later in the first year
                '   Thus anything that uses it or POPR before then is subject to a first
                '   year transient; this calculation of dth should be refined to be more
                '   like Deaths later on
                'If RegName$(R%) = "Taiwan" Then Close #6
                '    If RegName$(R%) = "China" Then Stop
                '   BBH 6/23/04 V5_0 End

                '
                '   First Year:  Set Initial Conditions
                '   -----------------------------------
                MORTMG(R%) = 1.0!
                '   BBH 5/7/04 V5_0
                'IncShrI(R%) = INCSHR(R%)
                conusei(R%) = CONTRUSE(R%)
                ContrExcessMin(R%) = 0
                LITI(R%) = LIT(R%)
                SLit(R%) = LIT(R%)
                SHIVRate(R%) = HIVRATE(R%)
                PopUrbGRI(R%) = POPURBGR(R%) / 100

                'jrs 2014/08/28 Exogenous Models
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("POPURBAN", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                'mti February 2011 Infarstructure -start
                'mti April 05, 2012 - start
                'sPopUrban1(R%) = POPURBAN(R%)
                sPopUrban1(R%) = POPURBAN(R%) / (1 + PopUrbGRI(R%))
                POPRURAL(R%) = POP(R%) - POPURBAN(R%)
                HHTOTAL(R%) = POP(R%) / HHSIZE(R%)
                'mti April 05, 2012 -end
                'mti February 2011 Infarstructure end

                SPOPURBAN(R%) = POPURBAN(R%)

                'mti 2022/06/21 start
                sPOPRURAL(R%) = POPRURAL(R%)
                'mti 2022/06/21 end

                'mti 2018/11/27 Start
                migrateRI(R%) = migrater(R%)
                'mti 2018/11/27 End
                'jrs 2026/08/12
                migrateinRI(R%) = migrateinr(R%)
                migrateoutRI(R%) = migrateoutr(R%)
                spopurbanm(R%) = popurbanm(R%)

                sGDPPCP(R%) = GDPPCP(R%)
                GDPPCPI(R%) = GDPPCP(R%)
                'bbh August 29, 2009
                SGDPinPOP(R%) = GDP(R%)

                'mti September 2017 Start
                sGDPPC(R%) = GDPPC(R%)
                'mti September 2017 End

                SCBR(R%) = CBR(R%)
                'dsr 2016/05/04 CDR can change so save SCDR later
                SCDR(R%) = CDR(R%)
                'If RegName$(R%) = "Italy" Then Stop
                SPopForeign(R%) = POPFOREIGN(R%)
                SPopForeignIn(R%) = POPFOREIGNIN(R%) 'bbh 2020/03/09
                SPopForeignOut(R%) = POPFOREIGNOUT(R%) 'bbh 2020/03/09

                '   BBH 1/23/04 begin V5_0
                CumSDeath(R%) = 0
                HIVTECCNTLReg(R%) = 0
                SHIVTECCNTLReg(R%) = 0
                If hivincr(R%) < 0 Then SHIVTECCNTLReg(R%) = -hivincr(R%) / 100
                SHIVTECCNTLRegI(R%) = SHIVTECCNTLReg(R%)
                AIDSDTHSCM(R%) = 0
                SAIDSDTHSCM(R%) = 0
                '   BBH 1/23/04 end V5_0
                '        SmoothGDPPCP(R%) = GDPPCP(R%)
                'JRS 2007/08/14
                HIVRATEI(R%) = HIVRATE(R%)
                'jrs 2011/10/11
                SHDINEW(R%) = HDI(R%)

                '
                '   First Year:  Set Initial Conditions
                '   -----------------------------------
                PopAge25Male = 0
                For j% = 6 To NCohorts
                    PopAge25Male = PopAge25Male + agedst(j%, R%, 1)
                Next j%

                PopAge25FeMale = 0
                For j% = 6 To NCohorts
                    PopAge25FeMale = PopAge25FeMale + agedst(j%, R%, 2)
                Next j%

                EDYRSAG25(R%, Total) =
                    CSng((PopAge25Male * EDYRSAG25(R%, Male) + PopAge25FeMale * EDYRSAG25(R%, Female)) _
                    / (PopAge25Male + PopAge25FeMale))

                'jrs 2016/01/13 added 2nd dimension to save females too
                'jrs 2010/04/10
                SEDYRSAG25(R%, 3) = EDYRSAG25(R%, Total)
                SEDYRSAG25(R%, 2) = EDYRSAG25(R%, Female)
                'dsr 2016/05/04 Add line to save EDYRSAG25 for males
                SEDYRSAG25(R%, 1) = EDYRSAG25(R%, Male)

                PopAge15Male = 0
                For j% = 4 To NCohorts
                    PopAge15Male = PopAge15Male + agedst(j%, R%, 1)
                Next j%

                PopAge15FeMale = 0
                For j% = 4 To NCohorts
                    PopAge15FeMale = PopAge15FeMale + agedst(j%, R%, 2)
                Next j%

                EDYRSAG15(R%, Total) =
                    CSng((PopAge15Male * EDYRSAG15(R%, Male) + PopAge15FeMale * EDYRSAG15(R%, Female)) _
                    / (PopAge15Male + PopAge15FeMale))

                'dsr 2016/05/04 Add lines to save EDYRSAG15 by gender
                SEDYRSAG15v2(R%, 3) = EDYRSAG15(R%, Total)
                SEDYRSAG15v2(R%, 2) = EDYRSAG15(R%, Female)
                SEDYRSAG15v2(R%, 1) = EDYRSAG15(R%, Male)
            Next R

            'bbh 2013/11/19 start
            'Get the initial condition for TFR growth rate; saved in preprocessor
            Call LoadGrowthRatesOneDim("TFRGR", TFRGR, context)
            'bbh 2013/11/19 end

            'mti 2022/11/25 start - moved to new procedure
            ''mti 2022/10/24 Start
            'ReDim AGEOFMARRIAGEGR(NR%, NGender%)
            'Call LoadGrowthRatesTwoDim("AGEOFMARRIAGEGR", AGEOFMARRIAGEGR, context)
            ''mti 2022/10/24 End
            'mti 2022/11/25 End

            'jrs 2024/06/06
            TblNameEducExp = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Govt Exp Educ as % of GDP (" & BaseYear & ") Log"
            TblNameEducExp = getFunctionNameYrOrMstRct(TblNameEducExp, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TblNameHealthExp = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Govt Exp Hlth as % of GDP (" & BaseYear & ") Log"
            TblNameHealthExp = getFunctionNameYrOrMstRct(TblNameHealthExp, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TblNameStunting = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Stunting (" & BaseYear & ") Log"
            TblNameStunting = getFunctionNameYrOrMstRct(TblNameStunting, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            'dsb 2026/06/08 GDPR calculation
            If IY = 1 Then
                ReDim IRGDPR(NR)
                LoadInitialGDPR(IRGDPR)
                For R% = 1 To NR%
                    If IRGDPR(R%) <> 0 AndAlso IRGDPR(R%) < 20 AndAlso IRGDPR(R%) > -10 Then
                        GDPR(R%) = IRGDPR(R%)
                    Else
                        GDPR(R%) = igdpr(R%)
                    End If
                Next
            End If
            ifsPOPAllYears(context)
        End Sub

        'jrs 2013/06/11 changed to public
        Public Function Pop_AgeYear(ageyear%, Rno%, Sex%) As Single
            '- Education Module --------------------------
            'mti Sept 2006, Added end cohorts
            'uses sprague multiplier for spreading agecohort to ageyears

            'can be used only within Populat.bas where agedst() is declared at module level
            'F0 is cohort of ageyear; F_2 (etc.) is two cohorts back; f2 (etc.) is two chorts forward
            'PopC(1) = 0-4 years;
            Dim f0 As Double, f1 As Double, f2 As Double, f3 As Double
            Dim F_1 As Double, cchrt As Integer, F_2 As Double, F_3 As Double

            Pop_AgeYear = 0
            Select Case ageyear
                Case 0 To 4 'mti May 30 2006
                    f0 = agedst(1, Rno%, Sex%)
                    f1 = agedst(2, Rno%, Sex%)
                    f2 = agedst(3, Rno%, Sex%)
                    f3 = agedst(4, Rno%, Sex%)
                    Select Case ageyear
                        Case 0
                            Pop_AgeYear = CSng(0.3616 * f0 - 0.2768 * f1 + 0.1488 * f2 - 0.0336 * f3)
                        Case 1
                            Pop_AgeYear = CSng(0.264 * f0 - 0.096 * f1 + 0.04 * f2 - 0.008 * f3)
                        Case 2
                            Pop_AgeYear = CSng(0.184 * f0 + 0.04 * f1 - 0.032 * f2 + 0.008 * f3)
                        Case 3
                            Pop_AgeYear = CSng(0.12 * f0 + 0.136 * f1 - 0.072 * f2 + 0.016 * f3)
                        Case 4
                            Pop_AgeYear = CSng(0.0704 * f0 + 0.1968 * f1 - 0.0848 * f2 + 0.0176 * f3)
                    End Select

                Case 5 To 9
                    F_1 = agedst(1, Rno%, Sex%)
                    f0 = agedst(2, Rno%, Sex%)
                    f1 = agedst(3, Rno%, Sex%)
                    f2 = agedst(4, Rno%, Sex%)

                    Select Case ageyear
                        Case 5
                            'mti May 30 2006
                            Pop_AgeYear = CSng(0.0336 * F_1 + 0.2272 * f0 - 0.0752 * f1 + 0.0144 * f2)
                        Case 6
                            Pop_AgeYear = CSng(0.008 * F_1 + 0.232 * f0 - 0.048 * f1 + 0.008 * f2)
                        Case 7
                            Pop_AgeYear = CSng(-0.008 * F_1 + 0.216 * f0 - 0.008 * f1)
                        Case 8
                            Pop_AgeYear = CSng(-0.016 * F_1 + 0.184 * f0 + 0.04 * f1 - 0.008 * f2)
                        Case 9
                            Pop_AgeYear = CSng(-0.0176 * F_1 + 0.1408 * f0 + 0.0912 * f1 - 0.0144 * f2)
                    End Select

                Case 10 To 89
                    cchrt = 1 + ageyear \ 5
                    F_2 = agedst(cchrt - 2, Rno%, Sex%)
                    F_1 = agedst(cchrt - 1, Rno%, Sex%)
                    f0 = agedst(cchrt, Rno%, Sex%)
                    f1 = agedst(cchrt + 1, Rno%, Sex%)
                    f2 = agedst(cchrt + 2, Rno%, Sex%)

                    Select Case ageyear Mod 5
                        Case 0
                            Pop_AgeYear = CSng(-0.0128 * F_2 + 0.0848 * F_1 + 0.1504 * f0 - 0.024 * f1 + 0.0016 * f2)
                        Case 1
                            Pop_AgeYear = CSng(-0.0016 * F_2 + 0.0144 * F_1 + 0.2224 * f0 - 0.0416 * f1 + 0.0064 * f2)
                        Case 2
                            Pop_AgeYear = CSng(0.0064 * F_2 - 0.0336 * F_1 + 0.2544 * f0 - 0.0336 * f1 + 0.0064 * f2)
                        Case 3
                            Pop_AgeYear = CSng(0.0064 * F_2 - 0.0416 * F_1 + 0.2224 * f0 + 0.0144 * f1 - 0.0016 * f2)
                        Case 4
                            Pop_AgeYear = CSng(0.0016 * F_2 - 0.024 * F_1 + 0.1504 * f0 + 0.0848 * f1 - 0.0128 * f2)
                    End Select

                Case 90 To 94
                    cchrt = 1 + ageyear \ 5
                    F_2 = agedst(cchrt - 2, Rno%, Sex%)
                    F_1 = agedst(cchrt - 1, Rno%, Sex%)
                    f0 = agedst(cchrt, Rno%, Sex%)
                    f1 = agedst(cchrt + 1, Rno%, Sex%)


                    Select Case ageyear
                        Case 90
                            'mti May 30 2006
                            Pop_AgeYear = CSng(-0.0144 * F_2 + 0.0912 * F_1 + 0.1408 * f0 - 0.0176 * f1)
                        Case 91
                            Pop_AgeYear = CSng(-0.008 * F_2 + 0.04 * F_1 + 0.184 * f0 - 0.016 * f1)
                        Case 92
                            Pop_AgeYear = CSng(-0.008 * F_1 + 0.216 * f0 - 0.008 * f1)
                        Case 93
                            Pop_AgeYear = CSng(0.008 * F_2 - 0.048 * F_1 + 0.232 * f0 + 0.008 * f1)
                        Case 94
                            Pop_AgeYear = CSng(0.0144 * F_2 - 0.0752 * F_1 + 0.2272 * f0 + 0.0336 * f1)
                    End Select

                Case 95 To 99
                    cchrt = 1 + ageyear \ 5
                    F_3 = agedst(cchrt - 3, Rno%, Sex%)
                    F_2 = agedst(cchrt - 2, Rno%, Sex%)
                    F_1 = agedst(cchrt - 1, Rno%, Sex%)
                    f0 = agedst(cchrt, Rno%, Sex%)

                    Select Case ageyear
                        Case 95
                            'mti May 30 2006
                            Pop_AgeYear = CSng(0.0176 * F_3 - 0.0848 * F_2 + 0.1968 * F_1 + 0.0704 * f0)
                        Case 96
                            Pop_AgeYear = CSng(0.016 * F_3 - 0.072 * F_2 + 0.136 * F_1 + 0.12 * f0)

                        Case 97
                            Pop_AgeYear = CSng(0.008 * F_3 - 0.032 * F_2 + 0.04 * F_1 + 0.184 * f0)

                        Case 98
                            Pop_AgeYear = CSng(-0.008 * F_3 + 0.04 * F_2 - 0.096 * F_1 + 0.264 * f0)
                        Case 99
                            Pop_AgeYear = CSng(-0.0336 * F_3 + 0.1488 * F_2 - 0.2768 * F_1 + 0.3616 * f0)
                    End Select

                Case 100 To NAges - 1 'jrs 2012/09/05
                    cchrt = 1 + ageyear \ 5
                    Pop_AgeYear = CSng(agedst(cchrt, Rno%, Sex%) * 0.2)
            End Select

        End Function

        Public Sub ifsPOPAllYears(context As IForecastContext)
            '   All Years 1: Early Population Calculations for Migration, AIDS, etc.
            '   -------------------------------------------------------------------




            WPOP = 0!
            '    wpop1550 = 0
            '    hivratechk = 0
            For R% = 1 To NR% 'temporary computation of Pop
                If IY = 1 Then POP_Lag(R%) = POP(R%)
                If IY% > 1 Then
                    POP(R%) = 0!
                    For j% = 1 To NCohorts
                        POP(R%) = CSng(POP(R%) + agedst(j%, R%, 1) + agedst(j%, R%, 2))
                    Next j%
                    POP(R%) = AMAX(0.001, POP(R%))
                    'If RegName$(R%) = "Unitd Kingdm" Then Stop
                    'jrs 2017/11/06
                    If ifsexomodelid > 0 Then
                        ReplaceVarWithExogenousForecastxReg("POP", "", "", CStr(FirstYear + IY - 1), R%, context)
                    End If
                    POP_Lag1(R%) = POP_Lag(R%)
                    POP_Lag(R%) = POP(R%)
                End If
            Next R%

            'bbh 2020/03/09 2 lines
            Call MigrationGross(context)
            'sgh dkb 2013/8/21 move call to migration up so that ECON model uses modified migration rate
            'Call Migration(context) 'dsb

            For R% = 1 To NR%

                'sgh dkb 2013/08/21 use MIGRATE(R%) instead of migrater(R%) so that ECON model uses modified migration rate
                '       POPR(R%) = (SCBR(R%) - SCDR(R%)) / 1000! * 100! + migrater(R%)
                POPR(R%) = (SCBR(R%) - SCDR(R%)) / 1000.0! * 100.0! + MIGRATE(R%)
                'If RegName$(R%) = "Italy" Then Stop
                POPR(R%) = Amin(7, AMAX(-2, POPR(R%)))
                'Note:  migrater for Kuwait history huge; must bound this
                ' Initial estimate; natural population only

                'jrs 2007/01/09 no double counting
                'If RegiontoSum(R%) Then
                WPOP = WPOP + POP(R%)
                'End If
                '        wpop1550 = wpop1550 + SPOP15to50(R%)

                'jrs 2010/04/23
                '       All years: Median Age of population
                '       ------------------------------------
                '       Half the population is younger, and half is older
                POPMEDAGE(R%) = 0
                For j = 1 To NCohorts
                    'Adding up population by 5-year age category
                    If POPMEDAGE(R%) + agedst(j%, R%, 1) + agedst(j%, R%, 2) < POP(R%) / 2 Then
                        POPMEDAGE(R%) = CSng(POPMEDAGE(R%) + agedst(j%, R%, 1) + agedst(j%, R%, 2))
                    Else
                        If j > 2 AndAlso j < NCohorts Then
                            'Adding up population by 1-year age category
                            For k = 5 To 1 Step -1
                                If POPMEDAGE(R%) + fagedst(j% * 5 - k, R%, 1) + fagedst(j% * 5 - k, R%, 2) < POP(R%) / 2 Then
                                    POPMEDAGE(R%) = CSng(POPMEDAGE(R%) + fagedst(j% * 5 - k, R%, 1) + fagedst(j% * 5 - k, R%, 2))
                                Else
                                    'Portion of the year that is missing to reach 50% of the population
                                    '(POP(R%) / 2 - POPMEDAGE(R%)) / (fagedst(j% * 5 - k, R%, 1) + fagedst(j% * 5 - k, R%, 2))
                                    'jrs 2026/03/05 added - 1, I was off by 1, Nigeria should be 17.57 intead of 18.57 in 2022
                                    POPMEDAGE(R%) = CSng(j * 5 - k - 1 + (POP(R%) / 2 - POPMEDAGE(R%)) / (fagedst(j% * 5 - k, R%, 1) + fagedst(j% * 5 - k, R%, 2)))
                                    Exit For
                                End If
                            Next k
                        End If
                        Exit For
                    End If
                Next j

            Next R%

            'BBH August 19, 2009 Begin

            '   All Years 1: Compute savings adjustment from population distribution
            '   --------------------------------------------------------------------
            Call SavingsDemogAdj()
            'BBH August 19, 2009 End

            '   All Years 1:  HIV Rate Calculation
            '   ------------------------------------------
            '   BBH 1/23/04 begin V5_0
            '   The ideal approach to HIV/AIDS would be more structural than the one implemented.
            '       It would track infected individuals, probably by age cohorts, but at least aggregate
            '       It would compute new infections annually, adding those to existing infected numbers,
            '       and then subtracting out those who died of AIDS and those who died of normal mortality
            '       That would link HIV infections and AIDS deaths much more closely
            '       But the data do not seem to exist to compute new infections; for instance, the HIV
            '       infection rates across time are not really comparable, because the measurement systems
            '       are crude and the 2001 values have sometimes been revised down from 1999, without
            '       there being revised and comparable 1999 values.  Thus even rates of increase or
            '       decrease in infection levels are crude.
            Dim hivgrrate As Single, hivratemul As Single, DecrementRate As Single

            If IY% > 1 Then
                For R% = 1 To NR%
                    'If GRPS(1, R%) = "South Africa" Then Stop
                    'sgh 2016/11/28
                    'Use "BaseYear" rather than a hard-coded year
                    '            If IY% <= 4 AndAlso 2005 + IY% <= hivpeakyr(R%) Then 'No intervention in growth
                    If IY% <= 4 AndAlso BaseYear + IY% <= hivpeakyr(R%) Then 'No intervention in growth
                        If SHIVRate(R%) < hivpeakr(R%) AndAlso hivincr(R%) < 1 Then
                            hivgrrate = 1
                        Else
                            hivgrrate = hivincr(R%)
                        End If
                        HIVRATE(R%) = SHIVRate(R%) * (1 + hivgrrate / 100) '(1 + hivincr(R%) / 100)
                        HIVRATE(R%) = Amin(HIVRATE(R%), AMAX(0, hivpeakr(R%)))
                        '            ElseIf IY% > 4 AndAlso 2005 + 4 + IY% <= hivpeakyr(R%) Then ' Still increasing
                        'Use "BaseYear" rather than a hard-coded year
                    ElseIf IY% > 4 AndAlso BaseYear + IY% <= hivpeakyr(R%) Then ' Still increasing
                        '                Call XYTABL("GDP/Capita (PPP) Versus HIV, Maximum Percent of Pop", _
                        '                    sgdppcp(R%) * (1 + igdpr(R%)), maxhivrate(R%))
                        'sgh 2016/11/28
                        'Use "BaseYear" rather than a hard-coded year
                        '                hivratemul = (hivpeakyr(R%) - (2005 + 4 + IY%)) / (hivpeakyr(R%) - (2005 + 4))
                        hivratemul = (hivpeakyr(R%) - (BaseYear + IY%)) / (hivpeakyr(R%) - (BaseYear))
                        If SHIVRate(R%) < hivpeakr(R%) AndAlso hivincr(R%) < 1 Then
                            hivgrrate = 1
                        Else
                            hivgrrate = hivincr(R%)
                        End If
                        HIVRATE(R%) = SHIVRate(R%) * (1 + hivgrrate * hivratemul / 100 * hivm(R%))
                        '                HIVRATE(R%) = SHIVRate(R%) * (1 + hivincr(R%) * hivratemul / 100 * hivm(R%))
                        '                HIVRATE(R%) = Amin(Amin(40, maxhivrate(R%) * hivmdcm), HIVRATE(R%))
                        HIVRATE(R%) = Amin(HIVRATE(R%), AMAX(0, hivpeakr(R%)))
                    Else 'Epidemic moving into control years
                        '                PhaseOutInitialDecrement = SHIVTECCNTLRegI(R%) * AMAX(0, (15 - IY%) / 15)
                        '                DecrementRate = (hivtadvr / 100) * IY% + PhaseOutInitialDecrement
                        'sgh 2016/11/28
                        'Make sure that the decrement begins with 1 after peak year is reached
                        If (BaseYear + IY%) > hivpeakyr(R%) Then
                            DecrementRate = (hivtadvr / 100) * ((BaseYear + IY%) - hivpeakyr(R%))
                        Else
                            DecrementRate = (hivtadvr / 100) * IY%
                        End If
                        HIVRATE(R%) = SHIVRate(R%) * AMAX(0, 1 - DecrementRate)

                        '                HIVTECCNTLReg(R%) = SHIVTECCNTLReg(R%) + hivtadvr / 100
                        '                SHIVTECCNTLReg(R%) = HIVTECCNTLReg(R%)
                        '                HIVRATE(R%) = SHIVRate(R%) * AMAX(0, 1 - HIVTECCNTLReg(R%))
                    End If
                    SHIVRate(R%) = HIVRATE(R%)
                    '   BBH 6/21/04 begin V5_0
                    For ICount% = 1 To 9
                        HIVRateL(R%, ICount%) = HIVRateL(R%, ICount% + 1)
                    Next ICount%
                    HIVRateL(R%, 10) = HIVRATE(R%)

                    'hivratechk = hivratechk + HIVRATE(R%) * SPOP15to50(R%)
                Next R%
                'hivratechk = hivratechk / wpop1550
            End If

            '   BBH 1/23/04 end V5_0


            '   All Years 1:  HIV Cases, AIDs Deaths, and AIDS Deaths not already in Mortality Data
            '   -----------------------------------------------------------------------------------
            For R% = 1 To NR%
                'HIVCASES(R%) = SPOP15to50(R%) * HIVRATE(R%) / 100
                HIVCASES(R%) = POP(R%) * HIVRATE(R%) / 100

                ' Note:  the inclusion of year 50 roughly represents cases at ages
                ' other than 15-49 (about 4% of the total cases are for children, other adults)
                '   BBH 1/23/04 begin V5_0
                ' Build in continued growth rate of AIDS deaths after infection peak as infected age
                '  Tuned to create life expectancy pattern forecast by UN for South Africa
                '        CurYear = IY% + 2000
                '        If CurYear < hivpeakyr(R%) Then
                '            aidsdr = AIDSDRATE(R%) / 100
                '        ElseIf CurYear >= hivpeakyr(R%) AndAlso CurYear < hivpeakyr(R%) + 10 Then
                '            aidsdr = AIDSDRATE(R%) / 100 + (CurYear - hivpeakyr(R%)) / 10 * 0.04
                '        ElseIf CurYear >= hivpeakyr(R%) + 10 AndAlso CurYear < hivpeakyr(R%) + 25 Then
                '             aidsdr = AIDSDRATE(R%) / 100 + 0.04 - (CurYear - (hivpeakyr(R%) + 10)) / 15 * 0.04
                '        Else
                '            aidsdr = AIDSDRATE(R%) / 100
                '        End If

                '   BBH 1/23/04 end V5_0
                If IY% = 1 Then
                    'bbh June 23, 2009 begin
                    'jrs 2009/10/20 moving logic to preprocessor when finding nulls
                    '            If AIDSDTHS(R%) <= 0.0001 Then 'bbh 2009/10/19 added =
                    '                AIDSDTHS(R%) = 0.1 * HIVRATE(R%) / 100 * POP(R%)
                    '            End If
                    'bbh June 23, 2009 end
                    AIDSDthRateI(R%) = AIDSDTHS(R%) / POP(R%)
                    AIDSDRATE(R%) = AIDSDthRateI(R%) * 100
                    SAIDSDRATE(R%) = AIDSDRATE(R%)
                    incaidsdths(R%) = 0
                    HIVRateL(R%, 10) = HIVRATE(R%)
                    'Assume that build-up to current rates was over 10 years
                    If hivincr(R%) <= 0 Then 'rates have been falling
                        HIVRateL(R%, 9) = HIVRateL(R%, 10) * (1 - hivincr(R%) / 100)
                        HIVRateL(R%, 8) = HIVRateL(R%, 9) * (1 - hivincr(R%) / 100)
                        For ICount% = 7 To 1 Step -1
                            HIVRateL(R%, ICount%) = CSng(HIVRateL(R%, ICount% + 1) * 1.1)
                        Next ICount%
                    Else  'rates have been rising
                        For ICount% = 9 To 1 Step -1
                            HIVRateL(R%, ICount%) = CSng(HIVRateL(R%, ICount% + 1) * 0.9)
                        Next ICount%
                    End If
                    HIVRateMAvg(R%) = 0
                    For ICount% = 1 To 10
                        HIVRateMAvg(R%) = HIVRateMAvg(R%) + HIVRateL(R%, ICount%)
                    Next ICount%
                    HIVRateMAvg(R%) = HIVRateMAvg(R%) / 10
                    'sgh 2016/11/28 - no longer use the moving average of prevalence
                    HIVAIDSRI(R%) = AMAX(3, Amin(1000, HIVRATE(R%) / AMAX(0.001, AIDSDRATE(R%))))
                    '            HIVAIDSRI(R%) = AMAX(3, Amin(1000, HIVRateMAvg(R%) / AMAX(0.001, AIDSDRATE(R%))))
                    'sgh 2016/11/28
                    'Note:  UNAIDS says survival time increasing from 12 years without treatment to
                    '   27.8 with treatment and treatment rising to 50-70 percent by 2015

                    'If RegName$(R%) = "Kenya" Then Stop
                Else
                    HIVRateMAvg(R%) = 0
                    For ICount% = 1 To 10
                        HIVRateMAvg(R%) = HIVRateMAvg(R%) + HIVRateL(R%, ICount%)
                    Next ICount%
                    HIVRateMAvg(R%) = HIVRateMAvg(R%) / 10
                    'sgh 2016/11/28 - no longer use the moving average
                    AIDSDRATE(R%) = CSng(HIVRATE(R%) / HIVAIDSRI(R%) * aidsdratem(R%) * (1 - aidsdrtadvr / 100) ^ (IY%))
                    '            AIDSDRATE(R%) = HIVRateMAvg(R%) / HIVAIDSRI(R%) * aidsdratem(R%) * (1 - aidsdrtadvr / 100) ^ IY%
                    'sgh 2016/11/28
                    '            AIDSDRATE(R%) = ConvergeOverTime(SAIDSDRATE(R%), HIVRateL(R%, 1), 10) _
                    '* aidsdratem(R%) * (1 - aidsdrtadvr / 100)
                    'Base, Target, Years
                    'If RegName$(R%) = "Kenya" Then Stop
                    SAIDSDRATE(R%) = AIDSDRATE(R%)
                    AIDSDTHS(R%) = POP(R%) * AIDSDRATE(R%) / 100

                    incaidsdths(R%) = AIDSDTHS(R%) - AIDSDthRateI(R%) * POP(R%)
                End If
                'If RegName$(R%) = "Kenya" Then Stop
                SAIDSDTHSCM(R%) = SAIDSDTHSCM(R%) + AIDSDTHS(R%)
                AIDSDTHSCM(R%) = SAIDSDTHSCM(R%)

                If incaidsdths(R%) < 0 Then 'Bring countries with reductions back to basic
                    ' mortality before convergence with curve;
                    ' else can result in very high LifExp for countries like Botswana
                    incaidsdths(R%) = ConvergeOverTime(incaidsdths(R%), 0, mortconv / 2)
                    'Base, Target, Years
                End If

                'Note that UN survivor tables from 1998 projection already represent
                'AIDS impact in the middle ages and IFs uses those survivor tables;
                'thus it is real incremental AIDS deaths that should be passed through

                'Note also that the UN apparently runs their model in five-year increments;
                'thus it is not impossible that there are some problems with their approach

            Next R%
            '   BBH 6/21/04 end V5_0

            '   All Years 1: Migration
            '   ----------------------
            '    Call Migration
            Dim Yth1 As Double, Yth2 As Double, workercon As Single
            Dim RetireYearsAdjNeed As Single, RetireYearsAdjActual As Single, RetireYearsAdj As Single
            Dim HHIntensity As Single, HHSizeCalc As Single

            For R% = 1 To NR%
                'jrs 2013/04/12 POP15TO65(R%) = SPOP15TO65(R%)
                'This line was not doing anything, SPOP15TO65 is empty, and it's never filled

                '   All Years 1: Nutrition Needs
                '       CLNEED is calculated for all regions; can be altered by CLNF
                '       CLAVAL in first year is from data; thereafter calculated in Agriculture
                '   ---------------------------------------------------------------------------

                CLNEED(R%) = 0!
                If IY% > 1 Then POP(R%) = 0!
                For j% = 1 To NCohorts
                    If IY% > 1 Then POP(R%) = CSng(POP(R%) + agedst(j%, R%, 1) + agedst(j%, R%, 2))
                    'jrs 2012/09/03
                    'If Not IsNull(clage(j%)) Then
                    CLNEED(R%) = CSng(CLNEED(R%) +
                    (agedst(j%, R%, 1) + agedst(j%, R%, 2)) * clage(j%) * clnf(R%))
                    'jrs 2012/10/30
                    'Else
                    '    j% = NCohorts
                    'End If
                Next j%
                'jrs 2017/11/06
                If ifsexomodelid > 0 AndAlso IY% > 1 Then
                    ReplaceVarWithExogenousForecastxReg("POP", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                'If RegName$(R%) = "Israel" Then Stop
                CLNEED(R%) = CLNEED(R%) * 1000
                '   BBH 1/25/04 Begin V5_0
                If IY% = 1 Then
                    If CLAVAL(R%) < CLNEED(R%) Then
                        'CLAVAL(R%) = CLNEED(R%)   'Assure nearly adequate cal 1st year
                        CalIAdj(R%) = CLNEED(R%) / CLAVAL(R%)
                    Else
                        CalIAdj(R%) = 1
                    End If
                End If
                '   BBH 1/25/04 End V5_0

                '   All Years 1: Dependent Population (<= 15 and >65), AIDS Adults (>15 and <50)
                '   ----------------------------------------------------------------------------
                POPLE15(R%) = 0
                POPGT65(R%) = 0
                YTHBULGE(R%) = 0
                POTSUPRAT(R%) = 0
                Yth1 = 0
                Yth2 = 0
                '        POPGT50 = 0

                For j% = 1 To 3
                    POPLE15(R%) = CSng(POPLE15(R%) + agedst(j%, R%, 1) + agedst(j%, R%, 2))
                Next j%
                For j% = 14 To NCohorts
                    POPGT65(R%) = CSng(POPGT65(R%) + agedst(j%, R%, 1) + agedst(j%, R%, 2))
                Next j%
                POP15TO65(R%) = POP(R%) - POPLE15(R%) - POPGT65(R%)

                'jrs 2018/03/01
                SPOPLE15(R%) = POPLE15(R%)
                sPOP15TO65(R%) = POP15TO65(R%)

                'jrs 2013/04/12
                'Adding flexible retirenment age and working entry age
                '-----------------------------------------------------
                'Call XYTABL("Age Versus Differential Marginal Propensity to Consume", _
                '(workageentry(R%) + workageretire(R%)) / 2, workercon) 'bbh 2014/03/02

                'If RegName$(R%) = "United States of America" Then Stop

                'jrs 2013/04/30 don't allow changing parameters in first year
                If IY% > 1 Then
                    'jrs 2013/10/25 workageretire(R%) = sworkageretire(R%) 'bbh 2013/08/1
                    'bbh 2014/03/02 start
                    If workageretirendpctuse(R%) > 0 Then workageretire(R%) = CSng(sworkageretire(R%)) 'bbh 2014/03/02
                    'bbh 2022/10/17 changed function reference to Old; is needed Absolute, not Differential
                    Call XYTABL("Age Versus Differential Marginal Propensity to Consume Old",
                    (workageentry(R%) + workageretire(R%)) / 2, workercon, context)
                    'bbh 2014/03/02 end

                    POPPREWORK(R%) = CSng(agedst(1, R%, 1) + agedst(1, R%, 2))
                    POPRETIRED(R%) = 0
                    For j% = 5 To CInt(workageentry(R%)) - 1
                        POPPREWORK(R%) = CSng(POPPREWORK(R%) + fagedst(j%, R%, 1) + fagedst(j%, R%, 2))
                    Next j%
                    'jrs 2013/05/27 added savings discount rate
                    'jrs 2013/05/07
                    WORKAGERETIREND(R%) = RetirementAgeToPreserveSavingsRate(AMAX(SLIFEXP(R%, Total), 85),
                    100 - workercon, CSng(workageentry(R%)), savingsdiscountrt(R%))
                    'bbh 2014/06/18 start
                    If workageretirendpctuse(R%) > 0 Then
                        'If RegName$(R%) = "United States of America" Then Stop
                        'Find Savings needed at needed retirement age
                        'Annuity of 20 during working years PV = C * [(1-(1+i)^-n)/i]
                        '                SavingsNew(R%) = 20 * ((1 - (1 + savingsdiscountrt(R%) / 100) ^ (workageentry(R%) - WORKAGERETIREND(R%))) / _
                        '                    (savingsdiscountrt(R%) / 100))
                        '                'Find current and original Savings
                        '                SavingsCurrent = 20 * ((1 - (1 + savingsdiscountrt(R%) / 100) ^ (workageentry(R%) - workageretire(R%))) / _
                        '                    (savingsdiscountrt(R%) / 100))
                        '                SavingsOld(R%) = 20 * ((1 - (1 + savingsdiscountrt(R%) / 100) ^ (workageentry(R%) - WorkAgeRetireI(R%))) / _
                        '                    (savingsdiscountrt(R%) / 100))
                        '                SavingsNeeded(R%) = SavingsNew(R%) - SavingsOld(R%)
                        '                SavingsCoveredRatio = (SavingsCurrent - SavingsOld(R%)) / AMAX(0.001, SavingsNeeded(R%)) * 100
                        '                SavingsAdj = workageretirendpctuse(R%) - SavingsCoveredRatio
                        '                RetireYearsAdj = (WORKAGERETIREND(R%) - WorkAgeRetireI(R%)) * SavingsAdj / 100 / 2 '/2 to avoid oscillation

                        RetireYearsAdjNeed = (WORKAGERETIREND(R%) - WorkAgeRetireI(R%)) * workageretirendpctuse(R%) / 100
                        RetireYearsAdjActual = workageretire(R%) - WorkAgeRetireI(R%)
                        RetireYearsAdj = (RetireYearsAdjNeed - RetireYearsAdjActual) / 2 '/2 to avoid oscillation
                        If RetireYearsAdj < 0 Then RetireYearsAdj = RetireYearsAdj / 10 'Greatly slow downside adjust

                        workageretire(R%) = workageretire(R%) + RetireYearsAdj
                        sworkageretire(R%) = workageretire(R%)

                        ''                'Find Savings needed at needed retirement age
                        ''                'Annuity of 20 during working years PV = C * [(1-(1+i)^-n)/i]
                        '                SavingsNew(R%) = 20 * ((1 - (1 + savingsdiscountrt(R%) / 100) ^ (workageentry(R%) - WORKAGERETIREND(R%))) / _
                        '                    (savingsdiscountrt(R%) / 100))
                        '                'Find current Savings
                        '                SavingsOld(R%) = 20 * ((1 - (1 + savingsdiscountrt(R%) / 100) ^ (workageentry(R%) - workageretire(R%))) / _
                        '                    (savingsdiscountrt(R%) / 100))

                        '                'Find hole in $
                        '                SavingsNeeded(R%) = SavingsNew(R%) - SavingsOld(R%)
                        '                'Find retirement age to fill in percentage of hole
                        '                'n = - ln(C/i - PV)/ln(1+i)
                        '                'bbh 2013/08/05 start
                        '                    'The original line below was blowing up in 2011 with workageretirendpctuse=100 for afghan (more generally also)
                        '                    '   Second term of log in numerator was provding a very large negative number and blowing up log calc
                        '                'WorkYearsNew = -Log((100 - workercon) / savingsdiscountrt(R%) / 100 - _
                        '                    (SavingsOld(R%) + SavingsNeeded(R%) * workageretirendpctuse(R%))) / Log(1 + savingsdiscountrt(R%) / 100)
                        '                'bbh tried replacing the above with the version below, which seems to better convert percent to decimal
                        '                '   But it produced a very large negative WorkYearsNew for Afghan in 2011
                        '                ' WorkYearsNew = -Log((100 - workercon) / (savingsdiscountrt(R%) / 100) - _
                        '                    (SavingsOld(R%) + SavingsNeeded(R%) * (workageretirendpctuse(R%) / 100))) / Log(1 + savingsdiscountrt(R%) / 100)
                        '                'bbh replaced line below with new lines followoing it
                        '                'workageretire(R%) = Round(WorkAgeEntry(R%) + WorkYearsNew, 0)
                        '                WorkLifeNeeded = WORKAGERETIREND(R%) - WorkAgeEntry(R%)
                        '                WorkLifeNow = sworkageretire(R%) - WorkAgeEntry(R%)
                        '                WorkLifeDif = WorkLifeNeeded - WorkLifeNow
                        '                workageretire(R%) = WorkAgeEntry(R%) + WorkLifeNow + (WorkLifeDif * workageretirendpctuse(R%) / 100) / 2
                        '                    '1/2 to avoid oscillation
                        '                workageretire(R%) = AMAX(workageretire(R%), WorkAgeEntry(R%) + 5)
                        '                workageretire(R%) = AMAX(workageretire(R%), 65) 'kludge line until analyze why drops with lifexp 200
                        '                sworkageretire(R%) = workageretire(R%)
                        '                'bbh 2013/08/05 end
                    End If
                    'bbh 2014/06/19 end
                    For j% = CInt(workageretire(R%)) To NAges
                        POPRETIRED(R%) = CSng(POPRETIRED(R%) + fagedst(j%, R%, 1) + fagedst(j%, R%, 2))
                    Next j%
                Else 'First year
                    POPPREWORK(R%) = 0
                    POPRETIRED(R%) = 0
                    For j% = 1 To 3
                        POPPREWORK(R%) = CSng(POPPREWORK(R%) + agedst(j%, R%, 1) + agedst(j%, R%, 2))
                    Next j%
                    For j% = 14 To NCohorts
                        POPRETIRED(R%) = CSng(POPRETIRED(R%) + agedst(j%, R%, 1) + agedst(j%, R%, 2))
                    Next j%
                    'bbh 2014/03/02 start
                    'bbh 2022/10/17 changed function reference to Old; is needed Absolute, not Differential
                    Call XYTABL("Age Versus Differential Marginal Propensity to Consume Old",
                    (workageentry(R%) + workageretire(R%)) / 2, workercon, context)
                    'bbh 2014/03/02 end

                    'jrs 2013/05/27 using savings discount rate
                    'jrs 2013/05/07
                    WORKAGERETIREND(R%) = RetirementAgeToPreserveSavingsRate(AMAX(LIFEXP(R%, 3), 85), 100 - workercon,
                    CSng(workageentry(R%)), savingsdiscountrt(R%))
                    sworkageretire(R%) = workageretire(R%) 'bbh 2013/08/05
                    WorkAgeRetireI(R%) = workageretire(R%) 'bbh 2014/06/18
                End If
                POPPREWORK(R%) = CSng(Math.Round(POPPREWORK(R%), 6))
                POPRETIRED(R%) = CSng(Math.Round(POPRETIRED(R%), 6))
                POPWORKING(R%) = CSng(Math.Round(POP(R%) - POPPREWORK(R%) - POPRETIRED(R%), 6))

                '        For j% = 11 To NCohorts
                '            POPGT50 = POPGT50 + agedst(j%, R%, 1) + agedst(j%, R%, 2)
                '        Next j%
                '        SPOP15to50(R%) = POP(R%) - POPLE15(R%) - POPGT50

                For j% = 4 To 6
                    Yth1 = Yth1 + agedst(j%, R%, 1) + agedst(j%, R%, 2)
                Next j%
                '   BBH 2/2/04 V5_0
                For j% = 4 To NCohorts  '7 To 11
                    Yth2 = Yth2 + agedst(j%, R%, 1) + agedst(j%, R%, 2)
                Next j%
                YTHBULGE(R%) = CSng(Yth1 / AMAX(Yth2, 0.001))
                'jrs 2013/04/12 POTSUPRAT(R%) = POP15TO65(R%) / AMAX(POPGT65(R%), 0.001)
                POTSUPRAT(R%) = CSng(POPWORKING(R%) / AMAX(POPRETIRED(R%), 0.001))

                'bbh 2022/06/23 Moved block below into MigrationGross
                ''   All Years 1: Foreign Population
                ''   -------------------------------
                ''        If IY% = 1 Then
                ''            If Abs(POPFOREIGN(R%)) < Abs(10 * MIGRANTS(R%)) Then
                ''                POPFOREIGN(R%) = 10 * MIGRANTS(R%)
                ''            End If
                ''            SPopForeign(R%) = POPFOREIGN(R%)
                ''        End If
                'POPFOREIGN(R%) = SPopForeign(R%)
                ''SPopForeign(R%) = SPopForeign(R%) * (1 - SCDR(R%) / 1000) + MIGRANTS(R%)
                'SPopForeign(R%) = CSng(SPopForeign(R%) * (1 - 15 / 1000) + MIGRANTS(R%))
                ''If use variable death rates, world sum will not be 0
                ''bbh 2020/03/09 start
                'POPFOREIGNIN(R%) = SPopForeignIn(R%)
                'POPFOREIGNOUT(R%) = SPopForeignOut(R%)
                'SPopForeignIn(R%) = CSng(SPopForeignIn(R%) * (1 - 15 / 1000) + MIGRANTSIN(R%))
                'SPopForeignOut(R%) = CSng(SPopForeignOut(R%) * (1 - 15 / 1000) + MIGRANTSOUT(R%))
                ''bbh 2020/03/09 end

                'mti HH Size for Infrastructure August 2010 start
                '   Household Size: Needs POP and POPLE15
                '   ----------------------------------------------------------------------
                If IY% > 1 Then
                    'jrs 2013/04/12 *** begin ***
                    '            If frm_MainMenu.IFsGUIctrl1.cHistoricBase = True Then
                    '                 Call XYTABL("Population Age Ratio Versus Households Intensity (1960)", _
                    '                            100 * POPLE15(R%) / AMAX(0.000001, POP(R%)), HHIntensity)
                    '            Else
                    '                 Call XYTABL("Population Age Ratio Versus Households Intensity (MOSTRECENT)", _
                    '                            100 * POPLE15(R%) / AMAX(0.000001, POP(R%)), HHIntensity)
                    '                 'HHSizeShift(R%) = HHSIZE(R%) - HHSizeCalc
                    '            End If
                    If context.HistoricMode = True Then
                        Call XYTABL("Population Age Ratio Versus Households Intensity (1960)",
                            100 * POPPREWORK(R%) / AMAX(0.000001, POP(R%)), HHIntensity, context)
                    Else
                        Call XYTABL("Population Age Ratio Versus Households Intensity (MOSTRECENT)",
                            100 * POPPREWORK(R%) / AMAX(0.000001, POP(R%)), HHIntensity, context)
                    End If
                    'jrs 2013/04/12 *** end ***
                    HHSizeCalc = 1 / AMAX(0.000001, HHIntensity)
                    'mti August 17, 2011 -start
                    'HHSIZE(R%) = Amin(10, AMAX(1, HHSizeCalc + ConvergeOverTime(HHSizeShift(R%), 0, 50)))
                    HHSIZE(R%) = Amin(10, AMAX(1, HHSizeCalc + ConvergeOverTime(CSng(HHSizeShift(R%)), 0, 100)))
                    'mti August 17, 2011 -end

                    'mti April 05, 2012 - start
                    'extrapolative approach
                    HHSizeGR(R%) = AMAX(-0.05F, Amin(0.05F, 0.2F * (HHSIZE(R%) / sHHSIZE(R%) - 1.0F) + 0.8F * HHSizeGR(R%)))
                    HHSIZE(R%) = CSng(Amin(10, AMAX(1, sHHSIZE(R%) * (1 + HHSizeGR(R%)))))
                    sHHSIZE(R%) = HHSIZE(R%)
                    'mti April 05, 2012 - end

                    HHSIZE(R%) = Amin(10, AMAX(1, HHSIZE(R%) * hhsizem(R%)))

                    'mti April 05, 2012 - start
                    HHTOTAL(R%) = POP(R%) / HHSIZE(R%)
                    'mti April 05, 2012 - end
                End If
                'mti HH Size for Infrastructure August 2010 end

            Next R%

        End Sub

        'jrs 2022/01/25
        Public Sub IFsPOPSpreadAgeDst(R As Integer, context As IForecastContext)
            '   First Year:  Spread Age Distribution Across Years - Full Distribution
            '   ---------------------------------------------------------------------

            Dim Sum As Double, MulFage As Double, Sum2 As Double
            For j% = 1 To NCohorts
                If j% < NCohorts Then
                    'For j2% = -5 To -1
                    'For gend% = 1 To 2
                    'fagedst(j% * 5 + j2%, R%, gend%) = agedst(j%, R%, gend%) / 5
                    'Next gend%
                    'Next j2%
                    For gend% = 1 To 2 'spline function spread of population across years
                        'mti September 2006 Start
                        '                    NextCohortAvg = agedst(j% + 1, R%, gend%) / 5
                        '                    CohortAvg = agedst(j%, R%, gend%) / 5
                        '                    Border = (NextCohortAvg + CohortAvg) / 2
                        'fagedst(j% * 5 - 5, R%, gend%) = AMAX(0, CohortAvg + (CohortAvg - Border))

                        'jrs 2025/01/22 data previously loaded in forecast mode, but only for the first year
                        If IY > 1 Or context.HistoricMode Or (fagedst(j% * 5 - 5, R%, gend%) = 0.001 And fagedst(j% * 5 - 1, R%, gend%) = 0.001) Then
                            fagedst(j% * 5 - 5, R%, gend%) = AMAX(0, Pop_AgeYear(j% * 5 - 5, R%, gend%))

                            'fagedst(j% * 5 - 4, R%, gend%) = AMAX(0, CohortAvg + (CohortAvg - Border) / 2)
                            fagedst(j% * 5 - 4, R%, gend%) = AMAX(0, Pop_AgeYear(j% * 5 - 4, R%, gend%))

                            '                    fagedst(j% * 5 - 3, R%, gend%) = CohortAvg
                            fagedst(j% * 5 - 3, R%, gend%) = AMAX(0, Pop_AgeYear(j% * 5 - 3, R%, gend%))

                            '                    fagedst(j% * 5 - 2, R%, gend%) = AMAX(0, CohortAvg + (Border - CohortAvg) / 2)
                            fagedst(j% * 5 - 2, R%, gend%) = AMAX(0, Pop_AgeYear(j% * 5 - 2, R%, gend%))

                            '                    fagedst(j% * 5 - 1, R%, gend%) = AMAX(0, CohortAvg + (Border - CohortAvg))
                            fagedst(j% * 5 - 1, R%, gend%) = AMAX(0, Pop_AgeYear(j% * 5 - 1, R%, gend%))
                        End If

                        Sum = fagedst(j% * 5 - 5, R%, gend%) + fagedst(j% * 5 - 4, R%, gend%) +
                            fagedst(j% * 5 - 3, R%, gend%) + fagedst(j% * 5 - 2, R%, gend%) +
                            fagedst(j% * 5 - 1, R%, gend%)
                        'dsr 20160324 Change next line - models seems to see differences when there are none
                        If Math.Abs(Sum - agedst(j%, R%, gend%)) > 0.0001 Then
                            '                    If Sum <> agedst(j%, R%, gend%) Then
                            If agedst(j%, R%, gend%) > 0 AndAlso Sum > 0 Then
                                MulFage = agedst(j%, R%, gend%) / Sum
                                fagedst(j% * 5 - 5, R%, gend%) = fagedst(j% * 5 - 5, R%, gend%) * MulFage
                                fagedst(j% * 5 - 4, R%, gend%) = fagedst(j% * 5 - 4, R%, gend%) * MulFage
                                fagedst(j% * 5 - 3, R%, gend%) = fagedst(j% * 5 - 3, R%, gend%) * MulFage
                                fagedst(j% * 5 - 2, R%, gend%) = fagedst(j% * 5 - 2, R%, gend%) * MulFage
                                fagedst(j% * 5 - 1, R%, gend%) = fagedst(j% * 5 - 1, R%, gend%) * MulFage
                                Sum2 = fagedst(j% * 5 - 5, R%, gend%) + fagedst(j% * 5 - 4, R%, gend%) +
                                    fagedst(j% * 5 - 3, R%, gend%) + fagedst(j% * 5 - 2, R%, gend%) +
                                    fagedst(j% * 5 - 1, R%, gend%)
                                'dsr 20160324 Change next line - models seems to see differences when there are none
                                If Math.Abs(Sum2 - agedst(j%, R%, gend%)) > 0.0001 AndAlso bShowMsg Then
                                    '                            If Sum2 <> agedst(j%, R%, gend%) AndAlso bShowMsg Then 'jrs 2008/04/02
                                    Dim msg As String
                                    msg$ = "There is an error in spreading populuation to 1-year cohorts"
                                    Debug.Print(msg$)
                                End If
                            End If
                        End If
                        '                    SumCohort5 = 0
                        '                    For j2% = -5 To -1
                        '                        SumCohort5 = SumCohort5 + fagedst(j% * 5 + j2%, R%, gend%)
                        '                    Next j2%
                        '                    For j2% = -5 To -1
                        '                        fagedst(j% * 5 + j2%, R%, gend%) = fagedst(j% * 5 + j2%, R%, gend%) * _
                        '                            agedst(j%, R%, gend%) / AMAX(0.0001, SumCohort5)
                        '                    Next j2%
                        'mti September 2006 end
                    Next gend%
                Else
                    For gend% = 1 To 2
                        fagedst(NAges, R%, gend%) = agedst(j%, R%, gend%) '>=100 Years of Age
                    Next gend%
                End If
            Next j%

        End Sub

        Sub Migration(context As IForecastContext)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            Dim SUMIM As Single, SUMEM As Single, AbsMigMax As Single, AbsPopDec As Single
            Dim WORLDIMEM As Single

            '   Normalize global migration to average of immigration, emigration
            '   ----------------------------------------------------------------

            SUMIM = 0
            SUMEM = 0

            'sgh dkb 2013/08/21 Change max immigration and pop decline from 3 to 4
            'AbsMigMax = AMAX(1, 3 - 0.1 * IY%) 'Drop max rate to 1% over 20 years
            'AbsPopDec = AMAX(0.5, 3 - 0.1 * IY%) 'Drop max pop decline with migration to .5% over 25 years

            'jrs 2020/06/12 Syria has a Net Migration rate of 6.26% negative, going out
            'AbsMigMax = AMAX(1S, CSng(4 - 0.1 * IY%)) 'Drop max rate to 1% over 30 years
            AbsMigMax = AMAX(1S, CSng(6 - 0.1 * IY%)) 'Drop max rate to 1% over 30 years
            AbsPopDec = AMAX(0.5, CSng(4 - 0.1 * IY%)) 'Drop max pop decline with migration to .5% over 35 years

            'dkb 2013/08/22 Change max imigration and pop decline to 6 (for WC migration targets)
            'AbsMigMax = AMAX(1, 6 - 0.1 * IY%) 'Drop max rate to 1% over 50 years
            'AbsPopDec = AMAX(0.5, 6 - 0.1 * IY%) 'Drop max pop decline with migration to .5% over 55 years

            For R% = 1 To NR%
                'jrs 2017/11/06
                If IY = 1 Then
                    smigrater(R%) = migrater(R%)
                End If
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("migrater", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If
                smigrater(R%) = migrater(R%)

                MIGRATE(R%) = migrater(R%) * wmigrm 'swap in exogenous rate
                If Math.Abs(MIGRATE(R%)) > AbsMigMax Then
                    If MIGRATE(R%) < 0 Then
                        MIGRATE(R%) = -AbsMigMax
                    Else
                        MIGRATE(R%) = AbsMigMax
                    End If
                End If
                'If RegName$(R%) = "Afghanistan" Then Stop
                ' jrs 2018/01/12 identified the next 3 lines as causing bad behavior in China; bbh removed
                '        If MIGRATE(R%) < 0 AndAlso MIGRATE(R%) + (SCBR(R%) - SCDR(R%)) / 1000 * 100 < -AbsPopDec Then
                '            MIGRATE(R%) = Amin(0, -AbsPopDec - (SCBR(R%) - SCDR(R%)) / 1000 * 100)
                '            'if do not bound, exporters become importers, potentially big ones (China)
                '        End If
                'If RegName$(R%) = "Afghanistan" Then Stop

                If MIGRATE(R%) > 0 Then
                    SUMIM = SUMIM + MIGRATE(R%) / 100 * POP(R%)
                Else
                    SUMEM = SUMEM - MIGRATE(R%) / 100 * POP(R%)
                End If
            Next R%
            WORLDIMEM = Amin(SUMEM, (SUMIM + SUMEM) / 2)
            'Bounding with Emigrants prevents "sucking" of people out of countries in low
            'fertility scenarios
            For R% = 1 To NR%
                If MIGRATE(R%) > 0 Then 'immigrants
                    MIGRANTS(R%) = MIGRATE(R%) / 100 * POP(R%) * WORLDIMEM / AMAX(0.001, SUMIM)
                Else 'emigrants
                    MIGRANTS(R%) = MIGRATE(R%) / 100 * POP(R%) * WORLDIMEM / AMAX(0.001, SUMEM)
                End If
                'jrs 2017/11/14
                If IY = 1 Then
                    smigrants(R%) = MIGRANTS(R%)
                End If
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("MIGRANTS", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If
                'smigrants(R%) = MIGRANTS(R%)

                MIGRATE(R%) = MIGRANTS(R%) / POP(R%) * 100
            Next R%

            MigrationDyadic(context)

            'jrs 2017/12/19
            For R% = 1 To NR%
                smigrants(R%) = MIGRANTS(R%)
            Next R%

        End Sub

        'bbh 2020/03/09 entire Sub
        Sub MigrationGross(context As IForecastContext)

            'gvdb 2024/04/05
            'unused variable
            Dim SUMEM As Single, SUMIM As Single ', WORLDDIMEM As Single
            Dim PopForInRatio As Single, ReturnOfImmigrants As Single 'bbh 2022/06/23 PopForIn As Single,
            Dim PopForInRatioMax As Single

            '   Normalize global migration to average of immigration, emigration
            '   ----------------------------------------------------------------
            SUMIM = 0
            SUMEM = 0
            'jrs 2020/04/22 Dim AbsMigMax As Single = CSng(AMAX(1, 4 - 0.1 * IY%)) 'Drop max rate to 1% over 30 years
            Dim AbsMigMax As Single = CSng(AMAX(MaxMIGRATEIN / 2, MaxMIGRATEIN - 0.1 * IY%))
            'jrs 2024/11/11
            'AbsMigMax = 20

            For R% = 1 To NR%
                'jrs 2024/11/13
                MIGRATEIN(R%) = migrateinr(R%)
                MIGRATEOUT(R%) = migrateoutr(R%)

                'jrs 2024/11/12
                If FirstYear + IY - 1 > 2100 Then
                    MIGRATEOUT(R%) = ConvergeOverTimeStartX(MIGRATEOUT(R%), 0, 2200 - FirstYear + 1, 2100 - FirstYear + 1)
                    MIGRATEIN(R%) = ConvergeOverTimeStartX(MIGRATEIN(R%), 0, 2200 - FirstYear + 1, 2100 - FirstYear + 1)
                End If
                'If GRPS(1, R%) = "Armenia" And IY = 171 Then Stop
                'jrs 2024/10/28 if POP has reached 1/20th of original population stop outward migration
                If POPI(R%) / POP(R%) >= migminpoplimit Then
                    MIGRATEOUT(R%) = 0
                ElseIf POPI(R%) / POP(R%) >= migminpoplimit * 0.75 Then 'jrs 2024/10/30 smoothing
                    'This was producing negatives for Albania in the 3rd year after this started
                    MIGRATEOUT(R%) = AMAX(MIGRATEOUT(R%) - MIGRATEOUT(R%) * (POPI(R%) / POP(R%) - migminpoplimit * 0.75) / (migminpoplimit * 0.25), 0)
                End If

                'jrs 2020/05/11
                If IY = 1 Then
                    SMigrateIn(R%) = MIGRATEIN(R%)
                    SMigrateOut(R%) = MIGRATEOUT(R%)
                End If
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("MIGRATEIN", "", "", CStr(FirstYear + IY - 1), R%, context)
                    ReplaceVarWithExogenousForecastxReg("MIGRATEOUT", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                SMigrateIn(R%) = MIGRATEIN(R%)
                SMigrateOut(R%) = MIGRATEOUT(R%)

                MIGRANTSIN(R%) = MIGRATEIN(R%) / 100 * POP(R%)
                MIGRANTSOUT(R%) = MIGRATEOUT(R%) / 100 * POP(R%)

                If IY > 1 Then
                    'If GRPS(1, R%) = "Ukraine" And IY = 7 Then Stop
                    'bbh 2020/04/11 multipliers added to 2 lines
                    MIGRANTSIN(R%) = MIGRANTSIN(R%) * wmigrm * migrateinm(R%) 'swap in exogenous rate
                    MIGRANTSOUT(R%) = MIGRANTSOUT(R%) * wmigrm * migrateoutm(R%)
                    'leave MIGRATEIN/OUT unchanged or create run-to-run; those are parameters

                    'jrs 2024/11/18
                    MIGRANTSIN(R%) = AMAX(MIGRANTSIN(R%) + migrateinadd(R%), 0)
                    MIGRANTSOUT(R%) = AMAX(MIGRANTSOUT(R%) + migrateoutadd(R%), 0)
                End If

                If MIGRANTSIN(R%) / POP(R%) > AbsMigMax / 100 Then
                    MIGRANTSIN(R%) = POP(R%) * AbsMigMax / 100
                End If

                If MIGRANTSOUT(R%) / POP(R%) > AbsMigMax / 100 Then
                    MIGRANTSOUT(R%) = POP(R%) * AbsMigMax / 100
                End If

                'jrs 2020/05/11
                If IY = 1 Then
                    SMigrantsIn(R%) = MIGRANTSIN(R%)
                    SMigrantsOut(R%) = MIGRANTSOUT(R%)
                    'jrs 2020/07/13
                    smigrants(R%) = MIGRANTS(R%)
                End If
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("MIGRANTSIN", "", "", CStr(FirstYear + IY - 1), R%, context)
                    ReplaceVarWithExogenousForecastxReg("MIGRANTSOUT", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If
                'bbh 2022/06/23 Start substantial revisions to long-term migration and pop foreign
                'First change was movement of All Years 1: Foreign Population block here as Foreign Population Analysis and Associated Changes in Mgiration
                'Then there was recognition that foreign populations in and out don't just change because of migration, but from deaths and that the rates of death
                'will reflect not some constant global average rate (like 15/1000) but cross-sectionally and temporally specific rates (SCDR), EVEN IF
                'global sums of in and out foreign populations will therefore not sum to same totals. Further changes were put in place to allow reductions of in
                'migration over time (post the first 20 years when the exogenous rates developed for IFs can be used unchanged) by setting targets for maximum
                'shares of country population that migrants constitute (e.g. 10%) and a convergence period over which those shares might be reached by control
                'of inward and outward migration through multipliers (MigInflowMul and MigOutFlowMul).

                '   Foreign Population Analyis and Associated Changes in Migration
                '   --------------------------------------------------------------

                '        If IY% = 1 Then
                '            If Abs(POPFOREIGN(R%)) < Abs(10 * MIGRANTS(R%)) Then
                '                POPFOREIGN(R%) = 10 * MIGRANTS(R%)
                '            End If
                '            SPopForeign(R%) = POPFOREIGN(R%)
                '        End If


                PopForInRatio = SPopForeignIn(R%) / POP(R%)
                If IY% = 20 Then PopForInRatioI(R%) = PopForInRatio 'Let first 20 years be fully determined by exogenous migration values
                If IY% >= 20 Then
                    PopForInRatioMax = Amin(PopForInRatio, ConvergeOverTimeStartX(PopForInRatioI(R%), CSng(Amin(0.1, PopForInRatioI(R%))), 50, 20)) 'replaced 90 with 65
                Else
                    PopForInRatioMax = 1
                End If
                Dim InitialCalcPopForeignIn = SPopForeignIn(R%) * (1 - SCDR(R%) / 1000) + MIGRANTSIN(R%) * MigInflowMul(R%) - MIGRANTSOUT(R%) * MigOutFlowMul(R%)
                'If RegName$(R%) = "United Arab Emirates" And IY% + 2016 >= 2060 Then Stop
                InitialCalcPopForeignIn = AMAX(0.0001, InitialCalcPopForeignIn) 'bbh 2023/01/05  bound divisor to be above 0
                PopForInRatioMax = AMAX(0.0001, PopForInRatioMax) 'bbh 2023/01/05  bound divisor to be above 0
                If (InitialCalcPopForeignIn / POP(R%) > PopForInRatioMax) And IY% >= 20 Then
                    InitialCalcPopForeignIn = AMAX(0.0001, InitialCalcPopForeignIn) 'bbh 2023/01/5  bound divisor to be above 0
                    PopForInRatioMax = AMAX(0.0001, PopForInRatioMax) 'bbh 2023/01/5  bound divisor to be above 0

                    MigInflowMul(R%) = CSng(MigInflowMul(R%) * (0.97 * PopForInRatioMax / (InitialCalcPopForeignIn / POP(R%))))
                    MigOutFlowMul(R%) = CSng(MigOutFlowMul(R%) * (1.02 * (InitialCalcPopForeignIn / POP(R%)) / PopForInRatioMax))
                End If

                MigInflowMul(R%) = 1  'These two lines added to override the calculation of multipliers that would move migration patterns so as to limit
                MigOutFlowMul(R%) = 1   'the share of migrant populations in countries near the end of century (focus on oil producing countries especialy)
                'Leave above code caluating non-1.0 values for them in case decide later to use it or variation of it.

                MIGRANTSIN(R%) = MIGRANTSIN(R%) * MigInflowMul(R%)
                ' If RegName$(R%) = "Kuwait" And IY% + 2016 >= 2020 Then Stop
                'If 0.15 * SPopForeignIn(R%) < MIGRANTSOUT(R%) * MigOutFlowMul(R%) Then Stop
                MIGRANTSOUT(R%) = MIGRANTSOUT(R%) * MigOutFlowMul(R%)
                'MIGRANTSOUT(R%) = Amin(0.15 * SPopForeignIn(R%), MIGRANTSOUT(R%) * MigOutFlowMul(R%))
                'MIGRANTSIN(R%) = AMAX(0, Amin(MIGRANTSIN(R%), PopForInRatioMax * POP(R%) - SPopForeignIn(R%) * (1 - SCDR(R%) / 1000) - MIGRANTSOUT(R%)))
                'If RegName$(R%) = "United Arab Emirates" And IY% + 2016 >= 2060 Then Stop
                'MIGRANTSOUT(R%) = CSng(Amin(MIGRANTSOUT(R%), POP(R%) * 0.1)) 'bbh 2024/03/22

                SUMIM = SUMIM + MIGRANTSIN(R%)
                SUMEM = SUMEM + MIGRANTSOUT(R%)
            Next R%

            Dim WORLDIMEM = Amin(SUMIM, SUMEM)  'Minimum instead of average does slow down migration globally over time
            'WORLDIMEM = (SUMIM + SUMEM) / 2

            For R% = 1 To NR%
                'If GRPS(1, R%) = "Armenia" And IY = 121 Then Stop
                MIGRANTSIN(R%) = MIGRANTSIN(R%) * WORLDIMEM / AMAX(0.001, SUMIM)
                MIGRANTSOUT(R%) = MIGRANTSOUT(R%) * WORLDIMEM / AMAX(0.001, SUMEM)
                MIGRANTS(R%) = MIGRANTSIN(R%) - MIGRANTSOUT(R%)
                MIGRATE(R%) = MIGRANTS(R%) / POP(R%) * 100
                'jrs 2024/11/11 this is needed or adjusments in headcount are not reflected in rates
                MIGRATEIN(R%) = MIGRANTSIN(R%) / POP(R%) * 100
                MIGRATEOUT(R%) = MIGRANTSOUT(R%) / POP(R%) * 100

                POPFOREIGNIN(R%) = SPopForeignIn(R%) 'Will be ok IY%=1, then pick up results of previous year's migation iy%>1
                POPFOREIGNOUT(R%) = SPopForeignOut(R%)
                POPFOREIGN(R%) = SPopForeign(R%)

                SPopForeignIn(R%) = POPFOREIGNIN(R%) * (1 - SCDR(R%) / 1000) + MIGRANTSIN(R%)
                PopForInRatio = SPopForeignIn(R%) / POP(R%)
                ReturnOfImmigrants = Amin(PopForInRatio * SPopForeignOut(R%), Amin(PopForInRatio * POP(R%), MIGRANTSOUT(R%) * SPopForeignIn(R%) / POP(R%)))
                'ReturnOfImmigrants = Amin(0.5 * SPopForeignOut(R%), Amin(0.5 * POP(R%), MIGRANTSOUT(R%) * SPopForeignIn(R%) / POP(R%)))
                'If RegName$(R%) = "Kuwait" And IY% + 2016 = 2090 Then Stop
                'bbh 2022/06/03 Computed and used this because outmigration should consist of native and foreign born; in the absence
                'of info about the relative propensity to leave (probably in many countries most are temporary workers going home),setting
                'proportion of returnees as proportion of foreign born, which is probably conservative in GCC and most outmigraton should be of former immigrants
                SPopForeignIn(R%) = SPopForeignIn(R%) - ReturnOfImmigrants
                SPopForeignOut(R%) = POPFOREIGNOUT(R%) * (1 - SCDR(R%) / 1000) + (MIGRANTSOUT(R%) - ReturnOfImmigrants)

                'SPopForeign(R%) = POPFOREIGN(R%) * (1 - SCDR(R%) / 1000) + MIGRANTS(R%)
                SPopForeign(R%) = SPopForeignIn(R%) - SPopForeignOut(R%)
                'bbh 2022/06/23 In DataEcon PopForeign is computed as the net of POPFOREIGNIN and POPFOREIGNOUT, so reasonable to do the same here
                'Not used for computation of remitances now and is tally of whether country is net recipient or sender of migrants
                'dsb 2026/02/23 Foreign born stocks from IFsFull.Dat file, so not using the calculation above for now, but leaving it in place in case want to use it in future or for sensitivity analysis
                POPFOREIGNIN(R%) = stockin(R%)
                POPFOREIGNOUT(R%) = stockout(R%)
                If migrateinm(R%) <> 1 Or migrateoutm(R%) <> 1 Then
                    POPFOREIGNIN(R%) = POPFOREIGNIN(R%) - SMigrantsIn(R%) * (1 - migrateinm(R%)) * (POPFOREIGNIN(R%) / POP(R%)) + SMigrantsOut(R%) * (1 - migrateoutm(R%)) * (POPFOREIGNOUT(R%) / POP(R%))
                    POPFOREIGNOUT(R%) = POPFOREIGNOUT(R%) - SMigrantsOut(R%) * (1 - migrateoutm(R%)) * (POPFOREIGNOUT(R%) / POP(R%)) + SMigrantsIn(R%) * (1 - migrateinm(R%)) * (stockin(R%) / POP(R%))
                End If
                POPFOREIGN(R%) = POPFOREIGNIN(R%) - POPFOREIGNOUT(R%)
                SPopForeign(R%) = POPFOREIGN(R%)
                SPopForeignIn(R%) = POPFOREIGNIN(R%)
                SPopForeignOut(R%) = POPFOREIGNOUT(R%)
            Next R%
            'bbh 2022/06/23 End substantial revisions to long-term migration and pop foreign

            MigrationDyadic(context)

            For R% = 1 To NR%
                SMigrantsIn(R%) = MIGRANTSIN(R%)
                SMigrantsOut(R%) = MIGRANTSOUT(R%)
                'dsb 2026/2/11
                MIGRANTS(R%) = MIGRANTSIN(R%) - MIGRANTSOUT(R%)
                MIGRATE(R%) = MIGRANTS(R%) / POP(R%) * 100
                'jrs 2020/07/13
                smigrants(R%) = MIGRANTS(R%)
            Next R%

        End Sub

        'dsb 2025/11/21
        Private Sub MigrationDyadic(cntx As IForecastContext)
            '    Select Case bilateralsw
            '        Case 1
            'MigrationDyadic_ForcedAndVoluntary(cntx)
            '        Case 2
            MigrationDyadic_ConflictBased(cntx)
            '        Case Else
            '            Exit Sub
            '    End Select
        End Sub

        ''dsb 2025/11/21 Sub without splitting into forced and voluntary and usign conflict based migration
        Private Sub MigrationDyadic_ConflictBased(cntx As IForecastContext)
            Try
                Dim NativeRatio As Single 'dsb 2026/1/29
                'dsb 2026/03/09
                Dim AbsMigMax As Single = CSng(AMAX(MaxMIGRATEIN / 2, MaxMIGRATEIN - 0.1 * IY%))
                If System.IO.File.Exists(PutFdrvRun$ + "IFsForDyadicWork.db") AndAlso (migratesw > 0 OrElse bilateralsw > 0) Then
                    '---------------------------------------------------------------
                    Dim z As Single
                    Dim i As Integer, j As Integer
                    'dsb 2026/1/29
                    Dim shiftfactorMonadic As Single
                    'dsb 2026/07/22 
                    Dim conflictindex As Single
                    'ReDim CONFLICTMAG(NR, NR)
                    'ReDim SCONFLICTMAG(NR, NR)
                    '===================================================================
                    'FIRST YEAR: (IY=1)  Load Dyadic Migration Data - Conflict Based
                    '===================================================================
                    If IY = 1 Then
                        ReDim XDYADMIG(NR, NR), SXDYADMIG(NR, NR)
                        ReDim GRAVITYDYADMIG(NR, NR), SGRAVITYDYADMIG(NR, NR), GRAVITYMIG(NR)
                        ReDim GRAVITYDYADMIGVAL(NR, NR), SGRAVITYDYADMIGVAL(NR, NR)
                        ReDim SHIFTGRAVITYDYADMIG(NR, NR)
                        ReDim DistanceBilat(NR, NR)

                        ReDim DYADPOPFOREIGN(NR, NR), SDYADPOPFOREIGN(NR, NR)
                        ReDim MIGIN(NR), MIGOUT(NR)
                        'ReDim SSFCIVILWARPOPIMP(NR)

                        'POPFORIEGN
                        ReDim migsharenativem(NR, NR), migdeathratesw(NR, NR)
                        ReDim limit4returningmigs(NR, NR)
                        ReDim SPopForeignIn(NR)
                        ReDim bPOPFOREIGNUPDATE(NR, NR)
                        ReDim MIGIN(NR), MIGOUT(NR)

                        ReDim alliancem(NR, NR)
                        ReDim tradeexportdyadm2(NR, NR, 5)
                        ReDim tradeexportdyadm(NR, NR, NSec)
                        ReDim tradecostgeneral(NR, NR, NSec)
                        ReDim tradeelastpriceexportsdyad(NR, NR, NSec)
                        ReDim igom(NR, NR)
                        ReDim allianceadd(NR, NR), loradd(NR, NR)
                        ReDim treatym(NR, NR)
                        ReDim dyadtradeagreementindexadd(NR, NR)
                        ReDim dyadmilmstockadd(NR, NR), xdyadaidadd(NR, NR)
                        ReDim dyadtariffratem(NR, NR, NSec)
                        ReDim dyadtariffrateadd(NR, NR, NSec)
                        ReDim dyadtradecomplementm(NR, NR, NSec)
                        ReDim migrantsmissingm(NR, NR)

                        ReDim AdjustFstYr(NR), AdjustBaseVal(NR)
                        ReDim DYADMIGFORRETLST0(NR, NR)
                        ReDim SPopForeignIn(NR)
                        ReDim dyadaidexoadd(NR, NR)
                        ReDim polrelevance(NR, NR), peaceyearset(NR, NR), peaceyearcountadj(NR, NR), dyadldrtripm(NR, NR)

                        ReDim DYADMIGFORCED(NR, NR), SDYADMIGFORCED(NR, NR), DYADMIGFORCEDRETURN(NR, NR), SDYADMIGFORCEDRETURN(NR, NR), DYADPOPFOREIGNFORCED(NR, NR), SDYADPOPFOREIGNFORCED(NR, NR)
                        ReDim SDYADMIGFORECEDRETURNEST(NR)
                        ReDim SharesForced(NR + 1, NR + 1), SSharesForced(NR + 1, NR + 1), GravityDyadForced(NR, NR), SGravityDyadForced(NR, NR), GravityForced(NR)
                        ReDim SharesFlowForced(NR + 1, NR + 1), SharesStockForced(NR + 1, NR + 1), SSharesFlowForced(NR + 1, NR + 1), SSharesStockForced(NR + 1, NR + 1)
                        ReDim DYADMIGVOL(NR, NR), SDYADMIGVOL(NR, NR), DYADPOPFOREIGNVOL(NR, NR), SDYADPOPFOREIGNVOL(NR, NR)
                        ReDim dyadmigforcedreturnadd(NR, NR)
                        ReDim IDYADMIGFORCEDTARGETPCTPOP(NR, NR), IDYADMIGVOLTARGETPCTPOP(NR, NR)
                        ReDim IDYADMIGFORCEDPCTPOP(NR, NR), IDYADMIGVOLPCTPOP(NR, NR)
                        ReDim dyadforcedreturnlimitm(NR, NR), VolTarget(NR, NR)
                        ReDim dyadmigvoltargetsw(NR, NR)
                        ReDim dyadforcedreturnsignal(NR, NR)
                        ReDim dyadmigrantforcedm(NR, NR)
                        'dsb 2026/1/16 
                        'ReDim XDYAD(NR, NR, NSec)
                        'DSB 2026/1/20 
                        ReDim MIGRANTSOUTFORECAST(NR), SMIGRANTSOUTFORECAST(NR), SMIGRANTSOUTN(NR)
                        'dsb 2026/01/30 
                        ReDim MigrantsOutAverage(NR)
                        'dsb 2026/02/16 
                        ReDim MigrantsBilatAverage(NR, NR)
                        'dsb 2026/04/13
                        ReDim Migrantsnetabove1percent(NR)
                        'dsb 2026/01/30 Last 10 years data to take the average for outward migration forecast. But the base year MigrantsOut does not take averaged value
                        'This average is only for trend smoothing in the forecast period
                        'Using db = New IFsHistSeriesContext()
                        '    Dim yearsOM As Short() = db.GetYears("SeriesOutwardMigrationAbel").OrderBy(Function(y) y).ToArray()
                        '    Dim yearsFields As String() = Enumerable.Range(FirstYear - 9, 10).Select(Function(y) y.ToString()).ToArray()
                        '    Dim parts() As String
                        '    Dim sumVal As Double? = 0, countYear As Integer = 0
                        '    Dim valuesOutMig As Single?()
                        '    Dim ic As Integer = 0
                        '    For i = 1 To NR
                        '        MigrantsOutAverage(i) = 0
                        '    Next
                        '    Dim rsOut = db.DiceSeries("SeriesOutwardMigrationAbel", yearsFields)
                        '    For Each rec In rsOut
                        '        ic += 1
                        '        valuesOutMig = rec.Value
                        '        sumVal = 0
                        '        countYear = 0
                        '        For y = 0 To valuesOutMig.Length - 1
                        '            If valuesOutMig(y).HasValue Then
                        '                sumVal += valuesOutMig(y)
                        '                countYear += 1
                        '            End If
                        '        Next
                        '        If countYear > 0 Then
                        '            MigrantsOutAverage(ic) = CSng(sumVal / countYear)
                        '        End If
                        '    Next

                        'End Using

                        'dsb 2026/02/16 Last 10 years average for bilateral forecasts. But the base year bilateral flows do not take averaged value. This is only for trend smoothing in the forecast period
                        Using db = New IFsHistSeriesDyadicContext()
                            Dim yearsBilat As Short() = db.GetYearsDyad("SeriesMigrationFlowOutDL").OrderBy(Function(y) y).ToArray()
                            'Dim yearsFields As String() = Enumerable.Range(FirstYear - 9, 10).Select(Function(y) y.ToString()).ToArray()
                            Dim yearsFields As String() = yearsBilat.Where(Function(y) y <= FirstYear).Select(Function(y) y.ToString()).ToArray()
                            Dim sumVal As Double? = 0, countYear As Integer = 0
                            Dim valuesBilat As Single?()
                            Dim actorName As String, partnerName As String
                            Dim ic As Integer = 0
                            For i = 1 To NR
                                For j = 1 To NR
                                    MigrantsBilatAverage(i, j) = 0
                                Next j
                                MigrantsOutAverage(i) = 0 'dsb 2026/04/01
                            Next i
                            Dim rsBilat = db.DiceSeries("SeriesMigrationFlowOutDL", yearsFields)
                            'Countries are not in order in the dyadic dataset, so need to have a look up by name to assign to the right place in the array
                            'country name -> index in GRPS
                            'Dim countryIndex As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
                            'For i = 1 To NR
                            '    countryIndex(GRPS(1, i)) = i
                            'Next
                            'dsb 2026/04/01 Map country name to index based on the REgionalization value (RegionId)
                            Dim countryIndexByRegionId As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
                            For Each rgn In cntx.Regionalization
                                countryIndexByRegionId(rgn.Key) = rgn.Value
                            Next
                            For Each rec In rsBilat
                                'Dim coun As String = GRPS(1, 2)
                                Dim parts = rec.Key.Split(";"c)
                                If parts.Length < 2 Then Continue For
                                actorName = rec.Key.Split(";"c)(0)
                                partnerName = rec.Key.Split(";"c)(1)
                                If String.IsNullOrEmpty(actorName) Or String.IsNullOrEmpty(partnerName) Then
                                    Continue For
                                End If
                                valuesBilat = rec.Value
                                sumVal = 0
                                countYear = 0
                                For y = 0 To valuesBilat.Length - 1
                                    If valuesBilat(y).HasValue Then
                                        sumVal += valuesBilat(y)
                                        countYear += 1
                                    End If
                                Next

                                If countYear = 0 Then Continue For

                                'If countryIndex.ContainsKey(actorName) AndAlso countryIndex.ContainsKey(partnerName) Then
                                '    MigrantsBilatAverage(countryIndex(actorName), countryIndex(partnerName)) = CSng(sumVal / countYear)
                                'End If

                                'dsb 2026/04/01 Bilateral averages
                                If countryIndexByRegionId.ContainsKey(actorName) AndAlso countryIndexByRegionId.ContainsKey(partnerName) Then
                                    MigrantsBilatAverage(countryIndexByRegionId(actorName), countryIndexByRegionId(partnerName)) = CSng(sumVal / countYear)
                                    'Add to the outward migration average for the actor country
                                    MigrantsOutAverage(countryIndexByRegionId(actorName)) += CSng(sumVal / countYear)
                                End If

                            Next
                        End Using

                        Using db = New IFsForDyadicContext()
                            db.Enter()
                            '----------------------------------------------------------------------
                            '1. Load DistanceBilat
                            '----------------------------------------------------------------------
                            Dim rsDistance = (From XD In db.Distance Where XD.Year = FirstYear).ToList
                            For Each rec As Distance In rsDistance
                                DistanceBilat(rec.Actor, rec.Partner) = rec.Val
                                DistanceBilat(rec.Partner, rec.Actor) = rec.Val
                            Next
                            '----------------------------------------------------------------------
                            '2. Load XDYADMIGRANT (Base year Flows)
                            '----------------------------------------------------------------------
                            Dim rsXDYADMIGRANT = (From XD In db.XDYADMIGRANT Where XD.Year = FirstYear).ToList
                            For Each rec As XDYADMIGRANT In rsXDYADMIGRANT
                                XDYADMIG(rec.Actor, rec.Partner) = rec.Val
                                MIGOUT(rec.Actor) = MIGOUT(rec.Actor) + rec.Val
                                MIGIN(rec.Partner) = MIGIN(rec.Partner) + rec.Val
                            Next
                            '----------------------------------------------------------------------
                            '3. Load DYADPOPFOREIGN (Base year Stocks) 
                            '----------------------------------------------------------------------
                            Dim rsDYADPOPFOREIGN = (From XD In db.DYADPOPFOREIGN Where XD.Year = FirstYear).ToList
                            For Each rec As DYADPOPFOREIGN In rsDYADPOPFOREIGN
                                DYADPOPFOREIGN(rec.Actor, rec.Partner) = rec.Val
                                SDYADPOPFOREIGN(rec.Actor, rec.Partner) = rec.Val
                            Next

                            '----------------------------------------------------------------------
                            LoadDefDyadPars()
                            For i = 1 To NR
                                For j = 1 To NR
                                    If migrantsmissingm(i, j) <> 1 Then
                                        XDYADMIG(i, j) = XDYADMIG(i, j) * migrantsmissingm(i, j)
                                    End If
                                Next j
                            Next i
                            For i = 1 To NR
                                SPopForeignIn(i) = 0
                                For j = 1 To NR
                                    SPopForeignIn(i) = SPopForeignIn(i) + SDYADPOPFOREIGN(j, i) / 1000000
                                Next j
                            Next i
                            '----------------------------------------------------------------------
                            'DSB 2026/01/15 *** start **** SFINSECUREPOPIMP
                            'Percent of Population Impacted calculated using OLS
                            'All drivers are lagged by one year
                            '---------------------------------------------------------------
                            Dim PredictionPOPIMP As Single
                            For i = 1 To NR
                                'If i = 184 Or i = 180 Then Stop
                                If sfcivilwarucdpsw = 1 Then 'dsb 2026/07/22 
                                    conflictindex = SFCIVILWARUCDP(i)
                                ElseIf sfcivilwarucdpsw = 2 Then
                                    conflictindex = SFINTLWARALL(i)
                                Else
                                    conflictindex = SFCIVILWARPARDEE(i)
                                End If
                                Call AnalFunc("State Failure Percent of Population Impacted (2026)",
                              New Single() {conflictindex, GOVEFFECT(i), LABPARR(i, Male), POP(i), DIPLOTREATY(i), SVDTHSOTHERINTERPERSON(i)}, PredictionPOPIMP,
                              New String() {"SFCIVILWARPARDEE", "GOVEFFECT", "LABPARR", "POP", "DIPLOTREATY", "SVDTHSOTHERINTERPERSON"}, cntx)
                                SFINSECUREPOPIMP(i) = PredictionPOPIMP
                                SFINSECUREPOPIMP(i) = AMAX(0, Amin(1, SFINSECUREPOPIMP(i)))
                            Next i
                            'DSB 2026/01/15 *** end ****
                            '----------------------------------------------------------------------
                            'dsb 2026/05/05 Civil war pop imp
                            For i = 1 To NR
                                If sfcivilwarucdpsw = 1 Then 'dsb 2026/07/22 
                                    conflictindex = SFCIVILWARUCDP(i)
                                ElseIf sfcivilwarucdpsw = 2 Then
                                    conflictindex = SFINTLWARALL(i)
                                Else
                                    conflictindex = SFCIVILWARPARDEE(i)
                                End If
                                Call AnalFunc("State Failure Percent of Population Impacted, Civil War (2026)",
                              New Single() {POP(i), conflictindex, SVDTHSWOMENCHILDREN(i), GOVEFFECT(i)}, PredictionPOPIMP,
                              New String() {"POP_Lag1", "SFCIVILWARPARDEE_Lag1", "SVDTHS_Lag1", "GOVEFFECT_Lag1"}, cntx)
                                SFCIVILWARPOPIMP(i) = PredictionPOPIMP
                                SFCIVILWARPOPIMP(i) = AMAX(0, Amin(1, SFCIVILWARPOPIMP(i)))
                            Next i
                            'DSB 2026/01/15 *** end ****
                            '----------------------------------------------------------------------
                            Dim squaredScale As Double = 1.0
                            If gdppcppeak <> 0 Then
                                Dim linearCoeff As Double = GetVariableCoefficient("Log of Emigration (Migrants out, percent of population) (2026)", "GDPPCP")
                                Dim squaredCoeff As Double = GetVariableCoefficient("Log of Emigration (Migrants out, percent of population) (2026)", "GDPPCPSQUARED")

                                If linearCoeff <> 0 AndAlso squaredCoeff <> 0 Then
                                    Dim targetGDPPCPCoeff As Double = -linearCoeff / (2 * Math.Log(gdppcppeak))
                                    squaredScale = targetGDPPCPCoeff / squaredCoeff
                                End If
                            End If
                            'dsb 2026/01/20 Calculating outward monadic migration flow (MIGRANTSOUT) 2026/1/21
                            '----------------------------------------------------------------------
                            For i = 1 To NR
                                MIGRANTSOUT(i) = MIGOUT(i)
                                SMIGRANTSOUTN(i) = MIGRANTSOUT(i)
                            Next
                            Dim PredVal As Single
                            For i = 1 To NR
                                'dsb 2026/06/08 GDPR comes in ratio 
                                Call AnalFunc("Log of Emigration (Migrants out, percent of population) (2026)", 'dsb 2026/1/27 New monadic regression model
                                              New Single() {POP(i), GDPR(i) * 100.0!, POVMIDHIGH(i) / POP(i) * 100, GDPPCP(i), CSng(squaredScale * Math.Log(If(GDPPCP(i) <= 0, 1, GDPPCP(i))) * Math.Log(If(GDPPCP(i) <= 0, 1, GDPPCP(i)))), (SFCIVILWARPOPIMP(i) * 100 + 1)}, PredVal,
                                              New String() {"POP", "GDPR", "POV685", "GDPPCP", "GDPPCPSQUARED", "SFPOPIMPPCT"}, cntx)
                                PredVal = CSng(Math.Exp(PredVal)) / 100 'Convert from percent of population to fraction of population
                                MIGRANTSOUTFORECAST(i) = PredVal * POP(i) * 1000000 'dsb 2026/1/27
                                'MIGRANTSOUTFORECAST(i) = MIGRANTSOUT(i) - MIGRANTSOUTFORECAST(i)  'dsb 2026/1/29 uncommented
                                MIGRANTSOUTFORECAST(i) = MigrantsOutAverage(i) - MIGRANTSOUTFORECAST(i)  'dsb 2026/1/30 Using average of last 10 years outward migration as base
                                SMIGRANTSOUTFORECAST(i) = MIGRANTSOUTFORECAST(i)
                            Next i
                            '----------------------------------------------------------------------
                            'Gravity Model for FIRST YEAR IY = 1
                            'USES: DISTANCE, POPULATION ORIGIN AND DESTINATION, GDPPCP ORIGIN AND DESTINATION, CONFLICT MAGNITUDE ORIGIN AND DESTINATION, MIGRATION STOCKS
                            '----------------------------------------------------------------------
                            Dim ValEst = 0!
                            For i = 1 To NR
                                GRAVITYMIG(i) = 0
                                For j = 1 To NR
                                    'If i = 1 And j = 180 Then Stop
                                    If i <> j AndAlso SDYADPOPFOREIGN(i, j) > 0 AndAlso SDYADPOPFOREIGN(j, i) > 0 Then 'dsb 2026/1/16
                                        'If i <> j Then 'dsb 2026/1/16
                                        'Call AnalFunc("Log of Emigration(Migration Flow from A to B) (2026)", 'dsb 2026/1/30 Mistakenly used GDPPCP, now using GDP
                                        '               New Single() {SFCIVILWARPOPIMP(i) * POP(i) + 1, GDPPCP(i), GDPPCP(j), POP(i), POP(j), DistanceBilat(i, j), SDYADPOPFOREIGN(i, j), SDYADPOPFOREIGN(j, i)}, ValEst,
                                        '               New String() {"SVCIVILWARPOPIMPABS_A", "GDPPCP_A", "GDPPCP_B", "POP_A", "POP_B", "DISTANCE", "MIGRANTSTOCKBILAT_A", "MIGRANTSTOCKBILAT_B"}, cntx)
                                        Call AnalFunc("Log of Emigration (Migration Flow from A to B), Insecure (2026)",
                                                  New Single() {SFINSECUREPOPIMP(i) * POP(i) + 1, GDPPCP(i) * 1000, GDPPCP(j) * 1000, POP(i), POP(j), DistanceBilat(i, j), SDYADPOPFOREIGN(i, j), SDYADPOPFOREIGN(j, i)}, ValEst,
                                                  New String() {"InsecureHeadCount_A", "GDPPCP_A", "GDPPCP_B", "POP_A", "POP_B", "DISTANCE", "MIGRANTSTOCKBILAT_A", "MIGRANTSTOCKBILAT_B"}, cntx)

                                        GRAVITYDYADMIG(i, j) = CSng(Math.Exp(ValEst))
                                        GRAVITYMIG(i) = GRAVITYMIG(i) + GRAVITYDYADMIG(i, j)

                                        'SHIFTGRAVITYDYADMIG(i, j) = XDYADMIG(i, j) / GRAVITYDYADMIG(i, j)
                                        SGRAVITYDYADMIGVAL(i, j) = GRAVITYDYADMIG(i, j)

                                    End If
                                Next j
                            Next i

                            For i = 1 To NR
                                For j = 1 To NR
                                    If i <> j Then
                                        If GRAVITYMIG(i) > 0 Then
                                            GRAVITYDYADMIG(i, j) = GRAVITYDYADMIG(i, j) / GRAVITYMIG(i)
                                        End If

                                        SGRAVITYDYADMIG(i, j) = GRAVITYDYADMIG(i, j)
                                    End If
                                Next j
                            Next i
                            '----------------------------------------------------------------------
                            ''DSB 2026/01/20 Reconciliation of bilateral out flows with monadic outward migration forecast
                            If migratebalancesw <> 1 Then 'dsb 2026/1/27
                                For i = 1 To NR
                                    Dim SumBilatOut As Double = 0
                                    For j = 1 To NR
                                        If i <> j Then
                                            SumBilatOut = SumBilatOut + XDYADMIG(i, j)
                                        End If
                                    Next j

                                    Dim scale As Double = 1
                                    If SumBilatOut > 0 Then
                                        scale = MIGRANTSOUT(i) / SumBilatOut
                                    End If
                                    For j = 1 To NR
                                        If i <> j Then
                                            XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * scale)
                                        End If
                                    Next j
                                Next i
                            End If
                            ''dsb 2026/01/20 *** end ***
                            '----------------------------------------------------------------------
                            For i = 1 To NR
                                For j = 1 To NR
                                    'SXDYADMIG(i, j) = XDYADMIG(i, j)
                                    'dsb 2026/02/16 Using average of last 10 years bilateral migration flows as base for smoothing in forecast period, but not adjusting base year flows
                                    SXDYADMIG(i, j) = MigrantsBilatAverage(i, j)
                                    'SSFCIVILWARPOPIMP(i) = SFCIVILWARPOPIMP(i)
                                Next j
                            Next i

                            'dsb 2026/02/11
                            'db.Clear(GetType(XDYADMIGRANT))
                            'db.Clear(GetType(MDYADMIGRANT))
                            'db.Clear(GetType(DYADPOPFOREIGN))
                            'db.Flush()
                        End Using
                        '---------------------------------------------------------------------
                        Using db As IFsForDyadicContext = New IFsForDyadicContext()
                            'dsb 2026/02/11
                            db.Clear(GetType(XDYADMIGRANT))
                            db.Clear(GetType(MDYADMIGRANT))
                            db.Clear(GetType(DYADPOPFOREIGN))
                            db.Flush()

                            Dim conn = db.Database.GetDbConnection
                            If conn.State = 0 Then conn.Open()
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

                            'Need to Save dyadic migration data
                            db.BulkInsertXDYADMIGRANT(XDYADMIG, FirstYear, NR)
                            db.BulkInsertMDYADMIGRANT(XDYADMIG, FirstYear, NR)

                            SaveBilatToCommon(XDYADMIG, MIGRANTSBILAT, 1000000)

                            db.BulkInsertDYADPOPFOREIGN(DYADPOPFOREIGN, FirstYear + IY - 1, NR)
                            'dsb 2026/01/26
                            SaveBilatToCommon(DYADPOPFOREIGN, POPFOREIGNBILAT, 1000000)
                        End Using
                    Else
                        '====================================================================
                        'OTHER YEARS: (IY>1)  Update Dyadic Migration Data - Conflict Based
                        '====================================================================
                        LoadDetailedDyadPars()
                        '----------------------------------------------------------------------
                        'DSB 2026/01/15 *** start **** SFINSECUREPOPIMP
                        'Percent of Population Impacted calculated using OLS
                        Dim PredictionPOPIMP As Single
                        For i = 1 To NR
                            'If i = 184 Or i = 180 Then Stop
                            If sfcivilwarucdpsw = 1 Then 'dsb 2026/07/22 
                                conflictindex = sSFCIVILWARUCDP(i)
                            ElseIf sfcivilwarucdpsw = 2 Then
                                conflictindex = ssfintlwarall(i)
                            Else
                                conflictindex = SSFCIVILWARPARDEE(i)
                            End If
                            Call AnalFunc("State Failure Percent of Population Impacted (2026)",
                              New Single() {conflictindex, SGoveffect(i), sLabParr_Pop(i, Male), POP(i), SDIPLOTREATY(i), SSVDTHSOTHERINTERPERSON_Pop(i)}, PredictionPOPIMP,
                              New String() {"SFCIVILWARPARDEE", "GOVEFFECT", "LABPARR", "POP", "DIPLOTREATY", "SVDTHSOTHERINTERPERSON"}, cntx)
                            SFINSECUREPOPIMP(i) = PredictionPOPIMP
                            SFINSECUREPOPIMP(i) = AMAX(0, Amin(1, SFINSECUREPOPIMP(i))) * sfinsecurepopimpm(i) 'dsb 2026/1/23 added multiplier
                        Next
                        'DSB 2026/01/15 *** end ****
                        '----------------------------------------------------------------------
                        'dsb 2026/05/05 Civil war pop imp
                        For i = 1 To NR
                            If sfcivilwarucdpsw = 1 Then 'dsb 2026/07/22 
                                conflictindex = sSFCIVILWARUCDP(i)
                            ElseIf sfcivilwarucdpsw = 2 Then
                                conflictindex = ssfintlwarall(i)
                            Else
                                conflictindex = SSFCIVILWARPARDEE(i)
                            End If
                            Call AnalFunc("State Failure Percent of Population Impacted, Civil War (2026)",
                              New Single() {POP_Lag1(i), conflictindex, SVDTHSWOMENCHILDREN_Lag(i), GOVEFFECT_Lag(i)}, PredictionPOPIMP,
                              New String() {"POP_Lag1", "SFCIVILWARPARDEE_Lag1", "SVDTHS_Lag1", "GOVEFFECT_Lag1"}, cntx)
                            SFCIVILWARPOPIMP(i) = PredictionPOPIMP
                            SFCIVILWARPOPIMP(i) = AMAX(0, Amin(1, SFCIVILWARPOPIMP(i))) * sfcivilwarpopimpm(i) 'dsb 2026/1/23 added multiplier
                        Next
                        'DSB 2026/01/15 *** end ****
                        '----------------------------------------------------------------------
                        'DSB 2026/01/20 Calculating outward monadic migration flow (MIGRANTSOUT)
                        Dim PredVal As Single
                        Dim squaredScale As Double = 1.0
                        If gdppcppeak <> 0 Then
                            Dim linearCoeff As Double = GetVariableCoefficient("Log of Emigration (Migrants out, percent of population) (2026)", "GDPPCP")
                            Dim squaredCoeff As Double = GetVariableCoefficient("Log of Emigration (Migrants out, percent of population) (2026)", "GDPPCPSQUARED")

                            If linearCoeff <> 0 AndAlso squaredCoeff <> 0 Then
                                Dim targetGDPPCPCoeff As Double = -linearCoeff / (2 * Math.Log(gdppcppeak))
                                squaredScale = targetGDPPCPCoeff / squaredCoeff
                            End If
                        End If
                        For i = 1 To NR
                            'dsb 2026/01/27 New monadic regression model
                            Call AnalFunc("Log of Emigration (Migrants out, percent of population) (2026)",
                                    New Single() {POP(i), SGDPR(i), SINCOMELT830LN(i) / POP(i) * 100, sGDPPCP(i), CSng(squaredScale * Math.Log(If(sGDPPCP(i) <= 0, 1, sGDPPCP(i))) * Math.Log(If(sGDPPCP(i) <= 0, 1, sGDPPCP(i)))), (SFCIVILWARPOPIMP(i) * 100 + 1)}, PredVal,
                                    New String() {"POP", "GDPR", "POV685", "GDPPCP", "GDPPCPSQUARED", "SFPOPIMPPCT"}, cntx)
                            PredVal = CSng(Math.Exp(PredVal)) / 100 'Convert from percent of population to fraction of population
                            MIGRANTSOUTFORECAST(i) = PredVal * POP(i) * 1000000
                        Next i

                        For i = 1 To NR
                            'dsb 2026/01/29 converging the shift factor to 0 by 2150
                            shiftfactorMonadic = ConvergeOverTime(SMIGRANTSOUTFORECAST(i), 0, 2151 - FirstYear)
                            MIGRANTSOUT(i) = AMAX(0, MIGRANTSOUTFORECAST(i) + shiftfactorMonadic) 'dsb 2026/01/29 uncommented
                            MIGRANTSOUT(i) = MIGRANTSOUT(i) * migrateoutm(i) 'dsb 2026/02/5 added multiplier
                        Next
                        For i = 1 To NR 'dsb 2026/01/26
                            SMIGRANTSOUTN(i) = MIGRANTSOUT(i)
                        Next
                        '----------------------------------------------------------------------
                        'Gravity Model for OTHER YEARS IY > 1
                        'USES: DISTANCE, POPULATION ORIGIN AND DESTINATION, GDPPCP ORIGIN AND DESTINATION, CONFLICT MAGNITUDE ORIGIN, MIGRATION STOCKS
                        '----------------------------------------------------------------------
                        Dim ValEst = 0!
                        For i = 1 To NR
                            GRAVITYMIG(i) = 0
                            For j = 1 To NR
                                If i <> j AndAlso SDYADPOPFOREIGN(i, j) > 0 AndAlso SDYADPOPFOREIGN(j, i) > 0 Then 'dsb 2026/01/16
                                    'If i <> j Then 'dsb 2026/01/16
                                    'Call AnalFunc("Log of Emigration(Migration Flow from A to B) (2026)", 'dsb 2026/1/30 I was usign GDPPCP, now corrected to GDP as per the regression
                                    'New Single() {SFCIVILWARPOPIMP(i) * POP(i) + 1, sGDPPCP(i), sGDPPCP(j), POP(i), POP(j), DistanceBilat(i, j), SDYADPOPFOREIGN(i, j), SDYADPOPFOREIGN(j, i)}, ValEst,
                                    'New String() {"SVCIVILWARPOPIMPABS_A", "GDPPCP_A", "GDPPCP_B", "POP_A", "POP_B", "DISTANCE", "MIGRANTSTOCKBILAT_A", "MIGRANTSTOCKBILAT_B"}, cntx)
                                    'dsb 2026/2/211 New Bilateral Forecast model from Jonathan based on Insecure population
                                    Call AnalFunc("Log of Emigration (Migration Flow from A to B), Insecure (2026)",
                                                  New Single() {SFINSECUREPOPIMP(i) * POP(i) + 1, sGDPPCP(i) * 1000, sGDPPCP(j) * 1000, POP(i), POP(j), DistanceBilat(i, j), SDYADPOPFOREIGN(i, j), SDYADPOPFOREIGN(j, i)}, ValEst,
                                                  New String() {"InsecureHeadCount_A", "GDPPCP_A", "GDPPCP_B", "POP_A", "POP_B", "DISTANCE", "MIGRANTSTOCKBILAT_A", "MIGRANTSTOCKBILAT_B"}, cntx)
                                    GRAVITYDYADMIGVAL(i, j) = CSng(Math.Exp(ValEst))
                                    GRAVITYMIG(i) = GRAVITYMIG(i) + GRAVITYDYADMIGVAL(i, j)
                                End If
                            Next j
                        Next i

                        For i = 1 To NR
                            For j = 1 To NR
                                If i <> j Then
                                    If GRAVITYMIG(i) > 0 Then
                                        GRAVITYDYADMIG(i, j) = GRAVITYDYADMIGVAL(i, j) / GRAVITYMIG(i)
                                    End If
                                End If
                            Next j
                        Next i
                        '----------------------------------------------------------------------
                        'Forecast flows 
                        For i = 1 To NR
                            For j = 1 To NR
                                If i <> j Then
                                    If SGRAVITYDYADMIGVAL(i, j) > 0 Then
                                        XDYADMIG(i, j) = CSng(SXDYADMIG(i, j) * Amin(GRAVITYDYADMIG(i, j) / SGRAVITYDYADMIG(i, j), migbilatgrowthratelimit(i, j)))
                                    Else
                                        XDYADMIG(i, j) = SXDYADMIG(i, j)
                                    End If

                                    SGRAVITYDYADMIG(i, j) = GRAVITYDYADMIG(i, j)
                                    SGRAVITYDYADMIGVAL(i, j) = GRAVITYDYADMIGVAL(i, j)

                                    XDYADMIG(i, j) = XDYADMIG(i, j) * tradeexportdyadm2(i, j, 4)
                                    XDYADMIG(i, j) = XDYADMIG(i, j) * migrateoutm(i)
                                    XDYADMIG(i, j) = XDYADMIG(i, j) * migrateinm(j)
                                End If
                            Next j
                        Next i

                        '----------------------------------------------------------------------
                        For i = 1 To NR
                            SPopForeignOut(i) = 0
                            SPopForeignIn(i) = 0
                        Next i
                        For i = 1 To NR
                            For j = 1 To NR
                                SPopForeignOut(i) = SPopForeignOut(i) + SDYADPOPFOREIGN(i, j) / 1000000
                                SPopForeignIn(j) = SPopForeignIn(j) + SDYADPOPFOREIGN(i, j) / 1000000
                            Next
                        Next
                        '----------------------------------------------------------------------
                        'Check population limits
                        '----------------------------------------------------------------------
                        Dim LimitPopF As Single
                        'dsb 2026/02/17
                        Dim nativeOut As Single, foreignOut As Single, foreignShare As Single

                        'dsb 2026/01/29 LimitPOPF to Monadic outward migration forecast
                        For i = 1 To NR
                            'If i = 144 And IY > 33 Then Stop
                            'If MIGRANTSOUT(i) > 0.1 * sPOP15TO65(i) * 1000000 * (1 - Amin(SPopForeignIn(i) / POP(i), 0.99)) Then
                            '    LimitPopF = CSng(0.1 * sPOP15TO65(i) * 1000000 * (1 - Amin(SPopForeignIn(i) / POP(i), 0.99)) / MIGRANTSOUT(i))
                            '    MIGRANTSOUT(i) = MIGRANTSOUT(i) * LimitPopF
                            'End If
                            'dsb 2026/02/17 More detailed limit by considering foreign share in the population, and only limit native out migrants
                            foreignShare = CSng(Amin(SPopForeignIn(i) / POP(i), 0.99))
                            nativeOut = MIGRANTSOUT(i) * (1 - foreignShare)
                            foreignOut = MIGRANTSOUT(i) - nativeOut
                            If nativeOut > 0.1 * sPOP15TO65(i) * 1000000 * (1 - foreignShare) Then
                                LimitPopF = CSng(0.1 * sPOP15TO65(i) * 1000000 * (1 - foreignShare) / nativeOut)
                                nativeOut = nativeOut * LimitPopF
                                MIGRANTSOUT(i) = nativeOut + foreignOut
                            End If
                        Next
                        '----------------------------------------------------------------------
                        'dsb 2026/01/27 Weighted-average monadic total before reconciliation 
                        'MIGRANTSOUT(i) - regression based monadic out flows
                        'SumBilatOut - sum of bilateral out flows
                        'DSB 2026/01/20 Reconciliation of bilateral out flows with monadic outward migration forecast
                        '----------------------------------------------------------------------
                        Dim scale As Double = 1
                        If migratebalancesw <> 1 Then
                            For i = 1 To NR
                                Dim SumBilatOut As Single = 0
                                For j = 1 To NR
                                    If i <> j Then
                                        SumBilatOut = SumBilatOut + XDYADMIG(i, j)
                                    End If
                                Next j
                                'dsb 2026/01/27
                                MIGRANTSOUT(i) = (1 - migratebalancesw) * MIGRANTSOUT(i) + migratebalancesw * SumBilatOut
                                scale = 1
                                If SumBilatOut > 0 Then
                                    'If MIGRANTSOUT(i) = 0 Then Stop
                                    scale = MIGRANTSOUT(i) / SumBilatOut
                                End If
                                For j = 1 To NR
                                    If i <> j Then
                                        XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * scale)
                                    End If
                                Next j
                            Next i
                        End If
                        'dsb 2026/01/20 *** end ***
                        '----------------------------------------------------------------------
                        'dsb 2026/02/11 Limit the population going out of a country for the case when bilateral drives monadic completely
                        For i = 1 To NR
                            MIGIN(i) = 0
                            MIGOUT(i) = 0
                        Next i

                        For i = 1 To NR
                            For j = 1 To NR
                                If XDYADMIG(i, j) > 0 Then
                                    MIGOUT(i) = MIGOUT(i) + XDYADMIG(i, j)
                                    MIGIN(j) = MIGIN(j) + XDYADMIG(i, j)
                                End If
                            Next j
                        Next i
                        'dsb 2026/03/09 Added nativeinLimitF and foreigninLimitF to limit native and foreign in migrants separately based on their shares in the population to avoid unrealistic population replacement in some countries with very high out migration and very low foreign share in the population
                        Dim nativeIn As Single, foreignIn As Single, nativeinLimitF As Single, foreigninLimitF As Single
                        For i = 1 To NR
                            'If MIGOUT(i) > 0.1 * sPOP15TO65(i) * 1000000 * (1 - Amin(SPopForeignIn(i) / POP(i), 0.99)) Then
                            '    LimitPopF = CSng(0.1 * sPOP15TO65(i) * 1000000 * (1 - Amin(SPopForeignIn(i) / POP(i), 0.99)) / MIGOUT(i))
                            '    For j = 1 To NR
                            '        XDYADMIG(i, j) = XDYADMIG(i, j) * LimitPopF
                            '    Next j
                            '    For j = 1 To NR
                            '        XDYADMIG(j, i) = XDYADMIG(j, i) * LimitPopF
                            '    Next j
                            'End If
                            'dsb 2026/02/17 More detailed limit by considering foreign share in the population, and only limit native out migrants
                            'If i = 144 And IY > 34 Then Stop
                            foreignShare = CSng(Amin(SPopForeignIn(i) / POP(i), 0.99))
                            nativeOut = MIGOUT(i) * (1 - foreignShare)
                            foreignOut = MIGOUT(i) - nativeOut
                            If nativeOut > 0.1 * sPOP15TO65(i) * 1000000 * (1 - foreignShare) Then
                                LimitPopF = CSng(0.1 * sPOP15TO65(i) * 1000000 * (1 - foreignShare) / nativeOut)
                                nativeOut = nativeOut * LimitPopF
                                scale = 1
                                If MIGOUT(i) > 0 Then scale = (foreignOut + nativeOut) / MIGOUT(i)
                                For j = 1 To NR
                                    If i <> j Then
                                        XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * scale)
                                    End If
                                Next j
                            End If
                        Next
                        For i = 1 To NR
                            foreignShare = CSng(Amin(SPopForeignIn(i) / POP(i), 0.99))
                            'dsb 2026/02/18 Limit the population coming in
                            nativeIn = MIGIN(i) * (1 - foreignShare)
                            foreignIn = MIGIN(i) - nativeIn
                            'dsb 2026/03/09
                            foreigninLimitF = foreignIn
                            nativeinLimitF = nativeIn
                            If foreignIn > 0.1 * sPOP15TO65(i) * 1000000 * foreignShare Then
                                LimitPopF = CSng(0.1 * sPOP15TO65(i) * 1000000 * foreignShare / foreignIn)
                                foreigninLimitF = foreignIn * LimitPopF
                                'scale = 1
                                'If MIGIN(i) > 0 Then scale = (nativeIn + foreignIn) / MIGIN(i)
                                'For j = 1 To NR
                                '    If i <> j Then
                                '        XDYADMIG(j, i) = CSng(XDYADMIG(j, i) * scale)
                                '    End If
                                'Next j
                            End If
                            'dsb 2026/03/09 Limiting nativein migrants for countries with very less foreign share to avoid unrealistic population replacement
                            If nativeIn > 0.5 * SPopForeignOut(i) * 1000000 Then 'nativein absolute numbers, popforeignout in millions
                                LimitPopF = CSng(0.5 * SPopForeignOut(i) * 1000000 / nativeIn)
                                nativeinLimitF = nativeIn * LimitPopF
                            End If
                            If nativeIn <> nativeinLimitF Or foreignIn <> foreigninLimitF Then
                                scale = 1
                                If MIGIN(i) > 0 Then scale = (nativeinLimitF + foreigninLimitF) / MIGIN(i)
                                For j = 1 To NR
                                    If i <> j Then
                                        XDYADMIG(j, i) = CSng(XDYADMIG(j, i) * scale)
                                    End If
                                Next j
                            End If
                        Next i
                        '----------------------------------------------------------------------
                        'dsb 2026/03/09 
                        For i = 1 To NR
                            MIGIN(i) = 0
                            MIGOUT(i) = 0
                        Next i

                        For i = 1 To NR
                            For j = 1 To NR
                                If XDYADMIG(i, j) > 0 Then
                                    MIGOUT(i) = MIGOUT(i) + XDYADMIG(i, j)
                                    MIGIN(j) = MIGIN(j) + XDYADMIG(i, j)
                                End If
                            Next j
                        Next i

                        For i = 1 To NR
                            If MIGIN(i) / POP(i) / 1000000 > AbsMigMax / 100 Then
                                scale = 1
                                If MIGIN(i) > 0 Then scale = CSng(AbsMigMax / 100 * POP(i) * 1000000 / MIGIN(i))
                                For j = 1 To NR
                                    If i <> j Then
                                        XDYADMIG(j, i) = CSng(XDYADMIG(j, i) * scale)
                                    End If
                                Next j
                            End If
                        Next i

                        For i = 1 To NR
                            If MIGOUT(i) / POP(i) / 1000000 > AbsMigMax / 100 Then
                                scale = 1
                                If MIGOUT(i) > 0 Then scale = CSng(AbsMigMax / 100 * POP(i) * 1000000 / MIGOUT(i))
                                For j = 1 To NR
                                    If i <> j Then
                                        XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * scale)
                                    End If
                                Next j
                            End If
                        Next i
                        '----------------------------------------------------------------------
                        'dsb 2026/04/07 Population for some countries is less than a million by 2100. So impose a limit on outward mirgation for these countries
                        For i = 1 To NR '
                            MIGIN(i) = 0
                            MIGOUT(i) = 0
                        Next i

                        For i = 1 To NR '
                            For j = 1 To NR ' 
                                If XDYADMIG(i, j) > 0 Then
                                    MIGOUT(i) = MIGOUT(i) + XDYADMIG(i, j)
                                    MIGIN(j) = MIGIN(j) + XDYADMIG(i, j)
                                End If
                            Next j
                        Next i '

                        For i = 1 To NR '
                            scale = 1
                            'If i = 177 Then Stop
                            If POP(i) * 1000000 + MIGIN(i) - MIGOUT(i) < 0.7 * POPI(i) * 1000000 And MIGOUT(i) > 0 Then
                                If POP(i) * 1000000 + MIGIN(i) - MIGOUT(i) <= 0.5 * POPI(i) * 1000000 Then
                                    scale = 0.1
                                Else
                                    scale = (POP(i) * 1000000 + MIGIN(i) - MIGOUT(i) - 0.5 * POPI(i) * 1000000) / (0.2 * POPI(i) * 1000000)
                                End If '
                            End If '
                            scale = AMAX(0.1, Amin(1, scale))
                            For j = 1 To NR
                                If i <> j Then
                                    XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * (1 - 0.5) + XDYADMIG(i, j) * scale * 0.5)
                                End If '
                            Next j
                        Next i '
                        'Net migration check
                        For i = 1 To NR '
                            MIGIN(i) = 0
                            MIGOUT(i) = 0
                        Next i

                        For i = 1 To NR '
                            For j = 1 To NR ' 
                                If XDYADMIG(i, j) > 0 Then
                                    MIGOUT(i) = MIGOUT(i) + XDYADMIG(i, j)
                                    MIGIN(j) = MIGIN(j) + XDYADMIG(i, j)
                                End If
                            Next j
                        Next i

                        For i = 1 To NR '
                            If MIGOUT(i) - MIGIN(i) > 0.01 * POP(i) * 1000000 Then
                                Migrantsnetabove1percent(i) = Migrantsnetabove1percent(i) + 1
                                If Migrantsnetabove1percent(i) >= 5 Then 'if net migration is above 1 percent of the population for more than 5 years, then impose a limit on net migration
                                    scale = 0.01 * POP(i) * 1000000 / (MIGOUT(i) - MIGIN(i))
                                    For j = 1 To NR '
                                        If i <> j Then
                                            XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * scale)
                                        End If
                                    Next j
                                End If
                            Else
                                Migrantsnetabove1percent(i) = 0
                            End If
                        Next i
                        '----------------------------------------------------------------------
                        For i = 1 To NR
                            For j = 1 To NR
                                SXDYADMIG(i, j) = XDYADMIG(i, j)
                            Next j
                        Next i

                        For i = 1 To NR
                            For j = 1 To NR
                                If migrantsmissingm(i, j) <> 1 Then
                                    XDYADMIG(i, j) = XDYADMIG(i, j) * migrantsmissingm(i, j)
                                End If
                            Next j
                        Next i
                        '----------------------------------------------------------------------
                        'DYADPOPFOREIGN Update
                        '----------------------------------------------------------------------
                        'For i = 1 To NR
                        '    SPopForeignIn(i) = 0
                        '    For j = 1 To NR
                        '        SPopForeignIn(i) = SPopForeignIn(i) + SDYADPOPFOREIGN(j, i) / 1000000
                        '    Next j
                        'Next i

                        For i = 1 To NR
                            For j = 1 To NR
                                If i <> j Then
                                    'dsb 2026/01/29 
                                    If (SDYADPOPFOREIGN(i, j) + SDYADPOPFOREIGN(j, i)) > 0 Then
                                        NativeRatio = SDYADPOPFOREIGN(i, j) / (SDYADPOPFOREIGN(i, j) + SDYADPOPFOREIGN(j, i))
                                    Else
                                        NativeRatio = 0
                                    End If

                                    NativeRatio = NativeRatio * migsharenativem(i, j)
                                    If migsharenativem(j, i) > 0 Then
                                        NativeRatio = NativeRatio / migsharenativem(j, i)
                                    Else
                                        NativeRatio = 1
                                    End If
                                    If NativeRatio > 1 Then
                                        NativeRatio = 1
                                    End If
                                    'If ((i = 49 And j = 98) Or (i = 98 And j = 49)) And IY > 24 Then Stop
                                    Dim dRate As Double = 0
                                    If POP(i) > 0 AndAlso POP(j) > 0 Then
                                        dRate = (SDEATHS(j) / POP(j)) * migdeathratesw(i, j) + (SDEATHS(i) / POP(i)) * (1 - migdeathratesw(i, j))
                                    End If
                                    Dim deathsF As Double = dRate * SDYADPOPFOREIGN(i, j)
                                    DYADPOPFOREIGN(i, j) = AMAX(SDYADPOPFOREIGN(i, j) - deathsF + XDYADMIG(i, j) * NativeRatio - Amin(XDYADMIG(j, i) * NativeRatio, limit4returningmigs(i, j) * SDYADPOPFOREIGN(i, j)), 0)
                                End If
                            Next j
                        Next i

                        'dsb 2026/02/17 Limiting foreign born population to 90 percent of the total population for extreme cases like UAE
                        For i = 1 To NR
                            POPFOREIGNIN(i) = 0
                        Next i
                        For i = 1 To NR
                            For j = 1 To NR
                                If i <> j Then
                                    POPFOREIGNIN(j) = POPFOREIGNIN(j) + DYADPOPFOREIGN(i, j)
                                End If
                            Next
                        Next
                        For j = 1 To NR
                            If POPFOREIGNIN(j) > 0.9 * POP(j) * 1000000 AndAlso POP(j) > 0 Then
                                For i = 1 To NR
                                    If i <> j Then
                                        DYADPOPFOREIGN(i, j) = CSng(DYADPOPFOREIGN(i, j) * 0.9 * POP(j) * 1000000 / POPFOREIGNIN(j))
                                        'dsb 2026/03/10 Limiting the in flow to the foreign born population to 90 percent of the total population for extreme cases like UAE, Qatar by adjusting the bilateral stocks
                                        XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * 0.9 * POP(j) * 1000000 / POPFOREIGNIN(j))
                                        SXDYADMIG(i, j) = XDYADMIG(i, j)
                                    End If
                                Next
                            End If
                        Next

                        For i = 1 To NR
                            For j = 1 To NR
                                SDYADPOPFOREIGN(i, j) = DYADPOPFOREIGN(i, j)
                            Next j
                        Next i

                        'For i = 1 To NR '2026/01/29 Test to see if stocks causing problems
                        '    For j = 1 To NR
                        '        SDYADPOPFOREIGN(i, j) = SDYADPOPFOREIGN(i, j)
                        '    Next j
                        'Next i
                        '----------------------------------------------------------------------
                        Using db = New IFsForDyadicContext()
                            Dim conn = db.Database.GetDbConnection
                            If conn.State = 0 Then conn.Open()
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

                            db.BulkInsertXDYADMIGRANT(XDYADMIG, FirstYear + IY - 1, NR)
                            db.BulkInsertMDYADMIGRANT(XDYADMIG, FirstYear + IY - 1, NR)

                            SaveBilatToCommon(XDYADMIG, MIGRANTSBILAT, 1000000)

                            db.BulkInsertDYADPOPFOREIGN(DYADPOPFOREIGN, FirstYear + IY - 1, NR)
                            'dsb 2026/01/26
                            SaveBilatToCommon(DYADPOPFOREIGN, POPFOREIGNBILAT, 1000000)
                        End Using
                    End If
                    '--------------------------------------------------------------
                    'Update MIGRANTSIN/MIGRANTSOUT and Migration Rates
                    '--------------------------------------------------------------
                    For i = 1 To NR
                        MIGRANTSOUT(i) = 0
                        MIGRANTSIN(i) = 0
                    Next i
                    For i = 1 To NR
                        For j = 1 To NR
                            If XDYADMIG(i, j) > 0 Then
                                MIGRANTSOUT(i) = MIGRANTSOUT(i) + XDYADMIG(i, j) / 1000000
                                MIGRANTSIN(j) = MIGRANTSIN(j) + XDYADMIG(i, j) / 1000000
                            End If
                        Next j
                    Next i
                    For i = 1 To NR
                        MIGRATEOUT(i) = MIGRANTSOUT(i) / POP(i) * 100
                        MIGRATEIN(i) = MIGRANTSIN(i) / POP(i) * 100
                    Next i
                    If IY = 1 Then
                        For i = 1 To NR
                            POPFOREIGNOUT(i) = 0
                            POPFOREIGNIN(i) = 0
                        Next i
                        For i = 1 To NR
                            For j = 1 To NR
                                POPFOREIGNOUT(i) = POPFOREIGNOUT(i) + DYADPOPFOREIGN(i, j) / 1000000
                                POPFOREIGNIN(j) = POPFOREIGNIN(j) + DYADPOPFOREIGN(i, j) / 1000000
                                SPopForeignIn(j) = POPFOREIGNIN(j)
                            Next
                        Next
                    Else
                        For i = 1 To NR
                            POPFOREIGNOUT(i) = 0
                            POPFOREIGNIN(i) = 0
                        Next i
                        For i = 1 To NR
                            For j = 1 To NR
                                POPFOREIGNOUT(i) = POPFOREIGNOUT(i) + DYADPOPFOREIGN(i, j) / 1000000
                                POPFOREIGNIN(j) = POPFOREIGNIN(j) + DYADPOPFOREIGN(i, j) / 1000000
                                SPopForeignIn(j) = POPFOREIGNIN(j)
                            Next
                        Next
                    End If

                End If
            Catch ex As Exception

            End Try
        End Sub

        'dsb 2026/05/20
        Private Function GetVariableCoefficient(TFName As String, variableName As String) As Double
            Dim regArray = RegArrays(TFName)
            Dim initial = RegCoeffs(TFName)
            For Each reg In regArray
                If String.Equals(reg.InputName, variableName, StringComparison.OrdinalIgnoreCase) Then
                    For i As Integer = 1 To 9
                        Dim key As String = reg.Seq.ToString() & "-b" & i.ToString()
                        If initial.ContainsKey(key) Then
                            Dim value = initial(key)
                            If value <> 0 Then
                                Return value
                            End If
                        End If
                    Next
                End If
            Next
            Return 0.0
        End Function
        'sgh 2018/07/11 - updated MigrationDyadic from Jose
        Private Sub MigrationDyadic_ForcedAndVoluntary(cntx As IForecastContext)
            'Private Sub MigrationDyadic(cntx As IForecastContext)
            'jrs 2017/11/24
            Try
                'jrs 2018/01/19
                Dim NativeRatio As Single
                'jrs 2018/01/10 added or bilateralsw > 0
                'If doing bilateral migration
                If System.IO.File.Exists(PutFdrvRun$ + "IFsForDyadicWork.db") AndAlso (migratesw > 0 OrElse bilateralsw > 0) Then
                    'gvdb 2024/04/05
                    'unused variable
                    'Dim migTbl As IDictionary(Of String, Single?()), migTbl2 As IDictionary(Of String, Single?()), migTbl3 As IDictionary(Of String, Single?())
                    Dim z As Single
                    Dim i As Integer, j As Integer
                    'gvdb 2024/04/05
                    'unused variable
                    'Dim RatioOfftoUnDoc As Single
                    'jrs 2018/03/01
                    Dim contig1 As Single

                    If IY = 1 Then
                        'Need to Open and Load dyadic migration data
                        ReDim XDYADMIG(NR, NR), SXDYADMIG(NR, NR)
                        ReDim tradeexportdyadm2(NR, NR, 5)
                        ReDim tradeexportdyadm(NR, NR, NSec)
                        ReDim tradecostgeneral(NR, NR, NSec)
                        ReDim tradeelastpriceexportsdyad(NR, NR, NSec)
                        ReDim GRAVITYDYADMIG(NR, NR), SGRAVITYDYADMIG(NR, NR), GRAVITYMIG(NR)
                        ReDim DYADPOPFOREIGN(NR, NR), SDYADPOPFOREIGN(NR, NR)
                        'jrs 2020/06/19
                        ReDim bPOPFOREIGNUPDATE(NR, NR)
                        ReDim DistanceBilat(NR, NR)
                        ReDim migsharenativem(NR, NR), migdeathratesw(NR, NR)
                        ReDim MIGIN(NR), MIGOUT(NR)
                        ReDim limit4returningmigs(NR, NR)

                        ReDim igom(NR, NR)
                        ReDim alliancem(NR, NR)
                        'jrs 2022/01/18
                        ReDim allianceadd(NR, NR), loradd(NR, NR)
                        ReDim treatym(NR, NR)
                        'jrs 2022/02/04
                        ReDim dyadtradeagreementindexadd(NR, NR)
                        'jrs 2022/04/28
                        ReDim dyadmilmstockadd(NR, NR), xdyadaidadd(NR, NR)

                        'jrs 2019/08/09
                        ReDim dyadtariffratem(NR, NR, NSec)
                        'jrs 2021/04/21
                        ReDim dyadtariffrateadd(NR, NR, NSec)

                        'jrs 2019/09/20
                        ReDim dyadtradecomplementm(NR, NR, NSec)

                        'jrs 2017/12/22
                        'jrs 2018/08/13 ReDim XDYADMIGUNDOC(NR, NR), DYADPOPFOREIGNUNDOC(NR, NR), SDYADPOPFOREIGNUNDOC(NR, NR)
                        'ReDim migundocsharenativem(NR, NR), migundocdeathratesw(NR, NR)
                        'ReDim limit4returningundocmigs(NR, NR)
                        'jrs 2018/01/04
                        'ReDim ratiomigdoctoundoc(NR, NR)
                        'jrs 2018/01/16
                        ReDim migrantsmissingm(NR, NR)
                        'jrs 2018/01/19
                        ReDim DYADMIGFORCED(NR, NR), SDYADMIGFORCED(NR, NR), DYADMIGFORCEDRETURN(NR, NR), SDYADMIGFORCEDRETURN(NR, NR), DYADPOPFOREIGNFORCED(NR, NR), SDYADPOPFOREIGNFORCED(NR, NR)
                        ReDim SDYADMIGFORECEDRETURNEST(NR) 'jrs 2018/06/19 SMIGFORCEDEST(NR),
                        ReDim SharesForced(NR + 1, NR + 1), SSharesForced(NR + 1, NR + 1), GravityDyadForced(NR, NR), SGravityDyadForced(NR, NR), GravityForced(NR)
                        ReDim SharesFlowForced(NR + 1, NR + 1), SharesStockForced(NR + 1, NR + 1), SSharesFlowForced(NR + 1, NR + 1), SSharesStockForced(NR + 1, NR + 1)
                        'jrs 2018/01/22
                        ReDim DYADMIGVOL(NR, NR), SDYADMIGVOL(NR, NR), DYADPOPFOREIGNVOL(NR, NR), SDYADPOPFOREIGNVOL(NR, NR)
                        'jrs 2018/01/26
                        ReDim dyadmigforcedreturnadd(NR, NR)
                        'jrs 2018/01/29
                        ReDim GRAVITYDYADMIGVAL(NR, NR)
                        'jrs 2018/01/30
                        ReDim SHIFTGRAVITYDYADMIG(NR, NR)
                        ReDim AdjustFstYr(NR), AdjustBaseVal(NR)
                        ReDim SGRAVITYDYADMIGVAL(NR, NR)
                        'jrs 2018/02/23
                        ReDim DYADMIGFORRETLST0(NR, NR)
                        'jrs 2018/03/20
                        'ReDim DYADMIGFORCEDTARGETPCTPOP(NR, NR), DYADMIGVOLTARGETPCTPOP(NR, NR)
                        ReDim IDYADMIGFORCEDTARGETPCTPOP(NR, NR), IDYADMIGVOLTARGETPCTPOP(NR, NR)
                        ReDim IDYADMIGFORCEDPCTPOP(NR, NR), IDYADMIGVOLPCTPOP(NR, NR)
                        'jrs 2018/04/03
                        ReDim SPopForeignIn(NR)
                        'jrs 2018/04/16
                        ReDim dyadforcedreturnlimitm(NR, NR), VolTarget(NR, NR)
                        'jrs 2018/05/04
                        ReDim dyadmigvoltargetsw(NR, NR)
                        'jrs 2018/06/26
                        ReDim dyadforcedreturnsignal(NR, NR)
                        'jrs 2018/07/09
                        ReDim dyadmigrantforcedm(NR, NR)
                        'clp 2023/12/20
                        ReDim dyadaidexoadd(NR, NR)
                        ReDim polrelevance(NR, NR), peaceyearset(NR, NR), peaceyearcountadj(NR, NR) 'clp 2024/07/10 Public due to first-year occuring in dyadic and later ones occuring in SocioPol
                        ReDim dyadldrtripm(NR, NR)

                        'jrs 2019/05/03 changed from BaseYear to FirstYear

                        'Load DistanceBilat
                        'gvdb 2024/04/05
                        'unused variable
                        'Dim rsD As IDictionary(Of String, Single?())
                        'Dim yrs As Short()
                        'jrs 2019/07/18 Translate later *** begin ***
                        'ttt 2019/09/05
                        Using db = New IFsForDyadicContext()
                            db.Enter()
                            'Load DistanceBilat
                            Dim rsDistance = (From XD In db.Distance Where XD.Year = FirstYear).ToList
                            For Each rec As Distance In rsDistance
                                DistanceBilat(rec.Actor, rec.Partner) = rec.Val
                                DistanceBilat(rec.Partner, rec.Actor) = rec.Val
                            Next
                            'Load XDYADMIGRANT
                            Dim rsXDYADMIGRANT = (From XD In db.XDYADMIGRANT Where XD.Year = FirstYear).ToList
                            For Each rec As XDYADMIGRANT In rsXDYADMIGRANT
                                XDYADMIG(rec.Actor, rec.Partner) = rec.Val
                                'SXDYADMIG(rsD.Fields("Actor"), rsD.Fields("Partner")) = XDYADMIG(rsD.Fields("Actor"), rsD.Fields("Partner"))
                                MIGOUT(rec.Actor) = MIGOUT(rec.Actor) + rec.Val
                                MIGIN(rec.Partner) = MIGIN(rec.Partner) + rec.Val
                            Next
                            'jrs 2018/01/19
                            '                        'First Year: Load DYADMIGRANTFORCED
                            '                        '------------------------------------
                            Dim rsDYADMIGRANTFORCED = (From XD In db.DYADMIGRANTFORCED Where XD.Year = FirstYear).ToList
                            For Each rec As DYADMIGRANTFORCED In rsDYADMIGRANTFORCED
                                DYADMIGFORCED(rec.Actor, rec.Partner) = rec.Val
                                SDYADMIGFORCED(rec.Actor, rec.Partner) = DYADMIGFORCED(rec.Actor, rec.Partner)
                                SharesForced(rec.Actor, rec.Partner) = DYADMIGFORCED(rec.Actor, rec.Partner)
                                SharesForced(rec.Actor, NR + 1) = SharesForced(rec.Actor, NR + 1) + SharesForced(rec.Actor, rec.Partner)
                                SSharesForced(rec.Actor, rec.Partner) = DYADMIGFORCED(rec.Actor, rec.Partner)
                                SSharesForced(rec.Actor, NR + 1) = SharesForced(rec.Actor, NR + 1)
                                SharesFlowForced(rec.Actor, rec.Partner) = DYADMIGFORCED(rec.Actor, rec.Partner)
                                SharesFlowForced(rec.Actor, NR + 1) = SharesForced(rec.Actor, NR + 1)
                                SSharesFlowForced(rec.Actor, rec.Partner) = DYADMIGFORCED(rec.Actor, rec.Partner)
                                SSharesFlowForced(rec.Actor, NR + 1) = SharesForced(rec.Actor, NR + 1)
                            Next
                            '            For i = 1 To NR
                            '                'jrs 2018/02/26 SMIGFORCEDEST(i) = 1.2825 + 17.3877 * SFINTLWARMAG(i) / POP(i)
                            '                'jrs 2018/06/19 SMIGFORCEDEST(i) = AMAX((-0.076 + 1.103 * GOVRISK(i) ^ 2) * POP(i), 0)
                            '                SMIGFORCEDEST(i) = AMAX(Exp(3.469 - 12.8129 * GOVINDSECUR(i)) * POP(i) / 100, 0)
                            '            Next i

                            'First Year: Load DYADMIGRANTFORCEDRETURN
                            '----------------------------------------
                            Dim rsDYADMIGRANTFORCEDRETURN = (From XD In db.DYADMIGRANTFORCEDRETURN Where XD.Year = FirstYear).ToList
                            For Each rec As DYADMIGRANTFORCEDRETURN In rsDYADMIGRANTFORCEDRETURN
                                DYADMIGFORCEDRETURN(rec.Actor, rec.Partner) = rec.Val
                                SDYADMIGFORCEDRETURN(rec.Actor, rec.Partner) = DYADMIGFORCEDRETURN(rec.Actor, rec.Partner)
                                'jrs 2018/02/23
                                If DYADMIGFORCEDRETURN(rec.Actor, rec.Partner) = 0 Then
                                    DYADMIGFORRETLST0(rec.Actor, rec.Partner) = 1
                                End If
                            Next
                            'First Year: Load DYADPOPFOREIGN
                            '-------------------------------
                            Dim rsDYADPOPFOREIGN = (From XD In db.DYADPOPFOREIGN Where XD.Year = FirstYear).ToList
                            For Each rec As DYADPOPFOREIGN In rsDYADPOPFOREIGN
                                DYADPOPFOREIGN(rec.Actor, rec.Partner) = rec.Val
                            Next
                            'First Year: Load DYADPOPFOREIGNFORCED
                            '------------------------------------
                            Dim rsDYADPOPFOREIGNFORCED = (From XD In db.DYADPOPFOREIGNFORCED Where XD.Year = FirstYear).ToList
                            For Each rec As DYADPOPFOREIGNFORCED In rsDYADPOPFOREIGNFORCED
                                DYADPOPFOREIGNFORCED(rec.Actor, rec.Partner) = rec.Val
                                SDYADPOPFOREIGNFORCED(rec.Actor, rec.Partner) = DYADPOPFOREIGNFORCED(rec.Actor, rec.Partner)
                                SharesStockForced(rec.Actor, rec.Partner) = DYADPOPFOREIGNFORCED(rec.Actor, rec.Partner)
                                SharesStockForced(rec.Actor, NR + 1) = SharesStockForced(rec.Actor, NR + 1) + SharesStockForced(rec.Actor, rec.Partner)
                                SSharesStockForced(rec.Actor, rec.Partner) = DYADPOPFOREIGNFORCED(rec.Actor, rec.Partner)
                                SSharesStockForced(rec.Actor, NR + 1) = SharesStockForced(rec.Actor, NR + 1)
                            Next

                            'Balance MIGIN, MIGOUT, with NET
                            For i = 1 To NR
                                If MIGIN(i) - MIGOUT(i) < MIGRANTS(i) * 1000000 Then
                                    MIGIN(i) = MIGRANTS(i) * 1000000 + MIGOUT(i)
                                ElseIf MIGIN(i) - MIGOUT(i) > MIGRANTS(i) * 1000000 Then
                                    MIGOUT(i) = MIGIN(i) - MIGRANTS(i) * 1000000
                                End If
                            Next i

                            BuildSharesMatrix(MIGIN, MIGOUT)

                            'jrs 2018/02/20 BalanceMigrationFlowsInAndOut MIGIN, MIGOUT, 1000000
                            BalanceMigrationFlowsInAndOut(MIGIN, MIGOUT, 0)
                            'AdjustMigrationDyadic 10000000, False

                            '                        'jrs 2018/01/21 moved up to use for migrants missing
                            '                        'First Year: Load Default DyadicParameters
                            '                        '-----------------------------
                            LoadDefDyadPars()
                            '                        'jrs 2018/01/22
                            '                        'First Year: Voluntary Migration
                            '                        '--------------------------------
                            '                        Dim sql As String
                            '                        'jrs 2018/02/26

                            'jrs 2018/03/21
                            'First Year: Load Forced Target
                            '------------------------------------
                            Dim rsDYADMIGFORCEDTARGETPCTPOP = (From XD In db.DYADMIGFORCEDTARGETPCTPOP Where XD.Year = FirstYear).ToList
                            For Each rec As DYADMIGFORCEDTARGETPCTPOP In rsDYADMIGFORCEDTARGETPCTPOP
                                IDYADMIGFORCEDTARGETPCTPOP(rec.Actor, rec.Partner) = rec.Val
                            Next
                            'First Year: Load Voluntary Target
                            '------------------------------------
                            Dim rsDYADMIGVOLTARGETPCTPOP = (From XD In db.DYADMIGVOLTARGETPCTPOP Where XD.Year = FirstYear).ToList
                            For Each rec As DYADMIGVOLTARGETPCTPOP In rsDYADMIGVOLTARGETPCTPOP
                                IDYADMIGVOLTARGETPCTPOP(rec.Actor, rec.Partner) = rec.Val
                            Next

                            Using conn = db.Database.GetDbConnection
                                For i = 1 To NR
                                    For j = 1 To NR
                                        'If i = 70 And j = 179 Then Stop
                                        DYADMIGVOL(i, j) = XDYADMIG(i, j) - DYADMIGFORCED(i, j) - DYADMIGFORCEDRETURN(i, j)
                                        If DYADMIGVOL(i, j) < 0 Then
                                            'jrs 2018/02/28
                                            If XDYADMIG(i, j) - DYADMIGFORCEDRETURN(i, j) < 0 Then
                                                DYADMIGFORCEDRETURN(i, j) = XDYADMIG(i, j)
                                                SDYADMIGFORCEDRETURN(i, j) = DYADMIGFORCEDRETURN(i, j)
                                                Using cmd = conn.CreateCommand()
                                                    Dim Sql1 = "UPDATE DYADMIGRANTFORCEDRETURN SET [Val] = " & CStr(DYADMIGFORCEDRETURN(i, j)) &
                                                    " WHERE Actor = " & CStr(i) & " AND Partner = " & CStr(j) & " AND Year = " & CStr(FirstYear)
                                                    cmd.CommandText = Sql1
                                                    cmd.ExecuteNonQuery()
                                                End Using
                                            End If

                                            'jrs 2018/02/26 can't change tot mig because it would mess up the balancing with MIGRANTS
                                            DYADMIGVOL(i, j) = (XDYADMIG(i, j) - DYADMIGFORCEDRETURN(i, j)) * 0.05!
                                            DYADMIGFORCED(i, j) = (XDYADMIG(i, j) - DYADMIGFORCEDRETURN(i, j)) * 0.95!
                                            SDYADMIGFORCED(i, j) = DYADMIGFORCED(i, j)
                                            Using cmd = conn.CreateCommand()
                                                Dim Sql1 = "UPDATE DYADMIGRANTFORCED SET [Val] = " & CStr(DYADMIGFORCED(i, j)) &
                         " WHERE Actor = " & CStr(i) & " AND Partner = " & CStr(j) & " AND Year = " & CStr(FirstYear)
                                                cmd.CommandText = Sql1
                                                cmd.ExecuteNonQuery()
                                            End Using

                                            'XDYADMIG(i, j) = DYADMIGVOL(i, j) + DYADMIGFORCED(i, j) + DYADMIGFORCEDRETURN(i, j)
                                            'SXDYADMIG(i, j) = XDYADMIG(i, j)
                                        End If
                                        'SDYADMIGVOL(i, j) = DYADMIGVOL(i, j)

                                        DYADPOPFOREIGNVOL(i, j) = DYADPOPFOREIGN(i, j) - DYADPOPFOREIGNFORCED(i, j)
                                        If DYADPOPFOREIGNVOL(i, j) < 0 Then
                                            DYADPOPFOREIGNVOL(i, j) = 0
                                            DYADPOPFOREIGN(i, j) = DYADPOPFOREIGNFORCED(i, j)
                                            SDYADPOPFOREIGN(i, j) = DYADPOPFOREIGN(i, j)
                                            'jrs 2020/06/19
                                            bPOPFOREIGNUPDATE(i, j) = True
                                            'Using cmd = conn.CreateCommand()
                                            '    Dim Sql1 = "UPDATE DYADPOPFOREIGN SET [Val] = " & CStr(DYADPOPFOREIGN(i, j)) &
                                            '" WHERE Actor = " & CStr(i) & " AND Partner = " & CStr(j) & " AND Year = " & CStr(FirstYear)
                                            '    cmd.CommandText = Sql1
                                            '    cmd.ExecuteNonQuery()
                                            'End Using
                                        End If
                                        'DYADPOPFOREIGNVOL(i, j) = DYADPOPFOREIGN(i, j)
                                        SDYADPOPFOREIGNVOL(i, j) = DYADPOPFOREIGNVOL(i, j)
                                        DYADPOPFOREIGN(i, j) = DYADPOPFOREIGNVOL(i, j) + DYADPOPFOREIGNFORCED(i, j)
                                        SDYADPOPFOREIGN(i, j) = DYADPOPFOREIGN(i, j)
                                    Next j
                                Next i
                            End Using

                            '---
                            'First Year: Tot migration has changed, so need to update total migration
                            '------------------------------------------------------------------
                            '            For i = 1 To NR
                            '                For j = 1 To NR
                            '                    XDYADMIG(i, j) = DYADMIGVOL(i, j) + DYADMIGFORCED(i, j) + DYADMIGFORCEDRETURN(i, j)
                            '               Next j
                            '            Next i

                            'jrs 2018/01/16
                            'First Year: Add missing migrants
                            '------------------------------------
                            For i = 1 To NR
                                For j = 1 To NR
                                    If migrantsmissingm(i, j) <> 1 Then
                                        XDYADMIG(i, j) = XDYADMIG(i, j) * migrantsmissingm(i, j)
                                    End If
                                Next j
                            Next i


                            'First Year: Forced Gravity Model
                            '--------------------------------
                            For i = 1 To NR
                                GravityForced(i) = 0
                                For j = 1 To NR
                                    If i <> j AndAlso SDYADPOPFOREIGNFORCED(i, j) > 0 Then
                                        'jrs 2018/01/28 changed GDPPCP from thousands to numbers
                                        z = CSng(0.8147626 + (-0.0578553 * Log(AMAX(DistanceBilat(i, j), 1))) + (0.0997219 * Log(POP(i))) + (-0.1751769 * Log((C(i) / POP(i)) / (C(j) / POP(j)))) + (0.2205637 * Log(GDPPCP(i) * 1000)) + (0.1618265 * Log(SDYADPOPFOREIGNFORCED(i, j) / (POP(j) * 1000000))))
                                        GravityDyadForced(i, j) = CSng(Exp(z))
                                        GravityForced(i) = GravityForced(i) + GravityDyadForced(i, j)
                                    End If
                                Next j
                            Next i

                            For i = 1 To NR
                                For j = 1 To NR
                                    If i <> j Then
                                        If GravityForced(i) > 0 Then
                                            GravityDyadForced(i, j) = GravityDyadForced(i, j) / GravityForced(i)
                                        End If
                                        SGravityDyadForced(i, j) = GravityDyadForced(i, j)
                                    End If
                                Next j
                            Next i

                            'jrs 2017/12/22
                            'First Year: Undocumented Migration
                            '--------------------------------------
                            '            For i = 1 To NR
                            '                For j = 1 To NR
                            '                    If i <> j AndAlso XDYADMIG(i, j) > 0 Then
                            '                        'jrs 2018/01/04
                            '                        If ratiomigdoctoundoc(i, j) = -100 Then
                            '                            RatioOfftoUnDoc = 0.9839688 + (0.0005411 * DistanceBilat(i, j)) + (54.85416 * (C(i) / POP(i)) / (C(j) / POP(j))) - (0.0006205 * GDPPCP(i))
                            '                        Else
                            '                            RatioOfftoUnDoc = ratiomigdoctoundoc(i, j)
                            '                        End If
                            '                        If RatioOfftoUnDoc <> 0 Then
                            '                            XDYADMIGUNDOC(i, j) = XDYADMIG(i, j) / RatioOfftoUnDoc
                            '                        Else
                            '                            XDYADMIGUNDOC(i, j) = 0
                            '                        End If
                            '                        DYADPOPFOREIGNUNDOC(i, j) = XDYADMIGUNDOC(i, j) * 20
                            '                        SDYADPOPFOREIGNUNDOC(i, j) = DYADPOPFOREIGNUNDOC(i, j)
                            '                    End If
                            '                Next j
                            '            Next i

                            For i = 1 To NR
                                SPopForeignIn(i) = 0
                                For j = 1 To NR
                                    SDYADMIGVOL(i, j) = DYADMIGVOL(i, j)
                                    SDYADPOPFOREIGN(i, j) = DYADPOPFOREIGN(i, j)
                                    SXDYADMIG(i, j) = XDYADMIG(i, j)
                                    'jrs 2018/04/26
                                    SPopForeignIn(i) = SPopForeignIn(i) + DYADPOPFOREIGN(j, i) / 1000000
                                Next j
                            Next i

                            db.Clear(GetType(XDYADMIGRANT))
                            db.Clear(GetType(MDYADMIGRANT))
                            'First Year: Save Voluntary Migration
                            '------------------------------------
                            db.Clear(GetType(DYADMIGRANTVOL))
                            'First Year: Save Voluntary PopForeign
                            '-------------------------------------
                            db.Clear(GetType(DYADPOPFOREIGNVOL))
                            'jrs 2020/06/19
                            db.Clear(GetType(DYADPOPFOREIGN))

                            'jrs 2017/12/04
                            'First Year: Gravity Model
                            '--------------------------
                            For i = 1 To NR
                                GRAVITYMIG(i) = 0
                                For j = 1 To NR
                                    If i <> j AndAlso SDYADPOPFOREIGN(i, j) > 0 Then
                                        'If GRPS(1, i) = "El Salvador" AndAlso GRPS(1, j) = "United States of America" Then Stop
                                        'jrs 2018/01/29 changed GDPPCP from thousands to numbers
                                        'jrs 2018/03/01
                                        'z = 0.8147626 + (-0.0578553 * Log(AMAX(DistanceBilat(i, j), 1))) + (0.0997219 * Log(POP(i))) + _
                                        '(-0.1751769 * Log((C(i) / POP(i)) / (C(j) / POP(j)))) + (0.2205637 * Log(GDPPCP(i) * 1000)) + _
                                        '(0.1618265 * Log(SDYADPOPFOREIGN(i, j) / (POP(j) * 1000000)))
                                        If contiguity(i, j) = 1 Then
                                            contig1 = 1
                                        Else
                                            contig1 = 0
                                        End If
                                        'jrs 2018/04/26 z = 6.135 + (0.193 * Log(POP(i))) + (0.732 * Log(POP(j))) + (0.568 * Log(100 * POPLE15(i) / POP(i))) + _
                                        '(0.477 * Log((SDYADPOPFOREIGN(i, j) / 1000000) / POP(j))) + (0.615 * contig1) + _
                                        '(-0.109 * Log((C(i) / POP(i)) / (C(j) / POP(j))))
                                        z = CSng(-4.744 _
                                        - (0.36 * Math.Log(AMAX(DistanceBilat(i, j), 1))) _
                                        + (1.033 * contig1) _
                                        + (0.318 * Math.Log(POP(i))) _
                                        + (0.546 * Math.Log(POP(j))) _
                                        - (0.364 * Math.Log((C(i) / POP(i)) / (C(j) / POP(j)))) _
                                        + (0.233 * Math.Log(SDYADPOPFOREIGN(i, j) / (POP(j) * 1000000))) _
                                        - (0.255 * Math.Log(CDbl(100 * AMAX(SPopForeignIn(j), 0.0001) / POP(j)))))


                                        GRAVITYDYADMIG(i, j) = CSng(Math.Exp(z) * 1000000)
                                        GRAVITYMIG(i) = GRAVITYMIG(i) + GRAVITYDYADMIG(i, j)
                                        'jrs 2018/01/30
                                        SHIFTGRAVITYDYADMIG(i, j) = DYADMIGVOL(i, j) / GRAVITYDYADMIG(i, j)
                                        SGRAVITYDYADMIGVAL(i, j) = GRAVITYDYADMIG(i, j)
                                        'If i = 70 AndAlso j = 179 Then Stop
                                    End If
                                Next j
                            Next i
                            For i = 1 To NR
                                For j = 1 To NR
                                    If i <> j Then
                                        If GRAVITYMIG(i) > 0 Then
                                            GRAVITYDYADMIG(i, j) = GRAVITYDYADMIG(i, j) / GRAVITYMIG(i)
                                        End If
                                        SGRAVITYDYADMIG(i, j) = GRAVITYDYADMIG(i, j)
                                    End If
                                Next j
                            Next i

                            '                        'First Year: Save Forced %Pop and Vol % Pop
                            '                        '-------------------------------------------
                            For i = 1 To NR
                                For j = 1 To NR
                                    IDYADMIGFORCEDPCTPOP(i, j) = DYADMIGFORCED(i, j) / POP(i) / 10000
                                    IDYADMIGVOLPCTPOP(i, j) = DYADMIGVOL(i, j) / POP(i) / 10000
                                Next j
                            Next i

                            db.Flush()
                            'db.Exit()

                        End Using

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
                            'jrs 2019/11/18 *** end ***

                            'Need to Save dyadic migration data
                            db.BulkInsertXDYADMIGRANT(XDYADMIG, FirstYear, NR)
                            db.BulkInsertMDYADMIGRANT(XDYADMIG, FirstYear, NR)

                            'jrs 2025/09/08 added converstion from Number to Millions
                            'jrs 2024/12/11
                            'Save Bilat to Common
                            SaveBilatToCommon(XDYADMIG, MIGRANTSBILAT, 1000000)

                            'First Year: Save Voluntary Migration
                            '------------------------------------
                            db.BulkInsertDYADMIGRANTVOL(DYADMIGVOL, FirstYear, NR)
                            'First Year: Save Voluntary PopForeign
                            '-------------------------------------
                            db.BulkInsertDYADPOPFOREIGNVOL(DYADPOPFOREIGNVOL, FirstYear, NR)
                            'jrs 2020/06/19
                            'db.BulkUpdateDYADPOPFOREIGN(DYADPOPFOREIGN, FirstYear + IY - 1, NR, bPOPFOREIGNUPDATE)
                            db.BulkInsertDYADPOPFOREIGN(DYADPOPFOREIGN, FirstYear + IY - 1, NR)
                        End Using

                    Else 'Other years ***********************************************************************************************
                        '***********************************************************************************************
                        '***********************************************************************************************
                        '***********************************************************************************************

                        LoadDetailedDyadPars()

                        'Gravity Model
                        '-------------
                        For i = 1 To NR
                            GRAVITYMIG(i) = 0
                            For j = 1 To NR
                                If i <> j AndAlso SDYADPOPFOREIGN(i, j) > 0 Then
                                    'jrs 2018/01/29 changed GDPPCP to numbers, instead of thousands
                                    'jrs 2018/03/01
                                    'z = 0.8147626 + (-0.0578553 * Log(AMAX(DistanceBilat(i, j), 1))) + _
                                    '(0.0997219 * Log(POP(i))) + (-0.1751769 * Log((sc(i) / POP(i)) / (sc(j) / POP(j)))) + _
                                    '(0.2205637 * Log(sGDPPCP(i) * 1000)) + (0.1618265 * Log(SDYADPOPFOREIGN(i, j) / (POP(j) * 1000000)))
                                    If contiguity(i, j) = 1 Then
                                        contig1 = 1
                                    Else
                                        contig1 = 0
                                    End If
                                    'jrs 2018/04/26 z = 6.135 + (0.193 * Log(POP(i))) + (0.732 * Log(POP(j))) + (0.568 * Log(100 * SPOPLE15(i) / POP(i))) + _
                                    '(0.477 * Log((SDYADPOPFOREIGN(i, j) / 1000000) / POP(j))) + (0.615 * contig1) + _
                                    '(-0.109 * Log((sc(i) / POP(i)) / (sc(j) / POP(j))))
                                    z = CSng(-4.744 _
                                    - (0.36 * Math.Log(AMAX(DistanceBilat(i, j), 1))) _
                                    + (1.033 * contig1) _
                                    + (0.318 * Math.Log(POP(i))) _
                                    + (0.546 * Math.Log(POP(j))) _
                                    - (0.364 * Math.Log((sc(i) / POP(i)) / (sc(j) / POP(j)))) _
                                    + (0.233 * Math.Log(SDYADPOPFOREIGN(i, j) / (POP(j) * 1000000))) _
                                    - (0.255 * Math.Log(CDbl(100 * AMAX(SPopForeignIn(j), 0.0001) / POP(j)))))

                                    'jrs 2018/01/29 GRAVITYDYADMIG(i, j) = Exp(z)
                                    'jrs 2018/01/29 GRAVITYMIG(i) = GRAVITYMIG(i) + GRAVITYDYADMIG(i, j)
                                    GRAVITYDYADMIGVAL(i, j) = CSng(Math.Exp(z) * 1000000)
                                    'If i = 58 AndAlso j = 51 Then Stop
                                    GRAVITYMIG(i) = GRAVITYMIG(i) + GRAVITYDYADMIGVAL(i, j)
                                End If
                            Next j
                        Next i

                        'jrs 2018/01/30
                        For i = 1 To NR
                            For j = 1 To NR
                                If i <> j AndAlso GRAVITYMIG(i) > 0 Then
                                    'jrs 2018/01/29 GRAVITYDYADMIG(i, j) = GRAVITYDYADMIG(i, j) / GRAVITYMIG(i)
                                    GRAVITYDYADMIG(i, j) = GRAVITYDYADMIGVAL(i, j) / GRAVITYMIG(i)
                                    'If i = 108 AndAlso j = 179 Then Stop
                                End If
                            Next j
                        Next i

                        'jrs 2018/03/21
                        Dim Adjustment As Single

                        'For i = 1 To NR
                        '    For j = 1 To NR
                        'jrs 2017/12/05 started using gravity
                        'jrs 2017/11/23 MIGRANTS(i) < 0 is migrants leaving country ii, only using growth if one of the countries is going the right direction
                        'since XDYAD(ii, j) is the exports from ii to j
                        'jrs 2017/11/22 added a Maximum growth rate of 100% (or a factor of 2)
                        'Forecast of MIGRANT exports
                        '                    If (MIGRANTS(i) < 0 AndAlso smigrants(i) < 0 AndAlso MIGRANTS(j) > 0 AndAlso smigrants(j) > 0) Then
                        '                        XDYADMIG(i, j) = SXDYADMIG(i, j) * Amin((MIGRANTS(i) / smigrants(i) + MIGRANTS(j) / smigrants(j)) / 2, 2)
                        '                    ElseIf MIGRANTS(i) < 0 AndAlso smigrants(i) < 0 Then
                        '                        XDYADMIG(i, j) = SXDYADMIG(i, j) * Amin(MIGRANTS(i) / smigrants(i), 2)
                        '                    ElseIf MIGRANTS(j) > 0 AndAlso smigrants(j) > 0 Then
                        '                        XDYADMIG(i, j) = SXDYADMIG(i, j) * Amin(MIGRANTS(j) / smigrants(j), 2)
                        '                    Else
                        '                        XDYADMIG(i, j) = SXDYADMIG(i, j)
                        '                    End If
                        'If GRPS(1, i) = "Honduras" AndAlso GRPS(1, j) = "United States of America" AndAlso FirstYear + IY - 1 >= 2016 Then Stop

                        'jrs 2018/01/23
                        '                    If SGRAVITYDYADMIG(i, j) > 0 Then
                        '                        XDYADMIG(i, j) = SXDYADMIG(i, j) * Amin(GRAVITYDYADMIG(i, j) / SGRAVITYDYADMIG(i, j), 2)
                        '                    Else
                        '                        XDYADMIG(i, j) = SXDYADMIG(i, j)
                        '                    End If
                        '                    SXDYADMIG(i, j) = XDYADMIG(i, j)
                        '                    SGRAVITYDYADMIG(i, j) = GRAVITYDYADMIG(i, j)
                        '
                        '                    XDYADMIG(i, j) = XDYADMIG(i, j) * tradeexportdyadm2(i, j, 4)
                        '                    XDYADMIG(i, j) = XDYADMIG(i, j) * migrateoutm(i)
                        '                    XDYADMIG(i, j) = XDYADMIG(i, j) * migrateinm(j)

                        'If i = 179 AndAlso j = 70 Then Stop

                        'jrs 2018/02/02
                        '                    'jrs 2018/01/30
                        '                    If SGRAVITYDYADMIG(i, j) > 0 Then
                        '                        DYADMIGVOL(i, j) = SDYADMIGVOL(i, j) * Amin(GRAVITYDYADMIG(i, j) / SGRAVITYDYADMIG(i, j), 2)
                        '                    Else
                        '                        DYADMIGVOL(i, j) = SDYADMIGVOL(i, j)
                        '                    End If
                        'jrs 2018/01/29
                        'jrs 2018/01/30 DYADMIGVOL(i, j) = ConvergeOverTime(DYADMIGVOL(i, j), GRAVITYDYADMIGVAL(i, j) * SHIFTGRAVITYDYADMIG(i, j), 15)
                        'DYADMIGVOL(i, j) = AMAX(GRAVITYDYADMIGVAL(i, j) * SHIFTGRAVITYDYADMIG(i, j), 0)
                        'If i = 70 AndAlso j = 179 AndAlso FirstYear + IY - 1 > 2015 Then Stop

                        'jrs 2018/03/21
                        '                    If SGRAVITYDYADMIGVAL(i, j) > 0 Then
                        '                        DYADMIGVOL(i, j) = SDYADMIGVOL(i, j) * Amin(GRAVITYDYADMIGVAL(i, j) / SGRAVITYDYADMIGVAL(i, j), 2)
                        '                    Else
                        '                        DYADMIGVOL(i, j) = SDYADMIGVOL(i, j)
                        '                    End If
                        '                    If IDYADMIGFORCEDPCTPOP(i, j) - IDYADMIGFORCEDTARGETPCTPOP(i, j) <> 0 Then
                        '                        Adjustment = 1 - (0 - IDYADMIGFORCEDTARGETPCTPOP(i, j)) / (IDYADMIGFORCEDPCTPOP(i, j) - IDYADMIGFORCEDTARGETPCTPOP(i, j))
                        '                    Else
                        '                        Adjustment = 1
                        '                    End If
                        '
                        '
                        '                    SDYADMIGVOL(i, j) = DYADMIGVOL(i, j)
                        '                    SGRAVITYDYADMIG(i, j) = GRAVITYDYADMIG(i, j)
                        '                    SGRAVITYDYADMIGVAL(i, j) = GRAVITYDYADMIGVAL(i, j)
                        '
                        '                    DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * tradeexportdyadm2(i, j, 4)
                        '                    DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * migrateoutm(i)
                        '                    DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * migrateinm(j)

                        '                Next j
                        '            Next i

                        'jrs 2018/01/19
                        'All but First: Forced Gravity
                        '--------------------------------
                        For i = 1 To NR
                            GravityForced(i) = 0
                            For j = 1 To NR
                                'If i = 150 AndAlso j = 7 And IY = 31 Then Stop
                                If i <> j AndAlso SDYADPOPFOREIGNFORCED(i, j) > 0 Then
                                    'jrs 2018/01/29 channged GDPPCP from thousands to numbers
                                    z = CSng(0.8147626 + (-0.0578553 * Math.Log(AMAX(DistanceBilat(i, j), 1))) + (0.0997219 * Math.Log(POP(i))) + (-0.1751769 * Log((sc(i) / POP(i)) / (sc(j) / POP(j)))) + (0.2205637 * Log(sGDPPCP(i) * 1000)) + (0.1618265 * Log(SDYADPOPFOREIGNFORCED(i, j) / (POP(j) * 1000000))))
                                    GravityDyadForced(i, j) = CSng(Math.Exp(z))
                                    GravityForced(i) = GravityForced(i) + GravityDyadForced(i, j)
                                Else
                                    GravityDyadForced(i, j) = 0 'jrs 2022/01/24
                                End If
                            Next j
                        Next i

                        For i = 1 To NR
                            For j = 1 To NR
                                If i <> j AndAlso GravityForced(i) > 0 Then
                                    GravityDyadForced(i, j) = GravityDyadForced(i, j) / GravityForced(i)
                                End If
                            Next j
                        Next i

                        'All but First: Advance Shares Matrix
                        '---------------------------------------
                        For i = 1 To NR
                            SharesFlowForced(i, NR + 1) = 0
                            SharesStockForced(i, NR + 1) = 0
                            SharesForced(i, NR + 1) = 0
                            For j = 1 To NR
                                'If i = 162 AndAlso j = 61 Then Stop
                                'If i = 150 AndAlso j = 7 And IY = 31 Then Stop
                                If i <> j AndAlso SGravityDyadForced(i, j) > 0 Then
                                    SharesFlowForced(i, j) = SSharesFlowForced(i, j) * GravityDyadForced(i, j) / SGravityDyadForced(i, j)
                                    SharesFlowForced(i, NR + 1) = SharesFlowForced(i, NR + 1) + SharesFlowForced(i, j)
                                    SharesStockForced(i, j) = SSharesStockForced(i, j) * GravityDyadForced(i, j) / SGravityDyadForced(i, j)
                                    SharesStockForced(i, NR + 1) = SharesStockForced(i, NR + 1) + SharesStockForced(i, j)
                                    'jrs 2018/02/28
                                    If SharesFlowForced(i, j) = 0 AndAlso IY < 5 Then
                                        SharesForced(i, j) = ConvergeOverTime(SharesFlowForced(i, j), SharesStockForced(i, j) / (5 - IY), 50)
                                    Else
                                        SharesForced(i, j) = ConvergeOverTime(SharesFlowForced(i, j), SharesStockForced(i, j), 50)
                                    End If
                                    SharesForced(i, NR + 1) = SharesForced(i, NR + 1) + SharesForced(i, j)
                                End If
                            Next j
                        Next i

                        'All but First: Save Gravity Forced after using it
                        '-----------------------------------------------------
                        For i = 1 To NR
                            For j = 1 To NR
                                SGravityDyadForced(i, j) = GravityDyadForced(i, j)
                            Next j
                        Next i

                        'All but First: Forced Migrants
                        '---------------------------------------------------
                        Dim Est As Single
                        For i = 1 To NR
                            If SMIGFORCEDEST(i) > 0 Then
                                'JRS 2018/02/26
                                'Est = 1.2825 + 17.3877 * SSFINTLWARMAG(i) / POP(i)
                                'jrs 2018/06/19 Est = AMAX((-0.076 + (1.103 * SGOVRISK(i) ^ 2)) * POP(i), 0)
                                Est = CSng(Math.Exp(3.469 - 12.8129 * SGOVINDSECUR(i)) * POP(i) / 100)
                                MIGRANTSFORCEDOUT(i) = 0
                                For j = 1 To NR
                                    MIGRANTSFORCEDOUT(i) = MIGRANTSFORCEDOUT(i) + SDYADMIGFORCED(i, j)
                                    'If MIGRANTSFORCEDOUT(i) < 0 Then Stop
                                Next j
                                'If i = 10 Then Stop
                                MIGRANTSFORCEDOUT(i) = MIGRANTSFORCEDOUT(i) * Est / SMIGFORCEDEST(i)
                                SMIGFORCEDEST(i) = Est

                                For j = 1 To NR
                                    If SharesForced(i, NR + 1) > 0 Then
                                        'If i = 150 AndAlso j = 7 And IY = 31 Then Stop
                                        DYADMIGFORCED(i, j) = MIGRANTSFORCEDOUT(i) * SharesForced(i, j) / SharesForced(i, NR + 1)
                                        SDYADMIGFORCED(i, j) = DYADMIGFORCED(i, j)
                                    End If
                                Next j

                                'If i = 70 Then Stop
                                'JRS 2018/02/26
                                'ElseIf SSFINTLWARMAG(i) > 0 Then
                            ElseIf SGOVRISK(i) > 0 Then
                                '14 pc -> 4%
                                'Est = 1.2825 + 17.3877 * SSFINTLWARMAG(i) / POP(i)
                                Est = AMAX((-0.076 + (1.103 * SGOVRISK(i) ^ 2)) * POP(i), 0)
                                MIGRANTSFORCEDOUT(i) = Est
                                SMIGFORCEDEST(i) = Est
                                For j = 1 To NR
                                    'If i = 150 AndAlso j = 7 And IY = 31 Then Stop
                                    If SharesForced(i, NR + 1) = 0 Then
                                        DYADMIGFORCED(i, j) = 0
                                    Else
                                        DYADMIGFORCED(i, j) = MIGRANTSFORCEDOUT(i) * SharesForced(i, j) / SharesForced(i, NR + 1)
                                    End If
                                    SDYADMIGFORCED(i, j) = DYADMIGFORCED(i, j)
                                Next j
                            End If

                            For j = 1 To NR
                                'If GRPS(1, j) = "Costa Rica" AndAlso GRPS(1, i) = "Venezuela" Then Stop
                                'If i = 10 AndAlso j = 29 Then Stop
                                'jrs 2018/07/06 DYADMIGFORCED(i, j) = DYADMIGFORCED(i, j) * tradeexportdyadm2(i, j, 4)
                                'jrs 2018/07/09
                                DYADMIGFORCED(i, j) = DYADMIGFORCED(i, j) * dyadmigrantforcedm(i, j)
                                DYADMIGFORCED(i, j) = DYADMIGFORCED(i, j) * migrateoutm(i)
                                DYADMIGFORCED(i, j) = DYADMIGFORCED(i, j) * migrateinm(j)
                                'JRS 2018/11/30
                                SDYADMIGFORCED(i, j) = DYADMIGFORCED(i, j)
                            Next j
                        Next i

                        'All but First: Save Shares Forced after using it
                        '------------------------------------------------
                        For i = 1 To NR
                            For j = 1 To NR
                                SSharesFlowForced(i, j) = SharesFlowForced(i, j)
                                SSharesStockForced(i, j) = SharesStockForced(i, j)
                                SSharesForced(i, j) = SharesForced(i, j)
                            Next j
                        Next i

                        'jrs 2018/04/17
                        Dim iMaxGrVol As Single

                        'jrs 2018/03/21 Move voluntary migration after Forced Migration
                        'All but First: Voluntary Migration
                        '----------------------------------
                        For i = 1 To NR
                            For j = 1 To NR
                                'If Single.IsNaN(SDYADMIGVOL(i, j)) Then Stop
                                'If i = 70 AndAlso j = 179 AndAlso IY > 2 Then Stop
                                'jrs 2018/05/04 added dyadmigvoltargetsw switch for target logic
                                If DYADMIGFORCED(i, j) > IDYADMIGFORCEDTARGETPCTPOP(i, j) * POP(i) * 10000 And
                                IDYADMIGVOLTARGETPCTPOP(i, j) > IDYADMIGVOLPCTPOP(i, j) And
                                IDYADMIGFORCEDPCTPOP(i, j) - IDYADMIGFORCEDTARGETPCTPOP(i, j) <> 0 _
                                AndAlso dyadmigvoltargetsw(i, j) > 0 Then
                                    Adjustment = AMAX(1 - (DYADMIGFORCED(i, j) / POP(i) / 10000 - IDYADMIGFORCEDTARGETPCTPOP(i, j)) / (IDYADMIGFORCEDPCTPOP(i, j) - IDYADMIGFORCEDTARGETPCTPOP(i, j)), 0)
                                    DYADMIGVOL(i, j) = (IDYADMIGVOLPCTPOP(i, j) + (Adjustment * (IDYADMIGVOLTARGETPCTPOP(i, j) - IDYADMIGVOLPCTPOP(i, j)))) * POP(i) * 10000
                                    'jrs 2018/04/03
                                    'jrs 2018/04/17 DYADMIGVOL(i, j) = Amin(DYADMIGVOL(i, j), 1.1 * SDYADMIGVOL(i, j))
                                    'If i = 70 AndAlso j = 179 Then Stop
                                    VolTarget(i, j) = DYADMIGVOL(i, j)
                                    If SDYADMIGVOL(i, j) > 0 Then
                                        iMaxGrVol = ConvergeOverTime(DYADMIGVOL(i, j) / SDYADMIGVOL(i, j), 1.1, 6)
                                    Else
                                        iMaxGrVol = 0
                                    End If
                                    DYADMIGVOL(i, j) = Amin(DYADMIGVOL(i, j), iMaxGrVol * SDYADMIGVOL(i, j))
                                Else
                                    If SGRAVITYDYADMIGVAL(i, j) > 0 Then
                                        DYADMIGVOL(i, j) = SDYADMIGVOL(i, j) * Amin(GRAVITYDYADMIGVAL(i, j) / SGRAVITYDYADMIGVAL(i, j), 2)
                                    Else
                                        DYADMIGVOL(i, j) = SDYADMIGVOL(i, j)
                                    End If
                                End If

                                SDYADMIGVOL(i, j) = DYADMIGVOL(i, j)
                                SGRAVITYDYADMIG(i, j) = GRAVITYDYADMIG(i, j)
                                SGRAVITYDYADMIGVAL(i, j) = GRAVITYDYADMIGVAL(i, j)

                                'If GRPS(1, i) = "Costa Rica" AndAlso GRPS(1, j) = "Venezuela" AndAlso IY > 8 Then Stop

                                DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * tradeexportdyadm2(i, j, 4)
                                DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * migrateoutm(i)
                                DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * migrateinm(j)

                                'jrs 2018/07/10
                                'When the voluntary flow is mainly from locals returning to their home country
                                'need to check that flow is not higher than the total population of locals in foreign country
                                'ex. Flow from China to Panama, check that the panamanians returning from China are at most the total population of Panamanians living in China
                                If DYADMIGFORCED(i, j) > DYADMIGVOL(i, j) Then
                                    NativeRatio = 0.5
                                ElseIf (SDYADPOPFOREIGNVOL(i, j) + SDYADPOPFOREIGNVOL(j, i)) > 0 Then
                                    NativeRatio = SDYADPOPFOREIGNVOL(i, j) / (SDYADPOPFOREIGNVOL(i, j) + SDYADPOPFOREIGNVOL(j, i))
                                Else
                                    NativeRatio = 0
                                End If

                                NativeRatio = NativeRatio * migsharenativem(i, j)
                                If migsharenativem(j, i) > 0 Then
                                    NativeRatio = NativeRatio / migsharenativem(j, i)
                                Else
                                    NativeRatio = 1
                                End If
                                If NativeRatio > 1 Then
                                    NativeRatio = 1
                                End If

                                If NativeRatio < 1 Then
                                    'If GRPS(1, i) = "China" AndAlso GRPS(1, j) = "Panama" Then Stop
                                    DYADMIGVOL(i, j) = Amin(DYADMIGVOL(i, j), SDYADPOPFOREIGNVOL(j, i) / (1 - NativeRatio))
                                End If

                                'jrs 2018/12/05
                                'This was messing up the entire flow, need to consider NativeRatio

                                'jrs 2018/11/15
                                'Need to be consistent with POPFOREIGN logic, where we have a limit for the people returning
                                'The case of people returning to Morocco from France in Dave's scenario
                                If DYADMIGVOL(i, j) * (1 - NativeRatio) > limit4returningmigs(j, i) * SDYADPOPFOREIGNVOL(j, i) Then
                                    DYADMIGVOL(i, j) = limit4returningmigs(j, i) * SDYADPOPFOREIGNVOL(j, i) / (1 - NativeRatio)
                                End If

                            Next j
                        Next i

                        'jrs 2018/04/20
                        'All but first: Check population limits POPFOREIGNIN SHOULD NEVER BE HIGHER THAN POP
                        '-----------------------------------------------------------------------------------
                        Dim VOLIN(), VOLOUT() As Single, LimitPOPF As Single
                        ReDim VOLIN(NR), VOLOUT(NR)

                        For i = 1 To NR
                            For j = 1 To NR
                                VOLOUT(i) = VOLOUT(i) + DYADMIGVOL(i, j)
                                VOLIN(j) = VOLIN(j) + DYADMIGVOL(i, j)
                            Next j
                        Next i

                        For i = 1 To NR
                            'jrs 2018/04/19 TOO MUCH GOING OUT OF COUNTRIES WITH FEW LOCAL PEOPLE LIKE OMAN
                            If VOLOUT(i) > 0.1 * sPOP15TO65(i) * 1000000 * (1 - Amin(SPopForeignIn(i) / POP(i), 0.99)) Then 'jrs 2020/04/24 added amin(*, 0.99)
                                'If i = 70 OrElse i = 179 Then Stop
                                'If GRPS(1, i) = "Costa Rica" AndAlso IY > 4 Then Stop
                                LimitPOPF = CSng(0.1 * sPOP15TO65(i) * 1000000 * (1 - Amin(SPopForeignIn(i) / POP(i), 0.99)) / VOLOUT(i)) 'jrs 2020/04/24 added amin(*, 0.99)
                                'Apply reduction factor to all immigrants and emmigrants
                                For j = 1 To NR
                                    DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * LimitPOPF
                                    SDYADMIGVOL(i, j) = DYADMIGVOL(i, j)
                                Next j
                                If migratebalancesw <> 1 AndAlso VOLIN(i) >= 0.000001 Then 'jrs 2020/06/18 changed from >0 to >= 0.000001
                                    'Dim diffLimit As Single 'dsb 2025/09/18
                                    'diffLimit = 1 - LimitPOPF
                                    LimitPOPF = CSng((MIGRANTS(i) + 0.1 * sPOP15TO65(i) * 1000000 * (1 - Amin(SPopForeignIn(i) / POP(i), 0.99))) / VOLIN(i)) 'jrs 2020/04/24 added amin(*, 0.99
                                    'If LimitPOPF > 1 Then Stop 'dsb 2025/10/29
                                    'If diffLimit > 0 Then
                                    '    LimitPOPF = Amin(1 + diffLimit, LimitPOPF)
                                    'End If
                                End If
                                For j = 1 To NR
                                    DYADMIGVOL(j, i) = DYADMIGVOL(j, i) * LimitPOPF
                                    SDYADMIGVOL(j, i) = DYADMIGVOL(j, i)
                                Next j
                            End If
                        Next i

                        'All but First: Return Migration
                        '-------------------------------
                        For i = 1 To NR
                            For j = 1 To NR
                                If i <> j Then
                                    If SDYADPOPFOREIGNFORCED(i, j) > 0 Then
                                        'jrs 2018/06/26 added dyadforcedreturnsignal
                                        If SDYADMIGFORCED(i, j) * dyadforcedreturnsignal(i, j) < SDYADMIGVOL(i, j) Then
                                            'If i = 1 AndAlso j = 8 Then Stop
                                            'DYADMIGFORCEDRETURN(j, i) = Amin(SDYADPOPFOREIGNFORCED(i, j) / 5, SXDYADMIG(j, i) * 0.1)
                                            'using voluntary migration as limit becuase that's the rate at which they become voluntary popforeign from refugees
                                            If SDYADMIGVOL(i, j) > SDYADPOPFOREIGNFORCED(i, j) / 5 Then
                                                DYADMIGFORCEDRETURN(j, i) = 0
                                            Else
                                                'jrs 2018/02/23
                                                If IY - DYADMIGFORRETLST0(j, i) > 2 Then
                                                    DYADMIGFORCEDRETURN(j, i) = SDYADPOPFOREIGNFORCED(i, j) / 5 - DYADMIGVOL(i, j)
                                                ElseIf IY - DYADMIGFORRETLST0(j, i) > 1 Then
                                                    DYADMIGFORCEDRETURN(j, i) = (SDYADPOPFOREIGNFORCED(i, j) / 5 - DYADMIGVOL(i, j)) / 2
                                                Else
                                                    DYADMIGFORCEDRETURN(j, i) = (SDYADPOPFOREIGNFORCED(i, j) / 5 - DYADMIGVOL(i, j)) / 4
                                                End If
                                                'jrs 2018/07/06
                                                DYADMIGFORCEDRETURN(j, i) = AMAX(DYADMIGFORCEDRETURN(j, i), 0)
                                            End If
                                            'jrs 2018/02/28
                                        ElseIf (SDYADMIGFORCEDRETURN(j, i) > 0 AndAlso IY < 6) Then
                                            DYADMIGFORCEDRETURN(j, i) = Amin(SDYADMIGFORCEDRETURN(j, i) / 2, SDYADPOPFOREIGNFORCED(i, j) / 5)
                                        Else
                                            DYADMIGFORCEDRETURN(j, i) = 0
                                        End If
                                    Else
                                        DYADMIGFORCEDRETURN(j, i) = 0
                                    End If
                                    'jrs 2018/04/16
                                    'Add limit to Forced Return
                                    'If i = 70 AndAlso j = 179 AndAlso IY > 16 Then Stop
                                    DYADMIGFORCEDRETURN(j, i) = Amin(DYADMIGFORCEDRETURN(j, i), SXDYADMIG(i, j) * dyadforcedreturnlimitm(j, i))

                                    DYADMIGFORCEDRETURN(j, i) = DYADMIGFORCEDRETURN(j, i) + dyadmigforcedreturnadd(j, i)
                                    SDYADMIGFORCEDRETURN(j, i) = DYADMIGFORCEDRETURN(j, i)
                                    'jrs 2018/02/23
                                    If DYADMIGFORCEDRETURN(j, i) = 0 Then
                                        DYADMIGFORRETLST0(j, i) = IY
                                    End If
                                End If
                            Next j
                        Next i
                        'All but First: Total Migration
                        '------------------------------
                        For i = 1 To NR
                            For j = 1 To NR
                                XDYADMIG(i, j) = DYADMIGVOL(i, j) + DYADMIGFORCED(i, j) + DYADMIGFORCEDRETURN(i, j)
                                'If Single.IsNaN(XDYADMIG(i, j)) Then Stop
                            Next j
                        Next i

                        'jrs 2017/11/24
                        'All but First: Save to pooled Net Migration
                        '---------------------------------------------
                        If migratebalancesw <> 0 Then
                            AdjustNetMigration()
                        End If

                        'jrs 2017/11/24
                        'All but First: Adjust Dyadic Migration to match Net
                        '------------------------------------------------------
                        Dim diff As Single, Tot As Single, Fact As Single
                        If migratebalancesw <> 1 Then
                            'jrs 2024/11/01
                            Dim InTot As Double, OutTot As Double
                            InTot = 0
                            OutTot = 0
                            'Balance MIGIN, MIGOUT, with NET
                            For i = 1 To NR
                                'If IY = 26 And GRPS(1, i) = "Iceland" Then Stop
                                diff = (MIGRANTS(i) - smigrants(i)) * 1000000
                                'jrs 2018/01/24 insted of using Tot I was using the SUM, but MIGIN had change for the 2nd line, so not fair
                                Tot = MIGIN(i) + MIGOUT(i)
                                If Tot > 0 Then
                                    'jrs 2024/11/02 
                                    'Making sure this doesn't go negative
                                    If MIGIN(i) + diff * MIGIN(i) / Tot > 0 And MIGOUT(i) - diff * MIGOUT(i) / Tot > 0 Then
                                        MIGIN(i) = MIGIN(i) + diff * MIGIN(i) / Tot
                                        MIGOUT(i) = MIGOUT(i) - diff * MIGOUT(i) / Tot
                                    Else
                                        If diff > 0 Then 'All to immigration
                                            MIGIN(i) = MIGIN(i) + diff
                                        Else 'all to emmigration
                                            MIGOUT(i) = MIGOUT(i) - diff
                                        End If
                                    End If
                                    'jrs 2024/11/01
                                    InTot = InTot + MIGIN(i)
                                    OutTot = OutTot + MIGOUT(i)
                                Else
                                    If Tot < 0 Or MIGIN(i) < 0 Or MIGOUT(i) < 0 Then Stop 'There shouldn't be negatives, if it stops go check what's going on
                                End If
                            Next i

                            'jrs 2024/11/01
                            'These 2 variables are only used for comparisson to check we're making progress
                            Dim InTot2 As Double = 0
                            Dim OutTot2 As Double = 0
                            If InTot <> OutTot Then
                                If InTot > 0 And OutTot > 0 Then
                                    'For some reason this makes it work, maybe rounding
                                    'Dim Avg As Double
                                    'Avg = (InTot + OutTot) / 2
                                    Dim diff2 As Double
                                    diff2 = InTot - OutTot

                                    For i = 1 To NR
                                        If diff2 > 0 Then
                                            MIGIN(i) = CSng(MIGIN(i) - diff2 * MIGIN(i) / InTot)
                                        Else
                                            MIGOUT(i) = CSng(MIGOUT(i) + diff2 * MIGOUT(i) / OutTot)
                                        End If
                                        'MIGIN(i) = CSng(MIGIN(i) * Avg / InTot)
                                        'MIGOUT(i) = CSng(MIGOUT(i) * Avg / OutTot)
                                        InTot2 = InTot2 + MIGIN(i)
                                        OutTot2 = OutTot2 + MIGOUT(i)
                                    Next
                                Else
                                    For i = 1 To NR
                                        MIGIN(i) = 0
                                        MIGOUT(i) = 0
                                    Next
                                End If
                            End If

                            'If IY = 46 Then Stop

                            BuildSharesMatrix(MIGIN, MIGOUT)

                            'jrs 2018/02/20 BalanceMigrationFlowsInAndOut MIGIN, MIGOUT, 1000000
                            BalanceMigrationFlowsInAndOut(MIGIN, MIGOUT, 0)

                            'All but first: need to readjust voluntary and forced migration
                            '---------------------------------------------------------------
                            For i = 1 To NR
                                For j = 1 To NR
                                    If i <> j AndAlso (DYADMIGVOL(i, j) + DYADMIGFORCED(i, j) + DYADMIGFORCEDRETURN(i, j)) > 0 Then
                                        If XDYADMIG(i, j) > DYADMIGFORCEDRETURN(i, j) + DYADMIGFORCED(i, j) AndAlso DYADMIGVOL(i, j) >= 0.000001 Then 'jrs 2020/06/22 changed from DYADMIGVOL(i, j) > 0
                                            Fact = (XDYADMIG(i, j) - DYADMIGFORCEDRETURN(i, j) - DYADMIGFORCED(i, j)) / (DYADMIGVOL(i, j))
                                            DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * Fact
                                            SDYADMIGVOL(i, j) = DYADMIGVOL(i, j)
                                        ElseIf XDYADMIG(i, j) > DYADMIGFORCEDRETURN(i, j) AndAlso DYADMIGVOL(i, j) + DYADMIGFORCED(i, j) >= 0.000001 Then 'jrs 2020/06/22 changed from > 0
                                            Fact = (XDYADMIG(i, j) - DYADMIGFORCEDRETURN(i, j)) / (DYADMIGVOL(i, j) + DYADMIGFORCED(i, j))
                                            DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * Fact
                                            SDYADMIGVOL(i, j) = DYADMIGVOL(i, j)
                                            DYADMIGFORCED(i, j) = DYADMIGFORCED(i, j) * Fact
                                            SDYADMIGFORCED(i, j) = DYADMIGFORCED(i, j)
                                        Else
                                            Fact = XDYADMIG(i, j) / (DYADMIGVOL(i, j) + DYADMIGFORCED(i, j) + DYADMIGFORCEDRETURN(i, j))
                                            DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * Fact
                                            SDYADMIGVOL(i, j) = DYADMIGVOL(i, j)
                                            DYADMIGFORCED(i, j) = DYADMIGFORCED(i, j) * Fact
                                            SDYADMIGFORCED(i, j) = DYADMIGFORCED(i, j)
                                            DYADMIGFORCEDRETURN(i, j) = DYADMIGFORCEDRETURN(i, j) * Fact
                                            SDYADMIGFORCEDRETURN(i, j) = DYADMIGFORCEDRETURN(i, j)
                                        End If
                                    End If
                                Next j
                            Next i
                        End If

                        'All but First: Total Migration
                        '------------------------------
                        For i = 1 To NR
                            For j = 1 To NR
                                ''If i = 179 AndAlso j = 70 Then Stop
                                '                    XDYADMIG(i, j) = DYADMIGVOL(i, j) + DYADMIGFORCED(i, j) + DYADMIGFORCEDRETURN(i, j)
                                'jrs 2018/04/16
                                SXDYADMIG(i, j) = XDYADMIG(i, j)
                            Next j
                        Next i

                        'jrs 2018/01/16
                        'All but First: Add missing migrants
                        '------------------------------------
                        For i = 1 To NR
                            For j = 1 To NR
                                If migrantsmissingm(i, j) <> 1 Then
                                    XDYADMIG(i, j) = XDYADMIG(i, j) * migrantsmissingm(i, j)
                                    DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * migrantsmissingm(i, j)
                                End If
                            Next j
                        Next i

                        'jrs 2017/12/22
                        'Other yeas: Undocumented Migration
                        '------------------------------------
                        '            For i = 1 To NR
                        '                For j = 1 To NR
                        '                    If i <> j AndAlso XDYADMIG(i, j) > 0 Then
                        '                        'jrs 2018/01/04
                        '                        If ratiomigdoctoundoc(i, j) = -100 Then
                        '                            RatioOfftoUnDoc = 0.9839688 + (0.0005411 * DistanceBilat(i, j)) + (54.85416 * (sc(i) / POP(i)) / (sc(j) / POP(j))) - (0.0006205 * sGDPPCP(i))
                        '                        Else
                        '                            RatioOfftoUnDoc = ratiomigdoctoundoc(i, j)
                        '                        End If
                        '                        If RatioOfftoUnDoc <> 0 Then
                        '                            XDYADMIGUNDOC(i, j) = XDYADMIG(i, j) / RatioOfftoUnDoc
                        '                        Else
                        '                            XDYADMIGUNDOC(i, j) = 0
                        '                        End If
                        '                    End If
                        '                Next j
                        '            Next i


                        'save adjusted migration
                        '            For i = 1 To NR
                        '                For j = 1 To NR
                        '                    If tradeexportdyadm2(i, j, 4) <> 0 AndAlso migrateoutm(i) <> 0 AndAlso migrateinm(j) <> 0 Then
                        '                        SXDYADMIG(i, j) = XDYADMIG(i, j) / tradeexportdyadm2(i, j, 4)
                        '                        SXDYADMIG(i, j) = SXDYADMIG(i, j) / migrateoutm(i)
                        '                        SXDYADMIG(i, j) = SXDYADMIG(i, j) / migrateinm(j)
                        '                    Else
                        '                        SXDYADMIG(i, j) = 0
                        '                    End If
                        '                Next j
                        '            Next i


                        For i = 1 To NR
                            For j = 1 To NR
                                If i <> j Then
                                    'If GRPS(1, i) = "Guatemala" AndAlso j = 79 Then Stop
                                    'jrs 2018/01/23
                                    '                        'jrs 2018/01/19
                                    '                        If (SDYADPOPFOREIGN(i, j) + SDYADPOPFOREIGN(j, i)) > 0 Then
                                    '                            NativeRatio = SDYADPOPFOREIGN(j, i) / (SDYADPOPFOREIGN(i, j) + SDYADPOPFOREIGN(j, i))
                                    '                        Else
                                    '                            NativeRatio = 0
                                    '                        End If
                                    'Plus immigrants, minus deaths, minus returning migrants
                                    '                        DYADPOPFOREIGN(i, j) = AMAX(SDYADPOPFOREIGN(i, j) + XDYADMIG(i, j) _
                                    '                        - (SDEATHS(j) / POP(j) * migdeathratesw(i, j) + SDEATHS(i) / POP(i) * (1 - migdeathratesw(i, j))) * SDYADPOPFOREIGN(i, j) _
                                    '                        - Amin(XDYADMIG(j, i) * migsharenativem(j, i), limit4returningmigs(i, j) * SDYADPOPFOREIGN(i, j)), 0)

                                    'jrs 2018/01/23
                                    '                        DYADPOPFOREIGN(i, j) = AMAX(SDYADPOPFOREIGN(i, j) + XDYADMIG(i, j) _
                                    '                        - (SDEATHS(j) / POP(j) * migdeathratesw(i, j) + SDEATHS(i) / POP(i) * (1 - migdeathratesw(i, j))) * SDYADPOPFOREIGN(i, j) _
                                    '                        - Amin(XDYADMIG(j, i) * NativeRatio, limit4returningmigs(i, j) * SDYADPOPFOREIGN(i, j)), 0)
                                    '                        SDYADPOPFOREIGN(i, j) = DYADPOPFOREIGN(i, j)

                                    'jrs 2018/04/17 added max(volTarget)
                                    If DYADMIGFORCED(i, j) > AMAX(DYADMIGVOL(i, j), VolTarget(i, j)) Then
                                        NativeRatio = 0.5
                                    ElseIf (SDYADPOPFOREIGNVOL(i, j) + SDYADPOPFOREIGNVOL(j, i)) > 0 Then
                                        NativeRatio = SDYADPOPFOREIGNVOL(i, j) / (SDYADPOPFOREIGNVOL(i, j) + SDYADPOPFOREIGNVOL(j, i))
                                    Else
                                        NativeRatio = 0
                                    End If

                                    'jrs 2018/06/19
                                    NativeRatio = NativeRatio * migsharenativem(i, j)
                                    If migsharenativem(j, i) > 0 Then
                                        NativeRatio = NativeRatio / migsharenativem(j, i)
                                    Else
                                        NativeRatio = 1
                                    End If
                                    If NativeRatio > 1 Then
                                        NativeRatio = 1
                                    End If


                                    'jrs 2018/04/17 added max(volTarget)
                                    'If GRPS(1, i) = "Morocco" AndAlso GRPS(1, j) = "France" AndAlso IY > 7 Then Stop 'And IY > 7
                                    'jrs 2018/06/18 added NativeRatio to VolFlow (only foreigners enterign the flow, not returnees)
                                    'Also from the reversed flow remove the part that are returnees
                                    'Plus immigrants, plus regugees becoming permanent, minus deaths, minus returning migrants
                                    'If Single.IsNaN(SDYADPOPFOREIGNVOL(i, j)) Then Stop
                                    DYADPOPFOREIGNVOL(i, j) = AMAX(SDYADPOPFOREIGNVOL(i, j) + DYADMIGVOL(i, j) * NativeRatio _
                       + Amin(SDYADPOPFOREIGNFORCED(i, j), AMAX(DYADMIGVOL(i, j), VolTarget(i, j))) _
                        - (SDEATHS(j) / POP(j) * migdeathratesw(i, j) + SDEATHS(i) / POP(i) * (1 - migdeathratesw(i, j))) * SDYADPOPFOREIGNVOL(i, j) _
                        - Amin(DYADMIGVOL(j, i) * NativeRatio, limit4returningmigs(i, j) * SDYADPOPFOREIGNVOL(i, j)), 0)
                                    SDYADPOPFOREIGNVOL(i, j) = DYADPOPFOREIGNVOL(i, j)

                                    'jrs 2017/12/22
                                    'All but First: Undocumented foreign population
                                    '----------------------------------------------
                                    'DYADPOPFOREIGNUNDOC(i, j) = AMAX(SDYADPOPFOREIGNUNDOC(i, j) + XDYADMIGUNDOC(i, j) _
                                    '- (SDEATHS(j) / POP(j) * migundocdeathratesw(i, j) + SDEATHS(i) / POP(i) * (1 - migdeathratesw(i, j))) * SDYADPOPFOREIGNUNDOC(i, j) _
                                    '- Amin(XDYADMIGUNDOC(j, i) * migundocsharenativem(j, i), limit4returningundocmigs(i, j) * SDYADPOPFOREIGNUNDOC(i, j)), 0)

                                    'SDYADPOPFOREIGN(i, j) = DYADPOPFOREIGN(i, j)
                                    'SDYADPOPFOREIGNUNDOC(i, j) = DYADPOPFOREIGNUNDOC(i, j)
                                End If
                            Next j
                        Next i


                        'All but First: PopForeign Forced
                        '---------------------------------
                        For i = 1 To NR
                            For j = 1 To NR
                                If i <> j Then
                                    'If i = 179 AndAlso j = 50 Then Stop
                                    'jrs 2018/04/17 added max(volTarget)
                                    'Plus immigrants, minus regugees becoming permanent, minus deaths, minus returning migrants
                                    DYADPOPFOREIGNFORCED(i, j) = AMAX(SDYADPOPFOREIGNFORCED(i, j) + DYADMIGFORCED(i, j) _
                        - Amin(SDYADPOPFOREIGNFORCED(i, j), AMAX(DYADMIGVOL(i, j), VolTarget(i, j))) _
                        - (SDEATHS(j) / POP(j) * migdeathratesw(i, j) + SDEATHS(i) / POP(i) * (1 - migdeathratesw(i, j))) * SDYADPOPFOREIGNFORCED(i, j) _
                        - Amin(DYADMIGFORCEDRETURN(j, i), SDYADPOPFOREIGNFORCED(i, j)), 0)
                                    SDYADPOPFOREIGNFORCED(i, j) = DYADPOPFOREIGNFORCED(i, j)
                                End If
                            Next j
                        Next i

                        'jrs 2018/01/22
                        'All but First: Total Pop Foreign
                        '----------------------------------
                        For i = 1 To NR
                            For j = 1 To NR
                                If i <> j Then
                                    DYADPOPFOREIGN(i, j) = DYADPOPFOREIGNVOL(i, j) + DYADPOPFOREIGNFORCED(i, j)
                                    SDYADPOPFOREIGN(i, j) = DYADPOPFOREIGN(i, j)
                                End If
                            Next j
                        Next i

                        Using db = New IFsForDyadicContext()
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
                            'jrs 2019/11/18 *** end ***

                            'All but First: Save Total Migrants
                            '------------------------------------
                            db.BulkInsertXDYADMIGRANT(XDYADMIG, FirstYear + IY - 1, NR)
                            db.BulkInsertMDYADMIGRANT(XDYADMIG, FirstYear + IY - 1, NR)

                            'jrs 2024/12/11
                            'Save Bilat to Common
                            SaveBilatToCommon(XDYADMIG, MIGRANTSBILAT, 1000000)

                            'All but First: Save Forced Migrants
                            '------------------------------------
                            db.BulkInsertDYADMIGRANTFORCED(DYADMIGFORCED, FirstYear + IY - 1, NR)
                            'All but First: Save Forced Return Migrants
                            '------------------------------------------
                            db.BulkInsertDYADMIGRANTFORCEDRETURN(DYADMIGFORCEDRETURN, FirstYear + IY - 1, NR)
                            'All but First: Save Voluntary Migrants
                            '------------------------------------
                            db.BulkInsertDYADMIGRANTVOL(DYADMIGVOL, FirstYear + IY - 1, NR)
                            'All but First: Save Total Pop Foreign
                            '-------------------------------------
                            db.BulkInsertDYADPOPFOREIGN(DYADPOPFOREIGN, FirstYear + IY - 1, NR)
                            'All but First: Save Voluntary Pop Foreign
                            '-------------------------------------
                            db.BulkInsertDYADPOPFOREIGNVOL(DYADPOPFOREIGNVOL, FirstYear + IY - 1, NR)
                            'All but First: Save Forced Pop Foreign
                            '-----------------------------------------
                            db.BulkInsertDYADPOPFOREIGNFORCED(DYADPOPFOREIGNFORCED, FirstYear + IY - 1, NR)
                        End Using

                    End If 'if year =1 else other years
                    'jrs 2019/07/18 *** translate later: end ***

                    'jrs 2017/12/04
                    For i = 1 To NR
                        MIGRANTSOUT(i) = 0
                        MIGRANTSIN(i) = 0
                    Next i

                    For i = 1 To NR
                        For j = 1 To NR
                            If XDYADMIG(i, j) > 0 Then
                                MIGRANTSOUT(i) = MIGRANTSOUT(i) + XDYADMIG(i, j) / 1000000
                                MIGRANTSIN(j) = MIGRANTSIN(j) + XDYADMIG(i, j) / 1000000
                            End If
                        Next j
                    Next i

                    'If IY = 46 Then Stop

                    For i = 1 To NR
                        'If Abs(MIGRANTS(i) - MIGRANTSIN(i) + MIGRANTSOUT(i)) > 0.06 AndAlso IY > 1 Then Stop
                        MIGRATEOUT(i) = MIGRANTSOUT(i) / POP(i) * 100
                        MIGRATEIN(i) = MIGRANTSIN(i) / POP(i) * 100
                    Next i

                    'jrs 2018/01/19
                    If IY = 1 Then
                        For i = 1 To NR
                            MIGRANTSFORCEDOUT(i) = 0
                            MIGRANTSFORCEDRETURN(i) = 0
                            MIGRANTSVOLIN(i) = 0
                            MIGRANTSVOLOUT(i) = 0
                            POPREFUGEE(i) = 0
                            'jrs 2018/03/16
                            POPFOREIGNOUT(i) = 0
                            POPFOREIGNIN(i) = 0
                        Next i

                        For i = 1 To NR
                            For j = 1 To NR
                                If DYADMIGFORCED(i, j) > 0 Then
                                    MIGRANTSFORCEDOUT(i) = MIGRANTSFORCEDOUT(i) + DYADMIGFORCED(i, j) / 1000000
                                End If
                                If DYADMIGFORCEDRETURN(i, j) > 0 Then
                                    MIGRANTSFORCEDRETURN(j) = MIGRANTSFORCEDRETURN(j) + DYADMIGFORCEDRETURN(i, j) / 1000000
                                End If
                                If DYADMIGVOL(i, j) > 0 Then
                                    MIGRANTSVOLIN(j) = MIGRANTSVOLIN(j) + DYADMIGVOL(i, j) / 1000000
                                    MIGRANTSVOLOUT(i) = MIGRANTSVOLOUT(i) + DYADMIGVOL(i, j) / 1000000
                                End If
                                If DYADPOPFOREIGNFORCED(i, j) > 0 Then
                                    'jrs 2018/06/22 added / 1000000
                                    POPREFUGEE(j) = POPREFUGEE(j) + DYADPOPFOREIGNFORCED(i, j) / 1000000
                                End If
                                'jrs 2018/03/16
                                POPFOREIGNOUT(i) = POPFOREIGNOUT(i) + DYADPOPFOREIGN(i, j) / 1000000
                                POPFOREIGNIN(j) = POPFOREIGNIN(j) + DYADPOPFOREIGN(i, j) / 1000000
                                'jrs 2018/04/03
                                SPopForeignIn(j) = POPFOREIGNIN(j)
                            Next j
                        Next i
                    Else

                        For i = 1 To NR
                            MIGRANTSFORCEDRETURN(i) = 0
                            MIGRANTSVOLIN(i) = 0
                            MIGRANTSVOLOUT(i) = 0
                            POPREFUGEE(i) = 0
                            'jrs 2018/03/16
                            POPFOREIGNOUT(i) = 0
                            POPFOREIGNIN(i) = 0
                        Next i

                        For i = 1 To NR
                            MIGRANTSFORCEDOUT(i) = MIGRANTSFORCEDOUT(i) / 1000000
                            For j = 1 To NR
                                If DYADMIGFORCEDRETURN(i, j) > 0 Then
                                    'jrs 2018/01/24
                                    MIGRANTSFORCEDRETURN(j) = MIGRANTSFORCEDRETURN(j) + DYADMIGFORCEDRETURN(i, j) / 1000000
                                End If
                                If DYADMIGVOL(i, j) > 0 Then
                                    'If j = 70 Then Stop
                                    MIGRANTSVOLIN(j) = MIGRANTSVOLIN(j) + DYADMIGVOL(i, j) / 1000000
                                    MIGRANTSVOLOUT(i) = MIGRANTSVOLOUT(i) + DYADMIGVOL(i, j) / 1000000
                                End If
                                If DYADPOPFOREIGNFORCED(i, j) > 0 Then
                                    'jrs 2018/06/22 added / 1000000
                                    POPREFUGEE(j) = POPREFUGEE(j) + DYADPOPFOREIGNFORCED(i, j) / 1000000
                                End If
                                'jrs 2018/03/16
                                POPFOREIGNOUT(i) = POPFOREIGNOUT(i) + DYADPOPFOREIGN(i, j) / 1000000
                                'If Single.IsNaN(DYADPOPFOREIGN(i, j)) Then Stop
                                POPFOREIGNIN(j) = POPFOREIGNIN(j) + DYADPOPFOREIGN(i, j) / 1000000
                                'jrs 2018/04/03
                                SPopForeignIn(j) = POPFOREIGNIN(j)
                            Next j
                        Next i
                    End If

                End If 'Doing bilateral migration

            Catch ex As Exception

            End Try

        End Sub

        'BBH August 19, 2009 Begin
        '=============================Population.BAS  =======================
        Private Sub SavingsDemogAdj()
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            If IY% = 1 Then
                'bbh 2009/09/09  Take
                ReDim savingsnetcoh(NCohorts), SavingsNetCohMDC(NCohorts), SavingsNetCohLDC(NCohorts), savingsagei(NR%), SavingsAgeAdj(NR%)
                '
                '   Fill savings vector by age category
                '
                'jrs 2009 / 8 / 20
                Using db As IFsContext = New IFsContext()
                    For Each item In db.IncConSav
                        SavingsNetCohMDC(CInt(item.AgeC)) = CSng(item.SavingsMDC)
                    Next
                    'bbh 2009/09/09
                    For Each item In db.IncConSav
                        SavingsNetCohLDC(CInt(item.AgeC)) = CSng(item.SavingsLDC)
                    Next
                End Using
            End If

            '
            '   Compute age-adjusted net savings adjustment
            '

            Dim savingscalc As Double
            For R% = 1 To NR%
                For j% = 1 To NCohorts
                    If SLIFEXPFin(R%, 3) > 80 Then
                        savingsnetcoh(j%) = SavingsNetCohMDC(j%)
                    ElseIf SLIFEXPFin(R%, 3) < 40 Then
                        savingsnetcoh(j%) = SavingsNetCohLDC(j%)
                    Else
                        savingsnetcoh(j%) = SavingsNetCohLDC(j%) + (SLIFEXPFin(R%, 3) - 40) / 40 * (SavingsNetCohMDC(j%) - SavingsNetCohLDC(j%))
                    End If
                Next j%

                savingscalc = 0
                For j% = 1 To NCohorts
                    savingscalc = savingscalc + savingsnetcoh(j%) * (agedst(j%, R%, 1) + agedst(j%, R%, 2))
                Next j%
                savingscalc = savingscalc / POP(R%)
                If IY% = 1 Then savingsagei(R%) = savingscalc
                'bbh August 29, 2009
                SavingsAgeAdj(R%) = CSng((savingscalc - savingsagei(R%)) * SGDPinPOP(R%) * 0.2)
                'If RegName$(R%) = "Azerbaijan" Then Stop
                'Savings is about 20 percent of GDP on average
            Next R%
            'BBH August 19, 2009 End


        End Sub

        'jrs 2013/05/03
        Private Function RetirementAgeToPreserveSavingsRate(ByRef LE As Single, SavingsRt As Single, workageentry As Single, DiscountRt As Single) As Single
            If workageentry > LE Then
                RetirementAgeToPreserveSavingsRate = LE
                Exit Function
            End If
            If SavingsRt < 0 Then
                RetirementAgeToPreserveSavingsRate = LE
                Exit Function
            End If
            'jrs 2016/02/23 Description added
            'This formula comes from making FV of annuities of SavingRt (20 or less) durig a period of RetirementAge - (WorkAgeEntry + 10)
            'Equal to PV of annuities of 100 for the remaining of your life, (LE - RetirementAge)

            'jrs 2013/05/27 adding new parameter for savings discount rate (3%)
            'jrs 2013/07/12 added + 1 to LE
            RetirementAgeToPreserveSavingsRate = CSng(Math.Log((SavingsRt + 100) / (SavingsRt / (1 + DiscountRt / 100) ^ (workageentry + 10) +
        100 / (1 + DiscountRt / 100) ^ (LE + 1))) / Math.Log(1 + DiscountRt / 100))

        End Function

        'jrs 2024/06/18
        Dim TFNameContraception As String
        'jrs 2024/07/04
        Dim TFNameUrbPct As String

        Public Sub ifsPOP2AllYears(context As IForecastContext)
            Dim zy As Integer
            Dim ConAdd, concomp, concomp2, contrbase, contrcomp As Single
            Dim contrexcess As Single, tfrcompi As Single, tfrcomptar As Single
            Dim tfrcomptar2 As Single, TimeFAc As Single, TFRMinReg As Single
            Dim mininit As Single, minbound As Single
            Dim TFRgrComp As Single, TFRgrSmoothed As Single
            Dim ultmult As Double
            Dim agehalflifeT As Single, ageinitT As Single, agepeakT As Single, agestopT As Single
            Dim peaklevelT As Single
            Dim fertdstm As Double
            Dim PredHlthSpend As Single, hlthspendrgdp As Single, hlthshiftmax As Single, elaslifspnd As Single
            Dim hlthcoef As Single, lifexpspendshift As Single, IncTerm1 As Single, IncTerm2 As Single
            Dim BalTerm As Single, Headroom As Single, MaxIncrease As Single, ultlifetar As Single
            Dim mFactorM As Single, mFactorF As Single, mfactor As Single
            Dim clav As Single, clsf As Single
            Dim MaleAIDSPortion As Single, maleaidsdths As Single, femaleaidsdths As Single, aidsdthscoh As Single
            Dim ChilSdeath As Single, calcinf As Single, lagcoef As Single, cumMort As Single
            Dim nqx As Single, POPMale As Double, POPFemale As Double
            Dim infMort As Single, cohMort As Single
            Dim lx, N, nax As Single
            Dim DeathCount As Double
            'jrs 2020/08/31 Dim PNO As Single, PSO As Single, qlno As Single, qlso As Single
            Dim POPURBANPOR As Single, PopLandRat As Single, PopUrbanRat As Single

            '   All Years 2
            '   -----------
            zy = IY% - 1
            'jrs 2007/06/19 changed 80 with a variable to make sure we include target values in the first year
            'lifexpmax = 80 * (1 - zy * tmortr)
            For gend% = 1 To 2
                LifExpMax(gend%) = lifexpmaxi(gend%) * (1 - zy * tmortr)
            Next gend%
            '80 posited to be max in 2000; Austria at 78
            '.0015 in 100 years on top of 80 yields 92 years

            'mti 2022/11/25 start - move entire marraige demography to a new procedure as IFsPop got greater than allowed size

            sErrorSub = "IFsPopMarriage1"
            If IY = 1 Then
                Call IFsPopMarriage1FirstYear(context)
            Else
                Call IFsPopMarriage1AllbutFirst(context)
            End If
            sErrorSub = "IFsPop2"
            'mti 2022/11/25 End

            'jrs 2024/06/18
            If IY = 1 Then
                TFNameContraception = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Contraception Use (" & BaseYear & ")"
                TFNameContraception = getFunctionNameYrOrMstRct(TFNameContraception, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            End If

            'ttt 2019/10/08 TakeOut
            Array.Copy(GDPPCP, sGDPPCP, NR + 1)
            Array.Copy(GDP, SGDPinPOP, NR + 1)
            Array.Copy(GDPPC, sGDPPC, NR + 1)
            For R% = 1 To NR%
                'ttt 2019/10/08 TakenOut
                'sGDPPCP(R%) = GDPPCP(R%)
                'bbh August 29, 2009
                'SGDPinPOP(R%) = GDP(R%)
                ''mti September 2017 Start
                'sGDPPC(R%) = GDPPC(R%)
                'mti September 2017 End
                'Already ReDim

                deathchk(R%) = 0
                birthchk(R%) = 0
                popchk(R%) = 0
                popchk2(R%) = 0
                popchk3(R%) = 0
                For ICount% = 0 To NAges
                    popchk(R%) = popchk(R%) + fagedst(ICount%, R%, 1) + fagedst(ICount%, R%, 2)
                Next ICount%
                If IY% = 1 Then GDPPCI(R%) = GDPPC(R%)

                '   BBH 5/7/04 Begin V5_0
                '   All Years 2:  Income Share
                '   --------------------------
                '        If IY% = 1 Then IncShrI(R%) = INCSHR(R%)
                '        Call XYTABL("GDP/Capita (PPP) Versus Income Share of Lowest 20% (1995)", _
                '            GDPPCP(R%), IncComp)
                '        Call XYTABL("GDP/Capita (PPP) Versus Income Share of Lowest 20% (1995)", _
                '            GDPPCIP(R%), IncComp2)
                '        IncMul = AMAX(0.01, IncComp) / AMAX(0.01, IncComp2)
                '        incshrbase = IncShrI(R%) * IncMul
                '        If IY% > 1 Then INCSHR(R%) = AMAX(0.01, incshrbase * incshrm(R%))
                '   BBH 5/7/04 End V5_0

                'mti 2022/11/25 start - move entire marraige demography to a new procedure as IFsPop got greater than allowed size
                'mti 2022/11/25 End

                '   All Years 2:  Contraceptive Use
                '   -------------------------------

                If context.HistoricMode = True Then
                    Call XYTABL("GDP/Capita (PPP) Versus Contraception Use (1975)",
                    GDPPCP(R%), concomp, context)
                    Call XYTABL("GDP/Capita (PPP) Versus Contraception Use (1975)",
                    GDPPCIP(R%), concomp2, context)
                Else
                    'jrs 2024/06/18 can't use the search function yet, function is too old
                    Call XYTABL("GDP/Capita (PPP) Versus Contraception Use (1990)",
                    GDPPCP(R%), concomp, context)
                    Call XYTABL("GDP/Capita (PPP) Versus Contraception Use (1990)",
                    GDPPCIP(R%), concomp2, context)
                    'Call XYTABL(TFNameContraception, GDPPCP(R%), concomp, context)
                    'Call XYTABL(TFNameContraception, GDPPCIP(R%), concomp2, context)
                End If
                'If RegName$(R%) = "Tanzania" AndAlso IY% > 13 Then Stop
                'ConMul = AMAX(1, concomp) / AMAX(1, concomp2)
                'contrbase = conusei(R%) * ConMul
                ConAdd = AMAX(1, concomp) - AMAX(1, concomp2)
                contrbase = conusei(R%) + ConAdd
                contrbase = Amin(100, contrbase)
                contrcomp = AMAX(1, contrbase + (IY% - 1) * tconr)
                If IY% > 1 Then CONTRUSE(R%) = Amin(100, AMAX(1, contrcomp * contrusm(R%)))

                '   All Years 2:  Contraceptive Excess/Deficit
                '       Compute factor to influence TFR; carry impact over time because
                '       when contraception use reaches maximum, impact of time shift in use
                '       must be protected
                '   --------------------------------------------------------------------
                '        If ContrComp - ContrBase > ContrExcessMin(R%) Then
                '            ContrExcessMin(R%) = ContrComp - ContrBase
                '        End If
                contrexcess = CONTRUSE(R%) - contrbase
                '        contrexcess = AMAX(contrexcess, ContrExcessMin(R%))
                'If RegName$(R%) = "Tanzania" AndAlso IY% > 13 Then Stop

                '   All Years 2:  Computed Fertility Rate
                '   -------------------------------------
                '   BBH 4/6/04 V5_0 Begin
                If context.HistoricMode = True Then
                    Call XYTABL("GDP/Capita (PPP) Versus Total Fertility Rate (1960)",
                GDPPCIP(R%), tfrcompi, context)
                    Call XYTABL("GDP/Capita (PPP) Versus Total Fertility Rate (1960)",
                GDPPCP(R%), tfrcomptar, context)
                    Call XYTABL("GDP/Capita (PPP) Versus Total Fertility Rate (2000)",
                GDPPCP(R%), tfrcomptar2, context)
                    tfrcomptar = CSng(tfrcomptar +
                IY% / 40 * (tfrcomptar2 - tfrcomptar))
                    'One-off global fertility reduction; no comparability in future

                    '            If IY% > 1 Then   'This multivariate function should be used when
                    '                   Education model better reproduces ed years historically.
                    '                Call AnalFunc("Total Fertilty Rate (1960)", _
                    '                    Array(GDPPCP(R%), EDYRSAG25I(R%), ConUseI(R%)), tfrcompi)
                    '                Call AnalFunc("Total Fertilty Rate (1960)", _
                    '                    Array(GDPPCP(R%), SEDYRSAG25(R%), CONTRUSE(R%)), tfrcomptar)
                    '            Else
                    '                tfrcompi = 1
                    '                tfrcomptar = 1
                    '            End If
                Else
                    'Call XYTABL("GDP/Capita (PPP) Versus Total Fertility Rate (1995)", _
                    'GDPPCIP(R%), tfrcompi)
                    'Call XYTABL("GDP/Capita (PPP) Versus Total Fertility Rate (1995)", _
                    'GDPPCP(R%), tfrcomptar)
                    If IY% > 1 Then
                        'jrs 2007/06/07 added second dimension to EDYRSAG25I
                        'bbh 2009/09/12
                        '                Call AnalFunc("Total Fertilty Rate (2000)", _
                        '                    Array(GDPPCIP(R%), AMAX(0.1, EDYRSAG15I(R%, 3)), conusei(R%)), tfrcompi)
                        '                Call AnalFunc("Total Fertilty Rate (2000)", _
                        '                    Array(GDPPCP(R%), AMAX(0.1, sedyrsag15(R%)), CONTRUSE(R%)), tfrcomptar)
                        'CDV 2022/04/26 ****START****
                        'mti 2022/10/24 Start
                        'Keep TFR Model as is, AoM impact on TFR will be only through scenarios that change AoM from the base value
                        'If UCase(sUseExperimentalTFR) = "FALSE" Then

                        Call AnalFunc("Total Fertilty Rate (2005)",
                            New Single() {infmori(R%), AMAX(0.1, EDYRSAG15I(R%, 3)), conusei(R%)}, tfrcompi,
                            New String() {"InfMor", "Educ15", "CONTRUSE"}, context) 'jrs 2014/10/09 added DepVar Names to check order
                        'Call AnalFunc("Total Fertilty Rate (2005)", _
                        'Array(sinfmor(R%), AMAX(0.1, sedyrsag15(R%)), CONTRUSE(R%)), tfrcomptar)
                        'bbh 2014/01/11
                        'Doc laginfmor(R%) = (1 - lagcoef) * INFMOR(R%) + lagcoef * laginfmor(R%) 'dummy for driver diagram
                        Call AnalFunc("Total Fertilty Rate (2005)",
                            New Single() {laginfmor(R%), AMAX(0.1, sedyrsag15(R%)), CONTRUSE(R%)}, tfrcomptar,
                            New String() {"InfMor", "Educ15", "CONTRUSE"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                        '                ElseIf UCase(sUseExperimentalTFR) = "ED" Then
                        '
                        '
                        '                    Call AnalFunc("Total Fertility Rate (2015) with ED", _
                        '                        Array(INFMORi(R%), AMAX(0.1, EDYRSAG15I(R%, 3)), conusei(R%), AGEOFMARRIAGE(R%, Female)), tfrcompi, _
                        '                        Array("InfMor", "Educ15", "CONTRUSE")) 'jrs 2014/10/09 added DepVar Names to check order
                        '
                        '                    Call AnalFunc("Total Fertility Rate (2015) with ED", _
                        '                        Array(laginfmor(R%), AMAX(0.1, sedyrsag15(R%)), CONTRUSE(R%), AGEOFMARRIAGE(R%, Female)), tfrcomptar, _
                        '                        Array("InfMor", "Educ15", "CONTRUSE", "AGEOFMARRIAGE")) 'jrs 2014/10/09 added DepVar Names to check order
                        '
                        '                Else 'If we have no value, this will be the default -> default in ifsInit is False
                        '
                        '
                        '                    Call AnalFunc("Total Fertility Rate (MostRecent) No ED AoM Log", _
                        '                       Array(INFMORi(R%), conusei(R%), iAGEOFMARRIAGEFemale(R%)), tfrcompi, _
                        '                        Array("InfMor", "CONTRUSE", "AGEOFMARRIAGE")) 'jrs 2014/10/09 added DepVar Names to check order
                        '
                        '                    Call AnalFunc("Total Fertility Rate (MostRecent) No ED AoM Log", _
                        '                        Array(laginfmor(R%), CONTRUSE(R%), AGEOFMARRIAGE(R%, Female)), tfrcomptar, _
                        '                        Array("InfMor", "CONTRUSE", "AGEOFMARRIAGE")) 'jrs 2014/10/09 added DepVar Names to check order
                        '                    'mti 2022/10/24 End
                        '                End If
                        '                'cdv 2022/04/26 + cdv 2022/05/18 ****END****
                        'bbh 2009/09/12 end
                        'mti 2022/10/24 End
                        tfrcompi = AMAX(1, tfrcompi)
                        tfrcomptar = AMAX(1, tfrcomptar)
                        'Else
                        '    tfrcompi = 1
                        '    tfrcomptar = 1
                    End If
                End If

                TimeFAc = Amin(IY% - 1, tfrconv)
                If IY% > 1 Then
                    TFR(R%) = TFRI(R%) * tfrcomptar / AMAX(1, tfrcompi)

                    If context.HistoricMode = False Then
                        TFR(R%) = TFR(R%) - contrexcess * eltfrcon 'residual impact of contraception
                        'on TFR, controlling for GDPPC effect, is linear
                    End If
                    'mti 2022/10/24 - AoM start
                    If context.HistoricMode = False Then
                        'ageofmarriagetfrelas is elasticity of TFR to AoM (women); the next term is the change of AoM from the base case forecast
                        TFR(R%) = TFR(R%) * (1 - ageofmarriagetfrelas(R%) * AGEOFMARRIAGE(R%, Female) * (1 - 1 / AMAX(0.00001, ageofmarriagem(R%, Female)))) 'residual impact of contraception
                    End If
                    'mti 2022/10/24 - AoM end

                    TFR(R%) = (TFR(R%) *
                (tfrconv - TimeFAc) + tfrcomptar * TimeFAc) / AMAX(1, tfrconv)

                    'If RegName$(R%) = "Zimbabwe" AndAlso IY% > 13 Then Stop
                    TFRMinReg = tfrmin(R%) '- POP(r%) / LD(r%, 1) * eltfrden
                    'This reduction in minimum fertility decimates population in Hong Kong
                    'Even a value for eltfrden of .025 would allow TFR to go to near zero
                    'Have removed eltfrden from COMMON
                    If TFRI(R%) >= TFRMinReg Then
                        tfrminr(R%) = TFRMinReg
                    Else
                        tfrminr(R%) = (TFRI(R%) * (tfrconv - TimeFAc) +
                    TFRMinReg * TimeFAc) / AMAX(1, tfrconv)
                    End If
                    TFR(R%) = TFR(R%) * (1 + zy * ttfrr) 'Technological/Cultural Shift
                    'If RegName$(R%) = "China" AndAlso IY% > 2 Then Stop
                    '   BBH 4/6/04 V5_0 End
                    'If IY% > 1 Then
                    'TFR(R%) = AMAX(tfrminr(R%), Amin(TFR(R%) * tfrm(R%), 10))
                    TFR(R%) = AMAX(tfrminr(R%), TFR(R%))
                    mininit = ConvergeOverTime(TFRI(R%), tfrmin(R%), 100) 'In case of low TFRI, converge up
                    minbound = Amin(mininit, tfrmin(R%))
                    'TFR(R%) = AMAX(minbound, Amin(TFR(R%), 10))
                    TFR(R%) = AMAX(minbound, Amin(TFR(R%) * tfrm(R%), 10))
                    'Else
                    '   TFR(R%) = AMAX(TFRMinR(R%), Amin(TFR(R%), 10))
                    'bbh 2013/11/19 begin

                    If TFRGR(R%).HasValue Then
                        TFRgrComp = (TFR(R%) - stfr(R%)) / AMAX(0.01, stfr(R%))
                        TFRgrSmoothed = ConvergeOverTime(CSng(TFRGR(R%)), TFRgrComp, 10)
                        TFR(R%) = stfr(R%) * (1 + TFRgrSmoothed)
                        'dsr 2016/05/04 Do not write stfr here as it can change later
                        'stfr(R%) = TFR(R%)
                    End If
                    '           TFR(R%) = AMAX(minbound, Amin(TFR(R%) * tfrm(R%), 10))
                    'If RegName$(R%) = "Sudan South" Then Stop
                End If
                '   BBH 7/31/04 V5_0 Begin

                '
                '   All Years 2: Adjust fertility distribution towards ultimate fertility distribution
                '   ----------------------------------------------------------------------------------
                If GDPPCP(R%) > GDPPCIP(R%) Then
                    ultmult = Amin(1, (GDPPCP(R%) - GDPPCIP(R%)) / (AMAX(45, GDPPCIP(R%) * 1.5) - GDPPCIP(R%)))
                Else
                    ultmult = 0
                End If
                For j% = 0 To NCohorts
                    ferdst(j%, R%) = ultimatefertility(j%) * ultmult +
                basferdst(j%, R%) * fb(R%) * (1 - ultmult)
                Next j%

                'jrs 2012/11/29 *** begin ***
                '   All Years 2: Converge to Target Fertility Distribution
                '   -------------------------------------------------------
                If hltfragepeak(R%) > hltfragestop(R%) Then
                    hltfragepeak(R%) = hltfragestop(R%)
                End If
                If hltfrageinit(R%) > hltfragepeak(R%) Then
                    hltfrageinit(R%) = hltfragepeak(R%)
                End If

                If IY > 1 AndAlso hltfrpeaklevel(R%) > 0 Then
                    If hltfrconv + 1 > IY Then
                        'Initialize parameters, age parameters store the age at the begining of the category
                        'i.e. for the 30-34 category stores 30, j=7

                        'jrs 2013/08/26 changing all integers to single
                        'jrs 2013/08/26 Dim agehalflife As Integer, ageinit As Integer, agepeak As Integer, agestop As Integer
                        Dim agehalflife As Single, ageinit As Single, agepeak As Single, agestop As Single
                        Dim peaklevel As Double
                        'jrs 2013/05/29 changed agehlaflifeT to Long, to use 2M for constant fertility
                        'Target parameters
                        'jrs 2013/08/26 Dim agehalflifeT As Long, ageinitT As Integer, agepeakT As Integer, agestopT As Integer
                        peaklevel = 0
                        For j% = 0 To NCohorts
                            If ferdst(j%, R%) > 0 AndAlso ageinit = 0 Then
                                ageinit = (j% - 1) * 5
                            End If
                            If ferdst(j%, R%) = 0 AndAlso ageinit > 0 AndAlso agestop = 0 Then
                                agestop = (j% - 1) * 5
                            End If
                            If ferdst(j%, R%) * 1000 > peaklevel Then
                                peaklevel = ferdst(j%, R%) * 1000
                                agepeak = (j% - 1) * 5
                            End If
                            If ferdst(j%, R%) * 1000 <= peaklevel * 0.5 AndAlso peaklevel > 0 AndAlso agehalflife = 0 Then
                                agehalflife = (j% - 1) * 5
                            End If
                        Next j%
                        'Converge parameters
                        ageinitT = ConvergeOverTime(ageinit, hltfrageinit(R%), hltfrconv + 1)
                        agestopT = ConvergeOverTime(agestop, hltfragestop(R%), hltfrconv + 1)
                        agepeakT = ConvergeOverTime(agepeak, hltfragepeak(R%), hltfrconv + 1)
                        peaklevelT = ConvergeOverTime(CSng(peaklevel), hltfrpeaklevel(R%), hltfrconv + 1)
                        'jrs 2012/12/17 rounding issues
                        If hltfragehalflife(R%) < 0.005 Then
                            agehalflifeT = agepeakT
                        Else
                            agehalflifeT = ConvergeOverTime(agehalflife, hltfragepeak(R%) + hltfragehalflife(R%), hltfrconv + 1) 'jrs 2013/01/24 I was using peak level instead of peak age
                        End If
                    Else
                        'Convergence is over
                        ageinitT = hltfrageinit(R%)
                        agestopT = hltfragestop(R%)
                        agepeakT = hltfragepeak(R%)
                        peaklevelT = hltfrpeaklevel(R%)
                        agehalflifeT = hltfragepeak(R%) + hltfragehalflife(R%)
                    End If
                    'Checking that ageinit is mod 5
                    'jrs 2013/08/26
                    ageinitT = CInt(ageinitT)
                    If ageinitT Mod 5 > 0 Then
                        ageinitT = ageinitT - (ageinitT Mod 5)
                    End If

                    'Find compound growth rate
                    Dim grt As Single
                    If agehalflifeT - agepeakT > 0.005 Then 'jrs 2012/12/17 rounding issues
                        grt = CSng(Exp(Log(0.5) / (agehalflifeT - agepeakT)) - 1)
                    Else 'no drop
                        grt = 0
                    End If
                    'Rebuild Fertility Distribution with Target Parameters
                    For j% = 0 To NCohorts
                        '1. 0 before ageinit, Initialization age
                        If (j% - 1) * 5 < ageinitT Then
                            ferdst(j%, R%) = 0
                            '2. linear growth to peak
                        ElseIf (j% - 1) * 5 <= agepeakT Then
                            If agepeakT = ageinitT Then
                                ferdst(j%, R%) = peaklevelT / 1000
                            Else
                                ferdst(j%, R%) = peaklevelT / 1000 * ((j% - 1) * 5 - ageinitT + 1) / (agepeakT - ageinitT + 1)
                            End If
                            '3. exponential drop from peak to menopause
                        ElseIf (j% - 1) * 5 < agestopT Then
                            ferdst(j%, R%) = peaklevelT / 1000 * (1 + grt) ^ ((j% - 1) * 5 - agepeakT)
                            '4. 0 from Menopause forward
                        Else
                            ferdst(j%, R%) = 0
                        End If
                    Next j%
                End If
                'jrs 2012/11/29 *** end ***
                tfrdst(R%) = 0
                For j% = 0 To NCohorts
                    'jrs 2012/08/31
                    'jrs 2019/07/26 Not IsNull(ferdst(j%, R%)) AndAlso 
                    If agedst(j, R, 2) > 0 Then 'jrs 2012/12/17 added and agedst(j,r,2)>0
                        'jrs 2013/01/04 *** begin ***
                        If (j% * 5) < agestopT OrElse agestopT = 0 Then
                            tfrdst(R%) = tfrdst(R%) + ferdst(j%, R%) * 5
                        ElseIf ferdst(j%, R%) > 0 AndAlso (agestopT - (j% - 1) * 5) > 0 Then
                            tfrdst(R%) = tfrdst(R%) + ferdst(j%, R%) * (agestopT - (j% - 1) * 5)
                        End If
                        'jrs 2013/01/04 *** end ***
                    End If
                    '*5 because of 5 year cohorts
                Next j%

                'jrs 2012/11/29 *** begin ***
                'using tfrm as switch to stage 2
                If hltfrpeaklevel(R%) > 0 AndAlso IY > 1 Then 'And tfrm(R%) = 2
                    'Adjust TFR
                    'jrs 2013/01/04 *** begin ***
                    If hltfrconv + 1 > IY Then
                        TFR(R%) = ConvergeOverTime(TFR(R%), CSng(tfrdst(R%)), hltfrconv + 1)
                    Else
                        TFR(R%) = CSng(tfrdst(R%))
                    End If
                    'jrs 2013/01/04 *** end ***
                    TFR(R%) = AMAX(minbound, TFR(R%))
                End If
                'jrs 2012/11/29 *** end ***

                'jrs 2019/05/03 *** BEGIN ***
                If tfrexovals(R%) > -100 Then
                    TFR(R%) = tfrexovals(R%)
                End If
                'jrs 2019/05/03 *** END ***

                'jrs 2014/08/26 Exogenous Model
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("TFR", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If
                'dsr 2016/05/04 Moved from earlier location, where is was calculated before final value of
                '               TFR determined
                stfr(R%) = TFR(R%)

                '
                '   All Years 2: Impose Total Fertility Rate on Fertility Distribution,
                '        Compute Birth Rate
                '   -------------------------------------------------------------------
                BIRTHS(R%) = 0!
                For j% = 1 To NCohorts
                    '            FertDstM = FerDst(j%, r%) * FB(r%) * TFR(r%) / TFRI(r%)
                    fertdstm = ferdst(j%, R%) / AMAX(0.0001, tfrdst(R%)) * TFR(R%)
                    'jrs 2012/08/31
                    'jrs 2019/07/26 If Not IsNull(fertdstm) Then
                    BIRTHS(R%) = CSng(BIRTHS(R%) + agedst(j%, R%, 2) * fertdstm)
                    'End If
                    modFERDST(j%, R%) = fertdstm * 1000
                Next j%

                'dsr 2016/03/24 Second Adjustment to Exogenous Model for births **** start ****
                'noticed that TFR adjustment was not enough, probably due to small differences in age distribution
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("BIRTHS", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If
                'dsr 2016/03/24 Second Adjustment to Exogenous Model for births **** end ****
                agedst(0, R%, 1) = BIRTHS(R%) * birthmshr
                'Hold infants here temporarily; will put in Cohort 1
                agedst(0, R%, 2) = BIRTHS(R%) * birthfshr
                'If RegName$(R%) = "Unitd Kingdm" Then Stop

                '    If RegName$(R%) = "Burundi" Then Stop


                '   All Years 2:  Compute "Basic" Life Expectancy, without Starvation, War, Plague
                '   ------------------------------------------------------------------------------
                If context.HistoricMode = True Then
                    Call XYTABL("GDP/Capita (PPP) Versus Life Expectancy Male (1962)",
                Amin(30, GDPPCIP(R%)), LifeCompI(1), context)
                    Call XYTABL("GDP/Capita (PPP) Versus Life Expectancy Male (1962)",
                Amin(30, GDPPCP(R%)), LifExpTarBas(1), context)
                    Call XYTABL("GDP/Capita (PPP) Versus Life Expectancy Male (2006)",
                Amin(30, GDPPCP(R%)), LifExpTarBas2(1), context)
                    LifExpTarBas(1) = CSng(LifExpTarBas(1) +
                IY% / 35 * (LifExpTarBas2(1) - LifExpTarBas(1)))
                    Call XYTABL("GDP/Capita (PPP) Versus Life Expectancy Female (1962)",
                Amin(30, GDPPCIP(R%)), LifeCompI(2), context)
                    Call XYTABL("GDP/Capita (PPP) Versus Life Expectancy Female (1962)",
                Amin(30, GDPPCP(R%)), LifExpTarBas(2), context)
                    Call XYTABL("GDP/Capita (PPP) Versus Life Expectancy Female (2006)",
                Amin(30, GDPPCP(R%)), LifExpTarBas2(2), context)
                    LifExpTarBas(2) = CSng(LifExpTarBas(2) +
                IY% / 35 * (LifExpTarBas2(2) - LifExpTarBas(2)))
                    'LDCs had a one-time tech transfer catch-up with MDCs (completed by 1997)
                Else
                    Call XYTABL("GDP/Capita (PPP) Versus Life Expectancy Male (2006)",
                Amin(30, GDPPCIP(R%)), LifeCompI(1), context)
                    Call XYTABL("GDP/Capita (PPP) Versus Life Expectancy Male (2006)",
                Amin(30, GDPPCP(R%)), LifExpTarBas(1), context)
                    Call XYTABL("GDP/Capita (PPP) Versus Life Expectancy Female (2006)",
                Amin(30, GDPPCIP(R%)), LifeCompI(2), context)
                    Call XYTABL("GDP/Capita (PPP) Versus Life Expectancy Female (2006)",
                Amin(30, GDPPCP(R%)), LifExpTarBas(2), context)
                    'Stop movement out function after $30,000; depend on TMORTR
                End If
                Call XYTABL("GDP/Capita (PPP) Versus Life Expectancy Male (2006)",
            40, LifExpTarMax(1), context)  'Maximum of target function
                Call XYTABL("GDP/Capita (PPP) Versus Life Expectancy Female (2006)",
            40, LifExpTarMax(2), context)


                '   All Years 2:  Smooth "Basic" Life Expectancy if there is downward pressure (lower GDPPCP)
                '         and add slow technology enhancement of life expectancy
                '   ----------------------------------------------------------------------------------------
                For gend% = 1 To 2
                    If IY% = 1 Then SmLifExpTarBase(R%, gend%) = LifExpTarBas(gend%)
                    If LifExpTarBas(gend%) < SmLifExpTarBase(R%, gend%) Then 'Inertia of LIfExp downward
                        SmLifExpTarBase(R%, gend%) = CSng(SmLifExpTarBase(R%, gend%) * 0.8 + LifExpTarBas(gend%) * 0.2)
                    Else
                        SmLifExpTarBase(R%, gend%) = LifExpTarBas(gend%)
                    End If
                    LifExpTar(gend%) = SmLifExpTarBase(R%, gend%) * (1 - zy * tmortr / 2)
                    LifExpTarMax(gend%) = LifExpTarMax(gend%) * (1 - zy * tmortr / 2)
                    If GDPPCPI(R%) < 5 Then  'catch up possible (used in historic run)
                        LifExpTar(gend%) = LifExpTar(gend%) * (1 - zy * tmortr2 / 2) 'tmortr2=0
                        LifExpTarMax(gend%) = LifExpTarMax(gend%) * (1 - zy * tmortr2 / 2)
                    End If
                Next gend%
                'If RegName$(R%) = "China" Then Stop

                '   All Years 2:  Compute impact of health spending as percent of GDP
                '   ----------------------------------------------------------------------------------------
                If context.HistoricMode = True Then
                    Call XYTABL("GDP/Capita (PPP) Versus Govt Exp Hlth as % of GDP (1990) - log",
                Amin(40, GDPPCP(R%)), PredHlthSpend, context)
                Else
                    'jrs 2024/06/10 
                    'Call XYTABL("GDP/Capita (PPP) Versus Govt Exp Hlth as % of GDP (2000) - log",
                    'Amin(40, GDPPCP(R%)), PredHlthSpend, context)
                    Call XYTABL(TblNameHealthExp, Amin(40, GDPPCP(R%)), PredHlthSpend, context)
                End If

                For gend% = 1 To 2
                    If IY% = 1 Then
                        HlthSpendRGDPI(R%) = (GDS(R%, Health) / GDP(R%) * 100) - PredHlthSpend
                        'jrs 2007/06/19
                        lifexpmaxi(gend%) = AMAX(lifexpmaxi(gend%), LifExpTar(gend%))
                    Else
                        hlthspendrgdp = (GDS(R%, Health) / GDP(R%) * 100) - PredHlthSpend
                        hlthshiftmax = 5
                        elaslifspnd = 4
                        hlthcoef = 4 - 4 * (Amin(1, LifExpTar(gend%) / 85))
                        'Cross-sectional estimation shows reln of health spending with Life Expect,
                        '    but T-score not significant (.5).  Coefficient is 6 years per percent;
                        '    have set at about half for most countries the value to be conservative
                        '    Phase out response to investment as approach 85 year life expectancy
                        lifexpspendshift =
                    AMAX(-hlthshiftmax, Amin(hlthshiftmax,
                    (hlthspendrgdp - HlthSpendRGDPI(R%)) * hlthcoef))
                        LifExpTar(gend%) = Amin(LifExpMax(gend%), LifExpTar(gend%))
                    End If
                    LifExpTar(gend%) = AMAX(25, LifExpTar(gend%))
                    LifeCompI(gend%) = AMAX(25, LifeCompI(gend%))
                    'Bound because function drops off rapidly on bottom end of GDPPCP

                    '   Put target from function, technology, and health spending elements together
                    '      When countries begin above function, GDPPCP can take too high, so reduce function impact
                    '   -------------------------------------------------------------------------------------------

                    If IY% = 1 Then
                        lifexptari(R%, gend%) = LifExpTar(gend%)
                        SLIFEXP(R%, gend%) = LifExpI(R%, gend%)
                    Else
                        IncTerm1 = (LifExpTar(gend%) / AMAX(1, lifexptari(R%, gend%)) - 1) * LifExpI(R%, gend%)
                        IncTerm2 = LifExpI(R%, gend%) * (-zy * tmortr)
                        If LifExpI(R%, gend%) > lifexptari(R%, gend%) Then 'Reduce growth impact of target
                            '                    BalTerm = Amin(1, (LifExpI(R%, gend%) - lifexptari(R%, gend%)) / 10)
                            '                    LIFEXP(R%, gend%) = LifExpI(R%, gend%) + (1 - BalTerm) * IncTerm1 + BalTerm * IncTerm2
                            LIFEXP(R%, gend%) = LifExpI(R%, gend%) * LifExpTar(gend%) / AMAX(1, lifexptari(R%, gend%)) +
                        LifExpI(R%, gend%) * (-zy * tmortr)
                        Else   'Allow full growth impact of target
                            BalTerm = 1 + ((lifexptari(R%, gend%) - LifExpI(R%, gend%)) / LifExpI(R%, gend%)) / 2
                            LIFEXP(R%, gend%) = LifExpI(R%, gend%) + (IncTerm1 + IncTerm2) * BalTerm
                        End If
                        LIFEXP(R%, gend%) = LIFEXP(R%, gend%) + lifexpspendshift
                        Headroom = LifExpTarMax(gend%) - SLIFEXP(R%, gend%)
                        MaxIncrease = Headroom / 50
                        LIFEXP(R%, gend%) = Amin(LIFEXP(R%, gend%), SLIFEXP(R%, gend%) + MaxIncrease)
                        SLIFEXP(R%, gend%) = LIFEXP(R%, gend%) 'Leave here, not later; want to control process below


                        '                LIFEXP(R%, gend%) = LifExpI(R%, gend%) * LifExpTar(gend%) / AMAX(1, lifexptari(R%, gend%)) + _
                        '                    lifexpspendshift
                        '                LifTechAdv(gend%) = LifExpI(R%, gend%) * (1 - zy * tmortr / 2)
                        '                If GDPPCPi(R%) < 5 Then  'catch up possible (used in historic run)
                        '                    LifTechAdv(gend%) = LifTechAdv(gend%) * (1 - zy * tmortr2 / 2) 'tmortr2=0
                        '                End If
                        '                LifNormMax(gend%) = AMAX(LifExpTar(gend%), LIFEXP(R%, gend%)) + hlthshiftmax
                        '                LifMax(gend%) = AMAX(LifTechAdv(gend%), LifNormMax(gend%))
                        '                LIFEXP(R%, gend%) = Amin(LIFEXP(R%, gend%), LifMax(gend%))
                        '  If RegName$(R%) = "Bangladesh" Then Stop

                        '  If RegName$(R%) = "China" AndAlso gend% = 2 AndAlso IY% > 30 Then Stop


                        '   Converge result towards target function plus health spending shift very slowly
                        '   ------------------------------------------------------------------------------

                        TimeFAc = Amin(IY%, mortconv)
                        ultlifetar = LifExpTar(gend%) + lifexpspendshift
                        LIFEXP(R%, gend%) = (LIFEXP(R%, gend%) * (mortconv - TimeFAc) +
                    ultlifetar * TimeFAc) / AMAX(1, mortconv)
                        '    If RegName$(R%) = "China" Then Stop
                    End If

                    '   All Years 2:  Adjust Mortality Distribution Towards Ultimate Life Tables
                    '       Using Life Expectancy
                    '   ------------------------------------------------------------------------
                    If LIFEXP(R%, gend%) > LifExpI(R%, gend%) Then
                        'jrs 2015/08/20 ultmult = Amin(1, (LIFEXP(R%, gend%) - LifExpI(R%, gend%)) / (85 - LifExpI(R%, gend%)))
                        'Note:  the 85 here represents life expectancy at ultimate mortality
                        ultmult = CSng(Amin(1, (LIFEXP(R%, gend%) - LifExpI(R%, gend%)) / AMAX((87 - LifExpI(R%, gend%)), 0.01)))
                        'Ultimate mortality for females seems to represent a LIfe Expectancy of 86.8
                        'Problem arrises when initial life expectancy is over 85, like for Hong Kong, females with 2015-2020 cohort data
                    Else
                        ultmult = 0
                    End If
                    For j% = 0 To NCohorts
                        mordst(j%, R%, gend%) = CSng(ultimatemort(j%, gend%) * ultmult +
                    BasMordst(j%, R%, gend%) * (1 - ultmult))
                    Next j%
                Next gend%
                'If RegName$(R%) = "China" Then Stop


                '   All Years 2:  Compute a multiplier (mfactor) to adjust mortality by cohort to life expectancy
                '   ---------------------------------------------------------------------------------------------
                'jrs 2009/05/22
                'Explained by Randall Kuhn
                'Life Expectancy was computed as the median age at death,
                'and it should be computed as a mean
                'average number of years of life given the survival rates in each age group

                'mfactor for Males
                '-----------------
                'jrs 2009/05/22
                '        mortinfants = mordst(0, R%, 1)
                '        cumMort = mortinfants '* fdeath(R%) '* mortmul 'Infant Mortality
                '        CumLif = 0
                '        For j% = 1 To NCohorts
                '            MortCohort = mordst(j%, R%, 1)
                '            cohMort = MortCohort '* fdeath(R%)
                '
                '            If CumLif + 5 < LIFEXP(R%, 1) Then
                '                cumMort = cumMort + cohMort * 5 'Five year cohorts
                '                CumLif = CumLif + 5
                '            Else
                '                Dif = LIFEXP(R%, 1) - CumLif
                '                cumMort = cumMort + cohMort * Dif
                '                CumLif = CumLif + Dif
                '                'jrs 2007/09/04
                '                mfactorMCohort = j
                '                Exit For
                '            End If
                '        Next j%
                '        mFactorM = 0.5 / cumMort  '.5 represents 50% chance of dying younger,
                'jrs 2019/05/06 maxfactor from 7 to 14 for Maldives historical
                mFactorM = matchMortWLifeExp(mordst, R, 1, LIFEXP(R, 1), 0, 14, bCancelMsgMatchMortLE)  'jrs 2016/11/01 change the max factor from 6 to 7

                ' and 50% change of dying older; hence life expectancy at birth

                'mfactor for Females
                '-------------------
                'jrs 2009/05/22
                '        mortinfants = mordst(0, R%, 2)
                '        cumMort = mortinfants '* fdeath(R%) '* mortmul 'Infant Mortality
                '        CumLif = 0
                '        For j% = 1 To NCohorts
                '            MortCohort = mordst(j%, R%, 2)
                '            cohMort = MortCohort '* fdeath(R%)
                '
                '            If CumLif + 5 < LIFEXP(R%, 2) Then
                '                cumMort = cumMort + cohMort * 5 'Five year cohorts
                '                CumLif = CumLif + 5
                '            Else
                '                Dif = LIFEXP(R%, 2) - CumLif
                '                cumMort = cumMort + cohMort * Dif
                '                CumLif = CumLif + Dif
                '                'jrs 2007/09/04
                '                mfactorFCohort = j
                '                Exit For
                '            End If
                '        Next j%
                '        mFactorF = 0.5 / cumMort  '.5 represents 50% chance of dying younger,
                ' and 50% change of dying older; hence life expectancy at birth

                'jrs 2019/05/06 maxfactor from 7 to 16 for Maldives historical
                mFactorF = matchMortWLifeExp(mordst, R, 2, LIFEXP(R, 2), 0, 16, bCancelMsgMatchMortLE)  'jrs 2016/11/01 change the max factor from 6 to 7

                'If IY = 1 Then
                '    Open FdrvRun$ & "ifslog.txt" For Append As #500
                '    Print #500, mFactorM & ", " & mFactorF
                '    Close #500
                'End If

                '
                '   All Years 2:  Compute Calorie-based Starvation Factor
                '   -----------------------------------------------------
                clav = 1.0E+8!
                If (agon > 0.5) Then
                    clav = AMAX(0.1, CLAVAL(R%))
                End If
                clsexp = AMAX(0, Amin(clsexp, 100))
                CLNEED(R%) = AMAX(0.001, CLNEED(R%))
                '   BBH 1/25/04 V5_0
                clsf = CSng(AMAX(1 - (clav / CLNEED(R%) * CalIAdj(R%)), 0) ^ clsexp)
                clsf = CSng(Amin(0.5, clsf))

                '
                '   All Years 2:  Compute Mortality:
                '        Adding Affects of Starvation, War Deaths, Plague (AIDS)
                '        Subtracting Affects of Technological Advance, Heathcare Spending
                '   ----------------------------------------------------------------------
                DEATHS(R%) = 0!
                SDEATH(R%) = 0
                Call XYTABL("GDP/Capita (PPP) Versus HIV Infections, Male Portion",
                GDPPCP(R%), MaleAIDSPortion, context)
                maleaidsdths = incaidsdths(R%) * MaleAIDSPortion
                femaleaidsdths = incaidsdths(R%) * (1 - MaleAIDSPortion)

                For j% = 0 To NCohorts
                    For gend% = 1 To 2
                        'jrs 2007/05/25
                        If gend% = 1 Then
                            'jrs 2009/05/22
                            'jrs 2007/09/04 facing out mfactor for higher cohorts
                            '                    If j <= mfactorMCohort Then
                            '                        mfactor = mFactorM
                            '                    Else
                            '                        mfactor = mFactorM + (1 - mFactorM) / (NCohorts - mfactorMCohort) * (j - mfactorMCohort)
                            '                    End If
                            mfactor = mFactorM
                        Else
                            'jrs 2009/05/22
                            'jrs 2007/09/04 facing out mfactor for higher cohorts
                            '                    If j <= mfactorFCohort Then
                            '                        mfactor = mFactorF
                            '                    Else
                            '                        mfactor = mFactorF + (1 - mFactorF) / (NCohorts - mfactorFCohort) * (j - mfactorFCohort)
                            '                    End If
                            mfactor = mFactorF
                        End If

                        '   All Years 2: First deal with Non-cohort specific factors - apply to all
                        '   ----------------------------------------------------------
                        mortdstmg(gend%) = mordst(j%, R%, gend%) '* fdeath(R%)
                        If IY% > 1 Then
                            mortdstmg(gend%) = mortdstmg(gend%) * mortm(R%) * MORTMG(R%)
                            'MORTMG is mortality multiplier from govt spending on health
                            'computed in economic module
                        End If

                        mortdstmg(gend%) = mortdstmg(gend%) + CIVDM(R%) / 2 'War Deaths
                        mortdstmg(gend%) = mortdstmg(gend%) * AMAX(0.1, (1 + zy * tmortr))
                        If GDPPCPI(R%) < 5 Then  'catch up possible (used in historic run)
                            mortdstmg(gend%) = mortdstmg(gend%) * AMAX(0.1, (1 + zy * tmortr2))
                        End If

                        'tmortr represents annual rate of reduced mortality with time/tech
                        'jrs 2010/07/27 added amin(x, 1000)
                        'modmordst(j%, R%, gend%) = mortdstmg(gend%) * 1000 * mfactor 'jrs 2007/05/23 added * mfactor to include this normalization in Health Module 'For Pop File Output
                        modmordst(j%, R%, gend%) = Amin(mortdstmg(gend%) * 1000 * mfactor, 1000)

                        '   All Years 2: Next deal with Cohort specific factors
                        '   --------------------------------------

                        'mti LifeExp/ gender May 2006 - death by gender in detail
                        'jrs 2007/08/14 using heavy aids vector
                        If gend% = 1 Then
                            aidsdthscoh = maleaidsdths * aidsdeathsbyageHeavy(j%, R, gend%)
                        Else
                            aidsdthscoh = femaleaidsdths * aidsdeathsbyageHeavy(j%, R, gend%)
                        End If

                        '                If CumSDeath(R%) / POP(R%) > 0.03 Then
                        '                    SDeathMul = AMAX(0, (0.1 - CumSDeath(R%) / POP(R%)) / 0.07)
                        '                Else
                        '                    SDeathMul = 1
                        '                End If

                        If j% = 0 Then 'infants
                            'jrs 2009/06/16 adding mfactor
                            infmorg(gend%) = mortdstmg(gend%) * mfactor '* mortmul + CLSF * CLMAGE(1)
                            'If RegName$(R%) = "Romania" AndAlso IY% >= 9 Then Stop

                            '                   If Gend% = 1 Then
                            '                        clsf = (Amin(clsf, (0.2 - mortdstmg(Gend%)) / (clmage(0) * mfactor)))
                            '                   End If
                            ChilSdeath = CSng(Amin(0.1 * agedst(j%, R%, gend%),
                        agedst(j%, R%, gend%) * (clsf * clmage(0))))
                            '                   infmorg(Gend%) = Amin(0.2, mortdstmg(Gend%) + clsf * clmage(0))
                            'Bounds infant mortality below 350 per thousand
                            'If RegName$(R%) = "Eritrea" AndAlso IY% >= 15 Then Stop
                            '   BBH 6/23/04 V5_0
                            'jrs 2007/12/17 adding aMax, to put a lower limit on deaths since they were going negative
                            'the value 0.0000336 is the minimum found for Japan in deaths per 1000 and divided by 100
                            'jrs 2009/12/10 d(j%, R%, gend%) = AMAX(0.000000336 * agedst(j%, R%, gend%), Amin(0.25 * agedst(j%, R%, gend%), _
                            'agedst(j%, R%, gend%) * mortdstmg(gend%) * mfactor + aidsdthscoh + ChilSdeath)) 'jrs 2007/05/21 added R% to d
                            'increasing limit from 25% to 40% of population
                            d(j%, R%, gend%) = CSng(AMAX(0.000000336 * agedst(j%, R%, gend%), Amin(0.4 * agedst(j%, R%, gend%),
                                                                                              agedst(j%, R%, gend%) * mortdstmg(gend%) * mfactor + aidsdthscoh + ChilSdeath))) 'jrs 2007/05/21 added R% to d
                            'jrs 2007/06/14 Computing Shift
                            '                    If IY = 1 Then
                            '                        d2(j%, gend%) = Amin(0.25 * agedst(j%, R%, gend%), _
                            '                             agedst(j%, R%, gend%) * mortdstmg(gend%) + aidsdthscoh + ChilSdeath)  'jrs 2007/05/21 added R% to d
                            '                    End If

                            'jrs 2009/12/10 deathsNS(j%, gend%) = AMAX(0.000000336 * agedst(j%, R%, gend%), Amin(0.25 * agedst(j%, R%, gend%), _
                            'agedst(j%, R%, gend%) * mortdstmg(gend%) * mfactor + aidsdthscoh))
                            deathsNS(j%, gend%) = CSng(AMAX(0.000000336 * agedst(j%, R%, gend%), Amin(0.4 * agedst(j%, R%, gend%),
                        agedst(j%, R%, gend%) * mortdstmg(gend%) * mfactor + aidsdthscoh)))

                            SDEATH(R%) = SDEATH(R%) + ChilSdeath
                        ElseIf j% = 1 Then 'As with child malnutrition, SDEATH applies to cohorts 0,1
                            ChilSdeath = CSng(Amin(0.01 * agedst(j%, R%, gend%),
                        agedst(j%, R%, gend%) * (clsf * clmage(1))))

                            SDEATH(R%) = SDEATH(R%) + ChilSdeath
                            'jrs 2007/12/17 adding aMax, to put a lower limit on deaths since they were going negative
                            'the value 0.0000336 is the minimum found for Japan in deaths per 1000 and divided by 100
                            'jrs 2009/12/10 d(j%, R%, gend%) = AMAX(0.000000336 * agedst(j%, R%, gend%), Amin(0.1 * agedst(j%, R%, gend%), _
                            'agedst(j%, R%, gend%) * mortdstmg(gend%) * mfactor + _
                            'ChilSdeath + aidsdthscoh)) 'jrs 2007/05/21 added R% to d
                            d(j%, R%, gend%) = CSng(AMAX(0.000000336 * agedst(j%, R%, gend%), Amin(0.2 * agedst(j%, R%, gend%),
                        agedst(j%, R%, gend%) * mortdstmg(gend%) * mfactor +
                        ChilSdeath + aidsdthscoh))) 'jrs 2007/05/21 added R% to d

                            'jrs 2007/06/14 Computing Shift
                            '                    If IY = 1 Then
                            '                        d2(j%, gend%) = Amin(0.1 * agedst(j%, R%, gend%), _
                            '                            agedst(j%, R%, gend%) * mortdstmg(gend%) + _
                            '                            ChilSdeath + aidsdthscoh) 'jrs 2007/05/21 added R% to d
                            '                    End If

                            'jrs 2009/12/10 deathsNS(j%, gend%) = AMAX(0.000000336 * agedst(j%, R%, gend%), Amin(0.1 * agedst(j%, R%, gend%), _
                            'agedst(j%, R%, gend%) * mortdstmg(gend%) * mfactor + _
                            'aidsdthscoh))
                            deathsNS(j%, gend%) = CSng(AMAX(0.000000336 * agedst(j%, R%, gend%), Amin(0.2 * agedst(j%, R%, gend%),
                        agedst(j%, R%, gend%) * mortdstmg(gend%) * mfactor +
                        aidsdthscoh)))

                        ElseIf j% = NCohorts Then 'jrs 2014/04/08 allowing all people in last age to die
                            d(j%, R%, gend%) = AMAX(0.000000336 * agedst(j%, R%, gend%), Amin(agedst(j%, R%, gend%),
                        agedst(j%, R%, gend%) * mortdstmg(gend%) * mfactor +
                        aidsdthscoh)) 'jrs 2007/05/24 changed from .5 to .75 limit 'jrs 2007/05/21 added R% to d
                        Else
                            'jrs 2007/12/17 adding aMax, to put a lower limit on deaths since they were going negative
                            'the value 0.0000336 is the minimum found for Japan in deaths per 1000 and divided by 100
                            d(j%, R%, gend%) = CSng(AMAX(0.000000336 * agedst(j%, R%, gend%), Amin(0.75 * agedst(j%, R%, gend%),
                        agedst(j%, R%, gend%) * mortdstmg(gend%) * mfactor +
                        aidsdthscoh))) 'jrs 2007/05/24 changed from .5 to .75 limit 'jrs 2007/05/21 added R% to d
                            'jrs 2007/06/14 Computing Shift
                            '                    If IY = 1 Then
                            '                        d2(j%, gend%) = Amin(0.75 * agedst(j%, R%, gend%), _
                            '                            agedst(j%, R%, gend%) * mortdstmg(gend%) + _
                            '                            aidsdthscoh) 'jrs 2007/05/24 changed from .5 to .75 limit 'jrs 2007/05/21 added R% to d
                            '                    End If
                        End If
                        'If IY = 49 AndAlso R = 142 AndAlso gend = 1 Then
                        '    Print #500, d(j, R, gend) & ", " & agedst(j, R, gend) & ", " & aidsdthscoh & ", " & ChilSdeath
                        'End If
                    Next gend%
                    'Here is portion of cohort; will be multiplied by 1000 later
                    DEATHS(R%) = CSng(DEATHS(R%) + d(j%, R%, 1) + d(j%, R%, 2)) 'Later will be * 1000
                Next j%

                'jrs 2017/12/05
                SDEATHS(R%) = DEATHS(R%)

                'If IY = 49 AndAlso R = 142 Then
                '    Close #500
                'End If
                '
                '   All Years 2:  Infant Mortality,CDR, Life Expectancy (Legacy; see Health model for newer)

                If context.HistoricMode = True Then
                    calcinf = birthmshr * infmorg(1) + birthfshr * infmorg(2)
                    If IY% = 1 Then
                        infmoradj(R%) = INFMOR(R%) / AMAX(0.001, calcinf)
                    Else
                        INFMOR(R%) = calcinf * infmoradj(R%)
                        'If RegName$(R%) = "Romania" AndAlso IY% >= 8 Then Stop
                    End If
                Else
                    INFMOR(R%) = birthmshr * infmorg(1) + birthfshr * infmorg(2)
                    INFMOR(R%) = INFMOR(R%) * 1000
                    'If RegName$(R%) = "India N East" Then Stop

                End If
                'bbh 2009/09/12
                If IY% = 1 Then infmori(R%) = INFMOR(R%)
                'bbh 2009/12/07
                '       If IY% = 1 Then

                'bbh 2014/01/11 start
                'sinfmor(R%) = INFMOR(R%)
                If IY% = 1 Then
                    sinfmor(R%) = INFMOR(R%) 'BBH 2014/01/11 NOTE: This was being done every year,
                    'but if health model is on we should pick up a value from there, lagged
                    laginfmor(R%) = INFMOR(R%)
                End If
                If hlmodelsw < 1 AndAlso IY% > 1 Then 'tie to INFMOR here rather than in health model
                    lagcoef = ConvergeOverTime(0, 0.8, 10) 'phasing in the lag much reduces initial transient rise
                    sinfmor(R%) = (1 - lagcoef) * INFMOR(R%) + lagcoef * sinfmor(R%)
                    'sinfmor(R%) = 0.2 * INFMOR(R%) + 0.8 * sinfmor(R%)
                    lagcoef = ConvergeOverTime(1, infmorttfrlag, 10)
                    laginfmor(R%) = (1 - lagcoef) * INFMOR(R%) + lagcoef * laginfmor(R%)
                End If
                'bbh 2014/01/11 end

                CDR(R%) = DEATHS(R%) / POP(R%) * 1000
                'jrs 2021/11/16 enabled these lines, not sure why commented out, need updated SCDR for exogenous growth calculations using ifsexomodeltype=2 for "DEATHS"
                'dsr 2016/05/04 CDR is recalculated later in all years but the first, so only calculate SCDR here in first year
                If IY% = 1 Then
                    SCDR(R%) = CDR(R%)
                End If

                CumSDeath(R%) = CumSDeath(R%) + SDEATH(R%)


                If IY% > 1 Then
                    'jrs 2009/05/22
                    'Explained by Randall Kuhn
                    'Life Expectancy was computed as the median age at death,
                    'and it should be computed as a mean
                    'average number of years of life given the survival rates in each age group
                    'gvdb 2024/04/05
                    'unused variable
                    'Dim pct As Single

                    For gend% = 1 To 2
                        'jrs 2009/05/22
                        '               'CumMort = (d(0, 1) + d(0, 2)) / _
                        '                    AMAX(0.00001, agedst(0, R%, 1) + agedst(0, R%, 2))  'Infants
                        '               cumMort = d(0, R%, gend%) / _
                        '                    AMAX(0.00001, agedst(0, R%, gend%))    'Infants 'jrs 2007/05/21 added R% to d
                        '
                        '               LIFEXP(R%, gend%) = 0!
                        '
                        '               For j% = 1 To NCohorts
                        '                   'CohMort = (d(j%, 1) + d(j%, 2)) / _
                        '                       AMAX(0.00001, agedst(j%, R%, 1) + agedst(j%, R%, 2)) * 5 'Five year cohorts
                        '                   cohMort = d(j%, R%, gend%) / _
                        '                       AMAX(0.00001, agedst(j%, R%, gend%)) * 5 'Five year cohorts 'jrs 2007/05/21 added R% to d
                        '                   If (cumMort + cohMort >= 0.5) Then
                        '                       'mti LifeExp/ gender May 2006
                        '                       LIFEXP(R%, gend%) = LIFEXP(R%, gend%) + _
                        '                           5! * (0.5 - cumMort) / AMAX(0.0001, cohMort)
                        '                       cumMort = cumMort + cohMort
                        '    'If IY = 49 AndAlso R = 142 AndAlso gend = 1 Then
                        '    '    Print #500, LIFEXP(R%, gend%) & ", " & CohMort & ", " & CumMort
                        '    'End If
                        '                       Exit For
                        '                   Else
                        '                       cumMort = cumMort + cohMort
                        '                       LIFEXP(R%, gend%) = LIFEXP(R%, gend%) + 5!
                        '    'If IY = 49 AndAlso R = 142 AndAlso gend = 1 Then
                        '    '    Print #500, LIFEXP(R%, gend%) & ", " & CohMort & ", " & CumMort
                        '    'End If
                        '                   End If
                        '               Next j%

                        LIFEXP(R%, gend%) = 0
                        infMort = CSng(d(0, R%, gend%) / AMAX(0.00001, agedst(0, R%, gend%)))
                        lx = 1
                        cumMort = 0
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
                            'cohMort = d(j%, R%, gend%) / AMAX(0.00001, agedst(j%, R%, gend%))
                            If agedst(j%, R%, gend%) > 0 Then
                                cohMort = CSng(d(j%, R%, gend%) / agedst(j%, R%, gend%))
                            Else
                                cohMort = CSng(modmordst(j%, R%, gend%) / 1000)
                                'jrs 2013/08/26 no Life Expectancy beyond people alive
                                '                        If j > 21 Then
                                '                            cohMort = 1
                                '                        Else
                                '                            cohMort = modmordst(j%, R%, gend%) / 1000
                                '                        End If

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
                            If j% < NCohorts Then
                                LIFEXP(R%, gend%) = LIFEXP(R%, gend%) + (lx * (1 - nqx) * N) + (lx * nqx * nax)
                            Else
                                'jrs 2012/09/04
                                If cohMort > 0 Then
                                    LIFEXP(R%, gend%) = LIFEXP(R%, gend%) + lx / cohMort
                                End If
                            End If
                            'Probability of surviving until age x
                            lx = lx * (1 - nqx)
                            'Computing cummulative mortality for Barry's logic when LE going above 100
                            cumMort = cumMort + N * cohMort
                        Next j

                        If cumMort < 0.5 Then
                            LIFEXP(R%, gend%) = CSng(LIFEXP(R%, gend%) + 15 * (0.5 - cumMort) / AMAX(cumMort, 0.001))
                            'In the absence of mortality tables beyond 100 years of age, we need some
                            'way of representing what would happen if life expectancy began to exceed
                            '100 (i.e., if CumMort was less than .5 after computing mortality to age
                            '100.  Have arbitrarily put into this eqn a maximum of 15 years additional
                            'life potential as CumMort at age 100 approaches 0 (obviously it wouldn't
                            'initially be much less than .5 and would never reach 0).  This could be
                            'improved.
                        End If

                        'jrs 2013/08/26 LIFEXP(R%, gend%) = AMAX(1, Amin(LIFEXP(R%, gend%), 200))
                        LIFEXP(R%, gend%) = AMAX(1, Amin(LIFEXP(R%, gend%), context.LastYearForecast - context.FirstYearHistory + 60))

                        'jrs 2013/08/27 SHIFT NO LONGER USED LIFEXP(R%, gend%) = LIFEXP(R%, gend%) + ConvergeOverTime(LIFEXPshift(R%, gend%), 0, 50)
                        ''If IY = 49 AndAlso R = 142 AndAlso gend = 1 Then
                        ''    Print #500, LIFEXP(R%, gend%) & " After Convergence Factor"
                        ''End If
                        'jrs 2013/08/26 LIFEXP(R%, gend%) = AMAX(1, Amin(LIFEXP(R%, gend%), 200))
                        'jrs 2013/08/27 LIFEXP(R%, gend%) = AMAX(1, Amin(LIFEXP(R%, gend%), LastYearForecast - FirstYearHistory + 60))
                        '            'mti Lifexp Smooth June 2006 -end

                    Next gend%
                    'If RegName$(R%) = "China" Then Stop
                    'If IY = 49 AndAlso R = 142 Then
                    '    Close #500
                    'End If

                    'jrs 2019/10/08 We need to recompute POPMale and POPFemale every year
                    'ttt 2019/10/08 
                    'POPMale = POPMaleG(R)
                    'POPFemale = POPFemaleG(R)
                    POPMale = 0
                    POPFemale = 0
                    For j% = 1 To NCohorts
                        POPMale = POPMale + agedst(j%, R%, 1)
                        POPFemale = POPFemale + agedst(j%, R%, 2)
                    Next j%
                    POPMaleG(R) = POPMale
                    POPFemaleG(R) = POPFemale
                    'jrs 2026/07/21
                    POPBYSEX(R%, Male) = CSng(POPMale)
                    POPBYSEX(R%, Female) = CSng(POPFemale)
                    POPBYSEX(R%, 3) = CSng(POPMale + POPFemale)
                    'End ttt 2019/10/08 
                    LIFEXP(R%, Total) = CSng((LIFEXP(R%, 1) * POPMale +
               LIFEXP(R%, 2) * POPFemale) / AMAX(0.00001, POPMale + POPFemale))


                End If

                '       Repeat with No Starvation Deaths
                If IY% > 1 Then
                    'jrs 2009/05/22
                    'Explained by Randall Kuhn
                    'Life Expectancy was computed as the median age at death,
                    'and it should be computed as a mean
                    'average number of years of life given the survival rates in each age group

                    For gend% = 1 To 2
                        'jrs 2009/05/22
                        '               cumMort = deathsNS(0, gend%) / _
                        '                    AMAX(0.00001, agedst(0, R%, gend%))    'Infants
                        '
                        '               LIFEXPNS(R%, gend%) = 0!
                        '               For j% = 1 To NCohorts
                        '                   DeathCount = d(j%, R%, gend%) 'jrs 2007/05/21 added R% to d
                        '                   If j% = 1 Then DeathCount = deathsNS(1, gend%)
                        '                   cohMort = DeathCount / _
                        '                       AMAX(0.00001, agedst(j%, R%, gend%)) * 5 'Five year cohorts
                        '                   If (cumMort + cohMort >= 0.5) Then
                        '                       'mti LifeExp/ gender May 2006
                        '                       LIFEXPNS(R%, gend%) = LIFEXPNS(R%, gend%) + _
                        '                           5! * (0.5 - cumMort) / AMAX(0.0001, cohMort)
                        '                       cumMort = cumMort + cohMort
                        '                       Exit For
                        '                   Else
                        '                       cumMort = cumMort + cohMort
                        '                       LIFEXPNS(R%, gend%) = LIFEXPNS(R%, gend%) + 5!
                        '                   End If
                        '               Next j%

                        LIFEXPNS(R%, gend%) = 0
                        infMort = CSng(deathsNS(0, gend%) / AMAX(0.00001, agedst(0, R%, gend%)))
                        lx = 1
                        cumMort = 0
                        For j = 0 To NCohorts
                            'Years in cohort
                            If j = 0 Then
                                N = 1
                            ElseIf j = 1 Then
                                N = 4
                            Else
                                N = 5
                            End If

                            If j > 1 Then
                                DeathCount = d(j%, R%, gend%)
                            Else
                                DeathCount = deathsNS(j%, gend%)
                            End If

                            'jrs 2012/10/31
                            'cohMort = DeathCount / AMAX(0.00001, agedst(j%, R%, gend%))
                            If agedst(j%, R%, gend%) > 0 Then
                                cohMort = CSng(DeathCount / agedst(j%, R%, gend%))
                            Else
                                cohMort = CSng(modmordst(j%, R%, gend%) / 1000)

                                'jrs 2013/08/26 no Life Expectancy beyond people alive
                                '                        If j > 21 Then
                                '                            cohMort = 1
                                '                        Else
                                '                            cohMort = modmordst(j%, R%, gend%) / 1000
                                '                        End If

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
                            If j% < NCohorts Then
                                LIFEXPNS(R%, gend%) = LIFEXPNS(R%, gend%) + (lx * (1 - nqx) * N) + (lx * nqx * nax)
                            Else
                                'jrs 2012/09/04
                                If cohMort > 0 Then
                                    LIFEXPNS(R%, gend%) = LIFEXPNS(R%, gend%) + lx / cohMort
                                End If
                            End If
                            'Probability of surviving until age x
                            lx = lx * (1 - nqx)
                            'Computing cummulative mortality for Barry's logic when LE going above 100
                            cumMort = cumMort + N * cohMort
                        Next j

                        If cumMort < 0.5 Then
                            LIFEXPNS(R%, gend%) = CSng(LIFEXPNS(R%, gend%) + 15 * (0.5 - cumMort) / AMAX(cumMort, 0.001))
                        End If
                        LIFEXPNS(R%, gend%) = AMAX(1, Amin(LIFEXPNS(R%, gend%), 200))
                        'mti Lifexp Smooth June 2006 -start
                        If IY% = 2 Then
                            'shift = data value (first year) - calculated value (second year)
                            LIFEXPshiftNS(R%, gend%) = LifExpI(R%, gend%) - LIFEXPNS(R%, gend%)
                        End If
                        LIFEXPNS(R%, gend%) = LIFEXPNS(R%, gend%) + ConvergeOverTime(LIFEXPshiftNS(R%, gend%), 0, 50)
                        LIFEXPNS(R%, gend%) = AMAX(1, Amin(LIFEXPNS(R%, gend%), 200))
                        'mti Lifexp Smooth June 2006 -end

                    Next gend%

                    'ttt 2019/10/08 
                    POPMale = POPMaleG(R)
                    POPFemale = POPFemaleG(R)
                    'POPMale = 0
                    'POPFemale = 0
                    'For j% = 1 To NCohorts
                    '    POPMale = POPMale + agedst(j%, R%, 1)
                    '    POPFemale = POPFemale + agedst(j%, R%, 2)
                    'Next j%
                    'End ttt 2019/10/08 
                    LIFEXPNS(R%, Total) = CSng((LIFEXPNS(R%, 1) * POPMale +
            LIFEXPNS(R%, 2) * POPFemale) / AMAX(0.00001, POPMale + POPFemale))
                End If
                'If RegName$(R%) = "India N East" Then Stop

                'Compute No Starvation Life Expect because children do not effect productivity
                'mti LifeExp/ gender May 2006 -end
                'If RegName$(R%) = "Eritrea" AndAlso IY% >= 29 Then Stop

                'If RegName$(R%) = "Afghanistan" Then Stop

                'jrs 2007/05/18 Move Advance Year to Fourth Call *** begin ***
                POP0TO5(R%) = 0
                For gend% = 1 To 2
                    POP0TO5(R%) = CSng(POP0TO5(R%) + agedst(0, R%, gend%))
                    For ICount% = 1 To 4 'jrs 2015/12/23 it used to include 5 year olds.
                        POP0TO5(R%) = CSng(POP0TO5(R%) + fagedst(ICount%, R%, gend%))
                    Next ICount%
                Next gend%

                'mti 2022/11/25 start
                POPFEM15TO24(R%) = CSng(agedst(4, R%, Female) + agedst(5, R%, Female))
                'mti 2022/11/25 end

                'mti 2023/05/30 start
                For gend% = 1 To 2
                    POP15ANDABOVE(R%, gend%) = CSng(agedst(4, R%, gend%))
                    POP25ANDABOVE(R%, gend%) = 0
                    For j% = 5 To NCohorts
                        POP15ANDABOVE(R%, gend%) = CSng(POP15ANDABOVE(R%, gend%) + agedst(j, R%, gend%))
                        POP25ANDABOVE(R%, gend%) = CSng(POP25ANDABOVE(R%, gend%) + agedst(j, R%, gend%))
                    Next j
                Next gend%
                POP15ANDABOVE(R%, 3) = POP15ANDABOVE(R%, 1) + POP15ANDABOVE(R%, 2)
                POP25ANDABOVE(R%, 3) = POP25ANDABOVE(R%, 1) + POP25ANDABOVE(R%, 2)
                'mti 2023/05/30 end

                'mti 2022/10/24 Start
                'Age of marriage code - CDV - moved up
                'cdv 2022/03/07 ****START****
                'cdv 2022/03/07 ****END****
                'mti 2022/10/24 End

                '   All Years 2: WVS Cohorts
                '   ------------------------

                For ICount% = 1 To WVSCohorts  '7th cohort is actually totl adult population
                    PopCohorts(R%, ICount%) = 0
                Next ICount%
                For j% = 18 To NAges
                    PopCohorts(R%, 7) = PopCohorts(R%, 7) + fagedst(j%, R%, 1) + fagedst(j%, R%, 2)
                    'Total Adults
                    Select Case j%
                        Case 18 To 24
                            PopCohorts(R%, 1) = PopCohorts(R%, 1) + fagedst(j%, R%, 1) +
                        fagedst(j%, R%, 2)
                        Case 25 To 34
                            PopCohorts(R%, 2) = PopCohorts(R%, 2) + fagedst(j%, R%, 1) +
                        fagedst(j%, R%, 2)
                        Case 35 To 44
                            PopCohorts(R%, 3) = PopCohorts(R%, 3) + fagedst(j%, R%, 1) +
                        fagedst(j%, R%, 2)
                        Case 45 To 54
                            PopCohorts(R%, 4) = PopCohorts(R%, 4) + fagedst(j%, R%, 1) +
                        fagedst(j%, R%, 2)
                        Case 55 To 64
                            PopCohorts(R%, 5) = PopCohorts(R%, 5) + fagedst(j%, R%, 1) +
                        fagedst(j%, R%, 2)
                        Case 65 To NAges
                            PopCohorts(R%, 6) = PopCohorts(R%, 6) + fagedst(j%, R%, 1) +
                        fagedst(j%, R%, 2)
                    End Select
                Next j%
                For j% = 11 To 64
                    Select Case j%
                        Case 17
                            AdvCohorts(R%, 1) = fagedst(j%, R%, 1) + fagedst(j%, R%, 2)
                        Case 24
                            AdvCohorts(R%, 2) = fagedst(j%, R%, 1) + fagedst(j%, R%, 2)
                        Case 34
                            AdvCohorts(R%, 3) = fagedst(j%, R%, 1) + fagedst(j%, R%, 2)
                        Case 44
                            AdvCohorts(R%, 4) = fagedst(j%, R%, 1) + fagedst(j%, R%, 2)
                        Case 54
                            AdvCohorts(R%, 5) = fagedst(j%, R%, 1) + fagedst(j%, R%, 2)
                        Case 64
                            AdvCohorts(R%, 6) = fagedst(j%, R%, 1) + fagedst(j%, R%, 2)
                    End Select
                Next j%

            Next R%

            '
            '   Save Cohort Data for Display Later
            '   ----------------------------------
            'jrs 2019/11/18 ifsPopWorkDB.Database.BeginTransaction()
            '   Age Distribution
            'jrs 2024/01/24
            If MultiThreading Then
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertPop(agedst, NCohorts, NR, IY% + FirstYear - 1))
            Else
                ifsPopWorkDB.BulkInsertPop(agedst, NCohorts, NR, IY% + FirstYear - 1)
            End If
            'This statement alone inserts all rows into database, but with ifs_buffer.Post we're sending the action to a new thread, 
            'so that execution doesn't have to wait for it
            'ifsPopWorkDB.BulkInsertPop(agedst, NCohorts, NR, IY% + FirstYear - 1)
            'For tempi% = 0 To NCohorts 'jrs 2008/04/15 save infants too, it used to be from 1
            '    For tempj% = 1 To NR%
            '        ifsPopWorkDB.Add(New PopBasecase() With {
            '                .TimeId = CShort(IY% + FirstYear - 1),
            '                .RegionId = CShort(tempj%),
            '                .CohortId = CShort(tempi%),
            '                .PopulationMales = CSng(Round(agedst(tempi%, tempj%, 1), 6)), 'jrs 2012/09/18 added round 6
            '                .PopulationFemales = CSng(Round(agedst(tempi%, tempj%, 2), 6)) 'jrs 2012/09/18 added round 6
            '            })
            '    Next
            'Next
            '   Fertility Distribution
            'jrs 2024/01/24
            If MultiThreading Then
                ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertFer(modFERDST, NCohorts, NR, IY% + FirstYear - 1))
            Else
                ifsPopWorkDB.BulkInsertFer(modFERDST, NCohorts, NR, IY% + FirstYear - 1)
            End If
            'For tempi% = 0 To NCohorts 'jrs 2008/04/15 save infants too, it used to be from 1
            '    For tempj% = 1 To NR%
            '        ifsPopWorkDB.Add(New FerBasecase() With {
            '                .TimeId = CShort(IY% + FirstYear - 1),
            '                .RegionId = CShort(tempj%),
            '                .CohortId = CShort(tempi%),
            '                .Fertility = modFERDST(tempi%, tempj%)
            '            })
            '    Next
            'Next
            'jrs 2012/10/30
            If Not (hlmodelsw = 1 AndAlso (IY > 1 OrElse NCohorts > 21)) Then
                'Mortality Distribution
                'jrs 2024/01/24
                If MultiThreading Then
                    ifs_buffer.Post(Sub() ifsPopWorkDB.BulkInsertMor(modmordst, NCohorts, NR, IY% + FirstYear - 1))
                Else
                    ifsPopWorkDB.BulkInsertMor(modmordst, NCohorts, NR, IY% + FirstYear - 1)
                End If
                'For tempi% = 0 To NCohorts 'jrs 2008/04/15 save infants too, it used to be from 1
                '    For tempj% = 1 To NR%
                '        ifsPopWorkDB.Add(New MorBasecase() With {
                '            .TimeId = CShort(IY% + FirstYear - 1),
                '            .RegionId = CShort(tempj%),
                '            .CohortId = CShort(tempi%),
                '            .MortalityMales = modmordst(tempi%, tempj%, 1),
                '            .MortalityFemales = modmordst(tempi%, tempj%, 2)
                '        })
                '    Next
                'Next
            End If
            'jrs 2019/11/18             ifsPopWorkDB.Flush()
            'ifsPopWorkDB.Database.CommitTransaction()

            '
            '   All Years 2:  CBR, CDR
            '   ----------------------
            If (IY% > 1) Then
                For R% = 1 To NR%
                    CBR(R%) = BIRTHS(R%) / POP(R%) * 1000.0!
                    CDR(R%) = DEATHS(R%) / POP(R%) * 1000.0!
                    'jrs 2016/05/04
                    'SCBR(R%) = CBR(R%)
                    'dsr 2016/05/04 CDR and SCDR get overwritten in health model when hlmodelsw = 1
                    'writing SCDR here was causing problems with using exogenous series, so only
                    'calculate here when hlmodelsw <> 1
                    'SCDR(R%) = CDR(R%)
                    If hlmodelsw <> 1 Then
                        SCDR(R%) = CDR(R%) 'Save IY > 1
                        'jrs 2016/05/04
                        SCBR(R%) = CBR(R%)
                    End If
                    '            SmoothGDPPCP(R%) = SmoothGDPPCP(R%) * 0.6 + GDPPCP(R%) * 0.4
                    'If RegName$(R%) = "Israel" Then Stop
                Next R%
            End If

            '   All Years 2:  Various Indicators
            '   --------------------------------
            WPOPR = 0!
            WLIFE = 0
            'jrs 2020/08/31 begin
            'move to ifsPop3 after calculation of PQLI
            'PNO = 0!
            'PSO = 0!
            'qlno = 0!
            'qlso = 0!
            'jrs 2020/08/31 end

            For R% = 1 To NR%
                POPR(R%) = (CBR(R%) - CDR(R%)) / 1000.0! * 100.0! + MIGRATE(R%)

                'If RegName$(R%) = "Kuwait" Then Stop

                WPOPR = WPOPR + POPR(R%) * POP(R%)
                'mti LifeExp/ gender May 2006
                WLIFE = WLIFE + LIFEXP(R%, 3) * POP(R%)
                '        WLIFE = WLIFE + LIFEXP(R%) * POP(R%)

                'jrs 2020/08/31 begin
                'move to ifsPop3 after calculation of PQLI
                'If GDPPCI(R%) > nsdiv Then
                '    PNO = PNO + POP(R%)
                '    qlno = qlno + POP(R%) * PQLI(R%)
                'Else
                '    PSO = PSO + POP(R%)
                '    qlso = qlso + POP(R%) * PQLI(R%)
                'End If
                'jrs 2020/08/31 end

            Next R%

            '   All Years 2:  Urban Population Calculation
            '   ------------------------------------------
            'jrs 2009/09/30 moved down to call 3 (after Health module calculations)
            If IY% = 1 Then
                'jrs 2025/05/15 added log to name
                'jrs 2024/07/04
                TFNameUrbPct = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Urbanization Percentage (" & BaseYear & ") Log"
                TFNameUrbPct = getFunctionNameYrOrMstRct(TFNameUrbPct, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")

                For R% = 1 To NR%
                    '        'Until here, do not have good POPR, POPRI base
                    '        '   If need urban population earlier, better to lag it
                    '        '   Do not use initial calculation of POPR, too rough
                    '        If IY% = 1 Then
                    POPRI(R%) = POPR(R%)
                    If context.HistoricMode = True Then
                        Call XYTABL("GDP/Capita (PPP) Versus Urbanization Percentage (1960)",
                        GDPPCP(R%), POPURBANPOR, context)
                    Else
                        'jrs 2024/07/04 Call XYTABL("GDP/Capita (PPP) Versus Urbanization Percentage (1998)",
                        'GDPPCP(R%), POPURBANPOR, context) 'returns portion; not percent
                        Call XYTABL(TFNameUrbPct, GDPPCP(R%), POPURBANPOR, context) 'returns portion; not percent
                    End If
                    POPURBShift(R%) = AMAX(0, POPURBAN(R%) / AMAX(0.001, POP(R%)) - POPURBANPOR)
                    'bbh 2012/03/26 start
                    PopLandRat = POP(R%) / LANDAREA(R%)
                    If PopLandRat > 20 Then
                        POPURBANRMAX(R%) = 1
                    ElseIf PopLandRat > 10 Then
                        POPURBANRMAX(R%) = 0.98
                    ElseIf PopLandRat > 5 Then
                        POPURBANRMAX(R%) = 0.95
                    Else
                        POPURBANRMAX(R%) = 0.92
                    End If
                    PopUrbanRat = POPURBAN(R%) / AMAX(0.001, POP(R%))
                    POPURBANRMAX(R%) = AMAX(POPURBANRMAX(R%), PopUrbanRat)
                    'bbh 2012/03/26 end
                    'e.g. Nigeria considerably higher than expected by GDPPCP
                    ' but downward shifts are unlikely; might cut shift in 1/2 or more
                    'If RegName$(R%) = "Nigeria" Then Stop
                    '        Else
                    '            'Step 1:  Compute Long-term Function-based Target
                    '            If frm_MainMenu.IFsGUIctrl1.cHistoricBase = True Then
                    '                Call XYTABL("GDP/Capita (PPP) Versus Urbanization Percentage (1960)", _
                    '                    sgdppcp(R%) * (1 + igdpr(R%)), POPURBANPOR)
                    '            Else
                    '                Call XYTABL("GDP/Capita (PPP) Versus Urbanization Percentage (1998)", _
                    '                    sgdppcp(R%) * (1 + igdpr(R%)), POPURBANPOR) 'returns portion; not percent
                    '            End If
                    '            POPURBANPOR = Amin(0.9, POPURBANPOR + POPURBShift(R%))
                    '            UrbanShiftGr = ConvergeOverTime(0.004, 0.002, 100)
                    '            POPURBANTAR = POPURBANPOR * (1 + UrbanShiftGr * IY%) '.0025 should go to 0 over time
                    '            If POPURBANTAR - POPURBANPOR > 0.1 * (0.9 - POPURBANPOR) Then
                    '                POPURBANPOR = POPURBANPOR + 0.1 * (0.9 - POPURBANPOR)
                    '            Else
                    '                POPURBANPOR = POPURBANTAR
                    '            End If
                    '            PopUrbanFunc = POP(R%) * Amin(0.9, POPURBANPOR)
                    '
                    '            'Step 2: Adjust urban growth with Pop Growth
                    '            POPUrbGrAdj = 1.5 * (POPR(R%) - POPRI(R%)) / 100
                    '                'Reln between POPR and POPURBGR has about 1.5 slope
                    '            PopUrbGrBase = PopUrbGRI(R%) + POPUrbGrAdj
                    '
                    ''            PopUrbGrConvFac = (PopUrbGrBase - POPR(R%) / 100) * Amin(1, IY% / 20)
                    ''            PopGrReduced = AMAX(POPR(R%) / 100, PopUrbGrBase - PopUrbGrConvFac)
                    ''            PopUrbanCalc = SPopUrban(R%) * (1 + PopGrReduced)
                    '
                    '            PopGrReduced = ConvergeOverTimeSlowing(PopUrbGrBase, POPR(R%) / 100, 20, 66)
                    '            'ConvergeOverTimeSlowing(Base, Target, YearsToConverge, MidwayPercShare)
                    '            PopUrbanCalc = SPopUrban(R%) * (1 + PopGrReduced)
                    '
                    '
                    'If RegName$(R%) = "Nigeria" AndAlso IY% >= 25 Then Stop
                    ''If RegName$(R%) = "Nigeria" Then Stop
                    '
                    '            'Step 3:  Converge to Long-term Target, mostly upward
                    '                'Indian urbanization way behind forecast; otherwise pulls too fast
                    '            If PopUrbanFunc > PopUrbanCalc Then 'Upward convergence, more urban
                    '                'POPURBAN(R%) = ConvergeOverTime(PopUrbanCalc, PopUrbanFunc, 250)
                    '                POPURBAN(R%) = PopUrbanCalc * 0.98 + PopUrbanFunc * 0.02
                    '            Else 'Downward convergence improbable, very slow
                    '                'POPURBAN(R%) = ConvergeOverTime(PopUrbanCalc, PopUrbanFunc, 2000)
                    '                POPURBAN(R%) = PopUrbanCalc * 0.999 + PopUrbanFunc * 0.001
                    '            End If
                    '                'Base, Target, Years  'tfrconv?
                    '
                    '            'Step 4:  Complete computations
                    '            POPURBAN(R%) = Amin(POP(R%), AMAX(0.01 * POP(R%), POPURBAN(R%)))
                    '            POPURBGR(R%) = (POPURBAN(R%) / SPopUrban(R%) - 1) * 100
                    '            SPopUrban(R%) = POPURBAN(R%)
                    '        End if
                Next R%
            End If

            '   All Years 2: Calories per Capita
            '   -------------------------------
            Dim Sum As Single
            Sum = 0
            For R% = 1 To NR%
                Sum = Sum + CLAVAL(R%)
                CLPC(R%, 4) = AMAX(1500, CLAVAL(R%) / POP(R%))
            Next R%
            WCLPC = Sum / WPOP

            '   All Years 2: Malnourished Children (0-5 Years of Age)
            'bbh 2019/06/02 ifsPopMALN malnchpi, smalncomp, smalnmul, CLPCi, malnchpsami, smalnsamcomp, samadmshift, sSAMAVERTED, _
            'malnpoppi, smalnpopmul
            ifsPopMALN(malnchpi, MalnChilCompI, smalnmul, CLPCi, malnchpsami, smalnsamcomp, samadmshift, sSAMAVERTED,
            malnpoppi, smalnpopmul, MalnChilShift, SMalnChP, context) 'bbh 2019/06/02

            '   All Years 2: Malnourished Population
            '   -------------------------------------
            'ifsPopMALN

            '   All Years 2:  Global Indicators
            '   -------------------------------
            WPOPR = WPOPR / WPOP
            WLIFE = WLIFE / WPOP
            'jrs 2020/08/31 begin
            'move to ifsPop3 after calculation of PQLI
            'qlno = qlno / AMAX(PNO, 1)
            'qlso = qlso / AMAX(PSO, 1)
            'NSPQLI = CSng(qlno / AMAX(qlso, 0.01))
            'jrs 2020/08/31 end
        End Sub

        'jrs 2024/06/20
        Dim TFNameSAMAdmissions As String, TFNameCoefVarPessimistic As String, TFNameCoefVarOptimistic As String, TFNameCoefVarMedian As String

        'jrs 2015/12/09
        Private Sub ifsPopMALN(ByRef malnchpi As Single(), ByRef MalnChilCompI As Single(), ByRef smalnmul As Single(),
        ByRef CLPCi As Single(), ByRef malnchpsami As Single(), ByRef smalnsamcomp As Single(),
        ByRef samadmshift As Single(), ByRef sSAMAVERTED As Single(),
        ByRef malnpoppi As Single(), ByRef smalnpopmul As Single(), ByRef MalnChilShift As Single(),
        ByRef SMalnChP As Single(), context As IForecastContext) 'bbh 2019/06/02 changed name of parameter from smalncomp to MalnChilCompI, and added , ByRef MalnChilShift As Variant, ByRef SMalnChP As Variant

            '   All Years 2: Malnourished Children (0-5 Years of Age)
            '   -----------------------------------------------------
            'jrs 2009/12/23
            Dim z As Single
            Dim TbName As String
            Dim samadmcomp, malncomp, ConvTime, malnbase, malnmul, MalnCompI, MaxChg, TimeFAc As Single

            If IY% = 1 Then
                'jrs 2016/03/07
                ReDim ISAMADMISSION(NR)

                'jrs 2024/06/20
                TFNameSAMAdmissions = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus SAM Admissions (Linear)"
                TFNameSAMAdmissions = getFunctionNameYrOrMstRct(TFNameSAMAdmissions, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
                TFNameCoefVarPessimistic = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Coef. of Variance (" & BaseYear & ") (Pessimistic) Power"
                TFNameCoefVarPessimistic = getFunctionNameYrOrMstRct(TFNameCoefVarPessimistic, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
                TFNameCoefVarOptimistic = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Coef. of Variance (" & BaseYear & ") (Optimistic) Power"
                TFNameCoefVarOptimistic = getFunctionNameYrOrMstRct(TFNameCoefVarOptimistic, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
                TFNameCoefVarMedian = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Coef. of Variance (" & BaseYear & ") (Median) Power"
                TFNameCoefVarMedian = getFunctionNameYrOrMstRct(TFNameCoefVarMedian, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")

                'first year
                '-------------------
                For R% = 1 To NR%
                    'malnchpi(R%) = MALNCHP(R%) 'bbh 2019/06/02 replaced this with following
                    malnchpi(R%) = AMAX(0.001, MALNCHP(R%))
                    CLPCi(R%) = CLPC(R%, 4)  'Also used for total population malnutrition computation

                    'jrs 2020/04/20
                    If context.HistoricMode = True Then
                        Call XYTABL("Calories per capita (2007) versus Malnourished Children",
                            CLPCi(R%), MalnChilCompI(R%), context) 'bbh 2019/06/02 was malncompi; this history branch looks weak
                    Else
                        'jrs 2009/12/16
                        'jrs 2009/12/23 smalncomp(R%) = Exp(16.68 - 1.49 * Log(CLPC(R%, 4)) + _
                        '   0.17 * Log(AMAX(SINCOMELT1LN(R%) / POP(R%) * 100, 0.0001)) - _
                        '   0.74 * Log(AMAX(SANITATION(R%, 2) + SANITATION(R%, 3), 0.0001)))
                        'mti 2022/08/17 start
                        'mti 2022/10/20 start 'watsafefwdsw
                        'If watsafenewsw = 1 Then 'new 5 category model
                        If watsafenewsw = 1 And watsafefwdsw = 1 Then 'new 5 category model
                            'mti 2022/10/20 end
                            'mti 2022/11/15 start
                            'WaterImproved = Amin(100, AMAX(0, 100 - WATERHH(R%, Surface, National) + WATERHH(R%, Unimproved, National)))
                            'SanitImproved = Amin(100, AMAX(0, 100 - SANITHH(R%, OpenDefecation, National) + SANITHH(R%, Unimproved, National)))

                            Dim WaterImproved = Amin(100, AMAX(0, 100 - (WATERHH(R%, Surface, National) + WATERHH(R%, Unimproved, National))))
                            Dim SanitImproved = Amin(100, AMAX(0, 100 - (SANITHH(R%, OpenDefecation, National) + SANITHH(R%, Unimproved, National))))
                            'mti 2022/11/15 end
                            z = CSng(1.9212672 - 0.00069 * CLPC(R%, 4) - 0.011589 * (WaterImproved) -
                                0.020278 * (SanitImproved))

                        Else
                            z = CSng(1.9212672 - 0.00069 * CLPC(R%, 4) - 0.011589 * (WATSAFE(R%, 2) + WATSAFE(R%, 3)) -
                        0.020278 * (SANITATION(R%, 2) + SANITATION(R%, 3)))
                        End If
                        'mti 2022/08/17 End
                        MalnChilCompI(R%) = CSng(100 * Exp(z) / (1 + Exp(z))) 'bbh 2019/06/02 Changed computed variable name so have this across time
                    End If

                    'bbh 2019/06/02 start
                    MalnChilCompI(R%) = AMAX(0.001, MalnChilCompI(R%))
                    MalnChilShift(R%) = AMAX(0.001, malnchpi(R%)) / MalnChilCompI(R%)
                    SMalnChP(R%) = malnchpi(R%)
                    'bbh not sure why code does no have malncompi = smalncomp(R%) here rather than in years after first
                    'bbh 2019/06/02 end

                    smalnmul(R%) = 1
                    'jrs 2015/11/27 *************** begin *******************************************************
                    'SAM
                    'David needs to change this regression, currently the same as MALNCHP, which makes SAM converge to MALNCHP with time
                    '------------------------------------------------------------------------------------------
                    'z = 1.9212672 - 0.00069 * CLPC(R%, 4) - 0.011589 * (WATSAFE(R%, 2) + WATSAFE(R%, 3)) - _
                    '    0.020278 * (SANITATION(R%, 2) + SANITATION(R%, 3))
                    '------------------------------------------------------------------------------------------

                    'clp 2017/11/13 - Updating SAM equation
                    'jrs 2016/01/13 - updating SAM equation
                    'dkb 2015/12/11 - Updating SAM equation
                    '------------------------------------------------------------------------------------------
                    'z = Amin(1.9212672 - 0.00069 * CLPC(R%, 4) - 0.011589 * (SWATSAFE(R%, 2) + SWATSAFE(R%, 3)) - _
                    '0.020278 * (SSANITATION(R%, 2) + SSANITATION(R%, 3)), 709)
                    'z = Amin(-2.977 - 0.084 * EDYRSAG25(R%, 3) - 0.147 * (EDYRSAG25(R%, 2) - EDYRSAG25(R%, 3)) - 0.187 * GOVCORRUPT(R%) + 0.103 * SFINTLWARMAG(R%), 709)
                    z = CSng(Amin(-2.455 - 0.103 * EDYRSAG25(R%, 3) - 0.306 * GOVCORRUPT(R%) + 0.114 * SFINTLWARMAG(R%), 709))
                    '------------------------------------------------------------------------------------------
                    smalnsamcomp(R%) = CSng(100 * Exp(z) / (1 + Exp(z)))
                    malnchpsami(R%) = MALNCHPSAM(R%)
                    'jrs 2021/05/13
                    SMALNCHPSAM(R%) = MALNCHPSAM(R%)

                    'SAM Incidence
                    'jrs 2016/03/01 MALNCHPSAMINC(R%) = MALNCHPSAM(R%) * (1 + malnchpsamadj) 'jrs 2016/02/19
                Next R%

                'SAM Admissions
                '===================
                'Initialization
                '==============
                SAMAdmissionInitialization(context)

                'first year
                '-------------------
                For R% = 1 To NR%
                    'dkb 2015/12/18 - SAM Admissions have a negative relationship with GDP/Capita (log),
                    'so forecasts will tend to decline slightly.
                    'jrs 2016/01/18 TbName$ = "GDP/Capita (PPP 2011) Versus MalnChilSAMAdmission% Log"
                    'jrs 2024/06/20 TbName$ = "GDP/Capita (PPP 2011) Versus SAM Admissions (Linear)"
                    Call XYTABL(TFNameSAMAdmissions, GDPPCP(R%), samadmcomp, context)
                    'jrs 2016/03/08
                    '            'validations
                    '            If samadmcomp > 100 Then
                    '                samadmcomp = 100
                    '            End If
                    '            If samadmcomp < 0 Then
                    '                samadmcomp = 0.01
                    '            End If
                    samadmshift(R%) = SAMADMISSION(R%) - samadmcomp 'jrs 2016/02/15 changed to additive shift factor
                    'jrs 2016/03/07
                    If samadmissionsw = 1 AndAlso SAMADMISSION(R%) = 0 Then
                        samadmshift(R%) = -9999
                    End If
                    ISAMADMISSION(R%) = SAMADMISSION(R%)

                    'SAM cure
                    sSAMAVERTED(R%) = SAMAVERTED(R%)
                    'jrs 2016/01/22
                    'SAMADMISSIONTOTCOST(R%) = MALNCHPSAM(R%) / 100 * SAMADMISSION(R%) / 100 * POP0TO5(R%) * 1000000 * samadmissioncost
                    ''jrs 2016/01/25 SAMADMISSIONTOTCOST(R%) = MALNCHPSAM(R%) * SAMADMISSION(R%) * POP0TO5(R%) * 100 * samadmissioncost(R%)
                    'jrs 2016/02/17
                    '            'jrs 2016/02/04
                    '            If SAMADMISSION(R%) = 100 AndAlso SAMAVERTED(R%) = 100 Then
                    '                SAMADMISSIONTOTCOST(R%) = (MALNCHPSAM(R%) + MALNCHPSAMINC(R%)) * SAMADMISSION(R%) * POP0TO5(R%) / 10000 * samadmissioncost(R%)
                    '            Else
                    '                SAMADMISSIONTOTCOST(R%) = (MALNCHPSAM(R%) + MALNCHPSAMINC(R%)) / (1 - SAMADMISSION(R%) / 100 * SAMAVERTED(R%) / 100) * SAMADMISSION(R%) * POP0TO5(R%) / 10000 * samadmissioncost(R%)
                    '            End If
                    'jrs 2016/03/01
                    '            If SAMADMISSION(R%) = 100 AndAlso SAMAVERTED(R%) = 100 Then
                    '                SAMADMISSIONTOTCOST(R%) = (MALNCHPSAM(R%) * (1 + malnchpsamadj)) * SAMADMISSION(R%) * POP0TO5(R%) / 10000 * samadmissioncost(R%)
                    '            Else
                    '                SAMADMISSIONTOTCOST(R%) = (MALNCHPSAM(R%) / (1 - SAMADMISSION(R%) / 100 * SAMAVERTED(R%) / 100) * (1 + malnchpsamadj)) * SAMADMISSION(R%) * POP0TO5(R%) / 10000 * samadmissioncost(R%)
                    '            End If
                    If SAMADMISSION(R%) = 100 AndAlso SAMAVERTED(R%) = 100 Then
                        SAMADMISSIONTOTCOST(R%) = MALNCHPSAM(R%) * malnchpsamadj * SAMADMISSION(R%) * POP0TO5(R%) / 10000 * samadmissioncost(R%)
                        MALNCHPSAMINC(R%) = MALNCHPSAM(R%) * malnchpsamadj
                    Else
                        SAMADMISSIONTOTCOST(R%) = (MALNCHPSAM(R%) * Amin(malnchpsamadj / (1 - SAMADMISSION(R%) / 100 * SAMAVERTED(R%) / 100), 5)) * SAMADMISSION(R%) * POP0TO5(R%) / 10000 * samadmissioncost(R%) 'jrs 2016/03/07 added amin to 5
                        MALNCHPSAMINC(R%) = MALNCHPSAM(R%) * Amin(malnchpsamadj / (1 - SAMADMISSION(R%) / 100 * SAMAVERTED(R%) / 100), 5) 'jrs 2016/03/07 added amin to 5
                    End If
                    'jrs 2016/02/17 adding total averted
                    'jrs 2016/03/01 MALNCHPSAMINC(R%) = MALNCHPSAMINC(R%) + SAMADMISSION(R%) / 100 * SAMAVERTED(R%) / 100 * MALNCHPSAM(R%) * (1 + malnchpsamadj)
                Next R%
                'jrs 2015/11/27 *************** end *******************************************************
            Else
                'All years but first
                '-------------------
                For R% = 1 To NR%
                    If context.HistoricMode = True Then
                        Call XYTABL("Calories per capita (2007) versus Malnourished Children",
                            CLPC(R%, 4), malncomp, context)
                        Call XYTABL("Calories per capita (2007) versus Malnourished Children",
                            CLPCi(R%), MalnChilCompI(R%), context) 'bbh 2019/06/02 was malncompi; this history branch looks weak
                    Else
                        'jrs 2009/12/16
                        '                Call XYTABL("Calories per Capita Versus Maln Children % (1995)", _
                        '                    CLPC(R%, 4), malncomp)
                        '                Call XYTABL("Calories per Capita Versus Maln Children % (1995)", _
                        '                    CLPCi(R%), malncompi)
                        'jrs 2009/12/23 malncomp = Exp(16.68 - 1.49 * Log(CLPC(R%, 4)) + _
                        '   0.17 * Log(AMAX(SINCOMELT1LN(R%) / POP(R%) * 100, 0.0001)) - _
                        '   0.74 * Log(AMAX(SSANITATION(R%, 2) + SSANITATION(R%, 3), 0.001)))
                        'jrs 2013/09/09 z = 1.9212672 - 0.00069 * CLPC(R%, 4) - 0.011589 * (SWATSAFE(R%, 2) + SWATSAFE(R%, 3)) - _
                        '0.020278 * (SSANITATION(R%, 2) + SSANITATION(R%, 3))
                        'mti 2022/08/17 start
                        'mti 2022/10/20 start 'watsafefwdsw
                        'If watsafenewsw = 1 Then 'new 5 category model
                        If watsafenewsw = 1 And watsafefwdsw = 1 Then 'new 5 category model
                            'mti 2022/10/20 end
                            'mti 2022/11/15 start
                            'WaterImproved = Amin(100, AMAX(0, 100 - sWATERHH(R%, Surface, National) + sWATERHH(R%, Unimproved, National)))
                            'SanitImproved = Amin(100, AMAX(0, 100 - sSANITHH(R%, OpenDefecation, National) + sSANITHH(R%, Unimproved, National)))

                            Dim WaterImproved = Amin(100, AMAX(0, 100 - (sWaterHH(R%, Surface, National) + sWaterHH(R%, Unimproved, National))))
                            Dim SanitImproved = Amin(100, AMAX(0, 100 - (sSanitHH(R%, OpenDefecation, National) + sSanitHH(R%, Unimproved, National))))
                            'mti 2022/11/15 end
                            z = CSng(1.9212672 - 0.00069 * CLPC(R%, 4) - 0.011589 * (WaterImproved) -
                            0.020278 * (SanitImproved))

                        Else
                            z = CSng(Amin(1.9212672 - 0.00069 * CLPC(R%, 4) - 0.011589 * (SWATSAFE(R%, 2) + SWATSAFE(R%, 3)) -
                            0.020278 * (SSANITATION(R%, 2) + SSANITATION(R%, 3)), 709))
                        End If
                        'mti 2022/08/17 End

                        malncomp = CSng(100 * Exp(z) / (1 + Exp(z)))
                        'jrs 2020/04/20 it wasn't computing MALNCHP in historic mode 
                    End If

                    'bbh 2019/06/02 start
                    '   5 factors drive change in proportion undernourished
                    '     (1) change in logisitc fucntion computation over time as drivers change
                    '     (2) shift factor, which converges toward 1, slowed substantially for upward movement
                    '     (3) a year-to-year constraint of 5% change in either direction
                    '     (4) malnm, multiplier
                    '     (5) possible override to hold constant at initial value

                    malncomp = AMAX(0.001, malncomp)
                    'malncomp = malnchpi(R%) * malncomp / MalnChilCompI(R%)

                    ConvTime = malnconv
                    If MalnChilShift(R%) < 1 Then ConvTime = ConvTime * 3
                    malncomp = malncomp * ConvergeOverTime(MalnChilShift(R%), 1, ConvTime)

                    MaxChg = 5
                    malncomp = AMAX(malncomp, SMalnChP(R%) - MaxChg)
                    malncomp = Amin(malncomp, SMalnChP(R%) + MaxChg)
                    MALNCHP(R%) = malncomp

                    'jrs 2021/05/13 move up to avoid saving multiplier effect, problems using 1.2 for Yemen
                    SMalnChP(R%) = MALNCHP(R%)

                    If malnhldsw = 0 Then
                        'jrs 2008/01/15 added Malnutrition Multiplier
                        MALNCHP(R%) = AMAX(0.001, Amin(MALNCHP(R%) * malnm(R%), 100))
                    Else
                        MALNCHP(R%) = AMAX(0.001, Amin(malnchpi(R%), 100))
                    End If

                    'jrs 2021/05/13 SMalnChP(R%) = MALNCHP(R%)


                    'bbh 2019/06/02 end

                    'jrs 2019/06/04 MalnCompI = smalncomp(R%)
                    'jrs 2019/06/04 MalnCompI = smalncomp  'Possible issue for SAM? bbh 2019/06/02
                    'I am not changing logic around malncompi because it seems to affect SAM computations (which look like would benefit also from reviwew)
                    'jrs 2019/06/04 MalnCompI = MalnChilCompI(R%)
                    'jrs 2020/04/20 it wasn't computing MALNCHP in historic mode End If

                    'bbh 2019/06/02 start block commented out
                    '            If malncomp <= 0 AndAlso MalnCompI <= 0 Then
                    '                malnmul = 1
                    '            ElseIf malncomp <= 0 AndAlso MalnCompI > 0 Then
                    '                malnmul = 0
                    '            ElseIf MalnCompI = 0 Then 'jrs 2009/12/17
                    '                malnmul = malncomp  'allows some undernutriton to begin even if 0 intially
                    '            Else
                    '                'malnmul = AMAX(0, malncomp) / AMAX(1, malncompi)
                    '                malnmul = malncomp / MalnCompI 'potentially unstable branch, see below
                    '            End If
                    '
                    '            'If initial computation of undernutriton is very near 0, the multiplier can be
                    '            '   unstable with small future changes; bound changes in multiplier to control
                    '            MaxChg = 0.05
                    '            malnmul = AMAX(smalnmul(R%) * (1 - MaxChg), Amin(smalnmul(R%) * _
                    '                (1 + MaxChg), malnmul))
                    '            smalnmul(R%) = malnmul
                    '
                    '' If RegName$(R%) = "Netherlands" Then Stop
                    '
                    ''           Adjust initial value by multipler but converge to computed value
                    '            TimeFAc = Amin(IY%, malnconv)
                    '            malnconv = AMAX(1, malnconv)
                    '            'jrs 2009/12/18
                    '            If malnchpi(R%) = 0 AndAlso CLPCi(R) > 3200 Then
                    '                malnbase = 0
                    '            Else
                    '                malnbase = (malnchpi(R%) * malnmul * (malnconv - TimeFAc) + _
                    '                    malncomp * TimeFAc) / malnconv
                    '            End If
                    '
                    '            'jrs 2010/01/08
                    '            If malnhldsw = 0 Then
                    '                'jrs 2008/01/15 added Malnutrition Multiplier
                    '                malnbase = malnbase * malnm(R%)
                    '                MALNCHP(R%) = AMAX(0, Amin(malnbase, 100))
                    '            Else
                    '                MALNCHP(R%) = malnchpi(R%)
                    '            End If
                    'bbh 2019/06/02 end block commented out

                    'All years but first
                    '-------------------
                    'jrs 2015/11/27 ********************** begin **********************************
                    'SAM: Severe Acute Malnutrition
                    'David needs to change this regression, currently the same as MALNCHP, which makes SAM converge to MALNCHP with time
                    '------------------------------------------------------------------------------------------
                    'z = Amin(1.9212672 - 0.00069 * CLPC(R%, 4) - 0.011589 * (SWATSAFE(R%, 2) + SWATSAFE(R%, 3)) - _
                    '0.020278 * (SSANITATION(R%, 2) + SSANITATION(R%, 3)), 709)
                    '------------------------------------------------------------------------------------------

                    'clp 2017/11/13 - Updating SAM equation
                    'jrs 2016/01/13 - updating SAM equation
                    'dkb 2015/12/11 - Updating SAM equation
                    '------------------------------------------------------------------------------------------
                    'z = Amin(1.9212672 - 0.00069 * CLPC(R%, 4) - 0.011589 * (SWATSAFE(R%, 2) + SWATSAFE(R%, 3)) - _
                    '0.020278 * (SSANITATION(R%, 2) + SSANITATION(R%, 3)), 709)
                    'z = Amin(-2.977 - 0.084 * SEDYRSAG25(R%, 3) - 0.147 * (SEDYRSAG25(R%, 2) - SEDYRSAG25(R%, 3)) - _
                    '0.187 * GOVCORRUPT(R%) + 0.103 * SSFINTLWARMAG(R%), 709)
                    z = CSng(Amin(-2.455 - 0.103 * SEDYRSAG25(R%, 3) - 0.306 * GOVCORRUPT(R%) + 0.114 * SSFINTLWARMAG(R%), 709))
                    '------------------------------------------------------------------------------------------

                    'jrs 2019/07/26 this line was commented out by bbh on 2019/06/02, and is still needed below
                    '           Adjust initial value by multipler but converge to computed value
                    TimeFAc = Amin(IY%, malnconv)

                    malncomp = CSng(100 * Exp(z) / (1 + Exp(z)))
                    MalnCompI = smalnsamcomp(R%)

                    If malncomp <= 0 AndAlso MalnCompI <= 0 Then
                        malnmul = 1
                    ElseIf malncomp <= 0 AndAlso MalnCompI > 0 Then
                        malnmul = 0
                    ElseIf MalnCompI = 0 Then 'jrs 2009/12/17
                        malnmul = malncomp  'allows some undernutriton to begin even if 0 intially
                    Else
                        'malnmul = AMAX(0, malncomp) / AMAX(1, malncompi)
                        malnmul = malncomp / MalnCompI 'potentially unstable branch, see below
                    End If

                    If malnchpsami(R%) = 0 AndAlso CLPCi(R) > 3200 Then
                        malnbase = 0
                    Else
                        malnbase = (malnchpsami(R%) * malnmul * (malnconv - TimeFAc) +
                            malncomp * TimeFAc) / malnconv
                    End If

                    If malnhldsw = 0 Then
                        malnbase = malnbase * malnm(R%)
                        If malnchpsamm(R%) <> 1 Then
                            Dim malnDiff As Single
                            malnDiff = malnbase * (malnchpsamm(R%) - 1)
                            MALNCHP(R%) = AMAX(0, Amin(MALNCHP(R%) + malnDiff, 100))
                            malnbase = malnbase + malnDiff
                        End If
                        MALNCHPSAM(R%) = CSng(AMAX(0, Amin(malnbase, 0.91 * MALNCHP(R%)))) 'I'm leavign at least 9% room for non-severe malnutrition (that's the min level I saw from Data, Eq. Guinea)
                    Else
                        MALNCHPSAM(R%) = malnchpsami(R%)
                    End If

                    'SAM Incidence
                    'jrs 2016/03/01 MALNCHPSAMINC(R%) = MALNCHPSAM(R%) * (1 + malnchpsamadj) 'jrs 2016/02/19


                    'All years but first
                    '-------------------
                    'SAM Admissions
                    'jrs 2016/01/18 TbName$ = "GDP/Capita (PPP 2011) Versus MalnChilSAMAdmission% Log"
                    'jrs 2024/06/20 TbName$ = "GDP/Capita (PPP 2011) Versus SAM Admissions (Linear)"
                    Call XYTABL(TFNameSAMAdmissions, GDPPCP(R%), samadmcomp, context)
                    'If GRPS(1, R%) = "Qatar" AndAlso IY = 2 Then Stop
                    'jrs 2016/03/07
                    If samadmissionsw = 1 AndAlso samadmshift(R%) = -9999 Then
                        SAMADMISSION(R%) = 0
                    ElseIf samadmissionsw = 2 Then
                        SAMADMISSION(R%) = ISAMADMISSION(R%)
                    Else
                        SAMADMISSION(R%) = samadmcomp + samadmshift(R%) 'jrs 2016/02/15 changed to additive shift factor
                    End If

                    'dkb 2015/12/18 - Bounding SAMADMISSION to 100%. Mostly affecting for countries with large samadmshift (small samadcomp)
                    SAMADMISSION(R%) = AMAX(Amin(SAMADMISSION(R%), 100), 0)
                    'dkb 2015/12/18 - Adding convergance
                    'SAMADMISSION(R%) = ConvergeOverTime(SAMADMISSION(R%), 100, 100)

                    'If GRPS(1, R%) = "India" AndAlso IY >= 9 Then Stop

                    'jrs 2016/02/17
                    '            'jrs 2016/02/04
                    '            If SAMADMISSION(R%) = 100 AndAlso SAMAVERTED(R%) = 100 Then
                    '                'jrs 2016/01/26
                    '                SAMADMISSIONTOTCOST(R%) = (MALNCHPSAM(R%) + MALNCHPSAMINC(R%)) * Amin(SAMADMISSION(R%) * samadmissionm(R%), 100) * POP0TO5(R%) / 10000 * samadmissioncost(R%)
                    '            Else
                    '                'Using the Pre endogenous averted value of SAM
                    '                'PREEndogAvertedSAM = SAM / (1-Adm/100*Avert/100)
                    '                SAMADMISSIONTOTCOST(R%) = (MALNCHPSAM(R%) + MALNCHPSAMINC(R%)) / (1 - SAMADMISSION(R%) / 100 * SAMAVERTED(R%) / 100) * Amin(SAMADMISSION(R%) * samadmissionm(R%), 100) * POP0TO5(R%) / 10000 * samadmissioncost(R%)
                    '            End If
                    'If GRPS(1, R) = "Niger" AndAlso FirstYear + IY - 1 = 2016 Then Stop

                    'jrs 2016/03/01
                    '            If SAMADMISSION(R%) = 100 AndAlso SAMAVERTED(R%) = 100 Then
                    '                SAMADMISSIONTOTCOST(R%) = (MALNCHPSAM(R%) * (1 + malnchpsamadj)) * SAMADMISSION(R%) * POP0TO5(R%) / 10000 * samadmissioncost(R%)
                    '            Else
                    '                SAMADMISSIONTOTCOST(R%) = (MALNCHPSAM(R%) / (1 - SAMADMISSION(R%) / 100 * SAMAVERTED(R%) / 100) * (1 + malnchpsamadj)) * SAMADMISSION(R%) * POP0TO5(R%) / 10000 * samadmissioncost(R%)
                    '            End If

                    'All years but first
                    '-------------------
                    'SAM cured
                    'dkb 2015/12/09 - Changing convegence (origionally to 100 over 50 years)
                    'dkb 2015/12/18 - Convergence looks too artifical. Now holding at initial values.
                    SAMAVERTED(R%) = ConvergeOverTime(sSAMAVERTED(R%), 100, samavertedconvyr)  'jrs 2016/03/08 changed from 100 to samavertedconvbyr

                    'jrs 2016/03/01
                    If SAMADMISSION(R%) = 100 AndAlso SAMAVERTED(R%) = 100 Then
                        SAMADMISSIONTOTCOST(R%) = (MALNCHPSAM(R%) * malnchpsamadj) * SAMADMISSION(R%) * POP0TO5(R%) / 10000 * samadmissioncost(R%)
                        MALNCHPSAMINC(R%) = MALNCHPSAM(R%) * malnchpsamadj
                    Else
                        SAMADMISSIONTOTCOST(R%) = (MALNCHPSAM(R%) * Amin(malnchpsamadj / (1 - SAMADMISSION(R%) / 100 * SAMAVERTED(R%) / 100), 5)) * SAMADMISSION(R%) * POP0TO5(R%) / 10000 * samadmissioncost(R%)
                        MALNCHPSAMINC(R%) = MALNCHPSAM(R%) * Amin(malnchpsamadj / (1 - SAMADMISSION(R%) / 100 * SAMAVERTED(R%) / 100), 5) 'jrs 2016/03/07 added amin to 5
                    End If

                    Dim DiffAverted As Single, DiffAdmission As Single
                    DiffAdmission = 0
                    DiffAverted = 0

                    'jrs 2016/02/17 adding total averted
                    'jrs 2016/03/01 MALNCHPSAMINC(R%) = MALNCHPSAMINC(R%) + SAMADMISSION(R%) / 100 * SAMAVERTED(R%) / 100 * MALNCHPSAM(R%) * (1 + malnchpsamadj)

                    If samavertedm(R%) <> 1 OrElse samadmissionm(R%) <> 1 OrElse samadmissionadd(R%) <> 0 Then
                        DiffAdmission = (SAMADMISSION(R%) + samadmissionadd(R%)) * samadmissionm(R%) - SAMADMISSION(R%) 'jrs 2016/02/13
                        DiffAdmission = AMAX(Amin(DiffAdmission, 100 - SAMADMISSION(R%)), -1 * SAMADMISSION(R%))
                        SAMADMISSION(R%) = (SAMADMISSION(R%) + samadmissionadd(R%)) * samadmissionm(R%)
                        'dkb 2015/12/18 - Bounding SAMADMISSION to 100%. Mostly affecting for countries with large samadmshift (small samadcomp)
                        SAMADMISSION(R%) = AMAX(Amin(SAMADMISSION(R%), 100), 0)
                        'jrs 2016/02/13
                        If DiffAdmission <> 0 Then
                            If SAMADMISSIONTOTCOST(R%) = 0 Then 'jrs 2016/02/15
                                'jrs 2016/04/05 I had not removed the extra + 1 that went with malnchpsamadj, so I was mult by 2.6 instead of 1.6
                                SAMADMISSIONTOTCOST(R%) = (MALNCHPSAM(R%) * (malnchpsamadj)) * SAMADMISSION(R%) * POP0TO5(R%) / 10000 * samadmissioncost(R%)
                            ElseIf SAMADMISSION(R%) - DiffAdmission > 0 Then
                                SAMADMISSIONTOTCOST(R%) = SAMADMISSIONTOTCOST(R%) * SAMADMISSION(R%) / (SAMADMISSION(R%) - DiffAdmission)
                            Else
                                SAMADMISSIONTOTCOST(R%) = 0
                            End If
                        End If

                        DiffAverted = SAMAVERTED(R%) * (samavertedm(R%) - 1)
                        DiffAverted = AMAX(Amin(DiffAverted, 100 - SAMAVERTED(R%)), -1 * SAMAVERTED(R%))
                        SAMAVERTED(R%) = SAMAVERTED(R%) * samavertedm(R%)
                        'jrs 2016/01/28
                        SAMAVERTED(R%) = Amin(SAMAVERTED(R%), 100)
                    End If

                    'jrs 2016/01/29 sSAMAVERTED(R%) = SAMAVERTED(R%)

                    'All years but first
                    '-------------------
                    'Adjust SAM
                    Dim SAMAVERTEDAsPctOfTotalPop As Single, SAMAVERTEDAsPctOfTotalPop2 As Single
                    SAMAVERTEDAsPctOfTotalPop = 0
                    SAMAVERTEDAsPctOfTotalPop2 = 0
                    'If GRPS(1, R) = "India" AndAlso FirstYear + IY - 1 > 2015 Then Stop
                    If malnhldsw = 0 AndAlso (samavertedm(R%) <> 1 OrElse samadmissionm(R%) <> 1 OrElse samadmissionadd(R%) <> 0) Then 'jrs 2016/02/13
                        'jrs 2016/02/17
                        '                SAMAVERTEDAsPctOfTotalPop = SAMAVERTED(R%) / 100 * SAMADMISSION(R%) / 100 * (MALNCHPSAM(R%) + MALNCHPSAMINC(R%))
                        '                SAMAVERTEDAsPctOfTotalPop = SAMAVERTEDAsPctOfTotalPop - (SAMAVERTED(R%) - DiffAverted) / 100 * (SAMADMISSION(R%) - DiffAdmission) / 100 * (MALNCHPSAM(R%) + MALNCHPSAMINC(R%))
                        '                'jrs 2016/02/04 now assuming SAM already has endogenous averted, so we want to find Pre-Endog Aveted SAM by dividing by (1-Adm/100*Avert/100)
                        '                If Not (SAMADMISSION(R%) - DiffAdmission = 100 AndAlso SAMAVERTED(R%) - DiffAverted = 100) Then
                        '                    SAMAVERTEDAsPctOfTotalPop = SAMAVERTEDAsPctOfTotalPop / (1 - (SAMADMISSION(R%) - DiffAdmission) / 100 * (SAMAVERTED(R%) - DiffAverted) / 100)
                        '                End If
                        '                SAMAVERTEDAsPctOfTotalPop2 = SAMAVERTEDAsPctOfTotalPop
                        '                If MALNCHPSAM(R%) + MALNCHPSAMINC(R%) <> 0 Then
                        '                    SAMAVERTEDAsPctOfTotalPop = SAMAVERTEDAsPctOfTotalPop * MALNCHPSAM(R%) / (MALNCHPSAM(R%) + MALNCHPSAMINC(R%))
                        '                    SAMAVERTEDAsPctOfTotalPop2 = SAMAVERTEDAsPctOfTotalPop2 * MALNCHPSAMINC(R%) / (MALNCHPSAM(R%) + MALNCHPSAMINC(R%))
                        '                End If
                        '
                        '                SAMAVERTEDAsPctOfTotalPop = Amin(SAMAVERTEDAsPctOfTotalPop, MALNCHPSAM(R%))
                        '                MALNCHPSAM(R%) = (MALNCHPSAM(R%) - SAMAVERTEDAsPctOfTotalPop)
                        SAMAVERTEDAsPctOfTotalPop = MALNCHPSAM(R%)
                        MALNCHPSAM(R%) = (1 - SAMADMISSION(R%) / 100 * SAMAVERTED(R%) / 100) * (MALNCHPSAM(R%) / (1 - (SAMADMISSION(R%) - DiffAdmission) / 100 * (SAMAVERTED(R%) - DiffAverted) / 100))
                        SAMAVERTEDAsPctOfTotalPop = SAMAVERTEDAsPctOfTotalPop - MALNCHPSAM(R%)

                        'SAMAVERTEDAsPctOfTotalPop2 = Amin(SAMAVERTEDAsPctOfTotalPop2, MALNCHPSAMINC(R%))
                        'MALNCHPSAMINC(R%) = (MALNCHPSAMINC(R%) - SAMAVERTEDAsPctOfTotalPop2)
                        'jrs 2016/05/12 added switch to control if sam averted stay malnurished or go fully healthy (default)
                        'jrs 2017/02/17 If samtomalnchpsw = 0 Then
                        If samtomalnchpsw < 1 Then
                            'jrs 2016/01/22
                            'jrs 2017/02/17 MALNCHP(R%) = AMAX(0, Amin(MALNCHP(R%) - SAMAVERTEDAsPctOfTotalPop, 100))
                            MALNCHP(R%) = AMAX(0, Amin(MALNCHP(R%) - (SAMAVERTEDAsPctOfTotalPop * (1 - samtomalnchpsw)), 100))
                        End If
                        MALNCHPSAM(R%) = CSng(AMAX(0, Amin(MALNCHPSAM(R%), 0.91 * MALNCHP(R%)))) 'I'm leavign at least 9% room for non-severe malnutrition (that's the min level I saw from Data, Eq. Guinea)
                    End If

                    'jrs 2016/01/22
                    'SAMADMISSIONTOTCOST(R%) = MALNCHPSAM(R%) / 100 * SAMADMISSION(R%) / 100 * POP0TO5(R%) * 1000000 * samadmissioncost
                    'jrs 2016/01/25 SAMADMISSIONTOTCOST(R%) = (MALNCHPSAM(R%) + SAMAVERTEDAsPctOfTotalPop) * SAMADMISSION(R%) * POP0TO5(R%) * 100 * samadmissioncost(R%)
                    'SAMADMISSIONTOTCOST(R%) = (MALNCHPSAM(R%) + SAMAVERTEDAsPctOfTotalPop) * SAMADMISSION(R%) * POP0TO5(R%) / 10000 * samadmissioncost(R%)
                    'jrs 2015/11/27 ********************** end **********************************

                    'If RegName$(R%) = "Netherlands" Then Stop

                    'BBH 2013/01/21  Additive logic below does look superior to multiplicative one above
                    '       Would avoid some of the instability of multipliers when divisor near zero
                    '   Switch over to code below (needs some debugging) after fix aquaculture and meat
                    '           so that Belgium does not have surge of malnutrition
                    '            Base = MalnChPI(R%) + (malncomp - malncompi)
                    '            Target = malncompi
                    '            malnbase = ConvergeOverTime(Base, Target, malnconv)
                    '                'Base, Target, Years
                    '            MALNCHP(R%) = AMAX(0, Amin(malnbase, 100))
                Next R%
            End If

            Dim ChildPop As Double

            For R% = 1 To NR%
                ChildPop = agedst(1, R%, 1) + agedst(1, R%, 2) '+ agedst(2, R%, 1)  + agedst(2, R%, 2)
                'Children defined as <5 by WB, others; cohort, region, gender is sequence
                MALNCHIL(R%) = CSng(ChildPop * MALNCHP(R%) / 100)
            Next R%

            '   All Years 2: Malnourished Population
            '   -------------------------------------

            'jrs 2020/12/10
            If malnpoppsw = 0 Then
                Dim malnpopcomp, malnpopcompi, malnpopmul, malnpopbase As Single

                For R% = 1 To NR%
                    If IY% = 1 Then
                        malnpoppi(R%) = MALNPOPP(R%)
                        'mti 03/10/2011 -start
                        smalnpopmul(R%) = 1
                        'mti 03/10/2011 - end
                        'jrs 2026/05/26
                        MALNPOP(R%) = MALNPOPP(R%) * POP(R%) / 100
                    Else
                        'sgh 2016/01/06 - START
                        'Use population equation rather than underweight children
                        '            Call XYTABL("Calories per capita (2009) versus Malnourished Children (2011) LN", _
                        '                CLPC(R%, 4), malnpopcomp)
                        '            Call XYTABL("Calories per capita (2009) versus Malnourished Children (2011) LN", _
                        '                CLPCi(R%), malnpopcompi)
                        'This is just replacing equation with OLD undernourished population equation
                        '            Call XYTABL("Calories per capita (2007) versus Malnourished Population Percent", _
                        '                CLPC(R%, 4), malnpopcomp)
                        '            Call XYTABL("Calories per capita (2007) versus Malnourished Population Percent", _
                        '                CLPCi(R%), malnpopcompi)
                        'This is the new (2015/01/08) undernourished vs. calories per capita per day equation
                        '            Call XYTABL("MalnPop%WB(MOSTRECENT) (Linear) versus Calories per capita (total) (Log)", _
                        '                CLPC(R%, 4), malnpopcomp)
                        '            Call XYTABL("MalnPop%WB(MOSTRECENT) (Linear) versus Calories per capita (total) (Log)", _
                        '                CLPCi(R%), malnpopcompi)
                        'If RegName$(R%) = "India" Then Stop
                        'jrs 2024/01/29 Call AnalFunc("MALNPOPP using HHINC and CLPC", New Single() {CLPC(R%, 4), HHINC(R%, 1) / labsups(R%)},
                        Call AnalFunc("MALNPOPP using HHINC and CLPC", New Single() {CLPC(R%, 4), HHINCDIS(R%, 1) / labsups(R%)},
                        malnpopcomp, New String() {"CLCP (log)", "HHINC (unskilled) / LABSUP (unskilled)"}, context)
                        Call AnalFunc("MALNPOPP using HHINC and CLPC", New Single() {CLPCi(R%), hhinci(R%) / labsupi(R%)},
                        malnpopcompi, New String() {"CLCP (log)", "HHINC (unskilled) / LABSUP (unskilled)"}, context)
                        malnpopcompi = CSng(Exp(malnpopcompi))
                        malnpopcomp = CSng(Exp(malnpopcomp))
                        'malnpopcomp = ConvergeOverTime(malnpopcomp, 5, 15)
                        'If RegName$(R%) = "Central AfR" Then Stop
                        'sgh 2016/01/06 - END


                        'If RegName$(R%) = "Kazakhstan" Then Stop

                        If malnpopcomp <= 0 AndAlso malnpopcompi <= 0 Then
                            malnpopmul = 1
                        ElseIf malnpopcomp <= 0 AndAlso malnpopcompi > 0 Then
                            'mti March 10, 2011
                            malnpopmul = 0
                            '                malnpopmul = 1
                        ElseIf malnpopcompi = 0 Then 'jrs 2009/12/17
                            malnpopmul = malnpopcomp
                        Else
                            'jrs 2009/12/17 malnpopmul = AMAX(0, malnpopcomp) / AMAX(1, MalnPopCompI)
                            malnpopmul = malnpopcomp / malnpopcompi 'potentially unstable branch, see below
                        End If

                        'If initial computation of undernutriton is very near 0, the multiplier can be
                        '   unstable with small future changes; bound changes in multiplier to control
                        'mti 03/10/2011 -start
                        MaxChg = 0.05
                        malnpopmul = AMAX(smalnpopmul(R%) * (1 - MaxChg), Amin(smalnpopmul(R%) *
                        (1 + MaxChg), malnpopmul))
                        smalnpopmul(R%) = malnpopmul
                        'mti 03/10/2011 - end

                        '           Computation for Adults; adjust initial value by multipler but converge to computed value
                        malnpopbase = (malnpoppi(R%) * malnpopmul * (malnconv - TimeFAc) +
                        malnpopcomp * TimeFAc) / malnconv
                        If MALNPOPP(R%) < 0.001 AndAlso MALNCHP(R%) > 0.01 Then
                            MALNPOPP(R%) = CSng(0.1 * MALNCHP(R%))
                        End If

                        'jrs 2017/11/10
                        'Check that Malnutrition in total Population leaves room for non-malnurished children under 5
                        'For example in Burundin we were reaching a 100% in MALNPOP while MALNCHP was NOT 100%
                        Dim WellNourishedChildren As Single
                        WellNourishedChildren = CSng(agedst(1, R%, 1) + agedst(2, R%, 1) - MALNCHIL(R%))
                        Dim MaxMALNPOP As Single
                        If POP(R%) > 0 Then
                            MaxMALNPOP = (1 - WellNourishedChildren / POP(R%)) * 100
                        Else
                            MaxMALNPOP = 100
                        End If

                        'jrs 2017/11/10 MALNPOPP(R%) = AMAX(0, Amin(malnpopbase, 100))
                        MALNPOPP(R%) = AMAX(0, Amin(malnpopbase, MaxMALNPOP))
                        'jrs 2026/05/26
                        MALNPOP(R%) = MALNPOPP(R%) * POP(R%) / 100

                        'If RegName$(R%) = "Kazakhstan" Then Stop

                        '   Switch over to code below (needs some debugging) after fix aquaculture and meat
                        '           so that Belgium does not have surge of malnutrition

                        '            Base = MalnPopPI(R%) + (MalnPopComp - MalnPopCompI)
                        '            Target = MalnPopCompI
                        '            MALNPOPP(R%) = ConvergeOverTime(Base, Target, malnconv)
                        '                'Base, Target, Years
                        '            MALNPOPP(R%) = AMAX(0, Amin(MALNPOPP, 100))

                    End If
                Next R%

            Else
                'jrs 2021/02/04 *** begin ***
                'Forecast Coefficient of Variation and MDER
                Dim Est As Single
                If IY% = 1 Then
                    'Shift factors
                    ReDim CLPCCVShift(NR), CLPCMDERShift(NR)
                    'jrs 2021/08/04
                    ReDim SCLPCCV(NR)

                    For R% = 1 To NR
                        'jrs 2021/04/07 Call AnalFunc("Coeff. of Var. (Linear) versus GDPPCP, GINIDOM, INFRAELECACC, POLITY",
                        'New Single() {GDPPCP(R%), GINIDOM(R%), INFRAELECACC(R%, 3), DEMOCPOLITY(R%)}, Est,
                        'New String() {"GDPPCP", "GINIDOM", "INFRAELECACC", "DEMOCPOLITY"}, context)

                        'JRS 2021/04/15
                        'jrs 2021/08/04 If clpccvr(R%) = 0 Then
                        'jrs 2022/01/10
                        Select Case clpccvr(R%)
                            Case 0
                                Call AnalFunc("Coeff. of Var. (Linear) versus GDPPCP, GINIDOM, LABPARR(Female), Youth Dep.",
                                New Single() {GDPPCP(R%), GINIDOM(R%), LABPARR(R%, 2), 100 * POPLE15(R%) / POP(R%)}, Est,
                                New String() {"GDPPCP", "GINIDOM", "LABPARR(Female)", "Youth Dep."}, context)
                            Case 2 'Median
                                'jrs 2024/06/20 XYTABL("Coef. of Variance versus GDP per Capita at PPP (Power) (Median)", GDPPCP(R%), Est, context)
                                XYTABL(TFNameCoefVarMedian, GDPPCP(R%), Est, context)
                            Case 3 'Optimistic
                                'jrs 2024/06/20 XYTABL("Coef. of Variance versus GDP per Capita at PPP (Power) (Optimistic)", GDPPCP(R%), Est, context)
                                XYTABL(TFNameCoefVarOptimistic, GDPPCP(R%), Est, context)
                            Case 4 'Pessimistic
                                'jrs 2024/06/20 XYTABL("Coef. of Variance versus GDP per Capita at PPP (Power) (Pessimistic)", GDPPCP(R%), Est, context)
                                XYTABL(TFNameCoefVarPessimistic, GDPPCP(R%), Est, context)
                        End Select

                        If Est <> 0 Then
                            CLPCCVShift(R%) = CLPCCV(R%) / Est
                        Else
                            CLPCCVShift(R%) = 1
                        End If
                        'jrs 2021/08/04 Else
                        'jrs 2021/08/04 CLPCCVShift(R%) = CLPCCV(R%)
                        'jrs 2021/08/04 End If
                        'jrs 2021/08/04
                        SCLPCCV(R%) = CLPCCV(R%)

                        Call XYTABL("CLPCMDER (Linear) versus POPMEDAGE (X^2)", POPMEDAGE(R%), Est, context)
                        If Est <> 0 Then
                            CLPCMDERShift(R%) = CLPCMDER(R%) / Est
                        Else
                            CLPCMDERShift(R%) = 1
                        End If
                    Next R%
                Else
                    'Forecast
                    For R% = 1 To NR
                        'jrs 2021/04/07 Call AnalFunc("Coeff. of Var. (Linear) versus GDPPCP, GINIDOM, INFRAELECACC, POLITY",
                        'New Single() {GDPPCP(R%), sGINIDOM(R%), sINFRAELECACC(R%, 3), SDEMOCPOLITY(R%)}, Est,
                        'New String() {"GDPPCP", "GINIDOM", "INFRAELECACC", "DEMOCPOLITY"}, context)

                        'jrs 2021/04/15
                        'jrs 2022/01/10 If clpccvr(R%) = 0 Then
                        Select Case clpccvr(R%)
                            Case 0
                                Call AnalFunc("Coeff. of Var. (Linear) versus GDPPCP, GINIDOM, LABPARR(Female), Youth Dep.",
                                New Single() {GDPPCP(R%), sGINIDOM(R%), LABPARR(R%, 2), 100 * POPLE15(R%) / POP(R%)}, Est,
                                New String() {"GDPPCP", "GINIDOM", "LABPARR(Female)", "Youth Dep."}, context)

                                CLPCCV(R%) = CLPCCVShift(R%) * Est
                            'jrs 2021/03/09
                            'jrs 2022/01/10 CLPCCV(R%) = CLPCCV(R%) * clpccvm(R%)
                'Else jrs 2022/01/10
                            Case 1 'Constant
                                'jrs 2021/08/04 CLPCCV(R%) = CLPCCVShift(R%)
                                CLPCCV(R%) = SCLPCCV(R%)
                                'jrs 2021/06/07
                                'jrs 2022/01/10 CLPCCV(R%) = CLPCCV(R%) * clpccvm(R%)
                            Case 2 'Median
                                'jrs 2024/06/20 XYTABL("Coef. of Variance versus GDP per Capita at PPP (Power) (Median)", GDPPCP(R%), Est, context)
                                XYTABL(TFNameCoefVarMedian, GDPPCP(R%), Est, context)
                                CLPCCV(R%) = CLPCCVShift(R%) * Est
                            Case 3 'Optimistic
                                'jrs 2024/06/20 XYTABL("Coef. of Variance versus GDP per Capita at PPP (Power) (Optimistic)", GDPPCP(R%), Est, context)
                                XYTABL(TFNameCoefVarOptimistic, GDPPCP(R%), Est, context)
                                CLPCCV(R%) = CLPCCVShift(R%) * Est
                            Case 4 'Pessimistic
                                'jrs 2024/06/20 XYTABL("Coef. of Variance versus GDP per Capita at PPP (Power) (Pessimistic)", GDPPCP(R%), Est, context)
                                XYTABL(TFNameCoefVarPessimistic, GDPPCP(R%), Est, context)
                                CLPCCV(R%) = CLPCCVShift(R%) * Est
                                'End If jrs 2022/01/10
                        End Select
                        CLPCCV(R%) = CLPCCV(R%) * clpccvm(R%)

                        'jrs 2021/08/04
                        SCLPCCV(R%) = CLPCCV(R%)

                        Call XYTABL("CLPCMDER (Linear) versus POPMEDAGE (X^2)", POPMEDAGE(R%), Est, context)
                        CLPCMDER(R%) = CLPCMDERShift(R%) * Est
                        'jrs 2021/03/09
                        CLPCMDER(R%) = CLPCMDER(R%) * clpcmderm(R%)

                    Next R%
                End If
                'jrs 2021/02/04 *** end
                'jrs 2020/12/10 Distribution Implementation
                Dim Sigma, Mu, Zz, pct As Single
                For R% = 1 To NR%
                    'jrs 2023/03/13
                    If CLPCCV(R%) = 0 Or CLPCMDER(R%) = 0 Then
                        pct = MALNPOPP(R%)
                    Else
                        Sigma = CSng(Log((CLPCCV(R%) ^ 2) + 1) ^ 0.5)
                        Mu = CSng(Log(CLPC(R%, 4)) - Sigma ^ 2 / 2)
                        Zz = CSng((Log(CLPCMDER(R%)) - Mu) / Sigma)
                        pct = CSng(CalcNormalCDF(Zz) * 100)
                    End If

                    If IY% = 1 Then
                        'malnpoppi(R%) = MALNPOPP(R%) / pct
                        malnpoppi(R%) = MALNPOPP(R%) - pct
                        'jrs 2026/05/26
                        MALNPOP(R%) = MALNPOPP(R%) * POP(R%) / 100
                    Else
                        'jrs 2020/12/15 added convergence
                        'MALNPOPP(R%) = pct * ConvergeOverTime(malnpoppi(R%), 1, 86)
                        MALNPOPP(R%) = AMAX(pct + ConvergeOverTime(malnpoppi(R%), 0, 30), 0)
                        'jrs 2026/05/26
                        MALNPOP(R%) = MALNPOPP(R%) * POP(R%) / 100
                    End If
                Next R%
            End If

        End Sub

        Public Sub ifsPOP3AllYears(context As IForecastContext)
            'bbh 2010/10/20 start
            'jrs 2009/09/30
            'Moved down to compute after Health module calculations
            '   All Years 3:  Urban Population Calculation
            '   ------------------------------------------
            'If IY = 38 Then Stop
            Dim POPURBANPOR, UrbanShiftGr, POPURBANTAR, PopUrbanFunc, POPUrbGrAdj As Single
            Dim poprdiff, popdiff, PopUrbGrBase, UrbInc, UrbIncM, TargetUrbIncM As Single
            Dim PopGrReduced As Single
            If IY% > 1 Then
                For R% = 1 To NR%
                    'bbh 2011/1/31, entire R% loop start here
                    'Until here, do not have good POPR, POPRI base
                    '   If need urban population earlier, better to lag it
                    '   Do not use initial calculation of POPR, too rough
                    'Step 1:  Compute Long-term Function-based Target
                    If context.HistoricMode = True Then
                        Call XYTABL("GDP/Capita (PPP) Versus Urbanization Percentage (1960)",
                            sGDPPCP(R%) * (1 + igdpr(R%)), POPURBANPOR, context)
                    Else
                        'jrs 2024/07/04 Call XYTABL("GDP/Capita (PPP) Versus Urbanization Percentage (1998)",
                        'sGDPPCP(R%) * (1 + igdpr(R%)), POPURBANPOR, context) 'returns portion; not percent
                        Call XYTABL(TFNameUrbPct, sGDPPCP(R%) * (1 + igdpr(R%)), POPURBANPOR, context) 'returns portion; not percent
                    End If
                    POPURBANPOR = CSng(Amin(0.9, POPURBANPOR + POPURBShift(R%)))
                    'If RegName$(R%) = "Timor-Leste" Then Stop
                    UrbanShiftGr = ConvergeOverTime(0.004, 0.002, 100)
                    POPURBANTAR = POPURBANPOR * (1 + UrbanShiftGr * IY%) '.0025 should go to 0 over time
                    If POPURBANTAR - POPURBANPOR > 0.1 * (0.9 - POPURBANPOR) Then
                        POPURBANPOR = CSng(POPURBANPOR + 0.1 * (0.9 - POPURBANPOR))
                    Else
                        POPURBANPOR = POPURBANTAR
                    End If
                    PopUrbanFunc = CSng(POP(R%) * Amin(0.9, POPURBANPOR))
                    'Step 2: Adjust urban growth with Pop Growth
                    'mti August 07, 2012 - start
                    If IY = 2 Then
                        POPUrbGrAdj = 0
                    Else 'Note that POPUrbanCalc is not available yet in year 2 (code not called in year 1);
                        'So cannot adjust until year 3
                        'mti August 07, 2012 - end
                        'mti 2018/11/27 start
                        'poprdiff = AMAX(0, POPR(R%)) - AMAX(0, POPRI(R%))

                        'jrs 2026/08/12 poprdiff = POPR(R%) - POPRI(R%) - (migrater(R%) - migrateRI(R%))
                        poprdiff = POPR(R%) - POPRI(R%) - (migrateinr(R%) - migrateoutr(R%) - migrateinRI(R%) + migrateoutRI(R%))


                        If PopUrbanFunc > PopUrbanCalc(R%) Then 'if urbanization low, do not decrease much (India)

                            '                    POPUrbGrAdj = 1 * (POPR(R%) - POPRI(R%)) / 100
                            '                    If PopUrbGRI(R%) < POPRI(R%) Then 'protect countries initially deurbanizing
                            '                        POPUrbGrAdj = 0.25 * (POPR(R%) - POPRI(R%)) / 100  'org
                            '                    End If
                            POPUrbGrAdj = 1 * (poprdiff) / 100
                            If PopUrbGRI(R%) < POPRI(R%) Then 'protect countries initially deurbanizing
                                POPUrbGrAdj = CSng(0.25 * (popdiff) / 100) 'org
                            End If

                        Else 'if urbanization high, decrease with popr (Nigeria)
                            '                    POPUrbGrAdj = 1.5 * (POPR(R%) - POPRI(R%)) / 100
                            '                    If PopUrbGRI(R%) < POPRI(R%) Then 'protect countries initially deurbanizing
                            '                        POPUrbGrAdj = 0.5 * (POPR(R%) - POPRI(R%)) / 100
                            '                    End If

                            POPUrbGrAdj = CSng(1.5 * poprdiff / 100)
                            If PopUrbGRI(R%) < POPRI(R%) Then 'protect countries initially deurbanizing
                                POPUrbGrAdj = CSng(0.5 * poprdiff / 100)
                            End If


                        End If
                        'mti 2018/11/27 End
                        'mti August 07, 2012 - start
                    End If
                    'mti August 07, 2012 - end
                    'Reln between POPR and POPURBGR has about 1.5 slope
                    PopUrbGrBase = AMAX(0, PopUrbGRI(R%) + POPUrbGrAdj)
                    '           Slow down the urban increment above population growth rate as approach 95% urban

                    UrbInc = PopUrbGrBase - POPR(R%) / 100

                    TargetUrbIncM = CSng(AMAX(0, 0.95 - SPOPURBAN(R%) / POP(R%)))
                    'mti 2018/11/27 Start
                    'UrbIncM = ConvergeOverTime(1, TargetUrbIncM, 40) 'Base,Target,Years
                    UrbIncM = ConvergeOverTime(1, TargetUrbIncM, 20) 'Base,Target,Years
                    'mti 2018/11/27 End
                    UrbInc = UrbInc * UrbIncM
                    If UrbInc < 0 Then UrbInc = ConvergeOverTime(UrbInc, 0, 20)
                    'Assume de-urbanization is temporary phenomenon


                    'mti 2018/11/27 Start
                    UrbInc = CSng(Amin(0.02, UrbInc))
                    'mti 2018/11/27 End

                    PopGrReduced = POPR(R%) / 100 + UrbInc


                    '            PopGrReduced = ConvergeOverTimeSlowing(PopUrbGrBase, POPR(R%) / 100, 20, 66)
                    'ConvergeOverTimeSlowing(Base, Target, YearsToConverge, MidwayPercShare)
                    PopUrbanCalc(R%) = SPOPURBAN(R%) * (1 + PopGrReduced)


                    'If RegName$(R%) = "Nigeria" AndAlso IY% >= 25 Then Stop


                    'Step 3:  Converge to Long-term Target, mostly upward
                    'Indian urbanization way behind forecast; otherwise pulls too fast
                    '            If PopUrbanFunc > PopUrbanCalc Then 'Upward convergence, more urban
                    '                POPURBAN(R%) = ConvergeOverTime(PopUrbanCalc, PopUrbanFunc, 250)
                    '                POPURBAN(R%) = PopUrbanCalc * 0.98 + PopUrbanFunc * 0.02
                    ''                POPURBAN(R%) = PopUrbanCalc * 0.999 + PopUrbanFunc * 0.001
                    '            Else 'Downward convergence improbable, very slow
                    '                POPURBAN(R%) = ConvergeOverTime(PopUrbanCalc, PopUrbanFunc, 2000)
                    '                POPURBAN(R%) = PopUrbanCalc * 0.999 + PopUrbanFunc * 0.001
                    '            End If
                    POPURBAN(R%) = PopUrbanCalc(R%) 'Removed convergence because of initial transients;
                    'jrs 2026/08/12
                    If spopurbanm(R%) > 0 Then
                        'This avoid cumulative effect by applying only the year to year change of the multiplier
                        POPURBAN(R%) = POPURBAN(R%) * popurbanm(R%) / spopurbanm(R%)
                    Else
                        POPURBAN(R%) = POPURBAN(R%) * popurbanm(R%)
                    End If
                    spopurbanm(R%) = popurbanm(R%)
                    'If RegName$(R%) = "Afghanistan" AndAlso IY < 4 Then Stop

                    'Overall design is now more extapolative with reaction to change in POPR
                    'If put back convergence, phase it in over time, not immediately.

                    'Step 4:  Complete computations
                    POPURBAN(R%) = Amin(POP(R%) * POPURBANRMAX(R%), AMAX(0.01 * POP(R%), POPURBAN(R%)))
                    'jrs 2014/08/28 Exogenous Models
                    If ifsexomodelid > 0 Then
                        ReplaceVarWithExogenousForecastxReg("POPURBAN", "", "", CStr(FirstYear + IY - 1), R%, context)
                    End If
                    POPURBGR(R%) = (POPURBAN(R%) / SPOPURBAN(R%) - 1) * 100 'bbh 2012/03/26

                    'mti April 05, 2012 - start
                    POPRURAL(R%) = POP(R%) - POPURBAN(R%)
                    'mti April 05, 2012 - end

                    'mti February 2011 Infarstructure -start
                    sPopUrban1(R%) = SPOPURBAN(R%)
                    'mti February 2011 Infarstructure end

                    SPOPURBAN(R%) = POPURBAN(R%)
                    'bbh 2011/1/31, entire R% loop end here

                    'mti 2022/06/21 start
                    sPOPRURAL(R%) = POPRURAL(R%)
                    'mti 2022/06/21 end

                Next R
            End If

            'bbh 2010/10/20 end
            'mti WatSafe->MalnchpSH May 2006
            '   All Years 3:  Child malnutrition according to Smith and Haddad
            '   ------------------------------------------------------------
            'mti MalnchpSH revise June 2006 start
            Dim lifeexprat As Single
            For R% = 1 To NR%  'Targets on these indicators may increase
                'percentage of people with access to safewater
                'If RegName(R%) = "India" Then Stop
                If IY% > 1 Then
                    lifeexprat = LIFEXP(R%, Female) / AMAX(1, LIFEXP(R%, Male))
                    'Kilocal = AMAX(500, CLAVAL(R%) / POP(R%))
                    'jrs 2009/02/19 MALNCHPSH(R%) = malnchpi(R%) - 25.24 * _
                    '(Log(CLPC(R%, 4)) - Log(sCLPC(R%))) - 71.76 * (lifeexprat - SLIFEXPRat(R%)) _
                    '- 0.22 * (EDSECENRG(R%, Female) - SEdSecEnrGFem(R%)) - 0.08 * (WATSAFE(R%) - sWATSAFE(R%))

                    'mti 2022/08/17 start
                    'mti 2022/10/20 start 'watsafefwdsw
                    'If watsafenewsw = 1 Then 'new 5 category model
                    If watsafenewsw = 1 And watsafefwdsw = 1 Then 'new 5 category model
                        'mti 2022/10/20 end

                        Dim WaterUnSafe = WATERHH(R%, Surface, National) + WATERHH(R%, Unimproved, National)
                        MALNCHPSH(R%) = CSng(malnchpi(R%) - 25.24 *
                        (Log(CLPC(R%, 4)) - Log(sCLPC(R%))) - 71.76 * (lifeexprat - SLIFEXPRat(R%)) _
                        - 0.22 * (EDSECENRG(R%, Female) - SEdSecEnrGFem(R%)) - 0.08 * (100 - WaterUnSafe - WATSAFEI(R%)))

                    Else

                        MALNCHPSH(R%) = CSng(malnchpi(R%) - 25.24 *
                        (Log(CLPC(R%, 4)) - Log(sCLPC(R%))) - 71.76 * (lifeexprat - SLIFEXPRat(R%)) _
                        - 0.22 * (EDSECENRG(R%, Female) - SEdSecEnrGFem(R%)) - 0.08 * (100 - WATSAFE(R%, 1) - WATSAFEI(R%)))

                    End If

                    'mti 2022/08/17 End

                    '            MALNCHPSH(R%) = 100 - 25.24 * Log(CLPC(R%, 4) - sCLPC(R%)) - 71.76 * (lifeexprat - SLIFEXPRat(R%)) _
                    '- 0.22 * (EDSECENRG(R%, Female) - SEdSecEnrGFem(R%)) - 0.08 * (WATSAFE(R%) - SWATSAFE(R%))
                    MALNCHPSH(R%) = AMAX(0, MALNCHPSH(R%))
                Else 'first year
                    MALNCHPSH(R%) = MALNCHP(R%)
                    'mti MalnchpSH Delta fix June 2006 -start
                    SLIFEXPRat(R%) = LIFEXP(R%, Female) / AMAX(1, LIFEXP(R%, Male))
                    SEdSecEnrGFem(R%) = EDSECENRG(R%, Female)
                    'jrs 2009/02/18 sWATSAFE(R%) = WATSAFE(R%)
                    'mti 2022/08/17 start
                    'mti 2022/10/20 start 'watsafefwdsw
                    'If watsafenewsw = 1 Then 'new 5 category model
                    If watsafenewsw = 1 And watsafefwdsw = 1 Then 'new 5 category model
                        'mti 2022/10/20 end
                        WATSAFEI(R%) = 100 - WATERHH(R%, Surface, National) - WATERHH(R%, Unimproved, National)
                    Else
                        WATSAFEI(R%) = 100 - WATSAFE(R%, 1)
                    End If
                    'mti 2022/08/17 End
                    sCLPC(R%) = CLPC(R%, 4)

                End If
            Next R

            '
            '   All Years 3:  Set PQLI and HDIPRE2010 Targets
            '   --------------------------------------
            Dim infmormin, LifExpMaxHDI, GDPPCPMAX, PQLILife, PQLIInfMor As Single
            infmormin = 200
            LifExpMaxHDI = 0
            GDPPCPMAX = 20 'Non-zero so Log works

            'mti March 2011 -start
            'mti April 2011 -start
            'New HDIPRE2010 (UN HDR 2010) - Education sub-component maxima determined by UN HDR technical Notes
            'mti August 2017 start
            'New HDR 2016
            '    EdYrs25Max = 13.2 '5 'set at low values that will change in the loop below
            '    EdYrsSLEMax = 20.6 '5 'set at low values that will change in the loop below
            '    LifExpMaxHDINew = 73.5
            '
            '    'mti July 18, 2011 - start
            '    'LifExpMinHDINew = 20
            '    LifExpMinHDINew = Amin(20, lifexpmin)
            '    'mti July 18, 2011 - end
            '
            'mti 2017/12/27 start
            '     Log70000 = Log(70000)
            ''    'mti April 2011 - end
            '
            '
            '    'New HDIPRE2010 min/max and formulation per UNDP HDR 2016
            '    EdYrs25Max = 15         'years of mean
            '    EdYrsSLEMax = 18        'years of expected years of schooling
            '    LifExpMaxHDINew = 85    'years
            '    LifExpMinHDINew = 20    'years
            '    GDPPCPHDINewMinLog1 = Log(100)
            '    GDPPCPHDINewDenom1 = (Log(75000) - Log(100))  'new max and min are 75,000 and 100 dollars 2011 PPP

            For gndr% = 1 To NGender%
                GDPPCPHDINewMinLog(gndr%) = CSng(Log(hdigdppcminun(gndr%) * 1000))
                GDPPCPHDINewDenom(gndr%) = CSng(Log(hdigdppcmaxun(gndr%) * 1000) - Log(hdigdppcminun(gndr%) * 1000))
            Next gndr%

            'mti August 2017 End
            'mti 2017/12/27 End

            For R% = 1 To NR%  'Targets on these indicators may increase

                infmormin = Amin(INFMOR(R%), infmormin)
                'mti LifeExp/ gender May 2006
                LifExpMaxHDI = AMAX(LIFEXP(R%, 3), LifExpMaxHDI)
                '        lifexpmax = AMAX(LIFEXP(R%), lifexpmax)
                GDPPCPMAX = AMAX(GDPPCP(R%), GDPPCPMAX)


                '        EdYrs25Max = AMAX(EDYRSAG25(R%, 3), EdYrs25Max)
                '        EdYrsSLEMax = AMAX(EDYRSSLE(R%), EdYrsSLEMax)

            Next R%


            '    EdIndHDINewMax = 0.0001

            'mti August 2017 Start
            Dim EdYrs25Ind, EdYrsSLEInd As Single
            For R% = 1 To NR%  'Targets on these indicators may increase
                For gndr% = 1 To NGender%
                    'mti 2017/12/27 start
                    '            EdYrs25Ind = EDYRSAG25(R%, gndr%) / EdYrs25Max
                    '            EdYrsSLEInd = EDYRSSLE(R%, gndr%) / EdYrsSLEMax
                    '            EdIndHDINew(R%, gndr%) = (EdYrs25Ind + EdYrsSLEInd) / 2
                    '            'Talk to BBH about bounds - AMAX(GDPPCP(R%), 0.10001) * 1000)
                    '            LifeExpIndHDINew(R%, gndr%) = AMAX(0.0001, (LIFEXP(R%, gndr%) - LifExpMinHDINew)) / (LifExpMaxHDINew - LifExpMinHDINew)
                    EdYrs25Ind = (EDYRSAG25(R%, gndr%) - hdiedyrsminun(gndr%)) / (hdiedyrsmaxun(gndr%) - hdiedyrsminun(gndr%))
                    EdYrsSLEInd = (EDYRSSLE(R%, gndr%) - hdiedsleminun(gndr%)) / (hdiedslemaxun(gndr%) - hdiedsleminun(gndr%))
                    EdIndHDINew(R%, gndr%) = (EdYrs25Ind + EdYrsSLEInd) / 2
                    'Talk to BBH about bounds - AMAX(GDPPCP(R%), 0.10001) * 1000)
                    LifeExpIndHDINew(R%, gndr%) = AMAX(0.0001, (LIFEXP(R%, gndr%) - hdilifeminun(gndr%))) / (hdilifemaxun(gndr%) - hdilifeminun(gndr%))

                    'mti 2017/12/27 End
                Next gndr%
            Next R%
            'mti August 2017 End

            'New HDIPRE2010 (UN HDR 2010) - Education component maximum determined by UN HDR technical Notes
            Dim EdIndHDINewMax As Single
            EdIndHDINewMax = 0.951
            'mti March 2011 - end'


            '   All Years 3:  Initiate
            '   --------------------------------
            WPQLI = 0!
            WLIT = 0
            'jrs 2020/08/31 begin
            'move to ifsPop3 after calculation of PQLI
            'jrs 2020/08/31 
            Dim PNO As Single, PSO As Single, qlno As Single, qlso As Single
            PNO = 0!
            PSO = 0!
            qlno = 0!
            qlso = 0!
            'jrs 2020/08/31 end

            '
            '   All Years 3:  Literacy, Physical Quality of Life
            '   ------------------------------------------------
            Dim TEMP1, temp2, LITM, LitHigh As Single
            For R% = 1 To NR%
                '        EDEX = GDS(R%, Educ) / POP(R%) * 1000!
                '        If (IY% = 1) Then SEDEX(R%) = EDEX
                '        Call XYTABL("Educational Spending/Capita Versus Literacy", EDEX, TEMP1)
                '        Call XYTABL("Educational Spending/Capita Versus Literacy", SEDEX(R%), TEMP2)

                '   BBH 4/6/04 V5_0 Begin
                sedyrsag15(R%) = EDYRSAG15(R%, 3)

                'jrs 2007/06/07 added second dimension to EDYRSAG25I
                If (IY% = 1) Then
                    For gend% = 1 To 3
                        EDYRSAG15I(R%, gend%) = EDYRSAG15(R%, gend%)
                        EDYRSAG25I(R%, gend%) = EDYRSAG25(R%, gend%)
                        EDPRIPERI(R%, gend%) = EDPRIPER(R%, gend%)
                    Next gend%

                End If
                '   BBH 4/6/04 V5_0 End

                If IY% > 1 Then
                    '            Call XYTABL("Educational Years at 15 Versus Literacy Percentage (2000)", EDYRSAG15(R%, 3), TEMP1)
                    '            Call XYTABL("Educational Years at 15 Versus Literacy Percentage (2000)", EDYRSAG15I(R%, 3), temp2)
                    'EdYrs15 function is too flat; requires 14 years to reach universal literacy; 10 for EdYrs25
                    Call XYTABL("Education Adult Primary Percent Versus Literacy  (2000)", EDPRIPER(R%, 3), TEMP1, context)
                    Call XYTABL("Education Adult Primary Percent Versus Literacy  (2000)", EDPRIPERI(R%, 3), temp2, context)
                    If LITI(R%) > temp2 Then
                        If TEMP1 > temp2 Then
                            TEMP1 = temp2 + (TEMP1 - temp2) * temp2 / AMAX(0.01, LITI(R%))
                        End If 'slow down countries way above function, e.g. Burundi
                    End If

                    LITM = AMAX(0.01, TEMP1) / AMAX(0.01, temp2)
                    LIT(R%) = AMAX(0.98 * SLit(R%), LITI(R%) * LITM)
                    LitHigh = ConvergeOverTime(LIT(R%), TEMP1, 100) 'Base, Target, Years
                    If LitHigh > LIT(R%) Then LIT(R%) = LitHigh 'Upward only; must be able to complete literacy transition
                    LIT(R%) = AMAX(0, Amin(LIT(R%), 100))
                    SLit(R%) = LIT(R%)
                End If
                WLIT = WLIT + LIT(R%) * POP(R%)
                'mti LifeExp/ gender May 2006
                LifExpMaxHDI = AMAX(LifExpMaxHDI, lifexpmin + 1)
                PQLILife = (LIFEXP(R%, 3) - lifexpmin) / (LifExpMaxHDI - lifexpmin) * 100
                '        PQLILife = (LIFEXP(R%) - lifexpmin) / (lifexpmax - lifexpmin) * 100
                infmormax = AMAX(infmormax, infmormin + 1)
                PQLIInfMor = (infmormax - INFMOR(R%)) / (infmormax - infmormin) * 100.0!
                PQLI(R%) = (LIT(R%) + PQLILife + PQLIInfMor) / 3.0!
                If PQLI(R%) > 100 Then PQLI(R%) = 100
                If PQLI(R%) < 0 Then PQLI(R%) = 0!
                WPQLI = WPQLI + PQLI(R%) * POP(R%)

                'jrs 2020/08/31 begin
                'move to ifsPop3 after calculation of PQLI
                If GDPPCI(R%) > nsdiv Then
                    PNO = PNO + POP(R%)
                    qlno = qlno + POP(R%) * PQLI(R%)
                Else
                    PSO = PSO + POP(R%)
                    qlso = qlso + POP(R%) * PQLI(R%)
                End If
                'jrs 2020/08/31 end
            Next R%

            '   All Years 3:  Human Development Index
            '   -------------------------------------
            Dim Log100, Log40000, LogGDPPCPMax As Single
            Log100 = CSng(Log(100))
            Log40000 = CSng(Log(40000))
            LogGDPPCPMax = CSng(Log(GDPPCPMAX * 1000))

            '    LogGDPPCPMax = ConvergeOverTime(Log40000, LogGDPPCPMax, 100) 'Floating - phased in
            '    HDILIFEMAXFloat = ConvergeOverTime(85, lifexpmax, 100) 'Floating - phased in
            Dim HDILIFEMAXFloat, LogGDPPCP21, HDILIFEMAX21 As Single
            HDILIFEMAXFloat = LifExpMaxHDI 'Floating - no phase in

            LogGDPPCP21 = CSng(Log(hdigdppcmax * 1000))
            '    LogGDPPCP21 = ConvergeOverTime(Log40000, LogGDPPCP21, 100) '21st - phased in
            'Base, Target, Years
            '    HDILIFEMAX21 = ConvergeOverTime(85, hdilifemaxf, 100) '21st - phased in
            HDILIFEMAX21 = hdilifemaxf '21st - no phase in

            Dim LifeExpInd, LitInd, GDPInd, GDPPCIndHDINew, POPMale, POPFemale As Double
            Dim FemshrPop, FemShRWage, GDPPCIndHDINewFem, GDPPCIndHDINewMale As Double
            For R% = 1 To NR%

                'Debug.Print IY; RegName(R%)
                'If RegName(R%) = "Iceland" Then Stop
                'mti LifeExp/ gender May 2006
                hdilifemax = AMAX(hdilifemax, lifexpmin + 1)

                LifeExpInd = (LIFEXP(R%, 3) - lifexpmin) / (hdilifemax - lifexpmin)
                'jrs 2011/12/02
                LifeExpInd = AMAX(LifeExpInd, 0)
                '        LifeExpInd = (LIFEXP(R%) - lifexpmin) / (hdilifemax - lifexpmin)
                LitInd = LIT(R%) / 100

                GDPInd = CSng((Log(GDPPCP(R%) * 1000) - Log100) / (Log40000 - Log100))

                'mti April 2011 - start
                '-----HDIPRE2010 as per UN HDR 2010 -----------
                'mti Note: New HDIPRE2010 uses 73.5 and 20 years for lifeexp max and min; and $108,211 (PPP 2008) and $163  GNIPC (as opposed to GDPPC) for max and min

                'mti August 2017 Start
                'LifeExpIndHDINew = (LIFEXP(R%, 3) - LifExpMinHDINew) / (LifExpMaxHDINew - LifExpMinHDINew)
                'mti 2017/12/27 Start
                GDPPCIndHDINew = CSng((Log(CDbl(AMAX(GDPPCP(R%), 0.10001) * 1000)) - GDPPCPHDINewMinLog(3)) / GDPPCPHDINewDenom(3))
                'mti 2017/12/27 End
                GDPPCIndHDINew = AMAX(0, GDPPCIndHDINew)
                'EdIndHDINewFinal = EdIndHDINew(R%) / EdIndHDINewMax
                'mti using HDR 2010 Lifeexp bounds give transient? - using existing (85; 25)
                'HDI(R%) = EdIndHDINewFinal ^ (1 / 3) * GDPIndHDINew ^ (1 / 3) * LifeExpInd ^ (1 / 3)
                HDI(R%) = CSng(EdIndHDINew(R%, 3) ^ (1 / 3) * GDPPCIndHDINew ^ (1 / 3) * LifeExpIndHDINew(R%, 3) ^ (1 / 3))

                ' If IY >= 70 AndAlso RegName(R%) = "Somalia" Then Stop
                ' If IY = 6 AndAlso RegName(R%) = "Luxembourg" Then Stop

                'Gender Develoepment Index
                'ttt 2019/10/08  
                POPMale = POPMaleG(R)
                POPMale = AMAX(0.001, POPMale)
                POPFemale = POPFemaleG(R)
                POPFemale = AMAX(0.001, POPFemale)
                'POPMale = 0!
                'For j% = 1 To NCohorts
                '    POPMale = POPMale + agedst(j%, R%, 1)
                'Next j%
                'POPMale = AMAX(0.001, POPMale)

                'POPFemale = 0!
                'For j% = 1 To NCohorts
                '    POPFemale = POPFemale + agedst(j%, R%, 2)
                'Next j%
                'POPFemale = AMAX(0.001, POPFemale)
                'End ttt 2019/10/08

                FemshrPop = POPFemale / (POPMale + POPFemale)
                'MaleShrPOP = POPMale / (POPMale + POPFemale) = 1 - FemshrPop

                'Female share of total wage: EA = economically active
                'Average WageBill per EA person = WageFem * EAShareFem  + WageMale * EAShareMale
                'Female share of WageBill = (WageFem * EAShareFem) / (WageFem * EAShareFem  + WageMale * EAShareMale)
                'Female share of WageBill = ( WageFem / WageMale * EAShareFem) / (WageFem / WageMale * EAShareFem  +  EAShareMale)
                'Female share of WageBill = ( WAGESEXRAT * FemShrLab) / (WAGESEXRAT * FemShrLab  +  (1-FemShrLab))
                FemShRWage = (LABWAGESEXRAT(R%) * FEMSHRLAB(R%) / 100) / (LABWAGESEXRAT(R%) * FEMSHRLAB(R%) / 100 + (1 - FEMSHRLAB(R%) / 100))

                'Male share of WageBill = 1 - Female share of WageBill


                'mti September 2017 Start
                'Female earned income per capita = Total income * female share of wage / Female population
                'Female earned income per capita = GDPPCP * Population * FemShRWage / Female population
                'Female earned income per capita = GDPPCP * FemShRWage / (Female population/Total Population)

                'GDPPCPFem = GDPPCP(R%) * FemShRWage / FemshrPop
                GDPPCPMF(R%, Female) = CSng(GDPPCP(R%) * FemShRWage / FemshrPop)

                'Male earned income per capita = GDPPCP * MaleShRWage / (Male population/Total Population)
                'Male earned income per capita = GDPPCP * (1 - Female share of WageBill) / (Male population/Total Population)

                'GDPPCPMale = GDPPCP(R%) * (1 - FemShRWage) / (1 - FemshrPop)
                GDPPCPMF(R%, Male) = CSng(GDPPCP(R%) * (1 - FemShRWage) / (1 - FemshrPop))

                'GDPPCIndHDINewFem = (Log(AMAX(GDPPCPFem, 0.10001) * 1000) - GDPPCPHDINewMinLog) / GDPPCPHDINewDenom
                'GDPPCIndHDINewMale = (Log(AMAX(GDPPCPMale, 0.10001) * 1000) - GDPPCPHDINewMinLog) / GDPPCPHDINewDenom

                'mti 2017/12/27 Start
                GDPPCPMF(R%, 3) = GDPPCP(R%)

                'GDPPCIndHDINewFem = (Log(AMAX(GDPPCPMF(R%, Female), 0.10001) * 1000) - GDPPCPHDINewMinLog) / GDPPCPHDINewDenom
                'GDPPCIndHDINewMale = (Log(AMAX(GDPPCPMF(R%, Male), 0.10001) * 1000) - GDPPCPHDINewMinLog) / GDPPCPHDINewDenom


                GDPPCIndHDINewFem = CSng((Log(CDbl(AMAX(GDPPCPMF(R%, Female), 0.10001) * 1000)) - GDPPCPHDINewMinLog(Female)) / GDPPCPHDINewDenom(Female))
                GDPPCIndHDINewMale = CSng((Log(CDbl(AMAX(GDPPCPMF(R%, Male), 0.10001) * 1000)) - GDPPCPHDINewMinLog(Male)) / GDPPCPHDINewDenom(Male))
                'mti 2017/12/27 End

                HDIMF(R%, Female) = CSng(EdIndHDINew(R%, 2) ^ (1 / 3) * GDPPCIndHDINewFem ^ (1 / 3) * LifeExpIndHDINew(R%, 2) ^ (1 / 3))
                HDIMF(R%, Male) = CSng(EdIndHDINew(R%, 1) ^ (1 / 3) * GDPPCIndHDINewMale ^ (1 / 3) * LifeExpIndHDINew(R%, 1) ^ (1 / 3))

                'GDI(R%) = HDINEWFemale / HDINEWMale
                GDI(R%) = HDIMF(R%, Female) / HDIMF(R%, Male)

                GDPPCPMF(R%, Total) = GDPPCP(R%)
                HDIMF(R%, Total) = HDI(R%)

                'mti August 2017 End
                'mti September 2017 End


                '        HDI(R%) = EdIndHDINewFinal ^ (1 / 3) * GDPIndHDINew ^ (1 / 3) * LifeExpIndHDINew ^ (1 / 3)
                'mti April 2011 - end

                'jrs 2011/10/11
                SHDINEW(R%) = HDI(R%)

                HDIPRE2010(R%) = CSng((LifeExpInd + LitInd + GDPInd) / 3) 'Traditional Index
                HDIPRE2010(R%) = CSng(Amin(1.0#, HDIPRE2010(R%)))
                'Note:  UNDP combines LitInd (2/3) with Gross enrollment Ind (1/3)
                '   to create an Educational Attainment Index used for HDIPRE2010
                '   The minimum and maximum on gross enrollment are 0 and 100
                '   Gross enrollment is combined primary, seconary, tertiary (1/3 weight)

                'LifeExpInd = (LIFEXP(R%) - lifexpmin) / (HDILIFEMAXFloat - lifexpmin)
                HDILIFEMAXFloat = AMAX(HDILIFEMAXFloat, lifexpmin + 1)
                LifeExpInd = (LIFEXP(R%, Total) - lifexpmin) / (HDILIFEMAXFloat - lifexpmin)
                'Floating index
                LitInd = LIT(R%) / 100
                GDPInd = CSng((Log(GDPPCP(R%) * 1000) - Log100) / (LogGDPPCPMax - Log100))
                HDIFLOAT(R%) = CSng(Amin(100, (LifeExpInd + LitInd + GDPInd) / 3))

                'LifeExpInd = (LIFEXP(R%) - lifexpmin) / (HDILIFEMAX21 - lifexpmin)
                HDILIFEMAX21 = AMAX(HDILIFEMAX21, lifexpmin + 1)
                LifeExpInd = (LIFEXP(R%, Total) - lifexpmin) / (HDILIFEMAX21 - lifexpmin)
                'Rescaled 21st century index
                LitInd = LIT(R%) / 100
                GDPInd = CSng((Log(GDPPCP(R%) * 1000) - Log100) / (LogGDPPCP21 - Log100))
                HDI21STFIX(R%) = CSng(Amin(100, (LifeExpInd + LitInd + GDPInd) / 3))
            Next R%


            'cdv 2022/10/28 ****START****
            ' All Years 3: Unpaid Labor
            ' ---------------------------

            For R% = 1 To NR%
                If IY = 1 Then
                    If UNPAIDLABOR(R%, Male) = 0 Then
                        'jrs 2023/01/20 Call AnalFunc("UnpaidWorkMale v. TFR and GDI", New Single() {TFR(R%), GDI(R%)}, UNPAIDLABOR(R%, Male), New String() {"TFR (Linear)", "GDI (Linear)"}, context)
                        Call AnalFunc("UnpaidDomesticWorkMale v LABPARRFem and PopMedianAge and PopMedianAgeSquared", New Single() {LABPARR(R%, Female), POPMEDAGE(R%), POPMEDAGE(R%) * POPMEDAGE(R%)}, UNPAIDLABOR(R%, Male), New String() {"LABPARRFem", "MedianPopulationAge", "MedianPopulationAgeSquared"}, context)
                    End If
                    If UNPAIDLABOR(R%, Female) = 0 Then
                        'jrs 2023/01/20 Call AnalFunc("UnpaidDomesticWorkFemale v GDPPC and PopMedianAge and PopMedianAgeSquared", New Single() {GDPPCP(R%), POPMEDAGE(R%), POPMEDAGE(R%) * POPMEDAGE(R%)}, UNPAIDLABOR(R%, Female), New String() {"GDPPCP", "MedianPopulationAge", "MedianPopulationAgeSquared"}, context)
                        Call AnalFunc("UnpaidDomesticWorkFemale v LABPARRFem and PopMedianAge and PopMedianAgeSquared", New Single() {LABPARR(R%, Female), POPMEDAGE(R%), POPMEDAGE(R%) * POPMEDAGE(R%)}, UNPAIDLABOR(R%, Female), New String() {"LABPARRFem", "MedianPopulationAge", "MedianPopulationAgeSquared"}, context)
                    End If
                    ChainedLastYearUnpaidLabor(R%, Male) = UNPAIDLABOR(R%, Male)
                    ChainedLastYearUnpaidLabor(R%, Female) = UNPAIDLABOR(R%, Female)

                    'jrs 2023/01/20
                    'Call AnalFunc("UnpaidWorkMale v. TFR and GDI", New Single() {TFR(R%), GDI(R%)}, UnchainedLastYearUnpaidLabor(R%, Male), New String() {"TFR (Linear)", "GDI (Linear)"}, context)
                    'Call AnalFunc("UnpaidDomesticWorkFemale v GDPPC and PopMedianAge and PopMedianAgeSquared", New Single() {GDPPCP(R%), POPMEDAGE(R%), POPMEDAGE(R%) * POPMEDAGE(R%)}, UnchainedLastYearUnpaidLabor(R%, Female), New String() {"GDPPCP", "MedianPopulationAge", "MedianPopulationAgeSquared"}, context)
                    Call AnalFunc("UnpaidDomesticWorkMale v LABPARRFem and PopMedianAge and PopMedianAgeSquared", New Single() {LABPARR(R%, Female), POPMEDAGE(R%), POPMEDAGE(R%) * POPMEDAGE(R%)}, UnchainedLastYearUnpaidLabor(R%, Male), New String() {"LABPARRFem", "MedianPopulationAge", "MedianPopulationAgeSquared"}, context)
                    Call AnalFunc("UnpaidDomesticWorkFemale v LABPARRFem and PopMedianAge and PopMedianAgeSquared", New Single() {LABPARR(R%, Female), POPMEDAGE(R%), POPMEDAGE(R%) * POPMEDAGE(R%)}, UnchainedLastYearUnpaidLabor(R%, Female), New String() {"LABPARRFem", "MedianPopulationAge", "MedianPopulationAgeSquared"}, context)

                    If UnchainedLastYearUnpaidLabor(R%, Male) < 0 Then
                        UnchainedLastYearUnpaidLabor(R%, Male) = -0.0001
                    End If
                    If UnchainedLastYearUnpaidLabor(R%, Female) < 0 Then
                        UnchainedLastYearUnpaidLabor(R%, Female) = -0.0001
                    End If
                    If UNPAIDLABOR(R%, Male) < 0 Then
                        UNPAIDLABOR(R%, Male) = -0.0001
                    End If
                    If UNPAIDLABOR(R%, Female) < 0 Then
                        UNPAIDLABOR(R%, Female) = -0.0001
                    End If
                    UNPAIDLABOR(R%, Total) = (UNPAIDLABOR(R%, Male) + UNPAIDLABOR(R%, Female)) / 2
                Else

                    Dim UNPAIDLABORForecasted(2) As Single
                    Dim UNPAIDLABORShiftRatio As Single

                    'jrs 2023/01/20
                    'Call AnalFunc("UnpaidWorkMale v. TFR and GDI", New Single() {TFR(R%), GDI(R%)}, UNPAIDLABORForecasted(0), New String() {"TFR (Linear)", "GDI (Linear)"}, context)
                    'Call AnalFunc("UnpaidDomesticWorkFemale v GDPPC and PopMedianAge and PopMedianAgeSquared", New Single() {GDPPCP(R%), POPMEDAGE(R%), POPMEDAGE(R%) * POPMEDAGE(R%)}, UNPAIDLABORForecasted(1), New String() {"GDPPCP", "MedianPopulationAge", "MedianPopulationAgeSquared"}, context)
                    Call AnalFunc("UnpaidDomesticWorkMale v LABPARRFem and PopMedianAge and PopMedianAgeSquared", New Single() {LABPARR(R%, Female), POPMEDAGE(R%), POPMEDAGE(R%) * POPMEDAGE(R%)}, UNPAIDLABORForecasted(0), New String() {"LABPARRFem", "MedianPopulationAge", "MedianPopulationAgeSquared"}, context)
                    Call AnalFunc("UnpaidDomesticWorkFemale v LABPARRFem and PopMedianAge and PopMedianAgeSquared", New Single() {LABPARR(R%, Female), POPMEDAGE(R%), POPMEDAGE(R%) * POPMEDAGE(R%)}, UNPAIDLABORForecasted(1), New String() {"LABPARRFem", "MedianPopulationAge", "MedianPopulationAgeSquared"}, context)

                    If ChainedLastYearUnpaidLabor(R%, Male) <= 0 Then
                        UNPAIDLABOR(R%, Male) = UnchainedLastYearUnpaidLabor(R%, Male)
                    Else
                        UNPAIDLABORShiftRatio = UNPAIDLABORForecasted(0) / UnchainedLastYearUnpaidLabor(R%, Male)
                        UNPAIDLABOR(R%, Male) = ChainedLastYearUnpaidLabor(R%, Male) * UNPAIDLABORShiftRatio
                    End If

                    If ChainedLastYearUnpaidLabor(R%, Female) <= 0 Then
                        UNPAIDLABOR(R%, Female) = UnchainedLastYearUnpaidLabor(R%, Female)
                    Else
                        UNPAIDLABORShiftRatio = UNPAIDLABORForecasted(1) / UnchainedLastYearUnpaidLabor(R%, Female)
                        UNPAIDLABOR(R%, Female) = ChainedLastYearUnpaidLabor(R%, Female) * UNPAIDLABORShiftRatio
                    End If

                    If UNPAIDLABOR(R%, Male) < 0 Then
                        UNPAIDLABOR(R%, Male) = -0.0001
                    End If
                    If UNPAIDLABOR(R%, Female) < 0 Then
                        UNPAIDLABOR(R%, Female) = -0.0001
                    End If

                    ChainedLastYearUnpaidLabor(R%, Male) = UNPAIDLABOR(R%, Male)
                    ChainedLastYearUnpaidLabor(R%, Female) = UNPAIDLABOR(R%, Female)

                    'jrs 2023/07/13 uncommented
                    'jrs 2023/03/07
                    UNPAIDLABOR(R%, Male) = UNPAIDLABOR(R%, Male) * unpaidlaborm(R%, Male) * unpaidlaborm(R%, Total)
                    UNPAIDLABOR(R%, Female) = UNPAIDLABOR(R%, Female) * unpaidlaborm(R%, Female) * unpaidlaborm(R%, Total)

                    If UNPAIDLABOR(R%, Male) < 0 Then
                        UNPAIDLABOR(R%, Male) = -0.0001
                    End If
                    If UNPAIDLABOR(R%, Female) < 0 Then
                        UNPAIDLABOR(R%, Female) = -0.0001
                    End If

                    UnchainedLastYearUnpaidLabor(R%, Male) = UNPAIDLABORForecasted(0)
                    UnchainedLastYearUnpaidLabor(R%, Female) = UNPAIDLABORForecasted(1)
                    UNPAIDLABOR(R%, Total) = (UNPAIDLABOR(R%, Male) + UNPAIDLABOR(R%, Female)) / 2
                End If
            Next R%
            'cdv 2022/10/28 ****END****
            ' Dim sql As String
            'Sql = "SELECT DISTINCT RegionNo, GroupNo" &
            '" FROM GroupingFull" &
            '" WHERE GroupNo in (SELECT GroupNum FROM GroupIDNewFull WHERE GroupName in ('Africa East', 'Africa North', 'Africa South', 'Africa West', 'Africa Central', " &
            '" 'America Caribbean', 'America Central', 'America North', 'America South', " &
            '" 'Asia Central' , 'Asia East', 'Asia South East', 'Asia South', 'Asia West' ," &
            '" 'Europe East', 'Europe North', 'Europe South' ,'Europe West', " &
            '" 'Oceania'))" &
            '" ORDER BY RegionNo"
            'cdv 2022/11/18 ****START****

            Dim group As String
            If IY = 1 Then
                Using db As IFsContext = New IFsContext() 'cdv 2022/11/18
                    Dim WomenLeadershipGroupings = (From GLI In db.GListID
                                                    Join GL In db.GList On GLI.Code Equals GL.GListCode
                                                    Join GI In db.GroupIDNewFull On GL.RegionName Equals GI.GroupName
                                                    Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                                    Join Re In db.RegionalizationFull On G.RegionId Equals Re.RegionId
                                                    Where GLI.Description = "UN SubRegions"
                                                    Order By Re.RegionId Ascending
                                                    Select GroupName = GI.GroupName, CountryName = Re.CountryName, RegionID = Re.RegionId)
                    Dim iterations As Integer
                    iterations = 1
                    For Each Grouping In WomenLeadershipGroupings
                        If iterations <> Grouping.RegionID Then
                            Using dbreg As IFsContext = New IFsContext()
                                Dim nation = (From g In dbreg.RegionalizationFull Where g.RegionId = iterations Select g.CountryName).FirstOrDefault()
                                If nation = "Kosovo" Then
                                    Groupings(iterations) = New ArrayList
                                    Groupings(iterations).Add("Eur-South")
                                    Groupings(iterations).Add("Kosovo")
                                    Groupings(iterations).Add(iterations)
                                Else
                                    Groupings(iterations) = New ArrayList
                                    Groupings(iterations).Add("")
                                    Groupings(iterations).Add(nation)
                                    Groupings(iterations).Add(iterations)
                                End If
                            End Using
                            iterations += 1
                        End If
                        Groupings(iterations) = New ArrayList
                        Groupings(iterations).Add(Grouping.GroupName)
                        Groupings(iterations).Add(Grouping.CountryName)
                        Groupings(iterations).Add(Grouping.RegionID)
                        iterations += 1
                    Next
                End Using
            End If
            For R% = 1 To NR% 'cdv 2022/11/18
                group = Groupings(R%).Item(0).ToString()
                Call CalculateFemaleLeadership(R%, C, GDI, EDYRSAG15, group, context)
            Next R%
            For R% = 1 To NR% 'cdv 2022/11/18
                group = Groupings(R%).Item(0).ToString()
                Call CalculateFoodInsecurity(R%, MALNPOPP, HDIMF, group, context) 'cdv 2022/12/12
            Next R%
            'cdv 2022/11/18 ****END****
            '   All Years 3:  Human Poverty Index
            '   -------------------------------------
            Dim cumMort, cohMort, IllitRate, NoWatSafe, p3 As Double
            For R% = 1 To NR%
                For gend% = 1 To 2
                    cumMort = d(0, R%, gend%) /
                    AMAX(0.00001, agedst(0, R%, gend%))
                    For j% = 1 To 8 'up through age 39
                        cohMort = d(j%, R%, gend%) /
                        AMAX(0.00001, agedst(j%, R%, gend%)) * 5
                        cumMort = cumMort + cohMort
                    Next j%
                Next gend%
                cumMort = cumMort * 100
                If IY% = 1 Then
                    IllitRate = 100 - LITI(R%)
                Else
                    IllitRate = 100 - LIT(R%)
                End If
                'jrs 2009/02/18 NoWatSafe = 100 - WATSAFE(R%)
                'Debug.Print IY; RegName(R%)
                'mti 2022/08/17 start
                'mti 2022/10/20 start 'watsafefwdsw
                'If watsafenewsw = 1 Then 'new 5 category model
                If watsafenewsw = 1 And watsafefwdsw = 1 Then 'new 5 category model
                    'mti 2022/10/20 end
                    NoWatSafe = WATERHH(R%, Surface, National) + WATERHH(R%, Unimproved, National)
                Else
                    NoWatSafe = WATSAFE(R%, 1)
                End If
                'mti 2022/08/17 End
                p3 = (MALNCHP(R%) + NoWatSafe) / 2
                HPI1(R%) = CSng((1 / 3 * (cumMort ^ 3 + IllitRate ^ 3 + p3 ^ 3)) ^ (1 / 3))



            Next R%







            '   All Years 3:  Global Indicators
            '   -------------------------------

            WLIT = WLIT / WPOP
            WPQLI = WPQLI / WPOP
            'jrs 2020/08/31 begin
            'move to ifsPop3 after calculation of PQLI
            qlno = qlno / AMAX(PNO, 1)
            qlso = qlso / AMAX(PSO, 1)
            NSPQLI = CSng(qlno / AMAX(qlso, 0.01))
            'jrs 2020/08/31 end

            'mti 2022/09/30 Start
            For R% = 1 To NR%
                For Srvc% = 1 To 5
                    'Number of people - done here, as the Infrastr module only uses pop from a year back for routine sequence issues
                    WATERHHPOP(R%, Srvc%, National) = WATERHH(R%, Srvc%, National) * POP(R%) / 100
                    WATERHHPOP(R%, Srvc%, Urban) = WATERHH(R%, Srvc%, Urban) * POPURBAN(R%) / 100
                    WATERHHPOP(R%, Srvc%, Rural) = WATERHHPOP(R%, Srvc%, National) - WATERHHPOP(R%, Srvc%, Urban)

                    SANITHHPOP(R%, Srvc%, National) = SANITHH(R%, Srvc%, National) * POP(R%) / 100
                    SANITHHPOP(R%, Srvc%, Urban) = SANITHH(R%, Srvc%, Urban) * POPURBAN(R%) / 100
                    SANITHHPOP(R%, Srvc%, Rural) = SANITHHPOP(R%, Srvc%, National) - SANITHHPOP(R%, Srvc%, Urban)

                    WATERHHDEMPOP(R%, Srvc%, National) = WATERHHDEM(R%, Srvc%, National) * POP(R%) / 100
                    WATERHHDEMPOP(R%, Srvc%, Urban) = WATERHHDEM(R%, Srvc%, Urban) * POPURBAN(R%) / 100
                    WATERHHDEMPOP(R%, Srvc%, Rural) = WATERHHDEMPOP(R%, Srvc%, National) - WATERHHDEMPOP(R%, Srvc%, Urban)

                    SANITHHDEMPOP(R%, Srvc%, National) = SANITHHDEM(R%, Srvc%, National) * POP(R%) / 100
                    SANITHHDEMPOP(R%, Srvc%, Urban) = SANITHHDEM(R%, Srvc%, Urban) * POPURBAN(R%) / 100
                    SANITHHDEMPOP(R%, Srvc%, Rural) = SANITHHDEMPOP(R%, Srvc%, National) - SANITHHDEMPOP(R%, Srvc%, Urban)
                Next Srvc%
                'mti 2022/11/25 start
                MARRIEDFEMPOP(R%, Age15_19) = CSng(MARRIEDFEMSHR(R%, Age15_19) * agedst(4, R%, Female) / 100) '4 is 15 to 19 for 5 year age-groups
                MARRIEDFEMPOP(R%, Age20_24) = CSng(MARRIEDFEMSHR(R%, Age20_24) * agedst(5, R%, Female) / 100)
                'mti 2022/11/25 end
            Next R%
            'mti 2022/09/30 End
        End Sub

        Public Sub ifsPOP4AllYears(context As IForecastContext)
            'VB6:Static Sub IFsPop4()
            Dim NewAge%, OldAge%, AgeinYears%, CohortMigrants!, MulWorkAge!
            Dim CalcDeaths As Double
            Dim j As Integer
            'ttt 2019/10/08 TakeOut
            Array.Copy(LIFEXP, SLIFEXPFin, LIFEXP.Length)
            'jrs 2020/02/28 need it down there Array.Copy(POP, sPOPTotal, NR% + 1)

            For R% = 1 To NR%
                '   All Years 4:  Advance to Next Year - Full Age Distribution and Collapsed One
                '   -----------------------------------------------------------------------------

                'jrs 2016/12/01
                'All Years 4: Advance Stunting distribution by one year
                '------------------------------------------------------
                Dim Ds As Single, Olds As Single, Youngs As Single, Migs As Single
                For G = 1 To 2
                    For j = NCohorts To 1 Step -1
                        'Do we need to add stunted migrants?
                        'I'm assuming we don't
                        If j < NCohorts AndAlso agedst(j, R%, G) > 0 Then
                            'single year index
                            NewAge% = j * 5 - 1
                            'Computing people passing to the next age category
                            'jrs 2021/05/12 Olds = CSng(StuntingByAge(j, R, G) * fagedst(NewAge%, R%, G) / agedst(j, R%, G))
                            Olds = CSng(fStuntingByAge(NewAge%, R, G))
                        Else
                            Olds = 0
                        End If
                        'single year index
                        If j = 1 Then
                            NewAge% = 0
                            'Computing people coming from the previous age category
                            Youngs = CSng(fStuntingByAge(0, R, G)) 'jrs 2021/06/21 changed from StuntingByAge to fStuntingByAge
                        Else
                            If agedst(j - 1, R, G) > 0 Then
                                NewAge% = (j - 1) * 5 - 1
                                'Computing people coming from the previous age category
                                'jrs 2021/05/12 Youngs = CSng(StuntingByAge(j - 1, R, G) * fagedst(NewAge%, R%, G) / agedst(j - 1, R%, G))
                                Youngs = CSng(fStuntingByAge(NewAge%, R, G))
                            Else
                                Youngs = 0
                            End If
                        End If
                        'Compute Migrants
                        If Migratebyage(j%, 1).HasValue AndAlso agedst(j, R, G) > 0 Then
                            If G = 1 Then
                                Migs = CSng((MIGRANTS(R%) * malemigr * Migratebyage(j%, 1)) / agedst(j, R, G) * StuntingByAge(j, R, G))
                            Else
                                Migs = CSng(MIGRANTS(R%) * (1 - malemigr) * Migratebyage(j%, 2) / agedst(j, R, G) * StuntingByAge(j, R, G))
                            End If
                        Else
                            Migs = 0
                        End If

                        'Compute Deaths
                        If agedst(j, R%, G) > 0 Then
                            'jrs 2021/06/21 Deaths for infants were not considered before
                            If j = 1 Then
                                Ds = CSng((StuntingByAge(j, R, G) - Olds) * d(j, R%, G) / agedst(j, R%, G))
                                If agedst(0, R, G) > 0 Then
                                    Ds = Ds + CSng(Youngs * d(0, R, G) / agedst(0, R, G))
                                End If
                            Else
                                Ds = CSng((StuntingByAge(j, R, G) + Youngs - Olds) * d(j, R%, G) / agedst(j, R%, G))
                            End If
                        Else
                            Ds = 0
                        End If
                        StuntingByAge(j, R, G) = AMAX(StuntingByAge(j, R, G) - Ds - Olds + Youngs + Migs, 0)

                        'jrs 2021/05/12 adjust Migs and Deaths later advance age
                        If Ds > 0 Or Migs <> 0 Then
                            If j = 1 Then
                                For k = 1 To 4
                                    fStuntingByAge(k, R, G) = AMAX(fStuntingByAge(k, R, G) + (Migs - Ds) * fagedst(k, R, G) / agedst(j, R, G), 0)
                                Next k
                            Else
                                If j < NCohorts Then
                                    For k = 5 * (j - 1) To 5 * j - 1
                                        fStuntingByAge(k, R, G) = AMAX(fStuntingByAge(k, R, G) + (Migs - Ds) * fagedst(k, R, G) / agedst(j, R, G), 0)
                                    Next k
                                Else
                                    Dim k As Integer = 5 * (j - 1)
                                    fStuntingByAge(k, R, G) = AMAX(fStuntingByAge(k, R, G) + (Migs - Ds), 0)
                                End If
                            End If
                        End If
                    Next j
                    'jrs 2021/06/21 infants deaths
                    If agedst(0, R%, G) > 0 Then
                        Ds = CSng((StuntingByAge(0, R, G)) * d(0, R%, G) / agedst(0, R%, G))
                    Else
                        Ds = 0
                    End If
                    'infant migrants
                    If Migratebyage(0, 1).HasValue And agedst(0, R, G) > 0 Then
                        If G = 1 Then
                            Migs = CSng((MIGRANTS(R%) * malemigr * Migratebyage(0, 1)) / agedst(0, R, G) * StuntingByAge(0, R, G))
                        Else
                            Migs = CSng(MIGRANTS(R%) * (1 - malemigr) * Migratebyage(0, 2) / agedst(0, R, G) * StuntingByAge(0, R, G))
                        End If
                    Else
                        Migs = 0
                    End If
                    StuntingByAge(0, R, G) = AMAX(StuntingByAge(0, R, G) - Ds + Migs, 0)

                    'jrs 2021/05/12
                    fStuntingByAge(NAges, R%, G) = fStuntingByAge(NAges, R%, G) +
                    fStuntingByAge(NAges - 1, R%, G)
                    '100+ year-olds = 100+ year-olds + new 100 year-olds (99 advanced)
                    For j% = NAges - 2 To 0 Step -1 '(from 99 years of age to 0 years of age)
                        OldAge% = j%
                        NewAge% = j% + 1
                        fStuntingByAge(NewAge%, R%, G) = fStuntingByAge(OldAge%, R%, G)
                    Next j%
                    fStuntingByAge(0, R%, G) = StuntingByAge(0, R%, G) 'have advanced old infants; add new ones
                Next G

                '   At this point, we have agedst from previous year, but have put births into
                '       agedst(0) to temporarily hold infants
                '   Also have fagedst from previous year
                '   Need to (1) advance fagedst, being careful to accummulate those over 100,
                '       and to put births/infants into bottom of fagedst
                '           (2) subtract out deaths from appropriate cohort (after advance)
                '           (3) add up fagedst into agedst, putting infants into agedst(1) = 0-4

                '       Advance Population by year
                '       --------------------------

                'POP0TO5(R%) = 0
                For gend% = 1 To 2
                    fagedst(NAges, R%, gend%) = fagedst(NAges, R%, gend%) +
                fagedst(NAges - 1, R%, gend%)
                    '100+ year-olds = 100+ year-olds + new 100 year-olds (99 advanced)
                    For j% = NAges - 2 To 0 Step -1 '(from 99 years of age to 0 years of age)
                        OldAge% = j%
                        NewAge% = j% + 1
                        fagedst(NewAge%, R%, gend%) = fagedst(OldAge%, R%, gend%)
                    Next j%
                    fagedst(0, R%, gend%) = agedst(0, R%, gend%) 'have advanced old infants; add new ones
                    birthchk(R%) = birthchk(R%) + agedst(0, R%, gend%)

                    '           Remove deaths by cohort
                    '           -----------------------
                    'jrs 2009/12/09
                    Dim fagedstT As Single

                    For j% = 0 To NCohorts
                        If j% = NCohorts Then '100+, so subtract all deaths
                            fagedst(NAges, R%, gend%) = fagedst(NAges, R%, gend%) _
                        - d(NCohorts, R%, gend%) 'jrs 2007/05/21 added R% to d
                            deathchk(R%) = deathchk(R%) + d(NCohorts, R%, gend%) 'jrs 2007/05/21 added R% to d
                        ElseIf j% = 0 Then 'Infants, so subtract all deaths
                            fagedst(j%, R%, gend%) = fagedst(j%, R%, gend%) - d(j%, R%, gend%) 'jrs 2007/05/21 added R% to d
                            deathchk(R%) = deathchk(R%) + d(j%, R%, gend%) 'jrs 2007/05/21 added R% to d

                        ElseIf j% = 1 Then 'deaths belong to years 1-4, not 0
                            'jrs 2009/12/09
                            fagedstT = 0
                            For j2% = -4 To -1
                                AgeinYears% = j% * 5 + j2%
                                fagedstT = CSng(fagedstT + fagedst(AgeinYears%, R%, gend%))
                            Next j2

                            If fagedstT = 0 Then
                                CalcDeaths = d(j%, R%, gend%) / 4
                                'jrs 2021/11/11 *** start ***
                                If Migratebyage(j%, 1).HasValue Then
                                    If gend% = 1 Then 'Prepare migration for single-year cohorts
                                        CohortMigrants = CSng(MIGRANTS(R%) * malemigr * Migratebyage(j%, 1) / 4)
                                    Else
                                        CohortMigrants = CSng(MIGRANTS(R%) * (1 - malemigr) _
                                    * Migratebyage(j%, 2) / 4)
                                    End If
                                Else
                                    CohortMigrants = 0
                                End If
                                'jrs 2021/11/11 *** end ***
                            End If

                            For j2% = -4 To -1
                                AgeinYears% = j% * 5 + j2%
                                'jrs 2009/12/09
                                'This allowed negative numbers in fagedst
                                'agedst is not evenly distributed in fagedst
                                'thus d/4 could produce negatives
                                'CalcDeaths = d(j%, R%, gend%) / 4 'jrs 2007/05/21 added R% to d
                                If fagedstT <> 0 Then
                                    CalcDeaths = d(j%, R%, gend%) * fagedst(AgeinYears%, R%, gend%) / fagedstT 'jrs 2007/05/21 added R% to d
                                    'jrs 2021/11/29 *** start ***
                                    If Migratebyage(j%, 1).HasValue Then
                                        If gend% = 1 Then 'Prepare migration for single-year cohorts
                                            CohortMigrants = CSng(MIGRANTS(R%) * malemigr * Migratebyage(j%, 1) * fagedst(AgeinYears%, R%, gend%) / fagedstT)
                                        Else
                                            CohortMigrants = CSng(MIGRANTS(R%) * (1 - malemigr) _
                                            * Migratebyage(j%, 2) * fagedst(AgeinYears%, R%, gend%) / fagedstT)
                                        End If
                                    Else
                                        CohortMigrants = 0
                                    End If
                                    'jrs 2021/11/29 *** end ***
                                End If

                                'jrs 2021/11/29 Puerto Rico needs 15% in base case, 20% is too much for migration multiplier to 2 for UAE
                                If CohortMigrants < -0.16 * fagedst(AgeinYears%, R%, gend%) Then
                                    CohortMigrants = CSng(-0.16 * fagedst(AgeinYears%, R%, gend%))
                                End If

                                'jrs 2021/11/29 added limit of 20% of original value copied from other cohorts
                                'Do not let migration exhaust cohort
                                fagedst(AgeinYears%, R%, gend%) =
                                AMAX(fagedst(AgeinYears%, R%, gend%) * 0.2,
                                fagedst(AgeinYears%, R%, gend%) - CalcDeaths + CohortMigrants) 'jrs 2021/11/11 added CohortMigrants
                                deathchk(R%) = deathchk(R%) + CalcDeaths
                            Next j2%
                        ElseIf j% > 1 AndAlso j% < NCohorts Then
                            'jrs 2021/11/29 move up
                            'jrs 2009/12/09
                            fagedstT = 0
                            For j2% = -5 To -1
                                AgeinYears% = j% * 5 + j2%
                                fagedstT = CSng(fagedstT + fagedst(AgeinYears%, R%, gend%))
                            Next j2

                            If fagedstT = 0 Then
                                CalcDeaths = d(j%, R%, gend%) / 5 'Prepare deaths for single-year cohorts 'jrs 2007/05/21 added R% to d
                                'jrs 2021/11/29 move inside
                                'jrs 2012/09/03
                                If Migratebyage(j%, 1).HasValue Then
                                    If gend% = 1 Then 'Prepare migration for single-year cohorts
                                        CohortMigrants = CSng(MIGRANTS(R%) * malemigr * Migratebyage(j%, 1) / 5)
                                    Else
                                        CohortMigrants = CSng(MIGRANTS(R%) * (1 - malemigr) _
                                    * Migratebyage(j%, 2) / 5)
                                    End If
                                Else
                                    CohortMigrants = 0
                                End If
                            End If

                            For j2% = -5 To -1
                                AgeinYears% = j% * 5 + j2%
                                'jrs 2009/12/09
                                'This allowed negative numbers in fagedst
                                'agedst is not evenly distributed in fagedst
                                'thus d/5 could produce negatives
                                If fagedstT <> 0 Then
                                    CalcDeaths = d(j%, R%, gend%) * fagedst(AgeinYears%, R%, gend%) / fagedstT
                                    'jrs 2021/11/29
                                    If Migratebyage(j%, 1).HasValue Then
                                        If gend% = 1 Then 'Prepare migration for single-year cohorts
                                            CohortMigrants = CSng(MIGRANTS(R%) * malemigr * Migratebyage(j%, 1) * fagedst(AgeinYears%, R%, gend%) / fagedstT)
                                        Else
                                            CohortMigrants = CSng(MIGRANTS(R%) * (1 - malemigr) _
                                                * Migratebyage(j%, 2) * fagedst(AgeinYears%, R%, gend%) / fagedstT)
                                        End If
                                    Else
                                        CohortMigrants = 0
                                    End If
                                End If
                                'jrs 2021/11/29 Puerto Rico needs 15% in base case, 20% is too much for migration multiplier to 2 for UAE
                                If CohortMigrants < -0.16 * fagedst(AgeinYears%, R%, gend%) Then
                                    CohortMigrants = CSng(-0.16 * fagedst(AgeinYears%, R%, gend%))
                                End If
                                'jrs 2024/03/23
                                'Check that Migration leaves at least 100 people in age
                                If CohortMigrants < 0 AndAlso fagedst(AgeinYears%, R%, gend%) + CohortMigrants < 0.0001 Then
                                    CohortMigrants = 0
                                End If
                                fagedst(AgeinYears%, R%, gend%) =
                                AMAX(fagedst(AgeinYears%, R%, gend%) * 0.2,
                                fagedst(AgeinYears%, R%, gend%) - CalcDeaths + CohortMigrants)
                                'Do not let migration exhaust cohort
                                deathchk(R%) = deathchk(R%) + CalcDeaths
                            Next j2%
                        End If
                    Next j%

                    '           All Years 4:  Collapse Full Age Distribution into 5-year Intervals
                    '           ------------------------------------------------------------------
                    ' If RegName(R%) = "Micronesia, Fed. Sts." Then Stop
                    For j% = 0 To NCohorts
                        agedst(j%, R%, gend%) = 0
                        If j% = 0 Then
                            agedst(j%, R%, gend%) = 0 'Have moved infants into fagedst; agedist(1) = 0-4
                        ElseIf j% <> 0 AndAlso j% < NCohorts Then
                            For j2% = -5 To -1 Step 1
                                agedst(j%, R%, gend%) = agedst(j%, R%, gend%) +
                            fagedst(j% * 5 + j2%, R%, gend%)
                            Next j2%
                        ElseIf j% = NCohorts Then
                            agedst(j%, R%, gend%) = fagedst(NAges, R%, gend%) '>=100 Years of Age
                        End If
                        'jrs 2012/12/19
                        If agedst(j%, R%, gend%) < 0.000001 Then
                            agedst(j%, R%, gend%) = 0
                        End If
                        'Debug.Print j, agedst(j%, R%, 1), agedst(j%, R%, 2)
                    Next j%
                    '            For ICount% = 0 To 5
                    '                POP0TO5(R%) = POP0TO5(R%) + fagedst(ICount%, R%, gend%)
                    '            Next ICount%
                Next gend%
                'If RegName$(R%) = "Unitd Kingdm" Then Stop
                ' If RegName$(R%) = "Burundi" Then Stop

                'mti August 26, 2009 start
                'Dummy line from Mohammod to help documentation parsing process
                'Doc POP(0) = agedst(j%, R%, 1) + agedst(j%, R%, 2)
                '    Doc POP(0) = agedst(j%, R%, 1) + agedst(j%, R%, 2)
                'mti August 26, 2009 end

                '    For ICount% = 0 To NCohorts
                '        popchk3(R%) = popchk3(R%) + agedst(ICount%, R%, 1) + agedst(ICount%, R%, 2)
                '    Next ICount%
                '        popchk3(R%) = popchk3(R%) - birthchk(R%) + deathchk(R%)
                '
                '
                '    For ICount% = 0 To NAges
                '        popchk2(R%) = popchk2(R%) + fagedst(ICount%, R%, 1) + fagedst(ICount%, R%, 2)
                '    Next ICount%
                '        popchk2(R%) = popchk2(R%) - birthchk(R%) + deathchk(R%)

                'bbh 2009/09/09

                'ttt 2019/10/08 Taken Out
                'For gend% = 1 To NGender%
                '    SLIFEXPFin(R%, gend%) = LIFEXP(R%, gend%)
                'Next gend%

                'mti February 2011 Infarstructure -start
                'mti May 04, 2012 - start
                If IY% = 1 Then
                    'sPopTotal1(R%) = POP(R%)
                    sPopTotal1(R%) = POP(R%) / (1 + POPR(R%) / 100)
                Else
                    sPopTotal1(R%) = sPOPTotal(R%)
                End If
                'mti May 04, 2012 - end


                'mti June 02, 2010 - added to avaoid run to run in averaging infraelecacc (rural and urban)
                'ttt 2019/10/08 Taken Out 
                'jrs 2020/02/28 put back in, need it here
                sPOPTotal(R%) = POP(R%)
                'sPOPRURAL(R%) = POP(R%) - POPURBAN(R%)
                'mti February 2011 Infarstructure end
                ' If RegName(R%) = "United States of America" Then Stop

                '   bbh 2013/08/11
                '   All Years 4:  Retirement Age Adjustement
                '   ---------------------------------------

                'bbh 2014/06/18 start
                If HHRETIRENEED(R%) > CRETIRE(R%) AndAlso workageretirendpctuse(R%) > 0 Then
                    MulWorkAge = CSng((HHRETIRENEED(R%) / CRETIRE(R%)) ^ 0.1)
                    sworkageretire(R%) = sworkageretire(R%) * MulWorkAge
                End If
                'bbh 2014/06/18 end



            Next R%

        End Sub

        'jrs 2026/05/20
        Dim TFNameEdvsWmninMarr As String
        Dim TFNameEdvsMarrDiff As String

        '=============================Population.BAS  =======================
        Private Sub IFsPopMarriage1FirstYear(context As IForecastContext)
            '------------------------------------------
            'mti 2022/11/25 New Procedure - computes rate and age of marriage - taken out of IFsPOp as it was running out of space

            '
            '   FIRST YEAR AND CALL ONLY
            '   -------------------------------------------------------------

            'mti 2022/11/25 new variables for rate of marriage
            ReDim MARRIEDFEMSHRShift(NR%, 2)
            ReDim sMARRIEDFEMSHR(NR%, 2)
            ReDim MARRIEDFEMSHRGR(NR%, 2)
            Call LoadGrowthRatesTwoDim("MARRIEDFEMSHRGR", MARRIEDFEMSHRGR, context)
            'mti 2022/11/25 End

            'cdv 2022/03/07
            ReDim AGEOFMARRIAGEShift(NR%, 3)
            'mti 2022/10/24 start
            ReDim sAGEOFMARRIAGE(NR%, NGender%)
            'mti 2022/10/24 End
            ReDim AGEOFMARRIAGEGR(NR%, NGender%)
            Call LoadGrowthRatesTwoDim("AGEOFMARRIAGEGR", AGEOFMARRIAGEGR, context)
            'mti 2022/10/24 End

            'jrs 2026/05/20
            If context.HistoricMode = True Then
                TFNameEdvsWmninMarr = "Education (Both Sexes, 25+) Versus Women in Marriage or Union, 15 to 19 (" & CStr(FirstYear) & ") Log"
                TFNameEdvsWmninMarr = getFunctionNameYrOrMstRct(TFNameEdvsWmninMarr, FirstYear, context)
            Else
                TFNameEdvsWmninMarr = "Education (Both Sexes, 25+) Versus Women in Marriage or Union, 15 to 19 (" & CStr(BaseYear) & ") Log"
                TFNameEdvsWmninMarr = getFunctionNameYrOrMstRct(TFNameEdvsWmninMarr, BaseYear, context)
            End If
            If context.HistoricMode = True Then
                TFNameEdvsMarrDiff = "Education (Both Sexes, 25+) Vs Married % Diff, 20_24 and 15_19, Women (" & CStr(FirstYear) & ") Lin"
                TFNameEdvsMarrDiff = getFunctionNameYrOrMstRct(TFNameEdvsMarrDiff, FirstYear, context)
            Else
                TFNameEdvsMarrDiff = "Education (Both Sexes, 25+) Vs Married % Diff, 20_24 and 15_19, Women (" & CStr(BaseYear) & ") Cubic"
                TFNameEdvsMarrDiff = getFunctionNameYrOrMstRct(TFNameEdvsMarrDiff, BaseYear, context)
            End If


            Dim TbName As String
            Dim CompVal As Single
            'mti 2022/11/25 start
            For R% = 1 To NR%
                'Rate of marriage, women, 15 to 19 year olds, first year
                'jrs 2026/05/20 moved function name out of loop, as it does not change with region
                'If context.HistoricMode = True Then
                '    TbName$ = "Education (Both Sexes, 25+) Versus Women in Marriage or Union, 15 to 19 (" & CStr(FirstYear) & ") Log"
                '    TbName$ = getFunctionNameYrOrMstRct(TbName$, FirstYear, context)
                'Else
                '    TbName$ = "Education (Both Sexes, 25+) Versus Women in Marriage or Union, 15 to 19 (" & CStr(BaseYear) & ") Log"
                '    TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
                'End If

                'jrs 2026/05/20
                'Call XYTABL(TbName$, AMAX(1, Amin(16, EDYRSAG25(R%, Total))), CompVal, context)
                Call XYTABL(TFNameEdvsWmninMarr, AMAX(1, Amin(16, EDYRSAG25(R%, Total))), CompVal, context)

                MARRIEDFEMSHRShift(R%, Age15_19) = MARRIEDFEMSHR(R%, Age15_19) - CompVal
                sMARRIEDFEMSHR(R%, Age15_19) = MARRIEDFEMSHR(R%, Age15_19)

                'Rate of marriage, women, 20 to 24 year olds, , first year
                'jrs 2026/05/20 
                'If context.HistoricMode = True Then
                '    TbName$ = "Education (Both Sexes, 25+) Vs Married % Diff, 20_24 and 15_19, Women (" & CStr(FirstYear) & ") Lin"
                '    TbName$ = getFunctionNameYrOrMstRct(TbName$, FirstYear, context)
                'Else
                '    TbName$ = "Education (Both Sexes, 25+) Vs Married % Diff, 20_24 and 15_19, Women (" & CStr(BaseYear) & ") Cubic"
                '    TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
                'End If
                'Call XYTABL(TbName$, AMAX(1, Amin(16, EDYRSAG25(R%, Total))), CompVal, context)
                Call XYTABL(TFNameEdvsMarrDiff, AMAX(1, Amin(16, EDYRSAG25(R%, Total))), CompVal, context)
                'the dependent variable is a difference, 20 to 24 in-marriage rate minus 15 to 19 rate, so is the shift factor
                MARRIEDFEMSHRShift(R%, Age20_24) = (MARRIEDFEMSHR(R%, Age20_24) - MARRIEDFEMSHR(R%, Age15_19)) - CompVal
                sMARRIEDFEMSHR(R%, Age20_24) = MARRIEDFEMSHR(R%, Age20_24)
                'mti 2022/11/25 end

                For gndr% = 1 To 2
                    If gndr% = Male Then
                        'note for cdv - function name not consistent with IFs Convention
                        Call AnalFunc("AgeOfMarraigeMaleWB V. GDPPC (Log) & EdYears15Male (Log) (All Most Recent)",
                                      New Single() {GDPPCP(R%), EDYRSAG15(R%, Male)}, ageofmarriagecomp(gndr%),
                                      New String() {"GDPPC (Log)", "EdYearsAge15Male(MOSTRECENT) (Log)"}, context)
                    Else
                        Call AnalFunc("AgeOfMarraigeWomenWB v. GDPPC (Log) & EdYears15Female (Log) (All Most Recent)",
                                      New Single() {GDPPCP(R%), EDYRSAG15(R%, Female)}, ageofmarriagecomp(gndr%),
                                      New String() {"GDPPC (Log)", "EdYearsAge15Female(MOSTRECENT) (Log)"}, context)
                    End If
                    AGEOFMARRIAGEShift(R%, gndr%) = AGEOFMARRIAGE(R%, gndr%) / ageofmarriagecomp(gndr%)
                    sAGEOFMARRIAGE(R%, gndr%) = AGEOFMARRIAGE(R%, gndr%)
                Next gndr%

            Next R%

            IFsPopMarriage1AllYears()



        End Sub

        'mti 2022/11/25
        Private Sub IFsPopMarriage1AllbutFirst(context As IForecastContext)
            Dim TbName As String
            Dim CompVal As Single
            'mti 2022/11/25
            For R% = 1 To NR%
                'Rate of Marriage, Age 15 to 19, projection
                'jrs 2026/05/20 
                'If context.HistoricMode = True Then
                '    TbName$ = "Education (Both Sexes, 25+) Versus Women in Marriage or Union, 15 to 19 (" & CStr(FirstYear) & ") Log"
                '    TbName$ = getFunctionNameYrOrMstRct(TbName$, FirstYear, context)
                'Else
                '    TbName$ = "Education (Both Sexes, 25+) Versus Women in Marriage or Union, 15 to 19 (" & CStr(BaseYear) & ") Log"
                '    TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
                'End If

                'Call XYTABL(TbName$, AMAX(1, Amin(16, SEDYRSAG25(R%, Total))), CompVal, context)
                Call XYTABL(TFNameEdvsWmninMarr, AMAX(1, Amin(16, SEDYRSAG25(R%, Total))), CompVal, context)
                MARRIEDFEMSHR(R%, Age15_19) = AMAX(1, Amin(95, CompVal + ConvergeOverTime1(MARRIEDFEMSHRShift(R%, Age15_19), 0, 100)))

                If MARRIEDFEMSHRGR(R%, Age15_19) > -9999 Then   'And MARRIEDFEMSHRGR(R%,Age15_19) < 0.015
                    Dim RoMgrComp = (MARRIEDFEMSHR(R%, Age15_19) - sMARRIEDFEMSHR(R%, Age15_19)) / AMAX(0.01, sMARRIEDFEMSHR(R%, Age15_19))
                    Dim RoMgrSmoothed = ConvergeOverTime(MARRIEDFEMSHRGR(R%, Age15_19), RoMgrComp, 20)
                    MARRIEDFEMSHR(R%, Age15_19) = AMAX(1, Amin(95, sMARRIEDFEMSHR(R%, Age15_19) * (1 + RoMgrSmoothed)))
                End If

                sMARRIEDFEMSHR(R%, Age15_19) = MARRIEDFEMSHR(R%, Age15_19)
                'mti 2022/12/13 start
                'MARRIEDFEMSHR(R%, Age15_19) = MARRIEDFEMSHR(R%, Age15_19) * marriedfemshrm(R%, Age15_19)
                MARRIEDFEMSHR(R%, Age15_19) = AMAX(1, Amin(95, MARRIEDFEMSHR(R%, Age15_19) * marriedfemshrm(R%, Age15_19)))
                'mti 2022/12/13 end

                'Rate of Marriage, Age 20 to 24, projection
                'jrs 2026/05/20 
                'If context.HistoricMode = True Then
                '    TbName$ = "Education (Both Sexes, 25+) Vs Married % Diff, 20_24 and 15_19, Women (" & CStr(FirstYear) & ") Lin"
                '    TbName$ = getFunctionNameYrOrMstRct(TbName$, FirstYear, context)
                'Else
                '    TbName$ = "Education (Both Sexes, 25+) Vs Married % Diff, 20_24 and 15_19, Women (" & CStr(BaseYear) & ") Cubic"
                '    TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, context)
                'End If

                'Call XYTABL(TbName$, AMAX(1, Amin(16, SEDYRSAG25(R%, Total))), CompVal, context)
                Call XYTABL(TFNameEdvsMarrDiff, AMAX(1, Amin(16, SEDYRSAG25(R%, Total))), CompVal, context)

                'the dependent variable is a difference, 20 to 24 in-marriage rate minus 15 to 19 rate, so is the shift factor
                MARRIEDFEMSHR(R%, Age20_24) = AMAX(1, Amin(95, MARRIEDFEMSHR(R%, Age15_19) + CompVal + ConvergeOverTime1(MARRIEDFEMSHRShift(R%, Age20_24), 0, 100)))

                If MARRIEDFEMSHRGR(R%, Age20_24) > -9999 Then   'And MARRIEDFEMSHRGR(R%,Age20_24) < 0.015
                    Dim RoMgrComp = (MARRIEDFEMSHR(R%, Age20_24) - sMARRIEDFEMSHR(R%, Age20_24)) / AMAX(0.01, sMARRIEDFEMSHR(R%, Age20_24))
                    Dim RoMgrSmoothed = ConvergeOverTime(MARRIEDFEMSHRGR(R%, Age20_24), RoMgrComp, 20)
                    MARRIEDFEMSHR(R%, Age20_24) = AMAX(1, Amin(95, sMARRIEDFEMSHR(R%, Age20_24) * (1 + RoMgrSmoothed)))
                End If

                sMARRIEDFEMSHR(R%, Age20_24) = MARRIEDFEMSHR(R%, Age20_24)
                'mti 2022/12/13 start
                'MARRIEDFEMSHR(R%, Age20_24) = MARRIEDFEMSHR(R%, Age20_24) * marriedfemshrm(R%, Age20_24)
                MARRIEDFEMSHR(R%, Age20_24) = AMAX(1, Amin(95, MARRIEDFEMSHR(R%, Age20_24) * marriedfemshrm(R%, Age20_24)))
                'mti 2022/12/13 End
                'mti 2022/11/25 end

                'All Years 2: Age of Marriage
                '--------------------------------------
                'cdv 2022/10/07 - Broke up AoM analfuncs to save space -> sub was too long to compile without this change

                For gndr% = 1 To 2
                    If gndr% = Male Then
                        'note for cdv - function name not consistent with IFs Convention
                        Call AnalFunc("AgeOfMarraigeMaleWB V. GDPPC (Log) & EdYears15Male (Log) (All Most Recent)",
                                      New Single() {GDPPCP(R%), SEDYRSAG15v2(R%, Male)}, ageofmarriagecomp(gndr%),
                                      New String() {"GDPPC (Log)", "EdYearsAge15Male(MOSTRECENT) (Log)"}, context)
                    Else
                        Call AnalFunc("AgeOfMarraigeWomenWB v. GDPPC (Log) & EdYears15Female (Log) (All Most Recent)",
                                      New Single() {GDPPCP(R%), SEDYRSAG15v2(R%, Female)}, ageofmarriagecomp(gndr%),
                                      New String() {"GDPPC (Log)", "EdYearsAge15Female(MOSTRECENT) (Log)"}, context)
                    End If
                    AGEOFMARRIAGE(R%, gndr%) = (ageofmarriagecomp(gndr%) * ConvergeOverTime(AGEOFMARRIAGEShift(R%, gndr%), 1, 150))
                    If AGEOFMARRIAGEGR(R%, gndr%) > -9999 And AGEOFMARRIAGEGR(R%, gndr%) < 0.015 Then
                        Dim AoMgrComp = (AGEOFMARRIAGE(R%, gndr%) - sAGEOFMARRIAGE(R%, gndr%)) / AMAX(0.01, sAGEOFMARRIAGE(R%, gndr%))
                        Dim AoMgrSmoothed = ConvergeOverTime(AGEOFMARRIAGEGR(R%, gndr%), AoMgrComp, 10)
                        AGEOFMARRIAGE(R%, gndr%) = sAGEOFMARRIAGE(R%, gndr%) * (1 + AoMgrSmoothed)
                        sAGEOFMARRIAGE(R%, gndr%) = AGEOFMARRIAGE(R%, gndr%)
                    End If

                    'mti 2022/12/13 start
                    AGEOFMARRIAGE(R%, gndr%) = Amin(50, AMAX(12, AGEOFMARRIAGE(R%, gndr%) * ageofmarriagem(R%, gndr%)))
                    'mti 2022/12/13 End
                Next gndr%

            Next R%

            IFsPopMarriage1AllYears()
        End Sub

        'mti 2022/11/25
        Private Sub IFsPopMarriage1AllYears()

            For R% = 1 To NR%
                Dim MalePop As Double = 0
                Dim Femalepop As Double = 0
                For j = 4 To 10
                    'cohorts will be behind by a year (?); should be close though
                    MalePop = MalePop + agedst(j, R%, Male)
                    Femalepop = Femalepop + agedst(j, R%, Female)
                Next
                MalePop = MalePop + fagedst(50, R%, Male)
                Femalepop = Femalepop + fagedst(50, R%, Female)

                'mti 2022/12/13 Start
                'compute the impact of scenario based changes in shsre married (15 to 19) on SMAM/AGEOFMARRIAGE of female
                If marriedfemshrm(R%, Age15_19) <> 1 And ageofmarriagem(R%, Female) = 1 Then

                    Dim MarriedNewPcntBase = sMARRIEDFEMSHR(R%, Age15_19) ' AMAX(0, sMARRIEDFEMSHR(R%, Age20_24) - sMARRIEDFEMSHR(R%, Age15_19))
                    Dim MarriedNewPcntScen = MARRIEDFEMSHR(R%, Age15_19)  'AMAX(0, MARRIEDFEMSHR(R%, Age20_24) - sMARRIEDFEMSHR(R%, Age15_19))

                    'added/reduced marriages would most likely be coming from the next/prior group;
                    'so add/subtract 3 years; 3 is the average of 1 to 5; we can also try putting 4 or 5 here
                    Dim YrChange = 3 * agedst(4, R%, Female) * (MarriedNewPcntScen - MarriedNewPcntBase) / 100
                    Dim YrChangePerPerson = YrChange / AMAX(0.00000001, Femalepop)

                    AGEOFMARRIAGE(R%, Female) = CSng(Amin(50, AMAX(12, AGEOFMARRIAGE(R%, Female) - YrChangePerPerson)))

                End If

                'compute the impact of scenario based changes in shsre married (20 to 24) on SMAM/AGEOFMARRIAGE of female
                If marriedfemshrm(R%, Age20_24) <> 1 And ageofmarriagem(R%, Female) = 1 Then

                    Dim MarriedNewPcntBase = AMAX(0, sMARRIEDFEMSHR(R%, Age20_24) - sMARRIEDFEMSHR(R%, Age15_19))
                    Dim MarriedNewPcntScen = AMAX(0, MARRIEDFEMSHR(R%, Age20_24) - sMARRIEDFEMSHR(R%, Age15_19))

                    Dim YrChange = 3 * agedst(5, R%, Female) * (MarriedNewPcntScen - MarriedNewPcntBase) / 100
                    Dim YrChangePerPerson = YrChange / AMAX(0.00000001, Femalepop)

                    AGEOFMARRIAGE(R%, Female) = CSng(Amin(50, AMAX(12, AGEOFMARRIAGE(R%, Female) - YrChangePerPerson)))

                End If
                'mti 2022/12/13 End


                'compute the impact of scenario based changes SMAM/AGEOFMARRIAGE of female on shsre married (15 to 19) 'mti 2022/12/13 - changed the comment
                If (ageofmarriagem(R%, Female) <> 1 Or ageofmarriagem(R%, Total) <> 1) And marriedfemshrm(R%, Age15_19) = 1 Then
                    'mti 2022/12/08 start
                    'accounting algorithm for the impact of average aom change, via multiplier, on share of girls married, 15 to 19
                    Dim TotalYrsBase = sAGEOFMARRIAGE(R%, Female) * Femalepop
                    Dim TotalYrsScen = AGEOFMARRIAGE(R%, Female) * Femalepop
                    Dim TotYrsChangeRatio = TotalYrsScen / TotalYrsBase

                    Dim AgeGroupYrsBase = 17 * MARRIEDFEMSHR(R%, Age15_19) * agedst(4, R%, Female) / 100   'sMARRIEDFEMSHR() holds base/pre-multiplier projection
                    AgeGroupYrsBase = AgeGroupYrsBase + sAGEOFMARRIAGE(R%, Female) * (100 - MARRIEDFEMSHR(R%, Age15_19)) * agedst(4, R%, Female) / 100

                    Dim AgeGroupYrsScen = AgeGroupYrsBase * TotYrsChangeRatio

                    Dim AgeGroupYrsChange = AgeGroupYrsScen - AgeGroupYrsBase 'AgeGroupYrsBase * TotYrsChangeRatio ' * MARRIEDFEMSHR(R%, Age15_19) / 100

                    Dim AgeGroupYrsChangeUnMarried = (AGEOFMARRIAGE(R%, Female) - sAGEOFMARRIAGE(R%, Female)) * (100 - MARRIEDFEMSHR(R%, Age15_19)) * agedst(4, R%, Female) / 100
                    Dim AgeGroupYrsChangeMarried = AgeGroupYrsChange - AgeGroupYrsChangeUnMarried

                    Dim AgeGroupPopMarriedScen = AgeGroupYrsChangeMarried / 17  'AGEOFMARRIAGE(R%, Female)
                    Dim ChangePcnt15_19 = 100 * AgeGroupPopMarriedScen / AMAX(0.00000001, agedst(4, R%, Female))

                    MARRIEDFEMSHR(R%, Age15_19) = CSng(AMAX(1, Amin(95, MARRIEDFEMSHR(R%, Age15_19) - ChangePcnt15_19)))
                    'mti 2022/12/08 End
                End If

                'compute the impact of scenario based changes SMAM/AGEOFMARRIAGE of female on shsre married (20 to 24) 'mti 2022/12/13 - changed the comment
                If (ageofmarriagem(R%, Female) <> 1 Or ageofmarriagem(R%, Total) <> 1) And marriedfemshrm(R%, Age20_24) = 1 Then

                    'mti 2022/12/08 start
                    'accounting algorithm for the impact of average aom change, via multiplier, on share of girls married, 15 to 19
                    Dim TotalYrsBase = sAGEOFMARRIAGE(R%, Female) * Femalepop
                    Dim TotalYrsScen = AGEOFMARRIAGE(R%, Female) * Femalepop
                    Dim TotYrsChangeRatio = TotalYrsScen / TotalYrsBase

                    Dim MarriedNewPcnt = AMAX(0, MARRIEDFEMSHR(R%, Age20_24) - MARRIEDFEMSHR(R%, Age15_19))
                    Dim AgeGroupYrsBase = 22 * MarriedNewPcnt * agedst(5, R%, Female) / 100   'sMARRIEDFEMSHR() holds base/pre-multiplier projection
                    AgeGroupYrsBase = AgeGroupYrsBase + sAGEOFMARRIAGE(R%, Female) * (100 - MarriedNewPcnt) * agedst(5, R%, Female) / 100

                    Dim AgeGroupYrsScen = AgeGroupYrsBase * TotYrsChangeRatio

                    Dim AgeGroupYrsChange = AgeGroupYrsScen - AgeGroupYrsBase 'AgeGroupYrsBase * TotYrsChangeRatio ' * MARRIEDFEMSHR(R%, Age20_24) / 100

                    'changes from those who married either earlier than 20, or after 24
                    Dim AgeGroupYrsChangeUnPrevMarried = (AGEOFMARRIAGE(R%, Female) - sAGEOFMARRIAGE(R%, Female)) * (100 - MarriedNewPcnt) * agedst(5, R%, Female) / 100
                    Dim AgeGroupYrsChangeMarried = AgeGroupYrsChange - AgeGroupYrsChangeUnPrevMarried

                    Dim AgeGroupPopMarriedScen = AgeGroupYrsChangeMarried / 22  'AGEOFMARRIAGE(R%, Female)
                    Dim ChangePcnt20_24 = 100 * AgeGroupPopMarriedScen / AMAX(0.00000001, agedst(5, R%, Female))


                    MARRIEDFEMSHR(R%, Age20_24) = CSng(AMAX(1, Amin(95, MARRIEDFEMSHR(R%, Age20_24) - ChangePcnt20_24)))
                    'mti 2022/12/08 End

                End If


                'mti 2022/11/25 end

                AGEOFMARRIAGE(R%, Total) = CSng((AGEOFMARRIAGE(R%, Male) * MalePop + AGEOFMARRIAGE(R%, Female) * Femalepop) / AMAX(0.0000001, (MalePop + Femalepop)))
                'cdv 2022/03/07 ****END****

                'mti 2022/10/24 End




            Next R%


        End Sub

        'JRS 2016/02/10
        Private Sub SAMAdmissionInitialization(context As IForecastContext)
            Dim CSAMADMISSION As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
            If context.HistoricMode Then
                For Each item In (From s In context.GetSlice("SeriesMalnChilSAMAdmission", "Earliest")
                                  Select Key = s.Key, Value = s.Value)
                    CSAMADMISSION.Add(item.Key, If(item.Value, -9999))
                Next
            Else
                For Each item In (From s In context.GetSlice("SeriesMalnChilSAMAdmission", BaseYear, CoalescePolicy.MostRecent) Select Key = s.Key, Value = s.Value)
                    CSAMADMISSION.Add(item.Key, If(item.Value, -9999))
                Next
            End If

            Dim SAMADMISSIONDict As Dictionary(Of Integer, Single)
            SAMADMISSIONDict = (From s In CSAMADMISSION
                                Join r In context.Regionalization On s.Key Equals r.Key
                                Group Value = s.Value By RegionId = r.Value Into Data = Group
       ).ToDictionary(
             Function(grp) grp.RegionId,
             Function(grp) grp.Data.Sum(Function(v) v.Value)
       )
            For Each reg In SAMADMISSIONDict
                SAMADMISSION(reg.Key) = reg.Value
            Next


            Using db As IFsContext = New IFsContext()
                'gvdb 2024/04/05
                'update for new EF syntax
                Dim valtblOECD = (From GI In db.GroupIDNewFull Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                  Where GI.GroupName = "OECD"
                                  Order By G.RegionId
                                  Select Key = G.RegionId, Value = G.RegionName).AsEnumerable()

                'Transformations and validations
                For ICount% = 1 To NR
                    If SAMADMISSION(ICount%) < 0 Then
                        If valtblOECD.Contains(New With {Key .Key = ICount, Key .Value = GRPS(1, ICount%)}) OrElse MALNCHPSAM(ICount%) < 0.05 Then
                            SAMADMISSION(ICount%) = 100
                        Else
                            SAMADMISSION(ICount%) = 0
                        End If
                    Else
                        'jrs 2016/02/10 convert to % (Admissions are in Thousands so need to convert to millions to match POP0TO5)
                        'jrs 2016/03/01 SAMADMISSION(ICount%) = SAMADMISSION(ICount%) / 1000 / ((1 + malnchpsamadj) * MALNCHPSAM(ICount%) / 100 * POP0TO5(ICount%) + SAMADMISSION(ICount%) / 1000 * SAMAVERTED(ICount%) / 100) * 100
                        SAMADMISSION(ICount%) = SAMADMISSION(ICount%) / 1000 / (malnchpsamadj * (MALNCHPSAM(ICount%) / 100 * POP0TO5(ICount%) + SAMADMISSION(ICount%) / 1000 * SAMAVERTED(ICount%) / 100)) * 100
                    End If

                    'validations
                    If SAMADMISSION(ICount%) > 100 Then
                        SAMADMISSION(ICount%) = 100
                    End If
                    If SAMADMISSION(ICount%) < 0 Then
                        SAMADMISSION(ICount%) = 0
                    End If
                Next ICount%
            End Using

        End Sub
        'jrs 2017/12/20
        Private Sub BuildSharesMatrix(ByRef AbelGrossIn As Single(), ByRef AbelGrossOut As Single())
            'This should be a matrix where all the sum of each row equals one
            Dim SharesMatrix(,) As Single
            ReDim SharesMatrix(NR, NR)
            Dim i As Integer, j As Integer, Tot As Single
            For i = 1 To NR
                Tot = 0
                For j = 1 To NR
                    Tot = Tot + XDYADMIG(i, j)
                    'jrs 2018/01/25 Tot = Tot + DYADMIGVOL(i, j)
                Next j
                'If GRPS(1, i) = "Mexico" And GRPS(1, j) = "United States of America" And FirstYear + IY - 1 >= 2048 Then Stop
                'If GRPS(1, i) = "Mexico" And FirstYear + IY - 1 >= 2048 Then Stop

                If Tot > 0 Then
                    For j = 1 To NR
                        SharesMatrix(i, j) = XDYADMIG(i, j) / Tot
                        'jrs 2018/01/25 SharesMatrix(i, j) = DYADMIGVOL(i, j) / Tot
                    Next j
                End If
            Next i

            'Then adjust the flow matrix with it
            For i = 1 To NR
                For j = 1 To NR
                    'jrs 2018/01/24 changed from Out(j) to Out(i)
                    'If i = 7 And j = 34 Then Stop
                    If AbelGrossOut(i) > 0 Then
                        XDYADMIG(i, j) = SharesMatrix(i, j) * AbelGrossOut(i)
                        'jrs 2018/01/25 DYADMIGVOL(i, j) = SharesMatrix(i, j) * AbelGrossOut(i)
                    Else
                        XDYADMIG(i, j) = 0
                        'jrs 2018/01/25 DYADMIGVOL(i, j) = 0
                    End If
                Next j
            Next i
        End Sub


        'jrs 2024/11/01
        'Instead of doing immigrants first, emigrants second or viceversa, let's compute the factors for both, and find the average
        Private Sub BalanceMigrationFlowsInAndOut(ByRef AbelGrossIn As Single(), ByRef AbelGrossOut As Single(), ByVal xmDiff As Single)
            Dim i As Integer, j As Integer
            Dim XAdjAvg As Double, XCount As Integer
            Dim MAdjAvg As Double, MCount As Integer
            Dim Factors(NR, NR, 3) As Single
            Dim MT As Single, XT As Single

            'jrs 2024/10/16 moved immigrants first, to give priority to emmigrants, so that shrinking countries like Albania get emmigrants right
            'Adjust immigrants first
            '--------------------
            Dim MTot(NR) As Single
            MT = 0
            For i = 1 To NR
                MT = MT + AbelGrossIn(i)
                For j = 1 To NR
                    MTot(j) = MTot(j) + XDYADMIG(i, j)
                    'jrs 2018/01/25 MTot(j) = MTot(j) + DYADMIGVOL(i, j)
                Next j
            Next i

            For i = 1 To NR
                If MTot(i) > 0 Then
                    MAdjAvg = MAdjAvg + AbelGrossIn(i) / MTot(i)
                    MCount = MCount + 1
                End If
                For j = 1 To NR
                    'If i = 70 And j = 108 And IY > 1 Then Stop
                    'Shift to match total M
                    If MTot(j) > 0 Then
                        Factors(i, j, 1) = CSng(AbelGrossIn(j) / MTot(j))
                        'jrs 2018/01/25 DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * AbelGrossIn(j) / MTot(j)
                    Else
                        Factors(i, j, 1) = 1
                    End If
                Next j
            Next i
            MAdjAvg = MAdjAvg / MCount

            'Match emigrants second
            '---------------------
            Dim XTOT(NR) As Single
            XAdjAvg = 0
            XCount = 0
            XT = 0
            For i = 1 To NR
                XT = XT + AbelGrossOut(i)
                For j = 1 To NR
                    XTOT(i) = XTOT(i) + XDYADMIG(i, j)
                    'jrs 2018/01/25 XTOT(i) = XTOT(i) + DYADMIGVOL(i, j)
                Next j
            Next i

            For i = 1 To NR
                'If GRPS(1, i) = "Bangladesh" And IY > 10 Then Stop
                If XTOT(i) > 0 Then
                    XAdjAvg = XAdjAvg + AbelGrossOut(i) / XTOT(i)
                    XCount = XCount + 1
                End If
                For j = 1 To NR
                    'Shift to match total X
                    If XTOT(i) > 0 Then
                        Factors(i, j, 2) = CSng(AbelGrossOut(i) / XTOT(i))
                    Else
                        Factors(i, j, 2) = 1
                    End If
                    'Compute the average
                    Factors(i, j, 3) = (Factors(i, j, 1) + Factors(i, j, 2)) / 2
                    'jrs 2018/01/25 DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * AbelGrossOut(i) / XTOT(i)
                Next j
            Next i
            XAdjAvg = XAdjAvg / XCount

            'jrs 2024/11/05
            Dim diff1 As Single, diff2 As Single, diff3 As Single, diffMax As Single, diffMin As Single

            'Apply the Average
            For i = 1 To NR
                For j = 1 To NR
                    'If i = 1 And j = 137 And xmDiff = 0 Then Stop
                    'If IY = 46 And i = 137 And j = 180 Then Stop
                    'If i = 112 And j = 74 And IY = 26 And xmDiff = 0 Then Stop
                    diff1 = XDYADMIG(i, j) * (Factors(i, j, 1) - 1)
                    diff2 = XDYADMIG(i, j) * (Factors(i, j, 2) - 1)
                    diff3 = XDYADMIG(i, j) * (Factors(i, j, 3) - 1)
                    If diff2 > diff1 Then
                        diffMax = diff2
                        diffMin = diff1
                    Else
                        diffMax = diff1
                        diffMin = diff2
                    End If
                    'Check if Origin Country i can keep sending more and Destination Country j can keep receiving
                    If AbelGrossOut(i) - AbelGrossIn(i) - XTOT(i) + MTot(i) >= diffMax And
                        (AbelGrossIn(j) - AbelGrossOut(j) - MTot(j) + XTOT(j) >= diffMax Or POP(j) / POP(i) > 10) And
                        diffMax >= 0 Then
                        XDYADMIG(i, j) = CSng(XDYADMIG(i, j) + diffMax)
                        XTOT(i) = XTOT(i) + diffMax
                        MTot(j) = MTot(j) + diffMax
                        'Check if Origin Country i needs less and Destination Country j needs less, unless j is a big country
                    ElseIf AbelGrossOut(i) - AbelGrossIn(i) - XTOT(i) + MTot(i) <= diffMin And
                        (AbelGrossIn(j) - AbelGrossOut(j) - MTot(j) + XTOT(j) <= diffMin Or POP(j) / POP(i) > 10) And
                        diffMin <= 0 Then
                        XDYADMIG(i, j) = CSng(XDYADMIG(i, j) + diffMin)
                        XTOT(i) = XTOT(i) + diffMin
                        MTot(j) = MTot(j) + diffMin
                    Else 'Use the average
                        XDYADMIG(i, j) = CSng(XDYADMIG(i, j) + diff3)
                        XTOT(i) = XTOT(i) + diff3
                        MTot(j) = MTot(j) + diff3
                    End If
                    'Remove those limits, let it go
                    'XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * Amin(AMAX(Factors(i, j, 3), 0.9), 1.1))
                    'If (MTot(i) * Factors(j, i, 3) - XTOT(i) * Factors(i, j, 3)) / (AbelGrossIn(i) - AbelGrossOut(i)) > 0 And
                    '    (MTot(j) * Factors(i, j, 3) - XTOT(j) * Factors(j, i, 3)) / (AbelGrossIn(j) - AbelGrossOut(j)) > 0 Then
                    '    'if desired total signs are achived apply average
                    '    XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * Factors(i, j, 3))
                    'ElseIf (MTot(j) * Factors(i, j, 3) - XTOT(j) * Factors(j, i, 3)) / (AbelGrossIn(j) - AbelGrossOut(j)) < 0 And
                    '    (MTot(j) * Factors(i, j, 1) - XTOT(j) * Factors(j, i, 3)) / (AbelGrossIn(j) - AbelGrossOut(j)) > 0 Then
                    '    'try with import factor
                    '    XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * Factors(i, j, 1))
                    'ElseIf (MTot(i) * Factors(j, i, 3) - XTOT(i) * Factors(i, j, 3)) / (AbelGrossIn(i) - AbelGrossOut(i)) < 0 And
                    '    (MTot(i) * Factors(j, i, 3) - XTOT(i) * Factors(i, j, 2)) / (AbelGrossIn(i) - AbelGrossOut(i)) > 0 Then
                    '    'try with export factor
                    '    XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * Factors(i, j, 2))
                    'ElseIf (MTot(j) * Factors(i, j, 3) - XTOT(j) * Factors(j, i, 3)) / (AbelGrossIn(j) - AbelGrossOut(j)) < 0 And
                    '    ((XTOT(j) * Factors(j, i, 3) > MTot(j) * Factors(i, j, 3) And Factors(i, j, 1) > Factors(i, j, 3)) Or
                    '    (XTOT(i) * Factors(i, j, 3) < MTot(i) * Factors(j, i, 3) And Factors(i, j, 1) < Factors(i, j, 3))) Then
                    '    'try with import factor even if it's not enough
                    '    XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * Factors(i, j, 1))
                    'ElseIf (MTot(i) * Factors(j, i, 3) - XTOT(i) * Factors(i, j, 3)) / (AbelGrossIn(i) - AbelGrossOut(i)) < 0 And
                    '    ((XTOT(i) * Factors(i, j, 3) > MTot(i) * Factors(j, i, 3) And Factors(i, j, 2) < Factors(i, j, 3)) Or
                    '    (XTOT(i) * Factors(i, j, 3) < MTot(i) * Factors(j, i, 3) And Factors(i, j, 2) > Factors(i, j, 3))) Then
                    '    'try with export factor even if it's not enough
                    '    XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * Factors(i, j, 2))
                    'Else
                    '    'Back to using some number in between
                    '    'If XDYADMIG(i, j) > 0 And Abs(AbelGrossIn(j) - AbelGrossOut(j)) > 0 And Abs(AbelGrossIn(i) - AbelGrossOut(i)) > 0 Then Stop
                    '    XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * Factors(i, j, 3))
                    'End If
                Next
            Next

            'Recompute Emigrants just for check
            '----------------------------------
            '    XAdjAvg = 0
            '    XCount = 0
            '    ReDim XTOT(NR)
            '    For i = 1 To NR
            '        For j = 1 To NR
            '            XTOT(i) = XTOT(i) + DYADMIGVOL(i, j)
            '        Next j
            '    Next i
            '
            '    For i = 1 To NR
            '        If XTOT(i) > 0 Then
            '            XAdjAvg = XAdjAvg + AbelGrossOut(i) / XTOT(i)
            '            XCount = XCount + 1
            '        End If
            '    Next i
            '    XAdjAvg = XAdjAvg / XCount

            'jrs 2017/12/21 added or xmDiff < 0.02
            'jrs 2018/02/16 If (XAdjAvg > 0.999 And XAdjAvg < 1.001 And MAdjAvg > 0.999 And MAdjAvg < 1.001) Or Abs(XAdjAvg - MAdjAvg) >= xmDiff Then
            'jrs 2018/02/20 If (XAdjAvg > 0.999 And XAdjAvg < 1.001 And MAdjAvg > 0.999 And MAdjAvg < 1.001) Then
            'jrs 2024/10/16 If xmDiff > 10 Then
            If xmDiff > 20 Then
                Exit Sub
            Else
                'jrs 2018/02/20 BalanceMigrationFlowsInAndOut AbelGrossIn, AbelGrossOut, Abs(XAdjAvg - MAdjAvg)
                BalanceMigrationFlowsInAndOut(AbelGrossIn, AbelGrossOut, xmDiff + 1)
            End If

        End Sub

        'jrs 2017/12/19
        Private Sub BalanceMigrationFlowsInAndOutOld(ByRef AbelGrossIn As Single(), ByRef AbelGrossOut As Single(), ByVal xmDiff As Single)
            Dim i As Integer, j As Integer
            Dim XAdjAvg As Double, XCount As Integer
            Dim MAdjAvg As Double, MCount As Integer

            'jrs 2024/10/16 moved immigrants first, to give priority to emmigrants, so that shrinking countries like Albania get emmigrants right
            'Adjust immigrants first
            '--------------------
            Dim MTot(NR) As Single
            For i = 1 To NR
                For j = 1 To NR
                    MTot(j) = MTot(j) + XDYADMIG(i, j)
                    'jrs 2018/01/25 MTot(j) = MTot(j) + DYADMIGVOL(i, j)
                Next j
            Next i

            For i = 1 To NR
                If MTot(i) > 0 Then
                    MAdjAvg = MAdjAvg + AbelGrossIn(i) / MTot(i)
                    MCount = MCount + 1
                End If
                For j = 1 To NR
                    'If i = 70 And j = 108 And IY > 1 Then Stop
                    'Shift to match total M
                    If MTot(j) > 0 Then
                        XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * Amin(AMAX(AbelGrossIn(j) / MTot(j), 0.9), 1.1))
                        'jrs 2018/01/25 DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * AbelGrossIn(j) / MTot(j)
                    End If
                Next j
            Next i
            MAdjAvg = MAdjAvg / MCount

            'Match emigrants second
            '---------------------
            Dim XTOT(NR) As Single
            XAdjAvg = 0
            XCount = 0
            For i = 1 To NR
                For j = 1 To NR
                    XTOT(i) = XTOT(i) + XDYADMIG(i, j)
                    'jrs 2018/01/25 XTOT(i) = XTOT(i) + DYADMIGVOL(i, j)
                Next j
            Next i

            For i = 1 To NR
                'If GRPS(1, i) = "Bangladesh" And IY > 10 Then Stop
                If XTOT(i) > 0 Then
                    XAdjAvg = XAdjAvg + AbelGrossOut(i) / XTOT(i)
                    XCount = XCount + 1
                End If
                For j = 1 To NR
                    'Shift to match total X
                    If XTOT(i) > 0 Then
                        XDYADMIG(i, j) = CSng(XDYADMIG(i, j) * Amin(AMAX(AbelGrossOut(i) / XTOT(i), 0.9), 1.1))
                        'jrs 2018/01/25 DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * AbelGrossOut(i) / XTOT(i)
                    End If
                Next j
            Next i
            XAdjAvg = XAdjAvg / XCount


            'Recompute Emigrants just for check
            '----------------------------------
            '    XAdjAvg = 0
            '    XCount = 0
            '    ReDim XTOT(NR)
            '    For i = 1 To NR
            '        For j = 1 To NR
            '            XTOT(i) = XTOT(i) + DYADMIGVOL(i, j)
            '        Next j
            '    Next i
            '
            '    For i = 1 To NR
            '        If XTOT(i) > 0 Then
            '            XAdjAvg = XAdjAvg + AbelGrossOut(i) / XTOT(i)
            '            XCount = XCount + 1
            '        End If
            '    Next i
            '    XAdjAvg = XAdjAvg / XCount

            'jrs 2017/12/21 added or xmDiff < 0.02
            'jrs 2018/02/16 If (XAdjAvg > 0.999 And XAdjAvg < 1.001 And MAdjAvg > 0.999 And MAdjAvg < 1.001) Or Abs(XAdjAvg - MAdjAvg) >= xmDiff Then
            'jrs 2018/02/20 If (XAdjAvg > 0.999 And XAdjAvg < 1.001 And MAdjAvg > 0.999 And MAdjAvg < 1.001) Then
            'jrs 2024/10/16 If xmDiff > 10 Then
            If xmDiff > 20 Then
                Exit Sub
            Else
                'jrs 2018/02/20 BalanceMigrationFlowsInAndOut AbelGrossIn, AbelGrossOut, Abs(XAdjAvg - MAdjAvg)
                BalanceMigrationFlowsInAndOut(AbelGrossIn, AbelGrossOut, xmDiff + 1)
            End If

        End Sub

        'jrs 2017/11/24
        Private Sub AdjustNetMigration()
            'gvdb 2024/04/05
            'unused variable
            Dim i As Integer, j As Integer, TotVal As Single ' k As Integer,
            'Immigrants
            Dim TotImmigrants(NR) As Single
            For i = 1 To NR
                TotImmigrants(i) = 0
                For j = 1 To NR
                    'jrs 2018/01/25
                    TotImmigrants(i) = TotImmigrants(i) + XDYADMIG(j, i)
                    'TotImmigrants(i) = TotImmigrants(i) + DYADMIGVOL(j, i)
                Next j
            Next i
            'Emigrants
            For i = 1 To NR
                'If i = 137 Then Stop
                TotVal = 0
                For j = 1 To NR
                    'jrs 2018/01/25
                    TotVal = TotVal + XDYADMIG(i, j)
                    'TotVal = TotVal + DYADMIGVOL(i, j)
                Next j

                'jrs 2018/01/04
                'If i = 162 Then Stop
                'If GRPS(1, i) = "Latvia" And FirstYear + IY - 1 >= 2045 Then Stop
                'Adding a cap to outward migration to avoid depletion of country, like Bosnia in 2086
                'Syria in 2016 already has a 4%, now trying to cap outward if net is more than 1% of population, net for Syria is 3.1% out
                'jrs 2018/01/31 If TotVal > 0.01 * POP(i) * 1000000 Then
                Dim Fact As Single, Target As Single
                '        If (TotVal - TotImmigrants(i)) > 0.035 * POP(i) * 1000000 Then
                '            Fact = (0.035 * POP(i) * 1000000 + TotImmigrants(i)) / TotVal
                '            TotVal = 0.035 * POP(i) * 1000000 + TotImmigrants(i)
                '            'jrs 2018/01/29
                '            'Adjust all Emigrants
                '            For j = 1 To NR
                '                DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * Fact
                '                SDYADMIGVOL(i, j) = DYADMIGVOL(i, j)
                '            Next j
                'Else

                'If i = 152 Then Stop
                'jrs 2018/04/03
                'Need to leave at least some population in fertil age
                If TotVal > 0.25 * sPOP15TO65(i) * 1000000 * (1 - SPopForeignIn(i) / POP(i)) Then
                    Fact = CSng((0.25 * sPOP15TO65(i) * 1000000 * (1 - SPopForeignIn(i) / POP(i))) / TotVal)
                    TotVal = CSng(0.25 * sPOP15TO65(i) * 1000000 * (1 - SPopForeignIn(i) / POP(i)))

                    'Adjust all Emigrants
                    For j = 1 To NR
                        'jrs 2018/03/01
                        DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * Fact
                        SDYADMIGVOL(i, j) = DYADMIGVOL(i, j)

                        DYADMIGFORCED(i, j) = DYADMIGFORCED(i, j) * Fact
                        SDYADMIGFORCED(i, j) = DYADMIGFORCED(i, j)

                        DYADMIGFORCEDRETURN(i, j) = DYADMIGFORCEDRETURN(i, j) * Fact
                        SDYADMIGFORCEDRETURN(i, j) = DYADMIGFORCEDRETURN(i, j)

                        XDYADMIG(i, j) = XDYADMIG(i, j) * Fact
                    Next j

                End If

                If IY = 2 Then
                    'If GRPS(1, i) = "Qatar" Then Stop
                    AdjustFstYr(i) = 1
                    AdjustBaseVal(i) = (TotVal - TotImmigrants(i)) / (sPOP15TO65(i) * 1000000)
                End If
                If (TotVal - TotImmigrants(i)) > 0.025 * sPOP15TO65(i) * 1000000 Then   'jrs 2018/01/31
                    'If i = 162 Then Stop
                    If AdjustBaseVal(i) > 0.025 Then
                        Target = ConvergeOverTimeStartX(AdjustBaseVal(i), 0.025, 5, CInt(AdjustFstYr(i)))
                    Else
                        Target = 0.025
                    End If
                    Fact = (Target * sPOP15TO65(i) * 1000000 + TotImmigrants(i)) / TotVal
                    TotVal = Target * sPOP15TO65(i) * 1000000 + TotImmigrants(i)
                    'Adjust all Emigrants
                    For j = 1 To NR
                        'jrs 2018/03/01
                        DYADMIGVOL(i, j) = DYADMIGVOL(i, j) * Fact
                        SDYADMIGVOL(i, j) = DYADMIGVOL(i, j)

                        DYADMIGFORCED(i, j) = DYADMIGFORCED(i, j) * Fact
                        SDYADMIGFORCED(i, j) = DYADMIGFORCED(i, j)

                        DYADMIGFORCEDRETURN(i, j) = DYADMIGFORCEDRETURN(i, j) * Fact
                        SDYADMIGFORCEDRETURN(i, j) = DYADMIGFORCEDRETURN(i, j)

                        XDYADMIG(i, j) = XDYADMIG(i, j) * Fact
                    Next j
                Else
                    If sPOP15TO65(i) > 0 Then
                        AdjustBaseVal(i) = (TotVal - TotImmigrants(i)) / (sPOP15TO65(i) * 1000000)
                        AdjustFstYr(i) = IY
                    End If
                End If
                TotVal = TotImmigrants(i) - TotVal
                MIGRANTS(i) = (1 - migratebalancesw) * MIGRANTS(i) + migratebalancesw * TotVal / 1000000
                MIGRATE(i) = MIGRANTS(i) / POP(i) * 100 'jrs 2018/04/04
            Next i
        End Sub

        'dkb 2018/06/02 - Breaking chunk out to its own procedure in order to obtain SMIGFORCEDEST after calculation of GOVINDSECUR
        Public Sub ForcedMigEstimate()
            ReDim SMIGFORCEDEST(NR)
            Dim i As Integer
            For i = 1 To NR
                SMIGFORCEDEST(i) = CSng(Exp(3.469 - 12.8129 * GOVINDSECUR(i)) * POP(i) / 100)
            Next i
        End Sub

        ' List of Groups and Fixed Effects being applied
        '1750    Afr -Eastern -0.7239454
        '1746    Afr -Northern -1.090974
        '1743    Afr -Southern -0.6365409
        '1742    Afr -Western -0.747674
        '1739    Amer -Carib -0.1862653
        '1738    Amer -Central -0.4343961
        '1768    Amer -North -0.5708562
        '1734    Amer -South -0.6401727
        '1731    Asia -Central -1.006453
        '1730    Asia -Eastern -1.201497
        '1726    Asia -Southeastern - 0.6908326
        '1727    Asia -Southern -1.083672
        '1767    Asia -Western -1.28088
        '1723    Europe -Eastern -0.6573148
        '1720    Europe -Northern -0.663181
        '1717    Europe -Southern -0.9434322
        '1774    Europe -Western - 0.7235794
        '1705    Oceania-Aus&NZ  -0.4334822
        Sub CalculateFemaleLeadership(R%, C As Single(), GDI As Single(), EDYRSAG15 As Single(,), group As String, context As IForecastContext) 'cdv 2022/11/18
            Dim SubRegionFixedEffect As Double
            Select Case group
                Case "Afr-Eastern" 'See above for what group each number goes to
                    SubRegionFixedEffect = -0.724
                Case "Afr-Northern"
                    SubRegionFixedEffect = -1.091
                Case "Afr-Southern"
                    SubRegionFixedEffect = -0.637
                Case "Afr-Western"
                    SubRegionFixedEffect = -0.748
                Case "Amer-Carib"
                    SubRegionFixedEffect = -0.186
                Case "Amer-Central"
                    SubRegionFixedEffect = -0.434
                Case "Amer-North"
                    SubRegionFixedEffect = -0.571
                Case "Amer-South"
                    SubRegionFixedEffect = -0.64
                Case "Asia-Central"
                    SubRegionFixedEffect = -1.006
                Case "Asia-East"
                    SubRegionFixedEffect = -1.201
                Case "Asia-SoEast"
                    SubRegionFixedEffect = -0.691
                Case "Asia-South"
                    SubRegionFixedEffect = -1.084
                Case "Asia-West"
                    SubRegionFixedEffect = -1.281
                Case "Eur-East"
                    SubRegionFixedEffect = -0.657
                Case "Eur-North"
                    SubRegionFixedEffect = -0.663
                Case "Eur-South"
                    SubRegionFixedEffect = -0.943
                Case "Eur-West"
                    SubRegionFixedEffect = -0.724
                Case "Oceania"
                    SubRegionFixedEffect = -0.433
                Case "Afr-Middle"
                    SubRegionFixedEffect = 0
                Case Else
                    SubRegionFixedEffect = 0
            End Select

            If IY = 1 Then
                If WOMENINLEADERSHIP(R%) = 0 Then
                    Call AnalFunc("WomenInLeadershipRoles v. EDYRS15f and Hhcon%GDP and GDI", New Single() {EDYRSAG15(R%, Female), C(R%) / GDP(R%) * 100, GDI(R%)}, WOMENINLEADERSHIP(R%), New String() {"Educ15", "Hhcon%GDP", "GDI"}, context)
                    WOMENINLEADERSHIP(R%) = CSng(Exp(WOMENINLEADERSHIP(R%) + SubRegionFixedEffect) / (1 + Exp(WOMENINLEADERSHIP(R%) + SubRegionFixedEffect))) * 100
                End If
                If WOMENINLEADERSHIP(R%) > 100 Then
                    WOMENINLEADERSHIP(R%) = 100
                ElseIf WOMENINLEADERSHIP(R%) < 0 Then
                    WOMENINLEADERSHIP(R%) = 0
                End If
                ChainedLastYearWomenLeadership(R%) = WOMENINLEADERSHIP(R%)
                Call AnalFunc("WomenInLeadershipRoles v. EDYRS15f and Hhcon%GDP and GDI", New Single() {EDYRSAG15(R%, Female), C(R%) / GDP(R%) * 100, GDI(R%)}, UnchainedLastYearWomenLeadership(R%), New String() {"Educ15", "Hhcon%GDP", "GDI"}, context)
                UnchainedLastYearWomenLeadership(R%) = CSng(Exp(UnchainedLastYearWomenLeadership(R%) + SubRegionFixedEffect) / (1 + Exp(UnchainedLastYearWomenLeadership(R%) + SubRegionFixedEffect))) * 100
            Else
                Dim WOMENINLEADERSHIPForecasted As Single
                Call AnalFunc("WomenInLeadershipRoles v. EDYRS15f and Hhcon%GDP and GDI", New Single() {EDYRSAG15(R%, Female), C(R%) / GDP(R%) * 100, GDI(R%)}, WOMENINLEADERSHIPForecasted, New String() {"Educ15", "Hhcon%GDP", "GDI"}, context)
                WOMENINLEADERSHIPForecasted = CSng(Exp(WOMENINLEADERSHIPForecasted + SubRegionFixedEffect) / (1 + Exp(WOMENINLEADERSHIPForecasted + SubRegionFixedEffect))) * 100
                WOMENINLEADERSHIP(R%) = ChainedLastYearWomenLeadership(R%) + (WOMENINLEADERSHIPForecasted - UnchainedLastYearWomenLeadership(R%))
                If WOMENINLEADERSHIP(R%) > 100 Then
                    WOMENINLEADERSHIP(R%) = 100
                ElseIf WOMENINLEADERSHIP(R%) < 0 Then
                    WOMENINLEADERSHIP(R%) = 0
                End If
                ChainedLastYearWomenLeadership(R%) = WOMENINLEADERSHIP(R%)
                UnchainedLastYearWomenLeadership(R%) = WOMENINLEADERSHIPForecasted

                'jrs 2023/03/07
                'WOMENINLEADERSHIP(R%) = WOMENINLEADERSHIP(R%) * womeninleadershipm(R%) * womeninleadershipm(R%)

            End If
        End Sub
        Sub CalculateFoodInsecurity(R%, maln As Single(), HDIPRE2010 As Single(,), group As String, context As IForecastContext) 'cdv 2022/12/12
            Dim SubRegionFixedEffect() As Double
            ReDim SubRegionFixedEffect(2)
            SubRegionFixedEffect(Male) = getFoodInsecuritySubRegionFixedEffect(R%, group, "Male")
            SubRegionFixedEffect(Female) = getFoodInsecuritySubRegionFixedEffect(R%, group, "Female")
            If IY = 1 Then
                If FOODINSECURITY(R%, Male) = 0 Then
                    Call AnalFunc("FIES_m V. Maln and hdi_m (Log) (All Most Recent)", New Single() {maln(R%), HDIPRE2010(R%, Male)}, FOODINSECURITY(R%, Male), New String() {"Maln", "HDI_m"}, context)
                    FOODINSECURITY(R%, Male) = CSng((Exp(FOODINSECURITY(R%, Male) + SubRegionFixedEffect(Male))) / (1 + Exp(FOODINSECURITY(R%, Male) + SubRegionFixedEffect(Male)))) * 100
                End If

                If FOODINSECURITY(R%, Female) = 0 Then
                    Call AnalFunc("FIES_f V. Maln and hdi_f (Log) (All Most Recent)", New Single() {maln(R%), HDIPRE2010(R%, Female)}, FOODINSECURITY(R%, Female), New String() {"Maln", "HDI_f"}, context)
                    FOODINSECURITY(R%, Female) = CSng((Exp(FOODINSECURITY(R%, Female) + SubRegionFixedEffect(Female))) / (1 + Exp(FOODINSECURITY(R%, Female) + SubRegionFixedEffect(Female)))) * 100
                End If

                If FOODINSECURITY(R%, Male) > 100 Then
                    FOODINSECURITY(R%, Male) = 100
                End If
                If FOODINSECURITY(R%, Female) > 100 Then
                    FOODINSECURITY(R%, Female) = 100
                End If


                If FOODINSECURITY(R%, Male) < 0 Then
                    FOODINSECURITY(R%, Male) = 0
                End If
                If FOODINSECURITY(R%, Female) < 0 Then
                    FOODINSECURITY(R%, Female) = 0
                End If
                FOODINSECURITY(R%, Total) = (FOODINSECURITY(R%, Female) + FOODINSECURITY(R%, Male)) / 2

                ChainedLastYearFoodInsecurity(R%, Male) = FOODINSECURITY(R%, Male)
                ChainedLastYearFoodInsecurity(R%, Female) = FOODINSECURITY(R%, Female)

                Call AnalFunc("FIES_m V. Maln and hdi_m (Log) (All Most Recent)", New Single() {maln(R%), HDIPRE2010(R%, Male)}, UnchainedLastYearFoodInsecurity(R%, Male), New String() {"Maln", "HDI_m"}, context)
                Call AnalFunc("FIES_f V. Maln and hdi_f (Log) (All Most Recent)", New Single() {maln(R%), HDIPRE2010(R%, Female)}, UnchainedLastYearFoodInsecurity(R%, Female), New String() {"Maln", "HDI_f"}, context)
                UnchainedLastYearFoodInsecurity(R%, Male) = CSng(Exp(UnchainedLastYearFoodInsecurity(R%, Male) + SubRegionFixedEffect(Male)) / (1 + Exp(UnchainedLastYearFoodInsecurity(R%, Male) + SubRegionFixedEffect(Male)))) * 100
                UnchainedLastYearFoodInsecurity(R%, Female) = CSng(Exp(UnchainedLastYearFoodInsecurity(R%, Female) + SubRegionFixedEffect(Female)) / (1 + Exp(UnchainedLastYearFoodInsecurity(R%, Female) + SubRegionFixedEffect(Female)))) * 100
            Else
                Dim FoodInsecurityForecasted(2) As Single
                Dim FoodInsecurityShiftRatio As Single
                Call AnalFunc("FIES_m V. Maln and hdi_m (Log) (All Most Recent)", New Single() {maln(R%), HDIPRE2010(R%, Male)}, FoodInsecurityForecasted(Male), New String() {"Maln", "HDI_m"}, context)
                Call AnalFunc("FIES_f V. Maln and hdi_f (Log) (All Most Recent)", New Single() {maln(R%), HDIPRE2010(R%, Female)}, FoodInsecurityForecasted(Female), New String() {"Maln", "HDI_f"}, context)

                FoodInsecurityForecasted(Male) = CSng(Exp(FoodInsecurityForecasted(Male) + SubRegionFixedEffect(Male)) / (1 + Exp(FoodInsecurityForecasted(Male) + SubRegionFixedEffect(Male)))) * 100
                FoodInsecurityForecasted(Female) = CSng(Exp(FoodInsecurityForecasted(Female) + SubRegionFixedEffect(Female)) / (1 + Exp(FoodInsecurityForecasted(Female) + SubRegionFixedEffect(Female)))) * 100

                FoodInsecurityShiftRatio = FoodInsecurityForecasted(Male) / UnchainedLastYearFoodInsecurity(R%, Male)
                FOODINSECURITY(R%, Male) = ChainedLastYearFoodInsecurity(R%, Male) * FoodInsecurityShiftRatio

                FoodInsecurityShiftRatio = FoodInsecurityForecasted(Female) / UnchainedLastYearFoodInsecurity(R%, Female)
                FOODINSECURITY(R%, Female) = ChainedLastYearFoodInsecurity(R%, Female) * FoodInsecurityShiftRatio

                'jrs 2023/03/07
                'FOODINSECURITY(R%, Male) = FOODINSECURITY(R%, Male) * foodinsecuritym(R%, Male) * foodinsecuritym(R%, Total)

                If FOODINSECURITY(R%, Male) > 100 Then
                    FOODINSECURITY(R%, Male) = 100
                End If

                'jrs 2023/03/07
                'FOODINSECURITY(R%, Female) = FOODINSECURITY(R%, Female) * foodinsecuritym(R%, Female) * foodinsecuritym(R%, Total)

                If FOODINSECURITY(R%, Female) > 100 Then
                    FOODINSECURITY(R%, Female) = 100
                End If

                If FOODINSECURITY(R%, Male) < 0 Then
                    FOODINSECURITY(R%, Male) = 0
                End If

                If FOODINSECURITY(R%, Female) < 0 Then
                    FOODINSECURITY(R%, Female) = 0
                End If
                FOODINSECURITY(R%, Total) = (FOODINSECURITY(R%, Female) + FOODINSECURITY(R%, Male)) / 2

                ChainedLastYearFoodInsecurity(R%, Male) = FOODINSECURITY(R%, Male)
                ChainedLastYearFoodInsecurity(R%, Female) = FOODINSECURITY(R%, Female)

                UnchainedLastYearFoodInsecurity(R%, Male) = FoodInsecurityForecasted(Male)
                UnchainedLastYearFoodInsecurity(R%, Female) = FoodInsecurityForecasted(Female)
            End If
        End Sub

        Function getFoodInsecuritySubRegionFixedEffect(R%, group As String, gender As String) As Double
            Dim SubRegionFixedEffect As Single
            If gender = "Male" Then
                Select Case group
                    Case "Afr-Eastern" 'See above for what group each number goes to
                        SubRegionFixedEffect = -0.686
                    Case "Afr-Northern"
                        SubRegionFixedEffect = -0.964
                    Case "Afr-Southern"
                        SubRegionFixedEffect = -0.321
                    Case "Afr-Western"
                        SubRegionFixedEffect = -0.459
                    Case "Amer-Carib"
                        SubRegionFixedEffect = -0.434
                    Case "Amer-Central"
                        SubRegionFixedEffect = -0.749
                    Case "Amer-North"
                        SubRegionFixedEffect = -0.89
                    Case "Amer-South"
                        SubRegionFixedEffect = -0.774
                    Case "Asia-Central"
                        SubRegionFixedEffect = -1.414
                    Case "Asia-East"
                        SubRegionFixedEffect = -1.413
                    Case "Asia-SoEast"
                        SubRegionFixedEffect = -0.909
                    Case "Asia-South"
                        SubRegionFixedEffect = -0.861
                    Case "Asia-West"
                        SubRegionFixedEffect = -1.02
                    Case "Eur-East"
                        SubRegionFixedEffect = -1.664
                    Case "Eur-North"
                        SubRegionFixedEffect = -1.693
                    Case "Eur-South"
                        SubRegionFixedEffect = -1.345
                    Case "Eur-West"
                        SubRegionFixedEffect = -2.17
                    Case "Oceania"
                        SubRegionFixedEffect = -1.178
                    Case "Afr-Middle"
                        SubRegionFixedEffect = 0
                    Case Else
                        SubRegionFixedEffect = 0
                End Select
            Else
                Select Case group
                    Case "Afr-Eastern" 'See above for what group each number goes to
                        SubRegionFixedEffect = -0.498
                    Case "Afr-Northern"
                        SubRegionFixedEffect = -0.831
                    Case "Afr-Southern"
                        SubRegionFixedEffect = -0.019
                    Case "Afr-Western"
                        SubRegionFixedEffect = -0.45
                    Case "Amer-Carib"
                        SubRegionFixedEffect = 0.04
                    Case "Amer-Central"
                        SubRegionFixedEffect = -0.379
                    Case "Amer-North"
                        SubRegionFixedEffect = -0.657
                    Case "Amer-South"
                        SubRegionFixedEffect = -0.427
                    Case "Asia-Central"
                        SubRegionFixedEffect = -1.226
                    Case "Asia-East"
                        SubRegionFixedEffect = -1.39
                    Case "Asia-SoEast"
                        SubRegionFixedEffect = -0.704
                    Case "Asia-South"
                        SubRegionFixedEffect = -0.876
                    Case "Asia-West"
                        SubRegionFixedEffect = -0.876
                    Case "Eur-East"
                        SubRegionFixedEffect = -1.289
                    Case "Eur-North"
                        SubRegionFixedEffect = -1.492
                    Case "Eur-South"
                        SubRegionFixedEffect = -1.084
                    Case "Eur-West"
                        SubRegionFixedEffect = -2.122
                    Case "Oceania"
                        SubRegionFixedEffect = -0.951
                    Case "Afr-Middle"
                        SubRegionFixedEffect = 0
                    Case Else
                        SubRegionFixedEffect = 0
                End Select
            End If
            getFoodInsecuritySubRegionFixedEffect = SubRegionFixedEffect
        End Function

    End Module

End Namespace
