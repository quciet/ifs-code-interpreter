Imports System.Data
Imports System.Data.SQLite

Partial Class frm_MortDisplay
    Inherits Page
    'cdv 2021/03/11 All references to Session(mnuSQLite) changed to treat it as a string instead of a bool
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Mortality Display: J - Curve (using Deaths per Thousand)"
        End If

        'jrs 2011/05/04 WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub
    Dim vSQLiteCon As SqliteConnection
    Dim vSQLiteCommand As SqliteCommand
    Dim vSQLiteReader As SqliteDataReader
    Dim vConnString As String
    Dim vSQLiteCommand2 As SqliteCommand
    Dim vSQLiteReader2 As SqliteDataReader
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        wmMain.UserData = "jsolorza::976739975"

        wmMain.MenuBar.ShowOnMouseOver = True

        wmMain.ClearPixelImage = "blank.gif"
        wmMain.PopupIcon = "./images/Popup.gif"
        wmMain.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        wmMain.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        wmMain.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        wmMain.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        wmMain.SelectedPopupIcon = "./images/SelectedPopup.gif"
        wmMain.ScrollEnabled = True
        'jrs 2012/01/17
        wmMain.AccessorKeyCode = 27
        'jrs 2012/01/24
        wmMain.ButtonFont.Size = "12px"
        wmMain.ItemFont.Size = "12px"

        AddHandler wmMain.ServerItemClick, AddressOf ItemClickHandlerMain

        'JAM 2020-06-14
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") = "True" Then
                'JAM 2020-05-04
                If Not Session("SQLiteDB") Is Nothing Then
                    vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";" '"ifsPopWork.db;"
                Else
                    vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPop.db;"
                End If

                vSQLiteCon = New SqliteConnection(vConnString) '& "New=true;"
                vSQLiteCon.Open()
            End If
        End If





        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_e2bf"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Grouping Options"
        cswmGroup1.ID = "mnuGroups"
        If Session("bUseReg") Is Nothing Then
            Session("bUseReg") = True
            Session("bUseGroup") = False
            Session("bUseGroupDec") = False
            Session("bUseGList") = False
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using Countries/Regions"
        cswmItem.ID = "mnuReg"
        If Session("bUseReg") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using Groups"
        cswmItem.ID = "mnuGroup"
        If Session("bUseGroup") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using Decomposed Groups"
        cswmItem.ID = "mnuGroupDec"
        If Session("bUseGroupDec") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using G-Lists"
        cswmItem.ID = "mnuGList"
        If Session("bUseGList") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Age Options"
        cswmGroup1.ID = "mnuAge"
        If Session("bUse100") Is Nothing Then
            Session("bUse100") = False
            Session("bUseInfants") = True
            Session("bUseAdltsOnly") = False
            Session("bUseGBDage") = False
        End If

        cswmItem = cswmGroup1.Items.Add()
        'jrs 2026/01/14 cswmItem.Caption = "Include 100+"
        cswmItem.Caption = "Include oldest age group"
        cswmItem.ID = "mnu100"
        If Session("bUse100") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Include Infants"
        cswmItem.ID = "mnuInfants"
        If Session("bUseInfants") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Adults Only (25+)"
        cswmItem.ID = "mnuAdltsOnly"
        If Session("bUseAdltsOnly") Then
            cswmItem.IconImage = "checkbox.gif"
            wmMain.Groups("mnuAge").Items("mnuInfants").Enabled = False
        Else
            cswmItem.IconImage = "blank.gif"
            wmMain.Groups("mnuAge").Items("mnuInfants").Enabled = True
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use GBD age categories"
        cswmItem.ID = "mnuGBDage"
        If Session("bUseGBDage") Then
            cswmItem.IconImage = "checkbox.gif"
            wmMain.Groups("mnuAge").Items("mnu100").Caption = "Include 80+"
        Else
            cswmItem.IconImage = "blank.gif"
            'jrs 2026/01/14 wmMain.Groups("mnuAge").Items("mnu100").Caption = "Include 100+"
            wmMain.Groups("mnuAge").Items("mnu100").Caption = "Include oldest age group"
        End If

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Display Type Options"
        cswmGroup1.ID = "mnuDispType"
        If Session("GraphType") Is Nothing Then
            Session("GraphType") = "J"
        End If
        'jrs 2011/05/04
        If IsNumeric(Session("GraphType")) Then
            Session("GraphType") = "J"
        End If
        If Session("bUseDeaths") Is Nothing Then
            Session("bUseDeaths") = False
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "J-Curve"
        cswmItem.ID = "mnuJCurve"
        If Session("GraphType") = "J" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Probability of Dying as a Child 0-4 (Longitudinal)"
        cswmItem.ID = "mnuChldProb"
        cswmItem.Enabled = True
        If Session("GraphType") = "ChldProb" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
            If Session("bUseDeaths") Then
                cswmItem.Enabled = False
            End If
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Child Mortality Rate per Year 0-4 (Longitudinal)"
        cswmItem.ID = "mnuChldRate"
        If Session("GraphType") = "ChldRate" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Probability of Dying as an Adult(Longitudinal)"
        cswmItem.ID = "mnuAdltProb"
        cswmItem.Enabled = True
        If Session("GraphType") = "AdltProb" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
            If Session("bUseDeaths") Then
                cswmItem.Enabled = False
            End If
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Adult Mortality Rate per Year (Longitudinal)"
        cswmItem.ID = "mnuAdltRate"
        If Session("GraphType") = "AdltRate" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        If Session("FirstAge") Is Nothing Then
            Session("FirstAge") = 3
            Session("LastAge") = 12
        End If
        Dim sAdltAge As String
        If Session("LastAge") = NCohorts Then
            sAdltAge = "Define Adult Age (" & CStr(Session("FirstAge") * 5) & " to " & CStr((NCohorts - 1) * 5) & "+)"
        Else
            sAdltAge = "Define Adult Age (" & CStr(Session("FirstAge") * 5) & " to " & CStr(Session("LastAge") * 5 - 1) & ")"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = sAdltAge
        cswmItem.ID = "mnuDefAdltAge"
        cswmItem.IconImage = "blank.gif"

        'jrs 2016/06/22
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Life Expectancy by Age"
        cswmItem.ID = "mnuDispLE"
        If Session("GraphType") = "LE" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
            'jrs 2020/07/01
            lstDim2.Enabled = True
        End If


        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Other Display Options"
        cswmGroup1.ID = "mnuOtherDisp"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Deaths"
        cswmItem.ID = "mnuDeaths"
        If Session("bUseDeaths") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2013/09/04
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Help"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        If Not Page.IsPostBack Then
            'jrs 2011/09/23
            Session("AboutParent") = ""

            'jrs 2014/09/03
            If Session("GraphType") = "J" Then
                'Redimension array to fit all cohorts, and as many region,gender,type combinations as possible
                ReDim Session("mPLOT")(NCohorts + 1, MaxVariables)
            Else
                ReDim Session("mPLOT")(Session("LastYear") - Session("FirstYear") + 1, MaxVariables)
            End If
            ReDim Session("mLegends")(5, MaxVariables)
            'Load Geografic List
            LoadDim1s()
            'Load Mortality Types
            Dim idx As Integer
            idx = IfsVars.Item("DEATHCAT").Index
            Dim DimNum As Integer
            DimNum = VDim2(idx)
            Dim i As Integer
            For i = 1 To MaxVal(DimNum) - 1
                lstDim2.Items.Add(GRPS$(DimNum, i))
            Next i
            lstDim2.Items.Add("TotComDis")
            lstDim2.Items.Add("TotNonComDis")
            lstDim2.Items.Add("TotInjuries")
            lstDim2.Items.Add("Total")
            lstDim2.SelectedIndex = lstDim2.Items.Count - 1
            'Load Gender
            DimNum = VDim3(idx)
            'cdv 2021/01/28 ****START****
            If bMaleFirst Then
                For i = 1 To MaxVal(DimNum) - 1
                    lstDim3.Items.Add(GRPS$(DimNum, i))
                Next i
            Else
                lstDim3.Items.Add(GRPS$(DimNum, 2))
                lstDim3.Items.Add(GRPS$(DimNum, 1))
                lstDim3.Items.Add(GRPS$(DimNum, 3))
            End If

            'cdv 2021/01/28 ****END****
            lstDim3.SelectedIndex = lstDim3.Items.Count - 1
            'Load Files
            LoadFiles()
            'Load Years
            Dim iLastYear As Integer
            Dim db As String
            'JAM 2020-06-14
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                db = Session("PutFdrvRun") & "ifsPopWork.mdb"
                Dim rs As DataTable
                SetDataTablefromAccess(db, "SELECT MAX(Years) as MaxYrs FROM MorDetBaseCase", "", rs)
                iLastYear = rs.Rows(0).Item("MaxYrs")

            Else

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT Max(TimeId) As MaxYear FROM Mor_Det_Base_Case"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                iLastYear = 0
                While (vSQLiteReader.Read())
                    iLastYear = vSQLiteReader("MaxYear")

                End While

            End If





            For i = Session("FirstYear") To iLastYear
                lstYears.Items.Add(i)
            Next i
            lstYears.SelectedIndex = 0

        End If

    End Sub

    Private Sub LoadDim1s()
        lstDim1.Items.Clear()
        Dim db As String, j As Integer
        Dim valtbl As DataTable
        If (Session("bUseGroup") Or Session("bUseGroupDec")) Then
            db = Session("IFs_MDB2Active")
            SetDataTablefromAccess(db, "SELECT * FROM [" & GroupIDNew$ & "] ORDER BY GroupName", "GroupName", valtbl)
            j = 0
            While valtbl.Rows.Count > j
                lstDim1.Items.Add(valtbl.Rows(j).Item("GroupName"))
                j = j + 1
            End While
        ElseIf Session("bUseGList") Then 'jrs 2009/09/03 changed to checked 'jrs 2006/01/17
            db = Session("IFs_MDB2Active")
            SetDataTablefromAccess(db, "SELECT * FROM GListId ORDER BY Description", "", valtbl)
            j = 0
            While valtbl.Rows.Count > j
                lstDim1.Items.Add(valtbl.Rows(j).Item("Description"))
                j = j + 1
            End While
        Else
            Dim i As Integer
            For i = 1 To MaxVal(1) - 1
                lstDim1.Items.Add(GRPS$(1, i))
            Next i
        End If
        If lstDim1.Items.Count > 0 Then
            lstDim1.SelectedIndex = 0
        End If
        cmdClear_Click(Me, New EventArgs)
        pnlAdltDef.Visible = False
    End Sub

    Sub ItemClickHandlerMain(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        Dim sAdltAge As String
        If Session("LastAge") = NCohorts Then
            sAdltAge = "Define Adult Age (" & CStr(Session("FirstAge") * 5) & " to " & CStr((NCohorts - 1) * 5) & "+)"
        Else
            sAdltAge = "Define Adult Age (" & CStr(Session("FirstAge") * 5) & " to " & CStr(Session("LastAge") * 5 - 1) & ")"
        End If

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Continue"
                mnuExit_Click()
            Case "Using Countries/Regions"
                mnuReg_Click()
            Case "Using Groups"
                mnuGroup_Click()
            Case "Using Decomposed Groups"
                mnuGroupDec_Click()
            Case "Using G-Lists"
                mnuGList_Click()
            Case "Include oldest age group", "Include 80+" 'jrs 2026/01/14
                mnu100_Click()
            Case "Include Infants"
                mnuInfants_Click()
            Case "Adults Only (25+)"
                mnuAdults_Click()
            Case "Use GBD age categories"
                mnuGBDage_Click()
            Case "J-Curve"
                mnuDispJ_Click()
            Case "Probability of Dying as a Child 0-4 (Longitudinal)"
                mnuDispInfant_Click()
            Case "Child Mortality Rate per Year 0-4 (Longitudinal)"
                mnuDispChildRate_Click()
            Case "Probability of Dying as an Adult(Longitudinal)"
                mnuDispAdults_Click()
            Case "Adult Mortality Rate per Year (Longitudinal)"
                mnuDispAdultsRate_Click()
            Case sAdltAge
                mnuDefineAdults_Click()
            Case "Use Deaths"
                mnuDeaths_Click()
            Case "Life Expectancy by Age" 'jrs 2016/06/22
                mnuDispLE_Click()
        End Select
    End Sub

    Private Sub mnuExit_Click()
        If lblJS.Visible Or grdMort.Visible Then
            cmdClear_Click(Me, New EventArgs)
        Else
            Session.Remove("mPLOT")
            Session.Remove("mLegends")
            Session.Remove("TotalGraphs")
            Session.Remove("bSuccess")
            Session.Remove("GraphType")
            Session.Remove("FirstAge")
            Session.Remove("LastAge")

            Session.Remove("bUseReg")
            Session.Remove("bUseGroup")
            Session.Remove("bUseGroupDec")
            Session.Remove("bUseGList")

            Session.Remove("bUse100")
            Session.Remove("bUseInfants")
            Session.Remove("bUseAdltsOnly")
            Session.Remove("bUseGBDage")

            Session.Remove("bUseDeaths")
            'jrs 2011/05/04
            If Session("DisplayParent") = Nothing Then
                Session("DisplayParent") = "frm_MainMenu.aspx"
            End If

            Support.FormManager.Show(Me, Session("DisplayParent"))
        End If

    End Sub

    Private Sub LoadFiles()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim fs
        Dim BASE
        Dim currRun
        fs = CreateObject("Scripting.FileSystemObject")
        Dim sWorkingFile As String
        'JAM 2020-06-15
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            sWorkingFile = Session("PutFdrvRun2") + "\IFsPopWork.mdb"

            If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsPopHist.mdb")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsPop.mdb")
            End If
            lstFiles.Items.Clear()
            lstFiles.Items.Add("Working File")
            lstFiles.Items.Add("Base File")

            Dim sFile As String
            ' Returns filename with specified extension. If more than one *.ini
            ' file exists, the first file found is returned.
            sFile = Dir(Session("PutFdrvRun2") + "\ifsPop*.mdb")

            Do While (sFile <> "")
                If UCase(sFile) <> UCase(BASE.Name) And
            UCase(sFile) <> UCase(sWorkingFile) And
            UCase(sFile) <> UCase("IFSPOPWORK.MDB") And
            UCase(sFile) <> UCase("IFSPOPHIST.MDB") Then 'jrs 2009/07/02 added ifsPopHist, since removing size validation
                    'jrs 2009/07/02
                    '            Set currRun = fs.GetFile(PutFdrvRun2$ + "\" + sFile)
                    '            If BASE.Size = currRun.Size Then
                    '                lstFiles.AddItem mid(Replace(Replace(sFile, ".mdb", ""), ".MDB", ""), 7)
                    '            End If
                    lstFiles.Items.Add(Mid(Replace(Replace(sFile, ".mdb", ""), ".MDB", ""), 7))
                End If
                ' Call Dir again without arguments to return the next *.INI file in the
                ' same directory.
                sFile = Dir()
            Loop

        Else
            sWorkingFile = Session("PutFdrvRun2") + "\IFsPopWork.db"

            If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN.DB" Then
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsPopHist.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsPop.db")
            End If
            lstFiles.Items.Clear()

            lstFiles.Items.Add("Working File")
            lstFiles.Items.Add("Base File")

            Dim sFile As String
            ' Returns filename with specified extension. If more than one *.ini
            ' file exists, the first file found is returned.
            sFile = Dir(Session("PutFdrvRun2") + "\ifsPop*.db")

            Do While (sFile <> "")
                If UCase(sFile) <> UCase(BASE.Name) And
            UCase(sFile) <> UCase(sWorkingFile) And
            UCase(sFile) <> UCase("IFSPOPWORK.DB") And
            UCase(sFile) <> UCase("IFSPOPHIST.DB") Then 'jrs 2009/07/02 added ifsPopHist, since removing size validation
                    'jrs 2009/07/02
                    '            Set currRun = fs.GetFile(PutFdrvRun2$ + "\" + sFile)
                    '            If BASE.Size = currRun.Size Then
                    '                lstFiles.AddItem mid(Replace(Replace(sFile, ".mdb", ""), ".MDB", ""), 7)
                    '            End If
                    lstFiles.Items.Add(Mid(Replace(Replace(sFile, ".db", ""), ".DB", ""), 7))
                End If
                ' Call Dir again without arguments to return the next *.INI file in the
                ' same directory.
                sFile = Dir()
            Loop

        End If
        'jrs 2026/01/26 changed default to base
        lstFiles.SelectedIndex = 1
    End Sub

    Protected Sub cmdGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGraph.Click
        If Not grdMort.Visible Then
            LoadSet()
        Else
            grdMort.Visible = False
        End If
        If Session("TotalGraphs") > 0 Then
            DrawGraph(Session("TotalGraphs"))
            cmdGraph.Enabled = False
            pnlMort.Visible = False
            '    'jrs 2010/03/02 Comparison
            '    If Not bComparisonOn Then
            '        ReDim CompBackup(UBound(mPLOT, 1), TotalGraphs)
            '        ReDim CompLabelBackup(5, TotalGraphs)
            '        mnuComp.Visible = True
            '    End If
        End If

        cmdTable.Enabled = True

    End Sub

    Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        lblJS.Visible = False
        grdMort.Visible = False
        cmdGraph.Enabled = True
        cmdTable.Enabled = True
        pnlMort.Visible = True
        pnlButtons.Visible = True
        'mnuSaveHdr.Visible = False
        'mnuPrint.Visible = False
        ''jrs 2010/03/04
        'bComparisonOn = False
        'mnuComp.Visible = False
        'mnuCompRatAB.Checked = False
        'mnuCompRatBA.Checked = False
        'mnuCompDiffAB.Checked = False
        'mnuCompDiffBA.Checked = False
    End Sub

    Private Sub LoadSet()
        If Session("GraphType") = "J" Then
            LoadSetJ()
        ElseIf Session("GraphType") = "LE" Then 'jrs 2016/06/22
            LoadSetLE()
        Else
            LoadSetAge()
        End If
    End Sub

    Private Sub LoadSetJ()
        Dim f As Integer
        Dim r As Integer, T As Integer, g As Integer, y As Integer
        Dim bGrp As Boolean
        Dim dbG As String
        Dim rsG As DataTable
        Dim iGL As Integer
        Dim sDim1 As String
        Dim iDim1 As Integer

        dbG = Session("IFs_MDB2Active")

        Session("TotalGraphs") = 0

        'jrs 2010/03/04
        'Open all needed files
        Dim sFileNames() As String
        ReDim sFileNames(lstFiles.Items.Count)

        Dim dbMs() As OleDb.OleDbConnection
        ReDim dbMs(lstFiles.Items.Count)
        'loop through files
        For f = 0 To lstFiles.Items.Count - 1
            If lstFiles.Items(f).Selected Then
                Select Case f
                    Case 0
                        'JAM 2020-06-15
                        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                            sFileNames(f) = "ifsPopWork.mdb"
                        Else
                            sFileNames(f) = "ifsPopWork.db"
                        End If

                    Case 1
                        'JAM 2020-06-15
                        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                            sFileNames(f) = "ifsPop.mdb"
                        Else
                            sFileNames(f) = "ifsPop.db"
                        End If
                    Case Else
                        'JAM 2020-06-15
                        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                            sFileNames(f) = "ifsPop" & lstFiles.Items(f).Text & ".mdb"

                        Else
                            sFileNames(f) = "ifsPop" & lstFiles.Items(f).Text & ".db"
                        End If
                End Select
                'dbMs(f) = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileNames(f))
                'dbMs(f).Open()
            End If
        Next f

        'loop through regions
        For r = 0 To lstDim1.Items.Count - 1
            If lstDim1.Items(r).Selected Then
                sDim1 = lstDim1.Items(r).Text
                iDim1 = r + 1
                'for later: consider g-lists and decomposed groups
                Select Case True
                    Case Session("bUseReg")
                        bGrp = False
                    Case Session("bUseGroup")
                        bGrp = True
                    Case Session("bUseGroupDec")
                        bGrp = False
                        SetDataTablefromAccess(dbG, "SELECT * FROM " & GroupIDNew & " i, " &
                        Grouping & " d WHERE i.GroupNum = d.GroupNo AND i.GroupName = """ &
                        lstDim1.Items(r).Text & """ ORDER BY Region", "", rsG)
                        iGL = 0
                    Case Session("bUseGList")
                        SetDataTablefromAccess(dbG, "SELECT * FROM GList d, GListId i WHERE i.Code = d.GListCode AND i.Description = """ & lstDim1.Items(r).Text & """", "", rsG)
                        iGL = 0
                End Select

                Do While True 'loop for G-Lists and Dec. Groups
                    If Session("bUseGList") Or Session("bUseGroupDec") Then
                        sDim1 = rsG.Rows(iGL).Item("Region")
                        iDim1 = IFsRegs.Item(sDim1).Index
                        If Session("bUseGList") Then
                            bGrp = rsG.Rows(iGL).Item("Group")
                        End If
                    End If
                    'loop through types
                    For T = 0 To lstDim2.Items.Count - 1
                        If lstDim2.Items(T).Selected Then
                            'loop through gender
                            For g = 0 To lstDim3.Items.Count - 1
                                If lstDim3.Items(g).Selected Then
                                    'loop through years
                                    For y = 0 To lstYears.Items.Count - 1
                                        If lstYears.Items(y).Selected Then
                                            'loop through files
                                            For f = 0 To lstFiles.Items.Count - 1
                                                If lstFiles.Items(f).Selected Then
                                                    If Session("TotalGraphs") < MaxVariables Then
                                                        Session("TotalGraphs") = Session("TotalGraphs") + 1
                                                        'LoadMortality(Session("TotalGraphs"), sFileNames(f), dbMs(f), sDim1, iDim1, bGrp,
                                                        'lstDim2.Items(T).Text, lstDim3.Items(g).Text, lstYears.Items(y).Text, False)

                                                        LoadMortality(Session("TotalGraphs"), sFileNames(f), sDim1, iDim1, bGrp,
                                                        lstDim2.Items(T).Text, lstDim3.Items(g).Text, lstYears.Items(y).Text, False)


                                                        'jrs 2009/07/02
                                                        If Not Session("bSuccess") Then
                                                            Session("TotalGraphs") = Session("TotalGraphs") - 1
                                                        End If
                                                    End If
                                                End If 'if file selected
                                            Next f
                                        End If 'if Year selected
                                    Next y
                                End If 'if gender selected
                            Next g
                        End If 'if type selected
                    Next T
                    If Session("bUseGList") Or Session("bUseGroupDec") Then
                        iGL = iGL + 1
                        If rsG.Rows.Count = iGL Then Exit Do
                    Else
                        Exit Do
                    End If
                Loop
            End If 'if region selected
        Next r

        'jrs 2010/03/04
        'Close used files
        'loop through files

        'JAM 2020-06-15
        'For f = 0 To lstFiles.Items.Count - 1
        '    If lstFiles.Items(f).Selected Then
        '        dbMs(f).Close()
        '    End If
        'Next f

        'dbG.Close()

        'jrs 2008/11/11
        'mnuPrint.Visible = True
        'mnuSaveHdr.Visible = True

    End Sub

    Private Sub LoadSetAge()
        Dim f As Integer
        Dim r As Integer, T As Integer, g As Integer, y As Integer
        Dim bGrp As Boolean
        Dim dbG As String
        Dim rsG As DataTable
        Dim sDim1 As String
        Dim iDim1 As Integer
        Dim iGL As Integer

        dbG = Session("IFs_MDB2Active")

        Session("TotalGraphs") = 0

        'Open all needed files
        Dim sFileNames() As String
        ReDim sFileNames(lstFiles.Items.Count)
        Dim dbMs() As OleDb.OleDbConnection
        ReDim dbMs(lstFiles.Items.Count)
        'loop through files
        For f = 0 To lstFiles.Items.Count - 1
            If lstFiles.Items(f).Selected Then
                Select Case f
                    Case 0
                        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                            sFileNames(f) = "ifsPopWork.mdb"
                        Else
                            sFileNames(f) = "ifsPopWork.db"
                        End If
                    Case 1
                        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                            sFileNames(f) = "ifsPop.mdb"
                        Else
                            sFileNames(f) = "ifsPop.db"
                        End If
                    Case Else
                        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                            sFileNames(f) = "ifsPop" & lstFiles.Items(f).Text & ".mdb"

                        Else
                            sFileNames(f) = "ifsPop" & lstFiles.Items(f).Text & ".db"
                        End If
                End Select
                ''dbMs(f) = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileNames(f))
                ''dbMs(f).Open()
            End If
        Next f

        'loop through regions
        For r = 0 To lstDim1.Items.Count - 1
            If lstDim1.Items(r).Selected Then
                sDim1 = lstDim1.Items(r).Text
                iDim1 = r + 1
                'for later: consider g-lists and decomposed groups
                Select Case True
                    Case Session("bUseReg")
                        bGrp = False
                    Case Session("bUseGroup")
                        bGrp = True
                    Case Session("bUseGroupDec")
                        bGrp = False
                        SetDataTablefromAccess(dbG, "SELECT * FROM " & GroupIDNew & " i, " &
                        Grouping & " d WHERE i.GroupNum = d.GroupNo AND i.GroupName = """ &
                        lstDim1.Items(r).Text & """ ORDER BY Region", "", rsG)
                        iGL = 0
                    Case Session("bUseGList")
                        SetDataTablefromAccess(dbG, "SELECT * FROM GList d, GListId i WHERE i.Code = d.GListCode AND i.Description = """ & lstDim1.Items(r).Text & """", "", rsG)
                        iGL = 0
                End Select

                Do While True 'loop for G-Lists and Dec. Groups
                    If Session("bUseGList") Or Session("bUseGroupDec") Then
                        sDim1 = rsG.Rows(iGL).Item("Region")
                        iDim1 = IFsRegs.Item(sDim1).Index
                        If Session("bUseGList") Then
                            bGrp = rsG.Rows(iGL).Item("Group")
                        End If
                    End If
                    'loop through types
                    For T = 0 To lstDim2.Items.Count - 1
                        If lstDim2.Items(T).Selected Then
                            'loop through gender
                            For g = 0 To lstDim3.Items.Count - 1
                                If lstDim3.Items(g).Selected Then
                                    'loop through files
                                    For f = 0 To lstFiles.Items.Count - 1
                                        If lstFiles.Items(f).Selected Then
                                            If Session("TotalGraphs") < MaxVariables Then
                                                Session("TotalGraphs") = Session("TotalGraphs") + 1
                                                If Session("GraphType") = "ChldProb" Then
                                                    'LoadMortalityChld(Session("TotalGraphs"), sFileNames(f), dbMs(f), sDim1, iDim1, bGrp,
                                                    'lstDim2.Items(T).Text, lstDim3.Items(g).Text)

                                                    LoadMortalityChld(Session("TotalGraphs"), sFileNames(f), sDim1, iDim1, bGrp,
                                                    lstDim2.Items(T).Text, lstDim3.Items(g).Text)
                                                ElseIf Session("GraphType") = "ChldRate" Then
                                                    'LoadMortalityInf(Session("TotalGraphs"), sFileNames(f), dbMs(f), sDim1, iDim1, bGrp,
                                                    'lstDim2.Items(T).Text, lstDim3.Items(g).Text)

                                                    LoadMortalityInf(Session("TotalGraphs"), sFileNames(f), sDim1, iDim1, bGrp,
                                                    lstDim2.Items(T).Text, lstDim3.Items(g).Text)

                                                ElseIf Session("GraphType") = "AdltProb" Then
                                                    'LoadMortalityAdlt(Session("TotalGraphs"), sFileNames(f), dbMs(f), sDim1, iDim1, bGrp,
                                                    'lstDim2.Items(T).Text, lstDim3.Items(g).Text)

                                                    LoadMortalityAdlt(Session("TotalGraphs"), sFileNames(f), sDim1, iDim1, bGrp,
                                                    lstDim2.Items(T).Text, lstDim3.Items(g).Text)

                                                ElseIf Session("GraphType") = "AdltRate" Then
                                                    'LoadMortalityRateAdlt(Session("TotalGraphs"), sFileNames(f), dbMs(f), sDim1, iDim1, bGrp,
                                                    'lstDim2.Items(T).Text, lstDim3.Items(g).Text)

                                                    LoadMortalityRateAdlt(Session("TotalGraphs"), sFileNames(f), sDim1, iDim1, bGrp,
                                                    lstDim2.Items(T).Text, lstDim3.Items(g).Text)
                                                End If
                                            End If
                                        End If 'if file selected
                                    Next f
                                End If 'if gender selected
                            Next g
                        End If 'if type selected
                    Next T
                    If Session("bUseGList") Or Session("bUseGroupDec") Then
                        iGL = iGL + 1
                        If rsG.Rows.Count = iGL Then Exit Do
                    Else
                        Exit Do
                    End If
                Loop
            End If 'if region selected
        Next r

        'jrs 2010/03/04
        'Close used files
        'loop through files

        'JAM 2020-06-15
        ''For f = 0 To lstFiles.Items.Count - 1
        ''    If lstFiles.Items(f).Selected Then
        ''        dbMs(f).Close()
        ''    End If
        ''Next f

        'mnuPrint.Visible = True
        'mnuSaveHdr.Visible = True

    End Sub

    'jrs 2016/06/22
    Private Sub LoadSetLE()
        Dim f As Integer
        Dim r As Integer, T As Integer, g As Integer, y As Integer
        Dim bGrp As Boolean
        Dim dbG As String
        Dim rsG As DataTable
        Dim iGL As Integer
        Dim sDim1 As String
        Dim iDim1 As Integer

        dbG = Session("IFs_MDB2Active")

        Session("TotalGraphs") = 0

        'jrs 2010/03/04
        'Open all needed files
        Dim sFileNames() As String
        ReDim sFileNames(lstFiles.Items.Count)

        Dim dbMs() As OleDb.OleDbConnection
        ReDim dbMs(lstFiles.Items.Count)
        'loop through files
        For f = 0 To lstFiles.Items.Count - 1
            If lstFiles.Items(f).Selected Then
                Select Case f
                    Case 0

                        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                            sFileNames(f) = "ifsPopWork.mdb"

                        Else

                            sFileNames(f) = "ifsPopWork.db"

                        End If
                    Case 1
                        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                            sFileNames(f) = "ifsPop.mdb"
                        Else
                            sFileNames(f) = "ifsPop.db"
                        End If
                    Case Else
                        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                            sFileNames(f) = "ifsPop" & lstFiles.Items(f).Text & ".mdb"
                        Else
                            sFileNames(f) = "ifsPop" & lstFiles.Items(f).Text & ".db"
                        End If
                End Select
                ''dbMs(f) = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileNames(f))
                ''dbMs(f).Open()
            End If
        Next f

        'loop through regions
        For r = 0 To lstDim1.Items.Count - 1
            If lstDim1.Items(r).Selected Then
                sDim1 = lstDim1.Items(r).Text
                iDim1 = r + 1
                'for later: consider g-lists and decomposed groups
                Select Case True
                    Case Session("bUseReg")
                        bGrp = False
                    Case Session("bUseGroup")
                        bGrp = True
                    Case Session("bUseGroupDec")
                        bGrp = False
                        SetDataTablefromAccess(dbG, "SELECT * FROM " & GroupIDNew & " i, " &
                        Grouping & " d WHERE i.GroupNum = d.GroupNo AND i.GroupName = """ &
                        lstDim1.Items(r).Text & """ ORDER BY Region", "", rsG)
                        iGL = 0
                    Case Session("bUseGList")
                        SetDataTablefromAccess(dbG, "SELECT * FROM GList d, GListId i WHERE i.Code = d.GListCode AND i.Description = """ & lstDim1.Items(r).Text & """", "", rsG)
                        iGL = 0
                End Select

                Do While True 'loop for G-Lists and Dec. Groups
                    If Session("bUseGList") Or Session("bUseGroupDec") Then
                        sDim1 = rsG.Rows(iGL).Item("Region")
                        iDim1 = IFsRegs.Item(sDim1).Index
                        If Session("bUseGList") Then
                            bGrp = rsG.Rows(iGL).Item("Group")
                        End If
                    End If
                    'loop through types
                    For T = 0 To lstDim2.Items.Count - 1
                        If lstDim2.Items(T).Selected Then
                            'loop through gender
                            For g = 0 To lstDim3.Items.Count - 1
                                If lstDim3.Items(g).Selected Then
                                    'loop through years
                                    For y = 0 To lstYears.Items.Count - 1
                                        If lstYears.Items(y).Selected Then
                                            'loop through files
                                            For f = 0 To lstFiles.Items.Count - 1
                                                If lstFiles.Items(f).Selected Then
                                                    If Session("TotalGraphs") < MaxVariables Then
                                                        Session("TotalGraphs") = Session("TotalGraphs") + 1
                                                        'LoadLifeExpectancyAtAllAges(Session("TotalGraphs"), sFileNames(f), dbMs(f), sDim1, iDim1, bGrp,
                                                        'lstDim2.Items(T).Text, lstYears.Items(y).Text)
                                                        'cdv 2021/01/29 Fixed LE not loading correctly (lstDim2.Items(T).Text -> listDim3.items(g).text)
                                                        LoadLifeExpectancyAtAllAges(Session("TotalGraphs"), sFileNames(f), sDim1, iDim1, bGrp,
                                                        lstDim3.Items(g).Text, lstYears.Items(y).Text)

                                                        'jrs 2009/07/02
                                                        If Not Session("bSuccess") Then
                                                            Session("TotalGraphs") = Session("TotalGraphs") - 1
                                                        End If
                                                    End If
                                                End If 'if file selected
                                            Next f
                                        End If 'if Year selected
                                    Next y
                                End If 'if gender selected
                            Next g
                        End If 'if type selected
                    Next T
                    If Session("bUseGList") Or Session("bUseGroupDec") Then
                        iGL = iGL + 1
                        If rsG.Rows.Count = iGL Then Exit Do
                    Else
                        Exit Do
                    End If
                Loop
            End If 'if region selected
        Next r

        'jrs 2010/03/04
        'Close used files
        'loop through files
        'JAM 2020-06-27 Commented
        ''For f = 0 To lstFiles.Items.Count - 1
        ''    If lstFiles.Items(f).Selected Then
        ''        dbMs(f).Close()
        ''    End If
        ''Next f

        'dbG.Close()

        'jrs 2008/11/11
        'mnuPrint.Visible = True
        'mnuSaveHdr.Visible = True

    End Sub

    'JAM 2020-06-27
    'Private Sub LoadMortality(ByVal TotalGraphs As Integer, ByVal sFileName As String, ByRef dbM As OleDb.OleDbConnection,
    'ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    'ByVal sGender As String, ByVal iYr As Integer, ByVal bReport As Boolean)

    Private Sub LoadMortality(ByVal TotalGraphs As Integer, ByVal sFileName As String,
    ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    ByVal sGender As String, ByVal iYr As Integer, ByVal bReport As Boolean)
        'jrs 2009/07/10 added bReport
        'parameter bReport is used to generate comparison report when true


        Dim Tindex As Integer
        Dim rId As Integer
        Dim TCohort As Integer
        Dim sMaleMortFld As String, sFemaleMortFld As String

        Dim TempDatabase(0, 0) As Single
        Dim TempPopDatabase(0, 0) As Single
        'Weighted Average for Population
        Dim POPMales(0, 0) As Single
        Dim POPFemales(0, 0) As Single

        'jrs 2009/07/02
        Session("bSuccess") = False

        'jrs 2024/05/28
        'jrs 2006/07/14 create connecion
        'Dim IfsConn As OleDb.OleDbConnection
        'IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        Dim IfsConn As SQLiteConnection = New SQLiteConnection
        IfsConn.ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"

        Dim rdr As OleDb.OleDbDataReader
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'myCMD.Connection = dbM
        Dim dbM As OleDb.OleDbConnection
        'JAM 2020-06-27
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileName)
            dbM.Open()
            myCMD.Connection = dbM
        Else 'jrs 2024/05/28
            vSQLiteCon.Close()
            vConnString = "DataSource=" + Session("PutFdrvRun") & sFileName & ";"
            vSQLiteCon = New SQLiteConnection(vConnString)
            vSQLiteCon.Open()
        End If


        'JAM 2020-06-27
        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable
        Dim regDS As DataSet

        DTsqlite = New DataTable
        DTsqlite2 = New DataTable
        regDS = New DataSet '

        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow

        Dim vchosenColumns As String

        Select Case sType
            Case "OthCommumDis"
                sMaleMortFld = "ComDisNoAIDSMales"
                sFemaleMortFld = "ComDisNoAIDSFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,ComDisNoAIDSMales,ComDisNoAIDSFemales"

            Case "MaligNeoPl"
                sMaleMortFld = "MaligNeoPlasMales"
                sFemaleMortFld = "MaligNeoPlasFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,MaligNeoPlasMales,MaligNeoPlasFemales"
            Case "CardioVasc"
                sMaleMortFld = "CardioVascMales"
                sFemaleMortFld = "CardioVascFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,CardioVascMales,CardioVascFemales"

            Case "Digestive"
                sMaleMortFld = "DigestiveMales"
                sFemaleMortFld = "DigestiveFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DigestiveMales,DigestiveFemales"

            Case "Respiratory"
                sMaleMortFld = "RespiratoryMales"
                sFemaleMortFld = "RespiratoryFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,RespiratoryMales,RespiratoryFemales"

            Case "OtherNonComm"
                sMaleMortFld = "OtherG2Males"
                sFemaleMortFld = "OtherG2Females"

                vchosenColumns = "TimeId,RegionId,CohortId,OtherG2Males,OtherG2Females"

            Case "TrafficAcc"
                sMaleMortFld = "RoadTrafficAccMales"
                sFemaleMortFld = "RoadTrafficAccFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,RoadTrafficAccMales,RoadTrafficAccFemales"
            Case "UnIntInj"
                sMaleMortFld = "OtherunintInjMales"
                sFemaleMortFld = "OtherunintInjFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,OtherunintInjMales,OtherunintInjFemales"
            Case "IntInj"
                sMaleMortFld = "IntenInjMales"
                sFemaleMortFld = "IntenInjFemales"


                vchosenColumns = "TimeId,RegionId,CohortId,IntenInjMales,IntenInjFemales"
            Case "Diabetes"
                sMaleMortFld = "DiabetesMales"
                sFemaleMortFld = "DiabetesFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DiabetesMales,DiabetesFemales"
            Case "AIDS"
                sMaleMortFld = "AIDSMales"
                sFemaleMortFld = "AIDSFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,AIDSMales,AIDSFemales"
            Case "Diarrhea" 'jrs 2009/08/14
                sMaleMortFld = "DiarrheaMales"
                sFemaleMortFld = "DiarrheaFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DiarrheaMales,DiarrheaFemales"
            Case "Malaria"
                sMaleMortFld = "MalariaMales"
                sFemaleMortFld = "MalariaFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,MalariaMales,MalariaFemales"

            Case "RespInfec"
                sMaleMortFld = "RespInfMales"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "RespInfFemales"

                    vchosenColumns = "TimeId,RegionId,CohortId,RespInfMales,RespInfFemales"

            Case "MentalHealth"
                sMaleMortFld = "MentalHealthMales"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MentalHealthFemales"

                    vchosenColumns = "TimeId,RegionId,CohortId,MentalHealthMales,MentalHealthFemales"

            Case "TotComDis"
                sMaleMortFld = "MortPerMalesComDis"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesComDis"

                    vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesComDis,MortPerFemalesComDis"

            Case "TotNonComDis"
                sMaleMortFld = "MortPerMalesNonComDis"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesNonComDis"

                    vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesNonComDis,MortPerFemalesNonComDis"

            Case "TotInjuries"
                sMaleMortFld = "MortPerMalesInjuries"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesInjuries"

                vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesInjuries,MortPerFemalesInjuries"

            Case "Total"
                'need to read all 6 fields

                vchosenColumns = "*"
        End Select

        ReDim TempPopDatabase(NCohorts, 2)
        ReDim TempDatabase(NCohorts, 2)

        'take population
        '   Change the data source
        '   ----------------------
        Tindex = 2
        If Not bGrp Then

            'JAM 2020-06-27
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM PopBaseCase Where Region = " &
                Str(iReg) & " and Years = " & Str(iYr))

                'jrs 2009/07/02
                'If tbl2.EOF Then
                '    MsgBox("There's no data in scenario " & _
                '    Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "") & _
                '    " for year " & CStr(iYr), vbCritical, "Load Mortality")
                '    Screen.MousePointer = vbNormal
                '    Exit Sub
                'End If

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    TempPopDatabase(rdr.Item("NCohorts"), 1) = rdr.Item("PopulationMales")
                    TempPopDatabase(rdr.Item("NCohorts"), 2) = rdr.Item("PopulationFemales")
                Loop
                rdr.Close()

            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where RegionId = " &
                                            Str(iReg) & " and TimeID = " & Str(iYr)
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())


                    TempPopDatabase(vSQLiteReader("CohortId"), 1) = vSQLiteReader("PopulationMales")
                    TempPopDatabase(vSQLiteReader("CohortId"), 2) = vSQLiteReader("PopulationFemales")

                End While

            End If
        Else
            ReDim POPMales(NR, NCohorts)
            ReDim POPFemales(NR, NCohorts)

            'jrs 2024/05/28
            IfsConn.Open()
            'Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            'Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            'Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            Dim grpDA As OleDb.OleDbDataAdapter
            Dim regDA As OleDb.OleDbDataAdapter
            Dim regIDDA As OleDb.OleDbDataAdapter

            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase Where Years = " & CStr(iYr) &
            '" ORDER BY Region, NCohorts", dbM)


            Dim popDA As OleDb.OleDbDataAdapter
            regDS = New DataSet
            'jrs 2024/05/28
            regDS.EnforceConstraints = False
            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
            '" ORDER BY Region, NCohorts", dbM)

            'jrs 2024/05/28
            Dim rdGrpCmd As SQLiteCommand = IfsConn.CreateCommand
            rdGrpCmd.CommandText = "SELECT * FROM " & GroupIDNew
            Dim rdGrp As SQLiteDataReader
            rdGrp = rdGrpCmd.ExecuteReader

            Dim myTbl As DataTable = New DataTable
            myTbl.TableName = "Groups"
            regDS.Tables.Add(myTbl)
            regDS.Tables(0).Load(rdGrp)

            Dim rdRegCmd As SQLiteCommand = IfsConn.CreateCommand
            rdRegCmd.CommandText = "SELECT * FROM " & Grouping
            Dim rdReg As SQLiteDataReader
            rdReg = rdRegCmd.ExecuteReader

            Dim myTbl1 As DataTable = New DataTable
            myTbl1.TableName = "Regions"
            regDS.Tables.Add(myTbl1)
            regDS.Tables(1).Load(rdReg)

            Dim rdRegIdCmd As SQLiteCommand = IfsConn.CreateCommand
            rdRegIdCmd.CommandText = "SELECT * FROM " & RegionIDNew
            Dim rdRegId As SQLiteDataReader
            rdRegId = rdRegIdCmd.ExecuteReader

            Dim myTbl2 As DataTable = New DataTable
            myTbl2.TableName = "RegionIDs"
            regDS.Tables.Add(myTbl2)
            regDS.Tables(2).Load(rdRegId)

            'JAM 2020-06-27
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                'jrs 2024/05/28
                IfsConn.Open()

                'grpDA = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
                'regDA = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
                'regIDDA = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)

                popDA = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase Where Years = " & CStr(iYr) &
                                                    " ORDER BY Region, NCohorts", dbM)
            Else

                '
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where TimeId = " & CStr(iYr) &
                                                    " ORDER BY RegionId, CohortId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite.Load(vSQLiteReader)


                'jrs 2024/05/28
                'dtnew = DTsqlite.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite)


            End If




            'regDS = New DataSet
            'jrs 2024/05/28
            'grpDA.Fill(regDS, "Groups")
            'regDA.Fill(regDS, "Regions")
            'regIDDA.Fill(regDS, "RegionIDs")
            'popDA.Fill(regDS, "Population")

            'JAM 2020-06-27
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                grpDA.Fill(regDS, "Groups")
                regDA.Fill(regDS, "Regions")
                regIDDA.Fill(regDS, "RegionIDs")

                popDA.Fill(regDS, "Population")
            End If



            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                 regDS.Tables("Groups").Columns("GroupNum"),
                                                 regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Population").Columns("Region"))


            Dim regPopRel As DataRelation

            'JAM 2020-06-26
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regPopRel = regDS.Relations.Add("RegPopulation",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Population").Columns("Region"))
            Else


                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("pop_base_case").Columns(1))


            End If


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("NCohorts"), 1) =
                                TempPopDatabase(cRow.Item("NCohorts"), 1) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("NCohorts"), 2) =
                                TempPopDatabase(cRow.Item("NCohorts"), 2) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("Region"), cRow.Item("NCohorts")) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("Region"), cRow.Item("NCohorts")) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next
            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("CohortId"), 1) =
                                TempPopDatabase(cRow.Item("CohortId"), 1) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("CohortId"), 2) =
                                TempPopDatabase(cRow.Item("CohortId"), 2) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("RegionId"), cRow.Item("CohortId")) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId")) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next
            End If
            'jrs 2009/07/02
            'If tbl2.EOF Then
            '    MsgBox("There's no data in scenario " & _
            '    Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "") & _
            '    " for year " & CStr(iYr), vbCritical, "Load Mortality")
            '    Screen.MousePointer = vbNormal
            '    Exit Sub
            'End If
        End If
        'End take population


        '   Change the data source
        '   ----------------------
        If Not bGrp Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM MorDetBaseCase Where Region = " & Str(iReg) &
            " and Years = " & CStr(iYr))

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    If Not (sType = "Total") Then
                        'males 
                        TCohort = rdr.Item("NCohorts")
                        TempDatabase(TCohort, 1) = AMAX(0, rdr.Item(sMaleMortFld))

                        'females 
                        TempDatabase(TCohort, 2) = AMAX(0, rdr.Item(sFemaleMortFld))
                    Else
                        'males 
                        TCohort = rdr.Item("NCohorts")
                        TempDatabase(TCohort, 1) = AMAX(0, rdr.Item("MortPerMalesComDis") +
                    rdr.Item("MortPerMalesNonComDis") + rdr.Item("MortPerMalesInjuries"))

                        'females 
                        TempDatabase(TCohort, 2) = AMAX(0, rdr.Item("MortPerFeMalesComDis") +
                    rdr.Item("MortPerFeMalesNonComDis") + rdr.Item("MortPerFeMalesInjuries"))
                    End If
                Loop
                rdr.Close()

            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT " & vchosenColumns & " FROM Mor_Det_Base_Case Where RegionId = " & Str(iReg) &
                                                " and TimeId = " & CStr(iYr)
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())

                    If Not (sType = "Total") Then
                        'males 
                        TCohort = vSQLiteReader("CohortId")
                        TempDatabase(TCohort, 1) = AMAX(0, vSQLiteReader(sMaleMortFld))

                        'females 
                        TempDatabase(TCohort, 2) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                    Else
                        'males 
                        TCohort = vSQLiteReader("CohortId")
                        TempDatabase(TCohort, 1) = AMAX(0, vSQLiteReader("MortPerMalesComDis") +
                        vSQLiteReader("MortPerMalesNonComDis") + vSQLiteReader("MortPerMalesInjuries"))

                        'females 
                        TempDatabase(TCohort, 2) = AMAX(0, vSQLiteReader("MortPerFeMalesComDis") +
                        vSQLiteReader("MortPerFeMalesNonComDis") + vSQLiteReader("MortPerFeMalesInjuries"))




                    End If

                End While

            End If
        Else

            'Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase Where Years = " &
            'CStr(iYr) & " ORDER BY Region, NCohorts", dbM)


            Dim mortDA As OleDb.OleDbDataAdapter

            'JAM 2020-06-27
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                mortDA = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase Where Years = " &
                                                    CStr(iYr) & " ORDER BY Region, NCohorts", dbM)
                mortDA.Fill(regDS, "Mortality")
            Else
                'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT " & vchosenColumns & " FROM Mor_Det_Base_Case Where TimeId = " &
                                            CStr(iYr) & " ORDER BY RegionId, CohortId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite2.Load(vSQLiteReader)

                'jrs 2024/05/28
                'dtnew = DTsqlite2.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite2.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite2)
            End If


            'mortDA.Fill(regDS, "Mortality")




            'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Mortality").Columns("Region"))


            Dim regMortRel As DataRelation

            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regMortRel = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Mortality").Columns("Region"))
            Else


                regMortRel = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("Mor_Det_Base_Case").Columns("RegionId"))


            End If

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("NCohorts")
                                    rId = cRow.Item("Region")

                                    If Not (sType = "Total") Then
                                        TempDatabase(TCohort, 1) =
                                    TempDatabase(TCohort, 1) +
                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort)

                                        TempDatabase(TCohort, 2) =
                                    TempDatabase(TCohort, 2) +
                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort)
                                    Else
                                        TempDatabase(TCohort, 1) =
                                    TempDatabase(TCohort, 1) +
                                    (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                    cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort)

                                        TempDatabase(TCohort, 2) =
                                    TempDatabase(TCohort, 2) +
                                    (cRow.Item("MortPerFemalesComDis") + cRow.Item("MortPerFemalesNonComDis") +
                                    cRow.Item("MortPerFemalesInjuries")) * POPFemales(rId, TCohort)
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next

            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("CohortId")
                                    rId = cRow.Item("RegionId")

                                    If Not (sType = "Total") Then
                                        TempDatabase(TCohort, 1) =
                                    TempDatabase(TCohort, 1) +
                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort)

                                        TempDatabase(TCohort, 2) =
                                    TempDatabase(TCohort, 2) +
                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort)
                                    Else
                                        TempDatabase(TCohort, 1) =
                                    TempDatabase(TCohort, 1) +
                                    (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                    cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort)

                                        TempDatabase(TCohort, 2) =
                                    TempDatabase(TCohort, 2) +
                                    (cRow.Item("MortPerFeMalesComDis") + cRow.Item("MortPerFeMalesNonComDis") +
                                    cRow.Item("MortPerFeMalesInjuries")) * POPFemales(rId, TCohort)
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next

            End If
            IfsConn.Close()

            'jrs 2007/03/05
            'Population Weigthed average
            For TCohort = 0 To NCohorts
                TempDatabase(TCohort, 1) = TempDatabase(TCohort, 1) / TempPopDatabase(TCohort, 1)

                TempDatabase(TCohort, 2) = TempDatabase(TCohort, 2) / TempPopDatabase(TCohort, 2)
            Next TCohort
        End If

        'jrs 2009/04/20
        'Using GBD cohorts
        If Session("bUseGBDage") Then 'jrs 2009/07/10
            AggGBDage(CInt(NCohorts), 3, TempDatabase, TempPopDatabase)
        End If


        'JAM 2020-06-26
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM.Close()
        End If

        'jrs 2009/04/22
        If Session("bUseDeaths") Then 'Or bReport Then 'jrs 2009/07/10
            ConvertToDeaths(CInt(NCohorts), 3, TempDatabase, TempPopDatabase)
        End If

        Dim iNCohorts As Integer
        'Using GBD cohorts
        If Session("bUseGBDage") Then
            iNCohorts = NCohorts - 14
        Else
            iNCohorts = NCohorts
        End If


        'Load Final Array
        For TCohort = 0 To iNCohorts
            If sGender = "Male" Then
                Session("mPLOT")(TCohort, TotalGraphs) = TempDatabase(TCohort, 1)
                'jrs 2009/07/10
                If bReport And sType = "OthCommumDis" Then
                    Session("mPLOT")(TCohort, TotalGraphs + 1) = TempPopDatabase(TCohort, 1)
                    'jrs 2009/07/17
                    If TCohort = 0 Then
                        'don't count newborns during the year
                        Session("mPLOT")(TCohort, TotalGraphs + 1) = Session("mPLOT")(TCohort, TotalGraphs + 1) - TempPopDatabase(8, 1)
                    End If
                End If
            ElseIf sGender = "Female" Then
                Session("mPLOT")(TCohort, TotalGraphs) = TempDatabase(TCohort, 2)
                'jrs 2009/07/10
                If bReport And sType = "OthCommumDis" Then
                    Session("mPLOT")(TCohort, TotalGraphs + 1) = TempPopDatabase(TCohort, 2)
                    'jrs 2009/07/17
                    If TCohort = 0 Then
                        'don't count newborns during the year
                        Session("mPLOT")(TCohort, TotalGraphs + 1) = Session("mPLOT")(TCohort, TotalGraphs + 1) - TempPopDatabase(8, 2)
                    End If
                End If
            Else
                'jrs 2009/07/10
                If Not Session("bUseDeaths") Then
                    Session("mPLOT")(TCohort, TotalGraphs) = (TempDatabase(TCohort, 1) * TempPopDatabase(TCohort, 1) +
                    TempDatabase(TCohort, 2) * TempPopDatabase(TCohort, 2)) /
                    (TempPopDatabase(TCohort, 1) + TempPopDatabase(TCohort, 2))
                Else
                    Session("mPLOT")(TCohort, TotalGraphs) = TempDatabase(TCohort, 1) + TempDatabase(TCohort, 2)
                End If
            End If
        Next TCohort

        'jrs 2010/02/09
        Session("mLegends")(0, TotalGraphs) = sReg
        'If bGrp And mnulblAlias.Checked Then
        '    'Read and load all aliases
        '    Dim db As Database
        '    Dim Tbl As Recordset
        '    db = OpenDatabase(IFs_MDB2)
        '    Tbl = db.OpenRecordset(GroupIDNew)
        '    Tbl.Index = "GroupName"
        '    Tbl.Seek("=", sReg)
        '    If Not Tbl.NoMatch Then
        '        mLegends(1, TotalGraphs) = Tbl.Fields("Alias")
        '    End If
        '    Tbl.Close()
        '    db.Close()
        'Else
        Session("mLegends")(1, TotalGraphs) = sReg
        'End If


        If sType <> "Total" Then
            Session("mLegends")(2, TotalGraphs) = sType
        Else
            Session("mLegends")(2, TotalGraphs) = "All Types"
        End If
        If sGender <> "Total" Then
            Session("mLegends")(3, TotalGraphs) = sGender
        Else
            Session("mLegends")(3, TotalGraphs) = "All Genders"
        End If
        Session("mLegends")(4, TotalGraphs) = CStr(iYr)
        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then
            Session("mLegends")(5, TotalGraphs) = "Working"
        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            Session("mLegends")(5, TotalGraphs) = "Base"
        Else
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "")

            Else
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".db", "")
            End If

        End If

        'jrs 2009/07/02
        Session("bSuccess") = True

    End Sub

    'Children Mortality Probability, computed using Life Tables (survivor tables) lx

    'Private Sub LoadMortalityChld(ByVal TotalGraphs As Integer, ByVal sFileName As String, ByRef dbM As OleDb.OleDbConnection,
    'ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    'ByVal sGender As String)


    Private Sub LoadMortalityChld(ByVal TotalGraphs As Integer, ByVal sFileName As String,
    ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    ByVal sGender As String)
        'jrs 2009/07/10 added bReport
        'parameter bReport is used to generate comparison report when true


        Dim Tindex As Integer
        Dim rId As Integer
        Dim TCohort As Integer
        Dim sMaleMortFld As String, sFemaleMortFld As String
        Dim iYr As Integer, g As Integer, j As Integer

        Dim TempDatabase(0, 0, 0) As Single
        Dim TempPopDatabase(0, 0, 0) As Single
        'Weighted Average for Population
        Dim POPMales(0, 0, 0) As Single
        Dim POPFemales(0, 0, 0) As Single

        'jrs 2009/07/02
        Session("bSuccess") = False

        'jrs 2024/05/28
        'jrs 2006/07/14 create connecion
        'Dim IfsConn As OleDb.OleDbConnection
        'IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        Dim IfsConn As SQLiteConnection = New SQLiteConnection
        IfsConn.ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"
        Dim rdr As OleDb.OleDbDataReader
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'myCMD.Connection = dbM
        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow
        Dim dbM As OleDb.OleDbConnection

        'JAM 2020-06-28
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileName)
            dbM.Open()
            myCMD.Connection = dbM
        Else 'jrs 2024/05/28
            vSQLiteCon.Close()
            vConnString = "DataSource=" + Session("PutFdrvRun") & sFileName & ";"
            vSQLiteCon = New SQLiteConnection(vConnString)
            vSQLiteCon.Open()

        End If

        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable

        DTsqlite = New DataTable
        DTsqlite2 = New DataTable
        regDS = New DataSet



        'Select Case sType
        '    Case "OthCommumDis"
        '        sMaleMortFld = "ComDisNoAIDSMales"
        '        sFemaleMortFld = "ComDisNoAIDSFemales"
        '    Case "MaligNeoPl"
        '        sMaleMortFld = "MaligNeoPlasMales"
        '        sFemaleMortFld = "MaligNeoPlasFemales"
        '    Case "CardioVasc"
        '        sMaleMortFld = "CardioVascMales"
        '        sFemaleMortFld = "CardioVascFemales"
        '    Case "Digestive"
        '        sMaleMortFld = "DigestiveMales"
        '        sFemaleMortFld = "DigestiveFemales"
        '    Case "Respiratory"
        '        sMaleMortFld = "RespiratoryMales"
        '        sFemaleMortFld = "RespiratoryFemales"
        '    Case "OtherNonComm"
        '        sMaleMortFld = "OtherG2Males"
        '        sFemaleMortFld = "OtherG2Females"
        '    Case "TrafficAcc"
        '        sMaleMortFld = "RoadTrafficAccMales"
        '        sFemaleMortFld = "RoadTrafficAccFemales"
        '    Case "UnIntInj"
        '        sMaleMortFld = "OtherunintInjMales"
        '        sFemaleMortFld = "OtherunintInjFemales"
        '    Case "IntInj"
        '        sMaleMortFld = "IntenInjMales"
        '        sFemaleMortFld = "IntenInjFemales"
        '    Case "Diabetes"
        '        sMaleMortFld = "DiabetesMales"
        '        sFemaleMortFld = "DiabetesFemales"
        '    Case "AIDS"
        '        sMaleMortFld = "AIDSMales"
        '        sFemaleMortFld = "AIDSFemales"
        '    Case "Diarrhea" 'jrs 2009/08/14
        '        sMaleMortFld = "DiarrheaMales"
        '        sFemaleMortFld = "DiarrheaFemales"
        '    Case "Malaria"
        '        sMaleMortFld = "MalariaMales"
        '        sFemaleMortFld = "MalariaFemales"
        '    Case "RespInfec"
        '        sMaleMortFld = "RespInfMales"
        '        sFemaleMortFld = "RespInfFeMales"
        '    Case "MentalHealth"
        '        sMaleMortFld = "MentalHealthMales"
        '        sFemaleMortFld = "MentalHealthFeMales"
        '    Case "TotComDis"
        '        sMaleMortFld = "MortPerMalesComDis"
        '        sFemaleMortFld = "MortPerFeMalesComDis"
        '    Case "TotNonComDis"
        '        sMaleMortFld = "MortPerMalesNonComDis"
        '        sFemaleMortFld = "MortPerFeMalesNonComDis"
        '    Case "TotInjuries"
        '        sMaleMortFld = "MortPerMalesInjuries"
        '        sFemaleMortFld = "MortPerFeMalesInjuries"
        '    Case "Total"
        '        'need to read all 6 fields
        'End Select

        Dim vchosenColumns As String

        Select Case sType
            Case "OthCommumDis"
                sMaleMortFld = "ComDisNoAIDSMales"
                sFemaleMortFld = "ComDisNoAIDSFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,ComDisNoAIDSMales,ComDisNoAIDSFemales"

            Case "MaligNeoPl"
                sMaleMortFld = "MaligNeoPlasMales"
                sFemaleMortFld = "MaligNeoPlasFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,MaligNeoPlasMales,MaligNeoPlasFemales"
            Case "CardioVasc"
                sMaleMortFld = "CardioVascMales"
                sFemaleMortFld = "CardioVascFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,CardioVascMales,CardioVascFemales"

            Case "Digestive"
                sMaleMortFld = "DigestiveMales"
                sFemaleMortFld = "DigestiveFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DigestiveMales,DigestiveFemales"

            Case "Respiratory"
                sMaleMortFld = "RespiratoryMales"
                sFemaleMortFld = "RespiratoryFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,RespiratoryMales,RespiratoryFemales"

            Case "OtherNonComm"
                sMaleMortFld = "OtherG2Males"
                sFemaleMortFld = "OtherG2Females"

                vchosenColumns = "TimeId,RegionId,CohortId,OtherG2Males,OtherG2Females"

            Case "TrafficAcc"
                sMaleMortFld = "RoadTrafficAccMales"
                sFemaleMortFld = "RoadTrafficAccFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,RoadTrafficAccMales,RoadTrafficAccFemales"
            Case "UnIntInj"
                sMaleMortFld = "OtherunintInjMales"
                sFemaleMortFld = "OtherunintInjFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,OtherunintInjMales,OtherunintInjFemales"
            Case "IntInj"
                sMaleMortFld = "IntenInjMales"
                sFemaleMortFld = "IntenInjFemales"


                vchosenColumns = "TimeId,RegionId,CohortId,IntenInjMales,IntenInjFemales"
            Case "Diabetes"
                sMaleMortFld = "DiabetesMales"
                sFemaleMortFld = "DiabetesFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DiabetesMales,DiabetesFemales"
            Case "AIDS"
                sMaleMortFld = "AIDSMales"
                sFemaleMortFld = "AIDSFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,AIDSMales,AIDSFemales"
            Case "Diarrhea" 'jrs 2009/08/14
                sMaleMortFld = "DiarrheaMales"
                sFemaleMortFld = "DiarrheaFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DiarrheaMales,DiarrheaFemales"
            Case "Malaria"
                sMaleMortFld = "MalariaMales"
                sFemaleMortFld = "MalariaFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,MalariaMales,MalariaFemales"

            Case "RespInfec"
                sMaleMortFld = "RespInfMales"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "RespInfFemales"

                    vchosenColumns = "TimeId,RegionId,CohortId,RespInfMales,RespInfFemales"

            Case "MentalHealth"
                sMaleMortFld = "MentalHealthMales"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MentalHealthFemales"

                    vchosenColumns = "TimeId,RegionId,CohortId,MentalHealthMales,MentalHealthFemales"

            Case "TotComDis"
                sMaleMortFld = "MortPerMalesComDis"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesComDis"

                    vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesComDis,MortPerFemalesComDis"

            Case "TotNonComDis"
                sMaleMortFld = "MortPerMalesNonComDis"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesNonComDis"

                    vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesNonComDis,MortPerFemalesNonComDis"

            Case "TotInjuries"
                sMaleMortFld = "MortPerMalesInjuries"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesInjuries"

                vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesInjuries,MortPerFemalesInjuries"

            Case "Total"
                'need to read all 6 fields

                vchosenColumns = "*"
        End Select



        ReDim TempPopDatabase(1, 2, Session("LastYear") - Session("FirstYear") + 1)
        ReDim TempDatabase(2, 2, Session("LastYear") - Session("FirstYear") + 1)

        'take population
        '   Change the data source
        '   ----------------------
        Tindex = 2
        If Not bGrp Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM PopBaseCase Where Region = " &
            Str(iReg) & " and NCohorts < 2 and Years <= " & Session("LastYear"))

                'jrs 2009/07/02
                'If tbl2.EOF Then
                '    MsgBox("There's no data in scenario " & _
                '    Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "") & _
                '    " for year " & CStr(iYr), vbCritical, "Load Mortality")
                '    Screen.MousePointer = vbNormal
                '    Exit Sub
                'End If

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    TempPopDatabase(rdr.Item("NCohorts"), 1, rdr.Item("Years") - Session("FirstYear") + 1) = rdr.Item("PopulationMales")
                    TempPopDatabase(rdr.Item("NCohorts"), 2, rdr.Item("Years") - Session("FirstYear") + 1) = rdr.Item("PopulationFemales")
                Loop
                rdr.Close()
            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where RegionId = " &
                                            Str(iReg) & " and CohortId < 2 and TimeId <= " & Session("LastYear")
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())

                    TempPopDatabase(vSQLiteReader("CohortId"), 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = vSQLiteReader("PopulationMales")
                    TempPopDatabase(vSQLiteReader("CohortId"), 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = vSQLiteReader("PopulationFemales")


                End While
            End If
        Else
            ReDim POPMales(NR, NCohorts, Session("LastYear") - Session("FirstYear") + 1)
            ReDim POPFemales(NR, NCohorts, Session("LastYear") - Session("FirstYear") + 1)

            'jrs 2024/05/28
            IfsConn.Open()
            'Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            'Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            'Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            Dim grpDA As OleDb.OleDbDataAdapter
            Dim regDA As OleDb.OleDbDataAdapter
            Dim regIDDA As OleDb.OleDbDataAdapter

            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase Where NCohorts < 2 and Years <= " & CStr(Session("LastYear")) & " ORDER BY Region, NCohorts, Years", dbM)
            'regDS = New DataSet
            'grpDA.Fill(regDS, "Groups")
            'regDA.Fill(regDS, "Regions")
            'regIDDA.Fill(regDS, "RegionIDs")


            Dim popDA As OleDb.OleDbDataAdapter
            regDS = New DataSet
            'jrs 2024/05/28
            regDS.EnforceConstraints = False

            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
            '" ORDER BY Region, NCohorts", dbM)

            'jrs 2024/05/28
            Dim rdGrpCmd As SQLiteCommand = IfsConn.CreateCommand
            rdGrpCmd.CommandText = "SELECT * FROM " & GroupIDNew
            Dim rdGrp As SQLiteDataReader
            rdGrp = rdGrpCmd.ExecuteReader

            Dim myTbl As DataTable = New DataTable
            myTbl.TableName = "Groups"
            regDS.Tables.Add(myTbl)
            regDS.Tables(0).Load(rdGrp)

            Dim rdRegCmd As SQLiteCommand = IfsConn.CreateCommand
            rdRegCmd.CommandText = "SELECT * FROM " & Grouping
            Dim rdReg As SQLiteDataReader
            rdReg = rdRegCmd.ExecuteReader

            Dim myTbl1 As DataTable = New DataTable
            myTbl1.TableName = "Regions"
            regDS.Tables.Add(myTbl1)
            regDS.Tables(1).Load(rdReg)

            Dim rdRegIdCmd As SQLiteCommand = IfsConn.CreateCommand
            rdRegIdCmd.CommandText = "SELECT * FROM " & RegionIDNew
            Dim rdRegId As SQLiteDataReader
            rdRegId = rdRegIdCmd.ExecuteReader

            Dim myTbl2 As DataTable = New DataTable
            myTbl2.TableName = "RegionIDs"
            regDS.Tables.Add(myTbl2)
            regDS.Tables(2).Load(rdRegId)


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                popDA = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase Where NCohorts < 2 and Years <= " & CStr(Session("LastYear")) & " ORDER BY Region, NCohorts, Years", dbM)
                popDA.Fill(regDS, "Population")
            Else


                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where CohortId < 2 and TimeId <= " & CStr(Session("LastYear")) & " ORDER BY RegionId, CohortId, TimeId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite.Load(vSQLiteReader)
                'regDS.Tables.Add(DTsqlite)

                'jrs 2024/05/28
                'dtnew = DTsqlite.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite)

            End If








            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                 regDS.Tables("Groups").Columns("GroupNum"),
                                                 regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Population").Columns("Region"))

            Dim regPopRel As DataRelation

            'JAM 2020-06-28
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Population").Columns("Region"))
            Else


                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("pop_base_case").Columns(1))


            End If



            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("NCohorts"), 1, cRow.Item("Years") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("NCohorts"), 1, cRow.Item("Years") - Session("FirstYear") + 1) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("NCohorts"), 2, cRow.Item("Years") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("NCohorts"), 2, cRow.Item("Years") - Session("FirstYear") + 1) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("Region"), cRow.Item("NCohorts"), cRow.Item("Years") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("Region"), cRow.Item("NCohorts"), cRow.Item("Years") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next
            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("CohortId"), 1, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("CohortId"), 1, cRow.Item("TimeId") - Session("FirstYear") + 1) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("CohortId"), 2, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("CohortId"), 2, cRow.Item("TimeId") - Session("FirstYear") + 1) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("RegionId"), cRow.Item("CohortId"), cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId"), cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next
            End If

            'jrs 2009/07/02
            'If tbl2.EOF Then
            '    MsgBox("There's no data in scenario " & _
            '    Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "") & _
            '    " for year " & CStr(iYr), vbCritical, "Load Mortality")
            '    Screen.MousePointer = vbNormal
            '    Exit Sub
            'End If
        End If
        'End take population


        '   Change the data source
        '   ----------------------
        If Not bGrp Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM MorDetBaseCase Where Region = " & Str(iReg) &
            " and NCohorts < 2 and Years <= " & CStr(Session("LastYear")))

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    If Not (sType = "Total") Then
                        'males 
                        TCohort = rdr.Item("NCohorts")
                        TempDatabase(TCohort, 1, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item(sMaleMortFld))

                        'females 
                        TempDatabase(TCohort, 2, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item(sFemaleMortFld))
                    Else
                        'males 
                        TCohort = rdr.Item("NCohorts")
                        TempDatabase(TCohort, 1, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item("MortPerMalesComDis") +
                    rdr.Item("MortPerMalesNonComDis") + rdr.Item("MortPerMalesInjuries"))

                        'females 
                        TempDatabase(TCohort, 2, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item("MortPerFeMalesComDis") +
                    rdr.Item("MortPerFemalesNonComDis") + rdr.Item("MortPerFemalesInjuries"))
                    End If
                Loop
                rdr.Close()
            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where RegionId = " & Str(iReg) &
                                                " and CohortId < 2 and TimeId <= " & CStr(Session("LastYear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())
                    If Not (sType = "Total") Then
                        'males 
                        TCohort = vSQLiteReader("CohortId")
                        TempDatabase(TCohort, 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader(sMaleMortFld))

                        'females 
                        TempDatabase(TCohort, 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                    Else
                        'males 
                        TCohort = vSQLiteReader("CohortId")
                        TempDatabase(TCohort, 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader("MortPerMalesComDis") +
                    vSQLiteReader("MortPerMalesNonComDis") + vSQLiteReader("MortPerMalesInjuries"))

                        'females 
                        TempDatabase(TCohort, 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader("MortPerFeMalesComDis") +
                    vSQLiteReader("MortPerFeMalesNonComDis") + vSQLiteReader("MortPerFeMalesInjuries"))
                    End If

                End While
            End If

        Else

            '    Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase Where NCohorts < 2 and Years <= " &
            'CStr(Session("LastYear")) & " ORDER BY Region, NCohorts, Years", dbM)
            'mortDA.Fill(regDS, "Mortality")



            Dim mortDA As OleDb.OleDbDataAdapter

            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                mortDA = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase Where NCohorts < 2 and Years <= " &
                    CStr(Session("LastYear")) & " ORDER BY Region, NCohorts, Years", dbM)
                mortDA.Fill(regDS, "Mortality")
            Else
                'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where CohortId < 2 and TimeId <= " &
                    CStr(Session("LastYear")) & " ORDER BY RegionId, CohortId, TimeId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite2.Load(vSQLiteReader)

                'jrs 2024/05/28
                'dtnew = DTsqlite2.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite2.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite2)
            End If

            'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Mortality").Columns("Region"))

            Dim regMortRel As DataRelation


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regMortRel = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Mortality").Columns("Region"))
            Else


                regMortRel = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("Mor_Det_Base_Case").Columns("RegionId"))


            End If

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("NCohorts")
                                    rId = cRow.Item("Region")

                                    If Not (sType = "Total") Then
                                        TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)
                                    Else
                                        TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                    cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerFemalesComDis") + cRow.Item("MortPerFemalesNonComDis") +
                                    cRow.Item("MortPerFemalesInjuries")) * POPFemales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next

            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("CohortId")
                                    rId = cRow.Item("RegionId")

                                    If Not (sType = "Total") Then
                                        TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)
                                    Else
                                        TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                    cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerFeMalesComDis") + cRow.Item("MortPerFeMalesNonComDis") +
                                    cRow.Item("MortPerFeMalesInjuries")) * POPFemales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next

            End If

            IfsConn.Close()

            'jrs 2007/03/05
            'Population Weigthed average
            For iYr = 1 To Session("LastYear") - Session("FirstYear") + 1
                For g = 1 To 2
                    For j = 0 To 1
                        If TempPopDatabase(j, g, iYr) > 0 Then
                            TempDatabase(j, g, iYr) = TempDatabase(j, g, iYr) / TempPopDatabase(j, g, iYr)
                        End If
                    Next j
                Next g
            Next iYr
        End If

        Dim LastReqAge As Integer
        LastReqAge = 1

        Dim N As Integer, cohMort As Single, nax As Single
        Dim infMort As Single, nqx As Single, lx As Single

        'Computing Life Table (lx)
        For iYr = 1 To Session("LastYear") - Session("FirstYear") + 1
            For g = 1 To 2
                lx = 1
                For j = 0 To LastReqAge
                    'Years in cohort
                    If j = 0 Then
                        N = 1
                    ElseIf j = 1 Then
                        N = 4
                    Else
                        N = 5
                    End If

                    'Mortality by age
                    cohMort = TempDatabase(j, g, iYr) / 1000

                    'Average years lived by those who die (per Keyfitz)
                    If j = 0 Then
                        infMort = cohMort
                        If cohMort >= 0.107 Then
                            nax = 0.34
                        Else
                            nax = 0.049 + 2.742 * cohMort
                        End If
                    ElseIf j = 1 Then
                        If infMort >= 0.107 Then
                            nax = 1.356
                        Else
                            nax = 1.587 - 2.167 * infMort
                        End If
                    Else
                        nax = 2.5
                    End If

                    'Probability of death using period data
                    If j = NCohorts Then
                        nqx = 1
                    Else
                        nqx = (N * cohMort) / (1 + ((N - nax) * cohMort))
                    End If

                    lx = lx * (1 - nqx)
                Next j
                TempDatabase(LastReqAge + 1, g, iYr) = 1 - lx
            Next g
        Next iYr

        'Load Final Array
        For iYr = 1 To Session("LastYear") - Session("FirstYear") + 1
            'jrs 2009/07/02 handling empty years
            If TempPopDatabase(0, 1, iYr) > 0 Then
                'jrs 2010/04/29 changin units to deaths/1000
                If sGender = "Male" Then
                    Session("mPLOT")(iYr, TotalGraphs) = TempDatabase(2, 1, iYr) * 1000
                ElseIf sGender = "Female" Then
                    Session("mPLOT")(iYr, TotalGraphs) = TempDatabase(2, 2, iYr) * 1000
                Else
                    Session("mPLOT")(iYr, TotalGraphs) = (TempDatabase(2, 1, iYr) *
                    (TempPopDatabase(0, 1, iYr) + TempPopDatabase(1, 1, iYr)) +
                    TempDatabase(2, 2, iYr) *
                    (TempPopDatabase(0, 2, iYr) + TempPopDatabase(1, 2, iYr))) /
                    (TempPopDatabase(0, 1, iYr) + TempPopDatabase(0, 2, iYr) +
                    TempPopDatabase(1, 1, iYr) + TempPopDatabase(1, 2, iYr)) * 1000
                End If
            Else
                Session("mPLOT")(iYr, TotalGraphs) = DBNull.Value
            End If
        Next iYr

        'jrs 2010/02/09
        Session("mLegends")(0, TotalGraphs) = sReg
        'If bGrp And mnulblAlias.Checked Then
        '    'Read and load all aliases
        '    Dim db As Database
        '    Dim Tbl As Recordset
        '    db = OpenDatabase(IFs_MDB2)
        '    Tbl = db.OpenRecordset(GroupIDNew)
        '    Tbl.Index = "GroupName"
        '    Tbl.Seek("=", sReg)
        '    If Not Tbl.NoMatch Then
        '        mLegends(1, TotalGraphs) = Tbl.Fields("Alias")
        '    End If
        '    Tbl.Close()
        '    db.Close()
        'Else
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM.Close()
        End If

        Session("mLegends")(1, TotalGraphs) = sReg
        'End If


        If sType <> "Total" Then
            Session("mLegends")(2, TotalGraphs) = sType
        Else
            Session("mLegends")(2, TotalGraphs) = "All Types"
        End If
        If sGender <> "Total" Then
            Session("mLegends")(3, TotalGraphs) = sGender
        Else
            Session("mLegends")(3, TotalGraphs) = "All Genders"
        End If
        Session("mLegends")(4, TotalGraphs) = "Children 0-4"
        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then
            Session("mLegends")(5, TotalGraphs) = "Working"
        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            Session("mLegends")(5, TotalGraphs) = "Base"
        Else
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "")
            Else
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".db", "")
            End If
        End If

        'jrs 2009/07/02
        Session("bSuccess") = True

    End Sub

    'Adult Mortality Probability, computed using Life Tables (survivor tables) lx
    ''Private Sub LoadMortalityAdlt(ByVal TotalGraphs As Integer, ByVal sFileName As String, ByRef dbM As OleDb.OleDbConnection,
    ''ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    ''ByVal sGender As String)

    Private Sub LoadMortalityAdlt(ByVal TotalGraphs As Integer, ByVal sFileName As String,
    ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    ByVal sGender As String)


        'jrs 2009/07/10 added bReport
        'parameter bReport is used to generate comparison report when true


        Dim Tindex As Integer
        Dim rId As Integer
        Dim TCohort As Integer
        Dim sMaleMortFld As String, sFemaleMortFld As String
        Dim iYr As Integer, g As Integer, j As Integer

        Dim TempDatabase(0, 0, 0) As Single
        Dim TempPopDatabase(0, 0, 0) As Single
        'Weighted Average for Population
        Dim POPMales(0, 0, 0) As Single
        Dim POPFemales(0, 0, 0) As Single
        Dim LastReqAge As Integer, FirstReqAge As Integer
        LastReqAge = Session("LastAge") '12 '12 is 55-59, lx = l60
        FirstReqAge = Session("FirstAge") '3 is 10-14, lx = l15

        'jrs 2009/07/02
        Session("bSuccess") = False

        'jrs 2024/05/28
        'jrs 2006/07/14 create connecion
        'Dim IfsConn As OleDb.OleDbConnection
        'IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        Dim IfsConn As SQLiteConnection = New SQLiteConnection
        IfsConn.ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"
        Dim rdr As OleDb.OleDbDataReader
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'myCMD.Connection = dbM
        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow

        Dim dbM As OleDb.OleDbConnection
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileName)
            dbM.Open()
            myCMD.Connection = dbM
        Else 'jrs 2024/05/28
            vSQLiteCon.Close()
            vConnString = "DataSource=" + Session("PutFdrvRun") & sFileName & ";"
            vSQLiteCon = New SQLiteConnection(vConnString)
            vSQLiteCon.Open()
        End If


        'JAM 2020-05-23
        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable

        DTsqlite = New DataTable
        DTsqlite2 = New DataTable
        regDS = New DataSet 'JAM 2020-05-23


        'Select Case sType
        '    Case "OthCommumDis"
        '        sMaleMortFld = "ComDisNoAIDSMales"
        '        sFemaleMortFld = "ComDisNoAIDSFemales"
        '    Case "MaligNeoPl"
        '        sMaleMortFld = "MaligNeoPlasMales"
        '        sFemaleMortFld = "MaligNeoPlasFemales"
        '    Case "CardioVasc"
        '        sMaleMortFld = "CardioVascMales"
        '        sFemaleMortFld = "CardioVascFemales"
        '    Case "Digestive"
        '        sMaleMortFld = "DigestiveMales"
        '        sFemaleMortFld = "DigestiveFemales"
        '    Case "Respiratory"
        '        sMaleMortFld = "RespiratoryMales"
        '        sFemaleMortFld = "RespiratoryFemales"
        '    Case "OtherNonComm"
        '        sMaleMortFld = "OtherG2Males"
        '        sFemaleMortFld = "OtherG2Females"
        '    Case "TrafficAcc"
        '        sMaleMortFld = "RoadTrafficAccMales"
        '        sFemaleMortFld = "RoadTrafficAccFemales"
        '    Case "UnIntInj"
        '        sMaleMortFld = "OtherunintInjMales"
        '        sFemaleMortFld = "OtherunintInjFemales"
        '    Case "IntInj"
        '        sMaleMortFld = "IntenInjMales"
        '        sFemaleMortFld = "IntenInjFemales"
        '    Case "Diabetes"
        '        sMaleMortFld = "DiabetesMales"
        '        sFemaleMortFld = "DiabetesFemales"
        '    Case "AIDS"
        '        sMaleMortFld = "AIDSMales"
        '        sFemaleMortFld = "AIDSFemales"
        '    Case "Diarrhea" 'jrs 2009/08/14
        '        sMaleMortFld = "DiarrheaMales"
        '        sFemaleMortFld = "DiarrheaFemales"
        '    Case "Malaria"
        '        sMaleMortFld = "MalariaMales"
        '        sFemaleMortFld = "MalariaFemales"
        '    Case "RespInfec"
        '        sMaleMortFld = "RespInfMales"
        '        sFemaleMortFld = "RespInfFeMales"
        '    Case "MentalHealth"
        '        sMaleMortFld = "MentalHealthMales"
        '        sFemaleMortFld = "MentalHealthFeMales"
        '    Case "TotComDis"
        '        sMaleMortFld = "MortPerMalesComDis"
        '        sFemaleMortFld = "MortPerFeMalesComDis"
        '    Case "TotNonComDis"
        '        sMaleMortFld = "MortPerMalesNonComDis"
        '        sFemaleMortFld = "MortPerFeMalesNonComDis"
        '    Case "TotInjuries"
        '        sMaleMortFld = "MortPerMalesInjuries"
        '        sFemaleMortFld = "MortPerFeMalesInjuries"
        '    Case "Total"
        '        'need to read all 6 fields
        'End Select

        Dim vchosenColumns As String

        Select Case sType
            Case "OthCommumDis"
                sMaleMortFld = "ComDisNoAIDSMales"
                sFemaleMortFld = "ComDisNoAIDSFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,ComDisNoAIDSMales,ComDisNoAIDSFemales"

            Case "MaligNeoPl"
                sMaleMortFld = "MaligNeoPlasMales"
                sFemaleMortFld = "MaligNeoPlasFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,MaligNeoPlasMales,MaligNeoPlasFemales"
            Case "CardioVasc"
                sMaleMortFld = "CardioVascMales"
                sFemaleMortFld = "CardioVascFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,CardioVascMales,CardioVascFemales"

            Case "Digestive"
                sMaleMortFld = "DigestiveMales"
                sFemaleMortFld = "DigestiveFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DigestiveMales,DigestiveFemales"

            Case "Respiratory"
                sMaleMortFld = "RespiratoryMales"
                sFemaleMortFld = "RespiratoryFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,RespiratoryMales,RespiratoryFemales"

            Case "OtherNonComm"
                sMaleMortFld = "OtherG2Males"
                sFemaleMortFld = "OtherG2Females"

                vchosenColumns = "TimeId,RegionId,CohortId,OtherG2Males,OtherG2Females"

            Case "TrafficAcc"
                sMaleMortFld = "RoadTrafficAccMales"
                sFemaleMortFld = "RoadTrafficAccFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,RoadTrafficAccMales,RoadTrafficAccFemales"
            Case "UnIntInj"
                sMaleMortFld = "OtherunintInjMales"
                sFemaleMortFld = "OtherunintInjFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,OtherunintInjMales,OtherunintInjFemales"
            Case "IntInj"
                sMaleMortFld = "IntenInjMales"
                sFemaleMortFld = "IntenInjFemales"


                vchosenColumns = "TimeId,RegionId,CohortId,IntenInjMales,IntenInjFemales"
            Case "Diabetes"
                sMaleMortFld = "DiabetesMales"
                sFemaleMortFld = "DiabetesFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DiabetesMales,DiabetesFemales"
            Case "AIDS"
                sMaleMortFld = "AIDSMales"
                sFemaleMortFld = "AIDSFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,AIDSMales,AIDSFemales"
            Case "Diarrhea" 'jrs 2009/08/14
                sMaleMortFld = "DiarrheaMales"
                sFemaleMortFld = "DiarrheaFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DiarrheaMales,DiarrheaFemales"
            Case "Malaria"
                sMaleMortFld = "MalariaMales"
                sFemaleMortFld = "MalariaFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,MalariaMales,MalariaFemales"

            Case "RespInfec"
                sMaleMortFld = "RespInfMales"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "RespInfFemales"

                    vchosenColumns = "TimeId,RegionId,CohortId,RespInfMales,RespInfFemales"

            Case "MentalHealth"
                sMaleMortFld = "MentalHealthMales"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MentalHealthFemales"

                    vchosenColumns = "TimeId,RegionId,CohortId,MentalHealthMales,MentalHealthFemales"

            Case "TotComDis"
                sMaleMortFld = "MortPerMalesComDis"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesComDis"

                    vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesComDis,MortPerFemalesComDis"

            Case "TotNonComDis"
                sMaleMortFld = "MortPerMalesNonComDis"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesNonComDis"

                    vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesNonComDis,MortPerFemalesNonComDis"

            Case "TotInjuries"
                sMaleMortFld = "MortPerMalesInjuries"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesInjuries"

                vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesInjuries,MortPerFemalesInjuries"

            Case "Total"
                'need to read all 6 fields

                vchosenColumns = "*"
        End Select


        ReDim TempPopDatabase(NCohorts + 1, 2, Session("LastYear") - Session("FirstYear") + 1)
        ReDim TempDatabase(NCohorts + 1, 2, Session("LastYear") - Session("FirstYear") + 1)

        'take population
        '   Change the data source
        '   ----------------------
        Tindex = 2
        If Not bGrp Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM PopBaseCase Where Region = " &
            Str(iReg) & " and Ncohorts <= " & CStr(LastReqAge) & " and Years <= " & Session("LastYear"))

                'jrs 2009/07/02
                'If tbl2.EOF Then
                '    MsgBox("There's no data in scenario " & _
                '    Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "") & _
                '    " for year " & CStr(iYr), vbCritical, "Load Mortality")
                '    Screen.MousePointer = vbNormal
                '    Exit Sub
                'End If

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    TempPopDatabase(rdr.Item("NCohorts"), 1, rdr.Item("Years") - Session("FirstYear") + 1) = rdr.Item("PopulationMales")
                    TempPopDatabase(rdr.Item("NCohorts"), 2, rdr.Item("Years") - Session("FirstYear") + 1) = rdr.Item("PopulationFemales")
                Loop
                rdr.Close()

            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where RegionId = " &
                            Str(iReg) & " and CohortId <= " & CStr(LastReqAge) & " and TimeId <= " & Session("LastYear")
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())
                    TempPopDatabase(vSQLiteReader("CohortId"), 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = vSQLiteReader("PopulationMales")
                    TempPopDatabase(vSQLiteReader("CohortId"), 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = vSQLiteReader("PopulationFemales")


                End While
            End If
        Else
            ReDim POPMales(NR, NCohorts, Session("LastYear") - Session("FirstYear") + 1)
            ReDim POPFemales(NR, NCohorts, Session("LastYear") - Session("FirstYear") + 1)

            'jrs 2024/05/28
            IfsConn.Open()
            'Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            'Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            'Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            Dim grpDA As OleDb.OleDbDataAdapter
            Dim regDA As OleDb.OleDbDataAdapter
            Dim regIDDA As OleDb.OleDbDataAdapter

            ''Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase Where NCohorts <= " & CStr(LastReqAge) & " and Years <= " & CStr(Session("LastYear")) &
            ''" ORDER BY Region, NCohorts, Years", dbM)
            ''regDS = New DataSet

            Dim popDA As OleDb.OleDbDataAdapter
            regDS = New DataSet
            'jrs 2024/05/28
            regDS.EnforceConstraints = False

            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
            '" ORDER BY Region, NCohorts", dbM)

            'jrs 2024/05/28
            Dim rdGrpCmd As SQLiteCommand = IfsConn.CreateCommand
            rdGrpCmd.CommandText = "SELECT * FROM " & GroupIDNew
            Dim rdGrp As SQLiteDataReader
            rdGrp = rdGrpCmd.ExecuteReader

            Dim myTbl As DataTable = New DataTable
            myTbl.TableName = "Groups"
            regDS.Tables.Add(myTbl)
            regDS.Tables(0).Load(rdGrp)

            Dim rdRegCmd As SQLiteCommand = IfsConn.CreateCommand
            rdRegCmd.CommandText = "SELECT * FROM " & Grouping
            Dim rdReg As SQLiteDataReader
            rdReg = rdRegCmd.ExecuteReader

            Dim myTbl1 As DataTable = New DataTable
            myTbl1.TableName = "Regions"
            regDS.Tables.Add(myTbl1)
            regDS.Tables(1).Load(rdReg)

            Dim rdRegIdCmd As SQLiteCommand = IfsConn.CreateCommand
            rdRegIdCmd.CommandText = "SELECT * FROM " & RegionIDNew
            Dim rdRegId As SQLiteDataReader
            rdRegId = rdRegIdCmd.ExecuteReader

            Dim myTbl2 As DataTable = New DataTable
            myTbl2.TableName = "RegionIDs"
            regDS.Tables.Add(myTbl2)
            regDS.Tables(2).Load(rdRegId)


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                popDA = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase Where NCohorts <= " & CStr(LastReqAge) & " and Years <= " & CStr(Session("LastYear")) &
                                                 " ORDER BY Region, NCohorts, Years", dbM)
                popDA.Fill(regDS, "Population")

                grpDA.Fill(regDS, "Groups")
                regDA.Fill(regDS, "Regions")
                regIDDA.Fill(regDS, "RegionIDs")
            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where CohortId <= " & CStr(LastReqAge) & " and TimeId <= " & CStr(Session("LastYear")) &
                                                 " ORDER BY RegionId, CohortId, TimeId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite.Load(vSQLiteReader)
                'regDS.Tables.Add(DTsqlite)

                'jrs 2024/05/28
                'dtnew = DTsqlite.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite)

            End If


            'popDA.Fill(regDS, "Population")
            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                 regDS.Tables("Groups").Columns("GroupNum"),
                                                 regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Population").Columns("Region"))

            Dim regPopRel As DataRelation

            'JAM 2020-06-28
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Population").Columns("Region"))
            Else


                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("pop_base_case").Columns(1))


            End If


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("NCohorts"), 1, cRow.Item("Years") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("NCohorts"), 1, cRow.Item("Years") - Session("FirstYear") + 1) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("NCohorts"), 2, cRow.Item("Years") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("NCohorts"), 2, cRow.Item("Years") - Session("FirstYear") + 1) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("Region"), cRow.Item("NCohorts"), cRow.Item("Years") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("Region"), cRow.Item("NCohorts"), cRow.Item("Years") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next
            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("CohortId"), 1, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("CohortId"), 1, cRow.Item("TimeId") - Session("FirstYear") + 1) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("CohortId"), 2, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("CohortId"), 2, cRow.Item("TimeId") - Session("FirstYear") + 1) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("RegionId"), cRow.Item("CohortId"), cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId"), cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next
            End If
            'jrs 2009/07/02
            'If tbl2.EOF Then
            '    MsgBox("There's no data in scenario " & _
            '    Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "") & _
            '    " for year " & CStr(iYr), vbCritical, "Load Mortality")
            '    Screen.MousePointer = vbNormal
            '    Exit Sub
            'End If
        End If
        'End take population


        '   Change the data source
        '   ----------------------
        If Not bGrp Then

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM MorDetBaseCase Where Region = " & Str(iReg) & " and NCohorts <= " & CStr(LastReqAge) &
            " and Years <= " & CStr(Session("LastYear")))

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    If Not (sType = "Total") Then
                        'males 
                        TCohort = rdr.Item("NCohorts")
                        TempDatabase(TCohort, 1, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item(sMaleMortFld))

                        'females 
                        TempDatabase(TCohort, 2, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item(sFemaleMortFld))
                    Else
                        'males 
                        TCohort = rdr.Item("NCohorts")
                        TempDatabase(TCohort, 1, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item("MortPerMalesComDis") +
                    rdr.Item("MortPerMalesNonComDis") + rdr.Item("MortPerMalesInjuries"))

                        'females 
                        TempDatabase(TCohort, 2, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item("MortPerFeMalesComDis") +
                    rdr.Item("MortPerFemalesNonComDis") + rdr.Item("MortPerFemalesInjuries"))
                    End If
                Loop
                rdr.Close()

            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where RegionId = " & Str(iReg) & " and CohortId <= " & CStr(LastReqAge) &
                                            " and TimeId <= " & CStr(Session("LastYear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())
                    If Not (sType = "Total") Then
                        'males 
                        TCohort = vSQLiteReader("CohortId")
                        TempDatabase(TCohort, 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader(sMaleMortFld))

                        'females 
                        TempDatabase(TCohort, 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                    Else
                        'males 
                        TCohort = vSQLiteReader("CohortId")
                        TempDatabase(TCohort, 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader("MortPerMalesComDis") +
                        vSQLiteReader("MortPerMalesNonComDis") + vSQLiteReader("MortPerMalesInjuries"))

                        'females 
                        TempDatabase(TCohort, 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader("MortPerFeMalesComDis") +
                        vSQLiteReader("MortPerFeMalesNonComDis") + vSQLiteReader("MortPerFeMalesInjuries"))
                    End If

                End While
            End If
        Else

            '    Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase Where NCohorts <= " & CStr(LastReqAge) & " and Years <= " &
            'CStr(Session("LastYear")) & " ORDER BY Region, NCohorts, Years", dbM)
            'mortDA.Fill(regDS, "Mortality")

            Dim mortDA As OleDb.OleDbDataAdapter


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                mortDA = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase Where NCohorts <= " & CStr(LastReqAge) & " and Years <= " &
                                        CStr(Session("LastYear")) & " ORDER BY Region, NCohorts, Years", dbM)
                mortDA.Fill(regDS, "Mortality")
            Else
                'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where CohortId <= " & CStr(LastReqAge) & " and TimeId <= " &
                            CStr(Session("LastYear")) & " ORDER BY RegionId, CohortId, TimeId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite2.Load(vSQLiteReader)

                'jrs 2024/05/28
                'dtnew = DTsqlite2.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite2.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite2)
            End If




            'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Mortality").Columns("Region"))


            Dim regMortRel As DataRelation


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regMortRel = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Mortality").Columns("Region"))
            Else


                regMortRel = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("Mor_Det_Base_Case").Columns("RegionId"))


            End If

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("NCohorts")
                                    rId = cRow.Item("Region")

                                    If Not (sType = "Total") Then
                                        TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)
                                    Else
                                        TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                    cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerFemalesComDis") + cRow.Item("MortPerFemalesNonComDis") +
                                    cRow.Item("MortPerFemalesInjuries")) * POPFemales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next


            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("CohortId")
                                    rId = cRow.Item("RegionId")

                                    If Not (sType = "Total") Then
                                        TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)
                                    Else
                                        TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                    cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerFeMalesComDis") + cRow.Item("MortPerFeMalesNonComDis") +
                                    cRow.Item("MortPerFeMalesInjuries")) * POPFemales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next


            End If
            IfsConn.Close()

            'jrs 2007/03/05
            'Population Weigthed average
            For iYr = 1 To Session("LastYear") - Session("FirstYear") + 1
                For g = 1 To 2
                    For j = 0 To LastReqAge
                        If TempPopDatabase(j, g, iYr) > 0 Then
                            TempDatabase(j, g, iYr) = TempDatabase(j, g, iYr) / TempPopDatabase(j, g, iYr)
                        End If
                    Next j
                Next g
            Next iYr
        End If

        Dim N As Integer, cohMort As Single, nax As Single
        Dim infMort As Single, nqx As Single, lx As Single

        'Computing Life Table (lx)
        For iYr = 1 To Session("LastYear") - Session("FirstYear") + 1
            For g = 1 To 2
                lx = 1
                For j = 0 To LastReqAge
                    'Years in cohort
                    If j = 0 Then
                        N = 1
                    ElseIf j = 1 Then
                        N = 4
                    Else
                        N = 5
                    End If

                    'Mortality by age
                    cohMort = TempDatabase(j, g, iYr) / 1000

                    'Average years lived by those who die (per Keyfitz)
                    If j = 0 Then
                        infMort = cohMort
                        If cohMort >= 0.107 Then
                            nax = 0.34
                        Else
                            nax = 0.049 + 2.742 * cohMort
                        End If
                    ElseIf j = 1 Then
                        If infMort >= 0.107 Then
                            nax = 1.356
                        Else
                            nax = 1.587 - 2.167 * infMort
                        End If
                    Else
                        nax = 2.5
                    End If

                    'Probability of death using period data
                    If j = NCohorts Then
                        nqx = 1
                    Else
                        nqx = (N * cohMort) / (1 + ((N - nax) * cohMort))
                    End If

                    lx = lx * (1 - nqx)

                    If j = FirstReqAge Then
                        TempDatabase(0, g, iYr) = lx
                    End If

                Next j
                If TempDatabase(0, g, iYr) > 0 Then
                    TempDatabase(LastReqAge + 1, g, iYr) = Math.Round(1 - lx / TempDatabase(0, g, iYr), 4)
                Else
                    TempDatabase(LastReqAge + 1, g, iYr) = 1
                End If
            Next g
        Next iYr

        'Load Final Array
        For iYr = 1 To Session("LastYear") - Session("FirstYear") + 1
            'jrs 2009/07/02 handling empty years
            If TempPopDatabase(0, 1, iYr) > 0 Then
                'jrs 2010/04/29 changin units to deaths/1000
                If sGender = "Male" Then
                    Session("mPLOT")(iYr, TotalGraphs) = TempDatabase(LastReqAge + 1, 1, iYr) * 1000
                ElseIf sGender = "Female" Then
                    Session("mPLOT")(iYr, TotalGraphs) = TempDatabase(LastReqAge + 1, 2, iYr) * 1000
                Else
                    Dim POPM As Single, POPF As Single
                    For j = FirstReqAge To LastReqAge
                        POPM = POPM + TempPopDatabase(j, 1, iYr)
                        POPF = POPF + TempPopDatabase(j, 2, iYr)
                    Next j

                    Session("mPLOT")(iYr, TotalGraphs) = (TempDatabase(LastReqAge + 1, 1, iYr) *
                    POPM + TempDatabase(LastReqAge + 1, 2, iYr) * POPF) /
                    (POPM + POPF) * 1000
                End If
            Else
                Session("mPLOT")(iYr, TotalGraphs) = DBNull.Value
            End If
        Next iYr


        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM.Close()
        End If

        'jrs 2010/02/09
        Session("mLegends")(0, TotalGraphs) = sReg
        'If bGrp And mnulblAlias.Checked Then
        '    'Read and load all aliases
        '    Dim db As Database
        '    Dim Tbl As Recordset
        '    db = OpenDatabase(IFs_MDB2)
        '    Tbl = db.OpenRecordset(GroupIDNew)
        '    Tbl.Index = "GroupName"
        '    Tbl.Seek("=", sReg)
        '    If Not Tbl.NoMatch Then
        '        mLegends(1, TotalGraphs) = Tbl.Fields("Alias")
        '    End If
        '    Tbl.Close()
        '    db.Close()
        'Else
        Session("mLegends")(1, TotalGraphs) = sReg
        'End If


        If sType <> "Total" Then
            Session("mLegends")(2, TotalGraphs) = sType
        Else
            Session("mLegends")(2, TotalGraphs) = "All Types"
        End If
        If sGender <> "Total" Then
            Session("mLegends")(3, TotalGraphs) = sGender
        Else
            Session("mLegends")(3, TotalGraphs) = "All Genders"
        End If
        Session("mLegends")(4, TotalGraphs) = "Adults " & CStr(FirstReqAge * 5) & " to " & CStr(LastReqAge * 5 - 1)
        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then
            Session("mLegends")(5, TotalGraphs) = "Working"
        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            Session("mLegends")(5, TotalGraphs) = "Base"
        Else
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "")
            Else
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".db", "")
            End If
        End If

        'jrs 2009/07/02
        Session("bSuccess") = True

    End Sub
    ''Private Sub LoadMortalityInf(ByVal TotalGraphs As Integer, ByVal sFileName As String, ByRef dbM As OleDb.OleDbConnection,
    ''ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    ''ByVal sGender As String)


    'Children Mortality Rate
    Private Sub LoadMortalityInf(ByVal TotalGraphs As Integer, ByVal sFileName As String,
    ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    ByVal sGender As String)
        'jrs 2009/07/10 added bReport
        'parameter bReport is used to generate comparison report when true


        Dim Tindex As Integer
        Dim rId As Integer
        Dim TCohort As Integer
        Dim sMaleMortFld As String, sFemaleMortFld As String
        Dim iYr As Integer, g As Integer, j As Integer

        Dim TempDatabase(0, 0, 0) As Single
        Dim TempPopDatabase(0, 0, 0) As Single
        'Weighted Average for Population
        Dim POPMales(0, 0, 0) As Single
        Dim POPFemales(0, 0, 0) As Single

        'jrs 2009/07/02
        Session("bSuccess") = False

        'jrs 2024/05/28
        'jrs 2006/07/14 create connecion
        'Dim IfsConn As OleDb.OleDbConnection
        'IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        Dim IfsConn As SQLiteConnection = New SQLiteConnection
        IfsConn.ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"
        Dim rdr As OleDb.OleDbDataReader
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'myCMD.Connection = dbM
        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow

        Dim dbM As OleDb.OleDbConnection
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileName)
            dbM.Open()
            myCMD.Connection = dbM
        Else 'jrs 2024/05/28
            vSQLiteCon.Close()
            vConnString = "DataSource=" + Session("PutFdrvRun") & sFileName & ";"
            vSQLiteCon = New SQLiteConnection(vConnString)
            vSQLiteCon.Open()

        End If

        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable

        DTsqlite = New DataTable
        DTsqlite2 = New DataTable
        regDS = New DataSet


        'Select Case sType
        '    Case "OthCommumDis"
        '        sMaleMortFld = "ComDisNoAIDSMales"
        '        sFemaleMortFld = "ComDisNoAIDSFemales"
        '    Case "MaligNeoPl"
        '        sMaleMortFld = "MaligNeoPlasMales"
        '        sFemaleMortFld = "MaligNeoPlasFemales"
        '    Case "CardioVasc"
        '        sMaleMortFld = "CardioVascMales"
        '        sFemaleMortFld = "CardioVascFemales"
        '    Case "Digestive"
        '        sMaleMortFld = "DigestiveMales"
        '        sFemaleMortFld = "DigestiveFemales"
        '    Case "Respiratory"
        '        sMaleMortFld = "RespiratoryMales"
        '        sFemaleMortFld = "RespiratoryFemales"
        '    Case "OtherNonComm"
        '        sMaleMortFld = "OtherG2Males"
        '        sFemaleMortFld = "OtherG2Females"
        '    Case "TrafficAcc"
        '        sMaleMortFld = "RoadTrafficAccMales"
        '        sFemaleMortFld = "RoadTrafficAccFemales"
        '    Case "UnIntInj"
        '        sMaleMortFld = "OtherunintInjMales"
        '        sFemaleMortFld = "OtherunintInjFemales"
        '    Case "IntInj"
        '        sMaleMortFld = "IntenInjMales"
        '        sFemaleMortFld = "IntenInjFemales"
        '    Case "Diabetes"
        '        sMaleMortFld = "DiabetesMales"
        '        sFemaleMortFld = "DiabetesFemales"
        '    Case "AIDS"
        '        sMaleMortFld = "AIDSMales"
        '        sFemaleMortFld = "AIDSFemales"
        '    Case "Diarrhea" 'jrs 2009/08/14
        '        sMaleMortFld = "DiarrheaMales"
        '        sFemaleMortFld = "DiarrheaFemales"
        '    Case "Malaria"
        '        sMaleMortFld = "MalariaMales"
        '        sFemaleMortFld = "MalariaFemales"
        '    Case "RespInfec"
        '        sMaleMortFld = "RespInfMales"
        '        sFemaleMortFld = "RespInfFeMales"
        '    Case "MentalHealth"
        '        sMaleMortFld = "MentalHealthMales"
        '        sFemaleMortFld = "MentalHealthFeMales"
        '    Case "TotComDis"
        '        sMaleMortFld = "MortPerMalesComDis"
        '        sFemaleMortFld = "MortPerFeMalesComDis"
        '    Case "TotNonComDis"
        '        sMaleMortFld = "MortPerMalesNonComDis"
        '        sFemaleMortFld = "MortPerFeMalesNonComDis"
        '    Case "TotInjuries"
        '        sMaleMortFld = "MortPerMalesInjuries"
        '        sFemaleMortFld = "MortPerFeMalesInjuries"
        '    Case "Total"
        '        'need to read all 6 fields
        'End Select

        Dim vchosenColumns As String

        Select Case sType
            Case "OthCommumDis"
                sMaleMortFld = "ComDisNoAIDSMales"
                sFemaleMortFld = "ComDisNoAIDSFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,ComDisNoAIDSMales,ComDisNoAIDSFemales"

            Case "MaligNeoPl"
                sMaleMortFld = "MaligNeoPlasMales"
                sFemaleMortFld = "MaligNeoPlasFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,MaligNeoPlasMales,MaligNeoPlasFemales"
            Case "CardioVasc"
                sMaleMortFld = "CardioVascMales"
                sFemaleMortFld = "CardioVascFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,CardioVascMales,CardioVascFemales"

            Case "Digestive"
                sMaleMortFld = "DigestiveMales"
                sFemaleMortFld = "DigestiveFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DigestiveMales,DigestiveFemales"

            Case "Respiratory"
                sMaleMortFld = "RespiratoryMales"
                sFemaleMortFld = "RespiratoryFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,RespiratoryMales,RespiratoryFemales"

            Case "OtherNonComm"
                sMaleMortFld = "OtherG2Males"
                sFemaleMortFld = "OtherG2Females"

                vchosenColumns = "TimeId,RegionId,CohortId,OtherG2Males,OtherG2Females"

            Case "TrafficAcc"
                sMaleMortFld = "RoadTrafficAccMales"
                sFemaleMortFld = "RoadTrafficAccFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,RoadTrafficAccMales,RoadTrafficAccFemales"
            Case "UnIntInj"
                sMaleMortFld = "OtherunintInjMales"
                sFemaleMortFld = "OtherunintInjFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,OtherunintInjMales,OtherunintInjFemales"
            Case "IntInj"
                sMaleMortFld = "IntenInjMales"
                sFemaleMortFld = "IntenInjFemales"


                vchosenColumns = "TimeId,RegionId,CohortId,IntenInjMales,IntenInjFemales"
            Case "Diabetes"
                sMaleMortFld = "DiabetesMales"
                sFemaleMortFld = "DiabetesFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DiabetesMales,DiabetesFemales"
            Case "AIDS"
                sMaleMortFld = "AIDSMales"
                sFemaleMortFld = "AIDSFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,AIDSMales,AIDSFemales"
            Case "Diarrhea" 'jrs 2009/08/14
                sMaleMortFld = "DiarrheaMales"
                sFemaleMortFld = "DiarrheaFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DiarrheaMales,DiarrheaFemales"
            Case "Malaria"
                sMaleMortFld = "MalariaMales"
                sFemaleMortFld = "MalariaFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,MalariaMales,MalariaFemales"

            Case "RespInfec"
                sMaleMortFld = "RespInfMales"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "RespInfFemales"

                    vchosenColumns = "TimeId,RegionId,CohortId,RespInfMales,RespInfFemales"

            Case "MentalHealth"
                sMaleMortFld = "MentalHealthMales"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MentalHealthFemales"

                    vchosenColumns = "TimeId,RegionId,CohortId,MentalHealthMales,MentalHealthFemales"

            Case "TotComDis"
                sMaleMortFld = "MortPerMalesComDis"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesComDis"

                    vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesComDis,MortPerFemalesComDis"

            Case "TotNonComDis"
                sMaleMortFld = "MortPerMalesNonComDis"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesNonComDis"

                    vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesNonComDis,MortPerFemalesNonComDis"

            Case "TotInjuries"
                sMaleMortFld = "MortPerMalesInjuries"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesInjuries"

                vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesInjuries,MortPerFemalesInjuries"

            Case "Total"
                'need to read all 6 fields

                vchosenColumns = "*"
        End Select


        ReDim TempPopDatabase(1, 2, Session("LastYear") - Session("FirstYear") + 1)
        ReDim TempDatabase(2, 2, Session("LastYear") - Session("FirstYear") + 1)

        'take population
        '   Change the data source
        '   ----------------------
        Tindex = 2
        If Not bGrp Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM PopBaseCase Where Region = " &
            Str(iReg) & " and NCohorts < 2 and Years <= " & Session("LastYear"))

                'jrs 2009/07/02
                'If tbl2.EOF Then
                '    MsgBox("There's no data in scenario " & _
                '    Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "") & _
                '    " for year " & CStr(iYr), vbCritical, "Load Mortality")
                '    Screen.MousePointer = vbNormal
                '    Exit Sub
                'End If

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    TempPopDatabase(rdr.Item("NCohorts"), 1, rdr.Item("Years") - Session("FirstYear") + 1) = rdr.Item("PopulationMales")
                    TempPopDatabase(rdr.Item("NCohorts"), 2, rdr.Item("Years") - Session("FirstYear") + 1) = rdr.Item("PopulationFemales")
                Loop
                rdr.Close()

            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where RegionId = " &
                                            Str(iReg) & " and CohortId < 2 and TimeId <= " & Session("LastYear")
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())

                    TempPopDatabase(vSQLiteReader("CohortId"), 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = vSQLiteReader("PopulationMales")
                    TempPopDatabase(vSQLiteReader("CohortId"), 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = vSQLiteReader("PopulationFemales")

                End While
            End If
        Else
            ReDim POPMales(NR, NCohorts, Session("LastYear") - Session("FirstYear") + 1)
            ReDim POPFemales(NR, NCohorts, Session("LastYear") - Session("FirstYear") + 1)

            'jrs 2024/05/28
            IfsConn.Open()
            'Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            'Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            'Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            Dim grpDA As OleDb.OleDbDataAdapter
            Dim regDA As OleDb.OleDbDataAdapter
            Dim regIDDA As OleDb.OleDbDataAdapter

            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase Where NCohorts < 2 and Years <= " & CStr(Session("LastYear")) & " ORDER BY Region, NCohorts, Years", dbM)
            'regDS = New DataSet

            Dim popDA As OleDb.OleDbDataAdapter
            regDS = New DataSet
            'jrs 2024/05/28
            regDS.EnforceConstraints = False

            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
            '" ORDER BY Region, NCohorts", dbM)

            'jrs 2024/05/28
            Dim rdGrpCmd As SQLiteCommand = IfsConn.CreateCommand
            rdGrpCmd.CommandText = "SELECT * FROM " & GroupIDNew
            Dim rdGrp As SQLiteDataReader
            rdGrp = rdGrpCmd.ExecuteReader

            Dim myTbl As DataTable = New DataTable
            myTbl.TableName = "Groups"
            regDS.Tables.Add(myTbl)
            regDS.Tables(0).Load(rdGrp)

            Dim rdRegCmd As SQLiteCommand = IfsConn.CreateCommand
            rdRegCmd.CommandText = "SELECT * FROM " & Grouping
            Dim rdReg As SQLiteDataReader
            rdReg = rdRegCmd.ExecuteReader

            Dim myTbl1 As DataTable = New DataTable
            myTbl1.TableName = "Regions"
            regDS.Tables.Add(myTbl1)
            regDS.Tables(1).Load(rdReg)

            Dim rdRegIdCmd As SQLiteCommand = IfsConn.CreateCommand
            rdRegIdCmd.CommandText = "SELECT * FROM " & RegionIDNew
            Dim rdRegId As SQLiteDataReader
            rdRegId = rdRegIdCmd.ExecuteReader

            Dim myTbl2 As DataTable = New DataTable
            myTbl2.TableName = "RegionIDs"
            regDS.Tables.Add(myTbl2)
            regDS.Tables(2).Load(rdRegId)

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                popDA = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase Where NCohorts < 2 and Years <= " & CStr(Session("LastYear")) & " ORDER BY Region, NCohorts, Years", dbM)
                popDA.Fill(regDS, "Population")
                'jrs 2024/05/24
                grpDA.Fill(regDS, "Groups")
                regDA.Fill(regDS, "Regions")
                regIDDA.Fill(regDS, "RegionIDs")

            Else


                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where CohortId < 2 and TimeId <= " & CStr(Session("LastYear")) & " ORDER BY RegionId, CohortId, TimeId"

                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite.Load(vSQLiteReader)
                'regDS.Tables.Add(DTsqlite)

                'jrs 2024/05/28
                'dtnew = DTsqlite.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite)

            End If



            'popDA.Fill(regDS, "Population")
            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                 regDS.Tables("Groups").Columns("GroupNum"),
                                                 regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Population").Columns("Region"))


            Dim regPopRel As DataRelation

            'JAM 2020-06-29
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Population").Columns("Region"))
            Else


                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("pop_base_case").Columns(1))


            End If


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("NCohorts"), 1, cRow.Item("Years") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("NCohorts"), 1, cRow.Item("Years") - Session("FirstYear") + 1) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("NCohorts"), 2, cRow.Item("Years") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("NCohorts"), 2, cRow.Item("Years") - Session("FirstYear") + 1) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("Region"), cRow.Item("NCohorts"), cRow.Item("Years") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("Region"), cRow.Item("NCohorts"), cRow.Item("Years") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next

            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("CohortId"), 1, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("CohortId"), 1, cRow.Item("TimeId") - Session("FirstYear") + 1) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("CohortId"), 2, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("CohortId"), 2, cRow.Item("TimeId") - Session("FirstYear") + 1) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("RegionId"), cRow.Item("CohortId"), cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId"), cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next


            End If
            'jrs 2009/07/02
            'If tbl2.EOF Then
            '    MsgBox("There's no data in scenario " & _
            '    Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "") & _
            '    " for year " & CStr(iYr), vbCritical, "Load Mortality")
            '    Screen.MousePointer = vbNormal
            '    Exit Sub
            'End If

            'End take population

        End If
        '   Change the data source
        '   ----------------------
        If Not bGrp Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM MorDetBaseCase Where Region = " & Str(iReg) &
                                " and NCohorts < 2 and Years <= " & CStr(Session("LastYear")))

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    If Not (sType = "Total") Then
                        'males 
                        TCohort = rdr.Item("NCohorts")
                        TempDatabase(TCohort, 1, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item(sMaleMortFld))

                        'females 
                        TempDatabase(TCohort, 2, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item(sFemaleMortFld))
                    Else
                        'males 
                        TCohort = rdr.Item("NCohorts")
                        TempDatabase(TCohort, 1, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item("MortPerMalesComDis") +
                        rdr.Item("MortPerMalesNonComDis") + rdr.Item("MortPerMalesInjuries"))

                        'females 
                        TempDatabase(TCohort, 2, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item("MortPerFeMalesComDis") +
                        rdr.Item("MortPerFemalesNonComDis") + rdr.Item("MortPerFemalesInjuries"))
                    End If

                    TempDatabase(TCohort, 1, rdr.Item("Years") - Session("FirstYear") + 1) =
                    TempDatabase(TCohort, 1, rdr.Item("Years") - Session("FirstYear") + 1) *
                    TempPopDatabase(TCohort, 1, rdr.Item("Years") - Session("FirstYear") + 1)

                    TempDatabase(TCohort, 2, rdr.Item("Years") - Session("FirstYear") + 1) =
                    TempDatabase(TCohort, 2, rdr.Item("Years") - Session("FirstYear") + 1) *
                    TempPopDatabase(TCohort, 2, rdr.Item("Years") - Session("FirstYear") + 1)

                Loop
                rdr.Close()

            Else

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where RegionId = " & Str(iReg) &
                                            " and CohortId < 2 and TimeId <= " & CStr(Session("LastYear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())
                    If Not (sType = "Total") Then
                        'males 
                        TCohort = vSQLiteReader("CohortId")
                        TempDatabase(TCohort, 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader(sMaleMortFld))

                        'females 
                        TempDatabase(TCohort, 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                    Else
                        'males 
                        TCohort = vSQLiteReader("CohortId")
                        TempDatabase(TCohort, 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader("MortPerMalesComDis") +
                        vSQLiteReader("MortPerMalesNonComDis") + vSQLiteReader("MortPerMalesInjuries"))

                        'females 
                        TempDatabase(TCohort, 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader("MortPerFeMalesComDis") +
                        vSQLiteReader("MortPerFeMalesNonComDis") + vSQLiteReader("MortPerFeMalesInjuries"))
                    End If

                    TempDatabase(TCohort, 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) =
                    TempDatabase(TCohort, 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) *
                    TempPopDatabase(TCohort, 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1)

                    TempDatabase(TCohort, 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) =
                    TempDatabase(TCohort, 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) *
                    TempPopDatabase(TCohort, 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1)

                End While

            End If

        Else

            'Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase Where NCohorts < 2 and Years <= " &
            'CStr(Session("LastYear")) & " ORDER BY Region, NCohorts, Years", dbM)
            '    mortDA.Fill(regDS, "Mortality")


            Dim mortDA As OleDb.OleDbDataAdapter

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                mortDA = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase Where NCohorts < 2 and Years <= " &
                                                CStr(Session("LastYear")) & " ORDER BY Region, NCohorts, Years", dbM)
                mortDA.Fill(regDS, "Mortality")
            Else
                'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where CohortId < 2 and TimeId <= " &
                                            CStr(Session("LastYear")) & " ORDER BY RegionId, CohortId, TimeId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite2.Load(vSQLiteReader)

                'jrs 2024/05/28
                'dtnew = DTsqlite2.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite2.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite2)
            End If






            'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
            '                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                 regDS.Tables("Mortality").Columns("Region"))


            Dim regMortRel As DataRelation

            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regMortRel = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Mortality").Columns("Region"))
            Else


                regMortRel = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("Mor_Det_Base_Case").Columns("RegionId"))


            End If

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("NCohorts")
                                    rId = cRow.Item("Region")

                                    If Not (sType = "Total") Then
                                        TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)
                                    Else
                                        TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                    cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerFemalesComDis") + cRow.Item("MortPerFemalesNonComDis") +
                                    cRow.Item("MortPerFemalesInjuries")) * POPFemales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next

            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("CohortId")
                                    rId = cRow.Item("RegionId")

                                    If Not (sType = "Total") Then
                                        TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)
                                    Else
                                        TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerFeMalesComDis") + cRow.Item("MortPerFeMalesNonComDis") +
                                    cRow.Item("MortPerFeMalesInjuries")) * POPFemales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next

            End If

            IfsConn.Close()

        End If


        'JAM 2020-06-29
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM.Close()
        End If


        'jrs 2007/03/05
        'Population Weigthed average
        For iYr = 1 To Session("LastYear") - Session("FirstYear") + 1
            If Not Session("bUseDeaths") Then
                'jrs 2009/07/02 handling empty years
                If TempPopDatabase(0, 1, iYr) > 0 Then
                    TempDatabase(2, 1, iYr) = (TempDatabase(0, 1, iYr) + TempDatabase(1, 1, iYr)) / (TempPopDatabase(0, 1, iYr) + TempPopDatabase(1, 1, iYr))

                    TempDatabase(2, 2, iYr) = (TempDatabase(0, 2, iYr) + TempDatabase(1, 2, iYr)) / (TempPopDatabase(0, 2, iYr) + TempPopDatabase(1, 2, iYr))
                End If
            Else
                TempDatabase(2, 1, iYr) = (TempDatabase(0, 1, iYr) + TempDatabase(1, 1, iYr))

                TempDatabase(2, 2, iYr) = (TempDatabase(0, 2, iYr) + TempDatabase(1, 2, iYr))
            End If
        Next iYr

        'Load Final Array
        For iYr = 1 To Session("LastYear") - Session("FirstYear") + 1
            'jrs 2009/07/02 handling empty years
            If TempPopDatabase(0, 1, iYr) > 0 Then
                'jrs 2010/04/29 changin units to deaths/1000
                If sGender = "Male" Then
                    Session("mPLOT")(iYr, TotalGraphs) = TempDatabase(2, 1, iYr)
                ElseIf sGender = "Female" Then
                    Session("mPLOT")(iYr, TotalGraphs) = TempDatabase(2, 2, iYr)
                Else
                    If Not Session("bUseDeaths") Then
                        Session("mPLOT")(iYr, TotalGraphs) = (TempDatabase(2, 1, iYr) *
                        (TempPopDatabase(0, 1, iYr) + TempPopDatabase(1, 1, iYr)) +
                        TempDatabase(2, 2, iYr) *
                        (TempPopDatabase(0, 2, iYr) + TempPopDatabase(1, 2, iYr))) /
                        (TempPopDatabase(0, 1, iYr) + TempPopDatabase(0, 2, iYr) +
                        TempPopDatabase(1, 1, iYr) + TempPopDatabase(1, 2, iYr))
                    Else
                        Session("mPLOT")(iYr, TotalGraphs) = TempDatabase(2, 1, iYr) +
                        TempDatabase(2, 2, iYr)
                    End If
                End If
            Else
                Session("mPLOT")(iYr, TotalGraphs) = DBNull.Value
            End If
        Next iYr

        'jrs 2010/02/09
        Session("mLegends")(0, TotalGraphs) = sReg
        'If bGrp And mnulblAlias.Checked Then
        '    'Read and load all aliases
        '    Dim db As Database
        '    Dim Tbl As Recordset
        '    db = OpenDatabase(IFs_MDB2)
        '    Tbl = db.OpenRecordset(GroupIDNew)
        '    Tbl.Index = "GroupName"
        '    Tbl.Seek("=", sReg)
        '    If Not Tbl.NoMatch Then
        '        mLegends(1, TotalGraphs) = Tbl.Fields("Alias")
        '    End If
        '    Tbl.Close()
        '    db.Close()
        'Else
        Session("mLegends")(1, TotalGraphs) = sReg
        'End If


        If sType <> "Total" Then
            Session("mLegends")(2, TotalGraphs) = sType
        Else
            Session("mLegends")(2, TotalGraphs) = "All Types"
        End If
        If sGender <> "Total" Then
            Session("mLegends")(3, TotalGraphs) = sGender
        Else
            Session("mLegends")(3, TotalGraphs) = "All Genders"
        End If
        Session("mLegends")(4, TotalGraphs) = "Children 0-4"
        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then
            Session("mLegends")(5, TotalGraphs) = "Working"
        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            Session("mLegends")(5, TotalGraphs) = "Base"
        Else
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "")

            Else
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".db", "")
            End If
        End If

        'jrs 2009/07/02
        Session("bSuccess") = True

    End Sub
    'Private Sub LoadMortalityRateAdlt(ByVal TotalGraphs As Integer, ByVal sFileName As String, ByRef dbM As OleDb.OleDbConnection,
    'ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    'ByVal sGender As String)


    'Adult Mortality Rate
    Private Sub LoadMortalityRateAdlt(ByVal TotalGraphs As Integer, ByVal sFileName As String,
    ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    ByVal sGender As String)
        'jrs 2009/07/10 added bReport
        'parameter bReport is used to generate comparison report when true


        Dim Tindex As Integer
        Dim rId As Integer
        Dim TCohort As Integer
        Dim sMaleMortFld As String, sFemaleMortFld As String
        Dim iYr As Integer, g As Integer, j As Integer

        Dim TempDatabase(0, 0, 0) As Single
        Dim TempPopDatabase(0, 0, 0) As Single
        'Weighted Average for Population
        Dim POPMales(0, 0, 0) As Single
        Dim POPFemales(0, 0, 0) As Single
        Dim LastReqAge As Integer, FirstReqAge As Integer
        LastReqAge = Session("LastAge") '12 '12 is 55-59, lx = l60
        FirstReqAge = Session("FirstAge") + 1 '3 is 10-14, lx = l15

        'jrs 2009/07/02
        Session("bSuccess") = False

        'jrs 2024/05/28
        'jrs 2006/07/14 create connecion
        'Dim IfsConn As OleDb.OleDbConnection
        'IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        Dim IfsConn As SQLiteConnection = New SQLiteConnection
        IfsConn.ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"
        Dim rdr As OleDb.OleDbDataReader
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'myCMD.Connection = dbM
        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow

        Dim dbM As OleDb.OleDbConnection

        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileName)
            dbM.Open()
            myCMD.Connection = dbM
        Else 'jrs 2024/05/28
            vSQLiteCon.Close()
            vConnString = "DataSource=" + Session("PutFdrvRun") & sFileName & ";"
            vSQLiteCon = New SQLiteConnection(vConnString)
            vSQLiteCon.Open()

        End If

        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable

        DTsqlite = New DataTable
        DTsqlite2 = New DataTable
        regDS = New DataSet

        'Select Case sType
        '    Case "OthCommumDis"
        '        sMaleMortFld = "ComDisNoAIDSMales"
        '        sFemaleMortFld = "ComDisNoAIDSFemales"
        '    Case "MaligNeoPl"
        '        sMaleMortFld = "MaligNeoPlasMales"
        '        sFemaleMortFld = "MaligNeoPlasFemales"
        '    Case "CardioVasc"
        '        sMaleMortFld = "CardioVascMales"
        '        sFemaleMortFld = "CardioVascFemales"
        '    Case "Digestive"
        '        sMaleMortFld = "DigestiveMales"
        '        sFemaleMortFld = "DigestiveFemales"
        '    Case "Respiratory"
        '        sMaleMortFld = "RespiratoryMales"
        '        sFemaleMortFld = "RespiratoryFemales"
        '    Case "OtherNonComm"
        '        sMaleMortFld = "OtherG2Males"
        '        sFemaleMortFld = "OtherG2Females"
        '    Case "TrafficAcc"
        '        sMaleMortFld = "RoadTrafficAccMales"
        '        sFemaleMortFld = "RoadTrafficAccFemales"
        '    Case "UnIntInj"
        '        sMaleMortFld = "OtherunintInjMales"
        '        sFemaleMortFld = "OtherunintInjFemales"
        '    Case "IntInj"
        '        sMaleMortFld = "IntenInjMales"
        '        sFemaleMortFld = "IntenInjFemales"
        '    Case "Diabetes"
        '        sMaleMortFld = "DiabetesMales"
        '        sFemaleMortFld = "DiabetesFemales"
        '    Case "AIDS"
        '        sMaleMortFld = "AIDSMales"
        '        sFemaleMortFld = "AIDSFemales"
        '    Case "Diarrhea" 'jrs 2009/08/14
        '        sMaleMortFld = "DiarrheaMales"
        '        sFemaleMortFld = "DiarrheaFemales"
        '    Case "Malaria"
        '        sMaleMortFld = "MalariaMales"
        '        sFemaleMortFld = "MalariaFemales"
        '    Case "RespInfec"
        '        sMaleMortFld = "RespInfMales"
        '        sFemaleMortFld = "RespInfFeMales"
        '    Case "MentalHealth"
        '        sMaleMortFld = "MentalHealthMales"
        '        sFemaleMortFld = "MentalHealthFeMales"
        '    Case "TotComDis"
        '        sMaleMortFld = "MortPerMalesComDis"
        '        sFemaleMortFld = "MortPerFeMalesComDis"
        '    Case "TotNonComDis"
        '        sMaleMortFld = "MortPerMalesNonComDis"
        '        sFemaleMortFld = "MortPerFeMalesNonComDis"
        '    Case "TotInjuries"
        '        sMaleMortFld = "MortPerMalesInjuries"
        '        sFemaleMortFld = "MortPerFeMalesInjuries"
        '    Case "Total"
        '        'need to read all 6 fields
        'End Select

        Dim vchosenColumns As String

        Select Case sType
            Case "OthCommumDis"
                sMaleMortFld = "ComDisNoAIDSMales"
                sFemaleMortFld = "ComDisNoAIDSFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,ComDisNoAIDSMales,ComDisNoAIDSFemales"

            Case "MaligNeoPl"
                sMaleMortFld = "MaligNeoPlasMales"
                sFemaleMortFld = "MaligNeoPlasFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,MaligNeoPlasMales,MaligNeoPlasFemales"
            Case "CardioVasc"
                sMaleMortFld = "CardioVascMales"
                sFemaleMortFld = "CardioVascFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,CardioVascMales,CardioVascFemales"

            Case "Digestive"
                sMaleMortFld = "DigestiveMales"
                sFemaleMortFld = "DigestiveFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DigestiveMales,DigestiveFemales"

            Case "Respiratory"
                sMaleMortFld = "RespiratoryMales"
                sFemaleMortFld = "RespiratoryFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,RespiratoryMales,RespiratoryFemales"

            Case "OtherNonComm"
                sMaleMortFld = "OtherG2Males"
                sFemaleMortFld = "OtherG2Females"

                vchosenColumns = "TimeId,RegionId,CohortId,OtherG2Males,OtherG2Females"

            Case "TrafficAcc"
                sMaleMortFld = "RoadTrafficAccMales"
                sFemaleMortFld = "RoadTrafficAccFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,RoadTrafficAccMales,RoadTrafficAccFemales"
            Case "UnIntInj"
                sMaleMortFld = "OtherunintInjMales"
                sFemaleMortFld = "OtherunintInjFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,OtherunintInjMales,OtherunintInjFemales"
            Case "IntInj"
                sMaleMortFld = "IntenInjMales"
                sFemaleMortFld = "IntenInjFemales"


                vchosenColumns = "TimeId,RegionId,CohortId,IntenInjMales,IntenInjFemales"
            Case "Diabetes"
                sMaleMortFld = "DiabetesMales"
                sFemaleMortFld = "DiabetesFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DiabetesMales,DiabetesFemales"
            Case "AIDS"
                sMaleMortFld = "AIDSMales"
                sFemaleMortFld = "AIDSFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,AIDSMales,AIDSFemales"
            Case "Diarrhea" 'jrs 2009/08/14
                sMaleMortFld = "DiarrheaMales"
                sFemaleMortFld = "DiarrheaFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,DiarrheaMales,DiarrheaFemales"
            Case "Malaria"
                sMaleMortFld = "MalariaMales"
                sFemaleMortFld = "MalariaFemales"

                vchosenColumns = "TimeId,RegionId,CohortId,MalariaMales,MalariaFemales"

            Case "RespInfec"
                sMaleMortFld = "RespInfMales"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "RespInfFemales"

                    vchosenColumns = "TimeId,RegionId,CohortId,RespInfMales,RespInfFemales"

            Case "MentalHealth"
                sMaleMortFld = "MentalHealthMales"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MentalHealthFemales"

                    vchosenColumns = "TimeId,RegionId,CohortId,MentalHealthMales,MentalHealthFemales"

            Case "TotComDis"
                sMaleMortFld = "MortPerMalesComDis"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesComDis"

                    vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesComDis,MortPerFemalesComDis"

            Case "TotNonComDis"
                sMaleMortFld = "MortPerMalesNonComDis"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesNonComDis"

                    vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesNonComDis,MortPerFemalesNonComDis"

            Case "TotInjuries"
                sMaleMortFld = "MortPerMalesInjuries"
                    'jrs 2024/02/13 case sensitive, changed from FeMales to Females
                    sFemaleMortFld = "MortPerFemalesInjuries"

                vchosenColumns = "TimeId,RegionId,CohortId,MortPerMalesInjuries,MortPerFemalesInjuries"

            Case "Total"
                'need to read all 6 fields

                vchosenColumns = "*"
        End Select



        ReDim TempPopDatabase(LastReqAge + 1, 2, Session("LastYear") - Session("FirstYear") + 1)
        ReDim TempDatabase(LastReqAge + 1, 2, Session("LastYear") - Session("FirstYear") + 1)

        'take population
        '   Change the data source
        '   ----------------------
        Tindex = 2
        If Not bGrp Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM PopBaseCase Where Region = " &
            Str(iReg) & " and Ncohorts <= " & CStr(LastReqAge) & " and NCohorts >= " & CStr(FirstReqAge) &
            " and Years <= " & CStr(Session("LastYear")))

                'jrs 2009/07/02
                'If tbl2.EOF Then
                '    MsgBox("There's no data in scenario " & _
                '    Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "") & _
                '    " for year " & CStr(iYr), vbCritical, "Load Mortality")
                '    Screen.MousePointer = vbNormal
                '    Exit Sub
                'End If

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    TempPopDatabase(rdr.Item("NCohorts"), 1, rdr.Item("Years") - Session("FirstYear") + 1) = rdr.Item("PopulationMales")
                    TempPopDatabase(rdr.Item("NCohorts"), 2, rdr.Item("Years") - Session("FirstYear") + 1) = rdr.Item("PopulationFemales")
                Loop
                rdr.Close()

            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where RegionId = " &
                                        Str(iReg) & " and CohortId <= " & CStr(LastReqAge) & " and CohortId >= " & CStr(FirstReqAge) &
                                        " and TimeId <= " & CStr(Session("LastYear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())
                    TempPopDatabase(vSQLiteReader("CohortId"), 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = vSQLiteReader("PopulationMales")
                    TempPopDatabase(vSQLiteReader("CohortId"), 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = vSQLiteReader("PopulationFemales")

                End While
            End If
        Else
            ReDim POPMales(NR, NCohorts, Session("LastYear") - Session("FirstYear") + 1)
            ReDim POPFemales(NR, NCohorts, Session("LastYear") - Session("FirstYear") + 1)

            'jrs 2024/05/28
            IfsConn.Open()
            'Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            'Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            'Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            Dim grpDA As OleDb.OleDbDataAdapter
            Dim regDA As OleDb.OleDbDataAdapter
            Dim regIDDA As OleDb.OleDbDataAdapter

            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase Where NCohorts <= " & CStr(LastReqAge) &
            '" and NCohorts >= " & CStr(FirstReqAge) & " and Years <= " & CStr(Session("LastYear")) &
            '" ORDER BY Region, NCohorts, Years", dbM)
            'regDS = New DataSet

            Dim popDA As OleDb.OleDbDataAdapter
            regDS = New DataSet
            'jrs 2024/05/28
            regDS.EnforceConstraints = False

            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
            '" ORDER BY Region, NCohorts", dbM)

            'jrs 2024/05/28
            Dim rdGrpCmd As SQLiteCommand = IfsConn.CreateCommand
            rdGrpCmd.CommandText = "SELECT * FROM " & GroupIDNew
            Dim rdGrp As SQLiteDataReader
            rdGrp = rdGrpCmd.ExecuteReader

            Dim myTbl As DataTable = New DataTable
            myTbl.TableName = "Groups"
            regDS.Tables.Add(myTbl)
            regDS.Tables(0).Load(rdGrp)

            Dim rdRegCmd As SQLiteCommand = IfsConn.CreateCommand
            rdRegCmd.CommandText = "SELECT * FROM " & Grouping
            Dim rdReg As SQLiteDataReader
            rdReg = rdRegCmd.ExecuteReader

            Dim myTbl1 As DataTable = New DataTable
            myTbl1.TableName = "Regions"
            regDS.Tables.Add(myTbl1)
            regDS.Tables(1).Load(rdReg)

            Dim rdRegIdCmd As SQLiteCommand = IfsConn.CreateCommand
            rdRegIdCmd.CommandText = "SELECT * FROM " & RegionIDNew
            Dim rdRegId As SQLiteDataReader
            rdRegId = rdRegIdCmd.ExecuteReader

            Dim myTbl2 As DataTable = New DataTable
            myTbl2.TableName = "RegionIDs"
            regDS.Tables.Add(myTbl2)
            regDS.Tables(2).Load(rdRegId)

            'JAM 2020-05-23
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                popDA = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase Where NCohorts <= " & CStr(LastReqAge) &
                                        " and NCohorts >= " & CStr(FirstReqAge) & " and Years <= " & CStr(Session("LastYear")) &
                                        " ORDER BY Region, NCohorts, Years", dbM)

                popDA.Fill(regDS, "Population")

                grpDA.Fill(regDS, "Groups")
                regDA.Fill(regDS, "Regions")
                regIDDA.Fill(regDS, "RegionIDs")

            Else

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where CohortId <= " & CStr(LastReqAge) &
                                        " and CohortId >= " & CStr(FirstReqAge) & " and TimeId <= " & CStr(Session("LastYear")) &
                                        " ORDER BY RegionId, CohortId, TimeId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite.Load(vSQLiteReader)
                'regDS.Tables.Add(DTsqlite)

                'jrs 2024/05/28
                'dtnew = DTsqlite.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite)

            End If

            'popDA.Fill(regDS, "Population")
            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                 regDS.Tables("Groups").Columns("GroupNum"),
                                                 regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Population").Columns("Region"))

            Dim regPopRel As DataRelation

            'JAM 2020-06-29
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Population").Columns("Region"))

            Else


                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("pop_base_case").Columns(1))


            End If


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("NCohorts"), 1, cRow.Item("Years") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("NCohorts"), 1, cRow.Item("Years") - Session("FirstYear") + 1) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("NCohorts"), 2, cRow.Item("Years") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("NCohorts"), 2, cRow.Item("Years") - Session("FirstYear") + 1) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("Region"), cRow.Item("NCohorts"), cRow.Item("Years") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("Region"), cRow.Item("NCohorts"), cRow.Item("Years") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next
            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("CohortId"), 1, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("CohortId"), 1, cRow.Item("TimeId") - Session("FirstYear") + 1) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("CohortId"), 2, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                TempPopDatabase(cRow.Item("CohortId"), 2, cRow.Item("TimeId") - Session("FirstYear") + 1) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("RegionId"), cRow.Item("CohortId"), cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId"), cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next

            End If

            'jrs 2009/07/02
            'If tbl2.EOF Then
            '    MsgBox("There's no data in scenario " & _
            '    Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "") & _
            '    " for year " & CStr(iYr), vbCritical, "Load Mortality")
            '    Screen.MousePointer = vbNormal
            '    Exit Sub
            'End If
        End If
        'End take population


        '   Change the data source
        '   ----------------------
        If Not bGrp Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM MorDetBaseCase Where Region = " & Str(iReg) & " and NCohorts <= " & CStr(LastReqAge) &
            " and NCohorts >= " & CStr(FirstReqAge) & " and Years <= " & CStr(Session("LastYear")))

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    If Not (sType = "Total") Then
                        'males 
                        TCohort = rdr.Item("NCohorts")
                        TempDatabase(TCohort, 1, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item(sMaleMortFld))

                        'females 
                        TempDatabase(TCohort, 2, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item(sFemaleMortFld))
                    Else
                        'males 
                        TCohort = rdr.Item("NCohorts")
                        TempDatabase(TCohort, 1, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item("MortPerMalesComDis") +
                    rdr.Item("MortPerMalesNonComDis") + rdr.Item("MortPerMalesInjuries"))

                        'females 
                        TempDatabase(TCohort, 2, rdr.Item("Years") - Session("FirstYear") + 1) = AMAX(0, rdr.Item("MortPerFeMalesComDis") +
                    rdr.Item("MortPerFemalesNonComDis") + rdr.Item("MortPerFemalesInjuries"))
                    End If

                    'turn into deaths
                    TempDatabase(TCohort, 1, rdr.Item("Years") - Session("FirstYear") + 1) =
                TempDatabase(TCohort, 1, rdr.Item("Years") - Session("FirstYear") + 1) *
                TempPopDatabase(TCohort, 1, rdr.Item("Years") - Session("FirstYear") + 1)

                    TempDatabase(TCohort, 2, rdr.Item("Years") - Session("FirstYear") + 1) =
                TempDatabase(TCohort, 2, rdr.Item("Years") - Session("FirstYear") + 1) *
                TempPopDatabase(TCohort, 2, rdr.Item("Years") - Session("FirstYear") + 1)

                Loop
                rdr.Close()
            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where RegionId = " & Str(iReg) & " and CohortId <= " & CStr(LastReqAge) &
                        " and CohortId >= " & CStr(FirstReqAge) & " and TimeId <= " & CStr(Session("LastYear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())

                    If Not (sType = "Total") Then
                        'males 
                        TCohort = vSQLiteReader("CohortId")
                        TempDatabase(TCohort, 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader(sMaleMortFld))

                        'females 
                        TempDatabase(TCohort, 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                    Else
                        'males 
                        TCohort = vSQLiteReader("CohortId")
                        TempDatabase(TCohort, 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader("MortPerMalesComDis") +
                        vSQLiteReader("MortPerMalesNonComDis") + vSQLiteReader("MortPerMalesInjuries"))

                        'females 
                        TempDatabase(TCohort, 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) = AMAX(0, vSQLiteReader("MortPerFeMalesComDis") +
                           vSQLiteReader("MortPerFeMalesNonComDis") + vSQLiteReader("MortPerFeMalesInjuries"))
                    End If

                    'turn into deaths
                    TempDatabase(TCohort, 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) =
                    TempDatabase(TCohort, 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1) *
                    TempPopDatabase(TCohort, 1, vSQLiteReader("TimeId") - Session("FirstYear") + 1)

                    TempDatabase(TCohort, 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) =
                    TempDatabase(TCohort, 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1) *
                    TempPopDatabase(TCohort, 2, vSQLiteReader("TimeId") - Session("FirstYear") + 1)

                End While
            End If


        Else

            '    Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase Where NCohorts <= " & CStr(LastReqAge) &
            '" and NCohorts >= " & CStr(FirstReqAge) & " and Years <= " &
            'CStr(Session("LastYear")) & " ORDER BY Region, NCohorts, Years", dbM)
            'mortDA.Fill(regDS, "Mortality")

            Dim mortDA As OleDb.OleDbDataAdapter


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                mortDA = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase Where NCohorts <= " & CStr(LastReqAge) &
                                            " and NCohorts >= " & CStr(FirstReqAge) & " and Years <= " &
                                                CStr(Session("LastYear")) & " ORDER BY Region, NCohorts, Years", dbM)
                mortDA.Fill(regDS, "Mortality")
            Else

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where CohortId <= " & CStr(LastReqAge) &
                                        " and CohortId >= " & CStr(FirstReqAge) & " and TimeId <= " &
                                        CStr(Session("LastYear")) & " ORDER BY RegionId, CohortId, TimeId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite2.Load(vSQLiteReader)

                'jrs 2024/05/28
                'dtnew = DTsqlite2.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite2.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite2)
            End If




            'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Mortality").Columns("Region"))


            Dim regMortRel As DataRelation


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regMortRel = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Mortality").Columns("Region"))
            Else


                regMortRel = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("Mor_Det_Base_Case").Columns("RegionId"))


            End If

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("NCohorts")
                                    rId = cRow.Item("Region")

                                    If Not (sType = "Total") Then
                                        TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)
                                    Else
                                        TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 1, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                    cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("Years") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerFemalesComDis") + cRow.Item("MortPerFemalesNonComDis") +
                                    cRow.Item("MortPerFemalesInjuries")) * POPFemales(rId, TCohort, cRow.Item("Years") - Session("FirstYear") + 1)
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next
            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("CohortId")
                                    rId = cRow.Item("RegionId")

                                    If Not (sType = "Total") Then
                                        TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)
                                    Else
                                        TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 1, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                    cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)

                                        TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) =
                                    TempDatabase(TCohort, 2, cRow.Item("TimeId") - Session("FirstYear") + 1) +
                                    (cRow.Item("MortPerFeMalesComDis") + cRow.Item("MortPerFeMalesNonComDis") +
                                    cRow.Item("MortPerFeMalesInjuries")) * POPFemales(rId, TCohort, cRow.Item("TimeId") - Session("FirstYear") + 1)
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next
            End If


            IfsConn.Close()

        End If


        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM.Close()
        End If

        'jrs 2007/03/05
        'Population Weigthed average
        For iYr = 1 To Session("LastYear") - Session("FirstYear") + 1
            For TCohort = FirstReqAge To LastReqAge
                'Add all Deaths across age categories
                '-------------------------------------
                'Males
                TempDatabase(LastReqAge + 1, 1, iYr) = TempDatabase(LastReqAge + 1, 1, iYr) + TempDatabase(TCohort, 1, iYr)
                'Females
                TempDatabase(LastReqAge + 1, 2, iYr) = TempDatabase(LastReqAge + 1, 2, iYr) + TempDatabase(TCohort, 2, iYr)
                'Add all Population across age categories
                '-----------------------------------------
                'Males
                TempPopDatabase(LastReqAge + 1, 1, iYr) = TempPopDatabase(LastReqAge + 1, 1, iYr) + TempPopDatabase(TCohort, 1, iYr)
                'Females
                TempPopDatabase(LastReqAge + 1, 2, iYr) = TempPopDatabase(LastReqAge + 1, 2, iYr) + TempPopDatabase(TCohort, 2, iYr)
            Next TCohort

            'jrs 2009/08/07
            If Not Session("bUseDeaths") Then
                'jrs 2009/07/02 handling empty years
                If TempPopDatabase(LastReqAge + 1, 1, iYr) > 0 Then
                    'Compute the rate
                    '-----------------
                    TempDatabase(LastReqAge + 1, 1, iYr) = TempDatabase(LastReqAge + 1, 1, iYr) / TempPopDatabase(LastReqAge + 1, 1, iYr)

                    TempDatabase(LastReqAge + 1, 2, iYr) = TempDatabase(LastReqAge + 1, 2, iYr) / TempPopDatabase(LastReqAge + 1, 2, iYr)
                End If
            End If
        Next iYr

        'Load Final Array
        For iYr = 1 To Session("LastYear") - Session("FirstYear") + 1
            'jrs 2009/07/02 handling empty years
            If TempPopDatabase(LastReqAge + 1, 1, iYr) > 0 Then
                'jrs 2010/04/29 changin units to deaths/1000
                If sGender = "Male" Then
                    Session("mPLOT")(iYr, TotalGraphs) = TempDatabase(LastReqAge + 1, 1, iYr)
                ElseIf sGender = "Female" Then
                    Session("mPLOT")(iYr, TotalGraphs) = TempDatabase(LastReqAge + 1, 2, iYr)
                Else
                    If Not Session("bUseDeaths") Then
                        Session("mPLOT")(iYr, TotalGraphs) = (TempDatabase(LastReqAge + 1, 1, iYr) *
                        TempPopDatabase(LastReqAge + 1, 1, iYr) +
                        TempDatabase(LastReqAge + 1, 2, iYr) *
                        TempPopDatabase(LastReqAge + 1, 2, iYr)) /
                        (TempPopDatabase(LastReqAge + 1, 1, iYr) + TempPopDatabase(LastReqAge + 1, 2, iYr))
                    Else
                        Session("mPLOT")(iYr, TotalGraphs) = TempDatabase(LastReqAge + 1, 1, iYr) +
                        TempDatabase(LastReqAge + 1, 2, iYr)
                    End If
                End If
            Else
                Session("mPLOT")(iYr, TotalGraphs) = DBNull.Value
            End If
        Next iYr

        'jrs 2010/02/09
        Session("mLegends")(0, TotalGraphs) = sReg
        'If bGrp And mnulblAlias.Checked Then
        '    'Read and load all aliases
        '    Dim db As Database
        '    Dim Tbl As Recordset
        '    db = OpenDatabase(IFs_MDB2)
        '    Tbl = db.OpenRecordset(GroupIDNew)
        '    Tbl.Index = "GroupName"
        '    Tbl.Seek("=", sReg)
        '    If Not Tbl.NoMatch Then
        '        mLegends(1, TotalGraphs) = Tbl.Fields("Alias")
        '    End If
        '    Tbl.Close()
        '    db.Close()
        'Else
        Session("mLegends")(1, TotalGraphs) = sReg
        'End If


        If sType <> "Total" Then
            Session("mLegends")(2, TotalGraphs) = sType
        Else
            Session("mLegends")(2, TotalGraphs) = "All Types"
        End If
        If sGender <> "Total" Then
            Session("mLegends")(3, TotalGraphs) = sGender
        Else
            Session("mLegends")(3, TotalGraphs) = "All Genders"
        End If
        'jrs 2024/05/28 added - 1, since FirstReqAge = Session("FirstAge") + 1
        Session("mLegends")(4, TotalGraphs) = "Adults " & CStr((FirstReqAge - 1) * 5) & " to " & CStr(LastReqAge * 5 - 1)
        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then
            Session("mLegends")(5, TotalGraphs) = "Working"
        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            Session("mLegends")(5, TotalGraphs) = "Base"
        Else
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "")

            Else
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".db", "")
            End If
        End If

        'jrs 2009/07/02
        Session("bSuccess") = True

    End Sub

    Private Sub DrawGraph(ByVal TotalGraphs As Integer)
        lblJS.Visible = True
        PackJScritpBuilder()
    End Sub


    Protected Sub cmdTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTable.Click
        If Not lblJS.Visible Then
            LoadSet()
        Else
            lblJS.Visible = False
        End If
        pnlMort.Visible = False
        If Session("TotalGraphs") > 0 Then
            LoadGrid()
            cmdTable.Enabled = False
            'jrs 2010/03/02 Comparison
            'If Not bComparisonOn Then
            '    ReDim CompBackup(UBound(mPLOT, 1), TotalGraphs)
            '    ReDim CompLabelBackup(5, TotalGraphs)
            '    mnuComp.Visible = True
            'End If
        End If
        cmdGraph.Enabled = True
    End Sub

    Private Sub LoadGrid()
        If Session("GraphType") = "J" Then
            LoadGridJ()
        ElseIf Session("GraphType") = "LE" Then
            LoadGridLE()
        Else
            LoadGridInf()
        End If
    End Sub

    Private Sub LoadGridJ()
        Dim j As Integer, i As Integer, r(5) As TableRow, ce As TableCell
        'Load Legends
        For j = 1 To Session("TotalGraphs")
            For i = 1 To 5
                If j = 1 Then
                    r(i - 1) = New TableRow
                    ce = New TableCell
                    ce.Text = ""
                    ce.BackColor = System.Drawing.Color.Gray
                    r(i - 1).Cells.Add(ce)
                End If
                ce = New TableCell
                ce.Text = Session("mLegends")(i, j)
                ce.BackColor = System.Drawing.Color.Gray
                r(i - 1).Cells.Add(ce)
            Next i
            '.row = 3
            '.CellAlignment = flexAlignLeftCenter
        Next j

        With grdMort
            '   Fill in top of table
            '   --------------------
            .Rows.Clear()
            For i = 1 To 5
                .Rows.Add(r(i - 1))
            Next

            Dim iNCohorts As Integer

            'Load Ages
            '---------
            If Not Session("bUseGBDage") Then
                ReDim Preserve r(NCohorts + 5)
                r(5) = New TableRow
                ce = New TableCell
                ce.Text = "Infants"
                ce.BackColor = System.Drawing.Color.Gray
                r(5).Cells.Add(ce)

                r(6) = New TableRow
                ce = New TableCell
                ce.Text = " 1- 4 "
                ce.BackColor = System.Drawing.Color.Gray
                r(6).Cells.Add(ce)
                For i = 2 To NCohorts - 1
                    r(i + 5) = New TableRow
                    ce = New TableCell
                    ce.Text = Str(i * 5 - 5) & "-" & Str(i * 5 - 1)
                    ce.BackColor = System.Drawing.Color.Gray
                    r(i + 5).Cells.Add(ce)
                Next i
                r(NCohorts + 5) = New TableRow
                ce = New TableCell
                ce.Text = CStr((NCohorts - 1) * 5) & "+" 'jrs 2026/01/14 "100+"
                ce.BackColor = System.Drawing.Color.Gray
                r(NCohorts + 5).Cells.Add(ce)
                iNCohorts = NCohorts
            Else
                ReDim Preserve r(NCohorts - 14 + 5)
                r(5) = New TableRow
                ce = New TableCell
                ce.Text = "0-4"
                ce.BackColor = System.Drawing.Color.Gray
                r(5).Cells.Add(ce)
                r(6) = New TableRow
                ce = New TableCell
                ce.Text = "5-14"
                ce.BackColor = System.Drawing.Color.Gray
                r(6).Cells.Add(ce)
                r(7) = New TableRow
                ce = New TableCell
                ce.Text = "15-29"
                ce.BackColor = System.Drawing.Color.Gray
                r(7).Cells.Add(ce)
                r(8) = New TableRow
                ce = New TableCell
                ce.Text = "30-44"
                ce.BackColor = System.Drawing.Color.Gray
                r(8).Cells.Add(ce)
                r(9) = New TableRow
                ce = New TableCell
                ce.Text = "45-59"
                ce.BackColor = System.Drawing.Color.Gray
                r(9).Cells.Add(ce)
                r(10) = New TableRow
                ce = New TableCell
                ce.Text = "60-69"
                ce.BackColor = System.Drawing.Color.Gray
                r(10).Cells.Add(ce)
                r(11) = New TableRow
                ce = New TableCell
                ce.Text = "70-79"
                ce.BackColor = System.Drawing.Color.Gray
                r(11).Cells.Add(ce)
                r(12) = New TableRow
                ce = New TableCell
                ce.Text = "80+"
                ce.BackColor = System.Drawing.Color.Gray
                r(12).Cells.Add(ce)

                iNCohorts = NCohorts - 14
            End If

            'Load Mortality Values
            For j = 1 To Session("TotalGraphs")
                For i = 0 To iNCohorts
                    ce = New TableCell
                    If (i = 0 And Not Session("bUseInfants")) Or
                    (i = iNCohorts And Not Session("bUse100")) Then
                        ce.Text = ""
                    Else
                        ce.Text = Format(Session("mPLOT")(i, j), "0.0000")
                        ce.HorizontalAlign = HorizontalAlign.Right
                    End If
                    r(i + 5).Cells.Add(ce)
                Next i
            Next j

            For i = 6 To iNCohorts + 6
                .Rows.Add(r(i - 1))
            Next

            .Visible = True
        End With
    End Sub

    Private Sub LoadGridInf()
        Dim j As Integer, i As Integer, r(4) As TableRow, ce As TableCell
        'Load Legends
        For j = 1 To Session("TotalGraphs")
            For i = 1 To 5
                If i <> 4 Then
                    If j = 1 Then
                        If i < 5 Then
                            r(i - 1) = New TableRow
                        Else
                            r(3) = New TableRow
                        End If
                        ce = New TableCell
                        ce.Text = ""
                        ce.BackColor = System.Drawing.Color.Gray
                        If i < 5 Then
                            r(i - 1).Cells.Add(ce)
                        Else
                            r(3).Cells.Add(ce)
                        End If
                    End If
                    ce = New TableCell
                    ce.Text = Session("mLegends")(i, j)
                    ce.BackColor = System.Drawing.Color.Gray
                    If i < 5 Then
                        r(i - 1).Cells.Add(ce)
                    Else
                        r(3).Cells.Add(ce)
                    End If
                End If
            Next i
            '.row = 3
            '.CellAlignment = flexAlignLeftCenter
        Next j

        With grdMort
            '   Fill in top of table
            '   --------------------
            .Rows.Clear()
            For i = 1 To 4
                .Rows.Add(r(i - 1))
            Next

            'Load Years
            '---------
            ReDim Preserve r(Session("LastYear") - Session("FirstYear") + 1 + 4)
            For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                r(i + 3) = New TableRow
                ce = New TableCell
                ce.Text = Session("FirstYear") + i - 1
                ce.BackColor = System.Drawing.Color.Gray
                r(i + 3).Cells.Add(ce)
            Next i

            'Load Mortality Values
            For j = 1 To Session("TotalGraphs")
                For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                    ce = New TableCell
                    ce.Text = Format(Session("mPLOT")(i, j), "0.0000")
                    ce.HorizontalAlign = HorizontalAlign.Right
                    r(i + 3).Cells.Add(ce)
                Next i
            Next j

            For i = 5 To Session("LastYear") - Session("FirstYear") + 1 + 4
                .Rows.Add(r(i - 1))
            Next

            .Visible = True
        End With
    End Sub

    'jrs 2016/06/22
    Private Sub LoadGridLE()
        Dim j As Integer, i As Integer, r(5) As TableRow, ce As TableCell
        'Load Legends
        For j = 1 To Session("TotalGraphs")
            For i = 1 To 5
                If j = 1 Then
                    r(i - 1) = New TableRow
                    ce = New TableCell
                    ce.Text = ""
                    ce.BackColor = System.Drawing.Color.Gray
                    r(i - 1).Cells.Add(ce)
                End If
                ce = New TableCell
                ce.Text = Session("mLegends")(i, j)
                ce.BackColor = System.Drawing.Color.Gray
                r(i - 1).Cells.Add(ce)
            Next i
            '.row = 3
            '.CellAlignment = flexAlignLeftCenter
        Next j

        With grdMort
            '   Fill in top of table
            '   --------------------
            .Rows.Clear()
            For i = 1 To 5
                .Rows.Add(r(i - 1))
            Next

            Dim iNCohorts As Integer

            'Load Ages
            '---------
            ReDim Preserve r(NCohorts + 5)
            r(5) = New TableRow
            ce = New TableCell
            ce.Text = "0"
            ce.BackColor = System.Drawing.Color.Gray
            r(5).Cells.Add(ce)

            r(6) = New TableRow
            ce = New TableCell
            ce.Text = "1"
            ce.BackColor = System.Drawing.Color.Gray
            r(6).Cells.Add(ce)
            For i = 2 To NCohorts
                r(i + 5) = New TableRow
                ce = New TableCell
                ce.Text = Str(i * 5 - 5)
                ce.BackColor = System.Drawing.Color.Gray
                r(i + 5).Cells.Add(ce)
            Next i
            r(NCohorts + 5) = New TableRow
            ce = New TableCell
            ce.Text = CStr((NCohorts - 1) * 5) & "+"
            ce.BackColor = System.Drawing.Color.Gray
            r(NCohorts + 5).Cells.Add(ce)
            iNCohorts = NCohorts

            'Load LE Values
            For j = 1 To Session("TotalGraphs")
                For i = 0 To iNCohorts
                    ce = New TableCell
                    If Session("mPLOT")(i, j) > -9999 Then
                        ce.Text = Format(Session("mPLOT")(i, j), "0.0000")
                        ce.HorizontalAlign = HorizontalAlign.Right
                    Else
                        ce.Text = ""
                    End If
                    r(i + 5).Cells.Add(ce)
                Next i
            Next j

            For i = 6 To iNCohorts + 6
                .Rows.Add(r(i - 1))
            Next

            .Visible = True
        End With
    End Sub

    Private Sub mnuReg_Click()
        Session("bUseReg") = True
        Session("bUseGroup") = False
        Session("bUseGroupDec") = False
        Session("bUseGList") = False

        wmMain.Groups("mnuGroups").Items("mnuReg").IconImage = "checkbox.gif"
        wmMain.Groups("mnuGroups").Items("mnuGroup").IconImage = "blank.gif"
        wmMain.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "blank.gif"
        wmMain.Groups("mnuGroups").Items("mnuGList").IconImage = "blank.gif"

        LoadDim1s()
    End Sub

    Private Sub mnuGroup_Click()
        Dim bSkip As Boolean
        bSkip = Session("bUseGroupDec")

        Session("bUseReg") = False
        Session("bUseGroup") = True
        Session("bUseGroupDec") = False
        Session("bUseGList") = False

        wmMain.Groups("mnuGroups").Items("mnuReg").IconImage = "blank.gif"
        wmMain.Groups("mnuGroups").Items("mnuGroup").IconImage = "checkbox.gif"
        wmMain.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "blank.gif"
        wmMain.Groups("mnuGroups").Items("mnuGList").IconImage = "blank.gif"

        If Not bSkip Then
            LoadDim1s()
        End If
    End Sub

    Private Sub mnuGroupDec_Click()
        Dim bSkip As Boolean
        bSkip = Session("bUseGroup")

        Session("bUseReg") = False
        Session("bUseGroup") = False
        Session("bUseGroupDec") = True
        Session("bUseGList") = False

        wmMain.Groups("mnuGroups").Items("mnuReg").IconImage = "blank.gif"
        wmMain.Groups("mnuGroups").Items("mnuGroup").IconImage = "blank.gif"
        wmMain.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "checkbox.gif"
        wmMain.Groups("mnuGroups").Items("mnuGList").IconImage = "blank.gif"

        If Not bSkip Then
            LoadDim1s()
        End If
    End Sub

    Private Sub mnuGList_Click()
        Session("bUseReg") = False
        Session("bUseGroup") = False
        Session("bUseGroupDec") = False
        Session("bUseGList") = True

        wmMain.Groups("mnuGroups").Items("mnuReg").IconImage = "blank.gif"
        wmMain.Groups("mnuGroups").Items("mnuGroup").IconImage = "blank.gif"
        wmMain.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "blank.gif"
        wmMain.Groups("mnuGroups").Items("mnuGList").IconImage = "checkbox.gif"

        LoadDim1s()
    End Sub

    Private Sub mnu100_Click()
        Session("bUse100") = Not Session("bUse100")
        If Session("bUse100") Then
            wmMain.Groups("mnuAge").Items("mnu100").IconImage = "checkbox.gif"
        Else
            wmMain.Groups("mnuAge").Items("mnu100").IconImage = "blank.gif"
        End If

        If lblJS.Visible Then
            DrawGraph(Session("TotalGraphs"))
        End If
    End Sub

    Private Sub mnuInfants_Click()
        Session("bUseInfants") = Not Session("bUseInfants")
        If Session("bUseInfants") Then
            wmMain.Groups("mnuAge").Items("mnuInfants").IconImage = "checkbox.gif"
        Else
            wmMain.Groups("mnuAge").Items("mnuInfants").IconImage = "blank.gif"
        End If

        If lblJS.Visible Then
            DrawGraph(Session("TotalGraphs"))
        End If
    End Sub

    Private Sub mnuAdults_Click()
        Session("bUseAdltsOnly") = Not Session("bUseAdltsOnly")
        If Session("bUseAdltsOnly") Then
            wmMain.Groups("mnuAge").Items("mnuAdltsOnly").IconImage = "checkbox.gif"
            Session("bUseInfants") = False
            wmMain.Groups("mnuAge").Items("mnuInfants").IconImage = "blank.gif"
            wmMain.Groups("mnuAge").Items("mnuInfants").Enabled = False
        Else
            wmMain.Groups("mnuAge").Items("mnuAdltsOnly").IconImage = "blank.gif"
            wmMain.Groups("mnuAge").Items("mnuInfants").Enabled = True
            Session("bUseInfants") = True
            wmMain.Groups("mnuAge").Items("mnuInfants").IconImage = "checkbox.gif"
        End If
        If lblJS.Visible Then
            DrawGraph(Session("TotalGraphs"))
        End If

    End Sub

    Private Sub mnuGBDage_Click()
        Session("bUseGBDage") = Not Session("bUseGBDage")
        If Session("bUseGBDage") Then
            wmMain.Groups("mnuAge").Items("mnuGBDage").IconImage = "checkbox.gif"
            wmMain.Groups("mnuAge").Items("mnu100").Caption = "Include 80+"
        Else
            wmMain.Groups("mnuAge").Items("mnuGBDage").IconImage = "blank.gif"
            wmMain.Groups("mnuAge").Items("mnu100").Caption = "Include oldest age group" 'jrs 2026/01/14 100+"
        End If

        'jrs 2009/06/23
        If grdMort.Visible Then
            cmdClear_Click(Me, New EventArgs)
            cmdTable_Click(Me, New EventArgs)
        ElseIf lblJS.Visible Then
            cmdClear_Click(Me, New EventArgs)
            cmdGraph_Click(Me, New EventArgs)
        End If
    End Sub

    Private Sub AggGBDage(ByRef iNCohorts As Integer, ByVal iTypes As Integer,
    ByRef TempDatabase As Object, ByRef TempPopDatabase As Object)
        'iTypes is the number of types already loaded in the temp array
        Dim T As Integer, POPval As Single
        Dim j As Integer, g As Integer

        iNCohorts = iNCohorts - 14
        'Group Results
        '-------------
        Dim GBDage(0, 0) As Single
        ReDim GBDage(7, 2 * iTypes)
        Dim GBDPOP(7, 2) As Single
        'GBD 0 to 4, IFs infants + 1 to 4
        '--------------------------------
        'Males
        GBDage(0, 1) = (TempDatabase(0, 1) * TempPopDatabase(0, 1) + TempDatabase(1, 1) * TempPopDatabase(1, 1)) / (TempPopDatabase(0, 1) + TempPopDatabase(1, 1))
        GBDPOP(0, 1) = TempPopDatabase(0, 1) + TempPopDatabase(1, 1)

        'Females
        GBDage(0, 2) = (TempDatabase(0, 2) * TempPopDatabase(0, 2) + TempDatabase(1, 2) * TempPopDatabase(1, 2)) / (TempPopDatabase(0, 2) + TempPopDatabase(1, 2))
        GBDPOP(0, 2) = TempPopDatabase(0, 2) + TempPopDatabase(1, 2)

        'GBD 5 to 14, IFs 5 to 9 and 10 to 14
        '------------------------------------
        'Males
        GBDage(1, 1) = (TempDatabase(2, 1) * TempPopDatabase(2, 1) + TempDatabase(3, 1) * TempPopDatabase(3, 1)) / (TempPopDatabase(2, 1) + TempPopDatabase(3, 1))
        GBDPOP(1, 1) = TempPopDatabase(2, 1) + TempPopDatabase(3, 1)

        'Females
        GBDage(1, 2) = (TempDatabase(2, 2) * TempPopDatabase(2, 2) + TempDatabase(3, 2) * TempPopDatabase(3, 2)) / (TempPopDatabase(2, 2) + TempPopDatabase(3, 2))
        GBDPOP(1, 2) = TempPopDatabase(2, 2) + TempPopDatabase(3, 2)

        'GBD 15 to 29, IFs 15 to 19, 20 to 24 and 25 to 29
        '-------------------------------------------------
        'Males
        GBDage(2, 1) = (TempDatabase(4, 1) * TempPopDatabase(4, 1) + TempDatabase(5, 1) * TempPopDatabase(5, 1) + TempDatabase(6, 1) * TempPopDatabase(6, 1)) / (TempPopDatabase(4, 1) + TempPopDatabase(5, 1) + TempPopDatabase(6, 1))
        GBDPOP(2, 1) = TempPopDatabase(4, 1) + TempPopDatabase(5, 1) + TempPopDatabase(6, 1)

        'Females
        GBDage(2, 2) = (TempDatabase(4, 2) * TempPopDatabase(4, 2) + TempDatabase(5, 2) * TempPopDatabase(5, 2) + TempDatabase(6, 2) * TempPopDatabase(6, 2)) / (TempPopDatabase(4, 2) + TempPopDatabase(5, 2) + TempPopDatabase(6, 2))
        GBDPOP(2, 2) = TempPopDatabase(4, 2) + TempPopDatabase(5, 2) + TempPopDatabase(6, 2)

        'GBD 30 to 44, IFs 30 to 34, 35 to 39 and 40 to 44
        '-------------------------------------------------
        'Males
        GBDage(3, 1) = (TempDatabase(7, 1) * TempPopDatabase(7, 1) + TempDatabase(8, 1) * TempPopDatabase(8, 1) + TempDatabase(9, 1) * TempPopDatabase(9, 1)) / (TempPopDatabase(7, 1) + TempPopDatabase(8, 1) + TempPopDatabase(9, 1))
        GBDPOP(3, 1) = TempPopDatabase(7, 1) + TempPopDatabase(8, 1) + TempPopDatabase(9, 1)

        'Females
        GBDage(3, 2) = (TempDatabase(7, 2) * TempPopDatabase(7, 2) + TempDatabase(8, 2) * TempPopDatabase(8, 2) + TempDatabase(9, 2) * TempPopDatabase(9, 2)) / (TempPopDatabase(7, 2) + TempPopDatabase(8, 2) + TempPopDatabase(9, 2))
        GBDPOP(3, 2) = TempPopDatabase(7, 2) + TempPopDatabase(8, 2) + TempPopDatabase(9, 2)

        'GBD 45 to 59, IFs 45 to 49, 50 to 54 and 55 to 59
        '-------------------------------------------------
        'Males
        GBDage(4, 1) = (TempDatabase(10, 1) * TempPopDatabase(10, 1) + TempDatabase(11, 1) * TempPopDatabase(11, 1) + TempDatabase(12, 1) * TempPopDatabase(12, 1)) / (TempPopDatabase(10, 1) + TempPopDatabase(11, 1) + TempPopDatabase(12, 1))
        GBDPOP(4, 1) = TempPopDatabase(10, 1) + TempPopDatabase(11, 1) + TempPopDatabase(12, 1)

        'Females
        GBDage(4, 2) = (TempDatabase(10, 2) * TempPopDatabase(10, 2) + TempDatabase(11, 2) * TempPopDatabase(11, 2) + TempDatabase(12, 2) * TempPopDatabase(12, 2)) / (TempPopDatabase(10, 2) + TempPopDatabase(11, 2) + TempPopDatabase(12, 2))
        GBDPOP(4, 2) = TempPopDatabase(10, 2) + TempPopDatabase(11, 2) + TempPopDatabase(12, 2)

        'GBD 60 to 69, IFs 60 to 64, 65 to 69
        '------------------------------------
        'Males
        GBDage(5, 1) = (TempDatabase(13, 1) * TempPopDatabase(13, 1) + TempDatabase(14, 1) * TempPopDatabase(14, 1)) / (TempPopDatabase(13, 1) + TempPopDatabase(14, 1))
        GBDPOP(5, 1) = TempPopDatabase(13, 1) + TempPopDatabase(14, 1)

        'Females
        GBDage(5, 2) = (TempDatabase(13, 2) * TempPopDatabase(13, 2) + TempDatabase(14, 2) * TempPopDatabase(14, 2)) / (TempPopDatabase(13, 2) + TempPopDatabase(14, 2))
        GBDPOP(5, 2) = TempPopDatabase(13, 2) + TempPopDatabase(14, 2)

        'GBD 70 to 79, IFs 70 to 74, 75 to 79
        '------------------------------------
        'Males
        GBDage(6, 1) = (TempDatabase(15, 1) * TempPopDatabase(15, 1) + TempDatabase(16, 1) * TempPopDatabase(16, 1)) / (TempPopDatabase(15, 1) + TempPopDatabase(16, 1))
        GBDPOP(6, 1) = TempPopDatabase(15, 1) + TempPopDatabase(16, 1)

        'Females
        GBDage(6, 2) = (TempDatabase(15, 2) * TempPopDatabase(15, 2) + TempDatabase(16, 2) * TempPopDatabase(16, 2)) / (TempPopDatabase(15, 2) + TempPopDatabase(16, 2))
        GBDPOP(6, 2) = TempPopDatabase(15, 2) + TempPopDatabase(16, 2)

        'GBD 80+, IFs 80 to 84, 85 to 89, 90 to 94, 95 to 99 and 100+
        '------------------------------------------------------------
        'All types
        For T = 1 To 2
            GBDage(7, T) = 0
            POPval = 0
            'Gender
            g = T
            'Age
            For j = 17 To NCohorts
                GBDage(7, T) = GBDage(7, T) + TempDatabase(j, T) * TempPopDatabase(j, g)
                POPval = POPval + TempPopDatabase(j, g)
            Next j
            GBDage(7, T) = GBDage(7, T) / POPval
            If T = 1 Then
                GBDPOP(7, 1) = POPval
            ElseIf T = 2 Then
                GBDPOP(7, 2) = POPval
            End If
        Next T

        'jrs 2009/07/17
        'Save new borns to avoid double counting in presenting population numbers
        'for the comparison report
        TempPopDatabase(8, 1) = TempPopDatabase(0, 1)
        TempPopDatabase(8, 2) = TempPopDatabase(0, 2)

        'Reload to target
        For j = 0 To 7
            'Mortality
            For T = 1 To 2
                TempDatabase(j, T) = GBDage(j, T)
            Next T
            'Population (for totals in grid)
            For T = 1 To 2
                TempPopDatabase(j, T) = GBDPOP(j, T)
            Next T
        Next j
    End Sub

    Private Sub mnuDispJ_Click()
        If Session("GraphType") <> "J" Then
            Session("GraphType") = "J"
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Mortality Display: J - Curve (using Deaths per Thousand)"
            wmMain.Groups("mnuDispType").Items("mnuJCurve").IconImage = "checkbox.gif"
            wmMain.Groups("mnuDispType").Items("mnuChldProb").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuAdltProb").IconImage = "blank.gif"
            'mnuDeaths.Enabled = True
            wmMain.Groups("mnuDispType").Items("mnuChldRate").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuAdltRate").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuDispLE").IconImage = "blank.gif"

            ReDim Session("mPLOT")(NCohorts + 1, MaxVariables)
            lblYears.Visible = True
            lstYears.Visible = True

            If grdMort.Visible Then
                cmdClear_Click(Me, New EventArgs)
                cmdTable_Click(Me, New EventArgs)
            ElseIf lblJS.Visible Then
                cmdClear_Click(Me, New EventArgs)
                cmdGraph_Click(Me, New EventArgs)
            End If

        End If

    End Sub

    Private Sub mnuDispInfant_Click()
        If Session("GraphType") <> "ChldProb" Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Mortality Display: J - Curve (using Probability of Death per 1000)"
            Session("GraphType") = "ChldProb"
            wmMain.Groups("mnuDispType").Items("mnuJCurve").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuChldProb").IconImage = "checkbox.gif"
            wmMain.Groups("mnuDispType").Items("mnuAdltProb").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuChldRate").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuAdltRate").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuDispLE").IconImage = "blank.gif"

            'mnuDeaths.Enabled = False

            ReDim Session("mPLOT")(Session("LastYear") - Session("FirstYear") + 1, MaxVariables)
            lblYears.Visible = False
            lstYears.Visible = False

            If grdMort.Visible Then
                cmdClear_Click(Me, New EventArgs)
                cmdTable_Click(Me, New EventArgs)
            ElseIf lblJS.Visible Then
                cmdClear_Click(Me, New EventArgs)
                cmdGraph_Click(Me, New EventArgs)
            End If

        End If

    End Sub

    Private Sub mnuDispChildRate_Click()
        If Session("GraphType") <> "ChldRate" Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Mortality Display: J - Curve (using Deaths per Thousand)"
            Session("GraphType") = "ChldRate"
            wmMain.Groups("mnuDispType").Items("mnuJCurve").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuChldProb").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuAdltProb").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuChldRate").IconImage = "checkbox.gif"
            wmMain.Groups("mnuDispType").Items("mnuAdltRate").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuDispLE").IconImage = "blank.gif"

            'mnuDeaths.Enabled = True

            ReDim Session("mPLOT")(Session("LastYear") - Session("FirstYear") + 1, MaxVariables)
            lblYears.Visible = False
            lstYears.Visible = False

            If grdMort.Visible Then
                cmdClear_Click(Me, New EventArgs)
                cmdTable_Click(Me, New EventArgs)
            ElseIf lblJS.Visible Then
                cmdClear_Click(Me, New EventArgs)
                cmdGraph_Click(Me, New EventArgs)
            End If

        End If

    End Sub

    Private Sub mnuDispAdults_Click()
        If Session("GraphType") <> "AdltProb" Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Mortality Display: J - Curve (using Probability of Deaths per 1000)"
            Session("GraphType") = "AdltProb"
            wmMain.Groups("mnuDispType").Items("mnuJCurve").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuChldProb").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuAdltProb").IconImage = "checkbox.gif"
            wmMain.Groups("mnuDispType").Items("mnuChldRate").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuAdltRate").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuDispLE").IconImage = "blank.gif"

            'mnuDeaths.Enabled = False

            ReDim Session("mPLOT")(Session("LastYear") - Session("FirstYear") + 1, MaxVariables)
            lblYears.Visible = False
            lstYears.Visible = False

            'jrs 2009/06/23
            If grdMort.Visible Then
                cmdClear_Click(Me, New EventArgs)
                cmdTable_Click(Me, New EventArgs)
            ElseIf lblJS.Visible Then
                cmdClear_Click(Me, New EventArgs)
                cmdGraph_Click(Me, New EventArgs)
            End If

        End If

    End Sub

    Private Sub mnuDispAdultsRate_Click()
        If Session("GraphType") <> "AdltRate" Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Mortality Display: J - Curve (using Deaths per Thousand)"
            Session("GraphType") = "AdltRate"
            wmMain.Groups("mnuDispType").Items("mnuJCurve").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuChldProb").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuAdltProb").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuChldRate").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuAdltRate").IconImage = "checkbox.gif"
            wmMain.Groups("mnuDispType").Items("mnuDispLE").IconImage = "blank.gif"

            'mnuDeaths.Enabled = True

            ReDim Session("mPLOT")(Session("LastYear") - Session("FirstYear") + 1, MaxVariables)
            lblYears.Visible = False
            lstYears.Visible = False

            If grdMort.Visible Then
                cmdClear_Click(Me, New EventArgs)
                cmdTable_Click(Me, New EventArgs)
            ElseIf lblJS.Visible Then
                cmdClear_Click(Me, New EventArgs)
                cmdGraph_Click(Me, New EventArgs)
            End If
        End If
    End Sub

    Private Sub mnuDefineAdults_Click()
        pnlMort.Visible = False
        pnlButtons.Visible = False
        grdMort.Visible = False
        lblJS.Visible = False

        txtDef1.Text = Session("FirstAge") * 5
        txtDef2.Text = Amin(Session("LastAge") * 5 - 1, 100)
        lblMsg.Visible = False
        pnlAdltDef.Visible = True
    End Sub

    'jrs 2016/06/22
    Private Sub mnuDispLE_Click()
        If Session("GraphType") <> "LE" Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Mortality Display: Life Expectancy at Age X"
            Session("GraphType") = "LE"
            wmMain.Groups("mnuDispType").Items("mnuJCurve").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuChldProb").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuAdltProb").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuChldRate").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuAdltRate").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuDispLE").IconImage = "checkbox.gif"

            'mnuDeaths.Enabled = True

            ReDim Session("mPLOT")(NCohorts + 1, MaxVariables)
            lblYears.Visible = True
            lstYears.Visible = True

            lstDim2.SelectedIndex = lstDim2.Items.Count - 1
            lstDim2.Enabled = False

            If grdMort.Visible Then
                cmdClear_Click(Me, New EventArgs)
                cmdTable_Click(Me, New EventArgs)
            ElseIf lblJS.Visible Then
                cmdClear_Click(Me, New EventArgs)
                cmdGraph_Click(Me, New EventArgs)
            End If
        End If
    End Sub

    Protected Sub cmdSaveAdltDef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveAdltDef.Click
        Dim sVal As String, ageVal1 As Single, ageVal2 As Single

        sVal = txtDef1.Text
        If IsNumeric(sVal) Then
            If Val(sVal) Mod 5 = 0 And Val(sVal) > 0 And Val(sVal) <= 100 Then
                ageVal1 = Val(sVal) / 5
            End If
        End If
        If sVal = "" Or ageVal1 = 0 Then
            txtDef1.Text = Session("FirstAge") * 5
            Exit Sub
        End If

        sVal = txtDef2.Text
        If IsNumeric(sVal) Then
            If (Val(sVal) + 1) Mod 5 = 0 And Val(sVal) > 0 And Val(sVal) <= 100 Then
                ageVal2 = (Val(sVal) + 1) / 5
            ElseIf Val(sVal) = 100 Then
                ageVal2 = 21
            End If
        End If
        If sVal = "" Or ageVal2 = 0 Then
            Exit Sub
        End If

        If ageVal2 < ageVal1 Then
            lblMsg.Visible = True
            Exit Sub
        End If

        Session("FirstAge") = ageVal1
        Session("LastAge") = ageVal2

        Dim sAdltAge As String
        If Session("LastAge") = NCohorts Then
            sAdltAge = "Define Adult Age (" & CStr(Session("FirstAge") * 5) & " to " & CStr((NCohorts - 1) * 5) & "+)"
        Else
            sAdltAge = "Define Adult Age (" & CStr(Session("FirstAge") * 5) & " to " & CStr(Session("LastAge") * 5 - 1) & ")"
        End If
        wmMain.Groups("mnuDispType").Items("mnuDefAdltAge").Caption = sAdltAge

        pnlAdltDef.Visible = False
        cmdClear_Click(Me, New EventArgs)
    End Sub

    Protected Sub cmdCancelAdltDef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelAdltDef.Click
        pnlAdltDef.Visible = False
        pnlMort.Visible = True
    End Sub

    Private Sub mnuDeaths_Click()
        Session("bUseDeaths") = Not Session("bUseDeaths")
        If Session("bUseDeaths") Then
            wmMain.Groups("mnuOtherDisp").Items("mnuDeaths").IconImage = "checkbox.gif"
            wmMain.Groups("mnuDispType").Items("mnuChldProb").Enabled = False
            wmMain.Groups("mnuDispType").Items("mnuAdltProb").Enabled = False
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Mortality Display: J - Curve (using number of Deaths in Thousands)"
        Else
            wmMain.Groups("mnuOtherDisp").Items("mnuDeaths").IconImage = "blank.gif"
            wmMain.Groups("mnuDispType").Items("mnuChldProb").Enabled = True
            wmMain.Groups("mnuDispType").Items("mnuAdltProb").Enabled = True
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Mortality Display: J - Curve (using Deaths per Thousand)"
        End If

        'jrs 2009/06/23
        If grdMort.Visible Then
            cmdClear_Click(Me, New EventArgs)
            cmdTable_Click(Me, New EventArgs)
        ElseIf lblJS.Visible Then
            cmdClear_Click(Me, New EventArgs)
            cmdGraph_Click(Me, New EventArgs)
        End If

    End Sub

    Private Sub ConvertToDeaths(ByVal iNCohorts As Integer, ByVal iTypes As Integer,
    ByRef TempDatabase As Object, ByRef TempPopDatabase As Object)
        Dim g As Integer, j As Integer, T As Integer
        'Reload to target
        For j = 0 To iNCohorts
            'Mortality
            For T = 1 To 2
                g = T
                TempDatabase(j, T) = TempDatabase(j, T) * TempPopDatabase(j, g) 'Thousand deaths
            Next T
        Next j
    End Sub

    'jrs 2026/01/22
    Private Function FindLastCohortWithData(ByRef Vals As Object) As Integer
        'jrs 2013/03/29
        FindLastCohortWithData = 21

        'jam 2026/01/18 MAxCohort
        Dim MaxCohortGroup = NCohorts

        'jrs 2014/02/21 If NCohorts > 21 Then
        'Find Last cohort with data
        Dim k As Integer
        'jrs 2014/02/21
        For l% = NCohorts To 1 Step -1
            For k = 1 To Session("TotalGraphs")
                'jrs 2014/10/15
                If Not IsDBNull(Vals(l%, k)) Then
                    If Vals(l%, k) > 0.00005 Then
                        FindLastCohortWithData = l
                        Exit For
                    End If
                End If
            Next k
            If k < Session("TotalGraphs") + 1 Then
                Exit For
            End If
        Next l%
        'jam 2026/01/18 With Compare option, return the maximum category with data
        'jrs 2014/02/21 End If
        'Return FindLastCohortWithData

        Return FindLastCohortWithData


    End Function

    Private Sub PackJScritpBuilder()
        Dim iNCohortsWD As Integer = NCohorts
        'jrs 2026/01/22 enable again
        iNCohortsWD = FindLastCohortWithData(Session("mPLOT"))

        Dim iNCohorts As Integer
        iNCohorts = iNCohortsWD 'jrs2013/08/16 NCohorts
        If Session("bUse100") Then
            iNCohorts = NCohorts
        Else
            'jrs 2026/01/22 iNCohorts = NCohorts - 1
            If iNCohorts = NCohorts Then
                iNCohorts = iNCohorts - 1
            End If
        End If

        If Session("bUseGBDage") Then
            iNCohorts = iNCohorts - 14
            'jrs 2026/01/14
            If NCohorts > 21 Then
                Dim diff As Integer
                diff = NCohorts - 21
                iNCohorts = iNCohorts - diff
            End If
        End If

        Dim Ages() As String
        ReDim Ages(iNCohorts)
        If Session("bUseGBDage") Then
            Ages(0) = "0-4"
            Ages(1) = "5-14"
            Ages(2) = "15-29"

            Ages(3) = "30-44"
            Ages(4) = "45-59"
            Ages(5) = "60-69"
            Ages(6) = "70-79"
            If Session("bUse100") Then
                Ages(iNCohorts) = "80+"
            End If

        Else
            If Session("bUse100") Then
                Ages(iNCohorts) = CStr((NCohorts - 1) * 5) & "+" 'jrs 2026/01/14 "100+"
            Else
                Ages(iNCohorts) = CStr(iNCohorts * 5 - 5) & "-" & CStr(iNCohorts * 5 - 1)
            End If

            Dim l As Integer
            For l = 2 To iNCohorts - 1
                Ages(l) = Str(l * 5 - 5) & "-" & Str(l * 5 - 1)
            Next l
            Ages(0) = "Infants"
            Ages(1) = "1-4"
        End If

        Dim graphtitle As String = ""

        'J-curve
        If Session("GraphType") = "J" Then
            graphtitle = "Mortality Distribution"
        ElseIf Session("GraphType") = "LE" Then 'jrs 2016/06/22
            graphtitle = "Life Expectancy at Age X"
        Else
            If Session("GraphType") = "ChldProb" Or Session("GraphType") = "ChldRate" Then
                If Session("GraphType") = "ChldProb" Then
                    graphtitle = "Probability of Dying as a Child (0 to 4)"
                Else
                    graphtitle = "Child Mortality Rate per Year (0 to 4)"
                End If
            Else
                'jrs 2014/09/03
                If Session("GraphType") = "AdltProb" Then
                    If Session("LastAge") = 21 Then
                        graphtitle = "Probability of Dying as an Adult (" & CStr(Session("FirstAge") * 5) & " to " & CStr((NCohorts - 1) * 5) & "+)"
                    Else
                        graphtitle = "Probability of Dying as an Adult (" & CStr(Session("FirstAge") * 5) & " to " & CStr(Session("LastAge") * 5 - 1) & ")"
                    End If
                Else
                    If Session("LastAge") = 21 Then
                        graphtitle = "Adult Mortality Rate per Year (" & CStr(Session("FirstAge") * 5) & " to " & CStr((NCohorts - 1) * 5) & "+)"
                    Else
                        graphtitle = "Adult Mortality Rate per Year (" & CStr(Session("FirstAge") * 5) & " to " & CStr(Session("LastAge") * 5 - 1) & ")"
                    End If
                End If
            End If
        End If

        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "            var mycategories = [" & vbCr
        Dim iStart As Integer = 0
        If Session("bUseAdltsOnly") Then
            iStart = 6
        ElseIf Not Session("bUseInfants") Then
            iStart = 1
        End If
        If Session("GraphType") = "J" Then
            For l = iStart To iNCohorts
                If l > iStart Then
                    sJscript2 = sJscript2 & ","
                End If
                sJscript2 = sJscript2 & "'" & Ages(l) & "'"
            Next
        ElseIf Session("GraphType") = "LE" Then 'jrs 2016/06/22
            sJscript2 = sJscript2 & "'0','1'"
            For l = 2 To iNCohorts
                sJscript2 = sJscript2 & ","
                sJscript2 = sJscript2 & "'" & Str(l * 5 - 5) & "'"
            Next
        Else
            For l = 1 To Session("LastYear") - Session("FirstYear") + 1
                If l > 1 Then
                    sJscript2 = sJscript2 & ","
                End If
                sJscript2 = sJscript2 & "'" & CStr(Session("FirstYear") + l - 1) & "'"
            Next
        End If
        sJscript2 = sJscript2 & "];" & vbCr

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'jam 2025/12/06 Increasing font size for legends
        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "itemStyle: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '24px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "} " & vbCr
        '''''''''''''''''''''''


        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr

        'jam 2025/03/11 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       '" & graphtitle & "'," & vbCr
        sJscript2 = sJscript2 & "x: -20 //center" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sAxisXLabel As String
        If Session("GraphType") = "J" Or Session("GraphType") = "LE" Then
            sAxisXLabel = "Age"
        Else
            sAxisXLabel = "Year"
        End If

        Dim sAxisYLabel As String
        If Session("GraphType") = "J" Then
            If Session("bUseDeaths") Then
                sAxisYLabel = "Deaths in Thousands"
            Else
                sAxisYLabel = "Deaths per 1000 people"
            End If
        ElseIf Session("GraphType") = "LE" Then
            sAxisYLabel = "Years"
        Else
            If Session("bUseDeaths") Then
                sAxisYLabel = "Deaths in Thousands"
            Else
                If Session("GraphType") = "AdltProb" Or Session("GraphType") = "ChldProb" Then
                    sAxisYLabel = "Chances (Probability) per 1000 of Death"
                Else
                    sAxisYLabel = "Deaths per Thousand"
                End If
            End If
        End If

        sJscript2 = sJscript2 & "xAxis: {" & vbCr

        'jam 2025/12/06 Increasing font for x axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr



        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       '" & sAxisXLabel & "'," & vbCr

        'jam 2025/12/06 Increasing font
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr


        sJscript2 = sJscript2 & "}" & vbCr
        'Load Years
        '----------
        sJscript2 = sJscript2 & ", categories: mycategories"
        If Session("GraphType") <> "J" And Session("GraphType") <> "LE" Then
            sJscript2 = sJscript2 & ", tickInterval: 5" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "yAxis: {" & vbCr

        'jam 2025/12/06 Increasing font for y axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr

        sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 1," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr

        sJscript2 = sJscript2 & "text:       '" & sAxisYLabel & "'," & vbCr

        'jam 2025/12/06 Increasing font
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr


        sJscript2 = sJscript2 & "}," & vbCr


        'jrs 2015/09/08
        sJscript2 = sJscript2 & "minPadding: 0," & vbCr

        sJscript2 = sJscript2 & "plotLines: [{" & vbCr
        sJscript2 = sJscript2 & "value: 0," & vbCr
        sJscript2 = sJscript2 & "width: 1," & vbCr
        sJscript2 = sJscript2 & "color:      '#808080'" & vbCr
        sJscript2 = sJscript2 & "}]" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "legend: {" & vbCr

        'jam 2025/12/06 Increasing fonts
        sJscript2 = sJscript2 & "itemStyle: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}, " & vbCr



        sJscript2 = sJscript2 & "layout:     'horizontal'," & vbCr
        sJscript2 = sJscript2 & "align:      'center'," & vbCr
        sJscript2 = sJscript2 & "verticalAlign:  'top'," & vbCr
        sJscript2 = sJscript2 & "x: -20, " & vbCr 'avoid overlap
        sJscript2 = sJscript2 & "y: 20, " & vbCr 'avoid overlap
        sJscript2 = sJscript2 & "borderWidth: 0" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        For i = 1 To Session("TotalGraphs")
            If i > 1 Then
                sJscript2 = sJscript2 & "," & vbCr
            End If
            sJscript2 = sJscript2 & PlotColToJSSeries(i, Session("mPLOT"), Session("bUse100"), iNCohorts)
        Next i

        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr
        sJscript2 = sJscript2 & ".on('click', function () {" & vbCr
        sJscript2 = sJscript2 & "location.href = 'https://korbel.du.edu/pardee/';" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".attr({" & vbCr
        sJscript2 = sJscript2 & "zIndex: 7" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".css({"
        sJscript2 = sJscript2 & "cursor: 'pointer'"
        sJscript2 = sJscript2 & "})"
        sJscript2 = sJscript2 & ".add();" & vbCr

        'If Not (Session("VLYear") Is Nothing) Then
        '    sJscript2 = sJscript2 & "chart.renderer.path(['m', " & CStr((Session("VLYear") - Session("FirstYear")) * 70 + 100) & ", 0, 'V', 800])" & vbCr
        '    sJscript2 = sJscript2 & ".attr({" & vbCr
        '    sJscript2 = sJscript2 & "'stroke-width': 1," & vbCr
        '    sJscript2 = sJscript2 & "stroke:         'red'," & vbCr
        '    sJscript2 = sJscript2 & "zIndex:1000" & vbCr
        '    sJscript2 = sJscript2 & "})" & vbCr
        '    sJscript2 = sJscript2 & ".add();" & vbCr
        'End If


        sJscript2 = sJscript2 & "var myWidth = 0, myHeight = 0;" & vbCr
        sJscript2 = sJscript2 & "if( typeof( window.innerWidth ) == 'number' ) {" & vbCr
        'Non-IE
        sJscript2 = sJscript2 & "myWidth = window.innerWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = window.innerHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {" & vbCr
        '//IE 6+ in 'standards compliant mode'
        sJscript2 = sJscript2 & "myWidth = document.documentElement.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.documentElement.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {" & vbCr
        '//IE 4 compatible
        sJscript2 = sJscript2 & "myWidth = document.body.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.body.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "if (myWidth == 0)" & vbCr
        sJscript2 = sJscript2 & "{" & vbCr
        sJscript2 = sJscript2 & "myWidth = screen.width;" & vbCr
        sJscript2 = sJscript2 & "myHeight = screen.height;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.95 - 50;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.90 - 50;" & vbCr

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2

    End Sub

    Private Function PlotColToJSSeries(ByVal i As Integer, ByRef Vals As Object, ByVal bShow100 As Boolean, iNCohorts As Integer) As String

        Dim bLeftPyramid As Boolean = False, colIdx As Integer, iLowCohort As Integer
        colIdx = i

        Dim sJscript As String = ""
        sJscript = sJscript & "{name:""" & getSeriesName(i) & """, color:""" & getColor(i) & """, data:[" & vbCr

        Dim j As Integer
        Dim bFirst As Boolean = True
        If Session("GraphType") = "J" Then
            If Not Session("bUseInfants") Then
                iLowCohort = 1
            End If
            If Session("bUseAdltsOnly") Then
                If Session("bUseGBDage") Then
                    iLowCohort = 4
                Else
                    iLowCohort = 6
                End If
            End If

            For j = iLowCohort To iNCohorts
                If bFirst Then
                    bFirst = False
                Else
                    sJscript = sJscript & "," & vbCr
                End If
                If Not IsDBNull(Vals(j, i)) Then
                    sJscript = sJscript & CStr(Math.Round(Vals(j, i), 2))
                End If
            Next j
        ElseIf Session("GraphType") = "LE" Then 'jrs 2016/06/22
            For j = 0 To iNCohorts
                If bFirst Then
                    bFirst = False
                Else
                    sJscript = sJscript & "," & vbCr
                End If
                If Vals(j, i) > -9999 Then
                    sJscript = sJscript & CStr(Math.Round(Vals(j, i), 2))
                End If
            Next j
        Else
            For j = 1 To Session("LastYear") - Session("FirstYear") + 1
                If Not IsDBNull(Session("mPLOT")(j, i)) Then
                    If bFirst Then
                        bFirst = False
                    Else
                        sJscript = sJscript & "," & vbCr
                    End If
                    sJscript = sJscript & CStr(Math.Round(Vals(j, i), 2))
                End If
            Next j
        End If

        sJscript = sJscript & "]}" & vbCr

        PlotColToJSSeries = sJscript
    End Function

    Private Function getSeriesName(ByVal ii As Integer) As String
        Dim Legend As String
        'J-curve
        Legend = Session("mLegends")(1, ii) & ", " & Session("mLegends")(2, ii) & ", " &
        Session("mLegends")(3, ii) & ", " & Session("mLegends")(4, ii) & ", " & Session("mLegends")(5, ii)
        getSeriesName = Legend
    End Function

    Private Function getColor(ByVal i As Integer) As String
        Dim sColor As String = ""
        If i < 9 Then
            Select Case i
                Case 1
                    sColor = "Lime"
                Case 2
                    sColor = "Blue"
                Case 3
                    sColor = "Red"
                Case 5 'blue violet
                    sColor = "RGB(138,43,226)"
                Case 6 'aqua 
                    sColor = "RGB(0,255,255)"
                Case 7 'deep pink
                    sColor = "RGB(255,20,147)"
                Case 8 'medium spring green
                    sColor = "RGB(0,250,154)"
            End Select
        End If
        getColor = sColor
    End Function

    'jrs 2016/06/22
    'jrs 2010/07/20
    'For a Single Year display Life Expectancy at all ages
    'e(x) = T(x)/l(x)
    'T(x) = Sum(L(x))
    'L(x) is the years lived in the category x to x+1
    'Private Sub LoadLifeExpectancyAtAllAges(ByVal TotalGraphs As Integer, ByVal sFileName As String, ByRef dbM As OleDb.OleDbConnection,
    'ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sGender As String, ByVal iYr As Integer)

    Private Sub LoadLifeExpectancyAtAllAges(ByVal TotalGraphs As Integer, ByVal sFileName As String,
    ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sGender As String, ByVal iYr As Integer)



        Dim SetNo%
        Dim rId As Integer
        Dim tbl2 As DataTable
        Dim TCohort As Integer
        Dim sMaleMortFld As String, sFemaleMortFld As String

        Dim TempDatabase(0, 0) As Single
        Dim TempPopDatabase(0, 0) As Single
        Dim RegionIdGroup() As Integer
        Dim GroupTot As Integer
        'Weighted Average for Population
        Dim POPMales(0, 0) As Single
        Dim POPFemales(0, 0) As Single
        Dim sRegs As String

        'jrs 2009/07/02
        Session("bSuccess") = False

        ReDim TempPopDatabase(NCohorts, 2)
        ReDim TempDatabase(NCohorts, 3)

        'jrs 2024/05/28
        'jrs 2006/07/14 create connecion
        'Dim IfsConn As OleDb.OleDbConnection
        'IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        Dim IfsConn As SQLiteConnection = New SQLiteConnection
        IfsConn.ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"
        Dim rdr As OleDb.OleDbDataReader
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'myCMD.Connection = dbM
        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow

        Dim dbM As OleDb.OleDbConnection
        'JAM 2020-06-01
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileName)
            dbM.Open()
            myCMD.Connection = dbM
        Else 'jrs 2024/05/28
            vSQLiteCon.Close()
            vConnString = "DataSource=" + Session("PutFdrvRun") & sFileName & ";"
            vSQLiteCon = New SQLiteConnection(vConnString)
            vSQLiteCon.Open()
        End If

        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable

        DTsqlite = New DataTable
        DTsqlite2 = New DataTable
        regDS = New DataSet


        'take population
        '   Change the data source
        '   ----------------------
        If Not bGrp Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM PopBaseCase Where Region = " &
            Str(iReg) & " and Years = " & Str(iYr))

                'jrs 2009/07/02
                'If tbl2.EOF Then
                '    MsgBox("There's no data in scenario " & _
                '    Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "") & _
                '    " for year " & CStr(iYr), vbCritical, "Load Mortality")
                '    Screen.MousePointer = vbNormal
                '    Exit Sub
                'End If

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    TempPopDatabase(rdr.Item("NCohorts"), 1) = rdr.Item("PopulationMales")
                    TempPopDatabase(rdr.Item("NCohorts"), 2) = rdr.Item("PopulationFemales")
                Loop
                rdr.Close()

            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where RegionId = " &
                                    Str(iReg) & " and TimeId = " & Str(iYr)
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())

                    TempPopDatabase(vSQLiteReader("CohortId"), 1) = vSQLiteReader("PopulationMales")
                    TempPopDatabase(vSQLiteReader("CohortId"), 2) = vSQLiteReader("PopulationFemales")

                End While
            End If

        Else
            ReDim POPMales(NR, NCohorts)
            ReDim POPFemales(NR, NCohorts)

            'jrs 2024/05/28
            IfsConn.Open()
            'Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            'Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            'Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            Dim grpDA As OleDb.OleDbDataAdapter
            Dim regDA As OleDb.OleDbDataAdapter
            Dim regIDDA As OleDb.OleDbDataAdapter

            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase Where Years = " & CStr(iYr) &
            '" ORDER BY Region, NCohorts", dbM)
            'regDS = New DataSet


            Dim popDA As OleDb.OleDbDataAdapter
            regDS = New DataSet
            'jrs 2024/05/28
            regDS.EnforceConstraints = False

            'jrs 2024/05/28
            Dim rdGrpCmd As SQLiteCommand = IfsConn.CreateCommand
            rdGrpCmd.CommandText = "SELECT * FROM " & GroupIDNew
            Dim rdGrp As SQLiteDataReader
            rdGrp = rdGrpCmd.ExecuteReader

            Dim myTbl As DataTable = New DataTable
            myTbl.TableName = "Groups"
            regDS.Tables.Add(myTbl)
            regDS.Tables(0).Load(rdGrp)

            Dim rdRegCmd As SQLiteCommand = IfsConn.CreateCommand
            rdRegCmd.CommandText = "SELECT * FROM " & Grouping
            Dim rdReg As SQLiteDataReader
            rdReg = rdRegCmd.ExecuteReader

            Dim myTbl1 As DataTable = New DataTable
            myTbl1.TableName = "Regions"
            regDS.Tables.Add(myTbl1)
            regDS.Tables(1).Load(rdReg)

            Dim rdRegIdCmd As SQLiteCommand = IfsConn.CreateCommand
            rdRegIdCmd.CommandText = "SELECT * FROM " & RegionIDNew
            Dim rdRegId As SQLiteDataReader
            rdRegId = rdRegIdCmd.ExecuteReader

            Dim myTbl2 As DataTable = New DataTable
            myTbl2.TableName = "RegionIDs"
            regDS.Tables.Add(myTbl2)
            regDS.Tables(2).Load(rdRegId)

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                popDA = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase Where Years = " & CStr(iYr) &
                                                    " ORDER BY Region, NCohorts", dbM)
                popDA.Fill(regDS, "Population")

                grpDA.Fill(regDS, "Groups")
                regDA.Fill(regDS, "Regions")
                regIDDA.Fill(regDS, "RegionIDs")

            Else


                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where TimeId = " & CStr(iYr) &
                                                    " ORDER BY RegionId, CohortId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite.Load(vSQLiteReader)
                'regDS.Tables.Add(DTsqlite)

                'jrs 2024/05/28
                'dtnew = DTsqlite.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite)

            End If




            'popDA.Fill(regDS, "Population")
            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                 regDS.Tables("Groups").Columns("GroupNum"),
                                                 regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Population").Columns("Region"))


            Dim regPopRel As DataRelation


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Population").Columns("Region"))
            Else


                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("pop_base_case").Columns(1))


            End If

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("NCohorts"), 1) =
                                TempPopDatabase(cRow.Item("NCohorts"), 1) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("NCohorts"), 2) =
                                TempPopDatabase(cRow.Item("NCohorts"), 2) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("Region"), cRow.Item("NCohorts")) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("Region"), cRow.Item("NCohorts")) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next
            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("CohortId"), 1) =
                                TempPopDatabase(cRow.Item("CohortId"), 1) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("CohortId"), 2) =
                                TempPopDatabase(cRow.Item("CohortId"), 2) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("RegionId"), cRow.Item("CohortId")) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId")) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next
            End If
        End If
        'End take population


        '   Change the data source
        '   ----------------------
        If Not bGrp Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM MorDetBaseCase Where Region = " & Str(iReg) &
            " and Years = " & CStr(iYr))

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    TCohort = rdr.Item("NCohorts")
                    'males 
                    TempDatabase(TCohort, 1) = AMAX(0, rdr.Item("MortPerMalesComDis") +
                rdr.Item("MortPerMalesNonComDis") + rdr.Item("MortPerMalesInjuries")) / 1000

                    'females 
                    TempDatabase(TCohort, 2) = AMAX(0, rdr.Item("MortPerFeMalesComDis") +
                rdr.Item("MortPerFemalesNonComDis") + rdr.Item("MortPerFemalesInjuries")) / 1000

                    'total
                    If TempPopDatabase(TCohort, 1) + TempPopDatabase(TCohort, 2) <> 0 Then
                        TempDatabase(TCohort, 3) = TempDatabase(TCohort, 1) * TempPopDatabase(TCohort, 1) +
                    TempDatabase(TCohort, 2) * TempPopDatabase(TCohort, 2)
                        TempDatabase(TCohort, 3) = TempDatabase(TCohort, 3) / (TempPopDatabase(TCohort, 1) + TempPopDatabase(TCohort, 2))
                    Else
                        TempDatabase(TCohort, 3) = 0
                    End If
                Loop
                rdr.Close()
            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where RegionId = " & Str(iReg) &
                                         " and TimeId = " & CStr(iYr)

                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())
                    TCohort = vSQLiteReader("CohortId")
                    'males 
                    TempDatabase(TCohort, 1) = AMAX(0, vSQLiteReader("MortPerMalesComDis") +
                            vSQLiteReader("MortPerMalesNonComDis") + vSQLiteReader("MortPerMalesInjuries")) / 1000

                    'females 
                    TempDatabase(TCohort, 2) = AMAX(0, vSQLiteReader("MortPerFeMalesComDis") +
                        vSQLiteReader("MortPerFeMalesNonComDis") + vSQLiteReader("MortPerFeMalesInjuries")) / 1000

                    'total
                    If TempPopDatabase(TCohort, 1) + TempPopDatabase(TCohort, 2) <> 0 Then
                        TempDatabase(TCohort, 3) = TempDatabase(TCohort, 1) * TempPopDatabase(TCohort, 1) +
                    TempDatabase(TCohort, 2) * TempPopDatabase(TCohort, 2)
                        TempDatabase(TCohort, 3) = TempDatabase(TCohort, 3) / (TempPopDatabase(TCohort, 1) + TempPopDatabase(TCohort, 2))
                    Else
                        TempDatabase(TCohort, 3) = 0
                    End If
                End While

            End If

        Else

            '    Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase Where Years = " &
            'CStr(iYr) & " ORDER BY Region, NCohorts", dbM)
            'mortDA.Fill(regDS, "Mortality")


            Dim mortDA As OleDb.OleDbDataAdapter


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                mortDA = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase Where Years = " &
                                                    CStr(iYr) & " ORDER BY Region, NCohorts", dbM)
                mortDA.Fill(regDS, "Mortality")
            Else

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where TimeId = " &
                                                    CStr(iYr) & " ORDER BY RegionId, CohortId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite2.Load(vSQLiteReader)

                'jrs 2024/05/28
                'dtnew = DTsqlite2.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite2.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite2)
            End If





            'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Mortality").Columns("Region"))

            Dim regMortRel As DataRelation


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regMortRel = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Mortality").Columns("Region"))
            Else


                regMortRel = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("Mor_Det_Base_Case").Columns("RegionId"))


            End If


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("NCohorts")
                                    rId = cRow.Item("Region")

                                    TempDatabase(TCohort, 1) =
                                    TempDatabase(TCohort, 1) +
                                    (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                    cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort)

                                    TempDatabase(TCohort, 2) =
                                TempDatabase(TCohort, 2) +
                                (cRow.Item("MortPerFemalesComDis") + cRow.Item("MortPerFemalesNonComDis") +
                                cRow.Item("MortPerFemalesInjuries")) * POPFemales(rId, TCohort)

                                Next
                            Next
                        Next
                    End If
                Next
            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("CohortId")
                                    rId = cRow.Item("RegionId")

                                    TempDatabase(TCohort, 1) =
                                    TempDatabase(TCohort, 1) +
                                    (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                    cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort)

                                    TempDatabase(TCohort, 2) =
                                TempDatabase(TCohort, 2) +
                                (cRow.Item("MortPerFeMalesComDis") + cRow.Item("MortPerFeMalesNonComDis") +
                                cRow.Item("MortPerFeMalesInjuries")) * POPFemales(rId, TCohort)

                                Next
                            Next
                        Next
                    End If
                Next
            End If
            IfsConn.Close()

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                dbM.Close()
            End If


            'Population Weigthed average
            For TCohort = 0 To NCohorts
                'jrs 2012/10/30
                If TempPopDatabase(TCohort, 1) + TempPopDatabase(TCohort, 2) > 0 Then
                    TempDatabase(TCohort, 3) = (TempDatabase(TCohort, 1) + TempDatabase(TCohort, 2)) /
                (1000 * (TempPopDatabase(TCohort, 1) + TempPopDatabase(TCohort, 2)))
                End If

                'jrs 2012/10/30
                If TempPopDatabase(TCohort, 1) > 0 Then
                    TempDatabase(TCohort, 1) = TempDatabase(TCohort, 1) / (1000 * TempPopDatabase(TCohort, 1))
                End If

                'jrs 2012/10/30
                If TempPopDatabase(TCohort, 2) > 0 Then
                    TempDatabase(TCohort, 2) = TempDatabase(TCohort, 2) / (1000 * TempPopDatabase(TCohort, 2))
                End If
            Next TCohort

        End If

        'Compute Life Expectancy LE(x)
        '=============================
        Dim T(0, 0) As Single, l(0, 0) As Single
        ReDim T(NCohorts, 3), l(NCohorts, 3)
        Dim N As Integer, j As Integer, g As Integer
        Dim nqx As Single, nax As Single, jj As Integer
        Dim cumMort As Single

        For g = 1 To 3
            cumMort = 0
            For j = 0 To NCohorts
                'Years in cohort
                If j = 0 Then
                    N = 1
                    For jj = 0 To NCohorts
                        l(jj, g) = 1
                    Next jj
                ElseIf j = 1 Then
                    N = 4
                Else
                    N = 5
                End If

                'Average years lived by those who die (per Keyfitz)
                If j = 0 Then
                    If TempDatabase(j, g) >= 0.107 Then
                        nax = 0.34
                    Else
                        nax = 0.049 + 2.742 * TempDatabase(0, g)
                    End If
                ElseIf j = 1 Then
                    If TempDatabase(0, g) >= 0.107 Then
                        nax = 1.356
                    Else
                        nax = 1.587 - 2.167 * TempDatabase(0, g)
                    End If
                Else
                    nax = 2.5
                End If

                'Probability of death using period data
                If j = NCohorts Then
                    nqx = 1
                Else
                    nqx = (N * TempDatabase(j, g)) / (1 + ((N - nax) * TempDatabase(j, g)))
                    'jrs 2012/10/29
                    If nqx > 1 Then
                        nqx = 1
                    End If
                End If

                'Adding N years for survivors, and nax for deaths
                If j < NCohorts Then
                    For jj = 0 To j
                        T(jj, g) = T(jj, g) + (l(j, g) * (1 - nqx) * N) + (l(j, g) * nqx * nax)
                    Next jj
                ElseIf TempDatabase(NCohorts, g) > 0 Then 'jrs 2012/10/29
                    T(NCohorts, g) = T(NCohorts, g) + l(NCohorts, g) / TempDatabase(NCohorts, g)
                End If
                'Probability of surviving until age x
                For jj = j + 1 To NCohorts
                    l(jj, g) = l(jj, g) * (1 - nqx)
                Next jj
                'Computing cummulative mortality for Barry's logic when LE going above 100
                cumMort = cumMort + N * TempDatabase(j, g)
                If j = NCohorts Then
                    If cumMort < 0.5 Then
                        T(NCohorts, g) = T(NCohorts, g) + 15 * (0.5 - cumMort) / AMAX(cumMort, 0.001)
                    End If
                End If
            Next j
        Next g

        'Load Final Array
        For TCohort = 0 To NCohorts - 1
            If sGender = "Male" Then
                If l(TCohort, 1) > 0 Then
                    Session("mPLOT")(TCohort, TotalGraphs) = AMAX(Math.Round(T(TCohort, 1) / l(TCohort, 1), 1), 0)
                Else
                    Session("mPLOT")(TCohort, TotalGraphs) = -9999
                End If
            ElseIf sGender = "Female" Then
                If l(TCohort, 2) > 0 Then
                    Session("mPLOT")(TCohort, TotalGraphs) = AMAX(Math.Round(T(TCohort, 2) / l(TCohort, 2), 1), 0)
                Else
                    Session("mPLOT")(TCohort, TotalGraphs) = -9999
                End If
            Else
                If l(TCohort, 3) > 0 Then
                    Session("mPLOT")(TCohort, TotalGraphs) = AMAX(Math.Round(T(TCohort, 3) / l(TCohort, 3), 1), 0)
                Else
                    Session("mPLOT")(TCohort, TotalGraphs) = -9999
                End If
            End If
        Next TCohort

        If sGender = "Male" Then
            Session("mPLOT")(NCohorts, TotalGraphs) = -9999
        ElseIf sGender = "Female" Then
            Session("mPLOT")(NCohorts, TotalGraphs) = -9999
        Else
            Session("mPLOT")(NCohorts, TotalGraphs) = -9999
        End If

        'jrs 2010/02/09
        Session("mLegends")(0, TotalGraphs) = sReg
        'If bGrp And mnulblAlias.Checked Then
        '    'Read and load all aliases
        '    Dim db As Database
        '    Dim Tbl As Recordset
        '    db = OpenDatabase(IFs_MDB2)
        '    Tbl = db.OpenRecordset(GroupIDNew)
        '    Tbl.Index = "GroupName"
        '    Tbl.Seek("=", sReg)
        '    If Not Tbl.NoMatch Then
        '        Session("mLegends")(1, TotalGraphs) = IIf(IsNull(Tbl.Fields("Alias")), "", Tbl.Fields("Alias")) 'jrs 2012/05/17
        '    End If
        '    Tbl.Close()
        '    db.Close()
        'Else
        '    Session("mLegends")(1, TotalGraphs) = sReg
        'End If
        Session("mLegends")(1, TotalGraphs) = sReg


        Session("mLegends")(2, TotalGraphs) = "All Types"
        If sGender <> "Total" Then
            Session("mLegends")(3, TotalGraphs) = sGender
        Else
            Session("mLegends")(3, TotalGraphs) = "All Genders"
        End If
        Session("mLegends")(4, TotalGraphs) = CStr(iYr)
        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then
            Session("mLegends")(5, TotalGraphs) = "Working"
        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            Session("mLegends")(5, TotalGraphs) = "Base"
        Else
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "")

            Else
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".db", "")
            End If
        End If

        'jrs 2009/07/02
        Session("bSuccess") = True


    End Sub

End Class
