Imports IFs.Data

Namespace Models

    Public Module RETR

        Public Sub Retriev(IFsResults As Dictionary(Of String, IDataframe), Year As Short, ByVal FirstYear As Short)

            Dim Dim1 as Short, Dim2 as Short, Dim3 as Short, sNR As Short
            sNR = CShort(NR)

            ReDim POVSHORTSEXDIFF420(sNR, 11)
            if Year = FirstYear then
                If IFsResults("POVSHORTSEXDIFF420").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            POVSHORTSEXDIFF420(Dim1, Dim2) = CSng(if(IFsResults("POVSHORTSEXDIFF420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim INTSFLOWS(sNR, 6, 6)
            if Year = FirstYear then
                If IFsResults("INTSFLOWS").RowCount > (Year - FirstYear) * (1 * sNR * 6 * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            For Dim3 = 1 to 6
                                INTSFLOWS(Dim1, Dim2, Dim3) = CSng(if(IFsResults("INTSFLOWS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6, Dim3, 6)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim outputagprotect(sNR)
            If IFsResults("outputagprotect").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    outputagprotect(Dim1) = CSng(IFsResults("outputagprotect")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim outputenprotect(sNR)
            If IFsResults("outputenprotect").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    outputenprotect(Dim1) = CSng(IFsResults("outputenprotect")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim HHSAVSTOCK(sNR)
            if Year = FirstYear then
                If IFsResults("HHSAVSTOCK").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HHSAVSTOCK(Dim1) = CSng(IFsResults("HHSAVSTOCK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim hhsaveneedrat(sNR)
            If IFsResults("hhsaveneedrat").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hhsaveneedrat(Dim1) = CSng(IFsResults("hhsaveneedrat")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("actreaon").RowCount > (Year - FirstYear) * (1) Then
                actreaon = CSng(IFsResults("actreaon")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                actreaon = 0
            End If

            ReDim HLDALY(sNR, 15, 3)
            if Year = FirstYear then
                If IFsResults("HLDALY").RowCount > (Year - FirstYear) * (1 * sNR * 15 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 15
                            For Dim3 = 1 to 3
                                HLDALY(Dim1, Dim2, Dim3) = CSng(if(IFsResults("HLDALY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HLSMOKING(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HLSMOKING").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HLSMOKING(Dim1, Dim2) = CSng(if(IFsResults("HLSMOKING")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim edqualag15m(sNR, 3)
            If IFsResults("edqualag15m").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edqualag15m(Dim1, Dim2) = CSng(if(IFsResults("edqualag15m")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim HLOBESITY(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HLOBESITY").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HLOBESITY(Dim1, Dim2) = CSng(if(IFsResults("HLOBESITY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ASARAWDMAT(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ASARAWDMAT").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ASARAWDMAT(Dim1, Dim2) = CSng(if(IFsResults("ASARAWDMAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ASAGDPDMAT(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ASAGDPDMAT").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ASAGDPDMAT(Dim1, Dim2) = CSng(if(IFsResults("ASAGDPDMAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ASALABDMAT(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ASALABDMAT").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ASALABDMAT(Dim1, Dim2) = CSng(if(IFsResults("ASALABDMAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ASAPOPDMAT(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ASAPOPDMAT").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ASAPOPDMAT(Dim1, Dim2) = CSng(if(IFsResults("ASAPOPDMAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ASAGDPCUMCHG(sNR)
            if Year = FirstYear then
                If IFsResults("ASAGDPCUMCHG").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ASAGDPCUMCHG(Dim1) = CSng(IFsResults("ASAGDPCUMCHG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ASALABCUMCHG(sNR)
            if Year = FirstYear then
                If IFsResults("ASALABCUMCHG").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ASALABCUMCHG(Dim1) = CSng(IFsResults("ASALABCUMCHG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ASAPOPCUMCHG(sNR)
            if Year = FirstYear then
                If IFsResults("ASAPOPCUMCHG").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ASAPOPCUMCHG(Dim1) = CSng(IFsResults("ASAPOPCUMCHG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ASAGDPGRRB(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ASAGDPGRRB").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ASAGDPGRRB(Dim1, Dim2) = CSng(if(IFsResults("ASAGDPGRRB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ASALABGRRB(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ASALABGRRB").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ASALABGRRB(Dim1, Dim2) = CSng(if(IFsResults("ASALABGRRB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ASAPOPGRRB(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ASAPOPGRRB").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ASAPOPGRRB(Dim1, Dim2) = CSng(if(IFsResults("ASAPOPGRRB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ASAGDP(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ASAGDP").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ASAGDP(Dim1, Dim2) = CSng(if(IFsResults("ASAGDP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ASALAB(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ASALAB").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ASALAB(Dim1, Dim2) = CSng(if(IFsResults("ASALAB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ASAPOP(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ASAPOP").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ASAPOP(Dim1, Dim2) = CSng(if(IFsResults("ASAPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ASARAW(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ASARAW").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ASARAW(Dim1, Dim2) = CSng(if(IFsResults("ASARAW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim workageentry(sNR)
            If IFsResults("workageentry").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    workageentry(Dim1) = CSng(IFsResults("workageentry")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim AGEOFMARRIAGE(sNR, 3)
            if Year = FirstYear then
                If IFsResults("AGEOFMARRIAGE").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            AGEOFMARRIAGE(Dim1, Dim2) = CSng(if(IFsResults("AGEOFMARRIAGE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ageofmarriagetfrelas(sNR)
            If IFsResults("ageofmarriagetfrelas").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ageofmarriagetfrelas(Dim1) = CSng(IFsResults("ageofmarriagetfrelas")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ageofmarriagem(sNR, 3)
            If IFsResults("ageofmarriagem").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        ageofmarriagem(Dim1, Dim2) = CSng(if(IFsResults("ageofmarriagem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim workageretire(sNR)
            If IFsResults("workageretire").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    workageretire(Dim1) = CSng(IFsResults("workageretire")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("workageretiresv").RowCount > (Year - FirstYear) * (1) Then
                workageretiresv = CSng(IFsResults("workageretiresv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                workageretiresv = 0
            End If

            If IFsResults("envylchgadd").RowCount > (Year - FirstYear) * (1) Then
                envylchgadd = CSng(IFsResults("envylchgadd")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                envylchgadd = 0
            End If

            ReDim envylchgm(sNR)
            If IFsResults("envylchgm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    envylchgm(Dim1) = CSng(IFsResults("envylchgm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim KAG(sNR)
            if Year = FirstYear then
                If IFsResults("KAG").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        KAG(Dim1) = CSng(IFsResults("KAG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim AGDEM(sNR, 3)
            if Year = FirstYear then
                If IFsResults("AGDEM").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            AGDEM(Dim1, Dim2) = CSng(if(IFsResults("AGDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("agconv").RowCount > (Year - FirstYear) * (1) Then
                agconv = CSng(IFsResults("agconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                agconv = 0
            End If

            ReDim AGX(sNR, 3)
            If IFsResults("AGX").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        AGX(Dim1, Dim2) = CSng(if(IFsResults("AGX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim agxvaluem(sNR, 3)
            If IFsResults("agxvaluem").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        agxvaluem(Dim1, Dim2) = CSng(if(IFsResults("agxvaluem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim agxm(sNR, 3)
            If IFsResults("agxm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        agxm(Dim1, Dim2) = CSng(if(IFsResults("agxm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim AGM(sNR, 3)
            If IFsResults("AGM").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        AGM(Dim1, Dim2) = CSng(if(IFsResults("AGM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim agmvaluem(sNR, 3)
            If IFsResults("agmvaluem").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        agmvaluem(Dim1, Dim2) = CSng(if(IFsResults("agmvaluem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim agmm(sNR, 3)
            If IFsResults("agmm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        agmm(Dim1, Dim2) = CSng(if(IFsResults("agmm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim inputagrim(sNR, 6)
            If IFsResults("inputagrim").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        inputagrim(Dim1, Dim2) = CSng(if(IFsResults("inputagrim")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim RETRAT(sNR)
            If IFsResults("RETRAT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    RETRAT(Dim1) = CSng(IFsResults("RETRAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("elinag2").RowCount > (Year - FirstYear) * (1) Then
                elinag2 = CSng(IFsResults("elinag2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elinag2 = 0
            End If

            If IFsResults("elinag1").RowCount > (Year - FirstYear) * (1) Then
                elinag1 = CSng(IFsResults("elinag1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elinag1 = 0
            End If

            ReDim aginvm(sNR)
            If IFsResults("aginvm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    aginvm(Dim1) = CSng(IFsResults("aginvm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim IALK(sNR)
            if Year = FirstYear then
                If IFsResults("IALK").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        IALK(Dim1) = CSng(IFsResults("IALK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("eliasp2").RowCount > (Year - FirstYear) * (1) Then
                eliasp2 = CSng(IFsResults("eliasp2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                eliasp2 = 0
            End If

            If IFsResults("eliasp1").RowCount > (Year - FirstYear) * (1) Then
                eliasp1 = CSng(IFsResults("eliasp1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                eliasp1 = 0
            End If

            ReDim ylmax(sNR)
            If IFsResults("ylmax").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ylmax(Dim1) = CSng(IFsResults("ylmax")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim AGP(sNR, 3)
            If IFsResults("AGP").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        AGP(Dim1, Dim2) = CSng(if(IFsResults("AGP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim AGLOSSPROD(sNR, 3)
            if Year = FirstYear then
                If IFsResults("AGLOSSPROD").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            AGLOSSPROD(Dim1, Dim2) = CSng(if(IFsResults("AGLOSSPROD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim aglossprodm(sNR, 5)
            If IFsResults("aglossprodm").RowCount > (Year - FirstYear) * (1 * sNR * 5) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        aglossprodm(Dim1, Dim2) = CSng(if(IFsResults("aglossprodm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim agstkadjsw(sNR)
            If IFsResults("agstkadjsw").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    agstkadjsw(Dim1) = CSng(IFsResults("agstkadjsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim AGTEC(sNR)
            if Year = FirstYear then
                If IFsResults("AGTEC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        AGTEC(Dim1) = CSng(IFsResults("AGTEC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim XBILATAGR(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("XBILATAGR").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            XBILATAGR(Dim1, Dim2) = CSng(if(IFsResults("XBILATAGR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim agtrdadjsw(sNR)
            If IFsResults("agtrdadjsw").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    agtrdadjsw(Dim1) = CSng(IFsResults("agtrdadjsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim AGLOSSTRANS(sNR, 3)
            if Year = FirstYear then
                If IFsResults("AGLOSSTRANS").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            AGLOSSTRANS(Dim1, Dim2) = CSng(if(IFsResults("AGLOSSTRANS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim aglosstransm(sNR, 3)
            If IFsResults("aglosstransm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        aglosstransm(Dim1, Dim2) = CSng(if(IFsResults("aglosstransm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("elfdpr2").RowCount > (Year - FirstYear) * (1) Then
                elfdpr2 = CSng(IFsResults("elfdpr2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elfdpr2 = 0
            End If

            If IFsResults("elfdpr1").RowCount > (Year - FirstYear) * (1) Then
                elfdpr1 = CSng(IFsResults("elfdpr1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elfdpr1 = 0
            End If

            If IFsResults("ylmaxgr").RowCount > (Year - FirstYear) * (1) Then
                ylmaxgr = CSng(IFsResults("ylmaxgr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ylmaxgr = 0
            End If

            ReDim ylm(sNR)
            If IFsResults("ylm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ylm(Dim1) = CSng(IFsResults("ylm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INAG(sNR)
            if Year = FirstYear then
                If IFsResults("INAG").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INAG(Dim1) = CSng(IFsResults("INAG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("agon").RowCount > (Year - FirstYear) * (1) Then
                agon = CSng(IFsResults("agon")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                agon = 0
            End If

            ReDim AGXVALUE(sNR, 3)
            If IFsResults("AGXVALUE").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        AGXVALUE(Dim1, Dim2) = CSng(if(IFsResults("AGXVALUE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim AGMVALUE(sNR, 3)
            If IFsResults("AGMVALUE").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        AGMVALUE(Dim1, Dim2) = CSng(if(IFsResults("AGMVALUE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            if Year = FirstYear then
                If IFsResults("AIMACHIQ").RowCount > (Year - FirstYear) * (1) Then
                    AIMACHIQ = CSng(IFsResults("AIMACHIQ")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    AIMACHIQ = 0
                End If
            Else
                AIMACHIQ = 0
            End if

            ReDim AITASK(6)
            if Year = FirstYear then
                If IFsResults("AITASK").RowCount > (Year - FirstYear) * (1 * 6) Then
                    For Dim1 = 1 to 6
                        AITASK(Dim1) = CSng(IFsResults("AITASK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)))
                    Next Dim1
                End If
            End if

            ReDim aiddon(sNR)
            If IFsResults("aiddon").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    aiddon(Dim1) = CSng(IFsResults("aiddon")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("aidgradpoint").RowCount > (Year - FirstYear) * (1) Then
                aidgradpoint = CSng(IFsResults("aidgradpoint")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                aidgradpoint = 0
            End If

            If IFsResults("aidlpm").RowCount > (Year - FirstYear) * (1) Then
                aidlpm = CSng(IFsResults("aidlpm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                aidlpm = 0
            End If

            ReDim aidlp(sNR)
            If IFsResults("aidlp").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    aidlp(Dim1) = CSng(IFsResults("aidlp")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim aidrecadd(sNR)
            If IFsResults("aidrecadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    aidrecadd(Dim1) = CSng(IFsResults("aidrecadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim aidrecgovconp(sNR)
            If IFsResults("aidrecgovconp").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    aidrecgovconp(Dim1) = CSng(IFsResults("aidrecgovconp")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim aidrecm(sNR)
            If IFsResults("aidrecm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    aidrecm(Dim1) = CSng(IFsResults("aidrecm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim AIDSDRATE(sNR)
            if Year = FirstYear then
                If IFsResults("AIDSDRATE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        AIDSDRATE(Dim1) = CSng(IFsResults("AIDSDRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim aidsdratem(sNR)
            If IFsResults("aidsdratem").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    aidsdratem(Dim1) = CSng(IFsResults("aidsdratem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("aidsdrtadvr").RowCount > (Year - FirstYear) * (1) Then
                aidsdrtadvr = CSng(IFsResults("aidsdrtadvr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                aidsdrtadvr = 0
            End If

            ReDim AIDSDTHS(sNR)
            If IFsResults("AIDSDTHS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    AIDSDTHS(Dim1) = CSng(IFsResults("AIDSDTHS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim AIDSDTHSCM(sNR)
            if Year = FirstYear then
                If IFsResults("AIDSDTHSCM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        AIDSDTHSCM(Dim1) = CSng(IFsResults("AIDSDTHSCM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ally(sNR, sNR)
            If IFsResults("ally").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to sNR
                        ally(Dim1, Dim2) = CSng(if(IFsResults("ally")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("thrtcoefally").RowCount > (Year - FirstYear) * (1) Then
                thrtcoefally = CSng(IFsResults("thrtcoefally")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                thrtcoefally = 0
            End If

            ReDim ALLIANCEBILAT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("ALLIANCEBILAT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            ALLIANCEBILAT(Dim1, Dim2) = CSng(if(IFsResults("ALLIANCEBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim hllongvtyalpharatio(15, 3)
            If IFsResults("hllongvtyalpharatio").RowCount > (Year - FirstYear) * (1 * 15 * 3) Then
                For Dim1 = 1 to 15
                    For Dim2 = 1 to 3
                        hllongvtyalpharatio(Dim1, Dim2) = CSng(if(IFsResults("hllongvtyalpharatio")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 15, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim hllongvtyalpha(sNR, 15, 3)
            If IFsResults("hllongvtyalpha").RowCount > (Year - FirstYear) * (1 * sNR * 15 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 15
                        For Dim3 = 1 to 3
                            hllongvtyalpha(Dim1, Dim2, Dim3) = CSng(if(IFsResults("hllongvtyalpha")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("mfpconvfunc").RowCount > (Year - FirstYear) * (1) Then
                mfpconvfunc = CSng(IFsResults("mfpconvfunc")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpconvfunc = 0
            End If

            ReDim druguseamphetaminem(sNR)
            If IFsResults("druguseamphetaminem").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    druguseamphetaminem(Dim1) = CSng(IFsResults("druguseamphetaminem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ENVPRCHG(sNR)
            if Year = FirstYear then
                If IFsResults("ENVPRCHG").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ENVPRCHG(Dim1) = CSng(IFsResults("ENVPRCHG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ENVTEMP(sNR)
            if Year = FirstYear then
                If IFsResults("ENVTEMP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ENVTEMP(Dim1) = CSng(IFsResults("ENVTEMP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ENVTPCHG(sNR)
            if Year = FirstYear then
                If IFsResults("ENVTPCHG").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ENVTPCHG(Dim1) = CSng(IFsResults("ENVTPCHG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim CARANN(sNR)
            if Year = FirstYear then
                If IFsResults("CARANN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CARANN(Dim1) = CSng(IFsResults("CARANN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("femshrgr").RowCount > (Year - FirstYear) * (1) Then
                femshrgr = CSng(IFsResults("femshrgr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                femshrgr = 0
            End If

            ReDim ENVYLCHG(sNR)
            if Year = FirstYear then
                If IFsResults("ENVYLCHG").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ENVYLCHG(Dim1) = CSng(IFsResults("ENVYLCHG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLHIVART(sNR)
            if Year = FirstYear then
                If IFsResults("HLHIVART").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLHIVART(Dim1) = CSng(IFsResults("HLHIVART")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim AQUACUL(sNR)
            If IFsResults("AQUACUL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    AQUACUL(Dim1) = CSng(IFsResults("AQUACUL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim aquaculgr(sNR)
            If IFsResults("aquaculgr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    aquaculgr(Dim1) = CSng(IFsResults("aquaculgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim aquaculm(sNR)
            If IFsResults("aquaculm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    aquaculm(Dim1) = CSng(IFsResults("aquaculm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("aquaculconv").RowCount > (Year - FirstYear) * (1) Then
                aquaculconv = CSng(IFsResults("aquaculconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                aquaculconv = 0
            End If

            ReDim XARMS(sNR)
            if Year = FirstYear then
                If IFsResults("XARMS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        XARMS(Dim1) = CSng(IFsResults("XARMS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim xarmsadd(sNR)
            If IFsResults("xarmsadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    xarmsadd(Dim1) = CSng(IFsResults("xarmsadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim xarmsm(sNR)
            If IFsResults("xarmsm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    xarmsm(Dim1) = CSng(IFsResults("xarmsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim MARMS(sNR)
            if Year = FirstYear then
                If IFsResults("MARMS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MARMS(Dim1) = CSng(IFsResults("MARMS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim marmsadd(sNR)
            If IFsResults("marmsadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    marmsadd(Dim1) = CSng(IFsResults("marmsadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim marmsm(sNR)
            If IFsResults("marmsm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    marmsm(Dim1) = CSng(IFsResults("marmsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("aimachiqm").RowCount > (Year - FirstYear) * (1) Then
                aimachiqm = CSng(IFsResults("aimachiqm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                aimachiqm = 0
            End If

            ReDim aitaskm(6)
            If IFsResults("aitaskm").RowCount > (Year - FirstYear) * (1 * 6) Then
                For Dim1 = 1 to 6
                    aitaskm(Dim1) = CSng(IFsResults("aitaskm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)))
                Next Dim1
            End If

            If IFsResults("co2perm").RowCount > (Year - FirstYear) * (1) Then
                co2perm = CSng(IFsResults("co2perm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                co2perm = 0
            End If

            if Year = FirstYear then
                If IFsResults("CO2PPM").RowCount > (Year - FirstYear) * (1) Then
                    CO2PPM = CSng(IFsResults("CO2PPM")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    CO2PPM = 0
                End If
            Else
                CO2PPM = 0
            End if

            If IFsResults("sanithhlifepitlatrinebasic").RowCount > (Year - FirstYear) * (1) Then
                sanithhlifepitlatrinebasic = CSng(IFsResults("sanithhlifepitlatrinebasic")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                sanithhlifepitlatrinebasic = 0
            End If

            ReDim hlbednetsm(sNR)
            If IFsResults("hlbednetsm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hlbednetsm(Dim1) = CSng(IFsResults("hlbednetsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim HLBEDNETS(sNR)
            if Year = FirstYear then
                If IFsResults("HLBEDNETS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLBEDNETS(Dim1) = CSng(IFsResults("HLBEDNETS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim hllongvtybetaratio(15, 3)
            If IFsResults("hllongvtybetaratio").RowCount > (Year - FirstYear) * (1 * 15 * 3) Then
                For Dim1 = 1 to 15
                    For Dim2 = 1 to 3
                        hllongvtybetaratio(Dim1, Dim2) = CSng(if(IFsResults("hllongvtybetaratio")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 15, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim hllongvtybeta(sNR, 15, 3)
            If IFsResults("hllongvtybeta").RowCount > (Year - FirstYear) * (1 * sNR * 15 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 15
                        For Dim3 = 1 to 3
                            hllongvtybeta(Dim1, Dim2, Dim3) = CSng(if(IFsResults("hllongvtybeta")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim AIDBILAT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("AIDBILAT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            AIDBILAT(Dim1, Dim2) = CSng(if(IFsResults("AIDBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim XARMSBILAT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("XARMSBILAT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            XARMSBILAT(Dim1, Dim2) = CSng(if(IFsResults("XARMSBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            If IFsResults("bilateralsw").RowCount > (Year - FirstYear) * (1) Then
                bilateralsw = CSng(IFsResults("bilateralsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                bilateralsw = 0
            End If

            If IFsResults("threatconstsw").RowCount > (Year - FirstYear) * (1) Then
                threatconstsw = CSng(IFsResults("threatconstsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                threatconstsw = 0
            End If

            If IFsResults("bilateraltradeforwardlinksw").RowCount > (Year - FirstYear) * (1) Then
                bilateraltradeforwardlinksw = CSng(IFsResults("bilateraltradeforwardlinksw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                bilateraltradeforwardlinksw = 0
            End If

            ReDim XBILAT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("XBILAT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            XBILAT(Dim1, Dim2) = CSng(if(IFsResults("XBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("threattradesw").RowCount > (Year - FirstYear) * (1) Then
                threattradesw = CSng(IFsResults("threattradesw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                threattradesw = 0
            End If

            ReDim BIRTHS(sNR)
            if Year = FirstYear then
                If IFsResults("BIRTHS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        BIRTHS(Dim1) = CSng(IFsResults("BIRTHS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLBMI(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HLBMI").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HLBMI(Dim1, Dim2) = CSng(if(IFsResults("HLBMI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim hlbmim(sNR, 3)
            If IFsResults("hlbmim").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        hlbmim(Dim1, Dim2) = CSng(if(IFsResults("hlbmim")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim ICTBROADCOST(sNR)
            if Year = FirstYear then
                If IFsResults("ICTBROADCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTBROADCOST(Dim1) = CSng(IFsResults("ICTBROADCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("ictbroadcostupper").RowCount > (Year - FirstYear) * (1) Then
                ictbroadcostupper = CSng(IFsResults("ictbroadcostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictbroadcostupper = 0
            End If

            If IFsResults("ictbroadcostlower").RowCount > (Year - FirstYear) * (1) Then
                ictbroadcostlower = CSng(IFsResults("ictbroadcostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictbroadcostlower = 0
            End If

            ReDim ictbroadcostm(sNR)
            If IFsResults("ictbroadcostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictbroadcostm(Dim1) = CSng(IFsResults("ictbroadcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("ictbroadlife").RowCount > (Year - FirstYear) * (1) Then
                ictbroadlife = CSng(IFsResults("ictbroadlife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictbroadlife = 0
            End If

            If IFsResults("ictbroadfromtelem").RowCount > (Year - FirstYear) * (1) Then
                ictbroadfromtelem = CSng(IFsResults("ictbroadfromtelem")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictbroadfromtelem = 0
            End If

            ReDim ICTBROADMOBILCOST(sNR)
            if Year = FirstYear then
                If IFsResults("ICTBROADMOBILCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTBROADMOBILCOST(Dim1) = CSng(IFsResults("ICTBROADMOBILCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("ictbroadmobilcostlower").RowCount > (Year - FirstYear) * (1) Then
                ictbroadmobilcostlower = CSng(IFsResults("ictbroadmobilcostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictbroadmobilcostlower = 0
            End If

            If IFsResults("ictbroadmobilcostupper").RowCount > (Year - FirstYear) * (1) Then
                ictbroadmobilcostupper = CSng(IFsResults("ictbroadmobilcostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictbroadmobilcostupper = 0
            End If

            ReDim ictbroadmobilcostm(sNR)
            If IFsResults("ictbroadmobilcostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictbroadmobilcostm(Dim1) = CSng(IFsResults("ictbroadmobilcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("ictbroadmobillife").RowCount > (Year - FirstYear) * (1) Then
                ictbroadmobillife = CSng(IFsResults("ictbroadmobillife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictbroadmobillife = 0
            End If

            ReDim ictbroadtechighrt(sNR)
            If IFsResults("ictbroadtechighrt").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictbroadtechighrt(Dim1) = CSng(IFsResults("ictbroadtechighrt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("ictbroadteclowrt").RowCount > (Year - FirstYear) * (1) Then
                ictbroadteclowrt = CSng(IFsResults("ictbroadteclowrt")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictbroadteclowrt = 0
            End If

            If IFsResults("ictbroadtecinflection").RowCount > (Year - FirstYear) * (1) Then
                ictbroadtecinflection = CSng(IFsResults("ictbroadtecinflection")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictbroadtecinflection = 0
            End If

            If IFsResults("ictcomputerstecinflection").RowCount > (Year - FirstYear) * (1) Then
                ictcomputerstecinflection = CSng(IFsResults("ictcomputerstecinflection")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictcomputerstecinflection = 0
            End If

            ReDim ictbroadm(sNR)
            If IFsResults("ictbroadm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictbroadm(Dim1) = CSng(IFsResults("ictbroadm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GOVBUSREGIND(sNR)
            if Year = FirstYear then
                If IFsResults("GOVBUSREGIND").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVBUSREGIND(Dim1) = CSng(IFsResults("GOVBUSREGIND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim GOVBUSCOST(sNR)
            if Year = FirstYear then
                If IFsResults("GOVBUSCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVBUSCOST(Dim1) = CSng(IFsResults("GOVBUSCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            if Year = FirstYear then
                If IFsResults("CO2PER").RowCount > (Year - FirstYear) * (1) Then
                    CO2PER = CSng(IFsResults("CO2PER")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    CO2PER = 0
                End If
            Else
                CO2PER = 0
            End if

            If IFsResults("wtempadd").RowCount > (Year - FirstYear) * (1) Then
                wtempadd = CSng(IFsResults("wtempadd")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wtempadd = 0
            End If

            ReDim clpcm(sNR, 4)
            If IFsResults("clpcm").RowCount > (Year - FirstYear) * (1 * sNR * 4) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 4
                        clpcm(Dim1, Dim2) = CSng(if(IFsResults("clpcm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim clnf(sNR)
            If IFsResults("clnf").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    clnf(Dim1) = CSng(IFsResults("clnf")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("clsexp").RowCount > (Year - FirstYear) * (1) Then
                clsexp = CSng(IFsResults("clsexp")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                clsexp = 0
            End If

            ReDim CLPC(sNR, 4)
            if Year = FirstYear then
                If IFsResults("CLPC").RowCount > (Year - FirstYear) * (1 * sNR * 4) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 4
                            CLPC(Dim1, Dim2) = CSng(if(IFsResults("CLPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim CLAVAL(sNR)
            If IFsResults("CLAVAL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    CLAVAL(Dim1) = CSng(IFsResults("CLAVAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("calmax").RowCount > (Year - FirstYear) * (1) Then
                calmax = CSng(IFsResults("calmax")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                calmax = 0
            End If

            ReDim clpccvm(sNR)
            If IFsResults("clpccvm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    clpccvm(Dim1) = CSng(IFsResults("clpccvm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim clpccvr(sNR)
            If IFsResults("clpccvr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    clpccvr(Dim1) = CSng(IFsResults("clpccvr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim clpcmderm(sNR)
            If IFsResults("clpcmderm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    clpcmderm(Dim1) = CSng(IFsResults("clpcmderm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim CLPCCV(sNR)
            if Year = FirstYear then
                If IFsResults("CLPCCV").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CLPCCV(Dim1) = CSng(IFsResults("CLPCCV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim clpcexovals(sNR, 4)
            If IFsResults("clpcexovals").RowCount > (Year - FirstYear) * (1 * sNR * 4) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 4
                        clpcexovals(Dim1, Dim2) = CSng(if(IFsResults("clpcexovals")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("calmeatm").RowCount > (Year - FirstYear) * (1) Then
                calmeatm = CSng(IFsResults("calmeatm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                calmeatm = 0
            End If

            ReDim CLPCMDER(sNR)
            if Year = FirstYear then
                If IFsResults("CLPCMDER").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CLPCMDER(Dim1) = CSng(IFsResults("CLPCMDER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim CLNEED(sNR)
            If IFsResults("CLNEED").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    CLNEED(Dim1) = CSng(IFsResults("CLNEED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ECGRCAPUTGDP(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ECGRCAPUTGDP").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ECGRCAPUTGDP(Dim1, Dim2) = CSng(if(IFsResults("ECGRCAPUTGDP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("elcaput2").RowCount > (Year - FirstYear) * (1) Then
                elcaput2 = CSng(IFsResults("elcaput2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elcaput2 = 0
            End If

            If IFsResults("elaszs").RowCount > (Year - FirstYear) * (1) Then
                elaszs = CSng(IFsResults("elaszs")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elaszs = 0
            End If

            If IFsResults("elcaput1").RowCount > (Year - FirstYear) * (1) Then
                elcaput1 = CSng(IFsResults("elcaput1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elcaput1 = 0
            End If

            ReDim KS(sNR, 6)
            if Year = FirstYear then
                If IFsResults("KS").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            KS(Dim1, Dim2) = CSng(if(IFsResults("KS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim qem(sNR, 9)
            If IFsResults("qem").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        qem(Dim1, Dim2) = CSng(if(IFsResults("qem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim qeunconm(sNR, 9)
            If IFsResults("qeunconm").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        qeunconm(Dim1, Dim2) = CSng(if(IFsResults("qeunconm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("keqshr").RowCount > (Year - FirstYear) * (1) Then
                keqshr = CSng(IFsResults("keqshr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                keqshr = 0
            End If

            ReDim ksadd(sNR, 6)
            If IFsResults("ksadd").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        ksadd(Dim1, Dim2) = CSng(if(IFsResults("ksadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim ksm(sNR, 6)
            If IFsResults("ksm").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        ksm(Dim1, Dim2) = CSng(if(IFsResults("ksm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim QE(sNR, 9)
            If IFsResults("QE").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        QE(Dim1, Dim2) = CSng(if(IFsResults("QE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim salpha(6)
            If IFsResults("salpha").RowCount > (Year - FirstYear) * (1 * 6) Then
                For Dim1 = 1 to 6
                    salpha(Dim1) = CSng(IFsResults("salpha")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)))
                Next Dim1
            End If

            ReDim CARS(sNR, 11)
            If IFsResults("CARS").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        CARS(Dim1, Dim2) = CSng(if(IFsResults("CARS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim carsm(sNR, 11)
            If IFsResults("carsm").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        carsm(Dim1, Dim2) = CSng(if(IFsResults("carsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim CARSALPC(sNR)
            if Year = FirstYear then
                If IFsResults("CARSALPC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CARSALPC(Dim1) = CSng(IFsResults("CARSALPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim CARSTOT(sNR)
            If IFsResults("CARSTOT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    CARSTOT(Dim1) = CSng(IFsResults("CARSTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim carstotm(sNR)
            If IFsResults("carstotm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    carstotm(Dim1) = CSng(IFsResults("carstotm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("carsconv").RowCount > (Year - FirstYear) * (1) Then
                carsconv = CSng(IFsResults("carsconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                carsconv = 0
            End If

            If IFsResults("carabr").RowCount > (Year - FirstYear) * (1) Then
                carabr = CSng(IFsResults("carabr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                carabr = 0
            End If

            If IFsResults("carinit").RowCount > (Year - FirstYear) * (1) Then
                carinit = CSng(IFsResults("carinit")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                carinit = 0
            End If

            If IFsResults("carprein").RowCount > (Year - FirstYear) * (1) Then
                carprein = CSng(IFsResults("carprein")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                carprein = 0
            End If

            If IFsResults("carfuel3").RowCount > (Year - FirstYear) * (1) Then
                carfuel3 = CSng(IFsResults("carfuel3")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                carfuel3 = 0
            End If

            If IFsResults("carfuel2").RowCount > (Year - FirstYear) * (1) Then
                carfuel2 = CSng(IFsResults("carfuel2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                carfuel2 = 0
            End If

            If IFsResults("carfuel1").RowCount > (Year - FirstYear) * (1) Then
                carfuel1 = CSng(IFsResults("carfuel1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                carfuel1 = 0
            End If

            If IFsResults("carforst").RowCount > (Year - FirstYear) * (1) Then
                carforst = CSng(IFsResults("carforst")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                carforst = 0
            End If

            ReDim carbtax(sNR)
            If IFsResults("carbtax").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    carbtax(Dim1) = CSng(IFsResults("carbtax")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("wpsysgr").RowCount > (Year - FirstYear) * (1) Then
                wpsysgr = CSng(IFsResults("wpsysgr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpsysgr = 0
            End If

            If IFsResults("hltechlinc").RowCount > (Year - FirstYear) * (1) Then
                hltechlinc = CSng(IFsResults("hltechlinc")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hltechlinc = 0
            End If

            If IFsResults("hltechssa").RowCount > (Year - FirstYear) * (1) Then
                hltechssa = CSng(IFsResults("hltechssa")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hltechssa = 0
            End If

            If IFsResults("wpconsq").RowCount > (Year - FirstYear) * (1) Then
                wpconsq = CSng(IFsResults("wpconsq")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpconsq = 0
            End If

            If IFsResults("wpcon").RowCount > (Year - FirstYear) * (1) Then
                wpcon = CSng(IFsResults("wpcon")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpcon = 0
            End If

            If IFsResults("wptrade").RowCount > (Year - FirstYear) * (1) Then
                wptrade = CSng(IFsResults("wptrade")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wptrade = 0
            End If

            If IFsResults("wpindsw").RowCount > (Year - FirstYear) * (1) Then
                wpindsw = CSng(IFsResults("wpindsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpindsw = 0
            End If

            If IFsResults("hlstuntchlsw").RowCount > (Year - FirstYear) * (1) Then
                hlstuntchlsw = CSng(IFsResults("hlstuntchlsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlstuntchlsw = 0
            End If

            ReDim CHLMOR(sNR)
            if Year = FirstYear then
                If IFsResults("CHLMOR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CHLMOR(Dim1) = CSng(IFsResults("CHLMOR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim CHILDDTHR(sNR)
            if Year = FirstYear then
                If IFsResults("CHILDDTHR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CHILDDTHR(Dim1) = CSng(IFsResults("CHILDDTHR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("cdmf").RowCount > (Year - FirstYear) * (1) Then
                cdmf = CSng(IFsResults("cdmf")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                cdmf = 0
            End If

            ReDim CIVDM(sNR)
            if Year = FirstYear then
                If IFsResults("CIVDM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CIVDM(Dim1) = CSng(IFsResults("CIVDM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("climeconimpstk").RowCount > (Year - FirstYear) * (1) Then
                climeconimpstk = CSng(IFsResults("climeconimpstk")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                climeconimpstk = 0
            End If

            If IFsResults("climeconimpperctrysw").RowCount > (Year - FirstYear) * (1) Then
                climeconimpperctrysw = CSng(IFsResults("climeconimpperctrysw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                climeconimpperctrysw = 0
            End If

            If IFsResults("climeconimplin").RowCount > (Year - FirstYear) * (1) Then
                climeconimplin = CSng(IFsResults("climeconimplin")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                climeconimplin = 0
            End If

            If IFsResults("climeconimpsq").RowCount > (Year - FirstYear) * (1) Then
                climeconimpsq = CSng(IFsResults("climeconimpsq")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                climeconimpsq = 0
            End If

            If IFsResults("climeconimpsw").RowCount > (Year - FirstYear) * (1) Then
                climeconimpsw = CSng(IFsResults("climeconimpsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                climeconimpsw = 0
            End If

            ReDim CDALF(sNR)
            If IFsResults("CDALF").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    CDALF(Dim1) = CSng(IFsResults("CDALF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim DRUGUSECOCAINE(sNR)
            If IFsResults("DRUGUSECOCAINE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    DRUGUSECOCAINE(Dim1) = CSng(IFsResults("DRUGUSECOCAINE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim drugusecocainem(sNR)
            If IFsResults("drugusecocainem").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    drugusecocainem(Dim1) = CSng(IFsResults("drugusecocainem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("labinformcoeffbus").RowCount > (Year - FirstYear) * (1) Then
                labinformcoeffbus = CSng(IFsResults("labinformcoeffbus")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                labinformcoeffbus = 0
            End If

            If IFsResults("gdpinformcoeffcorrupt").RowCount > (Year - FirstYear) * (1) Then
                gdpinformcoeffcorrupt = CSng(IFsResults("gdpinformcoeffcorrupt")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                gdpinformcoeffcorrupt = 0
            End If

            If IFsResults("labinformcoeffed").RowCount > (Year - FirstYear) * (1) Then
                labinformcoeffed = CSng(IFsResults("labinformcoeffed")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                labinformcoeffed = 0
            End If

            If IFsResults("labinformcoeffhhtrn").RowCount > (Year - FirstYear) * (1) Then
                labinformcoeffhhtrn = CSng(IFsResults("labinformcoeffhhtrn")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                labinformcoeffhhtrn = 0
            End If

            If IFsResults("infinformcoefflabinf").RowCount > (Year - FirstYear) * (1) Then
                infinformcoefflabinf = CSng(IFsResults("infinformcoefflabinf")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infinformcoefflabinf = 0
            End If

            If IFsResults("gdpinformcoeffrand").RowCount > (Year - FirstYear) * (1) Then
                gdpinformcoeffrand = CSng(IFsResults("gdpinformcoeffrand")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                gdpinformcoeffrand = 0
            End If

            If IFsResults("gdpshadowcoeffrand").RowCount > (Year - FirstYear) * (1) Then
                gdpshadowcoeffrand = CSng(IFsResults("gdpshadowcoeffrand")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                gdpshadowcoeffrand = 0
            End If

            If IFsResults("labinformcoefftax").RowCount > (Year - FirstYear) * (1) Then
                labinformcoefftax = CSng(IFsResults("labinformcoefftax")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                labinformcoefftax = 0
            End If

            If IFsResults("gdpinformcoefflabinf").RowCount > (Year - FirstYear) * (1) Then
                gdpinformcoefflabinf = CSng(IFsResults("gdpinformcoefflabinf")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                gdpinformcoefflabinf = 0
            End If

            ReDim hlmortcdadltm(sNR, 3)
            If IFsResults("hlmortcdadltm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        hlmortcdadltm(Dim1, Dim2) = CSng(if(IFsResults("hlmortcdadltm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim hlmortcdchldm(sNR, 3)
            If IFsResults("hlmortcdchldm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        hlmortcdchldm(Dim1, Dim2) = CSng(if(IFsResults("hlmortcdchldm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim hlcomaddmort(sNR, 10)
            If IFsResults("hlcomaddmort").RowCount > (Year - FirstYear) * (1 * sNR * 10) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 10
                        hlcomaddmort(Dim1, Dim2) = CSng(if(IFsResults("hlcomaddmort")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 10)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim hlcomaddprev(sNR, 10)
            If IFsResults("hlcomaddprev").RowCount > (Year - FirstYear) * (1 * sNR * 10) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 10
                        hlcomaddprev(Dim1, Dim2) = CSng(if(IFsResults("hlcomaddprev")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 10)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDPRIPER(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDPRIPER").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDPRIPER(Dim1, Dim2) = CSng(if(IFsResults("EDPRIPER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDPRIPER15TO24(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDPRIPER15TO24").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDPRIPER15TO24(Dim1, Dim2) = CSng(if(IFsResults("EDPRIPER15TO24")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDPRIPER15TO19(sNR, 3)
            If IFsResults("EDPRIPER15TO19").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDPRIPER15TO19(Dim1, Dim2) = CSng(if(IFsResults("EDPRIPER15TO19")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDSECLOWRPERTOT(sNR, 3)
            If IFsResults("EDSECLOWRPERTOT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECLOWRPERTOT(Dim1, Dim2) = CSng(if(IFsResults("EDSECLOWRPERTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDSECLOWRPER20TO24(sNR, 3)
            If IFsResults("EDSECLOWRPER20TO24").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECLOWRPER20TO24(Dim1, Dim2) = CSng(if(IFsResults("EDSECLOWRPER20TO24")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDPRIPERTOT(sNR, 3)
            If IFsResults("EDPRIPERTOT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDPRIPERTOT(Dim1, Dim2) = CSng(if(IFsResults("EDPRIPERTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDTERPERTOT(sNR, 3)
            If IFsResults("EDTERPERTOT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDTERPERTOT(Dim1, Dim2) = CSng(if(IFsResults("EDTERPERTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDSECUPPRPERTOT(sNR, 3)
            If IFsResults("EDSECUPPRPERTOT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECUPPRPERTOT(Dim1, Dim2) = CSng(if(IFsResults("EDSECUPPRPERTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDSECUPPRPER20TO24(sNR, 3)
            If IFsResults("EDSECUPPRPER20TO24").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECUPPRPER20TO24(Dim1, Dim2) = CSng(if(IFsResults("EDSECUPPRPER20TO24")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDPRICR(sNR, 3)
            If IFsResults("EDPRICR").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDPRICR(Dim1, Dim2) = CSng(if(IFsResults("EDPRICR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDTERPER25TO29(sNR, 3)
            If IFsResults("EDTERPER25TO29").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDTERPER25TO29(Dim1, Dim2) = CSng(if(IFsResults("EDTERPER25TO29")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("ictcomputerstechighrt").RowCount > (Year - FirstYear) * (1) Then
                ictcomputerstechighrt = CSng(IFsResults("ictcomputerstechighrt")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictcomputerstechighrt = 0
            End If

            If IFsResults("ictcomputersteclowrt").RowCount > (Year - FirstYear) * (1) Then
                ictcomputersteclowrt = CSng(IFsResults("ictcomputersteclowrt")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictcomputersteclowrt = 0
            End If

            ReDim hlcondomusem(sNR)
            If IFsResults("hlcondomusem").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hlcondomusem(Dim1) = CSng(IFsResults("hlcondomusem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("wpterdisp").RowCount > (Year - FirstYear) * (1) Then
                wpterdisp = CSng(IFsResults("wpterdisp")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpterdisp = 0
            End If

            If IFsResults("confforsw").RowCount > (Year - FirstYear) * (1) Then
                confforsw = CSng(IFsResults("confforsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                confforsw = 0
            End If

            ReDim sfintlwaralltoginidom(sNR)
            If IFsResults("sfintlwaralltoginidom").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    sfintlwaralltoginidom(Dim1) = CSng(IFsResults("sfintlwaralltoginidom")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("retireconsumrat").RowCount > (Year - FirstYear) * (1) Then
                retireconsumrat = CSng(IFsResults("retireconsumrat")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                retireconsumrat = 0
            End If

            ReDim CS(sNR, 6)
            If IFsResults("CS").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        CS(Dim1, Dim2) = CSng(if(IFsResults("CS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim cconstsw(sNR)
            If IFsResults("cconstsw").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    cconstsw(Dim1) = CSng(IFsResults("cconstsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim CPCTND(sNR)
            if Year = FirstYear then
                If IFsResults("CPCTND").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CPCTND(Dim1) = CSng(IFsResults("CPCTND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim CRETIRE(sNR)
            if Year = FirstYear then
                If IFsResults("CRETIRE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CRETIRE(Dim1) = CSng(IFsResults("CRETIRE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim cexovals(sNR)
            If IFsResults("cexovals").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    cexovals(Dim1) = CSng(IFsResults("cexovals")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim contiguity(sNR, sNR)
            If IFsResults("contiguity").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to sNR
                        contiguity(Dim1, Dim2) = CSng(if(IFsResults("contiguity")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim CONTRUSE(sNR)
            If IFsResults("CONTRUSE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    CONTRUSE(Dim1) = CSng(IFsResults("CONTRUSE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("tconr").RowCount > (Year - FirstYear) * (1) Then
                tconr = CSng(IFsResults("tconr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                tconr = 0
            End If

            ReDim contrusm(sNR)
            If IFsResults("contrusm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    contrusm(Dim1) = CSng(IFsResults("contrusm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("mfpdemoc").RowCount > (Year - FirstYear) * (1) Then
                mfpdemoc = CSng(IFsResults("mfpdemoc")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpdemoc = 0
            End If

            If IFsResults("mfpconflict").RowCount > (Year - FirstYear) * (1) Then
                mfpconflict = CSng(IFsResults("mfpconflict")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpconflict = 0
            End If

            If IFsResults("mfpefree").RowCount > (Year - FirstYear) * (1) Then
                mfpefree = CSng(IFsResults("mfpefree")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpefree = 0
            End If

            ReDim mfpeconints(sNR, 6)
            If IFsResults("mfpeconints").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        mfpeconints(Dim1, Dim2) = CSng(if(IFsResults("mfpeconints")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("mfpedspn").RowCount > (Year - FirstYear) * (1) Then
                mfpedspn = CSng(IFsResults("mfpedspn")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpedspn = 0
            End If

            If IFsResults("mfpinfrel").RowCount > (Year - FirstYear) * (1) Then
                mfpinfrel = CSng(IFsResults("mfpinfrel")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpinfrel = 0
            End If

            If IFsResults("mfpinfrnt").RowCount > (Year - FirstYear) * (1) Then
                mfpinfrnt = CSng(IFsResults("mfpinfrnt")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpinfrnt = 0
            End If

            If IFsResults("mfpenpri").RowCount > (Year - FirstYear) * (1) Then
                mfpenpri = CSng(IFsResults("mfpenpri")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpenpri = 0
            End If

            If IFsResults("mfpfree").RowCount > (Year - FirstYear) * (1) Then
                mfpfree = CSng(IFsResults("mfpfree")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpfree = 0
            End If

            If IFsResults("mfpgovcor").RowCount > (Year - FirstYear) * (1) Then
                mfpgovcor = CSng(IFsResults("mfpgovcor")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpgovcor = 0
            End If

            If IFsResults("mfpgoveff").RowCount > (Year - FirstYear) * (1) Then
                mfpgoveff = CSng(IFsResults("mfpgoveff")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpgoveff = 0
            End If

            If IFsResults("mfphlspn").RowCount > (Year - FirstYear) * (1) Then
                mfphlspn = CSng(IFsResults("mfphlspn")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfphlspn = 0
            End If

            If IFsResults("mfpinfrindict").RowCount > (Year - FirstYear) * (1) Then
                mfpinfrindict = CSng(IFsResults("mfpinfrindict")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpinfrindict = 0
            End If

            If IFsResults("mfpinfict").RowCount > (Year - FirstYear) * (1) Then
                mfpinfict = CSng(IFsResults("mfpinfict")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpinfict = 0
            End If

            If IFsResults("mfplife").RowCount > (Year - FirstYear) * (1) Then
                mfplife = CSng(IFsResults("mfplife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfplife = 0
            End If

            If IFsResults("mfpiospn").RowCount > (Year - FirstYear) * (1) Then
                mfpiospn = CSng(IFsResults("mfpiospn")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpiospn = 0
            End If

            If IFsResults("mfpedqual").RowCount > (Year - FirstYear) * (1) Then
                mfpedqual = CSng(IFsResults("mfpedqual")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpedqual = 0
            End If

            If IFsResults("mfprandd").RowCount > (Year - FirstYear) * (1) Then
                mfprandd = CSng(IFsResults("mfprandd")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfprandd = 0
            End If

            If IFsResults("mfpinfrrd").RowCount > (Year - FirstYear) * (1) Then
                mfpinfrrd = CSng(IFsResults("mfpinfrrd")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpinfrrd = 0
            End If

            If IFsResults("mfpedscien").RowCount > (Year - FirstYear) * (1) Then
                mfpedscien = CSng(IFsResults("mfpedscien")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpedscien = 0
            End If

            If IFsResults("mfpstunt").RowCount > (Year - FirstYear) * (1) Then
                mfpstunt = CSng(IFsResults("mfpstunt")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpstunt = 0
            End If

            If IFsResults("mfpinfrte").RowCount > (Year - FirstYear) * (1) Then
                mfpinfrte = CSng(IFsResults("mfpinfrte")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpinfrte = 0
            End If

            If IFsResults("mfpinfrindtrad").RowCount > (Year - FirstYear) * (1) Then
                mfpinfrindtrad = CSng(IFsResults("mfpinfrindtrad")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpinfrindtrad = 0
            End If

            If IFsResults("mfpedvoc").RowCount > (Year - FirstYear) * (1) Then
                mfpedvoc = CSng(IFsResults("mfpedvoc")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpedvoc = 0
            End If

            If IFsResults("mfpedyrs").RowCount > (Year - FirstYear) * (1) Then
                mfpedyrs = CSng(IFsResults("mfpedyrs")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpedyrs = 0
            End If

            If IFsResults("mfphlyld").RowCount > (Year - FirstYear) * (1) Then
                mfphlyld = CSng(IFsResults("mfphlyld")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfphlyld = 0
            End If

            ReDim CPOW(sNR)
            if Year = FirstYear then
                If IFsResults("CPOW").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CPOW(Dim1) = CSng(IFsResults("CPOW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("cpowdf").RowCount > (Year - FirstYear) * (1) Then
                cpowdf = CSng(IFsResults("cpowdf")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                cpowdf = 0
            End If

            If IFsResults("drcpow").RowCount > (Year - FirstYear) * (1) Then
                drcpow = CSng(IFsResults("drcpow")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                drcpow = 0
            End If

            ReDim CWAR(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("CWAR").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            CWAR(Dim1, Dim2) = CSng(if(IFsResults("CWAR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim cwarbase(sNR, sNR)
            If IFsResults("cwarbase").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to sNR
                        cwarbase(Dim1, Dim2) = CSng(if(IFsResults("cwarbase")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("cwarf").RowCount > (Year - FirstYear) * (1) Then
                cwarf = CSng(IFsResults("cwarf")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                cwarf = 0
            End If

            ReDim CWARPB(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("CWARPB").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            CWARPB(Dim1, Dim2) = CSng(if(IFsResults("CWARPB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("cwarsv").RowCount > (Year - FirstYear) * (1) Then
                cwarsv = CSng(IFsResults("cwarsv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                cwarsv = 0
            End If

            If IFsResults("mfpconv2").RowCount > (Year - FirstYear) * (1) Then
                mfpconv2 = CSng(IFsResults("mfpconv2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpconv2 = 0
            End If

            ReDim hllongvtyalphaconv(sNR, 15)
            If IFsResults("hllongvtyalphaconv").RowCount > (Year - FirstYear) * (1 * sNR * 15) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 15
                        hllongvtyalphaconv(Dim1, Dim2) = CSng(if(IFsResults("hllongvtyalphaconv")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("extmaposnconvtimeelectran").RowCount > (Year - FirstYear) * (1) Then
                extmaposnconvtimeelectran = CSng(IFsResults("extmaposnconvtimeelectran")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                extmaposnconvtimeelectran = 0
            End If

            If IFsResults("extmaposnconvtimeroadpaved").RowCount > (Year - FirstYear) * (1) Then
                extmaposnconvtimeroadpaved = CSng(IFsResults("extmaposnconvtimeroadpaved")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                extmaposnconvtimeroadpaved = 0
            End If

            If IFsResults("extmaposnconvtimeroad").RowCount > (Year - FirstYear) * (1) Then
                extmaposnconvtimeroad = CSng(IFsResults("extmaposnconvtimeroad")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                extmaposnconvtimeroad = 0
            End If

            ReDim COOKSTOVES(sNR, 3)
            if Year = FirstYear then
                If IFsResults("COOKSTOVES").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            COOKSTOVES(Dim1, Dim2) = CSng(if(IFsResults("COOKSTOVES")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim cookstovesadd(sNR, 3)
            If IFsResults("cookstovesadd").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        cookstovesadd(Dim1, Dim2) = CSng(if(IFsResults("cookstovesadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim ictcybevcost(4, 3)
            If IFsResults("ictcybevcost").RowCount > (Year - FirstYear) * (1 * 4 * 3) Then
                For Dim1 = 1 to 4
                    For Dim2 = 1 to 3
                        ictcybevcost(Dim1, Dim2) = CSng(if(IFsResults("ictcybevcost")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim LANDIRCOST(sNR)
            If IFsResults("LANDIRCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    LANDIRCOST(Dim1) = CSng(IFsResults("LANDIRCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("landircostupper").RowCount > (Year - FirstYear) * (1) Then
                landircostupper = CSng(IFsResults("landircostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                landircostupper = 0
            End If

            If IFsResults("landircostlower").RowCount > (Year - FirstYear) * (1) Then
                landircostlower = CSng(IFsResults("landircostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                landircostlower = 0
            End If

            ReDim landircostm(sNR)
            If IFsResults("landircostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    landircostm(Dim1) = CSng(IFsResults("landircostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim CLD(sNR)
            if Year = FirstYear then
                If IFsResults("CLD").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CLD(Dim1) = CSng(IFsResults("CLD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim govbuscostm(sNR)
            If IFsResults("govbuscostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govbuscostm(Dim1) = CSng(IFsResults("govbuscostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SAMADMISSIONTOTCOST(sNR)
            if Year = FirstYear then
                If IFsResults("SAMADMISSIONTOTCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SAMADMISSIONTOTCOST(Dim1) = CSng(IFsResults("SAMADMISSIONTOTCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim samadmissioncost(sNR)
            If IFsResults("samadmissioncost").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    samadmissioncost(Dim1) = CSng(IFsResults("samadmissioncost")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("wpsovcount").RowCount > (Year - FirstYear) * (1) Then
                wpsovcount = CSng(IFsResults("wpsovcount")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpsovcount = 0
            End If

            If IFsResults("envco2fert").RowCount > (Year - FirstYear) * (1) Then
                envco2fert = CSng(IFsResults("envco2fert")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                envco2fert = 0
            End If

            ReDim elascd(sNR)
            If IFsResults("elascd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    elascd(Dim1) = CSng(IFsResults("elascd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ldcropm(sNR)
            If IFsResults("ldcropm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ldcropm(Dim1) = CSng(IFsResults("ldcropm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim CBR(sNR)
            If IFsResults("CBR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    CBR(Dim1) = CSng(IFsResults("CBR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim CDR(sNR)
            If IFsResults("CDR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    CDR(Dim1) = CSng(IFsResults("CDR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim tgrld(sNR)
            If IFsResults("tgrld").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    tgrld(Dim1) = CSng(IFsResults("tgrld")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim CULTREG(sNR)
            If IFsResults("CULTREG").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    CULTREG(Dim1) = CSng(IFsResults("CULTREG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim cultshmp(11)
            If IFsResults("cultshmp").RowCount > (Year - FirstYear) * (1 * 11) Then
                For Dim1 = 1 to 11
                    cultshmp(Dim1) = CSng(IFsResults("cultshmp")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 11)))
                Next Dim1
            End If

            ReDim cultshseload(11)
            If IFsResults("cultshseload").RowCount > (Year - FirstYear) * (1 * 11) Then
                For Dim1 = 1 to 11
                    cultshseload(Dim1) = CSng(IFsResults("cultshseload")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 11)))
                Next Dim1
            End If

            ReDim cultshse(11)
            If IFsResults("cultshse").RowCount > (Year - FirstYear) * (1 * 11) Then
                For Dim1 = 1 to 11
                    cultshse(Dim1) = CSng(IFsResults("cultshse")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 11)))
                Next Dim1
            End If

            ReDim cultshtsload(11)
            If IFsResults("cultshtsload").RowCount > (Year - FirstYear) * (1 * 11) Then
                For Dim1 = 1 to 11
                    cultshtsload(Dim1) = CSng(IFsResults("cultshtsload")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 11)))
                Next Dim1
            End If

            ReDim cultshts(11)
            If IFsResults("cultshts").RowCount > (Year - FirstYear) * (1 * 11) Then
                For Dim1 = 1 to 11
                    cultshts(Dim1) = CSng(IFsResults("cultshts")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 11)))
                Next Dim1
            End If

            ReDim CURACT(sNR)
            if Year = FirstYear then
                If IFsResults("CURACT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CURACT(Dim1) = CSng(IFsResults("CURACT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim CURACTDATABOTTOM(sNR)
            If IFsResults("CURACTDATABOTTOM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    CURACTDATABOTTOM(Dim1) = CSng(IFsResults("CURACTDATABOTTOM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim CURACTDATATOP(sNR)
            If IFsResults("CURACTDATATOP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    CURACTDATATOP(Dim1) = CSng(IFsResults("CURACTDATATOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim CURACTNETPRI(sNR)
            If IFsResults("CURACTNETPRI").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    CURACTNETPRI(Dim1) = CSng(IFsResults("CURACTNETPRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim CURACTNETSEC(sNR)
            If IFsResults("CURACTNETSEC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    CURACTNETSEC(Dim1) = CSng(IFsResults("CURACTNETSEC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ictconsurplusm(sNR)
            If IFsResults("ictconsurplusm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictconsurplusm(Dim1) = CSng(IFsResults("ictconsurplusm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ICTCONSURPLUS(sNR)
            if Year = FirstYear then
                If IFsResults("ICTCONSURPLUS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTCONSURPLUS(Dim1) = CSng(IFsResults("ICTCONSURPLUS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ictcybbenefitm(sNR)
            If IFsResults("ictcybbenefitm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictcybbenefitm(Dim1) = CSng(IFsResults("ictcybbenefitm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ictcybevtcostm(sNR, 4)
            If IFsResults("ictcybevtcostm").RowCount > (Year - FirstYear) * (1 * sNR * 4) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 4
                        ictcybevtcostm(Dim1, Dim2) = CSng(if(IFsResults("ictcybevtcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim ictcybopcostadd(sNR)
            If IFsResults("ictcybopcostadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictcybopcostadd(Dim1) = CSng(IFsResults("ictcybopcostadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ictcybsecspendm(sNR)
            If IFsResults("ictcybsecspendm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictcybsecspendm(Dim1) = CSng(IFsResults("ictcybsecspendm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ICTCYBEVTCOST(sNR)
            if Year = FirstYear then
                If IFsResults("ICTCYBEVTCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTCYBEVTCOST(Dim1) = CSng(IFsResults("ICTCYBEVTCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ICTCYBSECSPEND(sNR)
            if Year = FirstYear then
                If IFsResults("ICTCYBSECSPEND").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTCYBSECSPEND(Dim1) = CSng(IFsResults("ICTCYBSECSPEND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ICTCYBOPCOST(sNR)
            if Year = FirstYear then
                If IFsResults("ICTCYBOPCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTCYBOPCOST(Dim1) = CSng(IFsResults("ICTCYBOPCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ICTCYBBENEFITCUMNET(sNR)
            if Year = FirstYear then
                If IFsResults("ICTCYBBENEFITCUMNET").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTCYBBENEFITCUMNET(Dim1) = CSng(IFsResults("ICTCYBBENEFITCUMNET")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ICTCYBBENEFIT(sNR)
            if Year = FirstYear then
                If IFsResults("ICTCYBBENEFIT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTCYBBENEFIT(Dim1) = CSng(IFsResults("ICTCYBBENEFIT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ICTCYBRISKTOT(sNR)
            if Year = FirstYear then
                If IFsResults("ICTCYBRISKTOT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTCYBRISKTOT(Dim1) = CSng(IFsResults("ICTCYBRISKTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ICTCYBRISKTOTCUM(sNR)
            if Year = FirstYear then
                If IFsResults("ICTCYBRISKTOTCUM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTCYBRISKTOTCUM(Dim1) = CSng(IFsResults("ICTCYBRISKTOTCUM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ictcybsecuritym(sNR)
            If IFsResults("ictcybsecuritym").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictcybsecuritym(Dim1) = CSng(IFsResults("ictcybsecuritym")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ICTCYBBENEFITNET(sNR)
            if Year = FirstYear then
                If IFsResults("ICTCYBBENEFITNET").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTCYBBENEFITNET(Dim1) = CSng(IFsResults("ICTCYBBENEFITNET")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ICTCYBBENTOT(sNR)
            if Year = FirstYear then
                If IFsResults("ICTCYBBENTOT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTCYBBENTOT(Dim1) = CSng(IFsResults("ICTCYBBENTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ICTCYBBENTOTCUM(sNR)
            if Year = FirstYear then
                If IFsResults("ICTCYBBENTOTCUM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTCYBBENTOTCUM(Dim1) = CSng(IFsResults("ICTCYBBENTOTCUM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim GRAMSPC(sNR, 3)
            if Year = FirstYear then
                If IFsResults("GRAMSPC").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            GRAMSPC(Dim1, Dim2) = CSng(if(IFsResults("GRAMSPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("hlstddthsw").RowCount > (Year - FirstYear) * (1) Then
                hlstddthsw = CSng(IFsResults("hlstddthsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlstddthsw = 0
            End If

            ReDim DEATHCAT(sNR, 15, 3)
            if Year = FirstYear then
                If IFsResults("DEATHCAT").RowCount > (Year - FirstYear) * (1 * sNR * 15 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 15
                            For Dim3 = 1 to 3
                                DEATHCAT(Dim1, Dim2, Dim3) = CSng(if(IFsResults("DEATHCAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HLCOMADDDTHS(sNR, 10)
            if Year = FirstYear then
                If IFsResults("HLCOMADDDTHS").RowCount > (Year - FirstYear) * (1 * sNR * 10) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 10
                            HLCOMADDDTHS(Dim1, Dim2) = CSng(if(IFsResults("HLCOMADDDTHS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 10)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim SVDTHSCONFLICT(sNR)
            If IFsResults("SVDTHSCONFLICT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SVDTHSCONFLICT(Dim1) = CSng(IFsResults("SVDTHSCONFLICT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SVDTHSPOLICS(sNR)
            If IFsResults("SVDTHSPOLICS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SVDTHSPOLICS(Dim1) = CSng(IFsResults("SVDTHSPOLICS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SVDTHSSELFHARM(sNR)
            If IFsResults("SVDTHSSELFHARM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SVDTHSSELFHARM(Dim1) = CSng(IFsResults("SVDTHSSELFHARM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SOCIETALVIOLENCEDEATHS(sNR)
            If IFsResults("SOCIETALVIOLENCEDEATHS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SOCIETALVIOLENCEDEATHS(Dim1) = CSng(IFsResults("SOCIETALVIOLENCEDEATHS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SVDTHSWOMENTOT(sNR)
            if Year = FirstYear then
                If IFsResults("SVDTHSWOMENTOT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SVDTHSWOMENTOT(Dim1) = CSng(IFsResults("SVDTHSWOMENTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim SVDTHSCHILTOT(sNR)
            if Year = FirstYear then
                If IFsResults("SVDTHSCHILTOT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SVDTHSCHILTOT(Dim1) = CSng(IFsResults("SVDTHSCHILTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim SVDTHSCONFLICTTOT(sNR)
            if Year = FirstYear then
                If IFsResults("SVDTHSCONFLICTTOT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SVDTHSCONFLICTTOT(Dim1) = CSng(IFsResults("SVDTHSCONFLICTTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim SVDTHSOTHERINTERTOT(sNR)
            if Year = FirstYear then
                If IFsResults("SVDTHSOTHERINTERTOT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SVDTHSOTHERINTERTOT(Dim1) = CSng(IFsResults("SVDTHSOTHERINTERTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim SVDTHSPOLICSTOT(sNR)
            if Year = FirstYear then
                If IFsResults("SVDTHSPOLICSTOT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SVDTHSPOLICSTOT(Dim1) = CSng(IFsResults("SVDTHSPOLICSTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim SVDTHSSELFHARMTOT(sNR)
            if Year = FirstYear then
                If IFsResults("SVDTHSSELFHARMTOT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SVDTHSSELFHARMTOT(Dim1) = CSng(IFsResults("SVDTHSSELFHARMTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim SVDTHSSOCIETALVIOLENCETOT(sNR)
            if Year = FirstYear then
                If IFsResults("SVDTHSSOCIETALVIOLENCETOT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SVDTHSSOCIETALVIOLENCETOT(Dim1) = CSng(IFsResults("SVDTHSSOCIETALVIOLENCETOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim deathtrpvm(sNR)
            If IFsResults("deathtrpvm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    deathtrpvm(Dim1) = CSng(IFsResults("deathtrpvm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim DEATHS(sNR)
            if Year = FirstYear then
                If IFsResults("DEATHS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        DEATHS(Dim1) = CSng(IFsResults("DEATHS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim SVDTHSOTHERINTERPERSON(sNR)
            If IFsResults("SVDTHSOTHERINTERPERSON").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SVDTHSOTHERINTERPERSON(Dim1) = CSng(IFsResults("SVDTHSOTHERINTERPERSON")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SVDTHSWOMENCHILDREN(sNR)
            If IFsResults("SVDTHSWOMENCHILDREN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SVDTHSWOMENCHILDREN(Dim1) = CSng(IFsResults("SVDTHSWOMENCHILDREN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("hivpeakdef").RowCount > (Year - FirstYear) * (1) Then
                hivpeakdef = CSng(IFsResults("hivpeakdef")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hivpeakdef = 0
            End If

            If IFsResults("thrtcoefcontig").RowCount > (Year - FirstYear) * (1) Then
                thrtcoefcontig = CSng(IFsResults("thrtcoefcontig")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                thrtcoefcontig = 0
            End If

            If IFsResults("thrtcoefdem").RowCount > (Year - FirstYear) * (1) Then
                thrtcoefdem = CSng(IFsResults("thrtcoefdem")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                thrtcoefdem = 0
            End If

            If IFsResults("democimpus").RowCount > (Year - FirstYear) * (1) Then
                democimpus = CSng(IFsResults("democimpus")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                democimpus = 0
            End If

            If IFsResults("democimpoth").RowCount > (Year - FirstYear) * (1) Then
                democimpoth = CSng(IFsResults("democimpoth")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                democimpoth = 0
            End If

            If IFsResults("democimpusn").RowCount > (Year - FirstYear) * (1) Then
                democimpusn = CSng(IFsResults("democimpusn")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                democimpusn = 0
            End If

            If IFsResults("eldemocimp").RowCount > (Year - FirstYear) * (1) Then
                eldemocimp = CSng(IFsResults("eldemocimp")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                eldemocimp = 0
            End If

            If IFsResults("DEMOCWVCOUNT").RowCount > (Year - FirstYear) * (1) Then
                DEMOCWVCOUNT = CSng(IFsResults("DEMOCWVCOUNT")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                DEMOCWVCOUNT = 0
            End If

            If IFsResults("DEMOCWVDIR").RowCount > (Year - FirstYear) * (1) Then
                DEMOCWVDIR = CSng(IFsResults("DEMOCWVDIR")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                DEMOCWVDIR = 0
            End If

            If IFsResults("democwvlen").RowCount > (Year - FirstYear) * (1) Then
                democwvlen = CSng(IFsResults("democwvlen")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                democwvlen = 0
            End If

            If IFsResults("democwvmax").RowCount > (Year - FirstYear) * (1) Then
                democwvmax = CSng(IFsResults("democwvmax")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                democwvmax = 0
            End If

            If IFsResults("DEMOCWAVE").RowCount > (Year - FirstYear) * (1) Then
                DEMOCWAVE = CSng(IFsResults("DEMOCWAVE")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                DEMOCWAVE = 0
            End If

            ReDim democm(sNR)
            If IFsResults("democm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    democm(Dim1) = CSng(IFsResults("democm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("democconv").RowCount > (Year - FirstYear) * (1) Then
                democconv = CSng(IFsResults("democconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                democconv = 0
            End If

            If IFsResults("dkl").RowCount > (Year - FirstYear) * (1) Then
                dkl = CSng(IFsResults("dkl")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                dkl = 0
            End If

            ReDim waterdesalinatedgr(sNR)
            If IFsResults("waterdesalinatedgr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    waterdesalinatedgr(Dim1) = CSng(IFsResults("waterdesalinatedgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim waterdesalinatedm(sNR)
            If IFsResults("waterdesalinatedm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    waterdesalinatedm(Dim1) = CSng(IFsResults("waterdesalinatedm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("desald").RowCount > (Year - FirstYear) * (1) Then
                desald = CSng(IFsResults("desald")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                desald = 0
            End If

            If IFsResults("desali").RowCount > (Year - FirstYear) * (1) Then
                desali = CSng(IFsResults("desali")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                desali = 0
            End If

            If IFsResults("dstlen").RowCount > (Year - FirstYear) * (1) Then
                dstlen = CSng(IFsResults("dstlen")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                dstlen = 0
            End If

            If IFsResults("dstl").RowCount > (Year - FirstYear) * (1) Then
                dstl = CSng(IFsResults("dstl")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                dstl = 0
            End If

            ReDim HLDIABETESRR(sNR, 7, 3)
            if Year = FirstYear then
                If IFsResults("HLDIABETESRR").RowCount > (Year - FirstYear) * (1 * sNR * 7 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 7
                            For Dim3 = 1 to 3
                                HLDIABETESRR(Dim1, Dim2, Dim3) = CSng(if(IFsResults("HLDIABETESRR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POWER(sNR)
            if Year = FirstYear then
                If IFsResults("POWER").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POWER(Dim1) = CSng(IFsResults("POWER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLYLD(sNR, 15, 3)
            if Year = FirstYear then
                If IFsResults("HLYLD").RowCount > (Year - FirstYear) * (1 * sNR * 15 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 15
                            For Dim3 = 1 to 3
                                HLYLD(Dim1, Dim2, Dim3) = CSng(if(IFsResults("HLYLD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HLYLDWORK(sNR, 15, 3)
            if Year = FirstYear then
                If IFsResults("HLYLDWORK").RowCount > (Year - FirstYear) * (1 * sNR * 15 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 15
                            For Dim3 = 1 to 3
                                HLYLDWORK(Dim1, Dim2, Dim3) = CSng(if(IFsResults("HLYLDWORK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("thrtcoefdist").RowCount > (Year - FirstYear) * (1) Then
                thrtcoefdist = CSng(IFsResults("thrtcoefdist")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                thrtcoefdist = 0
            End If

            ReDim diplodistancetoeu(sNR)
            If IFsResults("diplodistancetoeu").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    diplodistancetoeu(Dim1) = CSng(IFsResults("diplodistancetoeu")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("govsecrskdiv").RowCount > (Year - FirstYear) * (1) Then
                govsecrskdiv = CSng(IFsResults("govsecrskdiv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                govsecrskdiv = 0
            End If

            ReDim GOVTDEBTD(sNR)
            If IFsResults("GOVTDEBTD").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    GOVTDEBTD(Dim1) = CSng(IFsResults("GOVTDEBTD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ENSHO(sNR)
            if Year = FirstYear then
                If IFsResults("ENSHO").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ENSHO(Dim1) = CSng(IFsResults("ENSHO")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ENST(sNR)
            if Year = FirstYear then
                If IFsResults("ENST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ENST(Dim1) = CSng(IFsResults("ENST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ginidomcnstsw(sNR)
            If IFsResults("ginidomcnstsw").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ginidomcnstsw(Dim1) = CSng(IFsResults("ginidomcnstsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ginidomr(sNR)
            If IFsResults("ginidomr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ginidomr(Dim1) = CSng(IFsResults("ginidomr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ginidomm(sNR)
            If IFsResults("ginidomm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ginidomm(Dim1) = CSng(IFsResults("ginidomm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GOVREVDOM(sNR)
            if Year = FirstYear then
                If IFsResults("GOVREVDOM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVREVDOM(Dim1) = CSng(IFsResults("GOVREVDOM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("hlgbdconvDown").RowCount > (Year - FirstYear) * (1) Then
                hlgbdconvDown = CSng(IFsResults("hlgbdconvDown")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlgbdconvDown = 0
            End If

            ReDim EDSECLOWRDRPGRD(sNR, 8, 3)
            if Year = FirstYear then
                If IFsResults("EDSECLOWRDRPGRD").RowCount > (Year - FirstYear) * (1 * sNR * 8 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 8
                            For Dim3 = 1 to 3
                                EDSECLOWRDRPGRD(Dim1, Dim2, Dim3) = CSng(if(IFsResults("EDSECLOWRDRPGRD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 8, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDPRIDRPGRD(sNR, 8, 3)
            if Year = FirstYear then
                If IFsResults("EDPRIDRPGRD").RowCount > (Year - FirstYear) * (1 * sNR * 8 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 8
                            For Dim3 = 1 to 3
                                EDPRIDRPGRD(Dim1, Dim2, Dim3) = CSng(if(IFsResults("EDPRIDRPGRD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 8, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim druginitsw(sNR)
            If IFsResults("druginitsw").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    druginitsw(Dim1) = CSng(IFsResults("druginitsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim drugglobalconvsw(sNR)
            If IFsResults("drugglobalconvsw").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    drugglobalconvsw(Dim1) = CSng(IFsResults("drugglobalconvsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim drugglobalconv(sNR)
            If IFsResults("drugglobalconv").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    drugglobalconv(Dim1) = CSng(IFsResults("drugglobalconv")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim DRUGUSEAMPHETAMINE(sNR)
            If IFsResults("DRUGUSEAMPHETAMINE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    DRUGUSEAMPHETAMINE(Dim1) = CSng(IFsResults("DRUGUSEAMPHETAMINE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim drugusepolyindex(sNR)
            If IFsResults("drugusepolyindex").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    drugusepolyindex(Dim1) = CSng(IFsResults("drugusepolyindex")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edprilen(sNR)
            If IFsResults("edprilen").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edprilen(Dim1) = CSng(IFsResults("edprilen")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("wpgreat1").RowCount > (Year - FirstYear) * (1) Then
                wpgreat1 = CSng(IFsResults("wpgreat1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpgreat1 = 0
            End If

            If IFsResults("wpgreat2").RowCount > (Year - FirstYear) * (1) Then
                wpgreat2 = CSng(IFsResults("wpgreat2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpgreat2 = 0
            End If

            If IFsResults("migratebalancesw").RowCount > (Year - FirstYear) * (1) Then
                migratebalancesw = CSng(IFsResults("migratebalancesw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                migratebalancesw = 0
            End If

            If IFsResults("migratesw").RowCount > (Year - FirstYear) * (1) Then
                migratesw = CSng(IFsResults("migratesw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                migratesw = 0
            End If

            ReDim CAPUT(sNR, 6)
            If IFsResults("CAPUT").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        CAPUT(Dim1, Dim2) = CSng(if(IFsResults("CAPUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim caputtar(sNR, 6)
            If IFsResults("caputtar").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        caputtar(Dim1, Dim2) = CSng(if(IFsResults("caputtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim ECONFREE(sNR)
            If IFsResults("ECONFREE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ECONFREE(Dim1) = CSng(IFsResults("ECONFREE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim econfreem(sNR)
            If IFsResults("econfreem").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    econfreem(Dim1) = CSng(IFsResults("econfreem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim CLIMECONIMP(sNR)
            if Year = FirstYear then
                If IFsResults("CLIMECONIMP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CLIMECONIMP(Dim1) = CSng(IFsResults("CLIMECONIMP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ECONINTEG(sNR)
            if Year = FirstYear then
                If IFsResults("ECONINTEG").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ECONINTEG(Dim1) = CSng(IFsResults("ECONINTEG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("edmodelsw").RowCount > (Year - FirstYear) * (1) Then
                edmodelsw = CSng(IFsResults("edmodelsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edmodelsw = 0
            End If

            ReDim hdiedyrsmaxun(3)
            If IFsResults("hdiedyrsmaxun").RowCount > (Year - FirstYear) * (1 * 3) Then
                For Dim1 = 1 to 3
                    hdiedyrsmaxun(Dim1) = CSng(IFsResults("hdiedyrsmaxun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)))
                Next Dim1
            End If

            ReDim hdiedyrsminun(3)
            If IFsResults("hdiedyrsminun").RowCount > (Year - FirstYear) * (1 * 3) Then
                For Dim1 = 1 to 3
                    hdiedyrsminun(Dim1) = CSng(IFsResults("hdiedyrsminun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)))
                Next Dim1
            End If

            ReDim edbudgon(sNR)
            If IFsResults("edbudgon").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edbudgon(Dim1) = CSng(IFsResults("edbudgon")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edqtqltrm(sNR, 4)
            If IFsResults("edqtqltrm").RowCount > (Year - FirstYear) * (1 * sNR * 4) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 4
                        edqtqltrm(Dim1, Dim2) = CSng(if(IFsResults("edqtqltrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim hdiedslemaxun(3)
            If IFsResults("hdiedslemaxun").RowCount > (Year - FirstYear) * (1 * 3) Then
                For Dim1 = 1 to 3
                    hdiedslemaxun(Dim1) = CSng(IFsResults("hdiedslemaxun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)))
                Next Dim1
            End If

            ReDim hdiedsleminun(3)
            If IFsResults("hdiedsleminun").RowCount > (Year - FirstYear) * (1 * 3) Then
                For Dim1 = 1 to 3
                    hdiedsleminun(Dim1) = CSng(IFsResults("hdiedsleminun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)))
                Next Dim1
            End If

            If IFsResults("edenrsecur").RowCount > (Year - FirstYear) * (1) Then
                edenrsecur = CSng(IFsResults("edenrsecur")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edenrsecur = 0
            End If

            ReDim infraelecacctrgtval(sNR)
            If IFsResults("infraelecacctrgtval").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraelecacctrgtval(Dim1) = CSng(IFsResults("infraelecacctrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INFRAELECACCPOP(sNR, 3)
            If IFsResults("INFRAELECACCPOP").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        INFRAELECACCPOP(Dim1, Dim2) = CSng(if(IFsResults("INFRAELECACCPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim INFRAELECACC(sNR, 3)
            if Year = FirstYear then
                If IFsResults("INFRAELECACC").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            INFRAELECACC(Dim1, Dim2) = CSng(if(IFsResults("INFRAELECACC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim INFRAELECACCDEM(sNR, 3)
            if Year = FirstYear then
                If IFsResults("INFRAELECACCDEM").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            INFRAELECACCDEM(Dim1, Dim2) = CSng(if(IFsResults("INFRAELECACCDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim infraelecaccsetar(sNR)
            If IFsResults("infraelecaccsetar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraelecaccsetar(Dim1) = CSng(IFsResults("infraelecaccsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim infraelecaccseyrtar(sNR)
            If IFsResults("infraelecaccseyrtar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraelecaccseyrtar(Dim1) = CSng(IFsResults("infraelecaccseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim infraelecaccm(sNR, 3)
            If IFsResults("infraelecaccm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        infraelecaccm(Dim1, Dim2) = CSng(if(IFsResults("infraelecaccm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim INFRAELECADJFACT(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAELECADJFACT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAELECADJFACT(Dim1) = CSng(IFsResults("INFRAELECADJFACT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("infraelecaccruralcostupper").RowCount > (Year - FirstYear) * (1) Then
                infraelecaccruralcostupper = CSng(IFsResults("infraelecaccruralcostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraelecaccruralcostupper = 0
            End If

            If IFsResults("infraelecaccruralcostlower").RowCount > (Year - FirstYear) * (1) Then
                infraelecaccruralcostlower = CSng(IFsResults("infraelecaccruralcostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraelecaccruralcostlower = 0
            End If

            ReDim infraelecaccruralcostm(sNR)
            If IFsResults("infraelecaccruralcostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraelecaccruralcostm(Dim1) = CSng(IFsResults("infraelecaccruralcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("infraelecaccurbancostupper").RowCount > (Year - FirstYear) * (1) Then
                infraelecaccurbancostupper = CSng(IFsResults("infraelecaccurbancostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraelecaccurbancostupper = 0
            End If

            If IFsResults("infraelecaccurbancostlower").RowCount > (Year - FirstYear) * (1) Then
                infraelecaccurbancostlower = CSng(IFsResults("infraelecaccurbancostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraelecaccurbancostlower = 0
            End If

            ReDim infraelecaccurbancostm(sNR)
            If IFsResults("infraelecaccurbancostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraelecaccurbancostm(Dim1) = CSng(IFsResults("infraelecaccurbancostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INFRAELECACCRURALCOST(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAELECACCRURALCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAELECACCRURALCOST(Dim1) = CSng(IFsResults("INFRAELECACCRURALCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim INFRAELECACCURBANCOST(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAELECACCURBANCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAELECACCURBANCOST(Dim1) = CSng(IFsResults("INFRAELECACCURBANCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim INFRAELECGENCAP(sNR)
            If IFsResults("INFRAELECGENCAP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INFRAELECGENCAP(Dim1) = CSng(IFsResults("INFRAELECGENCAP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INFRAELECGENCAPDEM(sNR)
            If IFsResults("INFRAELECGENCAPDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INFRAELECGENCAPDEM(Dim1) = CSng(IFsResults("INFRAELECGENCAPDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim infraelecgencapm(sNR)
            If IFsResults("infraelecgencapm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraelecgencapm(Dim1) = CSng(IFsResults("infraelecgencapm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INFRAELECGENCOST(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAELECGENCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAELECGENCOST(Dim1) = CSng(IFsResults("INFRAELECGENCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("infraelecgencostupper").RowCount > (Year - FirstYear) * (1) Then
                infraelecgencostupper = CSng(IFsResults("infraelecgencostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraelecgencostupper = 0
            End If

            If IFsResults("infraelecgencostlower").RowCount > (Year - FirstYear) * (1) Then
                infraelecgencostlower = CSng(IFsResults("infraelecgencostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraelecgencostlower = 0
            End If

            ReDim infraelecgencostm(sNR)
            If IFsResults("infraelecgencostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraelecgencostm(Dim1) = CSng(IFsResults("infraelecgencostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("infraelecgenlife").RowCount > (Year - FirstYear) * (1) Then
                infraelecgenlife = CSng(IFsResults("infraelecgenlife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraelecgenlife = 0
            End If

            ReDim INFRAELECPROD(sNR)
            If IFsResults("INFRAELECPROD").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INFRAELECPROD(Dim1) = CSng(IFsResults("INFRAELECPROD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INFRAELECCONSPRODRATIO(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAELECCONSPRODRATIO").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAELECCONSPRODRATIO(Dim1) = CSng(IFsResults("INFRAELECCONSPRODRATIO")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim INFRAINDICT(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAINDICT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAINDICT(Dim1) = CSng(IFsResults("INFRAINDICT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("infraelecaccrurallife").RowCount > (Year - FirstYear) * (1) Then
                infraelecaccrurallife = CSng(IFsResults("infraelecaccrurallife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraelecaccrurallife = 0
            End If

            If IFsResults("infraelecaccurbanlife").RowCount > (Year - FirstYear) * (1) Then
                infraelecaccurbanlife = CSng(IFsResults("infraelecaccurbanlife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraelecaccurbanlife = 0
            End If

            ReDim INFRAELECTRANLOSS(sNR)
            If IFsResults("INFRAELECTRANLOSS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INFRAELECTRANLOSS(Dim1) = CSng(IFsResults("INFRAELECTRANLOSS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim infraelectranlossm(sNR)
            If IFsResults("infraelectranlossm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraelectranlossm(Dim1) = CSng(IFsResults("infraelectranlossm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim infraelecacctrgtyr(sNR)
            If IFsResults("infraelecacctrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraelecacctrgtyr(Dim1) = CSng(IFsResults("infraelecacctrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INFRAELEC(sNR)
            If IFsResults("INFRAELEC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INFRAELEC(Dim1) = CSng(IFsResults("INFRAELEC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ENELECSHRENDEM(sNR)
            if Year = FirstYear then
                If IFsResults("ENELECSHRENDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ENELECSHRENDEM(Dim1) = CSng(IFsResults("ENELECSHRENDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim enelecshrendemm(sNR)
            If IFsResults("enelecshrendemm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    enelecshrendemm(Dim1) = CSng(IFsResults("enelecshrendemm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim malelimprecisesw(sNR)
            If IFsResults("malelimprecisesw").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    malelimprecisesw(Dim1) = CSng(IFsResults("malelimprecisesw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim malnelimstartyr(sNR)
            If IFsResults("malnelimstartyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    malnelimstartyr(Dim1) = CSng(IFsResults("malnelimstartyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim malnelimtargetyr(sNR)
            If IFsResults("malnelimtargetyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    malnelimtargetyr(Dim1) = CSng(IFsResults("malnelimtargetyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim EMBASSYBILAT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("EMBASSYBILAT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            EMBASSYBILAT(Dim1, Dim2) = CSng(if(IFsResults("EMBASSYBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim MIGRANTSOUT(sNR)
            if Year = FirstYear then
                If IFsResults("MIGRANTSOUT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MIGRANTSOUT(Dim1) = CSng(IFsResults("MIGRANTSOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim LABEMPS(sNR, 6, 3)
            if Year = FirstYear then
                If IFsResults("LABEMPS").RowCount > (Year - FirstYear) * (1 * sNR * 6 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            For Dim3 = 1 to 3
                                LABEMPS(Dim1, Dim2, Dim3) = CSng(if(IFsResults("LABEMPS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ken(sNR, 9)
            If IFsResults("ken").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        ken(Dim1, Dim2) = CSng(if(IFsResults("ken")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim lke(9)
            If IFsResults("lke").RowCount > (Year - FirstYear) * (1 * 9) Then
                For Dim1 = 1 to 9
                    lke(Dim1) = CSng(IFsResults("lke")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9)))
                Next Dim1
            End If

            ReDim enpcoalmax(sNR)
            If IFsResults("enpcoalmax").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    enpcoalmax(Dim1) = CSng(IFsResults("enpcoalmax")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ENDEM(sNR)
            if Year = FirstYear then
                If IFsResults("ENDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ENDEM(Dim1) = CSng(IFsResults("ENDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim elasde(sNR)
            If IFsResults("elasde").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    elasde(Dim1) = CSng(IFsResults("elasde")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ENRGDP(sNR)
            if Year = FirstYear then
                If IFsResults("ENRGDP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ENRGDP(Dim1) = CSng(IFsResults("ENRGDP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim enrgdpgr(sNR)
            If IFsResults("enrgdpgr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    enrgdpgr(Dim1) = CSng(IFsResults("enrgdpgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("elendemict").RowCount > (Year - FirstYear) * (1) Then
                elendemict = CSng(IFsResults("elendemict")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elendemict = 0
            End If

            ReDim ENDEMSH(sNR)
            if Year = FirstYear then
                If IFsResults("ENDEMSH").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ENDEMSH(Dim1) = CSng(IFsResults("ENDEMSH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim endemm(sNR)
            If IFsResults("endemm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    endemm(Dim1) = CSng(IFsResults("endemm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("enconv").RowCount > (Year - FirstYear) * (1) Then
                enconv = CSng(IFsResults("enconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                enconv = 0
            End If

            If IFsResults("elasdi").RowCount > (Year - FirstYear) * (1) Then
                elasdi = CSng(IFsResults("elasdi")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elasdi = 0
            End If

            ReDim rdinr(9)
            If IFsResults("rdinr").RowCount > (Year - FirstYear) * (1 * 9) Then
                For Dim1 = 1 to 9
                    rdinr(Dim1) = CSng(IFsResults("rdinr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9)))
                Next Dim1
            End If

            ReDim ENX(sNR)
            If IFsResults("ENX").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ENX(Dim1) = CSng(IFsResults("ENX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim enxm(sNR)
            If IFsResults("enxm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    enxm(Dim1) = CSng(IFsResults("enxm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim enml(sNR)
            If IFsResults("enml").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    enml(Dim1) = CSng(IFsResults("enml")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ENM(sNR)
            If IFsResults("ENM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ENM(Dim1) = CSng(IFsResults("ENM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim enmm(sNR)
            If IFsResults("enmm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    enmm(Dim1) = CSng(IFsResults("enmm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim inputenerm(sNR, 6)
            If IFsResults("inputenerm").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        inputenerm(Dim1, Dim2) = CSng(if(IFsResults("inputenerm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim ENINVT(sNR, 9)
            if Year = FirstYear then
                If IFsResults("ENINVT").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 9
                            ENINVT(Dim1, Dim2) = CSng(if(IFsResults("ENINVT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim eninvtm(sNR, 9)
            If IFsResults("eninvtm").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        eninvtm(Dim1, Dim2) = CSng(if(IFsResults("eninvtm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("elenpr2").RowCount > (Year - FirstYear) * (1) Then
                elenpr2 = CSng(IFsResults("elenpr2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elenpr2 = 0
            End If

            If IFsResults("eleniprof2").RowCount > (Year - FirstYear) * (1) Then
                eleniprof2 = CSng(IFsResults("eleniprof2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                eleniprof2 = 0
            End If

            If IFsResults("elenpr").RowCount > (Year - FirstYear) * (1) Then
                elenpr = CSng(IFsResults("elenpr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elenpr = 0
            End If

            If IFsResults("eleniprof").RowCount > (Year - FirstYear) * (1) Then
                eleniprof = CSng(IFsResults("eleniprof")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                eleniprof = 0
            End If

            ReDim eninvadd(sNR)
            If IFsResults("eninvadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    eninvadd(Dim1) = CSng(IFsResults("eninvadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim eninvm(sNR)
            If IFsResults("eninvm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    eninvm(Dim1) = CSng(IFsResults("eninvm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim enpgasmax(sNR)
            If IFsResults("enpgasmax").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    enpgasmax(Dim1) = CSng(IFsResults("enpgasmax")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ennoprodsw(sNR)
            If IFsResults("ennoprodsw").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ennoprodsw(Dim1) = CSng(IFsResults("ennoprodsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim enpoilmax(sNR)
            If IFsResults("enpoilmax").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    enpoilmax(Dim1) = CSng(IFsResults("enpoilmax")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("eprafs").RowCount > (Year - FirstYear) * (1) Then
                eprafs = CSng(IFsResults("eprafs")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                eprafs = 0
            End If

            If IFsResults("epra").RowCount > (Year - FirstYear) * (1) Then
                epra = CSng(IFsResults("epra")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                epra = 0
            End If

            ReDim enprix(sNR)
            If IFsResults("enprix").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    enprix(Dim1) = CSng(IFsResults("enprix")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ENPRI(sNR)
            If IFsResults("ENPRI").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ENPRI(Dim1) = CSng(IFsResults("ENPRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ENP(sNR, 9)
            If IFsResults("ENP").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        ENP(Dim1, Dim2) = CSng(if(IFsResults("ENP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim ENPRR(sNR, 9)
            If IFsResults("ENPRR").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        ENPRR(Dim1, Dim2) = CSng(if(IFsResults("ENPRR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim eprodr(sNR, 9)
            If IFsResults("eprodr").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        eprodr(Dim1, Dim2) = CSng(if(IFsResults("eprodr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("elenpst2").RowCount > (Year - FirstYear) * (1) Then
                elenpst2 = CSng(IFsResults("elenpst2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elenpst2 = 0
            End If

            If IFsResults("elenpst").RowCount > (Year - FirstYear) * (1) Then
                elenpst = CSng(IFsResults("elenpst")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elenpst = 0
            End If

            ReDim enpadd(sNR, 9)
            If IFsResults("enpadd").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        enpadd(Dim1, Dim2) = CSng(if(IFsResults("enpadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim enpm(sNR, 9)
            If IFsResults("enpm").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        enpm(Dim1, Dim2) = CSng(if(IFsResults("enpm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EPROFITR(sNR)
            if Year = FirstYear then
                If IFsResults("EPROFITR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        EPROFITR(Dim1) = CSng(IFsResults("EPROFITR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim RESER(sNR, 9)
            If IFsResults("RESER").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        RESER(Dim1, Dim2) = CSng(if(IFsResults("RESER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim ENPCAPUT(sNR)
            If IFsResults("ENPCAPUT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ENPCAPUT(Dim1) = CSng(IFsResults("ENPCAPUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("squeez").RowCount > (Year - FirstYear) * (1) Then
                squeez = CSng(IFsResults("squeez")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                squeez = 0
            End If

            ReDim elass(sNR, 9)
            If IFsResults("elass").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        elass(Dim1, Dim2) = CSng(if(IFsResults("elass")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim XBILATENE(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("XBILATENE").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            XBILATENE(Dim1, Dim2) = CSng(if(IFsResults("XBILATENE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim qevremul(sNR)
            If IFsResults("qevremul").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    qevremul(Dim1) = CSng(IFsResults("qevremul")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("enon").RowCount > (Year - FirstYear) * (1) Then
                enon = CSng(IFsResults("enon")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                enon = 0
            End If

            ReDim engel(6)
            If IFsResults("engel").RowCount > (Year - FirstYear) * (1 * 6) Then
                For Dim1 = 1 to 6
                    engel(Dim1) = CSng(IFsResults("engel")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)))
                Next Dim1
            End If

            ReDim EDSECLOWRENRGGRD(sNR, 8, 3)
            if Year = FirstYear then
                If IFsResults("EDSECLOWRENRGGRD").RowCount > (Year - FirstYear) * (1 * sNR * 8 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 8
                            For Dim3 = 1 to 3
                                EDSECLOWRENRGGRD(Dim1, Dim2, Dim3) = CSng(if(IFsResults("EDSECLOWRENRGGRD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 8, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDPRIENRGGRD(sNR, 8, 3)
            if Year = FirstYear then
                If IFsResults("EDPRIENRGGRD").RowCount > (Year - FirstYear) * (1 * sNR * 8 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 8
                            For Dim3 = 1 to 3
                                EDPRIENRGGRD(Dim1, Dim2, Dim3) = CSng(if(IFsResults("EDPRIENRGGRD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 8, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDPRIENRG(sNR, 3)
            If IFsResults("EDPRIENRG").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDPRIENRG(Dim1, Dim2) = CSng(if(IFsResults("EDPRIENRG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDSECLOWRENRG(sNR, 3)
            If IFsResults("EDSECLOWRENRG").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECLOWRENRG(Dim1, Dim2) = CSng(if(IFsResults("EDSECLOWRENRG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDSECENRN(sNR, 3)
            If IFsResults("EDSECENRN").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECENRN(Dim1, Dim2) = CSng(if(IFsResults("EDSECENRN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDBASENRG(sNR, 3)
            If IFsResults("EDBASENRG").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDBASENRG(Dim1, Dim2) = CSng(if(IFsResults("EDBASENRG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDTERENRG(sNR, 3)
            If IFsResults("EDTERENRG").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDTERENRG(Dim1, Dim2) = CSng(if(IFsResults("EDTERENRG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDPRIENRN(sNR, 3)
            If IFsResults("EDPRIENRN").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDPRIENRN(Dim1, Dim2) = CSng(if(IFsResults("EDPRIENRN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDSECLOWRTOT(sNR, 3)
            If IFsResults("EDSECLOWRTOT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECLOWRTOT(Dim1, Dim2) = CSng(if(IFsResults("EDSECLOWRTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDSECENRG(sNR, 3)
            If IFsResults("EDSECENRG").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECENRG(Dim1, Dim2) = CSng(if(IFsResults("EDSECENRG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDSECUPPRTOT(sNR, 3)
            If IFsResults("EDSECUPPRTOT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECUPPRTOT(Dim1, Dim2) = CSng(if(IFsResults("EDSECUPPRTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edpristart(sNR)
            If IFsResults("edpristart").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edpristart(Dim1) = CSng(IFsResults("edpristart")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim TES(sNR)
            if Year = FirstYear then
                If IFsResults("TES").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        TES(Dim1) = CSng(IFsResults("TES")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ethnic1(sNR)
            If IFsResults("ethnic1").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ethnic1(Dim1) = CSng(IFsResults("ethnic1")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ethnic2(sNR)
            If IFsResults("ethnic2").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ethnic2(Dim1) = CSng(IFsResults("ethnic2")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim eumembsw(sNR)
            If IFsResults("eumembsw").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    eumembsw(Dim1) = CSng(IFsResults("eumembsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("elerca1").RowCount > (Year - FirstYear) * (1) Then
                elerca1 = CSng(IFsResults("elerca1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elerca1 = 0
            End If

            If IFsResults("elerca2").RowCount > (Year - FirstYear) * (1) Then
                elerca2 = CSng(IFsResults("elerca2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elerca2 = 0
            End If

            ReDim EXRATE(sNR)
            if Year = FirstYear then
                If IFsResults("EXRATE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        EXRATE(Dim1) = CSng(IFsResults("EXRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("ifsexomodeltype").RowCount > (Year - FirstYear) * (1) Then
                ifsexomodeltype = CSng(IFsResults("ifsexomodeltype")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ifsexomodeltype = 0
            End If

            ReDim DEMOCEXP(sNR)
            if Year = FirstYear then
                If IFsResults("DEMOCEXP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        DEMOCEXP(Dim1) = CSng(IFsResults("DEMOCEXP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim edpricostadjrural(sNR)
            If IFsResults("edpricostadjrural").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edpricostadjrural(Dim1) = CSng(IFsResults("edpricostadjrural")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("edexpslconv").RowCount > (Year - FirstYear) * (1) Then
                edexpslconv = CSng(IFsResults("edexpslconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edexpslconv = 0
            End If

            ReDim edexperseclowrm(sNR)
            If IFsResults("edexperseclowrm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edexperseclowrm(Dim1) = CSng(IFsResults("edexperseclowrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("edexppconv").RowCount > (Year - FirstYear) * (1) Then
                edexppconv = CSng(IFsResults("edexppconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edexppconv = 0
            End If

            If IFsResults("edqualprispndimpgov").RowCount > (Year - FirstYear) * (1) Then
                edqualprispndimpgov = CSng(IFsResults("edqualprispndimpgov")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edqualprispndimpgov = 0
            End If

            If IFsResults("edqualprispndimp").RowCount > (Year - FirstYear) * (1) Then
                edqualprispndimp = CSng(IFsResults("edqualprispndimp")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edqualprispndimp = 0
            End If

            ReDim edprigoalexperon(sNR)
            If IFsResults("edprigoalexperon").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edprigoalexperon(Dim1) = CSng(IFsResults("edprigoalexperon")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edexperprim(sNR)
            If IFsResults("edexperprim").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edexperprim(Dim1) = CSng(IFsResults("edexperprim")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edprigoalexper(sNR)
            If IFsResults("edprigoalexper").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edprigoalexper(Dim1) = CSng(IFsResults("edprigoalexper")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("edqualsecspndimpgov").RowCount > (Year - FirstYear) * (1) Then
                edqualsecspndimpgov = CSng(IFsResults("edqualsecspndimpgov")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edqualsecspndimpgov = 0
            End If

            If IFsResults("edqualsecspndimp").RowCount > (Year - FirstYear) * (1) Then
                edqualsecspndimp = CSng(IFsResults("edqualsecspndimp")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edqualsecspndimp = 0
            End If

            If IFsResults("edexptconv").RowCount > (Year - FirstYear) * (1) Then
                edexptconv = CSng(IFsResults("edexptconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edexptconv = 0
            End If

            ReDim edexperterm(sNR)
            If IFsResults("edexperterm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edexperterm(Dim1) = CSng(IFsResults("edexperterm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("edexpsuconv").RowCount > (Year - FirstYear) * (1) Then
                edexpsuconv = CSng(IFsResults("edexpsuconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edexpsuconv = 0
            End If

            ReDim edexpersecupprm(sNR)
            If IFsResults("edexpersecupprm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edexpersecupprm(Dim1) = CSng(IFsResults("edexpersecupprm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim hlexpcostgaptohealthel(15)
            If IFsResults("hlexpcostgaptohealthel").RowCount > (Year - FirstYear) * (1 * 15) Then
                For Dim1 = 1 to 15
                    hlexpcostgaptohealthel(Dim1) = CSng(IFsResults("hlexpcostgaptohealthel")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 15)))
                Next Dim1
            End If

            ReDim WATRESEXPLOITRENEW(sNR, 2)
            If IFsResults("WATRESEXPLOITRENEW").RowCount > (Year - FirstYear) * (1 * sNR * 2) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 2
                        WATRESEXPLOITRENEW(Dim1, Dim2) = CSng(if(IFsResults("WATRESEXPLOITRENEW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim watresexploitrenewm(sNR, 2)
            If IFsResults("watresexploitrenewm").RowCount > (Year - FirstYear) * (1 * sNR * 2) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 2
                        watresexploitrenewm(Dim1, Dim2) = CSng(if(IFsResults("watresexploitrenewm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("xcadjsw").RowCount > (Year - FirstYear) * (1) Then
                xcadjsw = CSng(IFsResults("xcadjsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                xcadjsw = 0
            End If

            ReDim xshift(sNR, 6)
            If IFsResults("xshift").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        xshift(Dim1, Dim2) = CSng(if(IFsResults("xshift")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim X(sNR)
            if Year = FirstYear then
                If IFsResults("X").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        X(Dim1) = CSng(IFsResults("X")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim XS(sNR, 6)
            If IFsResults("XS").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        XS(Dim1, Dim2) = CSng(if(IFsResults("XS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim xsm(sNR, 6)
            If IFsResults("xsm").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        xsm(Dim1, Dim2) = CSng(if(IFsResults("xsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("elxpr2").RowCount > (Year - FirstYear) * (1) Then
                elxpr2 = CSng(IFsResults("elxpr2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elxpr2 = 0
            End If

            If IFsResults("elxpr1").RowCount > (Year - FirstYear) * (1) Then
                elxpr1 = CSng(IFsResults("elxpr1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elxpr1 = 0
            End If

            ReDim elasxincshock(sNR, 6)
            If IFsResults("elasxincshock").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        elasxincshock(Dim1, Dim2) = CSng(if(IFsResults("elasxincshock")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim elasxinc(sNR, 6)
            If IFsResults("elasxinc").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        elasxinc(Dim1, Dim2) = CSng(if(IFsResults("elasxinc")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim XRPA(sNR)
            if Year = FirstYear then
                If IFsResults("XRPA").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        XRPA(Dim1) = CSng(IFsResults("XRPA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim XDEBTRPA(sNR)
            If IFsResults("XDEBTRPA").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XDEBTRPA(Dim1) = CSng(IFsResults("XDEBTRPA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XFIRMDEBT(sNR)
            If IFsResults("XFIRMDEBT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XFIRMDEBT(Dim1) = CSng(IFsResults("XFIRMDEBT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XGOVTDEBT(sNR)
            If IFsResults("XGOVTDEBT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XGOVTDEBT(Dim1) = CSng(IFsResults("XGOVTDEBT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XGOVTDEBTB(sNR)
            If IFsResults("XGOVTDEBTB").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XGOVTDEBTB(Dim1) = CSng(IFsResults("XGOVTDEBTB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XGOVTDEBTBF(sNR)
            if Year = FirstYear then
                If IFsResults("XGOVTDEBTBF").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        XGOVTDEBTBF(Dim1) = CSng(IFsResults("XGOVTDEBTBF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim EXTRASTDRATIO(sNR, 4)
            if Year = FirstYear then
                If IFsResults("EXTRASTDRATIO").RowCount > (Year - FirstYear) * (1 * sNR * 4) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 4
                            EXTRASTDRATIO(Dim1, Dim2) = CSng(if(IFsResults("EXTRASTDRATIO")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim xfdifinm(sNR)
            If IFsResults("xfdifinm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    xfdifinm(Dim1) = CSng(IFsResults("xfdifinm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim xfdistockr(sNR)
            If IFsResults("xfdistockr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    xfdistockr(Dim1) = CSng(IFsResults("xfdistockr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("elfdilifexp").RowCount > (Year - FirstYear) * (1) Then
                elfdilifexp = CSng(IFsResults("elfdilifexp")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elfdilifexp = 0
            End If

            ReDim XFDIFIN(sNR)
            If IFsResults("XFDIFIN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XFDIFIN(Dim1) = CSng(IFsResults("XFDIFIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XFDIFOUT(sNR)
            If IFsResults("XFDIFOUT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XFDIFOUT(Dim1) = CSng(IFsResults("XFDIFOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XFDISTOUT(sNR)
            If IFsResults("XFDISTOUT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XFDISTOUT(Dim1) = CSng(IFsResults("XFDISTOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim labfemshrm(sNR)
            If IFsResults("labfemshrm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    labfemshrm(Dim1) = CSng(IFsResults("labfemshrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim FEMSHRLAB(sNR)
            If IFsResults("FEMSHRLAB").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    FEMSHRLAB(Dim1) = CSng(IFsResults("FEMSHRLAB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim hltfragepeak(sNR)
            If IFsResults("hltfragepeak").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hltfragepeak(Dim1) = CSng(IFsResults("hltfragepeak")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim hltfrageinit(sNR)
            If IFsResults("hltfrageinit").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hltfrageinit(Dim1) = CSng(IFsResults("hltfrageinit")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim hltfrpeaklevel(sNR)
            If IFsResults("hltfrpeaklevel").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hltfrpeaklevel(Dim1) = CSng(IFsResults("hltfrpeaklevel")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("hltfrconv").RowCount > (Year - FirstYear) * (1) Then
                hltfrconv = CSng(IFsResults("hltfrconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hltfrconv = 0
            End If

            ReDim FIRMINC(sNR)
            if Year = FirstYear then
                If IFsResults("FIRMINC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        FIRMINC(Dim1) = CSng(IFsResults("FIRMINC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim FIRMINCDIS(sNR)
            if Year = FirstYear then
                If IFsResults("FIRMINCDIS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        FIRMINCDIS(Dim1) = CSng(IFsResults("FIRMINCDIS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim FIRMEARN(sNR)
            if Year = FirstYear then
                If IFsResults("FIRMEARN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        FIRMEARN(Dim1) = CSng(IFsResults("FIRMEARN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim FIRMSAV(sNR)
            if Year = FirstYear then
                If IFsResults("FIRMSAV").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        FIRMSAV(Dim1) = CSng(IFsResults("FIRMSAV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim FIRMGOVSS(sNR)
            if Year = FirstYear then
                If IFsResults("FIRMGOVSS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        FIRMGOVSS(Dim1) = CSng(IFsResults("FIRMGOVSS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim FIRMTAX(sNR)
            if Year = FirstYear then
                If IFsResults("FIRMTAX").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        FIRMTAX(Dim1) = CSng(IFsResults("FIRMTAX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim FIRMTAXR(sNR)
            if Year = FirstYear then
                If IFsResults("FIRMTAXR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        FIRMTAXR(Dim1) = CSng(IFsResults("FIRMTAXR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim firmtaxrm(sNR)
            If IFsResults("firmtaxrm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    firmtaxrm(Dim1) = CSng(IFsResults("firmtaxrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim AGFISHCATCH(sNR)
            if Year = FirstYear then
                If IFsResults("AGFISHCATCH").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        AGFISHCATCH(Dim1) = CSng(IFsResults("AGFISHCATCH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim fishcatchm(sNR)
            If IFsResults("fishcatchm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    fishcatchm(Dim1) = CSng(IFsResults("fishcatchm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim elasfd(sNR)
            If IFsResults("elasfd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    elasfd(Dim1) = CSng(IFsResults("elasfd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim FISH(sNR)
            if Year = FirstYear then
                If IFsResults("FISH").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        FISH(Dim1) = CSng(IFsResults("FISH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ictbroadtrgtval(sNR)
            If IFsResults("ictbroadtrgtval").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictbroadtrgtval(Dim1) = CSng(IFsResults("ictbroadtrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ICTBROADDEM(sNR)
            if Year = FirstYear then
                If IFsResults("ICTBROADDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTBROADDEM(Dim1) = CSng(IFsResults("ICTBROADDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("ictbroadurimpmax").RowCount > (Year - FirstYear) * (1) Then
                ictbroadurimpmax = CSng(IFsResults("ictbroadurimpmax")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictbroadurimpmax = 0
            End If

            If IFsResults("ictbroadurimpmin").RowCount > (Year - FirstYear) * (1) Then
                ictbroadurimpmin = CSng(IFsResults("ictbroadurimpmin")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictbroadurimpmin = 0
            End If

            ReDim ictbroadsetar(sNR)
            If IFsResults("ictbroadsetar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictbroadsetar(Dim1) = CSng(IFsResults("ictbroadsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ICTBROAD(sNR)
            if Year = FirstYear then
                If IFsResults("ICTBROAD").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTBROAD(Dim1) = CSng(IFsResults("ICTBROAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ictbroadseyrtar(sNR)
            If IFsResults("ictbroadseyrtar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictbroadseyrtar(Dim1) = CSng(IFsResults("ictbroadseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim aglossconsm(sNR, 3)
            If IFsResults("aglossconsm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        aglossconsm(Dim1, Dim2) = CSng(if(IFsResults("aglossconsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim AGLOSSCONS(sNR, 3)
            if Year = FirstYear then
                If IFsResults("AGLOSSCONS").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            AGLOSSCONS(Dim1, Dim2) = CSng(if(IFsResults("AGLOSSCONS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim FDEM(sNR, 3)
            If IFsResults("FDEM").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        FDEM(Dim1, Dim2) = CSng(if(IFsResults("FDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim FOODINSECURITY(sNR, 3)
            if Year = FirstYear then
                If IFsResults("FOODINSECURITY").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            FOODINSECURITY(Dim1, Dim2) = CSng(if(IFsResults("FOODINSECURITY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim foodinsecuritym(sNR, 3)
            If IFsResults("foodinsecuritym").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        foodinsecuritym(Dim1, Dim2) = CSng(if(IFsResults("foodinsecuritym")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim FMDEM(sNR, 3)
            If IFsResults("FMDEM").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        FMDEM(Dim1, Dim2) = CSng(if(IFsResults("FMDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim FPRI(sNR, 3)
            if Year = FirstYear then
                If IFsResults("FPRI").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            FPRI(Dim1, Dim2) = CSng(if(IFsResults("FPRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("fprimt2").RowCount > (Year - FirstYear) * (1) Then
                fprimt2 = CSng(IFsResults("fprimt2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                fprimt2 = 0
            End If

            If IFsResults("fprimt1").RowCount > (Year - FirstYear) * (1) Then
                fprimt1 = CSng(IFsResults("fprimt1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                fprimt1 = 0
            End If

            If IFsResults("fpricr2").RowCount > (Year - FirstYear) * (1) Then
                fpricr2 = CSng(IFsResults("fpricr2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                fpricr2 = 0
            End If

            If IFsResults("fpricr1").RowCount > (Year - FirstYear) * (1) Then
                fpricr1 = CSng(IFsResults("fpricr1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                fpricr1 = 0
            End If

            ReDim FPROFITR(sNR, 3)
            if Year = FirstYear then
                If IFsResults("FPROFITR").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            FPROFITR(Dim1, Dim2) = CSng(if(IFsResults("FPROFITR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim FSTOCK(sNR, 3)
            if Year = FirstYear then
                If IFsResults("FSTOCK").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            FSTOCK(Dim1, Dim2) = CSng(if(IFsResults("FSTOCK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim AIDREC(sNR)
            If IFsResults("AIDREC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    AIDREC(Dim1) = CSng(IFsResults("AIDREC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim AID(sNR)
            if Year = FirstYear then
                If IFsResults("AID").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        AID(Dim1) = CSng(IFsResults("AID")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim AIDDONATE(sNR)
            if Year = FirstYear then
                If IFsResults("AIDDONATE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        AIDDONATE(Dim1) = CSng(IFsResults("AIDDONATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim AIDRECEIPT(sNR)
            if Year = FirstYear then
                If IFsResults("AIDRECEIPT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        AIDRECEIPT(Dim1) = CSng(IFsResults("AIDRECEIPT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POPFOREIGNIN(sNR)
            If IFsResults("POPFOREIGNIN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    POPFOREIGNIN(Dim1) = CSng(IFsResults("POPFOREIGNIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XFDISTOCK(sNR)
            If IFsResults("XFDISTOCK").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XFDISTOCK(Dim1) = CSng(IFsResults("XFDISTOCK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim xfdifoutm(sNR)
            If IFsResults("xfdifoutm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    xfdifoutm(Dim1) = CSng(IFsResults("xfdifoutm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim FORSAV(sNR)
            if Year = FirstYear then
                If IFsResults("FORSAV").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        FORSAV(Dim1) = CSng(IFsResults("FORSAV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim FORSAVRPA(sNR)
            if Year = FirstYear then
                If IFsResults("FORSAVRPA").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        FORSAVRPA(Dim1) = CSng(IFsResults("FORSAVRPA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POPFOREIGNOUT(sNR)
            If IFsResults("POPFOREIGNOUT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    POPFOREIGNOUT(Dim1) = CSng(IFsResults("POPFOREIGNOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim forestm(sNR)
            If IFsResults("forestm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    forestm(Dim1) = CSng(IFsResults("forestm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim FREEDOM(sNR)
            If IFsResults("FREEDOM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    FREEDOM(Dim1) = CSng(IFsResults("FREEDOM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim freedomm(sNR)
            If IFsResults("freedomm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    freedomm(Dim1) = CSng(IFsResults("freedomm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("govconswing").RowCount > (Year - FirstYear) * (1) Then
                govconswing = CSng(IFsResults("govconswing")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                govconswing = 0
            End If

            ReDim GDPP(sNR)
            if Year = FirstYear then
                If IFsResults("GDPP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GDPP(Dim1) = CSng(IFsResults("GDPP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("gdpadjsw").RowCount > (Year - FirstYear) * (1) Then
                gdpadjsw = CSng(IFsResults("gdpadjsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                gdpadjsw = 0
            End If

            ReDim GDPADJ(sNR)
            if Year = FirstYear then
                If IFsResults("GDPADJ").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GDPADJ(Dim1) = CSng(IFsResults("GDPADJ")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("histgdpforce").RowCount > (Year - FirstYear) * (1) Then
                histgdpforce = CSng(IFsResults("histgdpforce")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                histgdpforce = 0
            End If

            ReDim GDPR(sNR)
            if Year = FirstYear then
                If IFsResults("GDPR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GDPR(Dim1) = CSng(IFsResults("GDPR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim gdprext(sNR)
            If IFsResults("gdprext").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    gdprext(Dim1) = CSng(IFsResults("gdprext")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim igdpr(sNR)
            If IFsResults("igdpr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    igdpr(Dim1) = CSng(IFsResults("igdpr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GDPPCP(sNR)
            if Year = FirstYear then
                If IFsResults("GDPPCP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GDPPCP(Dim1) = CSng(IFsResults("GDPPCP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim GDPPCP2005(sNR)
            if Year = FirstYear then
                If IFsResults("GDPPCP2005").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GDPPCP2005(Dim1) = CSng(IFsResults("GDPPCP2005")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim GDPPCPMF(sNR, 3)
            if Year = FirstYear then
                If IFsResults("GDPPCPMF").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            GDPPCPMF(Dim1, Dim2) = CSng(if(IFsResults("GDPPCPMF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim GDPPC(sNR)
            if Year = FirstYear then
                If IFsResults("GDPPC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GDPPC(Dim1) = CSng(IFsResults("GDPPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim hdigdppcmaxun(3)
            If IFsResults("hdigdppcmaxun").RowCount > (Year - FirstYear) * (1 * 3) Then
                For Dim1 = 1 to 3
                    hdigdppcmaxun(Dim1) = CSng(IFsResults("hdigdppcmaxun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)))
                Next Dim1
            End If

            If IFsResults("hdigdppcmax").RowCount > (Year - FirstYear) * (1) Then
                hdigdppcmax = CSng(IFsResults("hdigdppcmax")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hdigdppcmax = 0
            End If

            ReDim hdigdppcminun(3)
            If IFsResults("hdigdppcminun").RowCount > (Year - FirstYear) * (1 * 3) Then
                For Dim1 = 1 to 3
                    hdigdppcminun(Dim1) = CSng(IFsResults("hdigdppcminun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)))
                Next Dim1
            End If

            ReDim GDPPOT(sNR)
            if Year = FirstYear then
                If IFsResults("GDPPOT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GDPPOT(Dim1) = CSng(IFsResults("GDPPOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim armsimmaxpctmilexp(sNR)
            If IFsResults("armsimmaxpctmilexp").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    armsimmaxpctmilexp(Dim1) = CSng(IFsResults("armsimmaxpctmilexp")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GDI(sNR)
            if Year = FirstYear then
                If IFsResults("GDI").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GDI(Dim1) = CSng(IFsResults("GDI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim gemsetar(sNR)
            If IFsResults("gemsetar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    gemsetar(Dim1) = CSng(IFsResults("gemsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim gemm(sNR)
            If IFsResults("gemm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    gemm(Dim1) = CSng(IFsResults("gemm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("gemconv").RowCount > (Year - FirstYear) * (1) Then
                gemconv = CSng(IFsResults("gemconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                gemconv = 0
            End If

            ReDim gemseyrtar(sNR)
            If IFsResults("gemseyrtar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    gemseyrtar(Dim1) = CSng(IFsResults("gemseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GEM(sNR)
            If IFsResults("GEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    GEM(Dim1) = CSng(IFsResults("GEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GII(sNR)
            If IFsResults("GII").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    GII(Dim1) = CSng(IFsResults("GII")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GINIDOM(sNR)
            if Year = FirstYear then
                If IFsResults("GINIDOM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GINIDOM(Dim1) = CSng(IFsResults("GINIDOM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("glocom").RowCount > (Year - FirstYear) * (1) Then
                glocom = CSng(IFsResults("glocom")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                glocom = 0
            End If

            If IFsResults("xfdiwgradd").RowCount > (Year - FirstYear) * (1) Then
                xfdiwgradd = CSng(IFsResults("xfdiwgradd")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                xfdiwgradd = 0
            End If

            ReDim globalfundsrecipients(sNR)
            If IFsResults("globalfundsrecipients").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    globalfundsrecipients(Dim1) = CSng(IFsResults("globalfundsrecipients")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            if Year = FirstYear then
                If IFsResults("GINIFULL").RowCount > (Year - FirstYear) * (1) Then
                    GINIFULL = CSng(IFsResults("GINIFULL")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    GINIFULL = 0
                End If
            Else
                GINIFULL = 0
            End if

            If IFsResults("mfpbasinc").RowCount > (Year - FirstYear) * (1) Then
                mfpbasinc = CSng(IFsResults("mfpbasinc")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpbasinc = 0
            End If

            If IFsResults("mfpbasgr").RowCount > (Year - FirstYear) * (1) Then
                mfpbasgr = CSng(IFsResults("mfpbasgr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mfpbasgr = 0
            End If

            If IFsResults("lintr").RowCount > (Year - FirstYear) * (1) Then
                lintr = CSng(IFsResults("lintr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                lintr = 0
            End If

            if Year = FirstYear then
                If IFsResults("OILGPR").RowCount > (Year - FirstYear) * (1) Then
                    OILGPR = CSng(IFsResults("OILGPR")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    OILGPR = 0
                End If
            Else
                OILGPR = 0
            End if

            ReDim POWERGPI(sNR)
            if Year = FirstYear then
                If IFsResults("POWERGPI").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POWERGPI(Dim1) = CSng(IFsResults("POWERGPI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim GLOBALIZ(sNR)
            if Year = FirstYear then
                If IFsResults("GLOBALIZ").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GLOBALIZ(Dim1) = CSng(IFsResults("GLOBALIZ")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim govriskm(sNR)
            If IFsResults("govriskm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govriskm(Dim1) = CSng(IFsResults("govriskm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("govsecrskexp").RowCount > (Year - FirstYear) * (1) Then
                govsecrskexp = CSng(IFsResults("govsecrskexp")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                govsecrskexp = 0
            End If

            ReDim GOVINDTOTAL(sNR)
            if Year = FirstYear then
                If IFsResults("GOVINDTOTAL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVINDTOTAL(Dim1) = CSng(IFsResults("GOVINDTOTAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim GOVINDCAPAC(sNR)
            if Year = FirstYear then
                If IFsResults("GOVINDCAPAC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVINDCAPAC(Dim1) = CSng(IFsResults("GOVINDCAPAC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim govconm(sNR)
            If IFsResults("govconm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govconm(Dim1) = CSng(IFsResults("govconm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GOVCORRUPT(sNR)
            If IFsResults("GOVCORRUPT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    GOVCORRUPT(Dim1) = CSng(IFsResults("GOVCORRUPT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim govcorruptsetar(sNR)
            If IFsResults("govcorruptsetar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govcorruptsetar(Dim1) = CSng(IFsResults("govcorruptsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim govcorruptseyrtar(sNR)
            If IFsResults("govcorruptseyrtar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govcorruptseyrtar(Dim1) = CSng(IFsResults("govcorruptseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim govcorruptm(sNR)
            If IFsResults("govcorruptm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govcorruptm(Dim1) = CSng(IFsResults("govcorruptm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GOVDEBT(sNR)
            If IFsResults("GOVDEBT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    GOVDEBT(Dim1) = CSng(IFsResults("GOVDEBT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim govdebtchg(sNR)
            If IFsResults("govdebtchg").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govdebtchg(Dim1) = CSng(IFsResults("govdebtchg")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GDSED(sNR, 4)
            if Year = FirstYear then
                If IFsResults("GDSED").RowCount > (Year - FirstYear) * (1 * sNR * 4) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 4
                            GDSED(Dim1, Dim2) = CSng(if(IFsResults("GDSED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim goveffectsetar(sNR)
            If IFsResults("goveffectsetar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    goveffectsetar(Dim1) = CSng(IFsResults("goveffectsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim goveffectseyrtar(sNR)
            If IFsResults("goveffectseyrtar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    goveffectseyrtar(Dim1) = CSng(IFsResults("goveffectseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GOVEFFECT(sNR)
            If IFsResults("GOVEFFECT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    GOVEFFECT(Dim1) = CSng(IFsResults("GOVEFFECT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim goveffectm(sNR)
            If IFsResults("goveffectm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    goveffectm(Dim1) = CSng(IFsResults("goveffectm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("goveffconv").RowCount > (Year - FirstYear) * (1) Then
                goveffconv = CSng(IFsResults("goveffconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                goveffconv = 0
            End If

            ReDim GOVEMBED(sNR)
            if Year = FirstYear then
                If IFsResults("GOVEMBED").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVEMBED(Dim1) = CSng(IFsResults("GOVEMBED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim GDS(sNR, 7)
            if Year = FirstYear then
                If IFsResults("GDS").RowCount > (Year - FirstYear) * (1 * sNR * 7) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 7
                            GDS(Dim1, Dim2) = CSng(if(IFsResults("GDS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim GS(sNR, 6)
            If IFsResults("GS").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        GS(Dim1, Dim2) = CSng(if(IFsResults("GS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim GOVEXP(sNR)
            if Year = FirstYear then
                If IFsResults("GOVEXP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVEXP(Dim1) = CSng(IFsResults("GOVEXP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim gdsm(sNR, 7)
            If IFsResults("gdsm").RowCount > (Year - FirstYear) * (1 * sNR * 7) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 7
                        gdsm(Dim1, Dim2) = CSng(if(IFsResults("gdsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim gdscnstsw(sNR, 7)
            If IFsResults("gdscnstsw").RowCount > (Year - FirstYear) * (1 * sNR * 7) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 7
                        gdscnstsw(Dim1, Dim2) = CSng(if(IFsResults("gdscnstsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim gdsedm(sNR, 4)
            If IFsResults("gdsedm").RowCount > (Year - FirstYear) * (1 * sNR * 4) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 4
                        gdsedm(Dim1, Dim2) = CSng(if(IFsResults("gdsedm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("elgexpdebt2").RowCount > (Year - FirstYear) * (1) Then
                elgexpdebt2 = CSng(IFsResults("elgexpdebt2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elgexpdebt2 = 0
            End If

            If IFsResults("elgexpdebt1").RowCount > (Year - FirstYear) * (1) Then
                elgexpdebt1 = CSng(IFsResults("elgexpdebt1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elgexpdebt1 = 0
            End If

            ReDim govexpm(sNR)
            If IFsResults("govexpm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govexpm(Dim1) = CSng(IFsResults("govexpm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("gdsnormalize").RowCount > (Year - FirstYear) * (1) Then
                gdsnormalize = CSng(IFsResults("gdsnormalize")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                gdsnormalize = 0
            End If

            If IFsResults("govfinconv").RowCount > (Year - FirstYear) * (1) Then
                govfinconv = CSng(IFsResults("govfinconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                govfinconv = 0
            End If

            ReDim GOVBAL(sNR)
            if Year = FirstYear then
                If IFsResults("GOVBAL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVBAL(Dim1) = CSng(IFsResults("GOVBAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim GOVINDINCLUS(sNR)
            if Year = FirstYear then
                If IFsResults("GOVINDINCLUS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVINDINCLUS(Dim1) = CSng(IFsResults("GOVINDINCLUS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim govbusregindm(sNR)
            If IFsResults("govbusregindm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govbusregindm(Dim1) = CSng(IFsResults("govbusregindm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GOVREGQUAL(sNR)
            If IFsResults("GOVREGQUAL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    GOVREGQUAL(Dim1) = CSng(IFsResults("GOVREGQUAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim govregqualsetar(sNR)
            If IFsResults("govregqualsetar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govregqualsetar(Dim1) = CSng(IFsResults("govregqualsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim govregqualseyrtar(sNR)
            If IFsResults("govregqualseyrtar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govregqualseyrtar(Dim1) = CSng(IFsResults("govregqualseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim govregqualm(sNR)
            If IFsResults("govregqualm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govregqualm(Dim1) = CSng(IFsResults("govregqualm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim HTCGRI(sNR)
            if Year = FirstYear then
                If IFsResults("HTCGRI").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HTCGRI(Dim1) = CSng(IFsResults("HTCGRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim govrevtrgtval(sNR)
            If IFsResults("govrevtrgtval").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govrevtrgtval(Dim1) = CSng(IFsResults("govrevtrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("govrevtrgtyr").RowCount > (Year - FirstYear) * (1) Then
                govrevtrgtyr = CSng(IFsResults("govrevtrgtyr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                govrevtrgtyr = 0
            End If

            ReDim GOVREV(sNR)
            if Year = FirstYear then
                If IFsResults("GOVREV").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVREV(Dim1) = CSng(IFsResults("GOVREV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("elgrevdebt2").RowCount > (Year - FirstYear) * (1) Then
                elgrevdebt2 = CSng(IFsResults("elgrevdebt2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elgrevdebt2 = 0
            End If

            ReDim govrevm(sNR)
            If IFsResults("govrevm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govrevm(Dim1) = CSng(IFsResults("govrevm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("elgrevdebt1").RowCount > (Year - FirstYear) * (1) Then
                elgrevdebt1 = CSng(IFsResults("elgrevdebt1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elgrevdebt1 = 0
            End If

            ReDim GOVINDSECUR(sNR)
            if Year = FirstYear then
                If IFsResults("GOVINDSECUR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVINDSECUR(Dim1) = CSng(IFsResults("GOVINDSECUR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("govindsecurconflict").RowCount > (Year - FirstYear) * (1) Then
                govindsecurconflict = CSng(IFsResults("govindsecurconflict")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                govindsecurconflict = 0
            End If

            If IFsResults("govindsecurgovrisk").RowCount > (Year - FirstYear) * (1) Then
                govindsecurgovrisk = CSng(IFsResults("govindsecurgovrisk")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                govindsecurgovrisk = 0
            End If

            If IFsResults("govindsecurviolence").RowCount > (Year - FirstYear) * (1) Then
                govindsecurviolence = CSng(IFsResults("govindsecurviolence")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                govindsecurviolence = 0
            End If

            ReDim govindsecurm(sNR)
            If IFsResults("govindsecurm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govindsecurm(Dim1) = CSng(IFsResults("govindsecurm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim gdstrgtval(sNR, 7)
            If IFsResults("gdstrgtval").RowCount > (Year - FirstYear) * (1 * sNR * 7) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 7
                        gdstrgtval(Dim1, Dim2) = CSng(if(IFsResults("gdstrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim gdsbudgetconv(sNR, 7)
            If IFsResults("gdsbudgetconv").RowCount > (Year - FirstYear) * (1 * sNR * 7) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 7
                        gdsbudgetconv(Dim1, Dim2) = CSng(if(IFsResults("gdsbudgetconv")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim gdsbudgetprotec(sNR, 7)
            If IFsResults("gdsbudgetprotec").RowCount > (Year - FirstYear) * (1 * sNR * 7) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 7
                        gdsbudgetprotec(Dim1, Dim2) = CSng(if(IFsResults("gdsbudgetprotec")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim gdstrgtyr(sNR, 7)
            If IFsResults("gdstrgtyr").RowCount > (Year - FirstYear) * (1 * sNR * 7) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 7
                        gdstrgtyr(Dim1, Dim2) = CSng(if(IFsResults("gdstrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim govhhpentndpctuse(sNR)
            If IFsResults("govhhpentndpctuse").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govhhpentndpctuse(Dim1) = CSng(IFsResults("govhhpentndpctuse")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GOVHHPENTND(sNR)
            if Year = FirstYear then
                If IFsResults("GOVHHPENTND").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVHHPENTND(Dim1) = CSng(IFsResults("GOVHHPENTND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim GOVHHPENT(sNR)
            if Year = FirstYear then
                If IFsResults("GOVHHPENT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVHHPENT(Dim1) = CSng(IFsResults("GOVHHPENT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim govhhtrnwelm(sNR, 3)
            If IFsResults("govhhtrnwelm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        govhhtrnwelm(Dim1, Dim2) = CSng(if(IFsResults("govhhtrnwelm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim govhhtrnpenm(sNR, 3)
            If IFsResults("govhhtrnpenm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        govhhtrnpenm(Dim1, Dim2) = CSng(if(IFsResults("govhhtrnpenm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDSECLOWRGRADS(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDSECLOWRGRADS").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDSECLOWRGRADS(Dim1, Dim2) = CSng(if(IFsResults("EDSECLOWRGRADS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDPRIGRADS(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDPRIGRADS").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDPRIGRADS(Dim1, Dim2) = CSng(if(IFsResults("EDPRIGRADS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDSECUPPRGRADS(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDSECUPPRGRADS").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDSECUPPRGRADS(Dim1, Dim2) = CSng(if(IFsResults("EDSECUPPRGRADS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDTERGRADS(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDTERGRADS").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDTERGRADS(Dim1, Dim2) = CSng(if(IFsResults("EDTERGRADS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDSECLOWRGRATE(sNR, 3)
            If IFsResults("EDSECLOWRGRATE").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECLOWRGRATE(Dim1, Dim2) = CSng(if(IFsResults("EDSECLOWRGRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDSECUPPRGRATE(sNR, 3)
            If IFsResults("EDSECUPPRGRATE").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECUPPRGRATE(Dim1, Dim2) = CSng(if(IFsResults("EDSECUPPRGRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDTERGRATE(sNR, 3)
            If IFsResults("EDTERGRATE").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDTERGRATE(Dim1, Dim2) = CSng(if(IFsResults("EDTERGRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim GLDCAP(sNR)
            if Year = FirstYear then
                If IFsResults("GLDCAP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GLDCAP(Dim1) = CSng(IFsResults("GLDCAP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim GDP(sNR)
            if Year = FirstYear then
                If IFsResults("GDP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GDP(Dim1) = CSng(IFsResults("GDP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim GDPRPA(sNR)
            if Year = FirstYear then
                If IFsResults("GDPRPA").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GDPRPA(Dim1) = CSng(IFsResults("GDPRPA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim GNI(sNR)
            if Year = FirstYear then
                If IFsResults("GNI").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GNI(Dim1) = CSng(IFsResults("GNI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ZS(sNR, 6)
            If IFsResults("ZS").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        ZS(Dim1, Dim2) = CSng(if(IFsResults("ZS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim AITASKGR(6)
            if Year = FirstYear then
                If IFsResults("AITASKGR").RowCount > (Year - FirstYear) * (1 * 6) Then
                    For Dim1 = 1 to 6
                        AITASKGR(Dim1) = CSng(IFsResults("AITASKGR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)))
                    Next Dim1
                End If
            End if

            ReDim hlcostgrowthel(15)
            If IFsResults("hlcostgrowthel").RowCount > (Year - FirstYear) * (1 * 15) Then
                For Dim1 = 1 to 15
                    hlcostgrowthel(Dim1) = CSng(IFsResults("hlcostgrowthel")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 15)))
                Next Dim1
            End If

            ReDim mfpleadr(6)
            If IFsResults("mfpleadr").RowCount > (Year - FirstYear) * (1 * 6) Then
                For Dim1 = 1 to 6
                    mfpleadr(Dim1) = CSng(IFsResults("mfpleadr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)))
                Next Dim1
            End If

            ReDim HDIFLOAT(sNR)
            if Year = FirstYear then
                If IFsResults("HDIFLOAT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HDIFLOAT(Dim1) = CSng(IFsResults("HDIFLOAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HDI21STFIX(sNR)
            if Year = FirstYear then
                If IFsResults("HDI21STFIX").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HDI21STFIX(Dim1) = CSng(IFsResults("HDI21STFIX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HDI(sNR)
            if Year = FirstYear then
                If IFsResults("HDI").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HDI(Dim1) = CSng(IFsResults("HDI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HDIMF(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HDIMF").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HDIMF(Dim1, Dim2) = CSng(if(IFsResults("HDIMF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HDIPRE2010(sNR)
            if Year = FirstYear then
                If IFsResults("HDIPRE2010").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HDIPRE2010(Dim1) = CSng(IFsResults("HDIPRE2010")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("hltechbase").RowCount > (Year - FirstYear) * (1) Then
                hltechbase = CSng(IFsResults("hltechbase")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hltechbase = 0
            End If

            If IFsResults("hltechshift").RowCount > (Year - FirstYear) * (1) Then
                hltechshift = CSng(IFsResults("hltechshift")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hltechshift = 0
            End If

            If IFsResults("hlmodelsw").RowCount > (Year - FirstYear) * (1) Then
                hlmodelsw = CSng(IFsResults("hlmodelsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlmodelsw = 0
            End If

            ReDim POWERHH(sNR)
            if Year = FirstYear then
                If IFsResults("POWERHH").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POWERHH(Dim1) = CSng(IFsResults("POWERHH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("cpowldcf").RowCount > (Year - FirstYear) * (1) Then
                cpowldcf = CSng(IFsResults("cpowldcf")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                cpowldcf = 0
            End If

            ReDim POWERHHMI(sNR)
            if Year = FirstYear then
                If IFsResults("POWERHHMI").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POWERHHMI(Dim1) = CSng(IFsResults("POWERHHMI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HIVRATE(sNR)
            If IFsResults("HIVRATE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    HIVRATE(Dim1) = CSng(IFsResults("HIVRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("hlmorthivmalsw").RowCount > (Year - FirstYear) * (1) Then
                hlmorthivmalsw = CSng(IFsResults("hlmorthivmalsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlmorthivmalsw = 0
            End If

            ReDim hlhivincm(sNR)
            If IFsResults("hlhivincm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hlhivincm(Dim1) = CSng(IFsResults("hlhivincm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim hlhivmortm(sNR)
            If IFsResults("hlhivmortm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hlhivmortm(Dim1) = CSng(IFsResults("hlhivmortm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim HIVCASES(sNR)
            if Year = FirstYear then
                If IFsResults("HIVCASES").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HIVCASES(Dim1) = CSng(IFsResults("HIVCASES")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim hivculf(11)
            If IFsResults("hivculf").RowCount > (Year - FirstYear) * (1 * 11) Then
                For Dim1 = 1 to 11
                    hivculf(Dim1) = CSng(IFsResults("hivculf")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 11)))
                Next Dim1
            End If

            ReDim HLHIVINC(sNR)
            if Year = FirstYear then
                If IFsResults("HLHIVINC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLHIVINC(Dim1) = CSng(IFsResults("HLHIVINC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLHIVINCPREVR(sNR)
            if Year = FirstYear then
                If IFsResults("HLHIVINCPREVR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLHIVINCPREVR(Dim1) = CSng(IFsResults("HLHIVINCPREVR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim hivincr(sNR)
            If IFsResults("hivincr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hivincr(Dim1) = CSng(IFsResults("hivincr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("hivmdcm").RowCount > (Year - FirstYear) * (1) Then
                hivmdcm = CSng(IFsResults("hivmdcm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hivmdcm = 0
            End If

            ReDim hivm(sNR)
            If IFsResults("hivm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hivm(Dim1) = CSng(IFsResults("hivm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("hivtadvr").RowCount > (Year - FirstYear) * (1) Then
                hivtadvr = CSng(IFsResults("hivtadvr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hivtadvr = 0
            End If

            ReDim HLHIVMORT(sNR)
            if Year = FirstYear then
                If IFsResults("HLHIVMORT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLHIVMORT(Dim1) = CSng(IFsResults("HLHIVMORT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLHIVMORTPREVR(sNR)
            if Year = FirstYear then
                If IFsResults("HLHIVMORTPREVR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLHIVMORTPREVR(Dim1) = CSng(IFsResults("HLHIVMORTPREVR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLHIVPREV(sNR)
            if Year = FirstYear then
                If IFsResults("HLHIVPREV").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLHIVPREV(Dim1) = CSng(IFsResults("HLHIVPREV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim hlhivprevm(sNR)
            If IFsResults("hlhivprevm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hlhivprevm(Dim1) = CSng(IFsResults("hlhivprevm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim hivpeakyr(sNR)
            If IFsResults("hivpeakyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hivpeakyr(Dim1) = CSng(IFsResults("hivpeakyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim HOMICIDEINDEX(sNR)
            if Year = FirstYear then
                If IFsResults("HOMICIDEINDEX").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HOMICIDEINDEX(Dim1) = CSng(IFsResults("HOMICIDEINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ICTCOMPUTERS(sNR)
            if Year = FirstYear then
                If IFsResults("ICTCOMPUTERS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTCOMPUTERS(Dim1) = CSng(IFsResults("ICTCOMPUTERS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim C(sNR)
            if Year = FirstYear then
                If IFsResults("C").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        C(Dim1) = CSng(IFsResults("C")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim CRA(sNR, 3)
            if Year = FirstYear then
                If IFsResults("CRA").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            CRA(Dim1, Dim2) = CSng(if(IFsResults("CRA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HHINCDIS(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HHINCDIS").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HHINCDIS(Dim1, Dim2) = CSng(if(IFsResults("HHINCDIS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HHINCDISPC(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HHINCDISPC").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HHINCDISPC(Dim1, Dim2) = CSng(if(IFsResults("HHINCDISPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HHDIVINT(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HHDIVINT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HHDIVINT(Dim1, Dim2) = CSng(if(IFsResults("HHDIVINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HHINC(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HHINC").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HHINC(Dim1, Dim2) = CSng(if(IFsResults("HHINC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HHINCPC(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HHINCPC").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HHINCPC(Dim1, Dim2) = CSng(if(IFsResults("HHINCPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HHGOVSS(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HHGOVSS").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HHGOVSS(Dim1, Dim2) = CSng(if(IFsResults("HHGOVSS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HHPOP(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HHPOP").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HHPOP(Dim1, Dim2) = CSng(if(IFsResults("HHPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HHSAV(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HHSAV").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HHSAV(Dim1, Dim2) = CSng(if(IFsResults("HHSAV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HHSIZE(sNR)
            If IFsResults("HHSIZE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    HHSIZE(Dim1) = CSng(IFsResults("HHSIZE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim hhsizem(sNR)
            If IFsResults("hhsizem").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hhsizem(Dim1) = CSng(IFsResults("hhsizem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim hhtaxrm(sNR, 3)
            If IFsResults("hhtaxrm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        hhtaxrm(Dim1, Dim2) = CSng(if(IFsResults("hhtaxrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim HHTAXR(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HHTAXR").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HHTAXR(Dim1, Dim2) = CSng(if(IFsResults("HHTAXR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HHTAX(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HHTAX").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HHTAX(Dim1, Dim2) = CSng(if(IFsResults("HHTAX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HHINCEARN(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HHINCEARN").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HHINCEARN(Dim1, Dim2) = CSng(if(IFsResults("HHINCEARN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HHTOTAL(sNR)
            If IFsResults("HHTOTAL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    HHTOTAL(Dim1) = CSng(IFsResults("HHTOTAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ictcomputersm(sNR)
            If IFsResults("ictcomputersm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictcomputersm(Dim1) = CSng(IFsResults("ictcomputersm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ictintnethhm(sNR)
            If IFsResults("ictintnethhm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictintnethhm(Dim1) = CSng(IFsResults("ictintnethhm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim HPI1(sNR)
            if Year = FirstYear then
                If IFsResults("HPI1").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HPI1(Dim1) = CSng(IFsResults("HPI1")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HTCTPI(sNR)
            if Year = FirstYear then
                If IFsResults("HTCTPI").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HTCTPI(Dim1) = CSng(IFsResults("HTCTPI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim inputictm(sNR, 6)
            If IFsResults("inputictm").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        inputictm(Dim1, Dim2) = CSng(if(IFsResults("inputictm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim ICTCYBSECURITY(sNR)
            if Year = FirstYear then
                If IFsResults("ICTCYBSECURITY").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTCYBSECURITY(Dim1) = CSng(IFsResults("ICTCYBSECURITY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ICTSPEND(sNR)
            if Year = FirstYear then
                If IFsResults("ICTSPEND").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTSPEND(Dim1) = CSng(IFsResults("ICTSPEND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ictspendm(sNR)
            If IFsResults("ictspendm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictspendm(Dim1) = CSng(IFsResults("ictspendm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INFRAINDICTZ(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAINDICTZ").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAINDICTZ(Dim1) = CSng(IFsResults("INFRAINDICTZ")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim XBILATICT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("XBILATICT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            XBILATICT(Dim1, Dim2) = CSng(if(IFsResults("XBILATICT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim labempidleper(sNR, 6)
            If IFsResults("labempidleper").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        labempidleper(Dim1, Dim2) = CSng(if(IFsResults("labempidleper")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim LABSIDLE(sNR, 6)
            if Year = FirstYear then
                If IFsResults("LABSIDLE").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            LABSIDLE(Dim1, Dim2) = CSng(if(IFsResults("LABSIDLE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("ximfcreditr").RowCount > (Year - FirstYear) * (1) Then
                ximfcreditr = CSng(IFsResults("ximfcreditr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ximfcreditr = 0
            End If

            ReDim XIMFCRFIN(sNR)
            If IFsResults("XIMFCRFIN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XIMFCRFIN(Dim1) = CSng(IFsResults("XIMFCRFIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("ximfcrintr").RowCount > (Year - FirstYear) * (1) Then
                ximfcrintr = CSng(IFsResults("ximfcrintr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ximfcrintr = 0
            End If

            ReDim XIMFCRFOUT(sNR)
            If IFsResults("XIMFCRFOUT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XIMFCRFOUT(Dim1) = CSng(IFsResults("XIMFCRFOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("ximfcrrepr").RowCount > (Year - FirstYear) * (1) Then
                ximfcrrepr = CSng(IFsResults("ximfcrrepr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ximfcrrepr = 0
            End If

            If IFsResults("ximfcreqr").RowCount > (Year - FirstYear) * (1) Then
                ximfcreqr = CSng(IFsResults("ximfcreqr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ximfcreqr = 0
            End If

            ReDim XIMFCREDIT(sNR)
            If IFsResults("XIMFCREDIT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XIMFCREDIT(Dim1) = CSng(IFsResults("XIMFCREDIT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ximfcrfinm(sNR)
            If IFsResults("ximfcrfinm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ximfcrfinm(Dim1) = CSng(IFsResults("ximfcrfinm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("XIMFCREDITTOT").RowCount > (Year - FirstYear) * (1) Then
                XIMFCREDITTOT = CSng(IFsResults("XIMFCREDITTOT")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                XIMFCREDITTOT = 0
            End If

            ReDim XIMFSUBF(sNR)
            if Year = FirstYear then
                If IFsResults("XIMFSUBF").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        XIMFSUBF(Dim1) = CSng(IFsResults("XIMFSUBF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ECGRINFTEF(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ECGRINFTEF").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ECGRINFTEF(Dim1, Dim2) = CSng(if(IFsResults("ECGRINFTEF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ECGRCLIMGDP(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ECGRCLIMGDP").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ECGRCLIMGDP(Dim1, Dim2) = CSng(if(IFsResults("ECGRCLIMGDP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ECGRCLIMTEF(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ECGRCLIMTEF").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ECGRCLIMTEF(Dim1, Dim2) = CSng(if(IFsResults("ECGRCLIMTEF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim marriageedseclowr(sNR, 2)
            If IFsResults("marriageedseclowr").RowCount > (Year - FirstYear) * (1 * sNR * 2) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 2
                        marriageedseclowr(Dim1, Dim2) = CSng(if(IFsResults("marriageedseclowr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim marriageedsecuppr(sNR, 2)
            If IFsResults("marriageedsecuppr").RowCount > (Year - FirstYear) * (1 * sNR * 2) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 2
                        marriageedsecuppr(Dim1, Dim2) = CSng(if(IFsResults("marriageedsecuppr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim cpowf(sNR)
            If IFsResults("cpowf").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    cpowf(Dim1) = CSng(IFsResults("cpowf")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("mcadjsw").RowCount > (Year - FirstYear) * (1) Then
                mcadjsw = CSng(IFsResults("mcadjsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mcadjsw = 0
            End If

            ReDim MTARIFFTAXR(sNR, 6)
            if Year = FirstYear then
                If IFsResults("MTARIFFTAXR").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            MTARIFFTAXR(Dim1, Dim2) = CSng(if(IFsResults("MTARIFFTAXR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim MTARIFFREV(sNR, 6)
            if Year = FirstYear then
                If IFsResults("MTARIFFREV").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            MTARIFFREV(Dim1, Dim2) = CSng(if(IFsResults("MTARIFFREV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim mtarifftaxrm(sNR, 6)
            If IFsResults("mtarifftaxrm").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        mtarifftaxrm(Dim1, Dim2) = CSng(if(IFsResults("mtarifftaxrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim M(sNR)
            if Year = FirstYear then
                If IFsResults("M").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        M(Dim1) = CSng(IFsResults("M")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MS(sNR, 6)
            If IFsResults("MS").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        MS(Dim1, Dim2) = CSng(if(IFsResults("MS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim msm(sNR, 6)
            If IFsResults("msm").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        msm(Dim1, Dim2) = CSng(if(IFsResults("msm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim elasmincshock(sNR, 6)
            If IFsResults("elasmincshock").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        elasmincshock(Dim1, Dim2) = CSng(if(IFsResults("elasmincshock")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("elmpr2").RowCount > (Year - FirstYear) * (1) Then
                elmpr2 = CSng(IFsResults("elmpr2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elmpr2 = 0
            End If

            If IFsResults("elmpr1").RowCount > (Year - FirstYear) * (1) Then
                elmpr1 = CSng(IFsResults("elmpr1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elmpr1 = 0
            End If

            ReDim elasminc(sNR, 6)
            If IFsResults("elasminc").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        elasminc(Dim1, Dim2) = CSng(if(IFsResults("elasminc")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim MRPA(sNR)
            if Year = FirstYear then
                If IFsResults("MRPA").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MRPA(Dim1) = CSng(IFsResults("MRPA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("hlincoefgr").RowCount > (Year - FirstYear) * (1) Then
                hlincoefgr = CSng(IFsResults("hlincoefgr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlincoefgr = 0
            End If

            If IFsResults("incpovlev").RowCount > (Year - FirstYear) * (1) Then
                incpovlev = CSng(IFsResults("incpovlev")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                incpovlev = 0
            End If

            If IFsResults("wppowtran2").RowCount > (Year - FirstYear) * (1) Then
                wppowtran2 = CSng(IFsResults("wppowtran2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wppowtran2 = 0
            End If

            ReDim INDIRECTTAX(sNR)
            if Year = FirstYear then
                If IFsResults("INDIRECTTAX").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INDIRECTTAX(Dim1) = CSng(IFsResults("INDIRECTTAX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim INDIRECTTAXR(sNR)
            if Year = FirstYear then
                If IFsResults("INDIRECTTAXR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INDIRECTTAXR(Dim1) = CSng(IFsResults("INDIRECTTAXR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim indirecttaxrm(sNR)
            If IFsResults("indirecttaxrm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    indirecttaxrm(Dim1) = CSng(IFsResults("indirecttaxrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim indemm(sNR)
            If IFsResults("indemm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    indemm(Dim1) = CSng(IFsResults("indemm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INDEM(sNR, 3)
            If IFsResults("INDEM").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        INDEM(Dim1, Dim2) = CSng(if(IFsResults("INDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("elagind").RowCount > (Year - FirstYear) * (1) Then
                elagind = CSng(IFsResults("elagind")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elagind = 0
            End If

            ReDim INFMOR(sNR)
            if Year = FirstYear then
                If IFsResults("INFMOR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFMOR(Dim1) = CSng(IFsResults("INFMOR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("infmormax").RowCount > (Year - FirstYear) * (1) Then
                infmormax = CSng(IFsResults("infmormax")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infmormax = 0
            End If

            If IFsResults("infmorttfrlag").RowCount > (Year - FirstYear) * (1) Then
                infmorttfrlag = CSng(IFsResults("infmorttfrlag")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infmorttfrlag = 0
            End If

            ReDim INFLUENCE(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("INFLUENCE").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            INFLUENCE(Dim1, Dim2) = CSng(if(IFsResults("INFLUENCE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim GDPSHADOWSHR(sNR)
            If IFsResults("GDPSHADOWSHR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    GDPSHADOWSHR(Dim1) = CSng(IFsResults("GDPSHADOWSHR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim gdpinformshrm(sNR)
            If IFsResults("gdpinformshrm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    gdpinformshrm(Dim1) = CSng(IFsResults("gdpinformshrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GDPINFORMSHR(sNR)
            If IFsResults("GDPINFORMSHR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    GDPINFORMSHR(Dim1) = CSng(IFsResults("GDPINFORMSHR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim LABINFORMPCNTNONINF(sNR, 3)
            if Year = FirstYear then
                If IFsResults("LABINFORMPCNTNONINF").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            LABINFORMPCNTNONINF(Dim1, Dim2) = CSng(if(IFsResults("LABINFORMPCNTNONINF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim LABINFORMPCNTINF(sNR, 3)
            if Year = FirstYear then
                If IFsResults("LABINFORMPCNTINF").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            LABINFORMPCNTINF(Dim1, Dim2) = CSng(if(IFsResults("LABINFORMPCNTINF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim LABINFORMSHR(sNR)
            If IFsResults("LABINFORMSHR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    LABINFORMSHR(Dim1) = CSng(IFsResults("LABINFORMSHR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim labinformshrm(sNR)
            If IFsResults("labinformshrm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    labinformshrm(Dim1) = CSng(IFsResults("labinformshrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INFRACOSTADJFAC(sNR)
            If IFsResults("INFRACOSTADJFAC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INFRACOSTADJFAC(Dim1) = CSng(IFsResults("INFRACOSTADJFAC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INFRABUDDEMNEW(sNR, 16)
            if Year = FirstYear then
                If IFsResults("INFRABUDDEMNEW").RowCount > (Year - FirstYear) * (1 * sNR * 16) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 16
                            INFRABUDDEMNEW(Dim1, Dim2) = CSng(if(IFsResults("INFRABUDDEMNEW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("infrafinon").RowCount > (Year - FirstYear) * (1) Then
                infrafinon = CSng(IFsResults("infrafinon")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infrafinon = 0
            End If

            ReDim INFRAINVESTMAINT(sNR, 16)
            if Year = FirstYear then
                If IFsResults("INFRAINVESTMAINT").RowCount > (Year - FirstYear) * (1 * sNR * 16) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 16
                            INFRAINVESTMAINT(Dim1, Dim2) = CSng(if(IFsResults("INFRAINVESTMAINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim INFRAINVESTNEW(sNR, 16)
            if Year = FirstYear then
                If IFsResults("INFRAINVESTNEW").RowCount > (Year - FirstYear) * (1 * sNR * 16) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 16
                            INFRAINVESTNEW(Dim1, Dim2) = CSng(if(IFsResults("INFRAINVESTNEW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim infrainvmaintpubshrm(sNR, 16)
            If IFsResults("infrainvmaintpubshrm").RowCount > (Year - FirstYear) * (1 * sNR * 16) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 16
                        infrainvmaintpubshrm(Dim1, Dim2) = CSng(if(IFsResults("infrainvmaintpubshrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim infrainvnewpubshrm(sNR, 16)
            If IFsResults("infrainvnewpubshrm").RowCount > (Year - FirstYear) * (1 * sNR * 16) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 16
                        infrainvnewpubshrm(Dim1, Dim2) = CSng(if(IFsResults("infrainvnewpubshrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim INFRAINVESTMAINTPUB(sNR, 16)
            if Year = FirstYear then
                If IFsResults("INFRAINVESTMAINTPUB").RowCount > (Year - FirstYear) * (1 * sNR * 16) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 16
                            INFRAINVESTMAINTPUB(Dim1, Dim2) = CSng(if(IFsResults("INFRAINVESTMAINTPUB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim INFRAINVESTNEWPUB(sNR, 16)
            if Year = FirstYear then
                If IFsResults("INFRAINVESTNEWPUB").RowCount > (Year - FirstYear) * (1 * sNR * 16) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 16
                            INFRAINVESTNEWPUB(Dim1, Dim2) = CSng(if(IFsResults("INFRAINVESTNEWPUB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("infrapubspndflr").RowCount > (Year - FirstYear) * (1) Then
                infrapubspndflr = CSng(IFsResults("infrapubspndflr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infrapubspndflr = 0
            End If

            If IFsResults("infrapubspndflrsw").RowCount > (Year - FirstYear) * (1) Then
                infrapubspndflrsw = CSng(IFsResults("infrapubspndflrsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infrapubspndflrsw = 0
            End If

            ReDim infrabudgetprotec(sNR, 16)
            If IFsResults("infrabudgetprotec").RowCount > (Year - FirstYear) * (1 * sNR * 16) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 16
                        infrabudgetprotec(Dim1, Dim2) = CSng(if(IFsResults("infrabudgetprotec")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim INFRAINDELECCOMPSDI(4)
            If IFsResults("INFRAINDELECCOMPSDI").RowCount > (Year - FirstYear) * (1 * 4) Then
                For Dim1 = 1 to 4
                    INFRAINDELECCOMPSDI(Dim1) = CSng(IFsResults("INFRAINDELECCOMPSDI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)))
                Next Dim1
            End If

            ReDim INFRAINDELECCOMPMEANI(4)
            If IFsResults("INFRAINDELECCOMPMEANI").RowCount > (Year - FirstYear) * (1 * 4) Then
                For Dim1 = 1 to 4
                    INFRAINDELECCOMPMEANI(Dim1) = CSng(IFsResults("INFRAINDELECCOMPMEANI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)))
                Next Dim1
            End If

            ReDim infraindeleccompwt(4)
            If IFsResults("infraindeleccompwt").RowCount > (Year - FirstYear) * (1 * 4) Then
                For Dim1 = 1 to 4
                    infraindeleccompwt(Dim1) = CSng(IFsResults("infraindeleccompwt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)))
                Next Dim1
            End If

            ReDim INFRAINDICTZCOMPSDI(4)
            If IFsResults("INFRAINDICTZCOMPSDI").RowCount > (Year - FirstYear) * (1 * 4) Then
                For Dim1 = 1 to 4
                    INFRAINDICTZCOMPSDI(Dim1) = CSng(IFsResults("INFRAINDICTZCOMPSDI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)))
                Next Dim1
            End If

            ReDim INFRAINDICTZCOMPMEANI(4)
            If IFsResults("INFRAINDICTZCOMPMEANI").RowCount > (Year - FirstYear) * (1 * 4) Then
                For Dim1 = 1 to 4
                    INFRAINDICTZCOMPMEANI(Dim1) = CSng(IFsResults("INFRAINDICTZCOMPMEANI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)))
                Next Dim1
            End If

            ReDim infraindictzcompwt(4)
            If IFsResults("infraindictzcompwt").RowCount > (Year - FirstYear) * (1 * 4) Then
                For Dim1 = 1 to 4
                    infraindictzcompwt(Dim1) = CSng(IFsResults("infraindictzcompwt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)))
                Next Dim1
            End If

            ReDim INFRAINDTRADCOMPSDI(7)
            If IFsResults("INFRAINDTRADCOMPSDI").RowCount > (Year - FirstYear) * (1 * 7) Then
                For Dim1 = 1 to 7
                    INFRAINDTRADCOMPSDI(Dim1) = CSng(IFsResults("INFRAINDTRADCOMPSDI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 7)))
                Next Dim1
            End If

            ReDim INFRAINDTRADCOMPMEANI(7)
            If IFsResults("INFRAINDTRADCOMPMEANI").RowCount > (Year - FirstYear) * (1 * 7) Then
                For Dim1 = 1 to 7
                    INFRAINDTRADCOMPMEANI(Dim1) = CSng(IFsResults("INFRAINDTRADCOMPMEANI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 7)))
                Next Dim1
            End If

            ReDim INFRAINDTRANCOMPMEANI(4)
            If IFsResults("INFRAINDTRANCOMPMEANI").RowCount > (Year - FirstYear) * (1 * 4) Then
                For Dim1 = 1 to 4
                    INFRAINDTRANCOMPMEANI(Dim1) = CSng(IFsResults("INFRAINDTRANCOMPMEANI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)))
                Next Dim1
            End If

            ReDim INFRAINDTRANCOMPSDI(4)
            If IFsResults("INFRAINDTRANCOMPSDI").RowCount > (Year - FirstYear) * (1 * 4) Then
                For Dim1 = 1 to 4
                    INFRAINDTRANCOMPSDI(Dim1) = CSng(IFsResults("INFRAINDTRANCOMPSDI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)))
                Next Dim1
            End If

            ReDim infraindtrancompwt(4)
            If IFsResults("infraindtrancompwt").RowCount > (Year - FirstYear) * (1 * 4) Then
                For Dim1 = 1 to 4
                    infraindtrancompwt(Dim1) = CSng(IFsResults("infraindtrancompwt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)))
                Next Dim1
            End If

            ReDim INFRAINDWATSANCOMPSDI(3)
            If IFsResults("INFRAINDWATSANCOMPSDI").RowCount > (Year - FirstYear) * (1 * 3) Then
                For Dim1 = 1 to 3
                    INFRAINDWATSANCOMPSDI(Dim1) = CSng(IFsResults("INFRAINDWATSANCOMPSDI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)))
                Next Dim1
            End If

            ReDim INFRAINDWATSANCOMPMEANI(3)
            If IFsResults("INFRAINDWATSANCOMPMEANI").RowCount > (Year - FirstYear) * (1 * 3) Then
                For Dim1 = 1 to 3
                    INFRAINDWATSANCOMPMEANI(Dim1) = CSng(IFsResults("INFRAINDWATSANCOMPMEANI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)))
                Next Dim1
            End If

            ReDim infraindwatsancompwt(3)
            If IFsResults("infraindwatsancompwt").RowCount > (Year - FirstYear) * (1 * 3) Then
                For Dim1 = 1 to 3
                    infraindwatsancompwt(Dim1) = CSng(IFsResults("infraindwatsancompwt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)))
                Next Dim1
            End If

            ReDim INFRAINDELEC(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAINDELEC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAINDELEC(Dim1) = CSng(IFsResults("INFRAINDELEC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim INFRAINDICTDEM(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAINDICTDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAINDICTDEM(Dim1) = CSng(IFsResults("INFRAINDICTDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim INFRAINDTRAD(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAINDTRAD").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAINDTRAD(Dim1) = CSng(IFsResults("INFRAINDTRAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim INFRAINDTRADEXP(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAINDTRADEXP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAINDTRADEXP(Dim1) = CSng(IFsResults("INFRAINDTRADEXP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim INFRAINDWATSAN(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAINDWATSAN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAINDWATSAN(Dim1) = CSng(IFsResults("INFRAINDWATSAN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim DIPLOINGOADVOCACY(sNR)
            if Year = FirstYear then
                If IFsResults("DIPLOINGOADVOCACY").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        DIPLOINGOADVOCACY(Dim1) = CSng(IFsResults("DIPLOINGOADVOCACY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim DIPLOINGOPROGRAM(sNR)
            if Year = FirstYear then
                If IFsResults("DIPLOINGOPROGRAM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        DIPLOINGOPROGRAM(Dim1) = CSng(IFsResults("DIPLOINGOPROGRAM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("enprixi").RowCount > (Year - FirstYear) * (1) Then
                enprixi = CSng(IFsResults("enprixi")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                enprixi = 0
            End If

            ReDim EDTERINT(sNR, 3)
            If IFsResults("EDTERINT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDTERINT(Dim1, Dim2) = CSng(if(IFsResults("EDTERINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("gdpinformcoeffintercept").RowCount > (Year - FirstYear) * (1) Then
                gdpinformcoeffintercept = CSng(IFsResults("gdpinformcoeffintercept")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                gdpinformcoeffintercept = 0
            End If

            If IFsResults("infinformcoeffintercept").RowCount > (Year - FirstYear) * (1) Then
                infinformcoeffintercept = CSng(IFsResults("infinformcoeffintercept")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infinformcoeffintercept = 0
            End If

            If IFsResults("labinformcoeffintercept").RowCount > (Year - FirstYear) * (1) Then
                labinformcoeffintercept = CSng(IFsResults("labinformcoeffintercept")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                labinformcoeffintercept = 0
            End If

            If IFsResults("thrtcoefint").RowCount > (Year - FirstYear) * (1) Then
                thrtcoefint = CSng(IFsResults("thrtcoefint")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                thrtcoefint = 0
            End If

            If IFsResults("gdpshadowcoeffintercept").RowCount > (Year - FirstYear) * (1) Then
                gdpshadowcoeffintercept = CSng(IFsResults("gdpshadowcoeffintercept")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                gdpshadowcoeffintercept = 0
            End If

            ReDim INTR(sNR)
            If IFsResults("INTR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INTR(Dim1) = CSng(IFsResults("INTR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim xportr(sNR)
            If IFsResults("xportr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    xportr(Dim1) = CSng(IFsResults("xportr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XRESERVES(sNR)
            If IFsResults("XRESERVES").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XRESERVES(Dim1) = CSng(IFsResults("XRESERVES")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ICTINTNETHH(sNR)
            if Year = FirstYear then
                If IFsResults("ICTINTNETHH").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTINTNETHH(Dim1) = CSng(IFsResults("ICTINTNETHH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim infranetm(sNR)
            If IFsResults("infranetm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infranetm(Dim1) = CSng(IFsResults("infranetm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INFRANET(sNR)
            If IFsResults("INFRANET").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INFRANET(Dim1) = CSng(IFsResults("INFRANET")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            if Year = FirstYear then
                If IFsResults("GINI").RowCount > (Year - FirstYear) * (1) Then
                    GINI = CSng(IFsResults("GINI")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    GINI = 0
                End If
            Else
                GINI = 0
            End if

            ReDim I(sNR)
            if Year = FirstYear then
                If IFsResults("I").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        I(Dim1) = CSng(IFsResults("I")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim IDS(sNR, 6)
            if Year = FirstYear then
                If IFsResults("IDS").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            IDS(Dim1, Dim2) = CSng(if(IFsResults("IDS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim INVS(sNR, 6)
            If IFsResults("INVS").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        INVS(Dim1, Dim2) = CSng(if(IFsResults("INVS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim idsm(sNR, 6)
            If IFsResults("idsm").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        idsm(Dim1, Dim2) = CSng(if(IFsResults("idsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim invm(sNR)
            If IFsResults("invm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    invm(Dim1) = CSng(IFsResults("invm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim IRA(sNR)
            if Year = FirstYear then
                If IFsResults("IRA").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        IRA(Dim1) = CSng(IFsResults("IRA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim IGCF(sNR)
            if Year = FirstYear then
                If IFsResults("IGCF").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        IGCF(Dim1) = CSng(IFsResults("IGCF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MIGRANTS(sNR)
            if Year = FirstYear then
                If IFsResults("MIGRANTS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MIGRANTS(Dim1) = CSng(IFsResults("MIGRANTS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim migrater(sNR)
            If IFsResults("migrater").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    migrater(Dim1) = CSng(IFsResults("migrater")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim migrateinm(sNR)
            If IFsResults("migrateinm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    migrateinm(Dim1) = CSng(IFsResults("migrateinm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim MIGRATE(sNR)
            If IFsResults("MIGRATE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    MIGRATE(Dim1) = CSng(IFsResults("MIGRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XPORTFIN(sNR)
            If IFsResults("XPORTFIN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XPORTFIN(Dim1) = CSng(IFsResults("XPORTFIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim xportfoliom(sNR)
            If IFsResults("xportfoliom").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    xportfoliom(Dim1) = CSng(IFsResults("xportfoliom")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XPORTFOLIO(sNR)
            If IFsResults("XPORTFOLIO").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XPORTFOLIO(Dim1) = CSng(IFsResults("XPORTFOLIO")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim xfdistockm(sNR)
            If IFsResults("xfdistockm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    xfdistockm(Dim1) = CSng(IFsResults("xfdistockm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim LANDIRAREAACTUAL(sNR)
            if Year = FirstYear then
                If IFsResults("LANDIRAREAACTUAL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        LANDIRAREAACTUAL(Dim1) = CSng(IFsResults("LANDIRAREAACTUAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim landirareaactualm(sNR)
            If IFsResults("landirareaactualm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    landirareaactualm(Dim1) = CSng(IFsResults("landirareaactualm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim WATDEMPERHA(sNR)
            if Year = FirstYear then
                If IFsResults("WATDEMPERHA").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WATDEMPERHA(Dim1) = CSng(IFsResults("WATDEMPERHA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim LANDIRAREAEQUIPDEM(sNR)
            if Year = FirstYear then
                If IFsResults("LANDIRAREAEQUIPDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        LANDIRAREAEQUIPDEM(Dim1) = CSng(IFsResults("LANDIRAREAEQUIPDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("landirlife").RowCount > (Year - FirstYear) * (1) Then
                landirlife = CSng(IFsResults("landirlife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                landirlife = 0
            End If

            ReDim KNOWSOC(sNR)
            if Year = FirstYear then
                If IFsResults("KNOWSOC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        KNOWSOC(Dim1) = CSng(IFsResults("KNOWSOC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim KNENVINDEX(sNR)
            if Year = FirstYear then
                If IFsResults("KNENVINDEX").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        KNENVINDEX(Dim1) = CSng(IFsResults("KNENVINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim KNHCINDEX(sNR)
            if Year = FirstYear then
                If IFsResults("KNHCINDEX").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        KNHCINDEX(Dim1) = CSng(IFsResults("KNHCINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim KNICTINDEX(sNR)
            if Year = FirstYear then
                If IFsResults("KNICTINDEX").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        KNICTINDEX(Dim1) = CSng(IFsResults("KNICTINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim KNINNOVINDEX(sNR)
            if Year = FirstYear then
                If IFsResults("KNINNOVINDEX").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        KNINNOVINDEX(Dim1) = CSng(IFsResults("KNINNOVINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim KNEXTINDEX(sNR)
            if Year = FirstYear then
                If IFsResults("KNEXTINDEX").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        KNEXTINDEX(Dim1) = CSng(IFsResults("KNEXTINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim KNTOTALINDEX(sNR)
            if Year = FirstYear then
                If IFsResults("KNTOTALINDEX").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        KNTOTALINDEX(Dim1) = CSng(IFsResults("KNTOTALINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim LAB(sNR)
            if Year = FirstYear then
                If IFsResults("LAB").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        LAB(Dim1) = CSng(IFsResults("LAB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim LABS(sNR, 6)
            if Year = FirstYear then
                If IFsResults("LABS").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            LABS(Dim1, Dim2) = CSng(if(IFsResults("LABS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim LABCOEFFS(sNR, 6, 3)
            if Year = FirstYear then
                If IFsResults("LABCOEFFS").RowCount > (Year - FirstYear) * (1 * sNR * 6 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            For Dim3 = 1 to 3
                                LABCOEFFS(Dim1, Dim2, Dim3) = CSng(if(IFsResults("LABCOEFFS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim LABDEMS(sNR, 6, 3)
            if Year = FirstYear then
                If IFsResults("LABDEMS").RowCount > (Year - FirstYear) * (1 * sNR * 6 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            For Dim3 = 1 to 3
                                LABDEMS(Dim1, Dim2, Dim3) = CSng(if(IFsResults("LABDEMS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("labmarketsw").RowCount > (Year - FirstYear) * (1) Then
                labmarketsw = CSng(IFsResults("labmarketsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                labmarketsw = 0
            End If

            ReDim labdemsm(sNR, 6)
            If IFsResults("labdemsm").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        labdemsm(Dim1, Dim2) = CSng(if(IFsResults("labdemsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim labretagem(sNR)
            If IFsResults("labretagem").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    labretagem(Dim1) = CSng(IFsResults("labretagem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim LAPOPR(sNR)
            If IFsResults("LAPOPR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    LAPOPR(Dim1) = CSng(IFsResults("LAPOPR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim LABPARR(sNR, 3)
            If IFsResults("LABPARR").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        LABPARR(Dim1, Dim2) = CSng(if(IFsResults("LABPARR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim labparm(sNR, 3)
            If IFsResults("labparm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        labparm(Dim1, Dim2) = CSng(if(IFsResults("labparm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim LABSUP(sNR, 3)
            if Year = FirstYear then
                If IFsResults("LABSUP").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            LABSUP(Dim1, Dim2) = CSng(if(IFsResults("LABSUP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim labunemprtrgtyr(sNR)
            If IFsResults("labunemprtrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    labunemprtrgtyr(Dim1) = CSng(IFsResults("labunemprtrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim LABWAGES(sNR, 6, 3)
            if Year = FirstYear then
                If IFsResults("LABWAGES").RowCount > (Year - FirstYear) * (1 * sNR * 6 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            For Dim3 = 1 to 3
                                LABWAGES(Dim1, Dim2, Dim3) = CSng(if(IFsResults("LABWAGES")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("labwageincfac").RowCount > (Year - FirstYear) * (1) Then
                labwageincfac = CSng(IFsResults("labwageincfac")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                labwageincfac = 0
            End If

            ReDim labwagesexratm(sNR)
            If IFsResults("labwagesexratm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    labwagesexratm(Dim1) = CSng(IFsResults("labwagesexratm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim LABWAGE(sNR, 3)
            if Year = FirstYear then
                If IFsResults("LABWAGE").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            LABWAGE(Dim1, Dim2) = CSng(if(IFsResults("LABWAGE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim LABWAGEIND(sNR)
            if Year = FirstYear then
                If IFsResults("LABWAGEIND").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        LABWAGEIND(Dim1) = CSng(IFsResults("LABWAGEIND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim LABWAGESEXRAT(sNR)
            If IFsResults("LABWAGESEXRAT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    LABWAGESEXRAT(Dim1) = CSng(IFsResults("LABWAGESEXRAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim LABWAGESSHRSKL(sNR, 6, 3)
            If IFsResults("LABWAGESSHRSKL").RowCount > (Year - FirstYear) * (1 * sNR * 6 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        For Dim3 = 1 to 3
                            LABWAGESSHRSKL(Dim1, Dim2, Dim3) = CSng(if(IFsResults("LABWAGESSHRSKL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim landarablepotmed(sNR)
            If IFsResults("landarablepotmed").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    landarablepotmed(Dim1) = CSng(IFsResults("landarablepotmed")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim landarablepotlow(sNR)
            If IFsResults("landarablepotlow").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    landarablepotlow(Dim1) = CSng(IFsResults("landarablepotlow")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim landarablepot(sNR)
            If IFsResults("landarablepot").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    landarablepot(Dim1) = CSng(IFsResults("landarablepot")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("landarablepotsw").RowCount > (Year - FirstYear) * (1) Then
                landarablepotsw = CSng(IFsResults("landarablepotsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                landarablepotsw = 0
            End If

            ReDim LANDAREA(sNR)
            If IFsResults("LANDAREA").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    LANDAREA(Dim1) = CSng(IFsResults("LANDAREA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim LD(sNR, 5)
            If IFsResults("LD").RowCount > (Year - FirstYear) * (1 * sNR * 5) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        LD(Dim1, Dim2) = CSng(if(IFsResults("LD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim landirareaequipm(sNR)
            If IFsResults("landirareaequipm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    landirareaequipm(Dim1) = CSng(IFsResults("landirareaequipm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim landirareasat(sNR)
            If IFsResults("landirareasat").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    landirareasat(Dim1) = CSng(IFsResults("landirareasat")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim LANDIRAREAEQUIP(sNR)
            if Year = FirstYear then
                If IFsResults("LANDIRAREAEQUIP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        LANDIRAREAEQUIP(Dim1) = CSng(IFsResults("LANDIRAREAEQUIP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("ldwf").RowCount > (Year - FirstYear) * (1) Then
                ldwf = CSng(IFsResults("ldwf")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ldwf = 0
            End If

            ReDim LIFEXP(sNR, 3)
            if Year = FirstYear then
                If IFsResults("LIFEXP").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            LIFEXP(Dim1, Dim2) = CSng(if(IFsResults("LIFEXP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim LIFEXPHLM(sNR, 3)
            if Year = FirstYear then
                If IFsResults("LIFEXPHLM").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            LIFEXPHLM(Dim1, Dim2) = CSng(if(IFsResults("LIFEXPHLM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("hdilifemaxf").RowCount > (Year - FirstYear) * (1) Then
                hdilifemaxf = CSng(IFsResults("hdilifemaxf")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hdilifemaxf = 0
            End If

            If IFsResults("hdilifemax").RowCount > (Year - FirstYear) * (1) Then
                hdilifemax = CSng(IFsResults("hdilifemax")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hdilifemax = 0
            End If

            ReDim hdilifemaxun(3)
            If IFsResults("hdilifemaxun").RowCount > (Year - FirstYear) * (1 * 3) Then
                For Dim1 = 1 to 3
                    hdilifemaxun(Dim1) = CSng(IFsResults("hdilifemaxun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)))
                Next Dim1
            End If

            ReDim hdilifeminun(3)
            If IFsResults("hdilifeminun").RowCount > (Year - FirstYear) * (1 * 3) Then
                For Dim1 = 1 to 3
                    hdilifeminun(Dim1) = CSng(IFsResults("hdilifeminun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)))
                Next Dim1
            End If

            If IFsResults("lifexpmin").RowCount > (Year - FirstYear) * (1) Then
                lifexpmin = CSng(IFsResults("lifexpmin")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                lifexpmin = 0
            End If

            ReDim HLCOSTLE(sNR)
            if Year = FirstYear then
                If IFsResults("HLCOSTLE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLCOSTLE(Dim1) = CSng(IFsResults("HLCOSTLE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLCOSTLESUB(sNR)
            if Year = FirstYear then
                If IFsResults("HLCOSTLESUB").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLCOSTLESUB(Dim1) = CSng(IFsResults("HLCOSTLESUB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("hlcostleyrper").RowCount > (Year - FirstYear) * (1) Then
                hlcostleyrper = CSng(IFsResults("hlcostleyrper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlcostleyrper = 0
            End If

            If IFsResults("hlcostletime").RowCount > (Year - FirstYear) * (1) Then
                hlcostletime = CSng(IFsResults("hlcostletime")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlcostletime = 0
            End If

            ReDim lks(6)
            If IFsResults("lks").RowCount > (Year - FirstYear) * (1 * 6) Then
                For Dim1 = 1 to 6
                    lks(Dim1) = CSng(IFsResults("lks")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)))
                Next Dim1
            End If

            If IFsResults("nwarf").RowCount > (Year - FirstYear) * (1) Then
                nwarf = CSng(IFsResults("nwarf")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                nwarf = 0
            End If

            ReDim LIQUIDITY(sNR)
            if Year = FirstYear then
                If IFsResults("LIQUIDITY").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        LIQUIDITY(Dim1) = CSng(IFsResults("LIQUIDITY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim LIT(sNR)
            if Year = FirstYear then
                If IFsResults("LIT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        LIT(Dim1) = CSng(IFsResults("LIT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim FEDDEM(sNR, 3)
            If IFsResults("FEDDEM").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        FEDDEM(Dim1, Dim2) = CSng(if(IFsResults("FEDDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim feddemm(sNR)
            If IFsResults("feddemm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    feddemm(Dim1) = CSng(IFsResults("feddemm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("elglinpr").RowCount > (Year - FirstYear) * (1) Then
                elglinpr = CSng(IFsResults("elglinpr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elglinpr = 0
            End If

            If IFsResults("livhdpro").RowCount > (Year - FirstYear) * (1) Then
                livhdpro = CSng(IFsResults("livhdpro")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                livhdpro = 0
            End If

            If IFsResults("ellvhpr2").RowCount > (Year - FirstYear) * (1) Then
                ellvhpr2 = CSng(IFsResults("ellvhpr2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ellvhpr2 = 0
            End If

            If IFsResults("ellvhpr1").RowCount > (Year - FirstYear) * (1) Then
                ellvhpr1 = CSng(IFsResults("ellvhpr1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ellvhpr1 = 0
            End If

            ReDim lvherdm(sNR)
            If IFsResults("lvherdm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    lvherdm(Dim1) = CSng(IFsResults("lvherdm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim slr(sNR)
            If IFsResults("slr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    slr(Dim1) = CSng(IFsResults("slr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim LVHERD(sNR)
            If IFsResults("LVHERD").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    LVHERD(Dim1) = CSng(IFsResults("LVHERD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("lvcf").RowCount > (Year - FirstYear) * (1) Then
                lvcf = CSng(IFsResults("lvcf")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                lvcf = 0
            End If

            ReDim GOVCONLOCAL(sNR)
            If IFsResults("GOVCONLOCAL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    GOVCONLOCAL(Dim1) = CSng(IFsResults("GOVCONLOCAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GDSLOCAL(sNR, 7)
            If IFsResults("GDSLOCAL").RowCount > (Year - FirstYear) * (1 * sNR * 7) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 7
                        GDSLOCAL(Dim1, Dim2) = CSng(if(IFsResults("GDSLOCAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim GOVEXPLOCAL(sNR)
            If IFsResults("GOVEXPLOCAL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    GOVEXPLOCAL(Dim1) = CSng(IFsResults("GOVEXPLOCAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GOVREVLOCAL(sNR)
            If IFsResults("GOVREVLOCAL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    GOVREVLOCAL(Dim1) = CSng(IFsResults("GOVREVLOCAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edseclowrlen(sNR)
            If IFsResults("edseclowrlen").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edseclowrlen(Dim1) = CSng(IFsResults("edseclowrlen")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edseclowrenrm(sNR, 3)
            If IFsResults("edseclowrenrm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edseclowrenrm(Dim1, Dim2) = CSng(if(IFsResults("edseclowrenrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDEXPERSECLOWR(sNR)
            If IFsResults("EDEXPERSECLOWR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    EDEXPERSECLOWR(Dim1) = CSng(IFsResults("EDEXPERSECLOWR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edseclowrdrpgrdm(sNR, 3)
            If IFsResults("edseclowrdrpgrdm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edseclowrdrpgrdm(Dim1, Dim2) = CSng(if(IFsResults("edseclowrdrpgrdm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edseclowrenrgrdm(sNR, 3)
            If IFsResults("edseclowrenrgrdm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edseclowrenrgrdm(Dim1, Dim2) = CSng(if(IFsResults("edseclowrenrgrdm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edseclowrgradgr(sNR, 3)
            If IFsResults("edseclowrgradgr").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edseclowrgradgr(Dim1, Dim2) = CSng(if(IFsResults("edseclowrgradgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edseclowrgndreqgrad(sNR)
            If IFsResults("edseclowrgndreqgrad").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edseclowrgndreqgrad(Dim1) = CSng(IFsResults("edseclowrgndreqgrad")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edseclowrgratrgtyr(sNR)
            If IFsResults("edseclowrgratrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edseclowrgratrgtyr(Dim1) = CSng(IFsResults("edseclowrgratrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edseclowrgram(sNR, 3)
            If IFsResults("edseclowrgram").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edseclowrgram(Dim1, Dim2) = CSng(if(IFsResults("edseclowrgram")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edseclowrtrangr(sNR, 3)
            If IFsResults("edseclowrtrangr").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edseclowrtrangr(Dim1, Dim2) = CSng(if(IFsResults("edseclowrtrangr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edseclowrtrancap(sNR)
            If IFsResults("edseclowrtrancap").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edseclowrtrancap(Dim1) = CSng(IFsResults("edseclowrtrancap")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edseclowrgndreqtran(sNR)
            If IFsResults("edseclowrgndreqtran").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edseclowrgndreqtran(Dim1) = CSng(IFsResults("edseclowrgndreqtran")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edseclowrtrantrgtyr(sNR)
            If IFsResults("edseclowrtrantrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edseclowrtrantrgtyr(Dim1) = CSng(IFsResults("edseclowrtrantrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edseclowrtranm(sNR, 3)
            If IFsResults("edseclowrtranm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edseclowrtranm(Dim1, Dim2) = CSng(if(IFsResults("edseclowrtranm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edseclowrvocadd(sNR, 3)
            If IFsResults("edseclowrvocadd").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edseclowrvocadd(Dim1, Dim2) = CSng(if(IFsResults("edseclowrvocadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edterpushon(sNR)
            If IFsResults("edterpushon").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edterpushon(Dim1) = CSng(IFsResults("edterpushon")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim hlmalariaincm(sNR)
            If IFsResults("hlmalariaincm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hlmalariaincm(Dim1) = CSng(IFsResults("hlmalariaincm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim HLMALARIAINC(sNR)
            if Year = FirstYear then
                If IFsResults("HLMALARIAINC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLMALARIAINC(Dim1) = CSng(IFsResults("HLMALARIAINC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLMALINCPREVR(sNR)
            if Year = FirstYear then
                If IFsResults("HLMALINCPREVR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLMALINCPREVR(Dim1) = CSng(IFsResults("HLMALINCPREVR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLMALARIAMORT(sNR)
            if Year = FirstYear then
                If IFsResults("HLMALARIAMORT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLMALARIAMORT(Dim1) = CSng(IFsResults("HLMALARIAMORT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLMALMORTPREVR(sNR)
            if Year = FirstYear then
                If IFsResults("HLMALMORTPREVR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLMALMORTPREVR(Dim1) = CSng(IFsResults("HLMALMORTPREVR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim hlmalariamortm(sNR)
            If IFsResults("hlmalariamortm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hlmalariamortm(Dim1) = CSng(IFsResults("hlmalariamortm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim HLMALARIAPREV(sNR)
            if Year = FirstYear then
                If IFsResults("HLMALARIAPREV").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLMALARIAPREV(Dim1) = CSng(IFsResults("HLMALARIAPREV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLMALPREVPOPR(sNR)
            if Year = FirstYear then
                If IFsResults("HLMALPREVPOPR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLMALPREVPOPR(Dim1) = CSng(IFsResults("HLMALPREVPOPR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim hlmalariaprevm(sNR)
            If IFsResults("hlmalariaprevm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hlmalariaprevm(Dim1) = CSng(IFsResults("hlmalariaprevm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim HLCONDOMUSE(sNR)
            if Year = FirstYear then
                If IFsResults("HLCONDOMUSE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLCONDOMUSE(Dim1) = CSng(IFsResults("HLCONDOMUSE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("malemigr").RowCount > (Year - FirstYear) * (1) Then
                malemigr = CSng(IFsResults("malemigr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                malemigr = 0
            End If

            If IFsResults("birthmshr").RowCount > (Year - FirstYear) * (1) Then
                birthmshr = CSng(IFsResults("birthmshr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                birthmshr = 0
            End If

            ReDim MALNCHIL(sNR)
            if Year = FirstYear then
                If IFsResults("MALNCHIL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MALNCHIL(Dim1) = CSng(IFsResults("MALNCHIL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MALNCHPSH(sNR)
            If IFsResults("MALNCHPSH").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    MALNCHPSH(Dim1) = CSng(IFsResults("MALNCHPSH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim MALNPOPP(sNR)
            If IFsResults("MALNPOPP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    MALNPOPP(Dim1) = CSng(IFsResults("MALNPOPP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("malnpoppsw").RowCount > (Year - FirstYear) * (1) Then
                malnpoppsw = CSng(IFsResults("malnpoppsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                malnpoppsw = 0
            End If

            If IFsResults("malnhldsw").RowCount > (Year - FirstYear) * (1) Then
                malnhldsw = CSng(IFsResults("malnhldsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                malnhldsw = 0
            End If

            If IFsResults("hlmlnsw").RowCount > (Year - FirstYear) * (1) Then
                hlmlnsw = CSng(IFsResults("hlmlnsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlmlnsw = 0
            End If

            ReDim malnm(sNR)
            If IFsResults("malnm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    malnm(Dim1) = CSng(IFsResults("malnm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("malnconv").RowCount > (Year - FirstYear) * (1) Then
                malnconv = CSng(IFsResults("malnconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                malnconv = 0
            End If

            ReDim womeninleadershipm(sNR)
            If IFsResults("womeninleadershipm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    womeninleadershipm(Dim1) = CSng(IFsResults("womeninleadershipm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XBILATMAN(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("XBILATMAN").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            XBILATMAN(Dim1, Dim2) = CSng(if(IFsResults("XBILATMAN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim inputmanum(sNR, 6)
            If IFsResults("inputmanum").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        inputmanum(Dim1, Dim2) = CSng(if(IFsResults("inputmanum")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("IMARK").RowCount > (Year - FirstYear) * (1) Then
                IMARK = CSng(IFsResults("IMARK")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                IMARK = 0
            End If

            ReDim inputmatem(sNR, 6)
            If IFsResults("inputmatem").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        inputmatem(Dim1, Dim2) = CSng(if(IFsResults("inputmatem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim MATPOSTR(sNR, 7)
            If IFsResults("MATPOSTR").RowCount > (Year - FirstYear) * (1 * sNR * 7) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 7
                        MATPOSTR(Dim1, Dim2) = CSng(if(IFsResults("MATPOSTR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim matpostradd(sNR)
            If IFsResults("matpostradd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    matpostradd(Dim1) = CSng(IFsResults("matpostradd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("mprconv").RowCount > (Year - FirstYear) * (1) Then
                mprconv = CSng(IFsResults("mprconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mprconv = 0
            End If

            ReDim XBILATMAT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("XBILATMAT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            XBILATMAT(Dim1, Dim2) = CSng(if(IFsResults("XBILATMAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim MATMORTRATIO(sNR)
            If IFsResults("MATMORTRATIO").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    MATMORTRATIO(Dim1) = CSng(IFsResults("MATMORTRATIO")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim matmortratiom(sNR)
            If IFsResults("matmortratiom").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    matmortratiom(Dim1) = CSng(IFsResults("matmortratiom")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            if Year = FirstYear then
                If IFsResults("DIPLOEMBASSYMAX").RowCount > (Year - FirstYear) * (1) Then
                    DIPLOEMBASSYMAX = CSng(IFsResults("DIPLOEMBASSYMAX")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    DIPLOEMBASSYMAX = 0
                End If
            Else
                DIPLOEMBASSYMAX = 0
            End if

            If IFsResults("diploembassymaxm").RowCount > (Year - FirstYear) * (1) Then
                diploembassymaxm = CSng(IFsResults("diploembassymaxm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                diploembassymaxm = 0
            End If

            if Year = FirstYear then
                If IFsResults("DIPLOIGOMAX").RowCount > (Year - FirstYear) * (1) Then
                    DIPLOIGOMAX = CSng(IFsResults("DIPLOIGOMAX")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    DIPLOIGOMAX = 0
                End If
            Else
                DIPLOIGOMAX = 0
            End if

            If IFsResults("diploigomaxm").RowCount > (Year - FirstYear) * (1) Then
                diploigomaxm = CSng(IFsResults("diploigomaxm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                diploigomaxm = 0
            End If

            if Year = FirstYear then
                If IFsResults("DIPLOTREATYMAX").RowCount > (Year - FirstYear) * (1) Then
                    DIPLOTREATYMAX = CSng(IFsResults("DIPLOTREATYMAX")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    DIPLOTREATYMAX = 0
                End If
            Else
                DIPLOTREATYMAX = 0
            End if

            If IFsResults("diplotreatymaxm").RowCount > (Year - FirstYear) * (1) Then
                diplotreatymaxm = CSng(IFsResults("diplotreatymaxm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                diplotreatymaxm = 0
            End If

            If IFsResults("meatmax").RowCount > (Year - FirstYear) * (1) Then
                meatmax = CSng(IFsResults("meatmax")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                meatmax = 0
            End If

            ReDim elasmd(sNR)
            If IFsResults("elasmd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    elasmd(Dim1) = CSng(IFsResults("elasmd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim POPMEDAGE(sNR)
            if Year = FirstYear then
                If IFsResults("POPMEDAGE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POPMEDAGE(Dim1) = CSng(IFsResults("POPMEDAGE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim hltfragestop(sNR)
            If IFsResults("hltfragestop").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hltfragestop(Dim1) = CSng(IFsResults("hltfragestop")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim MFPCOR(sNR)
            if Year = FirstYear then
                If IFsResults("MFPCOR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MFPCOR(Dim1) = CSng(IFsResults("MFPCOR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MFPRATET(sNR)
            if Year = FirstYear then
                If IFsResults("MFPRATET").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MFPRATET(Dim1) = CSng(IFsResults("MFPRATET")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MFPHC(sNR)
            if Year = FirstYear then
                If IFsResults("MFPHC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MFPHC(Dim1) = CSng(IFsResults("MFPHC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MFPPCDEM(sNR)
            if Year = FirstYear then
                If IFsResults("MFPPCDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MFPPCDEM(Dim1) = CSng(IFsResults("MFPPCDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MFPSCCONFL(sNR)
            if Year = FirstYear then
                If IFsResults("MFPSCCONFL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MFPSCCONFL(Dim1) = CSng(IFsResults("MFPSCCONFL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MFPCORS(sNR, 6)
            if Year = FirstYear then
                If IFsResults("MFPCORS").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            MFPCORS(Dim1, Dim2) = CSng(if(IFsResults("MFPCORS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            if Year = FirstYear then
                If IFsResults("MFPGLOCOR").RowCount > (Year - FirstYear) * (1) Then
                    MFPGLOCOR = CSng(IFsResults("MFPGLOCOR")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    MFPGLOCOR = 0
                End If
            Else
                MFPGLOCOR = 0
            End if

            ReDim MFPGROWTH(sNR, 6)
            if Year = FirstYear then
                If IFsResults("MFPGROWTH").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            MFPGROWTH(Dim1, Dim2) = CSng(if(IFsResults("MFPGROWTH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim MFPKN(sNR)
            if Year = FirstYear then
                If IFsResults("MFPKN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MFPKN(Dim1) = CSng(IFsResults("MFPKN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MFPPC(sNR)
            if Year = FirstYear then
                If IFsResults("MFPPC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MFPPC(Dim1) = CSng(IFsResults("MFPPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MFPPREMIUM(sNR)
            if Year = FirstYear then
                If IFsResults("MFPPREMIUM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MFPPREMIUM(Dim1) = CSng(IFsResults("MFPPREMIUM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MFPRATE(sNR, 6)
            if Year = FirstYear then
                If IFsResults("MFPRATE").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            MFPRATE(Dim1, Dim2) = CSng(if(IFsResults("MFPRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim MFPSC(sNR)
            if Year = FirstYear then
                If IFsResults("MFPSC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MFPSC(Dim1) = CSng(IFsResults("MFPSC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MFPBAS(sNR)
            if Year = FirstYear then
                If IFsResults("MFPBAS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MFPBAS(Dim1) = CSng(IFsResults("MFPBAS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MFPTOT(sNR)
            if Year = FirstYear then
                If IFsResults("MFPTOT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MFPTOT(Dim1) = CSng(IFsResults("MFPTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MIGRANTSBILAT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("MIGRANTSBILAT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            MIGRANTSBILAT(Dim1, Dim2) = CSng(if(IFsResults("MIGRANTSBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim MIGRANTSFORCEDOUT(sNR)
            if Year = FirstYear then
                If IFsResults("MIGRANTSFORCEDOUT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MIGRANTSFORCEDOUT(Dim1) = CSng(IFsResults("MIGRANTSFORCEDOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MIGRANTSFORCEDRETURN(sNR)
            if Year = FirstYear then
                If IFsResults("MIGRANTSFORCEDRETURN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MIGRANTSFORCEDRETURN(Dim1) = CSng(IFsResults("MIGRANTSFORCEDRETURN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MIGRANTSIN(sNR)
            if Year = FirstYear then
                If IFsResults("MIGRANTSIN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MIGRANTSIN(Dim1) = CSng(IFsResults("MIGRANTSIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("migminpoplimit").RowCount > (Year - FirstYear) * (1) Then
                migminpoplimit = CSng(IFsResults("migminpoplimit")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                migminpoplimit = 0
            End If

            ReDim MIGRATEIN(sNR)
            If IFsResults("MIGRATEIN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    MIGRATEIN(Dim1) = CSng(IFsResults("MIGRATEIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim migrateinr(sNR)
            If IFsResults("migrateinr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    migrateinr(Dim1) = CSng(IFsResults("migrateinr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim migrateinadd(sNR)
            If IFsResults("migrateinadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    migrateinadd(Dim1) = CSng(IFsResults("migrateinadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim MIGRATEOUT(sNR)
            If IFsResults("MIGRATEOUT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    MIGRATEOUT(Dim1) = CSng(IFsResults("MIGRATEOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim migrateoutr(sNR)
            If IFsResults("migrateoutr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    migrateoutr(Dim1) = CSng(IFsResults("migrateoutr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim migrateoutadd(sNR)
            If IFsResults("migrateoutadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    migrateoutadd(Dim1) = CSng(IFsResults("migrateoutadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim MILEXPSTOCK(sNR)
            if Year = FirstYear then
                If IFsResults("MILEXPSTOCK").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MILEXPSTOCK(Dim1) = CSng(IFsResults("MILEXPSTOCK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("wpmilthr").RowCount > (Year - FirstYear) * (1) Then
                wpmilthr = CSng(IFsResults("wpmilthr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpmilthr = 0
            End If

            ReDim enxl(sNR)
            If IFsResults("enxl").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    enxl(Dim1) = CSng(IFsResults("enxl")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ictbroadmobiltrgtval(sNR)
            If IFsResults("ictbroadmobiltrgtval").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictbroadmobiltrgtval(Dim1) = CSng(IFsResults("ictbroadmobiltrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ictbroadmobiltrgtyr(sNR)
            If IFsResults("ictbroadmobiltrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictbroadmobiltrgtyr(Dim1) = CSng(IFsResults("ictbroadmobiltrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ICTBROADMOBILDEM(sNR)
            if Year = FirstYear then
                If IFsResults("ICTBROADMOBILDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTBROADMOBILDEM(Dim1) = CSng(IFsResults("ICTBROADMOBILDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ictbroadmobilsetar(sNR)
            If IFsResults("ictbroadmobilsetar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictbroadmobilsetar(Dim1) = CSng(IFsResults("ictbroadmobilsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ictbroadmobiltechighrt(sNR)
            If IFsResults("ictbroadmobiltechighrt").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictbroadmobiltechighrt(Dim1) = CSng(IFsResults("ictbroadmobiltechighrt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("ictbroadmobilteclowrt").RowCount > (Year - FirstYear) * (1) Then
                ictbroadmobilteclowrt = CSng(IFsResults("ictbroadmobilteclowrt")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictbroadmobilteclowrt = 0
            End If

            If IFsResults("ictbroadmobiltecinflection").RowCount > (Year - FirstYear) * (1) Then
                ictbroadmobiltecinflection = CSng(IFsResults("ictbroadmobiltecinflection")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictbroadmobiltecinflection = 0
            End If

            ReDim ICTBROADMOBIL(sNR)
            if Year = FirstYear then
                If IFsResults("ICTBROADMOBIL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTBROADMOBIL(Dim1) = CSng(IFsResults("ICTBROADMOBIL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ictbroadmobilseyrtar(sNR)
            If IFsResults("ictbroadmobilseyrtar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictbroadmobilseyrtar(Dim1) = CSng(IFsResults("ictbroadmobilseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ictbroadmobilm(sNR)
            If IFsResults("ictbroadmobilm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictbroadmobilm(Dim1) = CSng(IFsResults("ictbroadmobilm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ICTMOBILCOST(sNR)
            if Year = FirstYear then
                If IFsResults("ICTMOBILCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTMOBILCOST(Dim1) = CSng(IFsResults("ICTMOBILCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("ictmobilcostupper").RowCount > (Year - FirstYear) * (1) Then
                ictmobilcostupper = CSng(IFsResults("ictmobilcostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictmobilcostupper = 0
            End If

            If IFsResults("ictmobilcostlower").RowCount > (Year - FirstYear) * (1) Then
                ictmobilcostlower = CSng(IFsResults("ictmobilcostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictmobilcostlower = 0
            End If

            ReDim ictmobilcostm(sNR)
            If IFsResults("ictmobilcostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictmobilcostm(Dim1) = CSng(IFsResults("ictmobilcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("ictmobillife").RowCount > (Year - FirstYear) * (1) Then
                ictmobillife = CSng(IFsResults("ictmobillife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictmobillife = 0
            End If

            ReDim ICTMOBILDEM(sNR)
            if Year = FirstYear then
                If IFsResults("ICTMOBILDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTMOBILDEM(Dim1) = CSng(IFsResults("ICTMOBILDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("ictmobilsaturation").RowCount > (Year - FirstYear) * (1) Then
                ictmobilsaturation = CSng(IFsResults("ictmobilsaturation")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictmobilsaturation = 0
            End If

            If IFsResults("ictmobilsetar").RowCount > (Year - FirstYear) * (1) Then
                ictmobilsetar = CSng(IFsResults("ictmobilsetar")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictmobilsetar = 0
            End If

            ReDim ICTMOBIL(sNR)
            if Year = FirstYear then
                If IFsResults("ICTMOBIL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTMOBIL(Dim1) = CSng(IFsResults("ICTMOBIL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("ictmobilseyrtar").RowCount > (Year - FirstYear) * (1) Then
                ictmobilseyrtar = CSng(IFsResults("ictmobilseyrtar")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictmobilseyrtar = 0
            End If

            ReDim ictmobiltechighrt(sNR)
            If IFsResults("ictmobiltechighrt").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictmobiltechighrt(Dim1) = CSng(IFsResults("ictmobiltechighrt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("ictmobilteclowrt").RowCount > (Year - FirstYear) * (1) Then
                ictmobilteclowrt = CSng(IFsResults("ictmobilteclowrt")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictmobilteclowrt = 0
            End If

            If IFsResults("ictmobiltecinflection").RowCount > (Year - FirstYear) * (1) Then
                ictmobiltecinflection = CSng(IFsResults("ictmobiltecinflection")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ictmobiltecinflection = 0
            End If

            If IFsResults("MODYEARI").RowCount > (Year - FirstYear) * (1) Then
                MODYEARI = CSng(IFsResults("MODYEARI")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                MODYEARI = 0
            End If

            ReDim HLCOSTMORB(sNR, 15)
            if Year = FirstYear then
                If IFsResults("HLCOSTMORB").RowCount > (Year - FirstYear) * (1 * sNR * 15) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 15
                            HLCOSTMORB(Dim1, Dim2) = CSng(if(IFsResults("HLCOSTMORB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim hlmorbtomortgthport(15)
            If IFsResults("hlmorbtomortgthport").RowCount > (Year - FirstYear) * (1 * 15) Then
                For Dim1 = 1 to 15
                    hlmorbtomortgthport(Dim1) = CSng(IFsResults("hlmorbtomortgthport")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 15)))
                Next Dim1
            End If

            If IFsResults("hlmorbconv").RowCount > (Year - FirstYear) * (1) Then
                hlmorbconv = CSng(IFsResults("hlmorbconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlmorbconv = 0
            End If

            If IFsResults("elhlmortspn").RowCount > (Year - FirstYear) * (1) Then
                elhlmortspn = CSng(IFsResults("elhlmortspn")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elhlmortspn = 0
            End If

            ReDim hlmortconstsw(sNR, 15)
            If IFsResults("hlmortconstsw").RowCount > (Year - FirstYear) * (1 * sNR * 15) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 15
                        hlmortconstsw(Dim1, Dim2) = CSng(if(IFsResults("hlmortconstsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim HLCOSTMORT(sNR, 15)
            if Year = FirstYear then
                If IFsResults("HLCOSTMORT").RowCount > (Year - FirstYear) * (1 * sNR * 15) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 15
                            HLCOSTMORT(Dim1, Dim2) = CSng(if(IFsResults("HLCOSTMORT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("hlmortmodsw").RowCount > (Year - FirstYear) * (1) Then
                hlmortmodsw = CSng(IFsResults("hlmortmodsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlmortmodsw = 0
            End If

            ReDim MORTMG(sNR)
            if Year = FirstYear then
                If IFsResults("MORTMG").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MORTMG(Dim1) = CSng(IFsResults("MORTMG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("elashc").RowCount > (Year - FirstYear) * (1) Then
                elashc = CSng(IFsResults("elashc")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elashc = 0
            End If

            If IFsResults("hlmorttranssw").RowCount > (Year - FirstYear) * (1) Then
                hlmorttranssw = CSng(IFsResults("hlmorttranssw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlmorttranssw = 0
            End If

            ReDim hlmortm(sNR, 15)
            If IFsResults("hlmortm").RowCount > (Year - FirstYear) * (1 * sNR * 15) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 15
                        hlmortm(Dim1, Dim2) = CSng(if(IFsResults("hlmortm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim mortm(sNR)
            If IFsResults("mortm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    mortm(Dim1) = CSng(IFsResults("mortm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("hlscaleconvdown").RowCount > (Year - FirstYear) * (1) Then
                hlscaleconvdown = CSng(IFsResults("hlscaleconvdown")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlscaleconvdown = 0
            End If

            If IFsResults("mortconv").RowCount > (Year - FirstYear) * (1) Then
                mortconv = CSng(IFsResults("mortconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mortconv = 0
            End If

            If IFsResults("extmaposnelectran").RowCount > (Year - FirstYear) * (1) Then
                extmaposnelectran = CSng(IFsResults("extmaposnelectran")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                extmaposnelectran = 0
            End If

            If IFsResults("extmaposnenelecshr").RowCount > (Year - FirstYear) * (1) Then
                extmaposnenelecshr = CSng(IFsResults("extmaposnenelecshr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                extmaposnenelecshr = 0
            End If

            If IFsResults("extmaposnroadpaved").RowCount > (Year - FirstYear) * (1) Then
                extmaposnroadpaved = CSng(IFsResults("extmaposnroadpaved")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                extmaposnroadpaved = 0
            End If

            If IFsResults("extmaposnroad").RowCount > (Year - FirstYear) * (1) Then
                extmaposnroad = CSng(IFsResults("extmaposnroad")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                extmaposnroad = 0
            End If

            If IFsResults("extmafuncelectran").RowCount > (Year - FirstYear) * (1) Then
                extmafuncelectran = CSng(IFsResults("extmafuncelectran")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                extmafuncelectran = 0
            End If

            If IFsResults("extmafuncroadpaved").RowCount > (Year - FirstYear) * (1) Then
                extmafuncroadpaved = CSng(IFsResults("extmafuncroadpaved")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                extmafuncroadpaved = 0
            End If

            If IFsResults("extmafuncroad").RowCount > (Year - FirstYear) * (1) Then
                extmafuncroad = CSng(IFsResults("extmafuncroad")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                extmafuncroad = 0
            End If

            ReDim mfpadd(sNR)
            If IFsResults("mfpadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    mfpadd(Dim1) = CSng(IFsResults("mfpadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim igdprcor(sNR)
            If IFsResults("igdprcor").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    igdprcor(Dim1) = CSng(IFsResults("igdprcor")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim povnational(sNR)
            If IFsResults("povnational").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    povnational(Dim1) = CSng(IFsResults("povnational")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SAVINGS(sNR)
            if Year = FirstYear then
                If IFsResults("SAVINGS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SAVINGS(Dim1) = CSng(IFsResults("SAVINGS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim SAVINGSRPA(sNR)
            if Year = FirstYear then
                If IFsResults("SAVINGSRPA").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SAVINGSRPA(Dim1) = CSng(IFsResults("SAVINGSRPA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim NSNARAT(sNR)
            if Year = FirstYear then
                If IFsResults("NSNARAT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        NSNARAT(Dim1) = CSng(IFsResults("NSNARAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim nsnaratm(sNR)
            If IFsResults("nsnaratm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    nsnaratm(Dim1) = CSng(IFsResults("nsnaratm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim workageretirendpctuse(sNR)
            If IFsResults("workageretirendpctuse").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    workageretirendpctuse(Dim1) = CSng(IFsResults("workageretirendpctuse")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim swingstsdem(9, 3)
            If IFsResults("swingstsdem").RowCount > (Year - FirstYear) * (1 * 9 * 3) Then
                For Dim1 = 1 to 9
                    For Dim2 = 1 to 3
                        swingstsdem(Dim1, Dim2) = CSng(if(IFsResults("swingstsdem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim NEONATMOR(sNR)
            If IFsResults("NEONATMOR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    NEONATMOR(Dim1) = CSng(IFsResults("NEONATMOR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim neonatmorm(sNR)
            If IFsResults("neonatmorm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    neonatmorm(Dim1) = CSng(IFsResults("neonatmorm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            if Year = FirstYear then
                If IFsResults("CARFOROTH").RowCount > (Year - FirstYear) * (1) Then
                    CARFOROTH = CSng(IFsResults("CARFOROTH")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    CARFOROTH = 0
                End If
            Else
                CARFOROTH = 0
            End if

            If IFsResults("carannwi").RowCount > (Year - FirstYear) * (1) Then
                carannwi = CSng(IFsResults("carannwi")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                carannwi = 0
            End If

            ReDim XNFA(sNR)
            If IFsResults("XNFA").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XNFA(Dim1) = CSng(IFsResults("XNFA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XNIIP(sNR)
            If IFsResults("XNIIP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XNIIP(Dim1) = CSng(IFsResults("XNIIP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim NUMNWP(sNR)
            if Year = FirstYear then
                If IFsResults("NUMNWP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        NUMNWP(Dim1) = CSng(IFsResults("NUMNWP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim NUMNWPGR(sNR)
            If IFsResults("NUMNWPGR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    NUMNWPGR(Dim1) = CSng(IFsResults("NUMNWPGR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GOVHHTRNWEL(sNR, 3)
            if Year = FirstYear then
                If IFsResults("GOVHHTRNWEL").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            GOVHHTRNWEL(Dim1, Dim2) = CSng(if(IFsResults("GOVHHTRNWEL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("nsdiv").RowCount > (Year - FirstYear) * (1) Then
                nsdiv = CSng(IFsResults("nsdiv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                nsdiv = 0
            End If

            if Year = FirstYear then
                If IFsResults("NSGAPA").RowCount > (Year - FirstYear) * (1) Then
                    NSGAPA = CSng(IFsResults("NSGAPA")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    NSGAPA = 0
                End If
            Else
                NSGAPA = 0
            End if

            if Year = FirstYear then
                If IFsResults("NSGAPR").RowCount > (Year - FirstYear) * (1) Then
                    NSGAPR = CSng(IFsResults("NSGAPR")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    NSGAPR = 0
                End If
            Else
                NSGAPR = 0
            End if

            if Year = FirstYear then
                If IFsResults("NSGAPRTB").RowCount > (Year - FirstYear) * (1) Then
                    NSGAPRTB = CSng(IFsResults("NSGAPRTB")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    NSGAPRTB = 0
                End If
            Else
                NSGAPRTB = 0
            End if

            if Year = FirstYear then
                If IFsResults("NSPQLI").RowCount > (Year - FirstYear) * (1) Then
                    NSPQLI = CSng(IFsResults("NSPQLI")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    NSPQLI = 0
                End If
            Else
                NSPQLI = 0
            End if

            If IFsResults("elndpow").RowCount > (Year - FirstYear) * (1) Then
                elndpow = CSng(IFsResults("elndpow")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elndpow = 0
            End If

            ReDim nmilf(sNR)
            If IFsResults("nmilf").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    nmilf(Dim1) = CSng(IFsResults("nmilf")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim NPOW(sNR)
            If IFsResults("NPOW").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    NPOW(Dim1) = CSng(IFsResults("NPOW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("drnpow").RowCount > (Year - FirstYear) * (1) Then
                drnpow = CSng(IFsResults("drnpow")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                drnpow = 0
            End If

            If IFsResults("npowf").RowCount > (Year - FirstYear) * (1) Then
                npowf = CSng(IFsResults("npowf")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                npowf = 0
            End If

            ReDim NWARPB(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("NWARPB").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            NWARPB(Dim1, Dim2) = CSng(if(IFsResults("NWARPB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("nwarsv").RowCount > (Year - FirstYear) * (1) Then
                nwarsv = CSng(IFsResults("nwarsv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                nwarsv = 0
            End If

            ReDim NWAR(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("NWAR").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            NWAR(Dim1, Dim2) = CSng(if(IFsResults("NWAR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim DIPLOEMBASSY(sNR)
            If IFsResults("DIPLOEMBASSY").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    DIPLOEMBASSY(Dim1) = CSng(IFsResults("DIPLOEMBASSY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim diploembassym(sNR)
            If IFsResults("diploembassym").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    diploembassym(Dim1) = CSng(IFsResults("diploembassym")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim MARRIEDFEMPOP(sNR, 2)
            If IFsResults("MARRIEDFEMPOP").RowCount > (Year - FirstYear) * (1 * sNR * 2) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 2
                        MARRIEDFEMPOP(Dim1, Dim2) = CSng(if(IFsResults("MARRIEDFEMPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim numnwpgrm(sNR)
            If IFsResults("numnwpgrm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    numnwpgrm(Dim1) = CSng(IFsResults("numnwpgrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("numnwplim").RowCount > (Year - FirstYear) * (1) Then
                numnwplim = CSng(IFsResults("numnwplim")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                numnwplim = 0
            End If

            ReDim GOVNUCWP(sNR)
            If IFsResults("GOVNUCWP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    GOVNUCWP(Dim1) = CSng(IFsResults("GOVNUCWP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim govnucwpadd(sNR)
            If IFsResults("govnucwpadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    govnucwpadd(Dim1) = CSng(IFsResults("govnucwpadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("hlobsw").RowCount > (Year - FirstYear) * (1) Then
                hlobsw = CSng(IFsResults("hlobsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlobsw = 0
            End If

            ReDim hlobesitym(sNR, 3)
            If IFsResults("hlobesitym").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        hlobesitym(Dim1, Dim2) = CSng(if(IFsResults("hlobesitym")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim POPEDSECLOWR(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POPEDSECLOWR").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POPEDSECLOWR(Dim1, Dim2) = CSng(if(IFsResults("POPEDSECLOWR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POPEDSECLOWRGRAD(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POPEDSECLOWRGRAD").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POPEDSECLOWRGRAD(Dim1, Dim2) = CSng(if(IFsResults("POPEDSECLOWRGRAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POPEDPRI(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POPEDPRI").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POPEDPRI(Dim1, Dim2) = CSng(if(IFsResults("POPEDPRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POPEDPRIINT(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POPEDPRIINT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POPEDPRIINT(Dim1, Dim2) = CSng(if(IFsResults("POPEDPRIINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POPEDPRIGRAD(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POPEDPRIGRAD").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POPEDPRIGRAD(Dim1, Dim2) = CSng(if(IFsResults("POPEDPRIGRAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POPEDTER(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POPEDTER").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POPEDTER(Dim1, Dim2) = CSng(if(IFsResults("POPEDTER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POPEDTERGRAD(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POPEDTERGRAD").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POPEDTERGRAD(Dim1, Dim2) = CSng(if(IFsResults("POPEDTERGRAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POPEDSECUPPR(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POPEDSECUPPR").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POPEDSECUPPR(Dim1, Dim2) = CSng(if(IFsResults("POPEDSECUPPR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POPEDSECUPPRGRAD(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POPEDSECUPPRGRAD").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POPEDSECUPPRGRAD(Dim1, Dim2) = CSng(if(IFsResults("POPEDSECUPPRGRAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("oilpcthresw").RowCount > (Year - FirstYear) * (1) Then
                oilpcthresw = CSng(IFsResults("oilpcthresw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                oilpcthresw = 0
            End If

            ReDim diploingoadvocacym(sNR)
            If IFsResults("diploingoadvocacym").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    diploingoadvocacym(Dim1) = CSng(IFsResults("diploingoadvocacym")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("diploingoadvocacyconv").RowCount > (Year - FirstYear) * (1) Then
                diploingoadvocacyconv = CSng(IFsResults("diploingoadvocacyconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                diploingoadvocacyconv = 0
            End If

            ReDim diploingoprogramm(sNR)
            If IFsResults("diploingoprogramm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    diploingoprogramm(Dim1) = CSng(IFsResults("diploingoprogramm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("diploingoprogramconv").RowCount > (Year - FirstYear) * (1) Then
                diploingoprogramconv = CSng(IFsResults("diploingoprogramconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                diploingoprogramconv = 0
            End If

            ReDim DRUGUSEOPIATES(sNR)
            If IFsResults("DRUGUSEOPIATES").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    DRUGUSEOPIATES(Dim1) = CSng(IFsResults("DRUGUSEOPIATES")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim druguseprescriptopioidm(sNR)
            If IFsResults("druguseprescriptopioidm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    druguseprescriptopioidm(Dim1) = CSng(IFsResults("druguseprescriptopioidm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim druguseopiatesm(sNR)
            If IFsResults("druguseopiatesm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    druguseopiatesm(Dim1) = CSng(IFsResults("druguseopiatesm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("hlcostotherrattarget").RowCount > (Year - FirstYear) * (1) Then
                hlcostotherrattarget = CSng(IFsResults("hlcostotherrattarget")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlcostotherrattarget = 0
            End If

            ReDim HLCOSTOTHER(sNR)
            if Year = FirstYear then
                If IFsResults("HLCOSTOTHER").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLCOSTOTHER(Dim1) = CSng(IFsResults("HLCOSTOTHER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim migrateoutm(sNR)
            If IFsResults("migrateoutm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    migrateoutm(Dim1) = CSng(IFsResults("migrateoutm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim xportstoutm(sNR)
            If IFsResults("xportstoutm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    xportstoutm(Dim1) = CSng(IFsResults("xportstoutm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XPORTFOUT(sNR)
            If IFsResults("XPORTFOUT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XPORTFOUT(Dim1) = CSng(IFsResults("XPORTFOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XPORTSTOUT(sNR)
            If IFsResults("XPORTSTOUT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XPORTSTOUT(Dim1) = CSng(IFsResults("XPORTSTOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim xfdistoutm(sNR)
            If IFsResults("xfdistoutm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    xfdistoutm(Dim1) = CSng(IFsResults("xfdistoutm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INFRAROADPAVEDPCNT(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAROADPAVEDPCNT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAROADPAVEDPCNT(Dim1) = CSng(IFsResults("INFRAROADPAVEDPCNT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim INFRAROADPAVEDPCNTDEM(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAROADPAVEDPCNTDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAROADPAVEDPCNTDEM(Dim1) = CSng(IFsResults("INFRAROADPAVEDPCNTDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim hivpeakr(sNR)
            If IFsResults("hivpeakr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hivpeakr(Dim1) = CSng(IFsResults("hivpeakr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim GOVHHTRNPEN(sNR, 3)
            if Year = FirstYear then
                If IFsResults("GOVHHTRNPEN").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            GOVHHTRNPEN(Dim1, Dim2) = CSng(if(IFsResults("GOVHHTRNPEN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim WOMENINLEADERSHIP(sNR)
            if Year = FirstYear then
                If IFsResults("WOMENINLEADERSHIP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WOMENINLEADERSHIP(Dim1) = CSng(IFsResults("WOMENINLEADERSHIP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POV15PCT(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV15PCT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV15PCT(Dim1, Dim2) = CSng(if(IFsResults("POV15PCT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POV15PCT32(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV15PCT32").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV15PCT32(Dim1, Dim2) = CSng(if(IFsResults("POV15PCT32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POV15PCT365(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV15PCT365").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV15PCT365(Dim1, Dim2) = CSng(if(IFsResults("POV15PCT365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POV15PCT420(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV15PCT420").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV15PCT420(Dim1, Dim2) = CSng(if(IFsResults("POV15PCT420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POV15PCT55(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV15PCT55").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV15PCT55(Dim1, Dim2) = CSng(if(IFsResults("POV15PCT55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POV15PCT685(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV15PCT685").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV15PCT685(Dim1, Dim2) = CSng(if(IFsResults("POV15PCT685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POV15PCT830(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV15PCT830").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV15PCT830(Dim1, Dim2) = CSng(if(IFsResults("POV15PCT830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POV25PCT32(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV25PCT32").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV25PCT32(Dim1, Dim2) = CSng(if(IFsResults("POV25PCT32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POV25PCT365(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV25PCT365").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV25PCT365(Dim1, Dim2) = CSng(if(IFsResults("POV25PCT365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POV25PCT420(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV25PCT420").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV25PCT420(Dim1, Dim2) = CSng(if(IFsResults("POV25PCT420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POV25PCT55(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV25PCT55").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV25PCT55(Dim1, Dim2) = CSng(if(IFsResults("POV25PCT55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POV25PCT685(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV25PCT685").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV25PCT685(Dim1, Dim2) = CSng(if(IFsResults("POV25PCT685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POV25PCT830(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV25PCT830").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV25PCT830(Dim1, Dim2) = CSng(if(IFsResults("POV25PCT830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POV25PCTNPL(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV25PCTNPL").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV25PCTNPL(Dim1, Dim2) = CSng(if(IFsResults("POV25PCTNPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POV25PCT(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV25PCT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV25PCT(Dim1, Dim2) = CSng(if(IFsResults("POV25PCT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ICTINTNET(sNR)
            if Year = FirstYear then
                If IFsResults("ICTINTNET").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTINTNET(Dim1) = CSng(IFsResults("ICTINTNET")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POV15PCTNPL(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POV15PCTNPL").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POV15PCTNPL(Dim1, Dim2) = CSng(if(IFsResults("POV15PCTNPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HHINCDISPERM(sNR, 3)
            if Year = FirstYear then
                If IFsResults("HHINCDISPERM").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            HHINCDISPERM(Dim1, Dim2) = CSng(if(IFsResults("HHINCDISPERM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim PERSCON(sNR)
            if Year = FirstYear then
                If IFsResults("PERSCON").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        PERSCON(Dim1) = CSng(IFsResults("PERSCON")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ictindexm(sNR)
            If IFsResults("ictindexm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictindexm(Dim1) = CSng(IFsResults("ictindexm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ICTINDEX(sNR)
            if Year = FirstYear then
                If IFsResults("ICTINDEX").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ICTINDEX(Dim1) = CSng(IFsResults("ICTINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim PQLI(sNR)
            if Year = FirstYear then
                If IFsResults("PQLI").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        PQLI(Dim1) = CSng(IFsResults("PQLI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("ellabwage2").RowCount > (Year - FirstYear) * (1) Then
                ellabwage2 = CSng(IFsResults("ellabwage2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ellabwage2 = 0
            End If

            If IFsResults("ellabwage1").RowCount > (Year - FirstYear) * (1) Then
                ellabwage1 = CSng(IFsResults("ellabwage1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ellabwage1 = 0
            End If

            If IFsResults("elwageunemp2").RowCount > (Year - FirstYear) * (1) Then
                elwageunemp2 = CSng(IFsResults("elwageunemp2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elwageunemp2 = 0
            End If

            If IFsResults("elwageunemp1").RowCount > (Year - FirstYear) * (1) Then
                elwageunemp1 = CSng(IFsResults("elwageunemp1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elwageunemp1 = 0
            End If

            If IFsResults("prdthreshold").RowCount > (Year - FirstYear) * (1) Then
                prdthreshold = CSng(IFsResults("prdthreshold")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                prdthreshold = 0
            End If

            ReDim PRDSEC(sNR)
            if Year = FirstYear then
                If IFsResults("PRDSEC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        PRDSEC(Dim1) = CSng(IFsResults("PRDSEC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim DEMOCPOLITY(sNR)
            If IFsResults("DEMOCPOLITY").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    DEMOCPOLITY(Dim1) = CSng(IFsResults("DEMOCPOLITY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim democpolitysetar(sNR)
            If IFsResults("democpolitysetar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    democpolitysetar(Dim1) = CSng(IFsResults("democpolitysetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim DEMOCPOLYARCHY(sNR)
            If IFsResults("DEMOCPOLYARCHY").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    DEMOCPOLYARCHY(Dim1) = CSng(IFsResults("DEMOCPOLYARCHY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim democpolyarchyadd(sNR)
            If IFsResults("democpolyarchyadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    democpolyarchyadd(Dim1) = CSng(IFsResults("democpolyarchyadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim democpolyarchym(sNR)
            If IFsResults("democpolyarchym").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    democpolyarchym(Dim1) = CSng(IFsResults("democpolyarchym")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim POP(sNR)
            If IFsResults("POP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    POP(Dim1) = CSng(IFsResults("POP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim POPGT65(sNR)
            if Year = FirstYear then
                If IFsResults("POPGT65").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POPGT65(Dim1) = CSng(IFsResults("POPGT65")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POPLE15(sNR)
            if Year = FirstYear then
                If IFsResults("POPLE15").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POPLE15(Dim1) = CSng(IFsResults("POPLE15")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POPPREWORK(sNR)
            if Year = FirstYear then
                If IFsResults("POPPREWORK").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POPPREWORK(Dim1) = CSng(IFsResults("POPPREWORK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POP15TO65(sNR)
            if Year = FirstYear then
                If IFsResults("POP15TO65").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POP15TO65(Dim1) = CSng(IFsResults("POP15TO65")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POPPOVAG(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POPPOVAG").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POPPOVAG(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POPPOVAG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim hlhivartm(sNR)
            If IFsResults("hlhivartm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hlhivartm(Dim1) = CSng(IFsResults("hlhivartm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim minpcgr(sNR)
            If IFsResults("minpcgr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    minpcgr(Dim1) = CSng(IFsResults("minpcgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim POPR(sNR)
            if Year = FirstYear then
                If IFsResults("POPR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POPR(Dim1) = CSng(IFsResults("POPR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POPRURAL(sNR)
            If IFsResults("POPRURAL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    POPRURAL(Dim1) = CSng(IFsResults("POPRURAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim POPRETIRED(sNR)
            if Year = FirstYear then
                If IFsResults("POPRETIRED").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POPRETIRED(Dim1) = CSng(IFsResults("POPRETIRED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim EDSECPER(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDSECPER").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDSECPER(Dim1, Dim2) = CSng(if(IFsResults("EDSECPER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDSECPER15TO24(sNR, 3)
            If IFsResults("EDSECPER15TO24").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECPER15TO24(Dim1, Dim2) = CSng(if(IFsResults("EDSECPER15TO24")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDSECPER20TO24(sNR, 3)
            If IFsResults("EDSECPER20TO24").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECPER20TO24(Dim1, Dim2) = CSng(if(IFsResults("EDSECPER20TO24")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDTERPER(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDTERPER").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDTERPER(Dim1, Dim2) = CSng(if(IFsResults("EDTERPER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POP0TO5(sNR)
            if Year = FirstYear then
                If IFsResults("POP0TO5").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POP0TO5(Dim1) = CSng(IFsResults("POP0TO5")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POP15ANDABOVE(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POP15ANDABOVE").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POP15ANDABOVE(Dim1, Dim2) = CSng(if(IFsResults("POP15ANDABOVE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POP25ANDABOVE(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POP25ANDABOVE").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POP25ANDABOVE(Dim1, Dim2) = CSng(if(IFsResults("POP25ANDABOVE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POPFEM15TO24(sNR)
            if Year = FirstYear then
                If IFsResults("POPFEM15TO24").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POPFEM15TO24(Dim1) = CSng(IFsResults("POPFEM15TO24")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POPFOREIGN(sNR)
            If IFsResults("POPFOREIGN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    POPFOREIGN(Dim1) = CSng(IFsResults("POPFOREIGN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("xportwgrm").RowCount > (Year - FirstYear) * (1) Then
                xportwgrm = CSng(IFsResults("xportwgrm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                xportwgrm = 0
            End If

            If IFsResults("wpthrwar").RowCount > (Year - FirstYear) * (1) Then
                wpthrwar = CSng(IFsResults("wpthrwar")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpthrwar = 0
            End If

            ReDim POTSUPRAT(sNR)
            if Year = FirstYear then
                If IFsResults("POTSUPRAT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POTSUPRAT(Dim1) = CSng(IFsResults("POTSUPRAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim INCOMELT125CS2005(sNR)
            If IFsResults("INCOMELT125CS2005").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INCOMELT125CS2005(Dim1) = CSng(IFsResults("INCOMELT125CS2005")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INCOMELT110LN(sNR)
            If IFsResults("INCOMELT110LN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INCOMELT110LN(Dim1) = CSng(IFsResults("INCOMELT110LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INCOMELT125LN2005(sNR)
            If IFsResults("INCOMELT125LN2005").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INCOMELT125LN2005(Dim1) = CSng(IFsResults("INCOMELT125LN2005")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INCOMELT190LN(sNR)
            If IFsResults("INCOMELT190LN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INCOMELT190LN(Dim1) = CSng(IFsResults("INCOMELT190LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INCOMELT200CS2005(sNR)
            If IFsResults("INCOMELT200CS2005").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INCOMELT200CS2005(Dim1) = CSng(IFsResults("INCOMELT200CS2005")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INCOMELT200LN2005(sNR)
            If IFsResults("INCOMELT200LN2005").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INCOMELT200LN2005(Dim1) = CSng(IFsResults("INCOMELT200LN2005")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INCOMELT215LN(sNR)
            If IFsResults("INCOMELT215LN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INCOMELT215LN(Dim1) = CSng(IFsResults("INCOMELT215LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim POVEXTREME(sNR)
            If IFsResults("POVEXTREME").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    POVEXTREME(Dim1) = CSng(IFsResults("POVEXTREME")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INCOMELT310LN(sNR)
            If IFsResults("INCOMELT310LN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INCOMELT310LN(Dim1) = CSng(IFsResults("INCOMELT310LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INCOMELT320LN(sNR)
            If IFsResults("INCOMELT320LN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INCOMELT320LN(Dim1) = CSng(IFsResults("INCOMELT320LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INCOMELT365LN(sNR)
            If IFsResults("INCOMELT365LN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INCOMELT365LN(Dim1) = CSng(IFsResults("INCOMELT365LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim POVMIDLOW(sNR)
            If IFsResults("POVMIDLOW").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    POVMIDLOW(Dim1) = CSng(IFsResults("POVMIDLOW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INCOMELT550LN(sNR)
            If IFsResults("INCOMELT550LN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INCOMELT550LN(Dim1) = CSng(IFsResults("INCOMELT550LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INCOMELT685LN(sNR)
            If IFsResults("INCOMELT685LN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INCOMELT685LN(Dim1) = CSng(IFsResults("INCOMELT685LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim POVMIDHIGH(sNR)
            If IFsResults("POVMIDHIGH").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    POVMIDHIGH(Dim1) = CSng(IFsResults("POVMIDHIGH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("povagesexgeneric").RowCount > (Year - FirstYear) * (1) Then
                povagesexgeneric = CSng(IFsResults("povagesexgeneric")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                povagesexgeneric = 0
            End If

            If IFsResults("povagesexdynamic").RowCount > (Year - FirstYear) * (1) Then
                povagesexdynamic = CSng(IFsResults("povagesexdynamic")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                povagesexdynamic = 0
            End If

            If IFsResults("povagesexnormstatic").RowCount > (Year - FirstYear) * (1) Then
                povagesexnormstatic = CSng(IFsResults("povagesexnormstatic")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                povagesexnormstatic = 0
            End If

            ReDim POVGAPNPL(sNR)
            if Year = FirstYear then
                If IFsResults("POVGAPNPL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POVGAPNPL(Dim1) = CSng(IFsResults("POVGAPNPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POVGAP(sNR)
            if Year = FirstYear then
                If IFsResults("POVGAP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POVGAP(Dim1) = CSng(IFsResults("POVGAP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POVGAP32(sNR)
            if Year = FirstYear then
                If IFsResults("POVGAP32").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POVGAP32(Dim1) = CSng(IFsResults("POVGAP32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POVGAP365(sNR)
            if Year = FirstYear then
                If IFsResults("POVGAP365").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POVGAP365(Dim1) = CSng(IFsResults("POVGAP365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POVGAP420(sNR)
            if Year = FirstYear then
                If IFsResults("POVGAP420").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POVGAP420(Dim1) = CSng(IFsResults("POVGAP420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POVGAP55(sNR)
            if Year = FirstYear then
                If IFsResults("POVGAP55").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POVGAP55(Dim1) = CSng(IFsResults("POVGAP55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POVGAP685(sNR)
            if Year = FirstYear then
                If IFsResults("POVGAP685").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POVGAP685(Dim1) = CSng(IFsResults("POVGAP685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POVGAP830(sNR)
            if Year = FirstYear then
                If IFsResults("POVGAP830").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POVGAP830(Dim1) = CSng(IFsResults("POVGAP830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim POVGNDRATIO(sNR, 11)
            If IFsResults("POVGNDRATIO").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        POVGNDRATIO(Dim1, Dim2) = CSng(if(IFsResults("POVGNDRATIO")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim POVGNDRATIO32(sNR, 11)
            If IFsResults("POVGNDRATIO32").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        POVGNDRATIO32(Dim1, Dim2) = CSng(if(IFsResults("POVGNDRATIO32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim POVGNDRATIO365(sNR, 11)
            If IFsResults("POVGNDRATIO365").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        POVGNDRATIO365(Dim1, Dim2) = CSng(if(IFsResults("POVGNDRATIO365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim POVGNDRATIO420(sNR, 11)
            If IFsResults("POVGNDRATIO420").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        POVGNDRATIO420(Dim1, Dim2) = CSng(if(IFsResults("POVGNDRATIO420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim POVGNDRATIO55(sNR, 11)
            If IFsResults("POVGNDRATIO55").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        POVGNDRATIO55(Dim1, Dim2) = CSng(if(IFsResults("POVGNDRATIO55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim POVGNDRATIO685(sNR, 11)
            If IFsResults("POVGNDRATIO685").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        POVGNDRATIO685(Dim1, Dim2) = CSng(if(IFsResults("POVGNDRATIO685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim POVGNDRATIO830(sNR, 11)
            If IFsResults("POVGNDRATIO830").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        POVGNDRATIO830(Dim1, Dim2) = CSng(if(IFsResults("POVGNDRATIO830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim POVGNDRATIONPL(sNR, 11)
            If IFsResults("POVGNDRATIONPL").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        POVGNDRATIONPL(Dim1, Dim2) = CSng(if(IFsResults("POVGNDRATIONPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim POVCOUNT(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVCOUNT").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVCOUNT(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVCOUNT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVCOUNT32(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVCOUNT32").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVCOUNT32(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVCOUNT32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVCOUNT365(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVCOUNT365").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVCOUNT365(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVCOUNT365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVCOUNT420(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVCOUNT420").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVCOUNT420(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVCOUNT420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVCOUNT55(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVCOUNT55").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVCOUNT55(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVCOUNT55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVCOUNT685(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVCOUNT685").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVCOUNT685(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVCOUNT685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVCOUNT830(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVCOUNT830").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVCOUNT830(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVCOUNT830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVCOUNTNPL(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVCOUNTNPL").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVCOUNTNPL(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVCOUNTNPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVRATE(sNR, 11, 3)
            If IFsResults("POVRATE").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        For Dim3 = 1 to 3
                            POVRATE(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim POVRATE32(sNR, 11, 3)
            If IFsResults("POVRATE32").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        For Dim3 = 1 to 3
                            POVRATE32(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVRATE32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim POVRATE365(sNR, 11, 3)
            If IFsResults("POVRATE365").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        For Dim3 = 1 to 3
                            POVRATE365(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVRATE365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim POVRATE420(sNR, 11, 3)
            If IFsResults("POVRATE420").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        For Dim3 = 1 to 3
                            POVRATE420(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVRATE420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim POVRATE55(sNR, 11, 3)
            If IFsResults("POVRATE55").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        For Dim3 = 1 to 3
                            POVRATE55(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVRATE55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim POVRATE685(sNR, 11, 3)
            If IFsResults("POVRATE685").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        For Dim3 = 1 to 3
                            POVRATE685(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVRATE685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim POVRATE830(sNR, 11, 3)
            If IFsResults("POVRATE830").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        For Dim3 = 1 to 3
                            POVRATE830(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVRATE830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim POVRATENPL(sNR, 11, 3)
            If IFsResults("POVRATENPL").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 11
                        For Dim3 = 1 to 3
                            POVRATENPL(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVRATENPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim POVSHORT(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVSHORT").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVSHORT(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVSHORT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVSHORT32(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVSHORT32").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVSHORT32(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVSHORT32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVSHORT365(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVSHORT365").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVSHORT365(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVSHORT365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVSHORT420(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVSHORT420").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVSHORT420(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVSHORT420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVSHORT55(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVSHORT55").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVSHORT55(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVSHORT55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVSHORT685(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVSHORT685").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVSHORT685(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVSHORT685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVSHORT830(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVSHORT830").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVSHORT830(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVSHORT830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVSHORTNPL(sNR, 11, 3)
            if Year = FirstYear then
                If IFsResults("POVSHORTNPL").RowCount > (Year - FirstYear) * (1 * sNR * 11 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            For Dim3 = 1 to 3
                                POVSHORTNPL(Dim1, Dim2, Dim3) = CSng(if(IFsResults("POVSHORTNPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVSHORTSEXDIFF(sNR, 11)
            if Year = FirstYear then
                If IFsResults("POVSHORTSEXDIFF").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            POVSHORTSEXDIFF(Dim1, Dim2) = CSng(if(IFsResults("POVSHORTSEXDIFF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVSHORTSEXDIFF32(sNR, 11)
            if Year = FirstYear then
                If IFsResults("POVSHORTSEXDIFF32").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            POVSHORTSEXDIFF32(Dim1, Dim2) = CSng(if(IFsResults("POVSHORTSEXDIFF32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVSHORTSEXDIFF365(sNR, 11)
            if Year = FirstYear then
                If IFsResults("POVSHORTSEXDIFF365").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            POVSHORTSEXDIFF365(Dim1, Dim2) = CSng(if(IFsResults("POVSHORTSEXDIFF365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVSHORTSEXDIFF55(sNR, 11)
            if Year = FirstYear then
                If IFsResults("POVSHORTSEXDIFF55").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            POVSHORTSEXDIFF55(Dim1, Dim2) = CSng(if(IFsResults("POVSHORTSEXDIFF55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVSHORTSEXDIFF685(sNR, 11)
            if Year = FirstYear then
                If IFsResults("POVSHORTSEXDIFF685").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            POVSHORTSEXDIFF685(Dim1, Dim2) = CSng(if(IFsResults("POVSHORTSEXDIFF685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVSHORTSEXDIFF830(sNR, 11)
            if Year = FirstYear then
                If IFsResults("POVSHORTSEXDIFF830").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            POVSHORTSEXDIFF830(Dim1, Dim2) = CSng(if(IFsResults("POVSHORTSEXDIFF830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim POVSHORTSEXDIFFNPL(sNR, 11)
            if Year = FirstYear then
                If IFsResults("POVSHORTSEXDIFFNPL").RowCount > (Year - FirstYear) * (1 * sNR * 11) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 11
                            POVSHORTSEXDIFFNPL(Dim1, Dim2) = CSng(if(IFsResults("POVSHORTSEXDIFFNPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("povagesexagratnat").RowCount > (Year - FirstYear) * (1) Then
                povagesexagratnat = CSng(IFsResults("povagesexagratnat")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                povagesexagratnat = 0
            End If

            ReDim INCOMELTNPLLN(sNR)
            If IFsResults("INCOMELTNPLLN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INCOMELTNPLLN(Dim1) = CSng(IFsResults("INCOMELTNPLLN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("wpconmax").RowCount > (Year - FirstYear) * (1) Then
                wpconmax = CSng(IFsResults("wpconmax")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpconmax = 0
            End If

            If IFsResults("wpconmin").RowCount > (Year - FirstYear) * (1) Then
                wpconmin = CSng(IFsResults("wpconmin")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpconmin = 0
            End If

            if Year = FirstYear then
                If IFsResults("PCONGREATEU").RowCount > (Year - FirstYear) * (1) Then
                    PCONGREATEU = CSng(IFsResults("PCONGREATEU")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    PCONGREATEU = 0
                End If
            Else
                PCONGREATEU = 0
            End if

            if Year = FirstYear then
                If IFsResults("PCONGREATEUF").RowCount > (Year - FirstYear) * (1) Then
                    PCONGREATEUF = CSng(IFsResults("PCONGREATEUF")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    PCONGREATEUF = 0
                End If
            Else
                PCONGREATEUF = 0
            End if

            if Year = FirstYear then
                If IFsResults("PCONSYS").RowCount > (Year - FirstYear) * (1) Then
                    PCONSYS = CSng(IFsResults("PCONSYS")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    PCONSYS = 0
                End If
            Else
                PCONSYS = 0
            End if

            if Year = FirstYear then
                If IFsResults("PCONGREAT").RowCount > (Year - FirstYear) * (1) Then
                    PCONGREAT = CSng(IFsResults("PCONGREAT")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    PCONGREAT = 0
                End If
            Else
                PCONGREAT = 0
            End if

            if Year = FirstYear then
                If IFsResults("PCONGREATF").RowCount > (Year - FirstYear) * (1) Then
                    PCONGREATF = CSng(IFsResults("PCONGREATF")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    PCONGREATF = 0
                End If
            Else
                PCONGREATF = 0
            End if

            If IFsResults("wppowtran1").RowCount > (Year - FirstYear) * (1) Then
                wppowtran1 = CSng(IFsResults("wppowtran1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wppowtran1 = 0
            End If

            If IFsResults("thrtcoefpwr").RowCount > (Year - FirstYear) * (1) Then
                thrtcoefpwr = CSng(IFsResults("thrtcoefpwr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                thrtcoefpwr = 0
            End If

            ReDim PPPCONV(sNR)
            if Year = FirstYear then
                If IFsResults("PPPCONV").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        PPPCONV(Dim1) = CSng(IFsResults("PPPCONV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("co2prein").RowCount > (Year - FirstYear) * (1) Then
                co2prein = CSng(IFsResults("co2prein")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                co2prein = 0
            End If

            ReDim CPREWORK(sNR)
            if Year = FirstYear then
                If IFsResults("CPREWORK").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CPREWORK(Dim1) = CSng(IFsResults("CPREWORK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim DRUGUSEPRESCRIPTOPIOID(sNR)
            If IFsResults("DRUGUSEPRESCRIPTOPIOID").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    DRUGUSEPRESCRIPTOPIOID(Dim1) = CSng(IFsResults("DRUGUSEPRESCRIPTOPIOID")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim HLDIABPREV(sNR)
            If IFsResults("HLDIABPREV").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    HLDIABPREV(Dim1) = CSng(IFsResults("HLDIABPREV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim HLDIABIGT(sNR)
            If IFsResults("HLDIABIGT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    HLDIABIGT(Dim1) = CSng(IFsResults("HLDIABIGT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("encartpp").RowCount > (Year - FirstYear) * (1) Then
                encartpp = CSng(IFsResults("encartpp")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                encartpp = 0
            End If

            ReDim PRI(sNR, 6)
            if Year = FirstYear then
                If IFsResults("PRI").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            PRI(Dim1, Dim2) = CSng(if(IFsResults("PRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            If IFsResults("elprst2").RowCount > (Year - FirstYear) * (1) Then
                elprst2 = CSng(IFsResults("elprst2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elprst2 = 0
            End If

            If IFsResults("elprst1").RowCount > (Year - FirstYear) * (1) Then
                elprst1 = CSng(IFsResults("elprst1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elprst1 = 0
            End If

            ReDim edqualpriallm(sNR, 3)
            If IFsResults("edqualpriallm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edqualpriallm(Dim1, Dim2) = CSng(if(IFsResults("edqualpriallm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edprienrm(sNR, 3)
            If IFsResults("edprienrm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edprienrm(Dim1, Dim2) = CSng(if(IFsResults("edprienrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDEXPERPRI(sNR)
            If IFsResults("EDEXPERPRI").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    EDEXPERPRI(Dim1) = CSng(IFsResults("EDEXPERPRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edpridrpgrdm(sNR, 3)
            If IFsResults("edpridrpgrdm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edpridrpgrdm(Dim1, Dim2) = CSng(if(IFsResults("edpridrpgrdm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edprienrgrdm(sNR, 3)
            If IFsResults("edprienrgrdm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edprienrgrdm(Dim1, Dim2) = CSng(if(IFsResults("edprienrgrdm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edprigndreqintn(sNR)
            If IFsResults("edprigndreqintn").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edprigndreqintn(Dim1) = CSng(IFsResults("edprigndreqintn")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim EDPRIINT(sNR, 3)
            If IFsResults("EDPRIINT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDPRIINT(Dim1, Dim2) = CSng(if(IFsResults("EDPRIINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDPRIINTN(sNR, 3)
            If IFsResults("EDPRIINTN").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDPRIINTN(Dim1, Dim2) = CSng(if(IFsResults("EDPRIINTN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDPRIINTNEXT(sNR, 3)
            If IFsResults("EDPRIINTNEXT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDPRIINTNEXT(Dim1, Dim2) = CSng(if(IFsResults("EDPRIINTNEXT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edqualprimathm(sNR, 3)
            If IFsResults("edqualprimathm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edqualprimathm(Dim1, Dim2) = CSng(if(IFsResults("edqualprimathm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDQUALPRIALL(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDQUALPRIALL").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDQUALPRIALL(Dim1, Dim2) = CSng(if(IFsResults("EDQUALPRIALL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDQUALPRIMATH(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDQUALPRIMATH").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDQUALPRIMATH(Dim1, Dim2) = CSng(if(IFsResults("EDQUALPRIMATH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDQUALPRIREAD(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDQUALPRIREAD").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDQUALPRIREAD(Dim1, Dim2) = CSng(if(IFsResults("EDQUALPRIREAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDQUALPRISCI(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDQUALPRISCI").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDQUALPRISCI(Dim1, Dim2) = CSng(if(IFsResults("EDQUALPRISCI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim edpriintngr(sNR, 3)
            If IFsResults("edpriintngr").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edpriintngr(Dim1, Dim2) = CSng(if(IFsResults("edpriintngr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edpriintntrgtyr(sNR)
            If IFsResults("edpriintntrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edpriintntrgtyr(Dim1) = CSng(IFsResults("edpriintntrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edpriintnm(sNR, 3)
            If IFsResults("edpriintnm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edpriintnm(Dim1, Dim2) = CSng(if(IFsResults("edpriintnm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edqualprireadm(sNR, 3)
            If IFsResults("edqualprireadm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edqualprireadm(Dim1, Dim2) = CSng(if(IFsResults("edqualprireadm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edqualpriscim(sNR, 3)
            If IFsResults("edqualpriscim").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edqualpriscim(Dim1, Dim2) = CSng(if(IFsResults("edqualpriscim")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edprisurgr(sNR, 3)
            If IFsResults("edprisurgr").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edprisurgr(Dim1, Dim2) = CSng(if(IFsResults("edprisurgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edprigndreqsur(sNR)
            If IFsResults("edprigndreqsur").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edprigndreqsur(Dim1) = CSng(IFsResults("edprigndreqsur")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edprisurtrgtyr(sNR)
            If IFsResults("edprisurtrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edprisurtrgtyr(Dim1) = CSng(IFsResults("edprisurtrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edprisurm(sNR, 3)
            If IFsResults("edprisurm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edprisurm(Dim1, Dim2) = CSng(if(IFsResults("edprisurm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDPRITOT(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDPRITOT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDPRITOT(Dim1, Dim2) = CSng(if(IFsResults("EDPRITOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HLEXPENDPRIV(sNR)
            if Year = FirstYear then
                If IFsResults("HLEXPENDPRIV").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLEXPENDPRIV(Dim1) = CSng(IFsResults("HLEXPENDPRIV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ictcybevprob(4, 3)
            If IFsResults("ictcybevprob").RowCount > (Year - FirstYear) * (1 * 4 * 3) Then
                For Dim1 = 1 to 4
                    For Dim2 = 1 to 3
                        ictcybevprob(Dim1, Dim2) = CSng(if(IFsResults("ictcybevprob")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim PFD(sNR, 6)
            if Year = FirstYear then
                If IFsResults("PFD").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            PFD(Dim1, Dim2) = CSng(if(IFsResults("PFD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim prodtf(sNR, 9)
            If IFsResults("prodtf").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        prodtf(Dim1, Dim2) = CSng(if(IFsResults("prodtf")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim AGPMILKEGGS(sNR)
            If IFsResults("AGPMILKEGGS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    AGPMILKEGGS(Dim1) = CSng(IFsResults("AGPMILKEGGS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim tefinfadjm(sNR)
            If IFsResults("tefinfadjm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    tefinfadjm(Dim1) = CSng(IFsResults("tefinfadjm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim tfpshockadjshr(sNR)
            If IFsResults("tfpshockadjshr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    tfpshockadjshr(Dim1) = CSng(IFsResults("tfpshockadjshr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim protecm(sNR, 6)
            If IFsResults("protecm").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        protecm(Dim1, Dim2) = CSng(if(IFsResults("protecm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim PROTEINPC(sNR, 4)
            if Year = FirstYear then
                If IFsResults("PROTEINPC").RowCount > (Year - FirstYear) * (1 * sNR * 4) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 4
                            PROTEINPC(Dim1, Dim2) = CSng(if(IFsResults("PROTEINPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim INFRABUDDEMMNT(sNR, 16)
            if Year = FirstYear then
                If IFsResults("INFRABUDDEMMNT").RowCount > (Year - FirstYear) * (1 * sNR * 16) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 16
                            INFRABUDDEMMNT(Dim1, Dim2) = CSng(if(IFsResults("INFRABUDDEMMNT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDPRIPTR(sNR)
            If IFsResults("EDPRIPTR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    EDPRIPTR(Dim1) = CSng(IFsResults("EDPRIPTR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim EDQUALAG15(sNR, 3)
            If IFsResults("EDQUALAG15").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDQUALAG15(Dim1, Dim2) = CSng(if(IFsResults("EDQUALAG15")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDQUALAG15PRI(sNR, 3)
            If IFsResults("EDQUALAG15PRI").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDQUALAG15PRI(Dim1, Dim2) = CSng(if(IFsResults("EDQUALAG15PRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDQUALAG15SEC(sNR, 3)
            If IFsResults("EDQUALAG15SEC").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDQUALAG15SEC(Dim1, Dim2) = CSng(if(IFsResults("EDQUALAG15SEC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim randdexpm(sNR)
            If IFsResults("randdexpm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    randdexpm(Dim1) = CSng(IFsResults("randdexpm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim RANDDEXP(sNR)
            If IFsResults("RANDDEXP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    RANDDEXP(Dim1) = CSng(IFsResults("RANDDEXP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("hwf").RowCount > (Year - FirstYear) * (1) Then
                hwf = CSng(IFsResults("hwf")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hwf = 0
            End If

            ReDim rdm(sNR, 9)
            If IFsResults("rdm").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        rdm(Dim1, Dim2) = CSng(if(IFsResults("rdm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim rdi(9)
            If IFsResults("rdi").RowCount > (Year - FirstYear) * (1 * 9) Then
                For Dim1 = 1 to 9
                    rdi(Dim1) = CSng(IFsResults("rdi")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9)))
                Next Dim1
            End If

            If IFsResults("hlcostotherrattargetconv").RowCount > (Year - FirstYear) * (1) Then
                hlcostotherrattargetconv = CSng(IFsResults("hlcostotherrattargetconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlcostotherrattargetconv = 0
            End If

            If IFsResults("xfdioutf").RowCount > (Year - FirstYear) * (1) Then
                xfdioutf = CSng(IFsResults("xfdioutf")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                xfdioutf = 0
            End If

            If IFsResults("wpally").RowCount > (Year - FirstYear) * (1) Then
                wpally = CSng(IFsResults("wpally")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpally = 0
            End If

            ReDim POPREFUGEE(sNR)
            If IFsResults("POPREFUGEE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    POPREFUGEE(Dim1) = CSng(IFsResults("POPREFUGEE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("samrrsw").RowCount > (Year - FirstYear) * (1) Then
                samrrsw = CSng(IFsResults("samrrsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                samrrsw = 0
            End If

            If IFsResults("hlproxdrvrrsw").RowCount > (Year - FirstYear) * (1) Then
                hlproxdrvrrsw = CSng(IFsResults("hlproxdrvrrsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlproxdrvrrsw = 0
            End If

            ReDim XWORKREMIT(sNR)
            If IFsResults("XWORKREMIT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XWORKREMIT(Dim1) = CSng(IFsResults("XWORKREMIT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XWORKREMITIN(sNR)
            If IFsResults("XWORKREMITIN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XWORKREMITIN(Dim1) = CSng(IFsResults("XWORKREMITIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XWORKREMITOUT(sNR)
            If IFsResults("XWORKREMITOUT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XWORKREMITOUT(Dim1) = CSng(IFsResults("XWORKREMITOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("renewtoenexsw").RowCount > (Year - FirstYear) * (1) Then
                renewtoenexsw = CSng(IFsResults("renewtoenexsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                renewtoenexsw = 0
            End If

            If IFsResults("elenrenict").RowCount > (Year - FirstYear) * (1) Then
                elenrenict = CSng(IFsResults("elenrenict")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elenrenict = 0
            End If

            ReDim qevrethr(sNR)
            If IFsResults("qevrethr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    qevrethr(Dim1) = CSng(IFsResults("qevrethr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim waterrenewconverge(sNR, 2)
            If IFsResults("waterrenewconverge").RowCount > (Year - FirstYear) * (1 * sNR * 2) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 2
                        waterrenewconverge(Dim1, Dim2) = CSng(if(IFsResults("waterrenewconverge")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim repayrm(sNR)
            If IFsResults("repayrm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    repayrm(Dim1) = CSng(IFsResults("repayrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim HHRETIRENEED(sNR)
            if Year = FirstYear then
                If IFsResults("HHRETIRENEED").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HHRETIRENEED(Dim1) = CSng(IFsResults("HHRETIRENEED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HHSAVNEEDSTOCK(sNR)
            if Year = FirstYear then
                If IFsResults("HHSAVNEEDSTOCK").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HHSAVNEEDSTOCK(Dim1) = CSng(IFsResults("HHSAVNEEDSTOCK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim WATERRESFOSSIL(sNR)
            if Year = FirstYear then
                If IFsResults("WATERRESFOSSIL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WATERRESFOSSIL(Dim1) = CSng(IFsResults("WATERRESFOSSIL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("hlpm2pt5sw").RowCount > (Year - FirstYear) * (1) Then
                hlpm2pt5sw = CSng(IFsResults("hlpm2pt5sw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlpm2pt5sw = 0
            End If

            If IFsResults("envpm25hldsw").RowCount > (Year - FirstYear) * (1) Then
                envpm25hldsw = CSng(IFsResults("envpm25hldsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                envpm25hldsw = 0
            End If

            ReDim envpm2pt5m(sNR)
            If IFsResults("envpm2pt5m").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    envpm2pt5m(Dim1) = CSng(IFsResults("envpm2pt5m")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim resor(sNR, 9)
            If IFsResults("resor").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        resor(Dim1, Dim2) = CSng(if(IFsResults("resor")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim resorm(sNR, 9)
            If IFsResults("resorm").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        resorm(Dim1, Dim2) = CSng(if(IFsResults("resorm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim resoruncon(sNR, 9)
            If IFsResults("resoruncon").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        resoruncon(Dim1, Dim2) = CSng(if(IFsResults("resoruncon")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim resorunconm(sNR, 9)
            If IFsResults("resorunconm").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        resorunconm(Dim1, Dim2) = CSng(if(IFsResults("resorunconm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("wpcontiguity").RowCount > (Year - FirstYear) * (1) Then
                wpcontiguity = CSng(IFsResults("wpcontiguity")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpcontiguity = 0
            End If

            If IFsResults("wpcommun").RowCount > (Year - FirstYear) * (1) Then
                wpcommun = CSng(IFsResults("wpcommun")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpcommun = 0
            End If

            If IFsResults("wpdemdist").RowCount > (Year - FirstYear) * (1) Then
                wpdemdist = CSng(IFsResults("wpdemdist")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpdemdist = 0
            End If

            If IFsResults("wpdemmin").RowCount > (Year - FirstYear) * (1) Then
                wpdemmin = CSng(IFsResults("wpdemmin")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpdemmin = 0
            End If

            ReDim WORKAGERETIREND(sNR)
            if Year = FirstYear then
                If IFsResults("WORKAGERETIREND").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WORKAGERETIREND(Dim1) = CSng(IFsResults("WORKAGERETIREND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HHSAVSURSTOCK(sNR)
            if Year = FirstYear then
                If IFsResults("HHSAVSURSTOCK").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HHSAVSURSTOCK(Dim1) = CSng(IFsResults("HHSAVSURSTOCK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim GOVRISK(sNR)
            if Year = FirstYear then
                If IFsResults("GOVRISK").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVRISK(Dim1) = CSng(IFsResults("GOVRISK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("infraroadpavedcostupper").RowCount > (Year - FirstYear) * (1) Then
                infraroadpavedcostupper = CSng(IFsResults("infraroadpavedcostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraroadpavedcostupper = 0
            End If

            If IFsResults("infraroadpavedcostlower").RowCount > (Year - FirstYear) * (1) Then
                infraroadpavedcostlower = CSng(IFsResults("infraroadpavedcostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraroadpavedcostlower = 0
            End If

            ReDim INFRAROADPAVEDCOST(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAROADPAVEDCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAROADPAVEDCOST(Dim1) = CSng(IFsResults("INFRAROADPAVEDCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim infraroadpavedcostm(sNR)
            If IFsResults("infraroadpavedcostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraroadpavedcostm(Dim1) = CSng(IFsResults("infraroadpavedcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INFRAROADUNPAVEDCOST(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAROADUNPAVEDCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAROADUNPAVEDCOST(Dim1) = CSng(IFsResults("INFRAROADUNPAVEDCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("infraroadunpavedcostupper").RowCount > (Year - FirstYear) * (1) Then
                infraroadunpavedcostupper = CSng(IFsResults("infraroadunpavedcostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraroadunpavedcostupper = 0
            End If

            If IFsResults("infraroadunpavedcostlower").RowCount > (Year - FirstYear) * (1) Then
                infraroadunpavedcostlower = CSng(IFsResults("infraroadunpavedcostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraroadunpavedcostlower = 0
            End If

            ReDim infraroadunpavedcostm(sNR)
            If IFsResults("infraroadunpavedcostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraroadunpavedcostm(Dim1) = CSng(IFsResults("infraroadunpavedcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INFRAROAD(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAROAD").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAROAD(Dim1) = CSng(IFsResults("INFRAROAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim INFRAROADDEM(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAROADDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAROADDEM(Dim1) = CSng(IFsResults("INFRAROADDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim infraroadpavedpcntm(sNR)
            If IFsResults("infraroadpavedpcntm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraroadpavedpcntm(Dim1) = CSng(IFsResults("infraroadpavedpcntm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim infraroadm(sNR)
            If IFsResults("infraroadm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraroadm(Dim1) = CSng(IFsResults("infraroadm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("infraroadpavedlife").RowCount > (Year - FirstYear) * (1) Then
                infraroadpavedlife = CSng(IFsResults("infraroadpavedlife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraroadpavedlife = 0
            End If

            ReDim INFRAROADRAI(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAROADRAI").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAROADRAI(Dim1) = CSng(IFsResults("INFRAROADRAI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim INFRAROADRAIDEM(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAROADRAIDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAROADRAIDEM(Dim1) = CSng(IFsResults("INFRAROADRAIDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("infraroadunpavedlife").RowCount > (Year - FirstYear) * (1) Then
                infraroadunpavedlife = CSng(IFsResults("infraroadunpavedlife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infraroadunpavedlife = 0
            End If

            ReDim infraroadraisetar(sNR)
            If IFsResults("infraroadraisetar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraroadraisetar(Dim1) = CSng(IFsResults("infraroadraisetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim infraroadraitrgtval(sNR)
            If IFsResults("infraroadraitrgtval").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraroadraitrgtval(Dim1) = CSng(IFsResults("infraroadraitrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim infraroadraiseyrtar(sNR)
            If IFsResults("infraroadraiseyrtar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraroadraiseyrtar(Dim1) = CSng(IFsResults("infraroadraiseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim infraroadraitrgtyr(sNR)
            If IFsResults("infraroadraitrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infraroadraitrgtyr(Dim1) = CSng(IFsResults("infraroadraitrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("sanithhlifepitlatrinetrad").RowCount > (Year - FirstYear) * (1) Then
                sanithhlifepitlatrinetrad = CSng(IFsResults("sanithhlifepitlatrinetrad")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                sanithhlifepitlatrinetrad = 0
            End If

            ReDim WATSAFE(sNR, 3)
            If IFsResults("WATSAFE").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        WATSAFE(Dim1, Dim2) = CSng(if(IFsResults("WATSAFE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim watsafetrgtval(sNR)
            If IFsResults("watsafetrgtval").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    watsafetrgtval(Dim1) = CSng(IFsResults("watsafetrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("watsafhldsw").RowCount > (Year - FirstYear) * (1) Then
                watsafhldsw = CSng(IFsResults("watsafhldsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                watsafhldsw = 0
            End If

            ReDim watsafenoconsetar(sNR)
            If IFsResults("watsafenoconsetar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    watsafenoconsetar(Dim1) = CSng(IFsResults("watsafenoconsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim watsafenoconseyrtar(sNR)
            If IFsResults("watsafenoconseyrtar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    watsafenoconseyrtar(Dim1) = CSng(IFsResults("watsafenoconseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim watsafem(sNR, 3)
            If IFsResults("watsafem").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        watsafem(Dim1, Dim2) = CSng(if(IFsResults("watsafem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("samtomalnchpsw").RowCount > (Year - FirstYear) * (1) Then
                samtomalnchpsw = CSng(IFsResults("samtomalnchpsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                samtomalnchpsw = 0
            End If

            ReDim samadmissionadd(sNR)
            If IFsResults("samadmissionadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    samadmissionadd(Dim1) = CSng(IFsResults("samadmissionadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim samadmissionm(sNR)
            If IFsResults("samadmissionm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    samadmissionm(Dim1) = CSng(IFsResults("samadmissionm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("samadmissionsw").RowCount > (Year - FirstYear) * (1) Then
                samadmissionsw = CSng(IFsResults("samadmissionsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                samadmissionsw = 0
            End If

            ReDim samavertedm(sNR)
            If IFsResults("samavertedm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    samavertedm(Dim1) = CSng(IFsResults("samavertedm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SAMADMISSION(sNR)
            If IFsResults("SAMADMISSION").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SAMADMISSION(Dim1) = CSng(IFsResults("SAMADMISSION")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("samavertedconvyr").RowCount > (Year - FirstYear) * (1) Then
                samavertedconvyr = CSng(IFsResults("samavertedconvyr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                samavertedconvyr = 0
            End If

            If IFsResults("malnchpsamtostunt").RowCount > (Year - FirstYear) * (1) Then
                malnchpsamtostunt = CSng(IFsResults("malnchpsamtostunt")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                malnchpsamtostunt = 0
            End If

            ReDim MALNCHPSAMINC(sNR)
            If IFsResults("MALNCHPSAMINC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    MALNCHPSAMINC(Dim1) = CSng(IFsResults("MALNCHPSAMINC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SAMAVERTED(sNR)
            If IFsResults("SAMAVERTED").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SAMAVERTED(Dim1) = CSng(IFsResults("SAMAVERTED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SANITATION(sNR, 3)
            If IFsResults("SANITATION").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        SANITATION(Dim1, Dim2) = CSng(if(IFsResults("SANITATION")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim SANITATIONDEM(sNR, 3)
            If IFsResults("SANITATIONDEM").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        SANITATIONDEM(Dim1, Dim2) = CSng(if(IFsResults("SANITATIONDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim sanitationtrgtval(sNR)
            If IFsResults("sanitationtrgtval").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    sanitationtrgtval(Dim1) = CSng(IFsResults("sanitationtrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SANITATIONCOST(sNR)
            if Year = FirstYear then
                If IFsResults("SANITATIONCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SANITATIONCOST(Dim1) = CSng(IFsResults("SANITATIONCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("sanitationcostupper").RowCount > (Year - FirstYear) * (1) Then
                sanitationcostupper = CSng(IFsResults("sanitationcostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                sanitationcostupper = 0
            End If

            If IFsResults("sanitationcostlower").RowCount > (Year - FirstYear) * (1) Then
                sanitationcostlower = CSng(IFsResults("sanitationcostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                sanitationcostlower = 0
            End If

            ReDim sanitationcostm(sNR)
            If IFsResults("sanitationcostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    sanitationcostm(Dim1) = CSng(IFsResults("sanitationcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim sanitbudgetprotec(sNR)
            If IFsResults("sanitbudgetprotec").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    sanitbudgetprotec(Dim1) = CSng(IFsResults("sanitbudgetprotec")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("sanithldsw").RowCount > (Year - FirstYear) * (1) Then
                sanithldsw = CSng(IFsResults("sanithldsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                sanithldsw = 0
            End If

            If IFsResults("sanitationimpcostlower").RowCount > (Year - FirstYear) * (1) Then
                sanitationimpcostlower = CSng(IFsResults("sanitationimpcostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                sanitationimpcostlower = 0
            End If

            If IFsResults("sanitationimplife").RowCount > (Year - FirstYear) * (1) Then
                sanitationimplife = CSng(IFsResults("sanitationimplife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                sanitationimplife = 0
            End If

            If IFsResults("sanitationimpcostupper").RowCount > (Year - FirstYear) * (1) Then
                sanitationimpcostupper = CSng(IFsResults("sanitationimpcostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                sanitationimpcostupper = 0
            End If

            ReDim sanitationimpcostm(sNR)
            If IFsResults("sanitationimpcostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    sanitationimpcostm(Dim1) = CSng(IFsResults("sanitationimpcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("sanitationlife").RowCount > (Year - FirstYear) * (1) Then
                sanitationlife = CSng(IFsResults("sanitationlife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                sanitationlife = 0
            End If

            If IFsResults("sanithhmodel2sw").RowCount > (Year - FirstYear) * (1) Then
                sanithhmodel2sw = CSng(IFsResults("sanithhmodel2sw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                sanithhmodel2sw = 0
            End If

            ReDim sanitnoconsetar(sNR)
            If IFsResults("sanitnoconsetar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    sanitnoconsetar(Dim1) = CSng(IFsResults("sanitnoconsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim sanitnoconseyrtar(sNR)
            If IFsResults("sanitnoconseyrtar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    sanitnoconseyrtar(Dim1) = CSng(IFsResults("sanitnoconseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim sanithhm(sNR, 5, 3)
            If IFsResults("sanithhm").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            sanithhm(Dim1, Dim2, Dim3) = CSng(if(IFsResults("sanithhm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim sanithhbasictrgtval(sNR, 3)
            If IFsResults("sanithhbasictrgtval").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        sanithhbasictrgtval(Dim1, Dim2) = CSng(if(IFsResults("sanithhbasictrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim sanithhbasictrgtyr(sNR, 3)
            If IFsResults("sanithhbasictrgtyr").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        sanithhbasictrgtyr(Dim1, Dim2) = CSng(if(IFsResults("sanithhbasictrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim sanithhsafetrgtyr(sNR, 3)
            If IFsResults("sanithhsafetrgtyr").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        sanithhsafetrgtyr(Dim1, Dim2) = CSng(if(IFsResults("sanithhsafetrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim sanithhsafetrgtval(sNR, 3)
            If IFsResults("sanithhsafetrgtval").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        sanithhsafetrgtval(Dim1, Dim2) = CSng(if(IFsResults("sanithhsafetrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim sanithhcostlowershr(sNR, 5, 3)
            If IFsResults("sanithhcostlowershr").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            sanithhcostlowershr(Dim1, Dim2, Dim3) = CSng(if(IFsResults("sanithhcostlowershr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim SANITHHCOSTHIGHER(sNR, 5, 3)
            If IFsResults("SANITHHCOSTHIGHER").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            SANITHHCOSTHIGHER(Dim1, Dim2, Dim3) = CSng(if(IFsResults("SANITHHCOSTHIGHER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim SANITHHCOSTLOWER(sNR, 5, 3)
            If IFsResults("SANITHHCOSTLOWER").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            SANITHHCOSTLOWER(Dim1, Dim2, Dim3) = CSng(if(IFsResults("SANITHHCOSTLOWER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim SANITHHINVESTCAP(sNR, 5, 3)
            if Year = FirstYear then
                If IFsResults("SANITHHINVESTCAP").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 5
                            For Dim3 = 1 to 3
                                SANITHHINVESTCAP(Dim1, Dim2, Dim3) = CSng(if(IFsResults("SANITHHINVESTCAP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim SANITHHINVESTMAINT(sNR, 5, 3)
            if Year = FirstYear then
                If IFsResults("SANITHHINVESTMAINT").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 5
                            For Dim3 = 1 to 3
                                SANITHHINVESTMAINT(Dim1, Dim2, Dim3) = CSng(if(IFsResults("SANITHHINVESTMAINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim SANITHHPOP(sNR, 5, 3)
            If IFsResults("SANITHHPOP").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            SANITHHPOP(Dim1, Dim2, Dim3) = CSng(if(IFsResults("SANITHHPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim SANITHHDEMPOP(sNR, 5, 3)
            If IFsResults("SANITHHDEMPOP").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            SANITHHDEMPOP(Dim1, Dim2, Dim3) = CSng(if(IFsResults("SANITHHDEMPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim SANITHH(sNR, 5, 3)
            If IFsResults("SANITHH").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            SANITHH(Dim1, Dim2, Dim3) = CSng(if(IFsResults("SANITHH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim SANITHHDEM(sNR, 5, 3)
            If IFsResults("SANITHHDEM").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            SANITHHDEM(Dim1, Dim2, Dim3) = CSng(if(IFsResults("SANITHHDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim sanitationexovals(sNR, 3)
            If IFsResults("sanitationexovals").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        sanitationexovals(Dim1, Dim2) = CSng(if(IFsResults("sanitationexovals")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim sanitationm(sNR, 3)
            If IFsResults("sanitationm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        sanitationm(Dim1, Dim2) = CSng(if(IFsResults("sanitationm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim SANITATIONIMPCOST(sNR)
            if Year = FirstYear then
                If IFsResults("SANITATIONIMPCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SANITATIONIMPCOST(Dim1) = CSng(IFsResults("SANITATIONIMPCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim savingsdiscountrt(sNR)
            If IFsResults("savingsdiscountrt").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    savingsdiscountrt(Dim1) = CSng(IFsResults("savingsdiscountrt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim cpctnduse(sNR)
            If IFsResults("cpctnduse").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    cpctnduse(Dim1) = CSng(IFsResults("cpctnduse")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim EDYRSSLE(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDYRSSLE").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDYRSSLE(Dim1, Dim2) = CSng(if(IFsResults("EDYRSSLE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim edqualsecmathm(sNR, 3)
            If IFsResults("edqualsecmathm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edqualsecmathm(Dim1, Dim2) = CSng(if(IFsResults("edqualsecmathm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edqualsecallm(sNR, 3)
            If IFsResults("edqualsecallm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edqualsecallm(Dim1, Dim2) = CSng(if(IFsResults("edqualsecallm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edqualsecreadm(sNR, 3)
            If IFsResults("edqualsecreadm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edqualsecreadm(Dim1, Dim2) = CSng(if(IFsResults("edqualsecreadm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edqualsecscim(sNR, 3)
            If IFsResults("edqualsecscim").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edqualsecscim(Dim1, Dim2) = CSng(if(IFsResults("edqualsecscim")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDEXPERSEC(sNR)
            If IFsResults("EDEXPERSEC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    EDEXPERSEC(Dim1) = CSng(IFsResults("EDEXPERSEC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim EDQUALSECALL(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDQUALSECALL").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDQUALSECALL(Dim1, Dim2) = CSng(if(IFsResults("EDQUALSECALL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDQUALSECMATH(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDQUALSECMATH").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDQUALSECMATH(Dim1, Dim2) = CSng(if(IFsResults("EDQUALSECMATH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDQUALSECREAD(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDQUALSECREAD").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDQUALSECREAD(Dim1, Dim2) = CSng(if(IFsResults("EDQUALSECREAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDQUALSECSCI(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDQUALSECSCI").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDQUALSECSCI(Dim1, Dim2) = CSng(if(IFsResults("EDQUALSECSCI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDSECTOT(sNR, 3)
            If IFsResults("EDSECTOT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECTOT(Dim1, Dim2) = CSng(if(IFsResults("EDSECTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("elinst2").RowCount > (Year - FirstYear) * (1) Then
                elinst2 = CSng(IFsResults("elinst2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elinst2 = 0
            End If

            If IFsResults("elinst1").RowCount > (Year - FirstYear) * (1) Then
                elinst1 = CSng(IFsResults("elinst1")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elinst1 = 0
            End If

            If IFsResults("sanithhlifesst").RowCount > (Year - FirstYear) * (1) Then
                sanithhlifesst = CSng(IFsResults("sanithhlifesst")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                sanithhlifesst = 0
            End If

            ReDim inputservm(sNR, 6)
            If IFsResults("inputservm").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        inputservm(Dim1, Dim2) = CSng(if(IFsResults("inputservm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim XBILATSER(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("XBILATSER").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            XBILATSER(Dim1, Dim2) = CSng(if(IFsResults("XBILATSER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim MALNCHPSAM(sNR)
            If IFsResults("MALNCHPSAM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    MALNCHPSAM(Dim1) = CSng(IFsResults("MALNCHPSAM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("malnchpsamadj").RowCount > (Year - FirstYear) * (1) Then
                malnchpsamadj = CSng(IFsResults("malnchpsamadj")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                malnchpsamadj = 0
            End If

            ReDim malnchpsamm(sNR)
            If IFsResults("malnchpsamm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    malnchpsamm(Dim1) = CSng(IFsResults("malnchpsamm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("gdpshadowcoeffcorrupt").RowCount > (Year - FirstYear) * (1) Then
                gdpshadowcoeffcorrupt = CSng(IFsResults("gdpshadowcoeffcorrupt")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                gdpshadowcoeffcorrupt = 0
            End If

            If IFsResults("gdpshadowcoefflabinf").RowCount > (Year - FirstYear) * (1) Then
                gdpshadowcoefflabinf = CSng(IFsResults("gdpshadowcoefflabinf")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                gdpshadowcoefflabinf = 0
            End If

            ReDim gdpshadowshrm(sNR)
            If IFsResults("gdpshadowshrm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    gdpshadowshrm(Dim1) = CSng(IFsResults("gdpshadowshrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("gdpshadowon").RowCount > (Year - FirstYear) * (1) Then
                gdpshadowon = CSng(IFsResults("gdpshadowon")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                gdpshadowon = 0
            End If

            ReDim MARRIEDFEMSHR(sNR, 2)
            If IFsResults("MARRIEDFEMSHR").RowCount > (Year - FirstYear) * (1 * sNR * 2) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 2
                        MARRIEDFEMSHR(Dim1, Dim2) = CSng(if(IFsResults("MARRIEDFEMSHR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim marriedfemshrm(sNR, 2)
            If IFsResults("marriedfemshrm").RowCount > (Year - FirstYear) * (1 * sNR * 2) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 2
                        marriedfemshrm(Dim1, Dim2) = CSng(if(IFsResults("marriedfemshrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDTERGRSCIEN(sNR)
            if Year = FirstYear then
                If IFsResults("EDTERGRSCIEN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        EDTERGRSCIEN(Dim1) = CSng(IFsResults("EDTERGRSCIEN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim enresunce(sNR)
            If IFsResults("enresunce").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    enresunce(Dim1) = CSng(IFsResults("enresunce")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim CSHHS(sNR, 6)
            if Year = FirstYear then
                If IFsResults("CSHHS").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            CSHHS(Dim1, Dim2) = CSng(if(IFsResults("CSHHS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HLSMOKINGIMP(sNR, 5, 3)
            If IFsResults("HLSMOKINGIMP").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            HLSMOKINGIMP(Dim1, Dim2, Dim3) = CSng(if(IFsResults("HLSMOKINGIMP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("hlsmimpsw").RowCount > (Year - FirstYear) * (1) Then
                hlsmimpsw = CSng(IFsResults("hlsmimpsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlsmimpsw = 0
            End If

            If IFsResults("hlsmokingdecm").RowCount > (Year - FirstYear) * (1) Then
                hlsmokingdecm = CSng(IFsResults("hlsmokingdecm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlsmokingdecm = 0
            End If

            If IFsResults("hlsmokingincm").RowCount > (Year - FirstYear) * (1) Then
                hlsmokingincm = CSng(IFsResults("hlsmokingincm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlsmokingincm = 0
            End If

            ReDim hlsmokingceiling(3)
            If IFsResults("hlsmokingceiling").RowCount > (Year - FirstYear) * (1 * 3) Then
                For Dim1 = 1 to 3
                    hlsmokingceiling(Dim1) = CSng(IFsResults("hlsmokingceiling")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)))
                Next Dim1
            End If

            ReDim hlsmokingfloor(3)
            If IFsResults("hlsmokingfloor").RowCount > (Year - FirstYear) * (1 * 3) Then
                For Dim1 = 1 to 3
                    hlsmokingfloor(Dim1) = CSng(IFsResults("hlsmokingfloor")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)))
                Next Dim1
            End If

            If IFsResults("hlsmkimeff").RowCount > (Year - FirstYear) * (1) Then
                hlsmkimeff = CSng(IFsResults("hlsmkimeff")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlsmkimeff = 0
            End If

            If IFsResults("hlsmkel").RowCount > (Year - FirstYear) * (1) Then
                hlsmkel = CSng(IFsResults("hlsmkel")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlsmkel = 0
            End If

            If IFsResults("hlsmokingpeakyr").RowCount > (Year - FirstYear) * (1) Then
                hlsmokingpeakyr = CSng(IFsResults("hlsmokingpeakyr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlsmokingpeakyr = 0
            End If

            ReDim hlsmokingm(sNR, 3)
            If IFsResults("hlsmokingm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        hlsmokingm(Dim1, Dim2) = CSng(if(IFsResults("hlsmokingm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("hlsmokingstsw").RowCount > (Year - FirstYear) * (1) Then
                hlsmokingstsw = CSng(IFsResults("hlsmokingstsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlsmokingstsw = 0
            End If

            ReDim SSWELTAXR(sNR)
            if Year = FirstYear then
                If IFsResults("SSWELTAXR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SSWELTAXR(Dim1) = CSng(IFsResults("SSWELTAXR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ssweltaxrm(sNR)
            If IFsResults("ssweltaxrm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ssweltaxrm(Dim1) = CSng(IFsResults("ssweltaxrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SSWELTAX(sNR)
            if Year = FirstYear then
                If IFsResults("SSWELTAX").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SSWELTAX(Dim1) = CSng(IFsResults("SSWELTAX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("edqualsecur").RowCount > (Year - FirstYear) * (1) Then
                edqualsecur = CSng(IFsResults("edqualsecur")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edqualsecur = 0
            End If

            ReDim svdthsadd(sNR, 6)
            If IFsResults("svdthsadd").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        svdthsadd(Dim1, Dim2) = CSng(if(IFsResults("svdthsadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim SOCIETALVIOLENCEMEASURE(sNR)
            If IFsResults("SOCIETALVIOLENCEMEASURE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SOCIETALVIOLENCEMEASURE(Dim1) = CSng(IFsResults("SOCIETALVIOLENCEMEASURE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("svvionormsw").RowCount > (Year - FirstYear) * (1) Then
                svvionormsw = CSng(IFsResults("svvionormsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                svvionormsw = 0
            End If

            If IFsResults("svtohlsw").RowCount > (Year - FirstYear) * (1) Then
                svtohlsw = CSng(IFsResults("svtohlsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                svtohlsw = 0
            End If

            ReDim svindexm(sNR, 6)
            If IFsResults("svindexm").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        svindexm(Dim1, Dim2) = CSng(if(IFsResults("svindexm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim svmulm(sNR, 6)
            If IFsResults("svmulm").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        svmulm(Dim1, Dim2) = CSng(if(IFsResults("svmulm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim ENSOLFUEL(sNR)
            if Year = FirstYear then
                If IFsResults("ENSOLFUEL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ENSOLFUEL(Dim1) = CSng(IFsResults("ENSOLFUEL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("hlsolfuelsw").RowCount > (Year - FirstYear) * (1) Then
                hlsolfuelsw = CSng(IFsResults("hlsolfuelsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlsolfuelsw = 0
            End If

            ReDim ensolfueltrgtyr(sNR)
            If IFsResults("ensolfueltrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ensolfueltrgtyr(Dim1) = CSng(IFsResults("ensolfueltrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ensolfueltrgtval(sNR)
            If IFsResults("ensolfueltrgtval").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ensolfueltrgtval(Dim1) = CSng(IFsResults("ensolfueltrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("ensolflhldsw").RowCount > (Year - FirstYear) * (1) Then
                ensolflhldsw = CSng(IFsResults("ensolflhldsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ensolflhldsw = 0
            End If

            ReDim ensolfuelsetar(sNR)
            If IFsResults("ensolfuelsetar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ensolfuelsetar(Dim1) = CSng(IFsResults("ensolfuelsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ensolfuelseyrtar(sNR)
            If IFsResults("ensolfuelseyrtar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ensolfuelseyrtar(Dim1) = CSng(IFsResults("ensolfuelseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ensolfuelm(sNR)
            If IFsResults("ensolfuelm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ensolfuelm(Dim1) = CSng(IFsResults("ensolfuelm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("edqualprispndimpthresholdval").RowCount > (Year - FirstYear) * (1) Then
                edqualprispndimpthresholdval = CSng(IFsResults("edqualprispndimpthresholdval")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edqualprispndimpthresholdval = 0
            End If

            If IFsResults("edqualsecspndimpthresholdval").RowCount > (Year - FirstYear) * (1) Then
                edqualsecspndimpthresholdval = CSng(IFsResults("edqualsecspndimpthresholdval")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edqualsecspndimpthresholdval = 0
            End If

            If IFsResults("edqualprispndimpthreshold").RowCount > (Year - FirstYear) * (1) Then
                edqualprispndimpthreshold = CSng(IFsResults("edqualprispndimpthreshold")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edqualprispndimpthreshold = 0
            End If

            If IFsResults("edqualsecspndimpthreshold").RowCount > (Year - FirstYear) * (1) Then
                edqualsecspndimpthreshold = CSng(IFsResults("edqualsecspndimpthreshold")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                edqualsecspndimpthreshold = 0
            End If

            ReDim hllongvtystartage(15)
            If IFsResults("hllongvtystartage").RowCount > (Year - FirstYear) * (1 * 15) Then
                For Dim1 = 1 to 15
                    hllongvtystartage(Dim1) = CSng(IFsResults("hllongvtystartage")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 15)))
                Next Dim1
            End If

            ReDim SDEATH(sNR)
            if Year = FirstYear then
                If IFsResults("SDEATH").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SDEATH(Dim1) = CSng(IFsResults("SDEATH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim SFCIVILWARUCDP(sNR)
            If IFsResults("SFCIVILWARUCDP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SFCIVILWARUCDP(Dim1) = CSng(IFsResults("SFCIVILWARUCDP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim sfmassrep(sNR)
            If IFsResults("sfmassrep").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    sfmassrep(Dim1) = CSng(IFsResults("sfmassrep")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SFDEM(sNR)
            if Year = FirstYear then
                If IFsResults("SFDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SFDEM(Dim1) = CSng(IFsResults("SFDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim SFECONDEV(sNR)
            if Year = FirstYear then
                If IFsResults("SFECONDEV").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SFECONDEV(Dim1) = CSng(IFsResults("SFECONDEV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim SFGOV(sNR)
            if Year = FirstYear then
                If IFsResults("SFGOV").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SFGOV(Dim1) = CSng(IFsResults("SFGOV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim SFIMBAL(sNR)
            if Year = FirstYear then
                If IFsResults("SFIMBAL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SFIMBAL(Dim1) = CSng(IFsResults("SFIMBAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim SFINSTABALL(sNR)
            If IFsResults("SFINSTABALL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SFINSTABALL(Dim1) = CSng(IFsResults("SFINSTABALL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SFINSTABMAG(sNR)
            If IFsResults("SFINSTABMAG").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SFINSTABMAG(Dim1) = CSng(IFsResults("SFINSTABMAG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SFINTLWARALL(sNR)
            If IFsResults("SFINTLWARALL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SFINTLWARALL(Dim1) = CSng(IFsResults("SFINTLWARALL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SFINTLWARMAG(sNR)
            If IFsResults("SFINTLWARMAG").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SFINTLWARMAG(Dim1) = CSng(IFsResults("SFINTLWARMAG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("sfusehist").RowCount > (Year - FirstYear) * (1) Then
                sfusehist = CSng(IFsResults("sfusehist")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                sfusehist = 0
            End If

            If IFsResults("sfconv").RowCount > (Year - FirstYear) * (1) Then
                sfconv = CSng(IFsResults("sfconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                sfconv = 0
            End If

            ReDim sfintlwarmagm(sNR)
            If IFsResults("sfintlwarmagm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    sfintlwarmagm(Dim1) = CSng(IFsResults("sfintlwarmagm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("sfcivilwarucdpsw").RowCount > (Year - FirstYear) * (1) Then
                sfcivilwarucdpsw = CSng(IFsResults("sfcivilwarucdpsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                sfcivilwarucdpsw = 0
            End If

            ReDim sfintlwaradd(sNR)
            If IFsResults("sfintlwaradd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    sfintlwaradd(Dim1) = CSng(IFsResults("sfintlwaradd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ST(sNR, 6)
            if Year = FirstYear then
                If IFsResults("ST").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            ST(Dim1, Dim2) = CSng(if(IFsResults("ST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDSECLOWRVOC(sNR, 3)
            If IFsResults("EDSECLOWRVOC").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECLOWRVOC(Dim1, Dim2) = CSng(if(IFsResults("EDSECLOWRVOC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDSECUPPRVOC(sNR, 3)
            If IFsResults("EDSECUPPRVOC").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECUPPRVOC(Dim1, Dim2) = CSng(if(IFsResults("EDSECUPPRVOC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim HLSTUNT(sNR)
            if Year = FirstYear then
                If IFsResults("HLSTUNT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLSTUNT(Dim1) = CSng(IFsResults("HLSTUNT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLSTUNTCHL(sNR)
            if Year = FirstYear then
                If IFsResults("HLSTUNTCHL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLSTUNTCHL(Dim1) = CSng(IFsResults("HLSTUNTCHL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLSTUNTWORK(sNR)
            if Year = FirstYear then
                If IFsResults("HLSTUNTWORK").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLSTUNTWORK(Dim1) = CSng(IFsResults("HLSTUNTWORK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim hlstuntchlm(sNR)
            If IFsResults("hlstuntchlm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hlstuntchlm(Dim1) = CSng(IFsResults("hlstuntchlm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("hlstuntyrs").RowCount > (Year - FirstYear) * (1) Then
                hlstuntyrs = CSng(IFsResults("hlstuntyrs")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlstuntyrs = 0
            End If

            ReDim XWBSUBF(sNR)
            if Year = FirstYear then
                If IFsResults("XWBSUBF").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        XWBSUBF(Dim1) = CSng(IFsResults("XWBSUBF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim SURVSE(sNR, 7)
            If IFsResults("SURVSE").RowCount > (Year - FirstYear) * (1 * sNR * 7) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 7
                        SURVSE(Dim1, Dim2) = CSng(if(IFsResults("SURVSE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim SURVSELD(sNR)
            If IFsResults("SURVSELD").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SURVSELD(Dim1) = CSng(IFsResults("SURVSELD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim EDPRISUR(sNR, 3)
            If IFsResults("EDPRISUR").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDPRISUR(Dim1, Dim2) = CSng(if(IFsResults("EDPRISUR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim survseadd(sNR)
            If IFsResults("survseadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    survseadd(Dim1) = CSng(IFsResults("survseadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SWINGSTS(9, sNR)
            If IFsResults("SWINGSTS").RowCount > (Year - FirstYear) * (1 * 9 * sNR) Then
                For Dim1 = 1 to 9
                    For Dim2 = 1 to sNR
                        SWINGSTS(Dim1, Dim2) = CSng(if(IFsResults("SWINGSTS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9, Dim2, sNR)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("swseffmax").RowCount > (Year - FirstYear) * (1) Then
                swseffmax = CSng(IFsResults("swseffmax")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                swseffmax = 0
            End If

            If IFsResults("swseffects").RowCount > (Year - FirstYear) * (1) Then
                swseffects = CSng(IFsResults("swseffects")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                swseffects = 0
            End If

            If IFsResults("wpgreatlev").RowCount > (Year - FirstYear) * (1) Then
                wpgreatlev = CSng(IFsResults("wpgreatlev")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpgreatlev = 0
            End If

            If IFsResults("wpgreatthresh").RowCount > (Year - FirstYear) * (1) Then
                wpgreatthresh = CSng(IFsResults("wpgreatthresh")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpgreatthresh = 0
            End If

            If IFsResults("hlmorbm").RowCount > (Year - FirstYear) * (1) Then
                hlmorbm = CSng(IFsResults("hlmorbm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlmorbm = 0
            End If

            If IFsResults("dyadtarifffundsw").RowCount > (Year - FirstYear) * (1) Then
                dyadtarifffundsw = CSng(IFsResults("dyadtarifffundsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                dyadtarifffundsw = 0
            End If

            ReDim XTARIFFREV(sNR, 6)
            if Year = FirstYear then
                If IFsResults("XTARIFFREV").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            XTARIFFREV(Dim1, Dim2) = CSng(if(IFsResults("XTARIFFREV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim XTARIFFTAXR(sNR, 6)
            if Year = FirstYear then
                If IFsResults("XTARIFFTAXR").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            XTARIFFTAXR(Dim1, Dim2) = CSng(if(IFsResults("XTARIFFTAXR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim TARIFFRATEAGR(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("TARIFFRATEAGR").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            TARIFFRATEAGR(Dim1, Dim2) = CSng(if(IFsResults("TARIFFRATEAGR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim TARIFFRATEENE(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("TARIFFRATEENE").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            TARIFFRATEENE(Dim1, Dim2) = CSng(if(IFsResults("TARIFFRATEENE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim TARIFFRATEICT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("TARIFFRATEICT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            TARIFFRATEICT(Dim1, Dim2) = CSng(if(IFsResults("TARIFFRATEICT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim TARIFFRATEMANU(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("TARIFFRATEMANU").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            TARIFFRATEMANU(Dim1, Dim2) = CSng(if(IFsResults("TARIFFRATEMANU")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim TARIFFRATEMAT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("TARIFFRATEMAT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            TARIFFRATEMAT(Dim1, Dim2) = CSng(if(IFsResults("TARIFFRATEMAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim TARIFFRATESER(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("TARIFFRATESER").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            TARIFFRATESER(Dim1, Dim2) = CSng(if(IFsResults("TARIFFRATESER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim TAXRA(sNR)
            if Year = FirstYear then
                If IFsResults("TAXRA").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        TAXRA(Dim1) = CSng(IFsResults("TAXRA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim taxinfadjm(sNR)
            If IFsResults("taxinfadjm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    taxinfadjm(Dim1) = CSng(IFsResults("taxinfadjm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim etechadv(sNR, 9)
            If IFsResults("etechadv").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        etechadv(Dim1, Dim2) = CSng(if(IFsResults("etechadv")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim etechadvuncon(9)
            If IFsResults("etechadvuncon").RowCount > (Year - FirstYear) * (1 * 9) Then
                For Dim1 = 1 to 9
                    etechadvuncon(Dim1) = CSng(IFsResults("etechadvuncon")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9)))
                Next Dim1
            End If

            ReDim labcoeffsm(sNR, 6, 3)
            If IFsResults("labcoeffsm").RowCount > (Year - FirstYear) * (1 * sNR * 6 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        For Dim3 = 1 to 3
                            labcoeffsm(Dim1, Dim2, Dim3) = CSng(if(IFsResults("labcoeffsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim techconv(4)
            If IFsResults("techconv").RowCount > (Year - FirstYear) * (1 * 4) Then
                For Dim1 = 1 to 4
                    techconv(Dim1) = CSng(IFsResults("techconv")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)))
                Next Dim1
            End If

            ReDim INFRATELECOST(sNR)
            if Year = FirstYear then
                If IFsResults("INFRATELECOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRATELECOST(Dim1) = CSng(IFsResults("INFRATELECOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("infratelecostupper").RowCount > (Year - FirstYear) * (1) Then
                infratelecostupper = CSng(IFsResults("infratelecostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infratelecostupper = 0
            End If

            If IFsResults("infratelecostlower").RowCount > (Year - FirstYear) * (1) Then
                infratelecostlower = CSng(IFsResults("infratelecostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infratelecostlower = 0
            End If

            ReDim infratelecostm(sNR)
            If IFsResults("infratelecostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infratelecostm(Dim1) = CSng(IFsResults("infratelecostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("infratelelife").RowCount > (Year - FirstYear) * (1) Then
                infratelelife = CSng(IFsResults("infratelelife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infratelelife = 0
            End If

            ReDim INFRATELE(sNR)
            If IFsResults("INFRATELE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INFRATELE(Dim1) = CSng(IFsResults("INFRATELE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("infrateledtfp").RowCount > (Year - FirstYear) * (1) Then
                infrateledtfp = CSng(IFsResults("infrateledtfp")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                infrateledtfp = 0
            End If

            ReDim INFRATELEDEM(sNR)
            If IFsResults("INFRATELEDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INFRATELEDEM(Dim1) = CSng(IFsResults("INFRATELEDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim infratelem(sNR)
            If IFsResults("infratelem").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    infratelem(Dim1) = CSng(IFsResults("infratelem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim INFRATELEOLD(sNR)
            If IFsResults("INFRATELEOLD").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    INFRATELEOLD(Dim1) = CSng(IFsResults("INFRATELEOLD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("envtemptoginidomel").RowCount > (Year - FirstYear) * (1) Then
                envtemptoginidomel = CSng(IFsResults("envtemptoginidomel")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                envtemptoginidomel = 0
            End If

            If IFsResults("envtemptoginidomsw").RowCount > (Year - FirstYear) * (1) Then
                envtemptoginidomsw = CSng(IFsResults("envtemptoginidomsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                envtemptoginidomsw = 0
            End If

            if Year = FirstYear then
                If IFsResults("TERMTR").RowCount > (Year - FirstYear) * (1) Then
                    TERMTR = CSng(IFsResults("TERMTR")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    TERMTR = 0
                End If
            Else
                TERMTR = 0
            End if

            If IFsResults("termtrm").RowCount > (Year - FirstYear) * (1) Then
                termtrm = CSng(IFsResults("termtrm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                termtrm = 0
            End If

            ReDim terdispute(sNR, sNR)
            If IFsResults("terdispute").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to sNR
                        terdispute(Dim1, Dim2) = CSng(if(IFsResults("terdispute")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDEXPERTER(sNR)
            If IFsResults("EDEXPERTER").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    EDEXPERTER(Dim1) = CSng(IFsResults("EDEXPERTER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edtergradgr(sNR, 3)
            If IFsResults("edtergradgr").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edtergradgr(Dim1, Dim2) = CSng(if(IFsResults("edtergradgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edtergndreqgrad(sNR)
            If IFsResults("edtergndreqgrad").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edtergndreqgrad(Dim1) = CSng(IFsResults("edtergndreqgrad")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edtergradtrgtyr(sNR)
            If IFsResults("edtergradtrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edtergradtrgtyr(Dim1) = CSng(IFsResults("edtergradtrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edtergradm(sNR, 3)
            If IFsResults("edtergradm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edtergradm(Dim1, Dim2) = CSng(if(IFsResults("edtergradm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edterintgr(sNR, 3)
            If IFsResults("edterintgr").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edterintgr(Dim1, Dim2) = CSng(if(IFsResults("edterintgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edtergndreqint(sNR)
            If IFsResults("edtergndreqint").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edtergndreqint(Dim1) = CSng(IFsResults("edtergndreqint")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edterintm(sNR, 3)
            If IFsResults("edterintm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edterintm(Dim1, Dim2) = CSng(if(IFsResults("edterintm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edterinttrgtyr(sNR)
            If IFsResults("edterinttrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edterinttrgtyr(Dim1) = CSng(IFsResults("edterinttrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edterscienshradd(sNR)
            If IFsResults("edterscienshradd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edterscienshradd(Dim1) = CSng(IFsResults("edterscienshradd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim EDTERTOT(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDTERTOT").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDTERTOT(Dim1, Dim2) = CSng(if(IFsResults("EDTERTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim reac(sNR, sNR)
            If IFsResults("reac").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to sNR
                        reac(Dim1, Dim2) = CSng(if(IFsResults("reac")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim THREAT(sNR, sNR)
            If IFsResults("THREAT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to sNR
                        THREAT(Dim1, Dim2) = CSng(if(IFsResults("THREAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim THREATREC(sNR)
            If IFsResults("THREATREC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    THREATREC(Dim1) = CSng(IFsResults("THREATREC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim THREATSENT(sNR)
            If IFsResults("THREATSENT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    THREATSENT(Dim1) = CSng(IFsResults("THREATSENT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("wpthrconv").RowCount > (Year - FirstYear) * (1) Then
                wpthrconv = CSng(IFsResults("wpthrconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wpthrconv = 0
            End If

            If IFsResults("envclimsens").RowCount > (Year - FirstYear) * (1) Then
                envclimsens = CSng(IFsResults("envclimsens")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                envclimsens = 0
            End If

            If IFsResults("hlsmokingtcsel").RowCount > (Year - FirstYear) * (1) Then
                hlsmokingtcsel = CSng(IFsResults("hlsmokingtcsel")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlsmokingtcsel = 0
            End If

            ReDim hlsmokingtcs(sNR)
            If IFsResults("hlsmokingtcs").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hlsmokingtcs(Dim1) = CSng(IFsResults("hlsmokingtcs")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim hlsmokingtcsm(sNR)
            If IFsResults("hlsmokingtcsm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hlsmokingtcsm(Dim1) = CSng(IFsResults("hlsmokingtcsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim EDTOTCOST(sNR)
            if Year = FirstYear then
                If IFsResults("EDTOTCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        EDTOTCOST(Dim1) = CSng(IFsResults("EDTOTCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("elasit").RowCount > (Year - FirstYear) * (1) Then
                elasit = CSng(IFsResults("elasit")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                elasit = 0
            End If

            ReDim TEFF(sNR, 6)
            if Year = FirstYear then
                If IFsResults("TEFF").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            TEFF(Dim1, Dim2) = CSng(if(IFsResults("TEFF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim TFR(sNR)
            If IFsResults("TFR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    TFR(Dim1) = CSng(IFsResults("TFR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("tfrconv").RowCount > (Year - FirstYear) * (1) Then
                tfrconv = CSng(IFsResults("tfrconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                tfrconv = 0
            End If

            ReDim tfrmin(sNR)
            If IFsResults("tfrmin").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    tfrmin(Dim1) = CSng(IFsResults("tfrmin")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("eltfrcon").RowCount > (Year - FirstYear) * (1) Then
                eltfrcon = CSng(IFsResults("eltfrcon")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                eltfrcon = 0
            End If

            ReDim tfrexovals(sNR)
            If IFsResults("tfrexovals").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    tfrexovals(Dim1) = CSng(IFsResults("tfrexovals")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim tfrm(sNR)
            If IFsResults("tfrm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    tfrm(Dim1) = CSng(IFsResults("tfrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("ofscth").RowCount > (Year - FirstYear) * (1) Then
                ofscth = CSng(IFsResults("ofscth")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ofscth = 0
            End If

            ReDim GOVCON(sNR)
            if Year = FirstYear then
                If IFsResults("GOVCON").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVCON(Dim1) = CSng(IFsResults("GOVCON")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLCOST(sNR)
            if Year = FirstYear then
                If IFsResults("HLCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLCOST(Dim1) = CSng(IFsResults("HLCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim HLEXPEND(sNR)
            if Year = FirstYear then
                If IFsResults("HLEXPEND").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        HLEXPEND(Dim1) = CSng(IFsResults("HLEXPEND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim GOVHHTRN(sNR)
            if Year = FirstYear then
                If IFsResults("GOVHHTRN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        GOVHHTRN(Dim1) = CSng(IFsResults("GOVHHTRN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim WATRESTOTALRENEW(sNR, 2)
            If IFsResults("WATRESTOTALRENEW").RowCount > (Year - FirstYear) * (1 * sNR * 2) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 2
                        WATRESTOTALRENEW(Dim1, Dim2) = CSng(if(IFsResults("WATRESTOTALRENEW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim INFRAROADTOTAL(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAROADTOTAL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAROADTOTAL(Dim1) = CSng(IFsResults("INFRAROADTOTAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim WATERTOTALSUPPLY(sNR)
            if Year = FirstYear then
                If IFsResults("WATERTOTALSUPPLY").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WATERTOTALSUPPLY(Dim1) = CSng(IFsResults("WATERTOTALSUPPLY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim TRADEAGREEMENTINDEXBILAT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("TRADEAGREEMENTINDEXBILAT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            TRADEAGREEMENTINDEXBILAT(Dim1, Dim2) = CSng(if(IFsResults("TRADEAGREEMENTINDEXBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim TRADEBAL(sNR)
            if Year = FirstYear then
                If IFsResults("TRADEBAL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        TRADEBAL(Dim1) = CSng(IFsResults("TRADEBAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim TRADECOMPLEMENTAGR(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("TRADECOMPLEMENTAGR").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            TRADECOMPLEMENTAGR(Dim1, Dim2) = CSng(if(IFsResults("TRADECOMPLEMENTAGR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim TRADECOMPLEMENTENE(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("TRADECOMPLEMENTENE").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            TRADECOMPLEMENTENE(Dim1, Dim2) = CSng(if(IFsResults("TRADECOMPLEMENTENE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim TRADECOMPLEMENTICT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("TRADECOMPLEMENTICT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            TRADECOMPLEMENTICT(Dim1, Dim2) = CSng(if(IFsResults("TRADECOMPLEMENTICT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim TRADECOMPLEMENTMANU(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("TRADECOMPLEMENTMANU").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            TRADECOMPLEMENTMANU(Dim1, Dim2) = CSng(if(IFsResults("TRADECOMPLEMENTMANU")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim TRADECOMPLEMENTMAT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("TRADECOMPLEMENTMAT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            TRADECOMPLEMENTMAT(Dim1, Dim2) = CSng(if(IFsResults("TRADECOMPLEMENTMAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim TRADECOMPLEMENTSER(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("TRADECOMPLEMENTSER").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            TRADECOMPLEMENTSER(Dim1, Dim2) = CSng(if(IFsResults("TRADECOMPLEMENTSER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            If IFsResults("trademax").RowCount > (Year - FirstYear) * (1) Then
                trademax = CSng(IFsResults("trademax")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                trademax = 0
            End If

            ReDim elastrpr(sNR, 6)
            If IFsResults("elastrpr").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        elastrpr(Dim1, Dim2) = CSng(if(IFsResults("elastrpr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("thrtcoeftrdshr").RowCount > (Year - FirstYear) * (1) Then
                thrtcoeftrdshr = CSng(IFsResults("thrtcoeftrdshr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                thrtcoeftrdshr = 0
            End If

            ReDim INFRAINDTRADDEM(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAINDTRADDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAINDTRADDEM(Dim1) = CSng(IFsResults("INFRAINDTRADDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim tradsratadd(sNR)
            If IFsResults("tradsratadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    tradsratadd(Dim1) = CSng(IFsResults("tradsratadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim TRADSRATLD(sNR)
            If IFsResults("TRADSRATLD").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    TRADSRATLD(Dim1) = CSng(IFsResults("TRADSRATLD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim TRADSRAT(sNR, 7)
            If IFsResults("TRADSRAT").RowCount > (Year - FirstYear) * (1 * sNR * 7) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 7
                        TRADSRAT(Dim1, Dim2) = CSng(if(IFsResults("TRADSRAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("hlvehsw").RowCount > (Year - FirstYear) * (1) Then
                hlvehsw = CSng(IFsResults("hlvehsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlvehsw = 0
            End If

            ReDim deathtrpvsetar(sNR)
            If IFsResults("deathtrpvsetar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    deathtrpvsetar(Dim1) = CSng(IFsResults("deathtrpvsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim deathtrpvseyrtar(sNR)
            If IFsResults("deathtrpvseyrtar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    deathtrpvseyrtar(Dim1) = CSng(IFsResults("deathtrpvseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim DEATHTRPV(sNR)
            if Year = FirstYear then
                If IFsResults("DEATHTRPV").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        DEATHTRPV(Dim1) = CSng(IFsResults("DEATHTRPV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim EDSECUPPRTRAN(sNR, 3)
            If IFsResults("EDSECUPPRTRAN").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECUPPRTRAN(Dim1, Dim2) = CSng(if(IFsResults("EDSECUPPRTRAN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDSECLOWRTRAN(sNR, 3)
            If IFsResults("EDSECLOWRTRAN").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECLOWRTRAN(Dim1, Dim2) = CSng(if(IFsResults("EDSECLOWRTRAN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim INFRAINDTRAN(sNR)
            if Year = FirstYear then
                If IFsResults("INFRAINDTRAN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        INFRAINDTRAN(Dim1) = CSng(IFsResults("INFRAINDTRAN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim wastewatertreatedm(sNR)
            If IFsResults("wastewatertreatedm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    wastewatertreatedm(Dim1) = CSng(IFsResults("wastewatertreatedm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("tmortr2").RowCount > (Year - FirstYear) * (1) Then
                tmortr2 = CSng(IFsResults("tmortr2")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                tmortr2 = 0
            End If

            If IFsResults("tmortr").RowCount > (Year - FirstYear) * (1) Then
                tmortr = CSng(IFsResults("tmortr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                tmortr = 0
            End If

            If IFsResults("ttfrr").RowCount > (Year - FirstYear) * (1) Then
                ttfrr = CSng(IFsResults("ttfrr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ttfrr = 0
            End If

            ReDim TRUCKSFL(sNR)
            if Year = FirstYear then
                If IFsResults("TRUCKSFL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        TRUCKSFL(Dim1) = CSng(IFsResults("TRUCKSFL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim TRUCKSFLPC(sNR)
            if Year = FirstYear then
                If IFsResults("TRUCKSFLPC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        TRUCKSFLPC(Dim1) = CSng(IFsResults("TRUCKSFLPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim TRUCKS(sNR, 12)
            If IFsResults("TRUCKS").RowCount > (Year - FirstYear) * (1 * sNR * 12) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 12
                        TRUCKS(Dim1, Dim2) = CSng(if(IFsResults("TRUCKS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 12)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim trucksm(sNR, 12)
            If IFsResults("trucksm").RowCount > (Year - FirstYear) * (1 * sNR * 12) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 12
                        trucksm(Dim1, Dim2) = CSng(if(IFsResults("trucksm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 12)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim TRUCKSALPC(sNR)
            if Year = FirstYear then
                If IFsResults("TRUCKSALPC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        TRUCKSALPC(Dim1) = CSng(IFsResults("TRUCKSALPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim truckstotm(sNR)
            If IFsResults("truckstotm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    truckstotm(Dim1) = CSng(IFsResults("truckstotm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("trucksconv").RowCount > (Year - FirstYear) * (1) Then
                trucksconv = CSng(IFsResults("trucksconv")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                trucksconv = 0
            End If

            ReDim TRUCKSTOT(sNR)
            If IFsResults("TRUCKSTOT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    TRUCKSTOT(Dim1) = CSng(IFsResults("TRUCKSTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("hlgbdconvUp").RowCount > (Year - FirstYear) * (1) Then
                hlgbdconvUp = CSng(IFsResults("hlgbdconvUp")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlgbdconvUp = 0
            End If

            ReDim QEUNCON(sNR, 9)
            If IFsResults("QEUNCON").RowCount > (Year - FirstYear) * (1 * sNR * 9) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 9
                        QEUNCON(Dim1, Dim2) = CSng(if(IFsResults("QEUNCON")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim MALNCHP(sNR)
            If IFsResults("MALNCHP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    MALNCHP(Dim1) = CSng(IFsResults("MALNCHP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim LABUNEMPR(sNR)
            If IFsResults("LABUNEMPR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    LABUNEMPR(Dim1) = CSng(IFsResults("LABUNEMPR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim labunemprm(sNR)
            If IFsResults("labunemprm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    labunemprm(Dim1) = CSng(IFsResults("labunemprm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim labunemprtrgtval(sNR)
            If IFsResults("labunemprtrgtval").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    labunemprtrgtval(Dim1) = CSng(IFsResults("labunemprtrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XFINUNEXP(sNR)
            If IFsResults("XFINUNEXP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XFINUNEXP(Dim1) = CSng(IFsResults("XFINUNEXP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim ictbroadtrgtyr(sNR)
            If IFsResults("ictbroadtrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ictbroadtrgtyr(Dim1) = CSng(IFsResults("ictbroadtrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim watsafetrgtyr(sNR)
            If IFsResults("watsafetrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    watsafetrgtyr(Dim1) = CSng(IFsResults("watsafetrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim sanitationtrgtyr(sNR)
            If IFsResults("sanitationtrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    sanitationtrgtyr(Dim1) = CSng(IFsResults("sanitationtrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim UNPAIDLABOR(sNR, 3)
            if Year = FirstYear then
                If IFsResults("UNPAIDLABOR").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            UNPAIDLABOR(Dim1, Dim2) = CSng(if(IFsResults("UNPAIDLABOR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim unpaidlaborm(sNR, 3)
            If IFsResults("unpaidlaborm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        unpaidlaborm(Dim1, Dim2) = CSng(if(IFsResults("unpaidlaborm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim CSHHU(sNR, 6)
            if Year = FirstYear then
                If IFsResults("CSHHU").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            CSHHU(Dim1, Dim2) = CSng(if(IFsResults("CSHHU")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim ldcropunusedshare(sNR)
            If IFsResults("ldcropunusedshare").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ldcropunusedshare(Dim1) = CSng(IFsResults("ldcropunusedshare")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edsecupprlen(sNR)
            If IFsResults("edsecupprlen").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edsecupprlen(Dim1) = CSng(IFsResults("edsecupprlen")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim EDSECUPPRENRG(sNR, 3)
            If IFsResults("EDSECUPPRENRG").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDSECUPPRENRG(Dim1, Dim2) = CSng(if(IFsResults("EDSECUPPRENRG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edsecupprenrm(sNR, 3)
            If IFsResults("edsecupprenrm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edsecupprenrm(Dim1, Dim2) = CSng(if(IFsResults("edsecupprenrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDEXPERSECUPPR(sNR)
            If IFsResults("EDEXPERSECUPPR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    EDEXPERSECUPPR(Dim1) = CSng(IFsResults("EDEXPERSECUPPR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edsecupprgndreqgrad(sNR)
            If IFsResults("edsecupprgndreqgrad").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edsecupprgndreqgrad(Dim1) = CSng(IFsResults("edsecupprgndreqgrad")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edsecupprgradgr(sNR, 3)
            If IFsResults("edsecupprgradgr").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edsecupprgradgr(Dim1, Dim2) = CSng(if(IFsResults("edsecupprgradgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edsecupprgratrgtyr(sNR)
            If IFsResults("edsecupprgratrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edsecupprgratrgtyr(Dim1) = CSng(IFsResults("edsecupprgratrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edsecupprgram(sNR, 3)
            If IFsResults("edsecupprgram").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edsecupprgram(Dim1, Dim2) = CSng(if(IFsResults("edsecupprgram")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edsecupprtrancap(sNR)
            If IFsResults("edsecupprtrancap").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edsecupprtrancap(Dim1) = CSng(IFsResults("edsecupprtrancap")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edsecupprgndreqtran(sNR)
            If IFsResults("edsecupprgndreqtran").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edsecupprgndreqtran(Dim1) = CSng(IFsResults("edsecupprgndreqtran")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edsecupprtrangr(sNR, 3)
            If IFsResults("edsecupprtrangr").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edsecupprtrangr(Dim1, Dim2) = CSng(if(IFsResults("edsecupprtrangr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edsecupprtrantrgtyr(sNR)
            If IFsResults("edsecupprtrantrgtyr").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    edsecupprtrantrgtyr(Dim1) = CSng(IFsResults("edsecupprtrantrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edsecupprtranm(sNR, 3)
            If IFsResults("edsecupprtranm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edsecupprtranm(Dim1, Dim2) = CSng(if(IFsResults("edsecupprtranm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim edsecupprvocadd(sNR, 3)
            If IFsResults("edsecupprvocadd").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edsecupprvocadd(Dim1, Dim2) = CSng(if(IFsResults("edsecupprvocadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim POPURBAN(sNR)
            If IFsResults("POPURBAN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    POPURBAN(Dim1) = CSng(IFsResults("POPURBAN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim POPURBGR(sNR)
            if Year = FirstYear then
                If IFsResults("POPURBGR").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POPURBGR(Dim1) = CSng(IFsResults("POPURBGR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ENVPM2PT5(sNR)
            if Year = FirstYear then
                If IFsResults("ENVPM2PT5").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        ENVPM2PT5(Dim1) = CSng(IFsResults("ENVPM2PT5")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("ifsexomodelid").RowCount > (Year - FirstYear) * (1) Then
                ifsexomodelid = CSng(IFsResults("ifsexomodelid")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ifsexomodelid = 0
            End If

            ReDim VADDHHS(sNR, 6)
            if Year = FirstYear then
                If IFsResults("VADDHHS").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            VADDHHS(Dim1, Dim2) = CSng(if(IFsResults("VADDHHS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim VADD(sNR, 6)
            If IFsResults("VADD").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        VADD(Dim1, Dim2) = CSng(if(IFsResults("VADD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim VADDHHU(sNR, 6)
            if Year = FirstYear then
                If IFsResults("VADDHHU").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            VADDHHU(Dim1, Dim2) = CSng(if(IFsResults("VADDHHU")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim VADDF(sNR, 6)
            if Year = FirstYear then
                If IFsResults("VADDF").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 6
                            VADDF(Dim1, Dim2) = CSng(if(IFsResults("VADDF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim VEHICLESTOT(sNR)
            if Year = FirstYear then
                If IFsResults("VEHICLESTOT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        VEHICLESTOT(Dim1) = CSng(IFsResults("VEHICLESTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim VEHICFLPC(sNR)
            if Year = FirstYear then
                If IFsResults("VEHICFLPC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        VEHICFLPC(Dim1) = CSng(IFsResults("VEHICFLPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim vehicflpcm(sNR)
            If IFsResults("vehicflpcm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    vehicflpcm(Dim1) = CSng(IFsResults("vehicflpcm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("vehfuncsw").RowCount > (Year - FirstYear) * (1) Then
                vehfuncsw = CSng(IFsResults("vehfuncsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                vehfuncsw = 0
            End If

            ReDim ensfvent(sNR)
            If IFsResults("ensfvent").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ensfvent(Dim1) = CSng(IFsResults("ensfvent")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SVDTHSCHIL(sNR)
            If IFsResults("SVDTHSCHIL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SVDTHSCHIL(Dim1) = CSng(IFsResults("SVDTHSCHIL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SVDTHSWOMEN(sNR)
            If IFsResults("SVDTHSWOMEN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SVDTHSWOMEN(Dim1) = CSng(IFsResults("SVDTHSWOMEN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim MIGRANTSVOLOUT(sNR)
            if Year = FirstYear then
                If IFsResults("MIGRANTSVOLOUT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MIGRANTSVOLOUT(Dim1) = CSng(IFsResults("MIGRANTSVOLOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim MIGRANTSVOLIN(sNR)
            if Year = FirstYear then
                If IFsResults("MIGRANTSVOLIN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        MIGRANTSVOLIN(Dim1) = CSng(IFsResults("MIGRANTSVOLIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("wagnerc").RowCount > (Year - FirstYear) * (1) Then
                wagnerc = CSng(IFsResults("wagnerc")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wagnerc = 0
            End If

            If IFsResults("waron").RowCount > (Year - FirstYear) * (1) Then
                waron = CSng(IFsResults("waron")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                waron = 0
            End If

            If IFsResults("watwastetreatcostupper").RowCount > (Year - FirstYear) * (1) Then
                watwastetreatcostupper = CSng(IFsResults("watwastetreatcostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                watwastetreatcostupper = 0
            End If

            If IFsResults("watwastetreatcostlower").RowCount > (Year - FirstYear) * (1) Then
                watwastetreatcostlower = CSng(IFsResults("watwastetreatcostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                watwastetreatcostlower = 0
            End If

            ReDim WATWASTETREATCOST(sNR)
            if Year = FirstYear then
                If IFsResults("WATWASTETREATCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WATWASTETREATCOST(Dim1) = CSng(IFsResults("WATWASTETREATCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim WATWASTE(sNR)
            if Year = FirstYear then
                If IFsResults("WATWASTE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WATWASTE(Dim1) = CSng(IFsResults("WATWASTE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim watwastem(sNR)
            If IFsResults("watwastem").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    watwastem(Dim1) = CSng(IFsResults("watwastem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim wastewaterportiontreated(sNR)
            If IFsResults("wastewaterportiontreated").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    wastewaterportiontreated(Dim1) = CSng(IFsResults("wastewaterportiontreated")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim watwastetreatsetar(sNR)
            If IFsResults("watwastetreatsetar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    watwastetreatsetar(Dim1) = CSng(IFsResults("watwastetreatsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim WASTEWATERSUPPLY(sNR, 3)
            If IFsResults("WASTEWATERSUPPLY").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        WASTEWATERSUPPLY(Dim1, Dim2) = CSng(if(IFsResults("WASTEWATERSUPPLY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim wastewatportreatreusedm(sNR)
            If IFsResults("wastewatportreatreusedm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    wastewatportreatreusedm(Dim1) = CSng(IFsResults("wastewatportreatreusedm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim WATWASTETREATDEM(sNR)
            if Year = FirstYear then
                If IFsResults("WATWASTETREATDEM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WATWASTETREATDEM(Dim1) = CSng(IFsResults("WATWASTETREATDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim watwastetreatcostm(sNR)
            If IFsResults("watwastetreatcostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    watwastetreatcostm(Dim1) = CSng(IFsResults("watwastetreatcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim WATWASTETREAT(sNR)
            if Year = FirstYear then
                If IFsResults("WATWASTETREAT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WATWASTETREAT(Dim1) = CSng(IFsResults("WATWASTETREAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim watwastetreatm(sNR)
            If IFsResults("watwastetreatm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    watwastetreatm(Dim1) = CSng(IFsResults("watwastetreatm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("watwastetreatlife").RowCount > (Year - FirstYear) * (1) Then
                watwastetreatlife = CSng(IFsResults("watwastetreatlife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                watwastetreatlife = 0
            End If

            ReDim watwastetreatseyrtar(sNR)
            If IFsResults("watwastetreatseyrtar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    watwastetreatseyrtar(Dim1) = CSng(IFsResults("watwastetreatseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim WATSAFEDEM(sNR, 3)
            If IFsResults("WATSAFEDEM").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        WATSAFEDEM(Dim1, Dim2) = CSng(if(IFsResults("WATSAFEDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("watsafenewsw").RowCount > (Year - FirstYear) * (1) Then
                watsafenewsw = CSng(IFsResults("watsafenewsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                watsafenewsw = 0
            End If

            If IFsResults("watsafefwdsw").RowCount > (Year - FirstYear) * (1) Then
                watsafefwdsw = CSng(IFsResults("watsafefwdsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                watsafefwdsw = 0
            End If

            If IFsResults("hlwatsansw").RowCount > (Year - FirstYear) * (1) Then
                hlwatsansw = CSng(IFsResults("hlwatsansw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                hlwatsansw = 0
            End If

            ReDim WATERHHCOSTHIGHER(sNR, 5, 3)
            If IFsResults("WATERHHCOSTHIGHER").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            WATERHHCOSTHIGHER(Dim1, Dim2, Dim3) = CSng(if(IFsResults("WATERHHCOSTHIGHER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim WATERHHCOSTLOWER(sNR, 5, 3)
            If IFsResults("WATERHHCOSTLOWER").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            WATERHHCOSTLOWER(Dim1, Dim2, Dim3) = CSng(if(IFsResults("WATERHHCOSTLOWER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim WATSAFECOST(sNR)
            if Year = FirstYear then
                If IFsResults("WATSAFECOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WATSAFECOST(Dim1) = CSng(IFsResults("WATSAFECOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("watsafecostupper").RowCount > (Year - FirstYear) * (1) Then
                watsafecostupper = CSng(IFsResults("watsafecostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                watsafecostupper = 0
            End If

            If IFsResults("watsafecostlower").RowCount > (Year - FirstYear) * (1) Then
                watsafecostlower = CSng(IFsResults("watsafecostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                watsafecostlower = 0
            End If

            ReDim watsafecostm(sNR)
            If IFsResults("watsafecostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    watsafecostm(Dim1) = CSng(IFsResults("watsafecostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("watsafeimpcostupper").RowCount > (Year - FirstYear) * (1) Then
                watsafeimpcostupper = CSng(IFsResults("watsafeimpcostupper")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                watsafeimpcostupper = 0
            End If

            If IFsResults("watsafeimpcostlower").RowCount > (Year - FirstYear) * (1) Then
                watsafeimpcostlower = CSng(IFsResults("watsafeimpcostlower")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                watsafeimpcostlower = 0
            End If

            ReDim watsafeimpcostm(sNR)
            If IFsResults("watsafeimpcostm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    watsafeimpcostm(Dim1) = CSng(IFsResults("watsafeimpcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("watsafelife").RowCount > (Year - FirstYear) * (1) Then
                watsafelife = CSng(IFsResults("watsafelife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                watsafelife = 0
            End If

            ReDim WATSAFEIMPCOST(sNR)
            if Year = FirstYear then
                If IFsResults("WATSAFEIMPCOST").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WATSAFEIMPCOST(Dim1) = CSng(IFsResults("WATSAFEIMPCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim WATDEMAND(sNR, 3)
            if Year = FirstYear then
                If IFsResults("WATDEMAND").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            WATDEMAND(Dim1, Dim2) = CSng(if(IFsResults("WATDEMAND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim waterdemandm(sNR, 3)
            If IFsResults("waterdemandm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        waterdemandm(Dim1, Dim2) = CSng(if(IFsResults("waterdemandm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim WATERDESALINATED(sNR)
            if Year = FirstYear then
                If IFsResults("WATERDESALINATED").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WATERDESALINATED(Dim1) = CSng(IFsResults("WATERDESALINATED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("watsafeimplife").RowCount > (Year - FirstYear) * (1) Then
                watsafeimplife = CSng(IFsResults("watsafeimplife")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                watsafeimplife = 0
            End If

            If IFsResults("watprid").RowCount > (Year - FirstYear) * (1) Then
                watprid = CSng(IFsResults("watprid")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                watprid = 0
            End If

            If IFsResults("watprii").RowCount > (Year - FirstYear) * (1) Then
                watprii = CSng(IFsResults("watprii")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                watprii = 0
            End If

            ReDim watpricetoylelas(sNR)
            If IFsResults("watpricetoylelas").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    watpricetoylelas(Dim1) = CSng(IFsResults("watpricetoylelas")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim landirareaequipelas(sNR)
            If IFsResults("landirareaequipelas").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    landirareaequipelas(Dim1) = CSng(IFsResults("landirareaequipelas")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim watdemandelas(sNR, 3)
            If IFsResults("watdemandelas").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        watdemandelas(Dim1, Dim2) = CSng(if(IFsResults("watdemandelas")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim WATPRICE(sNR)
            If IFsResults("WATPRICE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    WATPRICE(Dim1) = CSng(IFsResults("WATPRICE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim watres(sNR)
            If IFsResults("watres").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    watres(Dim1) = CSng(IFsResults("watres")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("waterhhlifeboreholetubewell").RowCount > (Year - FirstYear) * (1) Then
                waterhhlifeboreholetubewell = CSng(IFsResults("waterhhlifeboreholetubewell")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                waterhhlifeboreholetubewell = 0
            End If

            If IFsResults("waterhhlifedugwell").RowCount > (Year - FirstYear) * (1) Then
                waterhhlifedugwell = CSng(IFsResults("waterhhlifedugwell")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                waterhhlifedugwell = 0
            End If

            If IFsResults("waterhhlifepiped").RowCount > (Year - FirstYear) * (1) Then
                waterhhlifepiped = CSng(IFsResults("waterhhlifepiped")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                waterhhlifepiped = 0
            End If

            ReDim watsafeexovals(sNR, 3)
            If IFsResults("watsafeexovals").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        watsafeexovals(Dim1, Dim2) = CSng(if(IFsResults("watsafeexovals")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim WATERHHDEMPOP(sNR, 5, 3)
            If IFsResults("WATERHHDEMPOP").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            WATERHHDEMPOP(Dim1, Dim2, Dim3) = CSng(if(IFsResults("WATERHHDEMPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim WATERHHDEM(sNR, 5, 3)
            If IFsResults("WATERHHDEM").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            WATERHHDEM(Dim1, Dim2, Dim3) = CSng(if(IFsResults("WATERHHDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim waterhhm(sNR, 5, 3)
            If IFsResults("waterhhm").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            waterhhm(Dim1, Dim2, Dim3) = CSng(if(IFsResults("waterhhm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim WATERHH(sNR, 5, 3)
            If IFsResults("WATERHH").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            WATERHH(Dim1, Dim2, Dim3) = CSng(if(IFsResults("WATERHH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim waterhhbasictrgtyr(sNR, 3)
            If IFsResults("waterhhbasictrgtyr").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        waterhhbasictrgtyr(Dim1, Dim2) = CSng(if(IFsResults("waterhhbasictrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim waterhhbasictrgtval(sNR, 3)
            If IFsResults("waterhhbasictrgtval").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        waterhhbasictrgtval(Dim1, Dim2) = CSng(if(IFsResults("waterhhbasictrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim waterbudgetprotec(sNR)
            If IFsResults("waterbudgetprotec").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    waterbudgetprotec(Dim1) = CSng(IFsResults("waterbudgetprotec")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim waterhhsafetrgtyr(sNR, 3)
            If IFsResults("waterhhsafetrgtyr").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        waterhhsafetrgtyr(Dim1, Dim2) = CSng(if(IFsResults("waterhhsafetrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim waterhhsafetrgtval(sNR, 3)
            If IFsResults("waterhhsafetrgtval").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        waterhhsafetrgtval(Dim1, Dim2) = CSng(if(IFsResults("waterhhsafetrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim waterhhcostlowershr(sNR, 5, 3)
            If IFsResults("waterhhcostlowershr").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            waterhhcostlowershr(Dim1, Dim2, Dim3) = CSng(if(IFsResults("waterhhcostlowershr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim WATERHHINVESTCAP(sNR, 5, 3)
            if Year = FirstYear then
                If IFsResults("WATERHHINVESTCAP").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 5
                            For Dim3 = 1 to 3
                                WATERHHINVESTCAP(Dim1, Dim2, Dim3) = CSng(if(IFsResults("WATERHHINVESTCAP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim WATERHHINVESTMAINT(sNR, 5, 3)
            if Year = FirstYear then
                If IFsResults("WATERHHINVESTMAINT").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 5
                            For Dim3 = 1 to 3
                                WATERHHINVESTMAINT(Dim1, Dim2, Dim3) = CSng(if(IFsResults("WATERHHINVESTMAINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim WATERHHPOP(sNR, 5, 3)
            If IFsResults("WATERHHPOP").RowCount > (Year - FirstYear) * (1 * sNR * 5 * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 5
                        For Dim3 = 1 to 3
                            WATERHHPOP(Dim1, Dim2, Dim3) = CSng(if(IFsResults("WATERHHPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)),0))
                        Next Dim3
                    Next Dim2
                Next Dim1
            End If

            ReDim WATERFILTERED(sNR, 3)
            If IFsResults("WATERFILTERED").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        WATERFILTERED(Dim1, Dim2) = CSng(if(IFsResults("WATERFILTERED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim waterfilteredm(sNR, 3)
            If IFsResults("waterfilteredm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        waterfilteredm(Dim1, Dim2) = CSng(if(IFsResults("waterfilteredm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim watertoyieldswitch(sNR)
            If IFsResults("watertoyieldswitch").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    watertoyieldswitch(Dim1) = CSng(IFsResults("watertoyieldswitch")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim WATUSE(sNR)
            if Year = FirstYear then
                If IFsResults("WATUSE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WATUSE(Dim1) = CSng(IFsResults("WATUSE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim WATUSEPC(sNR)
            If IFsResults("WATUSEPC").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    WATUSEPC(Dim1) = CSng(IFsResults("WATUSEPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim WATERWITHDRAWAL(sNR, 3)
            if Year = FirstYear then
                If IFsResults("WATERWITHDRAWAL").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            WATERWITHDRAWAL(Dim1, Dim2) = CSng(if(IFsResults("WATERWITHDRAWAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim waterwithdrawalm(sNR, 3)
            If IFsResults("waterwithdrawalm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        waterwithdrawalm(Dim1, Dim2) = CSng(if(IFsResults("waterwithdrawalm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            If IFsResults("fprihw").RowCount > (Year - FirstYear) * (1) Then
                fprihw = CSng(IFsResults("fprihw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                fprihw = 0
            End If

            If IFsResults("ylhw").RowCount > (Year - FirstYear) * (1) Then
                ylhw = CSng(IFsResults("ylhw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ylhw = 0
            End If

            ReDim mhwag(sNR)
            If IFsResults("mhwag").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    mhwag(Dim1) = CSng(IFsResults("mhwag")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("eprohw").RowCount > (Year - FirstYear) * (1) Then
                eprohw = CSng(IFsResults("eprohw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                eprohw = 0
            End If

            If IFsResults("mhw").RowCount > (Year - FirstYear) * (1) Then
                mhw = CSng(IFsResults("mhw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mhw = 0
            End If

            If IFsResults("ehw").RowCount > (Year - FirstYear) * (1) Then
                ehw = CSng(IFsResults("ehw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ehw = 0
            End If

            If IFsResults("mbasehw").RowCount > (Year - FirstYear) * (1) Then
                mbasehw = CSng(IFsResults("mbasehw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                mbasehw = 0
            End If

            If IFsResults("xhw").RowCount > (Year - FirstYear) * (1) Then
                xhw = CSng(IFsResults("xhw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                xhw = 0
            End If

            ReDim infraindictcompwt(4)
            If IFsResults("infraindictcompwt").RowCount > (Year - FirstYear) * (1 * 4) Then
                For Dim1 = 1 to 4
                    infraindictcompwt(Dim1) = CSng(IFsResults("infraindictcompwt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)))
                Next Dim1
            End If

            ReDim infraindtradcompwt(7)
            If IFsResults("infraindtradcompwt").RowCount > (Year - FirstYear) * (1 * 7) Then
                For Dim1 = 1 to 7
                    infraindtradcompwt(Dim1) = CSng(IFsResults("infraindtradcompwt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 7)))
                Next Dim1
            End If

            ReDim wpwghtpow(26)
            If IFsResults("wpwghtpow").RowCount > (Year - FirstYear) * (1 * 26) Then
                For Dim1 = 1 to 26
                    wpwghtpow(Dim1) = CSng(IFsResults("wpwghtpow")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 26)))
                Next Dim1
            End If

            ReDim IGOBILAT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("IGOBILAT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
             If bilateralsw = 1 Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            IGOBILAT(Dim1, Dim2) = CSng(if(IFsResults("IGOBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
             End If
                End If
            End if

            ReDim DIPLOIGO(sNR)
            if Year = FirstYear then
                If IFsResults("DIPLOIGO").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        DIPLOIGO(Dim1) = CSng(IFsResults("DIPLOIGO")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim DIPLOTREATY(sNR)
            if Year = FirstYear then
                If IFsResults("DIPLOTREATY").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        DIPLOTREATY(Dim1) = CSng(IFsResults("DIPLOTREATY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            if Year = FirstYear then
                If IFsResults("WPQLI").RowCount > (Year - FirstYear) * (1) Then
                    WPQLI = CSng(IFsResults("WPQLI")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WPQLI = 0
                End If
            Else
                WPQLI = 0
            End if

            ReDim govriskweight(27)
            If IFsResults("govriskweight").RowCount > (Year - FirstYear) * (1 * 27) Then
                For Dim1 = 1 to 27
                    govriskweight(Dim1) = CSng(IFsResults("govriskweight")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 27)))
                Next Dim1
            End If

            If IFsResults("exomerpppmatchsw").RowCount > (Year - FirstYear) * (1) Then
                exomerpppmatchsw = CSng(IFsResults("exomerpppmatchsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                exomerpppmatchsw = 0
            End If

            ReDim xworkremitinm(sNR)
            If IFsResults("xworkremitinm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    xworkremitinm(Dim1) = CSng(IFsResults("xworkremitinm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim xworkremitoutm(sNR)
            If IFsResults("xworkremitoutm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    xworkremitoutm(Dim1) = CSng(IFsResults("xworkremitoutm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim POPWORKING(sNR)
            if Year = FirstYear then
                If IFsResults("POPWORKING").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POPWORKING(Dim1) = CSng(IFsResults("POPWORKING")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim CWORKING(sNR)
            if Year = FirstYear then
                If IFsResults("CWORKING").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        CWORKING(Dim1) = CSng(IFsResults("CWORKING")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            if Year = FirstYear then
                If IFsResults("WTEMP").RowCount > (Year - FirstYear) * (1) Then
                    WTEMP = CSng(IFsResults("WTEMP")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WTEMP = 0
                End If
            Else
                WTEMP = 0
            End if

            ReDim XWBFLOWS(sNR, 5)
            if Year = FirstYear then
                If IFsResults("XWBFLOWS").RowCount > (Year - FirstYear) * (1 * sNR * 5) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 5
                            XWBFLOWS(Dim1, Dim2) = CSng(if(IFsResults("XWBFLOWS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim XWBLNFIN(sNR)
            If IFsResults("XWBLNFIN").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XWBLNFIN(Dim1) = CSng(IFsResults("XWBLNFIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("xwblnintr").RowCount > (Year - FirstYear) * (1) Then
                xwblnintr = CSng(IFsResults("xwblnintr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                xwblnintr = 0
            End If

            If IFsResults("XWBLOANSTOT").RowCount > (Year - FirstYear) * (1) Then
                XWBLOANSTOT = CSng(IFsResults("XWBLOANSTOT")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                XWBLOANSTOT = 0
            End If

            If IFsResults("xwbloanr").RowCount > (Year - FirstYear) * (1) Then
                xwbloanr = CSng(IFsResults("xwbloanr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                xwbloanr = 0
            End If

            If IFsResults("xwblnrepr").RowCount > (Year - FirstYear) * (1) Then
                xwblnrepr = CSng(IFsResults("xwblnrepr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                xwblnrepr = 0
            End If

            If IFsResults("xwblneqr").RowCount > (Year - FirstYear) * (1) Then
                xwblneqr = CSng(IFsResults("xwblneqr")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                xwblneqr = 0
            End If

            ReDim xwblnfinm(sNR)
            If IFsResults("xwblnfinm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    xwblnfinm(Dim1) = CSng(IFsResults("xwblnfinm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XWBLOANS(sNR)
            If IFsResults("XWBLOANS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XWBLOANS(Dim1) = CSng(IFsResults("XWBLOANS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim XWBLNFOUT(sNR)
            If IFsResults("XWBLNFOUT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    XWBLNFOUT(Dim1) = CSng(IFsResults("XWBLNFOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim xwbsectar(5)
            If IFsResults("xwbsectar").RowCount > (Year - FirstYear) * (1 * 5) Then
                For Dim1 = 1 to 5
                    xwbsectar(Dim1) = CSng(IFsResults("xwbsectar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 5)))
                Next Dim1
            End If

            if Year = FirstYear then
                If IFsResults("WCLPC").RowCount > (Year - FirstYear) * (1) Then
                    WCLPC = CSng(IFsResults("WCLPC")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WCLPC = 0
                End If
            Else
                WCLPC = 0
            End if

            if Year = FirstYear then
                If IFsResults("WCPOW").RowCount > (Year - FirstYear) * (1) Then
                    WCPOW = CSng(IFsResults("WCPOW")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WCPOW = 0
                End If
            Else
                WCPOW = 0
            End if

            If IFsResults("WEPBYEAR").RowCount > (Year - FirstYear) * (1) Then
                WEPBYEAR = CSng(IFsResults("WEPBYEAR")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                WEPBYEAR = 0
            End If

            if Year = FirstYear then
                If IFsResults("WEP").RowCount > (Year - FirstYear) * (1) Then
                    WEP = CSng(IFsResults("WEP")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WEP = 0
                End If
            Else
                WEP = 0
            End if

            ReDim WENP(9)
            if Year = FirstYear then
                If IFsResults("WENP").RowCount > (Year - FirstYear) * (1 * 9) Then
                    For Dim1 = 1 to 9
                        WENP(Dim1) = CSng(IFsResults("WENP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9)))
                    Next Dim1
                End If
            End if

            ReDim WRESER(9)
            if Year = FirstYear then
                If IFsResults("WRESER").RowCount > (Year - FirstYear) * (1 * 9) Then
                    For Dim1 = 1 to 9
                        WRESER(Dim1) = CSng(IFsResults("WRESER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9)))
                    Next Dim1
                End If
            End if

            If IFsResults("xfdiwgrm").RowCount > (Year - FirstYear) * (1) Then
                xfdiwgrm = CSng(IFsResults("xfdiwgrm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                xfdiwgrm = 0
            End If

            ReDim WAP(3)
            If IFsResults("WAP").RowCount > (Year - FirstYear) * (1 * 3) Then
                For Dim1 = 1 to 3
                    WAP(Dim1) = CSng(IFsResults("WAP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)))
                Next Dim1
            End If

            if Year = FirstYear then
                If IFsResults("WFORST").RowCount > (Year - FirstYear) * (1) Then
                    WFORST = CSng(IFsResults("WFORST")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WFORST = 0
                End If
            Else
                WFORST = 0
            End if

            if Year = FirstYear then
                If IFsResults("WGDP").RowCount > (Year - FirstYear) * (1) Then
                    WGDP = CSng(IFsResults("WGDP")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WGDP = 0
                End If
            Else
                WGDP = 0
            End if

            if Year = FirstYear then
                If IFsResults("WGDPPC").RowCount > (Year - FirstYear) * (1) Then
                    WGDPPC = CSng(IFsResults("WGDPPC")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WGDPPC = 0
                End If
            Else
                WGDPPC = 0
            End if

            if Year = FirstYear then
                If IFsResults("WLIFE").RowCount > (Year - FirstYear) * (1) Then
                    WLIFE = CSng(IFsResults("WLIFE")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WLIFE = 0
                End If
            Else
                WLIFE = 0
            End if

            if Year = FirstYear then
                If IFsResults("WLIT").RowCount > (Year - FirstYear) * (1) Then
                    WLIT = CSng(IFsResults("WLIT")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WLIT = 0
                End If
            Else
                WLIT = 0
            End if

            If IFsResults("wmigrm").RowCount > (Year - FirstYear) * (1) Then
                wmigrm = CSng(IFsResults("wmigrm")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wmigrm = 0
            End If

            if Year = FirstYear then
                If IFsResults("WNPOW").RowCount > (Year - FirstYear) * (1) Then
                    WNPOW = CSng(IFsResults("WNPOW")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WNPOW = 0
                End If
            Else
                WNPOW = 0
            End if

            ReDim wpextinterv(sNR)
            If IFsResults("wpextinterv").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    wpextinterv(Dim1) = CSng(IFsResults("wpextinterv")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("wppowhhsw").RowCount > (Year - FirstYear) * (1) Then
                wppowhhsw = CSng(IFsResults("wppowhhsw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wppowhhsw = 0
            End If

            if Year = FirstYear then
                If IFsResults("WPOP").RowCount > (Year - FirstYear) * (1) Then
                    WPOP = CSng(IFsResults("WPOP")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WPOP = 0
                End If
            Else
                WPOP = 0
            End if

            if Year = FirstYear then
                If IFsResults("WPOPR").RowCount > (Year - FirstYear) * (1) Then
                    WPOPR = CSng(IFsResults("WPOPR")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WPOPR = 0
                End If
            Else
                WPOPR = 0
            End if

            ReDim WP(6)
            if Year = FirstYear then
                If IFsResults("WP").RowCount > (Year - FirstYear) * (1 * 6) Then
                    For Dim1 = 1 to 6
                        WP(Dim1) = CSng(IFsResults("WP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)))
                    Next Dim1
                End If
            End if

            ReDim WRESOR(9)
            if Year = FirstYear then
                If IFsResults("WRESOR").RowCount > (Year - FirstYear) * (1 * 9) Then
                    For Dim1 = 1 to 9
                        WRESOR(Dim1) = CSng(IFsResults("WRESOR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9)))
                    Next Dim1
                End If
            End if

            if Year = FirstYear then
                If IFsResults("WTHREAT").RowCount > (Year - FirstYear) * (1) Then
                    WTHREAT = CSng(IFsResults("WTHREAT")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WTHREAT = 0
                End If
            Else
                WTHREAT = 0
            End if

            if Year = FirstYear then
                If IFsResults("WTRADE").RowCount > (Year - FirstYear) * (1) Then
                    WTRADE = CSng(IFsResults("WTRADE")(getDataFrameIdxFromDims(Year, FirstYear)))
                else
                    WTRADE = 0
                End If
            Else
                WTRADE = 0
            End if

            If IFsResults("wvsagesw").RowCount > (Year - FirstYear) * (1) Then
                wvsagesw = CSng(IFsResults("wvsagesw")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                wvsagesw = 0
            End If

            If IFsResults("MODYEAR").RowCount > (Year - FirstYear) * (1) Then
                MODYEAR = CSng(IFsResults("MODYEAR")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                MODYEAR = 0
            End If

            ReDim democpolityseyrtar(sNR)
            If IFsResults("democpolityseyrtar").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    democpolityseyrtar(Dim1) = CSng(IFsResults("democpolityseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim edyrsagm(sNR, 3)
            If IFsResults("edyrsagm").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        edyrsagm(Dim1, Dim2) = CSng(if(IFsResults("edyrsagm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDYRSAG15(sNR, 3)
            If IFsResults("EDYRSAG15").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDYRSAG15(Dim1, Dim2) = CSng(if(IFsResults("EDYRSAG15")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDYRSAG20TO29(sNR, 3)
            If IFsResults("EDYRSAG20TO29").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDYRSAG20TO29(Dim1, Dim2) = CSng(if(IFsResults("EDYRSAG20TO29")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim EDYRSAG25(sNR, 3)
            if Year = FirstYear then
                If IFsResults("EDYRSAG25").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            EDYRSAG25(Dim1, Dim2) = CSng(if(IFsResults("EDYRSAG25")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim EDYRSAG15TO24(sNR, 3)
            If IFsResults("EDYRSAG15TO24").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 3
                        EDYRSAG15TO24(Dim1, Dim2) = CSng(if(IFsResults("EDYRSAG15TO24")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim HLYLL(sNR, 15, 3)
            if Year = FirstYear then
                If IFsResults("HLYLL").RowCount > (Year - FirstYear) * (1 * sNR * 15 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 15
                            For Dim3 = 1 to 3
                                HLYLL(Dim1, Dim2, Dim3) = CSng(if(IFsResults("HLYLL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim HLYLLWORK(sNR, 15, 3)
            if Year = FirstYear then
                If IFsResults("HLYLLWORK").RowCount > (Year - FirstYear) * (1 * sNR * 15 * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 15
                            For Dim3 = 1 to 3
                                HLYLLWORK(Dim1, Dim2, Dim3) = CSng(if(IFsResults("HLYLLWORK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)),0))
                            Next Dim3
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim hltfragehalflife(sNR)
            If IFsResults("hltfragehalflife").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    hltfragehalflife(Dim1) = CSng(IFsResults("hltfragehalflife")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim mfpconv(sNR)
            If IFsResults("mfpconv").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    mfpconv(Dim1) = CSng(IFsResults("mfpconv")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim TGRYL(sNR)
            if Year = FirstYear then
                If IFsResults("TGRYL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        TGRYL(Dim1) = CSng(IFsResults("TGRYL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim YL(sNR)
            if Year = FirstYear then
                If IFsResults("YL").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        YL(Dim1) = CSng(IFsResults("YL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim YLIMPACTWATPRICE(sNR)
            if Year = FirstYear then
                If IFsResults("YLIMPACTWATPRICE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        YLIMPACTWATPRICE(Dim1) = CSng(IFsResults("YLIMPACTWATPRICE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim YIELDINT(sNR)
            if Year = FirstYear then
                If IFsResults("YIELDINT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        YIELDINT(Dim1) = CSng(IFsResults("YIELDINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            If IFsResults("ylexp").RowCount > (Year - FirstYear) * (1) Then
                ylexp = CSng(IFsResults("ylexp")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                ylexp = 0
            End If

            ReDim YTHBULGE(sNR)
            if Year = FirstYear then
                If IFsResults("YTHBULGE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        YTHBULGE(Dim1) = CSng(IFsResults("YTHBULGE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim LDRTRIP(sNR)
            if Year = FirstYear then
                If IFsResults("LDRTRIP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        LDRTRIP(Dim1) = CSng(IFsResults("LDRTRIP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim LDRTRIPOUT(sNR)
            if Year = FirstYear then
                If IFsResults("LDRTRIPOUT").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        LDRTRIPOUT(Dim1) = CSng(IFsResults("LDRTRIPOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim ldrtripm(sNR)
            If IFsResults("ldrtripm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    ldrtripm(Dim1) = CSng(IFsResults("ldrtripm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SFCIVILWARPOPIMP(sNR)
            If IFsResults("SFCIVILWARPOPIMP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SFCIVILWARPOPIMP(Dim1) = CSng(IFsResults("SFCIVILWARPOPIMP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SFCIVILWARPARDEE(sNR)
            If IFsResults("SFCIVILWARPARDEE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SFCIVILWARPARDEE(Dim1) = CSng(IFsResults("SFCIVILWARPARDEE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SFCIVILWARPOPIMPABS(sNR)
            if Year = FirstYear then
                If IFsResults("SFCIVILWARPOPIMPABS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        SFCIVILWARPOPIMPABS(Dim1) = CSng(IFsResults("SFCIVILWARPOPIMPABS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim sfcivilwaradd(sNR)
            If IFsResults("sfcivilwaradd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    sfcivilwaradd(Dim1) = CSng(IFsResults("sfcivilwaradd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim sfcivilwarpopimpm(sNR)
            If IFsResults("sfcivilwarpopimpm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    sfcivilwarpopimpm(Dim1) = CSng(IFsResults("sfcivilwarpopimpm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim POPFOREIGNBILAT(sNR, sNR)
            if Year = FirstYear then
                If IFsResults("POPFOREIGNBILAT").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to sNR
                            POPFOREIGNBILAT(Dim1, Dim2) = CSng(if(IFsResults("POPFOREIGNBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim migbilatgrowthratelimit(sNR, sNR)
            If IFsResults("migbilatgrowthratelimit").RowCount > (Year - FirstYear) * (1 * sNR * sNR) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to sNR
                        migbilatgrowthratelimit(Dim1, Dim2) = CSng(if(IFsResults("migbilatgrowthratelimit")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)),0))
                    Next Dim2
                Next Dim1
            End If

            ReDim stockin(sNR)
            If IFsResults("stockin").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    stockin(Dim1) = CSng(IFsResults("stockin")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim stockout(sNR)
            If IFsResults("stockout").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    stockout(Dim1) = CSng(IFsResults("stockout")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim POWERBECKLEY(sNR)
            if Year = FirstYear then
                If IFsResults("POWERBECKLEY").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        POWERBECKLEY(Dim1) = CSng(IFsResults("POWERBECKLEY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim Theta1(sNR)
            if Year = FirstYear then
                If IFsResults("Theta1").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        Theta1(Dim1) = CSng(IFsResults("Theta1")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim Theta2(sNR)
            if Year = FirstYear then
                If IFsResults("Theta2").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        Theta2(Dim1) = CSng(IFsResults("Theta2")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim Theta3(sNR)
            if Year = FirstYear then
                If IFsResults("Theta3").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        Theta3(Dim1) = CSng(IFsResults("Theta3")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim Theta1High(sNR)
            if Year = FirstYear then
                If IFsResults("Theta1High").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        Theta1High(Dim1) = CSng(IFsResults("Theta1High")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim Theta2High(sNR)
            if Year = FirstYear then
                If IFsResults("Theta2High").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        Theta2High(Dim1) = CSng(IFsResults("Theta2High")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim Theta3High(sNR)
            if Year = FirstYear then
                If IFsResults("Theta3High").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        Theta3High(Dim1) = CSng(IFsResults("Theta3High")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim Theta1Low(sNR)
            if Year = FirstYear then
                If IFsResults("Theta1Low").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        Theta1Low(Dim1) = CSng(IFsResults("Theta1Low")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim Theta2Low(sNR)
            if Year = FirstYear then
                If IFsResults("Theta2Low").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        Theta2Low(Dim1) = CSng(IFsResults("Theta2Low")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim Theta3Low(sNR)
            if Year = FirstYear then
                If IFsResults("Theta3Low").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        Theta3Low(Dim1) = CSng(IFsResults("Theta3Low")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim WOMENINLEGISLATURE(sNR)
            if Year = FirstYear then
                If IFsResults("WOMENINLEGISLATURE").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WOMENINLEGISLATURE(Dim1) = CSng(IFsResults("WOMENINLEGISLATURE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim WOMENINJOURNALISM(sNR)
            if Year = FirstYear then
                If IFsResults("WOMENINJOURNALISM").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WOMENINJOURNALISM(Dim1) = CSng(IFsResults("WOMENINJOURNALISM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim WOMENPROPRIGHTS(sNR)
            if Year = FirstYear then
                If IFsResults("WOMENPROPRIGHTS").RowCount > (Year - FirstYear) * (1 * sNR) Then
                    For Dim1 = 1 to sNR
                        WOMENPROPRIGHTS(Dim1) = CSng(IFsResults("WOMENPROPRIGHTS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                    Next Dim1
                End If
            End if

            ReDim mtarifftaxrgl(sNR)
            If IFsResults("mtarifftaxrgl").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    mtarifftaxrgl(Dim1) = CSng(IFsResults("mtarifftaxrgl")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim SFINSECUREPOPIMP(sNR)
            If IFsResults("SFINSECUREPOPIMP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    SFINSECUREPOPIMP(Dim1) = CSng(IFsResults("SFINSECUREPOPIMP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim sfinsecurepopimpm(sNR)
            If IFsResults("sfinsecurepopimpm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    sfinsecurepopimpm(Dim1) = CSng(IFsResults("sfinsecurepopimpm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim MALNPOP(sNR)
            If IFsResults("MALNPOP").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    MALNPOP(Dim1) = CSng(IFsResults("MALNPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            If IFsResults("gdppcppeak").RowCount > (Year - FirstYear) * (1) Then
                gdppcppeak = CSng(IFsResults("gdppcppeak")(getDataFrameIdxFromDims(Year, FirstYear)))
            else
                gdppcppeak = 0
            End If

            ReDim womenproprightsm(sNR)
            If IFsResults("womenproprightsm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    womenproprightsm(Dim1) = CSng(IFsResults("womenproprightsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim womenproprightsadd(sNR)
            If IFsResults("womenproprightsadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    womenproprightsadd(Dim1) = CSng(IFsResults("womenproprightsadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim womeninjournalismm(sNR)
            If IFsResults("womeninjournalismm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    womeninjournalismm(Dim1) = CSng(IFsResults("womeninjournalismm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim womeninjournalismadd(sNR)
            If IFsResults("womeninjournalismadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    womeninjournalismadd(Dim1) = CSng(IFsResults("womeninjournalismadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim womeninlegislaturem(sNR)
            If IFsResults("womeninlegislaturem").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    womeninlegislaturem(Dim1) = CSng(IFsResults("womeninlegislaturem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim womeninlegistlatureadd(sNR)
            If IFsResults("womeninlegistlatureadd").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    womeninlegistlatureadd(Dim1) = CSng(IFsResults("womeninlegistlatureadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim POPBYSEX(sNR, 3)
            if Year = FirstYear then
                If IFsResults("POPBYSEX").RowCount > (Year - FirstYear) * (1 * sNR * 3) Then
                    For Dim1 = 1 to sNR
                        For Dim2 = 1 to 3
                            POPBYSEX(Dim1, Dim2) = CSng(if(IFsResults("POPBYSEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)),0))
                        Next Dim2
                    Next Dim1
                End If
            End if

            ReDim popurbanm(sNR)
            If IFsResults("popurbanm").RowCount > (Year - FirstYear) * (1 * sNR) Then
                For Dim1 = 1 to sNR
                    popurbanm(Dim1) = CSng(IFsResults("popurbanm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)))
                Next Dim1
            End If

            ReDim mfpaddsec(sNR, 6)
            If IFsResults("mfpaddsec").RowCount > (Year - FirstYear) * (1 * sNR * 6) Then
                For Dim1 = 1 to sNR
                    For Dim2 = 1 to 6
                        mfpaddsec(Dim1, Dim2) = CSng(if(IFsResults("mfpaddsec")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)),0))
                    Next Dim2
                Next Dim1
            End If

        End Sub

    End Module

End Namespace
