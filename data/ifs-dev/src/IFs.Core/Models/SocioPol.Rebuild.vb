Option Explicit On
Option Strict Off
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data

Namespace Models
    Partial Public Module SocioPol
        <RebuildsVariable("FREEDOM", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("ECONFREE", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("GOVEFFECT", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("GOVCORRUPT", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("GOVBUSCOST", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("GOVBUSREGIND", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("GOVREGQUAL", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("GOVEMBED", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("DEMOCPOLITY", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("DEMOCPOLYARCHY", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("WOMENINLEGISLATURE", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("WOMENINJOURNALISM", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("WOMENPROPRIGHTS", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("Theta1", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("Theta2", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("Theta3", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("ethnic1", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("ethnic2", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("NUMNWP", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("NUMNWPGR", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("GEM", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("GII", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("DRUGUSEAMPHETAMINE", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("DRUGUSECOCAINE", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("DRUGUSEOPIATES", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("DRUGUSEPRESCRIPTOPIOID", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("SVDTHSSELFHARM", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("SVDTHSCONFLICT", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("SVDTHSPOLICS", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("SVDTHSWOMENCHILDREN", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("SVDTHSOTHERINTERPERSON", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("SOCIETALVIOLENCEDEATHS", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("SOCIETALVIOLENCEMEASURE", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("SVDTHSWOMEN", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("SVDTHSCHIL", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("eumembsw", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("cpowf", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("nmilf", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("NPOW", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("PRDSEC", AggregatorType:=GetType(SocPolAggregator))>
        <RebuildsVariable("LDRTRIP", AggregatorType:=GetType(SocPolAggregator))>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        <ImportsAsset("CPOPURBAN")>
        <ImportsAsset("CINCOMELTMID")>
        <ImportsAsset("CGDP")>
        <ImportsAsset("CGINIDOM")>
        <ImportsAsset("CGCon")>
        <ImportsAsset("GExpDef")>
        Public Iterator Function RebuildSocioPol(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Yield context.Query("FREEDOM").Filter(BaseYearOnly).Execute()
            Yield context.Query("ECONFREE").Filter(BaseYearOnly).Execute()
            Yield context.Query("GOVEFFECT").Filter(BaseYearOnly).Execute()
            Yield context.Query("GOVCORRUPT").Filter(BaseYearOnly).Execute()
            Yield context.Query("GOVBUSCOST").Filter(BaseYearOnly).Execute()
            Yield context.Query("GOVBUSREGIND").Filter(BaseYearOnly).Execute()
            Yield context.Query("GOVREGQUAL").Filter(BaseYearOnly).Execute()
            Yield context.Query("GOVEMBED").Filter(BaseYearOnly).Execute()
            Yield context.Query("DEMOCPOLITY").Filter(BaseYearOnly).Execute()
            'jrs 2023/10/13
            Yield context.Query("DEMOCPOLYARCHY").Filter(BaseYearOnly).Execute()
            'clp 2026/02/26 Add V-Dem women's indicators
            Yield context.Query("WOMENINLEGISLATURE").Filter(BaseYearOnly).Execute()
            Yield context.Query("WOMENINJOURNALISM").Filter(BaseYearOnly).Execute()
            Yield context.Query("WOMENPROPRIGHTS").Filter(BaseYearOnly).Execute()
            Yield context.Query("Theta1").Filter(BaseYearOnly).Execute()
            Yield context.Query("Theta2").Filter(BaseYearOnly).Execute()
            Yield context.Query("Theta3").Filter(BaseYearOnly).Execute()
            Yield context.Query("LDRTRIP").Filter(BaseYearOnly).Execute()

            Dim dfTmp As IDataframe = context.Query("ethnic1").Filter(BaseYearOnly).Execute()
            Dim dfethnic1 As Dataframe = Dataframe.Create("ethnic1", context, context.FirstYear, context.LastYear, Nothing)
            Dim tmpIdx As Integer
            For R = 1 To CShort(NR)
                tmpIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, CShort(R), CShort(NR))
                For y = FirstYear To LastYear
                    dfethnic1(getDataFrameIdxFromDims(y, FirstYear, CShort(R), CShort(NR))) = dfTmp(tmpIdx)
                Next
            Next
            Yield dfethnic1

            dfTmp = context.Query("ethnic2").Filter(BaseYearOnly).Execute()
            Dim dfethnic2 As Dataframe = Dataframe.Create("ethnic2", context, context.FirstYear, context.LastYear, Nothing)
            For R = 1 To CShort(NR)
                tmpIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, CShort(R), CShort(NR))
                For y = FirstYear To LastYear
                    dfethnic2(getDataFrameIdxFromDims(y, FirstYear, CShort(R), CShort(NR))) = dfTmp(tmpIdx)
                Next
            Next
            Yield dfethnic2

            Yield context.Query("NUMNWP").Filter(BaseYearOnly).Execute()
            Yield context.Query("NUMNWPGR").Filter(BaseYearOnly).Execute()
            Yield context.Query("GEM").Filter(BaseYearOnly).Execute()
            Yield context.Query("GII").Filter(BaseYearOnly).Execute()
            Yield context.Query("DRUGUSEAMPHETAMINE").Filter(BaseYearOnly).Execute()
            Yield context.Query("DRUGUSECOCAINE").Filter(BaseYearOnly).Execute()
            Yield context.Query("DRUGUSEOPIATES").Filter(BaseYearOnly).Execute()
            Yield context.Query("DRUGUSEPRESCRIPTOPIOID").Filter(BaseYearOnly).Execute()
            Yield context.Query("SVDTHSSELFHARM").Filter(BaseYearOnly).Execute()
            Yield context.Query("SVDTHSCONFLICT").Filter(BaseYearOnly).Execute()
            Yield context.Query("SVDTHSPOLICS").Filter(BaseYearOnly).Execute()
            Yield context.Query("SVDTHSWOMENCHILDREN").Filter(BaseYearOnly).Execute()
            Yield context.Query("SVDTHSOTHERINTERPERSON").Filter(BaseYearOnly).Execute()
            Yield context.Query("SOCIETALVIOLENCEDEATHS").Filter(BaseYearOnly).Execute()

            dfTmp = context.Query("SOCIETALVIOLENCEMEASURE").Filter(BaseYearOnly).Execute()
            Dim dfSOCIETALVIOLENCEMEASURE As Dataframe = Dataframe.Create("SOCIETALVIOLENCEMEASURE", context, context.FirstYear, context.LastYear, Nothing)
            For R = 1 To NR
                tmpIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, CShort(R), CShort(NR))
                For y = FirstYear To LastYear
                    dfSOCIETALVIOLENCEMEASURE(getDataFrameIdxFromDims(y, FirstYear, CShort(R), CShort(NR))) = dfTmp(tmpIdx)
                Next
            Next
            Yield dfSOCIETALVIOLENCEMEASURE

            Yield context.Query("SVDTHSWOMEN").Filter(BaseYearOnly).Execute()
            Yield context.Query("SVDTHSCHIL").Filter(BaseYearOnly).Execute()

            dfTmp = context.Query("eumembsw").Filter(BaseYearOnly).Execute()
            Dim dfeumembsw As Dataframe = Dataframe.Create("eumembsw", context, context.FirstYear, context.LastYear, Nothing)
            For R = 1 To NR
                tmpIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, CShort(R), CShort(NR))
                For y = FirstYear To LastYear
                    dfeumembsw(getDataFrameIdxFromDims(y, FirstYear, CShort(R), CShort(NR))) = dfTmp(tmpIdx)
                Next
            Next
            Yield dfeumembsw

            Yield context.Query("cpowf").Filter(BaseYearOnly).Execute()
            dfTmp = context.Query("nmilf").Filter(BaseYearOnly).Execute()
            Dim dfnmilf As Dataframe = Dataframe.Create("nmilf", context, context.FirstYear, context.LastYear, Nothing)
            For R = 1 To NR
                tmpIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, CShort(R), CShort(NR))
                For y = FirstYear To LastYear
                    dfnmilf(getDataFrameIdxFromDims(y, FirstYear, CShort(R), CShort(NR))) = dfTmp(tmpIdx)
                Next
            Next
            Yield dfnmilf
            Yield context.Query("NPOW").Filter(BaseYearOnly).Execute()
            'jrs 2025/08/28
            Yield context.Query("PRDSEC").Filter(BaseYearOnly).Execute()
        End Function

        <RebuildsVariable("SWINGSTS")>
        <ImportsAsset("CPOP")>
        Public Iterator Function RebuildSwingSts(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Dim CPOP As IDictionary(Of String, Single?)
            CPOP = context.Fetch(Of IDictionary(Of String, Single?))("CPOP")
            Dim CSWINGSTS As IDictionary(Of String, Single?()) = New Dictionary(Of String, Single?())
            For Each key In context.Regionalization.Keys
                Dim VarVals = New List(Of Single?)
                Using db As IFsContext = New IFsContext()
                    Dim sw As SwingStates = db.SwingStates.Where(Function(g) g.Country = key).Single()
                    With VarVals
                        .Add(sw.Brazil)
                        .Add(sw.Indonesia)
                        .Add(sw.Mexico)
                        .Add(sw.Nigeria)
                        .Add(sw.Pakistan)
                        .Add(sw.Russia)
                        .Add(sw.SouthAfrica)
                        .Add(sw.Turkey)
                        .Add(sw.Ukraine)
                    End With
                End Using
                CSWINGSTS.Add(key, VarVals.ToArray)
            Next

            Dim df As Dataframe = Dataframe.Create("SWINGSTS", context, context.FirstYear, context.LastYear, Nothing)
            Dim Series As Dictionary(Of String, Single?)
            Dim i As Integer
            For i = 0 To 8
                Series = New Dictionary(Of String, Single?)
                For Each key In context.Regionalization.Keys
                    Series.Add(key, CSWINGSTS(key)(i))
                Next

                For Each item In (From s In Series
                                  Join r In context.Regionalization On s.Key Equals r.Key
                                  Join w In CPOP On s.Key Equals w.Key
                                  Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
                           )
                    For y = context.FirstYear To context.LastYear
                        df(getDataFrameIdxFromDims(y, FirstYear, i + 1, 9, item.Key, NR)) = item.Value
                    Next
                Next
            Next

            Yield df
        End Function

        <RebuildsVariable("swingstsdem")>
        Public Iterator Function RebuildSwingstsdem(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Dim df As Dataframe = Dataframe.Create("swingstsdem", context, context.FirstYear, context.LastYear, Nothing)
            Using db As IFsContext = New IFsContext()
                For Each item In db.GlobalParametersPolityRegions
                    For y = FirstYear To LastYear
                        df(getDataFrameIdxFromDims(y, FirstYear, item.SwingStateRegion, 9, 1, 3)) = item.GlobalAverage
                        df(getDataFrameIdxFromDims(y, FirstYear, item.SwingStateRegion, 9, 2, 3)) = item.SwingState
                        df(getDataFrameIdxFromDims(y, FirstYear, item.SwingStateRegion, 9, 3, 3)) = item.RegionalAverage
                    Next
                Next
            End Using
            Yield df
        End Function

        Class SocPolAggregator
            Inherits DefaultSumAggregator

            'Private Property CSANITATION1 As IDictionary(Of String, Single?)

            Private Property processed As Boolean
            Private Property CFreedom As IDictionary(Of String, Single?)
            Private Property CEconFree As IDictionary(Of String, Single?)
            Private Property CGovEffect As IDictionary(Of String, Single?)
            Private Property CGovRegQual As IDictionary(Of String, Single?)
            Private Property CGovCorrupt As IDictionary(Of String, Single?)
            Private Property CGovBusCost As IDictionary(Of String, Single?)
            Private Property CGovBusRegInd As IDictionary(Of String, Single?)
            Private Property CEthnic1 As IDictionary(Of String, Single?)
            Private Property CEthnic2 As IDictionary(Of String, Single?)
            Private Property CDemoc As IDictionary(Of String, Single?)
            Private Property CAutoc As IDictionary(Of String, Single?)
            Private Property CDemocPolity As IDictionary(Of String, Single?)
            'jrs 2023/10/13
            Private Property CDemocPolyarchy As IDictionary(Of String, Single?)
            Private Property CWOMENINLEGISLATURE As IDictionary(Of String, Single?) 'clp 2026/02/26 
            Private Property CWOMENINJOURNALISM As IDictionary(Of String, Single?) 'clp 2026/02/26
            Private Property CWOMENPROPRIGHTS As IDictionary(Of String, Single?) 'clp 2026/02/26

            Private Property CTHETA1 As IDictionary(Of String, Single?) 'clp 2026/02/26

            Private Property CTHETA2 As IDictionary(Of String, Single?) 'clp 2026/02/26

            Private Property CTHETA3 As IDictionary(Of String, Single?) 'clp 2026/02/26

            Private Property CGEM As IDictionary(Of String, Single?)
            Private Property CGII As IDictionary(Of String, Single?) 'dkb 2020/02/24
            Private Property CDRUGUSEAMPHETAMINE As IDictionary(Of String, Single?)
            Private Property CDRUGUSECOCAINE As IDictionary(Of String, Single?)
            Private Property CDRUGUSEOPIATES As IDictionary(Of String, Single?)
            Private Property CDRUGUSEPRESCRIPTOPIOID As IDictionary(Of String, Single?)
            Private Property PopOver65 As IDictionary(Of String, Single?)
            Private Property CINCOMELTMID As IDictionary(Of String, Single?)
            Private Property CGCon As IDictionary(Of String, Single?)
            Private Property GExpDef As IDictionary(Of String, Single?)
            Private Property Ythblgtemp As IDictionary(Of String, Single?)
            Private Property Healthexptemp As IDictionary(Of String, Single?)
            Private Property ConsumptionperCapitatemp As IDictionary(Of String, Single?)
            Private Property AmphetamineIHME As IDictionary(Of String, Single?)
            Private Property CocaineIHME As IDictionary(Of String, Single?)
            Private Property OpioidIHME As IDictionary(Of String, Single?)
            Private Property Amph3 As IDictionary(Of String, Single?)
            Private Property Cocaine3 As IDictionary(Of String, Single?)
            Private Property Opiate3 As IDictionary(Of String, Single?)
            Private Property PresOp3 As IDictionary(Of String, Single?)
            Private Property CSVDTHSSELFHARM As IDictionary(Of String, Single?)
            Private Property CSVDTHSCONFLICT As IDictionary(Of String, Single?)
            Private Property CSVDTHSPOLICS As IDictionary(Of String, Single?)
            Private Property CSVDTHSWOMENCHILDREN As IDictionary(Of String, Single?)
            Private Property CSVDTHSOTHERINTERPERSON As IDictionary(Of String, Single?)
            Private Property CSOCIETALVIOLENCEDEATHS As IDictionary(Of String, Single?)
            Private Property CSOCIETALVIOLENCEMEASURE As IDictionary(Of String, Single?)
            Private Property CSVDTHSWOMEN As IDictionary(Of String, Single?)
            Private Property CSVDTHSCHIL As IDictionary(Of String, Single?)
            Private Property CNPow As IDictionary(Of String, Single?)
            'jrs 2025/08/28
            Private Property CPRDSEC As IDictionary(Of String, Single?)
            Private Property CCPowF As IDictionary(Of String, Single?)
            Private Property CNMilF As IDictionary(Of String, Single?)
            Private Property CEmbeddedness As IDictionary(Of String, Single?)
            Private Property CEmbeddednessGR As IDictionary(Of String, Single?)
            Private Property druginitsw As IDictionary(Of String, Single?)
            Private Property CNumNWP As IDictionary(Of String, Single?)
            Private Property CNumNWPGR As IDictionary(Of String, Single?)
            Private Property CEUMembSw As IDictionary(Of String, Single?)
            Private Property CGDPPCP As IDictionary(Of String, Single?)
            Private Property CGINIDOM As IDictionary(Of String, Single?) '??
            Private Property CPOP As IDictionary(Of String, Single?)
            Private Property CPOPURBAN As IDictionary(Of String, Single?)
            Private Property CGDP As IDictionary(Of String, Single?)

            Private Property CLDRTRIP As IDictionary(Of String, Single?)

            <ExportsAsset("CGovCorrupt")>
            Private Sub PreProcess(cntx As IRebuildFacade)
                If Me.processed Then
                    Return
                End If

                Dim objWriter = New System.IO.StreamWriter(cntx.PutFdrvRun + "progress.txt", append:=True)
                objWriter.WriteLine("Processing Data on Socio-Political Systems - Please Wait")
                objWriter.Close()

                Me.CFreedom = New Dictionary(Of String, Single?)
                Me.CEconFree = New Dictionary(Of String, Single?)
                Me.CGovEffect = New Dictionary(Of String, Single?)
                Me.CGovRegQual = New Dictionary(Of String, Single?)
                Me.CGovCorrupt = New Dictionary(Of String, Single?)
                Me.CGovBusCost = New Dictionary(Of String, Single?)
                Me.CGovBusRegInd = New Dictionary(Of String, Single?)
                Me.CEthnic1 = New Dictionary(Of String, Single?)
                Me.CEthnic2 = New Dictionary(Of String, Single?)
                Me.CDemoc = New Dictionary(Of String, Single?)
                Me.CAutoc = New Dictionary(Of String, Single?)
                Me.CDemocPolity = New Dictionary(Of String, Single?)
                'jrs 2023/10/13
                Me.CDemocPolyarchy = New Dictionary(Of String, Single?)
                Me.CWOMENINLEGISLATURE = New Dictionary(Of String, Single?) 'clp 2026/02/26
                Me.CWOMENINJOURNALISM = New Dictionary(Of String, Single?) 'clp 2026/02/26
                Me.CWOMENPROPRIGHTS = New Dictionary(Of String, Single?) 'clp 2026/02/26
                Me.CTHETA1 = New Dictionary(Of String, Single?) 'clp 2026/02/26
                Me.CTHETA2 = New Dictionary(Of String, Single?) 'clp 2026/02/26
                Me.CTHETA3 = New Dictionary(Of String, Single?) 'clp 2026/02/26
                Me.CGEM = New Dictionary(Of String, Single?)
                Me.CGII = New Dictionary(Of String, Single?)
                Me.CDRUGUSEAMPHETAMINE = New Dictionary(Of String, Single?)
                Me.CDRUGUSECOCAINE = New Dictionary(Of String, Single?)
                Me.CDRUGUSEOPIATES = New Dictionary(Of String, Single?)
                Me.CDRUGUSEPRESCRIPTOPIOID = New Dictionary(Of String, Single?)
                Me.Ythblgtemp = New Dictionary(Of String, Single?)
                Me.Healthexptemp = New Dictionary(Of String, Single?)
                Me.ConsumptionperCapitatemp = New Dictionary(Of String, Single?)
                Me.AmphetamineIHME = New Dictionary(Of String, Single?)
                Me.CocaineIHME = New Dictionary(Of String, Single?)
                Me.OpioidIHME = New Dictionary(Of String, Single?)
                Me.Amph3 = New Dictionary(Of String, Single?)
                Me.Cocaine3 = New Dictionary(Of String, Single?)
                Me.Opiate3 = New Dictionary(Of String, Single?)
                Me.PresOp3 = New Dictionary(Of String, Single?)
                Me.CSVDTHSSELFHARM = New Dictionary(Of String, Single?)
                Me.CSVDTHSCONFLICT = New Dictionary(Of String, Single?)
                Me.CSVDTHSPOLICS = New Dictionary(Of String, Single?)
                Me.CSVDTHSWOMENCHILDREN = New Dictionary(Of String, Single?)
                Me.CSVDTHSOTHERINTERPERSON = New Dictionary(Of String, Single?)
                Me.CSOCIETALVIOLENCEDEATHS = New Dictionary(Of String, Single?)
                Me.CSOCIETALVIOLENCEMEASURE = New Dictionary(Of String, Single?)
                Me.CSVDTHSWOMEN = New Dictionary(Of String, Single?)
                Me.CSVDTHSCHIL = New Dictionary(Of String, Single?)
                Me.CNPow = New Dictionary(Of String, Single?)
                'jrs 2025/08/28
                Me.CPRDSEC = New Dictionary(Of String, Single?)
                Me.CLDRTRIP = New Dictionary(Of String, Single?)
                Me.CNMilF = New Dictionary(Of String, Single?)
                Me.CEmbeddedness = New Dictionary(Of String, Single?)
                Me.CEmbeddednessGR = New Dictionary(Of String, Single?)
                Me.druginitsw = New Dictionary(Of String, Single?)
                Me.CCPowF = New Dictionary(Of String, Single?)
                Me.CNumNWP = New Dictionary(Of String, Single?)
                Me.CNumNWPGR = New Dictionary(Of String, Single?)
                Me.CEUMembSw = New Dictionary(Of String, Single?)
                Me.PopOver65 = New Dictionary(Of String, Single?)
                Me.CGDPPCP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
                Me.CGINIDOM = cntx.Fetch(Of IDictionary(Of String, Single?))("CGINIDOM")
                Me.CGDP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDP")
                Me.CPOP = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
                Me.CPOPURBAN = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOPURBAN")
                Me.CINCOMELTMID = cntx.Fetch(Of IDictionary(Of String, Single?))("CINCOMELTMID")
                Me.CGCon = cntx.Fetch(Of IDictionary(Of String, Single?))("CGCon")
                Me.GExpDef = cntx.Fetch(Of IDictionary(Of String, Single?))("GExpDef")

                '-------
                Dim GetField As String = If(cntx.HistoricMode, "Earliest", CStr(cntx.BaseYear))
                Dim GetField2 As String = GetField
                Dim Slice As String = ""
                Dim coal As CoalescePolicy = CoalescePolicy.Null
                Dim Valhold As Single?

                'General---
                GetField2 = If(Not cntx.HistoricMode, "MostRecent", GetField)
                Slice = "SeriesPopulation65PlusMill"
                'JAM 2019-06-29 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                '                  Select Key = s.Key, Value = s.Value)
                '    PopOver65.Add(item.Key, item.Value)
                '    '----
                'Next

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs26 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim Population65PlusMillTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs26)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(Population65PlusMillTbl, yrs26, key, cntx.FirstYear, HistDist)
                        PopOver65.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *******
                    For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                                      Select Key = s.Key, Value = s.Value)
                        PopOver65.Add(item.Key, item.Value)
                        '----
                    Next

                End If

                Dim Estimate = cntx.GetRegression("GDP/Capita (PPP) Versus Freedom -Logged and Inverted (Earliest)").ToEstimator(CGDPPCP)
                'jrs 2024/06/12 Dim Estimate2005 = cntx.GetRegression("GDP/Capita (PPP) Versus Freedom -Logged and Inverted (2005)").ToEstimator(CGDPPCP)
                Dim TblName As String = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Freedom -Logged and Inverted (" & BaseYear & ")"
                TblName = getFunctionNameYrOrMstRct(TblName, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim Estimate2005 = cntx.GetRegression(TblName).ToEstimator(CGDPPCP)

                Slice = "SeriesFreedom"

                'JAM 2019-06-25 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    Dim val = item.Value
                '    If cntx.HistoricMode Then
                '        If val Is Nothing OrElse val < 0.01 Then
                '            val = Estimate(item.Key)
                '        End If
                '    Else
                '        If val Is Nothing OrElse val < 0.01 Then
                '            val = Estimate2005(item.Key)
                '        End If
                '    End If
                '    '-----
                '    val = 16 - val 'Reverse scale
                '    val = AMAX(2, Amin(val, 14))
                '    CFreedom.Add(item.Key, val)

                'Next

                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim FreedomTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(FreedomTbl, yrs, key, cntx.FirstYear, HistDist)

                        'If cntx.HistoricMode Then
                        If ValHold Is Nothing OrElse ValHold < 0.01 Then
                            ValHold = Estimate(key)
                        End If
                        'End If
                        '-----
                        ValHold = 16 - ValHold 'Reverse scale
                        ValHold = AMAX(2, Amin(ValHold, 14))
                        CFreedom.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        Dim val = item.Value
                        'If cntx.HistoricMode Then
                        '    If val Is Nothing OrElse val < 0.01 Then
                        '        val = Estimate(item.Key)
                        '    End If
                        'Else
                        If val Is Nothing OrElse val < 0.01 Then
                            val = Estimate2005(item.Key)
                        End If
                        'End If
                        '-----
                        val = 16 - val 'Reverse scale
                        val = AMAX(2, Amin(val, 14))
                        CFreedom.Add(item.Key, val)

                    Next

                End If

                ' CEconFree
                Estimate = cntx.GetRegression("GDP/Capita (PPP) Versus Economic Freedom (1970)").ToEstimator(CGDPPCP)
                Dim GDPPCP1970 = New Dictionary(Of String, Single?)
                For Each g In CGDPPCP
                    GDPPCP1970.Add(g.Key, g.Value * (1.03) ^ 10)
                Next
                Dim Estimate1970 = cntx.GetRegression("GDP/Capita (PPP) Versus Economic Freedom (1970)").ToEstimator(GDPPCP1970)
                'jrs 2024/06/18
                Dim TFName As String = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Economic Freedom (" & CStr(cntx.BaseYear) & ") Log"
                TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                'Dim EstimateBase = cntx.GetRegression(getFunctionNameYrOrMstRct("GDP/Capita (PPP) Versus Economic Freedom (" & CStr(cntx.BaseYear) & ") Log", cntx.BaseYear, cntx)).ToEstimator(CGDPPCP)
                Dim EstimateBase = cntx.GetRegression(TFName).ToEstimator(CGDPPCP)
                Slice = "SeriesFreedomEcon"

                'JAM 2019-06-25 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    Dim val = item.Value
                '    If cntx.HistoricMode Then
                '        If val Is Nothing OrElse val < 0.01 Then
                '            val = Estimate(item.Key)
                '        Else
                '            Dim Calc1960 = Estimate(item.Key)
                '            Dim Calc1970 = Estimate1970(item.Key)
                '            Dim Growth60to70 = Calc1970 / AMAX(0.01, Calc1960)
                '            val = val / AMAX(0.01, Growth60to70)
                '        End If
                '    Else 'baseyear
                '        If val Is Nothing OrElse val < 0.01 Then
                '            val = EstimateBase(item.Key)
                '        End If
                '    End If
                '    CEconFree.Add(item.Key, AMAX(2, Amin(val, 14)))
                'Next

                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs2 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim FreedomEconTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs2)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(FreedomEconTbl, yrs2, key, cntx.FirstYear, HistDist)
                        'If cntx.HistoricMode Then
                        If ValHold Is Nothing OrElse ValHold < 0.01 Then
                            ValHold = Estimate(key)
                        Else
                            Dim Calc1960 = Estimate(key)
                            Dim Calc1970 = Estimate1970(key)
                            Dim Growth60to70 = Calc1970 / AMAX(0.01, Calc1960)
                            ValHold = ValHold / AMAX(0.01, Growth60to70)
                        End If
                        'End If
                        CEconFree.Add(key, AMAX(2, Amin(ValHold, 14)))

                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        Dim val = item.Value
                        'If cntx.HistoricMode Then
                        '    If val Is Nothing OrElse val < 0.01 Then
                        '        val = Estimate(item.Key)
                        '    Else
                        '        Dim Calc1960 = Estimate(item.Key)
                        '        Dim Calc1970 = Estimate1970(item.Key)
                        '        Dim Growth60to70 = Calc1970 / AMAX(0.01, Calc1960)
                        '        val = val / AMAX(0.01, Growth60to70)
                        '    End If
                        'Else 'baseyear
                        If val Is Nothing OrElse val < 0.01 Then
                            val = EstimateBase(item.Key)
                        End If
                        'End If
                        CEconFree.Add(item.Key, AMAX(2, Amin(val, 14)))
                    Next

                End If

                'jrs 2024/06/17
                TblName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Governance Effectiveness (" & BaseYear & ") Linear"
                TblName = getFunctionNameYrOrMstRct(TblName, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                'Estimate = cntx.GetRegression("GDP/Capita (PPP 2000) Versus Governance Effectiveness (2002) Linear").ToEstimator(CGDPPCP)
                Estimate = cntx.GetRegression(TblName).ToEstimator(CGDPPCP)
                Slice = "SeriesGovernanceEffect"

                'JAM 2019-06-25 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CGovEffect.Add(item.Key, ApplyLimits(CSng(If(item.Value, Estimate(item.Key)) + 2.5), 0, 5))
                'Next

                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs3 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim GovernanceEffectTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs3)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(GovernanceEffectTbl, yrs3, key, cntx.FirstYear, HistDist)

                        CGovEffect.Add(key, ApplyLimits(CSng(If(ValHold, Estimate(key)) + 2.5), 0, 5))



                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CGovEffect.Add(item.Key, ApplyLimits(CSng(If(item.Value, Estimate(item.Key)) + 2.5), 0, 5))
                    Next

                End If

                'jrs 2024/06/21
                TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Governance Reg Quality (" & BaseYear & ") Linear"
                TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                'Estimate = cntx.GetRegression("GDP/Capita (PPP 2000) Versus Governance Reg Quality (2002) Linear").ToEstimator(CGDPPCP)
                Estimate = cntx.GetRegression(TFName).ToEstimator(CGDPPCP)
                'jrs 2024/06/21
                TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Governance Reg Quality (" & BaseYear & ") Linear"
                TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                'Dim Estimate2006 = cntx.GetRegression("GDP/Capita (PPP 2000) Versus Governance Reg Quality (2006) Linear").ToEstimator(CGDPPCP)
                Dim Estimate2006 = cntx.GetRegression(TFName).ToEstimator(CGDPPCP)

                Slice = "SeriesGovernanceRegQual"

                'JAM 2019-06-25 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    If cntx.HistoricMode Then
                '        CGovRegQual.Add(item.Key, ApplyLimits(CSng(If(item.Value, Estimate(item.Key)) + 2.5), 0, 5))
                '    Else
                '        CGovRegQual.Add(item.Key, ApplyLimits(CSng(If(item.Value, Estimate2006(item.Key)) + 2.5), 0, 5))
                '    End If
                'Next

                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs4 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim GovernanceRegQualTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs4)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(GovernanceRegQualTbl, yrs4, key, cntx.FirstYear, HistDist)
                        CGovRegQual.Add(key, ApplyLimits(CSng(If(ValHold, Estimate(key)) + 2.5), 0, 5))

                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        'If cntx.HistoricMode Then
                        '    CGovRegQual.Add(item.Key, ApplyLimits(CSng(If(item.Value, Estimate(item.Key)) + 2.5), 0, 5))
                        'Else
                        CGovRegQual.Add(item.Key, ApplyLimits(CSng(If(item.Value, Estimate2006(item.Key)) + 2.5), 0, 5))
                        'End If
                    Next

                End If



                '--
                'jrs 2024/06/17 Dim TbName$ = "GDP/Capita (PPP 2000) Versus TI Corruption Perception (" & CStr(cntx.BaseYear) & ") Linear"
                Dim TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus TI Corruption Perception (" & CStr(cntx.BaseYear) & ") Linear"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Estimate = cntx.GetRegression(TbName$).ToEstimator(CGDPPCP)
                'jrs 2021/12/03 Slice = "SeriesCorruption"
                Slice = "SeriesCorruptionCPINew"

                'JAM 2019-06-25 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CGovCorrupt.Add(item.Key, ApplyLimits(CSng(If(item.Value, Estimate(item.Key))), 1, 10))
                'Next

                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs5 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim CorruptionTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs5)
                    For Each key In cntx.Regionalization.Keys
                        'jrs 2021/12/03 added / 10
                        Valhold = getClosestYrVal(CorruptionTbl, yrs5, key, cntx.FirstYear, HistDist) / 10
                        CGovCorrupt.Add(key, ApplyLimits(CSng(If(Valhold, Estimate(key))), 1, 10))

                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else
                    'jrs 2021/12/03 added /10 
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value / 10)
                        CGovCorrupt.Add(item.Key, ApplyLimits(CSng(If(item.Value, Estimate(item.Key))), 1, 10))
                    Next

                End If


                '--

                'jrs 2024/06/27
                TbName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Cost of Starting Business (" & CStr(cntx.BaseYear) & ") Log"
                TbName = getFunctionNameYrOrMstRct(TbName, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                'Estimate = cntx.GetRegression(getFunctionNameYrOrMstRct("GDP/Capita (PPP 2011) Versus Cost of Starting Business (" & CStr(cntx.BaseYear) & ") Log", cntx.BaseYear, cntx)).ToEstimator(CGDPPCP)
                Estimate = cntx.GetRegression(TbName).ToEstimator(CGDPPCP)
                Slice = "SeriesGovWBDoingBusinessCostofStarting"


                'JAM 2019-06-26 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CGovBusCost.Add(item.Key, ApplyLimits(CSng(If(item.Value, Estimate(item.Key))), 1, 300))
                'Next

                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs6 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim GovWBDoingBusinessCostofStartingTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs6)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(GovWBDoingBusinessCostofStartingTbl, yrs6, key, cntx.FirstYear, HistDist)

                        CGovBusCost.Add(key, ApplyLimits(CSng(If(ValHold, Estimate(key))), 1, 300))
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CGovBusCost.Add(item.Key, ApplyLimits(CSng(If(item.Value, Estimate(item.Key))), 1, 300))
                    Next

                End If


                Estimate = cntx.GetRegression(getFunctionNameYrOrMstRct("GDP/Capita (PPP 2011) Versus Business Regulation Index (" & CStr(cntx.BaseYear) & ") Log", cntx.BaseYear, cntx)).ToEstimator(CGDPPCP)
                Slice = "SeriesGovBusinessRegulationIndex"
                'JAM 2019-06-26 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CGovBusRegInd.Add(item.Key, ApplyLimits(CSng(If(item.Value, Estimate(item.Key))), 0.01, 10))
                'Next


                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs7 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim GovBusinessRegulationIndexTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs7)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(GovBusinessRegulationIndexTbl, yrs7, key, cntx.FirstYear, HistDist)

                        CGovBusRegInd.Add(key, ApplyLimits(CSng(If(ValHold, Estimate(key))), 0.01, 10))
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CGovBusRegInd.Add(item.Key, ApplyLimits(CSng(If(item.Value, Estimate(item.Key))), 0.01, 10))
                    Next

                End If





                'Democ
                GetField2 = If(cntx.HistoricMode, "1960", GetField)
                Estimate = cntx.GetRegression("GDP/Capita (PPP) Versus Democracy, Polity measure (1980)").ToEstimator(CGDPPCP)
                'jrs 2024/06/18
                TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Democracy, Polity measure (" & BaseYear & ")"
                TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                'Dim Estimate1999 = cntx.GetRegression("GDP/Capita (PPP) Versus Democracy, Polity measure (1999)").ToEstimator(CGDPPCP)
                Dim Estimate1999 = cntx.GetRegression(TFName).ToEstimator(CGDPPCP)
                Slice = "SeriesPolityDemoc"
                'JAM 2019-06-27 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                '                  Select Key = s.Key, Value = s.Value)
                '    Dim val = item.Value
                '    If cntx.HistoricMode Then
                '        If val Is Nothing OrElse val < 0 Then
                '            val = Estimate(item.Key)
                '        End If
                '    Else
                '        If val Is Nothing OrElse val < 0 Then
                '            val = Estimate1999(item.Key)
                '        End If
                '    End If
                '    CDemoc.Add(item.Key, AMAX(0, Amin(val, 10)))
                'Next

                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs8 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim PolityDemocTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs8)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(PolityDemocTbl, yrs8, key, cntx.FirstYear, HistDist)


                        If ValHold Is Nothing OrElse ValHold < 0 Then
                            ValHold = Estimate(key)
                        End If

                        CDemoc.Add(key, AMAX(0, Amin(ValHold, 10)))
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else
                    For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                                      Select Key = s.Key, Value = s.Value)
                        Dim val = item.Value
                        'If cntx.HistoricMode Then
                        '    If val Is Nothing OrElse val < 0 Then
                        '        val = Estimate(item.Key)
                        '    End If
                        'Else
                        If val Is Nothing OrElse val < 0 Then
                            val = Estimate1999(item.Key)
                        End If
                        'End If
                        CDemoc.Add(item.Key, AMAX(0, Amin(val, 10)))
                    Next

                End If

                Slice = "SeriesPolityAutoc"
                'JAM 2019-06-27 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                '                  Select Key = s.Key, Value = s.Value)
                '    Dim val = item.Value
                '    If cntx.HistoricMode Then
                '        If val Is Nothing OrElse val < 0 Then
                '            val = Estimate(item.Key)
                '        End If
                '    Else
                '        If val Is Nothing OrElse val < 0 Then
                '            val = Estimate1999(item.Key)
                '        End If
                '    End If
                '    CAutoc.Add(item.Key, AMAX(0, Amin(val, 10)))
                '    CDemocPolity.Add(item.Key, AMAX(1, CDemoc(item.Key) - CAutoc(item.Key) + 10))
                'Next

                If cntx.HistoricMode Then
                    Estimate = cntx.GetRegression("GDP/Capita (PPP) Versus Autocracy, Polity measure (1980)").ToEstimator(CGDPPCP)
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs9 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim PolityAutocTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs9)
                    For Each key In cntx.Regionalization.Keys
                        Valhold = getClosestYrVal(PolityAutocTbl, yrs9, key, cntx.FirstYear, HistDist)

                        'If cntx.HistoricMode Then
                        If Valhold Is Nothing OrElse Valhold < 0 Then
                            Valhold = Estimate(key)
                        End If
                        'Else
                        '    If val Is Nothing OrElse val < 0 Then
                        '        val = Estimate1999(item.Key)
                        '    End If
                        'End If
                        CAutoc.Add(key, AMAX(0, Amin(Valhold, 10)))
                        CDemocPolity.Add(key, AMAX(1, CDemoc(key) - CAutoc(key) + 10))
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else
                    'jrs 2024/06/18
                    TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Autocracy, Polity measure (" & BaseYear & ")"
                    TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                    'Estimate = cntx.GetRegression("GDP/Capita (PPP) Versus Autocracy, Polity measure (1999)").ToEstimator(CGDPPCP)
                    Estimate = cntx.GetRegression(TFName).ToEstimator(CGDPPCP)

                    For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                                      Select Key = s.Key, Value = s.Value)
                        Dim val = item.Value
                        'If cntx.HistoricMode Then
                        '    If val Is Nothing OrElse val < 0 Then
                        '        val = Estimate(item.Key)
                        '    End If
                        'Else
                        If val Is Nothing OrElse val < 0 Then
                            val = Estimate(item.Key)
                        End If
                        'End If
                        CAutoc.Add(item.Key, AMAX(0, Amin(val, 10)))
                        CDemocPolity.Add(item.Key, AMAX(1, CDemoc(item.Key) - CAutoc(item.Key) + 10))
                    Next

                End If

                'jrs 2023/10/13 *** start ****
                'Polyarchy Index
                '===============
                For Each item In (From s In cntx.GetSlice("SeriesPolyarchy", cntx.FirstYear, CoalescePolicy.Null)
                                  Select Key = s.Key, Value = s.Value)
                    CDemocPolyarchy.Add(item.Key, If(item.Value, -9999))
                Next
                'jrs 2023/10/13 *** end ***

                ' clp 2026/02/26 ***START*** V-Dem women's indicators for Essex collab
                For Each item In (From s In cntx.GetSlice("SeriesFemaleLegShr", cntx.FirstYear, CoalescePolicy.Null)
                                  Select Key = s.Key, Value = s.Value)
                    CWOMENINLEGISLATURE.Add(item.Key, If(item.Value, -9999))
                Next

                For Each item In (From s In cntx.GetSlice("SeriesFemaleJurShr", cntx.FirstYear, CoalescePolicy.Null)
                                  Select Key = s.Key, Value = s.Value)
                    CWOMENINJOURNALISM.Add(item.Key, If(item.Value, -9999))
                Next

                For Each item In (From s In cntx.GetSlice("SeriesWomenPropRights", cntx.FirstYear, CoalescePolicy.Null)
                                  Select Key = s.Key, Value = s.Value)
                    CWOMENPROPRIGHTS.Add(item.Key, If(item.Value, -9999))
                Next

                For Each item In (From s In cntx.GetSlice("SeriesTheta1", cntx.FirstYear, CoalescePolicy.Null)
                                  Select Key = s.Key, Value = s.Value)
                    CTHETA1.Add(item.Key, If(item.Value, -9999))
                Next

                For Each item In (From s In cntx.GetSlice("SeriesTheta2", cntx.FirstYear, CoalescePolicy.Null)
                                  Select Key = s.Key, Value = s.Value)
                    CTHETA2.Add(item.Key, If(item.Value, -9999))
                Next

                For Each item In (From s In cntx.GetSlice("SeriesTheta3", cntx.FirstYear, CoalescePolicy.Null)
                                  Select Key = s.Key, Value = s.Value)
                    CTHETA3.Add(item.Key, If(item.Value, -9999))
                Next
                ' clp 2026/02/26 ***END*** V-Dem women's indicators for Essex collab

                Estimate = cntx.GetRegression("GDP/Capita Versus Global Empowerment Measure").ToEstimator(CGDPPCP)
                Slice = "SeriesGEM"
                'JAM 2019-06-27 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    Dim val = item.Value
                '    If val Is Nothing OrElse val < 0.01 Then
                '        val = Estimate(item.Key)
                '    End If
                '    CGEM.Add(item.Key, AMAX(0, Amin(val, CSng(1.5))))
                'Next
                Dim CGDPPCPK As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each key In cntx.Regionalization.Keys
                    CGDPPCPK.Add(key, CGDPPCP(key) * 1000)
                Next
                'jrs 2024/07/04
                'jrs 2024/07/23 added log to the name
                TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Gender Inequality Index Log"
                TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                'Dim EstimateCGII As SeriesEstimator = cntx.GetRegression("GDP/Capita (PPP) Versus Gender Inequality Index").ToEstimator(CGDPPCPK)
                Dim EstimateCGII As SeriesEstimator = cntx.GetRegression(TFName).ToEstimator(CGDPPCPK)

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs10 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim GEMTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs10)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(GEMTbl, yrs10, key, cntx.FirstYear, HistDist)
                        If ValHold Is Nothing OrElse ValHold < 0.01 Then
                            ValHold = Estimate(key)
                        End If
                        CGEM.Add(key, AMAX(0, Amin(Valhold, CSng(1.5))))
                    Next
                    'mti 2019/04/29  Flex Hist Base End

                    'dkb 2020/02/24 *** start ***
                    Dim yrs As Short() = cntx.GetYears("SeriesGenIneqInd").OrderBy(Function(y) y).ToArray()
                    Dim GIITbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesGenIneqInd", yrs)
                    For Each key In cntx.Regionalization.Keys
                        Valhold = getClosestYrVal(GIITbl, yrs, key, cntx.FirstYear, HistDist)
                        If Valhold Is Nothing OrElse Valhold < 0.01 Then
                            Valhold = EstimateCGII(key)
                        End If
                        CGII.Add(key, AMAX(0, Amin(Valhold, CSng(1))))
                    Next
                    'dkb 2020/02/24 *** end ***

                Else 'Forecast ********
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        Dim val = item.Value
                        If val Is Nothing OrElse val < 0.01 Then
                            val = Estimate(item.Key)
                        End If
                        CGEM.Add(item.Key, AMAX(0, Amin(val, CSng(1.5))))
                    Next

                    'dkb 2020/02/24 *** start ***
                    For Each item In (From s In cntx.GetSlice("SeriesGenIneqInd", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = s.Value)
                        Dim val = item.Value
                        If val Is Nothing OrElse val < 0.01 Then
                            val = EstimateCGII(item.Key)
                        End If
                        CGII.Add(item.Key, AMAX(0, Amin(val, CSng(1))))
                    Next
                    'dkb 2020/02/24 *** end ***
                End If


                'always BaseYear
                For Each item In (From s In cntx.GetSlice("SeriesEthnic1", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value)
                    Dim val = If(item.Value, 0)
                    If val < 0.01 Then val = 100
                    If val > 100 Then val = 100
                    CEthnic1.Add(item.Key, val)
                Next
                For Each item In (From s In cntx.GetSlice("SeriesEthnic2", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value)
                    Dim val = If(item.Value, 0)
                    If val < 0.01 Then val = 100 - CEthnic1(item.Key)
                    If CEthnic1(item.Key) + val > 100 Then
                        val = 100 - CEthnic1(item.Key)
                    End If
                    CEthnic2.Add(item.Key, val)
                Next
                Dim NumNWPBase As Single? = 0
                Dim yrsInternet As Short() = Nothing
                Dim InternetTbl As IDictionary(Of String, Single?()) = Nothing
                If Not cntx.HistoricMode Then
                    yrsInternet = cntx.GetYears("SeriesICTInternetPop").OrderBy(Function(y) y).ToArray()
                    InternetTbl = cntx.GetDice("SeriesICTInternetPop", yrsInternet)
                End If

                Dim CGDPPCPLT40 As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each key In cntx.Regionalization.Keys
                    CGDPPCPLT40.Add(key, Amin(CGDPPCP(key), 40))
                Next

                'jrs 2024/06/19
                TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Internet Percent Use (" & CStr(cntx.BaseYear) & ")"
                TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                'Estimate = cntx.GetRegression(getFunctionNameYrOrMstRct("GDP/Capita (PPP) Versus Internet Percent Use (" & CStr(cntx.BaseYear) & ")", cntx.BaseYear, cntx)).ToEstimator(CGDPPCPLT40)
                Estimate = cntx.GetRegression(TFName).ToEstimator(CGDPPCPLT40)
                For Each item In (From s In cntx.GetSlice("SeriesICTInternetPop", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Select Key = s.Key, Value = s.Value)
                    If cntx.HistoricMode Then
                        CNumNWP.Add(item.Key, 0)
                        CNumNWPGR.Add(item.Key, 0)
                    Else
                        NumNWPBase = item.Value
                        If Not NumNWPBase.HasValue Then
                            NumNWPBase = Estimate(item.Key)
                        End If
                        CNumNWP.Add(item.Key, CPOP(item.Key) * NumNWPBase / 100)
                        CNumNWPGR.Add(item.Key, getAnnualGrowthRate(InternetTbl, yrsInternet, item.Key, cntx.BaseYear, 0.5, 0.05, 4, 5, 10, 10))
                        If CNumNWPGR(item.Key).HasValue Then
                            CNumNWPGR(item.Key) = CNumNWPGR(item.Key) * 100
                        Else
                            CNumNWPGR(item.Key) = 10 ' assuming a 10% growth rate when there is nodata
                        End If
                        CNumNWPGR(item.Key) = AMAX(5, Amin(50, CNumNWPGR(item.Key)))
                    End If
                Next
                GetField2 = If(cntx.HistoricMode, "2000", GetField)
                Dim yrsCEmbed As Short() = Nothing
                Dim CEmbedTbl As IDictionary(Of String, Single?()) = Nothing
                If Not cntx.HistoricMode Then
                    yrsCEmbed = cntx.GetYears("SeriesPoliticalEmbeddCOWJDM").OrderBy(Function(y) y).ToArray()
                    CEmbedTbl = cntx.GetDice("SeriesPoliticalEmbeddCOWJDM", yrsCEmbed)
                End If
                Slice = "SeriesPoliticalEmbeddCOWJDM"

                'JAM 2019-06-27 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                '                  Select Key = s.Key, Value = s.Value)
                '    If cntx.HistoricMode Then
                '        Me.CEmbeddedness.Add(item.Key, If(item.Value, 0))
                '    Else
                '        Me.CEmbeddedness.Add(item.Key, If(item.Value, 0))
                '        Me.CEmbeddednessGR.Add(item.Key, getAnnualGrowthRate(CEmbedTbl, yrsCEmbed, item.Key, cntx.BaseYear, 0.5, 0.05, 4, 5, 10, 10))
                '        If Not CEmbeddednessGR(item.Key).HasValue Then
                '            CEmbeddednessGR(item.Key) = -9999
                '        Else
                '            'Most Recent Year is 2000
                '            '    'we'll forecast constant growth in the next 5 years
                '            CEmbeddedness(item.Key) = CEmbeddedness(item.Key) * (1 + CEmbeddednessGR(item.Key)) ^ (cntx.BaseYear - 2000)
                '        End If
                '    End If
                'Next

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs11 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim PoliticalEmbeddCOWJDMTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs11)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(PoliticalEmbeddCOWJDMTbl, yrs11, key, cntx.FirstYear, HistDist)
                        'If cntx.HistoricMode Then
                        Me.CEmbeddedness.Add(key, If(Valhold, 0))
                        CEmbeddednessGR.Add(key, 0)
                        'Else
                        '    Me.CEmbeddedness.Add(item.Key, If(item.Value, 0))
                        '    Me.CEmbeddednessGR.Add(item.Key, getAnnualGrowthRate(CEmbedTbl, yrsCEmbed, item.Key, cntx.BaseYear, 0.5, 0.05, 4, 5, 10, 10))
                        '    If Not CEmbeddednessGR(item.Key).HasValue Then
                        '        CEmbeddednessGR(item.Key) = -9999
                        '    Else
                        '        'Most Recent Year is 2000
                        '        '    'we'll forecast constant growth in the next 5 years
                        '        CEmbeddedness(item.Key) = CEmbeddedness(item.Key) * (1 + CEmbeddednessGR(item.Key)) ^ (cntx.BaseYear - 2000)
                        '    End If
                        'End If

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                                      Select Key = s.Key, Value = s.Value)
                        'If cntx.HistoricMode Then
                        '    Me.CEmbeddedness.Add(item.Key, If(item.Value, 0))
                        'Else
                        Me.CEmbeddedness.Add(item.Key, If(item.Value, 0))
                        Me.CEmbeddednessGR.Add(item.Key, getAnnualGrowthRate(CEmbedTbl, yrsCEmbed, item.Key, cntx.BaseYear, 0.5, 0.05, 4, 5, 10, 10))
                        If Not CEmbeddednessGR(item.Key).HasValue Then
                            CEmbeddednessGR(item.Key) = -9999
                        Else
                            'Most Recent Year is 2000
                            '    'we'll forecast constant growth in the next 5 years
                            CEmbeddedness(item.Key) = CEmbeddedness(item.Key) * (1 + CEmbeddednessGR(item.Key)) ^ (cntx.BaseYear - 2000)
                        End If
                        'End If
                    Next



                End If


                '-------------Drug-----------------------------
                GetField2 = If(Not cntx.HistoricMode, "MostRecent", GetField)
                Dim AnalFunc As SeriesEstimator
                Slice = "SeriesHealthExpendofGDPForDM"

                'JAM 2019-06-28 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                '                  Select Key = s.Key, Value = s.Value)
                '    Healthexptemp.Add(item.Key, item.Value)
                'Next

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs16 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim HealthExpendofGDPForDMTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs16)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(HealthExpendofGDPForDMTbl, yrs16, key, cntx.FirstYear, HistDist)
                        Healthexptemp.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                                      Select Key = s.Key, Value = s.Value)
                        Healthexptemp.Add(item.Key, item.Value)
                    Next
                End If

                Slice = "SeriesPopYouthBulgeBy15"
                ''JAM 2019-06-28 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                '                  Select Key = s.Key, Value = s.Value)
                '    Ythblgtemp.Add(item.Key, item.Value)
                'Next

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs17 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim PopYouthBulgeBy15Tbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs17)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(PopYouthBulgeBy15Tbl, yrs17, key, cntx.FirstYear, HistDist)
                        Ythblgtemp.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                                      Select Key = s.Key, Value = s.Value)
                        Ythblgtemp.Add(item.Key, item.Value)
                    Next

                End If


                Slice = "SeriesConsumptionperCapitaDM"

                'JAM 2019-06-28 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                '                  Select Key = s.Key, Value = s.Value)
                '    ConsumptionperCapitatemp.Add(item.Key, item.Value)
                'Next

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs18 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim ConsumptionperCapitaDMTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs18)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(ConsumptionperCapitaDMTbl, yrs18, key, cntx.FirstYear, HistDist)
                        ConsumptionperCapitatemp.Add(key, If(Valhold, 0.000001))

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                                      Select Key = s.Key, Value = s.Value)
                        ConsumptionperCapitatemp.Add(item.Key, If(item.Value, 0.000001))
                    Next
                End If


                Slice = "SeriesDrugPrevalenceAmphetaminesIHME"
                'JAM 2019-06-28 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                '                  Select Key = s.Key, Value = s.Value)
                '    AmphetamineIHME.Add(item.Key, item.Value)
                'Next

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs19 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim DrugPrevalenceAmphetaminesIHMETbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs19)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DrugPrevalenceAmphetaminesIHMETbl, yrs19, key, cntx.FirstYear, HistDist)
                        AmphetamineIHME.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                                      Select Key = s.Key, Value = s.Value)
                        AmphetamineIHME.Add(item.Key, item.Value)
                    Next
                End If


                Slice = "SeriesDrugPrevalenceCocaineIHME"
                'JAM 2019-06-28 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                '                  Select Key = s.Key, Value = s.Value)
                '    CocaineIHME.Add(item.Key, item.Value)
                'Next

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs20 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim DrugPrevalenceCocaineIHMETbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs20)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DrugPrevalenceCocaineIHMETbl, yrs20, key, cntx.FirstYear, HistDist)
                        CocaineIHME.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                                      Select Key = s.Key, Value = s.Value)
                        CocaineIHME.Add(item.Key, item.Value)
                    Next
                End If

                Slice = "SeriesDrOpiodPrevIHME"
                'JAM 2019-06-28 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                '                  Select Key = s.Key, Value = s.Value)
                '    OpioidIHME.Add(item.Key, item.Value)
                'Next


                If cntx.HistoricMode Then


                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs21 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim DrOpiodPrevIHMETbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs21)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DrOpiodPrevIHMETbl, yrs21, key, cntx.FirstYear, HistDist)
                        OpioidIHME.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                                      Select Key = s.Key, Value = s.Value)
                        OpioidIHME.Add(item.Key, item.Value)
                    Next
                End If

                Slice = "SeriesDrugPrevAmphetamineRegAvg"
                'JAM 2019-06-28 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                '                  Select Key = s.Key, Value = s.Value)
                '    Amph3.Add(item.Key, If(item.Value, 0))
                'Next


                If cntx.HistoricMode Then


                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs22 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim DrugPrevAmphetamineRegAvgTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs22)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DrugPrevAmphetamineRegAvgTbl, yrs22, key, cntx.FirstYear, HistDist)
                        Amph3.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                                      Select Key = s.Key, Value = s.Value)
                        Amph3.Add(item.Key, If(item.Value, 0))
                    Next

                End If

                Slice = "SeriesDrugPrevCocaineRegAvg"
                'JAM 2019-06-28 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                '                  Select Key = s.Key, Value = s.Value)
                '    Cocaine3.Add(item.Key, If(item.Value, 0))
                'Next

                If cntx.HistoricMode Then


                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs23 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim DrugPrevCocaineRegAvgTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs23)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DrugPrevCocaineRegAvgTbl, yrs23, key, cntx.FirstYear, HistDist)
                        Cocaine3.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                                      Select Key = s.Key, Value = s.Value)
                        Cocaine3.Add(item.Key, If(item.Value, 0))
                    Next
                End If


                Slice = "SeriesDrugPrevOpiateRegAvg"
                'JAM 2019-06-28 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                '                  Select Key = s.Key, Value = s.Value)
                '    Opiate3.Add(item.Key, If(item.Value, 0))
                'Next

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs24 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim DrugPrevOpiateRegAvgTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs24)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DrugPrevOpiateRegAvgTbl, yrs24, key, cntx.FirstYear, HistDist)
                        Opiate3.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *******
                    For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                                      Select Key = s.Key, Value = s.Value)
                        Opiate3.Add(item.Key, If(item.Value, 0))
                    Next
                End If


                Slice = "SeriesDrugPrevPresOpRegAvg"
                'JAM 2019-06-28 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                '                  Select Key = s.Key, Value = s.Value)
                '    PresOp3.Add(item.Key, If(item.Value, 0))
                'Next

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs25 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim DrugPrevPresOpRegAvgTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs25)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DrugPrevPresOpRegAvgTbl, yrs25, key, cntx.FirstYear, HistDist)
                        PresOp3.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *******
                    For Each item In (From s In If(IsShort(GetField2, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField2), coal), cntx.GetSlice(Slice, GetField2))
                                      Select Key = s.Key, Value = s.Value)
                        PresOp3.Add(item.Key, If(item.Value, 0))
                    Next
                End If


                'Call AnalFunc("Amphetmanine Drug prevalence versus youth bulge and GEM", Array(Ythblgtemp(ICount%) / 100, CGEM(ICount%)), Zamph, Array("Youthbulge", "GEM"))
                Dim Ythblgtemp2 = New Dictionary(Of String, Single?)
                For Each cc In Me.Ythblgtemp
                    Ythblgtemp2.Add(cc.Key, cc.Value / 100)
                Next
                AnalFunc = cntx.GetRegression("Amphetmanine Drug prevalence versus youth bulge and GEM").Normalize("Youthbulge", "GEM").ToEstimator(Ythblgtemp2, CGEM)
                Slice = "SeriesDrugPrevalenceRateAmphetamines"

                'JAM 2019-06-27 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CDRUGUSEAMPHETAMINE.Add(item.Key, item.Value)
                '    Using db As IFsContext = New IFsContext()
                '        Dim gp As GlobalParameters = db.GlobalParameters.Where(Function(g) g.ParameterName = "druginitsw").Single()
                '        druginitsw.Add(item.Key, CSng(gp.Value))
                '    End Using
                '    If (Not CDRUGUSEAMPHETAMINE(item.Key).HasValue) AndAlso druginitsw(item.Key) = 1 Then
                '        Estimate = cntx.GetRegression("DrugPrevalenceAmphetaminesIHME(MOSTRECENT) Versus DrPrevRateAmphetamines Linear").ToEstimator(AmphetamineIHME)
                '        CDRUGUSEAMPHETAMINE(item.Key) = ApplyLimits(CSng(Estimate(item.Key)), 0.0001, 2.3)
                '    ElseIf (Not CDRUGUSEAMPHETAMINE(item.Key).HasValue) AndAlso druginitsw(item.Key) = 2 Then
                '        'Call AnalFunc("Amphetmanine Drug prevalence versus youth bulge and GEM", Array(Ythblgtemp(ICount%) / 100, CGEM(ICount%)), Zamph, Array("Youthbulge", "GEM"))
                '        Dim Val As Single = 100 * (Math.E ^ AnalFunc(item.Key) / (1 + Math.E ^ AnalFunc(item.Key))) 'Convert to logistic formulation here
                '        CDRUGUSEAMPHETAMINE(item.Key) = AMAX(0.0001, Amin(Val, CSng(2.3))) 'kbn 2018/01/11 Cap value here
                '    ElseIf (Not CDRUGUSEAMPHETAMINE(item.Key).HasValue) AndAlso druginitsw(item.Key) = 2 Then
                '        CDRUGUSEAMPHETAMINE(item.Key) = Amph3(item.Key)
                '    End If
                'Next

                If cntx.HistoricMode Then


                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs12 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim DrugPrevalenceRateAmphetaminesTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs12)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DrugPrevalenceRateAmphetaminesTbl, yrs12, key, cntx.FirstYear, HistDist)
                        CDRUGUSEAMPHETAMINE.Add(key, ValHold)
                        
                        Using db As IFsContext = New IFsContext()
                            Dim gp As GlobalParameters = db.GlobalParameters.Where(Function(g) g.ParameterName = "druginitsw").Single()
                            druginitsw.Add(key, CSng(gp.Value))
                        End Using
                        If (Not CDRUGUSEAMPHETAMINE(key).HasValue) AndAlso druginitsw(key) = 1 Then
                            Estimate = cntx.GetRegression("DrugPrevalenceAmphetaminesIHME(MOSTRECENT) Versus DrPrevRateAmphetamines Linear").ToEstimator(AmphetamineIHME)
                            CDRUGUSEAMPHETAMINE(key) = ApplyLimits(CSng(Estimate(key)), 0.0001, 2.3)
                        ElseIf (Not CDRUGUSEAMPHETAMINE(Key).HasValue) AndAlso druginitsw(Key) = 2 Then
                            'Call AnalFunc("Amphetmanine Drug prevalence versus youth bulge and GEM", Array(Ythblgtemp(ICount%) / 100, CGEM(ICount%)), Zamph, Array("Youthbulge", "GEM"))
                            Dim Val As Single = 100 * (Math.E ^ AnalFunc(key) / (1 + Math.E ^ AnalFunc(key))) 'Convert to logistic formulation here
                            CDRUGUSEAMPHETAMINE(key) = AMAX(0.0001, Amin(Val, CSng(2.3))) 'kbn 2018/01/11 Cap value here
                        ElseIf (Not CDRUGUSEAMPHETAMINE(Key).HasValue) AndAlso druginitsw(Key) = 2 Then
                            CDRUGUSEAMPHETAMINE(key) = Amph3(key)
                        End If




                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CDRUGUSEAMPHETAMINE.Add(item.Key, item.Value)
                        Using db As IFsContext = New IFsContext()
                            Dim gp As GlobalParameters = db.GlobalParameters.Where(Function(g) g.ParameterName = "druginitsw").Single()
                            druginitsw.Add(item.Key, CSng(gp.Value))
                        End Using
                        If (Not CDRUGUSEAMPHETAMINE(item.Key).HasValue) AndAlso druginitsw(item.Key) = 1 Then
                            Estimate = cntx.GetRegression("DrugPrevalenceAmphetaminesIHME(MOSTRECENT) Versus DrPrevRateAmphetamines Linear").ToEstimator(AmphetamineIHME)
                            CDRUGUSEAMPHETAMINE(item.Key) = ApplyLimits(CSng(Estimate(item.Key)), 0.0001, 2.3)
                        ElseIf (Not CDRUGUSEAMPHETAMINE(item.Key).HasValue) AndAlso druginitsw(item.Key) = 2 Then
                            'Call AnalFunc("Amphetmanine Drug prevalence versus youth bulge and GEM", Array(Ythblgtemp(ICount%) / 100, CGEM(ICount%)), Zamph, Array("Youthbulge", "GEM"))
                            Dim Val As Single = 100 * (Math.E ^ AnalFunc(item.Key) / (1 + Math.E ^ AnalFunc(item.Key))) 'Convert to logistic formulation here
                            CDRUGUSEAMPHETAMINE(item.Key) = AMAX(0.0001, Amin(Val, CSng(2.3))) 'kbn 2018/01/11 Cap value here
                        ElseIf (Not CDRUGUSEAMPHETAMINE(item.Key).HasValue) AndAlso druginitsw(item.Key) = 2 Then
                            CDRUGUSEAMPHETAMINE(item.Key) = Amph3(item.Key)
                        End If
                    Next
                End If


                AnalFunc = cntx.GetRegression("Cocaine Drug prevalence versus consumption per person, GEM and GINI").Normalize("C/POP", "GEM", "GINIDOM").ToEstimator(ConsumptionperCapitatemp, CGEM, CGINIDOM)
                Slice = "SeriesDrugPrevalenceRateCocaine"

                'JAM 2019-06-28 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CDRUGUSECOCAINE.Add(item.Key, item.Value)
                '    If (Not CDRUGUSECOCAINE(item.Key).HasValue) AndAlso druginitsw(item.Key) = 1 Then
                '        Estimate = cntx.GetRegression("DrugPrevalenceCocaineIHME(MOSTRECENT) Versus DrugPrevRateCocaine(MOSTRECENT) Linear").ToEstimator(CocaineIHME)
                '        CDRUGUSECOCAINE(item.Key) = ApplyLimits(CSng(Estimate(item.Key)), 0, 2.3)
                '    ElseIf (Not CDRUGUSECOCAINE(item.Key).HasValue) AndAlso druginitsw(item.Key) = 2 Then
                '        Dim Val As Single = 100 * (Math.E ^ AnalFunc(item.Key) / (1 + Math.E ^ AnalFunc(item.Key))) 'Convert to logistic here
                '        CDRUGUSECOCAINE(item.Key) = AMAX(0, Amin(Val, CSng(2.3))) 'kbn 2018/01/11 Cap value here
                '    ElseIf (Not CDRUGUSECOCAINE(item.Key).HasValue) AndAlso druginitsw(item.Key) = 3 Then
                '        CDRUGUSECOCAINE(item.Key) = Cocaine3(item.Key)
                '    End If
                'Next

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs13 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim DrugPrevalenceRateCocaineTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs13)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DrugPrevalenceRateCocaineTbl, yrs13, key, cntx.FirstYear, HistDist)
                        CDRUGUSECOCAINE.Add(key, ValHold)
                        If (Not CDRUGUSECOCAINE(key).HasValue) AndAlso druginitsw(key) = 1 Then
                            Estimate = cntx.GetRegression("DrugPrevalenceCocaineIHME(MOSTRECENT) Versus DrugPrevRateCocaine(MOSTRECENT) Linear").ToEstimator(CocaineIHME)
                            CDRUGUSECOCAINE(key) = ApplyLimits(CSng(Estimate(key)), 0, 2.3)
                        ElseIf (Not CDRUGUSECOCAINE(Key).HasValue) AndAlso druginitsw(Key) = 2 Then
                            Dim Val As Single = 100 * (Math.E ^ AnalFunc(key) / (1 + Math.E ^ AnalFunc(key))) 'Convert to logistic here
                            CDRUGUSECOCAINE(key) = AMAX(0, Amin(Val, CSng(2.3))) 'kbn 2018/01/11 Cap value here
                        ElseIf (Not CDRUGUSECOCAINE(Key).HasValue) AndAlso druginitsw(Key) = 3 Then
                            CDRUGUSECOCAINE(key) = Cocaine3(key)
                        End If

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CDRUGUSECOCAINE.Add(item.Key, item.Value)
                        If (Not CDRUGUSECOCAINE(item.Key).HasValue) AndAlso druginitsw(item.Key) = 1 Then
                            Estimate = cntx.GetRegression("DrugPrevalenceCocaineIHME(MOSTRECENT) Versus DrugPrevRateCocaine(MOSTRECENT) Linear").ToEstimator(CocaineIHME)
                            CDRUGUSECOCAINE(item.Key) = ApplyLimits(CSng(Estimate(item.Key)), 0, 2.3)
                        ElseIf (Not CDRUGUSECOCAINE(item.Key).HasValue) AndAlso druginitsw(item.Key) = 2 Then
                            Dim Val As Single = 100 * (Math.E ^ AnalFunc(item.Key) / (1 + Math.E ^ AnalFunc(item.Key))) 'Convert to logistic here
                            CDRUGUSECOCAINE(item.Key) = AMAX(0, Amin(Val, CSng(2.3))) 'kbn 2018/01/11 Cap value here
                        ElseIf (Not CDRUGUSECOCAINE(item.Key).HasValue) AndAlso druginitsw(item.Key) = 3 Then
                            CDRUGUSECOCAINE(item.Key) = Cocaine3(item.Key)
                        End If
                    Next
                End If


                Dim UrbanPop = New Dictionary(Of String, Single?)
                Dim Over65Pop = New Dictionary(Of String, Single?)
                Dim Poverty = New Dictionary(Of String, Single?)
                For Each cc In CPOP
                    UrbanPop.Add(cc.Key, 100 * CPOPURBAN(cc.Key) / CPOP(cc.Key))
                    Over65Pop.Add(cc.Key, (PopOver65(cc.Key) / CPOP(cc.Key)) * 100)
                    Poverty.Add(cc.Key, (PopOver65(cc.Key) / 100 * (CINCOMELTMID(cc.Key) / CPOP(cc.Key))))
                Next
                AnalFunc = cntx.GetRegression("Opiate Drug prevalence").Normalize("Urbanpopulation", "POP over the age of 65", "Poverty").ToEstimator(UrbanPop, Over65Pop, Poverty)
                Slice = "SeriesDrugPrevalenceRateOpiates"

                'JAM 2019-06-28 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CDRUGUSEOPIATES.Add(item.Key, item.Value)
                '    If (Not CDRUGUSEOPIATES(item.Key).HasValue) AndAlso druginitsw(item.Key) = 1 Then
                '        Estimate = cntx.GetRegression("DrOpioidPrevIHME(MOSTRECENT) Versus DrugPrevRateOpiates Linear").ToEstimator(OpioidIHME)
                '        CDRUGUSEOPIATES(item.Key) = ApplyLimits(CSng(Estimate(item.Key)), 0.0001, 2.3)
                '    ElseIf (Not CDRUGUSEOPIATES(item.Key).HasValue) AndAlso druginitsw(item.Key) = 2 Then
                '        '                     Call AnalFunc("Opiate Drug prevalence", Array((100 * CPOPURBAN(ICount%) / CPOP(ICount%)),
                '        '(PopOver65(ICount%) / CPOP(ICount%)) * 100, (100 * (CINCOMELT31(ICount%) / CPOP(ICount%)))), Zopiates, Array("Urbanpopulation", "POP over the age of 65", "Poverty")) 'Opiates
                '        '                     CDRUGUSEOPIATES(ICount%) = 100 * (Exp(Zopiates) / (1 + Exp(Zopiates)))
                '        '                     CDRUGUSEOPIATES(ICount%) = AMAX(0.0001, Amin(CDRUGUSEOPIATES(ICount%), 2.3))
                '        Dim Val As Single = 100 * (Math.E ^ AnalFunc(item.Key) / (1 + Math.E ^ AnalFunc(item.Key)))
                '        CDRUGUSEOPIATES(item.Key) = AMAX(0.0001, Amin(Val, CSng(2.3)))
                '    ElseIf (Not CDRUGUSEOPIATES(item.Key).HasValue) AndAlso druginitsw(item.Key) = 3 Then
                '        CDRUGUSEOPIATES(item.Key) = Opiate3(item.Key)
                '    End If
                'Next

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs14 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim DrugPrevalenceRateOpiatesTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs14)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DrugPrevalenceRateOpiatesTbl, yrs14, key, cntx.FirstYear, HistDist)
                        CDRUGUSEOPIATES.Add(key, ValHold)

                        If (Not CDRUGUSEOPIATES(key).HasValue) AndAlso druginitsw(key) = 1 Then
                            Estimate = cntx.GetRegression("DrOpioidPrevIHME(MOSTRECENT) Versus DrugPrevRateOpiates Linear").ToEstimator(OpioidIHME)
                            CDRUGUSEOPIATES(key) = ApplyLimits(CSng(Estimate(key)), 0.0001, 2.3)
                        ElseIf (Not CDRUGUSEOPIATES(Key).HasValue) AndAlso druginitsw(Key) = 2 Then
                            '                     Call AnalFunc("Opiate Drug prevalence", Array((100 * CPOPURBAN(ICount%) / CPOP(ICount%)),
                            '(PopOver65(ICount%) / CPOP(ICount%)) * 100, (100 * (CINCOMELT31(ICount%) / CPOP(ICount%)))), Zopiates, Array("Urbanpopulation", "POP over the age of 65", "Poverty")) 'Opiates
                            '                     CDRUGUSEOPIATES(ICount%) = 100 * (Exp(Zopiates) / (1 + Exp(Zopiates)))
                            '                     CDRUGUSEOPIATES(ICount%) = AMAX(0.0001, Amin(CDRUGUSEOPIATES(ICount%), 2.3))
                            Dim Val As Single = 100 * (Math.E ^ AnalFunc(key) / (1 + Math.E ^ AnalFunc(key)))
                            CDRUGUSEOPIATES(key) = AMAX(0.0001, Amin(Val, CSng(2.3)))
                        ElseIf (Not CDRUGUSEOPIATES(Key).HasValue) AndAlso druginitsw(Key) = 3 Then
                            CDRUGUSEOPIATES(key) = Opiate3(key)
                        End If

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CDRUGUSEOPIATES.Add(item.Key, item.Value)
                        If (Not CDRUGUSEOPIATES(item.Key).HasValue) AndAlso druginitsw(item.Key) = 1 Then
                            Estimate = cntx.GetRegression("DrOpioidPrevIHME(MOSTRECENT) Versus DrugPrevRateOpiates Linear").ToEstimator(OpioidIHME)
                            CDRUGUSEOPIATES(item.Key) = ApplyLimits(CSng(Estimate(item.Key)), 0.0001, 2.3)
                        ElseIf (Not CDRUGUSEOPIATES(item.Key).HasValue) AndAlso druginitsw(item.Key) = 2 Then
                            '                     Call AnalFunc("Opiate Drug prevalence", Array((100 * CPOPURBAN(ICount%) / CPOP(ICount%)),
                            '(PopOver65(ICount%) / CPOP(ICount%)) * 100, (100 * (CINCOMELT31(ICount%) / CPOP(ICount%)))), Zopiates, Array("Urbanpopulation", "POP over the age of 65", "Poverty")) 'Opiates
                            '                     CDRUGUSEOPIATES(ICount%) = 100 * (Exp(Zopiates) / (1 + Exp(Zopiates)))
                            '                     CDRUGUSEOPIATES(ICount%) = AMAX(0.0001, Amin(CDRUGUSEOPIATES(ICount%), 2.3))
                            Dim Val As Single = 100 * (Math.E ^ AnalFunc(item.Key) / (1 + Math.E ^ AnalFunc(item.Key)))
                            CDRUGUSEOPIATES(item.Key) = AMAX(0.0001, Amin(Val, CSng(2.3)))
                        ElseIf (Not CDRUGUSEOPIATES(item.Key).HasValue) AndAlso druginitsw(item.Key) = 3 Then
                            CDRUGUSEOPIATES(item.Key) = Opiate3(item.Key)
                        End If
                    Next

                End If

                'Call AnalFunc("Prescription opioids initialization", Array(100 * CGINIDOM(ICount%), ConsumptionperCapitatemp(ICount%)), Zpresop, Array("CGINIDOM", "Consumption per capita"))
                Dim CGINIDOM2 = New Dictionary(Of String, Single?)
                For Each cc In Me.CGINIDOM
                    CGINIDOM2.Add(cc.Key, 100 * cc.Value)
                Next
                AnalFunc = cntx.GetRegression("Prescription opioids initialization").Normalize("CGINIDOM", "Consumption per capita").ToEstimator(CGINIDOM2, ConsumptionperCapitatemp)
                Dim AnalFunc2 = cntx.GetRegression("Prescription Drug Use prevalence versus health expenditure").Normalize("Healthexpenditure").ToEstimator(Healthexptemp)
                Slice = "SeriesDrugPrevalenceRatePrescriptionOpioids"

                'JAM 2019-06-28 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CDRUGUSEPRESCRIPTOPIOID.Add(item.Key, item.Value)
                '    If (Not CDRUGUSEPRESCRIPTOPIOID(item.Key).HasValue) AndAlso druginitsw(item.Key) = 1 Then
                '        Dim val = CDRUGUSEOPIATES(item.Key) * Math.E ^ AnalFunc(item.Key)
                '        CDRUGUSEPRESCRIPTOPIOID(item.Key) = ApplyLimits(val, 0, 5.5)
                '    ElseIf (Not CDRUGUSEPRESCRIPTOPIOID(item.Key).HasValue) AndAlso druginitsw(item.Key) = 2 Then
                '        Dim val = 100 * (Math.E ^ AnalFunc2(item.Key) / (1 + Math.E ^ AnalFunc2(item.Key)))
                '        CDRUGUSEPRESCRIPTOPIOID(item.Key) = ApplyLimits(val, 0, 5.5)
                '    ElseIf (Not CDRUGUSEPRESCRIPTOPIOID(item.Key).HasValue) AndAlso druginitsw(item.Key) = 3 Then
                '        CDRUGUSEPRESCRIPTOPIOID(item.Key) = PresOp3(item.Key)
                '    End If
                'Next


                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs15 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim DrugPrevalenceRatePrescriptionOpioidsTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs15)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DrugPrevalenceRatePrescriptionOpioidsTbl, yrs15, key, cntx.FirstYear, HistDist)

                        CDRUGUSEPRESCRIPTOPIOID.Add(key, ValHold)
                        If (Not CDRUGUSEPRESCRIPTOPIOID(key).HasValue) AndAlso druginitsw(key) = 1 Then
                            Dim val = CDRUGUSEOPIATES(key) * Math.E ^ AnalFunc(key)
                            CDRUGUSEPRESCRIPTOPIOID(key) = ApplyLimits(val, 0, 5.5)
                        ElseIf (Not CDRUGUSEPRESCRIPTOPIOID(Key).HasValue) AndAlso druginitsw(Key) = 2 Then
                            Dim val = 100 * (Math.E ^ AnalFunc2(key) / (1 + Math.E ^ AnalFunc2(key)))
                            CDRUGUSEPRESCRIPTOPIOID(key) = ApplyLimits(val, 0, 5.5)
                        ElseIf (Not CDRUGUSEPRESCRIPTOPIOID(Key).HasValue) AndAlso druginitsw(Key) = 3 Then
                            CDRUGUSEPRESCRIPTOPIOID(key) = PresOp3(key)
                        End If

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CDRUGUSEPRESCRIPTOPIOID.Add(item.Key, item.Value)
                        If (Not CDRUGUSEPRESCRIPTOPIOID(item.Key).HasValue) AndAlso druginitsw(item.Key) = 1 Then
                            Dim val = CDRUGUSEOPIATES(item.Key) * Math.E ^ AnalFunc(item.Key)
                            CDRUGUSEPRESCRIPTOPIOID(item.Key) = ApplyLimits(val, 0, 5.5)
                        ElseIf (Not CDRUGUSEPRESCRIPTOPIOID(item.Key).HasValue) AndAlso druginitsw(item.Key) = 2 Then
                            Dim val = 100 * (Math.E ^ AnalFunc2(item.Key) / (1 + Math.E ^ AnalFunc2(item.Key)))
                            CDRUGUSEPRESCRIPTOPIOID(item.Key) = ApplyLimits(val, 0, 5.5)
                        ElseIf (Not CDRUGUSEPRESCRIPTOPIOID(item.Key).HasValue) AndAlso druginitsw(item.Key) = 3 Then
                            CDRUGUSEPRESCRIPTOPIOID(item.Key) = PresOp3(item.Key)
                        End If
                    Next

                End If


                '-------- violence ----
                Slice = "SeriesViolenceSelfHarm"

                'JAM 2019-06-29 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CSVDTHSSELFHARM.Add(item.Key, If(item.Value, 0))
                'Next

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs27 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim ViolenceSelfHarmTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs27)
                    For Each key In cntx.Regionalization.Keys
                        Valhold = getClosestYrVal(ViolenceSelfHarmTbl, yrs27, key, cntx.FirstYear, HistDist)
                        CSVDTHSSELFHARM.Add(key, Valhold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *******
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CSVDTHSSELFHARM.Add(item.Key, If(item.Value, 0))
                    Next
                End If

                Slice = "SeriesViolencePoliticalPoliceExecution"
                'JAM 2019-06-29 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CSVDTHSPOLICS.Add(item.Key, If(item.Value, 0))
                'Next

                If cntx.HistoricMode Then

                    'JAM 2019-06-08 Commented due mti improved
                    'For Each item In (From s In cntx.GetSlice("SeriesEnvSolidFuelsImproved", "Earliest")
                    '                  Select Key = s.Key, Value = If(s.Value, 0))
                    '    CCOOKSTOVES.Add(item.Key, item.Value)
                    'Next


                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs28 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim ViolencePoliticalPoliceExecutionTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs28)
                    For Each key In cntx.Regionalization.Keys
                        Valhold = getClosestYrVal(ViolencePoliticalPoliceExecutionTbl, yrs28, key, cntx.FirstYear, HistDist)
                        CSVDTHSPOLICS.Add(key, Valhold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *******
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CSVDTHSPOLICS.Add(item.Key, If(item.Value, 0))
                    Next
                End If



                Slice = "SeriesViolenceInterpersonWomenChild"
                'JAM 2019-06-29 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CSVDTHSWOMENCHILDREN.Add(item.Key, If(item.Value, 0))
                'Next

                If cntx.HistoricMode Then



                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs29 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim ViolenceInterpersonWomenChildTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs29)
                    For Each key In cntx.Regionalization.Keys
                        Valhold = getClosestYrVal(ViolenceInterpersonWomenChildTbl, yrs29, key, cntx.FirstYear, HistDist)
                        CSVDTHSWOMENCHILDREN.Add(key, Valhold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *******
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CSVDTHSWOMENCHILDREN.Add(item.Key, If(item.Value, 0))
                    Next
                End If

                Slice = "SeriesViolenceInterpersonOther"
                'JAM 2019-06-29 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CSVDTHSOTHERINTERPERSON.Add(item.Key, If(item.Value, 0))
                'Next

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs30 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim ViolenceInterpersonOtherTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs30)
                    For Each key In cntx.Regionalization.Keys
                        Valhold = getClosestYrVal(ViolenceInterpersonOtherTbl, yrs30, key, cntx.FirstYear, HistDist)
                        CSVDTHSOTHERINTERPERSON.Add(key, Valhold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *******
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CSVDTHSOTHERINTERPERSON.Add(item.Key, If(item.Value, 0))
                    Next
                End If

                Slice = "SeriesViolenceInterpersonWomenAdult"
                'JAM 2019-06-30 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CSVDTHSWOMEN.Add(item.Key, If(item.Value, 0))
                'Next


                If cntx.HistoricMode Then


                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs31 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim ViolenceInterpersonWomenAdultTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs31)
                    For Each key In cntx.Regionalization.Keys
                        Valhold = getClosestYrVal(ViolenceInterpersonWomenAdultTbl, yrs31, key, cntx.FirstYear, HistDist)
                        CSVDTHSWOMEN.Add(key, Valhold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *******
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CSVDTHSWOMEN.Add(item.Key, If(item.Value, 0))
                    Next

                End If

                Slice = "SeriesViolenceInterpersonChild"
                'JAM 2019-06-30 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CSVDTHSCHIL.Add(item.Key, If(item.Value, 0))
                'Next

                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs32 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim ViolenceInterpersonChildTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs32)
                    For Each key In cntx.Regionalization.Keys
                        Valhold = getClosestYrVal(ViolenceInterpersonChildTbl, yrs32, key, cntx.FirstYear, HistDist)
                        CSVDTHSCHIL.Add(key, Valhold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *******
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CSVDTHSCHIL.Add(item.Key, If(item.Value, 0))
                    Next
                End If



                Slice = "SeriesSocietalViolenceDeathsTotal"
                'JAM 2019 - 6 - 30 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CSOCIETALVIOLENCEDEATHS.Add(item.Key, If(item.Value, 0))
                '    CSOCIETALVIOLENCEMEASURE.Add(item.Key, If(item.Value, 0)) 'Placeholder for total societal violence measure
                'Next

                If cntx.HistoricMode Then


                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs33 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim SocietalViolenceDeathsTotalTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs33)
                    For Each key In cntx.Regionalization.Keys
                        Valhold = getClosestYrVal(SocietalViolenceDeathsTotalTbl, yrs33, key, cntx.FirstYear, HistDist)
                        CSOCIETALVIOLENCEDEATHS.Add(key, If(Valhold, 0))
                        CSOCIETALVIOLENCEMEASURE.Add(key, If(Valhold, 0)) 'Placeholder for total societal violence measure
                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *******
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CSOCIETALVIOLENCEDEATHS.Add(item.Key, If(item.Value, 0))
                        CSOCIETALVIOLENCEMEASURE.Add(item.Key, If(item.Value, 0)) 'Placeholder for total societal violence measure
                    Next
                End If


                '---Conflict
                If cntx.HistoricMode Then
                    'JAM 2019-06-30 Commented due mti improved
                    'For Each item In (From s In cntx.GetSlice("SeriesViolencePoliticalConflictTerror", GetField)
                    '                  Select Key = s.Key, Value = s.Value)
                    '    CSVDTHSCONFLICT.Add(item.Key, item.Value)
                    'Next


                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs34 As Short() = cntx.GetYears("SeriesViolencePoliticalConflictTerror").OrderBy(Function(y) y).ToArray()
                    Dim ViolencePoliticalConflictTerrorTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesViolencePoliticalConflictTerror", yrs34)
                    For Each key In cntx.Regionalization.Keys
                        Valhold = getClosestYrVal(ViolencePoliticalConflictTerrorTbl, yrs34, key, cntx.FirstYear, HistDist)
                        CSVDTHSCONFLICT.Add(key, Valhold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else
                    Dim yearsCONFLICT As Short()
                    yearsCONFLICT = (From y In cntx.GetYears("SeriesViolencePoliticalConflictTerror")
                                     Where y >= 2005 AndAlso y <= 2015
                                     Order By y
                    ).ToArray
                    Dim CONFLICTTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesViolencePoliticalConflictTerror", yearsCONFLICT)
                    For Each item In (From s In cntx.GetSlice("SeriesViolencePoliticalConflictTerror", "MostRecent")
                                      Select Key = s.Key, Value = s.Value)
                        Dim yridx = getYearIdx(yearsCONFLICT, 2015)
                        Dim Val2015 As Single = AMAX(CONFLICTTbl(item.Key)(getYearIdx(yearsCONFLICT, 2015)), 0)
                        Dim Val2014 As Single = AMAX(CONFLICTTbl(item.Key)(getYearIdx(yearsCONFLICT, 2014)), 0)
                        Dim Val2013 As Single = AMAX(CONFLICTTbl(item.Key)(getYearIdx(yearsCONFLICT, 2013)), 0)
                        Dim Val2012 As Single = AMAX(CONFLICTTbl(item.Key)(getYearIdx(yearsCONFLICT, 2012)), 0)
                        Dim Val2011 As Single = AMAX(CONFLICTTbl(item.Key)(getYearIdx(yearsCONFLICT, 2011)), 0)
                        Dim Val2010 As Single = AMAX(CONFLICTTbl(item.Key)(getYearIdx(yearsCONFLICT, 2010)), 0)
                        Dim Val2009 As Single = AMAX(CONFLICTTbl(item.Key)(getYearIdx(yearsCONFLICT, 2009)), 0)
                        Dim Val2008 As Single = AMAX(CONFLICTTbl(item.Key)(getYearIdx(yearsCONFLICT, 2008)), 0)
                        Dim Val2007 As Single = AMAX(CONFLICTTbl(item.Key)(getYearIdx(yearsCONFLICT, 2007)), 0)
                        Dim Val2006 As Single = AMAX(CONFLICTTbl(item.Key)(getYearIdx(yearsCONFLICT, 2006)), 0)
                        Dim Val2005 As Single = AMAX(CONFLICTTbl(item.Key)(getYearIdx(yearsCONFLICT, 2005)), 0)
                        Dim myval As Single = ((Val2015 * 10) + (Val2014 * 9) + (Val2013 * 8) + (Val2012 * 7) + (Val2011 * 6) + (Val2010 * 5) + (Val2009 * 4) + (Val2008 * 3) + (Val2007 * 2) + (Val2006 * 1) + (Val2005 * 1)) / (1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10)
                        CSVDTHSCONFLICT.Add(item.Key, Amin(myval / 2, item.Value))
                        'If IsNull(CSVDTHSCONFLICT(ICount%)) Then
                        '    CSVDTHSCONFLICT(ICount%) = 0
                        'End If
                    Next

                End If

                '   Conventional and Nuclear Power
                Dim cpowldcf As Single = 0
                Using db As IFsContext = New IFsContext()
                    Dim gp As GlobalParameters = db.GlobalParameters.Where(Function(g) g.ParameterName = "CPOWLDCF").Single()
                    cpowldcf = CSng(gp.Value)
                End Using
                Slice = "SeriesNPOW"

                'JAM 2019-06-30 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    Dim PerCap = CGDP(item.Key) / CPOP(item.Key)
                '    CCPowF.Add(item.Key, cpowldcf - (cpowldcf - 1) * Amin(10, PerCap) / 10)
                '    CNPow.Add(item.Key, item.Value)
                '    If Not CNPow(item.Key).HasValue Then
                '        CNPow(item.Key) = 0
                '    End If
                '    Dim Val = CNPow(item.Key) * 0.002  'Each warhead costs $.002 billion
                '    'Note:  This is inconsistent with NPOWF specification (10 warheads/billion)
                '    'and results in sharp decline in arsenals; result right, but wrong reasons
                '    'ttt Not Known Functions
                '    Dim TotMil = 0 'temp, -> CGCon(item.Key) * GExpDef(item.Key) / 100
                '    Val = Val / TotMil
                '    If Val > 0.15 Then Val = 0.15
                '    CNMilF.Add(item.Key, Val)
                'Next


                If cntx.HistoricMode Then

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs35 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim NPOWTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs35)
                    For Each key In cntx.Regionalization.Keys
                        Valhold = getClosestYrVal(NPOWTbl, yrs35, key, cntx.FirstYear, HistDist)

                        Dim PerCap = CGDP(key) / CPOP(key)
                        CCPowF.Add(key, cpowldcf - (cpowldcf - 1) * Amin(10, PerCap) / 10)
                        CNPow.Add(key, Valhold)
                        If Not CNPow(key).HasValue Then
                            CNPow(key) = 0
                        End If
                        Dim Val = CNPow(key) * 0.002  'Each warhead costs $.002 billion
                        'Note:  This is inconsistent with NPOWF specification (10 warheads/billion)
                        'and results in sharp decline in arsenals; result right, but wrong reasons
                        Dim TotMil = CGCon(key) * GExpDef(key) / 100
                        Val = Val / TotMil
                        If Val > 0.15 Then Val = 0.15
                        CNMilF.Add(key, Val)


                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        Dim PerCap = CGDP(item.Key) / CPOP(item.Key)
                        CCPowF.Add(item.Key, cpowldcf - (cpowldcf - 1) * Amin(10, PerCap) / 10)
                        CNPow.Add(item.Key, item.Value)
                        If Not CNPow(item.Key).HasValue Then
                            CNPow(item.Key) = 0
                        End If
                        Dim Val = CNPow(item.Key) * 0.002  'Each warhead costs $.002 billion
                        'Note:  This is inconsistent with NPOWF specification (10 warheads/billion)
                        'and results in sharp decline in arsenals; result right, but wrong reasons
                        Dim TotMil = CGCon(item.Key) * GExpDef(item.Key) / 100
                        Val = Val / TotMil
                        If Val > 0.15 Then Val = 0.15
                        CNMilF.Add(item.Key, Val)
                    Next
                End If


                Slice = "SeriesEUMembership"

                'JAM 2019-06-30 Commented due mti improved
                'For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                '                  Select Key = s.Key, Value = s.Value)
                '    CEUMembSw.Add(item.Key, item.Value)
                'Next

                If cntx.HistoricMode Then


                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs36 As Short() = cntx.GetYears(Slice).OrderBy(Function(y) y).ToArray()
                    Dim EUMembershipTbl As IDictionary(Of String, Single?()) = cntx.GetDice(Slice, yrs36)
                    For Each key In cntx.Regionalization.Keys
                        Valhold = getClosestYrVal(EUMembershipTbl, yrs36, key, cntx.FirstYear, HistDist)
                        CEUMembSw.Add(key, Valhold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                      Select Key = s.Key, Value = s.Value)
                        CEUMembSw.Add(item.Key, item.Value)
                    Next
                End If

                'jrs 2025/08/28
                Slice = "SeriesDiploCBIISPRDSecCount"
                For Each item In (From s In If(IsShort(GetField, cntx.BaseYear, coal), cntx.GetSlice(Slice, CShort(GetField), coal), cntx.GetSlice(Slice, GetField))
                                  Select Key = s.Key, Value = s.Value)
                    CPRDSEC.Add(item.Key, item.Value)
                Next

                'Slice = "SeriesLeaderTravelBoth"
                'For Each item In (From s In cntx.GetSlice(Slice, cntx.FirstYear, CoalescePolicy.Null)
                'Select Case Key = s.Key, Value = s.Value)
                'CLDRTRIP.Add(item.Key, If(item.Value, -9999))
                'Next

                Dim yrsLDRTRIP As Short() = cntx.GetYears("SeriesLeaderTravelBoth").OrderBy(Function(y) y).ToArray()
                Dim LDRTRIPTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesLeaderTravelBoth", yrsLDRTRIP)

                CreateLDRTRIPMAInital(cntx, yrsLDRTRIP, LDRTRIPTbl)

                cntx.Store("CGovCorrupt", CGovCorrupt)

                Me.processed = True
                '============================================
                'CGDPPCP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")

                'Me.CSANITATION1 = New Dictionary(Of String, Single?)
            End Sub

            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Me.PreProcess(cntx)
                Dim series As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim weightSeries As IDictionary(Of String, Single?)
                weightSeries = CPOP
                Dim MinVal As Single, MaxVal As Single
                Dim bLimits As Boolean = False
                Dim bSum As Boolean = False
                ' Dim Vals As IDictionary(Of Integer, Single?)
                Select Case seriesName
                    Case "FREEDOM"
                        bLimits = True
                        MinVal = 2
                        MaxVal = 14
                        series = Me.CFreedom
                        weightSeries = CPOP
                    Case "ECONFREE"
                        bLimits = True
                        MinVal = 2
                        MaxVal = 14
                        series = Me.CEconFree
                        weightSeries = CPOP
                    Case "GOVEFFECT"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 5
                        series = Me.CGovEffect
                        weightSeries = CPOP
                    Case "GOVCORRUPT"
                        bLimits = True
                        MinVal = 1
                        MaxVal = 10
                        series = Me.CGovCorrupt
                        weightSeries = CPOP
                    Case "GOVBUSCOST"
                        bLimits = True
                        MinVal = 1
                        MaxVal = 300
                        series = Me.CGovBusCost
                        weightSeries = CPOP
                    Case "GOVBUSREGIND"
                        bLimits = True
                        MinVal = 0.01
                        MaxVal = 10
                        series = Me.CGovBusRegInd
                        weightSeries = CPOP
                    Case "GOVREGQUAL"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 5
                        series = Me.CGovRegQual
                        weightSeries = CPOP
                    Case "DEMOCPOLITY"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 20
                        series = Me.CDemocPolity
                        weightSeries = CPOP
                    Case "DEMOCPOLYARCHY" 'jrs 2023/10/13
                        bLimits = False
                        MinVal = 0
                        MaxVal = 1
                        series = Me.CDemocPolyarchy
                        weightSeries = CPOP
                    Case "WOMENINLEGISLATURE" 'clp 2026/02/26
                        bLimits = False
                        MinVal = 0
                        MaxVal = 100
                        series = Me.CWOMENINLEGISLATURE
                        weightSeries = CPOP
                    Case "WOMENINJOURNALISM" 'clp 2026/02/26
                        bLimits = False
                        MinVal = 0
                        MaxVal = 100
                        series = Me.CWOMENINJOURNALISM
                        weightSeries = CPOP
                    Case "WOMENPROPRIGHTS" 'clp 2026/02/26
                        bLimits = False
                        MinVal = -3.5
                        MaxVal = 3.5
                        series = Me.CWOMENPROPRIGHTS
                        weightSeries = CPOP
                    Case "Theta1" 'clp 2026/02/26
                        bLimits = False
                        MinVal = -3.5
                        MaxVal = 3.5
                        series = Me.CTHETA1
                        weightSeries = CPOP
                    Case "Theta2" 'clp 2026/02/26
                        bLimits = False
                        MinVal = -3.5
                        MaxVal = 3.5
                        series = Me.CTHETA2
                        weightSeries = CPOP
                    Case "Theta3" 'clp 2026/02/26
                        bLimits = False
                        MinVal = -3.5
                        MaxVal = 3.5
                        series = Me.CTHETA3
                        weightSeries = CPOP
                    Case "ethnic1"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = Me.CEthnic1
                        weightSeries = CPOP
                    Case "ethnic2"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = Me.CEthnic2
                        weightSeries = CPOP
                    Case "NUMNWP"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 10000
                        series = Me.CNumNWP
                        weightSeries = CPOP
                    Case "NUMNWPGR"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000
                        series = Me.CNumNWPGR
                        weightSeries = CPOP
                    Case "GEM"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1
                        series = Me.CGEM
                        weightSeries = CPOP
                    Case "GII"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1
                        series = Me.CGII
                        weightSeries = CPOP
                    Case "DRUGUSEAMPHETAMINE"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = Me.CDRUGUSEAMPHETAMINE
                        weightSeries = CPOP
                    Case "DRUGUSECOCAINE"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = Me.CDRUGUSECOCAINE
                        weightSeries = CPOP
                    Case "DRUGUSEOPIATES"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = Me.CDRUGUSEOPIATES
                        weightSeries = CPOP
                    Case "DRUGUSEPRESCRIPTOPIOID"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = Me.CDRUGUSEPRESCRIPTOPIOID
                        weightSeries = CPOP
                    Case "SVDTHSSELFHARM"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000
                        series = Me.CSVDTHSSELFHARM
                        weightSeries = CPOP
                    Case "SVDTHSCONFLICT"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000
                        series = Me.CSVDTHSCONFLICT
                        weightSeries = CPOP
                    Case "SVDTHSPOLICS"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000
                        series = Me.CSVDTHSPOLICS
                        weightSeries = CPOP
                    Case "SVDTHSWOMENCHILDREN"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000
                        series = Me.CSVDTHSWOMENCHILDREN
                        weightSeries = CPOP
                    Case "SVDTHSOTHERINTERPERSON"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000
                        series = Me.CSVDTHSOTHERINTERPERSON
                        weightSeries = CPOP
                    Case "SOCIETALVIOLENCEDEATHS"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000
                        series = Me.CSOCIETALVIOLENCEDEATHS
                        weightSeries = CPOP
                    Case "SOCIETALVIOLENCEMEASURE"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000
                        series = Me.CSOCIETALVIOLENCEMEASURE
                        weightSeries = CPOP
                    Case "SVDTHSWOMEN"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000
                        series = Me.CSVDTHSWOMEN
                        weightSeries = CPOP
                    Case "SVDTHSCHIL"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000
                        series = Me.CSVDTHSCHIL
                        weightSeries = CPOP
                    Case "eumembsw"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1
                        series = Me.CEUMembSw
                        weightSeries = CPOP
                    Case "cpowf"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 10
                        series = Me.CCPowF
                        weightSeries = CGDP
                    Case "nmilf"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1
                        series = Me.CNMilF
                        weightSeries = CGDP
                    Case "NPOW"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000
                        series = Me.CNPow
                        bSum = True
                    Case "PRDSEC" 'jrs 2025/08/28
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000
                        series = Me.CPRDSEC
                        bSum = True

                    Case "LDRTRIP" 'clp 2025/10/24
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000
                        series = Me.CLDRTRIP
                        bSum = True

                    Case "GOVEMBED"
                        series = Me.CEmbeddednessGR
                        Dim Vals = (From s In series
                                    Join r In cntx.Regionalization On s.Key Equals r.Key
                                    Join w In weightSeries On s.Key Equals w.Key
                                    Group Value = s.Value, Weight = w.Value
                                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                             ).ToDictionary(
                             Function(grp) grp.RegionId,
                             Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)
                        BasePutAnnualGrowthOneDim("GOVEMBEDGR", Vals, cntx)
                        series = Me.CEmbeddedness
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
            Friend Shared Function IsShort(ByVal expression As String, ByVal byr As Short, ByRef coal As CoalescePolicy) As Boolean
                If expression Is Nothing Then
                    Return False
                End If

                Dim testShort As Short
                If Short.TryParse(expression.ToString(), testShort) Then
                    If testShort = byr Then
                        coal = CoalescePolicy.MostRecent
                    Else
                        coal = CoalescePolicy.Null
                    End If
                    Return True
                End If
                Return False
            End Function

            Private Sub CreateLDRTRIPMAInital(cntx As IRebuildFacade, yrsLDRTRIP As Short(), LDRTRIPTbl As IDictionary(Of String, Single?()))
                ' clp 2025/12/12 - Create initial moving average for leader trips
                'Lines of code for reference
                '
                'Private Sub FillInitialGDPGRNIDM(cntx As IRebuildFacade, yrsGDP As Short(), GDPTbl As IDictionary(Of String, Single?())
                '
                'Dim yrsGDPTbl As Short() = cntx.GetYears("GDP" & ConstantDollarsYear.ToString).OrderBy(Function(y) y).ToArray()
                'Dim GDPTbl As IDictionary(Of String, Single?()) = cntx.GetDice("GDP" & ConstantDollarsYear.ToString, yrsGDPTbl)
                '
                ' CIGDPR.Add(key, ValHold)

                ' *** START NEW CODE ***
                ' Declare internal vars
                Dim syr As Single ' start year
                Dim eyr As Single ' end year
                Dim ind2020 As Boolean
                Dim ind2021 As Boolean
                Dim idx2020 As Single
                Dim idx2021 As Single
                Dim vals(9) As Single
                Dim valCnt As Single
                Dim sum As Single


                ' Set inital values
                'tmpbool = cntx.HistoricMode 'was throwing an unexpected error
                syr = CSng(cntx.BaseYear - 10) 'get first year for average
                eyr = CSng(cntx.BaseYear) ' get base year for end of average
                ind2020 = False
                ind2021 = False
                idx2020 = getYearIdx(yrsLDRTRIP, 2020)
                idx2021 = getYearIdx(yrsLDRTRIP, 2021)
                valCnt = 10
                syr = getYearIdx(yrsLDRTRIP, syr) ' switch from years to index values
                eyr = getYearIdx(yrsLDRTRIP, eyr) ' switch from years to index values

                'find last available year
                Dim iLastyrsLDRTRIP As Integer
                iLastyrsLDRTRIP = yrsLDRTRIP(yrsLDRTRIP.Count - 1)
                Dim iLastyrsLDRTRIPnoLimit As Integer
                iLastyrsLDRTRIPnoLimit = iLastyrsLDRTRIP

                ' check base year is present
                If iLastyrsLDRTRIPnoLimit < cntx.BaseYear And cntx.BaseYear > 1989 Then
                    Debug.Print("The leader trips are missing values from " & CStr(cntx.BaseYear) & " and cannot proceed.")
                    Exit Sub
                End If

                'identify if exclusion is needed
                If eyr >= 2021 And syr <= 2021 Then
                    ind2021 = True
                End If

                If eyr >= 2020 And syr <= 2020 Then
                    ind2020 = True
                End If

                ' calculate values
                For Each key In cntx.Regionalization.Keys
                    If cntx.BaseYear < 1999 Then
                        If cntx.BaseYear > 1989 Then
                            sum = LDRTRIPTbl(key)(CInt(getYearIdx(yrsLDRTRIP, cntx.BaseYear)))
                            CLDRTRIP.Add(key, sum)
                        Else
                            CLDRTRIP.Add(key, 0)
                        End If
                    Else
                            sum = 0 'set sum to zero, cya
                        valCnt = 10
                        If cntx.HistoricMode Then
                            'if historical mode do nothing but fill with zeros
                            CLDRTRIP.Add(key, 0)
                        Else
                            'fill array
                            For y = 0 To 9 ' indexing starts from 0
                                If CInt(syr + y + 1) <> idx2020 AndAlso CInt(syr + y + 1) <> idx2021 Then
                                    vals(y) = LDRTRIPTbl(key)(CInt(syr + y + 1))
                                Else
                                    vals(y) = 0
                                    valCnt = valCnt - 1
                                End If
                            Next y
                            'sum array and collapse to single value
                            For y = 0 To 9
                                sum = sum + vals(y)
                            Next
                            sum = sum / valCnt
                            'If key = "Afghanistan" Then Stop
                            CLDRTRIP.Add(key, sum)
                        End If
                    End If
                Next
            End Sub
        End Class

    End Module
End Namespace