Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.IO
Imports System.Linq
Imports IFs.Data
Imports Microsoft.EntityFrameworkCore.Metadata.Internal
'Imports Microsoft.EntityFrameworkCore.Metadata.Internal

Namespace Models
    Partial Public Module Energy

        <RebuildsVariable("GDPRPA", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("ENM", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("ENX", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("RESER", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("ENPRR", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("ENP", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("resor", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("resoruncon", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("enpoilmax", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("enpgasmax", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("enpcoalmax", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("enresunce", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("QE", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("QEUNCON", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("prodtf", AggregatorType:=GetType(ENAggregator))>
        <RebuildsVariable("rdm", AggregatorType:=GetType(ENAggregator))>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        <ImportsAsset("CGDP")>
        <ImportsAsset("CLandArea")>
        <ImportsAsset("CGDPPCPLT50")>
        Public Iterator Function RebuildEnergy(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Dim TbName As String
            Dim GDPPCPLT50 As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCPLT50")
            Dim CGDPPCP As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")

            'jrs 2024/06/12 TbName$ = "GDP/Capita (PPP) Versus Energy Demand per GDP Dollar PPP (" & CStr(context.FirstYear) & ") Logarithmic"
            TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Energy Demand per GDP Dollar PPP (" & CStr(context.FirstYear) & ") Logarithmic"
            TbName$ = getFunctionNameYrOrMstRct(TbName$, context.FirstYear, context, "(PPP " & ConstantDollarsYear & ")")
            If context.HistoricMode Then
                context.PutEstimator("ENM", context.GetRegression(TbName).ToEstimator(GDPPCPLT50))
                context.PutEstimator("ENM_2", context.GetRegression(TbName).ToEstimator(CGDPPCP))
            Else
                context.PutEstimator("ENM", context.GetRegression(TbName).ToEstimator(CGDPPCP))
            End If

            Yield context.Query("GDPRPA").Filter(BaseYearOnly).Execute()
            Yield context.Query("ENM").Filter(BaseYearOnly).Execute()
            Yield context.Query("ENX").Filter(BaseYearOnly).Execute()
            Yield context.Query("RESER").Filter(BaseYearOnly).Execute()
            Yield context.Query("ENPRR").Filter(BaseYearOnly).Execute()
            Yield context.Query("ENP").Filter(BaseYearOnly).Execute()

            'jrs 2019/11/26
            Dim dftmp As IDataframe
            dftmp = context.Query("resor").Filter(BaseYearOnly).Execute()
            Dim dfresor As Dataframe = Dataframe.Create("resor", context, context.FirstYear, context.LastYear, 0)
            Dim R, e As Short
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    'mti 2024/07/23 start
                    'For e = 1 To 6
                    '    dfresor(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), e, 6)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), e, 6))
                    'Next
                    For e = 1 To 9
                        dfresor(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), e, 9)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), e, 9))
                    Next
                    'mti 2024/07/23 end
                Next
            Next
            Yield dfresor

            dftmp = context.Query("resoruncon").Filter(BaseYearOnly).Execute()
            Dim dfresoruncon As Dataframe = Dataframe.Create("resoruncon", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    'mti 2024/07/23 start
                    'For e = 1 To 6
                    '    dfresoruncon(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), e, 6)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), e, 6))
                    'Next
                    For e = 1 To 9
                        dfresoruncon(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), e, 9)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), e, 9))
                    Next
                    'mti 2024/07/23 end
                Next
            Next
            Yield dfresoruncon

            dftmp = context.Query("enpoilmax").Filter(BaseYearOnly).Execute()
            Dim dfenpoilmax As Dataframe = Dataframe.Create("enpoilmax", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    dfenpoilmax(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR)))
                Next
            Next
            Yield dfenpoilmax

            dftmp = context.Query("enpgasmax").Filter(BaseYearOnly).Execute()
            Dim dfenpgasmax As Dataframe = Dataframe.Create("enpgasmax", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    dfenpgasmax(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR)))
                Next
            Next
            Yield dfenpgasmax
            dftmp = context.Query("enpcoalmax").Filter(BaseYearOnly).Execute()
            Dim dfenpcoalmax As Dataframe = Dataframe.Create("enpcoalmax", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    dfenpcoalmax(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR)))
                Next
            Next
            Yield dfenpcoalmax
            dftmp = context.Query("enresunce").Filter(BaseYearOnly).Execute()
            Dim dfenresunce As Dataframe = Dataframe.Create("enresunce", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    dfenresunce(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR)))
                Next
            Next
            Yield dfenresunce

            Yield context.Query("QE").Filter(BaseYearOnly).Execute()
            Yield context.Query("QEUNCON").Filter(BaseYearOnly).Execute()
            dftmp = context.Query("prodtf").Filter(BaseYearOnly).Execute()
            Dim dfprodtf As Dataframe = Dataframe.Create("prodtf", context, context.FirstYear, context.LastYear, 0)
            Dim Dim2 As Short
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    'mti 2024/07/23 start
                    'For Dim2 = 1 To 6
                    '    dfprodtf(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), Dim2, 6)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), Dim2, 6))
                    'Next
                    For Dim2 = 1 To 9
                        dfprodtf(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), Dim2, 9)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), Dim2, 9))
                    Next
                    'mti 2024/07/23 end
                Next
            Next
            Yield dfprodtf
            dftmp = context.Query("rdm").Filter(BaseYearOnly).Execute()
            Dim dfrdm As Dataframe = Dataframe.Create("rdm", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    'mti 2024/07/23 start
                    'For Dim2 = 1 To 6
                    '    dfrdm(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), Dim2, 6)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), Dim2, 6))
                    'Next
                    For Dim2 = 1 To 9
                        dfrdm(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), Dim2, 9)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), Dim2, 9))
                    Next
                    'mti 2024/07/23 end
                Next
            Next
            Yield dfrdm

        End Function

        Class ENAggregator
            Inherits DefaultSumAggregator

            Private Property CGDPPCP As IDictionary(Of String, Single?)
            Private Property CPOP As IDictionary(Of String, Single?)
            Private Property CGDP As IDictionary(Of String, Single?)
            Private Property CLANDAREA As IDictionary(Of String, Single?)

            Private Property cachedGDPRPA As IDictionary(Of String, Single?)
            Private Property cachedENM As IDictionary(Of String, Single?)
            Private Property cachedENX As IDictionary(Of String, Single?)
            Private Property cachedENPOil As IDictionary(Of String, Single?)
            Private Property cachedENPGas As IDictionary(Of String, Single?)
            Private Property cachedENPCoal As IDictionary(Of String, Single?)
            Private Property cachedENPELEC As IDictionary(Of String, Single?)
            Private Property cachedENPHYD As IDictionary(Of String, Single?)
            Private Property cachedENPNUC As IDictionary(Of String, Single?)
            Private Property cachedENPGEO As IDictionary(Of String, Single?)
            Private Property cachedENPRENEW As IDictionary(Of String, Single?)

            'mti 2024/07/23 start
            Private Property cachedENPSOLAR As IDictionary(Of String, Single?)
            Private Property cachedENPWIND As IDictionary(Of String, Single?)
            Private Property cachedENPGEOTHERMAL As IDictionary(Of String, Single?)
            'mti 2024/07/23 End

            Private Property cachedENPOTHREN As IDictionary(Of String, Single?)

            Private Property cachedENPTOT As IDictionary(Of String, Single?)

            Private Property cachedRESOIL As IDictionary(Of String, Single?)
            Private Property cachedRESGAS As IDictionary(Of String, Single?)
            Private Property cachedRESCOAL As IDictionary(Of String, Single?)
            Private Property cachedRESHYD As IDictionary(Of String, Single?)

            'mti 2024/08/29 start
            'Private Property cachedRESSOLAR As IDictionary(Of String, Single?)
            'Private Property cachedRESWIND As IDictionary(Of String, Single?)
            'Private Property cachedRESGEO As IDictionary(Of String, Single?)
            'mti 2024/08/29 End

            'mti 2024/07/23 start
            Private Property cachedENPRRSOLAR As IDictionary(Of String, Single?)
            Private Property cachedENPRRWIND As IDictionary(Of String, Single?)
            Private Property cachedENPRRGEOTHERMAL As IDictionary(Of String, Single?)
            'mti 2024/07/23 End

            Private Property cachedENPRRRen As IDictionary(Of String, Single?)


            Private Property cachedENPRROil As IDictionary(Of String, Single?)
            Private Property cachedENPRRGas As IDictionary(Of String, Single?)
            Private Property cachedENPRRHyd As IDictionary(Of String, Single?)
            Private Property cachedENPRRCoal As IDictionary(Of String, Single?)
            Private Property cachedENPRRNuc As IDictionary(Of String, Single?)



            Private Property cachedRESOROIL As IDictionary(Of String, Single?)
            Private Property cachedRESOROILUN As IDictionary(Of String, Single?)
            Private Property cachedRESORGAS As IDictionary(Of String, Single?)
            Private Property cachedRESORGASUN As IDictionary(Of String, Single?)
            Private Property cachedRESORCOAL As IDictionary(Of String, Single?)

            'mti 2024/07/23 start
            Private Property cachedRESORSOLAR As IDictionary(Of String, Single?)
            Private Property cachedRESORWIND As IDictionary(Of String, Single?)
            Private Property cachedRESORGEOTHERMAL As IDictionary(Of String, Single?)
            'mti 2024/07/23 End
            Private Property ResorRenew As IDictionary(Of String, Single?)

            Private Property CENPOILMAX As IDictionary(Of String, Single?)
            Private Property CENPGASMAX As IDictionary(Of String, Single?)
            Private Property CENPCOALMAX As IDictionary(Of String, Single?)
            Private Property CENRESUNCE As IDictionary(Of String, Single?)

            Private Property QEHold As IDictionary(Of Integer, Single?)

            Private Property rdm_Oil As IDictionary(Of Integer, Single?)
            Private Property rdm_Gas As IDictionary(Of Integer, Single?)
            Private Property rdm_Coal As IDictionary(Of Integer, Single?)
            Private Property rdm_Hyd As IDictionary(Of Integer, Single?)

            Private Property processed As Boolean
            <ExportsAsset("ENPOIL")>
            <ExportsAsset("ENPGAS")>
            <ExportsAsset("ENPCOAL")>
            <ExportsAsset("ENPHyd")>
            <ExportsAsset("ENPNUC")>
            <ExportsAsset("ENPSOLAR")> 'mti 2024/07/23 
            <ExportsAsset("ENPWIND")> 'mti 2024/07/23 
            <ExportsAsset("ENPGeoThermal")> 'mti 2024/07/23 
            <ExportsAsset("ENPOthRen")>
            <ExportsAsset("CENM")>
            <ExportsAsset("CENX")>
            <ExportsAsset("AppConEn")>
            Private Sub PreProcess(cntx As IRebuildFacade)
                If Me.processed Then
                    Return
                End If

                Dim objWriter = New System.IO.StreamWriter(cntx.PutFdrvRun + "progress.txt", append:=True)
                objWriter.WriteLine("Processing Data on Energy - Please Wait")
                objWriter.Close()

                Me.CGDPPCP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
                Me.CPOP = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
                Me.CGDP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDP")
                Me.CLANDAREA = cntx.Fetch(Of IDictionary(Of String, Single?))("CLandArea")

                Me.cachedGDPRPA = New Dictionary(Of String, Single?)

                Me.cachedENM = New Dictionary(Of String, Single?)
                Me.cachedENX = New Dictionary(Of String, Single?)

                Me.cachedENPOil = New Dictionary(Of String, Single?)
                Me.cachedENPGas = New Dictionary(Of String, Single?)
                Me.cachedENPCoal = New Dictionary(Of String, Single?)
                Me.cachedENPELEC = New Dictionary(Of String, Single?)
                Me.cachedENPHYD = New Dictionary(Of String, Single?)
                Me.cachedENPNUC = New Dictionary(Of String, Single?)
                Me.cachedENPGEO = New Dictionary(Of String, Single?)
                Me.cachedENPRENEW = New Dictionary(Of String, Single?)

                'mti 2024/07/23 start
                Me.cachedENPSOLAR = New Dictionary(Of String, Single?)
                Me.cachedENPWIND = New Dictionary(Of String, Single?)
                Me.cachedENPGEOTHERMAL = New Dictionary(Of String, Single?)
                'mti 2024/07/23 End

                Me.cachedENPOTHREN = New Dictionary(Of String, Single?)
                Me.cachedENPTOT = New Dictionary(Of String, Single?)

                Me.cachedRESOIL = New Dictionary(Of String, Single?)
                Me.cachedRESGAS = New Dictionary(Of String, Single?)
                Me.cachedRESCOAL = New Dictionary(Of String, Single?)
                Me.cachedRESHYD = New Dictionary(Of String, Single?)

                'mti 2024/08/29 start
                'Me.cachedRESSOLAR = New Dictionary(Of String, Single?)
                'Me.cachedRESWIND = New Dictionary(Of String, Single?)
                'Me.cachedRESGEO = New Dictionary(Of String, Single?)
                'mti 2024/08/29 End

                Me.cachedENPRRRen = New Dictionary(Of String, Single?)

                'mti 2024/07/23 start
                Me.cachedENPRRSOLAR = New Dictionary(Of String, Single?)
                Me.cachedENPRRWIND = New Dictionary(Of String, Single?)
                Me.cachedENPRRGEOTHERMAL = New Dictionary(Of String, Single?)
                'mti 2024/07/23 End

                Me.cachedENPRROil = New Dictionary(Of String, Single?)
                Me.cachedENPRRGas = New Dictionary(Of String, Single?)
                Me.cachedENPRRHyd = New Dictionary(Of String, Single?)
                Me.cachedENPRRCoal = New Dictionary(Of String, Single?)
                Me.cachedENPRRNuc = New Dictionary(Of String, Single?)

                Me.cachedRESOROIL = New Dictionary(Of String, Single?)
                Me.cachedRESOROILUN = New Dictionary(Of String, Single?)
                Me.cachedRESORGAS = New Dictionary(Of String, Single?)
                Me.cachedRESORGASUN = New Dictionary(Of String, Single?)
                Me.cachedRESORCOAL = New Dictionary(Of String, Single?)

                'mti 2024/07/23 start
                Me.cachedRESORSOLAR = New Dictionary(Of String, Single?)
                Me.cachedRESORWIND = New Dictionary(Of String, Single?)
                Me.cachedRESORGEOTHERMAL = New Dictionary(Of String, Single?)
                'mti 2024/07/23 End

                Me.ResorRenew = New Dictionary(Of String, Single?)

                Me.CENPOILMAX = New Dictionary(Of String, Single?)
                Me.CENPGASMAX = New Dictionary(Of String, Single?)
                Me.CENPCOALMAX = New Dictionary(Of String, Single?)
                Me.CENRESUNCE = New Dictionary(Of String, Single?)

                Me.QEHold = New Dictionary(Of Integer, Single?)

                Me.rdm_Oil = New Dictionary(Of Integer, Single?)
                Me.rdm_Gas = New Dictionary(Of Integer, Single?)
                Me.rdm_Coal = New Dictionary(Of Integer, Single?)
                Me.rdm_Hyd = New Dictionary(Of Integer, Single?)

                '   Read Maximum Energy Proudction from IFs.mdb; also unconventional Economical
                '   ---------------------------------------------------------------------------
                Dim WorldEP As Single
                If cntx.HistoricMode Then
                    For Each key In cntx.Regionalization.Keys
                        CENPOILMAX.Add(key, 0)
                        CENPGASMAX.Add(key, 0)
                        CENPCOALMAX.Add(key, 0)
                        CENRESUNCE.Add(key, 0)
                    Next

                    Using db As IFsContext = New IFsContext()
                        'jrs 2022/05/02
                        'Read No Energy Production countries (only one for now)
                        sEnergyNoProductionCountry = ""
                        For Each ctry As EnergyNoProductionCountries In db.EnergyNoProductionCountries
                            sEnergyNoProductionCountry = ctry.CountryName
                            Exit For
                        Next

                        Dim gp As GlobalParametersHist = db.GlobalParametersHist.Where(Function(g) g.ParameterName = "WEPBYEAR").Single()
                        WorldEP = CSng(gp.Value)

                        For Each pos As GlobalParametersMultipleCols In db.GlobalParametersMultipleCols
                            If pos.QE > 0 Then
                                QEHold.Add(pos.Position, CSng(pos.QE))
                            End If
                        Next
                    End Using
                    'WorldEP = 9.0F
                Else
                    Using db As IFsContext = New IFsContext()
                        'jrs 2022/05/02
                        'Read No Energy Production countries (only one for now)
                        sEnergyNoProductionCountry = ""
                        For Each ctry As EnergyNoProductionCountries In db.EnergyNoProductionCountries
                            sEnergyNoProductionCountry = ctry.CountryName
                            Exit For
                        Next

                        Dim gp As GlobalParameters = db.GlobalParameters.Where(Function(g) g.ParameterName = "WEPBYEAR").Single()
                        WorldEP = CSng(gp.Value)

                        For Each ctry As CountryParameters In db.CountryParameters
                            CENPOILMAX.Add(ctry.Country, CSng(If(ctry.ENPOILMAX, 0)))
                            CENPGASMAX.Add(ctry.Country, CSng(If(ctry.ENPGASMAX, 0)))
                            CENPCOALMAX.Add(ctry.Country, CSng(If(ctry.ENPCOALMAX, 0)))
                            CENRESUNCE.Add(ctry.Country, CSng(If(ctry.ENRESUNCE, 0)))
                        Next

                        For Each pos As GlobalParametersMultipleCols In db.GlobalParametersMultipleCols
                            If pos.QE > 0 Then
                                QEHold.Add(pos.Position, CSng(pos.QE))
                            End If
                        Next
                    End Using
                    'WorldEP = 40.0F
                End If

                'GDPRPA
                If cntx.HistoricMode Then
                    For Each key In cntx.Regionalization.Keys
                        cachedGDPRPA.Add(key, CGDP(key))
                    Next
                Else
                    For Each item In (From s In cntx.GetSlice("GDPRPA", cntx.FirstYear, MyBase.Policy)
                                      Join x In CGDP On s.Key Equals x.Key
                                      Select Key = s.Key, Value = If(s.Value, x.Value)
                )
                        cachedGDPRPA.Add(item.Key, item.Value)
                    Next
                End If



                '   Read Production and Trade; Fix Nulls
                '   ------------------------------------
                Dim Estimate As SeriesEstimator = cntx.GetEstimator("ENM")
                Dim approxendem As Double?, LocalProdMin As Double?, LocalProdEst As Double?
                Dim ENPTempTotal As Double?, COil As Double?, CGas As Double?, CCoal As Double?, Chyd As Double?
                Dim CENM As Double?, CENX As Single?, VeryConsDemand As Single?, VeryConsImport As Double?
                Dim CRenew? As Double, COthRenew As Double?, Cnuc As Double?

                If cntx.HistoricMode Then
                    'Note:  Use of WDI/UN table instead of BP allows 1960 instead of 1965 start
                    '   But could be problematic - e.g. Algeria is very different; BP might be better

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriTables As New List(Of String)
                    Dim DictTbls As New List(Of IDictionary(Of String, Single?()))
                    Dim yrss As New List(Of Short())
                    SeriTables.Add("ENM") '0
                    SeriTables.Add("ENPOILBP")
                    SeriTables.Add("ENPOILIEA")
                    SeriTables.Add("ENPOIL") '3
                    SeriTables.Add("ENPGAS")
                    SeriTables.Add("ENPCOALBP") '5
                    SeriTables.Add("ENPCOALIEA")
                    SeriTables.Add("ENPCOAL")
                    'jrs 2024/06/04 SeriTables.Add("ENPELEC") '8
                    SeriTables.Add("ENPHYD") 'jrs 2024/06/04 new 8
                    SeriTables.Add("ENPNUC")
                    SeriTables.Add("ENPGEOIEA") 'jrs 2024/06/04 new 10 '11
                    'jrs 2024/06/04 SeriTables.Add("ENPGEO")
                    SeriTables.Add("ENX") 'jrs 2024/06/04 new 11

                    'mti 2024/07/23 start
                    SeriTables.Add("ENPPHOTOIEA")
                    SeriTables.Add("ENPSOLARIEA")
                    SeriTables.Add("ENPWINDIEA") '5
                    SeriTables.Add("ENPTIDEWAVEIEA")
                    SeriTables.Add("ENPBIODIESELIEA")
                    SeriTables.Add("ENPBIOGASIEA")
                    'mti 2024/07/23 end

                    Dim i As Integer
                    For i = 0 To SeriTables.Count - 1
                        yrss.Add(cntx.GetYears(SeriTables(i)).OrderBy(Function(y) y).ToArray())
                        DictTbls.Add(cntx.GetDice(SeriTables(i), yrss(i)))
                    Next
                    For Each key In cntx.Regionalization.Keys
                        Dim compendemperunit = AMAX(Estimate(key), 0.0001)
                        approxendem = compendemperunit * CGDPPCP(key) * CPOP(key)
                        LocalProdMin = approxendem * 0.15F
                        LocalProdEst = AMAX(LocalProdMin, CSng(approxendem - If(getClosestYrVal(DictTbls(0), yrss(0), key, cntx.FirstYear, HistDist), 0.0001F)))
                        '----
                        'SeriesTable = "SeriesEnProdOilBP"
                        COil = getClosestYrVal(DictTbls(1), yrss(1), key, cntx.FirstYear, HistDist)
                        If Not COil.HasValue Then
                            'SeriesTable = "SeriesEnProdOilIEA"
                            COil = getClosestYrVal(DictTbls(2), yrss(2), key, cntx.FirstYear, HistDist)
                        End If
                        If Not COil.HasValue Then
                            'SeriesTable = "SeriesEnProdOil"
                            COil = getClosestYrVal(DictTbls(3), yrss(3), key, cntx.FirstYear, HistDist)
                        End If

                        'Would prefer a minimum local production of .1 of demand, but
                        '   without elec/gas distribution in energy it sets up
                        '   inconsistency with IO matrix/economy
                        If COil.HasValue AndAlso COil < 0.000001 Then
                            COil = LocalProdEst * 0.1F
                        Else
                            COil = If(COil, LocalProdEst * 0.1F)
                        End If

                        'Set at 10% of approximate total energy demand
                        '            If COIL < 0.0005 Then COIL = 0.0005
                        'Removed 12/04 because too big for Sao Tome and Principe
                        'SeriesTable = "SeriesEnProdGas"
                        Dim iGas = getClosestYrVal(DictTbls(4), yrss(4), key, cntx.FirstYear, HistDist)
                        If iGas.HasValue AndAlso iGas < 0.000001 Then
                            CGas = LocalProdEst * 0.1F
                        Else
                            CGas = If(iGas, LocalProdEst * 0.1F)
                        End If
                        'SeriesTable = "SeriesEnProdCoalBP"
                        CCoal = getClosestYrVal(DictTbls(5), yrss(5), key, cntx.FirstYear, HistDist)
                        If Not CCoal.HasValue Then
                            'SeriesTable = "SeriesEnProdCoalIEA"
                            CCoal = getClosestYrVal(DictTbls(6), yrss(6), key, cntx.FirstYear, HistDist)
                        End If
                        If Not CCoal.HasValue Then
                            'SeriesTable = "SeriesEnProdCoal"
                            CCoal = getClosestYrVal(DictTbls(7), yrss(7), key, cntx.FirstYear, HistDist)
                        End If
                        If CCoal.HasValue AndAlso CCoal < 0.0000001 Then
                            CCoal = LocalProdEst * 0.2F
                        Else
                            CCoal = If(CCoal, LocalProdEst * 0.2F)
                        End If

                        'SeriesTable = "SeriesEnConElec"
                        'jrs 2024/06/04 Dim Elec = getClosestYrVal(DictTbls(8), yrss(8), key, cntx.FirstYear, HistDist)
                        'change following indices because we have removed this table

                        'SeriesTable = "SeriesEnProdHydroIEA"
                        Dim hyd = getClosestYrVal(DictTbls(8), yrss(8), key, cntx.FirstYear, HistDist)
                        If hyd.HasValue AndAlso hyd < LocalProdEst * 0.001 Then
                            Chyd = LocalProdEst * 0.001F
                        Else
                            Chyd = If(hyd, LocalProdEst * 0.01F)
                        End If
                        'SeriesTable = "SeriesEnProdNuclearIEA"
                        Dim Nuc = getClosestYrVal(DictTbls(9), yrss(9), key, cntx.FirstYear, HistDist)
                        If Nuc.HasValue AndAlso Nuc < 0.00001 Then
                            Me.cachedENPNUC.Add(key, 0.00001F)
                        Else
                            Me.cachedENPNUC.Add(key, If(Nuc, 0.00001F))
                        End If
                        'SeriesTable = "SeriesEnProdGeothermIEA"
                        Dim Geo = getClosestYrVal(DictTbls(10), yrss(10), key, cntx.FirstYear, HistDist)
                        'jrs 2024/06/04
                        'If Not Geo.HasValue Then
                        '    'SeriesTable = "SeriesEnProdGeoTherm"
                        '    Geo = getClosestYrVal(DictTbls(12), yrss(12), key, cntx.FirstYear, HistDist)
                        'End If

                        Me.cachedENPGEO.Add(key, If(Geo, 0.00001F))

                        'mti 2024/07/23 start
                        Me.cachedENPGEOTHERMAL.Add(key, If(Geo, 0.00001F))

                        Dim Photo = getClosestYrVal(DictTbls(12), yrss(12), key, cntx.FirstYear, HistDist)
                        Dim Solar = getClosestYrVal(DictTbls(13), yrss(13), key, cntx.FirstYear, HistDist)
                        Me.cachedENPSOLAR.Add(key, AMAX(If(Photo, 0.00000F) + If(Solar, 0.00000F), 0.000001F))

                        Dim Wind = getClosestYrVal(DictTbls(14), yrss(14), key, cntx.FirstYear, HistDist)
                        Me.cachedENPWIND.Add(key, If(Wind, 0.00001F))

                        Dim Tidal = getClosestYrVal(DictTbls(15), yrss(15), key, cntx.FirstYear, HistDist)
                        Dim BioDiesel = getClosestYrVal(DictTbls(16), yrss(16), key, cntx.FirstYear, HistDist)
                        Dim BioGas = getClosestYrVal(DictTbls(17), yrss(17), key, cntx.FirstYear, HistDist)
                        Me.cachedENPOTHREN.Add(key, AMAX(If(Tidal, 0.00000F) + If(BioDiesel, 0.00000F) + If(BioGas, 0.00000F), 0.000001F))


                        'ENPTempTotal = COil + CGas + CCoal +
                        'Chyd + Me.cachedENPNUC(key) + Me.cachedENPGEO(key)

                        ENPTempTotal = COil + CGas + CCoal +
                        Chyd + Me.cachedENPNUC(key) + +Me.cachedENPSOLAR(key) + +Me.cachedENPWIND(key) + +Me.cachedENPGEOTHERMAL(key) + Me.cachedENPOTHREN(key)
                        'mti 2024/07/23 end

                        If ENPTempTotal < LocalProdMin Then
                            COil = COil * LocalProdMin / ENPTempTotal
                            CGas = CGas * LocalProdMin / ENPTempTotal
                            CCoal = CCoal * LocalProdMin / ENPTempTotal
                            Chyd = Chyd * LocalProdMin / ENPTempTotal
                        End If

                        Me.cachedENPOil.Add(key, CSng(COil))
                        Me.cachedENPGas.Add(key, CSng(CGas))
                        Me.cachedENPCoal.Add(key, CSng(CCoal))
                        Me.cachedENPHYD.Add(key, CSng(Chyd))

                        'SeriesTable = "SeriesEnExportsTotalIEA"
                        CENX = getClosestYrVal(DictTbls(11), yrss(11), key, cntx.FirstYear, HistDist)
                        CENX = If(CENX, Me.CGDP(key) * 0.0002F)
                        If CENX < 0.0001 Then CENX = 0.0001F
                        Me.cachedENX.Add(key, CENX)

                        'SeriesTable = "SeriesEnImports"
                        CENM = If(getClosestYrVal(DictTbls(0), yrss(0), key, cntx.FirstYear, HistDist), 0.0001F)
                        VeryConsDemand = 0.0002F * Me.CGDP(key) 'World Average .0024*GDP
                        VeryConsImport = VeryConsDemand - ENPTempTotal + CENX
                        If CENM < VeryConsImport Then
                            CENM = AMAX(0.0001, CSng(VeryConsImport))
                        Else
                            CENM = If(CENM, AMAX(0.0001, CSng(VeryConsImport)))
                        End If

                        Me.cachedENM.Add(key, CSng(CENM))

                        Me.cachedENPRENEW.Add(key, CSng(Chyd + Me.cachedENPGEO(key)))


                        'mti 2024/07/23 start
                        'Me.cachedENPOTHREN.Add(key, AMAX(Me.cachedENPGEO(key), CGDP(key) * 0.002F * 0.003F))

                        'Me.cachedENPTOT.Add(key, Me.cachedENPOil(key) + Me.cachedENPGas(key) + Me.cachedENPCoal(key) +
                        'Me.cachedENPHYD(key) + Me.cachedENPNUC(key) + Me.cachedENPOTHREN(key))
                        Me.cachedENPTOT.Add(key, Me.cachedENPOil(key) + Me.cachedENPGas(key) + Me.cachedENPCoal(key) +
                                        Me.cachedENPHYD(key) + Me.cachedENPNUC(key) + Me.cachedENPSOLAR(key) + Me.cachedENPWIND(key) + Me.cachedENPGEOTHERMAL(key) + Me.cachedENPOTHREN(key))

                        'mti 2024/07/23 end

                    Next

                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast
                    Dim Photo As Single?, Wind As Single?, Solar As Single?, TideWave As Single?, BioDiesel As Single?, BioGas As Single
                    Dim ENXCoal As Single?, ENXPeat As Single?, ENXNatGas As Single?, ENXOil As Single?, ENXOilProductsNGL As Single?
                    Dim XOil As Single?, XFossFu As Single?

                    Dim ENMCoal As Single?, ENMPeat As Single?, ENMNatGas As Single?, ENMOil As Single?, ENMOilProductsNGL As Single?
                    Dim MOil As Single?, MFossFu As Single?

                    'jrs 2024/05/04 removing WDI energy sources wich are not being updated anymore: ENPPHOTO, ENPWIND, ENPGEO, ENPTIDEWAVE, ENPSOLAR


                    'Use BP for Oil, Gas, Nuc most recent unless have better data from IEA/UN
                    For Each item In (From s In cntx.GetSlice("ENM", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("ENX", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("ENXCOALIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("ENXPEATIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("ENXNATGASIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("ENXOILIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("ENXOILPRIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals s7.Key
                                      Join mcoal In cntx.GetSlice("ENMCOAL", cntx.FirstYear, MyBase.Policy) On s.Key Equals mcoal.Key
                                      Join mpeat In cntx.GetSlice("ENMPEAT", cntx.FirstYear, MyBase.Policy) On s.Key Equals mpeat.Key
                                      Join mnatgas In cntx.GetSlice("ENMNATGAS", cntx.FirstYear, MyBase.Policy) On s.Key Equals mnatgas.Key
                                      Join mo In cntx.GetSlice("ENMOIL", cntx.FirstYear, MyBase.Policy) On s.Key Equals mo.Key
                                      Join moilpr In cntx.GetSlice("ENMOILPROD", cntx.FirstYear, MyBase.Policy) On s.Key Equals moilpr.Key
                                      Join oil In cntx.GetSlice("ENPOILIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals oil.Key
                                      Join oil2 In cntx.GetSlice("ENPOILBP", cntx.FirstYear, MyBase.Policy) On s.Key Equals oil2.Key
                                      Join gas In cntx.GetSlice("ENPGASIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals gas.Key
                                      Join gas2 In cntx.GetSlice("ENPGASBP", cntx.FirstYear, MyBase.Policy) On s.Key Equals gas2.Key
                                      Join coal In cntx.GetSlice("ENPCOALIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals coal.Key
                                      Join coal2 In cntx.GetSlice("ENPCOALBP", cntx.FirstYear, MyBase.Policy) On s.Key Equals coal2.Key
                                      Join coal3 In cntx.GetSlice("ENPCOAL", cntx.FirstYear, MyBase.Policy) On s.Key Equals coal3.Key
                                      Join hyd In cntx.GetSlice("ENPHYD", cntx.FirstYear, MyBase.Policy) On s.Key Equals hyd.Key
                                      Join hyd2 In cntx.GetSlice("ENPHYDBP", cntx.FirstYear, MyBase.Policy) On s.Key Equals hyd2.Key
                                      Join nuc In cntx.GetSlice("ENPNUC", cntx.FirstYear, MyBase.Policy) On s.Key Equals nuc.Key
                                      Join nuc2 In cntx.GetSlice("ENPNUCBP", cntx.FirstYear, MyBase.Policy) On s.Key Equals nuc2.Key
                                      Join geo In cntx.GetSlice("ENPGEOIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals geo.Key
                                      Join phot In cntx.GetSlice("ENPPHOTOIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals phot.Key
                                      Join win In cntx.GetSlice("ENPWINDIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals win.Key
                                      Join sol In cntx.GetSlice("ENPSOLARIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals sol.Key
                                      Join tw In cntx.GetSlice("ENPTIDEWAVEIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals tw.Key
                                      Join BioD In cntx.GetSlice("ENPBIODIESELIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals BioD.Key
                                      Join BioG In cntx.GetSlice("ENPBIOGASIEA", cntx.FirstYear, MyBase.Policy) On s.Key Equals BioG.Key
                                      Select Key = s.Key, Value = s.Value, compendemperunit = Estimate(s.Key),
                    Oil = If(oil.Value, oil2.Value), Gas = If(gas.Value, gas2.Value),
                    Coal = If(If(coal.Value, coal2.Value), coal3.Value),
                    hyd = If(hyd.Value, hyd2.Value), nuc = If(nuc.Value, nuc2.Value), geo = geo.Value,
                    phot = If(phot.Value, 0.000001F), win = If(win.Value, 0.000001F),
                    sol = If(sol.Value, 0.000001F), tideW = If(tw.Value, 0.000001F),
                    bioD = If(BioD.Value, 0.000001F), bioG = If(BioG.Value, 0.000001F), enx = s2.Value, enxC = s3.Value,
                    enxP = s4.Value, enxNG = s5.Value, enxO = s6.Value, enxOP = s7.Value,
                    mCoal = mcoal.Value, mPeat = mpeat.Value, mNatGas = mnatgas.Value, mO = mo.Value, mOilPr = moilpr.Value)

                        'bbh 2021/08/04 Very old WRI data with too many 0s Join oil3 In cntx.GetSlice("ENPOIL", cntx.FirstYear, MyBase.Policy) On s.Key Equals oil3.Key

                        approxendem = item.compendemperunit * CGDPPCP(item.Key) * CPOP(item.Key)
                        'If item.Key = "Rwanda" Then Stop
                        'jrs 2022/04/06 added * 0.0001, problems with Guinea Bissau
                        'jrs 2026/06/03 COil = If(item.Oil, CGDP(item.Key) * 0.002F * 0.1F) ' * 0.0001F)
                        If item.Oil.HasValue Then
                            COil = CSng(Math.Round(CDbl(item.Oil), 7))
                        Else
                            'jrs 2026/06/03 matching with seed for Oil and Gas
                            If item.Gas.HasValue And item.Coal.HasValue Then
                                COil = 0.1 * (item.Gas + item.Coal)
                            ElseIf item.Gas.HasValue Then
                                COil = 0.1 * item.Gas
                            ElseIf item.Coal.HasValue Then
                                COil = 0.1 * item.Coal
                            Else
                                COil = CGDP(item.Key) * 0.002F * 0.1F
                            End If
                        End If

                        'jrs 2022/04/06
                        If CGas.HasValue Then
                            CGas = AMAX(CGas, 0.000001F)
                            'CGas = AMAX(CGas, 1.0E-10F)
                        End If
                        'jrs 2022/04/06 added * 0.0001, problems with Guinea Bissau
                        'jrs 2026/07/27 CGas = If(item.Gas, CGDP(item.Key) * 0.002F * 0.1F) ' * 0.0001F)
                        If item.Gas.HasValue Then
                            CGas = item.Gas
                        Else
                            'jrs 2026/07/27 matching with seed for Oil and Gas
                            If item.Oil.HasValue And item.Coal.HasValue Then
                                CGas = 0.1 * (item.Oil + item.Coal)
                            ElseIf item.Oil.HasValue Then
                                CGas = 0.1 * item.Oil
                            ElseIf item.Coal.HasValue Then
                                CGas = 0.1 * item.Coal
                            Else
                                CGas = CGDP(item.Key) * 0.002F * 0.1F
                            End If
                        End If
                        'World average is about .0005 of GDP
                        'Set at 10% of approximate total energy demand
                        'CGas = CGas * 7.33 / 1000 'if in million tons of oil, convert to billion barrels
                        If item.Coal.HasValue Then
                            CCoal = CSng(Math.Round(CDbl(item.Coal), 7))
                            'jrs 2022/04/06
                            CCoal = AMAX(CCoal, 0.000001F)
                            'CCoal = AMAX(CCoal, 1.0E-10F)
                        Else
                            'jrs 2022/04/06 matching with seed for Oil and Gas
                            'jrs 2026/07/27 CCoal = CGDP(item.Key) * 0.002F * 0.2F
                            If item.Oil.HasValue And item.Gas.HasValue Then
                                CCoal = 0.1 * (item.Oil + item.Gas)
                            ElseIf item.Oil.HasValue Then
                                CCoal = 0.1 * item.Oil
                            ElseIf item.Gas.HasValue Then
                                CCoal = 0.1 * item.Gas
                            Else
                                CCoal = CGDP(item.Key) * 0.002F * 0.2F
                            End If
                            'CCoal = CGDP(item.Key) * 0.002F * 0.1F * 0.0001F
                        End If
                        'CCoal = If(item.Coal, CGDP(item.Key) * 0.002F * 0.2F)
                        'jrs 2022/04/06 matching with seed for Oil and Gas
                        Chyd = If(item.hyd, CGDP(item.Key) * 0.002F * 0.01F)
                        'Chyd = If(item.hyd, CGDP(item.Key) * 0.002F * 0.1F * 0.0001F)
                        'jrs 2022/04/06 matching with seed for Oil and Gas
                        If Chyd < CGDP(item.Key) * 0.002F * 0.001F Then Chyd = CGDP(item.Key) * 0.002F * 0.001F
                        'If Chyd < CGDP(item.Key) * 0.002F * 0.1F * 0.0001F Then Chyd = CGDP(item.Key) * 0.002F * 0.1F * 0.0001F

                        'jrs 2022/04/06
                        If item.nuc < 0.00001F Then
                            'If item.nuc < 1.0E-9F Then
                            'jrs 2022/04/06
                            Me.cachedENPNUC.Add(item.Key, 0.00001F)
                            'Me.cachedENPNUC.Add(item.Key, 1.0E-9F)
                        Else
                            'jrs 2022/04/06
                            Me.cachedENPNUC.Add(item.Key, If(item.nuc, 0.00001F))
                            'Me.cachedENPNUC.Add(item.Key, If(item.nuc, 1.0E-9F))
                        End If

                        'jrs 2022/04/06
                        'mti 2024/09/05 start
                        If item.Key = "Indonesia" And item.geo.HasValue Then  'data incorrect
                            'Stop
                            Me.cachedENPGEO.Add(item.Key, item.geo / 15)
                        Else
                            Me.cachedENPGEO.Add(item.Key, If(item.geo, 0.00001F))
                        End If
                        'mti 2024/09/05 end

                        Photo = item.phot
                        Wind = item.win
                        Solar = item.sol
                        TideWave = item.tideW
                        BioDiesel = item.bioD
                        BioGas = item.bioG

                        ENPTempTotal = COil + CGas + CCoal +
                    Chyd + Me.cachedENPNUC(item.Key) + Me.cachedENPGEO(item.Key) + Photo + Wind + Solar + TideWave + BioDiesel + BioGas 'jrs 2009/10/07 added bioDiesel and bioGas

                        CENX = item.enx
                        ENXCoal = item.enxC
                        ENXPeat = item.enxP
                        ENXNatGas = item.enxNG
                        ENXOil = item.enxO
                        ENXOilProductsNGL = item.enxOP

                        'If item.Key = "Serbia" Then Stop

                        XOil = Nothing
                        XFossFu = Nothing
                        If ENXOil.HasValue Then
                            XOil = ENXOil
                            If ENXOilProductsNGL.HasValue Then
                                XOil = XOil + ENXOilProductsNGL
                                If ENXCoal.HasValue And ENXPeat.HasValue And ENXNatGas.HasValue Then
                                    XFossFu = XOil + ENXCoal + ENXPeat + ENXNatGas
                                End If
                            End If
                        End If

                        If CENX.HasValue And XFossFu.HasValue Then CENX = AMAX(CENX, XFossFu)
                        If CENX.HasValue And XOil.HasValue Then CENX = AMAX(CENX, XOil)
                        If Not CENX.HasValue Then
                            If XFossFu.HasValue Then CENX = XFossFu
                        End If
                        If Not CENX.HasValue Then
                            If XOil.HasValue Then CENX = XOil
                        End If

                        If Not CENX.HasValue Then
                            'jrs 2022/04/06 added * 0.0001
                            CENX = 0.0002F * CGDP(item.Key) '* 0.0001F
                        End If
                        '        CENX(KEY) = CENX(KEY) * 7.33 / 1000
                        'jrs 2022/04/06
                        If CENX < 0.0001 Then CENX = 0.0001F
                        'If CENX < 0.00000001 Then CENX = 0.00000001F

                        CENM = item.Value

                        ENMCoal = item.mCoal
                        ENMPeat = item.mPeat
                        ENMNatGas = item.mNatGas

                        ENMOil = item.mO
                        ENMOilProductsNGL = item.mOilPr

                        MOil = Nothing
                        MFossFu = Nothing

                        If ENMOil.HasValue Then
                            MOil = ENMOil
                            If ENMOilProductsNGL.HasValue Then
                                MOil = MOil + ENMOilProductsNGL
                                If ENMCoal.HasValue And ENMPeat.HasValue And ENMNatGas.HasValue Then
                                    MFossFu = MOil + ENMCoal + ENMPeat + ENMNatGas
                                End If
                            End If
                        End If

                        If CENM.HasValue And MFossFu.HasValue Then CENM = AMAX(CENM, MFossFu)
                        If CENM.HasValue And MOil.HasValue Then CENM = AMAX(CENM, MOil)
                        If Not CENM.HasValue Then
                            If MFossFu.HasValue Then CENM = MFossFu
                        End If
                        If Not CENM.HasValue Then
                            If MOil.HasValue Then CENM = MOil
                        End If

                        'jrs 2022/04/06 added * 0.0001
                        VeryConsDemand = 0.0002F * CGDP(item.Key) '* 0.0001F 'World Average .0024*GDP
                        VeryConsImport = VeryConsDemand - ENPTempTotal + CENX
                        If Not CENM.HasValue Or CENM < VeryConsImport Then
                            'jrs 2022/04/06
                            CENM = AMAX(0.0001F, VeryConsImport)
                            'CENM = AMAX(0.00000001F, VeryConsImport)
                            'Note:  globally, about .002 barrels/$ of GDP
                            '                CENM(KEY) = AMAX(0.0001, CGDP(KEY) * 0.002 + _
                            'CENX(KEY)) ' ENPTotal not yet computed
                        End If

                        '            If CENM(KEY) * WorldEP > 0.1 * CGDP(KEY) Then
                        '                CENM(KEY) = 0.1 * CGDP(KEY) / AMAX(1, WorldEP)
                        '            End If   'Wasn't allowing transshipment, a la Ukraine
                        If (CENM - CENX) * WorldEP > 0.15F * CGDP(item.Key) Then
                            CENM = 0.15F * CGDP(item.Key) / AMAX(1, WorldEP) + CENX
                        End If

                        Me.cachedENM.Add(item.Key, CSng(CENM))
                        Me.cachedENX.Add(item.Key, CENX)

                        Me.cachedENPOil.Add(item.Key, CSng(COil))
                        Me.cachedENPGas.Add(item.Key, CSng(CGas))
                        Me.cachedENPCoal.Add(item.Key, CSng(CCoal))
                        Me.cachedENPHYD.Add(item.Key, CSng(Chyd))

                        Me.cachedENPRENEW.Add(item.Key, CSng(Chyd + Me.cachedENPGEO(item.Key) + Photo + Solar + Wind + TideWave + BioDiesel + BioGas))
                        'jrs 2022/04/06 * 0.0001

                        'mti 2024/07/23 start
                        'Me.cachedENPOTHREN.Add(item.Key, AMAX(Me.cachedENPGEO(item.Key) + Photo + Solar + WIND + TideWave + BioDiesel + BioGas, CGDP(item.Key) * 0.002F * 0.003F))
                        ''Me.cachedENPOTHREN.Add(item.Key, AMAX(Me.cachedENPGEO(item.Key) + Photo + Solar + WIND + TideWave + BioDiesel + BioGas, CGDP(item.Key) * 0.002F * 0.003F * 0.0001F))

                        'Me.cachedENPTOT.Add(item.Key, Me.cachedENPOil(item.Key) + Me.cachedENPGas(item.Key) + Me.cachedENPCoal(item.Key) +
                        '                Me.cachedENPHYD(item.Key) + Me.cachedENPNUC(item.Key) + Me.cachedENPOTHREN(item.Key))

                        Me.cachedENPSOLAR.Add(item.Key, CSng(Photo + Solar))
                        Me.cachedENPWIND.Add(item.Key, CSng(Wind))

                        'mti 2024/09/05 start
                        If item.Key = "Indonesia" And item.geo.HasValue Then  'data incorrect
                            Me.cachedENPGEOTHERMAL.Add(item.Key, item.geo / 15)
                        Else
                            Me.cachedENPGEOTHERMAL.Add(item.Key, If(item.geo, 0.00001F))
                        End If
                        'mti 2024/09/05 end


                        Me.cachedENPOTHREN.Add(item.Key, AMAX(TideWave + BioDiesel + BioGas, CGDP(item.Key) * 0.002F * 0.003F))
                        'Me.cachedENPOTHREN.Add(item.Key, AMAX(Me.cachedENPGEO(item.Key) + Photo + Solar + WIND + TideWave + BioDiesel + BioGas, CGDP(item.Key) * 0.002F * 0.003F * 0.0001F))

                        Me.cachedENPTOT.Add(item.Key, Me.cachedENPOil(item.Key) + Me.cachedENPGas(item.Key) + Me.cachedENPCoal(item.Key) +
                                        Me.cachedENPHYD(item.Key) + Me.cachedENPNUC(item.Key) + Me.cachedENPSOLAR(item.Key) +
                                        Me.cachedENPWIND(item.Key) + Me.cachedENPGEOTHERMAL(item.Key) + Me.cachedENPOTHREN(item.Key))
                        'mti 2024/07/23 end


                    Next
                End If 'if historical the else forecast end if

                '   Normalize world energy trade
                '   --------------------
                Dim ExportTot As Single?, ImportTot As Single?, GloAppCon As Single?, GloGDP As Single?, TradeTot As Single?
                Dim AppCon_1 As Single?, AppConF As Double?, AppProd As Single?, MulF As Single?, Surplus As Single?
                Dim BaseForExport As Single?, Headroom As Single?, msg As String, SumOilGas As Single?, ReducedProd As Single?, ProdMul As Single?
                Dim AppConEn As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                ExportTot = 0
                ImportTot = 0
                GloAppCon = 0
                GloGDP = 0
                For Each key In cntx.Regionalization.Keys
                    ExportTot = ExportTot + Me.cachedENX(key)
                    ImportTot = ImportTot + Me.cachedENM(key)
                Next

                TradeTot = (ExportTot + ImportTot) / 2   'Actual Normalization

                For Each key In cntx.Regionalization.Keys
                    'jrs 2020/03/24 If key = "Cuba" Then Stop
                    CENX = Me.cachedENX(key) * TradeTot / ExportTot
                    CENM = Me.cachedENM(key) * TradeTot / ImportTot

                    cachedENX.Remove(key)
                    cachedENX.Add(key, CENX)

                    cachedENM.Remove(key)
                    cachedENM.Add(key, CSng(CENM))

                    '       Apparent Consumption
                    '       --------------------

                    AppCon_1 = cachedENPOil(key) + cachedENPGas(key) + cachedENPCoal(key)
                    'If CountryName(KEY) = "Ethiopia" Then Stop

                    'mti 2024/07/23 start
                    'AppCon_1 = AppCon_1 + cachedENPHYD(key) + cachedENPNUC(key) + cachedENPOTHREN(key)
                    AppCon_1 = AppCon_1 + cachedENPHYD(key) + cachedENPNUC(key) + cachedENPSOLAR(key) +
                                cachedENPWIND(key) + cachedENPGEOTHERMAL(key) + cachedENPOTHREN(key)
                    'mti 2024/07/23 end
                    AppConEn.Add(key, CSng(AppCon_1 + CENM - CENX))
                    If AppConEn(key) > 0 Then
                        GloAppCon = GloAppCon + AppConEn(key)
                        GloGDP = GloGDP + CGDP(key)
                    End If
                Next

                AppConF = GloAppCon / AMAX(0.01, GloGDP)

                '   Readjust Where Apparent Consumption is Zero or Negative or Too Large
                '   --------------------------------------------------------------------
                'gvdb 2024/04/05
                'unused variable
                Dim IndVar As Single?, GDPPTemp As Single?, EnMinF As Single, compendemperun As Single? ' TbName As String, 
                Dim computedenergy As Single?, NewProd As Double?, AdjustFac As Single?

                If cntx.HistoricMode Then
                    Estimate = cntx.GetEstimator("ENM_2")
                End If

                For Each key In cntx.Regionalization.Keys
                    'If key = "Iraq" Then Stop
                    IndVar = CGDPPCP(key)
                    GDPPTemp = IndVar * CPOP(key)

                    'jrs 2022/04/06
                    EnMinF = 0.2 'bbh 2016/05/03
                    'EnMinF = 0
                    ' Comments by bbh 2016/05/03
                    ' The TabFunc used here has a significant downward slope, consistent with improving energy efficency at higher GDPPCP
                    ' But the energy data we have (look in 2010 at cross-section of energy demand ratio to GDPP against GDPPCP) has a
                    '  lot of African countries esp with very low ratios and the best fit would have a small upward slope
                    ' This gives rise to big initial transients in history to forecast energy demand ratio to GDPP
                    ' If used that function instead would have all countries including high income becoming less efficient in forecasts,
                    '   clearly that is wrong pattern historically
                    ' Two possibilities:  (1) could have inverted-U, the famous Kuznets energy demand curve, but cross-section rrather weak for it
                    '   (2) could recognize that African energy production data (basis for app demand) and other low-income does not include biomatter,
                    '   so maybe downward sloping logarithmic function is ok.
                    ' For right now I changed EnMinF from 0.4 to 0.2 to bring the initial forcing of energy demand for low-income countries down a lot.
                    ' The ideal would be to have a non-commerical biomatter energy element on the production side; or maybe a good inverted-U demand curve
                    '    Ethiopia is an especially bad case of disjoint between apparent consumption and the current function's value
                    'bbh 2017/07/29 I ran up against this issue again:
                    '1.   We are not including biomass in our energy model energy type set.  We might want to put it into  other renewable, an easy fix,
                    'although it is rather nice to separate old and new renewable forms
                    '2.  We have an energy demand function that shows that as GDP per capita drops, energy demand/GDP should rise (as in our base year).
                    'Because biomass is not added to production (and therefore apparent demand) in the preprocessor, that function imposes an increase
                    'on the production of the energy forms that are represented.  In the case of Ethiopia, only hydro is produced in any quantity, so
                    'it gets jacked up way beyond what is actually in the data, creating a second error in an effort to adjust for the first one (biomass omission).
                    'Even doing that, total energy production in Ethiopia is initialized below the level that the nice ENPRODTOTCDIEA series tells us it should be,
                    'were biomatter in the equation,
                    'We could use ENPRODOTCDIEA/GDP as a function of GDPPCP to update the energy demand function that is serving us pretty well.

                    compendemperun = Estimate(key)

                    computedenergy = compendemperun * GDPPTemp
                    'If key = "Rwanda" Then Stop
                    If AppConEn(key) <= 0 Or AppConEn(key) < EnMinF * computedenergy Then
                        'if exports are greater than production + imports or energy too small

                        'AppConEn(KEY) = CGDP(KEY) * AppConF
                        AppConEn(key) = EnMinF * computedenergy

                        cachedENPTOT.Remove(key)
                        'mti 2024/07/23 start
                        'cachedENPTOT.Add(key, cachedENPOil(key) + cachedENPGas(key) + cachedENPCoal(key) +
                        'cachedENPHYD(key) + cachedENPNUC(key) + cachedENPOTHREN(key))
                        cachedENPTOT.Add(key, cachedENPOil(key) + cachedENPGas(key) + cachedENPCoal(key) +
                        cachedENPHYD(key) + cachedENPNUC(key) + cachedENPSOLAR(key) +
                         +cachedENPWIND(key) + +cachedENPGEOTHERMAL(key) + cachedENPOTHREN(key))
                        'mti 2024/07/23 end

                        'If CountryName$(KEY) = "Qatar" Then Stop

                        NewProd = AMAX(0.00001, AppConEn(key) - cachedENM(key) + cachedENX(key))
                        'ENPOilGas(key) = ENPOilGas(key) * NewProd / ENPTotal(key)
                        COil = cachedENPOil(key) * NewProd / cachedENPTOT(key)
                        cachedENPOil.Remove(key)
                        cachedENPOil.Add(key, CSng(COil))
                        CGas = cachedENPGas(key) * NewProd / cachedENPTOT(key)
                        cachedENPGas.Remove(key)
                        cachedENPGas.Add(key, CSng(CGas))
                        CCoal = cachedENPCoal(key) * NewProd / cachedENPTOT(key)
                        cachedENPCoal.Remove(key)
                        cachedENPCoal.Add(key, CSng(CCoal))
                        ' If CountryName$(KEY) = "Bangladesh" Then Stop
                        Chyd = cachedENPHYD(key) * NewProd / cachedENPTOT(key)
                        cachedENPHYD.Remove(key)
                        cachedENPHYD.Add(key, CSng(Chyd))
                        CRenew = cachedENPRENEW(key) * NewProd / cachedENPTOT(key)
                        cachedENPRENEW.Remove(key)
                        cachedENPRENEW.Add(key, CSng(CRenew))
                        Cnuc = cachedENPNUC(key) * NewProd / cachedENPTOT(key)
                        cachedENPNUC.Remove(key)
                        cachedENPNUC.Add(key, CSng(Cnuc))
                        'If CountryName$(KEY) = "Afghanistan" Then Stop

                        'mti 2024/07/23 start
                        COthRenew = cachedENPSOLAR(key) * NewProd / cachedENPTOT(key)
                        cachedENPSOLAR.Remove(key)
                        cachedENPSOLAR.Add(key, CSng(COthRenew))

                        COthRenew = cachedENPWIND(key) * NewProd / cachedENPTOT(key)
                        cachedENPWIND.Remove(key)
                        cachedENPWIND.Add(key, CSng(COthRenew))

                        COthRenew = cachedENPGEOTHERMAL(key) * NewProd / cachedENPTOT(key)
                        cachedENPGEOTHERMAL.Remove(key)
                        cachedENPGEOTHERMAL.Add(key, CSng(COthRenew))
                        'mti 2024/07/23 end

                        COthRenew = cachedENPOTHREN(key) * NewProd / cachedENPTOT(key)
                        cachedENPOTHREN.Remove(key)
                        cachedENPOTHREN.Add(key, CSng(COthRenew))
                    End If
                    'If CountryName$(KEY) = "Papua New Guinea" Then Stop

                    'Reconcile data on production and trade with computed energy demand for
                    '       Forecasting
                    AppCon_1 = cachedENPOil(key) + cachedENPGas(key) + cachedENPCoal(key)

                    'mti 2024/07/23 start
                    'AppCon_1 = AppCon_1 + cachedENPHYD(key) + cachedENPNUC(key) + cachedENPOTHREN(key)
                    AppCon_1 = AppCon_1 + cachedENPHYD(key) + cachedENPNUC(key) + cachedENPSOLAR(key) +
                            cachedENPWIND(key) + cachedENPGEOTHERMAL(key) + cachedENPOTHREN(key)
                    'mti 2024/07/23 end

                    AppProd = AMAX(0.0001F, AppCon_1)
                    AppConEn(key) = AppCon_1 + cachedENM(key) - cachedENX(key)

                    'bbh 2017/10/21 Changed factor of 4 below to factor of 5; was messing up Bangladesh
                    '       by about doubling its natural gas production and bumping up against reserves,
                    '       thereby constraining productoin in early years and causing shortages (low imports)
                    If computedenergy > 5 * AppConEn(key) Then 'Increase En Production
                        '            AdjustFac = Amin(20, ComputedEnergy / AMAX(0.00001, (2 * AppConEn(KEY))))
                        '            ENPOil(KEY) = ENPOil(KEY) * AdjustFac
                        '            ENPGas(KEY) = ENPGas(KEY) * AdjustFac
                        '            ENPCoal(KEY) = ENPCoal(KEY) * AdjustFac
                        AdjustFac = computedenergy - 5 * AppConEn(key)
                        AdjustFac = Amin(AdjustFac, 20 * AppProd)
                        COil = cachedENPOil(key) + AdjustFac * cachedENPOil(key) / AppProd
                        cachedENPOil.Remove(key)
                        cachedENPOil.Add(key, CSng(COil))
                        CGas = cachedENPGas(key) + AdjustFac * cachedENPGas(key) / AppProd
                        cachedENPGas.Remove(key)
                        cachedENPGas.Add(key, CSng(CGas))
                        CCoal = cachedENPCoal(key) + AdjustFac * cachedENPCoal(key) / AppProd
                        cachedENPCoal.Remove(key)
                        cachedENPCoal.Add(key, CSng(CCoal))

                        ' If CountryName$(KEY) = "Bangladesh" Then Stop

                        Chyd = cachedENPHYD(key) + AdjustFac * cachedENPHYD(key) / AppProd
                        cachedENPHYD.Remove(key)
                        cachedENPHYD.Add(key, CSng(Chyd))
                        Cnuc = cachedENPNUC(key) + AdjustFac * cachedENPNUC(key) / AppProd
                        cachedENPNUC.Remove(key)
                        cachedENPNUC.Add(key, CSng(Cnuc))

                        'mti 2024/07/23 start
                        COthRenew = cachedENPSOLAR(key) + AdjustFac * cachedENPSOLAR(key) / AppProd
                        cachedENPSOLAR.Remove(key)
                        cachedENPSOLAR.Add(key, CSng(COthRenew))

                        COthRenew = cachedENPWIND(key) + AdjustFac * cachedENPWIND(key) / AppProd
                        cachedENPWIND.Remove(key)
                        cachedENPWIND.Add(key, CSng(COthRenew))

                        COthRenew = cachedENPGEOTHERMAL(key) + AdjustFac * cachedENPGEOTHERMAL(key) / AppProd
                        cachedENPGEOTHERMAL.Remove(key)
                        cachedENPGEOTHERMAL.Add(key, CSng(COthRenew))
                        'mti 2024/07/23 end

                        COthRenew = cachedENPOTHREN(key) + AdjustFac * cachedENPOTHREN(key) / AppProd
                        cachedENPOTHREN.Remove(key)
                        cachedENPOTHREN.Add(key, CSng(COthRenew))
                    End If

                    'jrs 2022/04/07
                    'Zero out all ENP for Guinea Bissau
                    '----------------------------------
                    'jrs 2022/05/02 If key = "Guinea Bissau" And Not cntx.HistoricMode Then

                    If key = sEnergyNoProductionCountry And Not cntx.HistoricMode Then
                        cachedENPOil(key) = 0
                        cachedENPGas(key) = 0
                        cachedENPCoal(key) = 0
                        cachedENPHYD(key) = 0
                        cachedENPNUC(key) = 0

                        'mti 2024/07/23 start
                        cachedENPSOLAR(key) = 0
                        cachedENPWIND(key) = 0
                        cachedENPGEOTHERMAL(key) = 0
                        'mti 2024/07/23 end

                        cachedENPOTHREN(key) = 0
                        cachedENX(key) = 0
                    End If


                    AppCon_1 = cachedENPOil(key) + cachedENPGas(key) + cachedENPCoal(key)

                    'mti 2024/07/23 start
                    'AppCon_1 = AppCon_1 + cachedENPHYD(key) + cachedENPNUC(key) + cachedENPOTHREN(key)
                    AppCon_1 = AppCon_1 + cachedENPHYD(key) + cachedENPNUC(key) + cachedENPSOLAR(key) +
                         +cachedENPWIND(key) + cachedENPGEOTHERMAL(key) + cachedENPOTHREN(key)
                    'mti 2024/07/23 end

                    'AppProd = AMAX(0.0001, AppCon_1)
                    AppConEn(key) = AppCon_1 + cachedENM(key) - cachedENX(key)

                    If cntx.HistoricMode And key = "China" Then
                        MulF = 6  'China much more energy intensive historically than normal econ
                    Else
                        MulF = 3
                        If key = "Ukraine" Then MulF = 6
                    End If

                    ' If CountryName$(KEY) = "China" Then Stop 'needs mulf=3.5

                    If AppConEn(key) > compendemperun * MulF * CGDP(key) Then 'Try to increase En Exports
                        'e.g. no export data for Equatorial Guinea after production, exports built up
                        Surplus = AppConEn(key) - compendemperun * MulF * CGDP(key)

                        BaseForExport = cachedENPOil(key) + cachedENPGas(key) + cachedENPCoal(key) + cachedENM(key)
                        If cachedENX(key) < 0.95 * BaseForExport Then
                            Headroom = 0.95F * BaseForExport - cachedENX(key)
                            CENX = cachedENX(key) + Amin(Surplus, Headroom)
                            cachedENX.Remove(key)
                            cachedENX.Add(key, CENX)
                        End If
                        'If CountryName$(KEY) = "Bhutan" Then Stop

                        'If CountryName$(KEY) = "Equatorial Guinea" Then Stop

                        ' If CountryName$(KEY) = "Equatorial Guinea" Then Stop

                    End If
                    'If CountryName$(KEY) = "Bhutan" Then Stop
                    AppCon_1 = cachedENPOil(key) + cachedENPGas(key) + cachedENPCoal(key)

                    'mti 2024/07/23 start
                    'AppCon_1 = AppCon_1 + cachedENPHYD(key) + cachedENPNUC(key) + cachedENPOTHREN(key)
                    AppCon_1 = AppCon_1 + cachedENPHYD(key) + cachedENPNUC(key) + cachedENPSOLAR(key) +
                                        cachedENPWIND(key) + cachedENPGEOTHERMAL(key) + cachedENPOTHREN(key)
                    'mti 2024/07/23 end

                    'AppProd = AMAX(0.0001, AppCon_1)
                    AppConEn(key) = AppCon_1 + cachedENM(key) - cachedENX(key)
                    If AppConEn(key) > compendemperun * MulF * CGDP(key) * 2 Then
                        msg$ = "Apparent energy consumption for " + key +
                        " is more than twice desired upper limit - performance will be poor."
                        'MsgBox(msg$)
                    End If


                    '   Adjust if Net Exports Too Large in GDP
                    '   --------------------------------------

                    '  If CountryName$(KEY) = "China" Then Stop

                    'If (cachedENX(key) - cachedENM(key)) > 0.7F * cachedGDPRPA(key) / WorldEP Then
                    'bbh 2024/06/26 Replaced 70% with 33% to fix Sudan South at 64%, well above #2 Congo at 32% (Qatar only 22%)
                    'If (cachedENX(key) - cachedENM(key)) > 0.33F * cachedGDPRPA(key) / WorldEP Then

                    'bbh 2025/02/10 start making South Sudan different from other countries
                    ''If (cachedENX(key) - cachedENM(key)) > 0.33F * CGDP(key) / WorldEP Then 'bbh 2024/06/26 GDPRPA much larger than CGDP
                    'jrs 2026/02/26 added Chad just for testing, but maybe should make it different like South Sudan
                    'If (cachedENX(key) - cachedENM(key)) > 0.7F * CGDP(key) / WorldEP Or (key = "Sudan South" Or key = "Chad") Then
                    If (cachedENX(key) - cachedENM(key)) > 0.7F * CGDP(key) / WorldEP Or (key = "Sudan South") Then
                        'Turkmenistan apparent exports out of line
                        'Surplus = (cachedENX(key) - cachedENM(key)) - 0.7F * cachedGDPRPA(key) / WorldEP 
                        'If (cachedENX(key) - cachedENM(key)) > 0.33F * CGDP(key) / WorldEP Then 'bbh 2024/06/26 GDPRPA much larger than CGDP

                        Surplus = (cachedENX(key) - cachedENM(key)) - 0.7F * CGDP(key) / WorldEP
                        'Surplus = (cachedENX(key) - cachedENM(key)) - 0.33F * CGDP(key) / WorldEP 'bbh 2024/06/26
                        'jrs 2026/02/26 added Chad just for testing
                        'If (key = "Sudan South" Or key = "Chad") And (cachedENX(key) - cachedENM(key)) > 0.33F * CGDP(key) / WorldEP Then
                        If (key = "Sudan South") And (cachedENX(key) - cachedENM(key)) > 0.33F * CGDP(key) / WorldEP Then
                            Surplus = (cachedENX(key) - cachedENM(key)) - 0.33F * CGDP(key) / WorldEP
                        End If
                        'bbh 2025/02/10 end making South Sudan different
                        SumOilGas = AMAX(0.001F, cachedENPOil(key) + cachedENPGas(key))
                        ReducedProd = AMAX(0.1F * SumOilGas, SumOilGas - Surplus)
                        ProdMul = ReducedProd / SumOilGas
                        COil = cachedENPOil(key) * ProdMul
                        cachedENPOil.Remove(key)
                        cachedENPOil.Add(key, CSng(COil))
                        CGas = cachedENPGas(key) * ProdMul
                        cachedENPGas.Remove(key)
                        cachedENPGas.Add(key, CSng(CGas))
                        CENX = cachedENX(key) - Surplus
                        cachedENX.Remove(key)
                        cachedENX.Add(key, CENX)
                        'End If
                    End If

                    'mti 2024/07/23 start
                    'temporary 
                    'Me.cachedENPSOLAR.Add(key, Me.cachedENPOTHREN(key))
                    'Me.cachedENPOTHREN(key) = 0
                    'mti 2024/07/23 end

                Next

                '   Renormalize world energy trade
                '   --------------------

                ExportTot = 0
                ImportTot = 0
                GloAppCon = 0
                GloGDP = 0
                For Each key In cntx.Regionalization.Keys
                    ExportTot = ExportTot + cachedENX(key)
                    ImportTot = ImportTot + cachedENM(key)
                Next

                TradeTot = (ExportTot + ImportTot) / 2   'Actual Normalization
                For Each key In cntx.Regionalization.Keys
                    ' If CountryName$(item.key) = "China" Then Stop
                    CENX = cachedENX(key) * TradeTot / ExportTot
                    cachedENX.Remove(key)
                    cachedENX.Add(key, CENX)
                    CENM = cachedENM(key) * TradeTot / ImportTot
                    cachedENM.Remove(key)
                    cachedENM.Add(key, CSng(CENM))
                Next

                'Reserves
                '----------
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriTables As New List(Of String)
                    Dim DictTbls As New List(Of IDictionary(Of String, Single?()))
                    Dim yrss As New List(Of Short())
                    SeriTables.Add("RESEROIL") '0
                    SeriTables.Add("RESERGAS")
                    SeriTables.Add("RESERCOALBP")
                    SeriTables.Add("RESERHYD") '3

                    'mti 2024/08/29 start
                    'SeriTables.Add("POTENTIALHYDRO") '4
                    'SeriTables.Add("POTENTIALSOLAR")
                    'SeriTables.Add("POTENTIALWIND")
                    'SeriTables.Add("POTENTIALGEO")
                    'mti 2024/08/29 end

                    Dim i As Integer
                    For i = 0 To SeriTables.Count - 1
                        yrss.Add(cntx.GetYears(SeriTables(i)).OrderBy(Function(y) y).ToArray())
                        DictTbls.Add(cntx.GetDice(SeriTables(i), yrss(i)))
                    Next
                    For Each key In cntx.Regionalization.Keys
                        cachedRESOIL.Add(key, If(getClosestYrVal(DictTbls(0), yrss(0), key, cntx.FirstYear, HistDist), 0.001F))
                        cachedRESGAS.Add(key, If(getClosestYrVal(DictTbls(1), yrss(1), key, cntx.FirstYear, HistDist), 0.001F))
                        cachedRESCOAL.Add(key, If(getClosestYrVal(DictTbls(2), yrss(2), key, cntx.FirstYear, HistDist), 0.01F))
                        cachedRESHYD.Add(key, If(getClosestYrVal(DictTbls(3), yrss(3), key, cntx.FirstYear, HistDist), 0.01F) / 474750.0F) 'installed megawatts to bil barrels; from WRI Annual

                        'mti 2024/08/29 start
                        'cachedRESHYD.Add(key, If(getClosestYrVal(DictTbls(4), yrss(4), key, cntx.FirstYear, HistDist), 0.01F) * 0.000629647F) 'POTENTIALHYDRO
                        'If Not cachedRESHYD(key).HasValue Then
                        '    cachedRESHYD.Add(key, If(getClosestYrVal(DictTbls(3), yrss(3), key, cntx.FirstYear, HistDist), 0.01F) / 474750.0F) 'RESERHYD
                        'End If
                        'cachedRESSOLAR.Add(key, If(getClosestYrVal(DictTbls(5), yrss(5), key, cntx.FirstYear, HistDist), 0.01F) * 0.000629647F) 'POTENTIALSOLAR
                        'cachedRESWIND.Add(key, If(getClosestYrVal(DictTbls(6), yrss(6), key, cntx.FirstYear, HistDist), 0.01F) * 0.000629647F) 'POTENTIALWIND
                        'cachedRESGEO.Add(key, If(getClosestYrVal(DictTbls(7), yrss(7), key, cntx.FirstYear, HistDist), 0.01F) * 0.000629647F) 'POTENTIALGeo
                        'mti 2024/08/29 end



                        If cachedRESOIL(key) < 0.01F Then cachedRESOIL(key) = 15 * cachedENPOil(key)
                        If cachedRESOIL(key) < 10 * cachedENPOil(key) Then
                            cachedRESOIL(key) = 10 * cachedENPOil(key)
                        End If

                        'jrs 2022/04/11 If cachedRESGAS(key) < 0.01F Then cachedRESGAS(key) = 15 * cachedENPGas(key)
                        If cachedRESGAS(key) < 0.001F Then cachedRESGAS(key) = 15 * cachedENPGas(key)
                        If cachedRESGAS(key) < 10 * cachedENPGas(key) Then
                            cachedRESGAS(key) = 10 * cachedENPGas(key)
                        End If

                        'jrs 2022/04/11 If cachedRESCOAL(key) < 0.02F Then cachedRESCOAL(key) = 20 * cachedENPCoal(key)
                        If cachedRESCOAL(key) < 0.01F Then cachedRESCOAL(key) = 20 * cachedENPCoal(key)
                        If cachedRESCOAL(key) < 30 * cachedENPCoal(key) Then
                            cachedRESCOAL(key) = 30 * cachedENPCoal(key)
                        End If

                        If cachedRESHYD(key) < 0.02F Then cachedRESHYD(key) = 2 * cachedENPHYD(key)
                        If cachedRESHYD(key) < 1.5 * cachedENPHYD(key) Then
                            cachedRESHYD(key) = CSng(1.5 * cachedENPHYD(key))
                        End If

                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else 'Forecast
                    'mti 2024/08/29 start

                    'Comment changes
                    'Dim Resero As Single?, ReserOBP As Single?, ReserOBGR As Single?, ReserG As Single?, ReserGBP As Single?, ReserGBGR As Single?
                    'Dim ReserCoal As Single?, ReserHyd As Single?, ReserSolar As Single?, ReserWind As Single?, ReserGeo As Single?

                    'and bring back original - Reser for Solar, Wind, Geo not needed - Resor needed
                    Dim Resero As Single?, ReserOBP As Single?, ReserOBGR As Single?, ReserG As Single?, ReserGBP As Single?, ReserGBGR As Single?
                    Dim ReserCoal As Single?, ReserHyd As Single?
                    For Each item In (From s In cntx.GetSlice("RESEROIL", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("RESEROILBP", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("RESEROILBGR", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join g In cntx.GetSlice("RESERGAS", cntx.FirstYear, MyBase.Policy) On s.Key Equals g.Key
                                      Join g2 In cntx.GetSlice("RESERGASBP", cntx.FirstYear, MyBase.Policy) On s.Key Equals g2.Key
                                      Join g3 In cntx.GetSlice("RESERGASBGR", cntx.FirstYear, MyBase.Policy) On s.Key Equals g3.Key
                                      Join c In cntx.GetSlice("RESERCOALBP", cntx.FirstYear, MyBase.Policy) On s.Key Equals c.Key
                                      Join h In cntx.GetSlice("RESERHYD", cntx.FirstYear, MyBase.Policy) On s.Key Equals h.Key
                                      Select Key = s.Key, Value = If(s.Value, 0.001F), oBP = If(s2.Value, 0.001F), oBGR = If(s3.Value, 0.001F),
                                      Gas = If(g.Value, 0.001F), GasBP = If(g2.Value, 0.001F), GasBGR = If(g3.Value, 0.001F),
                                      Coal = If(c.Value, 0.01F), Hyd = If(h.Value, 0.01F) / 474750.0F)

                        '    For Each item In (From s In cntx.GetSlice("RESEROIL", cntx.FirstYear, MyBase.Policy)
                        '                      Join s2 In cntx.GetSlice("RESEROILBP", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                        '                      Join s3 In cntx.GetSlice("RESEROILBGR", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                        '                      Join g In cntx.GetSlice("RESERGAS", cntx.FirstYear, MyBase.Policy) On s.Key Equals g.Key
                        '                      Join g2 In cntx.GetSlice("RESERGASBP", cntx.FirstYear, MyBase.Policy) On s.Key Equals g2.Key
                        '                      Join g3 In cntx.GetSlice("RESERGASBGR", cntx.FirstYear, MyBase.Policy) On s.Key Equals g3.Key
                        '                      Join c In cntx.GetSlice("RESERCOALBP", cntx.FirstYear, MyBase.Policy) On s.Key Equals c.Key
                        '                      Join h In cntx.GetSlice("RESERHYD", cntx.FirstYear, MyBase.Policy) On s.Key Equals h.Key
                        '                      Join hp In cntx.GetSlice("POTENTIALHYDRO", cntx.FirstYear, MyBase.Policy) On s.Key Equals hp.Key
                        '                      Join sp In cntx.GetSlice("POTENTIALSOLAR", cntx.FirstYear, MyBase.Policy) On s.Key Equals sp.Key
                        '                      Join wp In cntx.GetSlice("POTENTIALWIND", cntx.FirstYear, MyBase.Policy) On s.Key Equals wp.Key
                        '                      Join gp In cntx.GetSlice("POTENTIALGEO", cntx.FirstYear, MyBase.Policy) On s.Key Equals gp.Key
                        '                      Select Key = s.Key, Value = If(s.Value, 0.001F), oBP = If(s2.Value, 0.001F), oBGR = If(s3.Value, 0.001F),
                        '                      Gas = If(g.Value, 0.001F), GasBP = If(g2.Value, 0.001F), GasBGR = If(g3.Value, 0.001F),
                        '                      Coal = If(c.Value, 0.01F), Hyd = If(h.Value, 0.01F) / 474750.0F, HydPot = If(hp.Value, -0.01F) * 0.000588440744F,
                        '                      SolarPot = If(sp.Value, -0.01F) * 0.000588440744F, WindPot = If(wp.Value, -0.01F) * 0.000588440744F, GeoPot = If(gp.Value, -0.01F) * 0.000588440744F
                        ')
                        'mti 2024/08/29 end

                        Resero = item.Value
                        ReserOBP = item.oBP
                        Resero = AMAX(Resero, ReserOBP)
                        ReserOBGR = item.oBGR
                        ReserOBGR = ReserOBGR * 7.33F / 1000.0F
                        Resero = AMAX(Resero, ReserOBGR)

                        ReserG = item.Gas
                        ReserGBP = item.GasBP
                        ReserG = AMAX(ReserG, ReserGBP)
                        ReserGBGR = item.GasBGR
                        ReserGBGR = ReserGBGR * 6.6F / 1000.0F
                        ReserG = AMAX(ReserG, ReserGBGR)

                        'use BP coal reserve data which is already converted into BBOE
                        '.181818 is the quadrillion BTU to BBOE conversion not mil tons to BBOE
                        'ReserCoal(item.key) = ReserCoal(item.key) / 0.181818 / 1000 'mil tons coal to bil barrels oil
                        ReserCoal = item.Coal
                        'mti 2024/08/29 start
                        ReserHyd = item.Hyd
                        'ReserHyd = item.HydPot
                        'If ReserHyd < 0 Then
                        '    ReserHyd = item.Hyd
                        'End If
                        'mti 2024/08/29 end
                        If ReserHyd < 0.02 Then ReserHyd = 2 * cachedENPHYD(item.Key)
                        If ReserHyd < 1.5 * cachedENPHYD(item.Key) Then
                            ReserHyd = CSng(1.5 * cachedENPHYD(item.Key))
                        End If

                        If Math.Round(CSng(Resero), 4) < 0.01 Then Resero = 15 * cachedENPOil(item.Key)
                        If Resero < 10 * cachedENPOil(item.Key) Then
                            Resero = 10 * cachedENPOil(item.Key)
                        End If

                        If ReserG < 0.01 Then ReserG = 15 * cachedENPGas(item.Key)
                        If ReserG < 10 * cachedENPGas(item.Key) Then
                            ReserG = 10 * cachedENPGas(item.Key)
                        End If

                        If ReserCoal < 0.02 Then ReserCoal = 20 * cachedENPCoal(item.Key)
                        'If item.Key = "Bosnia and Herzegovina" Then Stop 'should return 0.897
                        'If item.Key = "Cameroon" Then Stop 'should return 3.6339 E-5
                        'If item.Key = "Botswana" Then Stop 'should return 0.255
                        If ReserCoal < 30 * cachedENPCoal(item.Key) Then
                            'Bosnia ReserCoal = CSng(Math.Round(CSng(30 * cachedENPCoal(item.Key)), 7))
                            'Cameroon
                            ReserCoal = 30 * cachedENPCoal(item.Key)
                        End If

                        'If item.Key = "United States of America" Then Stop

                        'mti 2024/08/29 start

                        'ReserSolar = item.SolarPot
                        'If ReserSolar < 0 Then ReserSolar = 2 * cachedENPSOLAR(item.Key)
                        'If ReserSolar < 1.5 * cachedENPSOLAR(item.Key) Then
                        '    ReserSolar = CSng(1.5 * cachedENPSOLAR(item.Key))
                        'End If

                        'ReserWind = item.WindPot
                        'If ReserWind < 0 Then ReserWind = 2 * cachedENPWIND(item.Key)
                        'If ReserWind < 1.5 * cachedENPWIND(item.Key) Then
                        '    ReserWind = CSng(1.5 * cachedENPWIND(item.Key))
                        'End If

                        'ReserGeo = item.GeoPot
                        'If ReserGeo < 0 Then ReserGeo = 2 * cachedENPGEO(item.Key)
                        'If ReserGeo < 1.5 * cachedENPGEO(item.Key) Then
                        '    ReserGeo = CSng(1.5 * cachedENPGEO(item.Key))
                        'End If

                        'cachedRESSOLAR.Add(item.Key, ReserSolar)
                        'cachedRESWIND.Add(item.Key, ReserWind)
                        'cachedRESGEO.Add(item.Key, ReserGeo)
                        'mti 2024/08/29 end

                        cachedRESOIL.Add(item.Key, Resero)
                        cachedRESGAS.Add(item.Key, ReserG)

                        cachedRESCOAL.Add(item.Key, ReserCoal)
                        cachedRESHYD.Add(item.Key, ReserHyd)

                    Next
                End If 'End of If Hist else Forecast

                Dim yrsCOALBP As Short(), yrsCOALIEA As Short(), yrsCOAL As Short()
                Dim diceCOALBP As IDictionary(Of String, Single?()), diceCOALIEA As IDictionary(Of String, Single?()), diceCOAL As IDictionary(Of String, Single?())

                '       Production Growth Rates
                '       -----------------------
                '       NER% = 4   'Number of conventional energy types with resource bases
                '       NEU% = 6   'Number of energy types with resources, including unconventional, renew
                '       NEP% = 6   'Number of conventional energy production types, total
                '       NET% = 7   'Number of conventional and unconventional energy types, total
                If cntx.HistoricMode Then
                    Dim yrsEnPOilBP As Short() = cntx.GetYears("ENPOILBP").OrderBy(Function(y) y).ToArray()
                    Dim EnPOilBPTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPOILBP", yrsEnPOilBP)
                    Dim IntvlMinEnpOilBPHist = AMAX(2, Amin(10, (getInterval(EnPOilBPTbl, yrsEnPOilBP, cntx, True) - 2)))

                    Dim yrsEnPOilIEA As Short() = cntx.GetYears("ENPOILIEA").OrderBy(Function(y) y).ToArray()
                    Dim EnPOilIEATbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPOILIEA", yrsEnPOilIEA)
                    Dim IntvlMinEnpOilHist = AMAX(2, Amin(10, (getInterval(EnPOilIEATbl, yrsEnPOilIEA, cntx, True) - 2)))

                    Dim yrsEnPOil As Short() = cntx.GetYears("ENPOIL").OrderBy(Function(y) y).ToArray()
                    Dim EnPOilTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPOIL", yrsEnPOil)
                    Dim IntvlMinEnpOilIEAHist = AMAX(2, Amin(10, (getInterval(EnPOilTbl, yrsEnPOil, cntx, True) - 2)))

                    Dim yrsEnPGas As Short() = cntx.GetYears("ENPGAS").OrderBy(Function(y) y).ToArray()
                    Dim EnPGasTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPGAS", yrsEnPGas)
                    Dim IntvlMinEnpGasHist = AMAX(2, Amin(30, (getInterval(EnPGasTbl, yrsEnPGas, cntx, True) - 2)))

                    Dim yrsEnPGasBP As Short() = cntx.GetYears("ENPGASBP").OrderBy(Function(y) y).ToArray()
                    Dim EnPGasBPTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPGASBP", yrsEnPGasBP)
                    Dim IntvlMinEnpGasBPHist = AMAX(2, Amin(30, (getInterval(EnPGasBPTbl, yrsEnPGasBP, cntx, True) - 2)))

                    Dim yrsEnPCoalBP As Short() = cntx.GetYears("ENPCOALBP").OrderBy(Function(y) y).ToArray()
                    Dim EnPCoalBPTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPCOALBP", yrsEnPCoalBP)
                    Dim IntvlMinEnpCoalBPHist = AMAX(2, Amin(30, (getInterval(EnPCoalBPTbl, yrsEnPCoalBP, cntx, True) - 2)))

                    Dim yrsEnPCoalIEA As Short() = cntx.GetYears("ENPCOALIEA").OrderBy(Function(y) y).ToArray()
                    Dim EnPCoalIEATbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPCOALIEA", yrsEnPCoalIEA)
                    Dim IntvlMinEnpCoalIEAHist = AMAX(2, Amin(30, (getInterval(EnPCoalIEATbl, yrsEnPCoalIEA, cntx, True) - 2)))

                    Dim yrsEnPCoal As Short() = cntx.GetYears("ENPCOAL").OrderBy(Function(y) y).ToArray()
                    Dim EnPCoalTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPCOAL", yrsEnPCoal)
                    Dim IntvlMinEnpCoalHist = AMAX(2, Amin(30, (getInterval(EnPCoalTbl, yrsEnPCoal, cntx, True) - 2)))

                    Dim yrsEnPNuc As Short() = cntx.GetYears("ENPNUC").OrderBy(Function(y) y).ToArray()
                    Dim EnPNucTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPNUC", yrsEnPNuc)
                    Dim IntvlMinEnpNucHist = AMAX(2, Amin(30, (getInterval(EnPNucTbl, yrsEnPNuc, cntx, True) - 2)))

                    Dim yrsEnPNucBP As Short() = cntx.GetYears("ENPNUCBP").OrderBy(Function(y) y).ToArray()
                    Dim EnPNucBPTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPNUCBP", yrsEnPNucBP)
                    Dim IntvlMinEnpNucBPHist = AMAX(2, Amin(30, (getInterval(EnPNucBPTbl, yrsEnPNucBP, cntx, True) - 2)))

                    Dim yrsEnPHydBP As Short() = cntx.GetYears("ENPHYDBP").OrderBy(Function(y) y).ToArray()
                    Dim EnPHydBPTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPHYDBP", yrsEnPHydBP)
                    Dim IntvlMinEnpHydBPHist = AMAX(2, Amin(30, (getInterval(EnPHydBPTbl, yrsEnPHydBP, cntx, True) - 2)))

                    Dim yrsEnPHydIEA As Short() = cntx.GetYears("ENPHYD").OrderBy(Function(y) y).ToArray()
                    Dim EnPHydIEATbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPHYD", yrsEnPHydIEA)
                    Dim IntvlMinEnpHydIEAHist = AMAX(2, Amin(30, (getInterval(EnPHydIEATbl, yrsEnPHydIEA, cntx, True) - 2)))

                    Dim yrsEnPHyd As Short() = cntx.GetYears("ENPHYD").OrderBy(Function(y) y).ToArray()
                    Dim EnPHydTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPHYD", yrsEnPHyd)
                    Dim IntvlMinEnpHydHist = AMAX(2, Amin(30, (getInterval(EnPHydTbl, yrsEnPHyd, cntx, True) - 2)))

                    'mti 2024/07/23  start
                    Dim yrsEnPGeo As Short() = cntx.GetYears("ENPGEOIEA").OrderBy(Function(y) y).ToArray()
                    Dim EnPGeoTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPGEOIEA", yrsEnPGeo)
                    Dim IntvlMinEnpGeoHist = AMAX(2, Amin(10, (getInterval(EnPGeoTbl, yrsEnPGeo, cntx, True) - 2)))

                    Dim yrsEnPPhoto As Short() = cntx.GetYears("ENPPHOTOIEA").OrderBy(Function(y) y).ToArray()
                    Dim EnPPhotoTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPPHOTOIEA", yrsEnPPhoto)
                    Dim IntvlMinEnpPhotoHist = AMAX(2, Amin(10, (getInterval(EnPPhotoTbl, yrsEnPPhoto, cntx, True) - 2)))

                    Dim yrsEnPSolar As Short() = cntx.GetYears("ENPSOLARIEA").OrderBy(Function(y) y).ToArray()
                    Dim EnPSolarTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPSOLARIEA", yrsEnPSolar)
                    Dim IntvlMinEnpSolarHist = AMAX(2, Amin(10, (getInterval(EnPSolarTbl, yrsEnPSolar, cntx, True) - 2)))

                    'jrs 2024/12/03 need uppercase
                    Dim yrsEnPWind As Short() = cntx.GetYears("ENPWINDIEA").OrderBy(Function(y) y).ToArray()
                    Dim EnPWindTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPWINDIEA", yrsEnPWind)
                    Dim IntvlMinEnpWindHist = AMAX(2, Amin(10, (getInterval(EnPWindTbl, yrsEnPWind, cntx, True) - 2)))

                    Dim yrsEnPTidal As Short() = cntx.GetYears("ENPTIDEWAVEIEA").OrderBy(Function(y) y).ToArray()
                    Dim EnPTidalTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPTIDEWAVEIEA", yrsEnPTidal)
                    Dim IntvlMinEnpTidalHist = AMAX(2, Amin(10, (getInterval(EnPTidalTbl, yrsEnPTidal, cntx, True) - 2)))

                    Dim yrsEnPBioDiesel As Short() = cntx.GetYears("ENPBIODIESELIEA").OrderBy(Function(y) y).ToArray()
                    Dim EnPBioDieselTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPBIODIESELIEA", yrsEnPBioDiesel)
                    Dim IntvlMinEnpBioDieselHist = AMAX(2, Amin(30, (getInterval(EnPBioDieselTbl, yrsEnPBioDiesel, cntx, True) - 2)))

                    Dim yrsEnPBioGas As Short() = cntx.GetYears("ENPBIOGASIEA").OrderBy(Function(y) y).ToArray()
                    Dim EnPBioGasTbl As IDictionary(Of String, Single?()) = cntx.GetDice("ENPBIOGASIEA", yrsEnPBioGas)
                    Dim IntvlMinEnpBioGasHist = AMAX(2, Amin(10, (getInterval(EnPBioGasTbl, yrsEnPBioGas, cntx, True) - 2)))

                    'mti 2024/07/23  end

                    For Each key In cntx.Regionalization.Keys
                        cachedENPRRRen(key) = 0.025
                        cachedENPRROil.Add(key, getAnnualGrowthRateFwd(EnPOilBPTbl, yrsEnPOilBP, key, cntx.FirstYear, 1, -0.05, IntvlMinEnpOilBPHist, IntvlMinEnpOilBPHist + 1, 2, 100))
                        '(filename, base year, max growth rate, min growth rate, growth interval min, growth interval max,
                        ' max years to go back, max years to go forward)
                        If Not cachedENPRROil(key).HasValue Then
                            'CENPRROil(ICount%) = getAnnualGrowthRate(EnPOilIEATbl, FirstYearHistory, 10, -10, 5, 10, 15, 5)
                            cachedENPRROil(key) = getAnnualGrowthRateFwd(EnPOilIEATbl, yrsEnPOilIEA, key, cntx.FirstYear, 1, -0.05, IntvlMinEnpOilHist, IntvlMinEnpOilHist + 1, 2, 100)
                        End If
                        If Not cachedENPRROil(key).HasValue Then
                            'CENPRROil(ICount%) = getAnnualGrowthRate(EnPOilTbl, FirstYearHistory, 10, -10, 5, 10, 15, 5)
                            cachedENPRROil(key) = getAnnualGrowthRateFwd(EnPOilTbl, yrsEnPOil, key, cntx.FirstYear, 1, -0.05, IntvlMinEnpOilIEAHist, IntvlMinEnpOilIEAHist + 1, 2, 100)
                        End If
                        If Not cachedENPRROil(key).HasValue Then
                            cachedENPRROil(key) = CSng(0.15 -
                                0.15 * Amin(1, cachedENPOil(key) * 8 / cachedRESOIL(key)))
                        End If

                        'earlier code used longer (30 years plus) interval
                        cachedENPRRGas(key) = getAnnualGrowthRateFwd(EnPGasTbl, yrsEnPGas, key, cntx.FirstYear, 10, -10, IntvlMinEnpGasHist, IntvlMinEnpGasHist + 1, 5, 100)
                        'CENPRRGas(ICount%) = getAnnualGrowthRate(EnPGasTbl, FirstYearHistory, 10, -10, 10, 10, 15, 50)

                        'getAnnualGrowthRate(SanitationCatHCTbl, yrsSanHC, key, cntx.FirstYear, 0.1, -0.02, 4, 5, 10, 10))


                        '(filename, base year, max growth rate, min growth rate, growth interval min, growth interval max,
                        ' max years to go back, max years to go forward)
                        If Not cachedENPRRGas(key).HasValue Then
                            'CENPRRGas(ICount%) = getAnnualGrowthRate(EnPGasBPTbl, FirstYearHistory, 10, -10, 5, 10, 15, 20)
                            cachedENPRRGas(key) = getAnnualGrowthRateFwd(EnPGasBPTbl, yrsEnPGasBP, key, cntx.FirstYear, 10, -10, IntvlMinEnpGasBPHist, IntvlMinEnpGasBPHist + 1, 5, 100)
                        End If
                        If Not cachedENPRRGas(key).HasValue Then
                            cachedENPRRGas(key) = CSng(0.1 -
                    0.1 * Amin(1, cachedENPGas(key) * 8 / cachedRESGAS(key)))
                        End If

                        cachedENPRRCoal(key) = getAnnualGrowthRateFwd(EnPCoalBPTbl, yrsEnPCoalBP, key, cntx.FirstYear, 10, -10, IntvlMinEnpCoalBPHist, IntvlMinEnpCoalBPHist + 1, 5, 100) 'fix later note mti: BP starts 1981; won't read BP for 1960 base
                        '(filename, base year, max growth rate, min growth rate, growth interval min, growth interval max,
                        ' max years to go back, max years to go forward)
                        If Not cachedENPRRCoal(key).HasValue Then
                            'CENPRRCoal(ICount%) = getAnnualGrowthRate(EnPCoalIEATbl, FirstYearHistory, 10, -10, 5, 10, 15, 15)
                            cachedENPRRCoal(key) = getAnnualGrowthRateFwd(EnPCoalIEATbl, yrsEnPCoalIEA, key, cntx.FirstYear, 10, -10, IntvlMinEnpCoalIEAHist, IntvlMinEnpCoalIEAHist + 1, 5, 100)
                        End If
                        If Not cachedENPRRCoal(key).HasValue Then
                            'CENPRRCoal(ICount%) = getAnnualGrowthRate(EnPCoalTbl, FirstYearHistory, 10, -10, 5, 10, 15, 15)
                            cachedENPRRCoal(key) = getAnnualGrowthRateFwd(EnPCoalTbl, yrsEnPCoal, key, cntx.FirstYear, 10, -10, IntvlMinEnpCoalHist, IntvlMinEnpCoalHist + 1, 5, 100)
                        End If
                        If Not cachedENPRRCoal(key).HasValue Then
                            cachedENPRRCoal(key) = CSng(0.02 -
                    0.02 * Amin(1, cachedENPCoal(key) * 15 / cachedRESCOAL(key)))
                        End If

                        cachedENPRRNuc(key) = getAnnualGrowthRateFwd(EnPNucTbl, yrsEnPNuc, key, cntx.FirstYear, 10, -10, IntvlMinEnpNucHist, IntvlMinEnpNucHist + 1, 5, 100)
                        '(filename, base year, max growth rate, min growth rate, growth interval min, growth interval max, max years to go back, max years to go forward)
                        If cachedENPRRNuc(key).HasValue Then
                            cachedENPRRNuc(key) = cachedENPRRNuc(key) / 2     'High values from low base cannot be maintained
                        Else
                            cachedENPRRNuc(key) = getAnnualGrowthRateFwd(EnPNucBPTbl, yrsEnPNucBP, key, cntx.FirstYear, 10, -10, IntvlMinEnpNucBPHist, IntvlMinEnpNucBPHist + 1, 5, 100)
                        End If

                        If cachedENPRRNuc(key).HasValue Then
                            cachedENPRRNuc(key) = cachedENPRRNuc(key) / 2           'High values from low base cannot be maintained
                        Else
                            cachedENPRRNuc(key) = 0.02
                        End If
                        cachedENPRRHyd(key) = getAnnualGrowthRateFwd(EnPHydBPTbl, yrsEnPHydBP, key, cntx.FirstYear, 10, -10, IntvlMinEnpHydBPHist, IntvlMinEnpHydBPHist + 1, 5, 100)
                        '(filename, base year, max growth rate, min growth rate, growth interval min, growth interval max, max years to go back, max years to go forward)
                        If Not cachedENPRRHyd(key).HasValue Then
                            cachedENPRRHyd(key) = getAnnualGrowthRateFwd(EnPHydIEATbl, yrsEnPHydIEA, key, cntx.FirstYear, 10, -10, IntvlMinEnpHydIEAHist, IntvlMinEnpHydIEAHist + 1, 5, 100)
                        End If
                        If Not cachedENPRRHyd(key).HasValue Then
                            cachedENPRRHyd(key) = getAnnualGrowthRateFwd(EnPHydTbl, yrsEnPHyd, key, cntx.FirstYear, 10, -10, IntvlMinEnpHydHist, IntvlMinEnpHydHist + 1, 5, 100)
                        End If
                        If Not cachedENPRRHyd(key).HasValue Then
                            cachedENPRRHyd(key) = 0.01
                        End If

                        'mti 2024/07/23 start 
                        'If key = "United States of America" Then Stop

                        Dim PhotoRR = getAnnualGrowthRateFwd(EnPPhotoTbl, yrsEnPPhoto, key, cntx.FirstYear, 0.05, -0.05, IntvlMinEnpPhotoHist, IntvlMinEnpPhotoHist + 1, 5, 100)
                        Dim SolarRR = getAnnualGrowthRateFwd(EnPSolarTbl, yrsEnPSolar, key, cntx.FirstYear, 0.05, -0.05, IntvlMinEnpSolarHist, IntvlMinEnpSolarHist + 1, 5, 100)

                        If PhotoRR.HasValue And SolarRR.HasValue Then
                            cachedENPRRSOLAR(key) = CSng((PhotoRR + SolarRR) / 2)
                        ElseIf PhotoRR.HasValue Then
                            cachedENPRRSOLAR(key) = CSng(PhotoRR)
                        ElseIf SolarRR.HasValue Then
                            cachedENPRRSOLAR(key) = CSng(SolarRR)
                        Else
                            cachedENPRRSOLAR(key) = 0.001
                        End If


                        cachedENPRRWIND(key) = getAnnualGrowthRateFwd(EnPWindTbl, yrsEnPWind, key, cntx.FirstYear, 0.05, -0.05, IntvlMinEnpWindHist, IntvlMinEnpWindHist + 1, 5, 100)
                        If Not cachedENPRRWIND(key).HasValue Then
                            cachedENPRRWIND(key) = 0.001
                        End If

                        cachedENPRRGEOTHERMAL(key) = getAnnualGrowthRateFwd(EnPGeoTbl, yrsEnPGeo, key, cntx.FirstYear, 0.05, -0.05, IntvlMinEnpGeoHist, IntvlMinEnpGeoHist + 1, 5, 100)

                        If Not cachedENPRRGEOTHERMAL(key).HasValue Then
                            cachedENPRRGEOTHERMAL(key) = 0.001
                        End If

                        Dim TidalRR = getAnnualGrowthRateFwd(EnPTidalTbl, yrsEnPTidal, key, cntx.FirstYear, 0.05, -0.05, IntvlMinEnpTidalHist, IntvlMinEnpTidalHist + 1, 5, 100)
                        Dim BioDieselRR = getAnnualGrowthRateFwd(EnPBioDieselTbl, yrsEnPBioDiesel, key, cntx.FirstYear, 0.05, -0.05, IntvlMinEnpBioDieselHist, IntvlMinEnpBioDieselHist + 1, 5, 100)
                        Dim BioGasRR = getAnnualGrowthRateFwd(EnPBioGasTbl, yrsEnPBioGas, key, cntx.FirstYear, 0.05, -0.05, IntvlMinEnpBioGasHist, IntvlMinEnpBioGasHist + 1, 5, 100)

                        If TidalRR.HasValue And BioDieselRR.HasValue And BioGasRR.HasValue Then
                            cachedENPRRRen(key) = CSng((TidalRR + BioDieselRR + BioGasRR) / 3)
                        ElseIf TidalRR.HasValue And (BioDieselRR.HasValue Or BioGasRR.HasValue) Then
                            cachedENPRRRen(key) = CSng((TidalRR + If(BioDieselRR, BioGasRR)) / 2)
                        ElseIf BioDieselRR.HasValue Then  'avoiding further combo of two to keep code simple; rather prioritizing
                            cachedENPRRRen(key) = CSng(BioDieselRR)
                        ElseIf BioGasRR.HasValue Then
                            cachedENPRRRen(key) = CSng(BioGasRR)
                        ElseIf TidalRR.HasValue Then
                            cachedENPRRRen(key) = CSng(TidalRR)
                        Else
                            cachedENPRRRen(key) = 0.001
                        End If

                        'mti 2024/07/23 end 

                        'mti 2019/05/14 End
                    Next
                    'Dim COilBP1970 As Single?, COilBP1965 As Single?, COil1965 As Single?, COil1960 As Single?, COilIEA1976 As Single?, COilIEA1971 As Single?
                    'Dim CGasBP1980 As Single?, CGasBP1970 As Single?, CGas1970 As Single?, CGas1960 As Single?
                    'Dim CCoalBP1990 As Single?, CCoalBP1981 As Single?, CCoal1970 As Single?, CCoal1960 As Single?, CCoalIEA1990 As Single?, CCoalIEA1980 As Single?
                    'Dim CNucBPBaseYr As Single?, CNucBP1970 As Single?, CNuc1995 As Single?, CNuc1960 As Single?
                    'Dim CHydBPBaseYr As Single?, CHydBP1965 As Single?, CHydBaseYr As Single?, CHyd1971 As Single?, CHydIEABaseYr As Single?, CHydIEA1971 As Single?
                    'For Each item In (From obp In cntx.GetSlice("ENPOILBP", 1970)
                    '                  Join obp2 In cntx.GetSlice("ENPOILBP", 1965) On obp.Key Equals obp2.Key
                    '                  Join oil In cntx.GetSlice("ENPOIL", 1965) On obp.Key Equals oil.Key
                    '                  Join oil2 In cntx.GetSlice("ENPOIL", 1960) On obp.Key Equals oil2.Key
                    '                  Join oiea In cntx.GetSlice("ENPOILIEA", 1976) On obp.Key Equals oiea.Key
                    '                  Join oiea2 In cntx.GetSlice("ENPOILIEA", 1971) On obp.Key Equals oiea2.Key
                    '                  Join gas In cntx.GetSlice("ENPGAS", 1970) On obp.Key Equals gas.Key
                    '                  Join gas2 In cntx.GetSlice("ENPGAS", 1960) On obp.Key Equals gas2.Key
                    '                  Join gbp In cntx.GetSlice("ENPGASBP", 1980) On obp.Key Equals gbp.Key
                    '                  Join gbp2 In cntx.GetSlice("ENPGASBP", 1970) On obp.Key Equals gbp2.Key
                    '                  Join coal In cntx.GetSlice("ENPCOAL", 1970) On obp.Key Equals coal.Key
                    '                  Join coal2 In cntx.GetSlice("ENPCOAL", 1960) On obp.Key Equals coal2.Key
                    '                  Join cbp In cntx.GetSlice("ENPCOALBP", 1990) On obp.Key Equals cbp.Key
                    '                  Join cbp2 In cntx.GetSlice("ENPCOALBP", 1981) On obp.Key Equals cbp2.Key
                    '                  Join ciea In cntx.GetSlice("ENPCOALIEA", 1990) On obp.Key Equals ciea.Key
                    '                  Join ciea2 In cntx.GetSlice("ENPCOALIEA", 1980) On obp.Key Equals ciea2.Key
                    '                  Join nuc In cntx.GetSlice("ENPNUC", 1995) On obp.Key Equals nuc.Key
                    '                  Join nuc2 In cntx.GetSlice("ENPNUC", 1960) On obp.Key Equals nuc2.Key
                    '                  Join nbp In cntx.GetSlice("ENPNUCBP", cntx.LastYear) On obp.Key Equals nbp.Key
                    '                  Join nbp2 In cntx.GetSlice("ENPNUCBP", 1970) On obp.Key Equals nbp2.Key
                    '                  Join hyd In cntx.GetSlice("ENPHYD", cntx.LastYear) On obp.Key Equals hyd.Key
                    '                  Join hyd2 In cntx.GetSlice("ENPHYD", 1971) On obp.Key Equals hyd2.Key
                    '                  Join hbp In cntx.GetSlice("ENPHYDBP", cntx.LastYear) On obp.Key Equals hbp.Key
                    '                  Join hbp2 In cntx.GetSlice("ENPHYDBP", 1965) On obp.Key Equals hbp2.Key
                    '                  Select Key = obp.Key, obp70 = obp.Value, obp65 = obp2.Value, oil65 = oil.Value, oil60 = oil2.Value,
                    '                  oiea76 = oiea.Value, oiea71 = oiea2.Value, gas70 = gas.Value, gas60 = gas2.Value, gbp80 = gbp.Value, gbp70 = gbp2.Value,
                    '                    coal70 = coal.Value, coal60 = coal2.Value, cbp90 = cbp.Value, cbp81 = cbp2.Value, ciea90 = ciea.Value, ciea80 = ciea2.Value,
                    '                    nuc95 = nuc.Value, nuc60 = nuc2.Value, nbpBYR = nbp.Value, nbp70 = nbp2.Value,
                    'hydBYR = hyd.Value, hyd71 = hyd2.Value, hbpBYR = hbp.Value, hbp65 = hbp2.Value)

                    '    cachedENPRRRen(item.Key) = 0.025
                    '    COilBP1970 = item.obp70
                    '    COilBP1965 = item.obp65
                    '    COil1965 = item.oil65
                    '    COil1960 = item.oil60
                    '    COilIEA1976 = item.oiea76
                    '    COilIEA1971 = item.oiea71
                    '    If item.Key = "Oman" Then Stop
                    '    If COilBP1970.HasValue And COilBP1965.HasValue And COilBP1965 > 0.001 Then
                    '        cachedENPRROil.Add(item.Key, CSng((COilBP1970 / COilBP1965) ^ (1 / 5) - 1))
                    '    ElseIf COilIEA1976.HasValue And COilIEA1971.HasValue And COilIEA1971 > 0.001 Then
                    '        cachedENPRROil.Add(item.Key, CSng((COilIEA1976 / COilIEA1971) ^ (1 / 5) - 1))
                    '    ElseIf COil1965.HasValue And COil1960.HasValue And COil1960 > 0.001 Then
                    '        cachedENPRROil.Add(item.Key, CSng((COil1965 / COil1960) ^ (1 / 5) - 1))
                    '    Else
                    '        cachedENPRROil.Add(item.Key, CSng(0.15 -
                    '        0.15 * Amin(1, cachedENPOil(item.Key) * 8 / cachedRESOIL(item.Key))))
                    '    End If

                    '    CGasBP1980 = item.gbp80
                    '    CGasBP1970 = item.gbp70
                    '    CGas1970 = item.gas70
                    '    CGas1960 = item.gas60

                    '    If CGas1970.HasValue And CGas1960.HasValue And CGas1960 > 0.001 Then
                    '        cachedENPRRGas.Add(item.Key, CSng((CGas1970 / CGas1960) ^ (1 / 10) - 1))
                    '    ElseIf CGasBP1980.HasValue And CGasBP1970.HasValue And CGasBP1970 > 0.001 Then
                    '        cachedENPRRGas.Add(item.Key, CSng((CGasBP1980 / CGasBP1970) ^ (1 / 10) - 1))
                    '    Else
                    '        cachedENPRRGas.Add(item.Key, CSng(0.1 -
                    '    0.1 * Amin(1, cachedENPGas(item.Key) * 8 / cachedRESGAS(item.Key))))
                    '    End If

                    '    CCoalBP1990 = item.cbp90
                    '    CCoalBP1981 = item.cbp81
                    '    CCoal1970 = item.coal70
                    '    CCoal1960 = item.coal60
                    '    CCoalIEA1990 = item.ciea90
                    '    CCoalIEA1980 = item.ciea80

                    '    If CCoalBP1990.HasValue And CCoalBP1981.HasValue And CCoalBP1981 > 0.001 Then
                    '        cachedENPRRCoal.Add(item.Key, CSng((CCoalBP1990 / CCoalBP1981) ^ (1 / 9) - 1))
                    '    ElseIf CCoalIEA1990.HasValue And CCoalIEA1980.HasValue And CCoalIEA1980 > 0.001 Then
                    '        cachedENPRRCoal.Add(item.Key, CSng((CCoalIEA1990 / CCoalIEA1980) ^ (1 / 10) - 1))
                    '    ElseIf CCoal1970.HasValue And CCoal1960.HasValue And CCoal1960 > 0.001 Then
                    '        cachedENPRRCoal.Add(item.Key, CSng((CCoal1970 / CCoal1960) ^ (1 / 10) - 1))
                    '    Else
                    '        cachedENPRRCoal.Add(item.Key, CSng(0.02 -
                    '    0.02 * Amin(1, cachedENPCoal(item.Key) * 15 / cachedRESCOAL(item.Key))))
                    '    End If

                    '    CNucBPBaseYr = item.nbpBYR
                    '    CNucBP1970 = item.nbp70
                    '    CNuc1995 = item.nuc95
                    '    CNuc1960 = item.nuc60

                    '    If CNuc1995.HasValue And CNuc1960.HasValue And CNuc1960 > 0.001 Then
                    '        cachedENPRRNuc.Add(item.Key, CSng((CNuc1995 / CNuc1960) ^ (1 / 35) - 1) / 2.0F)
                    '        'High values from low base cannot be maintained
                    '        'jrs 2006/12/18 ElseIf Not (IsNull(CNucBP2000)) And Not (IsNull(CNucBP1970)) And CNucBP1970 > 0.001 Then
                    '    ElseIf CNucBPBaseYr.HasValue And CNucBP1970.HasValue And CNucBP1970 > 0.001 Then
                    '        'jrs 2006/12/18 CENPRRNuc(item.key) = (CNucBP2000 / CNucBP1970) ^ (1 / 30) - 1
                    '        cachedENPRRNuc.Add(item.Key, CSng((CNucBPBaseYr / CNucBP1970) ^ (1 / (cntx.LastYear - 1970)) - 1) / 2.0F)
                    '    Else
                    '        cachedENPRRNuc.Add(item.Key, 0.02F)
                    '    End If

                    '    CHydBPBaseYr = item.hbpBYR
                    '    CHydBP1965 = item.hbp65
                    '    CHydBaseYr = item.hydBYR
                    '    CHyd1971 = item.hyd71
                    '    CHydIEABaseYr = item.hydBYR
                    '    CHydIEA1971 = item.hyd71

                    '    If CHydBPBaseYr.HasValue And CHydBP1965.HasValue And CHydBP1965 > 0.001 Then
                    '        'jrs 2006/12/18 CENPRRHyd(item.key) = (CHydBP2000 / CHydBP1965) ^ (1 / 35) - 1
                    '        cachedENPRRHyd.Add(item.Key, CSng((CHydBPBaseYr / CHydBP1965) ^ (1 / (cntx.LastYear - 1965)) - 1))
                    '        'jrs 2006/12/18 ElseIf Not (IsNull(CHyd2000)) And Not (IsNull(CHyd1971)) And CHyd1971 > 0.001 Then
                    '    ElseIf CHydIEABaseYr.HasValue And CHydIEA1971.HasValue And CHydIEA1971 > 0.001 Then
                    '        'jrs 2006/12/18 CENPRRHyd(item.key) = (CHyd2000 / CHyd1971) ^ (1 / 29) - 1
                    '        cachedENPRRHyd.Add(item.Key, CSng((CHydIEABaseYr / CHydIEA1971) ^ (1 / (cntx.LastYear - 1971)) - 1))
                    '        'jrs 2006/12/18 If Not (IsNull(CHydBP2000)) And Not (IsNull(CHydBP1965)) And CHydBP1965 > 0.001 Then
                    '    ElseIf CHydBaseYr.HasValue And CHyd1971.HasValue And CHyd1971 > 0.001 Then
                    '        'jrs 2006/12/18 CENPRRHyd(item.key) = (CHyd2000 / CHyd1971) ^ (1 / 29) - 1
                    '        cachedENPRRHyd.Add(item.Key, CSng((CHydBaseYr / CHyd1971) ^ (1 / (cntx.LastYear - 1971)) - 1))
                    '    Else
                    '        cachedENPRRHyd.Add(item.Key, 0.01F)
                    '    End If

                    'Next
                Else 'Forecast Mode
                    Dim ENPRROilEndingYear As Short = 2100
                    Dim ENPRROilInterval As Short = 5
                    Dim EnpOilEndingYear As Single?
                    Dim years As IList(Of Short)
                    years = (From y In cntx.GetYears("ENPOILBP")
                             Where cntx.FirstYear - 10S < y
                             Order By y Descending
                    ).Take(ENPRROilInterval + 1).ToList()
                    Dim ENPRROilMostRecentYear As Short, ENPRROilBPMostRecentYear As Short, ENPRROilIEAMostRecentYear As Short
                    Dim EnpOilBeginYear As Single?, EnpOilBPBeginYear As Short, EnpOilIEABeginYear As Short
                    ENPRROilBPMostRecentYear = years(0)
                    EnpOilBPBeginYear = years(Amin(ENPRROilInterval, years.Count - 1)) 'jrs 2022/03/01 added amin( , years.count -1)
                    Dim ENPRROilData As Short
                    years = (From y In cntx.GetYears("ENPOILIEA")
                             Where cntx.FirstYear - 10S < y
                             Order By y Descending
                    ).Take(ENPRROilInterval + 1).ToList()
                    ENPRROilIEAMostRecentYear = years(0)
                    EnpOilIEABeginYear = years(Amin(ENPRROilInterval, years.Count - 1)) 'jrs 2022/03/01 added amin( , years.count -1)
                    Dim ENPRROilGap As Short
                    'Load entire table for Gas BP
                    Dim yrsGASBP As Short() = cntx.GetYears("ENPGASBP").OrderByDescending(Function(y) y).ToArray()
                    Dim diceGASBP As IDictionary(Of String, Single?()) = (
                    From yr In yrsGASBP From kv In cntx.GetSlice("ENPGASBP", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )
                    'Load entire table for Gas IEA
                    Dim yrsGASIEA As Short() = cntx.GetYears("ENPGASIEA").OrderByDescending(Function(y) y).ToArray()
                    Dim diceGASIEA As IDictionary(Of String, Single?()) = (
                    From yr In yrsGASIEA From kv In cntx.GetSlice("ENPGASIEA", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )

                    Dim GasMRYr As Integer?, GasMRm5Yr As Integer?, GasMRValue As Single?, GasMRm5Value As Single?
                    Dim GasMRYrIdx As Integer?, GasMRm5YrIdx As Integer?

                    'Load entire table for Coal BP
                    yrsCOALBP = cntx.GetYears("ENPCOALBP").OrderByDescending(Function(y) y).ToArray()
                    diceCOALBP = (
                    From yr In yrsCOALBP From kv In cntx.GetSlice("ENPCOALBP", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )
                    'Load entire table for COAL IEA
                    yrsCOALIEA = cntx.GetYears("ENPCOALIEA").OrderByDescending(Function(y) y).ToArray()
                    diceCOALIEA = (
                    From yr In yrsCOALIEA From kv In cntx.GetSlice("ENPCOALIEA", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )

                    Dim CoalMRYr As Integer?, CoalMRm5Yr As Integer?, CoalMRValue As Single?, CoalMRm5Value As Single?
                    Dim CoalMRYrIdx As Integer?, CoalMRm5YrIdx As Integer?

                    'Load entire table for Hyd BP
                    Dim yrsHYDBP As Short() = cntx.GetYears("ENPHYDBP").OrderByDescending(Function(y) y).ToArray()
                    Dim diceHYDBP As IDictionary(Of String, Single?()) = (
                    From yr In yrsHYDBP From kv In cntx.GetSlice("ENPHYDBP", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )
                    'Load entire table for HYD IEA
                    Dim yrsHYDIEA As Short() = cntx.GetYears("ENPHYD").OrderByDescending(Function(y) y).ToArray()
                    Dim diceHYDIEA As IDictionary(Of String, Single?()) = (
                    From yr In yrsHYDIEA From kv In cntx.GetSlice("ENPHYD", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )

                    Dim HydMRYr As Integer?, HydMRm5Yr As Integer?, HydMRValue As Single?, HydMRm5Value As Single?
                    Dim HydMRYrIdx As Integer?, HydMRm5YrIdx As Integer?

                    'Load entire table for Nuc BP
                    Dim yrsNUCBP As Short() = cntx.GetYears("ENPNUCBP").OrderByDescending(Function(y) y).ToArray()
                    Dim diceNUCBP As IDictionary(Of String, Single?()) = (
                    From yr In yrsNUCBP From kv In cntx.GetSlice("ENPNUCBP", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )

                    Dim NucMRYr As Integer?, NucMRm5Yr As Integer?, NucMRValue As Single?, NucMRm5Value As Single?
                    Dim NucMRYrIdx As Integer?, NucMRm5YrIdx As Integer?

                    'Load entire table for Solar IEA
                    Dim yrsSOLARIEA As Short() = cntx.GetYears("ENPSOLARIEA").OrderByDescending(Function(y) y).ToArray()
                    Dim diceSOLARIEA As IDictionary(Of String, Single?()) = (
                    From yr In yrsSOLARIEA From kv In cntx.GetSlice("ENPSOLARIEA", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )

                    Dim SolarMRYr As Integer?, SolarMRm5Yr As Integer?, SolarMRValue As Single?, SolarMRm5Value As Single?
                    Dim SolarMRYrIdx As Integer?, SolarMRm5YrIdx As Integer?

                    'Load entire table for Geo IEA
                    Dim yrsGEOIEA As Short() = cntx.GetYears("ENPGEOIEA").OrderByDescending(Function(y) y).ToArray()
                    Dim diceGEOIEA As IDictionary(Of String, Single?()) = (
                    From yr In yrsGEOIEA From kv In cntx.GetSlice("ENPGEOIEA", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )

                    Dim GeoMRYr As Integer?, GeoMRm5Yr As Integer?, GeoMRValue As Single?, GeoMRm5Value As Single?
                    Dim GeoMRYrIdx As Integer?, GeoMRm5YrIdx As Integer?

                    'Load entire table for TideWave IEA
                    Dim yrsTIDEWAVEIEA As Short() = cntx.GetYears("ENPTIDEWAVEIEA").OrderByDescending(Function(y) y).ToArray()
                    Dim diceTIDEWAVEIEA As IDictionary(Of String, Single?()) = (
                    From yr In yrsTIDEWAVEIEA From kv In cntx.GetSlice("ENPTIDEWAVEIEA", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )

                    Dim TideWaveMRYr As Integer?, TideWaveMRm5Yr As Integer?, TideWaveMRValue As Single?, TideWaveMRm5Value As Single?
                    Dim TideWaveMRYrIdx As Integer?, TideWaveMRm5YrIdx As Integer?

                    'Load entire table for Photo IEA
                    Dim yrsPHOTOIEA As Short() = cntx.GetYears("ENPPHOTOIEA").OrderByDescending(Function(y) y).ToArray()
                    Dim dicePHOTOIEA As IDictionary(Of String, Single?()) = (
                    From yr In yrsPHOTOIEA From kv In cntx.GetSlice("ENPPHOTOIEA", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )

                    Dim PhotoMRYr As Integer?, PhotoMRm5Yr As Integer?, PhotoMRValue As Single?, PhotoMRm5Value As Single?
                    Dim PhotoMRYridx As Integer?, PhotoMRm5Yridx As Integer?

                    'Load entire table for Wind IEA
                    Dim yrsWindIEA As Short() = cntx.GetYears("ENPWINDIEA").OrderByDescending(Function(y) y).ToArray()
                    Dim diceWindIEA As IDictionary(Of String, Single?()) = (
                    From yr In yrsWindIEA From kv In cntx.GetSlice("ENPWINDIEA", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )

                    Dim WindMRYr As Integer?, WindMRm5Yr As Integer?, WindMRValue As Single?, WindMRm5Value As Single?
                    Dim WindMRYridx As Integer?, WindMRm5Yridx As Integer?

                    'Load entire table for BioDiesel IEA
                    Dim yrsBIODIESELIEA As Short() = cntx.GetYears("ENPBIODIESELIEA").OrderByDescending(Function(y) y).ToArray()
                    Dim diceBIODIESELIEA As IDictionary(Of String, Single?()) = (
                    From yr In yrsBIODIESELIEA From kv In cntx.GetSlice("ENPBIODIESELIEA", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )

                    Dim BioDieselMRYr As Integer?, BioDieselMRm5Yr As Integer?, BioDieselMRValue As Single?, BioDieselMRm5Value As Single?
                    Dim BioDieselMRYridx As Integer?, BioDieselMRm5Yridx As Integer?

                    'Load entire table for BioGas IEA
                    Dim yrsBIOGASIEA As Short() = cntx.GetYears("ENPBIOGASIEA").OrderByDescending(Function(y) y).ToArray()
                    Dim diceBIOGASIEA As IDictionary(Of String, Single?()) = (
                    From yr In yrsBIOGASIEA From kv In cntx.GetSlice("ENPBIOGASIEA", yr, CoalescePolicy.Null)
                    Group kv.Value By Country = kv.Key Into Data = Group
                ).ToDictionary(
                        Function(grp) grp.Country,
                        Function(grp) grp.Data.ToArray()
                )
                    Dim BioGasMRYr As Integer?, BioGasMRm5Yr As Integer?, BioGasMRValue As Single?, BioGasMRm5Value As Single?
                    Dim BioGasMRYridx As Integer?, BioGasMRm5Yridx As Integer?

                    'Renewables
                    Dim V2010Sum As Single?, V2005Sum As Single?, Yrs As Single?, YrCount As Short

                    For Each item In (From obp In cntx.GetSlice("ENPOILBP", ENPRROilEndingYear, MyBase.Policy)
                                      Join obp2 In cntx.GetSlice("ENPOILBP", EnpOilBPBeginYear) On obp.Key Equals obp2.Key
                                      Join oil In cntx.GetSlice("ENPOIL", 1965) On obp.Key Equals oil.Key
                                      Join oil2 In cntx.GetSlice("ENPOIL", 1960) On obp.Key Equals oil2.Key
                                      Join oiea In cntx.GetSlice("ENPOILIEA", ENPRROilEndingYear, MyBase.Policy) On obp.Key Equals oiea.Key
                                      Join oiea2 In cntx.GetSlice("ENPOILIEA", EnpOilIEABeginYear) On obp.Key Equals oiea2.Key
                                      Select Key = obp.Key, obpLYR = obp.Value, obpBYR = obp2.Value, oil65 = oil.Value, oil60 = oil2.Value,
                                      oieaLYR = oiea.Value, oieaBYR = oiea2.Value)
                        'sgh 2013/09/03
                        'Energy production growth rate (ENPRR) is initialized using historical data to calculate a
                        'compound annual growth rate. This requires two years of data and an interval. In the code
                        'below, the ending year is found using the getYearOrMstRct function. Thus, if the
                        'ENPRROilEndingYear initializtion below is later than the base year, the calculation will use
                        'the most recent year. To use a year earlier than the most recent to calculate ENPRR you may
                        'change ENPRROilEndingYear. Likewise, the time interval can be changed using the ENPRROilInterval
                        'initialization below.
                        EnpOilEndingYear = item.obpLYR

                        If EnpOilEndingYear.HasValue Then
                            ENPRROilMostRecentYear = ENPRROilBPMostRecentYear
                            ENPRROilData = 1
                        Else
                            EnpOilEndingYear = item.oieaLYR
                            If EnpOilEndingYear.HasValue Then
                                ENPRROilMostRecentYear = ENPRROilIEAMostRecentYear
                                ENPRROilData = 2
                            End If
                        End If

                        If ENPRROilEndingYear < ENPRROilMostRecentYear Then
                            ENPRROilGap = ENPRROilMostRecentYear - ENPRROilEndingYear
                        Else
                            ENPRROilGap = 0
                        End If

                        If ENPRROilData = 1 Then
                            EnpOilBeginYear = item.obpBYR
                        ElseIf ENPRROilData = 2 Then
                            EnpOilBeginYear = item.oieaBYR
                        End If

                        'If item.Key = "Kuwait" Then Stop

                        If EnpOilEndingYear = 0 And EnpOilBeginYear = 0 Then
                            cachedENPRROil.Add(item.Key, 0S)
                        ElseIf Not EnpOilEndingYear.HasValue Or Not EnpOilBeginYear.HasValue Or EnpOilBeginYear < 0.001 Then
                            If cachedENPOil(item.Key) = 0 And cachedRESOIL(item.Key) = 0 Then
                                cachedENPRROil.Add(item.Key, 0S)
                            Else
                                cachedENPRROil.Add(item.Key, CSng(0.0252 - 0.0252 * Amin(1, cachedENPOil(item.Key) * 8 / cachedRESOIL(item.Key))))
                                '.0252 is global annual 1995-2000
                            End If
                        Else
                            cachedENPRROil.Add(item.Key, CSng((EnpOilEndingYear / EnpOilBeginYear) ^ (1 / ENPRROilInterval) - 1))
                        End If

                        'Gas
                        GasMRValue = Nothing
                        GasMRm5Value = Nothing
                        GasMRYrIdx = getYearIdxMstRctWData(diceGASBP, item.Key, yrsGASBP)
                        If GasMRYrIdx.HasValue Then
                            GasMRYr = yrsGASBP(CInt(GasMRYrIdx))
                        End If
                        If Not GasMRYrIdx.HasValue Then
                            GasMRYrIdx = getYearIdxMstRctWData(diceGASIEA, item.Key, yrsGASIEA)
                            If GasMRYrIdx.HasValue Then
                                GasMRYr = yrsGASIEA(CInt(GasMRYrIdx))
                                GasMRValue = diceGASIEA(item.Key)(CInt(GasMRYrIdx))
                                GasMRm5YrIdx = getYearIdxMstRctWData(diceGASIEA, item.Key, yrsGASIEA, (CInt(GasMRYr)) - 5)
                                If GasMRm5YrIdx.HasValue Then
                                    GasMRm5Yr = yrsGASIEA(CInt(GasMRm5YrIdx))
                                    GasMRm5Value = diceGASIEA(item.Key)(CInt(GasMRm5YrIdx))
                                End If
                            Else
                                GasMRm5Yr = Nothing
                            End If
                        Else
                            GasMRValue = diceGASBP(item.Key)(CInt(GasMRYrIdx))
                            GasMRm5YrIdx = getYearIdxMstRctWData(diceGASBP, item.Key, yrsGASBP, (CInt(GasMRYr)) - 5)
                            If GasMRm5YrIdx.HasValue Then
                                GasMRm5Yr = yrsGASBP(CInt(GasMRm5YrIdx))
                                GasMRm5Value = diceGASBP(item.Key)(CInt(GasMRm5YrIdx))
                            End If
                        End If
                        If GasMRValue = 0 And GasMRm5Value = 0 Then
                            cachedENPRRGas.Add(item.Key, 0S)
                        ElseIf Not GasMRValue.HasValue Or Not GasMRm5Value.HasValue Or GasMRm5Value < 0.001 Then
                            cachedENPRRGas.Add(item.Key, CSng(0.0339 -
                           0.0339 * Amin(1, cachedENPGas(item.Key) * 8 / cachedRESGAS(item.Key))))
                            '.0339 is global annual 1995-2000
                        Else
                            cachedENPRRGas.Add(item.Key, CSng((GasMRValue / GasMRm5Value) ^ (1 / (GasMRYr - GasMRm5Yr)) - 1)) 'If change years, change ratio
                        End If

                        'Coal
                        CoalMRValue = Nothing
                        CoalMRm5Value = Nothing
                        CoalMRYrIdx = getYearIdxMstRctWData(diceCOALBP, item.Key, yrsCOALBP)
                        If CoalMRYrIdx.HasValue Then
                            CoalMRYr = yrsCOALBP(CInt(CoalMRYrIdx))
                        End If
                        If Not CoalMRYrIdx.HasValue Then
                            CoalMRYrIdx = getYearIdxMstRctWData(diceCOALIEA, item.Key, yrsCOALIEA)
                            If CoalMRYrIdx.HasValue Then
                                CoalMRYr = yrsCOALIEA(CInt(CoalMRYrIdx))
                            End If
                            If CoalMRYrIdx.HasValue Then
                                CoalMRValue = diceCOALIEA(item.Key)(CInt(CoalMRYrIdx))
                                CoalMRm5YrIdx = getYearIdxMstRctWData(diceCOALIEA, item.Key, yrsCOALIEA, (CInt(CoalMRYr)) - 5)
                                If CoalMRm5YrIdx.HasValue Then
                                    CoalMRm5Yr = yrsCOALIEA(CInt(CoalMRm5YrIdx))
                                    CoalMRm5Value = diceCOALIEA(item.Key)(CInt(CoalMRm5YrIdx))
                                End If
                            Else
                                CoalMRm5Yr = Nothing
                            End If
                        Else
                            CoalMRValue = diceCOALBP(item.Key)(CInt(CoalMRYrIdx))
                            CoalMRm5YrIdx = getYearIdxMstRctWData(diceCOALBP, item.Key, yrsCOALBP, (CInt(CoalMRYr)) - 5)
                            If CoalMRm5YrIdx.HasValue Then
                                CoalMRm5Yr = yrsCOALBP(CInt(CoalMRm5YrIdx))
                                CoalMRm5Value = diceCOALBP(item.Key)(CInt(CoalMRm5YrIdx))
                            End If
                        End If
                        If CoalMRValue = 0 And CoalMRm5Value = 0 Then
                            cachedENPRRCoal.Add(item.Key, 0S)
                        ElseIf Not CoalMRValue.HasValue Or Not CoalMRm5Value.HasValue Or CoalMRm5Value < 0.001 Then
                            cachedENPRRCoal.Add(item.Key, CSng(0.0032 -
                           0.0032 * Amin(1, cachedENPCoal(item.Key) * 15 / cachedRESCOAL(item.Key))))
                        Else
                            cachedENPRRCoal.Add(item.Key, CSng((CoalMRValue / CoalMRm5Value) ^ (1 / (CoalMRYr - CoalMRm5Yr)) - 1)) 'If change years, change ratio
                        End If

                        'Hyd
                        HydMRValue = Nothing
                        HydMRm5Value = Nothing
                        HydMRYrIdx = getYearIdxMstRctWData(diceHYDBP, item.Key, yrsHYDBP)
                        If HydMRYrIdx.HasValue Then
                            HydMRYr = yrsHYDBP(CInt(HydMRYrIdx))
                        End If
                        If Not HydMRYrIdx.HasValue Then
                            HydMRYrIdx = getYearIdxMstRctWData(diceHYDIEA, item.Key, yrsHYDIEA)
                            If HydMRYrIdx.HasValue Then
                                HydMRYr = yrsHYDIEA(CInt(HydMRYrIdx))
                                HydMRValue = diceHYDIEA(item.Key)(CInt(HydMRYrIdx))
                                HydMRm5YrIdx = getYearIdxMstRctWData(diceHYDIEA, item.Key, yrsHYDIEA, (CInt(HydMRYr)) - 5)
                                If HydMRm5YrIdx.HasValue Then
                                    HydMRm5Yr = yrsHYDIEA(CInt(HydMRm5YrIdx))
                                    HydMRm5Value = diceHYDIEA(item.Key)(CInt(HydMRm5YrIdx))
                                End If
                            Else
                                HydMRm5Yr = Nothing
                            End If
                        Else
                            HydMRValue = diceHYDBP(item.Key)(CInt(HydMRYrIdx))
                            HydMRm5YrIdx = getYearIdxMstRctWData(diceHYDBP, item.Key, yrsHYDBP, (CInt(HydMRYr)) - 5)
                            If HydMRm5YrIdx.HasValue Then
                                HydMRm5Yr = yrsHYDBP(CInt(HydMRm5YrIdx))
                                HydMRm5Value = diceHYDBP(item.Key)(CInt(HydMRm5YrIdx))
                            End If
                        End If
                        If HydMRValue = 0 And HydMRm5Value = 0 Then
                            cachedENPRRHyd.Add(item.Key, 0S)
                        ElseIf Not HydMRValue.HasValue Or Not HydMRm5Value.HasValue Or HydMRm5Value < 0.001 Then
                            cachedENPRRHyd.Add(item.Key, 0.01F)
                        Else
                            cachedENPRRHyd.Add(item.Key, CSng((HydMRValue / HydMRm5Value) ^ (1 / (HydMRYr - HydMRm5Yr)) - 1)) 'If change years, change ratio
                        End If

                        'Nuc
                        NucMRValue = Nothing
                        NucMRm5Value = Nothing
                        NucMRYrIdx = getYearIdxMstRctWData(diceNUCBP, item.Key, yrsNUCBP)
                        If NucMRYrIdx.HasValue Then
                            NucMRYr = yrsNUCBP(CInt(NucMRYrIdx))
                            NucMRValue = diceNUCBP(item.Key)(CInt(NucMRYrIdx))
                            NucMRm5YrIdx = getYearIdxMstRctWData(diceNUCBP, item.Key, yrsNUCBP, (CInt(NucMRYr)) - 5)
                            If NucMRm5YrIdx.HasValue Then
                                NucMRm5Yr = yrsNUCBP(CInt(NucMRm5YrIdx))
                                NucMRm5Value = diceNUCBP(item.Key)(CInt(NucMRm5YrIdx))
                            End If
                        End If
                        If NucMRValue = 0 And NucMRm5Value = 0 Then
                            cachedENPRRNuc.Add(item.Key, 0S)
                        ElseIf Not NucMRValue.HasValue Or Not NucMRm5Value.HasValue Or NucMRm5Value < 0.001 Then
                            cachedENPRRNuc.Add(item.Key, 0.005F)
                        Else
                            cachedENPRRNuc.Add(item.Key, CSng(Amin(((NucMRValue / NucMRm5Value) ^ (1 / (NucMRYr - NucMRm5Yr)) - 1), 0.01F))) 'If change years, change ratio
                        End If

                        'mti 2024/08/20 start 
                        'Solar
                        'SolarMRValue = Nothing
                        'SolarMRm5Value = Nothing
                        'SolarMRYrIdx = getYearIdxMstRctWData(diceSOLARIEA, item.Key, yrsSOLARIEA)
                        'If SolarMRYrIdx.HasValue Then
                        '    SolarMRYr = yrsSOLARIEA(CInt(SolarMRYrIdx))
                        '    SolarMRValue = diceSOLARIEA(item.Key)(CInt(SolarMRYrIdx))
                        '    SolarMRm5YrIdx = getYearIdxMstRctWData(diceSOLARIEA, item.Key, yrsSOLARIEA, (CInt(SolarMRYr)) - 5)
                        '    If SolarMRm5YrIdx.HasValue Then
                        '        SolarMRm5Yr = yrsSOLARIEA(CInt(SolarMRm5YrIdx))
                        '        SolarMRm5Value = diceSOLARIEA(item.Key)(CInt(SolarMRm5YrIdx))
                        '    End If
                        'Else
                        '    'SolarMRm5Yr = Null
                        '    'bbh 2023/03/14 IEA table has nulls that should be zeros as added below
                        '    SolarMRm5Yr = 0
                        '    SolarMRYr = 0
                        '    SolarMRm5Value = 0
                        '    SolarMRValue = 0
                        'End If


                        '''mti 2024/07/23 start
                        '''CSP and photo together determine solar growth; code below
                        '''mti 2024/07/23 end

                        '''Photo
                        'PhotoMRValue = Nothing
                        'PhotoMRm5Value = Nothing
                        'PhotoMRYridx = getYearIdxMstRctWData(dicePHOTOIEA, item.Key, yrsPHOTOIEA)
                        'If PhotoMRYridx.HasValue Then
                        '    PhotoMRYr = yrsPHOTOIEA(CInt(PhotoMRYridx))
                        '    PhotoMRValue = dicePHOTOIEA(item.Key)(CInt(PhotoMRYridx))
                        '    PhotoMRm5Yridx = getYearIdxMstRctWData(dicePHOTOIEA, item.Key, yrsPHOTOIEA, (CInt(PhotoMRYr)) - 5)
                        '    If PhotoMRm5Yridx.HasValue Then
                        '        PhotoMRm5Yr = yrsPHOTOIEA(CInt(PhotoMRm5Yridx))
                        '        PhotoMRm5Value = dicePHOTOIEA(item.Key)(CInt(PhotoMRm5Yridx))
                        '    End If
                        'Else
                        '    'PhotoMRm5Yr = Null
                        '    'bbh 2023/03/14 IEA table has nulls that should be zeros as added below
                        '    PhotoMRm5Yr = 0
                        '    PhotoMRYr = 0
                        '    PhotoMRm5Value = 0
                        '    PhotoMRValue = 0
                        'End If




                        ''If item.Key = "United States of America" Then Stop
                        ''If item.Key = "Uruguay" Then Stop
                        ''If item.Key = "Zambia" Then Stop

                        ''mti 2024/07/23 start
                        ''CSP and photo together determine solar growth
                        ''If Not SolarMRValue.HasValue Or Not SolarMRm5Value.HasValue Or Not PhotoMRValue.HasValue Or Not PhotoMRm5Value.HasValue Then
                        'If (Not SolarMRValue.HasValue And Not PhotoMRValue.HasValue) Or (Not SolarMRm5Value.HasValue And Not PhotoMRm5Value.HasValue) Then
                        '    'no data at all at either of the two end points 
                        '    cachedENPRRSOLAR.Add(item.Key, 0.045F)
                        'Else

                        '    V2010Sum = SolarMRValue + PhotoMRValue
                        '    V2005Sum = SolarMRm5Value + PhotoMRm5Value
                        '    Yrs = AMAX((CInt(SolarMRYr)) - (CInt(SolarMRm5Yr)), (CInt(PhotoMRYr)) - (CInt(PhotoMRm5Yr)))
                        '    'YrCount = 5
                        '    'Yrs = Yrs / YrCount
                        '    V2010Sum = AMAX(0.0001F, V2010Sum)
                        '    V2005Sum = AMAX(0.0001F, V2005Sum)
                        '    If Yrs > 0 Then 'bbh 2023/03/14 put line below into new if-then; Aghanistan was showing 0 Yrs, no renewable
                        '        cachedENPRRSOLAR.Add(item.Key, ApplyLimits(CSng((V2010Sum / V2005Sum) ^ (1 / Yrs) - 1), 0.045, 0.15)) '.2 'mti 2024/08/20 'If change change years, change ratio
                        '    Else
                        '        cachedENPRRSOLAR.Add(item.Key, 0.045)
                        '    End If
                        'End If
                        ''mti 2024/07/23 end


                        'If item.Key = "United States of America" Then Stop

                        'If item.Key = "Ethiopia" Then Stop

                        'If item.Key = "Canada" Then Stop
                        'If item.Key = "Argentina" Then Stop
                        'If item.Key = "Malta" Then Stop
                        'If item.Key = "Slovenia" Then Stop

                        'cachedENPRRGas(key) = getAnnualGrowthRateFwd(EnPGasTbl, yrsEnPGas, key, cntx.FirstYear, 10, -10, IntvlMinEnpGasHist, IntvlMinEnpGasHist + 1, 5, 100)
                        'replacing the negative growth rates with a very small positive lower bound; otherwise they will result in disinvestment from wind and solar 

                        'mti 2025/02/10 start
                        PhotoMRValue = getAnnualGrowthRate(dicePHOTOIEA, yrsPHOTOIEA, item.Key, cntx.FirstYear, 0.2, 0.02, 4, 5, 20, 10)
                        SolarMRValue = getAnnualGrowthRate(diceSOLARIEA, yrsSOLARIEA, item.Key, cntx.FirstYear, 0.2, 0.02, 4, 5, 20, 10)

                        'PhotoMRValue = getAnnualGrowthRate(dicePHOTOIEA, yrsPHOTOIEA, item.Key, cntx.FirstYear, 2, 0.02, 4, 5, 20, 10)
                        'SolarMRValue = getAnnualGrowthRate(diceSOLARIEA, yrsSOLARIEA, item.Key, cntx.FirstYear, 2, 0.02, 4, 5, 20, 10)
                        'mti 2025/02/10 end






                        'Wind
                        'WindMRValue = Nothing
                        'WindMRm5Value = Nothing
                        'WindMRYridx = getYearIdxMstRctWData(diceWindIEA, item.Key, yrsWindIEA)
                        'If WindMRYridx.HasValue Then
                        '    WindMRYr = yrsWindIEA(CInt(WindMRYridx))
                        '    WindMRValue = diceWindIEA(item.Key)(CInt(WindMRYridx))
                        '    WindMRm5Yridx = getYearIdxMstRctWData(diceWindIEA, item.Key, yrsWindIEA, (CInt(WindMRYr)) - 5)
                        '    If WindMRm5Yridx.HasValue Then
                        '        WindMRm5Yr = yrsWindIEA(CInt(WindMRm5Yridx))
                        '        WindMRm5Value = diceWindIEA(item.Key)(CInt(WindMRm5Yridx))
                        '    End If
                        'Else
                        '    'WindMRm5Yr = Null
                        '    'bbh 2023/03/14 IEA table has nulls that should be zeros as added below
                        '    WindMRm5Yr = 0
                        '    WindMRYr = 0
                        '    WindMRm5Value = 0
                        '    WindMRValue = 0
                        'End If

                        ''mti 2024/07/23 start
                        'If WindMRValue = 0 And WindMRm5Value = 0 Then
                        '    cachedENPRRWIND.Add(item.Key, 0S)
                        'ElseIf Not WindMRValue.HasValue Or Not WindMRm5Value.HasValue Or WindMRm5Value < 0.001 Then
                        '    cachedENPRRWIND.Add(item.Key, 0.01F)
                        'Else
                        '    cachedENPRRWIND.Add(item.Key, ApplyLimits(CSng((WindMRValue / WindMRm5Value) ^ (1 / (WindMRYr - WindMRm5Yr)) - 1), 0.01, 0.15)) '.2 'mti 2024/08/20
                        'End If
                        ''mti 2024/07/23 end

                        'mti 2025/02/10 start
                        WindMRValue = getAnnualGrowthRate(diceWindIEA, yrsWindIEA, item.Key, cntx.FirstYear, 0.2, 0.02, 4, 5, 20, 10)
                        'WindMRValue = getAnnualGrowthRate(diceWindIEA, yrsWindIEA, item.Key, cntx.FirstYear, 0.5, 0.02, 4, 5, 20, 10)
                        'mti 2025/02/10 end

                        If PhotoMRValue = -9999 Then PhotoMRValue = Nothing
                        If SolarMRValue = -9999 Then SolarMRValue = Nothing
                        If WindMRValue = -9999 Then WindMRValue = Nothing


                        If PhotoMRValue.HasValue And SolarMRValue.HasValue Then
                            cachedENPRRSOLAR.Add(item.Key, CSng(0.7 * PhotoMRValue + 0.3 * SolarMRValue))
                            If WindMRValue.HasValue Then
                                'mti 2025/02/10 start
                                WindMRValue = ApplyLimits(WindMRValue, 0.02, CSng(cachedENPRRSOLAR(item.Key) + 0.01))
                                'WindMRValue = ApplyLimits(WindMRValue, 0.02, CSng(cachedENPRRSOLAR(item.Key) + 0.3))
                                'mti 2025/02/10 end
                            End If
                        ElseIf PhotoMRValue.HasValue Then
                            cachedENPRRSOLAR.Add(item.Key, CSng(PhotoMRValue))
                            If WindMRValue.HasValue Then

                                'mti 2025/02/10 start
                                WindMRValue = ApplyLimits(WindMRValue, 0.02, CSng(cachedENPRRSOLAR(item.Key) + 0.01))
                                'WindMRValue = ApplyLimits(WindMRValue, 0.02, CSng(cachedENPRRSOLAR(item.Key) + 0.3))
                                'mti 2025/02/10 end
                            End If
                        ElseIf SolarMRValue.HasValue Then
                            cachedENPRRSOLAR.Add(item.Key, CSng(SolarMRValue))
                            If WindMRValue.HasValue Then

                                'mti 2025/02/10 start
                                WindMRValue = ApplyLimits(WindMRValue, 0.02, CSng(cachedENPRRSOLAR(item.Key) + 0.01))
                                'WindMRValue = ApplyLimits(WindMRValue, 0.02, CSng(cachedENPRRSOLAR(item.Key) + 0.3))
                                'mti 2025/02/10 end
                            End If

                        Else
                            'mti 2024/09/05 start
                            'cachedENPRRSOLAR.Add(item.Key, 0.02)
                            cachedENPRRSOLAR.Add(item.Key, 0.08)
                            'mti 2024/09/05 end
                        End If

                        If WindMRValue.HasValue Then
                            cachedENPRRWIND.Add(item.Key, CSng(WindMRValue))
                        Else
                            'mti 2024/09/05 start
                            'cachedENPRRWIND.Add(item.Key, 0.02)
                            cachedENPRRWIND.Add(item.Key, 0.05)
                            'mti 2024/09/05 end
                        End If

                        'Geo
                        'GeoMRValue = Nothing
                        'GeoMRm5Value = Nothing
                        'GeoMRYrIdx = getYearIdxMstRctWData(diceGEOIEA, item.Key, yrsGEOIEA)
                        'If GeoMRYrIdx.HasValue Then
                        '    GeoMRYr = yrsGEOIEA(CInt(GeoMRYrIdx))
                        '    GeoMRValue = diceGEOIEA(item.Key)(CInt(GeoMRYrIdx))
                        '    GeoMRm5YrIdx = getYearIdxMstRctWData(diceGEOIEA, item.Key, yrsGEOIEA, (CInt(GeoMRYr)) - 5)
                        '    If GeoMRm5YrIdx.HasValue Then
                        '        GeoMRm5Yr = yrsGEOIEA(CInt(GeoMRm5YrIdx))
                        '        GeoMRm5Value = diceGEOIEA(item.Key)(CInt(GeoMRm5YrIdx))
                        '    End If
                        'Else
                        '    'GeoMRm5Yr = Null
                        '    'bbh 2023/03/14 IEA table has nulls that should be zeros as added below
                        '    GeoMRm5Yr = 0
                        '    GeoMRYr = 0
                        '    GeoMRm5Value = 0
                        '    GeoMRValue = 0
                        'End If


                        ''mti 2024/07/23 start
                        'If GeoMRValue = 0 And GeoMRm5Value = 0 Then
                        '    cachedENPRRGEOTHERMAL.Add(item.Key, 0S)
                        'ElseIf Not GeoMRValue.HasValue Or Not GeoMRm5Value.HasValue Or GeoMRm5Value < 0.001 Then
                        '    cachedENPRRGEOTHERMAL.Add(item.Key, 0.005F)
                        'Else
                        '    cachedENPRRGEOTHERMAL.Add(item.Key, ApplyLimits(CSng((GeoMRValue / GeoMRm5Value) ^ (1 / (GeoMRYr - GeoMRm5Yr)) - 1), 0.005, 0.1)) '.1 'mti 2024/08/20
                        'End If
                        ''mti 2024/07/23 end

                        GeoMRValue = getAnnualGrowthRate(diceGEOIEA, yrsGEOIEA, item.Key, cntx.FirstYear, 0.1, 0.001, 4, 5, 20, 10)

                        If GeoMRValue = -9999 Then GeoMRValue = Nothing

                        If GeoMRValue.HasValue Then
                            cachedENPRRGEOTHERMAL.Add(item.Key, CSng(GeoMRValue))
                        Else
                            cachedENPRRGEOTHERMAL.Add(item.Key, 0.01)
                        End If




                        'TideWave
                        'TideWaveMRValue = Nothing
                        'TideWaveMRm5Value = Nothing
                        'TideWaveMRYrIdx = getYearIdxMstRctWData(diceTIDEWAVEIEA, item.Key, yrsTIDEWAVEIEA)
                        'If TideWaveMRYrIdx.HasValue Then
                        '    TideWaveMRYr = yrsTIDEWAVEIEA(CInt(TideWaveMRYrIdx))
                        '    TideWaveMRValue = diceTIDEWAVEIEA(item.Key)(CInt(TideWaveMRYrIdx))
                        '    TideWaveMRm5YrIdx = getYearIdxMstRctWData(diceTIDEWAVEIEA, item.Key, yrsTIDEWAVEIEA, (CInt(TideWaveMRYr)) - 5)
                        '    If TideWaveMRm5YrIdx.HasValue Then
                        '        TideWaveMRm5Yr = yrsTIDEWAVEIEA(CInt(TideWaveMRm5YrIdx))
                        '        TideWaveMRm5Value = diceTIDEWAVEIEA(item.Key)(CInt(TideWaveMRm5YrIdx))
                        '    End If
                        'Else
                        '    'TideWaveMRm5Yr = Null 'bbh 2023/03/14 EnPTideWaveIEATbl is full of nulls that should be zeros as added below
                        '    TideWaveMRm5Yr = 0
                        '    TideWaveMRYr = 0
                        '    TideWaveMRm5Value = 0
                        '    TideWaveMRValue = 0
                        'End If


                        ''BioDiesel
                        'BioDieselMRValue = Nothing
                        'BioDieselMRm5Value = Nothing
                        'BioDieselMRYridx = getYearIdxMstRctWData(diceBIODIESELIEA, item.Key, yrsBIODIESELIEA)
                        'If BioDieselMRYridx.HasValue Then
                        '    BioDieselMRYr = yrsBIODIESELIEA(CInt(BioDieselMRYridx))
                        '    BioDieselMRValue = diceBIODIESELIEA(item.Key)(CInt(BioDieselMRYridx))
                        '    BioDieselMRm5Yridx = getYearIdxMstRctWData(diceBIODIESELIEA, item.Key, yrsBIODIESELIEA, (CInt(BioDieselMRYr)) - 5)
                        '    If BioDieselMRm5Yridx.HasValue Then
                        '        BioDieselMRm5Yr = yrsBIODIESELIEA(CInt(BioDieselMRm5Yridx))
                        '        BioDieselMRm5Value = diceBIODIESELIEA(item.Key)(CInt(BioDieselMRm5Yridx))
                        '    End If
                        'End If

                        ''BioGas
                        'BioGasMRValue = Nothing
                        'BioGasMRm5Value = Nothing
                        'BioGasMRYridx = getYearIdxMstRctWData(diceBIOGASIEA, item.Key, yrsBIOGASIEA)
                        'If BioGasMRYridx.HasValue Then
                        '    BioGasMRYr = yrsBIOGASIEA(CInt(BioGasMRYridx))
                        '    BioGasMRValue = diceBIOGASIEA(item.Key)(CInt(BioGasMRYridx))
                        '    BioGasMRm5Yridx = getYearIdxMstRctWData(diceBIOGASIEA, item.Key, yrsBIOGASIEA, (CInt(BioGasMRYr)) - 5)
                        '    If BioGasMRm5Yridx.HasValue Then
                        '        BioGasMRm5Yr = yrsBIOGASIEA(CInt(BioGasMRm5Yridx))
                        '        BioGasMRm5Value = diceBIOGASIEA(item.Key)(CInt(BioGasMRm5Yridx))
                        '    End If
                        'End If

                        ''mti 2024/07/23 start
                        ''        If Not SolarMRValue.HasValue Or Not SolarMRm5Value.HasValue Or Not GeoMRValue.HasValue Or Not GeoMRm5Value.HasValue _
                        ''Or Not TideWaveMRValue.HasValue Or Not TideWaveMRm5Value.HasValue Or Not PhotoMRValue.HasValue Or Not PhotoMRm5Value.HasValue _
                        ''Or Not WindMRValue.HasValue Or Not WindMRm5Value.HasValue Then

                        'If (Not TideWaveMRValue.HasValue And Not BioDieselMRValue.HasValue And Not BioGasMRValue.HasValue) Or
                        '    (Not TideWaveMRm5Value.HasValue And Not BioDieselMRm5Value.HasValue And Not BioGasMRm5Value.HasValue) Then
                        '    'no data at all at either of the two end points 
                        '    cachedENPRRRen.Add(item.Key, 0.045F)
                        'Else
                        '    'jrs 2009/10/07 only use BioDiesel or gas when data is available
                        '    V2010Sum = TideWaveMRValue + BioDieselMRValue + BioGasMRValue
                        '    V2005Sum = TideWaveMRm5Value + BioDieselMRm5Value + BioGasMRm5Value

                        '    If TideWaveMRYr.HasValue And TideWaveMRm5Yr.HasValue Then
                        '        Yrs = CInt(TideWaveMRYr) - CInt(TideWaveMRm5Yr)
                        '    Else
                        '        Yrs = 0
                        '    End If

                        '    If BioDieselMRYr.HasValue And BioDieselMRm5Yr.HasValue Then
                        '        Yrs = AMAX(CInt(BioDieselMRYr) - CInt(BioDieselMRm5Yr), Yrs)
                        '    End If

                        '    If BioGasMRYr.HasValue And BioGasMRm5Yr.HasValue Then
                        '        Yrs = AMAX(CInt(BioGasMRYr) - CInt(BioGasMRm5Yr), Yrs)
                        '    End If

                        '    V2010Sum = AMAX(0.0001F, V2010Sum)
                        '    V2005Sum = AMAX(0.0001F, V2005Sum)

                        '    If Yrs > 0 Then 'bbh 2023/03/14 put line below into new if-then; Aghanistan was showing 0 Yrs, no renewable
                        '        cachedENPRRRen.Add(item.Key, ApplyLimits(CSng((V2010Sum / V2005Sum) ^ (1 / Yrs) - 1), 0.005, 0.07)) '.2 'mti 2024/08/20'If change change years, change ratio
                        '    Else
                        '        cachedENPRRRen.Add(item.Key, 0.005)
                        '    End If
                        'End If

                        'mti 2024/09/06 start
                        'TideWaveMRValue = getAnnualGrowthRate(diceTIDEWAVEIEA, yrsTIDEWAVEIEA, item.Key, cntx.FirstYear, 0.1, 0.001, 4, 5, 20, 10)
                        'BioDieselMRValue = getAnnualGrowthRate(diceBIODIESELIEA, yrsBIODIESELIEA, item.Key, cntx.FirstYear, 0.1, 0.001, 4, 5, 20, 10)
                        'BioGasMRValue = getAnnualGrowthRate(diceBIOGASIEA, yrsBIOGASIEA, item.Key, cntx.FirstYear, 0.1, 0.001, 4, 5, 20, 10)

                        TideWaveMRValue = getAnnualGrowthRate(diceTIDEWAVEIEA, yrsTIDEWAVEIEA, item.Key, cntx.FirstYear, 0.02, 0.001, 4, 5, 20, 10)
                        BioDieselMRValue = getAnnualGrowthRate(diceBIODIESELIEA, yrsBIODIESELIEA, item.Key, cntx.FirstYear, 0.02, 0.001, 4, 5, 20, 10)
                        BioGasMRValue = getAnnualGrowthRate(diceBIOGASIEA, yrsBIOGASIEA, item.Key, cntx.FirstYear, 0.02, 0.001, 4, 5, 20, 10)
                        'mti 2024/09/06 end

                        If TideWaveMRValue = -9999 Then TideWaveMRValue = Nothing
                        If BioDieselMRValue = -9999 Then BioDieselMRValue = Nothing
                        If BioGasMRValue = -9999 Then BioGasMRValue = Nothing

                        'If item.Key = "Indonesia" Then Stop

                        If TideWaveMRValue.HasValue And BioDieselMRValue.HasValue And BioGasMRValue.HasValue Then
                            cachedENPRRRen.Add(item.Key, CSng(0.2 * TideWaveMRValue + 0.4 * BioDieselMRValue + 0.4 * BioGasMRValue))
                        ElseIf BioDieselMRValue.HasValue And BioGasMRValue.HasValue Then
                            cachedENPRRRen.Add(item.Key, CSng(0.5 * BioDieselMRValue + 0.5 * BioGasMRValue))
                        ElseIf BioDieselMRValue.HasValue Then
                            cachedENPRRRen.Add(item.Key, CSng(BioDieselMRValue))
                        ElseIf BioGasMRValue.HasValue Then
                            cachedENPRRRen.Add(item.Key, CSng(BioGasMRValue))
                        ElseIf TideWaveMRValue.HasValue Then
                            cachedENPRRRen.Add(item.Key, TideWaveMRValue)
                        Else
                            cachedENPRRRen.Add(item.Key, 0.01)
                        End If

                        'mti 2024/08/20 end


                        '=======old ==
                        'If Not TideWaveMRValue.HasValue Or Not TideWaveMRm5Value.HasValue Then
                        '    cachedENPRRRen.Add(item.Key, 0.045F)
                        'Else
                        '    'jrs 2009/10/07 only use BioDiesel or gas when data is available
                        '    'V2010Sum = SolarMRValue + GeoMRValue + TideWaveMRValue + PhotoMRValue + WindMRValue
                        '    'V2005Sum = SolarMRm5Value + GeoMRm5Value + TideWaveMRm5Value + PhotoMRm5Value + WindMRm5Value
                        '    V2010Sum = TideWaveMRValue
                        '    V2005Sum = TideWaveMRm5Value
                        '    'Yrs = (CInt(SolarMRYr)) - (CInt(SolarMRm5Yr)) + (CInt(GeoMRYr)) - (CInt(GeoMRm5Yr)) +
                        '    '(CInt(TideWaveMRYr)) - (CInt(TideWaveMRm5Yr)) + (CInt(PhotoMRYr)) - (CInt(WindMRm5Yr)) +
                        '    '(CInt(PhotoMRYr)) - (CInt(WindMRm5Yr))
                        '    Yrs = (CInt(TideWaveMRYr)) - (CInt(TideWaveMRm5Yr))
                        '    YrCount = 5
                        '    If BioDieselMRValue.HasValue And BioDieselMRm5Value.HasValue Then
                        '        V2010Sum = V2010Sum + BioDieselMRValue
                        '        V2005Sum = V2005Sum + BioDieselMRm5Value
                        '        Yrs = Yrs + (CInt(BioDieselMRYr)) - (CInt(BioDieselMRm5Yr))
                        '        YrCount = YrCount + 1S
                        '    End If
                        '    If BioGasMRValue.HasValue And BioGasMRm5Value.HasValue Then
                        '        V2010Sum = V2010Sum + BioGasMRValue
                        '        V2005Sum = V2005Sum + BioGasMRm5Value
                        '        Yrs = Yrs + (CInt(BioGasMRYr)) - (CInt(BioGasMRm5Yr))
                        '        YrCount = YrCount + 1S
                        '    End If
                        '    Yrs = Yrs / YrCount

                        '    V2010Sum = AMAX(0.0001F, V2010Sum)
                        '    V2005Sum = AMAX(0.0001F, V2005Sum)
                        '    If Yrs > 0 Then 'bbh 2023/03/14 put line below into new if-then; Aghanistan was showing 0 Yrs, no renewable
                        '        cachedENPRRRen.Add(item.Key, ApplyLimits(CSng((V2010Sum / V2005Sum) ^ (1 / Yrs) - 1), 0.045, 0.2)) 'If change change years, change ratio
                        '    Else
                        '        cachedENPRRRen.Add(item.Key, 0.045)
                        '    End If
                        'End If

                        'cachedENPRRSOLAR.Add(item.Key, cachedENPRRRen(item.Key))
                        'mti 2024/07/23 end
                    Next
                End If 'End if Hist then else Forecast

                '   7/29/06 USGS 2000 assessment shows total oil resources at 2659, of which
                '       539 are cummulative production, 859 remaining reserves, 612 reserve growth,
                '       and 649 undiscovered (have undiscovered in RESOROUSGS)
                '   For gas total is 2249, 150 produced, 770 reserves, 551 reserve growth, 779 undisc
                '   For NGL 324 total, 68 produced, 42 reserves, 207 undiscovered (in RESORNGLUSGS)

                '   Need to allocate reserve growth to countries, but data on oil reserves (BP/Oil&Gas(
                '       show oil reserves at 1246 and gas at 1163, so some of growth already in it
                '   For oil, 612+859 is 1471 discovered plus extentions vs 1246 reserve data;
                '       expansion factor is 1471/1246 = 1.18; for gas it is 1.14
                '   See http://energy.cr.usgs.gov/WEcont/chaps/ES.pdf

                '   bbh 2013/04/03 start (with energy modeling seminar)
                '       Using data from BGR on unconventional oil and gas resources
                '       SeriesEnResorOilBGR includes all conventional and unconventional resources and also includes reserves
                '       SeriesEnReserGasBGR similarly includes all
                '       So need to back unconventional totals out of the total from those series to get conventional totals
                '       SeriesEnResorOilBGR and SeriesEnReserGasBGR include most all countries while specific series on
                '           unconventional resources and reserves do not; another reasonto start with these two series and
                '           back out unconventional totals.

                Dim ResorOilSands As Single?, ResorShaleOil As Single?, ResorHeavyOil As Single?
                Dim ReserOilSands As Single?, ReserShaleOil As Single?, ReserHeavyOil As Single?
                Dim ResorOilBGR As Single?, ReserOilUBGR As Single?, ResorOilUBGR As Single?
                Dim ResorOil As Single?

                Dim ResorShaleGas As Single?, ResorCBM As Single?, ResorTightGas As Single?
                Dim ReserShaleGas As Single?, ReserCBM As Single?
                Dim ResorGasBGR As Single?
                Dim ResorGas As Single?
                Dim ReserGasUBGR As Single?, ResorGasUBGR As Single?
                Dim ResorCoal As Single?, LandAreaRen As Single?, CumProdOil As Single?, CumProdGas As Single?, CumProdCoal As Single?
                Dim YCoal As Single?, YOil As Single?, YGas As Single?, ProdYear As String
                Dim OilBPIdx As Integer, CoalBPidx As Integer, OilIEAIdx As Integer, CoalIEAIdx As Integer
                Dim yrsOILBP As Short(), yrsOILIEA As Short(), yrsOIL As Short()
                Dim diceOILBP As IDictionary(Of String, Single?()), diceOILIEA As IDictionary(Of String, Single?()), diceOIL As IDictionary(Of String, Single?())
                Dim yrsGas As Short()
                Dim diceGas As IDictionary(Of String, Single?())

                'mti 2024/08/29 start
                Dim ResorSolar As Single?, ResorWind As Single?, ResorGeo As Single?
                Dim ENPTotal As Single, ENPSolar As Single, ENPWind As Single
                'mti 2024/08/29 end

                'Only Load Dice if historic mode
                If cntx.HistoricMode Then
                    yrsOILBP = cntx.GetYears("ENPOILBP").OrderBy(Function(y) y).ToArray()
                    diceOILBP = cntx.GetDice("ENPOILBP", yrsOILBP)
                    yrsOILIEA = cntx.GetYears("ENPOILIEA").OrderBy(Function(y) y).ToArray()
                    diceOILIEA = cntx.GetDice("ENPOILIEA", yrsOILIEA)
                    yrsOIL = cntx.GetYears("ENPOIL").OrderBy(Function(y) y).ToArray()
                    diceOIL = cntx.GetDice("ENPOIL", yrsOIL)

                    yrsCOALBP = cntx.GetYears("ENPCOALBP").OrderBy(Function(y) y).ToArray()
                    diceCOALBP = cntx.GetDice("ENPCOALBP", yrsCOALBP)
                    yrsCOALIEA = cntx.GetYears("ENPCOALIEA").OrderBy(Function(y) y).ToArray()
                    diceCOALIEA = cntx.GetDice("ENPCOALIEA", yrsCOALIEA)
                    yrsCOAL = cntx.GetYears("ENPCOAL").OrderBy(Function(y) y).ToArray()
                    diceCOAL = cntx.GetDice("ENPCOAL", yrsCOAL)

                    yrsGas = cntx.GetYears("ENPGAS").OrderBy(Function(y) y).ToArray()
                    diceGas = cntx.GetDice("ENPGAS", yrsGas)
                End If

                'mti 2024/08/29 start
                'For Each item In (From obg In cntx.GetSlice("RESOROILSANDSBGR", cntx.BaseYear, CoalescePolicy.MostRecent)
                '                  Join sho In cntx.GetSlice("RESORSHALEOIL", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals sho.Key
                '                  Join heo In cntx.GetSlice("RESORHEAVYOIL", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals heo.Key
                '                  Join osb In cntx.GetSlice("RESEROILSANDSBGR", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals osb.Key
                '                  Join shore In cntx.GetSlice("RESERSHALEOIL", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals shore.Key
                '                  Join heore In cntx.GetSlice("RESERHEAVYOIL", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals heore.Key
                '                  Join obgr In cntx.GetSlice("RESOROILBGR", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals obgr.Key
                '                  Join shg In cntx.GetSlice("RESORSHALEGAS", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals shg.Key
                '                  Join cbm In cntx.GetSlice("RESORCBM", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals cbm.Key
                '                  Join tgs In cntx.GetSlice("RESORTIGHTGAS", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals tgs.Key
                '                  Join shgres In cntx.GetSlice("RESERSHALEGAS", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals shgres.Key
                '                  Join cbmres In cntx.GetSlice("RESERCBM", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals cbmres.Key
                '                  Join gbgr In cntx.GetSlice("RESORGASBGR", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals gbgr.Key
                '                  Join coal In cntx.GetSlice("RESORCOAL", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals coal.Key
                '                  Select Key = obg.Key, obg = If(obg.Value, 0), sho = If(sho.Value, 0), heo = If(heo.Value, 0), osb = If(osb.Value, 0),
                '                                          shore = If(shore.Value, 0), heore = If(heore.Value, 0), obgr = obgr.Value,
                '                            shg = If(shg.Value, 0), cbm = If(cbm.Value, 0), tgs = If(tgs.Value, 0), shgres = If(shgres.Value, 0),
                '              cbmres = If(cbmres.Value, 0), gbgr = gbgr.Value, coal = If(coal.Value, 0.001F))

                For Each item In (From obg In cntx.GetSlice("RESOROILSANDSBGR", cntx.BaseYear, CoalescePolicy.MostRecent)
                                  Join sho In cntx.GetSlice("RESORSHALEOIL", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals sho.Key
                                  Join heo In cntx.GetSlice("RESORHEAVYOIL", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals heo.Key
                                  Join osb In cntx.GetSlice("RESEROILSANDSBGR", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals osb.Key
                                  Join shore In cntx.GetSlice("RESERSHALEOIL", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals shore.Key
                                  Join heore In cntx.GetSlice("RESERHEAVYOIL", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals heore.Key
                                  Join obgr In cntx.GetSlice("RESOROILBGR", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals obgr.Key
                                  Join shg In cntx.GetSlice("RESORSHALEGAS", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals shg.Key
                                  Join cbm In cntx.GetSlice("RESORCBM", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals cbm.Key
                                  Join tgs In cntx.GetSlice("RESORTIGHTGAS", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals tgs.Key
                                  Join shgres In cntx.GetSlice("RESERSHALEGAS", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals shgres.Key
                                  Join cbmres In cntx.GetSlice("RESERCBM", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals cbmres.Key
                                  Join gbgr In cntx.GetSlice("RESORGASBGR", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals gbgr.Key
                                  Join coal In cntx.GetSlice("RESORCOAL", cntx.BaseYear, CoalescePolicy.MostRecent) On obg.Key Equals coal.Key
                                  Join solar In cntx.GetSlice("POTENTIALSOLAR", cntx.BaseYear, MyBase.Policy) On obg.Key Equals solar.Key
                                  Join wind In cntx.GetSlice("POTENTIALWIND", cntx.BaseYear, MyBase.Policy) On obg.Key Equals wind.Key
                                  Join geo In cntx.GetSlice("POTENTIALGEO", cntx.BaseYear, MyBase.Policy) On obg.Key Equals geo.Key
                                  Select Key = obg.Key, obg = If(obg.Value, 0), sho = If(sho.Value, 0), heo = If(heo.Value, 0), osb = If(osb.Value, 0),
                                                              shore = If(shore.Value, 0), heore = If(heore.Value, 0), obgr = obgr.Value,
                                                shg = If(shg.Value, 0), cbm = If(cbm.Value, 0), tgs = If(tgs.Value, 0), shgres = If(shgres.Value, 0),
                                  cbmres = If(cbmres.Value, 0), gbgr = gbgr.Value, coal = If(coal.Value, 0.001F), solar = If(solar.Value, -0.001F),
                                    wind = If(wind.Value, -0.001F), geo = If(geo.Value, -0.001F))

                    'mti 2024/12/10 on the geo line change "POTENTIALSOLAR" to "POTENTIALGEO", also changed cntx.FirstYear to cntx.BaseYear on solar, wind and geo

                    'mti 2024/08/29 end

                    ResorOilSands = item.obg
                    ResorShaleOil = item.sho
                    ResorHeavyOil = item.heo
                    ReserOilSands = item.osb
                    ReserShaleOil = item.shore
                    ReserHeavyOil = item.heore
                    ResorOilBGR = item.obgr
                    'bbh 2023/03/14 Substituted line below and then came back to this
                    'after fixing renewable energy growth; but the big 2016 jump still a potential future issue
                    'ResorOilBGR = ResorOilBGRTbl("2015") 'bbh 2023/03/14 Huge jump in values from this source in 2016; use 2015
                    ResorOilBGR = ResorOilBGR * 7.33F / 1000.0F
                    If Not ResorOilBGR.HasValue Then
                        ResorOilBGR = 1.6F * cachedRESOIL(item.Key)
                    End If 'Consider revisting to look also at earlier resource data (USGS)
                    ResorOil = AMAX(ResorOilBGR, 1.6F * cachedRESOIL(item.Key))

                    'If item.Key = "United States of America" Then
                    '    Stop
                    'End If

                    ReserOilUBGR = ReserOilSands + ReserShaleOil + ReserHeavyOil
                    ResorOilUBGR = ReserOilUBGR + ResorOilSands + ResorShaleOil + ResorHeavyOil
                    cachedRESOROILUN.Add(item.Key, CSng(ResorOilUBGR * (7.33 / 1000)))
                    cachedRESOROIL.Add(item.Key, AMAX(0.02F * ResorOil, ResorOil - cachedRESOROILUN(item.Key)))

                    ResorShaleGas = item.shg
                    ResorCBM = item.cbm
                    ResorTightGas = item.tgs

                    ReserShaleGas = item.shgres
                    ReserCBM = item.cbmres
                    ResorGasBGR = item.gbgr
                    If ResorGasBGR.HasValue Then
                        ResorGasBGR = CSng(ResorGasBGR * CDbl(6.6) / CDbl(1000.0))
                    End If
                    If Not ResorGasBGR.HasValue Then
                        ResorGasBGR = 1.6F * cachedRESGAS(item.Key)
                    End If    'Consider revisting to look also at earlier resource data (USGS)
                    ResorGas = AMAX(ResorGasBGR, 1.6F * cachedRESGAS(item.Key))

                    ReserGasUBGR = ReserShaleGas + ReserCBM
                    ResorGasUBGR = ReserGasUBGR + ResorShaleGas + ResorCBM + ResorTightGas 'bbh 2014/02/21
                    cachedRESORGASUN.Add(item.Key, ResorGasUBGR * (6.6F / 1000.0F)) 'million cubic meters to bboe                    
                    cachedRESORGAS.Add(item.Key, AMAX(0.02F * ResorGas, ResorGas - cachedRESORGASUN(item.Key)))
                    'See note above about the data series including unconventional also
                    ResorCoal = item.coal
                    If ResorCoal < 0.02 Then ResorCoal = cachedRESCOAL(item.Key) * 2
                    If ResorCoal < cachedRESCOAL(item.Key) * 1.3 Then
                        ResorCoal = cachedRESCOAL(item.Key) * 1.3F
                    End If
                    cachedRESORCOAL.Add(item.Key, ResorCoal)

                    LandAreaRen = CLANDAREA(item.Key)


                    'mti 2024/07/23 start
                    'ResorRenew.Add(item.Key, ((CPOP(item.Key) + LandAreaRen) / 2) / 8)
                    'cachedRESORSOLAR.Add(item.Key, (0.4D * (CPOP(item.Key) + LandAreaRen) / 2) / 8)
                    'cachedRESORWind.Add(item.Key, (0.3D * (CPOP(item.Key) + LandAreaRen) / 2) / 8)
                    'cachedRESORGEOTHERMAL.Add(item.Key, (0.2D * (CPOP(item.Key) + LandAreaRen) / 2) / 8)
                    'ResorRenew.Add(item.Key, (0.1D * (CPOP(item.Key) + LandAreaRen) / 2) / 8)

                    'mti 2024/08/29 start
                    ResorSolar = item.solar
                    If ResorSolar < 0 Then
                        ResorSolar = ((CPOP(item.Key) + LandAreaRen) / 2) / 8
                    Else
                        ResorSolar = CSng(ResorSolar * 0.00058844074)  'TW-Hr to bboe
                    End If
                    cachedRESORSOLAR.Add(item.Key, ResorSolar)

                    ResorWind = item.wind
                    If ResorWind < 0 Then
                        ResorWind = ((CPOP(item.Key) + LandAreaRen) / 2) / 8
                    Else
                        ResorWind = CSng(ResorWind * 0.00058844074)  'TW-Hr to bboe
                    End If
                    cachedRESORWIND.Add(item.Key, ResorWind)

                    ResorGeo = item.geo
                    'If item.Key = "Indonesia" Then Stop
                    If ResorGeo < 0 Then
                        ResorGeo = ((CPOP(item.Key) + LandAreaRen) / 2) / 8
                    Else
                        ResorGeo = CSng(ResorGeo * 0.00058844074)  'TW-Hr to bboe
                    End If
                    cachedRESORGEOTHERMAL.Add(item.Key, ResorGeo)

                    'cachedRESORSOLAR.Add(item.Key, ((CPOP(item.Key) + LandAreaRen) / 2) / 8)
                    'cachedRESORWIND.Add(item.Key, ((CPOP(item.Key) + LandAreaRen) / 2) / 8)
                    'cachedRESORGEOTHERMAL.Add(item.Key, ((CPOP(item.Key) + LandAreaRen) / 2) / 8)
                    'mti 2024/08/29 end

                    'mti 2024/07/23 start
                    'If item.Key = "Indonesia" Then Stop
                    'mti 2024/09/05 start
                    'ResorRenew.Add(item.Key, ((CPOP(item.Key) + LandAreaRen) / 2) / 8)
                    'ResorRenew.Add(item.Key, ((CPOP(item.Key) + LandAreaRen) / 2) / 32)

                    'If item.Key = "China" Then Stop
                    'If item.Key = "Indonesia" Then Stop
                    ResorRenew.Add(item.Key, CSng(LandAreaRen * 0.006)) '30% for crop; 2% of that for energy crop
                    'mti 2024/09/05 End
                    'mti 2024/07/23 end

                    'mti 2024/07/23 end
                    'Logic is renewable goes up with land area for some countries (Saudi, not Greenland)
                    '  and goes up for countries with better climate as indicated by population density
                    'Second division is scaling factor

                    If cntx.HistoricMode Then
                        CumProdOil = 0
                        CumProdGas = 0
                        CumProdCoal = 0
                        'jrs 2009/11/11
                        OilBPIdx = 0
                        OilIEAIdx = 0
                        CoalBPidx = 0
                        CoalIEAIdx = 0
                        YCoal = Nothing
                        YOil = Nothing
                        YGas = Nothing

                        For YCount% = 1960 To cntx.BaseYear
                            ProdYear$ = YCount%.ToString.Trim
                            'Advance BP table to get to year ProdYear$
                            Do While YCount% > yrsOILBP(OilBPIdx) _
                        And OilBPIdx + 1 < yrsOILBP.Count
                                OilBPIdx = OilBPIdx + 1
                            Loop
                            If YCount% = yrsOILBP(OilBPIdx) Then
                                YOil = diceOILBP(item.Key)(OilBPIdx)
                            End If
                            If Not YOil.HasValue Then
                                'Advance IEA table to get to year ProdYear$
                                Do While YCount% > yrsOILIEA(OilIEAIdx) _
                            And OilIEAIdx + 1 < yrsOILIEA.Count
                                    OilIEAIdx = OilIEAIdx + 1
                                Loop
                                If YCount = yrsOILIEA(OilIEAIdx) Then
                                    YOil = diceOILIEA(item.Key)(OilIEAIdx)
                                End If
                                If Not YOil.HasValue And YCount% <= 1995 Then
                                    YOil = diceOIL(item.Key)(YCount% - yrsOIL(0))
                                End If
                            End If

                            If Not YOil.HasValue Then YOil = 0

                            'jrs 2009/11/11
                            If YCount% <= 1995 Then
                                YGas = diceGas(item.Key)(YCount% - yrsGas(0))
                            End If
                            If Not YGas.HasValue Then YGas = 0

                            'Advance BP table to get to year ProdYear$
                            Do While YCount > yrsCOALBP(CoalBPidx) _
                        And CoalBPidx + 1 < yrsCOALBP.Count
                                CoalBPidx = CoalBPidx + 1
                            Loop
                            If YCount = yrsCOALBP(CoalBPidx) Then
                                YCoal = diceCOALBP(item.Key)(CoalBPidx)
                            End If
                            If Not YCoal.HasValue Then
                                'Advance IEA table to get to year ProdYear$
                                Do While YCount > yrsCOALIEA(CoalIEAIdx) _
                            And CoalIEAIdx + 1 < yrsCOALIEA.Count
                                    CoalIEAIdx = CoalIEAIdx + 1
                                Loop
                                If YCount = yrsCOALIEA(CoalIEAIdx) Then
                                    YCoal = diceCOALIEA(item.Key)(CoalIEAIdx)
                                End If
                                If Not YCoal.HasValue And YCount% <= 1995 Then
                                    YCoal = diceCOAL(item.Key)(YCount - yrsCOAL(0))
                                End If
                            End If

                            If Not YCoal.HasValue Then YCoal = 0
                            CumProdOil = CumProdOil + YOil
                            CumProdGas = CumProdGas + YGas
                            CumProdCoal = CumProdCoal + YCoal

                            'jrs 2009/11/11
                            YCoal = Nothing
                            YOil = Nothing
                            YGas = Nothing
                        Next

                        '   bbh 2013/04/03 Note:  are adding cumulative producction to resources here, but in
                        '       model first year set them to zero; so are allowing production of them a second time; looks like error
                        '       Was CUMPR once a global variable?  Would need to make it one to carry this into model
                        cachedRESOROIL(item.Key) = cachedRESOROIL(item.Key) + CumProdOil
                        '   bbh 2013/04/03 (with energy modeling seminar)
                        cachedRESOROILUN(item.Key) = cachedRESOROILUN(item.Key) 'No Cum Production to add to unconventional
                        cachedRESORGAS(item.Key) = cachedRESORGAS(item.Key) + CumProdGas
                        cachedRESORGASUN(item.Key) = cachedRESORGASUN(item.Key)
                        '   bbh 2013/04/03 end
                        cachedRESORCOAL(item.Key) = cachedRESORCOAL(item.Key) + CumProdCoal

                    End If 'End of if history 

                    'mti 2024/09/09 start
                    'If item.Key = "Bangladesh" Then Stop
                    'If item.Key = "India" Then Stop

                    ENPTotal = CSng(cachedENPOil(item.Key) + cachedENPGas(item.Key) + cachedENPCoal(item.Key) +
                        cachedENPHYD(item.Key) + cachedENPNUC(item.Key) + cachedENPSOLAR(item.Key) +
                         +cachedENPWIND(item.Key) + +cachedENPGEOTHERMAL(item.Key) + cachedENPOTHREN(item.Key))

                    ENPTotal = AMAX(CSng(AppConEn(item.Key)), ENPTotal) 'greater of consumption and production


                    'mti 2025/02/10 start
                    If (cachedENPSOLAR(item.Key) / ENPTotal) < 0.005 Then
                        'Stop
                        cachedENPRRSOLAR(item.Key) = AMAX(0.2, CSng(cachedENPRRSOLAR(item.Key)))
                    End If
                    If (cachedENPWIND(item.Key) / ENPTotal) < 0.005 Then
                        'Stop
                        cachedENPRRWIND(item.Key) = AMAX(0.15, CSng(cachedENPRRWIND(item.Key)))
                    End If

                    'If item.Key = "Canada" Then Stop
                    'If item.Key = "Argentina" Then Stop
                    'If item.Key = "Malta" Then Stop
                    'If item.Key = "Slovenia" Then Stop

                    'If (cachedENPSOLAR(item.Key) / ENPTotal) < 0.005 Then
                    '    'Stop
                    '    cachedENPRRSOLAR(item.Key) = AMAX(0.8, CSng(cachedENPRRSOLAR(item.Key)))
                    'End If
                    'If (cachedENPWIND(item.Key) / ENPTotal) < 0.005 Then
                    '    'Stop
                    '    cachedENPRRWIND(item.Key) = AMAX(0.8, CSng(cachedENPRRWIND(item.Key)))
                    'End If

                    'mti 2025/02/10 end
                    'mti 2024/09/09 end
                Next

                cntx.Store("ENPOIL", cachedENPOil)
                cntx.Store("ENPGAS", cachedENPGas)
                cntx.Store("ENPCOAL", cachedENPCoal)
                cntx.Store("ENPHyd", cachedENPHYD)
                cntx.Store("ENPNUC", cachedENPNUC)

                'mti 2024/07/23 start
                cntx.Store("ENPSOLAR", cachedENPSOLAR)
                cntx.Store("ENPWIND", cachedENPWIND)
                cntx.Store("ENPGeoThermal", cachedENPGEOTHERMAL)
                'mti 2024/07/23 end

                cntx.Store("ENPOthRen", cachedENPOTHREN)


                cntx.Store("CENM", cachedENM)
                cntx.Store("CENX", cachedENX)

                cntx.Store("AppConEn", AppConEn)

                ' mark pre-processed for all three series
                Me.processed = True

            End Sub

            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Me.PreProcess(cntx)
                Dim series As IDictionary(Of String, Single?)
                Dim bAvg As Boolean = False
                Dim bApplyLimits As Boolean = False
                Dim MinLimit As Single, MaxLimit As Single

                Select Case seriesName
                    Case "GDPRPA"
                        series = Me.cachedGDPRPA
                    Case "ENM"
                        series = Me.cachedENM
                        MinLimit = 0.001
                        MaxLimit = 1000
                        bApplyLimits = True
                    Case "ENX"
                        series = Me.cachedENX
                        MinLimit = 0.001
                        MaxLimit = 1000
                        bApplyLimits = True
                    Case "ENP_Oil"
                        series = Me.cachedENPOil
                        MinLimit = 0.000000001 'jrs 2022/04/04
                        MaxLimit = 1000
                        bApplyLimits = True
                    Case "ENP_Gas"
                        series = Me.cachedENPGas
                        MinLimit = 0.000000001 'jrs 2022/04/04
                        MaxLimit = 1000
                        bApplyLimits = True
                    Case "ENP_Coal"
                        series = Me.cachedENPCoal
                        MinLimit = 0.000000001 'jrs 2022/04/04
                        MaxLimit = 1000
                        bApplyLimits = True
                    Case "ENP_Hydro"
                        series = Me.cachedENPHYD
                        MinLimit = 0.000000001 'jrs 2022/04/04
                        MaxLimit = 1000
                        bApplyLimits = True
                    Case "ENP_Nuclear"
                        series = Me.cachedENPNUC
                        MinLimit = 0.000000001 'jrs 2022/04/04
                        MaxLimit = 1000
                        bApplyLimits = True

                    'mti 2024/07/23 start
                    Case "ENP_Solar"
                        series = Me.cachedENPSOLAR
                        MinLimit = 0.000000001 'jrs 2022/04/04
                        MaxLimit = 1000
                        bApplyLimits = True
                    Case "ENP_Wind"
                        series = Me.cachedENPWIND
                        MinLimit = 0.000000001 'jrs 2022/04/04
                        MaxLimit = 1000
                        bApplyLimits = True
                    Case "ENP_Geothermal"
                        series = Me.cachedENPGEOTHERMAL
                        MinLimit = 0.000000001 'jrs 2022/04/04
                        MaxLimit = 1000
                        bApplyLimits = True
                    'mti 2024/07/23 end
                    Case "ENP_OthRenew"
                        series = Me.cachedENPOTHREN
                        MinLimit = 0.000000001 'jrs 2022/04/04
                        MaxLimit = 1000
                        bApplyLimits = True


                    Case "RESER_Oil"
                        series = Me.cachedRESOIL
                        MinLimit = 0
                        MaxLimit = 10000
                        bApplyLimits = True
                    Case "RESER_Gas"
                        series = Me.cachedRESGAS
                        MinLimit = 0
                        MaxLimit = 10000
                        bApplyLimits = True
                    Case "RESER_Coal"
                        series = Me.cachedRESCOAL
                        MinLimit = 0
                        MaxLimit = 10000
                        bApplyLimits = True
                    Case "RESER_Hydro"
                        series = Me.cachedRESHYD
                        MinLimit = 0
                        MaxLimit = 10000
                        bApplyLimits = True
                    Case "ENPRR_Oil"
                        series = Me.cachedENPRROil
                        bAvg = True
                        MinLimit = -0.1
                        MaxLimit = 0.5
                        bApplyLimits = True
                    Case "ENPRR_Gas"
                        series = Me.cachedENPRRGas
                        bAvg = True
                        MinLimit = -0.1
                        MaxLimit = 0.5
                        bApplyLimits = True
                    Case "ENPRR_Coal"
                        series = Me.cachedENPRRCoal
                        bAvg = True
                        MinLimit = -0.1
                        MaxLimit = 0.5
                        bApplyLimits = True
                    Case "ENPRR_Hydro"
                        series = Me.cachedENPRRHyd
                        bAvg = True
                        MinLimit = -0.1
                        MaxLimit = 0.5
                        bApplyLimits = True
                    Case "ENPRR_Nuclear"
                        series = Me.cachedENPRRNuc
                        bAvg = True
                        MinLimit = -0.1
                        MaxLimit = 0.5
                        bApplyLimits = True
                    'mti 2024/07/23 start
                    Case "ENPRR_Solar"
                        series = Me.cachedENPRRSOLAR
                        bAvg = True
                        MinLimit = -0.1
                        MaxLimit = 0.5
                        bApplyLimits = True
                    Case "ENPRR_Wind"
                        series = Me.cachedENPRRWIND
                        bAvg = True
                        MinLimit = -0.1
                        MaxLimit = 0.5
                        bApplyLimits = True
                    Case "ENPRR_Geothermal"
                        series = Me.cachedENPRRGEOTHERMAL
                        bAvg = True
                        MinLimit = -0.1
                        MaxLimit = 0.5
                        bApplyLimits = True
                    'mti 2024/07/23 end

                    Case "ENPRR_OthRenew"
                        series = Me.cachedENPRRRen
                        bAvg = True
                        MinLimit = -0.1
                        MaxLimit = 0.5
                        bApplyLimits = True

                    Case "resor_Oil"
                        series = Me.cachedRESOROIL
                        MinLimit = 0
                        MaxLimit = 10000
                        bApplyLimits = True
                    Case "resor_Gas"
                        series = Me.cachedRESORGAS
                        MinLimit = 0
                        MaxLimit = 10000
                        bApplyLimits = True
                    Case "resor_Coal"
                        series = Me.cachedRESORCOAL
                        MinLimit = 0
                        MaxLimit = 10000
                        bApplyLimits = True
                    Case "resor_Hydro"
                        series = Me.cachedRESHYD
                        MinLimit = 0
                        MaxLimit = 10000
                        bApplyLimits = True

                        'mti 2024/07/23 start
                    Case "resor_Solar"
                        series = Me.cachedRESORSOLAR
                        MinLimit = 0
                        MaxLimit = 10000
                        bApplyLimits = True

                    Case "resor_Wind"
                        series = Me.cachedRESORWIND
                        MinLimit = 0
                        MaxLimit = 10000
                        bApplyLimits = True

                    Case "resor_Geothermal"
                        series = Me.cachedRESORGEOTHERMAL
                        MinLimit = 0
                        MaxLimit = 10000
                        bApplyLimits = True


                    Case "resor_OthRenew"
                        series = Me.ResorRenew
                        MinLimit = 0
                        MaxLimit = 10000
                        bApplyLimits = True

                        'mti 2024/07/23 end

                    Case "resoruncon_Oil"
                        series = Me.cachedRESOROILUN
                    Case "resoruncon_Gas"
                        series = Me.cachedRESORGASUN
                    Case "enpoilmax"
                        series = Me.CENPOILMAX
                        MinLimit = 0
                        MaxLimit = 1000
                        bApplyLimits = True
                    Case "enpgasmax"
                        series = Me.CENPGASMAX
                        MinLimit = 0
                        MaxLimit = 1000
                        bApplyLimits = True
                    Case "enpcoalmax"
                        series = Me.CENPCOALMAX
                        MinLimit = 0
                        MaxLimit = 1000
                        bApplyLimits = True
                    Case "enresunce"
                        series = Me.CENRESUNCE
                        bAvg = True
                        MinLimit = 0
                        MaxLimit = 1
                        bApplyLimits = True
                    Case "QE_Oil"
                        series = Me.cachedRESOIL
                    Case "QE_Gas"
                        series = Me.cachedRESGAS
                    Case "QE_Coal"
                        series = Me.cachedRESCOAL
                    Case "QE_Hydro"
                        series = Me.cachedRESHYD
                    Case "QE_Nuclear"
                    Case "QE_Renew"
                    Case "QEUNCON_Oil"
                    Case "prodtf_Oil"
                        series = Me.cachedRESOIL
                    Case "prodtf_Gas"
                        series = Me.cachedRESGAS
                    Case "prodtf_Coal"
                        series = Me.cachedRESCOAL
                    Case "prodtf_Hydro"
                        series = Me.cachedRESHYD
                    Case "rdm_Oil"
                        Return rdm_Oil
                    Case "rdm_Gas"
                        Return rdm_Gas
                    Case "rdm_Coal"
                        Return rdm_Coal
                    Case "rdm_Hydro"
                        Return rdm_Hyd
                    Case Else
                        'Throw New ArgumentException($"Aggregator cannot be applied on series {seriesName}.")
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, Nothing)
                        Next
                End Select

                If seriesName = "QE_Oil" Then
                    Dim Regs As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                    For Each key In (From s In series
                                     Join e In cachedENPOil On s.Key Equals e.Key
                                     Join r In cntx.Regionalization On s.Key Equals r.Key
                                     Group Value = s.Value, ENP = e.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value), TotalENP = Sum(ENP)
            )
                        If key.TotalVal = 0 And key.TotalENP = 0 Then
                            Regs.Add(key.RegionId, QEHold(1) - 53)
                        Else
                            Regs.Add(key.RegionId, QEHold(1) -
                            Amin(53.0F, 10.0F * key.TotalVal / (AMAX(key.TotalENP, 0.0001F) * 15.0F)))
                        End If
                        Regs(key.RegionId) = ApplyLimits(Regs(key.RegionId), 0.01, 1000)
                    Next

                    Return Regs
                ElseIf seriesName = "QE_Gas" Then
                    Dim Regs As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                    For Each key In (From s In series
                                     Join e In cachedENPGas On s.Key Equals e.Key
                                     Join r In cntx.Regionalization On s.Key Equals r.Key
                                     Group Value = s.Value, ENP = e.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value), TotalENP = Sum(ENP)
            )
                        'jrs 2022/04/11
                        If key.TotalENP = 0 Then
                            Regs.Add(key.RegionId, QEHold(2) - 53)
                        Else
                            Regs.Add(key.RegionId, QEHold(2) -
                            Amin(53, 10 * key.TotalVal / (key.TotalENP * 15)))
                        End If
                        Regs(key.RegionId) = ApplyLimits(Regs(key.RegionId), 0.01, 1000)
                    Next

                    Return Regs
                ElseIf seriesName = "QE_Coal" Then
                    Dim Regs As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                    For Each key In cntx.GetDimension(1).Buckets
                        Regs.Add(key.Key, QEHold(3))
                    Next
                    Return Regs
                ElseIf seriesName = "QE_Hydro" Then
                    Dim Regs As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                    For Each key In cntx.GetDimension(1).Buckets
                        Regs.Add(key.Key, QEHold(4))
                    Next
                    Return Regs
                ElseIf seriesName = "QE_Nuclear" Then
                    Dim Regs As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                    For Each key In cntx.GetDimension(1).Buckets
                        Regs.Add(key.Key, QEHold(5))
                    Next
                    Return Regs
                    'mti 2024/07/23 start
                ElseIf seriesName = "QE_Solar" Then
                    Dim Regs As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                    For Each key In cntx.GetDimension(1).Buckets
                        Regs.Add(key.Key, QEHold(6))
                    Next
                    Return Regs
                ElseIf seriesName = "QE_Wind" Then
                    Dim Regs As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                    For Each key In cntx.GetDimension(1).Buckets
                        Regs.Add(key.Key, QEHold(7))
                    Next
                    Return Regs
                ElseIf seriesName = "QE_Geothermal" Then
                    Dim Regs As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                    For Each key In cntx.GetDimension(1).Buckets
                        Regs.Add(key.Key, QEHold(8))
                    Next
                    Return Regs
                    'mti 2024/07/23 end
                ElseIf seriesName = "QE_OthRenew" Then
                    Dim Regs As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                    For Each key In cntx.GetDimension(1).Buckets
                        Regs.Add(key.Key, QEHold(9))
                    Next
                    Return Regs

                ElseIf seriesName = "QEUNCON_Oil" Then
                    Dim Regs As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                    For Each key In cntx.GetDimension(1).Buckets
                        Regs.Add(key.Key, QEHold(7))
                    Next
                    Return Regs
                ElseIf seriesName = "prodtf_Oil" Then
                    Dim Regs As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                    For Each key In (From s In series
                                     Join e In cachedENPOil On s.Key Equals e.Key
                                     Join r In cntx.Regionalization On s.Key Equals r.Key
                                     Group Value = s.Value, ENP = e.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value), TotalENP = Sum(ENP)
            )
                        If key.TotalVal = 0 And key.TotalENP = 0 Then
                            Regs.Add(key.RegionId, 1)
                        Else
                            Regs.Add(key.RegionId, Amin(15.0F, key.TotalVal / AMAX(key.TotalENP, 0.0001F)))
                        End If
                        Regs(key.RegionId) = ApplyLimits(Regs(key.RegionId), 0, 500)
                        rdm_Oil.Add(key.RegionId, ApplyLimits(CSng(1 + 1.5 * Amin(1, AMAX(0, (Regs(key.RegionId) - 15) / 5))), 0.5, 5))
                    Next

                    Return Regs
                ElseIf seriesName = "prodtf_Gas" Then
                    Dim Regs As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                    For Each key In (From s In series
                                     Join e In cachedENPGas On s.Key Equals e.Key
                                     Join r In cntx.Regionalization On s.Key Equals r.Key
                                     Group Value = s.Value, ENP = e.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value), TotalENP = Sum(ENP)
            )
                        If key.TotalVal = 0 And key.TotalENP = 0 Then
                            Regs.Add(key.RegionId, 1)
                        Else
                            'jrs 2022/04/11
                            If key.TotalENP = 0 Then
                                Regs.Add(key.RegionId, 20)
                            Else
                                Regs.Add(key.RegionId, Amin(20, key.TotalVal / key.TotalENP))
                            End If
                        End If
                        Regs(key.RegionId) = ApplyLimits(Regs(key.RegionId), 0, 500)
                        rdm_Gas.Add(key.RegionId, ApplyLimits(CSng(1 + 1.5 * Amin(1, AMAX(0, (Regs(key.RegionId) - 15) / 5))), 0.5, 5))
                    Next

                    Return Regs
                ElseIf seriesName = "prodtf_Coal" Then
                    Dim Regs As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                    For Each key In (From s In series
                                     Join e In cachedENPCoal On s.Key Equals e.Key
                                     Join r In cntx.Regionalization On s.Key Equals r.Key
                                     Group Value = s.Value, ENP = e.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value), TotalENP = Sum(ENP)
            )
                        If key.TotalVal = 0 And key.TotalENP = 0 Then
                            Regs.Add(key.RegionId, 1)
                        Else
                            'jrs 2022/04/11
                            If key.TotalENP = 0 Then
                                Regs.Add(key.RegionId, 100)
                            Else
                                Regs.Add(key.RegionId, Amin(15, key.TotalVal / key.TotalENP))
                                If key.TotalVal / key.TotalENP > 200 Then Regs(key.RegionId) = 100
                            End If
                        End If
                        Regs(key.RegionId) = ApplyLimits(Regs(key.RegionId), 0, 500)
                        rdm_Coal.Add(key.RegionId, ApplyLimits(CSng(1 + 1.5 * Amin(1, AMAX(0, (Regs(key.RegionId) - 15) / 5))), 0.5, 5))
                    Next

                    Return Regs
                ElseIf seriesName = "prodtf_Hydro" Then
                    Dim Regs As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                    For Each key In (From s In series
                                     Join e In cachedENPHYD On s.Key Equals e.Key
                                     Join r In cntx.Regionalization On s.Key Equals r.Key
                                     Group Value = s.Value, ENP = e.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value), TotalENP = Sum(ENP)
            )
                        If key.TotalVal = 0 And key.TotalENP = 0 Then
                            Regs.Add(key.RegionId, 1)
                        Else
                            'jrs 2022/04/11
                            If key.TotalENP = 0 Then
                                Regs.Add(key.RegionId, 2)
                            Else
                                Regs.Add(key.RegionId, Amin(2, key.TotalVal / key.TotalENP))
                            End If
                        End If
                        Regs(key.RegionId) = ApplyLimits(Regs(key.RegionId), 0, 500)
                        rdm_Hyd.Add(key.RegionId, ApplyLimits(CSng(1 + 1.5 * Amin(1, AMAX(0, (Regs(key.RegionId) - 2) / 5))), 0.5, 5))
                    Next

                    Return Regs
                ElseIf Not bAvg Then
                    If bApplyLimits Then
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
                        ).ToDictionary(
                            Function(grp) grp.RegionId,
                            Function(grp) ApplyLimits(grp.TotalVal, MinLimit, MaxLimit)
                            )
                    Else
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
                        ).ToDictionary(
                            Function(grp) grp.RegionId,
                            Function(grp) grp.TotalVal
                            )
                    End If
                Else
                    If bApplyLimits Then
                        Return (From s In series
                                Join w In Me.CGDP On s.Key Equals w.Key
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                        ).ToDictionary(
                            Function(grp) grp.RegionId,
                            Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, MinLimit, MaxLimit)
                            )
                    Else
                        Return (From s In series
                                Join w In Me.CGDP On s.Key Equals w.Key
                                Join r In cntx.Regionalization On s.Key Equals r.Key
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
