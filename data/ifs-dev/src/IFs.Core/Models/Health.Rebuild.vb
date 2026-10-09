Option Strict Off
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data
Imports Microsoft.EntityFrameworkCore
'jrs 2023/05/30
Imports Microsoft.Data.Sqlite

Namespace Models
    Partial Public Module HealthMod

        <RebuildsVariable("HLBMI", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLSMOKING", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLOBESITY", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLDIABIGT", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLDIABPREV", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("hlsmokingtcs", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLSTUNT", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("MALNCHPSAM", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("SAMAVERTED", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("AIDSDTHS", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLSMOKINGIMP", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLDIABETESRR", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("CHILDDTHR", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLMALARIAINC", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLMALARIAPREV", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLMALARIAMORT", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLHIVINC", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLHIVPREV", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLHIVMORT", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLBEDNETS", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLHIVART", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLCONDOMUSE", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("HLSTUNTCHL", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("DEATHTRPV", AggregatorType:=GetType(HealthAggregator))>
        <RebuildsVariable("VEHICFLPC", AggregatorType:=GetType(HealthAggregator))>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        <ImportsAsset("CCLPC_Tot")>
        <ImportsAsset("CSANITATION1")>
        <ImportsAsset("CEDYRSAGE25T")>
        <ImportsAsset("CGovCorrupt")>
        <ImportsAsset("CSFINTLWARMAG")>
        <ImportsAsset("CCBR")>
        <ImportsAsset("LIFEXP_Male")>
        <ImportsAsset("LIFEXP_Female")>
        <ImportsAsset("POP")>
        <ImportsAsset("CBR")>
        <ImportsAsset("AIDSDTHStmp")>
        <ImportsAsset("HIVRATE")>
        <ImportsAsset("VEHICFLPC")>
        <ImportsAsset("INFMOR")>
        <ImportsAsset("CEDSECPERF")>
        Public Iterator Function RebuildHealth(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Yield context.Query("HLOBESITY").Filter(BaseYearOnly).Execute()
            Yield context.Query("HLBMI").Filter(BaseYearOnly).Execute()
            Yield context.Query("HLSMOKING").Filter(BaseYearOnly).Execute()

            'jrs 2019/11/26
            Dim dftmp As IDataframe
            dftmp = context.Query("hlsmokingtcs").Filter(BaseYearOnly).Execute()
            Dim dfhlsmokingtcs As Dataframe = Dataframe.Create("hlsmokingtcs", context, context.FirstYear, context.LastYear, 0)
            Dim R As Short
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    dfhlsmokingtcs(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR)))
                Next
            Next
            Yield dfhlsmokingtcs

            dftmp = context.Query("HLDIABPREV").Filter(BaseYearOnly).Execute()
            Dim dfHLDIABPREV As Dataframe = Dataframe.Create("HLDIABPREV", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    dfHLDIABPREV(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR)))
                Next
            Next
            Yield dfHLDIABPREV

            dftmp = context.Query("HLDIABIGT").Filter(BaseYearOnly).Execute()
            Dim dfHLDIABIGT As Dataframe = Dataframe.Create("HLDIABIGT", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    dfHLDIABIGT(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR)))
                Next
            Next
            Yield dfHLDIABIGT

            Yield context.Query("HLSTUNT").Filter(BaseYearOnly).Execute()
            Yield context.Query("MALNCHPSAM").Filter(BaseYearOnly).Execute()
            Yield context.Query("SAMAVERTED").Filter(BaseYearOnly).Execute()
            Yield context.Query("AIDSDTHS").Filter(BaseYearOnly).Execute()

            dftmp = context.Query("HLSMOKINGIMP").Filter(BaseYearOnly).Execute()
            If context.HistoricMode Then
                Dim dfHLSMOKINGIMP As Dataframe = Dataframe.Create("HLSMOKINGIMP", context, context.FirstYear, context.LastYear, 0)
                Dim dfIdx As Integer
                For R = 1 To CShort(NR)
                    For Dim2 = 1 To 5
                        For Dim3 = 1 To 3
                            dfIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), Dim2, 5, Dim3, 3)
                            For y = FirstYear To LastYear
                                dfHLSMOKINGIMP(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), Dim2, 5, Dim3, 3)) = dftmp(dfIdx)
                            Next y
                        Next Dim3
                    Next Dim2
                Next R
                Yield dfHLSMOKINGIMP
            Else
                Yield dftmp
            End If

            Yield context.Query("HLDIABETESRR").Filter(BaseYearOnly).Execute()
            Yield context.Query("CHILDDTHR").Filter(BaseYearOnly).Execute()

            Yield context.Query("HLMALARIAINC").Filter(BaseYearOnly).Execute()
            Yield context.Query("HLMALARIAPREV").Filter(BaseYearOnly).Execute()
            Yield context.Query("HLMALARIAMORT").Filter(BaseYearOnly).Execute()
            Yield context.Query("HLHIVINC").Filter(BaseYearOnly).Execute()
            Yield context.Query("HLHIVPREV").Filter(BaseYearOnly).Execute()
            Yield context.Query("HLHIVMORT").Filter(BaseYearOnly).Execute()
            Yield context.Query("HLBEDNETS").Filter(BaseYearOnly).Execute()
            Yield context.Query("HLHIVART").Filter(BaseYearOnly).Execute()
            Yield context.Query("HLCONDOMUSE").Filter(BaseYearOnly).Execute()
            Yield context.Query("HLSTUNTCHL").Filter(BaseYearOnly).Execute()
            Yield context.Query("VEHICFLPC").Filter(BaseYearOnly).Execute()

            Dim dfDEATHTRPV As Dataframe = Dataframe.Create("DEATHTRPV", context, context.FirstYear, context.FirstYear, 0)
            If Not context.HistoricMode Then
                For R = 1 To CShort(NR)
                    dfDEATHTRPV(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR))) = DEATHTRPV(R)
                Next
            End If
            Yield dfDEATHTRPV

        End Function

        Class HealthAggregator
            Inherits DefaultSumAggregator
            Dim HLDIABETESRR(NR, 7, 3) As Single?
            Dim HLSMOKINGIMP(NR, 7, 3) As Single?
            Dim HLDIABETESRRMALE As Single?(,)
            Dim HLDIABETESRRFEMALE As Single?(,)
            Dim HLSMOKINGIMPMALE As Single?(,)
            Dim HLSMOKINGIMPFEMALE As Single?(,)

            Dim agedstM As Dictionary(Of String, Dictionary(Of String, Single?))
            Dim agedstF As Dictionary(Of String, Dictionary(Of String, Single?))

            'Private Property CSANITATION1 As IDictionary(Of String, Single?)
            Private Property CHLSMOKINGFEM As IDictionary(Of String, Single?)
            Private Property CHLSMOKINGMALE As IDictionary(Of String, Single?)
            'Private Property CHLSMOKINGTOT As IDictionary(Of String, Single?)
            Private Property CHLOBESITYFEM As IDictionary(Of String, Single?)
            Private Property CHLOBESITYMALE As IDictionary(Of String, Single?)
            'Private Property CHLOBESITYTOT As IDictionary(Of String, Single?)
            Private Property CHLSMOKINGIMPFEM As IDictionary(Of String, Single?())
            Private Property CHLSMOKINGIMPMALE As IDictionary(Of String, Single?())
            Private Property CHLDIABETESRRFEM As IDictionary(Of String, Single?())
            Private Property CHLDIABETESRRMALE As IDictionary(Of String, Single?())
            Private Property CHLSMOKINGIMPFEMI As IDictionary(Of String, Single?())
            Private Property CHLSMOKINGIMPMALEI As IDictionary(Of String, Single?())
            Private Property CHLDIABIGT As IDictionary(Of String, Single?)
            Private Property CHLDIABPREV As IDictionary(Of String, Single?)
            Private Property CHLSTUNT As IDictionary(Of String, Single?)
            Private Property CBMIF As IDictionary(Of String, Single?)
            Private Property CBMIM As IDictionary(Of String, Single?)
            Private Property HIVRATE As IDictionary(Of Integer, Single?)
            'jrs 2009/10/28 made public for ventilation factor in china (energy module)
            'declared in Population module Public agedst As IDictionary(Of String, Single?)
            Private Property CTCS As IDictionary(Of String, Single?)
            Private Property CMALNCHPSAM As IDictionary(Of String, Single?)
            Private Property CSAMADMISSION As IDictionary(Of String, Single?)
            Private Property CSAMAVERTED As IDictionary(Of String, Single?)
            Private Property CGDPPCP As IDictionary(Of String, Single?)
            Private Property CPOP As IDictionary(Of String, Single?)
            Private Property CCLPC_Tot As IDictionary(Of String, Single?)
            Private Property CMALNCHP As IDictionary(Of String, Single?)
            Private Property CSANITATION1 As IDictionary(Of String, Single?)
            Private Property CEDYRSAGE25T As IDictionary(Of String, Single?)
            Private Property CGovCorrupt As IDictionary(Of String, Single?)
            Private Property CSFINTLWARMAG As IDictionary(Of String, Single?)
            Private Property CCBR As IDictionary(Of String, Single?)
            Private Property CEDSECPERF As IDictionary(Of String, Single?)
            Private Property LIFEXP_Male As IDictionary(Of Integer, Single?)
            Private Property LIFEXP_Female As IDictionary(Of Integer, Single?)
            Private Property POP As IDictionary(Of Integer, Single?)
            Private Property CBR As IDictionary(Of Integer, Single?)
            Private Property AIDSDTHS As IDictionary(Of Integer, Single?)
            Private Property VEHICFLPC As IDictionary(Of Integer, Single?)
            Private Property CCHILDTHR As IDictionary(Of String, Single?)
            Private Property INFMOR As IDictionary(Of Integer, Single?)
            Private Property CHILDDTHR As IDictionary(Of Integer, Single?)

            Private Property CHLMALARIAINC As IDictionary(Of String, Single?)
            Private Property CHLMALARIAPREV As IDictionary(Of String, Single?)
            Private Property CHLMALARIAMORT As IDictionary(Of String, Single?)
            Private Property CHLHIVINC As IDictionary(Of String, Single?)
            Private Property CHLHIVPREV As IDictionary(Of String, Single?)
            Private Property CHLHIVMORT As IDictionary(Of String, Single?)
            Private Property CHLBEDNETS As IDictionary(Of String, Single?)
            Private Property CHLHIVART As IDictionary(Of String, Single?)
            Private Property CHLCONDOMUSE As IDictionary(Of String, Single?)
            'kbn 2019/05/10-Start-
            Private Property CHLSTUNTCHL As IDictionary(Of String, Single?)
            'kbn 2019/05/10-End-

            Private Property processed As Boolean
            Private Property dataloaded As Boolean = False
            ' total number of regions

            Private Sub PreProcess(cntx As IRebuildFacade)
                If Me.processed Then
                    Return
                End If

                Dim objWriter = New System.IO.StreamWriter(cntx.PutFdrvRun + "progress.txt", append:=True)
                objWriter.WriteLine("Processing Data on Health - Please Wait")
                objWriter.Close()


                CPOP = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
                CGDPPCP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")

                'jrs 2023/05/30
                FillSmokingRateHistCP(cntx)

                CCLPC_Tot = cntx.Fetch(Of IDictionary(Of String, Single?))("CCLPC_Tot")
                CMALNCHP = cntx.Fetch(Of IDictionary(Of String, Single?))("CMALNCHP")
                CSANITATION1 = cntx.Fetch(Of IDictionary(Of String, Single?))("CSANITATION1")
                CEDYRSAGE25T = cntx.Fetch(Of IDictionary(Of String, Single?))("CEDYRSAGE25T")
                CGovCorrupt = cntx.Fetch(Of IDictionary(Of String, Single?))("CGovCorrupt")
                CSFINTLWARMAG = cntx.Fetch(Of IDictionary(Of String, Single?))("CSFINTLWARMAG")
                CCBR = cntx.Fetch(Of IDictionary(Of String, Single?))("CCBR")
                CEDSECPERF = cntx.Fetch(Of IDictionary(Of String, Single?))("CEDSECPERF")
                LIFEXP_Male = cntx.Fetch(Of IDictionary(Of Integer, Single?))("LIFEXP_Male")
                LIFEXP_Female = cntx.Fetch(Of IDictionary(Of Integer, Single?))("LIFEXP_Female")
                POP = cntx.Fetch(Of IDictionary(Of Integer, Single?))("POP")
                CBR = cntx.Fetch(Of IDictionary(Of Integer, Single?))("CBR")
                AIDSDTHS = cntx.Fetch(Of IDictionary(Of Integer, Single?))("AIDSDTHStmp")
                HIVRATE = cntx.Fetch(Of IDictionary(Of Integer, Single?))("HIVRATE")
                VEHICFLPC = cntx.Fetch(Of IDictionary(Of Integer, Single?))("VEHICFLPC")
                INFMOR = cntx.Fetch(Of IDictionary(Of Integer, Single?))("INFMOR")

                CHLSMOKINGFEM = New Dictionary(Of String, Single?)
                CHLSMOKINGMALE = New Dictionary(Of String, Single?)
                CHLOBESITYFEM = New Dictionary(Of String, Single?)
                CHLOBESITYMALE = New Dictionary(Of String, Single?)
                CHLSMOKINGIMPFEM = New Dictionary(Of String, Single?()) 'Dictionary(Of String, Single?))
                CHLSMOKINGIMPMALE = New Dictionary(Of String, Single?()) ', Dictionary(Of String, Single?))
                CHLDIABETESRRFEM = New Dictionary(Of String, Single?()) ', Dictionary(Of String, Single?))
                CHLDIABETESRRMALE = New Dictionary(Of String, Single?()) ', Dictionary(Of String, Single?))
                CHLSMOKINGIMPFEMI = New Dictionary(Of String, Single?())
                CHLSMOKINGIMPMALEI = New Dictionary(Of String, Single?())
                CHLDIABIGT = New Dictionary(Of String, Single?)
                CHLDIABPREV = New Dictionary(Of String, Single?)
                CHLSTUNT = New Dictionary(Of String, Single?)
                CBMIF = New Dictionary(Of String, Single?)
                CBMIM = New Dictionary(Of String, Single?)
                CCHILDTHR = New Dictionary(Of String, Single?)
                CHILDDTHR = New Dictionary(Of Integer, Single?)

                'jrs 2009/10/28 made public for ventilation factor in china (energy module)
                'declared in Population module Public agedst = New Dictionary(Of String, Single?)
                CTCS = New Dictionary(Of String, Single?)
                CMALNCHPSAM = New Dictionary(Of String, Single?)
                CSAMADMISSION = New Dictionary(Of String, Single?)
                CSAMAVERTED = New Dictionary(Of String, Single?)

                CHLMALARIAINC = New Dictionary(Of String, Single?)
                CHLMALARIAPREV = New Dictionary(Of String, Single?)
                CHLMALARIAMORT = New Dictionary(Of String, Single?)
                CHLHIVINC = New Dictionary(Of String, Single?)
                CHLHIVPREV = New Dictionary(Of String, Single?)
                CHLHIVMORT = New Dictionary(Of String, Single?)
                CHLBEDNETS = New Dictionary(Of String, Single?)
                CHLHIVART = New Dictionary(Of String, Single?)
                CHLCONDOMUSE = New Dictionary(Of String, Single?)
                'kbn 2019/05/10
                CHLSTUNTCHL = New Dictionary(Of String, Single?)
                '======
                Dim ValHold As Single?
                Dim SeriesTable As String = ""
                Dim yrs As Short()
                Dim DictTbl As IDictionary(Of String, Single?())
                '====
                Dim pcoal As CoalescePolicy = CoalescePolicy.Null
                Dim GetField As String
                If cntx.HistoricMode Then
                    GetField = "Earliest"
                Else
                    GetField = CStr(cntx.BaseYear)
                    pcoal = CoalescePolicy.MostRecent
                End If

                'bbh 2019/04/06 start Malaria and HIV Incidence, Prevalence, Mortality (Satellite)
                'Malarial incidence
                '------------------
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    SeriesTable = "SeriesIHMEInciMalariaNumber"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLMALARIAINC.Add(key, If(ValHold.HasValue, ValHold / 1000000, 0))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesIHMEInciMalariaNumber", GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        CHLMALARIAINC.Add(item.Key, If(item.Value / 1000000, 0)) 'Units are rate per 100,000
                    Next
                End If


                'Malarial prevalence
                '------------------
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    SeriesTable = "SeriesIHMEPrevMalariaNumber"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLMALARIAPREV.Add(key, If(ValHold.HasValue, ValHold / 1000000, 0))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesIHMEPrevMalariaNumber", GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        CHLMALARIAPREV.Add(item.Key, If(item.Value / 1000000, 0)) 'Units are portion
                    Next
                End If

                'Malarial mortaltiy
                '------------------
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    SeriesTable = "SeriesIHMEDthsMalar"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLMALARIAMORT.Add(key, If(ValHold.HasValue, ValHold, 0))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesIHMEDthsMalar", GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        'kbn 2019/05/11-Start Remove millions below
                        'CHLMALARIAMORT.Add(item.Key, If(item.Value / 1000000, 0)) 'Units are headcount
                        CHLMALARIAMORT.Add(item.Key, If(item.Value, 0)) 'Units are headcount
                        'kbn 2019/05/11-End
                    Next
                End If

                'HIV incidence
                '------------------
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    SeriesTable = "SeriesIHMEInciHIVNumber"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLHIVINC.Add(key, If(ValHold.HasValue, ValHold / 1000000, 0))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesIHMEInciHIVNumber", GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        CHLHIVINC.Add(item.Key, If(item.Value / 1000000, 0)) 'Units are rate per 100,000
                    Next
                End If

                'HIV prevalence
                '------------------
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    SeriesTable = "SeriesIHMEPrevHIVNumber"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLHIVPREV.Add(key, If(ValHold.HasValue, ValHold / 1000000, 0))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesIHMEPrevHIVNumber", GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        CHLHIVPREV.Add(item.Key, If(item.Value / 1000000, 0)) 'Units are portion
                    Next
                End If

                'HIV mortality
                '------------------
                '   Set valtbl = db.OpenRecordset("SeriesHealthUNAIDSDeathsMidEst")
                'Unclear whether best IHME or UNAIDS series
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    SeriesTable = "SeriesIHMEDthsHIV"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLHIVMORT.Add(key, If(ValHold.HasValue, ValHold, 0))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesIHMEDthsHIV", GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        'kbn 2019/05/11-Start Remove millions below
                        'CHLHIVMORT.Add(item.Key, If(item.Value / 1000000, 0)) 'Units are millions UNAIDS; Headcount IHME
                        CHLHIVMORT.Add(item.Key, If(item.Value, 0)) 'Units are millions UNAIDS; Headcount IHME
                        'kbn 2019/05/11-End
                    Next
                End If

                'Bed net percentage usage by children <5
                '---------------------------------------
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    SeriesTable = "SeriesHealthMalarBedNetsWDI"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLBEDNETS.Add(key, If(ValHold.HasValue, ValHold, 0))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesHealthMalarBedNetsWDI", GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        CHLBEDNETS.Add(item.Key, If(item.Value, 0)) 'Units are percent
                    Next
                End If

                'ART percentage usage by infected
                '--------------------------------
                Dim TbName As String
                Dim CGDPPCPLT120 As New Dictionary(Of String, Single?)
                For Each cc In CGDPPCP
                    CGDPPCPLT120.Add(cc.Key, Amin(cc.Value, 120))
                Next

                'jrs 2024/06/20 TbName$ = "GDP/Capita (PPP 2011) Versus Percentage ART Coverage (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Percentage ART Coverage (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim XYTBL As SeriesEstimator = cntx.GetRegression(TbName).ToEstimator(CGDPPCPLT120)
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    SeriesTable = "SeriesHealthUNAIDSARTCoverMidEst"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLHIVART.Add(key, If(ValHold.HasValue, ValHold, XYTBL(key)) * 100)
                        CHLHIVART(key) = ApplyBounds(CHLHIVART(key), 0, 100)
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesHealthUNAIDSARTCoverMidEst", GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        CHLHIVART.Add(item.Key, If(item.Value, XYTBL(item.Key)) * 100) 'Units are porton
                        CHLHIVART(item.Key) = ApplyBounds(CHLHIVART(item.Key), 0, 100)
                    Next
                End If

                'Condom Availability/Use
                '-----------------------
                Dim CEDYRSAGE25TLT120 As New Dictionary(Of String, Single?)
                For Each cc In CEDYRSAGE25T
                    CEDYRSAGE25TLT120.Add(cc.Key, Amin(cc.Value, 120))
                Next

                TbName$ = "Education Years (15+) Versus Condom Use (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                XYTBL = cntx.GetRegression(TbName).ToEstimator(CEDYRSAGE25TLT120)
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    SeriesTable = "SeriesHealthHIVMenCondUse"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLCONDOMUSE.Add(key, If(ValHold.HasValue, ValHold, XYTBL(key)))
                        CHLCONDOMUSE(key) = ApplyBounds(CHLCONDOMUSE(key), 0, 100)
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesHealthHIVMenCondUse", GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        CHLCONDOMUSE.Add(item.Key, If(item.Value, XYTBL(item.Key))) 'Units are percent
                        CHLCONDOMUSE(item.Key) = ApplyBounds(CHLCONDOMUSE(item.Key), 0, 100)
                    Next
                End If

                'bbh 2019/04/06 end

                'jrs 2021/06/29
                TbName$ = "Child Stunting vs Child Malnutrition and Adult Female Sec. Completion"
                Dim StuntVal As Single

                'kbn 2019/05/10-Start-Child stunting initialization
                For Each item In (From s In cntx.GetSlice("SeriesChildStuntingPercentWDI", "MostRecent")
                                  Select Key = s.Key, Value = s.Value)
                    CHLSTUNTCHL.Add(item.Key, item.Value)
                    'jrs 2021/06/29
                    If Not CHLSTUNTCHL(item.Key).HasValue Then
                        Call AnalFunc(TbName$, New Single() {CSng(CMALNCHP(item.Key)), CSng(CEDSECPERF(item.Key))}, StuntVal,
                                    New String() {"MALNCHP", "EDSECPER(Female)"}, cntx)
                        If StuntVal <= 0 Then
                            StuntVal = CMALNCHP(item.Key)
                        End If
                        CHLSTUNTCHL(item.Key) = StuntVal
                    End If
                Next
                'kbn 2019/05/10-End

                'jrs 2021/06/22
                Dim yrsStunt() As Short
                yrsStunt = cntx.GetYears("SeriesChildStuntingPercentWDI").OrderBy(Function(y) y).ToArray()
                DictTbl = cntx.GetDice("SeriesChildStuntingPercentWDI", yrsStunt)
                For Each key In cntx.Regionalization.Keys
                    CHLSTUNT.Add(key, Nothing)
                    CHLSTUNT(key) = getClosestYrVal(DictTbl, yrsStunt, key, cntx.FirstYear - 30, 10)
                    'jrs 2026/02/23 added or CHLSTUNT=0
                    If Not CHLSTUNT(key).HasValue OrElse CHLSTUNT(key) = 0 Then
                        CHLSTUNT(key) = CHLSTUNTCHL(key) * 1.5
                    End If
                Next

                'Smoking Rate Females
                '--------------------
                Dim CGDPPCPMin As New Dictionary(Of String, Single?)
                For Each cc In CGDPPCP
                    CGDPPCPMin.Add(cc.Key, Amin(cc.Value, 30))
                Next
                'jrs 2024/06/17 TbName$ = "GDP/Capita (PPP 2000) Versus Female Smoking Rate (" & CStr(cntx.BaseYear) & ") Quadratic"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Female Smoking Rate (" & CStr(cntx.BaseYear) & ") Quadratic"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim Regress = cntx.GetRegression(TbName$).ToEstimator(CGDPPCPMin)
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    SeriesTable = "SeriesHealthSmokingFemalesSI"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLSMOKINGFEM.Add(key, If(ValHold.HasValue, ValHold, Regress(key)))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesHealthSmokingFemalesSI", GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        Dim ValCHLSMOKINGFEM = item.Value
                        If Not ValCHLSMOKINGFEM.HasValue Then
                            ValCHLSMOKINGFEM = Regress(item.Key)
                        End If
                        CHLSMOKINGFEM.Add(item.Key, ValCHLSMOKINGFEM)
                    Next
                End If

                'Smoking Rate Males
                '------------------
                CGDPPCPMin = New Dictionary(Of String, Single?)
                For Each cc In CGDPPCP
                    CGDPPCPMin.Add(cc.Key, Amin(cc.Value, 50))
                Next

                'jrs 2024/06/06
                'TbName$ = "GDP/Capita (PPP 2000) Versus Male Smoking Rate (" & CStr(cntx.BaseYear) & ") Quadratic"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Male Smoking Rate (" & CStr(cntx.BaseYear) & ") Quadratic"
                'TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")

                Regress = cntx.GetRegression(TbName$).ToEstimator(CGDPPCPMin)
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    SeriesTable = "SeriesHealthSmokingMalesSI"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLSMOKINGMALE.Add(key, If(ValHold.HasValue, ValHold, Regress(key)))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesHealthSmokingMalesSI", GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        Dim ValCHLSMOKINGMALE = item.Value
                        If Not ValCHLSMOKINGMALE.HasValue Then
                            ValCHLSMOKINGMALE = Regress(item.Key)
                        End If
                        CHLSMOKINGMALE.Add(item.Key, ValCHLSMOKINGMALE)
                    Next
                End If

                'Tobacco Control Score
                '---------------------
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    SeriesTable = "SeriesHealthSmokingTCS"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CTCS.Add(key, If(ValHold.HasValue, ValHold, 0))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesHealthSmokingTCS", GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        CTCS.Add(item.Key, If(item.Value, 0))
                    Next
                End If

                'Obesity Rate Females
                '--------------------
                Dim Slice = "SeriesHealthObesityFemales30Est"
                TbName$ = "Calories per Capita Versus Obesity Rate Adult Females (" & CStr(cntx.BaseYear) & ") Linear"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                Regress = cntx.GetRegression(TbName$).ToEstimator(CCLPC_Tot)
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    yrs = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(Slice, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLOBESITYFEM.Add(key, If(ValHold.HasValue, ValHold, Regress(key)))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice(Slice, GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        Dim ValCHLOBESITYFEM = item.Value
                        If Not ValCHLOBESITYFEM.HasValue Then
                            ValCHLOBESITYFEM = Regress(item.Key)
                        End If
                        CHLOBESITYFEM.Add(item.Key, ValCHLOBESITYFEM)
                    Next
                End If

                'Obesity Rate Males
                '------------------
                Slice = "SeriesHealthObesityMales30Est"
                TbName$ = "Calories per Capita Versus Obesity Rate Adult Males (" & CStr(cntx.BaseYear) & ") Linear"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                Regress = cntx.GetRegression(TbName$).ToEstimator(CCLPC_Tot)
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    yrs = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(Slice, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLOBESITYMALE.Add(key, If(ValHold.HasValue, ValHold, Regress(key)))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice(Slice, GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        Dim ValCHLOBESITYMALE = item.Value
                        If Not ValCHLOBESITYMALE.HasValue Then
                            ValCHLOBESITYMALE = Regress(item.Key)
                        End If
                        CHLOBESITYMALE.Add(item.Key, ValCHLOBESITYMALE)
                    Next
                End If

                'BMI Females
                '-----------
                Slice = "SeriesHealthBMIFemales30EstMean"
                TbName$ = "Calories per Capita Versus Female BMI (" & CStr(cntx.BaseYear) & ") Linear"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                Regress = cntx.GetRegression(TbName$).ToEstimator(CCLPC_Tot)
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    yrs = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(Slice, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CBMIF.Add(key, If(ValHold.HasValue, ValHold, Regress(key)))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice(Slice, GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        Dim ValCBMI = item.Value
                        If Not ValCBMI.HasValue Then
                            ValCBMI = Regress(item.Key)
                        End If
                        CBMIF.Add(item.Key, ValCBMI)
                    Next
                End If

                'BMI Males
                '-----------
                Slice = "SeriesHealthBMIMales30EstMean"
                TbName$ = "Calories per Capita Versus Male BMI (" & CStr(cntx.BaseYear) & ") Linear"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                Regress = cntx.GetRegression(TbName$).ToEstimator(CCLPC_Tot)
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    yrs = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(Slice, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CBMIM.Add(key, If(ValHold.HasValue, ValHold, Regress(key)))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice(Slice, GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        Dim ValCBMI = item.Value
                        If Not ValCBMI.HasValue Then
                            ValCBMI = Regress(item.Key)
                        End If
                        CBMIM.Add(item.Key, ValCBMI)
                    Next
                End If

                'Diabetes, Impared Glucose Tolerance
                '-----------------------------------
                Slice = "SeriesHealthIGTPrev"
                'jrs 2024/06/06 TbName$ = "GDP/Capita (PPP 2000) Versus Impaired Glucose Tolerance (2003) Log"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Impaired Glucose Tolerance (" & BaseYear.ToString & ") Log"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")

                Regress = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    yrs = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(Slice, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLDIABIGT.Add(key, If(ValHold.HasValue, ValHold, Regress(key)))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice(Slice, GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        Dim ValCHLDIABIGT = item.Value
                        If Not ValCHLDIABIGT.HasValue Then
                            ValCHLDIABIGT = Regress(item.Key)
                        End If
                        CHLDIABIGT.Add(item.Key, ValCHLDIABIGT)
                    Next
                End If

                'Diabetes Prevalence
                '-----------------------------------
                Slice = "SeriesHealthDiabetesPrev"
                'jrs 2024/06/17 TbName$ = "GDP/Capita (PPP 2000) Versus Diabetes Prevalence (2003) Log"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Diabetes Prevalence (" & BaseYear & ") Log"
                TbName = getFunctionNameYrOrMstRct(TbName, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Regress = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    yrs = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(Slice, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CHLDIABPREV.Add(key, If(ValHold.HasValue, ValHold, Regress(key)))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice(Slice, GetField, pcoal)
                                      Select Key = s.Key, Value = s.Value)
                        Dim ValCHLDIABPREV = item.Value
                        If Not ValCHLDIABPREV.HasValue Then
                            ValCHLDIABPREV = Regress(item.Key)
                        End If
                        CHLDIABPREV.Add(item.Key, ValCHLDIABPREV)
                    Next
                End If

                ' Read Smoking Impact Data per Cohort - and fill up holes
                ' ----------------------------------------------------------
                'Male
                'jrs 2024/06/13 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Male 30 to 44 (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Male 30 to 44 (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim Regress3044 = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                'jrs 2024/06/13 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Male 45 to 59 (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Male 45 to 59 (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim Regress4559 = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                'jrs 2024/06/13 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Male 60 to 69 (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Male 60 to 69 (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim Regress6069 = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                'jrs 2024/06/13 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Male 70+ (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Male 70+ (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim Regress70P = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                Dim res(cntx.NCohorts) As Single?
                For Each key In cntx.Regionalization.Keys
                    Dim res0(cntx.NCohorts) As Single?
                    CHLSMOKINGIMPMALE.Add(key, res0)
                    Dim res1(cntx.NCohorts) As Single?
                    CHLSMOKINGIMPMALEI.Add(key, res1)
                    Dim res2(cntx.NCohorts) As Single?
                    CHLSMOKINGIMPFEM.Add(key, res2)
                    Dim res3(cntx.NCohorts) As Single?
                    CHLSMOKINGIMPFEMI.Add(key, res3)
                    Dim res4(cntx.NCohorts) As Single?
                    CHLDIABETESRRMALE.Add(key, res4)
                    Dim res5(cntx.NCohorts) As Single?
                    CHLDIABETESRRFEM.Add(key, res5)
                Next key

                ReDim HLSMOKINGIMPMALE(NR, cntx.NCohorts), HLSMOKINGIMPFEMALE(NR, cntx.NCohorts)
                ReDim HLDIABETESRRMALE(NR, cntx.NCohorts), HLDIABETESRRFEMALE(NR, cntx.NCohorts)
                If cntx.HistoricMode Then
                    Slice = "SeriesHealthSmokingImpactMales30to44"
                    For jCount = 0 To cntx.NCohorts
                        Select Case jCount
                            Case 10
                                Slice = "SeriesHealthSmokingImpactMales45to59"
                            Case 13
                                Slice = "SeriesHealthSmokingImpactMales60to69"
                            Case 15
                                Slice = "SeriesHealthSmokingImpactMales70to100"
                        End Select
                        'mti 2019/04/29  Flex Hist Base Start
                        yrs = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                        DictTbl = cntx.GetDice(Slice, yrs)
                        For Each key In cntx.Regionalization.Keys
                            Dim ValCHLSMOKINGIMPMALE = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                            If Not ValCHLSMOKINGIMPMALE.HasValue Then
                                If jCount < 7 Then
                                    ValCHLSMOKINGIMPMALE = 0
                                ElseIf jCount < 10 Then
                                    ValCHLSMOKINGIMPMALE = AMAX(Regress3044(key), 0)
                                ElseIf jCount < 13 Then
                                    ValCHLSMOKINGIMPMALE = AMAX(Regress4559(key), 0)
                                ElseIf jCount < 15 Then
                                    ValCHLSMOKINGIMPMALE = AMAX(Regress6069(key), 0)
                                Else
                                    ValCHLSMOKINGIMPMALE = AMAX(Regress70P(key), 0)
                                End If
                            End If
                            CHLSMOKINGIMPMALE(key)(jCount) = ValCHLSMOKINGIMPMALE
                        Next
                        'mti 2019/04/29  Flex Hist Base End	
                    Next jCount
                    'Smoking Impact: Females
                    '-----------------------
                    'jrs 2024/06/06 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Female 30 to 44 (" & CStr(cntx.BaseYear) & ") Linear"
                    TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Female 30 to 44 (" & CStr(cntx.BaseYear) & ") Linear"
                    'jrs 2024/06/06 TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                    Regress3044 = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                    'jrs 2024/06/06 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Female 45 to 59 (" & CStr(cntx.BaseYear) & ") Linear"
                    TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Female 45 to 59 (" & CStr(cntx.BaseYear) & ") Linear"
                    'jrs 2024/06/06 TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                    Regress4559 = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                    'jrs 2024/06/06 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Female 60 to 69 (" & CStr(cntx.BaseYear) & ") Linear"
                    TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Female 60 to 69 (" & CStr(cntx.BaseYear) & ") Linear"
                    'jrs 2024/06/06 TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                    Regress6069 = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                    'jrs 2024/06/06 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Female 70+ (" & CStr(cntx.BaseYear) & ") Linear"
                    TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Female 70+ (" & CStr(cntx.BaseYear) & ") Linear"
                    'jrs 2024/06/06 TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                    Regress70P = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                    Slice = "SeriesHealthSmokingImpactFemales30to44"
                    For jCount = 0 To cntx.NCohorts
                        Select Case jCount
                            Case 10
                                Slice = "SeriesHealthSmokingImpactFemales45to59"
                            Case 13
                                Slice = "SeriesHealthSmokingImpactFemales60to69"
                            Case 15
                                Slice = "SeriesHealthSmokingImpactFemales70to100"
                        End Select
                        'mti 2019/04/29  Flex Hist Base Start
                        yrs = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                        DictTbl = cntx.GetDice(Slice, yrs)
                        For Each key In cntx.Regionalization.Keys
                            Dim ValCHLSMOKINGIMPFEM = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                            If Not ValCHLSMOKINGIMPFEM.HasValue Then
                                If jCount < 7 Then
                                    ValCHLSMOKINGIMPFEM = 0
                                ElseIf jCount < 10 Then
                                    ValCHLSMOKINGIMPFEM = AMAX(Regress3044(key), 0)
                                ElseIf jCount < 13 Then
                                    ValCHLSMOKINGIMPFEM = AMAX(Regress4559(key), 0)
                                ElseIf jCount < 15 Then
                                    ValCHLSMOKINGIMPFEM = AMAX(Regress6069(key), 0)
                                Else
                                    ValCHLSMOKINGIMPFEM = AMAX(Regress70P(key), 0)
                                End If
                            End If
                            CHLSMOKINGIMPFEM(key)(jCount) = ValCHLSMOKINGIMPFEM
                        Next
                        'mti 2019/04/29  Flex Hist Base End	

                    Next jCount

                    '   Smoking Impact: Regional Averages
                    '   ---------------------------------
                    '
                    Call RegAvgCohorts(CHLSMOKINGIMPMALE, HLSMOKINGIMPMALE, CPOP, "SMOKINGIMPACTMALE", False, cntx)
                    Call RegAvgCohorts(CHLSMOKINGIMPFEM, HLSMOKINGIMPFEMALE, CPOP, "SMOKINGIMPACTFEMALE", False, cntx)

                    'Diabetes Relative Risk
                    '======================
                    'DRR: Males
                    '---------------------
                    Slice = "SeriesHealthDiabetesRRMales15to29"
                    For jCount = 0 To cntx.NCohorts
                        Select Case jCount
                            Case 7
                                Slice = "SeriesHealthDiabetesRRMales30to44"
                            Case 10
                                Slice = "SeriesHealthDiabetesRRMales45to59"
                            Case 13
                                Slice = "SeriesHealthDiabetesRRMales60to69"
                            Case 15
                                Slice = "SeriesHealthDiabetesRRMales70to79"
                            Case 17
                                Slice = "SeriesHealthDiabetesRRMales80to100"
                        End Select
                        For Each item In (From s In cntx.GetSlice(Slice, "2000")
                                          Select Key = s.Key, Value = s.Value)
                            Dim ValCHLDIABETESRRMALE = item.Value
                            If Not ValCHLDIABETESRRMALE.HasValue Then
                                If jCount < 4 Then
                                    ValCHLDIABETESRRMALE = 0
                                Else
                                    ValCHLDIABETESRRMALE = 1
                                End If
                            End If
                            CHLDIABETESRRMALE(item.Key)(jCount) = ValCHLDIABETESRRMALE
                        Next item
                    Next jCount
                    'DRR: Females
                    '---------------------
                    Slice = "SeriesHealthDiabetesRRFemales15to29"
                    For jCount = 0 To cntx.NCohorts
                        Select Case jCount
                            Case 7
                                Slice = "SeriesHealthDiabetesRRFemales30to44"
                            Case 10
                                Slice = "SeriesHealthDiabetesRRFemales45to59"
                            Case 13
                                Slice = "SeriesHealthDiabetesRRFemales60to69"
                            Case 15
                                Slice = "SeriesHealthDiabetesRRFemales70to79"
                            Case 17
                                Slice = "SeriesHealthDiabetesRRFemales80to100"
                        End Select
                        For Each item In (From s In cntx.GetSlice(Slice, "2000")
                                          Select Key = s.Key, Value = s.Value)
                            Dim ValCHLDIABETESRRFEM = item.Value
                            If Not ValCHLDIABETESRRFEM.HasValue Then
                                If jCount < 4 Then
                                    ValCHLDIABETESRRFEM = 0
                                Else
                                    ValCHLDIABETESRRFEM = 1
                                End If
                            End If
                            CHLDIABETESRRFEM(item.Key)(jCount) = ValCHLDIABETESRRFEM
                        Next item
                    Next jCount
                    '   Diabetes Relative Risk : Regional Averages
                    '   -------------------------------------------
                    Call RegAvgCohorts(CHLDIABETESRRMALE, HLDIABETESRRMALE, CPOP, "DIABETESRRMALE", False, cntx)
                    Call RegAvgCohorts(CHLDIABETESRRFEM, HLDIABETESRRFEMALE, CPOP, "DIABETESRRFEMALE", False, cntx)
                    'Saving Diabetes Relative Risk
                    '-----------------------------
                    For r = 1 To NR
                        HLDIABETESRR(r, 7, 1) = 0
                        HLDIABETESRR(r, 7, 2) = 0
                        HLDIABETESRR(r, 7, 3) = 0
                        For cc = 0 To cntx.NCohorts
                            Select Case cc
                                Case 4 '15 to 29
                                    HLDIABETESRR(r, 1, 1) = HLDIABETESRRMALE(r, cc)
                                    HLDIABETESRR(r, 1, 2) = HLDIABETESRRFEMALE(r, cc)
                                    HLDIABETESRR(r, 1, 3) = 0
                                Case 7 '30 to 44
                                    HLDIABETESRR(r, 2, 1) = HLDIABETESRRMALE(r, cc)
                                    HLDIABETESRR(r, 2, 2) = HLDIABETESRRFEMALE(r, cc)
                                    HLDIABETESRR(r, 2, 3) = 0
                                Case 10 '45 to 59
                                    HLDIABETESRR(r, 3, 1) = HLDIABETESRRMALE(r, cc)
                                    HLDIABETESRR(r, 3, 2) = HLDIABETESRRFEMALE(r, cc)
                                    HLDIABETESRR(r, 3, 3) = 0
                                Case 13 '60 to 69
                                    HLDIABETESRR(r, 4, 1) = HLDIABETESRRMALE(r, cc)
                                    HLDIABETESRR(r, 4, 2) = HLDIABETESRRFEMALE(r, cc)
                                    HLDIABETESRR(r, 4, 3) = 0
                                Case 15 '70 to 79
                                    HLDIABETESRR(r, 5, 1) = HLDIABETESRRMALE(r, cc)
                                    HLDIABETESRR(r, 5, 2) = HLDIABETESRRFEMALE(r, cc)
                                    HLDIABETESRR(r, 5, 3) = 0
                                Case 17 '80 plus
                                    HLDIABETESRR(r, 6, 1) = HLDIABETESRRMALE(r, cc)
                                    HLDIABETESRR(r, 6, 2) = HLDIABETESRRFEMALE(r, cc)
                                    HLDIABETESRR(r, 6, 3) = 0
                            End Select
                        Next cc
                    Next


                Else 'Not in Historic Mode *************************************************************************************
                    Dim iYr As Integer, iYear As Integer
                    Dim Coh1 As Integer
                    Dim iFstYr As Integer
                    If cntx.BaseYear <= 2030 Then
                        'jrs 2020/03/12 added to use base year = 1990
                        If cntx.BaseYear >= 2000 Then
                            iFstYr = cntx.BaseYear
                        Else
                            iFstYr = 2000
                        End If
                    Else
                        iFstYr = 2030
                    End If
                    'Now loading only 1 year
                    For iYr = iFstYr To iFstYr 'jrs 2009/12/11 2030
                        If iYr < 2002 Then
                            iYear = 2002
                        Else
                            iYear = iYr
                        End If

                        'Smoking Impact for Males
                        '------------------------
                        'jrs 2024/06/13 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Male 30 to 44 (" & CStr(cntx.BaseYear) & ") Log"
                        TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Male 30 to 44 (" & CStr(cntx.BaseYear) & ") Log"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                        Regress3044 = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                        'jrs 2024/06/13 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Male 45 to 59 (" & CStr(cntx.BaseYear) & ") Log"
                        TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Male 45 to 59 (" & CStr(cntx.BaseYear) & ") Log"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                        Regress4559 = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                        'jrs 2024/06/13 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Male 60 to 69 (" & CStr(cntx.BaseYear) & ") Log"
                        TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Male 60 to 69 (" & CStr(cntx.BaseYear) & ") Log"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                        Regress6069 = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                        'jrs 2024/06/13 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Male 70+ (" & CStr(cntx.BaseYear) & ") Log"
                        TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Male 70+ (" & CStr(cntx.BaseYear) & ") Log"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                        Regress70P = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                        Dim resI(cntx.NCohorts) As Single?
                        Dim jCount As Integer
                        'SI: Males 30 to 44
                        '------------------
                        Slice = "SeriesHealthSmokingImpactMales30to44"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 7
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLSMOKINGIMPMALE = item.Value
                            Dim ValCHLSMOKINGIMPMALEI As Single?
                            If Not ValCHLSMOKINGIMPMALE.HasValue Then
                                If iYr = cntx.BaseYear Then
                                    ValCHLSMOKINGIMPMALE = AMAX(Regress3044(item.Key), 0.1)
                                    ValCHLSMOKINGIMPMALEI = ValCHLSMOKINGIMPMALE
                                Else
                                    ValCHLSMOKINGIMPMALE = ValCHLSMOKINGIMPMALEI
                                End If
                            End If
                            For jCount = 7 To 9
                                CHLSMOKINGIMPMALE(item.Key)(jCount) = ValCHLSMOKINGIMPMALE
                                CHLSMOKINGIMPMALEI(item.Key)(jCount) = ValCHLSMOKINGIMPMALEI
                            Next jCount
                        Next item
                        'SI: Males 45 to 59
                        '------------------
                        Slice = "SeriesHealthSmokingImpactMales45to59"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 10
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLSMOKINGIMPMALE = item.Value
                            Dim ValCHLSMOKINGIMPMALEI As Single?
                            If Not ValCHLSMOKINGIMPMALE.HasValue Then
                                If iYr = cntx.BaseYear Then
                                    ValCHLSMOKINGIMPMALE = AMAX(Regress4559(item.Key), 0.1)
                                    ValCHLSMOKINGIMPMALEI = ValCHLSMOKINGIMPMALE
                                Else
                                    ValCHLSMOKINGIMPMALE = ValCHLSMOKINGIMPMALEI
                                End If
                            End If
                            For jCount = 10 To 12
                                CHLSMOKINGIMPMALE(item.Key)(jCount) = ValCHLSMOKINGIMPMALE
                                CHLSMOKINGIMPMALEI(item.Key)(jCount) = ValCHLSMOKINGIMPMALEI
                            Next jCount
                        Next item
                        'SI: Males 60 to 69
                        '------------------
                        Slice = "SeriesHealthSmokingImpactMales60to69"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 13
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLSMOKINGIMPMALE = item.Value
                            Dim ValCHLSMOKINGIMPMALEI As Single?
                            If Not ValCHLSMOKINGIMPMALE.HasValue Then
                                If iYr = cntx.BaseYear Then
                                    ValCHLSMOKINGIMPMALE = AMAX(Regress6069(item.Key), 0.1)
                                    ValCHLSMOKINGIMPMALEI = ValCHLSMOKINGIMPMALE
                                Else
                                    ValCHLSMOKINGIMPMALE = ValCHLSMOKINGIMPMALEI
                                End If
                            End If
                            For jCount = 13 To 14
                                CHLSMOKINGIMPMALE(item.Key)(jCount) = ValCHLSMOKINGIMPMALE
                                CHLSMOKINGIMPMALEI(item.Key)(jCount) = ValCHLSMOKINGIMPMALEI
                            Next jCount
                        Next item
                        'SI: Males 70+
                        '------------------
                        Slice = "SeriesHealthSmokingImpactMales70to100"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 15
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLSMOKINGIMPMALE = item.Value
                            Dim ValCHLSMOKINGIMPMALEI As Single?
                            If Not ValCHLSMOKINGIMPMALE.HasValue Then
                                If iYr = cntx.BaseYear Then
                                    ValCHLSMOKINGIMPMALE = AMAX(Regress70P(item.Key), 0.1)
                                    ValCHLSMOKINGIMPMALEI = ValCHLSMOKINGIMPMALE
                                Else
                                    ValCHLSMOKINGIMPMALE = ValCHLSMOKINGIMPMALEI
                                End If
                            End If
                            For jCount = 15 To cntx.NCohorts
                                CHLSMOKINGIMPMALE(item.Key)(jCount) = ValCHLSMOKINGIMPMALE
                                CHLSMOKINGIMPMALEI(item.Key)(jCount) = ValCHLSMOKINGIMPMALEI
                            Next jCount
                        Next item
                        'Smoking Impact for Females
                        '--------------------------
                        'jrs 2024/06/06 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Female 30 to 44 (" & CStr(cntx.BaseYear) & ") Linear"
                        TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Female 30 to 44 (" & CStr(cntx.BaseYear) & ") Linear"
                        'jrs 2024/06/06 TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                        Regress3044 = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                        'jrs 2024/06/06 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Female 45 to 59 (" & CStr(cntx.BaseYear) & ") Linear"
                        TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Female 45 to 59 (" & CStr(cntx.BaseYear) & ") Linear"
                        'jrs 2024/06/06 TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                        Regress4559 = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                        'jrs 2024/06/06 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Female 60 to 69 (" & CStr(cntx.BaseYear) & ") Linear"
                        TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Female 60 to 69 (" & CStr(cntx.BaseYear) & ") Linear"
                        'jrs 2024/06/06 TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                        Regress6069 = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                        'jrs 2024/06/06 TbName$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Female 70+ (" & CStr(cntx.BaseYear) & ") Linear"
                        TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Smoking Impact Female 70+ (" & CStr(cntx.BaseYear) & ") Linear"
                        'jrs 2024/06/06 TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                        Regress70P = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                        'SI: Females 30 to 44
                        '------------------
                        Slice = "SeriesHealthSmokingImpactFemales30to44"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 7
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLSMOKINGIMPFEM = item.Value
                            Dim ValCHLSMOKINGIMPFEMI As Single?
                            If Not ValCHLSMOKINGIMPFEM.HasValue Then
                                If iYr = cntx.BaseYear Then
                                    ValCHLSMOKINGIMPFEM = AMAX(Regress3044(item.Key), 0.1)
                                    ValCHLSMOKINGIMPFEMI = ValCHLSMOKINGIMPFEM
                                Else
                                    ValCHLSMOKINGIMPFEM = ValCHLSMOKINGIMPFEMI
                                End If
                            End If
                            For jCount = 7 To 9
                                CHLSMOKINGIMPFEM(item.Key)(jCount) = ValCHLSMOKINGIMPFEM
                                CHLSMOKINGIMPFEMI(item.Key)(jCount) = ValCHLSMOKINGIMPFEMI
                            Next jCount
                        Next item
                        'SI: Males 45 to 59
                        '------------------
                        Slice = "SeriesHealthSmokingImpactFemales45to59"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 10
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLSMOKINGIMPFEM = item.Value
                            Dim ValCHLSMOKINGIMPFEMI As Single?
                            If Not ValCHLSMOKINGIMPFEM.HasValue Then
                                If iYr = cntx.BaseYear Then
                                    ValCHLSMOKINGIMPFEM = AMAX(Regress4559(item.Key), 0.1)
                                    ValCHLSMOKINGIMPFEMI = ValCHLSMOKINGIMPFEM
                                Else
                                    ValCHLSMOKINGIMPFEM = ValCHLSMOKINGIMPFEMI
                                End If
                            End If

                            For jCount = 10 To 12
                                CHLSMOKINGIMPFEM(item.Key)(jCount) = ValCHLSMOKINGIMPFEM
                                CHLSMOKINGIMPFEMI(item.Key)(jCount) = ValCHLSMOKINGIMPFEMI
                            Next jCount

                        Next item
                        'SI: Males 60 to 69
                        '------------------
                        Slice = "SeriesHealthSmokingImpactFemales60to69"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 13
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLSMOKINGIMPFEM = item.Value
                            Dim ValCHLSMOKINGIMPFEMI As Single?
                            If Not ValCHLSMOKINGIMPFEM.HasValue Then
                                If iYr = cntx.BaseYear Then
                                    ValCHLSMOKINGIMPFEM = AMAX(Regress6069(item.Key), 0.1)
                                    ValCHLSMOKINGIMPFEMI = ValCHLSMOKINGIMPFEM
                                Else
                                    ValCHLSMOKINGIMPFEM = ValCHLSMOKINGIMPFEMI
                                End If
                            End If

                            For jCount = 13 To 14
                                CHLSMOKINGIMPFEM(item.Key)(jCount) = ValCHLSMOKINGIMPFEM
                                CHLSMOKINGIMPFEMI(item.Key)(jCount) = ValCHLSMOKINGIMPFEMI
                            Next jCount
                        Next item
                        'SI: Males 70+
                        '------------------
                        Slice = "SeriesHealthSmokingImpactFemales70to100"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 15
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLSMOKINGIMPFEM = item.Value
                            Dim ValCHLSMOKINGIMPFEMI As Single?
                            If Not ValCHLSMOKINGIMPFEM.HasValue Then
                                If iYr = cntx.BaseYear Then
                                    ValCHLSMOKINGIMPFEM = AMAX(Regress70P(item.Key), 0.1)
                                    ValCHLSMOKINGIMPFEMI = ValCHLSMOKINGIMPFEM
                                Else
                                    ValCHLSMOKINGIMPFEM = ValCHLSMOKINGIMPFEMI
                                End If
                            End If

                            For jCount = 15 To cntx.NCohorts
                                CHLSMOKINGIMPFEM(item.Key)(jCount) = ValCHLSMOKINGIMPFEM
                                CHLSMOKINGIMPFEMI(item.Key)(jCount) = ValCHLSMOKINGIMPFEMI
                            Next jCount
                        Next item

                        '   Smoking Impact: Regional Averages
                        '   ---------------------------------
                        '
                        Call RegAvgCohorts(CHLSMOKINGIMPMALE, HLSMOKINGIMPMALE, CPOP, "SMOKINGIMPACTMALE", False, cntx)
                        Call RegAvgCohorts(CHLSMOKINGIMPFEM, HLSMOKINGIMPFEMALE, CPOP, "SMOKINGIMPACTFEMALE", False, cntx)
                        'Diabetes Relative Risk for Males
                        '--------------------------------
                        'DRR: Males 15 to 29
                        '-------------------
                        TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 15 to 29 Cubic"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        Regress = cntx.GetRegression(TbName$).ToEstimator(CBMIM)
                        Slice = "SeriesHealthDiabetesRRMales15to29"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 4
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLDIABETESRRMALE = item.Value
                            If Not ValCHLDIABETESRRMALE.HasValue Then
                                ValCHLDIABETESRRMALE = AMAX(Regress(item.Key), 1)
                            End If
                            For jCount = 4 To 6
                                CHLDIABETESRRMALE(item.Key)(jCount) = ValCHLDIABETESRRMALE
                            Next jCount
                        Next item
                        'DRR: Males 30 to 44
                        '-------------------
                        TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 30 to 44 Cubic"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        Regress = cntx.GetRegression(TbName$).ToEstimator(CBMIM)
                        Slice = "SeriesHealthDiabetesRRMales30to44"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 7
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLDIABETESRRMALE = item.Value
                            If Not ValCHLDIABETESRRMALE.HasValue Then
                                ValCHLDIABETESRRMALE = AMAX(Regress(item.Key), 1)
                            End If
                            For jCount = 7 To 9
                                CHLDIABETESRRMALE(item.Key)(jCount) = ValCHLDIABETESRRMALE
                            Next jCount
                        Next item
                        'DRR: Males 45 to 59
                        '------------------
                        TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 45 to 59 Cubic"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        Regress = cntx.GetRegression(TbName$).ToEstimator(CBMIM)
                        Slice = "SeriesHealthDiabetesRRMales45to59"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 10
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLDIABETESRRMALE = item.Value
                            If Not ValCHLDIABETESRRMALE.HasValue Then
                                ValCHLDIABETESRRMALE = AMAX(Regress(item.Key), 1)
                            End If
                            For jCount = 10 To 12
                                CHLDIABETESRRMALE(item.Key)(jCount) = ValCHLDIABETESRRMALE
                            Next jCount
                        Next item
                        'DRR: Males 60 to 69
                        '------------------
                        TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 60 to 69 Cubic"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        Regress = cntx.GetRegression(TbName$).ToEstimator(CBMIM)
                        Slice = "SeriesHealthDiabetesRRMales60to69"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 13
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLDIABETESRRMALE = item.Value
                            If Not ValCHLDIABETESRRMALE.HasValue Then
                                ValCHLDIABETESRRMALE = AMAX(Regress(item.Key), 1)
                            End If
                            For jCount = 13 To 14
                                CHLDIABETESRRMALE(item.Key)(jCount) = ValCHLDIABETESRRMALE
                            Next jCount
                        Next item
                        'DRR: Males 70 to 79
                        '-------------------
                        TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 70 to 79 Cubic"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        Regress = cntx.GetRegression(TbName$).ToEstimator(CBMIM)
                        Slice = "SeriesHealthDiabetesRRMales70to79"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 15
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLDIABETESRRMALE = item.Value
                            If Not ValCHLDIABETESRRMALE.HasValue Then
                                ValCHLDIABETESRRMALE = AMAX(Regress(item.Key), 1)
                            End If
                            For jCount = 15 To 16
                                CHLDIABETESRRMALE(item.Key)(jCount) = ValCHLDIABETESRRMALE
                            Next jCount
                        Next item
                        'DRR: Males 80+
                        '------------------
                        TbName$ = "BMI(2005) vs Diabetes RR[2005] Males 80+ Cubic"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        Regress = cntx.GetRegression(TbName$).ToEstimator(CBMIM)
                        Slice = "SeriesHealthDiabetesRRMales80to100"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 17
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLDIABETESRRMALE = item.Value
                            If Not ValCHLDIABETESRRMALE.HasValue Then
                                ValCHLDIABETESRRMALE = AMAX(Regress(item.Key), 1)
                            End If
                            For jCount = 17 To cntx.NCohorts
                                CHLDIABETESRRMALE(item.Key)(jCount) = ValCHLDIABETESRRMALE
                            Next jCount
                        Next item
                        '===COPY->FEM
                        'Diabetes Relative Risk for Females
                        '----------------------------------
                        'DRR: Females 15 to 29
                        '-------------------
                        TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 15 to 29 Cubic"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        Regress = cntx.GetRegression(TbName$).ToEstimator(CBMIF)
                        Slice = "SeriesHealthDiabetesRRFemales15to29"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 4
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLDIABETESRRFEM = item.Value
                            If Not ValCHLDIABETESRRFEM.HasValue Then
                                ValCHLDIABETESRRFEM = AMAX(Regress(item.Key), 1)
                            End If
                            For jCount = 4 To 6
                                CHLDIABETESRRFEM(item.Key)(jCount) = ValCHLDIABETESRRFEM
                            Next jCount
                        Next item
                        'DRR: Females 30 to 44
                        '-------------------
                        TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 30 to 44 Cubic"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        Regress = cntx.GetRegression(TbName$).ToEstimator(CBMIF)
                        Slice = "SeriesHealthDiabetesRRFemales30to44"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 7
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLDIABETESRRFEM = item.Value
                            If Not ValCHLDIABETESRRFEM.HasValue Then
                                ValCHLDIABETESRRFEM = AMAX(Regress(item.Key), 1)
                            End If
                            For jCount = 7 To 9
                                CHLDIABETESRRFEM(item.Key)(jCount) = ValCHLDIABETESRRFEM
                            Next jCount
                        Next item
                        'DRR: Females 45 to 59
                        '------------------
                        TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 45 to 59 Cubic"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        Regress = cntx.GetRegression(TbName$).ToEstimator(CBMIF)
                        Slice = "SeriesHealthDiabetesRRFemales45to59"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 10
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLDIABETESRRFEM = item.Value
                            If Not ValCHLDIABETESRRFEM.HasValue Then
                                ValCHLDIABETESRRFEM = AMAX(Regress(item.Key), 1)
                            End If
                            For jCount = 10 To 12
                                CHLDIABETESRRFEM(item.Key)(jCount) = ValCHLDIABETESRRFEM
                            Next jCount
                        Next item
                        'DRR: Females 60 to 69
                        '------------------
                        TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 60 to 69 Cubic"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        Regress = cntx.GetRegression(TbName$).ToEstimator(CBMIF)
                        Slice = "SeriesHealthDiabetesRRFemales60to69"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 13
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLDIABETESRRFEM = item.Value
                            If Not ValCHLDIABETESRRFEM.HasValue Then
                                ValCHLDIABETESRRFEM = AMAX(Regress(item.Key), 1)
                            End If
                            For jCount = 13 To 14
                                CHLDIABETESRRFEM(item.Key)(jCount) = ValCHLDIABETESRRFEM
                            Next jCount
                        Next item
                        'DRR: Females 70 to 79
                        '-------------------
                        TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 70 to 79 Cubic"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        Regress = cntx.GetRegression(TbName$).ToEstimator(CBMIF)
                        Slice = "SeriesHealthDiabetesRRFemales70to79"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 15
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLDIABETESRRFEM = item.Value
                            If Not ValCHLDIABETESRRFEM.HasValue Then
                                ValCHLDIABETESRRFEM = AMAX(Regress(item.Key), 1)
                            End If
                            For jCount = 15 To 16
                                CHLDIABETESRRFEM(item.Key)(jCount) = ValCHLDIABETESRRFEM
                            Next jCount
                        Next item
                        'DRR: Females 80+
                        '------------------
                        TbName$ = "BMI(2005) vs Diabetes RR[2005] Females 80+ Cubic"
                        TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                        Regress = cntx.GetRegression(TbName$).ToEstimator(CBMIF)
                        Slice = "SeriesHealthDiabetesRRFemales80to100"
                        For Each item In (From s In cntx.GetSlice(Slice, CStr(iYear))
                                          Select Key = s.Key, Value = s.Value)
                            jCount = 17
                            Coh1 = jCount
                            'ReDim res(cntx.NCohorts)
                            Dim ValCHLDIABETESRRFEM = item.Value
                            If Not ValCHLDIABETESRRFEM.HasValue Then
                                ValCHLDIABETESRRFEM = AMAX(Regress(item.Key), 1)
                            End If

                            For jCount = 17 To cntx.NCohorts
                                CHLDIABETESRRFEM(item.Key)(jCount) = ValCHLDIABETESRRFEM
                            Next jCount
                        Next item
                        '   Diabetes Relative Risk : Regional Averages
                        '   -------------------------------------------
                        Call RegAvgCohorts(CHLDIABETESRRMALE, HLDIABETESRRMALE, CPOP, "DIABETESRRMALE", False, cntx)
                        Call RegAvgCohorts(CHLDIABETESRRFEM, HLDIABETESRRFEMALE, CPOP, "DIABETESRRFEMALE", False, cntx)
                    Next iYr
                End If 'end of if historic base else forecast
                '
                For ICount% = 1 To NR
                    HLSMOKINGIMP(ICount%, 5, 1) = 0
                    HLSMOKINGIMP(ICount%, 5, 2) = 0
                    HLSMOKINGIMP(ICount%, 5, 3) = 0
                    For cc = 7 To 15
                        Select Case cc
                            Case 7 '30 to 44
                                HLSMOKINGIMP(ICount%, 1, 1) = HLSMOKINGIMPMALE(ICount%, cc)
                                HLSMOKINGIMP(ICount%, 1, 2) = HLSMOKINGIMPFEMALE(ICount%, cc)
                                HLSMOKINGIMP(ICount%, 1, 3) = 0
                            Case 10 '45 to 59
                                HLSMOKINGIMP(ICount%, 2, 1) = HLSMOKINGIMPMALE(ICount%, cc)
                                HLSMOKINGIMP(ICount%, 2, 2) = HLSMOKINGIMPFEMALE(ICount%, cc)
                                HLSMOKINGIMP(ICount%, 2, 3) = 0
                            Case 13 '60 to 69
                                HLSMOKINGIMP(ICount%, 3, 1) = HLSMOKINGIMPMALE(ICount%, cc)
                                HLSMOKINGIMP(ICount%, 3, 2) = HLSMOKINGIMPFEMALE(ICount%, cc)
                                HLSMOKINGIMP(ICount%, 3, 3) = 0
                            Case 15 '70 plus
                                HLSMOKINGIMP(ICount%, 4, 1) = HLSMOKINGIMPMALE(ICount%, cc)
                                HLSMOKINGIMP(ICount%, 4, 2) = HLSMOKINGIMPFEMALE(ICount%, cc)
                                HLSMOKINGIMP(ICount%, 4, 3) = 0
                        End Select
                    Next cc
                Next ICount%
                'Saving Diabetes RR
                '----------------------------
                For ICount% = 1 To NR
                    HLDIABETESRR(ICount%, 6, 1) = 0
                    HLDIABETESRR(ICount%, 6, 2) = 0
                    HLDIABETESRR(ICount%, 6, 3) = 0
                    For cc = 4 To 17
                        Select Case cc
                            Case 4 '15 to 29
                                HLDIABETESRR(ICount%, 1, 1) = HLDIABETESRRMALE(ICount%, cc)
                                HLDIABETESRR(ICount%, 1, 2) = HLDIABETESRRFEMALE(ICount%, cc)
                                HLDIABETESRR(ICount%, 1, 3) = 0
                            Case 7 '30 to 44
                                HLDIABETESRR(ICount%, 2, 1) = HLDIABETESRRMALE(ICount%, cc)
                                HLDIABETESRR(ICount%, 2, 2) = HLDIABETESRRFEMALE(ICount%, cc)
                                HLDIABETESRR(ICount%, 2, 3) = 0
                            Case 10 '45 to 59
                                HLDIABETESRR(ICount%, 3, 1) = HLDIABETESRRMALE(ICount%, cc)
                                HLDIABETESRR(ICount%, 3, 2) = HLDIABETESRRFEMALE(ICount%, cc)
                                HLDIABETESRR(ICount%, 3, 3) = 0
                            Case 13 '60 to 69
                                HLDIABETESRR(ICount%, 4, 1) = HLDIABETESRRMALE(ICount%, cc)
                                HLDIABETESRR(ICount%, 4, 2) = HLDIABETESRRFEMALE(ICount%, cc)
                                HLDIABETESRR(ICount%, 4, 3) = 0
                            Case 15 '70 to 79
                                HLDIABETESRR(ICount%, 5, 1) = HLDIABETESRRMALE(ICount%, cc)
                                HLDIABETESRR(ICount%, 5, 2) = HLDIABETESRRFEMALE(ICount%, cc)
                                HLDIABETESRR(ICount%, 5, 3) = 0
                            Case 17 '80+
                                HLDIABETESRR(ICount%, 6, 1) = HLDIABETESRRMALE(ICount%, cc)
                                HLDIABETESRR(ICount%, 6, 2) = HLDIABETESRRFEMALE(ICount%, cc)
                                HLDIABETESRR(ICount%, 6, 3) = 0
                        End Select
                    Next cc
                Next ICount%
                'Update WHO Low-Income Codes
                'Table: HealthWHOLowGDPModel
                'jrs 2022/11/01 flex constant dollars
                Dim GDPPCPTbl = cntx.GetSlice("SeriesGDP" & ConstantDollarsYear.ToString & "PCPPP", "2002")
                Dim GDPTbl = cntx.GetSlice("SeriesGDP" & ConstantDollarsYear.ToString, "2002")
                Dim poptbl = cntx.GetSlice("SeriesPopulation", "2002")
                Dim GDPPCP2 As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each e In GDPTbl
                    GDPPCP2.Add(e.Key, e.Value / poptbl(e.Key))
                Next
                TbName$ = "GDP/Capita Versus GDP/Capita at Purchasing Power Parity (2000) Power Function"
                Dim GDPPCatPPP = cntx.GetRegression(TbName$).ToEstimator(GDPPCP2)
                'ttt Add Check ContainsKey

                Using db As IFs.Data.IFsContext = New IFsContext()
                    For Each WHORec As HealthWHOLowGDPModel In db.HealthWHOLowGDPModel
                        If WHORec Is DBNull.Value Then Exit For
                        Dim key = WHORec.Country
                        'jrs 2019/03/29
                        If Not WHORec.Code.HasValue Then
                            WHORec.Code = -1
                        End If
                        If WHORec.Code <> 1 Then
                            If GDPPCPTbl.ContainsKey(key) AndAlso GDPPCPTbl(key).HasValue Then 'Use year 2002 as the GBD does
                                If GDPPCPTbl(key) < 3000 Then
                                    WHORec.Code = 2 'low-income
                                Else
                                    WHORec.Code = 0 'high-income
                                End If
                            Else
                                If GDPTbl.ContainsKey(key) AndAlso GDPTbl(key).HasValue AndAlso poptbl(key).HasValue Then
                                    If GDPPCatPPP(key) < 3 Then
                                        WHORec.Code = 2 'low-income
                                    Else
                                        WHORec.Code = 0 'high-income
                                    End If
                                Else
                                    WHORec.Code = -1
                                End If
                            End If
                        End If
                        'db.SaveChanges(True)
                    Next WHORec
                    db.SaveChanges(True)
                End Using
                '-------------------------------------------------
                'SAM: Severe Acute Malnutrition
                '
                If Not cntx.HistoricMode Then
                    'Procedure to Load Stunting = Malnutrition 25 years ago
                    'for Countries
                    '-------------------------------------------------------
                    'jrs 2021/06/22 LoadCStunt(cntx)
                Else
                    'jrs 2021/06/22
                    'For Each key In cntx.Regionalization.Keys
                    '    CHLSTUNT.Add(key, 0)
                    'Next
                End If
                Dim TblEarliest = cntx.GetSlice("SeriesSevereWasting", "Earliest")
                Dim TblBase = cntx.GetSlice("SeriesSevereWasting", cntx.BaseYear, CoalescePolicy.MostRecent)
                '  Set valtbl = db.OpenRecordset("SeriesSevereWasting")
                SeriesTable = "SeriesSevereWasting"
                yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                DictTbl = cntx.GetDice(SeriesTable, yrs)
                For Each key In cntx.Regionalization.Keys
                    Dim ValCMALNCHPSAM As Single?
                    If cntx.HistoricMode Then
                        'mti 2019/04/29  Flex Hist Base Start
                        ValCMALNCHPSAM = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        'mti 2019/04/29  Flex Hist Base End	
                    Else
                        ValCMALNCHPSAM = TblBase(key)
                    End If
                    If Not ValCMALNCHPSAM.HasValue Then
                        If CSANITATION1(key) < 0.5 Then
                            ValCMALNCHPSAM = 0.01
                        Else
                            Dim z = Amin(-2.455 - 0.103 * CEDYRSAGE25T(key) - 0.306 * CGovCorrupt(key) + 0.114 * CSFINTLWARMAG(key), 709)
                            ValCMALNCHPSAM = 100 * Math.Exp(z) / (1 + Math.Exp(z))
                        End If
                    End If
                    If ValCMALNCHPSAM > CMALNCHP(key) * 0.91 Then
                        ValCMALNCHPSAM = CMALNCHP(key) * 0.91
                    End If
                    If ValCMALNCHPSAM < 0 Then
                        ValCMALNCHPSAM = 0
                    End If
                    CMALNCHPSAM.Add(key, ValCMALNCHPSAM)
                Next key
                '
                'SAM successful treatments
                '------------------------------
                TblEarliest = cntx.GetSlice("SeriesMalnChilSAMCuredAdmission", "Earliest")
                TblBase = cntx.GetSlice("SeriesMalnChilSAMCuredAdmission", cntx.BaseYear, CoalescePolicy.MostRecent)
                SeriesTable = "SeriesMalnChilSAMCuredAdmission"
                yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                DictTbl = cntx.GetDice(SeriesTable, yrs)
                For Each key In cntx.Regionalization.Keys
                    Dim ValCSAMAVERTED As Single?
                    If cntx.HistoricMode Then
                        'mti 2019/04/29  Flex Hist Base Start
                        ValCSAMAVERTED = If(TblEarliest.ContainsKey(key), getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist), Nothing)
                        'mti 2019/04/29  Flex Hist Base End	
                    Else
                        ValCSAMAVERTED = If(TblBase.ContainsKey(key), TblBase(key), Nothing)
                    End If
                    If Not ValCSAMAVERTED.HasValue Then
                        ValCSAMAVERTED = 67
                    End If
                    'validations
                    If ValCSAMAVERTED > 100 Then
                        ValCSAMAVERTED = 100
                    End If
                    If ValCSAMAVERTED < 0 Then
                        ValCSAMAVERTED = 0
                    End If
                    CSAMAVERTED.Add(key, ValCSAMAVERTED)
                Next key

                'jrs 2019/03/21 ***** begin ****
                'Children under 5 Mortality Probablity
                '--------------------------------------
                TblEarliest = cntx.GetSlice("SeriesUnder5MortRateIHME", "Earliest")
                TblBase = cntx.GetSlice("SeriesUnder5MortRateIHME", cntx.BaseYear, CoalescePolicy.MostRecent)
                SeriesTable = "SeriesUnder5MortRateIHME"
                yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                DictTbl = cntx.GetDice(SeriesTable, yrs)
                For Each key In cntx.Regionalization.Keys
                    Dim ValCCHILDTHR As Single?
                    If cntx.HistoricMode Then
                        'mti 2019/04/29  Flex Hist Base Start
                        ValCCHILDTHR = If(TblEarliest.ContainsKey(key), getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist), Nothing)
                        'mti 2019/04/29  Flex Hist Base End	
                    Else
                        ValCCHILDTHR = If(TblBase.ContainsKey(key), TblBase(key), Nothing)
                    End If
                    If Not ValCCHILDTHR.HasValue Then
                        ValCCHILDTHR = 0
                    End If
                    CCHILDTHR.Add(key, ValCCHILDTHR)
                Next key

                'ttt Skip: Write to Health Output Data Table - Still Country Specific
                '   Table: HealthOutput
                '======
                Me.processed = True
            End Sub

            Private Sub LoadCStunt(cntx As IRebuildFacade)
                Dim TbName As String
                Dim CGDPPCPVal As Single, CMalnVal As Single
                Dim MalnShift As Single
                Dim lag As Integer
                lag = cntx.BaseYear - 25
                'jrs 2022/11/01 flex constant dollars
                Dim yrs As Short() = cntx.GetYears("SeriesGDP" & ConstantDollarsYear.ToString & "PCPPP").OrderBy(Function(y) y).ToArray()
                Dim GDPPCPTbl = cntx.GetSlice("SeriesGDP" & ConstantDollarsYear.ToString & "PCPPP", cntx.BaseYear, CoalescePolicy.MostRecent)
                Dim GDPPCPLagTbl = cntx.GetSlice("SeriesGDP" & ConstantDollarsYear.ToString & "PCPPP", CStr(lag))
                Dim poptbl = cntx.GetSlice("SeriesPopulation", CStr(lag))
                Dim GDPTbl = cntx.GetSlice("SeriesGDP" & ConstantDollarsYear.ToString, CStr(lag))
                For Each key In cntx.Regionalization.Keys 'For cc = 1 To CCount
                    'Find GDPPCP for Base Year
                    '-------------------------
                    'mti 2015/09/08 - start
                    If yrs.Contains(cntx.BaseYear) Then
                        If GDPPCPTbl(key).HasValue Then
                            CGDPPCPVal = GDPPCPTbl(key) / 1000
                        Else
                            'Use GDPPCP for Base Year
                            '----------------------------
                            CGDPPCPVal = CGDPPCP(key)
                        End If
                    Else
                        'Use GDPPCP for Base Year
                        '----------------------------
                        CGDPPCPVal = CGDPPCP(key)
                    End If

                    'Estimate Malnutrition for Base Year
                    '-----------------------------------
                    TbName = "GDP/Capita (PPP 2000) Versus Malnutrition (2000) Power"
                    'Call XYTABL(TbName, CGDPPCPVal, CMalnVal)
                    Dim DrvVals As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                    DrvVals.Add("DrvVal", CGDPPCPVal)
                    CMalnVal = cntx.GetRegression(TbName).ToEstimator(DrvVals)("DrvVal")
                    'Find Additive Shift factor
                    '--------------------------
                    MalnShift = CMALNCHP(key) - CMalnVal
                    'Find GDPPCP 25 years ago
                    '------------------------
                    If GDPPCPLagTbl(key).HasValue Then
                        CGDPPCPVal = GDPPCPLagTbl(key) / 1000
                    Else
                        'Estimate GDPPCP 25 years ago
                        '----------------------------
                        If GDPTbl(key).HasValue AndAlso poptbl(key).HasValue Then
                            TbName = "GDP/Capita Versus GDP/Capita at Purchasing Power Parity (2000) Power Function"
                            DrvVals = New Dictionary(Of String, Single?)
                            DrvVals.Add("DrvVal", GDPTbl(key) / poptbl(key))
                            CGDPPCPVal = cntx.GetRegression(TbName).ToEstimator(DrvVals)("DrvVal")
                        Else 'assumes no change in malnutrition in last 25 years!!!
                            CGDPPCPVal = CGDPPCP(key)
                        End If
                    End If
                    'Estimate Malnutrition 25 years ago
                    '----------------------------------
                    TbName = "GDP/Capita (PPP 2000) Versus Malnutrition (2000) Power"
                    DrvVals = New Dictionary(Of String, Single?)
                    DrvVals.Add("DrvVal", CGDPPCPVal)
                    CMalnVal = cntx.GetRegression(TbName).ToEstimator(DrvVals)("DrvVal")
                    'Save Stunting for countries
                    '---------------------------
                    CHLSTUNT.Add(key, AMAX(Amin(CMalnVal + MalnShift, 80), 0))
                Next key
            End Sub
            Private Sub RegionalizeEstHisSmokingRt(ByRef CWeight As Dictionary(Of String, Single?), ByVal cntx As IRebuildFacade)
                Dim iLagYrs As Integer
                iLagYrs = 25
                'must not include %
                'Dim yrsFemales As Short() = cntx.GetYears("SeriesHealthSmokingFemales%SI").OrderBy(Function(yy) yy).ToArray()
                'Dim FemalesTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesHealthSmokingFemales%SI", yrsFemales)
                'Dim yrsMales As Short() = cntx.GetYears("SeriesHealthSmokingMales%SI").OrderBy(Function(yy) yy).ToArray()
                'Dim MalesTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesHealthSmokingMales%SI", yrsMales)

                Dim yrsFemales As Short() = cntx.GetYears("SeriesHealthSmokingFemalesSI").OrderBy(Function(yy) yy).ToArray()
                Dim FemalesTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesHealthSmokingFemalesSI", yrsFemales)
                Dim yrsMales As Short() = cntx.GetYears("SeriesHealthSmokingMalesSI").OrderBy(Function(yy) yy).ToArray()
                Dim MalesTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesHealthSmokingMalesSI", yrsMales)
                'Save results to array before using target table
                Dim RegSmRt(NR, iLagYrs + 1, 2) As Single '2 genders
                Dim RegIdx As Integer
                Dim iYr As Integer
                Dim RWeight(NR) As Single
                'For each country
                For Each key In cntx.Regionalization.Keys
                    'Group by Region
                    RegIdx = cntx.Regionalization(key)
                    'for each year
                    For iYr = 1 To iLagYrs + 1
                        If cntx.BaseYear - iLagYrs - 1 + iYr >= 1980 Then
                            Dim kk = MalesTbl(key)(CShort(cntx.BaseYear - iLagYrs - 1 + iYr) - yrsMales(0))
                            'jrs 2022/12/08
                            If MalesTbl(key)(CShort(cntx.BaseYear - iLagYrs - 1 + iYr) - yrsMales(0)).HasValue Then
                                RegSmRt(RegIdx, iYr, 1) = RegSmRt(RegIdx, iYr, 1) + MalesTbl(key)(CShort(cntx.BaseYear - iLagYrs - 1 + iYr) - yrsMales(0)) *
                                CWeight(key)
                            End If
                            'jrs 2022/12/08
                            If FemalesTbl(key)(CShort(cntx.BaseYear - iLagYrs - 1 + iYr) - yrsFemales(0)).HasValue Then
                                RegSmRt(RegIdx, iYr, 2) = RegSmRt(RegIdx, iYr, 2) + FemalesTbl(key)(CShort(cntx.BaseYear - iLagYrs - 1 + iYr) - yrsFemales(0)) *
                                CWeight(key)
                            End If
                        End If
                    Next iYr
                    RWeight(RegIdx) = RWeight(RegIdx) + CWeight(key)
                Next key
                Dim rr As Integer, gg As Integer
                Using dbT As IFs.Data.IFsSmokingContext = New IFsSmokingContext()
                    dbT.Clear(GetType(SmokingRateEstimatesReg))
                    For rr = 1 To NR
                        'for each year
                        For iYr = 1 To iLagYrs + 1
                            'for each gender
                            For gg = 1 To 2
                                RegSmRt(rr, iYr, gg) = RegSmRt(rr, iYr, gg) / RWeight(rr)
                                'Add new row
                                Dim rec As SmokingRateEstimatesReg = New SmokingRateEstimatesReg()
                                rec.RegionName = cntx.GetDimension(1).Buckets.Item(CShort(rr))
                                rec.RegionId = rr
                                rec.Year = iYr + cntx.BaseYear - iLagYrs - 1
                                rec.Sex = gg
                                rec.SmokingRate = RegSmRt(rr, iYr, gg)
                                dbT.SmokingRateEstimatesReg.Add(rec)
                            Next gg
                        Next iYr
                    Next rr
                    dbT.Flush()
                End Using
            End Sub

            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Me.PreProcess(cntx)
                'Compute Regional Average for Detailed Deaths (Mortality Rates in deaths per 1000)
                '=================================================================================
                If Not Me.dataloaded Then
                    'load CHILDDTHR
                    CHILDDTHR = (From s In CCHILDTHR
                                 Join r In cntx.Regionalization On s.Key Equals r.Key
                                 Join w In CPOP On s.Key Equals w.Key
                                 Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
                           )

                    If Not cntx.HistoricMode Then
                        RegDetailedDeaths(cntx)
                    End If
#Region "Load/Write Data"
                    If cntx.HistoricMode Then
                        '   Smoking Impact: Regional Averages
                        '   ---------------------------------
                        ReDim HLSMOKINGIMPMALE(NR, cntx.NCohorts), HLSMOKINGIMPFEMALE(NR, cntx.NCohorts)
                        Call RegAvgCohorts(CHLSMOKINGIMPMALE, HLSMOKINGIMPMALE, CPOP, "SMOKINGIMPACTMALE", False, cntx)
                        Call RegAvgCohorts(CHLSMOKINGIMPFEM, HLSMOKINGIMPFEMALE, CPOP, "SMOKINGIMPACTFEMALE", False, cntx)
                    End If
                    'ttt skip
                    '   Put Regional Data into Health Base Access Table
                    '   Table: HealthBase
                    'Compute Regional Average for Historical Estimation of Smoking Rate
                    '==================================================================
                    If Not cntx.HistoricMode Then
                        RegionalizeEstHisSmokingRt(CPOP, cntx)
                    End If
                    'Compute Life Expectancy Based on AIDS data
                    '==========================================

                    'First: Create AIDS Mortality Distribution
                    '-----------------------------------------
                    Dim AidsMortDistM = New Dictionary(Of String, Dictionary(Of String, Single?))
                    Dim AidsMortDistF = New Dictionary(Of String, Dictionary(Of String, Single?))
                    agedstM = New Dictionary(Of String, Dictionary(Of String, Single?))
                    agedstF = New Dictionary(Of String, Dictionary(Of String, Single?))
                    Dim aidsdeathsbyageM(cntx.NCohorts) As Single?
                    Dim aidsdeathsbyageF(cntx.NCohorts) As Single?
                    Dim aidsdeathsbyageHeavyM(cntx.NCohorts) As Single?
                    Dim aidsdeathsbyageHeavyF(cntx.NCohorts) As Single?
                    Dim aidsdeathsbyageGBDM = New Dictionary(Of String, Dictionary(Of String, Single?))
                    Dim aidsdeathsbyageGBDF = New Dictionary(Of String, Dictionary(Of String, Single?))
                    'Load Aids Cohort Distribution vector
                    LoadAIDSDthbyAge(aidsdeathsbyageM, aidsdeathsbyageF, cntx)
                    LoadAIDSDthbyAgeHeavy(aidsdeathsbyageHeavyM, aidsdeathsbyageHeavyF, cntx)
                    'Load population cohorts
                    'ReadPopCohortDist agedst, coname, db
                    'Load BirthShare
                    Dim birthmshr As Single
                    Dim birthfshr As Single
                    Using db As IFsContext = New IFsContext
                        Using db2 As IFs2Context = New IFs2Context
                            ReadPopCohortDist(agedstM, agedstF, cntx, db2)
                        End Using
                        Dim gp As GlobalParameters
                        gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "BIRTHMSHR").FirstOrDefault
                        birthmshr = If(gp Is Nothing, 0, CSng(gp.Value))
                    End Using
                    birthfshr = 1 - birthmshr
                    For R = 1 To NR
                        'Adjust to Regional Population values
                        Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                        AdjustCohPop(agedstM, agedstF, POP, R, cntx)
                        'Fill in new borns
                        If agedstM(RegionName).ContainsKey("Cohort0") Then
                            agedstM(RegionName)("Cohort0") = CBR(R) * POP(R) * birthmshr / 1000
                        Else
                            agedstM(RegionName).Add("Cohort0", CBR(R) * POP(R) * birthmshr / 1000)
                        End If
                        If agedstF(RegionName).ContainsKey("Cohort0") Then
                            agedstF(RegionName)("Cohort0") = CBR(R) * POP(R) * birthfshr / 1000
                        Else
                            agedstF(RegionName).Add("Cohort0", CBR(R) * POP(R) * birthfshr / 1000)
                        End If
                    Next R
                    'jrs 2024/12/03
                    Dim MaleAIDSPortion As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                    LoadAIDSDthbyAgeGBD(aidsdeathsbyageGBDM, aidsdeathsbyageGBDF,
                                     aidsdeathsbyageM, aidsdeathsbyageF,
                                     aidsdeathsbyageHeavyM, aidsdeathsbyageHeavyF,
                                      agedstM, agedstF, cntx, MaleAIDSPortion)
                    Dim TbName$ = "GDP/Capita (PPP) Versus HIV Infections, Male Portion"
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                    Dim MaleAIDSPortionEst = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                    For R = 1 To NR
                        'For Each key In cntx.Regionalization.Keys
                        Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                        If AIDSDTHS(R) > 0 Then
                            If Not MaleAIDSPortion.ContainsKey(RegionName) Then
                                MaleAIDSPortion.Add(RegionName, MaleAIDSPortionEst(RegionName))
                            End If
                            ComputeAIDsMortDistGBD(R, RegionName, agedstM, agedstF, aidsdeathsbyageGBDM,
                          aidsdeathsbyageGBDF, AidsMortDistM, AidsMortDistF, MaleAIDSPortion, cntx)
                        End If
                    Next R
                    'Second: Compute Life Expectancy
                    'Explained by Randall Kuhn
                    'Life Expectancy was computed as the median age at death,
                    'and it should be computed as a mean
                    'average number of years of life given the survival rates in each age group
                    Dim infMort, cohMort, LE, pct As Single?
                    Dim lx, N, nax, nqx As Single?
                    For R = 1 To NR
                        Dim key = cntx.GetDimension(1).Buckets.Item(CShort(R))
                        If AIDSDTHS(R) > 0 Then
                            For k = 1 To 2
                                If k = 1 Then 'Male
                                    LE = 0
                                    'infMort = AidsMortDist(0, R, k)
                                    infMort = AidsMortDistM(key)("Cohort0")
                                    lx = 1
                                    For j = 0 To cntx.NCohorts
                                        'Years in cohort
                                        If j = 0 Then
                                            N = 1
                                        ElseIf j = 1 Then
                                            N = 4
                                        Else
                                            N = 5
                                        End If
                                        'cohMort = AidsMortDist(j, R, k)
                                        cohMort = AidsMortDistM(key)("Cohort" & j.ToString)
                                        'Average years lived by those who die (per Keyfitz)
                                        If j = 0 Then
                                            If cohMort >= 0.107 Then
                                                nax = 0.34
                                            Else
                                                nax = 0.049 + 2.742 * cohMort
                                            End If
                                        ElseIf j = 1 Then
                                            If infMort >= 0.107 Then
                                                nax = 1.356
                                            Else
                                                nax = 1.587 - 2.167 * infMort
                                            End If
                                        Else
                                            nax = 2.5
                                        End If
                                        'Probability of death using period data
                                        If j = cntx.NCohorts And False Then 'jrs 2019/04/05 needs to be fixed in vb6
                                            nqx = 1
                                        Else
                                            If cohMort.HasValue Then
                                                nqx = (N * cohMort) / (1 + ((N - nax) * cohMort))
                                                If nqx > 1 Then
                                                    nqx = 1
                                                End If
                                            Else
                                                nqx = 0
                                            End If
                                        End If
                                        'Adding N years for survivors, and nax for deaths
                                        If j < cntx.NCohorts Or True Then 'jrs 2019/04/05 needs to be fixed in vb6
                                            LE = LE + (lx * (1 - nqx) * N) + (lx * nqx * nax)
                                        Else
                                            If cohMort > 0 Then
                                                LE = LE + lx / cohMort
                                            Else
                                                LE = LE + N
                                            End If
                                        End If
                                        'Probability of surviving until age x
                                        lx = lx * (1 - nqx)
                                    Next j
                                    'Compare Life expectancy
                                    If LE < 1.1 * LIFEXP_Male(R) Then
                                        'AIDS death are too high, incompatible with LIFEXP
                                        'Reduce AIDS deaths, by 150% of the excess percentage
                                        pct = (LIFEXP_Male(R) - LE) / LE * 1.5
                                        pct = AMAX(pct, 0.1) 'reduce by at least 10% each time
                                        AIDSDTHS(R) = AIDSDTHS(R) * (1 - pct)
                                        'Save Result
                                        'ttt Skip
                                        'db.Execute("UPDATE PopulationBase Set AIDSDths = " &
                                        'CStr(AMAX(0, Amin(AIDSDTHS(R), 1000))) &
                                        '" WHERE Region = '" & GRPS(1, R) & "'")
                                        'Recompute AidsMortDist
                                        'ComputeAIDsMortDistGBD(R, key, agedstM, agedstM, aidsdeathsbyageGBDM, aidsdeathsbyageGBDF,
                                        'AidsMortDistM, AidsMortDistF, MaleAIDSPortion, cntx)
                                        ComputeAIDsMortDistGBD(R, key, agedstM, agedstM, aidsdeathsbyageGBDM, aidsdeathsbyageGBDF,
                                        AidsMortDistM, AidsMortDistF, MaleAIDSPortion, cntx)
                                        'Go back to recompute LE 
                                        k = k - 1
                                    End If
                                Else 'Female
                                    'ttt Copy above, replace M by F
                                    LE = 0
                                    'infMort = AidsMortDist(0, R, k)
                                    infMort = AidsMortDistF(key)("Cohort0")
                                    lx = 1
                                    For j = 0 To cntx.NCohorts
                                        'Years in cohort
                                        If j = 0 Then
                                            N = 1
                                        ElseIf j = 1 Then
                                            N = 4
                                        Else
                                            N = 5
                                        End If
                                        'cohMort = AidsMortDist(j, R, k)
                                        cohMort = AidsMortDistF(key)("Cohort" & j.ToString)
                                        'Average years lived by those who die (per Keyfitz)
                                        If j = 0 Then
                                            If cohMort >= 0.107 Then
                                                nax = 0.34
                                            Else
                                                nax = 0.049 + 2.742 * cohMort
                                            End If
                                        ElseIf j = 1 Then
                                            If infMort >= 0.107 Then
                                                nax = 1.356
                                            Else
                                                nax = 1.587 - 2.167 * infMort
                                            End If
                                        Else
                                            nax = 2.5
                                        End If
                                        'Probability of death using period data
                                        If j = cntx.NCohorts And False Then 'jrs 2019/04/05 needs to be fixed in vb6
                                            nqx = 1
                                        Else
                                            If cohMort.HasValue Then
                                                nqx = (N * cohMort) / (1 + ((N - nax) * cohMort))
                                                If nqx > 1 Then
                                                    nqx = 1
                                                End If
                                            Else
                                                nqx = 0
                                            End If
                                        End If
                                        'Adding N years for survivors, and nax for deaths
                                        If j < cntx.NCohorts Or True Then 'jrs 2019/04/05 needs to be fixed in vb6
                                            LE = LE + (lx * (1 - nqx) * N) + (lx * nqx * nax)
                                        Else
                                            If cohMort > 0 Then
                                                LE = LE + lx / cohMort
                                            Else
                                                LE = LE + N
                                            End If
                                        End If
                                        'Probability of surviving until age x
                                        lx = lx * (1 - nqx)
                                    Next j
                                    'Compare Life expectancy
                                    If LE < 1.1 * LIFEXP_Female(R) Then
                                        'AIDS death are too high, incompatible with LIFEXP
                                        'Reduce AIDS deaths, by 150% of the excess percentage
                                        pct = (LIFEXP_Female(R) - LE) / LE * 1.5
                                        pct = AMAX(pct, 0.1) 'reduce by at least 10% each time
                                        AIDSDTHS(R) = AIDSDTHS(R) * (1 - pct)
                                        'Save Result
                                        'ttt skip
                                        'db.Execute("UPDATE PopulationBase SET AIDSDths = " &
                                        'CStr(AMAX(0, Amin(AIDSDTHS(R), 1000))) &
                                        '" WHERE Region = '" & GRPS(1, R) & "'")
                                        'Recompute AidsMortDist
                                        ComputeAIDsMortDistGBD(R, key, agedstM, agedstF, aidsdeathsbyageGBDM, aidsdeathsbyageGBDF,
                                                           AidsMortDistM, AidsMortDistF, MaleAIDSPortion, cntx)
                                        'Go back to recompute LE 
                                        k = k - 1
                                    End If
                                End If
                            Next k 'Sex
                        End If
                    Next R
                    'Compute AIDS distribution Matrix
                    '================================

                    '0. Load and Compute Mortality Distribution
                    '------------------------------------------
                    'Dim modmordst()
                    'ReDim modmordst(NCohorts, NR, 2)
                    Dim modmordstM = New Dictionary(Of String, Dictionary(Of String, Single?))
                    Dim modmordstF = New Dictionary(Of String, Dictionary(Of String, Single?))
                    LoadMortdist(modmordstM, modmordstF, cntx) 'modmordst, coname 
                    '0.5 Mark low AIDS countries that have too much AIDS in a cohort
                    '---------------------------------------------------------------
                    Dim TooMuchAIDS(NR) As Boolean
                    For R = 1 To NR
                        Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                        If HIVRATE.ContainsKey(R) AndAlso HIVRATE(R) < 10 Then
                            For j = 0 To cntx.NCohorts
                                For k = 1 To 2
                                    'If modmordst(j, R, k) < AidsMortDist(j, R, k) * 1000 Then
                                    If k = 1 Then 'Male
                                        If AidsMortDistM.ContainsKey(RegionName) AndAlso modmordstM(RegionName)("Cohort" & j.ToString) < AidsMortDistM(RegionName)("Cohort" & j.ToString) * 1000 Then
                                            TooMuchAIDS(R) = True
                                            k = 2
                                            j = cntx.NCohorts
                                        End If
                                    Else 'female
                                        If AidsMortDistF.ContainsKey(RegionName) AndAlso modmordstF(RegionName)("Cohort" & j.ToString) < AidsMortDistF(RegionName)("Cohort" & j.ToString) * 1000 Then
                                            TooMuchAIDS(R) = True
                                            k = 2
                                            j = cntx.NCohorts
                                        End If
                                    End If
                                Next k
                            Next j
                        End If
                    Next R
                    'ComputeAIDsMortDist("key", agedstM, agedstF, aidsdeathsbyageM, aidsdeathsbyageF, AidsMortDistM, AidsMortDistF, MaleAIDSPortion, cntx)
                    '0.6 check too much AIDS if using heavy vector
                    '---------------------------------------------
                    Dim TooMuchAIDSHeavy(NR) As Boolean
                    For R = 1 To NR
                        Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                        If TooMuchAIDS(R) Then
                            ComputeAIDsMortDist(R, RegionName, agedstM, agedstF,
                                             aidsdeathsbyageHeavyM, aidsdeathsbyageHeavyF,
                                             AidsMortDistF, AidsMortDistF,
                                             MaleAIDSPortion, cntx)
                            For j = 0 To cntx.NCohorts
                                For k = 1 To 2
                                    If k = 1 Then 'Male
                                        'If modmordst(j, R, k) < AidsMortDist(j, R, k) * 1000 Then
                                        If AidsMortDistM.ContainsKey(RegionName) AndAlso modmordstM(RegionName)("Cohort" & j.ToString) < AidsMortDistM(RegionName)("Cohort" & j.ToString) * 1000 Then
                                            TooMuchAIDSHeavy(R) = True
                                            k = 2
                                            j = cntx.NCohorts
                                            ' go back to the original distribution if this didn't fix it
                                            ComputeAIDsMortDistGBD(R, RegionName, agedstM, agedstF,
                                                               aidsdeathsbyageGBDM, aidsdeathsbyageGBDF,
                                                               AidsMortDistM, AidsMortDistF, MaleAIDSPortion, cntx)
                                        End If
                                    Else 'Female
                                        If AidsMortDistF.ContainsKey(RegionName) AndAlso modmordstF(RegionName)("Cohort" & j.ToString) < AidsMortDistF(RegionName)("Cohort" & j.ToString) * 1000 Then
                                            TooMuchAIDSHeavy(R) = True
                                            k = 2
                                            j = cntx.NCohorts
                                            ' go back to the original distribution if this didn't fix it
                                            ComputeAIDsMortDistGBD(R, RegionName, agedstM, agedstF,
                                                               aidsdeathsbyageGBDM, aidsdeathsbyageGBDF,
                                                               AidsMortDistM, AidsMortDistF, MaleAIDSPortion, cntx)
                                        End If
                                    End If
                                Next k
                            Next j
                        End If
                    Next R
                    '0.7 check too much AIDS when HIV>=10
                    '---------------------------------------------
                    Dim TooMuchAIDS10(NR) As Boolean
                    For R = 1 To NR
                        Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                        If HIVRATE.ContainsKey(R) AndAlso HIVRATE(R) >= 10 Then
                            For j = 0 To cntx.NCohorts
                                For k = 1 To 2
                                    If k = 1 Then
                                        'If modmordst(j, R, k) < AidsMortDist(j, R, k) * 1000 Then
                                        If AidsMortDistM.ContainsKey(RegionName) AndAlso modmordstM(RegionName)("Cohort" & j.ToString) < AidsMortDistM(RegionName)("Cohort" & j.ToString) * 1000 Then
                                            TooMuchAIDS10(R) = True
                                            k = 2
                                            j = cntx.NCohorts
                                        End If
                                    Else
                                        'If modmordst(j, R, k) < AidsMortDist(j, R, k) * 1000 Then
                                        If AidsMortDistF.ContainsKey(RegionName) AndAlso modmordstF(RegionName)("Cohort" & j.ToString) < AidsMortDistF(RegionName)("Cohort" & j.ToString) * 1000 Then
                                            TooMuchAIDS10(R) = True
                                            k = 2
                                            j = cntx.NCohorts
                                        End If
                                    End If
                                Next k
                            Next j
                        End If
                    Next R
                    '0.8 if still too much aids, reduce AIDSDTHS again
                    '-------------------------------------------------
                    Dim Adj As Single
                    For R = 1 To NR
                        Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                        If TooMuchAIDS10(R) OrElse TooMuchAIDSHeavy(R) Then
                            Adj = 0
                            For k = 1 To 2
                                If k = 1 Then
                                    For j = 0 To cntx.NCohorts
                                        'If modmordst(j, R, k) < AidsMortDist(j, R, k) * 1000 Then
                                        If AidsMortDistM.ContainsKey(RegionName) AndAlso modmordstM(RegionName)("Cohort" & j.ToString) < AidsMortDistM(RegionName)("Cohort" & j.ToString) * 1000 Then
                                            If (modmordstM(RegionName)("Cohort" & j.ToString) - AidsMortDistM(RegionName)("Cohort" & j.ToString) * 1000) / (AMAX(AidsMortDistM(RegionName)("Cohort" & j.ToString), 0.0001) * 1000) < Adj Then
                                                Adj = (modmordstM(RegionName)("Cohort" & j.ToString) - AidsMortDistM(RegionName)("Cohort" & j.ToString) * 1000) / (AMAX(AidsMortDistM(RegionName)("Cohort" & j.ToString), 0.0001) * 1000)
                                            End If
                                        End If
                                    Next j
                                Else 'Female
                                    For j = 0 To cntx.NCohorts
                                        'If modmordst(j, R, k) < AidsMortDist(j, R, k) * 1000 Then
                                        If AidsMortDistF.ContainsKey(RegionName) AndAlso modmordstF(RegionName)("Cohort" & j.ToString) < AidsMortDistF(RegionName)("Cohort" & j.ToString) * 1000 Then
                                            If (modmordstF(RegionName)("Cohort" & j.ToString) - AidsMortDistF(RegionName)("Cohort" & j.ToString) * 1000) / (AMAX(AidsMortDistF(RegionName)("Cohort" & j.ToString), 0.0001) * 1000) < Adj Then
                                                Adj = (modmordstF(RegionName)("Cohort" & j.ToString) - AidsMortDistF(RegionName)("Cohort" & j.ToString) * 1000) / (AMAX(AidsMortDistF(RegionName)("Cohort" & j.ToString), 0.0001) * 1000)
                                            End If
                                        End If
                                    Next j
                                End If
                            Next k
                            If Adj < 0 Then
                                'Give an extra 10% of room, and reduce at least 10% each time
                                'jrs 2025/02/24 added amax(0.99)
                                'AIDSDTHS(R) = AIDSDTHS(R) * (1 + Amin(Adj * 1.1, -0.1))
                                AIDSDTHS(R) = AIDSDTHS(R) * (1 + AMAX(Amin(Adj * 1.1, -0.1), -0.99))
                                'ttt Skip
                                'db.Execute("UPDATE PopulationBase SET AIDSDths = " &
                                'CStr(AMAX(0, Amin(AIDSDTHS(R), 1000))) &
                                '" WHERE Region = """ & GRPS(1, R) & """") 'jrs 2015/05/22 replaced ' with "

                                'Recompute AidsMortDist
                                ComputeAIDsMortDistGBD(R, RegionName, agedstM, agedstF,
                                                   aidsdeathsbyageGBDM, aidsdeathsbyageGBDF,
                                                   AidsMortDistM, AidsMortDistF, MaleAIDSPortion, cntx)
                                'Retry
                                R = R - 1 '???
                            Else
                                TooMuchAIDS10(R) = False
                                TooMuchAIDSHeavy(R) = False
                                'jrs 2012/12/10

                                TooMuchAIDS(R) = False
                            End If
                        End If
                    Next R
                    '4. Compute Aids spread vectors
                    '------------------------------
                    'ReDim AIDSRegDist(NCohorts, NR, 2) As Single
                    Dim AIDSRegDistM = New Dictionary(Of String, Dictionary(Of String, Single?))
                    Dim AIDSRegDistF = New Dictionary(Of String, Dictionary(Of String, Single?))
                    For R = 1 To NR
                        Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                        Dim ValM = New Dictionary(Of String, Single?)
                        Dim ValF = New Dictionary(Of String, Single?)
                        For p = 0 To cntx.NCohorts
                            ValM.Add("Cohort" & p.ToString, Nothing)
                            ValF.Add("Cohort" & p.ToString, Nothing)
                        Next p
                        If HIVRATE.ContainsKey(R) AndAlso HIVRATE(R) < 10 AndAlso Not TooMuchAIDS(R) Then
                            For j = 0 To cntx.NCohorts
                                If aidsdeathsbyageGBDM.ContainsKey(RegionName) AndAlso aidsdeathsbyageGBDM(RegionName)("Cohort" & j.ToString).HasValue Then
                                    ValM("Cohort" & j.ToString) = aidsdeathsbyageGBDM(RegionName)("Cohort" & j.ToString)
                                End If
                                If aidsdeathsbyageGBDF.ContainsKey(RegionName) AndAlso aidsdeathsbyageGBDF(RegionName)("Cohort" & j.ToString).HasValue Then
                                    ValF("Cohort" & j.ToString) = aidsdeathsbyageGBDF(RegionName)("Cohort" & j.ToString)
                                End If
                            Next j
                        ElseIf TooMuchAIDS(R) AndAlso Not TooMuchAIDSHeavy(R) Then
                            For j = 0 To cntx.NCohorts
                                If aidsdeathsbyageHeavyM(j).HasValue Then
                                    ValM("Cohort" & j.ToString) = aidsdeathsbyageHeavyM(j)
                                End If
                                If aidsdeathsbyageHeavyF(j).HasValue Then
                                    ValF("Cohort" & j.ToString) = aidsdeathsbyageHeavyF(j)
                                End If
                            Next j
                        ElseIf HIVRATE.ContainsKey(R) AndAlso HIVRATE(R) >= 10 AndAlso Not TooMuchAIDS10(R) Then 'jrs 2010/02/23
                            'For countries with high Aids, but handled by new GBD distribution (None!)
                            For j = 0 To cntx.NCohorts
                                ValM("Cohort" & j.ToString) = If(Not aidsdeathsbyageGBDM.ContainsKey(RegionName), Nothing, aidsdeathsbyageGBDM(RegionName)("Cohort" & j.ToString))
                                ValF("Cohort" & j.ToString) = If(Not aidsdeathsbyageGBDF.ContainsKey(RegionName), Nothing, aidsdeathsbyageGBDF(RegionName)("Cohort" & j.ToString))
                            Next j
                        Else
                            'jrs 2010/02/24
                            'No longe used
                            Debug.WriteLine("Too much AIDS in the preprocessor-Health data")
                        End If
                        AIDSRegDistM.Add(RegionName, ValM)
                        AIDSRegDistF.Add(RegionName, ValF)
                    Next R

                    If Not cntx.HistoricMode Then
                        Using dbT As IFs.Data.IFs2Context = New IFs2Context()
                            dbT.Clear(GetType(PopCohortAIDSRegion))
                            For R = 1 To NR
                                Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                                For k = 1 To 2
                                    Dim rec As PopCohortAIDSRegion = New PopCohortAIDSRegion()
                                    rec.RegionName = RegionName
                                    rec.RegionId = R
                                    rec.Sex = k
                                    For j = 0 To cntx.NCohorts
                                        Dim val As Double = 0
                                        If k = 1 Then
                                            val = CDbl(If(Not AIDSRegDistM(RegionName)("Cohort" & j.ToString).HasValue, 0, AIDSRegDistM(RegionName)("Cohort" & j.ToString).Value))
                                        Else
                                            val = CDbl(If(Not AIDSRegDistF(RegionName)("Cohort" & j.ToString).HasValue, 0, AIDSRegDistF(RegionName)("Cohort" & j.ToString).Value))
                                        End If
                                        Select Case j
                                            Case 0
                                                rec.Cohort0 = val
                                            Case 1
                                                rec.Cohort1 = val
                                            Case 2
                                                rec.Cohort2 = val
                                            Case 3
                                                rec.Cohort3 = val
                                            Case 4
                                                rec.Cohort4 = val
                                            Case 5
                                                rec.Cohort5 = val
                                            Case 6
                                                rec.Cohort6 = val
                                            Case 7
                                                rec.Cohort7 = val
                                            Case 8
                                                rec.Cohort8 = val
                                            Case 9
                                                rec.Cohort9 = val
                                            Case 10
                                                rec.Cohort10 = val
                                            Case 11
                                                rec.Cohort11 = val
                                            Case 12
                                                rec.Cohort12 = val
                                            Case 13
                                                rec.Cohort13 = val
                                            Case 14
                                                rec.Cohort14 = val
                                            Case 15
                                                rec.Cohort15 = val
                                            Case 16
                                                rec.Cohort16 = val
                                            Case 17
                                                rec.Cohort17 = val
                                            Case 18
                                                rec.Cohort18 = val
                                            Case 19
                                                rec.Cohort19 = val
                                            Case 20
                                                rec.Cohort20 = val
                                            Case 21
                                                rec.Cohort21 = val
                                            Case 22
                                                rec.Cohort22 = val
                                            Case 23
                                                rec.Cohort23 = val
                                            Case 24
                                                rec.Cohort24 = val
                                            Case 25
                                                rec.Cohort25 = val
                                            Case 26
                                                rec.Cohort26 = val
                                            Case 27
                                                rec.Cohort27 = val
                                            Case 28
                                                rec.Cohort28 = val
                                            Case 29
                                                rec.Cohort29 = val
                                            Case 30
                                                rec.Cohort30 = val
                                            Case 31
                                                rec.Cohort31 = val
                                            Case 32
                                                rec.Cohort32 = val
                                            Case 33
                                                rec.Cohort33 = val
                                            Case 34
                                                rec.Cohort34 = val
                                            Case 35
                                                rec.Cohort35 = val
                                            Case 36
                                                rec.Cohort36 = val
                                            Case 37
                                                rec.Cohort37 = val
                                            Case 38
                                                rec.Cohort38 = val
                                            Case 39
                                                rec.Cohort39 = val
                                            Case 40
                                                rec.Cohort40 = val
                                            Case 41
                                                rec.Cohort41 = val
                                        End Select
                                        'old
                                        'dbT.Entry(rec).CurrentValues("Cohort" & j.ToString) = CDbl(If(Not AIDSRegDistF(RegionName)("Cohort" & j.ToString).HasValue, 0, AIDSRegDistF(RegionName)("Cohort" & j.ToString).Value))
                                    Next j
                                    dbT.PopCohortAIDSRegion.Add(rec)
                                Next k
                            Next R
                            dbT.Flush()
                        End Using
                    End If
                    'Load Diarrheal Data
                    If Not cntx.HistoricMode Then
                        Dim Ntypes As Integer
                        Ntypes = 30
                        Dim modmordstdet(cntx.NCohorts, NR%, Ntypes) As Single?
                        'Dim modmordstdet(NCohorts, NR%, Ntypes)
                        LoadInitData(modmordstdet, cntx)
                        '            'Regionalizing Diarrhea
                        LoadDiarrheaData(modmordstdet, cntx)
                        '            'Regionalizing Malaria
                        LoadMalariaData(agedstM, agedstF, cntx)
                        ''Compute Population for WHO groups
                        '            '---------------------------------
                        Dim agedstWHOM(14, cntx.NCohorts) As Single?
                        Dim agedstWHOF(14, cntx.NCohorts) As Single?
                        Dim WHOIndex As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)
                        BuildPopGroup(agedstM, agedstF, agedstWHOM, agedstWHOF, WHOIndex, cntx)
                        ''Regionalizing Resp. Inf.
                        LoadRespInfData(modmordstdet, cntx)
                        LoadMentalHealthData(WHOIndex, agedstM, agedstF, agedstWHOM, agedstWHOF, cntx)
                    End If
                    If Not cntx.HistoricMode Then
                        LoadRatioYLDtoYLL()
                    End If
                    If Not cntx.HistoricMode Then
                        LoadDEATHTRPV(cntx, agedstM, agedstF)
                    End If
#End Region
                    Me.dataloaded = True

                End If
                '=========================
                Dim series As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim weightSeries As IDictionary(Of String, Single?)
                weightSeries = CPOP
                Dim MinVal As Single, MaxVal As Single, bLimits As Boolean = False
                Dim bSum As Boolean = False
                Select Case seriesName
                    Case "HLBMI_Male"
                        series = Me.CBMIM
                    Case "HLBMI_Female"
                        series = Me.CBMIF
                    Case "HLBMI_Total"
                        series = Me.CBMIF ' calculate average below
                    Case "HLSMOKING_Male"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CHLSMOKINGMALE
                    Case "HLSMOKING_Female"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CHLSMOKINGFEM
                    Case "HLSMOKING_Total"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CHLSMOKINGFEM ' calculate average below
                    Case "HLSMOKINGIMP_Age30_44_Male"
                        Dim vHLSMOKINGIMP As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLSMOKINGIMP.Add(ICount%, HLSMOKINGIMP(ICount%, 1, 1))
                        Next ICount%
                        Return vHLSMOKINGIMP
                    Case "HLSMOKINGIMP_Age45_59_Male"
                        Dim vHLSMOKINGIMP As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLSMOKINGIMP.Add(ICount%, HLSMOKINGIMP(ICount%, 2, 1))
                        Next ICount%
                        Return vHLSMOKINGIMP
                    Case "HLSMOKINGIMP_Age60_69_Male"
                        Dim vHLSMOKINGIMP As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLSMOKINGIMP.Add(ICount%, HLSMOKINGIMP(ICount%, 3, 1))
                        Next ICount%
                        Return vHLSMOKINGIMP
                    Case "HLSMOKINGIMP_Age70Plus_Male"
                        Dim vHLSMOKINGIMP As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLSMOKINGIMP.Add(ICount%, HLSMOKINGIMP(ICount%, 4, 1))
                        Next ICount%
                        Return vHLSMOKINGIMP
                    Case "HLSMOKINGIMP_Age30_44_Female"
                        Dim vHLSMOKINGIMP As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLSMOKINGIMP.Add(ICount%, HLSMOKINGIMP(ICount%, 1, 2))
                        Next ICount%
                        Return vHLSMOKINGIMP
                    Case "HLSMOKINGIMP_Age45_59_Female"
                        Dim vHLSMOKINGIMP As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLSMOKINGIMP.Add(ICount%, HLSMOKINGIMP(ICount%, 2, 2))
                        Next ICount%
                        Return vHLSMOKINGIMP
                    Case "HLSMOKINGIMP_Age60_69_Female"
                        Dim vHLSMOKINGIMP As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLSMOKINGIMP.Add(ICount%, HLSMOKINGIMP(ICount%, 3, 2))
                        Next ICount%
                        Return vHLSMOKINGIMP
                    Case "HLSMOKINGIMP_Age70Plus_Female"
                        Dim vHLSMOKINGIMP As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLSMOKINGIMP.Add(ICount%, HLSMOKINGIMP(ICount%, 4, 2))
                        Next ICount%
                        Return vHLSMOKINGIMP
                    Case "HLDIABETESRR_Age15_29_Male"
                        Dim vHLDIABETESRR As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLDIABETESRR.Add(ICount%, HLDIABETESRR(ICount%, 1, 1))
                        Next ICount%
                        Return vHLDIABETESRR
                    Case "HLDIABETESRR_Age30_44_Male"
                        Dim vHLDIABETESRR As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLDIABETESRR.Add(ICount%, HLDIABETESRR(ICount%, 2, 1))
                        Next ICount%
                        Return vHLDIABETESRR
                    Case "HLDIABETESRR_Age45_59_Male"
                        Dim vHLDIABETESRR As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLDIABETESRR.Add(ICount%, HLDIABETESRR(ICount%, 3, 1))
                        Next ICount%
                        Return vHLDIABETESRR
                    Case "HLDIABETESRR_Age60_69_Male"
                        Dim vHLDIABETESRR As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLDIABETESRR.Add(ICount%, HLDIABETESRR(ICount%, 4, 1))
                        Next ICount%
                        Return vHLDIABETESRR
                    Case "HLDIABETESRR_Age70_79_Male"
                        Dim vHLDIABETESRR As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLDIABETESRR.Add(ICount%, HLDIABETESRR(ICount%, 5, 1))
                        Next ICount%
                        Return vHLDIABETESRR
                    Case "HLDIABETESRR_Age80Plus_Male"
                        Dim vHLDIABETESRR As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLDIABETESRR.Add(ICount%, HLDIABETESRR(ICount%, 6, 1))
                        Next ICount%
                        Return vHLDIABETESRR
                    Case "HLDIABETESRR_Age15_29_Female"
                        Dim vHLDIABETESRR As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLDIABETESRR.Add(ICount%, HLDIABETESRR(ICount%, 1, 2))
                        Next ICount%
                        Return vHLDIABETESRR
                    Case "HLDIABETESRR_Age30_44_Female"
                        Dim vHLDIABETESRR As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLDIABETESRR.Add(ICount%, HLDIABETESRR(ICount%, 2, 2))
                        Next ICount%
                        Return vHLDIABETESRR
                    Case "HLDIABETESRR_Age45_59_Female"
                        Dim vHLDIABETESRR As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLDIABETESRR.Add(ICount%, HLDIABETESRR(ICount%, 3, 2))
                        Next ICount%
                        Return vHLDIABETESRR
                    Case "HLDIABETESRR_Age60_69_Female"
                        Dim vHLDIABETESRR As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLDIABETESRR.Add(ICount%, HLDIABETESRR(ICount%, 4, 2))
                        Next ICount%
                        Return vHLDIABETESRR
                    Case "HLDIABETESRR_Age70_79_Female"
                        Dim vHLDIABETESRR As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLDIABETESRR.Add(ICount%, HLDIABETESRR(ICount%, 5, 2))
                        Next ICount%
                        Return vHLDIABETESRR
                    Case "HLDIABETESRR_Age80Plus_Female"
                        Dim vHLDIABETESRR As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For ICount% = 1 To NR
                            vHLDIABETESRR.Add(ICount%, HLDIABETESRR(ICount%, 6, 2))
                        Next ICount%
                        Return vHLDIABETESRR
                    Case "HLOBESITY_Male"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CHLOBESITYMALE
                    Case "HLOBESITY_Female"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CHLOBESITYFEM
                    Case "HLOBESITY_Total"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CHLOBESITYFEM ' calculate average below
                    Case "HLDIABIGT"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CHLDIABIGT
                    Case "HLDIABPREV"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CHLDIABPREV
                    Case "hlsmokingtcs"
                        bLimits = False
                        series = CTCS
                    Case "HLSTUNT"
                        series = CHLSTUNT
                    Case "MALNCHPSAM"
                        series = CMALNCHPSAM
                    Case "SAMAVERTED"
                        series = CSAMAVERTED
                    Case "AIDSDTHS"
                        Return AIDSDTHS
                    Case "CHILDDTHR"
                        Return CHILDDTHR
                    Case "HLMALARIAINC"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000000000
                        series = CHLMALARIAINC
                    Case "HLMALARIAPREV"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000000000
                        series = CHLMALARIAPREV
                    Case "HLMALARIAMORT"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000000000
                        series = CHLMALARIAMORT
                    Case "HLHIVINC"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000000000
                        series = CHLHIVINC
                    Case "HLHIVPREV"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000000000
                        series = CHLHIVPREV
                    Case "HLHIVMORT"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000000000
                        series = CHLHIVMORT
                    Case "HLBEDNETS"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000000000
                        series = CHLBEDNETS
                    Case "HLHIVART"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000000000
                        series = CHLHIVART
                    Case "HLCONDOMUSE"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000000000
                        series = CHLCONDOMUSE
                    Case "HLSTUNTCHL"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000000000
                        series = CHLSTUNTCHL
                    Case "VEHICFLPC"
                        Return VEHICFLPC
                    Case Else
                        'Throw New ArgumentException($"Aggregator cannot be applied on series {seriesName}.")
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, Nothing)
                        Next
                End Select

                If seriesName = "HLBMI_Total" Then
                    'series = Me.CBMIF
                    'Average
                    Return (From s In series
                            Join r In cntx.Regionalization On s.Key Equals r.Key
                            Join m In Me.CBMIM On s.Key Equals m.Key
                            Join w In weightSeries On s.Key Equals w.Key
                            Group Value = (s.Value + m.Value) / 2, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
                           )
                ElseIf seriesName = "HLOBESITY_Total" Then
                    Return (From s In series
                            Join r In cntx.Regionalization On s.Key Equals r.Key
                            Join m In Me.CHLOBESITYMALE On s.Key Equals m.Key
                            Join w In weightSeries On s.Key Equals w.Key
                            Group Value = (s.Value + m.Value) / 2, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                       ).ToDictionary(
                          Function(grp) grp.RegionId,
                          Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
                          )
                ElseIf seriesName = "HLSMOKING_Total" Then
                    Return (From s In series
                            Join r In cntx.Regionalization On s.Key Equals r.Key
                            Join m In Me.CHLSMOKINGMALE On s.Key Equals m.Key
                            Join w In weightSeries On s.Key Equals w.Key
                            Group Value = (s.Value + m.Value) / 2, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                       ).ToDictionary(
                          Function(grp) grp.RegionId,
                          Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
                          )
                    'ElseIf seriesName = "HLSMOKINGIMP_Total" Then
                    '    Return (From s In series
                    '            Join r In cntx.Regionalization On s.Key Equals r.Key
                    '            Join m In Me.CHLSMOKINGIMPMALE On s.Key Equals m.Key
                    '            Join w In weightSeries On s.Key Equals w.Key
                    '            Group Value = (s.Value + m.Value) / 2, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                    '       ).ToDictionary(
                    '          Function(grp) grp.RegionId,
                    '          Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
                    '          )
                ElseIf bLimits Then
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

            'jrs 2024/12/03 changed MaleAIDSPortion from SeriesEstimator to Dictionary
            Private Sub ComputeAIDsMortDist(N As Integer, RegionName As String, agedstM As Dictionary(Of String, Dictionary(Of String, Single?)), agedstF As Dictionary(Of String, Dictionary(Of String, Single?)),
                                               aidsdeathsbyageM As Single?(), aidsdeathsbyageF As Single?(),
                                              ByRef AidsMortDistM As Dictionary(Of String, Dictionary(Of String, Single?)), ByRef AidsMortDistF As Dictionary(Of String, Dictionary(Of String, Single?)),
                                               MaleAIDSPortion As Dictionary(Of String, Single?),
                                               cntx As IRebuildFacade)
                'ttt->ByRef
                Dim maleaidsdths As Single, femaleaidsdths As Single, AidsDeaths As Single, aidsdthscoh As Single
                maleaidsdths = AIDSDTHS(N) * MaleAIDSPortion(RegionName)
                femaleaidsdths = AIDSDTHS(N) * (1 - MaleAIDSPortion(RegionName))

                Dim valM As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim valF As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For j = 0 To cntx.NCohorts
                    For k = 1 To 2
                        If k = 1 Then 'Male
                            If j = 0 Then
                                valM = New Dictionary(Of String, Single?)
                            End If
                            AidsDeaths = maleaidsdths
                            'If agedstM(j, RegionName%, k) <> 0 Then
                            If agedstM(RegionName)("Cohort" & j.ToString).Value <> 0 Then
                                'NO RegionName
                                aidsdthscoh = AidsDeaths * aidsdeathsbyageM(j).Value / agedstM(RegionName)("Cohort" & j.ToString).Value
                            Else
                                aidsdthscoh = 0
                            End If
                            valM.Add("Cohort" & j.ToString, aidsdthscoh)
                            'AidsMortDist(j, RegionName, k) = aidsdthscoh
                        Else 'Female
                            If j = 0 Then
                                valF = New Dictionary(Of String, Single?)
                            End If
                            AidsDeaths = femaleaidsdths
                            If agedstF(RegionName)("Cohort" & j.ToString).Value <> 0 Then
                                aidsdthscoh = AidsDeaths * aidsdeathsbyageF(j) / agedstF(RegionName)("Cohort" & j.ToString).Value
                            Else
                                aidsdthscoh = 0
                            End If
                            'AidsMortDist(j, RegionName, k) = aidsdthscoh
                            valF.Add("Cohort" & j.ToString, aidsdthscoh)
                        End If
                    Next k 'Sex
                    If j = cntx.NCohorts Then
                        If AidsMortDistM.ContainsKey(RegionName) Then
                            AidsMortDistM(RegionName) = valM
                        Else
                            AidsMortDistM.Add(RegionName, valM)
                        End If
                        If AidsMortDistF.ContainsKey(RegionName) Then
                            AidsMortDistF(RegionName) = valF
                        Else
                            AidsMortDistF.Add(RegionName, valF)
                        End If
                    End If
                Next j 'cohort
            End Sub

            'jrs 2024/12/03 changed MaleAIDSPortion from SeriesEstimator to Dictionary
            Private Sub ComputeAIDsMortDistGBD(N As Integer, RegionName As String, agedstM As Dictionary(Of String, Dictionary(Of String, Single?)), agedstF As Dictionary(Of String, Dictionary(Of String, Single?)),
                                               aidsdeathsbyageGBDM As Dictionary(Of String, Dictionary(Of String, Single?)), aidsdeathsbyageGBDF As Dictionary(Of String, Dictionary(Of String, Single?)),
                                              ByRef AidsMortDistM As Dictionary(Of String, Dictionary(Of String, Single?)), ByRef AidsMortDistF As Dictionary(Of String, Dictionary(Of String, Single?)),
                                               MaleAIDSPortion As Dictionary(Of String, Single?),
                                               cntx As IRebuildFacade)
                'ttt->ByRef
                Dim maleaidsdths As Single?, femaleaidsdths As Single?, AidsDeaths As Single?, aidsdthscoh As Single?
                maleaidsdths = AIDSDTHS(N) * MaleAIDSPortion(RegionName)
                femaleaidsdths = AIDSDTHS(N) * (1 - MaleAIDSPortion(RegionName))
                Dim valM As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim valF As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For j = 0 To cntx.NCohorts
                    For k = 1 To 2
                        If k = 1 Then
                            If j = 0 Then
                                valM = New Dictionary(Of String, Single?)
                            End If
                            AidsDeaths = maleaidsdths
                            'If agedstM(j, R%, k) <> 0 Then
                            If agedstM(RegionName)("Cohort" & j.ToString).Value <> 0 Then
                                aidsdthscoh = AidsDeaths * If(aidsdeathsbyageGBDM.ContainsKey(RegionName), aidsdeathsbyageGBDM(RegionName)("Cohort" & j.ToString), 0) / agedstM(RegionName)("Cohort" & j.ToString).Value
                            Else
                                aidsdthscoh = 0
                            End If
                            valM.Add("Cohort" & j.ToString, aidsdthscoh)
                            'AidsMortDist(j, R, k) = aidsdthscoh
                        Else
                            If j = 0 Then
                                valF = New Dictionary(Of String, Single?)
                            End If
                            AidsDeaths = femaleaidsdths
                            If agedstF(RegionName)("Cohort" & j.ToString).Value <> 0 Then
                                aidsdthscoh = AidsDeaths * If(aidsdeathsbyageGBDF.ContainsKey(RegionName), aidsdeathsbyageGBDF(RegionName)("Cohort" & j.ToString), 0) / agedstF(RegionName)("Cohort" & j.ToString).Value
                            Else
                                aidsdthscoh = 0
                            End If
                            'AidsMortDist(j, R, k) = aidsdthscoh
                            valF.Add("Cohort" & j.ToString, aidsdthscoh)
                        End If
                    Next k 'Sex
                    If j = cntx.NCohorts Then
                        If Not AidsMortDistM.ContainsKey(RegionName) Then
                            AidsMortDistM.Add(RegionName, valM)
                        Else
                            AidsMortDistM(RegionName) = valM
                        End If
                        If Not AidsMortDistF.ContainsKey(RegionName) Then
                            AidsMortDistF.Add(RegionName, valF)
                        Else
                            AidsMortDistF(RegionName) = valF
                        End If


                    End If
                Next j 'cohort
            End Sub

            Public Sub LoadAIDSDthbyAge(ByRef aidsdeathsbyageM As Single?(), ByRef aidsdeathsbyageF As Single?(), cntx As IRebuildFacade)
                Dim ValM As PopCohortGlobal
                Dim ValF As PopCohortGlobal
                Using db2 As IFs2Context = New IFs2Context()

                    'ValM = db2.PopCohortGlobal.Find(Function(x As PopCohortGlobal) x.Variable = "AIDSDeathsByAgeM")
                    ValM = db2.PopCohortGlobal.Where(Function(x) x.Variable = "AIDSDeathsByAgeM").FirstOrDefault
                    ValF = db2.PopCohortGlobal.Where(Function(x) x.Variable = "AIDSDeathsByAgeF").FirstOrDefault
                    'ValF = db2.PopCohortGlobal.Find(Function(x As PopCohortGlobal) x.Variable = "AIDSDeathsByAgeF")
                End Using
                Dim MaleDeaths As Single = 0
                Dim FemaleDeaths As Single = 0
                For j% = 0 To cntx.NCohorts  'Read differential AIDS deaths by age, males
                    aidsdeathsbyageM(j%) = Convert.ToSingle(GetType(PopCohortGlobal).GetProperty("Cohort" & j%.ToString).GetValue(ValM, Nothing))
                    If Not aidsdeathsbyageM(j%) Is Nothing Then
                        MaleDeaths = MaleDeaths + aidsdeathsbyageM(j%)
                    End If
                    aidsdeathsbyageF(j%) = Convert.ToSingle(GetType(PopCohortGlobal).GetProperty("Cohort" & j%.ToString).GetValue(ValF, Nothing))
                    If Not aidsdeathsbyageM(j%) Is Nothing Then
                        FemaleDeaths = FemaleDeaths + aidsdeathsbyageF(j%)
                    End If
                Next j%
                For j% = 0 To cntx.NCohorts 'Normalize deaths by age to portion of total
                    aidsdeathsbyageM(j%) = aidsdeathsbyageM(j%) / MaleDeaths
                    aidsdeathsbyageF(j%) = aidsdeathsbyageF(j%) / FemaleDeaths
                Next j%
            End Sub
            Public Sub LoadAIDSDthbyAgeHeavy(ByRef aidsdeathsbyageM As Single?(), ByRef aidsdeathsbyageF As Single?(), cntx As IRebuildFacade)
                Dim ValM As PopCohortGlobal
                Dim ValF As PopCohortGlobal
                Using db2 As IFs2Context = New IFs2Context()
                    'ValM = db2.PopCohortGlobal.Find(Function(x As PopCohortGlobal) x.Variable = "AIDSDeathsByAgeMHeavy")
                    'ValF = db2.PopCohortGlobal.Find(Function(x As PopCohortGlobal) x.Variable = "AIDSDeathsByAgeFHeavy")
                    ValM = db2.PopCohortGlobal.Where(Function(x) x.Variable = "AIDSDeathsByAgeMHeavy").FirstOrDefault
                    ValF = db2.PopCohortGlobal.Where(Function(x) x.Variable = "AIDSDeathsByAgeFHeavy").FirstOrDefault
                End Using
                Dim MaleDeaths As Single = 0
                Dim FemaleDeaths As Single = 0
                For j% = 0 To cntx.NCohorts  'Read differential AIDS deaths by age, males
                    aidsdeathsbyageM(j%) = Convert.ToSingle(GetType(PopCohortGlobal).GetProperty("Cohort" & j%.ToString).GetValue(ValM, Nothing))
                    If Not aidsdeathsbyageM(j%) Is Nothing Then
                        MaleDeaths = MaleDeaths + aidsdeathsbyageM(j%)
                    End If
                    aidsdeathsbyageF(j%) = Convert.ToSingle(GetType(PopCohortGlobal).GetProperty("Cohort" & j%.ToString).GetValue(ValF, Nothing))
                    If Not aidsdeathsbyageM(j%) Is Nothing Then
                        FemaleDeaths = FemaleDeaths + aidsdeathsbyageF(j%)
                    End If
                Next j%
                For j% = 0 To cntx.NCohorts 'Normalize deaths by age to portion of total
                    aidsdeathsbyageM(j%) = aidsdeathsbyageM(j%) / MaleDeaths
                    aidsdeathsbyageF(j%) = aidsdeathsbyageF(j%) / FemaleDeaths
                Next j%
            End Sub
            Public Sub LoadAIDSDthbyAgeGBD(ByRef aidsdeathsbyageGBDM As Dictionary(Of String, Dictionary(Of String, Single?)), ByRef aidsdeathsbyageGBDF As Dictionary(Of String, Dictionary(Of String, Single?)),
                                           ByVal aidsdeathsbyageM As Single?(), ByVal aidsdeathsbyageF As Single?(),
                                           ByRef aidsdeathsbyageHeavyM As Single?(), ByRef aidsdeathsbyageHeavyF As Single?(),
                                           ByVal agedstM As Dictionary(Of String, Dictionary(Of String, Single?)), ByVal agedstF As Dictionary(Of String, Dictionary(Of String, Single?)),
                                           ByVal cntx As IRebuildFacade, ByRef MalePortion As Dictionary(Of String, Single?))
                Dim iGender As Integer '1 = Male, 2 = Female
                Dim R As Integer, j As Integer
                Dim RN As String
                Dim DEATHS As Single, Deaths85 As Single
                Dim RegDist As Single, HeavyDist As Single
                'jrs 2024/12/03
                Dim DEATHSM As Single, DEATHSF As Single
                Dim TbName$ = "GDP/Capita (PPP) Versus HIV Infections, Male Portion"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                Dim MaleAIDSPortionEst = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)

                Using db2 As IFs2Context = New IFs2Context()
                    For Each valrec As HealthDetailedDeathsRegs In db2.HealthDetailedDeathsRegs.Where(Function(x) x.CauseID = 11)
                        iGender = valrec.sex
                        R = valrec.RegNum
                        RN = valrec.RegName
                        DEATHS = 0
                        Deaths85 = 0
                        RegDist = 0
                        HeavyDist = 0
                        If iGender = 1 Then 'Male
                            Dim ValaidsdeathsbyageGBDM = New Dictionary(Of String, Single?)
                            For k = 0 To cntx.NCohorts
                                ValaidsdeathsbyageGBDM.Add("Cohort" & k.ToString, Nothing)
                            Next
                            'Infants
                            'ValaidsdeathsbyageGBDM("Cohort0") = CSng(valrec.R0) * If(agedstM.ContainsKey(R) Andalso agedstM(R).HasValue,agedstM(R),0)("Cohort0") / 1000
                            ValaidsdeathsbyageGBDM("Cohort0") = CSng(valrec.R0) * If(agedstM.ContainsKey(RN), agedstM(RN)("Cohort0"), 0) / 1000
                            DEATHS = DEATHS + ValaidsdeathsbyageGBDM("Cohort0").Value
                            'Children 1-4
                            'ValaidsdeathsbyageGBDM("Cohort1") = CSng(valrec.R1) * If(agedstM.ContainsKey(R) Andalso agedstM(R).HasValue,agedstM(R),0)("Cohort1") / 1000
                            ValaidsdeathsbyageGBDM("Cohort1") = CSng(valrec.R1) * If(agedstM.ContainsKey(RN), agedstM(RN)("Cohort1"), 0) / 1000
                            DEATHS = DEATHS + ValaidsdeathsbyageGBDM("Cohort1").Value
                            'Redistribution of children <5 since GBD data is 0-4
                            'Low HIV Rate
                            If HIVRATE.ContainsKey(R) AndAlso HIVRATE(R).HasValue Then
                                If HIVRATE(R) < 10 Then
                                    ValaidsdeathsbyageGBDM("Cohort0") = DEATHS * aidsdeathsbyageM(0) / (aidsdeathsbyageM(0) + aidsdeathsbyageM(1))
                                    ValaidsdeathsbyageGBDM("Cohort1") = DEATHS * aidsdeathsbyageM(1) / (aidsdeathsbyageM(0) + aidsdeathsbyageM(1))
                                Else
                                    ValaidsdeathsbyageGBDM("Cohort0") = DEATHS * aidsdeathsbyageHeavyM(0) / (aidsdeathsbyageHeavyM(0) + aidsdeathsbyageHeavyM(1))
                                    ValaidsdeathsbyageGBDM("Cohort1") = DEATHS * aidsdeathsbyageHeavyM(1) / (aidsdeathsbyageHeavyM(0) + aidsdeathsbyageHeavyM(1))
                                End If
                            End If

                            For j% = 2 To 17 'Read differential AIDS deaths by age, males
                                Dim curVal = CSng(GetType(HealthDetailedDeathsRegs).GetProperty("R" & (j + 1).ToString).GetValue(valrec, Nothing))
                                ValaidsdeathsbyageGBDM("Cohort" & j.ToString) = curVal * If(agedstM.ContainsKey(RN), agedstM(RN)("Cohort" & j.ToString), 0) / 1000 'jrs 2011/09/19 now it's deaths per 1000
                                DEATHS = DEATHS + ValaidsdeathsbyageGBDM("Cohort" & j.ToString)
                            Next j%
                            '85+
                            For j = 18 To cntx.NCohorts
                                If aidsdeathsbyageM(j).HasValue OrElse aidsdeathsbyageHeavyM(j).HasValue Then
                                    Dim curVal = CSng(GetType(HealthDetailedDeathsRegs).GetProperty("R19").GetValue(valrec, Nothing))
                                    ValaidsdeathsbyageGBDM("Cohort" & j.ToString) = curVal * If(agedstM.ContainsKey(RN), agedstM(RN)("Cohort" & j.ToString), 0) / 1000 'jrs 2011/09/19 now it's deaths per 1000
                                    DEATHS = DEATHS + ValaidsdeathsbyageGBDM("Cohort" & j.ToString)
                                    Deaths85 = Deaths85 + ValaidsdeathsbyageGBDM("Cohort" & j.ToString)
                                End If
                                If aidsdeathsbyageM(j).HasValue Then
                                    RegDist = RegDist + aidsdeathsbyageM(j)
                                End If
                                If aidsdeathsbyageHeavyM(j).HasValue Then
                                    HeavyDist = HeavyDist + aidsdeathsbyageHeavyM(j)
                                End If
                            Next j
                            'Redistribution of 85+, since GBD data is in a single group for 85+
                            'if there are deaths
                            If Deaths85 > 0 Then
                                'Low HIV Rate
                                If HIVRATE.ContainsKey(R) AndAlso HIVRATE(R).HasValue Then
                                    If HIVRATE(R) < 10 Then
                                        If RegDist > 0 Then
                                            For j = 18 To cntx.NCohorts
                                                ValaidsdeathsbyageGBDM("Cohort" & j.ToString) = Deaths85 * aidsdeathsbyageM(j) / RegDist
                                            Next j
                                        End If
                                    Else
                                        If HeavyDist Then
                                            For j = 18 To cntx.NCohorts
                                                ValaidsdeathsbyageGBDM("Cohort" & j.ToString) = Deaths85 * aidsdeathsbyageHeavyM(j) / HeavyDist
                                            Next j
                                        End If
                                    End If
                                End If

                            End If
                            If DEATHS > 0 Then
                                For j% = 0 To cntx.NCohorts 'Normalize deaths by age to portion of total
                                    ValaidsdeathsbyageGBDM("Cohort" & j.ToString) = ValaidsdeathsbyageGBDM("Cohort" & j.ToString) / DEATHS
                                Next j%
                            Else 'No distribution from GBD, keep using the old one
                                If HIVRATE.ContainsKey(R) AndAlso HIVRATE(R).HasValue Then
                                    If HIVRATE(R) < 10 Then
                                        For j% = 0 To cntx.NCohorts 'Normalize deaths by age to portion of total
                                            ValaidsdeathsbyageGBDM("Cohort" & j.ToString) = aidsdeathsbyageM(j%)
                                        Next j%
                                    Else
                                        For j% = 0 To cntx.NCohorts 'Normalize deaths by age to portion of total
                                            ValaidsdeathsbyageGBDM("Cohort" & j.ToString) = aidsdeathsbyageHeavyM(j%)
                                        Next j%
                                    End If
                                End If
                            End If
                            '------
                            aidsdeathsbyageGBDM.Add(RN, ValaidsdeathsbyageGBDM)
                            'jrs 2024/12/03
                            DEATHSM = DEATHS
                        Else 'Female ---------------------------------------------------------
                            'ttt- Copy all above, replace M with F
                            Dim ValaidsdeathsbyageGBDF = New Dictionary(Of String, Single?)
                            For k = 0 To cntx.NCohorts
                                ValaidsdeathsbyageGBDF.Add("Cohort" & k.ToString, Nothing)
                            Next
                            'Infants
                            ValaidsdeathsbyageGBDF("Cohort0") = CSng(valrec.R0) * If(agedstF.ContainsKey(RN), agedstF(RN)("Cohort0"), 0) / 1000
                            DEATHS = DEATHS + ValaidsdeathsbyageGBDF("Cohort0").Value
                            'Children 1-4
                            ValaidsdeathsbyageGBDF("Cohort1") = CSng(valrec.R1) * If(agedstF.ContainsKey(RN), agedstF(RN)("Cohort1"), 0) / 1000
                            DEATHS = DEATHS + ValaidsdeathsbyageGBDF("Cohort1").Value
                            'Redistribution of children <5 since GBD data is 0-4
                            'Low HIV Rate
                            'ttt VB6: R
                            If HIVRATE.ContainsKey(R) AndAlso HIVRATE(R).HasValue Then
                                If HIVRATE(R) < 10 Then
                                    ValaidsdeathsbyageGBDF("Cohort0") = DEATHS * aidsdeathsbyageF(0) / (aidsdeathsbyageF(0) + aidsdeathsbyageF(1))
                                    ValaidsdeathsbyageGBDF("Cohort1") = DEATHS * aidsdeathsbyageF(1) / (aidsdeathsbyageF(0) + aidsdeathsbyageF(1))
                                Else
                                    ValaidsdeathsbyageGBDF("Cohort0") = DEATHS * aidsdeathsbyageHeavyF(0) / (aidsdeathsbyageHeavyF(0) + aidsdeathsbyageHeavyF(1))
                                    ValaidsdeathsbyageGBDF("Cohort1") = DEATHS * aidsdeathsbyageHeavyF(1) / (aidsdeathsbyageHeavyF(0) + aidsdeathsbyageHeavyF(1))
                                End If
                            End If

                            For j% = 2 To 17 'Read differential AIDS deaths by age, males
                                Dim curVal = CSng(GetType(HealthDetailedDeathsRegs).GetProperty("R" & (j + 1).ToString).GetValue(valrec, Nothing))
                                ValaidsdeathsbyageGBDF("Cohort" & j.ToString) = curVal * If(agedstF.ContainsKey(RN), agedstF(RN)("Cohort" & j.ToString), 0) / 1000 'jrs 2011/09/19 now it's deaths per 1000
                                DEATHS = DEATHS + ValaidsdeathsbyageGBDF("Cohort" & j.ToString)
                            Next j%
                            '85+
                            For j = 18 To cntx.NCohorts
                                If aidsdeathsbyageF(j).HasValue OrElse aidsdeathsbyageHeavyF(j).HasValue Then
                                    Dim curVal = CSng(GetType(HealthDetailedDeathsRegs).GetProperty("R19").GetValue(valrec, Nothing))
                                    ValaidsdeathsbyageGBDF("Cohort" & j.ToString) = curVal * If(agedstF.ContainsKey(RN), agedstF(RN)("Cohort" & j.ToString), 0) / 1000 'jrs 2011/09/19 now it's deaths per 1000
                                    DEATHS = DEATHS + ValaidsdeathsbyageGBDF("Cohort" & j.ToString)
                                    Deaths85 = Deaths85 + ValaidsdeathsbyageGBDF("Cohort" & j.ToString)
                                End If
                                If aidsdeathsbyageF(j).HasValue Then
                                    RegDist = RegDist + aidsdeathsbyageF(j)
                                End If
                                If aidsdeathsbyageHeavyF(j).HasValue Then
                                    HeavyDist = HeavyDist + aidsdeathsbyageHeavyF(j)
                                End If
                            Next j
                            'Redistribution of 85+, since GBD data is in a single group for 85+
                            'if there are deaths
                            If Deaths85 > 0 Then
                                'Low HIV Rate
                                If HIVRATE.ContainsKey(R) AndAlso HIVRATE(R).HasValue Then
                                    If HIVRATE(R) < 10 Then
                                        If RegDist > 0 Then
                                            For j = 18 To cntx.NCohorts
                                                ValaidsdeathsbyageGBDF("Cohort" & j.ToString) = Deaths85 * aidsdeathsbyageF(j) / RegDist
                                            Next j
                                        End If
                                    Else
                                        If HeavyDist Then
                                            For j = 18 To cntx.NCohorts
                                                ValaidsdeathsbyageGBDF("Cohort" & j.ToString) = Deaths85 * aidsdeathsbyageHeavyF(j) / HeavyDist
                                            Next j
                                        End If
                                    End If
                                End If

                            End If
                            If DEATHS > 0 Then
                                For j% = 0 To cntx.NCohorts 'Normalize deaths by age to portion of total
                                    ValaidsdeathsbyageGBDF("Cohort" & j.ToString) = ValaidsdeathsbyageGBDF("Cohort" & j.ToString) / DEATHS
                                Next j%
                            Else 'No distribution from GBD, keep using the old one
                                If HIVRATE.ContainsKey(R) AndAlso HIVRATE(R).HasValue Then
                                    If HIVRATE(R) < 10 Then
                                        For j% = 0 To cntx.NCohorts 'Normalize deaths by age to portion of total
                                            ValaidsdeathsbyageGBDF("Cohort" & j.ToString) = aidsdeathsbyageF(j%)
                                        Next j%
                                    Else
                                        For j% = 0 To cntx.NCohorts 'Normalize deaths by age to portion of total
                                            ValaidsdeathsbyageGBDF("Cohort" & j.ToString) = aidsdeathsbyageHeavyF(j%)
                                        Next j%
                                    End If
                                End If
                            End If
                            '------
                            aidsdeathsbyageGBDF.Add(RN, ValaidsdeathsbyageGBDF)
                            'jrs 2024/12/03
                            DEATHSF = DEATHS
                            DEATHS = DEATHSF + DEATHSM
                            If DEATHS > 0 Then
                                MalePortion.Add(RN, DEATHSM / DEATHS)
                            Else
                                MalePortion.Add(RN, MaleAIDSPortionEst(RN))
                            End If
                        End If
                    Next valrec
                End Using
            End Sub

            Public Sub ReadMortCohortDist(ByRef BasMordstM As Dictionary(Of String, Dictionary(Of String, Single?)),
                     ByRef BasMordstF As Dictionary(Of String, Dictionary(Of String, Single?)), cntx As IRebuildFacade)
                'BasMordst As Variant, coname As Variant, db As Database)
                Dim msg$ = ""
                Using db2 As IFs2Context = New IFs2Context()
                    If cntx.HistoricMode Then
                        '  Set valtbl = db.OpenRecordset("PopMortalityCohortRegionMaleHist")
                        'jrs 2024/04/08 Dim qr = (From p In db2.PopMortalityCohortRegionMaleHist Select p).DistinctBy(Function(p) p.RegionName)
                        Dim qr = db2.PopMortalityCohortRegionMaleHist.AsQueryable().GroupBy(Function(p) p.RegionName).Select(Function(grp) grp.First())
                        For Each valrec As PopMortalityCohortRegionMaleHist In qr ' db2.PopMortalityCohortRegionMaleHist.OrderBy(Function(y) y.RegionId)
                            Dim rec = New Dictionary(Of String, Single?)
                            For j% = 0 To cntx.NCohorts
                                ' msg$ = "Fatal Error: BasMorDst, male, is null for " + RegName$(R%)
                                'valrec.GetType().GetProperty("Cohort" & j.ToString).GetValue(valrec, Nothing)
                                Select Case j
                                    Case 0
                                        If Not valrec.Cohort0.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort0 / 1000)
                                        End If
                                    Case 1
                                        If Not valrec.Cohort1.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort1 / 1000)
                                        End If
                                    Case 2
                                        If Not valrec.Cohort2.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort2 / 1000)
                                        End If
                                    Case 3
                                        If Not valrec.Cohort3.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort3 / 1000)
                                        End If
                                    Case 4
                                        If Not valrec.Cohort4.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort4 / 1000)
                                        End If
                                    Case 5
                                        If Not valrec.Cohort5.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort5 / 1000)
                                        End If
                                    Case 6
                                        If Not valrec.Cohort6.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort6 / 1000)
                                        End If
                                    Case 7
                                        If Not valrec.Cohort7.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort7 / 1000)
                                        End If
                                    Case 8
                                        If Not valrec.Cohort8.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort8 / 1000)
                                        End If
                                    Case 9
                                        If Not valrec.Cohort9.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort9 / 1000)
                                        End If
                                    Case 10
                                        If Not valrec.Cohort10.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort10 / 1000)
                                        End If
                                    Case 11
                                        If Not valrec.Cohort11.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort11 / 1000)
                                        End If
                                    Case 12
                                        If Not valrec.Cohort12.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort12 / 1000)
                                        End If
                                    Case 13
                                        If Not valrec.Cohort13.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort13 / 1000)
                                        End If
                                    Case 14
                                        If Not valrec.Cohort14.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort14 / 1000)
                                        End If
                                    Case 15
                                        If Not valrec.Cohort15.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort15 / 1000)
                                        End If
                                    Case 16
                                        If Not valrec.Cohort16.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort16 / 1000)
                                        End If
                                    Case 17
                                        If Not valrec.Cohort17.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort17 / 1000)
                                        End If
                                    Case 18
                                        If Not valrec.Cohort18.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort18 / 1000)
                                        End If
                                    Case 19
                                        If Not valrec.Cohort19.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort19 / 1000)
                                        End If
                                    Case 20
                                        If Not valrec.Cohort20.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort20 / 1000)
                                        End If
                                    Case 21
                                        If Not valrec.Cohort21.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort21 / 1000)
                                        End If
                                    Case 22
                                        If Not valrec.Cohort22.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort22 / 1000)
                                        End If
                                    Case 23
                                        If Not valrec.Cohort23.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort23 / 1000)
                                        End If
                                    Case 24
                                        If Not valrec.Cohort24.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort24 / 1000)
                                        End If
                                    Case 25
                                        If Not valrec.Cohort25.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort25 / 1000)
                                        End If
                                    Case 26
                                        If Not valrec.Cohort26.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort26 / 1000)
                                        End If
                                    Case 27
                                        If Not valrec.Cohort27.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort27 / 1000)
                                        End If
                                    Case 28
                                        If Not valrec.Cohort28.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort28 / 1000)
                                        End If
                                    Case 29
                                        If Not valrec.Cohort29.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort29 / 1000)
                                        End If
                                    Case 30
                                        If Not valrec.Cohort30.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort30 / 1000)
                                        End If
                                    Case 31
                                        If Not valrec.Cohort31.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort31 / 1000)
                                        End If
                                    Case 32
                                        If Not valrec.Cohort32.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort32 / 1000)
                                        End If
                                    Case 33
                                        If Not valrec.Cohort33.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort33 / 1000)
                                        End If
                                    Case 34
                                        If Not valrec.Cohort34.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort34 / 1000)
                                        End If
                                    Case 35
                                        If Not valrec.Cohort35.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort35 / 1000)
                                        End If
                                    Case 36
                                        If Not valrec.Cohort36.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort36 / 1000)
                                        End If
                                    Case 37
                                        If Not valrec.Cohort37.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort37 / 1000)
                                        End If
                                    Case 38
                                        If Not valrec.Cohort38.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort38 / 1000)
                                        End If
                                    Case 39
                                        If Not valrec.Cohort39.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort39 / 1000)
                                        End If
                                    Case 40
                                        If Not valrec.Cohort40.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort40 / 1000)
                                        End If
                                    Case 41
                                        If Not valrec.Cohort41.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort41 / 1000)
                                        End If
                                End Select
                            Next
                            BasMordstM.Add(valrec.RegionName, rec)
                        Next valrec
                        'jrs 2024/04/08 Dim qr2 = (From p In db2.PopMortalityCohortRegionFemaleHist Select p).DistinctBy(Function(p) p.RegionName)
                        Dim qr2 = db2.PopMortalityCohortRegionFemaleHist.AsQueryable().GroupBy(Function(p) p.RegionName).Select(Function(grp) grp.First())
                        For Each valrec As PopMortalityCohortRegionFemaleHist In qr2 'db2.PopMortalityCohortRegionFemaleHist.OrderBy(Function(y) y.RegionId)
                            Dim rec = New Dictionary(Of String, Single?)
                            For j% = 0 To cntx.NCohorts
                                ' msg$ = "Fatal Error: BasMorDst, male, is null for " + RegName$(R%)
                                'valrec.GetType().GetProperty("Cohort" & j.ToString).GetValue(valrec, Nothing)
                                Select Case j
                                    Case 0
                                        If Not valrec.Cohort0.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort0 / 1000)
                                        End If
                                    Case 1
                                        If Not valrec.Cohort1.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort1 / 1000)
                                        End If
                                    Case 2
                                        If Not valrec.Cohort2.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort2 / 1000)
                                        End If
                                    Case 3
                                        If Not valrec.Cohort3.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort3 / 1000)
                                        End If
                                    Case 4
                                        If Not valrec.Cohort4.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort4 / 1000)
                                        End If
                                    Case 5
                                        If Not valrec.Cohort5.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort5 / 1000)
                                        End If
                                    Case 6
                                        If Not valrec.Cohort6.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort6 / 1000)
                                        End If
                                    Case 7
                                        If Not valrec.Cohort7.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort7 / 1000)
                                        End If
                                    Case 8
                                        If Not valrec.Cohort8.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort8 / 1000)
                                        End If
                                    Case 9
                                        If Not valrec.Cohort9.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort9 / 1000)
                                        End If
                                    Case 10
                                        If Not valrec.Cohort10.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort10 / 1000)
                                        End If
                                    Case 11
                                        If Not valrec.Cohort11.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort11 / 1000)
                                        End If
                                    Case 12
                                        If Not valrec.Cohort12.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort12 / 1000)
                                        End If
                                    Case 13
                                        If Not valrec.Cohort13.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort13 / 1000)
                                        End If
                                    Case 14
                                        If Not valrec.Cohort14.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort14 / 1000)
                                        End If
                                    Case 15
                                        If Not valrec.Cohort15.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort15 / 1000)
                                        End If
                                    Case 16
                                        If Not valrec.Cohort16.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort16 / 1000)
                                        End If
                                    Case 17
                                        If Not valrec.Cohort17.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort17 / 1000)
                                        End If
                                    Case 18
                                        If Not valrec.Cohort18.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort18 / 1000)
                                        End If
                                    Case 19
                                        If Not valrec.Cohort19.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort19 / 1000)
                                        End If
                                    Case 20
                                        If Not valrec.Cohort20.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort20 / 1000)
                                        End If
                                    Case 21
                                        If Not valrec.Cohort21.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort21 / 1000)
                                        End If
                                    Case 22
                                        If Not valrec.Cohort22.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort22 / 1000)
                                        End If
                                    Case 23
                                        If Not valrec.Cohort23.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort23 / 1000)
                                        End If
                                    Case 24
                                        If Not valrec.Cohort24.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort24 / 1000)
                                        End If
                                    Case 25
                                        If Not valrec.Cohort25.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort25 / 1000)
                                        End If
                                    Case 26
                                        If Not valrec.Cohort26.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort26 / 1000)
                                        End If
                                    Case 27
                                        If Not valrec.Cohort27.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort27 / 1000)
                                        End If
                                    Case 28
                                        If Not valrec.Cohort28.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort28 / 1000)
                                        End If
                                    Case 29
                                        If Not valrec.Cohort29.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort29 / 1000)
                                        End If
                                    Case 30
                                        If Not valrec.Cohort30.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort30 / 1000)
                                        End If
                                    Case 31
                                        If Not valrec.Cohort31.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort31 / 1000)
                                        End If
                                    Case 32
                                        If Not valrec.Cohort32.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort32 / 1000)
                                        End If
                                    Case 33
                                        If Not valrec.Cohort33.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort33 / 1000)
                                        End If
                                    Case 34
                                        If Not valrec.Cohort34.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort34 / 1000)
                                        End If
                                    Case 35
                                        If Not valrec.Cohort35.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort35 / 1000)
                                        End If
                                    Case 36
                                        If Not valrec.Cohort36.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort36 / 1000)
                                        End If
                                    Case 37
                                        If Not valrec.Cohort37.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort37 / 1000)
                                        End If
                                    Case 38
                                        If Not valrec.Cohort38.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort38 / 1000)
                                        End If
                                    Case 39
                                        If Not valrec.Cohort39.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort39 / 1000)
                                        End If
                                    Case 40
                                        If Not valrec.Cohort40.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort40 / 1000)
                                        End If
                                    Case 41
                                        If Not valrec.Cohort41.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort41 / 1000)
                                        End If
                                End Select
                            Next
                            BasMordstF.Add(valrec.RegionName, rec)
                        Next valrec
                    Else
                        '   Set valtbl = db.OpenRecordset("PopMortalityCohortRegionMale")
                        'gvdb 2024/04/08
                        ' update query for new EF syntax
                        'Dim qr = (From p In db2.PopMortalityCohortRegionMale Select p).DistinctBy(Function(p) p.RegionName)
                        Dim qr = db2.PopMortalityCohortRegionMale.AsQueryable().GroupBy(Function(p) p.RegionName).Select(Function(grp) grp.First())
                        For Each valrec As PopMortalityCohortRegionMale In qr ' db2.PopMortalityCohortRegionMale.Select(Of PopMortalityCohortRegionMale).Distinct().OrderBy(Function(y) y.RegionId)
                            Dim rec = New Dictionary(Of String, Single?)
                            For j% = 0 To cntx.NCohorts
                                ' msg$ = "Fatal Error: BasMorDst, male, is null for " + RegName$(R%)
                                'valrec.GetType().GetProperty("Cohort" & j.ToString).GetValue(valrec, Nothing)
                                Select Case j
                                    Case 0
                                        If Not valrec.Cohort0.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort0 / 1000)
                                        End If
                                    Case 1
                                        If Not valrec.Cohort1.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort1 / 1000)
                                        End If
                                    Case 2
                                        If Not valrec.Cohort2.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort2 / 1000)
                                        End If
                                    Case 3
                                        If Not valrec.Cohort3.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort3 / 1000)
                                        End If
                                    Case 4
                                        If Not valrec.Cohort4.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort4 / 1000)
                                        End If
                                    Case 5
                                        If Not valrec.Cohort5.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort5 / 1000)
                                        End If
                                    Case 6
                                        If Not valrec.Cohort6.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort6 / 1000)
                                        End If
                                    Case 7
                                        If Not valrec.Cohort7.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort7 / 1000)
                                        End If
                                    Case 8
                                        If Not valrec.Cohort8.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort8 / 1000)
                                        End If
                                    Case 9
                                        If Not valrec.Cohort9.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort9 / 1000)
                                        End If
                                    Case 10
                                        If Not valrec.Cohort10.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort10 / 1000)
                                        End If
                                    Case 11
                                        If Not valrec.Cohort11.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort11 / 1000)
                                        End If
                                    Case 12
                                        If Not valrec.Cohort12.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort12 / 1000)
                                        End If
                                    Case 13
                                        If Not valrec.Cohort13.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort13 / 1000)
                                        End If
                                    Case 14
                                        If Not valrec.Cohort14.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort14 / 1000)
                                        End If
                                    Case 15
                                        If Not valrec.Cohort15.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort15 / 1000)
                                        End If
                                    Case 16
                                        If Not valrec.Cohort16.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort16 / 1000)
                                        End If
                                    Case 17
                                        If Not valrec.Cohort17.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort17 / 1000)
                                        End If
                                    Case 18
                                        If Not valrec.Cohort18.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort18 / 1000)
                                        End If
                                    Case 19
                                        If Not valrec.Cohort19.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort19 / 1000)
                                        End If
                                    Case 20
                                        If Not valrec.Cohort20.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort20 / 1000)
                                        End If
                                    Case 21
                                        If Not valrec.Cohort21.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort21 / 1000)
                                        End If
                                    Case 22
                                        If Not valrec.Cohort22.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort22 / 1000)
                                        End If
                                    Case 23
                                        If Not valrec.Cohort23.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort23 / 1000)
                                        End If
                                    Case 24
                                        If Not valrec.Cohort24.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort24 / 1000)
                                        End If
                                    Case 25
                                        If Not valrec.Cohort25.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort25 / 1000)
                                        End If
                                    Case 26
                                        If Not valrec.Cohort26.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort26 / 1000)
                                        End If
                                    Case 27
                                        If Not valrec.Cohort27.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort27 / 1000)
                                        End If
                                    Case 28
                                        If Not valrec.Cohort28.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort28 / 1000)
                                        End If
                                    Case 29
                                        If Not valrec.Cohort29.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort29 / 1000)
                                        End If
                                    Case 30
                                        If Not valrec.Cohort30.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort30 / 1000)
                                        End If
                                    Case 31
                                        If Not valrec.Cohort31.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort31 / 1000)
                                        End If
                                    Case 32
                                        If Not valrec.Cohort32.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort32 / 1000)
                                        End If
                                    Case 33
                                        If Not valrec.Cohort33.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort33 / 1000)
                                        End If
                                    Case 34
                                        If Not valrec.Cohort34.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort34 / 1000)
                                        End If
                                    Case 35
                                        If Not valrec.Cohort35.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort35 / 1000)
                                        End If
                                    Case 36
                                        If Not valrec.Cohort36.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort36 / 1000)
                                        End If
                                    Case 37
                                        If Not valrec.Cohort37.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort37 / 1000)
                                        End If
                                    Case 38
                                        If Not valrec.Cohort38.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort38 / 1000)
                                        End If
                                    Case 39
                                        If Not valrec.Cohort39.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort39 / 1000)
                                        End If
                                    Case 40
                                        If Not valrec.Cohort40.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort40 / 1000)
                                        End If
                                    Case 41
                                        If Not valrec.Cohort41.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort41 / 1000)
                                        End If
                                End Select
                            Next
                            BasMordstM.Add(valrec.RegionName, rec)
                        Next valrec
                        'gvdb 2024/04/08
                        ' update for new EF syntax
                        'Dim qr2 = (From p In db2.PopMortalityCohortRegionFemale Select p).DistinctBy(Function(p) p.RegionName)
                        Dim qr2 = db2.PopMortalityCohortRegionFemale.AsQueryable().GroupBy(Function(p) p.RegionName).Select(Function(grp) grp.First())
                        For Each valrec As PopMortalityCohortRegionFemale In qr2 'db2.PopMortalityCohortRegionFemale.OrderBy(Function(y) y.RegionId)
                            Dim rec = New Dictionary(Of String, Single?)
                            For j% = 0 To cntx.NCohorts
                                ' msg$ = "Fatal Error: BasMorDst, male, is null for " + RegName$(R%)
                                'valrec.GetType().GetProperty("Cohort" & j.ToString).GetValue(valrec, Nothing)
                                Select Case j
                                    Case 0
                                        If Not valrec.Cohort0.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort0 / 1000)
                                        End If
                                    Case 1
                                        If Not valrec.Cohort1.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort1 / 1000)
                                        End If
                                    Case 2
                                        If Not valrec.Cohort2.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort2 / 1000)
                                        End If
                                    Case 3
                                        If Not valrec.Cohort3.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort3 / 1000)
                                        End If
                                    Case 4
                                        If Not valrec.Cohort4.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort4 / 1000)
                                        End If
                                    Case 5
                                        If Not valrec.Cohort5.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort5 / 1000)
                                        End If
                                    Case 6
                                        If Not valrec.Cohort6.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort6 / 1000)
                                        End If
                                    Case 7
                                        If Not valrec.Cohort7.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort7 / 1000)
                                        End If
                                    Case 8
                                        If Not valrec.Cohort8.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort8 / 1000)
                                        End If
                                    Case 9
                                        If Not valrec.Cohort9.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort9 / 1000)
                                        End If
                                    Case 10
                                        If Not valrec.Cohort10.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort10 / 1000)
                                        End If
                                    Case 11
                                        If Not valrec.Cohort11.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort11 / 1000)
                                        End If
                                    Case 12
                                        If Not valrec.Cohort12.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort12 / 1000)
                                        End If
                                    Case 13
                                        If Not valrec.Cohort13.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort13 / 1000)
                                        End If
                                    Case 14
                                        If Not valrec.Cohort14.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort14 / 1000)
                                        End If
                                    Case 15
                                        If Not valrec.Cohort15.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort15 / 1000)
                                        End If
                                    Case 16
                                        If Not valrec.Cohort16.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort16 / 1000)
                                        End If
                                    Case 17
                                        If Not valrec.Cohort17.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort17 / 1000)
                                        End If
                                    Case 18
                                        If Not valrec.Cohort18.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort18 / 1000)
                                        End If
                                    Case 19
                                        If Not valrec.Cohort19.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort19 / 1000)
                                        End If
                                    Case 20
                                        If Not valrec.Cohort20.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort20 / 1000)
                                        End If
                                    Case 21
                                        If Not valrec.Cohort21.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort21 / 1000)
                                        End If
                                    Case 22
                                        If Not valrec.Cohort22.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort22 / 1000)
                                        End If
                                    Case 23
                                        If Not valrec.Cohort23.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort23 / 1000)
                                        End If
                                    Case 24
                                        If Not valrec.Cohort24.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort24 / 1000)
                                        End If
                                    Case 25
                                        If Not valrec.Cohort25.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort25 / 1000)
                                        End If
                                    Case 26
                                        If Not valrec.Cohort26.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort26 / 1000)
                                        End If
                                    Case 27
                                        If Not valrec.Cohort27.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort27 / 1000)
                                        End If
                                    Case 28
                                        If Not valrec.Cohort28.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort28 / 1000)
                                        End If
                                    Case 29
                                        If Not valrec.Cohort29.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort29 / 1000)
                                        End If
                                    Case 30
                                        If Not valrec.Cohort30.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort30 / 1000)
                                        End If
                                    Case 31
                                        If Not valrec.Cohort31.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort31 / 1000)
                                        End If
                                    Case 32
                                        If Not valrec.Cohort32.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort32 / 1000)
                                        End If
                                    Case 33
                                        If Not valrec.Cohort33.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort33 / 1000)
                                        End If
                                    Case 34
                                        If Not valrec.Cohort34.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort34 / 1000)
                                        End If
                                    Case 35
                                        If Not valrec.Cohort35.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort35 / 1000)
                                        End If
                                    Case 36
                                        If Not valrec.Cohort36.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort36 / 1000)
                                        End If
                                    Case 37
                                        If Not valrec.Cohort37.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort37 / 1000)
                                        End If
                                    Case 38
                                        If Not valrec.Cohort38.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort38 / 1000)
                                        End If
                                    Case 39
                                        If Not valrec.Cohort39.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort39 / 1000)
                                        End If
                                    Case 40
                                        If Not valrec.Cohort40.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort40 / 1000)
                                        End If
                                    Case 41
                                        If Not valrec.Cohort41.HasValue Then
                                            msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                            Debug.WriteLine(msg)
                                            rec.Add("Cohort" & j.ToString, Nothing)
                                        Else
                                            rec.Add("Cohort" & j.ToString, valrec.Cohort41 / 1000)
                                        End If
                                End Select
                            Next
                            BasMordstF.Add(valrec.RegionName, rec)
                        Next valrec
                    End If

                End Using
                'ttt: vb6: Region, vb.net:Country (Table Names)


            End Sub

            Private Sub LoadMortdist(ByRef modmordstM As Dictionary(Of String, Dictionary(Of String, Single?)),
                                     ByRef modmordstF As Dictionary(Of String, Dictionary(Of String, Single?)),
                                     cntx As IRebuildFacade) '(modmordst As Variant, coname As Variant)
                'Dim BasMordst()
                'ReDim BasMordst(NCohorts, NR, 2)
                Dim BasMordstM = New Dictionary(Of String, Dictionary(Of String, Single?))
                Dim BasMordstF = New Dictionary(Of String, Dictionary(Of String, Single?))
                ReadMortCohortDist(BasMordstM, BasMordstF, cntx)
                Dim R As Integer
                'jrs 2009/05/22 Dim mfactorMCohort As Integer, mfactorFCohort As Integer
                Dim mFactorM As Single, mFactorF As Single
                'jrs 2011/03/01
                Dim bCancelMsgMatchMortLE = False

                'jrs 2019/03/21
                Dim INFMORdata As Single, CHILDDRTdata As Single, FtoMmortRat As Single, FtoMmortRat2 As Single

                'Explained by Randall Kuhn
                'Life Expectancy was computed as the median age at death,
                'and it should be computed as a mean
                'average number of years of life given the survival rates in each age group
                For R = 1 To NR
                    Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                    'mfactor for Males

                    '        mfactorM = 0.5 / cumMort  '.5 represents 50% chance of dying younger,
                    '            ' and 50% change of dying older; hence life expectancy at birth
                    'If GRPS(1, R) = "Kuwait" Then Stop
                    'jrs 2019/03/21
                    FtoMmortRat = BasMordstF(RegionName)("Cohort0") / BasMordstM(RegionName)("Cohort0")
                    'If R = 38 Then Stop 'Congo DRC
                    'jrs 2026/03/17 added comments to remember how this was computed
                    'INFMOR(R) = (BasMordstF(RegionName)("Cohort0") * agedstF(RegionName)("Cohort0") + BasMordstM(RegionName)("Cohort0") * agedstM(RegionName)("Cohort0"))/(agedstF(RegionName)("Cohort0")+agedstM(RegionName)("Cohort0"))
                    'INFMOR(R)*(agedstF(RegionName)("Cohort0")+agedstM(RegionName)("Cohort0")) = (BasMordstF(RegionName)("Cohort0") * agedstF(RegionName)("Cohort0") + BasMordstM(RegionName)("Cohort0") * agedstM(RegionName)("Cohort0"))
                    'INFMOR(R)*(agedstF(RegionName)("Cohort0")+agedstM(RegionName)("Cohort0")) = (BasMordstM(RegionName)("Cohort0")*FtoMmortRat * agedstF(RegionName)("Cohort0") + BasMordstM(RegionName)("Cohort0") * agedstM(RegionName)("Cohort0"))
                    'INFMOR(R)*(agedstF(RegionName)("Cohort0")+agedstM(RegionName)("Cohort0")) = BasMordstM(RegionName)("Cohort0") (FtoMmortRat * agedstF(RegionName)("Cohort0") + agedstM(RegionName)("Cohort0"))
                    'BasMordstM(RegionName)("Cohort0") = INFMOR(R)*(agedstF(RegionName)("Cohort0")+agedstM(RegionName)("Cohort0")) / (FtoMmortRat * agedstF(RegionName)("Cohort0") + agedstM(RegionName)("Cohort0"))
                    INFMORdata = INFMOR(R) * (agedstM(RegionName)("Cohort0") + agedstF(RegionName)("Cohort0")) / (agedstM(RegionName)("Cohort0") + agedstF(RegionName)("Cohort0") * FtoMmortRat)
                    FtoMmortRat2 = (BasMordstF(RegionName)("Cohort0") + 4 * BasMordstF(RegionName)("Cohort1")) / (BasMordstM(RegionName)("Cohort0") + 4 * BasMordstM(RegionName)("Cohort1"))
                    CHILDDRTdata = CHILDDTHR(R) * (agedstM(RegionName)("Cohort0") + agedstF(RegionName)("Cohort0") + agedstM(RegionName)("Cohort1") + agedstF(RegionName)("Cohort1")) / (agedstM(RegionName)("Cohort0") + agedstM(RegionName)("Cohort1") + (agedstF(RegionName)("Cohort0") + agedstF(RegionName)("Cohort1")) * FtoMmortRat2)
                    mFactorM = matchMortWLifeExp(BasMordstM, RegionName, LIFEXP_Male(R), 0, 14, bCancelMsgMatchMortLE, cntx, INFMORdata, CHILDDRTdata)  'jrs 2019/05/06 maxfactor from 6 to 14 for Iran historical
                    'mfactor for Females
                    '-------------------
                    '        mfactorF = 0.5 / cumMort  '.5 represents 50% chance of dying younger,
                    '            ' and 50% change of dying older; hence life expectancy at birth
                    'jrs 2019/03/21
                    INFMORdata = INFMORdata * FtoMmortRat
                    CHILDDRTdata = CHILDDRTdata * FtoMmortRat2
                    mFactorF = matchMortWLifeExp(BasMordstF, RegionName, LIFEXP_Female(R), 0, 16, bCancelMsgMatchMortLE, cntx, INFMORdata, CHILDDRTdata) 'jrs 2019/05/06 maxfactor from 6 to 16 for Maldives historical
                    Dim ValM = New Dictionary(Of String, Single?)
                    Dim ValF = New Dictionary(Of String, Single?)
                    For j = 0 To cntx.NCohorts
                        If BasMordstM.ContainsKey(RegionName) Then
                            ValM.Add("Cohort" & j.ToString, If(BasMordstM(RegionName)("Cohort" & j.ToString), 0) * 1000 * mFactorM)
                        Else
                            ValM.Add("Cohort" & j.ToString, 0)
                        End If
                        If BasMordstF.ContainsKey(RegionName) Then
                            ValF.Add("Cohort" & j.ToString, If(BasMordstF(RegionName)("Cohort" & j.ToString), 0) * 1000 * mFactorF)
                        Else
                            ValF.Add("Cohort" & j.ToString, 0)
                        End If

                    Next j
                    modmordstM.Add(RegionName, ValM)
                    modmordstF.Add(RegionName, ValF)
                Next R

                'jrs 2019/03/21
                'Save Base Mortality after adjustments for INFMOR and CHILDDTHR
                Using db2 As IFs2Context = New IFs2Context()
                    If cntx.HistoricMode Then
                        SaveMortCohortDist(BasMordstM, db2.PopMortalityCohortRegionMaleHist, cntx)
                        SaveMortCohortDist(BasMordstF, db2.PopMortalityCohortRegionFemaleHist, cntx)
                    Else
                        SaveMortCohortDist(BasMordstM, db2.PopMortalityCohortRegionMale, cntx)
                        SaveMortCohortDist(BasMordstF, db2.PopMortalityCohortRegionFemale, cntx)
                    End If
                    db2.Flush()
                End Using
            End Sub

            Public Sub LoadInitData(ByRef modmordstdet As Single?(,,), ByVal cntx As IRebuildFacade)
                Dim iGender As Integer '1 = Male, 2 = Female
                Dim iType As Integer 'from 1 to 30, odds for males, evens for females
                Dim R As Integer
                Dim RN As String
                Using db2 As IFs2Context = New IFs2Context()
                    For Each valrec As HealthDetailedDeathsRegs In db2.HealthDetailedDeathsRegs.Where(Function(x) x.CauseID <> 11)
                        iGender = valrec.sex
                        R = valrec.RegNum
                        RN = valrec.RegName
                        iType = 2 * (valrec.CauseID - 1) + iGender
                        'Load infants and children 1-4
                        modmordstdet(0, R, iType) = valrec.R0 'jrs 2011/09/19 now it's deaths per 1000 * 1000
                        modmordstdet(1, R, iType) = valrec.R1 'jrs 2011/09/19 now it's deaths per 1000 * 1000
                        'For ages 5 to 84
                        For j = 2 To 17
                            Select Case j
                                Case 2
                                    modmordstdet(j, R, iType) = valrec.R3
                                Case 3
                                    modmordstdet(j, R, iType) = valrec.R4
                                Case 4
                                    modmordstdet(j, R, iType) = valrec.R5
                                Case 5
                                    modmordstdet(j, R, iType) = valrec.R6
                                Case 6
                                    modmordstdet(j, R, iType) = valrec.R7
                                Case 7
                                    modmordstdet(j, R, iType) = valrec.R8
                                Case 8
                                    modmordstdet(j, R, iType) = valrec.R9
                                Case 9
                                    modmordstdet(j, R, iType) = valrec.R10
                                Case 10
                                    modmordstdet(j, R, iType) = valrec.R11
                                Case 11
                                    modmordstdet(j, R, iType) = valrec.R12
                                Case 12
                                    modmordstdet(j, R, iType) = valrec.R13
                                Case 13
                                    modmordstdet(j, R, iType) = valrec.R14
                                Case 14
                                    modmordstdet(j, R, iType) = valrec.R15
                                Case 15
                                    modmordstdet(j, R, iType) = valrec.R16
                                Case 16
                                    modmordstdet(j, R, iType) = valrec.R17
                                Case 17
                                    modmordstdet(j, R, iType) = valrec.R18
                            End Select
                            'old
                            'modmordstdet(j, R, iType) = Convert.ToSingle(db2.Entry(valrec).CurrentValues("R" & (j + 1).ToString)) 'jrs 2011/09/19 now it's deaths per 1000 * 1000)
                        Next j
                        'Load 85+
                        For j = 18 To cntx.NCohorts
                            modmordstdet(j, R, iType) = valrec.R19 'jrs 2011/09/19 now it's deaths per 1000 * 1000
                        Next j
                    Next valrec
                    'Add up MH to ONCDs and set MH to 0
                    For R = 1 To NR
                        For j = 0 To cntx.NCohorts
                            For iGender = 1 To 2
                                modmordstdet(j, R, 10 + iGender) = modmordstdet(j, R, 10 + iGender) + modmordstdet(j, R, 28 + iGender)
                            Next iGender
                        Next j
                    Next R
                End Using
            End Sub
            Public Sub LoadDiarrheaData(ByRef modmordstdet As Single?(,,), ByVal cntx As IRebuildFacade)
                'Loading age specific data
                '=========================
                Using db As IFsContext = New IFsContext()
                    Dim rsAgeF = (From h In db.HealthGBDDeaths
                                  Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                  Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                  Where h.Code = "U010" AndAlso h.GroupName <> "World" AndAlso h.Gender = 2
                                  Order By G.RegionId
                                  Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Dim rsAgeM = (From h In db.HealthGBDDeaths
                                  Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                  Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                  Where h.Code = "U010" AndAlso h.GroupName <> "World" AndAlso h.Gender = 1
                                  Order By G.RegionId
                                  Order By G.RegionId
                                  Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Dim rsAgeAIDSF = (From h In db.HealthGBDDeaths
                                      Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                      Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                      Where h.Code = "U009" AndAlso h.GroupName <> "World" AndAlso h.Gender = 2
                                      Order By G.RegionId
                                      Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Dim rsAgeAIDSM = (From h In db.HealthGBDDeaths
                                      Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                      Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                      Where h.Code = "U009" AndAlso h.GroupName <> "World" AndAlso h.Gender = 1
                                      Order By G.RegionId
                                      Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Dim rsAgeCDF = (From h In db.HealthGBDDeaths
                                    Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                    Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                    Where h.Code = "U001" AndAlso h.GroupName <> "World" AndAlso h.Gender = 2
                                    Order By G.RegionId
                                    Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Dim rsAgeCDM = (From h In db.HealthGBDDeaths
                                    Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                    Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                    Where h.Code = "U001" AndAlso h.GroupName <> "World" AndAlso h.Gender = 1
                                    Order By G.RegionId
                                    Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Using db2 As IFs2Context = New IFs2Context()
                        db2.Clear(GetType(HealthDiarrheaReg))
                        'Save Mortality data
                        '-------------------
                        'Mortality = Mortality_CD * Deaths_Diarr/Deaths_CD
                        'Females 0 to 4
                        '==============
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 0 To 1
                                'Add new row
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 2
                                'rec.Mortality = modmordstdet(j, R, 2) * rsAgeF.Fields("0-4") / (rsAgeCDF.Fields("0-4") - rsAgeAIDSF.Fields("0-4"))
                                Dim Val1 = rsAgeF(R0).F0_4
                                Dim Val2 = rsAgeCDF(R0).F0_4
                                Dim Val3 = rsAgeAIDSF(R0).F0_4
                                rec.Mortality = modmordstdet(j, R, 2) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        'Females 5 to 14
                        '===============
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 2 To 3
                                'Add new row
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 2

                                ' Dim Val1 = RsAgeF(R0).F5_14
                                Dim Val1 = rsAgeF(R0).F5_14
                                Dim Val2 = rsAgeCDF(R0).F5_14
                                Dim Val3 = rsAgeAIDSF(R0).F5_14
                                rec.Mortality = modmordstdet(j, R, 2) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        'Females 15 to 29
                        '===================
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 4 To 6
                                'Add new row
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 2
                                Dim Val1 = rsAgeF(R0).F15_29
                                Dim Val2 = rsAgeCDF(R0).F15_29
                                Dim Val3 = rsAgeAIDSF(R0).F15_29
                                rec.Mortality = modmordstdet(j, R, 2) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        'Females 30 to 44
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 7 To 9
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 2
                                Dim Val1 = rsAgeF(R0).F30_44
                                Dim Val2 = rsAgeCDF(R0).F30_44
                                Dim Val3 = rsAgeAIDSF(R0).F30_44
                                rec.Mortality = modmordstdet(j, R, 2) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        'Females 45 to 59
                        '=================
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 10 To 12
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 2
                                Dim Val1 = rsAgeF(R0).F45_59
                                Dim Val2 = rsAgeCDF(R0).F45_59
                                Dim Val3 = rsAgeAIDSF(R0).F45_59
                                rec.Mortality = modmordstdet(j, R, 2) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        'Females 60 to 69
                        '================
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 13 To 14
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 2
                                Dim Val1 = rsAgeF(R0).F60_69
                                Dim Val2 = rsAgeCDF(R0).F60_69
                                Dim Val3 = rsAgeAIDSF(R0).F60_69
                                rec.Mortality = modmordstdet(j, R, 2) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        'Females 70 to 79
                        '================
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 15 To 16
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 2
                                Dim Val1 = rsAgeF(R0).F70_79
                                Dim Val2 = rsAgeCDF(R0).F70_79
                                Dim Val3 = rsAgeAIDSF(R0).F70_79
                                rec.Mortality = modmordstdet(j, R, 2) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        'Females 80+
                        '================
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 17 To 21
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 2
                                Dim Val1 = rsAgeF(R0).F80P
                                Dim Val2 = rsAgeCDF(R0).F80P
                                Dim Val3 = rsAgeAIDSF(R0).F80P
                                rec.Mortality = modmordstdet(j, R, 2) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        '************************************************
                        'Males 0 to 4
                        '==============
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 0 To 1
                                'Add new row
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 1
                                'rec.Mortality = modmordstdet(j, R, 1) * rsAgeM.Fields("0-4") / (rsAgeCDM.Fields("0-4") - rsAgeAIDSM.Fields("0-4"))
                                Dim Val1 = rsAgeM(R0).F0_4
                                Dim Val2 = rsAgeCDM(R0).F0_4
                                Dim Val3 = rsAgeAIDSM(R0).F0_4
                                rec.Mortality = modmordstdet(j, R, 1) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        'Males 5 to 14
                        '===============
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 2 To 3
                                'Add new row
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 1
                                Dim Val1 = rsAgeM(R0).F5_14
                                Dim Val2 = rsAgeCDM(R0).F5_14
                                Dim Val3 = rsAgeAIDSM(R0).F5_14
                                rec.Mortality = modmordstdet(j, R, 1) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        'Males 15 to 29
                        '===================
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 4 To 6
                                'Add new row
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 1
                                Dim Val1 = rsAgeM(R0).F15_29
                                Dim Val2 = rsAgeCDM(R0).F15_29
                                Dim Val3 = rsAgeAIDSM(R0).F15_29
                                rec.Mortality = modmordstdet(j, R, 1) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        'Males 30 to 44
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 7 To 9
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 1
                                Dim Val1 = rsAgeM(R0).F30_44
                                Dim Val2 = rsAgeCDM(R0).F30_44
                                Dim Val3 = rsAgeAIDSM(R0).F30_44
                                rec.Mortality = modmordstdet(j, R, 1) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        'Males 45 to 59
                        '=================
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 10 To 12
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 1
                                Dim Val1 = rsAgeM(R0).F45_59
                                Dim Val2 = rsAgeCDM(R0).F45_59
                                Dim Val3 = rsAgeAIDSM(R0).F45_59
                                rec.Mortality = modmordstdet(j, R, 1) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        'Males 60 to 69
                        '================
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 13 To 14
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 1
                                Dim Val1 = rsAgeM(R0).F60_69
                                Dim Val2 = rsAgeCDM(R0).F60_69
                                Dim Val3 = rsAgeAIDSM(R0).F60_69
                                rec.Mortality = modmordstdet(j, R, 1) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        'Males 70 to 79
                        '================
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 15 To 16
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 1
                                Dim Val1 = rsAgeM(R0).F70_79
                                Dim Val2 = rsAgeCDM(R0).F70_79
                                Dim Val3 = rsAgeAIDSM(R0).F70_79
                                rec.Mortality = modmordstdet(j, R, 1) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        'db2.Flush()
                        'Males 80+
                        '================
                        For R = 1 To NR
                            Dim R0 = R
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For j = 17 To 21
                                Dim rec As HealthDiarrheaReg = New HealthDiarrheaReg
                                rec.RegionId = R
                                rec.Cohort = j
                                rec.Gender = 1
                                Dim Val1 = rsAgeM(R0).F80P
                                Dim Val2 = rsAgeCDM(R0).F80P
                                Dim Val3 = rsAgeAIDSM(R0).F80P
                                rec.Mortality = modmordstdet(j, R, 1) * Val1 / (Val2 - Val3)
                                db2.HealthDiarrheaReg.Add(rec)
                            Next j
                        Next R
                        db2.Flush()
                    End Using
                End Using
            End Sub
            Public Sub LoadRespInfData(ByRef modmordstdet As Single?(,,), ByVal cntx As IRebuildFacade)
                'Loading age specific data
                '=========================
                Using db As IFsContext = New IFsContext()
                    Dim rsAgeF = (From h In db.HealthGBDDeaths
                                  Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                  Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                  Where h.Code = "U038" AndAlso h.GroupName <> "World" AndAlso h.Gender = 2
                                  Order By G.RegionId
                                  Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Dim rsAgeM = (From h In db.HealthGBDDeaths
                                  Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                  Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                  Where h.Code = "U038" AndAlso h.GroupName <> "World" AndAlso h.Gender = 1
                                  Order By G.RegionId
                                  Order By G.RegionId
                                  Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Dim rsAgeAIDSF = (From h In db.HealthGBDDeaths
                                      Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                      Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                      Where h.Code = "U009" AndAlso h.GroupName <> "World" AndAlso h.Gender = 2
                                      Order By G.RegionId
                                      Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Dim rsAgeAIDSM = (From h In db.HealthGBDDeaths
                                      Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                      Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                      Where h.Code = "U009" AndAlso h.GroupName <> "World" AndAlso h.Gender = 1
                                      Order By G.RegionId
                                      Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Dim rsAgeCDF = (From h In db.HealthGBDDeaths
                                    Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                    Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                    Where h.Code = "U001" AndAlso h.GroupName <> "World" AndAlso h.Gender = 2
                                    Order By G.RegionId
                                    Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Dim rsAgeCDM = (From h In db.HealthGBDDeaths
                                    Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                    Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                    Where h.Code = "U001" AndAlso h.GroupName <> "World" AndAlso h.Gender = 1
                                    Order By G.RegionId
                                    Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Using db2 As IFs2Context = New IFs2Context()
                        db2.Clear(GetType(HealthRespInfReg))
                        'Save Mortality data
                        '-------------------
                        'Mortality = Deaths / (Population * 1000) -> by age and gender
                        'Mortality = Mortality_CD * Deaths_RI/Deaths_CD
                        For R = 1 To NR
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            Dim R0 As Integer = R
                            For j = 0 To 21
                                Dim Val1(2) As Single?
                                Dim Val2(2) As Single?
                                Dim Val3(2) As Single?
                                If j >= 0 AndAlso j <= 1 Then
                                    ' 0 to 4
                                    Val1(1) = rsAgeCDM(R0).F0_4
                                    Val1(2) = rsAgeCDF(R0).F0_4
                                    Val2(1) = rsAgeAIDSM(R0).F0_4
                                    Val2(2) = rsAgeAIDSF(R0).F0_4
                                    Val3(1) = rsAgeM(R0).F0_4
                                    Val3(2) = rsAgeF(R0).F0_4
                                ElseIf j >= 2 AndAlso j <= 3 Then
                                    ' 5 to 14
                                    Val1(1) = rsAgeCDM(R0).F5_14
                                    Val1(2) = rsAgeCDF(R0).F5_14
                                    Val2(1) = rsAgeAIDSM(R0).F5_14
                                    Val2(2) = rsAgeAIDSF(R0).F5_14
                                    Val3(1) = rsAgeM(R0).F5_14
                                    Val3(2) = rsAgeF(R0).F5_14
                                ElseIf j >= 4 AndAlso j <= 6 Then
                                    ' 15 to 29
                                    Val1(1) = rsAgeCDM(R0).F15_29
                                    Val1(2) = rsAgeCDF(R0).F15_29
                                    Val2(1) = rsAgeAIDSM(R0).F15_29
                                    Val2(2) = rsAgeAIDSF(R0).F15_29
                                    Val3(1) = rsAgeM(R0).F15_29
                                    Val3(2) = rsAgeF(R0).F15_29
                                ElseIf j >= 7 AndAlso j <= 9 Then
                                    ' 30 to 44
                                    Val1(1) = rsAgeCDM(R0).F30_44
                                    Val1(2) = rsAgeCDF(R0).F30_44
                                    Val2(1) = rsAgeAIDSM(R0).F30_44
                                    Val2(2) = rsAgeAIDSF(R0).F30_44
                                    Val3(1) = rsAgeM(R0).F30_44
                                    Val3(2) = rsAgeF(R0).F30_44
                                ElseIf j >= 10 AndAlso j <= 12 Then
                                    ' 45 to 59
                                    Val1(1) = rsAgeCDM(R0).F45_59
                                    Val1(2) = rsAgeCDF(R0).F45_59
                                    Val2(1) = rsAgeAIDSM(R0).F45_59
                                    Val2(2) = rsAgeAIDSF(R0).F45_59
                                    Val3(1) = rsAgeM(R0).F45_59
                                    Val3(2) = rsAgeF(R0).F45_59
                                ElseIf j >= 13 AndAlso j <= 14 Then
                                    ' 60 to 69
                                    Val1(1) = rsAgeCDM(R0).F60_69
                                    Val1(2) = rsAgeCDF(R0).F60_69
                                    Val2(1) = rsAgeAIDSM(R0).F60_69
                                    Val2(2) = rsAgeAIDSF(R0).F60_69
                                    Val3(1) = rsAgeM(R0).F60_69
                                    Val3(2) = rsAgeF(R0).F60_69
                                ElseIf j >= 15 AndAlso j <= 16 Then
                                    ' 70 to 79
                                    Val1(1) = rsAgeCDM(R0).F70_79
                                    Val1(2) = rsAgeCDF(R0).F70_79
                                    Val2(1) = rsAgeAIDSM(R0).F70_79
                                    Val2(2) = rsAgeAIDSF(R0).F70_79
                                    Val3(1) = rsAgeM(R0).F70_79
                                    Val3(2) = rsAgeF(R0).F70_79
                                ElseIf j >= 17 AndAlso j <= 21 Then
                                    ' 80+
                                    Val1(1) = rsAgeCDM(R0).F80P
                                    Val1(2) = rsAgeCDF(R0).F80P
                                    Val2(1) = rsAgeAIDSM(R0).F80P
                                    Val2(2) = rsAgeAIDSF(R0).F80P
                                    Val3(1) = rsAgeM(R0).F80P
                                    Val3(2) = rsAgeF(R0).F80P
                                End If
                                If New Integer() {1, 3, 6, 9, 12, 14, 16, 21}.Contains(j) Then
                                    'End of each group - Add new row
                                    For g = 1 To 2 'gender
                                        Dim rec As HealthRespInfReg = New HealthRespInfReg
                                        rec.RegionId = R
                                        rec.Cohort = j
                                        rec.Gender = g
                                        rec.Mortality = modmordstdet(j, R, 2) * Val3(g) / (Val1(g) - Val2(g))
                                        If rec.Mortality.HasValue AndAlso Double.IsNaN(rec.Mortality) Then
                                            rec.Mortality = Nothing
                                        End If
                                        db2.HealthRespInfReg.Add(rec)
                                    Next
                                End If
                            Next j
                        Next R
                        db2.Flush()
                    End Using
                End Using
            End Sub

            Public Sub LoadMentalHealthData(ByVal WHOIndex As Dictionary(Of Integer, String), ByVal agedstM As Dictionary(Of String, Dictionary(Of String, Single?)),
                                            ByVal agedstF As Dictionary(Of String, Dictionary(Of String, Single?)),
                                            ByVal agedstWHOM As Single?(,), ByVal agedstWHOF As Single?(,), ByVal cntx As IRebuildFacade)

                Using db As IFsContext = New IFsContext()
                    Dim rsAgeF = (From h In db.HealthGBDYLD
                                  Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                  Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                  Where h.Code = "U081" AndAlso h.GroupName <> "World" AndAlso h.Gender = 2
                                  Order By G.RegionId
                                  Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Dim rsAgeM = (From h In db.HealthGBDYLD
                                  Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                  Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                  Where h.Code = "U081" AndAlso h.GroupName <> "World" AndAlso h.Gender = 1
                                  Order By G.RegionId
                                  Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )

                    Dim PopTot As Single?
                    Using db2 As IFs2Context = New IFs2Context()
                        db2.Clear(GetType(HealthMentalHealthReg))
                        'Save Morbidity data
                        '-------------------
                        'Morbidity = YLD (in units) / (Population (in millions) * 1,000,000 )

                        'All ages
                        '================
                        For R = 1 To NR
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            For g = 1 To 2 'gender
                                Dim R0 As Integer = R
                                Dim rec As HealthMentalHealthReg = New HealthMentalHealthReg
                                rec.RegionId = R
                                rec.Cohort = -1
                                PopTot = 0
                                rec.Gender = g
                                If g = 1 Then 'Male
                                    For j = 1 To cntx.NCohorts
                                        Dim Val1 = rsAgeM(R0).GroupName
                                        Dim idx = (WHOIndex.FirstOrDefault(Function(w) w.Value = Val1)).Key
                                        'Dim idx = (WHOIndex.FirstOrDefault(Function(w) w.Value = rsAgeM(R0).Value.GroupName)).Key
                                        PopTot = PopTot + agedstWHOM(idx, j)
                                    Next j
                                    If PopTot <> 0 Then
                                        rec.YLDtoPOP = (rsAgeM(R0).Total / 1000000) / (PopTot)
                                    End If
                                Else
                                    For j = 1 To cntx.NCohorts
                                        Dim Val1 = rsAgeF(R0).GroupName
                                        Dim idx = (WHOIndex.FirstOrDefault(Function(w) w.Value = Val1)).Key
                                        PopTot = PopTot + agedstWHOF(idx, j)
                                    Next j
                                    If PopTot <> 0 Then
                                        rec.YLDtoPOP = (rsAgeF(R0).Total / 1000000) / (PopTot)
                                    End If
                                End If
                                If rec.YLDtoPOP.HasValue AndAlso Double.IsNaN(rec.YLDtoPOP) Then
                                    rec.YLDtoPOP = Nothing
                                End If
                                db2.HealthMentalHealthReg.Add(rec)
                            Next
                        Next
                        For R = 1 To NR
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            Dim R0 As Integer = R
                            'Dim Val1 = rsAgeF(R0).Total
                            'Dim Val2 = rsAgeM(R0).Total
                            Dim idx(2) As Integer
                            Dim Val4(2) As Single?
                            For j = 0 To 21
                                Dim Val3(2) As Single?
                                If j >= 0 AndAlso j <= 1 Then
                                    ' 0 to 4

                                    Val3(1) = rsAgeM(R0).F0_4
                                    Val3(2) = rsAgeF(R0).F0_4
                                ElseIf j >= 2 AndAlso j <= 3 Then
                                    ' 5 to 14
                                    Val3(1) = rsAgeM(R0).F5_14
                                    Val3(2) = rsAgeF(R0).F5_14
                                ElseIf j >= 4 AndAlso j <= 6 Then
                                    ' 15 to 29
                                    Val3(1) = rsAgeM(R0).F15_29
                                    Val3(2) = rsAgeF(R0).F15_29
                                ElseIf j >= 7 AndAlso j <= 9 Then
                                    ' 30 to 44
                                    Val3(1) = rsAgeM(R0).F30_44
                                    Val3(2) = rsAgeF(R0).F30_44
                                ElseIf j >= 10 AndAlso j <= 12 Then
                                    ' 45 to 59
                                    Val3(1) = rsAgeM(R0).F45_59
                                    Val3(2) = rsAgeF(R0).F45_59
                                ElseIf j >= 13 AndAlso j <= 14 Then
                                    ' 60 to 69
                                    Val3(1) = rsAgeM(R0).F60_69
                                    Val3(2) = rsAgeF(R0).F60_69
                                ElseIf j >= 15 AndAlso j <= 16 Then
                                    ' 70 to 79
                                    Val3(1) = rsAgeM(R0).F70_79
                                    Val3(2) = rsAgeF(R0).F70_79
                                ElseIf j >= 17 AndAlso j <= 21 Then
                                    ' 80+
                                    Val3(1) = rsAgeM(R0).F80P
                                    Val3(2) = rsAgeF(R0).F80P
                                End If
                                idx(1) = (WHOIndex.FirstOrDefault(Function(w) w.Value = rsAgeM(R0).GroupName)).Key
                                idx(2) = (WHOIndex.FirstOrDefault(Function(w) w.Value = rsAgeF(R0).GroupName)).Key
                                Val4(1) += agedstWHOM(idx(1), j)
                                Val4(2) += agedstWHOF(idx(2), j)
                                If New Integer() {1, 3, 6, 9, 12, 14, 16, 21}.Contains(j) Then
                                    'End of each group - Add new row
                                    For g = 1 To 2 'gender
                                        Dim rec As HealthMentalHealthReg = New HealthMentalHealthReg
                                        rec.RegionId = R
                                        rec.Cohort = j
                                        rec.Gender = g
                                        rec.YLDtoPOP = (Val3(g) / 1000000) / Val4(g)
                                        If rec.YLDtoPOP.HasValue AndAlso Double.IsNaN(rec.YLDtoPOP) Then
                                            rec.YLDtoPOP = Nothing
                                        End If
                                        db2.HealthMentalHealthReg.Add(rec)
                                        Val4(g) = 0
                                    Next
                                End If
                            Next j
                        Next R
                        db2.Flush()
                    End Using
                End Using

            End Sub

            Public Sub LoadMalariaData(ByVal agedstM As Dictionary(Of String, Dictionary(Of String, Single?)), ByVal agedstF As Dictionary(Of String, Dictionary(Of String, Single?)), ByVal cntx As IRebuildFacade)
                'write to HealthMalariaReg
                Dim PalestineIdx = AMAX(cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("Palestine")).Key, 0)
                Dim PrtoRicoIdx = AMAX(cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("Puerto Rico")).Key, 0)
                Dim HongKongIdx = AMAX(cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("Hong Kong")).Key, 0)
                Dim TaiwanIdx = AMAX(cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("Taiwan")).Key, 0)
                Dim JordanIdx = AMAX(cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("Jordan")).Key, 0)
                'jrs 2026/03/04
                Dim USAidx = 0
                If gRegionIdx("United States of America") > 0 Then
                    USAidx = AMAX(cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("United States of America")).Key, 0)
                End If
                'jrs 2014/11/06
                If USAidx <= 0 Then
                    USAidx = AMAX(cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("US-California")).Key, 0)
                End If
                'jrs 2009/06/23 ChinaIdx = gRegionIdx("China")

                'Get Region Index by Region Name
                'jrs 2026/03/04 change the casing Of to of
                Dim SKoreaIdx = AMAX(cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("Korea, Republic of")).Key, 0)
                Dim TimorIdx = AMAX(cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("Timor-Leste")).Key, 0)
                Dim IndonesIdx = AMAX(cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("Indonesia")).Key, 0)
                Dim MonteNegIdx = AMAX(cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("Montenegro")).Key, 0)
                Dim SerbiaIdx = AMAX(cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("Serbia")).Key, 0)
                'jrs 2026/03/04 change the casing And to and
                Dim StVinctIdx = AMAX(cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("St. Vincent and the Grenadines")).Key, 0)
                Dim BarbadosIdx = AMAX(cntx.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("Barbados")).Key, 0)
                Dim CMalar As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim auxTbl = cntx.GetSlice("SeriesHealthMalarDthsper100000", cntx.BaseYear, CoalescePolicy.MostRecent)
                For Each key In cntx.Regionalization.Keys
                    CMalar.Add(key, auxTbl(key))
                    If CMalar(key).HasValue Then
                        'convert to deaths per 1000
                        CMalar(key) = CMalar(key) / 100
                    End If
                Next

                'Convert to Regions
                '-------------------
                Dim RMalar = RegSum(CMalar, cntx)
                'Filling up holes with proxy
                RMalar(PalestineIdx) = RMalar(JordanIdx)
                RMalar(PrtoRicoIdx) = RMalar(USAidx)
                RMalar(HongKongIdx) = RMalar(SKoreaIdx) 'jrs 2009/06/23 ChinaIdx)
                RMalar(TaiwanIdx) = RMalar(SKoreaIdx) 'jrs 2009/06/23 ChinaIdx)
                RMalar(TimorIdx) = RMalar(IndonesIdx)
                RMalar(MonteNegIdx) = RMalar(SerbiaIdx)
                RMalar(StVinctIdx) = RMalar(BarbadosIdx)
                Using db As IFsContext = New IFsContext()
                    Dim rsAgeF = (From h In db.HealthGBDDeaths
                                  Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                  Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                  Where h.Code = "U020" AndAlso h.GroupName <> "World" AndAlso h.Gender = 2
                                  Order By G.RegionId
                                  Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Dim rsAgeM = (From h In db.HealthGBDDeaths
                                  Join GI In db.GroupIDNewFull On h.GroupName Equals GI.GroupName
                                  Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                  Where h.Code = "U020" AndAlso h.GroupName <> "World" AndAlso h.Gender = 1
                                  Order By G.RegionId
                                  Select Key = G.RegionId, Value = h).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Using db2 As IFs2Context = New IFs2Context()
                        db2.Clear(GetType(HealthMalariaReg))
                        For R = 1 To NR
                            Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                            Dim R0 As Integer = R
                            Dim Val1 = rsAgeF(R0).Total
                            Dim Val2 = rsAgeM(R0).Total
                            Dim Val4(2) As Single?
                            For j = 0 To 21
                                Dim Val3(2) As Single?
                                If j >= 0 AndAlso j <= 1 Then
                                    ' 0 to 4
                                    Val3(1) = rsAgeM(R0).F0_4
                                    Val3(2) = rsAgeF(R0).F0_4
                                ElseIf j >= 2 AndAlso j <= 3 Then
                                    ' 5 to 14
                                    Val3(1) = rsAgeM(R0).F5_14
                                    Val3(2) = rsAgeF(R0).F5_14
                                ElseIf j >= 4 AndAlso j <= 6 Then
                                    ' 15 to 29
                                    Val3(1) = rsAgeM(R0).F15_29
                                    Val3(2) = rsAgeF(R0).F15_29
                                ElseIf j >= 7 AndAlso j <= 9 Then
                                    ' 30 to 44
                                    Val3(1) = rsAgeM(R0).F30_44
                                    Val3(2) = rsAgeF(R0).F30_44
                                ElseIf j >= 10 AndAlso j <= 12 Then
                                    ' 45 to 59
                                    Val3(1) = rsAgeM(R0).F45_59
                                    Val3(2) = rsAgeF(R0).F45_59
                                ElseIf j >= 13 AndAlso j <= 14 Then
                                    ' 60 to 69
                                    Val3(1) = rsAgeM(R0).F60_69
                                    Val3(2) = rsAgeF(R0).F60_69
                                ElseIf j >= 15 AndAlso j <= 16 Then
                                    ' 70 to 79
                                    Val3(1) = rsAgeM(R0).F70_79
                                    Val3(2) = rsAgeF(R0).F70_79
                                ElseIf j >= 17 AndAlso j <= 21 Then
                                    ' 80+
                                    Val3(1) = rsAgeM(R0).F80P
                                    Val3(2) = rsAgeF(R0).F80P
                                End If
                                Val4(1) += If(Not agedstM.ContainsKey(RegionName), 0, agedstM(RegionName)("Cohort" & j.ToString))
                                Val4(2) += If(Not agedstF.ContainsKey(RegionName), 0, agedstF(RegionName)("Cohort" & j.ToString))
                                If New Integer() {1, 3, 6, 9, 12, 14, 16, 21}.Contains(j) Then
                                    'End of each group - Add new row
                                    For g = 1 To 2 'gender
                                        Dim rec As HealthMalariaReg = New HealthMalariaReg
                                        rec.RegionId = R
                                        rec.Cohort = j
                                        rec.Gender = g
                                        If Val1 + Val2 > 0 Then
                                            rec.Mortality = RMalar(R) * If(Not CPOP.ContainsKey(RegionName), 0, CPOP(RegionName)) * Val3(g) /
                                                 ((Val1 + Val2) * Val4(g))
                                        Else
                                            rec.Mortality = 0
                                        End If
                                        If rec.Mortality.HasValue AndAlso Double.IsNaN(rec.Mortality) Then
                                            rec.Mortality = Nothing
                                        End If
                                        db2.HealthMalariaReg.Add(rec)
                                        Val4(g) = 0
                                    Next
                                End If
                            Next j
                        Next R
                        db2.Flush()
                    End Using
                End Using

            End Sub
            Private Function RegSum(ByVal CHold As Dictionary(Of String, Single?), cntx As IRebuildFacade) As Single()
                '-----------------------------------------------------------------

                '   Initialize Holder of Regional Sum and Valid Region Members
                '   ----------------------------------------------------------

                Dim RegHold(NR) As Single
                Dim reg As Integer
                For reg% = 1 To NR
                    RegHold(reg%) = 0
                Next reg%
                '   Weighted Sum Across Countries and Sum Weights
                '   ---------------------------------------------
                Dim Warned = False
                'cntx.Regionalization
                For Each r In cntx.Regionalization
                    Dim ValHold = CHold(r.Key)
                    If ValHold.HasValue Then
                        Dim rid = r.Value 'VB6, CounReg = RegionID
                        RegHold(rid) = RegHold(rid) + ValHold
                    End If
                Next
                Return RegHold
            End Function
            'Build Population values for WHO groups
            Private Sub BuildPopGroup(agedstM As Dictionary(Of String, Dictionary(Of String, Single?)), agedstF As Dictionary(Of String, Dictionary(Of String, Single?)),
                                     ByRef agedstWHOM As Single?(,), ByRef agedstWHOF As Single?(,), ByRef WHOIndex As Dictionary(Of Integer, String), cntx As IRebuildFacade)
                Dim sGroups(14) As String
                '1. WHO Afro D
                sGroups(1) = "WHO Afro D"
                '2. WHO Afro E
                sGroups(2) = "WHO Afro E"
                '3. WHO Amro A
                sGroups(3) = "WHO Amro A"
                '4. WHO Amro B
                sGroups(4) = "WHO Amro B"
                '5. WHO Amro D
                sGroups(5) = "WHO Amro D"
                '6. WHO Emro B
                sGroups(6) = "WHO Emro B"
                '7. WHO Emro D
                sGroups(7) = "WHO Emro D"
                '8. WHO Euro A
                sGroups(8) = "WHO Euro A"
                '9. WHO Euro B
                sGroups(9) = "WHO Euro B"
                '10. WHO Euro C
                sGroups(10) = "WHO Euro C"
                '11. WHO Searo B
                sGroups(11) = "WHO Searo B"
                '12. WHO Searo D
                sGroups(12) = "WHO Searo D"
                '13. WHO Wpro A
                sGroups(13) = "WHO Wpro A"
                '14. WHO Wpro B
                sGroups(14) = "WHO Wpro B"
                Dim i As Integer
                For i = 1 To 14
                    Dim i0 As Integer = i
                    WHOIndex.Add(i, sGroups(i))
                    Using db As IFsContext = New IFsContext()
                        Dim rs = (From GI In db.GroupIDNewFull Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                  Where GI.GroupName = sGroups(i0)
                                  Order By G.RegionId
                                  Select Key = G.RegionId, Value = G.RegionName)
                        For Each r In rs
                            For j = 0 To cntx.NCohorts
                                agedstWHOM(i, j) = If(agedstWHOM(i, j), 0) + If(Not agedstM.ContainsKey(r.Value), 0, agedstM(r.Value)("Cohort" & j))
                                agedstWHOF(i, j) = If(agedstWHOF(i, j), 0) + If(Not agedstF.ContainsKey(r.Value), 0, agedstF(r.Value)("Cohort" & j))
                            Next j
                        Next
                    End Using
                Next i
            End Sub

            'jrs 2019/03/21 Adding INFMOR data and CHILDDTHR data to match too
            Public Function matchMortWLifeExp(ByRef MortDist As Dictionary(Of String, Dictionary(Of String, Single?)),
                ByVal RegionName As String, ByVal TargetLE As Single,
                ByVal mFactorMin As Single, ByVal mFactorMax As Single,
                ByRef bCancelMsg As Boolean, cntx As IRebuildFacade,
                Optional ByVal INFMORdata As Single = 0, Optional ByVal CHILDDTHRData As Single = 0)
                ', ByVal R As Integer, ByVal G As Integer, _
                'ByVal TargetLE As Single, ByVal mFactorMin As Single, ByVal mFactorMax As Single, ByRef bCancelMsg As Boolean) As Single

                '1. MortDist is the array (3 dimensioned), 1- cohorts, 2-regions, 3-gender

                '2. R is the Region index

                '3. g is the gender index, 1 for males, 2 for females

                '4. TargetLE is the Life expectancy value to match

                '5. Minimum Value for mFactor

                '6. Max Value for mFactor
                '7. bCancelMsg flag to cancel warning message
                '8. Context
                '9. INFMORData Infant Mortality Data
                '10. CHILDDTHRData Child Mortality Probability Data

                Dim infMort As Single, LE As Single, lx As Single
                Dim j As Integer, N As Integer, cohMort As Single, cohMort2 As Single
                Dim nax As Single, nqx As Single, nax0 As Single
                Dim mFactor1 As Single
                mFactor1 = (mFactorMax + mFactorMin) / 2

                'jrs 2019/03/21
                If INFMORdata > 0 And mFactor1 > 0 Then
                    MortDist(RegionName)("Cohort0") = INFMORdata / 1000 / mFactor1
                End If

                'infMort = MortDist(0, R, G) * mFactor1
                If MortDist.ContainsKey(RegionName) Then
                    infMort = If(MortDist(RegionName)("Cohort0"), 0) * mFactor1
                Else
                    infMort = 0
                End If

                LE = 0
                lx = 1
                For j = 0 To cntx.NCohorts
                    'Years in cohort
                    If j = 0 Then
                        N = 1
                    ElseIf j = 1 Then
                        N = 4
                    Else
                        N = 5
                    End If
                    If MortDist.ContainsKey(RegionName) Then
                        cohMort = If(MortDist(RegionName)("Cohort" & j.ToString), 0) * mFactor1
                    Else
                        cohMort = 0
                    End If
                    'Average years lived by those who die (per Keyfitz)
                    If j = 0 Then
                        If cohMort >= 0.107 Then
                            nax = 0.34
                        Else
                            nax = 0.049 + 2.742 * cohMort
                        End If
                        nax0 = nax
                    ElseIf j = 1 Then
                        If infMort >= 0.107 Then
                            nax = 1.356
                        Else
                            nax = 1.587 - 2.167 * infMort
                        End If
                        'jrs 2019/03/21
                        If CHILDDTHRData > 0 And mFactor1 > 0 Then
                            'CHILDDTHRData/1000 = 1 - lx1
                            '= 1 - lx0 * (1 - nqx1)
                            '= 1 - 1 * (1 - nqx0) * (1 - nqx1)
                            '= 1 - (1 - infMort/(1+(1 - nax0)*InfMort)) * (1 - 4 * MortDist/(1+(4-nax1)MortDist))

                            '(1 - infMort/(1+(1 - nax0)*InfMort)) * (1 - 4 * MortDist/(1+(4-nax1)MortDist)) = 1 - CHILDDTHRData/1000
                            '(1 - 4 * MortDist/(1+(4-nax1)MortDist)) = (1 - CHILDDTHRData/1000) / (1 - infMort/(1+(1 - nax0)*InfMort))
                            '4 * MortDist/(1+(4-nax1)MortDist) = 1 - (1 - CHILDDTHRData/1000) / (1 - infMort/(1+(1 - nax0)*InfMort))
                            '4 * MortDist = (1 - (1 - CHILDDTHRData/1000) / (1 - infMort/(1+(1 - nax0)*InfMort))) * (1+(4-nax1)MortDist)
                            '4 * MortDist - (1 - (1 - CHILDDTHRData/1000) / (1 - infMort/(1+(1 - nax0)*InfMort))) * (4-nax1) MortDist = (1 - (1 - CHILDDTHRData/1000) / (1 - infMort/(1+(1 - nax0)*InfMort)))
                            'MortDist = ((1 - (1 - CHILDDTHRData/1000) / (1 - infMort/(1+(1 - nax0)*InfMort)))) / (4 - (1 - (1 - CHILDDTHRData/1000) / (1 - infMort/(1+(1 - nax0)*InfMort))) * (4-nax1))
                            cohMort = ((1 - (1 - CHILDDTHRData / 1000) / (1 - infMort / (1 + (1 - nax0) * infMort))))
                            cohMort2 = (4 - (1 - (1 - CHILDDTHRData / 1000) / (1 - infMort / (1 + (1 - nax0) * infMort))) * (4 - nax))
                            cohMort = cohMort / cohMort2
                            If cohMort < 0 Then
                                cohMort = 0.02 * infMort
                            End If
                            MortDist(RegionName)("Cohort" & j.ToString) = cohMort / mFactor1
                        End If
                    Else
                        nax = 2.5
                    End If
                    'Probability of death using period data
                    If j = cntx.NCohorts Then
                        nqx = 1
                    Else
                        nqx = (N * cohMort) / (1 + ((N - nax) * cohMort))
                        If nqx > 1 Then
                            nqx = 1
                        End If
                    End If
                    'Adding N years for survivors, and nax for deaths

                    If j < cntx.NCohorts Then
                        LE = LE + (lx * (1 - nqx) * N) + (lx * nqx * nax)
                    Else
                        If cohMort > 0 Then
                            LE = LE + lx / cohMort
                        End If
                    End If
                    'Probability of surviving until age x
                    lx = lx * (1 - nqx)
                Next j
                Dim portion As Single
                portion = (TargetLE - LE) / LE
                If Math.Abs(portion) < 0.001 Then
                    Return mFactor1
                ElseIf mFactorMin >= mFactorMax Then
                    If Not bCancelMsg Then
                        Debug.WriteLine("Not possible To find matching factor between Mortality And Life Expectancy For " & RegionName & "- Matching Mort. And Lif. Exp.")
                        bCancelMsg = True
                    End If
                    If mFactorMin = mFactorMax Then
                        Return mFactorMin
                    Else
                        Return 1
                    End If
                Else 'Try another mFactor
                    'more lifeexpectancy, less mortality
                    If portion > 0 Then
                        Return matchMortWLifeExp(MortDist, RegionName, TargetLE, mFactorMin, mFactor1, bCancelMsg, cntx, INFMORdata, CHILDDTHRData)
                    Else 'less lifeexpectancy, more mortality
                        Return matchMortWLifeExp(MortDist, RegionName, TargetLE, mFactor1, mFactorMax, bCancelMsg, cntx, INFMORdata, CHILDDTHRData)
                    End If
                End If
            End Function

            Public Sub RegDetailedDeaths(cntx As IRebuildFacade) '(db As Database)
                Dim CtrIdx As Integer, RegIdx As Integer
                Dim RegHold(NR, 2, 15, 21) As Single? 'jrs 2019/06/05 RegHold was dimensioned to 19, now to 21 in 4th dimension
                Dim RegWeight(NR) As Single
                Dim i As Integer

                'db.Execute("DELETE * FROM HealthDetailedDeathsRegs") 'jrs 2013/08/31 HealthDetailedDeathsRegs2k8
                'Open Country Table
                'Set rs = db.OpenRecordset("Select * FROM HealthDetailedDeathsCtry ORDER BY Country") 'jrs 2013/08/31 HealthDetailedDeathsCtry2k8
                'Aggreagete for Region
                'rs.MoveFirst
                CtrIdx = 1
                Using db As IFs.Data.IFsContext = New IFsContext()
                    For Each HRec As HealthDetailedDeathsCtry In db.HealthDetailedDeathsCtry.OrderBy(Function(h) h.Country)
                        If HRec Is DBNull.Value Then Exit For
                        Dim ctry = HRec.Country
                        Dim fRegIdx As Integer? = (From s In cntx.Regionalization Where s.Key = ctry).FirstOrDefault().Value
                        Dim fRegHongKongIdx As Integer? = (From s In cntx.Regionalization Where s.Key = "Hong Kong").FirstOrDefault().Value
                        Dim fRegKosovoIdx As Integer? = (From s In cntx.Regionalization Where s.Key = "Kosovo").FirstOrDefault().Value
                        If Not fRegIdx.HasValue Then
                            Debug.WriteLine("There Is a problem With the Regionalization Of Detailed Deaths, please check the data.- Regionalize Detailed Deaths")
                            Exit Sub
                        Else
                            'RegIdx = CounReg(CtrIdx) 'VB6, CounReg = RegionID
                            RegIdx = fRegIdx
                        End If
                        If HRec.CauseID.IsNonZero Then
                            For i = 1 To 21 'jrs 2019/06/05 used to stop at 19
                                'Dim pval = Convert.ToSingle(db.Entry(HRec).CurrentValues("p" & i.ToString))
                                Dim pval As Single?
                                Select Case i
                                    Case 1
                                        pval = HRec.p1
                                    Case 2
                                        pval = HRec.p2
                                    Case 3
                                        pval = HRec.p3
                                    Case 4
                                        pval = HRec.p4
                                    Case 5
                                        pval = HRec.p5
                                    Case 6
                                        pval = HRec.p6
                                    Case 7
                                        pval = HRec.p7
                                    Case 8
                                        pval = HRec.p8
                                    Case 9
                                        pval = HRec.p9
                                    Case 10
                                        pval = HRec.p10
                                    Case 11
                                        pval = HRec.p11
                                    Case 12
                                        pval = HRec.p12
                                    Case 13
                                        pval = HRec.p13
                                    Case 14
                                        pval = HRec.p14
                                    Case 15
                                        pval = HRec.p15
                                    Case 16
                                        pval = HRec.p16
                                    Case 17
                                        pval = HRec.p17
                                    Case 18
                                        pval = HRec.p18
                                    Case 19
                                        pval = HRec.p19
                                    Case 20
                                        pval = HRec.p20
                                    Case 21
                                        pval = HRec.p21
                                End Select

                                If pval.HasValue Then
                                    If Not RegHold(RegIdx, HRec.sex, HRec.CauseID, i).HasValue Then
                                        RegHold(RegIdx, HRec.sex, HRec.CauseID, i) = 0
                                    End If
                                    RegHold(RegIdx, HRec.sex, HRec.CauseID, i) = RegHold(RegIdx, HRec.sex, HRec.CauseID, i) + pval * If(CPOP.ContainsKey(ctry), If(CPOP(ctry), 0), 0)
                                End If

                                'jrs 2019/06/05
                                If ctry = "Singapore" Then
                                    'If HRec.sex = 1 And HRec.CauseID = 1 And i = 1 Then Stop
                                    If pval.HasValue Then
                                        If Not RegHold(fRegHongKongIdx, HRec.sex, HRec.CauseID, i).HasValue Then
                                            RegHold(fRegHongKongIdx, HRec.sex, HRec.CauseID, i) = 0
                                        End If
                                        RegHold(fRegHongKongIdx, HRec.sex, HRec.CauseID, i) = RegHold(fRegHongKongIdx, HRec.sex, HRec.CauseID, i) + pval * If(CPOP.ContainsKey("Hong Kong"), If(CPOP("Hong Kong"), 0), 0)
                                    End If

                                ElseIf ctry = "Serbia" Then
                                    If pval.HasValue Then
                                        If Not RegHold(fRegKosovoIdx, HRec.sex, HRec.CauseID, i).HasValue Then
                                            RegHold(fRegKosovoIdx, HRec.sex, HRec.CauseID, i) = 0
                                        End If
                                        RegHold(fRegKosovoIdx, HRec.sex, HRec.CauseID, i) = RegHold(fRegKosovoIdx, HRec.sex, HRec.CauseID, i) + pval * If(CPOP.ContainsKey("Kosovo"), If(CPOP("Kosovo"), 0), 0)
                                    End If

                                End If
                            Next i
                        End If
                    Next HRec
                End Using
                'For each Country
                For Each r In cntx.Regionalization
                    RegIdx = r.Value
                    RegWeight(RegIdx) = RegWeight(RegIdx) + If(CPOP.ContainsKey(r.Key), If(CPOP(r.Key), 0), 0)
                Next
                'Compute Weigthed Average using population
                'Save in Region Table
                Dim G As Integer, T As Integer, A As Integer
                Using dbT As IFs.Data.IFs2Context = New IFs2Context()
                    dbT.Clear(GetType(HealthDetailedDeathsRegs))
                    For i = 1 To NR
                        For G = 1 To 2
                            For T = 1 To 15
                                If RegHold(i, G, T, 19).HasValue AndAlso RegWeight(i) <> 0 Then
                                    Dim rec As HealthDetailedDeathsRegs = New HealthDetailedDeathsRegs()
                                    rec.RegNum = i
                                    'rec.RegName = GRPS(1, i)
                                    Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(i))
                                    rec.RegName = RegionName
                                    rec.sex = G
                                    rec.CauseID = T
                                    rec.Cause = cntx.GetDimension(27).Buckets.Item(CShort(T)) 'jrs 2019/04/02 - GRPS(27, T)
                                    'jrs 2017/03/02 rec.R0") = Round(RegHold(i, G, T, 2) / RegWeight(i), 4)
                                    rec.R0 = Math.Round(CSng(RegHold(i, G, T, 1)) / RegWeight(i), 4)
                                    rec.R1 = Math.Round(CSng(RegHold(i, G, T, 2)) / RegWeight(i), 4)
                                    For A = 3 To 21 'jrs 2019/06/05 used to stop at 19
                                        'dbT.Entry(rec).CurrentValues("R" & A.ToString) = Math.Round(RegHold(i, G, T, A) / RegWeight(i), 4)
                                        Dim val = Math.Round(CSng(RegHold(i, G, T, A)) / RegWeight(i), 4)
                                        Select Case A
                                            Case 3
                                                rec.R3 = val
                                            Case 4
                                                rec.R4 = val
                                            Case 5
                                                rec.R5 = val
                                            Case 6
                                                rec.R6 = val
                                            Case 7
                                                rec.R7 = val
                                            Case 8
                                                rec.R8 = val
                                            Case 9
                                                rec.R9 = val
                                            Case 10
                                                rec.R10 = val
                                            Case 11
                                                rec.R11 = val
                                            Case 12
                                                rec.R12 = val
                                            Case 13
                                                rec.R13 = val
                                            Case 14
                                                rec.R14 = val
                                            Case 15
                                                rec.R15 = val
                                            Case 16
                                                rec.R16 = val
                                            Case 17
                                                rec.R17 = val
                                            Case 18
                                                rec.R18 = val
                                            Case 19
                                                rec.R19 = val
                                            Case 20
                                                rec.R20 = val
                                            Case 21
                                                rec.R21 = val
                                        End Select
                                    Next A
                                    dbT.HealthDetailedDeathsRegs.Add(rec)
                                End If
                            Next
                        Next
                    Next
                    dbT.Flush()
                End Using
            End Sub

            'jrs 2019/04/05 implemented by ttt, takes about 14 seconds, maybe ask LIU to check for performance
            Private Sub LoadRatioYLDtoYLL()
                Dim sql As String
                Dim T As Integer, G As Integer
                '1. Other communicable (excluding AIDS): U001 - U009 - U010 - U020 - U038

                '2. Malignant Neoplasms : U060
                '3. Cardiovascular Diseases: U104
                '4. Digestive Diseases: U115
                '5. Respiratory Diseases: U111
                '6. Other Non-communicable Diseases: U059 - U060 - U104 - U115 - U111 - U079 - U081

                '7. Road Traffic Accidents: U150
                '8. Other unintentional injuries: U149 - U150
                '9. Intentional Injuries: U156

                '10. Diabetes: U079
                '11. AIDS: U009

                '12. Diarrhea: U010
                '13. Malaria: U020
                '14. Resp. Infections: U038

                '15. Mental Health: U081, not computed because it's not based on mortallity since
                'we're not reporting mortality for Mental Health.

                Dim GBDCodes(14) As String
                GBDCodes(1) = "U001"
                GBDCodes(2) = "U060"
                GBDCodes(3) = "U104"
                GBDCodes(4) = "U115"
                GBDCodes(5) = "U111"
                GBDCodes(6) = "U059"
                GBDCodes(7) = "U150"
                GBDCodes(8) = "U149"
                GBDCodes(9) = "U156"
                GBDCodes(10) = "U079"
                GBDCodes(11) = "U009"
                GBDCodes(12) = "U010"
                GBDCodes(13) = "U020"
                GBDCodes(14) = "U038"

                Dim GBDCodes2(3) As String
                GBDCodes2(1) = "U009"
                GBDCodes2(2) = "U060"
                GBDCodes2(3) = "U150"

                Dim GBDCodesNewComm(3) As String
                GBDCodesNewComm(1) = "U010"
                GBDCodesNewComm(2) = "U020"
                GBDCodesNewComm(3) = "U038"

                Dim GBDCodes6(5) As String
                GBDCodes6(1) = "U104"
                GBDCodes6(2) = "U115"
                GBDCodes6(3) = "U111"
                GBDCodes6(4) = "U079"
                GBDCodes6(5) = "U081"
                Using db As IFs.Data.IFs2Context = New IFs2Context()
                    db.Clear(GetType(HealthRatioYLDYLLRegion))
                    Dim conn = db.Database.GetDbConnection
                    If conn.State = 0 Then 'closed
                        conn.Open()
                    End If
                    For T = 2 To 14
                        'Simple ones not 1, 6, 8
                        If T <> 6 And T <> 8 Then
                            'Gender
                            For G = 1 To 2
                                Using cmd = conn.CreateCommand()
                                    sql = "INSERT INTO HealthRatioYLDYLLRegion " &
                "SELECT GroupingFull.RegionNo as RegionId, " & CStr(T) & " AS Code, HealthGBDYLD.Gender, " &
                "[HealthGBDYLD].[Total]/[HealthGBDYLL].[Total] AS RatioYLDYLL " &
                "FROM HealthGBDYLD, HealthGBDYLL, GroupIDNewFull, GroupingFull " &
                "WHERE HealthGBDYLD.Code = HealthGBDYLL.Code AND (HealthGBDYLD.Gender = HealthGBDYLL.Gender) AND " &
                "(HealthGBDYLD.GroupName = HealthGBDYLL.GroupName) AND " &
                "HealthGBDYLL.GroupName = GroupIDNewFull.GroupName AND " &
                "GroupIDNewFull.GroupNum = GroupingFull.GroupNo AND " &
                "(((HealthGBDYLD.gender) = " & CStr(G) & ") And ((HealthGBDYLD.GroupName) <> 'World') And " &
                "((HealthGBDYLD.Code) = '" & GBDCodes(T) & "')) " &
                "ORDER BY GroupingFull.RegionNo"
                                    cmd.CommandText = sql
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next G
                            'jrs 2009/06/04 Next c2
                            'jrs 2009/06/04 Next C
                        End If
                    Next T
                    'Complicated ones, retrieve and compute locally
                    Dim ComplicatedTypes(3) As Integer
                    ComplicatedTypes(1) = 1
                    ComplicatedTypes(2) = 6
                    ComplicatedTypes(3) = 8
                    'create empty table

                    Using cmd = conn.CreateCommand()
                        'sql = "SELECT HealthGBDYLD.* INTO TMPHealthGBDYLD From HealthGBDYLD " &
                        cmd.CommandText = "DROP TABLE IF EXISTS TMPHealthGBDYLD"
                        cmd.ExecuteNonQuery()
                        sql = "CREATE TABLE TMPHealthGBDYLD as SELECT HealthGBDYLD.* From HealthGBDYLD " &
                        "WHERE HealthGBDYLD.GroupName is Null"
                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()
                    End Using

                    'create empty table
                    Using cmd = conn.CreateCommand()
                        'sql = "SELECT HealthGBDYLD.* INTO TMPHealthGBDYLD2 From HealthGBDYLD " &
                        cmd.CommandText = "DROP TABLE IF EXISTS TMPHealthGBDYLD2"
                        cmd.ExecuteNonQuery()
                        sql = "CREATE TABLE TMPHealthGBDYLD2 as SELECT HealthGBDYLD.* From HealthGBDYLD " &
                        "WHERE HealthGBDYLD.GroupName is Null"
                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()
                    End Using
                    'create empty table
                    Using cmd = conn.CreateCommand()
                        'sql = "SELECT HealthGBDYLL.* INTO TMPHealthGBDYLL From HealthGBDYLL " &
                        cmd.CommandText = "DROP TABLE IF EXISTS TMPHealthGBDYLL"
                        cmd.ExecuteNonQuery()
                        sql = "CREATE TABLE TMPHealthGBDYLL as SELECT HealthGBDYLL.* From HealthGBDYLL " &
                        "WHERE HealthGBDYLL.GroupName is Null"
                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()
                    End Using
                    'create empty table
                    Using cmd = conn.CreateCommand()
                        'sql = "CREATE TABLE TMPHealthGBDYLL as 
                        'sql = "SELECT HealthGBDYLL.* INTO TMPHealthGBDYLL2 From HealthGBDYLL " &
                        cmd.CommandText = "DROP TABLE IF EXISTS TMPHealthGBDYLL2"
                        cmd.ExecuteNonQuery()
                        sql = "CREATE TABLE TMPHealthGBDYLL2 as SELECT HealthGBDYLL.* From HealthGBDYLL " &
                        "WHERE HealthGBDYLL.GroupName is Null"
                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()
                    End Using
                    Dim tt As Integer
                    For T = 1 To 3
                        'YLD
                        '---
                        'Insert First Type into Temp table
                        Using cmd = conn.CreateCommand()
                            sql = "INSERT INTO TMPHealthGBDYLD SELECT HealthGBDYLD.* FROM HealthGBDYLD " &
                            "WHERE (((HealthGBDYLD.Code)='" & GBDCodes(ComplicatedTypes(T)) & "') AND ((HealthGBDYLD.GroupName)<>'World'))"
                            cmd.CommandText = sql
                            cmd.ExecuteNonQuery()
                        End Using

                        'Insert other types with negative values (to sum them later)
                        Using cmd = conn.CreateCommand()
                            sql = "INSERT INTO TMPHealthGBDYLD(Code, [Total], Gender, GroupName) " &
        "SELECT '" & GBDCodes(ComplicatedTypes(T)) & "',  -1*[Total], Gender, GroupName FROM HealthGBDYLD " &
        "WHERE (((HealthGBDYLD.Code)='" & GBDCodes2(T) & "') AND ((HealthGBDYLD.GroupName)<>'World'))"
                            cmd.CommandText = sql
                            cmd.ExecuteNonQuery()
                        End Using

                        'for Other Non-communicable Diseases we need to remove more than one type
                        If ComplicatedTypes(T) = 6 Then
                            For tt = 1 To 5
                                Using cmd = conn.CreateCommand()
                                    sql = "INSERT INTO TMPHealthGBDYLD(Code, [Total], Gender, GroupName) " &
                "SELECT '" & GBDCodes(ComplicatedTypes(T)) & "',  -1*[Total], Gender, GroupName FROM HealthGBDYLD " &
                "WHERE (((HealthGBDYLD.Code)='" & GBDCodes6(tt) & "') AND ((HealthGBDYLD.GroupName)<>'World'))"
                                    cmd.CommandText = sql
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next tt
                            'jrs 2009/08/09
                        ElseIf ComplicatedTypes(T) = 1 Then
                            For tt = 1 To 3
                                Using cmd = conn.CreateCommand()
                                    sql = "INSERT INTO TMPHealthGBDYLD(Code, [Total], Gender, GroupName) " &
                                    "SELECT '" & GBDCodes(ComplicatedTypes(T)) & "',  -1*[Total], Gender, GroupName FROM HealthGBDYLD " &
                                    "WHERE (((HealthGBDYLD.Code)='" & GBDCodesNewComm(tt) & "') AND ((HealthGBDYLD.GroupName)<>'World'))"
                                    cmd.CommandText = sql
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next tt
                        End If
                        'insert into final table
                        Using cmd = conn.CreateCommand()
                            sql = "INSERT INTO TMPHealthGBDYLD2(Code, [Total], Gender, GroupName) " &
        "SELECT Code, SUM([Total]), Gender, GroupName " &
        "FROM TMPHealthGBDYLD " &
        "WHERE Code = '" & GBDCodes(ComplicatedTypes(T)) &
        "' GROUP BY Code, Gender, GroupName"
                            cmd.CommandText = sql
                            cmd.ExecuteNonQuery()
                        End Using

                        'YLL
                        '---

                        'Insert First Type into Temp table
                        Using cmd = conn.CreateCommand()
                            sql = "INSERT INTO TMPHealthGBDYLL SELECT HealthGBDYLL.* FROM HealthGBDYLL " &
                            "WHERE (((HealthGBDYLL.Code)='" & GBDCodes(ComplicatedTypes(T)) & "') AND ((HealthGBDYLL.GroupName)<>'World'))"
                            cmd.CommandText = sql
                            cmd.ExecuteNonQuery()
                        End Using
                        'Insert other types with negative values (to sum them later)
                        Using cmd = conn.CreateCommand()
                            sql = "INSERT INTO TMPHealthGBDYLL(Code, [Total], Gender, GroupName) " &
        "SELECT '" & GBDCodes(ComplicatedTypes(T)) & "',  -1*[Total], Gender, GroupName FROM HealthGBDYLL " &
        "WHERE (((HealthGBDYLL.Code)='" & GBDCodes2(T) & "') AND ((HealthGBDYLL.GroupName)<>'World'))"
                            cmd.CommandText = sql
                            cmd.ExecuteNonQuery()
                        End Using

                        'for Other Non-communicable Diseases we need to remove more than one type
                        If ComplicatedTypes(T) = 6 Then
                            For tt = 1 To 5
                                Using cmd = conn.CreateCommand()
                                    sql = "INSERT INTO TMPHealthGBDYLL(Code, [Total], Gender, GroupName) " &
                "SELECT '" & GBDCodes(ComplicatedTypes(T)) & "',  -1*[Total], Gender, GroupName FROM HealthGBDYLL " &
                "WHERE (((HealthGBDYLL.Code)='" & GBDCodes6(tt) & "') AND ((HealthGBDYLL.GroupName)<>'World'))"
                                    cmd.CommandText = sql
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next tt
                            'jrs 2009/08/10
                        ElseIf ComplicatedTypes(T) = 1 Then
                            For tt = 1 To 3
                                Using cmd = conn.CreateCommand()
                                    sql = "INSERT INTO TMPHealthGBDYLL(Code, [Total], Gender, GroupName) " &
                                    "SELECT '" & GBDCodes(ComplicatedTypes(T)) & "',  -1*[Total], Gender, GroupName FROM HealthGBDYLL " &
                                    "WHERE (((HealthGBDYLL.Code)='" & GBDCodesNewComm(tt) & "') AND ((HealthGBDYLL.GroupName)<>'World'))"
                                    cmd.CommandText = sql
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next tt
                        End If

                        'insert into final table

                        Using cmd = conn.CreateCommand()
                            sql = "INSERT INTO TMPHealthGBDYLL2(Code, [Total], Gender, GroupName) " &
                            "SELECT Code, SUM([Total]), Gender, GroupName " &
                            "FROM TMPHealthGBDYLL " &
                            "WHERE Code = '" & GBDCodes(ComplicatedTypes(T)) &
                            "' GROUP BY Code, Gender, GroupName"
                            cmd.CommandText = sql
                            cmd.ExecuteNonQuery()
                        End Using
                    Next T

                    'delete temp table
                    Using cmd = conn.CreateCommand()
                        cmd.CommandText = "DROP TABLE [TMPHealthGBDYLD]"
                        cmd.ExecuteNonQuery()
                    End Using
                    Using cmd = conn.CreateCommand()
                        cmd.CommandText = "DROP TABLE [TMPHealthGBDYLL]"
                        cmd.ExecuteNonQuery()
                    End Using
                    'insert into final table
                    For T = 1 To 3
                        'Gender
                        For G = 1 To 2
                            Using cmd = conn.CreateCommand()
                                sql = "INSERT INTO HealthRatioYLDYLLRegion " &
                "SELECT GroupingFull.RegionNo as RegionId, " & CStr(ComplicatedTypes(T)) & " AS Code, TMPHealthGBDYLD2.Gender, " &
                "[TMPHealthGBDYLD2].[Total]/[TMPHealthGBDYLL2].[Total] AS RatioYLDYLL " &
                "FROM TMPHealthGBDYLD2, TMPHealthGBDYLL2, GroupIDNewFull, GroupingFull " &
                "WHERE TMPHealthGBDYLD2.Code = TMPHealthGBDYLL2.Code AND (TMPHealthGBDYLD2.Gender = TMPHealthGBDYLL2.Gender) AND " &
                "(TMPHealthGBDYLD2.GroupName = TMPHealthGBDYLL2.GroupName) AND " &
                "TMPHealthGBDYLL2.GroupName = GroupIDNewFull.GroupName AND " &
                "GroupIDNewFull.GroupNum = GroupingFull.GroupNo AND " &
                "(((TMPHealthGBDYLD2.gender) = " & CStr(G) & ") And ((TMPHealthGBDYLD2.GroupName) <> 'World') And " &
                "((TMPHealthGBDYLD2.Code) = '" & GBDCodes(ComplicatedTypes(T)) & "')) " &
                "ORDER BY GroupingFull.RegionNo"
                                cmd.CommandText = sql
                                cmd.ExecuteNonQuery()
                            End Using
                        Next G
                        'jrs 2009/06/04 Next c2
                        'jrs 2009/06/04 Next C
                    Next T

                    'jrs 2009/08/12
                    'UPDATE nulls with 0s, division by 0 is producing nulls

                    Using cmd = conn.CreateCommand()
                        cmd.CommandText = "UPDATE HealthRatioYLDYLLRegion SET RatioYLDYLL = 0 WHERE RatioYLDYLL IS NULL"
                        cmd.ExecuteNonQuery()
                    End Using

                    'delete temp table
                    Using cmd = conn.CreateCommand()
                        cmd.CommandText = "DROP TABLE TMPHealthGBDYLD2"
                        cmd.ExecuteNonQuery()
                    End Using
                    Using cmd = conn.CreateCommand()
                        cmd.CommandText = "DROP TABLE TMPHealthGBDYLL2"
                        cmd.ExecuteNonQuery()
                    End Using
                    'jrs 2018/05/28
                    'create empty table
                    Using cmd = conn.CreateCommand()
                        'sql = "CREATE TABLE TMPHealthGBDYLD as 
                        cmd.CommandText = "DROP TABLE IF EXISTS TMPHealthRatioYLDYLLRegion"
                        cmd.ExecuteNonQuery()
                        'sql = "SELECT HealthRatioYLDYLLRegion.* INTO TMPHealthRatioYLDYLLRegion From HealthRatioYLDYLLRegion " &
                        sql = "CREATE TABLE TMPHealthRatioYLDYLLRegion as SELECT HealthRatioYLDYLLRegion.* From HealthRatioYLDYLLRegion " &
                    "WHERE HealthRatioYLDYLLRegion.RegionId is Null"
                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()
                    End Using
                    'fill new table with country data
                    For T = 1 To 15
                        For G = 1 To 2
                            Using cmd = conn.CreateCommand()
                                sql = "INSERT INTO TMPHealthRatioYLDYLLRegion " &
                            "SELECT RegionalizationFull.NewRegion as RegionId, " & CStr(T) & " AS Code, HealthGBDCtryYLD.sex as Gender, " &
                            "[HealthGBDCtryYLD].[Total]/[HealthGBDCtryYLL].[Total] AS RatioYLDYLL " &
                            "FROM HealthGBDCtryYLD, HealthGBDCtryYLL, RegionalizationFull " &
                            "WHERE HealthGBDCtryYLD.CauseID = HealthGBDCtryYLL.CauseID AND (HealthGBDCtryYLD.sex = HealthGBDCtryYLL.sex) AND " &
                            "(HealthGBDCtryYLD.Country = HealthGBDCtryYLL.Country) AND " &
                            "HealthGBDCtryYLL.Country = RegionalizationFull.Country AND " &
                            "(((HealthGBDCtryYLD.sex) = " & CStr(G) & ") And " &
                            "((HealthGBDCtryYLD.CauseId) = " & T & ")) " &
                            " AND [HealthGBDCtryYLL].[Total] > 0 " &
                            "ORDER BY RegionalizationFull.NewRegion"
                                cmd.CommandText = sql
                                cmd.ExecuteNonQuery()
                            End Using
                        Next G
                    Next T
                    'delete group data for countries that have data
                    Using cmd = conn.CreateCommand()
                        '  sql = "DELETE HealthRatioYLDYLLRegion.* FROM HealthRatioYLDYLLRegion INNER JOIN TMPHealthRatioYLDYLLRegion ON (TMPHealthRatioYLDYLLRegion.RegionId = HealthRatioYLDYLLRegion.RegionId AND  " &
                        '"TMPHealthRatioYLDYLLRegion.Code = HealthRatioYLDYLLRegion.Code AND TMPHealthRatioYLDYLLRegion.Gender = HealthRatioYLDYLLRegion.Gender)"
                        sql = "DELETE FROM HealthRatioYLDYLLRegion Where Exists(Select * from TMPHealthRatioYLDYLLRegion as T where T.RegionId = HealthRatioYLDYLLRegion.RegionId AND T.Code = HealthRatioYLDYLLRegion.Code AND  " &
             "T.Gender = HealthRatioYLDYLLRegion.Gender)"
                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()
                    End Using
                    'insert country specific data
                    Using cmd = conn.CreateCommand()
                        sql = "INSERT INTO HealthRatioYLDYLLRegion SELECT * FROM TMPHealthRatioYLDYLLRegion"
                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()
                    End Using

                    'delete temp table
                    Using cmd = conn.CreateCommand()
                        cmd.CommandText = "DROP TABLE TMPHealthRatioYLDYLLRegion"
                        cmd.ExecuteNonQuery()
                    End Using

                End Using
            End Sub
            Private Sub LoadDEATHTRPV(cntx As IRebuildFacade, agedstM As Dictionary(Of String, Dictionary(Of String, Single?)), agedstF As Dictionary(Of String, Dictionary(Of String, Single?)))
                'Initialize DEATHRPV
                Dim R As Integer, G As Integer, j As Integer, P As Single
                ReDim DEATHTRPV(NR)

                'Open Detailed Death file (units in deaths per 100,000)
                'Read Traffic Death Rates (by age and gender)
                Using db2 As IFs2Context = New IFs2Context()
                    'Total Traffic Deaths by Region (country)
                    'saved in millions like agedst
                    Dim sRegName As String
                    For Each rec As HealthDetailedDeathsRegs In db2.HealthDetailedDeathsRegs.Where(Function(x) x.CauseID = 7)
                        G = rec.sex
                        R = rec.RegNum
                        sRegName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                        'Load children under 5
                        If G = 1 Then
                            DEATHTRPV(R) = DEATHTRPV(R) + agedstM(sRegName)("Cohort0") * rec.R0 / 1000 'jrs 2011/09/19 now it's deaths per 1000
                            DEATHTRPV(R) = DEATHTRPV(R) + agedstM(sRegName)("Cohort1") * rec.R1 / 1000 'jrs 2011/09/19 now it's deaths per 1000
                        Else
                            DEATHTRPV(R) = DEATHTRPV(R) + agedstF(sRegName)("Cohort0") * rec.R0 / 1000 'jrs 2011/09/19 now it's deaths per 1000
                            DEATHTRPV(R) = DEATHTRPV(R) + agedstF(sRegName)("Cohort1") * rec.R1 / 1000 'jrs 2011/09/19 now it's deaths per 1000
                        End If
                        'Load 5 to 84
                        If G = 1 Then
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort2") * rec.R3 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort3") * rec.R4 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort4") * rec.R5 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort5") * rec.R6 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort6") * rec.R7 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort7") * rec.R8 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort8") * rec.R9 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort9") * rec.R10 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort10") * rec.R11 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort11") * rec.R12 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort12") * rec.R13 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort13") * rec.R14 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort14") * rec.R15 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort15") * rec.R16 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort16") * rec.R17 / 1000
                            DEATHTRPV(R) += agedstM(sRegName)("Cohort17") * rec.R18 / 1000
                        Else
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort2") * rec.R3 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort3") * rec.R4 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort4") * rec.R5 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort5") * rec.R6 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort6") * rec.R7 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort7") * rec.R8 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort8") * rec.R9 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort9") * rec.R10 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort10") * rec.R11 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort11") * rec.R12 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort12") * rec.R13 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort13") * rec.R14 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort14") * rec.R15 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort15") * rec.R16 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort16") * rec.R17 / 1000
                            DEATHTRPV(R) += agedstF(sRegName)("Cohort17") * rec.R18 / 1000
                        End If
                        'Load 85+
                        P = 0
                        For j = 18 To cntx.NCohorts
                            If G = 1 Then
                                P += agedstM(sRegName)("Cohort" & j.ToString)
                            Else
                                P += agedstF(sRegName)("Cohort" & j.ToString)
                            End If
                        Next j
                        DEATHTRPV(R) += P * rec.R19 / 1000
                    Next
                End Using
                'Total # of vehicles per Region
                'VEHICFLPC(R) * POP(R)
                'In Veh per /1000 * Million
                'Deaths are in Millions
                'In order to compute deaths/1000 need to multiply by 1000
                'Deaths per Vehicle, in deaths per 1000 vehicle
                For R = 1 To NR
                    'Dim RegionName = cntx.GetDimension(1).Buckets.Item(CShort(R))
                    'adjust # of cars if too much death
                    If DEATHTRPV(R) / (VEHICFLPC(R) * POP(R) / 1000) * 1000 > 40 Then
                        VEHICFLPC(R) = DEATHTRPV(R) / (40 * POP(R) / 1000) * 1000
                        DEATHTRPV(R) = 40
                    Else
                        DEATHTRPV(R) = DEATHTRPV(R) / (VEHICFLPC(R) * POP(R) / 1000) * 1000
                    End If
                Next R
            End Sub

            Public Sub FillSmokingRateHistCP(cntx As IRebuildFacade)
                Dim iLagYrs As Integer
                iLagYrs = 25
                Dim FirstYearSM As Integer
                FirstYearSM = AMAX(1979, BaseYear - iLagYrs - 1)

                Dim SmImpCohMale(cntx.Regionalization.Count, 4, iLagYrs + 1) As Single?
                Dim SmImpCohFemale(cntx.Regionalization.Count, 4, iLagYrs + 1) As Single?
                LoadSmokingImpact(SmImpCohMale, SmImpCohFemale,
                  "SeriesHealthSmokingImpactMales30to44", "SeriesHealthSmokingImpactFemales30to44", 1, cntx)
                LoadSmokingImpact(SmImpCohMale, SmImpCohFemale,
                  "SeriesHealthSmokingImpactMales45to59", "SeriesHealthSmokingImpactFemales45to59", 2, cntx)
                LoadSmokingImpact(SmImpCohMale, SmImpCohFemale,
                  "SeriesHealthSmokingImpactMales60to69", "SeriesHealthSmokingImpactFemales60to69", 3, cntx)
                LoadSmokingImpact(SmImpCohMale, SmImpCohFemale,
                  "SeriesHealthSmokingImpactMales70to100", "SeriesHealthSmokingImpactFemales70to100", 4, cntx)
                'Compute Smoking Prevalence by Cohort
                '====================================
                Dim SmRtCohMale(cntx.Regionalization.Count, 4, iLagYrs + 1) As Single?
                Dim SmRtCohFemale(cntx.Regionalization.Count, 4, iLagYrs + 1) As Single?
                Dim TbName$
                Dim DrvVals As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For ICount = 1 To cntx.Regionalization.Count
                    For iYr = 1 To iLagYrs + 1
                        TbName$ = "Male 30 to 44 Smoking Impact (2030) Versus Male Smoking Prevalence (2005) Linear"
                        DrvVals = New Dictionary(Of String, Single?)
                        DrvVals.Add("DrvVal", SmImpCohMale(ICount, 1, iYr))
                        SmRtCohMale(ICount, 1, iYr) = cntx.GetRegression(TbName).ToEstimator(DrvVals)("DrvVal")
                        TbName$ = "Male 45 to 59 Smoking Impact (2030) Versus Male Smoking Prevalence (2005) Linear"
                        DrvVals = New Dictionary(Of String, Single?)
                        DrvVals.Add("DrvVal", SmImpCohMale(ICount, 2, iYr))
                        SmRtCohMale(ICount, 2, iYr) = cntx.GetRegression(TbName).ToEstimator(DrvVals)("DrvVal")
                        TbName$ = "Male 60 to 69 Smoking Impact (2030) Versus Male Smoking Prevalence (2005) Linear"
                        DrvVals = New Dictionary(Of String, Single?)
                        DrvVals.Add("DrvVal", SmImpCohMale(ICount, 3, iYr))
                        SmRtCohMale(ICount, 3, iYr) = cntx.GetRegression(TbName).ToEstimator(DrvVals)("DrvVal")
                        TbName$ = "Male 70+ Smoking Impact (2030) Versus Male Smoking Prevalence (2005) Linear"
                        DrvVals = New Dictionary(Of String, Single?)
                        DrvVals.Add("DrvVal", SmImpCohMale(ICount, 4, iYr))
                        SmRtCohMale(ICount, 4, iYr) = cntx.GetRegression(TbName).ToEstimator(DrvVals)("DrvVal")
                        '---
                        TbName$ = "Female 30 to 44 Smok Impact (2030) Versus Female Smoking Prev (2005) Linear"
                        DrvVals = New Dictionary(Of String, Single?)
                        DrvVals.Add("DrvVal", SmImpCohFemale(ICount, 1, iYr))
                        SmRtCohFemale(ICount, 1, iYr) = cntx.GetRegression(TbName).ToEstimator(DrvVals)("DrvVal")

                        TbName$ = "Female 45 to 59 Smok Impact (2030) Versus Female Smoking Prev (2005) Linear"
                        DrvVals = New Dictionary(Of String, Single?)
                        DrvVals.Add("DrvVal", SmImpCohFemale(ICount, 2, iYr))
                        SmRtCohFemale(ICount, 2, iYr) = cntx.GetRegression(TbName).ToEstimator(DrvVals)("DrvVal")

                        TbName$ = "Female 60 to 69 Smok Impact (2030) Versus Female Smoking Prev (2005) Linear"
                        DrvVals = New Dictionary(Of String, Single?)
                        DrvVals.Add("DrvVal", SmImpCohFemale(ICount, 3, iYr))
                        SmRtCohFemale(ICount, 3, iYr) = cntx.GetRegression(TbName).ToEstimator(DrvVals)("DrvVal")

                        TbName$ = "Female 70+ Smok Impact (2030) Versus Female Smoking Prev (2005) Linear"
                        DrvVals = New Dictionary(Of String, Single?)
                        DrvVals.Add("DrvVal", SmImpCohFemale(ICount, 4, iYr))
                        SmRtCohFemale(ICount, 4, iYr) = cntx.GetRegression(TbName).ToEstimator(DrvVals)("DrvVal")
                    Next
                Next
                'Compute simple Average for Smoking Rate (across age cohorts)
                '================================================================
                'keeping age structure constant across time
                Dim SmRtMale(cntx.Regionalization.Count, iLagYrs + 1) As Single
                Dim SmRtFemale(cntx.Regionalization.Count, iLagYrs + 1) As Single
                Dim Gr As Single = 0
                Dim MaxSmRtGrMale(cntx.Regionalization.Count) As Single
                Dim MaxSmRtGrFemale(cntx.Regionalization.Count) As Single
                For ICount = 1 To cntx.Regionalization.Count
                    For iYr = 1 To iLagYrs + 1
                        For aa = 1 To 4
                            SmRtMale(ICount, iYr) = SmRtMale(ICount, iYr) + AMAX(SmRtCohMale(ICount, aa, iYr), 0) 'jrs 2011/10/26 added amax
                            SmRtFemale(ICount, iYr) = SmRtFemale(ICount, iYr) + AMAX(SmRtCohFemale(ICount, aa, iYr), 0) 'jrs 2011/10/26 added amax
                        Next aa
                        SmRtMale(ICount, iYr) = SmRtMale(ICount, iYr) / 4
                        SmRtFemale(ICount, iYr) = SmRtFemale(ICount, iYr) / 4
                        If SmRtMale(ICount, iYr - 1) > 0 Then
                            Gr = (SmRtMale(ICount, iYr) - SmRtMale(ICount, iYr - 1)) / SmRtMale(ICount, iYr - 1)
                            If Gr > MaxSmRtGrMale(ICount) Then
                                MaxSmRtGrMale(ICount) = Gr
                            End If
                        End If
                        If SmRtFemale(ICount, iYr - 1) > 0 Then
                            Gr = (SmRtFemale(ICount, iYr) - SmRtFemale(ICount, iYr - 1)) / SmRtFemale(ICount, iYr - 1)
                            If Gr > MaxSmRtGrFemale(ICount) Then
                                MaxSmRtGrFemale(ICount) = Gr
                            End If
                        End If
                    Next iYr
                Next ICount
                'Add Smoking Rate shift based on Initialization Data.
                '=============================================================
                Dim iMRYrM As Integer, iMRYrF As Integer, YrIdx As Integer
                Dim iMRYrMidx As Integer, iMRYrFidx As Integer
                Dim SmRtShiftMale(cntx.Regionalization.Count) As Single
                Dim SmRtShiftFemale(cntx.Regionalization.Count) As Single
                Dim GroupShiftMale(cntx.Regionalization.Count) As Boolean
                Dim GroupShiftFemale(cntx.Regionalization.Count) As Boolean
                Dim yrsMale As Short() = cntx.GetYears("SeriesHealthSmokingPrevalenceWHOMales").OrderBy(Function(y) y).ToArray()
                Dim tblMale As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesHealthSmokingPrevalenceWHOMales", yrsMale)
                Dim yrsFemale As Short() = cntx.GetYears("SeriesHealthSmokingPrevalenceWHOFemales").OrderBy(Function(y) y).ToArray()
                Dim tblFemale As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesHealthSmokingPrevalenceWHOFemales", yrsFemale)
                For Each key In cntx.Regionalization.Keys
                    Dim ICount = cntx.Regionalization(key)
                    iMRYrM = getMstRctYr(tblMale, yrsMale, key)
                    iMRYrMidx = getYearIdx(yrsMale, iMRYrM)
                    iMRYrF = getMstRctYr(tblFemale, yrsFemale, key)
                    iMRYrFidx = getYearIdx(yrsFemale, iMRYrF)
                    'Males
                    If iMRYrM > FirstYearSM Then
                        If iMRYrM < 2006 Then
                            YrIdx = iMRYrM - FirstYearSM
                        Else
                            YrIdx = iLagYrs + 1
                        End If
                        SmRtShiftMale(ICount) = tblMale(key)(iMRYrMidx) - SmRtMale(ICount, YrIdx)
                    Else
                        SmRtShiftMale(ICount) = 0
                        GroupShiftMale(ICount) = True
                    End If
                    'add shift
                    For iYr = 1 To iLagYrs + 1
                        SmRtMale(ICount, iYr) = SmRtMale(ICount, iYr) + SmRtShiftMale(ICount)
                    Next iYr
                    'Females
                    If iMRYrF > FirstYearSM Then
                        If iMRYrF < 2006 Then
                            YrIdx = iMRYrF - FirstYearSM
                        Else
                            YrIdx = iLagYrs + 1
                        End If
                        SmRtShiftFemale(ICount) = tblFemale(key)(iMRYrFidx) - SmRtFemale(ICount, YrIdx)
                    Else
                        SmRtShiftFemale(ICount) = 0
                        GroupShiftFemale(ICount) = True
                    End If
                    'add shift
                    For iYr = 1 To iLagYrs + 1
                        SmRtFemale(ICount, iYr) = SmRtFemale(ICount, iYr) + SmRtShiftFemale(ICount)
                    Next iYr
                Next
                'Check Max SmRt and Adjust
                '=========================
                Dim MaxSmRt As Single
                MaxSmRt = 82
                Dim Adj As Single, Adj2 As Single
                For Each key In cntx.Regionalization.Keys
                    Dim ICount = cntx.Regionalization(key)
                    'Male
                    If SmRtMale(ICount, iLagYrs + 1) > MaxSmRt Then
                        Adj = MaxSmRt / SmRtMale(ICount, iLagYrs + 1)
                        For iYr = 1 To iLagYrs + 1
                            SmRtMale(ICount, iYr) = SmRtMale(ICount, iYr) * Adj
                        Next iYr
                    End If
                    'Check Min too
                    If SmRtMale(ICount, 1) < 0 Then
                        Adj = -1 * SmRtMale(ICount, 1) + 0.0001
                        Adj2 = SmRtMale(ICount, iLagYrs + 1) / (SmRtMale(ICount, iLagYrs + 1) + Adj)
                        For iYr = iLagYrs + 1 To 1 Step -1
                            SmRtMale(ICount, iYr) = (SmRtMale(ICount, iYr) + Adj) * Adj2
                            If iYr < iLagYrs + 1 Then
                                SmRtMale(ICount, iYr) = AMAX(SmRtMale(ICount, iYr), SmRtMale(ICount, iYr + 1) / (1 + MaxSmRtGrMale(ICount)))
                            End If
                        Next iYr
                    End If
                    'Female
                    If SmRtFemale(ICount, iLagYrs + 1) > MaxSmRt Then
                        Adj = MaxSmRt / SmRtFemale(ICount, iLagYrs + 1)
                        For iYr = 1 To iLagYrs + 1
                            SmRtFemale(ICount, iYr) = SmRtFemale(ICount, iYr) * Adj
                        Next iYr
                    End If
                    'Check Min too
                    If SmRtFemale(ICount, 1) < 0 Then
                        Adj = -1 * SmRtFemale(ICount, 1) + 0.0001
                        Adj2 = SmRtFemale(ICount, iLagYrs + 1) / (SmRtFemale(ICount, iLagYrs + 1) + Adj)
                        For iYr = iLagYrs + 1 To 1 Step -1
                            SmRtFemale(ICount, iYr) = (SmRtFemale(ICount, iYr) + Adj) * Adj2
                            If iYr < iLagYrs + 1 Then
                                SmRtFemale(ICount, iYr) = AMAX(SmRtFemale(ICount, iYr), SmRtFemale(ICount, iYr + 1) / (1 + MaxSmRtGrFemale(ICount)))
                            End If
                        Next iYr
                    End If
                Next
                'Do Aggregation for Shift when there's no data
                '=============================================
                Using db As IFsContext = New IFsContext()
                    '                    Dim Sql = "SELECT Mid(GList.Region,4,1) AS GroupId, " &
                    '"RegionalizationFull.Country" &
                    '    " FROM GListId, GList, GroupIDNewFull, GroupingFull, RegionalizationFull" &
                    '    " WHERE GListId.Code = GList.GListCode" &
                    '    " AND GList.Region = GroupIDNewFull.GroupName" &
                    '    " AND GroupIDNewFull.GroupNum = GroupingFull.GroupNo" &
                    '    " AND GroupingFull.RegionNo = RegionalizationFull.NewRegion" &
                    '    " AND GListId.Description=""WHO Regions""" &
                    '    " ORDER BY RegionalizationFull.Country"
                    Dim rs = (From GLI In db.GListID
                              Join GL In db.GList On GLI.Code Equals GL.GListCode
                              Join GI In db.GroupIDNewFull On GL.RegionName Equals GI.GroupName
                              Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                              Join R In db.RegionalizationFull On G.RegionId Equals R.RegionId
                              Where GLI.Description = "WHO Regions"
                              Order By R.CountryName
                              Select Key = R.CountryName, Value = GL.RegionName.Substring(3, 1)).ToDictionary(
                     Function(x) x.Key,
                     Function(x) x.Value
                     )
                    Dim SmRtAggMale(7) As Single
                    Dim SmRtAggFemale(7) As Single
                    Dim PopAggMale(7) As Single?
                    Dim PopAggFemale(7) As Single?
                    Dim ICount As Integer = 1
                    For Each key In rs.Keys
                        If Not GroupShiftMale(ICount) Then
                            SmRtAggMale(rs(key)) = SmRtAggMale(rs(key)) + SmRtShiftMale(ICount) * CPOP(key)
                            If PopAggMale(rs(key)).HasValue Then
                                PopAggMale(rs(key)) = PopAggMale(rs(key)) + CPOP(key)
                            Else
                                PopAggMale(rs(key)) = CPOP(key)
                            End If
                        End If
                        If Not GroupShiftFemale(ICount) Then
                            SmRtAggFemale(rs(key)) = SmRtAggFemale(rs(key)) + SmRtShiftFemale(ICount) * CPOP(key)
                            If PopAggFemale(rs(key)).HasValue Then
                                PopAggFemale(rs(key)) = PopAggFemale(rs(key)) + CPOP(key)
                            Else
                                PopAggFemale(rs(key)) = CPOP(key)
                            End If
                        End If
                        ICount += 1
                    Next
                    For ICount = 1 To 7
                        If PopAggMale(ICount).HasValue Then
                            SmRtAggMale(ICount) = SmRtAggMale(ICount) / PopAggMale(ICount)
                            If SmRtAggMale(ICount) > MaxSmRt Then
                                SmRtAggMale(ICount) = MaxSmRt
                            End If
                        End If
                        If PopAggFemale(ICount).HasValue Then
                            SmRtAggFemale(ICount) = SmRtAggFemale(ICount) / PopAggFemale(ICount)
                            If SmRtAggFemale(ICount) > MaxSmRt Then
                                SmRtAggFemale(ICount) = MaxSmRt
                            End If
                        End If
                    Next ICount
                    'Find shift
                    ICount = 1
                    For Each key In rs.Keys
                        If GroupShiftMale(ICount) Then
                            SmRtShiftMale(ICount) = SmRtAggMale(CShort(rs(key)))
                            'add shift
                            For iYr = 1 To iLagYrs + 1
                                SmRtMale(ICount, iYr) = SmRtMale(ICount, iYr) + SmRtShiftMale(ICount)
                                SmRtMale(ICount, iYr) = AMAX(Amin(SmRtMale(ICount, iYr), MaxSmRt), 0)
                            Next iYr
                        End If
                        If GroupShiftFemale(ICount) Then
                            SmRtShiftFemale(ICount) = SmRtAggFemale(CShort(rs(key)))
                            'add shift
                            For iYr = 1 To iLagYrs + 1
                                SmRtFemale(ICount, iYr) = SmRtFemale(ICount, iYr) + SmRtShiftFemale(ICount)
                                SmRtFemale(ICount, iYr) = AMAX(Amin(SmRtFemale(ICount, iYr), MaxSmRt), 0)
                            Next iYr
                        End If
                        ICount += 1
                    Next


                    'Save Smoking Rate historic estimate
                    '===================================
                    If System.IO.File.Exists(cntx.PutFdrvRun + "IFsHistSeries.db") Then
                        'jrs 2023/05/30
                        Dim gdpPCPO = (From R In db.RegionalizationFull
                                       Order By R.CountryName
                                       Select Key = R.CountryName, Value = R.FipsCode).ToDictionary(
                         Function(x) x.Key,
                         Function(x) x.Value
                         )

                        Dim dbHis As SqliteConnection, dbCmd As SqliteCommand
                        dbHis = New SqliteConnection("DataSource=" & cntx.PutFdrvRun & "ifsHistSeries.db;")
                        dbHis.Open()
                        dbCmd = dbHis.CreateCommand

                        dbCmd.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
                        dbCmd.ExecuteNonQuery()

                        dbCmd.CommandText = "DELETE FROM [SeriesHealthSmokingMales%SI]"
                        dbCmd.ExecuteNonQuery()

                        dbCmd.CommandText = "DELETE FROM [SeriesHealthSmokingFemales%SI]"
                        dbCmd.ExecuteNonQuery()

                        'jrs 2011/07/26 check the last year versus base
                        Dim yrs As Short() = cntx.GetYears("SeriesHealthSmokingMalesSI").OrderByDescending(Function(yy) yy).ToArray()
                        Dim sIns As String = "INSERT INTO [SeriesHealthSmokingMales%SI]("
                        Dim sFields As String = "Country, FIPS_CODE"
                        Dim sVals As String, sValsF As String

                        For j = yrs.Count - 1 To 0 Step -1
                            If yrs(j) > FirstYearSM And yrs(j) <= BaseYear Then
                                sFields = sFields & ", [" & yrs(j).ToString & "]"
                            End If
                        Next


                        If yrs(0) < BaseYear Then

                            dbCmd.CommandText = "DROP TABLE [SeriesHealthSmokingMales%SI]"
                            dbCmd.ExecuteNonQuery()

                            dbCmd.CommandText = "DROP TABLE [SeriesHealthSmokingFemales%SI]"
                            dbCmd.ExecuteNonQuery()

                            Dim sCreate As String = "CREATE TABLE ""SeriesHealthSmokingMales%SI"" (""Country"" VARCHAR(255), ""FIPS_CODE"" VARCHAR(255)"

                            For j = yrs.Count - 1 To 0 Step -1
                                'jrs 2024/02/02 let's create the table with old fields too or will loose them
                                'If yrs(j) > FirstYearSM Then
                                sCreate = sCreate & ", """ & yrs(j).ToString & """ DOUBLE(53)"
                                'End If
                            Next

                            'jrs 2024/02/02 added + 1, since yrs(0) was already included in previous loop
                            For j = yrs(0) + 1 To BaseYear
                                If j > FirstYearSM Then
                                    sCreate = sCreate & ", """ & j.ToString & """ DOUBLE(53)"
                                    sFields = sFields & ", [" & j.ToString & "]"
                                End If
                            Next

                            sCreate = sCreate & ", Earliest  DOUBLE(53)"
                            sCreate = sCreate & ", MostRecent  DOUBLE(53))"

                            dbCmd.CommandText = sCreate
                            dbCmd.ExecuteNonQuery()

                            sCreate = sCreate.Replace("Male", "Female")
                            dbCmd.CommandText = sCreate
                            dbCmd.ExecuteNonQuery()

                        End If

                        sFields = sFields & ", Earliest, MostRecent"

                        ICount = 0
                        For Each key In rs.Keys
                            sVals = "VALUES(""" & key & """, """ & gdpPCPO(key) & """"
                            sValsF = sVals
                            ICount = ICount + 1

                            For j = yrs.Count - 1 To 0 Step -1
                                If yrs(j) > FirstYearSM Then
                                    Dim iYr = yrs(j) - BaseYear + iLagYrs + 1
                                    If iYr <= iLagYrs + 1 Then
                                        sVals = sVals & ", " & SmRtMale(ICount, iYr).ToString
                                        sValsF = sValsF & ", " & SmRtFemale(ICount, iYr).ToString
                                    End If
                                End If
                            Next

                            'jrs 2023/08/08 yrs(0) was already included in previous loop
                            For j = yrs(0) + 1 To BaseYear
                                If j > FirstYearSM Then
                                    Dim iYr = j - BaseYear + iLagYrs + 1
                                    If iYr <= iLagYrs + 1 Then
                                        sVals = sVals & ", " & SmRtMale(ICount, iYr).ToString
                                        sValsF = sValsF & ", " & SmRtFemale(ICount, iYr).ToString
                                    End If
                                End If
                            Next

                            'Earliest
                            sVals = sVals & ", " & SmRtMale(ICount, 1).ToString
                            sValsF = sValsF & ", " & SmRtFemale(ICount, 1).ToString

                            'Most Recent
                            sVals = sVals & ", " & SmRtMale(ICount, iLagYrs + 1).ToString
                            sValsF = sValsF & ", " & SmRtFemale(ICount, iLagYrs + 1).ToString

                            dbCmd.CommandText = sIns & sFields & ")" & sVals & ")"
                            dbCmd.ExecuteNonQuery()

                            dbCmd.CommandText = sIns.Replace("Male", "Female") & sFields & ")" & sValsF & ")"
                            dbCmd.ExecuteNonQuery()

                        Next

                        dbHis.Close()
                        db.Dispose()
                    End If

                End Using

                'jrs 2023/05/31
                System.IO.File.Copy(cntx.PutFdrvRun + "IFsHistSeries.db", DataFileDir$ + "IfsHistSeries.db", True)
            End Sub
            'Private Sub LoadSmokingImpact(ByRef SmImpMale As Dictionary(Of String, Dictionary(Of String, Dictionary(Of Integer, Single?))),
            '                              ByRef SmImpFemale As Dictionary(Of String, Dictionary(Of String, Dictionary(Of Integer, Single?))),
            '       ByVal sTableMale As String, ByVal sTableFemale As String,
            '       ByVal Cohort As Integer, cntx As IRebuildFacade)
            Private Sub LoadSmokingImpact(ByRef SmImpMale As Single?(,,),
                                          ByRef SmImpFemale As Single?(,,),
                   ByVal sTableMale As String, ByVal sTableFemale As String,
                   ByVal Cohort As Integer, cntx As IRebuildFacade)
                Dim yrsMales As Short() = cntx.GetYears(sTableMale).OrderBy(Function(yy) yy).ToArray()
                Dim MalesTbl As IDictionary(Of String, Single?()) = cntx.GetDice(sTableMale, yrsMales)
                Dim yrsFemales As Short() = cntx.GetYears(sTableFemale).OrderBy(Function(yy) yy).ToArray()
                Dim FemalesTbl As IDictionary(Of String, Single?()) = cntx.GetDice(sTableFemale, yrsFemales)
                'ttt - fixed TbName in VB6 module
                Dim TbNameM$ = "GDP/Capita (PPP 2000) Versus Smoking Impact Male 30 to 44 (" & CStr(cntx.BaseYear) & ") Log"
                TbNameM$ = getFunctionNameYrOrMstRct(TbNameM$, cntx.BaseYear, cntx)
                Dim RegressM = cntx.GetRegression(TbNameM$).ToEstimator(CGDPPCP)
                For Each key In cntx.Regionalization.Keys
                    Dim ICount% = cntx.Regionalization(key)
                    For iYr = 2005 To 2030
                        'Debug.WriteLine(key & ": " & iYr.ToString)
                        'If key = "Montenegro" Then Stop
                        '  SmImp(ICount%, Cohort, iYr - 2005 + 1, gender) = valtbl.Fields(CStr(iYr)).Value
                        Dim ValMale = MalesTbl(key)(CShort(CStr(iYr)) - yrsMales(1) + 1)
                        Dim ValFemale = FemalesTbl(key)(CShort(CStr(iYr)) - yrsFemales(1) + 1)
                        If Not ValMale.HasValue Then
                            If iYr = 2005 Then
                                ValMale = RegressM(key)
                                SmImpMale(ICount%, Cohort, iYr - 2005 + 1) = AMAX(ValMale, 0.1)
                            Else
                                SmImpMale(ICount%, Cohort, iYr - 2005 + 1) = SmImpMale(ICount%, Cohort, 1)
                            End If
                        Else
                            SmImpMale(ICount%, Cohort, iYr - 2005 + 1) = AMAX(ValMale, 0.1)
                        End If
                        If Not ValFemale.HasValue Then
                            If iYr = 2005 Then
                                ValFemale = RegressM(key)
                                SmImpFemale(ICount%, Cohort, iYr - 2005 + 1) = AMAX(ValFemale, 0.1)
                            Else
                                SmImpFemale(ICount%, Cohort, iYr - 2005 + 1) = SmImpFemale(ICount%, Cohort, 1)
                            End If
                        Else
                            SmImpFemale(ICount%, Cohort, iYr - 2005 + 1) = AMAX(ValFemale, 0.1)
                        End If
                    Next
                Next
            End Sub

        End Class
    End Module
End Namespace
