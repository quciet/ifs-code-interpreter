        Public Sub ifsKnowledge1AllYears(context As IForecastContext)
            sErrorSub = "IFsKnowledge1" ' & CStr(IFUN)
            '
            '   FIRST TIME STEP, FIRST CALL ONLY
            '   -----------------------------------------------------------------------

            'ReDim


            '   All Years 1:  Knowledge Indices
            '   -------------------------------------

            'Education and Human Skills

            '---Minimum for adults with secondary education -----------

            Dim AdultSecPerFixMax = 100.0!
            Dim AdultSecPerFixMin = 0!

            Dim AdultSecPerFloatMin = EDSECPER(1, Total) '+ EDTERPER(1, Total)
            Dim AdultSecPerFloatMax = EDSECPER(1, Total) '+ EDTERPER(1, Total)

            'Recalc (floating max and min)each year---
            For R% = 2 To NR%
                AdultSecPerFloatMin = Amin(EDSECPER(R%, Total), AdultSecPerFloatMin)
                AdultSecPerFloatMax = AMAX(EDSECPER(R%, Total), AdultSecPerFloatMax)
            Next R%


            For R% = 1 To NR%

                '0 to 1 - logarithmic
                'LitIndex = AMAX(0, Amin(1, Log(LIT(R%)) / 2 * Log(10))) 'Log(100)/2 =1  '0 to 1

                Dim LitIndex = AMAX(0, Amin(1, LIT(R%) / 100)) 'Log(100)/2 =1  '0 to 1

                '0 to 1 - Linear
                Dim AdultSecPerIndex = AMAX(0, Amin(1, (EDSECPER(R%, Total) - AdultSecPerFixMin) /
             (AdultSecPerFixMax - AdultSecPerFixMin)))

                'mti 2010/04/12 ICTTelephoneIndex = AMAX(0, Amin(1, Log(INFRATELE(R%)) / 3 / Log(10)))  'Log in VB is natural log; Log(x)/Log(n) goves log of x with base n
                Dim ICTTelephoneIndex = AMAX(0, Amin(1, Math.Log(AMAX(0.00000001, INFRATELE(R%))) / 3 / Math.Log(10))) 'Log in VB is natural log; Log(x)/Log(n) goves log of x with base n
                Dim ICTMobileIndex = AMAX(0, Amin(1, ICTMOBIL(R%) / 100))
                Dim ICTBroadIndex = AMAX(0, Amin(1, ICTBROAD(R%) / 100))
                Dim ICTComputersIndex = AMAX(0, Amin(1, ICTCOMPUTERS(R%) / 100))

                Dim RandDExpIndex = AMAX(0, Amin(1, (Amin(4, RANDDEXP(R%)) - 0) / (4 - 0))) 'max at 4%

                'EdterGRateIndex = AMAX(0, Amin(1, EDTERGRATE(R%, Total) / 100))

                Dim EdTerGRSciEnRate = AMAX(1, Amin(70, EDTERGRATE(R%, Total) * EDTERGRSCIEN(R%) / 100))
                Dim EdterGRateIndex = AMAX(0, Amin(1, EdTerGRSciEnRate / 25)) 'maxed on 25% (of 100% total grad)
                'If IY% = 1 Then Debug.Print RegName(R%); EdterGRateIndex

                Dim EonFreeIndex = AMAX(0, Amin(1, ECONFREE(R%) / 10))
                Dim FreeDomIndex = AMAX(0, Amin(1, FREEDOM(R%) / 14))
                Dim GovRegQualIndex = AMAX(0, Amin(1, GOVREGQUAL(R%) / 5))


                'mti Sept 2010 Revise Knowledge Model

                Dim TradeCompFixMax = Math.Log(1000)
                Dim InvCompFixMax = Math.Log(500)
                'TradeCompFixMin = 0
                '  If RegName(R%) = "Luxembourg" Then Stop
                '      For R% = 1 To NR%
                Dim TradeComp = Math.Log(AMAX(1, 100 * (XRPA(R%) + MRPA(R%)) / GDPPOT(R%))) '/ TradeBenchI
                Dim TradeIndex = Amin(1, TradeComp / TradeCompFixMax)
                Dim InvIndex = 0!
                If context.HistoricMode Then
                    InvIndex = 0  'No FDI data historically
                Else
                    Dim InvComp = Math.Log(AMAX(1, 100 * (XFDISTOCK(R%) + XFDISTOUT(R%)) / GDPPOT(R%))) '/ FDIBenchI
                    InvIndex = CSng(Amin(1, InvComp / InvCompFixMax))
                End If

                'greater emphasis on FDI
                KNEXTINDEX(R%) = CSng((TradeIndex + 2 * InvIndex) / 3) '* 100

                '       Next R%
                'EconIntegIndex = ECONINTEG(R%) / 100

                KNHCINDEX(R%) = (LitIndex + AdultSecPerIndex) / 2
                KNICTINDEX(R%) = CSng((ICTTelephoneIndex + ICTMobileIndex + ICTBroadIndex + ICTComputersIndex) / 4)
                KNINNOVINDEX(R%) = (RandDExpIndex + EdterGRateIndex) / 2
                KNENVINDEX(R%) = (EonFreeIndex + FreeDomIndex + GovRegQualIndex) / 3

                'KNEXTINDEX(R%) = EconIntegIndex '/ 100

                KNTOTALINDEX(R%) = (KNHCINDEX(R%) + KNICTINDEX(R%) + KNINNOVINDEX(R%) + KNENVINDEX(R%)) / 4

                KNTOTALINDEX(R%) = CSng(KNTOTALINDEX(R%) * 0.9 + KNEXTINDEX(R%) / 10)
                'mti Sept 2010 Revise Knowledge Model
                ' + KNEXTINDEX(R%)) / 5

            Next R%

            'clp 2026/02/25 ***START*** Add V-Dem indicator forecasts for Essex collaboration
            Dim estval As Single
            If IY = 1 Then

                ReDim womenjurshift(NR%), womenlegshift(NR%), womenproprightshift(NR%), theta1shift(NR%), theta2shift(NR%), theta3shift(NR%)
                For R% = 1 To NR%
                    ' Female Legislature Share
                    estval = CSng(5.55 + GDPPCP(R%) * 0.00009066 + GINIDOM(R%) * -13.19 + FEMSHRLAB(R%) * 0.3344 + DEMOCPOLYARCHY(R%) * 1.997 + MARRIEDFEMPOP(R%, Age15_19) * -0.06457)
                    womenlegshift(R%) = WOMENINLEGISLATURE(R%) - estval

                    ' Female Journalism Share
                    estval = CSng(27.23 + TFR(R%) * -2.61433 + FEMSHRLAB(R%) * 0.20343 + MARRIEDFEMPOP(R%, Age15_19) * -0.03387 + EDYRSAG25(R%, 3) * 1.02831)
                    womenjurshift(R%) = WOMENINJOURNALISM(R%) - estval

                    ' Women's Property Rights
                    estval = CSng(0.3503 + TFR(R%) * -0.08805 + DEMOCPOLYARCHY(R%) * 2.781 + MATMORTRATIO(R%) * -0.0005325 + GINIDOM(R%) * -0.1833)
                    womenproprightshift(R%) = WOMENPROPRIGHTS(R%) - estval

                    'Thetas
                    'estval = CSng(0.1336 + WOMENINLEGISLATURE(R%) * 0.0013 + WOMENINJOURNALISM(R%) * 0.0035 + WOMENPROPRIGHTS(R%) * 0.0021 + GDPPCP(R%) * 0.00000411 + YTHBULGE(R%) * -0.0071)
                    Call AnalFunc("Theta 1",
New Single() {WOMENPROPRIGHTS(R%), WOMENINJOURNALISM(R%), WOMENINLEGISLATURE(R%), EDPRIENRN(R%, Total), EDPRISUR(R%, Total), SANITATION(R%, 3)}, estval,
New String() {"WOMENPROPRIGHTS", "WOMENINJOURNALISM", "WOMENINLEGISLATURE", "EDPRIENRN", "EDPRISUR", "SANITATION"}, context)
                    theta1shift(R%) = Theta1(R%) - estval

                    Call AnalFunc("Theta 2",
New Single() {WOMENPROPRIGHTS(R%), WOMENINJOURNALISM(R%), WOMENINLEGISLATURE(R%), CBR(R%), POVRATE685(R%, AG_AllAges, Total), MATMORTRATIO(R%)}, estval,
New String() {"WOMENPROPRIGHTS", "WOMENINJOURNALISM", "WOMENINLEGISLATURE", "CBR", "IncBelow6D85cPct", "MMR"}, context)
                    theta2shift(R%) = Theta2(R%) - estval

                    Call AnalFunc("Theta 3",
New Single() {WOMENPROPRIGHTS(R%), WOMENINJOURNALISM(R%), WOMENINLEGISLATURE(R%), YTHBULGE(R%), ECONFREE(R%), INFRAELECACC(R%, Urban)}, estval,
New String() {"WOMENPROPRIGHTS", "WOMENINJOURNALISM", "WOMENINLEGISLATURE", "PopulationYouthDep", "FreedomEcon", "EnElecAccessPctUrban"}, context)
                    theta3shift(R%) = Theta3(R%) - estval

                    Theta3High(R%) = Theta3(R%)
                    Theta3Low(R%) = Theta3(R%)

                    Theta2High(R%) = Theta2(R%)
                    Theta2Low(R%) = Theta2(R%)

                    Theta1High(R%) = Theta1(R%)
                    Theta1Low(R%) = Theta1(R%)
                Next
            Else
                For R% = 1 To NR%
                    ' Female Legislature Share
                    WOMENINLEGISLATURE(R%) = CSng(5.55 + GDPPCP(R%) * 0.00009066 + GINIDOM(R%) * -13.19 + FEMSHRLAB(R%) * 0.3344 + DEMOCPOLYARCHY(R%) * 1.997 + MARRIEDFEMPOP(R%, Age15_19) * -0.06457)
                    WOMENINLEGISLATURE(R%) = WOMENINLEGISLATURE(R%) + womenlegshift(R%)
                    WOMENINLEGISLATURE(R%) = WOMENINLEGISLATURE(R%) + womeninlegistlatureadd(R%) 'ems 2026/7/8 additive factor and multiplier parameters added for female legistlature share
                    WOMENINLEGISLATURE(R%) = WOMENINLEGISLATURE(R%) * womeninlegislaturem(R%)
                    WOMENINLEGISLATURE(R%) = CSng(AMAX(0, Amin(100, WOMENINLEGISLATURE(R%))))

                    ' Female Journalism Share
                    WOMENINJOURNALISM(R%) = CSng(27.23 + TFR(R%) * -2.61433 + FEMSHRLAB(R%) * 0.20343 + MARRIEDFEMPOP(R%, Age15_19) * -0.03387 + EDYRSAG25(R%, 3) * 1.02831)
                    WOMENINJOURNALISM(R%) = WOMENINJOURNALISM(R%) + womenjurshift(R%)
                    WOMENINJOURNALISM(R%) = WOMENINJOURNALISM(R%) + womeninjournalismadd(R%) 'ems 2026/7/8 additive factor and multiplier parameters added for female journalism share
                    WOMENINJOURNALISM(R%) = WOMENINJOURNALISM(R%) * womeninjournalismm(R%)
                    WOMENINJOURNALISM(R%) = CSng(AMAX(0, Amin(100, WOMENINJOURNALISM(R%))))

                    ' Women's Property Rights
                    WOMENPROPRIGHTS(R%) = CSng(0.3503 + TFR(R%) * -0.08805 + DEMOCPOLYARCHY(R%) * 2.781 + MATMORTRATIO(R%) * -0.0005325 + GINIDOM(R%) * -0.1833)
                    WOMENPROPRIGHTS(R%) = WOMENPROPRIGHTS(R%) + womenproprightshift(R%)
                    WOMENPROPRIGHTS(R%) = WOMENPROPRIGHTS(R%) + womenproprightsadd(R%) 'ems 2026/7/8 additive factor and multiplier parameters added for womens property rights
                    WOMENPROPRIGHTS(R%) = WOMENPROPRIGHTS(R%) * womenproprightsm(R%)
                    WOMENPROPRIGHTS(R%) = CSng(AMAX(-3.5, Amin(3.5, WOMENPROPRIGHTS(R%))))

                    Call AnalFunc("Theta 1",
New Single() {WOMENPROPRIGHTS(R%), WOMENINJOURNALISM(R%), WOMENINLEGISLATURE(R%), EDPRIENRN(R%, Total), EDPRISUR(R%, Total), SANITATION(R%, 3)}, estval,
New String() {"WOMENPROPRIGHTS", "WOMENINJOURNALISM", "WOMENINLEGISLATURE", "EDPRIENRN", "EDPRISUR", "SANITATION"}, context)

                    Theta1(R%) = CSng(estval) + theta1shift(R%)

                    Call AnalFunc("Theta 2",
New Single() {WOMENPROPRIGHTS(R%), WOMENINJOURNALISM(R%), WOMENINLEGISLATURE(R%), CBR(R%), POVRATE685(R%, AG_AllAges, Total), MATMORTRATIO(R%)}, estval,
New String() {"WOMENPROPRIGHTS", "WOMENINJOURNALISM", "WOMENINLEGISLATURE", "CBR", "IncBelow6D85cPct", "MMR"}, context)

                    Theta2(R%) = CSng(estval) + theta2shift(R%)

                    Call AnalFunc("Theta 3",
New Single() {WOMENPROPRIGHTS(R%), WOMENINJOURNALISM(R%), WOMENINLEGISLATURE(R%), YTHBULGE(R%), ECONFREE(R%), INFRAELECACC(R%, Urban)}, estval,
New String() {"WOMENPROPRIGHTS", "WOMENINJOURNALISM", "WOMENINLEGISLATURE", "PopulationYouthDep", "FreedomEcon", "EnElecAccessPctUrban"}, context)

                    Theta3(R%) = CSng(estval) + theta3shift(R%)

                    Call AnalFunc("Theta 1 High",
New Single() {WOMENPROPRIGHTS(R%), WOMENINJOURNALISM(R%), WOMENINLEGISLATURE(R%), EDPRIENRN(R%, Total), EDPRISUR(R%, Total), SANITATION(R%, 3)}, estval,
New String() {"WOMENPROPRIGHTS", "WOMENINJOURNALISM", "WOMENINLEGISLATURE", "EDPRIENRN", "EDPRISUR", "SANITATION"}, context)

                    Theta1High(R%) = CSng(estval) + theta1shift(R%)

                    Call AnalFunc("Theta 2 High",
New Single() {WOMENPROPRIGHTS(R%), WOMENINJOURNALISM(R%), WOMENINLEGISLATURE(R%), CBR(R%), POVRATE685(R%, AG_AllAges, Total), MATMORTRATIO(R%)}, estval,
New String() {"WOMENPROPRIGHTS", "WOMENINJOURNALISM", "WOMENINLEGISLATURE", "CBR", "IncBelow6D85cPct", "MMR"}, context)

                    Theta2High(R%) = CSng(estval) + theta2shift(R%)

                    Call AnalFunc("Theta 3 High",
New Single() {WOMENPROPRIGHTS(R%), WOMENINJOURNALISM(R%), WOMENINLEGISLATURE(R%), YTHBULGE(R%), ECONFREE(R%), INFRAELECACC(R%, Urban)}, estval,
New String() {"WOMENPROPRIGHTS", "WOMENINJOURNALISM", "WOMENINLEGISLATURE", "PopulationYouthDep", "FreedomEcon", "EnElecAccessPctUrban"}, context)

                    Theta3High(R%) = CSng(estval) + theta3shift(R%)

                    Call AnalFunc("Theta 1 Low",
New Single() {WOMENPROPRIGHTS(R%), WOMENINJOURNALISM(R%), WOMENINLEGISLATURE(R%), EDPRIENRN(R%, Total), EDPRISUR(R%, Total), SANITATION(R%, 3)}, estval,
New String() {"WOMENPROPRIGHTS", "WOMENINJOURNALISM", "WOMENINLEGISLATURE", "EDPRIENRN", "EDPRISUR", "SANITATION"}, context)

                    Theta1Low(R%) = estval + theta1shift(R%)

                    Call AnalFunc("Theta 2 Low",
New Single() {WOMENPROPRIGHTS(R%), WOMENINJOURNALISM(R%), WOMENINLEGISLATURE(R%), CBR(R%), POVRATE685(R%, AG_AllAges, Total), MATMORTRATIO(R%)}, estval,
New String() {"WOMENPROPRIGHTS", "WOMENINJOURNALISM", "WOMENINLEGISLATURE", "CBR", "IncBelow6D85cPct", "MMR"}, context)

                    Theta2Low(R%) = CSng(estval) + theta2shift(R%)

                    Call AnalFunc("Theta 3 Low",
New Single() {WOMENPROPRIGHTS(R%), WOMENINJOURNALISM(R%), WOMENINLEGISLATURE(R%), YTHBULGE(R%), ECONFREE(R%), INFRAELECACC(R%, Urban)}, estval,
New String() {"WOMENPROPRIGHTS", "WOMENINJOURNALISM", "WOMENINLEGISLATURE", "PopulationYouthDep", "FreedomEcon", "EnElecAccessPctUrban"}, context)

                    Theta3Low(R%) = CSng(estval) + theta3shift(R%)
                Next R%
            End If
            'clp 2026/02/25 ***END*** Add V-Dem indicator forecasts for Essex collaboration

        End Sub
