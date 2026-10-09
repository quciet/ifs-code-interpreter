Imports IFs.Data

Namespace Models

    Public Module FILL

        Public Sub Fillit(ByRef IFsResults As Dictionary(Of String, Dataframe), Year As Short, FirstYear As Short)

            Dim Dim1 as Short, Dim2 as Short, Dim3 as Short, sNR as Short
            sNR = CShort(NR)

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVSHORTSEXDIFF420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVSHORTSEXDIFF420(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    For Dim3 = 1 to 6
                        IFsResults("INTSFLOWS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6, Dim3, 6)) = INTSFLOWS(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("outputagprotect")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = outputagprotect(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("outputenprotect")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = outputenprotect(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HHSAVSTOCK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HHSAVSTOCK(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hhsaveneedrat")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hhsaveneedrat(Dim1)
            Next Dim1

            IFsResults("actreaon")(getDataFrameIdxFromDims(Year, FirstYear)) = actreaon

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 15
                    For Dim3 = 1 to 3
                        IFsResults("HLDALY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)) = HLDALY(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HLSMOKING")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HLSMOKING(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edqualag15m")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edqualag15m(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HLOBESITY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HLOBESITY(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ASARAWDMAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ASARAWDMAT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ASAGDPDMAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ASAGDPDMAT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ASALABDMAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ASALABDMAT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ASAPOPDMAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ASAPOPDMAT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ASAGDPCUMCHG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ASAGDPCUMCHG(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ASALABCUMCHG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ASALABCUMCHG(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ASAPOPCUMCHG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ASAPOPCUMCHG(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ASAGDPGRRB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ASAGDPGRRB(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ASALABGRRB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ASALABGRRB(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ASAPOPGRRB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ASAPOPGRRB(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ASAGDP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ASAGDP(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ASALAB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ASALAB(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ASAPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ASAPOP(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ASARAW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ASARAW(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("workageentry")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = workageentry(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("AGEOFMARRIAGE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = AGEOFMARRIAGE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ageofmarriagetfrelas")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ageofmarriagetfrelas(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("ageofmarriagem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = ageofmarriagem(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("workageretire")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = workageretire(Dim1)
            Next Dim1

            IFsResults("workageretiresv")(getDataFrameIdxFromDims(Year, FirstYear)) = workageretiresv

            IFsResults("envylchgadd")(getDataFrameIdxFromDims(Year, FirstYear)) = envylchgadd

            For Dim1 = 1 to sNR
                IFsResults("envylchgm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = envylchgm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("KAG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = KAG(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("AGDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = AGDEM(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("agconv")(getDataFrameIdxFromDims(Year, FirstYear)) = agconv

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("AGX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = AGX(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("agxvaluem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = agxvaluem(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("agxm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = agxm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("AGM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = AGM(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("agmvaluem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = agmvaluem(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("agmm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = agmm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("inputagrim")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = inputagrim(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("RETRAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = RETRAT(Dim1)
            Next Dim1

            IFsResults("elinag2")(getDataFrameIdxFromDims(Year, FirstYear)) = elinag2

            IFsResults("elinag1")(getDataFrameIdxFromDims(Year, FirstYear)) = elinag1

            For Dim1 = 1 to sNR
                IFsResults("aginvm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = aginvm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("IALK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = IALK(Dim1)
            Next Dim1

            IFsResults("eliasp2")(getDataFrameIdxFromDims(Year, FirstYear)) = eliasp2

            IFsResults("eliasp1")(getDataFrameIdxFromDims(Year, FirstYear)) = eliasp1

            For Dim1 = 1 to sNR
                IFsResults("ylmax")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ylmax(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("AGP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = AGP(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("AGLOSSPROD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = AGLOSSPROD(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    IFsResults("aglossprodm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5)) = aglossprodm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("agstkadjsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = agstkadjsw(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("AGTEC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = AGTEC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("XBILATAGR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = XBILATAGR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("agtrdadjsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = agtrdadjsw(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("AGLOSSTRANS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = AGLOSSTRANS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("aglosstransm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = aglosstransm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("elfdpr2")(getDataFrameIdxFromDims(Year, FirstYear)) = elfdpr2

            IFsResults("elfdpr1")(getDataFrameIdxFromDims(Year, FirstYear)) = elfdpr1

            IFsResults("ylmaxgr")(getDataFrameIdxFromDims(Year, FirstYear)) = ylmaxgr

            For Dim1 = 1 to sNR
                IFsResults("ylm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ylm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INAG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INAG(Dim1)
            Next Dim1

            IFsResults("agon")(getDataFrameIdxFromDims(Year, FirstYear)) = agon

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("AGXVALUE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = AGXVALUE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("AGMVALUE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = AGMVALUE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("AIMACHIQ")(getDataFrameIdxFromDims(Year, FirstYear)) = AIMACHIQ

            For Dim1 = 1 to 6
                IFsResults("AITASK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)) = AITASK(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("aiddon")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = aiddon(Dim1)
            Next Dim1

            IFsResults("aidgradpoint")(getDataFrameIdxFromDims(Year, FirstYear)) = aidgradpoint

            IFsResults("aidlpm")(getDataFrameIdxFromDims(Year, FirstYear)) = aidlpm

            For Dim1 = 1 to sNR
                IFsResults("aidlp")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = aidlp(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("aidrecadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = aidrecadd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("aidrecgovconp")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = aidrecgovconp(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("aidrecm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = aidrecm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("AIDSDRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = AIDSDRATE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("aidsdratem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = aidsdratem(Dim1)
            Next Dim1

            IFsResults("aidsdrtadvr")(getDataFrameIdxFromDims(Year, FirstYear)) = aidsdrtadvr

            For Dim1 = 1 to sNR
                IFsResults("AIDSDTHS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = AIDSDTHS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("AIDSDTHSCM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = AIDSDTHSCM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("ally")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = ally(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("thrtcoefally")(getDataFrameIdxFromDims(Year, FirstYear)) = thrtcoefally

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("ALLIANCEBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = ALLIANCEBILAT(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

            For Dim1 = 1 to 15
                For Dim2 = 1 to 3
                    IFsResults("hllongvtyalpharatio")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 15, Dim2, 3)) = hllongvtyalpharatio(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 15
                    For Dim3 = 1 to 3
                        IFsResults("hllongvtyalpha")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)) = hllongvtyalpha(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            IFsResults("mfpconvfunc")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpconvfunc

            For Dim1 = 1 to sNR
                IFsResults("druguseamphetaminem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = druguseamphetaminem(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ENVPRCHG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENVPRCHG(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ENVTEMP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENVTEMP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ENVTPCHG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENVTPCHG(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CARANN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CARANN(Dim1)
            Next Dim1

            IFsResults("femshrgr")(getDataFrameIdxFromDims(Year, FirstYear)) = femshrgr

            For Dim1 = 1 to sNR
                IFsResults("ENVYLCHG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENVYLCHG(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLHIVART")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLHIVART(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("AQUACUL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = AQUACUL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("aquaculgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = aquaculgr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("aquaculm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = aquaculm(Dim1)
            Next Dim1

            IFsResults("aquaculconv")(getDataFrameIdxFromDims(Year, FirstYear)) = aquaculconv

            For Dim1 = 1 to sNR
                IFsResults("XARMS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XARMS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("xarmsadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = xarmsadd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("xarmsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = xarmsm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MARMS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MARMS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("marmsadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = marmsadd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("marmsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = marmsm(Dim1)
            Next Dim1

            IFsResults("aimachiqm")(getDataFrameIdxFromDims(Year, FirstYear)) = aimachiqm

            For Dim1 = 1 to 6
                IFsResults("aitaskm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)) = aitaskm(Dim1)
            Next Dim1

            IFsResults("co2perm")(getDataFrameIdxFromDims(Year, FirstYear)) = co2perm

            IFsResults("CO2PPM")(getDataFrameIdxFromDims(Year, FirstYear)) = CO2PPM

            IFsResults("sanithhlifepitlatrinebasic")(getDataFrameIdxFromDims(Year, FirstYear)) = sanithhlifepitlatrinebasic

            For Dim1 = 1 to sNR
                IFsResults("hlbednetsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hlbednetsm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLBEDNETS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLBEDNETS(Dim1)
            Next Dim1

            For Dim1 = 1 to 15
                For Dim2 = 1 to 3
                    IFsResults("hllongvtybetaratio")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 15, Dim2, 3)) = hllongvtybetaratio(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 15
                    For Dim3 = 1 to 3
                        IFsResults("hllongvtybeta")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)) = hllongvtybeta(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("AIDBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = AIDBILAT(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("XARMSBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = XARMSBILAT(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

            IFsResults("bilateralsw")(getDataFrameIdxFromDims(Year, FirstYear)) = bilateralsw

            IFsResults("threatconstsw")(getDataFrameIdxFromDims(Year, FirstYear)) = threatconstsw

            IFsResults("bilateraltradeforwardlinksw")(getDataFrameIdxFromDims(Year, FirstYear)) = bilateraltradeforwardlinksw

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("XBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = XBILAT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("threattradesw")(getDataFrameIdxFromDims(Year, FirstYear)) = threattradesw

            For Dim1 = 1 to sNR
                IFsResults("BIRTHS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = BIRTHS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HLBMI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HLBMI(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("hlbmim")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = hlbmim(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTBROADCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTBROADCOST(Dim1)
            Next Dim1

            IFsResults("ictbroadcostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = ictbroadcostupper

            IFsResults("ictbroadcostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = ictbroadcostlower

            For Dim1 = 1 to sNR
                IFsResults("ictbroadcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictbroadcostm(Dim1)
            Next Dim1

            IFsResults("ictbroadlife")(getDataFrameIdxFromDims(Year, FirstYear)) = ictbroadlife

            IFsResults("ictbroadfromtelem")(getDataFrameIdxFromDims(Year, FirstYear)) = ictbroadfromtelem

            For Dim1 = 1 to sNR
                IFsResults("ICTBROADMOBILCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTBROADMOBILCOST(Dim1)
            Next Dim1

            IFsResults("ictbroadmobilcostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = ictbroadmobilcostlower

            IFsResults("ictbroadmobilcostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = ictbroadmobilcostupper

            For Dim1 = 1 to sNR
                IFsResults("ictbroadmobilcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictbroadmobilcostm(Dim1)
            Next Dim1

            IFsResults("ictbroadmobillife")(getDataFrameIdxFromDims(Year, FirstYear)) = ictbroadmobillife

            For Dim1 = 1 to sNR
                IFsResults("ictbroadtechighrt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictbroadtechighrt(Dim1)
            Next Dim1

            IFsResults("ictbroadteclowrt")(getDataFrameIdxFromDims(Year, FirstYear)) = ictbroadteclowrt

            IFsResults("ictbroadtecinflection")(getDataFrameIdxFromDims(Year, FirstYear)) = ictbroadtecinflection

            IFsResults("ictcomputerstecinflection")(getDataFrameIdxFromDims(Year, FirstYear)) = ictcomputerstecinflection

            For Dim1 = 1 to sNR
                IFsResults("ictbroadm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictbroadm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVBUSREGIND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVBUSREGIND(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVBUSCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVBUSCOST(Dim1)
            Next Dim1

            IFsResults("CO2PER")(getDataFrameIdxFromDims(Year, FirstYear)) = CO2PER

            IFsResults("wtempadd")(getDataFrameIdxFromDims(Year, FirstYear)) = wtempadd

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 4
                    IFsResults("clpcm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)) = clpcm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("clnf")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = clnf(Dim1)
            Next Dim1

            IFsResults("clsexp")(getDataFrameIdxFromDims(Year, FirstYear)) = clsexp

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 4
                    IFsResults("CLPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)) = CLPC(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CLAVAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CLAVAL(Dim1)
            Next Dim1

            IFsResults("calmax")(getDataFrameIdxFromDims(Year, FirstYear)) = calmax

            For Dim1 = 1 to sNR
                IFsResults("clpccvm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = clpccvm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("clpccvr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = clpccvr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("clpcmderm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = clpcmderm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CLPCCV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CLPCCV(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 4
                    IFsResults("clpcexovals")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)) = clpcexovals(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("calmeatm")(getDataFrameIdxFromDims(Year, FirstYear)) = calmeatm

            For Dim1 = 1 to sNR
                IFsResults("CLPCMDER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CLPCMDER(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CLNEED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CLNEED(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ECGRCAPUTGDP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ECGRCAPUTGDP(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("elcaput2")(getDataFrameIdxFromDims(Year, FirstYear)) = elcaput2

            IFsResults("elaszs")(getDataFrameIdxFromDims(Year, FirstYear)) = elaszs

            IFsResults("elcaput1")(getDataFrameIdxFromDims(Year, FirstYear)) = elcaput1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("KS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = KS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("qem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = qem(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("qeunconm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = qeunconm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("keqshr")(getDataFrameIdxFromDims(Year, FirstYear)) = keqshr

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ksadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ksadd(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ksm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ksm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("QE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = QE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to 6
                IFsResults("salpha")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)) = salpha(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("CARS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = CARS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("carsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = carsm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CARSALPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CARSALPC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CARSTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CARSTOT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("carstotm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = carstotm(Dim1)
            Next Dim1

            IFsResults("carsconv")(getDataFrameIdxFromDims(Year, FirstYear)) = carsconv

            IFsResults("carabr")(getDataFrameIdxFromDims(Year, FirstYear)) = carabr

            IFsResults("carinit")(getDataFrameIdxFromDims(Year, FirstYear)) = carinit

            IFsResults("carprein")(getDataFrameIdxFromDims(Year, FirstYear)) = carprein

            IFsResults("carfuel3")(getDataFrameIdxFromDims(Year, FirstYear)) = carfuel3

            IFsResults("carfuel2")(getDataFrameIdxFromDims(Year, FirstYear)) = carfuel2

            IFsResults("carfuel1")(getDataFrameIdxFromDims(Year, FirstYear)) = carfuel1

            IFsResults("carforst")(getDataFrameIdxFromDims(Year, FirstYear)) = carforst

            For Dim1 = 1 to sNR
                IFsResults("carbtax")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = carbtax(Dim1)
            Next Dim1

            IFsResults("wpsysgr")(getDataFrameIdxFromDims(Year, FirstYear)) = wpsysgr

            IFsResults("hltechlinc")(getDataFrameIdxFromDims(Year, FirstYear)) = hltechlinc

            IFsResults("hltechssa")(getDataFrameIdxFromDims(Year, FirstYear)) = hltechssa

            IFsResults("wpconsq")(getDataFrameIdxFromDims(Year, FirstYear)) = wpconsq

            IFsResults("wpcon")(getDataFrameIdxFromDims(Year, FirstYear)) = wpcon

            IFsResults("wptrade")(getDataFrameIdxFromDims(Year, FirstYear)) = wptrade

            IFsResults("wpindsw")(getDataFrameIdxFromDims(Year, FirstYear)) = wpindsw

            IFsResults("hlstuntchlsw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlstuntchlsw

            For Dim1 = 1 to sNR
                IFsResults("CHLMOR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CHLMOR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CHILDDTHR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CHILDDTHR(Dim1)
            Next Dim1

            IFsResults("cdmf")(getDataFrameIdxFromDims(Year, FirstYear)) = cdmf

            For Dim1 = 1 to sNR
                IFsResults("CIVDM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CIVDM(Dim1)
            Next Dim1

            IFsResults("climeconimpstk")(getDataFrameIdxFromDims(Year, FirstYear)) = climeconimpstk

            IFsResults("climeconimpperctrysw")(getDataFrameIdxFromDims(Year, FirstYear)) = climeconimpperctrysw

            IFsResults("climeconimplin")(getDataFrameIdxFromDims(Year, FirstYear)) = climeconimplin

            IFsResults("climeconimpsq")(getDataFrameIdxFromDims(Year, FirstYear)) = climeconimpsq

            IFsResults("climeconimpsw")(getDataFrameIdxFromDims(Year, FirstYear)) = climeconimpsw

            For Dim1 = 1 to sNR
                IFsResults("CDALF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CDALF(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("DRUGUSECOCAINE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = DRUGUSECOCAINE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("drugusecocainem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = drugusecocainem(Dim1)
            Next Dim1

            IFsResults("labinformcoeffbus")(getDataFrameIdxFromDims(Year, FirstYear)) = labinformcoeffbus

            IFsResults("gdpinformcoeffcorrupt")(getDataFrameIdxFromDims(Year, FirstYear)) = gdpinformcoeffcorrupt

            IFsResults("labinformcoeffed")(getDataFrameIdxFromDims(Year, FirstYear)) = labinformcoeffed

            IFsResults("labinformcoeffhhtrn")(getDataFrameIdxFromDims(Year, FirstYear)) = labinformcoeffhhtrn

            IFsResults("infinformcoefflabinf")(getDataFrameIdxFromDims(Year, FirstYear)) = infinformcoefflabinf

            IFsResults("gdpinformcoeffrand")(getDataFrameIdxFromDims(Year, FirstYear)) = gdpinformcoeffrand

            IFsResults("gdpshadowcoeffrand")(getDataFrameIdxFromDims(Year, FirstYear)) = gdpshadowcoeffrand

            IFsResults("labinformcoefftax")(getDataFrameIdxFromDims(Year, FirstYear)) = labinformcoefftax

            IFsResults("gdpinformcoefflabinf")(getDataFrameIdxFromDims(Year, FirstYear)) = gdpinformcoefflabinf

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("hlmortcdadltm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = hlmortcdadltm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("hlmortcdchldm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = hlmortcdchldm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 10
                    IFsResults("hlcomaddmort")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 10)) = hlcomaddmort(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 10
                    IFsResults("hlcomaddprev")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 10)) = hlcomaddprev(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDPRIPER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDPRIPER(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDPRIPER15TO24")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDPRIPER15TO24(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDPRIPER15TO19")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDPRIPER15TO19(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECLOWRPERTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECLOWRPERTOT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECLOWRPER20TO24")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECLOWRPER20TO24(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDPRIPERTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDPRIPERTOT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDTERPERTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDTERPERTOT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECUPPRPERTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECUPPRPERTOT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECUPPRPER20TO24")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECUPPRPER20TO24(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDPRICR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDPRICR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDTERPER25TO29")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDTERPER25TO29(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("ictcomputerstechighrt")(getDataFrameIdxFromDims(Year, FirstYear)) = ictcomputerstechighrt

            IFsResults("ictcomputersteclowrt")(getDataFrameIdxFromDims(Year, FirstYear)) = ictcomputersteclowrt

            For Dim1 = 1 to sNR
                IFsResults("hlcondomusem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hlcondomusem(Dim1)
            Next Dim1

            IFsResults("wpterdisp")(getDataFrameIdxFromDims(Year, FirstYear)) = wpterdisp

            IFsResults("confforsw")(getDataFrameIdxFromDims(Year, FirstYear)) = confforsw

            For Dim1 = 1 to sNR
                IFsResults("sfintlwaralltoginidom")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = sfintlwaralltoginidom(Dim1)
            Next Dim1

            IFsResults("retireconsumrat")(getDataFrameIdxFromDims(Year, FirstYear)) = retireconsumrat

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("CS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = CS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("cconstsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = cconstsw(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CPCTND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CPCTND(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CRETIRE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CRETIRE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("cexovals")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = cexovals(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("contiguity")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = contiguity(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CONTRUSE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CONTRUSE(Dim1)
            Next Dim1

            IFsResults("tconr")(getDataFrameIdxFromDims(Year, FirstYear)) = tconr

            For Dim1 = 1 to sNR
                IFsResults("contrusm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = contrusm(Dim1)
            Next Dim1

            IFsResults("mfpdemoc")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpdemoc

            IFsResults("mfpconflict")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpconflict

            IFsResults("mfpefree")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpefree

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("mfpeconints")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = mfpeconints(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("mfpedspn")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpedspn

            IFsResults("mfpinfrel")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpinfrel

            IFsResults("mfpinfrnt")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpinfrnt

            IFsResults("mfpenpri")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpenpri

            IFsResults("mfpfree")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpfree

            IFsResults("mfpgovcor")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpgovcor

            IFsResults("mfpgoveff")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpgoveff

            IFsResults("mfphlspn")(getDataFrameIdxFromDims(Year, FirstYear)) = mfphlspn

            IFsResults("mfpinfrindict")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpinfrindict

            IFsResults("mfpinfict")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpinfict

            IFsResults("mfplife")(getDataFrameIdxFromDims(Year, FirstYear)) = mfplife

            IFsResults("mfpiospn")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpiospn

            IFsResults("mfpedqual")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpedqual

            IFsResults("mfprandd")(getDataFrameIdxFromDims(Year, FirstYear)) = mfprandd

            IFsResults("mfpinfrrd")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpinfrrd

            IFsResults("mfpedscien")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpedscien

            IFsResults("mfpstunt")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpstunt

            IFsResults("mfpinfrte")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpinfrte

            IFsResults("mfpinfrindtrad")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpinfrindtrad

            IFsResults("mfpedvoc")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpedvoc

            IFsResults("mfpedyrs")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpedyrs

            IFsResults("mfphlyld")(getDataFrameIdxFromDims(Year, FirstYear)) = mfphlyld

            For Dim1 = 1 to sNR
                IFsResults("CPOW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CPOW(Dim1)
            Next Dim1

            IFsResults("cpowdf")(getDataFrameIdxFromDims(Year, FirstYear)) = cpowdf

            IFsResults("drcpow")(getDataFrameIdxFromDims(Year, FirstYear)) = drcpow

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("CWAR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = CWAR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("cwarbase")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = cwarbase(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("cwarf")(getDataFrameIdxFromDims(Year, FirstYear)) = cwarf

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("CWARPB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = CWARPB(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("cwarsv")(getDataFrameIdxFromDims(Year, FirstYear)) = cwarsv

            IFsResults("mfpconv2")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpconv2

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 15
                    IFsResults("hllongvtyalphaconv")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15)) = hllongvtyalphaconv(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("extmaposnconvtimeelectran")(getDataFrameIdxFromDims(Year, FirstYear)) = extmaposnconvtimeelectran

            IFsResults("extmaposnconvtimeroadpaved")(getDataFrameIdxFromDims(Year, FirstYear)) = extmaposnconvtimeroadpaved

            IFsResults("extmaposnconvtimeroad")(getDataFrameIdxFromDims(Year, FirstYear)) = extmaposnconvtimeroad

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("COOKSTOVES")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = COOKSTOVES(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("cookstovesadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = cookstovesadd(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to 4
                For Dim2 = 1 to 3
                    IFsResults("ictcybevcost")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4, Dim2, 3)) = ictcybevcost(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("LANDIRCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LANDIRCOST(Dim1)
            Next Dim1

            IFsResults("landircostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = landircostupper

            IFsResults("landircostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = landircostlower

            For Dim1 = 1 to sNR
                IFsResults("landircostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = landircostm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CLD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CLD(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("govbuscostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govbuscostm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SAMADMISSIONTOTCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SAMADMISSIONTOTCOST(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("samadmissioncost")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = samadmissioncost(Dim1)
            Next Dim1

            IFsResults("wpsovcount")(getDataFrameIdxFromDims(Year, FirstYear)) = wpsovcount

            IFsResults("envco2fert")(getDataFrameIdxFromDims(Year, FirstYear)) = envco2fert

            For Dim1 = 1 to sNR
                IFsResults("elascd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = elascd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ldcropm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ldcropm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CBR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CBR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CDR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CDR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("tgrld")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = tgrld(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CULTREG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CULTREG(Dim1)
            Next Dim1

            For Dim1 = 1 to 11
                IFsResults("cultshmp")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 11)) = cultshmp(Dim1)
            Next Dim1

            For Dim1 = 1 to 11
                IFsResults("cultshseload")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 11)) = cultshseload(Dim1)
            Next Dim1

            For Dim1 = 1 to 11
                IFsResults("cultshse")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 11)) = cultshse(Dim1)
            Next Dim1

            For Dim1 = 1 to 11
                IFsResults("cultshtsload")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 11)) = cultshtsload(Dim1)
            Next Dim1

            For Dim1 = 1 to 11
                IFsResults("cultshts")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 11)) = cultshts(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CURACT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CURACT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CURACTDATABOTTOM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CURACTDATABOTTOM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CURACTDATATOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CURACTDATATOP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CURACTNETPRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CURACTNETPRI(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CURACTNETSEC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CURACTNETSEC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictconsurplusm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictconsurplusm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTCONSURPLUS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTCONSURPLUS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictcybbenefitm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictcybbenefitm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 4
                    IFsResults("ictcybevtcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)) = ictcybevtcostm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictcybopcostadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictcybopcostadd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictcybsecspendm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictcybsecspendm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTCYBEVTCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTCYBEVTCOST(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTCYBSECSPEND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTCYBSECSPEND(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTCYBOPCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTCYBOPCOST(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTCYBBENEFITCUMNET")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTCYBBENEFITCUMNET(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTCYBBENEFIT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTCYBBENEFIT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTCYBRISKTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTCYBRISKTOT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTCYBRISKTOTCUM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTCYBRISKTOTCUM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictcybsecuritym")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictcybsecuritym(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTCYBBENEFITNET")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTCYBBENEFITNET(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTCYBBENTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTCYBBENTOT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTCYBBENTOTCUM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTCYBBENTOTCUM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("GRAMSPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = GRAMSPC(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("hlstddthsw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlstddthsw

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 15
                    For Dim3 = 1 to 3
                        IFsResults("DEATHCAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)) = DEATHCAT(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 10
                    IFsResults("HLCOMADDDTHS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 10)) = HLCOMADDDTHS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SVDTHSCONFLICT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SVDTHSCONFLICT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SVDTHSPOLICS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SVDTHSPOLICS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SVDTHSSELFHARM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SVDTHSSELFHARM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SOCIETALVIOLENCEDEATHS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SOCIETALVIOLENCEDEATHS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SVDTHSWOMENTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SVDTHSWOMENTOT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SVDTHSCHILTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SVDTHSCHILTOT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SVDTHSCONFLICTTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SVDTHSCONFLICTTOT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SVDTHSOTHERINTERTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SVDTHSOTHERINTERTOT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SVDTHSPOLICSTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SVDTHSPOLICSTOT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SVDTHSSELFHARMTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SVDTHSSELFHARMTOT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SVDTHSSOCIETALVIOLENCETOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SVDTHSSOCIETALVIOLENCETOT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("deathtrpvm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = deathtrpvm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("DEATHS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = DEATHS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SVDTHSOTHERINTERPERSON")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SVDTHSOTHERINTERPERSON(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SVDTHSWOMENCHILDREN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SVDTHSWOMENCHILDREN(Dim1)
            Next Dim1

            IFsResults("hivpeakdef")(getDataFrameIdxFromDims(Year, FirstYear)) = hivpeakdef

            IFsResults("thrtcoefcontig")(getDataFrameIdxFromDims(Year, FirstYear)) = thrtcoefcontig

            IFsResults("thrtcoefdem")(getDataFrameIdxFromDims(Year, FirstYear)) = thrtcoefdem

            IFsResults("democimpus")(getDataFrameIdxFromDims(Year, FirstYear)) = democimpus

            IFsResults("democimpoth")(getDataFrameIdxFromDims(Year, FirstYear)) = democimpoth

            IFsResults("democimpusn")(getDataFrameIdxFromDims(Year, FirstYear)) = democimpusn

            IFsResults("eldemocimp")(getDataFrameIdxFromDims(Year, FirstYear)) = eldemocimp

            IFsResults("DEMOCWVCOUNT")(getDataFrameIdxFromDims(Year, FirstYear)) = DEMOCWVCOUNT

            IFsResults("DEMOCWVDIR")(getDataFrameIdxFromDims(Year, FirstYear)) = DEMOCWVDIR

            IFsResults("democwvlen")(getDataFrameIdxFromDims(Year, FirstYear)) = democwvlen

            IFsResults("democwvmax")(getDataFrameIdxFromDims(Year, FirstYear)) = democwvmax

            IFsResults("DEMOCWAVE")(getDataFrameIdxFromDims(Year, FirstYear)) = DEMOCWAVE

            For Dim1 = 1 to sNR
                IFsResults("democm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = democm(Dim1)
            Next Dim1

            IFsResults("democconv")(getDataFrameIdxFromDims(Year, FirstYear)) = democconv

            IFsResults("dkl")(getDataFrameIdxFromDims(Year, FirstYear)) = dkl

            For Dim1 = 1 to sNR
                IFsResults("waterdesalinatedgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = waterdesalinatedgr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("waterdesalinatedm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = waterdesalinatedm(Dim1)
            Next Dim1

            IFsResults("desald")(getDataFrameIdxFromDims(Year, FirstYear)) = desald

            IFsResults("desali")(getDataFrameIdxFromDims(Year, FirstYear)) = desali

            IFsResults("dstlen")(getDataFrameIdxFromDims(Year, FirstYear)) = dstlen

            IFsResults("dstl")(getDataFrameIdxFromDims(Year, FirstYear)) = dstl

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 7
                    For Dim3 = 1 to 3
                        IFsResults("HLDIABETESRR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7, Dim3, 3)) = HLDIABETESRR(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POWER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POWER(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 15
                    For Dim3 = 1 to 3
                        IFsResults("HLYLD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)) = HLYLD(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 15
                    For Dim3 = 1 to 3
                        IFsResults("HLYLDWORK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)) = HLYLDWORK(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            IFsResults("thrtcoefdist")(getDataFrameIdxFromDims(Year, FirstYear)) = thrtcoefdist

            For Dim1 = 1 to sNR
                IFsResults("diplodistancetoeu")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = diplodistancetoeu(Dim1)
            Next Dim1

            IFsResults("govsecrskdiv")(getDataFrameIdxFromDims(Year, FirstYear)) = govsecrskdiv

            For Dim1 = 1 to sNR
                IFsResults("GOVTDEBTD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVTDEBTD(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ENSHO")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENSHO(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ENST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENST(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ginidomcnstsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ginidomcnstsw(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ginidomr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ginidomr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ginidomm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ginidomm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVREVDOM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVREVDOM(Dim1)
            Next Dim1

            IFsResults("hlgbdconvDown")(getDataFrameIdxFromDims(Year, FirstYear)) = hlgbdconvDown

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 8
                    For Dim3 = 1 to 3
                        IFsResults("EDSECLOWRDRPGRD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 8, Dim3, 3)) = EDSECLOWRDRPGRD(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 8
                    For Dim3 = 1 to 3
                        IFsResults("EDPRIDRPGRD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 8, Dim3, 3)) = EDPRIDRPGRD(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("druginitsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = druginitsw(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("drugglobalconvsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = drugglobalconvsw(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("drugglobalconv")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = drugglobalconv(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("DRUGUSEAMPHETAMINE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = DRUGUSEAMPHETAMINE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("drugusepolyindex")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = drugusepolyindex(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edprilen")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edprilen(Dim1)
            Next Dim1

            IFsResults("wpgreat1")(getDataFrameIdxFromDims(Year, FirstYear)) = wpgreat1

            IFsResults("wpgreat2")(getDataFrameIdxFromDims(Year, FirstYear)) = wpgreat2

            IFsResults("migratebalancesw")(getDataFrameIdxFromDims(Year, FirstYear)) = migratebalancesw

            IFsResults("migratesw")(getDataFrameIdxFromDims(Year, FirstYear)) = migratesw

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("CAPUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = CAPUT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("caputtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = caputtar(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ECONFREE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ECONFREE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("econfreem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = econfreem(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CLIMECONIMP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CLIMECONIMP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ECONINTEG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ECONINTEG(Dim1)
            Next Dim1

            IFsResults("edmodelsw")(getDataFrameIdxFromDims(Year, FirstYear)) = edmodelsw

            For Dim1 = 1 to 3
                IFsResults("hdiedyrsmaxun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)) = hdiedyrsmaxun(Dim1)
            Next Dim1

            For Dim1 = 1 to 3
                IFsResults("hdiedyrsminun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)) = hdiedyrsminun(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edbudgon")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edbudgon(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 4
                    IFsResults("edqtqltrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)) = edqtqltrm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to 3
                IFsResults("hdiedslemaxun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)) = hdiedslemaxun(Dim1)
            Next Dim1

            For Dim1 = 1 to 3
                IFsResults("hdiedsleminun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)) = hdiedsleminun(Dim1)
            Next Dim1

            IFsResults("edenrsecur")(getDataFrameIdxFromDims(Year, FirstYear)) = edenrsecur

            For Dim1 = 1 to sNR
                IFsResults("infraelecacctrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraelecacctrgtval(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("INFRAELECACCPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = INFRAELECACCPOP(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("INFRAELECACC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = INFRAELECACC(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("INFRAELECACCDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = INFRAELECACCDEM(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("infraelecaccsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraelecaccsetar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("infraelecaccseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraelecaccseyrtar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("infraelecaccm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = infraelecaccm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAELECADJFACT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAELECADJFACT(Dim1)
            Next Dim1

            IFsResults("infraelecaccruralcostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = infraelecaccruralcostupper

            IFsResults("infraelecaccruralcostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = infraelecaccruralcostlower

            For Dim1 = 1 to sNR
                IFsResults("infraelecaccruralcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraelecaccruralcostm(Dim1)
            Next Dim1

            IFsResults("infraelecaccurbancostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = infraelecaccurbancostupper

            IFsResults("infraelecaccurbancostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = infraelecaccurbancostlower

            For Dim1 = 1 to sNR
                IFsResults("infraelecaccurbancostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraelecaccurbancostm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAELECACCRURALCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAELECACCRURALCOST(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAELECACCURBANCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAELECACCURBANCOST(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAELECGENCAP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAELECGENCAP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAELECGENCAPDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAELECGENCAPDEM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("infraelecgencapm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraelecgencapm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAELECGENCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAELECGENCOST(Dim1)
            Next Dim1

            IFsResults("infraelecgencostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = infraelecgencostupper

            IFsResults("infraelecgencostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = infraelecgencostlower

            For Dim1 = 1 to sNR
                IFsResults("infraelecgencostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraelecgencostm(Dim1)
            Next Dim1

            IFsResults("infraelecgenlife")(getDataFrameIdxFromDims(Year, FirstYear)) = infraelecgenlife

            For Dim1 = 1 to sNR
                IFsResults("INFRAELECPROD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAELECPROD(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAELECCONSPRODRATIO")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAELECCONSPRODRATIO(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAINDICT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAINDICT(Dim1)
            Next Dim1

            IFsResults("infraelecaccrurallife")(getDataFrameIdxFromDims(Year, FirstYear)) = infraelecaccrurallife

            IFsResults("infraelecaccurbanlife")(getDataFrameIdxFromDims(Year, FirstYear)) = infraelecaccurbanlife

            For Dim1 = 1 to sNR
                IFsResults("INFRAELECTRANLOSS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAELECTRANLOSS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("infraelectranlossm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraelectranlossm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("infraelecacctrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraelecacctrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAELEC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAELEC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ENELECSHRENDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENELECSHRENDEM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("enelecshrendemm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = enelecshrendemm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("malelimprecisesw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = malelimprecisesw(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("malnelimstartyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = malnelimstartyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("malnelimtargetyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = malnelimtargetyr(Dim1)
            Next Dim1

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("EMBASSYBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = EMBASSYBILAT(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

            For Dim1 = 1 to sNR
                IFsResults("MIGRANTSOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MIGRANTSOUT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    For Dim3 = 1 to 3
                        IFsResults("LABEMPS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6, Dim3, 3)) = LABEMPS(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("ken")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = ken(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to 9
                IFsResults("lke")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9)) = lke(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("enpcoalmax")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = enpcoalmax(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ENDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENDEM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("elasde")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = elasde(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ENRGDP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENRGDP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("enrgdpgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = enrgdpgr(Dim1)
            Next Dim1

            IFsResults("elendemict")(getDataFrameIdxFromDims(Year, FirstYear)) = elendemict

            For Dim1 = 1 to sNR
                IFsResults("ENDEMSH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENDEMSH(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("endemm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = endemm(Dim1)
            Next Dim1

            IFsResults("enconv")(getDataFrameIdxFromDims(Year, FirstYear)) = enconv

            IFsResults("elasdi")(getDataFrameIdxFromDims(Year, FirstYear)) = elasdi

            For Dim1 = 1 to 9
                IFsResults("rdinr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9)) = rdinr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ENX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENX(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("enxm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = enxm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("enml")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = enml(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ENM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("enmm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = enmm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("inputenerm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = inputenerm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("ENINVT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = ENINVT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("eninvtm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = eninvtm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("elenpr2")(getDataFrameIdxFromDims(Year, FirstYear)) = elenpr2

            IFsResults("eleniprof2")(getDataFrameIdxFromDims(Year, FirstYear)) = eleniprof2

            IFsResults("elenpr")(getDataFrameIdxFromDims(Year, FirstYear)) = elenpr

            IFsResults("eleniprof")(getDataFrameIdxFromDims(Year, FirstYear)) = eleniprof

            For Dim1 = 1 to sNR
                IFsResults("eninvadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = eninvadd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("eninvm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = eninvm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("enpgasmax")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = enpgasmax(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ennoprodsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ennoprodsw(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("enpoilmax")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = enpoilmax(Dim1)
            Next Dim1

            IFsResults("eprafs")(getDataFrameIdxFromDims(Year, FirstYear)) = eprafs

            IFsResults("epra")(getDataFrameIdxFromDims(Year, FirstYear)) = epra

            For Dim1 = 1 to sNR
                IFsResults("enprix")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = enprix(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ENPRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENPRI(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("ENP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = ENP(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("ENPRR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = ENPRR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("eprodr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = eprodr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("elenpst2")(getDataFrameIdxFromDims(Year, FirstYear)) = elenpst2

            IFsResults("elenpst")(getDataFrameIdxFromDims(Year, FirstYear)) = elenpst

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("enpadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = enpadd(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("enpm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = enpm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("EPROFITR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = EPROFITR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("RESER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = RESER(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ENPCAPUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENPCAPUT(Dim1)
            Next Dim1

            IFsResults("squeez")(getDataFrameIdxFromDims(Year, FirstYear)) = squeez

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("elass")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = elass(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("XBILATENE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = XBILATENE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("qevremul")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = qevremul(Dim1)
            Next Dim1

            IFsResults("enon")(getDataFrameIdxFromDims(Year, FirstYear)) = enon

            For Dim1 = 1 to 6
                IFsResults("engel")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)) = engel(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 8
                    For Dim3 = 1 to 3
                        IFsResults("EDSECLOWRENRGGRD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 8, Dim3, 3)) = EDSECLOWRENRGGRD(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 8
                    For Dim3 = 1 to 3
                        IFsResults("EDPRIENRGGRD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 8, Dim3, 3)) = EDPRIENRGGRD(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDPRIENRG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDPRIENRG(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECLOWRENRG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECLOWRENRG(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECENRN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECENRN(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDBASENRG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDBASENRG(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDTERENRG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDTERENRG(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDPRIENRN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDPRIENRN(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECLOWRTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECLOWRTOT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECENRG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECENRG(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECUPPRTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECUPPRTOT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edpristart")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edpristart(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("TES")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = TES(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ethnic1")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ethnic1(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ethnic2")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ethnic2(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("eumembsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = eumembsw(Dim1)
            Next Dim1

            IFsResults("elerca1")(getDataFrameIdxFromDims(Year, FirstYear)) = elerca1

            IFsResults("elerca2")(getDataFrameIdxFromDims(Year, FirstYear)) = elerca2

            For Dim1 = 1 to sNR
                IFsResults("EXRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = EXRATE(Dim1)
            Next Dim1

            IFsResults("ifsexomodeltype")(getDataFrameIdxFromDims(Year, FirstYear)) = ifsexomodeltype

            For Dim1 = 1 to sNR
                IFsResults("DEMOCEXP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = DEMOCEXP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edpricostadjrural")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edpricostadjrural(Dim1)
            Next Dim1

            IFsResults("edexpslconv")(getDataFrameIdxFromDims(Year, FirstYear)) = edexpslconv

            For Dim1 = 1 to sNR
                IFsResults("edexperseclowrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edexperseclowrm(Dim1)
            Next Dim1

            IFsResults("edexppconv")(getDataFrameIdxFromDims(Year, FirstYear)) = edexppconv

            IFsResults("edqualprispndimpgov")(getDataFrameIdxFromDims(Year, FirstYear)) = edqualprispndimpgov

            IFsResults("edqualprispndimp")(getDataFrameIdxFromDims(Year, FirstYear)) = edqualprispndimp

            For Dim1 = 1 to sNR
                IFsResults("edprigoalexperon")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edprigoalexperon(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edexperprim")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edexperprim(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edprigoalexper")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edprigoalexper(Dim1)
            Next Dim1

            IFsResults("edqualsecspndimpgov")(getDataFrameIdxFromDims(Year, FirstYear)) = edqualsecspndimpgov

            IFsResults("edqualsecspndimp")(getDataFrameIdxFromDims(Year, FirstYear)) = edqualsecspndimp

            IFsResults("edexptconv")(getDataFrameIdxFromDims(Year, FirstYear)) = edexptconv

            For Dim1 = 1 to sNR
                IFsResults("edexperterm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edexperterm(Dim1)
            Next Dim1

            IFsResults("edexpsuconv")(getDataFrameIdxFromDims(Year, FirstYear)) = edexpsuconv

            For Dim1 = 1 to sNR
                IFsResults("edexpersecupprm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edexpersecupprm(Dim1)
            Next Dim1

            For Dim1 = 1 to 15
                IFsResults("hlexpcostgaptohealthel")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 15)) = hlexpcostgaptohealthel(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 2
                    IFsResults("WATRESEXPLOITRENEW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)) = WATRESEXPLOITRENEW(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 2
                    IFsResults("watresexploitrenewm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)) = watresexploitrenewm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("xcadjsw")(getDataFrameIdxFromDims(Year, FirstYear)) = xcadjsw

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("xshift")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = xshift(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("X")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = X(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("XS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = XS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("xsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = xsm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("elxpr2")(getDataFrameIdxFromDims(Year, FirstYear)) = elxpr2

            IFsResults("elxpr1")(getDataFrameIdxFromDims(Year, FirstYear)) = elxpr1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("elasxincshock")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = elasxincshock(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("elasxinc")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = elasxinc(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XRPA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XRPA(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XDEBTRPA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XDEBTRPA(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XFIRMDEBT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XFIRMDEBT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XGOVTDEBT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XGOVTDEBT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XGOVTDEBTB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XGOVTDEBTB(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XGOVTDEBTBF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XGOVTDEBTBF(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 4
                    IFsResults("EXTRASTDRATIO")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)) = EXTRASTDRATIO(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("xfdifinm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = xfdifinm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("xfdistockr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = xfdistockr(Dim1)
            Next Dim1

            IFsResults("elfdilifexp")(getDataFrameIdxFromDims(Year, FirstYear)) = elfdilifexp

            For Dim1 = 1 to sNR
                IFsResults("XFDIFIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XFDIFIN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XFDIFOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XFDIFOUT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XFDISTOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XFDISTOUT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("labfemshrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = labfemshrm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("FEMSHRLAB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = FEMSHRLAB(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hltfragepeak")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hltfragepeak(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hltfrageinit")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hltfrageinit(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hltfrpeaklevel")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hltfrpeaklevel(Dim1)
            Next Dim1

            IFsResults("hltfrconv")(getDataFrameIdxFromDims(Year, FirstYear)) = hltfrconv

            For Dim1 = 1 to sNR
                IFsResults("FIRMINC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = FIRMINC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("FIRMINCDIS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = FIRMINCDIS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("FIRMEARN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = FIRMEARN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("FIRMSAV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = FIRMSAV(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("FIRMGOVSS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = FIRMGOVSS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("FIRMTAX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = FIRMTAX(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("FIRMTAXR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = FIRMTAXR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("firmtaxrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = firmtaxrm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("AGFISHCATCH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = AGFISHCATCH(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("fishcatchm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = fishcatchm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("elasfd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = elasfd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("FISH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = FISH(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictbroadtrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictbroadtrgtval(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTBROADDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTBROADDEM(Dim1)
            Next Dim1

            IFsResults("ictbroadurimpmax")(getDataFrameIdxFromDims(Year, FirstYear)) = ictbroadurimpmax

            IFsResults("ictbroadurimpmin")(getDataFrameIdxFromDims(Year, FirstYear)) = ictbroadurimpmin

            For Dim1 = 1 to sNR
                IFsResults("ictbroadsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictbroadsetar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTBROAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTBROAD(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictbroadseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictbroadseyrtar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("aglossconsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = aglossconsm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("AGLOSSCONS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = AGLOSSCONS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("FDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = FDEM(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("FOODINSECURITY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = FOODINSECURITY(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("foodinsecuritym")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = foodinsecuritym(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("FMDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = FMDEM(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("FPRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = FPRI(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("fprimt2")(getDataFrameIdxFromDims(Year, FirstYear)) = fprimt2

            IFsResults("fprimt1")(getDataFrameIdxFromDims(Year, FirstYear)) = fprimt1

            IFsResults("fpricr2")(getDataFrameIdxFromDims(Year, FirstYear)) = fpricr2

            IFsResults("fpricr1")(getDataFrameIdxFromDims(Year, FirstYear)) = fpricr1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("FPROFITR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = FPROFITR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("FSTOCK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = FSTOCK(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("AIDREC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = AIDREC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("AID")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = AID(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("AIDDONATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = AIDDONATE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("AIDRECEIPT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = AIDRECEIPT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POPFOREIGNIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPFOREIGNIN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XFDISTOCK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XFDISTOCK(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("xfdifoutm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = xfdifoutm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("FORSAV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = FORSAV(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("FORSAVRPA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = FORSAVRPA(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POPFOREIGNOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPFOREIGNOUT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("forestm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = forestm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("FREEDOM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = FREEDOM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("freedomm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = freedomm(Dim1)
            Next Dim1

            IFsResults("govconswing")(getDataFrameIdxFromDims(Year, FirstYear)) = govconswing

            For Dim1 = 1 to sNR
                IFsResults("GDPP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GDPP(Dim1)
            Next Dim1

            IFsResults("gdpadjsw")(getDataFrameIdxFromDims(Year, FirstYear)) = gdpadjsw

            For Dim1 = 1 to sNR
                IFsResults("GDPADJ")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GDPADJ(Dim1)
            Next Dim1

            IFsResults("histgdpforce")(getDataFrameIdxFromDims(Year, FirstYear)) = histgdpforce

            For Dim1 = 1 to sNR
                IFsResults("GDPR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GDPR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("gdprext")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = gdprext(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("igdpr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = igdpr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GDPPCP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GDPPCP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GDPPCP2005")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GDPPCP2005(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("GDPPCPMF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = GDPPCPMF(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GDPPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GDPPC(Dim1)
            Next Dim1

            For Dim1 = 1 to 3
                IFsResults("hdigdppcmaxun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)) = hdigdppcmaxun(Dim1)
            Next Dim1

            IFsResults("hdigdppcmax")(getDataFrameIdxFromDims(Year, FirstYear)) = hdigdppcmax

            For Dim1 = 1 to 3
                IFsResults("hdigdppcminun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)) = hdigdppcminun(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GDPPOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GDPPOT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("armsimmaxpctmilexp")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = armsimmaxpctmilexp(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GDI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GDI(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("gemsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = gemsetar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("gemm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = gemm(Dim1)
            Next Dim1

            IFsResults("gemconv")(getDataFrameIdxFromDims(Year, FirstYear)) = gemconv

            For Dim1 = 1 to sNR
                IFsResults("gemseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = gemseyrtar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GEM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GII")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GII(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GINIDOM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GINIDOM(Dim1)
            Next Dim1

            IFsResults("glocom")(getDataFrameIdxFromDims(Year, FirstYear)) = glocom

            IFsResults("xfdiwgradd")(getDataFrameIdxFromDims(Year, FirstYear)) = xfdiwgradd

            For Dim1 = 1 to sNR
                IFsResults("globalfundsrecipients")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = globalfundsrecipients(Dim1)
            Next Dim1

            IFsResults("GINIFULL")(getDataFrameIdxFromDims(Year, FirstYear)) = GINIFULL

            IFsResults("mfpbasinc")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpbasinc

            IFsResults("mfpbasgr")(getDataFrameIdxFromDims(Year, FirstYear)) = mfpbasgr

            IFsResults("lintr")(getDataFrameIdxFromDims(Year, FirstYear)) = lintr

            IFsResults("OILGPR")(getDataFrameIdxFromDims(Year, FirstYear)) = OILGPR

            For Dim1 = 1 to sNR
                IFsResults("POWERGPI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POWERGPI(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GLOBALIZ")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GLOBALIZ(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("govriskm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govriskm(Dim1)
            Next Dim1

            IFsResults("govsecrskexp")(getDataFrameIdxFromDims(Year, FirstYear)) = govsecrskexp

            For Dim1 = 1 to sNR
                IFsResults("GOVINDTOTAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVINDTOTAL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVINDCAPAC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVINDCAPAC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("govconm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govconm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVCORRUPT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVCORRUPT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("govcorruptsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govcorruptsetar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("govcorruptseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govcorruptseyrtar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("govcorruptm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govcorruptm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVDEBT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVDEBT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("govdebtchg")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govdebtchg(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 4
                    IFsResults("GDSED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)) = GDSED(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("goveffectsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = goveffectsetar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("goveffectseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = goveffectseyrtar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVEFFECT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVEFFECT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("goveffectm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = goveffectm(Dim1)
            Next Dim1

            IFsResults("goveffconv")(getDataFrameIdxFromDims(Year, FirstYear)) = goveffconv

            For Dim1 = 1 to sNR
                IFsResults("GOVEMBED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVEMBED(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 7
                    IFsResults("GDS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)) = GDS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("GS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = GS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVEXP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVEXP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 7
                    IFsResults("gdsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)) = gdsm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 7
                    IFsResults("gdscnstsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)) = gdscnstsw(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 4
                    IFsResults("gdsedm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)) = gdsedm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("elgexpdebt2")(getDataFrameIdxFromDims(Year, FirstYear)) = elgexpdebt2

            IFsResults("elgexpdebt1")(getDataFrameIdxFromDims(Year, FirstYear)) = elgexpdebt1

            For Dim1 = 1 to sNR
                IFsResults("govexpm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govexpm(Dim1)
            Next Dim1

            IFsResults("gdsnormalize")(getDataFrameIdxFromDims(Year, FirstYear)) = gdsnormalize

            IFsResults("govfinconv")(getDataFrameIdxFromDims(Year, FirstYear)) = govfinconv

            For Dim1 = 1 to sNR
                IFsResults("GOVBAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVBAL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVINDINCLUS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVINDINCLUS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("govbusregindm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govbusregindm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVREGQUAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVREGQUAL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("govregqualsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govregqualsetar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("govregqualseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govregqualseyrtar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("govregqualm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govregqualm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HTCGRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HTCGRI(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("govrevtrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govrevtrgtval(Dim1)
            Next Dim1

            IFsResults("govrevtrgtyr")(getDataFrameIdxFromDims(Year, FirstYear)) = govrevtrgtyr

            For Dim1 = 1 to sNR
                IFsResults("GOVREV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVREV(Dim1)
            Next Dim1

            IFsResults("elgrevdebt2")(getDataFrameIdxFromDims(Year, FirstYear)) = elgrevdebt2

            For Dim1 = 1 to sNR
                IFsResults("govrevm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govrevm(Dim1)
            Next Dim1

            IFsResults("elgrevdebt1")(getDataFrameIdxFromDims(Year, FirstYear)) = elgrevdebt1

            For Dim1 = 1 to sNR
                IFsResults("GOVINDSECUR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVINDSECUR(Dim1)
            Next Dim1

            IFsResults("govindsecurconflict")(getDataFrameIdxFromDims(Year, FirstYear)) = govindsecurconflict

            IFsResults("govindsecurgovrisk")(getDataFrameIdxFromDims(Year, FirstYear)) = govindsecurgovrisk

            IFsResults("govindsecurviolence")(getDataFrameIdxFromDims(Year, FirstYear)) = govindsecurviolence

            For Dim1 = 1 to sNR
                IFsResults("govindsecurm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govindsecurm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 7
                    IFsResults("gdstrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)) = gdstrgtval(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 7
                    IFsResults("gdsbudgetconv")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)) = gdsbudgetconv(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 7
                    IFsResults("gdsbudgetprotec")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)) = gdsbudgetprotec(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 7
                    IFsResults("gdstrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)) = gdstrgtyr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("govhhpentndpctuse")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govhhpentndpctuse(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVHHPENTND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVHHPENTND(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVHHPENT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVHHPENT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("govhhtrnwelm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = govhhtrnwelm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("govhhtrnpenm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = govhhtrnpenm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECLOWRGRADS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECLOWRGRADS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDPRIGRADS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDPRIGRADS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECUPPRGRADS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECUPPRGRADS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDTERGRADS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDTERGRADS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECLOWRGRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECLOWRGRATE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECUPPRGRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECUPPRGRATE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDTERGRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDTERGRATE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GLDCAP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GLDCAP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GDP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GDP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GDPRPA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GDPRPA(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GNI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GNI(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ZS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ZS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to 6
                IFsResults("AITASKGR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)) = AITASKGR(Dim1)
            Next Dim1

            For Dim1 = 1 to 15
                IFsResults("hlcostgrowthel")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 15)) = hlcostgrowthel(Dim1)
            Next Dim1

            For Dim1 = 1 to 6
                IFsResults("mfpleadr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)) = mfpleadr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HDIFLOAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HDIFLOAT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HDI21STFIX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HDI21STFIX(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HDI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HDI(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HDIMF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HDIMF(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HDIPRE2010")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HDIPRE2010(Dim1)
            Next Dim1

            IFsResults("hltechbase")(getDataFrameIdxFromDims(Year, FirstYear)) = hltechbase

            IFsResults("hltechshift")(getDataFrameIdxFromDims(Year, FirstYear)) = hltechshift

            IFsResults("hlmodelsw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlmodelsw

            For Dim1 = 1 to sNR
                IFsResults("POWERHH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POWERHH(Dim1)
            Next Dim1

            IFsResults("cpowldcf")(getDataFrameIdxFromDims(Year, FirstYear)) = cpowldcf

            For Dim1 = 1 to sNR
                IFsResults("POWERHHMI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POWERHHMI(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HIVRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HIVRATE(Dim1)
            Next Dim1

            IFsResults("hlmorthivmalsw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlmorthivmalsw

            For Dim1 = 1 to sNR
                IFsResults("hlhivincm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hlhivincm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hlhivmortm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hlhivmortm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HIVCASES")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HIVCASES(Dim1)
            Next Dim1

            For Dim1 = 1 to 11
                IFsResults("hivculf")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 11)) = hivculf(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLHIVINC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLHIVINC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLHIVINCPREVR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLHIVINCPREVR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hivincr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hivincr(Dim1)
            Next Dim1

            IFsResults("hivmdcm")(getDataFrameIdxFromDims(Year, FirstYear)) = hivmdcm

            For Dim1 = 1 to sNR
                IFsResults("hivm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hivm(Dim1)
            Next Dim1

            IFsResults("hivtadvr")(getDataFrameIdxFromDims(Year, FirstYear)) = hivtadvr

            For Dim1 = 1 to sNR
                IFsResults("HLHIVMORT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLHIVMORT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLHIVMORTPREVR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLHIVMORTPREVR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLHIVPREV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLHIVPREV(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hlhivprevm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hlhivprevm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hivpeakyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hivpeakyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HOMICIDEINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HOMICIDEINDEX(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTCOMPUTERS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTCOMPUTERS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("C")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = C(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("CRA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = CRA(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HHINCDIS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HHINCDIS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HHINCDISPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HHINCDISPC(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HHDIVINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HHDIVINT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HHINC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HHINC(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HHINCPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HHINCPC(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HHGOVSS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HHGOVSS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HHPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HHPOP(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HHSAV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HHSAV(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HHSIZE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HHSIZE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hhsizem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hhsizem(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("hhtaxrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = hhtaxrm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HHTAXR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HHTAXR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HHTAX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HHTAX(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HHINCEARN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HHINCEARN(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HHTOTAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HHTOTAL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictcomputersm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictcomputersm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictintnethhm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictintnethhm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HPI1")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HPI1(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HTCTPI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HTCTPI(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("inputictm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = inputictm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTCYBSECURITY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTCYBSECURITY(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTSPEND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTSPEND(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictspendm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictspendm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAINDICTZ")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAINDICTZ(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("XBILATICT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = XBILATICT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("labempidleper")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = labempidleper(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("LABSIDLE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = LABSIDLE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("ximfcreditr")(getDataFrameIdxFromDims(Year, FirstYear)) = ximfcreditr

            For Dim1 = 1 to sNR
                IFsResults("XIMFCRFIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XIMFCRFIN(Dim1)
            Next Dim1

            IFsResults("ximfcrintr")(getDataFrameIdxFromDims(Year, FirstYear)) = ximfcrintr

            For Dim1 = 1 to sNR
                IFsResults("XIMFCRFOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XIMFCRFOUT(Dim1)
            Next Dim1

            IFsResults("ximfcrrepr")(getDataFrameIdxFromDims(Year, FirstYear)) = ximfcrrepr

            IFsResults("ximfcreqr")(getDataFrameIdxFromDims(Year, FirstYear)) = ximfcreqr

            For Dim1 = 1 to sNR
                IFsResults("XIMFCREDIT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XIMFCREDIT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ximfcrfinm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ximfcrfinm(Dim1)
            Next Dim1

            IFsResults("XIMFCREDITTOT")(getDataFrameIdxFromDims(Year, FirstYear)) = XIMFCREDITTOT

            For Dim1 = 1 to sNR
                IFsResults("XIMFSUBF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XIMFSUBF(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ECGRINFTEF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ECGRINFTEF(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ECGRCLIMGDP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ECGRCLIMGDP(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ECGRCLIMTEF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ECGRCLIMTEF(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 2
                    IFsResults("marriageedseclowr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)) = marriageedseclowr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 2
                    IFsResults("marriageedsecuppr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)) = marriageedsecuppr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("cpowf")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = cpowf(Dim1)
            Next Dim1

            IFsResults("mcadjsw")(getDataFrameIdxFromDims(Year, FirstYear)) = mcadjsw

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("MTARIFFTAXR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = MTARIFFTAXR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("MTARIFFREV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = MTARIFFREV(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("mtarifftaxrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = mtarifftaxrm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("M")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = M(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("MS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = MS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("msm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = msm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("elasmincshock")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = elasmincshock(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("elmpr2")(getDataFrameIdxFromDims(Year, FirstYear)) = elmpr2

            IFsResults("elmpr1")(getDataFrameIdxFromDims(Year, FirstYear)) = elmpr1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("elasminc")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = elasminc(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MRPA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MRPA(Dim1)
            Next Dim1

            IFsResults("hlincoefgr")(getDataFrameIdxFromDims(Year, FirstYear)) = hlincoefgr

            IFsResults("incpovlev")(getDataFrameIdxFromDims(Year, FirstYear)) = incpovlev

            IFsResults("wppowtran2")(getDataFrameIdxFromDims(Year, FirstYear)) = wppowtran2

            For Dim1 = 1 to sNR
                IFsResults("INDIRECTTAX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INDIRECTTAX(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INDIRECTTAXR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INDIRECTTAXR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("indirecttaxrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = indirecttaxrm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("indemm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = indemm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("INDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = INDEM(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("elagind")(getDataFrameIdxFromDims(Year, FirstYear)) = elagind

            For Dim1 = 1 to sNR
                IFsResults("INFMOR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFMOR(Dim1)
            Next Dim1

            IFsResults("infmormax")(getDataFrameIdxFromDims(Year, FirstYear)) = infmormax

            IFsResults("infmorttfrlag")(getDataFrameIdxFromDims(Year, FirstYear)) = infmorttfrlag

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("INFLUENCE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = INFLUENCE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GDPSHADOWSHR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GDPSHADOWSHR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("gdpinformshrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = gdpinformshrm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GDPINFORMSHR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GDPINFORMSHR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("LABINFORMPCNTNONINF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = LABINFORMPCNTNONINF(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("LABINFORMPCNTINF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = LABINFORMPCNTINF(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("LABINFORMSHR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LABINFORMSHR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("labinformshrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = labinformshrm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRACOSTADJFAC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRACOSTADJFAC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 16
                    IFsResults("INFRABUDDEMNEW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)) = INFRABUDDEMNEW(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("infrafinon")(getDataFrameIdxFromDims(Year, FirstYear)) = infrafinon

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 16
                    IFsResults("INFRAINVESTMAINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)) = INFRAINVESTMAINT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 16
                    IFsResults("INFRAINVESTNEW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)) = INFRAINVESTNEW(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 16
                    IFsResults("infrainvmaintpubshrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)) = infrainvmaintpubshrm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 16
                    IFsResults("infrainvnewpubshrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)) = infrainvnewpubshrm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 16
                    IFsResults("INFRAINVESTMAINTPUB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)) = INFRAINVESTMAINTPUB(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 16
                    IFsResults("INFRAINVESTNEWPUB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)) = INFRAINVESTNEWPUB(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("infrapubspndflr")(getDataFrameIdxFromDims(Year, FirstYear)) = infrapubspndflr

            IFsResults("infrapubspndflrsw")(getDataFrameIdxFromDims(Year, FirstYear)) = infrapubspndflrsw

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 16
                    IFsResults("infrabudgetprotec")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)) = infrabudgetprotec(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to 4
                IFsResults("INFRAINDELECCOMPSDI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)) = INFRAINDELECCOMPSDI(Dim1)
            Next Dim1

            For Dim1 = 1 to 4
                IFsResults("INFRAINDELECCOMPMEANI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)) = INFRAINDELECCOMPMEANI(Dim1)
            Next Dim1

            For Dim1 = 1 to 4
                IFsResults("infraindeleccompwt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)) = infraindeleccompwt(Dim1)
            Next Dim1

            For Dim1 = 1 to 4
                IFsResults("INFRAINDICTZCOMPSDI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)) = INFRAINDICTZCOMPSDI(Dim1)
            Next Dim1

            For Dim1 = 1 to 4
                IFsResults("INFRAINDICTZCOMPMEANI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)) = INFRAINDICTZCOMPMEANI(Dim1)
            Next Dim1

            For Dim1 = 1 to 4
                IFsResults("infraindictzcompwt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)) = infraindictzcompwt(Dim1)
            Next Dim1

            For Dim1 = 1 to 7
                IFsResults("INFRAINDTRADCOMPSDI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 7)) = INFRAINDTRADCOMPSDI(Dim1)
            Next Dim1

            For Dim1 = 1 to 7
                IFsResults("INFRAINDTRADCOMPMEANI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 7)) = INFRAINDTRADCOMPMEANI(Dim1)
            Next Dim1

            For Dim1 = 1 to 4
                IFsResults("INFRAINDTRANCOMPMEANI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)) = INFRAINDTRANCOMPMEANI(Dim1)
            Next Dim1

            For Dim1 = 1 to 4
                IFsResults("INFRAINDTRANCOMPSDI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)) = INFRAINDTRANCOMPSDI(Dim1)
            Next Dim1

            For Dim1 = 1 to 4
                IFsResults("infraindtrancompwt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)) = infraindtrancompwt(Dim1)
            Next Dim1

            For Dim1 = 1 to 3
                IFsResults("INFRAINDWATSANCOMPSDI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)) = INFRAINDWATSANCOMPSDI(Dim1)
            Next Dim1

            For Dim1 = 1 to 3
                IFsResults("INFRAINDWATSANCOMPMEANI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)) = INFRAINDWATSANCOMPMEANI(Dim1)
            Next Dim1

            For Dim1 = 1 to 3
                IFsResults("infraindwatsancompwt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)) = infraindwatsancompwt(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAINDELEC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAINDELEC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAINDICTDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAINDICTDEM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAINDTRAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAINDTRAD(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAINDTRADEXP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAINDTRADEXP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAINDWATSAN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAINDWATSAN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("DIPLOINGOADVOCACY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = DIPLOINGOADVOCACY(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("DIPLOINGOPROGRAM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = DIPLOINGOPROGRAM(Dim1)
            Next Dim1

            IFsResults("enprixi")(getDataFrameIdxFromDims(Year, FirstYear)) = enprixi

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDTERINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDTERINT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("gdpinformcoeffintercept")(getDataFrameIdxFromDims(Year, FirstYear)) = gdpinformcoeffintercept

            IFsResults("infinformcoeffintercept")(getDataFrameIdxFromDims(Year, FirstYear)) = infinformcoeffintercept

            IFsResults("labinformcoeffintercept")(getDataFrameIdxFromDims(Year, FirstYear)) = labinformcoeffintercept

            IFsResults("thrtcoefint")(getDataFrameIdxFromDims(Year, FirstYear)) = thrtcoefint

            IFsResults("gdpshadowcoeffintercept")(getDataFrameIdxFromDims(Year, FirstYear)) = gdpshadowcoeffintercept

            For Dim1 = 1 to sNR
                IFsResults("INTR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INTR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("xportr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = xportr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XRESERVES")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XRESERVES(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTINTNETHH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTINTNETHH(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("infranetm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infranetm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRANET")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRANET(Dim1)
            Next Dim1

            IFsResults("GINI")(getDataFrameIdxFromDims(Year, FirstYear)) = GINI

            For Dim1 = 1 to sNR
                IFsResults("I")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = I(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("IDS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = IDS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("INVS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = INVS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("idsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = idsm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("invm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = invm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("IRA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = IRA(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("IGCF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = IGCF(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MIGRANTS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MIGRANTS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("migrater")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = migrater(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("migrateinm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = migrateinm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MIGRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MIGRATE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XPORTFIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XPORTFIN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("xportfoliom")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = xportfoliom(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XPORTFOLIO")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XPORTFOLIO(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("xfdistockm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = xfdistockm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("LANDIRAREAACTUAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LANDIRAREAACTUAL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("landirareaactualm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = landirareaactualm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WATDEMPERHA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WATDEMPERHA(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("LANDIRAREAEQUIPDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LANDIRAREAEQUIPDEM(Dim1)
            Next Dim1

            IFsResults("landirlife")(getDataFrameIdxFromDims(Year, FirstYear)) = landirlife

            For Dim1 = 1 to sNR
                IFsResults("KNOWSOC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = KNOWSOC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("KNENVINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = KNENVINDEX(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("KNHCINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = KNHCINDEX(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("KNICTINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = KNICTINDEX(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("KNINNOVINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = KNINNOVINDEX(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("KNEXTINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = KNEXTINDEX(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("KNTOTALINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = KNTOTALINDEX(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("LAB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LAB(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("LABS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = LABS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    For Dim3 = 1 to 3
                        IFsResults("LABCOEFFS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6, Dim3, 3)) = LABCOEFFS(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    For Dim3 = 1 to 3
                        IFsResults("LABDEMS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6, Dim3, 3)) = LABDEMS(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            IFsResults("labmarketsw")(getDataFrameIdxFromDims(Year, FirstYear)) = labmarketsw

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("labdemsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = labdemsm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("labretagem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = labretagem(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("LAPOPR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LAPOPR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("LABPARR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = LABPARR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("labparm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = labparm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("LABSUP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = LABSUP(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("labunemprtrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = labunemprtrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    For Dim3 = 1 to 3
                        IFsResults("LABWAGES")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6, Dim3, 3)) = LABWAGES(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            IFsResults("labwageincfac")(getDataFrameIdxFromDims(Year, FirstYear)) = labwageincfac

            For Dim1 = 1 to sNR
                IFsResults("labwagesexratm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = labwagesexratm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("LABWAGE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = LABWAGE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("LABWAGEIND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LABWAGEIND(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("LABWAGESEXRAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LABWAGESEXRAT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    For Dim3 = 1 to 3
                        IFsResults("LABWAGESSHRSKL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6, Dim3, 3)) = LABWAGESSHRSKL(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("landarablepotmed")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = landarablepotmed(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("landarablepotlow")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = landarablepotlow(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("landarablepot")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = landarablepot(Dim1)
            Next Dim1

            IFsResults("landarablepotsw")(getDataFrameIdxFromDims(Year, FirstYear)) = landarablepotsw

            For Dim1 = 1 to sNR
                IFsResults("LANDAREA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LANDAREA(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    IFsResults("LD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5)) = LD(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("landirareaequipm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = landirareaequipm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("landirareasat")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = landirareasat(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("LANDIRAREAEQUIP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LANDIRAREAEQUIP(Dim1)
            Next Dim1

            IFsResults("ldwf")(getDataFrameIdxFromDims(Year, FirstYear)) = ldwf

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("LIFEXP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = LIFEXP(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("LIFEXPHLM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = LIFEXPHLM(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("hdilifemaxf")(getDataFrameIdxFromDims(Year, FirstYear)) = hdilifemaxf

            IFsResults("hdilifemax")(getDataFrameIdxFromDims(Year, FirstYear)) = hdilifemax

            For Dim1 = 1 to 3
                IFsResults("hdilifemaxun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)) = hdilifemaxun(Dim1)
            Next Dim1

            For Dim1 = 1 to 3
                IFsResults("hdilifeminun")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)) = hdilifeminun(Dim1)
            Next Dim1

            IFsResults("lifexpmin")(getDataFrameIdxFromDims(Year, FirstYear)) = lifexpmin

            For Dim1 = 1 to sNR
                IFsResults("HLCOSTLE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLCOSTLE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLCOSTLESUB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLCOSTLESUB(Dim1)
            Next Dim1

            IFsResults("hlcostleyrper")(getDataFrameIdxFromDims(Year, FirstYear)) = hlcostleyrper

            IFsResults("hlcostletime")(getDataFrameIdxFromDims(Year, FirstYear)) = hlcostletime

            For Dim1 = 1 to 6
                IFsResults("lks")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)) = lks(Dim1)
            Next Dim1

            IFsResults("nwarf")(getDataFrameIdxFromDims(Year, FirstYear)) = nwarf

            For Dim1 = 1 to sNR
                IFsResults("LIQUIDITY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LIQUIDITY(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("LIT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LIT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("FEDDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = FEDDEM(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("feddemm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = feddemm(Dim1)
            Next Dim1

            IFsResults("elglinpr")(getDataFrameIdxFromDims(Year, FirstYear)) = elglinpr

            IFsResults("livhdpro")(getDataFrameIdxFromDims(Year, FirstYear)) = livhdpro

            IFsResults("ellvhpr2")(getDataFrameIdxFromDims(Year, FirstYear)) = ellvhpr2

            IFsResults("ellvhpr1")(getDataFrameIdxFromDims(Year, FirstYear)) = ellvhpr1

            For Dim1 = 1 to sNR
                IFsResults("lvherdm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = lvherdm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("slr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = slr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("LVHERD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LVHERD(Dim1)
            Next Dim1

            IFsResults("lvcf")(getDataFrameIdxFromDims(Year, FirstYear)) = lvcf

            For Dim1 = 1 to sNR
                IFsResults("GOVCONLOCAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVCONLOCAL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 7
                    IFsResults("GDSLOCAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)) = GDSLOCAL(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVEXPLOCAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVEXPLOCAL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVREVLOCAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVREVLOCAL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edseclowrlen")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edseclowrlen(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edseclowrenrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edseclowrenrm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("EDEXPERSECLOWR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = EDEXPERSECLOWR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edseclowrdrpgrdm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edseclowrdrpgrdm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edseclowrenrgrdm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edseclowrenrgrdm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edseclowrgradgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edseclowrgradgr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edseclowrgndreqgrad")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edseclowrgndreqgrad(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edseclowrgratrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edseclowrgratrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edseclowrgram")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edseclowrgram(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edseclowrtrangr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edseclowrtrangr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edseclowrtrancap")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edseclowrtrancap(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edseclowrgndreqtran")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edseclowrgndreqtran(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edseclowrtrantrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edseclowrtrantrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edseclowrtranm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edseclowrtranm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edseclowrvocadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edseclowrvocadd(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edterpushon")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edterpushon(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hlmalariaincm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hlmalariaincm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLMALARIAINC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLMALARIAINC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLMALINCPREVR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLMALINCPREVR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLMALARIAMORT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLMALARIAMORT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLMALMORTPREVR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLMALMORTPREVR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hlmalariamortm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hlmalariamortm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLMALARIAPREV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLMALARIAPREV(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLMALPREVPOPR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLMALPREVPOPR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hlmalariaprevm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hlmalariaprevm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLCONDOMUSE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLCONDOMUSE(Dim1)
            Next Dim1

            IFsResults("malemigr")(getDataFrameIdxFromDims(Year, FirstYear)) = malemigr

            IFsResults("birthmshr")(getDataFrameIdxFromDims(Year, FirstYear)) = birthmshr

            For Dim1 = 1 to sNR
                IFsResults("MALNCHIL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MALNCHIL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MALNCHPSH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MALNCHPSH(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MALNPOPP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MALNPOPP(Dim1)
            Next Dim1

            IFsResults("malnpoppsw")(getDataFrameIdxFromDims(Year, FirstYear)) = malnpoppsw

            IFsResults("malnhldsw")(getDataFrameIdxFromDims(Year, FirstYear)) = malnhldsw

            IFsResults("hlmlnsw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlmlnsw

            For Dim1 = 1 to sNR
                IFsResults("malnm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = malnm(Dim1)
            Next Dim1

            IFsResults("malnconv")(getDataFrameIdxFromDims(Year, FirstYear)) = malnconv

            For Dim1 = 1 to sNR
                IFsResults("womeninleadershipm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = womeninleadershipm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("XBILATMAN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = XBILATMAN(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("inputmanum")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = inputmanum(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("IMARK")(getDataFrameIdxFromDims(Year, FirstYear)) = IMARK

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("inputmatem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = inputmatem(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 7
                    IFsResults("MATPOSTR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)) = MATPOSTR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("matpostradd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = matpostradd(Dim1)
            Next Dim1

            IFsResults("mprconv")(getDataFrameIdxFromDims(Year, FirstYear)) = mprconv

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("XBILATMAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = XBILATMAT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MATMORTRATIO")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MATMORTRATIO(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("matmortratiom")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = matmortratiom(Dim1)
            Next Dim1

            IFsResults("DIPLOEMBASSYMAX")(getDataFrameIdxFromDims(Year, FirstYear)) = DIPLOEMBASSYMAX

            IFsResults("diploembassymaxm")(getDataFrameIdxFromDims(Year, FirstYear)) = diploembassymaxm

            IFsResults("DIPLOIGOMAX")(getDataFrameIdxFromDims(Year, FirstYear)) = DIPLOIGOMAX

            IFsResults("diploigomaxm")(getDataFrameIdxFromDims(Year, FirstYear)) = diploigomaxm

            IFsResults("DIPLOTREATYMAX")(getDataFrameIdxFromDims(Year, FirstYear)) = DIPLOTREATYMAX

            IFsResults("diplotreatymaxm")(getDataFrameIdxFromDims(Year, FirstYear)) = diplotreatymaxm

            IFsResults("meatmax")(getDataFrameIdxFromDims(Year, FirstYear)) = meatmax

            For Dim1 = 1 to sNR
                IFsResults("elasmd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = elasmd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POPMEDAGE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPMEDAGE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hltfragestop")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hltfragestop(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MFPCOR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MFPCOR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MFPRATET")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MFPRATET(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MFPHC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MFPHC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MFPPCDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MFPPCDEM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MFPSCCONFL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MFPSCCONFL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("MFPCORS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = MFPCORS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("MFPGLOCOR")(getDataFrameIdxFromDims(Year, FirstYear)) = MFPGLOCOR

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("MFPGROWTH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = MFPGROWTH(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MFPKN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MFPKN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MFPPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MFPPC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MFPPREMIUM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MFPPREMIUM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("MFPRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = MFPRATE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MFPSC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MFPSC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MFPBAS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MFPBAS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MFPTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MFPTOT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("MIGRANTSBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = MIGRANTSBILAT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MIGRANTSFORCEDOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MIGRANTSFORCEDOUT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MIGRANTSFORCEDRETURN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MIGRANTSFORCEDRETURN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MIGRANTSIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MIGRANTSIN(Dim1)
            Next Dim1

            IFsResults("migminpoplimit")(getDataFrameIdxFromDims(Year, FirstYear)) = migminpoplimit

            For Dim1 = 1 to sNR
                IFsResults("MIGRATEIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MIGRATEIN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("migrateinr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = migrateinr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("migrateinadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = migrateinadd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MIGRATEOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MIGRATEOUT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("migrateoutr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = migrateoutr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("migrateoutadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = migrateoutadd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MILEXPSTOCK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MILEXPSTOCK(Dim1)
            Next Dim1

            IFsResults("wpmilthr")(getDataFrameIdxFromDims(Year, FirstYear)) = wpmilthr

            For Dim1 = 1 to sNR
                IFsResults("enxl")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = enxl(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictbroadmobiltrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictbroadmobiltrgtval(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictbroadmobiltrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictbroadmobiltrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTBROADMOBILDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTBROADMOBILDEM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictbroadmobilsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictbroadmobilsetar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictbroadmobiltechighrt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictbroadmobiltechighrt(Dim1)
            Next Dim1

            IFsResults("ictbroadmobilteclowrt")(getDataFrameIdxFromDims(Year, FirstYear)) = ictbroadmobilteclowrt

            IFsResults("ictbroadmobiltecinflection")(getDataFrameIdxFromDims(Year, FirstYear)) = ictbroadmobiltecinflection

            For Dim1 = 1 to sNR
                IFsResults("ICTBROADMOBIL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTBROADMOBIL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictbroadmobilseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictbroadmobilseyrtar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictbroadmobilm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictbroadmobilm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTMOBILCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTMOBILCOST(Dim1)
            Next Dim1

            IFsResults("ictmobilcostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = ictmobilcostupper

            IFsResults("ictmobilcostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = ictmobilcostlower

            For Dim1 = 1 to sNR
                IFsResults("ictmobilcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictmobilcostm(Dim1)
            Next Dim1

            IFsResults("ictmobillife")(getDataFrameIdxFromDims(Year, FirstYear)) = ictmobillife

            For Dim1 = 1 to sNR
                IFsResults("ICTMOBILDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTMOBILDEM(Dim1)
            Next Dim1

            IFsResults("ictmobilsaturation")(getDataFrameIdxFromDims(Year, FirstYear)) = ictmobilsaturation

            IFsResults("ictmobilsetar")(getDataFrameIdxFromDims(Year, FirstYear)) = ictmobilsetar

            For Dim1 = 1 to sNR
                IFsResults("ICTMOBIL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTMOBIL(Dim1)
            Next Dim1

            IFsResults("ictmobilseyrtar")(getDataFrameIdxFromDims(Year, FirstYear)) = ictmobilseyrtar

            For Dim1 = 1 to sNR
                IFsResults("ictmobiltechighrt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictmobiltechighrt(Dim1)
            Next Dim1

            IFsResults("ictmobilteclowrt")(getDataFrameIdxFromDims(Year, FirstYear)) = ictmobilteclowrt

            IFsResults("ictmobiltecinflection")(getDataFrameIdxFromDims(Year, FirstYear)) = ictmobiltecinflection

            IFsResults("MODYEARI")(getDataFrameIdxFromDims(Year, FirstYear)) = MODYEARI

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 15
                    IFsResults("HLCOSTMORB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15)) = HLCOSTMORB(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to 15
                IFsResults("hlmorbtomortgthport")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 15)) = hlmorbtomortgthport(Dim1)
            Next Dim1

            IFsResults("hlmorbconv")(getDataFrameIdxFromDims(Year, FirstYear)) = hlmorbconv

            IFsResults("elhlmortspn")(getDataFrameIdxFromDims(Year, FirstYear)) = elhlmortspn

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 15
                    IFsResults("hlmortconstsw")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15)) = hlmortconstsw(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 15
                    IFsResults("HLCOSTMORT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15)) = HLCOSTMORT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("hlmortmodsw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlmortmodsw

            For Dim1 = 1 to sNR
                IFsResults("MORTMG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MORTMG(Dim1)
            Next Dim1

            IFsResults("elashc")(getDataFrameIdxFromDims(Year, FirstYear)) = elashc

            IFsResults("hlmorttranssw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlmorttranssw

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 15
                    IFsResults("hlmortm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15)) = hlmortm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("mortm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = mortm(Dim1)
            Next Dim1

            IFsResults("hlscaleconvdown")(getDataFrameIdxFromDims(Year, FirstYear)) = hlscaleconvdown

            IFsResults("mortconv")(getDataFrameIdxFromDims(Year, FirstYear)) = mortconv

            IFsResults("extmaposnelectran")(getDataFrameIdxFromDims(Year, FirstYear)) = extmaposnelectran

            IFsResults("extmaposnenelecshr")(getDataFrameIdxFromDims(Year, FirstYear)) = extmaposnenelecshr

            IFsResults("extmaposnroadpaved")(getDataFrameIdxFromDims(Year, FirstYear)) = extmaposnroadpaved

            IFsResults("extmaposnroad")(getDataFrameIdxFromDims(Year, FirstYear)) = extmaposnroad

            IFsResults("extmafuncelectran")(getDataFrameIdxFromDims(Year, FirstYear)) = extmafuncelectran

            IFsResults("extmafuncroadpaved")(getDataFrameIdxFromDims(Year, FirstYear)) = extmafuncroadpaved

            IFsResults("extmafuncroad")(getDataFrameIdxFromDims(Year, FirstYear)) = extmafuncroad

            For Dim1 = 1 to sNR
                IFsResults("mfpadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = mfpadd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("igdprcor")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = igdprcor(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("povnational")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = povnational(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SAVINGS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SAVINGS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SAVINGSRPA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SAVINGSRPA(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("NSNARAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = NSNARAT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("nsnaratm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = nsnaratm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("workageretirendpctuse")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = workageretirendpctuse(Dim1)
            Next Dim1

            For Dim1 = 1 to 9
                For Dim2 = 1 to 3
                    IFsResults("swingstsdem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9, Dim2, 3)) = swingstsdem(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("NEONATMOR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = NEONATMOR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("neonatmorm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = neonatmorm(Dim1)
            Next Dim1

            IFsResults("CARFOROTH")(getDataFrameIdxFromDims(Year, FirstYear)) = CARFOROTH

            IFsResults("carannwi")(getDataFrameIdxFromDims(Year, FirstYear)) = carannwi

            For Dim1 = 1 to sNR
                IFsResults("XNFA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XNFA(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XNIIP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XNIIP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("NUMNWP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = NUMNWP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("NUMNWPGR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = NUMNWPGR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("GOVHHTRNWEL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = GOVHHTRNWEL(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("nsdiv")(getDataFrameIdxFromDims(Year, FirstYear)) = nsdiv

            IFsResults("NSGAPA")(getDataFrameIdxFromDims(Year, FirstYear)) = NSGAPA

            IFsResults("NSGAPR")(getDataFrameIdxFromDims(Year, FirstYear)) = NSGAPR

            IFsResults("NSGAPRTB")(getDataFrameIdxFromDims(Year, FirstYear)) = NSGAPRTB

            IFsResults("NSPQLI")(getDataFrameIdxFromDims(Year, FirstYear)) = NSPQLI

            IFsResults("elndpow")(getDataFrameIdxFromDims(Year, FirstYear)) = elndpow

            For Dim1 = 1 to sNR
                IFsResults("nmilf")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = nmilf(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("NPOW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = NPOW(Dim1)
            Next Dim1

            IFsResults("drnpow")(getDataFrameIdxFromDims(Year, FirstYear)) = drnpow

            IFsResults("npowf")(getDataFrameIdxFromDims(Year, FirstYear)) = npowf

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("NWARPB")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = NWARPB(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("nwarsv")(getDataFrameIdxFromDims(Year, FirstYear)) = nwarsv

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("NWAR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = NWAR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("DIPLOEMBASSY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = DIPLOEMBASSY(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("diploembassym")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = diploembassym(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 2
                    IFsResults("MARRIEDFEMPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)) = MARRIEDFEMPOP(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("numnwpgrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = numnwpgrm(Dim1)
            Next Dim1

            IFsResults("numnwplim")(getDataFrameIdxFromDims(Year, FirstYear)) = numnwplim

            For Dim1 = 1 to sNR
                IFsResults("GOVNUCWP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVNUCWP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("govnucwpadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = govnucwpadd(Dim1)
            Next Dim1

            IFsResults("hlobsw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlobsw

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("hlobesitym")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = hlobesitym(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POPEDSECLOWR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POPEDSECLOWR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POPEDSECLOWRGRAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POPEDSECLOWRGRAD(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POPEDPRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POPEDPRI(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POPEDPRIINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POPEDPRIINT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POPEDPRIGRAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POPEDPRIGRAD(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POPEDTER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POPEDTER(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POPEDTERGRAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POPEDTERGRAD(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POPEDSECUPPR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POPEDSECUPPR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POPEDSECUPPRGRAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POPEDSECUPPRGRAD(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("oilpcthresw")(getDataFrameIdxFromDims(Year, FirstYear)) = oilpcthresw

            For Dim1 = 1 to sNR
                IFsResults("diploingoadvocacym")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = diploingoadvocacym(Dim1)
            Next Dim1

            IFsResults("diploingoadvocacyconv")(getDataFrameIdxFromDims(Year, FirstYear)) = diploingoadvocacyconv

            For Dim1 = 1 to sNR
                IFsResults("diploingoprogramm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = diploingoprogramm(Dim1)
            Next Dim1

            IFsResults("diploingoprogramconv")(getDataFrameIdxFromDims(Year, FirstYear)) = diploingoprogramconv

            For Dim1 = 1 to sNR
                IFsResults("DRUGUSEOPIATES")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = DRUGUSEOPIATES(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("druguseprescriptopioidm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = druguseprescriptopioidm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("druguseopiatesm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = druguseopiatesm(Dim1)
            Next Dim1

            IFsResults("hlcostotherrattarget")(getDataFrameIdxFromDims(Year, FirstYear)) = hlcostotherrattarget

            For Dim1 = 1 to sNR
                IFsResults("HLCOSTOTHER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLCOSTOTHER(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("migrateoutm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = migrateoutm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("xportstoutm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = xportstoutm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XPORTFOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XPORTFOUT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XPORTSTOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XPORTSTOUT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("xfdistoutm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = xfdistoutm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAROADPAVEDPCNT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAROADPAVEDPCNT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAROADPAVEDPCNTDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAROADPAVEDPCNTDEM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hivpeakr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hivpeakr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("GOVHHTRNPEN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = GOVHHTRNPEN(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WOMENINLEADERSHIP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WOMENINLEADERSHIP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV15PCT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV15PCT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV15PCT32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV15PCT32(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV15PCT365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV15PCT365(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV15PCT420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV15PCT420(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV15PCT55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV15PCT55(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV15PCT685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV15PCT685(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV15PCT830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV15PCT830(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV25PCT32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV25PCT32(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV25PCT365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV25PCT365(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV25PCT420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV25PCT420(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV25PCT55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV25PCT55(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV25PCT685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV25PCT685(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV25PCT830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV25PCT830(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV25PCTNPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV25PCTNPL(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV25PCT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV25PCT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTINTNET")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTINTNET(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POV15PCTNPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POV15PCTNPL(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("HHINCDISPERM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = HHINCDISPERM(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("PERSCON")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = PERSCON(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictindexm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictindexm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ICTINDEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ICTINDEX(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("PQLI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = PQLI(Dim1)
            Next Dim1

            IFsResults("ellabwage2")(getDataFrameIdxFromDims(Year, FirstYear)) = ellabwage2

            IFsResults("ellabwage1")(getDataFrameIdxFromDims(Year, FirstYear)) = ellabwage1

            IFsResults("elwageunemp2")(getDataFrameIdxFromDims(Year, FirstYear)) = elwageunemp2

            IFsResults("elwageunemp1")(getDataFrameIdxFromDims(Year, FirstYear)) = elwageunemp1

            IFsResults("prdthreshold")(getDataFrameIdxFromDims(Year, FirstYear)) = prdthreshold

            For Dim1 = 1 to sNR
                IFsResults("PRDSEC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = PRDSEC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("DEMOCPOLITY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = DEMOCPOLITY(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("democpolitysetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = democpolitysetar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("DEMOCPOLYARCHY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = DEMOCPOLYARCHY(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("democpolyarchyadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = democpolyarchyadd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("democpolyarchym")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = democpolyarchym(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POPGT65")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPGT65(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POPLE15")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPLE15(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POPPREWORK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPPREWORK(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POP15TO65")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POP15TO65(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POPPOVAG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POPPOVAG(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hlhivartm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hlhivartm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("minpcgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = minpcgr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POPR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POPRURAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPRURAL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POPRETIRED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPRETIRED(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECPER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECPER(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECPER15TO24")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECPER15TO24(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECPER20TO24")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECPER20TO24(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDTERPER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDTERPER(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POP0TO5")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POP0TO5(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POP15ANDABOVE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POP15ANDABOVE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POP25ANDABOVE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POP25ANDABOVE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POPFEM15TO24")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPFEM15TO24(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POPFOREIGN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPFOREIGN(Dim1)
            Next Dim1

            IFsResults("xportwgrm")(getDataFrameIdxFromDims(Year, FirstYear)) = xportwgrm

            IFsResults("wpthrwar")(getDataFrameIdxFromDims(Year, FirstYear)) = wpthrwar

            For Dim1 = 1 to sNR
                IFsResults("POTSUPRAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POTSUPRAT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INCOMELT125CS2005")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INCOMELT125CS2005(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INCOMELT110LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INCOMELT110LN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INCOMELT125LN2005")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INCOMELT125LN2005(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INCOMELT190LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INCOMELT190LN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INCOMELT200CS2005")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INCOMELT200CS2005(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INCOMELT200LN2005")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INCOMELT200LN2005(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INCOMELT215LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INCOMELT215LN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POVEXTREME")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POVEXTREME(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INCOMELT310LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INCOMELT310LN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INCOMELT320LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INCOMELT320LN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INCOMELT365LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INCOMELT365LN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POVMIDLOW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POVMIDLOW(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INCOMELT550LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INCOMELT550LN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INCOMELT685LN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INCOMELT685LN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POVMIDHIGH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POVMIDHIGH(Dim1)
            Next Dim1

            IFsResults("povagesexgeneric")(getDataFrameIdxFromDims(Year, FirstYear)) = povagesexgeneric

            IFsResults("povagesexdynamic")(getDataFrameIdxFromDims(Year, FirstYear)) = povagesexdynamic

            IFsResults("povagesexnormstatic")(getDataFrameIdxFromDims(Year, FirstYear)) = povagesexnormstatic

            For Dim1 = 1 to sNR
                IFsResults("POVGAPNPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POVGAPNPL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POVGAP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POVGAP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POVGAP32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POVGAP32(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POVGAP365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POVGAP365(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POVGAP420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POVGAP420(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POVGAP55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POVGAP55(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POVGAP685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POVGAP685(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POVGAP830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POVGAP830(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVGNDRATIO")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVGNDRATIO(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVGNDRATIO32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVGNDRATIO32(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVGNDRATIO365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVGNDRATIO365(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVGNDRATIO420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVGNDRATIO420(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVGNDRATIO55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVGNDRATIO55(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVGNDRATIO685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVGNDRATIO685(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVGNDRATIO830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVGNDRATIO830(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVGNDRATIONPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVGNDRATIONPL(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVCOUNT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVCOUNT(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVCOUNT32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVCOUNT32(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVCOUNT365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVCOUNT365(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVCOUNT420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVCOUNT420(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVCOUNT55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVCOUNT55(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVCOUNT685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVCOUNT685(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVCOUNT830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVCOUNT830(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVCOUNTNPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVCOUNTNPL(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVRATE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVRATE(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVRATE32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVRATE32(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVRATE365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVRATE365(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVRATE420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVRATE420(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVRATE55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVRATE55(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVRATE685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVRATE685(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVRATE830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVRATE830(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVRATENPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVRATENPL(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVSHORT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVSHORT(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVSHORT32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVSHORT32(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVSHORT365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVSHORT365(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVSHORT420")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVSHORT420(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVSHORT55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVSHORT55(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVSHORT685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVSHORT685(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVSHORT830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVSHORT830(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    For Dim3 = 1 to 3
                        IFsResults("POVSHORTNPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11, Dim3, 3)) = POVSHORTNPL(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVSHORTSEXDIFF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVSHORTSEXDIFF(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVSHORTSEXDIFF32")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVSHORTSEXDIFF32(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVSHORTSEXDIFF365")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVSHORTSEXDIFF365(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVSHORTSEXDIFF55")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVSHORTSEXDIFF55(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVSHORTSEXDIFF685")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVSHORTSEXDIFF685(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVSHORTSEXDIFF830")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVSHORTSEXDIFF830(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 11
                    IFsResults("POVSHORTSEXDIFFNPL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 11)) = POVSHORTSEXDIFFNPL(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("povagesexagratnat")(getDataFrameIdxFromDims(Year, FirstYear)) = povagesexagratnat

            For Dim1 = 1 to sNR
                IFsResults("INCOMELTNPLLN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INCOMELTNPLLN(Dim1)
            Next Dim1

            IFsResults("wpconmax")(getDataFrameIdxFromDims(Year, FirstYear)) = wpconmax

            IFsResults("wpconmin")(getDataFrameIdxFromDims(Year, FirstYear)) = wpconmin

            IFsResults("PCONGREATEU")(getDataFrameIdxFromDims(Year, FirstYear)) = PCONGREATEU

            IFsResults("PCONGREATEUF")(getDataFrameIdxFromDims(Year, FirstYear)) = PCONGREATEUF

            IFsResults("PCONSYS")(getDataFrameIdxFromDims(Year, FirstYear)) = PCONSYS

            IFsResults("PCONGREAT")(getDataFrameIdxFromDims(Year, FirstYear)) = PCONGREAT

            IFsResults("PCONGREATF")(getDataFrameIdxFromDims(Year, FirstYear)) = PCONGREATF

            IFsResults("wppowtran1")(getDataFrameIdxFromDims(Year, FirstYear)) = wppowtran1

            IFsResults("thrtcoefpwr")(getDataFrameIdxFromDims(Year, FirstYear)) = thrtcoefpwr

            For Dim1 = 1 to sNR
                IFsResults("PPPCONV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = PPPCONV(Dim1)
            Next Dim1

            IFsResults("co2prein")(getDataFrameIdxFromDims(Year, FirstYear)) = co2prein

            For Dim1 = 1 to sNR
                IFsResults("CPREWORK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CPREWORK(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("DRUGUSEPRESCRIPTOPIOID")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = DRUGUSEPRESCRIPTOPIOID(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLDIABPREV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLDIABPREV(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLDIABIGT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLDIABIGT(Dim1)
            Next Dim1

            IFsResults("encartpp")(getDataFrameIdxFromDims(Year, FirstYear)) = encartpp

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("PRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = PRI(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("elprst2")(getDataFrameIdxFromDims(Year, FirstYear)) = elprst2

            IFsResults("elprst1")(getDataFrameIdxFromDims(Year, FirstYear)) = elprst1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edqualpriallm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edqualpriallm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edprienrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edprienrm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("EDEXPERPRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = EDEXPERPRI(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edpridrpgrdm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edpridrpgrdm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edprienrgrdm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edprienrgrdm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edprigndreqintn")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edprigndreqintn(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDPRIINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDPRIINT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDPRIINTN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDPRIINTN(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDPRIINTNEXT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDPRIINTNEXT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edqualprimathm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edqualprimathm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDQUALPRIALL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDQUALPRIALL(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDQUALPRIMATH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDQUALPRIMATH(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDQUALPRIREAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDQUALPRIREAD(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDQUALPRISCI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDQUALPRISCI(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edpriintngr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edpriintngr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edpriintntrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edpriintntrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edpriintnm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edpriintnm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edqualprireadm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edqualprireadm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edqualpriscim")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edqualpriscim(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edprisurgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edprisurgr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edprigndreqsur")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edprigndreqsur(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edprisurtrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edprisurtrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edprisurm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edprisurm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDPRITOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDPRITOT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLEXPENDPRIV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLEXPENDPRIV(Dim1)
            Next Dim1

            For Dim1 = 1 to 4
                For Dim2 = 1 to 3
                    IFsResults("ictcybevprob")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4, Dim2, 3)) = ictcybevprob(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("PFD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = PFD(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("prodtf")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = prodtf(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("AGPMILKEGGS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = AGPMILKEGGS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("tefinfadjm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = tefinfadjm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("tfpshockadjshr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = tfpshockadjshr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("protecm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = protecm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 4
                    IFsResults("PROTEINPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 4)) = PROTEINPC(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 16
                    IFsResults("INFRABUDDEMMNT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 16)) = INFRABUDDEMMNT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("EDPRIPTR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = EDPRIPTR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDQUALAG15")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDQUALAG15(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDQUALAG15PRI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDQUALAG15PRI(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDQUALAG15SEC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDQUALAG15SEC(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("randdexpm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = randdexpm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("RANDDEXP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = RANDDEXP(Dim1)
            Next Dim1

            IFsResults("hwf")(getDataFrameIdxFromDims(Year, FirstYear)) = hwf

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("rdm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = rdm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to 9
                IFsResults("rdi")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9)) = rdi(Dim1)
            Next Dim1

            IFsResults("hlcostotherrattargetconv")(getDataFrameIdxFromDims(Year, FirstYear)) = hlcostotherrattargetconv

            IFsResults("xfdioutf")(getDataFrameIdxFromDims(Year, FirstYear)) = xfdioutf

            IFsResults("wpally")(getDataFrameIdxFromDims(Year, FirstYear)) = wpally

            For Dim1 = 1 to sNR
                IFsResults("POPREFUGEE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPREFUGEE(Dim1)
            Next Dim1

            IFsResults("samrrsw")(getDataFrameIdxFromDims(Year, FirstYear)) = samrrsw

            IFsResults("hlproxdrvrrsw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlproxdrvrrsw

            For Dim1 = 1 to sNR
                IFsResults("XWORKREMIT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XWORKREMIT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XWORKREMITIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XWORKREMITIN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XWORKREMITOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XWORKREMITOUT(Dim1)
            Next Dim1

            IFsResults("renewtoenexsw")(getDataFrameIdxFromDims(Year, FirstYear)) = renewtoenexsw

            IFsResults("elenrenict")(getDataFrameIdxFromDims(Year, FirstYear)) = elenrenict

            For Dim1 = 1 to sNR
                IFsResults("qevrethr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = qevrethr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 2
                    IFsResults("waterrenewconverge")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)) = waterrenewconverge(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("repayrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = repayrm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HHRETIRENEED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HHRETIRENEED(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HHSAVNEEDSTOCK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HHSAVNEEDSTOCK(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WATERRESFOSSIL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WATERRESFOSSIL(Dim1)
            Next Dim1

            IFsResults("hlpm2pt5sw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlpm2pt5sw

            IFsResults("envpm25hldsw")(getDataFrameIdxFromDims(Year, FirstYear)) = envpm25hldsw

            For Dim1 = 1 to sNR
                IFsResults("envpm2pt5m")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = envpm2pt5m(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("resor")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = resor(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("resorm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = resorm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("resoruncon")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = resoruncon(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("resorunconm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = resorunconm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("wpcontiguity")(getDataFrameIdxFromDims(Year, FirstYear)) = wpcontiguity

            IFsResults("wpcommun")(getDataFrameIdxFromDims(Year, FirstYear)) = wpcommun

            IFsResults("wpdemdist")(getDataFrameIdxFromDims(Year, FirstYear)) = wpdemdist

            IFsResults("wpdemmin")(getDataFrameIdxFromDims(Year, FirstYear)) = wpdemmin

            For Dim1 = 1 to sNR
                IFsResults("WORKAGERETIREND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WORKAGERETIREND(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HHSAVSURSTOCK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HHSAVSURSTOCK(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVRISK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVRISK(Dim1)
            Next Dim1

            IFsResults("infraroadpavedcostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = infraroadpavedcostupper

            IFsResults("infraroadpavedcostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = infraroadpavedcostlower

            For Dim1 = 1 to sNR
                IFsResults("INFRAROADPAVEDCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAROADPAVEDCOST(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("infraroadpavedcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraroadpavedcostm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAROADUNPAVEDCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAROADUNPAVEDCOST(Dim1)
            Next Dim1

            IFsResults("infraroadunpavedcostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = infraroadunpavedcostupper

            IFsResults("infraroadunpavedcostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = infraroadunpavedcostlower

            For Dim1 = 1 to sNR
                IFsResults("infraroadunpavedcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraroadunpavedcostm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAROAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAROAD(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAROADDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAROADDEM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("infraroadpavedpcntm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraroadpavedpcntm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("infraroadm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraroadm(Dim1)
            Next Dim1

            IFsResults("infraroadpavedlife")(getDataFrameIdxFromDims(Year, FirstYear)) = infraroadpavedlife

            For Dim1 = 1 to sNR
                IFsResults("INFRAROADRAI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAROADRAI(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAROADRAIDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAROADRAIDEM(Dim1)
            Next Dim1

            IFsResults("infraroadunpavedlife")(getDataFrameIdxFromDims(Year, FirstYear)) = infraroadunpavedlife

            For Dim1 = 1 to sNR
                IFsResults("infraroadraisetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraroadraisetar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("infraroadraitrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraroadraitrgtval(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("infraroadraiseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraroadraiseyrtar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("infraroadraitrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infraroadraitrgtyr(Dim1)
            Next Dim1

            IFsResults("sanithhlifepitlatrinetrad")(getDataFrameIdxFromDims(Year, FirstYear)) = sanithhlifepitlatrinetrad

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("WATSAFE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = WATSAFE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("watsafetrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = watsafetrgtval(Dim1)
            Next Dim1

            IFsResults("watsafhldsw")(getDataFrameIdxFromDims(Year, FirstYear)) = watsafhldsw

            For Dim1 = 1 to sNR
                IFsResults("watsafenoconsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = watsafenoconsetar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("watsafenoconseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = watsafenoconseyrtar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("watsafem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = watsafem(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("samtomalnchpsw")(getDataFrameIdxFromDims(Year, FirstYear)) = samtomalnchpsw

            For Dim1 = 1 to sNR
                IFsResults("samadmissionadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = samadmissionadd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("samadmissionm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = samadmissionm(Dim1)
            Next Dim1

            IFsResults("samadmissionsw")(getDataFrameIdxFromDims(Year, FirstYear)) = samadmissionsw

            For Dim1 = 1 to sNR
                IFsResults("samavertedm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = samavertedm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SAMADMISSION")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SAMADMISSION(Dim1)
            Next Dim1

            IFsResults("samavertedconvyr")(getDataFrameIdxFromDims(Year, FirstYear)) = samavertedconvyr

            IFsResults("malnchpsamtostunt")(getDataFrameIdxFromDims(Year, FirstYear)) = malnchpsamtostunt

            For Dim1 = 1 to sNR
                IFsResults("MALNCHPSAMINC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MALNCHPSAMINC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SAMAVERTED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SAMAVERTED(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("SANITATION")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = SANITATION(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("SANITATIONDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = SANITATIONDEM(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("sanitationtrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = sanitationtrgtval(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SANITATIONCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SANITATIONCOST(Dim1)
            Next Dim1

            IFsResults("sanitationcostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = sanitationcostupper

            IFsResults("sanitationcostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = sanitationcostlower

            For Dim1 = 1 to sNR
                IFsResults("sanitationcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = sanitationcostm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("sanitbudgetprotec")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = sanitbudgetprotec(Dim1)
            Next Dim1

            IFsResults("sanithldsw")(getDataFrameIdxFromDims(Year, FirstYear)) = sanithldsw

            IFsResults("sanitationimpcostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = sanitationimpcostlower

            IFsResults("sanitationimplife")(getDataFrameIdxFromDims(Year, FirstYear)) = sanitationimplife

            IFsResults("sanitationimpcostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = sanitationimpcostupper

            For Dim1 = 1 to sNR
                IFsResults("sanitationimpcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = sanitationimpcostm(Dim1)
            Next Dim1

            IFsResults("sanitationlife")(getDataFrameIdxFromDims(Year, FirstYear)) = sanitationlife

            IFsResults("sanithhmodel2sw")(getDataFrameIdxFromDims(Year, FirstYear)) = sanithhmodel2sw

            For Dim1 = 1 to sNR
                IFsResults("sanitnoconsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = sanitnoconsetar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("sanitnoconseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = sanitnoconseyrtar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("sanithhm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = sanithhm(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("sanithhbasictrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = sanithhbasictrgtval(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("sanithhbasictrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = sanithhbasictrgtyr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("sanithhsafetrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = sanithhsafetrgtyr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("sanithhsafetrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = sanithhsafetrgtval(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("sanithhcostlowershr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = sanithhcostlowershr(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("SANITHHCOSTHIGHER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = SANITHHCOSTHIGHER(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("SANITHHCOSTLOWER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = SANITHHCOSTLOWER(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("SANITHHINVESTCAP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = SANITHHINVESTCAP(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("SANITHHINVESTMAINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = SANITHHINVESTMAINT(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("SANITHHPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = SANITHHPOP(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("SANITHHDEMPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = SANITHHDEMPOP(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("SANITHH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = SANITHH(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("SANITHHDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = SANITHHDEM(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("sanitationexovals")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = sanitationexovals(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("sanitationm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = sanitationm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SANITATIONIMPCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SANITATIONIMPCOST(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("savingsdiscountrt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = savingsdiscountrt(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("cpctnduse")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = cpctnduse(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDYRSSLE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDYRSSLE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edqualsecmathm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edqualsecmathm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edqualsecallm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edqualsecallm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edqualsecreadm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edqualsecreadm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edqualsecscim")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edqualsecscim(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("EDEXPERSEC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = EDEXPERSEC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDQUALSECALL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDQUALSECALL(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDQUALSECMATH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDQUALSECMATH(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDQUALSECREAD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDQUALSECREAD(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDQUALSECSCI")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDQUALSECSCI(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECTOT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("elinst2")(getDataFrameIdxFromDims(Year, FirstYear)) = elinst2

            IFsResults("elinst1")(getDataFrameIdxFromDims(Year, FirstYear)) = elinst1

            IFsResults("sanithhlifesst")(getDataFrameIdxFromDims(Year, FirstYear)) = sanithhlifesst

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("inputservm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = inputservm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("XBILATSER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = XBILATSER(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MALNCHPSAM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MALNCHPSAM(Dim1)
            Next Dim1

            IFsResults("malnchpsamadj")(getDataFrameIdxFromDims(Year, FirstYear)) = malnchpsamadj

            For Dim1 = 1 to sNR
                IFsResults("malnchpsamm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = malnchpsamm(Dim1)
            Next Dim1

            IFsResults("gdpshadowcoeffcorrupt")(getDataFrameIdxFromDims(Year, FirstYear)) = gdpshadowcoeffcorrupt

            IFsResults("gdpshadowcoefflabinf")(getDataFrameIdxFromDims(Year, FirstYear)) = gdpshadowcoefflabinf

            For Dim1 = 1 to sNR
                IFsResults("gdpshadowshrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = gdpshadowshrm(Dim1)
            Next Dim1

            IFsResults("gdpshadowon")(getDataFrameIdxFromDims(Year, FirstYear)) = gdpshadowon

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 2
                    IFsResults("MARRIEDFEMSHR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)) = MARRIEDFEMSHR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 2
                    IFsResults("marriedfemshrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)) = marriedfemshrm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("EDTERGRSCIEN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = EDTERGRSCIEN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("enresunce")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = enresunce(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("CSHHS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = CSHHS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("HLSMOKINGIMP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = HLSMOKINGIMP(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            IFsResults("hlsmimpsw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlsmimpsw

            IFsResults("hlsmokingdecm")(getDataFrameIdxFromDims(Year, FirstYear)) = hlsmokingdecm

            IFsResults("hlsmokingincm")(getDataFrameIdxFromDims(Year, FirstYear)) = hlsmokingincm

            For Dim1 = 1 to 3
                IFsResults("hlsmokingceiling")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)) = hlsmokingceiling(Dim1)
            Next Dim1

            For Dim1 = 1 to 3
                IFsResults("hlsmokingfloor")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)) = hlsmokingfloor(Dim1)
            Next Dim1

            IFsResults("hlsmkimeff")(getDataFrameIdxFromDims(Year, FirstYear)) = hlsmkimeff

            IFsResults("hlsmkel")(getDataFrameIdxFromDims(Year, FirstYear)) = hlsmkel

            IFsResults("hlsmokingpeakyr")(getDataFrameIdxFromDims(Year, FirstYear)) = hlsmokingpeakyr

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("hlsmokingm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = hlsmokingm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("hlsmokingstsw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlsmokingstsw

            For Dim1 = 1 to sNR
                IFsResults("SSWELTAXR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SSWELTAXR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ssweltaxrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ssweltaxrm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SSWELTAX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SSWELTAX(Dim1)
            Next Dim1

            IFsResults("edqualsecur")(getDataFrameIdxFromDims(Year, FirstYear)) = edqualsecur

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("svdthsadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = svdthsadd(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SOCIETALVIOLENCEMEASURE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SOCIETALVIOLENCEMEASURE(Dim1)
            Next Dim1

            IFsResults("svvionormsw")(getDataFrameIdxFromDims(Year, FirstYear)) = svvionormsw

            IFsResults("svtohlsw")(getDataFrameIdxFromDims(Year, FirstYear)) = svtohlsw

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("svindexm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = svindexm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("svmulm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = svmulm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ENSOLFUEL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENSOLFUEL(Dim1)
            Next Dim1

            IFsResults("hlsolfuelsw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlsolfuelsw

            For Dim1 = 1 to sNR
                IFsResults("ensolfueltrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ensolfueltrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ensolfueltrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ensolfueltrgtval(Dim1)
            Next Dim1

            IFsResults("ensolflhldsw")(getDataFrameIdxFromDims(Year, FirstYear)) = ensolflhldsw

            For Dim1 = 1 to sNR
                IFsResults("ensolfuelsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ensolfuelsetar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ensolfuelseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ensolfuelseyrtar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ensolfuelm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ensolfuelm(Dim1)
            Next Dim1

            IFsResults("edqualprispndimpthresholdval")(getDataFrameIdxFromDims(Year, FirstYear)) = edqualprispndimpthresholdval

            IFsResults("edqualsecspndimpthresholdval")(getDataFrameIdxFromDims(Year, FirstYear)) = edqualsecspndimpthresholdval

            IFsResults("edqualprispndimpthreshold")(getDataFrameIdxFromDims(Year, FirstYear)) = edqualprispndimpthreshold

            IFsResults("edqualsecspndimpthreshold")(getDataFrameIdxFromDims(Year, FirstYear)) = edqualsecspndimpthreshold

            For Dim1 = 1 to 15
                IFsResults("hllongvtystartage")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 15)) = hllongvtystartage(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SDEATH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SDEATH(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SFCIVILWARUCDP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SFCIVILWARUCDP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("sfmassrep")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = sfmassrep(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SFDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SFDEM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SFECONDEV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SFECONDEV(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SFGOV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SFGOV(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SFIMBAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SFIMBAL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SFINSTABALL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SFINSTABALL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SFINSTABMAG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SFINSTABMAG(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SFINTLWARALL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SFINTLWARALL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SFINTLWARMAG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SFINTLWARMAG(Dim1)
            Next Dim1

            IFsResults("sfusehist")(getDataFrameIdxFromDims(Year, FirstYear)) = sfusehist

            IFsResults("sfconv")(getDataFrameIdxFromDims(Year, FirstYear)) = sfconv

            For Dim1 = 1 to sNR
                IFsResults("sfintlwarmagm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = sfintlwarmagm(Dim1)
            Next Dim1

            IFsResults("sfcivilwarucdpsw")(getDataFrameIdxFromDims(Year, FirstYear)) = sfcivilwarucdpsw

            For Dim1 = 1 to sNR
                IFsResults("sfintlwaradd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = sfintlwaradd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("ST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = ST(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECLOWRVOC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECLOWRVOC(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECUPPRVOC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECUPPRVOC(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLSTUNT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLSTUNT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLSTUNTCHL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLSTUNTCHL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLSTUNTWORK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLSTUNTWORK(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hlstuntchlm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hlstuntchlm(Dim1)
            Next Dim1

            IFsResults("hlstuntyrs")(getDataFrameIdxFromDims(Year, FirstYear)) = hlstuntyrs

            For Dim1 = 1 to sNR
                IFsResults("XWBSUBF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XWBSUBF(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 7
                    IFsResults("SURVSE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)) = SURVSE(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SURVSELD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SURVSELD(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDPRISUR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDPRISUR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("survseadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = survseadd(Dim1)
            Next Dim1

            For Dim1 = 1 to 9
                For Dim2 = 1 to sNR
                    IFsResults("SWINGSTS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9, Dim2, sNR)) = SWINGSTS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("swseffmax")(getDataFrameIdxFromDims(Year, FirstYear)) = swseffmax

            IFsResults("swseffects")(getDataFrameIdxFromDims(Year, FirstYear)) = swseffects

            IFsResults("wpgreatlev")(getDataFrameIdxFromDims(Year, FirstYear)) = wpgreatlev

            IFsResults("wpgreatthresh")(getDataFrameIdxFromDims(Year, FirstYear)) = wpgreatthresh

            IFsResults("hlmorbm")(getDataFrameIdxFromDims(Year, FirstYear)) = hlmorbm

            IFsResults("dyadtarifffundsw")(getDataFrameIdxFromDims(Year, FirstYear)) = dyadtarifffundsw

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("XTARIFFREV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = XTARIFFREV(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("XTARIFFTAXR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = XTARIFFTAXR(Dim1, Dim2)
                Next Dim2
            Next Dim1

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("TARIFFRATEAGR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = TARIFFRATEAGR(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("TARIFFRATEENE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = TARIFFRATEENE(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("TARIFFRATEICT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = TARIFFRATEICT(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("TARIFFRATEMANU")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = TARIFFRATEMANU(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("TARIFFRATEMAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = TARIFFRATEMAT(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("TARIFFRATESER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = TARIFFRATESER(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

            For Dim1 = 1 to sNR
                IFsResults("TAXRA")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = TAXRA(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("taxinfadjm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = taxinfadjm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("etechadv")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = etechadv(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to 9
                IFsResults("etechadvuncon")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9)) = etechadvuncon(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    For Dim3 = 1 to 3
                        IFsResults("labcoeffsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6, Dim3, 3)) = labcoeffsm(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to 4
                IFsResults("techconv")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)) = techconv(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRATELECOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRATELECOST(Dim1)
            Next Dim1

            IFsResults("infratelecostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = infratelecostupper

            IFsResults("infratelecostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = infratelecostlower

            For Dim1 = 1 to sNR
                IFsResults("infratelecostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infratelecostm(Dim1)
            Next Dim1

            IFsResults("infratelelife")(getDataFrameIdxFromDims(Year, FirstYear)) = infratelelife

            For Dim1 = 1 to sNR
                IFsResults("INFRATELE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRATELE(Dim1)
            Next Dim1

            IFsResults("infrateledtfp")(getDataFrameIdxFromDims(Year, FirstYear)) = infrateledtfp

            For Dim1 = 1 to sNR
                IFsResults("INFRATELEDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRATELEDEM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("infratelem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = infratelem(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRATELEOLD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRATELEOLD(Dim1)
            Next Dim1

            IFsResults("envtemptoginidomel")(getDataFrameIdxFromDims(Year, FirstYear)) = envtemptoginidomel

            IFsResults("envtemptoginidomsw")(getDataFrameIdxFromDims(Year, FirstYear)) = envtemptoginidomsw

            IFsResults("TERMTR")(getDataFrameIdxFromDims(Year, FirstYear)) = TERMTR

            IFsResults("termtrm")(getDataFrameIdxFromDims(Year, FirstYear)) = termtrm

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("terdispute")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = terdispute(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("EDEXPERTER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = EDEXPERTER(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edtergradgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edtergradgr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edtergndreqgrad")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edtergndreqgrad(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edtergradtrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edtergradtrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edtergradm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edtergradm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edterintgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edterintgr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edtergndreqint")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edtergndreqint(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edterintm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edterintm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edterinttrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edterinttrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edterscienshradd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edterscienshradd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDTERTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDTERTOT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("reac")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = reac(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("THREAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = THREAT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("THREATREC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = THREATREC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("THREATSENT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = THREATSENT(Dim1)
            Next Dim1

            IFsResults("wpthrconv")(getDataFrameIdxFromDims(Year, FirstYear)) = wpthrconv

            IFsResults("envclimsens")(getDataFrameIdxFromDims(Year, FirstYear)) = envclimsens

            IFsResults("hlsmokingtcsel")(getDataFrameIdxFromDims(Year, FirstYear)) = hlsmokingtcsel

            For Dim1 = 1 to sNR
                IFsResults("hlsmokingtcs")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hlsmokingtcs(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hlsmokingtcsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hlsmokingtcsm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("EDTOTCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = EDTOTCOST(Dim1)
            Next Dim1

            IFsResults("elasit")(getDataFrameIdxFromDims(Year, FirstYear)) = elasit

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("TEFF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = TEFF(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("TFR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = TFR(Dim1)
            Next Dim1

            IFsResults("tfrconv")(getDataFrameIdxFromDims(Year, FirstYear)) = tfrconv

            For Dim1 = 1 to sNR
                IFsResults("tfrmin")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = tfrmin(Dim1)
            Next Dim1

            IFsResults("eltfrcon")(getDataFrameIdxFromDims(Year, FirstYear)) = eltfrcon

            For Dim1 = 1 to sNR
                IFsResults("tfrexovals")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = tfrexovals(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("tfrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = tfrm(Dim1)
            Next Dim1

            IFsResults("ofscth")(getDataFrameIdxFromDims(Year, FirstYear)) = ofscth

            For Dim1 = 1 to sNR
                IFsResults("GOVCON")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVCON(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLCOST(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("HLEXPEND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = HLEXPEND(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("GOVHHTRN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = GOVHHTRN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 2
                    IFsResults("WATRESTOTALRENEW")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 2)) = WATRESTOTALRENEW(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAROADTOTAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAROADTOTAL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WATERTOTALSUPPLY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WATERTOTALSUPPLY(Dim1)
            Next Dim1

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("TRADEAGREEMENTINDEXBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = TRADEAGREEMENTINDEXBILAT(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

            For Dim1 = 1 to sNR
                IFsResults("TRADEBAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = TRADEBAL(Dim1)
            Next Dim1

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("TRADECOMPLEMENTAGR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = TRADECOMPLEMENTAGR(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("TRADECOMPLEMENTENE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = TRADECOMPLEMENTENE(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("TRADECOMPLEMENTICT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = TRADECOMPLEMENTICT(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("TRADECOMPLEMENTMANU")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = TRADECOMPLEMENTMANU(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("TRADECOMPLEMENTMAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = TRADECOMPLEMENTMAT(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("TRADECOMPLEMENTSER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = TRADECOMPLEMENTSER(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

            IFsResults("trademax")(getDataFrameIdxFromDims(Year, FirstYear)) = trademax

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("elastrpr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = elastrpr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("thrtcoeftrdshr")(getDataFrameIdxFromDims(Year, FirstYear)) = thrtcoeftrdshr

            For Dim1 = 1 to sNR
                IFsResults("INFRAINDTRADDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAINDTRADDEM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("tradsratadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = tradsratadd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("TRADSRATLD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = TRADSRATLD(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 7
                    IFsResults("TRADSRAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 7)) = TRADSRAT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("hlvehsw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlvehsw

            For Dim1 = 1 to sNR
                IFsResults("deathtrpvsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = deathtrpvsetar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("deathtrpvseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = deathtrpvseyrtar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("DEATHTRPV")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = DEATHTRPV(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECUPPRTRAN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECUPPRTRAN(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECLOWRTRAN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECLOWRTRAN(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("INFRAINDTRAN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = INFRAINDTRAN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("wastewatertreatedm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = wastewatertreatedm(Dim1)
            Next Dim1

            IFsResults("tmortr2")(getDataFrameIdxFromDims(Year, FirstYear)) = tmortr2

            IFsResults("tmortr")(getDataFrameIdxFromDims(Year, FirstYear)) = tmortr

            IFsResults("ttfrr")(getDataFrameIdxFromDims(Year, FirstYear)) = ttfrr

            For Dim1 = 1 to sNR
                IFsResults("TRUCKSFL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = TRUCKSFL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("TRUCKSFLPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = TRUCKSFLPC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 12
                    IFsResults("TRUCKS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 12)) = TRUCKS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 12
                    IFsResults("trucksm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 12)) = trucksm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("TRUCKSALPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = TRUCKSALPC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("truckstotm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = truckstotm(Dim1)
            Next Dim1

            IFsResults("trucksconv")(getDataFrameIdxFromDims(Year, FirstYear)) = trucksconv

            For Dim1 = 1 to sNR
                IFsResults("TRUCKSTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = TRUCKSTOT(Dim1)
            Next Dim1

            IFsResults("hlgbdconvUp")(getDataFrameIdxFromDims(Year, FirstYear)) = hlgbdconvUp

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 9
                    IFsResults("QEUNCON")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 9)) = QEUNCON(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MALNCHP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MALNCHP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("LABUNEMPR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LABUNEMPR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("labunemprm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = labunemprm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("labunemprtrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = labunemprtrgtval(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XFINUNEXP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XFINUNEXP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ictbroadtrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ictbroadtrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("watsafetrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = watsafetrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("sanitationtrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = sanitationtrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("UNPAIDLABOR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = UNPAIDLABOR(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("unpaidlaborm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = unpaidlaborm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("CSHHU")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = CSHHU(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ldcropunusedshare")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ldcropunusedshare(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edsecupprlen")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edsecupprlen(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDSECUPPRENRG")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDSECUPPRENRG(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edsecupprenrm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edsecupprenrm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("EDEXPERSECUPPR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = EDEXPERSECUPPR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edsecupprgndreqgrad")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edsecupprgndreqgrad(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edsecupprgradgr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edsecupprgradgr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edsecupprgratrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edsecupprgratrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edsecupprgram")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edsecupprgram(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edsecupprtrancap")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edsecupprtrancap(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edsecupprgndreqtran")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edsecupprgndreqtran(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edsecupprtrangr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edsecupprtrangr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("edsecupprtrantrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = edsecupprtrantrgtyr(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edsecupprtranm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edsecupprtranm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edsecupprvocadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edsecupprvocadd(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POPURBAN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPURBAN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POPURBGR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPURBGR(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ENVPM2PT5")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ENVPM2PT5(Dim1)
            Next Dim1

            IFsResults("ifsexomodelid")(getDataFrameIdxFromDims(Year, FirstYear)) = ifsexomodelid

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("VADDHHS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = VADDHHS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("VADD")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = VADD(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("VADDHHU")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = VADDHHU(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("VADDF")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = VADDF(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("VEHICLESTOT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = VEHICLESTOT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("VEHICFLPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = VEHICFLPC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("vehicflpcm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = vehicflpcm(Dim1)
            Next Dim1

            IFsResults("vehfuncsw")(getDataFrameIdxFromDims(Year, FirstYear)) = vehfuncsw

            For Dim1 = 1 to sNR
                IFsResults("ensfvent")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ensfvent(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SVDTHSCHIL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SVDTHSCHIL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SVDTHSWOMEN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SVDTHSWOMEN(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MIGRANTSVOLOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MIGRANTSVOLOUT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MIGRANTSVOLIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MIGRANTSVOLIN(Dim1)
            Next Dim1

            IFsResults("wagnerc")(getDataFrameIdxFromDims(Year, FirstYear)) = wagnerc

            IFsResults("waron")(getDataFrameIdxFromDims(Year, FirstYear)) = waron

            IFsResults("watwastetreatcostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = watwastetreatcostupper

            IFsResults("watwastetreatcostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = watwastetreatcostlower

            For Dim1 = 1 to sNR
                IFsResults("WATWASTETREATCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WATWASTETREATCOST(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WATWASTE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WATWASTE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("watwastem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = watwastem(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("wastewaterportiontreated")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = wastewaterportiontreated(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("watwastetreatsetar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = watwastetreatsetar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("WASTEWATERSUPPLY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = WASTEWATERSUPPLY(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("wastewatportreatreusedm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = wastewatportreatreusedm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WATWASTETREATDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WATWASTETREATDEM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("watwastetreatcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = watwastetreatcostm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WATWASTETREAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WATWASTETREAT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("watwastetreatm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = watwastetreatm(Dim1)
            Next Dim1

            IFsResults("watwastetreatlife")(getDataFrameIdxFromDims(Year, FirstYear)) = watwastetreatlife

            For Dim1 = 1 to sNR
                IFsResults("watwastetreatseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = watwastetreatseyrtar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("WATSAFEDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = WATSAFEDEM(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("watsafenewsw")(getDataFrameIdxFromDims(Year, FirstYear)) = watsafenewsw

            IFsResults("watsafefwdsw")(getDataFrameIdxFromDims(Year, FirstYear)) = watsafefwdsw

            IFsResults("hlwatsansw")(getDataFrameIdxFromDims(Year, FirstYear)) = hlwatsansw

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("WATERHHCOSTHIGHER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = WATERHHCOSTHIGHER(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("WATERHHCOSTLOWER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = WATERHHCOSTLOWER(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WATSAFECOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WATSAFECOST(Dim1)
            Next Dim1

            IFsResults("watsafecostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = watsafecostupper

            IFsResults("watsafecostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = watsafecostlower

            For Dim1 = 1 to sNR
                IFsResults("watsafecostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = watsafecostm(Dim1)
            Next Dim1

            IFsResults("watsafeimpcostupper")(getDataFrameIdxFromDims(Year, FirstYear)) = watsafeimpcostupper

            IFsResults("watsafeimpcostlower")(getDataFrameIdxFromDims(Year, FirstYear)) = watsafeimpcostlower

            For Dim1 = 1 to sNR
                IFsResults("watsafeimpcostm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = watsafeimpcostm(Dim1)
            Next Dim1

            IFsResults("watsafelife")(getDataFrameIdxFromDims(Year, FirstYear)) = watsafelife

            For Dim1 = 1 to sNR
                IFsResults("WATSAFEIMPCOST")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WATSAFEIMPCOST(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("WATDEMAND")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = WATDEMAND(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("waterdemandm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = waterdemandm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WATERDESALINATED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WATERDESALINATED(Dim1)
            Next Dim1

            IFsResults("watsafeimplife")(getDataFrameIdxFromDims(Year, FirstYear)) = watsafeimplife

            IFsResults("watprid")(getDataFrameIdxFromDims(Year, FirstYear)) = watprid

            IFsResults("watprii")(getDataFrameIdxFromDims(Year, FirstYear)) = watprii

            For Dim1 = 1 to sNR
                IFsResults("watpricetoylelas")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = watpricetoylelas(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("landirareaequipelas")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = landirareaequipelas(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("watdemandelas")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = watdemandelas(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WATPRICE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WATPRICE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("watres")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = watres(Dim1)
            Next Dim1

            IFsResults("waterhhlifeboreholetubewell")(getDataFrameIdxFromDims(Year, FirstYear)) = waterhhlifeboreholetubewell

            IFsResults("waterhhlifedugwell")(getDataFrameIdxFromDims(Year, FirstYear)) = waterhhlifedugwell

            IFsResults("waterhhlifepiped")(getDataFrameIdxFromDims(Year, FirstYear)) = waterhhlifepiped

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("watsafeexovals")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = watsafeexovals(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("WATERHHDEMPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = WATERHHDEMPOP(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("WATERHHDEM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = WATERHHDEM(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("waterhhm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = waterhhm(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("WATERHH")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = WATERHH(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("waterhhbasictrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = waterhhbasictrgtyr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("waterhhbasictrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = waterhhbasictrgtval(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("waterbudgetprotec")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = waterbudgetprotec(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("waterhhsafetrgtyr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = waterhhsafetrgtyr(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("waterhhsafetrgtval")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = waterhhsafetrgtval(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("waterhhcostlowershr")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = waterhhcostlowershr(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("WATERHHINVESTCAP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = WATERHHINVESTCAP(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("WATERHHINVESTMAINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = WATERHHINVESTMAINT(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    For Dim3 = 1 to 3
                        IFsResults("WATERHHPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5, Dim3, 3)) = WATERHHPOP(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("WATERFILTERED")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = WATERFILTERED(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("waterfilteredm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = waterfilteredm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("watertoyieldswitch")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = watertoyieldswitch(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WATUSE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WATUSE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WATUSEPC")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WATUSEPC(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("WATERWITHDRAWAL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = WATERWITHDRAWAL(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("waterwithdrawalm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = waterwithdrawalm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            IFsResults("fprihw")(getDataFrameIdxFromDims(Year, FirstYear)) = fprihw

            IFsResults("ylhw")(getDataFrameIdxFromDims(Year, FirstYear)) = ylhw

            For Dim1 = 1 to sNR
                IFsResults("mhwag")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = mhwag(Dim1)
            Next Dim1

            IFsResults("eprohw")(getDataFrameIdxFromDims(Year, FirstYear)) = eprohw

            IFsResults("mhw")(getDataFrameIdxFromDims(Year, FirstYear)) = mhw

            IFsResults("ehw")(getDataFrameIdxFromDims(Year, FirstYear)) = ehw

            IFsResults("mbasehw")(getDataFrameIdxFromDims(Year, FirstYear)) = mbasehw

            IFsResults("xhw")(getDataFrameIdxFromDims(Year, FirstYear)) = xhw

            For Dim1 = 1 to 4
                IFsResults("infraindictcompwt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 4)) = infraindictcompwt(Dim1)
            Next Dim1

            For Dim1 = 1 to 7
                IFsResults("infraindtradcompwt")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 7)) = infraindtradcompwt(Dim1)
            Next Dim1

            For Dim1 = 1 to 26
                IFsResults("wpwghtpow")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 26)) = wpwghtpow(Dim1)
            Next Dim1

         If bilateralsw = 1 Then
            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("IGOBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = IGOBILAT(Dim1, Dim2)
                Next Dim2
            Next Dim1
         End If

            For Dim1 = 1 to sNR
                IFsResults("DIPLOIGO")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = DIPLOIGO(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("DIPLOTREATY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = DIPLOTREATY(Dim1)
            Next Dim1

            IFsResults("WPQLI")(getDataFrameIdxFromDims(Year, FirstYear)) = WPQLI

            For Dim1 = 1 to 27
                IFsResults("govriskweight")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 27)) = govriskweight(Dim1)
            Next Dim1

            IFsResults("exomerpppmatchsw")(getDataFrameIdxFromDims(Year, FirstYear)) = exomerpppmatchsw

            For Dim1 = 1 to sNR
                IFsResults("xworkremitinm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = xworkremitinm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("xworkremitoutm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = xworkremitoutm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POPWORKING")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POPWORKING(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("CWORKING")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = CWORKING(Dim1)
            Next Dim1

            IFsResults("WTEMP")(getDataFrameIdxFromDims(Year, FirstYear)) = WTEMP

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 5
                    IFsResults("XWBFLOWS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 5)) = XWBFLOWS(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XWBLNFIN")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XWBLNFIN(Dim1)
            Next Dim1

            IFsResults("xwblnintr")(getDataFrameIdxFromDims(Year, FirstYear)) = xwblnintr

            IFsResults("XWBLOANSTOT")(getDataFrameIdxFromDims(Year, FirstYear)) = XWBLOANSTOT

            IFsResults("xwbloanr")(getDataFrameIdxFromDims(Year, FirstYear)) = xwbloanr

            IFsResults("xwblnrepr")(getDataFrameIdxFromDims(Year, FirstYear)) = xwblnrepr

            IFsResults("xwblneqr")(getDataFrameIdxFromDims(Year, FirstYear)) = xwblneqr

            For Dim1 = 1 to sNR
                IFsResults("xwblnfinm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = xwblnfinm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XWBLOANS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XWBLOANS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("XWBLNFOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = XWBLNFOUT(Dim1)
            Next Dim1

            For Dim1 = 1 to 5
                IFsResults("xwbsectar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 5)) = xwbsectar(Dim1)
            Next Dim1

            IFsResults("WCLPC")(getDataFrameIdxFromDims(Year, FirstYear)) = WCLPC

            IFsResults("WCPOW")(getDataFrameIdxFromDims(Year, FirstYear)) = WCPOW

            IFsResults("WEPBYEAR")(getDataFrameIdxFromDims(Year, FirstYear)) = WEPBYEAR

            IFsResults("WEP")(getDataFrameIdxFromDims(Year, FirstYear)) = WEP

            For Dim1 = 1 to 9
                IFsResults("WENP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9)) = WENP(Dim1)
            Next Dim1

            For Dim1 = 1 to 9
                IFsResults("WRESER")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9)) = WRESER(Dim1)
            Next Dim1

            IFsResults("xfdiwgrm")(getDataFrameIdxFromDims(Year, FirstYear)) = xfdiwgrm

            For Dim1 = 1 to 3
                IFsResults("WAP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 3)) = WAP(Dim1)
            Next Dim1

            IFsResults("WFORST")(getDataFrameIdxFromDims(Year, FirstYear)) = WFORST

            IFsResults("WGDP")(getDataFrameIdxFromDims(Year, FirstYear)) = WGDP

            IFsResults("WGDPPC")(getDataFrameIdxFromDims(Year, FirstYear)) = WGDPPC

            IFsResults("WLIFE")(getDataFrameIdxFromDims(Year, FirstYear)) = WLIFE

            IFsResults("WLIT")(getDataFrameIdxFromDims(Year, FirstYear)) = WLIT

            IFsResults("wmigrm")(getDataFrameIdxFromDims(Year, FirstYear)) = wmigrm

            IFsResults("WNPOW")(getDataFrameIdxFromDims(Year, FirstYear)) = WNPOW

            For Dim1 = 1 to sNR
                IFsResults("wpextinterv")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = wpextinterv(Dim1)
            Next Dim1

            IFsResults("wppowhhsw")(getDataFrameIdxFromDims(Year, FirstYear)) = wppowhhsw

            IFsResults("WPOP")(getDataFrameIdxFromDims(Year, FirstYear)) = WPOP

            IFsResults("WPOPR")(getDataFrameIdxFromDims(Year, FirstYear)) = WPOPR

            For Dim1 = 1 to 6
                IFsResults("WP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 6)) = WP(Dim1)
            Next Dim1

            For Dim1 = 1 to 9
                IFsResults("WRESOR")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, 9)) = WRESOR(Dim1)
            Next Dim1

            IFsResults("WTHREAT")(getDataFrameIdxFromDims(Year, FirstYear)) = WTHREAT

            IFsResults("WTRADE")(getDataFrameIdxFromDims(Year, FirstYear)) = WTRADE

            IFsResults("wvsagesw")(getDataFrameIdxFromDims(Year, FirstYear)) = wvsagesw

            IFsResults("MODYEAR")(getDataFrameIdxFromDims(Year, FirstYear)) = MODYEAR

            For Dim1 = 1 to sNR
                IFsResults("democpolityseyrtar")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = democpolityseyrtar(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("edyrsagm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = edyrsagm(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDYRSAG15")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDYRSAG15(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDYRSAG20TO29")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDYRSAG20TO29(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDYRSAG25")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDYRSAG25(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("EDYRSAG15TO24")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = EDYRSAG15TO24(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 15
                    For Dim3 = 1 to 3
                        IFsResults("HLYLL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)) = HLYLL(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 15
                    For Dim3 = 1 to 3
                        IFsResults("HLYLLWORK")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 15, Dim3, 3)) = HLYLLWORK(Dim1, Dim2, Dim3)
                    Next Dim3
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("hltfragehalflife")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = hltfragehalflife(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("mfpconv")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = mfpconv(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("TGRYL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = TGRYL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("YL")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = YL(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("YLIMPACTWATPRICE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = YLIMPACTWATPRICE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("YIELDINT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = YIELDINT(Dim1)
            Next Dim1

            IFsResults("ylexp")(getDataFrameIdxFromDims(Year, FirstYear)) = ylexp

            For Dim1 = 1 to sNR
                IFsResults("YTHBULGE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = YTHBULGE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("LDRTRIP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LDRTRIP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("LDRTRIPOUT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = LDRTRIPOUT(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("ldrtripm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = ldrtripm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SFCIVILWARPOPIMP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SFCIVILWARPOPIMP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SFCIVILWARPARDEE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SFCIVILWARPARDEE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SFCIVILWARPOPIMPABS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SFCIVILWARPOPIMPABS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("sfcivilwaradd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = sfcivilwaradd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("sfcivilwarpopimpm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = sfcivilwarpopimpm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("POPFOREIGNBILAT")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = POPFOREIGNBILAT(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to sNR
                    IFsResults("migbilatgrowthratelimit")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, sNR)) = migbilatgrowthratelimit(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("stockin")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = stockin(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("stockout")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = stockout(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("POWERBECKLEY")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = POWERBECKLEY(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("Theta1")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = Theta1(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("Theta2")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = Theta2(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("Theta3")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = Theta3(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("Theta1High")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = Theta1High(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("Theta2High")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = Theta2High(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("Theta3High")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = Theta3High(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("Theta1Low")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = Theta1Low(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("Theta2Low")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = Theta2Low(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("Theta3Low")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = Theta3Low(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WOMENINLEGISLATURE")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WOMENINLEGISLATURE(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WOMENINJOURNALISM")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WOMENINJOURNALISM(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("WOMENPROPRIGHTS")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = WOMENPROPRIGHTS(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("mtarifftaxrgl")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = mtarifftaxrgl(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("SFINSECUREPOPIMP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = SFINSECUREPOPIMP(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("sfinsecurepopimpm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = sfinsecurepopimpm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("MALNPOP")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = MALNPOP(Dim1)
            Next Dim1

            IFsResults("gdppcppeak")(getDataFrameIdxFromDims(Year, FirstYear)) = gdppcppeak

            For Dim1 = 1 to sNR
                IFsResults("womenproprightsm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = womenproprightsm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("womenproprightsadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = womenproprightsadd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("womeninjournalismm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = womeninjournalismm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("womeninjournalismadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = womeninjournalismadd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("womeninlegislaturem")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = womeninlegislaturem(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("womeninlegistlatureadd")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = womeninlegistlatureadd(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 3
                    IFsResults("POPBYSEX")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 3)) = POPBYSEX(Dim1, Dim2)
                Next Dim2
            Next Dim1

            For Dim1 = 1 to sNR
                IFsResults("popurbanm")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR)) = popurbanm(Dim1)
            Next Dim1

            For Dim1 = 1 to sNR
                For Dim2 = 1 to 6
                    IFsResults("mfpaddsec")(getDataFrameIdxFromDims(Year, FirstYear, Dim1, sNR, Dim2, 6)) = mfpaddsec(Dim1, Dim2)
                Next Dim2
            Next Dim1

        End Sub

    End Module

End Namespace
