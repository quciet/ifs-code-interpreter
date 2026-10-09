Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data
Imports Microsoft.EntityFrameworkCore.Metadata.Internal

Namespace Models

    Partial Public Module Agriculture

        <RebuildsVariable("LANDAREA", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("LD", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("landarablepot", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("landarablepotmed", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("landarablepotlow", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("CLPC", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("PROTEINPC", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("CLAVAL", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("MALNCHP", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("MALNPOPP", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("FDEM", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("FMDEM", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("FEDDEM", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("INDEM", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("AGLOSSTRANS", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("AGP", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("AGM", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("AGX", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("IALK", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("tgrld", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("TGRYL", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("LVHERD", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("AGPMILKEGGS", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("AGFISHCATCH", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("AQUACUL", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("watres", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("WATUSEPC", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("CLPCCV", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("CLPCMDER", AggregatorType:=GetType(AGRIAggregator))>
        <RebuildsVariable("YIELDINT", AggregatorType:=GetType(AGRIAggregator))>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        <ImportsAsset("CGDPPC")>
        <ImportsAsset("CGDP")>
        Public Iterator Function RebuildAgri(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Dim CGDPPCP As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
            'jrs 2024/06/14
            Dim TbName As String = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Built Land/Capita (" & BaseYear & ") Linear"
            TbName = getFunctionNameYrOrMstRct(TbName, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            'context.PutEstimator("LD_Urban", context.GetRegression("GDP/Capita (PPP 2011) Versus Built Land/Capita (2010) Linear").ToEstimator(CGDPPCP))
            context.PutEstimator("LD_Urban", context.GetRegression(TbName).ToEstimator(CGDPPCP))

            Yield context.Query("LANDAREA").Filter(BaseYearOnly).Execute()
            Yield context.Query("LD").Filter(BaseYearOnly).Execute()
            Dim dftmp As IDataframe
            Dim R As Short
            Dim dfIdx As Integer
            dftmp = context.Query("landarablepot").Filter(BaseYearOnly).Execute()
            Dim dflandarablepot As Dataframe = Dataframe.Create("landarablepot", context, context.FirstYear, context.LastYear, 0)
            For R = 1 To CShort(NR)
                dfIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR))
                For y = FirstYear To LastYear
                    dflandarablepot(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(dfIdx)
                Next
            Next
            Yield dflandarablepot

            'wv 2021/08/12 added med and low
            dftmp = context.Query("landarablepotmed").Filter(BaseYearOnly).Execute()
            Dim dflandarablepotmed As Dataframe = Dataframe.Create("landarablepotmed", context, context.FirstYear, context.LastYear, 0)
            For R = 1 To CShort(NR)
                dfIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR))
                For y = FirstYear To LastYear
                    dflandarablepotmed(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(dfIdx)
                Next
            Next
            Yield dflandarablepotmed

            dftmp = context.Query("landarablepotlow").Filter(BaseYearOnly).Execute()
            Dim dflandarablepotlow As Dataframe = Dataframe.Create("landarablepotlow", context, context.FirstYear, context.LastYear, 0)
            For R = 1 To CShort(NR)
                dfIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR))
                For y = FirstYear To LastYear
                    dflandarablepotlow(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(dfIdx)
                Next
            Next
            Yield dflandarablepotlow

            Yield context.Query("CLPC").Filter(BaseYearOnly).Execute()
            Yield context.Query("PROTEINPC").Filter(BaseYearOnly).Execute()
            Yield context.Query("CLAVAL").Filter(BaseYearOnly).Execute()
            Yield context.Query("MALNCHP").Filter(BaseYearOnly).Execute()
            Yield context.Query("MALNPOPP").Filter(BaseYearOnly).Execute()
            Yield context.Query("FDEM").Filter(BaseYearOnly).Execute()
            Yield context.Query("FMDEM").Filter(BaseYearOnly).Execute()
            Yield context.Query("FEDDEM").Filter(BaseYearOnly).Execute()
            Yield context.Query("INDEM").Filter(BaseYearOnly).Execute()
            Yield context.Query("AGLOSSTRANS").Filter(BaseYearOnly).Execute()
            Yield context.Query("AGP").Filter(BaseYearOnly).Execute()
            Yield context.Query("AGM").Filter(BaseYearOnly).Execute()
            Yield context.Query("AGX").Filter(BaseYearOnly).Execute()
            'AGMVALUE and AGXVALUE are saved in Economy after normalization
            'Yield context.Query("AGMVALUE").Filter(BaseYearOnly).Execute()
            'Yield context.Query("AGXVALUE").Filter(BaseYearOnly).Execute()
            Yield context.Query("IALK").Filter(BaseYearOnly).Execute()

            'jrs 2019/11/26
            dftmp = context.Query("tgrld").Filter(BaseYearOnly).Execute()
            Dim dftgrld As Dataframe = Dataframe.Create("tgrld", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    dftgrld(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR)))
                Next
            Next
            Yield dftgrld

            Yield context.Query("TGRYL").Filter(BaseYearOnly).Execute()
            Yield context.Query("LVHERD").Filter(BaseYearOnly).Execute()
            Yield context.Query("AGPMILKEGGS").Filter(BaseYearOnly).Execute()
            Yield context.Query("AGFISHCATCH").Filter(BaseYearOnly).Execute()
            Yield context.Query("AQUACUL").Filter(BaseYearOnly).Execute()
            dftmp = context.Query("watres").Filter(BaseYearOnly).Execute()
            Dim dfwatres As Dataframe = Dataframe.Create("watres", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    dfwatres(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR)))
                Next
            Next
            Yield dfwatres
            Yield context.Query("WATUSEPC").Filter(BaseYearOnly).Execute()
            'jrs 2020/12/10
            Yield context.Query("CLPCCV").Filter(BaseYearOnly).Execute()
            Yield context.Query("CLPCMDER").Filter(BaseYearOnly).Execute()
            'wv 2020/12/11
            Yield context.Query("YIELDINT").Filter(BaseYearOnly).Execute()
        End Function

        Class AGRIAggregator
            Inherits DefaultSumAggregator

            Private Property CLandArea As IDictionary(Of String, Single?)
            Private Property LDCrop As IDictionary(Of String, Single?)
            Private Property LDGraz As IDictionary(Of String, Single?)
            Private Property LDFor As IDictionary(Of String, Single?)
            Private Property LDOth As IDictionary(Of String, Single?)
            Private Property LDUrb As IDictionary(Of String, Single?)
            Private Property CLdArabPot As IDictionary(Of String, Single?)
            'wv 2021/08/12 added medium and low dimension
            Private Property CLdArabPotMed As IDictionary(Of String, Single?)
            Private Property CLdArabPotLow As IDictionary(Of String, Single?)

            'wv 2020/12/11 agpy added yield intensity variable
            Private Property CYIELDINT As IDictionary(Of String, Single?)

            Private Property AgFishCalPerCapPerDayFAO As IDictionary(Of String, Dictionary(Of Integer, Single?))
            Private Property CCLPCFISH As IDictionary(Of String, Single?)
            Private Property CCLPCAQPLANTS As IDictionary(Of String, Single?)
            Private Property CCLPCFISHOILS As IDictionary(Of String, Single?)

            Private Property AgFishProteinPerCapPerDayFAO As IDictionary(Of String, Dictionary(Of Integer, Single?))
            Private Property CPROTEINPCFISH As IDictionary(Of String, Single?)
            Private Property CPROTEINPCAQPLANTS As IDictionary(Of String, Single?)
            Private Property CPROTEINPCFISHOILS As IDictionary(Of String, Single?)

            Private Property AgFishGramsPerCapPerDayFAO As IDictionary(Of String, Dictionary(Of Integer, Single?))
            Private Property CGRAMSPCFISH As IDictionary(Of String, Single?)
            Private Property CGRAMSPCAQPLANTS As IDictionary(Of String, Single?)
            Private Property CGRAMSPCFISHOILS As IDictionary(Of String, Single?)

            Private Property CPOP As IDictionary(Of String, Single?)
            Private Property CGDPPCP As IDictionary(Of String, Single?)
            Private Property CGDPPC As IDictionary(Of String, Single?)
            'jrs 2023/08/02
            Private Property CGDP As IDictionary(Of String, Single?)

            Private Property CCLPC_Crop As IDictionary(Of String, Single?)
            Private Property CCLPC_Meat As IDictionary(Of String, Single?)
            Private Property CCLPC_Fish As IDictionary(Of String, Single?)
            Private Property CCLPC_Tot As IDictionary(Of String, Single?)

            Private Property CPROTEINPC_Crop As IDictionary(Of String, Single?)
            Private Property CPROTEINPC_Meat As IDictionary(Of String, Single?)
            Private Property CPROTEINPC_Fish As IDictionary(Of String, Single?)
            Private Property CPROTEINPC_Tot As IDictionary(Of String, Single?)

            Private Property CGRAMSPC_Crop As IDictionary(Of String, Single?)
            Private Property CGRAMSPC_Meat As IDictionary(Of String, Single?)
            Private Property CGRAMSPC_Fish As IDictionary(Of String, Single?)
            Private Property CGRAMSPC_Tot As IDictionary(Of String, Single?)

            Private Property CMALNCHP As IDictionary(Of String, Single?)
            Private Property CMALNPOPP As IDictionary(Of String, Single?)
            'jrs 2022/07/18
            Private Property CMALNPOPP2_5LIM As IDictionary(Of String, Boolean)
            'jrs 2020/12/10
            Private Property CCLPCCV As IDictionary(Of String, Single?)
            Private Property CCLPCMDER As IDictionary(Of String, Single?)

            Private Property AgFishtoFoodFAO As IDictionary(Of String, Dictionary(Of Integer, Single?))
            Private Property AgFishtoFeedFAO As IDictionary(Of String, Dictionary(Of Integer, Single?))
            Private Property AgFishtoSeedFAO As IDictionary(Of String, Dictionary(Of Integer, Single?))
            Private Property AgFishtoOtherUtilFAO As IDictionary(Of String, Dictionary(Of Integer, Single?))

            Private Property CFDEM_Crop As IDictionary(Of String, Single?)
            Private Property CFDEM_Meat As IDictionary(Of String, Single?)
            Private Property CFDEM_Fish As IDictionary(Of String, Single?)

            Private Property CFMDEM_Crop As IDictionary(Of String, Single?)
            Private Property CFMDEM_Meat As IDictionary(Of String, Single?)
            Private Property CFMDEM_Fish As IDictionary(Of String, Single?)

            Private Property CFEDDEM_Crop As IDictionary(Of String, Single?)
            Private Property CFEDDEM_Meat As IDictionary(Of String, Single?)
            Private Property CFEDDEM_Fish As IDictionary(Of String, Single?)

            Private Property CINDEM_Crop As IDictionary(Of String, Single?)
            Private Property CINDEM_Meat As IDictionary(Of String, Single?)
            Private Property CINDEM_Fish As IDictionary(Of String, Single?)

            Private Property CAGLOSSTRANS_Crop As IDictionary(Of String, Single?)
            Private Property CAGLOSSTRANS_Meat As IDictionary(Of String, Single?)
            Private Property CAGLOSSTRANS_Fish As IDictionary(Of String, Single?)

            Private Property CSEEDDEM_Crop As IDictionary(Of String, Single?)
            Private Property CSEEDDEM_Meat As IDictionary(Of String, Single?)
            Private Property CSEEDDEM_Fish As IDictionary(Of String, Single?)

            Private Property AgFishProd As IDictionary(Of String, Dictionary(Of Integer, Single?))
            Private Property AgFishImports As IDictionary(Of String, Dictionary(Of Integer, Single?))
            Private Property AgFishExports As IDictionary(Of String, Dictionary(Of Integer, Single?))
            Private Property AgFishDomesticSupply As IDictionary(Of String, Dictionary(Of Integer, Single?))
            Private Property AgFishAquaProdFSJ As IDictionary(Of String, Dictionary(Of Integer, Single?))
            Private Property AgFishCatchProdFSJ As IDictionary(Of String, Dictionary(Of Integer, Single?))
            Private Property AgFishAquaProd As IDictionary(Of String, Dictionary(Of Integer, Single?))
            Private Property AgFishCatchProd As IDictionary(Of String, Dictionary(Of Integer, Single?))

            Private Property CAGFISHCATCH As IDictionary(Of String, Single?)
            Private Property CAQUACUL As IDictionary(Of String, Single?)
            Private Property AGMFish As IDictionary(Of String, Single?)
            Private Property AGXFish As IDictionary(Of String, Single?)
            Private Property CAGDOMSUPP_Fish As IDictionary(Of String, Single?)

            Private Property AGPTot As IDictionary(Of String, Single?)
            'jrs 2026/03/27
            Private Property bAGPTotData As IDictionary(Of String, Boolean)

            Private Property MeatProd As IDictionary(Of String, Single?)
            Private Property BovineMeatProd As IDictionary(Of String, Single?)
            Private Property MuttonAndGoatMeatProd As IDictionary(Of String, Single?)
            Private Property PigMeatProd As IDictionary(Of String, Single?)
            Private Property PoultryMeatProd As IDictionary(Of String, Single?)
            Private Property MeatOtherProd As IDictionary(Of String, Single?)
            Private Property TotalAnimalProd As IDictionary(Of String, Single?)
            Private Property LiveHerd As IDictionary(Of String, Single?)
            Private Property CAGPMILKEGGS As IDictionary(Of String, Single?)

            Private Property AGMCrop As IDictionary(Of String, Single?)
            Private Property AGXCrop As IDictionary(Of String, Single?)
            Private Property AGMMeat As IDictionary(Of String, Single?)
            Private Property AGXMeat As IDictionary(Of String, Single?)

            Private Property CAGDOMSUPP_Crop As IDictionary(Of String, Single?)
            Private Property CAGDOMSUPP_Meat As IDictionary(Of String, Single?)

            Private Property AGMCropQuantTrade As IDictionary(Of String, Single?)
            Private Property AGXCropQuantTrade As IDictionary(Of String, Single?)
            Private Property AGMCropValueTrade As IDictionary(Of String, Single?)
            Private Property AGXCropValueTrade As IDictionary(Of String, Single?)
            Private Property AGMMeatQuantTrade As IDictionary(Of String, Single?)
            Private Property AGXMeatQuantTrade As IDictionary(Of String, Single?)
            Private Property AGMMeatValueTrade As IDictionary(Of String, Single?)
            Private Property AGXMeatValueTrade As IDictionary(Of String, Single?)
            Private Property AGMFishQuantTrade As IDictionary(Of String, Single?)
            Private Property AGXFishQuantTrade As IDictionary(Of String, Single?)
            Private Property AGMFishValueTrade As IDictionary(Of String, Single?)
            Private Property AGXFishValueTrade As IDictionary(Of String, Single?)

            Private Property CWATRES As IDictionary(Of String, Single?)
            Private Property CWATUSEPC As IDictionary(Of String, Single?)

            Private Property CAGPGR As IDictionary(Of String, Single?)
            Private Property CIALK As IDictionary(Of String, Single?)
            Private Property CTGRLD As IDictionary(Of String, Single?)
            Private Property CTGRYL As IDictionary(Of String, Single?)

            Private Property processed As Boolean

            Private Property slr As Single?

            <ExportsAsset("CLandArea")>
            <ExportsAsset("LDCrop")>
            <ExportsAsset("AGMCropValueTrade")>
            <ExportsAsset("AGXCropValueTrade")>
            <ExportsAsset("AGMMeatValueTrade")>
            <ExportsAsset("AGXMeatValueTrade")>
            <ExportsAsset("AGMFishValueTrade")>
            <ExportsAsset("AGXFishValueTrade")>
            <ExportsAsset("CCLPC_Tot")>
            <ExportsAsset("CMALNCHP")>
            Private Sub PreProcess(cntx As IRebuildFacade)
                If Me.processed Then
                    Return
                End If

                Dim objWriter = New System.IO.StreamWriter(cntx.PutFdrvRun + "progress.txt", append:=True)
                objWriter.WriteLine("Processing Data on Agriculture - Please Wait")
                objWriter.Close()

                Me.CLandArea = New Dictionary(Of String, Single?)
                Me.LDCrop = New Dictionary(Of String, Single?)

                Me.LDGraz = New Dictionary(Of String, Single?)
                Me.LDFor = New Dictionary(Of String, Single?)
                Me.LDOth = New Dictionary(Of String, Single?)
                Me.LDUrb = New Dictionary(Of String, Single?)
                Me.CLdArabPot = New Dictionary(Of String, Single?)
                'wv 2021/08/12
                Me.CLdArabPotMed = New Dictionary(Of String, Single?)
                Me.CLdArabPotLow = New Dictionary(Of String, Single?)

                'wv 2020/12/11 agpa
                Me.CYIELDINT = New Dictionary(Of String, Single?)

                Me.AgFishCalPerCapPerDayFAO = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Me.CCLPCFISH = New Dictionary(Of String, Single?)
                Me.CCLPCAQPLANTS = New Dictionary(Of String, Single?)
                Me.CCLPCFISHOILS = New Dictionary(Of String, Single?)

                Me.AgFishProteinPerCapPerDayFAO = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Me.CPROTEINPCFISH = New Dictionary(Of String, Single?)
                Me.CPROTEINPCAQPLANTS = New Dictionary(Of String, Single?)
                Me.CPROTEINPCFISHOILS = New Dictionary(Of String, Single?)

                Me.AgFishGramsPerCapPerDayFAO = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Me.CGRAMSPCFISH = New Dictionary(Of String, Single?)
                Me.CGRAMSPCAQPLANTS = New Dictionary(Of String, Single?)
                Me.CGRAMSPCFISHOILS = New Dictionary(Of String, Single?)

                Me.CPOP = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
                Me.CGDPPCP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
                Me.CGDPPC = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPC")
                'jrs 2023/08/02
                Me.CGDP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDP")

                Me.CCLPC_Crop = New Dictionary(Of String, Single?)
                Me.CCLPC_Meat = New Dictionary(Of String, Single?)
                Me.CCLPC_Fish = New Dictionary(Of String, Single?)
                Me.CCLPC_Tot = New Dictionary(Of String, Single?)

                Me.CPROTEINPC_Crop = New Dictionary(Of String, Single?)
                Me.CPROTEINPC_Meat = New Dictionary(Of String, Single?)
                Me.CPROTEINPC_Fish = New Dictionary(Of String, Single?)
                Me.CPROTEINPC_Tot = New Dictionary(Of String, Single?)

                Me.CGRAMSPC_Crop = New Dictionary(Of String, Single?)
                Me.CGRAMSPC_Meat = New Dictionary(Of String, Single?)
                Me.CGRAMSPC_Fish = New Dictionary(Of String, Single?)
                Me.CGRAMSPC_Tot = New Dictionary(Of String, Single?)

                Me.CMALNCHP = New Dictionary(Of String, Single?)
                Me.CMALNPOPP = New Dictionary(Of String, Single?)

                'jrs 2022/07/18
                Me.CMALNPOPP2_5LIM = New Dictionary(Of String, Boolean)

                'jrs 2020/12/10
                Me.CCLPCCV = New Dictionary(Of String, Single?)
                Me.CCLPCMDER = New Dictionary(Of String, Single?)

                Me.AgFishtoFoodFAO = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Me.AgFishtoFeedFAO = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Me.AgFishtoSeedFAO = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Me.AgFishtoOtherUtilFAO = New Dictionary(Of String, Dictionary(Of Integer, Single?))

                Me.CFDEM_Crop = New Dictionary(Of String, Single?)
                Me.CFDEM_Meat = New Dictionary(Of String, Single?)
                Me.CFDEM_Fish = New Dictionary(Of String, Single?)

                Me.CFMDEM_Crop = New Dictionary(Of String, Single?)
                Me.CFMDEM_Meat = New Dictionary(Of String, Single?)
                Me.CFMDEM_Fish = New Dictionary(Of String, Single?)

                Me.CFEDDEM_Crop = New Dictionary(Of String, Single?)
                Me.CFEDDEM_Meat = New Dictionary(Of String, Single?)
                Me.CFEDDEM_Fish = New Dictionary(Of String, Single?)

                Me.CINDEM_Crop = New Dictionary(Of String, Single?)
                Me.CINDEM_Meat = New Dictionary(Of String, Single?)
                Me.CINDEM_Fish = New Dictionary(Of String, Single?)

                Me.CAGLOSSTRANS_Crop = New Dictionary(Of String, Single?)
                Me.CAGLOSSTRANS_Meat = New Dictionary(Of String, Single?)
                Me.CAGLOSSTRANS_Fish = New Dictionary(Of String, Single?)

                Me.CSEEDDEM_Crop = New Dictionary(Of String, Single?)
                Me.CSEEDDEM_Meat = New Dictionary(Of String, Single?)
                Me.CSEEDDEM_Fish = New Dictionary(Of String, Single?)

                Me.AgFishProd = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Me.AgFishImports = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Me.AgFishExports = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Me.AgFishDomesticSupply = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Me.AgFishAquaProdFSJ = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Me.AgFishCatchProdFSJ = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Me.AgFishAquaProd = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Me.AgFishCatchProd = New Dictionary(Of String, Dictionary(Of Integer, Single?))

                Me.CAGFISHCATCH = New Dictionary(Of String, Single?)
                Me.CAQUACUL = New Dictionary(Of String, Single?)
                Me.AGMFish = New Dictionary(Of String, Single?)
                Me.AGXFish = New Dictionary(Of String, Single?)
                Me.CAGDOMSUPP_Fish = New Dictionary(Of String, Single?)

                Me.AGPTot = New Dictionary(Of String, Single?)
                'jrs 2026/03/27
                Me.bAGPTotData = New Dictionary(Of String, Boolean)

                Me.MeatProd = New Dictionary(Of String, Single?)
                Me.BovineMeatProd = New Dictionary(Of String, Single?)
                Me.MuttonAndGoatMeatProd = New Dictionary(Of String, Single?)
                Me.PigMeatProd = New Dictionary(Of String, Single?)
                Me.PoultryMeatProd = New Dictionary(Of String, Single?)
                Me.MeatOtherProd = New Dictionary(Of String, Single?)
                Me.TotalAnimalProd = New Dictionary(Of String, Single?)
                Me.LiveHerd = New Dictionary(Of String, Single?)
                Me.CAGPMILKEGGS = New Dictionary(Of String, Single?)

                Me.AGMCrop = New Dictionary(Of String, Single?)
                Me.AGXCrop = New Dictionary(Of String, Single?)
                Me.AGMMeat = New Dictionary(Of String, Single?)
                Me.AGXMeat = New Dictionary(Of String, Single?)

                Me.CAGDOMSUPP_Crop = New Dictionary(Of String, Single?)
                Me.CAGDOMSUPP_Meat = New Dictionary(Of String, Single?)

                Me.AGMCropQuantTrade = New Dictionary(Of String, Single?)
                Me.AGXCropQuantTrade = New Dictionary(Of String, Single?)

                Me.AGMCropValueTrade = New Dictionary(Of String, Single?)
                Me.AGXCropValueTrade = New Dictionary(Of String, Single?)

                Me.AGMMeatQuantTrade = New Dictionary(Of String, Single?)
                Me.AGXMeatQuantTrade = New Dictionary(Of String, Single?)

                Me.AGMMeatValueTrade = New Dictionary(Of String, Single?)
                Me.AGXMeatValueTrade = New Dictionary(Of String, Single?)

                Me.AGMFishQuantTrade = New Dictionary(Of String, Single?)
                Me.AGXFishQuantTrade = New Dictionary(Of String, Single?)

                Me.AGMFishValueTrade = New Dictionary(Of String, Single?)
                Me.AGXFishValueTrade = New Dictionary(Of String, Single?)

                Me.CWATRES = New Dictionary(Of String, Single?)
                Me.CWATUSEPC = New Dictionary(Of String, Single?)

                Me.CAGPGR = New Dictionary(Of String, Single?)
                Me.CIALK = New Dictionary(Of String, Single?)
                Me.CTGRLD = New Dictionary(Of String, Single?)
                Me.CTGRYL = New Dictionary(Of String, Single?)

                Dim ValHold As Single?

                If cntx.HistoricMode Then
                    'ttt 2019/06/20
                    'mti 2019/04/29  Flex Hist Base Start
                    'CLandArea(ICount%) = getClosestYrVal(LandAreaTbl, FirstYearHistory, HistDist)
                    Dim yrs As Short() = cntx.GetYears("LANDAREA").OrderBy(Function(y) y).ToArray()

                    Dim LandAreaTbl As IDictionary(Of String, Single?()) = cntx.GetDice("LANDAREA", yrs)

                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(LandAreaTbl, yrs, key, cntx.FirstYear, HistDist)

                        CLandArea.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End
                    '======================
                    'mti 2019/04/29  Flex Hist Base Start

                    'LDCrop(ICount%) = getClosestYrVal(LDCropTbl, FirstYearHistory, HistDist)

                    yrs = cntx.GetYears("LD_Crop").OrderBy(Function(y) y).ToArray()

                    Dim LDCropTbl As IDictionary(Of String, Single?()) = cntx.GetDice("LD_Crop", yrs)

                    For Each key In cntx.Regionalization.Keys

                        ValHold = getClosestYrVal(LDCropTbl, yrs, key, cntx.FirstYear, HistDist)

                        LDCrop.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End
                    '======================
                    'mti 2019/04/29  Flex Hist Base Start

                    'LDGraz(ICount%) = getClosestYrVal(LdGrazTbl, FirstYearHistory, HistDist)
                    'SeriesLandGrazing
                    yrs = cntx.GetYears("LD_Grazing").OrderBy(Function(y) y).ToArray()

                    Dim LdGrazTbl As IDictionary(Of String, Single?()) = cntx.GetDice("LD_Grazing", yrs)

                    For Each key In cntx.Regionalization.Keys

                        ValHold = getClosestYrVal(LdGrazTbl, yrs, key, cntx.FirstYear, HistDist)

                        LDGraz.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End
                    '=======================
                    'mti 2019/04/29  Flex Hist Base Start

                    'LDFor(ICount%) = getClosestYrVal(LdForTbl, FirstYearHistory, HistDist)
                    'SeriesLandForest
                    yrs = cntx.GetYears("LD_Forest").OrderBy(Function(y) y).ToArray()

                    Dim LdForTbl As IDictionary(Of String, Single?()) = cntx.GetDice("LD_Forest", yrs)

                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(LdForTbl, yrs, key, cntx.FirstYear, HistDist)

                        LDFor.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End
                    '==========================
                    'mti 2019/04/29  Flex Hist Base Start

                    'LDOth(ICount%) = getClosestYrVal(LdOthTbl, FirstYearHistory, HistDist)
                    'SeriesLandOther
                    yrs = cntx.GetYears("LD_Other").OrderBy(Function(y) y).ToArray()

                    Dim LdOthTbl As IDictionary(Of String, Single?()) = cntx.GetDice("LD_Other", yrs)

                    For Each key In cntx.Regionalization.Keys

                        ValHold = getClosestYrVal(LdOthTbl, yrs, key, cntx.FirstYear, HistDist)

                        LDOth.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End
                    '==========================
                    'mti 2019/04/29  Flex Hist Base start - changed to the frequently used syntax from LdUrbTblCorine("Earliest") to LdUrbTblCorine.Fields("Earliest")
                    'mti 2019/04/29  Flex Hist Base Start

                    'LDUrb(ICount%) = getClosestYrVal(LdUrbTblCorine, FirstYearHistory, HistDist)
                    'SeriesLandBuiltGFNCorine
                    yrs = cntx.GetYears("LD_Urban").OrderBy(Function(y) y).ToArray()
                    Dim LdUrbTblCorine As IDictionary(Of String, Single?()) = cntx.GetDice("LD_Urban", yrs)
                    Dim yrsGAEZ As Short() = cntx.GetYears("SeriesLandBuiltGFNGAEZ").OrderBy(Function(y) y).ToArray()
                    Dim LdUrbTblGAEZ As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesLandBuiltGFNGAEZ", yrsGAEZ)
                    Dim yrsglc = cntx.GetYears("SeriesLandBuiltGFNglc").OrderBy(Function(y) y).ToArray()
                    Dim LdUrbTblGFN As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesLandBuiltGFNglc", yrsglc)
                    Dim yrsage = cntx.GetYears("SeriesLandBuiltGFNSage").OrderBy(Function(y) y).ToArray()
                    Dim LdUrbTblSAGE As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesLandBuiltGFNSage", yrsage)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(LdUrbTblCorine, yrs, key, cntx.FirstYear, HistDist)
                        'mti 2019/04/29  Flex Hist Base End
                        'mti 2019/04/29  Flex Hist Base Start

                        'If IsNull(LDUrb(ICount%)) Then LDUrb(ICount%) = getClosestYrVal(LdUrbTblGAEZ, FirstYearHistory, HistDist)
                        'SeriesLandBuiltGFNGAEZ
                        If Not ValHold.HasValue Then
                            ValHold = getClosestYrVal(LdUrbTblGAEZ, yrsGAEZ, key, cntx.FirstYear, HistDist)
                        End If

                        'mti 2019/04/29  Flex Hist Base End
                        'mti 2019/04/29  Flex Hist Base Start

                        'If IsNull(LDUrb(ICount%)) Then LDUrb(ICount%) = getClosestYrVal(LdUrbTblGFN, FirstYearHistory, HistDist)
                        'SeriesLandBuiltGFNglc
                        If Not ValHold.HasValue Then
                            ValHold = getClosestYrVal(LdUrbTblGFN, yrsglc, key, cntx.FirstYear, HistDist)
                        End If
                        'mti 2019/04/29  Flex Hist Base End
                        'mti 2019/04/29  Flex Hist Base Start

                        'If IsNull(LDUrb(ICount%)) Then LDUrb(ICount%) = getClosestYrVal(LdUrbTblSAGE, FirstYearHistory, HistDist)
                        'SeriesLandBuiltGFNSage
                        If Not ValHold.HasValue Then
                            ValHold = getClosestYrVal(LdUrbTblSAGE, yrsage, key, cntx.FirstYear, HistDist)
                        End If

                        LDUrb.Add(key, ValHold)
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                    'mti 2019/04/29  Flex Hist Base End
                    '===========================
                    'mti 2019/04/29  Flex Hist Base Start

                    'CLdArabPot(ICount%) = getClosestYrVal(LandArablePotTbl, FirstYearHistory, HistDist)
                    'SeriesLandPotentialArable
                    'wv 2020/12/11
                    yrs = cntx.GetYears("SeriesPotArableLandEitelbergHigh").OrderBy(Function(y) y).ToArray()
                    Dim yrs2 As Short() = cntx.GetYears("SeriesLandPotentialArable").OrderBy(Function(y) y).ToArray()
                    Dim yrs3 As Short() = cntx.GetYears("SeriesYieldIntWtMueller").OrderBy(Function(y) y).ToArray()
                    ' wv 2021/08/12
                    Dim yrsPotArableLandEitelbergMed As Short() = cntx.GetYears("SeriesPotArableLandEitelbergMed").OrderBy(Function(y) y).ToArray()
                    Dim yrsPotArableLandEitelbergLow As Short() = cntx.GetYears("SeriesPotArableLandEitelbergLow").OrderBy(Function(y) y).ToArray()

                    Dim LandArablePotTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesPotArableLandEitelbergHigh", yrs)
                    Dim LandArablePotTbl2 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesLandPotentialArable", yrs2)
                    Dim YlIntTable As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesYieldIntWtMueller", yrs3)
                    'wv 2021/08/12
                    Dim PotArableLandEitelbergMedTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesPotArableLandEitelbergMed", yrsPotArableLandEitelbergMed)
                    Dim PotArableLandEitelbergLowTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesPotArableLandEitelbergLow", yrsPotArableLandEitelbergLow)

                    For Each key In cntx.Regionalization.Keys

                        ValHold = getClosestYrVal(LandArablePotTbl, yrs, key, cntx.FirstYear, HistDist)
                        'wv 2020/12/11
                        If Not ValHold.HasValue Then
                            ValHold = getClosestYrVal(LandArablePotTbl2, yrs2, key, cntx.FirstYear, HistDist)
                            If ValHold.HasValue Then ValHold = ValHold / 1000
                        Else
                            ValHold = ValHold / 1000000
                        End If

                        CLdArabPot.Add(key, ValHold)
                        'jrs 2021/08/16
                        ValHold = getClosestYrVal(PotArableLandEitelbergMedTbl, yrsPotArableLandEitelbergMed, key, cntx.FirstYear, HistDist)
                        If Not ValHold.HasValue Then
                            ValHold = getClosestYrVal(LandArablePotTbl2, yrs2, key, cntx.FirstYear, HistDist)
                            If ValHold.HasValue Then ValHold = ValHold / 1000
                        Else
                            ValHold = ValHold / 1000000
                        End If
                        CLdArabPotMed.Add(key, ValHold)
                        ValHold = getClosestYrVal(PotArableLandEitelbergLowTbl, yrsPotArableLandEitelbergLow, key, cntx.FirstYear, HistDist)
                        If Not ValHold.HasValue Then
                            ValHold = getClosestYrVal(LandArablePotTbl2, yrs2, key, cntx.FirstYear, HistDist)
                            If ValHold.HasValue Then ValHold = ValHold / 1000
                        Else
                            ValHold = ValHold / 1000000
                        End If
                        CLdArabPotLow.Add(key, ValHold)

                        'wv 2020/12/11
                        ValHold = getClosestYrVal(YlIntTable, yrs3, key, cntx.FirstYear, 100)
                        CYIELDINT.Add(key, ValHold)
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else 'Forecast *********
                    Dim yearsLDCrop As Short()
                    yearsLDCrop = (From y In cntx.GetYears("LD_Crop")
                                   Where cntx.FirstYear - 20S < y
                                   Order By y Descending
                    ).Take(10).ToArray
                    Dim diceLDCrop As IDictionary(Of String, Single?()) = cntx.GetDice("LD_Crop", yearsLDCrop)

                    Dim yearsLDGraz As Short()
                    yearsLDGraz = (From y In cntx.GetYears("LD_Grazing")
                                   Where cntx.FirstYear - 20S < y
                                   Order By y Descending
                    ).Take(10).ToArray
                    Dim diceLDGraz As IDictionary(Of String, Single?()) = cntx.GetDice("LD_Grazing", yearsLDGraz)

                    Dim yearsLDFor As Short()
                    yearsLDFor = (From y In cntx.GetYears("LD_Forest")
                                  Where cntx.FirstYear - 20S < y
                                  Order By y Descending
                    ).Take(10).ToArray
                    Dim diceLDFor As IDictionary(Of String, Single?()) = cntx.GetDice("LD_Forest", yearsLDFor)

                    Dim yearsLDOth As Short()
                    yearsLDOth = (From y In cntx.GetYears("LD_Other")
                                  Where cntx.FirstYear - 20S < y
                                  Order By y Descending
                    ).Take(10).ToArray
                    Dim diceLDOth As IDictionary(Of String, Single?()) = cntx.GetDice("LD_Other", yearsLDOth)

                    Dim yearsLDUrb As Short()
                    yearsLDUrb = (From y In cntx.GetYears("LD_Urban")
                                  Where cntx.FirstYear - 20S < y
                                  Order By y Descending
                    ).Take(10).ToArray
                    Dim diceLDUrb As IDictionary(Of String, Single?()) = cntx.GetDice("LD_Urban", yearsLDUrb)

                    Dim yearsLDUrb2 As Short()
                    yearsLDUrb2 = (From y In cntx.GetYears("SeriesLandBuiltGFNGAEZ")
                                   Where cntx.FirstYear - 20S < y
                                   Order By y Descending
                    ).Take(10).ToArray
                    Dim diceLDUrb2 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesLandBuiltGFNGAEZ", yearsLDUrb2)

                    Dim yearsLDUrb3 As Short()
                    yearsLDUrb3 = (From y In cntx.GetYears("SeriesLandBuiltGFNglc")
                                   Where cntx.FirstYear - 20S < y
                                   Order By y Descending
                    ).Take(10).ToArray
                    Dim diceLDUrb3 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesLandBuiltGFNglc", yearsLDUrb3)

                    Dim yearsLDUrb4 As Short()
                    yearsLDUrb4 = (From y In cntx.GetYears("SeriesLandBuiltGFNSage")
                                   Where cntx.FirstYear - 20S < y
                                   Order By y Descending
                    ).Take(10).ToArray
                    Dim diceLDUrb4 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesLandBuiltGFNSage", yearsLDUrb4)

                    Dim UrbVal As Single?

                    'wv 2020/12/11 changed from SeriesLandPotentialArable to SeriesPotArableLandEitelbergHigh
                    'update potential arable land computation based on new data series
                    ' also updated the rationale if no data on arable land is available
                    For Each item In (From s In cntx.GetSlice("LANDAREA", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("LandTotal", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join c In cntx.GetSlice("LD_Crop", cntx.FirstYear, CoalescePolicy.Null) On s.Key Equals c.Key
                                      Join g In cntx.GetSlice("LD_Grazing", cntx.FirstYear, CoalescePolicy.Null) On s.Key Equals g.Key
                                      Join f In cntx.GetSlice("LD_Forest", cntx.FirstYear, CoalescePolicy.Null) On s.Key Equals f.Key
                                      Join o In cntx.GetSlice("LD_Other", cntx.FirstYear, CoalescePolicy.Null) On s.Key Equals o.Key
                                      Join u In cntx.GetSlice("LD_Urban", cntx.FirstYear, CoalescePolicy.Null) On s.Key Equals u.Key
                                      Join ug In cntx.GetSlice("SeriesLandBuiltGFNGAEZ", cntx.FirstYear, CoalescePolicy.Null) On s.Key Equals ug.Key
                                      Join ug2 In cntx.GetSlice("SeriesLandBuiltGFNglc", cntx.FirstYear, CoalescePolicy.Null) On s.Key Equals ug2.Key
                                      Join ug3 In cntx.GetSlice("SeriesLandBuiltGFNSage", cntx.FirstYear, CoalescePolicy.Null) On s.Key Equals ug3.Key
                                      Join a2 In cntx.GetSlice("SeriesPotArableLandEitelbergHigh", "MostRecent") On s.Key Equals a2.Key
                                      Join a3 In cntx.GetSlice("SeriesLandPotentialArable", "MostRecent") On s.Key Equals a3.Key
                                      Join a3Med In cntx.GetSlice("SeriesPotArableLandEitelbergMed", "MostRecent") On s.Key Equals a3Med.Key
                                      Join a3Low In cntx.GetSlice("SeriesPotArableLandEitelbergLow", "MostRecent") On s.Key Equals a3Low.Key
                                      Join y1 In cntx.GetSlice("SeriesYieldIntWtMueller", cntx.FirstYear, CoalescePolicy.MostRecent) On s.Key Equals y1.Key
                                      Select Key = s.Key, Value = If(s.Value, s2.Value), Crop = c.Value, Gra = g.Value, Frst = f.Value,
                                  Oth = o.Value, Urb = u.Value, U2 = ug.Value, U3 = ug2.Value, U4 = ug3.Value, Arb = a2.Value, Arb2 = a3.Value, Yld = y1.Value, Arb2Med = a3Med.Value, Arb2Low = a3Low.Value)
                        CLandArea.Add(item.Key, item.Value)
                        LDCrop.Add(item.Key, If(item.Crop, estimateWLineFitXyrs(diceLDCrop, cntx.FirstYear, yearsLDCrop, item.Key)))
                        LDGraz.Add(item.Key, If(item.Gra, estimateWLineFitXyrs(diceLDGraz, cntx.FirstYear, yearsLDGraz, item.Key)))
                        LDFor.Add(item.Key, If(item.Frst, estimateWLineFitXyrs(diceLDFor, cntx.FirstYear, yearsLDFor, item.Key)))
                        LDOth.Add(item.Key, If(item.Oth, estimateWLineFitXyrs(diceLDOth, cntx.FirstYear, yearsLDOth, item.Key)))
                        UrbVal = If(item.Urb, estimateWLineFitXyrs(diceLDUrb, cntx.FirstYear, yearsLDUrb, item.Key))
                        If Not UrbVal.HasValue Then
                            UrbVal = If(item.U2, estimateWLineFitXyrs(diceLDUrb2, cntx.FirstYear, yearsLDUrb2, item.Key))
                        End If
                        If Not UrbVal.HasValue Then
                            UrbVal = If(item.U3, estimateWLineFitXyrs(diceLDUrb3, cntx.FirstYear, yearsLDUrb3, item.Key))
                        End If
                        If Not UrbVal.HasValue Then
                            UrbVal = If(item.U4, estimateWLineFitXyrs(diceLDUrb4, cntx.FirstYear, yearsLDUrb4, item.Key))
                        End If
                        LDUrb.Add(item.Key, UrbVal)
                        If item.Arb.HasValue Then
                            CLdArabPot.Add(item.Key, item.Arb / 1000000)
                        ElseIf item.Arb2.HasValue Then
                            CLdArabPot.Add(item.Key, item.Arb2 / 1000)
                        Else
                            CLdArabPot.Add(item.Key, Nothing)
                        End If
                        'wv 2021/08/12
                        If item.Arb2Med.HasValue Then
                            CLdArabPotMed.Add(item.Key, item.Arb2Med / 1000000)
                        Else
                            CLdArabPotMed.Add(item.Key, item.Arb2 / 1000)
                        End If
                        If item.Arb2Low.HasValue Then
                            CLdArabPotLow.Add(item.Key, item.Arb2Low / 1000000)
                        Else
                            CLdArabPotLow.Add(item.Key, item.Arb2 / 1000)
                        End If

                        'wv 2020/12/11
                        CYIELDINT.Add(item.Key, item.Yld)
                    Next
                End If 'End of if history else forecast

                Dim EstimateUrb As SeriesEstimator = cntx.GetEstimator("LD_Urban")
                Dim LDOthVal As Single?, LDTot As Single?, CLandArabVal As Single?

                For Each key In cntx.Regionalization.Keys
                    'dsr 2015/01/19 FILL HOLES and DEAL with LIMITS in LAND DATA
                    'dsr 2015 12/21 Fix issues with land use: 1) have not read in AGPTot yet and 2) some countries
                    '               have very small land area, i.e. crop land < 0.01. Therefore: 1) change 0.01 to 0.0001
                    '               and 2) fill holes as fraction of total land as we do with forests
                    'If Not LDCrop(key)) Or (LDCrop(key) < 0.01) Then
                    '    LDCrop(key) = 0.5 * AGPTot(key) '2 tons/ha is null data assumption
                    If (Not LDCrop(key).HasValue) Or (LDCrop(key) < 0.0001) Then
                        LDCrop(key) = CSng(0.3 * CLandArea(key)) 'use same fraction as for forests
                    End If

                    If Not LDGraz(key).HasValue And CLandArea(key).HasValue Then
                        LDGraz.Remove(key)
                        LDGraz.Add(key, CSng(0.05 * CLandArea(key)))
                    End If
                    If Not LDGraz(key).HasValue Then
                        LDGraz.Remove(key)
                        LDGraz.Add(key, LDCrop(key))
                    End If
                    'dsr 2015/12/22 Come back and look at this; may not want minimum
                    If LDGraz(key) < 0.001 Then
                        LDGraz.Remove(key)
                        LDGraz.Add(key, 0.001)
                    End If

                    If Not LDFor(key).HasValue And CLandArea(key).HasValue Then
                        LDFor.Remove(key)
                        LDFor.Add(key, CSng(0.3 * CLandArea(key)))
                    End If
                    If Not LDFor(key).HasValue Then
                        LDFor.Remove(key)
                        LDFor.Add(key, 0.3)
                    End If
                    'dsr 2015/12/22 Come back and look at this; may not want minimum
                    If LDFor(key) < 0.001 Then
                        LDFor.Remove(key)
                        LDFor.Add(key, 0.001)
                    End If
                    If Not LDOth(key).HasValue And CLandArea(key).HasValue Then
                        LDOth.Remove(key)
                        LDOth.Add(key, CSng(0.25 * CLandArea(key)))
                    End If
                    If Not LDOth(key).HasValue Then
                        LDOth.Remove(key)
                        LDOth.Add(key, 0.25)
                    End If
                    'dsr 2015/12/22 Come back and look at this; may not want minimum
                    If LDOth(key) < 0.001 Then
                        LDOth.Remove(key)
                        LDOth.Add(key, 0.001)
                    End If

                    If Not LDUrb(key).HasValue Then
                        LDUrb.Remove(key)
                        LDUrb.Add(key, EstimateUrb(key))
                    End If

                    'dsr 2015/01/19 RECOMPUTE OTHER LAND as OTHER LAND - URBAN LAND
                    'Previously assumed Urban Land could be no more than 75% of Other Land
                    'Change this to 99.9%
                    If LDUrb(key) > 0.999 * LDOth(key) Then
                        LDUrb.Remove(key)
                        LDUrb.Add(key, CSng(0.999 * LDOth(key)))
                    End If
                    LDOthVal = LDOth(key)
                    LDOth.Remove(key)
                    LDOth.Add(key, LDOthVal - LDUrb(key))

                    LDTot = LDCrop(key) + LDGraz(key) + LDFor(key) + LDOth(key) + LDUrb(key)
                    If Not CLandArea(key).HasValue Then
                        CLandArea.Remove(key)
                        CLandArea.Add(key, LDTot)
                    End If
                    'Once needed to convert from hectares to sq kilom; no longer
                    '1 hectare = 10,000 sq meters
                    '1 million hectares = 10,000 sq kilometers

                    'Arable Land Potential
                    '---------------------
                    CLandArabVal = CLdArabPot(key)
                    CLdArabPot.Remove(key)
                    If Not CLandArabVal.HasValue Then
                        'Forest, Grazing and Crop Land
                        CLdArabPot.Add(key, LDFor(key) + LDGraz(key) + LDCrop(key))
                    Else
                        CLdArabPot.Add(key, CLandArabVal)
                    End If
                    'wv 2021/08/12
                    If Not CLdArabPotMed(key).HasValue Then
                        CLdArabPotMed(key) = LDGraz(key) + LDCrop(key)
                    End If
                    If Not CLdArabPotLow(key).HasValue Then
                        CLdArabPotLow(key) = LDCrop(key)
                    End If
                    'wv 2020/12/11
                    If CLdArabPot(key) < LDCrop(key) Then CLdArabPot(key) = LDCrop(key)
                    'wv 2021/08/12
                    If CLdArabPotMed(key) < LDCrop(key) Then CLdArabPotMed(key) = LDCrop(key)
                    If CLdArabPotLow(key) < LDCrop(key) Then CLdArabPotLow(key) = LDCrop(key)

                    ' wv 2021/03/01 added check so that arable land never exceeds potential
                    If CLdArabPot(key) > (LDCrop(key) + LDGraz(key) + LDFor(key)) Then CLdArabPot(key) = LDCrop(key) + LDGraz(key) + LDFor(key)
                    'wv 2021/08/12
                    If CLdArabPotMed(key) > (LDCrop(key) + LDGraz(key)) Then CLdArabPotMed(key) = LDCrop(key) + LDGraz(key)
                    If CLdArabPotLow(key) > LDCrop(key) Then CLdArabPotLow(key) = LDCrop(key)
                Next

                Using db As IFsContext = New IFsContext()
                    'Load BirthShare
                    Dim gp As GlobalParameters = db.GlobalParameters.Where(Function(g) g.ParameterName = "SLR").Single()
                    slr = CSng(gp.Value)
                End Using

                'wv 2021/02/11 call function to compute average yield intensities for countries missing data
                ' calculation is further described below
                ComputeAvgYields()
                'FillHoles

                'dsr 2015/11/03 NUTRITION - CALORIES PER CAPITA PER DAY, PROTEIN PER CAPITA PER DAY, GRAMS PER CAPITA PER DAY, and UNDERNUTRITION - *** start ***
                GetFishCalorieData(cntx)
                GetFishProteinData(cntx)
                GetFishGramsData(cntx)
                FillFishNutritionHolesIfCan(cntx)

                'jrs 2022/03/08
                Dim yearsMALNPOPP As Short()
                Dim yearsMALNPOPPUN As Short()
                Dim diceMALNPOPP As IDictionary(Of String, Single?())
                Dim diceMALNPOPPUN As IDictionary(Of String, Single?())
                Dim EstimatorMALNPOPP As SeriesEstimator

                If cntx.HistoricMode Then
                    For Each item In (From s In cntx.GetSlice("SeriesAGCropCalPerCapPerDayFAO", "Earliest")
                                      Join m In cntx.GetSlice("SeriesAGMeatCalPerCapPerDayFAO", "Earliest") On s.Key Equals m.Key
                                      Select Key = s.Key, Value = s.Value, Meat = m.Value)
                        CCLPC_Crop(item.Key) = item.Value
                        CCLPC_Meat(item.Key) = item.Meat
                    Next

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs As Short() = cntx.GetYears("SeriesAGCropCalPerCapPerDayFAO").OrderBy(Function(y) y).ToArray()
                    Dim CLPCCropTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCropCalPerCapPerDayFAO", yrs)

                    For Each key In cntx.Regionalization.Keys

                        ValHold = getClosestYrVal(CLPCCropTbl, yrs, key, cntx.FirstYear, HistDist)

                        CCLPC_Crop(key) = If(ValHold, estimateWLineFitXyrs(CLPCCropTbl, cntx.FirstYear, yrs, key))

                    Next
                    'mti 2019/04/29  Flex Hist Base End
                    '=========================================
                    'mti 2019/04/29  Flex Hist Base Start

                    'CCLPC(ICount%, 2) = getClosestYrVal(CLPCMeatTbl, FirstYearHistory, HistDist)
                    '"SeriesAGMeatCalPerCapPerDayFAO"
                    yrs = cntx.GetYears("SeriesAGMeatCalPerCapPerDayFAO").OrderBy(Function(y) y).ToArray()
                    Dim CLPCMeatTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeatCalPerCapPerDayFAO", yrs)

                    For Each key In cntx.Regionalization.Keys

                        ValHold = getClosestYrVal(CLPCMeatTbl, yrs, key, cntx.FirstYear, HistDist)

                        CCLPC_Meat(key) = If(ValHold, estimateWLineFitXyrs(CLPCMeatTbl, cntx.FirstYear, yrs, key))

                    Next
                    'mti 2019/04/29  Flex Hist Base End
                    '=====
                    'jrs 2024/07/16
                    For Each key In cntx.Regionalization.Keys
                        CCLPC_Tot.Add(key, Nothing)
                    Next
                Else 'Forecast *********
                        Dim yearsCLPCCrop As Short()
                    yearsCLPCCrop = (From y In cntx.GetYears("SeriesAGCropCalPerCapPerDayFAO")
                                     Where cntx.FirstYear - 20S < y
                                     Order By y Descending
                    ).Take(10).ToArray
                    Dim diceCLPCCrop As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCropCalPerCapPerDayFAO", yearsCLPCCrop)

                    Dim yearsCLPCMeat As Short()
                    yearsCLPCMeat = (From y In cntx.GetYears("SeriesAGMeatCalPerCapPerDayFAO")
                                     Where cntx.FirstYear - 20S < y
                                     Order By y Descending
                    ).Take(10).ToArray
                    Dim diceCLPCMeat As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeatCalPerCapPerDayFAO", yearsCLPCMeat)

                    'jrs 2022/03/08 *** begin ***
                    Dim yearsCLPCTot As Short()
                    yearsCLPCTot = (From y In cntx.GetYears("SeriesMalnDietEnSup")
                                    Where cntx.FirstYear - 20S < y
                                    Order By y Descending
                    ).Take(10).ToArray
                    'jrs 2024/07/16 Dim diceCLPCTot As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeatCalPerCapPerDayFAO", yearsCLPCTot)
                    Dim diceCLPCTot As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesMalnDietEnSup", yearsCLPCTot)

                    Dim yearsCLPCCV As Short()
                    yearsCLPCCV = (From y In cntx.GetYears("SeriesMalnCaloricDistribution")
                                   Where cntx.FirstYear - 20S < y
                                   Order By y Descending
                    ).Take(10).ToArray
                    Dim diceCLPCCV As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesMalnCaloricDistribution", yearsCLPCCV)

                    Dim yearsCLPCMDER As Short()
                    yearsCLPCMDER = (From y In cntx.GetYears("SeriesMalnMinNutrRequired")
                                     Where cntx.FirstYear - 20S < y
                                     Order By y Descending
                    ).Take(10).ToArray
                    Dim diceCLPCMDER As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesMalnMinNutrRequired", yearsCLPCMDER)

                    yearsMALNPOPP = (From y In cntx.GetYears("MALNPOPP")
                                     Where cntx.FirstYear - 20S < y
                                     Order By y Descending
                    ).Take(10).ToArray
                    yearsMALNPOPPUN = (From y In cntx.GetYears("SeriesMalnPopUN")
                                       Where cntx.FirstYear - 20S < y
                                       Order By y Descending
                    ).Take(10).ToArray
                    diceMALNPOPP = cntx.GetDice("MALNPOPP", yearsMALNPOPP)
                    diceMALNPOPPUN = cntx.GetDice("SeriesMalnPopUN", yearsMALNPOPPUN)


                    For Each item In (From s In cntx.GetSlice("SeriesAGCropCalPerCapPerDayFAO", cntx.FirstYear)
                                      Join m In cntx.GetSlice("SeriesAGMeatCalPerCapPerDayFAO", cntx.FirstYear) On s.Key Equals m.Key
                                      Join t In cntx.GetSlice("SeriesMalnDietEnSup", cntx.FirstYear) On s.Key Equals t.Key
                                      Join v In cntx.GetSlice("SeriesMalnCaloricDistribution", cntx.FirstYear) On s.Key Equals v.Key
                                      Join e In cntx.GetSlice("SeriesMalnMinNutrRequired", cntx.FirstYear) On s.Key Equals e.Key
                                      Join p In cntx.GetSlice("MALNPOPP", cntx.FirstYear) On s.Key Equals p.Key
                                      Join u In cntx.GetSlice("SeriesMalnPopUN", cntx.FirstYear) On s.Key Equals u.Key
                                      Select Key = s.Key, Value = s.Value, Meat = m.Value, Tot = t.Value, CV = v.Value,
                                          MDER = e.Value, MALN = p.Value, MALN2 = u.Value)
                        CCLPC_Crop.Add(item.Key, item.Value)
                        CCLPC_Meat.Add(item.Key, item.Meat)
                        CCLPC_Tot.Add(item.Key, item.Tot)
                        CCLPCCV.Add(item.Key, item.CV)
                        CCLPCMDER.Add(item.Key, item.MDER)
                        CMALNPOPP.Add(item.Key, item.MALN)
                        'jrs 2022/07/18 *** start ***                       
                        If CMALNPOPP(item.Key) = 2.5 Then
                            CMALNPOPP2_5LIM.Add(item.Key, True)
                        Else
                            CMALNPOPP2_5LIM.Add(item.Key, False)
                        End If
                        If Not CMALNPOPP(item.Key).HasValue And item.MALN2.HasValue Then
                            CMALNPOPP(item.Key) = item.MALN2
                        End If
                        If CMALNPOPP2_5LIM(item.Key) Then
                            CMALNPOPP(item.Key) = Nothing
                        End If
                        'jrs 2022/07/18 *** end ***

                        If CCLPC_Tot(item.Key).HasValue And CCLPC_Crop(item.Key).HasValue And CCLPC_Meat(item.Key).HasValue And CCLPC_Fish(item.Key).HasValue Then
                            Dim Factor As Single?
                            Factor = CCLPC_Tot(item.Key) / (CCLPC_Crop(item.Key) + CCLPC_Meat(item.Key) + CCLPC_Fish(item.Key))
                            CCLPC_Crop(item.Key) = Factor * CCLPC_Crop(item.Key)
                            CCLPC_Meat(item.Key) = Factor * CCLPC_Meat(item.Key)
                            CCLPC_Fish(item.Key) = Factor * CCLPC_Fish(item.Key)
                        End If

                        'jrs 2022/12/02 start
                        If Not CCLPCCV(item.Key).HasValue And yearsCLPCCV(0) > BaseYear Then
                            CCLPCCV(item.Key) = getClosestYrVal(diceCLPCCV, yearsCLPCCV, item.Key, BaseYear, HistDist)
                        End If
                        If Not CCLPCMDER(item.Key).HasValue And yearsCLPCMDER(0) > BaseYear Then
                            CCLPCMDER(item.Key) = getClosestYrVal(diceCLPCMDER, yearsCLPCMDER, item.Key, BaseYear, HistDist)
                        End If
                        'jrs 2022/12/02 end

                        LoadCLPCandMALNPOPPdataFromSameYear(item.Key)

                        If Not CCLPC_Tot(item.Key).HasValue Then
                            CCLPC_Tot(item.Key) = estimateWLineFitXyrs(diceCLPCTot, BaseYear, yearsCLPCTot, item.Key)
                            If CCLPC_Tot(item.Key).HasValue Then
                                LoadCLPCandMALNPOPPdataFromSameYear(item.Key)
                            End If
                        End If

                        If Not CMALNPOPP(item.Key).HasValue Then
                            CMALNPOPP(item.Key) = estimateWLineFitXyrs(diceMALNPOPP, BaseYear, yearsMALNPOPP, item.Key)

                            If Not CMALNPOPP(item.Key).HasValue And CCLPC_Tot(item.Key).HasValue Then
                                Dim VarVal As Single
                                Call XYTABL("Calories per capita (2009) versus Malnourished Pop Percent (Most Recent) Log",
                                CSng(CCLPC_Tot(item.Key)), VarVal, cntx)
                                'jrs 2026/02/24 Bahrain, the US is showing an estimation of 0.00183 in 2022
                                If VarVal < 0 Then
                                    VarVal = 0.001
                                End If
                                CMALNPOPP(item.Key) = VarVal
                            End If

                            If CMALNPOPP(item.Key).HasValue Then
                                'jrs 2022/07/18
                                If CMALNPOPP2_5LIM(item.Key) Then
                                    CMALNPOPP(item.Key) = CSng(Amin(CMALNPOPP(item.Key), 2.5))
                                End If
                                LoadCLPCandMALNPOPPdataFromSameYear(item.Key)
                            End If
                        End If

                        If Not CCLPCMDER(item.Key).HasValue Then
                            CCLPCMDER(item.Key) = estimateWLineFitXyrs(diceCLPCMDER, BaseYear, yearsCLPCMDER, item.Key)
                            If CCLPCMDER(item.Key).HasValue Then
                                LoadCLPCandMALNPOPPdataFromSameYear(item.Key)
                            End If
                        End If

                        If Not CCLPCCV(item.Key).HasValue Then
                            CCLPCCV(item.Key) = estimateWLineFitXyrs(diceCLPCCV, BaseYear, yearsCLPCCV, item.Key)
                            If CCLPCCV(item.Key).HasValue Then
                                LoadCLPCandMALNPOPPdataFromSameYear(item.Key)
                            End If
                        End If
                    Next

                    'For Each item In (From s In cntx.GetSlice("SeriesAGCropCalPerCapPerDayFAO", cntx.FirstYear, CoalescePolicy.Null)
                    '                  Join m In cntx.GetSlice("SeriesAGMeatCalPerCapPerDayFAO", cntx.FirstYear, CoalescePolicy.Null) On s.Key Equals m.Key
                    '                  Select Key = s.Key, Value = s.Value, Meat = m.Value)
                    '    CCLPC_Crop.Add(item.Key, If(item.Value, estimateWLineFitXyrs(diceCLPCCrop, cntx.FirstYear, yearsCLPCCrop, item.Key)))
                    '    CCLPC_Meat.Add(item.Key, If(item.Meat, estimateWLineFitXyrs(diceCLPCMeat, cntx.FirstYear, yearsCLPCMeat, item.Key)))
                    'Next

                    'jrs 2022/03/08 *** end ***
                End If 'End of if history else forecast

                'dsr 2015/11/03 Fill holes in Calorie Data
                'Will need meat calories as share of fish + meat calories; no clear pattern so use global average
                Dim GlobalMeatCalories As Single?, GlobalFishCalories As Single?, GlobalMeatShare As Single?
                GlobalMeatCalories = 0
                GlobalFishCalories = 0
                For Each key In cntx.Regionalization.Keys
                    'jrs 2018/09/20 changed from POP to CPop
                    If CCLPC_Meat(key).HasValue Then GlobalMeatCalories = GlobalMeatCalories + CCLPC_Meat(key) * CPOP(key)
                    If CCLPC_Fish(key).HasValue Then GlobalFishCalories = GlobalFishCalories + CCLPC_Fish(key) * CPOP(key)
                Next

                'jrs 2024/07/08
                If GlobalMeatCalories = 0 Then
                    'ReLoad Meat from older years
                    For Each item In (From s In cntx.GetSlice("SeriesAGMeatCalPerCapPerDayFAO", "MostRecent")
                                      Select Key = s.Key, Value = s.Value)
                        CCLPC_Meat(item.Key) = item.Value
                        If CCLPC_Meat(item.Key).HasValue Then GlobalMeatCalories = GlobalMeatCalories + CCLPC_Meat(item.Key) * CPOP(item.Key)
                    Next
                End If

                GlobalMeatShare = GlobalMeatCalories / (GlobalMeatCalories + GlobalFishCalories) 'for 2010 this is about 0.937 or 14 meat to 1 fish


                'jrs 2024/06/14
                Dim TfNameCalDem As String = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Calorie Demand (fixed-effect)"
                TfNameCalDem = getFunctionNameYrOrMstRct(TfNameCalDem, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim TfNameCLMeat As String = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus CLPC from MeatandFish (" & BaseYear & ") Log"
                TfNameCLMeat = getFunctionNameYrOrMstRct(TfNameCLMeat, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")

                'jrs 2024/07/08
                Dim EstimateTot As SeriesEstimator = cntx.GetRegression(TfNameCalDem).ToEstimator(CGDPPCP)
                Dim EstimateMeatAndFish As SeriesEstimator = cntx.GetRegression(TfNameCLMeat).ToEstimator(CGDPPCP)

                For Each key In cntx.Regionalization.Keys

                    'dsr 2014/12/16 Calculate total calories as sum when have data for crops, meat, and fish; otherwise estimate from function
                    If CCLPC_Crop(key).HasValue And CCLPC_Meat(key).HasValue And CCLPC_Fish(key).HasValue Then
                        CCLPC_Tot(key) = CCLPC_Crop(key) + CCLPC_Meat(key) + CCLPC_Fish(key)
                    Else
                        'dsr 2014/12/16 use equation based only on CGDPPCP
                        'jrs 2024/06/14 Dim Estimate As SeriesEstimator = cntx.GetRegression("GDP/Capita (PPP 2011) Versus Calorie Demand (fixed-effect)").ToEstimator(CGDPPCP)
                        'jrs 2024/07/08
                        If Not CCLPC_Tot(key).HasValue Then
                            CCLPC_Tot(key) = EstimateTot(key)
                        End If
                    End If
                    CCLPC_Tot(key) = AMAX(CCLPC_Tot(key), 1)  'dsr 2015/03/17 Set minimum based on historical data
                    'No data for crops, meat, and fish
                    If Not CCLPC_Crop(key).HasValue And Not CCLPC_Meat(key).HasValue And Not CCLPC_Fish(key).HasValue Then
                        'Estimate percentage of total calories from meat and fish based on function
                        'dsr 2015/03/25 Equation was giving odd results, so new one created
                        '           Call AnalFunc("Percentage of calories from meat and fish (combined)", _
                        '                Array(CGDPPCP(key), CPOPURBAN(key) / CPOP(key) * 100), MeatAndFishPct, _
                        '                Array("GDPPCP", "POPURBAN%"))
                        Dim MeatAndFishPctPred As Double?, MeatAndFishPct As Double?
                        'jrs 2024/06/14 Dim Estimate As SeriesEstimator = cntx.GetRegression("GDP/Capita (PPP 2011) Versus CLPC from MeatandFish (2010) Log").ToEstimator(CGDPPCP)
                        'Dim Estimate As SeriesEstimator = cntx.GetRegression(TfNameCLMeat).ToEstimator(CGDPPCP)
                        MeatAndFishPctPred = EstimateMeatAndFish(key)
                        MeatAndFishPct = Amin(AMAX(MeatAndFishPctPred, 3.5), 100) 'dsr 2015/03/17 Set Minimum based on historical data
                        'Calculate crop calories as share of total not from meat and fish
                        CCLPC_Crop(key) = CSng(CCLPC_Tot(key) * (1 - MeatAndFishPct / 100))
                        'Calculate meat calories as share of remainder from meat using value calculated above
                        CCLPC_Meat(key) = (CCLPC_Tot(key) - CCLPC_Crop(key)) * GlobalMeatShare
                        'Calculate fish calories as remainder
                        CCLPC_Fish(key) = CCLPC_Tot(key) - CCLPC_Crop(key) - CCLPC_Meat(key)
                    End If

                    'No data for only one of crops, meat, or fish: calculate as remainder
                    If Not CCLPC_Crop(key).HasValue And CCLPC_Meat(key).HasValue And CCLPC_Fish(key).HasValue Then
                        CCLPC_Crop(key) = CCLPC_Tot(key) - CCLPC_Meat(key) - CCLPC_Fish(key)
                    End If
                    If Not CCLPC_Meat(key).HasValue And CCLPC_Crop(key).HasValue And CCLPC_Fish(key).HasValue Then
                        CCLPC_Meat(key) = CCLPC_Tot(key) - CCLPC_Crop(key) - CCLPC_Fish(key)
                    End If
                    If Not CCLPC_Fish(key).HasValue And CCLPC_Crop(key).HasValue And CCLPC_Meat(key).HasValue Then
                        CCLPC_Fish(key) = CCLPC_Tot(key) - CCLPC_Crop(key) - CCLPC_Meat(key)
                    End If

                    'No data for only two of crops, meat, or fish
                    'Data only for crops
                    If CCLPC_Crop(key).HasValue And Not CCLPC_Meat(key).HasValue And Not CCLPC_Fish(key).HasValue Then
                        'Calculate meat calories as share of remainder from meat using value calculated above
                        CCLPC_Meat(key) = (CCLPC_Tot(key) - CCLPC_Crop(key)) * GlobalMeatShare
                        'Calculate fish calories as remainder
                        CCLPC_Fish(key) = CCLPC_Tot(key) - CCLPC_Crop(key) - CCLPC_Meat(key)
                    End If
                    'Data only for meat
                    If Not CCLPC_Crop(key).HasValue And CCLPC_Meat(key).HasValue And Not CCLPC_Fish(key).HasValue Then
                        'Use GlobalMeatShare to calculate fish calories
                        CCLPC_Fish(key) = CCLPC_Meat(key) * (1 - GlobalMeatShare) / GlobalMeatShare
                        'Calculate crop calories as remainder
                        CCLPC_Crop(key) = CCLPC_Tot(key) - CCLPC_Meat(key) - CCLPC_Fish(key)
                    End If
                    'Data only for fish
                    If Not CCLPC_Crop(key).HasValue And Not CCLPC_Meat(key).HasValue And CCLPC_Fish(key).HasValue Then
                        'Use GlobalMeatShare to calculate meat calories
                        CCLPC_Meat(key) = CCLPC_Fish(key) * GlobalMeatShare / (1 - GlobalMeatShare)
                        'Calculate crop calories as remainder
                        CCLPC_Crop(key) = CCLPC_Tot(key) - CCLPC_Meat(key) - CCLPC_Fish(key)
                    End If
                    'dsr 2015/03/17 Make sure none of the calorie values are <= 0 and recalculate total calories
                    If CCLPC_Crop(key) <= 0 Then CCLPC_Crop(key) = 1 'dsr 2015/11/12 change minimum to 1 from 0.00001
                    If CCLPC_Meat(key) <= 0 Then CCLPC_Meat(key) = 1 'dsr 2015/11/12 change minimum to 1 from 0.00001
                    If CCLPC_Fish(key) <= 0 Then CCLPC_Fish(key) = 1 'dsr 2015/11/12 change minimum to 1 from 0.00001
                    CCLPC_Tot(key) = CCLPC_Crop(key) + CCLPC_Meat(key) + CCLPC_Fish(key)

                    'dsr 2015/11/02 Use value of CCLPC_Fish(key) to backfill nulls in CCLPCFISH(key) for later use
                    If Not CCLPCFISH(key).HasValue Then CCLPCFISH(key) = CCLPC_Fish(key)
                Next

                'Malnutrition
                '------------
                'Undernourished Children
                If cntx.HistoricMode Then
                    Dim Estimator As SeriesEstimator = cntx.GetRegression("Calories per capita (2009) versus Malnourished Children (2011) LN").ToEstimator(CCLPC_Tot)
                    'mti 2019/04/29  Flex Hist Base Start
                    'CMALNCHP(ICount%) = getClosestYrVal(MalnChPTbl, FirstYearHistory, HistDist)
                    Dim SeriesTable As String = "MALNCHP"
                    Dim yrs As Short() = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    Dim DictTbl As IDictionary(Of String, Single?()) = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        If Not ValHold.HasValue Then
                            ValHold = Estimator(key)
                        End If
                        ValHold = AMAX(ValHold, 2)
                        CMALNCHP.Add(key, ValHold)
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else
                    'mti Jan 2005 - added for MDG consistency
                    'mti March 23, 2005
                    'bbh removed the line below because overrides data  2012/12/15
                    '            CMALNCHP(key) = LReg(MalnChPTbl, "SeriesCalPCap", CountryName$(key), _
                    '                "Calories per Capita Versus Maln Children % (1995)", FirstYear, 100, 0)
                    'jrs 2017/04/28 CMALNCHP(key) = getYearOrMstRct(MalnChPTbl, BaseYear)
                    Dim yearsMALNCHP As Short()
                    yearsMALNCHP = (From y In cntx.GetYears("MALNCHP")
                                    Where cntx.FirstYear - 20S < y
                                    Order By y Descending
                    ).Take(10).ToArray
                    Dim diceMALNCHP As IDictionary(Of String, Single?()) = cntx.GetDice("MALNCHP", yearsMALNCHP)
                    Dim Estimator As SeriesEstimator = cntx.GetRegression("Calories per capita (2009) versus Malnourished Children (2011) LN").ToEstimator(CCLPC_Tot)

                    For Each item In (From s In cntx.GetSlice("MALNCHP", cntx.FirstYear)
                                      Join m In cntx.GetSlice("MALNCHP", "MostRecent") On m.Key Equals s.Key
                                      Select Key = s.Key, Value = s.Value, MR = m.Value)
                        CMALNCHP.Add(item.Key, AMAX(If(If(If(item.Value, estimateWLineFitXyrs(diceMALNCHP, cntx.FirstYear, yearsMALNCHP, item.Key)), item.MR), Estimator(item.Key)), 2))
                    Next
                End If
                'mti 2019/04/29  Flex Hist Base Start
                'ttt 2019/06/21 This is lacking. Wrote new codes 
                'Undernourished Population as a Whole
                '======
                If cntx.HistoricMode Then
                    Dim Estimator As SeriesEstimator = cntx.GetRegression("Calories per capita (2009) versus Malnourished Pop Percent (Most Recent) Log").ToEstimator(CCLPC_Tot)
                    Dim SeriesTable As String = "MALNPOPP"
                    Dim yrs As Short() = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    Dim DictTbl As IDictionary(Of String, Single?()) = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        If Not ValHold.HasValue Then
                            ValHold = Estimator(key)
                        End If
                        CMALNPOPP.Add(key, ValHold)
                    Next
                Else
                    '==VB6
                    'CMALNPOPP(ICount%) = getYearOrFitLineOnXYrs(MalnPopPTbl, BaseYear, 10)
                    'If IsNull(CMALNPOPP(ICount%)) Then CMALNPOPP(ICount%) = getYearOrFitLineOnXYrs(MalnPopPUNTbl, BaseYear, 10)
                    'If CMALNPOPP(ICount%) < 5.01 And CMALNPOPP(ICount%) > 4.99 Then CMALNPOPP(ICount%) = 1.5
                    'If IsNull(CMALNPOPP(ICount%)) Then

                    '    Call XYTABL("Calories per capita (2009) versus Malnourished Pop Percent (Most Recent) Log",
                    'CCLPC(ICount%, 4), CMALNPOPP(ICount%))

                    'End If
                    'If CMALNPOPP(ICount%) < 1.5 Then CMALNPOPP(ICount%) = 1.5 'sgh 2017/05/18 move here
                    'End VB6======
                    EstimatorMALNPOPP = cntx.GetRegression("Calories per capita (2009) versus Malnourished Pop Percent (Most Recent) Log").ToEstimator(CCLPC_Tot)
                    For Each item In (From s In cntx.GetSlice("MALNPOPP", cntx.FirstYear)
                                      Join m2 In cntx.GetSlice("SeriesMalnPopUN", cntx.FirstYear) On m2.Key Equals s.Key
                                      Select Key = s.Key, Value = s.Value, Value2 = m2.Value)
                        'jrs 2022/03/08
                        If Not CMALNPOPP(item.Key).HasValue Then
                            Dim ValC = If(item.Value, estimateWLineFitXyrs(diceMALNPOPP, cntx.FirstYear, yearsMALNPOPP, item.Key))
                            If Not ValC.HasValue Then
                                ValC = If(item.Value2, estimateWLineFitXyrs(diceMALNPOPPUN, cntx.FirstYear, yearsMALNPOPPUN, item.Key))
                            End If
                            If ValC < 5.01 AndAlso ValC > 4.99 Then ValC = 1.5
                            If Not ValC.HasValue Then
                                ValC = EstimatorMALNPOPP(item.Key)
                            End If

                            'jrs 2022/07/18 If ValC < 1.5 Then ValC = 1.5
                            If CMALNPOPP2_5LIM(item.Key) Then
                                ValC = CSng(Amin(ValC, 2.5))
                            Else
                                If ValC < 1.5 Then ValC = 1.5
                            End If

                            'jrs 2022/03/08
                            CMALNPOPP(item.Key) = ValC
                        End If
                    Next
                End If
                'mti 2019/04/29  Flex Hist Base End
                '=====

                'jrs 2020/12/10
                'Distribution Formulation
                '------------------------
                If cntx.HistoricMode Then
                    Dim SeriesTable As String = "SeriesMalnCaloricDistribution"
                    Dim yrs As Short() = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    Dim DictTbl As IDictionary(Of String, Single?()) = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CCLPCCV.Add(key, ValHold)
                    Next

                    SeriesTable = "SeriesMalnMinNutrRequired"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CCLPCMDER.Add(key, ValHold)
                    Next
                Else
                    'jrs 2022/03/08
                    'For Each item In (From s In cntx.GetSlice("SeriesMalnCaloricDistribution", cntx.BaseYear, CoalescePolicy.MostRecent)
                    '                  Join m In cntx.GetSlice("SeriesMalnMinNutrRequired", cntx.BaseYear, CoalescePolicy.MostRecent) On m.Key Equals s.Key
                    '                  Select Key = s.Key, MCV = s.Value, MMin = m.Value)
                    '    CCLPCCV.Add(item.Key, item.MCV)
                    '    CCLPCMDER.Add(item.Key, item.MMin)
                    'Next
                End If
                'Distribution Formulation
                '------------------------
                'Check for Nulls
                If Not CCLPCMDER("Eritrea").HasValue Then
                    CCLPCMDER("Eritrea") = CCLPCMDER("Ethiopia")
                    'jrs 2022/12/02
                    If Not CCLPCCV("Eritrea").HasValue Then
                        LoadCLPCandMALNPOPPdataFromSameYear("Eritrea")
                    End If
                End If
                If Not CCLPCMDER("Kosovo").HasValue Then
                    CCLPCMDER("Kosovo") = CCLPCMDER("Serbia")
                    'jrs 2022/12/02
                    If Not CCLPCCV("Kosovo").HasValue Then
                        LoadCLPCandMALNPOPPdataFromSameYear("Kosovo")
                    End If
                End If

                'jrs 2021/05/25
                'Reset CLPCCV With other values
                '================================
                'Sigma = Log((CLPCCV(R%) ^ 2) + 1) ^ 0.5

                'Zz = (Log(CLPCMDER(R%)) - Mu) / Sigma
                'Zz = (Log(CLPCMDER(R%)) - Log(CLPC(R%, 4)) - Sigma ^ 2 / 2) / Sigma
                'Sigma * Zz = Log(CLPCMDER(R%)) - Log(CLPC(R%, 4)) + Sigma ^ 2 / 2
                '0 = Sigma ^ 2 / 2 - Zz * Sigma + (Log(CLPCMDER(R%)) - Log(CLPC(R%, 4)))

                Dim aa, bb, cc, Zz, Sigma1, Sigma2 As Single
                For Each key In cntx.Regionalization.Keys
                    'jrs 2022/03/08
                    If Not CCLPCCV(key).HasValue Then
                        'A = 1 / 2
                        aa = 1 / 2
                        'B = -Zz
                        'pct = CalcNormalCDF(Zz) * 100 -> we have pct, then can find Zz
                        Zz = CalcNormalInverse(CMALNPOPP(key) / 100)
                        bb = -1 * Zz
                        'C = (Log(CLPCMDER(R%)) - Log(CLPC(R%, 4)))
                        cc = CSng(Math.Log(CDbl(CCLPCMDER(key))) - Math.Log(CDbl(CCLPC_Tot(key))))
                        'Sigma = (-b +- Sqrt(b^2 - 4ac))/(2a)
                        'jrs 2021/06/09
                        If bb ^ 2 - 4 * aa * cc >= 0 Then
                            Sigma1 = CSng((-bb - Math.Sqrt(bb ^ 2 - 4 * aa * cc)) / (2 * aa))
                            Sigma2 = CSng((-bb + Math.Sqrt(bb ^ 2 - 4 * aa * cc)) / (2 * aa))
                            'CLPCCV(R%) = Sqrt(exp(Sigma^2)-1)
                            If Math.Exp(Sigma1 ^ 2) - 1 > 0 And Math.Exp(Sigma2 ^ 2) - 1 > 0 Then
                                If Math.Sqrt(Math.Exp(Sigma1 ^ 2) - 1) < Math.Sqrt(Math.Exp(Sigma2 ^ 2) - 1) Then
                                    CCLPCCV(key) = CSng(Math.Sqrt(Math.Exp(Sigma1 ^ 2) - 1))
                                Else
                                    CCLPCCV(key) = CSng(Math.Sqrt(Math.Exp(Sigma2 ^ 2) - 1))
                                End If
                            ElseIf Math.Exp(Sigma1 ^ 2) - 1 > 0 Then
                                CCLPCCV(key) = CSng(Math.Sqrt(Math.Exp(Sigma1 ^ 2) - 1))
                            ElseIf Math.Exp(Sigma2 ^ 2) - 1 > 0 Then
                                CCLPCCV(key) = CSng(Math.Sqrt(Math.Exp(Sigma2 ^ 2) - 1))
                            End If
                        End If
                    End If
                Next

                'dsr 2015/11/03 PROTEIN PER CAPITA PER DAY - *** start ***
                '-------
                If cntx.HistoricMode Then
                    For Each item In (From s In cntx.GetSlice("PROTEINPC_Crop", "Earliest")
                                      Join m In cntx.GetSlice("PROTEINPC_Meat", "Earliest") On m.Key Equals s.Key
                                      Select Key = s.Key, Value = s.Value, Meat = m.Value)
                        CPROTEINPC_Crop.Add(item.Key, item.Value)
                        CPROTEINPC_Meat.Add(item.Key, item.Meat)
                    Next
                    'mti 2019/04/29  Flex Hist Base Start
                    'CPROTEINPC(ICount%, 1) = getClosestYrVal(ProteinPCCropTbl, FirstYearHistory, HistDist)
                    Dim SeriesTable As String = "PROTEINPC_Crop"
                    Dim yrs As Short() = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    Dim DictTbl As IDictionary(Of String, Single?()) = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CPROTEINPC_Crop(key) = ValHold
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                    'mti 2019/04/29  Flex Hist Base Start
                    'CPROTEINPC(ICount%, 2) = getClosestYrVal(ProteinPCMeatTbl, FirstYearHistory, HistDist)
                    SeriesTable = "PROTEINPC_Meat"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CPROTEINPC_Meat(key) = ValHold
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else
                    Dim yearsPROTCrop As Short()
                    yearsPROTCrop = (From y In cntx.GetYears("PROTEINPC_Crop")
                                     Where cntx.FirstYear - 20S < y
                                     Order By y Descending
                    ).Take(10).ToArray
                    Dim dicePROTCrop As IDictionary(Of String, Single?()) = cntx.GetDice("PROTEINPC_Crop", yearsPROTCrop)

                    Dim yearsPROTMeat As Short()
                    yearsPROTMeat = (From y In cntx.GetYears("PROTEINPC_Meat")
                                     Where cntx.FirstYear - 20S < y
                                     Order By y Descending
                    ).Take(10).ToArray
                    Dim dicePROTMeat As IDictionary(Of String, Single?()) = cntx.GetDice("PROTEINPC_Meat", yearsPROTMeat)

                    For Each item In (From s In cntx.GetSlice("PROTEINPC_Crop", cntx.FirstYear)
                                      Join m In cntx.GetSlice("PROTEINPC_Meat", cntx.FirstYear) On m.Key Equals s.Key
                                      Select Key = s.Key, Value = s.Value, Meat = m.Value)
                        CPROTEINPC_Crop.Add(item.Key, If(item.Value, estimateWLineFitXyrs(dicePROTCrop, cntx.FirstYear, yearsPROTCrop, item.Key)))
                        CPROTEINPC_Meat.Add(item.Key, If(item.Meat, estimateWLineFitXyrs(dicePROTMeat, cntx.FirstYear, yearsPROTMeat, item.Key)))
                    Next
                End If 'if historic mode else forecast mode

                'dsr 2015/11/03 Fill holes in Protein Data
                Dim GlobalCropCalories As Single?, GlobalCropProtein As Single?, GlobalMeatProtein As Single?, GlobalFishProtein As Single?
                Dim GlobalCropProteinPerCal As Single?, GlobalMeatProteinPerCal As Single?, GlobalFishProteinPerCal As Single?
                'Use global average ratios of protein to calories, so first calculate these using countries w/o null protein data
                GlobalCropCalories = 0
                GlobalMeatCalories = 0
                GlobalFishCalories = 0
                GlobalCropProtein = 0
                GlobalMeatProtein = 0
                GlobalFishProtein = 0
                For Each key In cntx.Regionalization.Keys
                    'jrs 2018/09/20 changed from POP to CPop
                    If CPROTEINPC_Crop(key).HasValue Then
                        GlobalCropCalories = GlobalCropCalories + CCLPC_Crop(key) * CPOP(key)
                        GlobalCropProtein = GlobalCropProtein + CPROTEINPC_Crop(key) * CPOP(key)
                    End If
                    If CPROTEINPC_Meat(key).HasValue Then
                        GlobalMeatCalories = GlobalMeatCalories + CCLPC_Meat(key) * CPOP(key)
                        GlobalMeatProtein = GlobalMeatProtein + CPROTEINPC_Meat(key) * CPOP(key)
                    End If
                    If CPROTEINPC_Fish(key).HasValue Then
                        GlobalFishCalories = GlobalFishCalories + CCLPC_Fish(key) * CPOP(key)
                        GlobalFishProtein = GlobalFishProtein + CPROTEINPC_Fish(key) * CPOP(key)
                    End If
                Next

                GlobalCropProteinPerCal = GlobalCropProtein / GlobalCropCalories 'for 2010 this is about 0.0198
                GlobalMeatProteinPerCal = GlobalMeatProtein / GlobalMeatCalories 'for 2010 this is about 0.0559
                GlobalFishProteinPerCal = GlobalFishProtein / GlobalFishCalories 'for 2010 this is about 0.1558

                For Each key In cntx.Regionalization.Keys
                    If Not CPROTEINPC_Crop(key).HasValue Then CPROTEINPC_Crop(key) = CCLPC_Crop(key) * GlobalCropProteinPerCal
                    If Not CPROTEINPC_Meat(key).HasValue Then CPROTEINPC_Meat(key) = CCLPC_Meat(key) * GlobalMeatProteinPerCal
                    If Not CPROTEINPC_Fish(key).HasValue Then CPROTEINPC_Fish(key) = CCLPC_Fish(key) * GlobalFishProteinPerCal
                    'Calculate Total Protein Per Capita Per Day
                    CPROTEINPC_Tot.Add(key, CPROTEINPC_Crop(key) + CPROTEINPC_Meat(key) + CPROTEINPC_Fish(key))
                    'dsr 2015/11/02 Use value of CPROTEINPC_Fish(key) to backfill nulls in CPROTEINPCFISH(key), for later use
                    If Not CPROTEINPCFISH(key).HasValue Then CPROTEINPCFISH(key) = CPROTEINPC_Fish(key)
                Next
                'dsr 2015/11/03 PROTEIN PER CAPITA PER DAY - *** end ***

                'dsr 2015/11/03 GRAMS PER CAPITA PER DAY - *** start ***
                '-------
                If cntx.HistoricMode Then
                    For Each item In (From s In cntx.GetSlice("SeriesAGCropFoodSupplyPerCapPerDayFAO", "Earliest")
                                      Join m In cntx.GetSlice("SeriesAGMeatFoodSupplyPerCapPerDayFAO", "Earliest") On m.Key Equals s.Key
                                      Select Key = s.Key, Value = s.Value, Meat = m.Value)
                        CGRAMSPC_Crop.Add(item.Key, item.Value)
                        CGRAMSPC_Meat.Add(item.Key, item.Meat)
                    Next
                Else
                    Dim yearsGRAMSCrop As Short()
                    yearsGRAMSCrop = (From y In cntx.GetYears("SeriesAGCropFoodSupplyPerCapPerDayFAO")
                                      Where cntx.FirstYear - 20S < y
                                      Order By y Descending
                    ).Take(10).ToArray
                    Dim diceGRAMSCrop As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCropFoodSupplyPerCapPerDayFAO", yearsGRAMSCrop)

                    Dim yearsGRAMSMeat As Short()
                    yearsGRAMSMeat = (From y In cntx.GetYears("SeriesAGMeatFoodSupplyPerCapPerDayFAO")
                                      Where cntx.FirstYear - 20S < y
                                      Order By y Descending
                    ).Take(10).ToArray
                    Dim diceGRAMSMeat As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeatFoodSupplyPerCapPerDayFAO", yearsGRAMSMeat)

                    For Each item In (From s In cntx.GetSlice("SeriesAGCropFoodSupplyPerCapPerDayFAO", cntx.FirstYear)
                                      Join m In cntx.GetSlice("SeriesAGMeatFoodSupplyPerCapPerDayFAO", cntx.FirstYear) On m.Key Equals s.Key
                                      Select Key = s.Key, Value = s.Value, Meat = m.Value)
                        CGRAMSPC_Crop.Add(item.Key, If(item.Value, estimateWLineFitXyrs(diceGRAMSCrop, cntx.FirstYear, yearsGRAMSCrop, item.Key)))
                        CGRAMSPC_Meat.Add(item.Key, If(item.Meat, estimateWLineFitXyrs(diceGRAMSMeat, cntx.FirstYear, yearsGRAMSMeat, item.Key)))
                    Next
                End If 'if historic mode else forecast mode

                'dsr 2015/11/03 Fill holes in Grams Data
                Dim GlobalCropGrams As Single?, GlobalMeatGrams As Single?, GlobalFishGrams As Single?
                'Use global average ratios of Grams to calories, so first calculate these using countries w/o null Grams data
                GlobalCropCalories = 0
                GlobalMeatCalories = 0
                GlobalFishCalories = 0
                GlobalCropGrams = 0
                GlobalMeatGrams = 0
                GlobalFishGrams = 0
                For Each key In cntx.Regionalization.Keys
                    If CGRAMSPC_Crop(key).HasValue Then
                        'jrs 2018/09/20 changed from POP to CPop
                        GlobalCropCalories = GlobalCropCalories + CCLPC_Crop(key) * CPOP(key)
                        GlobalCropGrams = GlobalCropGrams + CGRAMSPC_Crop(key) * CPOP(key)
                    End If
                    If CGRAMSPC_Meat(key).HasValue Then
                        GlobalMeatCalories = GlobalMeatCalories + CCLPC_Meat(key) * CPOP(key)
                        GlobalMeatGrams = GlobalMeatGrams + CGRAMSPC_Meat(key) * CPOP(key)
                    End If
                    If CGRAMSPC_Fish(key).HasValue Then
                        GlobalFishCalories = GlobalFishCalories + CCLPC_Fish(key) * CPOP(key)
                        GlobalFishGrams = GlobalFishGrams + CGRAMSPC_Fish(key) * CPOP(key)
                    End If
                Next

                Dim GlobalCropGramsPerCal As Single?, GlobalMeatGramsPerCal As Single?, GlobalFishGramsPerCal As Single?
                GlobalCropGramsPerCal = GlobalCropGrams / GlobalCropCalories 'for 2010 this is about 0.5954
                GlobalMeatGramsPerCal = GlobalMeatGrams / GlobalMeatCalories 'for 2010 this is about 0.8180
                GlobalFishGramsPerCal = GlobalFishGrams / GlobalFishCalories 'for 2010 this is about 1.5494

                For Each key In cntx.Regionalization.Keys
                    If Not CGRAMSPC_Crop(key).HasValue Then CGRAMSPC_Crop(key) = CCLPC_Crop(key) * GlobalCropGramsPerCal
                    If Not CGRAMSPC_Meat(key).HasValue Then CGRAMSPC_Meat(key) = CCLPC_Meat(key) * GlobalMeatGramsPerCal
                    If Not CGRAMSPC_Fish(key).HasValue Then CGRAMSPC_Fish(key) = CCLPC_Fish(key) * GlobalFishGramsPerCal

                    'Calculate Total Grams Per Capita Per Day
                    CGRAMSPC_Tot.Add(key, CGRAMSPC_Crop(key) + CGRAMSPC_Meat(key) + CGRAMSPC_Fish(key))

                    'dsr 2015/11/02 Use value of CGRAMSPC_Fish(key) to backfill nulls in CGramsPCFISH(key), for later use
                    If Not CGRAMSPCFISH(key).HasValue Then CGRAMSPCFISH(key) = CGRAMSPC_Fish(key)
                Next
                'dsr 2015/11/03 GRAMS PER CAPITA PER DAY - *** end ***

                'dsr 2015/11/03 NUTRITION - CALORIES PER CAPITA PER DAY, PROTEIN PER CAPITA PER DAY, GRAMS PER CAPITA PER DAY, and UNDERNUTRITION - *** end ***

                'Get all fish data
                Call GetFishDemandSideData(cntx)

                'CROPS and MEAT

                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriesTable As String = "SeriesAGCroptoFoodFAO"
                    Dim yrs As Short() = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    Dim DictTbl As IDictionary(Of String, Single?()) = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CFDEM_Crop.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGMeattoFoodFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CFDEM_Meat.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGCroptoFoodManuFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CFMDEM_Crop.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGMeattoFoodManuFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CFMDEM_Meat.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGCroptoSeedFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CSEEDDEM_Crop.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGMeattoSeedFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CSEEDDEM_Meat.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGCroptoOtherUtilFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CINDEM_Crop.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGMeattoOtherUtilFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CINDEM_Meat.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGCroptoFeedFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CFEDDEM_Crop.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGMeattoFeedFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CFEDDEM_Meat.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGCroptoWasteFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CAGLOSSTRANS_Crop.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGMeattoWasteFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CAGLOSSTRANS_Meat.Add(key, ValHold / 1000000)
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else 'Forecast *********
                    Dim years As Short()
                    years = (From y In cntx.GetYears("SeriesAGCroptoFoodFAO")
                             Where cntx.FirstYear - 20S < y
                             Order By y Descending
                    ).Take(10).ToArray
                    Dim dice As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCroptoFoodFAO", years)

                    Dim years2 As Short()
                    years2 = (From y In cntx.GetYears("SeriesAGMeattoFoodFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice2 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeattoFoodFAO", years2)

                    Dim years3 As Short()
                    years3 = (From y In cntx.GetYears("SeriesAGCroptoFoodManuFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice3 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCroptoFoodManuFAO", years3)

                    Dim years4 As Short()
                    years4 = (From y In cntx.GetYears("SeriesAGMeattoFoodManuFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice4 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeattoFoodManuFAO", years4)

                    Dim years5 As Short()
                    years5 = (From y In cntx.GetYears("SeriesAGCroptoSeedFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice5 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCroptoSeedFAO", years5)

                    Dim years6 As Short()
                    years6 = (From y In cntx.GetYears("SeriesAGMeattoSeedFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice6 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeattoSeedFAO", years6)

                    Dim years7 As Short()
                    years7 = (From y In cntx.GetYears("SeriesAGCroptoOtherUtilFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice7 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCroptoOtherUtilFAO", years7)

                    Dim years8 As Short()
                    years8 = (From y In cntx.GetYears("SeriesAGMeattoOtherUtilFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice8 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeattoOtherUtilFAO", years8)

                    Dim years9 As Short()
                    years9 = (From y In cntx.GetYears("SeriesAGCroptoFeedFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice9 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCroptoFeedFAO", years9)

                    Dim years10 As Short()
                    years10 = (From y In cntx.GetYears("SeriesAGMeattoFeedFAO")
                               Where cntx.FirstYear - 20S < y
                               Order By y Descending
                    ).Take(10).ToArray
                    Dim dice10 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeattoFeedFAO", years10)

                    Dim years11 As Short()
                    years11 = (From y In cntx.GetYears("SeriesAGCroptoWasteFAO")
                               Where cntx.FirstYear - 20S < y
                               Order By y Descending
                    ).Take(10).ToArray
                    Dim dice11 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCroptoWasteFAO", years11)

                    Dim years12 As Short()
                    years12 = (From y In cntx.GetYears("SeriesAGMeattoWasteFAO")
                               Where cntx.FirstYear - 20S < y
                               Order By y Descending
                    ).Take(10).ToArray
                    Dim dice12 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeattoWasteFAO", years12)

                    For Each item In (From s In cntx.GetSlice("SeriesAGCroptoFoodFAO", cntx.FirstYear)
                                      Join s2 In cntx.GetSlice("SeriesAGMeattoFoodFAO", cntx.FirstYear) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAGCroptoFoodManuFAO", cntx.FirstYear) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAGMeattoFoodManuFAO", cntx.FirstYear) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAGCroptoSeedFAO", cntx.FirstYear) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAGMeattoSeedFAO", cntx.FirstYear) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAGCroptoOtherUtilFAO", cntx.FirstYear) On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAGMeattoOtherUtilFAO", cntx.FirstYear) On s.Key Equals s8.Key
                                      Join s9 In cntx.GetSlice("SeriesAGCroptoFeedFAO", cntx.FirstYear) On s.Key Equals s9.Key
                                      Join s10 In cntx.GetSlice("SeriesAGMeattoFeedFAO", cntx.FirstYear) On s.Key Equals s10.Key
                                      Join s11 In cntx.GetSlice("SeriesAGCroptoWasteFAO", cntx.FirstYear) On s.Key Equals s11.Key
                                      Join s12 In cntx.GetSlice("SeriesAGMeattoWasteFAO", cntx.FirstYear) On s.Key Equals s12.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value,
                                  Val5 = s5.Value, Val6 = s6.Value, Val7 = s7.Value, Val8 = s8.Value, Val9 = s9.Value,
                                  Val10 = s10.Value, Val11 = s11.Value, Val12 = s12.Value)
                        CFDEM_Crop.Add(item.Key, If(item.Value, estimateWLineFitXyrs(dice, cntx.FirstYear, years, item.Key)) / 1000000)
                        CFDEM_Meat.Add(item.Key, If(item.Val2, estimateWLineFitXyrs(dice2, cntx.FirstYear, years2, item.Key)) / 1000000)
                        CFMDEM_Crop.Add(item.Key, If(item.Val3, estimateWLineFitXyrs(dice3, cntx.FirstYear, years3, item.Key)) / 1000000)
                        CFMDEM_Meat.Add(item.Key, If(item.Val4, estimateWLineFitXyrs(dice4, cntx.FirstYear, years4, item.Key)) / 1000000)
                        CSEEDDEM_Crop.Add(item.Key, If(item.Val5, estimateWLineFitXyrs(dice5, cntx.FirstYear, years5, item.Key)) / 1000000)
                        CSEEDDEM_Meat.Add(item.Key, If(item.Val6, estimateWLineFitXyrs(dice6, cntx.FirstYear, years6, item.Key)) / 1000000)
                        CINDEM_Crop.Add(item.Key, If(item.Val7, estimateWLineFitXyrs(dice7, cntx.FirstYear, years7, item.Key)) / 1000000)
                        CINDEM_Meat.Add(item.Key, If(item.Val8, estimateWLineFitXyrs(dice8, cntx.FirstYear, years8, item.Key)) / 1000000)
                        CFEDDEM_Crop.Add(item.Key, If(item.Val9, estimateWLineFitXyrs(dice9, cntx.FirstYear, years9, item.Key)) / 1000000)
                        CFEDDEM_Meat.Add(item.Key, If(item.Val10, estimateWLineFitXyrs(dice10, cntx.FirstYear, years10, item.Key)) / 1000000)
                        CAGLOSSTRANS_Crop.Add(item.Key, If(item.Val11, estimateWLineFitXyrs(dice11, cntx.FirstYear, years11, item.Key)) / 1000000)
                        CAGLOSSTRANS_Meat.Add(item.Key, If(item.Val12, estimateWLineFitXyrs(dice12, cntx.FirstYear, years12, item.Key)) / 1000000)
                    Next
                End If 'End of if history else forecast

                For Each key In cntx.Regionalization.Keys
                    'dsr 2015/01/19 Fill holes and fix negative values for crops and meat (fish dealt with in GetFishUseData)
                    'dsr 2015/12/22 Treat 0s like negative values
                    'jrs 2018/09/20 changed from POP to CPop
                    'for food, calculate missing values as grams per capita per day * population * days in a year
                    If Not CFDEM_Crop(key).HasValue Or CFDEM_Crop(key) <= 0 Then CFDEM_Crop(key) = CGRAMSPC_Crop(key) * CPOP(key) * 365 / 1000000
                    If Not CFDEM_Meat(key).HasValue Or CFDEM_Meat(key) <= 0 Then CFDEM_Meat(key) = CGRAMSPC_Meat(key) * CPOP(key) * 365 / 1000000
                    'for other categories, use a minimal value of one ton (FM and Seed add to one ton)
                    If Not CFMDEM_Crop(key).HasValue Or CFMDEM_Crop(key) <= 0 Then CFMDEM_Crop(key) = 0.0000005
                    If Not CFMDEM_Meat(key).HasValue Or CFMDEM_Meat(key) <= 0 Then CFMDEM_Meat(key) = 0.0000005
                    If Not CSEEDDEM_Crop(key).HasValue Or CSEEDDEM_Crop(key) <= 0 Then CSEEDDEM_Crop(key) = 0.0000005
                    If Not CSEEDDEM_Meat(key).HasValue Or CSEEDDEM_Meat(key) <= 0 Then CSEEDDEM_Meat(key) = 0.0000005
                    If Not CINDEM_Crop(key).HasValue Or CINDEM_Crop(key) <= 0 Then CINDEM_Crop(key) = 0.000001
                    If Not CINDEM_Meat(key).HasValue Or CINDEM_Meat(key) <= 0 Then CINDEM_Meat(key) = 0.000001
                    If Not CFEDDEM_Crop(key).HasValue Or CFEDDEM_Crop(key) <= 0 Then CFEDDEM_Crop(key) = 0.000001
                    If Not CFEDDEM_Meat(key).HasValue Or CFEDDEM_Meat(key) <= 0 Then CFEDDEM_Meat(key) = 0.000001
                    'If CountryName(key) = "United States of America" Then Stop
                    If Not CAGLOSSTRANS_Crop(key).HasValue Or CAGLOSSTRANS_Crop(key) <= 0 Then CAGLOSSTRANS_Crop(key) = 0.000001
                    If Not CAGLOSSTRANS_Meat(key).HasValue Or CAGLOSSTRANS_Meat(key) <= 0 Then CAGLOSSTRANS_Meat(key) = 0.000001
                    'dsr 2015/02/22 Set value for food manufacturing as sum of commodities to Seed and Food Manu
                    CFMDEM_Crop(key) = CSEEDDEM_Crop(key) + CFMDEM_Crop(key)
                    CFMDEM_Meat(key) = CSEEDDEM_Meat(key) + CFMDEM_Meat(key)
                Next
                'dsr 2015/01/19 DEMAND/USE SIDE - *** End ***

                'dsr 2015/01/15 SUPPLY SIDE - *** Start ***
                Call GetFishSupplySideData(cntx)

                'CROPS and MEAT
                Dim AgpCrops As Single?
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    'AgpCrops = getClosestYrVal(AgpCropsTbl, FirstYearHistory, HistDist) / 1000000 'dsr 2014/12/21 original data in metric tons
                    Dim SeriesTable As String = "SeriesAGCropProductionFAO"
                    Dim yrs As Short() = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    Dim DictTbl As IDictionary(Of String, Single?()) = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        If (Not ValHold.HasValue) OrElse ValHold <= 0 Then
                            ValHold = 3 * LDCrop(key)
                        Else
                            ValHold = ValHold / 1000000
                        End If
                        AGPTot.Add(key, ValHold)
                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    Dim years As Short()
                    years = (From y In cntx.GetYears("SeriesAGCropProductionFAO")
                             Where cntx.FirstYear - 20S < y
                             Order By y Descending
                    ).Take(10).ToArray
                    Dim dice As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCropProductionFAO", years)

                    For Each item In (From s In cntx.GetSlice("SeriesAGCropProductionFAO", cntx.FirstYear)
                                      Select Key = s.Key, Value = s.Value)
                        AGPTot.Add(item.Key, 0)
                        'jrs 2026/03/27
                        bAGPTotData.Add(item.Key, item.Value.HasValue)
                        AgpCrops = If(item.Value, estimateWLineFitXyrs(dice, cntx.FirstYear, years, item.Key)) / 1000000
                        If Not AgpCrops.HasValue Or AgpCrops <= 0 Then AgpCrops = 3 * LDCrop(item.Key)
                        AGPTot(item.Key) = AgpCrops 'dsr 2014/12/21 agpCer + agpRoots + agpPul + agpVeg + agpFruit
                    Next
                End If 'End of if history else forecast

                'dsr 2014/12/21 CROP PRODUCTION - *** End ***

                'dsr 2014/12/21 MEAT PRODUCTION - *** Start ***
                'dsr 2014/12/21 Read in meat production, no need to recompute exports to balance equation
                '       -----------------------------------------------------------------
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    'MeatProd(ICount%) = getClosestYrVal(MeatProdTbl, FirstYearHistory, HistDist) / 1000000 'dsr 2014/12/18 original data in metric tons
                    Dim SeriesTable As String = "SeriesAGMeatProductionFAO"
                    Dim yrs As Short() = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    Dim DictTbl As IDictionary(Of String, Single?()) = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        MeatProd.Add(key, ValHold / 1000000)
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else 'Forecast *********
                    Dim years As Short()
                    years = (From y In cntx.GetYears("SeriesAGMeatProductionFAO")
                             Where cntx.FirstYear - 20S < y
                             Order By y Descending
                    ).Take(10).ToArray
                    Dim dice As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeatProductionFAO", years)

                    For Each item In (From s In cntx.GetSlice("SeriesAGMeatProductionFAO", cntx.FirstYear)
                                      Select Key = s.Key, Value = s.Value)
                        MeatProd.Add(item.Key, If(item.Value, estimateWLineFitXyrs(dice, cntx.FirstYear, years, item.Key)) / 1000000)
                    Next
                End If 'End of if history else forecast

                For Each key In cntx.Regionalization.Keys
                    If Not MeatProd(key).HasValue Or MeatProd(key) <= 0 Then MeatProd(key) = 0.000001 '0.023 * CGDP(key)  'dsr 2014/12/21 median across countries in 2010
                Next
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriesTable As String = "SeriesAGBovineMeatProductionFAO"
                    Dim yrs As Short() = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    Dim DictTbl As IDictionary(Of String, Single?()) = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        BovineMeatProd.Add(key, ValHold)
                    Next
                    SeriesTable = "SeriesAGMeatOtherProductionFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        MeatOtherProd.Add(key, ValHold)
                    Next
                    SeriesTable = "SeriesAGMuttonandGoatMeatProductionFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        MuttonAndGoatMeatProd.Add(key, ValHold)
                    Next
                    SeriesTable = "SeriesAGPigmeatProductionFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        PigMeatProd.Add(key, ValHold)
                    Next
                    SeriesTable = "SeriesAGPoultryMeatProductionFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        PoultryMeatProd.Add(key, ValHold)
                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    Dim years As Short()
                    years = (From y In cntx.GetYears("SeriesAGBovineMeatProductionFAO")
                             Where cntx.FirstYear - 20S < y
                             Order By y Descending
                    ).Take(10).ToArray
                    Dim dice As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGBovineMeatProductionFAO", years)

                    Dim years2 As Short()
                    years2 = (From y In cntx.GetYears("SeriesAGMeatOtherProductionFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice2 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeatOtherProductionFAO", years2)

                    Dim years3 As Short()
                    years3 = (From y In cntx.GetYears("SeriesAGMuttonandGoatMeatProductionFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice3 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMuttonandGoatMeatProductionFAO", years3)

                    Dim years4 As Short()
                    years4 = (From y In cntx.GetYears("SeriesAGPigmeatProductionFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice4 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGPigmeatProductionFAO", years4)

                    Dim years5 As Short()
                    years5 = (From y In cntx.GetYears("SeriesAGPoultryMeatProductionFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice5 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGPoultryMeatProductionFAO", years5)

                    For Each item In (From s In cntx.GetSlice("SeriesAGBovineMeatProductionFAO", cntx.FirstYear)
                                      Join s2 In cntx.GetSlice("SeriesAGMeatOtherProductionFAO", cntx.FirstYear) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAGMuttonandGoatMeatProductionFAO", cntx.FirstYear) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAGPigmeatProductionFAO", cntx.FirstYear) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAGPoultryMeatProductionFAO", cntx.FirstYear) On s.Key Equals s5.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value,
                                  Val5 = s5.Value)
                        BovineMeatProd.Add(item.Key, If(item.Value, estimateWLineFitXyrs(dice, cntx.FirstYear, years, item.Key)))
                        'jrs 2026/02/16 fixing order
                        MeatOtherProd.Add(item.Key, If(item.Val2, estimateWLineFitXyrs(dice2, cntx.FirstYear, years2, item.Key)))
                        MuttonAndGoatMeatProd.Add(item.Key, If(item.Val3, estimateWLineFitXyrs(dice3, cntx.FirstYear, years3, item.Key)))
                        PigMeatProd.Add(item.Key, If(item.Val4, estimateWLineFitXyrs(dice4, cntx.FirstYear, years4, item.Key)))
                        PoultryMeatProd.Add(item.Key, If(item.Val5, estimateWLineFitXyrs(dice5, cntx.FirstYear, years5, item.Key)))
                        'jrs 2026/02/16 MeatOtherProd.Add(item.Key, If(item.Val5, estimateWLineFitXyrs(dice5, cntx.FirstYear, years5, item.Key)))
                    Next
                End If 'End of if history else forecast

                Dim EstimatorFeed As SeriesEstimator = cntx.GetRegression("GDP/Capita (PPP) Versus Feed Requirements").ToEstimator(CGDPPCP)
                Dim fedreq As Double?, GLandCAP As Double?, MinLDProd As Single?

                For Each key In cntx.Regionalization.Keys
                    'If key = "Djibouti" Then Stop
                    'If key = "Somalia" Then Stop
                    'If key = "Sudan South" Then Stop
                    'dsr 2015/04/17 If all entries for animal production are blank, let TotalAnimalProduction = MeatProd
                    '               Otherwise set blank entries to 0 and calculate TotalAnimalProduction as sum of non-blank entries
                    If Not BovineMeatProd(key).HasValue And Not MuttonAndGoatMeatProd(key).HasValue And Not PigMeatProd(key).HasValue And
                 Not PoultryMeatProd(key).HasValue And Not MeatOtherProd(key).HasValue Then
                        TotalAnimalProd.Add(key, CSng(0.3 * MeatProd(key))) 'dsr 2015/04/17 On average, animals make up 30% of meat production
                    Else
                        If Not BovineMeatProd(key).HasValue Then BovineMeatProd(key) = 0
                        If Not MuttonAndGoatMeatProd(key).HasValue Then MuttonAndGoatMeatProd(key) = 0
                        If Not PigMeatProd(key).HasValue Then PigMeatProd(key) = 0
                        If Not PoultryMeatProd(key).HasValue Then PoultryMeatProd(key) = 0
                        If Not MeatOtherProd(key).HasValue Then MeatOtherProd(key) = 0
                        TotalAnimalProd.Add(key, BovineMeatProd(key) + MuttonAndGoatMeatProd(key) + PigMeatProd(key) _
                                                            + PoultryMeatProd(key) + MeatOtherProd(key))
                    End If
                    'dsr 2015/11/11 Moved out of If loop
                    'dsr 2015/03/01 Do not let total Animal Production exceed 99% of Meat Production
                    'jrs 2026/03/16 I don't think we're using the same units here, MeatProd has already been divided by 1M (Millin Tons), but not TotalAnimalProd
                    TotalAnimalProd(key) = Amin(CSng(0.99 * MeatProd(key)), TotalAnimalProd(key))
                    'jrs 2026/02/16 adding a lower limit
                    TotalAnimalProd(key) = CSng(AMAX(TotalAnimalProd(key), 0.0000001))
                    'dsr 2015/03/01 Calculate Livestock Herd by dividing total Animal Prodcution by slaughter rate
                    LiveHerd.Add(key, TotalAnimalProd(key) / slr)
                    'If CountryName(key) = "United States of America" Then Stop
                    'dsr 2015/03/01 Calculate CAGPMILKEGGS as Meat Production minus Animal Production; make sure each country has a minimum

                    'If key = 4 Then Stop
                    CAGPMILKEGGS.Add(key, MeatProd(key) - TotalAnimalProd(key))

                    'bbh 2017/05/04 start   Moved from higher up because LiveHerd was not computed yet
                    '   RESTRICT FEED DEMAND of CROPS IF GRAZING LAND PRODUCTION NOT BEING USED - *** start ***
                    '   --------------------------------------------------------------
                    fedreq = EstimatorFeed(key)
                    GLandCAP = (LiveHerd(key) * fedreq - CFEDDEM_Crop(key)) / LDGraz(key)
                    MinLDProd = 0.01 'One-hundredth ton of meat per hectare (e.g. Saudi desert)
                    'jrs 2026/03/16 but LiveHerd and FEDDEM are both in Million Tons,
                    'so I think we need to multiply GLandCAP * 1M
                    'or divide MinLDProd by 1M
                    If GLandCAP <= MinLDProd Then 'Reduce feed demand
                        CFEDDEM_Crop(key) = CSng(AMAX(0.01, LiveHerd(key) * fedreq - MinLDProd * LDGraz(key)))
                        'If CountryName(key) = "United States of America" Then Stop
                    End If 'GLandCAP <= MinLDProd
                Next
                'dsr 2014/12/21 MEAT PRODUCTION - *** End ***

                'dsr 2014/12/22 CROP and MEAT IMPORTS and EXPORTS - *** Start ***
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriesTable As String = "SeriesAGCropImportsFAO"
                    Dim yrs As Short() = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    Dim DictTbl As IDictionary(Of String, Single?()) = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        AGMCrop.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGCropExportsFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        AGXCrop.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGMeatImportsFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        AGMMeat.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGMeatExportsFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        AGXMeat.Add(key, ValHold / 1000000)
                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    Dim years As Short()
                    years = (From y In cntx.GetYears("SeriesAGCropImportsFAO")
                             Where cntx.FirstYear - 20S < y
                             Order By y Descending
                    ).Take(10).ToArray
                    Dim dice As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCropImportsFAO", years)

                    Dim years2 As Short()
                    years2 = (From y In cntx.GetYears("SeriesAGCropExportsFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice2 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCropExportsFAO", years2)

                    Dim years3 As Short()
                    years3 = (From y In cntx.GetYears("SeriesAGMeatImportsFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice3 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeatImportsFAO", years3)

                    Dim years4 As Short()
                    years4 = (From y In cntx.GetYears("SeriesAGMeatExportsFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice4 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeatExportsFAO", years4)

                    For Each item In (From s In cntx.GetSlice("SeriesAGCropImportsFAO", cntx.FirstYear)
                                      Join s2 In cntx.GetSlice("SeriesAGCropExportsFAO", cntx.FirstYear) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAGMeatImportsFAO", cntx.FirstYear) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAGMeatExportsFAO", cntx.FirstYear) On s.Key Equals s4.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value)
                        AGMCrop.Add(item.Key, If(item.Value, estimateWLineFitXyrs(dice, cntx.FirstYear, years, item.Key)) / 1000000)
                        AGXCrop.Add(item.Key, If(item.Val2, estimateWLineFitXyrs(dice2, cntx.FirstYear, years2, item.Key)) / 1000000)
                        AGMMeat.Add(item.Key, If(item.Val3, estimateWLineFitXyrs(dice3, cntx.FirstYear, years3, item.Key)) / 1000000)
                        AGXMeat.Add(item.Key, If(item.Val4, estimateWLineFitXyrs(dice4, cntx.FirstYear, years4, item.Key)) / 1000000)
                    Next
                End If 'End of if history else forecast

                For Each key In cntx.Regionalization.Keys
                    'If Not AGMCrop(key).HasValue Or AGMCrop(key) <= 0 Then AGMCrop(key) = 0.000001
                    'If Not AGXCrop(key).HasValue Or AGXCrop(key) <= 0 Then AGXCrop(key) = 0.000001
                    'If Not AGMMeat(key).HasValue Or AGMMeat(key) <= 0 Then AGMMeat(key) = 0.000001
                    'If Not AGXMeat(key).HasValue Or AGXMeat(key) <= 0 Then AGXMeat(key) = 0.000001
                    'bbh 2022/10/27 Replaced 4 lines above with 4 below to give bigger seeds and less model volatility in early years
                    If Not AGMCrop(key).HasValue Or AGMCrop(key) <= 0 Then AGMCrop(key) = 0.00001
                    If Not AGXCrop(key).HasValue Or AGXCrop(key) <= 0 Then AGXCrop(key) = 0.00001
                    If Not AGMMeat(key).HasValue Or AGMMeat(key) <= 0 Then AGMMeat(key) = 0.00001
                    If Not AGXMeat(key).HasValue Or AGXMeat(key) <= 0 Then AGXMeat(key) = 0.00001
                Next
                'dsr 2014/12/22 READ in CROP and MEAT IMPORTS and EXPORTS - *** End ***

                'dsr 2014/12/22 READ in CROP and MEAT TOTAL DOMESTIC SUPPLY and NORMALIZE PRODUCTION, EXPORTS, and IMPORTS - *** Start ***
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriesTable As String = "SeriesAGCropDomesticSupplyFAO"
                    Dim yrs As Short() = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    Dim DictTbl As IDictionary(Of String, Single?()) = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CAGDOMSUPP_Crop.Add(key, ValHold / 1000000)
                    Next
                    SeriesTable = "SeriesAGMeatDomesticSupplyFAO"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CAGDOMSUPP_Meat.Add(key, ValHold / 1000000)
                    Next

                    'mti 2019/04/29  Flex Hist Base End


                Else 'Forecast *********
                    Dim years As Short()
                    years = (From y In cntx.GetYears("SeriesAGCropDomesticSupplyFAO")
                             Where cntx.FirstYear - 20S < y
                             Order By y Descending
                    ).Take(10).ToArray
                    Dim dice As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCropDomesticSupplyFAO", years)

                    Dim years2 As Short()
                    years2 = (From y In cntx.GetYears("SeriesAGMeatDomesticSupplyFAO")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                    Dim dice2 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeatDomesticSupplyFAO", years2)

                    For Each item In (From s In cntx.GetSlice("SeriesAGCropDomesticSupplyFAO", cntx.FirstYear)
                                      Join s2 In cntx.GetSlice("SeriesAGMeatDomesticSupplyFAO", cntx.FirstYear) On s.Key Equals s2.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value)
                        CAGDOMSUPP_Crop.Add(item.Key, If(item.Value, estimateWLineFitXyrs(dice, cntx.FirstYear, years, item.Key)) / 1000000)
                        CAGDOMSUPP_Meat.Add(item.Key, If(item.Val2, estimateWLineFitXyrs(dice2, cntx.FirstYear, years2, item.Key)) / 1000000)
                    Next
                End If 'End of if history else forecast

                For Each key In cntx.Regionalization.Keys
                    If Not CAGDOMSUPP_Crop(key).HasValue Or CAGDOMSUPP_Crop(key) <= 0 _
                    Then CAGDOMSUPP_Crop(key) = CSng(AMAX(AGPTot(key) + AGMCrop(key) - AGXCrop(key), 0.000001))
                    If Not CAGDOMSUPP_Meat(key).HasValue Or CAGDOMSUPP_Meat(key) <= 0 _
                    Then CAGDOMSUPP_Meat(key) = CSng(AMAX(MeatProd(key) + AGMMeat(key) - AGXMeat(key), 0.000001))
                Next
                'dsr 2014/12/22 READ in CROP and MEAT TOTAL DOMESTIC SUPPLY and NORMALIZE PRODUCTION, EXPORTS, and IMPORTS - *** End ***

                'dsr 2015/11/11 Normalize production, imports, and exports to domestic supply when remove stock variations *** start ***
                'Logic: Stock variation (SV) = Dom Supp - (Prod + Imp - Exp)
                '       IF SV > 0, i.e., stocks are being drawn down, achieve balance by increasing Prod and Imp, while decreasing Exp
                '       If SV < 0, i.e., stocks are being added to, achieve balance by decreasing Prod and Imp, while increasing Exp
                '       Base size of adjustments on relative sizes of Prod, Imp, and Exp, but do not let Exp, Imp, or Prod fall below 0
                Dim SV As Double?, ExportAdj As Double?, ImportAdj As Double?, AnimalShare As Single?
                Dim AGPTotD As Dictionary(Of String, Double?) = New Dictionary(Of String, Double?)

                Dim AGMCropD As Dictionary(Of String, Double?) = New Dictionary(Of String, Double?)
                Dim AGXCropD As Dictionary(Of String, Double?) = New Dictionary(Of String, Double?)
                Dim AGMMeatD As Dictionary(Of String, Double?) = New Dictionary(Of String, Double?)
                Dim AGXMeatD As Dictionary(Of String, Double?) = New Dictionary(Of String, Double?)

                For Each key In cntx.Regionalization.Keys
                    'If key = "Djibouti" Then Stop
                    'Crops
                    'dsr 2015/12/21 Error found in next line and corrected
                    'SV = CAGDOMSUPP_Crop(key) - (AGPTot(key) + AGXCrop(key) - AGMCrop(key))
                    SV = CDbl(CAGDOMSUPP_Crop(key).ToString) - (CDbl(AGPTot(key).ToString) + CDbl(AGMCrop(key).ToString) - CDbl(AGXCrop(key).ToString))
                    ExportAdj = Amin(AGXCrop(key) - 0.000001,
                    CDbl(AGXCrop(key).ToString()) / CDbl((AGPTot(key) + AGMCrop(key) + AGXCrop(key)).ToString()) * SV)
                    AGXCropD.Add(key, CDbl(AGXCrop(key).ToString()) - ExportAdj)
                    AGXCrop(key) = CSng(AGXCropD(key))
                    ImportAdj = AMAX(-AGMCrop(key) + 0.000001,
                    CDbl(AGMCrop(key).ToString()) / CDbl((AGPTot(key) + AGMCrop(key)).ToString()) * (SV - ExportAdj))
                    AGMCropD.Add(key, CDbl(AGMCrop(key).ToString()) + ImportAdj)
                    AGMCrop(key) = CSng(AGMCropD(key))
                    AGPTotD.Add(key, AMAX(0.000001, CDbl(AGPTot(key).ToString) + (SV - ExportAdj - ImportAdj)))
                    AGPTot(key) = CSng(AGPTotD(key))

                    'Meat
                    'remember to update values of TotalAnimalProd, CAGPMILKEGGS, and LiveHerd when change MeatProd
                    'dsr 2015/12/21 Error found in next line and corrected
                    'SV = CAGDOMSUPP_Meat(key) - (MeatProd(key) + AGXMeat(key) - AGMMeat(key))
                    SV = CDbl(CAGDOMSUPP_Meat(key).ToString) - (CDbl(MeatProd(key).ToString) + CDbl(AGMMeat(key).ToString) - CDbl(AGXMeat(key).ToString))
                    ExportAdj = Amin(AGXMeat(key) - 0.000001,
                    CDbl(AGXMeat(key).ToString) / CDbl((MeatProd(key) + AGMMeat(key) + AGXMeat(key)).ToString) * SV)
                    AGXMeatD.Add(key, CDbl(AGXMeat(key).ToString()) - ExportAdj)
                    AGXMeat(key) = CSng(AGXMeatD(key))
                    ImportAdj = AMAX(-AGMMeat(key) + 0.000001,
                    CDbl(AGMMeat(key).ToString) / CDbl((MeatProd(key) + AGMMeat(key)).ToString) * (SV - ExportAdj))
                    AGMMeatD.Add(key, CDbl(AGMMeat(key).ToString()) + ImportAdj)
                    AGMMeat(key) = CSng(AGMMeatD(key))
                    AnimalShare = TotalAnimalProd(key) / MeatProd(key)
                    MeatProd(key) = CSng(AMAX(0.000001, MeatProd(key) + (SV - ExportAdj - ImportAdj)))
                    TotalAnimalProd(key) = MeatProd(key) * AnimalShare
                    'If key = 4 Then Stop
                    CAGPMILKEGGS(key) = MeatProd(key) - TotalAnimalProd(key)
                    LiveHerd(key) = TotalAnimalProd(key) / slr
                Next

                'dsr 2015/11/11 Normalize production, imports, and exports to domestic supply when remove stock variations *** end ***

                'dsr 2015/01/19 SUPPLY SIDE - *** End ***

                '--------------------------------------------------------------------------------------------------------------------
                ' dsr 2015/12/22 COMPARE DOMESTIC SUPPLY TO SUM OF DEMANDS FIRST TIME for CROPS - *** start ***
                ' if too much demand increase imports of crops
                ' if too much supply increase all demands except food proportionately so as to not mess up food to grams per capita calculations
                Dim CropDemand As Single?, increase As Single?, DemandsToIncrease As Single?
                For Each key In cntx.Regionalization.Keys
                    'If key = "Djibouti" Then Stop
                    CropDemand = CFDEM_Crop(key) + CFMDEM_Crop(key) + CFEDDEM_Crop(key) +
                     CINDEM_Crop(key) + CAGLOSSTRANS_Crop(key)
                    If CAGDOMSUPP_Crop(key) <= CropDemand Then
                        AGMCropD(key) = AGMCropD(key) + (CropDemand - CAGDOMSUPP_Crop(key))
                        AGMCrop(key) = CSng(AGMCropD(key))
                    Else
                        increase = CAGDOMSUPP_Crop(key) - CropDemand
                        DemandsToIncrease = CFMDEM_Crop(key) + CFEDDEM_Crop(key) + CINDEM_Crop(key) + CAGLOSSTRANS_Crop(key)
                        CFMDEM_Crop(key) = CFMDEM_Crop(key) + increase * (CFMDEM_Crop(key) / DemandsToIncrease)
                        CFEDDEM_Crop(key) = CFEDDEM_Crop(key) + increase * (CFEDDEM_Crop(key) / DemandsToIncrease)
                        'If CountryName(key) = "United States of America" Then Stop
                        CINDEM_Crop(key) = CINDEM_Crop(key) + increase * (CINDEM_Crop(key) / DemandsToIncrease)
                        CAGLOSSTRANS_Crop(key) = CAGLOSSTRANS_Crop(key) + (increase * CAGLOSSTRANS_Crop(key) / DemandsToIncrease)
                        CropDemand = CFDEM_Crop(key) + CFMDEM_Crop(key) + CFEDDEM_Crop(key) +
                        CINDEM_Crop(key) + CAGLOSSTRANS_Crop(key)
                    End If
                Next

                ' bbh 2018/03/09 Import (and offseting export) adjustment for those with missing data
                '    without adjustment, seeds of only .00001 result in inabilty to import more if needed
                '----------------------------------------------------------------------------------------
                Dim ImportRatio As Double?, AdjAmount As Double?
                For Each key In cntx.Regionalization.Keys
                    ImportRatio = AGMCropD(key) / (CFDEM_Crop(key) + CFEDDEM_Crop(key))
                    '2015 data show 3% or above for all countries with import data;
                    'CFEDDEM only needed for Micronesia and appears a data error with way too much feddem relatively
                    If ImportRatio < 0.03 Then
                        AdjAmount = 0.03 * (CFDEM_Crop(key) + CFEDDEM_Crop(key)) - AGMCropD(key)
                        AGMCropD(key) = AGMCropD(key) + AdjAmount
                        AGMCrop(key) = CSng(AGMCropD(key))
                        AGXCropD(key) = AGXCropD(key) + AdjAmount 'All balanced above so maintain balance
                        AGXCrop(key) = CSng(AGXCropD(key))
                    End If
                Next

                ' dsr 2015/12/22 COMPARE DOMESTIC SUPPLY TO SUM OF DEMANDS FIRST TIME for CROPS - *** end ***
                '--------------------------------------------------------------------------------------------------------------------

                'dsr 2014/12/22 ADJUST GLOBAL CROP and MEAT IMPORTS to MATCH EXPORTS - *** Start ***
                Dim GlobalCropImports As Double?, GlobalCropExports As Double?
                Dim GlobalMeatImports As Double?, GlobalMeatExports As Double?
                Dim GlobalTradeCrops As Double?, GlobalTradeMeat As Double?

                GlobalCropImports = 0
                GlobalCropExports = 0
                GlobalMeatImports = 0
                GlobalMeatExports = 0
                '    GlobalFishImport = 0
                '    GlobalFishExport = 0
                For Each key In cntx.Regionalization.Keys
                    GlobalCropImports = GlobalCropImports + AGMCropD(key)
                    GlobalCropExports = GlobalCropExports + AGXCropD(key)
                    GlobalMeatImports = GlobalMeatImports + AGMMeatD(key)
                    GlobalMeatExports = GlobalMeatExports + AGXMeatD(key)
                    '        GlobalFishImport = GlobalFishImport + AGMFish(Key)
                    '        GlobalFishExport = GlobalFishExport + AGXFish(Key)
                Next

                GlobalTradeCrops = 0.5 * (GlobalCropImports + GlobalCropExports)
                GlobalTradeMeat = 0.5 * (GlobalMeatImports + GlobalMeatExports)

                For Each key In cntx.Regionalization.Keys
                    AGMCropD(key) = CDbl(AGMCropD(key).ToString) * (GlobalTradeCrops / GlobalCropImports)
                    AGMCrop(key) = CSng(AGMCropD(key))
                    AGXCropD(key) = CDbl(AGXCropD(key).ToString) * (GlobalTradeCrops / GlobalCropExports)
                    AGXCrop(key) = CSng(AGXCropD(key))
                    AGMMeatD(key) = CDbl(AGMMeatD(key).ToString) * (GlobalTradeMeat / GlobalMeatImports)
                    AGMMeat(key) = CSng(AGMMeatD(key))
                    AGXMeatD(key) = CDbl(AGXMeatD(key).ToString) * (GlobalTradeMeat / GlobalMeatExports)
                    AGXMeat(key) = CSng(AGXMeatD(key))
                Next

                'dsr 2014/12/22 RECOMPUTE TOTAL DOMESTIC SUPPLY GIVEN POSSIBLE CHANGES IN IMPORTS - *** Start ***
                For Each key In cntx.Regionalization.Keys
                    CAGDOMSUPP_Crop(key) = AGPTot(key) + AGMCrop(key) - AGXCrop(key)
                    CAGDOMSUPP_Meat(key) = MeatProd(key) + AGMMeat(key) - AGXMeat(key)
                    '        CAGDOMSUPP(Key, 3) = Aquaculture(Key) + CAGFISHCATCH(Key) + AGMFish(Key) - AGXFish(Key)
                Next

                'dsr 2014/12/22 RECOMPUTE TOTAL DOMESTIC SUPPLY GIVEN POSSIBLE CHANGES IN IMPORTS - *** End ***
                'dsr 2014/12/22 ADJUST GLOBAL CROP, MEAT and FISH IMPORTS to EXPORTS - *** End ***

                '--------------------------------------------------------------------------------------------------------------------
                ' dsr 2015/11/05 COMPARE DOMESTIC SUPPLY TO SUM OF DEMANDS SECOND TIME for CROPS AND ALSO NOW FOR MEAT AND FISH - *** start ***
                ' if too much demand increase production so as to not mess up trade balances
                ' if too much supply increase all demands except food proportionately so as to not mess up food to grams per capita calculations
                Dim Demand As Single?, oldAgptot As Single?, adjfactforland As Double?
                Dim FishProdIncrease As Single?, CatchShare As Single?
                For Each key In cntx.Regionalization.Keys
                    'If key = "Djibouti" Then Stop
                    'Crop
                    Demand = CFDEM_Crop(key) + CFMDEM_Crop(key) + CFEDDEM_Crop(key) +
                  CINDEM_Crop(key) + CAGLOSSTRANS_Crop(key)
                    'mti 2026/04/08 start - Fix Djibouti and Belgium H&F transient in AGP
                    Dim adjDemand As Boolean = False

                    If CAGDOMSUPP_Crop(key) <= Demand Then
                        'jrs 2026/03/27 only adjust if there's no data, otherwise causing transients
                        If Not bAGPTotData(key) Then
                            'dsr 2015/12/22 When adjust crop production, also adjust cropland to keep yield in check *** Start ***
                            oldAgptot = AGPTot(key)
                            Dim adjSupply As Single = CSng(Amin(0.2 * oldAgptot, 0.5 * (Demand - CAGDOMSUPP_Crop(key)))) 'split adjustment 50/50 between demand ad supply; and, make sure adjustment is not too big when supply side is much smaller (like Djibouti), to avoid big jumps; 1.2 is arbitrary and can be changed if needed
                            AGPTot(key) = AGPTot(key) + (Demand - CAGDOMSUPP_Crop(key))
                            'mti 2026/04/13 AGPTot(key) = oldAgptot + adjSupply

                            LDCrop(key) = LDCrop(key) * (AGPTot(key) / oldAgptot)
                            'dsr 2015/12/22 Adjust other land types to keep sum to total land
                            adjfactforland = (CLandArea(key) - LDCrop(key)) / (LDGraz(key) + LDFor(key) + LDOth(key) + LDUrb(key))
                            LDGraz(key) = CSng(LDGraz(key) * adjfactforland)
                            LDFor(key) = CSng(LDFor(key) * adjfactforland)
                            LDOth(key) = CSng(LDOth(key) * adjfactforland)
                            LDUrb(key) = CSng(LDUrb(key) * adjfactforland)
                            'dsr 2015/12/11 When adjust crop production, also adjust cropland to keep yield in check *** End ***
                            CAGDOMSUPP_Crop(key) = Demand
                            'mti 2026/04/13 CAGDOMSUPP_Crop(key) = CAGDOMSUPP_Crop(key) + adjSupply
                        End If
                        'remaining adjustment, if any, goes to demand side
                    Else 'mti 2026/04/13
                        increase = CAGDOMSUPP_Crop(key) - Demand
                        DemandsToIncrease = CFMDEM_Crop(key) + CFEDDEM_Crop(key) + CINDEM_Crop(key) + CAGLOSSTRANS_Crop(key)
                        CFMDEM_Crop(key) = CFMDEM_Crop(key) + increase * (CFMDEM_Crop(key) / DemandsToIncrease)
                        CFEDDEM_Crop(key) = CFEDDEM_Crop(key) + increase * (CFEDDEM_Crop(key) / DemandsToIncrease)
                        'If CountryName(Key) = "United States of America" Then Stop
                        CINDEM_Crop(key) = CINDEM_Crop(key) + increase * (CINDEM_Crop(key) / DemandsToIncrease)
                        CAGLOSSTRANS_Crop(key) = CAGLOSSTRANS_Crop(key) + (increase * CAGLOSSTRANS_Crop(key) / DemandsToIncrease)
                        Demand = CFDEM_Crop(key) + CFMDEM_Crop(key) + CFEDDEM_Crop(key) +
                    CINDEM_Crop(key) + CAGLOSSTRANS_Crop(key)
                    End If 'mti 2026/04/13
                    'mti 2026/04/08 end - Fix Djibouti and Belgium H&F transient in AGP

                    'Meat
                    Demand = CFDEM_Meat(key) + CFMDEM_Meat(key) + CFEDDEM_Meat(key) +
                  CINDEM_Meat(key) + CAGLOSSTRANS_Meat(key)
                    If CAGDOMSUPP_Meat(key) <= Demand Then
                        'share between animal production and other animal products
                        'remember to update values of TotalAnimalProd, CAGPMILKEGGS, and LiveHerd when change MeatProd
                        AnimalShare = TotalAnimalProd(key) / MeatProd(key)
                        MeatProd(key) = MeatProd(key) + (Demand - CAGDOMSUPP_Meat(key))
                        TotalAnimalProd(key) = MeatProd(key) * AnimalShare
                        'If Key = 4 Then Stop
                        CAGPMILKEGGS(key) = MeatProd(key) - TotalAnimalProd(key)
                        LiveHerd(key) = TotalAnimalProd(key) / slr
                        CAGDOMSUPP_Meat(key) = Demand
                    Else
                        increase = CAGDOMSUPP_Meat(key) - Demand
                        DemandsToIncrease = CFMDEM_Meat(key) + CFEDDEM_Meat(key) + CINDEM_Meat(key) + CAGLOSSTRANS_Meat(key)
                        CFMDEM_Meat(key) = CFMDEM_Meat(key) + increase * (CFMDEM_Meat(key) / DemandsToIncrease)
                        CFEDDEM_Meat(key) = CFEDDEM_Meat(key) + increase * (CFEDDEM_Meat(key) / DemandsToIncrease)
                        'If CountryName(Key) = "United States of America" Then Stop
                        CINDEM_Meat(key) = CINDEM_Meat(key) + increase * (CINDEM_Meat(key) / DemandsToIncrease)
                        CAGLOSSTRANS_Meat(key) = CAGLOSSTRANS_Meat(key) + (increase * CAGLOSSTRANS_Meat(key) / DemandsToIncrease)
                        Demand = CFDEM_Meat(key) + CFMDEM_Meat(key) + CFEDDEM_Meat(key) +
                    CINDEM_Meat(key) + CAGLOSSTRANS_Meat(key)
                    End If
                    'Fish
                    Demand = CFDEM_Fish(key) + CFMDEM_Fish(key) + CFEDDEM_Fish(key) +
                        CINDEM_Fish(key) + CAGLOSSTRANS_Fish(key)
                    If CAGDOMSUPP_Fish(key) <= Demand Then
                        'share between catch and aquaculture
                        FishProdIncrease = (Demand - CAGDOMSUPP_Fish(key))
                        CatchShare = CAGFISHCATCH(key) / (CAGFISHCATCH(key) + CAQUACUL(key))
                        CAGFISHCATCH(key) = CAGFISHCATCH(key) + FishProdIncrease * CatchShare
                        CAQUACUL(key) = CAQUACUL(key) + FishProdIncrease * (1 - CatchShare)
                        CAGDOMSUPP_Fish(key) = Demand
                    Else
                        increase = CAGDOMSUPP_Fish(key) - Demand
                        DemandsToIncrease = CFMDEM_Fish(key) + CFEDDEM_Fish(key) + CINDEM_Fish(key) + CAGLOSSTRANS_Fish(key)
                        CFMDEM_Fish(key) = CFMDEM_Fish(key) + increase * (CFMDEM_Fish(key) / DemandsToIncrease)
                        CFEDDEM_Fish(key) = CFEDDEM_Fish(key) + increase * (CFEDDEM_Fish(key) / DemandsToIncrease)
                        'If CountryName(Key) = "United States of America" Then Stop
                        CINDEM_Fish(key) = CINDEM_Fish(key) + increase * (CINDEM_Fish(key) / DemandsToIncrease)
                        'CAGLOSSTRANS_Fish(key) = CAGLOSSTRANS_Fish(key) + (increase * CAGLOSSTRANS_Fish(key) / DemandsToIncrease)
                        Demand = CFDEM_Fish(key) + CFMDEM_Fish(key) + CFEDDEM_Fish(key) +
                    CINDEM_Fish(key) + CAGLOSSTRANS_Fish(key)
                    End If
                    'jrs 2026/03/12 This covers New Zealand, and basically 83 countries in version 8.6
                    'it's here because I need LiveHerd
                    'now I'm thinking missmatch with supply side, maybe need to re-do adjustments
                    'If CFEDDEM_Crop(key) < 1.05 Then
                    '    CFEDDEM_Crop(key) = CSng(0.787 + 2.7999 * LiveHerd(key))
                    '    'Crop
                    '    Demand = CFDEM_Crop(key) + CFMDEM_Crop(key) + CFEDDEM_Crop(key) +
                    '  CINDEM_Crop(key) + CAGLOSSTRANS_Crop(key)
                    '    If CAGDOMSUPP_Crop(key) <= Demand Then
                    '        'dsr 2015/12/22 When adjust crop production, also adjust cropland to keep yield in check *** Start ***
                    '        oldAgptot = AGPTot(key)
                    '        AGPTot(key) = AGPTot(key) + (Demand - CAGDOMSUPP_Crop(key))
                    '        LDCrop(key) = LDCrop(key) * (AGPTot(key) / oldAgptot)
                    '        'dsr 2015/12/22 Adjust other land types to keep sum to total land
                    '        adjfactforland = (CLandArea(key) - LDCrop(key)) / (LDGraz(key) + LDFor(key) + LDOth(key) + LDUrb(key))
                    '        LDGraz(key) = CSng(LDGraz(key) * adjfactforland)
                    '        LDFor(key) = CSng(LDFor(key) * adjfactforland)
                    '        LDOth(key) = CSng(LDOth(key) * adjfactforland)
                    '        LDUrb(key) = CSng(LDUrb(key) * adjfactforland)
                    '        'dsr 2015/12/11 When adjust crop production, also adjust cropland to keep yield in check *** End ***
                    '        CAGDOMSUPP_Crop(key) = Demand
                    '    Else
                    '        increase = CAGDOMSUPP_Crop(key) - Demand
                    '        DemandsToIncrease = CFMDEM_Crop(key) + CFEDDEM_Crop(key) + CINDEM_Crop(key) + CAGLOSSTRANS_Crop(key)
                    '        CFMDEM_Crop(key) = CFMDEM_Crop(key) + increase * (CFMDEM_Crop(key) / DemandsToIncrease)
                    '        CFEDDEM_Crop(key) = CFEDDEM_Crop(key) + increase * (CFEDDEM_Crop(key) / DemandsToIncrease)
                    '        'If CountryName(Key) = "United States of America" Then Stop
                    '        CINDEM_Crop(key) = CINDEM_Crop(key) + increase * (CINDEM_Crop(key) / DemandsToIncrease)
                    '        CAGLOSSTRANS_Crop(key) = CAGLOSSTRANS_Crop(key) + (increase * CAGLOSSTRANS_Crop(key) / DemandsToIncrease)
                    '    End If
                    'End If
                Next
                ' dsr 2015/11/05 COMPARE DOMESTIC SUPPLY TO SUM OF DEMANDS SECOND TIME for CROPS AND ALSO NOW FOR MEAT AND FISH - *** end ***

                '--------------------------------------------------------------------------------------------------------------------
                Dim yearsAGM As Short()
                yearsAGM = (From y In cntx.GetYears("SeriesAGCropImportQuantityFAOTrade")
                            Where cntx.BaseYear - 20S < y
                            Order By y Descending
                    ).Take(10).ToArray
                Dim diceAGM As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCropImportQuantityFAOTrade", yearsAGM)

                Dim yearsAGM2 As Short()
                yearsAGM2 = (From y In cntx.GetYears("SeriesAGCropExportQuantityFAOTrade")
                             Where cntx.BaseYear - 20S < y
                             Order By y Descending
                    ).Take(10).ToArray
                Dim diceAGM2 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCropExportQuantityFAOTrade", yearsAGM2)

                Dim yearsAGM3 As Short()
                yearsAGM3 = (From y In cntx.GetYears("SeriesAgCropImportValueGDPFAOTrade")
                             Where cntx.BaseYear - 20S < y
                             Order By y Descending
                    ).Take(10).ToArray
                Dim diceAGM3 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAgCropImportValueGDPFAOTrade", yearsAGM3)

                Dim yearsAGM4 As Short()
                yearsAGM4 = (From y In cntx.GetYears("SeriesAgCropExportValueGDPFAOTrade")
                             Where cntx.BaseYear - 20S < y
                             Order By y Descending
                    ).Take(10).ToArray
                Dim diceAGM4 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAgCropExportValueGDPFAOTrade", yearsAGM4)

                Dim yearsAGM5 As Short()
                yearsAGM5 = (From y In cntx.GetYears("SeriesAGMeatImportQuantityFAOTrade")
                             Where cntx.BaseYear - 20S < y
                             Order By y Descending
                    ).Take(10).ToArray
                Dim diceAGM5 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeatImportQuantityFAOTrade", yearsAGM5)

                Dim yearsAGM6 As Short()
                yearsAGM6 = (From y In cntx.GetYears("SeriesAGMeatExportQuantityFAOTrade")
                             Where cntx.BaseYear - 20S < y
                             Order By y Descending
                    ).Take(10).ToArray
                Dim diceAGM6 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGMeatExportQuantityFAOTrade", yearsAGM6)

                Dim yearsAGM7 As Short()
                yearsAGM7 = (From y In cntx.GetYears("SeriesAgMeatImportValueGDPFAOTrade")
                             Where cntx.BaseYear - 20S < y
                             Order By y Descending
                    ).Take(10).ToArray
                Dim diceAGM7 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAgMeatImportValueGDPFAOTrade", yearsAGM7)

                Dim yearsAGM8 As Short()
                yearsAGM8 = (From y In cntx.GetYears("SeriesAgMeatExportValueGDPFAOTrade")
                             Where cntx.BaseYear - 20S < y
                             Order By y Descending
                    ).Take(10).ToArray
                Dim diceAGM8 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAgMeatExportValueGDPFAOTrade", yearsAGM8)

                Dim yearsAGM9 As Short()
                yearsAGM9 = (From y In cntx.GetYears("SeriesAgFishImportQuantityFAOTrade")
                             Where cntx.BaseYear - 20S < y
                             Order By y Descending
                    ).Take(10).ToArray
                Dim diceAGM9 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAgFishImportQuantityFAOTrade", yearsAGM9)

                Dim yearsAGM10 As Short()
                yearsAGM10 = (From y In cntx.GetYears("SeriesAgFishExportQuantityFAOTrade")
                              Where cntx.BaseYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                Dim diceAGM10 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAgFishExportQuantityFAOTrade", yearsAGM10)

                Dim yearsAGM11 As Short()
                yearsAGM11 = (From y In cntx.GetYears("SeriesAgFishImportValueGDPFAOTrade")
                              Where cntx.BaseYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                Dim diceAGM11 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAgFishImportValueGDPFAOTrade", yearsAGM11)

                Dim yearsAGM12 As Short()
                yearsAGM12 = (From y In cntx.GetYears("SeriesAgFishExportValueGDPFAOTrade")
                              Where cntx.BaseYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                Dim diceAGM12 As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAgFishExportValueGDPFAOTrade", yearsAGM12)
                'jrs 2023/08/02 changed ex and imp to use %GDP
                For Each item In (From s In cntx.GetSlice("SeriesAGCropImportQuantityFAOTrade", cntx.FirstYear)
                                  Join s2 In cntx.GetSlice("SeriesAGCropExportQuantityFAOTrade", cntx.FirstYear) On s.Key Equals s2.Key
                                  Join s3 In cntx.GetSlice("SeriesAgCropImportValueGDPFAOTrade", cntx.FirstYear) On s.Key Equals s3.Key
                                  Join s4 In cntx.GetSlice("SeriesAgCropExportValueGDPFAOTrade", cntx.FirstYear) On s.Key Equals s4.Key
                                  Join s5 In cntx.GetSlice("SeriesAGMeatImportQuantityFAOTrade", cntx.FirstYear) On s.Key Equals s5.Key
                                  Join s6 In cntx.GetSlice("SeriesAGMeatExportQuantityFAOTrade", cntx.FirstYear) On s.Key Equals s6.Key
                                  Join s7 In cntx.GetSlice("SeriesAgMeatImportValueGDPFAOTrade", cntx.FirstYear) On s.Key Equals s7.Key
                                  Join s8 In cntx.GetSlice("SeriesAgMeatExportValueGDPFAOTrade", cntx.FirstYear) On s.Key Equals s8.Key
                                  Join s9 In cntx.GetSlice("SeriesAgFishImportQuantityFAOTrade", cntx.FirstYear) On s.Key Equals s9.Key
                                  Join s10 In cntx.GetSlice("SeriesAgFishExportQuantityFAOTrade", cntx.FirstYear) On s.Key Equals s10.Key
                                  Join s11 In cntx.GetSlice("SeriesAgFishImportValueGDPFAOTrade", cntx.FirstYear) On s.Key Equals s11.Key
                                  Join s12 In cntx.GetSlice("SeriesAgFishExportValueGDPFAOTrade", cntx.FirstYear) On s.Key Equals s12.Key
                                  Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value,
                                  Val5 = s5.Value, Val6 = s6.Value, Val7 = s7.Value, Val8 = s8.Value, Val9 = s9.Value,
                                  Val10 = s10.Value, Val11 = s11.Value, Val12 = s12.Value)
                    AGMCropQuantTrade.Add(item.Key, AMAX(If(item.Value, estimateWLineFitXyrs(diceAGM, cntx.BaseYear, yearsAGM, item.Key)), 0))
                    AGXCropQuantTrade.Add(item.Key, AMAX(If(item.Val2, estimateWLineFitXyrs(diceAGM2, cntx.BaseYear, yearsAGM2, item.Key)), 0))
                    AGMCropValueTrade.Add(item.Key, AMAX(If(item.Val3, estimateWLineFitXyrs(diceAGM3, cntx.BaseYear, yearsAGM3, item.Key)) * 1000, 0))
                    'If item.Key = "Sudan South" Then Stop 'jrs 2020/03/09 I was unable to get 3 decimals on this next line
                    AGXCropValueTrade.Add(item.Key, AMAX(If(item.Val4, estimateWLineFitXyrs(diceAGM4, cntx.BaseYear, yearsAGM4, item.Key)) * 1000, 0))
                    AGMMeatQuantTrade.Add(item.Key, AMAX(If(item.Val5, estimateWLineFitXyrs(diceAGM5, cntx.BaseYear, yearsAGM5, item.Key)), 0))
                    AGXMeatQuantTrade.Add(item.Key, AMAX(If(item.Val6, estimateWLineFitXyrs(diceAGM6, cntx.BaseYear, yearsAGM6, item.Key)), 0))
                    AGMMeatValueTrade.Add(item.Key, AMAX(If(item.Val7, estimateWLineFitXyrs(diceAGM7, cntx.BaseYear, yearsAGM7, item.Key)) * 1000, 0))
                    AGXMeatValueTrade.Add(item.Key, AMAX(If(item.Val8, estimateWLineFitXyrs(diceAGM8, cntx.BaseYear, yearsAGM8, item.Key)) * 1000, 0))
                    AGMFishQuantTrade.Add(item.Key, AMAX(If(item.Val9, estimateWLineFitXyrs(diceAGM9, cntx.BaseYear, yearsAGM9, item.Key)), 0))
                    AGXFishQuantTrade.Add(item.Key, AMAX(If(item.Val10, estimateWLineFitXyrs(diceAGM10, cntx.BaseYear, yearsAGM10, item.Key)), 0))
                    AGMFishValueTrade.Add(item.Key, AMAX(If(item.Val11, estimateWLineFitXyrs(diceAGM11, cntx.BaseYear, yearsAGM11, item.Key)) * 1000, 0))
                    AGXFishValueTrade.Add(item.Key, AMAX(If(item.Val12, estimateWLineFitXyrs(diceAGM12, cntx.BaseYear, yearsAGM12, item.Key)) * 1000, 0))
                    'jrs 2023/08/02
                    AGMCropValueTrade(item.Key) = AGMCropValueTrade(item.Key) * CGDP(item.Key) / 100
                    AGXCropValueTrade(item.Key) = AGXCropValueTrade(item.Key) * CGDP(item.Key) / 100
                    AGMMeatValueTrade(item.Key) = AGMMeatValueTrade(item.Key) * CGDP(item.Key) / 100
                    AGXMeatValueTrade(item.Key) = AGXMeatValueTrade(item.Key) * CGDP(item.Key) / 100
                    AGMFishValueTrade(item.Key) = AGMFishValueTrade(item.Key) * CGDP(item.Key) / 100
                    AGXFishValueTrade(item.Key) = AGXFishValueTrade(item.Key) * CGDP(item.Key) / 100
                Next

                'FILL HOLES
                'If there are no trade data in quantity terms but there is in value terms then use value data and global
                'prices to fill holes in quantity data. If there are neither quantity nor value data use quantity data
                'from FBS and then global prices to fill holes in value data

                'World average prices of crops, meat, and fish imports and exports (in what year?):
                'crop imports: $928 / tonne
                'crop exports: $875 / tonne
                'meat imports: $3032 / tonne
                'meat exports: $2916 / tonne
                'fish imports: $2618 / tonne
                For Each key In cntx.Regionalization.Keys
                    If Not AGMCropQuantTrade(key).HasValue Then
                        If AGMCropValueTrade(key).HasValue Then
                            AGMCropQuantTrade(key) = AGMCropValueTrade(key) / 928
                        Else
                            AGMCropQuantTrade(key) = AGMCrop(key) * 1000000 'AGMCrop is in million metric tons
                            AGMCropValueTrade(key) = AGMCropQuantTrade(key) * 928
                        End If
                    End If
                    If Not AGXCropQuantTrade(key).HasValue Then
                        If AGXCropValueTrade(key).HasValue Then
                            AGXCropQuantTrade(key) = AGXCropValueTrade(key) / 875
                        Else
                            AGXCropQuantTrade(key) = AGXCrop(key) * 1000000 'AGXCrop is in million metric tons
                            AGXCropValueTrade(key) = AGXCropQuantTrade(key) * 875
                        End If
                    End If
                    If Not AGMMeatQuantTrade(key).HasValue Then
                        If AGMMeatValueTrade(key).HasValue Then
                            AGMMeatQuantTrade(key) = AGMMeatValueTrade(key) / 3032
                        Else
                            AGMMeatQuantTrade(key) = AGMMeat(key) * 1000000 'AGMMeat is in million metric tons
                            AGMMeatValueTrade(key) = AGMMeatQuantTrade(key) * 3032
                        End If
                    End If
                    If Not AGXMeatQuantTrade(key).HasValue Then
                        If AGXMeatValueTrade(key).HasValue Then
                            AGXMeatQuantTrade(key) = AGXMeatValueTrade(key) / 2916
                        Else
                            AGXMeatQuantTrade(key) = AGXMeat(key) * 1000000 'AGXMeat is in million metric tons
                            AGXMeatValueTrade(key) = AGXMeatQuantTrade(key) * 2916
                        End If
                    End If
                    If Not AGMFishQuantTrade(key).HasValue Then
                        If AGMFishValueTrade(key).HasValue Then
                            AGMFishQuantTrade(key) = AGMFishValueTrade(key) / 2618
                        Else
                            AGMFishQuantTrade(key) = AGMFish(key) * 1000000 'AGMFish is in million metric tons
                            AGMFishValueTrade(key) = AGMFishQuantTrade(key) * 2618
                        End If
                    End If
                    If Not AGXFishQuantTrade(key).HasValue Then
                        If AGXFishValueTrade(key).HasValue Then
                            AGXFishQuantTrade(key) = AGXFishValueTrade(key) / 2984
                        Else
                            AGXFishQuantTrade(key) = AGXFish(key) * 1000000 'AGXFish is in million metric tons
                            AGXFishValueTrade(key) = AGXFishQuantTrade(key) * 2984
                        End If
                    End If

                    'If any trade data in value terms are missing fill holes using quantity data. At this point there should
                    'be no holes in quantity data.
                    If Not AGMCropValueTrade(key).HasValue Then AGMCropValueTrade(key) = AGMCropQuantTrade(key) * 928
                    If Not AGXCropValueTrade(key).HasValue Then AGXCropValueTrade(key) = AGXCropQuantTrade(key) * 875
                    If Not AGMMeatValueTrade(key).HasValue Then AGMMeatValueTrade(key) = AGMMeatQuantTrade(key) * 3032
                    If Not AGXMeatValueTrade(key).HasValue Then AGXMeatValueTrade(key) = AGXMeatQuantTrade(key) * 2916
                    If Not AGMFishValueTrade(key).HasValue Then AGMFishValueTrade(key) = AGMFishQuantTrade(key) * 2618
                    If Not AGXFishValueTrade(key).HasValue Then AGXFishValueTrade(key) = AGXFishQuantTrade(key) * 2984

                    'jrs 2018/12/14 Avoid division by 0s *** begin ***
                    'MAKE SURE PRICES ARE IN REASONABLE RANGES
                    'crop $200 - $6000 per ton
                    If AGMCropValueTrade(key) < 200 * AGMCropQuantTrade(key) Then
                        AGMCropValueTrade(key) = AGMCropQuantTrade(key) * 200
                    End If
                    If AGMCropValueTrade(key) > 6000 * AGMCropQuantTrade(key) Then
                        AGMCropValueTrade(key) = AGMCropQuantTrade(key) * 6000
                    End If
                    If AGXCropValueTrade(key) < 200 * AGXCropQuantTrade(key) Then
                        AGXCropValueTrade(key) = AGXCropQuantTrade(key) * 200
                    End If
                    If AGXCropValueTrade(key) > 6000 * AGXCropQuantTrade(key) Then
                        AGXCropValueTrade(key) = AGXCropQuantTrade(key) * 6000
                    End If
                    'meat $1000 - $6000 per ton
                    If AGMMeatValueTrade(key) < 1000 * AGMMeatQuantTrade(key) Then
                        AGMMeatValueTrade(key) = AGMMeatQuantTrade(key) * 1000
                    End If
                    If AGMMeatValueTrade(key) > 6000 * AGMMeatQuantTrade(key) Then
                        AGMMeatValueTrade(key) = AGMMeatQuantTrade(key) * 6000
                    End If
                    If AGXMeatValueTrade(key) < 1000 * AGXMeatQuantTrade(key) Then
                        AGXMeatValueTrade(key) = AGXMeatQuantTrade(key) * 1000
                    End If
                    If AGXMeatValueTrade(key) > 6000 * AGXMeatQuantTrade(key) Then
                        AGXMeatValueTrade(key) = AGXMeatQuantTrade(key) * 6000
                    End If
                    'Fish $900 - $11000 per ton
                    If AGMFishValueTrade(key) < 900 * AGMFishQuantTrade(key) Then
                        AGMFishValueTrade(key) = AGMFishQuantTrade(key) * 900
                    End If
                    If AGMFishValueTrade(key) > 11000 * AGMFishQuantTrade(key) Then
                        AGMFishValueTrade(key) = AGMFishQuantTrade(key) * 11000
                    End If
                    If AGXFishValueTrade(key) < 900 * AGXFishQuantTrade(key) Then
                        AGXFishValueTrade(key) = AGXFishQuantTrade(key) * 900
                    End If
                    If AGXFishValueTrade(key) > 11000 * AGXFishQuantTrade(key) Then
                        AGXFishValueTrade(key) = AGXFishQuantTrade(key) * 11000
                    End If

                    'ADJUST TRADE VALUES TO BE IN LINE WITH IMPORTS AND EXPORTS FROM FBS
                    'note that this results in values in billion $US, which is what we want, since
                    'we need to multiply AGMCrop (etc.) by 1,000,000 to be in same units as AGMCropQuantTrade
                    'and divide AGMCropValueTrade by 1,000,000,000 to go from $US to billion $US
                    'bbh 2020/07/01 start
                    'For New Zealand AGXMEAT was about 20x AGXMeatQuantTrade and the jump in value was making NZ total exports way too big
                    ' Upward adjustments like that are more problematic than downward ones, so decided to bound at 1.5 times
                    Dim MulAdjust As Single, MaxValMul As Single
                    MaxValMul = 1.5 / 1000000 'ratio of quant terms is million metric tons/tones
                    MulAdjust = CSng(Amin(MaxValMul, AGMCrop(key) / AMAX(AGMCropQuantTrade(key), 0.0001)))
                    AGMCropValueTrade(key) = AGMCropValueTrade(key) * MulAdjust / 1000
                    MulAdjust = CSng(Amin(MaxValMul, AGXCrop(key) / AMAX(AGXCropQuantTrade(key), 0.0001)))
                    AGXCropValueTrade(key) = AGXCropValueTrade(key) * MulAdjust / 1000
                    MulAdjust = CSng(Amin(MaxValMul, AGMMeat(key) / AMAX(AGMMeatQuantTrade(key), 0.0001)))
                    AGMMeatValueTrade(key) = AGMMeatValueTrade(key) * MulAdjust / 1000
                    MulAdjust = CSng(Amin(MaxValMul, AGXMeat(key) / AMAX(AGXMeatQuantTrade(key), 0.0001)))
                    AGXMeatValueTrade(key) = AGXMeatValueTrade(key) * MulAdjust / 1000
                    MulAdjust = CSng(Amin(MaxValMul, AGMFish(key) / AMAX(AGMFishQuantTrade(key), 0.0001)))
                    AGMFishValueTrade(key) = AGMFishValueTrade(key) * MulAdjust / 1000
                    MulAdjust = CSng(Amin(MaxValMul, AGXFish(key) / AMAX(AGXFishQuantTrade(key), 0.0001)))
                    AGXFishValueTrade(key) = AGXFishValueTrade(key) * MulAdjust / 1000

                    'AGMCropValueTrade(key) = CSng((AGMCropValueTrade(key) * AGMCrop(key) / AMAX(AGMCropQuantTrade(key), 0.0001)) / 1000)
                    ''If key = "Cuba" Then Stop
                    'AGXCropValueTrade(key) = CSng((AGXCropValueTrade(key) * AGXCrop(key) / AMAX(AGXCropQuantTrade(key), 0.0001)) / 1000)
                    'AGMMeatValueTrade(key) = CSng((AGMMeatValueTrade(key) * AGMMeat(key) / AMAX(AGMMeatQuantTrade(key), 0.0001)) / 1000)
                    'AGXMeatValueTrade(key) = CSng((AGXMeatValueTrade(key) * AGXMeat(key) / AMAX(AGXMeatQuantTrade(key), 0.0001)) / 1000)
                    'AGMFishValueTrade(key) = CSng((AGMFishValueTrade(key) * AGMFish(key) / AMAX(AGMFishQuantTrade(key), 0.0001)) / 1000)
                    'AGXFishValueTrade(key) = CSng((AGXFishValueTrade(key) * AGXFish(key) / AMAX(AGXFishQuantTrade(key), 0.0001)) / 1000)
                    'bbh 2020/07/01 end
                    'jrs 2018/12/14 Avoid division by 0s *** end ***
                Next
                'dsr 2015/07/24 BRING IN STEVE'S CODE TO READ IN DATA FROM TRADE DOMAIN - *** End ***

                'dsr 2015/01/15 END of SUPPLY SIDE - *** End ***

                'jrs 2015/11/16 *** begin ***
                'Global exports of type should be equal to global imports of the same type
                'SUM(AGXVALUES)(CROP) = SUM(AGMValues)(CROP)
                Dim XcropTot As Single?, McropTot As Single?
                XcropTot = 0
                McropTot = 0
                Dim XmeatTot As Single?, MmeatTot As Single?
                XmeatTot = 0
                MmeatTot = 0
                Dim XfishTot As Single?, MfishTot As Single?
                XfishTot = 0
                MfishTot = 0
                For Each key In cntx.Regionalization.Keys
                    'Crop
                    XcropTot = XcropTot + If(AGXCropValueTrade(key), 0)
                    McropTot = McropTot + If(AGMCropValueTrade(key), 0)
                    'Meat
                    XmeatTot = XmeatTot + If(AGXMeatValueTrade(key), 0)
                    MmeatTot = MmeatTot + If(AGMMeatValueTrade(key), 0)
                    'Fish
                    XfishTot = XfishTot + If(AGXFishValueTrade(key), 0)
                    MfishTot = MfishTot + If(AGMFishValueTrade(key), 0)
                Next
                'Average factor
                Dim cropFact As Single?
                cropFact = (XcropTot + McropTot) / 2
                Dim meatFact As Single?
                meatFact = (XmeatTot + MmeatTot) / 2
                Dim fishFact As Single?
                fishFact = (XfishTot + MfishTot) / 2
                For Each key In cntx.Regionalization.Keys
                    'Crop
                    AGXCropValueTrade(key) = AGXCropValueTrade(key) * cropFact / XcropTot
                    AGMCropValueTrade(key) = AGMCropValueTrade(key) * cropFact / McropTot
                    'Meat
                    AGXMeatValueTrade(key) = AGXMeatValueTrade(key) * meatFact / XmeatTot
                    AGMMeatValueTrade(key) = AGMMeatValueTrade(key) * meatFact / MmeatTot
                    'Fish
                    AGXFishValueTrade(key) = AGXFishValueTrade(key) * fishFact / XfishTot
                    AGMFishValueTrade(key) = AGMFishValueTrade(key) * fishFact / MfishTot
                Next
                'jrs 2015/11/16 *** end ***

                '   WATER RESOURCES - *** start ***
                '   ---------------
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriesTable As String = "SeriesWaterAnRenResources"
                    Dim yrs As Short() = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    Dim DictTbl As IDictionary(Of String, Single?()) = cntx.GetDice(SeriesTable, yrs)
                    For Each item In (From s In cntx.GetSlice("SeriesWaterAnRenResourcesOld", "1977-2001")
                                      Select Key = s.Key, Value = s.Value)
                        ValHold = getClosestYrVal(DictTbl, yrs, item.Key, cntx.FirstYear, HistDist)
                        CWATRES.Add(item.Key, If(ValHold, item.Value))
                    Next
                    SeriesTable = "SeriesWaterAnWithdrawals"
                    yrs = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    DictTbl = cntx.GetDice(SeriesTable, yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DictTbl, yrs, key, cntx.FirstYear, HistDist)
                        CWATUSEPC.Add(key, ValHold)
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else 'Forecast *********
                    For Each item In (From s In cntx.GetSlice("SeriesWaterAnRenResources", "MostRecent")
                                      Join s2 In cntx.GetSlice("SeriesWaterAnRenResourcesOld", "1977-2001") On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesWaterAnWithdrawals", 2000) On s.Key Equals s3.Key
                                      Select Key = s.Key, Value = If(s.Value, s2.Value), Val3 = s3.Value)
                        CWATRES.Add(item.Key, item.Value)
                        CWATUSEPC.Add(item.Key, item.Val3)
                    Next
                End If 'End of if history else forecast

                Dim EstimateWAT As SeriesEstimator = cntx.GetRegression("Crop Production/Capita Versus Water Use/Capita").ToEstimator(AGPTot)
                For Each key In cntx.Regionalization.Keys
                    If Not CWATUSEPC(key).HasValue Then
                        CWATUSEPC(key) = EstimateWAT(key)
                    Else
                        CWATUSEPC(key) = CWATUSEPC(key) / CPOP(key)
                    End If
                    If Not CWATRES(key).HasValue Then CWATRES(key) = 10 * CWATUSEPC(key) * CPOP(key)
                    If CWATRES(key) < 0.01 Then CWATRES(key) = 0.01
                Next
                '   WATER RESOURCES - *** end ***

                '   CROP and CROPLAND GROWTH RATES - *** start ***
                'dsr 2014/12/22 COMPUTE CROP GROWTH RATE - *** Start ***

                'sgh 2013/9/2 *** begin ***
                'The ag production growth rates are initialized using a compound annual growth rate that is
                'calculated from historical data. The growth rate calculation requires a starting year, an ending year,
                'and the interval between them. In this code, the ending year is calculated using the
                'getYearOrMstRct function. Thus, if the "EndingYear" initialization is later than the most recent
                'year, then the code will take the data from the "Most Recent" column in the IFsHistSeries.
                'If you would like to use an ending year earlier than the most recent year, you can change
                'the initialization below. Likewise, if you would like to use a different interval than 18 years
                'you may change the interval initialization below.
                Dim yearsCrop As Short()
                yearsCrop = cntx.GetYears("SeriesAGCropProductionFAO").OrderByDescending(Function(y) y).ToArray()
                Dim yearsCrop10 As Short()
                yearsCrop10 = yearsCrop.Take(10).ToArray

                Dim diceCrop As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesAGCropProductionFAO", yearsCrop)

                Dim yearsCrop2 As Short()
                yearsCrop2 = (From y In cntx.GetYears("LD_Crop")
                              Where cntx.FirstYear - 20S < y
                              Order By y Descending
                    ).Take(10).ToArray
                Dim diceCrop2 As IDictionary(Of String, Single?()) = cntx.GetDice("LD_Crop", yearsCrop2)

                Dim AgpGREndingYear As Short, AgpGRInterval As Short
                AgpGREndingYear = cntx.BaseYear  'Set to 2100 so it will use most recent 'dsr 2014/12/21 changed to BaseYear
                AgpGRInterval = 18    'Set to compound growth rate over 18 years
                Dim AgpCropsEndYr As Single?, AgpCropsBeginYr As Single?, AgpTotEndYr As Single?, AgpTotBeginYr As Single?
                Dim AgpGRCropsMostRecentYear As Short
                AgpGRCropsMostRecentYear = yearsCrop(0)

                Dim AgpGRCropsGap As Short
                If AgpGREndingYear < AgpGRCropsMostRecentYear Then
                    AgpGRCropsGap = AgpGRCropsMostRecentYear - AgpGREndingYear
                Else
                    AgpGRCropsGap = 0
                End If

                Dim A As Double, B As Double, LDCropBY As Double?, LDCropBYminus10 As Double?, LDRAT As Single?, MaxGrow As Single?

                For Each item In (From s In cntx.GetSlice("SeriesAGCropProductionFAO", AgpGREndingYear)
                                  Join c In cntx.GetSlice("LD_Crop", cntx.FirstYear) On s.Key Equals c.Key
                                  Select Key = s.Key, Value = s.Value, Land = c.Value)
                    AgpCropsEndYr = If(item.Value, estimateWLineFitXyrs(diceCrop, AgpGREndingYear, yearsCrop10, item.Key))

                    'jrs 2015/12/23
                    If yearsCrop.Count > AgpGRInterval + AgpGRCropsGap Then
                        AgpCropsBeginYr = diceCrop(item.Key)(AgpGRInterval + AgpGRCropsGap)
                    Else
                        AgpCropsBeginYr = diceCrop(item.Key)(yearsCrop.Count - 1)
                    End If

                    AgpTotEndYr = 0
                    AgpTotBeginYr = 0

                    If Not (Not AgpCropsBeginYr.HasValue And Not AgpCropsEndYr.HasValue) Then
                        AgpTotEndYr = AgpTotEndYr + AgpCropsEndYr
                        AgpTotBeginYr = AgpTotBeginYr + AgpCropsBeginYr
                    End If
                    If AgpTotEndYr > 0.001 And AgpTotBeginYr > 0.001 Then
                        CAGPGR.Add(item.Key, CSng((AgpTotEndYr / AMAX(0.001, AgpTotBeginYr)) ^ (1 / AgpGRInterval) - 1))
                    Else
                        CAGPGR.Add(item.Key, CSng(0.027 - 0.015 * Amin(1, CGDPPC(item.Key) / 30)))
                        'Historic LDC growth was 2.7%, Historic OECD was 1.2%
                        'Could tie to pop growth rates; about rate + 1%
                    End If

                    CAGPGR(item.Key) = CSng(Amin(0.035, AMAX(-0.001, CAGPGR(item.Key))))

                    'dsr 2015/01/19 SPECIFY SHARE OF AGRICULTURAL CAPITAL GOING TO LAND - *** start ***
                    'Assume this is 25%. Not sure why.
                    CIALK.Add(item.Key, 0.25)  'Perhaps make formula instead of constant
                    'dsr 2015/01/19 SPECIFY SHARE OF AGRICULTURAL CAPITAL GOING TO LAND - *** end **

                    'jrs 2017/05/01 *** begin ***
                    Call LinReg(diceCrop2, yearsCrop2, item.Key, A, B)
                    If A <> -9999 And B <> -9999 Then
                        LDCropBY = AMAX(A + B * cntx.BaseYear, CDbl(0))  'jrs 2019/07/16 added limit to avoid negatives
                        LDCropBYminus10 = AMAX(A + B * (cntx.BaseYear - 10), CDbl(0))  'jrs 2019/07/16 added limit to avoid negatives
                    Else
                        LDCropBY = Nothing
                        LDCropBYminus10 = Nothing
                    End If
                    If Not LDCropBY.HasValue Or Not LDCropBYminus10.HasValue Then
                        LDRAT = LDCrop(item.Key) / LDTot
                        MaxGrow = CSng(0.015 - 0.01 * Amin(1, CSng(LDRAT / 0.2)))
                        'If Cropped = 20%, then very limited growth potential
                        CTGRLD.Add(item.Key, CSng(0.009 - 0.011 * Amin(1, CGDPPC(item.Key) / 30)))
                        'Historic LDC growth was .9%, Historic OECD was -.2%
                        CTGRLD(item.Key) = Amin(MaxGrow, CTGRLD(item.Key))
                    Else
                        'jrs 2017/05/01 CTGRLD(item.key) = (LDCrop2011 / AMAX(0.001, LDCrop2002)) ^ (1 / 9) - 1
                        CTGRLD.Add(item.Key, CSng((LDCropBY / AMAX(0.001, LDCropBYminus10)) ^ (1 / 10) - 1))
                    End If
                    'wv 2021/03/08 update amin to 0.01 instead of 0.02. especially to limit growth of land in china
                    CTGRLD(item.Key) = CSng(Amin(0.01, AMAX(-0.003, CTGRLD(item.Key))))
                    'bbh 2017/05/01 end
                    CTGRYL.Add(item.Key, CAGPGR(item.Key) - CTGRLD(item.Key))
                    CTGRYL(item.Key) = CSng(Amin(0.035, AMAX(0.007, CTGRYL(item.Key))))

                Next

                cntx.Store("CLandArea", CLandArea)
                cntx.Store("LDCrop", LDCrop)
                cntx.Store("AGMCropValueTrade", AGMCropValueTrade)
                cntx.Store("AGXCropValueTrade", AGXCropValueTrade)
                cntx.Store("AGMMeatValueTrade", AGMMeatValueTrade)
                cntx.Store("AGXMeatValueTrade", AGXMeatValueTrade)
                cntx.Store("AGMFishValueTrade", AGMFishValueTrade)
                cntx.Store("AGXFishValueTrade", AGXFishValueTrade)
                cntx.Store("CCLPC_Tot", CCLPC_Tot)
                cntx.Store("CMALNCHP", CMALNCHP)

                ' mark pre-processed for all three series
                Me.processed = True
            End Sub

            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Me.PreProcess(cntx)
                Dim series As IDictionary(Of String, Single?)
                Dim bAggSUM As Boolean = False
                Dim bApplyLimits As Boolean = False
                Dim MinLimit As Single, MaxLimit As Single
                Select Case seriesName
                    Case "LANDAREA"
                        series = Me.CLandArea
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0
                        MaxLimit = 10000
                    Case "LD_Crop"
                        series = LDCrop
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.001
                        MaxLimit = 10000
                    Case "LD_Grazing"
                        series = Me.LDGraz
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.001
                        MaxLimit = 10000
                    Case "LD_Forest"
                        series = Me.LDFor
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.001
                        MaxLimit = 10000
                    Case "LD_Other"
                        series = Me.LDOth
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.001
                        MaxLimit = 10000
                    Case "LD_Urban"
                        series = Me.LDUrb
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.001
                        MaxLimit = 10000
                    Case "landarablepot"
                        series = Me.CLdArabPot
                        bAggSUM = True
                    Case "landarablepotmed" 'wv 2021/08/12
                        series = Me.CLdArabPotMed
                        bAggSUM = True
                    Case "landarablepotlow" 'wv 2021/08/12
                        series = Me.CLdArabPotLow
                        bAggSUM = True
                    Case "YIELDINT" 'wv 2020/12/11
                        series = Me.CYIELDINT
                    Case "CLPC_Crop"
                        series = Me.CCLPC_Crop
                    Case "CLPC_Meat"
                        series = Me.CCLPC_Meat
                    Case "CLPC_Fish"
                        series = Me.CCLPC_Fish
                    Case "CLPC_Total"
                        series = Me.CCLPC_Tot
                    Case "CLPCCV" 'jrs 2020/12/10
                        series = Me.CCLPCCV
                    Case "CLPCMDER" 'jrs 2020/12/10
                        series = Me.CCLPCMDER
                    Case "PROTEINPC_Crop"
                        series = Me.CPROTEINPC_Crop
                    Case "PROTEINPC_Meat"
                        series = Me.CPROTEINPC_Meat
                    Case "PROTEINPC_Fish"
                        series = Me.CPROTEINPC_Fish
                    Case "PROTEINPC_Total"
                        series = Me.CPROTEINPC_Tot
                    Case "CLAVAL"
                        series = Me.CCLPC_Tot
                    Case "MALNCHP"
                        series = Me.CMALNCHP
                    Case "MALNPOPP"
                        series = Me.CMALNPOPP
                    Case "FDEM_Crop"
                        series = Me.CFDEM_Crop
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "FDEM_Meat"
                        series = Me.CFDEM_Meat
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "FDEM_Fish"
                        series = Me.CFDEM_Fish
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "FMDEM_Crop"
                        series = Me.CFMDEM_Crop
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "FMDEM_Meat"
                        series = Me.CFMDEM_Meat
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "FMDEM_Fish"
                        series = Me.CFMDEM_Fish
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "FEDDEM_Crop"
                        series = Me.CFEDDEM_Crop
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "FEDDEM_Meat"
                        series = Me.CFEDDEM_Meat
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "FEDDEM_Fish"
                        series = Me.CFEDDEM_Fish
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "INDEM_Crop"
                        series = Me.CINDEM_Crop
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "INDEM_Meat"
                        series = Me.CINDEM_Meat
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "INDEM_Fish"
                        series = Me.CINDEM_Fish
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGLOSSTRANS_Crop"
                        series = Me.CAGLOSSTRANS_Crop
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGLOSSTRANS_Meat"
                        series = Me.CAGLOSSTRANS_Meat
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGLOSSTRANS_Fish"
                        series = Me.CAGLOSSTRANS_Fish
                        bAggSUM = True
                    Case "AGP_Crop"
                        series = Me.AGPTot
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGP_Meat"
                        series = Me.LiveHerd
                        bAggSUM = True
                    Case "AGP_Fish"
                        series = Me.CAQUACUL
                        bAggSUM = True
                    Case "AGM_Crop"
                        series = Me.AGMCrop
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGM_Meat"
                        series = Me.AGMMeat
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGM_Fish"
                        series = Me.AGMFish
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGX_Crop"
                        series = Me.AGXCrop
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGX_Meat"
                        series = Me.AGXMeat
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGX_Fish"
                        series = Me.AGXFish
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGMVALUE_Crop"
                        series = Me.AGMCropValueTrade
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGMVALUE_Meat"
                        series = Me.AGMMeatValueTrade
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGMVALUE_Fish"
                        series = Me.AGMFishValueTrade
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGXVALUE_Crop"
                        series = Me.AGXCropValueTrade
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGXVALUE_Meat"
                        series = Me.AGXMeatValueTrade
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "AGXVALUE_Fish"
                        series = Me.AGXFishValueTrade
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.000001
                        MaxLimit = 10000
                    Case "IALK"
                        series = Me.CIALK
                        bApplyLimits = True
                        MinLimit = 0
                        MaxLimit = 1
                    Case "tgrld"
                        series = Me.CTGRLD
                        bApplyLimits = True
                        MinLimit = -0.1
                        MaxLimit = 0.2
                    Case "TGRYL"
                        series = Me.CTGRYL
                        bApplyLimits = True
                        MinLimit = -0.1
                        MaxLimit = 0.2
                    Case "LVHERD"
                        series = Me.LiveHerd
                        bAggSUM = True
                    Case "AGPMILKEGGS"
                        series = Me.CAGPMILKEGGS
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0
                        MaxLimit = 10000
                    Case "AGFISHCATCH"
                        series = Me.CAGFISHCATCH
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0
                        MaxLimit = 10000
                    Case "AQUACUL"
                        series = Me.CAQUACUL
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0
                        MaxLimit = 10000
                    Case "watres"
                        series = Me.CWATRES
                        bAggSUM = True
                        bApplyLimits = True
                        MinLimit = 0.01
                        MaxLimit = 100000
                    Case "WATUSEPC"
                        series = Me.CWATUSEPC
                        bApplyLimits = True
                        MinLimit = 0.01
                        MaxLimit = 100000
                    Case Else
                        'Throw New ArgumentException($"Aggregator cannot be applied on series {seriesName}.")
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, Nothing)
                        Next
                End Select

                'wv 2021/08/12 added med and low
                If seriesName = "landarablepot" Or seriesName = "landarablepotmed" Or seriesName = "landarablepotlow" Then
                    Return (From s In series
                            Join r In cntx.Regionalization On s.Key Equals r.Key
                            Join w In Me.CLandArea On s.Key Equals w.Key
                            Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                           ).ToDictionary(
                               Function(grp) grp.RegionId,
                               Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
                               )
                ElseIf seriesName = "CLAVAL" Then
                    Return (From s In series
                            Join r In cntx.Regionalization On s.Key Equals r.Key
                            Join w In Me.CPOP On s.Key Equals w.Key
                            Group gValue = s.Value * w.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(gValue)
                       ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) ApplyLimits(grp.TotalVal, 100, 100000000)
                           )
                ElseIf seriesName = "AGP_Meat" Then
                    Return (From s In series
                            Join r In cntx.Regionalization On s.Key Equals r.Key
                            Join w In Me.CAGPMILKEGGS On s.Key Equals w.Key
                            Group gValue = s.Value * slr + w.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(gValue)
                           ).ToDictionary(
                               Function(grp) grp.RegionId,
                               Function(grp) Amin(grp.TotalVal, 10000)
                               )
                ElseIf seriesName = "AGP_Fish" Then
                    Return (From s In series
                            Join r In cntx.Regionalization On s.Key Equals r.Key
                            Join w In Me.CAGFISHCATCH On s.Key Equals w.Key
                            Group gValue = s.Value + w.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(gValue)
                           ).ToDictionary(
                               Function(grp) grp.RegionId,
                               Function(grp) Amin(grp.TotalVal, 10000)
                               )
                ElseIf seriesName = "YIELDINT" Then
                    Return (From s In series
                            Join r In cntx.Regionalization On s.Key Equals r.Key
                            Join w In Me.CLandArea On s.Key Equals w.Key
                            Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                            ).ToDictionary(
                               Function(grp) grp.RegionId,
                               Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
                               )
                ElseIf bAggSUM Then
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
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In Me.CPOP On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, MinLimit, MaxLimit)
                           )
                    Else
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

            End Function

            'jrs 2022/03/08
            Private Sub LoadCLPCandMALNPOPPdataFromSameYear(sCtry As String)
                If CCLPC_Tot(sCtry).HasValue And Not CCLPC_Crop(sCtry).HasValue And CCLPC_Meat(sCtry).HasValue And CCLPC_Fish(sCtry).HasValue Then
                    CCLPC_Crop(sCtry) = CCLPC_Tot(sCtry) - (CCLPC_Meat(sCtry) + CCLPC_Fish(sCtry))
                End If
                If CCLPC_Tot(sCtry).HasValue And CCLPC_Crop(sCtry).HasValue And Not CCLPC_Meat(sCtry).HasValue And CCLPC_Fish(sCtry).HasValue Then
                    CCLPC_Meat(sCtry) = CCLPC_Tot(sCtry) - (CCLPC_Crop(sCtry) + CCLPC_Fish(sCtry))
                End If
                If CCLPC_Tot(sCtry).HasValue And CCLPC_Crop(sCtry).HasValue And CCLPC_Meat(sCtry).HasValue And Not CCLPC_Fish(sCtry).HasValue Then
                    CCLPC_Fish(sCtry) = CCLPC_Tot(sCtry) - (CCLPC_Crop(sCtry) + CCLPC_Meat(sCtry))
                End If
                If Not CCLPC_Tot(sCtry).HasValue And CCLPC_Crop(sCtry).HasValue And CCLPC_Meat(sCtry).HasValue And CCLPC_Fish(sCtry).HasValue Then
                    CCLPC_Tot(sCtry) = CCLPC_Crop(sCtry) + CCLPC_Meat(sCtry) + CCLPC_Fish(sCtry)
                End If

                Dim A, B, C, Zz, Sigma1, Sigma2 As Single
                'Reset CLPCCV With other values
                '================================
                If Not CCLPCCV(sCtry).HasValue And CCLPCMDER(sCtry).HasValue And CCLPC_Tot(sCtry).HasValue And CMALNPOPP(sCtry).HasValue Then
                    'Sigma = Log((CLPCCV(R%) ^ 2) + 1) ^ 0.5

                    'Zz = (Log(CLPCMDER(R%)) - Mu) / Sigma
                    'Zz = (Log(CLPCMDER(R%)) - Log(CLPC(R%, 4)) - Sigma ^ 2 / 2) / Sigma
                    'Sigma * Zz = Log(CLPCMDER(R%)) - Log(CLPC(R%, 4)) + Sigma ^ 2 / 2
                    '0 = Sigma ^ 2 / 2 - Zz * Sigma + (Log(CLPCMDER(R%)) - Log(CLPC(R%, 4)))

                    'A = 1 / 2
                    A = 1 / 2
                    'B = -Zz
                    'pct = CalcNormalCDF(Zz) * 100 -> we have pct, then can find Zz
                    Zz = CalcNormalInverse(CMALNPOPP(sCtry) / 100)
                    B = -1 * Zz
                    'C = (Log(CLPCMDER(R%)) - Log(CLPC(R%, 4)))
                    C = CSng(Math.Log(CSng(CCLPCMDER(sCtry))) - Math.Log(CSng(CCLPC_Tot(sCtry))))
                    'Sigma = (-b +- Sqrt(b^2 - 4ac))/(2a)
                    'jrs 2021/06/09
                    If B ^ 2 - 4 * A * C >= 0 Then
                        Sigma1 = CSng((-B - Math.Sqrt(B ^ 2 - 4 * A * C)) / (2 * A))
                        Sigma2 = CSng((-B + Math.Sqrt(B ^ 2 - 4 * A * C)) / (2 * A))
                        'CLPCCV(R%) = Sqrt(exp(Sigma^2)-1)
                        If Math.Exp(Sigma1 ^ 2) - 1 > 0 And Math.Exp(Sigma2 ^ 2) - 1 > 0 Then
                            If Math.Sqrt(Math.Exp(Sigma1 ^ 2) - 1) < Math.Sqrt(Math.Exp(Sigma2 ^ 2) - 1) Then
                                CCLPCCV(sCtry) = CSng(Math.Sqrt(Math.Exp(Sigma1 ^ 2) - 1))
                            Else
                                CCLPCCV(sCtry) = CSng(Math.Sqrt(Math.Exp(Sigma2 ^ 2) - 1))
                            End If
                        ElseIf math.Exp(Sigma1 ^ 2) - 1 > 0 Then
                            CCLPCCV(sCtry) = CSng(Math.Sqrt(Math.Exp(Sigma1 ^ 2) - 1))
                        ElseIf math.Exp(Sigma2 ^ 2) - 1 > 0 Then
                            CCLPCCV(sCtry) = CSng(Math.Sqrt(Math.Exp(Sigma2 ^ 2) - 1))
                        End If
                    End If
                End If
                'Reset CLPCMDER With other values
                '================================
                If CCLPCCV(sCtry).HasValue And Not CCLPCMDER(sCtry).HasValue And CCLPC_Tot(sCtry).HasValue And CMALNPOPP(sCtry).HasValue Then
                    'Sigma = Log((CLPCCV(R%) ^ 2) + 1) ^ 0.5

                    'Zz = (Log(CLPCMDER(R%)) - Mu) / Sigma
                    'Zz = (Log(CLPCMDER(R%)) - Log(CLPC(R%, 4)) - Sigma ^ 2 / 2) / Sigma
                    'Sigma * Zz = Log(CLPCMDER(R%)) - Log(CLPC(R%, 4)) + Sigma ^ 2 / 2
                    'Log(CLPCMDER(R%)) = Sigma * Zz + Log(CLPC(R%, 4)) - Sigma ^ 2 / 2
                    'CLPCMDER(R%) = exp(Sigma * Zz + Log(CLPC(R%, 4)) - Sigma ^ 2 / 2)

                    Sigma1 = CSng(Math.Log(CSng((CCLPCCV(sCtry) ^ 2) + 1)) ^ 0.5)

                    'pct = CalcNormalCDF(Zz) * 100 -> we have pct, then can find Zz
                    Zz = CalcNormalInverse(CMALNPOPP(sCtry) / 100)
                    CCLPCMDER(sCtry) = CSng(Math.Exp(Sigma1 * Zz + Math.Log(CSng(CCLPC_Tot(sCtry))) - Sigma1 ^ 2 / 2))
                End If
                'Reset CLPC(Tot) With other values
                '================================
                If CCLPCCV(sCtry).HasValue And CCLPCMDER(sCtry).HasValue And Not CCLPC_Tot(sCtry).HasValue And CMALNPOPP(sCtry).HasValue Then
                    'Sigma = Log((CLPCCV(R%) ^ 2) + 1) ^ 0.5

                    'Zz = (Log(CLPCMDER(R%)) - Mu) / Sigma
                    'Zz = (Log(CLPCMDER(R%)) - Log(CLPC(R%, 4)) - Sigma ^ 2 / 2) / Sigma
                    'Sigma * Zz = Log(CLPCMDER(R%)) - Log(CLPC(R%, 4)) + Sigma ^ 2 / 2
                    'Log(CLPC(R%, 4)) = Log(CLPCMDER(R%)) + Sigma ^ 2 / 2 - Sigma * Zz
                    'CLPC(R%, 4) = exp(Log(CLPCMDER(R%)) + Sigma ^ 2 / 2 - Sigma * Zz)

                    Sigma1 = CSng(Math.Log(CSng((CCLPCCV(sCtry) ^ 2) + 1)) ^ 0.5)

                    'pct = CalcNormalCDF(Zz) * 100 -> we have pct, then can find Zz
                    Zz = CalcNormalInverse(CMALNPOPP(sCtry) / 100)
                    CCLPC_Tot(sCtry) = CSng(Math.Exp(Math.Log(CSng(CCLPCMDER(sCtry))) + Sigma1 ^ 2 / 2 - Sigma1 * Zz))
                End If
                'Reset MALNPOPP  With other values
                '================================
                If CCLPCCV(sCtry).HasValue And CCLPCMDER(sCtry).HasValue And CCLPC_Tot(sCtry).HasValue And Not CMALNPOPP(sCtry).HasValue Then
                    'Sigma = Log((CLPCCV(R%) ^ 2) + 1) ^ 0.5

                    'Zz = (Log(CLPCMDER(R%)) - Mu) / Sigma
                    'Zz = (Log(CLPCMDER(R%)) - Log(CLPC(R%, 4)) - Sigma ^ 2 / 2) / Sigma

                    Sigma1 = CSng(Math.Log(CSng((CCLPCCV(sCtry) ^ 2) + 1)) ^ 0.5)
                    Zz = CSng((Math.Log(CSng(CCLPCMDER(sCtry))) - Math.Log(CSng(CCLPC_Tot(sCtry))) - Sigma1 ^ 2 / 2) / Sigma1)

                    'pct = CalcNormalCDF(Zz) * 100 -> we have pct, then can find Zz
                    CMALNPOPP(sCtry) = CSng(CalcNormalCDF(Zz) * 100)
                    'jrs 2022/07/18
                    If CMALNPOPP2_5LIM(sCtry) Then
                        CMALNPOPP(sCtry) = CSng(Amin(CMALNPOPP(sCtry), 2.5))
                    End If
                End If

            End Sub

            Private Sub GetFishCalorieData(cntx As IRebuildFacade)
                '1: Cephalopods, 2: Crustaceans, 3: Demersal, 4: Freshwater, 5: Marine, 6: Molluscs, 7: Pelagic
                '8: AqAnimals, 9: AqPlants, 10: BodyOil, 11: LiverOil. Note: no human consumption of fish meal.
                Dim ValHold As Single?
                Dim i As Integer
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriTables As New List(Of String)
                    Dim DictTbls As New List(Of IDictionary(Of String, Single?()))
                    Dim yrss As New List(Of Short())
                    SeriTables.Add("SeriesAgFishCalPerCapPerDayCephalopodsFAO")
                    SeriTables.Add("SeriesAgFishCalPerCapPerDayCrustaceansFAO")
                    SeriTables.Add("SeriesAgFishCalPerCapPerDayDemersalFAO")
                    SeriTables.Add("SeriesAgFishCalPerCapPerDayFreshwaterFAO")
                    SeriTables.Add("SeriesAgFishCalPerCapPerDayMarineFAO")
                    SeriTables.Add("SeriesAgFishCalPerCapPerDayMolluscsFAO")
                    SeriTables.Add("SeriesAgFishCalPerCapPerDayPelagicFAO")
                    SeriTables.Add("SeriesAgFishCalPerCapPerDayAqAnimalsFAO")
                    SeriTables.Add("SeriesAgFishCalPerCapPerDayAqPlantsFAO")
                    SeriTables.Add("SeriesAgFishCalPerCapPerDayBodyOilFAO")
                    SeriTables.Add("SeriesAgFishCalPerCapPerDayLiverOilFAO")
                    For i = 0 To SeriTables.Count - 1
                        yrss.Add(cntx.GetYears(SeriTables(i)).OrderBy(Function(y) y).ToArray())
                        DictTbls.Add(cntx.GetDice(SeriTables(i), yrss(i)))
                    Next
                    For Each key In cntx.Regionalization.Keys
                        Dim FishCal = New Dictionary(Of Integer, Single?)
                        For i = 0 To SeriTables.Count - 1
                            ValHold = getClosestYrVal(DictTbls(i), yrss(i), key, cntx.FirstYear, HistDist)
                            FishCal.Add(i + 1, AMAX(If(ValHold.HasValue, ValHold, 0), 0))
                        Next
                        AgFishCalPerCapPerDayFAO.Add(key, FishCal)
                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In cntx.GetSlice("SeriesAgFishCalPerCapPerDayCephalopodsFAO", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("SeriesAgFishCalPerCapPerDayCrustaceansFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAgFishCalPerCapPerDayDemersalFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAgFishCalPerCapPerDayFreshwaterFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAgFishCalPerCapPerDayMarineFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAgFishCalPerCapPerDayMolluscsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAgFishCalPerCapPerDayPelagicFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAgFishCalPerCapPerDayAqAnimalsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s8.Key
                                      Join s9 In cntx.GetSlice("SeriesAgFishCalPerCapPerDayAqPlantsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s9.Key
                                      Join s10 In cntx.GetSlice("SeriesAgFishCalPerCapPerDayBodyOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s10.Key
                                      Join s11 In cntx.GetSlice("SeriesAgFishCalPerCapPerDayLiverOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s11.Key
                                      Select Key = s.Key, Value = If(s.Value, 0), Val2 = If(s2.Value, 0), Val3 = If(s3.Value, 0), Val4 = If(s4.Value, 0),
                                  Val5 = If(s5.Value, 0), Val6 = If(s6.Value, 0), Val7 = If(s7.Value, 0), Val8 = If(s8.Value, 0), Val9 = If(s9.Value, 0),
                                  Val10 = If(s10.Value, 0), Val11 = If(s11.Value, 0))
                        Dim FishCal = New Dictionary(Of Integer, Single?)
                        FishCal.Add(1, item.Value)
                        FishCal.Add(2, item.Val2)
                        FishCal.Add(3, item.Val3)
                        FishCal.Add(4, item.Val4)
                        FishCal.Add(5, item.Val5)
                        FishCal.Add(6, item.Val6)
                        FishCal.Add(7, item.Val7)
                        FishCal.Add(8, item.Val8)
                        FishCal.Add(9, item.Val9)
                        FishCal.Add(10, item.Val10)
                        FishCal.Add(11, item.Val11)

                        AgFishCalPerCapPerDayFAO.Add(item.Key, FishCal)
                    Next
                End If 'End of if history else forecast

                Dim CCLPCFISHVal As Single?
                For Each key In cntx.Regionalization.Keys
                    'Sum across first 8 categories to get total 'fish' calories
                    CCLPCFISHVal = 0
                    For fc% = 1 To 8
                        CCLPCFISHVal = CCLPCFISHVal + AgFishCalPerCapPerDayFAO(key)(fc%)
                    Next fc%

                    'dsr 2015/11/12 Treat 0 values as null, to be filled in later
                    If CCLPCFISHVal = 0 Then CCLPCFISHVal = Nothing

                    CCLPCFISH.Add(key, CCLPCFISHVal)

                    'Define calories for Aquatic Plants and FishOils. These can be 0.
                    CCLPCAQPLANTS.Add(key, AgFishCalPerCapPerDayFAO(key)(9))
                    CCLPCFISHOILS.Add(key, AgFishCalPerCapPerDayFAO(key)(10) + AgFishCalPerCapPerDayFAO(key)(11))

                Next
            End Sub

            Private Sub GetFishProteinData(cntx As IRebuildFacade)
                '1: Cephalopods, 2: Crustaceans, 3: Demersal, 4: Freshwater, 5: Marine, 6: Molluscs, 7: Pelagic
                '8: AqAnimals, 9: AqPlants, 10: BodyOil, 11: LiverOil. Note: no human consumption of fish meal.
                Dim Fish As Dictionary(Of Integer, Single?)
                If cntx.HistoricMode Then
                    For Each item In (From s In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayCephalopodsFAO", "Earliest")
                                      Join s2 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayCrustaceansFAO", "Earliest") On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayDemersalFAO", "Earliest") On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayFreshwaterFAO", "Earliest") On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayMarineFAO", "Earliest") On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayMolluscsFAO", "Earliest") On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayPelagicFAO", "Earliest") On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayAqAnimalsFAO", "Earliest") On s.Key Equals s8.Key
                                      Join s9 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayAqPlantsFAO", "Earliest") On s.Key Equals s9.Key
                                      Join s10 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayBodyOilFAO", "Earliest") On s.Key Equals s10.Key
                                      Join s11 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayLiverOilFAO", "Earliest") On s.Key Equals s11.Key
                                      Select Key = s.Key, Value = AMAX(If(s.Value, 0), 0), Val2 = AMAX(If(s2.Value, 0), 0), Val3 = AMAX(If(s3.Value, 0), 0), Val4 = AMAX(If(s4.Value, 0), 0),
                                  Val5 = AMAX(If(s5.Value, 0), 0), Val6 = AMAX(If(s6.Value, 0), 0), Val7 = AMAX(If(s7.Value, 0), 0), Val8 = AMAX(If(s8.Value, 0), 0), Val9 = AMAX(If(s9.Value, 0), 0),
                                  Val10 = AMAX(If(s10.Value, 0), 0), Val11 = AMAX(If(s11.Value, 0), 0))
                        Fish = New Dictionary(Of Integer, Single?)
                        Fish.Add(1, item.Value)
                        Fish.Add(2, item.Val2)
                        Fish.Add(3, item.Val3)
                        Fish.Add(4, item.Val4)
                        Fish.Add(5, item.Val5)
                        Fish.Add(6, item.Val6)
                        Fish.Add(7, item.Val7)
                        Fish.Add(8, item.Val8)
                        Fish.Add(9, item.Val9)
                        Fish.Add(10, item.Val10)
                        Fish.Add(11, item.Val11)

                        AgFishProteinPerCapPerDayFAO.Add(item.Key, Fish)
                    Next
                Else 'Forecast *********
                    For Each item In (From s In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayCephalopodsFAO", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayCrustaceansFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayDemersalFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayFreshwaterFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayMarineFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayMolluscsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayPelagicFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayAqAnimalsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s8.Key
                                      Join s9 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayAqPlantsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s9.Key
                                      Join s10 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayBodyOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s10.Key
                                      Join s11 In cntx.GetSlice("SeriesAgFishProteinPerCapPerDayLiverOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s11.Key
                                      Select Key = s.Key, Value = AMAX(If(s.Value, 0), 0), Val2 = AMAX(If(s2.Value, 0), 0), Val3 = AMAX(If(s3.Value, 0), 0), Val4 = AMAX(If(s4.Value, 0), 0),
                                  Val5 = AMAX(If(s5.Value, 0), 0), Val6 = AMAX(If(s6.Value, 0), 0), Val7 = AMAX(If(s7.Value, 0), 0), Val8 = AMAX(If(s8.Value, 0), 0), Val9 = AMAX(If(s9.Value, 0), 0),
                                  Val10 = AMAX(If(s10.Value, 0), 0), Val11 = AMAX(If(s11.Value, 0), 0))
                        Fish = New Dictionary(Of Integer, Single?)
                        Fish.Add(1, item.Value)
                        Fish.Add(2, item.Val2)
                        Fish.Add(3, item.Val3)
                        Fish.Add(4, item.Val4)
                        Fish.Add(5, item.Val5)
                        Fish.Add(6, item.Val6)
                        Fish.Add(7, item.Val7)
                        Fish.Add(8, item.Val8)
                        Fish.Add(9, item.Val9)
                        Fish.Add(10, item.Val10)
                        Fish.Add(11, item.Val11)

                        AgFishProteinPerCapPerDayFAO.Add(item.Key, Fish)
                    Next
                End If 'End of if history else forecast

                Dim FishVal As Single?
                For Each key In cntx.Regionalization.Keys
                    'Sum across first 8 categories to get total 'fish' calories
                    FishVal = 0
                    For fc% = 1 To 8
                        FishVal = FishVal + AgFishProteinPerCapPerDayFAO(key)(fc%)
                    Next fc%

                    'dsr 2015/11/12 Treat 0 values as null, to be filled in later
                    If FishVal = 0 Then FishVal = Nothing

                    CPROTEINPCFISH.Add(key, FishVal)

                    'Define protein for Aquatic Plants and FishOils. These can be 0.
                    CPROTEINPCAQPLANTS.Add(key, AgFishProteinPerCapPerDayFAO(key)(9))
                    CPROTEINPCFISHOILS.Add(key, AgFishProteinPerCapPerDayFAO(key)(10) + AgFishProteinPerCapPerDayFAO(key)(11))

                Next
            End Sub

            Private Sub GetFishGramsData(cntx As IRebuildFacade)
                '1: Cephalopods, 2: Crustaceans, 3: Demersal, 4: Freshwater, 5: Marine, 6: Molluscs, 7: Pelagic
                '8: AqAnimals, 9: AqPlants, 10: BodyOil, 11: LiverOil. Note: no human consumption of fish meal.
                Dim ValHold As Single?, i As Integer
                Dim Fish As Dictionary(Of Integer, Single?)
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriTables As New List(Of String)
                    Dim DictTbls As New List(Of IDictionary(Of String, Single?()))
                    Dim yrss As New List(Of Short())
                    SeriTables.Add("SeriesAgFishFoodSupplyPerCapPerDayCephalopodsFAO")
                    SeriTables.Add("SeriesAgFishFoodSupplyPerCapPerDayCrustaceansFAO")
                    SeriTables.Add("SeriesAgFishFoodSupplyPerCapPerDayDemersalFAO")
                    SeriTables.Add("SeriesAgFishFoodSupplyPerCapPerDayFreshwaterFAO")
                    SeriTables.Add("SeriesAgFishFoodSupplyPerCapPerDayMarineFAO")
                    SeriTables.Add("SeriesAgFishFoodSupplyPerCapPerDayMolluscsFAO")
                    SeriTables.Add("SeriesAgFishFoodSupplyPerCapPerDayPelagicFAO")
                    SeriTables.Add("SeriesAgFishFoodSupplyPerCapPerDayAqAnimalsFAO")
                    SeriTables.Add("SeriesAgFishFoodSupplyPerCapPerDayAqPlantsFAO")
                    SeriTables.Add("SeriesAgFishFoodSupplyPerCapPerDayBodyOilFAO")
                    SeriTables.Add("SeriesAgFishFoodSupplyPerCapPerDayLiverOilFAO")
                    For i = 0 To SeriTables.Count - 1
                        yrss.Add(cntx.GetYears(SeriTables(i)).OrderBy(Function(y) y).ToArray())
                        DictTbls.Add(cntx.GetDice(SeriTables(i), yrss(i)))
                    Next
                    For Each key In cntx.Regionalization.Keys
                        Dim FishCal = New Dictionary(Of Integer, Single?)
                        For i = 0 To SeriTables.Count - 1
                            ValHold = getClosestYrVal(DictTbls(i), yrss(i), key, cntx.FirstYear, HistDist)
                            FishCal.Add(i + 1, AMAX(If(ValHold.HasValue, ValHold, 0), 0))
                        Next
                        AgFishGramsPerCapPerDayFAO.Add(key, FishCal)
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else 'Forecast *********
                    For Each item In (From s In cntx.GetSlice("SeriesAgFishFoodSupplyPerCapPerDayCephalopodsFAO", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("SeriesAgFishFoodSupplyPerCapPerDayCrustaceansFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAgFishFoodSupplyPerCapPerDayDemersalFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAgFishFoodSupplyPerCapPerDayFreshwaterFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAgFishFoodSupplyPerCapPerDayMarineFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAgFishFoodSupplyPerCapPerDayMolluscsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAgFishFoodSupplyPerCapPerDayPelagicFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAgFishFoodSupplyPerCapPerDayAqAnimalsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s8.Key
                                      Join s9 In cntx.GetSlice("SeriesAgFishFoodSupplyPerCapPerDayAqPlantsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s9.Key
                                      Join s10 In cntx.GetSlice("SeriesAgFishFoodSupplyPerCapPerDayBodyOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s10.Key
                                      Join s11 In cntx.GetSlice("SeriesAgFishFoodSupplyPerCapPerDayLiverOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s11.Key
                                      Select Key = s.Key, Value = AMAX(If(s.Value, 0), 0), Val2 = AMAX(If(s2.Value, 0), 0), Val3 = AMAX(If(s3.Value, 0), 0), Val4 = AMAX(If(s4.Value, 0), 0),
                                  Val5 = AMAX(If(s5.Value, 0), 0), Val6 = AMAX(If(s6.Value, 0), 0), Val7 = AMAX(If(s7.Value, 0), 0), Val8 = AMAX(If(s8.Value, 0), 0), Val9 = AMAX(If(s9.Value, 0), 0),
                                  Val10 = AMAX(If(s10.Value, 0), 0), Val11 = AMAX(If(s11.Value, 0), 0))
                        Fish = New Dictionary(Of Integer, Single?)
                        Fish.Add(1, item.Value)
                        Fish.Add(2, item.Val2)
                        Fish.Add(3, item.Val3)
                        Fish.Add(4, item.Val4)
                        Fish.Add(5, item.Val5)
                        Fish.Add(6, item.Val6)
                        Fish.Add(7, item.Val7)
                        Fish.Add(8, item.Val8)
                        Fish.Add(9, item.Val9)
                        Fish.Add(10, item.Val10)
                        Fish.Add(11, item.Val11)

                        AgFishGramsPerCapPerDayFAO.Add(item.Key, Fish)
                    Next

                End If 'End of if history else forecast

                Dim FishVal As Single?
                For Each key In cntx.Regionalization.Keys
                    'Sum across first 8 categories to get total 'fish' calories
                    FishVal = 0
                    For fc% = 1 To 8
                        FishVal = FishVal + AgFishGramsPerCapPerDayFAO(key)(fc%)
                    Next fc%
                    'dsr 2015/11/12 Treat 0 values as null, to be filled in later
                    If FishVal = 0 Then FishVal = Nothing
                    CGRAMSPCFISH.Add(key, FishVal)

                    'Define protein for Aquatic Plants and FishOils. These can be 0.
                    CGRAMSPCAQPLANTS.Add(key, AgFishGramsPerCapPerDayFAO(key)(9))
                    CGRAMSPCFISHOILS.Add(key, AgFishGramsPerCapPerDayFAO(key)(10) + AgFishGramsPerCapPerDayFAO(key)(11))

                Next
            End Sub

            Private Sub FillFishNutritionHolesIfCan(cntx As IRebuildFacade)
                Dim GlobalFishCalories As Single, GlobalFishProtein As Single, GlobalFishGrams As Single
                'Use global average ratios of protein to calories, etc. to fill holes
                GlobalFishCalories = 0
                GlobalFishProtein = 0
                GlobalFishGrams = 0
                For Each key In cntx.Regionalization.Keys
                    If CCLPCFISH(key).HasValue And CPROTEINPCFISH(key).HasValue And CGRAMSPCFISH(key).HasValue Then
                        'jrs 2018/09/20 changed from POP to CPop
                        GlobalFishCalories = GlobalFishCalories + CSng(CCLPCFISH(key) * CPOP(key))
                        GlobalFishProtein = GlobalFishProtein + CSng(CPROTEINPCFISH(key) * CPOP(key))
                        GlobalFishGrams = GlobalFishGrams + CSng(CGRAMSPCFISH(key) * CPOP(key))
                    End If
                Next

                For Each key In cntx.Regionalization.Keys
                    If Not CCLPCFISH(key).HasValue Then
                        If CPROTEINPCFISH(key).HasValue Then
                            CCLPCFISH.Remove(key)
                            CCLPCFISH.Add(key, CPROTEINPCFISH(key) * GlobalFishCalories / GlobalFishProtein)
                        Else : If CGRAMSPCFISH(key).HasValue Then
                                CCLPCFISH.Remove(key)
                                CCLPCFISH.Add(key, CGRAMSPCFISH(key) * GlobalFishCalories / GlobalFishGrams)
                            End If
                        End If
                    End If
                    If Not CPROTEINPCFISH(key).HasValue Then
                        If CCLPCFISH(key).HasValue Then
                            CPROTEINPCFISH.Remove(key)
                            CPROTEINPCFISH.Add(key, CCLPCFISH(key) * GlobalFishProtein / GlobalFishCalories)
                        Else : If CGRAMSPCFISH(key).HasValue Then
                                CPROTEINPCFISH.Remove(key)
                                CPROTEINPCFISH.Add(key, CGRAMSPCFISH(key) * GlobalFishProtein / GlobalFishGrams)
                            End If
                        End If
                    End If
                    If Not CGRAMSPCFISH(key).HasValue Then
                        If CCLPCFISH(key).HasValue Then
                            CGRAMSPCFISH.Remove(key)
                            CGRAMSPCFISH.Add(key, CCLPCFISH(key) * GlobalFishGrams / GlobalFishCalories)
                        Else : If CPROTEINPCFISH(key).HasValue Then
                                CGRAMSPCFISH.Remove(key)
                                CGRAMSPCFISH.Add(key, CPROTEINPCFISH(key) * GlobalFishGrams / GlobalFishProtein)
                            End If
                        End If
                    End If

                    'Use final values for aggregate fish value until we deal with plants and oils
                    CCLPC_Fish.Add(key, CCLPCFISH(key))
                    CPROTEINPC_Fish.Add(key, CPROTEINPCFISH(key))
                    CGRAMSPC_Fish.Add(key, CGRAMSPCFISH(key))
                Next
            End Sub

            Private Sub GetFishDemandSideData(cntx As IRebuildFacade)
                'Step 8: Get data on use from Commodity Balances
                '1: Cephalopods, 2: Crustaceans, 3: Demersal, 4: Freshwater, 5: Marine, 6: Molluscs, 7: Pelagic
                '8: AqAnimals, 9: AqPlants, 10: BodyOil, 11: LiverOil, 12: FishMeal
                'dsr 2015/08/13 Note no food data for FishMeal, no feed data for AqAnimals,
                '               no seed data for AqPlants, FishOils, or FishMeal
                Dim ValHold As Single?, i As Integer
                Dim Fish As Dictionary(Of Integer, Single?)
                If cntx.HistoricMode Then
                    '1: Cephalopods, 2: Crustaceans, 3: Demersal, 4: Freshwater, 5: Marine, 6: Molluscs, 7: Pelagic
                    '8: AqAnimals, 9: AqPlants, 10: BodyOil, 11: LiverOil, 12: Meal
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriTables As New List(Of String)
                    Dim DictTbls As New List(Of IDictionary(Of String, Single?()))
                    Dim yrss As New List(Of Short())
                    SeriTables.Add("SeriesAgFishtoFoodCephalopodsFAO")
                    SeriTables.Add("SeriesAgFishtoFoodCrustaceansFAO")
                    SeriTables.Add("SeriesAgFishtoFoodDemersalFAO")
                    SeriTables.Add("SeriesAgFishtoFoodFreshwaterFAO")
                    SeriTables.Add("SeriesAgFishtoFoodMarineFAO")
                    SeriTables.Add("SeriesAgFishtoFoodMolluscsFAO")
                    SeriTables.Add("SeriesAgFishtoFoodPelagicFAO")
                    SeriTables.Add("SeriesAgFishtoFoodAqAnimalsFAO")
                    SeriTables.Add("SeriesAgFishtoFoodAqPlantsFAO")
                    SeriTables.Add("SeriesAgFishtoFoodBodyOilFAO")
                    SeriTables.Add("SeriesAgFishtoFoodLiverOilFAO")
                    For i = 0 To SeriTables.Count - 1
                        yrss.Add(cntx.GetYears(SeriTables(i)).OrderBy(Function(y) y).ToArray())
                        DictTbls.Add(cntx.GetDice(SeriTables(i), yrss(i)))
                    Next
                    For Each key In cntx.Regionalization.Keys
                        Dim FishCal = New Dictionary(Of Integer, Single?)
                        For i = 0 To SeriTables.Count - 1
                            ValHold = getClosestYrVal(DictTbls(i), yrss(i), key, cntx.FirstYear, HistDist)
                            FishCal.Add(i + 1, AMAX(If(ValHold.HasValue, ValHold, 0), 0))
                        Next
                        AgFishtoFoodFAO.Add(key, FishCal)
                    Next
                    'mti 2019/04/29  Flex Hist Base End


                Else 'Forecast *********
                    For Each item In (From s In cntx.GetSlice("SeriesAgFishtoFoodCephalopodsFAO", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("SeriesAgFishtoFoodCrustaceansFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAgFishtoFoodDemersalFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAgFishtoFoodFreshwaterFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAgFishtoFoodMarineFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAgFishtoFoodMolluscsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAgFishtoFoodPelagicFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAgFishtoFoodAqAnimalsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s8.Key
                                      Join s9 In cntx.GetSlice("SeriesAgFishtoFoodAqPlantsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s9.Key
                                      Join s10 In cntx.GetSlice("SeriesAgFishtoFoodBodyOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s10.Key
                                      Join s11 In cntx.GetSlice("SeriesAgFishtoFoodLiverOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s11.Key
                                      Select Key = s.Key, Value = If(s.Value, 0), Val2 = If(s2.Value, 0), Val3 = If(s3.Value, 0), Val4 = If(s4.Value, 0),
                                  Val5 = If(s5.Value, 0), Val6 = If(s6.Value, 0), Val7 = If(s7.Value, 0), Val8 = If(s8.Value, 0), Val9 = If(s9.Value, 0),
                                  Val10 = If(s10.Value, 0), Val11 = If(s11.Value, 0))
                        Fish = New Dictionary(Of Integer, Single?)
                        Fish.Add(1, item.Value)
                        Fish.Add(2, item.Val2)
                        Fish.Add(3, item.Val3)
                        Fish.Add(4, item.Val4)
                        Fish.Add(5, item.Val5)
                        Fish.Add(6, item.Val6)
                        Fish.Add(7, item.Val7)
                        Fish.Add(8, item.Val8)
                        Fish.Add(9, item.Val9)
                        Fish.Add(10, item.Val10)
                        Fish.Add(11, item.Val11)

                        AgFishtoFoodFAO.Add(item.Key, Fish)
                    Next
                End If 'End of if history else forecast

                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriTables As New List(Of String)
                    Dim DictTbls As New List(Of IDictionary(Of String, Single?()))
                    Dim yrss As New List(Of Short())
                    SeriTables.Add("SeriesAgFishtoFeedCephalopodsFAO")
                    SeriTables.Add("SeriesAgFishtoFeedCrustaceansFAO")
                    SeriTables.Add("SeriesAgFishtoFeedDemersalFAO")
                    SeriTables.Add("SeriesAgFishtoFeedFreshwaterFAO")
                    SeriTables.Add("SeriesAgFishtoFeedMarineFAO")
                    SeriTables.Add("SeriesAgFishtoFeedMolluscsFAO")
                    SeriTables.Add("SeriesAgFishtoFeedPelagicFAO")
                    SeriTables.Add("SeriesAgFishtoFeedAqPlantsFAO")
                    SeriTables.Add("SeriesAgFishtoFeedBodyOilFAO")
                    SeriTables.Add("SeriesAgFishtoFeedLiverOilFAO")
                    SeriTables.Add("SeriesAgFishtoFeedMealFAO")
                    For i = 0 To SeriTables.Count - 1
                        yrss.Add(cntx.GetYears(SeriTables(i)).OrderBy(Function(y) y).ToArray())
                        DictTbls.Add(cntx.GetDice(SeriTables(i), yrss(i)))
                    Next
                    For Each key In cntx.Regionalization.Keys
                        Dim FishCal = New Dictionary(Of Integer, Single?)
                        For i = 0 To SeriTables.Count - 1
                            ValHold = getClosestYrVal(DictTbls(i), yrss(i), key, cntx.FirstYear, HistDist)
                            FishCal.Add(i + 1, AMAX(If(ValHold.HasValue, ValHold, 0), 0))
                        Next
                        AgFishtoFeedFAO.Add(key, FishCal)
                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In cntx.GetSlice("SeriesAgFishtoFeedCephalopodsFAO", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("SeriesAgFishtoFeedCrustaceansFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAgFishtoFeedDemersalFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAgFishtoFeedFreshwaterFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAgFishtoFeedMarineFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAgFishtoFeedMolluscsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAgFishtoFeedPelagicFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAgFishtoFeedAqPlantsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s8.Key
                                      Join s9 In cntx.GetSlice("SeriesAgFishtoFeedBodyOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s9.Key
                                      Join s10 In cntx.GetSlice("SeriesAgFishtoFeedLiverOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s10.Key
                                      Join s11 In cntx.GetSlice("SeriesAgFishtoFeedMealFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s11.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value,
                                  Val5 = s5.Value, Val6 = s6.Value, Val7 = s7.Value, Val8 = s8.Value, Val9 = s9.Value,
                                  Val10 = s10.Value, Val11 = s11.Value)
                        Fish = New Dictionary(Of Integer, Single?)
                        Fish.Add(1, item.Value)
                        Fish.Add(2, item.Val2)
                        Fish.Add(3, item.Val3)
                        Fish.Add(4, item.Val4)
                        Fish.Add(5, item.Val5)
                        Fish.Add(6, item.Val6)
                        Fish.Add(7, item.Val7)
                        Fish.Add(8, item.Val8)
                        Fish.Add(9, item.Val9)
                        Fish.Add(10, item.Val10)
                        Fish.Add(11, item.Val11)

                        AgFishtoFeedFAO.Add(item.Key, Fish)
                    Next
                End If 'End of if history else forecast

                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start

                    Dim SeriTables As New List(Of String)
                    Dim DictTbls As New List(Of IDictionary(Of String, Single?()))
                    Dim yrss As New List(Of Short())
                    SeriTables.Add("SeriesAgFishtoSeedCephalopodsFAO")
                    SeriTables.Add("SeriesAgFishtoSeedCrustaceansFAO")
                    SeriTables.Add("SeriesAgFishtoSeedDemersalFAO")
                    SeriTables.Add("SeriesAgFishtoSeedFreshwaterFAO")
                    SeriTables.Add("SeriesAgFishtoSeedMarineFAO")
                    SeriTables.Add("SeriesAgFishtoSeedMolluscsFAO")
                    SeriTables.Add("SeriesAgFishtoSeedPelagicFAO")
                    SeriTables.Add("SeriesAgFishtoSeedAqAnimalsFAO")
                    For i = 0 To SeriTables.Count - 1
                        yrss.Add(cntx.GetYears(SeriTables(i)).OrderBy(Function(y) y).ToArray())
                        DictTbls.Add(cntx.GetDice(SeriTables(i), yrss(i)))
                    Next
                    For Each key In cntx.Regionalization.Keys
                        Dim FishCal = New Dictionary(Of Integer, Single?)
                        For i = 0 To SeriTables.Count - 1
                            ValHold = getClosestYrVal(DictTbls(i), yrss(i), key, cntx.FirstYear, HistDist)
                            FishCal.Add(i + 1, AMAX(If(ValHold.HasValue, ValHold, 0), 0))
                        Next
                        AgFishtoSeedFAO.Add(key, FishCal)
                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In cntx.GetSlice("SeriesAgFishtoSeedCephalopodsFAO", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("SeriesAgFishtoSeedCrustaceansFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAgFishtoSeedDemersalFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAgFishtoSeedFreshwaterFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAgFishtoSeedMarineFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAgFishtoSeedMolluscsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAgFishtoSeedPelagicFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAgFishtoSeedAqAnimalsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s8.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value,
                                  Val5 = s5.Value, Val6 = s6.Value, Val7 = s7.Value, Val8 = s8.Value)
                        Fish = New Dictionary(Of Integer, Single?)
                        Fish.Add(1, item.Value)
                        Fish.Add(2, item.Val2)
                        Fish.Add(3, item.Val3)
                        Fish.Add(4, item.Val4)
                        Fish.Add(5, item.Val5)
                        Fish.Add(6, item.Val6)
                        Fish.Add(7, item.Val7)
                        Fish.Add(8, item.Val8)

                        AgFishtoSeedFAO.Add(item.Key, Fish)
                    Next
                End If 'End of if history else forecast

                If cntx.HistoricMode Then
                    Dim SeriTables As New List(Of String)
                    Dim DictTbls As New List(Of IDictionary(Of String, Single?()))
                    Dim yrss As New List(Of Short())
                    SeriTables.Add("SeriesAgFishtoOtherUtilCephalopodsFAO")
                    SeriTables.Add("SeriesAgFishtoOtherUtilCrustaceansFAO")
                    SeriTables.Add("SeriesAgFishtoOtherUtilDemersalFAO")
                    SeriTables.Add("SeriesAgFishtoOtherUtilFreshwaterFAO")
                    SeriTables.Add("SeriesAgFishtoOtherUtilMarineFAO")
                    SeriTables.Add("SeriesAgFishtoOtherUtilMolluscsFAO")
                    SeriTables.Add("SeriesAgFishtoOtherUtilPelagicFAO")
                    SeriTables.Add("SeriesAgFishtoOtherUtilAqAnimalsFAO")
                    SeriTables.Add("SeriesAgFishtoOtherUtilAqPlantsFAO")
                    SeriTables.Add("SeriesAgFishtoOtherUtilBodyOilFAO")
                    SeriTables.Add("SeriesAgFishtoOtherUtilLiverOilFAO")
                    SeriTables.Add("SeriesAgFishtoOtherUtilMealFAO")
                    For i = 0 To SeriTables.Count - 1
                        yrss.Add(cntx.GetYears(SeriTables(i)).OrderBy(Function(y) y).ToArray())
                        DictTbls.Add(cntx.GetDice(SeriTables(i), yrss(i)))
                    Next
                    For Each key In cntx.Regionalization.Keys
                        Dim FishCal = New Dictionary(Of Integer, Single?)
                        For i = 0 To SeriTables.Count - 1
                            ValHold = getClosestYrVal(DictTbls(i), yrss(i), key, cntx.FirstYear, HistDist)
                            FishCal.Add(i + 1, AMAX(If(ValHold.HasValue, ValHold, 0), 0))
                        Next
                        AgFishtoOtherUtilFAO.Add(key, FishCal)
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else 'Forecast *********
                    For Each item In (From s In cntx.GetSlice("SeriesAgFishtoOtherUtilCephalopodsFAO", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("SeriesAgFishtoOtherUtilCrustaceansFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAgFishtoOtherUtilDemersalFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAgFishtoOtherUtilFreshwaterFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAgFishtoOtherUtilMarineFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAgFishtoOtherUtilMolluscsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAgFishtoOtherUtilPelagicFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAgFishtoOtherUtilAqAnimalsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s8.Key
                                      Join s9 In cntx.GetSlice("SeriesAgFishtoOtherUtilAqPlantsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s9.Key
                                      Join s10 In cntx.GetSlice("SeriesAgFishtoOtherUtilBodyOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s10.Key
                                      Join s11 In cntx.GetSlice("SeriesAgFishtoOtherUtilLiverOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s11.Key
                                      Join s12 In cntx.GetSlice("SeriesAgFishtoOtherUtilMealFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s12.Key
                                      Select Key = s.Key, Value = If(s.Value, 0), Val2 = If(s2.Value, 0), Val3 = If(s3.Value, 0), Val4 = If(s4.Value, 0),
                                  Val5 = If(s5.Value, 0), Val6 = If(s6.Value, 0), Val7 = If(s7.Value, 0), Val8 = If(s8.Value, 0), Val9 = If(s9.Value, 0),
                                  Val10 = If(s10.Value, 0), Val11 = If(s11.Value, 0), Val12 = If(s12.Value, 0))
                        Fish = New Dictionary(Of Integer, Single?)
                        Fish.Add(1, item.Value)
                        Fish.Add(2, item.Val2)
                        Fish.Add(3, item.Val3)
                        Fish.Add(4, item.Val4)
                        Fish.Add(5, item.Val5)
                        Fish.Add(6, item.Val6)
                        Fish.Add(7, item.Val7)
                        Fish.Add(8, item.Val8)
                        Fish.Add(9, item.Val9)
                        Fish.Add(10, item.Val10)
                        Fish.Add(11, item.Val11)
                        Fish.Add(12, item.Val12)

                        AgFishtoOtherUtilFAO.Add(item.Key, Fish)
                    Next
                End If 'End of if history else forecast

                Dim CAGFISHFDEM As Dictionary(Of String, Single?), CAGFISHFEDDEM As Dictionary(Of String, Single?)
                Dim CAGFISHFMDEM As Dictionary(Of String, Single?), CAGFISHINDEM As Dictionary(Of String, Single?)
                CAGFISHFDEM = New Dictionary(Of String, Single?)
                CAGFISHFEDDEM = New Dictionary(Of String, Single?)
                CAGFISHFMDEM = New Dictionary(Of String, Single?)
                CAGFISHINDEM = New Dictionary(Of String, Single?)
                Dim noFooddata As Integer
                For Each key In cntx.Regionalization.Keys
                    'Fill holes, make sure no negative values, also sum to get total 'fish' to feed, converting from tons to MMT
                    For fc% = 1 To 11
                        'Fill holes, make sure no negative values, and indicate if any of the data for categories 1-8 are not null
                        noFooddata = 1 '1 = true; set to 0 (false) if any Food data for categories 1-8
                        If Not AgFishtoFoodFAO(key)(fc%).HasValue Then
                            AgFishtoFoodFAO(key)(fc%) = 0
                        Else : If fc% < 9 Then noFooddata = 0
                        End If
                        If Not AgFishtoFeedFAO(key)(fc%).HasValue Or AgFishtoFeedFAO(key)(fc%) < 0 Then AgFishtoFeedFAO(key)(fc%) = 0
                        If fc% < 9 Then
                            If Not AgFishtoSeedFAO(key)(fc%).HasValue Or AgFishtoSeedFAO(key)(fc%) < 0 Then AgFishtoSeedFAO(key)(fc%) = 0
                        End If
                        If Not AgFishtoOtherUtilFAO(key)(fc%).HasValue Or AgFishtoOtherUtilFAO(key)(fc%) < 0 Then AgFishtoOtherUtilFAO(key)(fc%) = 0
                    Next fc%
                    If Not AgFishtoOtherUtilFAO(key)(12).HasValue Or AgFishtoOtherUtilFAO(key)(12) < 0 Then AgFishtoOtherUtilFAO(key)(12) = 0
                    'Sum across first 8 categories to get total 'fish' to Food, Food Manufacturing, Feed, and Industry and convert from tons to MMT
                    CAGFISHFDEM.Add(key, 0)
                    CAGFISHFEDDEM.Add(key, 0)
                    CAGFISHFMDEM.Add(key, 0)
                    CAGFISHINDEM.Add(key, 0)
                    For fc% = 1 To 8
                        CAGFISHFDEM(key) = CAGFISHFDEM(key) + AgFishtoFoodFAO(key)(fc%) / 1000000
                        'dsr 2015/09/16 Remember no feed from AqAnimals
                        If fc% < 8 Then CAGFISHFEDDEM(key) = CAGFISHFEDDEM(key) + AgFishtoFeedFAO(key)(fc%) / 1000000
                        CAGFISHFMDEM(key) = CAGFISHFMDEM(key) + AgFishtoSeedFAO(key)(fc%) / 1000000
                        CAGFISHINDEM(key) = CAGFISHINDEM(key) + AgFishtoOtherUtilFAO(key)(fc%) / 1000000
                    Next fc%
                    'If truly no food data for categories 1-8, then estimate value for total 'fish' based on total grams per capita per day estimated earlier
                    'Otherwise, put in a minimal value of one ton.
                    If noFooddata = 1 Then
                        'jrs 2018/09/20 changed from POP to CPOP
                        CAGFISHFDEM(key) = CGRAMSPC_Fish(key) * CPOP(key) * 365 / 1000000
                    Else
                        CAGFISHFDEM(key) = AMAX(CAGFISHFDEM(key), 0.000001F)
                    End If
                    'Set minimal values of one ton for FEDDEM, FMDEM, and INDEM
                    CAGFISHFEDDEM(key) = AMAX(CAGFISHFEDDEM(key), 0.000001F)
                    CAGFISHFMDEM(key) = AMAX(CAGFISHFMDEM(key), 0.000001F)
                    CAGFISHINDEM(key) = AMAX(CAGFISHINDEM(key), 0.000001F)

                    'Set values for aggregate fish category until we deal with plants and oils
                    CFDEM_Fish.Add(key, CAGFISHFDEM(key))
                    CFMDEM_Fish.Add(key, CAGFISHFMDEM(key))
                    CFEDDEM_Fish.Add(key, CAGFISHFEDDEM(key))
                    CINDEM_Fish.Add(key, CAGFISHINDEM(key))
                    CAGLOSSTRANS_Fish.Add(key, 0) ' no data so set to 0

                    'jrs 2018/11/15 Nob being used
                    'Calculate demands for food, feed, and industry for Aquatic Plants, FishOils and FishMeal and convert from tons to MMT
                    'dsr 2015/09/16 Remember no demand for food from FishMeal or for Food Manufacture from Plants, Oils, or Meal
                    'CAGAQPLANTSFDEM(key) = AgFishtoFoodFAO(key)(9) / 1000000
                    'CAGFISHOILSFDEM(key) = (AgFishtoFoodFAO(key)(10) + AgFishtoFoodFAO(key)(11)) / 1000000
                    'CAGAQPLANTSFEDDEM(key) = AgFishtoFeedFAO(key)(9) / 1000000
                    'CAGFISHOILSFEDDEM(key) = (AgFishtoFeedFAO(key)(10) + AgFishtoFeedFAO(key)(11)) / 1000000
                    'CAGFISHMEALFEDDEM(key) = AgFishtoFeedFAO(key)(12) / 1000000
                    'CAGAQPLANTSINDEM(key) = AgFishtoOtherUtilFAO(key)(9) / 1000000
                    'CAGFISHOILSINDEM(key) = (AgFishtoOtherUtilFAO(key)(10) + AgFishtoFeedFAO(key)(11)) / 1000000
                    'CAGFISHMEALINDEM(key) = AgFishtoOtherUtilFAO(key)(12) / 1000000
                Next 'For each country
            End Sub

            Private Sub GetFishSupplySideData(cntx As IRebuildFacade)
                Dim AgFishGlobalFeedDomSuppRatio As Dictionary(Of Integer, Single?)
                AgFishGlobalFeedDomSuppRatio = New Dictionary(Of Integer, Single?)
                Dim i As Integer
                Dim Fish As Dictionary(Of Integer, Single?)
                Using db As IFsContext = New IFsContext()
                    Dim gp As AgGlobalFeedtoDomSuppRatios = db.AgGlobalFeedtoDomSuppRatios.Where(Function(g) g.FishCategory = "Cephalopods").Single()
                    AgFishGlobalFeedDomSuppRatio.Add(1, CSng(gp.MostRecent))
                    gp = db.AgGlobalFeedtoDomSuppRatios.Where(Function(g) g.FishCategory = "Crustaceans").Single()
                    AgFishGlobalFeedDomSuppRatio.Add(2, CSng(gp.MostRecent))
                    gp = db.AgGlobalFeedtoDomSuppRatios.Where(Function(g) g.FishCategory = "Demersal").Single()
                    AgFishGlobalFeedDomSuppRatio.Add(3, CSng(gp.MostRecent))
                    gp = db.AgGlobalFeedtoDomSuppRatios.Where(Function(g) g.FishCategory = "Freshwater").Single()
                    AgFishGlobalFeedDomSuppRatio.Add(4, CSng(gp.MostRecent))
                    gp = db.AgGlobalFeedtoDomSuppRatios.Where(Function(g) g.FishCategory = "Marine").Single()
                    AgFishGlobalFeedDomSuppRatio.Add(5, CSng(gp.MostRecent))
                    gp = db.AgGlobalFeedtoDomSuppRatios.Where(Function(g) g.FishCategory = "Molluscs").Single()
                    AgFishGlobalFeedDomSuppRatio.Add(6, CSng(gp.MostRecent))
                    gp = db.AgGlobalFeedtoDomSuppRatios.Where(Function(g) g.FishCategory = "Pelagic").Single()
                    AgFishGlobalFeedDomSuppRatio.Add(7, CSng(gp.MostRecent))
                    gp = db.AgGlobalFeedtoDomSuppRatios.Where(Function(g) g.FishCategory = "AqAnimals").Single()
                    AgFishGlobalFeedDomSuppRatio.Add(8, CSng(gp.MostRecent))
                    gp = db.AgGlobalFeedtoDomSuppRatios.Where(Function(g) g.FishCategory = "AqPlants").Single()
                    AgFishGlobalFeedDomSuppRatio.Add(9, CSng(gp.MostRecent))
                End Using
                'Step 1: Get data on production, imports, exports, and domestic supply by fish category from Commodity Balances
                '     a: Read in data
                '     b: Fill in holes
                '     c: Normalize data so that production + imports - exports = domestic supply
                Dim ValHold As Single?
                'Dim Fish As Dictionary(Of Integer, Single?)
                If cntx.HistoricMode Then
                    '1: Cephalopods, 2: Crustaceans, 3: Demersal, 4: Freshwater, 5: Marine, 6: Molluscs, 7: Pelagic
                    '8: AqAnimals, 9: AqPlants, 10: BodyOil, 11: LiverOil, 12: FishMeal
                    'mti 2019/04/29  Flex Hist Base Start

                    Dim SeriTables As New List(Of String)
                    Dim DictTbls As New List(Of IDictionary(Of String, Single?()))
                    Dim yrss As New List(Of Short())
                    SeriTables.Add("SeriesAgFishProdCephalopodsFAO")
                    SeriTables.Add("SeriesAgFishProdCrustaceansFAO")
                    SeriTables.Add("SeriesAgFishProdDemersalFAO")
                    SeriTables.Add("SeriesAgFishProdFreshwaterFAO")
                    SeriTables.Add("SeriesAgFishProdMarineFAO")
                    SeriTables.Add("SeriesAgFishProdMolluscsFAO")
                    SeriTables.Add("SeriesAgFishProdPelagicFAO")
                    SeriTables.Add("SeriesAgFishProdAqAnimalsFAO")
                    SeriTables.Add("SeriesAgFishProdAqPlantsFAO")
                    SeriTables.Add("SeriesAgFishProdBodyOilFAO")
                    SeriTables.Add("SeriesAgFishProdLiverOilFAO")
                    SeriTables.Add("SeriesAgFishProdMealFAO")
                    For i = 0 To SeriTables.Count - 1
                        yrss.Add(cntx.GetYears(SeriTables(i)).OrderBy(Function(y) y).ToArray())
                        DictTbls.Add(cntx.GetDice(SeriTables(i), yrss(i)))
                    Next
                    For Each key In cntx.Regionalization.Keys
                        Dim FishCal = New Dictionary(Of Integer, Single?)
                        For i = 0 To SeriTables.Count - 1
                            ValHold = getClosestYrVal(DictTbls(i), yrss(i), key, cntx.FirstYear, HistDist)
                            FishCal.Add(i + 1, ValHold)
                        Next
                        AgFishProd.Add(key, FishCal)
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else 'Forecast *********
                    For Each item In (From s In cntx.GetSlice("SeriesAgFishProdCephalopodsFAO", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("SeriesAgFishProdCrustaceansFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAgFishProdDemersalFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAgFishProdFreshwaterFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAgFishProdMarineFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAgFishProdMolluscsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAgFishProdPelagicFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAgFishProdAqAnimalsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s8.Key
                                      Join s9 In cntx.GetSlice("SeriesAgFishProdAqPlantsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s9.Key
                                      Join s10 In cntx.GetSlice("SeriesAgFishProdBodyOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s10.Key
                                      Join s11 In cntx.GetSlice("SeriesAgFishProdLiverOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s11.Key
                                      Join s12 In cntx.GetSlice("SeriesAgFishProdMealFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s12.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value,
                                  Val5 = s5.Value, Val6 = s6.Value, Val7 = s7.Value, Val8 = s8.Value, Val9 = s9.Value,
                                  Val10 = s10.Value, Val11 = s11.Value, Val12 = s12.Value)
                        Fish = New Dictionary(Of Integer, Single?)
                        Fish.Add(1, item.Value)
                        Fish.Add(2, item.Val2)
                        Fish.Add(3, item.Val3)
                        Fish.Add(4, item.Val4)
                        Fish.Add(5, item.Val5)
                        Fish.Add(6, item.Val6)
                        Fish.Add(7, item.Val7)
                        Fish.Add(8, item.Val8)
                        Fish.Add(9, item.Val9)
                        Fish.Add(10, item.Val10)
                        Fish.Add(11, item.Val11)
                        Fish.Add(12, item.Val12)

                        AgFishProd.Add(item.Key, Fish)
                    Next
                End If 'End of if history else forecast

                If cntx.HistoricMode Then
                    '1: Cephalopods, 2: Crustaceans, 3: Demersal, 4: Freshwater, 5: Marine, 6: Molluscs, 7: Pelagic
                    '8: AqAnimals, 9: AqPlants, 10: BodyOil, 11: LiverOil, 12: FishMeal
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriTables As New List(Of String)
                    Dim DictTbls As New List(Of IDictionary(Of String, Single?()))
                    Dim yrss As New List(Of Short())
                    SeriTables.Add("SeriesAgFishImportsCephalopodsFAO")
                    SeriTables.Add("SeriesAgFishImportsCrustaceansFAO")
                    SeriTables.Add("SeriesAgFishImportsDemersalFAO")
                    SeriTables.Add("SeriesAgFishImportsFreshwaterFAO")
                    SeriTables.Add("SeriesAgFishImportsMarineFAO")
                    SeriTables.Add("SeriesAgFishImportsMolluscsFAO")
                    SeriTables.Add("SeriesAgFishImportsPelagicFAO")
                    SeriTables.Add("SeriesAgFishImportsAqAnimalsFAO")
                    SeriTables.Add("SeriesAgFishImportsAqPlantsFAO")
                    SeriTables.Add("SeriesAgFishImportsBodyOilFAO")
                    SeriTables.Add("SeriesAgFishImportsLiverOilFAO")
                    SeriTables.Add("SeriesAgFishImportsMealFAO")
                    For i = 0 To SeriTables.Count - 1
                        yrss.Add(cntx.GetYears(SeriTables(i)).OrderBy(Function(y) y).ToArray())
                        DictTbls.Add(cntx.GetDice(SeriTables(i), yrss(i)))
                    Next
                    For Each key In cntx.Regionalization.Keys
                        Fish = New Dictionary(Of Integer, Single?)
                        For i = 0 To SeriTables.Count - 1
                            ValHold = getClosestYrVal(DictTbls(i), yrss(i), key, cntx.FirstYear, HistDist)
                            Fish.Add(i + 1, ValHold)
                        Next
                        AgFishImports.Add(key, Fish)
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else 'Forecast *********
                    For Each item In (From s In cntx.GetSlice("SeriesAgFishImportsCephalopodsFAO", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("SeriesAgFishImportsCrustaceansFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAgFishImportsDemersalFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAgFishImportsFreshwaterFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAgFishImportsMarineFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAgFishImportsMolluscsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAgFishImportsPelagicFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAgFishImportsAqAnimalsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s8.Key
                                      Join s9 In cntx.GetSlice("SeriesAgFishImportsAqPlantsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s9.Key
                                      Join s10 In cntx.GetSlice("SeriesAgFishImportsBodyOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s10.Key
                                      Join s11 In cntx.GetSlice("SeriesAgFishImportsLiverOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s11.Key
                                      Join s12 In cntx.GetSlice("SeriesAgFishImportsMealFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s12.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value,
                                  Val5 = s5.Value, Val6 = s6.Value, Val7 = s7.Value, Val8 = s8.Value, Val9 = s9.Value,
                                  Val10 = s10.Value, Val11 = s11.Value, Val12 = s12.Value)
                        Fish = New Dictionary(Of Integer, Single?)
                        Fish.Add(1, item.Value)
                        Fish.Add(2, item.Val2)
                        Fish.Add(3, item.Val3)
                        Fish.Add(4, item.Val4)
                        Fish.Add(5, item.Val5)
                        Fish.Add(6, item.Val6)
                        Fish.Add(7, item.Val7)
                        Fish.Add(8, item.Val8)
                        Fish.Add(9, item.Val9)
                        Fish.Add(10, item.Val10)
                        Fish.Add(11, item.Val11)
                        Fish.Add(12, item.Val12)

                        AgFishImports.Add(item.Key, Fish)
                    Next
                End If 'End of if history else forecast

                If cntx.HistoricMode Then
                    '1: Cephalopods, 2: Crustaceans, 3: Demersal, 4: Freshwater, 5: Marine, 6: Molluscs, 7: Pelagic
                    '8: AqAnimals, 9: AqPlants, 10: BodyOil, 11: LiverOil, 12: FishMeal
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriTables As New List(Of String)
                    Dim DictTbls As New List(Of IDictionary(Of String, Single?()))
                    Dim yrss As New List(Of Short())
                    SeriTables.Add("SeriesAgFishExportsCephalopodsFAO")
                    SeriTables.Add("SeriesAgFishExportsCrustaceansFAO")
                    SeriTables.Add("SeriesAgFishExportsDemersalFAO")
                    SeriTables.Add("SeriesAgFishExportsFreshwaterFAO")
                    SeriTables.Add("SeriesAgFishExportsMarineFAO")
                    SeriTables.Add("SeriesAgFishExportsMolluscsFAO")
                    SeriTables.Add("SeriesAgFishExportsPelagicFAO")
                    SeriTables.Add("SeriesAgFishExportsAqAnimalsFAO")
                    SeriTables.Add("SeriesAgFishExportsAqPlantsFAO")
                    SeriTables.Add("SeriesAgFishExportsBodyOilFAO")
                    SeriTables.Add("SeriesAgFishExportsLiverOilFAO")
                    SeriTables.Add("SeriesAgFishExportsMealFAO")
                    For i = 0 To SeriTables.Count - 1
                        yrss.Add(cntx.GetYears(SeriTables(i)).OrderBy(Function(y) y).ToArray())
                        DictTbls.Add(cntx.GetDice(SeriTables(i), yrss(i)))
                    Next
                    For Each key In cntx.Regionalization.Keys
                        Fish = New Dictionary(Of Integer, Single?)
                        For i = 0 To SeriTables.Count - 1
                            ValHold = getClosestYrVal(DictTbls(i), yrss(i), key, cntx.FirstYear, HistDist)
                            Fish.Add(i + 1, ValHold)
                        Next
                        AgFishExports.Add(key, Fish)
                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else 'Forecast *********
                    For Each item In (From s In cntx.GetSlice("SeriesAgFishExportsCephalopodsFAO", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("SeriesAgFishExportsCrustaceansFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAgFishExportsDemersalFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAgFishExportsFreshwaterFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAgFishExportsMarineFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAgFishExportsMolluscsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAgFishExportsPelagicFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAgFishExportsAqAnimalsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s8.Key
                                      Join s9 In cntx.GetSlice("SeriesAgFishExportsAqPlantsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s9.Key
                                      Join s10 In cntx.GetSlice("SeriesAgFishExportsBodyOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s10.Key
                                      Join s11 In cntx.GetSlice("SeriesAgFishExportsLiverOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s11.Key
                                      Join s12 In cntx.GetSlice("SeriesAgFishExportsMealFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s12.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value,
                                  Val5 = s5.Value, Val6 = s6.Value, Val7 = s7.Value, Val8 = s8.Value, Val9 = s9.Value,
                                  Val10 = s10.Value, Val11 = s11.Value, Val12 = s12.Value)
                        Fish = New Dictionary(Of Integer, Single?)
                        Fish.Add(1, item.Value)
                        Fish.Add(2, item.Val2)
                        Fish.Add(3, item.Val3)
                        Fish.Add(4, item.Val4)
                        Fish.Add(5, item.Val5)
                        Fish.Add(6, item.Val6)
                        Fish.Add(7, item.Val7)
                        Fish.Add(8, item.Val8)
                        Fish.Add(9, item.Val9)
                        Fish.Add(10, item.Val10)
                        Fish.Add(11, item.Val11)
                        Fish.Add(12, item.Val12)

                        AgFishExports.Add(item.Key, Fish)
                    Next
                End If 'End of if history else forecast

                If cntx.HistoricMode Then
                    '1: Cephalopods, 2: Crustaceans, 3: Demersal, 4: Freshwater, 5: Marine, 6: Molluscs, 7: Pelagic
                    '8: AqAnimals, 9: AqPlants, 10: BodyOil, 11: LiverOil, 12: FishMeal
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriTables As New List(Of String)
                    Dim DictTbls As New List(Of IDictionary(Of String, Single?()))
                    Dim yrss As New List(Of Short())
                    SeriTables.Add("SeriesAgFishDomesticSupplyCephalopodsFAO")
                    SeriTables.Add("SeriesAgFishDomesticSupplyCrustaceansFAO")
                    SeriTables.Add("SeriesAgFishDomesticSupplyDemersalFAO")
                    SeriTables.Add("SeriesAgFishDomesticSupplyFreshwaterFAO")
                    SeriTables.Add("SeriesAgFishDomesticSupplyMarineFAO")
                    SeriTables.Add("SeriesAgFishDomesticSupplyMolluscsFAO")
                    SeriTables.Add("SeriesAgFishDomesticSupplyPelagicFAO")
                    SeriTables.Add("SeriesAgFishDomesticSupplyAqAnimalsFAO")
                    SeriTables.Add("SeriesAgFishDomesticSupplyAqPlantsFAO")
                    SeriTables.Add("SeriesAgFishDomesticSupplyBodyOilFAO")
                    SeriTables.Add("SeriesAgFishDomesticSupplyLiverOilFAO")
                    SeriTables.Add("SeriesAgFishDomesticSupplyMealFAO")
                    For i = 0 To SeriTables.Count - 1
                        yrss.Add(cntx.GetYears(SeriTables(i)).OrderBy(Function(y) y).ToArray())
                        DictTbls.Add(cntx.GetDice(SeriTables(i), yrss(i)))
                    Next
                    For Each key In cntx.Regionalization.Keys
                        Fish = New Dictionary(Of Integer, Single?)
                        For i = 0 To SeriTables.Count - 1
                            ValHold = getClosestYrVal(DictTbls(i), yrss(i), key, cntx.FirstYear, HistDist)
                            Fish.Add(i + 1, ValHold)
                        Next
                        AgFishDomesticSupply.Add(key, Fish)
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else 'Forecast *********
                    For Each item In (From s In cntx.GetSlice("SeriesAgFishDomesticSupplyCephalopodsFAO", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("SeriesAgFishDomesticSupplyCrustaceansFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAgFishDomesticSupplyDemersalFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAgFishDomesticSupplyFreshwaterFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAgFishDomesticSupplyMarineFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAgFishDomesticSupplyMolluscsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAgFishDomesticSupplyPelagicFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAgFishDomesticSupplyAqAnimalsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s8.Key
                                      Join s9 In cntx.GetSlice("SeriesAgFishDomesticSupplyAqPlantsFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s9.Key
                                      Join s10 In cntx.GetSlice("SeriesAgFishDomesticSupplyBodyOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s10.Key
                                      Join s11 In cntx.GetSlice("SeriesAgFishDomesticSupplyLiverOilFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s11.Key
                                      Join s12 In cntx.GetSlice("SeriesAgFishDomesticSupplyMealFAO", cntx.FirstYear, MyBase.Policy) On s.Key Equals s12.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value,
                                  Val5 = s5.Value, Val6 = s6.Value, Val7 = s7.Value, Val8 = s8.Value, Val9 = s9.Value,
                                  Val10 = s10.Value, Val11 = s11.Value, Val12 = s12.Value)
                        Fish = New Dictionary(Of Integer, Single?)
                        Fish.Add(1, item.Value)
                        Fish.Add(2, item.Val2)
                        Fish.Add(3, item.Val3)
                        Fish.Add(4, item.Val4)
                        Fish.Add(5, item.Val5)
                        Fish.Add(6, item.Val6)
                        Fish.Add(7, item.Val7)
                        Fish.Add(8, item.Val8)
                        Fish.Add(9, item.Val9)
                        Fish.Add(10, item.Val10)
                        Fish.Add(11, item.Val11)
                        Fish.Add(12, item.Val12)

                        AgFishDomesticSupply.Add(item.Key, Fish)
                    Next
                End If 'End of if history else forecast

                Dim SV As Single?, ExportAdj As Single?, ImportAdj As Single?
                Dim CAGFISHOILSPROD As Dictionary(Of String, Single?), CAGFISHOILSIMPORTS As Dictionary(Of String, Single?)
                Dim CAGFISHOILSEXPORTS As Dictionary(Of String, Single?), CAGFISHMEALPROD As Dictionary(Of String, Single?)
                Dim CAGFISHMEALIMPORTS As Dictionary(Of String, Single?), CAGFISHMEALEXPORTS As Dictionary(Of String, Single?)
                Dim CAGAQPLANTSIMPORTS As Dictionary(Of String, Single?), CAGAQPLANTSEXPORTS As Dictionary(Of String, Single?)
                CAGFISHOILSPROD = New Dictionary(Of String, Single?)
                CAGFISHOILSIMPORTS = New Dictionary(Of String, Single?)
                CAGFISHOILSEXPORTS = New Dictionary(Of String, Single?)
                CAGFISHMEALPROD = New Dictionary(Of String, Single?)
                CAGFISHMEALIMPORTS = New Dictionary(Of String, Single?)
                CAGFISHMEALEXPORTS = New Dictionary(Of String, Single?)
                CAGAQPLANTSIMPORTS = New Dictionary(Of String, Single?)
                CAGAQPLANTSEXPORTS = New Dictionary(Of String, Single?)

                For Each key In cntx.Regionalization.Keys
                    'Fill holes, make sure no negative values, and normalize so production + imports - exports = domestic supply; i.e. get rid of stock variations
                    For fc% = 1 To 12 'fc stands for fish category
                        'Deal with null or negative values
                        If Not AgFishProd(key)(fc%).HasValue Or AgFishProd(key)(fc%) <= 0 Then AgFishProd(key)(fc%) = 0.01
                        If Not AgFishImports(key)(fc%).HasValue Or AgFishImports(key)(fc%) <= 0 Then AgFishImports(key)(fc%) = 0.01
                        If Not AgFishExports(key)(fc%).HasValue Or AgFishExports(key)(fc%) <= 0 Then AgFishExports(key)(fc%) = 0.01
                        If Not AgFishDomesticSupply(key)(fc%).HasValue Or AgFishDomesticSupply(key)(fc%) <= 0 Then AgFishDomesticSupply(key)(fc%) =
                   AMAX(0.01F, AgFishProd(key)(fc%) + AgFishImports(key)(fc%) - AgFishExports(key)(fc%))
                        'Normalize production, imports, and exports to domestic supply when remove stock variations
                        'Logic: Stock variation (SV) = Dom Supp - (Prod + Imp - Exp)
                        '       IF SV > 0, i.e., stocks are being drawn down, achieve balance by increasing Prod and Imp, while decreasing Exp
                        '       If SV < 0, i.e., stocks are being added to, achieve balance by decreasing Prod and Imp, while increasing Exp
                        '       Base size of adjustments on relative sizes of Prod, Imp, and Exp, but do not let Exp, Imp, or Prod fall below 0
                        SV = AgFishDomesticSupply(key)(fc%) - (AgFishProd(key)(fc%) + AgFishImports(key)(fc%) - AgFishExports(key)(fc%))
                        ExportAdj = Amin(CSng(AgFishExports(key)(fc%) - 0.01),
                                     AgFishExports(key)(fc%) / (AgFishProd(key)(fc%) + AgFishImports(key)(fc%) + AgFishExports(key)(fc%)) * SV)
                        AgFishExports(key)(fc%) = AgFishExports(key)(fc%) - ExportAdj
                        ImportAdj = AMAX(CSng(-AgFishImports(key)(fc%) + 0.01),
                           AgFishImports(key)(fc%) / (AgFishProd(key)(fc%) + AgFishImports(key)(fc%)) * (SV - ExportAdj))
                        AgFishImports(key)(fc%) = AgFishImports(key)(fc%) + ImportAdj
                        AgFishProd(key)(fc%) = AMAX(0.01F, AgFishProd(key)(fc%) + (SV - ExportAdj - ImportAdj))
                    Next fc%

                    'dsr 2015/08/07 Calculate values for Production, Imports, Exports of FishOils and FishMeal and convert from tons to MMT; to be used later
                    'Calculate values for Imports and Exports of Aquatic Plants and convert from tons to MMT; also to be used later
                    CAGFISHOILSPROD(key) = (AgFishProd(key)(10) + AgFishProd(key)(11)) / 1000000
                    CAGFISHOILSIMPORTS(key) = (AgFishImports(key)(10) + AgFishImports(key)(11)) / 1000000
                    CAGFISHOILSEXPORTS(key) = (AgFishExports(key)(10) + AgFishExports(key)(11)) / 1000000
                    CAGFISHMEALPROD(key) = AgFishProd(key)(12) / 1000000
                    CAGFISHMEALIMPORTS(key) = AgFishImports(key)(12) / 1000000
                    CAGFISHMEALEXPORTS(key) = AgFishExports(key)(12) / 1000000
                    CAGAQPLANTSIMPORTS(key) = AgFishImports(key)(9) / 1000000
                    CAGAQPLANTSEXPORTS(key) = AgFishExports(key)(9) / 1000000
                Next

                'Step 2: Get data on aquaculture and catch by fish category from FishStatJ 
                '(only for fc% = 1 to 9)
                '     a: Read in data
                '     b: Fill in holes
                If cntx.HistoricMode Then
                    '1: Cephalopods, 2: Crustaceans, 3: Demersal, 4: Freshwater, 5: Marine, 6: Molluscs, 7: Pelagic
                    '8: AqAnimals, 9: AqPlants
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriTables As New List(Of String)
                    Dim DictTbls As New List(Of IDictionary(Of String, Single?()))
                    Dim yrss As New List(Of Short())
                    SeriTables.Add("SeriesAgFishAquaProdCephalopodsFSJ")
                    SeriTables.Add("SeriesAgFishAquaProdCrustaceansFSJ")
                    SeriTables.Add("SeriesAgFishAquaProdDemersalFSJ")
                    SeriTables.Add("SeriesAgFishAquaProdFreshwaterFSJ")
                    SeriTables.Add("SeriesAgFishAquaProdMarineFSJ")
                    SeriTables.Add("SeriesAgFishAquaProdMolluscsFSJ")
                    SeriTables.Add("SeriesAgFishAquaProdPelagicFSJ")
                    SeriTables.Add("SeriesAgFishAquaProdAqAnimalsFSJ")
                    SeriTables.Add("SeriesAgFishAquaProdAqPlantsFSJ")
                    For i = 0 To SeriTables.Count - 1
                        yrss.Add(cntx.GetYears(SeriTables(i)).OrderBy(Function(y) y).ToArray())
                        DictTbls.Add(cntx.GetDice(SeriTables(i), yrss(i)))
                    Next
                    For Each key In cntx.Regionalization.Keys
                        Fish = New Dictionary(Of Integer, Single?)
                        For i = 0 To SeriTables.Count - 1
                            ValHold = getClosestYrVal(DictTbls(i), yrss(i), key, cntx.FirstYear, HistDist)
                            Fish.Add(i + 1, ValHold)
                        Next
                        AgFishAquaProdFSJ.Add(key, Fish)
                    Next
                    'mti 2019/04/29  Flex Hist Base End
                Else 'Forecast *********
                    For Each item In (From s In cntx.GetSlice("SeriesAgFishAquaProdCephalopodsFSJ", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("SeriesAgFishAquaProdCrustaceansFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAgFishAquaProdDemersalFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAgFishAquaProdFreshwaterFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAgFishAquaProdMarineFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAgFishAquaProdMolluscsFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAgFishAquaProdPelagicFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAgFishAquaProdAqAnimalsFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s8.Key
                                      Join s9 In cntx.GetSlice("SeriesAgFishAquaProdAqPlantsFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s9.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value,
                                  Val5 = s5.Value, Val6 = s6.Value, Val7 = s7.Value, Val8 = s8.Value, Val9 = s9.Value)
                        Fish = New Dictionary(Of Integer, Single?)
                        Fish.Add(1, item.Value)
                        Fish.Add(2, item.Val2)
                        Fish.Add(3, item.Val3)
                        Fish.Add(4, item.Val4)
                        Fish.Add(5, item.Val5)
                        Fish.Add(6, item.Val6)
                        Fish.Add(7, item.Val7)
                        Fish.Add(8, item.Val8)
                        Fish.Add(9, item.Val9)

                        AgFishAquaProdFSJ.Add(item.Key, Fish)
                    Next
                End If 'End of if history else forecast

                If cntx.HistoricMode Then
                    '1: Cephalopods, 2: Crustaceans, 3: Demersal, 4: Freshwater, 5: Marine, 6: Molluscs, 7: Pelagic
                    '8: AqAnimals, 9: AqPlants
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriTables As New List(Of String)
                    Dim DictTbls As New List(Of IDictionary(Of String, Single?()))
                    Dim yrss As New List(Of Short())
                    SeriTables.Add("SeriesAgFishCatchProdCephalopodsFSJ")
                    SeriTables.Add("SeriesAgFishCatchProdCrustaceansFSJ")
                    SeriTables.Add("SeriesAgFishCatchProdDemersalFSJ")
                    SeriTables.Add("SeriesAgFishCatchProdFreshwaterFSJ")
                    SeriTables.Add("SeriesAgFishCatchProdMarineFSJ")
                    SeriTables.Add("SeriesAgFishCatchProdMolluscsFSJ")
                    SeriTables.Add("SeriesAgFishCatchProdPelagicFSJ")
                    SeriTables.Add("SeriesAgFishCatchProdAqAnimalsFSJ")
                    SeriTables.Add("SeriesAgFishCatchProdAqPlantsFSJ")
                    For i = 0 To SeriTables.Count - 1
                        yrss.Add(cntx.GetYears(SeriTables(i)).OrderBy(Function(y) y).ToArray())
                        DictTbls.Add(cntx.GetDice(SeriTables(i), yrss(i)))
                    Next
                    For Each key In cntx.Regionalization.Keys
                        Fish = New Dictionary(Of Integer, Single?)
                        For i = 0 To SeriTables.Count - 1
                            ValHold = getClosestYrVal(DictTbls(i), yrss(i), key, cntx.FirstYear, HistDist)
                            Fish.Add(i + 1, ValHold)
                        Next
                        AgFishCatchProdFSJ.Add(key, Fish)
                    Next

                    'mti 2019/04/29  Flex Hist Base End
                Else 'Forecast *********
                    For Each item In (From s In cntx.GetSlice("SeriesAgFishCatchProdCephalopodsFSJ", cntx.FirstYear, MyBase.Policy)
                                      Join s2 In cntx.GetSlice("SeriesAgFishCatchProdCrustaceansFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesAgFishCatchProdDemersalFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesAgFishCatchProdFreshwaterFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesAgFishCatchProdMarineFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesAgFishCatchProdMolluscsFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s6.Key
                                      Join s7 In cntx.GetSlice("SeriesAgFishCatchProdPelagicFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s7.Key
                                      Join s8 In cntx.GetSlice("SeriesAgFishCatchProdAqAnimalsFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s8.Key
                                      Join s9 In cntx.GetSlice("SeriesAgFishCatchProdAqPlantsFSJ", cntx.FirstYear, MyBase.Policy) On s.Key Equals s9.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value,
                                  Val5 = s5.Value, Val6 = s6.Value, Val7 = s7.Value, Val8 = s8.Value, Val9 = s9.Value)
                        Fish = New Dictionary(Of Integer, Single?)
                        Fish.Add(1, item.Value)
                        Fish.Add(2, item.Val2)
                        Fish.Add(3, item.Val3)
                        Fish.Add(4, item.Val4)
                        Fish.Add(5, item.Val5)
                        Fish.Add(6, item.Val6)
                        Fish.Add(7, item.Val7)
                        Fish.Add(8, item.Val8)
                        Fish.Add(9, item.Val9)

                        AgFishCatchProdFSJ.Add(item.Key, Fish)
                    Next
                End If 'End of if history else forecast

                For Each key In cntx.Regionalization.Keys
                    'Fill holes, make sure no negative values
                    For fc% = 1 To 9
                        If Not AgFishAquaProdFSJ(key)(fc%).HasValue Or AgFishAquaProdFSJ(key)(fc%) <= 0 Then AgFishAquaProdFSJ(key)(fc%) = 0.01
                        If Not AgFishCatchProdFSJ(key)(fc%).HasValue Or AgFishCatchProdFSJ(key)(fc%) <= 0 Then AgFishCatchProdFSJ(key)(fc%) = 0.01
                    Next fc%
                Next

                'Step 3: Break FAOSTAT production into aquaculture and catch by fish category
                Dim Aquashare As Single?
                Dim CAGAQPLANTSCATCH As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CAGAQPLANTSAQUACUL As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each key In cntx.Regionalization.Keys
                    AgFishAquaProd.Add(key, New Dictionary(Of Integer, Single?))
                    AgFishCatchProd.Add(key, New Dictionary(Of Integer, Single?))
                    For fc% = 1 To 9
                        If AgFishAquaProdFSJ(key)(fc%) = 0 And AgFishCatchProdFSJ(key)(fc%) = 0 Then
                            Aquashare = 0.5
                        Else
                            Aquashare = AgFishAquaProdFSJ(key)(fc%) / (AgFishAquaProdFSJ(key)(fc%) + AgFishCatchProdFSJ(key)(fc%))
                        End If
                        AgFishAquaProd(key).Add(fc%, Aquashare * AgFishProd(key)(fc%))
                        AgFishCatchProd(key).Add(fc%, (1 - Aquashare) * AgFishProd(key)(fc%))
                    Next fc%
                    'dsr 2015/08/07 Save results on production of aquatic plants and convert from tons to MMT
                    CAGAQPLANTSCATCH.Add(key, AgFishCatchProd(key)(9) / 1000000)
                    CAGAQPLANTSAQUACUL.Add(key, AgFishAquaProd(key)(9) / 1000000)
                Next

                'Step 4: Read in Global ratios of feed to total domestic supply by fish category
                'Done above before reading in country level data

                'Step 5: Separate catch, imports, and exports into good and bad by fish category and convert from tons to MMT
                'While here also aggregate catch and aquaculture production across categories
                Dim CAGFISHGOODCATCH As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CAGFISHBADCATCH As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CAGFISHAQUACUL As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)

                Dim AgFishBadCatchFAO As Dictionary(Of String, Dictionary(Of Integer, Single?)) = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Dim AgFishGoodCatchFAO As Dictionary(Of String, Dictionary(Of Integer, Single?)) = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Dim AgFishBadImportsSp As Dictionary(Of String, Dictionary(Of Integer, Single?)) = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Dim AgFishGoodImportsSp As Dictionary(Of String, Dictionary(Of Integer, Single?)) = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Dim AgFishBadExportsSp As Dictionary(Of String, Dictionary(Of Integer, Single?)) = New Dictionary(Of String, Dictionary(Of Integer, Single?))
                Dim AgFishGoodExportsSp As Dictionary(Of String, Dictionary(Of Integer, Single?)) = New Dictionary(Of String, Dictionary(Of Integer, Single?))

                For Each key In cntx.Regionalization.Keys
                    CAGFISHGOODCATCH.Add(key, 0)
                    CAGFISHBADCATCH.Add(key, 0)
                    CAGFISHAQUACUL.Add(key, 0)
                    AgFishBadCatchFAO.Add(key, New Dictionary(Of Integer, Single?))
                    AgFishGoodCatchFAO.Add(key, New Dictionary(Of Integer, Single?))
                    AgFishBadImportsSp.Add(key, New Dictionary(Of Integer, Single?))
                    AgFishGoodImportsSp.Add(key, New Dictionary(Of Integer, Single?))
                    AgFishBadExportsSp.Add(key, New Dictionary(Of Integer, Single?))
                    AgFishGoodExportsSp.Add(key, New Dictionary(Of Integer, Single?))
                    For fc% = 1 To 8
                        AgFishBadCatchFAO(key).Add(fc%, AgFishGlobalFeedDomSuppRatio(fc%) * AgFishCatchProd(key)(fc%) / 1000000)
                        AgFishGoodCatchFAO(key).Add(fc%, AgFishCatchProd(key)(fc%) / 1000000 - AgFishBadCatchFAO(key)(fc%))
                        AgFishBadImportsSp(key).Add(fc%, AgFishGlobalFeedDomSuppRatio(fc%) * AgFishImports(key)(fc%) / 1000000)
                        AgFishGoodImportsSp(key).Add(fc%, AgFishImports(key)(fc%) / 1000000 - AgFishBadImportsSp(key)(fc%))
                        AgFishBadExportsSp(key).Add(fc%, AgFishGlobalFeedDomSuppRatio(fc%) * AgFishExports(key)(fc%) / 1000000)
                        AgFishGoodExportsSp(key).Add(fc%, AgFishExports(key)(fc%) / 1000000 - AgFishBadExportsSp(key)(fc%))
                        'Aggregation
                        CAGFISHGOODCATCH(key) = CAGFISHGOODCATCH(key) + AgFishGoodCatchFAO(key)(fc%)
                        CAGFISHBADCATCH(key) = CAGFISHBADCATCH(key) + AgFishBadCatchFAO(key)(fc%)
                        CAGFISHAQUACUL(key) = CAGFISHAQUACUL(key) + AgFishAquaProd(key)(fc%) / 1000000
                    Next fc%
                Next

                'Step 6:
                'dsr 2015/08/11 Balance global trade in good fish, bad fish, aquatic plants, fish oils, and fish meal
                'Sum imports and exports across fish categories
                Dim GlobalImportsGoodFish As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                Dim GlobalExportsGoodFish As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                Dim GlobalImportsBadFish As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                Dim GlobalExportsBadFish As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                Dim GlobalTradeGoodFish As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                Dim GlobalTradeBadFish As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)

                Dim GlobalImportsAqPlants As Single?, GlobalExportsAqPlants As Single?, GlobalImportsFishOils As Single?
                Dim GlobalExportsFishOils As Single?, GlobalImportsFishMeal As Single?, GlobalExportsFishMeal As Single?
                Dim GlobalTradeAqPlants As Single?, GlobalTradeFishOils As Single?, GlobalTradeFishMeal As Single?

                For fc% = 1 To 8
                    GlobalImportsGoodFish.Add(fc%, 0)
                    GlobalExportsGoodFish.Add(fc%, 0)
                    GlobalImportsBadFish.Add(fc%, 0)
                    GlobalExportsBadFish.Add(fc%, 0)
                Next fc%
                GlobalImportsAqPlants = 0
                GlobalExportsAqPlants = 0
                GlobalImportsFishOils = 0
                GlobalExportsFishOils = 0
                GlobalImportsFishMeal = 0
                GlobalExportsFishMeal = 0
                For Each key In cntx.Regionalization.Keys
                    For fc% = 1 To 8
                        GlobalImportsGoodFish(fc%) = GlobalImportsGoodFish(fc%) + AgFishGoodImportsSp(key)(fc%)
                        GlobalExportsGoodFish(fc%) = GlobalExportsGoodFish(fc%) + AgFishGoodExportsSp(key)(fc%)
                        GlobalImportsBadFish(fc%) = GlobalImportsBadFish(fc%) + AgFishBadImportsSp(key)(fc%)
                        GlobalExportsBadFish(fc%) = GlobalExportsBadFish(fc%) + AgFishBadExportsSp(key)(fc%)
                    Next fc%
                    GlobalImportsAqPlants = GlobalImportsAqPlants + CAGAQPLANTSIMPORTS(key)
                    GlobalExportsAqPlants = GlobalExportsAqPlants + CAGAQPLANTSEXPORTS(key)
                    GlobalImportsFishOils = GlobalImportsFishOils + CAGFISHOILSIMPORTS(key)
                    GlobalExportsFishOils = GlobalExportsFishOils + CAGFISHOILSEXPORTS(key)
                    GlobalImportsFishMeal = GlobalImportsFishMeal + CAGFISHMEALIMPORTS(key)
                    GlobalExportsFishMeal = GlobalExportsFishMeal + CAGFISHMEALEXPORTS(key)
                Next
                For fc% = 1 To 8
                    GlobalTradeGoodFish.Add(fc%, CSng(0.5 * (GlobalImportsGoodFish(fc%) + GlobalExportsGoodFish(fc%))))
                    GlobalTradeBadFish.Add(fc%, CSng(0.5 * (GlobalImportsBadFish(fc%) + GlobalExportsBadFish(fc%))))
                Next fc%
                GlobalTradeAqPlants = CSng(0.5 * (GlobalImportsAqPlants + GlobalExportsAqPlants))
                GlobalTradeFishOils = CSng(0.5 * (GlobalImportsFishOils + GlobalExportsFishOils))
                GlobalTradeFishMeal = CSng(0.5 * (GlobalImportsFishMeal + GlobalExportsFishMeal))

                Dim CAGFISHGOODIMPORTS As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CAGFISHGOODEXPORTS As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CAGFISHBADIMPORTS As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CAGFISHBADEXPORTS As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)

                For Each key In cntx.Regionalization.Keys
                    CAGFISHGOODIMPORTS.Add(key, 0)
                    CAGFISHGOODEXPORTS.Add(key, 0)
                    CAGFISHBADIMPORTS.Add(key, 0)
                    CAGFISHBADEXPORTS.Add(key, 0)
                    'dsr 2015/08/12 Adjust country values and aggregate across 'fish' categories here
                    '    be careful when no trade in specific category, as in Bad Fish for Aquatic Animals
                    For fc% = 1 To 8
                        If GlobalTradeGoodFish(fc%) <> 0 Then
                            CAGFISHGOODIMPORTS(key) = AgFishGoodImportsSp(key)(fc%) * (GlobalTradeGoodFish(fc%) / GlobalImportsGoodFish(fc%)) _
                                        + CAGFISHGOODIMPORTS(key)
                            CAGFISHGOODEXPORTS(key) = AgFishGoodExportsSp(key)(fc%) * (GlobalTradeGoodFish(fc%) / GlobalExportsGoodFish(fc%)) _
                                        + CAGFISHGOODEXPORTS(key)
                        End If
                        If GlobalTradeBadFish(fc%) <> 0 Then
                            CAGFISHBADIMPORTS(key) = AgFishBadImportsSp(key)(fc%) * (GlobalTradeBadFish(fc%) / GlobalImportsBadFish(fc%)) _
                                        + CAGFISHBADIMPORTS(key)
                            CAGFISHBADEXPORTS(key) = AgFishBadExportsSp(key)(fc%) * (GlobalTradeBadFish(fc%) / GlobalExportsBadFish(fc%)) _
                                        + CAGFISHBADEXPORTS(key)
                        End If
                    Next fc%
                    CAGAQPLANTSIMPORTS(key) = CAGAQPLANTSIMPORTS(key) * (GlobalTradeAqPlants / GlobalImportsAqPlants)
                    CAGAQPLANTSEXPORTS(key) = CAGAQPLANTSEXPORTS(key) * (GlobalTradeAqPlants / GlobalExportsAqPlants)
                    CAGFISHOILSIMPORTS(key) = CAGFISHOILSIMPORTS(key) * (GlobalTradeFishOils / GlobalImportsFishOils)
                    CAGFISHOILSEXPORTS(key) = CAGFISHOILSEXPORTS(key) * (GlobalTradeFishOils / GlobalExportsFishOils)
                    CAGFISHMEALIMPORTS(key) = CAGFISHMEALIMPORTS(key) * (GlobalTradeFishMeal / GlobalImportsFishMeal)
                    CAGFISHMEALEXPORTS(key) = CAGFISHMEALEXPORTS(key) * (GlobalTradeFishMeal / GlobalExportsFishMeal)
                Next

                'Step 7: Calculate and check domestic supply (apparent consumption). If less than minimum, adjust production so as to not mess up trade balances
                Dim CAGFISHGOODDOMSUPP As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CAGFISHBADDOMSUPP As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CAGAQPLANTSDOMSUPP As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CAGFISHOILSDOMSUPP As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CAGFISHMEALDOMSUPP As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each key In cntx.Regionalization.Keys
                    If CAGFISHGOODCATCH(key) < 0 Or CAGFISHBADCATCH(key) < 0 Or CAGFISHAQUACUL(key) < 0 Then Stop
                    CAGFISHGOODDOMSUPP.Add(key, CAGFISHGOODCATCH(key) + CAGFISHAQUACUL(key) + CAGFISHGOODIMPORTS(key) - CAGFISHGOODEXPORTS(key))
                    If CAGFISHGOODDOMSUPP(key) < 0.01 Then
                        CAGFISHGOODDOMSUPP(key) = 0.01
                        CAGFISHGOODCATCH(key) = CAGFISHGOODDOMSUPP(key) - CAGFISHAQUACUL(key) - CAGFISHGOODIMPORTS(key) + CAGFISHGOODEXPORTS(key)
                    End If
                    CAGFISHBADDOMSUPP.Add(key, CAGFISHBADCATCH(key) + CAGFISHBADIMPORTS(key) - CAGFISHBADEXPORTS(key))
                    If CAGFISHBADDOMSUPP(key) < 0.000001 Then
                        CAGFISHBADDOMSUPP(key) = 0.000001
                        CAGFISHBADCATCH(key) = CAGFISHBADDOMSUPP(key) - CAGFISHBADIMPORTS(key) + CAGFISHBADEXPORTS(key)
                    End If
                    CAGAQPLANTSDOMSUPP.Add(key, CAGAQPLANTSCATCH(key) + CAGAQPLANTSAQUACUL(key) + CAGAQPLANTSIMPORTS(key) - CAGAQPLANTSEXPORTS(key))
                    If CAGAQPLANTSDOMSUPP(key) < 0.000001 Then
                        CAGAQPLANTSDOMSUPP(key) = 0.000001
                        CAGAQPLANTSCATCH(key) = CAGAQPLANTSDOMSUPP(key) - CAGAQPLANTSAQUACUL(key) - CAGAQPLANTSIMPORTS(key) + CAGAQPLANTSEXPORTS(key)
                    End If
                    CAGFISHOILSDOMSUPP.Add(key, CAGFISHOILSPROD(key) + CAGFISHOILSIMPORTS(key) - CAGFISHOILSEXPORTS(key))
                    If CAGFISHOILSDOMSUPP(key) < 0.000001 Then
                        CAGFISHOILSDOMSUPP(key) = 0.000001
                        CAGFISHOILSPROD(key) = CAGFISHOILSDOMSUPP(key) - CAGFISHOILSIMPORTS(key) + CAGFISHOILSEXPORTS(key)
                    End If
                    CAGFISHMEALDOMSUPP.Add(key, CAGFISHMEALPROD(key) + CAGFISHMEALIMPORTS(key) - CAGFISHMEALEXPORTS(key))
                    If CAGFISHMEALDOMSUPP(key) < 0.000001 Then
                        CAGFISHMEALDOMSUPP(key) = 0.000001
                        CAGFISHMEALPROD(key) = CAGFISHMEALDOMSUPP(key) - CAGFISHMEALIMPORTS(key) + CAGFISHMEALEXPORTS(key)
                    End If

                    'Use fish value for aggregate fish category until we deal with plants and oils
                    CAGFISHCATCH.Add(key, CAGFISHGOODCATCH(key) + CAGFISHBADCATCH(key))
                    CAQUACUL.Add(key, CAGFISHAQUACUL(key))
                    AGMFish.Add(key, CAGFISHGOODIMPORTS(key) + CAGFISHBADIMPORTS(key))
                    AGXFish.Add(key, CAGFISHGOODEXPORTS(key) + CAGFISHBADEXPORTS(key))
                    CAGDOMSUPP_Fish.Add(key, CAGFISHCATCH(key) + CAQUACUL(key) + AGMFish(key) - AGXFish(key))
                Next
            End Sub


            'jrs 2021/02/11
            Dim GYIELDINT() As Single

            'jrs 2021/02/12 function to calculate average yield intensities across world regions using G-lists
            Private Sub ComputeAvgYields()
                ReDim GYIELDINT(18)

                'Load G-List
                Using db As IFsContext = New IFsContext()
                    '            Dim sql As String
                    '            sql = "SELECT GList.Region AS GroupId, RegionalizationFull.Country" &
                    '" FROM GListId, GList, GroupIDNewFull, GroupingFull, RegionalizationFull" &
                    '" WHERE GListId.Code = GList.GListCode" &
                    '" AND GList.Region = GroupIDNewFull.GroupName" &
                    '" AND GroupIDNewFull.GroupNum = GroupingFull.GroupNo" &
                    '" AND GroupingFull.RegionNo = RegionalizationFull.NewRegion" &
                    '" AND GListId.Description=""World_AgrSubregions""" &
                    '" ORDER BY GList.Region, RegionalizationFull.Country"
                    Dim rsGrp = (From GLI In db.GListID
                                 Join GL In db.GList On GLI.Code Equals GL.GListCode
                                 Join GI In db.GroupIDNewFull On GL.RegionName Equals GI.GroupName
                                 Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                 Join R In db.RegionalizationFull On G.RegionId Equals R.RegionId
                                 Where GLI.Description = "World_AgrSubregions"
                                 Order By GL.RegionName, R.CountryName
                                 Select GroupId = GL.RegionName, Country = R.CountryName)

                    Dim GroupIdx As Integer
                    GroupIdx = 1
                    Dim sPrevGroup As String = ""
                    Dim GroupVal As Single
                    Dim GroupCount As Integer
                    GroupCount = 0
                    GroupVal = 0

                    'Iterate for each group member of the G-List
                    For Each item In rsGrp
                        'For each group we need get the members
                        If sPrevGroup > "" And sPrevGroup <> item.GroupId Then
                            If GroupCount > 0 Then
                                GYIELDINT(GroupIdx) = GroupVal / GroupCount
                            Else
                                GYIELDINT(GroupIdx) = 0
                            End If

                            GroupIdx = GroupIdx + 1
                            GroupCount = 0
                            GroupVal = 0
                        End If

                        'Then compute the average of the available data
                        If CYIELDINT(item.Country).HasValue Then
                            GroupVal = GroupVal + CSng(CYIELDINT(item.Country))
                            GroupCount = GroupCount + 1
                        End If

                        sPrevGroup = item.GroupId
                    Next

                    'Last Group
                    If GroupCount > 0 Then
                        GYIELDINT(GroupIdx) = GroupVal / GroupCount
                    Else
                        GYIELDINT(GroupIdx) = 0
                    End If

                End Using

            End Sub

            'jrs 2021/02/11 function to use previously calculated average yield intensities to fill in data points for countries with missing data. For example belgium is assigned average yield intensity for western europe, whereas somalia is assigned average yield intensity for east africa
            Private Sub FillHoles()
                'Load G-List
                Using db As IFsContext = New IFsContext()
                    '     Dim sql As String
                    '     sql = "SELECT GList.Region AS GroupId, RegionalizationFull.Country" &
                    '" FROM GListId, GList, GroupIDNewFull, GroupingFull, RegionalizationFull" &
                    '" WHERE GListId.Code = GList.GListCode" &
                    '" AND GList.Region = GroupIDNewFull.GroupName" &
                    '" AND GroupIDNewFull.GroupNum = GroupingFull.GroupNo" &
                    '" AND GroupingFull.RegionNo = RegionalizationFull.NewRegion" &
                    '" AND GListId.Description=""World_AgrSubregions""" &
                    '" ORDER BY GList.Region, RegionalizationFull.Country"
                    Dim rsGrp = (From GLI In db.GListID
                                 Join GL In db.GList On GLI.Code Equals GL.GListCode
                                 Join GI In db.GroupIDNewFull On GL.RegionName Equals GI.GroupName
                                 Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                 Join R In db.RegionalizationFull On G.RegionId Equals R.RegionId
                                 Where GLI.Description = "World_AgrSubregions"
                                 Order By GL.RegionName, R.CountryName
                                 Select GroupId = GL.RegionName, Country = R.CountryName)

                    Dim GroupIdx As Integer
                    GroupIdx = 1
                    Dim sPrevGroup As String = ""
                    'Iterate for each group member of the G-List
                    For Each item In rsGrp
                        'For each group we need get the members
                        If sPrevGroup > "" And sPrevGroup <> item.GroupId Then
                            GroupIdx = GroupIdx + 1
                        End If

                        'Then compute the average of the available data
                        If Not CYIELDINT(item.Country).HasValue Then
                            CYIELDINT(item.Country) = GYIELDINT(GroupIdx)
                        End If

                        sPrevGroup = item.GroupId
                    Next
                End Using
            End Sub

        End Class

    End Module

End Namespace
