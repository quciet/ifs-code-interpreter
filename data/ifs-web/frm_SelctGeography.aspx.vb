'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''          JAM 2010-03-15 GList/Groups Selection
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   Copyright by Barry B. Hughes 1988-2003
'   Model code available for review and education only
'   Reproduction or incorporation in other software by permission only
'   Last revision by BBH on July 17, 2002 - added MTI's validation code
Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility

Partial Class frm_SelctGeography
    Inherits Page
    Dim db As String 'DAO.Database
    Dim table As DataTable 'DAO.TableDef
    Dim tbl As DataTable 'DAO.Recordset
    Dim valtbl As DataTable


    Private Sub Page_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load


        Dim i As Integer
        If Not Page.IsPostBack Then
            'jrs 2015/09/16
            'If Not Session("bBubble") Or Session("GType") <> "SCAT" Then
            '    Session("pCOUNTE") = Session("COUNTE")
            'Else
            '    Session("pCOUNTE") = Session("COUNTE") / 3 * 2
            'End If
            If Session("pCOUNTE") = Nothing Then
                Session("pCOUNTE") = Session("COUNTE")
            End If

            If Not Session("IsaGroup") Then
                If lstDim.Items.Count = 0 Then
                    db = Session("IFs_MDB2Active")
                    SetDataTablefromAccess(db, "SELECT * FROM GListId ORDER BY Description", "", valtbl)

                    For i = 0 To valtbl.Rows.Count - 1
                        lstDim.Items.Add((valtbl.Rows(i).Item("Description")))
                    Next i
                End If
            Else    'JAM 2010-07-11 Addin groups
                If lstDim.Items.Count = 0 Then
                    db = Session("IFs_MDB2Active")
                    'jrs 2025/02/05 Use GroupName instead of LongGroupName
                    SetDataTablefromAccess(db, "SELECT DISTINCT * FROM GroupIDNewFull ORDER BY GroupName", "", valtbl)

                    For i = 0 To valtbl.Rows.Count - 1
                        'jrs 2025/02/05 Use GroupName instead of LongGroupName
                        lstDim.Items.Add((valtbl.Rows(i).Item("GroupName")))
                    Next i
                End If
            End If
        End If
    End Sub

    Protected Sub cmd_GListOpt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_GListOpt.Click
        Dim GCount, i As Integer
        'jrs 2012/07/23
        If Session("bAlias") = True Then
            Session.Remove("bAlias")
            Session("GraphLabelsParent") = Replace(Session("GraphLabelsParent"), "Alias=1", "Alias=0")
        End If

        'JAM 2010-07-18 Only it's a GList
        If lstDim.Text > "" And Not Session("IsaGroup") Then
            LoadGList(lstDim.Text)


            Session("IsaGlist") = True
            Session("GChanged") = True

            Support.FormManager.Show(Me, Session("SelectGeographyParent"))
        Else    'JAM 2010-07-11    Choosing a Group
            LoadGroup(lstDim.Text)

            'jrs 2021/01/14
            If lstDim.Text = "World" Then
                Session("IsaGroup") = False
            Else
                Session("IsaGroup") = True
            End If
            Session("IsaGlist") = False
            Session("GChanged") = True

            Support.FormManager.Show(Me, Session("SelectGeographyParent"))
        End If
    End Sub

    Protected Sub cmd_GListExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_GListExit.Click
        Select Case True
            Case Session("BDCheck") = "POP"
                Session("BDAction") = "POP"
            Case Session("BDCheck") = "GDP"
                Session("BDAction") = "GDP"
            Case Session("BDCheck") = "None"
                Session("BDAction") = "None"
            Case Else
                Session("BDAction") = "Other"
        End Select

        Support.FormManager.Show(Me, Session("SelectGeographyParent"))
    End Sub


    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        'JAM 2010-07-18
        If Session("IsaGlist") And Not Session("IsaGroup") Then
            Session("mnuGroupChecked") = False
            Session("mnuGeographicChecked") = True
            Session("mnuCountriesChecked") = False
        Else
            If Session("IsaGroup") Then
                Session("mnuGroupChecked") = True
                Session("mnuGeographicChecked") = False
                Session("mnuCountriesChecked") = False
            Else
                Session("mnuGroupChecked") = False
                Session("mnuGeographicChecked") = False
                Session("mnuCountriesChecked") = True
            End If
        End If
    End Sub

    Private Sub LoadGList(ByVal sList As String)
        'jrs 2010/09/23
        On Error GoTo localHdlr

        'Store Dimensions to Reload (2, 3)
        Dim sDim2X As String, sDim2Y As String
        Dim sDim3X As String, sDim3Y As String
        sDim2X = Session("GR2N")(Session("pCOUNTE") / 2 + 1)
        sDim2Y = Session("GR2N")(1)
        sDim3X = Session("GR3N")(Session("pCOUNTE") / 2 + 1)
        sDim3Y = Session("GR3N")(1)

        'Store formulas to reload (X, Y, Z)
        Dim fX As Formula, fY As Formula, fZ As Formula
        If Session("bCompX") Then
            fX = Session("aFormulas")(Session("pCOUNTE") / 2 + 1)
        End If
        If Session("bCompY") Then
            fY = Session("aFormulas")(1)
        End If
        'Clear the plot vector
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
        'Load GList Detail
        Dim dbGL As String
        Dim rsGL As DataTable
        Dim rr As Integer = 0
        dbGL = Session("IFs_MDB2Active")
        SetDataTablefromAccess(dbGL, "SELECT * FROM GList d, GListId i WHERE i.Code = d.GListCode AND i.Description = '" & sList & "' ORDER BY Region", "", rsGL)
        'Reload Y for all members of GList
        Do While rr < rsGL.Rows.Count
            LoadGroupMemberVals("Y", Session("COUNTE"), rsGL.Rows(rr).Item("Region"),
            Session("sVarY"), Session("fileIdxY"), Session("bCompY"), 0, sDim2Y, sDim3Y, fY, rsGL.Rows(rr).Item("Group"))

            rr = rr + 1
        Loop
        'Reload X for all members of GList
        rr = 0
        Do While rr < rsGL.Rows.Count
            LoadGroupMemberVals("X", Session("COUNTE"), rsGL.Rows(rr).Item("Region"),
            Session("sVarX"), Session("fileIdxX"), Session("bCompX"), 0, sDim2X, sDim3X, fX, rsGL.Rows(rr).Item("Group"))

            rr = rr + 1
        Loop
        'Reload Z for all Members of GList
        If Session("sVarZ") > "" Then 'jrs 2012/03/19 If Session("bBubble") Then
            rr = 0
            Do While rr < rsGL.Rows.Count
                LoadGroupMemberVals("Z", Session("COUNTE"), rsGL.Rows(rr).Item("Region"),
                Session("sVarZ"), Session("fileIdxZ"), Session("bCompZ"), 0, Session("sDim2Z"),
                Session("sDim3Z"), fZ, rsGL.Rows(rr).Item("Group"))

                rr = rr + 1
            Loop
        End If
        'Restore session("COUNTE")
        If Session("sVarZ") > "" Then 'jrs 2012/03/19 If Session("bBubble") Then
            Session("pCOUNTE") = Session("COUNTE") / 3 * 2
            'jrs 2010/10/01
            ReDim Session("BDOTHR")(Session("FinYear")(1), Session("pCOUNTE") / 2)
            Dim i As Integer, j As Integer
            For j = 1 To Session("pCOUNTE") / 2
                For i = 1 To Session("FinYear")(1)
                    If IsNumeric(Session("PLOT")(i, j + Session("pCOUNTE"))) Then
                        Session("BDOTHR")(i, j) = Session("PLOT")(i, j + Session("pCOUNTE"))
                    Else
                        Session("BDOTHR")(i, j) = System.DBNull.Value
                    End If
                Next i
            Next j
        Else
            Session("pCOUNTE") = Session("COUNTE")
        End If
        'List of points to be decomposed
        'ReDim DecPoints(session("COUNTE") / 2) As DecomposedGroup
        'Dim p As Integer
        'For p = 1 To Session("COUNTE") / 2
        '    DecPoints(p) = New DecomposedGroup
        'Next p
        'jrs 2010/09/29
        'FirstYearNotNull = FindFirstYearNotNull
        'jrs 2010/10/13
        'If Session("bBubble") Then
        '    SetUpManualScaleToMinMaxZ()
        'End If

        'Storing Auxiliaries for Bubble Diameter
        ReDim Session("BDGDP")(Session("FinYear")(1), Session("pCOUNTE") / 2)
        If Session("sVarZ") <> "POP" Then
            ReDim Session("BDPOP")(Session("FinYear")(1), Session("pCOUNTE") / 2)
            LoadBD("POP", Session("BDPOP"))
        End If
        If Session("sVarZ") <> "GDP" Then
            ReDim Session("BDGDP")(Session("FinYear")(1), Session("pCOUNTE") / 2)
            LoadBD("GDP", Session("BDGDP"))
        End If

        Session("NoneOrder") = VB6.Support.CopyArray(Session("GR1N"))

        'jrs 2010/10/13
        Dim bOldBB As Boolean
        bOldBB = Session("bBubble")
        Session("bBubble") = False
        ColorDistribution()
        Session("bBubble") = bOldBB

        'jrs 2010/10/25
        'mnuStatistics.Visible = (COUNTE Mod NR = 0) JAM 2010-11-22
        'jrs 2010/10/27
        'ReDim FirstYearToPrintLblWTrace(Session("COUNTE") / 2)

        Select Case True
            Case Session("BDCheck") = "POP"
                Session("BDAction") = "POP"
            Case Session("BDCheck") = "GDP"
                Session("BDAction") = "GDP"
            Case Session("BDCheck") = "None"
                Session("BDAction") = "None"
            Case Else
                Session("BDAction") = "Other"
        End Select

        'jrs 2010/09/23
        Exit Sub
localHdlr:
        LoadMessage(Err.Description, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")
    End Sub

    Private Sub LoadGroup(ByVal sGroupName As String)
        'jrs 2010/09/23
        On Error GoTo localHdlr

        'Store Dimensions to Reload (2, 3)
        Dim sDim2X As String, sDim2Y As String
        Dim sDim3X As String, sDim3Y As String
        sDim2X = Session("GR2N")(Session("pCOUNTE") / 2 + 1)
        sDim2Y = Session("GR2N")(1)
        sDim3X = Session("GR3N")(Session("pCOUNTE") / 2 + 1)
        sDim3Y = Session("GR3N")(1)

        'Store formulas to reload (X, Y, Z)
        Dim fX As Formula, fY As Formula, fZ As Formula
        If Session("bCompX") Then
            fX = Session("aFormulas")(Session("pCOUNTE") / 2 + 1)
        End If
        If Session("bCompY") Then
            fY = Session("aFormulas")(1)
        End If
        'Clear the plot vector
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
        'Load GList Detail
        Dim db As String
        Dim rs As DataTable
        Dim rr As Integer = 0
        db = Session("IFs_MDB2Active")
        'jrs 2025/02/05 changed from LongGroupName to GroupName
        SetDataTablefromAccess(db, "SELECT RegionNum, RegionName FROM GroupIDNewFull a, GroupingFull b," &
        RegionIDNew & " c WHERE a.GroupNum = b.GroupNo AND b.RegionNo = c.RegionNum AND a.GroupName = '" &
        sGroupName & "' ORDER BY RegionName", "", rs)
        'Reload Y for all members of GList
        Do While rr < rs.Rows.Count
            LoadGroupMemberVals("Y", Session("COUNTE"), rs.Rows(rr).Item("RegionName"),
            Session("sVarY"), Session("fileIdxY"), Session("bCompY"), 0, sDim2Y, sDim3Y, fY, False)

            rr = rr + 1
        Loop
        'Reload X for all members of GList
        rr = 0
        Do While rr < rs.Rows.Count
            LoadGroupMemberVals("X", Session("COUNTE"), rs.Rows(rr).Item("RegionName"),
            Session("sVarX"), Session("fileIdxX"), Session("bCompX"), 0, sDim2X, sDim3X, fX, False)

            rr = rr + 1
        Loop
        'Reload Z for all Members of GList
        If Session("sVarZ") > "" Then 'jrs 2012/03/19 If Session("bBubble") Then
            rr = 0
            Do While rr < rs.Rows.Count
                LoadGroupMemberVals("Z", Session("COUNTE"), rs.Rows(rr).Item("RegionName"),
                Session("sVarZ"), Session("fileIdxZ"), Session("bCompZ"), 0, Session("sDim2Z"),
                Session("sDim3Z"), fZ, False)

                rr = rr + 1
            Loop
        End If
        'Restore session("COUNTE")
        If Session("sVarZ") > "" Then 'jrs 2012/03/19 If Session("bBubble") Then
            Session("pCOUNTE") = Session("COUNTE") / 3 * 2
            'jrs 2010/10/01
            ReDim Session("BDOTHR")(Session("FinYear")(1), Session("pCOUNTE") / 2)
            Dim i As Integer, j As Integer
            For j = 1 To Session("pCOUNTE") / 2
                For i = 1 To Session("FinYear")(1)
                    If IsNumeric(Session("PLOT")(i, j + Session("pCOUNTE"))) Then
                        Session("BDOTHR")(i, j) = Session("PLOT")(i, j + Session("pCOUNTE"))
                    Else
                        Session("BDOTHR")(i, j) = System.DBNull.Value
                    End If
                Next i
            Next j
        Else
            Session("pCOUNTE") = Session("COUNTE")
        End If
        'List of points to be decomposed
        'ReDim DecPoints(session("COUNTE") / 2) As DecomposedGroup
        'Dim p As Integer
        'For p = 1 To Session("COUNTE") / 2
        '    DecPoints(p) = New DecomposedGroup
        'Next p
        'jrs 2010/09/29
        'FirstYearNotNull = FindFirstYearNotNull
        'jrs 2010/10/13
        'If Session("bBubble") Then
        '    SetUpManualScaleToMinMaxZ()
        'End If

        'Storing Auxiliaries for Bubble Diameter
        ReDim Session("BDGDP")(Session("FinYear")(1), Session("pCOUNTE") / 2)
        If Session("sVarZ") <> "POP" Then
            ReDim Session("BDPOP")(Session("FinYear")(1), Session("pCOUNTE") / 2)
            LoadBD("POP", Session("BDPOP"))
        End If
        If Session("sVarZ") <> "GDP" Then
            ReDim Session("BDGDP")(Session("FinYear")(1), Session("pCOUNTE") / 2)
            LoadBD("GDP", Session("BDGDP"))
        End If

        Session("NoneOrder") = VB6.CopyArray(Session("GR1N"))

        'jrs 2010/10/13
        Dim bOldBB As Boolean
        bOldBB = Session("bBubble")
        Session("bBubble") = False
        ColorDistribution()
        Session("bBubble") = bOldBB

        'jrs 2010/10/25
        'mnuStatistics.Visible = (COUNTE Mod NR = 0) JAM 2010-11-22
        'jrs 2010/10/27
        'ReDim FirstYearToPrintLblWTrace(Session("COUNTE") / 2)

        Select Case True
            Case Session("BDCheck") = "POP"
                Session("BDAction") = "POP"
            Case Session("BDCheck") = "GDP"
                Session("BDAction") = "GDP"
            Case Session("BDCheck") = "None"
                Session("BDAction") = "None"
            Case Else
                Session("BDAction") = "Other"
        End Select

        'jrs 2010/09/23
        Exit Sub
localHdlr:
        LoadMessage(Err.Description, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")
    End Sub

    Private Sub LoadGroupMemberVals(ByRef sTargetVector As String, ByVal TargetCol As Integer, ByVal sDim1 As String, ByVal sVar As String,
    ByVal fileIdx As Integer, ByVal bComp As Boolean, ByVal PointPos As Integer,
    ByVal sDim2 As String, ByVal sDim3 As String, ByRef ff As Formula, ByVal bGroup As Boolean)
        '1. sTargetVector, is Y, X or Z to indicate where to store result
        '2. TargetCol, indicates where in the 2-dimensioned array to store the country
        '3. sDim1, is the name of the country
        '4. sVar, name of the variable
        '5. fileIdx index of the file to be used
        '6. bComp is true if using a formula
        '7. PointPos is the column where to save the result for decompose (0) when loading a new group

        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        Dim sDef As String, sUn As String
        Dim idxVar As Integer

        Dim bHistFor As Boolean
        bHistFor = (Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory)

        If bComp Then
            If ff Is Nothing Then
                LoadMessage("Formula is not properly setup", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            If ff.Formula2 > "" Then
                gLoadCompHisFor(Nothing, sDim1, sDim2, sDim3, fileIdx, -1, Session("bFillHis"),
                Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1), Session("COUNTE"), Session("aFormulas"),
                Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("bLoadForDisplaySuccess"), Me,
                Session("FinYear"), Session("VARN"), Session("FileforDisplay"), IfsPars, Session("PLOT"), Session("bFillHisOnlyHoles"), ff,
                Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("StaYearHis")(Session("COUNTE")), Session("mnuSQLite"))
            Else
                Session("COUNTE") = Session("COUNTE") + 1
                Dim f As Formula
                f = ff
                gLoadComp(Session("COUNTE"), f.Formula_Renamed, sVar, sDim1, sDim2, sDim3,
                f.Unit, -1, bGroup, fileIdx, bHistFor, False, f, Session("bFillHis"),
                Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                False, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"))

                'jrs 2025/02/05
                If Session("bLoadForDisplaySuccess") Then
                    Session("VARN")(1, Session("COUNTE")) = sVar
                    Session("VARN")(0, Session("COUNTE")) = sVar
                    If Not (IsDBNull(sDim1)) Then
                        Session("GR1N")(Session("COUNTE")) = sDim1
                        Session("FDNG1")(Session("COUNTE")) = sDim1
                    End If

                    If Not (IsDBNull(sDim2)) Then
                        Session("GR2N")(Session("COUNTE")) = sDim2
                        Session("FDNG2")(Session("COUNTE")) = sDim2
                    End If

                    If Not (IsDBNull(sDim3)) Then
                        Session("GR3N")(Session("COUNTE")) = sDim3
                        Session("FDNG3")(Session("COUNTE")) = sDim3
                    End If

                    'If Not (IsDBNull(rsList.Rows.Item(iv - 1)("DisplayUnits"))) Then
                    '    Session("GR4N")(Session("COUNTE")) = rsList.Rows.Item(iv - 1)("DisplayUnits")
                    'End If
                End If
            End If
        Else
            Session("COUNTE") = Session("COUNTE") + 1

            'jrs 2011/12/19 added and IfsVars.Item(sVar).Index
            If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" And IfsVars.Item(sVar).Index < 0 Then
                gLoadForDisplayHis(sVar, sDim1, -1, sDim2, -1, sDim3, -1,
                bGroup, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory,
                False, False, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                If (PointPos = 0) Then
                    gLoadForDisplayHisSaveLbls(Session("COUNTE"), sVar, sDim1, bGroup, sDef, sUn, "", Session("FileNumbr"), Session("VARN"),
                    Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                End If
            Else
                Dim iRowPosition As Integer, iFirstYear As Integer
                If bHistFor Then
                    iRowPosition = LastYearHistory - FirstYearHistory + 1
                    iFirstYear = FirstYearForecast
                Else
                    iRowPosition = 1
                    If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then 'JRS 2006/05/22
                        iFirstYear = FirstYearForecast
                    Else
                        iFirstYear = FirstYearHistory
                    End If
                End If
                gLoadForDisplayNET(sVar, idxVar, sDim1, -1, sDim2, -1, sDim3, -1, "", fileIdx, True, True, bGroup, 1, Session("PLOT"), Session("COUNTE"),
                iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
                'gLoadForDisplay(sVar, idxVar, sDim1, -1, sDim2, -1, sDim3, -1, "", fileIdx, True, True, bGroup, 1, Session("PLOT"), Session("COUNTE"),
                'iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                If (PointPos = 0) Then
                    gLoadForDisplaySaveLbls(Session("COUNTE"), idxVar, sDim1, sDim2, sDim3, bGroup, fileIdx, "",
                    Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                End If
            End If
        End If
        If bHistFor And Not bComp Then
            Dim FinAll As Integer
            If PointPos = 0 Then
                FinAll = Session("FinYear")(Session("COUNTE"))
            End If
            gLoadForDisplayHis(sVar, sDim1, -1, sDim2, -1, sDim3, -1,
            bGroup, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory - 1,
            False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
            Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
            Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))

            If PointPos = 0 Then
                Session("FinYear")(Session("COUNTE")) = LastYearHistory - FirstYearHistory + FinAll
            End If
        End If

        'Move Results from session("PLOT") to Target
        Dim i As Integer
        If PointPos > 0 Then
            For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                'DecPoints(PointPos).setValatDim(Session("PLOT")(i, Session("COUNTE")), sTargetVector, i, TargetCol)
            Next i
            'jrs 2010/10/27
            'DecPoints(PointPos).SetFirstYearNotNullAtPos(TargetCol)
            'Reset session("COUNTE")
            Session("COUNTE") = Session("COUNTE") - 1
        End If

    End Sub

    Private Sub LoadBD(ByVal sVar As String, ByVal Target As Object)
        'Load sVar for all Members of GR1N
        Dim i As Integer
        For i = 1 To Session("pCOUNTE") / 2
            LoadBDMemberVals(Target, i, Session("GR1N")(i), sVar, Session("fileIdxZ"))
        Next i

    End Sub

    Private Sub LoadBDMemberVals(ByRef TargetVector As Object, ByVal TargetCol As Integer, ByVal sDim1 As String, ByVal sVar As String,
    ByVal fileIdx As Integer)
        '1. TargetVector, is vector to store results
        '2. TargetCol, indicates where in the 2-dimensioned array to store the country
        '3. sDim1, is the name of the country
        '4. sVar, name of the variable
        '5. fileIdx index of the file to be used

        Dim bHistFor As Boolean
        bHistFor = (Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory)

        'jrs 2011/04/07 when using historic mode,
        'better to load forecast variables for Bubble Diameter
        '    If UCase(ActiveBase) = "IFSBASEHIST.RUN" Then
        '        gLoadForDisplayHis sVar, sDim1, -1, "", -1, "", -1, _
        '        True, True, Null, 1, TargetVector, TargetCol, FirstYearHistory, LastYearHistory, _
        '        False, False, False, "", bFillHis, buseAllHis, -1, bFillHisOnlyHoles
        '    Else
        Dim iRowPosition As Integer, iFirstYear As Integer
        If bHistFor Then
            iRowPosition = LastYearHistory - FirstYearHistory + 1
            iFirstYear = FirstYearForecast
        Else
            iRowPosition = 1
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then 'JRS 2006/05/22
                iFirstYear = FirstYearForecast
            Else
                iFirstYear = FirstYearHistory
            End If
        End If
        'jrs 2011/12/16
        If fileIdx < 0 Then
            fileIdx = 1
        End If

        gLoadForDisplayNET(sVar, -1, sDim1, -1, "", -1, "", -1, "", fileIdx, True, True, Nothing, 1, TargetVector, TargetCol,
        iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
        Session("IFs_MDB2Active"), Session("CurrencyVal"))
        'gLoadForDisplay(sVar, -1, sDim1, -1, "", -1, "", -1, "", fileIdx, True, True, Nothing, 1, TargetVector, TargetCol,
        'iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
        'Session("IFs_MDB2Active"), Session("CurrencyVal"))
        '    End If

        If bHistFor Then
            Dim Fin As Integer
            Dim IfsPars As New myParameters
            IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
            Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
            Session("PutFdrvRun"), Session("CurrencySym"))

            Dim sDef As String, sUn As String
            Fin = Session("FinYear")(TargetCol)
            gLoadForDisplayHis(sVar, sDim1, -1, "", -1, "", -1,
            Nothing, 1, TargetVector, TargetCol, FirstYearHistory, LastYearHistory - 1,
            False, True, Session("bFillHis"), Session("buseAllHis"), -1,
            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
            Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
            Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
            Session("FinYear")(TargetCol) = LastYearHistory - FirstYearHistory + Fin
        End If

    End Sub

    Private Sub ColorDistribution()
        'For All Regions do the same color for region
        If Session("pCOUNTE") / 2 = NR Then
            ColorDistributionNR()
            Exit Sub
        End If

        'jrs 2010/09/28
        'Distribute Colors
        Dim GreenIdx As Integer, OrangeIdx As Integer, BlueIdx As Integer
        Dim YellowIdx As Integer, NavyBlueIdx As Integer, RedIdx As Integer
        Dim i As Integer
        If Session("bBubble") Then
            If InStr(Session("VARN")(1, Session("pCOUNTE") + 1), "GDP") > 0 Then
                GreenIdx = 4
                OrangeIdx = 1
                BlueIdx = 2
                YellowIdx = 3
                NavyBlueIdx = 0
                RedIdx = 5
            Else
                GreenIdx = 1
                OrangeIdx = 0
                BlueIdx = 2
                YellowIdx = 3
                NavyBlueIdx = 4
                RedIdx = 5
            End If
        Else
            GreenIdx = 0
            OrangeIdx = 1
            BlueIdx = 2
            YellowIdx = 3
            NavyBlueIdx = 4
            RedIdx = 5
        End If

        'jrs 2010/09/14 Load Colors
        ReDim Session("RColor")(AMAX(Session("pCOUNTE") / 2, 10)), Session("GColor")(AMAX(Session("pCOUNTE") / 2, 10)), Session("BColor")(AMAX(Session("pCOUNTE") / 2, 10))
        'Green for Africa 0, 150, 0
        Session("RColor")(GreenIdx) = 0
        Session("GColor")(GreenIdx) = 150
        Session("BColor")(GreenIdx) = 0
        'Orange for Asia 255, 102, 0
        Session("RColor")(OrangeIdx) = 255
        Session("GColor")(OrangeIdx) = 102
        Session("BColor")(OrangeIdx) = 0
        'Blue for Europe 0, 0, 150
        Session("RColor")(BlueIdx) = 0
        Session("GColor")(BlueIdx) = 0
        Session("BColor")(BlueIdx) = 150
        'Yellow for Latam&Car 255, 255, 0
        Session("RColor")(YellowIdx) = 255
        Session("GColor")(YellowIdx) = 255
        Session("BColor")(YellowIdx) = 0
        'Navy Blue for North America 0, 191, 255
        Session("RColor")(NavyBlueIdx) = 0
        Session("GColor")(NavyBlueIdx) = 191
        Session("BColor")(NavyBlueIdx) = 255
        'Red for Oceania 255, 0, 0
        Session("RColor")(RedIdx) = 255
        Session("GColor")(RedIdx) = 0
        Session("BColor")(RedIdx) = 0
        'brown 130, 43, 0
        Session("RColor")(6) = 130
        Session("GColor")(6) = 43
        Session("BColor")(6) = 0
        'magenta 128, 0, 128
        Session("RColor")(7) = 128
        Session("GColor")(7) = 0
        Session("BColor")(7) = 128
        'pink 255, 153, 204
        Session("RColor")(8) = 255
        Session("GColor")(8) = 153
        Session("BColor")(8) = 204
        'gray 128, 128, 128
        Session("RColor")(9) = 128
        Session("GColor")(9) = 128
        Session("BColor")(9) = 128
        'Load randomly the remaining NR - 10 colors
        For i = 10 To UBound(Session("RColor"))
            Session("RColor")(i) = (Rnd() * 1000) Mod 256
            Session("GColor")(i) = (Rnd() * 1000) Mod 256
            Session("BColor")(i) = (Rnd() * 1000) Mod 256
        Next i

    End Sub

    Private Sub ColorDistributionNR()
        'jrs 2010/09/28
        'Distribute Colors
        Dim GreenIdx As Integer, OrangeIdx As Integer, BlueIdx As Integer
        Dim YellowIdx As Integer, NavyBlueIdx As Integer, RedIdx As Integer

        Dim sql As String
        Dim db As String
        Dim rs As DataTable
        db = Session("IFs_MDB2")
        sql = "SELECT GroupName, RegionNo " &
        "From GListId, GList, " & GroupIDNew & ", " & Grouping &
        " WHERE GListId.Code = GList.GListCode And " &
        "GList.Region = " & GroupIDNew & ".GroupName AND " &
        GroupIDNew & ".GroupNum = " & Grouping & ".GroupNo AND " &
        "GListId.Description = 'UN Regions'"

        SetDataTablefromAccess(db, sql, "", rs)
        Dim sGroupNames() As String
        ReDim sGroupNames(NR)
        Dim i As Integer
        Dim rr As Integer = 0

        Do While rr < rs.Rows.Count
            sGroupNames(rs.Rows(rr).Item("RegionNo")) = rs.Rows(rr).Item("GroupName")
            rr = rr + 1
        Loop

        'jrs 2010/09/14 Load Colors
        ReDim Session("RColor")(NR), Session("GColor")(NR), Session("BColor")(NR)

        For i = 1 To NR
            If InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "AFRICA") > 0 Then
                'Green for Africa 0, 150, 0
                Session("RColor")(i - 1) = 0
                Session("GColor")(i - 1) = 150
                Session("BColor")(i - 1) = 0
            ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "ASIA") > 0 Then
                'Orange for Asia 255, 102, 0
                Session("RColor")(i - 1) = 255
                Session("GColor")(i - 1) = 102
                Session("BColor")(i - 1) = 0
            ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "EUROPE") > 0 Then
                'Blue for Europe 0, 0, 150
                Session("RColor")(i - 1) = 0
                Session("GColor")(i - 1) = 0
                Session("BColor")(i - 1) = 150
            ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "LATAM") > 0 Then
                'Yellow for Latam&Car 255, 255, 0
                Session("RColor")(i - 1) = 255
                Session("GColor")(i - 1) = 255
                Session("BColor")(i - 1) = 0
            ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "NORTH") > 0 Then
                'Navy Blue for North America 0, 191, 255
                Session("RColor")(i - 1) = 0
                Session("GColor")(i - 1) = 191
                Session("BColor")(i - 1) = 255
            ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "OCEANIA") > 0 Then
                'Red for Oceania 255, 0, 0
                Session("RColor")(i - 1) = 255
                Session("GColor")(i - 1) = 0
                Session("BColor")(i - 1) = 0
            Else
                'Random if not found
                Session("RColor")(i - 1) = (Rnd() * 1000) Mod 256
                Session("GColor")(i - 1) = (Rnd() * 1000) Mod 256
                Session("BColor")(i - 1) = (Rnd() * 1000) Mod 256
            End If
        Next i

    End Sub

End Class

