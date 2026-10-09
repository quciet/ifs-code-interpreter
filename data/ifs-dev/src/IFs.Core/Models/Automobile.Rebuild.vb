Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data
Imports System.Math 'to avoid having to put Math in front of Log and Exp

Namespace Models
    Partial Public Module Automobile

        <RebuildsVariable("CARS", AggregatorType:=GetType(AutoAggregator))>
        <RebuildsVariable("TRUCKS", AggregatorType:=GetType(AutoAggregator))>
        <RebuildsVariable("CARSTOT", AggregatorType:=GetType(AutoAggregator))>
        <RebuildsVariable("TRUCKSTOT", AggregatorType:=GetType(AutoAggregator))>
        <RebuildsVariable("VEHICLESTOT", AggregatorType:=GetType(AutoAggregator))>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        <ImportsAsset("CLandArea")>
        Public Iterator Function RebuildAutomobile(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Yield context.Query("CARS").Filter(BaseYearOnly).Execute()
            Yield context.Query("TRUCKS").Filter(BaseYearOnly).Execute()
            Yield context.Query("CARSTOT").Filter(BaseYearOnly).Execute()
            Yield context.Query("TRUCKSTOT").Filter(BaseYearOnly).Execute()
            Yield context.Query("VEHICLESTOT").Filter(BaseYearOnly).Execute()
        End Function

        Class AutoAggregator
            Inherits DefaultSumAggregator

            Private Property CCarsTotal As IDictionary(Of String, Single?)
            Private Property CTrucksTotal As IDictionary(Of String, Single?)
            Private Property CVehiclesTotal As IDictionary(Of String, Single?)
            Private Property CCarsType As IDictionary(Of String, Single?())
            Private Property CTrucksType As IDictionary(Of String, Single?())
            Private Property CVehicFlpc As IDictionary(Of String, Single?)

            Private Property CGDPPCP As IDictionary(Of String, Single?)
            Private Property CPOP As IDictionary(Of String, Single?)
            Private Property CLandArea As IDictionary(Of String, Single?)

            Private Property processed As Boolean

            Private Sub PreProcess(cntx As IRebuildFacade)
                If Me.processed Then
                    Return
                End If

                Dim objWriter = New System.IO.StreamWriter(cntx.PutFdrvRun + "progress.txt", append:=True)
                objWriter.WriteLine("Processing Data on Automobiles - Please Wait")
                objWriter.Close()

                CGDPPCP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
                CPOP = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
                CLandArea = cntx.Fetch(Of IDictionary(Of String, Single?))("CLandArea")

                CCarsTotal = New Dictionary(Of String, Single?)
                CTrucksTotal = New Dictionary(Of String, Single?)
                CVehiclesTotal = New Dictionary(Of String, Single?)
                CCarsType = New Dictionary(Of String, Single?())
                CTrucksType = New Dictionary(Of String, Single?())
                CVehicFlpc = New Dictionary(Of String, Single?)

                '
                '   Read Total Cars and Total Trucks and Fill in Missing Data
                '   ---------------------------------------------------------
                Dim TotalCarCount As Single?, TotalTruckCount As Single?
                Using dbCV As IFsCoVaTraContext = New IFsCoVaTraContext()
                    'jrs 2025/05/15
                    Dim TFNameTotCarsPC = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Total Cars/Capita"
                    TFNameTotCarsPC = getFunctionNameYrOrMstRct(TFNameTotCarsPC, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")

                    'Dim XYTABL As SeriesEstimator = cntx.GetRegression("GDP/Capita (PPP) Versus Total Cars/Capita").ToEstimator(CGDPPCP)
                    Dim XYTABL As SeriesEstimator = cntx.GetRegression(TFNameTotCarsPC).ToEstimator(CGDPPCP)
                    Dim XYTABL2 As SeriesEstimator = cntx.GetRegression("GDP/Capita (PPP) Versus Total Trucks/Capita").ToEstimator(CGDPPCP)

                    For Each valtbl As Automobiles In dbCV.Automobiles
                        CCarsTotal.Add(valtbl.Country, valtbl.CarsTot)
                        If Not CCarsTotal(valtbl.Country).HasValue Or CCarsTotal(valtbl.Country) = 0 Then
                            'Missing Data - Fill
                            CCarsTotal(valtbl.Country) = XYTABL(valtbl.Country)
                            CCarsTotal(valtbl.Country) = AMAX(0.001F, CCarsTotal(valtbl.Country)) 'Still per capita
                            CCarsTotal(valtbl.Country) = AMAX(2, CCarsTotal(valtbl.Country) * CPOP(valtbl.Country) * 1000000)
                        End If
                        CTrucksTotal.Add(valtbl.Country, valtbl.TrucksTot)
                        If Not CTrucksTotal(valtbl.Country).HasValue Or CTrucksTotal(valtbl.Country) = 0 Then
                            CTrucksTotal(valtbl.Country) = XYTABL2(valtbl.Country)
                            CTrucksTotal(valtbl.Country) = AMAX(0.001F, CTrucksTotal(valtbl.Country)) 'Still per capita
                            CTrucksTotal(valtbl.Country) = AMAX(2.0F, CTrucksTotal(valtbl.Country) * CPOP(valtbl.Country) * 1000000)
                        End If
                        CVehiclesTotal(valtbl.Country) = CCarsTotal(valtbl.Country) + CTrucksTotal(valtbl.Country)

                        '   Fill data holes for specific vehicle types and normalize to total
                        '   -----------------------------------------------------------------
                        Dim CarsTypeVals(11) As Single?, TruckTypeVals(12) As Single?
                        CCarsType.Add(valtbl.Country, CarsTypeVals)
                        For cc% = 1 To 11 'Could use MaxVal(11) from IFs.grp
                            Select Case cntx.GetDimension(11).Buckets(CShort(cc%))
                                Case "Mini"
                                    CCarsType(valtbl.Country)(cc%) = valtbl.Mini
                                Case "Small"
                                    CCarsType(valtbl.Country)(cc%) = valtbl.Small
                                Case "LowMed"
                                    CCarsType(valtbl.Country)(cc%) = valtbl.LowMed
                                Case "Interm"
                                    CCarsType(valtbl.Country)(cc%) = valtbl.Interm
                                Case "UpMed"
                                    CCarsType(valtbl.Country)(cc%) = valtbl.UpMed
                                Case "Large"
                                    CCarsType(valtbl.Country)(cc%) = valtbl.Large
                                Case "High"
                                    CCarsType(valtbl.Country)(cc%) = valtbl.High
                                Case "LFamWag"
                                    CCarsType(valtbl.Country)(cc%) = valtbl.LFamWag
                                Case "SFamWag"
                                    CCarsType(valtbl.Country)(cc%) = valtbl.SFamWag
                                Case "SportL"
                                    CCarsType(valtbl.Country)(cc%) = valtbl.SportL
                                Case "SportH"
                                    CCarsType(valtbl.Country)(cc%) = valtbl.SportH
                            End Select
                        Next cc%

                        CTrucksType.Add(valtbl.Country, TruckTypeVals)
                        For cc% = 1 To 12 'Could use MaxVal(11) from IFs.grp
                            Select Case cntx.GetDimension(12).Buckets(CShort(cc%))
                                Case "PCD"
                                    CTrucksType(valtbl.Country)(cc%) = valtbl.PCD
                                Case "SPickup"
                                    CTrucksType(valtbl.Country)(cc%) = valtbl.SPickup
                                Case "SVan"
                                    CTrucksType(valtbl.Country)(cc%) = valtbl.SVan
                                Case "MPickup"
                                    CTrucksType(valtbl.Country)(cc%) = valtbl.MPickup
                                Case "LPickup"
                                    CTrucksType(valtbl.Country)(cc%) = valtbl.LPickup
                                Case "SUtility"
                                    CTrucksType(valtbl.Country)(cc%) = valtbl.SUtility
                                Case "MUtility"
                                    CTrucksType(valtbl.Country)(cc%) = valtbl.MUtility
                                Case "LUtility"
                                    CTrucksType(valtbl.Country)(cc%) = valtbl.LUtility
                                Case "MVan"
                                    CTrucksType(valtbl.Country)(cc%) = valtbl.MVan
                                Case "LVan"
                                    CTrucksType(valtbl.Country)(cc%) = valtbl.LVan
                                Case "LightComl"
                                    CTrucksType(valtbl.Country)(cc%) = valtbl.LightComl
                                Case "MedHeavy"
                                    CTrucksType(valtbl.Country)(cc%) = valtbl.MedHeavy
                            End Select
                        Next cc%
                    Next 'valtbl in Automobiles
                End Using

                Dim IndVar As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each key In cntx.Regionalization.Keys
                    IndVar.Add(key, CSng(CCarsTotal(key) / AMAX(0.001F, CPOP(key)) / 1000000))
                Next
                Dim Title As String
                Dim XYTABLs(11) As SeriesEstimator
                For cc% = 1 To 11
                    Title = "CarsTot Versus Cars" + cntx.GetDimension(11).Buckets(CShort(cc%))
                    XYTABLs(cc%) = cntx.GetRegression(Title).ToEstimator(IndVar)
                Next
                Dim IndVar2 As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each key In cntx.Regionalization.Keys
                    IndVar2.Add(key, CSng(CTrucksTotal(key) / AMAX(0.001, CPOP(key)) / 1000000))
                Next
                Dim XYTABL2s(12) As SeriesEstimator
                For cc% = 1 To 12
                    Title = "TrucksTot Versus Trucks" + cntx.GetDimension(12).Buckets(CShort(cc%))
                    XYTABL2s(cc%) = cntx.GetRegression(Title).ToEstimator(IndVar2)
                Next

                For Each key In cntx.Regionalization.Keys
                    TotalCarCount = 0
                    For cc% = 1 To 11 'Could use MaxVal(11) from IFs.grp
                        If Not CCarsType(key)(cc%).HasValue Then
                            CCarsType(key)(cc%) = XYTABLs(cc%)(key)
                        End If
                        CCarsType(key)(cc%) = AMAX(0, CCarsType(key)(cc%)) * CCarsTotal(key)
                        TotalCarCount = TotalCarCount + CCarsType(key)(cc%)
                    Next cc%
                    For cc% = 1 To 11
                        CCarsType(key)(cc%) = CCarsType(key)(cc%) _
                            * CCarsTotal(key) / AMAX(1, TotalCarCount)
                    Next cc%

                    TotalTruckCount = 0
                    For cc% = 1 To 12 'Could use MaxVal(11) from IFs.grp
                        If Not CTrucksType(key)(cc%).HasValue Then
                            CTrucksType(key)(cc%) = XYTABL2s(cc%)(key)
                        End If
                        CTrucksType(key)(cc%) = AMAX(0, CTrucksType(key)(cc%)) * CTrucksTotal(key)
                        TotalTruckCount = TotalTruckCount + CTrucksType(key)(cc%)
                    Next cc%
                    For cc% = 1 To 12
                        CTrucksType(key)(cc%) = CTrucksType(key)(cc%) _
                            * CTrucksTotal(key) / AMAX(1, TotalTruckCount)
                    Next cc%
                Next 'country

                '   Read vehicles (fleet) per capita and fill holes
                '   ------------------------------------------------
                'jrs 2007/10/17
                '852 is the saturation level for the US
                'other countries with higher density have a lower saturation level
                'Reduction factor comes from a paper by Dargay, Gately, and Sommer 2007
                Dim sTecLeader As String = "United States of America"
                If Not cntx.Regionalization.Keys.Contains(sTecLeader) Then
                    sTecLeader = "California"
                End If
                Dim RdcFc As Single?, CalcVal As Double?
                For Each key In cntx.Regionalization.Keys
                    If CPOP(key) / CLandArea(key) > CPOP(sTecLeader) / CLandArea(sTecLeader) Then
                        RdcFc = CSng(38.8 * (CPOP(key) / CLandArea(key) - CPOP(sTecLeader) / CLandArea(sTecLeader)))
                        'jrs 2010/07/12
                        'set a maximum of 852/2 for the reduction factor
                        'Singapore is going crazy trying to reduce the factor by 2700
                        RdcFc = CSng(Amin(RdcFc, 852 / 2))
                    Else
                        RdcFc = 0
                    End If
                    CalcVal = (852 - RdcFc) * Exp(CDbl(-5.987 * Exp(CDbl(-0.2 * CDbl(CGDPPCP(key).ToString())))))
                    CVehicFlpc.Add(key, CSng(CalcVal))
                Next

                If Not cntx.HistoricMode Then
                    For Each item In (From s In cntx.GetSlice("SeriesVehiclesper1000", cntx.BaseYear, CoalescePolicy.MostRecent) Select Key = s.Key, Value = s.Value)
                        If item.Value.HasValue Then
                            CalcVal = CVehicFlpc(item.Key)
                            CVehicFlpc(item.Key) = CSng(Amin(4.0F * CalcVal, AMAX(0.25F * CalcVal, item.Value)))
                        End If
                    Next
                End If

                For Each key In cntx.Regionalization.Keys
                    CVehicFlpc(key) = Amin(1000, AMAX(1, CVehicFlpc(key)))
                Next

                Me.processed = True
            End Sub

            <ExportsAsset("VEHICFLPC")>
            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Me.PreProcess(cntx)

                Dim series As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim weightSeries As IDictionary(Of String, Single?)
                weightSeries = CPOP
                Dim MinVal As Single, MaxVal As Single, bLimits As Boolean = False
                Dim bSum As Boolean = True

                Select Case seriesName
                    Case "CARS_Mini"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CCarsType(key)(1))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "CARS_Small"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CCarsType(key)(2))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "CARS_LowMed"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CCarsType(key)(3))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "CARS_Interm"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CCarsType(key)(4))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "CARS_UpMed"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CCarsType(key)(5))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "CARS_Large"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CCarsType(key)(6))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "CARS_High"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CCarsType(key)(7))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "CARS_LFamWag"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CCarsType(key)(8))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "CARS_SFamWag"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CCarsType(key)(9))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "CARS_SportL"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CCarsType(key)(10))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "CARS_SportH"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CCarsType(key)(11))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "TRUCKS_PCD"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CTrucksType(key)(1))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "TRUCKS_SPickup"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CTrucksType(key)(2))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "TRUCKS_SVan"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CTrucksType(key)(3))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "TRUCKS_MPickup"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CTrucksType(key)(4))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "TRUCKS_LPickup"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CTrucksType(key)(5))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "TRUCKS_SUtility"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CTrucksType(key)(6))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "TRUCKS_MUtility"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CTrucksType(key)(7))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "TRUCKS_LUtility"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CTrucksType(key)(8))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "TRUCKS_MVan"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CTrucksType(key)(9))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "TRUCKS_LVan"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CTrucksType(key)(10))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "TRUCKS_LightComl"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CTrucksType(key)(11))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "TRUCKS_MedHeavy"
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, CTrucksType(key)(12))
                        Next
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "CARSTOT"
                        series = CCarsTotal
                        bLimits = True
                        MinVal = 0
                        MaxVal = 10000000
                    Case "TRUCKSTOT"
                        series = CTrucksTotal
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case "VEHICLESTOT"
                        series = CVehicFlpc
                        MinVal = 0
                        MaxVal = 1000

                        Dim res2 = (From s In series
                                    Join r In cntx.Regionalization On s.Key Equals r.Key
                                    Join w In weightSeries On s.Key Equals w.Key
                                    Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, MinVal, MaxVal)
                           )
                        cntx.Store("VEHICFLPC", res2)

                        series = CVehiclesTotal
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000
                    Case Else
                        'Throw New ArgumentException($"Aggregator cannot be applied on series {seriesName}.")
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
                        Dim res = (From s In series
                                   Join r In cntx.Regionalization On s.Key Equals r.Key
                                   Join w In weightSeries On s.Key Equals w.Key
                                   Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, MinVal, MaxVal)
                           )
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

    End Module
End Namespace
