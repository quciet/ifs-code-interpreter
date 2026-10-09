Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data

Namespace Models

    Partial Public Module Environment

        <RebuildsVariable("ENVTEMP", AggregatorType:=GetType(ENVTEMPAggregator))>
        <ImportsAsset("CLandArea")>
        Public Iterator Function RebuildENVTEMP(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Yield context.Query("ENVTEMP").Filter(BaseYearOnly).Execute()
        End Function

        <RebuildsVariable("ENVPM2PT5", AggregatorType:=GetType(CENVPM2PT5Aggregator))>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        <ImportsAsset("CEDYRSAGE25T")>
        Public Iterator Function RebuildENVPM2PT5(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Yield context.Query("ENVPM2PT5").Filter(BaseYearOnly).Execute()
        End Function

        <RebuildsVariable("CARANN", AggregatorType:=GetType(CCARANNAggregator))>
        <RebuildsVariable("carinit")>
        <RebuildsVariable("carannwi")>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        <ImportsAsset("ENPOIL")>
        <ImportsAsset("ENPGAS")>
        <ImportsAsset("ENPCOAL")>
        Public Iterator Function RebuildCARANN(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Yield context.Query("CARANN").Filter(BaseYearOnly).Execute()

            CO2SystemInit(context)

            Dim dfcarinit As Dataframe = Dataframe.Create("carinit", context, context.FirstYear, context.LastYear, carinit)
            Yield dfcarinit

            Dim dfcarannwi As Dataframe = Dataframe.Create("carannwi", context, context.FirstYear, context.LastYear, carannwi)
            Yield dfcarannwi

        End Function

        'bbh 2020/02/24 Initialize Atmospheric CO2 level and World Carbon Emissions
        Private Sub CO2SystemInit(cntx As IRebuildFacade)
            Using db As IFsContext = New IFsContext()

                Dim TargetYear As Integer
                If cntx.HistoricMode = True Then
                    TargetYear = cntx.FirstYear - 1
                Else
                    TargetYear = BaseYear - 1 'Minus 1 because is augmented in model first year
                End If

                'jrs 2024/02/02 Dim gp1 As GlobalParametersTimeSeriesLong = db.GlobalParametersTimeSeriesLong.Where(Function(g) g.DataYear = TargetYear).Single()
                Dim gp1 As GlobalParametersTimeSeriesLong
                Try
                    gp1 = db.GlobalParametersTimeSeriesLong.Where(Function(g) g.DataYear = TargetYear).Single()
                Catch ex As Exception
                    gp1 = Nothing
                End Try

                If gp1 Is Nothing Then
                    TargetYear = db.GlobalParametersTimeSeriesLong.Max(Function(g) g.DataYear)
                    gp1 = db.GlobalParametersTimeSeriesLong.Where(Function(g) g.DataYear = TargetYear).Single()
                End If
                carinit = If(gp1.AtmosphericCO2PPM.HasValue, CSng(gp1.AtmosphericCO2PPM), 414)
                carannwi = If(gp1.AnnWorldEmsC.HasValue, CSng(gp1.AnnWorldEmsC), 10)
            End Using

        End Sub

        <RebuildsVariable("COOKSTOVES", AggregatorType:=GetType(CCOOKSTOVESAggregator))>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        <ImportsAsset("CPOPURBAN")>
        <ImportsAsset("CInfraElecAccTotal")>
        <ImportsAsset("CHHSIZE")>
        Public Iterator Function RebuildCOOKSTOVES(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Yield context.Query("COOKSTOVES").Filter(BaseYearOnly).Execute()
        End Function
    End Module

    Class CENVPM2PT5Aggregator
        Inherits DefaultSumAggregator


        'JAM 2018-12-28
        Private Property processed As Boolean


        'Pop is used as weight for the average
        Private Property weightSeries As IDictionary(Of String, Single?)
        ' Private Property gdppcp As IDictionary(Of String, Single?)

        'JAM 2018-12-28
        Private Property GDPPCP As IDictionary(Of String, Single?)
        Private Property EDYRSAGE25T As IDictionary(Of String, Single?)



        'JAM 2018-12-28
        Private Property CENVPM2PT5 As IDictionary(Of String, Single?)


        ''' <summary>
        ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' 
        ''' JAM 2018-12-18 PreProcess
        ''' 
        ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' </summary>

        ''' <param name="cntx"></param>
        ''' <returns></returns>
        Private Sub PreProcess(cntx As IRebuildFacade)
            If Me.processed Then
                Return
            End If

            Dim objWriter = New System.IO.StreamWriter(cntx.PutFdrvRun + "progress.txt", append:=True)
            objWriter.WriteLine("Processing Data on Environment - Please Wait")
            objWriter.Close()

            Me.weightSeries = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
            Me.GDPPCP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            Me.EDYRSAGE25T = cntx.Fetch(Of IDictionary(Of String, Single?))("CEDYRSAGE25T")


            Me.CENVPM2PT5 = New Dictionary(Of String, Single?)

            'Load PM2.5 conversion factors
            Dim ConvFacts As Dictionary(Of String, Single) = New Dictionary(Of String, Single)

            Using db As IFsContext = New IFsContext()
                Dim grp As GroupIDNewFull = db.GroupIDNewFull.Where(Function(g) g.GroupName = "WB Developing Economies").Single()
                db.Entry(grp).Collection(Function(g) g.Regions).Load()

                Dim grp2 As GroupIDNewFull = db.GroupIDNewFull.Where(Function(g) g.GroupName = "Europe").Single()
                db.Entry(grp2).Collection(Function(g) g.Regions).Load()

                'Developing Countries (W/O Europe)
                For Each reg As GroupingFull In grp.Regions
                    For Each item In (From s In cntx.Regionalization Where s.Value = reg.RegionId)
                        ConvFacts.Add(item.Key, 0.5)
                    Next
                Next

                'Europe
                For Each reg As GroupingFull In grp2.Regions
                    For Each item In (From s In cntx.Regionalization Where s.Value = reg.RegionId)
                        If Not ConvFacts.ContainsKey(item.Key) Then
                            ConvFacts.Add(item.Key, 0.73)
                        Else
                            ConvFacts(item.Key) = 0.73
                        End If
                    Next
                Next

                'Rest of the world
                For Each key In cntx.Regionalization.Keys
                    If Not ConvFacts.ContainsKey(key) Then
                        ConvFacts.Add(key, 0.65)
                    End If
                Next

            End Using


            Dim Comp As Single?

            'jrs 2010/03/05
            '*************** Please don't remove this comment ********************
            '   Need this comments to Mark tables as used in the preprocessor
            '   which is needed for estimation when Adding Provinces
            'jrs 2012/02/17 added the * to mark that they're used in preprocessor,
            'since commented tables are no longer marked
            '*    From s In cntx.GetSlice("SeriesEnvAvgAnnTemp", cntx.FirstYear, MyBase.Policy)
            '*    From s In cntx.GetSlice("SeriesEnvAvgAnnTempChg", cntx.FirstYear, MyBase.Policy)
            '*    From s In cntx.GetSlice("SeriesEnvPrecipitation", cntx.FirstYear, MyBase.Policy)
            '*    From s In cntx.GetSlice("SeriesEnvPrecipitationChg", cntx.FirstYear, MyBase.Policy)
            '***************** end comment ***************************************


            If cntx.HistoricMode Then
                'JAM 2019-01-13
                For Each item In (From s In cntx.GetSlice("SeriesEnvPMWDI", cntx.FirstYear, MyBase.Policy)
                                  Select Key = s.Key, Value = If(s.Value, 0))
                    CENVPM2PT5.Add(item.Key, 0)
                Next

            Else 'Forecast *********



                For Each item In (From s In cntx.GetSlice("SeriesEnvPMWDI", cntx.FirstYear, MyBase.Policy)
                                  Select Key = s.Key, Value = s.Value)
                    CENVPM2PT5.Add(item.Key, item.Value)
                Next



                'Comp = Exp(4.8929 + 0.2145 * Log(CGDPPCP(ICount%)) - 0.0995 * (Log(CGDPPCP(ICount%)) ^ 2) -
                '0.451 * Log(CEDYRSAGE25T(ICount%)) - 0.0181 * (BaseYear - 1989))
                'CENVPM2PT5(ICount%) = Comp * rsC.Fields(1)

                For Each key In cntx.Regionalization.Keys

                    If Not CENVPM2PT5(key).HasValue Then

                        Comp = CSng(Math.Exp(4.8929 + 0.2145 * Math.Log(CDbl(GDPPCP(key))) - 0.0995 * (Math.Log(CDbl(GDPPCP(key))) ^ 2) -
                                0.451 * Math.Log(CDbl(EDYRSAGE25T(key))) - 0.0181 * (cntx.FirstYear - 1989)))

                        '¿?
                        'CENVPM2PT5(ICount%) = Comp * rsC.Fields(1)

                        CENVPM2PT5(key) = Comp * ConvFacts(key)


                    End If



                Next


            End If 'End of if history else forecast



            ' mark pre-processed for all three series
            Me.processed = True
        End Sub

        Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
            'JAM 2018-12-15
            Me.PreProcess(cntx)
            Dim Estimate As SeriesEstimator = cntx.GetEstimator(seriesName)
            Dim series As IDictionary(Of String, Single?)

            'Estimate = cntx.GetRegression("Percent of People using Primarily Solid Fuels (Full Model 2010)").
            'Normalize("GDPPCP", "INFRAELECACC", "PopUrban%").ToEstimator(gdppcp, cachedTFR)

            Select Case seriesName
                Case "ENVPM2PT5"
                    series = Me.CENVPM2PT5
                Case Else
                    'Throw New ArgumentException($"Aggregator cannot be applied on series {seriesName}.")
                    series = New Dictionary(Of String, Single?)
                    For Each key In cntx.Regionalization.Keys
                        series.Add(key, Nothing)
                    Next
            End Select


            ' regionalize series
            'Return (From s In series
            '        Join r In cntx.Regionalization On s.Key Equals r.Key
            '        Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
            ').ToDictionary(
            '    Function(grp) grp.RegionId,
            '    Function(grp) ApplyLimits(grp.TotalVal, 0, 1000)
            ')

            'JAM 2019-01-13 Regionalization based in average
            Return (From s In series
                    Join r In cntx.Regionalization On s.Key Equals r.Key
                    Join w In weightSeries On s.Key Equals w.Key
                    Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
           ).ToDictionary(
               Function(grp) grp.RegionId,
               Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
               )



        End Function

    End Class
    ''''''JAM 2018-02-12
    Class CCARANNAggregator
        Inherits DefaultSumAggregator


        'JAM 2018-12-15

        Private Property processed As Boolean
        Private Property carfuel1 As Single?
        Private Property carfuel2 As Single?
        Private Property carfuel3 As Single?

        'Pop is used as weight for the average
        Private Property weightSeries As IDictionary(Of String, Single?)
        Private Property gdppcp As IDictionary(Of String, Single?)

        'JAM 2018-12-16
        Private Property ENPOIL As IDictionary(Of String, Single?)
        Private Property ENPGAS As IDictionary(Of String, Single?)
        Private Property ENPCOAL As IDictionary(Of String, Single?)


        'JAM 2018-12-25
        Private Property CCARANN As IDictionary(Of String, Single?)


        ''' <summary>
        ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' 
        ''' JAM 2018-12-15 PreProcess
        ''' 
        ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' </summary>

        ''' <param name="cntx"></param>
        ''' <returns></returns>
        Private Sub PreProcess(cntx As IRebuildFacade)
            If Me.processed Then
                Return
            End If

            'Dim vtemp As Boolean
            Me.weightSeries = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
            Me.gdppcp = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            Me.ENPOIL = cntx.Fetch(Of IDictionary(Of String, Single?))("ENPOIL")
            Me.ENPGAS = cntx.Fetch(Of IDictionary(Of String, Single?))("ENPGAS")
            Me.ENPCOAL = cntx.Fetch(Of IDictionary(Of String, Single?))("ENPCOAL")

            Me.CCARANN = New Dictionary(Of String, Single?)



            'JAM 2018-12-26 According to vb6 version, itsn't difference between historic and forecast modes
            'If cntx.HistoricMode Then
            '    vtemp = False

            'Else 'Forecast *********

            'JAM 2018-12-16
            'Load CCARANN required values
            Using db As IFsContext = New IFsContext()

                'jrs 2022/07/27 changed to upper case to match content of file
                Dim gp1 As GlobalParameters = db.GlobalParameters.Where(Function(g) g.ParameterName = "CARFUEL1").Single()
                carfuel1 = CSng(gp1.Value)

                Dim gp2 As GlobalParameters = db.GlobalParameters.Where(Function(g) g.ParameterName = "CARFUEL2").Single()
                carfuel2 = CSng(gp2.Value)

                Dim gp3 As GlobalParameters = db.GlobalParameters.Where(Function(g) g.ParameterName = "CARFUEL3").Single()
                carfuel3 = CSng(gp3.Value)

            End Using

            Dim yrs As Short() = cntx.GetYears("SeriesEmissionsCarbonCDIAC").OrderBy(Function(y) y).ToArray()
            Dim valtbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesEmissionsCarbonCDIAC", yrs)

            'jrs 2022/11/01 flex constant dollars
            Dim yrsGDP As Short() = cntx.GetYears("GDP" & ConstantDollarsYear.ToString).OrderBy(Function(y) y).ToArray()
            Dim GDPTbl As IDictionary(Of String, Single?()) = cntx.GetDice("GDP" & ConstantDollarsYear.ToString, yrsGDP)

            If Not cntx.HistoricMode Then
                For Each item In (From s In cntx.GetSlice("SeriesEmissionsCarbonCDIAC", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value)
                    CCARANN.Add(item.Key, Nothing)
                    CCARANN(item.Key) = EstimateUsingElasticity(BaseYear, valtbl, yrs, GDPTbl, yrsGDP, item.Key, 6, 5) '10,5 (interval needs to be at least one more)
                    If Not CCARANN(item.Key).HasValue Then
                        CCARANN(item.Key) = item.Value
                    End If
                Next
            Else
                'jrs 2020/03/30
                Dim ValHold As Single?
                For Each key In cntx.Regionalization.Keys
                    ValHold = getClosestYrVal(valtbl, yrs, key, cntx.FirstYear, HistDist)
                    CCARANN.Add(key, ValHold)
                Next
            End If

            'CCCARANN(ICount%) = (ENPOil(ICount%) * carfuel1 +
            'ENPGas(ICount%) * carfuel2 + ENPCoal(ICount%) * carfuel3)
            'carfuel1, carfuel2, carfuel3


            For Each key In cntx.Regionalization.Keys
                If Not CCARANN(key).HasValue Then
                    CCARANN(key) = ENPOIL(key) * carfuel1 + ENPGAS(key) *
                              carfuel2 + ENPCOAL(key) * carfuel3
                End If
                'jrs 2026/06/02 add a limit to avoid negatives
                CCARANN(key) = CSng(AMAX(CCARANN(key), 0.00001))
            Next


            'End If 'End of if history else forecast



            ' mark pre-processed for all three series
            Me.processed = True
        End Sub

        Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
            'JAM 2018-12-15
            Me.PreProcess(cntx)
            Dim Estimate As SeriesEstimator = cntx.GetEstimator(seriesName)
            Dim series As IDictionary(Of String, Single?)

            'Estimate = cntx.GetRegression("Percent of People using Primarily Solid Fuels (Full Model 2010)").
            'Normalize("GDPPCP", "INFRAELECACC", "PopUrban%").ToEstimator(gdppcp, cachedTFR)

            Select Case seriesName
                Case "CARANN"
                    series = Me.CCARANN
                Case Else
                    'Throw New ArgumentException($"Aggregator cannot be applied on series {seriesName}.")
                    series = New Dictionary(Of String, Single?)
                    For Each key In cntx.Regionalization.Keys
                        series.Add(key, Nothing)
                    Next
            End Select


            ' regionalize series
            'Return (From s In series
            '        Join r In cntx.Regionalization On s.Key Equals r.Key
            '        Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
            ').ToDictionary(
            '    Function(grp) grp.RegionId,
            '    Function(grp) ApplyLimits(grp.TotalVal, 0, 1000)
            ')

            'JAM 2019-01-13 Regionalization based in average
            Return (From s In series
                    Join r In cntx.Regionalization On s.Key Equals r.Key
                    Join w In Me.CCARANN On s.Key Equals w.Key
                    Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
           ).ToDictionary(
               Function(grp) grp.RegionId,
               Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
           )

        End Function

    End Class

    ''''''JAM 2018-02-29
    Class CCOOKSTOVESAggregator
        Inherits DefaultSumAggregator


        'JAM 2018-12-29

        Private Property processed As Boolean


        'Pop is used as weight for the average
        Private Property gdppcp As IDictionary(Of String, Single?)

        'JAM 2018-12-16
        Private Property POP As IDictionary(Of String, Single?)
        Private Property POPURBAN As IDictionary(Of String, Single?)
        Private Property INFRAELECACC As IDictionary(Of String, Single?)
        Private Property CHHSIZE As IDictionary(Of String, Single?)

        'JAM 2018-12-25
        Private Property CCOOKSTOVES As IDictionary(Of String, Single?)
        Private Property CCOOKSTOVES2 As IDictionary(Of String, Single?)
        Private Property CCOOKSTOVES3 As IDictionary(Of String, Single?)
        Private Property CCOOKSTOVESBaseYear As IDictionary(Of String, Single?)
        Private Property CCOOKSTOVESEarliest As IDictionary(Of String, Single?)
        Private Property CCOMPINFRAPOP As IDictionary(Of String, Single?)


        ''' <summary>
        ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' 
        ''' JAM 2018-12-15 PreProcess
        ''' 
        ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' </summary>

        ''' <param name="cntx"></param>
        ''' <returns></returns>
        Private Sub PreProcess(cntx As IRebuildFacade)
            If Me.processed Then
                Return
            End If

            'Dim vtemp As Boolean
            Me.gdppcp = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            Me.POP = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
            Me.POPURBAN = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOPURBAN")
            Me.INFRAELECACC = cntx.Fetch(Of IDictionary(Of String, Single?))("CInfraElecAccTotal")
            Me.CHHSIZE = cntx.Fetch(Of IDictionary(Of String, Single?))("CHHSIZE")

            Me.CCOOKSTOVES = New Dictionary(Of String, Single?)
            Me.CCOOKSTOVES2 = New Dictionary(Of String, Single?)
            Me.CCOOKSTOVES3 = New Dictionary(Of String, Single?)
            Me.CCOOKSTOVESBaseYear = New Dictionary(Of String, Single?)
            Me.CCOOKSTOVESEarliest = New Dictionary(Of String, Single?)
            Me.CCOMPINFRAPOP = New Dictionary(Of String, Single?)

            Dim ValHold As Single?


            If cntx.HistoricMode Then

                'JAM 2019-06-08 Commented due mti improved
                'For Each item In (From s In cntx.GetSlice("SeriesEnvSolidFuelsImproved", "Earliest")
                '                  Select Key = s.Key, Value = If(s.Value, 0))
                '    CCOOKSTOVES.Add(item.Key, item.Value)
                'Next


                'mti 2019/04/29  Flex Hist Base Start
                Dim yrs As Short() = cntx.GetYears("SeriesEnvSolidFuels").OrderBy(Function(y) y).ToArray()
                Dim CookStvPctTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesEnvSolidFuels", yrs)
                For Each key In cntx.Regionalization.Keys
                    ValHold = getClosestYrVal(CookStvPctTbl, yrs, key, cntx.FirstYear, HistDist)
                    CCOOKSTOVES.Add(key, ValHold)

                Next
                'mti 2019/04/29  Flex Hist Base End

            Else 'Forecast *********


                For Each item In (From s In cntx.GetSlice("SeriesEnvSolidFuels", cntx.FirstYear, MyBase.Policy)
                                  Select Key = s.Key, Value = s.Value)
                    CCOOKSTOVES.Add(item.Key, item.Value)
                Next

            End If 'End of if history else forecast

            '            Call AnalFunc("Percent of People using Primarily Solid Fuels (Full Model 2010)",
            'Array(CGDPPCP(ICount%), CInfraElecAccTotal(ICount%), 100 * CPOPURBAN(ICount%) / CPop(ICount%)), cVal,
            'Array("GDPPCP", "INFRAELECACC", "PopUrban%"))

            '¿? calculating in toestimator CInfraElecAccTotal(ICount%), 100 * CPOPURBAN(ICount%) / CPop(ICount%))

            'JAM 2019-01-13 Calculating Required parameter array for Normalize
            'For Each item In (From s In cntx.GetSlice("POPURBAN", cntx.FirstYear, MyBase.Policy)
            '                  Join s1 In cntx.GetSlice("POP", cntx.FirstYear, MyBase.Policy) On s.Key Equals s1.Key
            '                  Select Key = s.Key, Value = If(s.Value, (100 * s.Value / If(s1.Value, 1))))

            '    Me.CCOMPINFRAPOP.Add(item.Key, item.Value)
            'Next
            'jrs 2020/01/15
            For Each key In cntx.Regionalization.Keys
                CCOMPINFRAPOP.Add(key, 100 * POPURBAN(key) / POP(key))
            Next

            Dim EstimateM As SeriesEstimator = cntx.GetRegression("Percent of People using Primarily Solid Fuels (Full Model 2010)").
            Normalize("GDPPCP", "INFRAELECACC", "PopUrban%").ToEstimator(gdppcp, INFRAELECACC, CCOMPINFRAPOP)


            ' Reference process MATMORT - AnalFunc
            'Dim EstimateM As SeriesEstimator = cntx.GetRegression("MaternalMortalityRatio versus TFR(Log) & GDPPCP(Log)").
            'Normalize("GDP per capita in 1000 USD (Log)", "TFRMedUNPD(MOSTRECENT) (Log)").ToEstimator(gdppcp, cachedTFR)


            'Call AnalFunc("MaternalMortalityRatio versus TFR(Log) & GDPPCP(Log)", Array(CGDPPCP(ICount%), CTFR(ICount%)), MarmortTemp, Array("GDP per capita in 1000 USD (Log)", "TFRMedUNPD(MOSTRECENT) (Log)"))

            'JAM 2019-01-22
            For Each item In (From s In cntx.GetSlice("SeriesEnvSolidFuelsImproved", cntx.BaseYear, CoalescePolicy.MostRecent)
                              Select Key = s.Key, Value = If(s.Value, 0))
                CCOOKSTOVESBaseYear.Add(item.Key, item.Value)
            Next


            'JAM 2019-06-08 Commented due MTI improved
            'JAM 2019-01-22
            'For Each item In (From s In cntx.GetSlice("SeriesEnvSolidFuelsImproved", "Earliest")
            '                  Select Key = s.Key, Value = If(s.Value, 0))
            '    CCOOKSTOVESEarliest.Add(item.Key, item.Value)
            'Next


            'mti 2019/04/29  Flex Hist Base Start
            Dim yrs2 As Short() = cntx.GetYears("SeriesEnvSolidFuelsImproved").OrderBy(Function(y) y).ToArray()
            Dim CCOOKSTOVESEarliest As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesEnvSolidFuelsImproved", yrs2)
            'mti 2019/04/29  Flex Hist Base End

            For Each key In cntx.Regionalization.Keys
                If Not CCOOKSTOVES(key).HasValue Then

                    '100 / (Exp(-1 * cVal) + 1)

                    CCOOKSTOVES(key) = CSng(100 / (Math.Exp(-1 * CDbl(EstimateM(key))) + 1))

                End If

                CCOOKSTOVES3.Add(key, CSng(100 - CCOOKSTOVES(key)))


                If Not cntx.HistoricMode Then
                    CCOOKSTOVES2.Add(key, CCOOKSTOVESBaseYear(key))


                Else
                    'JAM 2019-06-08 Commented due MTI improved
                    'CCOOKSTOVES2.Add(key, CCOOKSTOVESEarliest(key))

                    'mti 2019/04/29  Flex Hist Base Start
                    ValHold = getClosestYrVal(CCOOKSTOVESEarliest, yrs2, key, cntx.FirstYear, HistDist)
                    CCOOKSTOVES2.Add(key, ValHold)
                    'mti 2019/04/29  Flex Hist Base End


                End If

                If Not CCOOKSTOVES2(key).HasValue Then
                    CCOOKSTOVES2(key) = 0
                Else
                    CCOOKSTOVES(key) = CCOOKSTOVES(key) - CCOOKSTOVES2(key)
                    If CCOOKSTOVES(key) < 0 Then
                        CCOOKSTOVES2(key) = CCOOKSTOVES2(key) + CCOOKSTOVES(key)
                        CCOOKSTOVES(key) = 0
                    End If
                End If

                CCOOKSTOVES(key) = CCOOKSTOVES(key) / 100 * POP(key) / CHHSIZE(key)
                CCOOKSTOVES2(key) = CCOOKSTOVES2(key) / 100 * POP(key) / CHHSIZE(key)
                CCOOKSTOVES3(key) = CCOOKSTOVES3(key) / 100 * POP(key) / CHHSIZE(key)

                '¿? 
                ''CCOOKSTOVES(ICount%, 3) = 100 - CCOOKSTOVES(ICount%, 1)
                '''jrs 2013/12/19
                '''Improved
                ''If frm_MainMenu.IFsGUIctrl1.cHistoricBase = False Then
                ''    CCOOKSTOVES(ICount%, 2) = getYearOrMstRct(CookStvImpPctTbl, BaseYear)
                ''Else
                ''    CCOOKSTOVES(ICount%, 2) = CookStvImpPctTbl.Fields("Earliest")
                ''End If
                ''If IsNull(CCOOKSTOVES(ICount%, 2)) Then
                ''    CCOOKSTOVES(ICount%, 2) = 0
                ''Else
                ''    CCOOKSTOVES(ICount%, 1) = CCOOKSTOVES(ICount%, 1) - CCOOKSTOVES(ICount%, 2)
                ''    If CCOOKSTOVES(ICount%, 1) < 0 Then
                ''        CCOOKSTOVES(ICount%, 2) = CCOOKSTOVES(ICount%, 2) + CCOOKSTOVES(ICount%, 1)
                ''        CCOOKSTOVES(ICount%, 1) = 0
                ''    End If
                ''End If
                '''Convert to absolute numbers from Percent
                '''----------------------------------------
                ''CCOOKSTOVES(ICount%, 1) = CCOOKSTOVES(ICount%, 1) / 100 * CPop(ICount%) / CHHSIZE(ICount%)
                ''CCOOKSTOVES(ICount%, 2) = CCOOKSTOVES(ICount%, 2) / 100 * CPop(ICount%) / CHHSIZE(ICount%)
                ''CCOOKSTOVES(ICount%, 3) = CCOOKSTOVES(ICount%, 3) / 100 * CPop(ICount%) / CHHSIZE(ICount%)


            Next






            ' mark pre-processed for all three series
            Me.processed = True
        End Sub

        Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
            'JAM 2018-12-29
            Me.PreProcess(cntx)
            Dim Estimate As SeriesEstimator = cntx.GetEstimator(seriesName)
            Dim series As IDictionary(Of String, Single?)

            'Estimate = cntx.GetRegression("Percent of People using Primarily Solid Fuels (Full Model 2010)").
            'Normalize("GDPPCP", "INFRAELECACC", "PopUrban%").ToEstimator(gdppcp, cachedTFR)

            Select Case seriesName
                Case "COOKSTOVES_Traditional"
                    series = Me.CCOOKSTOVES
                Case "COOKSTOVES_Improved"
                    series = Me.CCOOKSTOVES2
                Case "COOKSTOVES_ModernFuel"
                    series = Me.CCOOKSTOVES3
                Case Else
                    'Throw New ArgumentException($"Aggregator cannot be applied on series {seriesName}.")
                    series = New Dictionary(Of String, Single?)
                    For Each key In cntx.Regionalization.Keys
                        series.Add(key, Nothing)
                    Next
            End Select


            ' regionalize series
            'Return (From s In series
            '        Join r In cntx.Regionalization On s.Key Equals r.Key
            '        Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
            ').ToDictionary(
            '    Function(grp) grp.RegionId,
            '    Function(grp) ApplyLimits(grp.TotalVal, 0, 1000)
            ')
            If seriesName = "COOKSTOVES_Traditional" Then
                Return (From s In series
                        Join r In cntx.Regionalization On s.Key Equals r.Key
                        Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalAvg = Average(Value)
                       ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) grp.TotalAvg
                       )
            ElseIf seriesName = "COOKSTOVES_Improved" Then
                Return (From s In series
                        Join r In cntx.Regionalization On s.Key Equals r.Key
                        Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalAvg = Average(Value)
                      ).ToDictionary(
                          Function(grp) grp.RegionId,
                          Function(grp) grp.TotalAvg
                      )
            ElseIf seriesName = "COOKSTOVES_ModernFuel" Then
                Return (From s In series
                        Join r In cntx.Regionalization On s.Key Equals r.Key
                        Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalAvg = Average(Value)
                      ).ToDictionary(
                          Function(grp) grp.RegionId,
                          Function(grp) grp.TotalAvg
                      )


            Else
                Return (From s In series
                        Join r In cntx.Regionalization On s.Key Equals r.Key
                        Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalAvg = Average(Value)
                      ).ToDictionary(
                          Function(grp) grp.RegionId,
                          Function(grp) grp.TotalAvg
                      )


            End If

        End Function

    End Class

    Class ENVTEMPAggregator
        Inherits DefaultSumAggregator

        Private Property processed As Boolean


        'Land is used as weight for the average
        Private Property weightSeries As IDictionary(Of String, Single?)

        Private Property CENVTEMP As IDictionary(Of String, Single?)


        Private Sub PreProcess(cntx As IRebuildFacade)
            If Me.processed Then
                Return
            End If

            Dim objWriter = New System.IO.StreamWriter(cntx.PutFdrvRun + "progress.txt", append:=True)
            objWriter.WriteLine("Processing Data on Temperature - Please Wait")
            objWriter.Close()

            Me.weightSeries = cntx.Fetch(Of IDictionary(Of String, Single?))("CLandArea")

            Me.CENVTEMP = New Dictionary(Of String, Single?)

            If cntx.HistoricMode Then
                'JAM 2019-01-13
                For Each item In (From s In cntx.GetSlice("SeriesForecastEnvTempMedianRCP45", 1986)
                                  Select Key = s.Key, Value = s.Value)
                    CENVTEMP.Add(item.Key, item.Value)
                Next

            Else 'Forecast *********

                For Each item In (From s In cntx.GetSlice("SeriesForecastEnvTempMedianRCP45", cntx.FirstYear)
                                  Select Key = s.Key, Value = s.Value)
                    CENVTEMP.Add(item.Key, item.Value)
                Next

            End If 'End of if history else forecast

            'Kosovo, Hong Kong and Taiwan are empty, use proxies
            If Not CENVTEMP("Kosovo").HasValue Then CENVTEMP("Kosovo") = CENVTEMP("Serbia")
            If Not CENVTEMP("Hong Kong").HasValue Then CENVTEMP("Hong Kong") = CENVTEMP("Korea, Republic of")
            If Not CENVTEMP("Taiwan").HasValue Then CENVTEMP("Taiwan") = CENVTEMP("Korea, Republic of")
            'jrs 2023/04/12
            If Not CENVTEMP("Kiribati").HasValue Then CENVTEMP("Kiribati") = CENVTEMP("Tonga")
            If Not CENVTEMP("Sahrawi Arab Democratic Republic").HasValue Then CENVTEMP("Sahrawi Arab Democratic Republic") = CENVTEMP("Mali")

            ' mark pre-processed for all three series
            Me.processed = True
        End Sub

        Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
            Me.PreProcess(cntx)
            'Dim Estimate As SeriesEstimator = cntx.GetEstimator(seriesName)
            Dim series As IDictionary(Of String, Single?)

            'Estimate = cntx.GetRegression("Percent of People using Primarily Solid Fuels (Full Model 2010)").
            'Normalize("GDPPCP", "INFRAELECACC", "PopUrban%").ToEstimator(gdppcp, cachedTFR)

            Select Case seriesName
                Case "ENVTEMP"
                    series = Me.CENVTEMP
                Case Else
                    'Throw New ArgumentException($"Aggregator cannot be applied on series {seriesName}.")
                    series = New Dictionary(Of String, Single?)
                    For Each key In cntx.Regionalization.Keys
                        series.Add(key, Nothing)
                    Next
            End Select


            ' regionalize series
            'Return (From s In series
            '        Join r In cntx.Regionalization On s.Key Equals r.Key
            '        Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
            ').ToDictionary(
            '    Function(grp) grp.RegionId,
            '    Function(grp) ApplyLimits(grp.TotalVal, 0, 1000)
            ')

            'JAM 2019-01-13 Regionalization based in average
            Return (From s In series
                    Join r In cntx.Regionalization On s.Key Equals r.Key
                    Join w In weightSeries On s.Key Equals w.Key
                    Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
           ).ToDictionary(
               Function(grp) grp.RegionId,
               Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
               )



        End Function

    End Class

End Namespace