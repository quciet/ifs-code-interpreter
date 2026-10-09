Imports IFs.Data

'jrs 2007/03/15 
'Conversion to the WEB and original design by Jose R. Solorzano
Partial Class frm_PovertyDisplay
    Inherits Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()

        'jrs 2005/04/22
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Or Session("optPov") = "Pct" Or Session("optPov") = "Range" Then 'jrs 2011/06/20
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            'jrs 2011/06/20
            If Session("GroupOpt") = Nothing Then
                'jrs 2008/12/22
                Session("GroupOpt") = 0
            End If
        End If
        Form_Activate()
        'jrs 11/08/04
        'jrs 2011/05/04 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.MenuBar.ShowOnMouseOver = True
        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display Format"
        'jrs 2008/03/31
        cswmGroup1.ID = "mnuGrpDisplay"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Set Title, Display Interval, or Year"
        'jrs 2006/11/28
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Currency"
        'jrs 2006/11/28
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Horizon"
        'jrs 2006/11/28
        cswmItem.IconImage = "blank.gif"
        'jrs 2008/03/28
        cswmItem.ID = "mnuHorizon"

        Dim cswmGroup2 As Coalesys.WebMenu.Group
        cswmGroup2 = cswmItem.AddGroup()
        'jrs 2008/03/28
        cswmGroup2.ID = "mnuHorizonGrp"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Display/Run Horizon"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Run File Horizon Choice (With Multiple File Use)"
        'jrs 2008/03/28
        cswmItem.ID = "mnuRunFile"

        Dim cswmGroup3 As Coalesys.WebMenu.Group
        cswmGroup3 = cswmItem.AddGroup()
        'jrs 2008/03/28
        cswmGroup3.ID = "mnuRunFileGrp"

        cswmItem = cswmGroup3.Items.Add()
        cswmItem.Caption = "Shortest Run File Horizon"
        'jrs 2008/03/28
        cswmItem.ID = "mnuShort"
        If Session("DisplayTime") = 0 Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup3.Items.Add()
        cswmItem.Caption = "Longest Run File Horizon"
        'jrs 2008/03/28
        cswmItem.ID = "mnuLong"
        If Session("DisplayTime") = 1 Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2006/01/18
        cswmGroup1 = cswmWebMenu.Groups.Add()
        'jrs 2008/12/22
        'If Session("mnuGroupsCaption") Is Nothing Then
        '    Session("mnuGroupsCaption") = "Using &Countries/Regions"
        'End If
        'cswmGroup1.Caption = Session("mnuGroupsCaption")
        ''jrs 2006/02/03
        'cswmGroup1.ID = "mnuGroups"
        cswmGroup1.Caption = "Group Options"
        cswmGroup1.ID = "mnuGroupOpt"

        cswmItem = cswmGroup1.Items.Add()
        'jrs 2008/12/22 cswmItem.Caption = "Switch"
        cswmItem.Caption = "Using Countries/Regions"
        cswmItem.ID = "mnuRegs"
        If Session("GroupOpt") = 0 Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using Groups"
        cswmItem.ID = "mnuGroups"
        If Session("GroupOpt") = 1 Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using G-Lists"
        cswmItem.ID = "mnuGlists"
        If Session("GroupOpt") = 2 Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using Decomposed Groups"
        cswmItem.ID = "mnuGroupsDec"
        If Session("GroupOpt") = 3 Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        If Session("mnuPctCaption") Is Nothing Then
            Session("mnuPctCaption") = "Using Millions of People"
        End If
        cswmGroup1.Caption = Session("mnuPctCaption")
        'jrs 2006/02/03
        cswmGroup1.ID = "mnuPct"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        'jrs 2024/03/12
        If Session("mnu2011PPP") Is Nothing Then
            'jrs 2025/10/03
            'Session("mnu2011PPP") = "Using 2017 PPP"
            Session("mnu2011PPP") = "Using " & ConstantDollarsYear.ToString & " PPP"
            txtPovLev.Text = PovLevelsByPPPYear(ConstantDollarsYear)("EXTREME").ToString
            Session("txtPovLev") = txtPovLev.Text
        End If
        cswmGroup1.Caption = Session("mnu2011PPP")
        'jrs 2006/02/03
        cswmGroup1.ID = "mnu2005PPP"

        'jrs 2025/10/03
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Switch"

        'jrs 2006/12/18
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16002);"
        'jrs 2013/09/04
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'Put user code to initialize the page here
        If Not Page.IsPostBack Then

            Session("TableDisplayParent") = "frm_PovertyDisplay.aspx"
            'jrs 2011/09/28 Session("ProEssentialParent") = "frm_PovertyDisplay.aspx"
            Session("GraphDisplayParent") = "frm_PovertyDisplay.aspx"
            If Session("bDisplayControl") Is Nothing Then
                'jrs 2005/11/29
                Session("TableTitle") = ""
            Else
                Session("bDisplayControl") = Nothing
            End If

            'Load Files
            LoadFiles()
            'Load Regions
            LoadRegions()

            If Session("ClearPov") Is Nothing Then
                'Clear Variables, to handle switch between whole and percent
                ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                    Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))

                Form_Activate()
            Else
                txtPovLev.Text = Session("txtPovLev")
                txtPct.Text = Session("txtPovPct")
                'jrs 2011/06/20
                txtIncomeR1.Text = Session("txtIncomeR1")
                txtIncomeR2.Text = Session("txtIncomeR2")
            End If
            'jrs 2007/03/16
            If Session("optPov") = "Pct" Then
                cswmWebMenu.Groups("mnuPct").ShowMenuBarItem = False
            End If

            'jrs 2005/04/19
            ReDim Preserve Session("PDFilesIdxs")(lstFiles.Items.Count - 1)
            If Not (Session("PDFilesIdxs")(0) Is Nothing) Then
                Dim fileIdx As Integer
                For fileIdx = 0 To lstFiles.Items.Count - 1
                    lstFiles.Items(fileIdx).Selected = Session("PDFilesIdxs")(fileIdx)
                Next fileIdx
            End If

            'jrs 2011/11/04
            If Not Session("txtFiles") Is Nothing Then
                txtFiles.Value = Session("txtFiles")
            End If

            'jrs 2006/01/18
            ReDim Preserve Session("RegIdxs")(lstReg.Items.Count - 1)
            If Not (Session("RegIdxs")(0) Is Nothing) Then
                Dim regIdx As Integer
                For regIdx = 0 To lstReg.Items.Count - 1
                    lstReg.Items(regIdx).Selected = Session("RegIdxs")(regIdx)
                Next regIdx
            End If
            If Session("DisplayLastYear") = 0 Then
                Session("DisplayLastYear") = Session("LastYear")
            End If
        End If
        'jrs 11/8/04
        Session("RefreshGraph") = "true"
        'jrs 2005/12/10
        Session("TableHis") = Nothing
        'jrs 2011/11/03
        lstFiles.Attributes.Add("onclick", "lstFiles_Click();")
        lstFiles.Attributes.Add("onmousedown", "lstFiles_ClickDown();")
        lstFiles.Attributes.Add("onmouseup", "lstFiles_ClickUp();")
    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Continue"
                cmdContinue_Click(Me, New EventArgs)
            Case "Currency"
                'jrs 2008/05/09
                Session("CurrencyParent") = "frm_PovertyDisplay.aspx"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_CurrencyOpt.aspx")
            Case "Set Title, Display Interval, or Year"
                mnuDispCtrl_Click(sender, e)
            Case "Display/Run Horizon"
                mnuDisplayRunHorizon_Click(sender, e)
                    'jrs 2008/12/22 Case "Switch" 'jrs 2006/01/18
                    'mnuGroups_Click()
            Case "Using Countries/Regions"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuRegs").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuGroups").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuGlists").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuGroupsDec").IconImage = "blank.gif"
                Session("GroupOpt") = 0
                Session.Remove("RegIdxs")
                LoadRegions()
            Case "Using Groups"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuRegs").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuGroups").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuGlists").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuGroupsDec").IconImage = "blank.gif"
                Session("GroupOpt") = 1
                Session.Remove("RegIdxs")
                LoadRegions()
            Case "Using G-Lists"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuRegs").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuGroups").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuGlists").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuGroupsDec").IconImage = "blank.gif"
                Session("GroupOpt") = 2
                Session.Remove("RegIdxs")
                LoadRegions()
            Case "Using Decomposed Groups"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuRegs").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuGroups").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuGlists").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuGroupOpt").Items("mnuGroupsDec").IconImage = "checkbox.gif"
                Session("GroupOpt") = 3
                Session.Remove("RegIdxs")
                LoadRegions()

            Case "Change" 'jrs 2006/01/18
                mnuPercent_Click()
            Case "Switch"
                mnu2005PPP_Click()
            Case "Shortest Run File Horizon" 'jrs 2008/03/28
                Session("DisplayTime") = 0
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuShort").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuLong").IconImage = "blank.gif"
            Case "Longest Run File Horizon"
                Session("DisplayTime") = 1
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuLong").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuShort").IconImage = "blank.gif"

        End Select
    End Sub

    Public Sub mnuDispCtrl_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Session("DisplayControlParent") = "frm_PovertyDisplay.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_DisplayControl.aspx")
        'frm_DisplayControl.DefInstance.Show() 
    End Sub

    Public Sub mnuDisplayRunHorizon_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim ICount As Short
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If CBool(Session("HistoricBase")) = True Then
            Msg = "You cannot change the horizon for historical analysis."
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If
        cmb_Horizon.Visible = True
        frm_Horizon.Visible = True
        'jrs 2005/12/11
        If Session("Interval") Is Nothing Or Session("Interval") = 0 Then
            Session("Interval") = 1
        End If
        cmb_Horizon.Items.Clear()
        cmb_Horizon.Items.Add("")
        'jrs 2008/12/22
        cmb_Horizon.Items.Add(Session("FirstYear"))

        For ICount = Session("FirstYear") + Session("Interval") To Session("LastYearForecast") Step Session("Interval") 'jrs 2005/12/11 added step interval
            cmb_Horizon.Items.Add(New ListItem(CStr(ICount)))
        Next ICount

    End Sub

    Private Sub mnuGroups_Click()
        If Session("mnuGroupsCaption") = "Using &Countries/Regions" Then
            Session("mnuGroupsCaption") = "Using &Groups"
            lblReg.Text = "Groups:"
        Else
            'jrs 2006/01/18
            If Session("mnuGroupsCaption") = "Using &Groups" Then
                Session("mnuGroupsCaption") = "Using &G-Lists"
            Else
                Session("mnuGroupsCaption") = "Using &Countries/Regions"
            End If
        End If
        'jrs 2006/02/03
        cswmWebMenu.Groups("mnuGroups").Caption = Session("mnuGroupsCaption")

        Session.Remove("RegIdxs")
        LoadRegions()
        'jrs 2008/07/21
        ReDim Session("RegIdxs")(lstReg.Items.Count - 1)
    End Sub

    Protected Sub cmdContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdContinue.Click
        'jrs 2005/08/24
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
        'jrs 2005/11/09
        Session("TableTitle") = ""
        'jrs 2007/03/16
        Session("ClearPov") = Nothing
        Session("optPov") = Nothing
        Session.Remove("GroupOpt")

        'jrs 2011/05/04
        If Session("DisplayParent") = Nothing Then
            Session("DisplayParent") = "frm_MainMenu.aspx"
        End If

        Support.FormManager.Show(Me, Session("DisplayParent"))

    End Sub

    Private Sub LoadRegions()
        Dim i As Integer
        Dim db As String
        Dim valtbl As DataTable
        lstReg.Items.Clear()
        'jrs 2008/12/22 If Session("mnuGroupsCaption") = "Using &G-Lists" Then
        If Session("GroupOpt") = 2 Then 'g-list
            db = Session("IFs_MDB2Active")
            SetDataTablefromAccess(db, "SELECT * FROM GListId ORDER BY Description", "", valtbl)
            i = 0
            While Not valtbl.Rows.Count = i
                lstReg.Items.Add(valtbl.Rows(i).Item("Description"))
                i = i + 1
            End While
        Else
            'jrs 2008/12/22 If Session("mnuGroupsCaption") = "Using &Groups" Then
            If Session("GroupOpt") = 1 Or Session("GroupOpt") = 3 Then
                db = Session("IFs_MDB2Active")
                SetDataTablefromAccess(db, "SELECT * FROM " & GroupIDNew$ & " ORDER BY GroupName", "", valtbl)
                i = 0
                While Not valtbl.Rows.Count = i
                    lstReg.Items.Add(valtbl.Rows(i).Item("GroupName"))
                    i = i + 1
                End While
            Else
                For i = 1 To NR
                    lstReg.Items.Add(GRPS(1, i))
                Next i
            End If
        End If
        If Session("RegIdxs") Is Nothing Then
            lstReg.SelectedIndex = 0
            'jrs 2008/12/22
            ReDim Session("RegIdxs")(lstReg.Items.Count - 1)
        Else
            For i = 0 To lstReg.Items.Count - 1
                lstReg.Items(i).Selected = Session("RegIdxs")(i)
            Next
        End If
    End Sub

    Private Sub LoadFiles()

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim fs 'As New FileSystemObject
        Dim BASE 'As File
        Dim currRun 'As File
        fs = CreateObject("Scripting.FileSystemObject")
        Dim sWorkingFile As String
        sWorkingFile = Right(Session("WorkingFile"), Len(Session("WorkingFile")) - InStrRev(Session("WorkingFile"), "\"))

        'JRS 05/20/03
        If Session("FirstYear") = FirstYearHistory And Session("LastYear") = LastYearHistory Then
            If Session("mnuSQLite") = "True" Then
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBaseHist.run.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBaseHist.run")
            End If
        Else
            If Session("mnuSQLite") = "True" Then
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBase.run.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBase.run")
            End If
        End If
        'jrs 2005/05/09
        lstFiles.Items.Clear()

        lstFiles.Items.Add("0 - " + "Working File, based on " + sWorkingFile)
        lstFiles.Items.Add("1 - " + BASE.Name)
        'jrs 2005/02/28
        Session("FilesSelected") = 1

        Dim sRunFile As String
        ' Returns filename with specified extension. If more than one *.ini
        ' file exists, the first file found is returned.
        sRunFile = Dir(Session("PutFdrvRun2") + "\*.run.db")

        Do While (sRunFile <> "")
            If UCase(sRunFile) <> UCase(BASE.Name) And
                UCase(sRunFile) <> UCase(sWorkingFile) And
                UCase(sRunFile) <> "WORKING.RUN.DB" Then 'JRS 11/08/02
                currRun = fs.GetFile(Session("PutFdrvRun2") + "\" + sRunFile)
                'jrs 2022/04/08 added Or StandAloneMode
                If Session("IFsRunFiles").item(sRunFile).Visible Or StandAloneMode Then 'jrs 2005/06/27
                    lstFiles.Items.Add(Trim(Str(lstFiles.Items.Count)) + " - " + sRunFile)
                    Session("FilesSelected") = Session("FilesSelected") + 1
                    ReDim Preserve Session("RunFiles")(Session("FilesSelected"))
                    Session("RunFiles")(Session("FilesSelected")) = Session("PutFdrvRun2") + "\" + sRunFile
                    'jrs 2005/02/25
                    If Session("FilesSelected") > UBound(Session("ShortSce")) Then
                        ReDim Preserve Session("ShortSce")(Session("FilesSelected"))
                    End If
                    Dim k As Integer
                    k = getLongSceIdx(sRunFile)
                    If k > -1 Then
                        Session("ShortSce")(Session("FilesSelected")) = ShortSce(k)
                    Else
                        'jrs 2008/05/13 Session("ShortSce")(Session("FilesSelected")) = Left(sRunFile, 8)
                        Session("ShortSce")(Session("FilesSelected")) = TruncateSceFileName(sRunFile)
                    End If
                End If
            End If
            ' Call Dir again without arguments to return the next *.INI file in the
            ' same directory.
            sRunFile = Dir()
        Loop
        'jrs 2026/01/26 changed default to base
        lstFiles.SelectedIndex = 1
        lstFiles.Items(1).Selected = True
    End Sub

    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        'Load Poverty Display
        LoadPovertyDisplay()
        'Refresh Status Box
        Form_Activate()
    End Sub

    Private Sub Form_Activate()
        Dim TotalColWidth As Single
        Dim ColWidth As Short
        Dim RowHeight As Single
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '
        '   Grid Setup
        '   ----------

        RowHeight = 230
        ColWidth = HalfInch
        TotalColWidth = ColWidth * 5

        grd_VariableDisplay.Visible = True


        '   Fill Grid
        '   ---------

        FillGrid()

    End Sub

    Public Sub FillGrid()
        Dim iloop As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 4 / 25 / 2
        '   Fill Grid
        '   ---------
        grd_VariableDisplay.Columns.Clear()
        grd_VariableDisplay.Visible = (Session("COUNTE") > 0)

        '      If Counte < 1 Then Counte = 1
        'JRS 02/18/04
        Dim r1 As New TableRow, r2 As New TableRow, r3 As New TableRow, r4 As New TableRow
        For iloop = 1 To Session("COUNTE")
            Dim myCol As New TMSWebControls.Column
            'myCol.Type = TMSWebControls.Column.ColumnType.Button
            myCol.WidthType = TMSWebControls.WType.Absolute
            'myCol.ButtonWidth = 100
            myCol.Width = 100
            grd_VariableDisplay.Columns.Add(myCol)

            grd_VariableDisplay(iloop - 1, 0) = Session("VARN")(1, iloop)
            grd_VariableDisplay(iloop - 1, 1) = Session("GR1N")(iloop)
            grd_VariableDisplay(iloop - 1, 2) = Session("GR2N")(iloop)
            grd_VariableDisplay(iloop - 1, 3) = Session("GR3N")(iloop)
            grd_VariableDisplay(iloop - 1, 4) = Session("GR4N")(iloop) 'jrs 2006/06/19

        Next iloop


    End Sub

    Private Sub LoadPovertyDisplay()
        'need validation for the level
        LevelValidation()
        If optLevel.Checked Then
            If txtPovLev.Text = "" Then Exit Sub
        ElseIf optRange.Checked Then 'jrs 2011/06/17
            If txtIncomeR1.Text = "" Or txtIncomeR2.Text = "" Then Exit Sub
        Else
            If txtPct.Text = "" Then Exit Sub
        End If
        'jrs 2007/03/16
        Session("ClearPov") = False
        Session("txtPovLev") = txtPovLev.Text
        Session("txtPovPct") = txtPct.Text
        'jrs 2011/06/20
        Session("txtIncomeR1") = txtIncomeR1.Text
        Session("txtIncomeR2") = txtIncomeR2.Text

        Dim dbGL As String
        dbGL = Session("IFs_MDB2Active")
        Dim rsGL As DataTable

        Dim bPctPop As Boolean
        If Session("mnuPctCaption") = "Using Percent of Population" Then
            bPctPop = True
        End If

        'jrs 2014/10/21
        Dim b2011PPP As Boolean
        'jrs 2024/03/12
        If Session("mnu2011PPP") = "Using 2011 PPP" Then
            b2011PPP = True
        End If

        'jrs 2011/11/04
        'Create Collection based on Order passed or listBox
        Dim SceOrdFiles As New Collection, j As Integer
        Dim sceFile As Object
        For j = 0 To lstFiles.Items.Count - 1
            Session("PDFilesIdxs")(j) = False
        Next
        If Trim(txtFiles.Value) > "" Then
            Dim Fs As Object
            Fs = Split(txtFiles.Value, ",")
            For j = 0 To UBound(Fs)
                SceOrdFiles.Add(CInt(Fs(j)), Fs(j).ToString)
            Next
        Else
            For j = 0 To lstFiles.Items.Count - 1
                If lstFiles.Items(j).Selected Then
                    SceOrdFiles.Add(j, j.ToString)
                End If
            Next
        End If

        'Loop for all regions
        Dim i As Integer, k As Integer
        For i = 0 To lstReg.Items.Count - 1
            Session("RegIdxs")(i) = lstReg.Items(i).Selected
            If lstReg.Items(i).Selected Then
                'jrs 2008/12/22 If Session("mnuGroupsCaption") = "Using &G-Lists" Then
                If Session("GroupOpt") = 2 Then
                    SetDataTablefromAccess(dbGL, "SELECT * FROM GList d, GListId i WHERE i.Code = d.GListCode AND i.Description = """ & lstReg.Items(i).Text & """ ORDER BY MemberOrder, Region", "", rsGL)
                    k = 0
                ElseIf Session("GroupOpt") = 3 Then
                    SetDataTablefromAccess(dbGL, "SELECT * FROM " & GroupIDNew & " i, " &
                        Grouping & " d WHERE i.GroupNum = d.GroupNo AND i.GroupName = """ & _
                        lstReg.Items(i).Text & """ ORDER BY Region", "", rsGL)
                    k = 0
                End If

                Do While True
                    'Loop for all files
                    'jrs 2011/11/11 For j = 0 To lstFiles.Items.Count - 1
                    For Each sceFile In SceOrdFiles
                        j = CInt(sceFile)

                        Session("PDFilesIdxs")(j) = lstFiles.Items(j).Selected
                        If lstFiles.Items(j).Selected Then
                            Session("COUNTE") = Session("COUNTE") + 1
                            'compute data
                            If optLevel.Checked Then
                                'jrs 2008/12/22 If Session("mnuGroupsCaption") = "Using &G-Lists" Then
                                If Session("GroupOpt") = 2 Then
                                        gLoadPovertyDisp(txtPovLev.Text, rsGL.Rows(k).Item("Region"), j, rsGL.Rows(k).Item("Group"), bPctPop, b2011PPP,
                                        Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"), Session("mnuSQLite"))
                                ElseIf Session("GroupOpt") = 3 Then
                                        gLoadPovertyDisp(txtPovLev.Text, rsGL.Rows(k).Item("Region"), j, False, bPctPop, b2011PPP,
                                        Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"), Session("mnuSQLite"))
                                Else
                                        gLoadPovertyDisp(txtPovLev.Text, lstReg.Items(i).Text, j, (Session("GroupOpt") = 1), bPctPop, b2011PPP,
                                        Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"), Session("mnuSQLite"))
                                End If
                            ElseIf optRange.Checked Then 'jrs 2011/06/17
                                If Session("GroupOpt") = 2 Then
                                        gLoadIncomeRange(txtIncomeR1.Text, txtIncomeR2.Text, rsGL.Rows(k).Item("Region"), j, rsGL.Rows(k).Item("Group"), bPctPop, b2011PPP,
                                        Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"), Session("mnuSQLite"))
                                ElseIf Session("GroupOpt") = 3 Then
                                        gLoadIncomeRange(txtIncomeR1.Text, txtIncomeR2.Text, rsGL.Rows(k).Item("Region"), j, False, bPctPop, b2011PPP,
                                        Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"), Session("mnuSQLite"))
                                Else
                                        gLoadIncomeRange(txtIncomeR1.Text, txtIncomeR2.Text, lstReg.Items(i).Text, j, (Session("GroupOpt") = 1), bPctPop, b2011PPP,
                                        Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"), Session("mnuSQLite"))
                                End If
                            Else
                                'jrs 2008/12/22 If Session("mnuGroupsCaption") = "Using &G-Lists" Then
                                If Session("GroupOpt") = 2 Then
                                        gFindPovertyLevel(rsGL.Rows(k).Item("Region"), j, rsGL.Rows(k).Item("Group"), txtPct.Text, b2011PPP,
                                        Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"), Session("mnuSQLite"))
                                ElseIf Session("GroupOpt") = 3 Then
                                        gFindPovertyLevel(rsGL.Rows(k).Item("Region"), j, False, txtPct.Text, b2011PPP,
                                        Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"), Session("mnuSQLite"))
                                Else
                                    gFindPovertyLevel(lstReg.Items(i).Text, j, (Session("GroupOpt") = 1), txtPct.Text, b2011PPP,
                                        Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"), Session("mnuSQLite"))
                                End If
                            End If
                        End If
                    Next 'jrs 2011/11/11 j
                    'jrs 2008/12/22 If Session("mnuGroupsCaption") = "Using &G-Lists" Then
                    If Session("GroupOpt") = 2 Or Session("GroupOpt") = 3 Then
                        k = k + 1
                        If rsGL.Rows.Count = k Then
                            Exit Do
                        End If
                    Else
                        Exit Do
                    End If
                Loop
            End If
        Next i

    End Sub

    Private Sub LevelValidation()
        If optLevel.Checked Then
            If Not IsNumeric(txtPovLev.Text) Then
                txtPovLev.Text = ""
                LoadMessage("Invalid Poverty Level.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            If Val(txtPovLev.Text) < 0.5 Then
                txtPovLev.Text = ""
                LoadMessage("Poverty Level has to be greater than 0.5", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        ElseIf optRange.Checked Then
            If Not IsNumeric(txtIncomeR1.Text) Or Not IsNumeric(txtIncomeR2.Text) Then
                txtIncomeR1.Text = ""
                txtIncomeR2.Text = ""
                LoadMessage("Invalid Income Range.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            If Val(txtIncomeR1.Text) > Val(txtIncomeR2.Text) Then
                txtIncomeR1.Text = ""
                txtIncomeR2.Text = ""
                LoadMessage("Invalid Income Range.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            If Val(txtIncomeR1.Text) < 0.5 Then
                txtIncomeR1.Text = ""
                LoadMessage("Minimum Income Level has to be greater than 0.5", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        Else
            If Not IsNumeric(txtPct.Text) Then
                txtPct.Text = ""
                LoadMessage("Invalid Poverty Percentile.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            If Val(txtPct.Text) <= 0 Or Val(txtPct.Text) > 99 Then
                txtPct.Text = ""
                LoadMessage("Poverty Percentile has to be greater than 0 and at most 99.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        End If
    End Sub

    Private Sub mnuPercent_Click()
        If Session("mnuPctCaption") = "Using Millions of People" Then
            Session("mnuPctCaption") = "Using Percent of Population"
        Else
            Session("mnuPctCaption") = "Using Millions of People"
        End If
        cswmWebMenu.Groups("mnuPct").Caption = Session("mnuPctCaption")
        ConvertToFromPct()
    End Sub

    Private Sub mnu2005PPP_Click()
            'jrs 2024/03/12 no more 2005 PPP
            If Session("mnu2011PPP") = "Using 2017 PPP" Then
                Session("mnu2011PPP") = "Using 2011 PPP"
            txtPovLev.Text = "1.90"
            ElseIf Session("mnu2011PPP") = "Using 2011 PPP" Then
                '    Session("mnu2011PPP") = "Using 2005 PPP"
                '    txtPovLev.Text = "1.25"
                'Else
                Session("mnu2011PPP") = "Using 2017 PPP"
                txtPovLev.Text = "2.15"
        End If
        cswmWebMenu.Groups("mnu2005PPP").Caption = Session("mnu2011PPP")
        cmdClear_Click(Me, New EventArgs)
    End Sub

    Private Sub ConvertToFromPct()
        Dim i As Integer, j As Integer
        Dim POP() As Single
        ReDim POP(Session("LastYear") - Session("FirstYear") + 1)
        Dim iPos As Integer, iPos2 As Integer
        Dim iFileNum As Integer
        Dim bPctPop As Boolean
        bPctPop = (Session("mnuPctCaption") = "Using Percent of Population")

        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe

        For i = 1 To Session("COUNTE")
            'Read File Number
            iPos = InStr(Session("VARN")(1, i), "[")
            iPos2 = InStr(Session("VARN")(1, i), "]")
            iFileNum = Val(Mid(Session("VARN")(1, i), iPos + 1, iPos2 - iPos - 1))
            'Load Population Data
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("POP", POPidx, Session("GR1N")(i), -1, "", -1, "", -1, "", iFileNum, i = 1, i = Session("COUNTE"), Nothing, 1,
                POP, 0, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"),
                Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
            Else
                gLoadForDisplay("POP", POPidx, Session("GR1N")(i), -1, "", -1, "", -1, "", iFileNum, i = 1, i = Session("COUNTE"), Nothing, 1,
                    POP, 0, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"),
                    Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
            'Apply the convertion
            For j = 1 To Session("FinYear")(i) 'jrs 2011/06/17 Session("LastYear") - Session("FirstYear") + 1
                If bPctPop Then
                    Session("PLOT")(j, i) = Session("PLOT")(j, i) / POP(j) * 100
                Else
                    Session("PLOT")(j, i) = Session("PLOT")(j, i) * POP(j) / 100
                End If
            Next j
            'Change VarName
            If bPctPop Then
                Session("VARN")(0, i) = Session("VARN")(0, i) & "%"
                Session("VARN")(1, i) = Session("VARN")(1, i) & "%"
                Session("VARN")(2, i) = Session("VARN")(2, i) & " Percent of Population"
                Session("GR4N")(i) = "Percent"
            Else
                Session("VARN")(0, i) = Replace(Session("VARN")(0, i), "%", "")
                Session("VARN")(1, i) = Replace(Session("VARN")(1, i), "%", "")
                Session("VARN")(2, i) = Replace(Session("VARN")(2, i), " Percent of Population", "")
                Session("GR4N")(i) = "Mil People"
            End If
        Next i

        'Refresh Status Box
        If Session("COUNTE") > 0 Then
            Form_Activate()
        End If
    End Sub

    Protected Sub cmdTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTable.Click
        If Session("COUNTE") = 0 Then
            cmdAdd_Click(Me, New EventArgs)
            If Session("COUNTE") = 0 Then Exit Sub
        End If
        If Session("COUNTE") > 0 Then
            If Session("optPov") = "Pct" Or Session("optPov") = "Range" Then 'jrs 2011/06/20
                Support.FormManager.StateStore(Me)
            End If
            Support.FormManager.Show(Me, "frm_TableDisplay.aspx")
        End If

    End Sub

    Protected Sub optPct_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optPct.CheckedChanged
        If optPct.Checked Then
            Session("optPov") = "Pct"
            cswmWebMenu.Groups("mnuPct").ShowMenuBarItem = False
            'jrs 2026/07/14
            'ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            '    Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            '    Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            'Form_Activate()
        End If
    End Sub

    Protected Sub cmdLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLine.Click
        If Session("COUNTE") = 0 Then
            cmdAdd_Click(Me, New EventArgs)
            If Session("COUNTE") = 0 Then Exit Sub
        End If
        'jrs 2005/10/13 Session("TableTitle") = lstPacks.SelectedValue

        Session("GType") = "LINE"
        If Session("optPov") = "Pct" Or Session("optPov") = "Range" Then 'jrs 2011/06/20
            Support.FormManager.StateStore(Me)
        End If

        'jrs 2011/09/28 Support.FormManager.Show(Me, "frm_ProEssential.aspx")
        Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")

    End Sub

    Protected Sub cmdBar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBar.Click
        If Session("COUNTE") = 0 Then
            cmdAdd_Click(Me, New EventArgs)
            If Session("COUNTE") = 0 Then Exit Sub
        End If


        On Error GoTo DefaultInterval
        If Session("Interval") < 1 Then Session("Interval") = 1
        Session("GType") = "BAR"
        If Session("optPov") = "Pct" Or Session("optPov") = "Range" Then 'jrs 2011/06/20
            Support.FormManager.StateStore(Me)
        End If
        'jrs 2011/09/28 Support.FormManager.Show(Me, "frm_ProEssential.aspx")
        Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")

        Exit Sub

DefaultInterval:
        Session("Interval") = 1
        Resume Next

    End Sub

    Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))

        Form_Activate()
    End Sub

    Protected Sub optLevel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optLevel.CheckedChanged
        If optLevel.Checked Then
            Session("optPov") = "Level"
            cswmWebMenu.Groups("mnuPct").ShowMenuBarItem = True
            'jrs 2026/07/14
            'ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            '    Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            '    Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            'Form_Activate()
        End If
    End Sub

    'jrs 2007/08/20
    Protected Sub lbtDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtDelete.Click
        MnuDelete_Click(sender, e)
        pnlContext.Visible = False
        'jrs 10/20/04
        cmdAdd.Enabled = True
        lstFiles.Enabled = True

    End Sub

    'jrs 2007/08/20
    Protected Sub grd_VariableDisplay_CellClick(ByVal sender As Object, ByVal e As TMSWebControls.ModEventArgs) Handles grd_VariableDisplay.CellClick
        'jrs 10/20/04
        cmdAdd.Enabled = False
        lstFiles.Enabled = False

        pnlContext.Visible = True
        'jrs 2006/04/04
        lblRowCol.Visible = True
        lblRowCol.Text = grd_VariableDisplay(e.Column, 0)
        If grd_VariableDisplay(e.Column, 1) > "" Then
            lblRowCol.Text = lblRowCol.Text & ", " & grd_VariableDisplay(e.Column, 1)
        End If
        If grd_VariableDisplay(e.Column, 2) > "" Then
            lblRowCol.Text = lblRowCol.Text & ", " & grd_VariableDisplay(e.Column, 2)
        End If
        If grd_VariableDisplay(e.Column, 3) > "" Then
            lblRowCol.Text = lblRowCol.Text & ", " & grd_VariableDisplay(e.Column, 3)
        End If
        Session("VarSelGridCol") = e.Column
    End Sub

    'jrs 2007/08/20
    Public Sub MnuDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As Object
        Dim curr_col As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        curr_col = Session("VarSelGridCol") + 1
        Msg = Session("VARN")(1, curr_col) & "<br>" & Session("VARN")(2, curr_col) & "<br>"
        Msg = Msg + Session("GR1N")(curr_col) + "-" + Session("GR2N")(curr_col) + "-" + Session("GR3N")(curr_col)
        Msg = Msg + "<br>" + Chr(10) + Chr(10) + "Delete this varaiable?"
        DeleteVar(curr_col)
    End Sub

    'jrs 2007/08/21
    Public Sub DeleteVar(ByVal curr_col As Short)
        Dim iloop As Short
        Dim ICount As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 9/10/01 CREATED FOR COMPUTATIONS
        ' Delete Variable
        Session("COUNTE") = Session("COUNTE") - 1 ''Do later?
        Dim i2 As Integer
        For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
            For ICount = curr_col To Session("COUNTE")
                Session("PLOT")(i2, ICount) = Session("PLOT")(i2, ICount + 1)
            Next ICount
            Session("PLOT")(i2, Session("COUNTE") + 1) = 0
        Next i2

        'JRS 9/24/01 COMPUTATIONS
        Dim j1 As Object
        Dim uaf As Short
        uaf = UBound(Session("aFormulas")) - 1
        If Session("COUNTE") + 1 < uaf Then uaf = Session("COUNTE") + 1
        For j1 = curr_col To uaf  'jrs 2005/08/18 remove + 1 from curr_col
            'JRS 04/15/03
            If Not (Session("aFormulas")(j1 + 1) Is Nothing) Then
                Session("aFormulas")(j1) = Session("aFormulas")(j1 + 1)
            End If
        Next j1

        ' Take care of Grid Display & Array Variables Shifting
        For iloop = curr_col To Session("COUNTE") Step 1
            Session("FileNumbr")(iloop) = Session("FileNumbr")(iloop + 1)
            Session("VARN")(0, iloop) = Session("VARN")(0, iloop + 1)
            Session("VARN")(1, iloop) = Session("VARN")(1, iloop + 1)
            Session("VARN")(2, iloop) = Session("VARN")(2, iloop + 1)
            Session("VARN")(3, iloop) = Session("VARN")(3, iloop + 1)
            Session("VARN")(6, iloop) = Session("VARN")(6, iloop + 1)
            'mti Validation changes
            Session("VARN")(4, iloop) = Session("VARN")(4, iloop + 1)
            'mti Validation changes
            'mti Validation changes 19 August, 2002
            Session("VARN")(5, iloop) = Session("VARN")(5, iloop + 1)
            'mti Validation changes 19 August, 2002

            Session("GR1N")(iloop) = Session("GR1N")(iloop + 1)
            Session("GR2N")(iloop) = Session("GR2N")(iloop + 1)
            Session("GR3N")(iloop) = Session("GR3N")(iloop + 1)
            Session("GR4N")(iloop) = Session("GR4N")(iloop + 1)

            'jrs 10/27/04 Flexible Dimension Names for Graphs
            Session("FDNG1")(iloop) = Session("FDNG1")(iloop + 1)
            Session("FDNG2")(iloop) = Session("FDNG2")(iloop + 1)
            Session("FDNG3")(iloop) = Session("FDNG3")(iloop + 1)
            Session("FDNG4")(iloop) = Session("FDNG4")(iloop + 1)
            'jrs 2006/09/25 Use all available history
            Session("FinYear")(iloop) = Session("FinYear")(iloop + 1)
            Session("FinYearHis")(iloop) = Session("FinYearHis")(iloop + 1)
        Next iloop
        Session("VARN")(0, Session("COUNTE") + 1) = ""
        Session("VARN")(1, Session("COUNTE") + 1) = ""
        Session("VARN")(2, Session("COUNTE") + 1) = ""
        Session("VARN")(3, Session("COUNTE") + 1) = ""
        Session("VARN")(6, Session("COUNTE") + 1) = ""
        'mti Validation changes July 28, 2002
        Session("VARN")(4, Session("COUNTE") + 1) = ""
        'mti Validation changes July 28, 2002
        'mti Validation changes 19 August, 2002
        Session("VARN")(5, Session("COUNTE") + 1) = ""
        'mti Validation changes 19 August, 2002

        Session("GR1N")(Session("COUNTE") + 1) = ""
        Session("GR2N")(Session("COUNTE") + 1) = ""
        Session("GR3N")(Session("COUNTE") + 1) = ""
        Session("GR4N")(Session("COUNTE") + 1) = ""

        'jrs 10/27/04 Flexible Dimension Names for Graphs
        Session("FDNG1")(Session("COUNTE") + 1) = ""
        Session("FDNG2")(Session("COUNTE") + 1) = ""
        Session("FDNG3")(Session("COUNTE") + 1) = ""
        Session("FDNG4")(Session("COUNTE") + 1) = ""

        'jrs 2006/10/25
        If Not (grd_VariableDisplay Is Nothing) Then
            FillGrid()
        End If
    End Sub

    Protected Sub lbtCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtCancel.Click
        pnlContext.Visible = False
        cmdAdd.Enabled = True
        lstFiles.Enabled = True

    End Sub

    Protected Sub cmb_Horizon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Horizon.SelectedIndexChanged
        Dim Ind As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2008/12/18 added -1 because added firstyear
        Ind = cmb_Horizon.SelectedIndex - 1
        'jrs 2005/12/11
        If Session("Interval") Is Nothing Or Session("Interval") = 0 Then
            Session("Interval") = 1
        End If

        'JRS 11/03/03 CHANGE LASTYEAR WITH DISPLAYLASTYEAR
        If Ind >= 1 Then
            Session("DisplayLastYear") = Session("FirstYear") + Ind * Session("Interval") 'jrs 2005/12/11
        Else
            Session("DisplayLastYear") = Val(cmb_Horizon.SelectedValue)
            'jrs 2008/12/18 If Session("DisplayLastYear") > LastYearForecast Or Session("DisplayLastYear") < FirstYearForecast + 1 Then Exit Sub
            If Session("DisplayLastYear") > Session("LastYearForecast") Or Session("DisplayLastYear") < FirstYearHistory Then
                Session("DisplayLastYear") = Session("LastYear")
                LoadMessage("Invalid Horizon, reseting to " & Session("LastYear"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        End If
        cmb_Horizon.Visible = False
        frm_Horizon.Visible = False

    End Sub

    'jrs 2011/06/20
    Protected Sub optRange_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optRange.CheckedChanged
        If optRange.Checked Then
            Session("optPov") = "Range"
            cswmWebMenu.Groups("mnuPct").ShowMenuBarItem = True
            'jrs 2026/07/14
            'ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            '    Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            '    Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            'Form_Activate()
        End If
    End Sub

    Protected Sub lstFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        'jrs 2011/11/04
        Session("txtFiles") = txtFiles.Value
    End Sub
End Class
