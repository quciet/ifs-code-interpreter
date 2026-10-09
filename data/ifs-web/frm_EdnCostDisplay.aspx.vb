'jrs 2007/03/15 
'Conversion to the WEB and original design by Jose R. Solorzano
Partial Class frm_EdnCostDisplay
    Inherits Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()

        'jrs 2005/04/22
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then 'jrs 2011/06/20
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

        'jrs 2006/12/18
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16002);"\
        'jrs 2013/09/04
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'Put user code to initialize the page here
        If Not Page.IsPostBack Then

            Session("TableDisplayParent") = "frm_EdnCostDisplay.aspx"
            'jrs 2011/09/28 Session("ProEssentialParent") = "frm_EdnCostDisplay.aspx"
            Session("GraphDisplayParent") = "frm_EdnCostDisplay.aspx"
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
            'Load Target Years
            LoadTargetStartYears()

            If Session("ClearEd") Is Nothing Then
                'Clear Variables, to handle switch between whole and percent
                ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                    Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))

                Form_Activate()
            Else
                txtPriTarget.Text = Session("txtPriTarget")
                txtSecTarget.Text = Session("txtSecTarget")
                txtTerTarget.Text = Session("txtTerTarget")

                chkCostDiffFromPop.Checked = Session("chkCostDiffFromPop")
                chkGDPPcnt.Checked = Session("chkGDPPcnt")
                chkGovConPcnt.Checked = Session("chkGovConPcnt")
                chkPrimary.Checked = Session("chkPrimary")
                chkSecondary.Checked = Session("chkSecondary")
                chkShowAll.Checked = Session("chkShowAll")
                chkShowEnrDiff.Checked = Session("chkShowEnrDiff")
                chkShowStudentsDiff.Checked = Session("chkShowStudentsDiff")
                chkShowUnitCost.Checked = Session("chkShowUnitCost")
                chkTertiary.Checked = Session("chkTertiary")
                chkUseFlatCost.Checked = Session("chkUseFlatCost")
                chkUseFlatEnr.Checked = Session("chkUseFlatEnr")

                cmdPriEnrType.Text = Session("cmdPriEnrType")
                Select Case cmdPriEnrType.Text
                    Case "Using Net Enrollment"
                        cmdPriEnrType.AccessKey = "N"
                    Case "Using Both Gross and Net Enrollment"
                        cmdPriEnrType.AccessKey = "B"
                End Select
                cmdPriEnrType.ToolTip = cmdPriEnrType.Text & " (Alt + " & cmdPriEnrType.AccessKey & ")"
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
                Session("CurrencyParent") = "frm_EdnCostDisplay.aspx"
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
        Session("DisplayControlParent") = "frm_EdnCostDisplay.aspx"
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

        txtHorizon.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdHorizon.ClientID + "')")
        cswmWebMenu.Visible = False
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
        Session("ClearEd") = Nothing
        Session.Remove("GroupOpt")

        Session.Remove("chkCostDiffFromPop")
        Session.Remove("chkGDPPcnt")
        Session.Remove("chkGovConPcnt")
        Session.Remove("chkPrimary")
        Session.Remove("chkSecondary")
        Session.Remove("chkShowAll")
        Session.Remove("chkShowEnrDiff")
        Session.Remove("chkShowStudentsDiff")
        Session.Remove("chkShowUnitCost")
        Session.Remove("chkTertiary")
        Session.Remove("chkUseFlatCost")
        Session.Remove("chkUseFlatEnr")

        Session.Remove("cmdPriEnrType")

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
        'jrs 2026/01/26 changed default to base case
        lstFiles.SelectedIndex = 1
        lstFiles.Items(1).Selected = True
    End Sub

    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If TargetValidation() Then
            'Load Poverty Display
            LoadEdnCostDisplay()
            'Refresh Status Box
            Form_Activate()
        End If
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

    Private Sub LoadEdnCostDisplay()
        'need validation for the level
        Session("txtPriTarget") = txtPriTarget.Text
        Session("txtSecTarget") = txtSecTarget.Text
        Session("txtTerTarget") = txtTerTarget.Text
        Session("ClearEd") = False

        Session("chkCostDiffFromPop") = chkCostDiffFromPop.Checked
        Session("chkGDPPcnt") = chkGDPPcnt.Checked
        Session("chkGovConPcnt") = chkGovConPcnt.Checked
        Session("chkPrimary") = chkPrimary.Checked
        Session("chkSecondary") = chkSecondary.Checked
        Session("chkShowAll") = chkShowAll.Checked
        Session("chkShowEnrDiff") = chkShowEnrDiff.Checked
        Session("chkShowStudentsDiff") = chkShowStudentsDiff.Checked
        Session("chkShowUnitCost") = chkShowUnitCost.Checked
        Session("chkTertiary") = chkTertiary.Checked
        Session("chkUseFlatCost") = chkUseFlatCost.Checked
        Session("chkUseFlatEnr") = chkUseFlatEnr.Checked

        Session("cmdPriEnrType") = cmdPriEnrType.Text

        Dim dbGL As String
        dbGL = Session("IFs_MDB2Active")
        Dim rsGL As DataTable

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
        Dim i As Integer, k As Integer, PriEnrType As String

        If cmdPriEnrType.Text = "Using Gross Enrollment" Then
            PriEnrType = "Gross"
        ElseIf cmdPriEnrType.Text = "Using Net Enrollment" Then
            PriEnrType = "Net"
        Else
            PriEnrType = "Both"
        End If

        For i = 0 To lstReg.Items.Count - 1
            Session("RegIdxs")(i) = lstReg.Items(i).Selected
            If lstReg.Items(i).Selected Then
                'jrs 2008/12/22 If Session("mnuGroupsCaption") = "Using &G-Lists" Then
                If Session("GroupOpt") = 2 Then
                    SetDataTablefromAccess(dbGL, "SELECT * FROM GList d, GListId i WHERE i.Code = d.GListCode AND i.Description = """ & lstReg.Items(i).Text & """ ORDER BY MemberOrder, Region", "", rsGL)
                    k = 0
                ElseIf Session("GroupOpt") = 3 Then
                    SetDataTablefromAccess(dbGL, "SELECT * FROM " & GroupIDNew & " i, " &
                        Grouping & " d WHERE i.GroupNum = d.GroupNo AND i.GroupName = """ &
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
                            If chkPrimary.Checked Then
                                'jrs 2008/12/22 If Session("mnuGroupsCaption") = "Using &G-Lists" Then
                                If Session("GroupOpt") = 2 Then
                                    gLoadEdnCostDisp(txtPriTarget.Text, CInt(cmb_TrgtStartYr.Text), CInt(cmb_TrgtEndYr.Text),
                                        "EDPRIENRG", "EDPRIENRN", PriEnrType, "EDPRITOT", "EDEXPERPRI", "EDPRICOST", rsGL.Rows(k).Item("Region"), "Total", j, rsGL.Rows(k).Item("Group"),
                                        chkShowUnitCost.Checked, chkGDPPcnt.Checked, chkGovConPcnt.Checked, chkShowEnrDiff.Checked, chkShowStudentsDiff.Checked,
                                        chkShowAll.Checked, chkCostDiffFromPop.Checked, chkUseFlatEnr.Checked, chkUseFlatCost.Checked,
                                        Session("PLOT"), Session("COUNTE"), Session("FirstYear"), Session("LastYear"),
                                        Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"))
                                ElseIf Session("GroupOpt") = 3 Then 'decomposed groups
                                    gLoadEdnCostDisp(txtPriTarget.Text, CInt(cmb_TrgtStartYr.Text), CInt(cmb_TrgtEndYr.Text),
                                        "EDPRIENRG", "EDPRIENRN", PriEnrType, "EDPRITOT", "EDEXPERPRI", "EDPRICOST", rsGL.Rows(k).Item("Region"), "Total", j, False,
                                        chkShowUnitCost.Checked, chkGDPPcnt.Checked, chkGovConPcnt.Checked, chkShowEnrDiff.Checked, chkShowStudentsDiff.Checked,
                                        chkShowAll.Checked, chkCostDiffFromPop.Checked, chkUseFlatEnr.Checked, chkUseFlatCost.Checked,
                                        Session("PLOT"), Session("COUNTE"), Session("FirstYear"), Session("LastYear"),
                                        Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"))
                                Else 'country/region or group
                                    gLoadEdnCostDisp(txtPriTarget.Text, CInt(cmb_TrgtStartYr.Text), CInt(cmb_TrgtEndYr.Text),
                                        "EDPRIENRG", "EDPRIENRN", PriEnrType, "EDPRITOT", "EDEXPERPRI", "EDPRICOST", lstReg.Items(i).Text, "Total", j, (Session("GroupOpt") = 1),
                                        chkShowUnitCost.Checked, chkGDPPcnt.Checked, chkGovConPcnt.Checked, chkShowEnrDiff.Checked, chkShowStudentsDiff.Checked,
                                        chkShowAll.Checked, chkCostDiffFromPop.Checked, chkUseFlatEnr.Checked, chkUseFlatCost.Checked,
                                        Session("PLOT"), Session("COUNTE"), Session("FirstYear"), Session("LastYear"),
                                        Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"))
                                End If
                                Call AdjustCountE()
                            End If

                            If chkSecondary.Checked Then 'not primary/ secondary/ tetiary
                                If Session("GroupOpt") = 2 Then
                                    gLoadEdnCostDisp(txtSecTarget.Text, CInt(cmb_TrgtStartYr.Text), CInt(cmb_TrgtEndYr.Text),
                                        "EDSECENRG", "", "Gross", "EDSECTOT", "EDEXPERSEC", "EDSECCOST", rsGL.Rows(k).Item("Region"), "Total", j, rsGL.Rows(k).Item("Group"),
                                        chkShowUnitCost.Checked, chkGDPPcnt.Checked, chkGovConPcnt.Checked, chkShowEnrDiff.Checked, chkShowStudentsDiff.Checked,
                                        chkShowAll.Checked, chkCostDiffFromPop.Checked, chkUseFlatEnr.Checked, chkUseFlatCost.Checked,
                                        Session("PLOT"), Session("COUNTE"), Session("FirstYear"), Session("LastYear"),
                                        Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"))
                                ElseIf Session("GroupOpt") = 3 Then 'decomposed groups
                                    gLoadEdnCostDisp(txtSecTarget.Text, CInt(cmb_TrgtStartYr.Text), CInt(cmb_TrgtEndYr.Text),
                                        "EDSECENRG", "", "Gross", "EDSECTOT", "EDEXPERSEC", "EDSECCOST", rsGL.Rows(k).Item("Region"), "Total", j, False,
                                        chkShowUnitCost.Checked, chkGDPPcnt.Checked, chkGovConPcnt.Checked, chkShowEnrDiff.Checked, chkShowStudentsDiff.Checked,
                                        chkShowAll.Checked, chkCostDiffFromPop.Checked, chkUseFlatEnr.Checked, chkUseFlatCost.Checked,
                                        Session("PLOT"), Session("COUNTE"), Session("FirstYear"), Session("LastYear"),
                                        Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"))
                                Else 'country/region or group
                                    gLoadEdnCostDisp(txtSecTarget.Text, CInt(cmb_TrgtStartYr.Text), CInt(cmb_TrgtEndYr.Text),
                                        "EDSECENRG", "", "Gross", "EDSECTOT", "EDEXPERSEC", "EDSECCOST", lstReg.Items(i).Text, "Total", j, (Session("GroupOpt") = 1),
                                        chkShowUnitCost.Checked, chkGDPPcnt.Checked, chkGovConPcnt.Checked, chkShowEnrDiff.Checked, chkShowStudentsDiff.Checked,
                                        chkShowAll.Checked, chkCostDiffFromPop.Checked, chkUseFlatEnr.Checked, chkUseFlatCost.Checked,
                                        Session("PLOT"), Session("COUNTE"), Session("FirstYear"), Session("LastYear"),
                                        Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"))
                                End If
                                Call AdjustCountE()
                            End If

                            If chkTertiary.Checked Then  'not primary/ secondary/ tetiary
                                'jrs 2008/12/22 If Session("mnuGroupsCaption") = "Using &G-Lists" Then
                                If Session("GroupOpt") = 2 Then
                                    gLoadEdnCostDisp(txtTerTarget.Text, CInt(cmb_TrgtStartYr.Text), CInt(cmb_TrgtEndYr.Text),
                                        "EDTERENRG", "", "Gross", "EDTERTOT", "EDEXPERTER", "EDTERCOST", rsGL.Rows(k).Item("Region"), "Total", j, rsGL.Rows(k).Item("Group"),
                                        chkShowUnitCost.Checked, chkGDPPcnt.Checked, chkGovConPcnt.Checked, chkShowEnrDiff.Checked, chkShowStudentsDiff.Checked,
                                        chkShowAll.Checked, chkCostDiffFromPop.Checked, chkUseFlatEnr.Checked, chkUseFlatCost.Checked,
                                        Session("PLOT"), Session("COUNTE"), Session("FirstYear"), Session("LastYear"),
                                        Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"))
                                ElseIf Session("GroupOpt") = 3 Then 'decomposed groups
                                    gLoadEdnCostDisp(txtTerTarget.Text, CInt(cmb_TrgtStartYr.Text), CInt(cmb_TrgtEndYr.Text),
                                        "EDTERENRG", "", "Gross", "EDTERTOT", "EDEXPERTER", "EDTERCOST", rsGL.Rows(k).Item("Region"), "Total", j, False,
                                        chkShowUnitCost.Checked, chkGDPPcnt.Checked, chkGovConPcnt.Checked, chkShowEnrDiff.Checked, chkShowStudentsDiff.Checked,
                                        chkShowAll.Checked, chkCostDiffFromPop.Checked, chkUseFlatEnr.Checked, chkUseFlatCost.Checked,
                                        Session("PLOT"), Session("COUNTE"), Session("FirstYear"), Session("LastYear"),
                                        Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"))
                                Else 'country/region or group
                                    gLoadEdnCostDisp(txtTerTarget.Text, CInt(cmb_TrgtStartYr.Text), CInt(cmb_TrgtEndYr.Text),
                                        "EDTERENRG", "", "Gross", "EDTERTOT", "EDEXPERTER", "EDTERCOST", lstReg.Items(i).Text, "Total", j, (Session("GroupOpt") = 1),
                                        chkShowUnitCost.Checked, chkGDPPcnt.Checked, chkGovConPcnt.Checked, chkShowEnrDiff.Checked, chkShowStudentsDiff.Checked,
                                        chkShowAll.Checked, chkCostDiffFromPop.Checked, chkUseFlatEnr.Checked, chkUseFlatCost.Checked,
                                        Session("PLOT"), Session("COUNTE"), Session("FirstYear"), Session("LastYear"),
                                        Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                        Session("SensitivityFlag"))
                                End If
                                Call AdjustCountE()
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

    Private Function TargetValidation() As Boolean
        TargetValidation = True
        lblMsg.Visible = False

        If chkPrimary.Checked Then
            Dim ValPriTar As Single
            ValPriTar = Val(txtPriTarget.Text)
            If Not (ValPriTar >= 5 And ValPriTar <= 100) Then
                txtPriTarget.Text = ""
                TargetValidation = False
                lblMsg.Text = "Invalid Primary Enrollment Target. Please put a value between 5 and 100."
                lblMsg.Visible = True
            End If
        Else
            txtPriTarget.Text = ""
        End If

        If chkSecondary.Checked Then
            Dim ValSecTar As Single
            ValSecTar = Val(txtSecTarget.Text)
            If Not (ValSecTar >= 5 And ValSecTar <= 100) Then
                txtSecTarget.Text = ""
                TargetValidation = False
                lblMsg.Text = "Invalid Secondary Enrollment Target. Please put a value between 5 and 100."
                lblMsg.Visible = True
            End If
        Else
            txtSecTarget.Text = ""
        End If

        If chkTertiary.Checked Then
            Dim ValTerTar As Single
            ValTerTar = Val(txtTerTarget.Text)
            If Not (ValTerTar >= 5 And ValTerTar <= 100) Then
                txtTerTarget.Text = ""
                TargetValidation = False
                lblMsg.Text = "Invalid Tertiary Enrollment Target. Please put a value between 5 and 100."
                lblMsg.Visible = True
            End If
        Else
            txtTerTarget.Text = ""
        End If


    End Function


    Protected Sub cmdTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTable.Click
        If Session("COUNTE") = 0 Then
            cmdAdd_Click(Me, New EventArgs)
            If Session("COUNTE") = 0 Then Exit Sub
        End If
        If Session("COUNTE") > 0 Then
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_TableDisplay.aspx")
        End If

    End Sub

    Protected Sub cmdLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLine.Click
        If Session("COUNTE") = 0 Then
            cmdAdd_Click(Me, New EventArgs)
            If Session("COUNTE") = 0 Then Exit Sub
        End If
        'jrs 2005/10/13 Session("TableTitle") = lstPacks.SelectedValue

        Session("GType") = "LINE"

        'jrs 2011/09/28 Support.FormManager.Show(Me, "frm_ProEssential.aspx")
        Support.FormManager.StateStore(Me)
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
        Support.FormManager.StateStore(Me)
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

        cswmWebMenu.Visible = True
    End Sub


    Protected Sub lstFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        'jrs 2011/11/04
        Session("txtFiles") = txtFiles.Value
    End Sub

    Private Sub AdjustCountE()
        'jrs 2012/07/06 Cint(chkbox) = -1 when true

        'increase count for showing cost differential due to population change only
        Session("COUNTE") = Session("COUNTE") - CInt(chkCostDiffFromPop.Checked)

        'increase session("COUNTE") for cost differential and costs
        Session("COUNTE") = Session("COUNTE") - CInt(chkShowAll.Checked) * 2

        'increase session("COUNTE") for Unit cost
        Session("COUNTE") = Session("COUNTE") - CInt(chkShowUnitCost.Checked)

        'increase session("COUNTE") for GDP% displays
        Session("COUNTE") = Session("COUNTE") - CInt(chkGDPPcnt.Checked) + CInt(chkShowAll.Checked) * CInt(chkGDPPcnt.Checked) * 2
        'increase session("COUNTE") for GOVCON% displays
        Session("COUNTE") = Session("COUNTE") - CInt(chkGovConPcnt.Checked) + CInt(chkShowAll.Checked) * CInt(chkGovConPcnt.Checked) * 2
        'increase session("COUNTE") for enrolment rate differential and enrolment rates
        Session("COUNTE") = Session("COUNTE") - CInt(chkShowEnrDiff.Checked) + CInt(chkShowAll.Checked) * CInt(chkShowEnrDiff.Checked) * 2
        'increase session("COUNTE") for students differential and students
        Session("COUNTE") = Session("COUNTE") - CInt(chkShowStudentsDiff.Checked) + CInt(chkShowAll.Checked) * CInt(chkShowStudentsDiff.Checked) * 2
    End Sub

    'jrs 2012/07/06
    Private Sub LoadTargetStartYears()
        'jrs 2005/12/11
        If Session("Interval") = 0 Then
            Session("Interval") = 1
        End If

        Dim iCount As Integer
        For iCount% = Session("FirstYear") To Session("LastYearForecast") Step Session("Interval")
            cmb_TrgtStartYr.Items.Add(iCount%)
        Next iCount%

        cmb_TrgtStartYr.SelectedIndex = 0
        cmb_TrgtStartYr_SelectedIndexChanged(Me, New EventArgs)
        '    TrgtStartYr = FirstYear%
        '    LoadTargetEndYears


    End Sub

    Private Sub LoadTargetEndYears()
        'jrs 2005/12/11
        If Session("Interval") = 0 Then
            Session("Interval") = 1
        End If
        cmb_TrgtEndYr.Items.Clear()
        'For ICount% = TrgtStartYr + Interval To LastYearForecast% Step Interval
        Dim iCount As Integer
        For iCount% = Session("trgtStartYr") To Session("LastYearForecast") Step Session("Interval")
            cmb_TrgtEndYr.Items.Add(iCount%)
        Next iCount%
        Dim Ind2015 As Integer
        Ind2015 = AMAX(1, 2015 - Session("trgtStartYr"))
        If Ind2015 > cmb_TrgtEndYr.Items.Count - 1 Then Ind2015 = cmb_TrgtEndYr.Items.Count - 1
        cmb_TrgtEndYr.SelectedIndex = Ind2015
    End Sub

    Protected Sub cmb_TrgtStartYr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_TrgtStartYr.SelectedIndexChanged
        Session("trgtStartYr") = Val(cmb_TrgtStartYr.Text)
        LoadTargetEndYears()
    End Sub

    Protected Sub cmdHorizon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHorizon.Click
        If Not IsNumeric(txtHorizon.Text) Then
            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("LastYear"), Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        If Session("FirstYear") = FirstYearForecast Then
            If CInt(txtHorizon.Text) > Session("LastYearForecast") Or CInt(txtHorizon.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearForecast) & " and " & Session("LastYearForecast"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If CInt(txtHorizon.Text) > Session("LastYearForecast") Or CInt(txtHorizon.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearHistory) & " and " & CInt(LastYearHistory), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        End If

        If Session("Interval") > 1 Then
            If (CInt(txtHorizon.Text) - Session("FirstYear")) Mod Session("Interval") <> 0 Then
                LoadMessage("Invalid Year, year must follow the existing Interval, please use the combo box.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        End If

        cmb_Horizon.Text = txtHorizon.Text
        cmb_Horizon_SelectedIndexChanged(Me, New EventArgs)

    End Sub

    Protected Sub cmdCheckAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCheckAll.Click
        Dim mcontrol As Control
        For Each mcontrol In Me.Controls
            If mcontrol.Controls.Count > 0 Then
                Dim c As Control
                For Each c In mcontrol.Controls
                    If TypeOf c Is CheckBox Then
                        If InStr(CType(c, CheckBox).Text, "Show") > 0 Then
                            CType(c, CheckBox).Checked = True
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    Protected Sub cmdUncheckAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUncheckAll.Click
        Dim mcontrol As Control
        For Each mcontrol In Me.Controls
            If mcontrol.Controls.Count > 0 Then
                Dim c As Control
                For Each c In mcontrol.Controls
                    If TypeOf c Is CheckBox Then
                        If InStr(CType(c, CheckBox).Text, "Show") > 0 Then
                            CType(c, CheckBox).Checked = False
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    Protected Sub cmdPriEnrType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPriEnrType.Click
        'button for primary enrollment types - toggle between net and gross
        If cmdPriEnrType.Text = "Using Gross Enrollment" Then
            cmdPriEnrType.Text = "Using Net Enrollment"
            cmdPriEnrType.AccessKey = "N"
            cmdPriEnrType.ToolTip = "Using Net Enrollment (Alt+N)"
        ElseIf cmdPriEnrType.Text = "Using Net Enrollment" Then
            cmdPriEnrType.Text = "Using Both Gross and Net Enrollment"
            cmdPriEnrType.AccessKey = "B"
            cmdPriEnrType.ToolTip = "Using Both Gross and Net Enrollment (Alt+B)"
        Else
            cmdPriEnrType.Text = "Using Gross Enrollment"
            cmdPriEnrType.AccessKey = "G"
            cmdPriEnrType.ToolTip = "Using Gross Enrollment (Alt+G)"
        End If
    End Sub
End Class

