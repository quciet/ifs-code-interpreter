Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data
Imports IFs.Data.ProducerScope
Imports System.Math

Namespace Models

    Partial Public Module IntlConf
        'jrs 2011/06/08
        Dim WPINDKANT(,) As Single, WPINDREAL(,) As Single, COI(,,) As Single, SharedRiv(,) As Boolean
        Dim COI2(,,) As Single
        'jrs 2011/12/22
        Dim MaxGovEmb1 As Single, MaxGovEmb2 As Single
        Dim MaxRiskIdx As Single
        'jrs 2011/11/10
        Dim MeanLiberal As Single, StdDevLiberal As Single
        Dim MeanReal As Single, StdDevReal As Single

        Public Sub IFsIntlConflictAllYears(context As IForecastContext)
            If context.HistoricMode = True Or wpindsw = 0 Or wpindsw > 4 Then 'clp 2024/07/10 Block if new code active
                Exit Sub
            End If

            'jrs 2011/12/08
            'sErrorSub = "IFsIntlConflict"

            Dim R1 As Integer, R2 As Integer, Democracy As Single, Trade As Single, IO As Single
            Using db As IFsContext = New IFsContext()

                If IY = 1 Then
                    'Load Region codes for countries
                    '        If IFsCtrys.Count = 0 Then 'jrs 2011/12/08 added condition in case running the scenario several times
                    '            For R1 = 1 To CCount
                    '                IFsCtrys.Add CounReg(R1), CountryName$(R1)
                    'Next R1
                    '        End If

                    ReDim WPINDKANT(NR, NR), WPINDREAL(NR, NR), COI(NR, NR, 2)
                    ReDim SharedRiv(NR, NR)
                    'Read Shared River data
                    '        Dim db As Database, sql As String, rs As Recordset
                    '        sql = "SELECT CT1.Country, CT2.Country" &
                    '" FROM [Country Translation] AS CT1, [Country Translation] AS CT2, SharedRiversCountry AS SR" &
                    '" WHERE CT1.[COW Numbers] = SR.CountryACowNumber" &
                    '" AND CT2.[COW Numbers] = SR.CountryBCowNumber"

                    For Each item In (From SR In db.SharedRiversCountry
                                      Join CT1 In db.CountryTranslation On CT1.COWNumbers Equals SR.CountryACowNumber
                                      Join CT2 In db.CountryTranslation On CT2.COWNumbers Equals SR.CountryBCowNumber
                                      Select Country1 = CT1.Country, Country2 = CT2.Country)

                        'R1 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(0)), False)
                        'R2 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(1)), False)
                        'R1 = IFsCtrys(rs.Fields(0))
                        'R2 = IFsCtrys(rs.Fields(1))
                        R1 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country1)).Value
                        R2 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country2)).Value

                        If SharedRiv(R1, R2) = False Then
                            SharedRiv(R1, R2) = True
                            SharedRiv(R2, R1) = True
                        End If
                    Next

                    'Initialize COI
                    'jrs 2011/11/09
                    '        sql = "SELECT CT1.Country, CT2.Country, CI.StandCOI " & _
                    '        " FROM GovConflict as CI, [Country Translation] AS CT1, [Country Translation] AS CT2" & _
                    '        " WHERE int(CT1.[COW Numbers]) = CI.COW1" & _
                    '        " AND int(CT2.[COW Numbers]) = CI.COW2" & _
                    '        " AND CI.Year = " & CStr(BaseYear - 9) 'jrs 2011/10/12 Jonathan wants to use 2001

                    Dim MaxCOIYear = (From CI In db.GovConflict
                                      Where CI.StandCOI.HasValue
                                      Group Value = CI.Year By CI.COW1, CI.COW2 Into Data = Group, MaxVal = Max(Value))


                    For Each item In (From CI In db.GovConflict
                                      Join CT1 In db.CountryTranslation On CT1.COWNumbers Equals CI.COW1.ToString
                                      Join CT2 In db.CountryTranslation On CT2.COWNumbers Equals CI.COW2.ToString
                                      Join MaxCOI In MaxCOIYear On MaxCOI.COW1 Equals CI.COW1 And MaxCOI.COW2 Equals CI.COW2 And MaxCOI.MaxVal Equals CI.Year
                                      Select Country1 = CT1.Country, Country2 = CT2.Country, StandCOI = CI.StandCOI, Year = CI.Year)

                        'R1 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(0)), False)
                        'R2 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(1)), False)
                        'R1 = IFsCtrys(rs.Fields(0))
                        'R2 = IFsCtrys(rs.Fields(1))
                        R1 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country1)).Value
                        R2 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country2)).Value

                        'Value
                        COI(R1, R2, 1) = CSng(item.StandCOI)
                        COI(R2, R1, 1) = CSng(item.StandCOI)
                        'Year
                        COI(R1, R2, 2) = item.Year
                        COI(R2, R1, 2) = item.Year

                    Next

                    'jrs 2011/11/10
                    ReDim COI2(NR, NR, 2)





                    '        Sql = "SELECT CT1.Country, CT2.Country, CI.StandCOI, CI.Year" &
                    '" FROM [Country Translation] AS CT1, [Country Translation] AS CT2, GovConflict as CI, " &
                    '" (SELECT CI2.COW1, CI2.COW2, MAX(CI2.Year) as MaxYrMinus10 " &
                    '" FROM (SELECT CI.COW1, CI.COW2, CI.Year " &
                    '" FROM GovConflict as CI, " &
                    '" (SELECT GovConflict.COW1, GovConflict.COW2, Max(GovConflict.Year) AS MaxOfYear " &
                    '" From GovConflict " &
                    '" Where ((Not (GovConflict.[StandCOI]) Is Null)) " &
                    '" GROUP BY GovConflict.COW1, GovConflict.COW2) as MaxCOIYear " &
                    '" Where MaxCOIYear.MaxOfYear > [CI].[Year] + 9 " &
                    '" AND MaxCOIYear.COW2=[CI].[COW2] " &
                    '" AND MaxCOIYear.COW1=[CI].[COW1]) AS CI2 " &
                    '" GROUP BY CI2.COW1, CI2.COW2) as MaxCOIYearM10 " &
                    '" Where Int([CT1].[COW Numbers]) = [CI].[COW1] " &
                    '" AND Int([CT2].[COW Numbers])=[CI].[COW2] " &
                    '" AND MaxCOIYearM10.MaxYrMinus10=[CI].[Year] " &
                    '" AND MaxCOIYearM10.COW2=[CI].[COW2] " &
                    '" AND MaxCOIYearM10.COW1=[CI].[COW1]"


                    'Set rs = dbIFs.OpenRecordset(sql)
                    'Do While Not rs.EOF
                    '                'R1 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(0)), False)
                    '                'R2 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(1)), False)
                    '                R1 = IFsCtrys(rs.Fields(0))
                    '                R2 = IFsCtrys(rs.Fields(1))

                    '                'Value
                    '                COI2(R1, R2, 1) = rs.Fields(2)
                    '                COI2(R2, R1, 1) = rs.Fields(2)
                    '                'Year
                    '                COI2(R1, R2, 2) = rs.Fields(3)
                    '                COI2(R2, R1, 2) = rs.Fields(3)
                    '                rs.MoveNext
                    '            Loop
                    '            rs.Close



                    Dim MaxCOIYearSQL = (From CI In db.GovConflict
                                         Where CI.StandCOI.HasValue
                                         Group Value = CI.Year By CI.COW1, CI.COW2 Into Data = Group, MaxVal2 = Max(Value))

                    Dim MaxCOIYearM10SQL = (From CI2 In db.GovConflict
                                            Join MaxCOI In MaxCOIYearSQL On MaxCOI.COW1 Equals CI2.COW1 And MaxCOI.COW2 Equals CI2.COW2
                                            Where MaxCOI.MaxVal2 > CI2.Year + 9 'jrs 2019/09/16 Review 2019-09-15
                                            Group Value = CI2.Year By CI2.COW1, CI2.COW2 Into Data = Group, MaxYrMinus10 = Max(Value))


                    For Each item In (From CI In db.GovConflict
                                      Join CT1 In db.CountryTranslation On CT1.COWNumbers Equals CI.COW1.ToString
                                      Join CT2 In db.CountryTranslation On CT2.COWNumbers Equals CI.COW2.ToString
                                      Join MaxCOIYearM10 In MaxCOIYearM10SQL On MaxCOIYearM10.COW1 Equals CI.COW1 And MaxCOIYearM10.COW2 Equals CI.COW2 And MaxCOIYearM10.MaxYrMinus10 Equals CI.Year
                                      Select Country1 = CT1.Country, Country2 = CT2.Country, StandCOI = CI.StandCOI, Year = CI.Year)

                        R1 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country1)).Value
                        R2 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country2)).Value

                        'Value
                        COI2(R1, R2, 1) = CSng(item.StandCOI)
                        COI2(R2, R1, 1) = CSng(item.StandCOI)
                        'Year
                        COI2(R1, R2, 2) = item.Year
                        COI2(R2, R1, 2) = item.Year



                    Next

                    Dim MinCOIYearSQL = (From CI2 In db.GovConflict
                                         Join MaxCOI In MaxCOIYearSQL On MaxCOI.COW1 Equals CI2.COW1 And MaxCOI.COW2 Equals CI2.COW2
                                         Where MaxCOI.MaxVal2 > CI2.Year 'jrs 2019/09/16 Review 2019-09-15
                                         Group Value = CI2.Year By CI2.COW1, CI2.COW2 Into Data = Group, MinYr = Max(Value))


                    For Each item In (From CI In db.GovConflict
                                      Join CT1 In db.CountryTranslation On CT1.COWNumbers Equals CI.COW1.ToString
                                      Join CT2 In db.CountryTranslation On CT2.COWNumbers Equals CI.COW2.ToString
                                      Join MinCOIYear In MinCOIYearSQL On MinCOIYear.COW1 Equals CI.COW1 And MinCOIYear.COW2 Equals CI.COW2 And MinCOIYear.MinYr Equals CI.Year
                                      Select Country1 = CT1.Country, Country2 = CT2.Country, StandCOI = CI.StandCOI, Year = CI.Year)


                        R1 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country1)).Value
                        R2 = context.Regionalization.First(Function(S) S.Key.Equals(item.Country2)).Value

                        If COI2(R1, R2, 1) = 0 Then
                            'Value
                            COI2(R1, R2, 1) = CSng(item.StandCOI)
                            COI2(R2, R1, 1) = CSng(item.StandCOI)
                            'Year
                            COI2(R1, R2, 2) = item.Year
                            COI2(R2, R1, 2) = item.Year
                        End If


                    Next


                    ''        Sql = "SELECT CT1.Country, CT2.Country, CI.StandCOI, CI.Year" &
                    ''" FROM [Country Translation] AS CT1, [Country Translation] AS CT2, GovConflict as CI, " &
                    ''" (SELECT CI2.COW1, CI2.COW2, MIN(CI2.Year) as MinYr " &
                    ''" FROM (SELECT CI.COW1, CI.COW2, CI.Year " &
                    ''" FROM GovConflict as CI, " &
                    ''" (SELECT GovConflict.COW1, GovConflict.COW2, Max(GovConflict.Year) AS MaxOfYear " &
                    ''" From GovConflict " &
                    ''" Where ((Not (GovConflict.[StandCOI]) Is Null)) " &
                    ''" GROUP BY GovConflict.COW1, GovConflict.COW2) as MaxCOIYear " &
                    ''" Where MaxCOIYear.MaxOfYear > [CI].[Year] " &
                    ''" AND MaxCOIYear.COW2=[CI].[COW2] " &
                    ''" AND MaxCOIYear.COW1=[CI].[COW1]) AS CI2 " &
                    ''" GROUP BY CI2.COW1, CI2.COW2) as MinCOIYear " &
                    ''" Where Int([CT1].[COW Numbers]) = [CI].[COW1] " &
                    ''" AND Int([CT2].[COW Numbers])=[CI].[COW2] " &
                    ''" AND MinCOIYear.MinYr=[CI].[Year] " &
                    ''" AND MinCOIYear.COW2=[CI].[COW2] " &
                    ''" AND MinCOIYear.COW1=[CI].[COW1]"

                    'Set rs = dbIFs.OpenRecordset(sql)
                    'Do While Not rs.EOF
                    '                'R1 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(0)), False)
                    '                'R2 = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, frm_MainMenu.IFsGUIctrl1.cgetRegionFromCtry(rs.Fields(1)), False)
                    '                R1 = IFsCtrys(rs.Fields(0))
                    '                R2 = IFsCtrys(rs.Fields(1))

                    '                If COI2(R1, R2, 1) = 0 Then
                    '                    'Value
                    '                    COI2(R1, R2, 1) = rs.Fields(2)
                    '                    COI2(R2, R1, 1) = rs.Fields(2)
                    '                    'Year
                    '                    COI2(R1, R2, 2) = rs.Fields(3)
                    '                    COI2(R2, R1, 2) = rs.Fields(3)
                    '                End If
                    '                rs.MoveNext
                    '            Loop
                    '            rs.Close

                    'jrs 2012/06/15 db.Close

                    'jrs 2011/11/21
                    '        Set dbD = OpenDatabase(PutFdrvRun$ & "ifsDyadic.mdb")
                    '        dbD.Execute ("DELETE * FROM WPINDKANTREG")
                    '        dbD.Execute ("DELETE * FROM WPINDREALREG")
                    '        dbD.Execute ("DELETE * FROM COIREG")
                    '        Set tblK = dbD.OpenRecordset("WPINDKANTREG")
                    '        Set tblR = dbD.OpenRecordset("WPINDREALREG")
                    '        Set tblC = dbD.OpenRecordset("COIREG")

                    'jrs 2011/12/22
                    'Finding Maximum Government Embededness
                    MaxGovEmb1 = 0
                    MaxGovEmb2 = 0
                    For R1 = 1 To NR
                        If GOVEMBED(R1) > MaxGovEmb1 Then
                            MaxGovEmb2 = MaxGovEmb1
                            MaxGovEmb1 = GOVEMBED(R1)
                        ElseIf GOVEMBED(R1) > MaxGovEmb2 Then
                            MaxGovEmb2 = GOVEMBED(R1)
                        End If
                    Next R1

                    'Finding Maximum Risk Index
                    For R1 = 1 To NR
                        If GOVRISK(R1) > MaxRiskIdx Then
                            MaxRiskIdx = GOVRISK(R1)
                        End If
                    Next R1

                End If

                'Total Energy Production
                Dim WENPT As Single
                Dim i As Integer
                WENPT = 0
                For i = 1 To 6
                    WENPT = WENPT + WENP(i)
                Next i

                'Liberal Index (Kant)
                '--------------------
                For R1 = 1 To NR - 1
                    For R2 = R1 + 1 To NR
                        '1. Democracy
                        Democracy = (DEMOCPOLITY(R1) + DEMOCPOLITY(R2)) / 40
                        '2. International Organization Membership
                        IO = (GOVEMBED(R1) + GOVEMBED(R2)) / (MaxGovEmb1 + MaxGovEmb2)
                        IO = Amin(IO, 1)
                        '3. Trade
                        Trade = ((M(R1) + X(R1)) / GDP(R1) + (M(R2) + X(R2)) / GDP(R2)) / 2
                        'jrs 2011/11/10 Trade = AMAX(Amin(Trade, 1), 0)
                        'Liberal Index
                        WPINDKANT(R1, R2) = (Democracy + IO + Trade) / 3
                    Next R2
                Next R1

                'jrs 2011/11/10
                If IY = 1 Then
                    'Find the mean of Liberal Index
                    For R1 = 1 To NR - 1
                        For R2 = R1 + 1 To NR
                            MeanLiberal = MeanLiberal + WPINDKANT(R1, R2)
                        Next R2
                    Next R1

                    MeanLiberal = CSng(MeanLiberal / (NR * CLng(NR - 1) / 2))
                    'Find the standard deviation
                    For R1 = 1 To NR - 1
                        For R2 = R1 + 1 To NR
                            StdDevLiberal = CSng(StdDevLiberal + (WPINDKANT(R1, R2) - MeanLiberal) ^ 2)
                        Next R2
                    Next R1
                    StdDevLiberal = CSng((StdDevLiberal / (NR * CLng(NR - 1) / 2)) ^ (1 / 2))
                End If

                'Standarize the score
                For R1 = 1 To NR - 1
                    For R2 = R1 + 1 To NR
                        WPINDKANT(R1, R2) = (WPINDKANT(R1, R2) - MeanLiberal) / StdDevLiberal
                    Next R2
                Next R1

                'Realist Index
                '-------------
                Dim MatPow As Single, Fragility As Single, WATER As Single, ENERGY As Single
                For R1 = 1 To NR - 1
                    For R2 = R1 + 1 To NR
                        'if not contiguous and both non powers
                        'jrs 2021/12/05 added swithc
                        If wppowhhsw = 0 Then
                            If contiguity(R1, R2) > 5 And POWER(R1) < wpgreatthresh And POWER(R2) < wpgreatthresh Then
                                MatPow = 0
                                Fragility = 0
                                WATER = 0
                                ENERGY = 0
                            Else
                                '1. Material Power
                                If POWER(R1) > POWER(R2) Then
                                    MatPow = (POWER(R1) + POWER(R2)) / (POWER(R1) * 2)
                                Else
                                    MatPow = (POWER(R1) + POWER(R2)) / (POWER(R2) * 2)
                                End If
                                '2. Fragility
                                If GOVRISK(R1) > GOVRISK(R2) Then
                                    Fragility = GOVRISK(R1) / MaxRiskIdx
                                Else
                                    Fragility = GOVRISK(R2) / MaxRiskIdx
                                End If
                                '3. Water
                                'If contiguity(r1, r2) < 6 Then
                                If SharedRiv(R1, R2) Then
                                    WATER = (WATUSE(R1) / watres(R1) + WATUSE(R2) / watres(R2)) / 2
                                    If WATER > 1 Then
                                        WATER = 1
                                    ElseIf WATER < 0.4 Then
                                        WATER = 0
                                    Else
                                        WATER = CSng((WATER - 0.4) / 0.6)
                                    End If
                                Else
                                    WATER = 0
                                End If
                                '4. Energy
                                ENERGY = (WENP(Oil) / WENPT) * 80 * (RESER(R1, Oil) + RESER(R2, Oil)) / WRESER(Oil)
                                ENERGY = ENERGY + (WENP(Gas) / WENPT) * 80 * (RESER(R1, Gas) + RESER(R2, Gas)) / WRESER(Gas)
                                ENERGY = ENERGY / 2
                                ENERGY = Amin(AMAX(ENERGY, 0), 1)
                            End If
                        Else
                            If contiguity(R1, R2) > 5 And POWERHH(R1) < wpgreatthresh And POWERHH(R2) < wpgreatthresh Then
                                MatPow = 0
                                Fragility = 0
                                WATER = 0
                                ENERGY = 0
                            Else
                                '1. Material Power
                                If POWERHH(R1) > POWERHH(R2) Then
                                    MatPow = (POWERHH(R1) + POWERHH(R2)) / (POWERHH(R1) * 2)
                                Else
                                    MatPow = (POWERHH(R1) + POWERHH(R2)) / (POWERHH(R2) * 2)
                                End If
                                '2. Fragility
                                If GOVRISK(R1) > GOVRISK(R2) Then
                                    Fragility = GOVRISK(R1) / MaxRiskIdx
                                Else
                                    Fragility = GOVRISK(R2) / MaxRiskIdx
                                End If
                                '3. Water
                                'If contiguity(r1, r2) < 6 Then
                                If SharedRiv(R1, R2) Then
                                    WATER = (WATUSE(R1) / watres(R1) + WATUSE(R2) / watres(R2)) / 2
                                    If WATER > 1 Then
                                        WATER = 1
                                    ElseIf WATER < 0.4 Then
                                        WATER = 0
                                    Else
                                        WATER = CSng((WATER - 0.4) / 0.6)
                                    End If
                                Else
                                    WATER = 0
                                End If
                                '4. Energy
                                ENERGY = (WENP(Oil) / WENPT) * 80 * (RESER(R1, Oil) + RESER(R2, Oil)) / WRESER(Oil)
                                ENERGY = ENERGY + (WENP(Gas) / WENPT) * 80 * (RESER(R1, Gas) + RESER(R2, Gas)) / WRESER(Gas)
                                ENERGY = ENERGY / 2
                                ENERGY = Amin(AMAX(ENERGY, 0), 1)
                            End If
                        End If
                        'Realist Index
                        WPINDREAL(R1, R2) = -1 * (MatPow + Fragility + WATER + ENERGY) / 4
                    Next R2
                Next R1

                'jrs 2011/11/10
                If IY = 1 Then
                    'Find the mean of Realist Index
                    For R1 = 1 To NR - 1
                        For R2 = R1 + 1 To NR
                            MeanReal = MeanReal + WPINDREAL(R1, R2)
                        Next R2
                    Next R1

                    MeanReal = CSng(MeanReal / (NR * CLng(NR - 1) / 2))
                    'Find the standard deviation
                    For R1 = 1 To NR - 1
                        For R2 = R1 + 1 To NR
                            StdDevReal = CSng(StdDevReal + (WPINDREAL(R1, R2) - MeanReal) ^ 2)
                        Next R2
                    Next R1
                    StdDevReal = CSng((StdDevReal / (NR * CLng(NR - 1) / 2)) ^ (1 / 2))
                End If

                'Standarize the score
                For R1 = 1 To NR - 1
                    For R2 = R1 + 1 To NR
                        WPINDREAL(R1, R2) = (WPINDREAL(R1, R2) - MeanReal) / StdDevReal
                    Next R2
                Next R1

                Dim COIPrediction As Single
                'Character if Bilateral State Interaction (proxy for War)
                For R1 = 1 To NR - 1
                    For R2 = R1 + 1 To NR
                        'COI Forecast uses constant growth until 2050 then stays flat
                        If COI2(R1, R2, 1) = 0 Or COI(R1, R2, 2) = COI2(R1, R2, 2) Or IY = 1 Or Abs(COI(R1, R2, 1)) > 3 Then
                            COIPrediction = COI(R1, R2, 1)
                        ElseIf COI(R1, R2, 1) > 0 Then
                            COIPrediction = CSng((COI(R1, R2, 1) + 3) * ((COI(R1, R2, 1) + 3) / (COI2(R1, R2, 1) + 3)) ^ ((Amin(IY, 41) - 1) / (COI(R1, R2, 2) - COI2(R1, R2, 2))))
                            COIPrediction = COIPrediction - 3
                            'jrs 2011/12/16
                            If Abs(COIPrediction) > 3 Then
                                COIPrediction = Sign(COIPrediction) * 3
                            End If
                        End If
                        If wpindsw <> 5 Then 'clp 2024/07/10 Block if new code active
                            THREAT(R1, R2) = (WPINDKANT(R1, R2) + WPINDREAL(R1, R2)) + COIPrediction
                            THREAT(R2, R1) = THREAT(R1, R2)
                        End If 'clp 2024/07/10 Block if new code active
                    Next R2
                Next R1

                'Save results to table
                If wpindsw > 1 And wpindsw < 5 Then 'clp 2024/07/10 Block if new code active
                    For R1 = 1 To NR - 1
                        For R2 = R1 + 1 To NR
                            'jrs 2011/11/21 saving to Threat variable
                            '                tblK.AddNew
                            '                tblK.Fields("RegionIdA") = R1
                            '                tblK.Fields("RegionIdB") = R2
                            '                tblK.Fields("Year") = FirstYear + IY - 1
                            '                tblK.Fields("Forecast") = WPINDKANT(R1, R2)
                            '                tblK.Update
                            '
                            '                tblR.AddNew
                            '                tblR.Fields("RegionIdA") = R1
                            '                tblR.Fields("RegionIdB") = R2
                            '                tblR.Fields("Year") = FirstYear + IY - 1
                            '                tblR.Fields("Forecast") = WPINDREAL(R1, R2)
                            '                tblR.Update
                            '
                            Select Case wpindsw
                                Case 2
                                    THREAT(R1, R2) = WPINDKANT(R1, R2)
                                Case 3
                                    THREAT(R1, R2) = WPINDREAL(R1, R2)
                                Case 4
                                    'COI Forecast uses constant growth until 2050 then stays flat
                                    If COI2(R1, R2, 1) = 0 Or COI(R1, R2, 2) = COI2(R1, R2, 2) Or IY = 1 Or Abs(COI(R1, R2, 1)) > 3 Then
                                        COIPrediction = COI(R1, R2, 1)
                                    Else
                                        COIPrediction = CSng((COI(R1, R2, 1) + 3) * ((COI(R1, R2, 1) + 3) / (COI2(R1, R2, 1) + 3)) ^ ((Amin(IY, 41) - 1) / (COI(R1, R2, 2) - COI2(R1, R2, 2))))
                                        COIPrediction = COIPrediction - 3
                                        'jrs 2011/12/16
                                        If Abs(COIPrediction) > 3 Then
                                            COIPrediction = Sign(COIPrediction) * 3
                                        End If
                                    End If
                                    THREAT(R1, R2) = COIPrediction
                            End Select
                            THREAT(R2, R1) = THREAT(R1, R2)
                            '
                            '                tblC.AddNew
                            '                tblC.Fields("RegionIdA") = R1
                            '                tblC.Fields("RegionIdB") = R2
                            '                tblC.Fields("Year") = FirstYear + IY - 1
                            '                tblC.Fields("Forecast") = COIPrediction
                            '                tblC.Update

                        Next R2
                    Next R1
                End If


            End Using
            'jrs 2011/11/21
            '    If FirstYear + IY - 1 = LastYearForecast Then
            '        tblK.Close
            '        tblR.Close
            '        tblC.Close
            '        dbD.Close
            '    End If


        End Sub


    End Module

End Namespace
