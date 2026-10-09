Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data
'jrs 2023/06/01
Imports Microsoft.Data.Sqlite

Namespace Models
    Partial Public Module StFailure
        <RebuildsVariable("SFINSTABALL", AggregatorType:=GetType(StFailAggregator))>
        <RebuildsVariable("SFINSTABMAG", AggregatorType:=GetType(StFailAggregator))>
        <RebuildsVariable("SFINTLWARALL", AggregatorType:=GetType(StFailAggregator))>
        <RebuildsVariable("SFINTLWARMAG", AggregatorType:=GetType(StFailAggregator))>
        <RebuildsVariable("SFCIVILWARUCDP", AggregatorType:=GetType(StFailAggregator))>
        <RebuildsVariable("DIPLOEMBASSY", AggregatorType:=GetType(StFailAggregator))>
        <RebuildsVariable("DIPLOIGO", AggregatorType:=GetType(StFailAggregator))>
        <RebuildsVariable("DIPLOTREATY", AggregatorType:=GetType(StFailAggregator))>
        <RebuildsVariable("diplodistancetoeu", AggregatorType:=GetType(StFailAggregator))>
        <RebuildsVariable("DIPLOINGOPROGRAM", AggregatorType:=GetType(StFailAggregator))>
        <RebuildsVariable("DIPLOINGOADVOCACY", AggregatorType:=GetType(StFailAggregator))>
        <RebuildsVariable("HTCGRI", AggregatorType:=GetType(StFailAggregator))>
        <RebuildsVariable("HTCTPI", AggregatorType:=GetType(StFailAggregator))>
        <RebuildsVariable("SFINSECUREPOPIMP", AggregatorType:=GetType(StFailAggregator))> 'dsb 2026/05/05
        <RebuildsVariable("SFCIVILWARPOPIMP", AggregatorType:=GetType(StFailAggregator))> 'dsb 2025/12/17
        <RebuildsVariable("SFCIVILWARPARDEE", AggregatorType:=GetType(StFailAggregator))> 'dsb 2026/01/06
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CPOP")>
        Public Iterator Function RebuildStFailure(context As IRebuildContext) As IEnumerable(Of IDataframe)
            'Yield context.Query("ENVPM2PT5").Filter(BaseYearOnly).Execute()
            Yield context.Query("SFINSTABALL").Filter(BaseYearOnly).Execute()
            Yield context.Query("SFINSTABMAG").Filter(BaseYearOnly).Execute()
            Yield context.Query("SFINTLWARALL").Filter(BaseYearOnly).Execute()
            Yield context.Query("SFINTLWARMAG").Filter(BaseYearOnly).Execute()
            'jrs 2024/05/17
            Yield context.Query("SFCIVILWARUCDP").Filter(BaseYearOnly).Execute()
            Yield context.Query("SFINSECUREPOPIMP").Filter(BaseYearOnly).Execute() 'dsb 2026/05/05
            Yield context.Query("SFCIVILWARPOPIMP").Filter(BaseYearOnly).Execute() 'dsb 2025/12/17
            Yield context.Query("SFCIVILWARPARDEE").Filter(BaseYearOnly).Execute() 'dsb 2026/01/06

            Dim dftmp As IDataframe
            Dim R As Short
            Dim yrIdx As Integer
            dftmp = context.Query("DIPLOEMBASSY").Filter(BaseYearOnly).Execute()
            If context.HistoricMode Then
                Dim dfDIPLOEMBASSY As Dataframe = Dataframe.Create("DIPLOEMBASSY", context, context.FirstYear, context.LastYear, 0)

                For R = 1 To CShort(NR)
                    yrIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR))
                    For y = FirstYear To LastYear
                        dfDIPLOEMBASSY(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(yrIdx)
                    Next
                Next
                Yield dfDIPLOEMBASSY

            Else
                Yield dftmp
            End If
            Yield context.Query("DIPLOIGO").Filter(BaseYearOnly).Execute()
            Yield context.Query("DIPLOTREATY").Filter(BaseYearOnly).Execute()
            'jrs 2019/11/26
            dftmp = context.Query("diplodistancetoeu").Filter(BaseYearOnly).Execute()
            Dim dfdiplodistancetoeu As Dataframe = Dataframe.Create("diplodistancetoeu", context, context.FirstYear, context.LastYear, 0)

            For R = 1 To CShort(NR)
                yrIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR))
                For y = FirstYear To LastYear
                    dfdiplodistancetoeu(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(yrIdx)
                Next
            Next
            Yield dfdiplodistancetoeu
            Yield context.Query("DIPLOINGOPROGRAM").Filter(BaseYearOnly).Execute()
            Yield context.Query("DIPLOINGOADVOCACY").Filter(BaseYearOnly).Execute()
            Yield context.Query("HTCGRI").Filter(BaseYearOnly).Execute()
            Yield context.Query("HTCTPI").Filter(BaseYearOnly).Execute()
        End Function

        <RebuildsVariable("DIPLOEMBASSYMAX")>
        <RebuildsVariable("DIPLOIGOMAX")>
        <RebuildsVariable("DIPLOTREATYMAX")>
        Public Iterator Function RebuildDIPLOMAX(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Dim DIPLOEMBASSYMAX As Single?
            Dim DIPLOIGOMAX As Single?
            Dim DIPLOTREATYMAX As Single?

            If context.HistoricMode Then
                Dim fkey = (From c In context.GetSlice("SeriesDiploEmbassyMaxGlobal", "MostRecent") Where (Not c.Value Is Nothing) Select c).FirstOrDefault().Key
                DIPLOEMBASSYMAX = (From c In context.GetSlice("SeriesDiploEmbassyMaxGlobal", context.FirstYear) Where c.Key = fkey Select c).FirstOrDefault().Value
                fkey = (From c In context.GetSlice("SeriesDiploIGOWeightMax", "MostRecent") Where (Not c.Value Is Nothing) Select c).FirstOrDefault().Key
                DIPLOIGOMAX = (From c In context.GetSlice("SeriesDiploIGOWeightMax", context.FirstYear) Where c.Key = fkey Select c).FirstOrDefault().Value
                fkey = (From c In context.GetSlice("SeriesDiploTreatyWeightedSign1Rat2Max", "MostRecent") Where (Not c.Value Is Nothing) Select c).FirstOrDefault().Key
                DIPLOTREATYMAX = (From c In context.GetSlice("SeriesDiploTreatyWeightedSign1Rat2Max", context.FirstYear) Where c.Key = fkey Select c).FirstOrDefault().Value
            Else
                Dim fkey = (From c In context.GetSlice("SeriesDiploEmbassyMaxGlobal", "MostRecent") Where (Not c.Value Is Nothing) Select c).FirstOrDefault().Key
                DIPLOEMBASSYMAX = (From c In context.GetSlice("SeriesDiploEmbassyMaxGlobal", context.BaseYear) Where c.Key = fkey Select c).FirstOrDefault().Value
                'jrs 2020/10/07
                If Not DIPLOEMBASSYMAX.HasValue Then
                    DIPLOEMBASSYMAX = (From c In context.GetSlice("SeriesDiploEmbassyMaxGlobal", "MostRecent") Where c.Key = fkey Select c).FirstOrDefault().Value
                End If
                fkey = (From c In context.GetSlice("SeriesDiploIGOWeightMax", "MostRecent") Where (Not c.Value Is Nothing) Select c).FirstOrDefault().Key
                DIPLOIGOMAX = (From c In context.GetSlice("SeriesDiploIGOWeightMax", context.BaseYear) Where c.Key = fkey Select c).FirstOrDefault().Value
                'jrs 2020/10/07
                If Not DIPLOIGOMAX.HasValue Then
                    DIPLOIGOMAX = (From c In context.GetSlice("SeriesDiploIGOWeightMax", "MostRecent") Where c.Key = fkey Select c).FirstOrDefault().Value
                End If
                fkey = (From c In context.GetSlice("SeriesDiploTreatyWeightedSign1Rat2Max", "MostRecent") Where (Not c.Value Is Nothing) Select c).FirstOrDefault().Key
                DIPLOTREATYMAX = (From c In context.GetSlice("SeriesDiploTreatyWeightedSign1Rat2Max", context.BaseYear) Where c.Key = fkey Select c).FirstOrDefault().Value
                'jrs 2020/10/07
                If Not DIPLOTREATYMAX.HasValue Then
                    DIPLOTREATYMAX = (From c In context.GetSlice("SeriesDiploTreatyWeightedSign1Rat2Max", "MostRecent") Where c.Key = fkey Select c).FirstOrDefault().Value
                End If
            End If

            Dim df As Dataframe = Dataframe.Create("DIPLOEMBASSYMAX", context, context.FirstYear, Nothing)
            df(context.FirstYear) = DIPLOEMBASSYMAX
            Yield df

            Dim df2 As Dataframe = Dataframe.Create("DIPLOIGOMAX", context, context.FirstYear, Nothing)
            df2(context.FirstYear) = DIPLOIGOMAX
            Yield df2

            Dim df3 As Dataframe = Dataframe.Create("DIPLOTREATYMAX", context, context.FirstYear, Nothing)
            df3(context.FirstYear) = DIPLOTREATYMAX
            Yield df3

        End Function

        Class StFailAggregator
            Inherits DefaultSumAggregator
            Private Property CSFINSTABALL As IDictionary(Of String, Single?)
            Private Property CSFINSTABMAG As IDictionary(Of String, Single?)
            Private Property CSFINTLWARALL As IDictionary(Of String, Single?)
            Private Property CSFINTLWARMAG As IDictionary(Of String, Single?) 'jrs 2016/01/18 changed to public
            'jrs 2024/05/17
            Private Property CSFCIVILWARUCDP As IDictionary(Of String, Single?)
            Private Property CSFINSTABY1 As IDictionary(Of String, Single?)
            Private Property CSFINTLWARY1 As IDictionary(Of String, Single?)
            Private Property CSFCONSOLIDATEDALL As IDictionary(Of String, Single?)
            Private Property CSFCONSOLIDATEDMAG As IDictionary(Of String, Single?)
            Private Property CSFCONSOLIDATEDY1 As IDictionary(Of String, Single?)
            Private Property CSFINTHISLON As IDictionary(Of String, Single?)
            Private Property CSFINTHISREC As IDictionary(Of String, Single?)
            Private Property CSFINSHISLON As IDictionary(Of String, Single?)
            Private Property CSFINSHISREC As IDictionary(Of String, Single?)
            Private Property CDIPLOEMBASSY As IDictionary(Of String, Single?)
            Private Property CDIPLOIGO As IDictionary(Of String, Single?)
            Private Property CDIPLOTREATY As IDictionary(Of String, Single?)
            Private Property CDIPLODISTANCE As IDictionary(Of String, Single?)
            Private Property CDIPLOINGOPROGRAM As IDictionary(Of String, Single?)
            Private Property CDIPLOINGOADVOCACY As IDictionary(Of String, Single?)
            Private Property CHTCGRI As IDictionary(Of String, Single?)
            Private Property CHTCTPI As IDictionary(Of String, Single?)
            Private Property processed As Boolean
            Private Property CGDPPCP As IDictionary(Of String, Single?)
            Private Property CPOP As IDictionary(Of String, Single?)
            Private Property CSFINSECUREPOPIMP As IDictionary(Of String, Single?) 'dsb 2026/05/05
            Private Property CSFCIVILWARPOPIMP As IDictionary(Of String, Single?) 'dsb 2025/12/17
            Private Property CSFCIVILWARPARDEE As IDictionary(Of String, Single?) 'dsb 2026/01/06

            <ExportsAsset("CSFINTLWARMAG")>
            <ExportsAsset("CSFINSECUREPOPIMP")> 'dsb 2026/05/05
            <ExportsAsset("CSFCIVILWARPOPIMP")> 'dsb 2025/12/17
            <ExportsAsset("CSFCIVILWARPARDEE")> 'dsb 2026/1/8
            Private Sub PreProcess(cntx As IRebuildFacade)
                If Me.processed Then
                    Return
                End If

                Dim objWriter = New System.IO.StreamWriter(cntx.PutFdrvRun + "progress.txt", append:=True)
                objWriter.WriteLine("Processing Data on State Failure - Please Wait")
                objWriter.Close()

                CPOP = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
                CGDPPCP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
                Me.CSFINSTABALL = New Dictionary(Of String, Single?)
                Me.CSFINSTABMAG = New Dictionary(Of String, Single?)
                Me.CSFINTLWARALL = New Dictionary(Of String, Single?)
                Me.CSFINTLWARMAG = New Dictionary(Of String, Single?)
                'jrs 2024/05/17
                Me.CSFCIVILWARUCDP = New Dictionary(Of String, Single?)
                Me.CSFINSECUREPOPIMP = New Dictionary(Of String, Single?) 'dsb 2026/05/05
                Me.CSFCIVILWARPOPIMP = New Dictionary(Of String, Single?) 'dsb 2025/12/17
                Me.CSFCIVILWARPARDEE = New Dictionary(Of String, Single?) 'dsb 2026/01/06

                Me.CSFINSTABY1 = New Dictionary(Of String, Single?)
                Me.CSFINTLWARY1 = New Dictionary(Of String, Single?)
                Me.CSFCONSOLIDATEDALL = New Dictionary(Of String, Single?)
                Me.CSFCONSOLIDATEDMAG = New Dictionary(Of String, Single?)
                Me.CSFCONSOLIDATEDY1 = New Dictionary(Of String, Single?)
                Me.CSFINTHISLON = New Dictionary(Of String, Single?)
                Me.CSFINTHISREC = New Dictionary(Of String, Single?)
                Me.CSFINSHISLON = New Dictionary(Of String, Single?)
                Me.CSFINSHISREC = New Dictionary(Of String, Single?)
                Me.CDIPLOEMBASSY = New Dictionary(Of String, Single?)
                Me.CDIPLOIGO = New Dictionary(Of String, Single?)
                Me.CDIPLOTREATY = New Dictionary(Of String, Single?)
                Me.CDIPLODISTANCE = New Dictionary(Of String, Single?)
                Me.CDIPLOINGOPROGRAM = New Dictionary(Of String, Single?)
                Me.CDIPLOINGOADVOCACY = New Dictionary(Of String, Single?)
                Me.CHTCGRI = New Dictionary(Of String, Single?)
                Me.CHTCTPI = New Dictionary(Of String, Single?)
                Dim yrsEthnicWarMag As Short() = cntx.GetYears("SeriesSFPITFEthnicWarMag").OrderBy(Function(y) y).ToArray()
                Dim EthnicWarMagTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesSFPITFEthnicWarMag", yrsEthnicWarMag)
                Dim yrsGenocideMag As Short() = cntx.GetYears("SeriesSFPITFGenocideMag").OrderBy(Function(y) y).ToArray()
                Dim GenocideMagTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesSFPITFGenocideMag", yrsGenocideMag)
                Dim yrsRevolWarMag As Short() = cntx.GetYears("SeriesSFPITFRevolWarMag").OrderBy(Function(y) y).ToArray()
                Dim RevolWarMagTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesSFPITFRevolWarMag", yrsRevolWarMag)
                Dim yrsRegTranMag As Short() = cntx.GetYears("SeriesSFPITFRegTranMag").OrderBy(Function(y) y).ToArray()
                Dim RegTranMagTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesSFPITFRegTranMag", yrsRegTranMag)
                Dim yrsEthnicWarEv As Short() = cntx.GetYears("SeriesSFPITFEthnicWarEv").OrderBy(Function(y) y).ToArray()
                Dim EthnicWarEvTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesSFPITFEthnicWarEv", yrsEthnicWarEv)
                Dim yrsGenocideEv As Short() = cntx.GetYears("SeriesSFPITFGenocideEv").OrderBy(Function(y) y).ToArray()
                Dim GenocideEvTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesSFPITFGenocideEv", yrsGenocideEv)
                Dim yrsRevolWarEv As Short() = cntx.GetYears("SeriesSFPITFRevolWarEv").OrderBy(Function(y) y).ToArray()
                Dim RevolWarEvTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesSFPITFRevolWarEv", yrsRevolWarEv)
                Dim yrsRegTranEv As Short() = cntx.GetYears("SeriesSFPITFRegTranEv").OrderBy(Function(y) y).ToArray()
                Dim RegTranEvTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesSFPITFRegTranEv", yrsRegTranEv)
                Dim yrsConsolidatedEv As Short() = cntx.GetYears("SeriesSFPITFConsolidatedEv").OrderBy(Function(y) y).ToArray()
                Dim ConsolidatedEvTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesSFPITFConsolidatedEv", yrsConsolidatedEv)
                Dim yrsConsolidatedMag As Short() = cntx.GetYears("SeriesSFPITFConsolidatedMag").OrderBy(Function(y) y).ToArray()
                Dim ConsolidatedMagTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesSFPITFConsolidatedMag", yrsConsolidatedMag)
                'jrs 2024/05/17
                Dim yrsConsolidatedUCDP As Short() = cntx.GetYears("SeriesSFUCDPConsolidatedEv").OrderBy(Function(y) y).ToArray()
                Dim ConsolidatedUCDPTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesSFUCDPConsolidatedEv", yrsConsolidatedUCDP)
                'dsb 2025/12/17 *** start ***
                Dim yrsCivilWarPopImp As Short() = cntx.GetYears("SeriesSFCivilWarPardeeImpPopCivilWar").OrderBy(Function(y) y).ToArray()
                Dim CivilWarPopImpTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesSFCivilWarPardeeImpPopCivilWar", yrsCivilWarPopImp)
                Dim baseyearCWPopImp As Short = CShort(cntx.BaseYear)
                For Each key In cntx.Regionalization.Keys
                    Dim val As Single? = Nothing
                    If yrsCivilWarPopImp.Contains(baseyearCWPopImp) Then
                        val = CivilWarPopImpTbl(key)(Array.IndexOf(yrsCivilWarPopImp, baseyearCWPopImp))
                    End If
                    CSFCIVILWARPOPIMP.Add(key, val)
                Next 'dsb 2025/12/17 *** end ***
                'dsb 2026/05/05 *** start ***
                Dim yrsInsecurePopImp As Short() = cntx.GetYears("SeriesSFCivilWarPardeeImpPop").OrderBy(Function(y) y).ToArray()
                Dim InsecurePopImpTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesSFCivilWarPardeeImpPop", yrsInsecurePopImp)
                Dim baseyearInsecurePopImp As Short = CShort(cntx.BaseYear)
                For Each key In cntx.Regionalization.Keys
                    Dim val As Single? = Nothing
                    If yrsInsecurePopImp.Contains(baseyearInsecurePopImp) Then
                        val = InsecurePopImpTbl(key)(Array.IndexOf(yrsInsecurePopImp, baseyearInsecurePopImp))
                    End If
                    CSFINSECUREPOPIMP.Add(key, val)
                Next
                'DSB 2026/1/8 *** start ***
                'For Each item In (From s In cntx.GetSlice("SeriesSFCivilWarPardeeOnset", cntx.FirstYear, MyBase.Policy)
                '                  Select Key = s.Key, Value = s.Value)
                '    CSFCIVILWARPARDEE.Add(item.Key, item.Value)
                'Next
                Dim yrsCivilWarPardeeConflictYear As Short() = cntx.GetYears("SeriesSFCivilWarPardeeConflictYear").OrderBy(Function(y) y).ToArray()
                Dim CivilWarPardeeConflictYearTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesSFCivilWarPardeeConflictYear", yrsCivilWarPardeeConflictYear)
                'DSB 2026/1/8 *** end ***

                'ConsolidatedMag
                'ConsolidatedEv
                Dim ValHold As Single?
                Dim ValCDIPLOINGOPROGRAM As Single?
                Dim ValCDIPLOINGOADVOCACY As Single?

                'jrs 2023/06/01 *** start ****
                Dim YearColsAdded = False

                'mti 2015/09/08 - start
                Dim LastYrComp% = BaseYear
                Dim LastYrEthWar% = yrsEthnicWarMag(yrsEthnicWarMag.Length - 1)
                Dim LastYrCons% = yrsConsolidatedMag(yrsConsolidatedMag.Length - 1)
                Dim LastYrConsEv% = yrsConsolidatedEv(yrsConsolidatedEv.Length - 1)

                Dim db As SqliteConnection
                db = New SqliteConnection("DataSource=" & PutFdrvRun & "ifsHistSeries.db")
                db.Open()
                Dim cmd As SqliteCommand = db.CreateCommand

                cmd.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
                cmd.ExecuteNonQuery()

                If LastYrEthWar% < LastYrComp% Then


                    AddYearFields(cmd, "SeriesSFPITFEthnicWarMag", yrsEthnicWarMag, LastYrEthWar% + 1, LastYrComp%)
                    'jrs 2023/09/01 need to refresh yrsEthnicWarMag
                    yrsEthnicWarMag = cntx.GetYears("SeriesSFPITFEthnicWarMag").OrderBy(Function(y) y).ToArray()
                    EthnicWarMagTbl = cntx.GetDice("SeriesSFPITFEthnicWarMag", yrsEthnicWarMag)

                    YearColsAdded = True

                End If

                Dim LastYrEthWarEv% = yrsEthnicWarEv(yrsEthnicWarEv.Length - 1)

                If LastYrEthWarEv% < LastYrComp% Then
                    AddYearFields(cmd, "SeriesSFPITFEthnicWarEv", yrsEthnicWarEv, LastYrEthWarEv% + 1, LastYrComp%)
                    'jrs 2023/09/01 need to refresh yrsEthnicWarMag
                    yrsEthnicWarEv = cntx.GetYears("SeriesSFPITFEthnicWarEv").OrderBy(Function(y) y).ToArray()
                    EthnicWarEvTbl = cntx.GetDice("SeriesSFPITFEthnicWarEv", yrsEthnicWarEv)

                    YearColsAdded = True

                End If

                Dim LastYrGen% = yrsGenocideMag(yrsGenocideMag.Length - 1)


                If LastYrGen% < LastYrComp% Then
                    AddYearFields(cmd, "SeriesSFPITFGenocideMag", yrsGenocideMag, LastYrGen% + 1, LastYrComp%)
                    'jrs 2023/09/01
                    yrsGenocideMag = cntx.GetYears("SeriesSFPITFGenocideMag").OrderBy(Function(y) y).ToArray()
                    GenocideMagTbl = cntx.GetDice("SeriesSFPITFGenocideMag", yrsGenocideMag)

                    YearColsAdded = True
                End If

                Dim LastYrGenEv% = yrsGenocideEv(yrsGenocideEv.Length - 1)
                If LastYrGenEv% < LastYrComp% Then
                    AddYearFields(cmd, "SeriesSFPITFGenocideEv", yrsGenocideEv, LastYrGenEv% + 1, LastYrComp%)
                    'jrs 2023/09/02
                    yrsGenocideEv = cntx.GetYears("SeriesSFPITFGenocideEv").OrderBy(Function(y) y).ToArray()
                    GenocideEvTbl = cntx.GetDice("SeriesSFPITFGenocideEv", yrsGenocideEv)

                    YearColsAdded = True
                End If

                Dim LastYrReg% = yrsRegTranMag(yrsRegTranMag.Length - 1)


                If LastYrReg% < LastYrComp% Then
                    AddYearFields(cmd, "SeriesSFPITFRegTranMag", yrsRegTranMag, LastYrReg% + 1, LastYrComp%)
                    'jrs 2023/09/01
                    yrsRegTranMag = cntx.GetYears("SeriesSFPITFRegTranMag").OrderBy(Function(y) y).ToArray()
                    RegTranMagTbl = cntx.GetDice("SeriesSFPITFRegTranMag", yrsRegTranMag)

                    YearColsAdded = True
                End If

                Dim LastYrRegEv% = yrsRegTranEv(yrsRegTranEv.Length - 1)


                If LastYrRegEv% < LastYrComp% Then
                    AddYearFields(cmd, "SeriesSFPITFRegTranEv", yrsRegTranEv, LastYrRegEv% + 1, LastYrComp%)
                    'jrs 2023/09/02
                    yrsRegTranEv = cntx.GetYears("SeriesSFPITFRegTranEv").OrderBy(Function(y) y).ToArray()
                    RegTranEvTbl = cntx.GetDice("SeriesSFPITFRegTranEv", yrsRegTranEv)

                    YearColsAdded = True
                End If


                Dim LastYrRevol% = yrsRevolWarMag(yrsRevolWarMag.Length - 1)


                If LastYrRevol% < LastYrComp% Then
                    AddYearFields(cmd, "SeriesSFPITFRevolWarMag", yrsRevolWarMag, LastYrRevol% + 1, LastYrComp%)
                    'jrs 2023/09/01
                    yrsRevolWarMag = cntx.GetYears("SeriesSFPITFRevolWarMag").OrderBy(Function(y) y).ToArray()
                    RevolWarMagTbl = cntx.GetDice("SeriesSFPITFRevolWarMag", yrsRevolWarMag)

                    YearColsAdded = True
                End If

                Dim LastYrRevolEv% = yrsRevolWarEv(yrsRevolWarEv.Length - 1)


                If LastYrRevolEv% < LastYrComp% Then
                    AddYearFields(cmd, "SeriesSFPITFRevolWarEv", yrsRevolWarEv, LastYrRevolEv% + 1, LastYrComp%)
                    'jrs 2023/09/01
                    yrsRevolWarEv = cntx.GetYears("SeriesSFPITFRevolWarEv").OrderBy(Function(y) y).ToArray()
                    RevolWarEvTbl = cntx.GetDice("SeriesSFPITFRevolWarEv", yrsRevolWarEv)

                    YearColsAdded = True
                End If

                Dim yrsInternalWarEv As Short() = cntx.GetYears("SeriesSFPITFInternalWarEv").OrderBy(Function(y) y).ToArray()
                Dim LastYrIntWarEv% = yrsInternalWarEv(yrsInternalWarEv.Length - 1)


                If LastYrIntWarEv% < LastYrComp% Then
                    AddYearFields(cmd, "SeriesSFPITFInternalWarEv", yrsInternalWarEv, LastYrIntWarEv% + 1, LastYrComp%)
                    'jrs 2023/09/01 
                    yrsInternalWarEv = cntx.GetYears("SeriesSFPITFInternalWarEv").OrderBy(Function(y) y).ToArray()

                    YearColsAdded = True
                End If


                If LastYrCons% < LastYrComp% Then
                    AddYearFields(cmd, "SeriesSFPITFConsolidatedMag", yrsConsolidatedMag, LastYrCons% + 1, LastYrComp%)
                    'jrs 2023/09/01
                    yrsConsolidatedMag = cntx.GetYears("SeriesSFPITFConsolidatedMag").OrderBy(Function(y) y).ToArray()
                    ConsolidatedMagTbl = cntx.GetDice("SeriesSFPITFConsolidatedMag", yrsConsolidatedMag)

                    YearColsAdded = True
                End If

                If LastYrConsEv% < LastYrComp% Then
                    AddYearFields(cmd, "SeriesSFPITFConsolidatedEv", yrsConsolidatedEv, LastYrConsEv% + 1, LastYrComp%)
                    'jrs 2023/09/01
                    yrsConsolidatedEv = cntx.GetYears("SeriesSFPITFConsolidatedEv").OrderBy(Function(y) y).ToArray()
                    ConsolidatedEvTbl = cntx.GetDice("SeriesSFPITFConsolidatedEv", yrsConsolidatedEv)

                    YearColsAdded = True
                End If


                '    CSFEthnicWarTbl.Close
                '    Set CSFEthnicWarTbl = Nothing

                'mti 2015/09/08 - end


                If YearColsAdded Then

                    'jrs 2023/06/02 not necessary here saved in Health for Smoking Impact
                    'System.IO.File.Copy(cntx.PutFdrvRun + "IFsHistSeries.db", DataFileDir$ + "IfsHistSeries.db", True)

                End If


                'mti 2015/06/25 - End
                'jrs 2023/06/01 *** end ****

                If cntx.HistoricMode Then
                    Dim BegYear% = 1960
                    Dim YearstoUse% = 5
                    Dim yearfield$ = ""
                    For Each key In cntx.Regionalization.Keys
                        'For Each item In (From s In cntx.GetSlice("SeriesSFPITFEthnicWarMag", "Earliest")
                        'Select Case Key = s.Key, Value = s.Value)
                        Dim CIntlWarAll = 0
                        Dim CInstabAll = 0
                        Dim CConsolidatedAll = 0
                        Dim Component1 As Single?
                        Dim Component2 As Single?
                        Dim Component3 As Single?
                        Dim SumComponent As Single?
                        Dim CIntlWarMag = 0
                        Dim CInstabMag = 0
                        Dim CConsolidatedMag = 0
                        Dim ValCSFINTLWARMAG As Single? = 0
                        Dim ValCSFINSTABMAG As Single? = 0
                        Dim ValCSFCONSOLIDATEDMAG As Single? = 0
                        'jrs 2024/05/17
                        Dim CUCDPWarAll = 0
                        'dsb 2026/1/9 
                        Dim CPardeeOnsetAll = 0
                        Dim ComponentPardee As Single?

                        For YCount% = 1 To YearstoUse%
                            yearfield$ = CStr(BegYear% - 1 + YCount%).Trim
                            Component1 = EthnicWarMagTbl(key)(CShort(yearfield$) - yrsEthnicWarMag(0))
                            If Not Component1.HasValue Then
                                Component1 = 0
                            Else
                                CIntlWarMag = CIntlWarMag + 1
                            End If
                            Component2 = If(GenocideMagTbl(key)(CShort(yearfield$) - yrsGenocideMag(0)), 0)
                            Component3 = If(RevolWarMagTbl(key)(CShort(yearfield$) - yrsRevolWarMag(0)), 0)
                            If Component1 + Component2 + Component3 > 0.01 Then
                                SumComponent = Component1 + Component2 + Component3
                            Else
                                SumComponent = 0
                            End If
                            ValCSFINTLWARMAG = ValCSFINTLWARMAG + SumComponent
                            'Component1 = If(RegTranMagTbl(key)(CShort(yearfield$) - yrsRegTranMag(0)), 0)
                            Component1 = RegTranMagTbl(key)(CShort(yearfield$) - yrsRegTranMag(0))
                            If Not Component1.HasValue Then
                                Component1 = 0
                            Else
                                CInstabMag = CInstabMag + 1
                            End If
                            ValCSFINSTABMAG = ValCSFINSTABMAG + Component1
                            CConsolidatedMag = CConsolidatedMag + 1
                            ValCSFCONSOLIDATEDMAG = ValCSFCONSOLIDATEDMAG + SumComponent + Component1
                        Next YCount%
                        If CIntlWarMag > 0 Then
                            ValCSFINTLWARMAG = ValCSFINTLWARMAG / CIntlWarMag 'YearstoUse%
                        Else
                            ValCSFINTLWARMAG = -1 'No valid cases
                        End If
                        If CInstabMag > 0 Then
                            ValCSFINSTABMAG = ValCSFINSTABMAG / CInstabMag ' YearstoUse%
                        Else
                            ValCSFINSTABMAG = -1 'No valid cases
                        End If

                        If CConsolidatedMag > 0 Then
                            ValCSFCONSOLIDATEDMAG = ValCSFCONSOLIDATEDMAG / CConsolidatedMag
                            'YearstoUse%
                        Else
                            ValCSFCONSOLIDATEDMAG = -1 'No valid cases
                        End If
                        CSFINTLWARMAG.Add(key, AMAX(0.001F, ValCSFINTLWARMAG))
                        CSFINSTABMAG.Add(key, AMAX(0.001F, ValCSFINSTABMAG))
                        CSFCONSOLIDATEDMAG.Add(key, ValCSFCONSOLIDATEDMAG)
                        '   Compute probability of initial, continuing state failure across selected years
                        '  ------------------------------------------------------------------------------
                        CIntlWarAll = 0
                        CInstabAll = 0
                        CConsolidatedAll = 0
                        Dim ValCSFINTLWARALL As Single? = 0
                        Dim ValCSFINSTABALL As Single? = 0
                        Dim ValCSFCONSOLIDATEDALL As Single? = 0
                        'jrs 2024/05/17
                        Dim ValCSFCIVILWARUCDP As Single? = 0
                        CUCDPWarAll = 0
                        'dsb 2026/1/9
                        Dim ValCSFCIVILWARPARDEE As Single? = 0
                        CPardeeOnsetAll = 0

                        For YCount% = 1 To YearstoUse%
                            yearfield$ = CStr(BegYear% - 1 + YCount%).Trim
                            Component1 = EthnicWarEvTbl(key)(CShort(yearfield$) - yrsEthnicWarEv(0))
                            If Not Component1.HasValue Then
                                Component1 = 0
                            Else
                                CIntlWarAll = CIntlWarAll + 1
                            End If
                            Component2 = 0
                            'If checkYear(SFGenocideEvTbl, CInt(yearfield$)) Then
                            If yrsGenocideEv.Contains(CShort(yearfield$)) Then
                                Component2 = If(GenocideEvTbl(key)(CShort(yearfield$) - yrsGenocideEv(0)), 0)
                            End If
                            Component3 = If(RevolWarEvTbl(key)(CShort(yearfield$) - yrsRevolWarEv(0)), 0)
                            If Component1 + Component2 + Component3 > 0.01 Then
                                SumComponent = 1
                            Else
                                SumComponent = 0
                            End If
                            ValCSFINTLWARALL = ValCSFINTLWARALL + SumComponent
                            Component1 = RegTranEvTbl(key)(CShort(yearfield$) - yrsRegTranEv(0))
                            If Not Component1.HasValue Then
                                Component1 = 0
                            Else
                                CInstabAll = CInstabAll + 1
                            End If
                            If Component1 > 0.01 Then Component1 = 1
                            ValCSFINSTABALL = ValCSFINSTABALL + Component1
                            Component1 = ConsolidatedEvTbl(key)(CShort(yearfield$) - yrsConsolidatedEv(0))
                            If Not Component1.HasValue Then
                                Component1 = 0
                            Else
                                CConsolidatedAll = CConsolidatedAll + 1
                            End If
                            If Component1 > 0.01 Then Component1 = 1
                            ValCSFCONSOLIDATEDALL = ValCSFCONSOLIDATEDALL + Component1
                            'jrs 2024/05/17
                            Component1 = ConsolidatedUCDPTbl(key)(CShort(yearfield$) - yrsConsolidatedUCDP(0))
                            If Not Component1.HasValue Then
                                Component1 = 0
                            Else
                                CUCDPWarAll = CUCDPWarAll + 1
                            End If
                            If Component1 > 0.01 Then Component1 = 1
                            ValCSFCIVILWARUCDP = ValCSFCIVILWARUCDP + Component1
                            'dsb 2026/1/9
                            ComponentPardee = CivilWarPardeeConflictYearTbl(key)(CShort(yearfield$) - yrsCivilWarPardeeConflictYear(0))
                            If Not ComponentPardee.HasValue Then
                                ComponentPardee = 0
                            Else
                                CPardeeOnsetAll = CPardeeOnsetAll + 1
                            End If
                            If ComponentPardee > 0.01 Then ComponentPardee = 1
                            ValCSFCIVILWARPARDEE = ValCSFCIVILWARPARDEE + ComponentPardee

                        Next YCount%
                        If CIntlWarAll > 0 Then
                            ValCSFINTLWARALL = ValCSFINTLWARALL / CIntlWarAll 'YearstoUse%
                        Else
                            ValCSFINTLWARALL = -1 'No valid cases
                        End If
                        If YearstoUse% = 10 Then CSFINTHISREC.Add(key, ValCSFINTLWARALL)
                        If YearstoUse% > 30 Then CSFINTHISLON.Add(key, ValCSFINTLWARALL)

                        If CInstabAll > 0 Then
                            ValCSFINSTABALL = ValCSFINSTABALL / CInstabAll 'YearstoUse%
                        Else
                            ValCSFINSTABALL = -1 'No valid cases
                        End If
                        If YearstoUse% = 10 Then CSFINSHISREC.Add(key, ValCSFINSTABALL)
                        If YearstoUse% > 30 Then CSFINSHISLON.Add(key, ValCSFINSTABALL)

                        If CConsolidatedAll > 0 Then
                            ValCSFCONSOLIDATEDALL = ValCSFCONSOLIDATEDALL / CConsolidatedAll
                            'YearstoUse%
                        Else
                            ValCSFCONSOLIDATEDALL = -1 'No valid cases
                        End If
                        CSFINTLWARALL.Add(key, AMAX(0.001F, ValCSFINTLWARALL))
                        CSFINSTABALL.Add(key, AMAX(0.001F, ValCSFINSTABALL))
                        CSFCONSOLIDATEDALL.Add(key, ValCSFCONSOLIDATEDALL)
                        'jrs 2024/05/17
                        If CUCDPWarAll > 0 Then
                            ValCSFCIVILWARUCDP = ValCSFCIVILWARUCDP / CUCDPWarAll 'YearstoUse%
                        Else
                            ValCSFCIVILWARUCDP = -1 'No valid cases
                        End If
                        CSFCIVILWARUCDP.Add(key, AMAX(0.001F, ValCSFCIVILWARUCDP))
                        'dsb 2026/1/9
                        If CPardeeOnsetAll > 0 Then
                            ValCSFCIVILWARPARDEE = ValCSFCIVILWARPARDEE / CPardeeOnsetAll
                        Else
                            ValCSFCIVILWARPARDEE = -1
                        End If
                        CSFCIVILWARPARDEE.Add(key, AMAX(0.001F, ValCSFCIVILWARPARDEE))

                        '           Compute probability of initial state failure across selected years
                        '           ------------------------------------------------------------------
                        Dim ValCSFINTLWARY1 As Single? = 0
                        Dim ValCSFINSTABY1 As Single? = 0
                        Dim ValCSFCONSOLIDATEDY1 As Single? = 0
                        Dim CIntlWarY1 = 0
                        Dim CInstabY1 = 0
                        Dim CConsolidatedY1 = 0
                        Dim INTLWarCont = False
                        Dim INSTCont = False
                        Dim ConsolidatedCont = False
                        For YCount% = 1 To YearstoUse%
                            yearfield$ = CStr(BegYear% - 1 + YCount%).Trim
                            Component1 = If(EthnicWarEvTbl(key)(CShort(yearfield$) - yrsEthnicWarEv(0)), 0)
                            If Not Component1.HasValue Then
                                Component1 = 0
                            Else
                                CIntlWarY1 = CIntlWarY1 + 1
                            End If
                            Component2 = If(GenocideEvTbl(key)(CShort(yearfield$) - yrsGenocideEv(0)), 0)
                            Component3 = If(RevolWarEvTbl(key)(CShort(yearfield$) - yrsRevolWarEv(0)), 0)
                            If Component1 + Component2 + Component3 > 0.01 Then
                                SumComponent = 1
                            Else
                                SumComponent = 0
                            End If
                            If INTLWarCont = False AndAlso SumComponent = 1 Then 'New Event
                                ValCSFINTLWARY1 = ValCSFINTLWARY1 + SumComponent
                                INTLWarCont = True
                            End If
                            If SumComponent = 0 Then INTLWarCont = False
                            '"SeriesSFPITFRegTranEv"
                            Component1 = RegTranEvTbl(key)(CShort(yearfield$) - yrsRegTranEv(0))
                            If Not Component1.HasValue Then
                                Component1 = 0
                            Else
                                CInstabY1 = CInstabY1 + 1
                            End If
                            If Component1 > 0.01 Then Component1 = 1
                            If INSTCont = False AndAlso Component1 = 1 Then 'New Event
                                ValCSFINSTABY1 = ValCSFINSTABY1 + Component1
                                INSTCont = True
                            End If
                            If Component1 = 0 Then INSTCont = False
                            Component1 = ConsolidatedMagTbl(key)(CShort(yearfield$) - yrsConsolidatedMag(0))
                            If Not Component1.HasValue Then
                                Component1 = 0
                            Else
                                CConsolidatedY1 = CConsolidatedY1 + 1
                            End If
                            If Component1 > 0.01 Then Component1 = 1
                            If ConsolidatedCont = False AndAlso Component1 = 1 Then 'New Event
                                ValCSFCONSOLIDATEDY1 = ValCSFCONSOLIDATEDY1 + Component1
                                ConsolidatedCont = True
                            End If
                            If Component1 = 0 Then ConsolidatedCont = False
                        Next YCount%
                        If CIntlWarY1 > 0 Then
                            ValCSFINTLWARY1 = ValCSFINTLWARY1 / CIntlWarY1 'YearstoUse%
                        Else
                            ValCSFINTLWARY1 = -1 'No valid cases
                        End If

                        If CInstabY1 > 0 Then
                            ValCSFINSTABY1 = ValCSFINSTABY1 / CInstabY1 'YearstoUse%
                        Else
                            ValCSFINSTABY1 = -1 'No valid cases
                        End If
                        If CConsolidatedY1 > 0 Then
                            ValCSFCONSOLIDATEDY1 = ValCSFCONSOLIDATEDY1 / CConsolidatedY1
                            'YearstoUse%
                        Else
                            ValCSFCONSOLIDATEDY1 = -1 'No valid cases
                        End If
                        CSFINTLWARY1.Add(key, ValCSFINTLWARY1)
                        CSFINSTABY1.Add(key, ValCSFINSTABY1)
                        CSFCONSOLIDATEDY1.Add(key, ValCSFCONSOLIDATEDY1)
                    Next
                Else 'Forecast rather than History ------------------------------------------
                    Dim FinalYear%
                    Dim yearsEWar As Short()
                    Dim YearstoUse%
                    yearsEWar = cntx.GetYears("SeriesSFPITFEthnicWarMag").OrderByDescending(Function(y) y).ToArray()
                    Dim EWarMostRecentYear As Short
                    EWarMostRecentYear = yearsEWar(0)
                    If cntx.BaseYear > CInt(EWarMostRecentYear) Then
                        FinalYear% = CInt(EWarMostRecentYear)
                    Else
                        FinalYear% = cntx.BaseYear  'Data horizon for state failure
                    End If
                    Dim BegYear% = 1955
                    Dim WriteColumn$ = ""
                    'jrs 2024/05/17
                    Dim FinalYearUCDP%
                    Dim yearsUCDPWar As Short()
                    yearsUCDPWar = cntx.GetYears("SeriesSFUCDPConsolidatedEv").OrderByDescending(Function(y) y).ToArray()
                    Dim UCDPWarMostRecentYear As Short
                    UCDPWarMostRecentYear = yearsUCDPWar(0)
                    If cntx.BaseYear > CInt(UCDPWarMostRecentYear) Then
                        FinalYearUCDP = CInt(UCDPWarMostRecentYear)
                    Else
                        FinalYearUCDP = cntx.BaseYear  'Data horizon for state failure
                    End If
                    'dsb 2026/1/9 *** Start ***
                    Dim FinalYearPardee%
                    Dim yearsPardeeOnset As Short()
                    yearsPardeeOnset = cntx.GetYears("SeriesSFCivilWarPardeeConflictYear").OrderByDescending(Function(y) y).ToArray()
                    Dim PardeeOnsetMostRecentYear As Short
                    PardeeOnsetMostRecentYear = yearsPardeeOnset(0)
                    If cntx.BaseYear > CInt(PardeeOnsetMostRecentYear) Then
                        FinalYearPardee = CInt(PardeeOnsetMostRecentYear)
                    Else
                        FinalYearPardee = cntx.BaseYear
                    End If
                    'dsb 2026/1/9 *** End ***

                    'For Each key In cntx.Regionalization.Keys
                    Dim CIntlWarAll = 0
                    Dim CInstabAll = 0
                    Dim CConsolidatedAll = 0
                    Dim Component1 As Single?
                    Dim Component2 As Single?
                    Dim Component3 As Single?
                    Dim SumComponent As Single?
                    Dim CIntlWarMag = 0
                    Dim CInstabMag = 0
                    Dim CConsolidatedMag = 0
                    Dim ValCSFINTLWARMAG As Single? = 0
                    Dim ValCSFINSTABMAG As Single? = 0
                    Dim ValCSFCONSOLIDATEDMAG As Single? = 0
                    Dim ValCSFINTLWARALL As Single? = 0
                    Dim ValCSFINSTABALL As Single? = 0
                    Dim ValCSFCONSOLIDATEDALL As Single? = 0
                    Dim ValCSFINTLWARY1 As Single? = 0
                    Dim ValCSFINSTABY1 As Single? = 0
                    Dim ValCSFCONSOLIDATEDY1 As Single? = 0
                    Dim yearfield$
                    'jrs 2024/05/17
                    Dim ValCSFCIVILWARUCDP As Single? = 0
                    Dim CUCDPWarAll = 0
                    'dsb 2026/1/9
                    Dim ValCSFCIVILWARPARDEE As Single? = 0
                    Dim CPardeeOnsetAll = 0

                    For CaseLoop% = 1 To 4 'four different summaries of probabilities
                        Select Case CaseLoop%
                            Case 1
                                YearstoUse% = 10
                                WriteColumn$ = "Last 10 Years"
                            Case 2
                                YearstoUse% = 30
                                WriteColumn$ = "Last 30 Years"
                            Case 3
                                YearstoUse% = FinalYear% - BegYear% + 1
                                WriteColumn$ = "All Years"
                            Case 4
                                YearstoUse% = 20 'Because this is last case, these are probabilities used to initialize model
                                WriteColumn$ = "Last 20 Years"
                        End Select
                        For Each key In cntx.Regionalization.Keys
                            'reset variables
                            CIntlWarMag = 0
                            CInstabMag = 0
                            CConsolidatedMag = 0
                            ValCSFINTLWARMAG = 0
                            ValCSFINSTABMAG = 0
                            ValCSFCONSOLIDATEDMAG = 0
                            ValCSFINTLWARALL = 0
                            ValCSFINSTABALL = 0
                            ValCSFCONSOLIDATEDALL = 0
                            ValCSFINTLWARY1 = 0
                            ValCSFINSTABY1 = 0
                            ValCSFCONSOLIDATEDY1 = 0
                            'jrs 2024/05/17
                            ValCSFCIVILWARUCDP = 0
                            CUCDPWarAll = 0
                            'dsb 2026/1/9
                            ValCSFCIVILWARPARDEE = 0
                            CPardeeOnsetAll = 0

                            '           Compute average magnitude of initial state failure across selected years
                            '           ------------------------------------------------------------------------
                            For YCount% = 1 To YearstoUse%  ' Pick out last 10 years of data if YearstoUse = 10
                                yearfield$ = CStr(FinalYear% - YearstoUse% + YCount%).Trim
                                Component1 = EthnicWarMagTbl(key)(CShort(yearfield$) - yrsEthnicWarMag(0))
                                If Not Component1.HasValue Then
                                    Component1 = 0
                                Else
                                    CIntlWarMag = CIntlWarMag + 1
                                End If
                                Component2 = 0
                                If yrsGenocideMag.Contains(CShort(yearfield$)) Then
                                    Component2 = If(GenocideMagTbl(key)(CShort(yearfield$) - yrsGenocideMag(0)), 0)
                                End If
                                Component3 = If(RevolWarMagTbl(key)(CShort(yearfield$) - yrsRevolWarMag(0)), 0)
                                If Component1 + Component2 + Component3 > 0.01 Then
                                    SumComponent = Component1 + Component2 + Component3
                                Else
                                    SumComponent = 0
                                End If
                                ValCSFINTLWARMAG = ValCSFINTLWARMAG + SumComponent
                                Component1 = RegTranMagTbl(key)(CShort(yearfield$) - yrsRegTranMag(0))
                                If Not Component1.HasValue Then
                                    Component1 = 0
                                Else
                                    CInstabMag = CInstabMag + 1
                                End If
                                ValCSFINSTABMAG = ValCSFINSTABMAG + Component1
                                CConsolidatedMag = CConsolidatedMag + 1
                                ValCSFCONSOLIDATEDMAG = ValCSFCONSOLIDATEDMAG + SumComponent + Component1
                                'ttt- Skip 
                                'SFConsolidatedTbl.Update
                                Dim sUpd As String = "UPDATE [SeriesSFPITFConsolidatedMag] SET [" & yearfield$ & "] = " & CStr(SumComponent + Component1) & " WHERE Country = """ & key & """"
                                cmd.CommandText = sUpd
                                cmd.ExecuteNonQuery()
                            Next YCount%
                            'Note:  This structure results in summing magnitude across all 3 types of internal war
                            '   and dividing the sum by the years used (the years for which ethnic war data exist, ultimately
                            '   in Case 4); it is therefore not an average across the war types, but a sum of magnitude,
                            '   averaged only by the years to use
                            If CIntlWarMag > 0 Then
                                ValCSFINTLWARMAG = ValCSFINTLWARMAG / CIntlWarMag 'YearstoUse%
                            Else
                                ValCSFINTLWARMAG = -1 'No valid cases
                            End If
                            If CInstabMag > 0 Then
                                ValCSFINSTABMAG = ValCSFINSTABMAG / CInstabMag ' YearstoUse%
                            Else
                                ValCSFINSTABMAG = -1 'No valid cases
                            End If

                            If CConsolidatedMag > 0 Then
                                ValCSFCONSOLIDATEDMAG = ValCSFCONSOLIDATEDMAG / CConsolidatedMag
                                'YearstoUse%
                            Else
                                ValCSFCONSOLIDATEDMAG = -1 'No valid cases
                            End If

                            '           Compute probability of initial, continuing state failure across selected years
                            '           ------------------------------------------------------------------------------
                            CIntlWarAll = 0
                            CInstabAll = 0
                            CConsolidatedAll = 0
                            ValCSFINTLWARALL = 0
                            ValCSFINSTABALL = 0
                            ValCSFCONSOLIDATEDALL = 0
                            For YCount% = 1 To YearstoUse%  ' Pick out most recent if YearstoUse = 10
                                yearfield$ = CStr(FinalYear% - YearstoUse% + YCount%).Trim
                                Component1 = EthnicWarEvTbl(key)(CShort(yearfield$) - yrsEthnicWarEv(0))
                                If Not Component1.HasValue Then
                                    Component1 = 0
                                Else
                                    CIntlWarAll = CIntlWarAll + 1
                                End If
                                Component2 = 0
                                If yrsGenocideEv.Contains(CShort(yearfield$)) Then
                                    Component2 = If(GenocideEvTbl(key)(CShort(yearfield$) - yrsGenocideEv(0)), 0)
                                End If
                                Component3 = If(RevolWarEvTbl(key)(CShort(yearfield$) - yrsRevolWarEv(0)), 0)
                                If Component1 + Component2 + Component3 > 0.01 Then
                                    SumComponent = 1
                                Else
                                    SumComponent = 0
                                End If
                                ValCSFINTLWARALL = ValCSFINTLWARALL + SumComponent
                                Component1 = RegTranEvTbl(key)(CShort(yearfield$) - yrsRegTranEv(0))
                                If Not Component1.HasValue Then
                                    Component1 = 0
                                Else
                                    CInstabAll = CInstabAll + 1
                                End If
                                If Component1 > 0.01 Then Component1 = 1
                                ValCSFINSTABALL = ValCSFINSTABALL + Component1
                                Component1 = ConsolidatedEvTbl(key)(CShort(yearfield$) - yrsConsolidatedEv(0))
                                If Not Component1.HasValue Then
                                    Component1 = 0
                                Else
                                    CConsolidatedAll = CConsolidatedAll + 1
                                End If
                                If Component1 > 0.01 Then Component1 = 1
                                ValCSFCONSOLIDATEDALL = ValCSFCONSOLIDATEDALL + Component1
                            Next YCount%
                            If CIntlWarAll > 0 Then
                                ValCSFINTLWARALL = ValCSFINTLWARALL / CIntlWarAll 'YearstoUse%
                            Else
                                ValCSFINTLWARALL = -1 'No valid cases
                            End If
                            If YearstoUse% = 10 Then CSFINTHISREC.Add(key, ValCSFINTLWARALL)
                            If YearstoUse% > 30 Then CSFINTHISLON.Add(key, ValCSFINTLWARALL)

                            If CInstabAll > 0 Then
                                ValCSFINSTABALL = ValCSFINSTABALL / CInstabAll 'YearstoUse%
                            Else
                                ValCSFINSTABALL = -1 'No valid cases
                            End If
                            If YearstoUse% = 10 Then CSFINSHISREC.Add(key, ValCSFINSTABALL)
                            If YearstoUse% > 30 Then CSFINSHISLON.Add(key, ValCSFINSTABALL)

                            If CConsolidatedAll > 0 Then
                                ValCSFCONSOLIDATEDALL = ValCSFCONSOLIDATEDALL / CConsolidatedAll
                                'YearstoUse%
                            Else
                                ValCSFCONSOLIDATEDALL = -1 'No valid cases
                            End If
                            'jrs 2024/05/17 *** start ***
                            For YCount% = 1 To YearstoUse%  ' Pick out most recent if YearstoUse = 10
                                yearfield$ = CStr(FinalYearUCDP - YearstoUse% + YCount%).Trim
                                Component1 = ConsolidatedUCDPTbl(key)(CShort(yearfield$) - yrsConsolidatedUCDP(0))
                                If Not Component1.HasValue Then
                                    Component1 = 0
                                Else
                                    CUCDPWarAll = CUCDPWarAll + 1
                                End If
                                ValCSFCIVILWARUCDP = ValCSFCIVILWARUCDP + Component1
                            Next YCount%

                            If CUCDPWarAll > 0 Then
                                'This is always going to be 1 
                                ValCSFCIVILWARUCDP = ValCSFCIVILWARUCDP / CUCDPWarAll
                            Else
                                ValCSFCIVILWARUCDP = -1
                            End If
                            'jrs 2024/05/17 *** end ***
                            'dsb 2026/1/9 *** start ***
                            For YCount% = 1 To YearstoUse%
                                yearfield$ = CStr(FinalYearPardee - YearstoUse% + YCount%).Trim
                                Component1 = CivilWarPardeeConflictYearTbl(key)(CShort(yearfield$) - yrsCivilWarPardeeConflictYear(0))
                                If Not Component1.HasValue Then
                                    Component1 = 0
                                Else
                                    CPardeeOnsetAll = CPardeeOnsetAll + 1
                                End If
                                ValCSFCIVILWARPARDEE = ValCSFCIVILWARPARDEE + Component1
                            Next YCount%
                            If CPardeeOnsetAll > 0 Then
                                ValCSFCIVILWARPARDEE = ValCSFCIVILWARPARDEE / CPardeeOnsetAll
                            Else
                                ValCSFCIVILWARPARDEE = -1
                            End If
                            'dsb 2026/1/9 *** end ***
                            '           Compute probability of initial state failure across selected years
                            '           ------------------------------------------------------------------
                            ValCSFINTLWARY1 = 0
                            ValCSFINSTABY1 = 0
                            ValCSFCONSOLIDATEDY1 = 0
                            Dim CIntlWarY1 = 0
                            Dim CInstabY1 = 0
                            Dim CConsolidatedY1 = 0
                            Dim INTLWarCont = False
                            Dim INSTCont = False
                            Dim ConsolidatedCont = False
                            For YCount% = 1 To YearstoUse%  ' Pick out most recent if YearstoUse = 10
                                yearfield$ = CStr(FinalYear% - YearstoUse% + YCount%).Trim
                                Component1 = EthnicWarEvTbl(key)(CShort(yearfield$) - yrsEthnicWarEv(0))
                                If Not Component1.HasValue Then
                                    Component1 = 0
                                Else
                                    CIntlWarY1 = CIntlWarY1 + 1
                                End If
                                Component2 = 0
                                If yrsGenocideEv.Contains(CShort(yearfield$)) Then
                                    Component2 = If(GenocideEvTbl(key)(CShort(yearfield$) - yrsGenocideEv(0)), 0)
                                End If
                                Component3 = If(RevolWarEvTbl(key)(CShort(yearfield$) - yrsRevolWarEv(0)), 0)
                                If Component1 + Component2 + Component3 > 0.01 Then
                                    SumComponent = 1
                                Else
                                    SumComponent = 0
                                End If
                                If INTLWarCont = False AndAlso SumComponent = 1 Then 'New Event
                                    ValCSFINTLWARY1 = ValCSFINTLWARY1 + SumComponent
                                    INTLWarCont = True
                                End If
                                If SumComponent = 0 Then INTLWarCont = False
                                Component1 = RegTranEvTbl(key)(CShort(yearfield$) - yrsRegTranEv(0))
                                If Not Component1.HasValue Then
                                    Component1 = 0
                                Else
                                    CInstabY1 = CInstabY1 + 1
                                End If
                                If Component1 > 0.01 Then Component1 = 1
                                If INSTCont = False AndAlso Component1 = 1 Then 'New Event
                                    ValCSFINSTABY1 = ValCSFINSTABY1 + Component1
                                    INSTCont = True
                                End If
                                If Component1 = 0 Then INSTCont = False
                                Component1 = ConsolidatedMagTbl(key)(CShort(yearfield$) - yrsConsolidatedMag(0))
                                If Not Component1.HasValue Then
                                    Component1 = 0
                                Else
                                    CConsolidatedY1 = CConsolidatedY1 + 1
                                End If
                                If Component1 > 0.01 Then Component1 = 1
                                If ConsolidatedCont = False AndAlso Component1 = 1 Then 'New Event
                                    ValCSFCONSOLIDATEDY1 = ValCSFCONSOLIDATEDY1 + Component1
                                    ConsolidatedCont = True
                                End If
                                If Component1 = 0 Then ConsolidatedCont = False

                            Next YCount%
                            If CIntlWarY1 > 0 Then
                                ValCSFINTLWARY1 = ValCSFINTLWARY1 / CIntlWarY1 'YearstoUse%
                            Else
                                ValCSFINTLWARY1 = -1 'No valid cases
                            End If

                            If CInstabY1 > 0 Then
                                ValCSFINSTABY1 = ValCSFINSTABY1 / CInstabY1 'YearstoUse%
                            Else
                                ValCSFINSTABY1 = -1 'No valid cases
                            End If
                            If CConsolidatedY1 > 0 Then
                                ValCSFCONSOLIDATEDY1 = ValCSFCONSOLIDATEDY1 / CConsolidatedY1
                                'YearstoUse%
                            Else
                                ValCSFCONSOLIDATEDY1 = -1 'No valid cases
                            End If

                            ' ttt- Skip: Write summary probabilities back to history for statistical analysis availability
                            '   --------------------------------------------------------------------------------
                            If CaseLoop% = 4 Then
                                CSFINTLWARMAG.Add(key, AMAX(0.001F, ValCSFINTLWARMAG))
                                CSFINSTABMAG.Add(key, AMAX(0.001F, ValCSFINSTABMAG))
                                CSFCONSOLIDATEDMAG.Add(key, ValCSFCONSOLIDATEDMAG)
                                CSFINTLWARALL.Add(key, AMAX(0.001F, ValCSFINTLWARALL))
                                CSFINSTABALL.Add(key, AMAX(0.001F, ValCSFINSTABALL))
                                CSFCONSOLIDATEDALL.Add(key, ValCSFCONSOLIDATEDALL)
                                CSFINTLWARY1.Add(key, ValCSFINTLWARY1)
                                CSFINSTABY1.Add(key, ValCSFINSTABY1)
                                CSFCONSOLIDATEDY1.Add(key, ValCSFCONSOLIDATEDY1)
                                'jrs 2024/05/17
                                CSFCIVILWARUCDP.Add(key, AMAX(0.001F, ValCSFCIVILWARUCDP))
                                'dsb 2026/1/9
                                CSFCIVILWARPARDEE.Add(key, AMAX(0.001F, ValCSFCIVILWARPARDEE))
                                'jrs 2023/06/02 start
                                Dim sUpd As String
                                sUpd = "UPDATE [SeriesSFPITFInternalWarEvProb] SET [" & WriteColumn$ & "] = "
                                If ValCSFINTLWARALL >= 0 Then
                                    sUpd = sUpd & ValCSFINTLWARALL.ToString
                                Else
                                    sUpd = sUpd & " Null"
                                End If
                                sUpd = sUpd & " WHERE Country = """ & key & """"
                                cmd.CommandText = sUpd
                                cmd.ExecuteNonQuery()

                                sUpd = "UPDATE [SeriesSFPITFDomInstabilityEvProb] SET [" & WriteColumn$ & "] = "
                                If ValCSFINSTABALL >= 0 Then
                                    sUpd = sUpd & ValCSFINSTABALL.ToString
                                Else
                                    sUpd = sUpd & " Null"
                                End If
                                sUpd = sUpd & " WHERE Country = """ & key & """"
                                cmd.CommandText = sUpd
                                cmd.ExecuteNonQuery()

                                sUpd = "UPDATE [SeriesSFPITFConsolidatedEvProb] SET [" & WriteColumn$ & "] = "
                                If ValCSFCONSOLIDATEDALL >= 0 Then
                                    sUpd = sUpd & ValCSFCONSOLIDATEDALL.ToString
                                Else
                                    sUpd = sUpd & " Null"
                                End If
                                sUpd = sUpd & " WHERE Country = """ & key & """"
                                cmd.CommandText = sUpd
                                cmd.ExecuteNonQuery()

                                sUpd = "UPDATE [SeriesSFPITFInternalWarY1Prob] SET [" & WriteColumn$ & "] = "
                                If ValCSFINTLWARY1 >= 0 Then
                                    sUpd = sUpd & ValCSFINTLWARY1.ToString
                                Else
                                    sUpd = sUpd & " Null"
                                End If
                                sUpd = sUpd & " WHERE Country = """ & key & """"
                                cmd.CommandText = sUpd
                                cmd.ExecuteNonQuery()

                                sUpd = "UPDATE [SeriesSFPITFDomInstabilityY1Prob] SET [" & WriteColumn$ & "] = "
                                If ValCSFINSTABY1 >= 0 Then
                                    sUpd = sUpd & ValCSFINSTABY1.ToString
                                Else
                                    sUpd = sUpd & " Null"
                                End If
                                sUpd = sUpd & " WHERE Country = """ & key & """"
                                cmd.CommandText = sUpd
                                cmd.ExecuteNonQuery()

                                sUpd = "UPDATE [SeriesSFPITFConsolidatedY1Prob] SET [" & WriteColumn$ & "] = "
                                If ValCSFCONSOLIDATEDY1 >= 0 Then
                                    sUpd = sUpd & ValCSFCONSOLIDATEDY1.ToString
                                Else
                                    sUpd = sUpd & " Null"
                                End If
                                sUpd = sUpd & " WHERE Country = """ & key & """"
                                cmd.CommandText = sUpd
                                cmd.ExecuteNonQuery()

                                sUpd = "UPDATE [SeriesSFPITFInternalWarMagAvg] SET [" & WriteColumn$ & "] = "
                                If ValCSFINTLWARMAG >= 0 Then
                                    sUpd = sUpd & ValCSFINTLWARMAG.ToString
                                Else
                                    sUpd = sUpd & " Null"
                                End If
                                sUpd = sUpd & " WHERE Country = """ & key & """"
                                cmd.CommandText = sUpd
                                cmd.ExecuteNonQuery()

                                sUpd = "UPDATE [SeriesSFPITFDomInstabilityMagAvg] SET [" & WriteColumn$ & "] = "
                                If ValCSFINSTABMAG >= 0 Then
                                    sUpd = sUpd & ValCSFINSTABMAG.ToString
                                Else
                                    sUpd = sUpd & " Null"
                                End If
                                sUpd = sUpd & " WHERE Country = """ & key & """"
                                cmd.CommandText = sUpd
                                cmd.ExecuteNonQuery()

                                sUpd = "UPDATE [SeriesSFPITFConsolidatedMagAvg] SET [" & WriteColumn$ & "] = "
                                If ValCSFCONSOLIDATEDMAG >= 0 Then
                                    sUpd = sUpd & ValCSFCONSOLIDATEDMAG.ToString
                                Else
                                    sUpd = sUpd & " Null"
                                End If
                                sUpd = sUpd & " WHERE Country = """ & key & """"
                                cmd.CommandText = sUpd
                                cmd.ExecuteNonQuery()
                                'jrs 2023/06/02 end
                            End If
                        Next key
                    Next CaseLoop% 'Last caseloop value (normally all years) remains in variables,

                    'jrs 2023/06/03 start
                    'jrs 2021/10/19 we're trying to save consolidated values for all years available, and not only to baseyear, for now baseyear is 2017 and data goes to 2018
                    'FinalYear% = BaseYear  'Data horizon for state failure
                    FinalYear% = Amin(Amin(Amin(LastYrEthWarEv%, LastYrGenEv%), LastYrRevolEv%), LastYrRegEv%)

                    BegYear% = 1955

                    For Each key In cntx.Regionalization.Keys
                        'If CountryName$(ICount%) = "Afghanistan" Then Stop
                        'jrs 2021/10/19 FinalYear% = BaseYear  'Data horizon for state failure
                        'jrs 2021/10/19 BegYear% = 1955
                        For YCount% = BegYear% To FinalYear%  'Final year/base year in 2012 was 2010, but genocide
                            'data only from 1955 through 2009; copied 2009 genocide to 2010 with Sudan genocide included

                            yearfield$ = CStr(YCount%).Trim
                            Dim yridx = getYearIdx(yrsEthnicWarEv, CShort(yearfield$))
                            'Begin Event calculation

                            Dim SFEthnicWarEv = EthnicWarEvTbl(key)(yridx) 'Internal War Start
                            If Not SFEthnicWarEv.HasValue Then SFEthnicWarEv = 0  'No Data Check
                            'jrs 2018/12/11
                            Dim SFGenocideEv As Single?
                            If yrsGenocideEv.Contains(CShort(yearfield)) Then
                                yridx = getYearIdx(yrsGenocideEv, CShort(yearfield$))
                                SFGenocideEv = GenocideEvTbl(key)(yridx)
                            End If
                            If Not SFGenocideEv.HasValue Then SFGenocideEv = 0  'No Data Check
                            yridx = getYearIdx(yrsRevolWarEv, CShort(yearfield))
                            Dim SFRevolWarEv = RevolWarEvTbl(key)(yridx)
                            If Not SFRevolWarEv.HasValue Then SFRevolWarEv = 0  'No Data Check
                            yridx = getYearIdx(yrsRegTranEv, CShort(yearfield))
                            Dim SFRegTranEv = RegTranEvTbl(key)(yridx)
                            If Not SFRegTranEv.HasValue Then SFRegTranEv = 0  'No Data Check

                            Dim ConsolidatedEv As Integer
                            If SFEthnicWarEv + SFGenocideEv + SFRevolWarEv + SFRegTranEv > 0.01 Then
                                ConsolidatedEv = 1
                            Else
                                ConsolidatedEv = 0
                            End If

                            Dim sUpd As String = "UPDATE [SeriesSFPITFConsolidatedEv] SET [" & yearfield$ & "] = " & CStr(ConsolidatedEv) & " WHERE Country = """ & key & """"
                            cmd.CommandText = sUpd
                            cmd.ExecuteNonQuery()

                            Dim InternalWarEv As Integer
                            If SFEthnicWarEv + SFGenocideEv + SFRevolWarEv > 0.01 Then
                                InternalWarEv = 1
                            Else
                                InternalWarEv = 0
                            End If
                            'If YCount% = 2007 And CountryName$(ICount%) = "Bangladesh" Then Stop
                            sUpd = "UPDATE [SeriesSFPITFInternalWarEv] SET [" & yearfield$ & "] = " & CStr(InternalWarEv) & " WHERE Country = """ & key & """"
                            cmd.CommandText = sUpd
                            cmd.ExecuteNonQuery()


                            'Begin Magnitude calculation
                            yridx = getYearIdx(yrsEthnicWarMag, CShort(yearfield$))
                            Dim SFEthnicWar = EthnicWarMagTbl(key)(yridx) 'Internal War Start
                            If Not SFEthnicWar.HasValue Then SFEthnicWar = 0  'No Data Check
                            'jrs 2018/12/11
                            Dim SFGenocide As Single?
                            If yrsGenocideMag.Contains(CShort(yearfield)) Then
                                yridx = getYearIdx(yrsGenocideMag, CShort(yearfield$))
                                SFGenocide = GenocideMagTbl(key)(yridx)
                            End If
                            If Not SFGenocide.HasValue Then SFGenocide = 0  'No Data Check
                            yridx = getYearIdx(yrsRevolWarMag, CShort(yearfield$))
                            Dim SFRevolWar = RevolWarMagTbl(key)(yridx)
                            If Not SFRevolWar.HasValue Then SFRevolWar = 0  'No Data Check
                            yridx = getYearIdx(yrsRegTranMag, CShort(yearfield$))
                            Dim SFRegTran = RegTranMagTbl(key)(yridx)
                            If Not SFRegTran.HasValue Then SFRegTran = 0  'No Data Check
                            Dim ConsolidatedMag = SFEthnicWar + SFGenocide + SFRevolWar + SFRegTran
                            sUpd = "UPDATE [SeriesSFPITFConsolidatedMag] SET [" & yearfield$ & "] = " & CStr(ConsolidatedMag) & " WHERE Country = """ & key & """"
                            cmd.CommandText = sUpd
                            cmd.ExecuteNonQuery()

                        Next YCount%

                    Next  'For each country
                    'jrs 2023/06/03 end

                End If 'Forecast only

                db.Close()
                db.Dispose()

                'Dispose huge variables
                EthnicWarMagTbl = Nothing
                GenocideMagTbl = Nothing
                RevolWarMagTbl = Nothing
                RegTranMagTbl = Nothing
                EthnicWarEvTbl = Nothing
                GenocideEvTbl = Nothing
                RevolWarEvTbl = Nothing
                RegTranEvTbl = Nothing
                ConsolidatedEvTbl = Nothing
                ConsolidatedMagTbl = Nothing
                '----LoadDIPLOCountry-----
                Dim HPP = New String() {"Hong Kong", "Palestine", "Puerto Rico"}
                If cntx.HistoricMode Then
                    For Each item In (From s In cntx.GetSlice("SeriesDiploEmbassyOut", cntx.FirstYear)
                                      Select Key = s.Key, Value = s.Value)
                        Dim ValCDIPLOEMBASSY = If(item.Value, 0)
                        If HPP.Contains(item.Key) Then
                            ValCDIPLOEMBASSY = 0.01
                        End If
                        CDIPLOEMBASSY.Add(item.Key, ValCDIPLOEMBASSY)
                    Next

                    'JAM 2019-06-20 Commented due mti improved
                    'Dim DiploIGOWeightedTotal_Earliest = cntx.GetSlice("SeriesDiploIGOWeightedTotal", "Earliest")

                    'For Each item In (From s In cntx.GetSlice("SeriesDiploIGOWeightedTotal", cntx.FirstYear)
                    '                  Select Key = s.Key, Value = s.Value)
                    '    Dim ValCDIPLOIGO = item.Value
                    '    If ValCDIPLOIGO Is Nothing Then
                    '        ValCDIPLOIGO = DiploIGOWeightedTotal_Earliest(item.Key)
                    '    End If
                    '    CDIPLOIGO.Add(item.Key, ValCDIPLOIGO)
                    'Next

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs As Short() = cntx.GetYears("SeriesDiploIGOWeightedTotal").OrderBy(Function(y) y).ToArray()
                    Dim DiploIGOWeightedTotalTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesDiploIGOWeightedTotal", yrs)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(DiploIGOWeightedTotalTbl, yrs, key, cntx.FirstYear, HistDist)

                        'Dim ValCDIPLOIGO = ValHold
                        'If ValCDIPLOIGO Is Nothing Then
                        '    ValCDIPLOIGO = DiploIGOWeightedTotal_Earliest(key)
                        'End If
                        CDIPLOIGO.Add(key, ValHold)

                    Next
                    'mti 2019/04/29  Flex Hist Base End



                    For Each item In (From s In cntx.GetSlice("SeriesDiploTreatyWeightedSign1Rat2", cntx.FirstYear)
                                      Select Key = s.Key, Value = s.Value)
                        CDIPLOTREATY.Add(item.Key, If(item.Value, 0))
                    Next
                    For Each item In (From s In cntx.GetSlice("SeriesDiploDistanceToEurope", cntx.BaseYear)
                                      Select Key = s.Key, Value = s.Value)
                        CDIPLODISTANCE.Add(item.Key, item.Value)
                    Next
                    'JAM 2019-06-21 Commented due mti improved
                    'Dim DiploINGOProgrammatic_Earliest = cntx.GetSlice("SeriesDiploINGOProgrammatic", "Earliest")
                    'For Each item In (From s In cntx.GetSlice("SeriesDiploINGOProgrammatic", 1990)
                    '                  Select Key = s.Key, Value = s.Value)
                    '    Dim ValCDIPLOINGOPROGRAM = item.Value
                    '    If ValCDIPLOINGOPROGRAM Is Nothing Then
                    '        ValCDIPLOINGOPROGRAM = DiploINGOProgrammatic_Earliest(item.Key)
                    '        If ValCDIPLOINGOPROGRAM Is Nothing Then
                    '            ValCDIPLOINGOPROGRAM = cntx.GetRegression("GDP/Capita (PPP 2011) Versus INGO Programmatic (2012) Log").ToEstimator(CGDPPCP)(item.Key)
                    '        End If
                    '    End If
                    '    CDIPLOINGOPROGRAM.Add(item.Key, ValCDIPLOINGOPROGRAM)
                    'Next

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs2 As Short() = cntx.GetYears("SeriesDiploINGOProgrammatic").OrderBy(Function(y) y).ToArray()
                    Dim DiploINGOProgrammaticTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesDiploINGOProgrammatic", yrs2)

                    For Each key In cntx.Regionalization.Keys
                        ValCDIPLOINGOPROGRAM = getClosestYrVal(DiploINGOProgrammaticTbl, yrs2, key, cntx.FirstYear, HistDist)

                        If ValCDIPLOINGOPROGRAM Is Nothing Then
                            'ValCDIPLOINGOPROGRAM = DiploINGOProgrammatic_Earliest(item.Key)
                            'If ValCDIPLOINGOPROGRAM Is Nothing Then
                            ValCDIPLOINGOPROGRAM = cntx.GetRegression("GDP/Capita (PPP 2011) Versus INGO Programmatic (2012) Log").ToEstimator(CGDPPCP)(key)
                            'End If
                        End If
                        CDIPLOINGOPROGRAM.Add(key, ValCDIPLOINGOPROGRAM)


                    Next
                    'mti 2019/04/29  Flex Hist Base End


                    'JAM 2019-06-22 Commented due mti improved
                    'Dim DiploINGOAdvocacy_Earliest = cntx.GetSlice("SeriesDiploINGOAdvocacy", "Earliest")
                    'For Each item In (From s In cntx.GetSlice("SeriesDiploINGOAdvocacy", 1990)
                    '                  Select Key = s.Key, Value = s.Value)
                    '    Dim ValCDIPLOINGOADVOCACY = item.Value
                    '    If ValCDIPLOINGOADVOCACY Is Nothing Then
                    '        ValCDIPLOINGOADVOCACY = DiploINGOAdvocacy_Earliest(item.Key)
                    '        If ValCDIPLOINGOADVOCACY Is Nothing Then
                    '            ValCDIPLOINGOADVOCACY = 5
                    '        End If
                    '    End If
                    '    CDIPLOINGOADVOCACY.Add(item.Key, ValCDIPLOINGOADVOCACY)
                    'Next

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs3 As Short() = cntx.GetYears("SeriesDiploINGOAdvocacy").OrderBy(Function(y) y).ToArray()
                    Dim DiploINGOAdvocacyTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesDiploINGOAdvocacy", yrs3)
                    For Each key In cntx.Regionalization.Keys
                        ValCDIPLOINGOADVOCACY = getClosestYrVal(DiploINGOAdvocacyTbl, yrs3, key, cntx.FirstYear, HistDist)
                        If ValCDIPLOINGOADVOCACY Is Nothing Then
                            'ValCDIPLOINGOADVOCACY = DiploINGOAdvocacy_Earliest(item.Key)
                            'If ValCDIPLOINGOADVOCACY Is Nothing Then
                            ValCDIPLOINGOADVOCACY = 5
                            'End If
                        End If
                        CDIPLOINGOADVOCACY.Add(key, ValCDIPLOINGOADVOCACY)

                    Next
                    'mti 2019/04/29  Flex Hist Base End


                Else
                    For Each item In (From s In cntx.GetSlice("SeriesDiploEmbassyOut", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = s.Value)
                        CDIPLOEMBASSY.Add(item.Key, If(item.Value, 0))
                    Next
                    For Each item In (From s In cntx.GetSlice("SeriesDiploIGOWeightedTotal", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = s.Value)
                        CDIPLOIGO.Add(item.Key, item.Value)
                    Next
                    For Each item In (From s In cntx.GetSlice("SeriesDiploTreatyWeightedSign1Rat2", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = s.Value)
                        CDIPLOTREATY.Add(item.Key, If(item.Value, 0))
                    Next
                    For Each item In (From s In cntx.GetSlice("SeriesDiploDistanceToEurope", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = s.Value)
                        CDIPLODISTANCE.Add(item.Key, item.Value)
                    Next
                    'jrs 2024/06/28
                    Dim TFNameINGOProgram As String
                    TFNameINGOProgram = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus INGO Programmatic (" & BaseYear & ") Log"
                    TFNameINGOProgram = getFunctionNameYrOrMstRct(TFNameINGOProgram, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")

                    For Each item In (From s In cntx.GetSlice("SeriesDiploINGOProgrammatic", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = s.Value)

                        'JAM 2019-06-21 Commented due mti improved
                        'Dim ValCDIPLOINGOPROGRAM = item.Value
                        ValCDIPLOINGOPROGRAM = item.Value
                        If ValCDIPLOINGOPROGRAM Is Nothing Then
                            'jrs 2024/06/28 ValCDIPLOINGOPROGRAM = cntx.GetRegression("GDP/Capita (PPP 2011) Versus INGO Programmatic (2012) Log").ToEstimator(CGDPPCP)(item.Key)
                            ValCDIPLOINGOPROGRAM = cntx.GetRegression(TFNameINGOProgram).ToEstimator(CGDPPCP)(item.Key)
                        End If
                        CDIPLOINGOPROGRAM.Add(item.Key, ValCDIPLOINGOPROGRAM)
                    Next
                    For Each item In (From s In cntx.GetSlice("SeriesDiploINGOAdvocacy", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = s.Value)
                        'JAM 2019-06-23 Commented due mti improved
                        'Dim ValCDIPLOINGOADVOCACY = item.Value
                        ValCDIPLOINGOADVOCACY = item.Value
                        If ValCDIPLOINGOADVOCACY Is Nothing Then
                            ValCDIPLOINGOADVOCACY = 5
                        End If
                        CDIPLOINGOADVOCACY.Add(item.Key, ValCDIPLOINGOADVOCACY)
                    Next
                End If
                '----LoadHTCCountry
                If cntx.HistoricMode Then
                    'JAM 2019-06-23 Commented due mti improved
                    'For Each item In (From s In cntx.GetSlice("SeriesHTCtpi", "Earliest")
                    '                  Select Key = s.Key, Value = s.Value)
                    '    CHTCTPI.Add(item.Key, If(item.Value, -1))
                    'Next

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs4 As Short() = cntx.GetYears("SeriesHTCtpi").OrderBy(Function(y) y).ToArray()
                    Dim HTCtpiTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesHTCtpi", yrs4)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(HTCtpiTbl, yrs4, key, cntx.FirstYear, HistDist)

                        CHTCTPI.Add(key, If(ValHold, -1))
                    Next
                    'mti 2019/04/29  Flex Hist Base End


                    'JAM 2019-06-23 Commented due mti improved
                    'For Each item In (From s In cntx.GetSlice("SeriesHTCgri", "Earliest")
                    '                  Select Key = s.Key, Value = s.Value)
                    '    CHTCGRI.Add(item.Key, If(item.Value, -1))
                    'Next

                    'mti 2019/04/29  Flex Hist Base Start
                    Dim yrs5 As Short() = cntx.GetYears("SeriesHTCgri").OrderBy(Function(y) y).ToArray()
                    Dim HTCgriTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesHTCgri", yrs5)
                    For Each key In cntx.Regionalization.Keys
                        ValHold = getClosestYrVal(HTCgriTbl, yrs5, key, cntx.FirstYear, HistDist)

                        CHTCGRI.Add(key, If(ValHold, -1))

                    Next
                    'mti 2019/04/29  Flex Hist Base End

                Else
                    For Each item In (From s In cntx.GetSlice("SeriesHTCtpi", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = s.Value)
                        CHTCTPI.Add(item.Key, If(item.Value, -1))
                    Next
                    For Each item In (From s In cntx.GetSlice("SeriesHTCgri", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = s.Value)
                        CHTCGRI.Add(item.Key, If(item.Value, -1))
                    Next
                End If

                RegionalizeColony(cntx)

                cntx.Store("CSFINTLWARMAG", CSFINTLWARMAG)

                Me.processed = True
                'ttt***REMARKS: Tables edited in VB6 Codes, that are skipped in VB.Net
                'SocialOutput
                'SocialBase
                'SeriesSFPITFConsolidatedMag
                'SeriesSFPITFConsolidatedEv
                'SeriesSFPITFInternalWarEv
                'SeriesSFPITFInternalWarEvProb
                'SeriesSFPITFDomInstabilityEvProb
                'SeriesSFPITFConsolidatedEvProb
                'SeriesSFPITFInternalWarY1Prob
                'SeriesSFPITFDomInstabilityY1Prob
                'SeriesSFPITFConsolidatedY1Prob
                'SeriesSFPITFInternalWarMagAvg
                'SeriesSFPITFDomInstabilityMagAvg
                'SeriesSFPITFConsolidatedMagAvg


            End Sub

            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Me.PreProcess(cntx)

                Dim series As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim weightSeries As IDictionary(Of String, Single?)
                weightSeries = CPOP
                Dim MinVal As Single, MaxVal As Single, bLimits As Boolean = False
                Dim bSum As Boolean = True
                Select Case seriesName
                    Case "SFINSTABALL"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1
                        series = CSFINSTABALL
                    Case "SFINSTABMAG"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000
                        series = CSFINSTABMAG
                    Case "SFINTLWARALL"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1
                        series = CSFINTLWARALL
                    Case "SFCIVILWARUCDP" 'jrs 2024/05/17
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1
                        series = CSFCIVILWARUCDP
                    Case "SFINSECUREPOPIMP" 'dsb 2026/05/05
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1
                        series = CSFINSECUREPOPIMP
                    Case "SFCIVILWARPOPIMP" 'dsb 2025/12/17
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1
                        series = CSFCIVILWARPOPIMP
                    Case "SFCIVILWARPARDEE" 'dsb 2026/1/8
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1
                        series = CSFCIVILWARPARDEE
                    Case "SFINTLWARMAG"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 1000
                        series = CSFINTLWARMAG
                    Case "SFINSTABY1"
                        series = CSFINSTABY1
                    Case "SFINTLWARY1"
                        series = CSFINTLWARY1
                    Case "SFCONSOLIDATEDALL"
                        series = CSFCONSOLIDATEDALL
                    Case "SFCONSOLIDATEDMAG"
                        series = CSFCONSOLIDATEDMAG
                    Case "SFCONSOLIDATEDY1"
                        series = CSFCONSOLIDATEDY1
                    Case "SFINTHISLON"
                        series = CSFINTHISLON
                    Case "SFINTHISREC"
                        series = CSFINTHISREC
                    Case "SFINSHISLON"
                        series = CSFINSHISLON
                    Case "SFINSHISREC"
                        series = CSFINSHISREC
                    Case "DIPLOEMBASSY"
                        series = CDIPLOEMBASSY
                    Case "DIPLOIGO"
                        series = CDIPLOIGO
                    Case "DIPLOTREATY"
                        series = CDIPLOTREATY
                    Case "diplodistancetoeu"
                        bSum = False 'avg
                        series = CDIPLODISTANCE
                    Case "DIPLOINGOPROGRAM"
                        series = CDIPLOINGOPROGRAM
                    Case "DIPLOINGOADVOCACY"
                        series = CDIPLOINGOADVOCACY
                    Case "HTCGRI"
                        series = CHTCGRI
                    Case "HTCTPI"
                        series = CHTCTPI
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

            Private Sub RegionalizeColony(cntx As IRebuildFacade)
                Using db As IFsContext = New IFsContext()
                    Using db2 As IFs2Context = New IFs2Context()
                        db2.Clear(GetType(RegionWasColonyOf))
                        'gvdb 2024/04/06
                        ' update for new EF syntax
                        'For Each item In (From s In db.CountryWasColonyOf
                        '                  Join r In cntx.Regionalization On s.Country Equals r.Key
                        '                  Group UKcol = s.ukcol, Fracol = s.fracol, Gercol = s.gercol,
                        '                  PorCol = s.porcol, SpaCol = s.spacol, NetCol = s.netcol, RusCol = s.ruscol
                        '                  By RegionId = r.Value Into Data = Group,
                        '                  MaxUKcol = Max(UKcol), MaxFracol = Max(Fracol), MaxGercol = Max(Gercol),
                        '                  MaxPorCol = Max(PorCol), MaxSpaCol = Max(SpaCol), MaxNetCol = Max(NetCol),
                        '                  MaxRusCol = Max(RusCol)
                        ')
                        For Each item In (From r In cntx.Regionalization
                                          Join s In db.CountryWasColonyOf On s.Country Equals r.Key
                                          Group UKcol = s.ukcol, Fracol = s.fracol, Gercol = s.gercol,
                                          PorCol = s.porcol, SpaCol = s.spacol, NetCol = s.netcol, RusCol = s.ruscol
                                          By RegionId = r.Value Into Data = Group,
                                          MaxUKcol = Max(UKcol), MaxFracol = Max(Fracol), MaxGercol = Max(Gercol),
                                          MaxPorCol = Max(PorCol), MaxSpaCol = Max(SpaCol), MaxNetCol = Max(NetCol),
                                          MaxRusCol = Max(RusCol)
                        )

                            Dim row As RegionWasColonyOf = New RegionWasColonyOf
                            row.RegionId = item.RegionId
                            row.ukcol = item.MaxUKcol
                            row.fracol = item.MaxFracol
                            row.gercol = item.MaxGercol
                            row.porcol = item.MaxPorCol
                            row.spacol = item.MaxSpaCol
                            row.netcol = item.MaxNetCol
                            row.ruscol = item.MaxRusCol
                            db2.RegionWasColonyOf.Add(row)
                        Next
                        db2.Flush()
                    End Using
                End Using
            End Sub
        End Class


    End Module
End Namespace
