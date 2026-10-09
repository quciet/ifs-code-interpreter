Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data

Namespace Models
    Partial Public Module IntlConf

        Dim ContiguDistance As Dictionary(Of String, Integer)
        Dim ContiguityReg(,) As Integer
        Dim TerritorialDispute As Dictionary(Of String, Integer)
        Dim terdispute(,) As Integer
        'jrs 2019/08/14 Dim THREAT(,) As Single

        <RebuildsVariable("contiguity")>
        <RebuildsVariable("THREAT")>
        <RebuildsVariable("terdispute")>
        <ImportsAsset("CGINIDOM")>
        Public Iterator Function RebuildIntlConf(context As IRebuildContext) As IEnumerable(Of IDataframe)

            Dim objWriter = New System.IO.StreamWriter(context.PutFdrvRun + "progress.txt", append:=True)
            objWriter.WriteLine("Processing Data on International Conflict - Please Wait")
            objWriter.Close()

            'Dim CGINIDOM As IDictionary(Of String, Single?) = context.Fetch(Of IDictionary(Of String, Single?))("CGINIDOM")
            ReDim ContiguityReg(NR, NR), terdispute(NR, NR)
            ContiguDistance = New Dictionary(Of String, Integer)
            TerritorialDispute = New Dictionary(Of String, Integer)

            '   Go to subroutine that uses country data to compute contiguity of regions
            '   ------------------------------------------------------------------------
            ComputeContiguity(context)

            '   Go to subroutine that uses country data to compute territorial disputes of regions
            '   ----------------------------------------------------------------------------------
            ComputeTerritorialDisputes(context)

            '   Go to subroutine that uses country data to compute dyadic threat
            '   ----------------------------------------------------------------
            'jrs 2018/08/13 Added if to avoid doing it when dyadic available, already initialized with dyadic data
            If Not System.IO.File.Exists(context.DataFileDir$ + "IFsForDyadic.db") Then
                ComputeThreat(context)
            End If

            Dim df As Dataframe = Dataframe.Create("contiguity", context, context.FirstYear, context.LastYear, Nothing)
            'Save ContiguityReg to df
            Dim R, R2 As Short
            For R = 1 To CShort(NR)
                For R2 = 1 To CShort(NR)
                    For y = FirstYear To LastYear
                        df(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), R2, CShort(NR))) = ContiguityReg(R, R2)
                    Next
                Next
            Next

            Dim df2 As Dataframe = Dataframe.Create("THREAT", context, context.FirstYear, Nothing)
            'Save THREAT to df2
            For Each regionA As Short In context.Dimensions(1).Buckets.Keys
                For Each regionB As Short In context.Dimensions(1).Buckets.Keys
                    df2(context.FirstYear, regionA, regionB) = THREAT(regionA, regionB)
                Next
            Next

            Dim df3 As Dataframe = Dataframe.Create("terdispute", context, context.FirstYear, context.LastYear, Nothing)
            'save terdispute to df3
            For R = 1 To CShort(NR)
                For R2 = 1 To CShort(NR)
                    For y = FirstYear To LastYear
                        df3(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), R2, CShort(NR))) = terdispute(R, R2)
                    Next
                Next
            Next

            Yield df
            Yield df2
            Yield df3
        End Function

        Private Sub ComputeContiguity(cntx As IRebuildFacade)
            Using db As IFsContext = New IFsContext()
                Dim total_contiguity As Integer
                total_contiguity% = 0    ' Total no. of records in Contiguity
                For Each tbl1 As Contiguity In db.Contiguity
                    total_contiguity% = total_contiguity% + 1
                    If Not ContiguDistance.ContainsKey(tbl1.Region + tbl1.Region_Adjacent) Then
                        ContiguDistance.Add(tbl1.Region + tbl1.Region_Adjacent, tbl1.Distance)
                    Else
                        ContiguDistance(tbl1.Region + tbl1.Region_Adjacent)= tbl1.Distance
                    End If
                Next
            End Using

            '   Prepare contiguity matrix for regions from country data
            '   -------------------------------------------------------
            Dim Minimum_Distance As Integer
            Dim CountryI As List(Of String), CountryJ As List(Of String)
            Dim RegIidx As Integer, RegJidx As Integer
            For ICount% = 1 To NR
                CountryI = New List(Of String)
                RegIidx = ICount
                For Each item In (From s In cntx.Regionalization Where s.Value = RegIidx)
                    CountryI.Add(item.Key)
                Next

                For jCount% = 1 To NR
                    If ICount% = jCount% Then
                        ContiguityReg(ICount%, jCount%) = 0
                    Else
                        Minimum_Distance% = 6 'Set distance at maximum and bring down as appropriate
                        'Select countries for JCount%
                        CountryJ = New List(Of String)
                        RegJidx = jCount
                        For Each item In (From s In cntx.Regionalization Where s.Value = RegJidx)
                            CountryJ.Add(item.Key)
                        Next
                        'Now start comparing the selected countries
                        For Each Ci In CountryI
                            For Each Cj In CountryJ
                                If ContiguDistance.ContainsKey(Ci + Cj) Then
                                    'Found
                                    If ContiguDistance(Ci + Cj) < Minimum_Distance% Then
                                        Minimum_Distance% = ContiguDistance(Ci + Cj)
                                    End If
                                ElseIf ContiguDistance.ContainsKey(Cj + Ci) Then
                                    'Found
                                    If ContiguDistance(Cj + Ci) < Minimum_Distance% Then
                                        Minimum_Distance% = ContiguDistance(Cj + Ci)
                                    End If
                                End If
                            Next Cj
                        Next Ci

                        ContiguityReg(ICount%, jCount%) = Minimum_Distance
                    End If
                Next jCount%
            Next ICount%

        End Sub

        Private Sub ComputeTerritorialDisputes(cntx As IRebuildFacade)
            Using db As IFsContext = New IFsContext()
                Dim total_terdispute As Integer
                total_terdispute% = 0    ' Total no. of records in Contiguity
                For Each tbl1 As TerritorialDisputes In db.TerritorialDisputes
                    total_terdispute% = total_terdispute% + 1
                    TerritorialDispute.Add(tbl1.Region + tbl1.Region_Dispute, tbl1.Magnitude)
                Next
            End Using

            '   Prepare contiguity matrix for regions from country data
            '   -------------------------------------------------------
            Dim Maximum_TerDispute As Integer
            Dim CountryI As List(Of String), CountryJ As List(Of String)
            Dim RegIidx As Integer, RegJidx As Integer
            For ICount% = 1 To NR
                CountryI = New List(Of String)
                RegIidx = ICount
                For Each item In (From s In cntx.Regionalization Where s.Value = RegIidx)
                    CountryI.Add(item.Key)
                Next

                For jCount% = 1 To NR
                    If ICount% = jCount% Then
                        terdispute(ICount%, jCount%) = 0
                    Else
                        Maximum_TerDispute% = 0 'Set dispute at minimum and bring up as appropriate
                        'Select countries for JCount%
                        CountryJ = New List(Of String)
                        RegJidx = jCount
                        For Each item In (From s In cntx.Regionalization Where s.Value = RegJidx)
                            CountryJ.Add(item.Key)
                        Next
                        'Now start comparing the selected countries
                        For Each Ci In CountryI
                            For Each Cj In CountryJ
                                If TerritorialDispute.ContainsKey(Ci + Cj) Then
                                    'Found
                                    If TerritorialDispute(Ci + Cj) > Maximum_TerDispute% Then
                                        Maximum_TerDispute% = TerritorialDispute(Ci + Cj)
                                    End If
                                ElseIf TerritorialDispute.ContainsKey(Cj + Ci) Then
                                    'Found
                                    If TerritorialDispute(Cj + Ci) > Maximum_TerDispute% Then
                                        Maximum_TerDispute% = TerritorialDispute(Cj + Ci)
                                    End If
                                End If
                            Next Cj
                        Next Ci

                        terdispute(ICount%, jCount%) = Maximum_TerDispute%
                    End If
                Next jCount%
            Next ICount%

        End Sub

        Private Sub ComputeThreat(cntx As IRebuildFacade)
            '   Read and store country translation codes for MIDS data set (COW)
            '   ----------------------------------------------------------------
            Dim CountryCode As Dictionary(Of String, String) = New Dictionary(Of String, String)
            Dim MIDS As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
            Dim DyadCount(,) As Integer
            ReDim DyadCount(NR, NR) 'jrs 2007/01/24 'jrs 2006/10/10
            ReDim THREAT(NR, NR)

            Using db As IFsContext = New IFsContext()
                For Each CountryCodes As CountryTranslation In db.CountryTranslation
                    If CountryCodes.COWCodes > "" Then
                        'jrs 2022/03/28 when doing provinces original country is still here, need to avoid using it
                        If cntx.Regionalization.Keys.Contains(CountryCodes.Country) And Not CountryCode.ContainsKey(CountryCodes.COWCodes) Then
                            CountryCode.Add(CountryCodes.COWCodes, CountryCodes.Country)
                        End If
                    End If
                Next
            End Using

            '   Read dyadic probabilities and put into the matrix cells
            '   -------------------------------------------------------
            For Each key In cntx.Regionalization.Keys
                For Each key2 In cntx.Regionalization.Keys
                    MIDS.Add(key + key2, 0)
                Next
            Next

            Dim VLine As String, X() As String
            Dim reg1 As String, code1 As String, Reg2 As String, code2 As String, Probability As Single
            Dim Dim1 As String, Dim2 As String
            Using reader As StreamReader = New StreamReader(cntx.DataFileDir + "MIDsInitialValues\" +
                "MIDsHistoryProbabilities.csv")
                ' Read one line from file
                VLine = reader.ReadLine
                Do While Not VLine = Nothing
                    X = VLine.Split(New Char() {","c})
                    reg1$ = X(0)
                    code1$ = X(1)
                    Reg2$ = X(2)
                    code2$ = X(3)
                    Probability = CSng(X(4))

                    If CountryCode.ContainsKey(reg1) Then
                        Dim1 = CountryCode(reg1)
                    Else
                        Dim1 = ""
                    End If
                    If CountryCode.ContainsKey(Reg2) Then
                        Dim2 = CountryCode(Reg2)
                    Else
                        Dim2 = ""
                    End If
                    If Dim1 > "" And Dim2 > "" Then 'Countries exist in IFs matrix
                        MIDS(Dim1 + Dim2) = Probability * 100
                        MIDS(Dim2 + Dim1) = Probability * 100
                    End If

                    VLine = reader.ReadLine
                Loop
            End Using

            '   Initialize regional threat and dyad count
            '   -----------------------------------------
            For regco1% = 1 To NR
                For regco2% = 1 To NR
                    THREAT(regco1%, regco2%) = 0
                    DyadCount(regco1%, regco2%) = 0
                Next regco2%
            Next regco1%

            '   Sum MIDs probabilties across regions
            '   ------------------------------------
            For Each item In cntx.Regionalization
                For Each item2 In cntx.Regionalization
                    If MIDS.ContainsKey(item.Key + item2.Key) Then
                        THREAT(item.Value, item2.Value) = THREAT(item.Value, item2.Value) + CSng(MIDS(item.Key + item2.Key))
                        DyadCount(item.Value, item2.Value) = DyadCount(item.Value, item2.Value) + 1
                    End If
                Next
            Next

            '   Compute regional average of threat from regional MIDS probabilities
            '   -------------------------------------------------------------------

            For regco1% = 1 To NR
                For regco2% = 1 To NR
                    If DyadCount(regco1%, regco2%) > 1 Then
                        THREAT(regco1%, regco2%) = THREAT(regco1%, regco2%) / DyadCount(regco1%, regco2%)
                    End If
                Next regco2%
            Next regco1%

        End Sub

    End Module
End Namespace
