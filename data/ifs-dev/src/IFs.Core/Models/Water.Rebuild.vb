Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data

Namespace Models
    Partial Public Module Water

        '<RebuildsVariable("ENVPM2PT5", AggregatorType:=GetType(WATAggregator))>
        '<ImportsAsset("CGDPPCP")>

        <RebuildsVariable("WATDEMAND", AggregatorType:=GetType(WATAggregator))>
        <RebuildsVariable("WATRESTOTALRENEW", AggregatorType:=GetType(WATAggregator))>
        <RebuildsVariable("WATRESEXPLOITRENEW", AggregatorType:=GetType(WATAggregator))>
        <RebuildsVariable("WATERWITHDRAWAL", AggregatorType:=GetType(WATAggregator))>
        <RebuildsVariable("WASTEWATERSUPPLY", AggregatorType:=GetType(WATAggregator))>
        <RebuildsVariable("WATERDESALINATED", AggregatorType:=GetType(WATAggregator))>
        <RebuildsVariable("LANDIRAREAACTUAL", AggregatorType:=GetType(WATAggregator))>
        <RebuildsVariable("WATERRESFOSSIL", AggregatorType:=GetType(WATAggregator))>
        <RebuildsVariable("WATERTOTALSUPPLY", AggregatorType:=GetType(WATAggregator))>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        <ImportsAsset("CPOPURBAN")>
        <ImportsAsset("CLandIrAreaEquip")>
        <ImportsAsset("ValAddManu")>
        <ImportsAsset("CLandArea")>
        <ImportsAsset("CWATSAFE3")>
        Public Iterator Function RebuildWater(context As IRebuildContext) As IEnumerable(Of IDataframe)
            'Yield context.Query("ENVPM2PT5").Filter(BaseYearOnly).Execute()

            Yield context.Query("WATDEMAND").Filter(BaseYearOnly).Execute()
            Dim dftmp As IDataframe
            dftmp = context.Query("WATRESTOTALRENEW").Filter(BaseYearOnly).Execute()
            Dim dfWATRESTOTALRENEW As Dataframe = Dataframe.Create("WATRESTOTALRENEW", context, context.FirstYear, context.LastYear, 0)
            Dim R, e As Short
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    For e = 1 To 2
                        dfWATRESTOTALRENEW(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), e, 2)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), e, 2))
                    Next
                Next
            Next
            Yield dfWATRESTOTALRENEW

            dftmp = context.Query("WATRESEXPLOITRENEW").Filter(BaseYearOnly).Execute()
            Dim dfWATRESEXPLOITRENEW As Dataframe = Dataframe.Create("WATRESEXPLOITRENEW", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    For e = 1 To 2
                        dfWATRESEXPLOITRENEW(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), e, 2)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), e, 2))
                    Next
                Next
            Next
            Yield dfWATRESEXPLOITRENEW

            Yield context.Query("WATERWITHDRAWAL").Filter(BaseYearOnly).Execute()
            Yield context.Query("WASTEWATERSUPPLY").Filter(BaseYearOnly).Execute()
            Yield context.Query("WATERDESALINATED").Filter(BaseYearOnly).Execute()
            Yield context.Query("LANDIRAREAACTUAL").Filter(BaseYearOnly).Execute()
            Yield context.Query("WATERRESFOSSIL").Filter(BaseYearOnly).Execute()
            Yield context.Query("WATERTOTALSUPPLY").Filter(BaseYearOnly).Execute()



        End Function

        Class WATAggregator
            Inherits DefaultSumAggregator
            'JAM 2019-03-09
            Private Property processed As Boolean

            Private Property GDPPCP As IDictionary(Of String, Single?)
            Private Property weightSeries As IDictionary(Of String, Single?)
            Private Property POPURBAN As IDictionary(Of String, Single?)
            Private Property LandIrAreaEquip As IDictionary(Of String, Single?)
            Private Property ValAddManu As IDictionary(Of String, Single?)
            Private Property LANDAREA As IDictionary(Of String, Single?)

            Private Property CWatWithdTotalSector As IDictionary(Of String, Single?)
            Private Property CWatWithdMun As IDictionary(Of String, Single?)
            Private Property CWatWithdInd As IDictionary(Of String, Single?)
            Private Property CWatWithdAg As IDictionary(Of String, Single?)
            Private Property CLANDIRAREAACTUAL As IDictionary(Of String, Single?)

            Private Property CWaterWithdrawalSurfaceGR As IDictionary(Of String, Single?)
            Private Property CWaterWithdrawalGroundGR As IDictionary(Of String, Single?)
            Private Property CWaterWithdrawalFossilGroundGR As IDictionary(Of String, Single?)
            Private Property CWatResTotalRenewSurface As IDictionary(Of String, Single?)
            Private Property CWatResTotalRenewGround As IDictionary(Of String, Single?)
            Private Property CWatResExploitRenewSurface As IDictionary(Of String, Single?)
            Private Property CWatResExploitRenewGround As IDictionary(Of String, Single?)
			Private Property CWatWithDSurface As IDictionary(Of String, Single?)
			Private Property CWatWithDGround As IDictionary(Of String, Single?)
			Private Property CWatWithDTotalSources As IDictionary(Of String, Single?)
			Private Property CWatWithDFossilGround As IDictionary(Of String, Single?)
			Private Property CWATERRESFOSSIL As IDictionary(Of String, Single?)
			Private Property CWaterResGroundAfrica As IDictionary(Of String, Single?)
			Private Property CWastewaterProduced As IDictionary(Of String, Single?)
			Private Property CWastewaterTreated As IDictionary(Of String, Single?)
			Private Property CWastewaterTreatedReused As IDictionary(Of String, Single?)
			Private Property CWaterResTotalExploit As IDictionary(Of String, Single?)
			Private Property CWaterResTotalRenew As IDictionary(Of String, Single?)
			Private Property Overlap  As IDictionary(Of String, Single?)
            Private Property CWATERDESALINATED As IDictionary(Of String, Single?)
            Private Property CWATSAFE As IDictionary(Of String, Single?)
            Private Property CWaterResTotalSurfaceGround As IDictionary(Of String, Single?)
            Private Property CTreatedPortionProduced As IDictionary(Of String, Single?)
            Private Property CWATERTOTALSUPPLY As IDictionary(Of String, Single?)

            Private Property URBANPERCENT As IDictionary(Of String, Single?)
            Private Property WatTotalDemand As Single
            Private Property CGDPPCP1k As IDictionary(Of String, Single?)
            Private Property Prediction As Single
            Private Property SurfacePlusGround As Single
            Private Property TotalDemand As IDictionary(Of String, Single?)
            Private Property totalsupply As Single
            Private Property adjustmentfactor As Single

            Private Sub PreProcess(cntx As IRebuildFacade)
                If Me.processed Then
                    Return
                End If

                Me.weightSeries = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
                Me.GDPPCP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
                Me.POPURBAN = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOPURBAN")
                Me.LandIrAreaEquip = cntx.Fetch(Of IDictionary(Of String, Single?))("CLandIrAreaEquip")
                Me.ValAddManu = cntx.Fetch(Of IDictionary(Of String, Single?))("ValAddManu")
                Me.LANDAREA = cntx.Fetch(Of IDictionary(Of String, Single?))("CLandArea")
                Me.CWATSAFE = cntx.Fetch(Of IDictionary(Of String, Single?))("CWATSAFE3")

                Me.CWatWithdTotalSector = New Dictionary(Of String, Single?)
                Me.CWatWithdMun = New Dictionary(Of String, Single?)
                Me.CWatWithdInd = New Dictionary(Of String, Single?)
                Me.CWatWithdAg = New Dictionary(Of String, Single?)
                Me.CLANDIRAREAACTUAL = New Dictionary(Of String, Single?)

                Me.CWaterWithdrawalSurfaceGR = New Dictionary(Of String, Single?)
                Me.CWaterWithdrawalGroundGR = New Dictionary(Of String, Single?)
                Me.CWaterWithdrawalFossilGroundGR = New Dictionary(Of String, Single?)

                Me.CWatResTotalRenewSurface = New Dictionary(Of String, Single?)
                Me.CWatResTotalRenewGround = New Dictionary(Of String, Single?)
                Me.CWatResExploitRenewSurface = New Dictionary(Of String, Single?)
                Me.CWatResExploitRenewGround = New Dictionary(Of String, Single?)
				
				Me.CWatWithDSurface = New Dictionary(Of String, Single?)
				Me.CWatWithDGround = New Dictionary(Of String, Single?)
				Me.CWatWithDTotalSources = New Dictionary(Of String, Single?)
				Me.CWatWithDFossilGround = New Dictionary(Of String, Single?)
				Me.CWATERRESFOSSIL = New Dictionary(Of String, Single?)
				Me.CWaterResGroundAfrica = New Dictionary(Of String, Single?)
				Me.CWastewaterProduced = New Dictionary(Of String, Single?)
				Me.CWastewaterTreated = New Dictionary(Of String, Single?)
				Me.CWastewaterTreatedReused = New Dictionary(Of String, Single?)
				Me.CWaterResTotalExploit = New Dictionary(Of String, Single?)
				Me.CWaterResTotalRenew = New Dictionary(Of String, Single?)
				Me.Overlap = New Dictionary(Of String, Single?)
                Me.CWATERDESALINATED = New Dictionary(Of String, Single?)
                Me.CGDPPCP1k = New Dictionary(Of String, Single?)
                Me.CWaterResTotalSurfaceGround = New Dictionary(Of String, Single?)
                Me.URBANPERCENT = New Dictionary(Of String, Single?)
                Me.CTreatedPortionProduced = New Dictionary(Of String, Single?)
                Me.TotalDemand = New Dictionary(Of String, Single?)
                Me.CWATERTOTALSUPPLY = New Dictionary(Of String, Single?)



                ''''''''''''''''''''''''''''''''''''''''''''''''''
                '''JAM 2019-03-09
                For Each item In (From s In cntx.GetSlice("SeriesWaterTotalWithdSector", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09

                    CWatWithdTotalSector.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWaterwithdMunicipal", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWatWithdMun.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWaterwithdIndustrial", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWatWithdInd.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWaterwithdAgriculture", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWatWithdAg.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesLandIrActualEquip", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CLANDIRAREAACTUAL.Add(item.Key, item.Value)
                    'jrs 2022/06/07
                    If CLANDIRAREAACTUAL(item.Key).HasValue Then
                        CLANDIRAREAACTUAL(item.Key) = Amin(CLANDIRAREAACTUAL(item.Key), 100)
                    End If
                Next

                'JAM 2019-05-06
                'For Each item In (From s In cntx.GetSlice("SeriesWaterSurfaceWithD", cntx.FirstYear, MyBase.Policy)
                '                  Select Key = s.Key, Value = If(s.Value, 0))
                '    CWatWithDSurface.Add(item.Key, item.Value)
                'Next

                'For Each item In (From s In cntx.GetSlice("SeriesWaterGroundWithD", cntx.FirstYear, MyBase.Policy)
                '                  Select Key = s.Key, Value = If(s.Value, 0))
                '    CWaterWithdrawalGroundGR.Add(item.Key, item.Value)
                'Next

                'For Each item In (From s In cntx.GetSlice("SeriesWaterFossilWithdrawal", cntx.FirstYear, MyBase.Policy)
                '                  Select Key = s.Key, Value = If(s.Value, 0))
                '    CWaterWithdrawalFossilGroundGR.Add(item.Key, item.Value)
                'Next
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '''
                For Each item In (From s In cntx.GetSlice("SeriesWaterResTotalRenewSurface", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWatResTotalRenewSurface.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWaterResTotalRenewGround", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWatResTotalRenewGround.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWaterResExploitSurface", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWatResExploitRenewSurface.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWaterResExploitGround", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWatResExploitRenewGround.Add(item.Key, item.Value)
                Next

                '''''Code 3''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                For Each item In (From s In cntx.GetSlice("SeriesWaterSurfaceWithD", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWatWithDSurface.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWaterGroundWithD", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWatWithDGround.Add(item.Key, item.Value)
                Next


                For Each item In (From s In cntx.GetSlice("SeriesWaterTotalWithdSources", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWatWithDTotalSources.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWaterFossilWithdrawal", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWatWithDFossilGround.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWaterResFossil", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWATERRESFOSSIL.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWaterResGroundAfrica", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value)  '''JAM 2019-05-09
                    CWaterResGroundAfrica.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWastewaterProduced", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWastewaterProduced.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWasterwaterTreated", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWastewaterTreated.Add(item.Key, item.Value)
                Next


                For Each item In (From s In cntx.GetSlice("SeriesWastewaterTreatedReused", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWastewaterTreatedReused.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWaterResTotalExploit", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value) '''JAM 2019-05-09
                    CWaterResTotalExploit.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWaterResTotalRenew", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value)  '''JAM 2019-05-09
                    CWaterResTotalRenew.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWaterResOverlap", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value)  '''JAM 2019-05-09
                    Overlap.Add(item.Key, item.Value)
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWaterDesalinated", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value)  '''JAM 2019-05-09
                    CWATERDESALINATED.Add(item.Key, item.Value)
                Next


                'Call AnalFunc("MunDemandPC (Linear) versus GDP2011PCPPP(MOSTRECENT) (Log) AND Connections",
                '    Array(CGDPPCP(ICount%), UrbanPercent, WATSAFE(ICount%, 3)), Prediction)


                Dim xytabl As SeriesEstimator = cntx.GetRegression("MunDemandPC (Linear) versus GDP2011PCPPP(MOSTRECENT) (Log) AND Connections").
                            Normalize("GDP2011PCPPP(MOSTRECENT) (Log)", "PopulationUrban%(MOSTRECENT) (Log)", "WSSJMPWaterTotal%Piped(MOSTRECENT) (Log)").ToEstimator(GDPPCP, URBANPERCENT, CWATSAFE)


                'Call xytabl("WaterWithdIndustrial(MOSTRECENT minus elec) (Linear) versus Man (Linear)", ValAdd(ICount%, 4),
                '    Prediction)
                Dim xytabl2 As SeriesEstimator = cntx.GetRegression("WaterWithdIndustrial(MOSTRECENT minus elec) (Linear) versus Man (Linear)").ToEstimator(ValAddManu)


                'Call xytabl("WastewaterProduced v MunicipalDemand", WatWithdMun(ICount%),
                '    WastewaterProduced(ICount%))
                Dim xytabl3 As SeriesEstimator = cntx.GetRegression("WastewaterProduced v MunicipalDemand").ToEstimator(CWatWithdMun)


                'Call xytabl("WastewaterTreated pct of produced (Linear) versus GDP2011PCPPP(MOSTRECENT) (Log)",
                '    CGDPPCP(ICount%) * 1000, TreatedPortionProduced(ICount%))

                'For Each item In (From s In cntx.GetSlice("CGDPPCP", cntx.BaseYear, MyBase.Policy)
                '                   Select Key = s.Key, Value = If(s.Value, 0))
                '    CGDPPCP1k.Add(item.Key, item.Value * 1000)
                'Next
                For Each item In GDPPCP
                    CGDPPCP1k.Add(item.Key, item.Value * 1000)
                Next

                Dim xytabl4 As SeriesEstimator = cntx.GetRegression("WastewaterTreated pct of produced (Linear) versus GDP2011PCPPP(MOSTRECENT) (Log)").ToEstimator(CGDPPCP1k)


                Dim yrsWaterSurfaceWithD As Short() = cntx.GetYears("SeriesWaterSurfaceWithD").OrderByDescending(Function(y) y).ToArray()
                Dim diceWaterSurfaceWithD As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesWaterSurfaceWithD", yrsWaterSurfaceWithD)

                Dim yrsWaterGroundWithD As Short() = cntx.GetYears("SeriesWaterGroundWithD").OrderByDescending(Function(y) y).ToArray()
                Dim diceWaterGroundWithD As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesWaterGroundWithD", yrsWaterGroundWithD)

                Dim yrsWaterFossilWithdrawal As Short() = cntx.GetYears("SeriesWaterFossilWithdrawal").OrderByDescending(Function(y) y).ToArray()
                Dim diceWaterFossilWithdrawal As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesWaterFossilWithdrawal", yrsWaterFossilWithdrawal)


                'Call XYTABL("WaterResTotalRenew(MOSTRECENT) (Linear) versus LandTotal(MOSTRECENT) (Linear)", _
                '	LANDAREA(ICount%), WaterResTotalRenew(ICount%))


                Dim xytabl5 As SeriesEstimator = cntx.GetRegression("WaterResTotalRenew(MOSTRECENT) (Linear) versus LandTotal(MOSTRECENT) (Linear)").ToEstimator(LANDAREA)


                For Each key In cntx.Regionalization.Keys

                    'If we have total, municipal, and industrial, we can estimate ag
                    If CWatWithdTotalSector(key).HasValue And CWatWithdMun(key).HasValue And CWatWithdInd(key).HasValue And Not CWatWithdAg(key).HasValue Then
                        CWatWithdAg(key) = CWatWithdTotalSector(key) - CWatWithdMun(key) - CWatWithdInd(key)
                    End If


                    'If we have total, industrial, and ag, we can estimate municipal
                    If CWatWithdTotalSector(key).HasValue And Not CWatWithdMun(key).HasValue And
                        CWatWithdInd(key).HasValue And CWatWithdAg(key).HasValue Then
                        CWatWithdMun(key) = CWatWithdTotalSector(key) - CWatWithdInd(key) - CWatWithdAg(key)
                    End If
                    'If we have total, municipal, and ag, can estimate industrial
                    If CWatWithdTotalSector(key).HasValue And CWatWithdMun(key).HasValue And
                        Not CWatWithdInd(key).HasValue And CWatWithdAg(key).HasValue Then
                        CWatWithdInd(key) = CWatWithdTotalSector(key) - CWatWithdMun(key) - CWatWithdAg(key)
                    End If
                    'If we have municipal, industrial, and ag, we can estimate total
                    If Not CWatWithdTotalSector(key).HasValue And CWatWithdMun(key).HasValue And
                        CWatWithdInd(key).HasValue And CWatWithdAg(key).HasValue Then
                        CWatWithdTotalSector(key) = CWatWithdInd(key) + CWatWithdMun(key) + CWatWithdAg(key)
                    End If


                    If Not CWatWithdMun(key).HasValue Then


                        URBANPERCENT(key) = POPURBAN(key) / weightSeries(key)

                        Prediction = CSng(xytabl(key))
                        CWatWithdMun(key) = Prediction * POPURBAN(key)

                    End If

                    'JAM 2019-05-10
                    If Not CWatWithdInd(key).HasValue Then

                        Prediction = CSng(xytabl2(key))

                        CWatWithdInd(key) = Prediction

                    End If


                    'JAM 2019-05-09
                    If Not CLANDIRAREAACTUAL(key).HasValue Then
                        CLANDIRAREAACTUAL(key) = CSng(90)
                    End If
                    CLANDIRAREAACTUAL(key) = CLANDIRAREAACTUAL(key) * (CSng(LandIrAreaEquip(key) / 100))
                    If Not CLANDIRAREAACTUAL(key).HasValue Then CLANDIRAREAACTUAL(key) = CSng(0.001)


                    If Not CWatWithdAg(key).HasValue Then
                        CWatWithdAg(key) = 0
                    End If


                    WatTotalDemand = CSng(CWatWithdMun(key)) + CSng(CWatWithdInd(key)) + CSng(CWatWithdAg(key))
                    If CWatWithdTotalSector(key) <> WatTotalDemand And CWatWithdTotalSector(key).HasValue Then
                        CWatWithdMun(key) = (CWatWithdMun(key) / WatTotalDemand) * CWatWithdTotalSector(key)
                        CWatWithdInd(key) = (CWatWithdInd(key) / WatTotalDemand) * CWatWithdTotalSector(key)
                        CWatWithdAg(key) = (CWatWithdAg(key) / WatTotalDemand) * CWatWithdTotalSector(key)
                    End If






                    CWaterWithdrawalSurfaceGR(key) = getAnnualGrowthRate(diceWaterSurfaceWithD, yrsWaterSurfaceWithD, key, cntx.BaseYear)
                    CWaterWithdrawalGroundGR(key) = getAnnualGrowthRate(diceWaterGroundWithD, yrsWaterGroundWithD, key, cntx.BaseYear)
                    CWaterWithdrawalFossilGroundGR(key) = getAnnualGrowthRate(diceWaterFossilWithdrawal, yrsWaterFossilWithdrawal, key, cntx.BaseYear)


                    If Not CWaterWithdrawalSurfaceGR(key).HasValue Then CWaterWithdrawalSurfaceGR(key) = 0
                    If Not CWaterWithdrawalGroundGR(key).HasValue Then CWaterWithdrawalGroundGR(key) = 0
                    If Not CWaterWithdrawalFossilGroundGR(key).HasValue Then CWaterWithdrawalFossilGroundGR(key) = 0

                    '''''Code 3'''
                    If Not CWatWithDFossilGround(key).HasValue Then CWatWithDFossilGround(key) = 0

                    If Not CWastewaterProduced(key).HasValue Then CWastewaterProduced(key) = 0

                    If Not CWastewaterTreated(key).HasValue Then CWastewaterTreated(key) = 0

                    If Not CWastewaterTreatedReused(key).HasValue Then CWastewaterTreatedReused(key) = 0

                    If Not Overlap(key).HasValue Then Overlap(key) = 0

                    '----Total Renewable Water Resources (TRWR)------
                    'Fill in data holes for total renewable and exploitable renewable. Since Ground + Surface = Total
                    'if we have 2 of the three we can calculate the third.

                    'If we have surface and total, estimate ground
                    If CWatResTotalRenewSurface(key).HasValue And Not CWatResTotalRenewGround(key).HasValue And CWaterResTotalRenew(key).HasValue Then
                        CWatResTotalRenewGround(key) = CWaterResTotalRenew(key) - CWatResTotalRenewSurface(key)
                    End If

                    'If we have total and ground, estimate surface
                    If CWatResTotalRenewGround(key).HasValue And Not CWatResTotalRenewSurface(key).HasValue And CWaterResTotalRenew(key).HasValue Then
                        CWatResTotalRenewSurface(key) = CWaterResTotalRenew(key) - CWatResTotalRenewGround(key)
                    End If

                    'If we have surface and ground, estimate total
                    If CWatResTotalRenewGround(key).HasValue And CWatResTotalRenewSurface(key).HasValue And Not CWaterResTotalRenew(key).HasValue Then
                        CWaterResTotalRenew(key) = CWatResTotalRenewGround(key) + CWatResTotalRenewSurface(key)
                    End If

                    'Now, either we have no data for total, surface, or ground, or we only have data for one of them.
                    'If we have no data for any then we estimate total renewable water resources (TRWR) using land area

                    'If we have no data for any 3 of them
                    If Not CWaterResTotalRenew(key).HasValue And Not CWatResTotalRenewGround(key).HasValue And
                        Not CWatResTotalRenewSurface(key).HasValue Then



                        CWaterResTotalRenew(key) = xytabl5(key)

                        CWatResTotalRenewGround(key) = CSng(0.29 * CWaterResTotalRenew(key))
                        CWatResTotalRenewSurface(key) = CSng(0.71 * CWaterResTotalRenew(key))
                    End If

                    'If we only have data for total:
                    If CWaterResTotalRenew(key).HasValue And Not CWatResTotalRenewGround(key).HasValue And
                        Not CWatResTotalRenewSurface(key).HasValue Then
                        CWatResTotalRenewGround(key) = CSng(0.29 * CWaterResTotalRenew(key))
                        CWatResTotalRenewSurface(key) = CSng(0.71 * CWaterResTotalRenew(key))
                    End If


                    'If we only have data for surface:
                    If Not CWaterResTotalRenew(key).HasValue And Not CWatResTotalRenewGround(key).HasValue And
                        CWatResTotalRenewSurface(key).HasValue Then
                        CWaterResTotalRenew(key) = CSng(CWatResTotalRenewSurface(key) / 0.71)
                        CWatResTotalRenewGround(key) = CSng(CWaterResTotalRenew(key) * 0.29)
                    End If



                    'If we only have data for ground:
                    If Not CWaterResTotalRenew(key).HasValue And CWatResTotalRenewGround(key).HasValue And
                        Not CWatResTotalRenewSurface(key).HasValue Then
                        CWaterResTotalRenew(key) = CSng(CWatResTotalRenewGround(key) / 0.29)
                        CWatResTotalRenewSurface(key) = CSng(CWaterResTotalRenew(key) * 0.71)
                    End If



                    'Subtract overlap from total renewable and exploitable renewable and recalculate supply
                    'as portion of total - overlap
                    CWaterResTotalSurfaceGround(key) = CWatResTotalRenewSurface(key) + CWatResTotalRenewGround(key)

                    'jrs 2022/06/07 added and WaterResTotalSurfaceGround(key)>0 Bahamas has 0s now
                    If CWatResTotalRenewSurface(key).HasValue And CWatResTotalRenewGround(key).HasValue AndAlso CWaterResTotalSurfaceGround(key) > 0 Then
                        'If CountryName$(ICount%) = "China" Then Stop
                        CWatResTotalRenewSurface(key) = (CWatResTotalRenewSurface(key) / CWaterResTotalSurfaceGround(key)) * (CWaterResTotalSurfaceGround(key) - Overlap(key))

                        CWatResTotalRenewGround(key) = (CWatResTotalRenewGround(key) / CWaterResTotalSurfaceGround(key)) * (CWaterResTotalSurfaceGround(key) - Overlap(key))
                        'If CountryName$(ICount%) = "China" Then Stop
                    End If



                    'Make sure the three reconcile, could be the original data doesn't reconcile
                    CWaterResTotalRenew(key) = CWatResTotalRenewGround(key) + CWatResTotalRenewSurface(key)

                    '-------Exploitable Renewable Water Resources-----

                    'If we have data for total renewable water resources (TRWR) but not TRWR surface and ground
                    'then we estimate based on the total using global averages. If we do not have data for exploitable
                    'surface and ground water resources we estimate using TRWR surface and ground resources.

                    'First check to see if we have withdrawals. We don't want to estimate exploitable below withdrawals.
                    'If CountryName$(ICount%) = "Kuwait" Then Stop
                    '    If Not IsNull(WatWithDSurface(ICount%)) And IsNull(WatResExploitRenewSurface(ICount%)) Then
                    '        WatResExploitRenewSurface(ICount%) = 2 * WatWithDSurface(ICount%)
                    '    End If
                    '
                    '    If Not IsNull(WatWithDGround(ICount%)) And IsNull(WatResExploitRenewGround(ICount%)) Then
                    '        WatResExploitRenewGround(ICount%) = 2 * WatWithDGround(ICount%)
                    '    End If
                    '
                    'If we have total and surface, estimate ground
                    If CWaterResTotalExploit(key).HasValue And CWatResExploitRenewSurface(key).HasValue And
                        Not CWatResExploitRenewGround(key).HasValue Then
                        CWatResExploitRenewGround(key) = CWaterResTotalExploit(key) - CWatResExploitRenewSurface(key)
                    End If

                    'If we have total and ground, estimate surface
                    If CWaterResTotalExploit(key).HasValue And Not CWatResExploitRenewSurface(key).HasValue And
                        CWatResExploitRenewGround(key).HasValue Then
                        CWatResExploitRenewSurface(key) = CWaterResTotalExploit(key) - CWatResExploitRenewGround(key)
                    End If


                    '	If we have ground and surface, estimate total
                    If Not CWaterResTotalExploit(key).HasValue And CWatResExploitRenewSurface(key).HasValue And
                        CWatResExploitRenewGround(key).HasValue Then
                        CWaterResTotalExploit(key) = CWatResExploitRenewSurface(key) + CWatResExploitRenewGround(key)
                    End If


                    'If we don't have any of the 3
                    If Not CWaterResTotalExploit(key).HasValue And Not CWatResExploitRenewSurface(key).HasValue And
                    Not CWatResExploitRenewGround(key).HasValue Then
                        CWatResExploitRenewSurface(key) = CSng(0.36 * CWatResTotalRenewSurface(key))
                        CWatResExploitRenewGround(key) = CSng(0.89 * CWatResTotalRenewGround(key))
                        CWaterResTotalExploit(key) = CWatResExploitRenewSurface(key) + CWatResExploitRenewGround(key)
                        'sgh 2018/01/30 - START
                        If CWaterResTotalExploit(key) < WatTotalDemand Then
                            CWaterResTotalExploit(key) = WatTotalDemand
                            CWatResExploitRenewSurface(key) = CWaterResTotalExploit(key) / 2
                            CWatResExploitRenewGround(key) = CWaterResTotalExploit(key) / 2
                        End If
                        If CWatResExploitRenewSurface(key) > CWatResTotalRenewSurface(key) Then
                            CWatResExploitRenewSurface(key) = CWatResTotalRenewSurface(key)
                        End If
                        If CWatResExploitRenewGround(key) > CWatResTotalRenewGround(key) Then
                            CWatResExploitRenewGround(key) = CWatResTotalRenewGround(key)
                        End If
                        'sgh 2018/01/30 - END
                    End If

                    'If we only have total use the 36 and 89 percent of total estimates but then normalize to the
                    'total exploitable data
                    If CWaterResTotalExploit(key).HasValue And Not CWatResExploitRenewSurface(key).HasValue And
                       Not CWatResExploitRenewGround(key).HasValue Then
                        CWatResExploitRenewSurface(key) = CSng(0.36 * CWatResTotalRenewSurface(key))
                        CWatResExploitRenewGround(key) = CSng(0.89 * CWatResTotalRenewGround(key))
                        SurfacePlusGround = CSng(CWatResExploitRenewSurface(key)) + CSng(CWatResExploitRenewGround(key))
                        CWatResExploitRenewSurface(key) = (CWatResExploitRenewSurface(key) / SurfacePlusGround) * CWaterResTotalExploit(key)
                        CWatResExploitRenewGround(key) = (CWatResExploitRenewGround(key) / SurfacePlusGround) * CWaterResTotalExploit(key)
                    End If

                    'If we only have surface
                    If Not CWaterResTotalExploit(key).HasValue And CWatResExploitRenewSurface(key).HasValue And
                       Not CWatResExploitRenewGround(key).HasValue Then
                        CWatResExploitRenewGround(key) = CSng(0.89 * CWatResTotalRenewGround(key))
                        CWaterResTotalExploit(key) = CWatResExploitRenewSurface(key) + CWatResExploitRenewGround(key)
                    End If

                    'If we only have ground
                    If Not CWaterResTotalExploit(key).HasValue And Not CWatResExploitRenewSurface(key).HasValue And
                       CWatResExploitRenewGround(key).HasValue Then
                        CWatResExploitRenewSurface(key) = CSng(0.36 * CWatResTotalRenewSurface(key))
                        CWaterResTotalExploit(key) = CWatResExploitRenewSurface(key) + CWatResExploitRenewGround(key)
                    End If

                    'Make sure they all reconcile
                    CWaterResTotalExploit(key) = CWatResExploitRenewSurface(key) + CWatResExploitRenewGround(key)
                    '    If WaterResTotalExploit(ICount%) > WaterResTotalRenew(ICount%) Then Stop
                    '    If IsNull(WatResExploitRenewGround(ICount%)) Then
                    '        WatResExploitRenewGround(ICount%) = 0.89 * WatResTotalRenewGround(ICount%)
                    '    End If
                    '
                    '    If IsNull(WatResExploitRenewSurface(ICount%)) Then
                    '        WatResExploitRenewSurface(ICount%) = 0.36 * WatResTotalRenewSurface(ICount%)
                    '    End If
                    'sgh 2014/03/02

                    'sgh 2014/03/11
                    'If we estimated exploitable resources using TRWR but we have data for total exploitable
                    'then we reconcile with the data.
                    '    If Not (WatResExploitRenewSurface(ICount%) + WatResExploitRenewGround(ICount%) = _
                    '    WaterResTotalExploit(ICount%)) Then
                    '        TotalExploit = WatResExploitRenewSurface(ICount%) + WatResExploitRenewGround(ICount%)
                    '        SurfacePortion = WatResExploitRenewSurface(ICount%) / TotalExploit
                    '        GroundPortion = 1 - SurfacePortion
                    '        WatResExploitRenewSurface(ICount%) = SurfacePortion * WaterResTotalExploit(ICount%)
                    '        WatResExploitRenewGround(ICount%) = GroundPortion * WaterResTotalExploit(ICount%)
                    '    End If
                    'sgh 2014/03/11

                    'sgh 2014/03/11
                    'fill in total exploitable and total renewable with the newly calculated surface and groundwater
                    'constituent parts
                    '    If WaterResTotalExploit(ICount%) = 0 Then
                    '        WaterResTotalExploit(ICount%) = WatResExploitRenewSurface(ICount%) + _
                    '        WatResExploitRenewGround(ICount%)
                    '    End If
                    '
                    '    If WaterResTotalRenew(ICount%) = 0 Then
                    '        WaterResTotalRenew(ICount%) = WatResTotalRenewSurface(ICount%) + _
                    '        WatResTotalRenewGround(ICount%)
                    '    End If
                    'sgh 2014/03/11

                    'sgh 2014/03/11
                    'Aquastat includes fossil water withdrawal in their groundwater withdrawal data so we must subtract it out
                    '    WatWithDGround(ICount%) = WatWithDGround(ICount%) - WatWithDFossilGround(ICount%)
                    'sgh 2014/03/11

                    'sgh 2014/05/08
                    'If CountryName$(ICount%) = "Yemen, Republic of" Then Stop
                    '    If Not IsNull(WaterResGroundAfrica(ICount%)) Then
                    '        WATERRESFOSSIL(ICount%) = WaterResGroundAfrica(ICount%) - WatResTotalRenewGround(ICount%)
                    '    End If
                    '
                    '    If IsNull(WATERRESFOSSIL(ICount%)) Then
                    '        If Not (WatWithDFossilGround(ICount%) = 0) Then
                    '            WATERRESFOSSIL(ICount%) = 10 * WatWithDFossilGround(ICount%)
                    '        Else
                    '        WATERRESFOSSIL(ICount%) = 0
                    '        End If
                    '    End If
                    'If CountryName$(ICount%) = "Yemen, Republic of" Then Stop
                    'sgh 2014/05/08

                    'sgh 2014/03/11
                    'Since water withdrawal from surface water plus water withdrawal from groundwater equals total water withdrawal
                    'if we have 2 of these data points we can fill in the third.
                    '    If Not (WatWithDTotalSources(ICount%) = 0) And Not (WatWithDGround(ICount%) = 0) And _
                    '    (WatWithDSurface(ICount%) = 0) Then
                    '        WatWithDSurface(ICount%) = WatWithDTotalSources(ICount%) - WatWithDGround(ICount%)
                    '    End If
                    '
                    '    If Not (WatWithDTotalSources(ICount%) = 0) And Not (WatWithDSurface(ICount%) = 0) And _
                    '    (WatWithDGround(ICount%) = 0) Then
                    '        WatWithDGround(ICount%) = WatWithDTotalSources(ICount%) - WatWithDSurface(ICount%)
                    '    End If
                    'If CountryName$(ICount%) = "Nicaragua" Then Stop
                    'If we have total and ground but not surface
                    If CWatWithDTotalSources(key).HasValue And CWatWithDGround(key).HasValue And
                       Not CWatWithDSurface(key).HasValue Then
                        CWatWithDSurface(key) = CWatWithDTotalSources(key) - CWatWithDGround(key)
                    End If

                    'If we have total and surface but not ground
                    If CWatWithDTotalSources(key).HasValue And CWatWithDSurface(key).HasValue And
                       Not CWatWithDGround(key).HasValue Then
                        CWatWithDGround(key) = CWatWithDTotalSources(key) - CWatWithDSurface(key)
                    End If

                    'If we have ground and surface but not total
                    If Not CWatWithDTotalSources(key).HasValue And CWatWithDSurface(key).HasValue And
                       CWatWithDGround(key).HasValue Then
                        CWatWithDTotalSources(key) = CWatWithDSurface(key) + CWatWithDGround(key)
                    End If
                    'sgh 2014/03/11
                    'If CountryName$(ICount%) = "Nicaragua" Then Stop
                    'sgh 2014/03/11
                    'if we do not have water withdrawal data by source then we estimate either using the total water withdrawal
                    'or the total demand

                    '    If IsNull(WatWithDSurface(ICount%)) And Not IsNull(WatResExploitRenewSurface(ICount%)) Then
                    '        WatWithDSurface(ICount%) = WatResExploitRenewSurface(ICount%) / 2
                    '    End If
                    '
                    '    If IsNull(WatWithDGround(ICount%)) And Not IsNull(WatResExploitRenewGround(ICount%)) Then
                    '        WatWithDGround(ICount%) = WatResExploitRenewGround(ICount%) / 2
                    '    End If

                    'If we have total but not surface or ground
                    If CWatWithDTotalSources(key).HasValue And Not CWatWithDSurface(key).HasValue And
                       Not CWatWithDGround(key).HasValue Then
                        CWatWithDSurface(key) = CSng(0.67 * CWatWithDTotalSources(key))
                        CWatWithDGround(key) = CSng(0.33 * CWatWithDTotalSources(key))
                    End If

                    'If we have surface but not total or ground
                    If Not CWatWithDTotalSources(key).HasValue And CWatWithDSurface(key).HasValue And
                       Not CWatWithDGround(key).HasValue Then
                        CWatWithDTotalSources(key) = CSng(CWatWithDSurface(key) / 0.67)
                        CWatWithDGround(key) = CSng(0.33 * CWatWithDTotalSources(key))
                    End If

                    'If we have ground but not surface or total
                    If Not CWatWithDTotalSources(key).HasValue And Not CWatWithDSurface(key).HasValue And
                        CWatWithDGround(key).HasValue Then
                        CWatWithDTotalSources(key) = CSng(CWatWithDGround(key) / 0.33)
                        CWatWithDSurface(key) = CSng(0.67 * CWatWithDTotalSources(key))
                    End If

                    If Not CWatWithDTotalSources(key).HasValue And Not CWatWithDSurface(key).HasValue And
                       Not CWatWithDGround(key).HasValue Then
                        TotalDemand(key) = CWatWithdAg(key) + CWatWithdInd(key) + CWatWithdMun(key)
                        CWatWithDSurface(key) = CSng(0.67 * TotalDemand(key))
                        CWatWithDGround(key) = CSng(0.33 * TotalDemand(key))
                        'sgh 2018/01/30 - START
                        'Make sure not to initialize withdrawals higher than exploitable
                        If CWatWithDSurface(key) > CWatResExploitRenewSurface(key) Then
                            CWatWithDSurface(key) = CWatResExploitRenewSurface(key)
                        End If
                        If CWatWithDGround(key) > CWatResExploitRenewGround(key) Then
                            CWatWithDGround(key) = CWatResExploitRenewGround(key)
                        End If
                        'sgh 2018/01/30 - END
                        CWatWithDTotalSources(key) = CWatWithDSurface(key) + CWatWithDGround(key)
                    End If
                    'If CountryName$(ICount%) = "Kuwait" Then Stop
                    'sgh 2014/03/11
                    'If CountryName$(ICount%) = "Nicaragua" Then Stop
                    '------fossil water------
                    'Aquastat includes fossil water withdrawal in their groundwater withdrawal data so we must subtract it out
                    CWatWithDGround(key) = CWatWithDGround(key) - CWatWithDFossilGround(key)
                    'data for fossil water reserves includes renewable, so remove that
                    If CWaterResGroundAfrica(key).HasValue Then
                        CWATERRESFOSSIL(key) = CWaterResGroundAfrica(key) - CWatResTotalRenewGround(key)
                    End If
                    'if we have data for fossil water withdrawals but not reserves, assume reserves are 10 times withdrawals.
                    'If we have no data for either, assume there are no fossil water reserves or withdrawals.
                    If Not CWATERRESFOSSIL(key).HasValue Then
                        If Not (CWatWithDFossilGround(key) = 0) Then
                            'sgh 2018/01/31 - START
                            '            WATERRESFOSSIL(ICount%) = 10 * WatWithDFossilGround(ICount%)
                            CWATERRESFOSSIL(key) = 25 * CWatWithDFossilGround(key)
                            'sgh 2018/01/31 - END
                        Else
                            CWATERRESFOSSIL(key) = 0
                        End If
                    End If
                    'sgh 2014/03/11

                    '-----------wastewater------------
                    'sgh 2014/03/02
                    'If we have data for both wastewater produced and wastewater treated
                    'and the data says that treated > produced we change volume treated
                    'to a portion of produced. This is because a country cannot treat more
                    'wastewater than is produced and is probably from a problem in taking the
                    'most recent data i.e. we are taking different years for treated v. produced
                    If Not (CWastewaterTreated(key) = 0) And Not (CWastewaterProduced(key) = 0) Then
                        If (CWastewaterTreated(key) / CWastewaterProduced(key)) > 1 Then
                            CWastewaterTreated(key) = CSng(0.95 * CWastewaterProduced(key))
                        End If
                    End If
                    'sgh 2014/03/02

                    'sgh 2014/08/26
                    'The same logic that applies to the relationship between treated wastewater
                    'and produced wastewater applies to the relationship between treated wastewater
                    'and treated-and-reused wastewater i.e. if we have data for both and treated
                    'and reused exceeds treated then we know there is a problem with the data and
                    'assume that the country reused 95% of treated wastewater.
                    If Not (CWastewaterTreated(key) = 0) And Not (CWastewaterTreatedReused(key) = 0) Then
                        If (CWastewaterTreatedReused(key) / CWastewaterTreated(key)) > 1 Then
                            CWastewaterTreatedReused(key) = CSng(0.95 * CWastewaterTreated(key))
                        End If
                    End If
                    'sgh 2014/08/26

                    'sgh 2014/03/02
                    'If we do not have data on wastewater produced we estimate using municipal water demand. If we do not have
                    'data on wastewater treated we estimate the portion of wastewater produced that is treated as a function
                    'of GDPPC. If we do not have data for wastewater treated and reused we estimate using a global average
                    'of 66%
                    If (CWastewaterProduced(key) = 0) Then

                        'Call xytabl("WastewaterProduced v MunicipalDemand", WatWithdMun(ICount%),
                        'WastewaterProduced(ICount%))
                        CWastewaterProduced(key) = xytabl3(key)

                        '    ResidWastewaterProduced(R%) = ExpectedWastewaterProduced(R%) - WASTEWATERSUPPLY(R%, 1)
                        '    WastewaterProduced(ICount%) = ExpectedWastewaterProduced(ICount%)
                    End If

                    'sgh 2014/08/26
                    'If treated wastewater is still greater than produced wastewater then that means that
                    'there was data for treated wastewater but not produced wastewater and that the equation
                    'we used to estimate produced wastewater (using Mun demand) was an underestimation. We
                    'correct for this by assuming these countries treat 80% of their produced wastewater.
                    If CWastewaterTreated(key) > CWastewaterProduced(key) Then
                        CWastewaterProduced(key) = CSng(CWastewaterTreated(key) * 1.2)
                        'If CountryName$(ICount%) = "Moldova" Then Stop
                    End If
                    'sgh 2014/08/26


                    'sgh 2014/10/23
                    'Updated for GDP 2011 data
                    If (CWastewaterTreated(key) = 0) Then
                        '        Call XYTABL("WastewaterTreated pct of produced (Linear) versus GDP2005PCPPP(MOSTRECENT) (Log)", _
                        '            CGDPPCP(ICount%) * 1000, TreatedPortionProduced(ICount%))

                        '    Call xytabl("WastewaterTreated pct of produced (Linear) versus GDP2011PCPPP(MOSTRECENT) (Log)",
                        'CGDPPCP(ICount%) * 1000, TreatedPortionProduced(ICount%))

                        CTreatedPortionProduced(key) = xytabl4(key)

                        CWastewaterTreated(key) = CTreatedPortionProduced(key) * CWastewaterProduced(key)
                    End If
                    'sgh 2014/10/23

                    'JAM 2019-05-02
                    'If (CWastewaterTreatedReused(key) = 0) Then
                    If (CWastewaterTreatedReused(key) = 0) And CWastewaterTreated(key).HasValue Then
                        CWastewaterTreatedReused(key) = CSng(0.66 * CWastewaterTreated(key))
                    End If

                    'sgh 2018/04/06 START - Make sure treated and reused wastewater is not too small
                    ' This could be just because we are taking a really old data point for
                    'treated and reused and a newer datapoint for treated
                    'If CountryName$(ICount%) = "Korea, Republic of" Then Stop

                    'JAM 2019-05-06
                    If CWastewaterTreatedReused(key) < CSng(0.1 * CWastewaterTreated(key)) Then
                        CWastewaterTreatedReused(key) = CSng(0.1 * CWastewaterTreated(key))
                    End If
                    '    'sgh 2018/04/06 - END

                    '    'sgh 2014/05/08
                    '    'add secondary water (municipal discharge or non-directly reused treated municipal wastewater to exploitable
                    '    'surface water resources
                    '    'If CountryName$(ICount%) = "South Africa" Then Stop
                    'JAM 2019-05-06
                    CWatResExploitRenewSurface(key) = CWatResExploitRenewSurface(key) + CWastewaterTreated(key) - CWastewaterTreatedReused(key)
                    '    'sgh 2014/05/08

                    'JAM 2019-05-09
                    '    'sgh 2017/01/24
                    If Not CWATERDESALINATED(key).HasValue Then CWATERDESALINATED(key) = CSng(0.001 * (CWatWithDFossilGround(key) + CWatWithDSurface(key) + CWatWithDGround(key) + CWastewaterTreatedReused(key)))
                    '    'WATERTOTALSUPPLY(ICount%) = WatWithDFossilGround(ICount%) + WatResExploitRenewSurface(ICount%) _
                    '    '    + WatResExploitRenewGround(ICount%) + WastewaterTreatedReused(ICount%) + WATERDESALINATED(ICount%)
                    '    'WATERTOTALSUPPLY(ICount%) = WatWithDFossilGround(ICount%) + WatWithDSurface(ICount%) + _
                    '    '    WatWithDGround(ICount%) + WastewaterTreatedReused(ICount%) + WATERDESALINATED(ICount%)
                    '    'If CountryName$(ICount%) = "United States of America" Then Stop

                    '    'If CountryName$(ICount%) = "Brunei" Then Stop

                    ''''''''''''JAM 2019-05-09'''''''''''''''''''''''''''''''''''''''
                    totalsupply = CSng((CWatWithDFossilGround(key)) + CSng(CWatWithDSurface(key)) + CSng(CWatWithDGround(key)) + CSng(CWastewaterTreatedReused(key)) + CSng(CWATERDESALINATED(key)))
                    TotalDemand(key) = CWatWithdMun(key) + CWatWithdInd(key) + CWatWithdAg(key)
                    '    'If CountryName$(ICount%) = "Nicaragua" Then Stop
                    adjustmentfactor = CSng(TotalDemand(key)) - totalsupply
                    'jrs 2022/06/07 added totalsupply > 0 Micronesia is now 0
                    If adjustmentfactor > 0 And adjustmentfactor > CSng(0.2 * TotalDemand(key)) And totalsupply > 0 Then
                        '        'If CountryName$(ICount%) = "Nicaragua" Then Stop

                        CWatWithDFossilGround(key) = (CWatWithDFossilGround(key) / totalsupply) * CSng(TotalDemand(key) * 0.8)
                        CWatWithDSurface(key) = (CWatWithDSurface(key) / totalsupply) * CSng(TotalDemand(key) * 0.8)
                        CWatWithDGround(key) = (CWatWithDGround(key) / totalsupply) * CSng(TotalDemand(key) * 0.8)
                        CWastewaterTreatedReused(key) = (CWastewaterTreatedReused(key) / totalsupply) * CSng(TotalDemand(key) * 0.8)
                        CWATERDESALINATED(key) = (CWATERDESALINATED(key) / totalsupply) * CSng(TotalDemand(key) * 0.8)
                    End If
                    '    'If CountryName$(ICount%) = "Brunei" Then Stop
                    '    'If CountryName$(ICount%) = "Nicaragua" Then Stop
                    CWATERTOTALSUPPLY(key) = 0
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''





                Next




                Me.processed = True
            End Sub

            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Me.PreProcess(cntx)

                Dim series As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                'Dim weightSeries As IDictionary(Of String, Single?)
                'weightSeries = CPOP
                Dim MinVal As Single, MaxVal As Single, bLimits As Boolean = False
                Dim bSum As Boolean = False

                'Dim Vals As IDictionary(Of Integer, Single?)
                'JAM 2019-05-04
                Select Case seriesName
                    Case "WATRESTOTALRENEW_Surface"
                        bLimits = True
                        MinVal = 0.001
                        MaxVal = 100000
                        series = Me.CWatResTotalRenewSurface
                    Case "WATRESTOTALRENEW_Ground"
                        bLimits = True
                        MinVal = 0.001
                        MaxVal = 100000
                        series = Me.CWatResTotalRenewGround
                    Case "WATRESEXPLOITRENEW_Surface"
                        bLimits = True
                        MinVal = 0.001
                        MaxVal = 100000
                        series = Me.CWatResExploitRenewSurface
                    Case "WATRESEXPLOITRENEW_Ground"
                        bLimits = True
                        MinVal = 0.001
                        MaxVal = 100000
                        series = Me.CWatResExploitRenewGround
                    Case "WATDEMAND_Municipal"
                        bLimits = True
                        MinVal = 0.0001
                        MaxVal = 100000
                        series = Me.CWatWithdMun
                    Case "WATDEMAND_Industrial"
                        bLimits = True
                        MinVal = 0.0001
                        MaxVal = 100000
                        series = Me.CWatWithdInd
                    Case "WATDEMAND_Agriculture"
                        bLimits = True
                        MinVal = 0.0001
                        MaxVal = 100000
                        series = Me.CWatWithdAg
                    Case "WATERWITHDRAWAL_Surface"
                        bLimits = True
                        MinVal = 0.0001
                        MaxVal = 100000
                        series = Me.CWatWithDSurface 'JAM 2019-05-06
                    Case "WATERWITHDRAWAL_Ground"
                        bLimits = True
                        MinVal = 0.0001
                        MaxVal = 100000
                        series = Me.CWatWithDGround 'JAM 2019-05-14
                    Case "WATERWITHDRAWAL_FossilGround"
                        bLimits = True
                        MinVal = 0.0001
                        MaxVal = 100000
                        series = Me.CWatWithDFossilGround 'JAM 2019-05-14
                    Case "WASTEWATERSUPPLY_Produced"
                        bLimits = True
                        MinVal = 0.0001
                        MaxVal = 100000
                        series = Me.CWastewaterProduced
                    Case "WASTEWATERSUPPLY_Treated"
                        bLimits = True
                        MinVal = 0.0001
                        MaxVal = 100000
                        series = Me.CWastewaterTreated
                    Case "WASTEWATERSUPPLY_TreatedandReused"
                        bLimits = True
                        MinVal = 0.0001
                        MaxVal = 100000
                        series = Me.CWastewaterTreatedReused
                    Case "WATERRESFOSSIL"
                        bLimits = True
                        MinVal = 0.0001
                        MaxVal = 100000
                        series = Me.CWATERRESFOSSIL
                    Case "WATERTOTALSUPPLY"
                        bLimits = True
                        MinVal = 0.0001
                        MaxVal = 100000
                        series = Me.CWATERTOTALSUPPLY
                    Case "WATERDESALINATED"
                        bLimits = True
                        MinVal = 0.0001
                        MaxVal = 100000
                        series = Me.CWATERDESALINATED
                    Case "LANDIRAREAACTUAL" 'JAM 2019-05-14
                        bLimits = True
                        MinVal = 0.0001
                        MaxVal = 100000
                        series = Me.CLANDIRAREAACTUAL

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
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, MinVal, MaxVal)
                           )
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