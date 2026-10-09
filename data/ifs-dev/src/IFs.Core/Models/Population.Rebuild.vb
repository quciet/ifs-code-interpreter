Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data

Namespace Models

    Partial Public Module Population

        <RebuildsVariable("POP", AggregatorType:=GetType(POPAggregator))>
        Public Function RebuildPOP(context As IRebuildContext) As IDataframe
            Using db As IFs2Context = New IFs2Context()
                If context.HistoricMode Then
                    db.Clear(GetType(AnnualGrowthRatesHist))
                Else
                    db.Clear(GetType(AnnualGrowthRates))
                End If
                'db.Clear(GetType(AnnualGrowthRatesExt))
                db.Flush()
            End Using

            Return context.Query("POP").Filter(BaseYearOnly).Execute()
        End Function

        <RebuildsVariable("TFR", AggregatorType:=GetType(TFRAggregator))>
        <RebuildsVariable("MATMORTRATIO", AggregatorType:=GetType(TFRAggregator))>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        Public Iterator Function RebuildTFR(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Dim CGDPPCP As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            If context.HistoricMode Then
                context.PutEstimator("TFR", context.GetRegression("GDP/Capita (PPP) Versus Total Fertility Rate (1960)").ToEstimator(CGDPPCP))
            Else
                'jrs 2024/07/04
                Dim TFName As String
                TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Total Fertility Rate (" & BaseYear & ")"
                TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
                'context.PutEstimator("TFR", context.GetRegression("GDP/Capita (PPP) Versus Total Fertility Rate (1995)").ToEstimator(CGDPPCP))
                context.PutEstimator("TFR", context.GetRegression(TFName).ToEstimator(CGDPPCP))
            End If
            Yield context.Query("TFR").Filter(BaseYearOnly).Execute()
            Yield context.Query("MATMORTRATIO").Filter(BaseYearOnly).Execute()
        End Function

        <RebuildsVariable("INFMOR", AggregatorType:=GetType(MORTAggregator))>
        <RebuildsVariable("NEONATMOR", AggregatorType:=GetType(MORTAggregator))>
        <ImportsAsset("CGDPPCP")>
        Public Iterator Function RebuildINFMOR(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Dim CGDPPCP As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            If context.HistoricMode Then
                context.PutEstimator("INFMOR", context.GetRegression("GDP/Capita (PPP) Versus Infant Mortality (1960)").ToEstimator(CGDPPCP))
            Else
                'jrs 2024/06/19 Disabled for now because  funtion is too old for search to find it
                'Dim TFName As String = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Infant Mortality (" & BaseYear & ")"
                'TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
                context.PutEstimator("INFMOR", context.GetRegression("GDP/Capita (PPP) Versus Infant Mortality (1995)").ToEstimator(CGDPPCP))
                'context.PutEstimator("INFMOR", context.GetRegression(TFName).ToEstimator(CGDPPCP))
            End If
            Yield context.Query("INFMOR").Filter(BaseYearOnly).Execute()
            Yield context.Query("NEONATMOR").Filter(BaseYearOnly).Execute()
        End Function

        '<RebuildsVariable("MATMORTRATIO", AggregatorType:=GetType(TFRAggregator))>
        'Public Function RebuildMATMORTRATIO(context As IRebuildContext) As IDataframe
        '    Return context.Query("MATMORTRATIO").Filter(BaseYearOnly).Execute()
        'End Function

        <RebuildsVariable("CONTRUSE", AggregatorType:=GetType(CONTRUSEAggregator))>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        Public Function RebuildCONTRUSE(context As IRebuildContext) As IDataframe
            Dim CGDPPCP As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            If context.HistoricMode Then
                context.PutEstimator("CONTRUSE", context.GetRegression("GDP/Capita (PPP) Versus Contraception Use (1975)").ToEstimator(CGDPPCP))
            Else
                'jrs 2024/06/18
                Dim TFName As String = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Contraception Use (" & BaseYear & ")"
                TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
                context.PutEstimator("CONTRUSE", context.GetRegression("GDP/Capita (PPP) Versus Contraception Use (1990)").ToEstimator(CGDPPCP))
                'This one is not found because algorithm only goes 25 years back, 1990 is too old, testing with base year = 2020
                'context.PutEstimator("CONTRUSE", context.GetRegression(TFName).ToEstimator(CGDPPCP))
            End If
            Return context.Query("CONTRUSE").Filter(BaseYearOnly).Execute()
        End Function

        <RebuildsVariable("LIFEXP", AggregatorType:=GetType(LIFEXPAggregator))>
        <ImportsAsset("CGDPPCPLT30")>
        Public Function RebuildLIFEXP(context As IRebuildContext) As IDataframe
            Dim CGDPPCPLT30 As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCPLT30")
            If context.HistoricMode Then
                context.PutEstimator("LIFEXP_Male", context.GetRegression("GDP/Capita (PPP) Versus Life Expectancy Male (1962)").ToEstimator(CGDPPCPLT30))
                context.PutEstimator("LIFEXP_Female", context.GetRegression("GDP/Capita (PPP) Versus Life Expectancy Female (1962)").ToEstimator(CGDPPCPLT30))
            Else
                context.PutEstimator("LIFEXP_Male", context.GetRegression("GDP/Capita (PPP) Versus Life Expectancy Male (2006)").ToEstimator(CGDPPCPLT30))
                context.PutEstimator("LIFEXP_Female", context.GetRegression("GDP/Capita (PPP) Versus Life Expectancy Female (2006)").ToEstimator(CGDPPCPLT30))
            End If
            Return context.Query("LIFEXP").Filter(BaseYearOnly).Execute()
        End Function
        'cdv 2022/02/28 ****START****
        <RebuildsVariable("AGEOFMARRIAGE", AggregatorType:=GetType(AGEOFMARRIAGEAggregator))>
        <ImportsAsset("CGDPPCP")>
        Public Function RebuildAGEOFMARRIAGE(context As IRebuildContext) As IDataframe
            Dim CGDPPCP As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            'If context.HistoricMode Then
            '    context.PutEstimator("LIFEXP_Male", context.GetRegression("GDP/Capita (PPP) Versus Life Expectancy Male (1962)").ToEstimator(CGDPPCPLT30))
            '    context.PutEstimator("LIFEXP_Female", context.GetRegression("GDP/Capita (PPP) Versus Life Expectancy Female (1962)").ToEstimator(CGDPPCPLT30))
            'Else
            'jrs 2024/06/20 
            Dim TFName As String
            TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus AvgAgeAtFirstMarriageMen-WB(" & BaseYear & ") Log"
            TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            'context.PutEstimator("AGEOFMARRIAGE_Male", context.GetRegression("GDP/Capita (PPP 2011) Versus AvgAgeAtFirstMarriageMen-WB(2017) Log").ToEstimator(CGDPPCP))
            context.PutEstimator("AGEOFMARRIAGE_Male", context.GetRegression(TFName).ToEstimator(CGDPPCP))
            TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus AvgAgeAtFirstMarriageWomen-WB(" & BaseYear & ") Log"
            TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            'context.PutEstimator("AGEOFMARRIAGE_Female", context.GetRegression("GDP/Capita (PPP 2011) Versus AvgAgeAtFirstMarriageWomen-WB(2017) Log").ToEstimator(CGDPPCP))
            context.PutEstimator("AGEOFMARRIAGE_Female", context.GetRegression(TFName).ToEstimator(CGDPPCP))
            'End If
            Return context.Query("AGEOFMARRIAGE").Filter(BaseYearOnly).Execute()
        End Function
        'cdv 2022/02/28 ****END****

        <RebuildsVariable("MARRIEDFEMSHR", AggregatorType:=GetType(MARRIEDFEMSHRAggregator))>
        <ImportsAsset("CEDYRSAGE25T")>
        Public Function RebuildMARRIAGEFEMSHR(context As IRebuildContext) As IDataframe
            Dim CEDYRSAGE25T As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CEDYRSAGE25T")

            Dim sFuncName As String = ""
            If context.HistoricMode Then
                sFuncName = "Education (Both Sexes, 25+) Versus Women in Marriage or Union, 15 to 19 (" & CStr(FirstYearHistory) & ") Log"
                sFuncName = getFunctionNameYrOrMstRct(sFuncName, FirstYearHistory, context)
            Else
                sFuncName = "Education (Both Sexes, 25+) Versus Women in Marriage or Union, 15 to 19 (" & CStr(BaseYear) & ") Log"
                sFuncName = getFunctionNameYrOrMstRct(sFuncName, BaseYear, context)
            End If

            context.PutEstimator("MARRIEDFEMSHR_Age15_19", context.GetRegression(sFuncName).ToEstimator(CEDYRSAGE25T))

            If context.HistoricMode Then
                sFuncName = "Education (Both Sexes, 25+) Vs Married % Diff, 20_24 and 15_19, Women (" & CStr(FirstYearHistory) & ") Lin"
                sFuncName = getFunctionNameYrOrMstRct(sFuncName, FirstYearHistory, context)
            Else
                sFuncName = "Education (Both Sexes, 25+) Vs Married % Diff, 20_24 and 15_19, Women (" & CStr(BaseYear) & ") Cubic"
                sFuncName = getFunctionNameYrOrMstRct(sFuncName, BaseYear, context)
            End If

            context.PutEstimator("MARRIEDFEMSHR_Age20_24", context.GetRegression(sFuncName).ToEstimator(CEDYRSAGE25T))

            Return context.Query("MARRIEDFEMSHR").Filter(BaseYearOnly).Execute()
        End Function

        'cdv 2022/10/28 ****START****
        <RebuildsVariable("UNPAIDLABOR", AggregatorType:=GetType(UnpaidLaborAggregator))>
        Public Function RebuildUNPAIDLABOR(context As IRebuildContext) As IDataframe
            'context.Store("UNPAIDLABOR_Male", 0)
            'context.Store("UNPAIDLABOR_Female", 0)
            Return context.Query("UNPAIDLABOR").Filter(BaseYearOnly).Execute()
        End Function
        'cdv 2022/10/28 ****END****
        <RebuildsVariable("WOMENINLEADERSHIP", AggregatorType:=GetType(WomenInLeadershipAggregator))>
        Public Function RebuildWOMENINLEADERSHIP(context As IRebuildContext) As IDataframe
            'context.Store("WOMENINLEADERSHIP", 0)
            Return context.Query("WOMENINLEADERSHIP").Filter(BaseYearOnly).Execute()
        End Function

        'cdv 2022/12/12
        <RebuildsVariable("FOODINSECURITY", AggregatorType:=GetType(FoodInsecurityAggregator))>
        Public Function RebuildFOODINSECURITY(context As IRebuildContext) As IDataframe
            Return context.Query("FOODINSECURITY").Filter(BaseYearOnly).Execute()
        End Function

        <RebuildsVariable("CBR", AggregatorType:=GetType(POPURBANAggregator))>
        <RebuildsVariable("CDR", AggregatorType:=GetType(POPURBANAggregator))>
        <RebuildsVariable("POPURBAN", AggregatorType:=GetType(POPURBANAggregator))>
        <RebuildsVariable("POPURBGR", AggregatorType:=GetType(POPURBANAggregator))>
        <RebuildsVariable("LABPARR", AggregatorType:=GetType(POPURBANAggregator))>
        <RebuildsVariable("LAB", AggregatorType:=GetType(POPURBANAggregator))>
        <ImportsAsset("CPOP")>
        <ImportsAsset("CGDPPCP")>
        Public Iterator Function RebuildPOPURBAN(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Dim CGDPPCP As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            'jrs 2024/06/20
            Dim TFName As String
            TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Labor Participation Rate Male (" & BaseYear & ") Lin"
            TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            'context.PutEstimator("LABPARR_Male", context.GetRegression("GDP/Capita (PPP 2011) Versus Labor Participation Rate Male (MostRecent) Lin").ToEstimator(CGDPPCP))
            context.PutEstimator("LABPARR_Male", context.GetRegression(TFName).ToEstimator(CGDPPCP))
            TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Labor Participation Rate Female (" & BaseYear & ") Lin"
            TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            'context.PutEstimator("LABPARR_Female", context.GetRegression("GDP/Capita (PPP 2011) Versus Labor Participation Rate Female (MostRecent) Lin").ToEstimator(CGDPPCP))
            context.PutEstimator("LABPARR_Female", context.GetRegression(TFName).ToEstimator(CGDPPCP))

            If context.HistoricMode Then
                context.PutEstimator("CBR", context.GetRegression("GDP/Capita (PPP) Versus Crude Birth Rate (1960)").ToEstimator(CGDPPCP))
            Else
                'jrs 2024/07/04
                'jrs 2024/07/23 added log to the name
                TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Crude Birth Rate (" & BaseYear & ") Log"
                TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
                'context.PutEstimator("CBR", context.GetRegression("GDP/Capita (PPP) Versus Crude Birth Rate (1998)").ToEstimator(CGDPPCP))
                context.PutEstimator("CBR", context.GetRegression(TFName).ToEstimator(CGDPPCP))
            End If

            If context.HistoricMode Then
                context.PutEstimator("CDR", context.GetRegression("GDP/Capita (PPP) Versus Crude Death Rate (1960)").ToEstimator(CGDPPCP))
            Else
                'jrs 2024/07/04
                'jrs 2024/07/23 added log to the name
                TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Crude Death Rate (" & BaseYear & ") Log"
                TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
                'context.PutEstimator("CDR", context.GetRegression("GDP/Capita (PPP) Versus Crude Death Rate (1998)").ToEstimator(CGDPPCP))
                context.PutEstimator("CDR", context.GetRegression(TFName).ToEstimator(CGDPPCP))
            End If

            If context.HistoricMode Then
                context.PutEstimator("POPURBAN", context.GetRegression("GDP/Capita (PPP) Versus Urbanization Percentage (1960)").ToEstimator(CGDPPCP))
            Else
                'jrs 2025/05/15 added Log to the name
                'jrs 2024/07/04
                TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Urbanization Percentage (" & BaseYear & ") Log"
                TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
                'context.PutEstimator("POPURBAN", context.GetRegression("GDP/Capita (PPP) Versus Urbanization Percentage (1998)").ToEstimator(CGDPPCP))
                context.PutEstimator("POPURBAN", context.GetRegression(TFName).ToEstimator(CGDPPCP))
            End If

            Yield context.Query("CBR").Filter(BaseYearOnly).Execute()
            Yield context.Query("CDR").Filter(BaseYearOnly).Execute()
            Yield context.Query("POPURBAN").Filter(BaseYearOnly).Execute()
            Yield context.Query("POPURBGR").Filter(BaseYearOnly).Execute()
            Yield context.Query("LABPARR").Filter(BaseYearOnly).Execute()
            Yield context.Query("LAB").Filter(BaseYearOnly).Execute()

        End Function

        <RebuildsVariable("LIT", AggregatorType:=GetType(LITAggregator))>
        <ImportsAsset("CPOP")>
        <ImportsAsset("CGDPPCP")>
        Public Iterator Function RebuildLIT(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Dim CGDPPCP As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            If context.HistoricMode Then
                Dim EstimateLIT As SeriesEstimator = context.GetRegression("GDP/Capita (PPP) Versus Illiteracy (1980)").ToEstimator(CGDPPCP)
                Dim NewEstimate = Function(key As String)
                                      If CGDPPCP(key) > 5 Then
                                          Return EstimateLIT(key) + 6
                                      Else
                                          Return EstimateLIT(key) + 12
                                      End If
                                  End Function

                context.PutEstimator("LIT", NewEstimate)
            Else
                Dim TbName As String
                TbName$ = "GDP/Capita (PPP) Versus Literacy (" & CStr(context.FirstYear) & ")"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, context.FirstYear, context)
                context.PutEstimator("LIT", context.GetRegression(TbName).ToEstimator(CGDPPCP))
            End If

            Yield context.Query("LIT").Filter(BaseYearOnly).Execute()
        End Function

        <RebuildsVariable("HIVRATE", AggregatorType:=GetType(HIVRatesAggregator))>
        <RebuildsVariable("hivpeakyr", AggregatorType:=GetType(HIVRatesAggregator))>
        <RebuildsVariable("hivpeakr", AggregatorType:=GetType(HIVRatesAggregator))>
        <RebuildsVariable("hivincr", AggregatorType:=GetType(HIVRatesAggregator))>
        <RebuildsVariable("clnf", AggregatorType:=GetType(HIVRatesAggregator))>
        <ImportsAsset("CPOP")>
        Public Iterator Function RebuildHIVRates(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Yield context.Query("HIVRATE").Filter(BaseYearOnly).Execute()
            'jrs 2019/11/26
            Dim dftmp As IDataframe
            dftmp = context.Query("hivpeakyr").Filter(BaseYearOnly).Execute()
            Dim dfhivpeakyr As Dataframe = Dataframe.Create("hivpeakyr", context, context.FirstYear, context.LastYear, 0)
            Dim R As Short
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    dfhivpeakyr(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR)))
                Next
            Next
            Yield dfhivpeakyr
            dftmp = context.Query("hivpeakr").Filter(BaseYearOnly).Execute()
            Dim dfhivpeakr As Dataframe = Dataframe.Create("hivpeakr", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    dfhivpeakr(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR)))
                Next
            Next
            Yield dfhivpeakr
            dftmp = context.Query("hivincr").Filter(BaseYearOnly).Execute()
            Dim dfhivincr As Dataframe = Dataframe.Create("hivincr", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    dfhivincr(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR)))
                Next
            Next
            Yield dfhivincr
            dftmp = context.Query("clnf").Filter(BaseYearOnly).Execute()
            Dim dfclnf As Dataframe = Dataframe.Create("clnf", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    dfclnf(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR)))
                Next
            Next
            Yield dfclnf
        End Function

        <RebuildsVariable("LAPOPR", AggregatorType:=GetType(LAPOPRaggregator))>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        Public Iterator Function RebuildLAPOPR(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Dim CGDPPCP As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            context.PutEstimator("LAPOPR", context.GetRegression("GDP/Capita Versus Labor Participation Rate").ToEstimator(CGDPPCP))
            Yield context.Query("LAPOPR").Filter(BaseYearOnly).Execute()

            Using db As IFsContext = New IFsContext()
                Using db2 As IFs2Context = New IFs2Context()
                    If context.HistoricMode Then
                        'mti 2020/09/01 start
                        db2.Clear(GetType(PopAgeSingleRegionMale))
                        db2.Clear(GetType(PopAgeSingleRegionFemale))

                        'Note mti - the hist table is not yet gleaned
                        'RegPopSumSingleYear(db.PopAgeSingleCountryMaleHist, context, db2.PopAgeSingleRegionMale, 100)
                        'RegPopSumSingleYear(db.PopAgeSingleCountryFemaleHist, context, db2.PopAgeSingleRegionFemale, 100)
                        RegPopSumSingleYear(db.PopAgeSingleCountryMale, context, db2.PopAgeSingleRegionMale, 100)
                        RegPopSumSingleYear(db.PopAgeSingleCountryFemale, context, db2.PopAgeSingleRegionFemale, 100)
                        'mti 2020/09/01 end

                        db2.Clear(GetType(PopAgeCohortRegionMaleHist))
                        db2.Clear(GetType(PopAgeCohortRegionFemaleHist))
                        'jrs 2019/06/03 
                        'jrs 2019/06/03 there's 2 countries with no data, that's why still running old RegDistribution
                        'Hong Kong and Kosovo
                        RegDistribution(db.PopAgeCohortCountryMaleHist, context, db2.PopAgeCohortRegionMaleHist)
                        RegDistribution(db.PopAgeCohortCountryFemaleHist, context, db2.PopAgeCohortRegionFemaleHist)

                        CPOPByAgeM = New Dictionary(Of String, Dictionary(Of String, Single?))
                        CPOPByAgeF = New Dictionary(Of String, Dictionary(Of String, Single?))
                        CPOPByAgeT = New Dictionary(Of String, Dictionary(Of String, Single?))
                        For Each item In db.PopAgeCohortCountryMaleHist
                            CPOPByAgeM.Add(item.CountryName, New Dictionary(Of String, Single?))
                            CPOPByAgeT.Add(item.CountryName, New Dictionary(Of String, Single?))
                            For Each item2 In item.Cohorts
                                CPOPByAgeM(item.CountryName).Add(item2.Key, CSng(item2.Value))
                                CPOPByAgeT(item.CountryName).Add(item2.Key, CSng(item2.Value))
                            Next
                        Next
                        For Each item In db.PopAgeCohortCountryFemaleHist
                            CPOPByAgeF.Add(item.CountryName, New Dictionary(Of String, Single?))
                            For Each item2 In item.Cohorts
                                CPOPByAgeF(item.CountryName).Add(item2.Key, CSng(item2.Value))
                                CPOPByAgeT(item.CountryName)(item2.Key) = CPOPByAgeT(item.CountryName)(item2.Key) + CSng(item2.Value)
                            Next
                        Next

                    Else
                        'mti 2020/09/01 start
                        db2.Clear(GetType(PopAgeSingleRegionMale))
                        db2.Clear(GetType(PopAgeSingleRegionFemale))

                        RegPopSumSingleYear(db.PopAgeSingleCountryMale, context, db2.PopAgeSingleRegionMale, 100)
                        RegPopSumSingleYear(db.PopAgeSingleCountryFemale, context, db2.PopAgeSingleRegionFemale, 100)
                        'mti 2020/09/01 end


                        db2.Clear(GetType(PopAgeCohortRegionMale))
                        db2.Clear(GetType(PopAgeCohortRegionFemale))
                        'jrs 2019/06/03 
                        'jrs 2019/06/03 there's 2 countries with no data, that's why still running old RegDistribution
                        'Hong Kong and Kosovo
                        RegDistribution(db.PopAgeCohortCountryMale, context, db2.PopAgeCohortRegionMale)
                        RegDistribution(db.PopAgeCohortCountryFemale, context, db2.PopAgeCohortRegionFemale)

                        CPOPByAgeM = New Dictionary(Of String, Dictionary(Of String, Single?))
                        CPOPByAgeF = New Dictionary(Of String, Dictionary(Of String, Single?))
                        CPOPByAgeT = New Dictionary(Of String, Dictionary(Of String, Single?))
                        For Each item In db.PopAgeCohortCountryMale
                            CPOPByAgeM.Add(item.CountryName, New Dictionary(Of String, Single?))
                            CPOPByAgeT.Add(item.CountryName, New Dictionary(Of String, Single?))
                            For Each item2 In item.Cohorts
                                CPOPByAgeM(item.CountryName).Add(item2.Key, CSng(item2.Value))
                                CPOPByAgeT(item.CountryName).Add(item2.Key, CSng(item2.Value))
                            Next
                        Next
                        For Each item In db.PopAgeCohortCountryFemale
                            CPOPByAgeF.Add(item.CountryName, New Dictionary(Of String, Single?))
                            For Each item2 In item.Cohorts
                                CPOPByAgeF(item.CountryName).Add(item2.Key, CSng(item2.Value))
                                CPOPByAgeT(item.CountryName)(item2.Key) = CPOPByAgeT(item.CountryName)(item2.Key) + CSng(item2.Value)
                            Next
                        Next
                    End If

                    If context.HistoricMode Then
                        db2.Clear(GetType(PopFertilityCohortRegionFemaleHist))
                        RegDistributionFert(db.PopFertilityCohortCountryFemaleHist, context, db2.PopFertilityCohortRegionFemaleHist)
                    Else
                        db2.Clear(GetType(PopFertilityCohortRegionFemale))
                        RegDistributionFert(db.PopFertilityCohortCountryFemale, context, db2.PopFertilityCohortRegionFemale)
                    End If

                    If context.HistoricMode Then
                        db2.Clear(GetType(PopMortalityCohortRegionMaleHist))
                        db2.Clear(GetType(PopMortalityCohortRegionFemaleHist))
                        'RegDistributionWAvg(db.PopMortalityCohortCountryMale, context, db2.PopMortalityCohortRegionMaleHist, True)
                        'RegDistributionWAvg(db.PopMortalityCohortCountryFemale, context, db2.PopMortalityCohortRegionFemaleHist, True)
                        RegDistributionIHMEMort(db.HealthDetailedDeathsCtry, context, db2.PopMortalityCohortRegionMaleHist, 1)
                        RegDistributionIHMEMort(db.HealthDetailedDeathsCtry, context, db2.PopMortalityCohortRegionFemaleHist, 2)
                    Else
                        db2.Clear(GetType(PopMortalityCohortRegionMale))
                        db2.Clear(GetType(PopMortalityCohortRegionFemale))
                        'RegDistributionWAvg(db.PopMortalityCohortCountryMale, context, db2.PopMortalityCohortRegionMale, True)
                        'RegDistributionWAvg(db.PopMortalityCohortCountryFemale, context, db2.PopMortalityCohortRegionFemale, True)
                        RegDistributionIHMEMort(db.HealthDetailedDeathsCtry, context, db2.PopMortalityCohortRegionFemale, 2)
                        RegDistributionIHMEMort(db.HealthDetailedDeathsCtry, context, db2.PopMortalityCohortRegionMale, 1)
                    End If

                    db2.Clear(GetType(HealthYLDComMatPerCohortRegionMale))
                    db2.Clear(GetType(HealthYLDComMatPerCohortRegionFemale))
                    RegDistributionWAvg(db.HealthYLDComMatPerCohortCountryMale, context, db2.HealthYLDComMatPerCohortRegionMale, False)
                    RegDistributionWAvg(db.HealthYLDComMatPerCohortCountryFemale, context, db2.HealthYLDComMatPerCohortRegionFemale, False)

                    db2.Clear(GetType(HealthYLDNonComDisCohortRegionMale))
                    db2.Clear(GetType(HealthYLDNonComDisCohortRegionFemale))
                    RegDistributionWAvg(db.HealthYLDNonComDisCohortCountryMale, context, db2.HealthYLDNonComDisCohortRegionMale, False)
                    RegDistributionWAvg(db.HealthYLDNonComDisCohortCountryFemale, context, db2.HealthYLDNonComDisCohortRegionFemale, False)

                    db2.Clear(GetType(HealthYLDInjuriesCohortRegionMale))
                    db2.Clear(GetType(HealthYLDInjuriesCohortRegionFemale))
                    RegDistributionWAvg(db.HealthYLDInjuriesCohortCountryMale, context, db2.HealthYLDInjuriesCohortRegionMale, False)
                    RegDistributionWAvg(db.HealthYLDInjuriesCohortCountryFemale, context, db2.HealthYLDInjuriesCohortRegionFemale, False)

                    db2.Flush()
                End Using
            End Using

            Using db As IBasecase = BasecaseContext.Open(True, context, sIFsPopFile)
                db.Clear(GetType(PopBasecase))
                db.Flush()
            End Using

        End Function

        <RebuildsVariable("FEMSHRLAB", AggregatorType:=GetType(FEMSHRLABAggregator))>
        <ImportsAsset("CGDPPCP")>
        Public Function RebuildFEMSHRLAB(context As IRebuildContext) As IDataframe
            Dim CGDPPCP As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            If context.HistoricMode Then
                Dim Estimate As SeriesEstimator = context.GetRegression("GDP/Capita (PPP) Versus Female Share of Labor Force (1960)").ToEstimator(CGDPPCP)
                Dim NewEstimate = Function(key As String)
                                      Dim MyEstimation As Single? = Estimate(key)
                                      Return ApplyLimits(MyEstimation, 5, 80)
                                  End Function
                context.PutEstimator("FEMSHRLAB", NewEstimate)
            Else
                'jrs 2024/06/12
                Dim TblName As String = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Female Share of Labor Force (" & BaseYear & ")"
                TblName = getFunctionNameYrOrMstRct(TblName, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
                'Dim Estimate As SeriesEstimator = context.GetRegression("GDP/Capita (PPP) Versus Female Share of Labor Force (1995)").ToEstimator(CGDPPCP)
                Dim Estimate As SeriesEstimator = context.GetRegression(TblName).ToEstimator(CGDPPCP)
                Dim NewEstimate = Function(key As String)
                                      Dim MyEstimation As Single? = Estimate(key)
                                      Return ApplyLimits(MyEstimation, 5, 80)
                                  End Function
                context.PutEstimator("FEMSHRLAB", NewEstimate)
            End If
            Return context.Query("FEMSHRLAB").Filter(BaseYearOnly).Execute()
        End Function

        <RebuildsVariable("LABUNEMPR", AggregatorType:=GetType(CONTRUSEAggregator))>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        <ExportsAsset("CLABUNEMPR")>
        Public Function RebuildLABUNEMPR(context As IRebuildContext) As IDataframe
            Dim CGDPPCP As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            'jrs 2024/07/04
            Dim TFName As String
            TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Unemployment Rate (" & BaseYear & ") Lin"
            TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            'context.PutEstimator("LABUNEMPR", context.GetRegression("GDP/Capita (PPP 2011) Versus Unemployment Rate (MostRecent) Lin").ToEstimator(CGDPPCP))
            context.PutEstimator("LABUNEMPR", context.GetRegression(TFName).ToEstimator(CGDPPCP))
            Return context.Query("LABUNEMPR").Filter(BaseYearOnly).Execute()
        End Function

        <RebuildsVariable("LABWAGESEXRAT", AggregatorType:=GetType(LABWAGESEXRATAggregator))>
        <ImportsAsset("CGDPPCP")>
        Public Function RebuildLABWAGESEXRAT(context As IRebuildContext) As IDataframe
            Dim CGDPPCP As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            Dim EstimateLW As SeriesEstimator = context.GetRegression("GDP/Capita (PPP 2011) Versus Wage Sex Ratio (MostRecent) Log").ToEstimator(CGDPPCP)
            Dim NewEstimateLW = Function(key As String)
                                    Dim MyEstimation As Single? = EstimateLW(key)
                                    Return ApplyLimits(MyEstimation, 0.1, 1.5)
                                End Function
            context.PutEstimator("LABWAGESEXRAT", NewEstimateLW)
            Dim dfTmp As IDataframe = context.Query("LABWAGESEXRAT").Filter(BaseYearOnly).Execute()
            Dim dfLABWAGESEXRAT As Dataframe = Dataframe.Create("LABWAGESEXRAT", context, context.FirstYear, context.LastYear, Nothing)
            Dim tmpIdx As Integer
            For r = 1 To CShort(NR)
                tmpIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, CShort(r), CShort(NR))
                For y = FirstYear To LastYear
                    dfLABWAGESEXRAT(getDataFrameIdxFromDims(y, FirstYear, CShort(r), CShort(NR))) = dfTmp(tmpIdx)
                Next
            Next
            Return dfLABWAGESEXRAT
        End Function

        '<RebuildsVariable("LABINFORMSHR", AggregatorType:=GetType(LABINFORMSHRAggregator))>
        '<ImportsAsset("CGDPPCP")>
        '<ImportsAsset("CPOP")>
        'Public Function RebuildLABINFORMSHR(context As IRebuildContext) As IDataframe
        '    Dim CGDPPCP As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
        '    context.PutEstimator("LABINFORMSHR", context.GetRegression("GDP/Capita (PPP 2011) Versus Informal Employment Share of Total Logit").ToEstimator(CGDPPCP))
        '    Return context.Query("LABINFORMSHR").Filter(BaseYearOnly).Execute()
        'End Function

        <RebuildsVariable("HHSIZE", AggregatorType:=GetType(HHSIZEAggregator))>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        Public Function RebuildHHSIZE(context As IRebuildContext) As IDataframe
            Dim CGDPPCP As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            Dim AGERATIO As IDictionary(Of String, Single?)
            If context.HistoricMode Then
                'AGERATIO = context.GetSlice("AgeRatio", context.FirstYear)
                'context.PutEstimator("HHSIZE_INTENSITY", context.GetRegression("Population Age Ratio Versus Households Intensity (1960)").ToEstimator(AGERATIO))
                context.PutEstimator("HHSIZE", context.GetRegression("GDP/Capita (PPP 2000) Versus Households Intensity (1960)").ToEstimator(CGDPPCP))
            Else
                AGERATIO = context.GetSlice("AgeRatio", context.FirstYear, CoalescePolicy.MostRecent)
                context.PutEstimator("HHSIZE_INTENSITY", context.GetRegression("Population Age Ratio Versus Households Intensity (MOSTRECENT)").ToEstimator(AGERATIO))
                'jrs 2024/06/17
                Dim TbName As String = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Households Intensity (" & BaseYear & ")"
                TbName = getFunctionNameYrOrMstRct(TbName, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
                'context.PutEstimator("HHSIZE", context.GetRegression("GDP/Capita (PPP 2000) Versus Households Intensity (MOSTRECENT)").ToEstimator(CGDPPCP))
                context.PutEstimator("HHSIZE", context.GetRegression(TbName).ToEstimator(CGDPPCP))
            End If
            Return context.Query("HHSIZE").Filter(BaseYearOnly).Execute()
        End Function

        Public CPOPByAgeM As Dictionary(Of String, Dictionary(Of String, Single?))
        Public CPOPByAgeF As Dictionary(Of String, Dictionary(Of String, Single?))
        Public CPOPByAgeT As Dictionary(Of String, Dictionary(Of String, Single?))

        '<RebuildsVariable("POPFOREIGN", AggregatorType:=GetType(MIGRATIONAggregator))>
        '<ImportsAsset("CPOP")>
        'Public Function RebuildPOPFOREIGN(context As IRebuildContext) As IDataframe
        '    Return context.Query("POPFOREIGN").Filter(BaseYearOnly).Execute()
        'End Function

        '<RebuildsVariable("hivpeakyr", AggregatorType:=GetType(HIVPeakYearAggregator))>
        'Public Function RebuildHIVPEAKYR(context As IRebuildContext) As IDataframe
        '    Return context.Query("hivpeakyr").Filter(BaseYearOnly).Execute()
        'End Function

        '<RebuildsVariable("hivincr", AggregatorType:=GetType(PopWeightedAverageAggregator))>
        'Public Function RebuildHIVINCR(context As IRebuildContext) As IDataframe
        '    Return context.Query("hivincr").Filter(BaseYearOnly).Execute()
        'End Function

        Class POPAggregator
            Inherits DefaultSumAggregator

            '<ExportsAsset("CPOP")>
            'Public Overrides Function Normalize(seriesName As String, cntx As IRebuildFacade) As IDictionary(Of String, Single?)
            '    Dim CPOP As IDictionary(Of String, Single?) = MyBase.Normalize(seriesName, cntx)
            '    cntx.Store("CPOP", CPOP)
            '    Return CPOP
            'End Function

            <ExportsAsset("CPOP")>
            <ExportsAsset("POP")>
            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Dim objWriter = New System.IO.StreamWriter(cntx.PutFdrvRun + "progress.txt", append:=True)
                objWriter.WriteLine("Processing Data on Population - Please Wait")
                objWriter.Close()

                Dim CPOP As IDictionary(Of String, Single?) = cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy)
                cntx.Store("CPOP", CPOP)

                Dim POP As Dictionary(Of Integer, Single?)

                POP = (From s In CPOP
                       Join r In cntx.Regionalization On s.Key Equals r.Key
                       Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
                    ).ToDictionary(
                       Function(grp) grp.RegionId,
                       Function(grp) grp.TotalVal
                       )

                cntx.Store("POP", POP)

                Return POP
            End Function
        End Class

        Class TFRAggregator
            Inherits PopWeightedAverageAggregator

            'Pop is used as weight for the average
            Private Property cachedTFR As IDictionary(Of String, Single?)

            Private Property cachedTFRGR As IDictionary(Of String, Single?)

            Private Property cachedMATMORTRATIO As IDictionary(Of String, Single?)

            Private Property processed As Boolean

            Private Function EstimateWith2Years(seriesName As String, cntx As IRebuildFacade) As IDictionary(Of String, Single?)
                Dim years As IList(Of Short)
                If cntx.HistoricMode Then
                    years = (From y In cntx.GetYears(seriesName)
                             Where cntx.FirstYear - 10S < y AndAlso y < cntx.FirstYear + 5S
                             Order By y Ascending
                        ).Take(5).ToList()
                Else
                    years = (From y In cntx.GetYears(seriesName)
                             Where cntx.FirstYear - 5S < y AndAlso y < cntx.FirstYear + 5S
                             Order By y Descending
                        ).Take(5).ToList()
                End If
                If years.Count < 1 Then
                    Throw New NotSupportedException($"Not enough historical data for {seriesName}.")
                End If

                Dim Yr2 As Short, Yr1 As Short, MaxYr As Short
                If cntx.HistoricMode Then
                    Yr2 = cntx.FirstYear - 5S
                    Yr1 = cntx.FirstYear - 10S
                    Dim MinYr As Short
                    MinYr = years(0)
                    If MinYr > Yr1 Then
                        Yr1 = MinYr
                        Yr2 = Yr1 + 5S
                    End If
                    MaxYr = Yr2
                Else
                    If cntx.FirstYear Mod 5 = 0 Then
                        Yr2 = cntx.FirstYear - 5S
                    Else
                        Dim diff As Short
                        diff = cntx.FirstYear Mod 5S
                        Yr2 = cntx.FirstYear + 5S - diff
                    End If

                    MaxYr = years(0)
                    If Yr2 > MaxYr Then
                        Yr2 = MaxYr
                    End If
                    Yr1 = Yr2 - 5S
                End If

                If cntx.FirstYear <= MaxYr Then
                    Return (From s0 In cntx.GetSlice(seriesName, cntx.FirstYear)
                            Join s1 In cntx.GetSlice(seriesName, Yr1) On s0.Key Equals s1.Key
                            Join s2 In cntx.GetSlice(seriesName, Yr2) On s1.Key Equals s2.Key
                            Select Key = s1.Key, Value = If(s0.Value, (s1.Value + (s2.Value - s1.Value) / 5 * (cntx.FirstYear - Yr1)))
                ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
                Else
                    Return (From s1 In cntx.GetSlice(seriesName, Yr1)
                            Join s2 In cntx.GetSlice(seriesName, Yr2) On s1.Key Equals s2.Key
                            Select Key = s1.Key, Value = (s1.Value + (s2.Value - s1.Value) / 5 * (cntx.FirstYear - Yr1))
                ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
                End If

            End Function

            Public Overrides Function Normalize(seriesName As String, cntx As IRebuildFacade) As IDictionary(Of String, Single?)
                '1. Read base year values from SeriesTFRMedUNPD, no coalescing
                '2. Find 2 years to interpolate/extrapolate
                '3. If found, use them to fill TFR
                '4. If not, then use SeriesTFR, coalescing to MostRecent
                Dim xytabl As SeriesEstimator = cntx.GetEstimator(seriesName)
                'jrs 2026/05/18 remove TFRMedUNPD and use TFR instead
                If cntx.HistoricMode Then
                    Return (From s In EstimateWith2Years("TFR", cntx)
                            Join x In cntx.GetSlice("TFR", cntx.FirstYear, DefaultPolicy) On s.Key Equals x.Key
                            Select Key = s.Key, Value = If(If(s.Value, x.Value), xytabl(s.Key))
                ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
                Else
                    Return (From s In EstimateWith2Years("TFR", cntx)
                            Join x In cntx.GetSlice("TFR", cntx.FirstYear, DefaultPolicy) On s.Key Equals x.Key
                            Select Key = s.Key, Value = If(If(s.Value, x.Value), xytabl(s.Key))
                ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
                End If
            End Function

            Private Sub PreProcess(cntx As IRebuildFacade)
                If Me.processed Then
                    Return
                End If

                Dim CGDPPCP As IDictionary(Of String, Single?) = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")

                Me.cachedTFR = New Dictionary(Of String, Single?)
                Me.cachedTFRGR = New Dictionary(Of String, Single?)
                Me.cachedMATMORTRATIO = New Dictionary(Of String, Single?)

                ' process TFR
                Dim Estimate As SeriesEstimator = cntx.GetEstimator("TFR")
                Dim ValHold As Single?
                Dim TFR5YrBk As Single?, TFRHBase As Single?
                Dim YrBk5Idx As Short, BaseIdx As Short
                Dim CalcYears As Short
                Dim slope As Single?



                '1. Read base year values from SeriesTFRMedUNPD, no coalescing
                '2. Find 2 years to interpolate/extrapolate
                '3. If found, use them to fill TFR
                '4. If not, then use SeriesTFR, coalescing to MostRecent

                'gvdb 2024/04/05
                'unused variable
                'Dim TFR As IDictionary(Of String, Single?)
                If cntx.HistoricMode Then

                    'JAM 2019-03-07 Commented due mti improved
                    '    For Each item In (From s In EstimateWith2Years("TFRMedUNPD", cntx)
                    '                      Join x In cntx.GetSlice("TFRMedUNPD", cntx.FirstYear, MyBase.Policy) On s.Key Equals x.Key
                    '                      Select Key = s.Key, Value = If(If(s.Value, x.Value), Estimate(s.Key))
                    ')
                    '        Me.cachedTFR.Add(item.Key, item.Value)
                    '    Next

                    'jrs 2020/11/18 changing TFR for TFRMedUNPD and viceversa
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs4 As Short() = cntx.GetYears("TFR").OrderBy(Function(y) y).ToArray()
                    Dim TFRMedUNPDTbl As IDictionary(Of String, Single?()) = cntx.GetDice("TFR", yrs4)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(TFRMedUNPDTbl, yrs4, key, cntx.FirstYear, HistDist)

                        'jrs 2019-07-10
                        If Not ValHold.HasValue Then
                            'jrs 2020/11/20 CalcYears = CShort(cntx.FirstYear) - (CShort(cntx.FirstYear) Mod CShort(5)) - CShort(5)
                            CalcYears = CShort(cntx.FirstYear) - (CShort(cntx.FirstYear) Mod CShort(5)) + CShort(5) 'WDI doesn't have 1955 like UNPD did
                            YrBk5Idx = getYearIdx(yrs4, CalcYears)
                            CalcYears = CShort(cntx.FirstYear) - (CShort(cntx.FirstYear) Mod CShort(5))
                            BaseIdx = getYearIdx(yrs4, CalcYears)

                            If YrBk5Idx >= 0 Then
                                TFR5YrBk = TFRMedUNPDTbl(key)(YrBk5Idx) 'TFRMEDUNPD has 5 year data from 1955 '(needed) hardcoded-year read in hist base
                            Else
                                TFR5YrBk = Nothing
                            End If

                            If BaseIdx >= 0 Then
                                TFRHBase = TFRMedUNPDTbl(key)(BaseIdx)  '(needed) hardcoded-year read in hist base          
                                'historical (early yeras) data reported in 5 year intervals; Mod 5
                            Else
                                TFRHBase = Nothing
                            End If

                            If TFR5YrBk.HasValue And TFRHBase.HasValue Then
                                'jrs 2020/11/20 slope = (TFRHBase - TFR5YrBk) / CSng(5)
                                slope = (TFR5YrBk - TFRHBase) / CSng(5)
                                ValHold = TFR5YrBk - slope * 2
                                'BBH code; not the same as growth rate; slope is rather applied on the replacement fertility rate; kept it as is                           
                            End If
                        End If

                        Me.cachedTFR.Add(key, If(ValHold, Estimate(key)))
                    Next
                    'mti 2019/04/29  Flex Hist Base End


                Else
                    'jrs 2026/05/18 remove TFRMedUNPD and use TFR instead
                    'jrs 2020/11/18 changing TFR for TFRMedUNPD and viceversa
                    For Each item In (From s In EstimateWith2Years("TFR", cntx)
                                      Join x In cntx.GetSlice("TFR", cntx.FirstYear, MyBase.Policy) On s.Key Equals x.Key
                                      Select Key = s.Key, Value = If(If(s.Value, x.Value), Estimate(s.Key))
                )
                        Me.cachedTFR.Add(item.Key, item.Value)
                    Next
                End If

                'jrs 2020/11/18 changing TFR for TFRMedUNPD and viceversa
                'process TFRGR
                Dim yrsTFRGR As Short() = cntx.GetYears("TFR").OrderByDescending(Function(y) y).ToArray()
                Dim diceTFRGR As IDictionary(Of String, Single?()) = cntx.GetDice("TFR", yrsTFRGR)
                For Each key In cntx.Regionalization.Keys
                    Me.cachedTFRGR.Add(key, getAnnualGrowthRate(diceTFRGR, yrsTFRGR, key, cntx.BaseYear, 10, -10, 4, 5, 10, 10))
                Next

                ' process MATMORT - AnalFunc
                Dim EstimateM As SeriesEstimator = cntx.GetRegression("MaternalMortalityRatio versus TFR(Log) & GDPPCP(Log)").
                Normalize("GDP per capita in 1000 USD (Log)", "TFRMedUNPD(MOSTRECENT) (Log)").ToEstimator(CGDPPCP, cachedTFR)

                If cntx.HistoricMode Then
                    'JAM 2019-07-06 Commented due mti improved
                    '    For Each item In (From s In cntx.GetSlice("MATMORTRATIO", "Earliest")
                    '                      Select Key = s.Key, Value = s.Value
                    ')
                    '        Me.cachedMATMORTRATIO.Add(item.Key, item.Value)
                    '    Next

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs4 As Short() = cntx.GetYears("MATMORTRATIO").OrderBy(Function(y) y).ToArray()
                    Dim MATMORTRATIOTbl As IDictionary(Of String, Single?()) = cntx.GetDice("MATMORTRATIO", yrs4)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(MATMORTRATIOTbl, yrs4, key, cntx.FirstYear, HistDist)

                        'jrs 2019/08/09
                        If Not ValHold.HasValue Then
                            ValHold = AMAX(EstimateM(key), 3)
                        End If

                        Me.cachedMATMORTRATIO.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else
                    For Each item In (From s In cntx.GetSlice("MATMORTRATIO", "MostRecent")
                                      Select Key = s.Key, Value = s.Value)
                        'Me.cachedMATMORTRATIO.Add(item.Key, EstimateM(item.Key))
                        Me.cachedMATMORTRATIO.Add(item.Key, If(item.Value, AMAX(EstimateM(item.Key), 3)))
                    Next
                End If

                ' mark pre-processed for all three series
                Me.processed = True
            End Sub

            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Me.PreProcess(cntx)

                Dim series As IDictionary(Of String, Single?)
                Dim LowLimit As Integer, HighLimit As Integer
                Dim weightSeries As IDictionary(Of String, Single?) = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")

                Select Case seriesName
                    Case "TFR"
                        'Need to save TFRGR to file first ****************
                        series = Me.cachedTFRGR
                        Dim Vals As IDictionary(Of Integer, Single?)
                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Where s.Value.HasValue
                                Group Value = s.Value, Weight = w.Value
                                    By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight), SumProd = Sum(Value * Weight)
                                ).ToDictionary(
                Function(grp) grp.RegionId,
                Function(grp) grp.SumProd / grp.TotalWeight
                )

                        For ii = 1 To NR
                            If Not Vals.ContainsKey(ii) Then
                                Vals.Add(ii, Nothing)
                            End If
                        Next

                        BasePutAnnualGrowthOneDim("TFRGR", Vals, cntx)
                        '********************************************************
                        series = Me.cachedTFR
                        LowLimit = 1
                        HighLimit = 12
                    Case "MATMORTRATIO"
                        series = Me.cachedMATMORTRATIO
                        LowLimit = 1
                        HighLimit = 1360
                    Case Else
                        Throw New ArgumentException($"Aggregator cannot be applied on series {seriesName}.")
                End Select

                Return (From s In series
                        Join r In cntx.Regionalization On s.Key Equals r.Key
                        Join w In weightSeries On s.Key Equals w.Key
                        Group Value = s.Value, Weight = w.Value
                    By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                ).ToDictionary(
                Function(grp) grp.RegionId,
                Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, LowLimit, HighLimit)
                )
            End Function

        End Class

        Class LIFEXPAggregator
            Inherits PopWeightedAverageAggregator

            'Public Overrides Function Normalize(seriesName As String, cntx As IRebuildFacade) As IDictionary(Of String, Single?)
            '    '1. Taiwan is using values from Korea
            '    '2. In historic mode China is using constant values
            '    Dim rawLIFEXP As IDictionary(Of String, Single?) = cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy)
            '    If cntx.HistoricMode Then
            '        Select Case seriesName
            '            Case "LIFEXP_Male"
            '                rawLIFEXP("China") = 51.5
            '            Case "LIFEXP_Female"
            '                rawLIFEXP("China") = 56.8
            '        End Select
            '    End If
            '    If seriesName <> "LIFEXP_Total" Then
            '        rawLIFEXP("Taiwan, China") = rawLIFEXP("Korea, Republic of")
            '    End If
            '    Dim xytabl As SeriesEstimator = cntx.GetEstimator(seriesName)
            '    Return rawLIFEXP.ToDictionary(Function(s) s.Key, Function(s) If(s.Value, xytabl(s.Key)))
            'End Function

            <ExportsAsset("LIFEXP_Male")>
            <ExportsAsset("LIFEXP_Female")>
            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                'JAM 2019-07-11 Commented due mti improvement
                'Dim series As IDictionary(Of String, Single?) = cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy)
                Dim Valhold As Single?
                Dim series As IDictionary(Of String, Single?)


                If cntx.HistoricMode Then 'JAM 2019-07-11
                    series = New Dictionary(Of String, Single?)

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs As Short() = cntx.GetYears(seriesName).OrderBy(Function(y) y).ToArray()
                    Dim LifeExpTbl As IDictionary(Of String, Single?()) = cntx.GetDice(seriesName, yrs)
                    For Each key In cntx.Regionalization.Keys
                        Valhold = getClosestYrVal(LifeExpTbl, yrs, key, cntx.FirstYear, HistDist)
                        series.Add(key, Valhold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else
                    series = cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy)

                End If

                Dim weightSeries As IDictionary(Of String, Single?)
                weightSeries = CPOP
                'gvdb 2024/04/05
            	'unused variable
                'Dim MinVal As Single, MaxVal As Single,
                Dim bLimits As Boolean = False
                Dim bSum As Boolean = False

                If cntx.HistoricMode Then
                    Select Case seriesName
                        Case "LIFEXP_Male"
                            series("China") = 51.5
                        Case "LIFEXP_Female"
                            series("China") = 56.8
                    End Select
                End If
                If seriesName <> "LIFEXP_Total" Then
                    series("Taiwan, China") = series("Korea, Republic of")
                End If
                Dim xytabl As SeriesEstimator = cntx.GetEstimator(seriesName)

                Dim LIFEXP As Dictionary(Of Integer, Single?)

                LIFEXP = (From s In series
                          Join r In cntx.Regionalization On s.Key Equals r.Key
                          Join w In weightSeries On s.Key Equals w.Key
                          Group Value = If(s.Value, xytabl(s.Key)), Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                    ).ToDictionary(
                       Function(grp) grp.RegionId,
                       Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 30, 200)
                       )

                Select Case seriesName
                    Case "LIFEXP_Male"
                        cntx.Store("LIFEXP_Male", LIFEXP)
                    Case "LIFEXP_Female"
                        cntx.Store("LIFEXP_Female", LIFEXP)
                End Select

                Return LIFEXP
            End Function
        End Class
        'cdv 2022/03/01 ****START****
        Class AGEOFMARRIAGEAggregator
            Inherits PopWeightedAverageAggregator
            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                'gvdb 2024/04/05
            	'unused variable
                'Dim Valhold As Single?
                Dim series As IDictionary(Of String, Single?)
                series = New Dictionary(Of String, Single?)
                Dim xytabl As SeriesEstimator = cntx.GetEstimator(seriesName)

                Dim weightSeries As IDictionary(Of String, Single?)
                weightSeries = CPOP
                'mti 2022/10/24 *** start ***
                Dim yrs As Short()
                Dim AgeofMarriageTbl As IDictionary(Of String, Single?())
                Dim sDim2 As String
                If seriesName = "AGEOFMARRIAGE_Male" Then
                    sDim2 = "Male"
                    'jrs 2024/06/12 AGEOFMARRIAGE_Male
                    'yrs = cntx.GetYears("SeriesAvgAgeAtFirstMarriageMenWB").OrderBy(Function(y) y).ToArray()
                    'AgeofMarriageTbl = cntx.GetDice("SeriesAvgAgeAtFirstMarriageMenWB", yrs)
                    yrs = cntx.GetYears("AGEOFMARRIAGE_Male").OrderBy(Function(y) y).ToArray()
                    AgeofMarriageTbl = cntx.GetDice("AGEOFMARRIAGE_Male", yrs)
                Else
                    sDim2 = "Female"
                    'jrs 2024/06/12
                    'yrs = cntx.GetYears("SeriesAvgAgeAtFirstMarriageWomenWB").OrderBy(Function(y) y).ToArray()
                    'AgeofMarriageTbl = cntx.GetDice("SeriesAvgAgeAtFirstMarriageWomenWB", yrs)
                    yrs = cntx.GetYears("AGEOFMARRIAGE_Female").OrderBy(Function(y) y).ToArray()
                    AgeofMarriageTbl = cntx.GetDice("AGEOFMARRIAGE_Female", yrs)
                End If
                For Each key In cntx.Regionalization.Keys
                    series.Add(key, getAnnualGrowthRate(AgeofMarriageTbl, yrs, key, cntx.BaseYear, 10, -10, 4, 5, 20, 5))
                Next
                Dim Vals = (From s In series
                            Join r In cntx.Regionalization On s.Key Equals r.Key
                            Join w In weightSeries On s.Key Equals w.Key
                            Group Value = s.Value, Weight = w.Value
              By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)
                BasePutAnnualGrowthTwoDim("AGEOFMARRIAGEGR", Vals, cntx, sDim2)
                series.Clear()
                'mti 2022/10/24 *** end ***


                If cntx.HistoricMode Then
                    If seriesName = "AGEOFMARRIAGE_Male" Then
                        For Each item In (From s In cntx.GetSlice(seriesName, "Earliest")
                                          Join s2 In cntx.GetSlice("SeriesAvgAgeAtFirstMarriageMenUN", "Earliest") On s.Key Equals s2.Key
                                          Select Key = s.Key, Value = If(s.Value, s2.Value))
                            series.Add(item.Key, item.Value)
                            If Not series(item.Key).HasValue Then
                                series(item.Key) = xytabl(item.Key)
                            End If
                        Next
                    Else
                        For Each item In (From s In cntx.GetSlice(seriesName, "Earliest")
                                          Join s2 In cntx.GetSlice("SeriesAvgAgeAtFirstMarriageWomenUN", "Earliest") On s.Key Equals s2.Key
                                          Select Key = s.Key, Value = If(s.Value, s2.Value))
                            series.Add(item.Key, item.Value)
                            If Not series(item.Key).HasValue Then
                                series(item.Key) = xytabl(item.Key)
                            End If
                        Next
                    End If
                Else
                    If seriesName = "AGEOFMARRIAGE_Male" Then
                        For Each item In (From s In cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy)
                                          Join s2 In cntx.GetSlice("SeriesAvgAgeAtFirstMarriageMenUN", cntx.FirstYear, DefaultPolicy) On s.Key Equals s2.Key
                                          Select Key = s.Key, Value = If(s.Value, s2.Value))
                            series.Add(item.Key, item.Value)
                            If Not series(item.Key).HasValue Then
                                series(item.Key) = xytabl(item.Key)
                            End If
                        Next
                    Else
                        For Each item In (From s In cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy)
                                          Join s2 In cntx.GetSlice("SeriesAvgAgeAtFirstMarriageWomenUN", cntx.FirstYear, DefaultPolicy) On s.Key Equals s2.Key
                                          Select Key = s.Key, Value = If(s.Value, s2.Value))
                            series.Add(item.Key, item.Value)
                            If Not series(item.Key).HasValue Then
                                series(item.Key) = xytabl(item.Key)
                            End If
                        Next
                    End If

                End If

				'gvdb 2024/04/05
            	'unused variable
                'Dim MinVal As Single, MaxVal As Single,
                Dim bLimits As Boolean = False
                Dim bSum As Boolean = False



                Dim AGEOFMARRIAGE As Dictionary(Of Integer, Single?)

                AGEOFMARRIAGE = (From s In series
                                 Join r In cntx.Regionalization On s.Key Equals r.Key
                                 Join w In weightSeries On s.Key Equals w.Key
                                 Group Value = If(s.Value, xytabl(s.Key)), Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                    ).ToDictionary(
                       Function(grp) grp.RegionId,
                       Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 0, 200)
                       )

                Select Case seriesName
                    Case "AGEOFMARRIAGE_Male"
                        cntx.Store("AGEOFMARRIAGE_Male", AGEOFMARRIAGE)
                    Case "AGEOFMARRIAGE_Female"
                        cntx.Store("AGEOFMARRIAGE_Female", AGEOFMARRIAGE)
                End Select

                Return AGEOFMARRIAGE
            End Function
        End Class
        'cdv 2022/03/01 ****END****

        Class MARRIEDFEMSHRAggregator
            Inherits PopWeightedAverageAggregator

            Dim series1519 As IDictionary(Of String, Single?)

            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
            	'gvdb 2024/04/05
            	'unused variable
                'Dim Valhold As Single?
                Dim series As IDictionary(Of String, Single?)
                Dim seriesGR As IDictionary(Of String, Single?)
                series = New Dictionary(Of String, Single?)
                seriesGR = New Dictionary(Of String, Single?)
                Dim xytabl As SeriesEstimator = cntx.GetEstimator(seriesName)

                Dim weightSeries As IDictionary(Of String, Single?)
                weightSeries = CPOP
                'mti 2022/10/24 *** start ***
                Dim yrs As Short()
                Dim MarriedFemShrTbl As IDictionary(Of String, Single?())
                Dim sDim2 As String

                If seriesName = "MARRIEDFEMSHR_Age15_19" Then
                    sDim2 = "Age15_19"
                    yrs = cntx.GetYears("MARRIEDFEMSHR_Age15_19").OrderBy(Function(y) y).ToArray()
                    MarriedFemShrTbl = cntx.GetDice("MARRIEDFEMSHR_Age15_19", yrs)
                Else
                    sDim2 = "Age20_24"
                    yrs = cntx.GetYears("MARRIEDFEMSHR_Age20_24").OrderBy(Function(y) y).ToArray()
                    MarriedFemShrTbl = cntx.GetDice("MARRIEDFEMSHR_Age20_24", yrs)
                End If

                If cntx.HistoricMode Then
                    For Each key In cntx.Regionalization.Keys
                        seriesGR.Add(key, getAnnualGrowthRate(MarriedFemShrTbl, yrs, key, FirstYearHistory, 0.15, -0.15, 5, 5, 20, 30))
                    Next
                Else
                    For Each key In cntx.Regionalization.Keys
                        seriesGR.Add(key, getAnnualGrowthRate(MarriedFemShrTbl, yrs, key, cntx.BaseYear, 0.1, -0.1, 5, 5, 20, 5))
                    Next
                End If

                Dim Vals = (From s In seriesGR
                            Join r In cntx.Regionalization On s.Key Equals r.Key
                            Join w In weightSeries On s.Key Equals w.Key
                            Group Value = s.Value, Weight = w.Value
              By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)
                BasePutAnnualGrowthTwoDim("MARRIEDFEMSHRGR", Vals, cntx, sDim2)
                'mti 2022/10/24 *** end ***


                If cntx.HistoricMode Then
                    For Each key In cntx.Regionalization.Keys
                        series.Add(key, getClosestYrVal(MarriedFemShrTbl, yrs, key, FirstYearHistory, HistDist))
                        If Not series(key).HasValue Then
                            series(key) = xytabl(key)
                            If seriesName = "MARRIEDFEMSHR_Age20_24" Then
                                series(key) = series(key) + series1519(key)
                            End If
                        End If
                        If seriesGR(key) <> -9999 Then
                            Dim MRYr = getYearNameMstRctWData(MarriedFemShrTbl, key, yrs)
                            If MRYr < BaseYear Then
                                Dim YrNo = BaseYear - MRYr
                                series(key) = CSng(series(key) * ((1 + seriesGR(key)) ^ YrNo))
                            End If
                        End If
                    Next
                Else
                    For Each key In cntx.Regionalization.Keys
                        series.Add(key, getYearOrMstRct(MarriedFemShrTbl, yrs, key, BaseYear))
                        If Not series(key).HasValue Then
                            series(key) = xytabl(key)
                            If seriesName = "MARRIEDFEMSHR_Age20_24" Then
                                series(key) = series(key) + series1519(key)
                            End If
                        End If
                        If seriesGR(key) <> -9999 Then
                            Dim MRYr = getYearNameMstRctWData(MarriedFemShrTbl, key, yrs)
                            If MRYr < BaseYear Then
                                Dim YrNo = BaseYear - MRYr
                                series(key) = CSng(series(key) * ((1 + seriesGR(key)) ^ YrNo))
                            End If
                        End If
                    Next
                End If

                If seriesName = "MARRIEDFEMSHR_Age15_19" Then
                    series1519 = series
                End If

                Dim MARRIAGEFEMSHR As Dictionary(Of Integer, Single?)

                MARRIAGEFEMSHR = (From s In series
                                  Join r In cntx.Regionalization On s.Key Equals r.Key
                                  Join w In weightSeries On s.Key Equals w.Key
                                  Group Value = If(s.Value, xytabl(s.Key)), Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                    ).ToDictionary(
                       Function(grp) grp.RegionId,
                       Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 1, 95)
                       )

                Return MARRIAGEFEMSHR
            End Function
        End Class

        'cdv 2022/10/28 ****START****
        Class UnpaidLaborAggregator
            Inherits PopWeightedAverageAggregator
            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                'gvdb 2024/04/05
            	'unused variable
                'Dim Valhold As Single?
                Dim series As IDictionary(Of String, Single?)
                series = New Dictionary(Of String, Single?)
                Dim xytabl As SeriesEstimator = cntx.GetEstimator(seriesName)

                Dim weightSeries As IDictionary(Of String, Single?)
                weightSeries = CPOP

                If cntx.HistoricMode Then
                    If seriesName = "UNPAIDLABOR_Male" Then
                        For Each item In (From s In cntx.GetSlice(seriesName, "Earliest")
                                          Join s2 In cntx.GetSlice("UNPAIDLABOR_Male", "Earliest") On s.Key Equals s2.Key
                                          Select Key = s.Key, Value = If(s.Value, s2.Value))
                            series.Add(item.Key, item.Value)
                            If Not series(item.Key).HasValue Then
                                series(item.Key) = xytabl(item.Key)
                            End If
                        Next
                    Else
                        For Each item In (From s In cntx.GetSlice(seriesName, "Earliest")
                                          Join s2 In cntx.GetSlice("UNPAIDLABOR_Female", "Earliest") On s.Key Equals s2.Key
                                          Select Key = s.Key, Value = If(s.Value, s2.Value))
                            series.Add(item.Key, item.Value)
                            If Not series(item.Key).HasValue Then
                                series(item.Key) = xytabl(item.Key)
                            End If
                        Next
                    End If
                Else
                    If seriesName = "UNPAIDLABOR_Male" Then
                        For Each item In (From s In cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy)
                                          Join s2 In cntx.GetSlice("UNPAIDLABOR_Male", cntx.FirstYear, DefaultPolicy) On s.Key Equals s2.Key
                                          Select Key = s.Key, Value = If(s.Value, s2.Value))
                            series.Add(item.Key, item.Value)
                            If Not series(item.Key).HasValue Then
                                series(item.Key) = xytabl(item.Key)
                            End If
                        Next
                    Else
                        For Each item In (From s In cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy)
                                          Join s2 In cntx.GetSlice("UNPAIDLABOR_Female", cntx.FirstYear, DefaultPolicy) On s.Key Equals s2.Key
                                          Select Key = s.Key, Value = If(s.Value, s2.Value))
                            series.Add(item.Key, item.Value)
                            If Not series(item.Key).HasValue Then
                                series(item.Key) = xytabl(item.Key)
                            End If
                        Next
                    End If

                End If

                'gvdb 2024/04/05
            	'unused variable
                'Dim MinVal As Single, MaxVal As Single,
                Dim bLimits As Boolean = False
                Dim bSum As Boolean = False



                Dim UNPAIDLABOR As Dictionary(Of Integer, Single?)

                UNPAIDLABOR = (From s In series
                               Join r In cntx.Regionalization On s.Key Equals r.Key
                               Join w In weightSeries On s.Key Equals w.Key
                               Group Value = If(s.Value, xytabl(s.Key)), Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                    ).ToDictionary(
                       Function(grp) grp.RegionId,
                       Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 0, 200)
                       )

                Select Case seriesName
                    Case "UNPAIDLABOR_Male"
                        cntx.Store("UNPAIDLABOR _Male", UNPAIDLABOR)
                    Case "UNPAIDLABOR_Female"
                        cntx.Store("UNPAIDLABOR _Female", UNPAIDLABOR)
                End Select

                Return UNPAIDLABOR
            End Function
        End Class
        'cdv 2022/10/28 ****END****

        'cdv 2022/11/18 ****START****
        Class WomenInLeadershipAggregator
            Inherits PopWeightedAverageAggregator
            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                'gvdb 2024/04/05
            	'unused variable
                'Dim Valhold As Single?
                Dim series As IDictionary(Of String, Single?)
                series = New Dictionary(Of String, Single?)
                Dim xytabl As SeriesEstimator = cntx.GetEstimator(seriesName)

                Dim weightSeries As IDictionary(Of String, Single?)
                weightSeries = CPOP

                If cntx.HistoricMode Then
                    For Each item In (From s In cntx.GetSlice(seriesName, "Earliest")
                                      Select Key = s.Key, Value = If(s.Value, 0))
                        series.Add(item.Key, item.Value)
                    Next
                Else
                    For Each item In (From s In cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy)
                                      Select Key = s.Key, Value = If(s.Value, 0))
                        series.Add(item.Key, item.Value)
                    Next
                End If

                'gvdb 2024/04/05
            	'unused variable
                'Dim MinVal As Single, MaxVal As Single, 
                Dim bLimits As Boolean = False
                Dim bSum As Boolean = False



                Dim WOMENINLEADERSHIP As Dictionary(Of Integer, Single?)

                WOMENINLEADERSHIP = (From s In series
                                     Join r In cntx.Regionalization On s.Key Equals r.Key
                                     Join w In weightSeries On s.Key Equals w.Key
                                     Group Value = If(s.Value, xytabl(s.Key)), Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                    ).ToDictionary(
                       Function(grp) grp.RegionId,
                       Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 0, 200)
                       )

                Return WOMENINLEADERSHIP
            End Function
        End Class
        'cdv 2022/11/18 ****END****

        'cdv 2022/12/12 ****START****
        Class FoodInsecurityAggregator
            Inherits PopWeightedAverageAggregator
            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                'gvdb 2024/04/05
            	'unused variable
                'Dim Valhold As Single?
                Dim series As IDictionary(Of String, Single?)
                series = New Dictionary(Of String, Single?)
                Dim xytabl As SeriesEstimator = cntx.GetEstimator(seriesName)

                Dim weightSeries As IDictionary(Of String, Single?)
                weightSeries = CPOP

                If cntx.HistoricMode Then
                    If seriesName = "FOODINSECURITY_Male" Then
                        For Each item In (From s In cntx.GetSlice(seriesName, "Earliest")
                                          Join s2 In cntx.GetSlice("FOODINSECURITY_Male", "Earliest") On s.Key Equals s2.Key
                                          Select Key = s.Key, Value = If(s.Value, s2.Value))
                            series.Add(item.Key, item.Value)
                            If Not series(item.Key).HasValue Then
                                series(item.Key) = 0
                            End If
                        Next
                    Else
                        For Each item In (From s In cntx.GetSlice(seriesName, "Earliest")
                                          Join s2 In cntx.GetSlice("FOODINSECURITY_Female", "Earliest") On s.Key Equals s2.Key
                                          Select Key = s.Key, Value = If(s.Value, s2.Value))
                            series.Add(item.Key, item.Value)
                            If Not series(item.Key).HasValue Then
                                series(item.Key) = 0
                            End If
                        Next
                    End If
                Else
                    If seriesName = "FOODINSECURITY_Male" Then
                        For Each item In (From s In cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy)
                                          Join s2 In cntx.GetSlice("FOODINSECURITY_Male", cntx.FirstYear, DefaultPolicy) On s.Key Equals s2.Key
                                          Select Key = s.Key, Value = If(s.Value, s2.Value))
                            series.Add(item.Key, item.Value)
                            If Not series(item.Key).HasValue Then
                                series(item.Key) = 0
                            End If
                        Next
                    Else
                        For Each item In (From s In cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy)
                                          Join s2 In cntx.GetSlice("FOODINSECURITY_Female", cntx.FirstYear, DefaultPolicy) On s.Key Equals s2.Key
                                          Select Key = s.Key, Value = If(s.Value, s2.Value))
                            series.Add(item.Key, item.Value)
                            If Not series(item.Key).HasValue Then
                                series(item.Key) = 0
                            End If
                        Next
                    End If

                End If

                'gvdb 2024/04/05
            	'unused variable
                'Dim MinVal As Single, MaxVal As Single,
                Dim bLimits As Boolean = False
                Dim bSum As Boolean = False



                Dim FOODINSECURITY As Dictionary(Of Integer, Single?)

                FOODINSECURITY = (From s In series
                                  Join r In cntx.Regionalization On s.Key Equals r.Key
                                  Join w In weightSeries On s.Key Equals w.Key
                                  Group Value = If(s.Value, xytabl(s.Key)), Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                    ).ToDictionary(
                       Function(grp) grp.RegionId,
                       Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 0, 200)
                       )

                Select Case seriesName
                    Case "FOODINSECURITY_Male"
                        cntx.Store("FOODINSECURITY_Male", FOODINSECURITY)
                    Case "FOODINSECURITY_Female"
                        cntx.Store("FOODINSECURITY_Female", FOODINSECURITY)
                End Select

                Return FOODINSECURITY
            End Function
        End Class
        Class CONTRUSEAggregator
            Inherits PopWeightedAverageAggregator

            Private Property weightSeries As IDictionary(Of String, Single?)

            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))

                weightSeries = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")

                Dim Estimate As SeriesEstimator = cntx.GetEstimator(seriesName)

                Dim CONTRUSE As IDictionary(Of String, Single?)
                Dim ValHold As Single?

                If cntx.HistoricMode Then
                    CONTRUSE = New Dictionary(Of String, Single?)
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs6 As Short() = cntx.GetYears(seriesName).OrderBy(Function(y) y).ToArray()
                    Dim CONTRUSETbl As IDictionary(Of String, Single?()) = cntx.GetDice(seriesName, yrs6)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(CONTRUSETbl, yrs6, key, cntx.FirstYear, HistDist)
                        'CONTRUSE.Add(key, ValHold)
                        CONTRUSE.Add(key, ApplyLimits(If(ValHold, Estimate(key)), 1, 90))

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else
                    CONTRUSE = (From s In cntx.GetSlice(seriesName, cntx.FirstYear, MyBase.Policy)
                                Select Key = s.Key, Value = ApplyLimits(If(s.Value, Estimate(s.Key)), 1, 90)
                    ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
                End If

                'JAM 2019-07-13 Commented due mti improvement
                'CONTRUSE = (From s In cntx.GetSlice(seriesName, cntx.FirstYear, MyBase.Policy)
                '            Select Key = s.Key, Value = ApplyLimits(If(s.Value, Estimate(s.Key)), 1, 90)
                ' ).ToDictionary(Function(s) s.Key, Function(s) s.Value)

                Dim series As IDictionary(Of String, Single?) = CONTRUSE

                If seriesName = "CONTRUSE" Then
                    Return (From s In series
                            Join r In cntx.Regionalization On s.Key Equals r.Key
                            Join w In Me.weightSeries On s.Key Equals w.Key
                            Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
        ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 0, 100)
        )
                Else 'LABUNEMPR

                    If cntx.HistoricMode Then

                        'mti 2019/04/29  Flex Hist Base Start
                        Dim yrs9 As Short() = cntx.GetYears("LABUNEMPR").OrderBy(Function(y) y).ToArray()
                        Dim LABUNEMPRTbl As IDictionary(Of String, Single?()) = cntx.GetDice("LABUNEMPR", yrs9)
                        For Each key In cntx.Regionalization.Keys
                            ValHold = getClosestYrVal(LABUNEMPRTbl, yrs9, key, cntx.FirstYear, HistDist)
                            series(key) = If(ValHold, Estimate(key))

                        Next
                        'mti 2019/04/29  Flex Hist Base End


                    End If

                    For Each key In cntx.Regionalization.Keys
                        series(key) = ApplyLimits(series(key), 1, 90)
                    Next

                    cntx.Store("CLABUNEMPR", series)

                    Return (From s In series
                            Join r In cntx.Regionalization On s.Key Equals r.Key
                            Join w In Me.weightSeries On s.Key Equals w.Key
                            Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
        ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 1, 90)
        )
                End If
            End Function

            Public Overrides Function Normalize(seriesName As String, cntx As IRebuildFacade) As IDictionary(Of String, Single?)
                Dim xytabl As SeriesEstimator = cntx.GetEstimator(seriesName)
                Return cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy).ToDictionary(
                Function(s) s.Key, Function(s) ApplyBounds(If(s.Value, xytabl(s.Key)), 0, 100))
            End Function

        End Class

        ' Aggregator for HIVRATE (HIV rate) and HIVPEAKR (HIV peak rate), 
        ' there is a circular dependency between the country-based series CHIVRATE and CHIVPEAKR,
        ' so we have to normalize them together. in general, each variable should have its own
        ' aggregator, and this composite aggregator pattern should be avoided.
        Class HIVRatesAggregator
            Inherits PopWeightedAverageAggregator

            Private Property weightSeries As IDictionary(Of String, Single?)

            Private Property cachedRate As IDictionary(Of String, Single?)

            Private Property cachedHIVINCR As IDictionary(Of String, Single?)

            Private Property cachedPeakRate As IDictionary(Of String, Single?)

            Private Property cachedPeakYear As IDictionary(Of String, Single?)

            Private Property cachedAIDSDTHS As IDictionary(Of String, Single?)

            Private Property cachedCLNF As IDictionary(Of String, Single?)

            Private Property processed As Boolean

            Private Sub PreProcess(cntx As IRebuildFacade)
                If Me.processed Then
                    Return
                End If

                Me.weightSeries = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
                Me.cachedRate = New Dictionary(Of String, Single?)
                Me.cachedHIVINCR = New Dictionary(Of String, Single?)
                Me.cachedPeakRate = New Dictionary(Of String, Single?)
                Me.cachedPeakYear = New Dictionary(Of String, Single?)
                Me.cachedAIDSDTHS = New Dictionary(Of String, Single?)
                Me.cachedCLNF = New Dictionary(Of String, Single?)

                ' process HIVRATE
                If cntx.HistoricMode Then
                    For Each key In cntx.Regionalization.Keys
                        Me.cachedRate.Add(key, 0.0F)
                    Next
                Else
                    For Each item In (From s In cntx.GetSlice("HIVRATE", cntx.FirstYear, MyBase.Policy)
                                      Join s1 In cntx.GetSlice("HIVRATEHigh", cntx.FirstYear, MyBase.Policy) On s.Key Equals s1.Key
                                      Join s2 In cntx.GetSlice("HIVRATELow", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join w In Me.weightSeries On s.Key Equals w.Key
                                      Select Key = s.Key, Value = If(If(s.Value, (s1.Value + s2.Value) / 2), w.Value * CSng(0.0001)) * 100 / w.Value)
                        Me.cachedRate.Add(item.Key, item.Value)
                    Next
                End If

                ' process HIVPEAKR
                'Dim EstimatePeakR As Func(Of String, Single?) = cntx.GetEstimator("hivpeakr")
                For Each item In (
                    From s In cntx.GetSlice("hivpeakr", "PeakPrevalence")
                    Select Key = s.Key, Value = If(s.Value, Me.cachedRate(s.Key)))
                    If cntx.HistoricMode Then
                        Me.cachedPeakRate.Add(item.Key, 0)
                    Else
                        Me.cachedPeakRate.Add(item.Key, item.Value)
                    End If
                    ' patch HIVRATE to not exceed HIVPEAKR
                    If Me.cachedRate(item.Key) > item.Value Then
                        Me.cachedRate(item.Key) = item.Value
                    End If
                Next
                ' process HIVPEAKYR
                'Dim EstimatePeakYear As Func(Of String, Single?) = cntx.GetEstimator("hivpeakyr")
                For Each item In (
                    From s In cntx.GetSlice("hivpeakyr", "PeakYear")
                    Select Key = s.Key, Value = s.Value)
                    If cntx.HistoricMode Then
                        Me.cachedPeakYear.Add(item.Key, 1960)
                    Else
                        Me.cachedPeakYear.Add(item.Key, If(item.Value, 2100))
                    End If
                Next
                'process AIDSDTHS
                If cntx.HistoricMode Then
                    For Each key In cntx.Regionalization.Keys
                        Me.cachedAIDSDTHS.Add(key, 0.0F)
                    Next
                Else
                    For Each item In (From s In cntx.GetSlice("AIDSDTHS", cntx.FirstYear, MyBase.Policy)
                                      Join s1 In cntx.GetSlice("AIDSDthsHigh", cntx.FirstYear, MyBase.Policy) On s.Key Equals s1.Key
                                      Join s2 In cntx.GetSlice("AIDSDthsLow", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Select Key = s.Key, Value = If(If(If(s.Value, (s1.Value + s2.Value) / 2), s1.Value), s2.Value))
                        Me.cachedAIDSDTHS.Add(item.Key, If(If(item.Value, 0.001F * Me.cachedRate(item.Key) * Me.weightSeries(item.Key)), 0F))
                    Next

                End If
                'process CLNF
                For Each key In cntx.Regionalization.Keys
                    Me.cachedCLNF.Add(key, 1.0F)
                Next

                'process HIVINCR
                'sgh 2016/11/21 - START - calculate HIV prevalence increase rate using either data or "peak year"
                'and "peak prevalence" data from the UNAIDS SPECTRUM model forecasts.
                'Use "BaseYear" so that year is no longer hard-coded. Use the "getAnnualGrowthRate" function
                'rather than calculating manually.
                If cntx.HistoricMode Then
                    For Each key In cntx.Regionalization.Keys
                        Me.cachedHIVINCR.Add(key, 0.0F)
                    Next
                Else
                    Dim MyVal As Single?
                    Dim yrsHIVINCR As Short() = cntx.GetYears("HIVRATE").OrderByDescending(Function(y) y).ToArray()
                    Dim diceHIVINCR As IDictionary(Of String, Single?()) = cntx.GetDice("HIVRATE", yrsHIVINCR)
                    For Each key In cntx.Regionalization.Keys
                        Me.cachedHIVINCR.Add(key, getAnnualGrowthRate(diceHIVINCR, yrsHIVINCR, key, cntx.FirstYear))
                    Next

                    For Each key In cntx.Regionalization.Keys
                        If cachedPeakYear(key) < cntx.FirstYear And cachedHIVINCR(key) >= 0 Then
                            cachedHIVINCR.Remove(key)
                            Me.cachedHIVINCR.Add(key, CSng((cachedPeakRate(key) / AMAX(cachedPeakRate(key), cachedRate(key))) ^
                                             (1 / (cntx.FirstYear - cachedPeakYear(key))) - 1))
                        End If

                        'if have coming peak year from data, recompute rate unless data already show decrease
                        If cachedHIVINCR(key) >= 0 Then
                            If cachedPeakYear(key) > cntx.FirstYear And cachedPeakYear(key) < 2100 Then
                                cachedHIVINCR.Remove(key)
                                Me.cachedHIVINCR.Add(key, CSng(2 * ((cachedPeakRate(key) / Amin(cachedPeakRate(key), cachedRate(key))) ^
                                             (1 / (cachedPeakYear(key) - cntx.FirstYear)) - 1)))
                                '2x larger than computed, because will slow
                            End If 'decimal form
                        End If
                        MyVal = cachedHIVINCR(key)
                        If Not MyVal.HasValue Then
                            MyVal = -0.1
                        End If
                        cachedHIVINCR.Remove(key)
                        Me.cachedHIVINCR.Add(key, ApplyLimits(MyVal, -0.1, 0.2) * 100)
                        If cachedPeakYear(key) = 2100 Then cachedPeakYear(key) = 2010
                        'Should read hivpeakdef from global common
                    Next

                End If


                ' mark pre-processed for all three series
                Me.processed = True
            End Sub

            <ExportsAsset("AIDSDTHStmp")>
            <ExportsAsset("HIVRATE")>
            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Me.PreProcess(cntx)
                Dim series As IDictionary(Of String, Single?)
                Select Case seriesName
                    Case "hivincr"
                        series = Me.cachedHIVINCR
                    Case "HIVRATE"
                        series = Me.cachedRate
                    Case "hivpeakr"
                        series = Me.cachedPeakRate
                    Case "hivpeakyr"

                        series = Me.cachedAIDSDTHS

                        Dim AIDSDTHS As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        AIDSDTHS = (From s In series
                                    Join r In cntx.Regionalization On s.Key Equals r.Key
                                    Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
                        ).ToDictionary(
                            Function(grp) grp.RegionId,
                            Function(grp) ApplyLimits(grp.TotalVal, 0, 1000)
                        )
                        cntx.Store("AIDSDTHStmp", AIDSDTHS)

                        series = Me.cachedPeakYear
                    Case "clnf"
                        series = Me.cachedCLNF
                    Case Else
                        Throw New ArgumentException($"Aggregator cannot be applied on series {seriesName}.")
                End Select

                If seriesName = "AIDSDTHS" Then
                    ' regionalize series
                    Return (From s In series
                            Join r In cntx.Regionalization On s.Key Equals r.Key
                            Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
            ).ToDictionary(
                Function(grp) grp.RegionId,
                Function(grp) ApplyLimits(grp.TotalVal, 0, 1000)
            )
                Else
                    If seriesName = "HIVRATE" Then
                        Dim HIVRATE As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        ' regionalize series
                        HIVRATE = (From s In series
                                   Join r In cntx.Regionalization On s.Key Equals r.Key
                                   Join w In Me.weightSeries On s.Key Equals w.Key
                                   Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
                Function(grp) grp.RegionId,
                Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 0, 100)
            )

                        cntx.Store("HIVRATE", HIVRATE)
                        Return HIVRATE
                    ElseIf seriesName = "hivpeakr" Then
                        ' regionalize series
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In Me.weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
                Function(grp) grp.RegionId,
                Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 0, 45)
            )
                    ElseIf seriesName = "hivpeakyr" Then
                        ' regionalize series
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In Me.weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
                Function(grp) grp.RegionId,
                Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 1990, 3000)
            )
                    Else 'CLNF, hivincr
                        ' regionalize series
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In Me.weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
                Function(grp) grp.RegionId,
                Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
            )
                    End If
                End If
            End Function

            Public Overrides Function Normalize(seriesName As String, cntx As IRebuildFacade) As IDictionary(Of String, Single?)
                ' declare as static to avoid re-calculation
                Static Dim CHIVRATE As IDictionary(Of String, Single?) = NormalizeHIVRATE(cntx)
                Static Dim CHIVPEAKR As IDictionary(Of String, Single?) = NormalizeHIVPEAKR(cntx, CHIVRATE)
                Select Case seriesName
                    Case "HIVRATE"
                        ' patch HIVRATE to not exceed HIVPEAKR
                        Return (From r In CHIVRATE
                                Join pr In CHIVPEAKR On r.Key Equals pr.Key
                                Select Key = r.Key, Value = If(r.Value > pr.Value, pr.Value, r.Value)
                ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
                    Case "hivpeakr"
                        Return CHIVPEAKR
                    Case Else
                        Throw New ArgumentException($"Aggregator cannot be applied on series {seriesName}.")
                End Select
            End Function

            Private Function NormalizeHIVRATE(cntx As IRebuildFacade) As IDictionary(Of String, Single?)
                If cntx.HistoricMode Then
                    Return cntx.Regionalization.ToDictionary(Function(s) s.Key, Function(s) CType(0.0F, Single?))
                Else
                    Return (From s In cntx.GetSlice("HIVRATE", cntx.FirstYear, DefaultPolicy)
                            Join s1 In cntx.GetSlice("HIVRATEHigh", cntx.FirstYear, DefaultPolicy) On s.Key Equals s1.Key
                            Join s2 In cntx.GetSlice("HIVRATELow", cntx.FirstYear, DefaultPolicy) On s.Key Equals s2.Key
                            Join w In CPOP On s.Key Equals w.Key
                            Select Key = s.Key, Value = If(If(s.Value, (s1.Value + s2.Value) / 2), w.Value * CSng(0.0001)) * 100 / w.Value
                ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
                End If
            End Function

            Private Function NormalizeHIVPEAKR(cntx As IRebuildFacade, CHIVRATE As IDictionary(Of String, Single?)) As IDictionary(Of String, Single?)
                If cntx.HistoricMode Then
                    Return cntx.GetSlice("hivpeakr", "PeakPrevalence") _
                    .ToDictionary(Function(s) s.Key, Function(s) CType(0, Single?))
                Else
                    Return cntx.GetSlice("hivpeakr", "PeakPrevalence") _
                    .ToDictionary(Function(s) s.Key, Function(s) If(s.Value, CHIVRATE(s.Key)))
                End If
            End Function

        End Class

        Class HIVPeakYearAggregator
            Inherits PopWeightedAverageAggregator

            Public Overrides Function Normalize(seriesName As String, cntx As IRebuildFacade) As IDictionary(Of String, Single?)
                ' process HIVPEAKYR
                'Dim EstimatePeakR As SeriesEstimator = cntx.GetEstimator("hivpeakr")
                'Dim EstimatePeakYear As SeriesEstimator = cntx.GetEstimator("hivpeakyr")
                If cntx.HistoricMode Then
                    Return cntx.GetSlice("hivpeakyr", "PeakYear") _
                    .ToDictionary(Function(s) s.Key, Function(s) CType(1960, Single?))
                Else
                    Return cntx.GetSlice("hivpeakyr", "PeakYear") _
                    .ToDictionary(Function(s) s.Key, Function(s) CType(2100, Single?))
                End If
            End Function

        End Class

        Class INFMORTAggregator
            Inherits PopWeightedAverageAggregator

            Public Overrides Function Normalize(seriesName As String, cntx As IRebuildFacade) As IDictionary(Of String, Single?)
                Dim xytabl As SeriesEstimator = cntx.GetEstimator(seriesName)
                Return cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy).ToDictionary(
                Function(s) s.Key, Function(s) ApplyBounds(If(s.Value, xytabl(s.Key)), 1, 400))
            End Function

        End Class

        Class MORTAggregator
            Inherits PopWeightedAverageAggregator
            Private Property CINFMOR As IDictionary(Of String, Single?)
            Private Property CNEONATMOR As IDictionary(Of String, Single?)

            Private Property processed As Boolean

            Private Sub PreProcess(cntx As IRebuildFacade)
                If Me.processed Then
                    Return
                End If
                Me.CINFMOR = New Dictionary(Of String, Single?)
                Me.CNEONATMOR = New Dictionary(Of String, Single?)
                Dim ValHold As Single?

                Dim xytabl As SeriesEstimator = cntx.GetEstimator("INFMOR")

                'jrs 2019/10/31 INFMOR is used in the new initialization of mortality by age, Hong Kong was negative
                'Added AMAX(CINFMOR, 1)
                If cntx.HistoricMode Then
                    'JAM 2019-03-07 Commented due mti improved
                    'For Each item In (From s In cntx.GetSlice("INFMOR", "Earliest")
                    '                  Select Key = s.Key, Value = s.Value)
                    '    CINFMOR.Add(item.Key, If(item.Value, xytabl(item.Key)))
                    'Next

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs2 As Short() = cntx.GetYears("INFMOR").OrderBy(Function(y) y).ToArray()
                    Dim INFMORTbl As IDictionary(Of String, Single?()) = cntx.GetDice("INFMOR", yrs2)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(INFMORTbl, yrs2, key, cntx.FirstYear, HistDist)
                        CINFMOR.Add(key, AMAX(If(ValHold, xytabl(key)), 1))

                    Next
                    'mti 2019/04/29  Flex Hist Base End


                Else
                    For Each item In (From s In cntx.GetSlice("INFMOR", cntx.FirstYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = s.Value)
                        CINFMOR.Add(item.Key, AMAX(If(item.Value, xytabl(item.Key)), 1))
                    Next
                End If

                If cntx.HistoricMode Then
                    xytabl = cntx.GetRegression("Infant Mortality Rate Versus Neonatal Mortality Rate (1960) Linear").ToEstimator(CINFMOR)

                    'JAM 2019-07-04 Commented due mti improved
                    'For Each item In (From s In cntx.GetSlice("NEONATMOR", "Earliest")
                    '                  Select Key = s.Key, Value = s.Value)
                    '    CNEONATMOR.Add(item.Key, If(item.Value, xytabl(item.Key)))
                    'Next

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs3 As Short() = cntx.GetYears("NEONATMOR").OrderBy(Function(y) y).ToArray()
                    Dim NEONATMORTbl As IDictionary(Of String, Single?()) = cntx.GetDice("NEONATMOR", yrs3)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(NEONATMORTbl, yrs3, key, cntx.FirstYear, HistDist)
                        CNEONATMOR.Add(key, If(ValHold, xytabl(key)))

                    Next
                    'mti 2019/04/29  Flex Hist Base End


                Else
                    Dim TbName As String
                    TbName$ = "Infant Mortality Rate Versus Neonatal Mortality Rate (" & CStr(cntx.BaseYear) & ") Linear" 'both in deaths per 1000 live births
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                    xytabl = cntx.GetRegression(TbName).ToEstimator(CINFMOR)

                    For Each item In (From s In cntx.GetSlice("NEONATMOR", cntx.FirstYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = s.Value)
                        CNEONATMOR.Add(item.Key, If(item.Value, xytabl(item.Key)))
                    Next
                End If

                For Each key In cntx.Regionalization.Keys
                    CNEONATMOR(key) = Amin(0.9F * CINFMOR(key), AMAX(0.2F * CINFMOR(key), CNEONATMOR(key)))
                Next

                Me.processed = True
            End Sub

            <ExportsAsset("INFMOR")>
            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Me.PreProcess(cntx)

                Dim series As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim weightSeries As IDictionary(Of String, Single?)
                weightSeries = CPOP
                Dim MinVal As Single, MaxVal As Single, bLimits As Boolean = False
                Dim bSum As Boolean = False
                Select Case seriesName
                    Case "INFMOR"
                        bLimits = True
                        MinVal = 1
                        MaxVal = 400
                        series = CINFMOR
                    Case "NEONATMOR"
                        bLimits = True
                        MinVal = 1
                        MaxVal = 400
                        series = CNEONATMOR
                    Case Else
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, Nothing)
                        Next
                End Select

                If bLimits Then
                    If bSum Then
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) ApplyLimits(grp.TotalVal, MinVal, MaxVal)
                           )
                    Else
                        Dim res As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        res = (From s In series
                               Join r In cntx.Regionalization On s.Key Equals r.Key
                               Join w In weightSeries On s.Key Equals w.Key
                               Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, MinVal, MaxVal)
                           )

                        If seriesName = "INFMOR" Then
                            cntx.Store("INFMOR", res)
                        End If

                        Return res
                    End If
                Else
                    If bSum Then
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) grp.TotalVal
                           )
                    Else
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
                           )
                    End If
                End If

            End Function

        End Class

        Class LITAggregator
            Inherits PopWeightedAverageAggregator

            'Pop is used as weight for the average
            Private Property weightSeries As IDictionary(Of String, Single?)
            Private Property CLIT As IDictionary(Of String, Single?)

            Private Function EstimateWith2Years(seriesName As String, cntx As IRebuildFacade) As IDictionary(Of String, Single?)
                Dim Yr2 As Short, Yr1 As Short
                Yr2 = 1990
                Yr1 = 1970

                Return (From s0 In cntx.GetSlice(seriesName, cntx.FirstYear)
                        Join s1 In cntx.GetSlice(seriesName, Yr1) On s0.Key Equals s1.Key
                        Join s2 In cntx.GetSlice(seriesName, Yr2) On s1.Key Equals s2.Key
                        Select Key = s1.Key, Value = (s1.Value + (s2.Value - s1.Value) / 20 * (cntx.FirstYear - Yr1))
                ).ToDictionary(Function(s) s.Key, Function(s) s.Value)

            End Function

            <ExportsAsset("CLIT")>
            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))

                Me.weightSeries = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")

                Me.CLIT = New Dictionary(Of String, Single?)

                Dim Estimate As SeriesEstimator = cntx.GetEstimator(seriesName)

                Dim LIT As IDictionary(Of String, Single?)
                If cntx.HistoricMode Then
                    LIT = (From s In EstimateWith2Years("ILIT", cntx)
                           Select Key = s.Key, Value = AMAX((100 - If(s.Value, Estimate(s.Key))), 5)
                ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
                Else
                    LIT = (From s In cntx.GetSlice("LIT", cntx.FirstYear, MyBase.Policy)
                           Select Key = s.Key, Value = If(s.Value, Estimate(s.Key))
                ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
                End If

                For Each key In cntx.Regionalization.Keys
                    CLIT.Add(key, LIT(key))
                Next

                cntx.Store("CLIT", CLIT)

                Dim series As IDictionary(Of String, Single?) = LIT

                Return (From s In series
                        Join r In cntx.Regionalization On s.Key Equals r.Key
                        Join w In Me.weightSeries On s.Key Equals w.Key
                        Group Value = s.Value, Weight = w.Value
                    By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
                Function(grp) grp.RegionId,
                Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 0, 100)
            )
            End Function

        End Class

        Class LAPOPRaggregator
            Inherits PopWeightedAverageAggregator

            'Pop is used as weight for the average
            Private Property weightSeries As IDictionary(Of String, Single?)
            <ImportsAsset("CGDPPCP")>
            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))

                Me.weightSeries = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")

                Dim Estimate As SeriesEstimator = cntx.GetEstimator(seriesName)

                Dim LAPOPR As IDictionary(Of String, Single?)
                Dim ValHold As Single?
                Dim CGDPPCP As IDictionary(Of String, Single?) = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")


                If cntx.HistoricMode Then
                    LAPOPR = New Dictionary(Of String, Single?)
                    'Call xytabl("WaterWithdIndustrial(MOSTRECENT minus elec) (Linear) versus Man (Linear)", ValAdd(ICount%, 4),
                    '    Prediction)
                    Dim xytabl2 As SeriesEstimator = cntx.GetRegression("GDP/Capita Versus Labor Participation Rate").ToEstimator(CGDPPCP)



                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs6 As Short() = cntx.GetYears(seriesName).OrderBy(Function(y) y).ToArray()
                    Dim LAPOPRTbl As IDictionary(Of String, Single?()) = cntx.GetDice(seriesName, yrs6)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(LAPOPRTbl, yrs6, key, cntx.FirstYear, HistDist)
                        LAPOPR.Add(key, ValHold)


                        If Not LAPOPR(key).HasValue Then
                            LAPOPR(key) = CSng(xytabl2(key))
                        Else
                            LAPOPR(key) = LAPOPR(key) / Me.weightSeries(key)
                        End If


                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else
                    LAPOPR = (From s In cntx.GetSlice(seriesName, cntx.FirstYear, MyBase.Policy)
                              Join w In Me.weightSeries On s.Key Equals w.Key
                              Select Key = s.Key, Value = If(s.Value / w.Value, Estimate(s.Key))
                ).ToDictionary(Function(s) s.Key, Function(s) s.Value)


                End If

                'JAM 2019-07-13 Commented due mti improvement
                'LAPOPR = (From s In cntx.GetSlice(seriesName, cntx.FirstYear, MyBase.Policy)
                '          Join w In Me.weightSeries On s.Key Equals w.Key
                '          Select Key = s.Key, Value = If(s.Value / w.Value, Estimate(s.Key))
                ').ToDictionary(Function(s) s.Key, Function(s) s.Value)



                Dim series As IDictionary(Of String, Single?) = LAPOPR

                Return (From s In series
                        Join r In cntx.Regionalization On s.Key Equals r.Key
                        Join w In Me.weightSeries On s.Key Equals w.Key
                        Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
        ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 0.1, 0.8)
        )
            End Function

        End Class

        Class FEMSHRLABAggregator
            Inherits PopWeightedAverageAggregator

            'jrs 2020/04/13 needed limit after regionalization, so added Regionalize and removed Normalize
            'Public Overrides Function Normalize(seriesName As String, cntx As IRebuildFacade) As IDictionary(Of String, Single?)
            '    Dim xytabl As SeriesEstimator = cntx.GetEstimator(seriesName)
            '    Dim Valhold As Single?
            '    Dim FEMSHRLABSlice As IDictionary(Of String, Single?)
            '    If cntx.HistoricMode Then
            '        FEMSHRLABSlice = New Dictionary(Of String, Single?)
            '        'mti 2019/04/29  Flex Hist Base Start
            '        Dim yrs7 As Short() = cntx.GetYears(seriesName).OrderBy(Function(y) y).ToArray()
            '        Dim FEMSHRLABTbl As IDictionary(Of String, Single?()) = cntx.GetDice(seriesName, yrs7)
            '        For Each key In cntx.Regionalization.Keys
            '            Valhold = getClosestYrVal(FEMSHRLABTbl, yrs7, key, cntx.FirstYear, HistDist)
            '            FEMSHRLABSlice.Add(key, Valhold)

            '            If Not FEMSHRLABSlice(key).HasValue Then
            '                FEMSHRLABSlice(key) = xytabl(key)

            '            End If

            '        Next

            '        Return FEMSHRLABSlice
            '    Else
            '        Return cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy).ToDictionary(
            '        Function(s) s.Key, Function(s) If(s.Value, xytabl(s.Key)))
            '    End If
            '    'JAM 2019-07-13 Commented due mti improvement
            '    'Return cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy).ToDictionary(
            '    'Function(s) s.Key, Function(s) If(s.Value, xytabl(s.Key)))
            'End Function

            'Pop is used as weight for the average
            Private Property weightSeries As IDictionary(Of String, Single?)
            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))

                Me.weightSeries = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")

                Dim xytabl As SeriesEstimator = cntx.GetEstimator(seriesName)
                Dim Valhold As Single?
                Dim FEMSHRLABSlice As IDictionary(Of String, Single?)
                If cntx.HistoricMode Then
                    FEMSHRLABSlice = New Dictionary(Of String, Single?)
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs7 As Short() = cntx.GetYears(seriesName).OrderBy(Function(y) y).ToArray()
                    Dim FEMSHRLABTbl As IDictionary(Of String, Single?()) = cntx.GetDice(seriesName, yrs7)
                    For Each key In cntx.Regionalization.Keys
                        Valhold = getClosestYrVal(FEMSHRLABTbl, yrs7, key, cntx.FirstYear, HistDist)
                        FEMSHRLABSlice.Add(key, Valhold)

                        If Not FEMSHRLABSlice(key).HasValue Then
                            FEMSHRLABSlice(key) = xytabl(key)

                        End If

                    Next

                Else
                    FEMSHRLABSlice = cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy).ToDictionary(
                    Function(s) s.Key, Function(s) If(s.Value, xytabl(s.Key)))
                End If


                Dim series As IDictionary(Of String, Single?) = FEMSHRLABSlice

                Return (From s In series
                        Join r In cntx.Regionalization On s.Key Equals r.Key
                        Join w In Me.weightSeries On s.Key Equals w.Key
                        Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
        ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 5, 80)
        )
            End Function
        End Class

        Class LABWAGESEXRATAggregator
            Inherits PopWeightedAverageAggregator


            'Pop is used as weight for the average
            Private Property weightSeries As IDictionary(Of String, Single?)

            <ExportsAsset("LABINFORMSHR")>
            <ExportsAsset("CLABINFORMSHRFromData")>
            Public Overrides Function Normalize(seriesName As String, cntx As IRebuildFacade) As IDictionary(Of String, Single?)
                Dim xytabl As SeriesEstimator = cntx.GetEstimator(seriesName)
                Dim ValHold As Single?

                'JAM 2019-08-01 Calling LABINFORMSHR for use in Economy.Rebuild
                Call RegionalizeLABINFORMSHR("LABINFORMSHR", cntx)

                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim LABWAGESEXRATSlice As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                    Dim yrs10 As Short() = cntx.GetYears(seriesName).OrderBy(Function(y) y).ToArray()
                    Dim LABWAGESEXRATTbl As IDictionary(Of String, Single?()) = cntx.GetDice(seriesName, yrs10)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(LABWAGESEXRATTbl, yrs10, key, cntx.FirstYear, HistDist)
                        LABWAGESEXRATSlice.Add(key, ApplyLimits(ValHold, 0.1, 1.5))


                        If Not LABWAGESEXRATSlice(key).HasValue Then
                            LABWAGESEXRATSlice(key) = xytabl(key)

                        End If

                    Next

                    Return LABWAGESEXRATSlice
                Else
                    Return cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy).ToDictionary(
                    Function(s) s.Key, Function(s) ApplyLimits(If(s.Value, xytabl(s.Key)), 0.1, 1.5))
                End If

                'JAM 2019-07-14 Commented due mti improvement
                'Return cntx.GetSlice(seriesName, cntx.FirstYear, DefaultPolicy).ToDictionary(
                'Function(s) s.Key, Function(s) If(s.Value, xytabl(s.Key)))
            End Function

            Public Sub RegionalizeLABINFORMSHR(seriesName As String, cntx As IRebuildFacade) 'As IEnumerable(Of KeyValuePair(Of Integer, Single?))


                weightSeries = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
                Dim CGDPPCP As IDictionary(Of String, Single?) = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")

                'jrs 2024/06/13
                Dim TblName As String = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Informal Employment Share of Total Logit"
                TblName = getFunctionNameYrOrMstRct(TblName, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                'Dim Estimate As SeriesEstimator = cntx.GetRegression("GDP/Capita (PPP 2011) Versus Informal Employment Share of Total Logit").ToEstimator(CGDPPCP)
                Dim Estimate As SeriesEstimator = cntx.GetRegression(TblName).ToEstimator(CGDPPCP)
                Dim ValHold As Single?

                Dim LABINFORMSHR As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CLABINFORMSHRFromData As IDictionary(Of String, Boolean) = New Dictionary(Of String, Boolean)
                If cntx.HistoricMode Then

                    'JAM 2019-07-29 Commented due mti improvement
                    '        LABINFORMSHR = (From s In cntx.GetSlice("LABINFORMSHR", cntx.FirstYear, MyBase.Policy)
                    '                        Select Key = s.Key, Value = If(s.Value, Estimate(s.Key))
                    ').ToDictionary(Function(s) s.Key, Function(s) s.Value)

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs14 As Short() = cntx.GetYears("LABINFORMSHR").OrderBy(Function(y) y).ToArray()
                    Dim LABINFORMSHRTbl As IDictionary(Of String, Single?()) = cntx.GetDice("LABINFORMSHR", yrs14)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(LABINFORMSHRTbl, yrs14, key, cntx.FirstYear, HistDist)
                        LABINFORMSHR.Add(key, If(ValHold, Estimate(key)))
                        CLABINFORMSHRFromData.Add(key, ValHold.HasValue)
                    Next
                    'mti 2019/04/29  Flex Hist Base End


                Else
                    For Each item In (From s In cntx.GetSlice("LABINFORMSHR", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("SeriesLaborInformalTotalAllWorldBank", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join X In LabInfShrNAgAll(cntx) On s.Key Equals X.Key
                                      Select Key = s.Key, Value = If(s.Value, s2.Value), Value2 = X.Value)
                        If item.Value.HasValue Then
                            LABINFORMSHR.Add(item.Key, item.Value)
                            CLABINFORMSHRFromData.Add(item.Key, True)
                        Else
                            LABINFORMSHR.Add(item.Key, If(item.Value2, Estimate(item.Key)))
                            CLABINFORMSHRFromData.Add(item.Key, False)
                        End If
                    Next
                End If

                'Dim series As IDictionary(Of String, Single?) = LABINFORMSHR

                '        Return (From s In series
                '                Join r In cntx.Regionalization On s.Key Equals r.Key
                '                Join w In Me.weightSeries On s.Key Equals w.Key
                '                Group Value = s.Value, Weight = w.Value
                '        By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                ').ToDictionary(
                '    Function(grp) grp.RegionId,
                '    Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
                ')

                cntx.Store("LABINFORMSHR", LABINFORMSHR)
                cntx.Store("CLABINFORMSHRFromData", CLABINFORMSHRFromData)
            End Sub
            Private Function LabInfShrNAgAll(cntx As IRebuildFacade) As IDictionary(Of String, Single?)
                'If LabNAgFem + LabNAgMal <> 0 Then
                '    LabInfShrNAgAll = 100 * (LabInfFem + LabInfMal) / (LabNAgFem + LabNAgMal)
                'End If
                Dim LAPOPR As IDictionary(Of String, Single?) = cntx.GetSlice("LAPOPR", cntx.FirstYear, MyBase.Policy)
                Dim LabAgrPcntTotFem As IDictionary(Of String, Single?) = cntx.GetSlice("LabAgrPcntTotFem", cntx.FirstYear, MyBase.Policy)
                Dim LabAgrPcntTotMale As IDictionary(Of String, Single?) = cntx.GetSlice("LabAgrPcntTotMale", cntx.FirstYear, MyBase.Policy)
                Dim FEMSHRLAB As IDictionary(Of String, Single?) = cntx.GetSlice("FEMSHRLAB", cntx.FirstYear, MyBase.Policy)

                Return (From s0 In LabInfFem(cntx, LAPOPR, LabAgrPcntTotFem, FEMSHRLAB)
                        Join s1 In LabInfMal(cntx, LAPOPR, LabAgrPcntTotMale, FEMSHRLAB) On s0.Key Equals s1.Key
                        Join s2 In LabNAgFem(cntx, LAPOPR, LabAgrPcntTotFem, FEMSHRLAB) On s1.Key Equals s2.Key
                        Join s3 In LabNAgMal(cntx, LAPOPR, LabAgrPcntTotMale, FEMSHRLAB) On s2.Key Equals s3.Key
                        Select Key = s1.Key, Value = 100 * (s0.Value + s1.Value) / (s2.Value + s3.Value)
            ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
            End Function

            Private Function LabInfFem(cntx As IRebuildFacade, LAPOPR As IDictionary(Of String, Single?),
                               LabAgrPcntTotFem As IDictionary(Of String, Single?),
                               FEMSHRLAB As IDictionary(Of String, Single?)) As IDictionary(Of String, Single?)
                'LabInfFem = LabInfPcntNAgFem * LabNAgFem / 100
                Return (From s0 In cntx.GetSlice("LabInfPcntNAgFem", cntx.FirstYear, MyBase.Policy)
                        Join s1 In LabNAgFem(cntx, LAPOPR, LabAgrPcntTotFem, FEMSHRLAB) On s0.Key Equals s1.Key
                        Select Key = s1.Key, Value = (s0.Value * s1.Value) / 100
            ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
            End Function

            Private Function LabInfMal(cntx As IRebuildFacade, LAPOPR As IDictionary(Of String, Single?),
                               LabAgrPcntTotFem As IDictionary(Of String, Single?),
                               FEMSHRLAB As IDictionary(Of String, Single?)) As IDictionary(Of String, Single?)
                'LabInfMal = LabInfPcntNAgMal * LabNAgMal / 100
                Return (From s0 In cntx.GetSlice("LabInfPcntNAgMale", cntx.FirstYear, MyBase.Policy)
                        Join s1 In LabNAgMal(cntx, LAPOPR, LabAgrPcntTotFem, FEMSHRLAB) On s0.Key Equals s1.Key
                        Select Key = s1.Key, Value = (s0.Value * s1.Value) / 100
            ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
            End Function

            Private Function LabNAgFem(cntx As IRebuildFacade, LAPOPR As IDictionary(Of String, Single?),
                               LabAgrPcntTotFem As IDictionary(Of String, Single?),
                               FEMSHRLAB As IDictionary(Of String, Single?)) As IDictionary(Of String, Single?)
                'LabNAgFem = (100 - LabAgrPcntTotFem) * LabTotFem / 100
                Return (From s0 In LabAgrPcntTotFem
                        Join s1 In LabTotFem(cntx, LAPOPR, FEMSHRLAB) On s0.Key Equals s1.Key
                        Select Key = s1.Key, Value = (100 - s0.Value) * s1.Value / 100
            ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
            End Function

            Private Function LabNAgMal(cntx As IRebuildFacade, LAPOPR As IDictionary(Of String, Single?),
                               LabAgrPcntTotMale As IDictionary(Of String, Single?),
                               FEMSHRLAB As IDictionary(Of String, Single?)) As IDictionary(Of String, Single?)
                'LabNAgMal = (100 - LabAgrPcntTotMal) * LabTotMal / 100
                Return (From s0 In LabAgrPcntTotMale
                        Join s1 In LabTotMal(cntx, LAPOPR, FEMSHRLAB) On s0.Key Equals s1.Key
                        Select Key = s1.Key, Value = (100 - s0.Value) * s1.Value / 100
            ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
            End Function

            Private Function LabTotFem(cntx As IRebuildFacade, LAPOPR As IDictionary(Of String, Single?),
                               FEMSHRLAB As IDictionary(Of String, Single?)) As IDictionary(Of String, Single?)
                'LabTotFem = LabTotAll * LabTotShrFem / 100
                Return (From s0 In LAPOPR
                        Join s1 In FEMSHRLAB On s0.Key Equals s1.Key
                        Select Key = s1.Key, Value = (s0.Value * s1.Value) / 100
            ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
            End Function

            Private Function LabTotMal(cntx As IRebuildFacade, LAPOPR As IDictionary(Of String, Single?),
                               FEMSHRLAB As IDictionary(Of String, Single?)) As IDictionary(Of String, Single?)
                'LabTotMal = LabTotAll - LabTotFem
                Return (From s0 In LAPOPR
                        Join s1 In LabTotFem(cntx, LAPOPR, FEMSHRLAB) On s0.Key Equals s1.Key
                        Select Key = s1.Key, Value = (s0.Value - s1.Value)
            ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
            End Function


        End Class
        ''' <summary>
        ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' 
        '''    JAM 2019-07-29 Decommented due mti improvement
        ''' 
        ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' </summary>
        'Public Class LABINFORMSHRAggregator
        '    Inherits PopWeightedAverageAggregator

        '    'Pop is used as weight for the average
        '    Private Property weightSeries As IDictionary(Of String, Single?)

        '    Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))


        '        weightSeries = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")

        '        Dim Estimate As SeriesEstimator = cntx.GetEstimator(seriesName)
        '        Dim ValHold As Single?

        '        Dim LABINFORMSHR As IDictionary(Of String, Single?)
        '        If cntx.HistoricMode Then

        '            'JAM 2019-07-29 Commented due mti improvement
        '            '        LABINFORMSHR = (From s In cntx.GetSlice("LABINFORMSHR", cntx.FirstYear, MyBase.Policy)
        '            '                        Select Key = s.Key, Value = If(s.Value, Estimate(s.Key))
        '            ').ToDictionary(Function(s) s.Key, Function(s) s.Value)

        '            'mti 2019/04/29  Flex Hist Base Start
        '            Dim yrs14 As Short() = cntx.GetYears("LABINFORMSHR").OrderBy(Function(y) y).ToArray()
        '            Dim LABINFORMSHRTbl As IDictionary(Of String, Single?()) = cntx.GetDice("LABINFORMSHR", yrs14)
        '            For Each key In cntx.Regionalization.Keys
        '                ValHold = getClosestYrVal(LABINFORMSHRTbl, yrs14, key, cntx.FirstYear, HistDist)
        '                LABINFORMSHR.Add(key, If(ValHold, Estimate(key)))

        '            Next
        '            'mti 2019/04/29  Flex Hist Base End


        '        Else
        '            LABINFORMSHR = (From s In cntx.GetSlice("LABINFORMSHR", cntx.FirstYear, MyBase.Policy)
        '                            Join x In LabInfShrNAgAll(cntx) On s.Key Equals x.Key
        '                            Select Key = s.Key, Value = If(If(s.Value, x.Value), Estimate(s.Key))
        '    ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
        '        End If

        '        Dim series As IDictionary(Of String, Single?) = LABINFORMSHR

        '        Return (From s In series
        '                Join r In cntx.Regionalization On s.Key Equals r.Key
        '                Join w In Me.weightSeries On s.Key Equals w.Key
        '                Group Value = s.Value, Weight = w.Value
        '        By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
        ').ToDictionary(
        '    Function(grp) grp.RegionId,
        '    Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
        ')
        '    End Function

        '    Private Function LabInfShrNAgAll(cntx As IRebuildFacade) As IDictionary(Of String, Single?)
        '        'If LabNAgFem + LabNAgMal <> 0 Then
        '        '    LabInfShrNAgAll = 100 * (LabInfFem + LabInfMal) / (LabNAgFem + LabNAgMal)
        '        'End If
        '        Dim LAPOPR As IDictionary(Of String, Single?) = cntx.GetSlice("LAPOPR", cntx.FirstYear, MyBase.Policy)
        '        Dim LabAgrPcntTotFem As IDictionary(Of String, Single?) = cntx.GetSlice("LabAgrPcntTotFem", cntx.FirstYear, MyBase.Policy)
        '        Dim LabAgrPcntTotMale As IDictionary(Of String, Single?) = cntx.GetSlice("LabAgrPcntTotMale", cntx.FirstYear, MyBase.Policy)
        '        Dim FEMSHRLAB As IDictionary(Of String, Single?) = cntx.GetSlice("FEMSHRLAB", cntx.FirstYear, MyBase.Policy)

        '        Return (From s0 In LabInfFem(cntx, LAPOPR, LabAgrPcntTotFem, FEMSHRLAB)
        '                Join s1 In LabInfMal(cntx, LAPOPR, LabAgrPcntTotMale, FEMSHRLAB) On s0.Key Equals s1.Key
        '                Join s2 In LabNAgFem(cntx, LAPOPR, LabAgrPcntTotFem, FEMSHRLAB) On s1.Key Equals s2.Key
        '                Join s3 In LabNAgMal(cntx, LAPOPR, LabAgrPcntTotMale, FEMSHRLAB) On s2.Key Equals s3.Key
        '                Select Key = s1.Key, Value = 100 * (s0.Value + s1.Value) / (s2.Value + s3.Value)
        '    ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
        '    End Function

        '    Private Function LabInfFem(cntx As IRebuildFacade, LAPOPR As IDictionary(Of String, Single?),
        '                       LabAgrPcntTotFem As IDictionary(Of String, Single?),
        '                       FEMSHRLAB As IDictionary(Of String, Single?)) As IDictionary(Of String, Single?)
        '        'LabInfFem = LabInfPcntNAgFem * LabNAgFem / 100
        '        Return (From s0 In cntx.GetSlice("LabInfPcntNAgFem", cntx.FirstYear, MyBase.Policy)
        '                Join s1 In LabNAgFem(cntx, LAPOPR, LabAgrPcntTotFem, FEMSHRLAB) On s0.Key Equals s1.Key
        '                Select Key = s1.Key, Value = (s0.Value * s1.Value) / 100
        '    ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
        '    End Function

        '    Private Function LabInfMal(cntx As IRebuildFacade, LAPOPR As IDictionary(Of String, Single?),
        '                       LabAgrPcntTotFem As IDictionary(Of String, Single?),
        '                       FEMSHRLAB As IDictionary(Of String, Single?)) As IDictionary(Of String, Single?)
        '        'LabInfMal = LabInfPcntNAgMal * LabNAgMal / 100
        '        Return (From s0 In cntx.GetSlice("LabInfPcntNAgMale", cntx.FirstYear, MyBase.Policy)
        '                Join s1 In LabNAgMal(cntx, LAPOPR, LabAgrPcntTotFem, FEMSHRLAB) On s0.Key Equals s1.Key
        '                Select Key = s1.Key, Value = (s0.Value * s1.Value) / 100
        '    ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
        '    End Function

        '    Private Function LabNAgFem(cntx As IRebuildFacade, LAPOPR As IDictionary(Of String, Single?),
        '                       LabAgrPcntTotFem As IDictionary(Of String, Single?),
        '                       FEMSHRLAB As IDictionary(Of String, Single?)) As IDictionary(Of String, Single?)
        '        'LabNAgFem = (100 - LabAgrPcntTotFem) * LabTotFem / 100
        '        Return (From s0 In LabAgrPcntTotFem
        '                Join s1 In LabTotFem(cntx, LAPOPR, FEMSHRLAB) On s0.Key Equals s1.Key
        '                Select Key = s1.Key, Value = (100 - s0.Value) * s1.Value / 100
        '    ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
        '    End Function

        '    Private Function LabNAgMal(cntx As IRebuildFacade, LAPOPR As IDictionary(Of String, Single?),
        '                       LabAgrPcntTotMale As IDictionary(Of String, Single?),
        '                       FEMSHRLAB As IDictionary(Of String, Single?)) As IDictionary(Of String, Single?)
        '        'LabNAgMal = (100 - LabAgrPcntTotMal) * LabTotMal / 100
        '        Return (From s0 In LabAgrPcntTotMale
        '                Join s1 In LabTotMal(cntx, LAPOPR, FEMSHRLAB) On s0.Key Equals s1.Key
        '                Select Key = s1.Key, Value = (100 - s0.Value) * s1.Value / 100
        '    ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
        '    End Function

        '    Private Function LabTotFem(cntx As IRebuildFacade, LAPOPR As IDictionary(Of String, Single?),
        '                       FEMSHRLAB As IDictionary(Of String, Single?)) As IDictionary(Of String, Single?)
        '        'LabTotFem = LabTotAll * LabTotShrFem / 100
        '        Return (From s0 In LAPOPR
        '                Join s1 In FEMSHRLAB On s0.Key Equals s1.Key
        '                Select Key = s1.Key, Value = (s0.Value * s1.Value) / 100
        '    ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
        '    End Function

        '    Private Function LabTotMal(cntx As IRebuildFacade, LAPOPR As IDictionary(Of String, Single?),
        '                       FEMSHRLAB As IDictionary(Of String, Single?)) As IDictionary(Of String, Single?)
        '        'LabTotMal = LabTotAll - LabTotFem
        '        Return (From s0 In LAPOPR
        '                Join s1 In LabTotFem(cntx, LAPOPR, FEMSHRLAB) On s0.Key Equals s1.Key
        '                Select Key = s1.Key, Value = (s0.Value - s1.Value)
        '    ).ToDictionary(Function(s) s.Key, Function(s) s.Value)
        '    End Function
        'End Class
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Class HHSIZEAggregator
            Inherits PopWeightedAverageAggregator

            'Pop is used as weight for the average
            Private Property CPOP As IDictionary(Of String, Single?)
            Private Property CHHSIZE As IDictionary(Of String, Single?)

            <ExportsAsset("CHHSIZE")>
            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Me.CPOP = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
                CHHSIZE = New Dictionary(Of String, Single?)

                Dim Estimate As SeriesEstimator = cntx.GetEstimator(seriesName)

                Dim EstimateHHIntensity As SeriesEstimator

                If cntx.HistoricMode Then
                    Dim CHHTot As Single?
                    Dim CPopTot As Single?

                    Dim years As Short() = cntx.GetYears("SeriesHouseholds").OrderBy(Function(yy) yy).ToArray()
                    Dim hhtottbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesHouseholds", years)

                    Dim yearsP As Short() = cntx.GetYears("POP").OrderBy(Function(yy) yy).ToArray()
                    Dim PopTotTbl As IDictionary(Of String, Single?()) = cntx.GetDice("POP", yearsP)

                    Dim CAgeRatio As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                    Dim ValHold As Single?
                    Dim CHHTotArray As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)

                    'If cntx.HistoricMode Then

                    'JAM 2019-07-18
                    For Each item In (From s In cntx.GetSlice("SeriesHouseholds", cntx.FirstYear, CoalescePolicy.Null)
                                      Select Key = s.Key, Value = s.Value)
                        CHHTotArray.Add(item.Key, item.Value)
                    Next

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs13 As Short() = cntx.GetYears("AgeRatio").OrderBy(Function(y) y).ToArray()
                    Dim AgeRatioTbl As IDictionary(Of String, Single?()) = cntx.GetDice("AgeRatio", yrs13)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(AgeRatioTbl, yrs13, key, cntx.FirstYear, HistDist)


                        CHHTot = CHHTotArray(key)
                        CPopTot = CPOP(key)
                        If Not CHHTot.HasValue Or Not CPopTot.HasValue Then
                            Dim hhyr As Integer, yrIdx As Integer
                            For hhyr = 1 To 10
                                If Not CHHTot.HasValue Or Not CPopTot.HasValue Then
                                    For yrIdx = 0 To years.Count - 1
                                        If years(yrIdx) = cntx.FirstYear + hhyr Then
                                            CHHTot = hhtottbl(key)(yrIdx)
                                            Exit For
                                        End If
                                    Next
                                    For yrIdx = 0 To yearsP.Count - 1
                                        If yearsP(yrIdx) = cntx.FirstYear + hhyr Then
                                            CPopTot = PopTotTbl(key)(yrIdx)
                                            Exit For
                                        End If
                                    Next
                                Else
                                    Exit For
                                End If
                            Next
                        End If

                        If Not CHHTot.HasValue Or Not CPopTot.HasValue Then
                            '   CAgeRatio.Add(key, item.AgeRatio) 
                            CAgeRatio.Add(key, ValHold)

                        Else
                            CHHSIZE.Add(key, CPopTot / (AMAX(0.000001F, CHHTot)))
                        End If



                    Next
                    'mti 2019/04/29  Flex Hist Base End                        

                    'Else
                    '    For Each item In (From s In cntx.GetSlice(seriesName, cntx.FirstYear, CoalescePolicy.Null)
                    '                      Join s1 In cntx.GetSlice("AgeRatio", cntx.FirstYear, CoalescePolicy.Null) On s.Key Equals s1.Key
                    '                      Select Key = s.Key, Value = s.Value, AgeRatio = s1.Value)

                    '        CHHTot = item.Value
                    '        CPopTot = CPOP(item.Key)
                    '        If Not CHHTot.HasValue Or Not CPopTot.HasValue Then
                    '            Dim hhyr As Integer, yrIdx As Integer
                    '            For hhyr = 1 To 10
                    '                If Not CHHTot.HasValue Or Not CPopTot.HasValue Then
                    '                    For yrIdx = 0 To years.Count - 1
                    '                        If years(yrIdx) = cntx.FirstYear + hhyr Then
                    '                            CHHTot = hhtottbl(item.Key)(yrIdx)
                    '                            Exit For
                    '                        End If
                    '                    Next
                    '                    For yrIdx = 0 To yearsP.Count - 1
                    '                        If yearsP(yrIdx) = cntx.FirstYear + hhyr Then
                    '                            CPopTot = PopTotTbl(item.Key)(yrIdx)
                    '                            Exit For
                    '                        End If
                    '                    Next
                    '                Else
                    '                    Exit For
                    '                End If
                    '            Next
                    '        End If

                    '        If Not CHHTot.HasValue Or Not CPopTot.HasValue Then
                    '            CAgeRatio.Add(item.Key, item.AgeRatio)
                    '        Else
                    '            CHHSIZE.Add(item.Key, CPopTot / (AMAX(0.000001, CHHTot)))
                    '        End If

                    '    Next
                    'End If

                    'JAM 2019-07-14 Commented due mti improvement
                    'For Each item In (From s In cntx.GetSlice(seriesName, cntx.FirstYear, CoalescePolicy.Null)
                    '                  Join s1 In cntx.GetSlice("AgeRatio", cntx.FirstYear, CoalescePolicy.Null) On s.Key Equals s1.Key
                    '                  Select Key = s.Key, Value = s.Value, AgeRatio = s1.Value)

                    '    CHHTot = item.Value
                    '    CPopTot = CPOP(item.Key)
                    '    If Not CHHTot.HasValue Or Not CPopTot.HasValue Then
                    '        Dim hhyr As Integer, yrIdx As Integer
                    '        For hhyr = 1 To 10
                    '            If Not CHHTot.HasValue Or Not CPopTot.HasValue Then
                    '                For yrIdx = 0 To years.Count - 1
                    '                    If years(yrIdx) = cntx.FirstYear + hhyr Then
                    '                        CHHTot = hhtottbl(item.Key)(yrIdx)
                    '                        Exit For
                    '                    End If
                    '                Next
                    '                For yrIdx = 0 To yearsP.Count - 1
                    '                    If yearsP(yrIdx) = cntx.FirstYear + hhyr Then
                    '                        CPopTot = PopTotTbl(item.Key)(yrIdx)
                    '                        Exit For
                    '                    End If
                    '                Next
                    '            Else
                    '                Exit For
                    '            End If
                    '        Next
                    '    End If

                    '    If Not CHHTot.HasValue Or Not CPopTot.HasValue Then
                    '        CAgeRatio.Add(item.Key, item.AgeRatio)
                    '    Else
                    '        CHHSIZE.Add(item.Key, CPopTot / (AMAX(0.000001, CHHTot)))
                    '    End If

                    'Next

                    Dim CHHIntensity As Single?
                    EstimateHHIntensity = cntx.GetRegression("Population Age Ratio Versus Households Intensity (1960)").ToEstimator(CAgeRatio)
                    For Each key In cntx.Regionalization.Keys
                        If Not CHHSIZE.ContainsKey(key) Then
                            If CAgeRatio(key).HasValue Then
                                CHHIntensity = EstimateHHIntensity(key)
                            Else
                                CHHIntensity = Estimate(key)
                            End If
                            CHHSIZE.Add(key, 1 / (AMAX(0.000001F, CHHIntensity)))
                        End If
                        CHHSIZE(key) = ApplyLimits(CHHSIZE(key), 1, 10)
                    Next

                Else
                    EstimateHHIntensity = cntx.GetEstimator("HHSIZE_INTENSITY")

                    'Dim CHHSIZETMP As Dictionary(Of String, Single?)
                    Dim Val As Single?
                    For Each item In (From s In cntx.GetSlice("SeriesHouseholds", cntx.FirstYear, CoalescePolicy.MostRecent)
                                      Join w In Me.CPOP On s.Key Equals w.Key
                                      Select Key = s.Key, Value = s.Value, Pop = w.Value
                    )
                        If Not item.Value.HasValue Or Not item.Pop.HasValue Then
                            Val = EstimateHHIntensity(item.Key)
                            If Not Val.HasValue Then
                                Val = Estimate(item.Key)
                            End If
                            CHHSIZE.Add(item.Key, ApplyLimits(1 / AMAX(Val, 0.000001F), 1, 10))
                        Else
                            CHHSIZE.Add(item.Key, ApplyLimits(item.Pop / AMAX(item.Value, 0.000001F), 1, 10))
                        End If
                    Next

                    'CHHSIZETMP = (From s In cntx.GetSlice("SeriesHouseholds", cntx.FirstYear, CoalescePolicy.MostRecent)
                    '              Join w In Me.CPOP On s.Key Equals w.Key
                    '              Select Key = s.Key, Value = ApplyLimits(If(If(w.Value / AMAX(s.Value, 0.000001F),
                    '         1 / AMAX(EstimateHHIntensity(s.Key), 0.000001F)), 1 / AMAX(Estimate(s.Key), 0.000001F)), 1, 10)
                    ').ToDictionary(Function(s) s.Key, Function(s) s.Value)

                    'ByRef didn't like assigning CHHSIZE = (select)
                    'For Each key In cntx.Regionalization.Keys
                    '    CHHSIZE.Add(key, CHHSIZETMP(key))
                    'Next
                End If

                cntx.Store("CHHSIZE", CHHSIZE)

                Dim series As IDictionary(Of String, Single?) = CHHSIZE

                Return (From s In series
                        Join r In cntx.Regionalization On s.Key Equals r.Key
                        Join w In Me.CPOP On s.Key Equals w.Key
                        Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
        ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 1, 10)
        )
            End Function

        End Class

        Class POPURBANAggregator
            Inherits DefaultSumAggregator

            Private Property CPOP As IDictionary(Of String, Single?)

            Private Property CCBR As IDictionary(Of String, Single?)

            Private Property CCDR As IDictionary(Of String, Single?)

            Private Property CPOPURBAN As IDictionary(Of String, Single?)

            Private Property CPOPURBGR As IDictionary(Of String, Single?)

            Private Property cachedLABPARRMALE As IDictionary(Of String, Single?)

            Private Property cachedLABPARRFEMALE As IDictionary(Of String, Single?)

            Private Property cachedLABPARRTOT As IDictionary(Of String, Single?)

            Private Property CLAB As IDictionary(Of String, Single?)

            Private Property weightSeries As IDictionary(Of String, Single?)

            Private Property cachedMIGRANTSTOCKS As IDictionary(Of String, Single?)

            Private Property cachedMIGRATE As IDictionary(Of Integer, Single?)

            Private Property processed As Boolean

            'bbh 2020/03/09 start
            Dim CPOPFOREIGNIN, CPOPFOREIGNOUT, CMIGRANTSIN, CMIGRANTSOUT As IDictionary(Of String, Single?)
            'bbh 2020/03/09 end

            <ExportsAsset("CPOPURBAN")>
            <ExportsAsset("CPOPURBGR")>
            <ExportsAsset("CLAB")>
            <ExportsAsset("CCBR")>
            <ExportsAsset("CCDR")>
            <ExportsAsset("cagedstM")>
            <ExportsAsset("cagedstF")>
            <ExportsAsset("cfagedstM")>
            <ExportsAsset("cfagedstF")>
            <ExportsAsset("CPOPFOREIGN")>
            <ExportsAsset("CPOPFOREIGNIN")>
            <ExportsAsset("CPOPFOREIGNOUT")>
            <ExportsAsset("CMIGRANTSIN")>
            <ExportsAsset("CMIGRANTSOUT")>
            <ExportsAsset("MIGRATEIN")>
            <ExportsAsset("MIGRATEOUT")>
            Private Sub PreProcess(cntx As IRebuildFacade)
                If Me.processed Then
                    Return
                End If

                Me.CPOP = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
                Me.CCBR = New Dictionary(Of String, Single?)
                Me.CCDR = New Dictionary(Of String, Single?)

                CPOPURBAN = New Dictionary(Of String, Single?)
                CPOPURBGR = New Dictionary(Of String, Single?)

                Me.cachedLABPARRMALE = New Dictionary(Of String, Single?)
                Me.cachedLABPARRFEMALE = New Dictionary(Of String, Single?)
                Me.cachedLABPARRTOT = New Dictionary(Of String, Single?)
                Me.CLAB = New Dictionary(Of String, Single?)

                'bbh 2020/03/09 start
                CPOPFOREIGNIN = New Dictionary(Of String, Single?)
                CPOPFOREIGNOUT = New Dictionary(Of String, Single?)
                CMIGRANTSIN = New Dictionary(Of String, Single?)
                CMIGRANTSOUT = New Dictionary(Of String, Single?)
                'bbh 2020/03/09 end

                Me.processed = False

                'process CBR
                Dim EstimateCBR As SeriesEstimator = cntx.GetEstimator("CBR")
                Dim ValHold As Single?

                If cntx.HistoricMode Then
                    'JAM 2019-07-14 Commented due mti improvement
                    'For Each item In (From s In cntx.GetSlice("CBR", 1962, CoalescePolicy.Null)
                    '                  Select Key = s.Key, Value = AMAX(If(s.Value, EstimateCBR(s.Key)), 5))
                    '    Me.CCBR.Add(item.Key, item.Value)
                    'Next


                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs11 As Short() = cntx.GetYears("CBR").OrderBy(Function(y) y).ToArray()
                    Dim CBRTbl As IDictionary(Of String, Single?()) = cntx.GetDice("CBR", yrs11)
                    Dim yr62Idx As Short = getYearIdx(yrs11, 1962)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(CBRTbl, yrs11, key, cntx.FirstYear, HistDist)
                        Me.CCBR.Add(key, ValHold)
                        If key = "China" And cntx.FirstYear < 1962 Then
                            CCBR(key) = CBRTbl(key)(yr62Idx)
                        End If
                        'mti 2019/04/29  Flex Hist Base End
                        'Huge CDR data transient in China, use 1962 to stablize
                        If Not CCBR(key).HasValue Then
                            CCBR(key) = EstimateCBR(key)
                        End If
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else
                    For Each item In (From s In cntx.GetSlice("CBR", cntx.FirstYear, MyBase.Policy)
                                      Select Key = s.Key, Value = AMAX(If(s.Value, EstimateCBR(s.Key)), 5))

                        Me.CCBR.Add(item.Key, item.Value)
                    Next
                End If
                'process CDR
                Dim EstimateCDR As SeriesEstimator = cntx.GetEstimator("CDR")

                If cntx.HistoricMode Then
                    'JAM 2019-07-14 Commented due mti improvement
                    'For Each item In (From s In cntx.GetSlice("CDR", 1962, CoalescePolicy.Null)
                    '                  Select Key = s.Key, Value = If(s.Value, EstimateCDR(s.Key)))
                    '    Me.CCDR.Add(item.Key, item.Value)
                    'Next

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs12 As Short() = cntx.GetYears("CDR").OrderBy(Function(y) y).ToArray()
                    Dim yr62Idx As Short = getYearIdx(yrs12, 1962)
                    Dim CDRTbl As IDictionary(Of String, Single?()) = cntx.GetDice("CDR", yrs12)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(CDRTbl, yrs12, key, cntx.FirstYear, HistDist)
                        Me.CCDR.Add(key, ValHold)
                        If key = "China" And cntx.FirstYear < 1962 Then
                            CCDR(key) = CDRTbl(key)(yr62Idx)
                        End If
                        'mti 2019/04/29  Flex Hist Base End
                        'Huge CDR data transient in China, use 1962 to stablize
                        If Not CCDR(key).HasValue Then
                            CCDR(key) = EstimateCDR(key)
                        End If

                    Next
                    'mti 2019/04/29  Flex Hist Base End


                Else
                    For Each item In (From s In cntx.GetSlice("CDR", cntx.FirstYear, MyBase.Policy)
                                      Select Key = s.Key, Value = If(s.Value, EstimateCDR(s.Key)))
                        Me.CCDR.Add(item.Key, item.Value)
                    Next
                End If

                'process POPURBGR and POPURBAN
                Dim EstimatePOPURBAN As SeriesEstimator = cntx.GetEstimator("POPURBAN")

                If cntx.HistoricMode Then
                    Dim years As Short() = cntx.GetYears("POPURBAN").OrderBy(Function(yy) yy).ToArray()
                    Dim UrbanTbl As IDictionary(Of String, Single?()) = cntx.GetDice("POPURBAN", years)

                    For Each item In (From s1 In cntx.GetSlice("POPURBAN", 1960)
                                      Join s2 In cntx.GetSlice("POPURBAN", 1970) On s1.Key Equals s2.Key
                                      Select Key = s1.Key, CPOPURBAN1960 = s1.Value, CPOPURBAN1970 = s2.Value)
                        If Not item.CPOPURBAN1960.HasValue Or Not item.CPOPURBAN1970.HasValue Then
                            CPOPURBGR.Add(item.Key, (CCBR(item.Key) - CCDR(item.Key)) / 10 + 1)
                        Else
                            CPOPURBGR.Add(item.Key, CSng(((item.CPOPURBAN1970 / item.CPOPURBAN1960) ^ (1 / 10) - 1) * 100))
                        End If

                        CPOPURBAN.Add(item.Key, item.CPOPURBAN1960)
                        If Not CPOPURBAN(item.Key).HasValue Then
                            Dim hhyr As Integer, yrIdx As Integer
                            For hhyr = 1 To 10
                                If Not CPOPURBAN(item.Key).HasValue Then
                                    For yrIdx = 0 To years.Count - 1
                                        If years(yrIdx) = cntx.FirstYear + hhyr Then
                                            CPOPURBAN(item.Key) = UrbanTbl(item.Key)(yrIdx)
                                            Exit For
                                        End If
                                    Next
                                Else
                                    Exit For
                                End If
                            Next
                        End If

                        If Not CPOPURBAN(item.Key).HasValue Then
                            CPOPURBAN(item.Key) = EstimatePOPURBAN(item.Key)
                            CPOPURBAN(item.Key) = Amin(0.95F, CPOPURBAN(item.Key)) * CPOP(item.Key)
                        End If
                        CPOPURBAN(item.Key) = AMAX(CPOPURBAN(item.Key), CSng(0.01 * CPOP(item.Key)))
                        If CPOPURBAN(item.Key) / CPOP(item.Key) > 0.9 Then CPOPURBGR(item.Key) = Amin(2, CPOPURBGR(item.Key))
                        If item.CPOPURBAN1960.HasValue And CPOP(item.Key).HasValue Then
                            If item.CPOPURBAN1960 >= 0.995 * CPOP(item.Key) Then
                                CPOPURBAN(item.Key) = CPOP(item.Key)  'Singapore and Hong Kong Rule
                            End If
                        End If
                        CPOPURBAN(item.Key) = Amin(CPOP(item.Key), AMAX(CPOPURBAN(item.Key), CSng(0.01 * CPOP(item.Key))))
                    Next
                Else
                    For Each item In (From s0 In cntx.GetSlice("POPURBAN", cntx.FirstYear)
                                      Join s1 In cntx.GetSlice("POPURBAN", CShort(cntx.FirstYear - 2)) On s1.Key Equals s0.Key
                                      Join s2 In cntx.GetSlice("POPURBAN", cntx.FirstYear, CoalescePolicy.MostRecent) On s2.Key Equals s0.Key
                                      Select Key = s1.Key, CPOPURBANBYrM2 = s1.Value, CPOPURBANBYr = s0.Value, Value = s2.Value)

                        If Not item.CPOPURBANBYrM2.HasValue Or Not item.CPOPURBANBYr.HasValue Then
                            CPOPURBGR.Add(item.Key, (CCBR(item.Key) - CCDR(item.Key)) / 10 + 1)
                        Else
                            CPOPURBGR.Add(item.Key, CSng(((item.CPOPURBANBYr / item.CPOPURBANBYrM2) ^ (1 / 2) - 1) * 100))
                        End If
                        CPOPURBGR(item.Key) = Amin(5, CPOPURBGR(item.Key))  'otherwise, like Rwanda, reach 100% very fast

                        CPOPURBAN.Add(item.Key, item.Value)

                        If Not CPOPURBAN(item.Key).HasValue Then
                            CPOPURBAN(item.Key) = EstimatePOPURBAN(item.Key)
                            CPOPURBAN(item.Key) = Amin(0.95F, CPOPURBAN(item.Key)) * CPOP(item.Key)
                        End If

                        If CPOPURBAN(item.Key) / CPOP(item.Key) > 0.9 Then CPOPURBGR(item.Key) = Amin(2, CPOPURBGR(item.Key))
                        If item.CPOPURBANBYr.HasValue And CPOP(item.Key).HasValue Then
                            If item.CPOPURBANBYr >= 0.995 * CPOP(item.Key) Then
                                CPOPURBAN(item.Key) = CPOP(item.Key)  'Singapore and Hong Kong Rule
                            End If
                        End If

                        CPOPURBAN(item.Key) = Amin(CPOP(item.Key), AMAX(CPOPURBAN(item.Key), CSng(0.01 * CPOP(item.Key))))
                    Next
                End If

                'LABPARR
                Dim Estimate As SeriesEstimator = cntx.GetEstimator("LABPARR_Male")

                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs8 As Short() = cntx.GetYears("LABPARR_Male").OrderBy(Function(y) y).ToArray()
                    Dim LABPARR_MaleTbl As IDictionary(Of String, Single?()) = cntx.GetDice("LABPARR_Male", yrs8)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(LABPARR_MaleTbl, yrs8, key, cntx.FirstYear, HistDist)
                        Me.cachedLABPARRMALE.Add(key, ApplyLimits(If(ValHold, Estimate(key)), 5, 95))

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else

                    For Each item In (From s In cntx.GetSlice("LABPARR_Male", cntx.FirstYear, MyBase.Policy)
                                      Select Key = s.Key, Value = ApplyLimits(If(s.Value, Estimate(s.Key)), 5, 95))
                        Me.cachedLABPARRMALE.Add(item.Key, item.Value)
                    Next
                End If

                'JAM 2019-07-13 Commented due mti improvement
                'For Each item In (From s In cntx.GetSlice("LABPARR_Male", cntx.FirstYear, MyBase.Policy)
                '                  Select Key = s.Key, Value = ApplyLimits(If(s.Value, Estimate(s.Key)), 5, 95))
                '    Me.cachedLABPARRMALE.Add(item.Key, item.Value)
                'Next

                Estimate = cntx.GetEstimator("LABPARR_Female")

                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs9 As Short() = cntx.GetYears("LABPARR_Female").OrderBy(Function(y) y).ToArray()
                    Dim LABPARR_FemaleTbl As IDictionary(Of String, Single?()) = cntx.GetDice("LABPARR_Female", yrs9)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(LABPARR_FemaleTbl, yrs9, key, cntx.FirstYear, HistDist)
                        Me.cachedLABPARRFEMALE.Add(key, ApplyLimits(If(ValHold, Estimate(key)), 5, 95))

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else
                    For Each item In (From s In cntx.GetSlice("LABPARR_Female", cntx.FirstYear, MyBase.Policy)
                                      Select Key = s.Key, Value = ApplyLimits(If(s.Value, Estimate(s.Key)), 5, 95))
                        Me.cachedLABPARRFEMALE.Add(item.Key, item.Value)
                    Next
                End If

                'JAM 2019-07-13 Commented due mti improvement
                'For Each item In (From s In cntx.GetSlice("LABPARR_Female", cntx.FirstYear, MyBase.Policy)
                '                  Select Key = s.Key, Value = ApplyLimits(If(s.Value, Estimate(s.Key)), 5, 95))
                '    Me.cachedLABPARRFEMALE.Add(item.Key, item.Value)
                'Next

                'Dim cagedst(,,) As Single?
                Dim cagedstM As Dictionary(Of String, Dictionary(Of String, Single?))
                Dim cagedstF As Dictionary(Of String, Dictionary(Of String, Single?))
                Dim birthmshr As Single, birthfshr As Single

                'ReDim cagedst(cntx.NCohorts, cntx.Regionalization.Count, 2)
                cagedstM = New Dictionary(Of String, Dictionary(Of String, Single?))
                cagedstF = New Dictionary(Of String, Dictionary(Of String, Single?))

                'mti 2020/09/01
                Dim cfagedstM As Dictionary(Of String, Dictionary(Of String, Single?))
                Dim cfagedstF As Dictionary(Of String, Dictionary(Of String, Single?))
                cfagedstM = New Dictionary(Of String, Dictionary(Of String, Single?))
                cfagedstF = New Dictionary(Of String, Dictionary(Of String, Single?))

                Using db As IFsContext = New IFsContext()
                    ReadPopCohortDistCtry(cagedstM, cagedstF, cntx, db)

                    'mti 2020/09/01 start
                    ReadPopAgeSingleDistCtry(cfagedstM, cfagedstF, cntx, db)
                    'mti 2020/09/01 end

                    'Load BirthShare
                    Dim gp As GlobalParameters = db.GlobalParameters.Where(Function(g) g.ParameterName = "BIRTHMSHR").Single()
                    birthmshr = CSng(gp.Value)
                    birthfshr = 1 - birthmshr
                End Using

                For Each key In cntx.Regionalization.Keys
                    'Adjust to Regional Population values
                    AdjustCohPop(cagedstM, cagedstF, CPOP, key, cntx)
                    cagedstM(key)("Cohort0") = CCBR(key) * CPOP(key) * birthmshr / 1000
                    cagedstF(key)("Cohort0") = CCBR(key) * CPOP(key) * birthfshr / 1000
                Next

                'mti 2022/04/12 - start
                For Each key In cntx.Regionalization.Keys
                    IFsPOPSpreadAgeDstCountry(key, cfagedstM, cagedstM)
                    IFsPOPSpreadAgeDstCountry(key, cfagedstF, cagedstF)
                Next
                'mti 2022/04/12 - End


                Dim pop_male As Single?, pop_female As Single?
                Dim j As Integer
                For Each key In cntx.Regionalization.Keys
                    pop_male = 0
                    pop_female = 0
                    For Each cohort As KeyValuePair(Of String, Single?) In cagedstM(key)
                        j = CInt(cohort.Key.Replace("Cohort", ""))
                        If j > 3 Then
                            pop_male = pop_male + cohort.Value
                        End If
                    Next
                    For Each cohort As KeyValuePair(Of String, Single?) In cagedstF(key)
                        j = CInt(cohort.Key.Replace("Cohort", ""))
                        If j > 3 Then
                            pop_female = pop_female + cohort.Value
                        End If
                    Next

                    CLAB.Add(key, pop_female * cachedLABPARRFEMALE(key) / 100 + pop_male * cachedLABPARRMALE(key) / 100)
                    cachedLABPARRTOT.Add(key, 100.0F * CLAB(key) / AMAX(0.001F, (pop_female + pop_male)))
                Next

                cntx.Store("CPOPURBAN", CPOPURBAN)
                cntx.Store("CPOPURBGR", CPOPURBGR)
                cntx.Store("CLAB", CLAB)
                cntx.Store("CCBR", CCBR)
                cntx.Store("CCDR", CCDR)
                cntx.Store("cagedstM", cagedstM)
                cntx.Store("cagedstF", cagedstF)
                cntx.Store("cfagedstM", cfagedstM)
                cntx.Store("cfagedstF", cfagedstF)

                Me.weightSeries = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
                Me.cachedMIGRANTSTOCKS = New Dictionary(Of String, Single?)
                Me.cachedMIGRATE = New Dictionary(Of Integer, Single?)

                'Load entire table for Migration Rate
                'bbh 2020/03/09 actually: SeriesForecastNetMigrationRateMergedUNPDAbel, set in IfsInit.mdb
                Dim yrsMigRate As Short() = cntx.GetYears(cntx.MigrationRateTbl).OrderBy(Function(y) y).ToArray()
                Dim diceMIGRATE As IDictionary(Of String, Single?()) = (
                    From yr In yrsMigRate From kv In cntx.GetSlice(cntx.MigrationRateTbl, yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )

                'bbh 2020/03/09 actually: SeriesForecastNetMigrationMergedUNPDAbel, set in IfsInit.mdb
                Dim yrsMigrants As Short() = cntx.GetYears(cntx.MigrantsTbl).OrderBy(Function(y) y).ToArray()
                Dim diceMIGRANTS As IDictionary(Of String, Single?()) = (
                    From yr In yrsMigrants From kv In cntx.GetSlice(cntx.MigrantsTbl, yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )

                'bbh 2020/03/09 *** start ***
                Dim yrsMigRateIn As Short() = cntx.GetYears("SeriesForecastMigrantRateIn").OrderBy(Function(y) y).ToArray()
                Dim diceMIGRATEIn As IDictionary(Of String, Single?()) = (
                    From yr In yrsMigRateIn From kv In cntx.GetSlice("SeriesForecastMigrantRateIn", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )
                Dim yrsMigRateOut As Short() = cntx.GetYears("SeriesForecastMigrantRateOut").OrderBy(Function(y) y).ToArray()
                Dim diceMIGRATEOut As IDictionary(Of String, Single?()) = (
                    From yr In yrsMigRateOut From kv In cntx.GetSlice("SeriesForecastMigrantRateOut", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )
                'bbh 2020/03/09 *** end ***
                'dsb 2026/02/23 start
                Dim yrsSPOPFOREIGNIn As Short() = cntx.GetYears("SeriesForecastMigrantStockIn").OrderBy(Function(y) y).ToArray()
                Dim diceSPOPFOREIGNIn As IDictionary(Of String, Single?()) = (
                    From yr In yrsSPOPFOREIGNIn From kv In cntx.GetSlice("SeriesForecastMigrantStockIn", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )
                Dim yrsSPOPFOREIGNOut As Short() = cntx.GetYears("SeriesForecastMigrantStockOut").OrderBy(Function(y) y).ToArray()
                Dim diceSPOPFOREIGNOut As IDictionary(Of String, Single?()) = (
                    From yr In yrsSPOPFOREIGNOut From kv In cntx.GetSlice("SeriesForecastMigrantStockOut", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )

                Dim iYrCount As Integer, iYr As Integer, ICount As Integer, R As Integer, iYr2 As Integer
                Dim SUMIM As Single, SUMEM As Single, WORLDIMEM As Single
                Dim NR As Integer = cntx.GetDimension(1).Buckets.Count

                iYrCount = cntx.LastYear - cntx.FirstYear + 1
                Dim MIGRATEY(0, 0) As Single?
                ReDim MIGRATEY(NR, iYrCount)

                'bbh 2020/03/09 start
                Dim CMigrateIIn As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CMigrateIOut As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim MIGRATEYIn(0, 0) As Single?, MIGRATEYOut(0, 0) As Single?
                ReDim MIGRATEYIn(NR, iYrCount), MIGRATEYOut(NR, iYrCount)
                Dim MIGRATEIN As IDictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                Dim MIGRATEOUT As IDictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                'bbh 2020/03/09 end
                'dsb 2026/02/23 
                Dim SPOPFOREIGNINY(0, 0) As Single?, SPOPFOREIGNOUTY(0, 0) As Single?
                ReDim SPOPFOREIGNINY(NR, iYrCount), SPOPFOREIGNOUTY(NR, iYrCount)
                Dim SPOPFOREIGNIN As IDictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                Dim SPOPFOREIGNOUT As IDictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                Dim CSpopForeignIIn As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CSpopForeignIOut As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)

                Dim CMigrateI As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CurrVal As Single?
                Dim MIGRATE As IDictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)

                For iYr = cntx.FirstYear To cntx.LastYear
                    If iYr > yrsMigRate(yrsMigRate.Count - 1) Or iYr > cntx.LastYearMigration Then 'jrs 2013/11/20 added or iYr > LastYearMigration
                        'bbh 2020/03/09 Additions below assume all migrate tables have same dimensionality; all come from Abel via David Bohl
                        'Need to keep constant for the rest of time
                        For R = 1 To NR
                            For iYr2 = iYr To cntx.LastYear 'jrs 2014/09/22 LastYear
                                MIGRATEY(R, iYr2 - cntx.FirstYear + 1) = MIGRATEY(R, iYr - cntx.FirstYear)
                                MIGRATEYIn(R, iYr2 - cntx.FirstYear + 1) = MIGRATEYIn(R, iYr - cntx.FirstYear) 'bbh 2020/03/09
                                MIGRATEYOut(R, iYr2 - cntx.FirstYear + 1) = MIGRATEYOut(R, iYr - cntx.FirstYear) 'bbh 2020/03/09
                                'dsb 2026/02/23
                                SPOPFOREIGNINY(R, iYr2 - cntx.FirstYear + 1) = SPOPFOREIGNINY(R, iYr - cntx.FirstYear)
                                SPOPFOREIGNOUTY(R, iYr2 - cntx.FirstYear + 1) = SPOPFOREIGNOUTY(R, iYr - cntx.FirstYear)
                            Next iYr2
                        Next R
                        Exit For
                    End If

                    SUMIM = 0
                    SUMEM = 0

                    Dim iYrIdx1, iYrIdx2 As Short
                    iYrIdx1 = getYearIdx(yrsMigRateIn, CShort(iYr))
                    iYrIdx2 = getYearIdx(yrsMigRateOut, CShort(iYr))
                    'dsb 2026/02/23
                    Dim iYrIdx3, iYrIdx4 As Short
                    iYrIdx3 = getYearIdx(yrsSPOPFOREIGNIn, CShort(iYr))
                    iYrIdx4 = getYearIdx(yrsSPOPFOREIGNOut, CShort(iYr))

                    For Each key In cntx.Regionalization.Keys
                        If iYr = cntx.FirstYear Then
                            cachedMIGRANTSTOCKS.Add(key, 0F)
                        Else
                            CMigrateI.Remove(key)
                            'jrs 2020/03/19
                            CMigrateIIn.Remove(key)
                            CMigrateIOut.Remove(key)
                            'dsb 2026/02/23
                            CSpopForeignIIn.Remove(key)
                            CSpopForeignIOut.Remove(key)
                        End If

                        If diceMIGRATE(key)(iYr - yrsMigRate(0)).HasValue And diceMIGRANTS(key)(iYr - yrsMigrants(0)).HasValue Then
                            CMigrateI.Add(key, diceMIGRATE(key)(iYr - yrsMigRate(0)))
                            If CMigrateI(key) > 0 Then
                                SUMIM = SUMIM + CSng(diceMIGRANTS(key)(iYr - yrsMigrants(0)))
                            Else
                                SUMEM = SUMEM - CSng(diceMIGRANTS(key)(iYr - yrsMigrants(0)))
                            End If

                            If cntx.HistoricMode Then
                                If iYr <= cntx.FirstYear + 15 Then
                                    CurrVal = cachedMIGRANTSTOCKS(key)
                                    cachedMIGRANTSTOCKS.Remove(key)
                                    cachedMIGRANTSTOCKS.Add(key, CurrVal + CSng(diceMIGRANTS(key)(iYr - yrsMigrants(0))))
                                End If
                            Else
                                If iYr - 40 <= cntx.FirstYear Then
                                    'jrs 2017/08/21
                                    If diceMIGRANTS(key)(iYr - yrsMigrants(0) - 40).HasValue Then
                                        CurrVal = cachedMIGRANTSTOCKS(key)
                                        cachedMIGRANTSTOCKS.Remove(key)
                                        cachedMIGRANTSTOCKS.Add(key, CurrVal + CSng(diceMIGRANTS(key)(iYr - yrsMigrants(0) - 40)))
                                    End If
                                End If
                            End If
                        Else
                            CMigrateI.Add(key, 0F)
                        End If

                        'bbh 2020/03/09 start
                        If iYr < yrsMigRateIn(0) Then
                            'jrs 2020/04/23 CMigrateIIn.Add(key, getClosestYrVal(diceMIGRATEIn, yrsMigRateIn, key, iYr, HistDist))
                            CMigrateIIn.Add(key, 0)
                        Else
                            If diceMIGRATEIn(key)(iYrIdx1).HasValue Then
                                CMigrateIIn.Add(key, diceMIGRATEIn(key)(iYrIdx1))
                            Else
                                CMigrateIIn.Add(key, getClosestYrVal(diceMIGRATEIn, yrsMigRateIn, key, iYr, HistDist))
                            End If
                        End If

                        If iYr < yrsMigRateOut(0) Then
                            'jrs 2020/04/23 CMigrateIOut.Add(key, getClosestYrVal(diceMIGRATEOut, yrsMigRateOut, key, iYr, HistDist))
                            CMigrateIOut.Add(key, 0)
                        Else
                            If diceMIGRATEOut(key)(iYrIdx2).HasValue Then
                                CMigrateIOut.Add(key, diceMIGRATEOut(key)(iYrIdx2))
                            Else
                                CMigrateIOut.Add(key, getClosestYrVal(diceMIGRATEOut, yrsMigRateOut, key, iYr, HistDist))
                            End If
                        End If
                        'bbh 2020/03/09 start

                        'dsb 2026/02/23 start
                        If iYr < yrsSPOPFOREIGNIn(0) Then
                            CSpopForeignIIn.Add(key, 0)
                        Else
                            If diceSPOPFOREIGNIn(key)(iYrIdx3).HasValue Then
                                CSpopForeignIIn.Add(key, diceSPOPFOREIGNIn(key)(iYrIdx3))
                            Else
                                CSpopForeignIIn.Add(key, getClosestYrVal(diceSPOPFOREIGNIn, yrsSPOPFOREIGNIn, key, iYr, HistDist))
                            End If
                        End If

                        If iYr < yrsSPOPFOREIGNOut(0) Then
                            CSpopForeignIOut.Add(key, 0)
                        Else
                            If diceSPOPFOREIGNOut(key)(iYrIdx4).HasValue Then
                                CSpopForeignIOut.Add(key, diceSPOPFOREIGNOut(key)(iYrIdx4))
                            Else
                                CSpopForeignIOut.Add(key, getClosestYrVal(diceSPOPFOREIGNOut, yrsSPOPFOREIGNOut, key, iYr, HistDist))
                            End If
                        End If
                    Next 'each country

                    '   Normalize global migration to average of immigration, emigration
                    '   ----------------------------------------------------------------
                    WORLDIMEM = (SUMIM + SUMEM) / 2

                    For Each key In cntx.Regionalization.Keys
                        If CMigrateI(key) > 0 Then
                            CurrVal = CMigrateI(key)
                            CMigrateI.Remove(key)
                            CMigrateI.Add(key, CurrVal * WORLDIMEM / AMAX(0.001, SUMIM))
                        Else
                            CurrVal = CMigrateI(key)
                            CMigrateI.Remove(key)
                            CMigrateI.Add(key, CurrVal * WORLDIMEM / AMAX(0.001, SUMEM))
                        End If
                    Next

                    ' regionalize series
                    MIGRATE = (From s In CMigrateI
                               Join reg In cntx.Regionalization On s.Key Equals reg.Key
                               Join w In Me.weightSeries On s.Key Equals w.Key
                               Group Value = s.Value, Weight = w.Value By RegionId = reg.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
                Function(grp) grp.RegionId,
                Function(grp) (grp.Data.Average(Function(v) v.Value * v.Weight) / grp.TotalWeight)
            )

                    'bbh 2020/03/09 *** begin ***
                    MIGRATEIN = (From s In CMigrateIIn
                                 Join reg In cntx.Regionalization On s.Key Equals reg.Key
                                 Join w In Me.weightSeries On s.Key Equals w.Key
                                 Group Value = s.Value, Weight = w.Value By RegionId = reg.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
                Function(grp) grp.RegionId,
                Function(grp) (grp.Data.Average(Function(v) v.Value * v.Weight) / grp.TotalWeight)
            )

                    MIGRATEOUT = (From s In CMigrateIOut
                                  Join reg In cntx.Regionalization On s.Key Equals reg.Key
                                  Join w In Me.weightSeries On s.Key Equals w.Key
                                  Group Value = s.Value, Weight = w.Value By RegionId = reg.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
                Function(grp) grp.RegionId,
                Function(grp) (grp.Data.Average(Function(v) v.Value * v.Weight) / grp.TotalWeight)
            )
                    'bbh 2020/03/09 *** end ***

                    'dsb 2026/02/23 *** begin ***
                    SPOPFOREIGNIN = (From s In CSpopForeignIIn
                                     Join reg In cntx.Regionalization On s.Key Equals reg.Key
                                     Join w In Me.weightSeries On s.Key Equals w.Key
                                     Group Value = s.Value, Weight = w.Value By RegionId = reg.Value Into Data = Group, TotalWeight = Sum(Weight)
                                         ).ToDictionary(
                                                Function(grp) grp.RegionId,
                                                Function(grp) (grp.Data.Average(Function(v) v.Value * v.Weight) / grp.TotalWeight)
                                            )
                    SPOPFOREIGNOUT = (From s In CSpopForeignIOut
                                      Join reg In cntx.Regionalization On s.Key Equals reg.Key
                                      Join w In Me.weightSeries On s.Key Equals w.Key
                                      Group Value = s.Value, Weight = w.Value By RegionId = reg.Value Into Data = Group, TotalWeight = Sum(Weight)
                                            ).ToDictionary(
                                                     Function(grp) grp.RegionId,
                                                     Function(grp) (grp.Data.Average(Function(v) v.Value * v.Weight) / grp.TotalWeight)
                                                 )

                    For R = 1 To NR
                        MIGRATEY(R, iYr - cntx.FirstYear + 1) = MIGRATE(R)
                        MIGRATEYIn(R, iYr - cntx.FirstYear + 1) = If(MIGRATEIN(R), 0) 'bbh 2020/03/09 'jrs 2023/04/14 added if(,0)
                        MIGRATEYOut(R, iYr - cntx.FirstYear + 1) = If(MIGRATEOUT(R), 0) 'bbh 2020/03/09 'jrs 2023/04/14 added if(,0)
                        'dsb 2026/02/23
                        SPOPFOREIGNINY(R, iYr - cntx.FirstYear + 1) = If(SPOPFOREIGNIN(R), 0)
                        SPOPFOREIGNOUTY(R, iYr - cntx.FirstYear + 1) = If(SPOPFOREIGNOUT(R), 0)
                    Next R
                Next 'each year

                Dim WMigrantsIn As Single, WMigrantsOut As Single, WMigrants As Single
                WMigrantsIn = 0
                WMigrantsOut = 0

                For Each key In cntx.Regionalization.Keys
                    If cachedMIGRANTSTOCKS(key) > 0 Then WMigrantsIn = WMigrantsIn + CSng(cachedMIGRANTSTOCKS(key))
                    If cachedMIGRANTSTOCKS(key) < 0 Then WMigrantsOut = WMigrantsOut + CSng(cachedMIGRANTSTOCKS(key))
                Next

                '   Balance global migration stocks, inward and outward
                '   ---------------------------------------------------

                WMigrants = (WMigrantsIn + Math.Abs(WMigrantsOut)) / 2
                For Each key In cntx.Regionalization.Keys
                    If cachedMIGRANTSTOCKS(key) > 0 Then
                        CurrVal = cachedMIGRANTSTOCKS(key)
                        cachedMIGRANTSTOCKS.Remove(key)
                        cachedMIGRANTSTOCKS.Add(key, CurrVal / WMigrantsIn * WMigrants)
                    End If
                    If cachedMIGRANTSTOCKS(key) < 0 Then
                        CurrVal = cachedMIGRANTSTOCKS(key)
                        cachedMIGRANTSTOCKS.Remove(key)
                        cachedMIGRANTSTOCKS.Add(key, CurrVal / Math.Abs(WMigrantsOut) * WMigrants)
                    End If
                Next

                'bbh 2020/03/09 start
                If cntx.HistoricMode Then
                    Dim yrsForPopStockIn As Short() = cntx.GetYears("SeriesForecastMigrantStockIn").OrderBy(Function(y) y).ToArray()
                    Dim ForPopStockInTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesForecastMigrantStockIn", yrsForPopStockIn)
                    Dim yrsForPopStockOut As Short() = cntx.GetYears("SeriesForecastMigrantStockOut").OrderBy(Function(y) y).ToArray()
                    Dim ForPopStockOutTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesForecastMigrantStockOut", yrsForPopStockOut)
                    Dim yrsForPopFlowIn As Short() = cntx.GetYears("SeriesForecastMigrantFlowIn").OrderBy(Function(y) y).ToArray()
                    Dim ForPopFlowInTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesForecastMigrantFlowIn", yrsForPopFlowIn)
                    Dim yrsForPopFlowOut As Short() = cntx.GetYears("SeriesForecastMigrantFlowOut").OrderBy(Function(y) y).ToArray()
                    Dim ForPopFlowOutTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesForecastMigrantFlowOut", yrsForPopFlowOut)

                    For Each key In cntx.Regionalization.Keys
                        'jrs 2024/12/04 CPOPFOREIGNIN.Add(key, getClosestYrVal(ForPopStockInTbl, yrsForPopStockIn, key, cntx.FirstYear, HistDist))
                        CPOPFOREIGNIN.Add(key, getClosestYrVal(ForPopStockInTbl, yrsForPopStockIn, key, cntx.FirstYear, 15))
                        'jrs 2024/12/04
                        If CPOPFOREIGNIN(key).HasValue Then
                            CPOPFOREIGNIN(key) = CSng(Amin(CPOPFOREIGNIN(key), 0.9 * CPOP(key)))
                        Else
                            CPOPFOREIGNIN(key) = 0
                        End If
                        CPOPFOREIGNOUT.Add(key, getClosestYrVal(ForPopStockOutTbl, yrsForPopStockOut, key, cntx.FirstYear, HistDist))
                        CMIGRANTSIN.Add(key, getClosestYrVal(ForPopFlowInTbl, yrsForPopFlowIn, key, cntx.FirstYear, HistDist))
                        CMIGRANTSOUT.Add(key, getClosestYrVal(ForPopFlowOutTbl, yrsForPopFlowOut, key, cntx.FirstYear, HistDist))
                    Next
                Else 'Forecast Mode
                    For Each item In (From s In cntx.GetSlice("SeriesForecastMigrantStockIn", cntx.BaseYear, CoalescePolicy.MostRecent) Select Key = s.Key, Value = s.Value)
                        CPOPFOREIGNIN.Add(item.Key, item.Value)
                    Next
                    For Each item In (From s In cntx.GetSlice("SeriesForecastMigrantStockOut", cntx.BaseYear, CoalescePolicy.MostRecent) Select Key = s.Key, Value = s.Value)
                        CPOPFOREIGNOUT.Add(item.Key, item.Value)
                    Next
                    For Each item In (From s In cntx.GetSlice("SeriesForecastMigrantFlowIn", cntx.BaseYear, CoalescePolicy.MostRecent) Select Key = s.Key, Value = s.Value)
                        CMIGRANTSIN.Add(item.Key, item.Value)
                    Next
                    For Each item In (From s In cntx.GetSlice("SeriesForecastMigrantFlowOut", cntx.BaseYear, CoalescePolicy.MostRecent) Select Key = s.Key, Value = s.Value)
                        CMIGRANTSOUT.Add(item.Key, item.Value)
                    Next
                End If
                'bbh 2020/03/09 end

                'Need to write results in MIGRATEY into file SupplementalDataFile$
                'jrs 2014/10/08 *** begin ***'
                'Read file to look for migrater changes
                'Dim PutDataFileDir As String = "D:/My Documents/IFs/DATA/"
                'Dim PutDataFileDir As String = "D:/ifs-vanilla/data/DATA/"
                'Dim SupplementalHistoricDataFile As String = "IFsHistFull.dat"
                'Dim SupplementalDataFile As String = "IFsFull.dat"

                Dim sDatFile As String = cntx.PutDataFileDir
                If cntx.HistoricMode Then
                    sDatFile = sDatFile & "IFsHist.Dat"
                Else
                    sDatFile = sDatFile & "IFs.Dat"
                End If

                Dim VLine As String, bMigrater() As Boolean, bWorld As Boolean
                'gvdb 2024/04/05
            	'unused variable
                Dim X() As String, RegIdx As Integer ', i As Integer
                ReDim bMigrater(NR)
                bWorld = False
                ' Create new StreamReader instance with Using block.
                Using reader As StreamReader = New StreamReader(sDatFile)
                    ' Read one line from file
                    VLine = reader.ReadLine
                    Do While Not VLine = Nothing
                        If VLine.Substring(1, 9).ToUpper = "MIGRATER," Then
                            'if found check if they're applied to the World, if true skip next block
                            If VLine.ToUpper.Contains("""WORLD""") Then
                                bWorld = True
                                reader.ReadToEnd()
                            Else
                                X = VLine.Split(New Char() {","c})
                                X(1) = X(1).Replace("""", "")
                                'if applied to specific countries, create an array of flags to indicate which countries to overwritte
                                '            RegIdx = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, X(1), False)
                                Dim v As New KeyValuePair(Of Short, String)
                                v = cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals(X(1)))
                                RegIdx = v.Key
                                If RegIdx > 0 Then
                                    bMigrater(RegIdx) = True
                                Else
                                    'GROUPS
                                    'if applied to a group, need to decompose group to mark flags
                                    Using db As IFsContext = New IFsContext()
                                        Dim grp As GroupIDNewFull = db.GroupIDNewFull.Where(Function(g) g.GroupName = X(1)).Single()
                                        db.Entry(grp).Collection(Function(g) g.Regions).Load()
                                        For Each reg As GroupingFull In grp.Regions
                                            'Console.WriteLine(reg.RegionName)
                                            RegIdx = reg.RegionId
                                            bMigrater(RegIdx) = True
                                        Next
                                    End Using
                                End If
                            End If
                        End If

                        VLine = reader.ReadLine
                    Loop
                End Using

                'jrs 2020/03/19
                Dim bDyadic As Boolean
                bDyadic = System.IO.File.Exists(cntx.DataFileDir$ + "IFsForDyadic.db")

                If Not bWorld Then
                    Dim objWriter As System.IO.StreamWriter
                    'If Historic ProcessTimeSeries picks up IFsHist.dat
                    If cntx.HistoricMode Then
                        'jrs 2020/09/02 put Data File at the end
                        'System.IO.File.Copy(cntx.PutDataFileDir + "IFsHist.Dat", cntx.PutDataFileDir + cntx.SupplementalHistoricDataFile, overwrite:=True)
                        'objWriter = New System.IO.StreamWriter(cntx.PutDataFileDir + cntx.SupplementalHistoricDataFile, append:=True)
                        objWriter = New System.IO.StreamWriter(cntx.PutDataFileDir + cntx.SupplementalHistoricDataFile, append:=False)
                    Else 'if Future oriented, need to seed SupplementDatafile with IFs.Dat
                        'jrs 2020/09/02 put Data File at the end
                        'System.IO.File.Copy(cntx.PutDataFileDir + "IFs.Dat", cntx.PutDataFileDir + cntx.SupplementalDataFile, overwrite:=True)
                        'objWriter = New System.IO.StreamWriter(cntx.PutDataFileDir + cntx.SupplementalDataFile, append:=True)
                        objWriter = New System.IO.StreamWriter(cntx.PutDataFileDir + cntx.SupplementalDataFile, append:=False)
                    End If
                    'jrs 2020/09/03 objWriter.WriteLine("")

                    Dim YearCount As Integer, WriteStr As String, IY As Integer, WriteStrAdd As String
                    Dim WriteStrIn, WriteStrOut, WriteStrAddIn, WriteStrAddOut As String
                    'dsb 2026/02/23 Add foreignborn stocks to data file along with migrate, migratein, and migrateout
                    Dim WriteStrStockOut, WriteStrStockIn, WriteStrAddStockOut, WriteStrAddStockIn As String
                    YearCount = cntx.LastYear - cntx.FirstYear + 1

                    If Not cntx.HistoricMode Then
                        Using reader As StreamReader = New StreamReader(sDatFile)
                            ' Read one line from file
                            WriteStr = reader.ReadLine
                            If Not WriteStr = Nothing Then
                                If WriteStr.ToUpper.Contains("BASEYEAR") Then
                                    objWriter.WriteLine(WriteStr)
                                End If
                            End If
                        End Using
                    End If

                    For ICount% = 1 To NR
                        'jrs 2014/10/08
                        If Not bMigrater(ICount%) Then
                            WriteStr$ = "migrater," + """" + cntx.GetDimension(1).Buckets(CShort(ICount%)) + """" + ","
                            'jrs 2024/11/13 WriteStrIn$ = "MIGRATEIN," + """" + cntx.GetDimension(1).Buckets(CShort(ICount%)) + """" + "," 'bbh 2020/03/09
                            WriteStrIn$ = "migrateinr," + """" + cntx.GetDimension(1).Buckets(CShort(ICount%)) + """" + "," 'bbh 2020/03/09
                            'jrs 2024/11/13 WriteStrOut$ = "MIGRATEOUT," + """" + cntx.GetDimension(1).Buckets(CShort(ICount%)) + """" + "," 'bbh 2020/03/09
                            WriteStrOut$ = "migrateoutr," + """" + cntx.GetDimension(1).Buckets(CShort(ICount%)) + """" + "," 'bbh 2020/03/09
                            'dsb 2026/02/23 Add foreignborn stocks to data file along with migrate, migratein, and migrateout
                            WriteStrStockIn$ = "stockin," + """" + cntx.GetDimension(1).Buckets(CShort(ICount%)) + """" + "," 'bbh 2020/03/09
                            WriteStrStockOut$ = "stockout," + """" + cntx.GetDimension(1).Buckets(CShort(ICount%)) + """" + "," 'bbh 2020/03/09
                            For IY% = 1 To YearCount%
                                If IY% = 1 Then
                                    WriteStrAdd = Math.Round(CSng(MIGRATEY(ICount%, 1)), 4).ToString.TrimStart
                                    WriteStrAddIn$ = Math.Round(CSng(MIGRATEYIn(ICount%, 1)), 4).ToString.TrimStart  'bbh 2020/03/09
                                    WriteStrAddOut$ = Math.Round(CSng(MIGRATEYOut(ICount%, 1)), 4).ToString.TrimStart  'bbh 2020/03/09
                                    WriteStr$ = WriteStr$ + WriteStrAdd$
                                    WriteStrIn$ = WriteStrIn$ + WriteStrAddIn$ 'bbh 2020/03/09
                                    WriteStrOut$ = WriteStrOut$ + WriteStrAddOut$ 'bbh 2020/03/09
                                    'dsb 2026/02/23 Add foreignborn stocks to data file along with migrate, migratein, and migrateout
                                    WriteStrAddStockIn$ = Math.Round(CSng(SPOPFOREIGNINY(ICount%, 1)), 4).ToString.TrimStart
                                    WriteStrAddStockOut$ = Math.Round(CSng(SPOPFOREIGNOUTY(ICount%, 1)), 4).ToString.TrimStart
                                    WriteStrStockIn$ = WriteStrStockIn$ + WriteStrAddStockIn$
                                    WriteStrStockOut$ = WriteStrStockOut$ + WriteStrAddStockOut$
                                End If
                                'jrs 2013/11/05 *** begin ***
                                If IY% > 1 Then
                                    WriteStr = WriteStr + "," + Math.Round(CSng(MIGRATEY(ICount%, IY)), 4).ToString.TrimStart
                                    WriteStrIn$ = WriteStrIn$ + "," + Math.Round(CSng(MIGRATEYIn(ICount%, IY)), 4).ToString.TrimStart 'bbh 2020/03/09
                                    WriteStrOut$ = WriteStrOut$ + "," + Math.Round(CSng(MIGRATEYOut(ICount%, IY)), 4).ToString.TrimStart 'bbh 2020/03/09
                                    'dsb 2026/02/23 Add foreignborn stocks to data file along with migrate, migratein, and migrateout
                                    WriteStrStockIn$ = WriteStrStockIn$ + "," + Math.Round(CSng(SPOPFOREIGNINY(ICount%, IY)), 4).ToString.TrimStart
                                    WriteStrStockOut$ = WriteStrStockOut$ + "," + Math.Round(CSng(SPOPFOREIGNOUTY(ICount%, IY)), 4).ToString.TrimStart
                                End If
                                'jrs 2013/11/05 *** end ***
                            Next IY%
                            objWriter.WriteLine(WriteStr)
                            'jrs 2020/06/12 If Dyadic Data is present we still need it, in case running the regular base
                            'jrs 2020/06/15 If Not bDyadic Then
                            objWriter.WriteLine(WriteStrIn$) 'bbh 2020/03/09
                            objWriter.WriteLine(WriteStrOut$) 'bbh 2020/03/09
                            'dsb 2026/02/23 Add foreignborn stocks to data file along with migrate, migratein, and migrateout
                            objWriter.WriteLine(WriteStrStockIn$)
                            objWriter.WriteLine(WriteStrStockOut$)
                            'End If
                        End If 'jrs 2014/10/08
                    Next ICount%

                    'jrs 2020/09/03
                    If cntx.HistoricMode Then
                        'jrs 2020/09/02 Now open Data File and append the content at the end
                        'this will allow to add a line for MIGRATEIN and MIGRATEOUT
                        sDatFile = cntx.PutDataFileDir + "IFsHist.Dat"
                        Using reader As StreamReader = New StreamReader(sDatFile)
                            ' Read one line from file
                            WriteStr = reader.ReadLine
                            Do While Not WriteStr = Nothing
                                objWriter.WriteLine(WriteStr)
                                WriteStr = reader.ReadLine
                            Loop
                        End Using
                    End If

                    objWriter.Close()
                End If
                'jrs 2014/10/08 *** end ***

                cntx.Store("CPOPFOREIGN", cachedMIGRANTSTOCKS)
                'jrs 2020/03/19
                cntx.Store("CPOPFOREIGNIN", CPOPFOREIGNIN)
                cntx.Store("CPOPFOREIGNOUT", CPOPFOREIGNOUT)
                cntx.Store("CMIGRANTSIN", CMIGRANTSIN)
                cntx.Store("CMIGRANTSOUT", CMIGRANTSOUT)
                cntx.Store("MIGRATEIN", MIGRATEIN)
                cntx.Store("MIGRATEOUT", MIGRATEOUT)

                ' mark pre-processed for all three series
                Me.processed = True
            End Sub

            <ExportsAsset("CBR")>
            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))

                Me.PreProcess(cntx)
                Dim series As IDictionary(Of String, Single?)
                Select Case seriesName
                    Case "CBR"
                        series = Me.CCBR

                        Dim CBR As Dictionary(Of Integer, Single?)

                        CBR = (From s In series
                               Join r In cntx.Regionalization On s.Key Equals r.Key
                               Join w In Me.CPOP On s.Key Equals w.Key
                               Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                                ).ToDictionary(
                                    Function(grp) grp.RegionId,
                                    Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 0, 50)
                                )
                        cntx.Store("CBR", CBR)
                        Return CBR
                    Case "CDR"
                        series = Me.CCDR
                    Case "POPURBAN"
                        series = CPOPURBAN
                    Case "POPURBGR"
                        series = CPOPURBGR
                    Case "LABPARR_Male"
                        series = Me.cachedLABPARRMALE
                    Case "LABPARR_Female"
                        series = Me.cachedLABPARRFEMALE
                    Case "LABPARR_Total"
                        series = Me.cachedLABPARRTOT
                    Case "LAB"
                        series = Me.CLAB
                    Case Else
                        Throw New ArgumentException($"Aggregator cannot be applied on series {seriesName}.")
                End Select

                If seriesName = "POPURBAN" Then
                    ' regionalize series
                    Return (From s In series
                            Join r In cntx.Regionalization On s.Key Equals r.Key
                            Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
                ).ToDictionary(
                    Function(grp) grp.RegionId,
                    Function(grp) ApplyLimits(grp.TotalVal, 0, 1000)
                )
                Else
                    If seriesName = "CBR" Or seriesName = "CDR" Then
                        ' regionalize series
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In Me.CPOP On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                ).ToDictionary(
                    Function(grp) grp.RegionId,
                    Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, 0, 50)
                )
                    Else
                        If seriesName = "POPURBGR" Then
                            'mti 2022/09/15 changed the minimum limit from 0 to -5
                            ' regionalize series POPURBGR
                            Return (From s In series
                                    Join r In cntx.Regionalization On s.Key Equals r.Key
                                    Join w In Me.CPOP On s.Key Equals w.Key
                                    Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                            ).ToDictionary(
                                Function(grp) grp.RegionId,
                                Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, -5, 1000)
                            )
                        ElseIf seriesName = "LAB" Then
                            ' regionalize series
                            Return (From s In series
                                    Join r In cntx.Regionalization On s.Key Equals r.Key
                                    Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
                            ).ToDictionary(
                                Function(grp) grp.RegionId,
                                Function(grp) grp.TotalVal
                            )
                        Else 'LABPARR_MALE, LABPARR_FEMALE, LABPARR_TOTAL
                            Return (From s In series
                                    Join r In cntx.Regionalization On s.Key Equals r.Key
                                    Join w In Me.CPOP On s.Key Equals w.Key
                                    Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                            ).ToDictionary(
                                Function(grp) grp.RegionId,
                                Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
                            )

                        End If
                    End If
                End If
            End Function
        End Class

        Public Sub IFsPOPSpreadAgeDstCountry(sCtry As String, ByRef cfagedst As Dictionary(Of String, Dictionary(Of String, Single?)), ByRef cagedst As Dictionary(Of String, Dictionary(Of String, Single?)))
            '------------------------------------------------------------
            'mti 2022/04/12 - take new proc - brought 'jrs 2022/01/25 from populat
            Dim Sum As Single, Sum2 As Single, MulFage As Single
            For j% = 1 To NCohorts
                If j% < NCohorts Then
                    cfagedst(sCtry)("Age" & (j% * 5 - 5).ToString) = AMAX(0, Pop_AgeYearCountry(j% * 5 - 5, sCtry, cagedst))
                    cfagedst(sCtry)("Age" & (j% * 5 - 4).ToString) = AMAX(0, Pop_AgeYearCountry(j% * 5 - 4, sCtry, cagedst))
                    cfagedst(sCtry)("Age" & (j% * 5 - 3).ToString) = AMAX(0, Pop_AgeYearCountry(j% * 5 - 3, sCtry, cagedst))
                    cfagedst(sCtry)("Age" & (j% * 5 - 2).ToString) = AMAX(0, Pop_AgeYearCountry(j% * 5 - 2, sCtry, cagedst))
                    cfagedst(sCtry)("Age" & (j% * 5 - 1).ToString) = AMAX(0, Pop_AgeYearCountry(j% * 5 - 1, sCtry, cagedst))
                    Sum = CSng(cfagedst(sCtry)("Age" & (j% * 5 - 5).ToString) + cfagedst(sCtry)("Age" & (j% * 5 - 4).ToString) +
                    cfagedst(sCtry)("Age" & (j% * 5 - 3).ToString) + cfagedst(sCtry)("Age" & (j% * 5 - 2).ToString) +
                    cfagedst(sCtry)("Age" & (j% * 5 - 1).ToString))
                    'dsr 20160324 Change next line - models seems to see differences when there are none
                    If Math.Abs(CDbl(Sum - cagedst(sCtry)("Cohort" + j%.ToString))) > 0.0001 Then
                        '                    If Sum <> cagedst(sCtry)("Cohort" + j%.ToString) Then
                        If cagedst(sCtry)("Cohort" + j%.ToString) > 0 And Sum > 0 Then
                            MulFage = CSng(cagedst(sCtry)("Cohort" + j%.ToString) / Sum)
                            cfagedst(sCtry)("Age" & (j% * 5 - 5).ToString) = cfagedst(sCtry)("Age" & (j% * 5 - 5).ToString) * MulFage
                            cfagedst(sCtry)("Age" & (j% * 5 - 4).ToString) = cfagedst(sCtry)("Age" & (j% * 5 - 4).ToString) * MulFage
                            cfagedst(sCtry)("Age" & (j% * 5 - 3).ToString) = cfagedst(sCtry)("Age" & (j% * 5 - 3).ToString) * MulFage
                            cfagedst(sCtry)("Age" & (j% * 5 - 2).ToString) = cfagedst(sCtry)("Age" & (j% * 5 - 2).ToString) * MulFage
                            cfagedst(sCtry)("Age" & (j% * 5 - 1).ToString) = cfagedst(sCtry)("Age" & (j% * 5 - 1).ToString) * MulFage
                            Sum2 = CSng(cfagedst(sCtry)("Age" & (j% * 5 - 5).ToString) + cfagedst(sCtry)("Age" & (j% * 5 - 4).ToString) +
                            cfagedst(sCtry)("Age" & (j% * 5 - 3).ToString) + cfagedst(sCtry)("Age" & (j% * 5 - 2).ToString) +
                            cfagedst(sCtry)("Age" & (j% * 5 - 1).ToString))
                            'dsr 20160324 Change next line - models seems to see differences when there are none
                            If Math.Abs(CDbl(Sum2 - cagedst(sCtry)("Cohort" + j%.ToString))) > 0.0001 And bShowMsg Then
                                '                            If Sum2 <> cagedst(sCtry)("Cohort" + j%.ToString) And bShowMsg Then 'jrs 2008/04/02
                                Dim msg As String = "There is an error in spreading populuation to 1-year cohorts"
                                Console.WriteLine(msg)
                                'Stop
                            End If
                        End If
                    End If
                Else
                    cfagedst(sCtry)("Age" & NAges.ToString) = cagedst(sCtry)("Cohort" + j%.ToString) '>=100 Years of Age
                End If
            Next j%


        End Sub

        '=============================Datapop.BAS  =================================
        Public Function Pop_AgeYearCountry(ageyear As Integer, sCtry As String, ByRef cagedst As Dictionary(Of String, Dictionary(Of String, Single?))) As Single
            '- Pop Pre-Proc --------------------------
            'mti 2022/04/12 take new proc
            'mti Sept 2006, Added end cohorts
            'uses sprague multiplier for spreading agecohort to ageyears

            'brought from populat.bas; should later merge with the onr in Populat.bas; cagedst() is declared at module level
            'F0 is cohort of ageyear; F_2 (etc.) is two cohorts back; f2 (etc.) is two chorts forward
            'PopC(1) = 0-4 years;
            Dim f0 As Single?, f1 As Single?, f2 As Single?, f3 As Single?
            Dim F_1 As Single?, cchrt As Integer, F_2 As Single?, F_3 As Single?

            Pop_AgeYearCountry = 0
            Select Case ageyear
                Case 0 To 4 'mti May 30 2006
                    f0 = cagedst(sCtry)("Cohort1")
                    f1 = cagedst(sCtry)("Cohort2")
                    f2 = cagedst(sCtry)("Cohort3")
                    f3 = cagedst(sCtry)("Cohort4")
                    Select Case ageyear
                        Case 0
                            Pop_AgeYearCountry = CSng(0.3616 * f0 - 0.2768 * f1 + 0.1488 * f2 - 0.0336 * f3)
                        Case 1
                            Pop_AgeYearCountry = CSng(0.264 * f0 - 0.096 * f1 + 0.04 * f2 - 0.008 * f3)
                        Case 2
                            Pop_AgeYearCountry = CSng(0.184 * f0 + 0.04 * f1 - 0.032 * f2 + 0.008 * f3)
                        Case 3
                            Pop_AgeYearCountry = CSng(0.12 * f0 + 0.136 * f1 - 0.072 * f2 + 0.016 * f3)
                        Case 4
                            Pop_AgeYearCountry = CSng(0.0704 * f0 + 0.1968 * f1 - 0.0848 * f2 + 0.0176 * f3)
                    End Select

                Case 5 To 9
                    F_1 = cagedst(sCtry)("Cohort1")
                    f0 = cagedst(sCtry)("Cohort2")
                    f1 = cagedst(sCtry)("Cohort3")
                    f2 = cagedst(sCtry)("Cohort4")

                    Select Case ageyear
                        Case 5
                            'mti May 30 2006
                            Pop_AgeYearCountry = CSng(0.0336 * F_1 + 0.2272 * f0 - 0.0752 * f1 + 0.0144 * f2)
                        Case 6
                            Pop_AgeYearCountry = CSng(0.008 * F_1 + 0.232 * f0 - 0.048 * f1 + 0.008 * f2)
                        Case 7
                            Pop_AgeYearCountry = CSng(-0.008 * F_1 + 0.216 * f0 - 0.008 * f1)
                        Case 8
                            Pop_AgeYearCountry = CSng(-0.016 * F_1 + 0.184 * f0 + 0.04 * f1 - 0.008 * f2)
                        Case 9
                            Pop_AgeYearCountry = CSng(-0.0176 * F_1 + 0.1408 * f0 + 0.0912 * f1 - 0.0144 * f2)
                    End Select

                Case 10 To 89
                    cchrt = 1 + ageyear \ 5
                    F_2 = cagedst(sCtry)("Cohort" & (cchrt - 2).ToString)
                    F_1 = cagedst(sCtry)("Cohort" & (cchrt - 1).ToString)
                    f0 = cagedst(sCtry)("Cohort" & (cchrt).ToString)
                    f1 = cagedst(sCtry)("Cohort" & (cchrt + 1).ToString)
                    f2 = cagedst(sCtry)("Cohort" & (cchrt + 2).ToString)


                    Select Case ageyear Mod 5
                        Case 0
                            Pop_AgeYearCountry = CSng(-0.0128 * F_2 + 0.0848 * F_1 + 0.1504 * f0 - 0.024 * f1 + 0.0016 * f2)
                        Case 1
                            Pop_AgeYearCountry = CSng(-0.0016 * F_2 + 0.0144 * F_1 + 0.2224 * f0 - 0.0416 * f1 + 0.0064 * f2)
                        Case 2
                            Pop_AgeYearCountry = CSng(0.0064 * F_2 - 0.0336 * F_1 + 0.2544 * f0 - 0.0336 * f1 + 0.0064 * f2)
                        Case 3
                            Pop_AgeYearCountry = CSng(0.0064 * F_2 - 0.0416 * F_1 + 0.2224 * f0 + 0.0144 * f1 - 0.0016 * f2)
                        Case 4
                            Pop_AgeYearCountry = CSng(0.0016 * F_2 - 0.024 * F_1 + 0.1504 * f0 + 0.0848 * f1 - 0.0128 * f2)
                    End Select

                Case 90 To 94
                    cchrt = 1 + ageyear \ 5
                    F_2 = cagedst(sCtry)("Cohort" & (cchrt - 2).ToString)
                    F_1 = cagedst(sCtry)("Cohort" & (cchrt - 1).ToString)
                    f0 = cagedst(sCtry)("Cohort" & (cchrt).ToString)
                    f1 = cagedst(sCtry)("Cohort" & (cchrt + 1).ToString)

                    Select Case ageyear
                        Case 90
                            'mti May 30 2006
                            Pop_AgeYearCountry = CSng(-0.0144 * F_2 + 0.0912 * F_1 + 0.1408 * f0 - 0.0176 * f1)
                        Case 91
                            Pop_AgeYearCountry = CSng(-0.008 * F_2 + 0.04 * F_1 + 0.184 * f0 - 0.016 * f1)
                        Case 92
                            Pop_AgeYearCountry = CSng(-0.008 * F_1 + 0.216 * f0 - 0.008 * f1)
                        Case 93
                            Pop_AgeYearCountry = CSng(0.008 * F_2 - 0.048 * F_1 + 0.232 * f0 + 0.008 * f1)
                        Case 94
                            Pop_AgeYearCountry = CSng(0.0144 * F_2 - 0.0752 * F_1 + 0.2272 * f0 + 0.0336 * f1)
                    End Select

                Case 95 To 99
                    cchrt = 1 + ageyear \ 5
                    F_3 = cagedst(sCtry)("Cohort" & (cchrt - 3).ToString)
                    F_2 = cagedst(sCtry)("Cohort" & (cchrt - 2).ToString)
                    F_1 = cagedst(sCtry)("Cohort" & (cchrt - 1).ToString)
                    f0 = cagedst(sCtry)("Cohort" & (cchrt).ToString)

                    Select Case ageyear
                        Case 95
                            'mti May 30 2006
                            Pop_AgeYearCountry = CSng(0.0176 * F_3 - 0.0848 * F_2 + 0.1968 * F_1 + 0.0704 * f0)
                        Case 96
                            Pop_AgeYearCountry = CSng(0.016 * F_3 - 0.072 * F_2 + 0.136 * F_1 + 0.12 * f0)

                        Case 97
                            Pop_AgeYearCountry = CSng(0.008 * F_3 - 0.032 * F_2 + 0.04 * F_1 + 0.184 * f0)

                        Case 98
                            Pop_AgeYearCountry = CSng(-0.008 * F_3 + 0.04 * F_2 - 0.096 * F_1 + 0.264 * f0)
                        Case 99
                            Pop_AgeYearCountry = CSng(-0.0336 * F_3 + 0.1488 * F_2 - 0.2768 * F_1 + 0.3616 * f0)
                    End Select

                Case 100 To NAges - 1 'jrs 2012/09/05
                    cchrt = 1 + ageyear \ 5
                    Pop_AgeYearCountry = CSng(cagedst(sCtry)("Cohort" & cchrt.ToString) * 0.2)
            End Select

        End Function

    End Module


End Namespace
