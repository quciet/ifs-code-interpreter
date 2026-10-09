Imports IFs.Data
Imports Microsoft.SqlServer
Imports QuickGraph.Graphviz

Namespace Models

    Partial Public Module Energy
        '=============================  Energy.BAS  ==========================
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '
        '
        '     Copyright by Barry B. Hughes 1988-2012
        '     Model code available for review and education only
        '     Reproduction or incorporation in other software by permission only
        '
        '
        '     ENERGY MODEL
        '
        '
        '     ENERGY CATEGORIES:  1=Oil; 2=Gas; 3=Coal; 4=Hydro; 5=Nuclear;
        '                         6=Other Renewable; 7=Unconventional Oil
        '
        '     NER% = 4 Number of conventional energy types with resource bases
        '     NEU% = 6 Number of energy types with resources, including unconventional, renew
        '     NEP% = 6 Number of conventional energy production types, total
        '     NET% = 7 Number of conventional and unconventional energy types, total
        '     All are specified in INIT
        'jrs 2013/04/23
        '     We have removed category 7=Unconventional Oil and we have added a set of
        '     Unconventional Energy variables with different categories, not only for Oil
        '     NET% = 6
        '     NER% = 6

        '     Energy Contstant Definitions
        '     ----------------------------

        '     Oil = 1, Gas = 2, Coal = 3, Hydro = 4, Nucl = 5, Renew = 6, Synth = 7
        Dim CompEnDem As Single
        'jrs 2009/05/13
        Dim SICTBroadExp() As Single
        'Dim FirstYearAdj() As Single
        'jrs 2009/05/25
        Dim ICTBroadExpShift() As Single
        Dim ICTSpendExpShift() As Single
        Dim GovRegQualShift() As Single
        Dim iMTARIFFTAXREn() As Single 'bbh 2019/10/12
        Dim SENP_UpBnd(,) As Single 'bbh 2021/09/30
        Public SENPTOT() As Single 'jrs 2023/10/25
        'jrs 2023/11/10
        Public SENPRENEW() As Single

        'dkb 2014/08/06 - Adding save variables for Energy Trade componenets of POWER - START
        Public SENX(), SENM() As Single
        'dkb 2014/08/06 - END

        'clp 2016/08/24 - Adding save variable for Energy Production compenent of power
        Public EnTotal(), SEnTotal() As Single

        'mti 2024/08/20 start
        Dim svreshare() As Single
        'mti 2024/08/20 end

        Dim sqe(,), BQE(,) As Single
        Dim IQESynConRat() As Single 'JRS 2013/04/19 IQE(NR%, NET%) NOT BEING USED
        Dim SmoothENP(,) As Single, smoothentot() As Single, smoothpendem() As Single, smoothtradebase() As Single
        Dim SmoothXEnBase() As Single
        Dim senst() As Single, XKAVE() As Single, mkave() As Single, mkavei() As Single, SImportDev() As Single
        Dim SENPRI() As Single, ENPRII() As Single ', SENPR() As Single
        Dim sken(,) As Single, tineed() As Single, SCSF() As Single  'jrs 2021/11/15 changed to sken
        Dim SSTF() As Single, CUMPR(,) As Single, sreser(,) As Single, SResProdR(,) As Single
        Dim eninvri(,) As Single, wunro() As Single, sidsf() As Single, SIDSFI() As Single
        Dim renpri() As Single, sENPCAPUT() As Single, SIENP(,) As Single, ineed(,) As Single 'jrs 2021/11/15 changed to sENPCAPUT
        'bbh 2011/10/17
        Dim SPRIF() As Single, sineed(,) As Single, sgdpp() As Single, SGDP() As Single ', sgdpenr() As Single
        Dim sendeminvr() As Single
        Dim EnReturnI() As Single, enreturnsi(,) As Single, SEProfitR() As Single, eprofitrs(,) As Single
        Dim XKAVMAX() As Single, MKAVMax() As Single, MarkUpEn() As Single, stbasef() As Single
        Dim XPriceTermLag() As Single, MPriceTermLag() As Single, cartaxenpriadd() As Single
        Dim SINSTPEn() As Single, emtax() As Single, ReducedOilProd() As Single, SReducedOilProd() As Single
        Dim ReducedGasProd() As Single, SReducedGasProd() As Single, sensho() As Single, pendem() As Single
        Dim SExRate() As Single, zsri() As Single
        Dim compendemperuniti() As Single, actendemperuniti() As Single, endefsurp() As Single
        Dim SWENP() As Single, invreduc() As Single, enppot(,) As Single, igdprppp() As Single
        Dim EnpCoalDemRI() As Single, EnpHydroDemRI() As Single, endemba() As Single, Exporter() As Single
        Dim SQEUC(,) As Single, BQEUC(,) As Single
        'bbh 2013/06/09
        Dim ResorTot(,) As Single
        'moved from 1FirstYear-------
        Dim iwep As Single
        Dim iwepbyear As Single
        Dim swep As Single
        Dim WorldKen As Single = 0
        Dim WorldEnp As Single = 0
        Dim wkenenpri As Single = 0
        Dim SEnCartPP As Single = 0
        Dim TENP As Single = 0
        Dim TENPRI As Single = 0
        'ttt Maybe Global?
        Dim SWENDEMR As Single = 0
        Dim Swkenenpr As Single = 0
        Dim IShrRenew() As Single 'clp 2024/08/07 Adding ability to have proportional reductions in exports as renewable energy becomes more prominent

        'jrs 2024/06/12
        Dim TbNameEnergyDemand As String

        'End moved from 1FirstYear-------
        'End ttt More
        Public Sub IFsEn1FirstYear(context As IForecastContext)
            sErrorSub = "IFsEn1"
            '
            '     FIRST TIME STEP ONLY - FIRST YEAR INITIALIZATIONS
            '     --------------------------------------------------
            '
            'mti 2024/08/20 start
            ReDim svreshare(NR%)
            'mti 2024/08/20 end
            ReDim sqe(NR%, NET), BQE(NR%, NET), IQESynConRat(NR%) 'JRS 2013/04/19 IQE(NR%, NET%) NOT BEING USED
            ReDim SmoothENP(NR%, NEP%), smoothentot(NR%), smoothpendem(NR%), smoothtradebase(NR%)
            ReDim SmoothXEnBase(NR%) 'bbh 2019/08/17
            ReDim senst(NR%), XKAVE(NR%), mkave(NR%), mkavei(NR%), SImportDev(NR%)
            ReDim SENPRI(NR%), ENPRII(NR%) ', SENPR(NR%)
            ReDim sken(NR%, NEP%), tineed(NR%), SCSF(NR%)  'jrs 2021/11/15 changed to sken
            ReDim SSTF(NR%), CUMPR(NR%, NET), sreser(NR%, NER%), SResProdR(NR%, NER%)
            ReDim eninvri(NR%, NEP%), wunro(NER%), sidsf(NR%), SIDSFI(NR%)
            ReDim renpri(NR%), sENPCAPUT(NR%), SIENP(NR%, NEP%), ineed(NR%, NEP%) 'jrs 2021/11/15
            'bbh 2011/10/17
            ReDim SPRIF(NR%), sineed(NR%, NEP%), sgdpp(NR%), SGDP(NR%), GDPPCI(NR%) ', sgdpenr(NR%)
            ReDim sendeminvr(NR%)
            ReDim EnReturnI(NR%), enreturnsi(NR%, NEP%), SEProfitR(NR%), eprofitrs(NR%, NET)
            ReDim XKAVMAX(NR%), MKAVMax(NR%), MarkUpEn(NR%), stbasef(NR%)
            ReDim XPriceTermLag(NR%), MPriceTermLag(NR%), cartaxenpriadd(NR%)
            ReDim SINSTPEn(NR%), emtax(NR%), ReducedOilProd(NR%), SReducedOilProd(NR%)
            ReDim ReducedGasProd(NR%), SReducedGasProd(NR%), sensho(NR%), pendem(NR%)
            ReDim SExRate(NR%), zsri(NR%)
            ReDim compendemperuniti(NR%), actendemperuniti(NR%), endefsurp(NR%)
            ReDim SWENP(NET), invreduc(NR%), enppot(NR%, NEP%), igdprppp(NR%)
            ReDim EnpCoalDemRI(NR%), EnpHydroDemRI(NR%), endemba(NR%), Exporter(NR%)
            'jrs 2013/04/19
            ReDim SQEUC(NR%, NET), BQEUC(NR%, NET)
            'bbh 2013/06/09
            ReDim ResorTot(NR%, NET)
            'dkb 2014/08/06 - Adding save variables for Energy Trade componenets of POWER - START
            ReDim SENX(NR%), SENM(NR%)
            'dkb 2014/08/06 - END
            'clp 2016/08/24 - Adding save variables for Energy Production componenet of Power
            ReDim EnTotal(NR%), SEnTotal(NR%)
            ReDim iMTARIFFTAXREn(NR%) 'bbh 2019/10/12
            ReDim SENP_UpBnd(NR%, NET%) 'bbh 2021/09/30
            ReDim SENPTOT(NR) 'jrs 2023/10/25
            ReDim SENPRENEW(NR) 'JRS 2023/11/10
            ReDim IShrRenew(NR) 'clp 2024/08/07 Adding ability to have proportional reductions in exports as renewable energy becomes more prominent

            'mti June 08, 2011 -start
            'jrs 2009/10/27
            '      ReDim ENSOLFUELShift(NR%)
            '      'jrs 2010/01/08
            '      ReDim ENSOLFUELI(NR%)

            'mti June 08, 2011 - end

            'jrs 2009/07/31 remember to remove later
            'jrs 2009/08/06 added to common elenrenict = 0.0005

            '
            '
            '     First Year: Initialize Stocks and Demand
            '     ----------------------------------------

            '      swfor = 10 'Initial annual decline in world forest area, mha
            Dim WorldEndem As Single = 0
            Dim WorldGDP As Single = 0
            For e% = 1 To NEP%
                WENP(e%) = 0
            Next e%
            'ttt 2019/10/08 Taken Out
            ReDim ENST(NR)
            ReDim ENSHO(NR)
            ReDim ENDEM(NR)
            For R% = 1 To NR%
                ENPCAPUT(R%) = 1.0!
                'jrs 2021/11/15
                sENPCAPUT(R%) = ENPCAPUT(R%)
                'ttt 2019/10/08
                'ENST(R%) = 0!
                'ENSHO(R%) = 0!
                'already redim
                'sensho(R%) = 0!
                Dim sump As Single = 0!
                Dim SUMPR As Single = 0!
                'ENDEM(R%) = 0!
                'already redim
                'ReducedOilProd(R%) = 0
                'SReducedOilProd(R%) = 0
                'ReducedGasProd(R%) = 0
                'SReducedGasProd(R%) = 0
                '         swfor = swfor + LD(R%, Fors)
                'dkb 2014/08/06 - Adding save variables for Energy Trade componenets of POWER, first year - START
                '         SENX(R%) = 0
                '         SENM(R%) = 0
                'dkb 2014/08/06 - END

                ' BBH 6/22/04 Begin V5_0 Begin  - use physical energy prices
                ENM(R%) = MS(R%, Ener) / WEPBYEAR 'assure consistency with economic model
                ENX(R%) = XS(R%, Ener) / WEPBYEAR 'keep this before any use of enm, enx or
                'ttt 2019/10/08 already redim
                'Exporter(R%) = 0
                If ENX(R%) > ENM(R%) Then Exporter(R%) = 1
                '         ENM(R%) = MS(R%, Ener) / ENPRI(R%) 'assure consistency with economic model
                '         ENX(R%) = XS(R%, Ener) / ENPRI(R%) 'keep this before any use of enm, enx or
                '  will have run-to-run problem
                ' BBH 6/22/04 Begin V5_0 End

                'jrs 2023/10/25
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("ENP", "Total", "", CStr(FirstYear + IY - 1), R%, context)
                End If
                'jrs 2023/11/10
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("ENP", "OthRenew", "", CStr(FirstYear + IY - 1), R%, context)
                End If


                'dkb 2014/08/06 - Adding save variables for Energy Trade componenets of POWER, first year - START
                SENX(R%) = ENX(R%)
                SENM(R%) = ENM(R%)
                'jrs 2023/10/25
                SENPTOT(R%) = 0
                'dkb 2014/08/06 - END
                For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable
                    ENST(R%) = ENST(R%) + ENP(R%, e%) * dstlen
                    ENDEM(R%) = ENDEM(R%) + ENP(R%, e%)
                    sump = sump + ENP(R%, e%)
                    SUMPR = SUMPR + ENP(R%, e%) * ENPRR(R%, e%)
                    SmoothENP(R%, e%) = ENP(R%, e%)
                    'jrs 2023/10/25
                    SENPTOT(R%) = SENPTOT(R%) + ENP(R%, e%)
                Next e%
                'jrs 2023/11/10

                SENPRENEW(R%) = ENP(R%, Renew)  'mti 2024/07/13 - not in use

                'If RegName$(R%) = "Papua NG" Then Stop
                Dim AVEPR = SUMPR / AMAX(sump, 0.001)
                Dim ENDEMEst = sump - ENX(R%) + ENM(R%) _
                   - dstlen * (sump - ENX(R%) + ENM(R%)) * AVEPR
                ENST(R%) = CSng(ENST(R%) + ENDEMEst * dstlen)
                senst(R%) = ENST(R%)
                ENDEM(R%) = CSng(sump - ENX(R%) + ENM(R%) - ENST(R%) * AVEPR)
                'If RegName$(R%) = "Papua NG" Then Stop
                ENDEM(R%) = AMAX(0.0001, ENDEM(R%))
                'jrs 2023/10/25
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("ENDEM", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                smoothpendem(R%) = ENDEM(R%)
                smoothtradebase(R%) = AMAX(ENDEM(R%), (ENX(R%) + ENM(R%)) * 2)
                '
                '     First Year:  Initialize Demand Adjustment
                '     -----------------------------------------
                Dim IndVar = GDPP(R%) / POP(R%)
                '         IndVar = GDP(R%) / POP(R%)
                ' BBH 4/9/04 Begin V5_0 No change
                'TBName$ = "GDP/Capita (PPP) Versus Energy Demand per GDP Dollar (1995)"
                'jrs 2024/06/12 Dim TbName$ = "GDP/Capita (PPP) Versus Energy Demand per GDP Dollar (2000 PPP) Logarithmic"
                TbNameEnergyDemand = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Energy Demand per GDP Dollar PPP (" & BaseYear & ") Logarithmic"
                TbNameEnergyDemand = getFunctionNameYrOrMstRct(TbNameEnergyDemand, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")

                'TBName$ = "GDP/Capita (PPP) Versus Energy Demand per GDP Dollar (1999)"
                ' BBH 4/9/04 End V5_0
                Dim compendemperunit As Single = 0
                Call XYTABL(TbNameEnergyDemand, IndVar, compendemperunit, context)
                compendemperunit = CSng(compendemperunit + 0.0005)
                'Demand data used to estimate function is less than apparent demand globally
                '         SENDEMPI(r%) = AMAX(0.01, GDPP(r%) * CompEnDemPerUnit)
                '         SEndemPI(R%) = AMAX(0.001, GDP(R%) * CompEnDemPerUnit)
                compendemperuniti(R%) = compendemperunit
                '         ActEnDemPerUnitI(R%) = ENDEM(R%) / AMAX(0.0001, GDP(R%))
                actendemperuniti(R%) = ENDEM(R%) / AMAX(0.0001, GDPP(R%))
                'If RegName$(R%) = "China" Then Stop

                'Note as of 2019/09/13: The approach here to changing demand with the function is reactive to omission of biomass
                '  from the energy production side and pushes up demand for some countries (e.g. Ethiopia); not ideal
                'jrs 2022/04/11 avoid for Guinea Bissau
                'jrs 2022/05/02 If GRPS(1, R%) <> "Guinea Bissau" Then
                If ennoprodsw(R%) = 0 Then
                    If actendemperuniti(R%) < 0.2 * compendemperuniti(R%) Then
                        'This should be prevented in preprocessor; just in case
                        ' ENDEM(R%) = 0.2 * CompEnDemPerUnitI(R%) * GDP(R%)
                        ENDEM(R%) = CSng(0.2 * compendemperuniti(R%) * GDPP(R%))
                        'ActEnDemPerUnitI(R%) = ENDEM(R%) / AMAX(0.0001, GDP(R%))
                        actendemperuniti(R%) = ENDEM(R%) / AMAX(0.0001, GDPP(R%))
                    End If
                End If

                'If RegName$(R%) = "China" Then Stop
                Dim pppcomp As Single = 0
                Dim pppcomp2 As Single = 0
                'jrs 2024/06/12
                Dim TbName As String
                If context.HistoricMode Then
                    TbName$ = "GDP/Capita Versus GDP/Capita at Purchasing Power Parity (1980)"
                    gdprext(R%) = -100
                    Call XYTABL(TbName$, GDPPC(R%), pppcomp, context)
                    If GDPPC(R%) > 14.29 Then pppcomp = GDPPC(R%)
                    Call XYTABL(TbName$, GDPPC(R%) * (1 + igdpr(R%) - POPR(R%) / 100), pppcomp2, context)
                    'bbh 2011/09/17
                    '            If GDPPC(R%) > 14.29 Then pppcomp2 = GDPPC(R%)
                    If GDPPC(R%) > 14.29 Then pppcomp2 = GDPPC(R%) * (1 + igdpr(R%) - POPR(R%) / 100)
                Else
                    TbName$ = "GDP/Capita Versus GDP/Capita at Purchasing Power Parity (2000) Power Function"
                    Call XYTABL(TbName$, GDPPC(R%), pppcomp, context)
                    'bbh 2011/09/17
                    ' If GDPPC(R%) > 17.2 Then pppcomp = GDPPC(R%)
                    If GDPPC(R%) > 30.617 Then pppcomp = GDPPC(R%)
                    'Solve for this value by substituting x for y (setting x=y) in
                    '   analytic function and then solving for x (remember x^2*x^3=x^5)
                    Call XYTABL(TbName$, GDPPC(R%) * (1 + igdpr(R%) - POPR(R%) / 100), pppcomp2, context)
                    'bbh 2011/09/17
                    'If GDPPC(R%) > 14.29 Then pppcomp2 = GDPPC(R%)
                    If GDPPC(R%) > 30.617 Then pppcomp2 = GDPPC(R%) * (1 + igdpr(R%) - POPR(R%) / 100)
                End If
                igdprppp(R%) = (pppcomp2 - pppcomp) / pppcomp + POPR(R%) / 100
                'If RegName$(R%) = "China" Then Stop

                '         sgdpp(R%) = GDPP(R%) / (1 + igdpr(R%))
                sgdpp(R%) = GDPP(R%) / (1 + igdprppp(R%))
                GDPPCI(R%) = GDPPC(R%)

                '
                '     First Year:  Initialize Trade Coefficients
                '     ------------------------------------------
                Dim EnTot As Single = 0
                'bbh 2019/08/17 start Create SmoothXEnBase with all energy production
                '  Ethiopia was showing rapid growth in renewables and not much else, not exporting, building stocks
                'ttt 2019/10/08 Already ReDim
                'SmoothXEnBase(R%) = 0

                For e% = 1 To Renew 'Oil, gas, coal, hydro, nuclear, renewable
                    If e% <= Hydro Then
                        EnTot = EnTot + ENP(R%, e%)
                        'bbh 2011/10/16
                        'Else
                        '   EnTot = EnTot + ENP(R%, e%) '* 0.5
                    End If
                    If e% <= Coal Then 'bbh 2020/05/26 start - reduced contribution of non-fossil/electric to export base
                        SmoothXEnBase(R%) = SmoothXEnBase(R%) + ENP(R%, e%)
                    Else
                        SmoothXEnBase(R%) = CSng(SmoothXEnBase(R%) + ENP(R%, e%) * 0.5)
                    End If 'bbh 2020/05/26 end
                    'bbh 2019/08/17 end
                Next e%

                smoothentot(R%) = EnTot
                'clp 2016/08/24 - Adding variable and save variable for energy production
                EnTotal(R%) = smoothentot(R%)
                SEnTotal(R%) = EnTotal(R%)

                'dkb 2019/04/05 - Removing the demand term from XKAVE (reduction in demand should increase the export base) - START
                'XKAVE(R%) = ENX(R%) / (EnTot + ENDEM(R%))
                'XKAVE(R%) = ENX(R%) / (EnTot)
                XKAVE(R%) = ENX(R%) / SmoothXEnBase(R%) 'bbh 2019/08/17 
                'dkb 2019/04/05 - END

                'demand needed for entrepot economies
                XKAVMAX(R%) = AMAX(trademax, XKAVE(R%))
                mkave(R%) = ENM(R%) / ENDEM(R%)
                mkavei(R%) = mkave(R%)

                SImportDev(R%) = 0
                MKAVMax(R%) = CSng(AMAX(trademax, mkave(R%) * 1.5))
                iMTARIFFTAXREn(R%) = MTARIFFTAXR(R%, Ener) 'bbh 2019/10/12
                'ttt 2019/10/08 Already ReDim
                'XPriceTermLag(R%) = 0
                'MPriceTermLag(R%) = 0
                '
                '     First Year:  Initialize Production Costs and Cumulative Production
                '     ------------------------------------------------------------------

                For e% = 1 To NET% 'All Forms
                    BQE(R%, e%) = QE(R%, e%)
                    sqe(R%, e%) = QE(R%, e%)
                    CUMPR(R%, e%) = 0!
                Next e%

                'jrs 2013/04/19
                For e% = 1 To NET% 'All Forms
                    BQEUC(R%, e%) = QEUNCON(R%, e%)
                    SQEUC(R%, e%) = QEUNCON(R%, e%)
                Next e%

                'jrs 2013/04/19 IQESynConRat(R%) = SQE(R%, Synth) / SQE(R%, Oil)
                IQESynConRat(R%) = SQEUC(R%, Oil) / sqe(R%, Oil)
                '
                '     First Year:  Initialize Capital and Investment
                '     ----------------------------------------------
                Dim renp = 0!
                'ttt 2019/10/08 Already ReDim
                'tineed(R%) = 0
                Dim SKenlke = 0!
                'If GRPS(1, R%) = "United States of America" Then Stop
                For e% = 1 To NEP%  'Oil, gas, coal, hydro, nuclear, renewable

                    'If GRPS(1, R) = "China" And e% = 6 Then Stop
                    enppot(R%, e%) = ENP(R%, e%)
                    ken(R%, e%) = ENP(R%, e%) * QE(R%, e%)
                    'jrs 2021/11/15
                    sken(R%, e%) = ken(R%, e%)
                    SKenlke = SKenlke + ken(R%, e%) / AMAX(5, lke(e%))
                    'cenprr = (ENPRR(R%, e%) * igdpr(R%) / AVEPR) * (1 - ENX(R%) / sump) + _
                    'ENPRR(R%, e%) * ENX(R%) / sump
                    'bbh 2019/04/02 regionalized enrgdpgr
                    Dim cenprr = (ENPRR(R%, e%) * (igdpr(R%) + enrgdpgr(R%)) / AVEPR) * (1 - ENX(R%) / sump) +
                    ENPRR(R%, e%) * ENX(R%) / sump

                    'If RegName$(R%) = "El Salvador" And e% = 1 Then Stop

                    'Normalize productions to growth rate, except for heavy exporters
                    'Do not replace ENPRR or changes initial conditions from base load
                    cenprr = ENPRR(R%, e%)
                    'If GRPS(1, R) = "France" Then Stop

                    ineed(R%, e%) = CSng(ken(R%, e%) * (cenprr + 1.0! / lke(e%)))
                    If (eprodr(R%, e%) > 0!) Then
                        ineed(R%, e%) = ken(R%, e%) * (eprodr(R%, e%) + 1.0! / lke(e%))
                    End If

                    'BBH 2024/09/07 start
                    'BBH Note: French nuclear ENPRR was so negative that ineed nuclear was large negative and even forced tineed negative; all ENP collapsed
                    'mti Note:
                    ineed(R%, e%) = AMAX(ineed(R%, e%), CSng(ken(R%, e%) / lke(e%) * 0.5))
                    'BBH 2024/09/07 end

                    tineed(R%) = tineed(R%) + ineed(R%, e%)
                    sineed(R%, e%) = ineed(R%, e%)
                    'If GRPS(1, R) = "United States of America" Then Stop
                    eninvri(R%, e%) = ineed(R%, e%) / ken(R%, e%)
                    renp = renp + ENP(R%, e%)
                    WENP(e%) = WENP(e%) + ENP(R%, e%)
                Next e%


                'mti 2025/02/06 start
                'For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, solar, wind, geo, other renewable
                '    If GRPS(1, R) = "United States of America" And e = 6 Then Stop
                '    ENINVT(R%, e%) = ineed(R%, e%) 'unadjusted
                '    'ENINVT(R%, e%) = ineed(R%, e%) * sidsf(R%)  'adjusted
                'Next e%
                'mti 2025/02/06 end

                WorldKen = WorldKen + SKenlke
                WorldEnp = WorldEnp + renp
                WorldEndem = WorldEndem + ENDEM(R%)
                WorldGDP = WorldGDP + GDP(R%)
                'bbh 2011/10/17
                'sgdpenr(R%) = tineed(R%) / GDP(R%) 'Use GDP instead of I for stabilty, esp
                ' in face of possible positive feedback loop across I and INeed, via PFD
                sendeminvr(R%) = tineed(R%) / ENDEM(R%)
                SCSF(R%) = CSng(CS(R%, Ener) / (ENDEM(R%) * 0.6))
                'Industrial demand is about 40% of total, transportation and residential
                '   is about 60%, with strong global relationship and r-squared = .86
                '         SENPR(R%) = 1!
                'ttt 2019/10/08 Already ReDim
                'invreduc(R%) = 0
                EnpCoalDemRI(R%) = ENP(R%, Coal) / ENDEM(R%)
                EnpHydroDemRI(R%) = ENP(R%, Hydro) / ENDEM(R%)

                '     First Year:  Initialize Costs, Prices, and Profits
                '     --------------------------------------------------
                Dim SumENP = 0!
                ENPRI(R%) = AMAX(0.01, ENPRI(R%))
                SENPRI(R%) = ENPRI(R%)
                ENPRII(R%) = ENPRI(R%)
                renpri(R%) = ENPRII(R%)
                For e% = 1 To NEP%  'Oil, gas, coal, hydro, nuclear, renewable
                    TENP = TENP + ENP(R%, e%)
                    SumENP = SumENP + ENP(R%, e%)
                    TENPRI = TENPRI + ENPRI(R%) * ENP(R%, e%) * EXRATE(R%)
                    SIENP(R%, e%) = ENP(R%, e%)
                Next e%
                EPROFITR(R%) = 1
                SEProfitR(R%) = 1
                MarkUpEn(R%) = 1
                cartaxenpriadd(R%) = 0
                stbasef(R%) = AMAX(1, SumENP / ENDEM(R%))
                SExRate(R%) = 1

                'clp 2024/08/07 ***START*** Adding ability to have proportional reductions in exports as renewable energy becomes more prominent
                'renewtoenexsw = 1
                Dim ShrRenew As Single
                Dim tot As Single
                If renewtoenexsw > 0 Then
                    ShrRenew = 0
                    tot = 0
                    For e% = 1 To NET%
                        'TradeRestraint = 0 'update restraints 'not actually used later so commented out and left for reference
                        'ShrRenew = (ENP(R%, 4) + ENP(R%, 6)) / (ENP(R%, 1) + ENP(R%, 2) + ENP(R%, 3) + ENP(R%, 4) + ENP(R%, 5) + ENP(R%, 6)) 'Oil, gas, coal, hydro, nuclear, renewable
                        'IShrRenew(R%) = 0
                        If e% > 3 Then ShrRenew = ShrRenew + ENP(R%, e%)
                        tot = tot + ENP(R%, e%)
                    Next e%
                    IShrRenew(R%) = ShrRenew / tot
                End If
                'clp 2024/08/07 ***END*** Adding ability to have proportional reductions in exports as renewable energy becomes more prominent
                'IShrRenew(R%) = (ENP(R%, 4) + ENP(R%, 6)) / (ENP(R%, 1) + ENP(R%, 2) + ENP(R%, 3) + ENP(R%, 4) + ENP(R%, 5) + ENP(R%, 6)) 'Oil, gas, coal, hydro, nuclear, renewable 'clp 2024/08/07 Adding ability to have proportional reductions in exports as renewable energy becomes more prominent
            Next R%
            WEP = TENPRI / TENP
            iwep = WEP
            iwepbyear = WEPBYEAR
            swep = WEP
            SEnCartPP = encartpp
            wkenenpri = WorldKen / WorldEnp
            '  BBH 7/14/04 V5.01 Begin
            Swkenenpr = WorldKen / WorldEnp

            SWENDEMR = WorldEndem / WorldGDP
            '  BBH 7/14/04 V5.01 End
            'bbh2013/06/09 start
            '
            '     First Year:  Initialize Undiscovered Resources
            '     ----------------------------------------------
            For e% = 1 To NET%
                For R% = 1 To NR%
                    'If GRPS(1, R) = "Albania" And e% = 8 Then Stop
                    ResorTot(R%, e%) = resorm(R%, e%) * resor(R%, e%) + resorunconm(R%, e%) * resoruncon(R%, e%)
                Next R%
            Next e%
            'bbh2013/06/09 start
            '
            '     First Year:  Initialize Undiscovered Resources
            '     ----------------------------------------------
            'jrs 2013/04/22 NET%
            For e% = 1 To Hydro  'Oil, gas, coal, hydro
                wunro(e%) = 0!
                For R% = 1 To NR%
                    If e% < Hydro Then
                        RESER(R%, e%) = AMAX(RESER(R%, e%), 10 * ENP(R%, e%))
                        'Not good form to be changing RESER from preprocessor value in 1st year
                        'in ENP were also changed, could lead to run to run
                        'This code perhaps should be in preprocessor; but would constrain resorm changes
                    ElseIf e% = Hydro Then 'jrs 2013/04/22 added e% = Hydro
                        RESER(R%, e%) = AMAX(RESER(R%, e%), 2 * ENP(R%, e%))
                    End If
                    'bbh2013/06/09
                    'wunro(e%) = wunro(e%) + _
                    'AMAX(resorm(R%, e%) * resor(R%, e%) - RESER(R%, e%), 0)
                    wunro(e%) = wunro(e%) +
                        AMAX(ResorTot(R%, e%) - RESER(R%, e%), 0)
                    'If GRPS(1, R) = "Bosnia" And e% = Coal Then Stop
                    SResProdR(R%, e%) = CSng(Math.Round(RESER(R%, e%) / ENP(R%, e%), 5))
                Next R%
            Next e%
            '     First Year:  Save Global Production by Type
            '     -------------------------------------------
            For e% = 1 To Renew
                SWENP(e%) = WENP(e%)
            Next e%
            '      sacarb = carinit

            'mti June 08, 2011 - start

            '    'jrs 2009/10/27
            '    'First Year: Shift factor for fosil fuel use
            '    '--------------------------------------------
            '    Dim PopAd As Single, PopCh As Single, Fct As Single
            '    For R% = 1 To NR%
            '        Call AnalFunc("Percent of People using Primarily Solid Fuels (Full Model)", _
            '        Array(GDPPCP(R%), EDYRSAG25(R%, 3), INCOMELT1LN(R%) / POP(R%) * 100), cVal)
            '        ENSOLFUELShift(R%) = ENSOLFUEL(R%) / (100 * Exp(cVal) / (Exp(cVal) + 1))
            '        'jrs 2010/01/08
            '        ENSOLFUELI(R%) = ENSOLFUEL(R%)
            '
            '        'mti June 08, 2011 -start
            '        sENSOLFUEL(R%) = ENSOLFUEL(R%)
            '        'mti June 08, 2011 - end
            '
            '    Next R%

            'mti June 08, 2011 - end
            'mti June 08, 2011 - end
            'GoTo AllYears=>
            Call SubAllYears()


        End Sub

        Dim cumwmul As Single
        Dim Swenstks As Single

        Public Sub IFsEn1AllbutFirstYear(context As IForecastContext)
            'AllButFirst:
            '
            '     All but First:  Initialize Save Variables
            '     -----------------------------------------
            Dim WENDEMSum = 0!
            Dim WGDPSum = 0!
            Dim BroadbandTerm = 0!
            'ttt 2019/10/08 TakeOut
            'jrs 2019/11/13 we can't copy this one because size is different
            'sreser is only NRx4 vs RESER NRx6
            'Array.Copy(sreser, RESER, sreser.Length)

            For R% = 1 To NR%
                'jrs 2013/04/22 NET%
                'ttt 2019/10/08 TakenOut
                For e% = 1 To Hydro  'Oil, gas, coal, hydro
                    RESER(R%, e%) = sreser(R%, e%)
                Next e%
                For e% = 1 To NET%
                    'jrs 2009/08/04
                    If e% = Renew Then
                        'bbh 2009/09/17 bound broadband cost change
                        BroadbandTerm = CSng(AMAX(-0.1, Amin(0.1, (SBroad(R, 2) - ICTBroadExp(R)) * elenrenict)))

                        '                'mti Sep 12, 2011 - start
                        '                'take it off after testing
                        BroadbandTerm = 0
                        '                'mti Sep 12, 2011 - end

                        'bbh 2019/04/02 regionalized etechadv
                        BQE(R%, e%) = BQE(R%, e%) * (1 - etechadv(R%, e%) - BroadbandTerm)
                    Else
                        'bbh 2019/04/02 regionalized etechadv
                        BQE(R%, e%) = BQE(R%, e%) * (1 - etechadv(R%, e%))
                    End If



                    QE(R%, e%) = AMAX(0.01, Amin(sqe(R%, e%) * qem(R%, e%), 1000))
                    'If RegName$(R%) = "Turkmenistan" And e% = Hydro Then Stop
                Next e%

                'jrs 2013/04/19
                For e% = 1 To NET%
                    BQEUC(R%, e%) = BQEUC(R%, e%) * (1 - etechadvuncon(e%))
                    QEUNCON(R%, e%) = AMAX(0.01, Amin(SQEUC(R%, e%) * qeunconm(R%, e%), 1000))
                Next e%
                '
                '     All but First:  Demand
                '     ----------------------
                '        Note:  This demand structure (without sectoral differentiation and
                '        without sectoral specific demand patterns, assumes perfect substitutability
                '        among supply forms
                '        Energy demand computation has three steps/drivers:
                '           1.  Define target demand per unit of GDP and converge towards it
                '           2.  Adjust with prices, including carbon taxes if any
                '           3.  Adjust with technological efficiency increases

                '        Step 1 - link to GDP per capita
                '         gdpexpect = sgdp(R%) * (1 + igdpr(R%))
                '         gdpexpect = sgdpp(R%) * (1 + igdpr(R%))
                Dim gdpexpect = sgdpp(R%) * (1 + igdprppp(R%))

                Dim IndVar = gdpexpect / POP(R%)
                ' BBH 4/9/04 Begin V5_0 - No change
                'TBName$ = "GDP/Capita (PPP) Versus Energy Demand per GDP Dollar (1995)"
                'jrs 2024/06/12 Dim TbName$ = "GDP/Capita (PPP) Versus Energy Demand per GDP Dollar (2000 PPP) Logarithmic"
                'TBName$ = "GDP/Capita (PPP) Versus Energy Demand per GDP Dollar (1999)"
                ' BBH 4/9/04 End V5_0
                Dim compendemperunit = 0!
                Call XYTABL(TbNameEnergyDemand, IndVar, compendemperunit, context)
                compendemperunit = CSng(compendemperunit + 0.0005)
                'Demand data used to estimate function is less than apparent demand globally
                Dim ConvergeTime = 0!
                If GDPPCI(R%) > 20 AndAlso actendemperuniti(R%) < compendemperuniti(R%) Then
                    ConvergeTime = enconv * 4
                    'Limit upward convergence for efficient developed countries like EU
                Else
                    ConvergeTime = enconv
                End If
                '         If RegName$(R%) = "China" Then ConvergeTime = enconv * 4 '/ 3
                Dim TimeFAc = Amin(IY%, ConvergeTime)
                Dim funcchangedemandratio = compendemperunit / AMAX(0.00001, compendemperuniti(R%))
                'If RegName$(R%) = "Unitd Kingdm" Then Stop
                'computed now vs computed 1st year
                Dim convergingenpergdpunit = (actendemperuniti(R%) * (ConvergeTime - TimeFAc) +
                   compendemperuniti(R%) * TimeFAc) / ConvergeTime
                'If GDPPCI(R%) > 20 Then ConvergingEnPerGDPUnit = Amin(ActEnDemPerUnitI(R%), ConvergingEnPerGDPUnit)
                'Note as of 2019/09/13: The approach here to changing demand with the function and final term is reactive to omission of biomass
                '  from the energy production side and pushes up demand for some countries (e.g. Ethiopia); not ideal
                endemba(R%) = gdpexpect * convergingenpergdpunit * funcchangedemandratio
                'If RegName$(R%) = "China" Then Stop


                '          Energy demand base shifts initial condition (e.g. Russia) towards analytic function
                '          over time, converging out initial condition differences; country demand also moves along function

                '           Simplified form of GDPExpect * ENDEMi/GDPPi * CompEnDem/(SENDEMPI/GDPPi)
                '              i.e, of GDP * initial energy intensity * change in intensity
                '         renpri(r%) = renpri(r%) * ehw + (SENPRI(r%) + CarTaxEnPriAdd(r%)) * (1! - ehw)

                '        Step 2 - adjust with prices
                'adjust carbon tax increment from per barrel (IWEPBYear) to index base (IWEP)
                renpri(R%) = renpri(R%) * ehw + (swep + cartaxenpriadd(R%) * iwep / iwepbyear) * (1.0! - ehw)
                'bbh 2010/10/17    A point elasticity in the curve mid-range will become smaller as price increase
                'because an elasticity creates a linear relationship of price and demand and it is curvilinear
                Dim elasadjusted = elasde(R%) * ENPRII(R%) / AMAX(0.001, renpri(R%))
                Dim elasterm = AMAX(0.2, 1.0! + (renpri(R%) - ENPRII(R%)) / ENPRII(R%) * elasadjusted)
                '        Step 3 - adjust with technological efficiency
                'DTEMP = endemba * endemm(R%) * elasterm * (1 + enrgdpgr / 100) ^ IY%
                ENDEM(R%) = endemba(R%) * endemm(R%) * elasterm

                'If R% = 36 And IY% >= 94 Then Stop

                WENDEMSum = WENDEMSum + ENDEM(R%)
                WGDPSum = WGDPSum + sgdpp(R%)

            Next R%

            Dim wenrgdpr = WENDEMSum / WGDPSum
            For R% = 1 To NR% 'compare energy use per GDP unit with global; iff a country is above
                'less efficient than global average, accelerage progress towards efficiency,
                'by no more than 1% per year
                Dim EnRGDPRTemp = Amin(1, (ENDEM(R%) / sgdpp(R%)) / wenrgdpr - 1)
                If Exporter(R%) > 0.5 Then EnRGDPRTemp = 0 'Don't accelerate efficiency for exporters
                '         EnRGDPGRCalc = enrgdpgr * AMAX(1, EnRGDPRTemp)
                'bbh 2019/04/02 regionalized enrgdpgr
                Dim enrgdpgrcalc = enrgdpgr(R%) + (-1) * AMAX(0, EnRGDPRTemp)
                'Note:  enrgdpgr used to be about -1%; now much smaller because other
                'convergence processes bring down demand nearly 1%/year
                'If RegName$(R%) = "China" Then Stop
                Dim DTEMP = ENDEM(R%) * (1 + enrgdpgrcalc / 100) ^ IY%
                'bbh 2019/11/14 relax downward constraint for major sustainability push scenarios
                'ENDEM(R%) = CSng(AMAX(0.2 * endemba(R%), DTEMP))
                ENDEM(R%) = CSng(AMAX(0.1 * endemba(R%), DTEMP))
                'If RegName$(R%) = "Armenia" Then Stop
                'If R% = 36 And IY% >= 94 Then Stop

                ENDEM(R%) = AMAX(0.0001, ENDEM(R%))
                'jrs 2023/10/25
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("ENDEM", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                'Computation of Smoothed-price based energy demand to drive adjustment
                '  mechanisms of trade, investment
                'adjust carbon tax increment from per barrel (IWEPBYear) to index base (IWEP)
                Dim PEnPRI = swep + cartaxenpriadd(R%) * iwep / iwepbyear 'Projected after adjustment lag
                'bbh 2010/10/17
                Dim elasadjusted = elasde(R%) * ENPRII(R%) / AMAX(0.001, PEnPRI)
                Dim elasterm = 1.0! + (PEnPRI - ENPRII(R%)) / ENPRII(R%) * elasadjusted
                'bbh 2019/04/02 regionalized enrgdpgr
                DTEMP = endemba(R%) * endemm(R%) * elasterm * (1 + enrgdpgr(R%) / 100) ^ IY%
                'bbh 2019/11/14 relax downward constraint for major sustainability push scenarios
                'pendem(R%) = CSng(AMAX(0.2 * endemba(R%), DTEMP))
                pendem(R%) = CSng(AMAX(0.1 * endemba(R%), DTEMP))
                pendem(R%) = AMAX(0.0001, pendem(R%))
                smoothpendem(R%) = CSng(0.8 * smoothpendem(R%) + 0.2 * pendem(R%))

                ENRGDP(R%) = ENDEM(R%) / AMAX(0.0001, (SGDP(R%) * (1 + igdpr(R%)))) * 1000
                'ENRGDP(R%) = ENDEM(R%) / AMAX(0.0001, (sgdpp(R%) * (1 + igdpr(R%)))) * 1000
                'For economic model which needs estimate in IO rev before final calculation
                'Do not need relative to GDPP because have in display options
            Next R%
            '
            '     All but First:  Production
            '     ---------------------------
            '      WorldKen = 0
            '      WorldEnp = 0
            Dim Garbage = 0!
            For R% = 1 To NR%
                For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable  



                    'If GRPS(1, R) = "India" And e = Solar And IY >= 2 Then Stop

                    ENP(R%, e%) = (sken(R%, e%) / QE(R%, e%)) * enpm(R%, e%)  'jrs 2021/11/15 changed to sken

                    'mti  2024/10/25 start
                    'sken(R%, e%) = sken(R%, e%) * enpm(R%, e%)
                    'mti  2024/10/25 end

                    'jrs 2022/04/05
                    ENP(R%, e%) = ENP(R%, e%) + enpadd(R%, e%)
                    If e% = 2 And enpgasmax(R%) > 0.001 Then 'bbh 2021/09/30 added If block to smooth production at top of max production
                        'If ENP(R%, e%) > 0.95 * enpgasmax(R%) Then
                        ENP(R%, e%) = CSng(ENP(R%, e%) * 0.2 + SmoothENP(R%, e%) * 0.8)
                        SmoothENP(R%, e%) = ENP(R%, e%)
                        'End If
                    End If

                    'sgh 2013/11/2 Tax gas production in South Africa to fund renewables Start
                    'This block of code was written for the South African policy brief concerning shale gas production
                    '"Fracking for Shale Gas in South Africa: Blessing or Curse?"
                    'When activated this will allow the user to place an excise tax on gas production (for South Africa only)
                    'and reinvest that revenue into renewable energy capital. GasProd is natural gas production for South Africa.
                    'GasRev is the revenue that is generated given the tax on gas production. The tax is in terms of dollars
                    'per million cubic feet. RenewIncrease is then calculated using the revenue generated from gas and the
                    'capital-output ratio for renewables in South Africa ($1837/barrel oil equivalent). The capital-output
                    'ratio decreases by 1.5 percent each year. CumRenewProd is the cumulative increase in renewable energy generated from
                    'the tax. Since these are investments in capital the energy generated from these investments is a cumulative output
                    'CumRenewProd is used to calculate the total energy production from renewables (ENP(R%,6)) + CumRenewProd and
                    'then to calculate the enpm(R%,6) necessary to shift production to that level.
                    'If e% = 2 And RegName$(R%) = "South Africa" Then
                    'GasProd = ENP(R%, 2)
                    'GasRev = GasProd * 1000 * 5.35 * GastoRenewTax(R%) * 1000
                    'RenewIncrease = (GasRev / 1000 / (1837 * (0.985 ^ IY%)))
                    'CumRenewProd = CumRenewProd + RenewIncrease
                    'enpm(R%, 6) = (ENP(R%, 6) + CumRenewProd) / ENP(R%, 6)
                    'If RegName$(R%) = "South Africa" Then Stop
                    'End If
                    'sgh 2013/11/2 Tax gas production in South Africa to fund renewables End
                    enppot(R%, e%) = (sken(R%, e%) / QE(R%, e%))  'jrs 2021/11/15 changed to sken
                    'If RegName$(R%) = "El Salvador" And e% = 1 Then Stop
                Next e%
                '
                '     All but First:  Bound Production by Reserves
                '     --------------------------------------------
                'jrs 2013/04/22 NET%
                Dim ENP_UpBnd = 0!
                For e% = 1 To Hydro  'Oil, gas, coal, hydro
                    '            fac = 1!
                    '            If (e% = Oil) Then fac = SENPR(R%)
                    '            ENP_UpBnd = RESER(R%, e%) / (AMAX(prodtf(R%, e%) * fac, 0.001))
                    'bbh 2021/09/30 start energy 1 block
                    ENP_UpBnd = CSng(RESER(R%, e%) / (AMAX(prodtf(R%, e%), 0.001)))
                    If IY% = 2 Then SENP_UpBnd(R%, e%) = ENP_UpBnd
                    If SResProdR(R%, e%) < 30 And SResProdR(R%, e%) > prodtf(R%, e%) Then
                        Dim SlowLimit = RESER(R%, e%) / (AMAX(SResProdR(R%, e%) - 1, 0.001))
                        ENP_UpBnd = CSng(Amin(ENP_UpBnd, SlowLimit))
                    End If
                    If enpoilmax(R%) > 0.001 And e% = Oil Then
                        ENP_UpBnd = Amin(ENP_UpBnd, enpoilmax(R%))
                    End If
                    If enpgasmax(R%) > 0.001 And e% = Gas Then
                        ENP_UpBnd = Amin(ENP_UpBnd, enpgasmax(R%))
                    End If
                    If enpcoalmax(R%) > 0.001 And e% = Coal Then
                        ENP_UpBnd = Amin(ENP_UpBnd, enpcoalmax(R%))
                    End If

                    'jrs 2013/04/23
                    'Nuclear and Other Renewables still don't have reserves to find bound this way
                    If e% <= Hydro Or ENP_UpBnd > 0 Then
                        ENP(R%, e%) = Amin(ENP(R%, e%), SENP_UpBnd(R%, e%))
                    End If

                    If e% <= Hydro Then 'bbh 2021/09/30 If block for smoothing production as approach reserve ratio bounding point
                        If SResProdR(R%, e%) <= 60 Then
                            Dim SmoothPhaseIn = AMAX(0, Amin(1, 1 - (SResProdR(R%, e%) - 30) / 30)) * 0.7 'goes from no use of smoothing at 60 to full use at 30
                            ENP(R%, e%) = CSng(ENP(R%, e%) * (1 - SmoothPhaseIn) + SmoothENP(R%, e%) * SmoothPhaseIn)
                            'If IY% >= 22 And RegName$(R%) = "Qatar" And e% = 2 Then Stop
                        End If
                        SmoothENP(R%, e%) = ENP(R%, e%)
                    End If 'bbh 2021/09/30 End If block

                    ' If IY% >= 22 And RegName$(R%) = "Qatar" And e% = 2 Then Stop

                    SENP_UpBnd(R%, e%) = CSng(SENP_UpBnd(R%, e%) * 0.9 + ENP_UpBnd * 0.1)
                    SResProdR(R%, e%) = CSng(RESER(R%, e%) / AMAX(ENP(R%, e%), 0.001)) 'jrs 2013/04/23 added amax(x, 0.001)
                    SResProdR(R%, e%) = CSng(SResProdR(R%, e%) * 0.9 + RESER(R%, e%) / AMAX(ENP(R%, e%), 0.001) * 0.1) 'bbh 2021/09/30 to smooth top of prod peak
                    'bbh 2021/09/30 end energy 1 block
                Next e%
                'If RegName$(R%) = "Unitd Kingdm" Then Stop
            Next R%
            '
            '     All but First:  Trade
            '     ---------------------

            'BBH 7/15/04 V5_01 Begin
            Dim wxc = 0!
            Dim wmd = 0!
            Dim TradeRestraint = 0!
            Dim GlobalStockBase = 0!
            Dim GlobalStocks = 0!
            Dim EnTot = 0!
            Dim EnTotCap = 0!
            Dim exportbase = 0!
            Dim StBase = 0!
            Dim EnTotAll = 0!
            For R% = 1 To NR%
                EnTot = 0
                EnTotCap = 0
                'bbh 2019/08/17 start update en export base that has all production
                EnTotAll = 0
                For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable
                    If e% <= Coal Then 'bbh 2020/05/26 start - reduced impact of non-fossil/electric to export base
                        EnTotAll = EnTotAll + ENP(R%, e%)
                    Else
                        EnTotAll = CSng(EnTotAll + ENP(R%, e%) * 0.5)
                    End If 'bbh 2020/05/26 end
                    ' EnTotAll = EnTotAll + ENP(R%, e%)
                Next e%
                'bbh 2011/10/16
                'For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable
                For e% = 1 To Hydro
                    EnTot = EnTot + ENP(R%, e%)
                    EnTotCap = EnTotCap + ENP(R%, e%) * sENPCAPUT(R%)  'jrs 2021/11/15 changed to SENPCAPUT
                Next e%
                SmoothXEnBase(R%) = CSng(0.9 * SmoothXEnBase(R%) + 0.1 * EnTotAll) 'bbh 2019/08/17
                smoothentot(R%) = CSng(0.9 * smoothentot(R%) + 0.1 * EnTot)

                'dkb 2019/04/05 - Removing the demand term from export base (reduction in demand should increase the export base) - START
                'exportbase = smoothentot(R%) + smoothpendem(R%)
                exportbase = smoothentot(R%)
                'dkb 2019/04/05 - END
                exportbase = SmoothXEnBase(R%)
                'bbh 2019/08/17 end
                'jrs 2022/04/06
                ENX(R%) = AMAX(0.0001, Amin(XKAVE(R%) * exportbase, XKAVMAX(R%) * exportbase))
                'ENX(R%) = AMAX(0.00000001, Amin(XKAVE(R%) * exportbase, XKAVMAX(R%) * exportbase))
                'Had minimum at .001, but messes up Liberia, Lesotho, etc - basically no exports

                'If RegName$(R%) = "Saudi Arabia" And IY% > 23 Then Stop

                'ENM(R%) = AMAX(0.0001, Amin(mkave(R%) * pendem(R%), MKAVMax(R%) * smoothpendem(R%)))
                'jrs 2022/04/06
                ENM(R%) = AMAX(0.0001, Amin(mkave(R%) * smoothpendem(R%), MKAVMax(R%) * smoothpendem(R%))) 'bbh 2020/05/26 start replaces line above
                'ENM(R%) = AMAX(0.00000001, Amin(mkave(R%) * smoothpendem(R%), MKAVMax(R%) * smoothpendem(R%)))
                'When base for mkave was pendem, subsequent code using smoothpdendem was allowing rise in mkave that pushed
                '   imports up to and even above energy demand (e.g. Nigeria); created oscillation in energy exports
                'This fix also reduces global level of ENSHO over time, but leaves some including Nigeria
                'bbh 2020/05/26 end

                StBase = smoothentot(R%) + smoothpendem(R%)

                'bbh 2019/10/12 start
                'iMTARIFFTAXR is computed locally in IFsEcoSAM, so need En value here
                'mtarifftaxrm is applied to iMARIFFTAXR in IFsEcoSAM but not in time for use here, so
                '   done here also; because applied to initial condition, is not compounded impact
                'Because AGM change also changes mkave over time, tariff level changes have somewhat compounding
                '   impact, higher than elasticity
                MTARIFFTAXR(R%, Ener) = iMTARIFFTAXREn(R%) * mtarifftaxrm(R%, Ener)
                'jrs 2020/07/07 MTARIFFTAXR not computed yet (constant when bilateral mode is off)
                'ENM(R%) = CSng(ENM(R%) * (1 + ((MTARIFFTAXR(R%, Ener) - iMTARIFFTAXREn(R%)) / 100) * -1.0#))
                'jrs 2020/10/26 start
                'Implementing Barry's idea: differential approach
                If SMTariffTaxR(R%, Ener) < iMTARIFFTAXREn(R%) Then
                    'This line was the orignal approach, which was producing negatives when Tariffs grew more than 100% above initial values
                    ENM(R%) = CSng(ENM(R%) * (1 + ((SMTariffTaxR(R%, Ener) - iMTARIFFTAXREn(R%)) / 100) * -1.0#))
                Else
                    ENM(R%) = CSng(ENM(R%) * (1 + (SMTariffTaxR(R%, Ener) - iMTARIFFTAXREn(R%)) / 100) ^ -1)
                End If
                'If RegName$(R%) = "Germany" Then Stop
                'bbh 2019/10/12 end

                '         If IY% = 2 Then
                '            endefsurp(R%) = ENDEM(R%) + dstlen * StBase + ENX(R%) - SENST(R%) _
                '            - SENSHO(R%) - EnTotCap - ENM(R%)
                '         Else
                '            defsurp = ENDEM(R%) + dstlen * StBase + ENX(R%) - SENST(R%) _
                '               - SENSHO(R%) - EnTotCap - ENM(R%)
                '            endefsurp(R%) = 0.85 * endefsurp(R%) + 0.15 * defsurp
                '         End If
                smoothtradebase(R%) = CSng(AMAX(ENDEM(R%),
            0.9 * smoothtradebase(R%) + 0.1 * (ENX(R%) + ENM(R%)) * 2))
                'If not large enough will result in oscillation of Enx/enm

                GlobalStockBase = GlobalStockBase + StBase
                GlobalStocks = GlobalStocks + senst(R%) - sensho(R%)
            Next R%
            Dim globalstockratio = GlobalStocks / GlobalStockBase
            Dim Mul = 0!
            Dim Diff1 = 0!
            Dim Diff2 = 0!
            For R% = 1 To NR%
                StBase = smoothentot(R%) + smoothpendem(R%)
                endefsurp(R%) = (globalstockratio - (senst(R%) - sensho(R%)) / StBase) * StBase

                'dkb 2019/04/05 - Removing the demand term from export base (reduction in demand should increase the export base) - START
                'exportbase = smoothentot(R%) + smoothpendem(R%)
                exportbase = smoothentot(R%)
                'dkb 2019/04/05 - END

                'bbh 2019/08/17 Change export base to include all of production
                exportbase = SmoothXEnBase(R%)

                Dim defsurpm = Amin(1, dstlen * StBase / (AMAX(0.0001, ENX(R%) + ENM(R%)))) * endefsurp(R%)
                'If RegName$(R%) = "Iraq" And IY% > 38 Then Stop

                If endefsurp(R%) > 0 Then 'Shortfall
                    ENX(R%) = AMAX(0.5 * ENX(R%), ENX(R%) - ENX(R%) / smoothtradebase(R%) * endefsurp(R%))
                    'ENX(R%) = AMAX(0.5 * ENX(R%), ENX(R%) - ENX(R%) / smoothtradebase(R%) * DefSurpM)
                    Dim ENMMax = ENM(R%) + (pendem(R%) * MKAVMax(R%) - ENM(R%)) / 5
                    ENM(R%) = Amin(ENMMax, ENM(R%) + ENM(R%) / smoothtradebase(R%) * endefsurp(R%))
                    'ENM(R%) = Amin(ENMMax, ENM(R%) + ENM(R%) / smoothtradebase(R%) * DefSurpM)
                Else   'Surplus
                    Dim enxmax = ENX(R%) + (exportbase * XKAVMAX(R%) - ENX(R%)) / 5
                    Diff1 = EXRATE(R%) - 1
                    Diff2 = EXRATE(R%) - SExRate(R%)
                    Call ADJSTR(1, Diff1, Diff2, -0.2, -0.4, Mul)
                    'Call ADJSTR(1, Diff1, Diff2, -0.1, -0.2, Mul) 'bbh 2020/05/26; considered reduction like this and may still be useful
                    ENX(R%) = CSng(ENX(R%) * Amin(1.05, AMAX(0.95, Mul)))
                    ENX(R%) = Amin(enxmax, ENX(R%) - ENX(R%) / smoothtradebase(R%) * endefsurp(R%))
                    ENM(R%) = AMAX(0.5 * ENM(R%), ENM(R%) + ENM(R%) / smoothtradebase(R%) * endefsurp(R%))
                    'ENX(R%) = Amin(ENXMax, ENX(R%) - ENX(R%) / smoothtradebase(R%) * DefSurpM)
                    'ENM(R%) = AMAX(0.5 * ENM(R%), ENM(R%) + ENM(R%) / smoothtradebase(R%) * DefSurpM)
                End If
                'If RegName$(R%) = "Iraq" And IY% > 38 Then Stop

                SExRate(R%) = EXRATE(R%)

                If enxl(R%) > 0.001 Then 'Export Restraint
                    TradeRestraint = TradeRestraint + AMAX(0, ENX(R%) - enxl(R%))
                    ENX(R%) = Amin(ENX(R%), enxl(R%))
                End If
                If enml(R%) > 0.001 Then 'Import Restraint
                    TradeRestraint = TradeRestraint + AMAX(0, ENM(R%) - enml(R%))
                    ENM(R%) = Amin(ENM(R%), enml(R%))
                End If

                Dim ShrRenew As Single
                Dim tot As Single
                Dim tmpShrRenewChng As Single
                'clp 2024/08/07 ***START*** Adding ability to have proportional reductions in exports as renewable energy becomes more prominent
                'renewtoenexsw = 1
                If renewtoenexsw > 0 Then
                    ShrRenew = 0
                    tot = 0
                    For e% = 1 To NET%
                        'TradeRestraint = 0 'update restraints 'not actually used later so commented out and left for reference
                        'ShrRenew = (ENP(R%, 4) + ENP(R%, 6)) / (ENP(R%, 1) + ENP(R%, 2) + ENP(R%, 3) + ENP(R%, 4) + ENP(R%, 5) + ENP(R%, 6)) 'Oil, gas, coal, hydro, nuclear, renewable
                        'IShrRenew(R%) = 0
                        If e% > 3 Then ShrRenew = ShrRenew + ENP(R%, e%)
                        tot = tot + ENP(R%, e%)
                    Next e%
                    ShrRenew = ShrRenew / tot
                    If ShrRenew > IShrRenew(R%) Then 'no else, leave usual behavior
                        tmpShrRenewChng = (1 - ShrRenew) / (1 - IShrRenew(R%))
                        tmpShrRenewChng = renewtoenexsw * (1 - tmpShrRenewChng) 'subtract from 1 to get distance then rescale; would need different setup if values could be greater than 1
                        tmpShrRenewChng = 1 - tmpShrRenewChng 'remove rescaled contribution to get multiplier 
                        ENX(R%) = tmpShrRenewChng * ENX(R%)
                    End If
                End If
                'clp 2024/08/07 ***END*** Adding ability to have proportional reductions in exports as renewable energy becomes more prominent

                'jrs 2020/06/23 *** begin ***
                If gdprext(R%) > -99 Then
                    Dim factorm As Single
                    Dim factorx As Single
                    'jrs 2020/07/06 changed parameter name
                    factorm = AMAX(1 + (gdprext(R%) / 100 - SmoothGDPR(R%)) * elasmincshock(R%, 2), 0.00001F) 'jrs 2020/07/01 added AMAX(,0)
                    factorx = AMAX(1 + (gdprext(R%) / 100 - SmoothGDPR(R%)) * elasxincshock(R%, 2), 0.00001F) 'jrs 2020/07/01 added AMAX(,0)
                    ENM(R%) = ENM(R%) * factorm
                    ENX(R%) = ENX(R%) * factorx
                End If
                'jrs 2020/06/23 *** end ***

                'bbh 2020/05/07 start Am nervous that done after all restraints
                ENM(R%) = ENM(R%) * enmm(R%)
                ENX(R%) = ENX(R%) * enxm(R%)
                'bbh 2020/05/07 end

                'jrs 2021/01/08 *** begin ***
                '---- Adjust Exports based on swing in Imports from main partners ----
                Call Savemdbasediff(ENM(R%), R)
                'jrs 2021/01/08 *** end ***

                wxc = wxc + ENX(R%)
                wmd = wmd + ENM(R%)

            Next R%

            'jrs 2021/01/08
            Call UpdateXSPhysicalwithDyad(wxc, ENX)

            'jrs 2024/11/13
            Call UpdateMSPhysicalwithDyad(wmd, ENM)

            '      WT = (WXC + WMD - TradeRestraint) / 2
            Dim WT = (wxc + wmd) / 2
            Dim mulmlev = 0!
            For R% = 1 To NR%
                ENX(R%) = ENX(R%) * WT / wxc
                ENM(R%) = ENM(R%) * WT / wmd
                EnTot = 0
                For e% = 1 To NEP%  'Oil, gas, coal, hydro, nuclear, renewable
                    EnTot = EnTot + ENP(R%, e%)
                Next e%

                'dkb 2019/04/05 - Removing the demand term from export base (reduction in demand should increase the export base) - START
                'exportbase = smoothentot(R%) + smoothpendem(R%)
                exportbase = smoothentot(R%)
                'dkb 2019/04/05 - END

                'bbh 2019/08/17 Change export base to include all of production
                exportbase = SmoothXEnBase(R%)

                'clp 2016/08/24 - Adding variable and save variable for energy production
                EnTotal(R%) = smoothentot(R%)
                SEnTotal(R%) = EnTotal(R%)

                XKAVE(R%) = XKAVE(R%) * xhw + (1.0! - xhw) * ENX(R%) / exportbase
                'If RegName$(R%) = "Saudi Arabia" And IY% > 23 Then Stop

                mkave(R%) = mkave(R%) * mhw + (1.0! - mhw) * ENM(R%) / smoothpendem(R%)
                Dim ImportTarget = ConvergeOverTime(mkavei(R%), 0, 150) 'Base, Target, Years
                Diff1 = mkave(R%) - ImportTarget
                Diff2 = Diff1 - SImportDev(R%)

                Call ADJSTR(1, Diff1, Diff2, -0.1, -0.2, mulmlev)
                mulmlev = Amin(1, mulmlev)
                mkave(R%) = mkave(R%) * mulmlev

                XKAVE(R%) = AMAX(0.001, Amin(XKAVE(R%), XKAVMAX(R%)))
                mkave(R%) = AMAX(0.001, Amin(mkave(R%), MKAVMax(R%)))

                'dkb 2014/08/06 - Adding save variables for Energy Trade componenets of POWER, all but first - START
                SENX(R%) = ENX(R%)
                SENM(R%) = ENM(R%)
                'dkb 2014/08/06 - END

            Next R%
            'BBH 7/15/04 V5_01 End

            'jrs 2025/04/29
            'If bilateralsw > 0 Then
            '    UpdateEnergyProdWithChangeInImports()
            'End If

            '     All but First:  Stocks and Shortages
            '     ------------------------------------
            WorldKen = 0
            WorldEnp = 0
            Dim WENDEM = 0!
            Dim WEnDemIm = 0!
            Dim wstbase = 0!
            Dim wenstks = 0!
            Dim wenpm = 0!
            Dim wkenenpr = 0!

            '      TENP = 0
            For e% = 1 To Renew
                WENP(e%) = 0
            Next e%

            For R% = 1 To NR%
                'WEnDem = WEnDem + PENDEM(R%)
                WENDEM = WENDEM + smoothpendem(R%)
                If XKAVE(R%) < 0.2 Then
                    WEnDemIm = WEnDemIm + smoothpendem(R%)
                    'If enpm(R%, 1) < 1 Then wenpm = 1 'production restriction on
                End If
                If enpm(R%, 1) < 1 Then wenpm = 1 'production restriction on
                WorldEnp = WorldEnp + smoothentot(R%)
                '         SENP(R%) = 0!
                ENST(R%) = senst(R%)
                For e% = 1 To Renew 'Oil, gas, coal, hydro, nuclear, renewable
                    'WorldEnp = WorldEnp + ENP(R%, e%)
                    '            SENP(R%) = SENP(R%) + ENP(R%, E%)
                    'Stock base for Price Change Not Affected by Capacity
                    'If GRPS(1, R) = "India" And e = Solar And IY >= 2 Then Stop
                    ENP(R%, e%) = ENP(R%, e%) * sENPCAPUT(R%)  'jrs 2021/11/15 changed to sENPCAPUT(R%)
                    ENST(R%) = ENST(R%) + ENP(R%, e%)
                Next e%

                'If RegName$(R%) = "Unitd Kingdm" Then Stop
                ENST(R%) = ENST(R%) - ENDEM(R%) - ENX(R%) + ENM(R%)
                'StBase = SENP(R%) + PENDEM(R%)
                StBase = smoothentot(R%) + smoothpendem(R%)
                Diff1 = ENST(R%) - dstlen * StBase 'excess stocks
                wstbase = wstbase + StBase

                If enxl(R%) > 0.001 AndAlso Diff1 > 0 Then 'In case of export constraint
                    Dim OilGasProd = AMAX(0.001, ENP(R%, Oil) + ENP(R%, Gas))
                    Dim ReducedEnProd = Amin(OilGasProd - 0.001, Diff1)
                    '            ReducedOilProd(r%) = Amin(ENP(r%, Oil) - 0.001, Diff1)
                    ReducedOilProd(R%) = CSng(ReducedEnProd * ENP(R%, Oil) / OilGasProd)
                    ReducedGasProd(R%) = CSng(ReducedEnProd * ENP(R%, Gas) / OilGasProd)
                    ENP(R%, Oil) = ENP(R%, Oil) - ReducedOilProd(R%)
                    ENP(R%, Gas) = ENP(R%, Gas) - ReducedGasProd(R%)
                    ENST(R%) = ENST(R%) - ReducedOilProd(R%) - ReducedGasProd(R%)
                    '            SENP(R%) = SENP(R%) - ReducedOilProd(R%) - ReducedGasProd(R%)
                Else
                    ReducedOilProd(R%) = 0
                    ReducedGasProd(R%) = 0
                End If
                'jrs 2023/10/25
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("ENP", "Total", "", CStr(FirstYear + IY - 1), R%, context)
                End If
                'jrs 2023/11/10
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("ENP", "OthRenew", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                wenstks = wenstks + ENST(R%) - sensho(R%)
                'jrs 2023/10/25
                SENPTOT(R%) = 0
                For e% = 1 To Renew
                    '            WorldEnp = WorldEnp + ENP(r%, E%)
                    WENP(e%) = WENP(e%) + ENP(R%, e%) 'Preliminary calc
                    '   BBH 7/14/04 V5_01
                    WorldKen = WorldKen + sken(R%, e%) * sENPCAPUT(R%) / AMAX(5, lke(e%))  'jrs 2021/11/15 changed to sken, sENPCAPUT
                    'Reduce KEN by capacity because ENP in WorldEnp is reduced
                    'Debug.Print(CStr(R%) + "," + CStr(e%) + "," + CStr(ken(R, e)) + "," + CStr(ENPCAPUT(R%)) + "," + CStr(lke(e%)) + "," + CStr(smoothentot(R)))
                    'jrs 2023/10/25
                    SENPTOT(R%) = SENPTOT(R%) + ENP(R%, e%)
                Next e%
                'jrs 2023/11/10
                SENPRENEW(R%) = ENP(R%, Renew)
                'ttt why in loop- gets value from last R?
                wkenenpr = WorldKen / WorldEnp  'TENP same as WorldEnp
            Next R%
            '
            '    All but First:  Shortfalls
            '    ---------------------------
            '       IFs does not have demand by energy type; all energy fungible
            '       But if one type of production falls sharply, would create a shortfall
            '           for users dependent on it.  Should pass through to prices and shortages
            '       This approach can be replaced if energy demand is differentiated
            '       Deals mostly with oil and gas production/export embargoes
            '
            Dim GlobalShortFall = 0!
            Dim WorldEnProd = 0!
            If wenpm > 0.5 Then 'There is a scenario oil production restriction
                For e% = 1 To Renew
                    GlobalShortFall = CSng(GlobalShortFall + AMAX(0, SWENP(e%) - WENP(e%) * 1.05))
                    WorldEnProd = WorldEnProd + WENP(e%)
                Next e%
            End If
            '     If IY% >= 8 Then Stop
            '
            '    All but First:  Prices
            '    ----------------------

            WGDP = 0
            'Swenstks = 0!
            'cumwmul = 0!
            If IY% = 2 Then
                Swenstks = wenstks
                cumwmul = 1
            End If
            Dim ShortFallSub = GlobalShortFall * Amin(10, WorldEnProd / AMAX(0.1, WENP(Oil)))
            Dim wdiff1 = wenstks - dstlen * wstbase - ShortFallSub
            Dim wdiff2 = wenstks - Swenstks - ShortFallSub
            Dim wmul = 0!
            If wenpm > 0.5 Then 'Oil production restriction on
                Call ADJSTR(wstbase, wdiff1, wdiff2, -0.2 * 3, -0.4 * 3, wmul)
            Else
                Call ADJSTR(wstbase, wdiff1, wdiff2, -0.2, -0.4, wmul)
                '        Call ADJSTR(wstbase, wdiff1, wdiff2, -0.4, -0.8, wmul)
            End If
            'cumwmul = cumwmul * wmul
            'bbh 2011/10/17
            cumwmul = cumwmul * (1 - eprohw) + cumwmul * wmul * eprohw


            For R% = 1 To NR%
                'If RegName$(R%) = "Egypt" And IY% >= 50 Then Stop

                'StBase = SENP(R%) + PENDEM(R%) 'Use projected energy demand
                StBase = smoothentot(R%) + smoothpendem(R%) 'Use projected energy demand
                Diff1 = ENST(R%) - sensho(R%) - dstlen * StBase 'Needed also in capacity utilization
                Diff2 = ENST(R%) - sensho(R%) - senst(R%)
                If (enprix(R%) > 0.1) Then
                    ENPRI(R%) = enprix(R%)
                Else
                    'epra = -0.3 'bbh 2021/09/30  Jose: better to put this parameter change in GlobalParameters table; please do
                    Call ADJSTR(StBase, Diff1, Diff2, epra, eprafs, Mul)
                    MarkUpEn(R%) = MarkUpEn(R%) * Mul
                    'ENPRI(R%) = SWEP * wkenenpr / wkenenpri * Mul * cumwmul + encartpp
                    If enprixi < 1 Then
                        ENPRI(R%) = iwep * wkenenpr / wkenenpri * Mul * cumwmul + encartpp
                    Else
                        ENPRI(R%) = enprixi * wkenenpr / wkenenpri * Mul * cumwmul + encartpp
                    End If
                    'BBH 6/15/04  V5_0  Need to increase range when go from dollars/barrel to index
                    If wenpm > 0.5 Then 'Restriction on, allow prices to rise
                        ENPRI(R%) = AMAX(SENPRI(R%) - 10,
                            Amin(ENPRI(R%), SENPRI(R%) + encartpp - SEnCartPP + 100))
                    Else
                        ENPRI(R%) = AMAX(SENPRI(R%) - 10,
                            Amin(ENPRI(R%), SENPRI(R%) + encartpp - SEnCartPP + 20))
                    End If
                    'ENPRI(R%) = AMAX(SENPRI(R%) - 2.5, _
                    'Amin(ENPRI(R%), SENPRI(R%) + encartpp - SEnCartPP + 5))
                End If
                SENPRI(R%) = ENPRI(R%)
                ENSHO(R%) = 0!
                sensho(R%) = 0!
                'If RegName$(R%) = "United States of America" And IY% >= 20 Then Stop

                If (ENST(R%) < 0) Then
                    ENSHO(R%) = -ENST(R%)
                    sensho(R%) = ENSHO(R%)
                    ENST(R%) = 0!
                End If
                If XKAVE(R%) < 0.2 Then
                    ENSHO(R%) = ENSHO(R%) + smoothpendem(R%) / WEnDemIm * GlobalShortFall
                End If
                SReducedOilProd(R%) = ReducedOilProd(R%)
                SReducedGasProd(R%) = ReducedGasProd(R%)
                WGDP = WGDP + SGDP(R%)
                '
                '        All but First:  Capacity Utilization
                '        ------------------------------------
                'elenpst = -0.4 'bbh 2021/09/30  Jose, please put these two lines of parameter change in Global Parameters rather than here
                'elenpst2 = -0.8 'bbh 2021/09/30

                Call ADJSTR(StBase, Diff1, Diff2, elenpst, elenpst2, Mul)
                '         Call ADJSTR(StBase, Diff1, Diff2, -0.2, -0.4, Mul)
                'cputf(R%) = 0.5 * cputf(R%) + 0.5 * Mul
                'cputf(R%) = CSng(0.5 * cputf(R%) + 0.5 * Mul * cputf(R%)) 'bbh 2021/09/30 added cputf, was major omission
                ENPCAPUT(R%) = CSng(0.5 * sENPCAPUT(R%) + 0.5 * Mul * sENPCAPUT(R%))  'jrs 2021/11/15 changed to sENPCAPUT(R%)
                ENPCAPUT(R%) = ConvergeOverTime(ENPCAPUT(R%), 1, 100) 'Base, Target, Years
                'ENPCAPUT(R%) = Mul
                ENPCAPUT(R%) = AMAX(0.2, Amin(ENPCAPUT(R%), 2))
                'jrs 2021/11/15
                sENPCAPUT(R%) = ENPCAPUT(R%)
            Next R%
            'If IY% = 50 Then Stop
            SEnCartPP = encartpp

            'If IY% >= 50 Then Stop
            '
            '     All but First: World Energy Price
            '     ---------------------------------
            TENP = 0!
            TENPRI = 0!
            Dim TENPRINoTax = 0!
            For R% = 1 To NR%
                For e% = 1 To Renew
                    TENP = TENP + ENP(R%, e%)
                    If (enprix(R%) > 0.1) Then
                        TENPRI = TENPRI + SENPRI(R%) * ENP(R%, e%)
                        TENPRINoTax = TENPRINoTax + SENPRI(R%) * ENP(R%, e%)
                    Else 'adjust carbon tax increment from per barrel (IWEPBYear) to index base (IWEP)
                        TENPRI = TENPRI + (SENPRI(R%) + cartaxenpriadd(R%) * iwep / iwepbyear) * ENP(R%, e%)
                        TENPRINoTax = TENPRINoTax + SENPRI(R%) * ENP(R%, e%)
                    End If
                    '            WorldKen = WorldKen + ken(r%, E%) / AMAX(5, lke(E%))
                Next e%
            Next R%

            '      wkenenpr = WorldKen / TENP  'TENP same as WorldEnp
            '
            WEP = CSng(TENPRI / AMAX(TENP, 0.001))  'Unlike ENPRI, includes carbon tax
            'ttt need global??
            Dim WEPNoTax = TENPRINoTax / AMAX(TENP, 0.001)

            swep = WEP
            WEPBYEAR = iwepbyear * WEP / iwep 'computes oil price in base year prices
            '
            '     All but First:  Energy Returns - Total Energy Sector
            '     ----------------------------------------------------
            For R% = 1 To NR%
                Dim ProdCost = 0!
                Dim EnReturn = 0!
                For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable
                    ProdCost = ProdCost + sken(R%, e%) / AMAX(5, lke(e%))  'jrs 2021/11/15 changed to sken
                    'BBH 7/15/04 V5_01
                    'EnReturn = EnReturn + ENP(R%, e%) * WEP
                    EnReturn = CSng(EnReturn + ENP(R%, e%) * WEPNoTax) '(tax does not increase return)
                    'World price provides stability; use without capacity factor
                    'to obtain true capital costs
                Next e%
                EnReturn = EnReturn / ProdCost
                If IY% = 2 Then EnReturnI(R%) = EnReturn
                EPROFITR(R%) = SEProfitR(R%) * eprohw + EnReturn / EnReturnI(R%) * (1 - eprohw)
            Next R%
            '
            '   All but First:  Total Investment Demand to Take to Economic Model
            '   -----------------------------------------------------------------

            Diff1 = wenstks - dstlen * wstbase
            Diff2 = wenstks - Swenstks
            Swenstks = wenstks
            Dim mulwst = 0!
            Call ADJSTR(wstbase, Diff1, Diff2, elenpr, elenpr2, mulwst)
            '    Call ADJSTR(WstBase, Diff1, Diff2, -0.2, -0.4, MulWSt)
            '    Call ADJSTR(WstBase, Diff1, Diff2, -0.4, -0.8, MulWSt)
            '  BBH 7/14/04 V5.01 Begin
            Dim mulendem = (WENDEM / WGDP) / SWENDEMR
            SWENDEMR = WENDEM / WGDP
            Dim mulkenenpr = wkenenpr / Swkenenpr
            Swkenenpr = wkenenpr

            Dim DesStocks = dstlen * wstbase
            '    mulstocks = (DesStocks / AMAX(0.5 * DesStocks, Amin(1.5 * DesStocks, wenstks)))
            Dim mulstocks = (DesStocks / AMAX(0.5 * DesStocks, Amin(4 * DesStocks, wenstks)))
            Dim mulrprof = 0!
            For R% = 1 To NR%
                Diff1 = EPROFITR(R%) - 1
                Diff2 = EPROFITR(R%) - SEProfitR(R%)
                Call ADJSTR(1, Diff1, Diff2, eleniprof, eleniprof2, mulrprof)
                Dim ImportTarget = ConvergeOverTime(mkavei(R%), 0, 150) 'Base, Target, Years
                Diff1 = mkave(R%) - ImportTarget
                Diff2 = Diff1 - SImportDev(R%)
                Call ADJSTR(1, Diff1, Diff2, 0.2, 0.4, mulmlev)
                mulmlev = AMAX(1, mulmlev)
                SImportDev(R%) = Diff1
                'bbh 2011/10/17
                Dim mulrenew As Single = 1

                'mti 2024/08/20 start
                'If GRPS(1, R) = "India" And IY >= 2 Then Stop
                If (ENP(R%, Solar) + ENP(R%, Wind) + ENP(R%, Geothermal) + ENP(R%, Renew)) > 0.7 * ENDEM(R%) Then
                    'If  ENP(R%, Renew) > 0.7 * ENDEM(R%) Then
                    'If GRPS(1, R) = "India" Then Stop
                    'If GRPS(1, R) = "China" Then Stop
                    'mulrenew = CSng(AMAX(0.5, 1 - (ENP(R%, Renew) / ENDEM(R%) - 0.7) * 1.0#))
                    mulrenew = CSng(AMAX(0.5, 1 - ((ENP(R%, Solar) + ENP(R%, Wind) + ENP(R%, Geothermal) + ENP(R%, Renew)) / ENDEM(R%) - 0.7) * 1.0#))
                End If
                'mti 2024/08/20 end

                ' sendeminvr(R%) = tineed(R%) / ENDEM(R%)
                'bbh 2011/10/16
                'tineed(R%) = SGDP(R%) * sgdpenr(R%) * mulwst * mulrprof
                tineed(R%) = ENDEM(R%) * (wkenenpr / wkenenpri) * sendeminvr(R%) * mulwst * mulrprof
                tineed(R%) = CSng(tineed(R%) * mulendem * mulkenenpr * (mulstocks ^ 0.5) * mulrenew)
                Dim Reduc = Amin(invreduc(R%) * 0.7, 0.6 * tineed(R%))
                tineed(R%) = CSng(tineed(R%) - Reduc)
                'sgdpenr(R%) = 0.85 * sgdpenr(R%) + 0.15 * (TINEED(R%) / sgdp(R%))
                'bbh 2011/10/17
                'sgdpenr(R%) = 0.95 * sgdpenr(R%) + 0.05 * (tineed(R%) / SGDP(R%))
                sendeminvr(R%) = CSng(0.95 * sendeminvr(R%) + 0.05 * (tineed(R%) / (ENDEM(R%) * wkenenpr / wkenenpri)))
                ''sgdpenr(R%) = 0.9 * sgdpenr(R%) + 0.1 * (TINEED(R%) / sgdp(R%))

                'If RegName$(R%) = "China" And IY% > 50 Then Stop
                tineed(R%) = tineed(R%) * eninvm(R%)
                'jrs 2022/04/18
                tineed(R%) = tineed(R%) + eninvadd(R%)

                SEProfitR(R%) = EPROFITR(R%)
                '  BBH 7/14/04 V5.01 End
                '  This is a key long-term adjustment dynamic subject to many forces
                '  Let base unit/GDP drift with global stock levels (MulWSt)
                '  Also let drift with regionally-specific profit rates (MulRProf), but
                '      very slowly (unlike Ag) because these diverge sharply
                '      with changing energy mix; earlier used this term as multiplier only
                '  Adjust quickly in face of global stock levels (MulStocks)
                '  Multiply by changes in global endem/GDP ratio over time (MulEnDem)
                '  Multiply by changes in global capital/energy production ratio,
                '       modified by capital lifetime (MulKenEnpR)
                '
                '     All but First:  Energy Returns by Energy Type
                '     ---------------------------------------------
                'emtax(R%) = 10 * IY%
                emtax(R%) = 2 * IY%
                Dim AvgCarFuel = (carfuel1 + carfuel2 + carfuel3) / 3
                Dim avgreturn = 0!
                Dim TotProd = 0!
                Dim EnCost = 0!
                For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable
                    If e% = 1 Then
                        'jrs 2021/11/15 changed to sken
                        EnCost = sken(R%, e%) / lke(e%) + ENP(R%, e%) * carfuel1 * carbtax(R%) +
                            AMAX(-sken(R%, e%) / lke(e%) / 2, ENP(R%, e%) * (carfuel1 - AvgCarFuel) * emtax(R%))
                    ElseIf e% = 2 Then
                        EnCost = sken(R%, e%) / lke(e%) + ENP(R%, e%) * carfuel2 * carbtax(R%) +
                            AMAX(-sken(R%, e%) / lke(e%) / 2, ENP(R%, e%) * (carfuel2 - AvgCarFuel) * emtax(R%))
                    ElseIf e% = 3 Then
                        EnCost = sken(R%, e%) / lke(e%) + ENP(R%, e%) * carfuel3 * carbtax(R%) +
                            AMAX(-sken(R%, e%) / lke(e%) / 2, ENP(R%, e%) * (carfuel3 - AvgCarFuel) * emtax(R%))
                    Else
                        EnCost = sken(R%, e%) / lke(e%)
                    End If
                    Dim EnReturnS = ENP(R%, e%) / EnCost
                    avgreturn = avgreturn + EnReturnS * ENP(R%, e%)
                    TotProd = TotProd + ENP(R%, e%)
                    If IY% = 2 Then enreturnsi(R%, e%) = ENP(R%, e%) / (sken(R%, e%) / lke(e%))
                    eprofitrs(R%, e%) = CSng(EnReturnS / AMAX(enreturnsi(R%, e%), 0.001)) 'jrs 2013/04/23 added amax(x, 0.001)
                    'If RegName$(R%) = "United States of America" And e% = 1 And IY% > 5 Then Stop
                    'In contrast to total energy profit, which takes total energy price
                    'levels into account,
                    'the sectoral profit levels are meant to reflect relative profit movements,
                    'and they do not involve price levels
                Next e%
                'avgreturn = avgreturn / TotProd
                avgreturn = avgreturn / smoothentot(R%)
                '
                '     All but First:  Energy Investment Demand by Energy Type
                '     -------------------------------------------------------
                For e% = 1 To Renew

                    'mti 2024/07/23 start
                    ineed(R%, e%) = eninvri(R%, e%) * sken(R%, e%)  'jrs 2021/11/15 changed to sken
                    'ineed(R%, e%) = CSng(0.11 * sken(R%, e%)) 'jrs 2021/11/15 changed to sken

                    ''mti 2024/08/20 start
                    'ineed(R%, e%) = CSng(sken(R%, e%) * (ENPRR(R%, e%) + 1.0! / lke(e%)))
                    ''mti 2024/07/20 end 

                    'ineed(R%, e%) = CSng(0.11 * sken(R%, e%)) 'jrs 2021/11/15 changed to sken
                    'mti 2024/07/23 end
                    '            ineed(R%, e%) = 0.5 * ineed(R%, e%) * eprofitrs(R%, e%) ^ elass(R%, e%) + _
                    '                0.5 * ineed(R%, e%) * _
                    '                (eprofitrs(R%, e%) * enreturnsi(R%, e%) / avgreturn) ^ elass(R%, e%)
                    '                  '1/2 of change dependent on change in costs and 1/2 dependent on
                    '                  'relative costs
                    ineed(R%, e%) = CSng(ineed(R%, e%) * eprofitrs(R%, e%) ^ elass(R%, e%))

                    'If GRPS(1, R%) = "France" And IY > 2 Then Stop
                    'If GRPS(1, R%) = "China" And IY > 2 Then Stop
                    'If GRPS(1, R%) = "India" And IY > 2 Then Stop
                    'If GRPS(1, R%) = "Indonesia" And IY > 2 Then Stop
                    'If GRPS(1, R%) = "United States of America" And IY > 2 Then Stop

                    'If RegName$(R%) = "South Africa" And (e% = 3 Or e% = 6) And IY% > 40 Then Stop
                    ineed(R%, e%) = CSng(AMAX(0.8 * sineed(R%, e%),
                        Amin(1.4 * sineed(R%, e%), ineed(R%, e%))))
                    If (eprodr(R%, e%) > 0!) Then   'Override with exogenous target growth
                        ineed(R%, e%) = sken(R%, e%) * (eprodr(R%, e%) + 1.0! / lke(e%))   'jrs 2021/11/15 changed to sken
                    End If
                    'If RegName$(R%) = "China" And (e% = 1 Or e% = 6) And IY% > 25 Then Stop
                    'jrs 2022/04/18
                    'jrs 2022/05/02 If GRPS(1, R%) = "Guinea Bissau" And e% = Renew And context.HistoricMode = False Then
                    'mti 2024/07/23  start
                    'If ennoprodsw(R%) = 1 And e% = Renew And context.HistoricMode = False Then
                    If ennoprodsw(R%) = 1 And (e% >= Solar And e% <= Renew) And context.HistoricMode = False Then
                        'If IY >= 7 Then Stop
                        'ineed(R%, e%) = ENP(R%, Renew) * QE(R%, Renew)
                        ineed(R%, e%) = ENP(R%, e%) * QE(R%, e%)
                    End If

                    'mti 2024/07/23  end
                Next e%
                '
                '     All but First:  Bound Investment by Reserves, Other Restrictions
                '     ---------------------------------------------------------------
                invreduc(R%) = 0
                'jrs 2013/04/22 NET%
                Dim maxinv = 0!
                Dim mininv = 0!
                For e% = 1 To Hydro  'Oil, gas, coal, hydro, nuclear
                    '            fac = 1!
                    '            If (e% = Oil) Then fac = SENPR(R%)

                    '            MaxInv = (RESER(R%, e%) / AMAX(prodtf(R%, e%), 0.001) - ENPPOT(R%, e%) * fac + _
                    'ENP(R%, e%) / lke(e%)) * qe(R%, e%)
                    maxinv = CSng((RESER(R%, e%) / AMAX(prodtf(R%, e%), 0.001) - enppot(R%, e%) +
                    ENP(R%, e%) / lke(e%)) * QE(R%, e%))
                    'Use ENPPOT because may have too much capital and ENP was constrained earlier
                    '            mininv = ENP(R%, e%) * 0.5 * 1 / lke(e%) * QE(R%, e%)
                    mininv = CSng(ENP(R%, e%) * 0.3 * 1 / lke(e%) * QE(R%, e%))
                    If enpoilmax(R%) > 0.001 AndAlso e% = Oil Then
                        If ENP(R%, e%) >= enpoilmax(R%) Then maxinv = mininv
                    End If
                    If enpgasmax(R%) > 0.001 AndAlso e% = Gas Then
                        If ENP(R%, e%) >= enpgasmax(R%) Then maxinv = mininv
                    End If
                    If enpcoalmax(R%) > 0.001 AndAlso e% = Coal Then
                        If ENP(R%, e%) >= enpcoalmax(R%) Then maxinv = mininv
                    End If

                    If e% = 3 AndAlso context.HistoricMode Then
                        Dim CoalMaxProd = ENDEM(R%) * EnpCoalDemRI(R%)
                        Dim CoalMaxInv = (CoalMaxProd - enppot(R%, e%) +
                            ENP(R%, e%) / lke(e%)) * QE(R%, e%)
                        maxinv = Amin(maxinv, CoalMaxInv)
                    End If 'Coal bound Historically only

                    If e% = 4 Then
                        Dim HydroMaxProd = ENDEM(R%) * EnpHydroDemRI(R%) * 2
                        Dim HydroMaxInv = (HydroMaxProd - enppot(R%, e%) +
                            ENP(R%, e%) / lke(e%)) * QE(R%, e%)
                        maxinv = Amin(maxinv, HydroMaxInv)
                    End If 'Hydro can't more than double its share of energy consumption


                    maxinv = AMAX(maxinv, mininv)

                    'If RegName$(R%) = "United States of America" And IY% = 38 Then Stop

                    invreduc(R%) = CSng(invreduc(R%) + AMAX(0, ineed(R%, e%) - maxinv))
                    ineed(R%, e%) = CSng(AMAX(mininv, Amin(maxinv, ineed(R%, e%))))

                Next e%

                'If RegName$(R%) = "China" Then Stop

                maxinv = CSng((enppot(R%, Nucl) * 0.01 +
                    ENP(R%, Nucl) / lke(Nucl)) * QE(R%, Nucl))
                mininv = CSng(ENP(R%, Nucl) * 0.5 * 1 / lke(Nucl) * QE(R%, Nucl))
                invreduc(R%) = invreduc(R%) + AMAX(0, ineed(R%, Nucl) - maxinv)
                ineed(R%, Nucl) = AMAX(mininv, Amin(maxinv, ineed(R%, Nucl)))

                ' If RegName$(R%) = "China" Then Stop

                Dim TINeedBound = 0!
                ' If GRPS(1, R) = "United States of America" And IY > 10 Then Stop
                For e% = 1 To NEP%

                    'mti 2025/01/25 start
                    ineed(R%, e%) = AMAX(0, ineed(R%, e%))
                    'mti 2025/01/25 end

                    sineed(R%, e%) = ineed(R%, e%)
                    TINeedBound = TINeedBound + ineed(R%, e%)
                Next e%
                'TINeed(R%) = Amin(TINeed(R%), TINeedBound)
                If TINeedBound < tineed(R%) Then tineed(R%) = (tineed(R%) + TINeedBound) / 2
                'If TINeedBound < TINeed(R%) Then TINeed(R%) = Amin(TINeed(R%), TINeedBound * 1.2)
                'TINEED was calcuated as responsive to systemic need; INeeds reflect some
                'local limits; need formulation that gives attention to both

            Next R%
            'mti June 08, 2011 -start
            '    'jrs 2009/10/27
            '    'All but first: forecast fosil fuel use
            '    '--------------------------------------------
            '    For R% = 1 To NR%
            '        'jrs 2010/01/08
            '        If ensolflhldsw = 0 Then
            '            'jrs 2010/04/10 using lagged version of Education
            '            Call AnalFunc("Percent of People using Primarily Solid Fuels (Full Model)", _
            '            Array(GDPPCP(R%), SEDYRSAG25(R%), SINCOMELT1LN(R%) / POP(R%) * 100), cVal)
            '            ENSOLFUEL(R%) = ENSOLFUELShift(R%) * (100 * Exp(cVal) / (Exp(cVal) + 1))
            '
            '            'mti June 08, 2011 -start
            '            'meet solid fuel, no use target
            '            If ensolfueltrgtyr(R%) > 0 Then
            '                DistTrgt = AMAX(0, sENSOLFUEL(R%) - 0)
            '                TimeLeft = AMAX(1, ensolfueltrgtyr(R%) + 2 - IY)
            '                ENSOLFUELTrgt = AMAX(0, sENSOLFUEL(R%) - DistTrgt / TimeLeft)
            '                ENSOLFUEL(R%) = Amin(ENSOLFUELTrgt, ENSOLFUEL(R%))
            '            End If
            '            'parameter goal seek - end
            '            'mti June 08, 2011 -end
            '
            '
            '            ENSOLFUEL(R%) = ENSOLFUEL(R%) * ensolfuelm(R%)
            '            ENSOLFUEL(R%) = Amin(AMAX(ENSOLFUEL(R%), 0), 100)
            '        Else 'if Solid Fueld Hold switch is On
            '            ENSOLFUEL(R%) = ENSOLFUELI(R%)
            '        End If
            '
            '        'mti June 08, 2011 -start
            '        sENSOLFUEL(R%) = ENSOLFUEL(R%)
            '        'mti June 08, 2011 - end
            '
            '    Next R%
            'mti June 08, 2011 - end

            SubAllYears()
        End Sub

        Public Sub IFsEn2AllYears(context As IForecastContext)
            '
            '     SECOND CALL OF ENERGY
            '     -----------------------------------------------------------------------
            '
            'SecondCall:
            Dim hwf2 = AMAX(0, Amin(hwf, 1)) ^ IY%
            'Used to smooth reactions to initial GDP flucations in real-data years

            '      wfor = 0
            'ttt 2019/10/08 TakeOut
            Array.Copy(GDPPOT, SGDP, NR + 1)
            Array.Copy(ENST, senst, NR + 1)
            For R% = 1 To NR%
                '         wfor = wfor + LD(R%, Fors)
                '         sgdpp(R%) = sgdpp(R%) * (1 + igdpr(R%)) * hwf + GDPP(R%) * (1 - hwf)
                sgdpp(R%) = CSng(sgdpp(R%) * (1 + igdprppp(R%)) * hwf2 + GDPP(R%) * (1 - hwf2))

                'BBH 8/12/04 v5_01
                'ttt 2019/10/08 SGDP(R%) = GDPPOT(R%)
                'BBH 7/15/04 v5_01
                'ttt 2019/10/08 senst(R%) = ENST(R%)
                '
                '     All Years 2:  Recalculate TINEED as Sum of Investment Demand by Type;
                '       Use to Normalize
                '     ---------------------------------------------------------------------
                tineed(R%) = 0
                For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable
                    tineed(R%) = tineed(R%) + ineed(R%, e%) * eninvtm(R%, e%)
                Next e%

                Dim inen = tineed(R%)
                If (enon > 0.5) Then inen = CSng(IDS(R%, 2) / AMAX(sidsf(R%), 0.001))
                For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable
                    'jrs 2021/11/30 If (tineed(R%) >= 0.001) Then Maldives blows up in 1961
                    'jrs 2022/03/25 If (tineed(R%) >= 0.0001) Then Guinea Bissau blows up in 2009


                    If (tineed(R%) >= 0.00001) Then
                        ineed(R%, e%) = inen * ineed(R%, e%) * eninvtm(R%, e%) / tineed(R%)
                        Dim KEN_Shr = ineed(R%, e%) - sken(R%, e%) / lke(e%)  'jrs 2021/11/15 changed to sken
                        ken(R%, e%) = (sken(R%, e%) + KEN_Shr) * (1 - CIVDM(R%))  'jrs 2021/11/15 changed to sken

                        'If GRPS(1, R) = "United States of America" And e = 6 And IY > 2 Then Stop


                        ken(R%, e%) = AMAX(0.00001, ken(R%, e%)) 'bbh 2013/11/20



                        'ken(R%, e%) = AMAX(0.001, ken(R%, e%))
                        'jrs 2021/11/15
                        'sken(R%, e%) = ken(R%, e%)   'bbh 2023/12/31+ replace this with if-then below
                        'The reason: oil production and export value =>GDP in early/exogenous years and grows much even as exogenous values of GDP jump + and -
                        'in large swings; model forces non ag/en sectors to absorb all of swings in exog GDP creating huge stock messes for other sectors
                        'and setting up crazy behavior for early post exog GDP years and longer term; need to constrain oil production to smooth all out
                        '   WHen the base year of IFs is beyond huge swings in GDP and oil is smaller share of GDP, probably take this if-then out and let model compute 
                        '   changes in oil production
                        If (GRPS(1, R) = "Sudan South") And e% = 1 And IY% < 10 Then
                            sken(R%, e%) = CSng(sken(R%, e%) * 1.03) 'bbh 2023/12/31+ 
                        ElseIf (GRPS(1, R) = "Sudan South") And e% = 1 And IY% >= 10 Then
                            sken(R%, e%) = CSng(sken(R%, e%) * ConvergeOverTimeFromStartYear(1.03, 1, 20, 10))
                            'ConvergeOverTimeFromStartYear(ByVal Base As Single, ByVal Target As Single, ByVal YearsToConverge As Integer, Optional StartYr As Integer = 2) As Single
                        Else
                            sken(R%, e%) = ken(R%, e%)
                        End If

                    End If
                    If IY% >= 2 Then    'If recompute 1st year, changes initial values
                        'jrs 2022/04/12 ENPRR(R%, e%) = ineed(R%, e%) / ken(R%, e%) - 1.0! / lke(e%)
                        ENPRR(R%, e%) = CSng(ineed(R%, e%) / AMAX(ken(R%, e%), 0.00001) - 1.0! / lke(e%))
                        'If RegName$(R%) = "China" And e% = Nucl Then Stop

                    End If

                    '     bbh2013/06/09
                    '     All Years 2:  Undiscovered Resources
                    '     ----------------------------------------------
                    'If GRPS(1, R) = "Albania" And e% = 8 Then Stop
                    ResorTot(R%, e%) = resorm(R%, e%) * resor(R%, e%) +
                    resorunconm(R%, e%) * resoruncon(R%, e%) '* enresunce(R%) 'was used earlier for synoil
                    'may need to revitalize and use enresunce

                Next e%

                'mti 2025/02/06 start: Add also in 8.32 for Octopus Project
                For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable
                    'ENINVT(R%, e%) = ineed(R%, e%) 'unadjusted
                    'If GRPS(1, R) = "China" And e% = 6 Then Stop
                    ENINVT(R%, e%) = ineed(R%, e%) * sidsf(R%) 'used in client version  'adjusted
                Next e%
                'mti 2025/02/06 end
                '
                '     All Years 2:  Update Capital Cost for Fossil Fuels
                '     --------------------------------------------------
                Dim RegionName = context.GetDimension(1).Buckets.Item(CShort(R))
                Dim resor_rem = 0!



                For e% = 1 To NET% 'jrs 2013/04/19 Synth 'Oil, gas, coal, hydro, nuclear, renewable, synthetic
                    If e% = Nucl Then 'jrs 2013/04/22 Or e% = Synth
                        sqe(R%, e%) = AMAX(0.01, Amin(BQE(R%, e%) * qem(R%, e%), 1000))
                        'mti 2024/07/23  start
                        'ElseIf e% = Hydro OrElse e% = Renew Then
                    ElseIf e% = Hydro OrElse e% = Solar OrElse e% = Wind OrElse e% = Geothermal OrElse e% = Renew Then
                        'mti 2024/07/23  end
                        'If GRPS(1, R) = "United States of America" And IY > 5 Then Stop
                        SmoothENP(R%, e%) = CSng(0.8 * SmoothENP(R%, e%) + 0.2 * ENP(R%, e%))
                        Dim ENPGR = AMAX(0, SmoothENP(R%, e%) - SIENP(R%, e%))
                        'bbh 2013/06/09 start
                        'resor_rem = AMAX(resor(R%, e%) * resorm(R%, e%) - ENPGR, 0.001)
                        'If GRPS(1, R) = "Albania" And e% = 8 Then Stop
                        resor_rem = CSng(AMAX(ResorTot(R%, e%) - ENPGR, 0.00001)) 'bbh 2017/05/03
                        'resor_rem = AMAX(ResorTot(R%, e%) - ENPGR, 0.001)
                        'SQE(R%, e%) = BQE(R%, e%) * resor(R%, e%) * resorm(R%, e%) / AMAX(0.001, resor_rem)
                        'sqe(R%, e%) = BQE(R%, e%) * ResorTot(R%, e%) / AMAX(0.001, resor_rem)
                        'mti 2024/12/11 start
                        'sqe(R%, e%) = CSng(BQE(R%, e%) * ResorTot(R%, e%) / AMAX(0.00001, resor_rem)) 'bbh 2017/05/03
                        If ResorTot(R%, e%) <= 0 Then
                            sqe(R%, e%) = BQE(R%, e%)
                        Else
                            sqe(R%, e%) = CSng(BQE(R%, e%) * ResorTot(R%, e%) / AMAX(0.00001, resor_rem)) 'bbh 2017/05/03
                        End If
                        'mti 2024/12/11 end

                        'If RegName$(R%) = "Tonga" And e% = Hydro Then Stop

                        'mti 2024/08/20 start
                        Dim mulvre As Single = 1
                        'Dim qevrethreshold As Single = 0.5
                        'Dim qevremul As Single = 1    'default shoud be zero'
                        'mti 2025/10/21 start
                        'Dim vreshare As Single = (ENP(R%, Solar) + ENP(R%, Wind)) / AMAX(0.000001, (ENDEM(R%)))
                        Dim vreshare As Single = Amin(1, (ENP(R%, Solar) + ENP(R%, Wind)) / AMAX(0.000001, (ENDEM(R%))))
                        'mti 2025/10/21 start
                        If qevrethr(R%) < 1 AndAlso vreshare > qevrethr(R%) AndAlso (e% = Solar OrElse e% = Wind) Then
                            'If GRPS(1, R) = "India" Then Stop     'If GRPS(1, R) = "China" Then Stop
                            'mti 2024/08/23 end
                            'mulvre = CSng(Amin(1.5, 1 + (vreshare - qevrethr(R%))) * qevremul(R%))
                            mulvre = CSng(Amin(1.5, 1 + (vreshare - qevrethr(R%)) * qevremul(R%)))
                            'mti 2024/08/23 end
                            sqe(R%, e%) = CSng(sqe(R%, e%) * mulvre)
                        End If

                        'mti 2024/08/20 end


                    Else
                        '                fac = 1!
                        '                If (e% = Oil) Then fac = SENPR(R%)
                        '                CUMPR(R%, e%) = CUMPR(R%, e%) + ENP(R%, e%) * fac
                        'If GRPS(1, R) = "Albania" And e% = 8 Then Stop
                        CUMPR(R%, e%) = CUMPR(R%, e%) + ENP(R%, e%)
                        Dim MaxFac = 0!
                        If e% = Oil Then
                            'ResourceBase = resor(R%, Oil) * resorm(R%, Oil) + _
                            'resor(R%, 5) * resorm(R%, 5) * enresunce(R%)
                            Dim ResourceBase = ResorTot(R%, Oil)
                            MaxFac = CSng(Amin(0.001, 0.1 * ResourceBase))
                            resor_rem = AMAX(ResourceBase - CUMPR(R%, e%), MaxFac)
                        Else
                            'MaxFac = Amin(0.001, 0.1 * resor(R%, e%) * resorm(R%, e%))
                            'resor_rem = AMAX(resor(R%, e%) * resorm(R%, e%) - CUMPR(R%, e%), MaxFac)
                            MaxFac = CSng(Amin(0.001, 0.1 * ResorTot(R%, e%)))
                            resor_rem = AMAX(ResorTot(R%, e%) - CUMPR(R%, e%), MaxFac)
                        End If
                        'jrs 2009/11/12
                        'If resor(R%, e%) = 0 And resor_rem = 0 Then
                        Dim CompQE = 0!
                        If ResorTot(R%, e%) = 0 AndAlso resor_rem = 0 Then
                            CompQE = BQE(R%, e%) * resorm(R%, e%)
                        Else
                            'CompQE = BQE(R%, e%) * (resor(R%, e%) * resorm(R%, e%) / AMAX(resor_rem, 0.0001)) ^ 0.4 '0.2
                            CompQE = CSng(BQE(R%, e%) * (ResorTot(R%, e%) / AMAX(resor_rem, 0.0001)) ^ 0.4) '0.2

                        End If
                        'bbh 2013/06/09 start
                        'If GRPS(1, R) = "Cameroon" And e = Coal Then Stop
                        sqe(R%, e%) = CSng(sqe(R%, e%) * 0.8 + CompQE * 0.2)
                        'ttt Comment Out at Stop (VB6: Uncommented)
                        'If RegionName = "Turkmenistan" AndAlso e% = Hydro Then Stop
                    End If
                Next e%



                'jrs 2013/04/19
                For e% = 1 To NET%
                    SQEUC(R%, e%) = AMAX(0.01, Amin(BQEUC(R%, e%) * qeunconm(R%, e%), 1000))
                Next e%
                '
                '     All Years 2:  Prepare Conventional/Unconventional Split for Next Cycle
                '     ----------------------------------------------------------------------
                '         CUMPR(R%, Synth) = CUMPR(R%, Synth) + ENP(R%, Oil) * (1 - SENPR(R%))
                '         ENPU = 0
                '         ENPC = ENP(R%, Oil)
                '         SynConRat = (SQE(R%, Synth) / AMAX(SQE(R%, Oil), 0.001)) / AMAX(0.001, IQESynConRat(R%))
                '         ENPU = ENP(R%, Oil) * AMAX(1 - SynConRat, 0.001)
                '         RemResource = resor(R%, 5) * resorm(R%, 5) - CUMPR(R%, Synth)
                '          'RESOR(position 5) holds synthetic to keep sizes of storage down
                '         ENPU = Amin(ENPU, RemResource / AMAX(5, prodtf(R%, Oil)))
                '         ENPC = ENP(R%, Oil) - ENPU
                '         SENPR(R%) = ENPC / AMAX(ENP(R%, Oil), 0.001)
                '
                '     All Years 2:  Discoveries
                '     -------------------------
                'jrs 2013/04/22 NET%
                Dim ResorBase = 0!
                For e% = 1 To Hydro  'Oil, gas, coal, hydro
                    If e% = Hydro Then
                        sreser(R%, e%) = RESER(R%, e%)
                        Exit For
                        'jrs 2013/04/22 e% = e% + 1
                    End If
                    If e% = Oil Then
                        'jrs 2013/04/22 ResorBase = resor(R%, Oil) * resorm(R%, Oil) + _
                        'resor(R%, 5) * resorm(R%, 5) * enresunce(R%)
                        'bbh 2013/06/09 start
                        'ResorBase = resor(R%, Oil) * resorm(R%, Oil) + _
                        'resoruncon(R%, Oil) * resorunconm(R%, Oil) * enresunce(R%)
                        ResorBase = ResorTot(R%, Oil)
                        'jrs 2013/04/22 Sync moved from 5 to the new unconventional array
                        resor_rem = (ResorBase - CUMPR(R%, e%) - RESER(R%, e%))
                    Else
                        'resor_rem = (resor(R%, e%) * resorm(R%, e%) - CUMPR(R%, e%) - RESER(R%, e%))
                        resor_rem = (ResorTot(R%, e%) - CUMPR(R%, e%) - RESER(R%, e%))

                    End If
                    Dim reterm = 0.5 * resor_rem / AMAX(wunro(e%), 0.001) +
                        0.5 * ENP(R%, e%) / AMAX(WENP(e%), 0.001) 'discovery share depends
                    'both on resources remaining and intensity of current exploitation
                    If reterm < 0 Then reterm = 0
                    Dim zy = IY% - 1
                    Dim rdi_aug = (rdi(e%) + rdinr(e%) * zy)
                    Dim wep_term = (1.0! + (WEP - iwep) / AMAX(iwep, 0.001) * elasdi)
                    Dim rd = rdi_aug * wep_term * reterm * rdm(R%, e%)
                    If e% = Oil Then
                        'jrs 2013/04/22 ResorBase = resor(R%, Oil) * resorm(R%, Oil) + _
                        'resor(R%, 5) * resorm(R%, 5) * enresunce(R%)
                        ResorBase = resor(R%, Oil) * resorm(R%, Oil) +
                    resoruncon(R%, Oil) * resorunconm(R%, Oil) * enresunce(R%)
                        resor_rem = (ResorBase - CUMPR(R%, e%) - RESER(R%, e%))

                        'bbh 2013/05/24 note:  enresunce is set in IFs.mdb/CountryParameters.  It is given a non-zero value only for
                        '    Canada (0.3).
                        'When deal with issues here (constraint of rd), also look at where enresunce is used on capital costs.
                    Else
                        'resor_rem = (resor(R%, e%) * resorm(R%, e%) - CUMPR(R%, e%) - RESER(R%, e%))
                        resor_rem = (ResorTot(R%, e%) - CUMPR(R%, e%) - RESER(R%, e%))
                    End If
                    rd = AMAX(0, Amin(rd, 0.04 * resor_rem))
                    'This calibrated with US oil/gas transition in historic period
                    Dim prterm = ENP(R%, e%)
                    '            If (e% = Oil) Then prterm = ENPC
                    'If r% = 1 Then Stop
                    'jrs 2013/04/22 not ready for discoveris in Nuclear and other renewables
                    If e% <= Hydro Then
                        sreser(R%, e%) = CSng(AMAX(0.001, RESER(R%, e%) - prterm + rd))
                    End If
                    'bbh 2013/05/24 note:  The lines below are the offending lines:  need to add unonventional resources or
                    '   else Canadian conventional resources constrain reserves; are we going to let all unconventional
                    '   become reserves or limit it somehow?
                    If IY% = 2 AndAlso e% < Hydro Then 'In case resorm brings down Resor below Reser
                        'sreser(R%, e%) = AMAX(0.0001, Amin(sreser(R%, e%), resorm(R%, e%) * resor(R%, e%)))
                        sreser(R%, e%) = AMAX(0.0001, Amin(sreser(R%, e%), ResorTot(R%, e%)))
                    End If
                    'bbh 2013/06/09 start

                    'If RegName$(R%) = "United States of America" And e% = 1 Then Stop
                Next e%
                '
                '     All Years 2:  Indicators of Energy Intensity and Global Production
                '     ------------------------------------------------------------------
                ENRGDP(R%) = CSng(ENDEM(R%) / AMAX(GDP(R%), 0.0001) * 1000)
                'ENRGDP(R%) = ENDEM(R%) / AMAX(GDPP(R%), 0.0001) * 1000
                'Do not need relative to GDPP because have in display options
            Next R%
            Dim TWENP = 0!
            For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable
                WENP(e%) = 0!
                For R% = 1 To NR%
                    WENP(e%) = WENP(e%) + ENP(R%, e%)
                    TWENP = TWENP + ENP(R%, e%)
                Next R%
                SWENP(e%) = CSng(0.8 * SWENP(e%) + 0.2 * WENP(e%))
            Next e%
            OILGPR = CSng((WENP(Oil) + WENP(Gas)) / AMAX(TWENP, 0.001))
            '
            '     All Years 2:  Indicators of Global Energy Resources
            '     ---------------------------------------------------
            For e% = 1 To NER%  'Oil, gas, coal, hydro
                WRESER(e%) = 0!
                WRESOR(e%) = 0!
                For R% = 1 To NR%
                    WRESER(e%) = WRESER(e%) + RESER(R%, e%)
                    'bbh 2013/06/09 May want to revisit and have WRESORUNCON
                    'WRESOR(e%) = WRESOR(e%) + resor(R%, e%) * resorm(R%, e%)
                    WRESOR(e%) = WRESOR(e%) + ResorTot(R%, e%)
                Next R%
            Next e%
            '
            '     All Years 2:  Indicators of Carbon Dioxide Build-up - Use Production Data
            '     -------------------------------------------------------------------------
            '       Note:  IFs carbon taxes are per unit carbon, not CO2.  To scale, need to
            '           multiply by 3.67 (molecular weight of CO2 versus carbon).
            '       A tax of $20 per ton of CO2 is about the same as $73.4 per ton on carbon

            For R% = 1 To NR%
                Dim SumCarbTax = (ENP(R%, 1) * carfuel1 + ENP(R%, 2) * carfuel2 +
            ENP(R%, 3) * carfuel3) * carbtax(R%)
                Dim SumENPCarb = 0!
                For e% = 1 To Coal
                    SumENPCarb = SumENPCarb + ENP(R%, e%)
                Next e%
                cartaxenpriadd(R%) = SumCarbTax / AMAX(0.0001, SumENPCarb)
                '
                '       All Years 2:  Environmental Stress - switch to demand side calculation
                '       Because fuel consumption pattern is estimated from production and global
                '       balances are not computed, accuracy is somewhat reduced
                '       ------------------------------------------------------------------------

                '        SumNoCarb = 0
                '        Sumcarb = 0
                '        For E% = Hydro To Renew
                '            SumNoCarb = SumNoCarb + ENP(R%, E%)
                '        Next E%
                '        For E% = Oil To Coal
                '            Sumcarb = Sumcarb + ENP(R%, E%)
                '        Next E%
                '        EnDemRem = AMAX(0, ENDEM(R%) - SumNoCarb)
                '        CARANN(R%) = EnDemRem * ENP(R%, Oil) / Sumcarb * carfuel1 + _
                '            EnDemRem * ENP(R%, Gas) / Sumcarb * carfuel2 + _
                '            EnDemRem * ENP(R%, Coal) / Sumcarb * carfuel3
                '        TES(R%) = CARANN(R%) * 1000 / GDP(R%) 'kilograms of carbon/ unit of GDP

            Next R%
        End Sub
        Private Sub SubAllYears()
            For R% = 1 To NR%
                '     All Years:  Translation for Economic Model
                '     ------------------------------------------
                ST(R%, Ener) = ENST(R%) * iwepbyear  'ENPRII(R%)
                Dim renp = 0!
                For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable
                    renp = renp + ENP(R%, e%)
                Next e%

                'If RegName$(R%) = "Unitd Kingdm" Then Stop
                If (IY%) = 1 Then
                    sidsf(R%) = CSng(IDS(R%, Ener) / AMAX(tineed(R%), 0.001))

                    '

                    'Debug.Print(GRPS(1, R), sidsf(R%))

                    SIDSFI(R%) = sidsf(R%)
                    SPRIF(R%) = PRI(R%, Ener) / ENPRI(R%)
                    'jrs 2022/04/07 adding condition for small countries with almost no production of energy, to allow scenarios to include some
                    If renp < 0.0001 Then
                        zsri(R%) = 1
                    Else
                        zsri(R%) = ZS(R%, Ener) / (renp * iwepbyear)
                    End If
                    'if RegName$(R%) = "Ukraine" Then Stop
                Else
                    Dim TimeFAc = Amin(IY%, enconv)
                    '            Shifts initial condition (e.g. China) towards solid Energy/Econ link
                    '            over time, smoothing out initial condition differences

                    ''mti 2025/01/15 start
                    sidsf(R%) = (SIDSFI(R%) * (enconv - TimeFAc) + 1 * TimeFAc) / enconv
                    'sidsf(R%) = sidsf(R%)
                    ''mti 2025/01/15 end

                    IDS(R%, Ener) = sidsf(R%) * tineed(R%)
                    'If GRPS(1, R%) = "Taiwan" Then Stop

                    '   BBH 6/17/04 V5_0 Begin  When moved to 100 base, must keep real price translation
                    'jrs 2025/04/01 trying with 200 again, Taiwan:
                    Dim Mul = ConvergeOverTime(zsri(R%), 1, 30)
                    'Dim Mul = ConvergeOverTime(zsri(R%), 1, 200)
                    'Dim Mul = ConvergeOverTime(zsri(R%), 1, 50) 'bbh 2024/07/11 Tried slowing down convergence to reduce transients; not needed
                    ZS(R%, Ener) = renp * iwepbyear * Mul  'ENPRII(R%) 'Direct translations of volumes to currency
                    'If RegName$(R%) = "Unitd Kingdm" Then Stop
                    XS(R%, Ener) = ENX(R%) * iwepbyear ' ENPRII(R%) 'for all but investment which is in
                    MS(R%, Ener) = ENM(R%) * iwepbyear 'ENPRII(R%) 'currency already in energy model
                    CS(R%, Ener) = CSng(SCSF(R%) * (ENDEM(R%) * 0.6))
                    'If IY% > 36 And RegName$(R%) = "Tajikistan" Then Stop

                    PRI(R%, Ener) = SPRIF(R%) * ENPRI(R%)
                    '   BBH 6/17/04 V5_0 End
                End If

                'If GRPS(1, R) = "United States of America" And IY > 2 Then Stop

                ''mti 2025/01/2025 start
                'Dim TotEnInvT As Single = 0
                'For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable
                '    TotEnInvT = TotEnInvT + ineed(R%, e%)
                'Next e%

                'If TotEnInvT = 0 Then
                '    For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable
                '        ENINVT(R%, e%) = 0
                '    Next e%
                'Else
                '    For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable
                '        ENINVT(R%, e%) = ineed(R%, e%) * IDS(R%, Ener) / TotEnInvT
                '    Next e%
                'End If

                ''For e% = 1 To Renew  'Oil, gas, coal, hydro, nuclear, renewable
                ''    ENINVT(R%, e%) = ineed(R%, e%)
                ''Next e%

                ''mti 2025/01/2025 end

            Next R%
            'jrs 2009/05/14
            'All Years: Adjusting Energy Demand using ICT broadband
            '-------------------------------------------------------
            adjENDEM_ICT()
        End Sub
        'jrs 2009/05/13
        Private Sub adjENDEM_ICT()
            'jrs 2009/06/05
            '    'Adjusting Energy Demand with ICT drivers
            '    Dim ICTBroadExp As Single
            '    Dim rVal As Single, incCS As Single, incTec As Single
            '    Dim ICTSpendExp As Single
            '    'jrs 2009/05/26
            '    Dim EffectAdd As Single
            '    Dim GOVREGQUALExp As Single
            '
            '    If IY = 1 Then
            '        ReDim SICTBroadExp(NR, 2) As Single
            '        'jrs 2009/05/25 ReDim FirstYearAdj(NR) As Single
            '        ReDim ICTBroadExpShift(NR) As Single
            '        ReDim ICTSpendExpShift(NR) As Single
            '        ReDim GovRegQualShift(NR) As Single
            '    End If
            '
            '    Dim R As Integer
            '    For R = 1 To NR
            '
            '        'Expected ICT Spending
            '        '---------------------
            '        TbName$ = "GDP/Capita (PPP 2000) Versus ICT Expenditures % GDP (2005) Linear"
            '        Call XYTABL(TbName$, GDPPCP(R), rVal)
            '
            '        If IY = 1 Then
            '            If ictspendm(R) > 0 Then
            '                ICTSpendExpShift(R%) = ICTSPEND(R) / ictspendm(R) - rVal
            '                ICTSpendExp = ICTSPEND(R) / ictspendm(R)
            '            Else
            '                ICTSpendExpShift(R%) = -1 * rVal
            '                ICTSpendExp = ICTSPEND(R)
            '            End If
            '        Else
            '            ICTSpendExp = rVal + ICTSpendExpShift(R)
            '            ICTSpendExp = AMAX(Amin(ICTSpendExp, 100), 0)
            '        End If
            '
            '        'Expected Government Reg. Quality
            '        '--------------------------------
            '        Call AnalFunc("Governance Regulatory Quality (2006)", _
            '            Array(GDPPCP(R%), EDYRSAG15(R%, 3)), rVal)
            '        rVal = AMAX(0, Amin(5, rVal + 2.5))
            '        If IY = 1 Then
            '            If govregqualm(R) > 0 Then
            '                GovRegQualShift(R%) = GOVREGQUAL(R%) / govregqualm(R) - rVal
            '                GOVREGQUALExp = GOVREGQUAL(R) / govregqualm(R)
            '            Else
            '                GovRegQualShift(R%) = -1 * rVal
            '                GOVREGQUALExp = GOVREGQUAL(R)
            '            End If
            '        Else
            '            EffectAdd = ConvergeOverTime(GovRegQualShift(R%), 0, goveffconv) 'Base, Target, Years
            '            GOVREGQUALExp = AMAX(0, Amin(5, (rVal + EffectAdd)))
            '        End If
            '
            '
            '        'jrs 2009/05/21 changing to multivariate
            ''        TbName$ = "GDP/Capita (PPP 2000) Versus ICT Broadband Subs per 100 ITU (" & CStr(BaseYear) & ") Linear"
            ''        TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear)
            ''        Call XYTABL(TbName$, GDPPCP(R), rVal)
            '        Call AnalFunc("ICT Broadband Access % of Population", _
            '        Array(GDPPCP(R), GOVREGQUALExp, ICTSpendExp), rVal)
            '
            '        ICTBroadExp = rVal
            '        If IY > 1 Then
            '            rVal = rVal + ConvergeOverTime(ICTBroadExpShift(R), 0, 100)
            '            incCS = rVal - SICTBroadExp(R, 1)
            '            If SICTBroadExp(R, 2) < ictbroadtecinflection Then
            '                incTec = (ictbroadtechighrt - ictbroadteclowrt) * SICTBroadExp(R, 2) / ictbroadtecinflection + ictbroadteclowrt
            '            Else
            '                incTec = (ictbroadteclowrt - ictbroadtechighrt) * (SICTBroadExp(R, 2) - ictbroadtecinflection) / (100 - ictbroadtecinflection) + ictbroadtechighrt
            '            End If
            '
            '            'Add Increase/Decrease to final value, this is the tec factor
            '            'Total increment can't be more than 25% of the remaining gap to reach target of 100
            '            ICTBroadExp = SICTBroadExp(R, 2) + Amin(incTec + incCS, (100 - SICTBroadExp(R, 2)) / 4)
            '
            '            'jrs 2009/05/25 ENDEM(R) = ENDEM(R) * (1 + (ICTBROAD(R) - ICTBroadExp) * elendemict + FirstYearAdj(R))
            '            ENDEM(R) = ENDEM(R) * (1 + (ICTBROAD(R) - ICTBroadExp) * elendemict)
            '        Else
            '            'jrs 2009/05/25 FirstYearAdj(R) = (ICTBroadExp - ICTBROAD(R)) * elendemict
            '            ICTBroadExpShift(R) = ICTBROAD(R) - ICTBroadExp
            '            rVal = ICTBROAD(R)
            '            ICTBroadExp = rVal
            '        End If
            '
            '        SICTBroadExp(R, 1) = rVal
            '        SICTBroadExp(R, 2) = ICTBroadExp
            '
            '    Next R

            Dim R As Integer

            For R = 1 To NR
                'bbh 2009/09/17 bound broadband energy demand change
                Dim BroadbandTerm = AMAX(-0.5, Amin(0.2, (ICTBROAD(R) - ICTBroadExp(R)) * elendemict))

                '                'mti Sep 12, 2011 - start
                '                'take it off after testing
                BroadbandTerm = 0
                '                'mti Sep 12, 2011 - end

                ENDEM(R) = CSng(ENDEM(R) * (1 + BroadbandTerm))
            Next R
        End Sub

        'jrs 2021/01/08
        'Adjust Energy Exports based on swing in Imports from main partners
        Private Sub UpdateXSPhysicalwithDyad(ByVal wxc As Single, ByRef xc As Single())
            Dim Actor, Partner As Integer
            Dim wxcAdj As Single
            wxcAdj = 0

            Dim S As Integer
            S = 2

            If bilateralsw > 0 Then
                If xcadjsw > 0 Then

                    For Actor = 1 To NR
                        xabsmod(Actor, S) = 0
                        For Partner = 1 To NR
                            'If GRPS(1, Actor) = "Turkmenistan" And GRPS(1, Partner) = "China" And S = 2 And IY = 2 Then Stop
                            'jrs 2020/06/25 SXS is in different units than SXDYAD needed * 1000000000
                            'dkb 2020/07/10 xabsmod(Actor, S) = xabsmod(Actor, S) + SXDYAD(Actor, Partner, S%) / AMAX(SXS(Actor, S%) * 1000000000, 0.000000001) * mbasediff(Partner, S) 'jrs 2020/06/23 added AMAX to avoid division by 0
                            'jrs 2020/07/15 xabsmod(Actor, S) = xabsmod(Actor, S) + SXDYAD(Actor, Partner, S%) / AMAX(SMS(Partner, S%) * 1000000000, 0.000000001) * mbasediff(Partner, S)
                            'jrs 2025/02/21
                            'mbasediff has the year to year difference in Imports of the Partener
                            'SXDYAD_Norm / SMS is the portion of Imports of the Partener that should impact the Actor
                            xabsmod(Actor, S) = CSng(xabsmod(Actor, S) + SXDYAD_Norm(Actor, Partner, S%) / AMAX(SMS(Partner, S%) * 1000000000, 0.000000001) * mbasediff(Partner, S))
                            'If GRPS(1, Actor) = "Turkmenistan" And S = 2 And IY = 2 And SXDYAD_Norm(Actor, Partner, S%) > 0 Then Debug.Print Partner & ", " & xabsmod(Actor, S)
                        Next Partner
                        'If IY = 7 And GRPS(1, Actor) = "Brazil" And S = 4 Then Stop
                        xabsmod(Actor, S) = xabsmod(Actor, S) * xcadjsw
                        xc(Actor) = AMAX(xc(Actor) + xabsmod(Actor, S), 0) 'jrs 2020/06/25 avoid going negative AMAX
                        wxcAdj = wxcAdj + xc(Actor)
                    Next Actor

                    For Actor = 1 To NR
                        If wxcAdj > 0 Then
                            xc(Actor) = xc(Actor) * wxc / wxcAdj
                        End If
                    Next Actor

                    'jrs 2021/01/08
                    '            If wxcAdj > 0 Then
                    '                wxc = wxcAdj
                    '            End If
                End If

            End If
        End Sub

        'jrs 2024/11/13
        'Adjust Energy Imports based on swing in Exports from main partners
        Private Sub UpdateMSPhysicalwithDyad(ByVal wmc As Single, ByRef mc As Single())
            Dim Actor, Partner As Integer
            Dim wmcAdj As Single
            wmcAdj = 0

            Dim S As Integer
            S = 2

            If bilateralsw > 0 Then
                If mcadjsw > 0 Then

                    For Actor = 1 To NR
                        mabsmod(Actor, S) = 0
                        For Partner = 1 To NR
                            'jrs 2020/06/25 SXS is in different units than SXDYAD needed * 1000000000
                            'jrs 2025/02/21
                            'xbasediff has the year to year difference in Exports of the Partener
                            'SXDYAD_Norm / SXS is the portion of Exports of the Partener that should impact the Actor
                            mabsmod(Actor, S) = CSng(mabsmod(Actor, S) + SXDYAD_Norm(Partner, Actor, S%) / AMAX(SXS(Partner, S%) * 1000000000, 0.000000001) * xbasediff(Partner, S))
                        Next Partner
                        'If IY = 7 And GRPS(1, Actor) = "Brazil" And S = 4 Then Stop
                        mabsmod(Actor, S) = mabsmod(Actor, S) * mcadjsw
                        mc(Actor) = AMAX(mc(Actor) + mabsmod(Actor, S), 0) 'jrs 2020/06/25 avoid going negative AMAX
                        wmcAdj = wmcAdj + mc(Actor)
                    Next Actor

                    For Actor = 1 To NR
                        If wmcAdj > 0 Then
                            mc(Actor) = mc(Actor) * wmc / wmcAdj
                        End If
                    Next Actor

                End If

            End If
        End Sub

        'jrs 2021/01/08
        Private Sub Savemdbasediff(ByVal mbase As Single, R As Integer)
            If bilateralsw > 0 Then
                If xcadjsw > 0 Then
                    If IY = 2 Then
                        mbasediff(R%, 2) = mbase - SENM(R%)
                    Else
                        mbasediff(R%, 2) = mbase - smbase(R%, 2)
                    End If
                    smbase(R%, 2) = mbase
                End If
            End If
        End Sub

        'jrs 2025/04/29
        Private Sub UpdateEnergyProdWithChangeInImports()
            Dim R As Integer
            Dim ReductionVal As Single, ReductionBBOE As Single
            Dim ENPRenew As Single, XDYADENETotPartner As Single
            'Check Import Reductions from intervention
            For R = 1 To NR
                'Monadic Intervention on value
                If msm(R, Ener) < 1 Then
                    If msm(R, Ener) > 0 Then
                        ReductionVal = MS(R, Ener) * (1 - msm(R, Ener)) / msm(R, Ener)
                    Else
                        ReductionVal = MS(R, Ener) 'all of it
                    End If
                    ReductionBBOE = ReductionVal / WEPBYEAR
                    'Find Total Production
                    ENPRenew = 0
                    For ee = 4 To Renew
                        ENPRenew = ENPRenew + ENP(R, ee)
                    Next

                    'Compensate Reduction in Imports with Increase of Renewable production
                    If ENPRenew > 0 And XDYADENETotPartner > 0 Then
                        For ee = 4 To Renew
                            ENP(R, ee) = ENP(R, ee) + ReductionBBOE * ENP(R, ee) / ENPRenew
                        Next
                        'Compensate Increase of Renew with Decrease of Fossil Production of Partners
                        For pp = 1 To NR
                            If XDYAD(pp, R, Ener) > 0 Then
                                For ee = 1 To 3
                                    ENP(pp, ee) = ENP(pp, ee) - ReductionBBOE * ENP(pp, ee) / XDYADENETotPartner
                                    SmoothENP(R, ee) = ENP(R, ee)
                                Next
                            End If
                        Next
                    End If
                End If
                'Monadic Intervention on product
                If enmm(R) < 1 Then
                    If enmm(R) > 0 Then
                        ReductionBBOE = ENM(R) * (1 - enmm(R)) / enmm(R)
                    Else
                        ReductionBBOE = ENM(R) 'all of it
                    End If
                    'Find Total Production in R
                    ENPRenew = 0
                    For ee = 4 To Renew
                        ENPRenew = ENPRenew + ENP(R, ee)
                    Next
                    'Find Total Production in all Partners
                    XDYADENETotPartner = 0
                    For pp = 1 To NR
                        If XDYAD(pp, R, Ener) > 0 Then
                            For ee = 1 To 3
                                XDYADENETotPartner = XDYADENETotPartner + ENP(pp, ee)
                            Next
                        End If
                    Next
                    'Compensate Reduction in Imports with Increase of Renewable production in R Country
                    If ENPRenew > 0 And XDYADENETotPartner > 0 Then
                        For ee = 4 To Renew
                            ENP(R, ee) = ENP(R, ee) + ReductionBBOE * ENP(R, ee) / ENPRenew
                        Next
                        'Compensate Increase of Renew with Decrease of Fossil Production of Partners
                        For pp = 1 To NR
                            If XDYAD(pp, R, Ener) > 0 Then
                                For ee = 1 To 3
                                    ENP(pp, ee) = ENP(pp, ee) - ReductionBBOE * ENP(pp, ee) / XDYADENETotPartner
                                    SmoothENP(R, ee) = ENP(R, ee)
                                Next
                            End If
                        Next
                    End If
                End If
                'Bilateral Intervention on value, maybe not necessary since this only re-arranges counry distribution
                'If tradeexportdyadm(R, Partner, Ener) < 1 Then
                'End If

            Next
        End Sub

    End Module

End Namespace
