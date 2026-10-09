Partial Class frm_CyberDashboardData4Area
    Inherits Page

    'jrs 2015/08/25
    Dim bMaxReload As Boolean

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        bMaxReload = False
        If Session("CDSceIdx") Is Nothing Then
            'Reload initialization values
            DashboardInitialization(Session("DisYear"), Session("DisplayLastYear"), Session("FirstYear"), Session("LastYear"),
                    Session("CostBenefitIdx"), Session("CDSceFileNames"), Session("PutFdrvRun2"), Session("vShortSce"),
                    Session("RunFiles"), Session("IFsGroupAlias"), Session("CDValues"), Session("CDValuesAG"),
                    Session("CDValuesLine"), Session("CDValuesLineDim1"), Session("CDValuesLineSce"), Session("CDSceIdx"))
        End If
        If Not (Request.QueryString("RegName") Is Nothing) Then
            Dim RegIdx As Integer, sReg As String
            sReg = Request.QueryString("RegName")
            If Session("CDDim1") <> sReg Then
                bMaxReload = True
            End If
            Session("CDDim1") = sReg

            Dim sSceName As String
            If Not (Request.QueryString("SceName") Is Nothing) Then
                sSceName = Trim(Replace(Request.QueryString("SceName"), """", ""))
                Session("CDSceName") = sSceName
                Dim ss As Integer
                If Session("CDSceName") = "Base Case" Then
                    Session("CDSceIdx") = 1
                Else
                    For ss = 1 To 4
                        If Session("CDSceFileNames")(ss) = Session("PutFdrvRun2") + "\" & sSceName & ".run" Then
                            Session("CDSceIdx") = 5 + ss
                            Exit For
                        End If
                    Next
                End If

                LoadSetCB()
                PackJS()
            End If
        End If
    End Sub

    Private Sub LoadSetCB()
        'jrs 2015/05/19
        'Dim cybGDP(0, 0) As Single
        ReDim Session("cybGDP")(Session("LastYear") - Session("FirstYear") + 1)
        Dim GDPCount As Integer
        GDPCount = 0

        Dim r As Integer, T As Integer, g As Integer, y As Integer, v As Integer
        'Dim dbM As Database
        Dim bGrp As Boolean = False
        Dim sDim1 As String
        Dim iDim1 As Integer

        Dim cf As Integer
        cf = 0


        'just for one region
        If Not CType(Session("IFsGroupAlias"), Dictionary(Of String, String)).ContainsKey(Session("CDDim1")) Then
            r = IFsRegs.Item(Session("CDDim1")).Index
            If r = -1 Then
                bGrp = True
                r = gGetGroupIdx(Session("CDDim1"), True, Me)
            End If
            sDim1 = Session("CDDim1") 'lstDim1.List(r)
            iDim1 = r '+ 1
        Else
            sDim1 = CType(Session("IFsGroupAlias"), Dictionary(Of String, String)).Item(Session("CDDim1"))
            r = gGetGroupIdx(sDim1, True, Me)
            iDim1 = r
            bGrp = True
        End If

        'for later: consider g-lists and decomposed groups
        'Select Case True
        '    Case mnuReg.Checked
        '        bGrp = False
        '    Case mnuGroups.Checked
        '        bGrp = True
        'End Select

        'only one file
        Dim f As Integer = Session("CDSceIdx")
        LoadCyberVar(f, sDim1, iDim1, bGrp, Session("CostBenefitIdx")(1), 1)
        LoadCyberVar(f, sDim1, iDim1, bGrp, Session("CostBenefitIdx")(2), 2)
        LoadCyberVar(f, sDim1, iDim1, bGrp, Session("CostBenefitIdx")(3), 3)
        LoadCyberVar(f, sDim1, iDim1, bGrp, Session("CostBenefitIdx")(4), 4)
        LoadCyberVar(f, sDim1, iDim1, bGrp, Session("CostBenefitIdx")(5), 5)

        'cf = cf + 1
        'ReDim Preserve CountryFileNames(cf)
        'CountryFileNames(cf) = sDim1 & "[" & getShortFileName(COUNTE) & "]"

        ''jrs 2015/05/19
        'GDPCount = GDPCount + 1
        Dim sFileName As String
        'jam 2023/10/28
        'If f = 1 Then
        '    sFileName = Session("PutFdrvRun") & "ifsBase.run"
        'Else
        '    sFileName = Session("PutFdrvRun") & Session("CDSceName") & ".run"
        'End If

        If f = 1 Then
            sFileName = Session("PutFdrvRun") & "ifsBase.run.db"
        Else
            sFileName = Session("PutFdrvRun") & Session("CDSceName") & ".run.db"
        End If

        gLoadForDisplayNET("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
            sFileName, f, True, True, bGrp, 1, Session("cybGDP"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
            Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
        'gLoadForDisplay("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
        'sFileName, f, True, True, bGrp, 1, Session("cybGDP"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
        'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
        'Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))

        'jrs 2015/08/25
        If bMaxReload Then
            'jrs 2015/08/25
            Dim i As Integer
            If f <> 6 Then
                'jam 2023/10/28
                'gLoadForDisplayNET("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
                'Session("PutFdrvRun") & "Clockwork Orange.run", 6, True, True, bGrp, 1, Session("cybGDPCO"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
                'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                'Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))

                'jam 2023/10/28
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
                      Session("PutFdrvRun") & "Clockwork Orange.run.db", 6, True, True, bGrp, 1, Session("cybGDPCO"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
                      Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                      Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                Else
                    gLoadForDisplayNET("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
                        Session("PutFdrvRun") & "Clockwork Orange.run", 6, True, True, bGrp, 1, Session("cybGDPCO"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                        Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                End If


                'gLoadForDisplay("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
                'Session("PutFdrvRun") & "Clockwork Orange.run", 6, True, True, bGrp, 1, Session("cybGDPCO"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
                'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                'Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Else 'simply copy
                For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                    Session("cybGDPCO")(i) = Session("cybGDP")(i)
                Next
            End If

            If f <> 9 Then
                'jam 2023/10/28
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
                        Session("PutFdrvRun") & "Shangri-La.run.db", 9, True, True, bGrp, 1, Session("cybGDPSL"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                        Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                Else
                    gLoadForDisplayNET("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
                       Session("PutFdrvRun") & "Shangri-La.run", 9, True, True, bGrp, 1, Session("cybGDPSL"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
                       Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                       Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                End If


                'gLoadForDisplay("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
                '                    Session("PutFdrvRun") & "Shangri-La.run", 9, True, True, bGrp, 1, Session("cybGDPSL"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
                '                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                '                    Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Else 'simply copy
                For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                    Session("cybGDPSL")(i) = Session("cybGDP")(i)
                Next
            End If

            'jrs 2015/08/25
            LoadMaxScale()
        End If
        'Save Map to order from higher to lower on last year
        'Create an array of columns
        'Dim PlotRank() As DisplayColumn
        'ReDim PlotRank(5)
        'Dim i As Integer

        'For i = 1 To 5
        '    PlotRank(i) = New DisplayColumn
        '    PlotRank(i).Index = i
        '    PlotRank(i).Value = Session("CDValuesAG")(Session("DisYear"), i)
        '    'jrs 2010/02/03 to break ties
        '    PlotRank(i).Value2 = Session("CDValuesAG")(1, i)
        '    'jrs 2011/07/04 save original order
        '    PlotRank(i).Index2 = i
        'Next i
        ''Sort the array
        'QuickSortObj(PlotRank, 1, 5, False, False)
        'ReDim Session("MapHighToLow4Areas")(5)
        'For i = 1 To 5
        '    Session("MapHighToLow4Areas")(i) = PlotRank(i).Index
        'Next

    End Sub

    Private Sub LoadCyberVar(ByVal iFile As Integer,
        ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sVar As String, ByVal iCol As Integer)
        Dim i As Integer, j As Integer
        'COUNTE = COUNTE + 1
        Dim VarIdx As Integer
        VarIdx = IfsVars.Item(sVar).Index

        'If sVar = "ICTCYBEVTCOST" And iFile = 0 Then
        '    'Need to compute prob x cost across the 12 cats
        '    EstimateEVTCOST()
        '    GR1N(COUNTE) = sReg
        '    FDNG1(COUNTE) = GR1N$(COUNTE)
        'ElseIf sVar = "ictcybevprob" Or sVar = "ictcybevcost" Then  'Add logic for Prob or Cost
        '    'Load all dimensions
        '    COUNTE = COUNTE - 1
        '    If iFile = 0 Then
        '        LoadSetSCE sVar
        '    Else
        '        For i = 1 To MAXCOM(VDim1(VarIdx)) - 1
        '            For j = 1 To MAXCOM(VDim2(VarIdx)) - 1
        '                COUNTE = COUNTE + 1
        '                gLoadForDisplay(sVar, VarIdx, "", i, "", j, "", -1, "", iFile, (i = 1 And j = 1), (i = MAXCOM(VDim1(VarIdx)) - 1 And j = MAXCOM(VDim2(VarIdx)) - 1), False, 1, PLOT, COUNTE, _
        '                1, FirstYear, LastYear, 0, False, "", True)
        '            Next j
        '        Next i
        '    End If
        'Else
        Dim sDim2 As String
        sDim2 = ""
        Dim sFile As String
        If iFile = 1 Then
            'jam 2023/10/14
            'sFile = Session("PutFdrvRun") & "ifsBase.run"
            If Session("mnuSQLite") = "True" Then
                sFile = Session("PutFdrvRun") & "ifsBase.run.db"
            Else
                sFile = Session("PutFdrvRun") & "ifsBase.run"
            End If
        Else
            'jam 2023/10/14
            'sFile = Session("PutFdrvRun") & Session("CDSceName") & ".run"


            If Session("mnuSQLite") = "True" Then
                sFile = Session("PutFdrvRun") & Session("CDSceName") & ".run.db"
            Else
                sFile = Session("PutFdrvRun") & Session("CDSceName") & ".run"
            End If

        End If
        gLoadForDisplayNET(sVar, VarIdx, sReg, iReg, sDim2, -1, "", -1,
            sFile, iFile, True, True, bGrp, 1, Session("CDValuesAG"), iCol, 1, Session("FirstYear"), Session("LastYear"), 0, False,
            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
            Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
        'gLoadForDisplay(sVar, VarIdx, sReg, iReg, sDim2, -1, "", -1,
        'sFile, iFile, True, True, bGrp, 1, Session("CDValuesAG"), iCol, 1, Session("FirstYear"), Session("LastYear"), 0, False,
        'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
        'Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))

        'jrs 2015/08/25
        If bMaxReload Then
            If iFile <> 6 Then
                'jam 2023/10/31
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET(sVar, VarIdx, sReg, iReg, sDim2, -1, "", -1,
                        Session("PutFdrvRun") & "Clockwork Orange.run.db", 6, True, True, bGrp, 1, Session("CDValuesAGCO"), iCol, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                        Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))

                Else
                    gLoadForDisplayNET(sVar, VarIdx, sReg, iReg, sDim2, -1, "", -1,
                       Session("PutFdrvRun") & "Clockwork Orange.run", 6, True, True, bGrp, 1, Session("CDValuesAGCO"), iCol, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                       Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                       Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                End If

                'gLoadForDisplay(sVar, VarIdx, sReg, iReg, sDim2, -1, "", -1,
                'Session("PutFdrvRun") & "Clockwork Orange.run", 6, True, True, bGrp, 1, Session("CDValuesAGCO"), iCol, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                'Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Else
                For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                    Session("CDValuesAGCO")(i, iCol) = Session("CDValuesAG")(i, iCol)
                Next
            End If

            If iFile <> 9 Then

                'jam 2023/10/31
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET(sVar, VarIdx, sReg, iReg, sDim2, -1, "", -1,
                        Session("PutFdrvRun") & "Shangri-La.run.db", 9, True, True, bGrp, 1, Session("CDValuesAGSL"), iCol, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                        Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                Else
                    gLoadForDisplayNET(sVar, VarIdx, sReg, iReg, sDim2, -1, "", -1,
                       Session("PutFdrvRun") & "Shangri-La.run", 9, True, True, bGrp, 1, Session("CDValuesAGSL"), iCol, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                       Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                       Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                End If


                'gLoadForDisplay(sVar, VarIdx, sReg, iReg, sDim2, -1, "", -1,
                '                    Session("PutFdrvRun") & "Shangri-La.run", 9, True, True, bGrp, 1, Session("CDValuesAGSL"), iCol, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                '                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                '                    Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Else
                For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                    Session("CDValuesAGSL")(i, iCol) = Session("CDValuesAG")(i, iCol)
                Next
            End If
        End If
        'End If
    End Sub

    Private Sub PackJS()
        Dim sJscript2 As String, i As Integer
        sJscript2 = "    var dataAllSeriesArea = [];" & vbCr
        For i = 1 To 5
            If i > 1 Then
                sJscript2 = sJscript2 & "," & vbCr
            End If
            'jrs 2015/05/27 sJscript2 = sJscript2 & PlotColToHCSeriesAG(Session("MapHighToLow4Areas")(i))
            sJscript2 = sJscript2 & "dataAllSeriesArea[" & i.ToString & "]=" & PlotColToHCSeriesAG(i)
        Next

        'Totals
        i = 6
        sJscript2 = sJscript2 & ", dataAllSeriesArea[" & i.ToString & "]=" & PlotColToHCSeriesAGTot(1)
        i = 7
        sJscript2 = sJscript2 & ", dataAllSeriesArea[" & i.ToString & "]=" & PlotColToHCSeriesAGTot(2)

        'Cumulatives
        For i = 1 To 5
            sJscript2 = sJscript2 & "," & vbCr
            'jrs 2015/05/27 sJscript2 = sJscript2 & PlotColToHCSeriesAG(Session("MapHighToLow4Areas")(i))
            sJscript2 = sJscript2 & "dataAllSeriesArea[" & (i + 7).ToString & "]=" & PlotColToHCSeriesAGCum(i)
        Next

        'Totals
        i = 13
        sJscript2 = sJscript2 & ", dataAllSeriesArea[" & i.ToString & "]=" & PlotColToHCSeriesAGTotCum(2)
        i = 14
        sJscript2 = sJscript2 & ", dataAllSeriesArea[" & i.ToString & "]=" & PlotColToHCSeriesAGTotCum(1)

        'jrs 2015/08/25
        sJscript2 = sJscript2 & "    var Max4Areas2 = [];" & vbCr
        For i = 1 To 6
            sJscript2 = sJscript2 & "Max4Areas2[" & i.ToString & "]=" & Math.Round(Session("MaxVals4Area")(i), 2) & ";" & vbCr
        Next

        Response.Write(sJscript2)
    End Sub

    Private Function PlotColToHCSeriesAG(ByVal i As Integer) As String

        Dim sJscript As String = "{data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim iNumDec As Integer = 2


        For j = 1 To Session("DisYear")
            If Not IsDBNull(Session("CDValuesAG")(j, i)) Then
                If IsNumeric(Session("CDValuesAG")(j, i)) Then
                    sJscript = sJscript & CStr(Math.Round(Session("CDValuesAG")(j, i), iNumDec))

                    If j < Session("DisYear") Then
                        sJscript = sJscript & ", "
                    End If
                    'ElseIf Session("GType") = "BAR" Then
                    '    sJscript = sJscript & "<point name = """ & CStr(Session("FirstYear") + j - 1) & _
                    '    """ y=""0"" x=""" & CStr(Session("FirstYear") + j - 1) & """ enabled = ""False"" />" & vbCr
                End If
            End If
        Next

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToHCSeriesAG = sJscript
    End Function

    Private Function PlotColToHCSeriesAGCum(ByVal i As Integer) As String

        Dim sJscript As String = "{data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim iNumDec As Integer = 2


        For j = 1 To Session("DisYear")
            If Not IsDBNull(Session("CDValuesAG")(j, i)) Then
                If IsNumeric(Session("CDValuesAG")(j, i)) Then
                    If i < 4 Then
                        sJscript = sJscript & CStr(Math.Round(CompoundAddVal(j, i, Session("CDValuesAG"), Session("cybGDP")), iNumDec))
                    Else
                        sJscript = sJscript & CStr(Math.Round(CompoundVal(j, i, Session("CDValuesAG"), Session("cybGDP")), iNumDec))
                    End If


                    If j < Session("DisYear") Then
                        sJscript = sJscript & ", "
                    End If
                    'ElseIf Session("GType") = "BAR" Then
                    '    sJscript = sJscript & "<point name = """ & CStr(Session("FirstYear") + j - 1) & _
                    '    """ y=""0"" x=""" & CStr(Session("FirstYear") + j - 1) & """ enabled = ""False"" />" & vbCr
                End If
            End If
        Next

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToHCSeriesAGCum = sJscript
    End Function

    Private Function PlotColToHCSeriesAGTot(ByVal i As Integer) As String

        Dim sJscript As String = "{data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim iNumDec As Integer = 2
        'jrs 2015/05/28
        Dim colStart As Integer, colEnd As Integer, k As Integer
        Dim yrVal As Single
        If i = 1 Then
            colStart = 1
            colEnd = 3
        Else
            colStart = 4
            colEnd = 5
        End If

        For j = 1 To Session("DisYear")
            yrVal = 0
            For k = colStart To colEnd
                If Not IsDBNull(Session("CDValuesAG")(j, k)) Then
                    If IsNumeric(Session("CDValuesAG")(j, k)) Then
                        yrVal = yrVal + CStr(Math.Round(Session("CDValuesAG")(j, k), iNumDec))
                    End If
                End If
            Next
            sJscript = sJscript & CStr(Math.Round(yrVal, iNumDec))

            If j < Session("DisYear") Then
                sJscript = sJscript & ", "
            End If
        Next

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToHCSeriesAGTot = sJscript
    End Function

    Private Function PlotColToHCSeriesAGTotCum(ByVal i As Integer) As String

        Dim sJscript As String = "{data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim iNumDec As Integer = 2
        'jrs 2015/05/28
        Dim colStart As Integer, colEnd As Integer, k As Integer
        Dim yrVal As Single
        If i = 1 Then
            colStart = 1
            colEnd = 3
        Else
            colStart = 4
            colEnd = 5
        End If

        For j = 1 To Session("DisYear")
            yrVal = 0
            For k = colStart To colEnd
                If Not IsDBNull(Session("CDValuesAG")(j, k)) Then
                    If IsNumeric(Session("CDValuesAG")(j, k)) Then
                        If k < 4 Then
                            yrVal = yrVal + CStr(Math.Round(CompoundAddVal(j, k, Session("CDValuesAG"), Session("cybGDP")), iNumDec))
                        Else
                            yrVal = yrVal + CStr(Math.Round(CompoundVal(j, k, Session("CDValuesAG"), Session("cybGDP")), iNumDec))
                        End If
                    End If
                End If
            Next
            sJscript = sJscript & CStr(Math.Round(yrVal, iNumDec))

            If j < Session("DisYear") Then
                sJscript = sJscript & ", "
            End If
        Next

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToHCSeriesAGTotCum = sJscript
    End Function

    Private Function LoadVarNameDsh(ByVal gPutFdrvRun As String, ByVal VarDefinition As String) As String
        Dim Rcnt As Short
        Dim Varname As String
        Dim valtbl As DataTable
        Dim db As String
        Dim idx As Integer
        Dim sql As String




        db = gPutFdrvRun & "IFSVAR.mdb"
        sql = "SELECT NAME FROM IFSVAR WHERE TYPE=1 AND DEFINITION LIKE  """ & VarDefinition & """"
        SetDataTablefromAccess(db, sql, "", valtbl)



        idx = 0
        Varname = Session("CDVarName")
        For Rcnt = 0 To valtbl.Rows.Count - 1
            Varname = valtbl.Rows(Rcnt).Item("NAME")

            'VarDefinition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")
            'VarDefinition = Replace(Replace(Replace(VarDefinition, Chr(10), ""), Chr(13), ""), """", "")

            idx = idx + 1

        Next Rcnt

        LoadVarNameDsh = Varname
    End Function

    Private Function PrepareDataForMap(ByVal yrIdx As Integer) As String
        Dim jScript As String = "", i As Integer
        Dim iRound As Integer = 4
        Select Case ABS_(Session("MaxCDValues"))
            Case Is > 1000
                iRound = 0
            Case Is > 100
                iRound = 1
            Case Is > 10
                iRound = 2
            Case Is > 1
                iRound = 3
            Case Else
                iRound = 4
        End Select
        Session("iRound") = iRound
        For i = 1 To NR
            If i > 1 Then
                jScript = jScript & "," & vbCr
            End If
            jScript = jScript & "{" & vbCr
            'jrs 2015/05/13 jScript = jScript & """code"": """ & IFsRegs.Item(GRPS(1, i)).ISO3166_2Code & """," & vbCr
            jScript = jScript & """key"": """ & IFsRegs.Item(GRPS(1, i)).ISO3166_2Code & """," & vbCr
            jScript = jScript & """value"": " & Math.Round(Session("CDValues")(yrIdx, i), iRound) & vbCr
            jScript = jScript & "}"
        Next
        jScript = jScript & vbCr
        PrepareDataForMap = jScript
    End Function

    Private Function getSeriesNameAG(ByVal ii As Integer) As String
        Dim Legend As String = ""
        Select Case ii
            Case 1
                Legend = "Security Spend"
            Case 2
                Legend = "Event Cost"
            Case 3
                Legend = "Opport Cost"
            Case 4
                Legend = "Economic Increment"
            Case 5
                Legend = "Consumer surplus"
        End Select
        getSeriesNameAG = Legend
    End Function

    Private Function CompoundAddVal(ByVal yr As Integer, ByVal col As Integer, ByRef CDValuesAG As Object, ByRef cybGDP As Object) As Single
        Dim i As Integer, Val As Single
        'jrs 2015/05/19
        Dim colGDP As Integer
        colGDP = (col - 1) \ 5 + 1
        If Not IsDBNull(CDValuesAG(1, col)) Then
            'jrs 2015/05/19 CompoundAddVal = Round(PLOT(1, col), 4)
            CompoundAddVal = Math.Round(CDValuesAG(1, col) / 100 * cybGDP(1), 4)
        Else
            CompoundAddVal = -9999
            Exit Function
        End If

        For i = 2 To yr
            If Not IsDBNull(CDValuesAG(i, col)) Then
                'jrs 2015/05/19 CompoundAddVal = Round(CompoundAddVal + PLOT(i, col), 4)
                CompoundAddVal = Math.Round(CompoundAddVal + CDValuesAG(i, col) / 100 * cybGDP(i), 4)
            Else
                CompoundAddVal = -9999
                Exit Function
            End If
        Next i
    End Function

    Private Function CompoundVal(ByVal yr As Integer, ByVal col As Integer, ByRef CDValuesAG As Object, ByRef cybGDP As Object) As Single
        Dim i As Integer, MultVal As Single
        'jrs 2015/05/19
        Dim colGDP As Integer
        colGDP = (col - 1) \ 5 + 1
        If Not IsDBNull(CDValuesAG(1, col)) Then
            'jrs 2015/05/19
            '        CompoundVal = Round(PLOT(1, col) / 100 + 1, 6)
            MultVal = Math.Round(CDValuesAG(1, col) / 100 + 1, 6)
            CompoundVal = Math.Round(CDValuesAG(1, col) / 100 * cybGDP(1), 6)
        Else
            CompoundVal = -9999
            Exit Function
        End If
        For i = 2 To yr
            If Not IsDBNull(CDValuesAG(i, col)) Then
                'jrs 2015/05/19
                MultVal = Math.Round(MultVal * (CDValuesAG(i, col) / 100 + 1), 6)
                'jrs 2015/05/21 MultVal = Round((PLOT(i, col) / 100 + 1) ^ i * PLOT(i, col) / 100 * frm_Cyber.getGDP(i, colGDP), 6)
                CompoundVal = CompoundVal + (MultVal - 1) * cybGDP(1)
            Else
                CompoundVal = -9999
                Exit Function
            End If
        Next i

        'jrs 2015/05/19 CompoundVal = (CompoundVal - yr) * 100
    End Function

    Private Function getSeriesNameAGTot(ByVal ii As Integer) As String
        Dim Legend As String = ""
        Select Case ii
            Case 1
                Legend = "Costs"
            Case 2
                Legend = "Benefits"
        End Select
        getSeriesNameAGTot = Legend
    End Function

    'jrs 2015/08/25
    Private Sub LoadMaxScale()
        ReDim Session("MaxVals4Area")(6)
        Dim Max As Single, Max2 As Single
        'Only Costs annualized
        Max = Session("CDValuesAGCO")(Session("DisYear"), 1) +
                Session("CDValuesAGCO")(Session("DisYear"), 2) +
                Session("CDValuesAGCO")(Session("DisYear"), 3)
        Session("MaxVals4Area")(1) = Math.Round(Max * 1.1, 2)

        'Only Benefits annualized
        Max = Session("CDValuesAGSL")(Session("DisYear"), 4) +
            Session("CDValuesAGSL")(Session("DisYear"), 5)
        Session("MaxVals4Area")(2) = Math.Round(Max * 1.1, 2)

        'Both Costs and Benefits annualized
        'Clockwork Orange
        If Session("CDValuesAGCO")(Session("DisYear"), 1) + Session("CDValuesAGCO")(Session("DisYear"), 2) + Session("CDValuesAGCO")(Session("DisYear"), 3) >
                Session("CDValuesAGCO")(Session("DisYear"), 4) + Session("CDValuesAGCO")(Session("DisYear"), 5) Then
            Max = Session("CDValuesAGCO")(Session("DisYear"), 1) + Session("CDValuesAGCO")(Session("DisYear"), 2) + Session("CDValuesAGCO")(Session("DisYear"), 3)
        Else
            Max = Session("CDValuesAGCO")(Session("DisYear"), 4) + Session("CDValuesAGCO")(Session("DisYear"), 5)
        End If
        'Shangri-La
        If Session("CDValuesAGSL")(Session("DisYear"), 1) + Session("CDValuesAGSL")(Session("DisYear"), 2) + Session("CDValuesAGSL")(Session("DisYear"), 3) >
                Session("CDValuesAGSL")(Session("DisYear"), 4) + Session("CDValuesAGSL")(Session("DisYear"), 5) Then
            If Session("CDValuesAGSL")(Session("DisYear"), 1) + Session("CDValuesAGSL")(Session("DisYear"), 2) + Session("CDValuesAGSL")(Session("DisYear"), 3) > Max Then
                Max = Session("CDValuesAGSL")(Session("DisYear"), 1) + Session("CDValuesAGSL")(Session("DisYear"), 2) + Session("CDValuesAGSL")(Session("DisYear"), 3)
            End If
        Else
            If Session("CDValuesAGSL")(Session("DisYear"), 4) + Session("CDValuesAGSL")(Session("DisYear"), 5) > Max Then
                Max = Session("CDValuesAGSL")(Session("DisYear"), 4) + Session("CDValuesAGSL")(Session("DisYear"), 5)
            End If
        End If
        Session("MaxVals4Area")(3) = Math.Round(Max * 1.1, 2)

        'Only Cost Cumulative
        Max = CompoundAddVal(Session("DisYear"), 1, Session("CDValuesAGCO"), Session("cybGDPCO")) +
                CompoundAddVal(Session("DisYear"), 2, Session("CDValuesAGCO"), Session("cybGDPCO")) +
                CompoundAddVal(Session("DisYear"), 3, Session("CDValuesAGCO"), Session("cybGDPCO"))
        Session("MaxVals4Area")(4) = Math.Round(Max * 1.1, 2)

        'Only Benefits Cumulative
        Max = CompoundVal(Session("DisYear"), 4, Session("CDValuesAGSL"), Session("cybGDPSL")) +
            CompoundVal(Session("DisYear"), 5, Session("CDValuesAGSL"), Session("cybGDPSL"))
        Session("MaxVals4Area")(5) = Math.Round(Max * 1.1, 2)

        'Both Costs and Benefits Cumulative
        Max = CompoundAddVal(Session("DisYear"), 1, Session("CDValuesAGCO"), Session("cybGDPCO")) +
                CompoundAddVal(Session("DisYear"), 2, Session("CDValuesAGCO"), Session("cybGDPCO")) +
                CompoundAddVal(Session("DisYear"), 3, Session("CDValuesAGCO"), Session("cybGDPCO"))
        Max2 = CompoundVal(Session("DisYear"), 4, Session("CDValuesAGCO"), Session("cybGDPCO")) +
                CompoundVal(Session("DisYear"), 5, Session("CDValuesAGCO"), Session("cybGDPCO"))
        If Max < Max2 Then
            Max = Max2
        End If
        Max2 = CompoundAddVal(Session("DisYear"), 1, Session("CDValuesAGSL"), Session("cybGDPSL")) +
                CompoundAddVal(Session("DisYear"), 2, Session("CDValuesAGSL"), Session("cybGDPSL")) +
                CompoundAddVal(Session("DisYear"), 3, Session("CDValuesAGSL"), Session("cybGDPSL"))
        If Max < Max2 Then
            Max = Max2
        End If
        Max2 = CompoundVal(Session("DisYear"), 4, Session("CDValuesAGSL"), Session("cybGDPSL")) +
                CompoundVal(Session("DisYear"), 5, Session("CDValuesAGSL"), Session("cybGDPSL"))
        If Max < Max2 Then
            Max = Max2
        End If
        Session("MaxVals4Area")(6) = Math.Round(Max * 1.1, 2)
    End Sub


End Class
